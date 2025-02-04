using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VTS.QLNS.CTC.App.Extensions;

namespace VTS.QLNS.CTC.App.Model
{
    public class CpDanhMucModel : ModelBase
    {
        private string _iIDMaDMCapPhat;
        [DisplayName("Mã danh mục")]
        [DisplayDetailInfo("Mã danh mục")]
        public string IIDMaDMCapPhat 
        {
            get => _iIDMaDMCapPhat;
            set => SetProperty(ref _iIDMaDMCapPhat, value);
        }

        private string _sTen;
        [DisplayName("Tên danh mục")]
        [DisplayDetailInfo("Tên danh mục")]
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

        [DisplayDetailInfo("Tên thông tri cấp")]
        public string STenThongTriCap { get; set; }
        [DisplayDetailInfo("Tên thông tri thu")]
        public string STenThongTriThu { get; set; }
        [DisplayDetailInfo("LNS")]
        public string Lns { get; set; }        
        public int? OrderIndex { get; set; }
        [DisplayDetailInfo("Năm làm việc")]
        public int? INamLamViec { get; set; }
        public int ITrangThai { get; set; }
        [DisplayDetailInfo("Ngày tạo")]
        public DateTime? DNgayTao { get; set; }
        [DisplayDetailInfo("Người tạo")]
        public string SNguoiTao { get; set; }
        [DisplayDetailInfo("Ngày sửa")]
        public DateTime? DNgaySua { get; set; }
        [DisplayDetailInfo("Người sửa")]
        public string SNguoiSua { get; set; }
        [DisplayDetailInfo("Tag")]
        public string Tag { get; set; }
        [DisplayDetailInfo("Log")]
        public string Log { get; set; }
        [DisplayDetailInfo("Trạng thái")]
        public string TrangThaiDisplay
        {
            get => ITrangThai switch
            {
                0 => "không sử dụng",
                1 => "Đang sử dụng",
                2 => "ngành nghiệp vụ",
                _ => ""
            };
        }

        public override string DetailInfoModalTitle => "Chi tiết danh mục " + STen;

        public override bool IsEditable => !IsDeleted;
    }
}
