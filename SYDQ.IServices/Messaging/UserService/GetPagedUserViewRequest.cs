using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYDQ.IServices.Messaging.UserService
{
    public class GetPagedUserViewRequest : PagedRequest
    {
        public string Username { get; set; }
        public string EmailAddress { get; set; }
    }
}
