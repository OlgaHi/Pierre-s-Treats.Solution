using Microsoft.AspNetCore.Mvc;
using Product.Models;
using System.Collections.Generic; 
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity; 
using System.Threading.Tasks; 
using System.Security.Claims;

namespace Product.Controllers
{
  public class FlavorsController : Controller
  {
    private readonly ProductContext _db;

    private readonly UserManager<ApplicationUser> _userManager;

    public FlavorsController(UserManager<ApplicationUser> userManager, ProductContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public ActionResult Index()
    {
      var flavorsList = _db.Flavors.ToList();
      return View(flavorsList);
    }

    [Authorize]
    public ActionResult Create()
    {
    ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "ProductName");
    return View();
    }

    [HttpPost]
    public ActionResult Create(Flavor flavor, int TreatId)
    {
      _db.Flavors.Add(flavor);
      _db.SaveChanges();
      if (TreatId != 0)
      {
        _db.FlavorTreat.Add(new FlavorTreat() { TreatId = TreatId, FlavorId = flavor.FlavorId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    


    

  }
}