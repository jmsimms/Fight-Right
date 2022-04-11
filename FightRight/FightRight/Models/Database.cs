using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;

namespace FightRight.Models
{
	/// <summary>
	/// Class for database connections
	/// </summary>
	public partial class Database
	{
		
		/// <summary>
		/// Runs the SELECT * statement 
		/// </summary>
		/// <returns>A hashset of the DataRow's returned from TEvents</returns>
		/// <exception cref="Exception"></exception>
		public HashSet<DataRow> SelectAllEvents(string connectionString, string additionalSelectCommands = "")
		{

			HashSet<DataRow> eventRows = new HashSet<DataRow>();



			try
			{

				DataSet ds = new DataSet(); //Data set to use
				SqlConnection conn = new SqlConnection(); //SQL connection to use

				//localhost testing
				//string connectionString = @"Data Source=(local);Initial Catalog=dbFightRight;Integrated Security=True;"; //The string to connect to the db with

				if (!GetDBConnection(ref conn, connectionString)) //Here for local host testing

				//If a connection could not be made to the database...
				//if (!GetDBConnection(ref conn))
				{
					//Throw a new exception
					throw new Exception("Could not connect to database");
				}

				//Create a data adapter
				string sqlStatement = "SELECT * FROM TEvents" + additionalSelectCommands;

				SqlDataAdapter adapter = new SqlDataAdapter(sqlStatement, conn);

				//Set the command type
				adapter.SelectCommand.CommandType = CommandType.Text;

				//Try to fill the data set from the data adapter...
				try
				{
					adapter.Fill(ds);


					//Check for errors
					if (ds.Tables[0].Rows.Count > 0)
					{
						//Get the data rows as a hash set
						eventRows.UnionWith(ds.Tables[0].Rows.Cast<DataRow>());
					}
				}
				catch (Exception ex2)
				{

					//SysLog.UpdateLogFile(this.ToString(), MethodBase.GetCurrentMethod().Name.ToString(), ex2.Message);
				}

				CloseDBConnection(ref conn);


			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}


			return eventRows;

		}



		/// <summary>
		/// Runs the SELECT * statement 
		/// </summary>
		/// <returns>A hashset of the DataRow's returned from TEvent_Fights</returns>
		/// <exception cref="Exception"></exception>
		public HashSet<DataRow> SelectAllEventFights(string connectionString, string additionalSelectCommands = "")
		{

			HashSet<DataRow> eventFights = new HashSet<DataRow>();



			try
			{

				DataSet ds = new DataSet(); //Data set to use
				SqlConnection conn = new SqlConnection(); //SQL connection to use

				//localhost testing
				//string connectionString = @"Data Source=(local);Initial Catalog=dbFightRight;Integrated Security=True;"; //The string to connect to the db with

				if (!GetDBConnection(ref conn, connectionString)) //Here for local host testing

				//If a connection could not be made to the database...
				//if (!GetDBConnection(ref conn))
				{
					//Throw a new exception
					throw new Exception("Could not connect to database");
				}

				//Create a data adapter
				string sqlStatement = "SELECT * FROM TEvent_Fights " + additionalSelectCommands;

				SqlDataAdapter adapter = new SqlDataAdapter(sqlStatement, conn);

				//Set the command type
				adapter.SelectCommand.CommandType = CommandType.Text;

				//Try to fill the data set from the data adapter...
				try
				{
					adapter.Fill(ds);


					//Check for errors
					if (ds.Tables[0].Rows.Count > 0)
					{
						//Get the data rows as a hash set
						eventFights.UnionWith(ds.Tables[0].Rows.Cast<DataRow>());
					}
				}
				catch (Exception ex2)
				{

					//SysLog.UpdateLogFile(this.ToString(), MethodBase.GetCurrentMethod().Name.ToString(), ex2.Message);
				}

				CloseDBConnection(ref conn);


			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}


			return eventFights;

		}
		


