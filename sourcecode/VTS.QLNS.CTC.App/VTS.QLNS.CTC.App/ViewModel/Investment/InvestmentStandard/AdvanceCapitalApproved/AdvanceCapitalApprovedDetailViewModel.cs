using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.AdvanceCapitalApproved
{
    public class AdvanceCapitalApprovedDetailViewModel : DetailViewModelBase<VdtKhvKeHoachVonUngModel, VdtKhvKeHoachVonUngChiTietModel>
    {
        private readonly IVdtKhvKeHoachVonUngChiTietService _vonUngChiTietService;
        private readonly IVdtKhvKeHoachVonUngService _vonUngService;
        private readonly INsMucLucNganSachService _mlNganSachService;
        private readonly ITongHopNguonNSDauTuService _tonghopService;
        private readonly ISessionService _sessionService;
        private Dictionary<string, NsMucLucNganSach> _dicMucLucNganSach;
        private bool _isUpdate;
        private IMapper _mapper;

        public override string Title => "Kế hoạch vốn ứng được duyệt";
        public override string Name => "Danh sách kế hoạch vốn ứng được duyệt chi tiết";

        private double _fTongMucDauTu;
        public double fTongMucDauTu
        {
            get => _fTongMucDauTu;
            set
            {
                SetProperty(ref _fTongMucDauTu, value);
            }
        }

        private double _fTongLenhChi;
        public double fTongLenhChi
        {
            get => _fTongLenhChi;
            set
            {
                SetProperty(ref _fTongLenhChi, value);
            }
        }

        private double _fTongKhobac;
        public double fTongKhobac
        {
            get => _fTongKhobac;
            set
            {
                SetProperty(ref _fTongKhobac, value);
            }
        }

        private double _fTongTonKhoanTaiDonVi;
        public double fTongTonKhoanTaiDonVi
        {
            get => _fTongTonKhoanTaiDonVi;
            set
            {
                SetProperty(ref _fTongTonKhoanTaiDonVi, value);
            }
        }

        private bool _bIsDetail;
        public bool BIsDetail
        {
            get => _bIsDetail;
            set => SetProperty(ref _bIsDetail, value);
        }

        public bool BDisableDetail => !BIsDetail;

        public Visibility VisibilityDC => Model.iId_ParentId != null ? Visibility.Visible : Visibility.Collapsed;
        public bool IsDieuChinh => Model.iId_ParentId != null;
       
        public RelayCommand SaveDataCommand { get; }

        public AdvanceCapitalApprovedDetailViewModel(
            IVdtKhvKeHoachVonUngChiTietService vonUngChiTietService,
            IVdtKhvKeHoachVonUngService vonUngService,
            INsMucLucNganSachService mlNganSachService,
            ITongHopNguonNSDauTuService tonghopService,
            ISessionService sessionService,
            IMapper mapper)
        {
            _vonUngChiTietService = vonUngChiTietService;
            _vonUngService = vonUngService;
            _mlNganSachService = mlNganSachService;
            _tonghopService = tonghopService;
            _sessionService = sessionService;
            _mapper = mapper;
            SaveDataCommand = new RelayCommand(obj => OnSaveData());
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(10);
            GetMucLucNganSach();
            LoadData();
        }

        #region RelayCommand
        public override void LoadData(params object[] args)
        {
            var data = _vonUngChiTietService.GetDuAnInKeHoachVonUngDetail(Model.iID_KeHoachUngDeXuatID.Value);
            if (Model.lstDuAnId != null)
            {
                data = data.Where(n => n.iID_DuAnID.HasValue && Model.lstDuAnId.Contains(n.iID_DuAnID.Value)).ToList();
            }
            else
            {
                data = new List<VdtKhvKeHoachVonUngChiTietQuery>();
            }
            List<VdtKhvKeHoachVonUngChiTietQuery> lstDataUpdate = _vonUngChiTietService.GetKeHoachVonUngChiTietByParentId(Model.Id).ToList();
            if (lstDataUpdate != null && lstDataUpdate.Count != 0)
            {
                var lstDataExist = _mapper.Map<ObservableCollection<VdtKhvKeHoachVonUngChiTietModel>>(lstDataUpdate);
                data = data.Where(n => !lstDataExist.Select(n => n.iID_DuAnID).Contains(n.iID_DuAnID)).ToList();
                Items = _mapper.Map<ObservableCollection<VdtKhvKeHoachVonUngChiTietModel>>(data);
                foreach (var item in lstDataExist)
                {
                    if (Model.lstDuAnId != null && !Model.lstDuAnId.Any(n => n == item.iID_DuAnID))
                    {
                        item.IsDeleted = true;
                    }
                    Items.Add(item);
                }
                _isUpdate = true;
            }
            else
            {
                _isUpdate = false;
                Items = _mapper.Map<ObservableCollection<VdtKhvKeHoachVonUngChiTietModel>>(data);
            }

            var keHoachVonUngParent = _vonUngService.FindById(Model.Id)?.IIdParentId;

            foreach (var item in Items)
            {
                var (fCapPhatTaiKhoBacTruocDieuChinh, fCapPhatBangLenhChiTruocDieuChinh, fTonKhoanTaiDonViTruocDieuChinh) = GetGiaTriTruocDC(item, keHoachVonUngParent);
                item.FCapPhatTaiKhoBacTruocDieuChinh = fCapPhatTaiKhoBacTruocDieuChinh;
                item.FCapPhatBangLenhChiTruocDieuChinh = fCapPhatBangLenhChiTruocDieuChinh;
                item.FTonKhoanTaiDonViTruocDieuChinh = fTonKhoanTaiDonViTruocDieuChinh;
                item.PropertyChanged += DetailModel_PropertyChanged;
            }
            fTongMucDauTu = Items.Where(n => !n.IsDeleted).Sum(n => n.fTongMucDauTuPheDuyet ?? 0);
            fTongLenhChi = Items.Where(n => !n.IsDeleted).Sum(n => (n.fCapPhatBangLenhChi ?? 0));
            fTongKhobac = Items.Where(n => !n.IsDeleted).Sum(n => (n.fCapPhatTaiKhoBac ?? 0));
            fTongTonKhoanTaiDonVi = Items.Where(n => !n.IsDeleted).Sum(n => (n.fTonKhoanTaiDonVi ?? 0));
            OnPropertyChanged(nameof(fTongMucDauTu));
            OnPropertyChanged(nameof(fTongLenhChi));
            OnPropertyChanged(nameof(fTongKhobac));
            OnPropertyChanged(nameof(fTongTonKhoanTaiDonVi));
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
        }

        protected override void OnAdd()
        {
            if (SelectedItem != null)
            {
                int currentRow = Items.IndexOf(SelectedItem);
                VdtKhvKeHoachVonUngChiTietModel newItem = ObjectCopier.Clone(SelectedItem);
                newItem.fCapPhatBangLenhChi = null;
                newItem.fCapPhatTaiKhoBac = null;
                newItem.fTonKhoanTaiDonVi = null;
                newItem.sGhiChu = string.Empty;
                newItem.sL = newItem.sK = newItem.sM = newItem.sTM = newItem.sTTM = newItem.sNG = string.Empty;
                newItem.iID_MucID = newItem.iID_TieuMucID = newItem.iID_TietMucID = newItem.iID_NganhID = null;
                newItem.PropertyChanged += DetailModel_PropertyChanged;
                Items.Insert(currentRow + 1, newItem);
                SelectedItem = newItem;
                OnPropertyChanged(nameof(Items));
                OnPropertyChanged(nameof(SelectedItem));
                fTongMucDauTu = Items.Where(n => !n.IsDeleted).Sum(n => n.fTongMucDauTuPheDuyet ?? 0);
                fTongLenhChi = Items.Where(n => !n.IsDeleted).Sum(n => (n.fCapPhatBangLenhChi ?? 0));
                fTongKhobac = Items.Where(n => !n.IsDeleted).Sum(n => (n.fCapPhatTaiKhoBac ?? 0));
                OnPropertyChanged(nameof(fTongMucDauTu));
                OnPropertyChanged(nameof(fTongLenhChi));
                OnPropertyChanged(nameof(fTongKhobac));
            }
            fTongMucDauTu = Items.Where(n => !n.IsDeleted).Sum(n => n.fTongMucDauTuPheDuyet ?? 0);
            OnPropertyChanged(nameof(fTongMucDauTu));
        }

        protected override void OnDelete()
        {
            if (SelectedItem != null)
            {
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
            }
            fTongMucDauTu = Items.Where(n => !n.IsDeleted).Sum(n => n.fTongMucDauTuPheDuyet ?? 0);
            fTongLenhChi = Items.Where(n => !n.IsDeleted).Sum(n => (n.fCapPhatBangLenhChi ?? 0));
            fTongKhobac = Items.Where(n => !n.IsDeleted).Sum(n => (n.fCapPhatTaiKhoBac ?? 0));
            fTongTonKhoanTaiDonVi = Items.Where(n => !n.IsDeleted).Sum(n => (n.fTonKhoanTaiDonVi ?? 0));
            OnPropertyChanged(nameof(fTongMucDauTu));
            OnPropertyChanged(nameof(fTongLenhChi));
            OnPropertyChanged(nameof(fTongKhobac));
            OnPropertyChanged(nameof(fTongTonKhoanTaiDonVi));
            OnPropertyChanged(nameof(Items));
        }

        public void OnSaveData()
        {
            List<string> messageBuilder = new List<string>();
            List<VdtKhvKeHoachVonUngChiTietModel> lstDataNew = Items
                .Where(n => 
                (
                // điều chỉnh
                (
                    IsDieuChinh &&
                    ((n.fCapPhatBangLenhChi.HasValue && n.fCapPhatBangLenhChi != 0)
                    || (n.fCapPhatTaiKhoBac.HasValue && n.fCapPhatTaiKhoBac != 0)
                    || (n.fTonKhoanTaiDonVi.HasValue && n.fTonKhoanTaiDonVi != 0))
                )
                ||
                // k điều chỉnh
                (
                    !IsDieuChinh &&
                    ((n.FCapPhatBangLenhChiTruocDieuChinh != 0) 
                    || ( n.FCapPhatTaiKhoBacTruocDieuChinh != 0)
                    || (n.FTonKhoanTaiDonViTruocDieuChinh != 0)
                    )
                )
                )
                && !n.IsDeleted).ToList();
            if (Model.lstDuAnId != null)
            {
                List<string> lstDuAnDelete = Items.Where(n => !Model.lstDuAnId.Contains(n.iID_DuAnID.Value) && n.IsDeleted).Select(n => n.sTenDuAn).Distinct().ToList();
                if (lstDuAnDelete.Any())
                {
                    MessageBoxResult dialogConfirm = MessageBox.Show(string.Format(Resources.MsgErrorConfirmDeleteChungTuChiTiet, string.Join(", ", lstDuAnDelete)), Name, System.Windows.MessageBoxButton.YesNo);
                    if (dialogConfirm == MessageBoxResult.No)
                    {
                        return;
                    }
                }
            }
            if (lstDataNew == null || lstDataNew.Count == 0)
            {
                messageBuilder.Add(string.Format(Resources.MsgErrorRequire, "Kế hoạch vốn ứng được duyệt chi tiết"));
                MessageBox.Show(string.Join("\n", messageBuilder));
                return;
            }
            List<VdtKhvKeHoachVonUngChiTiet> lstData = new List<VdtKhvKeHoachVonUngChiTiet>();
            foreach (var item in lstDataNew)
            {
                VdtKhvKeHoachVonUngChiTiet itemData = ConvertDataInsert(item);
                if (itemData == null)
                {
                    messageBuilder.Add(Resources.MsgErrorMucLucNganSachNotExist);
                    break;
                }
                lstData.Add(itemData);

            }
            if (messageBuilder.Count != 0)
            {
                MessageBox.Show(string.Join("\n", messageBuilder));
                return;
            }
            bool isSucess = _vonUngChiTietService.Insert(Model.Id, lstData);
            if (!isSucess)
            {
                messageBuilder.Add(Resources.AlertDataError);
                MessageBox.Show(string.Join("\n", messageBuilder));
                return;
            }
            if (_isUpdate)
            {
                _tonghopService.InsertTongHopNguonDauTu_Tang(LOAI_CHUNG_TU.KE_HOACH_VON_UNG, (int)TypeExecute.Update, Model.Id);
            }
            else
            {
                _tonghopService.InsertTongHopNguonDauTu_Tang(LOAI_CHUNG_TU.KE_HOACH_VON_UNG, (int)TypeExecute.Insert, Model.Id);
            }
            MessageBox.Show(Resources.MsgSaveDone);
            LoadData();
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case nameof(VdtKhvKeHoachVonUngChiTietModel.fCapPhatBangLenhChi):
                    fTongMucDauTu = Items.Where(n => !n.IsDeleted).Sum(n => n.fTongMucDauTuPheDuyet ?? 0);
                    fTongLenhChi = Items.Where(n => !n.IsDeleted).Sum(n => (n.fCapPhatBangLenhChi ?? 0));
                    OnPropertyChanged(nameof(fTongMucDauTu));
                    OnPropertyChanged(nameof(fTongLenhChi));
                    break;
                case nameof(VdtKhvKeHoachVonUngChiTietModel.fCapPhatTaiKhoBac):
                    fTongMucDauTu = Items.Where(n => !n.IsDeleted).Sum(n => n.fTongMucDauTuPheDuyet ?? 0);
                    fTongKhobac = Items.Where(n => !n.IsDeleted).Sum(n => (n.fCapPhatTaiKhoBac ?? 0));
                    OnPropertyChanged(nameof(fTongMucDauTu));
                    OnPropertyChanged(nameof(fTongKhobac));
                    break;
                case nameof(VdtKhvKeHoachVonUngChiTietModel.fTonKhoanTaiDonVi):
                    fTongMucDauTu = Items.Where(n => !n.IsDeleted).Sum(n => n.fTongMucDauTuPheDuyet ?? 0);
                    fTongTonKhoanTaiDonVi = Items.Where(n => !n.IsDeleted).Sum(n => (n.fTonKhoanTaiDonVi ?? 0));
                    OnPropertyChanged(nameof(fTongMucDauTu));
                    OnPropertyChanged(nameof(fTongKhobac));
                    break;
            }
        }

        public void CheckMucLucNganSach()
        {
            var itemCheck = ConvertDataInsert(SelectedItem);
            if (itemCheck == null)
            {
                MessageBox.Show(Resources.MsgErrorMucLucNganSachNotExist);
                SelectedItem.IsDeleted = true;
            }
        }

        private (double fCapPhatTaiKhoBacTruocDieuChinh, double fCapPhatBangLenhChiTruocDieuChinh, double fTonKhoanTaiDonViTruocDieuChinh) GetGiaTriTruocDC(VdtKhvKeHoachVonUngChiTietModel item, Guid? keHoachVonUngParent)
        {
            var fCapPhatTaiKhoBacTruocDieuChinh = item.fCapPhatTaiKhoBac??0;
            var fCapPhatBangLenhChiTruocDieuChinh = item.fCapPhatBangLenhChi??0;
            var fTonKhoanTaiDonViTruocDieuChinh = item.fTonKhoanTaiDonVi ?? 0;

            if (keHoachVonUngParent == null) return (fCapPhatTaiKhoBacTruocDieuChinh, fCapPhatBangLenhChiTruocDieuChinh, fTonKhoanTaiDonViTruocDieuChinh);
            
            var keHoachVonUngChiTiet = _vonUngChiTietService.FindById(item.Id);
           
            if (keHoachVonUngChiTiet == null) return (fCapPhatTaiKhoBacTruocDieuChinh, fCapPhatBangLenhChiTruocDieuChinh, fTonKhoanTaiDonViTruocDieuChinh);
           
            return (keHoachVonUngChiTiet.FCapPhatTaiKhoBac ?? 0, keHoachVonUngChiTiet.FCapPhatBangLenhChi ?? 0, keHoachVonUngChiTiet.FTonKhoanTaiDonVi ?? 0);
        }

        #endregion

        #region Helper
        private VdtKhvKeHoachVonUngChiTiet ConvertDataInsert(VdtKhvKeHoachVonUngChiTietModel data)
        {
            VdtKhvKeHoachVonUngChiTiet dataInsert = SetMucLucNganSachItem(data);
            if (dataInsert == null) return null;
            dataInsert.Id = Guid.NewGuid();
            dataInsert.FCapPhatBangLenhChi = IsDieuChinh ? data.fCapPhatBangLenhChi : data.FCapPhatBangLenhChiTruocDieuChinh;
            dataInsert.FCapPhatTaiKhoBac = IsDieuChinh ? data.fCapPhatTaiKhoBac : data.FCapPhatTaiKhoBacTruocDieuChinh;
            dataInsert.FTonKhoanTaiDonVi = IsDieuChinh ? data.fTonKhoanTaiDonVi : data.FTonKhoanTaiDonViTruocDieuChinh;
            dataInsert.FTiGia = data.fTiGia;
            dataInsert.FTiGiaDonVi = data.fTiGiaDonVi;
            dataInsert.IIdDonViTienTeId = data.iID_DonViTienTeID;
            dataInsert.IIdDuAnId = data.iID_DuAnID;
            dataInsert.IIdKeHoachUngId = Model.Id;
            dataInsert.IIdTienTeId = data.iID_TienTeID;
            dataInsert.SGhiChu = data.sGhiChu;
            dataInsert.STrangThaiDuAnDangKy = data.sTrangThaiDuAnDangKy;
            dataInsert.ID_DuAn_HangMuc = data.ID_DuAn_HangMuc;
            return dataInsert;
        }

        private void GetMucLucNganSach()
        {
            _dicMucLucNganSach = new Dictionary<string, NsMucLucNganSach>();
            var datas = _mlNganSachService.FindAll(Model.iNamKeHoach.Value);
            if (datas == null) return;
            foreach (var item in datas)
            {
                string key = item.Lns + "\t" + item.L + "\t" + item.K + "\t"
                    + item.M + "\t" + item.Tm + "\t" + item.Ttm + "\t" + item.Ng;
                if (!_dicMucLucNganSach.ContainsKey(key))
                    _dicMucLucNganSach.Add(key, item);
            }
        }

        private VdtKhvKeHoachVonUngChiTiet SetMucLucNganSachItem(VdtKhvKeHoachVonUngChiTietModel item)
        {
            string key = item.sLNS + "\t" + item.sL + "\t" + item.sK + "\t"
                    + item.sM + "\t" + item.sTM + "\t" + item.sTTM + "\t" + item.sNG;
            if (!_dicMucLucNganSach.ContainsKey(key)) return null;
            if (!string.IsNullOrEmpty(item.sNG))
                return new VdtKhvKeHoachVonUngChiTiet() { IIdNganhId = _dicMucLucNganSach[key].Id };
            if (!string.IsNullOrEmpty(item.sTTM))
                return new VdtKhvKeHoachVonUngChiTiet() { IIdTietMucId = _dicMucLucNganSach[key].Id };
            if (!string.IsNullOrEmpty(item.sTM))
                return new VdtKhvKeHoachVonUngChiTiet() { IIdTieuMucId = _dicMucLucNganSach[key].Id };
            if (!string.IsNullOrEmpty(item.sM))
                return new VdtKhvKeHoachVonUngChiTiet() { IIdMucId = _dicMucLucNganSach[key].Id };
            return null;
        }
        #endregion
    }
}
