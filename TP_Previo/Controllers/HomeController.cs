using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Newtonsoft.Json;
using TP_Previo.Models.Extras;
using System.Configuration;
using System.Data.SqlClient;

namespace TP_Previo.Controllers
{
    public class HomeController : Controller
    {
        List<Country> paises;
        public ActionResult Index()
        {
            LoadCountryDDL();
            SelectCountry();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public void SelectCountry()
        {
            ViewData["requestpais"] = Request.Form["paises"];
            if (User.Identity.IsAuthenticated && Request.Form["paises"] != "")
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                conn.Open();
                string query = "UPDATE dbo.AspNetUsers SET Country = '" + Request.Form["paises"] + "' WHERE Email = '" + User.Identity.Name + "'";
                //ViewData["requestpais"] = query;
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
            }
        }

        public void LoadCountryDDL()
        {
            string strApiUrl = "https://api.mercadolibre.com/classified_locations/countries";
            var json = new WebClient().DownloadString(strApiUrl);
            this.paises = JsonConvert.DeserializeObject<List<Country>>(json);
            ViewData["paises"] = new SelectList(paises, "Name", "Name", paises[0]);
        }
    }
}