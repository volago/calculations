using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculations.Messages
{
    public sealed class CalculateLoan
    {
        public CalculateLoan(string from, int loanId)
        {
            From = from;
            LoanId = loanId;
        }

        /// <summary>
        /// E-mail address of calculation reporter
        /// </summary>
        public string From { get; set; }

        public int LoanId { get; set; }
    }
}
