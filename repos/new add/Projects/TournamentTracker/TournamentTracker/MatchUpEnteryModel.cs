using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentTracker
{
    internal class MatchUpEnteryModel
    {
        public TeamModel TeamCompeting { get; set; }
        public int score { get; set; }
        public MatchUpModel parentMatchUp { get; set; } 

        public MatchUpEnteryModel(double initialScore)
        {
            Console.WriteLine("match up entery");
        }
    }
}
