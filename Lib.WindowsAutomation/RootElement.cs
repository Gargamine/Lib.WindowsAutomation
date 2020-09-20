using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Automation;

namespace Lib.WindowsAutomation
{
    public class RootElement : AbstractAutomationElement
    {
        internal RootElement(AutomationElement automationElement) : base()
        {
            this.automationElement = automationElement;
            this.Name = automationElement.Current.Name;
            this.ControlTypeName = automationElement.Current.ControlType.ProgrammaticName;
            this.Measures = Coordinates.GetCoordinateInfos(automationElement);

            // Populate list of children UIElements
            WalkEnabledElements(this.automationElement);
        }

        public void RefreshUIComponents()
        {
            this.ChildNodes.Clear();

            this.WalkEnabledElements(this.automationElement);
        }

        /// <summary>
        /// WARNING! Makes Win32Api loose the process handle.
        /// </summary>
        public void BringToFront()
        {
            try
            {
                var ptr = FindWindow(null, this.Name);

                if (ptr != null)
                {
                    SetForegroundWindow(ptr);
                    ShowWindow(ptr, 9);
                    Thread.Sleep(500);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Could not bring element to front because it is not of type '{ControlType.Window.ProgrammaticName}' or '{ControlType.Pane.ProgrammaticName}'.\n{ex}");
            }
        }

        public IReadOnlyCollection<UIElement> FindElementsByName(string name)
        {
            var results = new List<UIElement>();

            if (string.IsNullOrEmpty(name))
            {
                return results.AsReadOnly();
            }

            foreach (var node in ChildNodes)
            {
                var nodeMatches = node.FindElementsByName(name);

                results.AddRange(nodeMatches);
            }

            return results.AsReadOnly();
        }
        
        public IReadOnlyCollection<UIElement> FindElementsStartingWithName(string name)
        {
            var results = new List<UIElement>();

            if (string.IsNullOrEmpty(name))
            {
                return results.AsReadOnly();
            }

            foreach (var node in ChildNodes)
            {
                var nodeMatches = node.FindElementsStartingWithName(name);

                results.AddRange(nodeMatches);
            }

            return results.AsReadOnly();
        }

        public IReadOnlyCollection<UIElement> FindElementsByAutomationId(string automationId)
        {
            var results = new List<UIElement>();

            if (string.IsNullOrEmpty(automationId))
            {
                return results.AsReadOnly();
            }

            foreach (var node in ChildNodes)
            {
                var nodeMatches = node.FindElementsByAutomationId(automationId);

                results.AddRange(nodeMatches);
            }

            return results.AsReadOnly();
        }

        /// <summary>
        /// This method returns the requested window TreeNode.<br/>
        /// WATCH OUT! May contain duplicate due to Microsoft UIAutomation library issues.
        /// </summary>
        /// <param name="name">Window or Pane name</param>
        /// <returns>Window treeNode</returns>
        public static RootElement FindWindow(string name)
        {
            try
            {
                var conditions =
                new AndCondition(
                    new PropertyCondition(AutomationElement.NameProperty, name),
                new OrCondition(
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window),
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Pane)
                 ));

                var windowElement = AutomationElement.RootElement.FindFirst(TreeScope.Element | TreeScope.Children, conditions);

                return new RootElement(windowElement);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed rooting Windows Automation UI elements.\n{ex}");
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public override void Click()
        {
            this.BringToFront();
        }
    }
}
