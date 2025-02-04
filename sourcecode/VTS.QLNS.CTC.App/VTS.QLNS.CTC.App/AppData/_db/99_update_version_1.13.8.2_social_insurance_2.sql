/****** Object:  StoredProcedure [dbo].[sp_dt_phanbo_dieuchinh_chi_tiet]    Script Date: 1/10/2024 5:45:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_phanbo_dieuchinh_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_phanbo_dieuchinh_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_getTienthuchien6thang]    Script Date: 1/10/2024 5:45:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dieuchinh_getTienthuchien6thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dieuchinh_getTienthuchien6thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_kpql_slns]    Script Date: 1/10/2024 5:45:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_kpql_slns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_kpql_slns]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_khac_slns]    Script Date: 1/10/2024 5:45:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_khac_slns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_khac_slns]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_kcbqy_slns]    Script Date: 1/10/2024 5:45:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_kcbqy_slns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_kcbqy_slns]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns]    Script Date: 1/10/2024 5:45:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt]    Script Date: 1/10/2024 5:45:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_Bhxh_slns]    Script Date: 1/10/2024 5:45:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_Bhxh_slns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_Bhxh_slns]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_phulucquyettoannam_KCB]    Script Date: 1/10/2024 5:45:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_phulucquyettoannam_KCB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_phulucquyettoannam_KCB]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]    Script Date: 1/10/2024 5:45:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_khc]    Script Date: 1/10/2024 5:45:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_get_data_khc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_get_data_khc]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet_dieuchinh]    Script Date: 1/10/2024 5:45:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dt_danhsach_pbdtc_chitiet_dieuchinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet_dieuchinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]    Script Date: 1/10/2024 5:45:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]    Script Date: 1/10/2024 5:45:32 PM ******/
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
	BH_DM_MucLucNganSach.sCPChiTietToi,
	BH_DM_MucLucNganSach.sDuToanChiTietToi,
	1 as Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	'' as sSoQuyetDinh,
	BH_DM_MucLucNganSach.bHangCha as bHangCha,
	0 as IsRemainRow
	into #tblMucLucNganSach
	from BH_DM_MucLucNganSach 
	--where sLNS  IN (SELECT * FROM f_split(@LNS)) and iNamLamViec = @NamLamViec  and sLNS LIKE '901%'
	where sLNS  IN (SELECT * FROM f_split(@LNS)) and iNamLamViec = @NamLamViec  

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
		chitiet_phanbo.fTienHienVat as fTienHienVat,
		#temp.fTienHienVat as fTienHienVatTruocDieuChinh,
		#temp.sCPChiTietToi,
		#temp.sDuToanChiTietToi,
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
	N'Số chưa phân bổ' as sNoiDung,
	chitiet_chuaphanbo.fTuChi as fTienTuChi,
	chitiet_chuaphanbo.fTuChi as fTienTuChiTruocDieuChinh,
	chitiet_chuaphanbo.fHienVat as fTienHienVat,
	chitiet_chuaphanbo.fHienVat as fTienHienVatTruocDieuChinh,
	muluc_ngansach.sCPChiTietToi,
	muluc_ngansach.sDuToanChiTietToi,
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
	#tblMucLucNganSach.sCPChiTietToi,
	#tblMucLucNganSach.sDuToanChiTietToi,
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet_dieuchinh]    Script Date: 1/10/2024 5:45:32 PM ******/
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
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_khc]    Script Date: 1/10/2024 5:45:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_get_data_khc]
	@NamLamViec int,
	@SoChungTu nvarchar(500),
	@SLNS nvarchar(500)
