using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class SettlementVoucherDetailExplainModel : BindableBase
    {
        private Guid _id;
        public Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private Guid _iIdQtchungTu;
        public Guid IIdQtchungTu
        {
            get => _iIdQtchungTu;
            set => SetProperty(ref _iIdQtchungTu, value);
        }

        private string _iIdGiaiThich;
        public string IIdGiaiThich
        {
            get => _iIdGiaiThich;
            set => SetProperty(ref _iIdGiaiThich, value);
        }

        private int _iThangQuy;
        public int IThangQuy
        {
            get => _iThangQuy;
            set => SetProperty(ref _iThangQuy, value);
        }

        private int _iThangQuyLoai;
        public int IThangQuyLoai
        {
            get => _iThangQuyLoai;
            set => SetProperty(ref _iThangQuyLoai, value);
        }

        private string _iIdMaDonVi;
        public string IIdMaDonVi
        {
            get => _iIdMaDonVi;
            set => SetProperty(ref _iIdMaDonVi, value);
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

        private string _sMoTa;
        public string SMoTa
        {
            get => _sMoTa;
            set => SetProperty(ref _sMoTa, value);
        }

        private double _fLuongSiQuan;
        public double FLuongSiQuan
        {
            get => _fLuongSiQuanQt + _fLuongSiQuanTru + _fLuongBhxhSiQuanTru;
            set => SetProperty(ref _fLuongSiQuan, value);
        }

        private double _fLuongSiQuanTru;
        public double FLuongSiQuanTru
        {
            get => _fLuongSiQuanTru;
            set
            {
                SetProperty(ref _fLuongSiQuanTru, value);
                OnPropertyChanged(nameof(FLuongSiQuan));
            }
        }

        private double _fLuongSiQuanQt;
        public double FLuongSiQuanQt
        {
            get => _fLuongSiQuanQt;
            set
            {
                SetProperty(ref _fLuongSiQuanQt, value);
                OnPropertyChanged(nameof(FLuongSiQuan));
            }
        }

        private double _fLuongQncn;
        public double FLuongQncn
        {
            get => _fLuongQncnQt + _fLuongQncnTru + _fLuongBhxhQncnTru;
            set => SetProperty(ref _fLuongQncn, value);
        }

        private double _fLuongQncnTru;
        public double FLuongQncnTru
        {
            get => _fLuongQncnTru;
            set
            {
                SetProperty(ref _fLuongQncnTru, value);
                OnPropertyChanged(nameof(FLuongQncn));
            }
        }

        private double _fLuongQncnQt;
        public double FLuongQncnQt
        {
            get => _fLuongQncnQt;
            set
            {
                SetProperty(ref _fLuongQncnQt, value);
                OnPropertyChanged(nameof(FLuongQncn));
            }
        }

        private double _fLuongCnvqp;
        public double FLuongCnvqp
        {
            get => _fLuongCnvqpQt + _fLuongCnvqpTru + _fLuongBhxhCnvqpTru;
            set => SetProperty(ref _fLuongCnvqp, value);
        }

        private double _fLuongCnvqpTru;
        public double FLuongCnvqpTru
        {
            get => _fLuongCnvqpTru;
            set
            {
                SetProperty(ref _fLuongCnvqpTru, value);
                OnPropertyChanged(nameof(FLuongCnvqp));
            }
        }

        private double _fLuongCnvqpQt;
        public double FLuongCnvqpQt
        {
            get => _fLuongCnvqpQt;
            set
            {
                SetProperty(ref _fLuongCnvqpQt, value);
                OnPropertyChanged(nameof(FLuongCnvqp));
            }
        }

        private double _fLuongHd;
        public double FLuongHd
        {
            get => _fLuongHdQt + _fLuongHdTru + _fLuongBhxhHdTru;
            set => SetProperty(ref _fLuongHd, value);
        }

        private double _fLuongHdTru;
        public double FLuongHdTru
        {
            get => _fLuongHdTru;
            set
            {
                SetProperty(ref _fLuongHdTru, value);
                OnPropertyChanged(nameof(FLuongHd));
            }
        }

        private double _fLuongHdQt;
        public double FLuongHdQt
        {
            get => _fLuongHdQt;
            set
            {
                SetProperty(ref _fLuongHdQt, value);
                OnPropertyChanged(nameof(FLuongHd));
            }
        }

        private double _fPhuCapSiQuan;
        public double FPhuCapSiQuan
        {
            get => _fPhuCapSiQuanQt + _fPhuCapSiQuanTru + _fPhuCapBhxhSiQuanTru;
            set => SetProperty(ref _fPhuCapSiQuan, value);
        }

        private double _fPhuCapSiQuanTru;
        public double FPhuCapSiQuanTru
        {
            get => _fPhuCapSiQuanTru;
            set
            {
                SetProperty(ref _fPhuCapSiQuanTru, value);
                OnPropertyChanged(nameof(FPhuCapSiQuan));
            }
        }

        private double _fPhuCapSiQuanQt;
        public double FPhuCapSiQuanQt
        {
            get => _fPhuCapSiQuanQt;
            set
            {
                SetProperty(ref _fPhuCapSiQuanQt, value);
                OnPropertyChanged(nameof(FPhuCapSiQuan));
            }
        }

        private double _fPhuCapQncn;
        public double FPhuCapQncn
        {
            get => _fPhuCapQncnQt + _fPhuCapQncnTru + _fPhuCapBhxhQncnTru;
            set => SetProperty(ref _fPhuCapQncn, value);
        }

        private double _fPhuCapQncnTru;
        public double FPhuCapQncnTru
        {
            get => _fPhuCapQncnTru;
            set
            {
                SetProperty(ref _fPhuCapQncnTru, value);
                OnPropertyChanged(nameof(FPhuCapQncn));
            }
        }

        private double _fPhuCapQncnQt;
        public double FPhuCapQncnQt
        {
            get => _fPhuCapQncnQt;
            set
            {
                SetProperty(ref _fPhuCapQncnQt, value);
                OnPropertyChanged(nameof(FPhuCapQncn));
            }
        }

        private double _fPhuCapCnvqp;
        public double FPhuCapCnvqp
        {
            get => _fPhuCapCnvqpQt + _fPhuCapCnvqpTru + _fPhuCapBhxhCnvqpTru;
            set => SetProperty(ref _fPhuCapCnvqp, value);
        }

        private double _fPhuCapCnvqpTru;
        public double FPhuCapCnvqpTru
        {
            get => _fPhuCapCnvqpTru;
            set
            {
                SetProperty(ref _fPhuCapCnvqpTru, value);
                OnPropertyChanged(nameof(FPhuCapCnvqp));
            }
        }

        private double _fPhuCapCnvqpQt;
        public double FPhuCapCnvqpQt
        {
            get => _fPhuCapCnvqpQt;
            set
            {
                SetProperty(ref _fPhuCapCnvqpQt, value);
                OnPropertyChanged(nameof(FPhuCapCnvqp));
            }
        }

        private double _fPhuCapHd;
        public double FPhuCapHd
        {
            get => _fPhuCapHdQt + _fPhuCapHdTru + _fPhuCapBhxhHdTru;
            set => SetProperty(ref _fPhuCapHd, value);
        }

        private double _fPhuCapHdTru;
        public double FPhuCapHdTru
        {
            get => _fPhuCapHdTru;
            set
            {
                SetProperty(ref _fPhuCapHdTru, value);
                OnPropertyChanged(nameof(FPhuCapHd));
            }
        }

        private double _fPhuCapHdQt;
        public double FPhuCapHdQt
        {
            get => _fPhuCapHdQt;
            set
            {
                SetProperty(ref _fPhuCapHdQt, value);
                OnPropertyChanged(nameof(FPhuCapHd));
            }
        }

        private double _fNgayAn;
        public double FNgayAn
        {
            get => _fNgayAn;
            set
            {
                SetProperty(ref _fNgayAn, value);
                OnPropertyChanged(nameof(FNgayAnQt));
            }
        }

        private double _fNgayAnCong;
        public double FNgayAnCong
        {
            get => _fNgayAnCong;
            set
            {
                SetProperty(ref _fNgayAnCong, value);
                OnPropertyChanged(nameof(FNgayAnQt));
            }
        }

        private double _fNgayAnTru;
        public double FNgayAnTru
        {
            get => _fNgayAnTru;
            set
            {
                SetProperty(ref _fNgayAnTru, value);
                OnPropertyChanged(nameof(FNgayAnQt));
            }
        }

        private double _fNgayAnQt;
        public double FNgayAnQt
        {
            get => _fNgayAn + _fNgayAnCong - _fNgayAnTru;
            set => SetProperty(ref _fNgayAnQt, value);
        }

        private double _fRaQuanSiQuanNguoiXuatNgu;
        public double FRaQuanSiQuanNguoiXuatNgu
        {
            get => _fRaQuanSiQuanNguoiXuatNgu;
            set => SetProperty(ref _fRaQuanSiQuanNguoiXuatNgu, value);
        }

        private double _fRaQuanSiQuanTienXuatNgu;
        public double FRaQuanSiQuanTienXuatNgu
        {
            get => _fRaQuanSiQuanTienXuatNgu;
            set => SetProperty(ref _fRaQuanSiQuanTienXuatNgu, value);
        }

        private double _fRaQuanSiQuanNguoiHuu;
        public double FRaQuanSiQuanNguoiHuu
        {
            get => _fRaQuanSiQuanNguoiHuu;
            set => SetProperty(ref _fRaQuanSiQuanNguoiHuu, value);
        }

        private double _fRaQuanSiQuanTienHuu;
        public double FRaQuanSiQuanTienHuu
        {
            get => _fRaQuanSiQuanTienHuu;
            set => SetProperty(ref _fRaQuanSiQuanTienHuu, value);
        }

        private double _fRaQuanSiQuanNguoiThoiViec;
        public double FRaQuanSiQuanNguoiThoiViec
        {
            get => _fRaQuanSiQuanNguoiThoiViec;
            set => SetProperty(ref _fRaQuanSiQuanNguoiThoiViec, value);
        }

        private double _fRaQuanSiQuanTienThoiViec;
        public double FRaQuanSiQuanTienThoiViec
        {
            get => _fRaQuanSiQuanTienThoiViec;
            set => SetProperty(ref _fRaQuanSiQuanTienThoiViec, value);
        }

        private double _fRaQuanQncnNguoiXuatNgu;
        public double FRaQuanQncnNguoiXuatNgu
        {
            get => _fRaQuanQncnNguoiXuatNgu;
            set => SetProperty(ref _fRaQuanQncnNguoiXuatNgu, value);
        }

        private double _fRaQuanQncnTienXuatNgu;
        public double FRaQuanQncnTienXuatNgu
        {
            get => _fRaQuanQncnTienXuatNgu;
            set => SetProperty(ref _fRaQuanQncnTienXuatNgu, value);
        }

        private double _fRaQuanQncnNguoiHuu;
        public double FRaQuanQncnNguoiHuu
        {
            get => _fRaQuanQncnNguoiHuu;
            set => SetProperty(ref _fRaQuanQncnNguoiHuu, value);
        }

        private double _fRaQuanQncnTienHuu;
        public double FRaQuanQncnTienHuu
        {
            get => _fRaQuanQncnTienHuu;
            set => SetProperty(ref _fRaQuanQncnTienHuu, value);
        }

        private double _fRaQuanQncnNguoiThoiViec;
        public double FRaQuanQncnNguoiThoiViec
        {
            get => _fRaQuanQncnNguoiThoiViec;
            set => SetProperty(ref _fRaQuanQncnNguoiThoiViec, value);
        }

        private double _fRaQuanQncnTienThoiViec;
        public double FRaQuanQncnTienThoiViec
        {
            get => _fRaQuanQncnTienThoiViec;
            set => SetProperty(ref _fRaQuanQncnTienThoiViec, value);
        }

        private double _fRaQuanCnvqpNguoiXuatNgu;
        public double FRaQuanCnvqpNguoiXuatNgu
        {
            get => _fRaQuanCnvqpNguoiXuatNgu;
            set => SetProperty(ref _fRaQuanCnvqpNguoiXuatNgu, value);
        }

        private double _fRaQuanCnvqpTienXuatNgu;
        public double FRaQuanCnvqpTienXuatNgu
        {
            get => _fRaQuanCnvqpTienXuatNgu;
            set => SetProperty(ref _fRaQuanCnvqpTienXuatNgu, value);
        }

        private double _fRaQuanCnvqpNguoiHuu;
        public double FRaQuanCnvqpNguoiHuu
        {
            get => _fRaQuanCnvqpNguoiHuu;
            set => SetProperty(ref _fRaQuanCnvqpNguoiHuu, value);
        }

        private double _fRaQuanCnvqpTienHuu;
        public double FRaQuanCnvqpTienHuu
        {
            get => _fRaQuanCnvqpTienHuu;
            set => SetProperty(ref _fRaQuanCnvqpTienHuu, value);
        }

        private double _fRaQuanCnvqpNguoiThoiViec;
        public double FRaQuanCnvqpNguoiThoiViec
        {
            get => _fRaQuanCnvqpNguoiThoiViec;
            set => SetProperty(ref _fRaQuanCnvqpNguoiThoiViec, value);
        }

        private double _fRaQuanCnvqpTienThoiViec;
        public double FRaQuanCnvqpTienThoiViec
        {
            get => _fRaQuanCnvqpTienThoiViec;
            set => SetProperty(ref _fRaQuanCnvqpTienThoiViec, value);
        }

        private double _fRaQuanHsqcsNguoiXuatNgu;
        public double FRaQuanHsqcsNguoiXuatNgu
        {
            get => _fRaQuanHsqcsNguoiXuatNgu;
            set => SetProperty(ref _fRaQuanHsqcsNguoiXuatNgu, value);
        }

        private double _fRaQuanHsqcsTienXuatNgu;
        public double FRaQuanHsqcsTienXuatNgu
        {
            get => _fRaQuanHsqcsTienXuatNgu;
            set => SetProperty(ref _fRaQuanHsqcsTienXuatNgu, value);
        }

        private double _fRaQuanHsqcsNguoiHuu;
        public double FRaQuanHsqcsNguoiHuu
        {
            get => _fRaQuanHsqcsNguoiHuu;
            set => SetProperty(ref _fRaQuanHsqcsNguoiHuu, value);
        }

        private double _fRaQuanHsqcsTienHuu;
        public double FRaQuanHsqcsTienHuu
        {
            get => _fRaQuanHsqcsTienHuu;
            set => SetProperty(ref _fRaQuanHsqcsTienHuu, value);
        }

        private double _fRaQuanHsqcsNguoiThoiViec;
        public double FRaQuanHsqcsNguoiThoiViec
        {
            get => _fRaQuanHsqcsNguoiThoiViec;
            set => SetProperty(ref _fRaQuanHsqcsNguoiThoiViec, value);
        }

        private double _fRaQuanHsqcsTienThoiViec;
        public double FRaQuanHsqcsTienThoiViec
        {
            get => _fRaQuanHsqcsTienThoiViec;
            set => SetProperty(ref _fRaQuanHsqcsTienThoiViec, value);
        }

        private int _iNamLamViec;
        public int INamLamViec
        {
            get => _iNamLamViec;
            set => SetProperty(ref _iNamLamViec, value);
        }

        private DateTime? _dNgayTao;
        public DateTime? DNgayTao
        {
            get => _dNgayTao;
            set => SetProperty(ref _dNgayTao, value);
        }

        private string _sNguoiTao;
        public string SNguoiTao
        {
            get => _sNguoiTao;
            set => SetProperty(ref _sNguoiTao, value);
        }

        private DateTime? _dNgaySua;
        public DateTime? DNgaySua
        {
            get => _dNgaySua;
            set => SetProperty(ref _dNgaySua, value);
        }

        private string _sNguoiSua;
        public string SNguoiSua
        {
            get => _sNguoiSua;
            set => SetProperty(ref _sNguoiSua, value);
        }

        private double _fLuongBhxhSiQuanTru;
        public double FLuongBhxhSiQuanTru 
        {
            get => _fLuongBhxhSiQuanTru;
            set
            {
                SetProperty(ref _fLuongBhxhSiQuanTru, value);
                OnPropertyChanged(nameof(FLuongSiQuan));
            }
        }

        private double _fLuongBhxhQncnTru;
        public double FLuongBhxhQncnTru 
        {
            get => _fLuongBhxhQncnTru;
            set
            {
                SetProperty(ref _fLuongBhxhQncnTru, value);
                OnPropertyChanged(nameof(FLuongQncn));
            }
        }

        private double _fLuongBhxhCnvqpTru;
        public double FLuongBhxhCnvqpTru
        {
            get => _fLuongBhxhCnvqpTru;
            set
            {
                SetProperty(ref _fLuongBhxhCnvqpTru, value);
                OnPropertyChanged(nameof(FLuongCnvqp));
            }
        }

        private double _fLuongBhxhHdTru;
        public double FLuongBhxhHdTru
        {
            get => _fLuongBhxhHdTru;
            set
            {
                SetProperty(ref _fLuongBhxhHdTru, value);
                OnPropertyChanged(nameof(FLuongHd));
            }
        }

        private double _fPhuCapBhxhSiQuanTru;
        public double FPhuCapBhxhSiQuanTru
        {
            get => _fPhuCapBhxhSiQuanTru;
            set
            {
                SetProperty(ref _fPhuCapBhxhSiQuanTru, value);
                OnPropertyChanged(nameof(FPhuCapSiQuan));
            }
        }

        private double _fPhuCapBhxhQncnTru;
        public double FPhuCapBhxhQncnTru
        {
            get => _fPhuCapBhxhQncnTru;
            set
            {
                SetProperty(ref _fPhuCapBhxhQncnTru, value);
                OnPropertyChanged(nameof(FPhuCapQncn));
            }
        }

        private double _fPhuCapBhxhCnvqpTru;
        public double FPhuCapBhxhCnvqpTru
        {
            get => _fPhuCapBhxhCnvqpTru;
            set
            {
                SetProperty(ref _fPhuCapBhxhCnvqpTru, value);
                OnPropertyChanged(nameof(FPhuCapCnvqp));
            }
        }

        private double _fPhuCapBhxhHdTru;
        public double FPhuCapBhxhHdTru
        {
            get => _fPhuCapBhxhHdTru;
            set
            {
                SetProperty(ref _fPhuCapBhxhHdTru, value);
                OnPropertyChanged(nameof(FPhuCapHd));
            }
        }

        private double _fKinhPhiLuongPcKhac;
        public double FKinhPhiLuongPcKhac
        {
            get => _fKinhPhiLuongPcKhac;
            set => SetProperty(ref _fKinhPhiLuongPcKhac, value);
        }

        private double _fKinhPhiPhuCapHsqbs;
        public double FKinhPhiPhuCapHsqbs
        {
            get => _fKinhPhiPhuCapHsqbs;
            set => SetProperty(ref _fKinhPhiPhuCapHsqbs, value);
        }

        private double _fKinhPhiAn;
        public double FKinhPhiAn
        {
            get => _fKinhPhiAn;
            set => SetProperty(ref _fKinhPhiAn, value);
        }
    }
}
