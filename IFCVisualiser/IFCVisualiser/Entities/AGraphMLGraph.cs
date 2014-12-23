using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace IFCVisualiser.Entities
{
    /// <summary>
    /// This class represents the complete graph of a room with nodes and edges
    /// There are two lists that can be retrieved: one for all edges, one for all nodes
    /// 
    /// The nodes and edges can be loaded by calling createFromString() by providing the AGraphML file content as string
    /// This method is used by all the components to load the nodes and edges
    /// The string reader uses a DOM-XML-Reader and stores all values in the corresponding list - using the classes AGraphMLPoint und AGraphMLEdge
    /// 
    /// To read the data out of the string, the XML reader uses constants that are defined at the beginning of this class.
    /// In case the specification of the AgraphML file format changes, it might be enough to simply adapt these variables.
    /// </summary>
    class AGraphMLGraph
    {
        // private variables of the graph
        IEnumerable<AGraphMLNode> nodes;
        IEnumerable<AGraphMLEdge> edges;

        // ##########################################################################################################################
        // Variables to identify parts of the 
        private static readonly string attributeXPathNode = "/x:graphml/x:graph/x:node";
        private static readonly string attributeNameId = "id";
        private static readonly string attributeNameRoomType = "roomType";
        private static readonly string attributeNameCenter = "center";
        private static readonly string attributeNameCorners = "corners";

        private static readonly string attributeXPathEdge = "/x:graphml/x:graph/x:edge";
        private static readonly string attributeNameSource = "source";
        private static readonly string attributeNameTarget = "target";
        private static readonly string attributeNameConnectionType = "edgeType";
        // ##########################################################################################################################

        /// <summary>
        /// This structure is a list of all nodes
        /// </summary>
        public IEnumerable<AGraphMLNode> Nodes
        {
            get
            {
                return nodes;
            }
            private set
            {
                nodes = value;
            }
        }

        /// <summary>
        /// This structure is a list of all edges
        /// </summary>
        public IEnumerable<AGraphMLEdge> Edges
        {
            get
            {
                return edges;
            }
            private set
            {
                edges = value;
            }
        }

        /// <summary>
        /// Default constructor that inits the list
        /// </summary>
        public AGraphMLGraph()
        {
            Nodes = new List<AGraphMLNode>();
            Edges = new List<AGraphMLEdge>();
        }

        /// <summary>
        /// This method creates the graph out of a string that contains the read data
        /// </summary>
        /// <param name="fileContent">AGraphML file content as string</param>
        public void createFromString(string fileContent)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(fileContent);

            // get nodes
            nodes = getPointsFromFile(doc);
            // get edges
            edges = getEdgesFromFile(doc, nodes);
        }

        /// <summary>
        /// This method returns a list with all nodes from the file
        /// </summary>
        /// <param name="doc">Representant of the xml document to be read</param>
        /// <returns>a list with nodes</returns>
        private IEnumerable<AGraphMLNode> getPointsFromFile(XmlDocument doc)
        {
            // return value
            List<AGraphMLNode> points = new List<AGraphMLNode>();

            // temporary attributes
            string id;
            double x;
            double y;
            string roomType;
            string corners;

            // set root element
            XmlElement root = doc.DocumentElement;
            // add the namespace
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("x", "http://graphml.graphdrawing.org/xmlns");


            // get all xml-tags with nodes
            XmlNodeList nodeList = root.SelectNodes(attributeXPathNode, nsmgr);

            foreach (XmlNode node in nodeList)
            {
                // init values
                id = "";
                x = 0.0;
                y = 0.0;
                roomType = "";
                corners = "";

                // get id as attribute from the child
                foreach (XmlAttribute attr in node.Attributes)
                {
                    if (attr.Name == attributeNameId)
                        id = attr.InnerText;
                }

                // get data from children
                foreach (XmlNode child in node.ChildNodes)
                {
                    // search in attributes
                    foreach (XmlAttribute attr in child.Attributes)
                    {
                        // center coordinate
                        if (attr.InnerText.Equals(attributeNameCenter))
                        {
                            // center has format "POINT (0.25 0.75)"
                            string center = child.InnerText;

                            // cut string
                            int firstIndex = center.IndexOf('(') + 1;
                            int lastIndex = center.LastIndexOf(')');
                            center = center.Substring(firstIndex, lastIndex - firstIndex);

                            // cut string by space symbol
                            x = double.Parse(center.Split(' ')[0], System.Globalization.CultureInfo.InvariantCulture);
                            y = double.Parse(center.Split(' ')[1], System.Globalization.CultureInfo.InvariantCulture);
                        }

                        // room type
                        if (attr.InnerText.Equals(attributeNameRoomType))
                        {
                            roomType = child.InnerText;
                        }

                        // corners
                        if (attr.InnerText.Equals(attributeNameCorners))
                        {
                            // corners has format "POLYGON ((0 0, 1 0, 1 1, 0 0))"
                            corners = child.InnerText;

                            // cut string
                            int first = corners.IndexOf('(') + 2;
                            int last = corners.LastIndexOf(')') - 1;
                            corners = corners.Substring(first, last - first);
                        }
                    }
                }

                // create point
                AGraphMLNode p = new AGraphMLNode(id, x, y, roomType);
                // add corners
                string[] corner = corners.Split(',');

                // process each corner
                for (int i = 0; i < corner.Length; i++)
                {
                    string temp = corner[i];
                    // delete leading space if there
                    if (temp[0] == ' ') temp = temp.Substring(1);

                    // add corner to list
                    p.addCorner(new AGraphMLCorner(double.Parse(temp.Split(' ')[0], System.Globalization.CultureInfo.InvariantCulture),
                        double.Parse(temp.Split(' ')[1], System.Globalization.CultureInfo.InvariantCulture)));
                }


                // add point
                points.Add(p);
            }

            // return list
            return points;
        }

        /// <summary>
        /// This methods returns a node that has the id given as attribute
        /// </summary>
        /// <param name="id">the id of the node to be found</param>
        /// <param name="points">the node list</param>
        /// <returns>the returning node</returns>
        private static AGraphMLNode getNodeById(string id, IEnumerable<AGraphMLNode> points)
        {
            // go through list
            foreach (AGraphMLNode p in points)
            {
                if (p.Id.Equals(id)) return p;
            }

            return null;
        }


        /// <summary>
        /// This method returns a list with all edges in the file
        /// </summary>
        /// <param name="doc">Representant of the XML file to be read</param>
        /// <param name="points">a list with the nodes</param>
        /// <returns>the edge list</returns>
        private static IEnumerable<AGraphMLEdge> getEdgesFromFile(XmlDocument doc, IEnumerable<AGraphMLNode> points)
        {
            // return value
            List<AGraphMLEdge> edges = new List<AGraphMLEdge>();

            // temporary attributes
            AGraphMLNode pStart;
            AGraphMLNode pEnd;
            string connectionType;

            // set root element
            XmlElement root = doc.DocumentElement;
            // add the namespace
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("x", "http://graphml.graphdrawing.org/xmlns");


            // get all xml-tags with nodes
            XmlNodeList nodeList = root.SelectNodes(attributeXPathEdge, nsmgr);

            foreach (XmlNode node in nodeList)
            {
                // init values
                pStart = null;
                pEnd = null;
                connectionType = "";

                // get source / target as attribute from the child
                foreach (XmlAttribute attr in node.Attributes)
                {
                    // get source
                    if (attr.Name == attributeNameSource)
                        pStart = getNodeById(attr.InnerText, points);

                    // get target
                    if (attr.Name == attributeNameTarget)
                        pEnd = getNodeById(attr.InnerText, points);

                }

                // get connection type
                foreach (XmlNode child in node.ChildNodes)
                {
                    // search in attributes
                    foreach (XmlAttribute attr in child.Attributes)
                    {
                        // connection type
                        if (attr.InnerText.Equals(attributeNameConnectionType))
                            connectionType = child.InnerText;
                    }
                }

                // add edge to list
                AGraphMLEdge e = new AGraphMLEdge(pStart, pEnd, connectionType);
                edges.Add(e);
            }

            // return edge list
            return edges;
        }

    }
}
