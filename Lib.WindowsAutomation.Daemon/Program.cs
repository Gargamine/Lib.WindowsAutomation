using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Lib.WindowsAutomation.Daemon
{
    class Program
    {

        private const string FortiClient_Username = "RCGT";
        private const string FortiClient_Password = "#R1!c2@g3$T4*";

        static void Main()
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
                var usernameInputsEn = fortiClientUiElement.FindElementsByName("Username {{username}}");

                if (usernameInputsEn.Count > 0)
                {
                    usernameInputsEn.First().SetValue(FortiClient_Username);

                    var usernamePasswordInputs = fortiClientUiElement.FindElementsStartingWithName("Password {{");

                    if (usernamePasswordInputs.Count == 0)
                    {
                        Console.WriteLine("Failed input password!");
                        return;
                    }

                    usernamePasswordInputs.First().SetValue(FortiClient_Password);

                    var btnConnectInputs = fortiClientUiElement.FindElementsStartingWithName("Connect");

                    if (btnConnectInputs.Count == 0)
                    {
                        Console.WriteLine("Failed button Connect!");
                        Console.ReadLine();
                        return;
                    }

                    btnConnectInputs.First().Click();

                }
                else if (usernameInputsFr.Count > 0)
                {
                    usernameInputsFr.First().SetValue(FortiClient_Username);

                    var usernamePasswordInputs = fortiClientUiElement.FindElementsStartingWithName("Password {{");

                    if (usernamePasswordInputs.Count == 0)
                    {
                        Console.WriteLine("Failed input password!");
                        Console.ReadLine();
                        return;
                    }

                    usernamePasswordInputs.First().SetValue(FortiClient_Password);

                    var btnConnectInputs = fortiClientUiElement.FindElementsStartingWithName("Connect");

                    if (btnConnectInputs.Count == 0)
                    {
                        Console.WriteLine("Failed button Connect!");
                        Console.ReadLine();
                        return;
                    }

                    btnConnectInputs.First().Click();
                }
                else
                {
                    Console.WriteLine("Failed to grab Username inputs for both english and french variants.");

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadLine();
        }
    }
}
