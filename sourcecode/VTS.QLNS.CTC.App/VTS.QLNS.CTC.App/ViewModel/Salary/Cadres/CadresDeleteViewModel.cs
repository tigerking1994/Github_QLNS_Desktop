using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Core.Service.Impl;
using Aspose.Cells.Charts;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.Cadres
{
    public class CadresDeleteViewModel : DialogViewModelBase<List<CadresModel>>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ITlCanBoPhuCapService _tlCanBoPhuCapService;
        private readonly ITlDmCanBoService _cadresService;
        private readonly INsQsMucLucService _nsQsMucLucService;
        private readonly ITlDmCanBoService _tlDmCanBoService;
        private readonly ITlCanBoCheDoBHXHService _tlCanBoCheDoBHXHService;

        public override string FuncCode => NSFunctionCode.SALARY_QUAN_LY_LUONG_KE_HOACH_DANH_SACH_DOI_TUONG_HUONG_LUONG_KE_HOACH_DELETE;

        public override string Title => "Xóa đối tượng/cập nhật mã tăng giảm";
        public override Type ContentType => typeof(View.Salary.Cadres.CadresDelete);

        private string _deleteCadres;
        public string DeleteCadres
        {
            get => _deleteCadres;
            set => SetProperty(ref _deleteCadres, value);
        }

        private FormViewState _viewState;
        public FormViewState ViewState
        {
            get => _viewState;
            set
            {
                SetProperty(ref _viewState, value);
                OnPropertyChanged(nameof(IsReadOnly));
            }
        }

        private List<QsMucLucModel> _mucLucItems;
        public List<QsMucLucModel> MucLucItems
        {
            get => _mucLucItems;
            set => SetProperty(ref _mucLucItems, value);
        }

        private QsMucLucModel _mucLucSelected;
        public QsMucLucModel MucLucSelected
        {
            get => _mucLucSelected;
            set => SetProperty(ref _mucLucSelected, value);
        }

        private Visibility _text;
        public Visibility Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        private Visibility _maTangGiam;
        public Visibility MaTangGiam
        {
            get => _maTangGiam;
            set => SetProperty(ref _maTangGiam, value);
        }

        private Visibility _delete;
        public Visibility Delete
        {
            get => _delete;
            set => SetProperty(ref _delete, value);
        }

        private Visibility _updateMaTang;
        public Visibility UpdateMaTang
        {
            get => _updateMaTang;
            set => SetProperty(ref _updateMaTang, value);
        }

        private Visibility _save;
        public Visibility Save
        {
            get => _save;
            set => SetProperty(ref _save, value);
        }

        public bool IsReadOnly => ViewState == FormViewState.ADD;

        public RelayCommand OnDeleteCommand { get; }
        public RelayCommand OnOpenDialogMaTangGiamCommand { get; }
        public RelayCommand OnOpenUpdateMaTangGiamCommand { get; }

        public string ComboboxDisplayMemberPathMucLuc => nameof(MucLucSelected.SMoTa);

        public CadresDeleteViewModel(
            ISessionService sessionService,
            IMapper mapper,
            ILog logger,
            ITlCanBoPhuCapService tlCanBoPhuCapService,
            ITlDmCanBoService cadresService,
            INsQsMucLucService nsQsMucLucService,
            ITlDmCanBoService tlDmCanBoService,
            ITlCanBoCheDoBHXHService tlCanBoCheDoBHXHService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;
            _cadresService = cadresService;
            _tlCanBoPhuCapService = tlCanBoPhuCapService;
            _nsQsMucLucService = nsQsMucLucService;
            _tlDmCanBoService = tlDmCanBoService;
            _tlCanBoCheDoBHXHService = tlCanBoCheDoBHXHService;

            OnDeleteCommand = new RelayCommand(obj => OnDelete());
            OnOpenDialogMaTangGiamCommand = new RelayCommand(obj => OnUpdateMaTangGiam());
            OnOpenUpdateMaTangGiamCommand = new RelayCommand(obj => OnOpenUpdateMaTangGiam());
        }

        public override void Init()
        {
            base.Init();
            LoadText();
            LoadData();
        }

        private void LoadText()
        {

            _deleteCadres = Resources.MsgChocieDelete;
            if (Model[0].ITrangThai != 3)
            {
                _text = Visibility.Visible;
                _maTangGiam = Visibility.Collapsed;
                _delete = Visibility.Visible;
                _updateMaTang = Visibility.Visible;
                _save = Visibility.Collapsed;
            }
            else
            {
                _text = Visibility.Visible;
                _maTangGiam = Visibility.Collapsed;
                _delete = Visibility.Visible;
                _updateMaTang = Visibility.Collapsed;
                _save = Visibility.Collapsed;
            }

            /* OnPropertyChanged(nameof(Save));
             OnPropertyChanged(nameof(Delete));
             OnPropertyChanged(nameof(UpdateMaTang));*/
        }

        public void OnDelete()
        {
            MessageBoxResult dialogResult = MessageBoxHelper.Confirm(Resources.ConfirmPermanentDeleteCadres);
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                if (dialogResult == MessageBoxResult.Yes)
                {
                    var lstMaCanBo = Model.Select(x => x.MaCanBo).ToList();
                    foreach (var cb in lstMaCanBo)
                    {
                        var lstCanBoCheDoBHXH = _tlCanBoCheDoBHXHService.FindByMaCanBo(cb).ToList();
                        //Xóa chế độ BHXH theo cán bộ
                        _tlCanBoCheDoBHXHService.RemoveRange(_mapper.Map<List<TlCanBoCheDoBHXH>>(lstCanBoCheDoBHXH));
                    }
                    string maCanBoXoa = string.Join(",", lstMaCanBo);
                    _tlCanBoPhuCapService.DeleteCanBo(maCanBoXoa);
                    
                }
            }, (s, e) =>
            {
                if (dialogResult == MessageBoxResult.No)
                {
                    return;
                }
                if (e.Error == null)
                {
                    MessageBoxHelper.Info("Xóa cán bộ thành công");
                    SavedAction?.Invoke(null);
                    DialogHost.Close("RootDialog");
                }
                else
                {
                    _logger.Error(e.Error.Message, e.Error);
                }
                IsLoading = false;
            });
            
        }

        private void LoadData()
        {
            var predicate = PredicateBuilder.True<NsQsMucLuc>();
            predicate = predicate.And(x => x.INamLamViec == Model[0].Nam);
            predicate = predicate.And(x => x.SM.Equals("3"));
            predicate = predicate.And(x => !string.IsNullOrEmpty(x.STm));

            var QsMuLuc = _nsQsMucLucService.FindAll(predicate);
            MucLucItems = _mapper.Map<ObservableCollection<QsMucLucModel>>(QsMuLuc).ToList();
        }

        private void OnUpdateMaTangGiam()
        {
            MessageBoxResult dialogResult = MessageBoxHelper.Confirm(Resources.ConfirmUpdateTangGiam);
            if (dialogResult == MessageBoxResult.Yes)
            {
                Model.Select(x =>
                {
                    x.IsDelete = false;
                    x.MaTangGiamCu = x.MaTangGiam;
                    x.MaTangGiam = MucLucSelected.SKyHieu;
                    x.ITrangThai = 3;
                    return x;
                }).ToList();
                var listEdit = _mapper.Map<ObservableCollection<TlDmCanBo>>(Model).ToList();
                foreach (var item in listEdit)
                {
                    _tlDmCanBoService.Update(item);
                }
                //_tlDmCanBoService.UpdateRange(listEdit);
            }
            SavedAction?.Invoke(null);
            DialogHost.Close("RootDialog");
        }

        private void OnOpenUpdateMaTangGiam()
        {
            _text = Visibility.Collapsed;
            _maTangGiam = Visibility.Visible;
            _delete = Visibility.Collapsed;
            _updateMaTang = Visibility.Collapsed;
            _save = Visibility.Visible;
            var predicate = PredicateBuilder.True<NsQsMucLuc>();
            predicate = predicate.And(x => x.INamLamViec == Model[0].Nam);
            predicate = predicate.And(x => x.SM.Equals("3"));
            predicate = predicate.And(x => !string.IsNullOrEmpty(x.STm));
            predicate = predicate.And(x => !x.SM.Equals("380"));
            predicate = predicate.And(x => !x.SM.Equals("350"));
            var NsQs = _nsQsMucLucService.FindAll(predicate);
            MucLucItems = _mapper.Map<ObservableCollection<QsMucLucModel>>(NsQs).ToList();

            OnPropertyChanged(nameof(Text));
            OnPropertyChanged(nameof(MaTangGiam));
            OnPropertyChanged(nameof(Save));
            OnPropertyChanged(nameof(Delete));
            OnPropertyChanged(nameof(UpdateMaTang));
        }
    }
}
