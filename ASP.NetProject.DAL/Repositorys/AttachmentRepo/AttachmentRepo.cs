using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketing.DAL
{
    public class AttachmentRepo:GenericRepo<Attachment>,IAttachmentRepo
    {
        private readonly Context context;

        public AttachmentRepo(Context context):base(context)
        {
            this.context = context;
        }
    }
}
