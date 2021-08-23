using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;
using NUnit.Framework;
using System.Linq;
using System.Threading;

namespace FLAUINetCore
{
    public class TestClassWinApp
    {
        Application _app;
        Window _mainWindow;
        Window[] _allWindowsList;
        UIA3Automation _automation;

        private AutomationElement FindElementByName(string text)
        {
            var element = _mainWindow.FindFirstDescendant(cf => cf.ByName(text));
            return element;
        }

        private AutomationElement FindElementByID(string text)
        {
            var element = _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId(text));
            return element;
        }

        [Category("Notepad")]
        [Test]
        public void StartApp()
        {
            System.Console.WriteLine("The test starts here");
            _app = Application.Launch("notepad.exe");
            System.Console.WriteLine("App is launched");

            _automation = new UIA3Automation();
            _mainWindow = _app.GetMainWindow(_automation);
            _mainWindow.AsTextBox().Text = "This should appear in Notepad";
            var FileMenu = _mainWindow.FindAllDescendants(x => x.ByName("File")).First();
            FileMenu.Click();

            var fileMenuItem = _mainWindow.FindAllDescendants(x => x.ByName("Save")).First();

            Thread.Sleep(3000);
            _app.Close();
            System.Console.WriteLine("The test ends here");
        }

    }
}
