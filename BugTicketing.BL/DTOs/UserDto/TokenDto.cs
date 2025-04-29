using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketing.BL
{
    public class TokenDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpirDate { get; set; } = DateTime.MinValue;
    }
}
