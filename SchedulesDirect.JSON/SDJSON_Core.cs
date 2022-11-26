using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Script.Serialization;

namespace SchedulesDirect {
    public class SDJsonCore : SDJsonErrorHandling {

#if DEBUG
        private static readonly string DEBUG_FILE = "SDGrabSharp.debug.txt";
#endif

#if DEBUG
        private static void DebugLog(string debugText) {
            string logStamp = DateTime.Now.ToString("O");
            File.AppendAllText(DEBUG_FILE, $"{logStamp}: {debugText}");
        }
#endif

        private static readonly string urlBase = "https://json.schedulesdirect.org/20141201/";
        private static readonly string userAgentDefault = "SDJSharp JSON C# Library/1.0 (https://github.com/M0OPK/SDJSharp)";
        private static readonly string userAgentShort = "SDJSharp JSON C# Library/1.0";
        private static string userAgentFull;

		protected SDJsonCore(string clientUserAgent = "") {
            userAgentFull = string.IsNullOrEmpty(clientUserAgent) ? userAgentDefault : $"{userAgentShort} ({clientUserAgent})";
        }
		
        // For cases where we can't create a known object type
        // Parse JSON string and return dynamic type
		protected static dynamic GetDynamic(string jsonstring) {
			var ser = new JavaScriptSerializer();
            return ser.Deserialize<dynamic>(jsonstring);
        }
		
        // Parse known class object and return JSON string
        protected static string CreateJSONstring<T>(T obj) {
            var jsonStream = new MemoryStream();
			var jsonSer = new DataContractJsonSerializer(typeof(T), new DataContractJsonSerializerSettings {
                DateTimeFormat = new DateTimeFormat("yyyy-MM-ddTHH:mm:ssZ"),
            });
            jsonSer.WriteObject(jsonStream, obj);

            jsonStream.Position = 0;
            var sr = new StreamReader(jsonStream);
            return sr.ReadToEnd();
        }

        // Parse JSON string and return known class object
        protected static T ParseJSON<T>(string input) {
            if (input == string.Empty)
                return default(T);

            var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(input));
            var jsonSer = new DataContractJsonSerializer(typeof(T), new DataContractJsonSerializerSettings {
                DateTimeFormat = new DateTimeFormat("yyyy-MM-ddTHH:mm:ssZ"),
            });

            return (T)jsonSer.ReadObject(jsonStream);
        }

        // Parse incoming known object with JSON serializer.
        // Perform post action and parse response via JSON serializer to known object type
        protected static V PostJSON<V, T>(string command, T obj, string token = "", WebHeaderCollection headers = null) {
            var requestString = CreateJSONstring(obj);
#if DEBUG
            DebugLog($"JSON Post [{command}] Request: {requestString}{Environment.NewLine}");
#endif
			var response = WebPost(command, requestString, token, headers);
			var result = ParseJSON<V>(response);
#if DEBUG
            DebugLog($"JSON Post Response: {response}{Environment.NewLine}");
#endif
			return result;
		}
		
        // Perform get action and parse response via JSON serializer to known object type
        protected static T GetJSON<T>(string command, string token = "", WebHeaderCollection headers = null) {
            return ParseJSON<T>(WebGet(command, token, headers));
        }
		
        // Perform put action and parse response via JSON serializer to known object type
        protected static T PutJSON<T>(string command, string token = "", WebHeaderCollection headers = null) {
            return ParseJSON<T>(WebPut(command, token, headers));
        }
		
        protected static T DeleteJSON<T>(string command, string token = "", WebHeaderCollection headers = null) {
            return ParseJSON<T>(WebDelete(command, token, headers));
        }
		
        // Handle get request, return response as string
        protected static string WebGet(string command, string token = "", WebHeaderCollection headers = null) {
            var getRequest = WebAction(urlBase + command, "GET", token, headers);

            try {
                var resp = (HttpWebResponse)getRequest.GetResponse();
                using (var sr = new StreamReader(resp.GetResponseStream())) {
                    return sr.ReadToEnd();
                }
            }
            catch (WebException ex) when (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null) {
                using (var resp = (HttpWebResponse)ex.Response) {
                    using (var sr = new StreamReader(resp.GetResponseStream())) {
                        if (ParseJSON<SDErrorResponse>(sr.ReadToEnd()) is SDErrorResponse response) {
                            throw new SchedulesDirectException(response.Response, response.Code, response.DateTime, response.ServerID, response.Message, ex);
                        }
                    }
                }
                throw; // response wasn't an SD error so re-throw the original error
            }
            //} catch (System.Exception ex) {
            //    queueError(ex);
            //    return "";
            //}
		}
		
