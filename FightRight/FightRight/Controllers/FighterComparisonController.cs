using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace FightRight.Controllers
{
    public class FighterComparisonController : Controller
    {

        Models.FighterChart chart = Models.DBHandler.fighterInfo;
        
        [HttpGet]
        public ActionResult Index()
        {
            return View(chart);
        }

        

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            if(form["fighterSelectA"] != null && form["fighterSelectA"] != "")
			{
                chart.currentFighterNameA = form["fighterSelectA"];
                int fId=0;

				if (chart.fightersByNameA.TryGetValue(chart.currentFighterNameA, out fId) == true)
				{
                    chart.currentSelectionA = fId;
                    chart.dcFighterA_stats = chart.CreateFighterStatsChart(fId);
                    chart.dcFighterA_profile = chart.CreateFighterProfileChart(fId);
				}

                chart.currentSelectionA = fId;
            }

            
            if(form["fighterSelectB"] != null && form["fighterSelectB"] != "")
			{
                chart.currentFighterNameB = form["fighterSelectB"];
                int fId=0;
				//if (chart.allFightersByName.TryGetValue(chart.currentFighterNameB, out fId) == true)
				if (chart.fightersByNameB.TryGetValue(chart.currentFighterNameB, out fId) == true)
				{
                    chart.dcFighterB_stats = chart.CreateFighterStatsChart(fId);
                    chart.dcFighterB_profile = chart.CreateFighterProfileChart(fId);
                }

                chart.currentSelectionB = fId;
            }


            if (chart.currentSelectionB != 0 && chart.currentSelectionA != 0)
            {
                var prediction = Models.DBHandler.GetFighterPrediction(chart.currentSelectionA, chart.currentSelectionB);
                chart.prediction = prediction;
            }

            if (form["fighterSelect"] == "Submit")
			{
            }

            if (form["btnShiftDownA"] == "shift")
			{
                chart.ShiftDown_A();
            }

            if (form["btnShiftUpA"] == "shift")
			{
                chart.ShiftUp_A();
            }

			Models.DBHandler.fighterInfo = chart;

			return View(chart);
        }







		[HttpGet]
        public ActionResult FighterChart()
        {
            return View(chart);
        }



		

		[HttpPost]
		public ActionResult FighterChart(FormCollection form)
		{

            if (form["fighterSelectA"] != null && form["fighterSelectA"] != "")
            {
                chart.currentFighterNameA = form["fighterSelectA"];
                int fId = 0;

                if (chart.fightersByNameA.TryGetValue(chart.currentFighterNameA, out fId) == true)
                {
                    chart.currentSelectionA = fId;
                    chart.dcFighterA_stats = chart.CreateFighterStatsChart(fId);
                    chart.dcFighterA_profile = chart.CreateFighterProfileChart(fId);
                }

                chart.currentSelectionA = fId;
            }


            if (form["fighterSelectB"] != null && form["fighterSelectB"] != "")
            {
                chart.currentFighterNameB = form["fighterSelectB"];
                int fId = 0;
                //if (chart.allFightersByName.TryGetValue(chart.currentFighterNameB, out fId) == true)
                if (chart.fightersByNameB.TryGetValue(chart.currentFighterNameB, out fId) == true)
                {
                    chart.dcFighterB_stats = chart.CreateFighterStatsChart(fId);
                    chart.dcFighterB_profile = chart.CreateFighterProfileChart(fId);
                }

                chart.currentSelectionB = fId;
            }


            if (chart.currentSelectionB != 0 && chart.currentSelectionA != 0)
            {
                var prediction = Models.DBHandler.GetFighterPrediction(chart.currentSelectionA, chart.currentSelectionB);
                chart.prediction = prediction;
            }

            if (form["fighterSelect"] == "Submit")
            {
            }

            

            Models.DBHandler.fighterInfo = chart;

            return View(chart);
        }


    }
}

