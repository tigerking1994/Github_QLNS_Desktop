/****** Object:  StoredProcedure [dbo].[sp_tl_find_luongthang_canbo_nq104]    Script Date: 5/4/2024 4:52:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_find_luongthang_canbo_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_find_luongthang_canbo_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_find_canbo_phucap_nq104]    Script Date: 5/4/2024 4:52:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_find_canbo_phucap_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_find_canbo_phucap_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_find_canbo_phucap_nq104]    Script Date: 5/4/2024 4:52:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_tl_find_canbo_phucap_nq104]
 @lstMaCBo nvarchar(max)
 AS BEGIN

		select
		cbPhuCap.MA_CBO as MaCbo,
		cbPhuCapBridge.ma_phu_cap as MaPhuCap,
		cbPhuCapBridge.gia_tri as GiaTri,
		cbPhuCapBridge.ngay_huong_phu_cap as HuongPcSn
		from TL_CanBo_PhuCap_NQ104 cbPhuCap
		left join TL_CanBo_PhuCap_Bridge_NQ104 cbPhuCapBridge on cbPhuCap.MA_CBO=cbPhuCapBridge.ma_can_bo 
		where cbPhuCap.MA_CBO in (select * from splitstring(@lstMaCBo))
		order by cbPhuCap.MA_CBO

END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_find_luongthang_canbo_nq104]    Script Date: 5/4/2024 4:52:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_find_luongthang_canbo_nq104]
 @thang int,
 @nam int,
 @maCachTl nvarchar(50),
 @maDonVi nvarchar(50),
 @parent nvarchar(max)
 AS BEGIN

		select luongthang.THANG,
		luongthang.NAM,
		luongthang.Ma_CB,
		luongthang.Ma_CBo as MaCbo,
		luongthang.Ten_Cbo as TenCbo,
		luongthang.Ma_DonVi as MaDonVi,
		luongthang.Ma_CachTL as MaCachTl,
		luongthangBridge.ma_phu_cap as MaPhuCap,
		luongthangBridge.ma_hieu_can_bo as MaHieuCanBo,
		luongthangBridge.parent as Parent,
		luongthangBridge.gia_tri as GiaTri
		from TL_BangLuong_Thang_NQ104 luongthang
		left join TL_BangLuong_Thang_Bridge_NQ104 luongthangBridge on luongthang.Ma_CBo=luongthangBridge.ma_can_bo and luongthang.Ma_DonVi=luongthangBridge.ma_don_vi
		where luongthang.THANG=@thang and luongthang.NAM=@nam 
		and luongthang.Ma_CachTL=@maCachTl and luongthang.Ma_DonVi=@maDonVi
		and luongthang.parent=@parent
		order by luongthang.Ten_Cbo

END
;
;
;
;
;
GO
