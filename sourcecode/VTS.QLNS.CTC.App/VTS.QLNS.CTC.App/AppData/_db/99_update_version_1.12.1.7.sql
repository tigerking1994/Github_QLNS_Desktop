/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_phan_bo_kiem_tra_theo_nganh_phu_luc_tong_hop]    Script Date: 27/10/2022 11:44:45 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_phan_bo_kiem_tra_theo_nganh_phu_luc_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_phan_bo_kiem_tra_theo_nganh_phu_luc_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_phan_bo_kiem_tra_theo_nganh_phu_luc_tong_hop]    Script Date: 27/10/2022 11:44:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_skt_phan_bo_kiem_tra_theo_nganh_phu_luc_tong_hop]
	@Nganh varchar(max),
	@MaDonVi varchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@Loai int,
	@dvt int,
	@bTongHop bit
AS
BEGIN
SELECT iID_MLSKT IIdMlskt,
       iID_MLSKTCha IIdMlsktCha,
	   dt_dv.id idDonVi,
	   dt_dv.sTenDonVi,
	   dt_dv.iLoai,
       sKyHieu,
	   sSTT STT,
	   bHangCha,
       sNG,
       sMoTa,
       sNG_Cha sNgCha,
       QuyetToan =SUM(IsNull(A.QuyetToan,0))/@dvt,
       DuToan =SUM(IsNull(A.DuToan,0))/@dvt,
       TuChi =SUM(IsNull(A.TuChi,0))/@dvt,
       PhanCap =SUM(IsNull(A.PhanCap,0))/@dvt,
       MuaHangHienVat =SUM(IsNull(A.MuaHangHienVat,0))/@dvt,
       DacThu =SUM(IsNull(A.PhanCap,0))/@dvt
FROM
  (SELECT ml.iID_MLSKT ,
                ml.iID_MLSKTCha,
                ml.sKyHieu,
                ml.sNG,
                ml.sMoTa,
                ml.sNG_Cha,
				ml.sSTT,
				ml.bHangCha,
                ct.iID_MaDonVi,
				sTenDonVi,
                QuyetToan =0,
                DuToan =0,
                IsNull(ct.fTuChi, 0) TuChi,
                IsNull(ct.fPhanCap, 0) PhanCap,
                IsNull(ct.fMuaHangCapHienVat, 0) MuaHangHienVat,
                IsNull(ct.fPhanCap, 0) DacThu
   FROM NS_SKT_ChungTuChiTiet ct
   LEFT JOIN NS_SKT_ChungTu chungtu ON ct.iID_CTSoKiemTra = chungtu.iID_CTSoKiemTra
   RIGHT JOIN NS_SKT_MucLuc ml ON ct.iID_MLSKT = ml.iID_MLSKT
   AND ml.iNamLamViec = @NamLamViec
   AND ml.iTrangThai = 1
   WHERE ct.iNamLamViec=@NamLamViec
     AND ct.iNamNganSach = @NamNganSach
     AND ct.iID_MaNguonNganSach = @NguonNganSach
     AND ((ct.iLoai = @Loai AND @Loai <> 4) OR (ct.iLoai IN (2, 4) AND @Loai = 4))
     AND ct.iLoaiChungTu = 1
	 AND ((@bTongHop = 0)  or (@bTongHop = 1 AND chungtu.bDaTongHop = @bTongHop))
     AND ml.sNG in
       (SELECT *
        FROM f_split(@Nganh)))AS A 
LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai=1
     AND iNamLamViec=@NamLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
	WHERE (@Loai = 0 and dt_dv.iLoai != 0) or @Loai <> 0
GROUP BY iID_MLSKT,
         iID_MLSKTCha,
		 dt_dv.id,
	     dt_dv.sTenDonVi,
         sKyHieu,
		 sSTT,
		 bHangCha,
         sNG,
         sMoTa,
         sNG_Cha,
		 iLoai
		 order by sKyHieu
END
;
;
;
;
GO


UPDATE TL_DM_ThueThuNhapCaNhan SET bIsThueThang = 1;
INSERT INTO TL_DM_ThueThuNhapCaNhan(Loai_Thue, Ten_Thue, ThuNhap_Den, ThuNhap_Tu, Thue_Xuat, bIsThueThang)
VALUES
(N'B1', N'Thu nhập tính thuế năm bậc 1', 60000000, 0, 5, 0),
(N'B2', N'Thu nhập tính thuế năm bậc 2', 120000000, 60000000, 10, 0),
(N'B3', N'Thu nhập tính thuế năm bậc 3', 216000000, 120000000, 15, 0),
(N'B4', N'Thu nhập tính thuế năm bậc 4', 384000000, 216000000, 20, 0),
(N'B5', N'Thu nhập tính thuế năm bậc 5', 624000000, 384000000, 25, 0),
(N'B6', N'Thu nhập tính thuế năm bậc 6', 960000000, 624000000, 30, 0),
(N'B7', N'Thu nhập tính thuế năm bậc 7', 0, 960000000, 35, 0)