		/// <summary>
		/// Runs the SELECT * statement 
		/// </summary>
		/// <returns>A hashset of the DataRow's returned from TFights</returns>
		/// <exception cref="Exception"></exception>
		public HashSet<DataRow> SelectAllFights(string connectionString, string additionalSelectCommands = "")
		{

			HashSet<DataRow> fights = new HashSet<DataRow>();



			try
			{

				DataSet ds = new DataSet(); //Data set to use
				SqlConnection conn = new SqlConnection(); //SQL connection to use

				//localhost testing
				//string connectionString = @"Data Source=(local);Initial Catalog=dbFightRight;Integrated Security=True;"; //The string to connect to the db with

				if (!GetDBConnection(ref conn, connectionString)) //Here for local host testing

				//If a connection could not be made to the database...
				//if (!GetDBConnection(ref conn))
				{
					//Throw a new exception
					throw new Exception("Could not connect to database");
				}

				//Create a data adapter
				string sqlStatement = "SELECT * FROM TFights " + additionalSelectCommands;

				SqlDataAdapter adapter = new SqlDataAdapter(sqlStatement, conn);

				//Set the command type
				adapter.SelectCommand.CommandType = CommandType.Text;

				//Try to fill the data set from the data adapter...
				try
				{
					adapter.Fill(ds);


					//Check for errors
					if (ds.Tables[0].Rows.Count > 0)
					{
						//Get the data rows as a hash set
						fights.UnionWith(ds.Tables[0].Rows.Cast<DataRow>());
					}
				}
				catch (Exception ex2)
				{

					//SysLog.UpdateLogFile(this.ToString(), MethodBase.GetCurrentMethod().Name.ToString(), ex2.Message);
				}

				CloseDBConnection(ref conn);


			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}


			return fights;

		}



		/// <summary>
		/// Runs the SELECT * statement 
		/// </summary>
		/// <returns>A hashset of the DataRow's returned from TFighters</returns>
		/// <exception cref="Exception"></exception>
		public HashSet<DataRow> SelectAllFighters(string connectionString, string additionalSelectCommands = "")
		{

			HashSet<DataRow> fighterRows = new HashSet<DataRow>();



			try
			{

				DataSet ds = new DataSet(); //Data set to use
				SqlConnection conn = new SqlConnection(); //SQL connection to use

				//localhost testing
				//string connectionString = @"Data Source=(local);Initial Catalog=dbFightRight;Integrated Security=True;"; //The string to connect to the db with

				if (!GetDBConnection(ref conn, connectionString)) //Here for local host testing

				//If a connection could not be made to the database...
				//if (!GetDBConnection(ref conn))
				{
					//Throw a new exception
					throw new Exception("Could not connect to database");
				}

				//Create a data adapter
				string sqlStatement = "SELECT * FROM TFighters " + additionalSelectCommands;

				SqlDataAdapter adapter = new SqlDataAdapter(sqlStatement, conn);

				//Set the command type
				adapter.SelectCommand.CommandType = CommandType.Text;

				//Try to fill the data set from the data adapter...
				try
				{
					adapter.Fill(ds);


					//Check for errors
					if (ds.Tables[0].Rows.Count > 0)
					{
						//Get the data rows as a hash set
						fighterRows.UnionWith(ds.Tables[0].Rows.Cast<DataRow>());
					}
				}
				catch (Exception ex2)
				{

					//SysLog.UpdateLogFile(this.ToString(), MethodBase.GetCurrentMethod().Name.ToString(), ex2.Message);
				}

				CloseDBConnection(ref conn);


			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}


			return fighterRows;

		}





