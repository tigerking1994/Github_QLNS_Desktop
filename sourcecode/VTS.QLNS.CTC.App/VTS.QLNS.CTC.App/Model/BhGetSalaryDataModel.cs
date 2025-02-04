using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhGetSalaryDataModel
    {
        public IEnumerable<BhKhtBHXHChiTietQuery> ItemsPlanSalary { get; set; } = new List<BhKhtBHXHChiTietQuery>();
        public IEnumerable<BhKhtBHXHChiTietQuery> ItemArmy { get; set; } = new List<BhKhtBHXHChiTietQuery>();
        public IEnumerable<TlDsBangLuongKeHoachModel> Items { get; set; } = new List<TlDsBangLuongKeHoachModel>();
    }
}
