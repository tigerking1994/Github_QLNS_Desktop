/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiao_index]    Script Date: 10/24/2023 1:43:57 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_nhandutoanchitrengiao_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiao_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_phulucquyettoannam_KCB]    Script Date: 10/24/2023 1:43:57 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_phulucquyettoannam_KCB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_phulucquyettoannam_KCB]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_danhsachquyettoannamKCB_index]    Script Date: 10/24/2023 1:43:57 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_danhsachquyettoannamKCB_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_danhsachquyettoannamKCB_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]    Script Date: 10/24/2023 1:43:57 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]    Script Date: 10/24/2023 1:43:57 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]    Script Date: 10/24/2023 1:43:57 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcnKCB_update_total]    Script Date: 10/24/2023 1:43:57 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcnKCB_update_total]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcnKCB_update_total]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcnKCB_create_data_summary]    Script Date: 10/24/2023 1:43:57 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcnKCB_create_data_summary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcnKCB_create_data_summary]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_export_tonghopcaptamung_kcbbhyt]    Script Date: 10/24/2023 1:43:57 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_export_tonghopcaptamung_kcbbhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_export_tonghopcaptamung_kcbbhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_export_thongtricaptamung_kcbbhyt]    Script Date: 10/24/2023 1:43:57 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_export_thongtricaptamung_kcbbhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_export_thongtricaptamung_kcbbhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_export_kehoachcaptamung_kcbbhyt]    Script Date: 10/24/2023 1:43:57 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_export_kehoachcaptamung_kcbbhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_export_kehoachcaptamung_kcbbhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dttm_danhsach_pbdttm_chitiet_dieuchinh]    Script Date: 10/24/2023 1:43:57 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dttm_danhsach_pbdttm_chitiet_dieuchinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dttm_danhsach_pbdttm_chitiet_dieuchinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dttm_danhsach_pbdttm_chitiet]    Script Date: 10/24/2023 1:43:57 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dttm_danhsach_pbdttm_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dttm_danhsach_pbdttm_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet_dieuchinh]    Script Date: 10/24/2023 1:43:57 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dt_danhsach_pbdtc_chitiet_dieuchinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet_dieuchinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]    Script Date: 10/24/2023 1:43:57 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]    Script Date: 10/24/2023 1:43:57 PM ******/
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
	---Lấy danh sách mục lục ngân sách theo điều kiện @LNS
	select 
	NEWID()  as iID_DTC_DuToanChiTrenGiao,
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
	0 as fTienTuChiTruocDieuChinh,
	0 as fTienHienVat,
	0 as fTienHienVatTruocDieuChinh,
	1 as Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	'' as sSoQuyetDinh,
	BH_DM_MucLucNganSach.bHangCha as bHangCha,
	0 as IsRemainRow
	into #tblMucLucNganSach
	from BH_DM_MucLucNganSach 
	where sLNS  IN (SELECT * FROM f_split(@LNS)) and iNamLamViec = @NamLamViec  and sLNS LIKE '901%'

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
			nhanpb_chitiet.fTienHienVat as fHienVat,
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
		#tblChiTietDuToanNhan.fHienVat as fTienHienVat,
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
		chitiet_phanbo.fTienHienVat as fTienHienVat,
		#temp.fTienHienVat as fTienHienVatTruocDieuChinh,
		3 as Type,
		#temp.iID_MaDonVi,
		#temp.sTenDonVi,
		#temp.sSoQuyetDinh,
		0 as bHangCha,
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
	N'Số Chưa Phân Bổ' as sNoiDung,
	chitiet_chuaphanbo.fTuChi as fTienTuChi,
	chitiet_chuaphanbo.fTuChi as fTienTuChiTruocDieuChinh,
	chitiet_chuaphanbo.fHienVat as fTienHienVat,
	chitiet_chuaphanbo.fHienVat as fTienHienVatTruocDieuChinh,
	2 as Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	npb.sSoQuyetDinh as sSoQuyetDinh,
	1 as bHangCha,
	1 as IsRemainRow
	into #tblSoChuaPhanBo
	from #tblMucLucNganSach as muluc_ngansach
	inner join 
	(
		select (ISNULL(ct_npb.fTienTuChi,0) - ISNULL(ct_pb_t.fTuChi,0)) as fTuChi , (ISNULL(ct_npb.fTienHienVat,0) - ISNULL(ct_pb_t.fHienVat,0)) as fHienVat, ct_npb.iID_MucLucNganSach, ct_npb.iID_DTC_DuToanChiTrenGiao 
		from BH_DTC_DuToanChiTrenGiao_ChiTiet as ct_npb
		left join
			(
				select sum(fTienTuChi) as fTuChi , sum(fTienHienVat) as fHienVat , iID_MucLucNganSach, iID_DTC_DuToanChiTrenGiao
				from BH_DTC_PhanBoDuToanChi_ChiTiet as ct_pb
				where iID_DTC_DuToanChiTrenGiao in (select iID_BHDTC_NhanPhanBo from #tblChungTuNhanPhanBo)
				group by  iID_MucLucNganSach, iID_DTC_DuToanChiTrenGiao
			)as ct_pb_t  

		on ct_pb_t.iID_MucLucNganSach = ct_npb.iID_MucLucNganSach and ct_npb.iID_DTC_DuToanChiTrenGiao = ct_pb_t.iID_DTC_DuToanChiTrenGiao) as chitiet_chuaphanbo
		on chitiet_chuaphanbo.iID_MucLucNganSach = muluc_ngansach.iID_MLNS 
	inner join BH_DTC_DuToanChiTrenGiao as npb on npb.ID = chitiet_chuaphanbo.iID_DTC_DuToanChiTrenGiao
	where npb.ID in ( select iID_BHDTC_NhanPhanBo from #tblChungTuNhanPhanBo)


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
	#tblSoChuaPhanBo.fTienHienVat as fTienHienVat,
	#tblSoChuaPhanBo.fTienHienVatTruocDieuChinh as fTienHienVatTruocDieuChinh,
	case when #tblSoChuaPhanBo.Type = 2 then 2 else #tblMucLucNganSach.Type end Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	#tblSoChuaPhanBo.sSoQuyetDinh as sSoQuyetDinh,
	case when #tblSoChuaPhanBo.Type = 2 then 1 else #tblMucLucNganSach.bHangCha end bHangCha,
	0 as IsRemainRow
	into #tblMucLucNganSach_duplicate
	from #tblMucLucNganSach
	left join #tblSoChuaPhanBo on #tblMucLucNganSach.iID_MLNS = #tblSoChuaPhanBo.iID_MLNS
	order by sXauNoiMa
	
	--select * from tblMucLucNganSach_duplicate
	--select * from tblSoChuaPhanBo
	--select * from #temp1

	--Hiển thị kết quả trả về
	select * from
	(
		SELECT * from #tblMucLucNganSach_duplicate
		UNION ALL
		SELECT * from #tblSoChuaPhanBo
		UNION ALL
		SELECT * from #temp1
	) as test
	order by test.sXauNoiMa, test.sSoQuyetDinh, test.iID_MaDonVi,test.Type,test.IsRemainRow


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
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet_dieuchinh]    Script Date: 10/24/2023 1:43:57 PM ******/
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
	bHangCha,
	0 as Type
	into #tblMucLucNganSach
	from BH_DM_MucLucNganSach 
	where sLNS  IN (SELECT * FROM f_split(@LNS)) and iNamLamViec = @NamLamViec and sLNS LIKE '901%'

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
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dttm_danhsach_pbdttm_chitiet]    Script Date: 10/24/2023 1:43:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_bh_dttm_danhsach_pbdttm_chitiet]
	@ChungTuId NVARCHAR(255),
	@LNS NVARCHAR(MAX),
	@IdDonVi NVARCHAR(MAX),
	@NamLamViec int,
	@UserName NVARCHAR(100)
