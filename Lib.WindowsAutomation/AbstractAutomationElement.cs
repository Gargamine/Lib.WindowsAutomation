using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Automation;

namespace Lib.WindowsAutomation
{
    public abstract class AbstractAutomationElement
    {
        protected AutomationElement automationElement;

        internal int ProcessId;

        internal int NativeWindowHandle;

        /// <summary>
        /// Name of the element.<br/>
        /// Also the displayed text on the node element in Inspect.exe
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Generic type of control.
        /// </summary>
        public string ControlTypeName { get; set; }

        /// <summary>
        /// Class name of the control (is rarely set!).
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// Automation ID (is rarely set!).
        /// </summary>
        public string AutomationId { get; set; }

        /// <summary>
        /// Measures and coordinates of the controol.
        /// </summary>
        public Coordinates Measures { get; set; }

        /// <summary>
        /// Children of the current node.
        /// </summary>
        public List<UIElement> ChildNodes { get; set; }

        protected AbstractAutomationElement()
        {
            this.ChildNodes = new List<UIElement>();
        }

        public abstract void Click();

        /// <summary>
        /// Walks the UI Automation tree and adds the control type of each enabled control 
        /// element it finds to a TreeView.
        /// </summary>
        /// <param name="rootElement">The root of the search on this iteration.</param>
        /// <param name="treeNode">The node in the TreeView for this iteration.</param>
        /// <remarks>
        /// This is a recursive function that maps out the structure of the subtree beginning at the
        /// UI Automation element passed in as rootElement on the first call. This could be, for example,
        /// an application window.
        /// CAUTION: Do not pass in AutomationElement.RootElement. Attempting to map out the entire subtree of
        /// the desktop could take a very long time and even lead to a stack overflow.
        /// </remarks>
        protected void WalkEnabledElements(AutomationElement automationElement)
        {
            if (automationElement == null)
            {
                return;
            }

            //ChildNodes.Clear();

            this.automationElement = automationElement;

            this.Name = automationElement.Current.Name;
            this.ClassName = automationElement.Current.ClassName;
            this.ControlTypeName = automationElement.Current.ControlType.ProgrammaticName;
            this.AutomationId = automationElement.Current.AutomationId;
            this.Measures = Coordinates.GetCoordinateInfos(automationElement);
            this.ProcessId = automationElement.Current.ProcessId;
            this.NativeWindowHandle = automationElement.Current.NativeWindowHandle;

            var walker = TreeWalker.ContentViewWalker;

            AutomationElement childNode = walker.GetFirstChild(automationElement);

            while (childNode != null)
            {
                var childTreeNode = new UIElement(childNode);

                ChildNodes.Add(childTreeNode);

                childNode = walker.GetNextSibling(childNode);
            }
        }

        public override string ToString()
        {
            return $"\"{this.Name}\", {this.ControlTypeName}";
        }

        public UIElement this[int index]
        {
            get
            {
                return this.ChildNodes[index];
            }
            set
            {
                this.ChildNodes[index] = value;
            }
        }
    }
}
