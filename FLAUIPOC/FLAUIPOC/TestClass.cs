using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlaUI.Core;
using System.Threading;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;

namespace FLAUIPOC
{
    public class TestClass
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

        [Test]
        public void StartApp()
        {
            _app = Application.Launch("notepad.exe");

            _automation = new UIA3Automation();
            _mainWindow = _app.GetMainWindow(_automation);
            _mainWindow.AsTextBox().Text = "This should appear in Notepad";

            Thread.Sleep(3000);
            _app.Close();
        }

        [Test]
        public void CalcTest()
        {
            _app = Application.Launch("calc.exe");
            _automation = new UIA3Automation();
            _allWindowsList = _app.GetAllTopLevelWindows(_automation);
            //these are 0
            var count = _allWindowsList.Count();

            _app.Close();
        }

        [Test]
        public void EdisApp()
        {
            _app = Application.Launch(@"C:\Projects\Nomura\Test App\TestApp\TestApplication.exe");
            _automation = new UIA3Automation();
            _mainWindow = _app.GetMainWindow(_automation);
            var element = FindElementByName("Tab1");
            element.Click();

            element.AsMenu().FindAllDescendants().Where(x => x.Name == "Opt2").First().Click();

        }

        [Test]
        public void DataGridTest()
        {
            _app = Application.Launch(@"C:\Projects\Nomura\Test App\TestApp\TestApplication.exe");
            _automation = new UIA3Automation();
            _mainWindow = _app.GetMainWindow(_automation);
            var addUser = FindElementByName("AddUser");
            addUser.Click();
            Thread.Sleep(500);
            addUser.Click();
            Thread.Sleep(500);
            addUser.Click();

            var DataGrid = FindElementByID("DataGrid").AsDataGridView();
            foreach (var row in DataGrid.Rows)
            {
                foreach (var cell in row.Cells)
                {

                }
            }
        }
    }
}
