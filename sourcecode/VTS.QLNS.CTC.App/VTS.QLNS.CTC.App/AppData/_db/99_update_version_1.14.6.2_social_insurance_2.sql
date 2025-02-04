/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkp_ql_thongtriloai2_bh]    Script Date: 6/17/2024 3:02:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_qkp_ql_thongtriloai2_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_qkp_ql_thongtriloai2_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_tonghop_KQPL_KCBQY_KCBTS_KPK_chitiet_donvi]    Script Date: 6/17/2024 3:02:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dtc_dieuchinh_tonghop_KQPL_KCBQY_KCBTS_KPK_chitiet_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_tonghop_KQPL_KCBQY_KCBTS_KPK_chitiet_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_tonghop_chitiet_donvi]    Script Date: 6/17/2024 3:02:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dtc_dieuchinh_tonghop_chitiet_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_tonghop_chitiet_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]    Script Date: 6/17/2024 3:02:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_phanbo_dieuchinh_chi_tiet_clone]    Script Date: 6/17/2024 3:02:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_phanbo_dieuchinh_chi_tiet_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_phanbo_dieuchinh_chi_tiet_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_nhanphanbo_dieuchinh_chi_tiet_clone]    Script Date: 6/17/2024 3:02:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_nhanphanbo_dieuchinh_chi_tiet_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_nhanphanbo_dieuchinh_chi_tiet_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_muclucngansach_theodieuchinh]    Script Date: 6/17/2024 3:02:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_muclucngansach_theodieuchinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_muclucngansach_theodieuchinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet]    Script Date: 6/17/2024 3:02:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone]    Script Date: 6/17/2024 3:02:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitheodieuchinh_clone]    Script Date: 6/17/2024 3:02:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_nhandutoanchitheodieuchinh_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_nhandutoanchitheodieuchinh_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_ndt_ctg_get_khc_slns]    Script Date: 6/17/2024 3:02:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_ndt_ctg_get_khc_slns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_ndt_ctg_get_khc_slns]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_dt_export_dieuchinh_chi_tiet]    Script Date: 6/17/2024 3:02:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_dt_export_dieuchinh_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_dt_export_dieuchinh_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_dtdc_clone]    Script Date: 6/17/2024 3:02:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_get_data_dtdc_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_get_data_dtdc_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_export_phan_bo_du_toan_chi_tiet]    Script Date: 6/17/2024 3:02:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_export_phan_bo_du_toan_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_export_phan_bo_du_toan_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet_dieuchinh]    Script Date: 6/17/2024 3:02:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dt_danhsach_pbdtc_chitiet_dieuchinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet_dieuchinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]    Script Date: 6/17/2024 3:02:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_create_quyet_toan_chinambhxh_chitiet_theo4quy]    Script Date: 6/19/2024 10:11:35 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_create_quyet_toan_chinambhxh_chitiet_theo4quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_create_quyet_toan_chinambhxh_chitiet_theo4quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]    Script Date: 6/17/2024 3:02:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]
	@ChungTuId NVARCHAR(255),
	@LNS NVARCHAR(MAX),
	@IdDonVi NVARCHAR(MAX),
	@NamLamViec int,
	@UserName NVARCHAR(100)
As

begin
	-- Lấy danh mục Phân bổ thu BHXH
	SELECT SUM(0.1*(pbctct.fBHYT_NLD+ pbctct.fBHYT_NSD)) as fTienPhanBo, pbctct.iID_MaDonVi INTO #temPBThuBHXH
	FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet as pbctct
	JOIN BH_DTT_BHXH_PhanBo_ChungTu as pbct
	ON pbctct.iID_DTT_BHXH_ChungTu = pbct.iID_DTT_BHXH_PhanBo_ChungTu
	WHERE (pbctct.sXauNoiMa like '9020001-010-011-0001%' or pbctct.sXauNoiMa like '9020002-010-011-0001%')
	AND pbctct.iNamLamViec = @NamLamViec
	AND pbctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND pbct.iLoaiDuToan = 1
	GROUP BY pbctct.iID_MaDonVi

	---Lấy danh sách mục lục ngân sách theo điều kiện @LNS
	select 
	NEWID() as iID_DTC_DuToanChiTrenGiao,
	CAST(NULL AS VARCHAR(50)) as iID_DTC_PhanBoDuToanChiTiet,
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
	BH_DM_MucLucNganSach.sMoTa as sNoiDung,
	0 as fTienTuChi,
	--0 as fTienTuChiTruocDieuChinh,
	--0 as fTienHienVat,
	--0 as fTienHienVatTruocDieuChinh,
	BH_DM_MucLucNganSach.sCPChiTietToi,
	BH_DM_MucLucNganSach.sDuToanChiTietToi,
	1 as Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	'' as sSoQuyetDinh,
	BH_DM_MucLucNganSach.bHangCha as bHangCha,
	BH_DM_MucLucNganSach.bHangChaDuToan,
	0 as IsRemainRow
	into #tblMucLucNganSach
	from BH_DM_MucLucNganSach 
	--where sLNS  IN (SELECT * FROM f_split(@LNS)) and iNamLamViec = @NamLamViec  and sLNS LIKE '901%'
	where sLNS  IN (SELECT * FROM f_split(@LNS)) and iNamLamViec = @NamLamViec  
	and bHangChaDuToan is not null
	and iTrangThai=1
	---Lấy danh sách đơn vị được phân bổ
	select * 
	into  #tblDonVi
	from DonVi where iNamLamViec = @NamLamViec and iID_MaDonVi  IN (SELECT * FROM f_split(@IdDonVi)) ;

	--lấy danh sách dự toán nhận phân bổ
	select *
	into #tblChungTuNhanPhanBo
	from BH_DTC_Nhan_PhanBo_Map
	where iID_BHDTC_PhanBo = @ChungTuId

	
	---Lấy danh sách chi tiết dự toán toán nhận phân bổ
	select 
			nhanpb_chitiet.ID as iIDNhan_ChiTiet,
			nhanpb_chitiet.iID_DTC_DuToanChiTrenGiao as iID_DTC_DuToanChiTrenGiao,
			'' as iID_MaDonVi,
			nhanpb_chitiet.iID_MucLucNganSach,
			nhanpb_chitiet.fTienTuChi as fTuChi,
			--nhanpb_chitiet.fTienHienVat as fHienVat,
			nhanpb.sSoQuyetDinh
	into #tblChiTietDuToanNhan
	from BH_DTC_DuToanChiTrenGiao_ChiTiet as nhanpb_chitiet
	inner join BH_DTC_DuToanChiTrenGiao as nhanpb on nhanpb.ID = nhanpb_chitiet.iID_DTC_DuToanChiTrenGiao
	where iID_DTC_DuToanChiTrenGiao in (select iID_BHDTC_NhanPhanBo from #tblChungTuNhanPhanBo)

	

	---Lấy  danh sách chi tiết nhận phân bổ có thông tin mục lục ngân sách
	select 
		#tblChiTietDuToanNhan.iID_DTC_DuToanChiTrenGiao,
		#tblChiTietDuToanNhan.iID_MucLucNganSach as iID_MLNS,
		#tblMucLucNganSach.iID_MLNS_Cha,
		#tblMucLucNganSach.sLNS,
		#tblMucLucNganSach.sL,
		#tblMucLucNganSach.sK, 
		#tblMucLucNganSach.sM,
		#tblMucLucNganSach.sTM,
		#tblMucLucNganSach.sTTM,
		#tblMucLucNganSach.sNG,
		#tblMucLucNganSach.sTNG,
		#tblMucLucNganSach.sXauNoiMa,
		#tblMucLucNganSach.sNoiDung,
		#tblChiTietDuToanNhan.sSoQuyetDinh,
		#tblChiTietDuToanNhan.fTuChi as fTienTuChi ,
		--#tblChiTietDuToanNhan.fHienVat as fTienHienVat,
		#tblMucLucNganSach.sCPChiTietToi,
		#tblMucLucNganSach.sDuToanChiTietToi,
		3 as Type
	into #tbl_tblChiTietDuToanNhan_MucLuc
	from #tblMucLucNganSach
	inner join #tblChiTietDuToanNhan on #tblMucLucNganSach.iID_MLNS = #tblChiTietDuToanNhan.iID_MucLucNganSach

	


	---Hiển thị danh sách chi tiết nhận phân bổ theo đơn vị được chọn phân bổ
	select  #tbl_tblChiTietDuToanNhan_MucLuc.*,#tblDonVi.iID_MaDonVi, concat(#tblDonVi.iID_MaDonVi, '-', #tblDonVi.sTenDonVi) as sTenDonVi, 0 as bHangCha
	into #temp
	from #tbl_tblChiTietDuToanNhan_MucLuc cross join #tblDonVi 


	---Map với bảng BH_DTC_PhanBoDuToanChi_ChiTiet để lấy thông tin fTuChi đã được phân bổ
	select 
		#temp.iID_DTC_DuToanChiTrenGiao, 
		chitiet_phanbo.ID as iID_DTC_PhanBoDuToanChiTiet,
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
		#temp.sNoiDung as sNoiDung,
		chitiet_phanbo.fTienTuChi as fTienTuChi,
		#temp.fTienTuChi as fTienTuChiTruocDieuChinh,
		--chitiet_phanbo.fTienHienVat as fTienHienVat,
		--#temp.fTienHienVat as fTienHienVatTruocDieuChinh,
		#temp.sCPChiTietToi,
		#temp.sDuToanChiTietToi,
		3 as Type,
		#temp.iID_MaDonVi,
		#temp.sTenDonVi,
		#temp.sSoQuyetDinh,
		0 as bHangCha,
		0 as bHangChaDuToan,
		0 as IsRemainRow
	into #temp1
	from #temp
	left join 
		(
			select * 
			from BH_DTC_PhanBoDuToanChi_ChiTiet 
			where iID_DTC_PhanBoDuToanChi = @ChungTuId
		) as chitiet_phanbo
		on chitiet_phanbo.iID_DTC_DuToanChiTrenGiao = #temp.iID_DTC_DuToanChiTrenGiao and chitiet_phanbo.iID_MaDonVi = #temp.iID_MaDonVi and chitiet_phanbo.iID_MucLucNganSach = #temp.iID_MLNS



	-----Lấy danh sách số chưa phân bổ
	select 
	npb.ID as iID_DTC_DuToanChiTrenGiao,
	CAST(NULL AS VARCHAR(50)) as iID_DTC_PhanBoDuToanChiTiet,
	muluc_ngansach.iID_MLNS as iID_MLNS,
	muluc_ngansach.iID_MLNS_Cha,
	muluc_ngansach.sLNS,
	muluc_ngansach.sL,
	muluc_ngansach.sK,
	muluc_ngansach.sM,
	muluc_ngansach.sTM,
	muluc_ngansach.sTTM,
	muluc_ngansach.sNG,
	muluc_ngansach.sTNG,
	muluc_ngansach.sXauNoiMa,
	N'Số chưa phân bổ' as sNoiDung,
	chitiet_chuaphanbo.fTuChi as fTienTuChi,
	chitiet_chuaphanbo.fTuChi as fTienTuChiTruocDieuChinh,
	--chitiet_chuaphanbo.fHienVat as fTienHienVat,
	--chitiet_chuaphanbo.fHienVat as fTienHienVatTruocDieuChinh,
	muluc_ngansach.sCPChiTietToi,
	muluc_ngansach.sDuToanChiTietToi,
	2 as Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	npb.sSoQuyetDinh as sSoQuyetDinh,
	1 as bHangCha,
	1 as bHangChaDuToan,
	1 as IsRemainRow
	into #tblSoChuaPhanBo
	from #tblMucLucNganSach as muluc_ngansach
	inner join 
	(
		select (
		
		ISNULL(ct_npb.fTienTuChi,0) -  ISNULL(ct_pb_t.fTuChi,0) ) as fTuChi ,
		ct_npb.iID_MucLucNganSach, ct_npb.iID_DTC_DuToanChiTrenGiao 
		--select (ISNULL(ct_npb.fTienTuChi,0) - ISNULL(ct_pb_t.fTuChi,0)) as fTuChi , (ISNULL(ct_npb.fTienHienVat,0) - ISNULL(ct_pb_t.fHienVat,0)) as fHienVat, ct_npb.iID_MucLucNganSach, ct_npb.iID_DTC_DuToanChiTrenGiao 
		from BH_DTC_DuToanChiTrenGiao_ChiTiet as ct_npb
		left join
			(
				select  sum(  fTienTuChi) as fTuChi , iID_MucLucNganSach, iID_DTC_DuToanChiTrenGiao
				from BH_DTC_PhanBoDuToanChi_ChiTiet as ct_pb
				where iID_DTC_DuToanChiTrenGiao in (select iID_BHDTC_NhanPhanBo from #tblChungTuNhanPhanBo)
				group by  iID_MucLucNganSach, iID_DTC_DuToanChiTrenGiao
			)as ct_pb_t  

		on ct_pb_t.iID_MucLucNganSach = ct_npb.iID_MucLucNganSach and ct_npb.iID_DTC_DuToanChiTrenGiao = ct_pb_t.iID_DTC_DuToanChiTrenGiao) as chitiet_chuaphanbo
		on chitiet_chuaphanbo.iID_MucLucNganSach = muluc_ngansach.iID_MLNS 
	inner join BH_DTC_DuToanChiTrenGiao as npb on npb.ID = chitiet_chuaphanbo.iID_DTC_DuToanChiTrenGiao
	where npb.ID in ( select iID_BHDTC_NhanPhanBo from #tblChungTuNhanPhanBo);
	---- Lấy mục lục ngân sách có dupliate các mục lục ngân sách con
	select 
	#tblSoChuaPhanBo.iID_DTC_DuToanChiTrenGiao as iID_DTC_DuToanChiTrenGiao,
	CAST(NULL AS VARCHAR(50)) as iID_DTC_PhanBoDuToanChiTiet,
	#tblMucLucNganSach.iID_MLNS as iID_MLNS,
	#tblMucLucNganSach.iID_MLNS_Cha,
	#tblMucLucNganSach.sLNS,
	#tblMucLucNganSach.sL,
	#tblMucLucNganSach.sK,
	#tblMucLucNganSach.sM,
	#tblMucLucNganSach.sTM,
	#tblMucLucNganSach.sTTM,
	#tblMucLucNganSach.sNG,
	#tblMucLucNganSach.sTNG,
	#tblMucLucNganSach.sXauNoiMa,
	#tblMucLucNganSach.sNoiDung as sNoiDung,
	#tblSoChuaPhanBo.fTienTuChi as fTienTuChi,
	#tblSoChuaPhanBo.fTienTuChiTruocDieuChinh as fTienTuChiTruocDieuChinh,
	--#tblSoChuaPhanBo.fTienHienVat as fTienHienVat,
	--#tblSoChuaPhanBo.fTienHienVatTruocDieuChinh as fTienHienVatTruocDieuChinh,
	#tblMucLucNganSach.sCPChiTietToi,
	#tblMucLucNganSach.sDuToanChiTietToi,
	case when #tblSoChuaPhanBo.Type = 2 then 2 else #tblMucLucNganSach.Type end Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	#tblSoChuaPhanBo.sSoQuyetDinh as sSoQuyetDinh,
	case when #tblSoChuaPhanBo.Type = 2 then 1 else #tblMucLucNganSach.bHangCha end bHangCha,
	case when #tblSoChuaPhanBo.Type = 2 then 1 else #tblMucLucNganSach.bHangCha end bHangChaDuToan,
	0 as IsRemainRow
	into #tblMucLucNganSach_duplicate
	from #tblMucLucNganSach
	left join #tblSoChuaPhanBo on #tblMucLucNganSach.iID_MLNS = #tblSoChuaPhanBo.iID_MLNS
	order by sXauNoiMa
	
	--select * from tblMucLucNganSach_duplicate
	--select * from tblSoChuaPhanBo
	--select * from #temp1
	---Tính lại dự toán, số đã phân bổ
	-- Dữ liệu nhận phân bổ
	declare @iiDotNhan nvarchar(500) =( select  iID_DotNhan from  BH_DTC_PhanBoDuToanChi where ID = @ChungTuId);
	select iID_MucLucNganSach,iID_DTC_DuToanChiTrenGiao, fTienTuChi INTO #tmpNhanDuToan from BH_DTC_DuToanChiTrenGiao_ChiTiet ct
	INNER JOIN BH_DTC_DuToanChiTrenGiao dt on dt.ID = ct.iID_DTC_DuToanChiTrenGiao
	where dt.ID IN (select * from splitstring( @iiDotNhan))


	-- Dữ liệu đã phân bổ
	declare @dNgayQuyetDinh Datetime = (select dNgayQuyetDinh from BH_DTC_PhanBoDuToanChi where ID = @ChungTuId);
	select iID_MucLucNganSach,iID_DTC_DuToanChiTrenGiao, 0 - SUM(ISNULL(fTienTuChi,0)) fTuChi   INTO #tmpDaPhanBo from BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	INNER JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID
	where 
	ct.iNamChungTu = @NamLamViec
	AND ct.dNgayQuyetDinh < @dNgayQuyetDinh
	group by iID_MucLucNganSach,iID_DTC_DuToanChiTrenGiao;

	--Hiển thị kết quả trả về
	select * INTO #result from
	(
		SELECT * from #tblMucLucNganSach_duplicate
		UNION ALL
		SELECT * from #tblSoChuaPhanBo
		UNION ALL
		SELECT * from #temp1
	) as test
	--order by test.sXauNoiMa, test.sSoQuyetDinh, test.iID_MaDonVi,test.Type,test.IsRemainRow

	----============
	SELECT
	rs.iID_DTC_DuToanChiTrenGiao,
	rs.iID_DTC_PhanBoDuToanChiTiet,
	rs.iID_MLNS,
	rs.iID_MLNS_Cha,
	rs.sLNS,
	rs.sL,
	rs.sK,
	rs.sM,
	rs.sTM,
	rs.sTTM,
	rs.sNG,
	rs.sTNG,
	rs.sXauNoiMa,
	rs.sNoiDung as sNoiDung,

	CASE WHEN rs.sXauNoiMa = '9010004' THEN pbbhxh.fTienPhanBo
	ELSE (
		CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fTienTuChi,0) + (ISNULL(dpb.fTuChi, 0))
		WHEN rs.IsRemainRow = 0 AND rs.Type = 2 THEN ISNULL(dt.fTienTuChi,0) + (ISNULL(dpb.fTuChi, 0))
		ELSE rs.fTienTuChi
		END
	)
	END as fTienTuChi,
	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fTienTuChi,0) + (ISNULL(dpb.fTuChi, 0))
		WHEN rs.IsRemainRow = 0 AND rs.Type = 2 THEN ISNULL(dt.fTienTuChi,0) + (ISNULL(dpb.fTuChi, 0))
		ELSE rs.fTienTuChi
	END as fTienTuChiTruocDieuChinh,
	--#tblSoChuaPhanBo.fTienHienVat as fTienHienVat,
	--#tblSoChuaPhanBo.fTienHienVatTruocDieuChinh as fTienHienVatTruocDieuChinh,
	rs.sCPChiTietToi,
	rs.sDuToanChiTietToi,
	rs.Type,
	rs.iID_MaDonVi,
	rs.sTenDonVi,
	rs.sSoQuyetDinh,
	rs.bHangCha,
	rs.bHangChaDuToan,
	rs.IsRemainRow
	FROM #result rs
	LEFT JOIN #tmpNhanDuToan dt ON rs.iID_MLNS = dt.iID_MucLucNganSach 
	LEFT JOIN #tmpDaPhanBo dpb ON dpb.iID_MucLucNganSach = rs.iID_MLNS and dpb.iID_DTC_DuToanChiTrenGiao = rs.iID_DTC_DuToanChiTrenGiao
	LEFT JOIN (
	SELECT SUM(fTienTuChi) fTuChi, iID_MucLucNganSach FROM BH_DTC_PhanBoDuToanChi_ChiTiet WHERE iID_DTC_PhanBoDuToanChi = @ChungTuId GROUP BY iID_MucLucNganSach

	) ct ON ct.iID_MucLucNganSach = rs.iID_MLNS
	LEFT JOIN #temPBThuBHXH pbbhxh ON rs.iID_MaDonVi = pbbhxh.iID_MaDonVi
	order by rs.sXauNoiMa, rs.sSoQuyetDinh, rs.iID_MaDonVi,rs.Type,rs.IsRemainRow
	--SELECT * from #tblSoChuaPhanBo


drop table #tblMucLucNganSach;
drop table #tblDonVi;
drop table #tblChungTuNhanPhanBo;
drop table #tblChiTietDuToanNhan;
drop table #tbl_tblChiTietDuToanNhan_MucLuc;
drop table #temp;
drop table #temp1;
drop table #tblSoChuaPhanBo;
drop table #tblMucLucNganSach_duplicate;

end
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet_dieuchinh]    Script Date: 6/17/2024 3:02:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet_dieuchinh]
	@ChungTuId NVARCHAR(255),
	@LNS NVARCHAR(MAX),
	@NamLamViec int,
	@UserName NVARCHAR(100)
