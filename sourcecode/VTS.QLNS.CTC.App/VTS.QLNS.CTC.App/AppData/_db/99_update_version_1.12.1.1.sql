/****** Object:  StoredProcedure [dbo].[sp_vdt_get_all_duan_nguonvon_by_duan]    Script Date: 20/10/2022 9:38:07 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_all_duan_nguonvon_by_duan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_all_duan_nguonvon_by_duan]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chi_tiet_export]    Script Date: 20/10/2022 9:38:07 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_ct_chi_tiet_export]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_ct_chi_tiet_export]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 20/10/2022 9:38:07 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bangluong_thang_dulieu_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bangluong_thang_dulieu_insert]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_chi_tiet_phan_bo_kiem_tra_nsbd]    Script Date: 20/10/2022 9:38:07 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_chi_tiet_phan_bo_kiem_tra_nsbd]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_chi_tiet_phan_bo_kiem_tra_nsbd]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_chi_tiet_phan_bo_kiem_tra_nsbd]    Script Date: 20/10/2022 9:38:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_skt_chi_tiet_phan_bo_kiem_tra_nsbd]
	@MaDonVi varchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@dvt int
AS
BEGIN
SELECT ml.iID_MLSKT IIdMlskt,
       ml.iID_MLSKTCha IIdMlsktCha,
       ml.sKyHieu,
	   ml.sSTT STT,
	   ml.bHangCha,
       ml.sNG,
       ml.sMoTa,
       ml.sNG_Cha sNgCha,
       QuyetToan =SUM(IsNull(A.QuyetToan,0))/@dvt,
       DuToan =SUM(IsNull(A.DuToan,0))/@dvt,
       TuChi =SUM(IsNull(A.TuChi,0))/@dvt,
       PhanCap =SUM(IsNull(A.PhanCap,0))/@dvt,
       MuaHangHienVat =SUM(IsNull(A.MuaHangHienVat,0))/@dvt,
       DacThu =SUM(IsNull(A.PhanCap,0))/@dvt,
	   ThongBaoDV = SUM(IsNull(A.ThongBaoDV, 0))/@dvt ,
	   HuyDongTonKho = SUM(ISNULL(A.HuyDongTonKho, 0))/@dvt
FROM
(select * from NS_SKT_MucLuc where iTrangThai = 1 and iNamLamViec = @NamLamViec) ml right join
  (SELECT ml.iID_MLSKT ,
          ml.iID_MLSKTCha,
          ml.sKyHieu,
          ml.sNG,
          ml.sMoTa,
          ml.sNG_Cha,
          ct.iID_MaDonVi,
          QuyetToan =0,
          DuToan =0,
          IsNull(ct.fTuChi, 0) TuChi,
          IsNull(ct.fPhanCap, 0) PhanCap,
          IsNull(ct.fMuaHangCapHienVat, 0) MuaHangHienVat,
          IsNull(ct.fPhanCap, 0) DacThu,
		  IsNull(ct.fThongBaoDonVi, 0) ThongBaoDV,
		  ISNULL(ct.fhuydongtonkho, 0) as HuyDongTonKho
   FROM NS_SKT_ChungTuChiTiet ct
   RIGHT JOIN NS_SKT_MucLuc ml ON ct.iID_MLSKT = ml.iID_MLSKT
   AND ml.iNamLamViec = @NamLamViec
   AND ml.iTrangThai = 1
   WHERE ct.iNamLamViec=@NamLamViec
     AND ct.iNamNganSach = @NamNganSach
     AND ct.iID_MaNguonNganSach = @NguonNganSach
     AND ct.iLoai=4
     AND ct.iLoaiChungTu = 2
     AND (@MaDonVi is null OR ct.iID_MaDonVi = @MaDonVi)
	 AND ct.iID_MaDonVi != '999'
   UNION SELECT ml.iID_MLSKT ,
                ml.iID_MLSKTCha,
                ml.sKyHieu,
                ml.sNG,
                ml.sMoTa,
                ml.sNG_Cha,
                ct.iID_MaDonVi,
                QuyetToan =0,
                DuToan =0,
                IsNull(ct.fTuChi, 0) TuChi,
                IsNull(ct.fPhanCap, 0) PhanCap,
                IsNull(ct.fMuaHangCapHienVat, 0) MuaHangHienVat,
                IsNull(ct.fPhanCap, 0) DacThu,
				IsNull(ct.fThongBaoDonVi, 0) ThongBaoDV,
				ISNULL(ct.fhuydongtonkho, 0) as HuyDongTonKho
   FROM NS_SKT_ChungTuChiTiet ct
   RIGHT JOIN NS_SKT_MucLuc ml ON ct.iID_MLSKT = ml.iID_MLSKT
   AND ml.iNamLamViec = @NamLamViec
   AND ml.iTrangThai = 1
   WHERE ct.iNamLamViec=@NamLamViec
     AND ct.iNamNganSach = @NamNganSach
     AND ct.iID_MaNguonNganSach = @NguonNganSach
     AND ct.iLoai=4
     AND ct.iLoaiChungTu = 1
	 AND ct.fTuChi > 0) AS A on ml.iID_MLSKT = A.iID_MLSKT
LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi
   FROM DonVi
   WHERE iTrangThai=1
     AND iNamLamViec=@NamLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
GROUP BY ml.iID_MLSKT,
         ml.iID_MLSKTCha,
         ml.sKyHieu,
		 ml.sSTT,
		 ml.bHangCha,
         ml.sNG,
         ml.sMoTa,
         ml.sNG_Cha
		 order by ml.sKyHieu
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 20/10/2022 9:38:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		TungNH
-- Create date: 21/04/2022
-- Description:	Lấy dữ liệu cho thêm mới bảng lương tháng
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_bangluong_thang_dulieu_insert] 
	@Thang int, 
	@Nam int,
	@MaDonVi NVARCHAR(MAX),
	@MaCachTl NVARCHAR(50),
	@SoNgay int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    WITH SoLieuTienAn AS (
		SELECT
			MA_CBO MaCanBo,
			SUM (
				CASE WHEN PARENT <> N'TIENAN' THEN 0
					WHEN canBoPhuCap.MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN ISNULL(@SoNgay, 0) * canBoPhuCap.GIA_TRI 
					ELSE ISNULL(canBoPhuCap.HuongPC_SN, 0) * canBoPhuCap.GIA_TRI
				END
			) GiaTriTA1,
			SUM (
				CASE WHEN PARENT <> N'TIENAN2' THEN 0
					WHEN canBoPhuCap.MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN ISNULL(@SoNgay, 0) * canBoPhuCap.GIA_TRI 
					ELSE ISNULL(canBoPhuCap.HuongPC_SN, 0) * canBoPhuCap.GIA_TRI
				END
			) GiaTriTA2
		FROM TL_CanBo_PhuCap canBoPhuCap
		INNER JOIN TL_DM_PhuCap as pc on canBoPhuCap.MA_PHUCAP = pc.Ma_PhuCap
		--WHERE
			--canBoPhuCap.MA_PHUCAP IN ('TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4', 'TA_DOCHAI_DG', 'TA_OM_DG', 'TA_BB_DG', 'TA_TRUCTRAI_DG')
		WHERE 
			pc.PARENT IN ('TIENAN', 'TIENAN2')
		GROUP BY canBoPhuCap.MA_CBO
	),

	ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo AS MaCanBo,
			canBo.Ten_CanBo AS TenCanBo,
			donVi.Ma_DonVi MaDonVi,
			canBo.Ma_Hieu_CanBo AS MaHieuCanBo,
			capBac.Ma_Cb MaCapBac,
			canBo.BHTN,
			canBo.PCCV
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent = donVi.Ma_DonVi
		INNER  JOIN TL_DM_CapBac capBac
			ON canBo.Ma_CB = capBac.Ma_Cb
		WHERE
			canBo.Thang = @Thang
			AND canBo.Nam = @Nam
			AND canBo.Parent IN (SELECT * FROM f_split(@MaDonVi))
	)

	SELECT
		newid()					AS Id,
		@Thang					AS Thang,
		@Nam					AS Nam,
		canBo.MaCanBo			AS MaCbo,
		canBo.TenCanBo			AS TenCbo,
		canBo.MaDonVi			AS MaDonVi,
		@MaCachTl				AS MaCachTl,
		canBoPhuCap.MA_PHUCAP	AS MaPhuCap,
		CASE
			WHEN (canBoPhuCap.MA_PHUCAP = 'TCVIECLAM_TT' AND cb.Parent in (1, 2, 3)) THEN 0 
			WHEN (canBoPhuCap.MA_PHUCAP = 'NTN' AND canBoPhuCap.GIA_TRI < 5) OR (canBoPhuCap.MA_PHUCAP = 'BHTNCN_HS' AND canBo.BHTN = 0) OR (canBoPhuCap.MA_PHUCAP = 'PCCOV_HS' AND canBo.PCCV = 0) THEN 0
			WHEN canBoPhuCap.MA_PHUCAP = 'TA_TT' THEN soLieuTienAn.GiaTriTA1
			WHEN canBoPhuCap.MA_PHUCAP = 'TA_TT2' THEN soLieuTienAn.GiaTriTA2
			WHEN canBoPhuCap.MA_PHUCAP = 'TRICHLUONG_TT' THEN (canBoPhuCap.GIA_TRI / 1000) * 1000
			ELSE canBoPhuCap.GIA_TRI
		END						AS GiaTri,
		canBo.MaHieuCanBo		AS MaHieuCanBo,
		canBo.MaCapBac			AS MaCb,
		canBoPhuCap.HuongPC_SN	AS HuongPcSn,
		cachTinhLuong.CongThuc	AS CongThuc,
		CASE WHEN cachTinhLuong.Id IS NULL THEN 1 ELSE 0 END AS IsCalculated
	FROM TL_CanBo_PhuCap canBoPhuCap
	INNER JOIN ThongTinCanBo canBo
		ON canBo.MaCanBo = canBoPhuCap.MA_CBO
	LEFT JOIN SoLieuTienAn soLieuTienAn
		ON canBoPhuCap.MA_CBO = soLieuTienAn.MaCanBo
	--LEFT JOIN SoLieuTienAn2 soLieuTienAn2
	--	ON canBoPhuCap.MA_CBO = soLieuTienAn.MaCanBo
	LEFT JOIN TL_DM_Cach_TinhLuong_Chuan cachTinhLuong
		ON cachTinhLuong.Ma_Cot = canBoPhuCap.MA_PHUCAP
	left join TL_DM_CapBac cb on canBo.MaCapBac = cb.Ma_Cb
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chi_tiet_export]    Script Date: 20/10/2022 9:38:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_tl_get_data_ct_chi_tiet_export]
	@lstId nvarchar(max),
	@lstCach nvarchar(100)
AS
BEGIN
	SELECT tbl.ID, Thang INTO #tblMaxThang
	FROM f_split(@lstId) as tmp
	INNER JOIN TL_QT_ChungTu as tbl on tmp.Item = tbl.ID;

	with ctct as (
	  select XauNoiMa,MaCachTl,  Sum(TongCong) AS TongCong,
		   Sum(DieuChinh) AS DieuChinh,
		 Sum(DDuToan) As DDuToan
	  from TL_QT_ChungTu as tbl
	  INNER JOIN TL_QT_ChungTuChiTiet as dt on tbl.Id = dt.Id_ChungTu
	  where  (ISNULL(@lstId, '') <> '' AND tbl.ID in (SELECT *  FROM f_split(@lstId)))
		AND  dt.MaCachTl in (SELECT *  FROM f_split(@lstCach))
	  group by dt.XauNoiMa, dt.MaCachTl
	),
	lstSoNguoi as (
		SELECT XauNoiMa,
			SoNguoi
		from TL_QT_ChungTu as tbl
		INNER JOIN TL_QT_ChungTuChiTiet as dt on tbl.Id = dt.Id_ChungTu
		where tbl.ID in (SELECT TOP(1) ID FROM #tblMaxThang ORDER BY Thang DESC)
		AND  dt.MaCachTl in (SELECT *  FROM f_split(@lstCach))
	)
SELECT 
     NEWID() as Id,
     iID_MLNS AS MlnsId,
       iID_MLNS_Cha AS MlnsIdParent,
       sXauNoiMa AS XauNoiMa,
       sLNS AS Lns,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS Ng,
       sTNG AS TNG,
       sTNG1 AS TNG1,
       sTNG2 AS TNG2,
       sTNG3 AS TNG3,
       sMoTa AS Mota,
     MaCachTl as MaCachTl,
       iNamLamViec AS NamLamViec,
       mlns.bHangCha AS BHangCha,
       sChiTietToi AS ChiTietToi,
       TongCong,
    SoNguoi,
       DieuChinh,
     DDuToan
FROM NS_MucLucNganSach mlns  
LEFT JOIN ctct ctct ON mlns.sXauNoiMa = ctct.XauNoiMa
LEFT JOIN lstSoNguoi as sn on mlns.sXauNoiMa = sn.XauNoiMa

WHERE sLNS IN ('1',
               '101',
               '1010000')
  AND iNamLamViec = 2022

order by mlns.sXauNoiMa

DROP TABLE #tblMaxThang
END
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_all_duan_nguonvon_by_duan]    Script Date: 20/10/2022 9:38:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_vdt_get_all_duan_nguonvon_by_duan]
	@duAnId nvarchar(500)
AS
BEGIN

	create table tmpNguonVon(nvId int)

	insert into tmpNguonVon(nvId)
		select iID_NguonVonID 
			from VDT_KHV_KeHoach5Nam as tbl
			INNER JOIN VDT_KHV_KeHoach5Nam_ChiTiet as dt on tbl.iID_KeHoach5NamID = dt.iID_KeHoach5NamID
			where iID_DuAnID = @duAnId AND tbl.bActive = 1

	SELECT distinct null as Id,
	null as IdChuTruongNguonVon,
	null as IIdChuTruongDauTuId,
	nv.nvId as IIdNguonVonId,
	ns.sTen as TenNguonVon,
	null as FGiaTriDieuChinh,
	null as GiaTriTruocDieuChinh,
	(select sum(dt.fHanMucDauTu) from VDT_KHV_KeHoach5Nam as tbl
			INNER JOIN VDT_KHV_KeHoach5Nam_ChiTiet as dt on tbl.iID_KeHoach5NamID = dt.iID_KeHoach5NamID
			where iID_DuAnID = @duAnId AND tbl.bActive = 1) as FTienPheDuyet
	FROM tmpNguonVon nv
	INNER JOIN NguonNganSach ns ON nv.nvId = ns.iID_MaNguonNganSach
	--where danv.iID_DuAn = @duAnId

	drop table tmpNguonVon
	
END;
;
;
GO

IF NOT EXISTS (SELECT * FROM TL_DM_CapBac WHERE Ma_Cb='3.1')
begin
INSERT INTO TL_DM_CapBac(Ma_Cb, Note, Ten_Cb, XauNoiMa, Parent, Bhcs_Cq, Bhtn_Cq, Bhxh_Cq, Bhyt_Cq, Hs_Bhcs, Hs_Bhtn, Hs_Bhxh, Hs_Bhyt, Hs_Kpcd, Kpcd_Cq, Lht_Hs, PhuCapRaQuan, Readonly, Splits, TiLeHuong)
SELECT '3.1', N'Công nhân quốc phòng', N'Công nhân quốc phòng', '3-3.1', '3', Bhcs_Cq, Bhtn_Cq, Bhxh_Cq, Bhyt_Cq, Hs_Bhcs, Hs_Bhtn, Hs_Bhxh, Hs_Bhyt, Hs_Kpcd, Kpcd_Cq, Lht_Hs, PhuCapRaQuan, Readonly, Splits, TiLeHuong
FROM TL_DM_CapBac WHERE Ma_Cb = '3'
end 

IF NOT EXISTS (SELECT * FROM TL_DM_CapBac WHERE Ma_Cb='3.2')
begin
INSERT INTO TL_DM_CapBac(Ma_Cb, Note, Ten_Cb, XauNoiMa, Parent, Bhcs_Cq, Bhtn_Cq, Bhxh_Cq, Bhyt_Cq, Hs_Bhcs, Hs_Bhtn, Hs_Bhxh, Hs_Bhyt, Hs_Kpcd, Kpcd_Cq, Lht_Hs, PhuCapRaQuan, Readonly, Splits, TiLeHuong)
SELECT '3.2', N'Công chức quốc phòng', N'Công chức quốc phòng', '3-3.2', '3', Bhcs_Cq, Bhtn_Cq, Bhxh_Cq, Bhyt_Cq, Hs_Bhcs, Hs_Bhtn, Hs_Bhxh, Hs_Bhyt, Hs_Kpcd, Kpcd_Cq, Lht_Hs, PhuCapRaQuan, Readonly, Splits, TiLeHuong
FROM TL_DM_CapBac WHERE Ma_Cb = '3'
end 

IF NOT EXISTS (SELECT * FROM TL_DM_CapBac WHERE Ma_Cb='3.3')
begin
INSERT INTO TL_DM_CapBac(Ma_Cb, Note, Ten_Cb, XauNoiMa, Parent, Bhcs_Cq, Bhtn_Cq, Bhxh_Cq, Bhyt_Cq, Hs_Bhcs, Hs_Bhtn, Hs_Bhxh, Hs_Bhyt, Hs_Kpcd, Kpcd_Cq, Lht_Hs, PhuCapRaQuan, Readonly, Splits, TiLeHuong)
SELECT '3.3', N'Viên chức quốc phòng', N'Viên chức quốc phòng', '3-3.3', '3', Bhcs_Cq, Bhtn_Cq, Bhxh_Cq, Bhyt_Cq, Hs_Bhcs, Hs_Bhtn, Hs_Bhxh, Hs_Bhyt, Hs_Kpcd, Kpcd_Cq, Lht_Hs, PhuCapRaQuan, Readonly, Splits, TiLeHuong
FROM TL_DM_CapBac WHERE Ma_Cb = '3'
end

UPDATE TL_DM_CapBac
SET 
Parent = '3.3',
XauNoiMa = CONCAT('3-3.3-', XauNoiMa)
WHERE Parent = '3' AND Ma_Cb NOT IN ('3.1', '3.2', '3.3')

update TL_DM_PhuCap set bSaoChep = 1
where Parent in ('CHIKHAC', 'GIAMTRU', 'THUE_TNCN', 'SUM')
update TL_DM_PhuCap set bSaoChep = 0
where Ma_PhuCap in ('GTNN', 'GTPT_DG', 'GTPT_SN')

update TL_DM_Cach_TinhLuong_TruyLinh set CongThuc = '(LHT_TT+PCCV_TT+PCTNVK_TT+HSBL_TT)*PCCOV_HS*TTL_LHT' 
where Ma_Cot = 'PCCOV_TT' and Ma_CachTL = 'CACH5'

delete from TL_DM_PhuCap
where Ma_PhuCap in ('NTN1', 'TTL_PCDTQUANSU', 'TTL_PCPHICONG', 'TTL_PCTRA', 'TTL_PCCOV', 'PCCOV_HS_CU', 'PCTQ_TT_CU', 'THUNHAPKHAC')

update TL_DM_PhuCap set Ten_PhuCap = N'Tiền ăn bị trừ' where Ma_PhuCap = 'TIENAN'
update TL_DM_PhuCap set Ten_PhuCap = N'Tiền ăn thêm' where Ma_PhuCap = 'TIENAN2'
update TL_DM_PhuCap set Ten_PhuCap = N'Tổng tiền ăn thêm trong tháng' where Ma_PhuCap = 'TA_TT2'
update TL_DM_PhuCap set Parent = 'TIENAN2', Xau_Noi_Ma = 'TIENAN2-TA_TRUCQY_DG1' where Ma_PhuCap = 'TA_TRUCQY_DG1'
update TL_DM_PhuCap set Parent = 'TIENAN2', Xau_Noi_Ma = 'TIENAN2-TA_TRUCQY_DG3' where Ma_PhuCap = 'TA_TRUCQY_DG3'
update TL_DM_PhuCap set Parent = 'TIENAN2', Xau_Noi_Ma = 'TIENAN2-TA_TRUCQY_DG4' where Ma_PhuCap = 'TA_TRUCQY_DG4'
update TL_DM_PhuCap set Parent = 'TIENAN2', Xau_Noi_Ma = 'TIENAN2-TA_TRUCTRAI_DG' where Ma_PhuCap = 'TA_TRUCTRAI_DG'
update TL_DM_PhuCap set Parent = 'TIENAN2', Xau_Noi_Ma = 'TIENAN2-TA_TRUCQY_DG2' where Ma_PhuCap = 'TA_TRUCQY_DG2'
update TL_DM_PhuCap set Parent = 'TIENAN2', Xau_Noi_Ma = 'TIENAN2-TA_DOCHAI_DG' where Ma_PhuCap = 'TA_DOCHAI_DG'
update TL_DM_PhuCap set Parent = 'TIENAN2', Xau_Noi_Ma = 'TIENAN2-TA_OM_DG' where Ma_PhuCap = 'TA_OM_DG'

update TL_DM_PhuCap set bGiaTri=1 where Ma_PhuCap='TILE_HUONG'

UPDATE TL_DM_Cach_TinhLuong_Chuan SET CongThuc ='PC10+PCTQ_TT+PCKHAC2_TT+PCKHAC3_TT+PCDT_TT+PCTHANHTRA_TT+PCNU_TT+PCTEMTHU_TT+PCDTQUANSU_TT+PCDTN_TT+PCCU_TT+PCHOIPHUNU_TT+PCCONGDOAN_TT+PCANQP_TT+PCGS_TT+PCTS_TT+PCPGS_TT+PCBCV_TT+TA_TT2'
WHERE Ma_Cot='PCKHAC_SUM' AND Ma_CachTL='CACH0'

IF NOT EXISTS (SELECT * FROM TL_DM_PhuCap WHERE Ma_PhuCap='PCBAOMAT_HS')
begin
insert into TL_DM_PhuCap (bGiaTri, bHuongPc_Sn, bSaoChep, Chon, Dinh_Dang, Gia_Tri, Is_Formula, Is_Readonly, Ma_PhuCap, Parent, Ten_PhuCap, Tinh_BHXH, Xau_Noi_Ma)
values (1, 1, 1, 1, 0, 0, 0, 0, 'PCBAOMAT_HS', 'PCTRA_HS', N'Hệ số phụ cấp trách nhiệm bảo mật', 1, 'PCTRA_HS-PCBAOMAT_HS')
end

IF NOT EXISTS (SELECT * FROM TL_DM_PhuCap WHERE Ma_PhuCap='PCBAOMAT_TT')
begin
insert into TL_DM_PhuCap (bGiaTri, bHuongPc_Sn, bSaoChep, Chon, Dinh_Dang, Gia_Tri, iDinhDang, iLoai, Is_Formula, Is_Readonly, Ma_PhuCap, Parent, Ten_PhuCap, Tinh_BHXH, Xau_Noi_Ma)
values (1, 1, 1, 1, 0, 0, 0, 2, 1, 0, 'PCBAOMAT_TT', 'PCTRA_TT', N'Phụ cấp trách nhiệm bảo mật', 1, 'PCTRA_TT-PCBAOMAT_TT')
end

IF NOT EXISTS (SELECT * FROM TL_DM_Cach_TinhLuong_Chuan WHERE Ma_Cot='PCBAOMAT_TT')
begin
insert into TL_DM_Cach_TinhLuong_Chuan (CongThuc, Ma_CachTL, Ma_Cot, NoiDung, Ten_Cot)
values ('LCS*PCBAOMAT_HS', 'CACH0', 'PCBAOMAT_TT',N'Phụ cấp văn thư bảo mật', N'Phụ cấp trách nhiệm bảo mật')
end

IF NOT EXISTS (SELECT * FROM TL_DM_Cach_TinhLuong_Chuan WHERE Ma_Cot='PCBAOVU_TT')
begin
insert into TL_DM_Cach_TinhLuong_Chuan (CongThuc, Ma_CachTL, Ma_Cot, NoiDung, Ten_Cot)
values ('LCS*PCBAOVU_HS', 'CACH0', 'PCBAOVU_TT',N'Phụ cấp báo vụ', N'Phụ cấp báo vụ')
end

IF NOT EXISTS (SELECT * FROM TL_DM_Cach_TinhLuong_Chuan WHERE Ma_Cot='PCCOYEU_TT')
begin
insert into TL_DM_Cach_TinhLuong_Chuan (CongThuc, Ma_CachTL, Ma_Cot, NoiDung, Ten_Cot)
values ('LCS*PCCOYEU_HS', 'CACH0', 'PCCOYEU_TT',N'Phụ cấp cơ yếu', N'Phụ cấp trách nhiệm cơ yếu')
end

update TL_DM_Cach_TinhLuong_Chuan set CongThuc = 
'PCTRA10_TT+PCTRA15_TT+PCTRA20_TT+PCTRA25_TT+PCTRA30_TT+PCTRA35_TT+PCTRA40_TT+PCTRA45_TT+PCTRA50_TT+PCBVBG_TT+PCTAUBP_TT+PCBAOMAT_TT+PCBAOVU_TT+PCCOYEU_TT' 
where Ma_CachTL= 'CACH0' and Ma_Cot = 'PCTRA_SUM'