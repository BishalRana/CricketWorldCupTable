using CricketWorldCupTable.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketWorldCupTable.dto
{
    /// <summary>
    ///  This class represents result of a team played against 
    ///  another team
    /// </summary>
    public class Match
    {
        public Result result { get; set; }

        /// <summary>
        /// matched played against team
        /// </summary>
        public Participator PlayedAgainst { get; set; }
    }
}