AS
BEGIN
	select
		ct.iID_MaDonVi IIdMaDonVi,
		mlns.sXauNoiMa,
		mlns.iID_MLNS IID_MucLucNganSach,
		ctct.fTienKeHoachThucHienNamNay
		into #temp
	from
	BH_DM_MucLucNganSach mlns
	left join
	BH_KHC_CheDoBHXH_ChiTiet ctct on ctct.iID_MucLucNganSach = mlns.iID_MLNS
	join
	(select * from BH_KHC_CheDoBHXH
		where iNamLamViec = @NamLamViec
		and sSoChungTu in ((SELECT * FROM f_split(@SoChungTu)))) ct on ctct.iID_KHC_CheDoBHXH = ct.id
	--join BH_DM_MucLucNganSach mlns on ctct.iID_MucLucNganSach = mlns.iID_MLNS
	----------------
	union
	select
		ct.iID_MaDonVi IIdMaDonVi,
		mlns.sXauNoiMa,
		mlns.iID_MLNS IID_MucLucNganSach,
		ctct.fTienKeHoachThucHienNamNay
	from
	 BH_DM_MucLucNganSach mlns  
	 left join 
	BH_KHC_K_ChiTiet ctct on ctct.iID_MucLucNganSach = mlns.iID_MLNS
	join
	(select * from BH_KHC_K
		where iNamLamViec = @NamLamViec
		and sSoChungTu in ((SELECT * FROM f_split(@SoChungTu)))) ct on ctct.iID_KHC_K = ct.iID_BH_KHC_K
	--join BH_DM_MucLucNganSach mlns on ctct.iID_MucLucNganSach = mlns.iID_MLNS
	------------------
	union
	select
		ct.iID_MaDonVi IIdMaDonVi,
		mlns.sXauNoiMa,
		mlns.iID_MLNS IID_MucLucNganSach,
		ctct.fTienKeHoachThucHienNamNay
	from
	 BH_DM_MucLucNganSach mlns 
	left join
	BH_KHC_KCB_ChiTiet ctct on ctct.iID_MucLucNganSach = mlns.iID_MLNS
	join
	(select * from BH_KHC_KCB
		where iNamLamViec = @NamLamViec
		and sSoChungTu in ((SELECT * FROM f_split(@SoChungTu)))) ct on ctct.iID_KHC_KCB = ct.iID_BH_KHC_KCB
	
	----------------
	union
	select
		ct.iID_MaDonVi IIdMaDonVi,
		mlns.sXauNoiMa,
		mlns.iID_MLNS IID_MucLucNganSach,
		ctct.fTienKeHoachThucHienNamNay
	from
	 BH_DM_MucLucNganSach mlns 
	left join BH_KHC_KinhPhiQuanLy_ChiTiet ctct on ctct.iID_MucLucNganSach = mlns.iID_MLNS
	join
	(select * from BH_KHC_KinhPhiQuanLy
		where iNamLamViec = @NamLamViec
		and sSoChungTu in ((SELECT * FROM f_split(@SoChungTu)))) ct on ctct.iID_KHC_KinhPhiQuanLy = ct.iID_BH_KHC_KinhPhiQuanLy
	
	select dm.* , tbl.fTienKeHoachThucHienNamNay, tbl.IIdMaDonVi from BH_DM_MucLucNganSach  dm
	left join 
	#temp tbl on dm.iID_MLNS=tbl.IID_MucLucNganSach
	where iNamLamViec=@NamLamViec and sLNS in (select * from splitstring(@SLNS))
	order by dm.sXauNoiMa 

	drop table #temp

END
;


--select * from BH_DM_MucLucNganSach

