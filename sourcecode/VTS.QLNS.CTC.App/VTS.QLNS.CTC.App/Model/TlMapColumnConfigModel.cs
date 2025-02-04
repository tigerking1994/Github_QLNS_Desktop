using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlMapColumnConfigModel : ModelBase
    {
        private string _oldColumn;
        public string OldColumn
        {
            get => _oldColumn;
            set => SetProperty(ref _oldColumn, value);
        }

        private string _newColumn;
        public string NewColumn
        {
            get => _newColumn;
            set => SetProperty(ref _newColumn, value);
        }

        private bool? _isMapPhuCap;
        public bool? IsMapPhuCap
        {
            get => _isMapPhuCap;
            set => SetProperty(ref _isMapPhuCap, value);
        }

        private bool? _isMapValue;
        public bool? IsMapValue
        {
            get => _isMapValue;
            set => SetProperty(ref _isMapValue, value);
        }

        private bool? _usePhuCapValue;
        public bool? UsePhuCapValue
        {
            get => _usePhuCapValue;
            set => SetProperty(ref _usePhuCapValue, value);
        }

        private string _mapExpression;
        public string MapExpression
        {
            get => _mapExpression;
            set => SetProperty(ref _mapExpression, value);
        }

        public bool BHangCha => IsHangCha;

        public int Mau;
    }
}
