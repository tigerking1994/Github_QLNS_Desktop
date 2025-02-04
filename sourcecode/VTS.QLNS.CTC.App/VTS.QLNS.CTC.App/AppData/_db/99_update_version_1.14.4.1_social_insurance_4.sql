/****** Object:  StoredProcedure [dbo].[sp_bh_export_kehoachcaptamung_kcbbhyt]    Script Date: 5/4/2024 4:41:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_export_kehoachcaptamung_kcbbhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_export_kehoachcaptamung_kcbbhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_export_kehoachcaptamung_kcbbhyt]    Script Date: 5/4/2024 4:41:59 PM ******/
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
@Lns nvarchar(1000),
@IsRoundMillion bit
As
begin
	if (@IsRoundMillion = 1)
	begin
		select 
			row_number() over (order by cptu_ct.iID_MaCoSoYTe) as sTT,
			ROUND(sum(ISNULL(cptu_ct.fQTQuyTruoc,0)),0)/@Donvitinh as fQTQuyTruoc, 
			ROUND(sum(ISNULL(cptu_ct.fTamUngQuyNay,0))/1000000 , 0)*1000000 /@Donvitinh as fTamUngQuyNay, 
			--ROUND(sum(ISNULL(cp_bs.fSoCapBoSung,0) + ISNULL(cp_bs.fDaCapUng,0)),0)/@Donvitinh  as  fLuyKeCapDenCuoiQuy,
			ROUND(sum(ISNULL(cptu_ct.fLuyKeCapDenCuoiQuy,0))/1000000 , 0)*1000000 /@Donvitinh as fLuyKeCapDenCuoiQuy,
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
	else
	begin
		select 
			row_number() over (order by cptu_ct.iID_MaCoSoYTe) as sTT,
			ROUND(sum(ISNULL(cptu_ct.fQTQuyTruoc,0)),0)/@Donvitinh as fQTQuyTruoc, 
			ROUND(sum(ISNULL(cptu_ct.fTamUngQuyNay,0)),0)/@Donvitinh as fTamUngQuyNay, 
			ROUND(sum(ISNULL(cptu_ct.fLuyKeCapDenCuoiQuy,0)),0)/@Donvitinh as fLuyKeCapDenCuoiQuy,
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
			and cptu.iQuy = @IQuy
			and cptu_ct.sLNS In (SELECT * FROM f_split(@Lns))
			AND csyt.iNamLamViec = @NamLamViec
			and cptu.iNamLamViec = @NamLamViec
		group by cptu_ct.iID_MaCoSoYTe, csyt.sTenCoSoYTe
	end
end
;
;
;
;
GO
