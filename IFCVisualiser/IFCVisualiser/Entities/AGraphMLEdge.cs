using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IFCVisualiser.Entities
{
    /// <summary>
    /// This class represents one edge of a room in a groundplan (the connection between two nodes)
    /// The edge has a starting point, an end point and a connection type
    /// This information is read out of the AGraphML file
    /// </summary>
    public class AGraphMLEdge
    {
        // private variables
        private AGraphMLNode pStart;
        private AGraphMLNode pEnd;
        private string connectionType;

        /// <summary>
        /// Main constructor for edge
        /// </summary>
        /// <param name="pStart">starting point of the edge</param>
        /// <param name="pEnd">ending point of the edge</param>
        /// <param name="connectionType">connection type of the edge</param>
        public AGraphMLEdge(AGraphMLNode pStart, AGraphMLNode pEnd, string connectionType)
        {
            // init values
            this.PStart = pStart;
            this.PEnd = pEnd;
            this.ConnectionType = connectionType;
        }

        /// <summary>
        /// The connection type of an edge
        /// </summary>
        public string ConnectionType
        {
            get
            {
                return connectionType;
            }
            private set
            {
                connectionType = value;
            }
        }

        /// <summary>
        /// The starting point of the edge
        /// </summary>
        public AGraphMLNode PStart
        {
            get
            {
                return pStart;
            }
            private set
            {
                pStart = value;
            }
        }

        /// <summary>
        /// The ending point of the edge
        /// </summary>
        public AGraphMLNode PEnd
        {
            get
            {
                return pEnd;
            }
            private set
            {
                pEnd = value;
            }
        }

    }
}
