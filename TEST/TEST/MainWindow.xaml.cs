using Microsoft.Win32;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TEST;


namespace Personalproject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TeamData db = new TeamData();
        List<string> continents = new List<string>() { "All","Africa","Asia","Central America","Europe","North America","Oceania","South America" };
        List<string> selectedteam = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
            var querry = from c in db.Teams
                         where c.ID > 1 && c.ID < 34
                         orderby c.Name
                         select c;
            lbx_teams.ItemsSource = querry.ToList().Distinct();
           
            cbx_continent.ItemsSource = continents;
        }

        private void cbx_continent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var astring = cbx_continent.SelectedItem;
            if (astring.ToString() != "All")
            {
                var q = from c in db.Teams
                        where c.Address == astring.ToString() && c.ID > 1 && c.ID < 34
                        orderby c.Name
                        select c;
                lbx_teams.ItemsSource = q.ToList();
            }
            else
            {
                var querry = from c in db.Teams
                             where c.ID > 1 && c.ID < 34
                             orderby c.Name
                             select c;
                lbx_teams.ItemsSource = querry.ToList().Distinct();
            }
        }

        private void lbx_teams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var astring = sender as ListBox;
            var a = (string)astring.SelectedValue;
            var q = from c in db.Players
                    where c.nationality == a && c.PlayerID < 812
                    orderby c.Name
                    select c;
            lbx_players.ItemsSource = q.ToList().Distinct();
            
        }

        private void btn_choice_Click(object sender, RoutedEventArgs e)
        {
            if(selectedteam.Count < 2)
            {
                var astring = lbx_teams.SelectedItem;
                if (astring != null && selectedteam.Count > -1)
                {
                    Team a = astring as Team;
                    selectedteam.Add(a.Name);
                }
                lbx_results.ItemsSource = selectedteam;
                lbx_results.Items.Refresh();
            }
            else 
            {
                MessageBox.Show("Remove a team first");
            }
        }

        private void btn_remove_Click(object sender, RoutedEventArgs e)
        {
            var astring = lbx_results.SelectedItem;
            if (astring != null )
            {
                string a = astring as string;
                selectedteam.Remove(a);
            }
            else
            {
                MessageBox.Show("Choose a team to remove");
            }
            lbx_results.ItemsSource = selectedteam;
            lbx_results.Items.Refresh();
        }

        private void btn_compare_Click(object sender, RoutedEventArgs e)
        {
            if(selectedteam.Count < 2)
            {
                MessageBox.Show("Add teams to compare");
            }
            else
            {
                var astring = lbx_teams.SelectedItem;
                Team tmp = astring as Team;
                
                Enemies enemy = new Enemies() { Name = selectedteam[0].ToLower() };
                int i = 0;
                bool played  = false;
                
                while (i < tmp.Enemy.Count && !played)
                {
                    if (tmp.Enemy[i].Name.ToLower() == enemy.Name || tmp.Enemy[i].Name.ToLower() == "maroccco")
                    {
                        played = true;
                    }
                    i++;
                }
                
                if (played)
                {
                    Results results = new Results();
                    results.t1.Text = selectedteam[0];
                    results.t2.Text = selectedteam[1];
                    var t0 = selectedteam[0];
                    var t1 = selectedteam[1];
                    var query = from c in db.Matchs
                                where t0 == c.Team1 && t1 == c.Team2 || t1 == c.Team1 && t0 == c.Team2
                                select c.Score_at_end;
                    var query1 = from c in db.Matchs
                                where t0 == c.Team1 && t1 == c.Team2 || t1 == c.Team1 && t0 == c.Team2
                                select c.Penalty;
                    var query2 = from c in db.Matchs
                                 where t0 == c.Team1 && t1 == c.Team2 || t1 == c.Team1 && t0 == c.Team2 
                                 select c.Times;
                    var query3 = from c in db.Matchs
                                 where t0 == c.Team1 && t1 == c.Team2 || t1 == c.Team1 && t0 == c.Team2
                                 select c.Times;
                    var query4 = from c in db.Matchs
                                where t0 == c.Team1 && t1 == c.Team2 || t1 == c.Team1 && t0 == c.Team2
                                select c.Date;
                    var query5 = from c in db.Matchs
                                 select c.Winner;
                    results.date.Text = query4.ToList().First();
                    results.score.Text = query.ToList().First();
                    int a = int.Parse(results.score.Text[0].ToString());
                    int b = int.Parse(results.score.Text[2].ToString());
                    if ((a < b && query5.ToList().First() != results.t1.Text) || (a > b && query5.ToList().First() != results.t1.Text))
                    {
                        results.score.Text = results.score.Text[2].ToString() + results.score.Text[1].ToString() + results.score.Text[0].ToString();
                    }
                    results.penalty.Text = query1.ToList().First();
                    List<string> scores0 = new List<string>();
                    List<string> scores1 = new List<string>();
                    var s0 = query2.ToList().Last();
                    var s1 = query3.ToList().Last();
                    foreach (var ti in s0)
                    {
                        if(ti.team == selectedteam[0])
                        {
                            scores0.Add(ti.Player +" "+ ti.Time);
                        }
                    }
                    foreach (var ti in s1)
                    {
                        if (ti.team == selectedteam[1])
                        {
                            scores1.Add(ti.Player +" "+ ti.Time);
                        }
                    }
                    results.goals1.ItemsSource = scores0;
                    results.goals2.ItemsSource = scores1;
                    results.Show();
                }
                
                else
                {
                    var astring1 = lbx_teams.SelectedItem;
                    Team tmp1 = astring as Team;
                    List<string> test = new List<string>();
                    foreach (var ti in tmp1.Enemy)
                    {
                        test.Add(ti.Name);
                    }
                    lbx_results.ItemsSource = test;
                    //MessageBox.Show(selectedteam[0]);
                }
                
                
            }
        }

        private void btn_score_Click(object sender, RoutedEventArgs e)
        {
            
            Score scorer = new Score();
            var querry = from c in db.Plays
                         where c.ID > 20
                         orderby c.Goals descending
                         select c.Name;
            
            var t1 = querry.ToList();
            var q = from c in db.Plays
                    where c.ID > 20
                    orderby c.Goals descending
                    select c.Goals;
            var t2 = q.ToList();
            var q2 = from c in db.Plays
                     where c.ID > 20
                     orderby c.Goals descending
                     select c.PlayedMatches;
            var t3 = q2.ToList();
            List<string> test = new List<string>();
            for (int i = 0;i < t1.Count;i++)
            {
                test.Add(t1[i]+"   Matches played :  "  + t3[i] + "   Goals :  " + t2[i]);
            }
            scorer.lst_top.ItemsSource = test.Distinct();
            scorer.Show();
        }
    }
}
