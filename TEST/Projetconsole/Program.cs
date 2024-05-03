using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Globalization;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using TEST;
using System.Diagnostics;
using System.IO.Ports;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Runtime.Remoting.Contexts;



namespace Projetconsole
{
    public class Program
    {
        public static async Task Main()
        {
            using (var db = new TeamData("ProjectTeam"))
            {
                #region Team
                /*
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://api.football-data.org/v4/competitions/WC/scorers")
                };
                request.Headers.Add("X-Auth-Token", "c560d87c22354afbb90c68e668eb2462");

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                string body = await response.Content.ReadAsStringAsync();
                string files = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                using (StreamWriter outfile = new StreamWriter(Path.Combine(files, "ScorerData")))
                {
                    foreach (var l in body)
                        outfile.Write(l);
                }
                */
                /*var tarray = teams["teams"].ToObject<Team[]>();
                string file = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                using (StreamWriter outfile = new StreamWriter(Path.Combine(file, "DataTeam")))
                {
                    foreach (var l in body)
                        outfile.Write(l);
                }*/
                //var teams = JsonConvert.DeserializeObject<JObject>("C:/Users/JesyCrouvisier-STUDE/OneDrive - Atlantic TU/Documents/DataTeam.json");
                string js2;
                using (StreamReader file = File.OpenText("C:/Users/JesyCrouvisier-STUDE/source/repos/Finalproject_OOD/TEST/ScorerData"))
                {
                    js2 = file.ReadToEnd();

                }
                var plays = JsonConvert.DeserializeObject<JObject>(js2);
                var parray = plays["scorers"].ToObject<Scorer[]>();
                foreach (var p in parray)
                {
                    db.Plays.Add(new Play() { ID = p.player.ID ,Name = p.player.Name, Nationality = p.player.Nationality,Section = p.player.Section , Goals = p.Goals, PlayedMatches = p.PlayedMatches});
                }
                
                string js1;
                using (StreamReader file = File.OpenText("C:/Users/JesyCrouvisier-STUDE/source/repos/Finalproject_OOD/TEST/StatsTeam1"))
                {
                    js1 = file.ReadToEnd();

                }
                var matche = JsonConvert.DeserializeObject<JObject>(js1);
                var marray = matche["matches"].ToObject<Match[]>();
                foreach (Match match in marray)
                {
                    db.Matchs.Add(match);
                }
                //
                
                string js;
                using (StreamReader file = File.OpenText("C:/Users/JesyCrouvisier-STUDE/source/repos/Finalproject_OOD/TEST/DataTeam - Copy"))
                {
                    js = file.ReadToEnd();

                }
                var teams = JsonConvert.DeserializeObject<JObject>(js);
                var tarray = teams["teams"].ToObject<Team[]>();
                foreach (var team in tarray)
                    db.Teams.Add(team);
               
                
                for (int i = 0; i < tarray.Length; i++)
                {
                    for (int j = 0; j < tarray[i].Squad.Count; j++)
                    {
                        db.Players.Add(tarray[i].Squad[j]);
                    }
                }
               db.SaveChanges();
                
                #endregion

            }
        }
    }
}
