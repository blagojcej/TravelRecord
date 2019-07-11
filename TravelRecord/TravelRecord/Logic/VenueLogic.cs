using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TravelRecord.Model;

namespace TravelRecord.Logic
{
    public class VenueLogic
    {
        public async static Task<List<Venue>> GetVenuesAsync(double latitude, double longitude)
        {
            List<Venue> venues=new List<Venue>();

            var url = VenueRoot.GenerateURL(latitude, longitude);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                var venueRoot = JsonConvert.DeserializeObject<VenueRoot>(json);

                venues = venueRoot.response.venues.ToList();
            }

            return venues;
        }
    }
}
