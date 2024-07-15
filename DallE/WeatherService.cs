using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DallE
{
    internal class WeatherService
    {
        public string GetWeather(string city)
        {
            if (city == "København")
            {
                return "Regnvejr";
            }
            else if (city == "Paris")
            {
                return "Solskin";
            }
            else if (city == "New York")
            {
                return "Tåget";
            }
            else
            {
                return "Gråvejr";
            }
        }
    }
}
