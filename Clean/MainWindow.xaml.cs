using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Clean
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Request> requests;
        public int NextId { get; private set; } = 1; // Для хранения следующего ID заявки

        public MainWindow()
        {
            InitializeComponent();
            requests = new List<Request>
            {
                new Request { Id = NextId++, ClientName = "Иванов И.И.", Room = "Кабинет", CleaningType = "Генеральная", Area = 50, Cost = 2000 },
                new Request { Id = NextId++, ClientName = "Петров П.П.", Room = "Офис", CleaningType = "Клининговая", Area = 30, Cost = 1500 }
            };
            lvRequests.ItemsSource = requests;

            UpdateStatistics(); // Обновляем статистику при загрузке окна
        }

        private void AddRequest_Click(object sender, RoutedEventArgs e)
        {
            AddRequestWindow addRequestWindow = new AddRequestWindow(this);
            addRequestWindow.ShowDialog();
            lvRequests.Items.Refresh();
            UpdateStatistics(); // Обновляем статистику после добавления заявки
        }

        public void AddRequest(Request newRequest)
        {
            requests.Add(newRequest);
            NextId++; // Увеличиваем следующий ID
        }

        private void EditRequest_Click(object sender, RoutedEventArgs e)
        {
            if (lvRequests.SelectedItem is Request selectedRequest)
            {
                EditRequestWindow editWindow = new EditRequestWindow(selectedRequest, requests);
                editWindow.ShowDialog();
                lvRequests.Items.Refresh();
                UpdateStatistics(); // Обновляем статистику после редактирования заявки
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите заявку для редактирования.");
            }
        }

        private void UpdateStatistics()
        {
            // Количество выполненных заявок
            int completedRequestsCount = requests.Count;
            txtCompletedRequestsCount.Text = completedRequestsCount.ToString();

            // Минимальная, максимальная и средняя цена заявки
            if (completedRequestsCount > 0)
            {
                decimal minPrice = requests.Min(r => r.Cost);
                decimal maxPrice = requests.Max(r => r.Cost);
                decimal averagePrice = requests.Average(r => r.Cost);

                txtMinPrice.Text = minPrice.ToString("C"); // Форматируем как валюту
                txtMaxPrice.Text = maxPrice.ToString("C"); // Форматируем как валюту
                txtAveragePrice.Text = averagePrice.ToString("C"); // Форматируем как валюту
            }
            else
            {
                txtMinPrice.Text = "0";
                txtMaxPrice.Text = "0";
                txtAveragePrice.Text = "0";
            }

            // Пример отображения дополнительной информации
            // Здесь можно добавить статистику по дополнительным услугам
            // Например, подсчет частоты использования услуг
            // Для примера, просто выводим количество уникальных типов уборки
            var cleaningTypes = requests.Select(r => r.CleaningType).Distinct().Count();
            txtAdditionalServices.Text = $"Количество типов уборки: {cleaningTypes}";
        }

        public bool Authorize(string login, string password)
        {
            throw new NotImplementedException();
        }
    }
}
