using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PodCastApp.Models
{
    public class PodCastDbContext : DbContext
    {
        public DbSet<PodCast> PodCasts { get; set; }
        public PodCastDbContext() : base("PodCastEntities")
        {
        }
    }
}