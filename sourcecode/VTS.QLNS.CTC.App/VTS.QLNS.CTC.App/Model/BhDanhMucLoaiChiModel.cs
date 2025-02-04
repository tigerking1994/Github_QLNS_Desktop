using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhDanhMucLoaiChiModel : ModelBase
    {
        public Guid Id { get; set; }
        private string _sMaLoaiChi;
        [DisplayName("Mã loại chi")]
        [DisplayDetailInfo("Mã loại chi")]
        [Validate("Mã loại chi", Utility.Enum.DATA_TYPE.String, true)]
        public string SMaLoaiChi
        {
            get => _sMaLoaiChi;
            set => SetProperty(ref _sMaLoaiChi, value);

        }
        private string _sTenDanhMucLoaiChi;
        [DisplayName("Tên loại chi")]
        [DisplayDetailInfo("Tên loại chi")]
        [Validate("Tên loại chi", Utility.Enum.DATA_TYPE.String, true)]
        public string STenDanhMucLoaiChi
        {
            get => _sTenDanhMucLoaiChi;
            set => SetProperty(ref _sTenDanhMucLoaiChi, value);
        }
        public int INamLamViec { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }

        private int? _iTrangThai;
        [DisplayName("Trạng thái")]
        [ColumnTypeAttribute(ColumnType.Combobox, "LoadComboboxData")]
        [HorizontalAttribute(HorizontalAlignment.Center)]
        public int? ITrangThai
        {
            get => _iTrangThai;
            set => SetProperty(ref _iTrangThai, value);
        }
        private string _sMota;

        [DisplayName("Mô tả")]
        [DisplayDetailInfo("Mô tả")]
        public string SMoTa
        {
            get => _sMota;
            set => SetProperty(ref _sMota, value);
        }
        public string SLNS { get; set; }

        private string _sDSXauNoiMa;
        [DisplayName("Xâu nối mã (F6)")]
        [DisplayDetailInfo("Xâu nối mã")]
        [ColumnTypeAttribute(ColumnType.ReferencePopup)]
        [IsAllowMultipleSelectAttribute(true)]
        public string SDSXauNoiMa
        {
            get => _sDSXauNoiMa;
            set => SetProperty(ref _sDSXauNoiMa, value);
        }


        public ICollection<BhDmMucLucNganSachModel> DanhMucNganSach { get; set; }
    }
}
