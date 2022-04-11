using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace FightRight.Models
{

    /// <summary>
    /// Static class for handling database connections.
    /// <todo>Remove connection strings that connect to local database and change to app connection string</todo>
    /// </summary>
    public static class DBHandler
	{
        
        /// <summary>
        /// enum for reference to the positions of each value in the fighter stats list
        /// </summary>
        public enum STAT_LIST_INDICES
		{
            Knockdowns,
            Significant_Strike_Percentage,
            Significant_Strikes,
            Significant_Strikes_Attempted,
            Submission_Attempts,
            Takedown_Percentage,
            Takedowns,
            Takedowns_Attempted,
            Total_strike_Percentage,
            Total_Strikes,
            Total_Strikes_Attempted
        };

        /// <summary>
        /// enum for easy reference to the positions of each value in the fighter profiles list
        /// </summary>
        public enum PROFILE_LIST_INDICES
		{
            nickname,
            birth_city,
            birth_state,
            birth_country,
            birth_country_code,
            birth_date,
            wins,
            draws,
            losses,
            no_contests,
            reach,
            height,
            weight

        };


        /// <summary>
        /// Persistant fighter info
        /// </summary>
        public static FighterChart fighterInfo = new FighterChart();


        /// <summary>
        /// Helper for creating an sql statement for when searching for fighter ID's within a range
        /// </summary>
        /// <param name="startIndex">The index for where to start in the Fighter_ID range</param>
        /// <param name="endIndex">The index for where to end in the Fighter_ID range</param>
        /// <param name="isInclusive">Optional parameter for if the range includes the beginning and ending id's</param>
        /// <returns>The string for creating a range sql select</returns>
        public static string CreateRangedFighterSelect(int startIndex, int endIndex, bool isInclusive = true)
		{

            string selectStatement = (isInclusive == true) ? "WHERE Fighter_ID >= " + startIndex.ToString() + " AND Fighter_ID <= " + endIndex.ToString() : "WHERE Fighter_ID > " + startIndex.ToString() + " AND Fighter_ID < " + endIndex.ToString();

            return selectStatement;
        }




        /// <summary>
        /// Gets the id number and name from tfighters and puts into a dictionary
        /// </summary>
        /// <param name="additionalArgs">Optional argument for any additional parameters for the select statement</param>
        /// <returns>A dictionary of fighter id's as the keys and the fighters names as values</returns>
        public static Dictionary<int, string> GetFighters(string additionalArgs="")
		{
            //Variables
            Dictionary<int, string> dict = new Dictionary<int, string>(); //The dictionary to return
            Database db = new Database(); //Variable for the database connection
            string connectionString = ConfigurationManager.AppSettings["AppDBConnect"]; //The string to connect to the db with
            //string connectionString = @"Data Source=(local);Initial Catalog=dbFightRight;Integrated Security=True;"; //The string to connect to the db with

            //try this...
            try
            {
                //Get the fighters by executing a select statement and any additional args
                var fighters = db.ExecSql("SELECT * FROM TFighters " + additionalArgs, connectionString);

                //Loop through each row in the fighters data row
                foreach(var row in fighters)
				{
                    //If the return dictionary does not contain the fighter ID as a key...
                    if (dict.ContainsKey((int)row["Fighter_ID"]) == false)
                    {
                        //Add to the dictionary
                        dict.Add((int)row["Fighter_ID"], (string)row["Fighter_Name"]);
                    }
                }
			}
            catch (Exception ex)
			{
                //Console.WriteLine(ex.Message);
			}

            //Return the dictionary
            return dict;

        }


        /// <summary>
        /// Gets the id number and name from tfighters and puts into a dictionary
        /// </summary>
        /// <param name="fightersByName">For another dictionary where the fighters names are the keys</param>
        /// <param name="additionalArgs">Optional argument for any additional parameters for the select statement</param>
        /// <returns>A dictionary of fighter id's as the keys and the fighters names as values</returns>
        public static Dictionary<int, string> GetFighters(out Dictionary<string,int> fightersByName, string additionalArgs="")
		{
            //Variables
            Dictionary<string, int> dictByName = new Dictionary<string, int>(); //The dictionary to set the out arg to
            Dictionary<int, string> dict = new Dictionary<int, string>(); //The dictionary to return
            Database db = new Database(); //Variable for the database connection
            string connectionString = ConfigurationManager.AppSettings["AppDBConnect"]; //The string to connect to the db with
            //string connectionString = @"Data Source=(local);Initial Catalog=dbFightRight;Integrated Security=True;"; //The string to connect to the db with

            //try this...
            try
            {
                //Get the fighters by executing a select statement and any additional args
                var fighters = db.ExecSql("SELECT * FROM TFighters " + additionalArgs, connectionString);

                //Loop through each row in the fighters data row
                foreach (var row in fighters)
                {
                    //If the return dictionary does not contain the fighter ID as a key...
                    if (dict.ContainsKey((int)row["Fighter_ID"]) == false)
                    {
                        //Add to the dictionary
                        dict.Add((int)row["Fighter_ID"], (string)row["Fighter_Name"]);
                    }

                    //If the name dictionary does not contain the fighters name as a key...
                    if (dictByName.ContainsKey((string)row["Fighter_Name"]) == false)
                    {
                        //Add to the dictionary
                        dictByName.Add((string)row["Fighter_Name"],(int)row["Fighter_ID"]);
                    }
				}

			}
            catch (Exception ex)
			{
                //Console.WriteLine(ex.Message);
			}

            //Set the out arg to the dictionary we created
            fightersByName = dictByName;

            //Return the dictionary
            return dict;

        }




        /// <summary>
        /// Gets the id number and name from tfighters and puts into a dictionary along with a dictionary ordered by name, a dictionary of the stats, and a dictionary of the profiles
        /// </summary>
        /// <param name="fightersByName">For another dictionary where the fighters names are the keys</param>
        /// <param name="fighterStats">A dictionary with the ID's as keys and a data row of the stats as values</param>
        /// <param name="fighterProfiles">A dictionary with the ID's as keys and a data row of the profiles as values</param>
        /// <param name="additionalArgs">Optional argument for any additional parameters for the select statement</param>
        /// <returns>A dictionary of fighter id's as the keys and the fighters names as values</returns>
        public static Dictionary<int, string> GetFighters(ref Dictionary<string,int> fightersByName,
            ref Dictionary<int, DataRow> fighterStats,
            ref Dictionary<int, DataRow> fighterProfiles,
            string additionalArgs="")
		{
            //Variables
            Dictionary<int, string> dict = new Dictionary<int, string>(); //The dictionary to return
            Database db = new Database(); //Variable for the database connection
            string connectionString = ConfigurationManager.AppSettings["AppDBConnect"]; //The string to connect to the db with
            //string connectionString = @"Data Source=(local);Initial Catalog=dbFightRight;Integrated Security=True;"; //The string to connect to the db with

            //try this...
            try
            {
                //Get the fighters by executing a select statement and any additional args
                var fighters = db.ExecSql("SELECT * FROM TFighters " + additionalArgs, connectionString);

                //Loop through each row in the fighters data row
                foreach (var row in fighters)
                {
                    //If the return dictionary does not contain the fighter ID as a key...
                    if (dict.ContainsKey((int)row["Fighter_ID"]) == false)
                    {
                        //Add to the dictionary
                        dict.Add((int)row["Fighter_ID"], (string)row["Fighter_Name"]);
                    }

                    //If the name dictionary does not contain the fighters name as a key...
                    if (fightersByName.ContainsKey((string)row["Fighter_Name"]) == false)
                    {
                        //Add to the dictionary
                        fightersByName.Add((string)row["Fighter_Name"],(int)row["Fighter_ID"]);
                    }

                    //If the return dictionary does not contain the fighter ID as a key...
                    if (fighterStats.ContainsKey((int)row["Fighter_ID"]) == false) {
                        //Get the fighters stats and add to the dictionary
                        var stats = GetFighterStats((int)row["Fighter_ID"]);
                        fighterStats.Add((int)row["Fighter_ID"], stats);

                    }

                    //If the return dictionary does not contain the fighter ID as a key...
                    if (fighterProfiles.ContainsKey((int)row["Fighter_ID"]) == false) {
                        //Get the fighters profile and add to the dictionary
                        var profile = GetFighterProfile((int)row["Fighter_ID"]);
                        fighterProfiles.Add((int)row["Fighter_ID"], profile);
                    }



                }

            }
            catch (Exception ex)
			{
                //Console.WriteLine(ex.Message);
			}

            //Return the dictionary
            return dict;

        }



        /// <summary>
        /// Gets the fighter stats based on the fighter ID passed
        /// </summary>
        /// <returns>A DataRow of containing the fighters stats</returns>
        public static DataRow GetFighterStats(int fighter_id, string additionalArgs = "")
        {
            //Variables
            DataRow fighterRow = null; //The row containing the fighters data
            Database db = new Database(); //Database variable for database connection
            string connectionString = ConfigurationManager.AppSettings["AppDBConnect"]; //The string to connect to the db with
            //string connectionString = @"Data Source=(local);Initial Catalog=dbFightRight;Integrated Security=True;"; //The string to connect to the db with

            try
            {
                var fighters = db.SelectAllFighterStats(connectionString, "WHERE Fighter_ID = " + fighter_id.ToString() + " " + additionalArgs);

                fighterRow = fighters.First();

            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }


            return fighterRow;

        }


        /// <summary>
        /// Gets the fighter profile based on the fighter ID passed
        /// </summary>
        /// <returns>A DataRow of containing the fighters profile</returns>
        public static DataRow GetFighterProfile(int fighter_id, string additionalArgs = "")
        {
            //Variables
            DataRow fighterRow = null; //The row containing the fighters data
            Database db = new Database(); //Database variable for database connection
            string connectionString = ConfigurationManager.AppSettings["AppDBConnect"]; //The string to connect to the db with
            //string connectionString = @"Data Source=(local);Initial Catalog=dbFightRight;Integrated Security=True;"; //The string to connect to the db with

            try
            {
                var fighters = db.SelectAllFighterProfiles(connectionString, "WHERE Fighter_ID = " + fighter_id.ToString() + " " + additionalArgs);

                fighterRow = fighters.First();

            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }


            return fighterRow;

        }



        /// <summary>
        /// Gets the fighter fight prediction based on the 2 id's passed
        /// </summary>
        /// <returns>The result</returns>
        public static DataRow GetFighterPrediction(int fighter1_id, int fighter2_id, string additionalArgs = "")
        {
            //Variables
            DataRow fighterRow = null; //The row containing the fighters data
            Database db = new Database(); //Database variable for database connection
            string connectionString = ConfigurationManager.AppSettings["AppDBConnect"]; //The string to connect to the db with
            //string connectionString = @"Data Source=(local);Initial Catalog=dbFightRight;Integrated Security=True;"; //The string to connect to the db with

            try
            {
                
                var fighters = db.ExecSqlStoredProc(fighter1_id, fighter2_id, connectionString);

                foreach(DataRow row in fighters)
				{
                    System.Diagnostics.Debug.WriteLine(row);

                }

            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }


            return fighterRow;

        }




        

	
    
    
    }
}