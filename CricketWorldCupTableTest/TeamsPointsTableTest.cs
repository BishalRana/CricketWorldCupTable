using CricketWorldCupTable.data;
using CricketWorldCupTable.dto;
using CricketWorldCupTable.Pattern;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Match = CricketWorldCupTable.dto.Match;

namespace CricketWorldCupTableTest
{
    [TestClass]
    public class TeamsPointsTableTest
    {
        [TestMethod()]
        public void SortTeamsStanding_DiffPoints_SameNumberOfMatches_Test()
        {

            Match matchAustralia = new Match()
            {
                PlayedAgainst = Participator.SriLanka,
                result = Result.Won
            };

            Dictionary<Participator, Match> australiaMatches = new Dictionary<Participator, Match>();
            australiaMatches.Add(matchAustralia.PlayedAgainst, matchAustralia);

            Team AustralianTeam = new Team()
            {
                Country = Participator.Australia,
                NumberOfMatchesPlayed = 1,
                TotalPoints = 2,
                NumberOfWonMatches = 1,
                Matches = australiaMatches
            };

            Match matchSriLanka = new Match()
            {
                PlayedAgainst = Participator.Australia,
                result = Result.Lost
            };

            Dictionary<Participator, Match> SriLankaMatches = new Dictionary<Participator, Match>();
            SriLankaMatches.Add(matchSriLanka.PlayedAgainst, matchSriLanka);

            Team SrilankanTeam = new Team()
            {
                Country = Participator.SriLanka,
                NumberOfMatchesPlayed = 1,
                TotalPoints = 0,
                NumberOfLostMatches = 1,
                Matches = SriLankaMatches
            };

            Dictionary<Participator, Team> teams = new Dictionary<Participator, Team>();
            teams.Add(AustralianTeam.Country, AustralianTeam);
            teams.Add(SrilankanTeam.Country, SrilankanTeam);

            MatchResultHandler matchResultHandler = new MatchResultHandler();
            matchResultHandler.SortTeamsStandingByPoints(teams);
            List<Team> teamsPositions = teams.Select(kvp => kvp.Value).ToList();

            Assert.AreEqual("Australia", teamsPositions.First().Country.ToString(), teamsPositions.First().Country + " At First Position");
            Assert.AreEqual("SriLanka", teamsPositions.Last().Country.ToString(), teamsPositions.Last().Country + " At Last Position");
            Assert.AreEqual(2, teamsPositions.First().TotalPoints, "Total Points of " + teamsPositions.First().Country + " : 2");
            Assert.AreEqual(0, teamsPositions.Last().TotalPoints, "Total Points of " + teamsPositions.Last().Country + " : 0");

        }

