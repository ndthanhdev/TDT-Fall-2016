using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quiz.Data;
using Quiz.Models;
using Quiz.Models.AttendQuizViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Controllers
{
    public class AttendController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AttendController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Attending(IndexViewModel vm)
        {
            var deThi = await _context.DeThis.SingleOrDefaultAsync(m => m.DeThiId == vm.SelectedDeThi);

            if (deThi == null)
                return NotFound();

            ViewData["Ten"] = deThi.Ten;

            var cauHois = await _context.CauHoiDeThis.Include(m => m.CauHoi)
                .Where(m => m.DeThiId == vm.SelectedDeThi)
                .Select(m => m.CauHoi)
                .ToListAsync();

            var userId = _userManager.GetUserId(HttpContext.User);

            var avm = new AttendingViewModel()
            {
                DeThiId = deThi.DeThiId,
                Questions = new List<QuestionItem>()
            };
            foreach (var item in cauHois ?? new List<CauHoi>())
            {
                ((List<QuestionItem>)avm.Questions).Add(new QuestionItem()
                {
                    CauHoiId = item.CauHoiId,
                    NoiDungCauHoi = item.NoiDung,
                    DapAns = _context.DapAns.Where(m => m.CauHoiId == item.CauHoiId).ToList() ?? new List<DapAn>()
                });
            }
            return View(avm);
        }

        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var baiLams = _context.BaiLams.Where(m => m.UserId == userId).ToList();
            ViewData["BaiLams"] = baiLams;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Submit(AttendingViewModel vm)
        {
            await Task.Yield();
            return View();
        }
    }
}