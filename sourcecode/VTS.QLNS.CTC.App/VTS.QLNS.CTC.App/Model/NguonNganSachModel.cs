using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class NguonNganSachModel : ModelBase
    {
        [DisplayDetailInfo("Mã")]
        public int? IIdMaNguonNganSach { get; set; }

        private string _sTen;
        [DisplayName("Tên")]
        [DisplayDetailInfo("Tên")]
        public string STen 
        {
            get => _sTen; 
            set => SetProperty(ref _sTen, value);
        }

        private string _sMoTa;
        [DisplayName("Mô tả")]
        [DisplayDetailInfo("Mô tả")]
        public string SMoTa 
        {
            get => _sMoTa;
            set => SetProperty(ref _sMoTa, value);
        }

        private int? _iSTT;
        [DisplayName("STT")]
        [DisplayDetailInfo("STT")]
        public int? IStt 
        {
            get => _iSTT;
            set => SetProperty(ref _iSTT, value);
        }

        private int? _iTrangThai;
        [DisplayName("Trạng thái")]
        [ColumnTypeAttribute(ColumnType.Combobox, "LoadAllTrangThaiNguonNganSach")]
        public int? ITrangThai 
        {
            get => _iTrangThai; 
            set => SetProperty(ref _iTrangThai, value);
        }
        public bool? BPublic { get; set; }
        public string IIdMaNhomNguoiDungPublic { get; set; }
        public string IIdMaNhomNguoiDungDuocGiao { get; set; }
        public string SIdMaNguoiDungDuocGiao { get; set; }
        [DisplayDetailInfo("Ngày tạo")]
        public DateTime? DNgayTao { get; set; }
        [DisplayDetailInfo("Người tạo")]
        public string SNguoiTao { get; set; }
        public int? ISoLanSua { get; set; }
        [DisplayDetailInfo("Ngày cập nhật")]
        public DateTime? DNgaySua { get; set; }
        public string SIpsua { get; set; }
        [DisplayDetailInfo("Người cập nhật")]
        public string SNguoiSua { get; set; }

        public override bool IsEditable => !IsHangCha && !IsDeleted;
    }
}
