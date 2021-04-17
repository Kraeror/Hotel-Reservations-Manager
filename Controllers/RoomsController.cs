using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hotel_Reservations_Manager.Models;
using Microsoft.AspNetCore.Http;

namespace Hotel_Reservations_Manager.Controllers
{
    public class RoomsController : Controller
    {
        private readonly SQLContext _context;

        public RoomsController(SQLContext context)
        {
            _context = context;
        }

        // GET: Rooms
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Username") != "mainadmin")
            {
                return RedirectToAction("Index", "Home");
            }
            foreach (var item in _context.Reservations)
            {
                var resrvationsObj = _context.Reservations.Where(rid => rid.ID == item.ID).FirstOrDefault();
                if ((DateTime.Now.Date == resrvationsObj.ReleaseDate.Date && DateTime.Now.Hour >= 12.00) || DateTime.Now.Date > resrvationsObj.ReleaseDate.Date)
                {
                    _context.Rooms.Where(rid => rid.RoomID == resrvationsObj.RoomId).FirstOrDefault().isFree = true;
                }
            }
            await _context.SaveChangesAsync();
            return View(await _context.Rooms.ToListAsync());
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Username") != "mainadmin")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Capacity,Type,isFree,AdoultPrice,KidsPrice,RoomID")] RoomsModel roomsModel)
        {
            if (HttpContext.Session.GetString("Username") != "mainadmin")
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                if(_context.Rooms.Where(rid => rid.RoomID == roomsModel.RoomID).Any())
                {
                    ViewData["error"] = "Вече съществува стая с номера, който сте избрали!";
                    return View(roomsModel);
                }
                roomsModel.isFree = true;
                _context.Add(roomsModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roomsModel);
        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("Username") != "mainadmin")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var roomsModel = await _context.Rooms.FindAsync(id);
            if (roomsModel == null)
            {
                return NotFound();
            }
            return View(roomsModel);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RoomsModel roomsModel)
        {
            if (HttpContext.Session.GetString("Username") != "mainadmin")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id != roomsModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(roomsModel).Property(X => X.RoomID).IsModified = true;
                    _context.Entry(roomsModel).Property(X => X.AdoultPrice).IsModified = true;
                    _context.Entry(roomsModel).Property(X => X.KidsPrice).IsModified = true;
                    _context.Entry(roomsModel).Property(X => X.Type).IsModified = true;
                    _context.Entry(roomsModel).Property(X => X.Capacity).IsModified = true;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomsModelExists(roomsModel.ID))
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
            return View(roomsModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (HttpContext.Session.GetString("Username") != "mainadmin")
            {
                return RedirectToAction("Index", "Home");
            }
            var roomsModel = await _context.Rooms.FindAsync(id);
            _context.Rooms.Remove(roomsModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomsModelExists(int id)
        {
            return _context.Rooms.Any(e => e.ID == id);
        }
    }
}
