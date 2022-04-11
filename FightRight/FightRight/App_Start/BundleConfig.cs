using System.Web;
using System.Web.Optimization;

namespace FightRight {
	public class BundleConfig {
		public static void RegisterBundles(BundleCollection bundles) {
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/web2.js",
						"~/Scripts/modernizr-{version}.js",
						"~/Scripts/jquery.filedrop.js",
						"~/Scripts/jquery-{version}.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
						"~/Content/bootstrap.css",
						"~/Content/bootstrap-utilities.css",
						"~/Content/styles.css",
						"~/Content/site.css"
			));

			


		}
	}
}


//				"~/ Scripts/bootstrap.bundle.js",
//
//"~/Scripts/bootstrap.js"
/*bundles.Add(new StyleBundle("~/bundles/bootstrap").Include(
						"~/Scripts/bootstrap.bundle.min.js",
						"~/Scripts/bootstrap.js"
			));
 *<script src="Scripts/bootstrap.bundle.min.js" crossorigin="anonymous"></script>
	<script src="Scripts/bootstrap.js"></script>
 */