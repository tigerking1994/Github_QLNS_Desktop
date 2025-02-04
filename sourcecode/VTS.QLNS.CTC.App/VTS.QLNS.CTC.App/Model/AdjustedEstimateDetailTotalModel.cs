using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class AdjustedEstimateDetailTotalModel : BindableBase
    {
        private double _fTongDuToanNganSachNam;
        public double FTongDuToanNganSachNam
        {
            get => _fTongDuToanNganSachNam;
            set => SetProperty(ref _fTongDuToanNganSachNam, value);
        }

        private double _fTongDuToanChuyenNamSau;
        public double FTongDuToanChuyenNamSau
        {
            get => _fTongDuToanChuyenNamSau;
            set => SetProperty(ref _fTongDuToanChuyenNamSau, value);
        }

        private double _fTongDuToanConLai;
        public double FTongDuToanConLai
        {
            get => _fTongDuToanConLai;
            set
            {
                SetProperty(ref _fTongDuToanConLai, value);
            }
        }

        private double _fTongDuKienQuyetToanDauNam;
        public double FTongDuKienQuyetToanDauNam
        {
            get => _fTongDuKienQuyetToanDauNam;
            set => SetProperty(ref _fTongDuKienQuyetToanDauNam, value);
        }

        private double _fTongDuKienQuyetToanCuoiNam;
        public double FTongDuKienQuyetToanCuoiNam
        {
            get => _fTongDuKienQuyetToanCuoiNam;
            set => SetProperty(ref _fTongDuKienQuyetToanCuoiNam, value);
        }

        private double _fTongTongCong;
        public double FTongTongCong
        {
            get => _fTongTongCong;
            set
            {
                SetProperty(ref _fTongTongCong, value);
            }
        }

       
        private double _fTongTang;
        public double FTongTang
        {
            get => _fTongTang;
            set => SetProperty(ref _fTongTang, value);
        }

        private double _fTongGiam;
        public double FTongGiam
        {
            get => _fTongGiam;
            set => SetProperty(ref _fTongGiam, value);
        }

        public AdjustedEstimateDetailTotalModel()
        {
            _fTongDuToanNganSachNam = 0;
            _fTongDuKienQuyetToanDauNam = 0;
            _fTongDuToanChuyenNamSau = 0;
            _fTongDuKienQuyetToanCuoiNam = 0;
            _fTongTongCong = 0;
            _fTongDuToanConLai = 0;
            _fTongTang = 0;
            _fTongGiam = 0;
        }
    }
}
