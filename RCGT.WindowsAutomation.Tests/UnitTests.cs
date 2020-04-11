using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace RCGT.WindowsAutomation.Tests
{
    [TestClass]
    public class UnitTests
    {
        public UnitTests() { }

        [TestMethod]
        public void TestCalculatrice()
        {
            //Process.Start("calc.exe");
            //Thread.Sleep(2000);

            var calculatriceUiElement = new UIElement("Calculatrice");

            calculatriceUiElement.BringToFront();

            calculatriceUiElement.FindElementStartingWithName("un").Click();

            calculatriceUiElement.FindElementStartingWithName("Plus").Click();

            calculatriceUiElement.FindElementStartingWithName("DEUX").Click();

            calculatriceUiElement.FindElementStartingWithName("Égale").Click();

            var result = calculatriceUiElement[1][1][1][0][0].GetValue();

            // We lost the handle of the process using BrintToFront
            var calcProcess = Process.GetProcesses().FirstOrDefault(x => x.ProcessName.ToUpper().StartsWith("CALC"));
            calcProcess.Kill();

            Assert.IsNotNull(calculatriceUiElement);

            Assert.AreEqual(int.Parse(result), 3);
        }

        [TestMethod]
        public void TestFortiClient1()
        {
            var fortiClientProcess = Process.GetProcesses().ToList().FirstOrDefault(x => x.ProcessName.ToUpper().StartsWith("FORTICLIENT"));
            if (fortiClientProcess == null)
            {
                fortiClientProcess = Process.Start("C:\\Program Files\\Fortinet\\FortiClient\\FortiClientConsole.exe");
                Thread.Sleep(25000);
            }

            try
            {
                var fortiClientProcessUiElement = new UIElement("FortiClient -- The Security Fabric Agent");
                Assert.IsNotNull(fortiClientProcessUiElement);

                fortiClientProcessUiElement.BringToFront();

                var usernameInput = fortiClientProcessUiElement.FindElementByName("Nom d'utilisateur {{username}}");
                Assert.IsNotNull(usernameInput);
                usernameInput.SetValue("RCGT");
                Thread.Sleep(500);

                // XPath method
                var passwordInput = fortiClientProcessUiElement[2][1][0][1][0][0][1][2][1][0][1][4][1];
                Assert.IsNotNull(passwordInput);
                passwordInput.SetValue("#R1!c2@g3$T4*");
                Thread.Sleep(500);

                // Name equals method
                var btnConnecter = fortiClientProcessUiElement.FindElementByName("Connecter");
                Assert.IsNotNull(btnConnecter);
                btnConnecter.Click();
                Thread.Sleep(5000);
            }
            finally
            {
                if (fortiClientProcess != null)
                {
                    fortiClientProcess.Kill();
                }
            }
        }

        [TestMethod]
        public void TestFortiClient2()
        {
            var fortiClientProcess = Process.GetProcesses().ToList().FirstOrDefault(x => x.ProcessName.ToUpper().StartsWith("FORTICLIENT"));
            if (fortiClientProcess == null)
            {
                fortiClientProcess = Process.Start("C:\\Program Files\\Fortinet\\FortiClient\\FortiClientConsole.exe");
                Thread.Sleep(25000);
            }
            try
            {
                var fortiClientWindowElement = new UIElement("FortiClient -- The Security Fabric Agent");

                fortiClientWindowElement.BringToFront();

                fortiClientWindowElement.Click(true);

                Thread.Sleep(500);

                fortiClientWindowElement.Input.Send(Keys.TAB);
                fortiClientWindowElement.Input.Send(Keys.TAB);
                fortiClientWindowElement.Input.Send("RCGT");
                fortiClientWindowElement.Input.Send(Keys.TAB);
                fortiClientWindowElement.Input.Send("#R1!c2@g3$T4*");
                fortiClientWindowElement.Input.Send(Keys.TAB);
                fortiClientWindowElement.Input.Send(Keys.ENTER);
            }
            finally
            {
                if (fortiClientProcess != null)
                {
                    fortiClientProcess.Kill();
                }
            }
        }
    }
}
