using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("JenisPakaian")]
        public int JenisPakaianId { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }
        public Users Users{ get; set;}
        public JenisPakaian JenisPakaian { get; set; }
    }
}