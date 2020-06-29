using DebtsWebApi.Entities;
using DebtsWebApi.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DebtsWebApi.Interfaces
{
    public interface IDebtService
    {
        Task<Debt> CreateDebtAsync(Debt next);

        Task<Debt> UpdateDebtAsync(Debt current, Debt next);

        Task<Debt> GetDebtByFieldsAsync(Debt debt);

        Task<Debt> GetDebtByIdAsync(int id);

        Task<ActionResult<IEnumerable<DebtResult>>> GetDebtsAsync(int debtTypeId, int year, int month, int[] businessUnits, long[] departments);

        Task<ActionResult<IEnumerable<object>>> GetCommonDebtsAsync(int year, int month, int[] businessUnits, long[] departments);
    }
}
