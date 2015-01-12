using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using System.Drawing;
using System.Windows.Forms;
using IFCVisualiser.Entities;

namespace IFCVisualiser
{
    public class BimDownload : GH_Component
    {

        // ##########################################################################################################################
        private const string sName = "IFCPicker";
        private const string sAbbreviation = "IfcPicker";
        private const string sDescription = "Shows the name of a list of IFC Project URIs and lets the user pick one";
        private const string sCategory = "KsdIFC";
        private const string sSubCategory = "IFC Tools";
        // ##########################################################################################################################

        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public IfcPicker() : base(sName, sAbbreviation, sDescription, sCategory, sSubCategory)
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
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            // Use the pManager object to register your input parameters.
            // You can often supply default values when creating parameters.
            // All parameters must have the correct access type. If you want 
            // to import lists or trees of values, modify the ParamAccess flag.
            // The first three arguments are always NAME, NICKNAME, and DESCRIPTION.

            // Input is the XML-File as String
            pManager.AddTextParameter("IFc-File", "Ifc", "IndustryFoundationClass-String", GH_ParamAccess.item);

            // If you want to change properties of certain parameters, 
            // you can use the pManager instance to access them by index:
            //pManager[0].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            // Use the pManager object to register your output parameters.
            // Output parameters do not have default values, but they too must have the correct access type.
            pManager.AddTextParameter("Result", "R", "Structure to be piped in an Object", GH_ParamAccess.item);

            // Sometimes you want to hide a specific parameter from the Rhino preview.
            // You can use the HideParameter() method as a quick way:
            //pManager.HideParameter(0);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // get attribute "filepath"
            String file = "";
            DA.GetData<String>(0, ref file);

            AGraphMLGraph graph = new AGraphMLGraph();
            graph.createFromString(file);


            // set return data
            DA.SetDataList(0, "Success");
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
                return IFCVisualiser.Properties.Resources.single_graphml;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{46c2ddc1-9c78-47c7-b67b-3e2a5302cf92}"); }
        }
    }
}
