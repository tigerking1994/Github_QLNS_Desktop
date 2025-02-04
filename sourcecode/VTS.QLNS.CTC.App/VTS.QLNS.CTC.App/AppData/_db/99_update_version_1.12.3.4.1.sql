/****** Object:  StoredProcedure [dbo].[sp_tl_update_dmphucap_canbo]    Script Date: 28/11/2022 2:17:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_update_dmphucap_canbo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_update_dmphucap_canbo]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_update_dmcapbac_canbo]    Script Date: 28/11/2022 2:17:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_update_dmcapbac_canbo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_update_dmcapbac_canbo]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_update_dmcapbac_canbo]    Script Date: 28/11/2022 2:17:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_tl_update_dmcapbac_canbo]
@nam int,
@thang int,
@bIsDelete bit,
@capbacIds t_tbl_uniqueidentifier READONLY
AS
BEGIN
	DECLARE @tmp as TABLE(sMaPhuCap nvarchar(200))
	INSERT INTO @tmp(sMaPhuCap) VALUES('PCTN_HS'), ('PCTEMTHU_TT'), ('PCNU_HS'), ('PCANQP_HS'), ('THANG_TCXN'), ('BHXHDV_HS'), ('BHXHCN_HS'), ('BHYTDV_HS'), ('BHYTCN_HS'), ('BHTNDV_HS'), ('BHTNCN_HS'), ('LHT_HS'), ('TILE_HUONG')

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
							WHEN pc.MA_PHUCAP = 'THANG_TCXN' THEN (CASE WHEN tbl.Ma_Cb LIKE '0%' AND tbl.Ngay_XN IS NOT NULL THEN  ((DATEDIFF(MONTH, tbl.Ngay_NN, tbl.Ngay_XN)/12)*2) + (CASE WHEN (DATEDIFF(MONTH, tbl.Ngay_NN, tbl.Ngay_XN)%12) > 6 THEN 2 ELSE 1 END) ELSE GIA_TRI END)
							WHEN pc.Ma_PhuCap = 'BHXHDV_HS' THEN ISNULL(Bhxh_Cq, 0)
							WHEN pc.MA_PHUCAP = 'BHXHCN_HS' THEN ISNULL(Hs_Bhxh, 0)
							WHEN pc.MA_PHUCAP = 'BHYTDV_HS' THEN ISNULL(Bhyt_Cq, 0)
							WHEN pc.MA_PHUCAP = 'BHYTCN_HS' THEN ISNULL(Hs_Bhyt, 0)
							WHEN pc.MA_PHUCAP = 'BHTNDV_HS' THEN ISNULL(Bhtn_Cq, 0)
							WHEN pc.MA_PHUCAP = 'BHTNCN_HS' THEN ISNULL(Hs_Bhtn, 0)
							WHEN pc.MA_PHUCAP = 'LHT_HS' THEN ISNULL(tbl.Lht_Hs, 0)
							WHEN pc.MA_PHUCAP = 'TILE_HUONG' THEN ISNULL(tbl.TiLeHuong, 0)
							ELSE GIA_TRI END)
		FROM #tmpCanBoPhuCap as tbl
		INNER JOIN TL_CanBo_PhuCap as pc on tbl.Ma_CanBo = pc.MA_CBO
		INNER JOIN @tmp	as tmp on pc.MA_PHUCAP = tmp.sMaPhuCap

		UPDATE cb
		SET
			HeSoLuong = tbl.Lht_Hs,
			PCCV = (CASE WHEN tbl.Ma_Cb LIKE '0%' THEN 1 ELSE PCCV END)
		FROM #tmpCanBoPhuCap as tbl
		INNER JOIN TL_DM_CanBo as cb on tbl.Ma_CanBo = cb.Ma_CanBo
		INNER JOIN TL_CanBo_PhuCap as pc on tbl.Ma_CanBo = pc.MA_CBO
		WHERE pc.MA_PHUCAP = 'LHT_HS'
	END
	ELSE
	BEGIN
		UPDATE pc
		SET 
			GIA_TRI = (CASE 
							WHEN pc.MA_PHUCAP = 'PCANQP_HS' THEN 0
							WHEN pc.MA_PHUCAP = 'THANG_TCXN' THEN (CASE WHEN tbl.Ma_Cb LIKE '0%' AND tbl.Ngay_XN IS NOT NULL THEN 0 ELSE GIA_TRI END) -- TinhThangHuongTcxn()
							WHEN pc.Ma_PhuCap = 'BHXHDV_HS' THEN 0
							WHEN pc.MA_PHUCAP = 'BHXHCN_HS' THEN 0
							WHEN pc.MA_PHUCAP = 'BHYTDV_HS' THEN 0
							WHEN pc.MA_PHUCAP = 'BHYTCN_HS' THEN 0
							WHEN pc.MA_PHUCAP = 'BHTNDV_HS' THEN 0
							WHEN pc.MA_PHUCAP = 'BHTNCN_HS' THEN 0
							WHEN pc.MA_PHUCAP = 'LHT_HS' THEN 0
							WHEN pc.MA_PHUCAP = 'TILE_HUONG' THEN 0
							ELSE GIA_TRI END)
		FROM #tmpCanBoPhuCap as tbl
		INNER JOIN TL_CanBo_PhuCap as pc on tbl.Ma_CanBo = pc.MA_CBO
		INNER JOIN @tmp	as tmp on pc.MA_PHUCAP = tmp.sMaPhuCap

		UPDATE cb
		SET
			HeSoLuong = 0
		FROM #tmpCanBoPhuCap as tbl
		INNER JOIN TL_DM_CanBo as cb on tbl.Ma_CanBo = cb.Ma_CanBo
		INNER JOIN TL_CanBo_PhuCap as pc on tbl.Ma_CanBo = pc.MA_CBO
		WHERE pc.MA_PHUCAP = 'LHT_HS'
	END

	DROP TABLE #tmpCapBac
	DROP TABLE #tmpCanBoPhuCap
END
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_update_dmphucap_canbo]    Script Date: 28/11/2022 2:17:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_tl_update_dmphucap_canbo]
@nam int,
@thang int,
@bIsDelete bit,
@phucapIds t_tbl_uniqueidentifier READONLY
AS
BEGIN
	SELECT Ma_PhuCap, Gia_Tri INTO #tmpPC
	FROM @phucapIds as tmp
	INNER JOIN TL_DM_PhuCap as pc on tmp.Id = pc.Id
	
	SELECT cb.Ma_CanBo, pc.Gia_Tri, pc.Ma_PhuCap INTO #tmpCanBoPC
	FROM TL_DM_CanBo as cb, #tmpPC as pc
	WHERE cb.Nam = @nam AND cb.Thang = @thang

	IF(@bIsDelete = 0)
	BEGIN
		CREATE TABLE #tmpUpdate(MaCB nvarchar(100), MaPC nvarchar(500))

		UPDATE tbl
		SET GIA_TRI = pc.Gia_Tri
		OUTPUT inserted.MA_CBO, inserted.MA_PHUCAP INTO #tmpUpdate(MaCB, MaPC)
		FROM TL_CanBo_PhuCap as tbl
		INNER JOIN #tmpCanBoPC as pc ON tbl.MA_PHUCAP = pc.Ma_PhuCap AND tbl.MA_CBO = pc.Ma_CanBo

		INSERT INTO TL_CanBo_PhuCap(bSaoChep, CHON, CONG_THUC, GIA_TRI, HE_SO, HuongPC_SN, MA_CBO, MA_KMCP, MA_PHUCAP, PHANTRAM_CT)
		SELECT pc.bSaoChep, pc.CHON, pc.CONG_THUC, tbl.Gia_Tri, HE_SO, HuongPC_SN, tbl.Ma_CanBo, MA_KMCP, tbl.Ma_PhuCap, PHANTRAM_CT
		FROM #tmpCanBoPC as tbl
		INNER JOIN TL_DM_PhuCap as pc on tbl.Ma_PhuCap = pc.Ma_PhuCap
		LEFT JOIN #tmpUpdate as dt on tbl.Ma_CanBo = dt.MaCB AND tbl.Ma_PhuCap = dt.MaPC
		WHERE dt.MaCB IS NULL OR dt.MaPC IS NULL

		DROP TABLE #tmpUpdate
	END
	ELSE
	BEGIN
		DELETE tbl
		FROM TL_CanBo_PhuCap as tbl
		INNER JOIN #tmpCanBoPC as pc ON tbl.MA_PHUCAP = pc.Ma_PhuCap AND tbl.MA_CBO = pc.Ma_CanBo
	END

	DROP TABLE #tmpPC
	DROP TABLE #tmpCanBoPC
END
GO
