/****** Object:  StoredProcedure [dbo].[sp_bhxh_get_quan_so_binh_quan_nam]    Script Date: 8/8/2023 2:46:22 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_get_quan_so_binh_quan_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_get_quan_so_binh_quan_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet_dieu_chinh]    Script Date: 8/8/2023 2:46:22 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet_dieu_chinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet_dieu_chinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]    Script Date: 8/8/2023 2:46:22 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]    Script Date: 8/8/2023 2:46:22 PM ******/
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
	'00000000-0000-0000-0000-000000000000' as iID_DTT_BHXH_ChungTu_ChiTiet,
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
	0 as iRowType,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
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
			0 as fThuBHXHNLD,
			0 as fThuBHXHNSD,
			0 as fThuBHYTNLD,
			0 as fThuBHYTNSD,
			0 as fThuBHTNNLD,
			0 as fThuBHTNNSD
	into tblChiTietDuToanNhan
	from BH_DTT_BHXH_ChungTu_ChiTiet as nhanpb_chitiet
	where iID_DTT_BHXH in (select iID_CTDuToan_Nhan from tblChungTuNhanPhanBo)

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
		tblChiTietDuToanNhan.fThuBHXHNLD,
		tblChiTietDuToanNhan.fThuBHXHNSD,
		tblChiTietDuToanNhan.fThuBHYTNLD,
		tblChiTietDuToanNhan.fThuBHYTNSD,
		tblChiTietDuToanNhan.fThuBHTNNLD,
		tblChiTietDuToanNhan.fThuBHTNNSD,
		1 as iRowType
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
		chitiet_phanbo.fBHXH_NLD as fBHXHNLDTruocDieuChinh,
		chitiet_phanbo.fBHXH_NSD as fBHXHNSD,
		chitiet_phanbo.fBHXH_NSD as fBHXHNSDTruocDieuChinh,
		chitiet_phanbo.fBHYT_NLD as fBHYTNLD,
		chitiet_phanbo.fBHYT_NLD as fBHYTNLDTruocDieuChinh,
		chitiet_phanbo.fBHYT_NSD as fBHYTNSD,
		chitiet_phanbo.fBHYT_NSD as fBHYTNSDTruocDieuChinh,
		chitiet_phanbo.fBHTN_NLD as fBHTNNLD,
		chitiet_phanbo.fBHTN_NLD as fBHTNNLDTruocDieuChinh,
		chitiet_phanbo.fBHTN_NSD as fBHTNNSD,
		chitiet_phanbo.fBHTN_NSD as fBHTNNSDTruocDieuChinh,
		1 as iRowType,
		#temp.iID_MaDonVi,
		#temp.sTenDonVi,
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
	NEWID() as iID_CTDuToan_Nhan,
	'00000000-0000-0000-0000-000000000000' as iID_DTT_BHXH_ChungTu_ChiTiet,
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
						iID_MLNS
						from BH_DTT_BHXH_PhanBo_ChungTuChiTiet as ct_pb
						where ct_pb.iID_DTT_BHXH_ChungTu =  @ChungTuId
						group by iID_MLNS
					)as ct_pb_t  
					on ct_pb_t.iID_MLNS = ct_npb.iID_MLNS) as chitiet_chuaphanbo on chitiet_chuaphanbo.iID_MLNS = muluc_ngansach.iID_MLNS 
	
	select * from
	(
		SELECT * from tblMucLucNganSach
		UNION ALL
		SELECT * from tblSoChuaPhanBo
		UNION ALL
		SELECT * from #temp1
	) as result
	order by result.sXauNoiMa, iRowType, iID_MaDonVi asc


