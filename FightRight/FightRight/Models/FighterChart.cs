#define DBUG_1


using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Mvc;
using System.Linq;
//using System.Runtime.Serialization;


namespace FightRight.Models
{

#if DBUG_1

	////DataContract for Serializing Data - required to serve in JSON format
	//[DataContract]

	/// <summary>
	/// Class for fighter values
	/// </summary>
	public class FighterChart
	{

		public int maxItems; //The maximum amount of possible items

		public int rangeIndexA; //The index for the current range of fighters chosen
		public int itemCountA; //The amount of values to show in the range, also is the amount added to the index for each range
		public int currentSelectionA; //The currently selected fighter(side A)
		public Dictionary<int, string> fightersA; //Fighters(A for comparisons)
		public Dictionary<string, int> fightersByNameA; //Fighters by name(A for comparisons)
		public List<DataChart> dcFighterA_stats = new List<DataChart>(); //Fighter comparison A list of datachart type that contains a label and value for an item
		public List<DataChart> dcFighterA_profile = new List<DataChart>(); //Fighter comparison A list of datachart type that contains a label and value for an item
		public string currentFighterNameA { get; set; } //The name of the current fighter(for comparison fighter A)
		
		public string currentFighterNameB { get; set; } //The name of the current fighter(for comparison fighter B)
		public int rangeIndexB; //The index for the current range of fighters chosen
		public int itemCountB; //The amount of values to show in the range, also is the amount added to the index for each range
		public int currentSelectionB; //The currently selected fighter(side B)
		public Dictionary<int, string> fightersB; //Fighters(B for comparisons)
		public Dictionary<string, int> fightersByNameB; //Fighters by name(B for comparisons)
		public List<DataChart> dcFighterB_stats = new List<DataChart>(); //Fighter comparison B list of datachart type that contains a label and value for an item
		public List<DataChart> dcFighterB_profile = new List<DataChart>(); //Fighter comparison B list of datachart type that contains a label and value for an item

		public DataRow prediction = null;


		/// <summary>
		/// Initializer for the class
		/// </summary>
		public FighterChart()
		{
			rangeIndexA = 0;
			rangeIndexB = 0;
			itemCountA = 0;
			itemCountB = 0;
			maxItems = 0;
			currentSelectionA = 0;
			currentSelectionB = 0;

			fightersA = new Dictionary<int, string>();
			fightersByNameA = new Dictionary<string, int>();
			fightersB = new Dictionary<int, string>();
			fightersByNameB = new Dictionary<string, int>();

			currentFighterNameA = "";
			currentFighterNameB = "";

			
			fightersA = DBHandler.GetFighters(out fightersByNameA);
			fightersB = DBHandler.GetFighters(out fightersByNameB);

		}


		/// <summary>
		/// Initializer for the class
		/// </summary>
		public FighterChart(int minIndex, int indexOffset, int maxItemCount = 1000)
		{
			rangeIndexA = minIndex;
			rangeIndexB = minIndex;
			itemCountA = indexOffset;
			itemCountB = indexOffset;
			maxItems = maxItemCount;
			currentSelectionA = 0;
			currentSelectionB = 0;

			fightersA = new Dictionary<int, string>();
			fightersByNameA = new Dictionary<string, int>();
			fightersB = new Dictionary<int, string>();
			fightersByNameB = new Dictionary<string, int>();

			currentFighterNameA = "";
			currentFighterNameB = "";


			if (itemCountA < 0)
			{
				rangeIndexA = 0;
				itemCountA = 25;
			}
			else
			{

				fightersA = DBHandler.GetFighters(out fightersByNameA, CreateFighterSelect_A());
				//if (rangeIndexA > 0) fightersA.Prepend(new KeyValuePair<int, string>(-1, "Previous"));
				//if (rangeIndexA < 1000) fightersA.Add(fightersA.Keys.Last() + 1, "\u2BC6...");

			}

			if (itemCountB < 0)
			{
				rangeIndexB = 0;
				itemCountB = 25;
			}
			else
			{
				fightersB = DBHandler.GetFighters(out fightersByNameB, CreateFighterSelect_B());
				//if (rangeIndexB > 0) fightersB.Prepend(new KeyValuePair<int, string>(-1, "Previous"));
				//if (rangeIndexB < 1000) fightersB.Add(fightersB.Keys.Last() + 1, "\u2BC6...");

			}

		}


		


