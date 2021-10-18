using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRS_Server.Models;

namespace PRS_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly PRSDbContext _context;

        public VendorsController(PRSDbContext context)
        {
            _context = context;
        }

        // GET: api/Vendors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendor>>> GetVendors()
        {
            return await _context.Vendors.ToListAsync();
        }

        // GET: api/Vendors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vendor>> GetVendor(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);

            if (vendor == null)
            {
                return NotFound();
            }

            return vendor;
        }

        [HttpGet("vendor/{Id}")]
        public async Task<IActionResult> PurchaseOrder(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor == null)
            {
                NotFound();
            }
            var orderVendor = new List<PoLines>();
            orderVendor = await (from r in _context.Requests  //List of instances in vendororders that met the criteria of Approved
                                 join rl in _context.RequestLines
                                 on r.Id equals rl.RequestId
                                 join u in _context.Users
                                 on r.UserId equals u.Id
                                 join p in _context.Products
                                 on rl.ProductId equals p.Id
                                 join v in _context.Vendors
                                 on p.VendorId equals v.Id
                                 where r.Status == "APPROVED" && v.Id == id
                                 select new PoLines(p, rl.Quantity))     
                                .ToListAsync();
            var Dict = new Dictionary<string, PoLines>();
            foreach (var i in orderVendor)
            {
                if (Dict.ContainsKey(i.Product.PartNbr) == true)
                {
                    Dict[i.Product.PartNbr].Quantity += i.Quantity;
                    Dict[i.Product.PartNbr].LineTotal += i.LineTotal;
                }
                else
                {
                    Dict.Add(i.Product.PartNbr, i);
                }
            }
            var PoReq = new PoRequests();
            foreach (var i in Dict)
            {
                PoReq.Vendor = vendor;
                PoReq.Header.Add(i.Value);
                var Total = 0m;
                foreach (var x in PoReq.Header)
                {
                    Total = Total + x.LineTotal;
                }
                PoReq.GrandTotal = Total;
            }
            return Ok(PoReq);
        }
        // PUT: api/Vendors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendor(int id, Vendor vendor)
        {
            if (id != vendor.Id)
            {
                return BadRequest();
            }

            _context.Entry(vendor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorExists(id))
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

        // POST: api/Vendors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vendor>> PostVendor(Vendor vendor)
        {
            _context.Vendors.Add(vendor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendor", new { id = vendor.Id }, vendor);
        }

        // DELETE: api/Vendors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendor(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }

            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VendorExists(int id)
        {
            return _context.Vendors.Any(e => e.Id == id);
        }
    }
}
