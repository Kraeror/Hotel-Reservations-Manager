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
    public class ReservationsController : Controller
    {
        private readonly SQLContext _context;

        public ReservationsController(SQLContext context)
        {
            _context = context;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var obj = new Models.Models
            {
                Reservations = await _context.Reservations.ToListAsync(),
                Rooms = await _context.Rooms.ToListAsync(),
                Clients = await _context.Clients.ToListAsync(),
            };
            return View(obj);
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var reservationsModel = await _context.Reservations
                .FirstOrDefaultAsync(m => m.ID == id);
            if (reservationsModel == null)
            {
                return NotFound();
            }

            return View(reservationsModel);
        }
        
        // GET: Reservations/Create
        public async Task<IActionResult> Create()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var obj = new Models.Models
            {
                Reservations = await _context.Reservations.ToListAsync(),
                Clients = await _context.Clients.ToListAsync(),
                Rooms = await _context.Rooms.ToListAsync()
            };
            return View(obj);
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.Models models)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                var obj = new Models.Models
                {
                    Reservations = await _context.Reservations.ToListAsync(),
                    Clients = await _context.Clients.ToListAsync(),
                    Rooms = await _context.Rooms.ToListAsync()
                };
                var cnt = 0;
                foreach (var client in models.Reservations2.ClientsIDs)
                {
                    if (client == 0)
                    {
                        cnt++;
                    }
                }
                if(_context.Reservations.Where(rid => rid.foreignID == models.Reservations2.foreignID).Any())
                {
                    ViewData["error"] = "Стаята, която сте избрали не е свободна!";
                    return View(obj);
                }
                string allClients = "";
                foreach (var item in _context.Reservations)
                {
                    var queryy = _context.Reservations.Where(rid => rid.ID == item.ID).FirstOrDefault();
                    allClients += queryy.ClientsInTheRoom + ", ";
                }
                var clientsArr = allClients.Split(", ", StringSplitOptions.RemoveEmptyEntries);
                var errorAppear = false;
                var intArray = Array.ConvertAll(clientsArr, (s) => { return int.Parse(s); });
                if (intArray.Intersect(models.Reservations2.ClientsIDs.ToArray()).Any())
                {
                    errorAppear = true;
                }
                if(errorAppear)
                {
                    ViewData["error"] = "Някой от клиентите, които сте избрали вече има резервация!";
                    return View(obj);
                }
                if (cnt == models.Reservations2.ClientsIDs.Count)
                {
                    ViewData["error"] = "Моля, изберете поне един клиент за резервацията.";
                    return View(obj);
                }
                if (models.Reservations2.ClientsIDs.Where(c => c != 0).Count() != models.Reservations2.ClientsIDs.Where(c => c != 0).Distinct().Count())
                {
                    ViewData["error"] = "Избрали сте два или повече еднакви клиенти!";
                    return View(obj);
                }
                var dateDiff = models.Reservations2.ReleaseDate - models.Reservations2.AccommodationDate;
                if (dateDiff.TotalDays <= 0)
                {
                    ViewData["error"] = "Датата на напускане трябва да бъде след датата на резервация!";
                    return View(obj);
                }
                if (models.Reservations2.AccommodationDate < DateTime.UtcNow.Date)
                {
                    ViewData["error"] = "Датата за резервация трябва да е след вчерашната!";
                    return View(obj);
                }
                models.Reservations2.ReservatedBy = HttpContext.Session.GetString("Username");
                models.Reservations2.ClientsInTheRoom = String.Join(", ", models.Reservations2.ClientsIDs.ToArray()).Replace(", 0", "").Replace(" 0,", "");
                if (models.Reservations2.ClientsInTheRoom[0] == '0')
                {
                    models.Reservations2.ClientsInTheRoom = models.Reservations2.ClientsInTheRoom.Remove(0, 3);
                }
                var roomsz = _context.Rooms.Where(rid => rid.ID == models.Reservations2.foreignID).FirstOrDefault();
                decimal mainPrice = 0;
                decimal extrasPrice = 0;
                foreach (var item in models.Reservations2.ClientsIDs)
                {
                    if (item != 0)
                    {
                        var clientsz = _context.Clients.Where(cid => cid.ID == item).FirstOrDefault();
                        if (clientsz.isAdoult)
                        {
                            mainPrice += (decimal)roomsz.AdoultPrice * (decimal)dateDiff.TotalDays;
                        }
                        else
                        {
                            mainPrice += (decimal)roomsz.KidsPrice * (decimal)dateDiff.TotalDays;
                        }
                    }
                }
                if(models.Reservations2.isAllInclusive)
                {
                    extrasPrice += 30;
                }
                if (models.Reservations2.isBreakfastIncluded)
                {
                    extrasPrice += 8;
                }
                var finalPrice = mainPrice + extrasPrice;
                models.Reservations2.Price = (decimal)finalPrice * (decimal)dateDiff.TotalDays;
                models.Reservations2.ReservatedBy = HttpContext.Session.GetString("Username");
                _context.Rooms.Where(rid => rid.ID == models.Reservations2.foreignID).FirstOrDefault().isFree = false;
                _context.Add(models.Reservations2);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); 
            }
            return View(models);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id, Models.Models models)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }
            models.Reservations2 = await _context.Reservations.FindAsync(id);
            models.Reservations = await _context.Reservations.ToListAsync();
            models.Clients = await _context.Clients.ToListAsync();
            models.Rooms = await _context.Rooms.ToListAsync();
            if (models.Reservations2 == null)
            {
                return NotFound();
            }
            return View(models);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Models.Models models)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id != models.Reservations2.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var dateDiff = models.Reservations2.ReleaseDate - models.Reservations2.AccommodationDate;
                var obj = new Models.Models
                {
                    Reservations = models.Reservations,
                    Clients = await _context.Clients.ToListAsync(),
                    Rooms = await _context.Rooms.ToListAsync()
                };
                try
                {
                    var cnt = 0;
                    foreach (var client in models.Reservations2.ClientsIDs)
                    {
                        if (client == 0)
                        {
                            cnt++;
                        }
                    }
                    
                    if (cnt != models.Reservations2.ClientsIDs.Count)
                    {
                        models.Reservations2.ClientsInTheRoom = String.Join(", ", models.Reservations2.ClientsIDs.ToArray()).Replace(", 0", "").Replace(" 0,", "");
                        if (models.Reservations2.ClientsInTheRoom[0] == '0')
                        {
                            models.Reservations2.ClientsInTheRoom = models.Reservations2.ClientsInTheRoom.Remove(0, 3);
                        }
                        _context.Entry(models.Reservations2).Property(X => X.ClientsInTheRoom).IsModified = true;
                    }
                    if (models.Reservations2.ClientsIDs.Where(c => c != 0).Count() != models.Reservations2.ClientsIDs.Where(c => c != 0).Distinct().Count())
                    {
                        ViewData["error"] = "Избрали сте два или повече еднакви клиенти!";
                        return View(obj);
                    }
                    
                    if (dateDiff.TotalDays <= 0)
                    {
                        ViewData["error"] = "Датата на напускане трябва да бъде след датата на резервация!";
                        return View(obj);
                    }
                    if (models.Reservations2.AccommodationDate < DateTime.UtcNow.Date)
                    {
                        ViewData["error"] = "Датата за резервация трябва да е след вчерашната!";
                        return View(obj);
                    }
                    
                    if (cnt == models.Reservations2.ClientsIDs.Count)
                    {
                        if (_context.Rooms.Where(rid => rid.ID == models.Reservations2.foreignID).FirstOrDefault().Capacity < models.Reservations2.ClientsInTheRoom.Trim(',').Split(", ").Length)
                        {
                            ViewData["error"] = "Избрали сте стая с по-малък капацитет от необходимия. Моля, изберете клиентите, които ще я посещават!";
                            return View(obj);
                        }
                    }
                    var roomsz = _context.Rooms.Where(rid => rid.ID == models.Reservations2.foreignID).FirstOrDefault();
                    decimal mainPrice = 0;
                    decimal extrasPrice = 0;
                    foreach (var item in models.Reservations2.ClientsIDs)
                    {
                        if (item != 0)
                        {
                            var clientsz = _context.Clients.Where(cid => cid.ID == item).FirstOrDefault();
                            if (clientsz.isAdoult)
                            {
                                mainPrice += (decimal)roomsz.AdoultPrice * (decimal)dateDiff.TotalDays;
                            }
                            else
                            {
                                mainPrice += (decimal)roomsz.KidsPrice * (decimal)dateDiff.TotalDays;
                            }
                        }
                    }
                    if (models.Reservations2.isAllInclusive)
                    {
                        extrasPrice += 30;
                    }
                    if (models.Reservations2.isBreakfastIncluded)
                    {
                        extrasPrice += 8;
                    }
                    var finalPrice = mainPrice + extrasPrice;
                    models.Reservations2.Price = (decimal)finalPrice * (decimal)dateDiff.TotalDays;
                    _context.Entry(models.Reservations2).Property(X => X.foreignID).IsModified = true;
                    _context.Entry(models.Reservations2).Property(X => X.AccommodationDate).IsModified = true;
                    _context.Entry(models.Reservations2).Property(X => X.ReleaseDate).IsModified = true;
                    _context.Entry(models.Reservations2).Property(X => X.Price).IsModified = true;
                    _context.Entry(models.Reservations2).Property(X => X.isBreakfastIncluded).IsModified = true;
                    _context.Entry(models.Reservations2).Property(X => X.isAllInclusive).IsModified = true;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationsModelExists(models.Reservations2.ID))
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
            return View(models);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }
            var reservationsModel = await _context.Reservations
                .FirstOrDefaultAsync(m => m.ID == id);
            if (reservationsModel == null)
            {
                return NotFound();
            }

            return View(reservationsModel);
        }
        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var reservationsModel = await _context.Reservations.FindAsync(id);
            var query = _context.Reservations.Where(rid => rid.ID == id).FirstOrDefault();
            _context.Rooms.Where(rid => rid.ID == query.foreignID).FirstOrDefault().isFree = true;
            _context.Clients.Where(cid => cid.ID == query.foreignID).FirstOrDefault().isBusy = false;
            _context.Reservations.Remove(reservationsModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationsModelExists(int id)
        {
            return _context.Reservations.Any(e => e.ID == id);
        }
    }
}