		/// <summary>
		/// Runs the SELECT * statement 
		/// </summary>
		/// <returns>The dataset returned from TFighter_Profile</returns>
		/// <exception cref="Exception"></exception>
		public DataSet GetFighterProfileDataset(string connectionString, string additionalSelectCommands = "")
		{
				DataSet ds = new DataSet(); //Data set to use

			try
			{

				SqlConnection conn = new SqlConnection(); //SQL connection to use

				//localhost testing
				//string connectionString = @"Data Source=(local);Initial Catalog=dbFightRight;Integrated Security=True;"; //The string to connect to the db with

				if (!GetDBConnection(ref conn, connectionString)) //Here for local host testing

				//If a connection could not be made to the database...
				//if (!GetDBConnection(ref conn))
				{
					//Throw a new exception
					throw new Exception("Could not connect to database");
				}

				//Create a data adapter
				string sqlStatement = "SELECT * FROM TFighter_Profile " + additionalSelectCommands;

				SqlDataAdapter adapter = new SqlDataAdapter(sqlStatement, conn);

				//Set the command type
				adapter.SelectCommand.CommandType = CommandType.Text;

				//Try to fill the data set from the data adapter...
				try
				{
					adapter.Fill(ds);
				}
				catch (Exception ex2)
				{

					//SysLog.UpdateLogFile(this.ToString(), MethodBase.GetCurrentMethod().Name.ToString(), ex2.Message);
				}

				CloseDBConnection(ref conn);


			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}


			return ds;

		}



		/// <summary>
		/// Runs the SELECT * statement 
		/// </summary>
		/// <returns>The dataset returned from TFighter_Stats</returns>
		/// <exception cref="Exception"></exception>
		public DataSet GetFighterStatDataset(string connectionString, string additionalSelectCommands = "")
		{
			DataSet ds = new DataSet(); //Data set to use

			try
			{

				SqlConnection conn = new SqlConnection(); //SQL connection to use

				//localhost testing
				//string connectionString = @"Data Source=(local);Initial Catalog=dbFightRight;Integrated Security=True;"; //The string to connect to the db with

				if (!GetDBConnection(ref conn, connectionString)) //Here for local host testing

				//If a connection could not be made to the database...
				//if (!GetDBConnection(ref conn))
				{
					//Throw a new exception
					throw new Exception("Could not connect to database");
				}

				//Create a data adapter
				string sqlStatement = "SELECT * FROM TFighter_Stats " + additionalSelectCommands;

				SqlDataAdapter adapter = new SqlDataAdapter(sqlStatement, conn);

				//Set the command type
				adapter.SelectCommand.CommandType = CommandType.Text;

				//Try to fill the data set from the data adapter...
				try
				{
					adapter.Fill(ds);
				}
				catch (Exception ex2)
				{

					//SysLog.UpdateLogFile(this.ToString(), MethodBase.GetCurrentMethod().Name.ToString(), ex2.Message);
				}

				CloseDBConnection(ref conn);


			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}


			return ds;

		}


		/// <summary>
		/// Runs the SELECT * statement 
		/// </summary>
		/// <returns>The dataset returned from TFighters</returns>
		/// <exception cref="Exception"></exception>
		public DataSet GetTFighterDataset(string connectionString, string additionalSelectCommands = "")
		{
			DataSet ds = new DataSet(); //Data set to use

			try
			{

				SqlConnection conn = new SqlConnection(); //SQL connection to use

				//localhost testing
				//string connectionString = @"Data Source=(local);Initial Catalog=dbFightRight;Integrated Security=True;"; //The string to connect to the db with

				if (!GetDBConnection(ref conn, connectionString)) //Here for local host testing

				//If a connection could not be made to the database...
				//if (!GetDBConnection(ref conn))
				{
					//Throw a new exception
					throw new Exception("Could not connect to database");
				}

				//Create a data adapter
				string sqlStatement = "SELECT * FROM TFighters " + additionalSelectCommands;

				SqlDataAdapter adapter = new SqlDataAdapter(sqlStatement, conn);

				//Set the command type
				adapter.SelectCommand.CommandType = CommandType.Text;

				//Try to fill the data set from the data adapter...
				try
				{
					adapter.Fill(ds);
				}
				catch (Exception ex2)
				{

					//SysLog.UpdateLogFile(this.ToString(), MethodBase.GetCurrentMethod().Name.ToString(), ex2.Message);
				}

				CloseDBConnection(ref conn);


			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}


			return ds;

		}



