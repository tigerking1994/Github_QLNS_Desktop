/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_cptubhyt_import]    Script Date: 3/18/2024 2:36:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dt_danhsach_cptubhyt_import]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dt_danhsach_cptubhyt_import]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_cptubhyt_import]    Script Date: 3/18/2024 2:36:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_dt_danhsach_cptubhyt_import]
	@iQuy int,
	@LNS NVARCHAR(MAX),
	@IdCsYTe NVARCHAR(MAX),
	@NamLamViec int,
	@INamKyTruoc int, 
	@IQuyKyTruoc int,
	@UserName NVARCHAR(100)
As
begin
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
		concat(csyt.iID_MaCoSoYTe, '-', csyt.sTenCoSoYTe) as sTenCoSoYTe
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
		CAST(NULL AS VARCHAR(50)) as iID_BH_CP_CapTamUng_KCB_BHYT_ChiTiet,
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
		0 as sGhiChu,
		0 as fQTQuyTruoc, 
		0 as fTamUngQuyNay,
		IsNull(cp_bs.fDaCapUng,0) +  ISNULL(cp_bs.fSoCapBoSung,0) as  FLuyKeCapCacQuyTruoc,
		IsNull(thuaqt.fCapThuaQuyTruocChuyenSang, 0) as FCapThuaQuyTruocChuyenSang,
		#temp.iID_MaCoSoYTe,
		#temp.sTenCoSoYTe
		into #tblCapPhatChiTiet
		from #temp
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