--where sLNS=9010001 and iNamLamViec=2023
--order by sXauNoiMa
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]    Script Date: 1/10/2024 5:45:32 PM ******/
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
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs in (select * from splitstring(@Lns))
				and danhmuc.iTrangThai=1


	---Lấy danh sách chi tiết
		select	
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sNoiDung,
			Sum(qtcn_ct.fTien_DuToanNamTruocChuyenSang) as fTien_DuToanNamTruocChuyenSang,
			Sum(qtcn_ct.fTien_DuToanGiaoNamNay) as fTien_DuToanGiaoNamNay,
			Sum(qtcn_ct.fTien_TongDuToanDuocGiao) as fTien_TongDuToanDuocGiao,
			Sum(qtcn_ct.fTien_ThucChi) as fTien_ThucChi,

			CASE WHEN ISNULL(Sum(qtcn_ct.fTien_TongDuToanDuocGiao),0) >  ISNULL(Sum(qtcn_ct.fTien_ThucChi),0) THEN 
			ISNULL(Sum(qtcn_ct.fTien_TongDuToanDuocGiao),0) -  ISNULL(Sum(qtcn_ct.fTien_ThucChi),0) ELSE 0 END fTienThua,

			CASE WHEN ISNULL(Sum(qtcn_ct.fTien_TongDuToanDuocGiao),0) <  ISNULL(Sum(qtcn_ct.fTien_ThucChi),0) THEN 
			ISNULL(Sum(qtcn_ct.fTien_ThucChi),0)- ISNULL(Sum(qtcn_ct.fTien_TongDuToanDuocGiao),0)  ELSE 0 END fTienThieu,
			--Sum(qtcn_ct.fTien_TongDuToanDuocGiao) - Sum(qtcn_ct.fTien_ThucChi)  as fTienThua,
			--Sum(qtcn_ct.fTien_ThucChi) - Sum(qtcn_ct.fTien_TongDuToanDuocGiao) as fTienThieu,
			CASE WHEN ISNULL(Sum(qtcn_ct.fTien_ThucChi),0) >0 THEN 
			ISNULL(Sum(qtcn_ct.fTien_ThucChi),0) / ISNULL(Sum(qtcn_ct.fTien_TongDuToanDuocGiao),0)  ELSE 0 END fTiLeThucHienTrenDuToan
			--Sum(qtcn_ct.fTiLeThucHienTrenDuToan) as fTiLeThucHienTrenDuToan

		into #tbl_qtcn_chitiet
		from BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet as  qtcn_ct
		inner join  BH_QTC_Nam_KCB_QuanYDonVi as qtcn on qtcn_ct.iID_QTC_Nam_KCB_QuanYDonVi = qtcn.ID_QTC_Nam_KCB_QuanYDonVi
		where qtcn.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
		and qtcn.iNamLamViec = @INamLamViec
		--and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_phulucquyettoannam_KCB]    Script Date: 1/10/2024 5:45:32 PM ******/
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
		0 as fChiTieuNamTruoc,
		--sum(chungtu_ct.fTien_TongDuToanDuocGiao) as fChiTieuNamTruoc,
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
	where chungtu.iNamLamViec = @INamLamViec -1
	and chungtu.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi))
	--and ((@IsTongHop = 0 and chungtu.bDaTongHop = 0 and chungtu.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  chungtu.sDSSoChungTuTongHop is not null))
	group by donvi.sTenDonVi

	---Lấy quyết toán năm trước

	select
		donvi.sTenDonVi,
		sum(chungtu_ct.fTien_DuToanNamTruocChuyenSang) fChiTieuNamTruoc,
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
	where chungtu.iNamLamViec = @INamLamViec
	and chungtu.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi))
	--and ((@IsTongHop = 0 and chungtu.bDaTongHop = 0 and chungtu.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  chungtu.sDSSoChungTuTongHop is not null))
	group by donvi.sTenDonVi

	--- Kết quả trả về

	select
		ROW_NUMBER () OVER (ORDER BY sTenDonVi) sTT,
		sTenDonVi,
		sum(fChiTieuNamTruoc) as fChiTieuNamTruoc,
		sum(fChiTieuNamNay) as fChiTieuNamNay,
		isnull(sum(fChiTieuNamTruoc) ,0) + isnull(sum(fChiTieuNamNay) ,0) as fTongCong,
		sum(fTienQuyetToan) as fTienQuyetToan,
		--isnull(sum(fChiTieuNamTruoc) ,0) + isnull(sum(fChiTieuNamNay) ,0) - isnull(sum(fTienQuyetToan) ,0) as fTienThua,
		--isnull(sum(fTienQuyetToan) ,0) - isnull(sum(fChiTieuNamTruoc) ,0) + isnull(sum(fChiTieuNamNay) ,0) as fTienThieu
		CASE WHEN ( isnull(sum(fChiTieuNamTruoc) ,0) + isnull(sum(fChiTieuNamNay) ,0)) > isnull(sum(fTienQuyetToan) ,0) THEN 
		isnull(sum(fChiTieuNamTruoc) ,0) + isnull(sum(fChiTieuNamNay) ,0) - isnull(sum(fTienQuyetToan) ,0) ELSE 0  END  fTienThua,
		CASE WHEN  (isnull(sum(fChiTieuNamTruoc) ,0) + isnull(sum(fChiTieuNamNay) ,0)) < isnull(sum(fTienQuyetToan) ,0) THEN 
		isnull(sum(fTienQuyetToan) ,0) - (isnull(sum(fChiTieuNamTruoc) ,0) + isnull(sum(fChiTieuNamNay) ,0)) ELSE 0  END  fTienThieu

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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_Bhxh_slns]    Script Date: 1/10/2024 5:45:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_Bhxh_slns]
@iDNdtctg uniqueidentifier,
@sLns nvarchar(max),
@NamLamViec int,
@IIDDonVi nvarchar(50)

