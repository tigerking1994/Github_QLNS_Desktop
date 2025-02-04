using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtKhlcntChiPhiNguonVonCanCuModel : ModelBase
    {
        private Guid _iIdCanCuId;
        public Guid IIdCanCuId
        {
            get => _iIdCanCuId;
            set => SetProperty(ref _iIdCanCuId, value);
        }

        private string _sNoiDung;
        public string SNoiDung
        {
            get => _sNoiDung;
            set => SetProperty(ref _sNoiDung, value);
        }

        private double _fGiaTriPheDuyet;
        public double FGiaTriPheDuyet
        {
            get => _fGiaTriPheDuyet;
            set => SetProperty(ref _fGiaTriPheDuyet, value);
        }

        private int _iLoai;
        public int ILoai
        {
            get => _iLoai;
            set => SetProperty(ref _iLoai, value);
        }

        public Guid? IIdParentId { get; set; }

        private string _sMaOrder;
        public string SMaOrder
        {
            get => _sMaOrder;
            set => SetProperty(ref _sMaOrder, value);
        }
    }
}
