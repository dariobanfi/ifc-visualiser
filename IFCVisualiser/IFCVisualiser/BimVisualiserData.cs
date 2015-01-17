using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IFCVisualiser
{
    internal class BimVisualiserData
    {
        // Singleton data because the component gets destroyed on ExpireSolution

        private static BimVisualiserData instance;


        public List<String> URIList = new List<String>();
        public List<String> NameList = new List<String>();
        public int Selected = -1;

        private BimVisualiserData()
        {
        }

        public static BimVisualiserData Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BimVisualiserData();
                }
                return instance;
            }
        }


    }
}