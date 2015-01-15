namespace IFCVisualiser.Server.Model
{
    public class BimResponse
    {
        public Response response { get; set; }
    }


    public class Response
    {
        public string result { get; set; }
        public int lastRevisionId { get; set; }
        public BimException exception { get; set; }
        public string name { get; set; }

    }


    public class BimException
    {
        public string __type { get; set; }
        public string message { get; set; }
    }




 
}