AS
BEGIN

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
	A.bHangCha as isHangCha,
	B.iID_DTC_DuToanChiTrenGiao,
	A.iID_MLNS AS iID_MucLucNganSach,
	B.fTienKeHoachThucHienNamNay as fTienTuChi,
	A.iNamlamViec,
	A.sCPChiTietToi,
	A.sDuToanChiTietToi,
	@IIDDonVi as iID_MaDonVi
	FROM BH_DM_MucLucNganSach AS A
	LEFT JOIN (select	
		@iDNdtctg as iID_DTC_DuToanChiTrenGiao
		, ctct.iID_MucLucNganSach
		, ctct.fTienKeHoachThucHienNamNay
		from BH_KHC_CheDoBHXH_ChiTiet ctct
left join BH_KHC_CheDoBHXH ct on ctct.iID_KHC_CheDoBHXH=ct.ID
where (ct.iLoaiTongHop=2 ) and ct.iID_MaDonVi=@IIDDonVi
		and ct.iNamLamViec=@NamLamViec
		and ct.bIsKhoa=1) AS B
	ON B.iID_MucLucNganSach = A.iID_MLNS
	WHERE   A.sLNS IN (SELECT * FROM f_split(@sLns))
		AND A.iNamlamViec=@NamLamViec
	order by A.sXauNoiMa
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt]    Script Date: 1/10/2024 5:45:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt]
@iDNdtctg uniqueidentifier

AS
BEGIN
	SELECT 
	B.ID,
	B.sLNS,
	B.iID_DTC_DuToanChiTrenGiao,
	B.iID_MucLucNganSach,
	B.fTongTien,
	B.fTienHienVat,
	B.fTienTuChi
	FROM BH_DTC_DuToanChiTrenGiao_ChiTiet AS B
	WHERE B.iID_DTC_DuToanChiTrenGiao = @iDNdtctg
	
	END;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns]    Script Date: 1/10/2024 5:45:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns]
@iDNdtctg uniqueidentifier,
@sLns nvarchar(max),
@NamLamViec int,
@IIDDonVi nvarchar(50)

AS
BEGIN

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
	A.bHangCha as isHangCha,
	B.ID,
	B.iID_DTC_DuToanChiTrenGiao,
	A.iID_MLNS AS iID_MucLucNganSach,
	B.fTongTien,
	B.fTienHienVat,
	B.fTienTuChi,
	A.sCPChiTietToi,
	A.sDuToanChiTietToi,
	A.iNamlamViec,
	@IIDDonVi as iID_MaDonVi,
	 B.dNgaySua,
	 B.dNgayTao,
	 B.sNguoiSua,
	 B.sNguoiTao
	FROM BH_DM_MucLucNganSach AS A
	LEFT JOIN ( SELECT ctct.ID
					, ctct.iID_DTC_DuToanChiTrenGiao
					, ctct.iID_MucLucNganSach
					,ctct.sLNS
					, ctct.sM
					, ctct.sTM
					, ctct.sTTM
					, ctct.sNG
					, ctct.sNoiDung
					, ctct.fTongTien
					, ctct.fTienTuChi
					, ctct.fTienHienVat
					, ctct.dNgaySua
					, ctct.dNgayTao
					, ctct.sNguoiSua
					, ctct.sNguoiTao
					, CT.iNamLamViec
					,CT.iID_MaDonVi 
					FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
				LEFT JOIN BH_DTC_DuToanChiTrenGiao CT ON ctct.iID_DTC_DuToanChiTrenGiao=CT.ID 
				WHERE ctct.iID_DTC_DuToanChiTrenGiao = @iDNdtctg
					 and ct.iID_MaDonVi=@IIDDonVi 
					 And CT.iNamLamViec=@NamLamViec) AS B
	ON B.iID_MucLucNganSach = A.iID_MLNS
	WHERE   A.sLNS IN (SELECT * FROM f_split(@sLns))
		AND A.iNamlamViec=@NamLamViec
	order by A.sXauNoiMa
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_kcbqy_slns]    Script Date: 1/10/2024 5:45:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_kcbqy_slns]
@iDNdtctg uniqueidentifier,
@sLns nvarchar(max),
@NamLamViec int,
@IIDDonVi nvarchar(50)

