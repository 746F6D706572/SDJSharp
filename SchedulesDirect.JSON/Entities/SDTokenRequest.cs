using System.Net;
using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Message contains login/hashed password to authenticate
    /// </summary>
    [DataContract]
    public class SDTokenRequest {
        [DataMember (Name = "username")]
        public string Username;
        [DataMember(Name = "password")]
        public string Password;

        public SDTokenRequest(NetworkCredential credential) : this(credential.UserName, credential.Password) {
        }
        public SDTokenRequest(string username = "", string password = "")
        {
            Username = username;
            Password = password;
        }
    }
}