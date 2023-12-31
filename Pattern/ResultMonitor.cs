using CricketWorldCupTable.data;
using CricketWorldCupTable.dto;
using CricketWorldCupTable.thirdpartyres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketWorldCupTable.Pattern
{
    public class ResultMonitor : IObserver<Dictionary<Participator, Team>>
    {

        private IDisposable cancellation;
        private String _sportsMonitorChannel;
        private readonly Table teamStandingTable;

        public ResultMonitor(String sportsMonitorChannel)
        {
            this._sportsMonitorChannel = sportsMonitorChannel;
            this.teamStandingTable = new Table();
            string[] columnNames =
                    {
                        "TEAM",
                        "PLAYED",
                        "WON",
                        "LOST",
                        "TIED",
                        "N/R",
                        "POINTS"
                    };

            teamStandingTable.SetHeaders(columnNames);

        }

        public virtual void Subscribe(MatchResultHandler provider)
        {
            cancellation = provider.Subscribe(this);
        }

        public virtual void Unsubscribe()
        {
            cancellation?.Dispose();
            teamStandingTable.ClearRows();
        }
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(Dictionary<Participator, Team> teamsMatchesDetails)
        {
            List<Team> teams = teamsMatchesDetails.Values.ToList();

            Console.WriteLine(_sportsMonitorChannel + " Presents World Cup Teams Standing");


            if (teams.Count() > 0)
            {
                teamStandingTable.ClearRows();

                if (CheckAllMatchesPlayed(teams))
                {
                    Console.WriteLine("Congratulations To Top Four Team For Qualifying To Semi-Finals");

                }
                else
                {
                    Console.WriteLine("Current Team Position");
                }

                foreach (Team team in teams)
                {
                    teamStandingTable.AddRow(team.Country.ToString(), team.NumberOfMatchesPlayed.ToString(), team.NumberOfWonMatches.ToString(),
                    team.NumberOfLostMatches.ToString(), team.NumberOfTiedMatches.ToString(), team.NumberOfNoResultMatches.ToString(), team.TotalPoints.ToString());
                }

                Console.WriteLine(teamStandingTable.ToString());

            }

        }

        /// <summary>
        /// checking if all matches have been played
        /// </summary>
        /// <param name="teams"></param>
        /// <returns></returns>
        private bool CheckAllMatchesPlayed(List<Team> teams)
        {
            if (teams.Count > 0)
            {
                foreach (Team team in teams)
                {
                    if (!team.AllMatchesPlayed)
                    {
                        return false;
                    }
                }

            }
            else
            {

                return false;
            }

            return true;


        }

    }
}
