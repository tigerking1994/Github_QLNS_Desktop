using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Shared.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Shared.ImportViewModel
{
    public class SuggestMlnsViewModel : GridViewModelBase<NsMuclucNgansachModel>
    {
        private INsMucLucNganSachService _mucLucNganSachService;
        private IMapper _mapper;
        private ICollectionView _dataCollectionView;
        private ISessionService _sessionService;

        public override string Name => "Mục lục ngân sách";
        public override string Description => "Mục lục ngân sách";
        public override Type ContentType => typeof(SuggestMlns);
        public override PackIconKind IconKind => PackIconKind.User;

        public IEnumerable<NsMuclucNgansachModel> excludeMlns { get; set; }
        public NsMucLucNganSach InvalidMlns { get; set; }
        public NsMuclucNgansachModel FilterModel { get; set; }
        public Action<NsMuclucNgansachModel> SaveAction { get; set; }

        public SuggestMlnsViewModel(INsMucLucNganSachService mucLucNganSachService,
            IMapper mapper,
            ISessionService sessionService)
        {
            _mucLucNganSachService = mucLucNganSachService;
            _mapper = mapper;
            _sessionService = sessionService;
            excludeMlns = new List<NsMuclucNgansachModel>();
        }

        public override void Init()
        {
            IEnumerable<string> xauNoima = excludeMlns.Select(t => t.XNM);
            int namLamViec = _sessionService.Current.YearOfWork;
            string mlnsType = getTypeOfMlns(InvalidMlns);
            Func<NsMucLucNganSach, bool> filterByTypeFunc = getFilterFunction(mlnsType);
            Expression<Func<NsMucLucNganSach, bool>> filterByValue = PredicateBuilder.True<NsMucLucNganSach>();
            filterByValue = filterByValue.And(t => t.Lns.Equals(InvalidMlns.Lns));
            filterByValue = filterByValue.And(t => t.L.Equals(InvalidMlns.L));
            filterByValue = filterByValue.And(t => t.K.Equals(InvalidMlns.K));
            filterByValue = filterByValue.And(t => t.M.Equals(InvalidMlns.M));
            if (!string.IsNullOrEmpty(InvalidMlns.Tm))
            {
                filterByValue = filterByValue.And(t => CompareNonEmptyValue(t.Tm,InvalidMlns.Tm) || CompareNonEmptyValue(t.Ttm, InvalidMlns.Ttm) ||
                    CompareNonEmptyValue(t.Ng, InvalidMlns.Ng) || CompareNonEmptyValue(t.Tng, InvalidMlns.Tng) || CompareNonEmptyValue(t.Tng1, InvalidMlns.Tng1) 
                    || CompareNonEmptyValue(t.Tng2, InvalidMlns.Tng2) || CompareNonEmptyValue(t.Tng3, InvalidMlns.Tng3));
            }
            IEnumerable<NsMucLucNganSach> nsMucLucNganSaches = _mucLucNganSachService.FindAll(namLamViec).Where(filterByTypeFunc).Where(filterByValue.Compile());
            IEnumerable<NsMuclucNgansachModel> nsMuclucNgansachModels = _mapper.Map<IEnumerable<NsMuclucNgansachModel>>(nsMucLucNganSaches).Where(t => !xauNoima.Contains(t.XNM));
            Items = new ObservableCollection<NsMuclucNgansachModel>(nsMuclucNgansachModels);

            foreach (var item in Items)
            {
                item.IsValidTM = !string.IsNullOrEmpty(item.TM) && InvalidMlns.Tm.Equals(item.TM);
                item.IsValidTTM = !string.IsNullOrEmpty(item.TTM) && InvalidMlns.Ttm.Equals(item.TTM);
                item.IsValidNG = !string.IsNullOrEmpty(item.NG) && InvalidMlns.Ng.Equals(item.NG);
                item.IsValidTNG = !string.IsNullOrEmpty(item.TNG) && InvalidMlns.Tng.Equals(item.TNG);
                item.IsValidTNG1 = !string.IsNullOrEmpty(item.TNG1) && InvalidMlns.Tng1.Equals(item.TNG1);
                item.IsValidTNG2 = !string.IsNullOrEmpty(item.TNG2) && InvalidMlns.Tng2.Equals(item.TNG2);
                item.IsValidTNG3 = !string.IsNullOrEmpty(item.TNG3) && InvalidMlns.Tng3.Equals(item.TNG3);
            }
            _dataCollectionView = CollectionViewSource.GetDefaultView(Items);
            _dataCollectionView.Filter = ItemsViewFilter;
            foreach (var model in Items)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    NsMuclucNgansachModel nsMuclucNgansachModel = sender as NsMuclucNgansachModel;
                    bool select = nsMuclucNgansachModel.IsSelected;
                    if (args.PropertyName == nameof(NsMuclucNgansachModel.IsSelected) && select)
                    {
                        DeselectAllButSelectedItem(nsMuclucNgansachModel.Id);
                    }
                };
            }
            OnPropertyChanged(nameof(Items));
        }

        public override void OnSave()
        {
            var selectedItem = Items.First(t => t.IsSelected);
            if (selectedItem != null)
             SaveAction(selectedItem);
        }

        private bool ItemsViewFilter(object obj)
        {
            bool result = true;
            var item = (NsMuclucNgansachModel)obj;
            if (!string.IsNullOrEmpty(FilterModel.Lns))
                result = result && item.Lns.ToLower().Equals(FilterModel.Lns.ToLower());
            if (!string.IsNullOrEmpty(FilterModel.L))
                result = result && item.L.ToLower().Equals(FilterModel.L.ToLower());
            if (!string.IsNullOrEmpty(FilterModel.K))
                result = result && item.K.ToLower().Equals(FilterModel.K.ToLower());
            if (!string.IsNullOrEmpty(FilterModel.M))
                result = result && item.M.ToLower().Equals(FilterModel.M.ToLower());
            if (!string.IsNullOrEmpty(FilterModel.TM))
                result = result && item.TM.ToLower().Equals(FilterModel.TM.ToLower());
            if (!string.IsNullOrEmpty(FilterModel.TTM))
                result = result && item.TTM.ToLower().Equals(FilterModel.TTM.ToLower());
            if (!string.IsNullOrEmpty(FilterModel.NG))
                result = result && item.NG.ToLower().Equals(FilterModel.NG.ToLower());
            if (!string.IsNullOrEmpty(FilterModel.TNG))
                result = result && item.TNG.ToLower().Equals(FilterModel.TNG.ToLower());
            if (!string.IsNullOrEmpty(FilterModel.TNG1))
                result = result && item.TNG1.ToLower().Equals(FilterModel.TNG1.ToLower());
            if (!string.IsNullOrEmpty(FilterModel.TNG2))
                result = result && item.TNG2.ToLower().Equals(FilterModel.TNG2.ToLower());
            if (!string.IsNullOrEmpty(FilterModel.TNG3))
                result = result && item.TNG3.ToLower().Equals(FilterModel.TNG3.ToLower());
            return result;
        }

        private void DeselectAllButSelectedItem(Guid mlnsId)
        {
            foreach (var item in Items)
            {
                if (!mlnsId.Equals(item.Id))
                    item.IsSelected = false;
            }
        }

        private List<string> mlnsType = new List<string>
        {
            "Tng3", "Tng2", "Tng1", "Tng", "Ng", "Ttm", "Tm", "M", "K", "L", "Lns"
        };

        private string getTypeOfMlns(NsMucLucNganSach entity)
        {
            foreach (string type in mlnsType)
            {
                PropertyInfo propertyInfo = typeof(NsMucLucNganSach).GetProperty(type);
                object val = propertyInfo.GetValue(entity, null);
                if (val != null && !string.IsNullOrWhiteSpace(val.ToString()))
                {
                    return type;
                }
            }
            return "";
        }

        private Func<NsMucLucNganSach, bool> getFilterFunction(string mlnsType)
        {
            switch(mlnsType)
            {
                case "Tng3":
                    return t => !string.IsNullOrEmpty(t.Tng3);
                case "Tng2":
                    return t => !string.IsNullOrEmpty(t.Tng2) && string.IsNullOrEmpty(t.Tng3);
                case "Tng1":
                    return t => !string.IsNullOrEmpty(t.Tng1) && string.IsNullOrEmpty(t.Tng2);
                case "Tng":
                    return t => !string.IsNullOrEmpty(t.Tng) && string.IsNullOrEmpty(t.Tng1);
                case "Ng":
                    return t => !string.IsNullOrEmpty(t.Ng) && string.IsNullOrEmpty(t.Tng);
                case "Ttm":
                    return t => !string.IsNullOrEmpty(t.Ttm) && string.IsNullOrEmpty(t.Ng);
                case "Tm":
                    return t => !string.IsNullOrEmpty(t.Tm) && string.IsNullOrEmpty(t.Ttm);
                case "M":
                    return t => !string.IsNullOrEmpty(t.M) && string.IsNullOrEmpty(t.Tm);
                case "K":
                    return t => !string.IsNullOrEmpty(t.K) && string.IsNullOrEmpty(t.M);
                case "L":
                    return t => !string.IsNullOrEmpty(t.L) && string.IsNullOrEmpty(t.K);
                case "Lns":
                    return t => !string.IsNullOrEmpty(t.Lns) && string.IsNullOrEmpty(t.L);
                default:
                    return t => true;
            }
        }

        public override void OnClose(object obj)
        {
            Window wd = obj as Window;
            wd.Close();
        }

        private bool CompareNonEmptyValue(string s1, string s2)
        {
            return !string.IsNullOrEmpty(s1) && !string.IsNullOrEmpty(s2) && s1.Equals(s2);
        }

    }
}
