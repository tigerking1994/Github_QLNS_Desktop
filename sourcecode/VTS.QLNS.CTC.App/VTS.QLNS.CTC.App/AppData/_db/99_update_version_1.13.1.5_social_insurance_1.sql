DELETE FROM [dbo].[DM_CoSoYTe]
GO
INSERT [dbo].[DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [sTenCoSoYTe], [iNamLamViec]) VALUES (N'c5e79875-268b-4582-be9c-819758e2c033', N'002', N'Cơ sở Y tế 2', 2023)
GO
INSERT [dbo].[DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [sTenCoSoYTe], [iNamLamViec]) VALUES (N'8250d814-b3f6-42f5-becb-95e72f11161c', N'003', N'Cơ sở Y tế 3', 2023)
GO
INSERT [dbo].[DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [sTenCoSoYTe], [iNamLamViec]) VALUES (N'7e24595b-3065-4c3d-be66-9aeff1b42bf8', N'001', N'Cơ sở Y tế 1', 2023)
GO
------------
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh]    Script Date: 9/12/2023 5:02:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_dtt_bhxh_export_phan_bo_du_toan_chi_tiet]    Script Date: 9/12/2023 5:02:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dtt_bhxh_export_phan_bo_du_toan_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dtt_bhxh_export_phan_bo_du_toan_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dieu_chinh_dtt_get_lns]    Script Date: 9/12/2023 5:02:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dieu_chinh_dtt_get_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dieu_chinh_dtt_get_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet_dieu_chinh]    Script Date: 9/12/2023 5:02:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet_dieu_chinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet_dieu_chinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]    Script Date: 9/12/2023 5:02:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]    Script Date: 9/12/2023 5:02:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]
	@ChungTuId NVARCHAR(255),
	@LNS NVARCHAR(MAX),
	@IdDonVi NVARCHAR(MAX),
	@NamLamViec int,
	@UserName NVARCHAR(100)
