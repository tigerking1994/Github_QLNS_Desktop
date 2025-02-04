using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtTtDeNghiThanhToanUngService : IVdtTtDeNghiThanhToanUngService
    {
        private readonly IVdtTtDeNghiThanhToanUngRepository _thanhToanUngRepository;
        private readonly IVdtTtDeNghiThanhToanUngChiTietRepository _thanhToanUngChiTietRepository;

        public VdtTtDeNghiThanhToanUngService(IVdtTtDeNghiThanhToanUngRepository thanhToanUngRepository, IVdtTtDeNghiThanhToanUngChiTietRepository thanhToanUngChiTietRepository)
        {
            _thanhToanUngRepository = thanhToanUngRepository;
            _thanhToanUngChiTietRepository = thanhToanUngChiTietRepository;
        }

        public IEnumerable<VdtTtDeNghiThanhToanUngQuery> GetDeNghiThanhToanUngIndex()
        {
            return _thanhToanUngRepository.GetDeNghiThanhToanUngIndex();
        }

        public bool Insert(VdtTtDeNghiThanhToanUng data, string sUserLogin)
        {
            data.Id = Guid.NewGuid();
            data.DDateCreate = DateTime.Now;
            data.SUserCreate = sUserLogin;
            return _thanhToanUngRepository.Add(data) != 0;
        }

        public bool Update(VdtTtDeNghiThanhToanUng data, string sUserLogin)
        {
            var objDataUpdate = _thanhToanUngRepository.Find(data.Id);
            objDataUpdate.SSoDeNghi = data.SSoDeNghi;
            objDataUpdate.SNguoiLap = data.SNguoiLap;
            objDataUpdate.SGhiChu = data.SGhiChu;
            objDataUpdate.DDateUpdate = DateTime.Now;
            objDataUpdate.SUserUpdate = sUserLogin;
            return _thanhToanUngRepository.Update(objDataUpdate) != 0;
        }

        public bool DeleteDeNghiThanhToanUng(VdtTtDeNghiThanhToanUng data, string sUserLogin)
        {
            var dataDelete = _thanhToanUngRepository.Find(data.Id);
            if (dataDelete == null || dataDelete.Id == Guid.Empty) return false;
            dataDelete.DDateDelete = DateTime.Now;
            dataDelete.SUserDelete = sUserLogin;
            return _thanhToanUngRepository.Update(dataDelete) != 0;
        }
    }
}
