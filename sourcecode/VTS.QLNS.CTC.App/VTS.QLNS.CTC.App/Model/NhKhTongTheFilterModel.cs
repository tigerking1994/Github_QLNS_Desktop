using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhKhTongTheFilterModel : BindableBase
    {
        public NhKhTongTheFilterModel()
        {
            SSoKeHoachTtcp = string.Empty;
            DNgayBanHanhTtcp = null;
            SMoTaKeHoachTtcp = null;
            SSoKeHoachBqp = string.Empty;
            DNgayBanHanhBqp = null;
            SMoTaKeHoachBqp = null;
            SNam = string.Empty;
            IGiaiDoanTu_TTCP = null;
            IGiaiDoanDen_TTCP = null;
            IGiaiDoanTu_BQP = null;
            IGiaiDoanDen_BQP = null;
        }

        private string _sSoKeHoachTtcp;
        public string SSoKeHoachTtcp
        {
            get => _sSoKeHoachTtcp;
            set => SetProperty(ref _sSoKeHoachTtcp, value);
        }

        private DateTime? _dNgayBanHanhTtcp;
        public DateTime? DNgayBanHanhTtcp
        {
            get => _dNgayBanHanhTtcp;
            set => SetProperty(ref _dNgayBanHanhTtcp, value);
        }

        private string _sMoTaKeHoachTtcp;
        public string SMoTaKeHoachTtcp
        {
            get => _sMoTaKeHoachTtcp;
            set => SetProperty(ref _sMoTaKeHoachTtcp, value);
        }

        private string _sSoKeHoachBqp;
        public string SSoKeHoachBqp
        {
            get => _sSoKeHoachBqp;
            set => SetProperty(ref _sSoKeHoachBqp, value);
        }

        private DateTime? _dNgayBanHanhBqp;
        public DateTime? DNgayBanHanhBqp
        {
            get => _dNgayBanHanhBqp;
            set => SetProperty(ref _dNgayBanHanhBqp, value);
        }

        private string _sMoTaKeHoachBqp;
        public string SMoTaKeHoachBqp
        {
            get => _sMoTaKeHoachBqp;
            set => SetProperty(ref _sMoTaKeHoachBqp, value);
        }
        private string _sNam;
        public string SNam
        {
            get => _sNam;
            set => SetProperty(ref _sNam, value);
        }

        private int? _iGiaiDoanTu_TTCP;
        public int? IGiaiDoanTu_TTCP
        {
            get => _iGiaiDoanTu_TTCP;
            set => SetProperty(ref _iGiaiDoanTu_TTCP, value);
        }

        private int? _iGiaiDoanDen_TTCP;
        public int? IGiaiDoanDen_TTCP
        {
            get => _iGiaiDoanDen_TTCP;
            set => SetProperty(ref _iGiaiDoanDen_TTCP, value);
        }

        private int? _iGiaiDoanTu_BQP;
        public int? IGiaiDoanTu_BQP
        {
            get => _iGiaiDoanTu_BQP;
            set => SetProperty(ref _iGiaiDoanTu_BQP, value);
        }

        private int? _iGiaiDoanDen_BQP;
        public int? IGiaiDoanDen_BQP
        {
            get => _iGiaiDoanDen_BQP;
            set => SetProperty(ref _iGiaiDoanDen_BQP, value);
        }
    }
}