AS
BEGIN
	---Lấy danh sách mục lục ngân sách
	select 
	NEWID()  as iID_CTDuToan_Nhan,
	CAST(NULL AS VARCHAR(50)) as iID_DTT_BHXH_ChungTu_ChiTiet,
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
	BH_DM_MucLucNganSach.sMoTa,
	0 as fBHXHNLD,
	0 as fBHXHNLDTruocDieuChinh,
	0 as fBHXHNSD,
	0 as fBHXHNSDTruocDieuChinh,
	0 as fBHYTNLD,
	0 as fBHYTNLDTruocDieuChinh,
	0 as fBHYTNSD,
	0 as fBHYTNSDTruocDieuChinh,
	0 as fBHTNNLD,
	0 as fBHTNNLDTruocDieuChinh,
	0 as fBHTNNSD,
	0 as fBHTNNSDTruocDieuChinh,
	1 as iRowType,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	'' as sSoQuyetDinh,
	BH_DM_MucLucNganSach.bHangCha as bHangCha,
	0 as IsRemainRow
	into tblMucLucNganSach
	from BH_DM_MucLucNganSach 
	where sLNS  IN (SELECT * FROM f_split(@LNS)) and iNamLamViec = @NamLamViec;

	---Lấy danh sách đơn vị được phân bổ
	select * 
	into  tblDonVi
	from DonVi where iNamLamViec = @NamLamViec and iID_MaDonVi  IN (SELECT * FROM f_split(@IdDonVi));
	
	--lấy danh sách dự toán nhận phân bổ
	select *
	into tblChungTuNhanPhanBo
	from BH_DTT_Nhan_Phan_Bo_Map
	where iID_CTDuToan_PhanBo = @ChungTuId

	---Lấy danh sách chi tiết dự toán toán nhận phân bổ
	select 
			nhanpb_chitiet.iID_DTT_BHXH_ChiTiet,
			nhanpb_chitiet.iID_DTT_BHXH as iID_CTDuToan_Nhan,
			'' as iID_MaDonVi,
			nhanpb_chitiet.iID_MLNS,
			nhanpb_chitiet.fThu_BHXH_NLD fThuBHXHNLD,
			nhanpb_chitiet.fThu_BHXH_NSD fThuBHXHNSD,
			nhanpb_chitiet.fThu_BHYT_NLD fThuBHYTNLD,
			nhanpb_chitiet.fThu_BHYT_NSD fThuBHYTNSD,
			nhanpb_chitiet.fThu_BHTN_NLD fThuBHTNNLD,
			nhanpb_chitiet.fThu_BHTN_NSD fThuBHTNNSD,
			nhanpb.sSoQuyetDinh
	into tblChiTietDuToanNhan
	from BH_DTT_BHXH_ChungTu_ChiTiet as nhanpb_chitiet
	inner join BH_DTT_BHXH_ChungTu as nhanpb on nhanpb.iID_DTT_BHXH = nhanpb_chitiet.iID_DTT_BHXH
	where nhanpb.iID_DTT_BHXH in (select iID_CTDuToan_Nhan from tblChungTuNhanPhanBo)

	---Lấy danh sách chi tiết nhận phân bổ có thông tin mục lục ngân sách
	select 
		tblChiTietDuToanNhan.iID_CTDuToan_Nhan,
		tblChiTietDuToanNhan.iID_MLNS,
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
		tblMucLucNganSach.sMoTa,
		tblChiTietDuToanNhan.sSoQuyetDinh,
		tblChiTietDuToanNhan.fThuBHXHNLD,
		tblChiTietDuToanNhan.fThuBHXHNSD,
		tblChiTietDuToanNhan.fThuBHYTNLD,
		tblChiTietDuToanNhan.fThuBHYTNSD,
		tblChiTietDuToanNhan.fThuBHTNNLD,
		tblChiTietDuToanNhan.fThuBHTNNSD,
		3 as iRowType
	into tbl_tblChiTietDuToanNhan_MucLuc
	from tblMucLucNganSach
	inner join tblChiTietDuToanNhan on tblMucLucNganSach.iID_MLNS = tblChiTietDuToanNhan.iID_MLNS


	---Hiển thị danh sách chi tiết nhận phân bổ theo đơn vị được chọn phân bổ
	select  tbl_tblChiTietDuToanNhan_MucLuc.*, tblDonVi.iID_MaDonVi, concat(tblDonVi.iID_MaDonVi, '-', tblDonVi.sTenDonVi) as sTenDonVi, 0 as bHangCha
	into #temp
	from tbl_tblChiTietDuToanNhan_MucLuc cross join tblDonVi 

	---Map với bảng BH_DTT_BHXH_PhanBo_ChungTuChiTiet để lấy thông tin đã được phân bổ
	select 
		#temp.iID_CTDuToan_Nhan, 
		chitiet_phanbo.iID_DTT_BHXH_ChungTu_ChiTiet,
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
		#temp.sMoTa as sMoTa,
		chitiet_phanbo.fBHXH_NLD as fBHXHNLD,
		#temp.fThuBHXHNLD as fBHXHNLDTruocDieuChinh,
		chitiet_phanbo.fBHXH_NSD as fBHXHNSD,
		#temp.fThuBHXHNSD as fBHXHNSDTruocDieuChinh,
		chitiet_phanbo.fBHYT_NLD as fBHYTNLD,
		#temp.fThuBHYTNLD as fBHYTNLDTruocDieuChinh,
		chitiet_phanbo.fBHYT_NSD as fBHYTNSD,
		#temp.fThuBHYTNSD as fBHYTNSDTruocDieuChinh,
		chitiet_phanbo.fBHTN_NLD as fBHTNNLD,
		#temp.fThuBHTNNLD as fBHTNNLDTruocDieuChinh,
		chitiet_phanbo.fBHTN_NSD as fBHTNNSD,
		#temp.fThuBHTNNSD as fBHTNNSDTruocDieuChinh,
		3 as iRowType,
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
			from BH_DTT_BHXH_PhanBo_ChungTuChiTiet 
			where iID_DTT_BHXH_ChungTu = @ChungTuId
		) as chitiet_phanbo
		on chitiet_phanbo.iID_CTDuToan_Nhan = #temp.iID_CTDuToan_Nhan and chitiet_phanbo.iID_MaDonVi = #temp.iID_MaDonVi and chitiet_phanbo.iID_MLNS = #temp.iID_MLNS


	-----Lấy danh sách số chưa phân bổ
	select 
	npb.iID_DTT_BHXH as iID_CTDuToan_Nhan,
	CAST(NULL AS VARCHAR(50)) as iID_DTT_BHXH_ChungTu_ChiTiet,
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
	N'Số Chưa Phân Bổ' as sMoTa,
	chitiet_chuaphanbo.fBHXHNLD as fBHXHNLD,
	chitiet_chuaphanbo.fBHXHNLD as fBHXHNLDTruocDieuChinh,
	chitiet_chuaphanbo.fBHXHNSD as fBHXHNSD,
	chitiet_chuaphanbo.fBHXHNSD as fBHXHNSDTruocDieuChinh,
	chitiet_chuaphanbo.fBHYTNLD as fBHYTNLD,
	chitiet_chuaphanbo.fBHYTNLD as fBHYTNLDTruocDieuChinh,
	chitiet_chuaphanbo.fBHYTNSD as fBHYTNSD,
	chitiet_chuaphanbo.fBHYTNSD as fBHYTNSDTruocDieuChinh,
	chitiet_chuaphanbo.fBHTNNLD as fBHTNNLD,
	chitiet_chuaphanbo.fBHTNNLD as fBHTNNLDTruocDieuChinh,
	chitiet_chuaphanbo.fBHTNNSD as fBHTNNSD,
	chitiet_chuaphanbo.fBHTNNSD as fBHTNNSDTruocDieuChinh,
	2 as iRowType,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	npb.sSoQuyetDinh as sSoQuyetDinh,
	1 as bHangCha,
	1 as IsRemainRow
	into tblSoChuaPhanBo
	from tblMucLucNganSach as muluc_ngansach
	inner join 
	(
		select (ISNULL(ct_npb.fThu_BHXH_NLD,0) - ISNULL(ct_pb_t.fBHXHNLD,0)) as fBHXHNLD,
				(ISNULL(ct_npb.fThu_BHXH_NSD,0) - ISNULL(ct_pb_t.fBHXHNSD,0)) as fBHXHNSD,
				(ISNULL(ct_npb.fThu_BHYT_NLD,0) - ISNULL(ct_pb_t.fBHYTNLD,0)) as fBHYTNLD,
				(ISNULL(ct_npb.fThu_BHYT_NSD,0) - ISNULL(ct_pb_t.fBHYTNSD,0)) as fBHYTNSD,
				(ISNULL(ct_npb.fThu_BHTN_NLD,0) - ISNULL(ct_pb_t.fBHTNNLD,0)) as fBHTNNLD,
				(ISNULL(ct_npb.fThu_BHTN_NSD,0) - ISNULL(ct_pb_t.fBHTNNSD,0)) as fBHTNNSD,
				ct_npb.iID_MLNS,
				ct_npb.iID_DTT_BHXH iID_CTDuToan_Nhan
				from BH_DTT_BHXH_ChungTu_ChiTiet as ct_npb
				left join
					(
						select sum(fBHXH_NLD) as fBHXHNLD,
						sum(fBHXH_NSD) as fBHXHNSD,
						sum(fBHYT_NLD) as fBHYTNLD,
						sum(fBHYT_NSD) as fBHYTNSD,
						sum(fBHTN_NLD) as fBHTNNLD,
						sum(fBHTN_NSD) as fBHTNNSD,
						iID_MLNS,
						iID_CTDuToan_Nhan
						from BH_DTT_BHXH_PhanBo_ChungTuChiTiet as ct_pb
						where ct_pb.iID_CTDuToan_Nhan in (select iID_CTDuToan_Nhan from tblChungTuNhanPhanBo)
						group by iID_MLNS, iID_CTDuToan_Nhan
					)as ct_pb_t  
					on ct_pb_t.iID_MLNS = ct_npb.iID_MLNS and ct_npb.iID_DTT_BHXH = ct_pb_t.iID_CTDuToan_Nhan) as chitiet_chuaphanbo on chitiet_chuaphanbo.iID_MLNS = muluc_ngansach.iID_MLNS
					inner join BH_DTT_BHXH_ChungTu as npb on npb.iID_DTT_BHXH = chitiet_chuaphanbo.iID_CTDuToan_Nhan
					where npb.iID_DTT_BHXH in ( select iID_CTDuToan_Nhan from tblChungTuNhanPhanBo)

	------Lấy mục lục ngân sách có dupliate các mục lục ngân sách con
	select 
	tblSoChuaPhanBo.iID_CTDuToan_Nhan as iID_CTDuToan_Nhan,
	CAST(NULL AS VARCHAR(50)) as iID_DTT_BHXH_ChungTu_ChiTiet,
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
	tblMucLucNganSach.sMoTa,
	tblSoChuaPhanBo.fBHXHNLD,
	tblSoChuaPhanBo.fBHXHNLDTruocDieuChinh,
	tblSoChuaPhanBo.fBHXHNSD,
	tblSoChuaPhanBo.fBHXHNSDTruocDieuChinh,
	tblSoChuaPhanBo.fBHYTNLD,
	tblSoChuaPhanBo.fBHYTNLDTruocDieuChinh,
	tblSoChuaPhanBo.fBHYTNSD,
	tblSoChuaPhanBo.fBHYTNSDTruocDieuChinh,
	tblSoChuaPhanBo.fBHTNNLD,
	tblSoChuaPhanBo.fBHTNNLDTruocDieuChinh,
	tblSoChuaPhanBo.fBHTNNSD,
	tblSoChuaPhanBo.fBHTNNSDTruocDieuChinh,
	case when tblSoChuaPhanBo.iRowType = 2 then 2 else tblMucLucNganSach.iRowType end iRowType,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	tblSoChuaPhanBo.sSoQuyetDinh as sSoQuyetDinh,
	case when tblSoChuaPhanBo.iRowType = 2 then 1 else tblMucLucNganSach.bHangCha end bHangCha,
	0 as IsRemainRow
	into tblMucLucNganSach_duplicate
	from tblMucLucNganSach
	left join tblSoChuaPhanBo on tblMucLucNganSach.iID_MLNS = tblSoChuaPhanBo.iID_MLNS
	order by sXauNoiMa
	
	select * from
	(
		SELECT * from tblMucLucNganSach_duplicate
		UNION ALL
		SELECT * from tblSoChuaPhanBo
		UNION ALL
		SELECT * from #temp1
	) as result
	order by result.sXauNoiMa,
		result.sSoQuyetDinh,
		result.iID_MaDonVi,
		result.iRowType,
		result.IsRemainRow


