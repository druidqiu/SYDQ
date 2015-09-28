using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYDQ.IServices.Messaging
{
    public class Response
    {
        public Response()
        {
            Success = true;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
