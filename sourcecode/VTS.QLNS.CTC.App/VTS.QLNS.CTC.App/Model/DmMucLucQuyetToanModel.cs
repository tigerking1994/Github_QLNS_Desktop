using ControlzEx.Standard;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class DmMucLucQuyetToanModel : ModelBase
    {

        private string _sMa;
        [DisplayName("Mã")]
        [DisplayDetailInfo("Mã")]
        public string SMa
        {
            get => _sMa;
            set => SetProperty(ref _sMa, value);
        }

        private string _sMaCha;
        [DisplayName("Mã cha")]
        [DisplayDetailInfo("Mã cha")]
        public string SMaCha
        {
            get => _sMaCha;
            set => SetProperty(ref _sMaCha, value);
        }

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
        public string SMoTa
        {
            get => _moTa;
            set => SetProperty(ref _moTa, value);
        }

        public string SMoTaDisplay => $"{_stt} {_moTa}";

        [DisplayDetailInfo("Ngày tạo")]
        public DateTime DNgayTao { get; set; }

        [DisplayDetailInfo("Người tạo")]
        public string SNguoiTao { get; set; }

        [DisplayDetailInfo("Ngày cập nhật")]
        public DateTime? DNgaySua { get; set; }

        [DisplayDetailInfo("Người cập nhật")]
        public string SNguoiSua { get; set; }
       
        private string _mlns;

        [DisplayName("Mục lục ngân sách (F6)")]
        [DisplayDetailInfo("Mục lục ngân sách (F6)")]
        [ColumnTypeAttribute(ColumnType.ReferencePopup)]
        public string Mlns
        {
            get => _mlns;
            set => SetProperty(ref _mlns, value);
        }

        private bool _isHangCha;
        public override bool IsHangCha {
            get => _isHangCha;
            set => SetProperty(ref _isHangCha, value);
        }

        [JsonIgnore]
        public ICollection<NsMucLucQuyetToanNamMLNS> NsMucLucQuyetToanNamMLNS { get; set; }

    }
}
