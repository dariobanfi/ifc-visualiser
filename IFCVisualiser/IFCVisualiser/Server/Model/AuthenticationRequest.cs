using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApi
{
    public class Parameters
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class Request
    {
        public string @interface = "Bimsie1AuthInterface";
        public string method = "login";
        public Parameters parameters { get; set; }

        public Request()
        {
            parameters = new Parameters();
        }
    }

    public class AuthenticationRequest
    {
        public Request request { get; set; }

        public AuthenticationRequest()
        {
            request = new Request();
        }
    }
}
