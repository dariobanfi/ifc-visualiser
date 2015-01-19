using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

using IFCVisualiser.Server.BIMplus;
using IFCVisualiser.Properties;

namespace IFCVisualiser
{
    public class BimPlusDownload : GH_Component
    {
        // ##########################################################################################################################
        private const string SName = "BimPlusDownload";
        private const string SAbbreviation = "BimPlusDownload";
        private const string SDescription = "Takes a BIM+ URI (and an optional user, password) and returns a string of the ifc file";
        private const string SCategory = "KsdIFC";
        private const string SSubCategory = "BIM+ Tools";
        // ##########################################################################################################################

        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public BimPlusDownload()
            : base(SName, SAbbreviation, SDescription, SCategory, SSubCategory)
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            // Input is the XML-File as String
            pManager.AddTextParameter("URI", "@", "Address of the BIM+ model", GH_ParamAccess.item);
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
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("IfcFile", "FileLocation", "Path of the downloaded IFC file", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            String uri = "";

            // Default values
            String password = BIMPlusServer.Password;
            String username = BIMPlusServer.Username;
            //String serializer = BimPlusApi.Ifc2XSersializer;

            DA.GetData<String>(0, ref uri);
            DA.GetData(1, ref username);
            DA.GetData(2, ref password);
            //DA.GetData(3, ref serializer);

            BIMPlusServer bimPlusServer = new BIMPlusServer();

            try
            {
                bimPlusServer.authorize();
            }
            catch (System.IO.FileNotFoundException e)
            {
                Console.WriteLine(e.FileName);
            }


        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return Resources.BimPlusDownload;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{bcb66c97-2f43-44c2-a969-af9733d4a6d4}"); }
        }
    }
}