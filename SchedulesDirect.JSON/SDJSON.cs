using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using System.Globalization;

namespace SchedulesDirect
{
    public class SDJson : SDJsonCore {
        private string _loginToken;
		
		public SDJson(string clientUserAgent = "", string token = "") : base(clientUserAgent) {
            // If token supplied, use it
            if (token != string.Empty)
                _loginToken = token;
        }
		
        /// <summary>
        /// Provide password hash in correct format for ScheduleDirect login
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string HashPassword(string password) {
            var passBytes = Encoding.UTF8.GetBytes(password);
            var hash = SHA1.Create();
            var hashBytes = hash.ComputeHash(passBytes);
            var hexString = BitConverter.ToString(hashBytes);
            return hexString.Replace("-", "").ToLower();
        }
		
		public bool LoggedIn 
			=> !string.IsNullOrEmpty(_loginToken);
		
        /// <summary>
        /// Log in, and retrieve login token for session
        /// </summary>
        /// <param name="credential"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="SchedulesDirectTokenResponseException"></exception>
        /// <exception cref="SchedulesDirectException"></exception>"
        public SDTokenResponse Login(NetworkCredential credential) {
            if (credential is null)
                throw new ArgumentNullException(nameof(credential), "Credential cannot be null");
            return Login(credential.UserName, credential.Password);
        }
        /// <summary>
        /// Log in, and retrieve login token for session
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="SchedulesDirectTokenResponseException"></exception>
        /// <exception cref="SchedulesDirectException"></exception>"
        public SDTokenResponse Login(string username, string password, bool isHash = false) {
			try {
				if (username is null)
                	throw new ArgumentNullException(nameof(username), "Username cannot be null");
            	if (username == string.Empty)
                	throw new ArgumentException("Username cannot be blank", nameof(username));
            	if (password is null)
                	throw new ArgumentNullException(nameof(password), "Password cannot be null");
            	if (password == string.Empty)
                	throw new ArgumentException("Password cannot be blank", nameof(password));
                var passHash = isHash ? password : HashPassword(password);
                var response = PostJSON<SDTokenResponse, SDTokenRequest>("token", new SDTokenRequest(username, passHash));
                if (response is null) // || response.Code != 0) {
                    return null;
                if (response.Code != 0)
                    throw new SchedulesDirectTokenResponseException(response);
                _loginToken = response.Token;
                return response;
            }
            catch (Exception ex) {
                AddError(ex);
            }
            return null;
        }
		
        /// <summary>
        /// Get system status, and account lineups (Requires login token)
        /// </summary>
        /// <returns></returns>
        public SDStatusResponse GetStatus() {
            try {
                if (_loginToken == string.Empty)
                    return null;
                var response = GetJSON<SDStatusResponse>("status", _loginToken);
                if (response is null)
                    return null;
                if (response.Code != 0)
                    throw new SchedulesDirectException(response);
                return response;
            }
			catch (Exception ex) {
                AddError(ex);
            }
            return null;
        }
		
        /// <summary>
        /// Returns version information for specified client name
        /// </summary>
        /// <param name="clientname"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="SchedulesDirectException"></exception>
        public SDVersionResponse GetVersion(string clientname) {
            try {
                if (clientname is null)
                    throw new ArgumentNullException(nameof(clientname), "");
                if (clientname == string.Empty)
                    throw new ArgumentException("", nameof(clientname));
                SDVersionResponse response = GetJSON<SDVersionResponse>("token/" + clientname);
                if (response is null)
                    return null;
                if (response.Code != 0)
                    throw new SchedulesDirectException(response);
                return response;
            }
            catch (Exception ex) {
                AddError(ex);
            }
            return null;
        }
		
        /// <summary>
        /// Returns list of available services
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SDAvailableResponse> GetAvailable() {
            try {
                return GetJSON<IEnumerable<SDAvailableResponse>>("available");
            }
            catch (Exception ex) {
                AddError(ex);
            }
            return null;
        }
		