As
begin
	---Lấy danh sách mục lục ngân sách theo điều kiện @LNS
	select 
	NEWID()  as IID_DTTM_BHYT_ThanNhan,
	CAST(NULL AS VARCHAR(50)) as iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet,
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
	0 as fDuToan,
	0 as fDuToanTruocDieuChinh,
	1 as Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	'' as sSoQuyetDinh,
	BH_DM_MucLucNganSach.bHangCha as bHangCha,
	0 as IsRemainRow
	into #tblMucLucNganSach
	from BH_DM_MucLucNganSach 
	where sLNS  IN (SELECT * FROM f_split(@LNS)) and iNamLamViec = @NamLamViec  and sLNS LIKE '903%'

	---Lấy danh sách đơn vị được phân bổ
	select * 
	into  #tblDonVi
	from DonVi where iNamLamViec = 2023 and iID_MaDonVi  IN (SELECT * FROM f_split(@IdDonVi)) ;

	--lấy danh sách dự toán nhận phân bổ
	select *
	into #tblChungTuNhanPhanBo
	from BH_DTTM_BHYT_Nhan_PhanBo_Map
	where iID_DTTM_BHYT_PhanBo = @ChungTuId

	

	---Lấy danh sách chi tiết dự toán toán nhận phân bổ
	select 
			nhanpb_chitiet.iID_DTTM_BHYT_ThanNhan_ChiTiet as iID_DTTM_BHYT_ThanNhan_ChiTiet,
			nhanpb_chitiet.iID_DTTM_BHYT_ThanNhan as iID_DTTM_BHYT_ThanNhan,
			'' as iID_MaDonVi,
			nhanpb_chitiet.iID_MLNS,
			nhanpb_chitiet.fDuToan as fDuToan,
			nhanpb.sSoQuyetDinh
	into #tblChiTietDuToanNhan
	from BH_DTTM_BHYT_ThanNhan_ChiTiet as nhanpb_chitiet
	inner join BH_DTTM_BHYT_ThanNhan as nhanpb on nhanpb.iID_DTTM_BHYT_ThanNhan = nhanpb_chitiet.iID_DTTM_BHYT_ThanNhan
	where nhanpb.iID_DTTM_BHYT_ThanNhan in (select iID_DTTM_BHYT_NhanPhanBo from #tblChungTuNhanPhanBo)

	
	

	---Lấy  danh sách chi tiết nhận phân bổ có thông tin mục lục ngân sách
	select 
		#tblChiTietDuToanNhan.iID_DTTM_BHYT_ThanNhan,
		#tblChiTietDuToanNhan.iID_MLNS as iID_MLNS,
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
		#tblChiTietDuToanNhan.fDuToan,
		3 as Type
	into #tbl_tblChiTietDuToanNhan_MucLuc
	from #tblMucLucNganSach
	inner join #tblChiTietDuToanNhan on #tblMucLucNganSach.iID_MLNS = #tblChiTietDuToanNhan.iID_MLNS

	


	---Hiển thị danh sách chi tiết nhận phân bổ theo đơn vị được chọn phân bổ
	select  #tbl_tblChiTietDuToanNhan_MucLuc.*,#tblDonVi.iID_MaDonVi, concat(#tblDonVi.iID_MaDonVi, '-', #tblDonVi.sTenDonVi) as sTenDonVi, 0 as bHangCha
	into #temp
	from #tbl_tblChiTietDuToanNhan_MucLuc cross join #tblDonVi 

	

	---Map với bảng BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet để lấy thông tin fDuToan đã được phân bổ
	select 
		#temp.iID_DTTM_BHYT_ThanNhan, 
		chitiet_phanbo.iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet as iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet,
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
		chitiet_phanbo.fDuToan as fDuToan,
		#temp.fDuToan as fDuToanTruocDieuChinh,
		3 as Type,
		#temp.iID_MaDonVi,
		#temp.sTenDonVi,
		#temp.sSoQuyetDinh,
		0 as bHangCha,
		0 as IsRemainRow
	into #temp1
	from #temp
	left join 
		(
			select * 
			from BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet 
			where iID_DTTM_BHYT_ThanNhan_PhanBo = @ChungTuId
		) as chitiet_phanbo
		on chitiet_phanbo.iID_DTTM_BHYT_ThanNhan = #temp.iID_DTTM_BHYT_ThanNhan and chitiet_phanbo.iID_MaDonVi = #temp.iID_MaDonVi and chitiet_phanbo.iID_MLNS = #temp.iID_MLNS


		
	-----Lấy danh sách số chưa phân bổ
	select 
	npb.iID_DTTM_BHYT_ThanNhan as iID_DTTM_BHYT_ThanNhan,
	CAST(NULL AS VARCHAR(50)) as iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet,
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
	N'Số Chưa Phân Bổ' as sNoiDung,
	chitiet_chuaphanbo.fDuToan as fDuToan,
	chitiet_chuaphanbo.fDuToan as fDuToanTruocDieuChinh,
	2 as Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	npb.sSoQuyetDinh as sSoQuyetDinh,
	1 as bHangCha,
	1 as IsRemainRow
	into #tblSoChuaPhanBo
	from #tblMucLucNganSach as muluc_ngansach
	inner join 
	(
		select (ISNULL(ct_npb.fDuToan,0) - ISNULL(ct_pb_t.fDuToan,0)) as fDuToan, ct_npb.iID_MLNS, ct_npb.iID_DTTM_BHYT_ThanNhan 
		from BH_DTTM_BHYT_ThanNhan_ChiTiet as ct_npb
		left join
			(
				select sum(fDuToan) as fDuToan , iID_MLNS, iID_DTTM_BHYT_ThanNhan
				from BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet as ct_pb
				where iID_DTTM_BHYT_ThanNhan in (select iID_DTTM_BHYT_NhanPhanBo from #tblChungTuNhanPhanBo)
				group by  iID_MLNS, iID_DTTM_BHYT_ThanNhan
			)as ct_pb_t  

		on ct_pb_t.iID_MLNS = ct_npb.iID_MLNS and  ct_npb.iID_DTTM_BHYT_ThanNhan = ct_pb_t.iID_DTTM_BHYT_ThanNhan) as chitiet_chuaphanbo
		on chitiet_chuaphanbo.iID_MLNS = muluc_ngansach.iID_MLNS 
	inner join BH_DTTM_BHYT_ThanNhan as npb on npb.iID_DTTM_BHYT_ThanNhan = chitiet_chuaphanbo.iID_DTTM_BHYT_ThanNhan
	where npb.iID_DTTM_BHYT_ThanNhan in ( select iID_DTTM_BHYT_NhanPhanBo from #tblChungTuNhanPhanBo)

	---- Lấy mục lục ngân sách có dupliate các mục lục ngân sách con
	select 
	#tblSoChuaPhanBo.iID_DTTM_BHYT_ThanNhan as IID_DTTM_BHYT_ThanNhan,
	CAST(NULL AS VARCHAR(50)) as iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet,
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
	#tblSoChuaPhanBo.fDuToan as fDuToan,
	#tblSoChuaPhanBo.fDuToanTruocDieuChinh as fDuToanTruocDieuChinh,
	case when #tblSoChuaPhanBo.Type = 2 then 2 else #tblMucLucNganSach.Type end Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	#tblSoChuaPhanBo.sSoQuyetDinh as sSoQuyetDinh,
	case when #tblSoChuaPhanBo.Type = 2 then 1 else #tblMucLucNganSach.bHangCha end bHangCha,
	0 as IsRemainRow
	into #tblMucLucNganSach_duplicate
	from #tblMucLucNganSach
	left join #tblSoChuaPhanBo on #tblMucLucNganSach.iID_MLNS = #tblSoChuaPhanBo.iID_MLNS
	order by sXauNoiMa
	

	--Hiển thị kết quả trả về
	select * from
	(
		SELECT * from #tblMucLucNganSach_duplicate
		UNION ALL
		SELECT * from #tblSoChuaPhanBo
		UNION ALL
		SELECT * from #temp1
	) as test
	order by test.sXauNoiMa, test.sSoQuyetDinh, test.iID_MaDonVi,test.Type,test.IsRemainRow


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
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dttm_danhsach_pbdttm_chitiet_dieuchinh]    Script Date: 10/24/2023 1:43:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_bh_dttm_danhsach_pbdttm_chitiet_dieuchinh]
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
	NULL as IID_DTTM_BHYT_ThanNhan,
	NULL as iID_DTTM_BHYT_ThanNhan_ChiTiet,
	NULL as iID_MaDonVi,
	NULL as sTenDonVi,
	NULL as sSoQuyetDinh,
	0 as fDuToanTruocDieuChinh,
	0 as fDuToan,
	0 as fDuToanSauDieuChinh,
	bHangCha,
	0 as Type
	into #tblMucLucNganSach
	from BH_DM_MucLucNganSach 
	where sLNS  IN (SELECT * FROM f_split(@LNS)) and iNamLamViec = @NamLamViec and sLNS LIKE '903%'

	---L?y danh sách chung tu bi dieu chinh
	select * 
	into #tblChungTuBiDieuChinh
	from BH_DTTM_BHYT_Nhan_PhanBo_Map
	where iID_DTTM_BHYT_PhanBo = @ChungTuId

	---L?y danh sách chi ti?t ch?ng t? bi dieu chinh
	select 
	dc_ct.iID_MLNS, 
	dc_ct.iID_MaDonVi,
	dc_ct.fDuToan as fDuToanTruocDieuChinh,
	dc_ct.iID_DTTM_BHYT_ThanNhan as iID_DTTM_BHYT_ThanNhan,
	dc_ct.iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet,
	dc.sSoQuyetDinh
	into #tblChungTuBiDieuChinh_Ct 
	from BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet as dc_ct  
	inner join BH_DTTM_BHYT_ThanNhan_PhanBo as dc on dc_ct.iID_DTTM_BHYT_ThanNhan_PhanBo = dc.iID_DTTM_BHYT_ThanNhan_PhanBo
	where dc_ct.iID_DTTM_BHYT_ThanNhan_PhanBo in ( select iID_DTTM_BHYT_NhanPhanBo from #tblChungTuBiDieuChinh)

	

	---Map voi bang BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet de lay thong tin fDuToan cua chung tu dieu chinh
	select 
	npb_ct.iID_MLNS,
	npb_ct.iID_MaDonVi,
	npb_ct.sSoQuyetDinh,
	npb_ct.fDuToanTruocDieuChinh as fDuToanTruocDieuChinh,
	npb_ct.iID_DTTM_BHYT_ThanNhan as iID_DTTM_BHYT_ThanNhan,
	pb_ct.iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet as iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet,
	pb_ct.fDuToan,
	0 as bHangCha,
	2 Type
	into #tblThongTinChungTu_BiDieuChinh
	from #tblChungTuBiDieuChinh_Ct as npb_ct
	left join ( select * from BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet where iID_DTTM_BHYT_ThanNhan_PhanBo =  @ChungTuId) as  pb_ct on npb_ct.iID_MLNS = pb_ct.iID_MLNS and npb_ct.iID_MaDonVi = pb_ct.iID_MaDonVi
	and npb_ct.iID_DTTM_BHYT_ThanNhan = pb_ct.iID_DTTM_BHYT_ThanNhan

	---Thong tin chi tiết chứng từ điều chỉnh thêm mới
	select 
	pb_ct.iID_MLNS,
	pb_ct.iID_MaDonVi,
	pb.sSoQuyetDinh,
	0 as fDuToanTruocDieuChinh,
	pb_ct.iID_DTTM_BHYT_ThanNhan as iID_DTTM_BHYT_ThanNhan,
	pb_ct.iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet,
	pb_ct.fDuToan,
	0 as bHangCha,
	2 Type
	into #tblThongTinChungTu_DieuChinhThemMoi
	from BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet as pb_ct
	inner join BH_DTTM_BHYT_ThanNhan_PhanBo as pb on pb_ct.iID_DTTM_BHYT_ThanNhan_PhanBo = pb.iID_DTTM_BHYT_ThanNhan_PhanBo
	where pb_ct.iID_DTTM_BHYT_ThanNhan_PhanBo = @ChungTuId and pb_ct.iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet not in  (select iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet from  #tblThongTinChungTu_BiDieuChinh where iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet is not null)

	

	---Thông tin chi tiết chúng từ
	select 
	*
	into #tblThongTinChungTu
	from
		(
			select * from #tblThongTinChungTu_BiDieuChinh
			UNION ALL
			select * from #tblThongTinChungTu_DieuChinhThemMoi

		) as tbl


	--select * from tblThongTinChungTu
	--select * from #tblMucLucNganSach

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
	#tblThongTinChungTu.iID_DTTM_BHYT_ThanNhan,
	#tblThongTinChungTu.iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet,
	#tblThongTinChungTu.iID_MaDonVi,
	CONCAT(DonVi.iID_MaDonVi, '-', DonVi.sTenDonVi) as sTenDonVi,
	#tblThongTinChungTu.sSoQuyetDinh,
	#tblThongTinChungTu.fDuToanTruocDieuChinh,
	#tblThongTinChungTu.fDuToan,
	ISNULL(#tblThongTinChungTu.fDuToanTruocDieuChinh,0) + ISNULL(#tblThongTinChungTu.fDuToan,0) as fDuToanSauDieuChinh,
	case when #tblThongTinChungTu.Type = 2 then #tblThongTinChungTu.bHangCha else #tblMucLucNganSach.bHangCha end as bHangCha
	into #tblThongTinChungTu_MLNS
	from #tblMucLucNganSach
	left join #tblThongTinChungTu on #tblMucLucNganSach.iID_MLNS = #tblThongTinChungTu.iID_MLNS
	left join DonVi on #tblThongTinChungTu.iID_MaDonVi = DonVi.iID_MaDonVi and DonVi.InamLamViec = @NamLamViec

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
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_export_kehoachcaptamung_kcbbhyt]    Script Date: 10/24/2023 1:43:57 PM ******/
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
@Donvitinh int
As
begin
	select 
			row_number() over (order by cptu_ct.iID_MaCoSoYTe) as sTT,
			sum(cptu_ct.fQTQuyTruoc)/@Donvitinh as fQTQuyTruoc, 
			sum(cptu_ct.fTamUngQuyNay)/@Donvitinh as fTamUngQuyNay, 
			sum(cp_bs.fSoCapBoSung + cp_bs.fDaCapUng)/@Donvitinh  as  fLuyKeCapDenCuoiQuy,
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
			and ((@ILoai = 1 and cptu_ct.sLNS = '9050001') or (@ILoai = 2 and cptu_ct.sLNS = '9050002'))
		group by cptu_ct.iID_MaCoSoYTe, csyt.sTenCoSoYTe
		
end
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_export_thongtricaptamung_kcbbhyt]    Script Date: 10/24/2023 1:43:57 PM ******/
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
		sum(cp_ct.fQTQuyTruoc) as fQTQuyTruoc,
		sum(cp_ct.fTamUngQuyNay) as fTamUngQuyNay,
		sum(cp_ct.fLuyKeCapDenCuoiQuy) as fLuyKeCapDenCuoiQuy
		into #tblCapPhatChiTiet
		from BH_CP_CapTamUng_KCB_BHYT_ChiTiet as cp_ct
		inner join BH_CP_CapTamUng_KCB_BHYT as  cp on cp.iID_BH_CP_CapTamUng_KCB_BHYT = cp_ct.iID_BH_CP_CapTamUng_KCB_BHYT
		where cp_ct.iID_MaCoSoYTe In (SELECT * FROM f_split(@IdCsYTe))
		and cp.iQuy = @IQuy and cp.iNamLamViec = @NamLamViec 
		and ((@ILoai = 5 and cp_ct.sLNS like '9050001') or (@ILoai = 6 and cp_ct.sLNS like '9050002'))
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
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_export_tonghopcaptamung_kcbbhyt]    Script Date: 10/24/2023 1:43:57 PM ******/
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
@Donvitinh int
As
begin
	select 
			row_number() over (order by cptu_ct.iID_MaCoSoYTe) as sTT,
			sum(cptu_ct.fQTQuyTruoc)/@Donvitinh as fQTQuyTruoc, 
			sum(cptu_ct.fTamUngQuyNay)/@Donvitinh as fTamUngQuyNay, 
			sum(cptu_ct.fLuyKeCapDenCuoiQuy)/@Donvitinh as fLuyKeCapDenCuoiQuy, 
			cptu_ct.iID_MaCoSoYTe as iID_MaCoSoYTe,
			csyt.sTenCoSoYTe as sTenCoSoYTe
		from BH_CP_CapTamUng_KCB_BHYT_ChiTiet as cptu_ct
		inner join BH_CP_CapTamUng_KCB_BHYT as cptu on cptu_ct.iID_BH_CP_CapTamUng_KCB_BHYT = cptu.iID_BH_CP_CapTamUng_KCB_BHYT
		inner join DM_CoSoYTe as csyt on csyt.iID_MaCoSoYTe = cptu_ct.iID_MaCoSoYTe
		where cptu_ct.iID_MaCoSoYTe In (SELECT * FROM f_split(@IdCsYTe))
			and  cptu.sDSSoChungTuTongHop is not null
			and cptu.iQuy = @IQuy
			and ((@ILoai = 3 and cptu_ct.sLNS like '9050001') or (@ILoai = 4 and cptu_ct.sLNS like '9050002'))
		group by cptu_ct.iID_MaCoSoYTe, csyt.sTenCoSoYTe
		
end
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcnKCB_create_data_summary]    Script Date: 10/24/2023 1:43:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtcnKCB_create_data_summary]
@IdChungTu nvarchar(MAX),
@NguoiTao nvarchar(500),
@YearOfWork int,
@IdChungTuSummary nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

