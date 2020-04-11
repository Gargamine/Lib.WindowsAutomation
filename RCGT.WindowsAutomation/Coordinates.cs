using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RCGT.WindowsAutomation
{
    public class Coordinates
    {
        /// <summary>
        /// X coordinate of top-left point of element.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Y coordinate of top-left point of element.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Width of element.
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Height of element.
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Central point of element.
        /// </summary>
        public Point CentralPoint { get; set; }
    }
}