        /// <summary>
        /// Returns list of countries supported.
        /// </summary>
        /// <returns></returns>
        public SDCountries GetCountries() {
            try {
                var result = GetDynamic(WebGet("available/countries"));
                if (result is null)
                    return null;
                var countries = new SDCountries();
                foreach (string key in result.Keys) {
                    var thisContinent = new SDCountries.Continent { ContinentName = key };
                    foreach (var country in result[key]) {
                        if (country == null)
                            continue;
                        var thisCountry = new SDCountries.Country();
                        try { thisCountry.FullName = country["fullName"]; } catch { }
                        try { thisCountry.ShortName = country["shortName"]; } catch { }
                        try { thisCountry.PostalCodeExample = country["postalCodeExample"]; } catch { }
                        try { thisCountry.PostalCode = country["postalCode"]; } catch { }
                        try { thisCountry.OnePostalCode = country["onePostalCode"]; } catch { }
                        thisContinent.Countries.Add(thisCountry);
                    }
                    countries.Continents.Add(thisContinent);
                }
                return countries;
            }
            catch (Exception ex) {
                AddError(ex);
            }
            return null;
        }
		
        /// <summary>
        /// Get list of transmitters for specified country
        /// </summary>
        /// <param name="countrycode"></param>
        /// <returns></returns>
        public IEnumerable<SDTransmitter> GetTransmitters(string countrycode) {
            try {
                var result = GetDynamic(WebGet("transmitters/" + countrycode));
                if (result is null)
                    return null;
                var txList = new List<SDTransmitter>();
                foreach (string key in result.Keys) {
                    var thisTx = new SDTransmitter {
                        TransmitterArea = key,
                        TransmitterID = result[key]
                    };
                    txList.Add(thisTx);
                }
                return txList.AsEnumerable();
            }
            catch (Exception ex) {
                AddError(ex);
            }
            return null;
        }
		
        /// <summary>
        /// Retrieves the list of headends for the specified country and postcode (Requires login token)
        /// </summary>
        /// <param name="country"></param>
        /// <param name="postcode"></param>
        /// <returns></returns>
        public IEnumerable<SDHeadendsResponse> GetHeadends(string country, string postcode) {
            try {
                return GetJSON<IEnumerable<SDHeadendsResponse>>($"headends?country={country}&postalcode={postcode}", _loginToken);
            }
            catch (Exception ex) {
                AddError(ex);
            }
            return null;
        }
		
        /// <summary>
        /// Adds specified lineup to this account (Requires login token)
        /// </summary>
        /// <param name="lineupID"></param>
        /// <returns></returns>
        public SDAddRemoveLineupResponse AddLineup(string lineupID) {
            try {
                var response = PutJSON<SDAddRemoveLineupResponse>("lineups/" + lineupID, _loginToken);
                if (response is null)
                    return null;
                if (response.Code != 0)
                    throw new SchedulesDirectException(response);
                return response;
            }
            catch (Exception ex) {
                AddError(ex);
            }
            return null;
        }
		
        public SDAddRemoveLineupResponse DeleteLineup(string lineupID) {
            try {
                var response = DeleteJSON<SDAddRemoveLineupResponse>("lineups/" + lineupID, _loginToken);
                if (response is null)
                    return null;
                if (response.Code != 0)
                    throw new SchedulesDirectException(response);
                return response;
            }
            catch (Exception ex) {
                AddError(ex);
            }
            return null;
        }
		
        /// <summary>
        /// Return lineups for this account (Requires login token)
        /// </summary>
        /// <returns></returns>
        public SDLineupsResponse GetLineups() {
            try {
                var response = GetJSON<SDLineupsResponse>("lineups", _loginToken);
                if (response is null)
                    return null;
                if (response != null && response.Code != 0)
                    throw new SchedulesDirectException(response);
                return response;
            }
            catch (Exception ex) {
                AddError(ex);
            }
            return null;
        }
		
