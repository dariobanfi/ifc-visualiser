using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IFCVisualiser.Server.Model
{
    class Serializers
    {


        public static Dictionary<string, string> SerializerList = new Dictionary<string, string>
        {
	        {"Ifc2x3", "3276838"},
	        {"IfcXML", "720937"},
	        {"CityGML1.0.0", "3932198"},
	        {"BinaryGeometrySerializer", ""},
	        {"Collada", ""},
	        {"Json", ""},
	        {"JsonGeometrySerializer", ""},
	        {"ObjectCSV", ""},
	        {"ObjectInfo", ""},
	        {"SceneJs3ShellSerializer", ""}
        };

        
        public static string GetSerializerId(string serializer)
        {
            // Try to get the result in the static Dictionary
            string result;
            if (SerializerList.TryGetValue(serializer, out result))
            {
                return result;
            }
                
            return null;
        }
    }
}
