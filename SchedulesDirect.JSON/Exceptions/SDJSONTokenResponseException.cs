using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulesDirect
{
    public class SchedulesDirectTokenResponseException : SchedulesDirectException
    {
        public string Token { get; set; }
        public SchedulesDirectTokenResponseException(SDTokenResponse response) : base(response)
        {
            Token = response.Token;
        }
    }
}
