using System;
using System.ComponentModel.DataAnnotations;

namespace MvcLaundry.Models
{
    public class Pegawai
    {
        public int Id { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string NoHp { get; set; }
        public string Jabatan { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}