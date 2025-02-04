using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Import
{
    public class RevenueExpenditureDivisionImportModel : RevenueExpenditurePlanImportModel
    {
        private string _dotNhan;
        [ColumnAttribute("Đợt Nhận", 9)]
        public string DotNhan
        {
            get => _dotNhan;
            set => SetProperty(ref _dotNhan, value);
        }
    }
}
