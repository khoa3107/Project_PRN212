using System;
using System.Collections.Generic;
using System.Text;

namespace Project_PRN212.Models
{

        public class ProductionOrder
        {
            public int Id { get; set; }
            public string ProductName { get; set; } = "";  
            public int Quantity { get; set; }               
            public string Unit { get; set; } = "cái";      
            public string? Note { get; set; }
            public DateTime CreatedAt { get; set; } = DateTime.Now;
            public string CreatedBy { get; set; } = "";  
        }
    }


