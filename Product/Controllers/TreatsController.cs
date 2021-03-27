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
  public class TreatsController : Controller
  {
    private readonly ProductContext _db;

    private readonly UserManager<ApplicationUser> _userManager;

    public TreatsController(UserManager<ApplicationUser> userManager, ProductContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public ActionResult Index()
    {
      var treatsList = _db.Treats.ToList();
      return View(treatsList);
    }

    [Authorize]
    public ActionResult Create()
    {
    ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "FlavorType");
    return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Treat treat, int FlavorId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      treat.User = currentUser;
      _db.Treats.Add(treat);
      if (FlavorId != 0)
      {
        _db.FlavorTreat.Add(new FlavorTreat() { FlavorId = FlavorId, TreatId = treat.TreatId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}