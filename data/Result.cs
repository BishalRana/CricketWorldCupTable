using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketWorldCupTable.data
{
    public enum Result
    {
        /// <summary>
        /// Point for matches with no result.
        /// </summary>
        NoResult = 0,

        ///<summary>
        /// Points for lost match.
        /// </summary>   
        Lost = 0,

        /// <summary>
        /// Points for tied match
        /// </summary>
        Tied = 1,

        /// <summary>
        /// Points for won match
        /// </summary>
        Won = 2

    }
}
