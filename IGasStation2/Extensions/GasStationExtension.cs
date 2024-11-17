using IGasStation2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGasStation2.Extensions
{
    public static class GasStationExtension
    {
        public static void Fill(
            this GasStation gasStation,
            string name,
            string location,
            string coordinates,
            string phoneNumber,
            string email,
            string allowedPower,
            string currentPower,
            string powerDiselGenerator,
            string typeAndPower,
            string note
        )
        {
            gasStation.CompanyName = name;
            gasStation.Location = location;
            gasStation.Coordinates = coordinates;
            gasStation.PhoneNumber = phoneNumber;
            gasStation.Email = email;
            gasStation.AllowedPower = Convert.ToInt32(allowedPower);
            gasStation.CurrentPower = Convert.ToInt32(currentPower);
            gasStation.PowerDiselGenerator = String.IsNullOrWhiteSpace(powerDiselGenerator) ? null : Convert.ToInt32(powerDiselGenerator);
            gasStation.TypeAndPower = typeAndPower;
            gasStation.Note = note;
        }
    }
}
