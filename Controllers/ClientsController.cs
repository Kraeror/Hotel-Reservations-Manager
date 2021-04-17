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
    public class ClientsController : Controller
    {
        private readonly SQLContext _context;

        public ClientsController(SQLContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(await _context.Clients.ToListAsync());
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,familyName,phoneNumber,Email,isAdoult")] ClientsModel clientsModel)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                var clientsObject = _context.Clients.Where(u => u.Email.Equals(clientsModel.Email)).FirstOrDefault();
                if (clientsObject == null)
                {
                    clientsModel.isBusy = false;
                    _context.Add(clientsModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else ViewData["CreateClientError"] = "Вече съществува клиент с посочената електронна поща.";
            }
            return View(clientsModel);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var clientsModel = await _context.Clients.FindAsync(id);
            if (clientsModel == null)
            {
                return NotFound();
            }
            return View(clientsModel);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,familyName,phoneNumber,Email,isAdoult")] ClientsModel clientsModel)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id != clientsModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientsModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientsModelExists(clientsModel.ID))
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
            return View(clientsModel);
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var clientsModel = await _context.Clients.FindAsync(id);
            if(clientsModel != null)
            {
                if (_context.Reservations.Where(rid => rid.foreignID == id).FirstOrDefault() == null)
                {
                    _context.Clients.Remove(clientsModel);
                }
                else
                {
                    ViewData["error"] = "Не можете да изтривате клиенти, които участват в резервация!";
                    return RedirectToAction("Index", "Clients");
                }
            }
            return RedirectToAction("Index", "Clients");
        }

        private bool ClientsModelExists(int id)
        {
            return _context.Clients.Any(e => e.ID == id);
        }
    }
}