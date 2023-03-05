using coreNotify.Data;
using coreNotify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace coreNotify.Controllers
{
    public class MessageController : Controller
    {
        private readonly ApplicationDbContext _db;
        public MessageController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Message> messages = _db.Messages;
            List<Message> shortened = new();
            foreach(Message message in messages) {
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
                shortened.Add(msg);
            }
            return View(shortened);
        }
        // GET
        public IActionResult Create(int? Appid)
        {
            int AppId = 1;
            if (Appid != null)
            {
                var app = _db.Application.Find(Appid);
                if (app != null)
                {
                    AppId = (int)Appid;
                }
            }
            List<Application> appList = _db.Application.ToList();
            ViewBag.appList = new SelectList(appList, "AppId", "DisplayName", AppId);
            return View();
        }
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Message obj)
        {
            List<Application> appList = _db.Application.ToList();
            ViewBag.appList = new SelectList(appList, "AppId", "DisplayName");
            if (ModelState.IsValid)
            {
                _db.Messages.Add(obj);
                _db.SaveChanges();
                TempData.Add("messageState", "Message Created");
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        // GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) {
                return NotFound();
            }
            var message = _db.Messages.Find(id);
            if (message == null)
            {
                return NotFound();
            }

            List<Application> appList = _db.Application.ToList();
            int i = 1;
            foreach(Application app in appList)
            {
                if (app.AppId == message.AppId) {
                    break;
                }
                i++;
            }
            SelectList bagList = new(appList, "AppId", "DisplayName", i);
            ViewBag.appList = bagList;
            return View(message);
        }
        
        // UPDATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Message obj)
        {
            List<Application> appList = _db.Application.ToList();
            ViewBag.App = new SelectList(appList, "AppId", "DisplayName");
            if (ModelState.IsValid)
            {
                _db.Messages.Update(obj);
                _db.SaveChanges();
                TempData.Add("messageState", "Message has been edited");
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        // GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var message = _db.Messages.Find(id);
            if (message == null)
            {
                return NotFound();
            }
            ViewBag.message = message;
            return View(message);
        }
        // DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            
            var message = _db.Messages.Find(id);
            if (message == null)
            {
                return NotFound();
            }
            _db.Messages.Remove(message);
            _db.SaveChanges();
            TempData.Add("messageState", "Message Deleted");
            return RedirectToAction("Index");
        }
    }
}