drop table tblMucLucNganSach;
drop table tblDonVi;
drop table tblChungTuNhanPhanBo;
drop table tblChiTietDuToanNhan;
drop table tbl_tblChiTietDuToanNhan_MucLuc;
drop table #temp;
drop table #temp1;
drop table tblSoChuaPhanBo;
drop table tblMucLucNganSach_duplicate;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet_dieu_chinh]    Script Date: 9/12/2023 5:02:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet_dieu_chinh]
	@ChungTuId NVARCHAR(255),
	@LNS NVARCHAR(MAX),
	@NamLamViec int,
	@UserName NVARCHAR(100)
AS
BEGIN
	---Lay danh sách muc luc ngân sách
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
	BH_DM_MucLucNganSach.sMoTa,
	NULL as iID_CTDuToan_Nhan,
	NULL as iID_DTT_BHXH_ChungTu_ChiTiet,
	NULL as iID_MaDonVi,
	NULL as sTenDonVi,
	NULL as sSoQuyetDinh,
	0 as fBHXHNLDTruocDieuChinh,
	0 as fBHXHNLD,
	0 as fBHXHNLDSauDieuChinh,
	
	0 as fBHXHNSDTruocDieuChinh,
	0 as fBHXHNSD,
	0 as fBHXHNSDtSauDieuChinh,
	
	0 as fBHYTNLDTruocDieuChinh,
	0 as fBHYTNLD,
	0 as fBHYTNLDSauDieuChinh,
	
	0 as fBHYTNSDTruocDieuChinh,
	0 as fBHYTNSD,
	0 as fBHYTNSDSauDieuChinh,
	
	0 as fBHTNNLDTruocDieuChinh,
	0 as fBHTNNLD,
	0 as fBHTNNLDSauDieuChinh,
	
	0 as fBHTNNSDTruocDieuChinh,
	0 as fBHTNNSD,
	0 as fBHTNNSDSauDieuChinh,
	bHangCha,
	0 as iRowType
	into tblMucLucNganSach
	from BH_DM_MucLucNganSach 
	where sLNS  IN (SELECT * FROM f_split(@LNS)) and iNamLamViec = @NamLamViec;

	---Lay danh sách chung tu bi dieu chinh
	select * 
	into tblChungTuBiDieuChinh
	from BH_DTT_Nhan_Phan_Bo_Map
	where iID_CTDuToan_PhanBo = @ChungTuId


	---Lay danh sách chi tiet chung tu bi dieu chinh
	select 
	dc_ct.iID_MLNS, 
	dc_ct.iID_MaDonVi,
	
	dc_ct.fBHXH_NLD as fBHXHNLD,
	dc_ct.fBHXH_NSD as fBHXHNSD,
	dc_ct.fBHYT_NLD as fBHYTNLD,
	dc_ct.fBHYT_NSD as fBHYTNSD,
	dc_ct.fBHTN_NLD as fBHTNNLD,
	dc_ct.fBHTN_NSD as fBHTNNSD,
	
	dc.iID_DTT_BHXH_PhanBo_ChungTu as iID_CTDuToan_Nhan,
	dc_ct.iID_DTT_BHXH_ChungTu_ChiTiet,
	dc.sSoQuyetDinh
	into tblChungTuBiDieuChinh_Ct 
	from BH_DTT_BHXH_PhanBo_ChungTuChiTiet as dc_ct  
	inner join BH_DTT_BHXH_PhanBo_ChungTu as dc on dc_ct.iID_DTT_BHXH_ChungTu = dc.iID_DTT_BHXH_PhanBo_ChungTu
	where dc_ct.iID_DTT_BHXH_ChungTu in ( select iID_CTDuToan_Nhan from tblChungTuBiDieuChinh)

	---Thông tin chi tiết chứng từ bị điều chỉnh 
	select 
	npb_ct.iID_MLNS,
	npb_ct.iID_MaDonVi,
	npb_ct.sSoQuyetDinh,
	npb_ct.fBHXHNLD fBHXHNLDTruocDieuChinh,
	npb_ct.fBHXHNSD fBHXHNSDTruocDieuChinh,
	npb_ct.fBHYTNLD fBHYTNLDTruocDieuChinh,
	npb_ct.fBHYTNSD fBHYTNSDTruocDieuChinh,
	npb_ct.fBHTNNLD fBHTNNLDTruocDieuChinh,
	npb_ct.fBHTNNSD fBHTNNSDTruocDieuChinh,
	npb_ct.iID_CTDuToan_Nhan,
	pb_ct.iID_DTT_BHXH_ChungTu_ChiTiet,
	pb_ct.fBHXH_NLD as fBHXHNLD,
	pb_ct.fBHXH_NSD as fBHXHNSD,
	pb_ct.fBHYT_NLD as fBHYTNLD,
	pb_ct.fBHYT_NSD as fBHYTNSD,
	pb_ct.fBHTN_NLD as fBHTNNLD,
	pb_ct.fBHTN_NSD as fBHTNNSD,
	0 as bHangCha,
	2 iRowType
	into tblThongTinChungTu_BiDieuChinh
	from tblChungTuBiDieuChinh_Ct as npb_ct
	left join ( select * from BH_DTT_BHXH_PhanBo_ChungTuChiTiet where iID_DTT_BHXH_ChungTu = @ChungTuId) as  pb_ct on npb_ct.iID_MLNS = pb_ct.iID_MLNS and npb_ct.iID_MaDonVi = pb_ct.iID_MaDonVi
	and npb_ct.iID_CTDuToan_Nhan = pb_ct.iID_CTDuToan_Nhan

	---Thong tin chi tiết chứng từ điều chỉnh thêm mới
	select 
	pb_ct.iID_MLNS,
	pb_ct.iID_MaDonVi,
	pb.sSoQuyetDinh,
	0 as fBHXHNLDTruocDieuChinh,
	0 as fBHXHNSDTruocDieuChinh,
	0 as fBHYTNLDTruocDieuChinh,
	0 as fBHYTNSDTruocDieuChinh,
	0 as fBHTNNLDTruocDieuChinh,
	0 as fBHTNNSDTruocDieuChinh,
	pb_ct.iID_CTDuToan_Nhan,
	pb_ct.iID_DTT_BHXH_ChungTu_ChiTiet,
	pb_ct.fBHXH_NLD as fBHXHNLD,
	pb_ct.fBHXH_NSD as fBHXHNSD,
	pb_ct.fBHYT_NLD as fBHYTNLD,
	pb_ct.fBHYT_NSD as fBHYTNSD,
	pb_ct.fBHTN_NLD as fBHTNNLD,
	pb_ct.fBHTN_NSD as fBHTNNSD,
	0 as bHangCha,
	2 iRowType
	into tblThongTinChungTu_DieuChinhThemMoi
	from BH_DTT_BHXH_PhanBo_ChungTuChiTiet as pb_ct
	inner join BH_DTT_BHXH_PhanBo_ChungTu as pb on pb_ct.iID_CTDuToan_Nhan = pb.iID_DTT_BHXH_PhanBo_ChungTu
	where pb_ct.iID_DTT_BHXH_ChungTu = @ChungTuId and pb_ct.iID_DTT_BHXH_ChungTu_ChiTiet not in (select iID_DTT_BHXH_ChungTu_ChiTiet from tblThongTinChungTu_BiDieuChinh where iID_DTT_BHXH_ChungTu_ChiTiet is not null)

	---Thông tin chi tiết chúng từ
	select * 
	into tblThongTinChungTu
	from
		(
			select * from tblThongTinChungTu_BiDieuChinh
			UNION ALL
			select * from tblThongTinChungTu_DieuChinhThemMoi
		) as tbl;

	--Result

	select 
	tblMucLucNganSach.iID_MLNS,
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
	tblMucLucNganSach.sMoTa,
	tblThongTinChungTu.iID_CTDuToan_Nhan,
	tblThongTinChungTu.iID_DTT_BHXH_ChungTu_ChiTiet,
	tblThongTinChungTu.iID_MaDonVi,
	CONCAT(DonVi.iID_MaDonVi, '-', DonVi.sTenDonVi) as sTenDonVi,
	tblThongTinChungTu.sSoQuyetDinh,
	
	tblThongTinChungTu.fBHXHNLDTruocDieuChinh,
	tblThongTinChungTu.fBHXHNLD,
	ISNULL(tblThongTinChungTu.fBHXHNLDTruocDieuChinh,0) + ISNULL(tblThongTinChungTu.fBHXHNLD,0) as fBHXHNLDSauDieuChinh,
	
	tblThongTinChungTu.fBHXHNSDTruocDieuChinh,
	tblThongTinChungTu.fBHXHNSD,
	ISNULL(tblThongTinChungTu.fBHXHNSDTruocDieuChinh,0) + ISNULL(tblThongTinChungTu.fBHXHNSD,0) as fBHXHNSDSauDieuChinh,
	
	tblThongTinChungTu.fBHYTNLDTruocDieuChinh,
	tblThongTinChungTu.fBHYTNLD,
	ISNULL(tblThongTinChungTu.fBHYTNLDTruocDieuChinh,0) + ISNULL(tblThongTinChungTu.fBHYTNLD,0) as fBHYTNLDSauDieuChinh,
	
	tblThongTinChungTu.fBHYTNSDTruocDieuChinh,
	tblThongTinChungTu.fBHYTNSD,
	ISNULL(tblThongTinChungTu.fBHYTNSDTruocDieuChinh,0) + ISNULL(tblThongTinChungTu.fBHYTNSD,0) as fBHYTNSDSauDieuChinh,
	
	tblThongTinChungTu.fBHTNNLDTruocDieuChinh,
	tblThongTinChungTu.fBHTNNLD,
	ISNULL(tblThongTinChungTu.fBHTNNLDTruocDieuChinh,0) + ISNULL(tblThongTinChungTu.fBHTNNLD,0) as fBHTNNLDSauDieuChinh,
	
	tblThongTinChungTu.fBHTNNSDTruocDieuChinh,
	tblThongTinChungTu.fBHTNNSD,
	ISNULL(tblThongTinChungTu.fBHTNNSDTruocDieuChinh,0) + ISNULL(tblThongTinChungTu.fBHTNNSD,0) as fBHTNNSDSauDieuChinh,
	
	case when tblThongTinChungTu.iRowType = 2 then tblThongTinChungTu.bHangCha else tblMucLucNganSach.bHangCha end as bHangCha

	into tblThongTinChungTu_MLNS
	from tblMucLucNganSach
	left join tblThongTinChungTu on tblMucLucNganSach.iID_MLNS = tblThongTinChungTu.iID_MLNS
	left join DonVi on tblThongTinChungTu.iID_MaDonVi = DonVi.iID_MaDonVi and DonVi.iNamLamViec = @NamLamViec

	select * from tblThongTinChungTu_MLNS 
	order  by sXauNoiMa, sSoQuyetDinh;

	drop table tblMucLucNganSach;
	drop table tblChungTuBiDieuChinh;
	drop table tblChungTuBiDieuChinh_Ct;
	drop table tblThongTinChungTu_BiDieuChinh
	drop table tblThongTinChungTu_DieuChinhThemMoi;
	drop table tblThongTinChungTu;
	drop table tblThongTinChungTu_MLNS;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dieu_chinh_dtt_get_lns]    Script Date: 9/12/2023 5:02:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_dieu_chinh_dtt_get_lns]
	@namLamViec int,
	@donVi nvarchar(max),
	@ngayChungTu date,
	@userName nvarchar(100)
