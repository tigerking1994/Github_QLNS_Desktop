using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhDmMucDongBHXHModel : ModelBase
    {
        public override Guid Id { get; set; }
        private string _sMaMucDong;
        [DisplayName("Mã mục đóng")]
        [DisplayDetailInfo("SMaMucDong")]
        [Validate("Mã mục đóng", Utility.Enum.DATA_TYPE.String, true)]
        public string SMaMucDong
        {
            get => _sMaMucDong;
            set => SetProperty(ref _sMaMucDong, value);
        }
        private string _sNoiDung;
        [DisplayName("Nội dung")]
        [DisplayDetailInfo("Nội dung")]
        [Validate("Nội dung", Utility.Enum.DATA_TYPE.String, true)]
        public string SNoiDung
        {
            get => _sNoiDung;
            set => SetProperty(ref _sNoiDung, value);
        }
        private string _sBH_XauNoiMa;
        [DisplayName("Xâu nối mã")]
        [DisplayDetailInfo("SBH_XauNoiMa")]
        [Validate("Xâu nối mã", Utility.Enum.DATA_TYPE.String, true)]
        public string SBH_XauNoiMa
        {
            get => _sBH_XauNoiMa;
            set => SetProperty(ref _sBH_XauNoiMa, value);
        }
        private double _fTyLe_BHXH_NLD;
        [DisplayName("Tỷ lệ BHXH NLD")]
        [DisplayDetailInfo("FTyLe_BHXH_NLD")]
        public double FTyLe_BHXH_NLD
        {
            get => _fTyLe_BHXH_NLD;
            set => SetProperty(ref _fTyLe_BHXH_NLD, value);
        }
        private double _fTyLe_BHXH_NSD;
        [DisplayName("Tỷ lệ BHXH NSD")]
        [DisplayDetailInfo("FTyLe_BHXH_NSD")]
        public double FTyLe_BHXH_NSD
        {
            get => _fTyLe_BHXH_NSD;
            set => SetProperty(ref _fTyLe_BHXH_NSD, value);
        }
        private double _fTyLe_BHYT_NLD;
        [DisplayName("Tỷ lệ BHYT NLD")]
        [DisplayDetailInfo("FTyLe_BHYT_NLD")]
        public double FTyLe_BHYT_NLD
        {
            get => _fTyLe_BHYT_NLD;
            set => SetProperty(ref _fTyLe_BHYT_NLD, value);
        }

        private double _fTyLe_BHYT_NSD;
        [DisplayName("Tỷ lệ BHYT NSD")]
        [DisplayDetailInfo("FTyLe_BHYT_NSD")]
        public double FTyLe_BHYT_NSD
        {
            get => _fTyLe_BHYT_NSD;
            set => SetProperty(ref _fTyLe_BHYT_NSD, value);
        }

        private double _fTyLe_BHTN_NLD;
        [DisplayName("Tỷ lệ BHTN NLD")]
        [DisplayDetailInfo("FTyLe_BHTN_NLD")]
        public double FTyLe_BHTN_NLD
        {
            get => _fTyLe_BHTN_NLD;
            set => SetProperty(ref _fTyLe_BHTN_NLD, value);
        }
        private double _fTyLe_BHTN_NSD;
        [DisplayName("Tỷ lệ BHTN NSD")]
        [DisplayDetailInfo("FTyLe_BHTN_NSD")]
        public double FTyLe_BHTN_NSD
        {
            get => _fTyLe_BHTN_NSD;
            set => SetProperty(ref _fTyLe_BHTN_NSD, value);
        }
        public int? INamLamViec { get; set; }
        private int? _iTrangThai;
        [DisplayName("Trạng thái")]
        [ColumnTypeAttribute(ColumnType.Combobox, "LoadComboboxData")]
        [HorizontalAttribute(HorizontalAlignment.Center)]
        public int? ITrangThai
        {
            get => _iTrangThai;
            set => SetProperty(ref _iTrangThai, value);
        }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
        public ICollection<BhDmMucLucNganSachModel> DanhMucNganSach { get; set; }
    }
}
