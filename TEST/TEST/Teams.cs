using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TEST
{

    public class Player
    {
        public int PlayerID { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Image { get; set; }
        public string nationality { get; set; }

        public virtual Team Team { get; set; }
    }
    public class Enemies
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class Tim
    {
        public int ID {get;set; }
        public string team { get; set; }
        public string Time { get; set; }
        public string Player { get; set; }
    }
    public class Match
    {
        public int ID { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public string Date { get; set; }
        public string Score_at_end { get; set; }
        public string score_halfway { get; set; }
        public string Penalty { get; set; }
        public List<Tim> Times { get; set; }
        public string Winner { get; set; }
        public string inner { get; set; }
        public Match()
        {
            Times = new List<Tim>();
        }
    }
    public class Play
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public string Section { get; set; }
        public string PlayedMatches { get; set; }
        public string Goals { get; set; }
    }
    public partial class Scorer
    {
        public int ID { get; set; }
        public Play player { get; set; }
        public string PlayedMatches { get; set; }
        public string Goals { get; set; }
    }
    public class Team
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string TLA { get; set; }
        public string Crest { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string Founded { get; set; }
        public string ClubColors { get; set; }
        public string Venues { get; set; }
        public string LastUpdated { get; set; }

        public virtual List<Player> Squad { get; set; }
        public virtual List<Enemies> Enemy { get; set; }

        public Team()
        {
            Squad = new List<Player>();
            Enemy = new List<Enemies>();
        }
    }
    public class TeamData : DbContext
    {
        
        public TeamData(string database) : base(database) { }
        public TeamData() : this("ProjectTeam") { }

        public DbSet<Player> Players { get; set; }
        public DbSet<Play> Plays { get; set; }
        public DbSet<Match> Matchs { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
    
}