INSERT INTO BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet(ID_QTC_Nam_KCB_QuanYDonVi_ChiTiet, iID_QTC_Nam_KCB_QuanYDonVi, iID_MucLucNganSach, sNoiDung, dNgaySua, dNgayTao, sNguoiSua, sNguoiTao,fTien_DuToanNamTruocChuyenSang,
fTien_DuToanGiaoNamNay, fTien_TongDuToanDuocGiao, fTien_ThucChi, fTienThua,fTienThieu)
SELECT 
	   NEWID(),
	   @IdChungTuSummary,
       iID_MucLucNganSach,
       sNoiDung,
	   GETDATE(),
	   GETDATE(),
	   @NguoiTao,
	   '',
	   Sum(fTien_DuToanNamTruocChuyenSang),
	   Sum(fTien_DuToanGiaoNamNay),
	   Sum(fTien_TongDuToanDuocGiao),
	   Sum(fTien_ThucChi),
	   Sum(fTienThua),
	   Sum(fTienThieu)
	   
FROM BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet
WHERE  iID_QTC_Nam_KCB_QuanYDonVi IN
    (SELECT *
     FROM f_split(@IdChungTu))
group by iID_MucLucNganSach, sNoiDung

UPDATE BH_QTC_Nam_KCB_QuanYDonVi SET bDaTongHop = 1 WHERE ID_QTC_Nam_KCB_QuanYDonVi IN (SELECT * FROM f_split(@IdChungTu));
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcnKCB_update_total]    Script Date: 10/24/2023 1:43:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qtcnKCB_update_total] 
	@VoucherId nvarchar(255),
	@UserModify nvarchar(100)
