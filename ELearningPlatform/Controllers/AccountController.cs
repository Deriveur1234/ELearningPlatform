﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ELearningPlatform.Data;
using ELearningPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ELearningPlatform.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly ELearningPlatformContext _context;

        public AccountController(ELearningPlatformContext context)
        {
            _context = context;

            
            if (_context.User.Count() == 0)
            {
                // Create a new User if collection is empty,
                // which means you can't delete all users.
                _context.User.Add(new User { Email = "loicburnand@gmail.com", IsConfirmed = true, IdCode = 3 });
                _context.SaveChanges();
            }
            UsersData.GetAllUser(_context);
        }

        [HttpGet("{id=0}")]
        public async Task<IActionResult> GetUser(int id)
        {
            UsersData.GetAllUser(_context);

            return NoContent();
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [Route("Login")]
        [HttpPost("{Username}")]
        public async Task<IActionResult> LoginUser(string Username, string Password)
        {
            return View("Index");
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
