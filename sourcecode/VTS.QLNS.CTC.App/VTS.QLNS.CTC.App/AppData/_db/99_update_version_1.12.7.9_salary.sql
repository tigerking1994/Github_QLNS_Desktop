/****** Object:  StoredProcedure [dbo].[sp_tl_update_dmcapbac_canbo]    Script Date: 5/17/2023 3:16:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_update_dmcapbac_canbo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_update_dmcapbac_canbo]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chitiet_kehoach]    Script Date: 5/17/2023 3:16:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_ct_chitiet_kehoach]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_ct_chitiet_kehoach]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop]    Script Date: 5/17/2023 3:57:18 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet]    Script Date: 5/17/2023 4:18:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet]    Script Date: 5/17/2023 4:18:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_tl_chungtu_chitiet]
	@maDonVi VARCHAR(MAX),
	@thang INT,
	@nam INT,
	@maCachTl NVARCHAR(50)
AS
BEGIN
	
	--SELECT Ma_Cot, pc.Ma_PhuCap INTO #tmpMapping
	--FROM 
	--(SELECT Ma_Cot, CONCAT('|', REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(CongThuc as nvarchar(max)),'+','|'),'-','|'),'*','|'),'/','|'),'(','|'),')','|'), '|') as CongThuc FROM TL_DM_Cach_TinhLuong_Chuan WHERE CongThuc IS NOT NULL) as tbl
	--INNER  JOIN TL_DM_PhuCap as pc on tbl.CongThuc LIKE CONCAT('%|', pc.Ma_PhuCap, '|%') ;


	SELECT Ma_Cot, pc.Ma_PhuCap into #tmp from (
		SELECT Ma_Cot, CONCAT('|', REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(CongThuc as nvarchar(max)),'+','|'),'-','|'),'*','|'),'/','|'),'(','|'),')','|'), '|') as CongThuc FROM TL_DM_Cach_TinhLuong_Chuan WHERE CongThuc IS NOT NULL) as tbl
		INNER  JOIN TL_DM_PhuCap as pc on tbl.CongThuc LIKE CONCAT('%|', pc.Ma_PhuCap, '|%') or pc.Parent in ('TIENAN', 'TIENAN2')

	select * into #tmpMapping
	FROM (
		select Ma_Cot, Ma_Cot as Ma_PhuCap from #tmp
		union 
		select Ma_PhuCap as Ma_Cot, Ma_PhuCap as Ma_PhuCap from #tmp
	) AS c

	SELECT mp.Ma_Cot, cb.Ma_CanBo, SUM(ISNULL(pc.HuongPC_SN, 0)) as HuongPC_SN INTO #tmpSoNgay
	FROM TL_DM_CanBo as cb
	INNER JOIN TL_CanBo_PhuCap as pc on cb.Ma_CanBo = pc.MA_CBO
	INNER JOIN #tmpMapping as mp on pc.MA_PHUCAP = mp.Ma_PhuCap
	WHERE cb.Parent in (SELECT * FROM f_split(@MaDonVi))
	AND Thang = @thang
	AND Nam = @nam
	GROUP BY mp.Ma_Cot, cb.Ma_CanBo;

	SELECT
		dsCapNhapBangLuong.Ma_CBo	AS MaDonVi,
		dsCapNhapBangLuong.Thang	AS Thang,
		dsCapNhapBangLuong.Nam		AS Nam,
		bangLuong.MA_PHUCAP			AS MaPhuCap,
		bangLuong.Ma_CB				AS MaCapBac,
		case when bangLuong.Ma_CB in ('3.1', '3.2', '3.3', '3.4', '3.5') then bangLuong.Ma_CB else capBac.Parent end AS Ngach,
		--SUM(case when ISNULL(cbpc.HuongPC_SN, 0) = 0 then dbo.fnTotalDayOfMonth(@thang,@nam) else cbpc.HuongPC_SN end) AS SoNgay,
		SUM( 
			case WHEN pc.Parent IN ('TIENAN', 'TIENAN2') AND ISNULL(cbpc1.HuongPC_SN, 0) <> 0 AND bHuongPc_Sn = 1 then cbpc.HuongPC_SN
			WHEN pc.Parent IN ('TIENAN', 'TIENAN2') THEN dbo.fnTotalDayOfMonth(@thang,@nam) else ISNULL(cbpc.HuongPC_SN, 0) end
		) AS SoNgay,
		SUM(
			case WHEN pc.Parent IN ('TIENAN', 'TIENAN2') AND ISNULL(cbpc1.HuongPC_SN, 0) <> 0 AND bHuongPc_Sn = 1 THEN ISNULL(cbpc1.HuongPC_SN, 0) * cbpc1.GIA_TRI
			WHEN pc.Parent IN ('TIENAN', 'TIENAN2') THEN dbo.fnTotalDayOfMonth(@thang,@nam) * cbpc1.GIA_TRI
			ELSE bangLuong.Gia_Tri END
		) AS GiaTri,
		COUNT(bangLuong.Ma_CBo)		AS SoNguoi
	INTO #LuongCapBac
	FROM (SELECT * FROM TL_BangLuong_Thang WHERE NAM = @nam AND THANG = @thang AND Gia_Tri != 0) bangLuong
	INNER JOIN TL_CanBo_PhuCap as cbpc1 on bangLuong.Ma_CBo = cbpc1.MA_CBO AND bangLuong.Ma_PhuCap = cbpc1.MA_PHUCAP
	INNER JOIN TL_DM_PhuCap as pc on bangLuong.Ma_PhuCap = pc.Ma_PhuCap
	LEFT JOIN #tmpSoNgay  as cbpc on bangLuong.Ma_PhuCap = cbpc.Ma_Cot AND bangLuong.Ma_CBo = cbpc.Ma_CanBo
	LEFT JOIN 
	(SELECT * FROM TL_DS_CapNhap_BangLuong WHERE Status = 1 AND NAM = @Nam AND THANG = @Thang AND Ma_CachTL IN (SELECT * FROM f_split(@maCachTl)) AND Ma_CBo IN (SELECT * FROM f_split(@MaDonVi))) dsCapNhapBangLuong 
	ON bangLuong.parent = dsCapNhapBangLuong.Id AND bangLuong.THANG = dsCapNhapBangLuong.Thang and bangLuong.NAM = dsCapNhapBangLuong.Nam 
	LEFT JOIN TL_DM_CapBac capBac ON bangLuong.Ma_CB = capBac.Ma_Cb		
	GROUP BY dsCapNhapBangLuong.Ma_CBo, dsCapNhapBangLuong.Thang, dsCapNhapBangLuong.Nam, bangLuong.MA_PHUCAP,bangLuong.Ma_CB, capBac.Parent
		
	SELECT
		luongCapBac.MaDonVi,
		phuCapMlns.XauNoiMa,
		phuCapMlns.Ma_Cb,
		SoNguoi,
		SoNgay,
		GiaTri,
		luongCapBac.Thang
		INTO #LuongCapBacMlns
	FROM TL_PhuCap_MLNS phuCapMlns
	JOIN #LuongCapBac luongCapBac ON phuCapMlns.Ma_Cb = luongCapBac.Ngach AND phuCapMlns.Ma_PhuCap = luongCapBac.MaPhuCap
	WHERE
		phuCapMlns.Nam = @nam

	Select Ma_DonVi, XauNoiMa, SUM(DDuToan) DuToan
	INTO #DataDuToan
	From TL_QT_ChungTuChiTiet ctchitiet
	Join TL_QT_ChungTu chungtu
	on chungtu.ID = ctchitiet.Id_ChungTu
	Where Nam = @nam
	And Ma_DonVi IN (SELECT * FROM f_split(@maCachTl))
	Group By XauNoiMa, Ma_DonVi

	SELECT
		luong.MaDonVi					AS MaDonVi,
		NEWID()							AS Id,
		iID_MLNS						AS MlnsId, 
		iID_MLNS_Cha					AS MlnsIdParent, 
		sXauNoiMa						AS XauNoiMa, 
		sLNS							AS Lns, 
		sL								AS L, 
		sK								AS K, 
		sM								AS M, 
		sTM								AS TM, 
		sTTM							AS TTM, 
		sNG								AS Ng, 
		sTNG							AS TNG, 
		sTNG1							AS TNG1, 
		sTNG2							AS TNG2, 
		sTNG3							AS TNG3, 
		sMoTa							AS Mota, 
		iNamLamViec						AS NamLamViec,
		bHangCha						AS BHangCha,
		sChiTietToi						AS ChiTietToi,
		CONVERT(decimal, SUM(SoNguoi))	AS SoNguoi,
		SUM(ISNULL(SoNgay, 0))			AS SoNgay,
		SUM(GiaTri)						AS TongCong,
		SUM(GiaTri)						AS DieuChinh,
		@maCachTl						AS MaCachTl,
		ISNULL(dataDuToan.DuToan, 0)	AS DDuToan,
		luong.Ma_Cb						AS MaCb,
		luong.Thang as Thang
	FROM NS_MucLucNganSach mlns
	JOIN #LuongCapBacMlns luong
		ON mlns.sXauNoiMa = luong.XauNoiMa
	LEFT JOIN #DataDuToan dataDuToan
		ON luong.XauNoiMa = dataDuToan.XauNoiMa and luong.MaDonVi = dataDuToan.Ma_DonVi
	WHERE
		sLNS IN ('1', '101', '1010000')
		AND iNamLamViec = @nam
		AND luong.Thang = @thang
	GROUP BY MaDonVi, iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, iNamLamViec, sChiTietToi, bHangCha, DuToan, luong.Ma_Cb, luong.Thang
	ORDER BY MaDonVi, sXauNoiMa

	DROP TABLE #LuongCapBac
	DROP TABLE #DataDuToan
	DROP TABLE #LuongCapBacMlns
	DROP TABLE #tmpSoNgay
	DROP TABLE #tmpMapping
END
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop]    Script Date: 5/17/2023 3:57:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop]
	@ListIdChungTuTongHop ntext,
	@IdChungTu nvarchar(100),
	@IdDonVi nvarchar(10),
	@TenDonVi nvarchar(250),
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int
AS
BEGIN

DECLARE @iIdChungTuOld uniqueidentifier,
	@iIdChungTuDuToanOld uniqueidentifier

SELECT TOP(1) @iIdChungTuOld = ol.ID, @iIdChungTuDuToanOld = case ol.IIdChungTuDuToan when '' then NULL else ol.IIdChungTuDuToan end
FROM TL_QT_ChungTu as ol
INNER JOIN (SELECT * FROM TL_QT_ChungTu WHERE ID = @IdChungTu) as nw 
on ol.Thang = (CASE WHEN nw.Thang = 1 THEN 12 ELSE nw.Thang - 1 END)
	AND ol.Nam = (CASE WHEN nw.Thang = 1 THEN nw.Nam - 1 ELSE nw.Nam END)
	AND ol.Ma_DonVi = nw.Ma_DonVi
	AND ol.sTongHop IS NOT NULL
ORDER BY ol.Ngay_tao DESC

CREATE TABLE #tmp(sXauNoiMa nvarchar(100), DDuToan decimal)

IF(@iIdChungTuOld IS NOT NULL AND @iIdChungTuDuToanOld IS NOT NULL)
BEGIN
	UPDATE TL_QT_ChungTu SET IIdChungTuDuToan = @iIdChungTuDuToanOld WHERE ID = @IdChungTu

	INSERT INTO #tmp(sXauNoiMa, DDuToan)
	SELECT DISTINCT XauNoiMa, ISNULL(DDuToan, 0)
	FROM TL_QT_ChungTuChiTiet 
	WHERE Id_ChungTu = @iIdChungTuOld AND ISNULL(DDuToan, 0) <> 0
END

INSERT INTO [dbo].[TL_QT_ChungTuChiTiet] ([Id_ChungTu] , [MLNS_Id] , [MLNS_Id_Parent] , [XauNoiMa] , [LNS] , [L] , [K] , [M] , [TM] , [TTM] , [NG] , [TNG] , [TNG1] , [TNG2] , [TNG3] , [MoTa] , [Chuong] , [NamNganSach] , [NguonNganSach] , [NamLamViec] , [iTrangThai] , [iThangQuy] , [Id_DonVi] , [TenDonVi] , [MucAn] , [GhiChu] , [DateCreated] , [UserCreator] , [DateModified] , [UserModifier] , [TongCong] , [BHangCha] , [ChiTietToi] , [SoNgay] , [SoNguoi] , [DieuChinh] , [MaCachTl], [DDuToan], [MaCb])
SELECT @idChungTu,
       MLNS_Id,
       MLNS_Id_Parent,
       XauNoiMa,
       LNS,
       L,
       K,
       M,
       TM,
       TTM,
       NG,
       TNG,
       TNG1,
       TNG2,
       TNG3,
       MoTa,
       NULL,
       @NamNganSach,
       @NguonNganSach,
       @NamLamViec,
       1,
       NULL,
       @IdDonVi,
       @TenDonVi,
       NULL,
       NULL,
       GETDATE(),
       NULL,
       NULL,
       NULL,
       sum(Isnull(TongCong, 0)),
       BHangCha,
       NULL,
       sum(Isnull(SoNgay, 0)),
       sum(Isnull(SoNguoi, 0)),
       sum(Isnull(DieuChinh, 0)),
	   MaCachTl,
	   ISNULL(tmp.DDuToan, 0),
	   ct.MaCb
FROM TL_QT_ChungTuChiTiet ct
LEFT JOIN #tmp as tmp on ct.XauNoiMa = tmp.sXauNoiMa
WHERE ct.Id_ChungTu in
    (SELECT *
     FROM f_split(@ListIdChungTuTongHop))
GROUP BY MLNS_Id,
         MLNS_Id_Parent,
         XauNoiMa,
         LNS,
         L,
         K,
         M,
         TM,
         TTM,
         NG,
         TNG,
         TNG1,
         TNG2,
         TNG3,
         MoTa,
         BHangCha,
		 MaCachTl,
		 tmp.sXauNoiMa,
		 ISNULL(tmp.DDuToan, 0),
		 ct.MaCb

	-- add du toan thang truoc da co
	INSERT INTO [dbo].[TL_QT_ChungTuChiTiet] ([Id_ChungTu] , [MLNS_Id] , [MLNS_Id_Parent] , [XauNoiMa] , [LNS] , [L] , [K] , [M] , [TM] , [TTM] , [NG] , [TNG] , [TNG1] , [TNG2] , [TNG3] , [MoTa] , [Chuong] , [NamNganSach] , [NguonNganSach] , [NamLamViec] , [iTrangThai] , [iThangQuy] , [Id_DonVi] , [TenDonVi] , [MucAn] , [GhiChu] , [DateCreated] , [UserCreator] , [DateModified] , [UserModifier] , [TongCong] , [BHangCha] , [ChiTietToi] , [SoNgay] , [SoNguoi] , [DieuChinh] , [MaCachTl], [DDuToan], [MaCb])
	SELECT @idChungTu, 
		ml.iID_MLNS, 
		ml.iID_MLNS_Cha, 
		tmp.sXauNoiMa, 
		ml.sLNS,
		ml.sL, 
		ml.sK, 
		ml.sM, 
		ml.sTM, 
		ml.sTTM, 
		ml.sNG,
		ml.sTNG, 
		ml.sTNG1, 
		ml.sTNG2, 
		ml.sTNG3,
		ml.sMoTa,
		NULL, 
		@NamNganSach,
       @NguonNganSach,
       @NamLamViec,
       1,
       NULL,
       @IdDonVi,
       @TenDonVi,
       NULL,
       NULL,
       GETDATE(),
       NULL,
       NULL,
       NULL,
       Isnull(TongCong, 0),
       ml.bHangCha,
       NULL,
       Isnull(SoNgay, 0),
       Isnull(SoNguoi, 0),
       Isnull(DieuChinh, 0),
	   '',
	   ISNULL(tmp.DDuToan, 0),
	   dt.MaCb
	FROM #tmp as tmp
	INNER JOIN NS_MucLucNganSach as ml on ml.iNamLamViec = @NamLamViec AND ml.sXauNoiMa = tmp.sXauNoiMa
	LEFT JOIN TL_QT_ChungTuChiTiet as dt on tmp.sXauNoiMa = dt.XauNoiMa AND dt.Id_ChungTu = @idChungTu
	WHERE dt.XauNoiMa IS NULL AND ISNULL(tmp.DDuToan, 0) <> 0

	DROP TABLE #tmp
END
;
GO

/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chitiet_kehoach]    Script Date: 5/17/2023 3:16:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_tl_get_data_ct_chitiet_kehoach]
@namNs int,
@nam int,
@idDonVi varchar(50)
AS
Begin
SELECT
  mlns.sXauNoiMa as XauNoiMa,
  mlns.sLNS as Lns,
  mlns.sL as L,
  mlns.sK as K,
  mlns.sM as M,
  mlns.sTM as Tm,
  mlns.sTTM as Ttm,
  mlns.sNG as Ng,
  mlns.sTNG as Tng,
  mlns.sMoTa as MoTa,
  SUM(chungTuChiTiet.TongNamTruoc) as TongNamTruoc,
  SUM(chungTuChiTiet.TongCong) as TongCong,
  SUM(chungTuChiTiet.DieuChinh) as DieuChinh,
  chungTuChiTiet.GhiChu as GhiChu,
  chungTuChiTiet.Id_DonVi as IdDonVi,
  chungTuChiTiet.NamLamViec as NamLamViec,
  mlns.BHangCha as BHangCha,
  chungTuChiTiet.TenDonVi as TenDonVi,
  chungTuChiTiet.Ngach as Ngach,
  chungTuChiTiet.MaPhuCap as MaPhuCap,
  mlns.iID_MLNS as MlnsId,
  mlns.iID_MLNS_Cha as MlnsIdParent,
  ChenhLech = null
FROM (SELECT * FROM NS_MucLucNganSach WHERE iNamLamViec = @namNs) mlns
LEFT JOIN (SELECT * FROM TL_QT_ChungTuChiTiet_KeHoach WHERE Id_DonVi = @idDonVi and NamLamViec = @nam) chungTuChiTiet ON mlns.sXauNoiMa = chungTuChiTiet.XauNoiMa 
WHERE sLNS IN ('1010000', '1', '101')
group by mlns.sXauNoiMa,
  mlns.sLNS,
  mlns.sL,
  mlns.sK,
  mlns.sM,
  mlns.sTM,
  mlns.sTTM,
  mlns.sNG,
  mlns.sTNG,
  mlns.sMoTa,
  --chungTuChiTiet.TongNamTruoc,
  --chungTuChiTiet.TongCong,
  --chungTuChiTiet.DieuChinh,
  chungTuChiTiet.GhiChu,
  chungTuChiTiet.Id_DonVi,
  chungTuChiTiet.NamLamViec,
  mlns.BHangCha,
  chungTuChiTiet.TenDonVi,
  chungTuChiTiet.Ngach,
  chungTuChiTiet.MaPhuCap,
  mlns.iID_MLNS,
  mlns.iID_MLNS_Cha
ORDER BY mlns.sXauNoiMa
End
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_update_dmcapbac_canbo]    Script Date: 5/17/2023 3:16:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_tl_update_dmcapbac_canbo]
@nam int,
@thang int,
@bIsDelete bit,
@capbacIds t_tbl_uniqueidentifier READONLY,
@sMaPhuCapChange nvarchar(MAX)
AS
BEGIN
--DECLARE @tmp as TABLE(sMaPhuCap nvarchar(200))
--INSERT INTO @tmp(sMaPhuCap) VALUES('PCTN_HS'), ('PCTEMTHU_TT'), ('PCNU_HS'), ('PCANQP_HS'), ('THANG_TCXN'), ('BHXHDV_HS'), ('BHXHCN_HS'), ('BHYTDV_HS'), ('BHYTCN_HS'), ('BHTNDV_HS'), ('BHTNCN_HS'), ('LHT_HS'), ('TILE_HUONG'), ('BHXHDVCS_HS'), ('BHYTDVCS_HS')

SELECT cb.* INTO #tmpCapBac
FROM @capbacIds as tmp
INNER JOIN TL_DM_CapBac as cb on tmp.Id = cb.Id

SELECT canbo.Ma_CanBo, capbac.*, canbo.Ngay_XN, canbo.Ngay_NN INTO #tmpCanBoPhuCap
FROM TL_DM_CanBo as canbo
INNER JOIN #tmpCapBac as capbac on canbo.Ma_CB = capbac.Ma_Cb
WHERE Thang = @thang AND Nam = @nam

IF(@bIsDelete = 0)
BEGIN
UPDATE pc
SET
GIA_TRI = (CASE WHEN pc.MA_PHUCAP = 'PCTN_HS' THEN (CASE WHEN tbl.Ma_Cb LIKE '0%' THEN 0 ELSE GIA_TRI END)
WHEN pc.MA_PHUCAP = 'PCTEMTHU_TT' THEN (CASE WHEN tbl.Ma_Cb NOT LIKE '0%' THEN 0 ELSE GIA_TRI END)
WHEN pc.MA_PHUCAP = 'PCNU_HS' THEN (CASE WHEN tbl.Ma_Cb NOT LIKE '0%' THEN 0 ELSE GIA_TRI END)
WHEN pc.MA_PHUCAP = 'PCANQP_HS' THEN (CASE WHEN tbl.Ma_Cb = '415' THEN 0.5
WHEN tbl.Ma_Cb = '413' THEN 0.3
ELSE GIA_TRI END)
WHEN pc.MA_PHUCAP = 'THANG_TCXN' THEN (CASE WHEN tbl.Ma_Cb LIKE '0%' AND tbl.Ngay_XN IS NOT NULL THEN ((DATEDIFF(MONTH, tbl.Ngay_NN, tbl.Ngay_XN)/12)*2) + (CASE WHEN (DATEDIFF(MONTH, tbl.Ngay_NN, tbl.Ngay_XN)%12) > 6 THEN 2 ELSE 1 END) ELSE GIA_TRI END)
WHEN pc.Ma_PhuCap = 'BHXHDVCS_HS' THEN ISNULL(Bhxh_Cq, 0)
WHEN pc.Ma_PhuCap = 'BHXHDV_HS' THEN ISNULL(Bhxh_Cq, 0)
WHEN pc.MA_PHUCAP = 'BHXHCN_HS' THEN ISNULL(Hs_Bhxh, 0)
WHEN pc.MA_PHUCAP = 'BHYTDVCS_HS' THEN ISNULL(Bhyt_Cq, 0)
WHEN pc.MA_PHUCAP = 'BHYTDV_HS' THEN ISNULL(Bhyt_Cq, 0)
WHEN pc.MA_PHUCAP = 'BHYTCN_HS' THEN ISNULL(Hs_Bhyt, 0)
WHEN pc.MA_PHUCAP = 'BHTNDV_HS' THEN ISNULL(Bhtn_Cq, 0)
WHEN pc.MA_PHUCAP = 'BHTNCN_HS' THEN ISNULL(Hs_Bhtn, 0)
WHEN pc.MA_PHUCAP = 'LHT_HS' THEN ISNULL(tbl.Lht_Hs, 0)
WHEN pc.MA_PHUCAP = 'TILE_HUONG' THEN ISNULL(tbl.TiLeHuong, 0)
ELSE GIA_TRI END)
FROM #tmpCanBoPhuCap as tbl
INNER JOIN TL_CanBo_PhuCap as pc on tbl.Ma_CanBo = pc.MA_CBO
WHERE pc.MA_PHUCAP in (select * from f_split(@sMaPhuCapChange))
--INNER JOIN @tmp as tmp on pc.MA_PHUCAP = tmp.sMaPhuCap

UPDATE cb
SET
HeSoLuong = tbl.Lht_Hs,
PCCV = (CASE WHEN tbl.Ma_Cb LIKE '0%' THEN 1 ELSE PCCV END)
FROM #tmpCanBoPhuCap as tbl
INNER JOIN TL_DM_CanBo as cb on tbl.Ma_CanBo = cb.Ma_CanBo
INNER JOIN TL_CanBo_PhuCap as pc on tbl.Ma_CanBo = pc.MA_CBO
--WHERE pc.MA_PHUCAP = 'LHT_HS'
WHERE pc.MA_PHUCAP = 'LHT_HS' and pc.MA_PHUCAP in (select * from f_split(@sMaPhuCapChange))
END
ELSE
BEGIN
UPDATE pc
SET
GIA_TRI = (CASE
WHEN pc.MA_PHUCAP = 'PCANQP_HS' THEN 0
WHEN pc.MA_PHUCAP = 'THANG_TCXN' THEN (CASE WHEN tbl.Ma_Cb LIKE '0%' AND tbl.Ngay_XN IS NOT NULL THEN 0 ELSE GIA_TRI END) -- TinhThangHuongTcxn()
WHEN pc.Ma_PhuCap = 'BHXHDVCS_HS' THEN 0
WHEN pc.Ma_PhuCap = 'BHXHDV_HS' THEN 0
WHEN pc.MA_PHUCAP = 'BHXHCN_HS' THEN 0
WHEN pc.MA_PHUCAP = 'BHYTDVCS_HS' THEN 0
WHEN pc.MA_PHUCAP = 'BHYTDV_HS' THEN 0
WHEN pc.MA_PHUCAP = 'BHYTCN_HS' THEN 0
WHEN pc.MA_PHUCAP = 'BHTNDV_HS' THEN 0
WHEN pc.MA_PHUCAP = 'BHTNCN_HS' THEN 0
WHEN pc.MA_PHUCAP = 'LHT_HS' THEN 0
WHEN pc.MA_PHUCAP = 'TILE_HUONG' THEN 0
ELSE GIA_TRI END)
FROM #tmpCanBoPhuCap as tbl
INNER JOIN TL_CanBo_PhuCap as pc on tbl.Ma_CanBo = pc.MA_CBO
--INNER JOIN @tmp as tmp on pc.MA_PHUCAP = tmp.sMaPhuCap
WHERE pc.MA_PHUCAP in (select * from f_split(@sMaPhuCapChange))

UPDATE cb
SET
HeSoLuong = 0
FROM #tmpCanBoPhuCap as tbl
INNER JOIN TL_DM_CanBo as cb on tbl.Ma_CanBo = cb.Ma_CanBo
INNER JOIN TL_CanBo_PhuCap as pc on tbl.Ma_CanBo = pc.MA_CBO
--WHERE pc.MA_PHUCAP = 'LHT_HS' 
WHERE pc.MA_PHUCAP = 'LHT_HS' and pc.MA_PHUCAP in (select * from f_split(@sMaPhuCapChange))
END

DROP TABLE #tmpCapBac
DROP TABLE #tmpCanBoPhuCap
END
;
;
GO
