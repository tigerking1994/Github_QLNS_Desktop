namespace VTS.QLNS.CTC.App.Model.Control
{
    public class CheckBoxTreeItem : CheckBoxItem
    {
        private string _id;
        public new string Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string _parentId;
        public string ParentId
        {
            get => _parentId;
            set => SetProperty(ref _parentId, value);
        }
    }
}
