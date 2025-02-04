using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.ViewModel;

namespace VTS.QLNS.CTC.App.Model
{
    public class SelectableModel : BindableBase
    {
        private bool _isSelected;
        private string? _name;
        private string? _description;
        private char _code;
        private double _numeric;
        private string? _food;

        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public char Code
        {
            get => _code;
            set => SetProperty(ref _code, value);
        }

        public string? Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string? Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public double Numeric
        {
            get => _numeric;
            set => SetProperty(ref _numeric, value);
        }

        public string? Food
        {
            get => _food;
            set => SetProperty(ref _food, value);
        }
    }
}