AS
BEGIN
	declare @FTongTienDuToanNamTruocChuyenSang float;
	declare @FTongTienDuToanGiaoNamNay float;
	declare @FTongTienTongDuToanDuocGiao float;
	declare @FTongTienThucChi float;
	declare @FTongTienThua float;
	declare @FTongTienThieu float;
	declare @FTongTiLeThucHienTrenDuToan float;

	select 
		@FTongTienDuToanNamTruocChuyenSang = SUM(fTien_DuToanNamTruocChuyenSang) ,
		@FTongTienDuToanGiaoNamNay= SUM(fTien_DuToanGiaoNamNay),
		@FTongTienTongDuToanDuocGiao = SUM(fTien_TongDuToanDuocGiao),
		@FTongTienThucChi = SUM(fTien_ThucChi) ,
		@FTongTienThua = SUM(fTienThua) ,
		@FTongTienThieu = SUM(fTienThieu) ,
		@FTongTiLeThucHienTrenDuToan = SUM(fTiLeThucHienTrenDuToan)

	FROM BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet where iID_QTC_Nam_KCB_QuanYDonVi = @VoucherId;


	update BH_QTC_Nam_KCB_QuanYDonVi 
	set fTongTien_DuToanNamTruocChuyenSang = @FTongTienDuToanNamTruocChuyenSang, 
		fTongTien_DuToanGiaoNamNay = @FTongTienDuToanGiaoNamNay, 
		fTongTien_TongDuToanDuocGiao = @FTongTienTongDuToanDuocGiao,
		fTongTien_ThucChi = @FTongTienThucChi,
		fTongTienThua = @FTongTienThua,
		fTongTienThieu =  @FTongTienThieu,
		fTiLeThucHienTrenDuToan = @FTongTiLeThucHienTrenDuToan,
		dNgaySua = GETDATE(), 
		sNguoiSua = @UserModify  
	where ID_QTC_Nam_KCB_QuanYDonVi = @VoucherId; 
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]    Script Date: 10/24/2023 1:43:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@Lns nvarchar(max),
@Donvitinh int,
@IsTongHop int
as
begin
	---Lấy danh sách mục lục ngân sách
		select 
			danhmuc.iID_MLNS as iID_MLNS,
			danhmuc.iID_MLNS_Cha,
			danhmuc.sLNS,
			danhmuc.sL,
			danhmuc.sK,
			danhmuc.sM,
			danhmuc.sTM,
			danhmuc.sTTM,
			danhmuc.sNG,
			danhmuc.sTNG,
			danhmuc.sXauNoiMa,
			danhmuc.sMoTa,
			danhmuc.bHangCha
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach as danhmuc
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs in ('9030001', '9030002')


	---Lấy danh sách chi tiết
		select	
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sNoiDung,
			Sum(qtcn_ct.fTien_DuToanNamTruocChuyenSang) as fTien_DuToanNamTruocChuyenSang,
			Sum(qtcn_ct.fTien_DuToanGiaoNamNay) as fTien_DuToanGiaoNamNay,
			Sum(qtcn_ct.fTien_TongDuToanDuocGiao) as fTien_TongDuToanDuocGiao,
			Sum(qtcn_ct.fTien_ThucChi) as fTien_ThucChi,
			Sum(qtcn_ct.fTien_TongDuToanDuocGiao) - Sum(qtcn_ct.fTien_ThucChi)  as fTienThua,
			Sum(qtcn_ct.fTien_ThucChi) - Sum(qtcn_ct.fTien_TongDuToanDuocGiao) as fTienThieu,
			Sum(qtcn_ct.fTiLeThucHienTrenDuToan) as fTiLeThucHienTrenDuToan

		into #tbl_qtcn_chitiet
		from BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet as  qtcn_ct
		inner join  BH_QTC_Nam_KCB_QuanYDonVi as qtcn on qtcn_ct.iID_QTC_Nam_KCB_QuanYDonVi = qtcn.ID_QTC_Nam_KCB_QuanYDonVi
		where qtcn.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
		and qtcn.iNamChungTu = @INamLamViec
		and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
		group by qtcn_ct.iID_MucLucNganSach,qtcn_ct.sNoiDung



		---Kết quả hiển thị trả về
		select 
			mucluc.iID_MLNS,
			mucluc.iID_MLNS_Cha,
			mucluc.sLNS,
			mucluc.sL,
			mucluc.sK,
			mucluc.sM,
			mucluc.sTM,
			mucluc.sTTM,
			mucluc.sNG,
			mucluc.sTNG,
			mucluc.sXauNoiMa,
			mucluc.bHangCha,
			mucluc.iID_MLNS as iID_MucLucNganSach,
			mucluc.sMoTa as sNoiDung,
			chi_tiet.fTien_DuToanNamTruocChuyenSang, 
			chi_tiet.fTien_DuToanGiaoNamNay,
			chi_tiet.fTien_TongDuToanDuocGiao,
			chi_tiet.fTien_ThucChi,
			chi_tiet.fTienThua,
			chi_tiet.fTienThieu,
			chi_tiet.fTiLeThucHienTrenDuToan

		from #tblMucLucNganSach as mucluc
		left join #tbl_qtcn_chitiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
		order by mucluc.sXauNoiMa


		drop table #tblMucLucNganSach;
		drop table #tbl_qtcn_chitiet;

