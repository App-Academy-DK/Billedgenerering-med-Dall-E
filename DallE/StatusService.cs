using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DallE
{
    internal class StatusService
    {
        public bool IsOnline(string location)
        {
            return location == "København" || location == "Paris";
        }
    }
}
