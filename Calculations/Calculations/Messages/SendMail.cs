using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculations.Messages
{
    public sealed class SendMail
    {
        public SendMail(string to, int result)
        {
            To = to;
            Result = result;
        }

        public string To { get; set; }
        public int Result { get; set; }
    }
}