		/// <summary>
		/// Runs the SELECT * statement 
		/// </summary>
		/// <returns>A hashset of the DataRow's returned from TFighter_Profile</returns>
		/// <exception cref="Exception"></exception>
		public HashSet<DataRow> SelectAllFighterProfiles(string connectionString, string additionalSelectCommands = "")
		{

			HashSet<DataRow> fighterRows = new HashSet<DataRow>();

			try
			{

				DataSet ds = new DataSet(); //Data set to use
				SqlConnection conn = new SqlConnection(); //SQL connection to use

				//localhost testing
				//string connectionString = @"Data Source=(local);Initial Catalog=dbFightRight;Integrated Security=True;"; //The string to connect to the db with

				if (!GetDBConnection(ref conn, connectionString)) //Here for local host testing

				//If a connection could not be made to the database...
				//if (!GetDBConnection(ref conn))
				{
					//Throw a new exception
					throw new Exception("Could not connect to database");
				}

				//Create a data adapter
				string sqlStatement = "SELECT * FROM TFighter_Profile " + additionalSelectCommands;

				SqlDataAdapter adapter = new SqlDataAdapter(sqlStatement, conn);

				//Set the command type
				adapter.SelectCommand.CommandType = CommandType.Text;

				//Try to fill the data set from the data adapter...
				try
				{
					adapter.Fill(ds);


					//Check for errors
					if (ds.Tables[0].Rows.Count > 0)
					{
						//Get the data rows as a hash set
						fighterRows.UnionWith(ds.Tables[0].Rows.Cast<DataRow>());
					}
				}
				catch (Exception ex2)
				{

					//SysLog.UpdateLogFile(this.ToString(), MethodBase.GetCurrentMethod().Name.ToString(), ex2.Message);
				}

				CloseDBConnection(ref conn);


			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}


			return fighterRows;

		}



		/// <summary>
		/// Runs the SELECT * statement 
		/// </summary>
		/// <returns>A hashset of the DataRow's returned from TFighter_Stats</returns>
		/// <exception cref="Exception"></exception>
		public HashSet<DataRow> SelectAllFighterStats(string connectionString, string additionalSelectCommands = "")
		{

			HashSet<DataRow> fighterRows = new HashSet<DataRow>();

			try
			{

				DataSet ds = new DataSet(); //Data set to use
				SqlConnection conn = new SqlConnection(); //SQL connection to use

				//localhost testing
				//string connectionString = @"Data Source=(local);Initial Catalog=dbFightRight;Integrated Security=True;"; //The string to connect to the db with

				if (!GetDBConnection(ref conn, connectionString)) //Here for local host testing

				//If a connection could not be made to the database...
				//if (!GetDBConnection(ref conn))
				{
					//Throw a new exception
					throw new Exception("Could not connect to database");
				}

				//Create a data adapter
				string sqlStatement = "SELECT * FROM TFighter_Stats " + additionalSelectCommands;

				SqlDataAdapter adapter = new SqlDataAdapter(sqlStatement, conn);

				//Set the command type
				adapter.SelectCommand.CommandType = CommandType.Text;

				//Try to fill the data set from the data adapter...
				try
				{
					adapter.Fill(ds);


					//Check for errors
					if (ds.Tables[0].Rows.Count > 0)
					{
						//Get the data rows as a hash set
						fighterRows.UnionWith(ds.Tables[0].Rows.Cast<DataRow>());
					}
				}
				catch (Exception ex2)
				{

					//SysLog.UpdateLogFile(this.ToString(), MethodBase.GetCurrentMethod().Name.ToString(), ex2.Message);
				}

				CloseDBConnection(ref conn);


			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}


			return fighterRows;

		}



	} //end class
} //end namespace
