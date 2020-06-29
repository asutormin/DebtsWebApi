using DebtsWebApi.Entities;
using DebtsWebApi.DAL.Models;
using System;

namespace DebtsWebApi.Helpers
{
    public static class ExtensionMethods
    {
        public static User WithoutPassword(this User user)
        {
            user.UserPassword = null;
            
            return user;
        }

        public static AuthResult ToAuthUser(this User user)
        {
            var authUser = new AuthResult
            {
                Id = user.Id,
                Name = user.Name,
                PositionName = user.PositionName,
                StateId = user.StateId,
                BusinessUnitId = user.BusinessUnitKey,
                DepartmentId = user.DepartmentKey,
                UserLogin = user.UserLogin,
                LdapLogin = user.LdapLogin
            };

            return authUser;
        }

        public static Debt ToDebt(this DebtInfo debtInfo)
        {
            var debt = new Debt
            {
                Id = debtInfo.Id,
                DebtorKey = debtInfo.DebtorId,
                DebtTypeKey = debtInfo.DebtTypeId,
                Year = debtInfo.Year,
                Month = debtInfo.Month,
                Count = debtInfo.Count,
                Cost = debtInfo.Cost,
                Description = debtInfo.Description,
                EditorKey = debtInfo.EditorId
            };

            return debt;
        }

        public static bool IsNew(this Debt debt)
        {
            return debt.Id == 0;
        }

        public static bool Is(this Debt debt1, Debt debt2)
        {
            return debt1.Id == debt2.Id;
        }

        public static bool Equals(this Debt debt1, Debt debt2)
        {
            return
               debt1.Id == debt2.Id &&
               debt1.DebtorKey == debt2.DebtorKey &&
               debt1.DebtTypeKey == debt2.DebtTypeKey &&
               debt1.Year == debt2.Year &&
               debt1.Month == debt2.Month &&
               debt1.Cost == debt2.Cost &&
               debt1.Count == debt2.Count;
        }
    }
}
