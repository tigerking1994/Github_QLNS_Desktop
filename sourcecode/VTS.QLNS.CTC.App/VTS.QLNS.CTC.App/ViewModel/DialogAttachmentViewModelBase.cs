using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel
{
    public class DialogAttachmentViewModelBase<T> : DialogViewModelBase<T> where T : ModelBase
    {
        protected readonly IMapper _mapper;
        protected readonly IStorageServiceFactory _storageServiceFactory;
        protected readonly IAttachmentService _attachService;

        public bool BIsEnable => !BIsReadOnly;
        public Guid? IdDieuChinh { get; set; }
        public virtual AttachmentEnum.Type ModuleType { get; }
        public virtual bool IsShowCanCu => ModuleType == AttachmentEnum.Type.VDT_DENGHI_THANHTOAN;

        private bool _bIsReadOnly;
        public virtual bool BIsReadOnly
        {
            get => _bIsReadOnly;
            set => SetProperty(ref _bIsReadOnly, value);
        }
        private bool _bIsReadOnlyButtonAdd;
        public virtual bool BIsReadOnlyButtonAdd
        {
            get => _bIsReadOnlyButtonAdd;
            set => SetProperty(ref _bIsReadOnlyButtonAdd, value);
        }

        private bool _isDieuChinh;
        public virtual bool IsDieuChinh
        {
            get => _isDieuChinh;
            set
            {
                if (SetProperty(ref _isDieuChinh, value))
                {
                    OnIsDieuChinhChanged();
                }
            }
        }

        private ObservableCollection<AttachmentModel> _itemsAttachment;
        public ObservableCollection<AttachmentModel> ItemsAttachment
        {
            get => _itemsAttachment;
            set => SetProperty(ref _itemsAttachment, value);
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiCanCu;
        public ObservableCollection<ComboboxItem> ItemsLoaiCanCu
        {
            get => _itemsLoaiCanCu;
            set => SetProperty(ref _itemsLoaiCanCu, value);
        }

        private AttachmentModel _selectedAttachment;
        public AttachmentModel SelectedAttachment
        {
            get => _selectedAttachment;
            set => SetProperty(ref _selectedAttachment, value);
        }

        public RelayCommand UploadFileCommand { get; set; }
        public RelayCommand DownloadFileCommand { get; set; }
        public RelayCommand DownloadAllFileCommand { get; set; }
        public RelayCommand DeleteFileCommand { get; set; }

        public DialogAttachmentViewModelBase(
            IMapper mapper,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService)
        {
            _mapper = mapper;
            _storageServiceFactory = storageServiceFactory;
            _attachService = attachService;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            DownloadFileCommand = new RelayCommand(obj => OnDownloadFile(), obj => SelectedAttachment != null);
            DownloadAllFileCommand = new RelayCommand(obj => OnDownloadAllFile(), obj => ItemsAttachment != null && ItemsAttachment.Count > 0);
            DeleteFileCommand = new RelayCommand(obj => OnDeleteFile(), obj => SelectedAttachment != null);
        }

        public virtual void LoadAttach()
        {
            //IsShowCanCu = true;
            LoadLoaiChungTu();
            ItemsAttachment = new ObservableCollection<AttachmentModel>();
            if (Model != null && !Model.Id.IsNullOrEmpty())
            {
                LoadAttach(Model.Id);
            }
            OnPropertyChanged(nameof(IsShowCanCu));
        }

        public virtual void LoadAttach(Guid objectId)
        {
            LoadLoaiChungTu();
            var data = _attachService.FindByModuleAndObjectId((int)ModuleType, objectId);
            ItemsAttachment = _mapper.Map<ObservableCollection<AttachmentModel>>(data);
            OnPropertyChanged(nameof(ItemsAttachment));
        }

        public virtual void OnUploadFile()
        {
            var openFileDialog = new System.Windows.Forms.OpenFileDialog
            {
                Title = "Chọn tệp đính kèm",
                Multiselect = true,
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (ItemsAttachment == null) ItemsAttachment = new ObservableCollection<AttachmentModel>();
                foreach (var item in openFileDialog.FileNames)
                {
                    if (!_storageServiceFactory.Instance.ValidateMaxlength(item))
                    {
                        return;
                    }
                    ItemsAttachment.Add(new AttachmentModel()
                    {
                        FileName = Path.GetFileName(item),
                        FilePath = item,
                        IsModified = true
                    });
                }
                OnPropertyChanged(nameof(ItemsAttachment));
            }
        }

        public virtual void OnDeleteFile()
        {
            if (SelectedAttachment != null)
            {
                SelectedAttachment.IsDeleted = !SelectedAttachment.IsDeleted;
            }
        }

        public virtual void OnDownloadAllFile()
        {
            if (ItemsAttachment != null && ItemsAttachment.Count > 0)
            {
                // Dowload from storage
                _storageServiceFactory.Instance.DownloadAll(ModuleType, Model.Id);
            }
            else
            {
                // Dowload from local path
            }
        }

        public virtual void OnDownloadFile()
        {
            if (SelectedAttachment != null)
            {
                if (SelectedAttachment.Id != Guid.Empty)
                {
                    // Dowload from storage
                    _storageServiceFactory.Instance.Download(SelectedAttachment.Id);
                }
                else
                {
                    // Dowload from local path
                }
            }
            else
            {
                // Cần chọn file
            }
        }

        public virtual void SaveAttachment(Guid objectId)
        {
            try
            {
                if (ItemsAttachment != null && ItemsAttachment.Any())
                {
                    // Upload file
                    var attachsAdd = ItemsAttachment.Where(x => x.IsModified && !x.IsDeleted);
                    if (attachsAdd.Any())
                    {
                        _storageServiceFactory.Instance.Upload(ModuleType, objectId, attachsAdd);
                    }

                    // Delete file
                    var attachsDelete = ItemsAttachment.Where(x => x.IsDeleted && !x.Id.IsNullOrEmpty());
                    if (attachsDelete.Any())
                    {
                        _storageServiceFactory.Instance.Remove(attachsDelete.Select(x => x.Id).ToList());
                    }

                    // Điều chỉnh
                    if (IsDieuChinh)
                    {
                        // Renew id
                        var attachsClone = ItemsAttachment.Where(x => !x.IsModified && !x.IsDeleted).ToList();
                        _storageServiceFactory.Instance.Copy(ModuleType, objectId, attachsClone);
                    }

                    // Reset state when save complete
                    LoadAttach(objectId);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public virtual void RefreshAttactment()
        {
            List<AttachmentModel> refreshItems = _itemsAttachment.Where(x => !x.IsDeleted).Select(x => { x.IsModified = false; return x; }).ToList();
            ItemsAttachment = new ObservableCollection<AttachmentModel>(refreshItems);
        }

        public virtual void ResetAttachment()
        {
            ItemsAttachment.Clear();
        }

        public virtual void OnIsDieuChinhChanged()
        {

        }

        private void LoadLoaiChungTu()
        {
            List<ComboboxItem> items = new List<ComboboxItem>();
            items.Add(new ComboboxItem() { DisplayItem = ATTACH_TYPE.TypeName.BANG_KHOI_LUONG, ValueItem = ((int)ATTACH_TYPE.TypeValue.BANG_KHOI_LUONG).ToString() });
            items.Add(new ComboboxItem() { DisplayItem = ATTACH_TYPE.TypeName.BIEN_BAN_NT, ValueItem = ((int)ATTACH_TYPE.TypeValue.BIEN_BAN_NT).ToString() });
            items.Add(new ComboboxItem() { DisplayItem = ATTACH_TYPE.TypeName.KHAC, ValueItem = ((int)ATTACH_TYPE.TypeValue.KHAC).ToString() });
            ItemsLoaiCanCu = new ObservableCollection<ComboboxItem>(items);
            OnPropertyChanged(nameof(ItemsLoaiCanCu));
        }
    }
}