        [TestMethod]
        public void SortTeamStanding_SamePoints_DifferentNumberOfMatches_Test()
        {
            Match matchAus1 = new Match()
            {
                PlayedAgainst = Participator.SriLanka,
                result = Result.Won
            };

            Match matchAus2 = new Match()
            {
                PlayedAgainst = Participator.England,
                result = Result.Tied
            };


            Match matchAus3 = new Match()
            {
                PlayedAgainst = Participator.NewZealand,
                result = Result.Lost
            };

            Dictionary<Participator, Match> ausMatches = new Dictionary<Participator, Match>();
            ausMatches.Add(matchAus1.PlayedAgainst, matchAus1);
            ausMatches.Add(matchAus2.PlayedAgainst, matchAus2);
            ausMatches.Add(matchAus3.PlayedAgainst, matchAus3);

            Team AustralianTeam = new Team()
            {
                Country = Participator.Australia,
                NumberOfMatchesPlayed = 3,
                TotalPoints = 3,
                NumberOfLostMatches = 1,
                NumberOfTiedMatches = 1,
                NumberOfWonMatches = 1,
                Matches = ausMatches
            };

            Match matchSri1 = new Match()
            {
                PlayedAgainst = Participator.Australia,
                result = Result.Lost
            };

            Dictionary<Participator, Match> sriMatches = new Dictionary<Participator, Match>();
            sriMatches.Add(matchSri1.PlayedAgainst, matchSri1);

            Team SrilankanTeam = new Team()
            {
                Country = Participator.SriLanka,
                NumberOfMatchesPlayed = 1,
                TotalPoints = 0,
                NumberOfLostMatches = 1,
                Matches = sriMatches
            };

            Match matchEng1 = new Match()
            {
                PlayedAgainst = Participator.Australia,
                result = Result.Tied
            };

            Match matchEng2 = new Match()
            {
                PlayedAgainst = Participator.NewZealand,
                result = Result.Tied
            };

            Dictionary<Participator, Match> engMatches = new Dictionary<Participator, Match>();
            engMatches.Add(matchEng1.PlayedAgainst, matchEng1);
            engMatches.Add(matchEng2.PlayedAgainst, matchEng2);

            Team EnglandTeam = new Team()
            {
                Country = Participator.England,
                NumberOfMatchesPlayed = 2,
                TotalPoints = 2,
                NumberOfTiedMatches = 2,
                Matches = engMatches
            };

            Match matchNewZealand1 = new Match()
            {
                PlayedAgainst = Participator.Australia,
                result = Result.Won
            };

            Match matchNewZealand2 = new Match()
            {
                PlayedAgainst = Participator.England,
                result = Result.Tied
            };

            Dictionary<Participator, Match> newZealandMatches = new Dictionary<Participator, Match>();
            newZealandMatches.Add(matchNewZealand1.PlayedAgainst, matchNewZealand1);
            newZealandMatches.Add(matchNewZealand2.PlayedAgainst, matchNewZealand2);

            Team NewZealandTeam = new Team()
            {
                Country = Participator.NewZealand,
                NumberOfMatchesPlayed = 2,
                TotalPoints = 3,
                NumberOfTiedMatches = 1,
                NumberOfWonMatches = 1,
                Matches = newZealandMatches
            };

            Dictionary<Participator, Team> teams = new Dictionary<Participator, Team>();
            teams.Add(AustralianTeam.Country, AustralianTeam);
            teams.Add(SrilankanTeam.Country, SrilankanTeam);
            teams.Add(EnglandTeam.Country, EnglandTeam);
            teams.Add(NewZealandTeam.Country, NewZealandTeam);

            MatchResultHandler matchResultHandler = new MatchResultHandler();

            matchResultHandler.SortTeamsStandingByPoints(teams);

            List<Team> teamsPositions = teams.Select(kvp => kvp.Value).ToList();

            Assert.AreEqual("NewZealand", teamsPositions.First().Country.ToString(), teamsPositions.First().Country + " At First Position");
            Assert.AreEqual("Australia", teamsPositions.ElementAt(1).Country.ToString(), teamsPositions.ElementAt(1).Country + " At Second Position");
            Assert.AreEqual("England", teamsPositions.ElementAt(2).Country.ToString(), teamsPositions.ElementAt(2).Country + " At Third Position");
            Assert.AreEqual("SriLanka", teamsPositions.ElementAt(3).Country.ToString(), teamsPositions.ElementAt(3).Country + " At Last Position");
        }

