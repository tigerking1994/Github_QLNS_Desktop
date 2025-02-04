/****** Object:  StoredProcedure [dbo].[sp_bh_export_tonghopcaptamung_kcbbhyt]    Script Date: 1/29/2024 8:46:39 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_export_tonghopcaptamung_kcbbhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_export_tonghopcaptamung_kcbbhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_export_thongtricaptamung_kcbbhyt]    Script Date: 1/29/2024 8:46:39 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_export_thongtricaptamung_kcbbhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_export_thongtricaptamung_kcbbhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_export_kehoachcaptamung_kcbbhyt]    Script Date: 1/29/2024 8:46:39 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_export_kehoachcaptamung_kcbbhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_export_kehoachcaptamung_kcbbhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_export_kehoachcaptamung_kcbbhyt]    Script Date: 1/29/2024 8:46:39 AM ******/
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
			and cptu.bIsTongHop <> 1 and cptu.sDSSoChungTuTongHop is null
			and cptu.iQuy = @IQuy
			and cptu_ct.sLNS In (SELECT * FROM f_split(@Lns))
			--and ((@ILoai = 1 and cptu_ct.sLNS = '9050001') or (@ILoai = 2 and cptu_ct.sLNS = '9050002'))
		group by cptu_ct.iID_MaCoSoYTe, csyt.sTenCoSoYTe
		
end
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_export_thongtricaptamung_kcbbhyt]    Script Date: 1/29/2024 8:46:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_export_thongtricaptamung_kcbbhyt]
@IdCsYTe NVARCHAR(MAX),
@ILoai int,
@LNS NVARCHAR(MAX),
@IQuy int,
@NamLamViec int,
@UserName NVARCHAR(100),
@Donvitinh int
As
begin
	select 
		mucluc.iID_MLNS_Cha,
		mucluc.iID_MLNS,
		mucluc.sLNS,
		mucluc.sL,
		mucluc.sK,
		mucluc.sM,
		mucluc.sTM,
		mucluc.sTTM,
		mucluc.sNG,
		mucluc.sMoTa,
		mucluc.bHangCha,
		mucluc.sXauNoiMa
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach as mucluc
		where mucluc.iNamLamViec = @NamLamViec and mucluc.sLNS In (SELECT * FROM f_split(@LNS))

	
	select 
		cp_ct.iID_MLNS,
		 ROUND(sum(isnull(cp_ct.fQTQuyTruoc,0)), 0)  as fQTQuyTruoc,
		ROUND(sum(isnull(cp_ct.fTamUngQuyNay,0)),0) as fTamUngQuyNay,
		ROUND(sum(isnull(cp_ct.fLuyKeCapDenCuoiQuy,0)),0) as fLuyKeCapDenCuoiQuy
		into #tblCapPhatChiTiet
		from BH_CP_CapTamUng_KCB_BHYT_ChiTiet as cp_ct
		inner join BH_CP_CapTamUng_KCB_BHYT as  cp on cp.iID_BH_CP_CapTamUng_KCB_BHYT = cp_ct.iID_BH_CP_CapTamUng_KCB_BHYT
		where cp_ct.iID_MaCoSoYTe In (SELECT * FROM f_split(@IdCsYTe))
		and cp.iQuy = @IQuy and cp.iNamLamViec = @NamLamViec 
		--and ((@ILoai = 5 and cp_ct.sLNS = '9040001') or (@ILoai = 6 and cp_ct.sLNS like '9040002'))
		and cp_ct.sLNS In (SELECT * FROM f_split(@LNS))
		group by cp_ct.iID_MLNS

	select 
		mucluc.iID_MLNS_Cha,
		mucluc.iID_MLNS,
		mucluc.sLNS,
		mucluc.sL,
		mucluc.sK,
		mucluc.sM,
		mucluc.sTM,
		mucluc.sTTM,
		mucluc.sNG,
		mucluc.sMoTa,
		mucluc.bHangCha,
		cp_ct.fLuyKeCapDenCuoiQuy,
		cp_ct.fQTQuyTruoc,
		cp_ct.fTamUngQuyNay
		from #tblMucLucNganSach as mucluc
		left  join #tblCapPhatChiTiet as cp_ct on  mucluc.iID_MLNS = cp_ct.iID_MLNS
		order by mucluc.sXauNoiMa


	drop table #tblMucLucNganSach;
	drop table #tblCapPhatChiTiet;
end
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_export_tonghopcaptamung_kcbbhyt]    Script Date: 1/29/2024 8:46:39 AM ******/
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
			and  cptu.sDSSoChungTuTongHop is not null
			and cptu.iQuy = @IQuy
			--and ((@ILoai = 3 and cptu_ct.sLNS like '9050001') or (@ILoai = 4 and cptu_ct.sLNS like '9050002'))
			AND cptu_ct.sLNS IN (SELECT * FROM splitstring(@LNS))
		group by cptu_ct.iID_MaCoSoYTe, csyt.sTenCoSoYTe
		
end
;
GO
