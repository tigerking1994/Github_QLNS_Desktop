using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Service;
using System.Collections.ObjectModel;
using System.Windows.Data;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using System.Windows;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanChi
{
    public class PhanBoDuToanChiTietChiKPQLViewModel : DetailViewModelBase<BhPbdtcBHXHChiTietModel, BhDtCtctKPQLModel>
    {
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly IBhDtCtctKPQLService _bhDtCtctKPQLService;
        private readonly ILog _logger;
        private SessionInfo _sessionInfo;
        private readonly IMapper _mapper;
        private ICollectionView _dtCtctKPQLModelView { get; set; }

        private BhDtctgBHXHModel _bhDtctgBHXHModel;
        public BhDtctgBHXHModel BhDtctgBHXHModel
        {
            get => _bhDtctgBHXHModel;
            set => SetProperty(ref _bhDtctgBHXHModel, value);
        }
        private BhPbdtcBHXHModel _bhPbdtcBHXHModel;
        public BhPbdtcBHXHModel BhPbdtcBHXHModel
        {
            get => _bhPbdtcBHXHModel;
            set => SetProperty(ref _bhPbdtcBHXHModel, value);
        }
        private BhDtctgBHXHChiTietModel _bhDtctgBHXHChiTietModel;
        public BhDtctgBHXHChiTietModel BhDtctgBHXHChiTietModel
        {
            get => _bhDtctgBHXHChiTietModel;
            set => SetProperty(ref _bhDtctgBHXHChiTietModel, value);
        }

        private bool _isSaveData;
        public bool IsSaveData
        {
            get => _isSaveData;
            set => SetProperty(ref _isSaveData, value);
        }

        public Guid? IIDChungTuChiTiet { get; set; }

        public bool IsInit { get; set; }
        public PhanBoDuToanChiTietChiKPQLViewModel(
              ISessionService sessionService,
            INsDonViService nsDonViService,
            ILog log,
            IMapper mapper,
            IBhDtCtctKPQLService bhDtCtctKPQLService)
        {
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _logger = log;
            _mapper = mapper;
            _bhDtCtctKPQLService = bhDtCtctKPQLService;
        }


        public override void Init()
        {
            IsInit = true;
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            IsInit = true;
            base.LoadData(args);
            var yearOfWork = _sessionInfo.YearOfWork;
            var lstDonVi = BhPbdtcBHXHModel.SID_MaDonVi.Split(",");
            List<BhDtCtctKPQLQuery> lstData = new List<BhDtCtctKPQLQuery>();
            foreach (var sMaDonVi in lstDonVi)
            {
                var lstDataChungTuChiTiet = _bhDtCtctKPQLService.FindPhanBoDuToanTrenGiaoKPQL(BhPbdtcBHXHModel.Id, sMaDonVi, yearOfWork);
                var lstDataQuery = _bhDtCtctKPQLService.FindIndex(yearOfWork, IIDChungTuChiTiet, BhPbdtcBHXHModel.Id, sMaDonVi).ToList();
                lstDataQuery.InsertRange(0, lstDataChungTuChiTiet);
                lstData.AddRange(lstDataQuery);
            }

            Items = _mapper.Map<ObservableCollection<BhDtCtctKPQLModel>>(lstData);
            _dtCtctKPQLModelView = CollectionViewSource.GetDefaultView(Items);
            foreach (var item in Items)
            {
                if (!item.BHangCha)
                {
                    item.PropertyChanged += (sender, args) =>
                    {
                        BhDtCtctKPQLModel chungTu = (BhDtCtctKPQLModel)sender;
                        item.IsModified = true;
                        IsSaveData = true;
                        OnPropertyChanged(nameof(IsSaveData));
                        CalculateData(chungTu.IIDMaDonVi);
                    };
                }
            }

            CalculateData();
        }

        private void CalculateData(string sMaDonVi = null)
        {
            if (IsInit)
            {
                var lstMaDonVi = BhPbdtcBHXHModel.SID_MaDonVi.Split(",");
                foreach (var maDonVi in lstMaDonVi)
                {
                    Items.Where(x => x.BHangCha && !x.IsRemainRow && x.IIDMaDonVi == maDonVi)
                         .ForAll(x =>
                         {
                             x.FSoTien = 0;
                         });

                    var dictByMlns = Items.Where(x => !x.IsRemainRow && x.IIDMaDonVi == maDonVi).GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
                    List<BhDtCtctKPQLModel> temp = Items.Where(x => !x.BHangCha && !x.IsRemainRow && x.IIDMaDonVi == maDonVi).ToList();
                    foreach (var item in temp)
                    {

                        CalculateParent(item.IID_MLNS_Cha, item, dictByMlns);
                    }
                }

                IsInit = false;
            }
            else
            {
                Items.Where(x => x.BHangCha && !x.IsRemainRow && x.IIDMaDonVi == sMaDonVi)
                     .ForAll(x =>
                     {
                         x.FSoTien = 0;
                     });

                var dictByMlns = Items.Where(x => !x.IsRemainRow && x.IIDMaDonVi == sMaDonVi).GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
                List<BhDtCtctKPQLModel> temp = Items.Where(x => !x.BHangCha && !x.IsRemainRow && x.IIDMaDonVi == sMaDonVi).ToList();
                foreach (var item in temp)
                {

                    CalculateParent(item.IID_MLNS_Cha, item, dictByMlns);
                }
            }
        }

        private void CalculateParent(Guid? idParent, BhDtCtctKPQLModel item, Dictionary<Guid?, BhDtCtctKPQLModel> dictByMlns)
        {

            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            model.FSoTien = model.FSoTien.GetValueOrDefault(0) + item.FSoTien.GetValueOrDefault(0);

            CalculateParent(model.IID_MLNS_Cha, item, dictByMlns);
        }

        public override void OnSave()
        {
            var lstDuToanPhanBo = Items.Where(x => x.IsRemainRow).ToList();
            StringBuilder sbTenDonVi = new StringBuilder();
            foreach (var item in lstDuToanPhanBo)
            {
                var fSoTienNhanPhanBo = Items.Where(x => x.IsRemainRow && x.IIDMaDonVi == item.IIDMaDonVi).Select(x => x.FSoTien).FirstOrDefault();
                var fSoTienDuToanKPQL = Items.Where(x => !x.BHangCha && x.IIDMaDonVi == item.IIDMaDonVi).Select(x => x.FSoTien).Sum();

                if (fSoTienNhanPhanBo < fSoTienDuToanKPQL)
                {
                    sbTenDonVi.AppendFormat(" {0}", item.STenDonVi + ",");

                }
            }

            if (!string.IsNullOrEmpty(sbTenDonVi.ToString()))
            {
                sbTenDonVi.Remove(0, 1);
                sbTenDonVi.Remove(sbTenDonVi.Length - 1, 1);
                MessageBox.Show(string.Format("Số tiền phân bổ chi tiết đơn vị: {0}  không được vượt quá dự toán chi KPQL được giao", sbTenDonVi.ToString()), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var lstDetaillAdd = Items.Where(x => x.IsModified && !x.IsHangCha && x.Id == Guid.Empty).ToList();
            var lstDetaillUpdate = Items.Where(x => x.IsModified && !x.IsHangCha && x.Id != Guid.Empty && x.FSoTien.GetValueOrDefault(0) != 0).ToList();
            var lstDetaillDelete = Items.Where(x => x.IsModified && !x.IsHangCha && x.Id != Guid.Empty && x.FSoTien == 0).ToList();
            var addItemList = new List<BhDtCtctKPQL>();
            if (lstDetaillAdd.Count() > 0)
            {
                _mapper.Map(lstDetaillAdd, addItemList);
                addItemList.Select(x =>
                {
                    x.Id = Guid.NewGuid();
                    x.IIDChungTu = BhPbdtcBHXHModel.Id;
                    x.DNgayTao = DateTime.Now;
                    x.SNguoiTao = _sessionInfo.Principal;
                    return x;
                }).ToList();

                _bhDtCtctKPQLService.AddRange(addItemList);
                Items.Where(x => !x.IsHangCha && x.IsModified).Select(x => { x.IsModified = false; x.IsAdded = false; return x; }).ToList();
            }

            if (lstDetaillUpdate.Count() > 0)
            {
                _mapper.Map(lstDetaillUpdate, addItemList);
                addItemList.Select(x =>
                {
                    x.DNgaySua = DateTime.Now;
                    x.SNguoiSua = _sessionInfo.Principal;
                    return x;
                }).ToList();
                foreach (var item in addItemList)
                {
                    _bhDtCtctKPQLService.Update(item);
                }

                Items.Where(x => !x.IsHangCha && x.IsModified).Select(x => { x.IsModified = false; x.IsAdded = false; return x; }).ToList();
            }

            if (lstDetaillDelete.Count() > 0)
            {
                foreach (var item in lstDetaillDelete)
                {
                    _bhDtCtctKPQLService.Delete(item.Id);
                }
            }

            MessageBoxHelper.Info(Resources.MsgSaveDone);
            IsSaveData = false;
            OnPropertyChanged(nameof(IsSaveData));
            this.LoadData();

        }
    }
}
