using System.Collections.Generic;
using System.Linq;
using MimeKit;

namespace Customers_BLL.Helpers
{
    public class MessageModel
    {
        public List<string> To { get; set; }
        
        public string Subject { get; set; }
        
        public string Content { get; set; }
    }
}