AS
begin
	---L?y danh sách m?c l?c ngân sách
	select 
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
	BH_DM_MucLucNganSach.sMoTa as sNoiDung,
	NULL as iID_DTC_DuToanChiTrenGiao,
	NULL as IID_DTC_PhanBoDuToanChiTiet,
	NULL as iID_MaDonVi,
	NULL as sTenDonVi,
	NULL as sSoQuyetDinh,
	0 as fTienTuChiTruocDieuChinh,
	0 as fTienTuChi,
	0 as fTienTuChiSauDieuChinh,
	0 as fTienHienVatTruocDieuChinh,
	0 as fTienHienVat,
	0 as fTienHienVatSauDieuChinh,
	BH_DM_MucLucNganSach.sCPChiTietToi,
	BH_DM_MucLucNganSach.sDuToanChiTietToi,
	bHangCha,
	0 as Type
	into #tblMucLucNganSach
	from BH_DM_MucLucNganSach 
	where sLNS  IN (SELECT * FROM f_split(@LNS)) and iNamLamViec = @NamLamViec 
	and iTrangThai=1
	--where sLNS  IN (SELECT * FROM f_split(@LNS)) and iNamLamViec = @NamLamViec and sLNS LIKE '901%'


	---L?y danh sách ch?ng t? b? ?i?u ch?nh
	select * 
	into #tblChungTuBiDieuChinh
	from BH_DTC_Nhan_PhanBo_Map
	where iID_BHDTC_PhanBo = @ChungTuId


	---L?y danh sách chi ti?t ch?ng t? b? ?i?u ch?nh
	select 
	dc_ct.iID_MucLucNganSach, 
	dc_ct.iID_MaDonVi,
	dc_ct.fTienTuChi,
	dc_ct.fTienHienVat,
	dc.ID as iID_DTC_DuToanChiTrenGiao,
	dc_ct.ID,
	dc.sSoQuyetDinh
	into #tblChungTuBiDieuChinh_Ct 
	from BH_DTC_PhanBoDuToanChi_ChiTiet as dc_ct  
	inner join BH_DTC_PhanBoDuToanChi as dc on dc_ct.iID_DTC_PhanBoDuToanChi = dc.ID
	where dc_ct.iID_DTC_PhanBoDuToanChi in ( select iID_BHDTC_NhanPhanBo from #tblChungTuBiDieuChinh)

	---Thông tin chi tiết chứng từ bị điều chỉnh 
	select 
	npb_ct.iID_MucLucNganSach,
	npb_ct.iID_MaDonVi,
	npb_ct.sSoQuyetDinh,
	npb_ct.fTienTuChi as fTienTuChiTruocDieuChinh,
	npb_ct.fTienHienVat as fTienHienVatTruocDieuChinh,
	npb_ct.iID_DTC_DuToanChiTrenGiao as iID_DTC_DuToanChiTrenGiao,
	pb_ct.ID as IID_DTC_PhanBoDuToanChiTiet,
	pb_ct.fTienTuChi,
	pb_ct.fTienHienVat,
	0 as bHangCha,
	2 Type
	into #tblThongTinChungTu_BiDieuChinh
	from #tblChungTuBiDieuChinh_Ct as npb_ct
	left join ( select * from BH_DTC_PhanBoDuToanChi_ChiTiet where iID_DTC_PhanBoDuToanChi =  @ChungTuId) as  pb_ct on npb_ct.iID_MucLucNganSach = pb_ct.iID_MucLucNganSach and npb_ct.iID_MaDonVi = pb_ct.iID_MaDonVi
	and npb_ct.iID_DTC_DuToanChiTrenGiao = pb_ct.iID_DTC_DuToanChiTrenGiao

	

	---Thong tin chi tiết chứng từ điều chỉnh thêm mới
	select 
	pb_ct.iID_MucLucNganSach,
	pb_ct.iID_MaDonVi,
	pb.sSoQuyetDinh,
	0 as fTienTuChiTruocDieuChinh,
	0 as fTienHienVatTruocDieuChinh,
	pb_ct.iID_DTC_DuToanChiTrenGiao as iID_DTC_DuToanChiTrenGiao,
	pb_ct.ID as iID_DTC_PhanBoDuToanChiTiet,
	pb_ct.fTienTuChi,
	pb_ct.fTienHienVat,
	0 as bHangCha,
	2 Type
	into #tblThongTinChungTu_DieuChinhThemMoi
	from BH_DTC_PhanBoDuToanChi_ChiTiet as pb_ct
	inner join BH_DTC_PhanBoDuToanChi as pb on pb_ct.iID_DTC_DuToanChiTrenGiao = pb.ID
	where pb_ct.iID_DTC_PhanBoDuToanChi = @ChungTuId and pb_ct.ID not in  (select IID_DTC_PhanBoDuToanChiTiet from  #tblThongTinChungTu_BiDieuChinh where IID_DTC_PhanBoDuToanChiTiet is not null)

	---Thông tin chi tiết chúng từ
	select * 
	into #tblThongTinChungTu
	from
		(
			select * from #tblThongTinChungTu_BiDieuChinh
			UNION ALL
			select * from #tblThongTinChungTu_DieuChinhThemMoi

		) as tbl;


	---Hi?n th? k?t qu? tr? v? có m?c l?c ngân sách

	select 
	#tblMucLucNganSach.iID_MLNS,
	#tblMucLucNganSach.iID_MLNS_Cha,
	#tblMucLucNganSach.sLNS,
	#tblMucLucNganSach.sL,
	#tblMucLucNganSach.sK,
	#tblMucLucNganSach.sM,
	#tblMucLucNganSach.sTM,
	#tblMucLucNganSach.sTTM,
	#tblMucLucNganSach.sNG,
	#tblMucLucNganSach.sTNG,
	#tblMucLucNganSach.sXauNoiMa,
	#tblMucLucNganSach.sNoiDung,
	#tblMucLucNganSach.sCPChiTietToi,
	#tblMucLucNganSach.sDuToanChiTietToi,
	#tblThongTinChungTu.iID_DTC_DuToanChiTrenGiao,
	#tblThongTinChungTu.IID_DTC_PhanBoDuToanChiTiet,
	#tblThongTinChungTu.iID_MaDonVi,
	CONCAT(DonVi.iID_MaDonVi, '-', DonVi.sTenDonVi) as sTenDonVi,
	#tblThongTinChungTu.sSoQuyetDinh,
	#tblThongTinChungTu.fTienTuChiTruocDieuChinh,
	#tblThongTinChungTu.fTienTuChi,
	ISNULL(#tblThongTinChungTu.fTienTuChiTruocDieuChinh,0) + ISNULL(#tblThongTinChungTu.fTienTuChi,0) as fTienTuChiSauDieuChinh,
	#tblThongTinChungTu.fTienHienVatTruocDieuChinh,
	#tblThongTinChungTu.fTienHienVat,
	ISNULL(#tblThongTinChungTu.fTienHienVatTruocDieuChinh,0) + ISNULL(#tblThongTinChungTu.fTienHienVat,0) as fTienHienVatSauDieuChinh,
	case when #tblThongTinChungTu.Type = 2 then #tblThongTinChungTu.bHangCha else #tblMucLucNganSach.bHangCha end as bHangCha
	into #tblThongTinChungTu_MLNS
	from #tblMucLucNganSach
	left join #tblThongTinChungTu on #tblMucLucNganSach.iID_MLNS = #tblThongTinChungTu.iID_MucLucNganSach
	left join DonVi on #tblThongTinChungTu.iID_MaDonVi = DonVi.iID_MaDonVi and DonVi.iNamLamViec =@NamLamViec

	select * from #tblThongTinChungTu_MLNS 
	order  by sXauNoiMa, sSoQuyetDinh 

	drop table #tblMucLucNganSach;
	drop table #tblChungTuBiDieuChinh;
	drop table #tblChungTuBiDieuChinh_Ct;
	drop table #tblThongTinChungTu_BiDieuChinh
	drop table #tblThongTinChungTu_DieuChinhThemMoi;
	drop table #tblThongTinChungTu;
	drop table #tblThongTinChungTu_MLNS;


end
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_export_phan_bo_du_toan_chi_tiet]    Script Date: 6/17/2024 3:02:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[sp_bh_export_phan_bo_du_toan_chi_tiet]
	@ChungTuId nvarchar(255),
	@LNS nvarchar(max),
	@NamLamViec int,
	@MaDonVi nvarchar(255)
As
Begin

select 
	ngan_sach.iID_MLNS,
	ngan_sach.iID_MLNS_Cha,
	ngan_sach.sLNS,
	ngan_sach.sL,
	ngan_sach.sK,
	ngan_sach.sM,
	ngan_sach.sTM,
	ngan_sach.sTTM,
	ngan_sach.sNG,
	ngan_sach.sTNG,
	ngan_sach.sXauNoiMa,
	ngan_sach.sMoTa as sNoiDung,
	ngan_sach.sDuToanChiTietToi,
	ngan_sach.bHangChaDuToan as bHangCha,
	pb_ct.fTienTuChi,
	pb_ct.fTienHienVat,
	pb_ct.IID_MaDonVi,
	pb_ct.sMaLoaiChi
	from
	BH_DM_MucLucNganSach as ngan_sach
	left join
	(
		select sum(fTienTuChi) as fTienTuChi, sum(fTienHienVat) as fTienHienVat, IID_MaDonVi, iID_MucLucNganSach, sMaLoaiChi
		from BH_DTC_PhanBoDuToanChi_ChiTiet 
		where iID_DTC_PhanBoDuToanChi = @ChungTuId  and iID_MaDonVi = @MaDonVi
		group by iID_MucLucNganSach, IID_MaDonVi,sMaLoaiChi) as pb_ct on pb_ct.iID_MucLucNganSach = ngan_sach.iID_MLNS
	where ngan_sach.iNamLamViec  = @NamLamViec and ngan_sach.sLNS in (select * from f_split(@LNS))
	and ngan_sach.bHangChaDuToan is not null and ngan_sach.iTrangThai=1
	order by sXauNoiMa
End
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_dtdc_clone]    Script Date: 6/17/2024 3:02:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_get_data_dtdc_clone]
	@NamLamViec int,
	@SoChungTu nvarchar(500),
	@SLNS nvarchar(500),
	@IDLoaiChi nvarchar(500)
AS
BEGIN
	select
		ct.iID_MaDonVi IIdMaDonVi,
		mlns.sXauNoiMa,
		mlns.iID_MLNS IID_MucLucNganSach,
		mlns.sLNS,
		CASE WHEN (ISNULL(ctct.fTienSoSanhTang,0))-  (ISNULL(ctct.fTienSoSanhGiam,0)) >0 THEN (ISNULL(ctct.fTienSoSanhTang,0))-  (ISNULL(ctct.fTienSoSanhGiam,0))
				ELSE -(((ISNULL(ctct.fTienSoSanhGiam,0))-ISNULL(ctct.fTienSoSanhTang,0))) END FTienTangGiam
		into #temp
	from
	BH_DM_MucLucNganSach mlns
	left join
	BH_DTC_DieuChinhDuToanChi_ChiTiet ctct on ctct.iID_MucLucNganSach = mlns.iID_MLNS
	join
	(select * from BH_DTC_DieuChinhDuToanChi
		where iNamLamViec = @NamLamViec and bDaTongHop=1
		and sSoChungTu in ((SELECT * FROM f_split(@SoChungTu)))) ct on ctct.iID_BH_DTC = ct.iID_BH_DTC
	where mlns.iNamLamViec=@NamLamViec AND mlns.iTrangThai=1

	--- Get data
	select * INTO #result from
	(
		--- che do bao hiem
		select SUBSTRING(A.sXauNoiMa,1,20) sXauNoiMa ,A.IIdMaDonVi, 
		SUM(A.FTienTangGiam) FTienTangGiam
		from #temp A
		where A.sLNS in (select * from splitstring('9010001,9010002'))
		Group by SUBSTRING(A.sXauNoiMa,1,20),A.IIdMaDonVi
		--- Cssk hssv va nld
		UNION ALL
		select SUBSTRING(A.sXauNoiMa,1,3) sXauNoiMa ,A.IIdMaDonVi, 
		SUM(A.FTienTangGiam) FTienTangGiam
		from #temp A
		where A.sLNS in (select * from splitstring('905,9050001,9050002'))
		Group by SUBSTRING(A.sXauNoiMa,1,3),A.IIdMaDonVi
		--- KPQL, KCB quan y, KCB truong sa,  KCB BHYT , TTB Y Te, BHTN
		UNION ALL
		select SUBSTRING(A.sXauNoiMa,1,7) sXauNoiMa ,A.IIdMaDonVi, 
		SUM(A.FTienTangGiam) FTienTangGiam
		from #temp A
		where A.sLNS in (select * from splitstring('9010004,9010003,9010006,9010008,9010009,9010010'))
		Group by SUBSTRING(A.sXauNoiMa,1,7),A.IIdMaDonVi

		) as test

	select * from #result

	drop table #temp
	drop table #result

END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_dt_export_dieuchinh_chi_tiet]    Script Date: 6/17/2024 3:02:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_bhxh_dt_export_dieuchinh_chi_tiet]
	@ChungTuId nvarchar(255),
	@LNS nvarchar(max),
	@NamLamViec int,
	@MaLoaiChi nvarchar(255),
	@MaDonVi nvarchar(255)
