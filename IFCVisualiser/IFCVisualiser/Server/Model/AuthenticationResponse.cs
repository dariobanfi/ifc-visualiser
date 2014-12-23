using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApi
{

    public class Exception
    {
        public string __type { get; set; }
        public string message { get; set; }
    }


    public class Response
    {
        public string result { get; set; }
        public Exception exception { get; set; }

    }

    public class AuthenticationResponse
    {
        public Response response { get; set; }
    }
}
