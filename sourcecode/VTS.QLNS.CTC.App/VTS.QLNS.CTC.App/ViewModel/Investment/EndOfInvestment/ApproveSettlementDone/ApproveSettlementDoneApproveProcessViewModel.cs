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


namespace VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.ApproveSettlementDone
{
    public class ApproveSettlementDoneApproveProcessViewModel : DetailViewModelBase<PheDuyetQuyetToanModel, PheDuyetQuyetToanProcessModel>
    {
        private IMapper _mapper;
        private ISessionService _sessionService;
        private readonly ILog _logger;
        private IVdtQtDeNghiQuyetToanChiTietService _qtDeNghiQuyetToanChiTietService;
        private IVdtDeNghiQuyetToanService _vdtDeNghiQuyetToanService;
        private readonly IVdtDaDuToanService _vdtDaDuToanService;
        private readonly IVdtQddtKhlcnhaThauService _vdtQddtKhlcnhaThauService;
        private readonly IApproveProjectService _approveProjectService;
        private readonly IVdtQtQuyetToanChiTietService _qtQuyetToanChiTietService;
        private readonly INsNguoiDungDonViService _nsNguoiDungDonViService;
        private readonly INsDonViService _nSDonViService;

        public override string Name => "Phê duyệt quyết toán dự án hoàn thành chi tiết";
        public override string Title => "Phê duyệt quyết toán dự án hoàn thành chi tiết";
        public override Type ContentType => typeof(View.Investment.EndOfInvestment.ApproveSettlementDone.ApproveSettlementDoneApproveProcess);
        public override PackIconKind IconKind => PackIconKind.FileDocumentBoxMultiple;
        public bool IsEnableButton => !(IsReadOnlyTable || IsDoubleClick);
        public bool IsReadOnlyGrid => (IsReadOnlyTable || IsDoubleClick);
        public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted);
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler ClosePopup;
        public List<DeNghiQuyetToanChiTietModel> ListDeNghiQuyetToan = new List<DeNghiQuyetToanChiTietModel>();
        public VdtQtDeNghiQuyetToan DeNghiQuyetToanModel = new VdtQtDeNghiQuyetToan();
        public List<VdtQtDeNghiQuyetToanChiTiet> ListDbDeNghiQuyetToan = new List<VdtQtDeNghiQuyetToanChiTiet>();

        private double _tongDuToanDuocDuyet;
        public double TongDuToanDuocDuyet
        {
            get => _tongDuToanDuocDuyet;
            set => SetProperty(ref _tongDuToanDuocDuyet, value);
        }

        private double _tongChuDauTuDeNghiQuyetToan;
        public double TongChuDauTuDeNghiQuyetToan
        {
            get => _tongChuDauTuDeNghiQuyetToan;
            set => SetProperty(ref _tongChuDauTuDeNghiQuyetToan, value);
        }

        private double _tongGiaTriThamTra;
        public double TongGiaTriThamTra
        {
            get => _tongGiaTriThamTra;
            set => SetProperty(ref _tongGiaTriThamTra, value);
        }

        private double _tongGiaTriQuyetToan;
        public double TongGiaTriQuyetToan
        {
            get => _tongGiaTriQuyetToan;
            set => SetProperty(ref _tongGiaTriQuyetToan, value);
        }

        private double _tongSoVoiDuToan;
        public double TongSoVoiDuToan
        {
            get => _tongSoVoiDuToan;
            set => SetProperty(ref _tongSoVoiDuToan, value);
        }

