using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Lib.WindowsAutomation.Tests
{
    [TestClass]
    public class UnitTests
    {
        private const string FortiClient_Username = "RCGT";
        private const string FortiClient_Password = "#R1!c2@g3$T4*";

        public UnitTests() { }

        [TestMethod]
        public void TestCalculatrice()
        {
            var calcProcess = Process.GetProcesses().ToList().FirstOrDefault(x => x.ProcessName.ToUpper().StartsWith("CALCULA"));
            if (calcProcess == null)
            {
                calcProcess = Process.Start("calc.exe");
                Thread.Sleep(2000);
            }

            var calculatriceUiElement = RootElement.FindWindow("Calculator");

            calculatriceUiElement.BringToFront();

            var oneButtons = calculatriceUiElement.FindElementsStartingWithName("One");

            if (oneButtons.Count == 0)
            {
                Assert.Fail("Could not find any button 'One'");
            }
            else if (oneButtons.Count > 1)
            {
                Assert.Fail("We found more than just one 'One' button!");
            }
            else
            {
                oneButtons.First().Click();
            }

            var plusButtons = calculatriceUiElement.FindElementsStartingWithName("Plus");

            if (plusButtons.Count == 0)
            {
                Assert.Fail("Could not find any button 'Plus'");
            }
            else if (plusButtons.Count > 1)
            {
                Assert.Fail("We found more than just one 'Plus' button!");
            }
            else
            {
                plusButtons.First().Click();
            }

            var twoButtons = calculatriceUiElement.FindElementsStartingWithName("Two");

            if (twoButtons.Count == 0)
            {
                Assert.Fail("Could not find any button 'Two'");
            }
            else if (twoButtons.Count > 1)
            {
                Assert.Fail("We found more than just one 'Two' button!");
            }
            else
            {
                twoButtons.First().Click();
            }

            var equalsButtons = calculatriceUiElement.FindElementsStartingWithName("Equals");

            if (equalsButtons.Count == 0)
            {
                Assert.Fail("Could not find any button 'Equals'");
            }
            else if (equalsButtons.Count > 1)
            {
                Assert.Fail("We found more than just one 'Equals' button!");
            }
            else
            {
                equalsButtons.First().Click();
            }

            calculatriceUiElement.RefreshUIComponents();

            var result = calculatriceUiElement[1][1][1][0][0].GetValue();

            // We lost the handle of the process using BrintToFront
            calcProcess.Kill();

            Assert.IsNotNull(calculatriceUiElement);

            Assert.AreEqual(int.Parse(result), 3);
        }

        [TestMethod]
        public void TestFortiClientConnect()
        {
            var fortiClientProcess = Process.GetProcesses().ToList().FirstOrDefault(x => x.ProcessName.ToUpper().StartsWith("FORTICLIENT"));
            if (fortiClientProcess == null)
            {
                fortiClientProcess = Process.Start("C:\\Program Files\\Fortinet\\FortiClient\\FortiClientConsole.exe");
                Thread.Sleep(25000);
            }

            try
            {
                var fortiClientUiElement = RootElement.FindWindow("FortiClient -- The Security Fabric Agent");

                // Probleme de doublons
                var usernameInputsFr = fortiClientUiElement.FindElementsByName("Nom d'utilisateur {{");
                var usernameInputsEn = fortiClientUiElement.FindElementsStartingWithName("Username {{username}}");

                if (usernameInputsEn.Count > 0)
                {
                    usernameInputsEn.First().SetValue("TEST");
                    usernameInputsEn.First().SetValue(FortiClient_Username);

                    usernameInputsEn.ElementAt(1).SetValue("TEST");
                    usernameInputsEn.ElementAt(1).SetValue(FortiClient_Username);

                    var usernamePasswordInputs = fortiClientUiElement.FindElementsStartingWithName("Password {{");

                    if (usernamePasswordInputs.Count == 0)
                    {
                        Assert.Fail("Failed input password!");
                    }

                    usernamePasswordInputs.First().SetValue("TEST");
                    usernamePasswordInputs.First().SetValue(FortiClient_Password);

                    var btnConnectInputs = fortiClientUiElement.FindElementsStartingWithName("Connect");

                    if (btnConnectInputs.Count == 0)
                    {
                        Assert.Fail("Failed button Connect!");
                    }

                    btnConnectInputs.First().Click();

                    var test1 = fortiClientUiElement.FindElementsStartingWithName("VPN Name {{");
                    
                    var test2 = fortiClientUiElement.FindElementsStartingWithName("VPN Name {{vpnName}}");

                }
                else if (usernameInputsFr.Count > 0)
                {
                    usernameInputsFr.First().SetValue(FortiClient_Username);

                    var usernamePasswordInputs = fortiClientUiElement.FindElementsStartingWithName("Password {{");

                    if (usernamePasswordInputs.Count == 0)
                    {
                        Assert.Fail("Failed input password!");
                    }

                    usernamePasswordInputs.First().SetValue(FortiClient_Password);

                    var btnConnectInputs = fortiClientUiElement.FindElementsStartingWithName("Connect");

                    if (btnConnectInputs.Count == 0)
                    {
                        Assert.Fail("Failed button Connect!");
                    }

                    btnConnectInputs.First().Click();
                }
                else
                {
                    Assert.Fail("Failed to grab Username inputs for both english and french variants.");
                }
            }
            finally
            {
                if (fortiClientProcess != null)
                {
                    var fortiClientProcesses = Process.GetProcesses().Where(x => x.ProcessName.ToUpper().StartsWith("FORTICLIENT")).ToList();
                    fortiClientProcesses.ForEach(x => x.Kill());
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
                var fortiClientWindowElement = RootElement.FindWindow("FortiClient -- The Security Fabric Agent");

                fortiClientWindowElement.Click();

                Thread.Sleep(2000);

                Input.Send(Keys.TAB);
                Input.Send(Keys.TAB);
                Input.Send(FortiClient_Username);
                Input.Send(Keys.TAB);
                Input.Send(FortiClient_Password); 
                Input.Send(Keys.TAB);
                Input.Send(Keys.ENTER);

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
    }
}
