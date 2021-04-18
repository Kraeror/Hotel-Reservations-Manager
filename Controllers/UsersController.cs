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
    public class UsersController : Controller
    {
        private readonly SQLContext _context;

        public UsersController(SQLContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Username") != "mainadmin")
            {
                return RedirectToAction("Index", "Home");
            }
            foreach (var item in _context.Users)
            {
                var usersObj = _context.Users.Where(uid => uid.ID == item.ID).FirstOrDefault();
                if (DateTime.Now.Date >= usersObj.releaseDate.Value.Date && usersObj.releaseDate.Value.Year > 2000 && usersObj.isActive == true)
                {
                    usersObj.isActive = false;
                }
            }
            await _context.SaveChangesAsync();
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Username") != "mainadmin")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Email");
            HttpContext.Session.Remove("Username");
            HttpContext.Session.Remove("Name");
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Username,Password,Name,Surname,familyName,IDNumber,phoneNumber,Email,AppointmentDate,isActive,releaseDate")] UsersModel usersModel)
        {
            if (HttpContext.Session.GetString("Username") != "mainadmin")
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                var userObject = _context.Users.Where(u => u.Username.Equals(usersModel.Username) || u.Email.Equals(usersModel.Email) || u.IDNumber.Equals(usersModel.IDNumber)).FirstOrDefault();
                if(userObject == null)
                {
                    if (usersModel.Username != "mainadmin")
                    {
                        if (usersModel.releaseDate > DateTime.Now.Date)
                        {
                            usersModel.AppointmentDate = DateTime.Now.Date;
                            usersModel.isActive = true;
                            if (usersModel.releaseDate == null)
                            {
                                usersModel.releaseDate = new DateTime(2000, 12, 31);
                            }
                            _context.Add(usersModel);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            ViewData["CreateUserError"] = "Датата на напускане трябва да е след днешната!";
                        }
                    }
                    else
                    {
                        ViewData["CreateUserError"] = "Потребителското име е заето от администратора на сайта!";
                    }
                }
                else
                {
                    ViewData["CreateUserError"] = "Вече съществува потребител с такова потребителско име, електронна поща или Единен Граждански Номер(ЕГН).";
                }
            }
            return View(usersModel);
        }

        // GET: Users/Edit/5
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

            var usersModel = await _context.Users.FindAsync(id);
            if (usersModel == null)
            {
                return NotFound();
            }
            return View(usersModel);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Username,Password,Name,Surname,familyName,IDNumber,phoneNumber,Email,AppointmentDate,isActive,releaseDate")] UsersModel usersModel)
        {
            if (HttpContext.Session.GetString("Username") != "mainadmin")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id != usersModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usersModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersModelExists(usersModel.ID))
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
            return View(usersModel);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (HttpContext.Session.GetString("Username") != "mainadmin")
            {
                return RedirectToAction("Index", "Home");
            }
            var usersModel = await _context.Users.FindAsync(id);
            if (usersModel != null)
            {
                _context.Users.Remove(usersModel);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Users");
        }

        private bool UsersModelExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.Session.GetString("Username") != null)
                {
                    return RedirectToAction("Index", "Home");
                }
                foreach (var item in _context.Users)
                {
                    var usersObj = _context.Users.Where(uid => uid.ID == item.ID).FirstOrDefault();
                    if (DateTime.Now.Date >= usersObj.releaseDate.Value.Date && usersObj.releaseDate.Value.Year > 2000 && usersObj.isActive == true)
                    {
                        usersObj.isActive = false;
                    }
                }
                _context.SaveChanges();
                if (loginModel.Username == "mainadmin" && loginModel.Password == "1")
                {
                    HttpContext.Session.SetString("Username", "mainadmin");
                    HttpContext.Session.SetString("Name", "Администратор");
                    return RedirectToAction("Index", "Home");
                }
                var userObject = _context.Users.Where(u => u.Username.Equals(loginModel.Username) && u.Password.Equals(loginModel.Password)).FirstOrDefault();
                if (userObject != null)
                {
                    if (_context.Users.Where(uid => uid.Username == loginModel.Username).FirstOrDefault().isActive == true)
                    {
                        HttpContext.Session.SetString("ID", userObject.ID.ToString());
                        HttpContext.Session.SetString("Email", userObject.Email);
                        HttpContext.Session.SetString("Username", userObject.Username);
                        HttpContext.Session.SetString("Name", userObject.Name);
                        return RedirectToAction("Index", "Home");
                    }
                    else ViewData["loginError"] = "Акаунтът Ви е неактивен!"; 
                }
                else ViewData["loginError"] = "Не беше открит потребител с предоставеното потребителско име и парола!";
            }
            return View();
        }
    }
}