AS
BEGIN

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
	A.bHangCha as isHangCha,
	B.iID_DTC_DuToanChiTrenGiao,
	A.iID_MLNS AS iID_MucLucNganSach,
	B.fTienKeHoachThucHienNamNay as fTienTuChi,
	A.iNamlamViec,
	A.sCPChiTietToi,
	A.sDuToanChiTietToi,
	@IIDDonVi as iID_MaDonVi
	FROM BH_DM_MucLucNganSach AS A
	LEFT JOIN (select	
		@iDNdtctg as iID_DTC_DuToanChiTrenGiao
		, ctct.iID_MucLucNganSach
		, ctct.fTienKeHoachThucHienNamNay
		from BH_KHC_KCB_ChiTiet ctct
left join BH_KHC_KCB ct on ctct.iID_KHC_KCB=ct.iID_BH_KHC_KCB
where (ct.iLoaiTongHop=2 ) and ct.iID_MaDonVi=@IIDDonVi
		and ct.iNamLamViec=@NamLamViec
		and ct.bIsKhoa=1) AS B
	ON B.iID_MucLucNganSach = A.iID_MLNS
	WHERE   A.sLNS IN (SELECT * FROM f_split(@sLns))
		AND A.iNamlamViec=@NamLamViec
	order by A.sXauNoiMa
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_khac_slns]    Script Date: 1/10/2024 5:45:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_khac_slns]
@iDNdtctg uniqueidentifier,
@IDLoaiChi uniqueidentifier,
@sLns nvarchar(max),
@NamLamViec int,
@IIDDonVi nvarchar(50)

AS
BEGIN

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
	A.bHangCha as isHangCha,
	B.iID_DTC_DuToanChiTrenGiao,
	A.iID_MLNS AS iID_MucLucNganSach,
	B.fTienKeHoachThucHienNamNay as fTienTuChi,
	A.iNamlamViec,
	A.sCPChiTietToi,
	A.sDuToanChiTietToi,
	@IIDDonVi as iID_MaDonVi
	FROM BH_DM_MucLucNganSach AS A
	LEFT JOIN (select	
		@iDNdtctg as iID_DTC_DuToanChiTrenGiao
		, ctct.iID_MucLucNganSach
		, ctct.fTienKeHoachThucHienNamNay
		from BH_KHC_K_ChiTiet ctct
left join BH_KHC_K ct on ctct.iID_KHC_K=ct.iID_BH_KHC_K
where (ct.iLoaiTongHop=2 ) and ct.iID_MaDonVi=@IIDDonVi
		and ct.iIDLoaiChi=@IDLoaiChi
		and ct.iNamLamViec=@NamLamViec
		and ct.bIsKhoa=1) AS B
	ON B.iID_MucLucNganSach = A.iID_MLNS
	WHERE   A.sLNS IN (SELECT * FROM f_split(@sLns))
		AND A.iNamlamViec=@NamLamViec
	order by A.sXauNoiMa
END

select * from BH_KHC_K
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_kpql_slns]    Script Date: 1/10/2024 5:45:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_kpql_slns]
@iDNdtctg uniqueidentifier,
@sLns nvarchar(max),
@NamLamViec int,
@IIDDonVi nvarchar(50)

AS
BEGIN

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
	A.bHangCha as isHangCha,
	B.iID_DTC_DuToanChiTrenGiao,
	A.iID_MLNS AS iID_MucLucNganSach,
	B.fTienKeHoachThucHienNamNay as fTienTuChi,
	A.sCPChiTietToi,
	A.sDuToanChiTietToi,
	A.iNamlamViec,
	@IIDDonVi as iID_MaDonVi
	FROM BH_DM_MucLucNganSach AS A
	LEFT JOIN (select	
		@iDNdtctg as iID_DTC_DuToanChiTrenGiao
		, ctct.iID_MucLucNganSach
		, ctct.fTienKeHoachThucHienNamNay
		from BH_KHC_KinhPhiQuanLy_ChiTiet ctct
left join BH_KHC_KinhPhiQuanLy ct on ctct.iID_KHC_KinhPhiQuanLy=ct.iID_BH_KHC_KinhPhiQuanLy
where (ct.iLoaiTongHop=2 ) and ct.iID_MaDonVi=@IIDDonVi
		and ct.iNamLamViec=@NamLamViec
		and ct.bIsKhoa=1) AS B
	ON B.iID_MucLucNganSach = A.iID_MLNS
	WHERE   A.sLNS IN (SELECT * FROM f_split(@sLns))
		AND A.iNamlamViec=@NamLamViec
	order by A.sXauNoiMa
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_getTienthuchien6thang]    Script Date: 1/10/2024 5:45:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_dieuchinh_getTienthuchien6thang]
	@NamLamViec int,
	@IdDonVi nvarchar(100),
	@IID_LoaiChi nvarchar(100),
	@SMaLoaiChi nvarchar(50),
	@SLNS nvarchar(100),
	@DNgayChungTu datetime
