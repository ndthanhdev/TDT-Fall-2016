using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Quiz.Data;
using System.Linq;

namespace Quiz.Controllers
{
    public class DeThisoldController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeThisoldController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            ViewData["Nhoms"] = _context.Nhoms
                .Select(m => new SelectListItem()
                {
                    Value = m.NhomId.ToString(),
                    Text = m.Ten
                });
            ViewData["CauHois"] = _context.Nhoms.Include(m => m.CauHois).FirstOrDefault()?.CauHois;

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}