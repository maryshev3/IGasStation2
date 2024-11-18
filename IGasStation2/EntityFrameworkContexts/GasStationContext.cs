using IGasStation2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGasStation2.EntityFrameworkContexts
{
    public class GasStationContext : DbContext
    {
        public DbSet<GasStation> GasStations { get; set; }
        public DbSet<GasStationPowerUsing> GasStationPowerUsings { get; set; }
        public GasStationContext(DbContextOptions<GasStationContext> options) : base(options)
        {
        }
    }
}
