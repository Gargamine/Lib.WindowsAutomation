using RCGT.WindowsAutomation.Win32Inputs;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Automation;

namespace RCGT.WindowsAutomation
{
    public class UIElement
    {
        protected UIElement rootUiElement;

        protected AutomationElement automationElement;

        public List<UIElement> ChildNodes { get; set; } = new List<UIElement>();

        public string Name { get; set; }

        public string ClassName { get; set; }

        public string ControlTypeName { get; set; }

        public string AutomationId { get; set; }

        public Coordinates Measures { get; set; }

        public Input Input { get; set; }

        public UIElement(string rootElementName)
        {
            var controlTypeConditions = new OrCondition(
                 new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window),
                 new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Pane)
                 );

            var newConditions = new AndCondition(
                new PropertyCondition(AutomationElement.NameProperty, rootElementName),
                controlTypeConditions
                );

            var windowElement = AutomationElement.RootElement.FindFirst(TreeScope.Subtree, newConditions);

            this.SetUIElement(windowElement, this);
        }

        private UIElement(AutomationElement automationElement, UIElement rootUiElement)
        {
            this.SetUIElement(automationElement, rootUiElement);
        }

        public UIElement FindElementByName(string name)
        {
            if (this.Name != null && this.Name.ToUpper() == name.ToUpper())
            {
                return this;
            }

            foreach (var node in ChildNodes)
            {
                var match = node.FindElementByName(name);
                if (match != null)
                    return match;
            }

            return null;
        }

        public UIElement FindElementStartingWithName(string name)
        {
            if (!string.IsNullOrEmpty(this.Name) && this.Name.ToUpper().StartsWith(name.ToUpper()))
            {
                return this;
            }

            foreach (var node in ChildNodes)
            {
                var match = node.FindElementStartingWithName(name);
                if (match != null)
                    return match;
            }

            return null;
        }

        public UIElement FindElementByAutomationId(string automationId)
        {
            if (this.AutomationId != null && this.AutomationId.ToUpper() == automationId.ToUpper())
            {
                return this;
            }

            foreach (var node in ChildNodes)
            {
                var match = node.FindElementByAutomationId(automationId);
                if (match != null)
                    return match;
            }

            return null;
        }

        public void SetValue(string value)
        {
            object objPattern = null;

            if (automationElement.TryGetCurrentPattern(ValuePattern.Pattern, out objPattern))
            {
                var valuePattern = objPattern as ValuePattern;

                if (valuePattern.Current.IsReadOnly)
                {
                    throw new NotSupportedException("UIElement is read-only. Cannot set value.\n" + this.ToString());
                }

                valuePattern.SetValue(value);

                this.RefreshUIComponents();
            }
        }

        public string GetValue()
        {
            object objPattern = null;

            if (automationElement.TryGetCurrentPattern(ValuePattern.Pattern, out objPattern))
            {
                var valuePattern = objPattern as ValuePattern;
                return valuePattern.Current.Value;
            }

            return this.Name;
        }

        public void Click(bool useCentralPosition = false)
        {
            if (useCentralPosition)
            {
                var centralPoint = this.Measures.CentralPoint;

                Mouse.SetCursorPosition((int)centralPoint.X, (int)centralPoint.Y);

                Mouse.MouseEvent(Mouse.MouseEventFlags.LeftDown);
            }
            else
            {
                object objPattern;
                if (!this.automationElement.TryGetCurrentPattern(InvokePattern.Pattern, out objPattern))
                {
                    throw new NotSupportedException($"Failed to invoke Click event.\n" + this.ToString());
                }

                var invokePattern = objPattern as InvokePattern;
                invokePattern.Invoke();
            }

            this.RefreshUIComponents();
        }

        /// <summary>
        /// WARNING! Makes Win32Api loose the process handle.
        /// </summary>
        public void BringToFront()
        {
            if (ControlTypeName.ToUpper() == ControlType.Window.ProgrammaticName.ToUpper()
                || ControlTypeName.ToUpper() == ControlType.Pane.ProgrammaticName.ToUpper())
            {
                var ptr = FindWindow(null, this.Name);

                if (ptr != null)
                {
                    SetForegroundWindow(ptr);
                    ShowWindow(ptr, 9);
                    Thread.Sleep(500);
                }

                this.RefreshUIComponents();
            }
            else
            {
                throw new InvalidOperationException($"Could not bring element to front because it is not of type '{ControlType.Window.ProgrammaticName}' or '{ControlType.Pane.ProgrammaticName}'");
            }
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

        public override string ToString()
        {
            return $"\"{this.Name}\", {this.ControlTypeName}";
        }

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
        private void WalkEnabledElements(UIElement nodeElement)
        {
            AutomationElement childNode = TreeWalker.RawViewWalker.GetFirstChild(nodeElement.automationElement);

            while (childNode != null)
            {
                UIElement childTreeNode = new UIElement(childNode, nodeElement.rootUiElement);
                ChildNodes.Add(childTreeNode);
                childNode = walker.GetNextSibling(childNode);
            }
        }

        private void SetUIElement(AutomationElement automationElement, UIElement rootUiElement)
        {
            if (automationElement != null)
            {
                this.automationElement = automationElement;
                this.rootUiElement = rootUiElement;

                this.Input = new Input();
                this.Name = automationElement.Current.Name;
                this.ClassName = automationElement.Current.ClassName;
                this.ControlTypeName = automationElement.Current.ControlType.ProgrammaticName;
                this.AutomationId = automationElement.Current.AutomationId;
                this.Measures = new Coordinates
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

                WalkEnabledElements(this);
            }
        }

        private void RefreshUIComponents()
        {
            this.SetUIElement(this.rootUiElement.automationElement, this.rootUiElement);

        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }
}
