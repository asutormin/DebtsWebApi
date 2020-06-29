using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DebtsWebApi.Entities;
using DebtsWebApi.Helpers;
using DebtsWebApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DebtsWebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DebtsController : ControllerBase
    {
        private readonly IDebtService _debtService;

        public DebtsController(IDebtService debtService)
        {
            _debtService = debtService;
        }

        [HttpGet("common/{year}/{month}/{businessUnits}/{departments}")]
        public async Task<ActionResult<IEnumerable<object>>> GetCommonDebts(int year, int month, string businessUnits, string departments)
        {
            var businessUnitsIds = GetArray<int>(businessUnits);
            var departmentsIds = GetArray<long>(departments);

            var result = await _debtService.GetCommonDebtsAsync(year, month, businessUnitsIds, departmentsIds);

            //if (!commonDebts.Any())
            //{
            //    return NotFound();
            //}

            return result;
        }

        [HttpGet("{debtTypeId}/{year}/{month}/{businessUnits}/{departments}")]
        public async Task<ActionResult<IEnumerable<DebtResult>>> GetDebts(int debtTypeId, int year, int month, string businessUnits, string departments)
        {
            var businessUnitsIds = GetArray<int>(businessUnits);
            var departmentsIds = GetArray<long>(departments);

            var result = await _debtService.GetDebtsAsync(debtTypeId, year, month, businessUnitsIds, departmentsIds);

            //if (!debts.Any())
            //{
            //    return NotFound();
            //}

            return result;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody]DebtInfo debtInfo)
        {
            var next = debtInfo.ToDebt();

            if (next == null)
                return BadRequest(new { message = "Информация о задолженности не передана." });

            var current = await _debtService.GetDebtByFieldsAsync(next);

            if (current == null)
            {
                // Добавляем новую запись
                await _debtService.CreateDebtAsync(next);
                return Ok(next.Id);
            }

            if (!next.Equals(current))
            {
                // Изменяем либо перезаписываем запись, созданную не нами
                await _debtService.UpdateDebtAsync(current, next);
                return Ok(next.Id);
            }

            return Ok(next.Id);
         }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync([FromBody]DebtInfo debtInfo)
        {
            var next = debtInfo.ToDebt();

            if (next == null)
                return BadRequest(new { message = "Информация о задолженности не передана." });

            var current = await _debtService.GetDebtByIdAsync(next.Id);

            if (current == null)
                return BadRequest(new { message = "Информация о задолженности не найдена." });

            if (!next.Equals(current))
            {
                // Изменяем либо перезаписываем запись, созданную не нами
                await _debtService.UpdateDebtAsync(current, next);
            }

            return Ok(next.Id);
        }

        private T[] GetArray<T>(string ids)
        {
            return ids.Replace("\"", "").Split(';').Where(n => n.Trim().Length > 0).Select(n => (T)Convert.ChangeType(n, typeof(T))).ToArray();
        }
    }
}