using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGasStation2.Models
{
    [Table("gas_station_power_usings")]
    public class GasStationPowerUsing
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [ForeignKey("gas_stations_id")]
        public GasStation GasStation { get; set; }
        [Column("year")]
        public int Year { get; set; }
        [Column("power_using")]
        public int PowerUsing { get; set; }
    }
}
