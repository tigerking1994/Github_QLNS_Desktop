/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_cptubhyt_chitiet]    Script Date: 11/20/2023 2:34:36 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dt_danhsach_cptubhyt_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dt_danhsach_cptubhyt_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_cptubhyt_chitiet]    Script Date: 11/20/2023 2:34:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_dt_danhsach_cptubhyt_chitiet]
	@ChungTuId NVARCHAR(255),
	@LNS NVARCHAR(MAX),
	@IdCsYTe NVARCHAR(MAX),
	@NamLamViec int,
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
		0 as fLuyKeCapDenCuoiQuy,
		'' as iID_MaCoSoYTe,
		'' as sTenCoSoYTe
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach 
		where sLNS  IN (SELECT * FROM f_split(@LNS))   and sLNS LIKE '904%'
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
		0 as fLuyKeCapDenCuoiQuy,
		DM_CoSoYTe.iID_MaCoSoYTe, 
		concat(DM_CoSoYTe.iID_MaCoSoYTe, '-', DM_CoSoYTe.sTenCoSoYTe) as sTenCoSoYTe
		into #temp
		from #tblMucLucNganSach  as tblMucLucNganSach cross join DM_CoSoYTe 
		where tblMucLucNganSach.bHangCha = 0 and DM_CoSoYTe.iID_MaCoSoYTe In (SELECT * FROM f_split(@IdCsYTe));


	-- Lay FluyKeDaCapCuoiQUy
	With #tempFluyKeDaCapQuytruoc(fSoCapBoSung,fDaCapUng,iID_MLNS,iID_MaCoSoYTe,iQuy)
	AS
	(
		--CapBoSung
		select sum(ct.fSoCapBoSung) as fSoCapBoSung, 0 as fDaCapUng,  iID_MLNS, iID_MaCoSoYTe, iQuy
					from BH_CP_CapBoSung_KCB_BHYT_ChiTiet as ct
					inner join   BH_CP_CapBoSung_KCB_BHYT bs on ct.iID_CTCapPhatBS = bs.iID_CTCapPhatBS
					where bs.iQuy = @iQuy - 1
					group by iID_MLNS, iID_MaCoSoYTe, iQuy
		UNION ALL
		--CapTam Ung
				select 0 as fSoCapBoSung, sum(ct.fTamUngQuyNay) as fDaCapUng,  iID_MLNS, iID_MaCoSoYTe, iQuy
					from BH_CP_CapTamUng_KCB_BHYT_ChiTiet as ct
					inner join   BH_CP_CapTamUng_KCB_BHYT bs on ct.iID_BH_CP_CapTamUng_KCB_BHYT = bs.iID_BH_CP_CapTamUng_KCB_BHYT
					where bs.iQuy = @iQuy - 1
					group by iID_MLNS, iID_MaCoSoYTe, iQuy
	)

	Select Sum(fSoCapBoSung) as fSoCapBoSung, Sum(fDaCapUng) as fDaCapUng,iID_MLNS, iID_MaCoSoYTe, iQuy INTO #temCapQuyTruoc
	FROM #tempFluyKeDaCapQuytruoc
	GROUP BY iID_MLNS, iID_MaCoSoYTe, iQuy

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
		IsNull(cp_bs.fDaCapUng,0) +  ISNULL(cp_bs.fSoCapBoSung,0) as  fLuyKeCapDenCuoiQuy,
		#temp.iID_MaCoSoYTe,
		#temp.sTenCoSoYTe
		into #tblCapPhatChiTiet
		from #temp
		left join (select * from BH_CP_CapTamUng_KCB_BHYT_ChiTiet where iID_BH_CP_CapTamUng_KCB_BHYT =  @ChungTuId)as ct 		
			on #temp.iID_MLNS = ct.iID_MLNS and #temp.iID_MaCoSoYTe = ct.iID_MaCoSoYTe
		left join (
					select * from #temCapQuyTruoc)	as cp_bs 
					on cp_bs.iID_MaCoSoYTe = #temp.iID_MaCoSoYTe and #temp.iID_MLNS = cp_bs.iID_MLNS

	--Kết quả trả về 

	select * from #tblMucLucNganSach
	where bHangCha = 1
	union all
	select * from #tblCapPhatChiTiet
	order by sXauNoiMa, iID_MaCoSoYTe

	---drop table
	drop table #tblMucLucNganSach
	drop table #temp
	drop table #tblCapPhatChiTiet
	drop table #temCapQuyTruoc
	
end
;
GO
