using Microsoft.EntityFrameworkCore;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    
    public class VehicleDbContext : DbContext
    {
        public VehicleDbContext( DbContextOptions<VehicleDbContext> options): base(options)
        {   
        }
        public DbSet<VehicleMake> VehicleMake { get; set; }
        public DbSet<VehicleModel> VehicleModel { get; set; }

    }

  
}