		/// <summary>
		/// Creates a stats data chart from the id of a fighter
		/// </summary>
		/// <param name="fighterID"></param>
		/// <returns></returns>
		public List<DataChart> CreateFighterStatsChart(int fighterID)
		{
			DataRow fighter = DBHandler.GetFighterStats(fighterID);
			List<DataChart> dataPoints = new List<DataChart>();

			if (fighter != null)
			{
				dataPoints.Add(new DataChart("Knock Downs: ", (int)fighter["Knockdowns"]));
				dataPoints.Add(new DataChart("Submission Attempts: ", (int)fighter["Submission_Attempts"]));
				dataPoints.Add(new DataChart("Takedowns: ", (int)fighter["Takedowns"]));
				dataPoints.Add(new DataChart("Takedowns Attempted: ", (int)fighter["Takedowns_Attempted"]));
				dataPoints.Add(new DataChart("Total Strikes: ", (int)fighter["Total_Strikes"]));
				dataPoints.Add(new DataChart("Total Strikes Attempted: ", (int)fighter["Total_Strikes_Attempted"]));
			}
			return dataPoints;
		}
		


		/// <summary>
		/// Creates a profile data chart from the id of a fighter
		/// </summary>
		/// <param name="fighterID"></param>
		/// <returns></returns>
		public List<DataChart> CreateFighterProfileChart(int fighterID)
		{
			DataRow fighter = DBHandler.GetFighterProfile(fighterID);
			List<DataChart> dataPoints = new List<DataChart>();

			if(fighter != null)
			{
				if (fighter["nickname"] != null) dataPoints.Add(new DataChart("AKA: ", fighter["nickname"]));
				if(fighter["birth_date"] != null) dataPoints.Add(new DataChart("Born: ", fighter["birth_date"]));
				if(fighter["reach"] != null)dataPoints.Add(new DataChart("Reach: ", (System.Decimal)fighter["reach"]));
				if(fighter["height"] != null)dataPoints.Add(new DataChart("Height: ", (System.Decimal)fighter["height"]));
				if(fighter["weight"] != null)dataPoints.Add(new DataChart("Weight: ", (System.Decimal)fighter["weight"]));
				if(fighter["wins"] != null) dataPoints.Add(new DataChart("Wins: ", (int)fighter["wins"]));
				if(fighter["losses"] != null) dataPoints.Add(new DataChart("Losses: ", (int)fighter["losses"]));
				if (fighter["draws"] != null) dataPoints.Add(new DataChart("Draws: ", (int)fighter["draws"]));
				if(fighter["birth_city"] != null)dataPoints.Add(new DataChart("City: ", fighter["birth_city"]));
				if(fighter["birth_state"] != null)dataPoints.Add(new DataChart("State: ", fighter["birth_state"]));
				if(fighter["birth_country"] != null) dataPoints.Add(new DataChart("Country: ", fighter["birth_country"]));
			}

			return dataPoints;
		}

		

		/// <summary>
		/// Shifts the values for the fighter lists down
		/// </summary>
		/// <returns></returns>
		public int ShiftDown_A()
		{
			rangeIndexA -= itemCountA;

			if(rangeIndexA < 0) rangeIndexA = 0;
			else if (maxItems - itemCountA > 0)
			{
				fightersA = DBHandler.GetFighters(out fightersByNameA, CreateFighterSelect_A());
			}

			return rangeIndexA;
		}


		/// <summary>
		/// Shifts the values for the fighter lists down
		/// </summary>
		/// <returns></returns>
		public int ShiftDown_B()
		{
			rangeIndexB -= itemCountB;

			if(rangeIndexB < 0) rangeIndexB = 0;
			else if (maxItems - itemCountB < rangeIndexB && maxItems - itemCountB > 0)
			{
				fightersB = DBHandler.GetFighters(out fightersByNameB, CreateFighterSelect_B());
				//if (rangeIndexB > 0) fightersB.Prepend(new KeyValuePair<int, string>(-1, "Previous"));
				//if (rangeIndexB < 1000) fightersB.Add(fightersB.Keys.Last() + 1, "...");

			}

			return rangeIndexB;
		}


