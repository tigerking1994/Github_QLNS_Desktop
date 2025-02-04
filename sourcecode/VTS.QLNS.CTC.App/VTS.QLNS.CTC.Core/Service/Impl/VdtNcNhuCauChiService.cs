using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtNcNhuCauChiService : IVdtNcNhuCauChiService
    {
        private readonly IVdtNcNhuCauChiRepository _repository;
        private readonly IVdtNcNhuCauChiChiTietRepository _detailRepository;

        public VdtNcNhuCauChiService(IVdtNcNhuCauChiRepository repository,
            IVdtNcNhuCauChiChiTietRepository detailRepository)
        {
            _repository = repository;
            _detailRepository = detailRepository;
        }

        public IEnumerable<VdtNcNhuCauChiQuery> GetNhuCauChiIndex()
        {
            return _repository.GetNhuCauChiIndex();
        }

        public KinhPhiCucTaiChinhCapQuery GetKinhPhiCucTaiChinhCap(int iNamKeHoach, string iIdMaDonVi, int iIdNguonVon, int iQuy)
        {
            return _repository.GetKinhPhiCucTaiChinhCap(iNamKeHoach, iIdMaDonVi, iIdNguonVon, iQuy);
        }

        public void InsertKeHoachChiQuy(VdtNcNhuCauChi data)
        {
            _repository.Add(data);
        }

        public void UpdateKeHoachChiQuy(VdtNcNhuCauChi data)
        {
            var dataUpdate = _repository.Find(data.Id);
            if (dataUpdate == null) return;
            dataUpdate.SSoDeNghi = data.SSoDeNghi;
            dataUpdate.DNgayDeNghi = data.DNgayDeNghi;
            dataUpdate.SNoiDung = data.SNoiDung;
            _repository.Update(dataUpdate);
        }

        public void DeleteKeHoachChiQuy(Guid iID)
        {
            var data = _repository.Find(iID);
            if (data == null) return;
            _repository.Delete(data);
        }

        public IEnumerable<VdtNcNhuCauChiChiTietQuery> GetNhuCauChiDetail(string iIdMaDonVi, int iNamKeHoach, int iIdNguonVon, int iQuy, int? DonviTinh = null)
        {
            return _repository.GetNhuCauChiDetail(iIdMaDonVi, iNamKeHoach, iIdNguonVon, iQuy, DonviTinh);
        }

        public List<VdtNcNhuCauChiChiTiet> GetDetailByParent(Guid iIdParentId)
        {
            return _detailRepository.GetDetailByParent(iIdParentId);
        }

        public void InsertDetailData(Guid iIDParentId, List<VdtNcNhuCauChiChiTiet> datas)
        {
            _detailRepository.DeleteDetailByParent(iIDParentId);
            _detailRepository.AddRange(datas);
        }

        public bool IsExistSoDeNghi(VdtNcNhuCauChi dataInsert)
        {
            return _repository.IsExistSoDeNghi(dataInsert);
        }
        
        public void InsertDetailDataImport(List<VdtNcNhuCauChiChiTiet> datas)
        {
            _detailRepository.AddRange(datas);
        }

        public bool CheckExistSoKeHoach(string sSoDeNghi, Guid? iId)
        {
            return _repository.FindAll().Where(x => x.SSoDeNghi == sSoDeNghi).Count() == 0 ? false : true;           
        }
    }
}