AS
BEGIN
	WITH tblLNS AS (
		SELECT DISTINCT sLNS
		FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet
		WHERE iID_MaDonVi in (select * from f_split(@donVi))
			AND iID_DTT_BHXH_ChungTu in 
				(SELECT iID_DTT_BHXH_PhanBo_ChungTu 
				FROM BH_DTT_BHXH_PhanBo_ChungTu 
				WHERE iNamLamViec = @namLamViec 
					AND sSoQuyetDinh IS NOT NULL
					AND bKhoa = 1
					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date))
	), 
	LNSTree AS (
		SELECT *
		FROM BH_DM_MucLucNganSach
		WHERE 
			sL = ''
			AND iNamLamViec = @namLamViec
			AND sLNS in (SELECT * FROM tblLNS)
		UNION ALL
		SELECT mlnsParent.*
		FROM BH_DM_MucLucNganSach mlnsParent
		INNER JOIN LNSTree
			ON mlnsParent.iID_MLNS = lnstree.iID_MLNS_Cha
		WHERE 
			mlnsParent.sL = '' 
			AND mlnsParent.iNamLamViec = @namLamViec
	)
	SELECT mlns.* 
	FROM (SELECT DISTINCT * FROM LNSTree) mlns
	WHERE mlns.iNamlamviec=@namLamViec
	AND mlns.sLNS like '902%'
	ORDER BY sXauNoiMa
