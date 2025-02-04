using AutoMapper;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Media;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.RequestSettlement
{
    public class RequestSettlementDetailViewModel : DetailViewModelBase<DeNghiQuyetToanModel, DeNghiQuyetToanChiTietModel>
    {
        private IMapper _mapper;
        private ISessionService _sessionService;
        private readonly ILog _logger;
        private IVdtQtDeNghiQuyetToanChiTietService _qtDeNghiQuyetToanChiTietService;
        private IVdtDeNghiQuyetToanService _vdtDeNghiQuyetToanService;
        private readonly IVdtDaDuToanService _vdtDaDuToanService;
        private readonly IVdtQddtKhlcnhaThauService _vdtQddtKhlcnhaThauService;
        private readonly IApproveProjectService _approveProjectService;
        private readonly INsNguoiDungDonViService _nsNguoiDungDonViService;
        private readonly INsDonViService _nSDonViService;

        public override string Name => "Quản lý đề nghị quyết toán dự án hoàn thành chi tiết";
        public override string Title => "Quản lý đề nghị quyết toán dự án hoàn thành chi tiết";
        public override Type ContentType => typeof(View.Investment.EndOfInvestment.RequestSettlement.RequestSettlementDetail);
        public override PackIconKind IconKind => PackIconKind.FileDocumentBoxMultiple;
        public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted);
        public bool IsTheoHangMuc { get; set; }
        public bool IsEnableButton => !(IsReadOnlyTable || IsDoubleClick);
        public bool IsReadOnlyGrid => (IsReadOnlyTable || IsDoubleClick);
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler ClosePopup;
        public List<DeNghiQuyetToanChiTietModel> ListDeNghiQuyetToan = new List<DeNghiQuyetToanChiTietModel>();
        public VdtQtDeNghiQuyetToan DeNghiQuyetToanModel = new VdtQtDeNghiQuyetToan();
        public List<VdtQtDeNghiQuyetToanChiTiet> ListDbData = new List<VdtQtDeNghiQuyetToanChiTiet>();
        public RelayCommand SaveCommand { get; }
        public RelayCommand CloseWindowCommand { get; }
        public RelayCommand ShowChildCommand { get; }

        private string _ngayLapBaoCao;
        public string NgayLapBaoCao
        {
            get => _ngayLapBaoCao;
            set => SetProperty(ref _ngayLapBaoCao, value);
        }

        private string _tenDonVi;
        public string TenDonVi
        {
            get => _tenDonVi;
            set => SetProperty(ref _tenDonVi, value);
        }

        private double _tongDuToanDuocDuyet;
        public double TongDuToanDuocDuyet
        {
            get => _tongDuToanDuocDuyet;
            set => SetProperty(ref _tongDuToanDuocDuyet, value);
        }

        private double _tongGiaTriQuyetToanAB;
        public double TongGiaTriQuyetToanAB
        {
            get => _tongGiaTriQuyetToanAB;
            set => SetProperty(ref _tongGiaTriQuyetToanAB, value);
        }

        private double _tongKetQuaKiemToan;
        public double TongKetQuaKiemToan
        {
            get => _tongKetQuaKiemToan;
            set => SetProperty(ref _tongKetQuaKiemToan, value);
        }

        private double _tongGiaTriDeNghiQuyetToan;
        public double TongGiaTriDeNghiQuyetToan
        {
            get => _tongGiaTriDeNghiQuyetToan;
            set => SetProperty(ref _tongGiaTriDeNghiQuyetToan, value);
        }

        private double _tongSoVoiDuToan;
        public double TongSoVoiDuToan
        {
            get => _tongSoVoiDuToan;
            set => SetProperty(ref _tongSoVoiDuToan, value);
        }

        private double _tongSoVoiQuyetToan;
        public double TongSoVoiQuyetToan
        {
            get => _tongSoVoiQuyetToan;
            set => SetProperty(ref _tongSoVoiQuyetToan, value);
        }

        private double _tongSoVoiKetQuaKiemToan;
        public double TongSoVoiKetQuaKiemToan
        {
            get => _tongSoVoiKetQuaKiemToan;
            set => SetProperty(ref _tongSoVoiKetQuaKiemToan, value);
        }

        private bool _isReadOnlyTable;
        public bool IsReadOnlyTable
        {
            get => _isReadOnlyTable;
            set => SetProperty(ref _isReadOnlyTable, value);
        }

        private bool _isDoubleClick;
        public bool IsDoubleClick
        {
            get => _isDoubleClick;
            set => SetProperty(ref _isDoubleClick, value);
        }

        public string SHeaderCot4 { get; set; }

        public RequestSettlementDetailViewModel(
           IMapper mapper,
           ISessionService sessionService,
           IVdtQtDeNghiQuyetToanChiTietService qtDeNghiQuyetToanChiTietService,
           IVdtDeNghiQuyetToanService vdtDeNghiQuyetToanService,
           IVdtDaDuToanService vdtDaDuToanService,
           IVdtQddtKhlcnhaThauService vdtQddtKhlcnhaThauService,
           IApproveProjectService approveProjectService,
           INsNguoiDungDonViService nsNguoiDungDonViService,
           INsDonViService nSDonViService,
           ILog logger)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _qtDeNghiQuyetToanChiTietService = qtDeNghiQuyetToanChiTietService;
            _vdtDeNghiQuyetToanService = vdtDeNghiQuyetToanService;
            _vdtDaDuToanService = vdtDaDuToanService;
            _vdtQddtKhlcnhaThauService = vdtQddtKhlcnhaThauService;
            _approveProjectService = approveProjectService;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            _nSDonViService = nSDonViService;
            _logger = logger;

            SaveCommand = new RelayCommand(obj => OnSaveData());
            CloseWindowCommand = new RelayCommand(obj => OnCloseWindow());
            ShowChildCommand = new RelayCommand(obj => OnShowNextLevel());
        }

        private List<NguoiDungDonVi> GetListNguoiDungDonVi()
        {
            var predicate = PredicateBuilder.True<NguoiDungDonVi>();
            predicate = predicate.And(x => x.IIDMaNguoiDung.Equals(_sessionService.Current.Principal));
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            List<NguoiDungDonVi> nsDungDonVis = _nsNguoiDungDonViService.FindAll(predicate).ToList();
            return nsDungDonVis;
        }

        private bool CheckReadonly(string idDonVi)
        {
            List<NguoiDungDonVi> listNguoiDungDonVi = GetListNguoiDungDonVi();
            if (listNguoiDungDonVi == null || listNguoiDungDonVi.Count() == 0)
            {
                return true;
            }
            if (!listNguoiDungDonVi.Select(n => n.IIdMaDonVi).ToList().Contains(idDonVi))
            {
                return true;
            }
            return false;
        }

        public List<DonVi> GetListDonVi()
        {
            var session = _sessionService.Current;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            List<DonVi> listDonVi = _nSDonViService.FindByCondition(predicate).ToList();
            return listDonVi;
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            IsReadOnlyTable = false;
            DeNghiQuyetToanModel = _vdtDeNghiQuyetToanService.Find(Model.Id);
            if (DeNghiQuyetToanModel.BKhoa || CheckReadonly(DeNghiQuyetToanModel.IIDMaDonVi))
            {
                IsReadOnlyTable = true;
            }
            List<DonVi> listDonVi = GetListDonVi();
            TenDonVi = string.Empty;
            if (listDonVi != null && listDonVi.Count() > 0)
            {
                TenDonVi = listDonVi.FirstOrDefault(n => n.IIDMaDonVi == DeNghiQuyetToanModel.IIDMaDonVi) != null ?
                    listDonVi.FirstOrDefault(n => n.IIDMaDonVi == DeNghiQuyetToanModel.IIDMaDonVi).TenDonVi : string.Empty;
            }
            NgayLapBaoCao = DeNghiQuyetToanModel.DThoiGianNhanBaoCao.HasValue ? DeNghiQuyetToanModel.DThoiGianNhanBaoCao.Value.ToString("dd/MM/yyyy") : string.Empty;
            if(DeNghiQuyetToanModel.iID_LoaiQuyetToan == LOAI_QUYETTOAN_DUAN_HOANTHANH.TypeName.THEO_HANGMUC)
            {
                IsTheoHangMuc = true;
                List<VdtDaDuToanChiPhiDataQuery> listDuToanChiPhi = _qtDeNghiQuyetToanChiTietService.FindListDuToanChiPhiByDuAnNew(DeNghiQuyetToanModel.iID_QuyetDinh.Value);
                ListDeNghiQuyetToan = _mapper.Map<List<Model.DeNghiQuyetToanChiTietModel>>(listDuToanChiPhi);

                //ListDeNghiQuyetToan.Where(n => n.PhanCap == 1 || n.PhanCap == 2).Select(n => { n.IsShow = true; return n; }).ToList();
                ListDeNghiQuyetToan.Where(n => n.PhanCap == 1).Select(n => { n.IsShow = true; return n; }).ToList();
                ListDeNghiQuyetToan.Select(n => { n.IsChiPhi = true; return n; }).ToList();
                CreateMaOrderItem();
                ListDbData = _qtDeNghiQuyetToanChiTietService.FindByDeNghiQuyetToanId(Model.Id);

                List<DeNghiQuyetToanChiTietModel> listHangMucShow = new List<DeNghiQuyetToanChiTietModel>();

                foreach (DeNghiQuyetToanChiTietModel item in ListDeNghiQuyetToan)
                {
                    item.ListHangMuc = LoadHangMuc(item);
                    //int indexInsert = ListDeNghiQuyetToan.IndexOf(item);
                    if (ListDbData != null && ListDbData.Count > 0)
                    {
                        foreach (DeNghiQuyetToanChiTietModel data in item.ListHangMuc)
                        {
                            VdtQtDeNghiQuyetToanChiTiet entity = ListDbData.Where(n => n.IIdHangMucId == data.HangMucId).FirstOrDefault();
                            if (entity != null)
                            {
                                data.IsShow = true;
                                data.IdChiPhiDuAnParent = item.ChiPhiId;
                                listHangMucShow.Add(data);
                                data.FGiaTriAB = entity.FGiaTriQuyetToanAB.HasValue ? entity.FGiaTriQuyetToanAB.Value : 0;
                                data.FGiaTriKiemToan = entity.FGiaTriKiemToan.HasValue ? entity.FGiaTriKiemToan.Value : 0;
                                data.FGiaTriDeNghiQuyetToan = entity.FGiaTriDeNghiQuyetToan.HasValue ? entity.FGiaTriDeNghiQuyetToan.Value : 0;
                                //indexInsert++;
                            }
                        }
                    }
                }

                if (ListDbData != null && ListDbData.Count > 0)
                {
                    foreach (DeNghiQuyetToanChiTietModel data in ListDeNghiQuyetToan)
                    {
                        VdtQtDeNghiQuyetToanChiTiet entity = ListDbData.Where(n => n.IIdChiPhiId == data.ChiPhiId).FirstOrDefault();
                        if (entity != null)
                        {
                            data.FGiaTriKiemToan = entity.FGiaTriKiemToan.HasValue ? entity.FGiaTriKiemToan.Value : 0;
                            data.FGiaTriDeNghiQuyetToan = entity.FGiaTriDeNghiQuyetToan.HasValue ? entity.FGiaTriDeNghiQuyetToan.Value : 0;
                            data.FGiaTriAB = entity.FGiaTriQuyetToanAB.HasValue ? entity.FGiaTriQuyetToanAB.Value : 0;
                            data.IsShow = true;
                        }
                    }
                }
                ListDeNghiQuyetToan.AddRange(listHangMucShow);
                ListDeNghiQuyetToan.Where(n => n.FGiaTriKiemToan != 0 || n.FGiaTriDeNghiQuyetToan != 0 || n.FGiaTriAB != 0).Select(n => { n.IsShow = true; return n; }).ToList();
                Items = new ObservableCollection<DeNghiQuyetToanChiTietModel>(ListDeNghiQuyetToan.Where(n => n.IsShow).OrderBy(n => n.MaOrderDb));

                foreach (DeNghiQuyetToanChiTietModel model in Items)
                {
                    model.PropertyChanged += DetailModel_PropertyChanged;
                }
                CheckHangCha();
            }
            if(DeNghiQuyetToanModel.iID_LoaiQuyetToan == LOAI_QUYETTOAN_DUAN_HOANTHANH.TypeName.THEO_GOITHAU)
            {
                IsTheoHangMuc = false;
                Items = new ObservableCollection<DeNghiQuyetToanChiTietModel>();
                List <VdtDaGoiThau> lstGoiThau = _vdtQddtKhlcnhaThauService.ListGoiThauByKHLCNhaThauId(Guid.Parse(DeNghiQuyetToanModel.iID_QuyetDinh.ToString())).ToList();
                ListDbData = _qtDeNghiQuyetToanChiTietService.FindByDeNghiQuyetToanId(Model.Id);
                if(ListDbData != null && ListDbData.Count() > 0)
                {
                    foreach (var item in ListDbData)
                    {
                        VdtDaGoiThau gt = lstGoiThau.Where(n => n.Id == item.IIDGoiThauId).FirstOrDefault();
                        
                        if(gt != null)
                            Items.Add(new DeNghiQuyetToanChiTietModel
                            {
                                Id = item.Id,
                                TenChiPhi = gt.STenGoiThau,
                                GoiThauId = item.Id,
                                GiaTriPheDuyet = gt.FTienTrungThau,
                                FGiaTriDeNghiQuyetToan = (Double)item.FGiaTriDeNghiQuyetToan,
                                FGiaTriKiemToan = (Double)item.FGiaTriKiemToan,
                                FGiaTriAB = (Double)item.FGiaTriQuyetToanAB,
                                MaOrderDb = item.SMaOrder,
                                IsGoiThau = true,

                            });
                    }
                }
                    
                
                if(lstGoiThau != null && lstGoiThau.Count > 0)
                {
                    int i = 1;
                    foreach (var item in lstGoiThau)
                    {
                        if(ListDbData != null && !ListDbData.Select(n=>n.IIDGoiThauId).Contains(item.Id))
                        Items.Add(new DeNghiQuyetToanChiTietModel
                        {
                            Id = Guid.NewGuid(),
                            TenChiPhi = item.STenGoiThau,
                            GoiThauId = item.Id,
                            GiaTriPheDuyet = item.FTienTrungThau,
                            IsGoiThau = true,
                            MaOrderDb = i++.ToString(),
                        });
                    }
                }
                if (Items != null)
                    Items = new ObservableCollection<DeNghiQuyetToanChiTietModel>(Items.OrderBy(x => x.MaOrderDb));


            }
            foreach (DeNghiQuyetToanChiTietModel model in Items)
            {
                model.PropertyChanged += DetailModel_PropertyChanged;
            }
            //CalculateData();
            CalculateTotal();
            OnPropertyChanged(nameof(NgayLapBaoCao));
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsReadOnlyTable));
            OnPropertyChanged(nameof(IsEnableButton));
            OnPropertyChanged(nameof(TenDonVi));
        }

        public void CheckHangCha()
        {
            foreach (DeNghiQuyetToanChiTietModel item in Items)
            {
                if (item.IsChiPhi)
                {
                    DeNghiQuyetToanChiTietModel child = Items.Where(n => n.IdChiPhiDuAnParent == item.ChiPhiId).FirstOrDefault();
                    if (child != null)
                    {
                        item.IsHangCha = true;
                    }
                    else
                    {
                        item.IsHangCha = false;
                    }
                    if (item.ListHangMuc != null && item.ListHangMuc.Count > 0)
                    {
                        foreach (DeNghiQuyetToanChiTietModel hangMucChild in item.ListHangMuc)
                        {
                            DeNghiQuyetToanChiTietModel checkItem = Items.Where(n => n.HangMucId == hangMucChild.HangMucId).FirstOrDefault();
                            if (checkItem != null)
                            {
                                item.IsHangCha = true;
                            }
                        }
                    }
                }
                else
                {
                    DeNghiQuyetToanChiTietModel child = Items.Where(n => n.IdHangMucParent == item.HangMucId).FirstOrDefault();
                    //if (child == null)
                    //{
                    //    child = Items.Where(n => n.IdChiPhiDuAnParent == item.ChiPhiId).FirstOrDefault();
                    //}
                    if (child != null)
                    {
                        item.IsHangCha = true;
                    }
                    else
                    {
                        item.IsHangCha = false;
                    }
                }
            }
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(DeNghiQuyetToanChiTietModel.FGiaTriKiemToan) ||
                args.PropertyName == nameof(DeNghiQuyetToanChiTietModel.FGiaTriDeNghiQuyetToan) ||
                args.PropertyName == nameof(DeNghiQuyetToanChiTietModel.FGiaTriAB))
            {
                DeNghiQuyetToanChiTietModel item = Items.Where(x => x.Id == ((DeNghiQuyetToanChiTietModel)sender).Id).First();

                if (!item.IsHangCha)
                {
                    item.IsModified = true;
                    CalculateData();
                    CalculateTotal();
                }

                OnPropertyChanged(nameof(IsSaveData));
            }
        }

        private void CalculateTotal()
        {
            TongDuToanDuocDuyet = 0;
            TongGiaTriQuyetToanAB = 0;
            TongKetQuaKiemToan = 0;
            TongGiaTriDeNghiQuyetToan = 0;
            TongSoVoiDuToan = 0;
            TongSoVoiQuyetToan = 0;
            TongSoVoiKetQuaKiemToan = 0;
            foreach (var item in Items.Where(n => !n.IsHangCha))
            {
                TongDuToanDuocDuyet += item.GiaTriPheDuyet.HasValue ? item.GiaTriPheDuyet.Value : 0;
                TongGiaTriQuyetToanAB += item.FGiaTriAB;
                TongKetQuaKiemToan += item.FGiaTriKiemToan;
                TongGiaTriDeNghiQuyetToan += item.FGiaTriDeNghiQuyetToan;
                TongSoVoiDuToan += item.SoVoiDuToan;
                TongSoVoiQuyetToan += item.SoVoiQuyetToan;
                TongSoVoiKetQuaKiemToan += item.SoVoiKetQuaKiemToan;
            }
            OnPropertyChanged(nameof(TongDuToanDuocDuyet));
            OnPropertyChanged(nameof(TongGiaTriQuyetToanAB));
            OnPropertyChanged(nameof(TongKetQuaKiemToan));
            OnPropertyChanged(nameof(TongGiaTriDeNghiQuyetToan));
            OnPropertyChanged(nameof(TongSoVoiDuToan));
            OnPropertyChanged(nameof(TongSoVoiQuyetToan));
            OnPropertyChanged(nameof(TongSoVoiKetQuaKiemToan));
        }

        private void CalculateData()
        {
            Items.Where(x => x.IsHangCha).Select(x => { x.FGiaTriKiemToan = 0; x.FGiaTriDeNghiQuyetToan = 0; x.FGiaTriAB = 0; return x; }).ToList();
            foreach (var item in Items.Where(x => !x.IsHangCha && !x.IsDeleted && (x.FGiaTriKiemToan != 0 || x.FGiaTriDeNghiQuyetToan != 0 || x.FGiaTriAB != 0)))
            {
                CalculateParent(item, item);
            }
        }

        private void CalculateParent(DeNghiQuyetToanChiTietModel currentItem, DeNghiQuyetToanChiTietModel selfItem)
        {
            DeNghiQuyetToanChiTietModel parentItem;
            if (currentItem.IsChiPhi)
            {
                parentItem = Items.Where(x => x.ChiPhiId == currentItem.IdChiPhiDuAnParent && currentItem.IdChiPhiDuAnParent != Guid.Empty).FirstOrDefault();
            }
            else
            {
                parentItem = Items.Where(x => x.HangMucId == currentItem.IdHangMucParent && currentItem.IdHangMucParent != Guid.Empty).FirstOrDefault();
                if (parentItem == null)
                {
                    parentItem = Items.Where(x => x.IsHangCha && x.ChiPhiId == currentItem.IdChiPhiDuAnParent && currentItem.IdChiPhiDuAnParent != Guid.Empty).FirstOrDefault();
                }
            }

            if (parentItem == null)
                return;
            parentItem.FGiaTriAB += selfItem.FGiaTriAB;
            parentItem.FGiaTriKiemToan += selfItem.FGiaTriKiemToan;
            parentItem.FGiaTriDeNghiQuyetToan += selfItem.FGiaTriDeNghiQuyetToan;
            CalculateParent(parentItem, selfItem);
        }

        public void OnSaveData()
        {
            _qtDeNghiQuyetToanChiTietService.DeleteByDeNghiQuyetToanId(Model.Id);
            List<VdtQtDeNghiQuyetToanChiTiet> entitys = new List<VdtQtDeNghiQuyetToanChiTiet>();
            foreach (DeNghiQuyetToanChiTietModel item in Items.Where(n => !n.IsDeleted && ((!n.IsHangCha) || (n.IsHangCha
            && (n.FGiaTriKiemToan != 0 || n.FGiaTriDeNghiQuyetToan != 0 || n.FGiaTriAB != 0)))))
            {
                entitys.Add(new VdtQtDeNghiQuyetToanChiTiet
                {
                    IIdDeNghiQuyetToanId = Model.Id,
                    IIdChiPhiId = item.IsChiPhi ? item.ChiPhiId : Guid.Empty,
                    IIdHangMucId = !item.IsChiPhi ? item.HangMucId : Guid.Empty,
                    IIDGoiThauId = item.GoiThauId,
                    FGiaTriQuyetToanAB = item.FGiaTriAB,
                    FGiaTriKiemToan = item.FGiaTriKiemToan,
                    FGiaTriDeNghiQuyetToan = item.FGiaTriDeNghiQuyetToan,
                    SMaOrder = item.MaOrderDb,
                    SUserCreate = _sessionService.Current.Principal,
                    DDateCreate = DateTime.Now
                });
            }
            _qtDeNghiQuyetToanChiTietService.AddRange(entitys);
            _vdtDeNghiQuyetToanService.UpdateTotal(Model.Id.ToString());
            System.Windows.Forms.MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            LoadData();
        }

        public List<DeNghiQuyetToanChiTietModel> LoadHangMuc(DeNghiQuyetToanChiTietModel chiphi)
        {
            List<DuToanDetailQuery> listData = new List<DuToanDetailQuery>();
            //Lúc ban đầu sẽ lấy hạng mục ở phê duyệt dự án => check tồn tại Dutoan trong bang DuToanHangMuc
            List<VdtDaDuToan> duToan = _vdtDaDuToanService.FindListByDuAnId(DeNghiQuyetToanModel.IIdDuAnId.Value);
            if (duToan == null || duToan.Count == 0)
            {
                return new List<DeNghiQuyetToanChiTietModel>();
            }
            bool checkExitsDuToanHangMuc = _vdtDaDuToanService.CheckExistInDuToanHangMuc(string.Join(",", duToan.Select(n => n.Id).ToList()), chiphi.ChiPhiId);
            if (checkExitsDuToanHangMuc)
            {
                listData = _vdtDaDuToanService.FindListDetail(string.Join(",", duToan.Select(n => n.Id).ToList()), chiphi.ChiPhiId).ToList();
            }
            else
            {
                VdtDaQddauTu qdDauTu = _approveProjectService.FindByDuAnId(DeNghiQuyetToanModel.IIdDuAnId.Value);
                if (qdDauTu != null)
                {
                    listData = _vdtDaDuToanService.ListHangMucInitial(qdDauTu.Id, chiphi.ChiPhiId).ToList();
                }
            }
            listData.Select(n => { n.Id = Guid.NewGuid(); n.MaOrDer = chiphi.MaOrderDb + "_" + n.MaOrDer; return n; }).ToList();
            List<DeNghiQuyetToanChiTietModel> listResult = _mapper.Map<List<Model.DeNghiQuyetToanChiTietModel>>(listData);
            listResult.Select(n => { n.ChiPhiIdParentOfHangMuc = chiphi.ChiPhiId; return n; }).ToList();
            return listResult;
        }

        public void OnShowNextLevel()
        {
            if (SelectedItem == null || IsReadOnlyTable)
                return;
            int index = Items.IndexOf(SelectedItem);
            List<Model.DeNghiQuyetToanChiTietModel> listInsert = ListDeNghiQuyetToan.Where(n => n.IdChiPhiDuAnParent == SelectedItem.ChiPhiId).ToList();
            foreach (DeNghiQuyetToanChiTietModel item in listInsert)
            {
                if (!Items.Contains(item))
                {
                    //Items.Insert(index + 1, item);
                    Items.Insert(index + 1, item);
                    item.PropertyChanged += DetailModel_PropertyChanged;
                    index++;
                    SelectedItem.IsHangCha = true;
                    item.IsHangCha = false;
                    SelectedItem.FGiaTriKiemToan = 0;
                    SelectedItem.FGiaTriAB = 0;
                    SelectedItem.FGiaTriDeNghiQuyetToan = 0;
                }
            }
            if ((SelectedItem.ListHangMuc != null && SelectedItem.ListHangMuc.Count > 0) || SelectedItem.ChiPhiIdParentOfHangMuc.HasValue)
            {
                if (SelectedItem.ChiPhiIdParentOfHangMuc.HasValue)
                {
                    //Click hang muc
                    DeNghiQuyetToanChiTietModel chiPhiParent = Items.Where(n => n.ChiPhiId == SelectedItem.ChiPhiIdParentOfHangMuc.Value).FirstOrDefault();

                    DeNghiQuyetToanChiTietModel item = chiPhiParent.ListHangMuc.OrderBy(n => n.MaOrDer).FirstOrDefault();
                    if (SelectedItem == item)
                    {
                        if (Items.Contains(item))
                        {
                            List<DeNghiQuyetToanChiTietModel> listInsertHangMuc = chiPhiParent.ListHangMuc.Where(n => n.IdHangMucParent == item.HangMucId).ToList();
                            int indexInsert = Items.IndexOf(item);
                            foreach (DeNghiQuyetToanChiTietModel itemChild in listInsertHangMuc)
                            {
                                if (!Items.Contains(itemChild))
                                {
                                    item.IsHangCha = true;
                                    item.FGiaTriAB = 0;
                                    item.FGiaTriKiemToan = 0;
                                    item.FGiaTriDeNghiQuyetToan = 0;
                                    itemChild.IsHangCha = false;
                                    Items.Insert(indexInsert + 1, itemChild);
                                    itemChild.PropertyChanged += DetailModel_PropertyChanged;
                                    indexInsert++;
                                }
                            }
                        }
                        else
                        {
                            chiPhiParent.IsHangCha = true;
                            chiPhiParent.FGiaTriAB = 0;
                            chiPhiParent.FGiaTriKiemToan = 0;
                            chiPhiParent.FGiaTriDeNghiQuyetToan = 0;
                            item.IsHangCha = false;
                            Items.Insert(index + 1, item);
                            item.PropertyChanged += DetailModel_PropertyChanged;
                            index++;
                            if (item.IsHangCha)
                            {
                                item.IdChiPhiDuAnParent = SelectedItem.ChiPhiId;
                            }
                        }
                    }
                    else
                    {
                        DeNghiQuyetToanChiTietModel hangMucSelected = chiPhiParent.ListHangMuc.Where(n => n.HangMucId == SelectedItem.HangMucId).FirstOrDefault();
                        List<DeNghiQuyetToanChiTietModel> listInsertHangMuc = chiPhiParent.ListHangMuc.Where(n => n.IdHangMucParent == hangMucSelected.HangMucId)
                                .OrderBy(n => n.MaOrDer).ToList();
                        if (listInsertHangMuc != null && listInsertHangMuc.Count > 0)
                        {
                            hangMucSelected.IsHangCha = true;
                        }
                        int indexInsert = Items.IndexOf(hangMucSelected);
                        foreach (DeNghiQuyetToanChiTietModel itemChild in listInsertHangMuc)
                        {
                            if (!Items.Contains(itemChild))
                            {
                                item.IsHangCha = true;
                                item.FGiaTriAB = 0;
                                item.FGiaTriKiemToan = 0;
                                item.FGiaTriDeNghiQuyetToan = 0;
                                itemChild.IsHangCha = false;
                                Items.Insert(indexInsert + 1, itemChild);
                                itemChild.PropertyChanged += DetailModel_PropertyChanged;
                                indexInsert++;
                            }
                        }
                    }
                }
                else
                {
                    //Click chi phi
                    DeNghiQuyetToanChiTietModel item = SelectedItem.ListHangMuc.OrderBy(n => n.MaOrDer).FirstOrDefault();
                    //
                    List<DeNghiQuyetToanChiTietModel> listHangMucInsert = SelectedItem.ListHangMuc.Where(n => n.MaOrDer.Length == item.MaOrDer.Length).ToList();
                    foreach (DeNghiQuyetToanChiTietModel itemInsert in listHangMucInsert)
                    {
                        if (!Items.Contains(itemInsert))
                        {
                            SelectedItem.IsHangCha = true;
                            SelectedItem.FGiaTriAB = 0;
                            SelectedItem.FGiaTriKiemToan = 0;
                            SelectedItem.FGiaTriDeNghiQuyetToan = 0;
                            //if (itemInsert.IsHangCha)
                            //{
                            itemInsert.IdChiPhiDuAnParent = SelectedItem.ChiPhiId;
                            //}
                            itemInsert.IsHangCha = false;
                            Items.Insert(index + 1, itemInsert);
                            itemInsert.PropertyChanged += DetailModel_PropertyChanged;
                            index++;
                        }
                    }

                }
            }
            CalculateData();
            OnPropertyChanged(nameof(Items));
        }

        public void CreateMaOrderItem()
        {
            if (ListDeNghiQuyetToan == null || ListDeNghiQuyetToan.Count == 0)
                return;
            List<DeNghiQuyetToanChiTietModel> roots = ListDeNghiQuyetToan.Where(n => n.IsChiPhi && n.IdChiPhiDuAnParent == Guid.Empty && n.PhanCap == 1).ToList();

            if (roots != null && roots.Count() > 0)
            {
                int count = 1;
                foreach (var item in roots)
                {
                    item.MaOrderDb = count.ToString();
                    CreateMaOrderItemChild(item);
                    count++;
                }
            }
        }

        public void CreateMaOrderItemChild(DeNghiQuyetToanChiTietModel parent)
        {
            List<DeNghiQuyetToanChiTietModel> listChild = ListDeNghiQuyetToan.Where(n => n.IdChiPhiDuAnParent == parent.ChiPhiId).ToList();
            if (listChild == null || listChild.Count == 0)
            {
                return;
            }
            for (int i = 0; i < listChild.Count; i++)
            {
                listChild[i].MaOrderDb = parent.MaOrderDb + "_" + (i + 1).ToString();
                CreateMaOrderItemChild(listChild[i]);
            }
        }

        protected override void OnRefresh(object obj)
        {
            LoadData();
        }

        public override void Init()
        {
            try
            {
                LoadData();
                LoadHeader();
                OnPropertyChanged(nameof(Items));
                OnPropertyChanged(nameof(IsSaveData));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        public void LoadHeader()
        {
            if (DeNghiQuyetToanModel.iID_LoaiQuyetToan == "1")
                SHeaderCot4 = "Dự toán được duyệt (4)";
            else if (DeNghiQuyetToanModel.iID_LoaiQuyetToan == "2")
                SHeaderCot4 = "Giá trị được duyệt (4)";
        }
        protected override void OnDelete()
        {
            if (Items != null && Items.Count > 0 && SelectedItem != null && !SelectedItem.IsHangCha && !(IsReadOnlyTable || IsDoubleClick))
            {
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
                CalculateData();
                OnPropertyChanged(nameof(IsSaveData));
            }
        }

        private void OnCloseWindow()
        {
            DataChangedEventHandler handler = ClosePopup;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }
    }
}
