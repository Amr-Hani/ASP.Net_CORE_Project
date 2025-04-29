using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketing.DAL
{
    public class Attachment
    {
        public Guid Attachment_id { get; set; }
        public string Attachment_Name { get; set; } = string.Empty;
       
        public string Attachment_URL { get; set; } = string.Empty;


        // ----------------------- releation between Project ------------------------- \\
        // -----------------------        One To Many        ------------------------- \\
        public string Bug_Id { get; set; }
        public Bug Bug { get; set; }

    }
}
