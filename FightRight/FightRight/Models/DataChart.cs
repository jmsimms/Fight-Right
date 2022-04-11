using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;





namespace FightRight.Models
{
	
	//DataContract for Serializing Data - required to serve in JSON format
	//[DataContract]

	/// <summary>
	/// Class for single points of data accompanying a label
	/// </summary>
	public class DataChart
	{
		//Variables
		public string Label { get; protected set; } //The label for the value
		public double value { get; protected set; } //The value(as a double)
		public string valueString{ get; protected set; } //The value(as a string)


		/// <summary>
		/// Initializer for the class
		/// </summary>
		/// <param name="label"></param>
		/// <param name="newValue"></param>
		public DataChart(string label, double newValue)
		{
			this.Label = label;
			this.value = newValue;
			this.valueString = newValue.ToString();
		}


		/// <summary>
		/// Initializer for the class
		/// </summary>
		/// <param name="label"></param>
		/// <param name="newValue"></param>
		public DataChart(string label, int newValue)
		{
			this.Label = label;
			this.value = Convert.ToDouble(newValue);
			this.valueString = newValue.ToString();
		}


		/// <summary>
		/// Initializer for the class
		/// </summary>
		/// <param name="label"></param>
		/// <param name="newValue"></param>
		public DataChart(string label, System.Decimal newValue)
		{
			this.Label = label;
			this.value = Convert.ToDouble(newValue);
			this.valueString = newValue.ToString();
		}


		/// <summary>
		/// Initializer for the class
		/// </summary>
		/// <param name="label"></param>
		/// <param name="newValue"></param>
		public DataChart(string label, string newValue)
		{
			this.Label = label;
			this.value = 0;
			this.valueString = newValue;
		}



		/// <summary>
		/// Initializer for the class
		/// </summary>
		/// <param name="label"></param>
		/// <param name="newValue"></param>
		public DataChart(string label, object newValue)
		{
			this.Label = label;
			this.value = 0;
			this.valueString = newValue.ToString();
		}
 
		////Explicitly setting the name to be used while serializing to JSON.
		//[DataMember(Name = "label")]
		//public string Label = "";
 
		////Explicitly setting the name to be used while serializing to JSON.
		//[DataMember(Name = "y")]
		//public Nullable<double> Y = null;
	}
}