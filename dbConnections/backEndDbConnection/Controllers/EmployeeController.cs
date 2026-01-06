using backEndDbConnection.Data;
using backEndDbConnection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace backEndDbConnection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IDatabase _cache;

        public EmployeeController(AppDbContext db, IConnectionMultiplexer redis)
        {
            _db = db;
            _cache = redis.GetDatabase();
        }

        // GET api/employee
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            const string cacheKey = "employees:all";

            var cached = await _cache.StringGetAsync(cacheKey);
            if (cached.HasValue)
            {
                var data = JsonSerializer.Deserialize<List<Employee>>(cached);
                return Ok(data);
            }

            var employees = await _db.Employees.ToListAsync();

            await _cache.StringSetAsync(
                cacheKey,
                JsonSerializer.Serialize(employees),
                TimeSpan.FromMinutes(5)
            );

            return Ok(employees);
        }

        // GET api/employee/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _db.Employees.FindAsync(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        // POST api/employee
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            _db.Employees.Add(employee);
            await _db.SaveChangesAsync();
            await _cache.KeyDeleteAsync("employees:all");
            return Ok(employee);
        }

        // PUT api/employee/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Employee employee)
        {
            var existing = await _db.Employees.FindAsync(id);
            if (existing == null)
                return NotFound();

            existing.Name = employee.Name;
            existing.Title = employee.Title;
            existing.Country = employee.Country;
            existing.Phone = employee.Phone;
            existing.ManagerId = employee.ManagerId;
            existing.ImgId = employee.ImgId;
            existing.Gender = employee.Gender;
            existing.Created_At = employee.Created_At;

            await _db.SaveChangesAsync();
            await _cache.KeyDeleteAsync("employees:all");
            return Ok(existing);
        }

        // DELETE api/employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _db.Employees.FindAsync(id);
            if (item == null)
                return NotFound();

            _db.Employees.Remove(item);
            await _db.SaveChangesAsync();
            await _cache.KeyDeleteAsync("employees:all");
            return Ok();
        }
    }
}
