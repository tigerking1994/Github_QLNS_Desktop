using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtDmDuToanChiModel : ModelBase
    {
        [ColumnIndex(0)]
        public Guid IIdDuToanChi { get; set; }

        private string _sMaDuToanChi;
        [DisplayName("Mã dự toán chi")]
        [DisplayDetailInfo("Mã dự toán chi")]
        public string SMaDuToanChi 
        {
            get => _sMaDuToanChi;
            set => SetProperty(ref _sMaDuToanChi, value);
        }

        private string _sTenVietTat;
        [DisplayName("Tên viết tắt")]
        [DisplayDetailInfo("Tên viết tắt")]
        public string STenVietTat 
        {
            get => _sTenVietTat;
            set => SetProperty(ref _sTenVietTat, value);
        }

        private string _sTenDuToanChi;
        [DisplayName("Tên dự toán chi")]
        [DisplayDetailInfo("Tên dự toán chi")]
        public string STenDuToanChi 
        {
            get => _sTenDuToanChi;
            set => SetProperty(ref _sTenDuToanChi, value);
        }

        private string _sMota;
        [DisplayName("Mô tả")]
        [DisplayDetailInfo("Mô tả")]
        public string SMoTa 
        {
            get => _sMota;
            set => SetProperty(ref _sMota, value);
        }

        public int IThuTu { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SIdMaNguoiDungTao { get; set; }
        public int? ISoLanSua { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SIpsua { get; set; }
        public string SIdMaNguoiDungSua { get; set; }
        public bool? BHangCha { get; set; }
        public Guid? IIdDuToanChiParent { get; set; }

        private string _duToanChiParent;
        [DisplayName("Dự toán cha")]
        [DisplayDetailInfo("Dự toán cha")]
        [ColumnType(ColumnType.ReferencePopup)]
        public string DuToanChiParent
        {
            get => _duToanChiParent;
            set => SetProperty(ref _duToanChiParent, value);
        }
    }
}
