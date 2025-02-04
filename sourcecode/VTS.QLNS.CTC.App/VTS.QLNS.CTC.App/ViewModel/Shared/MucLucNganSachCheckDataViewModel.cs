using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Shared;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Shared
{
    public class MucLucNganSachCheckDataViewModel : DialogViewModelBase<NsMuclucNgansachModel>
    {
        private readonly ISktChungTuService _sktChungTuService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly IMucLucNganSachService _iMucLucNganSachService;
        private readonly INsDtChungTuService _chungTuDuToanService;
        private readonly INsDtChungTuChiTietService _chungTuDuToanChiTietService;
        private readonly INsQtChungTuChiTietService _chungTuQuyetToanChiTietService;
        private readonly ILbChungTuService _chungTuPhanCapNganhService;
        private readonly ILbChungTuChiTietService _chungTuPhanCapNganhChiTietService;
        private readonly INsQtChungTuService _chungTuQuyetToanService;
        private readonly ICpChungTuService _chungTuCapPhatService;
        private readonly ICpChungTuChiTietService _chungTuCapPhatChiTietService;
        private readonly ISktSoLieuChungTuService _sktChungTuDuToanDauNamService;
        private readonly ISysAuditLogService _log;
        private readonly IMapper _mapper;
        private readonly ICollectionView _nsDonViModelsView;
        private SessionInfo _sessionInfo;

        public override Type ContentType => typeof(MucLucNganSachCheckDataView);

        private IEnumerable<MucLucNganSachCheckDataModel> _dataDuToan;
        public IEnumerable<MucLucNganSachCheckDataModel> DataDuToan
        {
            get => _dataDuToan;
            set => SetProperty(ref _dataDuToan, value);
        }

        private IEnumerable<MucLucNganSachCheckDataModel> _dataQuyetToan;
        public IEnumerable<MucLucNganSachCheckDataModel> DataQuyetToan
        {
            get => _dataQuyetToan;
            set => SetProperty(ref _dataQuyetToan, value);
        }

        private IEnumerable<MucLucNganSachCheckDataModel> _dataCapPhat;
        public IEnumerable<MucLucNganSachCheckDataModel> DataCapPhat
        {
            get => _dataCapPhat;
            set => SetProperty(ref _dataCapPhat, value);
        }

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler DeletedActionHandler;

        public string CodeChain { get; set; }
        public string CodeChainDuToan { get; set; }
        public string CodeChainQuyetToan { get; set; }
        public string CodeChainCapPhat { get; set; }

        public bool IsCheckedDuToan { get; set; } = true;
        public bool IsCheckedQuyetToan { get; set; }
        public bool IsCheckedCapPhat { get; set; }

        public Command.RelayCommand DeleteCommand { get; }
        public Command.RelayCommand DeleteAllCommand { get; }


        public MucLucNganSachCheckDataViewModel(INsDonViService nsDonViService,
            IMapper mapper,
            ISessionService sessionService,
            IMucLucNganSachService iMucLucNganSachService,
            INsDtChungTuService chungTuDuToanService,
            INsDtChungTuChiTietService chungTuDuToanChiTietService,
            INsQtChungTuChiTietService chungTuQuyetToanChiTietService,
            ILbChungTuService chungTuPhanCapNganhService,
            ILbChungTuChiTietService chungTuPhanCapNganhChiTietService,
            INsQtChungTuService chungTuQuyetToanService,
            ISktSoLieuChungTuService sktChungTuDuToanDauNamService,
            ICpChungTuService chungTuCapPhatService,
            ICpChungTuChiTietService chungTuCapPhatChiTietService,
            ISysAuditLogService log)
        {
            _sessionService = sessionService;
            _iMucLucNganSachService = iMucLucNganSachService;
            _nsDonViService = nsDonViService;
            _log = log;
            _mapper = mapper;
            _chungTuDuToanService = chungTuDuToanService;
            _chungTuDuToanChiTietService = chungTuDuToanChiTietService;
            _chungTuQuyetToanChiTietService = chungTuQuyetToanChiTietService;
            _chungTuPhanCapNganhChiTietService = chungTuPhanCapNganhChiTietService;
            _chungTuQuyetToanService = chungTuQuyetToanService;
            _chungTuPhanCapNganhService = chungTuPhanCapNganhService;
            _sktChungTuDuToanDauNamService = sktChungTuDuToanDauNamService;
            _chungTuCapPhatChiTietService = chungTuCapPhatChiTietService;
            _chungTuCapPhatService = chungTuCapPhatService;

            DeleteCommand = new Command.RelayCommand(obj => DeleteData());
            DeleteAllCommand = new Command.RelayCommand(obj => DeleteAllData());
        }

        public override void Init()
        {
            IsCheckedDuToan = true;
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadData();
        }

        public bool? IsAllItemsSelected
        {
            get
            {
                if (IsCheckedDuToan)
                {
                    if (DataDuToan != null)
                    {
                        var selected = DataDuToan.Select(item => item.Selected).Distinct().ToList();
                        return selected.Count == 1 ? selected.Single() : (bool?)null;
                    }
                    return false;
                }
                else if (IsCheckedQuyetToan)
                {
                    if (DataQuyetToan != null)
                    {
                        var selected = DataQuyetToan.Select(item => item.Selected).Distinct().ToList();
                        return selected.Count == 1 ? selected.Single() : (bool?)null;
                    }
                    return false;
                }
                else
                {
                    if (DataCapPhat != null)
                    {
                        var selected = DataCapPhat.Select(item => item.Selected).Distinct().ToList();
                        return selected.Count == 1 ? selected.Single() : (bool?)null;
                    }
                    return false;
                }

            }
            set
            {
                if (value.HasValue)
                {
                    if (IsCheckedDuToan)
                    {
                        SelectAll(value.Value, DataDuToan);
                    }
                    else if (IsCheckedQuyetToan)
                    {
                        SelectAll(value.Value, DataQuyetToan);
                    }
                    else
                    {
                        SelectAll(value.Value, DataCapPhat);
                    }

                    OnPropertyChanged();
                }
            }
        }

        void DeleteByType(string codeChain, string type, Guid uniqueidentifier)
        {
            _iMucLucNganSachService.DeleteHasDataMLNS(codeChain, type, uniqueidentifier);
        }

        public void DeleteAllData()
        {
            var dataDuToanHasChecked = DataDuToan.ToList();
            var dataQuyetToanHasChecked = DataQuyetToan.ToList();
            var dataCapPhatHasChecked = DataCapPhat.ToList();

            if (!dataDuToanHasChecked.Any() && !dataQuyetToanHasChecked.Any() && !dataCapPhatHasChecked.Any())
            {
                Helper.MessageBoxHelper.Warning(Resources.AlertVoucherEmpty);
                return;
            }
            MessageBoxResult messageValidate = Helper.MessageBoxHelper.Confirm(Resources.MsgConfirmDeleteAllDetail);
            if (messageValidate.Equals(MessageBoxResult.Yes))
            {
                try
                {
                    dataDuToanHasChecked.ForEach(item =>
                    {
                        DeleteByType(CodeChainDuToan, item.Loai, item.ID);
                        UpdateChungTu(item.ID_Parent, item.Loai);
                    });
                    dataQuyetToanHasChecked.ForEach(item =>
                    {
                        DeleteByType(CodeChainQuyetToan, item.Loai, item.ID);
                        UpdateChungTu(item.ID_Parent, item.Loai);
                    });
                    dataCapPhatHasChecked.ForEach(item =>
                    {
                        DeleteByType(CodeChainCapPhat, item.Loai, item.ID);
                        UpdateChungTu(item.ID_Parent, item.Loai);
                    });

                    DeletedActionHandler?.Invoke(Model, new EventArgs());

                    DialogHost.Close(SystemConstants.ROOT_DIALOG);

                    Helper.MessageBoxHelper.Info(Resources.MsgDeleteDone, Resources.NotifiTitle);

                    //System.Windows.MessageBox.Show(Resources.MsgDeleteDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception)
                {
                    DialogHost.Close(SystemConstants.ROOT_DIALOG);
                    Helper.MessageBoxHelper.Error(Resources.MsgDeleteError, Resources.NotifiTitle);
                }
            }

        }

        public void DeleteData()
        {
            var dataDuToanHasChecked = DataDuToan.Where(n => n.Selected).ToList();
            var dataQuyetToanHasChecked = DataQuyetToan.Where(n => n.Selected).ToList();
            var dataCapPhatHasChecked = DataCapPhat.Where(n => n.Selected).ToList();

            if (!dataDuToanHasChecked.Any() && !dataQuyetToanHasChecked.Any() && !dataCapPhatHasChecked.Any())
            {
                Helper.MessageBoxHelper.Warning(Resources.AlertVoucherEmpty);
                return;
            }
            MessageBoxResult messageValidate = Helper.MessageBoxHelper.Confirm(Resources.MsgConfirmDeleteDetail);
            if (messageValidate.Equals(MessageBoxResult.Yes))
            {
                try
                {
                    dataDuToanHasChecked.ForEach(item =>
                    {
                        DeleteByType(CodeChainDuToan, item.Loai, item.ID);
                        UpdateChungTu(item.ID_Parent, item.Loai);
                    });
                    dataQuyetToanHasChecked.ForEach(item =>
                    {
                        DeleteByType(CodeChainQuyetToan, item.Loai, item.ID);
                        UpdateChungTu(item.ID_Parent, item.Loai);
                    });
                    dataCapPhatHasChecked.ForEach(item =>
                    {
                        DeleteByType(CodeChainCapPhat, item.Loai, item.ID);
                        UpdateChungTu(item.ID_Parent, item.Loai);
                    });

                    DeletedActionHandler?.Invoke(Model, new EventArgs());

                    DialogHost.Close(SystemConstants.ROOT_DIALOG);

                    Helper.MessageBoxHelper.Info(Resources.MsgDeleteDone, Resources.NotifiTitle);

                    //System.Windows.MessageBox.Show(Resources.MsgDeleteDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception)
                {
                    DialogHost.Close(SystemConstants.ROOT_DIALOG);
                    Helper.MessageBoxHelper.Error(Resources.MsgDeleteError, Resources.NotifiTitle);
                }

            }

        }

        private void UpdateChungTu(Guid id, string loai)
        {
            if (loai.Equals(LoaiModuleMLNS.NHAN_DU_TOAN) || loai.Equals(LoaiModuleMLNS.PHAN_BO_DU_TOAN))
            {
                UpdateChungTuDuToan(id);
            }
            else if (loai.Equals(LoaiModuleMLNS.DU_TOAN_DAU_NAM))
            {
                UpdateChungTuLapDuToanDauNam(id);
            }
            else if (loai.Equals(LoaiModuleMLNS.QUYET_TOAN))
            {
                UpdateChungTuQuyetToan(id);
            }
            else if (loai.Equals(LoaiModuleMLNS.PHAN_CAP_NGAN_SACH_NGANH))
            {
                UpdateChungTuPhanCapNganh(id);
            }
            else if (loai.Equals(LoaiModuleMLNS.CAP_PHAT))
            {
                UpdateChungTuCapPhat(id);
            }
        }

        private void UpdateChungTuDuToan(Guid id)
        {
            NsDtChungTu chungTu = _chungTuDuToanService.FindById(id);
            List<NsDtChungTuChiTiet> chungTuChiTiet = _chungTuDuToanChiTietService.FindByIdChungTu(id.ToString()).ToList();

            var childs = chungTuChiTiet.Where(x => !x.BHangCha && (x.FTuChi != 0 || x.FHienVat != 0 || x.FHangNhap != 0 || x.FHangMua != 0 ||
                                    x.FPhanCap != 0 || x.FDuPhong != 0)).ToList();

            chungTu.FTongTuChi = childs.Sum(x => x.FTuChi);
            chungTu.FTongHienVat = childs.Sum(x => x.FHienVat);
            chungTu.FTongHangNhap = childs.Sum(x => x.FHangNhap);
            chungTu.FTongHangMua = childs.Sum(x => x.FHangMua);
            chungTu.FTongPhanCap = childs.Sum(x => x.FPhanCap);
            chungTu.FTongDuPhong = childs.Sum(x => x.FDuPhong);

            _chungTuDuToanService.Update(chungTu);
        }

        private void UpdateChungTuPhanCapNganh(Guid id)
        {
            var chungTu = _chungTuPhanCapNganhService.FindById(id);
            if (chungTu != null)
            {
                _chungTuPhanCapNganhService.UpdateTotalLbChungTu(chungTu.Id.ToString(), _sessionService.Current.Principal);
            }
        }

        private void UpdateChungTuCapPhat(Guid id)
        {
            NsCpChungTu chungTu = _chungTuCapPhatService.FindById(id);
            var predicate = PredicateBuilder.True<NsCpChungTuChiTiet>();

            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
            predicate = predicate.And(x => x.IIdCtcapPhat.HasValue && x.IIdCtcapPhat == chungTu.Id);
            var childs = _chungTuCapPhatChiTietService.FindByCondition(predicate);

            chungTu.FTongTuChi = (double)childs.Sum(x => x.FTuChi);
            chungTu.FTongHienVat = (double)childs.Sum(x => x.FHienVat);

            _chungTuCapPhatService.Update(chungTu);
        }

        private void UpdateChungTuQuyetToan(Guid id)
        {
            NsQtChungTu chungTu = _chungTuQuyetToanService.FindById(id);

            var searchCondition = new SettlementVoucherDetailSearch
            {
                VoucherId = chungTu.Id,
                LNS = string.Join(",", chungTu.SDslns),
                YearOfWork = _sessionInfo.YearOfWork,
                YearOfBudget = _sessionInfo.YearOfBudget,
                Type = chungTu.SLoai,
                BudgetSource = _sessionInfo.Budget,
                AgencyId = chungTu.IIdMaDonVi,
                VoucherDate = chungTu.DNgayChungTu.Value,
                UserName = _sessionInfo.Principal
            };

            var childs = _chungTuQuyetToanChiTietService.FindByCondition(searchCondition);

            chungTu.FTongTuChiDeNghi = (double)childs.Sum(x => x.FTuChiDeNghi);
            chungTu.FTongTuChiPheDuyet = (double)childs.Sum(x => x.FTuChiPheDuyet);

            _chungTuQuyetToanService.Update(chungTu);
        }

        private void UpdateChungTuLapDuToanDauNam(Guid id)
        {
            var chungTu = _sktChungTuDuToanDauNamService.Find(id);

            List<DonVi> userAgency = _nsDonViService.FindAll(x => x.NamLamViec == _sessionInfo.YearOfWork).ToList();

            string loaiDonVi = userAgency.FirstOrDefault(n => n.IIDMaDonVi == chungTu.IIdMaDonVi).Loai;

            _sktChungTuDuToanDauNamService.UpdateTotalChungTu(chungTu.IIdMaDonVi, loaiDonVi, (int)chungTu.ILoaiChungTu,
            _sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget, _sessionService.Current.Budget, chungTu.ILoaiNguonNganSach ?? 0, chungTu.Id.ToString());
        }


        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            var dataDuToanQuery = _iMucLucNganSachService.FindHasDataMLNS(_sessionService.Current.YearOfWork, CodeChainDuToan, 0);
            DataDuToan = _mapper.Map<IEnumerable<MucLucNganSachCheckDataModel>>(dataDuToanQuery);
            DataDuToan.Select((n, i) =>
            {
                n.IRowIndex = i + 1;
                return n;
            }).ToList();
            var dataQuyetToanQuery = _iMucLucNganSachService.FindHasDataMLNS(_sessionService.Current.YearOfWork, CodeChainQuyetToan, 1);
            DataQuyetToan = _mapper.Map<IEnumerable<MucLucNganSachCheckDataModel>>(dataQuyetToanQuery);
            DataQuyetToan.Select((n, i) =>
            {
                n.IRowIndex = i + 1;
                return n;
            }).ToList();
            var dataCapPhatQuery = _iMucLucNganSachService.FindHasDataMLNS(_sessionService.Current.YearOfWork, CodeChainCapPhat, 2);
            DataCapPhat = _mapper.Map<IEnumerable<MucLucNganSachCheckDataModel>>(dataCapPhatQuery);
            DataCapPhat.Select((n, i) =>
            {
                n.IRowIndex = i + 1;
                return n;
            }).ToList();
        }


        public override void OnSave()
        {
            base.OnSave();
            DialogHost.CloseDialogCommand.Execute(null, null);
        }


        private void SelectAll(bool select, IEnumerable<MucLucNganSachCheckDataModel> models)
        {
            foreach (var model in models)
            {
                model.Selected = select;
            }
        }

    }
}