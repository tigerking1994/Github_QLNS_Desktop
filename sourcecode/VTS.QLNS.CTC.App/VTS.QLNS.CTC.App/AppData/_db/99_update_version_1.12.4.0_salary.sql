/****** Object:  StoredProcedure [dbo].[sp_tl_update_dmcapbac_canbo]    Script Date: 13/12/2022 9:37:45 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_update_dmcapbac_canbo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_update_dmcapbac_canbo]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop]    Script Date: 13/12/2022 9:37:45 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop]    Script Date: 13/12/2022 9:37:45 AM ******/
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

SELECT TOP(1) @iIdChungTuOld = ol.ID, @iIdChungTuDuToanOld = ol.IIdChungTuDuToan
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

INSERT INTO [dbo].[TL_QT_ChungTuChiTiet] ([Id_ChungTu] , [MLNS_Id] , [MLNS_Id_Parent] , [XauNoiMa] , [LNS] , [L] , [K] , [M] , [TM] , [TTM] , [NG] , [TNG] , [TNG1] , [TNG2] , [TNG3] , [MoTa] , [Chuong] , [NamNganSach] , [NguonNganSach] , [NamLamViec] , [iTrangThai] , [iThangQuy] , [Id_DonVi] , [TenDonVi] , [MucAn] , [GhiChu] , [DateCreated] , [UserCreator] , [DateModified] , [UserModifier] , [TongCong] , [BHangCha] , [ChiTietToi] , [SoNgay] , [SoNguoi] , [DieuChinh] , [MaCachTl], [DDuToan])
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
		 ISNULL(tmp.DDuToan, 0)

	-- add du toan thang truoc da co
	INSERT INTO [dbo].[TL_QT_ChungTuChiTiet] ([Id_ChungTu] , [MLNS_Id] , [MLNS_Id_Parent] , [XauNoiMa] , [LNS] , [L] , [K] , [M] , [TM] , [TTM] , [NG] , [TNG] , [TNG1] , [TNG2] , [TNG3] , [MoTa] , [Chuong] , [NamNganSach] , [NguonNganSach] , [NamLamViec] , [iTrangThai] , [iThangQuy] , [Id_DonVi] , [TenDonVi] , [MucAn] , [GhiChu] , [DateCreated] , [UserCreator] , [DateModified] , [UserModifier] , [TongCong] , [BHangCha] , [ChiTietToi] , [SoNgay] , [SoNguoi] , [DieuChinh] , [MaCachTl], [DDuToan])
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
	FROM #tmp as tmp
	INNER JOIN NS_MucLucNganSach as ml on ml.iNamLamViec = @NamLamViec AND ml.sXauNoiMa = tmp.sXauNoiMa
	LEFT JOIN TL_QT_ChungTuChiTiet as dt on tmp.sXauNoiMa = dt.XauNoiMa AND dt.Id_ChungTu = @idChungTu
	WHERE dt.XauNoiMa IS NULL AND ISNULL(tmp.DDuToan, 0) <> 0

	DROP TABLE #tmp
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_update_dmcapbac_canbo]    Script Date: 13/12/2022 9:37:45 AM ******/
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
INSERT INTO @tmp(sMaPhuCap) VALUES('PCTN_HS'), ('PCTEMTHU_TT'), ('PCNU_HS'), ('PCANQP_HS'), ('THANG_TCXN'), ('BHXHDV_HS'), ('BHXHCN_HS'), ('BHYTDV_HS'), ('BHYTCN_HS'), ('BHTNDV_HS'), ('BHTNCN_HS'), ('LHT_HS'), ('TILE_HUONG'), ('BHXHDVCS_HS'), ('BHYTDVCS_HS')

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
INNER JOIN @tmp as tmp on pc.MA_PHUCAP = tmp.sMaPhuCap

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
INNER JOIN @tmp as tmp on pc.MA_PHUCAP = tmp.sMaPhuCap

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
;
GO
