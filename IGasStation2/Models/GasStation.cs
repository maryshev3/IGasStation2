using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGasStation2.Models
{
    [Table("gas_stations")]
    public class GasStation
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("company_name")]
        public String CompanyName { get; set; }
        [Column("location")]
        public String Location { get; set; }
        [Column("coordinates")]
        public String Coordinates { get; set; }
        [Column("phone_number")]
        public String PhoneNumber { get; set; }
        [Column("email")]
        public String Email { get; set; }
        [Column("allowed_power")]
        public int AllowedPower { get; set; }
        [Column("current_power")]
        public int CurrentPower { get; set; }
        [Column("power_disel_generator")]
        public int? PowerDiselGenerator { get; set; }
        [Column("type_and_power")]
        public String? TypeAndPower { get; set; }
        [Column("note")]
        public String? Note { get; set; }
    }
}
