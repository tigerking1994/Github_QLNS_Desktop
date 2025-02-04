using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Utility
{
    public class AuthenticationInfo
    {
        public string Principal { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public string IdDonVi { get; set; }
        public List<string> Authorities { get; set; }
        public int YearOfBudget { get; set; }
        public string YearOfBudgetStr { get; set; }
        public int YearOfWork { get; set; }
        public int Month { get; set; }
        public string Time { get; set; }
        public int Budget { get; set; }
        public string BudgetStr { get; set; }
        public object[] OptionalParam { get; set; }
    }
}
