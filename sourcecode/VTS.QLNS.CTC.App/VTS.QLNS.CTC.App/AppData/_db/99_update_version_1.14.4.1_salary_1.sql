/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_kehoach_chitiet_canbo_nq104]    Script Date: 5/3/2024 5:38:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_kehoach_chitiet_canbo_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_kehoach_chitiet_canbo_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chitiet_kehoach_nq104]    Script Date: 5/3/2024 5:38:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_ct_chitiet_kehoach_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_ct_chitiet_kehoach_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_export_quy_luong_can_cu_nq104]    Script Date: 5/3/2024 5:38:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_export_quy_luong_can_cu_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_export_quy_luong_can_cu_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_baocao_luong_nam_ke_hoach_nq104]    Script Date: 5/3/2024 5:38:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_baocao_luong_nam_ke_hoach_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_baocao_luong_nam_ke_hoach_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_baocao_luong_nam_ke_hoach_nq104]    Script Date: 5/3/2024 5:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_baocao_luong_nam_ke_hoach_nq104] @spMaDonVi varchar(100), @spNam int
AS
DECLARE @cols AS NVARCHAR(MAX);
DECLARE  @query  AS NVARCHAR(MAX);
DECLARE @maDonVi AS varchar(100);
DECLARE @nam AS NVARCHAR(MAX);

SET @maDonVi = @spMaDonVi
SET @nam = @spNam

SET @cols = STUFF(
  (
    SELECT
      distinct ',' + QUOTENAME(phucap.Ma_PhuCap) 
    FROM TL_DM_PhuCap_NQ104 phucap
        FOR XML PATH(''), TYPE
  ).value('.', 'NVARCHAR(MAX)'),1,1,'')
  
SET @query = 'SELECT Thang, Nam, Ten_DonVi, ' + @cols + ' from 
            (
                select TL_BangLuong_KeHoach.Thang, TL_BangLuong_KeHoach.Nam, TL_DM_DonVi_NQ104.Ten_Donvi, MA_PHUCAP, SUM(Gia_Tri) as Gia_Tri
				from [dbo].[TL_BangLuong_KeHoach_NQ104] TL_BangLuong_KeHoach
				join [dbo].[TL_DM_CanBo_NQ104_KeHoach]
				ON TL_BangLuong_KeHoach.Ma_CanBo = TL_DM_CanBo_NQ104_KeHoach.Ma_CanBo
				join [dbo].[TL_DM_DonVi_NQ104]
				ON TL_BangLuong_KeHoach.Ma_DonVi = [dbo].[TL_DM_DonVi_NQ104].Ma_DonVi
				where Ma_CachTL = ''CACH0''
				And TL_BangLuong_KeHoach.Ma_DonVi = ' + @maDonVi + '
				And TL_BangLuong_KeHoach.Nam = ' + @nam + ' ' + '
				Group By Ma_PhuCap, TL_BangLuong_KeHoach.Thang, TL_BangLuong_KeHoach.Nam, TL_DM_DonVi_NQ104.Ten_Donvi, MA_PHUCAP
           ) x
            pivot 
            (
                 SUM(Gia_Tri)
                for MA_PHUCAP in (' + @cols + ')
            ) p '
execute(@query)
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_export_quy_luong_can_cu_nq104]    Script Date: 5/3/2024 5:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_tl_export_quy_luong_can_cu_nq104]
	@NamLamViec int,
	@LuongChinh nvarchar(50),
	@PhuCapCV nvarchar(50),
	@PhuCapTNN nvarchar(50),
	@PhuCapTNVK nvarchar(50),
	@PhuCapHSBL nvarchar(50),
	@lstIdChungTu nvarchar(max)
