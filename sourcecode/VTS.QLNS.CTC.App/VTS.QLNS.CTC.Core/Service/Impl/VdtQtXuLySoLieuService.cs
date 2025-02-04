using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtQtXuLySoLieuService : IVdtQtXuLySoLieuService
    {
        private readonly IVdtQtXuLySoLieuRepository _xldlRepository;

        public VdtQtXuLySoLieuService(IVdtQtXuLySoLieuRepository xldlRepository)
        {
            _xldlRepository = xldlRepository;
        }

        public bool Insert(VdtQtDeNghiQuyetToanNienDo objNienDo, List<VdtQtXuLySoLieu> lstDataInsert, string sUserLogin)
        {
            _xldlRepository.DeleteAllXuLySoLieuByParent(objNienDo);
            lstDataInsert = lstDataInsert.Select(n =>
            {
                n.Id = Guid.NewGuid();
                n.DDateCreate = DateTime.Now;
                n.DNgayLap = objNienDo.DNgayDeNghi;
                n.IIdDonViQuanLyId = objNienDo.IIdDonViDeNghiId;
                n.IIdLoaiNguonVonId = objNienDo.IIdLoaiNguonVonId;
                n.IIDMaDonViQuanLy = objNienDo.IIDMaDonViDeNghi;
                n.IIdNguonVonId = objNienDo.IIdNguonVonId;
                n.INamKeHoach = objNienDo.INamKeHoach;
                n.SUserCreate = sUserLogin;
                return n;
            }).ToList();
            return _xldlRepository.AddRange(lstDataInsert) != 0;
        }
    }
}
