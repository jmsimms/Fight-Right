using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace FightRight.Controllers {
	public class HomeController : Controller {
		Models.FighterChart chart = new Models.FighterChart(0,25);
		

		public ActionResult Index() {
			//Models.DBHandler.fighter_id_name = Models.DBHandler.GetFighters();
			//return View(Models.DBHandler.fighter_id_name);
			return View();
		}

		//public ActionResult FighterChart()
		//{
		//	var dict = Models.DBHandler.GetFighters(chart.CreateFighterSelect());

		//	return View(dict);
		//}



		//[HttpPost]
		//public ActionResult Chart(FormCollection col)
		//{
		//	if (col["btnSubmit"] == "close")
		//	{
		//		return RedirectToAction("Index", "Home");
		//	}

		//	if (col["btnSubmit"] == "gallery")
		//	{
		//		return RedirectToAction("EventGallery", "Event");
		//	}

		//	if(col["btnTableShiftLeft"] == "")
		//	{
		//		var ls = chart.ShiftLeft(); 
		//	}
		//	if(col["btnTableShiftRight"] == "")
		//	{
		//		var rs = chart.ShiftRight();
		//	}
		//	return View();
		//}


		//FormCollection col

	}
}