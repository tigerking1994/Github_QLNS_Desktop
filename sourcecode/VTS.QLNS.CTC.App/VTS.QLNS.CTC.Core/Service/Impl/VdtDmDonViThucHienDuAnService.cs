using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtDmDonViThucHienDuAnService : IService<VdtDmDonViThucHienDuAn>, IVdtDmDonViThucHienDuAnService
    {
        private IVdtDmDonViThucHienDuAnRepository _vdtDmDonViThucHienDuAnRepository;

        public VdtDmDonViThucHienDuAnService(IVdtDmDonViThucHienDuAnRepository vdtDmDonViThucHienDuAnRepository)
        {
            _vdtDmDonViThucHienDuAnRepository = vdtDmDonViThucHienDuAnRepository;
        }

        public override void AddOrUpdateRange(IEnumerable<VdtDmDonViThucHienDuAn> listEntities, AuthenticationInfo authenticationInfo)
        {
            _vdtDmDonViThucHienDuAnRepository.AddOrUpdateRange(listEntities, authenticationInfo.YearOfWork);
        }

        public override IEnumerable<VdtDmDonViThucHienDuAn> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _vdtDmDonViThucHienDuAnRepository.FindAllWithOrder();
        }

        public IEnumerable<VdtDmDonViThucHienDuAn> FindAll()
        {
            return _vdtDmDonViThucHienDuAnRepository.FindAll();
        }

        public VdtDmDonViThucHienDuAn FindByMaDonVi(string sMaDonVi)
        {
            return _vdtDmDonViThucHienDuAnRepository.FindByMaDonVi(sMaDonVi);
        }

        public IEnumerable<NSDonViThucHienDuAnExportQuery> GetDonViThucHienDuAnExport()
        {
            return _vdtDmDonViThucHienDuAnRepository.GetDonViThucHienDuAnExport();
        }
    }
}