As
Begin

		SELECT Ml.*,
			Ml.sMoTa sNoiDung,
			Ml.bHangChaDuToanDieuChinh IsHangCha,
			CTCT.fTienDuToanDuocGiao FTienDuToanDuocGiao,
			CTCT.fTienSoSanhGiam FTienSoSanhGiam,
			CTCT.fTienSoSanhTang FTienSoSanhTang,
			CTCT.fTienUocThucHien06ThangCuoiNam FTienUocThucHien06ThangCuoiNam,
			CTCT.FTienThucHien06ThangDauNam FTienThucHien06ThangDauNam,
			CTCT.fTienUocThucHienCaNam  FTienUocThucHienCaNam
		FROM BH_DM_MucLucNganSach ML
		LEFT JOIN
			( SELECT * FROM BH_DTC_DieuChinhDuToanChi_ChiTiet 
					WHERE iID_BH_DTC IN
					(
					SELECT iID_BH_DTC FROM BH_DTC_DieuChinhDuToanChi
							WHERE iID_BH_DTC=@ChungTuId
							AND sMaLoaiChi=@MaLoaiChi
							AND iNamLamViec=@NamLamViec
							AND iID_MaDonVi=@MaDonVi
					)) CTCT ON ML.sXauNoiMa =CTCT.sXauNoiMa 

		WHERE ML.iNamLamViec=@NamLamViec
		AND ML.sLNS IN (SELECT * FROM splitstring(@LNS))
		AND ML.bHangChaDuToanDieuChinh IS NOT NULL
		AND ML.iTrangThai=1
		--AND (ML.sDuToanChiTietToi IS NOT NULL or ML.sL is null)
		ORDER BY ML.sXauNoiMa

	
End
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_ndt_ctg_get_khc_slns]    Script Date: 6/17/2024 3:02:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_ndt_ctg_get_khc_slns]
@iDNdtctg uniqueidentifier,
@sLns nvarchar(max),
@NamLamViec int,
@IIDDonVi nvarchar(50)

AS
BEGIN

	DECLARE @fTienDauNam FLOAT;
	SELECT @fTienDauNam = SUM(0.1*(ctct.fThu_BHYT_NLD + ctct.fThu_BHYT_NSD))
	FROM BH_DTT_BHXH_ChungTu_ChiTiet as ctct
	JOIN BH_DTT_BHXH_ChungTu as ct
	ON ctct.iID_DTT_BHXH = ct.iID_DTT_BHXH
	WHERE
	(ctct.sXauNoiMa like '9020001-010-011-0001%' or ctct.sXauNoiMa like '9020002-010-011-0001%')
	AND ctct.iNamLamViec = @NamLamViec
	AND ct.iLoaiDuToan = 1

