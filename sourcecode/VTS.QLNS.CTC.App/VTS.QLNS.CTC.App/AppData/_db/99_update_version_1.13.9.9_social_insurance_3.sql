/****** Object:  StoredProcedure [dbo].[sp_bh_report_ke_hoach_cap_bo_sung_kcb_bhyt]    Script Date: 2/16/2024 3:34:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_report_ke_hoach_cap_bo_sung_kcb_bhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_report_ke_hoach_cap_bo_sung_kcb_bhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_export_tonghopcaptamung_kcbbhyt]    Script Date: 2/16/2024 3:34:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_export_tonghopcaptamung_kcbbhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_export_tonghopcaptamung_kcbbhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_export_kehoachcaptamung_kcbbhyt]    Script Date: 2/16/2024 3:34:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_export_kehoachcaptamung_kcbbhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_export_kehoachcaptamung_kcbbhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_export_kehoachcaptamung_kcbbhyt]    Script Date: 2/16/2024 3:34:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_export_kehoachcaptamung_kcbbhyt]
@IdCsYTe NVARCHAR(MAX),
@ILoai int,
@IQuy int,
@NamLamViec int,
@UserName NVARCHAR(100),
@Donvitinh int,
@Lns nvarchar(1000)
As
begin
	select 
			row_number() over (order by cptu_ct.iID_MaCoSoYTe) as sTT,
			ROUND(sum(ISNULL(cptu_ct.fQTQuyTruoc,0)),0)/@Donvitinh as fQTQuyTruoc, 
			ROUND(sum(ISNULL(cptu_ct.fTamUngQuyNay,0)),0)/@Donvitinh as fTamUngQuyNay, 
			ROUND(sum(ISNULL(cp_bs.fSoCapBoSung,0) + ISNULL(cp_bs.fDaCapUng,0)),0)/@Donvitinh  as  fLuyKeCapDenCuoiQuy,
			cptu_ct.iID_MaCoSoYTe as iID_MaCoSoYTe,
			csyt.sTenCoSoYTe as sTenCoSoYTe
		from BH_CP_CapTamUng_KCB_BHYT_ChiTiet as cptu_ct
		inner join BH_CP_CapTamUng_KCB_BHYT as cptu on cptu_ct.iID_BH_CP_CapTamUng_KCB_BHYT = cptu.iID_BH_CP_CapTamUng_KCB_BHYT
		left join (
					select sum(isnull(ct.fSoCapBoSung,0)) as fSoCapBoSung, sum(isnull(ct.fDaCapUng,0)) as fDaCapUng,  iID_MLNS, iID_MaCoSoYTe
					from BH_CP_CapBoSung_KCB_BHYT_ChiTiet as ct
					inner join   BH_CP_CapBoSung_KCB_BHYT bs on ct.iID_CTCapPhatBS = bs.iID_CTCapPhatBS
					where bs.iQuy = @iQuy - 1
					group by iID_MLNS, iID_MaCoSoYTe) as cp_bs on cp_bs.iID_MaCoSoYTe = cptu_ct.iID_MaCoSoYTe and cptu_ct.iID_MLNS = cp_bs.iID_MLNS
		inner join DM_CoSoYTe as csyt on csyt.iID_MaCoSoYTe = cptu_ct.iID_MaCoSoYTe
		where cptu_ct.iID_MaCoSoYTe In (SELECT * FROM f_split(@IdCsYTe))
			--and cptu.bIsTongHop <> 1 and cptu.sDSSoChungTuTongHop is null
			and cptu.iQuy = @IQuy
			and cptu_ct.sLNS In (SELECT * FROM f_split(@Lns))
			AND csyt.iNamLamViec = @NamLamViec
			and cptu.iNamLamViec = @NamLamViec
			--and ((@ILoai = 1 and cptu_ct.sLNS = '9050001') or (@ILoai = 2 and cptu_ct.sLNS = '9050002'))
		group by cptu_ct.iID_MaCoSoYTe, csyt.sTenCoSoYTe
		
end
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_export_tonghopcaptamung_kcbbhyt]    Script Date: 2/16/2024 3:34:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_export_tonghopcaptamung_kcbbhyt]
@IdCsYTe NVARCHAR(MAX),
@ILoai int,
@IQuy int,
@NamLamViec int,
@UserName NVARCHAR(100),
@LNS NVARCHAR(500),
@Donvitinh int
As
begin
	select 
			row_number() over (order by cptu_ct.iID_MaCoSoYTe) as sTT,
			ROUND(sum(cptu_ct.fQTQuyTruoc),0)/@Donvitinh as fQTQuyTruoc, 
			ROUND(sum(cptu_ct.fTamUngQuyNay),0)/@Donvitinh as fTamUngQuyNay, 
			ROUND(sum(cptu_ct.fLuyKeCapDenCuoiQuy),0)/@Donvitinh as fLuyKeCapDenCuoiQuy, 
			cptu_ct.iID_MaCoSoYTe as iID_MaCoSoYTe,
			csyt.sTenCoSoYTe as sTenCoSoYTe
		from BH_CP_CapTamUng_KCB_BHYT_ChiTiet as cptu_ct
		inner join BH_CP_CapTamUng_KCB_BHYT as cptu on cptu_ct.iID_BH_CP_CapTamUng_KCB_BHYT = cptu.iID_BH_CP_CapTamUng_KCB_BHYT
		inner join DM_CoSoYTe as csyt on csyt.iID_MaCoSoYTe = cptu_ct.iID_MaCoSoYTe
		where cptu_ct.iID_MaCoSoYTe In (SELECT * FROM f_split(@IdCsYTe))
			--and  cptu.sDSSoChungTuTongHop is not null
			and cptu.iQuy = @IQuy
			--and ((@ILoai = 3 and cptu_ct.sLNS like '9050001') or (@ILoai = 4 and cptu_ct.sLNS like '9050002'))
			AND cptu_ct.sLNS IN (SELECT * FROM splitstring(@LNS))
			AND csyt.iNamLamViec = @NamLamViec
			and cptu.iNamLamViec = @NamLamViec
		group by cptu_ct.iID_MaCoSoYTe, csyt.sTenCoSoYTe
		
end
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_report_ke_hoach_cap_bo_sung_kcb_bhyt]    Script Date: 2/16/2024 3:34:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_report_ke_hoach_cap_bo_sung_kcb_bhyt]
	@IdCsYTe NVARCHAR(MAX),
	@IQuy int,
	@NamLamViec int,
	@UserName NVARCHAR(100),
	@Donvitinh int
AS
BEGIN
		select 
			row_number() over (order by ctct.iID_MaCoSoYTe) as sTT,
			sum(ctct.fDaQuyetToan)/@Donvitinh as fDaQuyetToan,
			sum(ctct.fDaCapUng)/@Donvitinh as fDaCapUng, 
			(sum(ctct.fDaCapUng)/@Donvitinh - sum(ctct.fDaQuyetToan)/@Donvitinh) as fThuaThieu, 
			sum(ctct.fSoCapBoSung)/@Donvitinh as fSoCapBoSung, 
			ctct.iID_MaCoSoYTe as iID_MaCoSoYTe,
			csyt.sTenCoSoYTe as sTenCoSoYTe
		from BH_CP_CapBoSung_KCB_BHYT_ChiTiet as ctct
		inner join BH_CP_CapBoSung_KCB_BHYT as ct on ctct.iID_CTCapPhatBS = ct.iID_CTCapPhatBS
		inner join DM_CoSoYTe as csyt on csyt.iID_MaCoSoYTe = ctct.iID_MaCoSoYTe
		where ctct.iID_MaCoSoYTe In (SELECT * FROM f_split(@IdCsYTe))
			and ct.iNamLamViec = @NamLamViec
			and ct.iLoaiTongHop <> 2 and ct.sDSSoChungTuTongHop is null
			and ct.iQuy = @IQuy
			AND csyt.iNamLamViec = @NamLamViec
		group by ctct.iID_MaCoSoYTe, csyt.sTenCoSoYTe
END
;
GO
