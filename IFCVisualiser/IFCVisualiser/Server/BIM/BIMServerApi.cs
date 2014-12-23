using System;
using IFCVisualiser.Server.Model;
using RestSharp;

namespace IFCVisualiser.Server.BIM
{
    class BimServer
    {

        public static String BaseUrl = "http://data.ksd.ai.ar.tum.de:8080/";
        public static String Username = "dario.banfi@tum.de";
        public static String Password = "testpasssword";
        
        private String _token = null;
        private RestClient _restClient;


        public BimServer()
        {
            _restClient = new RestClient(BaseUrl);

            authenticate();
        }


        private Boolean authenticate()
        {
            var request = new RestRequest(Method.POST);
            request.Resource = "json/login";
            request.RequestFormat = DataFormat.Json;

            var authRequest = new AuthenticationRequest();
            authRequest.request.parameters.username = Username;
            authRequest.request.parameters.password = Password;

            request.AddBody(authRequest);

            var response = _restClient.Execute<AuthenticationResponse>(request);

            if (response.Data.response.exception != null)
            {
                return false;
            }

            _token = response.Data.response.result;
            return true;
            
        }

        public void checkout()
        {
            if (_token == null)
                authenticate();

        }
    }
}