AS
BEGIN
		Create table #TempData6thang
		(
			iID_MaDonVi nvarchar(50),
			iID_MucLucNganSach uniqueidentifier,
			sM nvarchar(50),
			sTM nvarchar(50),
			sNoiDung nvarchar(max),
			iNamLamViec int ,
			fTienThucHien06ThangDauNam float
		);

		---- Get data 6 thang tu qtc quy 
		---- Data 6 thang Chi các chế độ BHXH
		 IF(@SLNS ='9010001,9010002')
		 	INSERT INTO #TempData6thang(ctct.iID_MaDonVi,ctct.iID_MucLucNganSach,sM,sTM,ctct.sNoiDung,ctct.iNamLamViec,ctct.fTienThucHien06ThangDauNam)
		   SELECT 
					ctct.iIDMaDonVi,
					ctct.iID_MucLucNganSach,
					'' sM,
					'' sTM,
					ctct.sLoaiTroCap sNoiDung,
					ctct.iNamLamViec,
					SUM(ctct.fTongTien_DeNghi) as fTienThucHien06ThangDauNam
				FROM 
				BH_QTC_Quy_CheDoBHXH ct
				LEFT JOIN BH_QTC_Quy_CheDoBHXH_ChiTiet ctct ON ct.ID_QTC_Quy_CheDoBHXH=ctct.iID_QTC_Quy_CheDoBHXH
				WHERE ct.iNamChungTu=@NamLamViec
				AND ctct.iIDMaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND ct.bIsKhoa=1
				AND	ct.iQuyChungTu<=2
				AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
				GROUP BY  iIDMaDonVi,iID_MucLucNganSach,sLoaiTroCap,iNamLamViec;
		 ---- Data 6 thang Chi kinh phí quản lý BHXH, BHYT
		ELSE IF(@SLNS='9010003')
		  BEGIN
			INSERT INTO #TempData6thang(ctct.iID_MaDonVi,ctct.iID_MucLucNganSach,ctct.sM,ctct.sTM,ctct.sNoiDung,ctct.iNamLamViec,ctct.fTienThucHien06ThangDauNam)
		   SELECT 
					ctct.iIDMaDonVi,
					ctct.iID_MucLucNganSach,
					'' sM,
					'' sTM,
					ctct.sNoiDung,
					ctct.iNamLamViec,
					SUM(ctct.fTienThucChi) fTienThucHien06ThangDauNam
				FROM 
				BH_QTC_Quy_KinhPhiQuanLy ct
				LEFT JOIN BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ctct ON ct.ID_QTC_Quy_KinhPhiQuanLy=ctct.iID_QTC_Quy_KinhPhiQuanLy
				WHERE ct.iNamChungTu=@NamLamViec
				AND ct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND ct.bIsKhoa=1
				AND	ct.iQuyChungTu<=2
				AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
				GROUP BY  iIDMaDonVi,iID_MucLucNganSach,sNoiDung,iNamLamViec;
		  END 
		  ---- Data 6 thang Chi kinh phí KCB tại Trường Sa 
		ELSE IF(@SLNS='905,9050001,9050002')
		  BEGIN
			INSERT INTO #TempData6thang(ctct.iID_MaDonVi,ctct.iID_MucLucNganSach,ctct.sM,ctct.sTM,ctct.sNoiDung,ctct.iNamLamViec,ctct.fTienThucHien06ThangDauNam)
		   SELECT 
					ctct.iIDMaDonVi,
					ctct.iID_MucLucNganSach,
					'' sM,
					'' sTM,
					ctct.sNoiDung,
					ctct.iNamLamViec,
					SUM(ctct.fTienThucChi) fTienThucHien06ThangDauNam
				FROM 
				BH_QTC_Quy_KinhPhiQuanLy ct
				LEFT JOIN BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ctct ON ct.ID_QTC_Quy_KinhPhiQuanLy=ctct.iID_QTC_Quy_KinhPhiQuanLy
				WHERE ct.iNamChungTu=@NamLamViec
				AND ct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND ct.bIsKhoa=1
				AND	ct.iQuyChungTu<=2
				AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
				GROUP BY  iIDMaDonVi,iID_MucLucNganSach,sNoiDung,iNamLamViec;
		  END 

		  ---- Data 6 thang Chi kinh phí KCB tại quân y đơn vị 
		ELSE IF(@SLNS='9010004,9010005')
		  BEGIN
			INSERT INTO #TempData6thang(ctct.iID_MaDonVi,ctct.iID_MucLucNganSach,ctct.sM,ctct.sTM,ctct.sNoiDung,ctct.iNamLamViec,ctct.fTienThucHien06ThangDauNam)
		   SELECT 
					ctct.iID_MaDonVi,
					ctct.iID_MucLucNganSach,
					'' sM,
					'' sTM,
					ctct.sNoiDung,
					ctct.iNamLamViec,
					SUM(ctct.fTienThucChi) fTienThucHien06ThangDauNam
				FROM 
				BH_QTC_Quy_KCB ct
				LEFT JOIN BH_QTC_Quy_KCB_ChiTiet ctct ON ct.ID_QTC_Quy_KCB=ctct.iID_QTC_Quy_KCB
				WHERE ct.iNamChungTu=@NamLamViec
				AND ct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND ct.bIsKhoa=1
				AND	ct.iQuyChungTu<=2
				AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
				GROUP BY  ctct.iID_MaDonVi,iID_MucLucNganSach,sNoiDung,iNamLamViec;
		  END 

		   ---- Data 6 thang Chi từ nguồn kết dư Quỹ KCB BHYT quân nhân
		ELSE IF(@SLNS='9010008')
		  BEGIN
			INSERT INTO #TempData6thang(ctct.iID_MaDonVi,ctct.iID_MucLucNganSach,ctct.sM,ctct.sTM,ctct.sNoiDung,ctct.iNamLamViec,ctct.fTienThucHien06ThangDauNam)
		   SELECT 
					ctct.iIDMaDonVi,
					ctct.iID_MucLucNganSach,
					'' sM,
					'' sTM,
					ctct.sNoiDung,
					ctct.iNamLamViec,
					SUM(ctct.fTienThucChi) fTienThucHien06ThangDauNam
				FROM 
				BH_QTC_Quy_KPK ct
				LEFT JOIN BH_QTC_Quy_KPK_ChiTiet ctct ON ct.ID_QTC_Quy_KPK=ctct.iID_QTC_Quy_KPK
				WHERE ct.iNamChungTu=@NamLamViec
				AND ct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND ct.iID_LoaiChi=@IID_LoaiChi
				AND ct.bIsKhoa=1
				AND	ct.iQuyChungTu<=2
				AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
				GROUP BY  iIDMaDonVi,iID_MucLucNganSach,sNoiDung,iNamLamViec;
		  END 

		  ---- Data 6 thang Chi hỗ trợ BHTN 
		ELSE IF(@SLNS='9010009')
		  BEGIN
			INSERT INTO #TempData6thang(ctct.iID_MaDonVi,ctct.iID_MucLucNganSach,ctct.sM,ctct.sTM,ctct.sNoiDung,ctct.iNamLamViec,ctct.fTienThucHien06ThangDauNam)
		   SELECT
					ctct.iIDMaDonVi,
					ctct.iID_MucLucNganSach,
					'' sM,
					'' sTM,
					ctct.sNoiDung,
					ctct.iNamLamViec,
					SUM(ctct.fTienThucChi) fTienThucHien06ThangDauNam
				FROM 
				BH_QTC_Quy_KPK ct
				LEFT JOIN BH_QTC_Quy_KPK_ChiTiet ctct ON ct.ID_QTC_Quy_KPK=ctct.iID_QTC_Quy_KPK
				WHERE ct.iNamChungTu=@NamLamViec
				AND ct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND ct.iID_LoaiChi=@IID_LoaiChi
				AND ct.bIsKhoa=1
				AND	ct.iQuyChungTu<=2
				AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
				GROUP BY  iIDMaDonVi,iID_MucLucNganSach,sNoiDung,iNamLamViec;
		  END 

		  ---- Data 6 thang Kinh phí mua sắm trang thiết bị y tế 
		   ELSE
		  BEGIN
			INSERT INTO #TempData6thang(ctct.iID_MaDonVi,ctct.iID_MucLucNganSach,ctct.sM,ctct.sTM,ctct.sNoiDung,ctct.iNamLamViec,ctct.fTienThucHien06ThangDauNam)
		   SELECT 
					ctct.iIDMaDonVi,
					ctct.iID_MucLucNganSach,
					'' sM,
					'' sTM,
					ctct.sNoiDung,
					ctct.iNamLamViec,
					SUM(ctct.fTienThucChi) fTienThucHien06ThangDauNam
				FROM 
				BH_QTC_Quy_KPK ct
				LEFT JOIN BH_QTC_Quy_KPK_ChiTiet ctct ON ct.ID_QTC_Quy_KPK=ctct.iID_QTC_Quy_KPK
				WHERE ct.iNamChungTu=@NamLamViec
				AND ct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND ct.iID_LoaiChi=@IID_LoaiChi
				AND ct.bIsKhoa=1
				AND	ct.iQuyChungTu<=2
				AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
				GROUP BY  iIDMaDonVi,iID_MucLucNganSach,sNoiDung,iNamLamViec;
		  END 

		SELECT dm.iID_MLNS_Cha as IdParent,
			   dm.iID_MLNS as IID_MucLucNganSach,
			   tbl.fTienThucHien06ThangDauNam,
			   dm.bHangCha as IsHangCha,
			   dm.sCPChiTietToi as SCPChiTietToi,
			   dm.sDuToanChiTietToi as SDuToanChiTietToi,
			   dm.sMoTa SNoiDung
			FROM BH_DM_MucLucNganSach dm
			LEFT JOIN #TempData6thang  tbl 
			on tbl.iID_MucLucNganSach=dm.iID_MLNS
			where dm.iNamLamViec=@NamLamViec and dm.sLNS in (select * from splitstring(@SLNS))
			order by dm.sXauNoiMa

		 -- SELECT tbl.*,dm.iID_MLNS_Cha IdParent, dm.bHangCha IsHangCha  FROM #TempData6thang  tbl 
			--LEFT JOIN BH_DM_MucLucNganSach dm on tbl.iID_MucLucNganSach=dm.iID_MLNS
			--where dm.iNamLamViec=@NamLamViec and dm.sLNS in (select * from splitstring(@SLNS))
		  drop table #TempData6thang
