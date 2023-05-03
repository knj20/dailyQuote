using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dailyQuote.Models
{
    public class EmailAddress
    {
        public string Address { get; set; }
        public string DisplayName { get; set; }

        public EmailAddress(string address, string displayName = null)
        {
            Address = address;
            DisplayName = displayName;
        }
    }
}
