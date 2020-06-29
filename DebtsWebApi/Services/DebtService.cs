using DebtsWebApi.DAL;
using DebtsWebApi.Entities;
using DebtsWebApi.Interfaces;
using DebtsWebApi.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DebtsWebApi.Services
{
    public class DebtService : IDebtService
    {
        private readonly DebtStatisticContext _context;

        public DebtService(DebtStatisticContext context)
        {
            _context = context;
        }

        public async Task<Debt> CreateDebtAsync(Debt next)
        {
            next.Id = await GetNewDebtIdAsync();
            next.BeginDate = DateTime.Now;
            next.EndDate = DateTime.MaxValue;

            _context.Debts.Add(next);
            _context.SaveChanges();

            return next;
        }

        public async Task<Debt> UpdateDebtAsync(Debt current, Debt next)
        {
            var now = DateTime.Now;

            current.EndDate = now;

            next.Id = current.Id;
            next.BeginDate = now;
            next.EndDate = DateTime.MaxValue;
            _context.Debts.Add(next);

            await _context.SaveChangesAsync();

            return next;
        }

        public async Task<Debt> GetDebtByFieldsAsync(Debt debt)
        {
            var result = await _context.Debts
                .FirstOrDefaultAsync(d =>
                    d.DebtorKey == debt.DebtorKey &&
                    d.DebtTypeKey == debt.DebtTypeKey &&
                    d.Year == debt.Year &&
                    d.Month == debt.Month &&
                    d.EndDate > DateTime.Now);

            return result;
        }

        public async Task<Debt> GetDebtByIdAsync(int id)
        {
            var result = await _context.Debts
                .SingleOrDefaultAsync(d =>
                    d.Id == id &&
                    d.EndDate > DateTime.Now);

            return result;
        }

        public async Task<ActionResult<IEnumerable<DebtResult>>> GetDebtsAsync(int debtTypeId, int year, int month, int[] businessUnits, long[] departments)
        {
            var existDebts = await GetExistsDebtsQuery(debtTypeId, year, month, businessUnits, departments).ToListAsync();
            var potentialDebts = await GetPotentialDebtsQuery(debtTypeId, year, month, businessUnits, departments).ToListAsync();

            var debts = existDebts.Concat(potentialDebts).ToList();

            return debts;
        }

        public async Task<ActionResult<IEnumerable<object>>> GetCommonDebtsAsync(int year, int month, int[] businessUnits, long[] departments)
        {
            var existDebts = await GetExistsDebtsQuery(0, year, month, businessUnits, departments).ToListAsync();
            var potentialDebts = await GetPotentialDebtsQuery(0, year, month, businessUnits, departments).ToListAsync();

            var debts = existDebts.Concat(potentialDebts);

            var result = debts
                .GroupBy(d => (d.DebtorId, d.DebtorName), d => (d.DebtTypeId, d.Cost * d.Count))
                .Select(g => new
                {
                    DebtorId = g.Key.Item1,
                    DebtorName = g.Key.DebtorName,
                    Dinners = g.Where(v => v.DebtTypeId == 2).Sum(v => v.Item2),
                    Travels = g.Where(v => v.DebtTypeId == 3).Sum(v => v.Item2),
                    Tickets = g.Where(v => v.DebtTypeId == 4).Sum(v => v.Item2),
                    Events = g.Where(v => v.DebtTypeId == 5).Sum(v => v.Item2),
                    Fitness = g.Where(v => v.DebtTypeId == 6).Sum(v => v.Item2),
                    Loans = g.Where(v => v.DebtTypeId == 7).Sum(v => v.Item2),
                    Acts = g.Where(v => v.DebtTypeId == 8).Sum(v => v.Item2),
                    Total = g.Sum(v => v.Item2)
                })
                .ToList();

            return result;
        }

        private async Task<int> GetNewDebtIdAsync()
        {
            var newDebtId = new DebtId { IsActual = true };

            _context.DebtIds.Add(newDebtId);
            _context.SaveChanges();

            var newId = await _context.DebtIds.MaxAsync(di => di.Id);

            return newId;
        }

        private IQueryable<DebtResult> GetExistsDebtsQuery(int debtTypeId, int year, int month, int[] businessUnits, long[] departments)
        {
            var query = _context.Debts
                .Include(d => d.Debtor)
                .Where(d =>
                    (debtTypeId == 0 || d.DebtTypeKey == debtTypeId) &&
                    (!businessUnits.Any() || businessUnits.Contains(d.Debtor.BusinessUnit.Id)) &&
                    (!departments.Any() || departments.Contains(d.Debtor.Department.Id)) &&
                    d.Year == year &&
                    d.Month == month &&
                    d.EndDate > DateTime.Now)
               .Select(d => new DebtResult
               {
                   Id = d.Id,
                   DebtTypeId = d.DebtTypeKey,
                   BusinessUnitId = d.Debtor.BusinessUnitKey,
                   DepartmentId = d.Debtor.DepartmentKey,
                   DebtorId = d.Debtor.Id,
                   DebtorName = d.Debtor.Name,
                   Month = month,
                   Year = year,
                   Cost = d.Cost,
                   Count = d.Count,
                   Description = d.Description
               });

            return query;
        }

        private IQueryable<DebtResult> GetPotentialDebtsQuery(int debtTypeId, int year, int month, int[] businessUnits, long[] departments)
        {
            var cost = GetDefaultCost(debtTypeId).Result.Value;

            var query = _context.Users
                .Where(u => u.StateId == 1 &&
                            (!businessUnits.Any() || businessUnits.Contains(u.BusinessUnitKey)) &&
                            (!departments.Any() || departments.Contains(u.DepartmentKey)) &&
                            !_context.Debts.Any(d =>
                                (debtTypeId == 0 || d.DebtTypeKey == debtTypeId) &&
                                d.Year == year &&
                                d.Month == month &&
                                d.DebtorKey == u.Id))
                .Select(u => new DebtResult
                {
                    Id = 0,
                    DebtTypeId = debtTypeId,
                    BusinessUnitId = u.BusinessUnitKey,
                    DepartmentId = u.DepartmentKey,
                    DebtorId = u.Id,
                    DebtorName = u.Name,
                    Month = month,
                    Year = year,
                    Cost = cost,
                    Count = 0,
                    Description = string.Empty
                });

            return query;
        }

        private async Task<ActionResult<float>> GetDefaultCost(int debtTypeId)
        {
            var defaultCost = await _context
                .DefaultCosts
                .SingleOrDefaultAsync(dc => dc.DebtTypeKey == debtTypeId && dc.EndDate > DateTime.Now);

            if (defaultCost == null)
                return 0;

            return defaultCost.Value;
        }
    }
}
