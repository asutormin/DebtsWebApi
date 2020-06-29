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
    public class DebtTypesController : ControllerBase
    {
        private readonly DebtStatisticContext _context;
        public DebtTypesController(DebtStatisticContext context)
        {
            _context = context;
        }

        // GET: api/DebtTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DebtType>>> GetDebtTypes()
        {
            return await _context.DebtTypes.ToListAsync();
        }
    }
}