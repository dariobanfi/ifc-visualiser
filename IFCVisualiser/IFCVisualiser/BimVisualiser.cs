using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization.Configuration;
using GH_IO.Serialization;
using Grasshopper.Kernel.Special;
using IFCVisualiser.Entities;
using IFCVisualiser.Server.Model;

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


        public String NamesList; 

        public BimVisualiser()
            : base(SName, SAbbreviation, SDescription, SCategory, SSubCategory)
        {
        }


        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Url List", "Uri List", "Bim server project URL list", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            // Use the pManager object to register your output parameters.
            // Output parameters do not have default values, but they too must have the correct access type.
            pManager.AddTextParameter("ChosenProject", "ChosenProject", "The URL of the chosen project", GH_ParamAccess.item);
        }


        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        protected override void SolveInstance(IGH_DataAccess DA)
        {

            var ret = "";
            DA.GetData(1, ref ret);

            DA.SetData(0, ret);
        }


        public override void CreateAttributes()
        {
            m_attributes = new BimVisualizerAttributes(this);
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override Bitmap Icon
        {
            get
            {
                return Properties.Resources.BimVisualiser;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("{7C629D39-CC59-4FEE-8F9D-FDD553231A83}"); }
        }
    }
}
