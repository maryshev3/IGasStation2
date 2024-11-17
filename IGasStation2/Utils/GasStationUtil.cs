using IGasStation2.EntityFrameworkContexts;
using IGasStation2.Extensions;
using IGasStation2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGasStation2.Utils
{
    public class GasStationUtil
    {
        private readonly GasStationContext _gasStationContext;

        public GasStationUtil(GasStationContext gasStationContext) 
        {
            _gasStationContext = gasStationContext;
        }

        public List<GasStation> GetGasStations(
            string name = "",
            string location = "",
            string phoneNumber = "",
            string email = "",
            string allowedPower = "",
            string currentPower = "",
            string powerDiselGenerator = "",
            string typeAndPower = ""
        )
        {
            return _gasStationContext
                .GasStations
                .Where(x =>
                    x.CompanyName.Contains(name)
                    && x.Location.Contains(location)
                    && x.PhoneNumber.Contains(phoneNumber)
                    && (String.IsNullOrWhiteSpace(allowedPower) ? true : x.AllowedPower == Convert.ToInt32(allowedPower))
                    && (String.IsNullOrWhiteSpace(currentPower) ? true : x.CurrentPower == Convert.ToInt32(currentPower))
                    && (String.IsNullOrWhiteSpace(powerDiselGenerator) ? true : x.PowerDiselGenerator == Convert.ToInt32(powerDiselGenerator))
                    && (String.IsNullOrWhiteSpace(typeAndPower) ? true : x.TypeAndPower != default && x.TypeAndPower.Contains(typeAndPower))
                )
            .ToList();
        }

        public void RemoveGasStation(GasStation gasStation)
        {
            _gasStationContext.GasStations.Remove(gasStation);

            _gasStationContext.SaveChanges();
        }

        public void EditGasStation(
            GasStation gasStation,
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
            gasStation.Fill(
                name,
                location,
                coordinates,
                phoneNumber,
                email,
                allowedPower,
                currentPower,
                powerDiselGenerator,
                typeAndPower,
                note
            );

            _gasStationContext.SaveChanges();
        }

        public List<GasStationPowerUsing> GetPowerUsings(GasStation gasStation)
        {
            return _gasStationContext
                .GasStationPowerUsings
                .Where(x => x.GasStation == gasStation)
                .ToList();
        }

        public void MergePowerUsing(
            GasStation gasStation,
            string year,
            string powerUsing
        )
        {
            int yearConverted = Convert.ToInt32(year);
            int powerUsingConverted = Convert.ToInt32(powerUsing);

            GasStationPowerUsing? existing = _gasStationContext.GasStationPowerUsings.FirstOrDefault(x =>
                x.GasStation == gasStation
                && x.Year == yearConverted
            );

            if (existing == default)
            {
                _gasStationContext.GasStationPowerUsings.Add(
                    new GasStationPowerUsing()
                    {
                        GasStation = gasStation,
                        Year = yearConverted,
                        PowerUsing = powerUsingConverted
                    }
                );
            }
            else
            {
                existing.PowerUsing = powerUsingConverted;
            }

            _gasStationContext.SaveChanges();
        }

        public void RemovePowerUsing(GasStationPowerUsing gasStationPowerUsing)
        {
            _gasStationContext.GasStationPowerUsings.Remove(gasStationPowerUsing);

            _gasStationContext.SaveChanges();
        }
    }
}
