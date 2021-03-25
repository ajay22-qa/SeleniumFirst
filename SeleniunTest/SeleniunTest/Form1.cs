using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeleniunTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        

        public IWebDriver _Driver;
        private void LogIn_Click_1(object sender, EventArgs e)
        {
            //IWebDriver _Driver = new ChromeDriver();
           // _Driver.Navigate().GoToUrl("https://en-gb.facebook.com/login/");

            //var element = _Driver.FindElement(By.Id("email"));
            //element.SendKeys(Uid.Text);
         
            //element = _Driver.FindElement(By.Id("submit button id"));
            //element.Click();


            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--disable-notifications");
            options.AddArgument("--disable-extensions");
            options.AddArgument("--test-type");
            options.AddArgument("--log-level=3");
            //options.AddArgument("--incognito");
            options.AddArgument("disable-infobars");

            ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService(Environment.CurrentDirectory);
            chromeDriverService.HideCommandPromptWindow = true;
            _Driver = new ChromeDriver(chromeDriverService, options);
            ChromeDriverService _chromeDriverService = ChromeDriverService.CreateDefaultService(Environment.CurrentDirectory);
            _chromeDriverService.HideCommandPromptWindow = true;
            _Driver.Manage().Window.Maximize();
            _Driver.Navigate().GoToUrl("https://en-gb.facebook.com/login/");
            Thread.Sleep(2000);
            ReadOnlyCollection<IWebElement> emailElement = _Driver.FindElements(By.Id("email"));
            if (emailElement.Count > 0)
            {

                emailElement[0].SendKeys(Uid.Text);

            }
            ReadOnlyCollection<IWebElement> passwordElement = _Driver.FindElements(By.Id("pass"));
            {
                if (passwordElement.Count > 0)
                {
                    passwordElement[0].SendKeys(Pwd.Text);
                }
            }
            ReadOnlyCollection<IWebElement> loginElement = _Driver.FindElements(By.Id("loginbutton"));
            if (loginElement.Count > 0)
            {
                loginElement[0].Click();
                Thread.Sleep(3000);
                ReadOnlyCollection<IWebElement> usernameWrong = _Driver.FindElements(By.XPath("//*[@class='_5v-0 _53in']"));
                {
                    if (usernameWrong.Count > 0)
                    {
                        if (usernameWrong[0].Text.Contains("The email address or phone number that you've entered doesn't match any account. Sign up for an account."))
                        {
                            _Driver.Quit();
                            MessageBox.Show("Please enter valid facebook Username!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //btnLogIn.Enabled = true;
                            //pictureBox1.Visible = false;
                        }
                        else if (usernameWrong[0].Text.Contains("The password that you've entered is incorrect. Forgotten password?"))
                        {
                            _Driver.Quit();
                            MessageBox.Show("Please enter valid facebook  Password!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //btnLogIn.Enabled = true;
                            //pictureBox1.Visible = false;
                        }
                    }
                    else
                    {
                        _Driver.Navigate().GoToUrl("https://www.facebook.com/marketplace/selling/");
                        var pagesoure = _Driver.PageSource;
                        //htmldocument.LoadHtml(pagesoure);
                        //pictureBox1.Visible = false;
                        MessageBox.Show("LoginSuccess..!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //btnLogIn.Enabled = false;
                        //button1.Enabled = true;
                    }
                }
            }
        }

        private void Pwd_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            
        }

        private void Uid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