        /// <summary>
        /// Return map, stations and metadata for the specified lineup
        /// </summary>
        /// <param name="lineup"></param>
        /// <param name="verbose"></param>
        /// <returns></returns>
        public SDGetLineupResponse GetLineup(string lineup, bool verbose = false) {
            try {
                WebHeaderCollection headers = null;
                if (verbose) {
                    headers = new WebHeaderCollection {"verboseMap: true"};
                }
                var response = GetJSON<SDGetLineupResponse>("lineups/" + lineup, _loginToken, headers);
                //if (response != null && response.Code != 0)
                //{
                //    throw new SchedulesDirectException(response);
                //}
                return response;
            }
            catch (Exception ex) {
                AddError(ex);
            }
            return null;
        }
		
        public IEnumerable<SDPreviewLineupResponse> GetLineupPreview(string lineup) {
            try {
                return GetJSON<IEnumerable<SDPreviewLineupResponse>>("lineups/preview/" + lineup, _loginToken);
            }
            catch (Exception ex) {
                AddError(ex);
            }
            return null;
        }
		
        /// <summary>
        /// Return programme information for the specified list of programmes
        /// </summary>
        /// <param name="programmes"></param>
        /// <returns></returns>
        public IEnumerable<SDProgramResponse> GetProgrammes(string[] programmes) {
            try {
                var response = PostJSON<IEnumerable<SDProgramResponse>, string[]>("programs", programmes, _loginToken);
                //if (response != null && response.Code != 0)
                //{
                //    throw new SchedulesDirectException(response);
                //}
                return response;
            }
            catch (Exception ex) {
                AddError(ex);
            }
            return null;
        }
		
        /// <summary>
        /// Return program descriptions for the specified list of programmes
        /// </summary>
        /// <param name="programmes"></param>
        /// <returns></returns>
        public IEnumerable<SDDescriptionResponse> GetDescriptions(string[] programmes) {
            try {
                var result = GetDynamic(WebPost("metadata/description", CreateJSONstring<string[]>(programmes), _loginToken));
                if (result is null)
                    return null;
                var programmeData = new List<SDDescriptionResponse>();
                foreach (string key in result.Keys) {
                    var thisProgramme = new SDDescriptionResponse {EpisodeID = key};
                    var temp = result[key];
                    try { thisProgramme.EpisodeDescription.Code = temp["code"]; } catch { };
                    try { thisProgramme.EpisodeDescription.Description100 = temp["description100"]; } catch { };
                    try { thisProgramme.EpisodeDescription.Description1000 = temp["description1000"]; } catch { };
                    programmeData.Add(thisProgramme);
                }

                return programmeData.AsEnumerable();
            }
            catch (Exception ex) {
                AddError(ex);
            }
            return null;
        }
		
        /// <summary>
        /// Retrieve schedule for the provides list of station/timeframe
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IEnumerable<SDStationScheduleResponse> GetSchedules(IEnumerable<SDStationScheduleRequest> request) {
            try {
                return PostJSON<IEnumerable<SDStationScheduleResponse>, IEnumerable<SDStationScheduleRequest>>("schedules", request, _loginToken);
            }
            catch (Exception ex) {
                AddError(ex);
            }
            return null;
        }
		
        /// <summary>
        /// Retrieve MD5 hashes for provided list of station/timeframe
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IEnumerable<SDStationMD5Response> GetMD5(IEnumerable<SDStationMD5Request> request) {
            try {
                var result = GetDynamic(WebPost("schedules/md5", CreateJSONstring<IEnumerable<SDStationMD5Request>>(request), _loginToken));

                if (result is null)
                    return null;
                var md5Data = new List<SDStationMD5Response>();
                foreach (string resultKey in result.Keys) {
                    var thisResponse = new SDStationMD5Response { StationId = resultKey};
                    var dates = result[resultKey];
                    var daysTemp = new List<SDStationMD5Response.SDMD5Day>();
                    foreach (string dateKey in dates.Keys) {
                        var thisDay = new SDStationMD5Response.SDMD5Day {Date = dateKey};
                        try { thisDay.Metadata.Code = dates[dateKey]["code"]; } catch { };
                        try { thisDay.Metadata.Message = dates[dateKey]["message"]; } catch { };
                        DateTime testDate;
                        if (DateTime.TryParse(dates[dateKey]["lastModified"], null, DateTimeStyles.RoundtripKind, out testDate))
                            thisDay.Metadata.LastModified = testDate;

                        try { thisDay.Metadata.MD5 = dates[dateKey]["md5"]; } catch { };
                        daysTemp.Add(thisDay);
                    }
                    thisResponse.Days = daysTemp.ToArray();
                    md5Data.Add(thisResponse);
                }

                return md5Data.AsEnumerable();
            }
            catch (Exception ex) {
                AddError(ex);
            }
            return null;
        }
		
