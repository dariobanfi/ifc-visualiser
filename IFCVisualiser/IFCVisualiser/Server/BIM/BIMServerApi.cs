﻿using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using IFCVisualiser.Server.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Exception = System.Exception;

namespace IFCVisualiser.Server.BIM
{
    public class BimServer
    {

        public static String BaseUrl = "http://data.ksd.ai.ar.tum.de:8080/";
        public static String Username = "dario.banfi@tum.de";
        public static String Password = "testpassword";

        public static String Ifc2XSersializer = "Ifc2x3";
        
        private String _token;

        public void Login()
        {

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(BaseUrl + "json/login");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Proxy = null;
            httpWebRequest.Method = "POST";

            var rq = new BimRequest();
            rq.request.method = "login";
            rq.request.@interface = "Bimsie1AuthInterface";
            rq.request.parameters.username = Username;
            rq.request.parameters.password = Password;

            var requestPayload = JsonConvert.SerializeObject(rq, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = requestPayload;

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();

                var httpResponse = (HttpWebResponse) httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var jsonResult = streamReader.ReadToEnd();
                    var result = JsonConvert.DeserializeObject<BimResponse>(jsonResult);
                    if (result.response.exception != null)
                    {
                        throw new Exception("Invalid Credentials");
                    }
                    else
                    {
                        _token = result.response.result;
                    }
                }
            }
        }

        private String LatestRoidRequest(String poid)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(BaseUrl + "json/getProjectByPoid");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Proxy = null;
            httpWebRequest.Method = "POST";

            var rq = new BimRequest();
            rq.token = _token;
            rq.request.@interface = "Bimsie1ServiceInterface";
            rq.request.method = "getProjectByPoid";
            rq.request.parameters.poid = poid;

            var requestPayload = JsonConvert.SerializeObject(rq, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = requestPayload;

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var jsonResult = streamReader.ReadToEnd();
                    dynamic stuff = JObject.Parse(jsonResult);
                    try
                    {
                        return stuff.response.result.lastRevisionId;
                    }
                    catch (System.Exception)
                    {
                    }

                }
            }

            return null;
        }


        public String ProjectNameRequest(string poid)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(BaseUrl + "json/getProjectByPoid");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Proxy = null;
            httpWebRequest.Method = "POST";

            var rq = new BimRequest();
            rq.token = _token;
            rq.request.@interface = "Bimsie1ServiceInterface";
            rq.request.method = "getProjectByPoid";
            rq.request.parameters.poid = poid.ToString();

            var requestPayload = JsonConvert.SerializeObject(rq, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = requestPayload;

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var jsonResult = streamReader.ReadToEnd();
                    dynamic stuff = JObject.Parse(jsonResult);
                    try
                    {
                        return stuff.response.result.name;
                    }
                    catch (Exception)
                    {
                    }

                }
            }

            return null;
        }


        private String DownloadRequest(string roid, string serializerId)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(BaseUrl + "json/download");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Proxy = null;
            httpWebRequest.Method = "POST";

            var rq = new BimRequest();
            rq.token = _token;
            rq.request.method = "download";
            rq.request.@interface = "Bimsie1ServiceInterface";
            rq.request.parameters.roid = roid;
            rq.request.parameters.serializerOid = serializerId;
            rq.request.parameters.showOwn = "false";
            rq.request.parameters.sync = "false";

            var requestPayload = JsonConvert.SerializeObject(rq, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = requestPayload;

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var jsonResult = streamReader.ReadToEnd();
                    var result = JsonConvert.DeserializeObject<BimResponse>(jsonResult);
                    var opValue = result.response.result;

                    return opValue;
                }
            }

        }

        private String getData(string code)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(BaseUrl + "json/getDownloadData");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Proxy = null;
            httpWebRequest.Method = "POST";

            var rq = new BimRequest();
            rq.token = _token;
            rq.request.method = "getDownloadData";
            rq.request.@interface = "Bimsie1ServiceInterface";
            rq.request.parameters.actionId = code;

            var requestPayload = JsonConvert.SerializeObject(rq, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = requestPayload;

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var jsonResult = streamReader.ReadToEnd();
                    dynamic resp = JObject.Parse(jsonResult);
                    string jsonResponse = resp.response.result.file;


                    var fileName = Path.Combine(Environment.GetFolderPath(
                        Environment.SpecialFolder.ApplicationData), "downloadedIFC");

                    using (StreamWriter file = new StreamWriter(fileName))
                    {
                        try
                        {
                            var bytes = Convert.FromBase64String(jsonResponse);
                            string ifcDecoded = Encoding.UTF8.GetString(bytes);
                            file.Write(ifcDecoded);
                            return fileName;
                        }
                        catch (Exception)
                        {
                            throw  new Exception("The request did not return data");
                        }
                        

                    }
                }
            }
        }


        public String Download(String id, String serializerName)
        {
            if (_token == null)
                Login();

            var serializer = Serializers.GetSerializerId(serializerName);
            var roid = LatestRoidRequest(id);
            var opcode = DownloadRequest(roid, serializer);
            var filePath = getData(opcode);
            return filePath;
        }
    }
}