        [TestMethod]
        public void SortTeamStanding_SamePoints_SameNumberOfMatches_DifferentNumberOfWonMatches_Test()
        {
            Match engMatch1 = new Match()
            {
                PlayedAgainst = Participator.Australia,
                result = Result.Tied
            };

            Match engMatch2 = new Match()
            {
                PlayedAgainst = Participator.NewZealand,
                result = Result.Won
            };

            Match engMatch3 = new Match()
            {
                PlayedAgainst = Participator.Pakistan,
                result = Result.Lost
            };

            Match engMatch4 = new Match()
            {
                PlayedAgainst = Participator.SriLanka,
                result = Result.Tied
            };

            Dictionary<Participator, Match> englandMatches = new Dictionary<Participator, Match>();
            englandMatches.Add(engMatch1.PlayedAgainst, engMatch1);
            englandMatches.Add(engMatch2.PlayedAgainst, engMatch2);
            englandMatches.Add(engMatch3.PlayedAgainst, engMatch3);
            englandMatches.Add(engMatch4.PlayedAgainst, engMatch4);

            Team EnglandTeam = new Team()
            {
                Country = Participator.England,
                NumberOfMatchesPlayed = 4,
                TotalPoints = 4,
                NumberOfWonMatches = 1,
                NumberOfLostMatches = 1,
                NumberOfTiedMatches = 2,
                Matches = englandMatches
            };

            Match PakistanMatch1 = new Match()
            {
                PlayedAgainst = Participator.England,
                result = Result.Won
            };

            Match PakistanMatch2 = new Match()
            {
                PlayedAgainst = Participator.SriLanka,
                result = Result.Won
            };

            Match PakistanMatch3 = new Match()
            {
                PlayedAgainst = Participator.NewZealand,
                result = Result.Lost
            };

            Match PakistanMatch4 = new Match()
            {
                PlayedAgainst = Participator.Australia,
                result = Result.Lost
            };

            Dictionary<Participator, Match> PakistanMatches = new Dictionary<Participator, Match>();
            PakistanMatches.Add(PakistanMatch1.PlayedAgainst, PakistanMatch1);
            PakistanMatches.Add(PakistanMatch2.PlayedAgainst, PakistanMatch2);
            PakistanMatches.Add(PakistanMatch3.PlayedAgainst, PakistanMatch3);
            PakistanMatches.Add(PakistanMatch4.PlayedAgainst, PakistanMatch4);

            Team PakistanTeam = new Team()
            {
                Country = Participator.Pakistan,
                NumberOfMatchesPlayed = 4,
                TotalPoints = 4,
                NumberOfLostMatches = 2,
                NumberOfWonMatches = 2,
                Matches = PakistanMatches
            };

            Match AustraliaMatch1 = new Match()
            {
                PlayedAgainst = Participator.Pakistan,
                result = Result.Won
            };

            Match AustraliaMatch2 = new Match()
            {
                PlayedAgainst = Participator.England,
                result = Result.Tied
            };

            Match AustraliaMatch3 = new Match()
            {
                PlayedAgainst = Participator.SriLanka,
                result = Result.Lost
            };

            Match AustraliaMatch4 = new Match()
            {
                PlayedAgainst = Participator.NewZealand,
                result = Result.Lost
            };

            Dictionary<Participator, Match> AustraliaMatches = new Dictionary<Participator, Match>();
            AustraliaMatches.Add(AustraliaMatch1.PlayedAgainst, AustraliaMatch1);
            AustraliaMatches.Add(AustraliaMatch2.PlayedAgainst, AustraliaMatch2);
            AustraliaMatches.Add(AustraliaMatch3.PlayedAgainst, AustraliaMatch3);
            AustraliaMatches.Add(AustraliaMatch4.PlayedAgainst, AustraliaMatch4);

            Team AustraliaTeam = new Team()
            {
                Country = Participator.Australia,
                NumberOfMatchesPlayed = 4,
                TotalPoints = 3,
                NumberOfWonMatches = 1,
                NumberOfTiedMatches = 1,
                NumberOfLostMatches = 2,
                Matches = AustraliaMatches
            };

            Match SrilankaMatch1 = new Match()
            {
                PlayedAgainst = Participator.Australia,
                result = Result.Won
            };

            Match SrilankaMatch2 = new Match()
            {
                PlayedAgainst = Participator.England,
                result = Result.Tied
            };

            Match SrilankaMatch3 = new Match()
            {
                PlayedAgainst = Participator.NewZealand,
                result = Result.Tied
            };

            Match SrilankaMatch4 = new Match()
            {
                PlayedAgainst = Participator.Pakistan,
                result = Result.Lost
            };

            Dictionary<Participator, Match> SrilankaMatches = new Dictionary<Participator, Match>();
            SrilankaMatches.Add(SrilankaMatch1.PlayedAgainst, SrilankaMatch1);
            SrilankaMatches.Add(SrilankaMatch2.PlayedAgainst, SrilankaMatch2);
            SrilankaMatches.Add(SrilankaMatch3.PlayedAgainst, SrilankaMatch3);
            SrilankaMatches.Add(SrilankaMatch4.PlayedAgainst, SrilankaMatch4);

            Team SrilankaTeam = new Team()
            {
                Country = Participator.SriLanka,
                NumberOfMatchesPlayed = 4,
                TotalPoints = 4,
                NumberOfLostMatches = 1,
                NumberOfWonMatches = 1,
                NumberOfTiedMatches = 2,
                Matches = SrilankaMatches
            };


            Match NewZealandMatch1 = new Match()
            {
                PlayedAgainst = Participator.Australia,
                result = Result.Won
            };

            Match NewZealandMatch2 = new Match()
            {
                PlayedAgainst = Participator.Pakistan,
                result = Result.Won
            };

            Match NewZealandMatch3 = new Match()
            {
                PlayedAgainst = Participator.England,
                result = Result.Lost
            };

            Match NewZealandMatch4 = new Match()
            {
                PlayedAgainst = Participator.SriLanka,
                result = Result.Tied
            };

            Dictionary<Participator, Match> NewZealandMatches = new Dictionary<Participator, Match>();
            NewZealandMatches.Add(NewZealandMatch1.PlayedAgainst, NewZealandMatch1);
            NewZealandMatches.Add(NewZealandMatch2.PlayedAgainst, NewZealandMatch2);
            NewZealandMatches.Add(NewZealandMatch3.PlayedAgainst, NewZealandMatch3);
            NewZealandMatches.Add(NewZealandMatch4.PlayedAgainst, NewZealandMatch4);

            Team NewZealandTeam = new Team()
            {
                Country = Participator.NewZealand,
                NumberOfMatchesPlayed = 4,
                TotalPoints = 5,
                NumberOfTiedMatches = 1,
                NumberOfLostMatches = 1,
                NumberOfWonMatches = 2,
                Matches = NewZealandMatches
            };

            Dictionary<Participator, Team> teamMatchesDetails = new Dictionary<Participator, Team>();
            teamMatchesDetails.Add(AustraliaTeam.Country, AustraliaTeam);
            teamMatchesDetails.Add(EnglandTeam.Country, EnglandTeam);
            teamMatchesDetails.Add(PakistanTeam.Country, PakistanTeam);
            teamMatchesDetails.Add(NewZealandTeam.Country, NewZealandTeam);
            teamMatchesDetails.Add(SrilankaTeam.Country, SrilankaTeam);

            MatchResultHandler matchResultHandler = new MatchResultHandler();
            matchResultHandler.SortTeamsStandingByPoints(teamMatchesDetails);

            List<Team> teamsPositions = teamMatchesDetails.Values.ToList();

            Assert.AreEqual("NewZealand", teamsPositions.First().Country.ToString(), teamsPositions.First().Country + " At First Position");
            Assert.AreEqual("Pakistan", teamsPositions.ElementAt(1).Country.ToString(), teamsPositions.ElementAt(1).Country + "At Second Position");

        }

