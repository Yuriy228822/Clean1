using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Clean
{
    public partial class AddRequestWindow : Window
    {
        private MainWindow mainWindow;

        public AddRequestWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void CalculateCost(object sender, RoutedEventArgs e)
        {
            // Получаем выбранный тип уборки
            if (cmbCleaningType.SelectedItem is ComboBoxItem selectedCleaningType)
            {
                decimal baseCost = Convert.ToDecimal((string)selectedCleaningType.Tag);

                // Площадь
                decimal area = decimal.TryParse(txtArea.Text, out decimal parsedArea) ? parsedArea : 0;

                // Стоимость дополнительных услуг
                decimal additionalServicesCost = 0;
                foreach (var checkBox in stackPanelAdditionalServices.Children.OfType<CheckBox>())
                {
                    if (checkBox.IsChecked == true)
                    {
                        additionalServicesCost += Convert.ToDecimal(checkBox.Tag);
                    }
                }

                // Итоговая стоимость
                decimal totalCost = (baseCost + additionalServicesCost) * area;
                txtCost.Text = totalCost.ToString("F2");
            }
        }

        private void AddRequest_Click(object sender, RoutedEventArgs e)
        {
            // Создаем новую заявку на основе введенных данных
            Request newRequest = new Request
            {
                Id = mainWindow.NextId,
                ClientName = txtClientName.Text,
                Room = txtRoom.Text,
                CleaningType = ((ComboBoxItem)cmbCleaningType.SelectedItem)?.Content.ToString(),
                Area = double.TryParse(txtArea.Text, out double area) ? area : 0,
                Cost = decimal.TryParse(txtCost.Text, out decimal cost) ? cost : 0,
                Comment = ""
            };

            // Добавляем заявку в список заявок в главном окне
            mainWindow.AddRequest(newRequest);
            Close();
        }
    }
}
