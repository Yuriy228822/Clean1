using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean
{
    public class Request
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string Room { get; set; }
        public string CleaningType { get; set; }
        public double Area { get; set; }
        public decimal Cost { get; set; }
        public string[] AdditionalServices { get; set; } // Дополнительные услуги
        public string Comment { get; set; } // Новое свойство для комментариев

        public string Title => $"Заявка #{Id}: {ClientName} - {Room}";
    }



}
