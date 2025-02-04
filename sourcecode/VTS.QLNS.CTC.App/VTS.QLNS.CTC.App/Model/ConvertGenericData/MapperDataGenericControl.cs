using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.ViewModel;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.ConvertGenericData
{
    // lưu ý không nên đặt tên các method giống nhau vì các method này sử dụng qua reflection 
    public class MapperDataGenericControl
    {
        private readonly ISysAuthorityService _sysAuthorityService;
        private readonly IMapper _mapper;

        public MapperDataGenericControl(ISysAuthorityService sysAuthorityService, IMapper mapper)
        {
            _sysAuthorityService = sysAuthorityService;
            _mapper = mapper;
        }

        public void ConvertDonViToPhongBan(IEnumerable<DonViModel> source, NSPhongBanModel desnitaion)
        {
            desnitaion.IIDMaBQuanLy = string.Join(", ", source.Select(s => s.IIDMaDonVi));
            // desnitaion.TenDonVi = string.Join(", ", source.Select(s => s.TenDonVi));
        }

        public void ConvertDonViToDonVi(DonViModel source, DonViModel desnitaion)
        {
            desnitaion.IdParent = source.Id;
            desnitaion.ParentName = source.TenDonVi;
        }

        public void SetSelectedDonVi(GenericControlCustomViewModel<DonViModel, DonVi, NsDonViService> source, GenericControlCustomViewModel<DonViModel, DonVi, NsDonViService> dialog)
        {
            dialog.SelectedItem = dialog.Items.Where(i => i.Id.Equals(source.SelectedItem.IdParent)).FirstOrDefault();
        }

        public void SetMultipleSelectedDonvis(GenericControlCustomViewModel<NSPhongBanModel, DmBQuanLy, NsPhongBanService> source, GenericControlCustomViewModel<DonViModel, DonVi, NsDonViService> dialog)
        {
            /*IEnumerable<string> idDonVis = source.SelectedItem.IdDonVi.Split(",").ToList();
            foreach (var model in dialog.Items)
            {
                if (idDonVis.Contains(model.IdDonVi))
                {
                    model.IsSelected = true;
                }
            }*/
        }

        public ObservableCollection<ComboboxItem> LoadAllTrangThaiMucLuc()
        {
            return new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem { DisplayItem = "Không sử dụng", ValueItem = "0" },
                new ComboboxItem { DisplayItem = "Đang sử dụng", ValueItem = "1" }
            };
        }

        public void ConvertSktMucLucToSktMucLuc(SktMucLucModel source, SktMucLucModel desnitaion)
        {
            desnitaion.IIDMLSKTCha = source.IIDMLSKT;
            desnitaion.KyHieuCha = source.SKyHieu;
        }

        public void SetSelectedSktMucLuc(GenericControlCustomViewModel<SktMucLucModel, NsSktMucLuc, SktMucLucService> source, GenericControlCustomViewModel<SktMucLucModel, NsSktMucLuc, SktMucLucService> dialog)
        {
            dialog.SelectedItem = dialog.Items.Where(i => i.IIDMLSKT.Equals(source.SelectedItem.IIDMLSKTCha)).FirstOrDefault();
        }

        public ObservableCollection<ComboboxItem> LoadAllLoaiDonVi()
        {
            return new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem { DisplayItem = "", ValueItem = "0" },
                new ComboboxItem { DisplayItem = "Nội bộ", ValueItem = "1" },
                new ComboboxItem { DisplayItem = "Toàn quân", ValueItem = "2" }
            };
        }

        public ObservableCollection<ComboboxItem> LoadAllTrangThaiDonVi()
        {
            return new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem { DisplayItem = "Không sử dụng", ValueItem = "0" },
                new ComboboxItem { DisplayItem = "Đang sử dụng", ValueItem = "1" }
            };
        }

        public ObservableCollection<ComboboxItem> LoadAllTrangThaiNguonNganSach()
        {
            return new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem { DisplayItem = "Không sử dụng", ValueItem = "0" },
                new ComboboxItem { DisplayItem = "Đang sử dụng", ValueItem = "1" }
            };
        }

        public ObservableCollection<ComboboxItem> LoadAllTrangThaiMLNS()
        {
            return new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem { DisplayItem = "Không sử dụng", ValueItem = "0" },
                new ComboboxItem { DisplayItem = "Đang sử dụng", ValueItem = "1" }
            };
        }

        public void ConvertDMNhonNganhToSktMucLuc(DanhMucNhomNganhModel source, SktMucLucModel destination)
        {
            destination.SNGCha = source.IIDMaDanhMuc;
        }

        public void SetSelectedNhomNganh(GenericControlCustomViewModel<SktMucLucModel, NsSktMucLuc, SktMucLucService> source, GenericControlCustomViewModel<DanhMucNhomNganhModel, DanhMuc, DanhMucNhomNganhService> dialog)
        {
            dialog.SelectedItem = dialog.Items.Where(i => i.IIDMaDanhMuc.Equals(source.SelectedItem.SNGCha)).FirstOrDefault();
        }

        public void ConvertNganhToNhomNganh(IEnumerable<DanhMucNganhModel> source, DanhMucNhomNganhModel desnitaion)
        {
            desnitaion.Values = source;
            desnitaion.SGiaTri = string.Join(", ", source.Select(s => s.IIDMaDanhMuc));
        }

        public void setSelectedNganh(GenericControlCustomViewModel<DanhMucNhomNganhModel, DanhMuc, DanhMucNhomNganhService> source, GenericControlCustomViewModel<DanhMucNganhModel, DanhMuc, DanhMucNganhService> dialog)
        {
            IEnumerable<string> idDanhMuc = source.SelectedItem.SGiaTri.Split(",").Select(p => p.Trim()).ToList();
            foreach (var model in dialog.Items)
            {
                if (idDanhMuc.Contains(model.IIDMaDanhMuc))
                {
                    model.IsSelected = true;
                }
            }
        }

        public void ConvertPhongBanToMLNS(NSPhongBanModel source, CauHinhMLNSModel desnitaion)
        {
            desnitaion.IdPhongBan = source.IIDMaBQuanLy;
            desnitaion.TenPhongBan = source.STenBQuanLy;
        }

        public void SetSelectedPhongBan(GenericControlCustomViewModel<CauHinhMLNSModel, NsMucLucNganSach, CauHinhMLNSService> source, GenericControlCustomViewModel<NSPhongBanModel, DmBQuanLy, NsPhongBanService> dialog)
        {
            dialog.SelectedItem = dialog.Items.Where(i => i.IIDMaBQuanLy.Equals(source.SelectedItem.IdPhongBan)).FirstOrDefault();
        }

        public void SetSelectedMLNS(GenericControlCustomViewModel<BhDmMucLucBHXHMapModel, BhDmMucLucNganSach, BhDmMucLucBHXHMapService> source, GenericControlCustomViewModel<NsMuclucNgansachModel, NsMucLucNganSach, NsMucLucNganSachService> dialog)
        {

            dialog.SelectedItem = dialog.Items.Where(i => i.XauNoiMa.Equals(source.SelectedItem.SNS_LuongChinh)).FirstOrDefault();
        }

        public ObservableCollection<ComboboxItem> LoadAllKhoiDonVi()
        {
            return new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem { DisplayItem = "", ValueItem = "0" },
                new ComboboxItem { DisplayItem = "Khối doanh nghiệp", ValueItem = "1" },
                new ComboboxItem { DisplayItem = "Khối dự toán", ValueItem = "2" }
            };
        }

        public void ConvertDmLoaiCongTrinhToDmLoaiCongTrinh(VdtDmLoaiCongTrinhModel source, VdtDmLoaiCongTrinhModel desnitaion)
        {
            desnitaion.IIdParent = source.IIdLoaiCongTrinh;
            desnitaion.Parent = source;
        }

        public void SetSelectedLoaiCongTrinh(GenericControlCustomViewModel<VdtDmLoaiCongTrinhModel, VdtDmLoaiCongTrinh, DmLoaiCongTrinhService> source, GenericControlCustomViewModel<VdtDmLoaiCongTrinhModel, VdtDmLoaiCongTrinh, DmLoaiCongTrinhService> dialog)
        {
            dialog.SelectedItem = dialog.Items.Where(i => i.IIdLoaiCongTrinh.Equals(source.SelectedItem.IIdParent)).FirstOrDefault();
        }

        public ObservableCollection<ComboboxItem> LoadAllCauHinhCanCuModule()
        {
            return new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem { DisplayItem = "Số nhu cầu", ValueItem = TypeModuleCanCu.DEMAND },
                //new ComboboxItem { DisplayItem = "Phân bổ số kiểm tra", ValueItem = TypeModuleCanCu.DISTRIBUTION },
                new ComboboxItem { DisplayItem = "Dự toán đầu năm", ValueItem = TypeModuleCanCu.PLAN_BEGIN_YEAR }
            };
        }

        public ObservableCollection<ComboboxItem> LoadAllCauHinhCanCu()
        {
            return new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem { DisplayItem = "Dự toán", ValueItem = "BUDGET_ESTIMATE" },
                new ComboboxItem { DisplayItem = "Quyết toán", ValueItem = "BUDGET_SETTLEMENT" },
                new ComboboxItem { DisplayItem = "Cấp phát", ValueItem = "BUDGET_ALLOCATION" },
                new ComboboxItem { DisplayItem = "Số nhu cầu", ValueItem = "BUDGET_DEMANDCHECK_DEMAND" },
                new ComboboxItem { DisplayItem = "Số kiểm tra", ValueItem = "BUDGET_DEMANDCHECK_CHECK" }
            };
        }

        public ObservableCollection<ComboboxItem> LoadAllCauHinhCanCuThietLap()
        {
            return new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem { DisplayItem = "Theo CT", ValueItem = "1" },
                new ComboboxItem { DisplayItem = "Nhiều chứng từ", ValueItem = "2" },
                new ComboboxItem { DisplayItem = "Lũy kế đến 1 chứng từ", ValueItem = "3" },
            };
        }

        public ObservableCollection<ComboboxItem> LoadAllTrangNgNganSach()
        {
            return new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem { DisplayItem = "", ValueItem = "" },
                new ComboboxItem { DisplayItem = "NG", ValueItem = "NG" },
                new ComboboxItem { DisplayItem = "TNG", ValueItem = "TNG" },
                new ComboboxItem { DisplayItem = "TNG1", ValueItem = "TNG1" },
                new ComboboxItem { DisplayItem = "TNG2", ValueItem = "TNG2" },
                new ComboboxItem { DisplayItem = "TNG3", ValueItem = "TNG3" }
            };
        }

        public ObservableCollection<ComboboxItem> LoadAllLoaiNganSach()
        {
            return new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem { DisplayItem = "Thường xuyên", ValueItem = "0" },
                new ComboboxItem { DisplayItem = "Nghiệp vụ", ValueItem = "1" },
                new ComboboxItem { DisplayItem = "NSNN", ValueItem = "2" },
                new ComboboxItem { DisplayItem = "Kinh phí khác", ValueItem = "3" },
                new ComboboxItem { DisplayItem = "Quốc phòng khác", ValueItem = "4" },
                new ComboboxItem { DisplayItem = "Nhà nước khác", ValueItem = "5" }
            };
        }

        public void MapNsDonViToDmNganhDonvi(DonViModel source, DanhMucNganhModel destination)
        {
            destination.SGiaTri = source.IIDMaDonVi;
            destination.TenDonViNoiBo = source.TenDonVi;
        }

        public void SetSelectedDonViOfDanhMucNganh(GenericControlCustomViewModel<DanhMucNganhModel, DanhMuc, DanhMucNganhService> source, GenericControlCustomViewModel<DonViModel, DonVi, NsDonViService> dialog)
        {
            dialog.SelectedItem = dialog.Items.Where(i => i.IIDMaDonVi.Equals(source.SelectedItem.SGiaTri)).FirstOrDefault();
        }

        public ObservableCollection<ComboboxItem> LoadAllMLNSNhapTheoTruong()
        {
            return new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem { DisplayItem = "", ValueItem = null },
                new ComboboxItem { DisplayItem = "sLNS", ValueItem = "sLNS" },
                new ComboboxItem { DisplayItem = "sM", ValueItem = "sM" },
                new ComboboxItem { DisplayItem = "sTM", ValueItem = "sTM" },
            };
        }

        public void MapMLNSToMLNSOfSktMucLuc(IEnumerable<NsMuclucNgansachModel> source, SktMucLucModel desnitaion)
        {
            desnitaion.MLNS = string.Join("; ", source.Select(t => t.XauNoiMa));
            List<NsMlsktMlns> sktMucLucMaps = source.Select(model => new NsMlsktMlns { SNsXauNoiMa = model.XauNoiMa }).ToList();
            desnitaion.SktMucLucMaps = sktMucLucMaps;
        }

        public void SetSelectedMLNSOfSktMucLuc(GenericControlCustomViewModel<SktMucLucModel, NsSktMucLuc, SktMucLucService> source, GenericControlCustomViewModel<NsMuclucNgansachModel, NsMucLucNganSach, MucLucNganSachService> dialog)
        {
            IEnumerable<string> nsXauNoima = source.SelectedItem.SktMucLucMaps.Select(t => t.SNsXauNoiMa);
            foreach (var model in dialog.Items)
            {
                if (nsXauNoima.Contains(model.XauNoiMa))
                {
                    model.IsSelected = true;
                }
            }
        }

        public void MapDanhMucChuyenNganhToDanhMucChuyenNganhDonvi(IEnumerable<DanhMucNganhModel> source, DonViModel destination)
        {
            destination.DanhMucChuyenNganh = source.ToList();
            destination.TenDanhMuc = string.Join(", ", source.Select(s => s.STen));
        }

        public void SetSelecteDanhMucChuyenNganhOfNsDonVi(GenericControlCustomViewModel<DonViModel, DonVi, NsDonViService> source, GenericControlCustomViewModel<DanhMucNganhModel, DanhMuc, DanhMucNganhService> dialog)
        {
            IEnumerable<string> DanhMucNganhModelIdCodes = source.SelectedItem.DanhMucChuyenNganh.Select(t => t.IIDMaDanhMuc);
            foreach (var model in dialog.Items)
            {
                if (DanhMucNganhModelIdCodes.Contains(model.IIDMaDanhMuc))
                {
                    model.IsSelected = true;
                }
            }
        }

        public void MapLnsToLnsOfDonvi(IEnumerable<NsMuclucNgansachModel> source, DonViModel destination)
        {
            destination.LNS = source.ToList();
            destination.TenLNS = string.Join(", ", source.Select(s => s.Lns));
        }

        public void SetSelecteLnsOfNsDonVi(GenericControlCustomViewModel<DonViModel, DonVi, NsDonViService> source, GenericControlCustomViewModel<NsMuclucNgansachModel, NsMucLucNganSach, MucLucNganSachService> dialog)
        {
            IEnumerable<string> lns = source.SelectedItem.LNS.Select(t => t.Lns);
            foreach (var model in dialog.Items)
            {
                if (lns.Contains(model.Lns))
                {
                    model.IsSelected = true;
                }
            }
        }

        public void MapAuthorityToSysFunctionAuthority(IEnumerable<HTQuyenModel> source, HTChucNangModel destination)
        {
            destination.HTQuyens = source.ToList();
            destination.SysAuthoritiesName = string.Join(", ", source.Select(s => s.IIDMaQuyen));
        }

        public void SetSelecteAuthorityOfSysFunction(GenericControlCustomViewModel<HTChucNangModel, HtChucNang, SysFunctionService> source, GenericControlCustomViewModel<HTQuyenModel, HtQuyen, AuthorityService> dialog)
        {
            IEnumerable<string> sysAuthoritiesName = source.SelectedItem.HTQuyens.Select(t => t.IIDMaQuyen);
            foreach (var model in dialog.Items)
            {
                if (sysAuthoritiesName.Contains(model.IIDMaQuyen))
                {
                    model.IsSelected = true;
                }
            }
        }

        public void MapDmChuDauTuParentToChuDauTu(DmChuDauTuModel source, DmChuDauTuModel desnitaion)
        {
            desnitaion.IIDDonViCha = source.Id;
            desnitaion.TenCdtParent = source.STenDonVi;
        }

        public void SetSelectedDmChuDauTu(GenericControlCustomViewModel<DmChuDauTuModel, DmChuDauTu, DmChuDauTuService> source, GenericControlCustomViewModel<DmChuDauTuModel, DmChuDauTu, DmChuDauTuService> dialog)
        {
            dialog.SelectedItem = dialog.Items.Where(i => i.Id.Equals(source.SelectedItem.IIDDonViCha)).FirstOrDefault();
        }

        public void MapPhuCapChaToPhuCap(TlDmPhuCapModel source, TlDmPhuCapModel destination)
        {
            destination.Parent = source.MaPhuCap;
        }
        public void MapPhuCapChaToPhuCapNq104(TlDmPhuCapNq104Model source, TlDmPhuCapNq104Model destination)
        {
            destination.Parent = source.MaPhuCap;
        }

        //public void MapPhuCapKemTheoToPhuCap(TlDmPhuCapModel source, TlDmPhuCapModel destination)
        //{
        //    destination.IIdMaPhuCapKemTheo = source.MaPhuCap;
        //    destination.IIdPhuCapKemTheo = source.Id;
        //}

        public void SetSelectePhuCapChaOfPhuCap(GenericControlCustomViewModel<TlDmPhuCapModel, TlDmPhuCap, TlDmPhuCapService> source, GenericControlCustomViewModel<TlDmPhuCapModel, TlDmPhuCap, TlDmPhuCapService> dialog)
        {
            dialog.SelectedItem = dialog.Items.Where(i => i.MaPhuCap.Equals(source.SelectedItem.Parent)).FirstOrDefault();
        }
        public void SetSelectePhuCapChaOfPhuCapNq104(GenericControlCustomViewModel<TlDmPhuCapNq104Model, TlDmPhuCapNq104, TlDmPhuCapNq104Service> source, GenericControlCustomViewModel<TlDmPhuCapNq104Model, TlDmPhuCapNq104, TlDmPhuCapNq104Service> dialog)
        {
            dialog.SelectedItem = dialog.Items.Where(i => i.MaPhuCap.Equals(source.SelectedItem.Parent)).FirstOrDefault();
        }

        //public void SetSelectedPhuCapKemTheoOfPhuCap(GenericControlCustomViewModel<TlDmPhuCapModel, TlDmPhuCap, TlDmPhuCapService> source, GenericControlCustomViewModel<TlDmPhuCapModel, TlDmPhuCap, TlDmPhuCapService> dialog)
        //{
        //    dialog.SelectedItem = dialog.Items.Where(i => i.Id.Equals(source.SelectedItem.IIdPhuCapKemTheo)).FirstOrDefault();
        //}

        public void MapFunctionToSysAuthorityFunction(IEnumerable<HTChucNangModel> source, HTQuyenModel destination)
        {
            destination.SysFunctionModels = source.ToList();
            destination.SysFunctionName = string.Join(", ", source.Select(s => s.STenChucNang));
        }

        public void SetSelecteFunctionOfSysAuthority(GenericControlCustomViewModel<HTQuyenModel, HtQuyen, SysAuthorityService> source, GenericControlCustomViewModel<HTChucNangModel, HtChucNang, SysFunctionService> dialog)
        {
            IEnumerable<string> funcCode = source.SelectedItem.SysFunctionModels.Select(t => t.IIDMaChucNang);
            foreach (var model in dialog.Items)
            {
                if (funcCode.Contains(model.IIDMaChucNang))
                {
                    model.IsSelected = true;
                }
            }
        }

        public ObservableCollection<ComboboxItem> LoadAuthorityType()
        {
            List<HTLoaiQuyenModel> authorTypes = _mapper.Map<List<HTLoaiQuyenModel>>(_sysAuthorityService.FindAllAuthorTypes());
            return new ObservableCollection<ComboboxItem>(authorTypes.Select(t => new ComboboxItem { DisplayItem = t.STenLoaiQuyen, ValueItem = t.ID.ToString()}));
        }
    }
}
