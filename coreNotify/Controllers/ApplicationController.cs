using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using coreNotify.Models;
using coreNotify.Data;

namespace coreNotify.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Application
        public async Task<IActionResult> Index()
        {
              return _context.Application != null ? 
                          View(await _context.Application.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Application'  is null.");
        }

        // GET: Application/Messages/5
        public async Task<IActionResult> Messages(int? id)
        {
            if (id == null || _context.Application == null)
            {
                return NotFound();
            }

            var application = await _context.Application
                .FirstOrDefaultAsync(m => m.AppId == id);
            if (application == null)
            {
                return NotFound();
            }
            ViewBag.Application = application.DisplayName;
            ViewBag.AppId = application.AppId;
            IEnumerable<Message> messages = _context.Messages;
            List<Message> shortened = new();
            foreach (Message message in messages)
            {
                const int MaxLength = 100;
                List<char> truncated = new();
                int length = message.Content.Length;
                if (message.Content.Length > MaxLength)
                {
                    length = MaxLength;
                }
                char[] explode = message.Content.ToCharArray();
                for (int i = 0; i < length; i++)
                {
                    truncated.Add(explode[i]);
                    // add trailing dots to truncated string
                    if (i == MaxLength - 1)
                    {
                        truncated.Add('.');
                        truncated.Add('.');
                        truncated.Add('.');
                    }
                }
                string content = new(truncated.ToArray());
                var msg = new Message
                {
                    Id = message.Id,
                    Title = message.Title,
                    AppId = message.AppId,
                    Content = content
                };
                if (msg.AppId == id)
                {
                    shortened.Add(msg);
                }
            }

            return View(shortened);
        }

        // GET: Application/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Application/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AppName,DisplayName")] Application application)
        {
            if (ModelState.IsValid)
            {
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(application);
        }

        // GET: Application/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Application == null)
            {
                return NotFound();
            }

            var application = await _context.Application.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }
            return View(application);
        }

        // POST: Application/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppId,AppName,DisplayName")] Application application)
        {
            if (id != application.AppId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(application);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(application.AppId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(application);
        }

        // GET: Application/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Application == null)
            {
                return NotFound();
            }

            var application = await _context.Application
                .FirstOrDefaultAsync(m => m.AppId == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Application/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Application == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Application'  is null.");
            }
            var application = await _context.Application.FindAsync(id);
            if (application != null)
            {
                _context.Application.Remove(application);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationExists(int id)
        {
          return (_context.Application?.Any(e => e.AppId == id)).GetValueOrDefault();
        }
    }
}
