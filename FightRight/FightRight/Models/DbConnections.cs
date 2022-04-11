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
	/// Partial database class for making connections to database. Partial so we don't fill one file with miles of code
	/// </summary>
	public partial class Database
	{


		/// <summary>
		/// Gets a connection to a database
		/// </summary>
		/// <param name="SQLConn">The sql connection variable to use</param>
		/// <param name="configurationString">The connection string</param>
		/// <returns>true if worked</returns>
		/// <exception cref="Exception">Literally any exception</exception>
		protected bool GetDBConnection(ref SqlConnection SQLConn, string configurationString)
		{

			try
			{
				if (SQLConn == null) SQLConn = new SqlConnection();
				if (SQLConn.State != ConnectionState.Open)
				{
					SQLConn.ConnectionString = configurationString;
					SQLConn.Open();
				}
				return true;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}



		/// <summary>
		/// Closes a connection to a database
		/// </summary>
		/// <param name="SQLConn">The open connection</param>
		/// <returns>True if closed</returns>
		/// <exception cref="Exception">Literally any exception</exception>
		protected bool CloseDBConnection(ref SqlConnection SQLConn)
		{
			try
			{
				if (SQLConn.State != ConnectionState.Closed)
				{
					SQLConn.Close();
					SQLConn.Dispose();
					SQLConn = null;
				}
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}



		/// <summary>
		/// Sets the parameters for sql command
		/// </summary>
		/// <param name="sqlCommand">The sql command to use</param>
		/// <param name="ParameterName">The name of the parameter</param>
		/// <param name="Value">Value for the parameter</param>
		/// <param name="ParameterType">The type of parameter</param>
		/// <param name="FieldSize">The size of the field</param>
		/// <param name="Direction">Direction for the parameters</param>
		/// <param name="Precision">The precision value for the parameters</param>
		/// <param name="Scale">The scale value for the parameters</param>
		/// <returns>0 on success</returns>
		/// <exception cref="Exception">Literally any exception</exception>
		protected int SetParameter(ref SqlCommand sqlCommand, string ParameterName, Object Value
			, SqlDbType ParameterType, int FieldSize = -1
			, ParameterDirection Direction = ParameterDirection.Input
			, Byte Precision = 0, Byte Scale = 0)
		{
			try
			{
				sqlCommand.CommandType = CommandType.StoredProcedure;
				if (FieldSize == -1)
					sqlCommand.Parameters.Add(ParameterName, ParameterType);
				else
					sqlCommand.Parameters.Add(ParameterName, ParameterType, FieldSize);

				if (Precision > 0) sqlCommand.Parameters[sqlCommand.Parameters.Count - 1].Precision = Precision;
				if (Scale > 0) sqlCommand.Parameters[sqlCommand.Parameters.Count - 1].Scale = Scale;

				sqlCommand.Parameters[sqlCommand.Parameters.Count - 1].Value = Value;
				sqlCommand.Parameters[sqlCommand.Parameters.Count - 1].Direction = Direction;

				return 0;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}



		/// <summary>
		/// Sets the parameters for sql command
		/// </summary>
		/// <param name="sqlDataAdapter">The sql Data adapter to use</param>
		/// <param name="ParameterName">The name of the parameter</param>
		/// <param name="Value">Value for the parameter</param>
		/// <param name="ParameterType">The type of parameter</param>
		/// <param name="FieldSize">The size of the field</param>
		/// <param name="Direction">Direction for the parameters</param>
		/// <param name="Precision">The precision value for the parameters</param>
		/// <param name="Scale">The scale value for the parameters</param>
		/// <returns>0 on success</returns>
		/// <exception cref="Exception">Literally any exception</exception>
		protected int SetParameter(ref SqlDataAdapter sqlDataAdapter, string ParameterName, Object Value
			, SqlDbType ParameterType, int FieldSize = -1
			, ParameterDirection Direction = ParameterDirection.Input
			, Byte Precision = 0, Byte Scale = 0)
		{
			try
			{
				sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
				if (FieldSize == -1)
					sqlDataAdapter.SelectCommand.Parameters.Add(ParameterName, ParameterType);
				else
					sqlDataAdapter.SelectCommand.Parameters.Add(ParameterName, ParameterType, FieldSize);

				if (Precision > 0) sqlDataAdapter.SelectCommand.Parameters[sqlDataAdapter.SelectCommand.Parameters.Count - 1].Precision = Precision;
				if (Scale > 0) sqlDataAdapter.SelectCommand.Parameters[sqlDataAdapter.SelectCommand.Parameters.Count - 1].Scale = Scale;

				sqlDataAdapter.SelectCommand.Parameters[sqlDataAdapter.SelectCommand.Parameters.Count - 1].Value = Value;
				sqlDataAdapter.SelectCommand.Parameters[sqlDataAdapter.SelectCommand.Parameters.Count - 1].Direction = Direction;

				return 0;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}





		/// <summary>
		/// Runs the cmd passed on the fight right database. 
		/// </summary>
		/// <param name="cmd">The command to run</param>
		/// <param name="cmdType">The type of command it is. Defaults to CommandType.Text</param>
		/// <returns>A list of the data rows returned. List great for ordering</returns>
		public List<DataRow> ExecSql(string cmd, string connectionString, CommandType cmdType = CommandType.Text)
		{
			List<DataRow> sqlReturn = new List<DataRow>();

			try
			{

				DataSet ds = new DataSet(); //Data set to use
				SqlConnection conn = new SqlConnection(); //SQL connection to use

				//If a connection could not be made to the database...
				if (!GetDBConnection(ref conn, connectionString)) //Here for local host testing
				{
					//Throw a new exception
					throw new Exception("Could not connect to database");
				}

				//Create a data adapter
				SqlDataAdapter adapter = new SqlDataAdapter(cmd, conn);

				//Set the command type
				adapter.SelectCommand.CommandType = cmdType;

				//Try to fill the data set from the data adapter...
				try
				{
					adapter.Fill(ds);


					//Check for errors
					if (ds.Tables[0].Rows.Count > 0)
					{
						//Get the data rows
						sqlReturn.AddRange(ds.Tables[0].Rows.Cast<DataRow>());
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

			return sqlReturn;
		}





		/// <summary>
		/// Runs the cmd passed on the fight right database. 
		/// </summary>
		/// <param name="stored_proc_name">The stored procedure to run</param>
		/// <param name="id">The id to perform on</param>
		/// <returns>A list of the data rows returned. List great for ordering</returns>
		public List<DataRow> ExecSqlStoredProc(string stored_proc_name, int id, string connectionString)
		{
			List<DataRow> sqlReturn = new List<DataRow>();

			try
			{

				DataSet ds = new DataSet(); //Data set to use
				SqlConnection conn = new SqlConnection(); //SQL connection to use

				//If a connection could not be made to the database...
				if (!GetDBConnection(ref conn, connectionString)) //Here for local host testing
				{
					//Throw a new exception
					throw new Exception("Could not connect to database");
				}

				//Create a data adapter
				SqlDataAdapter adapter = new SqlDataAdapter(stored_proc_name, conn);

				//Set the command type
				adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

				SetParameter(ref adapter, "Fighter_ID", id, SqlDbType.Int);

				//Try to fill the data set from the data adapter...
				try
				{
					adapter.Fill(ds);


					//Check for errors
					if (ds.Tables[0].Rows.Count > 0)
					{
						//Get the data rows
						sqlReturn.AddRange(ds.Tables[0].Rows.Cast<DataRow>());
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

			return sqlReturn;
		}



		/// <summary>
		/// Runs the cmd passed on the fight right database for the fighter prediction. 
		/// </summary>
		/// <param name="id1">The id to perform on</param>
		/// <param name="id2">The id to perform on</param>
		/// <returns>A list of the data rows returned. List great for ordering</returns>
		public List<DataRow> ExecSqlStoredProc(int id1, int id2, string connectionString)
		{
			List<DataRow> sqlReturn = new List<DataRow>();

			try
			{

				DataSet ds = new DataSet(); //Data set to use
				SqlConnection conn = new SqlConnection(); //SQL connection to use

				//If a connection could not be made to the database...
				if (!GetDBConnection(ref conn, connectionString)) //Here for local host testing
				{
					//Throw a new exception
					throw new Exception("Could not connect to database");
				}

				//Create a data adapter
				//SqlCommand cmd = new SqlCommand("FR_Predict_Fight_Outcome", conn);
				SqlDataAdapter adapter = new SqlDataAdapter("FR_Predict_Fight_Outcome", conn);

				//Set the command type
				adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

				SetParameter(ref adapter, "Fighter1_ID ", id1, SqlDbType.Int);
				SetParameter(ref adapter, "Fighter2_ID ", id2, SqlDbType.Int);

				//Try to fill the data set from the data adapter...
				try
				{
					adapter.Fill(ds);


					//Check for errors
					if (ds.Tables[0].Rows.Count > 0)
					{
						//Get the data rows
						sqlReturn.AddRange(ds.Tables[0].Rows.Cast<DataRow>());
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

			return sqlReturn;
		}

	}
}
