using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace TheWorld.Services
{
    public class CoordService
    {
        private ILogger<CoordService> _logger;

        public CoordService(ILogger<CoordService> logger)
        {
            _logger = logger;
        }

        public async Task<CoordServiceResult> Lookup(string location)
        {
            var result = new CoordServiceResult()
            {
                Success = false,
                Message = "Undetermined failure while looking up coordinates"
            };
            // Look  Coordinates Bing map services....
            var encodedLocation = System.Net.WebUtility.UrlEncode(location);
            var url = $"http://maps.googleapis.com/maps/api/geocode/json?address={encodedLocation}&sensor=true";
            var client = new HttpClient();
            var json = await client.GetStringAsync(url);

            var res = JObject.Parse(json);
            if (res["status"].Equals("ZERO_RESULTS"))
            {
                result.Message = "Location was not found.";
                return result;
            }
            if (res["status"].ToString() != "OK")
            {
                result.Message = "Unknown error!";
                return result;
            }
            result.Latitude =  (double)res["results"][0]["geometry"]["location"]["lat"];
            result.Longitude = (double)res["results"][0]["geometry"]["location"]["lng"];
            result.Message = "Success";
            result.Success = true;
            return result;
            
        }
    }
}
