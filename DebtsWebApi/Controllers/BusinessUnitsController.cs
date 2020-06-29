using System.Collections.Generic;
using System.Threading.Tasks;
using DebtsWebApi.DAL;
using DebtsWebApi.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DebtsWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessUnitsController : ControllerBase
    {
        private readonly DebtStatisticContext _context;

        public BusinessUnitsController(DebtStatisticContext context)
        {
            _context = context;
        }
                
        public async Task<ActionResult<IEnumerable<BusinessUnit>>> GetBusinessUnits()
        {
            return await _context.BusinessUnits.ToListAsync();
        }
    }
}