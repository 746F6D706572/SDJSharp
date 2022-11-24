using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Message contains login/hashed password to authenticate
    /// </summary>
    [DataContract]
    public class SDTokenRequest {
        [DataMember]
        public string username;
        [DataMember]
        public string password;

        public SDTokenRequest(string Username = "", string Password = "") {
            username = Username;
            password = Password;
        }
    }
}
