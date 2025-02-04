using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.View.Shared;
using VTS.QLNS.CTC.App.ViewModel;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class BhDmMucLucBHXHMapModelControlService : GenericControlBaseService<BhDmMucLucBHXHMapModel, BhDmMucLucNganSach, BhDmMucLucBHXHMapService>
    {
        public override void InitDialog(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(BhDmMucLucBHXHMapModel.TenSNSLuongChinh)) || property.Name.Equals(nameof(BhDmMucLucBHXHMapModel.TenSNSPCCV)) || property.Name.Equals(nameof(BhDmMucLucBHXHMapModel.TenSNSPCTN)) || property.Name.Equals(nameof(BhDmMucLucBHXHMapModel.TenSNSPCTNVK)) || property.Name.Equals(nameof(BhDmMucLucBHXHMapModel.TenNSHSBL)))
            {
                var nsmlnsService = sourceVM._serviceProvider.GetService(typeof(IMucLucNganSachService));
                GenericControlCustomViewModel<NsMucLucNgansachMapModel, NsMucLucNganSach, MucLucNganSachService> dialogVM = new GenericControlCustomViewModel<NsMucLucNgansachMapModel, NsMucLucNganSach, MucLucNganSachService>
                    ((MucLucNganSachService)nsmlnsService, sourceVM._mapper, sourceVM._sessionService, sourceVM._serviceProvider);
                var itemChosen = "";
                if (property.Name.Equals(nameof(BhDmMucLucBHXHMapModel.TenSNSLuongChinh)))
                {
                    itemChosen += string.Join(",", sourceVM.Items.Where(x=>!string.IsNullOrEmpty(x.SNS_PCCV)).Select(t => t.SNS_PCCV));
                    itemChosen += "," + string.Join(",", sourceVM.Items.Where(x => !string.IsNullOrEmpty(x.SNS_PCTN)).Select(t => t.SNS_PCTN));
                    itemChosen += "," + string.Join(",", sourceVM.Items.Where(x => !string.IsNullOrEmpty(x.SNS_PCTNVK)).Select(t => t.SNS_PCTNVK));
                    itemChosen += "," + string.Join(",", sourceVM.Items.Where(x => !string.IsNullOrEmpty(x.SNS_HSBL)).Select(t => t.SNS_HSBL));
                } else if (property.Name.Equals(nameof(BhDmMucLucBHXHMapModel.TenSNSPCCV)))
                {
                    itemChosen += string.Join(",", sourceVM.Items.Where(x => !string.IsNullOrEmpty(x.SNS_LuongChinh)).Select(t => t.SNS_LuongChinh));
                    itemChosen += "," + string.Join(",", sourceVM.Items.Where(x => !string.IsNullOrEmpty(x.SNS_PCTN)).Select(t => t.SNS_PCTN));
                    itemChosen += "," + string.Join(",", sourceVM.Items.Where(x => !string.IsNullOrEmpty(x.SNS_PCTNVK)).Select(t => t.SNS_PCTNVK));
                    itemChosen += "," + string.Join(",", sourceVM.Items.Where(x => !string.IsNullOrEmpty(x.SNS_HSBL)).Select(t => t.SNS_HSBL));
                }
                else if (property.Name.Equals(nameof(BhDmMucLucBHXHMapModel.TenSNSPCTN)))
                {
                    itemChosen += string.Join(",", sourceVM.Items.Where(x => !string.IsNullOrEmpty(x.SNS_PCCV)).Select(t => t.SNS_PCCV));
                    itemChosen += "," + string.Join(",", sourceVM.Items.Where(x => !string.IsNullOrEmpty(x.SNS_LuongChinh)).Select(t => t.SNS_LuongChinh));
                    itemChosen += "," + string.Join(",", sourceVM.Items.Where(x => !string.IsNullOrEmpty(x.SNS_PCTNVK)).Select(t => t.SNS_PCTNVK));
                    itemChosen += "," + string.Join(",", sourceVM.Items.Where(x => !string.IsNullOrEmpty(x.SNS_HSBL)).Select(t => t.SNS_HSBL));
                }
                else if (property.Name.Equals(nameof(BhDmMucLucBHXHMapModel.TenSNSPCTNVK)))
                {
                    itemChosen += string.Join(",", sourceVM.Items.Where(x => !string.IsNullOrEmpty(x.SNS_PCCV)).Select(t => t.SNS_PCCV));
                    itemChosen += "," + string.Join(",", sourceVM.Items.Where(x => !string.IsNullOrEmpty(x.SNS_PCTN)).Select(t => t.SNS_PCTN));
                    itemChosen += "," + string.Join(",", sourceVM.Items.Where(x => !string.IsNullOrEmpty(x.SNS_LuongChinh)).Select(t => t.SNS_LuongChinh));
                    itemChosen += "," + string.Join(",", sourceVM.Items.Where(x => !string.IsNullOrEmpty(x.SNS_HSBL)).Select(t => t.SNS_HSBL));
                }
                else if (property.Name.Equals(nameof(BhDmMucLucBHXHMapModel.TenNSHSBL)))
                {
                    itemChosen += string.Join(",", sourceVM.Items.Where(x => !string.IsNullOrEmpty(x.SNS_PCCV)).Select(t => t.SNS_PCCV));
                    itemChosen += "," + string.Join(",", sourceVM.Items.Where(x => !string.IsNullOrEmpty(x.SNS_PCTN)).Select(t => t.SNS_PCTN));
                    itemChosen += "," + string.Join(",", sourceVM.Items.Where(x => !string.IsNullOrEmpty(x.SNS_LuongChinh)).Select(t => t.SNS_LuongChinh));
                    itemChosen += "," + string.Join(",", sourceVM.Items.Where(x => !string.IsNullOrEmpty(x.SNS_PCTNVK)).Select(t => t.SNS_PCTNVK));
                }
                dialogVM._authenticationInfo.OptionalParam = new object[] { DialogType.LoadMLNSOfBHXH, property.Name, sourceVM.SelectedItem.SXauNoiMa, itemChosen };
                dialogVM.IsDialog = true;
                dialogVM.Description = "Danh mục mục lục ngân sách";
                dialogVM.Title = "Danh mục mục lục ngân sách";
                GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(dialogVM);
                dialogVM.IsMultipleSelect = true;
                dialogVM.IsVisibleFilterByMlnsMappingType = true;
                SetSelectedMLNS(dialogVM, property);
                GenericControlCustomWindow GenericControlCustomWindow = new GenericControlCustomWindow
                {
                    DataContext = genericControlCustomWindowViewModel
                };
                GenericControlCustomWindow.SavedAction = obj =>
                {

                    IEnumerable<NsMucLucNgansachMapModel> data = obj as IEnumerable<NsMucLucNgansachMapModel>;
                    // chỉ lưu mlns có bhangchadutoan = 0 (false)
                    switch(property.Name)
                    {
                        case nameof(BhDmMucLucBHXHMapModel.TenSNSLuongChinh):
                            sourceVM.SelectedItem.SNS_LuongChinh = string.Join(",", data.Where(x=>!x.BHangCha).Select(t => t.XauNoiMa));
                            sourceVM.SelectedItem.TenSNSLuongChinh = string.Join(";", data.Where(x => !x.BHangCha).Select(t => t.MoTa));
                            break;
                        case nameof(BhDmMucLucBHXHMapModel.TenSNSPCCV):
                            sourceVM.SelectedItem.SNS_PCCV = string.Join(",", data.Where(x => !x.BHangCha).Select(t => t.XauNoiMa));
                            sourceVM.SelectedItem.TenSNSPCCV = string.Join("; ", data.Where(x => !x.BHangCha).Select(t => t.MoTa));
                            break;
                        case nameof(BhDmMucLucBHXHMapModel.TenSNSPCTN):
                            sourceVM.SelectedItem.SNS_PCTN = string.Join(",", data.Where(x => !x.BHangCha).Select(t => t.XauNoiMa));
                            sourceVM.SelectedItem.TenSNSPCTN = string.Join("; ", data.Where(x => !x.BHangCha).Select(t => t.MoTa));
                            break;
                        case nameof(BhDmMucLucBHXHMapModel.TenSNSPCTNVK):
                            sourceVM.SelectedItem.SNS_PCTNVK = string.Join(",", data.Where(x => !x.BHangCha).Select(t => t.XauNoiMa));
                            sourceVM.SelectedItem.TenSNSPCTNVK = string.Join("; ", data.Where(x => !x.BHangCha).Select(t => t.MoTa));
                            break;
                        case nameof(BhDmMucLucBHXHMapModel.TenNSHSBL):
                            sourceVM.SelectedItem.SNS_HSBL = string.Join(",", data.Where(x => !x.BHangCha).Select(t => t.XauNoiMa));
                            sourceVM.SelectedItem.TenNSHSBL = string.Join("; ", data.Where(x => !x.BHangCha).Select(t => t.MoTa));
                            break;
                        default:
                            break;
                    }
                    GenericControlCustomWindow.Close();
                };
                dialogVM.GenericControlCustomWindow = GenericControlCustomWindow;
                GenericControlCustomWindow.Show();
            }
            else if (property.Name.Equals(nameof(BhDmMucLucBHXHMapModel.TenSLuongChinh)) || property.Name.Equals(nameof(BhDmMucLucBHXHMapModel.TenSPCCV)) || property.Name.Equals(nameof(BhDmMucLucBHXHMapModel.TenSPCTN)) || property.Name.Equals(nameof(BhDmMucLucBHXHMapModel.TenSPCTNVK)))
            {
                var tldmpcService = sourceVM._serviceProvider.GetService(typeof(ITlDmPhuCapService));
                GenericControlCustomViewModel<TlDmPhuCapMapModel, TlDmPhuCap, TlDmPhuCapService> dialogVM = new GenericControlCustomViewModel<TlDmPhuCapMapModel, TlDmPhuCap, TlDmPhuCapService>
                    ((TlDmPhuCapService)tldmpcService, sourceVM._mapper, sourceVM._sessionService, sourceVM._serviceProvider);
                var itemChosen = "";
                if (property.Name.Equals(nameof(BhDmMucLucBHXHMapModel.TenSLuongChinh)))
                {
                    itemChosen += string.Join(",", sourceVM.Items.Where(x => !string.IsNullOrEmpty(x.SPCCV)).Select(t => t.SPCCV));
                    itemChosen += "," + string.Join(",", sourceVM.Items.Where(x => !string.IsNullOrEmpty(x.SPCTN)).Select(t => t.SPCTN));
                    itemChosen += "," + string.Join(",", sourceVM.Items.Where(x => !string.IsNullOrEmpty(x.SPCTNVK)).Select(t => t.SPCTNVK));
                }
                else if (property.Name.Equals(nameof(BhDmMucLucBHXHMapModel.TenSPCCV)))
                {
                    itemChosen += string.Join(",", sourceVM.Items.Where(x => !string.IsNullOrEmpty(x.SLuongChinh)).Select(t => t.SLuongChinh));
                    itemChosen += "," + string.Join(",", sourceVM.Items.Where(x => !string.IsNullOrEmpty(x.SPCTN)).Select(t => t.SPCTN));
                    itemChosen += "," + string.Join(",", sourceVM.Items.Where(x => !string.IsNullOrEmpty(x.SPCTNVK)).Select(t => t.SPCTNVK));
                }
                else if (property.Name.Equals(nameof(BhDmMucLucBHXHMapModel.TenSPCTN)))
                {
                    itemChosen += string.Join(",", sourceVM.Items.Where(x => !string.IsNullOrEmpty(x.SPCCV)).Select(t => t.SPCCV));
                    itemChosen += "," + string.Join(",", sourceVM.Items.Where(x => !string.IsNullOrEmpty(x.SLuongChinh)).Select(t => t.SLuongChinh));
                    itemChosen += "," + string.Join(",", sourceVM.Items.Where(x => !string.IsNullOrEmpty(x.SPCTNVK)).Select(t => t.SPCTNVK));
                }
                else if (property.Name.Equals(nameof(BhDmMucLucBHXHMapModel.TenSPCTNVK)))
                {
                    itemChosen += string.Join(",", sourceVM.Items.Where(x => !string.IsNullOrEmpty(x.SPCCV)).Select(t => t.SPCCV));
                    itemChosen += "," + string.Join(",", sourceVM.Items.Where(x => !string.IsNullOrEmpty(x.SPCTN)).Select(t => t.SPCTN));
                    itemChosen += "," + string.Join(",", sourceVM.Items.Where(x => !string.IsNullOrEmpty(x.SLuongChinh)).Select(t => t.SLuongChinh));
                }
                dialogVM._authenticationInfo.OptionalParam = new object[] { property.Name, sourceVM.SelectedItem.SXauNoiMa, itemChosen };
                dialogVM.IsDialog = true;
                dialogVM.Description = "Danh mục phụ cấp lương";
                dialogVM.Title = "Danh mục phụ cấp lương";
                GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(dialogVM);
                dialogVM.IsMultipleSelect = true;
                dialogVM.IsVisibleFilterByMlnsMappingType = true;
                SetSelectedPhuCap(dialogVM, property);
                GenericControlCustomWindow GenericControlCustomWindow = new GenericControlCustomWindow
                {
                    DataContext = genericControlCustomWindowViewModel
                };
                GenericControlCustomWindow.SavedAction = obj =>
                {
                    IEnumerable<TlDmPhuCapMapModel> data = obj as IEnumerable<TlDmPhuCapMapModel>;
                    // chỉ lưu mlns có bhangchadutoan = 0 (false)
                    switch (property.Name)
                    {
                        case nameof(BhDmMucLucBHXHMapModel.TenSLuongChinh):
                            sourceVM.SelectedItem.SLuongChinh = string.Join(",", data.Select(t => t.MaPhuCap));
                            sourceVM.SelectedItem.TenSLuongChinh = string.Join("; ", data.Select(t => t.TenPhuCap));
                            break;
                        case nameof(BhDmMucLucBHXHMapModel.TenSPCCV):
                            sourceVM.SelectedItem.SPCCV = string.Join(",", data.Select(t => t.MaPhuCap));
                            sourceVM.SelectedItem.TenSPCCV = string.Join("; ", data.Select(t => t.TenPhuCap));
                            break;
                        case nameof(BhDmMucLucBHXHMapModel.TenSPCTN):
                            sourceVM.SelectedItem.SPCTN = string.Join(",", data.Select(t => t.MaPhuCap));
                            sourceVM.SelectedItem.TenSPCTN = string.Join("; ", data.Select(t => t.TenPhuCap));
                            break;
                        case nameof(BhDmMucLucBHXHMapModel.TenSPCTNVK):
                            sourceVM.SelectedItem.SPCTNVK = string.Join(",", data.Select(t => t.MaPhuCap));
                            sourceVM.SelectedItem.TenSPCTNVK = string.Join("; ", data.Select(t => t.TenPhuCap));
                            break;                       
                        default:
                            break;
                    }
                    GenericControlCustomWindow.Close();
                };
                dialogVM.GenericControlCustomWindow = GenericControlCustomWindow;
                GenericControlCustomWindow.Show();
            }
        }

        private void SetSelectedMLNS(GenericControlCustomViewModel<NsMucLucNgansachMapModel, NsMucLucNganSach, MucLucNganSachService> dialogVM, PropertyInfo property)
        {
            dialogVM.AfterSelectAll = true;
            string allXNm = string.Empty;
            if (property.Name.Equals(nameof(BhDmMucLucBHXHMapModel.TenSNSLuongChinh)) && !string.IsNullOrEmpty(sourceVM.SelectedItem.SNS_LuongChinh))
            {
                allXNm = sourceVM.SelectedItem.SNS_LuongChinh;
            }
            else if (property.Name.Equals(nameof(BhDmMucLucBHXHMapModel.TenSNSPCCV)) && !string.IsNullOrEmpty(sourceVM.SelectedItem.SNS_PCCV))
            {
                allXNm = sourceVM.SelectedItem.SNS_PCCV;

            }
            else if (property.Name.Equals(nameof(BhDmMucLucBHXHMapModel.TenSNSPCTN)) && !string.IsNullOrEmpty(sourceVM.SelectedItem.SNS_PCTN))
            {
                allXNm = sourceVM.SelectedItem.SNS_PCTN;

            }
            else if (property.Name.Equals(nameof(BhDmMucLucBHXHMapModel.TenSNSPCTNVK)) && !string.IsNullOrEmpty(sourceVM.SelectedItem.SNS_PCTNVK))
            {
                allXNm = sourceVM.SelectedItem.SNS_PCTNVK;
            }
            else if (property.Name.Equals(nameof(BhDmMucLucBHXHMapModel.TenNSHSBL)) && !string.IsNullOrEmpty(sourceVM.SelectedItem.SNS_HSBL))
            {
                allXNm = sourceVM.SelectedItem.SNS_HSBL;
            }
            foreach (var model in dialogVM.Items)
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
            }
            dialogVM.AfterSelectAll = false;
        }

        private void SetSelectedPhuCap(GenericControlCustomViewModel<TlDmPhuCapMapModel, TlDmPhuCap, TlDmPhuCapService> dialogVM, PropertyInfo property)
        {
            dialogVM.AfterSelectAll = true;
            string allXNm = string.Empty;
            if (property.Name.Equals(nameof(BhDmMucLucBHXHMapModel.TenSLuongChinh)) && !string.IsNullOrEmpty(sourceVM.SelectedItem.SLuongChinh))
            {
                allXNm = sourceVM.SelectedItem.SLuongChinh;
            }
            else if (property.Name.Equals(nameof(BhDmMucLucBHXHMapModel.TenSPCCV)) && !string.IsNullOrEmpty(sourceVM.SelectedItem.SPCCV))
            {
                allXNm = sourceVM.SelectedItem.SPCCV;

            }
            else if (property.Name.Equals(nameof(BhDmMucLucBHXHMapModel.TenSPCTN)) && !string.IsNullOrEmpty(sourceVM.SelectedItem.SPCTN))
            {
                allXNm = sourceVM.SelectedItem.SPCTN;

            }
            else if (property.Name.Equals(nameof(BhDmMucLucBHXHMapModel.TenSPCTNVK)) && !string.IsNullOrEmpty(sourceVM.SelectedItem.SPCTNVK))
            {
                allXNm = sourceVM.SelectedItem.SPCTNVK;
            }
            foreach (var model in dialogVM.Items)
            {
                if (allXNm.Contains(model.MaPhuCap))
                {
                    model.IsSelected = true;
                }
            }
            dialogVM.AfterSelectAll = false;
        }
    }
}
