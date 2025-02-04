using System;
using System.ComponentModel;
using VTS.QLNS.CTC.App.Extensions;

namespace VTS.QLNS.CTC.App.Model
{
    public class DmChuKyModel : ModelBase
    {
        private string _idCode;
        [DisplayName("STT")]
        [DisplayDetailInfo("STT")]
        public string IdCode
        {
            get => _idCode;
            set => SetProperty(ref _idCode, value);
        }

        private string _idType;
        [DisplayName("Mã báo cáo")]
        [DisplayDetailInfo("Mã báo cáo")]
        public string IdType
        {
            get => _idType;
            set => SetProperty(ref _idType, value);
        }

        private string _ten;
        [DisplayName("Tên báo cáo")]
        [DisplayDetailInfo("Tên báo cáo")]
        public string Ten
        {
            get => _ten;
            set => SetProperty(ref _ten, value);
        }

        public string KyHieu { get; set; }
        public string MoTa { get; set; }

        private string _tieuDe1;
        public string TieuDe1
        {
            get => _tieuDe1;
            set => SetProperty(ref _tieuDe1, value);
        }

        [DisplayName("Tiêu đề 1")]
        [DisplayDetailInfo("Tiêu đề 1")]
        public string TieuDe1MoTa { get; set; }

        private string _tieuDe2;
        public string TieuDe2
        {
            get => _tieuDe2;
            set => SetProperty(ref _tieuDe2, value);
        }

        [DisplayName("Tiêu đề 2")]
        [DisplayDetailInfo("Tiêu đề 2")]
        public string TieuDe2MoTa { get; set; }

        [DisplayName("Tiêu đề 3")]
        [DisplayDetailInfo("Tiêu đề 3")]
        public string TieuDe3MoTa { get; set; }

        private string _chucDanh1;
        public string ChucDanh1
        {
            get => _chucDanh1;
            set => SetProperty(ref _chucDanh1, value);
        }

        private string _chucDanh1MoTa;
        [DisplayName("Chức danh 1")]
        [DisplayDetailInfo("Chức danh 1")]
        public string ChucDanh1MoTa
        {
            get => _chucDanh1MoTa;
            set => SetProperty(ref _chucDanh1MoTa, value);
        }

        private string _thuaLenh1;
        public string ThuaLenh1
        {
            get => _thuaLenh1;
            set => SetProperty(ref _thuaLenh1, value);
        }

        private string _thuaLenh1MoTa;
        [DisplayName("Thừa lệnh 1")]
        [DisplayDetailInfo("Thừa lệnh 1")]
        public string ThuaLenh1MoTa
        {
            get => _thuaLenh1MoTa;
            set => SetProperty(ref _thuaLenh1MoTa, value);
        }

        private string _ten1;
        public string Ten1
        {
            get => _ten1;
            set => SetProperty(ref _ten1, value);
        }

        private string _ten1Mota;
        [DisplayName("Chữ ký 1")]
        [DisplayDetailInfo("Chữ ký 1")]
        public string Ten1MoTa
        {
            get => _ten1Mota;
            set => SetProperty(ref _ten1Mota, value);
        }

        private string _chucDanh2;
        public string ChucDanh2
        {
            get => _chucDanh2;
            set => SetProperty(ref _chucDanh2, value);
        }

        private string _chucDanh2MoTa;
        [DisplayName("Chức danh 2")]
        [DisplayDetailInfo("Chức danh 2")]
        public string ChucDanh2MoTa
        {
            get => _chucDanh2MoTa;
            set => SetProperty(ref _chucDanh2MoTa, value);
        }

        private string _thuaLenh2;
        public string ThuaLenh2
        {
            get => _thuaLenh2;
            set => SetProperty(ref _thuaLenh2, value);
        }

        private string _thuaLenh2MoTa;
        [DisplayName("Thừa lệnh 2")]
        [DisplayDetailInfo("Thừa lệnh 2")]
        public string ThuaLenh2MoTa
        {
            get => _thuaLenh2MoTa;
            set => SetProperty(ref _thuaLenh2MoTa, value);
        }

        private string _ten2;
        public string Ten2
        {
            get => _ten2;
            set => SetProperty(ref _ten2, value);
        }

