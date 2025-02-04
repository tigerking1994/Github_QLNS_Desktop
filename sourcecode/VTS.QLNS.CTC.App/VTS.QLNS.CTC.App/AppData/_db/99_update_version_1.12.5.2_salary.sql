/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tien_an]    Script Date: 1/18/2023 2:32:45 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_tien_an]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_tien_an]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tien_an]    Script Date: 1/18/2023 2:32:45 PM ******/
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
  and canbo.IsDelete = 1
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
;
GO