drop table tblMucLucNganSach;
drop table tblDonVi;
drop table tblChungTuNhanPhanBo;
drop table tblChiTietDuToanNhan;
drop table tbl_tblChiTietDuToanNhan_MucLuc;
drop table #temp;
drop table #temp1;
drop table tblSoChuaPhanBo;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet_dieu_chinh]    Script Date: 8/8/2023 2:46:22 PM ******/
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
	NULL as IID_DTC_PhanBoDuToanChiTiet,
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
	--0 as bEmty,
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
	
	dc.iID_DTT_BHXH_PhanBo_ChungTu as iID_DTT_BHXH,
	dc_ct.iID_DTT_BHXH_ChungTu_ChiTiet,
	dc.sSoQuyetDinh
	into tblChungTuBiDieuChinh_Ct 
	from BH_DTT_BHXH_PhanBo_ChungTuChiTiet as dc_ct  
	inner join BH_DTT_BHXH_PhanBo_ChungTu as dc on dc_ct.iID_DTT_BHXH_ChungTu = dc.iID_DTT_BHXH_PhanBo_ChungTu
	where dc_ct.iID_DTT_BHXH_ChungTu in ( select iID_CTDuToan_Nhan from tblChungTuBiDieuChinh)


	select 
	dc_ct.iID_MLNS, 
	dc_ct.iID_MaDonVi,
	dc_ct.fBHXH_NLD as fBHXHNLD,
	dc_ct.fBHXH_NSD as fBHXHNSD,
	dc_ct.fBHYT_NLD as fBHYTNLD,
	dc_ct.fBHYT_NSD as fBHYTNSD,
	dc_ct.fBHTN_NLD as fBHTNNLD,
	dc_ct.fBHTN_NSD as fBHTNNSD,
	dc.iID_DTT_BHXH_PhanBo_ChungTu as iID_DTT_BHXH,
	dc.sSoQuyetDinh
	into tblChungTuBiDieuChinh_Ct_themmoi
	from BH_DTT_BHXH_PhanBo_ChungTuChiTiet as dc_ct  
	inner join BH_DTT_BHXH_PhanBo_ChungTu as dc on dc_ct.iID_DTT_BHXH_ChungTu = dc.iID_DTT_BHXH_PhanBo_ChungTu
	where dc_ct.iID_DTT_BHXH_ChungTu in ( select iID_CTDuToan_Nhan from tblChungTuBiDieuChinh)


	--select * from tblChungTuBiDieuChinh_Ct

	--- Map voi bang BH_DTT_BHXH_PhanBo_ChungTuChiTiet lay thông tin so truoc và sau khi dieu chinh
	select  
	npb_ct.iID_MLNS,
	npb_ct.iID_MaDonVi,
	DonVi.sTenDonVi,
	npb_ct.sSoQuyetDinh,
	npb_ct.fBHXHNLD as fBHXHNLDTruocDieuChinh,
	npb_ct.fBHXHNSD as fBHXHNSDTruocDieuChinh,
	
	npb_ct.fBHYTNLD as fBHYTNLDTruocDieuChinh,
	npb_ct.fBHYTNSD as fBHYTNSDTruocDieuChinh,
	npb_ct.fBHTNNLD as fBHTNNLDTruocDieuChinh,
	npb_ct.fBHTNNSD as fBHTNNSDTruocDieuChinh,
	
	npb_ct.iID_DTT_BHXH as iID_CTDuToan_Nhan,
	pb_ct.iID_DTT_BHXH_ChungTu_ChiTiet,
	
	pb_ct.fBHXH_NLD as fBHXHNLD,
	pb_ct.fBHXH_NSD as fBHXHNSD,
	pb_ct.fBHYT_NLD as fBHYTNLD,
	pb_ct.fBHYT_NSD as fBHYTNSD,
	pb_ct.fBHTN_NLD as fBHTNNLD,
	pb_ct.fBHTN_NSD as fBHTNNSD,
	
	0 as bHangCha,
	2 iRowType
	into tblThongTinChungTu
	from tblChungTuBiDieuChinh_Ct as npb_ct
	left join ( select * from BH_DTT_BHXH_PhanBo_ChungTuChiTiet where iID_DTT_BHXH_ChungTu =  @ChungTuId) as  pb_ct on npb_ct.iID_MLNS = pb_ct.iID_MLNS and npb_ct.iID_MaDonVi = pb_ct.iID_MaDonVi
	and npb_ct.iID_DTT_BHXH = pb_ct.iID_CTDuToan_Nhan
	left join DonVi on npb_ct.iID_MaDonVi = DonVi.iID_MaDonVi
	where DonVi.iNamLamViec = @NamLamViec
	
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
	CONCAT(tblThongTinChungTu.iID_MaDonVi, '-', tblThongTinChungTu.sTenDonVi) as sTenDonVi,
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

	select * from tblThongTinChungTu_MLNS 
	order  by sXauNoiMa, sSoQuyetDinh;

	drop table tblMucLucNganSach;
	drop table tblChungTuBiDieuChinh;
	drop table tblChungTuBiDieuChinh_Ct;
	drop table tblThongTinChungTu;
	drop table tblThongTinChungTu_MLNS;
	drop table tblChungTuBiDieuChinh_Ct_themmoi;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_get_quan_so_binh_quan_nam]    Script Date: 8/8/2023 2:46:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_bhxh_get_quan_so_binh_quan_nam]
	@YearOfWork int
AS
BEGIN
	declare @LuongKeHoach table (Id uniqueidentifier,Nam int, Ma_CanBo varchar(20), Ma_PhuCap nvarchar(50), Ma_CB varchar(20), Gia_Tri numeric(15, 4));

	INSERT INTO @LuongKeHoach (Nam, Ma_CanBo, Ma_CB)
	SELECT DISTINCT Nam, Ma_CanBo, Ma_CB
		FROM TL_BangLuong_KeHoach 
		WHERE Nam = 2023

		SELECT '9020001-010-011-0001-0000' XauNoiMa,
		count(1)/12 AS QSBQ 
		FROM @LuongKeHoach 
		WHERE Ma_CB LIKE '1%' --Lấy quân số bình quân năm của cấp bậc Sĩ quan
		
		UNION
		SELECT '9020001-010-011-0001-0001',
		count(1)/12 AS QSBQ_QNCN FROM @LuongKeHoach 
		where Ma_CB LIKE '2%' --Lấy quân số bình quân năm của cấp bậc Quân nhân chuyên nghiệp
		
		UNION
		SELECT '9020001-010-011-0001-0002',
		count(1)/12 AS QSBQ_HSQ FROM @LuongKeHoach 
		where Ma_CB LIKE '0%' --Lấy quân số bình quân năm của cấp bậc Hạ sĩ quan
		
		UNION
		SELECT '9020001-010-011-0002-0000',
		count(1)/12 AS QSBQ_VCQP FROM @LuongKeHoach 
		where Ma_CB in ('3.1', '3.2', '3.3') --Lấy quân số bình quân năm của cấp bậc CC, CN, VCQP
		
		UNION
		SELECT '9020001-010-011-0002-0001',
		count(1)/12 AS QSBQ_LDHD FROM @LuongKeHoach 
		where Ma_CB = '43' --Lấy quân số bình quân năm của cấp bậc LDHD
		
END
GO
