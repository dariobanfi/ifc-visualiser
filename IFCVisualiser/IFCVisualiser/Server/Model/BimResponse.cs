namespace TestApi
{
    public class BimResponse
    {
        public Response response { get; set; }
    }


    public class Response
    {
        public string result { get; set; }
        public int lastRevisionId { get; set; }
        public Exception exception { get; set; }
        public string name { get; set; }

    }


    public class Exception : System.Exception
    {
        public string __type { get; set; }
        public string message { get; set; }
    }




 
}
