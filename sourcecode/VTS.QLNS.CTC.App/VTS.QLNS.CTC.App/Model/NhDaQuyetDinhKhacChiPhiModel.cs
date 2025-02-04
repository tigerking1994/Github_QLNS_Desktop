using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDaQuyetDinhKhacChiPhiModel : ModelBase
    {
        public double? _fGiaTriUsd;
        public double? FGiaTriUsd
        {
            get => _fGiaTriUsd;
            set => SetProperty(ref _fGiaTriUsd, value);

        }
        public double? _fGiaTriVnd;
        public double? FGiaTriVnd
        {
            get => _fGiaTriVnd;
            set => SetProperty(ref _fGiaTriVnd, value);

        }
        public Guid? _iIdDmChiPhiId;
        public Guid? IIdDmChiPhiId
        {
            get => _iIdDmChiPhiId;
            set => SetProperty(ref _iIdDmChiPhiId, value);

        }
        public Guid? IIdParentId { get; set; }
        public Guid? IIdQuyetDinhKhacId { get; set; }
        public string _sMaOrder;
        public string SMaOrder
        {
            get => _sMaOrder;
            set => SetProperty(ref _sMaOrder, value);

        }
        public string STenChiPhi { get; set; }
        public ObservableCollection<NhDmChiPhiModel> ItemsLoaiNoiDungChi { get; set; }
        private bool _isHasChildren;
        public bool IsHasChildren
        {
            get => _isHasChildren;
            set => SetProperty(ref _isHasChildren, value);
        }

        private bool _isEnableEdit;
        public bool IsEnableEdit
        {
            get => _isEnableEdit;
            set => SetProperty(ref _isEnableEdit, value);

        }

        private string _sMaChiPhi;
        public string SMaChiPhi
        {
            get => _sMaChiPhi;
            set => SetProperty(ref _sMaChiPhi, value);
        }
    }
}
