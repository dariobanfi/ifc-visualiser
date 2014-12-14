using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace IFCVisualiser
{
    public class IFCVisualiserInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "IFCVisualiser";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return null;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("60f852e9-ee23-43e8-a759-c82ff5a6f3be");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "";
            }
        }
    }
}
