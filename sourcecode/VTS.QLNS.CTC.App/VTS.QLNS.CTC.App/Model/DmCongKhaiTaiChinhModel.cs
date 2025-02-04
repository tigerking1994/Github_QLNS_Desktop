using ControlzEx.Standard;
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
    public class DmCongKhaiTaiChinhModel : ModelBase
    {
        private string _stt;
        [DisplayName("Số thứ tự")]
        [DisplayDetailInfo("Số thứ tự")]
        public string Stt
        {
            get => _stt;
            set => SetProperty(ref _stt, value);
        }

        private string _moTa;
        [DisplayName("Mô tả")]
        [DisplayDetailInfo("Mô tả")]
        public string sMoTa
        {
            get => _moTa;
            set => SetProperty(ref _moTa, value);
        }

        //private string _iNamLamViec;
        //[DisplayDetailInfo("Năm làm việc")]
        //public string iNamLamViec
        //{
        //    get => _iNamLamViec;
        //    set => SetProperty(ref _iNamLamViec, value);
        //}

        [DisplayDetailInfo("Ngày tạo")]
        public DateTime DNgayTao { get; set; }

        [DisplayDetailInfo("Người tạo")]
        public string SNguoiTao { get; set; }

        [DisplayDetailInfo("Ngày cập nhật")]
        public DateTime? DNgaySua { get; set; }

        [DisplayDetailInfo("Người cập nhật")]
        public string SNguoiSua { get; set; }

        [DisplayDetailInfo("Thẻ")]
        public string Tag { get; set; }

       
        private string _mlns;

        [DisplayName("Mục lục ngân sách")]
        [DisplayDetailInfo("Mục lục ngân sách")]
        [ColumnTypeAttribute(ColumnType.ReferencePopup)]
        public string Mlns
        {
            get => _mlns;
            set => SetProperty(ref _mlns, value);
        }

        private Guid? _iIdDmCongKhaiCha;
        public Guid? IIdDmCongKhaiCha
        {
            get => _iIdDmCongKhaiCha;
            set => SetProperty(ref _iIdDmCongKhaiCha, value);
        }

        private string _sMa;
        public string SMa
        {
            get => _sMa;
            set => SetProperty(ref _sMa, value);
        }

        private string _sMaCha;
        public string SMaCha
        {
            get => _sMaCha;
            set => SetProperty(ref _sMaCha, value);
        }

        private bool _isHangCha;
        public override bool IsHangCha {
            get => _isHangCha;
            set => SetProperty(ref _isHangCha, value);
        }
    }
}