		/// <summary>
		/// Shifts the values for the fighter lists up
		/// </summary>
		/// <returns></returns>
		public int ShiftUp_A()
		{
			rangeIndexA += itemCountA;


			if(itemCountA > maxItems)
			{
				rangeIndexA = maxItems- itemCountA;
			}
			else if(maxItems-itemCountA > rangeIndexA)
			{
				fightersA = DBHandler.GetFighters(out fightersByNameA, CreateFighterSelect_A());
				//if (rangeIndexA > 0) fightersA.Prepend(new KeyValuePair<int, string>(-1, "Previous"));
				//if (rangeIndexA < maxItems) fightersA.Add(fightersA.Keys.Last() + 1, "...");

			}


			return rangeIndexA;
		}



		/// <summary>
		/// Shifts the values for the fighter lists up
		/// </summary>
		/// <returns></returns>
		public int ShiftUp_B()
		{
			rangeIndexB += itemCountB;


			if(itemCountB > maxItems)
			{
				rangeIndexB = maxItems- itemCountB;
			}
			else if(maxItems-itemCountB > rangeIndexB)
			{
				fightersB = DBHandler.GetFighters(out fightersByNameB, CreateFighterSelect_B());
				//if (rangeIndexB > 0) fightersB.Prepend(new KeyValuePair<int, string>(-1, "Previous"));
				//if (rangeIndexB < maxItems) fightersB.Add(fightersB.Keys.Last() + 1, "...");

			}


			return rangeIndexB;
		}


		/// <summary>
		/// Creates the select string for fighter collection A
		/// </summary>
		/// <returns></returns>
		public string CreateFighterSelect_A()
		{
			return ("WHERE Fighter_ID >= " + rangeIndexA.ToString() + " AND Fighter_ID <= " + (rangeIndexA + itemCountA).ToString());
			//return DBHandler.CreateRangedFighterSelect(rangeIndexA, rangeIndexA + itemCountA);
		}

		/// <summary>
		/// Creates the select string for fighter collection B
		/// </summary>
		/// <returns></returns>
		public string CreateFighterSelect_B()
		{
			return ("WHERE Fighter_ID >= " + rangeIndexB.ToString() + " AND Fighter_ID <= " + (rangeIndexB + itemCountB).ToString());
			//return DBHandler.CreateRangedFighterSelect(rangeIndexB, rangeIndexB + itemCountB);
		}


	} //end class



#else


/// <summary>
	/// Class for fighter values
	/// </summary>
	public class FighterChart
	{

		public int maxItems; //The maximum amount of possible items
		public Dictionary<int, string> allFighters = new Dictionary<int, string>();
		public Dictionary<string, int> allFightersByName; //Fighters by name(A for comparisons)

		public int rangeIndexA; //The index for the current range of fighters chosen
		public int itemCountA; //The amount of values to show in the range, also is the amount added to the index for each range
		public int currentSelectionA; //The currently selected fighter(side A)
		public List<DataChart> dcFighterA_stats = new List<DataChart>(); //Fighter comparison A list of datachart type that contains a label and value for an item
		public List<DataChart> dcFighterA_profile = new List<DataChart>(); //Fighter comparison A list of datachart type that contains a label and value for an item
		public string currentFighterNameA { get; set; } //The name of the current fighter(for comparison fighter A)
		
		public string currentFighterNameB { get; set; } //The name of the current fighter(for comparison fighter B)
		public int rangeIndexB; //The index for the current range of fighters chosen
		public int itemCountB; //The amount of values to show in the range, also is the amount added to the index for each range
		public int currentSelectionB; //The currently selected fighter(side B)
		public List<DataChart> dcFighterB_stats = new List<DataChart>(); //Fighter comparison B list of datachart type that contains a label and value for an item
		public List<DataChart> dcFighterB_profile = new List<DataChart>(); //Fighter comparison B list of datachart type that contains a label and value for an item

		public DataRow prediction = null;