SELECT 
	A.sLNS,
	A.sTM,
	A.sTTM,
	A.sM,
	A.sNG,
	A.sMoTa AS sNoiDung,
	A.sXauNoiMa,
	A.iID_MLNS,
	A.iID_MLNS_Cha,
	A.bHangCha,
	a.bHangChaDuToan,
	A.bHangCha as isHangCha,
	B.iID_DTC_DuToanChiTrenGiao,
	A.iID_MLNS AS iID_MucLucNganSach,
	CASE WHEN A.sXauNoiMa = '9010004' THEN @fTienDauNam ELSE B.fTienKeHoachThucHienNamNay END as fTienTuChi,
	A.iNamlamViec,
	A.sCPChiTietToi,
	A.sDuToanChiTietToi,
	@IIDDonVi as iID_MaDonVi
	FROM BH_DM_MucLucNganSach AS A
		LEFT JOIN (
					select	
					@iDNdtctg as iID_DTC_DuToanChiTrenGiao
					, ctct.sXauNoiMa
					, ctct.fTienKeHoachThucHienNamNay
					from BH_KHC_CheDoBHXH_ChiTiet ctct
					left join BH_KHC_CheDoBHXH ct on ctct.iID_KHC_CheDoBHXH=ct.ID
					where (ct.iLoaiTongHop=2 ) and ct.iID_MaDonVi=@IIDDonVi
					and ct.iNamLamViec=@NamLamViec
					and ct.bIsKhoa=1
					UNION All

					select	
					@iDNdtctg as iID_DTC_DuToanChiTrenGiao
					, ctct.sXauNoiMa
					, ctct.fTienKeHoachThucHienNamNay
					from BH_KHC_KinhPhiQuanLy_ChiTiet ctct
					left join BH_KHC_KinhPhiQuanLy ct on ctct.iID_KHC_KinhPhiQuanLy=ct.iID_BH_KHC_KinhPhiQuanLy
					where (ct.iLoaiTongHop=2 ) and ct.iID_MaDonVi=@IIDDonVi
					and ct.iNamLamViec=@NamLamViec
					and ct.bIsKhoa=1

					UNION ALL
					select
					@iDNdtctg as iID_DTC_DuToanChiTrenGiao
					, ctct.sXauNoiMa
					, ctct.fTienKeHoachThucHienNamNay
					from BH_KHC_KCB_ChiTiet ctct
					left join BH_KHC_KCB ct on ctct.iID_KHC_KCB=ct.iID_BH_KHC_KCB
					where (ct.iLoaiTongHop=2 ) and ct.iID_MaDonVi=@IIDDonVi
					and ct.iNamLamViec=@NamLamViec
					and ct.bIsKhoa=1

					UNION ALL
					select	
					@iDNdtctg as iID_DTC_DuToanChiTrenGiao
					, ctct.sXauNoiMa
					, ctct.fTienKeHoachThucHienNamNay
					from BH_KHC_K_ChiTiet ctct
					left join BH_KHC_K ct on ctct.iID_KHC_K=ct.iID_BH_KHC_K
					where (ct.iLoaiTongHop=2 ) and ct.iID_MaDonVi=@IIDDonVi
					and ct.iNamLamViec=@NamLamViec
					and ct.bIsKhoa=1
			
			) AS B
		ON A.sXauNoiMa = B.sXauNoiMa
	WHERE   A.sLNS IN (SELECT * FROM f_split(@sLns))
		AND A.iNamlamViec=@NamLamViec
		AND A.iTrangThai=1
		--AND a.bHangChaDuToan IS NOT NULL
	order by A.sXauNoiMa
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitheodieuchinh_clone]    Script Date: 6/17/2024 3:02:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_nhandutoanchitheodieuchinh_clone]
@sLns nvarchar(max),
@NamLamViec int,
@IIDDonVi nvarchar(50),
@DNgayChungTu datetime

AS
BEGIN
		-- Get dieu chinh
		SELECT 
			ct.sXauNoiMa,
			Sum(isnull(ct.fTienThucHien06ThangDauNam,0)) fTienThucHien06ThangDauNam,
			Sum(isnull(ct.fTienUocThucHien06ThangCuoiNam,0)) fTienUocThucHien06ThangCuoiNam
			into #tempDieuChinh
		FROM
			(
				SELECT
					--bh.iID_MaDonVi,
					bh.sMoTa,
					ddv.sTenDonVi,
					bhct.*
				FROM 
					BH_DTC_DieuChinhDuToanChi bh
				JOIN 
					BH_DTC_DieuChinhDuToanChi_ChiTiet bhct ON bh.iID_BH_DTC = bhct.iID_BH_DTC 
				LEFT JOIN 
					(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
				WHERE
					bh.iID_MaDonVi in (SELECT * FROM splitstring(@IIDDonVi))
					AND	bh.iNamLamViec=@NamLamViec
					--and bh.bIsKhoa=1
					and bh.iLoaiTongHop=2
					--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
			) ct
			Group by ct.sXauNoiMa

		-- get phan bo đầu nam 
		SELECT 
		ct.iNamLamViec,
		ct.sXauNoiMa,
		Sum(ISNULL(ct.fTienTuChi, 0))as FTienTuChi
		into #tempNhanpbt
	FROM
		(
			SELECT
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTC_DuToanChiTrenGiao bh
			JOIN 
				BH_DTC_DuToanChiTrenGiao_ChiTiet bhct ON bh.ID = bhct.iID_DTC_DuToanChiTrenGiao 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bhct.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bhct.iID_MaDonVi = @IIDDonVi
				and bh.bIsKhoa = 1
				AND bh.sSoQuyetDinh IS NOT NULL
				AND bh.sSoQuyetDinh <> ''
				AND cast(bh.DngayChungTu as date) <= cast(@DNgayChungTu as date)
				AND bh.iNamLamViec=@NamLamViec
		) ct
		
		Group by 
		ct.sXauNoiMa,
		ct.iNamLamViec

		-- get data
		SELECT 
		A.sLNS,
		A.sTM,
		A.sTTM,
		A.sM,
		A.sNG,
		A.sMoTa AS sNoiDung,
		A.sXauNoiMa,
		A.iID_MLNS as IID_MucLucNganSach,
		A.iID_MLNS_Cha as IdParent,
		A.bHangChaDuToan bHangCha,
		A.bHangChaDuToan as isHangCha,
		A.iID_MLNS AS iID_MucLucNganSach,
		C.FTienTuChi fTienDuToanDuocGiao,
		B.fTienThucHien06ThangDauNam,
		B.fTienUocThucHien06ThangCuoiNam,
		A.sCPChiTietToi,
		A.sDuToanChiTietToi,
		A.bHangCha as IsHangCha,
		A.bHangCha ,
		A.bHangChaDuToan,
		@NamLamViec iNamlamViec,
		@IIDDonVi as iID_MaDonVi
		FROM BH_DM_MucLucNganSach AS A
		LEFT JOIN #tempDieuChinh AS B
		ON A.sXauNoiMa = B.sXauNoiMa
		LEFT JOIN #tempNhanpbt AS C
		ON A.sXauNoiMa=C.sXauNoiMa
		WHERE   A.sLNS IN (SELECT * FROM f_split( @sLns))
			AND A.iNamlamViec=@NamLamViec
			AND A.iTrangThai=1
			--AND A.bHangChaDuToan IS NOT NULL
	order by A.sXauNoiMa


END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone]    Script Date: 6/17/2024 3:02:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone]
@iDNdtctg uniqueidentifier,
@sLns nvarchar(max),
@NamLamViec int,
@IIDDonVi nvarchar(50)

AS
BEGIN

	DECLARE @fTienDauNam FLOAT;
	SELECT @fTienDauNam = SUM(0.1*(ctct.fThu_BHYT_NLD + ctct.fThu_BHYT_NSD))
	FROM BH_DTT_BHXH_ChungTu_ChiTiet as ctct
	JOIN BH_DTT_BHXH_ChungTu as ct
	ON ctct.iID_DTT_BHXH = ct.iID_DTT_BHXH
	WHERE
	(ctct.sXauNoiMa like '9020001-010-011-0001%' or ctct.sXauNoiMa like '9020002-010-011-0001%')
	AND ctct.iNamLamViec = @NamLamViec
	AND ct.iLoaiDuToan = 1


SELECT 
	A.sLNS,
	A.sTM,
	A.sTTM,
	A.sM,
	A.sNG,
	A.sMoTa AS sNoiDung,
	A.sXauNoiMa,
	A.iID_MLNS,
	A.iID_MLNS_Cha,
	A.bHangChaDuToan bHangCha,
	A.bHangChaDuToan as isHangCha,
	B.ID,
	B.iID_DTC_DuToanChiTrenGiao,
	A.iID_MLNS AS iID_MucLucNganSach,
	B.fTongTien,
	B.fTienHienVat,
	CASE WHEN A.sXauNoiMa = '9010004' THEN @fTienDauNam ELSE B.fTienTuChi END as fTienTuChi,
	A.sCPChiTietToi,
	A.sDuToanChiTietToi,
	A.iNamlamViec,
	@IIDDonVi as iID_MaDonVi,
	 B.dNgaySua,
	 B.dNgayTao,
	 B.sNguoiSua,
	 B.sNguoiTao
	FROM BH_DM_MucLucNganSach AS A
	LEFT JOIN ( SELECT ctct.*, CT.iLoaiDotNhanPhanBo
					FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
				LEFT JOIN BH_DTC_DuToanChiTrenGiao CT ON ctct.iID_DTC_DuToanChiTrenGiao=CT.ID 
				WHERE ctct.iID_DTC_DuToanChiTrenGiao = @iDNdtctg
					 and ct.iID_MaDonVi=@IIDDonVi 
					 And CT.iNamLamViec=@NamLamViec) AS B
	ON B.iID_MucLucNganSach = A.iID_MLNS
	WHERE   A.sLNS IN (SELECT * FROM f_split( @sLns))
		AND A.iNamlamViec=@NamLamViec
		AND A.bHangChaDuToan IS NOT NULL
		AND A.iTrangThai=1
	order by A.sXauNoiMa
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet]    Script Date: 6/17/2024 3:02:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_chungtu_chi_tiet]
	@VoucherId uniqueidentifier ,
	@LNS nvarchar(max),
	@YearOfWork int,
	@AgencyId nvarchar(500),
	@VoucherDate datetime,
	@iID_LoaiDanhMucChi nvarchar(255)
