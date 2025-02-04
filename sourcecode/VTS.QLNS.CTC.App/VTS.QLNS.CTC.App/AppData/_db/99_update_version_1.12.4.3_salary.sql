/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tien_an]    Script Date: 16/12/2022 6:30:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_tien_an]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_tien_an]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_donvi_baocao_quanso]    Script Date: 16/12/2022 6:30:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_donvi_baocao_quanso]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_donvi_baocao_quanso]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 20/12/2022 9:54:24 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bangluong_thang_dulieu_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bangluong_thang_dulieu_insert]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chi_tiet_export]    Script Date: 20/12/2022 2:16:45 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_ct_chi_tiet_export]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_ct_chi_tiet_export]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chi_tiet_export]    Script Date: 20/12/2022 2:16:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_tl_get_data_ct_chi_tiet_export]
	@lstId nvarchar(max),
	@lstCach nvarchar(100),
	@nam int
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
	INNER JOIN TL_QT_ChungTu as tbl on tmp.Item = tbl.ID;

	select DISTINCT XauNoiMa  into #tmpPcMlns FROM TL_PhuCap_MLNS WHERE Nam = @nam;

	with ctct as (
	  select XauNoiMa,MaCachTl,  Sum(TongCong) AS TongCong,
		   Sum(DieuChinh) AS DieuChinh,
		 Sum(ISNULL(dt.DDuToan, 0)) As DDuToan
	  from TL_QT_ChungTu as tbl
	  INNER JOIN TL_QT_ChungTuChiTiet as dt on tbl.Id = dt.Id_ChungTu
	  where  (ISNULL(@lstId, '') <> '' AND tbl.ID in (SELECT *  FROM f_split(@lstId)))
		AND ((dt.MaCachTl = '' AND @isHaveCachTinhLuong = 0) OR (@isHaveCachTinhLuong = 1 AND dt.MaCachTl in (SELECT id  FROM #tmp)))
	  group by dt.XauNoiMa, dt.MaCachTl
	),
	lstSoNguoi as (
		SELECT XauNoiMa,
			SoNguoi,SoNgay
		from TL_QT_ChungTu as tbl
		INNER JOIN TL_QT_ChungTuChiTiet as dt on tbl.Id = dt.Id_ChungTu
		where tbl.ID in (SELECT TOP(1) ID FROM #tblMaxThang ORDER BY Thang DESC)
		AND ((@isHaveCachTinhLuong = 0 AND dt.MaCachTl = '') OR (@isHaveCachTinhLuong = 1 AND dt.MaCachTl in (SELECT id  FROM #tmp)))
	),
	phucapMlns as (
		SELECT TL_PhuCap_MLNS.XauNoiMa, min(TL_PhuCap_MLNS.Ma_Cb) as Ma_Cb FROM #tmpPcMlns 
		left join TL_PhuCap_MLNS on #tmpPcMlns.XauNoiMa = TL_PhuCap_MLNS.XauNoiMa
		WHERE Nam = @nam
		group by TL_PhuCap_MLNS.XauNoiMa
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
	   pcmlns.Ma_Cb as MaCb,
       sMoTa AS Mota,
     MaCachTl as MaCachTl,
       iNamLamViec AS NamLamViec,
       mlns.bHangCha AS BHangCha,
       sChiTietToi AS ChiTietToi,
       TongCong,
    SoNguoi,
	SoNgay,
       DieuChinh,
     DDuToan
FROM NS_MucLucNganSach mlns  
LEFT JOIN ctct ctct ON mlns.sXauNoiMa = ctct.XauNoiMa
LEFT JOIN lstSoNguoi as sn on mlns.sXauNoiMa = sn.XauNoiMa 
left JOIN phucapMlns as pcmlns on mlns.sXauNoiMa = pcmlns.XauNoiMa

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

GO

/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 20/12/2022 9:54:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

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
			MA_CBO MaCanBo,canBoPhuCap.MA_PHUCAP,
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
			pc.PARENT IN ('TIENAN', 'TIENAN2') and canBoPhuCap.Gia_Tri <> 0
		GROUP BY canBoPhuCap.MA_CBO, canBoPhuCap.MA_PHUCAP
	),

	ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo AS MaCanBo,
			canBo.Ten_CanBo AS TenCanBo,
			donVi.Ma_DonVi MaDonVi,
			canBo.Ma_Hieu_CanBo AS MaHieuCanBo,
			capBac.Ma_Cb MaCapBac,
			canBo.BHTN,
			canBo.PCCV,
			canBo.BNuocNgoai
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent = donVi.Ma_DonVi
		INNER  JOIN TL_DM_CapBac capBac
			ON canBo.Ma_CB = capBac.Ma_Cb
		WHERE
			canBo.Thang = @Thang
			AND canBo.Nam = @Nam
			AND canBo.Parent IN (SELECT * FROM f_split(@MaDonVi))
			AND canBo.IsDelete = 1
			AND canBo.Khong_Luong <> 1
	)

	SELECT
		newid()					AS Id,
		@Thang					AS Thang,
		@Nam					AS Nam,
		canBo.MaCanBo			AS MaCbo,
		canBo.TenCanBo			AS TenCbo,
		canBo.MaDonVi			AS MaDonVi,
		canBo.BNuocNgoai				,		
		@MaCachTl				AS MaCachTl,
		canBoPhuCap.MA_PHUCAP	AS MaPhuCap,
		CASE
			WHEN (canBoPhuCap.MA_PHUCAP = 'TCVIECLAM_TT' AND cb.Parent in ('1', '2', '3')) THEN 0 
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
	LEFT JOIN TL_DM_Cach_TinhLuong_Chuan cachTinhLuong
		ON cachTinhLuong.Ma_Cot = canBoPhuCap.MA_PHUCAP
	left join TL_DM_CapBac cb on canBo.MaCapBac = cb.Ma_Cb
END
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_tl_get_donvi_baocao_quanso]    Script Date: 16/12/2022 6:30:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_tl_get_donvi_baocao_quanso] 
	-- Add the parameters for the stored procedure here
	@nam int
AS
BEGIN
	select dv.Id , iTrangThai, dv.Ma_DonVi, dv.Parent_id, dv.Ten_DonVi, dv.XauNoiMa 
	from TL_DM_DonVi dv
	join TL_QS_ChungTu qs on dv.Ma_DonVi = qs.Ma_DonVi 
	where qs.sTongHop is null and iTrangThai = 1

	union all

	select iID_DonVi as Id, cast(iTrangThai as bit), iID_MaDonVi as Ma_DonVi, NULL as Parent_id, sTenDonVi as Ten_DonVi, NULL as XauNoiMa 
	from DonVi 
	where iNamLamViec = @nam and iTrangThai = 1
END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tien_an]    Script Date: 16/12/2022 6:30:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_tien_an] @thang int, @nam int, @maDonVi nvarchar(MAX), @daysInMonth int AS
BEGIN
SELECT 
       canBo.Parent MaDonVi,
	   donvi.Ten_DonVi TenDonVi,
       PhuCapTienAn.MA_PHUCAP MaPhuCap,
       phucap.Ten_PhuCap TienAn,
       PhuCapTienAn.GIA_TRI DinhMuc,
	   'x' as Nhan,
	   CAST(COUNT(canBo.Ma_CanBo) as decimal) SoNguoi,
	   'x' as Nhan, 
		CASE 
			When PhuCapTienAn.MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN CAST(@daysInMonth as decimal)
			--WHEN PhuCapTienAn.MA_PHUCAP IN ('TA_DOCHAI_DG', 'TA_OM_DG', 'TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4') THEN CAST(SUM(PhuCapTienAn.HuongPC_SN) as decimal)
			WHEN PhuCapTienAn.HuongPC_SN IS NULL THEN 0
			WHEN phucap.Parent = 'TIENAN2'  THEN CAST(PhuCapTienAn.HuongPC_SN as decimal)
		End SoNgay,
	   'ngày' as Dv_tinh,
	   '=' Bang,
	   (PhuCapTienAn.GIA_TRI * COUNT(canBo.Ma_CanBo) * CASE 
			When PhuCapTienAn.MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN CAST(@daysInMonth as decimal)
			--WHEN PhuCapTienAn.MA_PHUCAP IN ('TA_DOCHAI_DG', 'TA_OM_DG', 'TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4') THEN CAST(SUM(PhuCapTienAn.HuongPC_SN) as decimal)
			WHEN PhuCapTienAn.HuongPC_SN IS NULL THEN 0
			WHEN phucap.Parent = 'TIENAN2'  THEN CAST(PhuCapTienAn.HuongPC_SN as decimal)
		End) ThanhTien
FROM Tl_Dm_CanBo canBo
JOIN
  (SELECT MA_CBO,
          cbopc.MA_PHUCAP,
          cbopc.GIA_TRI,
          cbopc.HuongPC_SN
   FROM TL_CanBo_PhuCap cbopc
   LEFT JOIN Tl_DM_PhuCap mapc ON cbopc.MA_PHUCAP = mapc.Ma_PhuCap
   WHERE mapc.Parent IN ('TIENAN', 'TIENAN2')
     AND cbopc.GIA_TRI > 0) PhuCapTienAn ON canBo.Ma_CanBo = PhuCapTienAn.MA_CBO
JOIN Tl_dm_PhuCap phucap ON PhuCapTienAn.MA_PHUCAP = phucap.Ma_PhuCap
JOIN TL_DM_DonVi donvi ON canBo.Parent = donvi.Ma_DonVi
WHERE canBo.Thang = @thang
  AND canBo.Nam = @nam
  And canbo.Parent IN (SELECT * FROM f_split(@maDonVi))
  Group By canBo.Parent,
	   donvi.Ten_DonVi,
       PhuCapTienAn.MA_PHUCAP,
	   PhuCapTienAn.HuongPC_SN,
       phucap.Ten_PhuCap,
	   phucap.Parent,
       PhuCapTienAn.GIA_TRI
End
;
;
;
GO

select cb.Ma_CanBo, pc.* INTO #tmpCbpc from tl_dm_canbo cb, tl_dm_phucap pc

insert into TL_CanBo_PhuCap
SELECT newid() as Id, tmp.bSaoChep, tmp.Chon, tmp.Cong_Thuc, null as DateEnd, null as DateStart, null as flag, 0 as Gia_Tri, tmp.He_So, tmp.HuongPC_SN, null as ISoThang_Huong, tmp.Ma_CanBo, null as Ma_HMCP, tmp.MA_PHUCAP, null AS PHANTRAM_CT
FROM #tmpCbpc tmp left join TL_CanBo_PhuCap cbpc on tmp.Ma_PhuCap = cbpc.MA_PHUCAP and tmp.Ma_CanBo = cbpc.MA_CBO
where cbpc.ma_phucap is null

drop table #tmpCbpc
GO
