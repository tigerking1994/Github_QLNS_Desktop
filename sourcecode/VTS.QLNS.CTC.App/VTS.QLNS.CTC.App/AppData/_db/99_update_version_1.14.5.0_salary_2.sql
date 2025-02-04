/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tien_an_nq104]    Script Date: 5/24/2024 5:24:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_tien_an_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_tien_an_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tien_an_nq104]    Script Date: 5/24/2024 5:24:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_tien_an_nq104] @thang int, @nam int, @maDonVi nvarchar(MAX), @daysInMonth int AS
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
			--When PhuCapTienAn.MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN CAST(@daysInMonth as decimal)
			WHEN (PhuCapTienAn.HuongPC_SN IS NULL or PhuCapTienAn.HuongPC_SN = 0) AND phucap.Parent in ('TIENAN2', 'TIENAN')  THEN CAST(@daysInMonth as decimal)
			WHEN phucap.Parent in ('TIENAN2', 'TIENAN')  THEN CAST(PhuCapTienAn.HuongPC_SN as decimal)
		End SoNgay,
	   'ngày' as Dv_tinh,
	   '=' Bang,
	   (PhuCapTienAn.GIA_TRI * COUNT(canBo.Ma_CanBo) * CASE 
			--When PhuCapTienAn.MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN CAST(@daysInMonth as decimal)
			WHEN (PhuCapTienAn.HuongPC_SN IS NULL or PhuCapTienAn.HuongPC_SN = 0) AND phucap.Parent in ('TIENAN2', 'TIENAN')  THEN CAST(@daysInMonth as decimal)
			WHEN phucap.Parent in ('TIENAN2', 'TIENAN')  THEN CAST(PhuCapTienAn.HuongPC_SN as decimal)
		End) ThanhTien
FROM TL_DM_CanBo_NQ104 canBo
JOIN
  (SELECT bridgemapc.ma_can_bo as MA_CBO,
          bridgemapc.ma_phu_cap as MA_PHUCAP,
          bridgemapc.gia_tri GIA_TRI,
          bridgemapc.ngay_huong_phu_cap HuongPC_SN
		  --bangluongbridge.parent
   from TL_CanBo_PhuCap_NQ104 cbopc
	 left join TL_CanBo_PhuCap_Bridge_NQ104 bridgemapc on cbopc.MA_CBO=bridgemapc.ma_can_bo
	 left join TL_BangLuong_Thang_Bridge_NQ104 bangluongbridge on bangluongbridge.ma_phu_cap=bridgemapc.ma_phu_cap and bangluongbridge.ma_can_bo=bridgemapc.ma_can_bo
	 left join TL_DM_PhuCap_NQ104 mapc on bridgemapc.ma_phu_cap=mapc.Ma_PhuCap
	 left join TL_DM_CanBo_NQ104 dmcbo on bangluongbridge.ma_don_vi=dmcbo.Parent and bangluongbridge.ma_can_bo=dmcbo.Ma_CanBo
	 where mapc.Parent IN ('TIENAN', 'TIENAN2')
	 and mapc.nam=@nam
	 and dmcbo.Thang=@thang
	 and dmcbo.Nam=@nam
	 and bridgemapc.gia_tri > 0) PhuCapTienAn ON canBo.Ma_CanBo = PhuCapTienAn.MA_CBO
JOIN TL_DM_PhuCap_NQ104 phucap ON PhuCapTienAn.MA_PHUCAP = phucap.Ma_PhuCap 
JOIN TL_DM_DonVi_NQ104 donvi ON canBo.Parent = donvi.Ma_DonVi
WHERE canBo.Thang = @thang
  AND canBo.Nam = @nam
  And canbo.Parent IN (SELECT * FROM f_split(@maDonVi))
  and (canbo.IsDelete = 1 or (canbo.Ma_TangGiam = '320' and month(canbo.Ngay_XN) = @thang and year(canbo.Ngay_XN) = @nam ))
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
;
GO
