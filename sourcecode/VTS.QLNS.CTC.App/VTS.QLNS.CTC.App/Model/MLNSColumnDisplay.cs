using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class MLNSColumnDisplay : BindableBase
    {
        private Visibility _columnM;
        [MLNSColumnAttribute((int)MLNSFiled.M)]
        public Visibility ColumnM
        {
            get => _columnM;
            set => SetProperty(ref _columnM, value);
        }

        private Visibility _columnTM;
        [MLNSColumnAttribute((int)MLNSFiled.TM)]
        public Visibility ColumnTM
        {
            get => _columnTM;
            set => SetProperty(ref _columnTM, value);
        }

        private Visibility _columnTTM;
        [MLNSColumnAttribute((int)MLNSFiled.TTM)]
        public Visibility ColumnTTM
        {
            get => _columnTTM;
            set => SetProperty(ref _columnTTM, value);
        }

        private Visibility _columnNG;
        [MLNSColumnAttribute((int)MLNSFiled.NG)]
        public Visibility ColumnNG
        {
            get => _columnNG;
            set => SetProperty(ref _columnNG, value);
        }

        private Visibility _columnTNG;
        [MLNSColumnAttribute((int)MLNSFiled.TNG)]
        public Visibility ColumnTNG
        {
            get => _columnTNG;
            set => SetProperty(ref _columnTNG, value);
        }

        private Visibility _columnTNG1;
        [MLNSColumnAttribute((int)MLNSFiled.TNG1)]
        public Visibility ColumnTNG1
        {
            get => _columnTNG1;
            set => SetProperty(ref _columnTNG1, value);
        }

        private Visibility _columnTNG2;
        [MLNSColumnAttribute((int)MLNSFiled.TNG2)]
        public Visibility ColumnTNG2
        {
            get => _columnTNG2;
            set => SetProperty(ref _columnTNG2, value);
        }

        private Visibility _columnTNG3;
        [MLNSColumnAttribute((int)MLNSFiled.TNG3)]
        public Visibility ColumnTNG3
        {
            get => _columnTNG3;
            set => SetProperty(ref _columnTNG3, value);
        }

        public MLNSColumnDisplay()
        {
            _columnM = Visibility.Visible;
            _columnTM = Visibility.Visible;
            _columnTTM = Visibility.Visible;
            _columnNG = Visibility.Visible;
            _columnTNG = Visibility.Visible;
            _columnTNG1 = Visibility.Visible;
            _columnTNG2 = Visibility.Visible;
            _columnTNG3 = Visibility.Visible;
        }
    }

    public class MLNSColumnAttribute : Attribute
    {
        public int ColumnIndex { get; set; }
        public MLNSColumnAttribute(int columnIndex)
        {
            ColumnIndex = columnIndex;
        }
    }
}
