using System;
using System.ComponentModel;
using VTS.QLNS.CTC.App.Extensions;

namespace VTS.QLNS.CTC.App.Model
{
    public class QsMucLucModel : ModelBase
    {
        public Guid IIdMlns { get; set; }
        public Guid? IIdMlnsCha { get; set; }

        private string _sm;
        [DisplayName("M")]
        [DisplayDetailInfo("M")]
        public string SM
        {
            get => _sm;
            set => SetProperty(ref _sm, value);
        }

        private string _sTm;
        [DisplayDetailInfo("TM")]
        [DisplayName("TM")]
        public string STm
        {
            get => _sTm;
            set => SetProperty(ref _sTm, value);
        }

        private string _sKyHieu;
        [DisplayName("Xâu nối mã")]
        [DisplayDetailInfo("Xâu nối mã")]
        public string SKyHieu
        {
            get => _sKyHieu;
            set => SetProperty(ref _sKyHieu, value);
        }

        private string _sMota;
        [DisplayName("Mô tả")]
        [DisplayDetailInfo("Mô tả")]
        public string SMoTa
        {
            get => _sMota;
            set => SetProperty(ref _sMota, value);
        }

        [DisplayDetailInfo("STT")]
        public int IThuTu { get; set; }
        public bool BHangCha { get; set; }

        private string _sHienThi;
        //[DisplayName("Hiển thị")]
        //[DisplayDetailInfo("Hiển thị")]
        public string SHienThi
        {
            get => _sHienThi;
            set => SetProperty(ref _sHienThi, value);
        }

        private int _iTrangThai;
        [ColumnTypeAttribute(Utility.ColumnType.Checkbox)]
        [DisplayName("Sử dụng")]
        [DisplayDetailInfo("Sử dụng")]
        public int ITrangThai
        {
            get => _iTrangThai;
            set => SetProperty(ref _iTrangThai, value);
        }

        //public int ITrangThai { get; set; }
        [DisplayDetailInfo("Năm làm việc")]
        public int INamLamViec { get; set; }

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

        public override string DetailInfoModalTitle => "Chi tiết MLQS " + SKyHieu;

        public override bool IsEditable => !IsDeleted;

        public string TangGiam => string.Format("{0} - {1}", SKyHieu, SMoTa);
    }
}
