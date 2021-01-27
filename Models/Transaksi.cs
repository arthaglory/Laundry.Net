using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic; 

namespace MvcLaundry.Models
{
    public class Transaksi
    {
        public int Id { get; set; }
        public String NamaUser { get; set; }
        public String AlamatUser { get; set; }
        public String NoHPUser { get; set; }
        public DateTime TglTransaksi { get; set; }
        public int TotalTransaksi { get; set; }

        public Users Users{ get; set;}
    }
}