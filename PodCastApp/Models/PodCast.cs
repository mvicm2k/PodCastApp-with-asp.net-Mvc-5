using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PodCastApp.Models
{
    public class PodCast
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string file { get; set; }
        public string tag { get; set; }
        public DateTime dateUploaded { get; set; }
    }
}