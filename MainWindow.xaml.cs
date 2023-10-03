using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ImageDisplayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Controls.WebBrowser browser = new System.Windows.Controls.WebBrowser();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected file name and display in a TextBox 
            string file_path = "C:\\Users\\Admin\\Documents\\Test";
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-fullscreen");


            IWebDriver driver = new ChromeDriver(options);
            while (true)
            {
                string[] files = Directory.GetFiles(file_path);

                foreach (string file in files)
                {
                    string[] parts = System.IO.Path.GetFileNameWithoutExtension(file).Split(';');

                    if (parts.Length == 3 && int.TryParse(parts[1], out int durationSec) && int.TryParse(parts[2], out int zoom))
                    {
                        driver.Navigate().GoToUrl(file);
                        ExecuteJavaScript($"document.body.style.zoom = '{(zoom / 100.0)}'");


                        //System.Windows.Forms.SendKeys.SendWait("{F11}");
                        await Task.Delay(durationSec * 1000);
                    }
                    if (System.IO.Path.GetExtension(file).Equals(".web", StringComparison.OrdinalIgnoreCase))
                    {
                        string[] fileparts = File.ReadAllText(file).Split(';');
                        if (fileparts.Length == 2 && int.TryParse(fileparts[0], out int duration))
                        {
                            string url = fileparts[1];
                            driver.Navigate().GoToUrl(url);
                            //System.Windows.Forms.SendKeys.SendWait("{F11}");
                            await Task.Delay(duration * 1000);
                        }
                    }
                }
            }
        }
        private void ExecuteJavaScript(string script)
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript(script);
        }
    }
}
