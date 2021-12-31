using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JvHomes.Models
{
    public class Expense:Common
    {
        public string Date { get; set; }
        public string Narration { get; set; }
        public string Type { get; set; }
        public string Amount { get; set; }
    }
}