END
GO
/****** Object:  StoredProcedure [dbo].[sp_dtt_bhxh_export_phan_bo_du_toan_chi_tiet]    Script Date: 9/12/2023 5:02:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_dtt_bhxh_export_phan_bo_du_toan_chi_tiet]
	@ChungTuId nvarchar(255),
	@LNS nvarchar(max),
	@YearOfWork int
AS
BEGIN
	SELECT isnull(ctct.iID_DTT_BHXH_ChungTu_ChiTiet, NEWID()) AS iID_DTCTChiTiet,
       ctct.iID_DTT_BHXH_ChungTu,
       mlns.iID_MLNS,
       mlns.iID_MLNS_Cha,
       mlns.sXauNoiMa,
       mlns.sLNS,
       mlns.sL,
       mlns.sK,
       mlns.sM,
       mlns.sTM,
       mlns.sTTM,
       mlns.sNG,
       mlns.sTNG,
       mlns.sTNG1,
       mlns.sTNG2,
       mlns.sTNG3,
       mlns.sMoTa,
       mlns.bHangCha,
       ctct.iNamLamViec,
       isnull(ctct.iPhanCap, 0) AS iPhanCap,
       ctct.iID_MaDonVi,
       isnull(ctct.fBHXH_NLD, 0) AS fBHXHNLD,
       isnull(ctct.fBHXH_NSD, 0) AS fBHXHNSD,
       isnull(ctct.fThuBHXH, 0) AS fThuBHXH,
       isnull(ctct.fBHYT_NLD, 0) AS fBHYTNLD,
       isnull(ctct.fBHYT_NSD, 0) AS fBHYTNSD,
       isnull(ctct.fThuBHYT, 0) AS fThuBHYT,
	   isnull(ctct.fBHTN_NLD, 0) AS fBHTNNLD,
	   isnull(ctct.fBHTN_NSD, 0) AS fBHTNNSD,
	   isnull(ctct.fThuBHTN, 0) AS fThuBHTN,
	   isnull(ctct.fTongCong, 0) AS fTongCong,
       ctct.dNgayTao,
       ctct.sNguoiTao,
       ctct.dNgaySua,
       ctct.sNguoiSua, 
	   ctct.iID_CTDuToan_Nhan,
	   isnull(ctct.iDuLieuNhan, 0) as iDuLieuNhan,
	   mlns.sChiTietToi,
	   dv.sTenDonVi,
	   mlns.bHangChaDuToan
	FROM
	  (SELECT *
	   FROM BH_DM_MucLucNganSach
	   WHERE iNamLamViec = @YearOfWork
	     --AND bHangChaDuToan IS NOT NULL
		 AND iTrangThai = 1
		 AND sLNS in
		   (SELECT *
			FROM f_split(@LNS))) mlns
	LEFT JOIN
	  (SELECT *
	   FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 --AND iPhanCap = 1
		 AND isnull(iDuLieuNhan, 0) = 0
		 AND iID_DTT_BHXH_ChungTu = @ChungTuId ) ctct ON mlns.iID_MLNS = ctct.iID_MLNS
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	ORDER BY mlns.sLNS,
			 mlns.sL,
			 mlns.sK,
			 mlns.sM,
			 mlns.sTM,
			 mlns.sTTM,
			 mlns.sNG,
			 mlns.sTNG;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh]    Script Date: 9/12/2023 5:02:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh]
@ChungTuId NVARCHAR(255),
@DonViTinh int
AS
BEGIN
--BHXH
--Lấy dữ liệu NLĐ, NSD của khối dự toán
select child.* into tbl_child from
(
select
5 STT,
N'5' MaSo,
N'- Người lao động đóng' NoiDung,
sum(fThuBHXH_NLD) DttDauNam,
sum(fThuBHXH_NLD_QTDauNam) Dtt6ThangDauNam,
sum(fThuBHXH_NLD_QTCuoiNam) Dtt6ThangCuoiNam,
sum(fThuBHXH_NLD_QTDauNam) + sum(fThuBHXH_NLD_QTCuoiNam) TongCong,
((sum(fThuBHXH_NLD_QTDauNam) + sum(fThuBHXH_NLD_QTCuoiNam)) - sum(fThuBHXH_NLD)) Tang,
(sum(fThuBHXH_NLD) - (sum(fThuBHXH_NLD_QTDauNam) + sum(fThuBHXH_NLD_QTCuoiNam))) Giam,
'DT' Khoi,
'BHXH' Thu,
'NLD' Type,
0 BHangCha
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where 
ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
and ctct.sLNS = '9020001' -- Khối dự toán
union
select
6 STT,
N'6' MaSo,
N'- Người sử dụng LĐ đóng' NoiDung,
sum(fThuBHXH_NSD) BhxhNsdDauNam,
sum(fThuBHXH_NSD_QTDauNam) BhxhNsd6ThangDauNam,
sum(fThuBHXH_NSD_QTCuoiNam) BhxhNsd6ThangCuoiNam,
sum(fThuBHXH_NSD_QTDauNam) + sum(fThuBHXH_NSD_QTCuoiNam) TongCong,
((sum(fThuBHXH_NSD_QTDauNam) + sum(fThuBHXH_NSD_QTCuoiNam)) - sum(fThuBHXH_NSD)) Tang,
(sum(fThuBHXH_NSD) - (sum(fThuBHXH_NSD_QTDauNam) + sum(fThuBHXH_NSD_QTCuoiNam))) Giam,
'DT' Khoi,
'BHXH' Thu,
'NSD' Type,
0 BHangCha
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where 
ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
and ctct.sLNS = '9020001' -- Khối dự toán

--Lấy dữ liệu NLĐ, NSD khối hạch toán
union
select
8 STT,
N'8' MaSo,
N'- Người lao động đóng' NoiDung,
sum(fThuBHXH_NLD) DttDauNam,
sum(fThuBHXH_NLD_QTDauNam) Dtt6ThangDauNam,
sum(fThuBHXH_NLD_QTCuoiNam) Dtt6ThangCuoiNam,
sum(fThuBHXH_NLD_QTDauNam) + sum(fThuBHXH_NLD_QTCuoiNam) TongCong,
((sum(fThuBHXH_NLD_QTDauNam) + sum(fThuBHXH_NLD_QTCuoiNam)) - sum(fThuBHXH_NLD)) Tang,
(sum(fThuBHXH_NLD) - (sum(fThuBHXH_NLD_QTDauNam) + sum(fThuBHXH_NLD_QTCuoiNam))) Giam,
'HT' Khoi,
'BHXH' Thu,
'NLD' Type,
0 BHangCha
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where 
ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
and ctct.sLNS = '9020002' -- Khối hạch toán
union
select
9 STT,
N'9' MaSo,
N'- Người sử dụng LĐ đóng' NoiDung,
sum(fThuBHXH_NSD) BhxhNsdDauNam,
sum(fThuBHXH_NSD_QTDauNam) Dtt6ThangDauNam,
sum(fThuBHXH_NSD_QTCuoiNam) Dtt6ThangCuoiNam,
sum(fThuBHXH_NSD_QTDauNam) + sum(fThuBHXH_NSD_QTCuoiNam) TongCong,
((sum(fThuBHXH_NSD_QTDauNam) + sum(fThuBHXH_NSD_QTCuoiNam)) - sum(fThuBHXH_NSD)) Tang,
(sum(fThuBHXH_NSD) - (sum(fThuBHXH_NSD_QTDauNam) + sum(fThuBHXH_NSD_QTCuoiNam))) Giam,
N'HT' Khoi,
N'BHXH' Thu,
'NSD' Type,
0 BHangCha
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where 
ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
and ctct.sLNS = '9020002' -- Khối hạch toán
-----------------------------------------
--BHYT
-- Khối dự toán
union
select
16 STT,
N'16' MaSo,
N'- BHYT quân nhân' NoiDung,
sum(0) DttDauNam,
sum(0) Dtt6ThangDauNam,
sum(0) Dtt6ThangCuoiNam,
sum(0) TongCong,
sum(0) Tang,
sum(0) Giam,
'DT' Khoi,
'BHYT' Thu,
'QN' Type,
0 BHangCha
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where 
ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
and ctct.sLNS = '9020001' -- Khối dự toán
and ctct.sXauNoiMa in
(
'9020001-010-011-0001-0000',  -- Sĩ quan
'9020001-010-011-0001-0001'	, -- QNCN
'9020001-010-011-0001-0002'  -- HSQ, BS
)
union
select
18 STT,
N'18' MaSo,
N'+ Người lao động đóng' NoiDung,
sum(fThuBHYT_NLD) DttDauNam,
sum(fThuBHYT_NLD_QTDauNam) Dtt6ThangDauNam,
sum(fThuBHYT_NLD_QTCuoiNam) Dtt6ThangCuoiNam,
sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam) TongCong,
((sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam)) - sum(fThuBHYT_NLD)) Tang,
(sum(fThuBHYT_NLD) - (sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam))) Giam,
'DT' Khoi,
'BHYT' Thu,
'NLD' Type,
0 BHangCha
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where 
ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
and ctct.sLNS = '9020001' -- Khối dự toán
and ctct.sXauNoiMa in
(
'9020001-010-011-0002-0000',  -- CC,CN, VCQP
'9020001-010-011-0002-0001'  -- LĐHĐ
)
union
select
19 STT,
N'19' MaSo,
N'+ Người sử dụng LĐ đóng' NoiDung,
sum(fThuBHYT_NSD) DttDauNam,
sum(fThuBHYT_NSD_QTDauNam) Dtt6ThangDauNam,
sum(fThuBHYT_NSD_QTCuoiNam) Dtt6ThangCuoiNam,
sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NSD_QTCuoiNam) TongCong,
((sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NSD_QTCuoiNam)) - sum(fThuBHYT_NSD)) Tang,
(sum(fThuBHYT_NSD) - (sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NSD_QTCuoiNam))) Giam,
'DT' Khoi,
'BHYT' Thu,
'NSD' Type,
0 BHangCha
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where 
ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
and ctct.sLNS = '9020001' -- Khối dự toán
and ctct.sXauNoiMa in
(
'9020001-010-011-0002-0000',  -- CC,CN, VCQP
'9020001-010-011-0002-0001'  -- LĐHĐ
)
-- Khối hạch toán
union
select
21 STT,
N'21' MaSo,
N'- BHYT quân nhân' NoiDung,
sum(0) DttDauNam,
sum(0) Dtt6ThangDauNam,
sum(0) Dtt6ThangCuoiNam,
sum(0) TongCong,
sum(0) Tang,
sum(0) Giam,
'HT' Khoi,
'BHYT' Thu,
'QN' Type,
0 BHangCha
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where 
ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
and ctct.sLNS = '9020002' -- Khối hạch toán
and ctct.sXauNoiMa in
(
'9020002-010-011-0001-0000',  -- Sĩ quan
'9020002-010-011-0001-0001'	, -- QNCN
'9020002-010-011-0001-0002'  -- HSQ, BS
)
union
select
23 STT,
N'23' MaSo,
N'+ Người lao động đóng' NoiDung,
sum(fThuBHYT_NLD) DttDauNam,
sum(fThuBHYT_NLD_QTDauNam) Dtt6ThangDauNam,
sum(fThuBHYT_NLD_QTCuoiNam) Dtt6ThangCuoiNam,
sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam) TongCong,
((sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam)) - sum(fThuBHYT_NLD)) Tang,
(sum(fThuBHYT_NLD) - (sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam))) Giam,
'HT' Khoi,
'BHYT' Thu,
'NLD' Type,
0 BHangCha
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where 
ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
and ctct.sLNS = '9020002' -- Khối hạch toán
and ctct.sXauNoiMa in
(
'9020002-010-011-0002-0000',  -- CC,CN, VCQP
'9020002-010-011-0002-0001'  -- LĐHĐ
)
union
select
24 STT,
N'24' MaSo,
N'+ Người sử dụng LĐ đóng' NoiDung,
sum(fThuBHYT_NSD) DttDauNam,
sum(fThuBHYT_NSD_QTDauNam) Dtt6ThangDauNam,
sum(fThuBHYT_NSD_QTCuoiNam) Dtt6ThangCuoiNam,
sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NSD_QTCuoiNam) TongCong,
((sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NSD_QTCuoiNam)) - sum(fThuBHYT_NSD)) Tang,
(sum(fThuBHYT_NSD) - (sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NSD_QTCuoiNam))) Giam,
'HT' Khoi,
'BHYT' Thu,
'NSD' Type,
0 BHangCha
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where 
ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
and ctct.sLNS = '9020002' -- Khối hạch toán
and ctct.sXauNoiMa in
(
'9020002-010-011-0002-0000',  -- CC,CN, VCQP
'9020002-010-011-0002-0001'  -- LĐHĐ
)
--BHTN
--Lấy dữ liệu khối dự toán
union
select
29 STT,
N'29' MaSo,
N'- Người lao động đóng' NoiDung,
sum(fThuBHTN_NLD) DttDauNam,
sum(fThuBHTN_NLD_QTDauNam) Dtt6ThangDauNam,
sum(fThuBHTN_NLD_QTCuoiNam) Dtt6ThangCuoiNam,
sum(fThuBHTN_NLD_QTDauNam) + sum(fThuBHTN_NLD_QTCuoiNam) TongCong,
((sum(fThuBHTN_NLD_QTDauNam) + sum(fThuBHTN_NLD_QTCuoiNam)) - sum(fThuBHTN_NLD)) Tang,
(sum(fThuBHTN_NLD) - (sum(fThuBHTN_NLD_QTDauNam) + sum(fThuBHTN_NLD_QTCuoiNam))) Giam,
'DT' Khoi,
'BHTN' Thu,
'NLD' Type,
0 BHangCha
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where 
ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
and ctct.sLNS = '9020001' -- Khối dự toán
union
select
30 STT,
N'30' MaSo,
N'- Người sử dụng LĐ đóng' NoiDung,
sum(fThuBHTN_NSD) BhxhNsdDauNam,
sum(fThuBHTN_NSD_QTDauNam) BhxhNsd6ThangDauNam,
sum(fThuBHTN_NSD_QTCuoiNam) BhxhNsd6ThangCuoiNam,
sum(fThuBHTN_NSD_QTDauNam) + sum(fThuBHTN_NSD_QTCuoiNam) TongCong,
((sum(fThuBHTN_NSD_QTDauNam) + sum(fThuBHTN_NSD_QTCuoiNam)) - sum(fThuBHXH_NSD)) Tang,
(sum(fThuBHTN_NSD) - (sum(fThuBHTN_NSD_QTDauNam) + sum(fThuBHTN_NSD_QTCuoiNam))) Giam,
'DT' Khoi,
'BHTN' Thu,
'NSD' Type,
0 BHangCha
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where 
ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
and ctct.sLNS = '9020001' -- Khối dự toán

--Lấy dữ liệu khối hạch toán
union
select
32 STT,
N'32' MaSo,
N'- Người lao động đóng' NoiDung,
sum(fThuBHTN_NLD) DttDauNam,
sum(fThuBHTN_NLD_QTDauNam) Dtt6ThangDauNam,
sum(fThuBHTN_NLD_QTCuoiNam) Dtt6ThangCuoiNam,
sum(fThuBHTN_NLD_QTDauNam) + sum(fThuBHTN_NLD_QTCuoiNam) TongCong,
((sum(fThuBHTN_NLD_QTDauNam) + sum(fThuBHTN_NLD_QTCuoiNam)) - sum(fThuBHTN_NLD)) Tang,
(sum(fThuBHTN_NLD) - (sum(fThuBHTN_NLD_QTDauNam) + sum(fThuBHTN_NLD_QTCuoiNam))) Giam,
'HT' Khoi,
'BHTN' Thu,
'NLD' Type,
0 BHangCha
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where 
ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
and ctct.sLNS = '9020002' -- Khối hạch toán
union
select
33 STT,
N'33' MaSo,
N'- Người sử dụng LĐ đóng' NoiDung,
sum(fThuBHTN_NSD) BhxhNsdDauNam,
sum(fThuBHTN_NSD_QTDauNam) Dtt6ThangDauNam,
sum(fThuBHTN_NSD_QTCuoiNam) Dtt6ThangCuoiNam,
sum(fThuBHTN_NSD_QTDauNam) + sum(fThuBHTN_NSD_QTCuoiNam) TongCong,
((sum(fThuBHTN_NSD_QTDauNam) + sum(fThuBHTN_NSD_QTCuoiNam)) - sum(fThuBHTN_NSD)) Tang,
(sum(fThuBHTN_NSD) - (sum(fThuBHTN_NSD_QTDauNam) + sum(fThuBHTN_NSD_QTCuoiNam))) Giam,
'HT' Khoi,
'BHTN' Thu,
'NSD' Type,
0 BHangCha
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where 
ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
and ctct.sLNS = '9020002' -- Khối hạch toán
) child
-----------------------------------------------------------------
--Lấy dữ liệu mục lục con
--BHXH
select child_ml.* into tbl_child_cat from
(
select
2 STT,
N'2=5+8' MaSo,
N'- Người lao động đóng' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'' Khoi,
'BHXH' Thu,
'' Type,
0 BHangCha
from tbl_child
where Thu = 'BHXH'
and Type = 'NLD'
union
select
3 STT,
N'3=6+9' MaSo,
N'- Người sử dụng LĐ đóng' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'' Khoi,
N'BHXH' Thu,
'' Type,
0 BHangCha
from tbl_child
where Thu = 'BHXH'
and Type = 'NSD'
union
select
4 STT,
N'4=5+6' MaSo,
N'a) Khối dự toán' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'DT' Khoi,
'BHXH' Thu,
'BHXH' Type,
0 BHangCha
from tbl_child
where Thu = 'BHXH'
and Khoi = 'DT'
union
select
7 STT,
N'7=8+9' MaSo,
N'a) Khối hạch toán' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'HT' Khoi,
'BHXH' Thu,
'BHXH' Type,
0 BHangCha
from tbl_child
where Thu = 'BHXH'
and Khoi = 'HT'
--BHYT
union
select
11 STT,
N'11=16+21' MaSo,
N'- BHYT quân nhân' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'' Khoi,
'BHYT' Thu,
'QN' Type,
0 BHangCha
from tbl_child
where Thu = 'BHYT'
and Type = 'QN'
union
select
13 STT,
N'13=18+23' MaSo,
N'+ Người lao động đóng' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'' Khoi,
'BHYT' Thu,
'LDHD' Type,
0 BHangCha
from tbl_child
where Thu = 'BHYT'
and Type = 'NLD'
union
select
14 STT,
N'14=19+24' MaSo,
N'+ Người sử dụng LĐ đóng' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'' Khoi,
'BHYT' Thu,
'LDHD' Type,
0 BHangCha
from tbl_child
where Thu = 'BHYT'
and Type = 'NSD'
union
select
17 STT,
N'17=18+19' MaSo,
N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'DT' Khoi,
'BHYT' Thu,
'' Type,
0 BHangCha
from tbl_child
where Thu = 'BHYT'
and Khoi = 'DT'
union
select
22 STT,
N'22=23+24' MaSo,
N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'HT' Khoi,
'BHYT' Thu,
'' Type,
0 BHangCha
from tbl_child
where Thu = 'BHYT'
and Khoi = 'HT'
--BHTN
union
select
26 STT,
N'26=29+32' MaSo,
N'- Người lao động đóng' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'' Khoi,
'BHTN' Thu,
'' Type,
0 BHangCha
from tbl_child
where Thu = 'BHTN'
and Type = 'NLD'
union
select
27 STT,
N'27=30+33' MaSo,
N'- Người sử dụng LĐ đóng' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'' Khoi,
'BHTN' Thu,
'' Type,
0 BHangCha
from tbl_child
where Thu = 'BHTN'
and Type = 'NSD'
union
select
28 STT,
N'28=29+330' MaSo,
N'a) Khối dự toán' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'DT' Khoi,
'BHTN' Thu,
'BHTN' Type,
0 BHangCha
from tbl_child
where Thu = 'BHTN'
and Khoi = 'DT'
union
select
31 STT,
N'31=32+33' MaSo,
N'a) Khối hạch toán' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'HT' Khoi,
'BHTN' Thu,
'BHTN' Type,
0 BHangCha
from tbl_child
where Thu = 'BHTN'
and Khoi = 'HT'
) child_ml

select tmp.* into temp1
from
(
select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child
union
select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child_cat
) tmp
-----------------------------------------------------------------
-- Lấy dữ liệu mục lục cha
select parent.* into tbl_parent from (
select
1 STT,
N'1=4+7' MaSo,
N'1. Thu Bảo hiểm xã hội' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'' Khoi,
'BHXH' Thu,
'' Type,
1 BHangCha
from tbl_child_cat
where Thu = 'BHXH'
and Type = 'BHXH'
union
select
25 STT,
N'25=28+31' MaSo,
N'3. Thu Bảo hiểm thất nghiệp' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'' Khoi,
'BHXH' Thu,
'' Type,
1 BHangCha
from tbl_child_cat
where Thu = 'BHTN'
and Type = 'BHTN'
union
select
12 STT,
N'12=13+14' MaSo,
N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'' Khoi,
'BHYT' Thu,
'LDHD' Type,
0 BHangCha
from tbl_child_cat
where Thu = 'BHYT'
and Type = 'LDHD'
union
select
15 STT,
N'15=16+17' MaSo,
N'a) Khối dự toán' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'DT' Khoi,
'BHYT' Thu,
'BHYT' Type,
0 BHangCha
from tbl_child_cat
where STT in (16, 17)
union
select
20 STT,
N'20=21+22' MaSo,
N'b) Khối hạch toán' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'DT' Khoi,
'BHYT' Thu,
'BHYT' Type,
0 BHangCha
from tbl_child_cat
where STT in (21, 22)
) parent

--------------------------------------------

select result.* into tbl_result from
(
select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from temp1
union
select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_parent
union
-- Lấy tổng số thu BHYT
select
10 STT,
N'10=15+20' MaSo,
N'2. Thu Bảo hiểm y tế' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
(sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'' Khoi,
'BHYT' Thu,
'BHYT' Type,
1 BHangCha
from tbl_parent
where STT in (15, 20)
) result
order by result.STT;

select STT, MaSo, NoiDung, DttDauNam/@DonViTinh DttDauNam, Dtt6ThangDauNam/@DonViTinh Dtt6ThangDauNam, Dtt6ThangCuoiNam/@DonViTinh Dtt6ThangCuoiNam, TongCong/@DonViTinh TongCong, Tang/@DonViTinh Tang, Giam/@DonViTinh Giam, Khoi, Thu, Type, BHangCha from tbl_result;

---------------------------------------------
drop table tbl_child;
drop table tbl_child_cat;
drop table temp1;
drop table tbl_parent;
drop table tbl_result;
END
GO
