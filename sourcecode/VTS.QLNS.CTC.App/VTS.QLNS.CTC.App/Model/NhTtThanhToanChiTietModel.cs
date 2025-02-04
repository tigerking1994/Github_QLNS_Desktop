using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhTtThanhToanChiTietModel : ModelBase
    {
        public Guid? IIdDeNghiThanhToanId { get; set; }
        public Guid? IIdPhuLucHopDongId { get; set; }
        public Guid? IIdNguonVonId { get; set; }
        public Guid? IIdMlnsMucId { get; set; }
        public Guid? IIdMlnsTieuMucId { get; set; }
        public Guid? IIdMlnsTietMucId { get; set; }
        public Guid? IIdMucId { get; set; }
        public Guid? IIdTieuMucId { get; set; }
        public Guid? IIdTietMucId { get; set; }
        public Guid? IIdLoaiNoiDungChiId { get; set; }

        private string _slns;
        public string SLns
        {
            get => _slns;
            set => SetProperty(ref _slns, value);
        }
        private string _sl;
        public string SL
        {
            get => _sl;
            set => SetProperty(ref _sl, value);
        }
        private string _sk;
        public string SK
        {
            get => _sk;
            set => SetProperty(ref _sk, value);
        }
        private string _sm;
        public string SM
        {
            get => _sm;
            set => SetProperty(ref _sm, value);
        }
        private string _stm;
        public string STm
        {
            get => _stm;
            set => SetProperty(ref _stm, value);
        }
        private string _sttm;
        public string STtm
        {
            get => _sttm;
            set => SetProperty(ref _sttm, value);
        }
        private double? _fDeNghiCapKyNayUsd;
        public double? FDeNghiCapKyNayUsd
        {
            get => _fDeNghiCapKyNayUsd;
            set => SetProperty(ref _fDeNghiCapKyNayUsd, value);
        }
        private double? _fDeNghiCapKyNayVnd;
        public double? FDeNghiCapKyNayVnd
        {
            get => _fDeNghiCapKyNayVnd;
            set => SetProperty(ref _fDeNghiCapKyNayVnd, value);
        }
        private double? _fDeNghiCapKyNayEur;
        public double? FDeNghiCapKyNayEur
        {
            get => _fDeNghiCapKyNayEur;
            set => SetProperty(ref _fDeNghiCapKyNayEur, value);
        }
        private double? _fDeNghiCapKyNayNgoaiTeKhac;
        public double? FDeNghiCapKyNayNgoaiTeKhac
        {
            get => _fDeNghiCapKyNayNgoaiTeKhac;
            set => SetProperty(ref _fDeNghiCapKyNayNgoaiTeKhac, value);
        }
        private double? _fDuocDuyetUsd;
        public double? FDuocDuyetUsd
        {
            get => _fDuocDuyetUsd;
            set => SetProperty(ref _fDuocDuyetUsd, value);
        }
        private double? _fDuocDuyetVnd;
        public double? FDuocDuyetVnd
        {
            get => _fDuocDuyetVnd;
            set => SetProperty(ref _fDuocDuyetVnd, value);
        }
        private double? _fDuocDuyetEur;
        public double? FDuocDuyetEur
        {
            get => _fDuocDuyetEur;
            set => SetProperty(ref _fDuocDuyetEur, value);
        }
        private double? _fDuocDuyetNgoaiTeKhac;
        public double? FDuocDuyetNgoaiTeKhac
        {
            get => _fDuocDuyetNgoaiTeKhac;
            set => SetProperty(ref _fDuocDuyetNgoaiTeKhac, value);
        }
        private double? _fDuocCapKyTruocNgoaiTeKhac;
        public double? FDuocCapKyTruocNgoaiTeKhac
        {
            get => _fDuocCapKyTruocNgoaiTeKhac;
            set => SetProperty(ref _fDuocCapKyTruocNgoaiTeKhac, value);
        }
        private double? _fDuocCapKyTruocUsd;
        public double? FDuocCapKyTruocUsd
        {
            get => _fDuocCapKyTruocUsd;
            set => SetProperty(ref _fDuocCapKyTruocUsd, value);
        }
        private double? _fDuocCapKyTruocVnd;
        public double? FDuocCapKyTruocVnd
        {
            get => _fDuocCapKyTruocVnd;
            set => SetProperty(ref _fDuocCapKyTruocVnd, value);
        }
        private double? _fDuocCapKyTruocEur;
        public double? FDuocCapKyTruocEur
        {
            get => _fDuocCapKyTruocEur;
            set => SetProperty(ref _fDuocCapKyTruocEur, value);
        }
        private double? _fPheDuyetCapKyNayNgoaiTeKhac;
        public double? FPheDuyetCapKyNayNgoaiTeKhac
        {
            get => _fPheDuyetCapKyNayNgoaiTeKhac;
            set => SetProperty(ref _fPheDuyetCapKyNayNgoaiTeKhac, value);
        }
        private double? _fPheDuyetCapKyNayUsd;
        public double? FPheDuyetCapKyNayUsd
        {
            get => _fPheDuyetCapKyNayUsd;
            set => SetProperty(ref _fPheDuyetCapKyNayUsd, value);
        }
        private double? _fPheDuyetCapKyNayVnd;
        public double? FPheDuyetCapKyNayVnd
        {
            get => _fPheDuyetCapKyNayVnd;
            set => SetProperty(ref _fPheDuyetCapKyNayVnd, value);
        }
        private double? _fPheDuyetCapKyNayEur;
        public double? FPheDuyetCapKyNayEur
        {
            get => _fPheDuyetCapKyNayEur;
            set => SetProperty(ref _fPheDuyetCapKyNayEur, value);
        }
        private double?_fTongGiaTriTheoHoaDonVnd;
        public double? FTongGiaTriTheoHoaDonVnd
        {
            get => _fTongGiaTriTheoHoaDonVnd;
            set => SetProperty(ref _fTongGiaTriTheoHoaDonVnd, value);
        }
        private double? _fTongGiaTriTheoHoaDonEur;
        public double? FTongGiaTriTheoHoaDonEur
        {
            get => _fTongGiaTriTheoHoaDonEur;
            set => SetProperty(ref _fTongGiaTriTheoHoaDonEur, value);
        }
        private double? _fTongGiaTriTheoHoaDonNgoaiTeKhac;
        public double? FTongGiaTriTheoHoaDonNgoaiTeKhac
        {
            get => _fTongGiaTriTheoHoaDonNgoaiTeKhac;
            set => SetProperty(ref _fTongGiaTriTheoHoaDonNgoaiTeKhac, value);
        }
        private double? _fTongGiaTriTheoHoaDonUsd;
        public double? FTongGiaTriTheoHoaDonUsd
        {
            get => _fTongGiaTriTheoHoaDonUsd;
            set => SetProperty(ref _fTongGiaTriTheoHoaDonUsd, value);
        }
        private double? _fTiLeThanhToan;
        public double? FTiLeThanhToan
        {
            get => _fTiLeThanhToan;
            set => SetProperty(ref _fTiLeThanhToan, value);
        }

        private string _sTenNoiDungChi;
        public string STenNoiDungChi
        {
            get => _sTenNoiDungChi;
            set => SetProperty(ref _sTenNoiDungChi, value);
        }
        public Guid? IIdMucLucNganSachId { get; set; }
        public Guid? IIdMlnsId { get; set; }

        public string sMucLucNganSach { get; set; }
        public bool _isDeNghiThanhToan;
        public bool IsDeNghiThanhToan
        {
            get => _isDeNghiThanhToan;
            set => SetProperty(ref _isDeNghiThanhToan, value);
        }

        public double? FGiaTriUsdChangeCol4
        {
            get
            {
                if (_isDeNghiThanhToan)
                    return (FDuocCapKyTruocUsd.GetValueOrDefault(0)) + (FDeNghiCapKyNayUsd.GetValueOrDefault(0));
                else
                    return (FDuocCapKyTruocUsd.GetValueOrDefault(0)) + (FPheDuyetCapKyNayUsd.GetValueOrDefault(0));
            }
        }

        public double? FGiaTriVndChangeCol4
        {
            get
            {
                if (_isDeNghiThanhToan)
                    return (FDuocCapKyTruocVnd.GetValueOrDefault(0) + FDeNghiCapKyNayVnd.GetValueOrDefault(0));
                else
                    return (FDuocCapKyTruocVnd.GetValueOrDefault(0) + FPheDuyetCapKyNayVnd.GetValueOrDefault(0));
            }
        }
        public double? FGiaTriEurChangeCol4
        {
            get
            {
                if (_isDeNghiThanhToan)
                    return (FDuocCapKyTruocEur.GetValueOrDefault(0) + FDeNghiCapKyNayEur.GetValueOrDefault(0));

                else
                    return (FDuocCapKyTruocEur.GetValueOrDefault(0) + FPheDuyetCapKyNayEur.GetValueOrDefault(0));
            }
        }

        public double? FGiaTriKinhPhiKhacChangeCol4
        {
            get
            {
                if (_isDeNghiThanhToan)
                    return (FDuocCapKyTruocNgoaiTeKhac.GetValueOrDefault(0) + FDeNghiCapKyNayNgoaiTeKhac.GetValueOrDefault(0));

                else
                    return (FDuocCapKyTruocNgoaiTeKhac.GetValueOrDefault(0) + FPheDuyetCapKyNayNgoaiTeKhac.GetValueOrDefault(0));
            }
        }

        public double? FTiLeThanhToanUsdChange
        {
            get
            {
                if (NumberUtils.DoubleIsNullOrZero(FDuocDuyetUsd))
                    return 0;
                return (FGiaTriUsdCol4.GetValueOrDefault(0) / FDuocDuyetUsd * 100);
            }

        }

        public double? FTiLeThanhToanVNDChange
        {
            get
            {
                if (NumberUtils.DoubleIsNullOrZero(FDuocDuyetVnd))
                    return 0;
                return (FGiaTriVndCol4.GetValueOrDefault(0) / FDuocDuyetVnd * 100);
            }

        }

        public double? _fGiaTriUsdCol4;
        public double? FGiaTriUsdCol4
        {
            get => _fGiaTriUsdCol4;
            set => SetProperty(ref _fGiaTriUsdCol4, value);
        }

        public double? _fGiaTriVndCol4;
        public double? FGiaTriVndCol4
        {
            get => _fGiaTriVndCol4;
            set => SetProperty(ref _fGiaTriVndCol4, value);

        }

        public double? _fGiaTriEurCol4;
        public double? FGiaTriEurCol4
        {
            get => _fGiaTriEurCol4;
            set => SetProperty(ref _fGiaTriEurCol4, value);

        }

        public double? _fGiatriNgoaiTeKhacCol4;
        public double? FGiatriNgoaiTeKhacCol4
        {
            get => _fGiatriNgoaiTeKhacCol4;
            set => SetProperty(ref _fGiatriNgoaiTeKhacCol4, value);
        }

        public bool _flagMessage;
        public bool FlagMessage
        {
            get => _flagMessage;
            set => SetProperty(ref _flagMessage, value);
        }
    }
}