        /// <summary>
        /// Delete specified system message from login status
        /// </summary>
        /// <param name="messageID"></param>
        /// <returns></returns>
        public SDDeleteResponse DeleteMessage(string messageID) {
            try {
                var response = DeleteJSON<SDDeleteResponse>("messages/" + messageID, _loginToken);
                if (response is null)
                    return null;
                if (response.Code != 0)
                    throw new SchedulesDirectException(response);
                return response;
            }
            catch (Exception ex) {
                AddError(ex);
            }
            return null;
        }
		
        /// <summary>
        /// Obtain live programme information for example, for sporting events (if available)
        /// </summary>
        /// <param name="programmeID"></param>
        /// <returns></returns>
        public SDStillRunningResponse GetStillRunning(string programmeID) {
            try {
                var response = GetJSON<SDStillRunningResponse>("metadata/stillRunning/" + programmeID, _loginToken);
                if (response is null)
                    return null;
                if (response.Code != 0)
                    throw new SchedulesDirectException(response);
                return response;
            }
            catch (Exception ex) {
                AddError(ex);
            }
            return null;
        }
		
        /// <summary>
        /// Obtain image data for assets for specified programme ID(s)
        /// </summary>
        /// <param name="programmes"></param>
        /// <returns></returns>
		public IEnumerable<SDProgramMetadataResponse> GetProgramMetadata(string[] programmes) {
            try {
                return PostJSON<IEnumerable<SDProgramMetadataResponse>, string[]>("metadata/programs/", programmes, _loginToken); //added token 25/11/22
            }
            catch (Exception ex) {
                AddError(ex);
            }
            return null;
        }
		
        /// <summary>
        /// Obtain image data for assets for specified root ID
        /// </summary>
        /// <param name="rootId"></param>
        /// <returns></returns>
        public IEnumerable<SDImageData> GetProgrammeRootMetadata(string rootId) {
            try {
                return GetJSON<IEnumerable<SDImageData>>("metadata/programs/" + rootId, _loginToken);  //added token 25/11/22
            }
            catch (Exception ex) {
                AddError(ex);
            }
            return null;
        }

        /// <summary>
        /// Return image data for assets for specified celebrity ID
        /// </summary>
        /// <param name="celebrityID"></param>
        /// <returns></returns>
        public IEnumerable<SDImageData> GetCelebrityMetadata(string celebrityID) {
            try {
                return GetJSON<IEnumerable<SDImageData>>("metadata/celebrity/" + celebrityID, _loginToken);  //added token 25/11/22
            } catch (Exception ex) {
                AddError(ex);
            }
            return null;
        }
        public void GetProgramImage(string path) {
            //try {
            //    WebGet("https://json.schedulesdirect.org/20141201/image/" + path, _loginToken);
            //} catch (Exception ex) {
            //    System.Diagnostics.Debug.WriteLine(ex);
            //    AddError(ex);
            //}

            //HttpWebRequest WebAction(string url, string action = "GET", string token = "", WebHeaderCollection headers = null) {
            //    var webRequest = (HttpWebRequest)WebRequest.Create(url);
            //    webRequest.Method = action;
            //    webRequest.ContentType = "application/json; charset=utf-8";
            //    webRequest.Accept = "application/json; charset=utf-8";
            //    webRequest.UserAgent = "SDJSharp JSON C# Library/1.0 (https://github.com/M0OPK/SDJSharp)";
            //    webRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            //    if (headers != null)
            //        webRequest.Headers = headers;
            //    if (token != "")
            //        webRequest.Headers.Add("token: " + token);
            //    return webRequest;
            //}

        }
    }
}