AS
BEGIN
	DECLARE @iD uniqueidentifier='00000000-0000-0000-0000-000000000000';
	DECLARE @CountIndex INT;
	SELECT * into #tempLNS from f_split(@LNS);
	SELECT * into #tempAgency from  f_split(@AgencyId);
	SELECT @CountIndex = COUNT(*) 
	FROM BH_CP_ChungTu_ChiTiet
	WHERE iID_CP_ChungTu = @VoucherId

	DECLARE
		@quy INT;
	SELECT
		@quy = iQuy 
	FROM
		BH_CP_ChungTu 
	WHERE
		iID_CP_ChungTu = @VoucherId;

	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha,sCPChiTietToi
			into #tblNsMlns
	FROM BH_DM_MucLucNganSach 
	where iNamLamViec = @YearOfWork 
		AND iTrangThai=1



		-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(fTienTuChi) AS fTienDuToan,
		  --fTongTien AS fTienDuToan,
           0 AS fTienKeHoachCap,
          0 AS fTienDaCap,
          iID_MaDonVi,
          iID_MucLucNganSach
		  into #tblPhanBoDuToan
   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
   WHERE iID_DTC_PhanBoDuToanChi IN
       (SELECT ID
        FROM BH_DTC_PhanBoDuToanChi
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iNamChungTu = @YearOfWork
		  AND bIsKhoa=1
          AND cast(dNgayQuyetDinh AS date) <= cast(@VoucherDate AS date))
     AND iID_MaDonVi in  (SELECT * FROM f_split(@AgencyId))-- donvi
   GROUP BY iID_MaDonVi, 
   iID_MucLucNganSach

	SELECT DISTINCT iID_MucLucNganSach
	--, iID_MLNS_Cha 
	into #tblPhanBoDuToanGroupbyiID_MLNS from #tblPhanBoDuToan

	SELECT T.* INTO #tblNsMlns1
	FROM #tblNsMlns T
	INNER JOIN #tblPhanBoDuToanGroupbyiID_MLNS E ON T.iID_MLNS = E.iID_MucLucNganSach 


	;WITH C AS  
	(  
	  SELECT *
	  FROM #tblNsMlns1
	  UNION ALL 
	  SELECT T.*
	  FROM #tblNsMlns T
	  INNER JOIN C
	  ON T.iID_MLNS = C.iID_MLNS_Cha
	)

	SELECT DISTINCT * into #mlnsPhanBo from C;

	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa,
			CASE
				WHEN (sNG is null OR sNG = '') THEN cast(1 as bit)
			ELSE cast(0 as bit)
			END AS bHangCha	
			into #tblMlnsByPhanCap
	FROM #mlnsPhanBo 
	WHERE 
		sLNS IN (SELECT * FROM #tempLNS)

	-- lấy ra đơn vị từ đơn vị của chứng từ
	SELECT iID_MaDonVi, iID_MaDonVi + ' - ' + sTenDonVi as sTenDonVi into #tblDonVi
	FROM DonVi 
	WHERE 
		INamLamViec = @YearOfWork 
		AND iID_MaDonVi IN (SELECT * FROM #tempAgency)

	-- Tao bang tam luu chu mlng con
	SELECT * INTO #tblMlnsByPhanCapExistDonVi
	FROM #tblMlnsByPhanCap tbl, #tblDonVi dv
	WHERE  tbl.bHangCha = 0 OR (tbl.bHangCha = 1 and (IsNull(tbl.sM, '') != '' OR tbl.sM <> '' OR IsNull(tbl.sTM, '') != '')  )
	AND tbl.sTM !=''  


	-- Tao bang tam luu chu mlng cha
	SELECT *, '' AS iID_MaDonVi, '' AS sTenDonVi  INTO #tblMlnsByPhanCapNotDonVi
	FROM #tblMlnsByPhanCap 
		WHERE bHangCha = 1 AND ( (IsNull(sM, '') = '' or sM = '')) AND ( (IsNull(sTM, '') = '' or sTM = '')) OR ((ISNULL(sTM,'')='' OR sTM='')) OR sTM='0'

	-- map bảng mlnsPhanCap và đơn vị
	SELECT * INTO #tblMLNS FROM (
		SELECT *
		FROM #tblMlnsByPhanCapExistDonVi tbl
		WHERE sTM !='0'
		UNION ALL
		SELECT *
		FROM #tblMlnsByPhanCapNotDonVi 
	) mlns

	-- map bảng mlns ns và đơn vị
	SELECT * INTO #tblMlnsRoot FROM (
		SELECT * FROM #tblNsMlns, #tblDonVi 
		WHERE sLNS in (select * from splitstring(@LNS)) and  bHangCha = 1
			and( sL <>'' or sL is not null) and (sCPChiTietToi <>'' or sCPChiTietToi is not null)
		UNION ALL
		SELECT *, null AS iID_MaDonVi, null AS sTenDonVi  
		FROM #tblNsMlns 
		WHERE sLNS in (select * from splitstring(@LNS)) and  bHangCha = 1
			and( sL ='' or sL is null) 
			and (sCPChiTietToi ='' or sCPChiTietToi is null)
	) mlns

	SELECT 
	SUM(ctct.fTienKeHoachCap) fTienDaCap,
	SUM(ctct.fTienKeHoachCap) fTienKeHoachCap,
	ctct.iID_MaDonVi,
	ct.iNamChungTu,
	ctct.sXauNoiMa
	into #tempDuToanCapQuyTruoc
	FROM BH_CP_ChungTu_ChiTiet ctct
	INNER JOIN BH_CP_ChungTu ct ON ctct.iID_CP_ChungTu=ct.iID_CP_ChungTu
	where ct.iQuy<@quy 
	and ct.iID_LoaiCap=@iID_LoaiDanhMucChi
	and CAST(dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
	and ctct.iID_MaDonVi in (select * from splitstring(@AgencyId)) 
	and ct.iNamChungTu=@YearOfWork
	GROUP BY
			ctct.iID_MaDonVi,
			ct.iNamChungTu,
			ctct.sXauNoiMa
	

	---- lấy dữ liệu đã cấp
	--SELECT SUM(fTienDuToan) AS fTienDuToan,
	--	  SUM(fTienKeHoachCap) AS fTienKeHoachCap,
	--	  SUM(fTienDaCap) AS fTienDaCap,
 --         iID_MaDonVi,
 --         iID_MucLucNganSach
	--	  into #tblDataDaCapExist
	--FROM BH_CP_ChungTu_ChiTiet
	--WHERE iID_CP_ChungTu IN
 --      (
	--	SELECT iID_CP_ChungTu
 --       FROM BH_CP_ChungTu
 --       WHERE iNamChungTu = @YearOfWork
	--	  AND iID_LoaiCap = @iID_LoaiDanhMucChi
 --         AND CAST(dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
	--	  AND EXISTS(SELECT * FROM #tempAgency INTERSECT SELECT * FROM f_split(@AgencyId))
	--	  AND iID_CP_ChungTu=@VoucherId
	--	)
	--	AND iID_MaDonVi IN (SELECT * FROM #tempAgency)
	--GROUP BY iID_MaDonVi, iID_MucLucNganSach

	--SELECT sum(fTienDuToan) AS fTienDuToan, sum(fTienKeHoachCap) AS fTienKeHoachCap, sum(fTienDaCap) fTienDaCap, iID_MaDonVi, iID_MucLucNganSach into #tblDataDaCapDuToanExist FROM (
	--	SELECT * FROM #tblDataDaCapExist
	--	) data
	--GROUP BY iID_MaDonVi, iID_MucLucNganSach;
	----select * from  #tblMlnsRoot
	--SELECT mlns.iID_MLNS,
	--	   mlns.iID_MLNS_Cha,
	--	   mlns.sXauNoiMa,
	--	   fTienDuToan,
	--	   fTienDaCap,
	--	   fTienKeHoachCap,
	--	   isnull(mlns.iID_MaDonVi, daCapDuToan.iID_MaDonVi) as iID_MaDonVi
	--	   INTO #tblDaCapDuToanExist
	--FROM #tblMlnsRoot mlns
	--LEFT JOIN
	--  #tblDataDaCapDuToanExist daCapDuToan
	--ON mlns.iID_MLNS = daCapDuToan.iID_MucLucNganSach and ((mlns.iID_MaDonVi is not null and mlns.iID_MaDonVi = daCapDuToan.iID_MaDonVi) or mlns.iID_MaDonVi is null)

	--SELECT iID_MLNS, iID_MLNS_Cha, iID_MaDonVi, sum(fTienDuToan) AS fTienDuToan, sum(fTienDaCap) AS fTienDaCap, SUM(fTienKeHoachCap) as fTienKeHoachCap INTO #tblDaCapDuToanResultExist FROM (
	--	SELECT distinct T.iID_MLNS,  
	--		   T.iID_MLNS_Cha,
	--		   --isnull(T.iID_MaDonVi, T.iID_MaDonVi) as iID_MaDonVi,
	--		   T.iID_MaDonVi,
	--		   T.fTienDuToan,
	--		   T.fTienDaCap,
	--		   T.fTienKeHoachCap
	--	FROM #tblDaCapDuToanExist T 
	--	WHERE T.fTienDaCap <> 0 OR T.fTienDuToan <> 0 OR t.fTienKeHoachCap<>0) data
	--GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	--option (maxrecursion 0)



	-- Get data
	SELECT
		isnull(ctct.iID_CP_ChungTu_ChiTiet, @iD) AS iID_CP_ChungTu_ChiTiet,
		@VoucherId AS iID_CP_ChungTu,
		mlnsPhanBo.iID_MLNS AS iID_MucLucNganSach,
		mlnsPhanBo.iID_MLNS_Cha AS IdParent,
		mlnsPhanBo.sXauNoiMa AS sXauNoiMa,
		mlnsPhanBo.sLNS AS SLNS,
		mlnsPhanBo.sL AS SL,
		mlnsPhanBo.sK AS SK,
		mlnsPhanBo.sM AS SM,
		mlnsPhanBo.sTM AS STM,
		mlnsPhanBo.sTTM AS STTM,
		mlnsPhanBo.sNG AS SNG,
		mlnsPhanBo.sTNG AS STNG,
		mlnsPhanBo.sTNG1 AS STNG1,
		mlnsPhanBo.sTNG2 AS STNG2,
		mlnsPhanBo.sTNG3 AS STNG3,
		mlnsPhanBo.sMoTa AS SNoiDung,
		mlnsPhanBo.bHangCha As IsHangCha,
		mlnsPhanBo.sCPChiTietToi sDuToanChiTietToi,
		@YearOfWork AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(daCaCap.fTienDaCap, 0) as FTienDaCaQuyTruoc,
		isnull(daCaCap.fTienKeHoachCap, 0) as FTienKeHoachCapQuyTruoc,
		isnull(daCapDuToan.fTienDuToan, 0) as FTienDuToan,
		isnull(ctct.fTienKeHoachCap, 0) as FTienKeHoachCap,
		ctct.sGhiChu AS SGhiChu,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsRoot AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_CP_ChungTu_ChiTiet
			WHERE 
		 		iID_CP_ChungTu = @VoucherId
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_CP_ChungTu ct ON ctct.iID_CP_ChungTu = ct.iID_CP_ChungTu
	LEFT JOIN #tblPhanBoDuToan daCapDuToan
	on mlnsPhanBo.iID_MLNS = daCapDuToan.iID_MucLucNganSach and daCapDuToan.iID_MaDonVi = mlnsPhanBo.iID_MaDonVi 
	LEFT JOIN #tempDuToanCapQuyTruoc daCaCap on mlnsPhanBo.iID_MaDonVi=daCaCap.iID_MaDonVi and mlnsPhanBo.sXauNoiMa=daCaCap.sXauNoiMa
	where mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	order by mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	
END
;
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_muclucngansach_theodieuchinh]    Script Date: 6/17/2024 3:02:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_muclucngansach_theodieuchinh]
	 @namLamViec int,
	 @SLNS nvarchar(MAX)
AS
BEGIN 
	SET NOCOUNT ON;
	SELECT *
	FROM BH_DM_MucLucNganSach
	WHERE sLNS in( SELECT * FROM splitstring(@SLNS))
	  AND iNamLamViec = @NamLamViec
	  AND bHangChaDuToanDieuChinh is not null
	  AND iTrangThai=1
	 ORDER BY sXauNoiMa
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_nhanphanbo_dieuchinh_chi_tiet_clone]    Script Date: 6/17/2024 3:02:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_nhanphanbo_dieuchinh_chi_tiet_clone]
	@NamLamViec int,
	@IdDonVi nvarchar(100),
	@loaiDanhMucCapChi nvarchar(100),
	@SLNS nvarchar(100),
	@ngayChungTu date
AS
BEGIN
SELECT 
		
		ct.sXauNoiMa,
		ct.iID_MaDonVi,
		ct.iNamLamViec,
	
		Sum(ISNULL(ct.fTienTuChi, 0))as FTienTuChi
		into #temp1
	FROM
		(
			SELECT
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTC_DuToanChiTrenGiao bh
			JOIN 
				BH_DTC_DuToanChiTrenGiao_ChiTiet bhct ON bh.ID = bhct.iID_DTC_DuToanChiTrenGiao 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bhct.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bhct.iID_MaDonVi = @IdDonVi
				and bh.bIsKhoa = 1
				AND bh.sSoQuyetDinh IS NOT NULL
				AND bh.sSoQuyetDinh <> ''
				AND cast(bh.DngayChungTu as date) <= cast(@ngayChungTu as date)
				--AND bh.iID_LoaiDanhMucChi=@loaiDanhMucCapChi
				AND bh.iNamLamViec=@NamLamViec
		) ct
		Group by 
		ct.sXauNoiMa,
		ct.iID_MaDonVi,
		ct.iNamLamViec
		;

		SELECT dm.iID_MLNS_Cha as IdParent,
			   dm.iID_MLNS as iID_MucLucNganSach,
			   tbl.FTienTuChi as fTienTuChi,
			   dm.bHangCha as IsHangCha,
			   dm.sCPChiTietToi as SCPChiTietToi,
			   dm.sDuToanChiTietToi as SDuToanChiTietToi,
			   dm.sMoTa SNoiDung,
			   dm.sXauNoiMa
			FROM BH_DM_MucLucNganSach dm
			LEFT JOIN #temp1  tbl 
			on dm.sXauNoiMa=tbl.sXauNoiMa
			where dm.iNamLamViec=@NamLamViec 
			and dm.sLNS in (select * from splitstring(@SLNS))
			and dm.bHangChaDuToan is not null
			and dm.iTrangThai=1
			order by dm.sXauNoiMa

			drop table #temp1

END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_phanbo_dieuchinh_chi_tiet_clone]    Script Date: 6/17/2024 3:02:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_phanbo_dieuchinh_chi_tiet_clone]
	@NamLamViec int,
	@IdDonVi nvarchar(100),
	@loaiDanhMucCapChi nvarchar(100),
	@SLNS nvarchar(100),
	@ngayChungTu date