        private string _ten2MoTa;
        [DisplayName("Chữ ký 2")]
        [DisplayDetailInfo("Chữ ký 2")]
        public string Ten2MoTa
        {
            get => _ten2MoTa;
            set => SetProperty(ref _ten2MoTa, value);
        }

        private string _chucDanh3;
        public string ChucDanh3
        {
            get => _chucDanh3;
            set => SetProperty(ref _chucDanh3, value);
        }

        private string _chucDanh3MoTa;
        [DisplayName("Chức danh 3")]
        [DisplayDetailInfo("Chức danh 3")]
        public string ChucDanh3MoTa
        {
            get => _chucDanh3MoTa;
            set => SetProperty(ref _chucDanh3MoTa, value);
        }

        private string _thuaLenh3;
        public string ThuaLenh3
        {
            get => _thuaLenh3;
            set => SetProperty(ref _thuaLenh3, value);
        }

        private string _thuaLenh3MoTa;
        [DisplayName("Thừa lệnh 3")]
        [DisplayDetailInfo("Thừa lệnh 3")]
        public string ThuaLenh3MoTa
        {
            get => _thuaLenh3MoTa;
            set => SetProperty(ref _thuaLenh3MoTa, value);
        }

        private string _ten3;
        public string Ten3
        {
            get => _ten3;
            set => SetProperty(ref _ten3, value);
        }

        private string _ten3MoTa;
        [DisplayName("Chữ ký 3")]
        [DisplayDetailInfo("Chữ ký 3")]
        public string Ten3MoTa
        {
            get => _ten3MoTa;
            set => SetProperty(ref _ten3MoTa, value);
        }

        private string _chucDanh4;
        public string ChucDanh4
        {
            get => _chucDanh4;
            set => SetProperty(ref _chucDanh4, value);
        }

        private string _chucDanh4MoTa;
        public string ChucDanh4MoTa
        {
            get => _chucDanh4MoTa;
            set => SetProperty(ref _chucDanh4MoTa, value);
        }

        private string _thuaLenh4;
        public string ThuaLenh4
        {
            get => _thuaLenh4;
            set => SetProperty(ref _thuaLenh4, value);
        }

        private string _thuaLenh4MoTa;
        public string ThuaLenh4MoTa
        {
            get => _thuaLenh4MoTa;
            set => SetProperty(ref _thuaLenh4MoTa, value);
        }

        private string _ten4;
        public string Ten4
        {
            get => _ten4;
            set => SetProperty(ref _ten4, value);
        }

        private string _ten4Mota;
        public string Ten4MoTa
        {
            get => _ten4Mota;
            set => SetProperty(ref _ten4Mota, value);
        }

        private string _chucDanh5;
        public string ChucDanh5
        {
            get => _chucDanh5;
            set => SetProperty(ref _chucDanh5, value);
        }

        private string _chucDanh5MoTa;
        public string ChucDanh5MoTa
        {
            get => _chucDanh5MoTa;
            set => SetProperty(ref _chucDanh5MoTa, value);
        }

        private string _thuaLenh5;
        public string ThuaLenh5
        {
            get => _thuaLenh5;
            set => SetProperty(ref _thuaLenh5, value);
        }

        private string _thuaLenh5MoTa;
        public string ThuaLenh5MoTa
        {
            get => _thuaLenh5MoTa;
            set => SetProperty(ref _thuaLenh5MoTa, value);
        }

        private string _ten5;
        public string Ten5
        {
            get => _ten5;
            set => SetProperty(ref _ten5, value);
        }

        private string _ten5MoTa;
        public string Ten5MoTa
        {
            get => _ten5MoTa;
            set => SetProperty(ref _ten5MoTa, value);
        }

        private string _chucDanh6;
        public string ChucDanh6
        {
            get => _chucDanh6;
            set => SetProperty(ref _chucDanh6, value);
        }

        private string _chucDanh6MoTa;
        public string ChucDanh6MoTa
        {
            get => _chucDanh6MoTa;
            set => SetProperty(ref _chucDanh6MoTa, value);
        }

        private string _thuaLenh6;
        public string ThuaLenh6
        {
            get => _thuaLenh6;
            set => SetProperty(ref _thuaLenh6, value);
        }

