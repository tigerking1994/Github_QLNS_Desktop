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
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.ThanhToanKhoBac
{
    public class ThanhToanKhoBacDetailViewModel : DetailViewModelBase<ThanhToanQuaKhoBacModel, ThanhToanQuaKhoBacChiTietModel>
    {
        private readonly IVdtTtThanhToanQuaKhoBacService _thanhToanService;
        private readonly ITongHopNguonNSDauTuService _tonghopService;
        private readonly ISessionService _sessionService;
        private readonly IVdtDaTtHopDongService _ttHopDongService;
        private IMapper _mapper;
        private bool _isUpdate;
        public override string Name => "Quản lý thanh toán qua kho bạc chi tiết";

        private string _Description;
        public override string Description
        {
            get => _Description;
            set
            {
                SetProperty(ref _Description, value);
            }
        }

        public RelayCommand SaveDataCommand { get; }

        public ThanhToanKhoBacDetailViewModel(
            IVdtTtThanhToanQuaKhoBacService thanhToanService,
            ISessionService sessionService,
            IVdtDaTtHopDongService ttHopDongService,
            ITongHopNguonNSDauTuService tonghopService,
            IMapper mapper)
        {
            _thanhToanService = thanhToanService;
            _sessionService = sessionService;
            _ttHopDongService = ttHopDongService;
            _tonghopService = tonghopService;
            _mapper = mapper;
            SaveDataCommand = new RelayCommand(obj => OnSaveData());
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(10);
            LoadData();
        }

        #region RelayCommand
        public override void LoadData(params object[] args)
        {
            Description = "Cấp phát cấp thanh toán trong chỉ tiêu chi tiết";
            var data = _thanhToanService.GetThanhToanKhoBacDetail(
                Model.iNamKeHoach, Model.dNgayThanhToan.Value, Model.iID_LoaiNguonVonID.Value, Model.iId_MaDonViQuanLyID);
            if (Model.lstDuAnId != null)
            {
                data = data.Where(n => Model.lstDuAnId.Contains(n.iID_DuAnID)).ToList();
            }
            else
            {
                data = new List<ThanhToanQuaKhoBacChiTietQuery>();
            }
            List<ThanhToanQuaKhoBacChiTietQuery> lstDataUpdate = _thanhToanService.GetThanhToanKhoBacDetailByParentId(Model.Id).ToList();
            if (lstDataUpdate != null && lstDataUpdate.Count != 0)
            {
                var lstDataExist = _mapper.Map<ObservableCollection<ThanhToanQuaKhoBacChiTietModel>>(lstDataUpdate);
                foreach (var item in lstDataExist)
                {
                    if (Model.lstDuAnId != null && !Model.lstDuAnId.Any(n => n == item.iID_DuAnID))
                    {
                        item.IsDeleted = true;
                    }
                }
                _isUpdate = true;
                Items = new ObservableCollection<ThanhToanQuaKhoBacChiTietModel>(lstDataExist);
            }
            else
            {
                _isUpdate = false;
                Items = _mapper.Map<ObservableCollection<ThanhToanQuaKhoBacChiTietModel>>(data);
            }
            OnPropertyChanged(nameof(Items));
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
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

        public void OnSaveData()
        {
            StringBuilder messageBuilder = new StringBuilder();
            List<ThanhToanQuaKhoBacChiTietModel> lstDataNew = Items.Where(n => n.fGiaTriThanhToan != 0 && !n.IsDeleted).ToList();
            if (lstDataNew == null || lstDataNew.Count == 0)
            {
                messageBuilder.Append(Resources.MsgErrorDataEmpty);
                MessageBox.Show(String.Join("\n", messageBuilder.ToString()));
                return;
            }
            List<VdtTtThanhToanQuaKhoBacChiTiet> lstData = new List<VdtTtThanhToanQuaKhoBacChiTiet>();
            foreach (var item in lstDataNew)
            {
                lstData.Add(ConvertDataInsert(item));
            }
            if (messageBuilder.Length != 0)
            {
                MessageBox.Show(String.Join("\n", messageBuilder.ToString()));
                return;
            }
            bool isSucess = _thanhToanService.InsertDetail(Model.Id, lstData);
            if (!isSucess)
            {
                messageBuilder.AppendFormat(Resources.AlertDataError);
                MessageBox.Show(String.Join("\n", messageBuilder.ToString()));
                return;
            }
            messageBuilder.AppendFormat(Resources.MsgSaveDone);
            MessageBox.Show(messageBuilder.ToString());
            LoadData();
        }
        #endregion

        #region Helper
        private VdtTtThanhToanQuaKhoBacChiTiet ConvertDataInsert(ThanhToanQuaKhoBacChiTietModel data)
        {
            VdtTtThanhToanQuaKhoBacChiTiet dataInsert = new VdtTtThanhToanQuaKhoBacChiTiet();
            dataInsert.Id = Guid.NewGuid();
            dataInsert.IIdThanhToanId = Model.Id;
            dataInsert.IIdMucId = data.iID_MucID;
            dataInsert.IIdTieuMucId = data.iID_TieuMucID;
            dataInsert.IIdTietMucId = data.iID_TietMucID;
            dataInsert.IIdNganhId = data.iID_NganhID;
            dataInsert.IIdDuAnId = data.iID_DuAnID;
            dataInsert.IIdHopDongId = data.iID_HopDongID;
            dataInsert.IIdNhaThauId = data.iID_NhaThauID;
            dataInsert.FGiaTriThanhToan = data.fGiaTriThanhToan;
            dataInsert.FGiaTriTamUng = data.fGiaTriTamUng;
            dataInsert.IIdDonViTienTeId = data.iID_DonViTienTeID;
            dataInsert.FTiGiaDonVi = data.fTiGiaDonVi;
            dataInsert.IIdTienTeId = data.iID_TienTeID;
            dataInsert.FTiGia = data.fTiGia;
            return dataInsert;
        }
        #endregion
    }
}