		// Handle put request, return response as string
        protected static string WebPut(string command, string token = "", WebHeaderCollection headers = null) {
            var putRequest = WebAction(urlBase + command, "PUT", token, headers);

            try {
                putRequest.Timeout = 5000;
                var resp = (HttpWebResponse)putRequest.GetResponse();
                using (var sr = new StreamReader(resp.GetResponseStream())) {
                    return sr.ReadToEnd();
                }
            }
            catch (WebException ex) when (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null) {
                using (var resp = (HttpWebResponse)ex.Response) {
                    using (var sr = new StreamReader(resp.GetResponseStream())) {
                        if (ParseJSON<SDErrorResponse>(sr.ReadToEnd()) is SDErrorResponse response) {
                            throw new SchedulesDirectException(response.Response, response.Code, response.DateTime, response.ServerID, response.Message, ex);
                        }
                    }
                }
                throw; // response wasn't an SD error so re-throw the original error
            }
			//} catch (System.Exception ex) {
			//    queueError(ex);
			//    return "";
			//}
		}
		
        // Handle delete request, return response as string
        protected static string WebDelete(string command, string token = "", WebHeaderCollection headers = null) {
            var deleteRequest = WebAction(urlBase + command, "DELETE", token, headers);

            try {
                deleteRequest.Timeout = 5000;
                var resp = (HttpWebResponse)deleteRequest.GetResponse();
                using (var sr = new StreamReader(resp.GetResponseStream())) {
                    return sr.ReadToEnd();
                }
            }
            catch (WebException ex) when (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null) {
                using (var resp = (HttpWebResponse)ex.Response) {
                    using (var sr = new StreamReader(resp.GetResponseStream())) {
                        if (ParseJSON<SDErrorResponse>(sr.ReadToEnd()) is SDErrorResponse response) {
                            throw new SchedulesDirectException(response.Response, response.Code, response.DateTime, response.ServerID, response.Message, ex);
                        }
                    }
                }
                throw; // response wasn't an SD error so re-throw the original error
            }
			//} catch (System.Exception ex) {
			//    queueError(ex);
			//    return "";
			//}
		}
		
        // Handle post request, return response as string
        protected static string WebPost(string command, string jsonstring, string token = "", WebHeaderCollection headers = null) {
            var postRequest = WebAction(urlBase + command, "POST", token, headers);

            using (var sr = new StreamWriter(postRequest.GetRequestStream())) {
                sr.Write(jsonstring);
                sr.Flush();
            }

            try {
				using (var resp = (HttpWebResponse)postRequest.GetResponse()) {
					using (var sr = new StreamReader(resp.GetResponseStream())) {
						return sr.ReadToEnd();
					}
				}
			}
            catch (WebException ex) when (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null) {
                using (var resp = (HttpWebResponse)ex.Response) {
                    using (var sr = new StreamReader(resp.GetResponseStream()))
                    {
                        if (ParseJSON<SDErrorResponse>(sr.ReadToEnd()) is SDErrorResponse response) {
                            throw new SchedulesDirectException(response.Response, response.Code, response.DateTime, response.ServerID, response.Message, ex);
                        }
                    }
                }
                throw; // response wasn't an SD error so re-throw the original error
            }
            //} catch (Exception ex) {
            //    addError(ex);
            //    return "";
            //}
		}
		
        // Create web request for specified action and URL
        private static HttpWebRequest WebAction(string url, string action = "GET", string token = "", WebHeaderCollection headers = null) {
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = action;
            webRequest.ContentType = "application/json; charset=utf-8";
            webRequest.Accept = "application/json; charset=utf-8";
            webRequest.UserAgent = userAgentFull;
            webRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            if (headers != null)
                webRequest.Headers = headers;
            if (token != "")
                webRequest.Headers.Add("token: " + token);
            return webRequest;
        }
    }
}
