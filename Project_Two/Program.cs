using System;
using System.Collections.Generic;
using System.IO;

namespace Project2_BrennaHull
{
    class Program
    {
        static void Main(string[] args)
        {
            const string path = @"C:\Users\hulbrea\Documents\Project2_BrennaHull\Super_Bowl_Project.csv";
            string line;
            string[] data;
            List<SuperBowl_Data> SBData_List = null;
            if (File.Exists(path))
            {
                FileStream input = new FileStream(path, FileMode.Open, FileAccess.Read);
                StreamReader read = new StreamReader(input);
                line = read.ReadLine();
                SBData_List = new List<SuperBowl_Data>();

                while (!read.EndOfStream)
                {
                    data = read.ReadLine().Split(',');
                    SBData_List.Add(new SuperBowl_Data(data[0], data[1], Convert.ToInt32(data[2]), data[3], data[4], data[5], Convert.ToInt32(data[6]), data[7], data[8], data[9], Convert.ToInt32(data[10]), data[11], data[12], data[13], data[14]));
                }
                read.Dispose();
                input.Dispose();

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\hulbrea\Documents\SuperBowlProject.txt"))
                {
                    
                    file.WriteLine("==================================================WINNERS==================================================");
                    foreach (SuperBowl_Data x in SBData_List)
                    {
                        int pointDifference = x.WinningPoints - x.LosingPoints;
                        file.WriteLine($"Year: {x.Date} Team: {x.WinningTeam} Quarterback: {x.QBWinning} Coach: {x.CoachWinning} MVP: {x.MVP} Point Difference: {pointDifference}\n");
                    }
                    file.WriteLine("");

                    Console.WriteLine("==================================================Top 5 Attended Super Bowls==================================================");
                    foreach (SuperBowl_Data x in SBData_List)
                    {

                    }
                    file.WriteLine("");

                    Console.WriteLine("==================================================State with the Most Super Bowls Hosted==================================================");
                    file.WriteLine("");

                    Console.WriteLine("==================================================Players with Multiple MVPs==================================================");
                    file.WriteLine("");

                    Console.WriteLine("==================================================QUESTIONS==================================================");
                    file.WriteLine("Which coach lost the most super bowls?");
                    file.WriteLine("Which coach won the most super bowls?");
                    file.WriteLine("Which team(s) won the most super bowls?");
                    file.WriteLine("Which team(s) lost the most super bowls?");
                    file.WriteLine("Which super bowl had the greatest point difference?");
                    file.WriteLine("What is the average attendance of all super bowls?");








                    file.WriteLine("Top 5 Attended Super Bowls");
                    file.WriteLine("Year: Winning Team: Losing Team: City: State: Stadium: ");
                    file.WriteLine("State with the Most Super Bowls Hosted");
                    file.WriteLine("City: State: Stadium: ");
                    file.WriteLine("Players with Multiple MVPs");
                    file.WriteLine("Name: Winning Team: Losing Team:");
                    file.WriteLine("");

                    
                }
            }

            

        }

    }

    //Creating a class to define each year's super bowl data
    class SuperBowl_Data
    {
        public string Date { get; set; }
        public string SBNumber { get; set; }
        public int Attendance { get; set; }
        public string QBWinning { get; set; }
        public string CoachWinning { get; set; }
        public string WinningTeam { get; set; }
        public int WinningPoints { get; set; }
        public string QBLosing { get; set; }
        public string CoachLosing { get; set; }
        public string LosingTeam { get; set; }
        public int LosingPoints { get; set; }
        public string MVP { get; set; }
        public string Stadium { get; set; }
        public string City { get; set; }
        public string State { get; set; }

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
