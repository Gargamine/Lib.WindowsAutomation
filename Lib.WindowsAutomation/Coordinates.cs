using System.Windows;
using System.Windows.Automation;

namespace Lib.WindowsAutomation
{
    public class Coordinates
    {
        /// <summary>
        /// X coordinate of top-left point of element.
        /// </summary>
        public double X { get; internal set; }

        /// <summary>
        /// Y coordinate of top-left point of element.
        /// </summary>
        public double Y { get; internal set; }

        /// <summary>
        /// Width of element.
        /// </summary>
        public double Width { get; internal set; }

        /// <summary>
        /// Height of element.
        /// </summary>
        public double Height { get; internal set; }

        /// <summary>
        /// Central point of element.
        /// </summary>
        public Point CentralPoint { get; internal set; }

        internal static Coordinates GetCoordinateInfos(AutomationElement automationElement)
        {
            if (automationElement == null)
            {
                return null;
            }

            return new Coordinates
            {
                X = automationElement.Current.BoundingRectangle.X,
                Y = automationElement.Current.BoundingRectangle.Y,
                Width = automationElement.Current.BoundingRectangle.Width,
                Height = automationElement.Current.BoundingRectangle.Height,
                CentralPoint = new System.Windows.Point
                {
                    X = automationElement.Current.BoundingRectangle.X + (automationElement.Current.BoundingRectangle.Width / 2),
                    Y = automationElement.Current.BoundingRectangle.Y + (automationElement.Current.BoundingRectangle.Height / 2),
                }
            };
        }
    }
}
