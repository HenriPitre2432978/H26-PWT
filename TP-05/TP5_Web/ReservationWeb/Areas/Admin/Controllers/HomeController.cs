using Microsoft.AspNetCore.Mvc;
using ReservationWeb.DataAccessLayer;
using ReservationWeb.Models;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace ReservationWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public ActionResult Index() => RedirectToAction("List", "MenuChoice");
    }
}

