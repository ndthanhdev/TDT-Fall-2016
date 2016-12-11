using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quiz.Data;
using Quiz.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Controllers
{
    public class NhomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NhomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Nhoms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Nhoms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NhomId,Ten")] Nhom nhom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nhom);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(nhom);
        }

        // GET: Nhoms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhom = await _context.Nhoms.SingleOrDefaultAsync(m => m.NhomId == id);
            if (nhom == null)
            {
                return NotFound();
            }

            return View(nhom);
        }

        // POST: Nhoms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nhom = await _context.Nhoms.SingleOrDefaultAsync(m => m.NhomId == id);
            _context.Nhoms.Remove(nhom);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Nhoms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhom = await _context.Nhoms.Include(n => n.CauHois).SingleOrDefaultAsync(m => m.NhomId == id);
            if (nhom == null)
            {
                return NotFound();
            }

            var vm = new Models.NhomViewModels.DetailViewModel()
            {
                NhomId = nhom.NhomId,
                TenNhom = nhom.Ten,
                CauHois = nhom.CauHois
            };

            return View(vm);
        }

        // GET: Nhoms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhom = await _context.Nhoms.SingleOrDefaultAsync(m => m.NhomId == id);
            if (nhom == null)
            {
                return NotFound();
            }
            return View(nhom);
        }

        // POST: Nhoms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NhomId,Ten")] Nhom nhom)
        {
            if (id != nhom.NhomId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhomExists(nhom.NhomId))
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
            return View(nhom);
        }

        // GET: Nhoms
        public async Task<IActionResult> Index()
        {
            return View(await _context.Nhoms.OrderBy(n => n.Ten).ToListAsync());
        }

        private bool NhomExists(int id)
        {
            return _context.Nhoms.Any(e => e.NhomId == id);
        }
    }
}