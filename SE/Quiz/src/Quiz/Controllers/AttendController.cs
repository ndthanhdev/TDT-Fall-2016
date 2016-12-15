using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Student, Admin, Teacher")]
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

            var vm = new IndexViewModel();
            vm.BaiLamViewItems = new List<BaiLamViewItem>();
            var baiLams = _context.BaiLams
                .Include(m => m.DeThi)
                .Include(m => m.ChiTietBaiLams)
                .Where(m => m.UserId == userId)
                .ToList();
            foreach (var bl in baiLams)
            {
                var item = new BaiLamViewItem()
                {
                    BaiLamId = bl.BaiLamId,
                    TenDeThi = bl.DeThi.Ten,
                };
                var chiTietBaiLams = _context.ChiTietBaiLams
                    .Include(m => m.DapAn)
                    .Where(m => bl.ChiTietBaiLams.Select(ctbl => ctbl.ChiTietBaiLamId)
                    .Contains(m.ChiTietBaiLamId));
                item.Diem = chiTietBaiLams.Count(m => m.DapAn.IsTrue);
                item.SoCau = _context.CauHoiDeThis.Count(m => m.DeThiId == bl.DeThiId);
                vm.BaiLamViewItems.Add(item);
            }
            ViewData["BaiLams"] = baiLams;
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Submit(AttendingViewModel vm)
        {
            await Task.Yield();
            var userId = _userManager.GetUserId(HttpContext.User);
            var baiLam = new BaiLam();
            baiLam.DeThiId = vm.DeThiId;
            baiLam.UserId = userId;
            _context.BaiLams.Add(baiLam);
            baiLam.ChiTietBaiLams = new List<ChiTietBaiLam>();
            await _context.SaveChangesAsync();
            foreach (var q in vm.Questions)
            {
                if (q.SelectedDapAn == 0)
                    continue;
                var ctbl = new ChiTietBaiLam()
                {
                    DapAnId = q.SelectedDapAn
                };
                _context.ChiTietBaiLams.Add(ctbl);
                await _context.SaveChangesAsync();
                baiLam.ChiTietBaiLams.Add(ctbl);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}