using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Windows.Forms;
using Grasshopper.Kernel;
using IFCVisualiser.Properties;
using IFCVisualiser.Server.BIM;

namespace IFCVisualiser
{
    public class BimVisualiser : GH_Component
    {
        // ##########################################################################################################################
        private const string SName = "BimVisualiser";
        private const string SAbbreviation = "BimVisualiser";
        private const string SDescription = "Shows the name of a BIM project";
        private const string SCategory = "KsdIFC";
        private const string SSubCategory = "BIM Tools";
        // ##########################################################################################################################


        private String _returnValue = "";

        public BimVisualiser()
            : base(SName, SAbbreviation, SDescription, SCategory, SSubCategory)
        {
        }


        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("ListofURL", "ListofURL", "Bim server project URL list", GH_ParamAccess.list);
            pManager.AddTextParameter("Username", "Username", "Username for the BIM Server", GH_ParamAccess.item);
            pManager.AddTextParameter("Password", "Password", "Password for the BIM Server", GH_ParamAccess.item);
            pManager[1].Optional = true;
            pManager[2].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            // Use the pManager object to register your output parameters.
            // Output parameters do not have default values, but they too must have the correct access type.
            pManager.AddTextParameter("ChosenProject", "ChosenProject", "The URL of the chosen project",
                GH_ParamAccess.item);
        }


        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var username = "";
            var password = "";
            var list = new List<string>();
            DA.GetDataList(0, list);
            DA.GetData(1, ref username);
            DA.GetData(2, ref password);


            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
            {
                BimServer.Username = username;
                BimServer.Password = password;
            }


            if (list.SequenceEqual(BimVisualiserData.Instance.URIList))
            {
                BimVisualiserData.Instance.URIList = list;
            }
            // New data feeded, need to recompute
            else
            {
                _returnValue = "";
                BimVisualiserData.Instance.Selected = -1;
                BimVisualiserData.Instance.URIList = list;
                BimVisualiserData.Instance.NameList = new List<string>();
            }


            if (BimVisualiserData.Instance.NameList.Count == 0)
            {
                foreach (var uri in BimVisualiserData.Instance.URIList)
                {
                    var client = new BimServer();
                    client.Login();
                    var name = GetProjectName(client, uri);
                    BimVisualiserData.Instance.NameList.Add(name);
                }

                m_attributes.ExpireLayout();
            }


            DA.SetData(0, _returnValue);
        }

        public string GetProjectName(BimServer client, string uri)
        {
            try
            {
                var url = new Uri(uri);
                var poid = HttpUtility.ParseQueryString(url.Query).Get("poid");
                if (poid == null)
                {
                    AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Poid parameter not found in the URL");
                    return null;
                }


                var name = client.ProjectNameRequest(poid);
                return name;
            }
            catch (Exception e)
            {
                // TODO REMOVE WHEN FINISHED DEVELOPING
                MessageBox.Show(e.Message + "\n" + e.TargetSite + "\n" + e.StackTrace);
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, e.Message);
            }
            return null;
        }

        public void setReturnValue(int index)
        {
            _returnValue = BimVisualiserData.Instance.URIList[index];
        }


        public override void CreateAttributes()
        {
            m_attributes = new BimVisualizerAttributes(this);
        }

        protected override Bitmap Icon
        {
            get { return Resources.BimVisualiser; }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("{7C629D39-CC59-4FEE-8F9D-FDD553231A83}"); }
        }
    }
}