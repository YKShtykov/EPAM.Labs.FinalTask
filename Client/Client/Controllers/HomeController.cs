using Client.Infrustructure;
using Client.Interfaces;
using Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Controllers
{
  public class HomeController : Controller
  {
    //private readonly IUserService _service;

    //public HomeController(IUserService service)
    //{
    //    _service = service;
    //}

    private readonly ClientUserService _service = new ClientUserService();

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
      bool register = _service.Login(id);
      if (register)
      {
        return Json(new { id }, JsonRequestBehavior.AllowGet);
      }
      else
        return RedirectToAction("Index");
    }
    public ActionResult CreateUser()
    {
      int id = _service.GetOrCreateUser();//_service.CreateUser("baba");
      return Json(new { id }, JsonRequestBehavior.AllowGet);
    }

    public ActionResult Logout(int id)
    {
      _service.Logout(id);
      return Json(new { data = true }, JsonRequestBehavior.AllowGet);
    }
  }
}