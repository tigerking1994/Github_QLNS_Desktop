/****** Object:  StoredProcedure [dbo].[sp_bh_report_tong_hop_cap_bo_sung_kcb_bhyt]    Script Date: 6/3/2024 5:30:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_report_tong_hop_cap_bo_sung_kcb_bhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_report_tong_hop_cap_bo_sung_kcb_bhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_report_ke_hoach_cap_bo_sung_kcb_bhyt]    Script Date: 6/3/2024 5:59:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_report_ke_hoach_cap_bo_sung_kcb_bhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_report_ke_hoach_cap_bo_sung_kcb_bhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_report_tong_hop_cap_bo_sung_kcb_bhyt]    Script Date: 6/3/2024 5:30:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_report_tong_hop_cap_bo_sung_kcb_bhyt] 
	@IdCsYTe NVARCHAR(MAX),
	@IQuy int,
	@NamLamViec int,
	@UserName NVARCHAR(100),
	@Donvitinh int,
	@XauNoiMa NVARCHAR(50)
AS
BEGIN
		select 
			--row_number() over (order by ctct.iID_MaCoSoYTe) as sTT,
			sum(ctct.fDaQuyetToan)/@Donvitinh as fDaQuyetToan, 
			(sum(ctct.fDaCapUng)/@Donvitinh - sum(ctct.fDaQuyetToan)/@Donvitinh) as fThuaThieu, 
			sum(ctct.fSoCapBoSung)/@Donvitinh as fSoCapBoSung 
			--ctct.iID_MaCoSoYTe as iID_MaCoSoYTe,
			--csyt.sTenCoSoYTe as sTenCoSoYTe
		from BH_CP_CapBoSung_KCB_BHYT_ChiTiet as ctct
		inner join BH_CP_CapBoSung_KCB_BHYT as ct on ctct.iID_CTCapPhatBS = ct.iID_CTCapPhatBS
		inner join DM_CoSoYTe as csyt on csyt.iID_MaCoSoYTe = ctct.iID_MaCoSoYTe
		where ctct.iID_MaCoSoYTe In (SELECT * FROM f_split(@IdCsYTe))
			and ct.iNamLamViec = @NamLamViec
			--and ct.iLoaiTongHop = 2 and ct.sDSSoChungTuTongHop is not null
			and ct.iQuy = @IQuy
			and csyt.iNamLamViec = @NamLamViec
			and ctct.sXauNoiMa = @XauNoiMa
		--group by ctct.iID_MaCoSoYTe, csyt.sTenCoSoYTe
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_report_ke_hoach_cap_bo_sung_kcb_bhyt]    Script Date: 6/3/2024 5:59:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_report_ke_hoach_cap_bo_sung_kcb_bhyt]
	@IdCsYTe NVARCHAR(MAX),
	@IQuy int,
	@NamLamViec int,
	@UserName NVARCHAR(100),
	@Donvitinh int,
	@XauNoiMa NVARCHAR(50)
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
			and ctct.sXauNoiMa = @XauNoiMa
		group by ctct.iID_MaCoSoYTe, csyt.sTenCoSoYTe
END
;
;
GO
