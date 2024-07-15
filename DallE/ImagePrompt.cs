using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DallE
{
    internal class ImagePrompt
    {        
        public string City { get; set; }
        public string Weather { get; set; }
        public int Time { get; set; }
        public bool Status { get; set; }

        public string Prompt
        {
            get
            {
                string person;
                if (Status)
                {
                    person = "der giver thumbs up";
                } else
                {
                    person = "i panik";
                }
                return $"Et billede af en velkendt bygning i {City} klokken {Time} i {Weather}. " +
                    $"Foran står en person {person}";
            }
        }

       
    }
}
