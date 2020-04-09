using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Project2_SBStats_BrennaHull
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter your username: ");
            string user_name = Console.ReadLine();
            Console.WriteLine($@"Please have the CSV file available at the path C:\Users\{user_name}\Documents\Super_Bowl_Project.csv");
            Console.WriteLine("");
            string path = $@"C:\Users\{user_name}\Documents\Super_Bowl_Project.csv";
            string line;
            string[] SBData_ReadIn;
            List<SuperBowl_Data> SBData_List = null;

            if (File.Exists(path))
            {
                Console.WriteLine(@$"Great, the file exists! The requested information can be found at: C:\Users\{user_name}\Documents\SuperBowlProject.txt");

                FileStream input = new FileStream(path, FileMode.Open, FileAccess.Read);

                StreamReader read = new StreamReader(input);

                line = read.ReadLine(); //Reads first line in (this is the line with the titles)

                SBData_List = new List<SuperBowl_Data>();

                while (!read.EndOfStream)
                {
                    SBData_ReadIn = read.ReadLine().Split(',');
                    
                    SBData_List.Add(new SuperBowl_Data(SBData_ReadIn[0], SBData_ReadIn[1], Convert.ToInt32(SBData_ReadIn[2]), SBData_ReadIn[3], SBData_ReadIn[4], SBData_ReadIn[5], Convert.ToInt32(SBData_ReadIn[6]), SBData_ReadIn[7], SBData_ReadIn[8], SBData_ReadIn[9], Convert.ToInt32(SBData_ReadIn[10]), SBData_ReadIn[11], SBData_ReadIn[12], SBData_ReadIn[13], SBData_ReadIn[14]));
                
                }
                read.Dispose();
                input.Dispose();

                using (StreamWriter file = new StreamWriter($@"C:\Users\{user_name}\Documents\SuperBowlProject.txt"))
                {

                    file.WriteLine("==================================================WINNERS==================================================\n");
                    
                    foreach (SuperBowl_Data x in SBData_List)
                    {
                        int pointDifference = x.WinningPoints - x.LosingPoints;
                        file.WriteLine($"\tYear: {x.Date} Team: {x.WinningTeam} Quarterback: {x.QBWinning} Coach: {x.CoachWinning} MVP: {x.MVP} Point Difference: {pointDifference}\n");
                    }
                    file.WriteLine("");

                    file.WriteLine("==================================================Top 5 Attended Super Bowls==================================================");

                    IEnumerable<SuperBowl_Data> attendance_Query =
                        (from superbowl_data in SBData_List
                         orderby superbowl_data.Attendance descending
                         select superbowl_data).ToList<SuperBowl_Data>().Take(5);

                    foreach (SuperBowl_Data x in attendance_Query)
                    {
                        file.WriteLine($"\n\tYear: {x.Date} Winning Team: {x.WinningTeam} Losing Team: {x.LosingTeam} City: {x.City} State: {x.State} Stadium: {x.Stadium}");
                    }
                    file.WriteLine("");

                    file.WriteLine("==================================================State with the Most Super Bowls Hosted==================================================");
                    var MostHost_Query =
                        (from superbowl_data in SBData_List
                         group superbowl_data by superbowl_data.State into MostHost_Group
                         orderby MostHost_Group.Count() descending
                         select MostHost_Group).Take(1);

                    foreach (var host in MostHost_Query)
                    {
                        file.WriteLine($"\n{host.Key} hosted {host.Count()} Super Bowls at various stadiums");
                        file.WriteLine("\tCity\n\t  Stadium");

                        foreach (SuperBowl_Data x in host)
                        {
                            file.WriteLine($"\n\t{x.City}\n\t  {x.Stadium}");
                        }
                    }

                    file.WriteLine("");

                    file.WriteLine("==================================================Players with Multiple MVPs==================================================");
                    var MVP_Query =
                        (from superbowl_data in SBData_List
                         group superbowl_data by superbowl_data.MVP into MVP_Group
                         where MVP_Group.Count() > 1
                         orderby MVP_Group.Key
                         select MVP_Group);
                    foreach (var player in MVP_Query)
                    {
                        file.WriteLine($"\nPlayer Name: {player.Key}");
                        file.WriteLine($"Below is the winning and losing team for the {player.Count()} times {player.Key} won MVP");
                        foreach (SuperBowl_Data x in player)
                        {
                            file.WriteLine($"\tWinning Team: {x.WinningTeam} Losing Team: {x.LosingTeam}");
                        }
                    }
                    file.WriteLine("");

                    file.WriteLine("==================================================QUESTIONS==================================================");
                    
                    var CoachLoser_Query =
                        (from superbowl_data in SBData_List
                         group superbowl_data by superbowl_data.CoachLosing into CoachLoser_Group
                         orderby CoachLoser_Group.Count() descending
                         select CoachLoser_Group).Take(1);
                    foreach (var CoachLoser in CoachLoser_Query)
                    {
                        file.WriteLine("Which coach lost the most super bowls?");
                        file.WriteLine($"\tCoach {CoachLoser.Key} lost {CoachLoser.Count()} Super Bowls.");
                    }
                    file.WriteLine("");
                    
                    var CoachWinner_Query =
                        (from superbowl_data in SBData_List
                         group superbowl_data by superbowl_data.CoachWinning into CoachWinner_Group
                         orderby CoachWinner_Group.Count() descending
                         select CoachWinner_Group).Take(1);
                    foreach (var CoachWinner in CoachWinner_Query)
                    {
                        file.WriteLine("Which coach won the most super bowls?");
                        file.WriteLine($"\tCoach {CoachWinner.Key} won {CoachWinner.Count()} Super Bowls.");
                    }
                    file.WriteLine("");

                    var TeamWinner_Query =
                       (from superbowl_data in SBData_List
                        group superbowl_data by superbowl_data.WinningTeam into TeamWinner_Group
                        orderby TeamWinner_Group.Count() descending
                        select TeamWinner_Group).Take(1);
                    foreach (var TeamWinner in TeamWinner_Query)
                    {
                        file.WriteLine("Which team(s) won the most super bowls?");
                        file.WriteLine($"\t{TeamWinner.Key} won {TeamWinner.Count()} Super Bowls."); //How do I change this to include teams that won the same number of times??
                    }
                    file.WriteLine("");

                    var TeamLoser_Query =
                       (from superbowl_data in SBData_List
                        group superbowl_data by superbowl_data.LosingTeam into TeamLoser_Group
                        orderby TeamLoser_Group.Count() descending
                        select TeamLoser_Group).Take(1);
                    foreach (var TeamLoser in TeamLoser_Query)
                    {
                        file.WriteLine("Which team(s) lost the most super bowls?");
                        file.WriteLine($"\t{TeamLoser.Key} lost {TeamLoser.Count()} Super Bowls.");
                    }
                    file.WriteLine("");

                    file.WriteLine("Which super bowl had the greatest point difference?");
                    file.WriteLine("");

                    file.WriteLine("What is the average attendance of all super bowls?");

                    file.WriteLine("");


                }
            }
            else
            {
                Console.WriteLine("Sorry the file could not be found. Please make sure your document path matches the output above.");
            }

        }


    }

    //Creating a class to define each year's super bowl data
    public class SuperBowl_Data
    {
        public string Date;
        public string SBNumber;
        public int Attendance;
        public string QBWinning;
        public string CoachWinning;
        public string WinningTeam;
        public int WinningPoints;
        public string QBLosing;
        public string CoachLosing;
        public string LosingTeam;
        public int LosingPoints;
        public string MVP;
        public string Stadium;
        public string City;
        public string State;

        public SuperBowl_Data(string date, string sbNum, int attendance, string qbWinning, string coachWinning,
            string winningTeam, int winningPoints, string qbLosing, string coachLosing, string losingTeam,
            int losingPoints, string mvp, string stadium, string city, string state)
        {
            Date = date;
            SBNumber = sbNum;
            Attendance = attendance;
            QBWinning = qbWinning;
            CoachWinning = coachWinning;
            WinningTeam = winningTeam;
            WinningPoints = winningPoints;
            QBLosing = qbLosing;
            CoachLosing = coachLosing;
            LosingTeam = losingTeam;
            LosingPoints = losingPoints;
            MVP = mvp;
            Stadium = stadium;
            City = city;
            State = state;
        }
    }
}