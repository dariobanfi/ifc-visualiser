using Grasshopper.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace IFCVisualiser.Entities
{
    /// <summary>
    /// This class represents one room in a groundplan
    /// The room is a point and therewith has a x-coordinate and a y-coordinate which represent the center of the room
    /// Furthermore, it has an id, a roomType and a list of corners
    /// This information is read out of the AGraphML file
    /// 
    /// Corners can be added dynamically and retrieved as string
    /// The point itself can be transferred to a GH_Point, loosing information like the id, roomType and corners
    /// </summary>
    public class AGraphMLNode
    {
        // private variables
        private double x;
        private double y;
        private string id;
        private string roomType;
        private List<AGraphMLCorner> corners;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">the id of the room</param>
        /// <param name="x">the x-coordinate of the room</param>
        /// <param name="y">the y-coordinate of the room</param>
        /// <param name="roomType">the room type of the room</param>
        public AGraphMLNode(string id, double x, double y, string roomType)
        {
            this.X = x;
            this.Y = y;
            this.Id = id;
            this.RoomType = roomType;
            Corners = new List<AGraphMLCorner>();
        }

        /// <summary>
        /// The X-Coordinate of the node
        /// </summary>
        public double X
        {
            get
            {
                return x;
            }
            private set
            {
                x = value;
            }
        }

        /// <summary>
        /// The Y-Coordinate of the node
        /// </summary>
        public double Y
        {
            get
            {
                return y;
            }
            private set
            {
                y = value;
            }
        }

        /// <summary>
        /// The roomname of the node
        /// </summary>
        public string RoomType
        {
            get
            {
                return roomType;
            }
            private set
            {
                roomType = value;
            }
        }

        /// <summary>
        /// The corners of the node
        /// </summary>
        public List<AGraphMLCorner> Corners
        {
            get
            {
                return corners;
            }
            private set
            {
                corners = value;
            }
        }

        /// <summary>
        /// The id of the node
        /// </summary>
        public string Id
        {
            get
            {
                return id;
            }
            private set
            {
                id = value;
            }
        }

        /// <summary>
        /// Adds a corner to the corner point list
        /// </summary>
        /// <param name="p">the new corner</param>
        public void addCorner(AGraphMLCorner p)
        {
            Corners.Add(p);
        }

        /// <summary>
        /// Returns all corners as string
        /// </summary>
        /// <returns>the corner string in format "12,45;13,30"</returns>
        public String getCornersAsString()
        {
            // go through corners
            String result = "";
            foreach (AGraphMLCorner p in corners)
            {
                result += p.X + "," + p.Y + ";";
            }

            // return string without last ";"
            return result.Substring(0, result.Length - 1);
        }

        /// <summary>
        /// This method returns this node as a GH_Point, that can be displayed directly in Rhinozeros.
        /// This conversion looses important parts like the id, roomType and corners
        /// </summary>
        /// <returns>the transferred GH_Point</returns>
        public GH_Point getPointAsGH_Point()
        {
            // make GH_Point out of Point3d
            return new GH_Point(new Rhino.Geometry.Point3d(x, y, 0));
        }
    }
}