--thêm mới cấu hình chỉ tiêu lương
if not exists (select * from TL_PhuCap_MLNS where Ma_PhuCap = 'BHYTDV_TT')
insert into TL_PhuCap_MLNS (idCachTinhLuong, idMlns, idNguonNganSach, idPhuCap, K, L, LNS, M, Ma_CachTL, Ma_Cb, Ma_PhuCap, MoTa, Nam, NG, NguonNganSach, Ten_PhuCap, TM, TTM, UserCreator, XauNoiMa)
values ('64118331-2DBC-4FC9-9208-361DBA90393E', '3607CE50-902C-406A-9744-709D2E7B570A', 'CE2E2FBB-BC23-4C26-BF3C-89AA7D66F2FC', 'BE041C5A-734E-405E-BDEB-DC6FEE7E1DCA', '011', '010', '1010000', '6400', 'CACH0', 4, 'TA_BB_DG', N'Tiền ăn cơ bản bộ binh - HSQ, BS', 2022, '00', N'Tiền ăn cơ bản bộ binh - HSQ, BS', N'Tiền ăn bộ binh', '6401', '10', N'admin', '1010000-010-011-6400-6401-10-00'),
('64118331-2DBC-4FC9-9208-361DBA90393E', '54EC2163-AD5F-498A-85E5-280A8170A583', 'CE2E2FBB-BC23-4C26-BF3C-89AA7D66F2FC', 'A4B656C1-8ABC-4633-833A-B538C05FA56B', '011', '010', '1010000', '6300', 'CACH0', 3.3, 'BHYTDV_TT', N'Bảo hiểm y tế - Viên chức quốc phòng', 2022, '00', N'Bảo hiểm y tế - Viên chức quốc phòng', N'Bảo hiểm y tế đơn vị đóng (thành tiền)', '6302', '30', N'admin', '1010000-010-011-6300-6302-30-00')

--update--
update TL_PhuCap_MLNS set TL_PhuCap_MLNS.idPhuCap = b.id from TL_PhuCap_MLNS inner join TL_DM_PhuCap b 
on TL_PhuCap_MLNS.Ma_PhuCap = b.Ma_PhuCap and TL_PhuCap_MLNS.idPhuCap in ('BE041C5A-734E-405E-BDEB-DC6FEE7E1DCA', 'A4B656C1-8ABC-4633-833A-B538C05FA56B')
update TL_PhuCap_MLNS set TL_PhuCap_MLNS.idMlns = b.iID from TL_PhuCap_MLNS inner join NS_MucLucNganSach b 
on TL_PhuCap_MLNS.XauNoiMa = b.sXauNoiMa and b.iNamLamViec = 2022 and TL_PhuCap_MLNS.idMlns in ('3607CE50-902C-406A-9744-709D2E7B570A', '54EC2163-AD5F-498A-85E5-280A8170A583')
update TL_PhuCap_MLNS set TL_PhuCap_MLNS.idCachTinhLuong = b.id from TL_PhuCap_MLNS inner join TL_DM_ThemCachTinhLuong b 
on TL_PhuCap_MLNS.Ma_CachTL = b.Ma_ThemCachTL and TL_PhuCap_MLNS.idCachTinhLuong = '64118331-2DBC-4FC9-9208-361DBA90393E'

update TL_DM_PhuCap set Ten_PhuCap = N'Phụ cấp trách nhiệm cơ yếu' where Ma_PhuCap = 'PCCOYEU_TT'
update TL_DM_PhuCap set Ten_PhuCap = N'Phụ cấp báo vụ' where Ma_PhuCap = 'PCBAOVU_TT'

update TL_DM_PhuCap set iDinhDang = 1 where Ma_PhuCap like '%HS' and Parent <> ' '
update TL_DM_PhuCap set iDinhDang = 0 where Ma_PhuCap like '%TT' and Parent <> ' ' or Parent in ('TIENAN', 'TIENAN2')

update TL_DM_PhuCap set iLoai = Null where Ma_PhuCap in ('PCCV_TT', 'PCCOV_TT', 'PCKVCS_TT', 'PCTN_TT', 'PCTNVK_TT', 'PCKV_TT', 'PCTRA_TT', 'BHCN_TT')
update TL_DM_PhuCap set iLoai = 1 where Ma_PhuCap in ('PCTEMTHU_TT', 'PCCV_TIEN')
update TL_DM_PhuCap set iLoai = 3 where Ma_PhuCap in ('LOIICHKHAC_TT', 'THUONG_TT', 'TA_TT2')
update TL_DM_PhuCap set iLoai = 4 where Ma_PhuCap in ('GTKHAC_TT', 'TA_THANG', 'TA_TT', 'TRICHLUONG_TIEN', 'TRICHQUYKHAC_TT', 'TRICHQUYPCTT_TT')

if not exists (select * from TL_PhuCap_MLNS where Ma_PhuCap = 'TIENAN2')
insert into TL_DM_PhuCap (bGiaTri, bHuongPc_Sn, bSaoChep, Chon, Dinh_Dang, Gia_Tri, iDinhDang, Is_Formula, Is_Readonly, Ma_PhuCap, Parent, Ten_PhuCap, Tinh_BHXH, Xau_Noi_Ma)
values (1, 1, 0, 1, 0, 0, 0, 0, 0, 'TA_THEM_BB_DG', 'TIENAN2', N'Tiền ăn thêm bộ binh', 1, 'TIENAN2-TA_THEM_BB_DG')
