using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Automation;

namespace Lib.WindowsAutomation
{
    public class UIElement : AbstractAutomationElement
    {
        internal UIElement(AutomationElement automationElement):base()
        {
            WalkEnabledElements(automationElement);
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

        public override void Click()
        {
            //if (useVirtualMouseMovement)
            //{
            //    var centralPoint = this.Measures.CentralPoint;

            //    Mouse.SetCursorPosition((int)centralPoint.X, (int)centralPoint.Y);

            //    Mouse.MouseEvent(Mouse.MouseEventFlags.LeftDown);
            //    Thread.Sleep(100);
            //    Mouse.MouseEvent(Mouse.MouseEventFlags.LeftUp);
            //    Thread.Sleep(100);
            //}
            //else
            //{
            object objPattern;
            if (!this.automationElement.TryGetCurrentPattern(InvokePattern.Pattern, out objPattern))
            {
                throw new NotSupportedException($"Failed to invoke Click event.\n" + this.ToString());
            }

            var invokePattern = objPattern as InvokePattern;
            invokePattern.Invoke();
            //}

            Thread.Sleep(500);
        }

        internal List<UIElement> FindElementsByName(string name)
        {
            var results = new List<UIElement>();

            if (string.IsNullOrEmpty(name))
            {
                return results;
            }

            if (this.Name.ToUpper() == name.ToUpper())
            {
                results.Add(this);
            }

            foreach (var child in ChildNodes)
            {
                var childResults = child.FindElementsByName(name);
                results.AddRange(childResults);
            }

            return results;
        }

        internal List<UIElement> FindElementsStartingWithName(string name)
        {
            var results = new List<UIElement>();

            if (string.IsNullOrEmpty(name))
            {
                return results;
            }

            if (this.Name.ToUpper().StartsWith(name.ToUpper()))
            {
                results.Add(this);
            }

            foreach (var child in ChildNodes)
            {
                var childResults = child.FindElementsStartingWithName(name);
                results.AddRange(childResults);
            }

            return results;
        }

        internal List<UIElement> FindElementsByAutomationId(string automationId)
        {
            var results = new List<UIElement>();

            if (string.IsNullOrEmpty(automationId))
            {
                return results;
            }

            if (this.AutomationId.ToUpper() == automationId.ToUpper())
            {
                results.Add(this);
            }

            foreach (var child in ChildNodes)
            {
                var childResults = child.FindElementsByAutomationId(automationId);
                results.AddRange(childResults);
            }

            return results;
        }

    }
}
