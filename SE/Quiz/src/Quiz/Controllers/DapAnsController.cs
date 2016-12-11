using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Quiz.Data;
using Quiz.Models;
using Quiz.Models.DapAnViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Controllers
{
    public class DapAnsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DapAnsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DapAns
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.DapAns.Include(d => d.CauHoi);
        //    return View(await applicationDbContext.ToListAsync());
        //}

        // GET: DapAns/Create
        public async Task<IActionResult> Create(int id)
        {
            var cauHoi = await _context.CauHois.SingleOrDefaultAsync(m => m.CauHoiId == id);
            var vm = new Models.DapAnViewModels.CreateViewModel()
            {
                CauHoiId = cauHoi.CauHoiId,
                NoiDungCauHoi = cauHoi.NoiDung
            };
            return View(vm);
        }

        // POST: DapAns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CauHoiId,IsTrue,NoiDungDapAn")] CreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(new DapAn()
                {
                    CauHoiId = vm.CauHoiId,
                    NoiDung = vm.NoiDungDapAn,
                    IsTrue = vm.IsTrue
                });
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "CauHois", new { id = vm.CauHoiId });
            }
            return View(vm);
        }

        // GET: DapAns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dapAn = await _context.DapAns.SingleOrDefaultAsync(m => m.DapAnId == id);
            if (dapAn == null)
            {
                return NotFound();
            }

            return View(dapAn);
        }

        // POST: DapAns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dapAn = await _context.DapAns.SingleOrDefaultAsync(m => m.DapAnId == id);
            _context.DapAns.Remove(dapAn);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "CauHois", new { id = dapAn.CauHoiId });
        }

        // GET: DapAns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dapAn = await _context.DapAns.SingleOrDefaultAsync(m => m.DapAnId == id);
            if (dapAn == null)
            {
                return NotFound();
            }

            return View(dapAn);
        }

        // GET: DapAns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dapAn = await _context.DapAns.SingleOrDefaultAsync(m => m.DapAnId == id);
            if (dapAn == null)
            {
                return NotFound();
            }
            ViewData["CauHoiId"] = new SelectList(_context.CauHois, "CauHoiId", "NoiDung", dapAn.CauHoiId);
            return View(dapAn);
        }

        // POST: DapAns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DapAnId,CauHoiId,IsTrue,NoiDung")] DapAn dapAn)
        {
            if (id != dapAn.DapAnId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dapAn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DapAnExists(dapAn.DapAnId))
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
            ViewData["CauHoiId"] = new SelectList(_context.CauHois, "CauHoiId", "NoiDung", dapAn.CauHoiId);
            return View(dapAn);
        }

        private bool DapAnExists(int id)
        {
            return _context.DapAns.Any(e => e.DapAnId == id);
        }
    }
}