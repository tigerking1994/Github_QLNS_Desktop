/****** Object:  StoredProcedure [dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop_nq104]    Script Date: 4/8/2024 4:16:23 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet_nq104]    Script Date: 4/8/2024 4:16:23 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_chungtu_chitiet_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_chungtu_chitiet_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_giaithich_bangso_export_nq104]    Script Date: 4/8/2024 4:44:58 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_giaithich_bangso_export_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_giaithich_bangso_export_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chi_tiet_export_nq104]    Script Date: 4/8/2024 4:44:58 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_ct_chi_tiet_export_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_ct_chi_tiet_export_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chi_tiet_nq104]    Script Date: 4/8/2024 5:09:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_ct_chi_tiet_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_ct_chi_tiet_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet_nq104]    Script Date: 4/8/2024 4:16:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_chungtu_chitiet_nq104]
	@maDonVi VARCHAR(MAX),
	@thang INT,
	@nam INT,
	@maCachTl NVARCHAR(50)
AS
BEGIN
	
	--SELECT Ma_Cot, pc.Ma_PhuCap INTO #tmpMapping
	--FROM 
	--(SELECT Ma_Cot, CONCAT('|', REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(CongThuc as nvarchar(max)),'+','|'),'-','|'),'*','|'),'/','|'),'(','|'),')','|'), '|') as CongThuc FROM TL_DM_Cach_TinhLuong_Chuan_NQ104 WHERE CongThuc IS NOT NULL) as tbl
	--INNER  JOIN TL_DM_PhuCap_NQ104 as pc on tbl.CongThuc LIKE CONCAT('%|', pc.Ma_PhuCap, '|%') ;
	DECLARE @checkTongHop INT; 
	SELECT @checkTongHop = count(*) FROM f_split(@maCachTl);


	SELECT Ma_Cot, pc.Ma_PhuCap into #tmp from (
		SELECT Ma_Cot, CONCAT('|', REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(CongThuc as nvarchar(max)),'+','|'),'-','|'),'*','|'),'/','|'),'(','|'),')','|'), '|') as CongThuc FROM TL_DM_Cach_TinhLuong_Chuan_NQ104 WHERE CongThuc IS NOT NULL) as tbl
		INNER  JOIN TL_DM_PhuCap_NQ104 as pc on tbl.CongThuc LIKE CONCAT('%|', pc.Ma_PhuCap, '|%') or pc.Parent in ('TIENAN', 'TIENAN2')

	select * into #tmpMapping
	FROM (
		select Ma_Cot, Ma_Cot as Ma_PhuCap from #tmp
		union 
		select Ma_PhuCap as Ma_Cot, Ma_PhuCap as Ma_PhuCap from #tmp
	) AS c

	SELECT mp.Ma_Cot, cb.Ma_CanBo, SUM(ISNULL(pc.HuongPC_SN, 0)) as HuongPC_SN INTO #tmpSoNgay
	FROM TL_DM_CanBo_NQ104 as cb
	INNER JOIN TL_CanBo_PhuCap_NQ104 as pc on cb.Ma_CanBo = pc.MA_CBO
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
		SUM(CASE WHEN @checkTongHop = 2 AND dsCapNhapBangLuong.Ma_CachTL ='CACH5' THEN 0 ELSE 1 END)	AS SoNguoi
	INTO #LuongCapBac
	FROM (SELECT * FROM TL_BangLuong_Thang_NQ104 WHERE NAM = @nam AND THANG = @thang AND Gia_Tri != 0) bangLuong
	INNER JOIN TL_CanBo_PhuCap_NQ104 as cbpc1 on bangLuong.Ma_CBo = cbpc1.MA_CBO AND bangLuong.Ma_PhuCap = cbpc1.MA_PHUCAP
	INNER JOIN TL_DM_PhuCap_NQ104 as pc on bangLuong.Ma_PhuCap = pc.Ma_PhuCap
	LEFT JOIN #tmpSoNgay  as cbpc on bangLuong.Ma_PhuCap = cbpc.Ma_Cot AND bangLuong.Ma_CBo = cbpc.Ma_CanBo
	LEFT JOIN 
	(SELECT * FROM TL_DS_CapNhap_BangLuong_NQ104 WHERE Status = 1 AND NAM = @Nam AND THANG = @Thang AND Ma_CachTL IN (SELECT * FROM f_split(@maCachTl)) AND Ma_CBo IN (SELECT * FROM f_split(@MaDonVi))) dsCapNhapBangLuong 
	ON bangLuong.parent = dsCapNhapBangLuong.Id AND bangLuong.THANG = dsCapNhapBangLuong.Thang and bangLuong.NAM = dsCapNhapBangLuong.Nam 
	LEFT JOIN TL_DM_CapBac_NQ104 capBac ON bangLuong.Ma_CB = capBac.Ma_Cb		
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
	FROM TL_PhuCap_MLNS_NQ104 phuCapMlns
	JOIN #LuongCapBac luongCapBac ON phuCapMlns.Ma_Cb = luongCapBac.Ngach AND phuCapMlns.Ma_PhuCap = luongCapBac.MaPhuCap
	WHERE
		phuCapMlns.Nam = @nam

	Select Ma_DonVi, XauNoiMa, SUM(DDuToan) DuToan
	INTO #DataDuToan
	From [TL_QT_ChungTuChiTiet_NQ104] ctchitiet
	Join TL_QT_ChungTu_NQ104 chungtu
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
/****** Object:  StoredProcedure [dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop_nq104]    Script Date: 4/8/2024 4:16:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop_nq104]
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

SELECT TOP(1) @iIdChungTuOld = ol.ID, @iIdChungTuDuToanOld = case ol.IIdChungTuDuToan when '' then NULL else REPLACE(ol.IIdChungTuDuToan, ';', '') end
FROM TL_QT_ChungTu_NQ104 as ol
INNER JOIN (SELECT * FROM TL_QT_ChungTu_NQ104 WHERE ID = @IdChungTu) as nw 
on ol.Thang = (CASE WHEN nw.Thang = 1 THEN 12 ELSE nw.Thang - 1 END)
	AND ol.Nam = (CASE WHEN nw.Thang = 1 THEN nw.Nam - 1 ELSE nw.Nam END)
	AND ol.Ma_DonVi = nw.Ma_DonVi
	AND ol.sTongHop IS NOT NULL
ORDER BY ol.Ngay_tao DESC

CREATE TABLE #tmp(sXauNoiMa nvarchar(100), DDuToan decimal)

IF(@iIdChungTuOld IS NOT NULL AND @iIdChungTuDuToanOld IS NOT NULL)
BEGIN
	UPDATE TL_QT_ChungTu_NQ104 SET IIdChungTuDuToan = @iIdChungTuDuToanOld WHERE ID = @IdChungTu

	INSERT INTO #tmp(sXauNoiMa, DDuToan)
	SELECT XauNoiMa, Sum(ISNULL(DDuToan, 0))
	FROM TL_QT_ChungTuChiTiet_NQ104 
	WHERE Id_ChungTu = @iIdChungTuOld AND ISNULL(DDuToan, 0) <> 0  AND (MaCachTl = '' or MaCachTl is null)
	group by XauNoiMa
END

--INSERT INTO [dbo].[TL_QT_ChungTu_NQ104ChiTiet] ([Id_ChungTu] , [MLNS_Id] , [MLNS_Id_Parent] , [XauNoiMa] , [LNS] , [L] , [K] , [M] , [TM] , [TTM] , [NG] , [TNG] , [TNG1] , [TNG2] , [TNG3] , [MoTa] , [Chuong] , [NamNganSach] , [NguonNganSach] , [NamLamViec] , [iTrangThai] , [iThangQuy] , [Id_DonVi] , [TenDonVi] , [MucAn] , [GhiChu] , [DateCreated] , [UserCreator] , [DateModified] , [UserModifier] , [TongCong] , [BHangCha] , [ChiTietToi] , [SoNgay] , [SoNguoi] , [DieuChinh] , [MaCachTl], [DDuToan], [MaCb])
INSERT INTO [dbo].TL_QT_ChungTuChiTiet_NQ104 ([Id_ChungTu] , [MLNS_Id] , [MLNS_Id_Parent] , [XauNoiMa] , [LNS] , [L] , [K] , [M] , [TM] , [TTM] , [NG] , [TNG] , [TNG1] , [TNG2] , [TNG3] , [MoTa] , [Chuong] , [NamNganSach] , [NguonNganSach] , [NamLamViec] , [iTrangThai] , [iThangQuy] , [Id_DonVi] , [TenDonVi] , [MucAn] , [GhiChu] , [DateCreated] , [UserCreator] , [DateModified] , [UserModifier] , [TongCong] , [BHangCha] , [ChiTietToi] , [SoNgay] , [SoNguoi] , [DieuChinh] , [MaCachTl], [DDuToan])
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
	   ISNULL(tmp.DDuToan, 0)
	   --,ct.MaCb
FROM TL_QT_ChungTuChiTiet_NQ104 ct
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
		 ISNULL(tmp.DDuToan, 0)
		 --,ct.MaCb

	-- add du toan thang truoc da co
	--INSERT INTO [dbo].[TL_QT_ChungTu_NQ104ChiTiet] ([Id_ChungTu] , [MLNS_Id] , [MLNS_Id_Parent] , [XauNoiMa] , [LNS] , [L] , [K] , [M] , [TM] , [TTM] , [NG] , [TNG] , [TNG1] , [TNG2] , [TNG3] , [MoTa] , [Chuong] , [NamNganSach] , [NguonNganSach] , [NamLamViec] , [iTrangThai] , [iThangQuy] , [Id_DonVi] , [TenDonVi] , [MucAn] , [GhiChu] , [DateCreated] , [UserCreator] , [DateModified] , [UserModifier] , [TongCong] , [BHangCha] , [ChiTietToi] , [SoNgay] , [SoNguoi] , [DieuChinh] , [MaCachTl], [DDuToan], [MaCb])
	INSERT INTO [dbo].TL_QT_ChungTuChiTiet_NQ104 ([Id_ChungTu] , [MLNS_Id] , [MLNS_Id_Parent] , [XauNoiMa] , [LNS] , [L] , [K] , [M] , [TM] , [TTM] , [NG] , [TNG] , [TNG1] , [TNG2] , [TNG3] , [MoTa] , [Chuong] , [NamNganSach] , [NguonNganSach] , [NamLamViec] , [iTrangThai] , [iThangQuy] , [Id_DonVi] , [TenDonVi] , [MucAn] , [GhiChu] , [DateCreated] , [UserCreator] , [DateModified] , [UserModifier] , [TongCong] , [BHangCha] , [ChiTietToi] , [SoNgay] , [SoNguoi] , [DieuChinh] , [MaCachTl], [DDuToan])
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
	   ISNULL(tmp.DDuToan, 0)
	   --,dt.MaCb
	FROM #tmp as tmp
	INNER JOIN NS_MucLucNganSach as ml on ml.iNamLamViec = @NamLamViec AND ml.sXauNoiMa = tmp.sXauNoiMa
	LEFT JOIN TL_QT_ChungTuChiTiet_NQ104 as dt on tmp.sXauNoiMa = dt.XauNoiMa AND dt.Id_ChungTu = @idChungTu
	WHERE dt.XauNoiMa IS NULL AND ISNULL(tmp.DDuToan, 0) <> 0

	DROP TABLE #tmp
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chi_tiet_export_nq104]    Script Date: 4/8/2024 4:44:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_tl_get_data_ct_chi_tiet_export_nq104]
	@lstId nvarchar(max),
	@lstCach nvarchar(100),
	@nam int,
	@isSummary bit,
	@maDonViTongHop nvarchar(100)
AS
BEGIN
	CREATE TABLE #tmp(id nvarchar(100))
	DECLARE @isHaveCachTinhLuong bit = 0

	if(ISNULL(@lstCach, '') <> '')
	BEGIN
		INSERT INTO #tmp(id)
		SELECT *
		FROM f_split(@lstCach)

		SET @isHaveCachTinhLuong = 1
	END

	SELECT tbl.ID, Thang INTO #tblMaxThang
	FROM f_split(@lstId) as tmp
	INNER JOIN TL_QT_ChungTu_NQ104 as tbl on tmp.Item = tbl.ID;

	select DISTINCT XauNoiMa  into #tmpPcMlns FROM TL_PhuCap_MLNS_NQ104 WHERE Nam = @nam;

	with ctct as (
	  select XauNoiMa,MaCachTl,  Sum(TongCong) AS TongCong,
		   Sum(DieuChinh) AS DieuChinh,
		 ISNULL(dt.DDuToan, 0) As DDuToan, SUM(SoNguoi) AS SoNguoi,SUM(SoNgay) as SoNgay
	  from  TL_QT_ChungTuChiTiet_NQ104 as dt 
	  left join TL_QT_ChungTu_NQ104 as tbl on tbl.Id = dt.Id_ChungTu
	  where  (ISNULL(@lstId, '') <> '' AND tbl.ID in (SELECT *  FROM f_split(@lstId)))
		AND (((dt.MaCachTl = '' or dt.MaCachTl is null) AND @isHaveCachTinhLuong = 0) OR (@isHaveCachTinhLuong = 1 AND dt.MaCachTl in (SELECT id  FROM #tmp)))
	  group by dt.XauNoiMa, dt.MaCachTl, ISNULL(dt.DDuToan, 0)
	),
	--lstSoNguoi as (
	--	SELECT XauNoiMa,
	--		SUM(SoNguoi) AS SoNguoi,SUM(SoNgay) as SoNgay
	--	from TL_QT_ChungTu_NQ104 as tbl
	--	INNER JOIN TL_QT_ChungTuChiTiet_NQ104 as dt on tbl.Id = dt.Id_ChungTu
	--	where tbl.ID in (SELECT ID FROM #tblMaxThang)
	--	AND ((@isHaveCachTinhLuong = 0 AND (dt.MaCachTl = '' or dt.MaCachTl is null)) OR (@isHaveCachTinhLuong = 1 AND dt.MaCachTl in (SELECT id  FROM #tmp)))
	--	group by XauNoiMa
	--)
	--,
	phucapMlns as (
		SELECT TL_PhuCap_MLNS_NQ104.XauNoiMa, min(TL_PhuCap_MLNS_NQ104.Ma_Cb) as Ma_Cb FROM #tmpPcMlns 
		left join TL_PhuCap_MLNS_NQ104 on #tmpPcMlns.XauNoiMa = TL_PhuCap_MLNS_NQ104.XauNoiMa
		WHERE Nam = @nam
		group by TL_PhuCap_MLNS_NQ104.XauNoiMa
	),
	ctTongHop as (
		select DDuToan, sum(DieuChinh) DieuChinh, SUM(SoNguoi) AS SoNguoi,SUM(SoNgay) as SoNgay, XauNoiMa from TL_QT_ChungTuChiTiet_NQ104 ctct
		left join TL_QT_ChungTu_NQ104 ct on ctct.Id_ChungTu = ct.ID where CHARINDEX(ct.sTongHop, @lstId, 0) > 0 and NamLamViec = @nam and @maDonViTongHop = ct.Ma_DonVi 
		AND (((ctct.MaCachTl = '' or ctct.MaCachTl is null) AND @isHaveCachTinhLuong = 0) OR (@isHaveCachTinhLuong = 1 AND ctct.MaCachTl in (SELECT id  FROM #tmp)))
		group by XauNoiMa, DDuToan
	)
SELECT 
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
       mlns.sTNG AS TNG,
       mlns.sTNG1 AS TNG1,
       mlns.sTNG2 AS TNG2,
       mlns.sTNG3 AS TNG3,
       sMoTa AS Mota,
       MaCachTl as MaCachTl,
       iNamLamViec AS NamLamViec,
       mlns.bHangCha AS BHangCha,
       sChiTietToi AS ChiTietToi,
       TongCong,
	   case when @isSummary = 1 then ctTongHop.SoNguoi when @isSummary = 0 then ctct.SoNguoi end as SoNguoi,
	   case when @isSummary = 1 then ctTongHop.SoNgay when @isSummary = 0 then ctct.SoNgay end as SoNgay,
       case when @isSummary = 1 then ctTongHop.DieuChinh when @isSummary = 0 then ctct.DieuChinh end as DieuChinh,
	   case when @isSummary = 1 then ctTongHop.DDuToan when @isSummary = 0 then  ctct.DDuToan end as DDuToan

FROM NS_MucLucNganSach mlns  
LEFT JOIN ctct ctct ON mlns.sXauNoiMa = ctct.XauNoiMa
--LEFT JOIN lstSoNguoi as sn on mlns.sXauNoiMa = sn.XauNoiMa 
LEFT JOIN ctTongHop on mlns.sXauNoiMa = ctTongHop.XauNoiMa

WHERE sLNS IN ('1',
               '101',
               '1010000')
  AND iNamLamViec = @nam

order by XauNoiMa

DROP TABLE #tblMaxThang
DROP TABLE #tmp
drop table #tmpPcMlns
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_giaithich_bangso_export_nq104]    Script Date: 4/8/2024 4:44:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_tl_get_data_giaithich_bangso_export_nq104]
	@lstId nvarchar(max),
	@lstCach nvarchar(100),
	@nam int,
	@isSummary bit,
	@maDonViTongHop nvarchar(100)
AS
BEGIN
	CREATE TABLE #tmp(id nvarchar(100))
	DECLARE @isHaveCachTinhLuong bit = 0

	if(ISNULL(@lstCach, '') <> '')
	BEGIN
		INSERT INTO #tmp(id)
		SELECT *
		FROM f_split(@lstCach)

		SET @isHaveCachTinhLuong = 1
	END

	SELECT tbl.ID, Thang INTO #tblMaxThang
	FROM f_split(@lstId) as tmp
	INNER JOIN TL_QT_ChungTu_NQ104 as tbl on tmp.Item = tbl.ID;


	with ctct as (
	  select dt.XauNoiMa,  MaCachTl, MaCb,  Sum(TongCong) AS TongCong,
		   Sum(DieuChinh) AS DieuChinh,
		 Sum(ISNULL(dt.DDuToan, 0)) As DDuToan
	  from TL_QT_ChungTu_NQ104 as tbl
	  INNER JOIN TL_QT_ChungTuChiTiet_NQ104 as dt on tbl.Id = dt.Id_ChungTu
	  where  (ISNULL(@lstId, '') <> '' AND tbl.ID in (SELECT *  FROM f_split(@lstId)))
		AND ((dt.MaCachTl = '' AND @isHaveCachTinhLuong = 0) OR (@isHaveCachTinhLuong = 1 AND dt.MaCachTl in (SELECT id  FROM #tmp)))
	  group by dt.XauNoiMa, dt.MaCachTl, dt.MaCb
	),
	ctTongHop as (
		select Sum(DDuToan) as DDuToan, sum(DieuChinh) DieuChinh, XauNoiMa from TL_QT_ChungTuChiTiet_NQ104 ctct
		left join TL_QT_ChungTu_NQ104 ct on ctct.Id_ChungTu = ct.ID where CHARINDEX(ct.sTongHop, @lstId, 0) > 0 and NamLamViec = @nam and @maDonViTongHop = ct.Ma_DonVi 
		AND (((ctct.MaCachTl = '' or ctct.MaCachTl is null) AND @isHaveCachTinhLuong = 0) OR (@isHaveCachTinhLuong = 1 AND ctct.MaCachTl in (SELECT id  FROM #tmp)))
		group by XauNoiMa
	)

SELECT 
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
       mlns.sTNG AS TNG,
       mlns.sTNG1 AS TNG1,
       mlns.sTNG2 AS TNG2,
       mlns.sTNG3 AS TNG3,
	   ctct.MaCb as MaCb,
       sMoTa AS Mota,
       MaCachTl as MaCachTl,
       iNamLamViec AS NamLamViec,
       mlns.bHangCha AS BHangCha,
       sChiTietToi AS ChiTietToi,
       TongCong,
       case when @isSummary = 1 then ctTongHop.DieuChinh when @isSummary = 0 then ctct.DieuChinh end as DieuChinh,
	   case when @isSummary = 1 then ctTongHop.DDuToan when @isSummary = 0 then  ctct.DDuToan end as DuToan,
	 ctct.MaCb as MaCb,
	 capbac.Parent as MaCbCha
FROM NS_MucLucNganSach mlns  
LEFT JOIN ctct ctct ON mlns.sXauNoiMa = ctct.XauNoiMa
LEFT JOIN TL_DM_CapBac capbac ON ctct.MaCb = capbac.Ma_Cb
LEFT JOIN ctTongHop on mlns.sXauNoiMa = ctTongHop.XauNoiMa

WHERE sLNS IN ('1',
               '101',
               '1010000')
  AND iNamLamViec = @nam
  and bHangCha = 0

order by XauNoiMa

DROP TABLE #tblMaxThang
DROP TABLE #tmp
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chi_tiet_nq104]    Script Date: 4/8/2024 5:09:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_get_data_ct_chi_tiet_nq104] @idChungTu nvarchar(MAX),
                                                       @nam int, @maCachTl nvarchar(50) AS BEGIN

with ctct as (
  select Id as Id, XauNoiMa,MaCachTl,  Sum(TongCong) AS TongCong,
       convert(decimal,Sum(SoNguoi)) as SoNguoi,
	   convert(decimal,Sum(SoNgay)) as SoNgay,
       Sum(DieuChinh) AS DieuChinh,
     Sum(DDuToan) As DDuToan,
	 MaCb
  from TL_QT_ChungTuChiTiet_NQ104 
  where   Id_ChungTu in (SELECT *  FROM f_split(@idChungTu))
    AND (MaCachTl in (SELECT *  FROM f_split(@maCachTl)) or (MaCachTl is null and @maCachTl = ''))
  group by id, XauNoiMa, MaCachTl, MaCb
)
SELECT 
     ctct.Id as Id,
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
     DDuToan, 
	 SoNgay,
	 MaCb,
	 cb.Ten_Cb as LoaiDoiTuong
FROM NS_MucLucNganSach mlns  
LEFT JOIN ctct ctct ON mlns.sXauNoiMa = ctct.XauNoiMa
left join tl_dm_capbac cb on ctct.MaCb = cb.Ma_Cb

WHERE sLNS IN ('1',
               '101',
               '1010000')
  AND iNamLamViec = @nam

order by mlns.sXauNoiMa

END
;
;
;
;
;
;
GO
