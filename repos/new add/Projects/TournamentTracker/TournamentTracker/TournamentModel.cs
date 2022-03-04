using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentTracker
{
    internal class TournamentModel
    {
        public string tournamentName { get; set; }
        public decimal enteryFee { get; set; }
        public List<TeamModel> enteredTeams { get; set; } = new List<TeamModel>();
        public List<Prize> prizes = new List<Prize>();
        public List<List<MatchUpModel>> rounds = new List<List<MatchUpModel>>();
        
    }
}
