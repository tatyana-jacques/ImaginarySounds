using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicBankAPI.Context;
using MusicBankAPI.Models;

namespace MusicBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusTablesController : ControllerBase
    {
        private readonly MusicBankContext _context;

        public StatusTablesController(MusicBankContext context)
        {
            _context = context;
        }

        // GET: api/StatusTables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusTable>>> GetStatusTable()
        {
          if (_context.StatusTable == null)
          {
              return NotFound();
          }
            return await _context.StatusTable.ToListAsync();
        }

        // GET: api/StatusTables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StatusTable>> GetStatusTable(int id)
        {
          if (_context.StatusTable == null)
          {
              return NotFound();
          }
            var statusTable = await _context.StatusTable.FindAsync(id);

            if (statusTable == null)
            {
                return NotFound();
            }

            return statusTable;
        }

        // PUT: api/StatusTables/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatusTable(int id, StatusTable statusTable)
        {
            if (id != statusTable.Id)
            {
                return BadRequest();
            }

            _context.Entry(statusTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusTableExists(id))
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

        // POST: api/StatusTables
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StatusTable>> PostStatusTable(StatusTable statusTable)
        {
          if (_context.StatusTable == null)
          {
              return Problem("Entity set 'MusicBankContext.StatusTable'  is null.");
          }
            _context.StatusTable.Add(statusTable);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStatusTable", new { id = statusTable.Id }, statusTable);
        }

        // DELETE: api/StatusTables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatusTable(int id)
        {
            if (_context.StatusTable == null)
            {
                return NotFound();
            }
            var statusTable = await _context.StatusTable.FindAsync(id);
            if (statusTable == null)
            {
                return NotFound();
            }

            _context.StatusTable.Remove(statusTable);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StatusTableExists(int id)
        {
            return (_context.StatusTable?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