end
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]    Script Date: 10/24/2023 1:43:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@SLN nvarchar(max),
@IsTongHop bit,
@IQuy int,
@Donvitinh int
as
begin
	---Lấy danh sách mục lục ngân sách
		select 
			danhmuc.iID_MLNS as iID_MLNS,
			danhmuc.iID_MLNS_Cha,
			danhmuc.sLNS,
			danhmuc.sL,
			danhmuc.sK,
			danhmuc.sM,
			danhmuc.sTM,
			danhmuc.sTTM,
			danhmuc.sNG,
			danhmuc.sTNG,
			danhmuc.sXauNoiMa,
			danhmuc.sMoTa,
			danhmuc.bHangCha
		into tblMucLucNganSach
		from BH_DM_MucLucNganSach as danhmuc
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs  in (select * from dbo.splitstring(@SLN)) 
		
		
		
	---Lấy thông tin chi tiết chứng từ
		select 
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sNoiDung,
			Sum(qtcn_ct.fTien_DuToanNamTruocChuyenSang) as fTien_DuToanNamTruocChuyenSang,
			Sum(qtcn_ct.fTien_DuToanGiaoNamNay) as fTien_DuToanGiaoNamNay,
			Sum(qtcn_ct.fTien_TongDuToanDuocGiao) as fTien_TongDuToanDuocGiao,
			sum(qtcn_ct.fTienThucChi) as fTienThucChi ,
			sum(qtcn_ct.fTienQuyetToanDaDuyet) as fTienQuyetToanDaDuyet ,
			sum(qtcn_ct.fTienDeNghiQuyetToanQuyNay) as fTienDeNghiQuyetToanQuyNay,
			sum(qtcn_ct.fTienXacNhanQuyetToanQuyNay) as fTienXacNhanQuyetToanQuyNay
		into tblQuyetToanQuyChiTiet
		from BH_QTC_Quy_KCB_ChiTiet as qtcn_ct
		inner join BH_QTC_Quy_KCB  as qtcn on qtcn_ct.iID_QTC_Quy_KCB = qtcn.ID_QTC_Quy_KCB
		where qtcn.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
		and qtcn.iNamChungTu = @INamLamViec
		and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
		and qtcn.iQuyChungTu = @IQuy
		group by qtcn_ct.iID_MucLucNganSach,qtcn_ct.sNoiDung

		
	---Kết quả hiển thị trả về
		select 
			mucluc.iID_MLNS,
			mucluc.iID_MLNS_Cha,
			mucluc.sLNS,
			mucluc.sL,
			mucluc.sK,
			mucluc.sM,
			mucluc.sTM,
			mucluc.sTTM,
			mucluc.sNG,
			mucluc.sTNG,
			mucluc.sXauNoiMa,
			mucluc.bHangCha,
			mucluc.iID_MLNS as iID_MucLucNganSach,
			mucluc.sMoTa as sNoiDung,
			chi_tiet.fTien_DuToanNamTruocChuyenSang,
			chi_tiet.fTien_DuToanGiaoNamNay,
			chi_tiet.fTien_TongDuToanDuocGiao,
			chi_tiet.fTienThucChi,
			chi_tiet.fTienQuyetToanDaDuyet,
			chi_tiet.fTienDeNghiQuyetToanQuyNay,
			chi_tiet.fTienXacNhanQuyetToanQuyNay
		from tblMucLucNganSach as mucluc
		left join tblQuyetToanQuyChiTiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
		order by mucluc.sXauNoiMa
	
	drop table tblMucLucNganSach;
	drop table tblQuyetToanQuyChiTiet;
