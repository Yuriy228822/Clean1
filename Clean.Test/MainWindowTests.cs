using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Test
{
    [TestClass]
    public class MainWindowTests
    {
        private MainWindow mainWindow;

        [TestInitialize]
        public void Setup()
        {
            mainWindow = new MainWindow();
            mainWindow.Show();
        }

        [TestCleanup]
        public void Cleanup()
        {
            mainWindow.Close();
        }

        [TestMethod]
        public void AddRequest_IncreasesRequestCount()
        {
            int initialRequestCount = mainWindow.requests.Count;

            mainWindow.AddRequest(new Request
            {
                Id = mainWindow.NextId,
                ClientName = "Сидоров С.С.",
                Room = "Гостиная",
                CleaningType = "Поверхностная",
                Area = 40,
                Cost = 1000
            });

            mainWindow.lvRequests.Items.Refresh();

            Assert.AreEqual(initialRequestCount + 1, mainWindow.lvRequests.Items.Count, "Количество заявок должно увеличиться на 1 после добавления новой заявки.");
        }

        [TestMethod]
        public void UpdateStatistics_CorrectlyCalculatesStatistics()
        {
            mainWindow.AddRequest(new Request
            {
                Id = mainWindow.NextId,
                ClientName = "Тестовый Клиент",
                Room = "Кухня",
                CleaningType = "Генеральная",
                Area = 25,
                Cost = 2500
            });

            mainWindow.UpdateStatistics();

            Assert.AreEqual("3", mainWindow.txtCompletedRequestsCount.Text, "Количество выполненных заявок должно корректно отображаться.");
            Assert.AreEqual("1 500,00 ₽", mainWindow.txtMinPrice.Text, "Минимальная цена заявки должна корректно отображаться.");
            Assert.AreEqual("2 500,00 ₽", mainWindow.txtMaxPrice.Text, "Максимальная цена заявки должна корректно отображаться.");
            Assert.AreEqual("2 000,00 ₽", mainWindow.txtAveragePrice.Text, "Средняя цена заявки должна корректно отображаться.");
        }
    }
}
