using System;

namespace IFCVisualiser.Server.Model
{

    public class BimRequest
    {

        public String token { get; set; }
        public Request request { get; set; }

        public BimRequest()
        {
            request = new Request();
        }
    }

    public class Request
    {
        public string @interface = "";
        public string method { get; set; }
        public Parameters parameters { get; set; }

        public Request()
        {
            parameters = new Parameters();
        }
    }

    public class Parameters
    {
        public string username { get; set; }
        public string password { get; set; }
        public string poid { get; set; }
        public string roid { get; set; }
        public string serializerOid{ get; set; }
        public string showOwn{ get; set; }
        public string sync{ get; set; }
        public string actionId { get; set; }

    }




}
