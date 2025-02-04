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
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.AnnualSettlement
{
    public class AnnualSettlementDetailViewModel : DetailViewModelBase<VdtQtBcquyetToanNienDoModel, VdtQtDenghiQuyetToanNienDoChiTietModel>
    {
        private readonly IVdtQtDeNghiQuyetToanNienDoService _quyetToanNienDoService;
        private readonly IVdtQtDeNghiQuyetToanNienDoChiTietService _quyetToanNienDoChiTietService;
        private readonly IVdtQtXuLySoLieuService _xldlService;
        private readonly ISessionService _sessionService;
        private readonly IVdtDaTtHopDongService _ttHopDongService;
        private IMapper _mapper;

        public override string Name => "Đề nghị quyết toán niên độ chi tiết";

        private string _Description;
        public override string Description
        {
            get => _Description;
            set
            {
                SetProperty(ref _Description, value);
            }
        }

        #region Header
        public string sVonTamUngChuaThuHoi => string.Format("Số vốn tạm ứng theo chế độ chưa thu hồi của các năm trước nộp điều chỉnh giảm trong năm {0} &#x0a;          (12)", Model.INamKeHoach);
        public string sKeHoachThanhToanVonNamNay => string.Format("Kế hoạch và thanh toán vốn đầu tư năm {0}&#x0a;          (12)", Model.INamKeHoach);
        public string sThanhToanVonTamUng => string.Format("Thanh toán KLHT của phần vốn tạm ứng theo chế độ từ KC đến hết niên độ năm trước năm {0}&#x0a;          (12)", Model.INamKeHoach);
        public string sKeHoachThanhToanVon => string.Format("Kế hoạch và thanh toán vốn đầu tư các năm trước được kéo dài thời gian thực hiện và thanh toán sang năm {0}&#x0a;          (12)", Model.INamKeHoach);
        public string sVonDaQuyetToanTrongNam => string.Format("Tổng cộng vốn đã thanh toán KLHT quyết toán trong năm {0}&#x0a;          (12)", Model.INamKeHoach);
        public string sLuyKeVonDaThanhToan => string.Format("Luỹ kế số vốn đã thanh toán từ K/C đến hết năm {0}&#x0a;          (12)", Model.INamKeHoach);
        public string sKeHoachVonNamNay => string.Format("Kế hoạch vốn đầu tư năm {0}&#x0a;          (12)", Model.INamKeHoach);
        #endregion

        #region data combobox
        private ObservableCollection<ComboboxItem> _cbxHopDong;
        public ObservableCollection<ComboboxItem> CbxHopDong
        {
            get => _cbxHopDong;
            set => SetProperty(ref _cbxHopDong, value);
        }
        #endregion

        public RelayCommand SaveDataCommand { get; }

        public AnnualSettlementDetailViewModel(
            IVdtQtDeNghiQuyetToanNienDoService quyetToanNienDoService,
            IVdtQtDeNghiQuyetToanNienDoChiTietService quyetToanNienDoChiTietService,
            IVdtDaTtHopDongService ttHopDongService,
            IVdtQtXuLySoLieuService xldlService,
            ISessionService sessionService,
            IMapper mapper)
        {
            _quyetToanNienDoService = quyetToanNienDoService;
            _quyetToanNienDoChiTietService = quyetToanNienDoChiTietService;
            _ttHopDongService = ttHopDongService;
            _xldlService = xldlService;
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
            Description = "Đề nghị quyết toán niên độ chi tiết";
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
                VdtQtDenghiQuyetToanNienDoChiTietModel newItem = ObjectCopier.Clone(SelectedItem);
                newItem.fGiaTriQuyetToanNamTruocDonVi = 0;
                newItem.fGiaTriQuyetToanNamNayDonVi = 0;
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
            List<VdtQtDenghiQuyetToanNienDoChiTietModel> lstDataNew = Items.Where(n => n.fGiaTriQuyetToanNamTruocDonVi != 0 && n.fGiaTriQuyetToanNamNayDonVi != 0 && !n.IsDeleted).ToList();
            if (lstDataNew == null || lstDataNew.Count == 0)
            {
                messageBuilder.Append(Resources.MsgErrorDataEmpty);
                MessageBox.Show(String.Join("\n", messageBuilder.ToString()));
                return;
            }
            List<VdtQtDeNghiQuyetToanNienDoChiTiet> lstData = new List<VdtQtDeNghiQuyetToanNienDoChiTiet>();
            foreach (var item in lstDataNew)
            {
                lstData.Add(ConvertDataInsert(item));
            }
            if (messageBuilder.Length != 0)
            {
                MessageBox.Show(String.Join("\n", messageBuilder.ToString()));
                return;
            }
            bool isSucess = _quyetToanNienDoChiTietService.Insert(Model.Id, lstData);
            if (!isSucess)
            {
                messageBuilder.AppendFormat(Resources.AlertDataError);
                MessageBox.Show(String.Join("\n", messageBuilder.ToString()));
                return;
            }
            _xldlService.Insert(_mapper.Map<VdtQtDeNghiQuyetToanNienDo>(Model), _mapper.Map<List<VdtQtXuLySoLieu>>(lstDataNew), _sessionService.Current.Principal);
            messageBuilder.AppendFormat(Resources.MsgSaveDone);
            MessageBox.Show(messageBuilder.ToString());
            LoadData();
        }
        #endregion

        #region Helper
        private VdtQtDeNghiQuyetToanNienDoChiTiet ConvertDataInsert(VdtQtDenghiQuyetToanNienDoChiTietModel data)
        {
            VdtQtDeNghiQuyetToanNienDoChiTiet dataInsert = new VdtQtDeNghiQuyetToanNienDoChiTiet();
            dataInsert.Id = Guid.NewGuid();
            dataInsert.IIdDeNghiQuyetToanNienDoId = Model.Id;
            dataInsert.IIdMucId = data.iId_MucId;
            dataInsert.IIdTieuMucId = data.iId_TieuMucId;
            dataInsert.IIdTietMucId = data.iId_TietMucId;
            dataInsert.IIdNganhId = data.iId_NganhId;
            dataInsert.IIdDuAnId = data.iID_DuAnId;
            dataInsert.FGiaTriQuyetToanNamTruocDonVi = data.fGiaTriQuyetToanNamTruocDonVi;
            dataInsert.FGiaTriQuyetToanNamTruoc = data.fGiaTriQuyetToanNamTruoc;
            dataInsert.FGiaTriQuyetToanNamNayDonVi = data.fGiaTriQuyetToanNamNayDonVi;
            dataInsert.FGiaTriQuyetToanNamNay = data.fGiaTriQuyetToanNamNay;
            dataInsert.IIdDonViTienTeId = data.iId_DonViTienTeId ?? Guid.Empty;
            dataInsert.IIdTienTeId = data.iId_TienTeId ?? Guid.Empty;
            dataInsert.MTiGia = data.mTiGia;
            dataInsert.MTiGiaDonVi = data.mTiGiaDonVi;
            dataInsert.Lns = data.LNS;
            dataInsert.L = data.L;
            dataInsert.K = data.K;
            dataInsert.M = data.M;
            dataInsert.Tm = data.TM;
            dataInsert.Ttm = data.TTM;
            dataInsert.Ng = data.NG;
            return dataInsert;
        }
        #endregion
    }
}
