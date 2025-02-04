using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.ApprovalOfAdvanceCaptital
{
    public class ApprovalOfAdvanceCaptitalDetailViewModel : DetailViewModelBase<VdtTtDeNghiThanhToanUngModel, VdtTtDeNghiThanhToanUngChiTietModel>
    {
        private readonly IVdtTtDeNghiThanhToanUngService _deNghiThanhToanUngService;
        private readonly IVdtTtDeNghiThanhToanUngChiTietService _deNghiThanhToanUngChiTietService;
        private readonly IVdtTtDeNghiThanhToanChiTietService _deNghiThanhToanChiTietService;
        private readonly ISessionService _sessionService;
        private readonly IVdtDaTtHopDongService _ttHopDongService;
        private IMapper _mapper;

        public override string Name => "Quản lý cấp phát cấp ứng ngoài chỉ tiêu";

        private string _Description;
        public override string Description
        {
            get => _Description;
            set
            {
                SetProperty(ref _Description, value);
            }
        }

        #region data combobox
        private ObservableCollection<ComboboxItem> _cbxHopDong;
        public ObservableCollection<ComboboxItem> CbxHopDong
        {
            get => _cbxHopDong;
            set => SetProperty(ref _cbxHopDong, value);
        }
        #endregion

        public RelayCommand SaveDataCommand { get; }

        public ApprovalOfAdvanceCaptitalDetailViewModel(
            IVdtTtDeNghiThanhToanUngService deNghiThanhToanUngService,
            IVdtTtDeNghiThanhToanUngChiTietService deNghiThanhToanUngChiTietService,
            IVdtTtDeNghiThanhToanChiTietService deNghiThanhToanChiTietService,
            IVdtDaTtHopDongService ttHopDongService,
            ISessionService sessionService,
            IMapper mapper)
        {
            _deNghiThanhToanUngService = deNghiThanhToanUngService;
            _deNghiThanhToanUngChiTietService = deNghiThanhToanUngChiTietService;
            _deNghiThanhToanChiTietService = deNghiThanhToanChiTietService;
            _ttHopDongService = ttHopDongService;
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
            Description = "Cấp phát cấp ứng ngoài chỉ tiêu chi tiết";
            var data = _deNghiThanhToanUngChiTietService.GetDuAnByDeNghiThanhToanUng(Model.iID_MaDonViQuanLy, Model.dNgayDeNghi.Value);
            if (Model.lstDuAnId != null)
            {
                data = data.Where(n => n.iID_DuAnID.HasValue && Model.lstDuAnId.Contains(n.iID_DuAnID.Value)).ToList();
            }
            List<VdtTtDeNghiThanhToanUngChiTietQuery> lstDataUpdate = _deNghiThanhToanUngChiTietService.GetDuAnByIdThanhToan(Model.Id, Model.iID_MaDonViQuanLy, Model.dNgayDeNghi.Value).ToList();
            if (lstDataUpdate != null && lstDataUpdate.Count != 0)
            {
                var lstDataExist = _mapper.Map<ObservableCollection<VdtTtDeNghiThanhToanUngChiTietModel>>(lstDataUpdate);
                data = data.Where(n => !lstDataExist.Select(n => n.iID_DuAnID).Contains(n.iID_DuAnID)).ToList();
                Items = _mapper.Map<ObservableCollection<VdtTtDeNghiThanhToanUngChiTietModel>>(data);
                foreach (var item in lstDataExist)
                {
                    if (Model.lstDuAnId != null && !Model.lstDuAnId.Any(n => n == item.iID_DuAnID))
                    {
                        item.IsDeleted = true;
                    }
                    Items.Add(item);
                }
            }
            else
            {
                Items = _mapper.Map<ObservableCollection<VdtTtDeNghiThanhToanUngChiTietModel>>(data);
            }
            List<VdtDaTtHopDong> lstHopDong = _ttHopDongService.FindByListDuAnId(data.Select(n => n.iID_DuAnID.Value).Distinct().ToList());
            ComboboxItem cbxDefault = new ComboboxItem() { DisplayItem = "" };
            foreach (var item in Items)
            {
                List<ComboboxItem> lstCb = new List<ComboboxItem>();
                lstCb.Add(cbxDefault);
                lstCb.AddRange(lstHopDong.Where(n => n.IIdDuAnId == item.iID_DuAnID).Select(n => new ComboboxItem { ValueItem = n.Id.ToString(), DisplayItem = n.SSoHopDong }));
                item.PropertyChanged += DetailModel_PropertyChanged;
                item.CbxHopDong = new ObservableCollection<ComboboxItem>(lstCb);
                if (item.iId_HopDongId.HasValue)
                {
                    var objHopDongData = _deNghiThanhToanChiTietService.GetHopDongInfo(item.iId_HopDongId.Value, Model.dNgayDeNghi.Value, Model.iId_NguonVonId ?? 0);
                    StringBuilder sHopDong = new StringBuilder();
                    sHopDong.AppendFormat("- Ngày hợp đồng : {0}\n", objHopDongData.dNgayHopDong.HasValue ? objHopDongData.dNgayHopDong.Value.ToString("dd/MM/yyyy") : string.Empty);
                    sHopDong.AppendFormat("- Giá trị hợp đồng : {0}\n", objHopDongData.fGiaTriHopDong.ToString("##,#", CultureInfo.GetCultureInfo("vi-VN")));
                    sHopDong.AppendFormat("- Dự toán, giá gói thầu được duyệt : {0}\n", (objHopDongData.fGiaGoiThau ?? 0).ToString("##,#", CultureInfo.GetCultureInfo("vi-VN")));
                    StringBuilder sGoiThau = new StringBuilder();
                    sGoiThau.AppendFormat("- Tên gói thầu : {0}\n", objHopDongData.sTenGoiThau);
                    sGoiThau.AppendFormat("- Tên nhà thầu : {0}\n", objHopDongData.sTenNhaThau);
                    sGoiThau.AppendFormat("- Số tài khoản : {0}\n", objHopDongData.sSoTaiKhoan);
                    sGoiThau.AppendFormat("- Ngân hàng : {0}\n", objHopDongData.sNganHang);
                    item.sThongTinHopDong = sHopDong.ToString();
                    item.sThongTinNhaThau = sGoiThau.ToString();
                    item.iId_GoiThau = objHopDongData.iId_GoiThau;
                }
            }
        }

        protected override void OnAdd()
        {
            if (SelectedItem != null)
            {
                int currentRow = Items.IndexOf(SelectedItem);
                VdtTtDeNghiThanhToanUngChiTietModel newItem = ObjectCopier.Clone(SelectedItem);
                newItem.iId_HopDongId = null;
                newItem.sThongTinHopDong = string.Empty;
                newItem.sThongTinNhaThau = string.Empty;
                newItem.fGiaTriTamUng = 0;
                newItem.fGiaTriThuHoi = 0;
                newItem.sGhiChu = string.Empty;
                Items.Insert(currentRow + 1, newItem);
                SelectedItem = newItem;
                OnPropertyChanged(nameof(Items));
                OnPropertyChanged(nameof(SelectedItem));
            }
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
            List<VdtTtDeNghiThanhToanUngChiTietModel> lstDataNew = Items.Where(n => !n.IsDeleted).ToList();
            if (lstDataNew == null || lstDataNew.Count == 0)
            {
                messageBuilder.Append(Resources.MsgErrorDataEmpty);
                MessageBox.Show(String.Join("\n", messageBuilder.ToString()));
                return;
            }
            List<VdtTtDeNghiThanhToanUngChiTiet> lstData = new List<VdtTtDeNghiThanhToanUngChiTiet>();
            foreach (var item in lstDataNew)
            {
                lstData.Add(ConvertDataInsert(item));
            }
            if (messageBuilder.Length != 0)
            {
                MessageBox.Show(String.Join("\n", messageBuilder.ToString()));
                return;
            }
            bool isSucess = _deNghiThanhToanUngChiTietService.Insert(Model.Id, lstData);
            if (!isSucess)
            {
                messageBuilder.AppendFormat(Resources.AlertDataError);
                MessageBox.Show(String.Join("\n", messageBuilder.ToString()));
                return;
            }
            messageBuilder.AppendFormat(Resources.MsgSaveDone);
            LoadData();
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            VdtTtDeNghiThanhToanUngChiTietModel item = (VdtTtDeNghiThanhToanUngChiTietModel)sender;
            switch (args.PropertyName)
            {
                case nameof(VdtTtDeNghiThanhToanUngChiTietModel.iId_HopDongId):
                    if (!item.iId_HopDongId.HasValue) return;
                    var objHopDongData = _deNghiThanhToanChiTietService.GetHopDongInfo(item.iId_HopDongId.Value, Model.dNgayDeNghi.Value, Model.iId_NguonVonId ?? 0);
                    StringBuilder sHopDong = new StringBuilder();
                    sHopDong.AppendFormat("- Ngày hợp đồng : {0}\n", objHopDongData.dNgayHopDong.HasValue ? objHopDongData.dNgayHopDong.Value.ToString("dd/MM/yyyy") : string.Empty);
                    sHopDong.AppendFormat("- Giá trị hợp đồng : {0}\n", objHopDongData.fGiaTriHopDong.ToString("##,#", CultureInfo.GetCultureInfo("vi-VN")));
                    sHopDong.AppendFormat("- Dự toán, giá gói thầu được duyệt : {0}\n", (objHopDongData.fGiaGoiThau ?? 0).ToString("##,#", CultureInfo.GetCultureInfo("vi-VN")));
                    StringBuilder sGoiThau = new StringBuilder();
                    sGoiThau.AppendFormat("- Tên gói thầu : {0}\n", objHopDongData.sTenGoiThau);
                    sGoiThau.AppendFormat("- Tên nhà thầu : {0}\n", objHopDongData.sTenNhaThau);
                    sGoiThau.AppendFormat("- Số tài khoản : {0}\n", objHopDongData.sSoTaiKhoan);
                    sGoiThau.AppendFormat("- Ngân hàng : {0}\n", objHopDongData.sNganHang);
                    item.sThongTinHopDong = sHopDong.ToString();
                    item.sThongTinNhaThau = sGoiThau.ToString();
                    item.iId_GoiThau = objHopDongData.iId_GoiThau;

                    var objLuyKe = _deNghiThanhToanUngChiTietService.GetLuyKeThanhToan(
                        item.iID_DuAnID.Value, item.iId_HopDongId, Model.iID_MaDonViQuanLy, Model.dNgayDeNghi.Value);
                    if(objLuyKe != null)
                    {
                        item.fLuyKeChiTieu = objLuyKe.fLuyKeChiTieu;
                        item.fLuyKeThanhToan = objLuyKe.fLuyKeThanhToan;
                    }
                    break;
            }
            OnPropertyChanged(nameof(Items));
        }
        #endregion

        #region Helper
        private VdtTtDeNghiThanhToanUngChiTiet ConvertDataInsert(VdtTtDeNghiThanhToanUngChiTietModel data)
        {
            VdtTtDeNghiThanhToanUngChiTiet dataInsert = new VdtTtDeNghiThanhToanUngChiTiet();
            dataInsert.Id = Guid.NewGuid();
            dataInsert.IIdDeNghiThanhToanId = Model.Id;
            dataInsert.IIdDuAnId = data.iID_DuAnID;
            dataInsert.IIdHopDongId = data.iId_HopDongId;
            dataInsert.IIdNhaThauId = data.iId_NhaThauId;
            dataInsert.FGiaTriTamUng = data.fGiaTriTamUng;
            dataInsert.FGiaTriThuHoiUngNgoaiChiTieu = data.fGiaTriThuHoiUngNgoaiChiTieu;
            dataInsert.SGhiChu = data.sGhiChu;
            dataInsert.FGiaTriThuHoi = data.fGiaTriThuHoi;
            dataInsert.IIdDonViTienTeId = data.iId_DonViTienTeId;
            dataInsert.IIdTienTeId = data.iId_TienTeId;
            dataInsert.FTiGiaDonVi = data.fTiGiaDonVi;
            dataInsert.FTiGia = data.fTiGia;
            dataInsert.SGhiChu = data.sGhiChu;
            return dataInsert;
        }
        #endregion
    }
}
