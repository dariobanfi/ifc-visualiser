using System;
using System.Drawing;
using System.Web;
using System.Windows.Forms;
using Grasshopper.Kernel;
using IFCVisualiser.Properties;
using IFCVisualiser.Server.BIM;

namespace IFCVisualiser
{
    public class BimDownload : GH_Component
    {

        // ##########################################################################################################################
        private const string SName = "BimDownload";
        private const string SAbbreviation = "BimDownload";
        private const string SDescription = "Takes a BIM Server URI (and an optional user, password) and returns a string of the ifc file";
        private const string SCategory = "KsdIFC";
        private const string SSubCategory = "BIM Tools";
        // ##########################################################################################################################

        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public BimDownload()
            : base(SName, SAbbreviation, SDescription, SCategory, SSubCategory)
        {
        }


        // Overrides the default menu of grasshopper (useful)
        public override bool AppendMenuItems(ToolStripDropDown menu)
        {
            Menu_AppendItem(menu, "First item");
            Menu_AppendItem(menu, "Second item");
            Menu_AppendItem(menu, "Third item");
            Menu_AppendSeparator(menu);
            Menu_AppendItem(menu, "Fourth item");
            Menu_AppendItem(menu, "Fifth item");
            Menu_AppendItem(menu, "Sixth item");

            // Return true, otherwise the menu won't be shown.
            return true;
        }


        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            // Use the pManager object to register your input parameters.
            // You can often supply default values when creating parameters.
            // All parameters must have the correct access type. If you want 
            // to import lists or trees of values, modify the ParamAccess flag.
            // The first three arguments are always NAME, NICKNAME, and DESCRIPTION.

            // Input is the XML-File as String
            pManager.AddTextParameter("URI", "@", "Address of the BIM model", GH_ParamAccess.item);
            pManager.AddTextParameter("Username", "Username", "Username to log in the server", GH_ParamAccess.item);
            pManager.AddTextParameter("Password", "Password", "Password to log in the server", GH_ParamAccess.item);
            pManager.AddTextParameter("Serializer", "Serializer", "Serializer to get the model", GH_ParamAccess.item);

            pManager[1].Optional = true;
            pManager[2].Optional = true;
            pManager[3].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            // Use the pManager object to register your output parameters.
            // Output parameters do not have default values, but they too must have the correct access type.
            pManager.AddTextParameter("IfcFile", "FileLocation", "Path of the downloaded IFC file", GH_ParamAccess.item);

            // Sometimes you want to hide a specific parameter from the Rhino preview.
            // You can use the HideParameter() method as a quick way:
            //pManager.HideParameter(0);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            String uri = "";

            // Default values
            String password = BimServer.Password;
            String username = BimServer.Username;
            String serializer = BimServer.Ifc2XSersializer;

            DA.GetData<String>(0, ref uri);
            DA.GetData(1, ref username);
            DA.GetData(2, ref password);
            DA.GetData(3, ref serializer);


            try
            {
                var url = new Uri(uri);
                var poid = HttpUtility.ParseQueryString(url.Query).Get("poid");
                if (poid == null)
                {
                    AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Poid parameter not found in the URL");
                    return;
                }
                
                BimServer.Username = username;
                BimServer.Password = password;
                var client = new BimServer();
                if (String.IsNullOrEmpty(serializer))
                {
                    serializer = BimServer.Ifc2XSersializer;
                }
                var filePath = client.Download(poid, serializer);
                // Set return data
                DA.SetData(0, filePath);
            }
            catch (Exception e)
            {
                // TODO REMOVE WHEN FINISHED DEVELOPING
                MessageBox.Show(e.Message + "\n" + e.TargetSite + "\n" + e.StackTrace);
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, e.Message);
            }
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return Resources.BimDownload;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{3af4fbd3-4108-4d9c-9bed-6653cd05f46a}"); }
        }
    }
}
