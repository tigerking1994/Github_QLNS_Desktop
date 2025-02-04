using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhMstnKeHoachDatHangDanhMucService : INhMstnKeHoachDatHangDanhMucService
    {
        private readonly INhMstnKeHoachDatHangDanhMucRepository _repository;

        public NhMstnKeHoachDatHangDanhMucService(INhMstnKeHoachDatHangDanhMucRepository nhMstnKeHoachDatHangDanhMucRepository)
        {
            _repository = nhMstnKeHoachDatHangDanhMucRepository;
        }

        public int AddRange(IEnumerable<NhMSTNKeHoachDatHangDanhMuc> entities)
        {
            return _repository.AddRange(entities);
        }
        public void AddOrUpdate(Guid khDatHangId, IEnumerable<NhMSTNKeHoachDatHangDanhMuc> entities)
        {
            _repository.AddOrUpdate(khDatHangId, entities);
        }
        public int DeleteRange(IEnumerable<NhMSTNKeHoachDatHangDanhMuc> entities)
        {
            return _repository.RemoveRange(entities);
        }

        public int Update(NhMSTNKeHoachDatHangDanhMuc entity)
        {
            return _repository.Update(entity);
        }

        public int UpdateRange(IEnumerable<NhMSTNKeHoachDatHangDanhMuc> entities)
        {
            return _repository.UpdateRange(entities);
        }

        public IEnumerable<NhMSTNKeHoachDatHangDanhMuc> FindByKHDatHangId(Guid? khDatHangId)
        {
            return _repository.FindAll(x => x.IID_KeHoachDatHang == khDatHangId).OrderBy(x => x.SMaOrder);
        }
    }
}
