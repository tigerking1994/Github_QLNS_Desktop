using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.NhanDuToanChiTrenGiao
{
    public class NhanDuToanChiTietChiKPQLViewModel : DetailViewModelBase<BhDtctgBHXHChiTietModel, BhDtCtctKPQLModel>
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
        public NhanDuToanChiTietChiKPQLViewModel(
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
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            var yearOfWork = _sessionInfo.YearOfWork;
            var lstDataChungTuChiTiet = _bhDtCtctKPQLService.FindDuToanTrenGiaoKPQL(BhDtctgBHXHModel.Id, BhDtctgBHXHModel.IID_MaDonVi, yearOfWork);
            var lstData = _bhDtCtctKPQLService.FindIndex(yearOfWork, IIDChungTuChiTiet, BhDtctgBHXHModel.Id, BhDtctgBHXHModel.IID_MaDonVi).ToList();
            lstData.InsertRange(0, lstDataChungTuChiTiet);
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
                        CalculateData();
                    };
                }
            }

            CalculateData();
        }

        private void CalculateData()
        {

            Items.Where(x => x.BHangCha && !x.IsRemainRow)
                       .ForAll(x =>
                       {
                           x.FSoTien = 0;
                       });

            var dictByMlns = Items.Where(x => !x.IsRemainRow).GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
            var temp = Items.Where(x => !x.BHangCha && !x.IsRemainRow).ToList();
            foreach (var item in temp)
            {

                CalculateParent(item.IID_MLNS_Cha, item, dictByMlns);
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
            var fSoTienNhanPhanBo = Items.Where(x => x.IsRemainRow).Select(x => x.FSoTien).FirstOrDefault();
            var fSoTienDuToanKPQL = Items.Where(x => !x.BHangCha).Select(x => x.FSoTien).Sum();
            if (fSoTienNhanPhanBo < fSoTienDuToanKPQL)
            {
                MessageBox.Show("Số tiền phân bổ chi tiết không được vượt quá dự toán chi KPQL được giao", Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
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
                    x.IIDChungTu = BhDtctgBHXHModel.Id;
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
        public override void OnCancel()
        {
            base.OnCancel();
        }
    }
}
