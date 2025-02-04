using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhMstnKeHoachDatHangService : INhMstnKeHoachDatHangService
    {
        private readonly INhMstnKeHoachDatHangRepository _repository;


        public NhMstnKeHoachDatHangService(INhMstnKeHoachDatHangRepository khdtRepository)
        {
            _repository = khdtRepository;
        }

        #region KHDT
        public NhMSTNKeHoachDatHang Find(Guid iId)
        {
            return _repository.Find(iId);
        }

        public bool Insert(NhMSTNKeHoachDatHang obj)
        {
            return _repository.Add(obj) != 0;
        }

        public bool Update(NhMSTNKeHoachDatHang data)
        {
            return _repository.Update(data) != 0;
        }

        public IEnumerable<NhMSTNKeHoachDatHang> FindAll()
        {
            return _repository.FindAll();
        }

        public void Delete(Guid iId)
        {
            var obj = _repository.Find(iId);
            if (obj == null) return;
            if (obj.IIdParentId.HasValue)
            {
                var objParent = _repository.Find(obj.IIdParentId.Value);
                if (objParent != null)
                {
                    objParent.BIsActive = true;
                    _repository.Update(objParent);
                }
            }
            _repository.DeleteById(iId);
        }

        public void Log(Guid iID, string sUserLogIn)
        {
            var data = _repository.Find(iID);
            if (data == null) return;
            data.SNguoiSua = sUserLogIn;
            data.DNgaySua = DateTime.Now;
            _repository.Update(data);
        }

        public IEnumerable<NhMstnKeHoachDatHangQuery> GetAllMstnKeHoachDatHangIndex()
        {
            return _repository.GetAllMstnKeHoachDatHangIndex();
        }

        public bool CheckDuplicateSoQD(string soQuyetDinh, Guid id)
        {
            return _repository.CheckDuplicateSoQD(soQuyetDinh, id);
        }

        public IEnumerable<NhMstnKeHoachDatHangQuery> FindMstnKeHoachDatHangByCondition(Guid? donviId, Guid? keHoachTongTheId, Guid? chuongTrinhId)
        {
            return _repository.FindMstnKeHoachDatHangByCondition(donviId, keHoachTongTheId, chuongTrinhId);
        }
        #endregion
    }
}
