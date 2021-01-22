using System;
using System.ComponentModel.DataAnnotations;

namespace MvcLaundry.Models
{
    public class JenisPakaian
    {
        public int Id { get; set; }
        public string NamaPakaian { get; set; }
        public int HargaPerKg { get; set; }
    }
}