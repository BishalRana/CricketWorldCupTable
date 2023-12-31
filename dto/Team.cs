using CricketWorldCupTable.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketWorldCupTable.dto
{
    public class Team
    {
        /// <summary>
        /// Team participated in cricket world cup.
        /// </summary>
        public Participator Country { get; set; }

        /// <summary>
        /// Number of matches played by a team.
        /// </summary>
        public int NumberOfMatchesPlayed { get; set; }

        /// <summary>
        /// Number of won matches
        /// </summary>
        public int NumberOfWonMatches { get; set; }

        /// <summary>
        /// Number of lost matches
        /// </summary>
        public int NumberOfLostMatches { get; set; }

        /// <summary>
        /// Number of tied matches
        /// </summary>
        public int NumberOfTiedMatches { get; set; }

        /// <summary>
        /// Number of no result matches
        /// </summary>
        public int NumberOfNoResultMatches { get; set; }


        /// <summary>
        /// Total Number of points
        /// </summary>
        public int TotalPoints { get; set; }

        public bool AllMatchesPlayed { get; set; }
        /// summary>
        /// Team matches in detail
        /// key of Participator type is name of the team played against
        /// </summary>
        public Dictionary<Participator, Match> Matches { get; set; }

    }
}
