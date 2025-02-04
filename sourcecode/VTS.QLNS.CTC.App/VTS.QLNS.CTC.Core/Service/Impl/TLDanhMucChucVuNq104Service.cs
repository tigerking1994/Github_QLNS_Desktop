using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TLDanhMucChucVuNq104Service : IService<TlDmChucVuNq104>
    {
        private readonly ITLDanhMucChucVuNq104Repository _tLDanhMucChucVuRepository;
        private readonly ITlDmCanBoNq104Repository _canBoRepository;
        private readonly ITlCanBoPhuCapNq104Repository _canBoPhuCapRepository;
        private readonly ITlDmPhuCapNq104Repository _phuCapRepository;
        private readonly ITlCanBoPhuCapBridgeNq104Service _tlCanBoPhuCapBridgeNq104Service;

        public TLDanhMucChucVuNq104Service(ITLDanhMucChucVuNq104Repository tLDanhMucChucVuRepository,
            ITlDmCanBoNq104Repository TlDmCanBoNq104Repository,
            ITlCanBoPhuCapNq104Repository iTlCanBoPhuCapNq104Repository,
            ITlDmPhuCapNq104Repository iTlDmPhuCapNq104Repository,
            ITlCanBoPhuCapBridgeNq104Service tlCanBoPhuCapBridgeNq104Service)
        {
            _tLDanhMucChucVuRepository = tLDanhMucChucVuRepository;
            _canBoRepository = TlDmCanBoNq104Repository;
            _canBoPhuCapRepository = iTlCanBoPhuCapNq104Repository;
            _phuCapRepository = iTlDmPhuCapNq104Repository;
            _tlCanBoPhuCapBridgeNq104Service = tlCanBoPhuCapBridgeNq104Service;
        }

        public override void AddOrUpdateRange(IEnumerable<TlDmChucVuNq104> listEntities, AuthenticationInfo authenticationInfo)
        {
            _tLDanhMucChucVuRepository.AddOrUpdateRange(listEntities);
        }

        public override IEnumerable<TlDmChucVuNq104> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _tLDanhMucChucVuRepository.FindAll();
        }

        public override IEnumerable<TlDmChucVuNq104> FindAll(Expression<Func<TlDmChucVuNq104, bool>> predicate)
        {
            return _tLDanhMucChucVuRepository.FindAll(predicate);
        }

        public override IEnumerable<TlDmChucVuNq104> FindListChucVu(bool isLoaiChucVu, int namLamViec)
        {
            return _tLDanhMucChucVuRepository.FindAll(x => x.Loai == isLoaiChucVu && x.Nam == namLamViec).OrderBy(x => x.XauNoiMa).ThenBy(x => x.MaSo);
        }

        public override void UpdateCadres()
        {
            var month = int.Parse(DateTime.Now.ToString("MM"));
            var year = int.Parse(DateTime.Now.ToString("yyyy"));
            var lstCanBo = _canBoRepository.FindByMonthYear(month, year);
            List<TlDmCanBoNq104> lstCanBoUpdate = new List<TlDmCanBoNq104>();

            foreach (var canBo in lstCanBo)
            {
                if (canBo.MaCvd104 != null)
                {
                    var chucVu = _tLDanhMucChucVuRepository.FindByMa(canBo.MaCvd104);
                    canBo.TienLuongCvd = chucVu.TienLuong ?? 0;
                    canBo.TienNangLuongCvd = chucVu.TienNangLuong ?? 0;
                    lstCanBoUpdate.Add(canBo);

                    UpdatePhuCapCanBo(canBo, year, chucVu);
                    _tlCanBoPhuCapBridgeNq104Service.DataPreprocess(month, year);
                }
            }
            if (lstCanBoUpdate.Any())
                _canBoRepository.UpdateRange(lstCanBoUpdate);
        }

        private void UpdatePhuCapCanBo(TlDmCanBoNq104 canBo, int year, TlDmChucVuNq104 chucVu)
        {
            var canBoPhuCap = _canBoPhuCapRepository.FindByMaCbo(canBo.MaCanBo).FirstOrDefault();
            var allowences =_phuCapRepository.FindAll(x => x.Nam == year);
            var allowencesSaved = new List<AllowencePhuCapNq104Criteria>();

            if (canBoPhuCap != null)
            {
                var plainText = CompressExtension.DecompressFromBase64(canBoPhuCap.Data);
                allowencesSaved = JsonConvert.DeserializeObject<AllowenceCanBoNq104Criteria>(plainText).X.ToList();

                //Chuc vu
                if (chucVu.Loai == false)
                {
                    allowencesSaved.FirstOrDefault(x => x.A.Equals("LCV_TT")).B = chucVu.TienLuong;
                    allowencesSaved.FirstOrDefault(x => x.A.Equals("NLCV_TT")).B = chucVu.TienNangLuong;
                    allowencesSaved.FirstOrDefault(x => x.A.Equals("LBLCV_TT")).B = (canBo.TienLuongCvdCu - (chucVu.TienLuong + chucVu.TienNangLuong)) > 0 ? (canBo.TienLuongCvdCu - (chucVu.TienLuong + chucVu.TienNangLuong)) : null;
                }
                //Chuc danh
                else if (chucVu.Loai == true)
                {
                    allowencesSaved.FirstOrDefault(x => x.A.Equals("LCD_TT")).B = chucVu.TienLuong;
                    allowencesSaved.FirstOrDefault(x => x.A.Equals("NLCD_TT")).B = chucVu.TienNangLuong;
                    allowencesSaved.FirstOrDefault(x => x.A.Equals("LBLCD_TT")).B = (canBo.TienLuongCvdCu - (chucVu.TienLuong + chucVu.TienNangLuong)) > 0 ? (canBo.TienLuongCvdCu - (chucVu.TienLuong + chucVu.TienNangLuong)) : null; ;
                }

                string strJson = JsonConvert.SerializeObject(new AllowenceCanBoNq104Criteria()
                {
                    X = allowencesSaved
                });
                canBoPhuCap.MaPhuCap = "";
                canBoPhuCap.Data = CompressExtension.CompressToBase64(strJson);
                _canBoPhuCapRepository.Update(canBoPhuCap);
            }
        }

        public class YourComparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                if (!x.StartsWith("CV") && !x.StartsWith("CD") && !y.StartsWith("CV") && !y.StartsWith("CD"))
                {
                    return x.CompareTo(y);
                } 
                else if (int.TryParse(x.Substring(2), out int xValue) && int.TryParse(y.Substring(2), out int yValue))
                {
                    return xValue > yValue ? 1 : -1;
                }
                else
                {
                    return 0;
                } 
                    
            }
        }
    }
}
