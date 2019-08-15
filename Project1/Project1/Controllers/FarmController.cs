using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project1.Controllers
{
    public class FarmController : Controller
    {

        public Class1 Model { get; set; }

        // GET: Farm
        public ActionResult DashBoard(int index = 0,string sortBy="",bool isUp=false)
        {
            Model = new Class1();
            List<string> list = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N" };
            try
            {
                if (!string.IsNullOrEmpty(sortBy))
                {
                    list.Sort((s1, s2) => s1.CompareTo(s2));
                    if (isUp)
                    {
                        list.Reverse();
                    }
                }
                if (index == 0 && Convert.ToInt32(Session["Index"]) != 0 && Convert.ToInt32(Session["Index"]) < list.Count)
                {
                    index = Convert.ToInt32(Session["Index"]);
                }
                 if (index >= Convert.ToInt32(Session["Count"]))
                {
                    index = Convert.ToInt32(Session["Count"]);
                }
                Session["Start"] = (((index * 5) - 5 < 0 ? 0 : (index * 5) - 5) < list.Count) ? ((index * 5) - 5 < 0 ? 0 : (index * 5) - 5) : (list.Count - 1);
                Session["End"] = ((Convert.ToInt32(Session["Start"]) + 5) < list.Count ? (Convert.ToInt32(Session["Start"]) + 5) : list.Count - 1) - Convert.ToInt32(Session["Start"]);
                Model.List1 = list.GetRange(Convert.ToInt32(Session["Start"]), Convert.ToInt32(Session["End"]));
                Session["Index"] = index == 0 ? 1 : index;
                Session["Count"] = list.Count % 5 == 0 ? (list.Count / 5) : ((list.Count / 5) + 1);
            }
            catch (Exception) { }
            return View(Model);
        }

        public ActionResult LandManagement()
        {
            return View();
        }

        public ActionResult PetManagement()
        {
            return View();
        }

        public ActionResult Resources()
        {
            return View();
        }

        public ActionResult Links()
        {
            return View();
        }

        public ActionResult Status()
        {
            return View();
        }

        public List<string> Pager(FormCollection col)
        {
            
            return new List<string>();
        }

    }
}