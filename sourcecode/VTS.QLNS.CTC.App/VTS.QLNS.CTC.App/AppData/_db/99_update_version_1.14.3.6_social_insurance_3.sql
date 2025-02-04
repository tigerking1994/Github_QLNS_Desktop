/****** Object:  StoredProcedure [dbo].[sp_bhxh_export_cap_phat_bo_sung]    Script Date: 22/04/2024 5:06:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_export_cap_phat_bo_sung]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_export_cap_phat_bo_sung]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_export_tonghopcaptamung_kcbbhyt]    Script Date: 22/04/2024 5:06:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_export_tonghopcaptamung_kcbbhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_export_tonghopcaptamung_kcbbhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_export_thongtricaptamung_kcbbhyt]    Script Date: 22/04/2024 5:06:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_export_thongtricaptamung_kcbbhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_export_thongtricaptamung_kcbbhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_cptubhyt_chitiet]    Script Date: 22/04/2024 5:06:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dt_danhsach_cptubhyt_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dt_danhsach_cptubhyt_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_cptubhyt_chitiet]    Script Date: 22/04/2024 5:06:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_dt_danhsach_cptubhyt_chitiet]
	@ChungTuId NVARCHAR(255),
	@LNS NVARCHAR(MAX),
	@IdCsYTe NVARCHAR(MAX),
	@NamLamViec int,
	@INamKyTruoc int, 
	@IQuyKyTruoc int,
	@UserName NVARCHAR(100)
As
begin
	Declare @iQuy int;
	set @iQuy = (select iQuy from BH_CP_CapTamUng_KCB_BHYT where iID_BH_CP_CapTamUng_KCB_BHYT = @ChungTuId)
	--- Lấy danh sách MLNS
	select 
		CAST(NULL AS VARCHAR(50)) as iID_BH_CP_CapTamUng_KCB_BHYT_ChiTiet,
		BH_DM_MucLucNganSach.iID_MLNS as iID_MLNS,
		BH_DM_MucLucNganSach.iID_MLNS_Cha,
		BH_DM_MucLucNganSach.sLNS,
		BH_DM_MucLucNganSach.sL,
		BH_DM_MucLucNganSach.sK,
		BH_DM_MucLucNganSach.sM,
		BH_DM_MucLucNganSach.sTM,
		BH_DM_MucLucNganSach.sTTM,
		BH_DM_MucLucNganSach.sNG,
		BH_DM_MucLucNganSach.sTNG,
		BH_DM_MucLucNganSach.sXauNoiMa,
		BH_DM_MucLucNganSach.sMoTa as sMoTa,
		BH_DM_MucLucNganSach.bHangCha as bHangCha,
		'' as sGhiChu,
		0 as fQTQuyTruoc,
		0 as fTamUngQuyNay,
		--0 as fLuyKeCapDenCuoiQuy,
		0 as FLuyKeCapCacQuyTruoc,
		0 as FCapThuaQuyTruocChuyenSang,
		'' as iID_MaCoSoYTe,
		'' as sTenCoSoYTe
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach 
		where sLNS  IN (SELECT * FROM f_split(@LNS))   and sLNS LIKE '904000%'
		--and iNamLamViec = @NamLamViec


	---Hiển thị mục lục ngân sách con theo đơn vị cơ sở y tế được chọn
	select  
		CAST(NULL AS VARCHAR(50)) as iID_BH_CP_CapTamUng_KCB_BHYT_ChiTiet,
		tblMucLucNganSach.iID_MLNS as iID_MLNS,
		tblMucLucNganSach.iID_MLNS_Cha,
		tblMucLucNganSach.sLNS,
		tblMucLucNganSach.sL,
		tblMucLucNganSach.sK,
		tblMucLucNganSach.sM,
		tblMucLucNganSach.sTM,
		tblMucLucNganSach.sTTM,
		tblMucLucNganSach.sNG,
		tblMucLucNganSach.sTNG,
		tblMucLucNganSach.sXauNoiMa,
		tblMucLucNganSach.sMoTa as sMoTa,
		tblMucLucNganSach.bHangCha as bHangCha,
		'' as sGhiChu,
		0 as fQTQuyTruoc,
		0 as fTamUngQuyNay,
		--0 as fLuyKeCapDenCuoiQuy,
		0 as FLuyKeCapCacQuyTruoc,
		csyt.iID_MaCoSoYTe, 
		csyt.sTenCoSoYTe as sTenCoSoYTe
		into #temp
		from #tblMucLucNganSach  as tblMucLucNganSach 
		cross join DM_CoSoYTe as csyt
		where tblMucLucNganSach.bHangCha = 0 and csyt.iID_MaCoSoYTe In (SELECT * FROM f_split(@IdCsYTe)) AND csyt.iNamLamViec = @NamLamViec;


	-- Lay FluyKeDaCapCuoiQUy
	With #tempFluyKeDaCapQuytruoc(fSoCapBoSung,fDaCapUng,iID_MLNS,iID_MaCoSoYTe)
	AS
	(
		--CapBoSung
		select sum(ct.fSoCapBoSung) as fSoCapBoSung, 0 as fDaCapUng,  iID_MLNS, iID_MaCoSoYTe
					from BH_CP_CapBoSung_KCB_BHYT_ChiTiet as ct
					inner join   BH_CP_CapBoSung_KCB_BHYT bs on ct.iID_CTCapPhatBS = bs.iID_CTCapPhatBS
					where bs.iQuy < @iQuy
					group by iID_MLNS, iID_MaCoSoYTe
		UNION ALL
		--CapTam Ung
				select 0 as fSoCapBoSung, sum(ct.fTamUngQuyNay) as fDaCapUng,  iID_MLNS, iID_MaCoSoYTe
					from BH_CP_CapTamUng_KCB_BHYT_ChiTiet as ct
					inner join   BH_CP_CapTamUng_KCB_BHYT bs on ct.iID_BH_CP_CapTamUng_KCB_BHYT = bs.iID_BH_CP_CapTamUng_KCB_BHYT
					where bs.iQuy < @iQuy
					group by iID_MLNS, iID_MaCoSoYTe
	)

	Select Sum(fSoCapBoSung) as fSoCapBoSung, Sum(fDaCapUng) as fDaCapUng,iID_MLNS, iID_MaCoSoYTe INTO #temCapQuyTruoc
	FROM #tempFluyKeDaCapQuytruoc
	GROUP BY iID_MLNS, iID_MaCoSoYTe

	-- Lay FCapThuaQuyTruocChuyenSang
	SELECT ct.iID_MLNS as iID_MLNS, ct.iID_MaCoSoYTe as iID_MaCoSoYTe ,
		CASE WHEN ct.fDaCapUng >= ct.fDaQuyetToan THEN ISNULL((ct.fDaCapUng - ct.fDaQuyetToan),0)
			WHEN ct.fDaCapUng < ct.fDaQuyetToan THEN 0 END as fCapThuaQuyTruocChuyenSang
	INTO #temCapThuaQuyTruocChuyenSang
	FROM BH_CP_CapBoSung_KCB_BHYT_ChiTiet as ct
	inner join BH_CP_CapBoSung_KCB_BHYT bs on ct.iID_CTCapPhatBS = bs.iID_CTCapPhatBS
	where bs.iQuy = @IQuyKyTruoc AND bs.iNamLamViec = @INamKyTruoc

	---Map với bảng BH_CP_CapTamUng_KCB_BHYT_ChiTiet để lấy thông tin chi tiết

	select 
		ct.iID_BH_CP_CapTamUng_KCB_BHYT_ChiTiet as iID_BH_CP_CapTamUng_KCB_BHYT_ChiTiet,
		#temp.iID_MLNS,
		#temp.iID_MLNS_Cha,
		#temp.sLNS,
		#temp.sL,
		#temp.sK,
		#temp.sM,
		#temp.sTM,
		#temp.sTTM,
		#temp.sNG,
		#temp.sTNG,
		#temp.sXauNoiMa,
		#temp.sMoTa,
		0 as bHangCha,
		ct.sGhiChu as sGhiChu,
		ct.fQTQuyTruoc, 
		ct.fTamUngQuyNay,
		--IsNull(cp_bs.fDaCapUng,0) +  ISNULL(cp_bs.fSoCapBoSung,0) as  fLuyKeCapDenCuoiQuy,
		IsNull(cp_bs.fDaCapUng,0) +  ISNULL(cp_bs.fSoCapBoSung,0) as  FLuyKeCapCacQuyTruoc,
		IsNull(thuaqt.fCapThuaQuyTruocChuyenSang, 0) as FCapThuaQuyTruocChuyenSang,
		#temp.iID_MaCoSoYTe,
		#temp.sTenCoSoYTe
		into #tblCapPhatChiTiet
		from #temp
		left join (select * from BH_CP_CapTamUng_KCB_BHYT_ChiTiet where iID_BH_CP_CapTamUng_KCB_BHYT =  @ChungTuId) as ct 		
			on #temp.iID_MLNS = ct.iID_MLNS and #temp.iID_MaCoSoYTe = ct.iID_MaCoSoYTe
		left join (select * from #temCapThuaQuyTruocChuyenSang) as thuaqt on thuaqt.iID_MLNS = #temp.iID_MLNS AND #temp.iID_MaCoSoYTe = thuaqt.iID_MaCoSoYTe
		left join (select * from #temCapQuyTruoc) as cp_bs 
					on cp_bs.iID_MaCoSoYTe = #temp.iID_MaCoSoYTe and #temp.iID_MLNS = cp_bs.iID_MLNS
	--Kết quả trả về 

	select * from #tblMucLucNganSach
	where bHangCha = 1
	union
	select * from #tblCapPhatChiTiet
	order by sXauNoiMa, iID_MaCoSoYTe

	---drop table
	drop table #tblMucLucNganSach
	drop table #temp
	drop table #tblCapPhatChiTiet
	drop table #temCapQuyTruoc
	drop table #temCapThuaQuyTruocChuyenSang
	
end
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_export_thongtricaptamung_kcbbhyt]    Script Date: 22/04/2024 5:06:39 PM ******/
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
@Donvitinh int,
@IsRoundMillion bit

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

	if (@IsRoundMillion = 1)
	begin
	select 
		cp_ct.iID_MLNS,
		ROUND(sum(isnull(cp_ct.fQTQuyTruoc,0)), 0)  as fQTQuyTruoc,
		ROUND(sum(isnull(cp_ct.fTamUngQuyNay,0)), 0) as fTamUngQuyNay,
		ROUND(sum(isnull(cp_ct.fLuyKeCapDenCuoiQuy,0)), 0) as fLuyKeCapDenCuoiQuy
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
		round((cp_ct.fLuyKeCapDenCuoiQuy / 1000000), 0) * 1000000 / @Donvitinh as fLuyKeCapDenCuoiQuy,
		round((cp_ct.fQTQuyTruoc / 1000000), 0) * 1000000 / @Donvitinh as fQTQuyTruoc,
		round((cp_ct.fTamUngQuyNay / 1000000), 0) * 1000000 / @Donvitinh as fTamUngQuyNay
		from #tblMucLucNganSach as mucluc
		left  join #tblCapPhatChiTiet as cp_ct on  mucluc.iID_MLNS = cp_ct.iID_MLNS
		order by mucluc.sXauNoiMa
	end
	else
	begin
	select
		cp_ct.iID_MLNS,
		ROUND(sum(isnull(cp_ct.fQTQuyTruoc,0)) / @Donvitinh, 0)  as fQTQuyTruoc,
		ROUND(sum(isnull(cp_ct.fTamUngQuyNay,0)) / @Donvitinh, 0) as fTamUngQuyNay,
		ROUND(sum(isnull(cp_ct.fLuyKeCapDenCuoiQuy,0)) / @Donvitinh, 0) as fLuyKeCapDenCuoiQuy
		into #tblCapPhatChiTiet1
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
		left  join #tblCapPhatChiTiet1 as cp_ct on  mucluc.iID_MLNS = cp_ct.iID_MLNS
		order by mucluc.sXauNoiMa
	end

