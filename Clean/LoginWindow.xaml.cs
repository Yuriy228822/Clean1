using System.Windows;

namespace Clean
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            // Простейшая проверка (в реальном приложении используйте безопасный способ хранения и проверки паролей)
            if (username == "admin" && password == "password")
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close(); // Закрыть окно авторизации
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
