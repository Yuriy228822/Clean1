using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Test
{
    [TestClass]
    public class EditRequestWindowTests
    {
        private Request testRequest;
        private List<Request> requests;
        private EditRequestWindow editWindow;

        [TestInitialize]
        public void Setup()
        {
            testRequest = new Request
            {
                Id = 1,
                ClientName = "Иванов И.И.",
                Room = "Кабинет",
                CleaningType = "Генеральная",
                Area = 50,
                Cost = 2000,
                Comment = "Необходима дополнительная уборка."
            };

            requests = new List<Request> { testRequest };
            editWindow = new EditRequestWindow(testRequest, requests);
            editWindow.Show();
        }

        [TestCleanup]
        public void Cleanup()
        {
            editWindow.Close();
        }

        [TestMethod]
        public void EditRequestWindow_InitializesWithCorrectValues()
        {
            Assert.AreEqual(testRequest.Id.ToString(), editWindow.txtRequestId.Text, "Request ID должен инициализироваться правильно.");
            Assert.AreEqual(testRequest.ClientName, editWindow.txtClientName.Text, "Client Name должен инициализироваться правильно.");
            Assert.AreEqual(testRequest.Room, editWindow.txtRoom.Text, "Room должен инициализироваться правильно.");
            Assert.AreEqual(testRequest.CleaningType, editWindow.txtCleaningType.Text, "Cleaning Type должен инициализироваться правильно.");
            Assert.AreEqual(testRequest.Area.ToString(), editWindow.txtArea.Text, "Area должен инициализироваться правильно.");
            Assert.AreEqual(testRequest.Cost.ToString(), editWindow.txtCost.Text, "Cost должен инициализироваться правильно.");
            Assert.AreEqual(testRequest.Comment, editWindow.txtComment.Text, "Комментарий должен инициализироваться правильно.");
        }

        [TestMethod]
        public void EditRequestWindow_SavesChangesCorrectly()
        {
            // Изменяем значения в текстовых полях
            editWindow.txtClientName.Text = "Петров П.П.";
            editWindow.txtRoom.Text = "Офис";
            editWindow.txtCleaningType.Text = "Клининговая";
            editWindow.txtArea.Text = "30";
            editWindow.txtCost.Text = "1500";
            editWindow.txtComment.Text = "Требуется уборка после ремонта.";

            // Сохраняем изменения
            editWindow.SaveChanges_Click(null, null);

            // Проверяем, что изменения сохранились
            Assert.AreEqual("Петров П.П.", testRequest.ClientName, "Client Name должен быть обновлен.");
            Assert.AreEqual("Офис", testRequest.Room, "Room должен быть обновлен.");
            Assert.AreEqual("Клининговая", testRequest.CleaningType, "Cleaning Type должен быть обновлен.");
            Assert.AreEqual(30, testRequest.Area, "Area должен быть обновлен.");
            Assert.AreEqual(1500, testRequest.Cost, "Cost должен быть обновлен.");
            Assert.AreEqual("Требуется уборка после ремонта.", testRequest.Comment, "Комментарий должен быть обновлен.");
        }
    }
}
