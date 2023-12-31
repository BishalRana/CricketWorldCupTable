using CricketWorldCupTable.data;
using CricketWorldCupTable.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketWorldCupTable.Pattern
{
    public class MatchResultHandler : IObservable<Dictionary<Participator, Team>>
    {
        private readonly HashSet<IObserver<Dictionary<Participator, Team>>> _observers = new HashSet<IObserver<Dictionary<Participator, Team>>>();
        private readonly Dictionary<Participator, Team> _teams = new Dictionary<Participator, Team>();

        public IDisposable Subscribe(IObserver<Dictionary<Participator, Team>> observer)
        {
            // check whether observer is already registered. If not, add it
            if (_observers.Add(observer))
            {
                observer.OnNext(_teams);

            }

            return new Unsubscriber(_observers, observer);
        }

        /// <summary>
        /// first participator and second participator match
        /// result of the match for first participator against second parcticipator
        /// </summary>
        /// <param name="firstParticipator"></param>
        /// <param name="secondParticipator"></param>
        /// <param name="result"></param>
        public void AddTeam(Participator firstParticipator, Participator secondParticipator, Result result)
        {
            Participator[] participators = new Participator[2];
            participators[0] = firstParticipator;
            participators[1] = secondParticipator;

            for (int i = 0; i < participators.Length; i++)
            {
                Team team = null;

                if (_teams.TryGetValue(participators[i], out team))
                {
                    if (i == 1)
                    {
                        if (team.Matches != null)
                        {
                            if (!team.Matches.ContainsKey(participators[i - 1]))
                            {
                                team.NumberOfMatchesPlayed++;
                                Match match = new Match();
                                match.PlayedAgainst = participators[i - 1];
                                SetResult(match, result);
                                team.Matches.Add(match.PlayedAgainst, match);
                                SetTeamDetails(team, match);

                            }
                        }
                    }
                    else if (i == 0)
                    {
                        if (team.Matches != null)
                        {
                            if (!team.Matches.ContainsKey(participators[i + 1]))
                            {
                                team.NumberOfMatchesPlayed++;
                                Match match = new Match();
                                match.result = result;
                                match.PlayedAgainst = participators[i + 1];
                                team.Matches.Add(match.PlayedAgainst, match);
                                SetTeamDetails(team, match);
                            }

                        }
                    }
                }
                else
                {
                    Dictionary<Participator, Match> matches = new Dictionary<Participator, Match>();

                    Match match = new Match();

                    if (i == 1)
                    {
                        SetResult(match, result);
                        match.PlayedAgainst = participators[i - 1];
                    }
                    else if (i == 0)
                    {
                        match.result = result;
                        match.PlayedAgainst = participators[i + 1];

                    }

                    matches.Add(match.PlayedAgainst, match);

                    team = new Team()
                    {
                        Country = participators[i],
                        NumberOfMatchesPlayed = 1,
                        Matches = matches
                    };

                    SetTeamDetails(team, match);

                    _teams.Add(team.Country, team);
                }
            }

            SortTeamsStandingByPoints(_teams);

            CheckAllMatchesPlayed(_teams);

            foreach (var observer in _observers)
            {
                observer.OnNext(_teams);
            }

        }

        private void SetResult(Match match, Result result)
        {
            switch (result)
            {
                case Result.Lost:
                    match.result = Result.Won;
                    break;
                case Result.Won:
                    match.result = Result.Lost;
                    break;
                case Result.Tied:
                    match.result = Result.Tied;
                    break;
                default:
                    match.result = Result.NoResult;
                    break;
            }
        }

        private void SetTeamDetails(Team team, Match match)
        {
            switch (match.result)
            {
                case Result.Won:
                    team.TotalPoints += ((int)match.result);
                    team.NumberOfWonMatches++;
                    break;
                case Result.Lost:
                    team.TotalPoints += ((int)match.result);
                    team.NumberOfLostMatches++;
                    break;
                case Result.Tied:
                    team.TotalPoints += ((int)match.result);
                    team.NumberOfTiedMatches++;
                    break;
                default:
                    team.TotalPoints += ((int)match.result);
                    team.NumberOfNoResultMatches++;
                    break;
            }

        }

        public void SortTeamsStandingByPoints(Dictionary<Participator, Team> teamsMatchesDetails)
        {
            Dictionary<Participator, Team> teamDetails = teamsMatchesDetails.OrderByDescending(teamDict => teamDict.Value.TotalPoints).ThenBy(teamDict => teamDict.Value.NumberOfMatchesPlayed).
             ThenByDescending(teamDict => teamDict.Value.NumberOfWonMatches).ThenByDescending(teamDict => teamDict.Value.NumberOfTiedMatches).
             ThenBy(teamDict => teamDict.Value.NumberOfLostMatches).ToDictionary(kv => kv.Key, kv => kv.Value);

            teamsMatchesDetails.Clear();

            foreach (var team in teamDetails)
            {
                teamsMatchesDetails.Add(team.Key, team.Value);
            }

        }

        /// <summary>
        /// checking if all matches have been played
        /// </summary>
        /// <param name="teams"></param>
        /// <returns></returns>
        public void CheckAllMatchesPlayed(Dictionary<Participator, Team> teams)
        {
            int totalParticipator = Enum.GetNames(typeof(Participator)).Length;

            int totalMatchesPerTeam = totalParticipator - 1;

            if (teams.Count() == totalParticipator)
            {
                foreach (Team team in teams.Values)
                {
                    if (team.NumberOfMatchesPlayed == totalMatchesPerTeam)
                    {
                        team.AllMatchesPlayed = true;
                    }
                }
            }

        }

    }
}
