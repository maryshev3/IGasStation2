using IGasStation2.EntityFrameworkContexts;
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

        public IEnumerable<GasStation> GetGasStations(
            string name = "",
            string location = "",
            string phoneNumber = "",
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
    }
}
