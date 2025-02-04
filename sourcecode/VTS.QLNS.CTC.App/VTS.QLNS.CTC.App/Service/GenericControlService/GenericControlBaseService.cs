using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.View.Shared;
using VTS.QLNS.CTC.App.ViewModel;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class GenericControlBaseService<TModel, TEntity, TService>
        where TModel : ModelBase, new()
        where TEntity : EntityBase, new()
        where TService : IService<TEntity>
    {
        public GenericControlCustomViewModel<TModel, TEntity, TService> sourceVM;

        private ICollection<TModel> _filterResult = new HashSet<TModel>();


        private TModel _filterModel;
        public TModel FilterModel
        {
            get => _filterModel;
            set => _filterModel = value;
        }

        public Type ModelType => typeof(TModel);

        private string xnmConcatenation = "";

        public static GenericControlBaseService<TModel, TEntity, TService> BuildControlService(Type modelType)
        {
            if (modelType.Equals(typeof(CauHinhMLNSModel)))
                return new CauHinhMLNSModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(DanhMucNhomNganhModel)))
                return new DanhMucNhomNganhModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(DmChuDauTuModel)))
                return new DmChuDauTuModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(DmChuKyModel)))
                return new DmChuKyModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(DonViModel)))
                return new DonViModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(HTChucNangModel)))
                return new HTChucNangModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(HTQuyenModel)))
                return new HTQuyenModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(NguonNganSachModel)))
                return new NguonNganSachModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(NsMuclucNgansachModel)))
                return new NsMuclucNganSachModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(CauHinhMLNSChiTieuLuongModel)))
                return new CauHinhMLNSChiTieuLuongModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(RevenueExpenditureCategoryModel)))
                return new RevenueExpenditureCategoryControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(SktMucLucModel)))
                return new SktMucLucModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(DanhMucNganhModel)))
                return new DanhMucNganhModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(NhDmLoaiCongTrinhModel)))
                return new NhDmLoaiCongTrinhControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(VdtDmLoaiCongTrinhModel)))
                return new VdtDmLoaiCongTrinhControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(CauHinhCanCuModel)))
                return new CauHinhCanCuModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(TLDmKinhPhiModel)))
                return new TLDmKinhPhiModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(TlDmPhuCapModel)))
                return new TlDmPhuCapModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            //if (modelType.Equals(typeof(TlDmPhuCapMapModel)))
            //    return new TlDmPhuCapModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(TlDmPhuCapNq104Model)))
                return new TlDmPhuCapNq104ModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(TlDmPhuCapNq104Model)))
                return new TlDmPhuCapNq104ModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(VdtDmChiPhiModel)))
                return new VdtDmChiPhiModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(TlDmPhuCapHeThongModel)))
                return new TlDmPhuCapHeThongModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(TlDmPhuCapHeThongNq104Model)))
                return new TlDmPhuCapHeThongNq104ModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(VdtDmDonViThucHienDuAnModel)))
                return new VdtDmDonViThucHienDuAnModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(DmCapBacModel)))
                return new DmCapBacModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(DmCapBacNhomChuKyModel)))
                return new DmCapBacNhomChuKyModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(TlDmDonViModel)))
                return new TlDmDonViModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(DmLoaiTienTeModel)))
                return new DmLoaiTienTeModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(DanhMucCauHinhHeThongModel)))
                return new DanhMucCauHinhHeThongModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(DanhMucNguonNganSachModel)))
                return new DanhMucNguonNganSachControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(NhDmNhiemVuChiModel)))
                return new NhDmNhiemVuChiModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(TlDmPhuCapModel)))
                return new TlDmPhuCapHeThongModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(TlDmPhuCapNq104Model)))
                return new TlDmPhuCapHeThongNq104ModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(BhDmCheDoBhxhModel)))
                return new BhDmCheDoBhxhModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(BhDmCauHinhThamSoModel)))
                return new BhDmCauHinhThamSoModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(BhDmMucLucBHXHMapModel)))
                return new BhDmMucLucBHXHMapModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(VdtDmDuToanChiModel)))
                return new VDTDmDTChiModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(NhDmLoaiHopDongModel)))
                return new NhDmLoaiHopDongModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(NhDmChiPhiModel)))
                return new NhDmChiPhiModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(DmCongKhaiTaiChinhModel)))
                return new DmCongKhaiTaiChinhControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(NsMuclucNganSachChildModel)))
                return new NsMucLucNganSachChildModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(NhDmLoaiTaiSanModel)))
                return new NhDmLoaiTaiSanModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(NhDmNoiDungChiModel)))
                return new NhDmNoiDungChiModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(NhDaDuAnModel)))
                return new NhDaDuAnModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(NhDaHopDongModel)))
                return new NhDaHopDongModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(NhDmNhaThauModel)))
                return new NhDmNhaThauModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(TlDmThueThuNhapCaNhanModel)))
                return new TlDmThueThuNhapCaNhanModelControllerService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(TlDmCheDoBHXHModel)))
                return new TlDmCheDoBHXHModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(BhDanhMucLoaiChiModel)))
                return new CauHinhMLLCModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(BhDmMucDongBHXHModel)))
                return new CauHinhMDBHXHModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(BhDmCoSoYTeModel)))
                return new CauHinhDMCoSoYTeModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(TlDmThueThuNhapCaNhanNq104Model)))
                return new TlDmThueThuNhapCaNhanNq104ModelControllerService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(TlDanhMucChucVuNq104Model)))
                return new TlDanhMucChucVuNq104ModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(TlDanhMucChucDanhNq104Model)))
                return new TlDanhMucChucDanhNq104ModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(BhDmThamDinhQuyetToanModel)))
                return new BhDmThamDinhQuyetToanDanhMucModelControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            if (modelType.Equals(typeof(DmMucLucQuyetToanModel)))
                return new NsMucLucQuyetToanNamControlService() as GenericControlBaseService<TModel, TEntity, TService>;
            return new GenericControlBaseService<TModel, TEntity, TService>();
        }

        public virtual void OnAdd(object obj)
        {
            var Items = sourceVM.Items;
            var SelectedItem = sourceVM.SelectedItem;
            string sKeyName = string.Empty;
            if (SelectedItem != null)
            {
                var prop = SelectedItem.GetType().GetProperties();
                if (prop[0].PropertyType.Name == "Guid")
                    sKeyName = prop[0].Name;
            }
            if (!sourceVM.IsVisibleAddBtn)
            {
                return;
            }
            try
            {
                DataGrid dgdData = obj as DataGrid;
                this.CancelEditData(dgdData);

                int currentRow = Items.Count - 1;
                TModel newRow;
                if (SelectedItem == null)
                {
                    newRow = new TModel();
                    CustomValueProps(newRow, SelectedItem);
                    Items.Add(newRow);
                }
                else
                {
                    currentRow = Items.IndexOf(SelectedItem);
                    newRow = ObjectCopier.Clone(SelectedItem);

                    if (!string.IsNullOrEmpty(sKeyName))
                    {
                        newRow.GetType().GetProperty(sKeyName).SetValue(newRow, Guid.Empty);
                    }
                    CustomValueProps(newRow, SelectedItem);
                    Items.Insert(currentRow + 1, newRow);
                }
                newRow.PropertyChanged += Item_PropertyChanged;
                OnPropertyChanged(newRow);
                OnCustomValueIndex(newRow, Items);
                sourceVM.InvokePropertyChange(nameof(Items));

                var cell = new DataGridCellInfo(Items[currentRow + 1], dgdData.Columns[0]);
                dgdData.ScrollIntoView(Items[currentRow + 1], dgdData.Columns[0]);
                dgdData.CurrentCell = cell;
                dgdData.BeginEdit();
            }
            catch (Exception ex)
            {
                sourceVM._logger.Error(ex.Message, ex);
            }
        }

        public void CancelEditData(DataGrid dgdData)
        {
            if (dgdData != null)
            {
                // One for the column and another for the row
                dgdData.CancelEdit();
                dgdData.CancelEdit();
            }
        }

        public virtual void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!e.PropertyName.Equals(nameof(ModelBase.IsDeleted)) && !e.PropertyName.Equals(nameof(ModelBase.IsSelected)) && !e.PropertyName.Equals(nameof(ModelBase.IsModified)) && !e.PropertyName.Equals(nameof(ModelBase.IsFilter)))
                ((ModelBase)sender).IsModified = true;

            if (sourceVM.IsMultipleSelect && e.PropertyName.Equals(nameof(ModelBase.IsSelected)))
            {
                sourceVM.InvokePropertyChange(nameof(sourceVM.IsAllItemsSelected));
            }

            if (ModelType.Name.Equals("TlDmPhuCapHeThongModel"))
            {
                TlDmPhuCapHeThongModel item = sender as TlDmPhuCapHeThongModel;
                if (e.PropertyName == nameof(TlDmPhuCapHeThongModel.GiaTriMoi))
                {
                    if (!item.MaPhuCap.Equals(PhuCap.TENNGANHANG))
                    {
                        double.TryParse(item.GiaTriMoi, out var rs);
                        item.GiaTriMoi = rs.ToString("N2");
                    }
                }
            }
            else if (ModelType.Name.Equals("TlDmPhuCapHeThongNq104Model"))
            {
                TlDmPhuCapHeThongNq104Model item = sender as TlDmPhuCapHeThongNq104Model;
                if (e.PropertyName == nameof(TlDmPhuCapHeThongNq104Model.GiaTriMoi))
                {
                    if (!item.MaPhuCap.Equals(PhuCap.TENNGANHANG))
                    {
                        double.TryParse(item.GiaTriMoi, out var rs);
                        item.GiaTriMoi = rs.ToString("N2");
                    }
                }
            }
            else if (ModelType.Name.Equals("TlDmCheDoBHXHModel"))
            {
                TlDmCheDoBHXHModel item = sender as TlDmCheDoBHXHModel;
                if (e.PropertyName == nameof(TlDmCheDoBHXHModel.SLoaiTruyLinh) && sourceVM.Items is ObservableCollection<TlDmCheDoBHXHModel>)
                {

                    var lstChuongTrinh = sourceVM._mapper.Map<ObservableCollection<TlDmCheDoBHXHModel>>(sourceVM.Items);
                    if (!lstChuongTrinh.IsEmpty() && (lstChuongTrinh.Any(x => !string.IsNullOrEmpty(x.SLoaiTruyLinh) && x.SLoaiTruyLinh.Equals(item.SLoaiTruyLinh) && !x.Id.Equals(item.Id)) || lstChuongTrinh.Any(x => !string.IsNullOrEmpty(x.SLoaiTruyLinh) && x.SLoaiTruyLinh.Equals(item.SLoaiTruyLinh) && x.Id.Equals(item.Id)) && lstChuongTrinh.Count(c => c.Id.Equals(Guid.Empty) && !string.IsNullOrEmpty(c.SLoaiTruyLinh) && c.SLoaiTruyLinh.Equals(item.SLoaiTruyLinh)) > 1))
                    {
                        MessageBoxHelper.Warning(string.Format(Resources.MsgWarningLoaiTruyLinhDuplicate, item.SLoaiTruyLinh));
                    }
                }
            }
        }

        public virtual void OnPropertyChanged(TModel model)
        {
        }

        public virtual void OnCustomValueIndex(TModel newRow, ObservableCollection<TModel> items, bool isAdd = true)
        {
            var index = items.IndexOf(newRow);
            if (typeof(TModel) == typeof(BhDmThamDinhQuyetToanModel))
            {
                for (int i = index; i <= items.Count; i++)
                {
                    BhDmThamDinhQuyetToanModel itemPre = GetItemChange(isAdd ? i - 1 : i - 2, items);
                    BhDmThamDinhQuyetToanModel item = GetItemChange(isAdd ? i : i - 1, items);

                    if (!itemPre.Equals(item))
                        item.ISTT = itemPre.ISTT + 1;

                }
            }
        }

        private BhDmThamDinhQuyetToanModel GetItemChange(int index, ObservableCollection<TModel> items)
        {
            BhDmThamDinhQuyetToanModel item = items[index] as BhDmThamDinhQuyetToanModel;
            if (item.IsDeleted)
                return GetItemChange(index - 1, items);
            return item;
        }

        public virtual void CustomValueProps(TModel newRow, TModel currentRow)
        {
            newRow.Id = Guid.Empty;
            newRow.IsModified = true;
            PropertyInfo[] properties = typeof(TModel).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (Attribute.IsDefined(property, typeof(DisplayNameAttribute)) && property.PropertyType == typeof(string))
                {
                    string propName = property.Name;
                    string formatVal = sourceVM._formatDictionary.ContainsKey(propName) ? sourceVM._formatDictionary[propName] : string.Empty;
                    if (string.IsNullOrEmpty(formatVal) || !formatVal.Contains("#"))
                    {
                        continue;
                    }
                    int formatLength = formatVal.Length;
                    string prefix = string.Empty;
                    string currentRowVal = currentRow == null || property.GetValue(currentRow) == null ? string.Empty : property.GetValue(currentRow).ToString();
                    string propNameWithPrefixKey = string.Empty;
                    string codeVal = string.Empty;
                    int indexVal = 0;
                    var regex = new Regex(@"\d{" + formatLength + "}", RegexOptions.RightToLeft);
                    int lastMatchIndex = regex.Match(currentRowVal).Index;
                    if (lastMatchIndex != currentRowVal.Length - formatLength)
                    {
                        prefix = currentRowVal;
                        while (prefix.Length > 0 && Char.IsDigit(prefix[prefix.Length - 1]))
                            prefix = prefix.Substring(0, prefix.Length - 1);
                        propNameWithPrefixKey = propName + "-" + prefix + "-" + formatVal;
                    }
                    else
                    {
                        prefix = currentRowVal.Substring(0, currentRowVal.Length - formatLength);
                        int currentIndexVal = int.Parse(currentRowVal.Substring(currentRowVal.Length - formatLength));
                        propNameWithPrefixKey = propName + "-" + prefix + "-" + formatVal;
                        if (!sourceVM._currentCodeValDictionary.ContainsKey(propNameWithPrefixKey))
                            sourceVM._currentCodeValDictionary[propNameWithPrefixKey] = currentIndexVal;
                    }

                    if (sourceVM._currentCodeValDictionary.ContainsKey(propNameWithPrefixKey))
                    {
                        indexVal = sourceVM._currentCodeValDictionary[propNameWithPrefixKey] + 1;
                    }
                    else
                    {
                        indexVal = sourceVM._service.GetNextValueOfCode(propName, prefix, sourceVM._authenticationInfo);
                    }
                    sourceVM._currentCodeValDictionary[propNameWithPrefixKey] = indexVal;
                    codeVal = prefix + indexVal.ToString("D" + formatVal.Length);
                    property.SetValue(newRow, codeVal);
                }


            }
            if (typeof(TModel) == typeof(DmCongKhaiTaiChinhModel))
            {
                foreach (var prop in properties)
                {
                    if (prop.Name.Equals(nameof(DmCongKhaiTaiChinhModel.Mlns)))
                    {
                        prop.SetValue(newRow, string.Empty);
                    }

                }
            }

            if (typeof(TModel) == typeof(BhDmThamDinhQuyetToanModel))
            {
                foreach (var prop in properties)
                {
                    if (prop.Name.Equals(nameof(BhDmThamDinhQuyetToanModel.ILock)))
                    {
                        prop.SetValue(newRow, false);
                    }
                    else if (prop.Name.Equals(nameof(BhDmThamDinhQuyetToanModel.IsEnabled)))
                    {
                        prop.SetValue(newRow, true);
                    }
                    else if (prop.Name.Equals(nameof(BhDmThamDinhQuyetToanModel.ISTT)))
                    {
                        BhDmThamDinhQuyetToanModel item = newRow as BhDmThamDinhQuyetToanModel;
                        prop.SetValue(newRow, item.ISTT + 1);
                    }
                }
            }
        }

        public virtual void OnAddChild(object obj)
        {
        }

        public virtual void OnPropertyChanged()
        {
        }

        public virtual void CustomDataValue()
        {
        }

        public virtual void OnRefresh(object obj)
        {
            var rawData = sourceVM._service.FindAll(sourceVM._authenticationInfo);
            if (ModelType.Name.Equals("TlDmPhuCapHeThongModel") || ModelType.Name.Equals("TlDmPhuCapHeThongNq104Model"))
            {
                rawData = sourceVM._service.FindAllPhuCapHeThong(sourceVM._authenticationInfo).ToList();
            }
            else if (ModelType.Name.Equals("BhDmMucLucBHXHMapModel"))
            {
                rawData = sourceVM._service.FindAllThu(sourceVM._authenticationInfo).ToList();
            }
            else if (ModelType.Name.Equals("DmCapBacNhomChuKyModel"))
            {
                rawData = sourceVM._service.FindAllNhomChuKy(sourceVM._authenticationInfo).ToList();
            }
            foreach (var i in rawData)
            {
                i.IsDeleted = false;
                i.IsModified = false;
            }
            sourceVM.FilterModel = new TModel();
            sourceVM._currentCodeValDictionary = new Dictionary<string, int>();
            sourceVM.Items.Clear();
            sourceVM.Items = new ObservableCollection<TModel>();
            sourceVM.Items = sourceVM._mapper.Map<ObservableCollection<TModel>>(rawData);
            if (sourceVM.Items is ObservableCollection<NhDmNhiemVuChiModel>)
            {
                ObservableCollection<NhDmNhiemVuChiModel> lstChuongTrinhOrdered = new ObservableCollection<NhDmNhiemVuChiModel>();
                var lstChuongTrinh = sourceVM._mapper.Map<ObservableCollection<NhDmNhiemVuChiModel>>(sourceVM.Items);
                var lstChuongTrinhParent = lstChuongTrinh.Where(n => n.IIdParentId == null);
                foreach (var itemParent in lstChuongTrinhParent)
                {
                    lstChuongTrinhOrdered.Add(itemParent);
                    foreach (var item in lstChuongTrinh)
                    {
                        if (item.IIdParentId == null)
                        {
                            item.IsHangCha = true;
                        }
                        else if (item.IIdParentId == itemParent.Id)
                        {
                            lstChuongTrinhOrdered.Add(item);
                        }
                    }
                }
                sourceVM.Items = sourceVM._mapper.Map<ObservableCollection<TModel>>(lstChuongTrinhOrdered);
            }
            else if (sourceVM.Items is ObservableCollection<TlDmDonViModel>)
            {
                ObservableCollection<TlDmDonViModel> lstPhanHoOrdered = new ObservableCollection<TlDmDonViModel>();
                var lstPhanHo = sourceVM._mapper.Map<ObservableCollection<TlDmDonViModel>>(sourceVM.Items);
                var lstPhanHoOrd = lstPhanHo.OrderBy(x => Convert.ToDecimal(x.MaDonVi));
                foreach (var item in lstPhanHoOrd)
                {
                    lstPhanHoOrdered.Add(item);
                }
                sourceVM.Items = sourceVM._mapper.Map<ObservableCollection<TModel>>(lstPhanHoOrdered);
            }
            else if (sourceVM.Items is ObservableCollection<NhDmNhaThauModel>)
            {
                ObservableCollection<NhDmNhaThauModel> lstPhanHoOrdered = new ObservableCollection<NhDmNhaThauModel>();
                var lstPhanHo = sourceVM._mapper.Map<ObservableCollection<NhDmNhaThauModel>>(sourceVM.Items);
                var lstPhanHoOrd = lstPhanHo.OrderByDescending(x => x.DNgayTao).OrderBy(x => x.ILoai);
                foreach (var item in lstPhanHoOrd)
                {
                    lstPhanHoOrdered.Add(item);
                }
                sourceVM.Items = sourceVM._mapper.Map<ObservableCollection<TModel>>(lstPhanHoOrdered);
            }
            else if (sourceVM.Items is ObservableCollection<TlDmPhuCapHeThongModel>)
            {
                ObservableCollection<TlDmPhuCapHeThongModel> lstPhuCapHTConverted = new ObservableCollection<TlDmPhuCapHeThongModel>();
                var lstPhuCapHT = sourceVM._mapper.Map<ObservableCollection<TlDmPhuCapHeThongModel>>(sourceVM.Items);
                foreach (var item in lstPhuCapHT)
                {
                    if (!item.MaPhuCap.Equals(PhuCap.TENNGANHANG))
                    {
                        double.TryParse(item.GiaTriMoi, out var rs);
                        item.GiaTriMoi = rs.ToString("N2");
                    }
                    lstPhuCapHTConverted.Add(item);
                }
                sourceVM.Items = sourceVM._mapper.Map<ObservableCollection<TModel>>(lstPhuCapHTConverted);
            }
            else if (sourceVM.Items is ObservableCollection<TlDmPhuCapHeThongNq104Model>)
            {
                ObservableCollection<TlDmPhuCapHeThongNq104Model> lstPhuCapHTConverted = new ObservableCollection<TlDmPhuCapHeThongNq104Model>();
                var lstPhuCapHT = sourceVM._mapper.Map<ObservableCollection<TlDmPhuCapHeThongNq104Model>>(sourceVM.Items);
                foreach (var item in lstPhuCapHT)
                {
                    if (!item.MaPhuCap.Equals(PhuCap.TENNGANHANG))
                    {
                        double.TryParse(item.GiaTriMoi, out var rs);
                        item.GiaTriMoi = rs.ToString("N2");
                    }
                    lstPhuCapHTConverted.Add(item);
                }
                sourceVM.Items = sourceVM._mapper.Map<ObservableCollection<TModel>>(lstPhuCapHTConverted);
            }
            else if (sourceVM.Items is ObservableCollection<BhDmMucLucBHXHMapModel>)
            {
                ObservableCollection<BhDmMucLucBHXHMapModel> lstDmMLBHXHConverted = new ObservableCollection<BhDmMucLucBHXHMapModel>();
                var lstDmMLBHXH = sourceVM._mapper.Map<ObservableCollection<BhDmMucLucBHXHMapModel>>(sourceVM.Items);
                foreach (var item in lstDmMLBHXH)
                {
                    if (!string.IsNullOrEmpty(item.SNS_LuongChinh))
                    {
                        item.TenSNSLuongChinh = ((IBhDmMucLucBHXHMapService)sourceVM._service).GetMoTaMLNS(item.SNS_LuongChinh, sourceVM._authenticationInfo.YearOfWork);
                    }
                    if (!string.IsNullOrEmpty(item.SNS_PCCV))
                    {
                        item.TenSNSPCCV = ((IBhDmMucLucBHXHMapService)sourceVM._service).GetMoTaMLNS(item.SNS_PCCV, sourceVM._authenticationInfo.YearOfWork);
                    }
                    if (!string.IsNullOrEmpty(item.SNS_PCTN))
                    {
                        item.TenSNSPCTN = ((IBhDmMucLucBHXHMapService)sourceVM._service).GetMoTaMLNS(item.SNS_PCTN, sourceVM._authenticationInfo.YearOfWork);
                    }
                    if (!string.IsNullOrEmpty(item.SNS_PCTNVK))
                    {
                        item.TenSNSPCTNVK = ((IBhDmMucLucBHXHMapService)sourceVM._service).GetMoTaMLNS(item.SNS_PCTNVK, sourceVM._authenticationInfo.YearOfWork);
                    }
                    if (!string.IsNullOrEmpty(item.SNS_HSBL))
                    {
                        item.TenNSHSBL = ((IBhDmMucLucBHXHMapService)sourceVM._service).GetMoTaMLNS(item.SNS_HSBL, sourceVM._authenticationInfo.YearOfWork);
                    }
                    if (!string.IsNullOrEmpty(item.SLuongChinh))
                    {
                        item.TenSLuongChinh = ((IBhDmMucLucBHXHMapService)sourceVM._service).GetTenPhuCap(item.SLuongChinh);
                    }
                    if (!string.IsNullOrEmpty(item.SPCCV))
                    {
                        item.TenSPCCV = ((IBhDmMucLucBHXHMapService)sourceVM._service).GetTenPhuCap(item.SPCCV);
                    }
                    if (!string.IsNullOrEmpty(item.SPCTN))
                    {
                        item.TenSPCTN = ((IBhDmMucLucBHXHMapService)sourceVM._service).GetTenPhuCap(item.SPCTN);
                    }
                    if (!string.IsNullOrEmpty(item.SPCTNVK))
                    {
                        item.TenSPCTNVK = ((IBhDmMucLucBHXHMapService)sourceVM._service).GetTenPhuCap(item.SPCTNVK);
                    }
                    if (item.IsHangCha && (item.FTyLeBHXHNLD.GetValueOrDefault() != 0 || item.FTyLeBHXHNSD.GetValueOrDefault() != 0
                        || item.FTyLeBHYTNLD.GetValueOrDefault() != 0 || item.FTyLeBHYTNSD.GetValueOrDefault() != 0
                        || item.FTyLeBHTNNLD.GetValueOrDefault() != 0 || item.FTyLeBHTNNSD.GetValueOrDefault() != 0 || item.FHeSoLayQuyLuong.GetValueOrDefault() != 0))
                    {
                        item.FTyLeBHXHNLD = null;
                        item.FTyLeBHXHNSD = null;
                        item.FTyLeBHYTNLD = null;
                        item.FTyLeBHYTNSD = null;
                        item.FTyLeBHTNNLD = null;
                        item.FTyLeBHTNNSD = null;
                        item.FHeSoLayQuyLuong = null;
                    }
                    lstDmMLBHXHConverted.Add(item);
                }
                sourceVM.Items = sourceVM._mapper.Map<ObservableCollection<TModel>>(lstDmMLBHXHConverted);
            }
            else if (sourceVM.Items is ObservableCollection<NsMucLucNgansachMapModel>)
            {
                ObservableCollection<NsMucLucNgansachMapModel> lstDmMLNSConverted = new ObservableCollection<NsMucLucNgansachMapModel>();
                var lstMLNS = sourceVM._mapper.Map<ObservableCollection<NsMucLucNgansachMapModel>>(sourceVM.Items);
                sourceVM.AfterSelectAll = true;
                var allXNm = sourceVM._service.FindByBHXHMucLucIn(sourceVM._authenticationInfo.YearOfWork, sourceVM._authenticationInfo.OptionalParam[1].ToString(), sourceVM._authenticationInfo.OptionalParam[2].ToString());
                foreach (var model in lstMLNS)
                {

                    if (allXNm.StartsWith(model.XNM + "-") || allXNm.Contains("," + model.XNM + "-") || allXNm.Contains(model.XNM))
                    {
                        model.IsSelected = true;
                    }
                    // nếu loại là LNS (1, 101, 1010000) thì check startwith (ko cos daaus -)
                    if (string.IsNullOrEmpty(model.L))
                    {
                        model.IsSelected = allXNm.StartsWith(model.XNM);
                    }
                    lstDmMLNSConverted.Add(model);
                }
                sourceVM.Items = sourceVM._mapper.Map<ObservableCollection<TModel>>(lstDmMLNSConverted);
                sourceVM.AfterSelectAll = false;
            }
            else if (sourceVM.Items is ObservableCollection<TlDmPhuCapMapModel>)
            {
                ObservableCollection<TlDmPhuCapMapModel> lstDmPhuCapConverted = new ObservableCollection<TlDmPhuCapMapModel>();
                var lstPhuCap = sourceVM._mapper.Map<ObservableCollection<TlDmPhuCapMapModel>>(sourceVM.Items);
                sourceVM.AfterSelectAll = true;
                var allXNm = sourceVM._service.FindByBHXHPhuCapIn(sourceVM._authenticationInfo.YearOfWork, sourceVM._authenticationInfo.OptionalParam[1].ToString(), sourceVM._authenticationInfo.OptionalParam[2].ToString());
                foreach (var model in lstPhuCap)
                {
                    if (allXNm.Contains(model.MaPhuCap))
                    {
                        model.IsSelected = true;
                    }
                    lstDmPhuCapConverted.Add(model);
                }
                sourceVM.Items = sourceVM._mapper.Map<ObservableCollection<TModel>>(lstDmPhuCapConverted);
                sourceVM.AfterSelectAll = false;
            }
            else if (sourceVM.Items is ObservableCollection<BhDmThamDinhQuyetToanModel>)
            {
                var lstDataConverted = sourceVM._mapper.Map<ObservableCollection<BhDmThamDinhQuyetToanModel>>(sourceVM.Items);
                lstDataConverted.Select(x => x.IsEnabled = !x.ILock).ToList();
                sourceVM.Items = sourceVM._mapper.Map<ObservableCollection<TModel>>(lstDataConverted);
                sourceVM.AfterSelectAll = false;
            }
            CustomDataValue();
            OrderData();
            foreach (var item in sourceVM.Items)
            {
                item.PropertyChanged += Item_PropertyChanged;
            }
            OnPropertyChanged();
            sourceVM._isFirstLoad = true;
            sourceVM._dataCollectionView = CollectionViewSource.GetDefaultView(sourceVM.Items);
            sourceVM._dataCollectionView.SortDescriptions.Clear();
            sourceVM._dataCollectionView.Filter = ItemsViewFilter;
            sourceVM._isFirstLoad = false;
            sourceVM.InvokePropertyChange(nameof(sourceVM.Items));
            DataGrid dgdData = obj as DataGrid;
            if (dgdData != null && dgdData.Columns != null)
            {
                foreach (var column in dgdData.Columns)
                {
                    column.SortDirection = null;
                }
            }
            this.CancelEditData(dgdData);
            if (dgdData.Items.Count > 0)
            {
                var cell = new DataGridCellInfo(dgdData.Items[0], dgdData.Columns[0]);
                sourceVM.SelectedItem = (TModel)dgdData.Items[0];
                dgdData.CurrentCell = cell;
            }
        }

        public virtual bool ItemsViewFilter(object obj)
        {
            if (sourceVM._isFirstLoad)
            {
                return true;
            }
            bool result = true;
            var item = (TModel)obj;

            foreach (var property in item.GetType().GetProperties())
            {
                string propertyType = property.PropertyType.Name;
                if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    propertyType = property.PropertyType.GetGenericArguments()[0].Name;
                }
                if (Attribute.IsDefined(property, typeof(DisplayNameAttribute)) && (propertyType.Equals("String") || propertyType.Equals(nameof(Int32))))
                {
                    if (property.Name.ToLower().Contains("thutu") || property.Name.ToLower().Contains("itrangthai"))
                        continue;
                    if (propertyType.Equals(nameof(Int32))) continue;
                    string value = property.GetValue(item, null) != null ? property.GetValue(item, null).ToString() : "";
                    string filterValue = property.GetValue(sourceVM.FilterModel, null) != null ? property.GetValue(sourceVM.FilterModel, null).ToString().Trim() : "";
                    if (!string.IsNullOrEmpty(filterValue))
                    {
                        property.SetValue(sourceVM.FilterModel, filterValue, null);
                        result = result && value.ToLower().IndexOf(filterValue.ToLower()) > -1;
                        if (typeof(NsMucLucNgansachMapModel).Equals(typeof(TModel)) || typeof(TlDmPhuCapMapModel).Equals(typeof(TModel)))
                        {
                            if (sourceVM.IsVisibleFilterByMlnsMappingType && !string.IsNullOrEmpty(sourceVM.MlnsMapping))
                                result = result && item.IsSelected.Equals(Convert.ToBoolean(sourceVM.MlnsMapping));
                        }
                    }
                }
            }

            return result;
        }

        public virtual void LoadData(params object[] args)
        {
            var iID_DonViID = args.Length > 1 ? Guid.TryParse(args[1]?.ToString(), out Guid iddv) ? iddv : (Guid?)null : (Guid?)null;
            var iIdKhttNhiemVuChiId = args.Length > 2 ? Guid.TryParse(args[2]?.ToString(), out Guid iddv1) ? iddv1 : (Guid?)null : (Guid?)null;
            var isLoaiChucVu = args.Length > 3 ? (bool)args[3] : false;
            var namLamViec = args.Length > 4 ? int.TryParse(args[4]?.ToString(), out int value) ? value : sourceVM._authenticationInfo.YearOfWork : sourceVM._authenticationInfo.YearOfWork;
            var data = sourceVM._service.FindAll(sourceVM._authenticationInfo).ToList();
            if (ModelType.Name.Equals("DmCapBacNhomChuKyModel"))
            {
                data = sourceVM._service.FindAllNhomChuKy(sourceVM._authenticationInfo).ToList();
            }
            if (ModelType.Name.Equals("TlDmPhuCapHeThongModel") || ModelType.Name.Equals("TlDmPhuCapHeThongNq104Model"))
            {
                data = sourceVM._service.FindAllPhuCapHeThong(sourceVM._authenticationInfo).ToList();
            }
            else if (ModelType.Name.Equals("TlDanhMucChucVuNq104Model") || ModelType.Name.Equals("TlDanhMucChucDanhNq104Model"))
            {
                data = sourceVM._service.FindListChucVu(isLoaiChucVu, namLamViec).ToList();
            }
            else if (ModelType.Name.Equals("BhDmMucLucBHXHMapModel"))
            {
                data = sourceVM._service.FindAllThu(sourceVM._authenticationInfo).ToList();
            }
            //else if (ModelType.Name.Equals("TlDmPhuCapMapModel"))
            //{
            //    data = sourceVM._service.FindAllTTPhuCapLuong(sourceVM._authenticationInfo).ToList();
            //}
            else if (ModelType.Name.Equals("TlDmPhuCapNq104Model"))
            {
                data = sourceVM._service.FindDmPhuCapNq104(namLamViec).ToList();
            }
            sourceVM.Items = sourceVM._mapper.Map<ObservableCollection<TModel>>(data);
            if (sourceVM.Items is ObservableCollection<NhDmNhiemVuChiModel>)
            {
                ObservableCollection<NhDmNhiemVuChiModel> lstChuongTrinhOrdered = new ObservableCollection<NhDmNhiemVuChiModel>();
                var lstChuongTrinh = sourceVM._mapper.Map<ObservableCollection<NhDmNhiemVuChiModel>>(sourceVM.Items);
                var lstChuongTrinhParent = lstChuongTrinh.Where(n => n.IIdParentId == null);
                foreach (var itemParent in lstChuongTrinhParent)
                {
                    lstChuongTrinhOrdered.Add(itemParent);
                    foreach (var item in lstChuongTrinh)
                    {
                        if (item.IIdParentId == null)
                        {
                            item.IsHangCha = true;
                        }
                        else if (item.IIdParentId == itemParent.Id)
                        {
                            lstChuongTrinhOrdered.Add(item);
                        }
                    }
                }
                sourceVM.Items = sourceVM._mapper.Map<ObservableCollection<TModel>>(lstChuongTrinhOrdered.OrderBy(x => x.SMaNhiemVuChi));
            }
            else if (sourceVM.Items is ObservableCollection<NhDaDuAnModel>)
            {
                ObservableCollection<NhDaDuAnModel> lstChuongTrinhOrdered = new ObservableCollection<NhDaDuAnModel>();
                var lstChuongTrinh = sourceVM._mapper.Map<ObservableCollection<NhDaDuAnModel>>(sourceVM.Items);
                var lstChuongTrinhParent = lstChuongTrinh.Where(n => n.IIdDonViQuanLyId == iID_DonViID).OrderByDescending(x => x.DNgayTao);
                foreach (var itemParent in lstChuongTrinhParent)
                {
                    lstChuongTrinhOrdered.Add(itemParent);
                    foreach (var item in lstChuongTrinh)
                    {
                        if (item.IIdDonViQuanLyId == itemParent.Id)
                        {
                            lstChuongTrinhOrdered.Add(item);
                        }
                    }
                }
                sourceVM.Items = sourceVM._mapper.Map<ObservableCollection<TModel>>(lstChuongTrinhOrdered);
            }
            else if (sourceVM.Items is ObservableCollection<TlDanhMucChucVuNq104Model> chucVu)
            {
                var dictMaCha = chucVu.Select(x => x.MaCha).ToHashSet();
                chucVu.ForAll(x =>
                {
                    x.IsHangCha = dictMaCha.Contains(x.Ma);
                });
                //sourceVM.Items = sourceVM._mapper.Map<ObservableCollection<TModel>>(chucVu);
            }
            else if (sourceVM.Items is ObservableCollection<TlDanhMucChucDanhNq104Model> chucDanh)
            {
                var dictMaCha = chucDanh.Select(x => x.MaCha).ToHashSet();
                chucDanh.ForAll(x =>
                {
                    x.IsHangCha = dictMaCha.Contains(x.Ma);
                });
                //sourceVM.Items = sourceVM._mapper.Map<ObservableCollection<TModel>>(chucVu);
            }
            else if (sourceVM.Items is ObservableCollection<NhDaHopDongModel>)
            {
                ObservableCollection<NhDaHopDongModel> lstChuongTrinhOrdered = new ObservableCollection<NhDaHopDongModel>();
                var lstChuongTrinh = sourceVM._mapper.Map<ObservableCollection<NhDaHopDongModel>>(sourceVM.Items);
                var lstChuongTrinhParent = lstChuongTrinh.Where(n => n.IIdDonViQuanLyId == iID_DonViID).OrderByDescending(x => x.DNgayTao);
                foreach (var itemParent in lstChuongTrinhParent)
                {
                    lstChuongTrinhOrdered.Add(itemParent);
                    foreach (var item in lstChuongTrinh)
                    {
                        if (item.IIdDonViQuanLyId == itemParent.Id)
                        {
                            lstChuongTrinhOrdered.Add(item);
                        }
                    }
                }
                sourceVM.Items = sourceVM._mapper.Map<ObservableCollection<TModel>>(lstChuongTrinhOrdered);
            }
            else if (sourceVM.Items is ObservableCollection<TlDmDonViModel>)
            {
                ObservableCollection<TlDmDonViModel> lstPhanHoOrdered = new ObservableCollection<TlDmDonViModel>();
                var lstPhanHo = sourceVM._mapper.Map<ObservableCollection<TlDmDonViModel>>(sourceVM.Items);
                var lstPhanHoOrd = lstPhanHo.OrderBy(x => Convert.ToDecimal(x.MaDonVi));
                foreach (var item in lstPhanHoOrd)
                {
                    lstPhanHoOrdered.Add(item);
                }
                sourceVM.Items = sourceVM._mapper.Map<ObservableCollection<TModel>>(lstPhanHoOrdered);
            }
            else if (sourceVM.Items is ObservableCollection<NhDmNhaThauModel>)
            {
                ObservableCollection<NhDmNhaThauModel> lstPhanHoOrdered = new ObservableCollection<NhDmNhaThauModel>();
                var lstPhanHo = sourceVM._mapper.Map<ObservableCollection<NhDmNhaThauModel>>(sourceVM.Items);
                var lstPhanHoOrd = lstPhanHo.OrderByDescending(x => x.DNgayTao).OrderBy(x => x.ILoai);
                foreach (var item in lstPhanHoOrd)
                {
                    lstPhanHoOrdered.Add(item);
                }
                sourceVM.Items = sourceVM._mapper.Map<ObservableCollection<TModel>>(lstPhanHoOrdered);
            }
            else if (sourceVM.Items is ObservableCollection<TlDmPhuCapHeThongModel>)
            {
                ObservableCollection<TlDmPhuCapHeThongModel> lstPhuCapHTConverted = new ObservableCollection<TlDmPhuCapHeThongModel>();
                var lstPhuCapHT = sourceVM._mapper.Map<ObservableCollection<TlDmPhuCapHeThongModel>>(sourceVM.Items);
                foreach (var item in lstPhuCapHT)
                {
                    if (!item.MaPhuCap.Equals(PhuCap.TENNGANHANG))
                    {
                        double.TryParse(item.GiaTriMoi, out var rs);
                        item.GiaTriMoi = rs.ToString("N2");
                    }
                    lstPhuCapHTConverted.Add(item);
                }
                sourceVM.Items = sourceVM._mapper.Map<ObservableCollection<TModel>>(lstPhuCapHTConverted);
            }
            else if (sourceVM.Items is ObservableCollection<TlDmPhuCapHeThongNq104Model>)
            {
                ObservableCollection<TlDmPhuCapHeThongNq104Model> lstPhuCapHTConverted = new ObservableCollection<TlDmPhuCapHeThongNq104Model>();
                var lstPhuCapHT = sourceVM._mapper.Map<ObservableCollection<TlDmPhuCapHeThongNq104Model>>(sourceVM.Items);
                foreach (var item in lstPhuCapHT)
                {
                    if (!item.MaPhuCap.Equals(PhuCap.TENNGANHANG))
                    {
                        double.TryParse(item.GiaTriMoi, out var rs);
                        item.GiaTriMoi = rs.ToString("N2");
                    }
                    lstPhuCapHTConverted.Add(item);
                }
                sourceVM.Items = sourceVM._mapper.Map<ObservableCollection<TModel>>(lstPhuCapHTConverted);
            }
            else if (sourceVM.Items is ObservableCollection<BhDmMucLucBHXHMapModel>)
            {
                ObservableCollection<BhDmMucLucBHXHMapModel> lstDmMLBHXHConverted = new ObservableCollection<BhDmMucLucBHXHMapModel>();
                var lstDmMLBHXH = sourceVM._mapper.Map<ObservableCollection<BhDmMucLucBHXHMapModel>>(sourceVM.Items);
                foreach (var item in lstDmMLBHXH)
                {
                    if (!string.IsNullOrEmpty(item.SNS_LuongChinh))
                    {
                        item.TenSNSLuongChinh = ((IBhDmMucLucBHXHMapService)sourceVM._service).GetMoTaMLNS(item.SNS_LuongChinh, sourceVM._authenticationInfo.YearOfWork);
                    }
                    if (!string.IsNullOrEmpty(item.SNS_PCCV))
                    {
                        item.TenSNSPCCV = ((IBhDmMucLucBHXHMapService)sourceVM._service).GetMoTaMLNS(item.SNS_PCCV, sourceVM._authenticationInfo.YearOfWork);
                    }
                    if (!string.IsNullOrEmpty(item.SNS_PCTN))
                    {
                        item.TenSNSPCTN = ((IBhDmMucLucBHXHMapService)sourceVM._service).GetMoTaMLNS(item.SNS_PCTN, sourceVM._authenticationInfo.YearOfWork);
                    }
                    if (!string.IsNullOrEmpty(item.SNS_PCTNVK))
                    {
                        item.TenSNSPCTNVK = ((IBhDmMucLucBHXHMapService)sourceVM._service).GetMoTaMLNS(item.SNS_PCTNVK, sourceVM._authenticationInfo.YearOfWork);
                    }
                    if (!string.IsNullOrEmpty(item.SNS_HSBL))
                    {
                        item.TenNSHSBL = ((IBhDmMucLucBHXHMapService)sourceVM._service).GetMoTaMLNS(item.SNS_HSBL, sourceVM._authenticationInfo.YearOfWork);
                    }
                    if (!string.IsNullOrEmpty(item.SLuongChinh))
                    {
                        item.TenSLuongChinh = ((IBhDmMucLucBHXHMapService)sourceVM._service).GetTenPhuCap(item.SLuongChinh);
                    }
                    if (!string.IsNullOrEmpty(item.SPCCV))
                    {
                        item.TenSPCCV = ((IBhDmMucLucBHXHMapService)sourceVM._service).GetTenPhuCap(item.SPCCV);
                    }
                    if (!string.IsNullOrEmpty(item.SPCTN))
                    {
                        item.TenSPCTN = ((IBhDmMucLucBHXHMapService)sourceVM._service).GetTenPhuCap(item.SPCTN);
                    }
                    if (!string.IsNullOrEmpty(item.SPCTNVK))
                    {
                        item.TenSPCTNVK = ((IBhDmMucLucBHXHMapService)sourceVM._service).GetTenPhuCap(item.SPCTNVK);
                    }
                    if (item.IsHangCha && (item.FTyLeBHXHNLD.GetValueOrDefault() != 0 || item.FTyLeBHXHNSD.GetValueOrDefault() != 0
                        || item.FTyLeBHYTNLD.GetValueOrDefault() != 0 || item.FTyLeBHYTNSD.GetValueOrDefault() != 0
                        || item.FTyLeBHTNNLD.GetValueOrDefault() != 0 || item.FTyLeBHTNNSD.GetValueOrDefault() != 0 || item.FHeSoLayQuyLuong.GetValueOrDefault() != 0))
                    {
                        item.FTyLeBHXHNLD = null;
                        item.FTyLeBHXHNSD = null;
                        item.FTyLeBHYTNLD = null;
                        item.FTyLeBHYTNSD = null;
                        item.FTyLeBHTNNLD = null;
                        item.FTyLeBHTNNSD = null;
                        item.FHeSoLayQuyLuong = null;
                    }
                    lstDmMLBHXHConverted.Add(item);
                }
                sourceVM.Items = sourceVM._mapper.Map<ObservableCollection<TModel>>(lstDmMLBHXHConverted);
            }
            else if (sourceVM.Items is ObservableCollection<NsMucLucNgansachMapModel>)
            {
                ObservableCollection<NsMucLucNgansachMapModel> lstDmMLNSConverted = new ObservableCollection<NsMucLucNgansachMapModel>();
                var lstMLNS = sourceVM._mapper.Map<ObservableCollection<NsMucLucNgansachMapModel>>(sourceVM.Items);
                sourceVM.AfterSelectAll = true;
                var allXNm = sourceVM._service.FindByBHXHMucLucIn(sourceVM._authenticationInfo.YearOfWork, sourceVM._authenticationInfo.OptionalParam[1].ToString(), sourceVM._authenticationInfo.OptionalParam[2].ToString());
                foreach (var model in lstMLNS)
                {

                    if (allXNm.StartsWith(model.XNM + "-") || allXNm.Contains("," + model.XNM + "-") || allXNm.Contains(model.XNM))
                    {
                        model.IsSelected = true;
                    }
                    // nếu loại là LNS (1, 101, 1010000) thì check startwith (ko cos daaus -)
                    if (string.IsNullOrEmpty(model.L))
                    {
                        model.IsSelected = allXNm.StartsWith(model.XNM);
                    }
                    lstDmMLNSConverted.Add(model);
                }
                sourceVM.Items = sourceVM._mapper.Map<ObservableCollection<TModel>>(lstDmMLNSConverted);
                sourceVM.AfterSelectAll = false;
            }
            else if (sourceVM.Items is ObservableCollection<TlDmPhuCapMapModel>)
            {
                ObservableCollection<TlDmPhuCapMapModel> lstDmPhuCapConverted = new ObservableCollection<TlDmPhuCapMapModel>();
                var lstPhuCap = sourceVM._mapper.Map<ObservableCollection<TlDmPhuCapMapModel>>(sourceVM.Items);
                sourceVM.AfterSelectAll = true;
                var allXNm = sourceVM._service.FindByBHXHPhuCapIn(sourceVM._authenticationInfo.YearOfWork, sourceVM._authenticationInfo.OptionalParam[1].ToString(), sourceVM._authenticationInfo.OptionalParam[2].ToString());
                foreach (var model in lstPhuCap)
                {
                    if (allXNm.Contains(model.MaPhuCap))
                    {
                        model.IsSelected = true;
                    }
                    lstDmPhuCapConverted.Add(model);
                }
                sourceVM.Items = sourceVM._mapper.Map<ObservableCollection<TModel>>(lstDmPhuCapConverted);
                sourceVM.AfterSelectAll = false;
            }
            else if (sourceVM.Items is ObservableCollection<BhDmThamDinhQuyetToanModel>)
            {
                var lstDataConverted = sourceVM._mapper.Map<ObservableCollection<BhDmThamDinhQuyetToanModel>>(sourceVM.Items);
                lstDataConverted.Select(x => x.IsEnabled = !x.ILock).ToList();
                sourceVM.Items = sourceVM._mapper.Map<ObservableCollection<TModel>>(lstDataConverted);
                sourceVM.AfterSelectAll = false;
            }
            OrderData();
            foreach (var item in sourceVM.Items)
            {
                item.PropertyChanged += Item_PropertyChanged;
            }
            OnPropertyChanged();
            sourceVM._isFirstLoad = true;
            sourceVM._dataCollectionView = CollectionViewSource.GetDefaultView(sourceVM.Items);
            sourceVM._dataCollectionView.Filter = ItemsViewFilter;
            sourceVM.InvokePropertyChange(nameof(sourceVM.Items));
            sourceVM._isFirstLoad = false;
        }

        public virtual void OrderData()
        {
            if (sourceVM.Items is ObservableCollection<NhDmTaiKhoanModel>)
            {
                ObservableCollection<NhDmTaiKhoanModel> listDmTaiKhoan = sourceVM._mapper.Map<ObservableCollection<NhDmTaiKhoanModel>>(sourceVM.Items);
                listDmTaiKhoan ??= new ObservableCollection<NhDmTaiKhoanModel>();
                sourceVM.Items = sourceVM._mapper.Map<ObservableCollection<TModel>>(listDmTaiKhoan.OrderBy(s => s.SNhomTaiKhoan).ThenBy(x => x.SMaTaiKhoan));
            }
        }

        public virtual void InitDialog(PropertyInfo property)
        {
        }

        public virtual void DeleteColumnDataRefer(PropertyInfo property)
        {

        }

        public virtual ObservableCollection<ComboboxItem> LoadComboboxData(PropertyInfo property)
        {
            return new ObservableCollection<ComboboxItem>();
        }

        public virtual void BeForeRefresh()
        {
        }

        public virtual void OnDelete(object obj)
        {
            sourceVM.SelectedItem.IsDeleted = !sourceVM.SelectedItem.IsDeleted;
            var Items = sourceVM.Items;
            var SelectedItem = sourceVM.SelectedItem;
            if (sourceVM.SelectedItem.IsDeleted)
            {
                DataGrid dgdData = obj as DataGrid;
                this.CancelEditData(dgdData);
                OnCustomValueIndex(SelectedItem, Items, false);
            }
            else
            {
                OnCustomValueIndex(SelectedItem, Items, true);
            }
        }

        public void OnViewDetail()
        {
            GenericControlCustomDetailViewModel genericControlCustomDetailViewModel = new GenericControlCustomDetailViewModel(sourceVM.SelectedItem)
            {
                Title = sourceVM.Title,
                Description = sourceVM.SelectedItem?.DetailInfoModalTitle
            };
            CustomViewDetailModelInfo(genericControlCustomDetailViewModel);
            GenericControlCustomViewDetail genericControlCustomViewDetail = new GenericControlCustomViewDetail()
            {
                DataContext = genericControlCustomDetailViewModel
            };
            var dialog = DialogHost.Show(genericControlCustomViewDetail, "RootDialog");
        }

        public virtual void CustomViewDetailModelInfo(GenericControlCustomDetailViewModel genericControlCustomDetailViewModel)
        {
            genericControlCustomDetailViewModel.ColumnWidth = 260;
        }

        public virtual void BeforeSave()
        {

        }

        public virtual bool validate()
        {
            return true;
        }
        public virtual bool ValidateField(IEnumerable<TModel> models)
        {
            foreach (TModel model in models)
            {
                if (!ValidateViewModelHelper.Validate(model))
                    return false;
            }
            return true;
        }

        public virtual bool IsDisableColumn(PropertyInfo property)
        {
            return false;
        }

        public virtual void CustomGenerateDatagridTextColumn(MaterialDesignThemes.Wpf.DataGridTextColumn col, PropertyInfo property)
        {

        }

        public virtual void CustomGenerateComboboxColumn(MaterialDesignThemes.Wpf.DataGridComboBoxColumn col, PropertyInfo property)
        {

        }
        public virtual void OnRefreshWithOutReload(object obj)
        {
            OnRefresh(obj);
        }

    }
}