using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.KeHoachChiQuy
{
    public class KeHoachChiQuyDetailViewModel : DetailViewModelBase<VdtNcNhuCauChiModel, VdtNcNhuCauChiChiTietModel>
    {
        private readonly IVdtNcNhuCauChiService _service;
        private readonly ISessionService _sessionService;
        private IMapper _mapper;

        public override string Title => "Quản lý kế hoạch chi Quý";
        public override string Name => "Quản lý kế hoạch chi Quý chi tiết";
        public string txtGiaiNganQuy => string.Format("Số giải ngân quý {0} năm {1}", Model.iQuy, Model.iNamKeHoach);

        private bool _bIsDetail;
        public bool BIsDetail
        {
            get => _bIsDetail;
            set => SetProperty(ref _bIsDetail, value);
        }

        public bool IsUpdateData => !BIsDetail;

        public RelayCommand SaveDataCommand { get; }

        public KeHoachChiQuyDetailViewModel(
            IVdtNcNhuCauChiService service,
            ISessionService sessionService,
            IMapper mapper)
        {
            _service = service;
            _sessionService = sessionService;
            _mapper = mapper;
            SaveDataCommand = new RelayCommand(obj => OnSaveData());
        }

        #region RelayCommand
        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(10);
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            var data = _service.GetNhuCauChiDetail(Model.iID_MaDonViQuanLy, Model.iNamKeHoach.Value, Model.iID_NguonVonID.Value, Model.iQuy);
            var detailData = _service.GetDetailByParent(Model.Id);
            if (detailData != null)
            {
                foreach (var item in detailData)
                {
                    var currentData = data.FirstOrDefault(n => n.iID_DuAnId == item.IIdDuAnId && n.sLoaiThanhToan == item.SLoaiThanhToan);
                    if (currentData != null)
                    {
                        currentData.fGiaTriDeNghi = item.FGiaTriDeNghi ?? 0;
                        currentData.sGhiChu = item.SGhiChu;
                    }
                }
            }
            Items = _mapper.Map<ObservableCollection<VdtNcNhuCauChiChiTietModel>>(data);
            OnPropertyChanged(nameof(Items));
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
        }

        public void OnSaveData()
        {
            if (!Validate()) return;
            List<VdtNcNhuCauChiChiTiet> lstData = new List<VdtNcNhuCauChiChiTiet>();
            foreach (var item in Items.Where(n => !n.IsDeleted && n.fGiaTriDeNghi != 0))
            {
                lstData.Add(ConvertData(item));
            }
            _service.InsertDetailData(Model.Id, lstData);
            MessageBox.Show(Resources.MsgSaveDone);
            LoadData();
        }

        protected override void OnDelete()
        {
            if (SelectedItem != null)
            {
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
            }
            OnPropertyChanged(nameof(Items));
        }
        #endregion

        #region Helper
        private bool Validate()
        {
            List<string> lstError = new List<string>();

            if (Items == null || !Items.Any(n => !n.IsDeleted && n.fGiaTriDeNghi != 0))
            {
                lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "chứng từ chi tiết"));
            }

            if (lstError != null && lstError.Count != 0)
            {
                MessageBox.Show(string.Join("\n", lstError));
                return false;
            }
            return true;
        }

        private VdtNcNhuCauChiChiTiet ConvertData(VdtNcNhuCauChiChiTietModel item)
        {
            VdtNcNhuCauChiChiTiet data = new VdtNcNhuCauChiChiTiet();
            data.Id = Guid.NewGuid();
            data.FGiaTriDeNghi = item.fGiaTriDeNghi;
            data.IIdDuAnId = item.iID_DuAnId;
            data.IIdLoaiCongTrinhId = item.iID_LoaiCongTrinhId;
            data.IIdNhuCauChiId = Model.Id;
            data.SGhiChu = item.sGhiChu;
            data.SLoaiThanhToan = item.sLoaiThanhToan;
            data.DDateCreate = DateTime.Now;
            data.SUserCreate = _sessionService.Current.Principal;
            return data;
        }
        #endregion
    }
}
