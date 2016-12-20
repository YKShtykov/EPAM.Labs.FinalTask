using Client.Infrustructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Controllers
{
  public class HomeController : Controller
  {
    private UserService manager = new UserService();
    // GET: Home
    public ActionResult Index()
    {
      return View();
    }
    public ActionResult ToDoList(int? id)
    {
      if (id == null) RedirectToAction("Index");
      ViewBag.Id = id;
      return View();
    }
    public ActionResult CheckId(int id)
    {
      return Json(new { });
    }
    public ActionResult CreateUser()
    {
      int id =  manager.CreateUser();
      return Json(new {id}, JsonRequestBehavior.AllowGet);
    }
  }
}