        private string _thuaLenh6MoTa;
        public string ThuaLenh6MoTa
        {
            get => _thuaLenh6MoTa;
            set => SetProperty(ref _thuaLenh6MoTa, value);
        }

        private string _ten6;
        public string Ten6
        {
            get => _ten6;
            set => SetProperty(ref _ten6, value);
        }

        private string _ten6MoTa;
        public string Ten6MoTa
        {
            get => _ten6MoTa;
            set => SetProperty(ref _ten6MoTa, value);
        }

        private string _thuaUyQuyen1;
        public string ThuaUyQuyen1
        {
            get => _thuaUyQuyen1;
            set => SetProperty(ref _thuaUyQuyen1, value);
        }

        private string _thuaUyQuyen1MoTa;
        public string ThuaUyQuyen1MoTa
        {
            get => _thuaUyQuyen1MoTa;
            set => SetProperty(ref _thuaUyQuyen1MoTa, value);
        }

        private string _thuaUyQuyen2;
        public string ThuaUyQuyen2
        {
            get => _thuaUyQuyen2;
            set => SetProperty(ref _thuaUyQuyen2, value);
        }

        private string _thuaUyQuyen2MoTa;
        public string ThuaUyQuyen2MoTa
        {
            get => _thuaUyQuyen2MoTa;
            set => SetProperty(ref _thuaUyQuyen2MoTa, value);
        }

        private string _thuaUyQuyen3;
        public string ThuaUyQuyen3
        {
            get => _thuaUyQuyen3;
            set => SetProperty(ref _thuaUyQuyen3, value);
        }

        private string _thuaUyQuyen3MoTa;
        public string ThuaUyQuyen3MoTa
        {
            get => _thuaUyQuyen3MoTa;
            set => SetProperty(ref _thuaUyQuyen3MoTa, value);
        }


        private string _tenDVBanHanh1;
        public string TenDVBanHanh1
        {
            get => _tenDVBanHanh1;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    SetProperty(ref _tenDVBanHanh1, value.Trim());
                }
                else
                {
                    SetProperty(ref _tenDVBanHanh1, value);
                }
            }
        }

        private string _tenDVBanHanh2;
        public string TenDVBanHanh2
        {
            get => _tenDVBanHanh2;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    SetProperty(ref _tenDVBanHanh2, value.Trim());
                }
                else
                {
                    SetProperty(ref _tenDVBanHanh2, value);
                }
            }
        }

        public bool IsEnableDVBanHanh1 => LoaiDVBanHanh1 == "5";
        public bool IsEnableDVBanHanh2 => LoaiDVBanHanh2 == "5";

        private string _loaiDVBanHanh1;
        public string LoaiDVBanHanh1
        {
            get => _loaiDVBanHanh1;
            set
            {
                if (value != "5")
                {
                    TenDVBanHanh1 = string.Empty;
                }
                SetProperty(ref _loaiDVBanHanh1, value);
                OnPropertyChanged(nameof(IsEnableDVBanHanh1));
            }
        }

        private string _loaiDVBanHanh2;
        public string LoaiDVBanHanh2
        {
            get => _loaiDVBanHanh2;
            set
            {
                if (value != "5")
                {
                    TenDVBanHanh2 = string.Empty;
                }
                SetProperty(ref _loaiDVBanHanh2, value);
                OnPropertyChanged(nameof(IsEnableDVBanHanh2));
            }
        }

        private string _stt;
        public string Stt
        {
            get => _stt;
            set => SetProperty(ref _stt, value);
        }

        private string _sKinhGuiCqttbqp;
        public string SKinhGuiCqttbqp
        {
            get => _sKinhGuiCqttbqp;
            set => SetProperty(ref _sKinhGuiCqttbqp, value);
        }

        private string _sKinhGuiKbnn;
        public string SKinhGuiKbnn
        {
            get => _sKinhGuiKbnn;
            set => SetProperty(ref _sKinhGuiKbnn, value);
        }

        public string SLoai { get; set; }
        public int ITrangThai { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModifier { get; set; }
        public string Tag { get; set; }
        public string Log { get; set; }
        public override bool IsEditable => !IsDeleted;
        public override bool IsHangCha => string.IsNullOrEmpty(IdType) ? true : false;
    }
}