end


GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]    Script Date: 10/24/2023 1:43:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]
@IdChungTu uniqueidentifier,
@INamLamViec int,
@IsTongHop4Quy bit
as
begin
	---Lấy danh sách mục lục ngân sách
		select 
			danhmuc.iID_MLNS as iID_MLNS,
			danhmuc.iID_MLNS_Cha,
			danhmuc.sLNS,
			danhmuc.sL,
			danhmuc.sK,
			danhmuc.sM,
			danhmuc.sTM,
			danhmuc.sTTM,
			danhmuc.sNG,
			danhmuc.sTNG,
			danhmuc.sXauNoiMa,
			danhmuc.sMoTa,
			danhmuc.bHangCha
		into tblMucLucNganSach
		from BH_DM_MucLucNganSach as danhmuc
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs in ('9030001', '9030002')
		
		
		
	---Lấy thông tin chi tiết chứng từ
		select 
			qtcn_ct.ID_QTC_Nam_KCB_QuanYDonVi_ChiTiet,
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sNoiDung,
			qtcn_ct.fTien_DuToanNamTruocChuyenSang,
			qtcn_ct.fTien_DuToanGiaoNamNay,
			qtcn_ct.fTien_TongDuToanDuocGiao,
			qtcn_ct.fTien_ThucChi,
			isnull(qtcn_ct.fTien_TongDuToanDuocGiao,0) - isnull(qtcn_ct.fTien_ThucChi,0)  as fTienThua,
			isnull(qtcn_ct.fTien_ThucChi,0) - isnull(qtcn_ct.fTien_TongDuToanDuocGiao,0) as  fTienThieu
		into tblQuyetToanNamChiTiet
		from BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet as qtcn_ct
		inner join BH_QTC_Nam_KCB_QuanYDonVi  as qtcn on qtcn_ct.iID_QTC_Nam_KCB_QuanYDonVi = qtcn.ID_QTC_Nam_KCB_QuanYDonVi
		where qtcn_ct.iID_QTC_Nam_KCB_QuanYDonVi = @IdChungTu;

		
	---Kết quả hiển thị trả về
	select 
		mucluc.iID_MLNS,
		mucluc.iID_MLNS_Cha,
		mucluc.sLNS,
		mucluc.sL,
		mucluc.sK,
		mucluc.sM,
		mucluc.sTM,
		mucluc.sTTM,
		mucluc.sNG,
		mucluc.sTNG,
		mucluc.sXauNoiMa,
		mucluc.bHangCha,
		chi_tiet.ID_QTC_Nam_KCB_QuanYDonVi_ChiTiet,
		mucluc.iID_MLNS as iID_MucLucNganSach,
		mucluc.sMoTa as sNoiDung,
		chi_tiet.fTien_DuToanNamTruocChuyenSang, 
		chi_tiet.fTien_DuToanGiaoNamNay,
		chi_tiet.fTien_TongDuToanDuocGiao,
		chi_tiet.fTien_ThucChi,
		chi_tiet.fTienThua,
		chi_tiet.fTienThieu
	from tblMucLucNganSach as mucluc
	left join tblQuyetToanNamChiTiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
	order by mucluc.sXauNoiMa
	
	drop table tblMucLucNganSach;
	drop table tblQuyetToanNamChiTiet;
