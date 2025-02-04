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

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.KeHoachVonUngDeXuat
{
    public class KeHoachVonUngDeXuatDetailViewModel : DetailViewModelBase<VdtKhvKeHoachVonUngDxModel, VdtKhvKeHoachVonUngDxChiTietModel>
    {
        private readonly IVdtKhvKeHoachVonUngDxService _vonUngService;
        private readonly ITongHopNguonNSDauTuService _tonghopService;
        private readonly ISessionService _sessionService;
        private IMapper _mapper;

        public override string Title => "Kế hoạch vốn ứng đề xuất";
        public override string Name => "Kế hoạch vốn ứng đề xuất chi tiết";

        private bool _bIsDetail;
        public bool BIsDetail
        {
            get => _bIsDetail;
            set => SetProperty(ref _bIsDetail, value);
        }

        public bool BIsEnableDetail => !BIsDetail && !BIsTongHop;

        private double _fTongMucDauTu;
        public double fTongMucDauTu
        {
            get => _fTongMucDauTu;
            set => SetProperty(ref _fTongMucDauTu, value);
        }

        private double _fTongGiaTri;
        public double fTongGiaTri
        {
            get => _fTongGiaTri;
            set
            {
                SetProperty(ref _fTongGiaTri, value);
            }
        }

        private bool _bIsTongHop;
        public bool BIsTongHop
        {
            get => _bIsTongHop;
            set => SetProperty(ref _bIsTongHop, value);
        }

        public Visibility VisibilityDC => Model.IIdParentId != null ? Visibility.Visible : Visibility.Collapsed;
        public bool IsDieuChinh => Model.IIdParentId != null;

        public RelayCommand SaveDataCommand { get; }

        public KeHoachVonUngDeXuatDetailViewModel(
            IVdtKhvKeHoachVonUngDxService vonUngService,
            ITongHopNguonNSDauTuService tonghopService,
            ISessionService sessionService,
            IMapper mapper)
        {
            _vonUngService = vonUngService;
            _tonghopService = tonghopService;
            _sessionService = sessionService;
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
            List<VdtKhvKeHoachVonUngDxChiTietQuery> data = new List<VdtKhvKeHoachVonUngDxChiTietQuery>();

            data = _vonUngService.GetKeHoachVonUngChiTietByParentId(Model.Id).ToList();

            if(data == null || data.Count == 0)
            {
                if (BIsTongHop)
                {
                    foreach (Guid iId in Model.STongHop.Split(";").Select(n => Guid.Parse(n)))
                    {
                        var child = _vonUngService.GetKeHoachVonUngChiTietByParentId(iId);
                        if (child != null)
                            data.AddRange(child);
                    }
                }
                else
                {
                    data = _vonUngService.GetDuAnInKeHoachVonUngDetail(Model.IIDMaDonViQuanLy, Model.DNgayDeNghi.Value, Model.STongHop).ToList();
                }
            }
            if (!BIsTongHop && data != null && Model.LstDuAnId != null)
            {
                data = data.Where(n => n.iID_DuAnID.HasValue && Model.LstDuAnId.Contains(n.iID_DuAnID.Value)).ToList();
            }
            Items = _mapper.Map<ObservableCollection<VdtKhvKeHoachVonUngDxChiTietModel>>(data);

            var keHoachVonUngParent = _vonUngService.FindAll(x => x.Id == Model.Id).SingleOrDefault()?.IIdParentId;
            foreach (var item in Items)
            {
                var fGiaTriDeNghiTruocDieuChinh = GetGiaTriDeNgiTruocDC(item, keHoachVonUngParent);
                item.FGiaTriDeNghiTruocDieuChinh = fGiaTriDeNghiTruocDieuChinh;
                item.PropertyChanged += DetailModel_PropertyChanged;
            }
            fTongMucDauTu = Items.Where(n => !n.IsDeleted).Sum(n => n.FTongMucDauTuPheDuyet ?? 0);
            fTongGiaTri = Items.Where(n => !n.IsDeleted).Sum(n => n.FGiaTriDeNghi);
            OnPropertyChanged(nameof(fTongGiaTri));
            OnPropertyChanged(nameof(fTongMucDauTu));
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
                VdtKhvKeHoachVonUngDxChiTietModel newItem = ObjectCopier.Clone(SelectedItem);
                newItem.FGiaTriDeNghi = 0;
                newItem.SGhiChu = string.Empty;
                newItem.PropertyChanged += DetailModel_PropertyChanged;
                Items.Insert(currentRow + 1, newItem);
                SelectedItem = newItem;
                OnPropertyChanged(nameof(Items));
                OnPropertyChanged(nameof(SelectedItem));
                fTongGiaTri = Items.Where(n => !n.IsDeleted).Sum(n => n.FGiaTriDeNghi);
                OnPropertyChanged(nameof(fTongGiaTri));
            }
        }

        protected override void OnDelete()
        {
            if (SelectedItem != null)
            {
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
            }
            fTongMucDauTu = Items.Where(n => !n.IsDeleted).Sum(n => n.FTongMucDauTuPheDuyet ?? 0);
            fTongGiaTri = Items.Where(n => !n.IsDeleted).Sum(n => n.FGiaTriDeNghi);
            OnPropertyChanged(nameof(fTongMucDauTu));
            OnPropertyChanged(nameof(fTongGiaTri));
            OnPropertyChanged(nameof(Items));
        }

        public void OnSaveData()
        {
            List<string> messageBuilder = new List<string>();
            List<VdtKhvKeHoachVonUngDxChiTietModel> lstDataNew = Items.Where(n => ((n.FGiaTriDeNghi != 0 && IsDieuChinh) || (n.FGiaTriDeNghiTruocDieuChinh != 0 &&!IsDieuChinh)) && !n.IsDeleted).ToList();
            if (Model.LstDuAnId != null)
            {
                List<string> lstDuAnDelete = Items.Where(n => !Model.LstDuAnId.Contains(n.IIDDuAnID.Value) && n.IsDeleted).Select(n => n.STenDuAn).Distinct().ToList();
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
                messageBuilder.Add(Resources.VoucherDataEmpty);
                MessageBox.Show(String.Join("\n", messageBuilder));
                return;
            }
            List<VdtKhvKeHoachVonUngDxChiTiet> lstData = new List<VdtKhvKeHoachVonUngDxChiTiet>();
            foreach (var item in lstDataNew)
            {
                VdtKhvKeHoachVonUngDxChiTiet itemData = ConvertDataInsert(item);
                if (itemData == null)
                {
                    messageBuilder.Add(Resources.MsgErrorMucLucNganSachNotExist);
                    break;
                }
                lstData.Add(itemData);

            }
            if (messageBuilder.Count != 0)
            {
                MessageBox.Show(String.Join("\n", messageBuilder));
                return;
            }
            bool isSucess = _vonUngService.InsertDetail(Model.Id, lstData);
            if (!isSucess)
            {
                messageBuilder.Add(Resources.AlertDataError);
                MessageBox.Show(String.Join("\n", messageBuilder));
                return;
            }
            MessageBox.Show(Resources.MsgSaveDone);
            LoadData();
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case nameof(VdtKhvKeHoachVonUngDxChiTietModel.FGiaTriDeNghi):
                case nameof(VdtKhvKeHoachVonUngDxChiTietModel.IsDeleted):
                    fTongGiaTri = Items.Where(n => !n.IsDeleted).Sum(n => n.FGiaTriDeNghi);
                    OnPropertyChanged(nameof(fTongGiaTri));
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

        private double GetGiaTriDeNgiTruocDC(VdtKhvKeHoachVonUngDxChiTietModel vdtKhvKeHoachVonUngDxChiTietModel, Guid? keHoachVonUngParent)
        {
            var result = 0.0d;
            if (keHoachVonUngParent == null) return vdtKhvKeHoachVonUngDxChiTietModel.FGiaTriDeNghi;
            var keHoachVonUngChiTiet = _vonUngService.FindAllCT(x=>x.IIdKeHoachUngId == keHoachVonUngParent && x.IIdDuAnId == vdtKhvKeHoachVonUngDxChiTietModel.IIDDuAnID).SingleOrDefault();
            if(keHoachVonUngChiTiet == null) return result;

            return keHoachVonUngChiTiet.FGiaTriDeNghi??0;
        }

        #endregion

        #region Helper
        private VdtKhvKeHoachVonUngDxChiTiet ConvertDataInsert(VdtKhvKeHoachVonUngDxChiTietModel data)
        {
            VdtKhvKeHoachVonUngDxChiTiet dataInsert = new VdtKhvKeHoachVonUngDxChiTiet();
            if (dataInsert == null) return null;
            dataInsert.Id = Guid.NewGuid();
            dataInsert.FGiaTriDeNghi = IsDieuChinh ? data.FGiaTriDeNghi : data.FGiaTriDeNghiTruocDieuChinh;
            dataInsert.FTiGia = data.FTiGia;
            dataInsert.FTiGiaDonVi = data.FTiGiaDonVi;
            dataInsert.IIdDonViTienTeId = data.IIDDonViTienTeID;
            dataInsert.IIdDuAnId = data.IIDDuAnID;
            dataInsert.IIdKeHoachUngId = Model.Id;
            dataInsert.IIdTienTeId = data.IIDTienTeID;
            dataInsert.ID_DuAn_HangMuc = data.ID_DuAn_HangMuc;
            dataInsert.SGhiChu = data.SGhiChu;
            dataInsert.STrangThaiDuAnDangKy = data.STrangThaiDuAnDangKy;
            return dataInsert;
        }
        #endregion
    }
}
