using BimPlus.LightCaseClient;
using BimPlus.Sdk.Data.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace IFCVisualiser.Server.BIMplus
{
    class BIMPlusServer
    {
        public static String HostUrl = "https://www.bimplus.net";
        public static String Username = "jana.pejic.9@gmail.com";
        public static String Password = "testpassword";

        public void authorize()
        {
            // Create authentication request, providing some user information 
            AuthenticationAuthorizeRequest request = new AuthenticationAuthorizeRequest();
            request.UserId = Username;
            request.Password = Password;
            request.ClientId = Guid.NewGuid();
            request.ApplicationId = new Guid("FE9BE983-DF2C-4414-AD3F-B9A1EF6AB29A");

            String serviceUrl = HostUrl + "/v2/authorize";
            AuthenticationAuthorizeResponse response = null;

            var config = GenericProxies.CreateDefaultConfiguration();

            // Post the authorize request
            try
            {
                response = GenericProxies.RestPost<AuthenticationAuthorizeResponse, AuthenticationAuthorizeRequest>(serviceUrl, request);
                if (response != null)
                {
                    config.AuthorizationAccessToken = new Guid(response.AccessToken); // Store the returned access token
                    config.AuthorizationTokenType = response.TokenType;
                }
            }
            catch (WebException ex)
            {
                // Authorization failed
                Console.WriteLine(ex.Message);
                Console.Write("Press Enter to exit");
                Console.ReadLine();
                return;
            }

            Console.WriteLine(String.Format("Hello {0}, welcome to bim+", Username));
        }

        public void exportIFC()
        {

        }
    }
}