AS 
BEGIN
	CREATE TABLE #result(Id uniqueidentifier, IIdParent uniqueidentifier, SNoiDung nvarchar(200),SXauNoiMa nvarchar(200), ILeVel int, IThuTu int , QSBQ float,LHT_TT float, PCCV_TT Float, PCTN_TT Float, PCTNVK_TT Float, HSBL_TT Float)
	DECLARE @iIDParent1 uniqueidentifier = NewID();
	DECLARE @iIDParent21 uniqueidentifier = NewID();
	DECLARE @iIDParent22 uniqueidentifier = NewID();
	--INSERT TIER 1
	INSERT INTO #result(Id, IIdParent, SNoiDung,SXauNoiMa, ILeVel, IThuTu, QSBQ, LHT_TT, PCCV_TT, PCTN_TT, PCTNVK_TT,HSBL_TT)
	(SELECT @iIDParent1, NULL, N'I. Khối dự toán',NULL,0, 0, 0, 0, 0, 0, 0,0
	--INSERT TIER 2
	UNION ALL 
	SELECT @iIDParent21, @iIDParent1, N'A. Quân nhân',NULL, 1, 1, 0, 0, 0, 0, 0,0
	--INSERT TIER 3 ' lương sĩ quan'
	UNION ALL 
	SELECT NEWID(), @iIDParent21, N'1. Sĩ quan', '9020001-010-011-0001-0000',2, 1,
	COUNT(Gia_Tri)/(12*5),--/(12*5) Vì chia cho 12 lấy bình quân và chia cho 5 vì lấy value 5 Ma_PhuCap--
	SUM( CASE WHEN Ma_PhuCap = @LuongChinh THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapCV THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapTNN THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapTNVK THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapHSBL THEN ISNULL(Gia_Tri, 0) ELSE 0 END)
	FROM TL_BangLuong_KeHoach_NQ104 kh
	WHERE
		Nam = @NamLamViec
		AND ((Ma_CB LIKE '1%' AND Ma_PhuCap in (@LuongChinh, @PhuCapCV, @PhuCapTNN, @PhuCapTNVK, @PhuCapHSBL)))
		AND kh.parent IN (SELECT * FROM splitstring(@lstIdChungTu)) 
	
	--INSERT TIER 3 ' lương QNCN'
	UNION ALL 
	SELECT NEWID(), @iIDParent21, N'2. QNCN', '9020001-010-011-0001-0001', 2, 2,
	COUNT(Gia_Tri)/(12*5),
	SUM( CASE WHEN Ma_PhuCap = @LuongChinh THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapCV THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapTNN THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapTNVK THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapHSBL THEN ISNULL(Gia_Tri, 0) ELSE 0 END)
	FROM TL_BangLuong_KeHoach_NQ104 kh
	WHERE
		Nam = @NamLamViec
		AND ((Ma_CB LIKE '2%' AND Ma_PhuCap in (@LuongChinh, @PhuCapCV, @PhuCapTNN, @PhuCapTNVK, @PhuCapHSBL)))
		AND kh.parent IN (SELECT * FROM splitstring(@lstIdChungTu)) 

	--INSERT TIER 3 ' lương HSQ-BS'
	UNION ALL 
	SELECT NEWID(), @iIDParent21, N'3. HSQ, BS', '9020001-010-011-0001-0002', 2, 2,
	COUNT(Gia_Tri)/(12*5),

	SUM( CASE WHEN Ma_PhuCap = @LuongChinh THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapCV THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapTNN THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapTNVK THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapHSBL THEN ISNULL(Gia_Tri, 0) ELSE 0 END)
	FROM TL_BangLuong_KeHoach_NQ104 kh
	WHERE
		Nam = @NamLamViec
		AND ((Ma_CB LIKE '0%' AND Ma_PhuCap in (@LuongChinh, @PhuCapCV, @PhuCapTNN, @PhuCapTNVK, @PhuCapHSBL)))
		AND kh.parent IN (SELECT * FROM splitstring(@lstIdChungTu)) 


	--INSERT TIER 2.2
	UNION ALL 
	SELECT @iIDParent22, @iIDParent1, N'B. Người lao động',NULL, 1, 2, 0, 0, 0, 0, 0,0
	--INSERT TIER 3 ' CC,CN,VCQP'
	UNION ALL 
	SELECT NEWID(), @iIDParent22, N'1. CC, CN, VCQP','9020001-010-011-0002-0000', 2, 1,
	COUNT(Gia_Tri)/(12*5),
	SUM( CASE WHEN Ma_PhuCap = @LuongChinh THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapCV THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapTNN THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapTNVK THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapHSBL THEN ISNULL(Gia_Tri, 0) ELSE 0 END)
	FROM TL_BangLuong_KeHoach_NQ104 kh
	WHERE
		Nam = @NamLamViec
		AND (((Ma_CB IN ('3.1','3.2','3.3') OR Ma_CB LIKE ('41%') OR Ma_CB LIKE ('42%')) AND Ma_PhuCap in (@LuongChinh, @PhuCapCV, @PhuCapTNN, @PhuCapTNVK, @PhuCapHSBL)))
		AND kh.parent IN (SELECT * FROM splitstring(@lstIdChungTu)) 
	
	--INSERT TIER 3 ' lương LĐHĐ'
	UNION ALL 
	SELECT NEWID(), @iIDParent21, N'2. LĐHĐ', '9020001-010-011-0002-0001', 2, 2,
	COUNT(Gia_Tri)/(12*5),

	SUM( CASE WHEN Ma_PhuCap = @LuongChinh THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapCV THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapTNN THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapTNVK THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapHSBL THEN ISNULL(Gia_Tri, 0) ELSE 0 END)
	FROM TL_BangLuong_KeHoach_NQ104 kh
	WHERE
		Nam = @NamLamViec
		AND ((Ma_CB LIKE ('43%') AND Ma_PhuCap in (@LuongChinh, @PhuCapCV, @PhuCapTNN, @PhuCapTNVK, @PhuCapHSBL)))
		AND kh.parent IN (SELECT * FROM splitstring(@lstIdChungTu)) 

	);

	SELECT * FROM #result;
	DROP TABLE #result;
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chitiet_kehoach_nq104]    Script Date: 5/3/2024 5:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_get_data_ct_chitiet_kehoach_nq104]
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
LEFT JOIN (SELECT * FROM TL_QT_ChungTuChiTiet_KeHoach_NQ104 WHERE Id_DonVi = @idDonVi and NamLamViec = @nam) chungTuChiTiet ON mlns.sXauNoiMa = chungTuChiTiet.XauNoiMa 
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
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_kehoach_chitiet_canbo_nq104]    Script Date: 5/3/2024 5:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_rpt_kehoach_chitiet_canbo_nq104]
	@NamKeHoach int,
	@MaDonVi varchar(500)