        private double _tongSoVoiDeNghi;
        public double TongSoVoiDeNghi
        {
            get => _tongSoVoiDeNghi;
            set => SetProperty(ref _tongSoVoiDeNghi, value);
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

        private string _tenDonVi;
        public string TenDonVi
        {
            get => _tenDonVi;
            set => SetProperty(ref _tenDonVi, value);
        }

        public RelayCommand SaveCommand { get; }
        public RelayCommand CloseWindowCommand { get; }
        public RelayCommand ShowChildCommand { get; }

        public ApproveSettlementDoneApproveProcessViewModel(
           IMapper mapper,
           ISessionService sessionService,
           IVdtQtDeNghiQuyetToanChiTietService qtDeNghiQuyetToanChiTietService,
           IVdtDeNghiQuyetToanService vdtDeNghiQuyetToanService,
           IVdtDaDuToanService vdtDaDuToanService,
           IVdtQddtKhlcnhaThauService vdtQddtKhlcnhaThauService,
           IApproveProjectService approveProjectService,
           IVdtQtQuyetToanChiTietService qtQuyetToanChiTietService,
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
            _qtQuyetToanChiTietService = qtQuyetToanChiTietService;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            _nSDonViService = nSDonViService;
            _logger = logger;

            SaveCommand = new RelayCommand(obj => OnSaveData());
            CloseWindowCommand = new RelayCommand(obj => OnCloseWindow());
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
            List<DeNghiQuyetToanChiTietModel> listResult = _mapper.Map<List<DeNghiQuyetToanChiTietModel>>(listData);
            listResult.Select(n => { n.ChiPhiIdParentOfHangMuc = chiphi.ChiPhiId; return n; }).ToList();
            return listResult;
        }

        private void CalculateTotal()
        {
            TongDuToanDuocDuyet = 0;
            TongChuDauTuDeNghiQuyetToan = 0;
            TongGiaTriThamTra = 0;
            TongGiaTriQuyetToan = 0;
            TongSoVoiDuToan = 0;
            TongSoVoiDeNghi = 0;
            foreach (var item in Items.Where(n => !n.IsHangCha))
            {
                TongDuToanDuocDuyet += item.GiaTriPheDuyet.HasValue ? item.GiaTriPheDuyet.Value : 0;
                TongChuDauTuDeNghiQuyetToan += item.FGiaTriDeNghiQuyetToan;
                TongGiaTriThamTra += item.GiaTriThamTra;
                TongGiaTriQuyetToan += item.GiaTriQuyetToan;
                TongSoVoiDuToan += item.SoVoiDuToan;
                TongSoVoiDeNghi += item.SoVoiDeNghi;
            }
            OnPropertyChanged(nameof(TongDuToanDuocDuyet));
            OnPropertyChanged(nameof(TongChuDauTuDeNghiQuyetToan));
            OnPropertyChanged(nameof(TongGiaTriThamTra));
            OnPropertyChanged(nameof(TongGiaTriQuyetToan));
            OnPropertyChanged(nameof(TongSoVoiDuToan));
            OnPropertyChanged(nameof(TongSoVoiDeNghi));
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
            //if (!listNguoiDungDonVi.Select(n => n.IIdMaDonVi).ToList().Contains(idDonVi))
            if (!listNguoiDungDonVi.Select(n => n.IIdMaDonVi).ToList().Contains(idDonVi) && idDonVi != null)
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
            //Get model de nghi quyet toan
            VdtQtDeNghiQuyetToan deNghiQuyetToan = _vdtDeNghiQuyetToanService.Find(Model.IdDeNghiQuyetToan);
            Items = new ObservableCollection<PheDuyetQuyetToanProcessModel>();
            if (deNghiQuyetToan == null)
                return;
            List<DonVi> listDonVi = GetListDonVi();
            TenDonVi = string.Empty;
            if (listDonVi != null && listDonVi.Count() > 0)
            {
                TenDonVi = listDonVi.FirstOrDefault(n => n.IIDMaDonVi == Model.MaDonVi) != null ? listDonVi.FirstOrDefault(n => n.IIDMaDonVi == Model.MaDonVi).TenDonVi : string.Empty;
            }
            IsReadOnlyTable = false;
            if (Model.BKhoa || CheckReadonly(Model.MaDonVi))
            {
                IsReadOnlyTable = true;
            }
            DeNghiQuyetToanModel = _vdtDeNghiQuyetToanService.Find(deNghiQuyetToan.Id);
            if(DeNghiQuyetToanModel.iID_LoaiQuyetToan == "1")
            {
               List<VdtDaDuToanChiPhiDataQuery> listDuToanChiPhi = _qtDeNghiQuyetToanChiTietService.FindListDuToanChiPhiByDuAnNew(DeNghiQuyetToanModel.iID_QuyetDinh.Value);
                ListDeNghiQuyetToan = _mapper.Map<List<DeNghiQuyetToanChiTietModel>>(listDuToanChiPhi);

                ListDeNghiQuyetToan.Where(n => n.PhanCap == 1 || n.PhanCap == 2).Select(n => { n.IsShow = true; return n; }).ToList();
                //ListDeNghiQuyetToan.Where(n => n.PhanCap == 1).Select(n => { n.IsShow = true; return n; }).ToList();
                ListDeNghiQuyetToan.Select(n => { n.IsChiPhi = true; return n; }).ToList();
                CreateMaOrderItem();
                ListDbDeNghiQuyetToan = _qtDeNghiQuyetToanChiTietService.FindByDeNghiQuyetToanId(deNghiQuyetToan.Id);

                List<DeNghiQuyetToanChiTietModel> listHangMucShow = new List<DeNghiQuyetToanChiTietModel>();
                foreach (DeNghiQuyetToanChiTietModel item in ListDeNghiQuyetToan)
                {
                    item.ListHangMuc = LoadHangMuc(item);
                    //int indexInsert = ListDeNghiQuyetToan.IndexOf(item);
                    if (ListDbDeNghiQuyetToan != null && ListDbDeNghiQuyetToan.Count > 0)
                    {
                        foreach (DeNghiQuyetToanChiTietModel data in item.ListHangMuc)
                        {
                            VdtQtDeNghiQuyetToanChiTiet entity = ListDbDeNghiQuyetToan.Where(n => n.IIdHangMucId == data.HangMucId).FirstOrDefault();
                            if (entity != null)
                            {
                                data.IsShow = true;
                                data.IdChiPhiDuAnParent = item.ChiPhiId;
                                data.MaOrderDb = entity.SMaOrder;
                                listHangMucShow.Add(data);
                                data.FGiaTriKiemToan = entity.FGiaTriKiemToan.HasValue ? entity.FGiaTriKiemToan.Value : 0;
                                data.FGiaTriDeNghiQuyetToan = entity.FGiaTriDeNghiQuyetToan.HasValue ? entity.FGiaTriDeNghiQuyetToan.Value : 0;
                                //indexInsert++;
                            }
                        }
                    }
                }

                if (ListDbDeNghiQuyetToan != null && ListDbDeNghiQuyetToan.Count > 0)
                {
                    foreach (DeNghiQuyetToanChiTietModel data in ListDeNghiQuyetToan)
                    {
                        VdtQtDeNghiQuyetToanChiTiet entity = ListDbDeNghiQuyetToan.Where(n => n.IIdChiPhiId == data.ChiPhiId).FirstOrDefault();
                        if (entity != null)
                        {
                            data.IsShow = true;
                            data.MaOrderDb = entity.SMaOrder;
                            data.FGiaTriKiemToan = entity.FGiaTriKiemToan.HasValue ? entity.FGiaTriKiemToan.Value : 0;
                            data.FGiaTriDeNghiQuyetToan = entity.FGiaTriDeNghiQuyetToan.HasValue ? entity.FGiaTriDeNghiQuyetToan.Value : 0;
                        }
                    }
                }
                ListDeNghiQuyetToan.AddRange(listHangMucShow);
                ListDeNghiQuyetToan.Where(n => n.FGiaTriKiemToan != 0 || n.FGiaTriDeNghiQuyetToan != 0).Select(n => { n.IsShow = true; return n; }).ToList();
                Items = _mapper.Map<ObservableCollection<PheDuyetQuyetToanProcessModel>>(ListDeNghiQuyetToan.Where(n => n.IsShow).OrderBy(n => n.MaOrderDb));
                //Get Db value
                List<VdtQtQuyetToanChiTiet> entitys = _qtQuyetToanChiTietService.FindByQuyetToanId(Model.Id);
                if (entitys != null && entitys.Count > 0)
                {
                    foreach (VdtQtQuyetToanChiTiet entity in entitys)
                    {
                        if (entity.IIdChiPhiId != Guid.Empty)
                        {
                            PheDuyetQuyetToanProcessModel map = Items.Where(n => n.ChiPhiId == entity.IIdChiPhiId).FirstOrDefault();
                            if (map != null)
                            {
                                map.GiaTriThamTra = entity.FGiaTriThamTra.HasValue ? entity.FGiaTriThamTra.Value : 0;
                                map.GiaTriQuyetToan = entity.FGiaTriQuyetToan.HasValue ? entity.FGiaTriQuyetToan.Value : 0;
                            }
                        }
                        else if (entity.IIdHangMucId != Guid.Empty)
                        {
                            PheDuyetQuyetToanProcessModel map = Items.Where(n => n.HangMucId == entity.IIdHangMucId).FirstOrDefault();
                            if (map != null)
                            {
                                map.GiaTriThamTra = entity.FGiaTriThamTra.HasValue ? entity.FGiaTriThamTra.Value : 0;
                                map.GiaTriQuyetToan = entity.FGiaTriQuyetToan.HasValue ? entity.FGiaTriQuyetToan.Value : 0;
                            }
                        }
                    }
                }
            }
            if(DeNghiQuyetToanModel.iID_LoaiQuyetToan == "2")
            {
                ListDbDeNghiQuyetToan = _qtDeNghiQuyetToanChiTietService.FindByDeNghiQuyetToanId(deNghiQuyetToan.Id);
                List<VdtDaGoiThau> lstGoiThau = _vdtQddtKhlcnhaThauService.ListGoiThauByKHLCNhaThauId(Guid.Parse(DeNghiQuyetToanModel.iID_QuyetDinh.ToString())).ToList();
                if (ListDbDeNghiQuyetToan != null && ListDbDeNghiQuyetToan.Count() > 0)
                {
                    foreach (var item in ListDbDeNghiQuyetToan)
                    {
                        VdtDaGoiThau gt = lstGoiThau.Where(n => n.Id == item.IIDGoiThauId).FirstOrDefault();
                        if (gt != null)
                        {
                            Items.Add(new PheDuyetQuyetToanProcessModel
                            {
                                Id = item.Id,
                                TenChiPhi = gt.STenGoiThau,
                                GoiThauId = item.Id,
                                MaOrderDb = item.SMaOrder,
                                GiaTriPheDuyet = gt.FTienTrungThau,
                                FGiaTriDeNghiQuyetToan = (Double)item.FGiaTriDeNghiQuyetToan,
                                FGiaTriKiemToan = (Double)item.FGiaTriKiemToan,
                                FGiaTriAB = (Double)item.FGiaTriQuyetToanAB,

                            });
                        }                        
                    }
                }


                if (lstGoiThau != null && lstGoiThau.Count > 0)
                {
                    foreach (var item in lstGoiThau)
                    {
                        if (ListDbDeNghiQuyetToan != null && !ListDbDeNghiQuyetToan.Select(n => n.IIDGoiThauId).Contains(item.Id))
                            Items.Add(new PheDuyetQuyetToanProcessModel
                            {
                                Id = Guid.NewGuid(),
                                TenChiPhi = item.STenGoiThau,
                                GoiThauId = item.Id,
                                GiaTriPheDuyet = item.FTienTrungThau,

                            });
                    }
                }

                //Get Db value
                List<VdtQtQuyetToanChiTiet> entitys = _qtQuyetToanChiTietService.FindByQuyetToanId(Model.Id);
                if (entitys != null && entitys.Count > 0)
                {
                    foreach (VdtQtQuyetToanChiTiet entity in entitys)
                    {
                        if (entity.IIdGoiThauId != Guid.Empty)
                        {
                            PheDuyetQuyetToanProcessModel map = Items.Where(n => n.GoiThauId == entity.IIdGoiThauId).FirstOrDefault();
                            if (map != null)
                            {
                                map.GiaTriThamTra = entity.FGiaTriThamTra.HasValue ? entity.FGiaTriThamTra.Value : 0;
                                map.GiaTriQuyetToan = entity.FGiaTriQuyetToan.HasValue ? entity.FGiaTriQuyetToan.Value : 0;
                            }
                        }
                    }
                }
                if (Items != null)
                    Items = new ObservableCollection<PheDuyetQuyetToanProcessModel>(Items.OrderBy(x => x.MaOrderDb));
            }

            foreach (PheDuyetQuyetToanProcessModel model in Items)
            {
                model.PropertyChanged += DetailModel_PropertyChanged;
            }
            CheckHangCha();
            CalculateTotal();
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsReadOnlyTable));
            OnPropertyChanged(nameof(TenDonVi));
            OnPropertyChanged(nameof(IsReadOnlyGrid));
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

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(PheDuyetQuyetToanProcessModel.GiaTriThamTra) ||
                args.PropertyName == nameof(PheDuyetQuyetToanProcessModel.GiaTriQuyetToan))
            {
                PheDuyetQuyetToanProcessModel item = Items.Where(x => x.Id == ((PheDuyetQuyetToanProcessModel)sender).Id).First();

                if ((!item.IsHangCha && DeNghiQuyetToanModel.iID_LoaiQuyetToan == "1") || DeNghiQuyetToanModel.iID_LoaiQuyetToan == "2")
                {
                    item.IsModified = true;
                    CalculateData();
                    CalculateTotal();
                }
                OnPropertyChanged(nameof(IsSaveData));
            }
        }

        private void CalculateData()
        {
            Items.Where(x => x.IsHangCha).Select(x => { x.GiaTriThamTra = 0; x.GiaTriQuyetToan = 0; return x; }).ToList();
            foreach (var item in Items.Where(x => !x.IsHangCha && !x.IsDeleted && (x.GiaTriThamTra != 0 || x.GiaTriQuyetToan != 0)))
            {
                CalculateParent(item, item);
            }
        }

        private void CalculateParent(PheDuyetQuyetToanProcessModel currentItem, PheDuyetQuyetToanProcessModel selfItem)
        {
            PheDuyetQuyetToanProcessModel parentItem;
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
            parentItem.GiaTriThamTra += selfItem.GiaTriThamTra;
            parentItem.GiaTriQuyetToan += selfItem.GiaTriQuyetToan;
            CalculateParent(parentItem, selfItem);
        }

        public void CheckHangCha()
        {
            foreach (PheDuyetQuyetToanProcessModel item in Items)
            {
                if (item.IsChiPhi)
                {
                    PheDuyetQuyetToanProcessModel child = Items.Where(n => n.IdChiPhiDuAnParent == item.ChiPhiId).FirstOrDefault();
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
                        foreach (PheDuyetQuyetToanProcessModel hangMucChild in item.ListHangMuc)
                        {
                            PheDuyetQuyetToanProcessModel checkItem = Items.Where(n => n.HangMucId == hangMucChild.HangMucId).FirstOrDefault();
                            if (checkItem != null)
                            {
                                item.IsHangCha = true;
                            }
                        }
                    }
                }
                else
                {
                    PheDuyetQuyetToanProcessModel child = Items.Where(n => n.IdHangMucParent == item.HangMucId && item.HangMucId != Guid.Empty).FirstOrDefault();
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

        protected override void OnRefresh(object obj)
        {
            LoadData();
        }

        public void OnSaveData()
        {
            _qtQuyetToanChiTietService.DeleteByQuyetToanId(Model.Id);
            List<VdtQtQuyetToanChiTiet> entitys = new List<VdtQtQuyetToanChiTiet>();
            foreach (PheDuyetQuyetToanProcessModel item in Items.Where(n => !n.IsDeleted))
            {
                entitys.Add(new VdtQtQuyetToanChiTiet
                {
                    IIdQuyetToanId = Model.Id,
                    IIdChiPhiId = item.IsChiPhi ? item.ChiPhiId : Guid.Empty,
                    IIdHangMucId = !item.IsChiPhi ? item.HangMucId : Guid.Empty,
                    FGiaTriThamTra = item.GiaTriThamTra,
                    FGiaTriQuyetToan = item.GiaTriQuyetToan,
                    SMaOrder = item.MaOrderDb,
                    SUserCreate = _sessionService.Current.Principal,
                    DDateCreate = DateTime.Now,
                    IIdGoiThauId = item.GoiThauId
                });
            }
            _qtQuyetToanChiTietService.AddRange(entitys);
            _qtQuyetToanChiTietService.UpdateTotal(Model.Id.ToString());
            System.Windows.Forms.MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            LoadData();
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

        public override void Init()
        {
            try
            {
                LoadData();
                OnPropertyChanged(nameof(Items));
                OnPropertyChanged(nameof(IsSaveData));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
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
