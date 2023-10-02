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

            try
            {


                //browser.Visible = true;
                //pdfWebViewer.Navigate(new Uri("about:blank"));

            }
            catch (Exception e)
            {
                Console.WriteLine("browser is visible/ not: ");

            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            /*           // Create OpenFileDialog 
                       Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



                       // Set filter for file extension and default file extension 
                       dlg.DefaultExt = ".png";
                       dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";


                       // Display OpenFileDialog by calling ShowDialog method 
                       Nullable<bool> result = dlg.ShowDialog();
           */

            // Get the selected file name and display in a TextBox 
            string file_path = "C:\\Users\\Admin\\Documents\\Test";
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-fullscreen");


            IWebDriver driver = new ChromeDriver(options);
            var browser = (IJavaScriptExecutor)driver;

            while (true)
            {
                string[] files = Directory.GetFiles(file_path);

                foreach (string file in files)
                {
                    string[] parts = System.IO.Path.GetFileNameWithoutExtension(file).Split(';');

                    if (parts.Length == 3 && int.TryParse(parts[1], out int durationSec) && int.TryParse(parts[2], out int zoom))
                    {
                        driver.Navigate().GoToUrl(file);
                        browser.ExecuteScript("document.body.style.zoom = '0.5'");
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

                /*  driver.Navigate().GoToUrl("file:///C:/Users/Admin/Documents/rapport-annuel-danone-2019.pdf");

                  var title = driver.Title;
                  //Assert.AreEqual("Web form", title);

                  driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(5000);*/
            }
            

           /* var textBox = driver.FindElement(By.Name("my-text"));
            var submitButton = driver.FindElement(By.TagName("button"));

            textBox.SendKeys("Selenium");
            submitButton.Click();

            var message = driver.FindElement(By.Id("message"));
            var value = message.Text;
            //Assert.AreEqual("Received!", value);

            driver.Quit();*/


        }
    }
}
