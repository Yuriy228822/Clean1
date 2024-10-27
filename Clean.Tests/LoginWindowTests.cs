using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;
using Clean;

namespace Clean.Tests
{
    [TestClass]
    public class LoginWindowTests
    {
        private LoginWindow loginWindow;

        [TestInitialize]
        public void Setup()
        {
            loginWindow = new LoginWindow();
            loginWindow.Show();
        }

        [TestCleanup]
        public void Cleanup()
        {
            loginWindow.Close();
        }

        [TestMethod]
        public void Login_Successful()
        {
            // Arrange
            var txtUsername = loginWindow.FindName("txtUsername") as TextBox;
            var txtPassword = loginWindow.FindName("txtPassword") as PasswordBox;

            txtUsername.Text = "admin";
            txtPassword.Password = "password";

            // Act
            var button = loginWindow.FindName("LoginButton") as Button;
            button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

            // Assert
            Assert.IsFalse(loginWindow.IsVisible, "LoginWindow should be closed after successful login.");
        }

        [TestMethod]
        public void Login_Failed()
        {
            // Arrange
            var txtUsername = loginWindow.FindName("txtUsername") as TextBox;
            var txtPassword = loginWindow.FindName("txtPassword") as PasswordBox;

            txtUsername.Text = "user";
            txtPassword.Password = "wrongpassword";

            // Act
            var button = loginWindow.FindName("LoginButton") as Button;
            button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

            // Assert
            Assert.IsTrue(loginWindow.IsVisible, "LoginWindow should remain open after failed login.");
        }
    }
}
