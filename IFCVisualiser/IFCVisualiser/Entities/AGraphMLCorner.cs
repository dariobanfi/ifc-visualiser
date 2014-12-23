using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IFCVisualiser.Entities
{
    /// <summary>
    /// This class represents one corner of a room in a groundplan.
    /// The corner has coordinates (X and Y-coordinates) that are read out of the AGraphML file
    /// There is one constructor that requires the coordinates as parameter
    /// </summary>
    public class AGraphMLCorner
    {
        // private variables for x-coordinates and y-coordinates
        private double x;
        private double y;

        /// <summary>
        /// Main constructor to initialize the x-coordinate and y-coordinate
        /// </summary>
        /// <param name="x">X-coordinate of the corner</param>
        /// <param name="y">Y-coordinate of the corner</param>
        public AGraphMLCorner(double x, double y)
        {
            // init values
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// The X-Coordinate of the corner
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
        /// The Y-Coordinate of the corner
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
    }
}
