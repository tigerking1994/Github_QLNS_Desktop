using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlQtChungTuChiTietGiaiThichModel : ModelBase
    {
        private Guid _iIdQtChungTu;
        public Guid IIdQtChungTu
        {
            get => _iIdQtChungTu;
            set => SetProperty(ref _iIdQtChungTu, value);
        }

        private string _iMaDonVi;
        public string IMaDonVi
        {
            get => _iMaDonVi;
            set => SetProperty(ref _iMaDonVi, value);
        }

        private int _ithang;
        public int IThang
        {
            get => _ithang;
            set => SetProperty(ref _ithang, value);
        }

        private int _inam;
        public int INam
        {
            get => _inam;
            set => SetProperty(ref _inam, value);
        }

        private double _fLuongSiQuan;
        public double FLuongSiQuan
        {
            get => _fLuongSiQuan;
            set => SetProperty(ref _fLuongSiQuan, value);
        }

        private double _fLuongQNCN;
        public double FLuongQNCN
        {
            get => _fLuongQNCN;
            set => SetProperty(ref _fLuongQNCN, value);
        }

        private double _fLuongCNVC;
        public double FLuongCNVC
        {
            get => _fLuongCNVC;
            set => SetProperty(ref _fLuongCNVC, value);
        }

        private double _fLuongHDLD;
        public double FLuongHDLD
        {
            get => _fLuongHDLD;
            set => SetProperty(ref _fLuongHDLD, value);
        }

        private double _fPcSiQuan;
        public double FPcSiQuan
        {
            get => _fPcSiQuan;
            set => SetProperty(ref _fPcSiQuan, value);
        }

        private double _fPcQNCN;
        public double FPcQNCN
        {
            get => _fPcQNCN;
            set => SetProperty(ref _fPcQNCN, value);
        }

        private double _fPcCNVC;
        public double FPcCNVC
        {
            get => _fPcCNVC;
            set => SetProperty(ref _fPcCNVC, value);
        }

        private double _fPcHDLD;
        public double FPcHDLD
        {
            get => _fPcHDLD;
            set => SetProperty(ref _fPcHDLD, value);
        }

        private double _fLuongSiQuanTru;
        public double FLuongSiQuanTru
        {
            get => _fLuongSiQuanTru;
            set
            {
                SetProperty(ref _fLuongSiQuanTru, value);
                OnPropertyChanged(nameof(FLuongQtSiquan));
            }
        }

        private double _fLuongQncnTru;
        public double FLuongQncnTru
        {
            get => _fLuongQncnTru;
            set
            {
                SetProperty(ref _fLuongQncnTru, value);
                OnPropertyChanged(nameof(FLuongQtQncn));
            }
        }

        private double _fLuongCnvqpTru;
        public double FLuongCnvqpTru
        {
            get => _fLuongCnvqpTru;
            set
            {
                SetProperty(ref _fLuongCnvqpTru, value);
                OnPropertyChanged(nameof(FLuongQtCnvc));
            }
        }

        private double _fLuongHdTru;
        public double FLuongHdTru
        {
            get => _fLuongHdTru;
            set
            {
                SetProperty(ref _fLuongHdTru, value);
                OnPropertyChanged(nameof(FLuongQtHd));
            }
        }

        private double _fPhuCapSiQuanTru;
        public double FPhuCapSiQuanTru
        {
            get => _fPhuCapSiQuanTru;
            set
            {
                SetProperty(ref _fPhuCapSiQuanTru, value);
                OnPropertyChanged(nameof(FPhuCapQtSiquan));
            }
        }

        private double _fPhuCapQncnTru;
        public double FPhuCapQncnTru
        {
            get => _fPhuCapQncnTru;
            set
            {
                SetProperty(ref _fPhuCapQncnTru, value);
                OnPropertyChanged(nameof(FPhuCapQtQncn));
            }
        }

        private double _fPhuCapCnvqpTru;
        public double FPhuCapCnvqpTru
        {
            get => _fPhuCapCnvqpTru;
            set
            {
                SetProperty(ref _fPhuCapCnvqpTru, value);
                OnPropertyChanged(nameof(FPhuCapQtCnvc));
            }
        }

        private double _fPhuCapHdTru;
        public double FPhuCapHdTru
        {
            get => _fPhuCapHdTru;
            set
            {
                SetProperty(ref _fPhuCapHdTru, value);
                OnPropertyChanged(nameof(FPhuCapQtHd));
            }
        }

        private double _fNgayAn;
        public double FNgayAn
        {
            get => _fNgayAn;
            set => SetProperty(ref _fNgayAn, value);
        }

        private double _fNgayAnCong;
        public double FNgayAnCong
        {
            get => _fNgayAnCong;
            set => SetProperty(ref _fNgayAnCong, value);
        }

        private double _fNgayAnTru;
        public double FNgayAnTru
        {
            get => _fNgayAnTru;
            set => SetProperty(ref _fNgayAnTru, value);
        }

        private double _fNgayAnQt;
        public double FNgayAnQt
        {
            get => _fNgayAnQt;
            set => SetProperty(ref _fNgayAnQt, value);
        }

        private double _xuatNguSQ;
        public double XuatNguSQ
        {
            get => _xuatNguSQ;
            set => SetProperty(ref _xuatNguSQ, value);
        }

        private double _xuatNguQNCN;
        public double XuatNguQNCN
        {
            get => _xuatNguQNCN;
            set => SetProperty(ref _xuatNguQNCN, value);
        }

        private double _xuatNguCNVC;
        public double XuatNguCNVC
        {
            get => _xuatNguCNVC;
            set => SetProperty(ref _xuatNguCNVC, value);
        }

        private double _xuatNguHSQ;
        public double XuatNguHSQ
        {
            get => _xuatNguHSQ;
            set => SetProperty(ref _xuatNguHSQ, value);
        }

        private double _huuSQ;
        public double HuuSQ
        {
            get => _huuSQ;
            set => SetProperty(ref _huuSQ, value);
        }

        private double _huuQNCN;
        public double HuuQNCN
        {
            get => _huuQNCN;
            set => SetProperty(ref _huuQNCN, value);
        }

        private double _huuCNVC;
        public double HuuCNVC
        {
            get => _huuCNVC;
            set => SetProperty(ref _huuCNVC, value);
        }

        private double _huuHSQ;
        public double HuuHSQ
        {
            get => _huuHSQ;
            set => SetProperty(ref _huuHSQ, value);
        }

        private double _thoiViecSQ;
        public double ThoiViecSQ
        {
            get => _thoiViecSQ;
            set => SetProperty(ref _thoiViecSQ, value);
        }

        private double _thoiViecQNCN;
        public double ThoiViecQNCN
        {
            get => _thoiViecQNCN;
            set => SetProperty(ref _thoiViecQNCN, value);
        }

        private double _thoiViecCNVC;
        public double ThoiViecCNVC
        {
            get => _thoiViecCNVC;
            set => SetProperty(ref _thoiViecCNVC, value);
        }

        private double _thoiViecHSQ;
        public double ThoiViecHSQ
        {
            get => _thoiViecHSQ;
            set => SetProperty(ref _thoiViecHSQ, value);
        }

        private string _sMoTaTinhHinh;
        public string SMoTaTinhHinh
        {
            get => _sMoTaTinhHinh;
            set => SetProperty(ref _sMoTaTinhHinh, value);
        }

        private string _sMoTaKienNghi;
        public string SMoTaKienNghi
        {
            get => _sMoTaKienNghi;
            set => SetProperty(ref _sMoTaKienNghi, value);
        }

        //Them cot
        private double? _fLuongBhxhSiQuanTru;
        public double? FLuongBhxhSiQuanTru
        {
            get => _fLuongBhxhSiQuanTru;
            set
            {
                SetProperty(ref _fLuongBhxhSiQuanTru, value);
                OnPropertyChanged(nameof(FLuongQtSiquan));
            }
        }

        private double? _fLuongBhxhQncnTru;
        public double? FLuongBhxhQncnTru
        {
            get => _fLuongBhxhQncnTru;
            set
            {
                SetProperty(ref _fLuongBhxhQncnTru, value);
                OnPropertyChanged(nameof(FLuongQtQncn));
            }
        }

        private double? _fLuongBhxhCnvqpTru;
        public double? FLuongBhxhCnvqpTru
        {
            get => _fLuongBhxhCnvqpTru;
            set
            {
                SetProperty(ref _fLuongBhxhCnvqpTru, value);
                OnPropertyChanged(nameof(FLuongQtCnvc));
            }
        }

        private double? _fLuongBhxhHdTru;
        public double? FLuongBhxhHdTru
        {
            get => _fLuongBhxhHdTru;
            set
            {
                SetProperty(ref _fLuongBhxhHdTru, value);
                OnPropertyChanged(nameof(FLuongQtHd));
            }
        }

        private double? _fPhuCapBhxhSiQuanTru;
        public double? FPhuCapBhxhSiQuanTru
        {
            get => _fPhuCapBhxhSiQuanTru;
            set
            {
                SetProperty(ref _fPhuCapBhxhSiQuanTru, value);
                OnPropertyChanged(nameof(FPhuCapQtSiquan));
            }
        }

        private double? _fPhuCapBhxhQncnTru;
        public double? FPhuCapBhxhQncnTru
        {
            get => _fPhuCapBhxhQncnTru;
            set
            {
                SetProperty(ref _fPhuCapBhxhQncnTru, value);
                OnPropertyChanged(nameof(FPhuCapQtQncn));
            }
        }

        private double? _fPhuCapBhxhCnvqpTru;
        public double? FPhuCapBhxhCnvqpTru
        {
            get => _fPhuCapBhxhCnvqpTru;
            set
            {
                SetProperty(ref _fPhuCapBhxhCnvqpTru, value);
                OnPropertyChanged(nameof(FPhuCapQtCnvc));
            }
        }

        private double? _fPhuCapBhxhHdTru;
        public double? FPhuCapBhxhHdTru
        {
            get => _fPhuCapBhxhHdTru;
            set
            {
                SetProperty(ref _fPhuCapBhxhHdTru, value);
                OnPropertyChanged(nameof(FPhuCapQtHd));
            }
        }

        private double? _fKinhPhiLuongPcKhac;
        public double? FKinhPhiLuongPcKhac
        {
            get => _fKinhPhiLuongPcKhac;
            set => SetProperty(ref _fKinhPhiLuongPcKhac, value);
        }

        private double? _fKinhPhiPhuCapHsqbs;
        public double? FKinhPhiPhuCapHsqbs
        {
            get => _fKinhPhiPhuCapHsqbs;
            set => SetProperty(ref _fKinhPhiPhuCapHsqbs, value);
        }

        private double? _fKinhPhiAn;
        public double? FKinhPhiAn
        {
            get => _fKinhPhiAn;
            set => SetProperty(ref _fKinhPhiAn, value);
        }

        private int? _iXuatNguSiQuan;
        public int? IXuatNguSiQuan
        {
            get => _iXuatNguSiQuan;
            set => SetProperty(ref _iXuatNguSiQuan, value);
        }

        private int? _iXuatNguQncn;
        public int? IXuatNguQncn
        {
            get => _iXuatNguQncn;
            set => SetProperty(ref _iXuatNguQncn, value);
        }

        private int? _iXuatNguHsqbs;
        public int? IXuatNguHsqbs
        {
            get => _iXuatNguHsqbs;
            set => SetProperty(ref _iXuatNguHsqbs, value);
        }

        private int? _iXuatNguKhac;
        public int? IXuatNguKhac
        {
            get => _iXuatNguKhac;
            set => SetProperty(ref _iXuatNguKhac, value);
        }

        private int? _iHuuSiQuan;
        public int? IHuuSiQuan
        {
            get => _iHuuSiQuan;
            set => SetProperty(ref _iHuuSiQuan, value);
        }

        private int? _iHuuQncn;
        public int? IHuuQncn
        {
            get => _iHuuQncn;
            set => SetProperty(ref _iHuuQncn, value);
        }

        private int? _iHuuHsqbs;
        public int? IHuuHsqbs
        {
            get => _iHuuHsqbs;
            set => SetProperty(ref _iHuuHsqbs, value);
        }

        private int? _iHuuKhac;
        public int? IHuuKhac
        {
            get => _iHuuKhac;
            set => SetProperty(ref _iHuuKhac, value);
        }

        private int? _iThoiViecSiQuan;
        public int? IThoiViecSiQuan
        {
            get => _iThoiViecSiQuan;
            set => SetProperty(ref _iThoiViecSiQuan, value);
        }

        private int? _iThoiViecQncn;
        public int? IThoiViecQncn
        {
            get => _iThoiViecQncn;
            set => SetProperty(ref _iThoiViecQncn, value);
        }

        private int? _iThoiViecHsqbs;
        public int? IThoiViecHsqbs
        {
            get => _iThoiViecHsqbs;
            set => SetProperty(ref _iThoiViecHsqbs, value);
        }

        private int? _iThoiViecKhac;
        public int? IThoiViecKhac
        {
            get => _iThoiViecKhac;
            set => SetProperty(ref _iThoiViecKhac, value);
        }

        public double FLuongQtSiquan => FLuongSiQuan - FLuongSiQuanTru - (FLuongBhxhSiQuanTru ?? 0);
        public double FLuongQtQncn => FLuongQNCN - FLuongQncnTru - (FLuongBhxhQncnTru ?? 0);
        public double FLuongQtHd => FLuongHDLD - FLuongHdTru - (FLuongBhxhHdTru ?? 0);
        public double FLuongQtCnvc => FLuongCNVC - FLuongCnvqpTru - (FLuongBhxhCnvqpTru ?? 0);

        public double FPhuCapQtSiquan => FPcSiQuan - FPhuCapSiQuanTru - (FPhuCapBhxhSiQuanTru ?? 0);
        public double FPhuCapQtQncn => FPcQNCN - FPhuCapQncnTru - (FPhuCapBhxhQncnTru ?? 0);
        public double FPhuCapQtHd => FPcHDLD - FPhuCapHdTru - (FPhuCapBhxhHdTru ?? 0);
        public double FPhuCapQtCnvc => FPcCNVC - FPhuCapCnvqpTru - (FPhuCapBhxhCnvqpTru ?? 0);

        public double TongLuong => FLuongSiQuan + FLuongQNCN + FLuongCNVC + FLuongHDLD;
        public double TongPc => FPcSiQuan + FPcQNCN + FPcCNVC + FPcHDLD;
        public double TongLuongTru => FLuongCnvqpTru + FLuongSiQuanTru + FLuongQncnTru + FLuongHdTru;
        public double TongPCTru => FPhuCapCnvqpTru + FPhuCapHdTru + FPhuCapQncnTru + FPhuCapSiQuanTru;
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNgaySua { get; set; }

    }
}
