using System;
using System.Collections.Generic;
using System.Windows;

namespace Clean
{
    /// <summary>
    /// Логика взаимодействия для EditRequestWindow.xaml
    /// </summary>
    public partial class EditRequestWindow : Window
    {
        private Request requestToEdit;
        private List<Request> requests;

        public EditRequestWindow(Request request, List<Request> requests)
        {
            InitializeComponent();
            requestToEdit = request;
            this.requests = requests;

            // Заполнение полей данными заявки
            txtRequestId.Text = request.Id.ToString();
            txtClientName.Text = request.ClientName;
            txtRoom.Text = request.Room;
            txtCleaningType.Text = request.CleaningType;
            txtArea.Text = request.Area.ToString();
            txtCost.Text = request.Cost.ToString();
            txtComment.Text = request.Comment; // Загрузка комментария
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            requestToEdit.ClientName = txtClientName.Text;
            requestToEdit.Room = txtRoom.Text;
            requestToEdit.CleaningType = txtCleaningType.Text;
            requestToEdit.Area = double.TryParse(txtArea.Text, out double area) ? area : 0;
            requestToEdit.Cost = decimal.TryParse(txtCost.Text, out decimal cost) ? cost : 0;
            requestToEdit.Comment = txtComment.Text; // Сохранение комментария

            MessageBox.Show("Изменения сохранены.");
            Close();
        }
    }
}
