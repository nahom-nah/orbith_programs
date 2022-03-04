using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentTracker
{
    internal class Prize
    {
        public int placeNumber { get; set; }
        public int placeName { get; set; }
        public decimal prize { get; set; }

        public double prizePercentage { get; set; }

    }
}
