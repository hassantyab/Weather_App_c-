using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather_Application
{
    class WeatherInfo
    {
        public class coord
        {
            public double lon { get; set; }
            public double lat { get; set; }
        }

        public class weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }

        public class main
        {
            public double temp { get; set; }
            public double pressure { get; set; }
            public double humidity { get; set; }
            public double temp_min { get; set; }

            public double temp_max { get; set; }
        
        }

        public class wind
        {
            public double speed { get; set; }
            public double deg { get; set; }
        }

        public class sys
        {
            public string country { get; set; }
            public double sunrise { get; set; }
            public double sunset { get; set; }
        }

        public class root
        {
           
            public sys sys { get; set; }
            public List<weather> weather { get; set; }
            public main main { get; set; }
            public wind wind { get; set; }
          
            
            public int dt { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public int cod { get; set; }
        }
    }
}