        [TestMethod]
        public void Check_If_Team_Played_AllMatches()
        {
            Match NewZealandMatch1 = new Match()
            {
                PlayedAgainst = Participator.Australia,
                result = Result.Won
            };

            Match NewZealandMatch2 = new Match()
            {
                PlayedAgainst = Participator.Pakistan,
                result = Result.Won
            };

            Match NewZealandMatch3 = new Match()
            {
                PlayedAgainst = Participator.England,
                result = Result.Lost
            };

            Match NewZealandMatch4 = new Match()
            {
                PlayedAgainst = Participator.SriLanka,
                result = Result.Tied
            };

            Dictionary<Participator, Match> NewZealandMatches = new Dictionary<Participator, Match>();
            NewZealandMatches.Add(NewZealandMatch1.PlayedAgainst, NewZealandMatch1);
            NewZealandMatches.Add(NewZealandMatch2.PlayedAgainst, NewZealandMatch2);
            NewZealandMatches.Add(NewZealandMatch3.PlayedAgainst, NewZealandMatch3);
            NewZealandMatches.Add(NewZealandMatch4.PlayedAgainst, NewZealandMatch4);

            Team NewZealandTeam = new Team()
            {
                Country = Participator.NewZealand,
                NumberOfMatchesPlayed = 4,
                TotalPoints = 5,
                NumberOfTiedMatches = 1,
                NumberOfLostMatches = 1,
                NumberOfWonMatches = 2,
                Matches = NewZealandMatches
            };

            Match matchAustralia1 = new Match()
            {
                PlayedAgainst = Participator.SriLanka,
                result = Result.Won
            };

            Match matchAustralia2 = new Match()
            {
                PlayedAgainst = Participator.NewZealand,
                result = Result.Lost
            };

            Dictionary<Participator, Match> australiaMatches = new Dictionary<Participator, Match>();
            australiaMatches.Add(matchAustralia1.PlayedAgainst, matchAustralia1);
            australiaMatches.Add(matchAustralia2.PlayedAgainst, matchAustralia2);

            Team AustralianTeam = new Team()
            {
                Country = Participator.Australia,
                NumberOfMatchesPlayed = 2,
                TotalPoints = 2,
                NumberOfLostMatches = 1,
                NumberOfWonMatches = 1,
                Matches = australiaMatches
            };

            Match PakistanMatch = new Match()
            {
                PlayedAgainst = Participator.NewZealand,
                result = Result.Lost
            };

            Dictionary<Participator, Match> pakistanMatches = new Dictionary<Participator, Match>();
            pakistanMatches.Add(PakistanMatch.PlayedAgainst, PakistanMatch);

            Team PakistanTeam = new Team()
            {
                Country = Participator.Pakistan,
                NumberOfMatchesPlayed = 1,
                TotalPoints = 0,
                NumberOfLostMatches = 1,
                Matches = pakistanMatches
            };


            Match EnglandMatch = new Match()
            {
                PlayedAgainst = Participator.NewZealand,
                result = Result.Won
            };

            Dictionary<Participator, Match> englandMatches = new Dictionary<Participator, Match>();
            englandMatches.Add(EnglandMatch.PlayedAgainst, EnglandMatch);

            Team EnglandTeam = new Team()
            {
                Country = Participator.England,
                NumberOfMatchesPlayed = 1,
                TotalPoints = 2,
                NumberOfWonMatches = 1,
                Matches = englandMatches
            };


            Match SriLankaMatch1 = new Match()
            {
                PlayedAgainst = Participator.NewZealand,
                result = Result.Tied
            };

            Match SriLankaMatch2 = new Match()
            {
                PlayedAgainst = Participator.Australia,
                result = Result.Lost
            };

            Dictionary<Participator, Match> srilankaMatches = new Dictionary<Participator, Match>();
            srilankaMatches.Add(SriLankaMatch1.PlayedAgainst, SriLankaMatch1);
            srilankaMatches.Add(SriLankaMatch2.PlayedAgainst, SriLankaMatch2);

            Team SriLankanTeam = new Team()
            {
                Country = Participator.SriLanka,
                NumberOfMatchesPlayed = 2,
                TotalPoints = 1,
                NumberOfLostMatches = 1,
                NumberOfTiedMatches = 1,
                Matches = srilankaMatches
            };

            Dictionary<Participator, Team> teamMatchesDetails = new Dictionary<Participator, Team>();
            teamMatchesDetails.Add(AustralianTeam.Country, AustralianTeam);
            teamMatchesDetails.Add(EnglandTeam.Country, EnglandTeam);
            teamMatchesDetails.Add(PakistanTeam.Country, PakistanTeam);
            teamMatchesDetails.Add(NewZealandTeam.Country, NewZealandTeam);
            teamMatchesDetails.Add(SriLankanTeam.Country, SriLankanTeam);

            MatchResultHandler matchResultHandler = new MatchResultHandler();
            matchResultHandler.CheckAllMatchesPlayed(teamMatchesDetails);

            Assert.AreEqual(true, teamMatchesDetails.ElementAt(3).Value.AllMatchesPlayed);
            Assert.AreEqual(false, teamMatchesDetails.ElementAt(0).Value.AllMatchesPlayed);

        }

        [TestMethod]
        public void ResultMonitoring_FewMatchesPlayed_Test()
        {
            MatchResultHandler provider = new MatchResultHandler();
            ResultMonitor skySportsObserver = new ResultMonitor("SKY SPORTS CHANNEL");
            ResultMonitor espnCricInfoObserver = new ResultMonitor("ESPN CRIC INFO CHANNEL");

            provider.AddTeam(Participator.Australia, Participator.SriLanka, Result.Won);
            skySportsObserver.Subscribe(provider);
            provider.AddTeam(Participator.England, Participator.Australia, Result.Tied);
            espnCricInfoObserver.Subscribe(provider);

            provider.AddTeam(Participator.England, Participator.SriLanka, Result.Tied);
            espnCricInfoObserver.Unsubscribe();

            provider.AddTeam(Participator.Pakistan, Participator.Australia, Result.Lost);
            skySportsObserver.Unsubscribe();

        }
    }
}
