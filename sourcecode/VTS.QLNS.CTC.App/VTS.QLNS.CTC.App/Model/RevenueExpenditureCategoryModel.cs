using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class RevenueExpenditureCategoryModel : ModelBase
    {
        public int? INamLamViec { get; set; }
        public string IdPhongBan { get; set; }

        private string _stt;
        [ColumnIndexAttribute(0)]
        [DisplayName("STT")]
        [DisplayDetailInfo("STT")]
        public string IStt
        {
            get => _stt;
            set => SetProperty(ref _stt, value);
        }

        private int? _iTrangThai;
        [DisplayName("Trạng thái")]
        [DisplayDetailInfo("Trạng thái")]
        [ColumnTypeAttribute(ColumnType.Combobox, "LoadAllTrangThaiMucLuc")]
        public int? ITrangThai 
        {
            get => _iTrangThai;
            set => SetProperty(ref _iTrangThai, value);
        }

        private string _lns;
        [DisplayName("LNS")]
        [DisplayDetailInfo("LNS")]
        public string Lns 
        {
            get => _lns;
            set => SetProperty(ref _lns, value);
        }
        [DisplayDetailInfo("Ngày tạo")]
        public DateTime? DateCreated { get; set; }
        [DisplayDetailInfo("Người tạo")]
        public string UserCreator { get; set; }
        [DisplayDetailInfo("Ngày cập nhật")]
        public DateTime? DateModified { get; set; }
        [DisplayDetailInfo("Người cập nhật")]
        public string UserModifier { get; set; }
        [DisplayDetailInfo("Số lần cập nhật")]
        public int? ISoLanSua { get; set; }
        [DisplayDetailInfo("Ip cập nhật")]
        public string SIpsua { get; set; }

        private string _mota;
        [DisplayName("Mô tả")]
        [DisplayDetailInfo("Mô tả")]
        public string MoTa 
        {
            get => _mota;
            set => SetProperty(ref _mota, value);
        }
        public string IIdMaNhomNguoiDungPublic { get; set; }
        public string IIdMaNhomNguoiDungDuocGiao { get; set; }
        public string SIdMaNguoiDungDuocGiao { get; set; }
        public Guid? IdMaLoaiHinh { get; set; }
        public Guid? IdMaLoaiHinhCha { get; set; }

        private bool _bLaHangCha;
        [DisplayName("Hàng cha")]
        [ColumnTypeAttribute(ColumnType.Checkbox)]
        public bool BLaHangCha 
        {
            get => _bLaHangCha;
            set
            {
                SetProperty(ref _bLaHangCha, value);
                IsHangCha = value;
            }
        }

        [DisplayDetailInfo("Trạng thái")]
        public string ITrangThaiText
        {
            get => ITrangThai switch
            {
                0 => "không sử dụng",
                1 => "Đang sử dụng",
                2 => "ngành nghiệp vụ",
                _ => ""
            };
        }

        public override bool IsEditable => !IsDeleted;
    }
}
