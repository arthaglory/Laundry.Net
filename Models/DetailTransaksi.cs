using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic; 

namespace MvcLaundry.Models
{
    public class DetailTransaksi
    {
        public int Id { get; set; }
        public int IdTransaksi { get; set; }
        public int IdPakaian { get; set; }
        public String NamaPakaian { get; set; }
        public int HargaPakaian { get; set; }
    }
}