AS
BEGIN
SELECT 
		
		ct.sXauNoiMa,
		ct.iID_MaDonVi,
		ct.iNamLamViec,
	
		Sum(ISNULL(ct.fTienTuChi, 0))as FTienTuChi
		into #temp1
	FROM
		(
			SELECT
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTC_PhanBoDuToanChi bh
			JOIN 
				BH_DTC_PhanBoDuToanChi_ChiTiet bhct ON bh.ID = bhct.iID_DTC_PhanBoDuToanChi 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bhct.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bhct.iID_MaDonVi = @IdDonVi
				and bh.bIsKhoa = 1
				AND bh.sSoQuyetDinh IS NOT NULL
				AND bh.sSoQuyetDinh <> ''
				AND cast(bh.DngayChungTu as date) <= cast(@ngayChungTu as date)
				AND bh.iNamChungTu=@NamLamViec
		) ct
		Group by 
		ct.sXauNoiMa,
		ct.iID_MaDonVi,
		ct.iNamLamViec
		;

		SELECT dm.iID_MLNS_Cha as IdParent,
			   dm.iID_MLNS as iID_MLNS,
			   tbl.FTienTuChi as fTienTuChi,
			   dm.bHangCha as IsHangCha,
			   dm.sCPChiTietToi as SCPChiTietToi,
			   dm.sDuToanChiTietToi as SDuToanChiTietToi,
			   dm.sMoTa SNoiDung,
			   dm.sXauNoiMa
			FROM BH_DM_MucLucNganSach dm
			LEFT JOIN #temp1  tbl 
			on dm.sXauNoiMa=tbl.sXauNoiMa
			where dm.iNamLamViec=@NamLamViec 
			and dm.sLNS in (select * from splitstring(@SLNS))
			and dm.bHangChaDuToan is not null
			and dm.iTrangThai=1
			order by dm.sXauNoiMa

			drop table #temp1

END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]    Script Date: 6/17/2024 3:02:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]
	@NamLamViec int,
	@IdDonVi nvarchar(max),
	@IDLoaiChi uniqueidentifier,
	@SLNS nvarchar(max),
	@ngayChungTu date, 
	@Dvt int,
	@Loai int
AS
BEGIN

	SELECT ML.*
			into #tblml
	FROM BH_DM_MucLucNganSach ML
	WHERE ML.sLNS IN  (SELECT * FROM splitstring(@SLNS))
			AND ML.iNamLamViec=@NamLamViec
			AND ML.bHangCha IS NOT NULL
			AND Ml.iTrangThai=1
	ORDER BY sXauNoiMa

				
	SELECT 
		distinct
		ct.dNgayChungTu
		, ct.iID_MaDonVi as  iID_MaDonVi
		into #tempTblNgayChungTuDonVi
	FROM BH_DTC_DieuChinhDuToanChi ct
	left join  BH_DTC_DieuChinhDuToanChi_ChiTiet ctct on ct.iID_BH_DTC=ctct.iID_BH_DTC
	where ctct.iNamLamViec=@NamLamViec
		and ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
		and ct.iID_LoaiCap=@IDLoaiChi

	--- Chung tu thuong
	IF (@Loai=0)
	SELECT 
		ml.iID_MLNS as iID_MucLucNganSach,
		ml.sM,
		ml.SLNS,
		ml.sTM,
		ml.sTTM,
		ml.sMoTa as sNoiDung,
		ml.sXauNoiMa,
		ml.iID_MLNS_Cha as IdParent,
		ml.bHangChaDuToanDieuChinh IsHangCha,
		ml.sDuToanChiTietToi,
		ct.iNamLamViec,
		ISNULL((dt.fTienTuChi),0)/ @Dvt fTienDuToanDuocGiao,
		ISNULL(SUM(ct.fTienThucHien06ThangDauNam),0)/ @Dvt fTienThucHien06ThangDauNam,
		ISNULL(SUM(ct.fTienUocThucHien06ThangCuoiNam),0)/ @Dvt fTienUocThucHien06ThangCuoiNam,
		ISNULL(SUM(ct.fTienUocThucHienCaNam),0) /@Dvt fTienUocThucHienCaNam,
		ISNULL(SUM(ct.fTienSoSanhTang),0)/ @Dvt fTienSoSanhTang,
		ISNULL(SUM(dt.fTienTuChi) - (SUM(ct.fTienUocThucHienCaNam)),0)/ @Dvt fTienSoSanhGiam

	FROM
	#tblml ml 
	LEFT JOIN
		(
			SELECT
				--bh.iID_MaDonVi,
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTC_DieuChinhDuToanChi bh
			JOIN 
				BH_DTC_DieuChinhDuToanChi_ChiTiet bhct ON bh.iID_BH_DTC = bhct.iID_BH_DTC 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND	bh.iNamLamViec=@NamLamViec
				AND BH.iID_LoaiCap=@IDLoaiChi
				--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
		) ct
		ON ct.iID_MucLucNganSach=ml.iID_MLNS
		LEFT JOIN (
			SELECT ctct.sXauNoiMa
					, SUM(ctct.fTienTuChi)  fTienTuChi
			FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct 
			JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID 
			LEFT JOIN #tempTblNgayChungTuDonVi ngayChungTuDonVi ON  ctct.iID_MaDonVi=ngayChungTuDonVi.iID_MaDonVi
				WHERE ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND BIsKhoa = 1
				AND ct.iNamChungTu = @NamLamViec
				And cast(ct.DngayChungTu as date) <= cast(ngayChungTuDonVi.dNgayChungTu as date)
				GROUP BY ctct.sXauNoiMa) dt ON dt.sXauNoiMa=ml.sXauNoiMa 
		WHERE ml.iNamLamViec=@NamLamViec
		GROUP BY 
		ml.iID_MLNS,
		ml.iID_MLNS_Cha,
		ml.bHangChaDuToanDieuChinh,
		ml.SLNS,
		ml.sM,
		ml.sTM,
		ml.sTTM,
		ml.sMoTa,
		ml.sXauNoiMa,
		ct.iNamLamViec,
		ml.sDuToanChiTietToi,
		dt.fTienTuChi
		ORDER BY sXauNoiMa
		--- Chung tu tong hop
		ELSE 
		SELECT 
		ml.iID_MLNS as iID_MucLucNganSach,
		ml.sM,
		ml.SLNS,
		ml.sTM,
		ml.sTTM,
		ml.sMoTa as sNoiDung,
		ml.sXauNoiMa,
		ml.iID_MLNS_Cha as IdParent,
		ml.bHangChaDuToanDieuChinh IsHangCha,
		ml.sDuToanChiTietToi,
		ct.iNamLamViec,
		ISNULL((dt.fTienTuChi),0)/ @Dvt fTienDuToanDuocGiao,
		ISNULL(SUM(ct.fTienThucHien06ThangDauNam),0)/ @Dvt fTienThucHien06ThangDauNam,
		ISNULL(SUM(ct.fTienUocThucHien06ThangCuoiNam),0)/ @Dvt fTienUocThucHien06ThangCuoiNam,
		ISNULL(SUM(ct.fTienUocThucHienCaNam),0) /@Dvt fTienUocThucHienCaNam,
		ISNULL(SUM(ct.fTienSoSanhTang),0)/ @Dvt fTienSoSanhTang,
		ISNULL(SUM(dt.fTienTuChi) - (SUM(ct.fTienUocThucHienCaNam)),0)/ @Dvt fTienSoSanhGiam

	FROM
	#tblml ml 
	LEFT JOIN
		(
			SELECT
				--bh.iID_MaDonVi,
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTC_DieuChinhDuToanChi bh
			JOIN 
				BH_DTC_DieuChinhDuToanChi_ChiTiet bhct ON bh.iID_BH_DTC = bhct.iID_BH_DTC 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND	bh.iNamLamViec=@NamLamViec
				AND BH.iID_LoaiCap=@IDLoaiChi
				--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
		) ct
		ON ct.iID_MucLucNganSach=ml.iID_MLNS
		LEFT JOIN (
					SELECT ctct.sXauNoiMa,
							SUM(ctct.fTienTuChi) fTienTuChi
							FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct 
							JOIN BH_DTC_DuToanChiTrenGiao ct ON ctct.iID_DTC_DuToanChiTrenGiao = ct.ID 
							LEFT JOIN #tempTblNgayChungTuDonVi ngayChungTuDonVi ON  ctct.iID_MaDonVi=ngayChungTuDonVi.iID_MaDonVi
							WHERE ct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
							AND BIsKhoa = 1
							And cast(ct.DngayChungTu as date) <= cast(ngayChungTuDonVi.dNgayChungTu as date)
							--AND ct.iID_LoaiDanhMucChi=@IDLoaiChi
							AND ct.iNamLamViec = @NamLamViec
							GROUP BY ctct.sXauNoiMa) dt ON dt.sXauNoiMa = ml.sXauNoiMa 
		WHERE ml.iNamLamViec=@NamLamViec
		GROUP BY 
		ml.iID_MLNS,
		ml.iID_MLNS_Cha,
		ml.bHangChaDuToanDieuChinh,
		ml.SLNS,
		ml.sM,
		ml.sTM,
		ml.sTTM,
		ml.sMoTa,
		ml.sXauNoiMa,
		ct.iNamLamViec,
		ml.sDuToanChiTietToi,
		dt.fTienTuChi
		ORDER BY sXauNoiMa;

		DROP TABLE #tblml
		drop table #tempTblNgayChungTuDonVi

END
;
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_tonghop_chitiet_donvi]    Script Date: 6/17/2024 3:02:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_tonghop_chitiet_donvi]
	@NamLamViec int,
	@IdDonVi nvarchar(100),
	@IDLoaiChi uniqueidentifier,
	@SLNS nvarchar(max),
	@ngayChungTu date, 
	@Dvt int,
	@Loai int
AS
BEGIN

	SELECT ML.*
			into #tblml
	FROM BH_DM_MucLucNganSach ML
	WHERE ML.sLNS IN  (SELECT * FROM splitstring(@SLNS))
			AND ML.iNamLamViec=@NamLamViec
			AND ML.bHangChaDuToanDieuChinh is not null
			AND ML.iTrangThai=1
	ORDER BY sXauNoiMa

	SELECT 
						chitiet.fTienDuToanDuocGiao, 
						chitiet.fTienSoSanhGiam, chitiet.fTienSoSanhTang, 
						dtct_by_donvi.fTienThucHien06ThangDauNam as fTienThucHien06ThangDauNam, 
						dtct_by_donvi.fTienUocThucHien06ThangCuoiNam as fTienUocThucHien06ThangCuoiNam, 
						dtct_by_donvi.fTienUocThucHienCaNam as fTienUocThucHienCaNam,
						chitiet.iID_BH_DTC,
						chitiet.iID_MucLucNganSach,
						dtct_by_donvi.sNoiMa,
						chitiet.sXauNoiMa,
						chitiet.iNamLamViec
					into #bh_dtc_dieuchinh_chitiet
					FROM BH_DTC_DieuChinhDuToanChi_ChiTiet chitiet
					LEFT JOIN (
						SELECT sTM, 
							iID_MaDonVi, 
							sNoiMa,
							sM,
							SUM(fTienThucHien06ThangDauNam) as fTienThucHien06ThangDauNam, 
							SUM(fTienUocThucHien06ThangCuoiNam) as fTienUocThucHien06ThangCuoiNam,
							SUM(fTienUocThucHienCaNam) as fTienUocThucHienCaNam 
						FROM (
								SELECT *, 
									CASE WHEN sXauNoiMa LIKE '9010001%' THEN '9010001' 
									WHEN sXauNoiMa LIKE '9010002%' THEN '9010002' END as sNoiMa
								FROM BH_DTC_DieuChinhDuToanChi_ChiTiet 
							) BH_DTC_DieuChinhDuToanChi_ChiTiet
						WHERE iNamLamViec=@NamLamViec
						AND iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
						AND sTM != ''
						GROUP BY sTM, iID_MaDonVi, sNoiMa, sM
					) dtct_by_donvi ON chitiet.sTM = '' 
						AND chitiet.sM = dtct_by_donvi.sM 
						AND chitiet.iID_MaDonVi = dtct_by_donvi.iID_MaDonVi
						AND chitiet.sXauNoiMa LIKE (dtct_by_donvi.sNoiMa + '%')

					ORDER BY chitiet.sXauNoiMa

		SELECT 
			distinct
			ct.dNgayChungTu
			, ct.iID_MaDonVi as  iID_MaDonVi
			into #tempTblNgayChungTuDonVi
		FROM BH_DTC_DieuChinhDuToanChi ct
		left join  BH_DTC_DieuChinhDuToanChi_ChiTiet ctct on ct.iID_BH_DTC=ctct.iID_BH_DTC
		where ctct.iNamLamViec=@NamLamViec
			and ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
			and ct.iID_LoaiCap=@IDLoaiChi
