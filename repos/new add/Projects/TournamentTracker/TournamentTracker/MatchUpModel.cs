using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentTracker
{
    internal class MatchUpModel
    {
        public List<MatchUpEnteryModel> Enteries = new List<MatchUpEnteryModel>();
        public TeamModel winner { get; set; }
        public int matchupRound { get; set; }
    }
}
