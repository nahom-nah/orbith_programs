﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentTracker
{
    internal class TeamModel
    {
        List<PersonModel> teamMembers = new List<PersonModel>(); 
        public string teamName { get; set; }
    }
}