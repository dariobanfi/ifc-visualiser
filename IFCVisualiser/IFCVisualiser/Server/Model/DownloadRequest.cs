using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IFCVisualiser.Server.Model
{


    public class DownloadParameters
    {
        public string roid { get; set; }
        public string serializerOid { get; set; }
        public string showOwn { get; set; }
        public string sync { get; set; }
    }

    public class MyDownlooadRequest
    {
        public string @interface { get; set; }
        public string method { get; set; }
        public DownloadParameters parameters { get; set; }

        public MyDownlooadRequest()
        {
            parameters = new DownloadParameters();
        }

    }

    public class DownloadRequest
    {
        public string token { get; set; }
        public MyDownlooadRequest request { get; set; }

        public DownloadRequest()
        {
            this.request = new MyDownlooadRequest();
        }
    }

}
