using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Quiz.Data;
using Quiz.Models;
using Quiz.Models.DeThiViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Controllers
{
    [Authorize(Roles = "Admin, Teacher")]
    public class DeThisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeThisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DeThis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DeThis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeThiId,Ten,ThoiGian")] DeThi deThi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deThi);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(deThi);
        }

        // GET: DeThis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deThi = await _context.DeThis.SingleOrDefaultAsync(m => m.DeThiId == id);
            if (deThi == null)
            {
                return NotFound();
            }

            return View(deThi);
        }

        // POST: DeThis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deThi = await _context.DeThis.SingleOrDefaultAsync(m => m.DeThiId == id);
            _context.DeThis.Remove(deThi);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteQuestion(int? id, int? cauHoi)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (cauHoi == null)
            {
                return NotFound();
            }

            var cauHoiDeThi = await _context.CauHoiDeThis.SingleOrDefaultAsync(m => m.DeThiId == id && m.CauHoiId == cauHoi);
            if (cauHoiDeThi == null)
            {
                return NotFound();
            }

            _context.Remove(cauHoiDeThi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = id.Value });
        }

        // GET: DeThis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deThi = await _context.DeThis.SingleOrDefaultAsync(m => m.DeThiId == id);
            if (deThi == null)
            {
                return NotFound();
            }

            var CauHois = await _context.CauHoiDeThis.Include(m => m.CauHoi)
                         .Where(m => m.DeThiId == id.Value)
                         .Select(m => m.CauHoi)
                         .ToListAsync();
            ViewData[nameof(CauHois)] = CauHois;
            return View(deThi);
        }

        // GET: DeThis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deThi = await _context.DeThis.SingleOrDefaultAsync(m => m.DeThiId == id);
            if (deThi == null)
            {
                return NotFound();
            }
            return View(deThi);
        }

        // POST: DeThis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeThiId,Ten")] DeThi deThi)
        {
            if (id != deThi.DeThiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deThi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeThiExists(deThi.DeThiId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(deThi);
        }

        public IActionResult FillCauHois(int id)
        {
            var cauHois = _context.Nhoms.Include(m => m.CauHois).SingleOrDefault(m => m.NhomId == id)?.CauHois;
            cauHois?.ForEach(m => m.Nhom = null);
            return Json(cauHois);
        }

        // GET: DeThis
        public async Task<IActionResult> Index()
        {
            return View(await _context.DeThis.ToListAsync());
        }

        public async Task<IActionResult> PickQuestion(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deThi = await _context.DeThis.SingleOrDefaultAsync(m => m.DeThiId == id);
            if (deThi == null)
            {
                return NotFound();
            }

            ViewData["Nhoms"] = _context.Nhoms
                .Select(m => new SelectListItem()
                {
                    Value = m.NhomId.ToString(),
                    Text = m.Ten
                });
            ViewData["CauHois"] = _context.Nhoms.Include(m => m.CauHois).FirstOrDefault()?.CauHois;
            ViewData["DeThiName"] = deThi.Ten;
            return View(new PickQuestionViewModel()
            {
                DeThiId = id.Value
            });
        }

        [HttpPost]
        public async Task<IActionResult> PickQuestion(PickQuestionViewModel vm)
        {
            var cauHoiDeThi = new CauHoiDeThi()
            {
                CauHoiId = vm.CauHoiId,
                DeThiId = vm.DeThiId
            };
            _context.Add(cauHoiDeThi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = vm.DeThiId });
        }

        private bool DeThiExists(int id)
        {
            return _context.DeThis.Any(e => e.DeThiId == id);
        }
    }
}