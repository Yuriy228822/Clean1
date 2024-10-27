using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Clean.Test
{
    [TestClass]
    public class AddRequestWindowTests
    {
        private MainWindow mainWindow;
        private AddRequestWindow addRequestWindow;

        [TestInitialize]
        public void Setup()
        {
            mainWindow = new MainWindow();
            addRequestWindow = new AddRequestWindow(mainWindow);
            addRequestWindow.Show();
        }

        [TestCleanup]
        public void Cleanup()
        {
            addRequestWindow.Close();
        }

        [TestMethod]
        public void AddRequestWindow_CalculateCost_CorrectlyCalculatesTotalCost()
        {
            // Устанавливаем площадь
            addRequestWindow.txtArea.Text = "10"; // 10 кв.м

            // Устанавливаем тип уборки (Генеральная - 2000)
            ComboBoxItem cleaningType = (ComboBoxItem)addRequestWindow.cmbCleaningType.Items[0];
            addRequestWindow.cmbCleaningType.SelectedItem = cleaningType;

            // Устанавливаем дополнительные услуги
            CheckBox windowCleaning = (CheckBox)addRequestWindow.stackPanelAdditionalServices.Children[0];
            windowCleaning.IsChecked = true; // 500

            CheckBox disinfection = (CheckBox)addRequestWindow.stackPanelAdditionalServices.Children[1];
            disinfection.IsChecked = true; // 700

            // Расчет стоимости
            addRequestWindow.CalculateCost(null, null);

            // Ожидаемую стоимость: (2000 + 500 + 700) * 10 = 32000
            string expectedCost = "32000,00";
            Assert.AreEqual(expectedCost, addRequestWindow.txtCost.Text, "Стоимость должна рассчитываться корректно.");
        }


        [TestMethod]
        public void AddRequest_Should_AddNewRequestToList()
        {
            // Arrange
            var addRequestWindow = new AddRequestWindow(mainWindow);

            // Устанавливаем значения для новых данных заявки
            addRequestWindow.txtClientName.Text = "Сидоров С.С.";
            addRequestWindow.txtRoom.Text = "Кабинет";
            addRequestWindow.cmbCleaningType.SelectedIndex = 0; // Генеральная
            addRequestWindow.txtArea.Text = "50";

            // Act
            addRequestWindow.AddRequest_Click(null, null); // Симуляция нажатия кнопки "Добавить"

            // Assert
            Assert.AreEqual(3, mainWindow.requests.Count, "Количество заявок должно быть 3");

            // Проверяем детали последней заявки
            var lastRequest = mainWindow.requests[mainWindow.requests.Count - 1];
            Assert.AreEqual("Сидоров С.С.", lastRequest.ClientName, "Имя клиента должно быть 'Сидоров С.С.'");
            Assert.AreEqual("Кабинет", lastRequest.Room, "Комната должна быть 'Кабинет'");
            Assert.AreEqual("Генеральная", lastRequest.CleaningType, "Тип уборки должен быть 'Генеральная'");
            Assert.AreEqual(50, lastRequest.Area, "Площадь должна быть 50");
            Assert.IsTrue(lastRequest.Cost > 0, "Стоимость должна быть больше 0");
        }
    }
}
