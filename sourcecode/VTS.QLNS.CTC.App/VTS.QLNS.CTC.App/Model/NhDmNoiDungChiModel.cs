using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDmNoiDungChiModel : ModelBase
    {
        private string _sMaNoiDungChi;
        [DisplayName("Mã nội dung chi")]
        [DisplayDetailInfo("Mã nội dung chi")]
        [Validate("Mã nội dung chi", DATA_TYPE.String, 50, true)]
        public string SMaNoiDungChi
        {
            get => _sMaNoiDungChi;
            set => SetProperty(ref _sMaNoiDungChi, value);
        }
        private string _sTenNoiDungChi;
        [DisplayName("Tên nội dung chi")]
        [DisplayDetailInfo("Tên nội dung chi")]
        [Validate("Tên nội dung chi", DATA_TYPE.String, 50, true)]
        public string STenNoiDungChi
        {
            get => _sTenNoiDungChi;
            set => SetProperty(ref _sTenNoiDungChi, value);
        }

        private string _sMoTa;
        [DisplayName("Mô tả")]
        [DisplayDetailInfo("Mô tả")]
        [Validate("Mô tả")]
        public string SMoTa
        {
            get => _sMoTa;
            set => SetProperty(ref _sMoTa, value);
        }
    }
}