IF @Loai=0
	SELECT 
		--ct.iID_MaDonVi,
		ct.sTenDonVi as STenDonVi,
		ml.iID_MLNS as iID_MucLucNganSach,
		ml.sM,
		ml.SLNS,
		ml.sTM,
		ml.sTTM,
		ml.sMoTa as sNoiDung,
		ml.sXauNoiMa,
		ml.iID_MLNS_Cha as IdParent,
		ml.bHangChaDuToanDieuChinh bHangCha,
		--ml.bHangChaDuToanDieuChinh IsHangCha,
		ct.iNamLamViec,
		ISNULL((dt.fTienTuChi),0)/ @Dvt fTienDuToanDuocGiao,
		ISNULL(SUM(ct.fTienThucHien06ThangDauNam),0)/ @Dvt fTienThucHien06ThangDauNam,
		ISNULL(SUM(ct.fTienUocThucHien06ThangCuoiNam),0)/ @Dvt fTienUocThucHien06ThangCuoiNam,
		ISNULL(SUM(ct.fTienUocThucHienCaNam),0) /@Dvt fTienUocThucHienCaNam,
		ISNULL(SUM(ct.fTienSoSanhTang),0)/ @Dvt fTienSoSanhTang,
		ISNULL(SUM(ct.fTienSoSanhGiam),0)/ @Dvt fTienSoSanhGiam

	FROM
	#tblml ml 
	LEFT JOIN
		(
			SELECT
				bh.sMoTa,
				CASE 
					WHEN (SELECT COUNT(*) FROM splitstring(@IdDonVi)) > 1 THEN ''
					ELSE ddv.sTenDonVi
				END as sTenDonVi,
				--ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTC_DieuChinhDuToanChi bh
			JOIN 
				#bh_dtc_dieuchinh_chitiet bhct ON bh.iID_BH_DTC = bhct.iID_BH_DTC 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND	bh.iNamLamViec=@NamLamViec
				AND BH.iID_LoaiCap=@IDLoaiChi
				--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
		) ct
		ON ct.iID_MucLucNganSach=ml.iID_MLNS
		LEFT JOIN (
			SELECT ctct.sXauNoiMa
					, SUM(ctct.fTienTuChi)  fTienTuChi
			
			FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct 
			JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID 
			LEFT JOIN #tempTblNgayChungTuDonVi ngayChungTuDonVi ON  ctct.iID_MaDonVi=ngayChungTuDonVi.iID_MaDonVi
				WHERE ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND BIsKhoa = 1
				AND ct.iNamChungTu = @NamLamViec
				And cast(ct.DngayChungTu as date) <= cast(ngayChungTuDonVi.dNgayChungTu as date) 
				GROUP BY ctct.sXauNoiMa) dt ON dt.sXauNoiMa=ml.sXauNoiMa 
		WHERE ml.iNamLamViec=@NamLamViec
		--and ml.bHangChaDuToanDieuChinh = 1
		and ml.sTM = ''
		GROUP BY 
		ml.iID_MLNS,
		ml.iID_MLNS_Cha,
		ml.bHangChaDuToanDieuChinh,
		ml.SLNS,
		ml.sM,
		ml.sTM,
		ml.sTTM,
		ml.sMoTa,
		ml.sXauNoiMa,
		ct.iNamLamViec,
		ct.sTenDonVi,
		dt.fTienTuChi
		--ct.iID_MaDonVi,
		ORDER BY sXauNoiMa;
ELSE
SELECT 
		--ct.iID_MaDonVi,
		ct.sTenDonVi as STenDonVi,
		ml.iID_MLNS as iID_MucLucNganSach,
		ml.sM,
		ml.SLNS,
		ml.sTM,
		ml.sTTM,
		ml.sMoTa as sNoiDung,
		ml.sXauNoiMa,
		ml.iID_MLNS_Cha as IdParent,
		ml.bHangChaDuToanDieuChinh bHangCha,
		--ml.bHangChaDuToanDieuChinh IsHangCha,
		ct.iNamLamViec,
		ISNULL((dt.fTienTuChi),0)/ @Dvt fTienDuToanDuocGiao,
		ISNULL(SUM(ct.fTienThucHien06ThangDauNam),0)/ @Dvt fTienThucHien06ThangDauNam,
		ISNULL(SUM(ct.fTienUocThucHien06ThangCuoiNam),0)/ @Dvt fTienUocThucHien06ThangCuoiNam,
		ISNULL(SUM(ct.fTienUocThucHienCaNam),0) /@Dvt fTienUocThucHienCaNam,
		ISNULL(SUM(ct.fTienSoSanhTang),0)/ @Dvt fTienSoSanhTang,
		ISNULL(SUM(ct.fTienSoSanhGiam),0)/ @Dvt fTienSoSanhGiam

	FROM
	#tblml ml 
	LEFT JOIN
		(
			SELECT
				bh.sMoTa,
				CASE 
					WHEN (SELECT COUNT(*) FROM splitstring(@IdDonVi)) > 1 THEN ''
					ELSE ddv.sTenDonVi
				END as sTenDonVi,
				--ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTC_DieuChinhDuToanChi bh
			JOIN 
				#bh_dtc_dieuchinh_chitiet bhct ON bh.iID_BH_DTC = bhct.iID_BH_DTC 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND	bh.iNamLamViec=@NamLamViec
				AND BH.iID_LoaiCap=@IDLoaiChi
				--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
		) ct
		ON ct.iID_MucLucNganSach=ml.iID_MLNS
		LEFT JOIN (
			SELECT ctct.sXauNoiMa
					, SUM(ctct.fTienTuChi)  fTienTuChi
			FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct 
			JOIN BH_DTC_DuToanChiTrenGiao ct ON ctct.iID_DTC_DuToanChiTrenGiao = ct.ID 
			LEFT JOIN #tempTblNgayChungTuDonVi ngayChungTuDonVi ON  ctct.iID_MaDonVi=ngayChungTuDonVi.iID_MaDonVi
				WHERE ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND BIsKhoa = 1
				AND ct.iNamLamViec = @NamLamViec
				And cast(ct.DngayChungTu as date) <= cast(ngayChungTuDonVi.dNgayChungTu as date) 
				GROUP BY ctct.sXauNoiMa) dt ON dt.sXauNoiMa=ml.sXauNoiMa  
		WHERE ml.iNamLamViec=@NamLamViec
		--and ml.bHangChaDuToanDieuChinh = 1
		and ml.sTM = ''
		GROUP BY 
		ml.iID_MLNS,
		ml.iID_MLNS_Cha,
		ml.bHangChaDuToanDieuChinh,
		ml.SLNS,
		ml.sM,
		ml.sTM,
		ml.sTTM,
		ml.sMoTa,
		ml.sXauNoiMa,
		ct.iNamLamViec,
		ct.sTenDonVi,
		dt.fTienTuChi
		--ct.iID_MaDonVi,
		ORDER BY sXauNoiMa;
		
		DROP TABLE #tblml		
		DROP TABLE #bh_dtc_dieuchinh_chitiet
		DROP TABLE #tempTblNgayChungTuDonVi
END
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_tonghop_KQPL_KCBQY_KCBTS_KPK_chitiet_donvi]    Script Date: 6/17/2024 3:02:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_tonghop_KQPL_KCBQY_KCBTS_KPK_chitiet_donvi]
	@NamLamViec int,
	@IdDonVi nvarchar(200),
	@IDLoaiChi uniqueidentifier,
	@SLNS nvarchar(max),
	@ngayChungTu date, 
	@Dvt int
AS
BEGIN

	SELECT 
		distinct
		ct.dNgayChungTu
		, ct.iID_MaDonVi as  iID_MaDonVi
		into #tempTblNgayChungTuDonVi
	FROM BH_DTC_DieuChinhDuToanChi ct
	left join  BH_DTC_DieuChinhDuToanChi_ChiTiet ctct on ct.iID_BH_DTC=ctct.iID_BH_DTC
	where ctct.iNamLamViec=@NamLamViec
		and ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
		and ct.iID_LoaiCap=@IDLoaiChi

	SELECT 
			B.iID_MLNS as iID_MucLucNganSach,
			B.iID_MLNS_Cha as IdParent,
			B.bHangCha as IsHangCha,
			B.sLNS,
			B.sL,
			B.sM,
			B.sTM,
			b.sMoTa sNoiDung,
			B.sXauNoiMa,
			B.sDuToanChiTietToi,
			A.fTienDuToanDuocGiao / @Dvt fTienDuToanDuocGiao,
			A.fTienSoSanhGiam/ @Dvt fTienSoSanhGiam,
			A.fTienSoSanhTang/ @Dvt fTienSoSanhTang,
			A.fTienThucHien06ThangDauNam/ @Dvt fTienThucHien06ThangDauNam,
			A.fTienUocThucHien06ThangCuoiNam / @Dvt fTienUocThucHien06ThangCuoiNam,
			A.fTienUocThucHienCaNam / @Dvt fTienUocThucHienCaNam,
			A.iID_MaDonVi,
			1 TYPE
			into #temp1
		FROM BH_DM_MucLucNganSach B
		LEFT JOIN (
			SELECT * FROM BH_DTC_DieuChinhDuToanChi_ChiTiet
			WHERE iID_BH_DTC IN 
								(SELECT iID_BH_DTC
									FROM BH_DTC_DieuChinhDuToanChi 
									WHERE iID_MaDonVi in (select * from splitstring(@IdDonVi))
									AND  iID_LoaiCap=@IDLoaiChi
									AND iNamLamViec=@NamLamViec
										)
			) A ON A.iID_MucLucNganSach=B.iID_MLNS
			
			LEFT JOIN (
			SELECT ctct.sXauNoiMa
					, SUM(ctct.fTienTuChi)  fTienTuChi
					, ctct.iID_MaDonVi
			FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct 
			JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID 
			LEFT JOIN #tempTblNgayChungTuDonVi ngayChungTuDonVi ON  ctct.iID_MaDonVi=ngayChungTuDonVi.iID_MaDonVi
				WHERE ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND BIsKhoa = 1
				AND ct.iNamChungTu = @NamLamViec
				And cast(ct.DngayChungTu as date) <= cast(ngayChungTuDonVi.dNgayChungTu as date) 
				GROUP BY ctct.sXauNoiMa,ctct.iID_MaDonVi) dt ON dt.sXauNoiMa=A.sXauNoiMa  and dt.iID_MaDonVi=A.iID_MaDonVi
			WHERE B.iNamLamViec=@NamLamViec
			AND B.iTrangThai=1
			AND (A.fTienDuToanDuocGiao<>0 
			OR A.fTienSoSanhGiam<>0 
			OR A.fTienSoSanhTang<>0 
			OR A.fTienThucHien06ThangDauNam<>0
			OR A.fTienUocThucHien06ThangCuoiNam<>0
			OR A.fTienUocThucHienCaNam<>0)

	SELECT 
		    tbl1.iID_MucLucNganSach,
			tbl1.IdParent,
			tbl1.IsHangCha ,
			tbl1.sLNS,
			tbl1.sL,
			tbl1.sM,
			tbl1.sTM,
			tbl1.sDuToanChiTietToi,
			'          '+ dv.sTenDonVi sNoiDung,
			tbl1.sXauNoiMa,
			tbl1.fTienDuToanDuocGiao,
			tbl1.fTienSoSanhGiam ,
			tbl1.fTienSoSanhTang ,
			tbl1.fTienThucHien06ThangDauNam ,
			tbl1.fTienUocThucHien06ThangCuoiNam ,
			tbl1.fTienUocThucHienCaNam ,
			tbl1.iID_MaDonVi,
			tbl1.TYPE
			INTO #tempChillMlns
		FROM #temp1 tbl1
		LEFT JOIN  DonVi dv ON tbl1.iID_MaDonVi=dv.iID_MaDonVi
		WHERE dv.iNamLamViec=@NamLamViec
		AND tbl1.sDuToanChiTietToi is null
		order by sXauNoiMa

		SELECT 
			B.iID_MLNS as iID_MucLucNganSach,
			B.iID_MLNS_Cha as IdParent,
			B.bHangCha as IsHangCha,
			B.sLNS,
			B.sL,
			B.sM,
			B.sTM,
			B.sDuToanChiTietToi,
			b.sMoTa sNoiDung,
			B.sXauNoiMa,
			dt.fTienTuChi / @Dvt fTienDuToanDuocGiao,
			Sum(A.fTienSoSanhGiam) /@Dvt fTienSoSanhGiam,
			Sum(A.fTienSoSanhTang) / @Dvt fTienSoSanhTang,
			Sum(A.fTienThucHien06ThangDauNam) / @Dvt fTienThucHien06ThangDauNam,
			Sum(A.fTienUocThucHien06ThangCuoiNam) / @Dvt fTienUocThucHien06ThangCuoiNam,
			Sum(fTienUocThucHienCaNam) / @Dvt fTienUocThucHienCaNam,
			null iID_MaDonVi,
			0 TYPE
			into #tblBhMlns
		FROM BH_DM_MucLucNganSach B
		LEFT JOIN (
			SELECT * FROM BH_DTC_DieuChinhDuToanChi_ChiTiet
			WHERE iID_BH_DTC IN 
								(SELECT iID_BH_DTC
									FROM BH_DTC_DieuChinhDuToanChi 
									WHERE iID_MaDonVi in (select * from splitstring(@IdDonVi))
									AND  iID_LoaiCap=@IDLoaiChi
									AND iNamLamViec=@NamLamViec
										)
			) A ON A.iID_MucLucNganSach=B.iID_MLNS
			LEFT JOIN (
			SELECT ctct.sXauNoiMa
					, SUM(ctct.fTienTuChi)  fTienTuChi
			FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct 
			JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID 
			LEFT JOIN #tempTblNgayChungTuDonVi ngayChungTuDonVi ON  ctct.iID_MaDonVi=ngayChungTuDonVi.iID_MaDonVi
				WHERE ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND BIsKhoa = 1
				AND ct.iNamChungTu = @NamLamViec
				And cast(ct.DngayChungTu as date) <= cast(ngayChungTuDonVi.dNgayChungTu as date) 
				GROUP BY ctct.sXauNoiMa) dt ON dt.sXauNoiMa=B.sXauNoiMa 
			WHERE B.iNamLamViec=@NamLamViec
			AND B.sLNS IN (SELECT * FROM splitstring(@SLNS))
			AND B.iTrangThai=1
			group by B.iID_MLNS,
				B.sLNS,
				B.sMoTa,
				B.iID_MLNS_Cha,
				B.sL,
				B.sM,
				B.sTM,
				B.sDuToanChiTietToi,
				B.sXauNoiMa,
				B.bHangCha,
				dt.fTienTuChi
		order by sXauNoiMa

	-- map bảng 
		SELECT * INTO #tblMLNS FROM (
			SELECT *
			FROM #tblBhMlns tbl
			UNION ALL
			SELECT *
			FROM #tempChillMlns 
		) mlns

		select * from #tblMLNS
		order by sXauNoiMa, TYPE 


		DROP TABLE #tblMLNS	
		DROP TABLE #tblBhMlns	
		DROP TABLE #tempChillMlns	
		DROP TABLE #temp1