end
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_export_tonghopcaptamung_kcbbhyt]    Script Date: 22/04/2024 5:06:39 PM ******/
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
			ROUND(sum(cptu_ct.fQTQuyTruoc) / @Donvitinh, 0) as fQTQuyTruoc, 
			ROUND(sum(cptu_ct.fTamUngQuyNay) /@Donvitinh, 0) as fTamUngQuyNay, 
			ROUND(sum(cptu_ct.fLuyKeCapDenCuoiQuy) /@Donvitinh, 0) as fLuyKeCapDenCuoiQuy, 
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_export_cap_phat_bo_sung]    Script Date: 22/04/2024 5:06:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_export_cap_phat_bo_sung]
	@VoucherId uniqueidentifier,
	@MaCSYT nvarchar(100),
	@NamLamViec int
AS
BEGIN
	
	select
		mlns.sXauNoiMa, mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sMoTa STenMLNS, mlns.bHangCha,
		chungTu.iID_MaCoSoYTe,
		chungtu.sTenCoSoYTe STenCSYT,
		chungtu.fDaQuyetToan,
		chungtu.fDaCapUng,
		chungtu.fThuaThieu,
		chungtu.fSoCapBoSung,
		chungtu.sGhiChu
	from
	(select sXauNoiMa, iID_MLNS, iID_MLNS_Cha, sMoTa, bHangCha from BH_DM_MucLucNganSach
	where sLNS like '904%'
	and iNamLamViec = @NamLamViec) mlns
	left join
	(select ctct.iID_MLNS,
		csyt.sTenCoSoYTe,
		csyt.iID_MaCoSoYTe,
		ctct.fDaQuyetToan,
		ctct.fDaCapUng,
		ctct.fThuaThieu,
		ctct.fSoCapBoSung,
		ctct.sGhiChu
	from BH_CP_CapBoSung_KCB_BHYT_ChiTiet ctct
	join BH_CP_CapBoSung_KCB_BHYT ct on ctct.iID_CTCapPhatBS = ct.iID_CTCapPhatBS
	join DM_CoSoYTe csyt on ctct.iID_MaCoSoYTe = csyt.iID_MaCoSoYTe
	where 
	ct.iID_CTCapPhatBS = @VoucherId
	and ctct.iID_MaCoSoYTe = @MaCSYT
	and csyt.iNamLamViec = @NamLamViec) chungtu on mlns.iID_MLNS = chungtu.iID_MLNS
	order by mlns.sXauNoiMa

END
;
GO
