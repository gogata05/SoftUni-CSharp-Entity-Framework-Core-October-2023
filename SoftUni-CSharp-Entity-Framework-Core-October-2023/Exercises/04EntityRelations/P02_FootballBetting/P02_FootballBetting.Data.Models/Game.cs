﻿namespace P02_FootballBetting.Data.Models
{
    public class Game
    {
        public Game()
        {
            this.PlayersStatistics = new HashSet<PlayerStatistic>();
            this.Bets = new HashSet<Bet>();
        }
        public int GameId { get; set; }
        public int HomeTeamId { get; set; }
        public Team? HomeTeam { get; set; }
        public int AwayTeamId { get; set; }
        public Team? AwayTeam { get; set; }
        public int HomeTeamGoals { get; set; }
        public int AwayTeamGoals { get; set; }
        public DateTime DateTime { get; set; }
        public double HomeTeamBetRate { get; set; }
        public double AwayTeamBetRate { get; set; }
        public double DrawBetRate { get; set; }
        public string? Result { get; set; }
        public Bet Bet { get; set; } = null!;
        public ICollection<PlayerStatistic> PlayersStatistics { get; set; }
        public ICollection<Bet> Bets { get; set; }
    }
}