END
;
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkp_ql_thongtriloai2_bh]    Script Date: 6/17/2024 3:02:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chung tu theo don vi và theo lns
CREATE PROCEDURE [dbo].[sp_rpt_qtc_qkp_ql_thongtriloai2_bh]
	 @NamLamViec int,
	 @iQuy nvarchar(100),
	 @IdDonVi nvarchar(max),
	 @LNS nvarchar(max),
	 @UserName nvarchar(100),
	 @LoaiTongHop int,
	 @Dvt int
AS
BEGIN 
	SET NOCOUNT ON;
	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
			into #tblMlnsByPhanCap
	FROM BH_DM_MucLucNganSach 
	WHERE 
		iNamLamViec = @NamLamViec 
		AND iTrangThai = 1
		--AND (
		--	(@PhanCap = 'M' AND (sTM IS NULL OR sTM = ''))
		--	OR (@PhanCap = 'TM' AND (sTTM IS NULL OR sTTM = ''))
		--	OR (@PhanCap = 'NG' AND (sTNG IS NULL OR sTNG = ''))
		--	)
		AND sLNS IN (SELECT * FROM f_split(@LNS))
		--(
		--			SELECT DISTINCT VALUE
		--			FROM 
		--			(
		--				SELECT 
		--					CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1, 
		--					CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3, 
		--					CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5, 
		--					CAST(sLNS AS nvarchar(10)) LNS 
		--				FROM
		--					NS_NguoiDung_LNS 
		--				WHERE 
		--					sMaNguoiDung = @UserName
		--					AND INamLamViec = @NamLamViec
		--					AND sLNS IN (SELECT * FROM f_split(@LNS))
		--			) LNS
		--			UNPIVOT
		--			(
		--				value
		--				FOR col in (LNS1, LNS3, LNS5, LNS)
		--			) un) 

IF @LoaiTongHop=1
BEGIN
SELECT 
		mlns.iID_MLNS as IdMlns,
		mlns.iID_MLNS_Cha as IdMlnsCha,
		mlns.sXauNoiMa as XauNoiMa,
		mlns.sLNS as SLNS,
		mlns.sL as SL,
		mlns.sK as SK,
		mlns.sM as SM,
		mlns.sTM as STM,
		mlns.sTTM as STTM,
		mlns.sNG as SNG,
		mlns.sTNG as STNG,
		mlns.sMoTa as SMoTa,
		mlns.bHangCha as IsHangCha,
		ISNULL(ctct.fTienDeNghiQuyetToanQuyNay, 0) / @Dvt as FTienDeNghiQuyetToanQuyNay 
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(SELECT * FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet 
				WHERE iID_QTC_Quy_KinhPhiQuanLy in
					( SELECT ID_QTC_Quy_KinhPhiQuanLy FROM BH_QTC_Quy_KinhPhiQuanLy
								WHERE iNamChungTu=@NamLamViec
								AND iQuyChungTu=@iQuy
								--AND bIsKhoa=1
								AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
								)) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
	Order by mlns.sXauNoiMa
END
ELSE
BEGIN
SELECT 
		mlns.iID_MLNS as IdMlns,
		mlns.iID_MLNS_Cha as IdMlnsCha,
		mlns.sXauNoiMa as XauNoiMa,
		mlns.sLNS as SLNS,
		mlns.sL as SL,
		mlns.sK as SK,
		mlns.sM as SM,
		mlns.sTM as STM,
		mlns.sTTM as STTM,
		mlns.sNG as SNG,
		mlns.sTNG as STNG,
		mlns.sMoTa as SMoTa,
		mlns.bHangCha as IsHangCha,
		ISNULL(ctct.fTienXacNhanQuyetToanQuyNay, 0) / @Dvt as FTienXacNhanQuyetToanQuyNay 
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(SELECT * FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet 
				WHERE iID_QTC_Quy_KinhPhiQuanLy in
					( SELECT ID_QTC_Quy_KinhPhiQuanLy FROM BH_QTC_Quy_KinhPhiQuanLy
								WHERE iNamChungTu=@NamLamViec
								AND iQuyChungTu=@iQuy
								AND iLoaiTongHop=@LoaiTongHop
								AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
								)) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
	Order by mlns.sXauNoiMa
END
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_create_quyet_toan_chinambhxh_chitiet_theo4quy]    Script Date: 6/19/2024 10:11:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_create_quyet_toan_chinambhxh_chitiet_theo4quy]
@IdChungTu uniqueidentifier,
@IdMaDonVi nvarchar(50),
@INamLamViec int,
@User  nvarchar(50),
@IsTongHop bit
as
begin
	insert into BH_QTC_Nam_CheDoBHXH_ChiTiet (	
			ID_QTC_Nam_CheDoBHXH_ChiTiet,
			iID_QTC_Nam_CheDoBHXH, 
			iID_MucLucNganSach, 
			sLoaiTroCap,
			sXauNoiMa,
			iID_MaDonVi,
			iNamLamViec,
			dNgaySua,
			dNgayTao,
			sNguoiSua,
			sNguoiTao,
			fTienDuToanDuyet,
			iSoSQ_ThucChi,
			fTienSQ_ThucChi,
			iSoQNCN_ThucChi,
			fTienQNCN_ThucChi,
			iSoCNVCQP_ThucChi,
			fTienCNVCQP_ThucChi,
			iSoHSQBS_ThucChi,
			fTienHSQBS_ThucChi,
			iSoLDHD_ThucChi,
			fTienLDHD_ThucChi,
			iTongSo_ThucChi,
			fTongTien_ThucChi
			)
	select 
			NEWID(),
			@IdChungTu,
			tb_qtcy.iID_MucLucNganSach,
			tb_qtcy.sLoaiTroCap,
			tb_qtcy.sXauNoiMa,
			tb_qtcy.iIDMaDonVi,
			tb_qtcy.iNamLamViec,
			null,
			null,
			null,
			@User,
			tb_qtcy.fTienDuToanDuyet,
			tb_qtcy.iSoSQ_ThucChi,
			tb_qtcy.fTienSQ_ThucChi,
			tb_qtcy.iSoQNCN_ThucChi,
			tb_qtcy.fTienQNCN_ThucChi,
			tb_qtcy.iSoCNVCQP_ThucChi,
			tb_qtcy.fTienCNVCQP_ThucChi,
			tb_qtcy.iSoHSQBS_ThucChi,
			tb_qtcy.fTienHSQBS_ThucChi,
			tb_qtcy.iSoLDHD_ThucChi,
			tb_qtcy.fTienLDHD_ThucChi,
			tb_qtcy.iTongSo_ThucChi,
			tb_qtcy.fTongTien_ThucChi

	from 
	(
		select 
				ctqt_quy_chitiet.iID_MucLucNganSach, 
				ctqt_quy_chitiet.sLoaiTroCap,
				ctqt_quy_chitiet.sXauNoiMa,
				ctqt_quy_chitiet.iNamLamViec,
				ctqt_quy_chitiet.iIDMaDonVi,
				null as fTienDuToanDuyet,
				SUM(ctqt_quy_chitiet.iSoSQ_DeNghi) as iSoSQ_ThucChi,
				SUM(ctqt_quy_chitiet.fTienSQ_DeNghi) as fTienSQ_ThucChi,
				SUM(ctqt_quy_chitiet.iSoQNCN_DeNghi) as iSoQNCN_ThucChi,
				SUM(ctqt_quy_chitiet.fTienQNCN_DeNghi) as fTienQNCN_ThucChi,
				SUM(ctqt_quy_chitiet.iSoCNVCQP_DeNghi) as iSoCNVCQP_ThucChi,
				SUM(ctqt_quy_chitiet.fTienCNVCQP_DeNghi) as fTienCNVCQP_ThucChi,
				SUM(ctqt_quy_chitiet.iSoHSQBS_DeNghi) as iSoHSQBS_ThucChi,
				SUM(ctqt_quy_chitiet.fTienHSQBS_DeNghi) as fTienHSQBS_ThucChi,
				SUM(ctqt_quy_chitiet.iSoLDHD_DeNghi) as iSoLDHD_ThucChi,
				SUM(ctqt_quy_chitiet.fTienLDHD_DeNghi) as fTienLDHD_ThucChi,
				SUM(ctqt_quy_chitiet.iTongSo_DeNghi) as iTongSo_ThucChi,
				SUM(ctqt_quy_chitiet.fTongTien_DeNghi) as fTongTien_ThucChi

		from BH_QTC_Quy_CheDoBHXH as ctqt_quy
		inner join BH_QTC_Quy_CheDoBHXH_ChiTiet as ctqt_quy_chitiet on ctqt_quy.ID_QTC_Quy_CheDoBHXH = ctqt_quy_chitiet.iID_QTC_Quy_CheDoBHXH
		where ctqt_quy.iID_MaDonVi = @IdMaDonVi and ctqt_quy.iNamChungTu = @INamLamViec
				and ( ctqt_quy_chitiet.iTongSo_DeNghi >0
				or ctqt_quy_chitiet.fTongTien_DeNghi>0)
		--and ((@IsTongHop = 0 and ctqt_quy.bDaTongHop = 0 and ctqt_quy.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  ctqt_quy.sDSSoChungTuTongHop is not null))
		group by ctqt_quy_chitiet.iID_MucLucNganSach, ctqt_quy_chitiet.sLoaiTroCap
				,ctqt_quy_chitiet.sXauNoiMa,
				ctqt_quy_chitiet.iNamLamViec,
				ctqt_quy_chitiet.iIDMaDonVi) as tb_qtcy

end
;
;
GO