end


GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_danhsachquyettoannamKCB_index]    Script Date: 10/24/2023 1:43:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_danhsachquyettoannamKCB_index]
@INamLamViec int,
@Search nvarchar(max)
As
Begin
	select 
		qtn.ID_QTC_Nam_KCB_QuanYDonVi,
		qtn.iID_DonVi,
		qtn.iID_MaDonVi,
		qtn.sSoChungTu,
		qtn.dNgayChungTu,
		qtn.bThucChiTheo4Quy,
		qtn.iNamChungTu,
		qtn.sSoQuyetDinh,
		qtn.dNgayQuyetDinh,
		qtn.sMoTa,
		qtn.bIsKhoa,
		qtn.iLoaiTongHop,
		qtn.sTongHop,
		qtn.fTongTien_DuToanNamTruocChuyenSang,
		qtn.fTongTien_DuToanGiaoNamNay,
		qtn.fTongTien_TongDuToanDuocGiao,
		qtn.fTongTien_ThucChi,
		isnull(qtn.fTongTien_TongDuToanDuocGiao,0) - isnull(qtn.fTongTien_ThucChi,0) as fTongTienThua,
		isnull(qtn.fTongTien_ThucChi,0) - isnull(qtn.fTongTien_TongDuToanDuocGiao,0) as fTongTienThieu,
		qtn.bDaTongHop,
		qtn.sDSSoChungTuTongHop,
		dv.sTenDonVi,
		qtn.sNguoiTao
	from BH_QTC_Nam_KCB_QuanYDonVi as qtn
	left join DonVi as dv on qtn.iID_DonVi = dv.iID_DonVi
	where dv.iNamLamViec = @INamLamViec
	and (@Search is null or ( @Search is not null and  qtn.sSoChungTu like N'%'+@Search+ '%'))

	