AS
BEGIN
SELECT blcl.Ma_Hieu_CanBo MaHieuCanBo,
          blcl.Ma_CB MaCapBac,
          blcl.Ten_CanBo TenCanBo,
          blcl.thangBl Thang,
          blcl.Ma_PhuCap MaPhuCap,
          (sum(ISNULL(blcl.luongKh, 0)) - sum(ISNULL(blcl.luongt, 0))) AS ChenhLech
   FROM
     (SELECT cb.Ma_Hieu_CanBo,
             cb.Ten_CanBo,
             cb.Thang AS thangCb,
             blkh.Thang AS thangBl,
             blkh.Nam,
             blkh.Ma_CB,
             blkh.Ma_PhuCap,
             blkh.Gia_Tri AS luongKh,
             0 AS luongt
      FROM TL_DM_CanBo_KeHoach_NQ104 cb
      LEFT JOIN TL_BangLuong_KeHoach_NQ104 blkh ON cb.Ma_Hieu_CanBo = blkh.Ma_Hieu_CanBo
      AND cb.Thang = blkh.Thang
      AND cb.Nam = @NamKeHoach
      WHERE blkh.Nam = @NamKeHoach
        AND blkh.Ma_DonVi = @MaDonVi
		AND exists (select 1 from TL_PhuCap_MLNS_NQ104 where Ma_PhuCap = blkh.Ma_PhuCap and Nam = @NamKeHoach -1)
      UNION ALL SELECT cb.Ma_Hieu_CanBo,
                       cb.Ten_CanBo,
                       cb.Thang AS thangCb,
                       blt.Thang AS thangBl,
                       blt.Nam,
                       blt.Ma_CB,
                       blt.Ma_PhuCap,
                       0 AS luongKh,
                       blt.Gia_Tri AS luongt
      FROM TL_DM_CanBo_KeHoach_NQ104 cb
      LEFT JOIN TL_BangLuong_Thang_NQ104 blt ON cb.Ma_Hieu_CanBo = blt.Ma_Hieu_CanBo
      AND cb.Thang = blt.Thang
      AND cb.Nam = @NamKeHoach
	  AND exists (select 1 from TL_PhuCap_MLNS_NQ104 where Ma_PhuCap = blt.Ma_PhuCap and Nam = @NamKeHoach - 1)
      WHERE blt.Nam = (@NamKeHoach - 2)
        AND blt.Ma_DonVi = @MaDonVi) AS blcl
   GROUP BY blcl.Ma_Hieu_CanBo,
            blcl.Ma_CB,
            blcl.Ten_CanBo,
            blcl.thangBl,
            blcl.Ma_PhuCap
END
/****** Object:  StoredProcedure [dbo].[sp_khv_khth_dexuat_dieuchinh_report]    Script Date: 15/12/2021 6:34:55 PM ******/
SET ANSI_NULLS ON
;
;
;
GO
