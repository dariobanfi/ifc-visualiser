using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization.Configuration;
using IFCVisualiser.Entities;
using IFCVisualiser.Server.Model;

namespace IFCVisualiser
{
    public class BimSerializer : GH_Component
    {

        // ##########################################################################################################################
        private const string SName = "BimSerializer";
        private const string SAbbreviation = "BimSerializer";
        private const string SDescription = "List of possible serializer values";
        private const string SCategory = "KsdIFC";
        private const string SSubCategory = "BIM Tools";
        // ##########################################################################################################################


        private String _chosenSerializer = "Ifc2x3";
        
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public BimSerializer()
            : base(SName, SAbbreviation, SDescription, SCategory, SSubCategory)
        {
        }


        public override bool AppendMenuItems(ToolStripDropDown menu)
        {
            foreach (var serializer in Serializers.SerializerList)
            {
                Menu_AppendItem(menu, serializer.Key, Menu_MyCustomItemClicked);
            }
           

            // Return true, otherwise the menu won't be shown.
            return true;
        }


        private void Menu_MyCustomItemClicked(Object sender, EventArgs e)
        {

            _chosenSerializer = ((ToolStripMenuItem)sender).ToString(); ;
            // Re-executes the component
            this.CollectData();
            this.ComputeData();
            this.ExpireSolution(true);
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            // No input parameters for this component
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            // Use the pManager object to register your output parameters.
            // Output parameters do not have default values, but they too must have the correct access type.
            pManager.AddTextParameter("Serializer", "Serializer", "Returns the serializer name", GH_ParamAccess.item);
        }


        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // set return data
            DA.SetData(0, _chosenSerializer);
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override Bitmap Icon
        {
            get
            {
                return Properties.Resources.BimSerializer;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("{00679ce6-6dd9-4004-b613-b3c0de2fde2e}"); }
        }
    }
}