		/// <summary>
		/// Initializer for the class
		/// </summary>
		public FighterChart()
		{
			rangeIndexA = 0;
			rangeIndexB = 0;
			itemCountA = 0;
			itemCountB = 0;
			maxItems = 0;
			currentSelectionA = 0;
			currentSelectionB = 0;
			currentFighterNameA = "";
			currentFighterNameB = "";

			allFighters = new Dictionary<int, string>();
			allFightersByName = new Dictionary<string, int>();
			allFighters = DBHandler.GetFighters(out allFightersByName);
			
		}


		/// <summary>
		/// Initializer for the class
		/// </summary>
		public FighterChart(int minIndex, int indexOffset, int maxItemCount = 1000)
		{
			rangeIndexA = minIndex;
			rangeIndexB = minIndex;
			itemCountA = indexOffset;
			itemCountB = indexOffset;
			maxItems = maxItemCount;
			currentSelectionA = 0;
			currentSelectionB = 0;
			currentFighterNameA = "";
			currentFighterNameB = "";

			allFighters = new Dictionary<int, string>();
			allFightersByName = new Dictionary<string, int>();
			allFighters = DBHandler.GetFighters(out allFightersByName);

			allFighters = DBHandler.GetFighters(out allFightersByName);

			if (itemCountA < 0)
			{
				rangeIndexA = 0;
				itemCountA = 25;
			}
			

			if (itemCountB < 0)
			{
				rangeIndexB = 0;
				itemCountB = 25;
			}
			

		}


		


		/// <summary>
		/// Creates a stats data chart from the id of a fighter
		/// </summary>
		/// <param name="fighterID"></param>
		/// <returns></returns>
		public List<DataChart> CreateFighterStatsChart(int fighterID)
		{
			DataRow fighter = DBHandler.GetFighterStats(fighterID);
			List<DataChart> dataPoints = new List<DataChart>();
			
			dataPoints.Add(new DataChart("Knock Downs: ", (int)fighter["Knockdowns"]));
			dataPoints.Add(new DataChart("Submission Attempts: ", (int)fighter["Submission_Attempts"]));
			dataPoints.Add(new DataChart("Takedowns: ", (int)fighter["Takedowns"]));
			dataPoints.Add(new DataChart("Takedowns Attempted: ", (int)fighter["Takedowns_Attempted"]));
			dataPoints.Add(new DataChart("Total Strikes: ", (int)fighter["Total_Strikes"]));
			dataPoints.Add(new DataChart("Total Strikes Attempted: ", (int)fighter["Total_Strikes_Attempted"]));

			return dataPoints;
		}
		


		/// <summary>
		/// Creates a profile data chart from the id of a fighter
		/// </summary>
		/// <param name="fighterID"></param>
		/// <returns></returns>
		public List<DataChart> CreateFighterProfileChart(int fighterID)
		{
			DataRow fighter = DBHandler.GetFighterProfile(fighterID);
			List<DataChart> dataPoints = new List<DataChart>();

			
			if (fighter["nickname"] != null) dataPoints.Add(new DataChart("AKA: ", fighter["nickname"]));
			if(fighter["birth_date"] != null) dataPoints.Add(new DataChart("Born: ", fighter["birth_date"]));
			if(fighter["reach"] != null)dataPoints.Add(new DataChart("Reach: ", (System.Decimal)fighter["reach"]));
			if(fighter["height"] != null)dataPoints.Add(new DataChart("Height: ", (System.Decimal)fighter["height"]));
			if(fighter["weight"] != null)dataPoints.Add(new DataChart("Weight: ", (System.Decimal)fighter["weight"]));
			if(fighter["wins"] != null) dataPoints.Add(new DataChart("Wins: ", (int)fighter["wins"]));
			if(fighter["losses"] != null) dataPoints.Add(new DataChart("Losses: ", (int)fighter["losses"]));
			if (fighter["draws"] != null) dataPoints.Add(new DataChart("Draws: ", (int)fighter["draws"]));
			if(fighter["birth_city"] != null)dataPoints.Add(new DataChart("City: ", fighter["birth_city"]));
			if(fighter["birth_state"] != null)dataPoints.Add(new DataChart("State: ", fighter["birth_state"]));
			if(fighter["birth_country"] != null) dataPoints.Add(new DataChart("Country: ", fighter["birth_country"]));
			
			
			return dataPoints;
		}