End
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_phulucquyettoannam_KCB]    Script Date: 10/24/2023 1:43:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_phulucquyettoannam_KCB]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@Lns nvarchar(max),
@Donvitinh int,
@IsTongHop int
as
begin
	---Lấy quyết toán năm trước
	select
		donvi.sTenDonVi,
		sum(chungtu_ct.fTien_TongDuToanDuocGiao) as fChiTieuNamTruoc,
		0 as fChiTieuNamNay,
		0 as fTongCong,
		0 as fTienQuyetToan,
		0 as fTienThua,
		0 as fTienThieu
	into #tblNamTruoc
	from BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet as chungtu_ct
	inner join BH_QTC_Nam_KCB_QuanYDonVi as chungtu on chungtu_ct.iID_QTC_Nam_KCB_QuanYDonVi = chungtu.ID_QTC_Nam_KCB_QuanYDonVi
	inner join (
					select * from BH_DM_MucLucNganSach where iNamLamViec = @INamLamViec -1 and sLNs in (select * from dbo.splitstring(@Lns))
				) as danhmuc on danhmuc.iID_MLNS = chungtu_ct.iID_MucLucNganSach
	inner join (
					select * from DonVi where iNamLamViec = @INamLamViec -1 
				) as donvi on donvi.iID_MaDonVi = chungtu.iID_MaDonVi
	where chungtu.iNamChungTu = @INamLamViec -1
	and chungtu.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi))
	and ((@IsTongHop = 0 and chungtu.bDaTongHop = 0 and chungtu.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  chungtu.sDSSoChungTuTongHop is not null))
	group by donvi.sTenDonVi

	---Lấy quyết toán năm trước

	select
		donvi.sTenDonVi,
		0 fChiTieuNamTruoc,
		sum(chungtu_ct.fTien_TongDuToanDuocGiao) as fChiTieuNamNay,
		0 as fTongCong,
		sum(fTien_ThucChi) as fTienQuyetToan,
		0 as fTienThua,
		0 as fTienThieu
	into #tblNamNay
	from BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet as chungtu_ct
	inner join BH_QTC_Nam_KCB_QuanYDonVi as chungtu on chungtu_ct.iID_QTC_Nam_KCB_QuanYDonVi = chungtu.ID_QTC_Nam_KCB_QuanYDonVi
	inner join (
					select * from BH_DM_MucLucNganSach where iNamLamViec = @INamLamViec and sLNs in (select * from dbo.splitstring(@Lns))
				) as danhmuc on danhmuc.iID_MLNS = chungtu_ct.iID_MucLucNganSach
	inner join (
					select * from DonVi where iNamLamViec = @INamLamViec
				) as donvi on donvi.iID_MaDonVi = chungtu.iID_MaDonVi
	where chungtu.iNamChungTu = @INamLamViec
	and chungtu.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi))
	and ((@IsTongHop = 0 and chungtu.bDaTongHop = 0 and chungtu.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  chungtu.sDSSoChungTuTongHop is not null))
	group by donvi.sTenDonVi

	--- Kết quả trả về

	select
		ROW_NUMBER () OVER (ORDER BY sTenDonVi) sTT,
		sTenDonVi,
		sum(fChiTieuNamTruoc) as fChiTieuNamTruoc,
		sum(fChiTieuNamNay) as fChiTieuNamNay,
		isnull(sum(fChiTieuNamTruoc) ,0) + isnull(sum(fChiTieuNamNay) ,0) as fTongCong,
		sum(fTienQuyetToan) as fTienQuyetToan,
		isnull(sum(fChiTieuNamTruoc) ,0) + isnull(sum(fChiTieuNamNay) ,0) - isnull(sum(fTienQuyetToan) ,0) as fTienThua,
		isnull(sum(fTienQuyetToan) ,0) - isnull(sum(fChiTieuNamTruoc) ,0) + isnull(sum(fChiTieuNamNay) ,0) as fTienThieu
	from
	(
		select * from #tblNamTruoc
		union all
		select * from #tblNamNay
	) as result

	group by sTenDonVi
	order by sTenDonVi


	drop table #tblNamTruoc;
	drop table #tblNamNay;
end
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiao_index]    Script Date: 10/24/2023 1:43:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE  [dbo].[sp_bhxh_nhandutoanchitrengiao_index]
	@YearOfWork int
AS
BEGIN
	SELECT
	DTC.ID,
	DTC.sSoChungTu,
	DTC.iID_MaDonVi,
	DV.sTenDonVi AS sTenDonVi,
	DTC.dNgayChungTu,
	DTC.sSoQuyetDinh,
	DTC.dNgayQuyetDinh,
	DTC.sLNS,
	DTC.fTongTienTuChi,
	DTC.fTongTienHienVat,
	DTC.fTongTien,
	DTC.iLoaiDotNhanPhanBo,
	DTC.sMoTa,
	DTC.iNamChungTu,
	DTC.sNguoiTao,
	DTC.bIsKhoa,
	BH_DM_LoaiChi.sTenDanhMucLoaiChi AS sLoaiChi
	FROM BH_DTC_DuToanChiTrenGiao DTC
	LEFT JOIN DonVi DV ON DTC.iID_MaDonVi = DV.iID_MaDonVi
	INNER JOIN BH_DM_LoaiChi ON DTC.iID_LoaiDanhMucChi = BH_DM_LoaiChi.iID
	WHERE DV.iNamLamViec = @YearOfWork and DTC.iNamChungTu = @YearOfWork
	ORDER BY DTC.dNgayQuyetDinh DESC
END;
;
;
GO