END
;
;
;


GO
/****** Object:  StoredProcedure [dbo].[sp_dt_phanbo_dieuchinh_chi_tiet]    Script Date: 1/10/2024 5:45:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_phanbo_dieuchinh_chi_tiet]
	@NamLamViec int,
	@IdDonVi nvarchar(100),
	@loaiDanhMucCapChi nvarchar(100),
	@SLNS nvarchar(100),
	@ngayChungTu date
AS
BEGIN
SELECT 
		ct.ID ,
		ct.iID_DTC_PhanBoDuToanChi ,
		ct.iID_DonVi,
		ct.iID_MucLucNganSach,
		ct.iID_DTC_DuToanChiTrenGiao,
		ct.sLNS,
		ct.sM,
		ct.sTM,
		ct.sTTM,
		ct.sNG,
		ct.sNoiDung,
		ct.sXauNoiMa,
		ct.iID_MaDonVi,
		ct.iNamLamViec,
		ct.iID_LoaiCap,
		ISNULL(ct.fTienTuChi, 0) as FTienTuChi
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
				AND bh.iID_LoaiDanhMucChi=@loaiDanhMucCapChi
				AND bh.iNamChungTu=@NamLamViec
		) ct;

		SELECT dm.iID_MLNS_Cha as IdParent,
			   dm.iID_MLNS as iID_MLNS,
			   tbl.FTienTuChi as fTienTuChi,
			   dm.bHangCha as IsHangCha,
			   dm.sCPChiTietToi as SCPChiTietToi,
			   dm.sDuToanChiTietToi as SDuToanChiTietToi,
			   dm.sMoTa SNoiDung
			FROM BH_DM_MucLucNganSach dm
			LEFT JOIN #temp1  tbl 
			on dm.iID_MLNS=tbl.iID_MucLucNganSach
			where dm.iNamLamViec=@NamLamViec 
			and dm.sLNS in (select * from splitstring(@SLNS))
			order by dm.sXauNoiMa

			drop table #temp1

END
;
;
;
GO
