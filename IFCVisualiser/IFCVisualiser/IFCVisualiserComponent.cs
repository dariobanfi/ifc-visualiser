using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using System.Drawing;
using IFCVisualiser.Entities;

namespace IFCVisualiser
{
    public class IFCVisualiserComponent : GH_Component
    {

        // ##########################################################################################################################
        private const string sName = "IFCVisualiser";
        private const string sAbbreviation = "ASpi";
        private const string sDescription = "Construct an Archimedean, or arithmetic, spiral given its radii and number of turns.";
        private const string sCategory = "Curve";
        private const string sSubCategory = "Primitive";
        // ##########################################################################################################################

        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public IFCVisualiserComponent() : base(sName, sAbbreviation, sDescription, sCategory, sSubCategory)
        {
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
            pManager.AddTextParameter("AGraphML-String", "AGraphML", "ArchitecturalGraphML-String", GH_ParamAccess.item);

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
            pManager.AddTextParameter("Result", "R", "String saying Success or Failure", GH_ParamAccess.item);

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