		/// <summary>
		/// Creates the select string for fighter collection A
		/// </summary>
		/// <returns></returns>
		public string CreateFighterSelect_A()
		{
			return ("WHERE Fighter_ID >= " + rangeIndexA.ToString() + " AND Fighter_ID <= " + (rangeIndexA + itemCountA).ToString());
			//return DBHandler.CreateRangedFighterSelect(rangeIndexA, rangeIndexA + itemCountA);
		}

		/// <summary>
		/// Creates the select string for fighter collection B
		/// </summary>
		/// <returns></returns>
		public string CreateFighterSelect_B()
		{
			return ("WHERE Fighter_ID >= " + rangeIndexB.ToString() + " AND Fighter_ID <= " + (rangeIndexB + itemCountB).ToString());
			//return DBHandler.CreateRangedFighterSelect(rangeIndexB, rangeIndexB + itemCountB);
		}



		///// <summary>
		///// Shifts the values for the fighter lists down
		///// </summary>
		///// <returns></returns>
		//public int ShiftDown_A()
		//{
		//	rangeIndexA -= itemCountA;

		//	if(rangeIndexA < 0) rangeIndexA = 0;
		//	else if (maxItems - itemCountA < rangeIndexA && maxItems - itemCountA > 0)
		//	{
		//		fightersA = DBHandler.GetFighters(out fightersByNameA, CreateFighterSelect_A());
		//		if (rangeIndexA > 0) fightersA.Prepend(new KeyValuePair<int, string>(-1, "Previous"));
		//		if (rangeIndexA < 1000) fightersA.Add(fightersA.Keys.Last() + 1, "...");

		//	}

		//	return rangeIndexA;
		//}


		///// <summary>
		///// Shifts the values for the fighter lists down
		///// </summary>
		///// <returns></returns>
		//public int ShiftDown_B()
		//{
		//	rangeIndexB -= itemCountB;

		//	if(rangeIndexB < 0) rangeIndexB = 0;
		//	else if (maxItems - itemCountB < rangeIndexB && maxItems - itemCountB > 0)
		//	{
		//		fightersB = DBHandler.GetFighters(out fightersByNameB, CreateFighterSelect_B());
		//		if (rangeIndexB > 0) fightersB.Prepend(new KeyValuePair<int, string>(-1, "Previous"));
		//		if (rangeIndexB < 1000) fightersB.Add(fightersB.Keys.Last() + 1, "...");

		//	}

		//	return rangeIndexB;
		//}


		///// <summary>
		///// Shifts the values for the fighter lists up
		///// </summary>
		///// <returns></returns>
		//public int ShiftUp_A()
		//{
		//	rangeIndexA += itemCountA;


		//	if(itemCountA > maxItems)
		//	{
		//		rangeIndexA = maxItems- itemCountA;
		//	}
		//	else if(maxItems-itemCountA > rangeIndexA)
		//	{
		//		fightersA = DBHandler.GetFighters(out fightersByNameA, CreateFighterSelect_A());
		//		if (rangeIndexA > 0) fightersA.Prepend(new KeyValuePair<int, string>(-1, "Previous"));
		//		if (rangeIndexA < maxItems) fightersA.Add(fightersA.Keys.Last() + 1, "...");

		//	}


		//	return rangeIndexA;
		//}



		///// <summary>
		///// Shifts the values for the fighter lists up
		///// </summary>
		///// <returns></returns>
		//public int ShiftUp_B()
		//{
		//	rangeIndexB += itemCountB;


		//	if(itemCountB > maxItems)
		//	{
		//		rangeIndexB = maxItems- itemCountB;
		//	}
		//	else if(maxItems-itemCountB > rangeIndexB)
		//	{
		//		fightersB = DBHandler.GetFighters(out fightersByNameB, CreateFighterSelect_B());
		//		if (rangeIndexB > 0) fightersB.Prepend(new KeyValuePair<int, string>(-1, "Previous"));
		//		if (rangeIndexB < maxItems) fightersB.Add(fightersB.Keys.Last() + 1, "...");

		//	}


		//	return rangeIndexB;
		//}





	} //end class


#endif






} //end namespace