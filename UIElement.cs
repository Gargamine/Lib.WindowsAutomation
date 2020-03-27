using System;
using System.Collections.Generic;
using System.Windows.Automation;

namespace RCGT.WindowsAutomation
{
    public class UIElement
    {
        protected AutomationElement automationElement;

        public List<UIElement> ChildNodes { get; set; } = new List<UIElement>();

        public string Name { get; set; }

        public string ClassName { get; set; }

        public string ControlTypeName { get; set; }

        public string AutomationId { get; set; }

        public UIElement(string rootElementName)
        {
            var conditions = new AndCondition(
                 new PropertyCondition(AutomationElement.NameProperty, rootElementName),
                 new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window)
            );

            var calculatorElement = AutomationElement.RootElement.FindFirst(TreeScope.Children, conditions);

            this.SetUIElement(calculatorElement);
        }

        public UIElement FindElementByName(string name)
        {
            if (this.Name.ToUpper() == name.ToUpper())
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

        public UIElement FindElementByAutomationId(string automationId)
        {
            if (this.AutomationId.ToUpper() == automationId.ToUpper())
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

                if (!valuePattern.Current.IsReadOnly)
                {
                    valuePattern.SetValue(value);
                    return;
                }
            }

            throw new NotSupportedException("UIElement is read-only. Cannot set value.\n" + this.ToString());
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

        public void Click()
        {
            object objPattern;
            if (this.automationElement.TryGetCurrentPattern(InvokePattern.Pattern, out objPattern))
            {
                var invokePattern = objPattern as InvokePattern;
                invokePattern.Invoke();
                return;
            }

            throw new NotSupportedException($"Failed to invoke Click event.\n" + this.ToString());
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

        private UIElement(AutomationElement automationElement)
        {
            this.SetUIElement(automationElement);
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
            //Condition condition1 = new PropertyCondition(AutomationElement.IsControlElementProperty, true);
            Condition condition2 = new PropertyCondition(AutomationElement.IsEnabledProperty, true);
            //TreeWalker walker = new TreeWalker(new AndCondition(condition1, condition2));
            TreeWalker walker = new TreeWalker(condition2);

            AutomationElement childNode = walker.GetFirstChild(nodeElement.automationElement);

            while (childNode != null)
            {
                UIElement childTreeNode = new UIElement(childNode);
                ChildNodes.Add(childTreeNode);
                childNode = walker.GetNextSibling(childNode);
            }
        }

        private void SetUIElement(AutomationElement automationElement)
        {
            if (automationElement != null)
            {
                this.automationElement = automationElement;
                this.Name = automationElement.Current.Name;
                this.ClassName = automationElement.Current.ClassName;
                this.ControlTypeName = automationElement.Current.ControlType.ProgrammaticName;
                this.AutomationId = automationElement.Current.AutomationId;

                WalkEnabledElements(this);
            }
        }
    }
}
