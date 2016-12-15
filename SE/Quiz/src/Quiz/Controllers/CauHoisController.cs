using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Quiz.Data;
using Quiz.Models;
using Quiz.Models.CauHoiViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Controllers
{
    [Authorize(Roles = "Admin, Teacher")]
    public class CauHoisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CauHoisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CauHois
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.CauHois.Include(c => c.Nhom);
        //    return View(await applicationDbContext.ToListAsync());
        //}

        // GET: CauHois/Create
        public IActionResult Create(int? id)
        {
            ViewData["NhomId"] = new SelectList(_context.Nhoms, "NhomId", "Ten");
            return View(new Models.CauHoiViewModels.CreateViewModel()
            {
                NhomId = id.HasValue ? id.Value : 0
            });
        }

        // POST: CauHois/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NhomId,NoiDungCauHoi")] CreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var cauHoi = new CauHoi()
                {
                    NoiDung = vm.NoiDungCauHoi,
                    NhomId = vm.NhomId
                };
                _context.Add(cauHoi);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Nhoms", new { id = cauHoi.NhomId });
            }
            ViewData["NhomId"] = new SelectList(_context.Nhoms, "NhomId", "Ten", vm.NhomId);
            return View(vm);
        }

        // GET: CauHois/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cauHoi = await _context.CauHois.SingleOrDefaultAsync(m => m.CauHoiId == id);
            if (cauHoi == null)
            {
                return NotFound();
            }

            return View(cauHoi);
        }

        // POST: CauHois/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cauHoi = await _context.CauHois.SingleOrDefaultAsync(m => m.CauHoiId == id);
            _context.CauHois.Remove(cauHoi);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Nhoms", new { id = cauHoi.NhomId });
        }

        // GET: CauHois/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cauHoi = await _context.CauHois.Include(m => m.DapAns).Include(m => m.Nhom).SingleOrDefaultAsync(m => m.CauHoiId == id);
            if (cauHoi == null)
            {
                return NotFound();
            }
            return View(cauHoi);
        }

        // GET: CauHois/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cauHoi = await _context.CauHois.SingleOrDefaultAsync(m => m.CauHoiId == id);
            if (cauHoi == null)
            {
                return NotFound();
            }
            ViewData["NhomId"] = new SelectList(_context.Nhoms, "NhomId", "Ten", cauHoi.NhomId);
            return View(cauHoi);
        }

        // POST: CauHois/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CauHoiId,NhomId,NoiDung")] CauHoi cauHoi)
        {
            if (id != cauHoi.CauHoiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cauHoi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CauHoiExists(cauHoi.CauHoiId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Nhoms", new { id = cauHoi.NhomId });
            }
            ViewData["NhomId"] = new SelectList(_context.Nhoms, "NhomId", "Ten", cauHoi.NhomId);
            return View(cauHoi);
        }

        private bool CauHoiExists(int id)
        {
            return _context.CauHois.Any(e => e.CauHoiId == id);
        }
    }
}