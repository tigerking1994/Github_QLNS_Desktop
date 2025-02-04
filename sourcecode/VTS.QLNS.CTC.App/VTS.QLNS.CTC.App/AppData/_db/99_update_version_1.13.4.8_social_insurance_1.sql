/****** Object:  StoredProcedure [dbo].[sp_tl_bang_luong_thang_bhxh_chitiet]    Script Date: 11/14/2023 10:58:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bang_luong_thang_bhxh_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bang_luong_thang_bhxh_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh]    Script Date: 11/14/2023 10:58:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan_nld]    Script Date: 11/14/2023 10:58:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan_nld]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan_nld]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan]    Script Date: 11/14/2023 10:58:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_hssv]    Script Date: 11/14/2023 10:58:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_hssv]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_hssv]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_rpt_qttm_bhyt_than_nhan_nld_tong_hop]    Script Date: 11/14/2023 10:58:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qttm_rpt_qttm_bhyt_than_nhan_nld_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qttm_rpt_qttm_bhyt_than_nhan_nld_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_rpt_bhyt_than_nhan_tong_hop]    Script Date: 11/14/2023 10:58:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qttm_rpt_bhyt_than_nhan_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qttm_rpt_bhyt_than_nhan_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_get_chung_tu_don_vi_tong_hop]    Script Date: 11/14/2023 10:58:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qttm_get_chung_tu_don_vi_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qttm_get_chung_tu_don_vi_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_get_chung_tu_don_vi]    Script Date: 11/14/2023 10:58:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qttm_get_chung_tu_don_vi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qttm_get_chung_tu_don_vi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_quy]    Script Date: 11/14/2023 10:58:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_nam]    Script Date: 11/14/2023 10:58:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]    Script Date: 11/14/2023 10:58:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_nam]    Script Date: 11/14/2023 10:58:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_truy_thu]    Script Date: 11/14/2023 10:58:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_giai_thich_truy_thu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_truy_thu]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_tong_hop_so_sanh]    Script Date: 11/14/2023 10:58:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_giai_thich_tong_hop_so_sanh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_tong_hop_so_sanh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_giam_dong]    Script Date: 11/14/2023 10:58:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_giai_thich_giam_dong]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_giam_dong]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_bang_loi]    Script Date: 11/14/2023 10:58:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_giai_thich_bang_loi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_bang_loi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]    Script Date: 11/14/2023 10:58:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]    Script Date: 11/14/2023 10:58:04 AM ******/
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

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblMucLucNganSachDTT]') AND type in (N'U'))
	drop table tblMucLucNganSachDTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblDonViDTT]') AND type in (N'U'))
	drop table tblDonViDTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblChungTuNhanPhanBoDTT]') AND type in (N'U'))
	drop table tblChungTuNhanPhanBoDTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblChiTietDuToanNhanDTT]') AND type in (N'U'))
	drop table tblChiTietDuToanNhanDTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_tblChiTietDuToanNhan_MucLucDTT]') AND type in (N'U'))
	drop table tbl_tblChiTietDuToanNhan_MucLucDTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDTT]') AND type in (N'U'))
	drop table tempDTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1DTT]') AND type in (N'U'))
	drop table temp1DTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblSoChuaPhanBoDTT]') AND type in (N'U'))
	drop table tblSoChuaPhanBoDTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblMucLucNganSach_duplicateDTT]') AND type in (N'U'))
	drop table tblMucLucNganSach_duplicateDTT;

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
	into tblMucLucNganSachDTT
	from BH_DM_MucLucNganSach 
	where sLNS  IN (SELECT * FROM f_split(@LNS)) and iNamLamViec = @NamLamViec;

	---Lấy danh sách đơn vị được phân bổ
	select * 
	into  tblDonViDTT
	from DonVi where iNamLamViec = @NamLamViec and iID_MaDonVi  IN (SELECT * FROM f_split(@IdDonVi));
	
	--lấy danh sách dự toán nhận phân bổ
	select *
	into tblChungTuNhanPhanBoDTT
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
	into tblChiTietDuToanNhanDTT
	from BH_DTT_BHXH_ChungTu_ChiTiet as nhanpb_chitiet
	inner join BH_DTT_BHXH_ChungTu as nhanpb on nhanpb.iID_DTT_BHXH = nhanpb_chitiet.iID_DTT_BHXH
	where nhanpb.iID_DTT_BHXH in (select iID_CTDuToan_Nhan from tblChungTuNhanPhanBoDTT)

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
	into tbl_tblChiTietDuToanNhan_MucLucDTT
	from tblMucLucNganSachDTT tblMucLucNganSach
	inner join tblChiTietDuToanNhanDTT tblChiTietDuToanNhan on tblMucLucNganSach.iID_MLNS = tblChiTietDuToanNhan.iID_MLNS


	---Hiển thị danh sách chi tiết nhận phân bổ theo đơn vị được chọn phân bổ
	select  tbl_tblChiTietDuToanNhan_MucLucDTT.*, tblDonVi.iID_MaDonVi, concat(tblDonVi.iID_MaDonVi, '-', tblDonVi.sTenDonVi) as sTenDonVi, 0 as bHangCha
	into tempDTT
	from tbl_tblChiTietDuToanNhan_MucLucDTT cross join tblDonViDTT tblDonVi

	---Map với bảng BH_DTT_BHXH_PhanBo_ChungTuChiTiet để lấy thông tin đã được phân bổ
	select 
		tempDTT.iID_CTDuToan_Nhan, 
		chitiet_phanbo.iID_DTT_BHXH_ChungTu_ChiTiet,
		tempDTT.iID_MLNS,
		tempDTT.iID_MLNS_Cha,
		tempDTT.sLNS,
		tempDTT.sL,
		tempDTT.sK,
		tempDTT.sM,
		tempDTT.sTM,
		tempDTT.sTTM,
		tempDTT.sNG,
		tempDTT.sTNG,
		tempDTT.sXauNoiMa,
		tempDTT.sMoTa as sMoTa,
		ISNULL(chitiet_phanbo.fBHXH_NLD, 0) as fBHXHNLD,
		ISNULL(tempDTT.fThuBHXHNLD, 0) as fBHXHNLDTruocDieuChinh,
		ISNULL(chitiet_phanbo.fBHXH_NSD, 0) as fBHXHNSD,
		ISNULL(tempDTT.fThuBHXHNSD, 0) as fBHXHNSDTruocDieuChinh,
		ISNULL(chitiet_phanbo.fBHYT_NLD, 0) as fBHYTNLD,
		ISNULL(tempDTT.fThuBHYTNLD, 0) as fBHYTNLDTruocDieuChinh,
		ISNULL(chitiet_phanbo.fBHYT_NSD, 0) as fBHYTNSD,
		ISNULL(tempDTT.fThuBHYTNSD, 0) as fBHYTNSDTruocDieuChinh,
		ISNULL(chitiet_phanbo.fBHTN_NLD, 0) as fBHTNNLD,
		ISNULL(tempDTT.fThuBHTNNLD, 0) as fBHTNNLDTruocDieuChinh,
		ISNULL(chitiet_phanbo.fBHTN_NSD, 0) as fBHTNNSD,
		tempDTT.fThuBHTNNSD as fBHTNNSDTruocDieuChinh,
		3 as iRowType,
		tempDTT.iID_MaDonVi,
		tempDTT.sTenDonVi,
		tempDTT.sSoQuyetDinh,
		0 as bHangCha,
		0 as IsRemainRow
	into temp1DTT
	from tempDTT
	left join 
		(
			select * 
			from BH_DTT_BHXH_PhanBo_ChungTuChiTiet 
			where iID_DTT_BHXH_ChungTu = @ChungTuId
		) as chitiet_phanbo
		on chitiet_phanbo.iID_CTDuToan_Nhan = tempDTT.iID_CTDuToan_Nhan and chitiet_phanbo.iID_MaDonVi = tempDTT.iID_MaDonVi and chitiet_phanbo.iID_MLNS = tempDTT.iID_MLNS


	-----Lấy danh sách Số chưa phân bổ
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
	N'Số chưa phân bổ' as sMoTa,
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
	into tblSoChuaPhanBoDTT
	from tblMucLucNganSachDTT as muluc_ngansach
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
						where ct_pb.iID_CTDuToan_Nhan in (select iID_CTDuToan_Nhan from tblChungTuNhanPhanBoDTT)
						group by iID_MLNS, iID_CTDuToan_Nhan
					)as ct_pb_t  
					on ct_pb_t.iID_MLNS = ct_npb.iID_MLNS and ct_npb.iID_DTT_BHXH = ct_pb_t.iID_CTDuToan_Nhan) as chitiet_chuaphanbo on chitiet_chuaphanbo.iID_MLNS = muluc_ngansach.iID_MLNS
					inner join BH_DTT_BHXH_ChungTu as npb on npb.iID_DTT_BHXH = chitiet_chuaphanbo.iID_CTDuToan_Nhan
					where npb.iID_DTT_BHXH in ( select iID_CTDuToan_Nhan from tblChungTuNhanPhanBoDTT)

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
	ISNULL(tblSoChuaPhanBo.fBHXHNLD, 0) fBHXHNLD,
	ISNULL(tblSoChuaPhanBo.fBHXHNLDTruocDieuChinh, 0) fBHXHNLDTruocDieuChinh,
	ISNULL(tblSoChuaPhanBo.fBHXHNSD, 0) fBHXHNSD,
	ISNULL(tblSoChuaPhanBo.fBHXHNSDTruocDieuChinh, 0) fBHXHNSDTruocDieuChinh,
	ISNULL(tblSoChuaPhanBo.fBHYTNLD, 0) fBHYTNLD,
	ISNULL(tblSoChuaPhanBo.fBHYTNLDTruocDieuChinh, 0) fBHYTNLDTruocDieuChinh,
	ISNULL(tblSoChuaPhanBo.fBHYTNSD, 0) fBHYTNSD,
	ISNULL(tblSoChuaPhanBo.fBHYTNSDTruocDieuChinh, 0) fBHYTNSDTruocDieuChinh,
	ISNULL(tblSoChuaPhanBo.fBHTNNLD, 0) fBHTNNLD,
	ISNULL(tblSoChuaPhanBo.fBHTNNLDTruocDieuChinh, 0) fBHTNNLDTruocDieuChinh,
	ISNULL(tblSoChuaPhanBo.fBHTNNSD, 0) fBHTNNSD,
	ISNULL(tblSoChuaPhanBo.fBHTNNSDTruocDieuChinh, 0) fBHTNNSDTruocDieuChinh,
	case when tblSoChuaPhanBo.iRowType = 2 then 2 else tblMucLucNganSach.iRowType end iRowType,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	tblSoChuaPhanBo.sSoQuyetDinh as sSoQuyetDinh,
	case when tblSoChuaPhanBo.iRowType = 2 then 1 else tblMucLucNganSach.bHangCha end bHangCha,
	0 as IsRemainRow
	into tblMucLucNganSach_duplicateDTT
	from tblMucLucNganSachDTT tblMucLucNganSach
	left join tblSoChuaPhanBoDTT tblSoChuaPhanBo on tblMucLucNganSach.iID_MLNS = tblSoChuaPhanBo.iID_MLNS
	order by sXauNoiMa
	
	select * from
	(
		SELECT * from tblMucLucNganSach_duplicateDTT
		UNION ALL
		SELECT * from tblSoChuaPhanBoDTT
		UNION ALL
		SELECT * from temp1DTT
	) as result
	order by result.sXauNoiMa,
		result.sSoQuyetDinh,
		result.iID_MaDonVi,
		result.iRowType,
		result.IsRemainRow
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_bang_loi]    Script Date: 11/14/2023 10:58:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_bang_loi]
	@Quy int,
	@NamLamViec int,
	@MaDonVi nvarchar(50)
AS
BEGIN
	select distinct
		ctgt.iID_QT_CTCT_GiaiThich,
		ctgt.iID_QTT_BHXH_ChungTu,
		ctgt.sNguoiTao,
		ctgt.sNguoiSua,
		ctgt.dNgayTao,
		ctgt.dNgaySua,
		ctgt.iID_MaDonVi,
		ctgt.iNamLamViec,
		ctgt.iQuyNam,
		ctgt.iQuyNamLoai,
		ctgt.sQuyNamMoTa,
		ctgt.iID_MLNS,
		ctgt.sNoiDung,
		ctgt.fPhaiNop_TrongQuyNam,
		ctgt.fTruyThu_QuyNamTruoc,
		ctgt.fPhaiNop_QuyNamTruoc,
		ctgt.fDaNop_TrongQuyNam,
		ctgt.fConPhaiNopTiep,
		ctgt.sKienNghi
	from BH_QTT_BHXH_CTCT_GiaiThich ctgt
	where ctgt.iQuyNam = @Quy
		and ctgt.iNamLamViec = @NamLamViec
		and ctgt.iID_MaDonVi = @MaDonVi
		and ctgt.iLoaiGiaiThich = 1 --loai giai thich bang loi
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_giam_dong]    Script Date: 11/14/2023 10:58:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_giam_dong]
	@NamLamViec int,
	@MaDonVi nvarchar(50)
AS
BEGIN
	select
	mlns.iID_MLGT,
	mlns.iSTT,
	mlns.sMoTa,
	mlns.sNoiDung,
	mlns.iLoai,
	chungtudonvi.fSoPhaiThuNop,
	chungtudonvi.fSoDaNopTrongNam,
	chungtudonvi.fSoDaNopSau31_12,
	chungtudonvi.fTongSoDaNop,
	chungtudonvi.fSoConPhaiNop,
	chungtudonvi.iQuanSo,
	chungtudonvi.fQuyTienLuongCanCu,
	chungtudonvi.fSoTienGiamDong,
	chungtudonvi.dTuNgay,
	chungtudonvi.dDenNgay
	from
		(select
			iSTT,
			sNoiDung,
			iID_MLGT,
			concat(iSTT, '. ' , sNoiDung) sMoTa,
			iLoai
		from BH_QTT_MucLucGiaiThich) mlns
		left join
			(select distinct
				ctgt.iID_QT_CTCT_GiaiThich,
				ctgt.iID_QTT_BHXH_ChungTu,
				ctgt.sNguoiTao,
				ctgt.sNguoiSua,
				ctgt.dNgayTao,
				ctgt.dNgaySua,
				ctgt.iID_MaDonVi,
				ctgt.iNamLamViec,
				ctgt.iQuyNam,
				ctgt.iQuyNamLoai,
				ctgt.sQuyNamMoTa,
				ctgt.iID_MLNS,
				ctgt.sNoiDung,
				ctgt.fSoPhaiThuNop,
				ctgt.fSoDaNopTrongNam,
				ctgt.fSoDaNopSau31_12,
				ctgt.fTongSoDaNop,
				ctgt.fSoConPhaiNop,
				ctgt.iQuanSo,
				ctgt.fQuyTienLuongCanCu,
				ctgt.fSoTienGiamDong,
				ctgt.dTuNgay,
				ctgt.dDenNgay,
				ctgt.iLoaiGiaiThich
			from BH_QTT_BHXH_CTCT_GiaiThich ctgt
			where ctgt.iQuyNam = @NamLamViec
			and ctgt.iLoaiGiaiThich = 4
			and ctgt.iID_MaDonVi = @MaDonVi) chungtudonvi
		on mlns.iID_MLGT = chungtudonvi.iID_MLNS
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_tong_hop_so_sanh]    Script Date: 11/14/2023 10:58:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_tong_hop_so_sanh]
	@NamLamViec int,
	@MaDonVi nvarchar(50)
AS
BEGIN
	select
	mlns.iID_MLGT,
	mlns.iSTT,
	mlns.sMoTa,
	mlns.sNoiDung,
	mlns.iLoai,
	chungtudonvi.fSoPhaiThuNop,
	chungtudonvi.fSoDaNopTrongNam,
	chungtudonvi.fSoDaNopSau31_12,
	chungtudonvi.fTongSoDaNop,
	chungtudonvi.fSoConPhaiNop,
	chungtudonvi.iQuanSo,
	chungtudonvi.fQuyTienLuongCanCu,
	chungtudonvi.fSoTienGiamDong,
	chungtudonvi.dTuNgay,
	chungtudonvi.dDenNgay
	from
		(select
			iSTT,
			sNoiDung,
			iID_MLGT,
			concat(iSTT, '. ' , sNoiDung) sMoTa,
			iLoai
		from BH_QTT_MucLucGiaiThich) mlns
		left join
			(select distinct
				ctgt.iID_QT_CTCT_GiaiThich,
				ctgt.iID_QTT_BHXH_ChungTu,
				ctgt.sNguoiTao,
				ctgt.sNguoiSua,
				ctgt.dNgayTao,
				ctgt.dNgaySua,
				ctgt.iID_MaDonVi,
				ctgt.iNamLamViec,
				ctgt.iQuyNam,
				ctgt.iQuyNamLoai,
				ctgt.sQuyNamMoTa,
				ctgt.iID_MLNS,
				ctgt.sNoiDung,
				ctgt.fSoPhaiThuNop,
				ctgt.fSoDaNopTrongNam,
				ctgt.fSoDaNopSau31_12,
				ctgt.fTongSoDaNop,
				ctgt.fSoConPhaiNop,
				ctgt.iQuanSo,
				ctgt.fQuyTienLuongCanCu,
				ctgt.fSoTienGiamDong,
				ctgt.dTuNgay,
				ctgt.dDenNgay,
				ctgt.iLoaiGiaiThich
			from BH_QTT_BHXH_CTCT_GiaiThich ctgt
			where ctgt.iQuyNam = @NamLamViec
			and ctgt.iLoaiGiaiThich = 3
			and ctgt.iID_MaDonVi = @MaDonVi) chungtudonvi
		on mlns.iID_MLGT = chungtudonvi.iID_MLNS
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_truy_thu]    Script Date: 11/14/2023 10:58:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_truy_thu]
	@NamLamViec int,
	@MaDonVi nvarchar(50)
AS
BEGIN
	
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dutoan_tbl]') AND type in (N'U'))
	drop table dutoan_tbl
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hachtoan_tbl]') AND type in (N'U'))
	drop table hachtoan_tbl

	--Tạo dữ liệu khói dự toán
	select
		mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.sMoTa,
		mlns.bHangCha,
		mlns.sLNS,
		mlns.sXauNoiMa,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		chungtudonvi.fTruyThu_BHXH_NLD,
		chungtudonvi.fTruyThu_BHXH_NSD,
		chungtudonvi.fTruyThu_BHXH_TongCong,
		chungtudonvi.fTruyThu_BHYT_NLD,
		chungtudonvi.fTruyThu_BHYT_NSD,
		chungtudonvi.fTruyThu_BHYT_TongCong,
		chungtudonvi.fTruyThu_BHTN_NLD,
		chungtudonvi.fTruyThu_BHTN_NSD,
		chungtudonvi.fTruyThu_BHTN_TongCong
	into dutoan_tbl
	from
		(select
			BH_DM_MucLucNganSach.iID_MLNS,
			BH_DM_MucLucNganSach.iID_MLNS_Cha,
			BH_DM_MucLucNganSach.bHangCha,
			BH_DM_MucLucNganSach.sLNS,
			BH_DM_MucLucNganSach.sL,
			BH_DM_MucLucNganSach.sK,
			BH_DM_MucLucNganSach.sM,
			BH_DM_MucLucNganSach.sTM,
			BH_DM_MucLucNganSach.sTTM,
			BH_DM_MucLucNganSach.sNG,
			BH_DM_MucLucNganSach.sTNG,
			BH_DM_MucLucNganSach.sXauNoiMa,
			BH_DM_MucLucNganSach.sMoTa
		from BH_DM_MucLucNganSach 
		where sLNS = '9020001' -- Khối dự toán
		and iNamLamViec = @NamLamViec
		and iID_MLNS_Cha is not null) mlns
		left join
		(select distinct
			ctgt.iID_QT_CTCT_GiaiThich,
			ctgt.iID_QTT_BHXH_ChungTu,
			ctgt.sNguoiTao,
			ctgt.sNguoiSua,
			ctgt.dNgayTao,
			ctgt.dNgaySua,
			ctgt.iID_MaDonVi,
			ctgt.iNamLamViec,
			ctgt.iQuyNam,
			ctgt.iQuyNamLoai,
			ctgt.sQuyNamMoTa,
			ctgt.sNoiDung,
			ctgt.fTruyThu_BHXH_NLD,
			ctgt.fTruyThu_BHXH_NSD,
			ctgt.fTruyThu_BHXH_TongCong,
			ctgt.fTruyThu_BHYT_NLD,
			ctgt.fTruyThu_BHYT_NSD,
			ctgt.fTruyThu_BHYT_TongCong,
			ctgt.fTruyThu_BHTN_NLD,
			ctgt.fTruyThu_BHTN_NSD,
			ctgt.fTruyThu_BHTN_TongCong,
			ctgt.iID_MLNS
		from BH_QTT_BHXH_CTCT_GiaiThich ctgt
		where ctgt.iLoaiGiaiThich = 2
			and ctgt.iQuyNam = @NamLamViec
			and ctgt.iID_MaDonVi = @MaDonVi) chungtudonvi 
	on mlns.iID_MLNS = chungtudonvi.iID_MLNS
---------
--Tạo dữ liệu khói hạch toán
	select
		mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.sMoTa,
		mlns.bHangCha,
		mlns.sLNS,
		mlns.sXauNoiMa,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		chungtudonvi.fTruyThu_BHXH_NLD,
		chungtudonvi.fTruyThu_BHXH_NSD,
		chungtudonvi.fTruyThu_BHXH_TongCong,
		chungtudonvi.fTruyThu_BHYT_NLD,
		chungtudonvi.fTruyThu_BHYT_NSD,
		chungtudonvi.fTruyThu_BHYT_TongCong,
		chungtudonvi.fTruyThu_BHTN_NLD,
		chungtudonvi.fTruyThu_BHTN_NSD,
		chungtudonvi.fTruyThu_BHTN_TongCong
	into hachtoan_tbl
	from
		(select
			BH_DM_MucLucNganSach.iID_MLNS,
			BH_DM_MucLucNganSach.iID_MLNS_Cha,
			BH_DM_MucLucNganSach.bHangCha,
			BH_DM_MucLucNganSach.sLNS,
			BH_DM_MucLucNganSach.sL,
			BH_DM_MucLucNganSach.sK,
			BH_DM_MucLucNganSach.sM,
			BH_DM_MucLucNganSach.sTM,
			BH_DM_MucLucNganSach.sTTM,
			BH_DM_MucLucNganSach.sNG,
			BH_DM_MucLucNganSach.sTNG,
			BH_DM_MucLucNganSach.sXauNoiMa,
			BH_DM_MucLucNganSach.sMoTa
		from BH_DM_MucLucNganSach 
		where sLNS = '9020002' -- Khối hạch toán
		and iNamLamViec = @NamLamViec
		and iID_MLNS_Cha is not null) mlns
		left join
		(select distinct
			ctgt.iID_QT_CTCT_GiaiThich,
			ctgt.iID_QTT_BHXH_ChungTu,
			ctgt.sNguoiTao,
			ctgt.sNguoiSua,
			ctgt.dNgayTao,
			ctgt.dNgaySua,
			ctgt.iID_MaDonVi,
			ctgt.iNamLamViec,
			ctgt.iQuyNam,
			ctgt.iQuyNamLoai,
			ctgt.sQuyNamMoTa,
			ctgt.sNoiDung,
			ctgt.fTruyThu_BHXH_NLD,
			ctgt.fTruyThu_BHXH_NSD,
			ctgt.fTruyThu_BHXH_TongCong,
			ctgt.fTruyThu_BHYT_NLD,
			ctgt.fTruyThu_BHYT_NSD,
			ctgt.fTruyThu_BHYT_TongCong,
			ctgt.fTruyThu_BHTN_NLD,
			ctgt.fTruyThu_BHTN_NSD,
			ctgt.fTruyThu_BHTN_TongCong,
			ctgt.iID_MLNS
		from BH_QTT_BHXH_CTCT_GiaiThich ctgt
		where ctgt.iLoaiGiaiThich = 2
			and ctgt.iQuyNam = @NamLamViec
			and ctgt.iID_MaDonVi = @MaDonVi) chungtudonvi 
	on mlns.iID_MLNS = chungtudonvi.iID_MLNS
------------
	select
		dt.iID_MLNS,
		dt.iID_MLNS_Cha,
		dt.sMoTa,
		dt.bHangCha,
		dt.sLNS,
		dt.sXauNoiMa,
		isnull(dt.fTruyThu_BHXH_NLD, 0) fTruyThu_BHXH_NLD_DT,
		isnull(dt.fTruyThu_BHXH_NSD, 0) fTruyThu_BHXH_NSD_DT,
		isnull(dt.fTruyThu_BHXH_TongCong, 0) fTruyThu_BHXH_TongCong_DT,
		isnull(dt.fTruyThu_BHYT_NLD, 0) fTruyThu_BHYT_NLD_DT,
		isnull(dt.fTruyThu_BHYT_NSD, 0) fTruyThu_BHYT_NSD_DT,
		isnull(dt.fTruyThu_BHYT_TongCong, 0) fTruyThu_BHYT_TongCong_DT,
		isnull(dt.fTruyThu_BHTN_NLD, 0) fTruyThu_BHTN_NLD_DT,
		isnull(dt.fTruyThu_BHTN_NSD, 0) fTruyThu_BHTN_NSD_DT,
		isnull(dt.fTruyThu_BHTN_TongCong, 0) fTruyThu_BHTN_TongCong_DT,
		
		isnull(ht.fTruyThu_BHXH_NLD, 0) fTruyThu_BHXH_NLD_HT,
		isnull(ht.fTruyThu_BHXH_NSD, 0) fTruyThu_BHXH_NSD_HT,
		isnull(ht.fTruyThu_BHXH_TongCong, 0) fTruyThu_BHXH_TongCong_HT,
		isnull(ht.fTruyThu_BHYT_NLD, 0) fTruyThu_BHYT_NLD_HT,
		isnull(ht.fTruyThu_BHYT_NSD, 0) fTruyThu_BHYT_NSD_HT,
		isnull(ht.fTruyThu_BHYT_TongCong, 0) fTruyThu_BHYT_TongCong_HT,
		isnull(ht.fTruyThu_BHTN_NLD, 0) fTruyThu_BHTN_NLD_HT,
		isnull(ht.fTruyThu_BHTN_NSD, 0) fTruyThu_BHTN_NSD_HT,
		isnull(ht.fTruyThu_BHTN_TongCong, 0) fTruyThu_BHTN_TongCong_HT,

		(isnull(dt.fTruyThu_BHXH_TongCong, 0) + isnull(ht.fTruyThu_BHXH_TongCong, 0)) fTong_TruyThu_BHXH,
		(isnull(dt.fTruyThu_BHYT_TongCong, 0) + isnull(ht.fTruyThu_BHYT_TongCong, 0)) fTong_TruyThu_BHYT,
		(isnull(dt.fTruyThu_BHTN_TongCong, 0) + isnull(ht.fTruyThu_BHTN_TongCong, 0)) fTong_TruyThu_BHTN,

		(isnull(dt.fTruyThu_BHXH_TongCong, 0) + isnull(ht.fTruyThu_BHXH_TongCong, 0) + isnull(dt.fTruyThu_BHYT_TongCong, 0) + isnull(ht.fTruyThu_BHYT_TongCong, 0) + isnull(dt.fTruyThu_BHTN_TongCong, 0) + isnull(ht.fTruyThu_BHTN_TongCong, 0)) fTong_TruyThu

	from
		dutoan_tbl dt left join
		hachtoan_tbl ht on dt.iID_MLNS = ht.iID_MLNS
	order by dt.sXauNoiMa
--------

END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_nam]    Script Date: 11/14/2023 10:58:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_nam] 
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IsTongHop bit,
	@IQuy int
AS
BEGIN
	select
		chungtudonvi.iID_QTT_BHXH_ChungTu_ChiTiet,
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sLNS,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.sTNG,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		chungtudonvi.iQSBQNam,
		chungtudonvi.fLuongChinh/@Donvitinh fLuongChinh,
		chungtudonvi.fPCChucVu/@Donvitinh fPCChucVu,
		chungtudonvi.fPCTNNghe/@Donvitinh fPCTNNghe,
		chungtudonvi.fPCTNVuotKhung/@Donvitinh fPCTNVuotKhung,
		chungtudonvi.fNghiOm/@Donvitinh fNghiOm,
		chungtudonvi.fHSBL/@Donvitinh fHSBL,
		(chungtudonvi.fLuongChinh + chungtudonvi.fPCChucVu + chungtudonvi.fPCTNNghe + chungtudonvi.fPCTNVuotKhung + chungtudonvi.fNghiOm + chungtudonvi.fHSBL)/@Donvitinh fTongQTLN,
		chungtudonvi.fDuToan,
		chungtudonvi.fDaQuyetToan,
		chungtudonvi.fConLai,
		chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
		chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD)/@Donvitinh FTongSoPhaiThuBHXH,
		chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
		chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
		(chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fTongSoPhaiThuBHYT,
		chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
		chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
		(chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongSoPhaiThuBHTN,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongCong
		from
		(select
			BH_DM_MucLucNganSach.iID_MLNS,
			BH_DM_MucLucNganSach.iID_MLNS_Cha,
			BH_DM_MucLucNganSach.bHangCha,
			BH_DM_MucLucNganSach.sLNS,
			BH_DM_MucLucNganSach.sL,
			BH_DM_MucLucNganSach.sK,
			BH_DM_MucLucNganSach.sM,
			BH_DM_MucLucNganSach.sTM,
			BH_DM_MucLucNganSach.sTTM,
			BH_DM_MucLucNganSach.sNG,
			BH_DM_MucLucNganSach.sTNG,
			BH_DM_MucLucNganSach.sXauNoiMa,
			BH_DM_MucLucNganSach.sMoTa
		from BH_DM_MucLucNganSach 
		where sLNS like '902%'
		and iNamLamViec = @NamLamViec
		union select NewId() iID_MLNS, (select distinct iID_MLNS_Cha from BH_DM_MucLucNganSach where sLNS = '9020001' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 1 bHangCha, '' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020001-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
		union select NewId() iID_MLNS, (select distinct iID_MLNS_Cha from BH_DM_MucLucNganSach where sLNS = '9020002' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 1 bHangCha, '' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020002-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
		) mlns
		left join
		(select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.*
			from
			BH_QTT_BHXH_ChungTu ct
			join
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			where --ct.iNamLamViec = @NamLamViec
			ct.iQuyNam = @IQuy
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = 1
			and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		
		order by mlns.sXauNoiMa
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]    Script Date: 11/14/2023 10:58:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IsTongHop bit,
	@IQuy int
AS
BEGIN
	select
		chungtudonvi.iID_QTT_BHXH_ChungTu_ChiTiet,
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sLNS,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.sTNG,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		chungtudonvi.iQSBQNam,
		chungtudonvi.fLuongChinh/@Donvitinh fLuongChinh,
		chungtudonvi.fPCChucVu/@Donvitinh fPCChucVu,
		chungtudonvi.fPCTNNghe/@Donvitinh fPCTNNghe,
		chungtudonvi.fPCTNVuotKhung/@Donvitinh fPCTNVuotKhung,
		chungtudonvi.fNghiOm/@Donvitinh fNghiOm,
		chungtudonvi.fHSBL/@Donvitinh fHSBL,
		(chungtudonvi.fLuongChinh + chungtudonvi.fPCChucVu + chungtudonvi.fPCTNNghe + chungtudonvi.fPCTNVuotKhung + chungtudonvi.fNghiOm + chungtudonvi.fHSBL)/@Donvitinh fTongQTLN,
		chungtudonvi.fDuToan,
		chungtudonvi.fDaQuyetToan,
		chungtudonvi.fConLai,
		chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
		chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fTongSoPhaiThuBHXH,
		chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
		chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
		(chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fTongSoPhaiThuBHYT,
		chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
		chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
		(chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongSoPhaiThuBHTN,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHTN_NLD) fTongNLD,
		(chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NSD) fTongNSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongCong
		from
		(select
			BH_DM_MucLucNganSach.iID_MLNS,
			BH_DM_MucLucNganSach.iID_MLNS_Cha,
			BH_DM_MucLucNganSach.bHangCha,
			BH_DM_MucLucNganSach.sLNS,
			BH_DM_MucLucNganSach.sL,
			BH_DM_MucLucNganSach.sK,
			BH_DM_MucLucNganSach.sM,
			BH_DM_MucLucNganSach.sTM,
			BH_DM_MucLucNganSach.sTTM,
			BH_DM_MucLucNganSach.sNG,
			BH_DM_MucLucNganSach.sTNG,
			BH_DM_MucLucNganSach.sXauNoiMa,
			BH_DM_MucLucNganSach.sMoTa
		from BH_DM_MucLucNganSach 
		where sLNS like '902%'
		and iNamLamViec = @NamLamViec) mlns
		left join
		(select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.*
			from
			BH_QTT_BHXH_ChungTu ct
			join
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = 1
			and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		
		order by mlns.sXauNoiMa
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_nam]    Script Date: 11/14/2023 10:58:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_nam] 
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IsTongHop bit,
	@IQuy int
AS
BEGIN
	select
		chungtudonvi.iID_QTT_BHXH_ChungTu_ChiTiet,
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sLNS,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.sTNG,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		chungtudonvi.iQSBQNam,
		chungtudonvi.fLuongChinh/@Donvitinh fLuongChinh,
		chungtudonvi.fPCChucVu/@Donvitinh fPCChucVu,
		chungtudonvi.fPCTNNghe/@Donvitinh fPCTNNghe,
		chungtudonvi.fPCTNVuotKhung/@Donvitinh fPCTNVuotKhung,
		chungtudonvi.fNghiOm/@Donvitinh fNghiOm,
		chungtudonvi.fHSBL/@Donvitinh fHSBL,
		(chungtudonvi.fLuongChinh + chungtudonvi.fPCChucVu + chungtudonvi.fPCTNNghe + chungtudonvi.fPCTNVuotKhung + chungtudonvi.fNghiOm + chungtudonvi.fHSBL)/@Donvitinh fTongQTLN,
		chungtudonvi.fDuToan,
		chungtudonvi.fDaQuyetToan,
		chungtudonvi.fConLai,
		chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
		chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD)/@Donvitinh FTongSoPhaiThuBHXH,
		chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
		chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
		(chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fTongSoPhaiThuBHYT,
		chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
		chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
		(chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongSoPhaiThuBHTN,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongCong
		from
		(select
			BH_DM_MucLucNganSach.iID_MLNS,
			BH_DM_MucLucNganSach.iID_MLNS_Cha,
			BH_DM_MucLucNganSach.bHangCha,
			BH_DM_MucLucNganSach.sLNS,
			BH_DM_MucLucNganSach.sL,
			BH_DM_MucLucNganSach.sK,
			BH_DM_MucLucNganSach.sM,
			BH_DM_MucLucNganSach.sTM,
			BH_DM_MucLucNganSach.sTTM,
			BH_DM_MucLucNganSach.sNG,
			BH_DM_MucLucNganSach.sTNG,
			BH_DM_MucLucNganSach.sXauNoiMa,
			BH_DM_MucLucNganSach.sMoTa
		from BH_DM_MucLucNganSach 
		where sLNS like '902%'
		and iNamLamViec = @NamLamViec
		union select NewId() iID_MLNS, (select distinct iID_MLNS_Cha from BH_DM_MucLucNganSach where sLNS = '9020001' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 1 bHangCha, '' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020001-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
		union select NewId() iID_MLNS, (select distinct iID_MLNS_Cha from BH_DM_MucLucNganSach where sLNS = '9020002' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 1 bHangCha, '' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020002-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
		) mlns
		left join
		(select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.*
			from
			BH_QTT_BHXH_ChungTu ct
			join
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			where --ct.iNamLamViec = @NamLamViec
			ct.iQuyNam = @IQuy
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = 2
			--and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		
		order by mlns.sXauNoiMa
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_quy]    Script Date: 11/14/2023 10:58:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_quy]
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IsTongHop bit,
	@IQuy int
AS
BEGIN
	select
		chungtudonvi.iID_QTT_BHXH_ChungTu_ChiTiet,
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sLNS,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.sTNG,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		chungtudonvi.iQSBQNam,
		chungtudonvi.fLuongChinh/@Donvitinh fLuongChinh,
		chungtudonvi.fPCChucVu/@Donvitinh fPCChucVu,
		chungtudonvi.fPCTNNghe/@Donvitinh fPCTNNghe,
		chungtudonvi.fPCTNVuotKhung/@Donvitinh fPCTNVuotKhung,
		chungtudonvi.fNghiOm/@Donvitinh fNghiOm,
		chungtudonvi.fHSBL/@Donvitinh fHSBL,
		(chungtudonvi.fLuongChinh + chungtudonvi.fPCChucVu + chungtudonvi.fPCTNNghe + chungtudonvi.fPCTNVuotKhung + chungtudonvi.fNghiOm + chungtudonvi.fHSBL)/@Donvitinh fTongQTLN,
		chungtudonvi.fDuToan,
		chungtudonvi.fDaQuyetToan,
		chungtudonvi.fConLai,
		chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
		chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fTongSoPhaiThuBHXH,
		chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
		chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
		(chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fTongSoPhaiThuBHYT,
		chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
		chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
		(chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongSoPhaiThuBHTN,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHTN_NLD) fTongNLD,
		(chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NSD) fTongNSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongCong
		from
		(select
			BH_DM_MucLucNganSach.iID_MLNS,
			BH_DM_MucLucNganSach.iID_MLNS_Cha,
			BH_DM_MucLucNganSach.bHangCha,
			BH_DM_MucLucNganSach.sLNS,
			BH_DM_MucLucNganSach.sL,
			BH_DM_MucLucNganSach.sK,
			BH_DM_MucLucNganSach.sM,
			BH_DM_MucLucNganSach.sTM,
			BH_DM_MucLucNganSach.sTTM,
			BH_DM_MucLucNganSach.sNG,
			BH_DM_MucLucNganSach.sTNG,
			BH_DM_MucLucNganSach.sXauNoiMa,
			BH_DM_MucLucNganSach.sMoTa
		from BH_DM_MucLucNganSach 
		where sLNS like '902%'
		and iNamLamViec = @NamLamViec) mlns
		left join
		(select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.*
			from
			BH_QTT_BHXH_ChungTu ct
			join
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = 2
			--and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		order by mlns.sXauNoiMa
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_get_chung_tu_don_vi]    Script Date: 11/14/2023 10:58:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qttm_get_chung_tu_don_vi]
	@YearOfWork int,
	@LoaiTongHop int,
	@DaTongHop bit,
	@QuyNam int
AS
BEGIN
	SELECT
		qtt.iID_QTTM_BHYT_ChungTu,
		qtt.sSoChungTu,
		qtt.iID_MaDonVi IIDMaDonVi,
		donvi.sTenDonVi,
		qtt.iQuyNam,
		qtt.iLoaiTongHop,
		qtt.bIsKhoa
	FROM BH_QTTM_BHYT_Chung_Tu qtt
		LEFT JOIN DonVi donvi
		ON qtt.iID_MaDonVi = donvi.iID_MaDonVi
	WHERE donvi.iNamLamViec = @YearOfWork
		AND qtt.iNamLamViec = @YearOfWork
		AND qtt.bDaTongHop = @DaTongHop
		AND qtt.iQuyNam = @QuyNam
		AND qtt.iLoaiTongHop = @LoaiTongHop
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_get_chung_tu_don_vi_tong_hop]    Script Date: 11/14/2023 10:58:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qttm_get_chung_tu_don_vi_tong_hop]
	@YearOfWork int,
	@LoaiTongHop int,
	@UserName nvarchar(100),
	@QuyNam int
AS
BEGIN
	DECLARE @CountDonViCha int;
	SELECT 
		@CountDonViCha = count(*) FROM (SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName AND iNamLamViec = @YearOfWork AND iTrangThai = 1) nddv
	INNER JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1 AND iLoai = 0) dv
	ON dv.iID_MaDonVi = nddv.iID_MaDonVi;

	SELECT
		qtt.iID_QTTM_BHYT_ChungTu,
		qtt.sSoChungTu,
		qtt.iID_MaDonVi IIDMaDonVi,
		donvi.sTenDonVi,
		qtt.iQuyNam,
		qtt.iLoaiTongHop,
		qtt.bIsKhoa
	INTO #tblChungTuQTT
	FROM BH_QTTM_BHYT_Chung_Tu qtt
		LEFT JOIN DonVi donvi
		ON qtt.iID_MaDonVi = donvi.iID_MaDonVi
	WHERE donvi.iNamLamViec = @YearOfWork
		AND qtt.iNamLamViec = @YearOfWork
		AND qtt.iLoaiTongHop = @LoaiTongHop
		AND qtt.iQuyNam = @QuyNam

	IF @CountDonViCha = 0
		SELECT 
			ct.*
		FROM #tblChungTuQTT ct
		INNER JOIN 
			(SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork and iTrangThai = 1) dv
		ON ct.IIDMaDonVi = dv.iID_MaDonVi
		ORDER BY sSoChungTu desc;
	ELSE
		SELECT 
			ct.*
		FROM #tblChungTuQTT ct
		LEFT JOIN 
			(SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork and iTrangThai = 1) dv
		ON ct.IIDMaDonVi = dv.iID_MaDonVi
		WHERE ((dv.iID_MaDonVi IS NULL AND ct.bIsKhoa = 1) OR (dv.iID_MaDonVi IS NOT NULL))
		ORDER BY sSoChungTu desc;

	drop table #tblChungTuQTT;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_rpt_bhyt_than_nhan_tong_hop]    Script Date: 11/14/2023 10:58:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qttm_rpt_bhyt_than_nhan_tong_hop]
	@NamLamViec int,
	@IsTongHop bit,
	@LstSelectedUnit ntext,
	@ThanNhanQuanNhan nvarchar(50),
	@ThanNhanCNVQP nvarchar(50),
	@Dvt int
AS
BEGIN
	declare @TNQN_TMP_TH table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), fSoPhaiThu float);
	declare @TN_CNVQP_TMP_TH table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), fSoPhaiThu float);

	-- Lấy dữ liệu số phải thu thân nhân quân nhân theo đơn vị
	INSERT INTO @TNQN_TMP_TH (IdDonVi, TenDonVI, fSoPhaiThu)
		SELECT
			dt_dv.id,
			dt_dv.sTenDonVi,
		   SUM(IsNull(A.fSoPhaiThu, 0)) ThanhTien
			FROM
			  (SELECT
					   ml.sMoTa,
					   ct.iID_MaDonVi,
					   IsNull(ctct.fSoPhaiThu, 0) fSoPhaiThu,
					   ml.sLNS
			   FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
			   LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
			   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
			   AND ml.iNamLamViec = @NamLamViec
			   AND ml.iTrangThai = 1
			   AND ml.sLNS = @ThanNhanQuanNhan
			   WHERE ct.iNamLamViec = @NamLamViec
					AND ct.iQuyNam = @NamLamViec
					AND ct.iLoaiTongHop = 2
					--AND ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
					) AS A 
			   JOIN
			  (SELECT iID_MaDonVi AS id,
					  sTenDonVi, iLoai
			   FROM DonVi
			   WHERE iTrangThai = 1
			   AND iNamLamViec = @NamLamViec
			   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id
			GROUP BY
			dt_dv.sTenDonVi,
			dt_dv.id;

		-- Lấy dữ liệu số phải thu thân nhân CNVCQP theo đơn vị
		INSERT INTO @TN_CNVQP_TMP_TH (IdDonVi, TenDonVI, fSoPhaiThu)
			SELECT
				dt_dv.id,
				dt_dv.sTenDonVi,
			   SUM(IsNull(A.fSoPhaiThu, 0)) ThanhTien
				FROM
				  (SELECT
						   ml.sMoTa,
						   ct.iID_MaDonVi,
						   IsNull(ctct.fSoPhaiThu, 0) fSoPhaiThu,
						   ml.sLNS
				   FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
				   LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
				   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
				   AND ml.iNamLamViec = @NamLamViec
				   AND ml.iTrangThai = 1
				   AND ml.sLNS = @ThanNhanCNVQP
				   WHERE ct.iNamLamViec = @NamLamViec
						AND ct.iQuyNam = @NamLamViec
						AND ct.iLoaiTongHop = 2
						--AND ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
						) AS A 
				   JOIN
				  (SELECT iID_MaDonVi AS id,
						  sTenDonVi, iLoai
				   FROM DonVi
				   WHERE iTrangThai = 1
				   AND iNamLamViec = @NamLamViec
				   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id
				GROUP BY
				dt_dv.sTenDonVi,
				dt_dv.id;

		-- Kết quả
		SELECT result.idDonVi, 
			result.TenDonVI sTenDonVi, 
			result.fSoPhaiThuTNQN/@dvt fSoPhaiThuTNQN, 
			result.fSoPhaiThuTNCNVQP/@dvt fSoPhaiThuTNCNVQP,
			result.fTongCong/@dvt fTongCong
		FROM
			(SELECT tnqn.idDonVi, 
				tnqn.TenDonVI,
				IsNull(tnqn.fSoPhaiThu, 0) fSoPhaiThuTNQN,
				IsNull(tncnvqp.fSoPhaiThu, 0) fSoPhaiThuTNCNVQP,
				IsNull(tnqn.fSoPhaiThu, 0) + IsNull(tncnvqp.fSoPhaiThu, 0) fTongCong
				FROM @TNQN_TMP_TH tnqn
				LEFT JOIN @TN_CNVQP_TMP_TH tncnvqp ON tnqn.idDonVi = tncnvqp.idDonVi) result
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_rpt_qttm_bhyt_than_nhan_nld_tong_hop]    Script Date: 11/14/2023 10:58:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qttm_rpt_qttm_bhyt_than_nhan_nld_tong_hop]
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IsTongHop bit,
	@IQuy int
AS
BEGIN
	select
		chungtudonvi.iID_QTTM_BHYT_ChungTu_ChiTiet,
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sLNS,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.sTNG,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		chungtudonvi.fDuToan/@Donvitinh fDuToan,
		chungtudonvi.fDaQuyetToan/@Donvitinh fDaQuyetToan,
		chungtudonvi.fConLai/@Donvitinh fConLai,
		chungtudonvi.fSoPhaiThu/@Donvitinh fSoPhaiThu
		from
		(select
			BH_DM_MucLucNganSach.iID_MLNS,
			BH_DM_MucLucNganSach.iID_MLNS_Cha,
			BH_DM_MucLucNganSach.bHangCha,
			BH_DM_MucLucNganSach.sLNS,
			BH_DM_MucLucNganSach.sL,
			BH_DM_MucLucNganSach.sK,
			BH_DM_MucLucNganSach.sM,
			BH_DM_MucLucNganSach.sTM,
			BH_DM_MucLucNganSach.sTTM,
			BH_DM_MucLucNganSach.sNG,
			BH_DM_MucLucNganSach.sTNG,
			BH_DM_MucLucNganSach.sXauNoiMa,
			BH_DM_MucLucNganSach.sMoTa
		from BH_DM_MucLucNganSach 
		where sLNS like '903%'
		and iNamLamViec = @NamLamViec) mlns
		left join
		(select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.*
			from
			BH_QTTM_BHYT_Chung_Tu ct
			join
			BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct on ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = 2
			--and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		order by mlns.sXauNoiMa
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_hssv]    Script Date: 11/14/2023 10:58:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_hssv]
	@NamLamViec int,
	@IsTongHop bit,
	@LstSelectedUnit ntext,
	@HSSV nvarchar(50),
	@LuuHS nvarchar(50),
	@HVSQ nvarchar(50),
	@SQDuBi nvarchar(50),
	@Dvt int
AS
BEGIN
	declare @Tbl_HSSV_QTTM table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_HSSV float);
	declare @Tbl_LuuHS_QTTM table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_LuuHS float);
	declare @Tbl_HVQS_QTTM table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_HVQS float);
	declare @Tbl_SQDuBi_QTTM table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_SQDuBi float);

	INSERT INTO @Tbl_HSSV_QTTM (IdDonVi, TenDonVI, ThanhTien_HSSV)
	SELECT 
		dt_dv.id,
		dt_dv.sTenDonVi,
	   SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ct.iID_MaDonVi,
				   IsNull(ctct.fSoPhaiThu, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
		   LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @hSSV
		   WHERE ct.iNamLamViec = @namLamViec
					AND ct.iQuyNam = @namLamViec
					AND ct.iLoaiTongHop = 1
					AND ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @namLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id
		GROUP BY
		dt_dv.sTenDonVi,
		dt_dv.id;

	INSERT INTO @Tbl_LuuHS_QTTM (IdDonVi, TenDonVI, ThanhTien_LuuHS)
	SELECT 
	   dt_dv.id,
		dt_dv.sTenDonVi,
	   SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ct.iID_MaDonVi,
				   IsNull(ctct.fSoPhaiThu, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
		   LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @luuHS
		   WHERE ct.iNamLamViec = @namLamViec
					AND ct.iQuyNam = @namLamViec
					AND ct.iLoaiTongHop = 1
					AND ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @namLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id
		GROUP BY
		dt_dv.sTenDonVi,
		dt_dv.id;

	INSERT INTO @Tbl_HVQS_QTTM (IdDonVi, TenDonVI, ThanhTien_HVQS)
	SELECT 
	   dt_dv.id,
		dt_dv.sTenDonVi,
	   SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ct.iID_MaDonVi,
				   IsNull(ctct.fSoPhaiThu, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
		   LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @hVSQ
		   WHERE ct.iNamLamViec = @namLamViec
					AND ct.iQuyNam = @namLamViec
					AND ct.iLoaiTongHop = 1
					AND ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @namLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id
		GROUP BY
		dt_dv.sTenDonVi,
		dt_dv.id;

	INSERT INTO @Tbl_SQDuBi_QTTM (IdDonVi, TenDonVI, ThanhTien_SQDuBi)
	SELECT 
	   dt_dv.id,
		dt_dv.sTenDonVi,
	   SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ct.iID_MaDonVi,
				   IsNull(ctct.fSoPhaiThu, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
		   LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @sQDuBi
		   WHERE ct.iNamLamViec = @namLamViec
					AND ct.iQuyNam = @namLamViec
					AND ct.iLoaiTongHop = 1
					AND ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @namLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id
		GROUP BY
		dt_dv.sTenDonVi,
		dt_dv.id;

	SELECT result.idDonVi, 
		result.TenDonVI STenDonVi, 
		result.HSSV/@dvt fHSSV, 
		result.LuuHS/@dvt fLuuHS,
		result.TongHSSV/@dvt fTongHSSV,
		result.HVQS/@dvt fHVQS,
		result.SQDuBi/@dvt fSQDuBi,
		(result.TongHSSV + result.HVQS + result.SQDuBi)/@dvt fTongCongHSSV
		FROM
		(SELECT hssv.idDonVi, 
		hssv.TenDonVI,
		IsNull(hssv.ThanhTien_HSSV, 0) HSSV,
		IsNull(luuhs.ThanhTien_LuuHS, 0) LuuHS,
		(IsNull(hssv.ThanhTien_HSSV + luuhs.ThanhTien_LuuHS, 0)) TongHSSV,
		IsNull(hvsq.ThanhTien_HVQS, 0) HVQS,
		IsNull(sqdb.ThanhTien_SQDuBi, 0) SQDuBi
		FROM @Tbl_HSSV_QTTM hssv
		LEFT JOIN @Tbl_LuuHS_QTTM luuhs ON hssv.idDonVi = luuhs.idDonVi
		LEFT JOIN @Tbl_HVQS_QTTM hvsq ON hssv.idDonVi = hvsq.idDonVi
		LEFT JOIN @Tbl_SQDuBi_QTTM sqdb ON hssv.idDonVi = sqdb.idDonVi) result
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan]    Script Date: 11/14/2023 10:58:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan]
	@NamLamViec int,
	@IsTongHop bit,
	@LstSelectedUnit ntext,
	@ThanNhanQuanNhan nvarchar(50),
	@ThanNhanCNVQP nvarchar(50),
	@Dvt int
AS
BEGIN
	declare @TNQN_TMP table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), fSoPhaiThu float);
	declare @TN_CNVQP_TMP table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), fSoPhaiThu float);

	-- Lấy dữ liệu số phải thu thân nhân quân nhân theo đơn vị
	INSERT INTO @TNQN_TMP (IdDonVi, TenDonVI, fSoPhaiThu)
		SELECT
			dt_dv.id,
			dt_dv.sTenDonVi,
		   SUM(IsNull(A.fSoPhaiThu, 0)) ThanhTien
			FROM
			  (SELECT
					   ml.sMoTa,
					   ct.iID_MaDonVi,
					   IsNull(ctct.fSoPhaiThu, 0) fSoPhaiThu,
					   ml.sLNS
			   FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
			   LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
			   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
			   AND ml.iNamLamViec = @NamLamViec
			   AND ml.iTrangThai = 1
			   AND ml.sLNS = @ThanNhanQuanNhan
			   WHERE ct.iNamLamViec = @NamLamViec
					AND ct.iQuyNam = @NamLamViec
					AND ct.iLoaiTongHop = 1
					AND ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end) AS A 
			   JOIN
			  (SELECT iID_MaDonVi AS id,
					  sTenDonVi, iLoai
			   FROM DonVi
			   WHERE iTrangThai = 1
			   AND iNamLamViec = @NamLamViec
			   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id
			GROUP BY
			dt_dv.sTenDonVi,
			dt_dv.id;

		-- Lấy dữ liệu số phải thu thân nhân CNVCQP theo đơn vị
		INSERT INTO @TN_CNVQP_TMP (IdDonVi, TenDonVI, fSoPhaiThu)
			SELECT
				dt_dv.id,
				dt_dv.sTenDonVi,
			   SUM(IsNull(A.fSoPhaiThu, 0)) ThanhTien
				FROM
				  (SELECT
						   ml.sMoTa,
						   ct.iID_MaDonVi,
						   IsNull(ctct.fSoPhaiThu, 0) fSoPhaiThu,
						   ml.sLNS
				   FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
				   LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
				   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
				   AND ml.iNamLamViec = @NamLamViec
				   AND ml.iTrangThai = 1
				   AND ml.sLNS = @ThanNhanCNVQP
				   WHERE ct.iNamLamViec = @NamLamViec
						AND ct.iQuyNam = @NamLamViec
						AND ct.iLoaiTongHop = 1
						AND ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end) AS A 
				   JOIN
				  (SELECT iID_MaDonVi AS id,
						  sTenDonVi, iLoai
				   FROM DonVi
				   WHERE iTrangThai = 1
				   AND iNamLamViec = @NamLamViec
				   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id
				GROUP BY
				dt_dv.sTenDonVi,
				dt_dv.id;

		-- Kết quả
		SELECT result.idDonVi, 
			result.TenDonVI sTenDonVi, 
			result.fSoPhaiThuTNQN/@dvt fSoPhaiThuTNQN, 
			result.fSoPhaiThuTNCNVQP/@dvt fSoPhaiThuTNCNVQP,
			result.fTongCong/@dvt fTongCong
		FROM
			(SELECT tnqn.idDonVi, 
				tnqn.TenDonVI,
				IsNull(tnqn.fSoPhaiThu, 0) fSoPhaiThuTNQN,
				IsNull(tncnvqp.fSoPhaiThu, 0) fSoPhaiThuTNCNVQP,
				IsNull(tnqn.fSoPhaiThu, 0) + IsNull(tncnvqp.fSoPhaiThu, 0) fTongCong
				FROM @TNQN_TMP tnqn
				LEFT JOIN @TN_CNVQP_TMP tncnvqp ON tnqn.idDonVi = tncnvqp.idDonVi) result
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan_nld]    Script Date: 11/14/2023 10:58:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan_nld]
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IsTongHop bit,
	@IQuy int
AS
BEGIN
	select
		chungtudonvi.iID_QTTM_BHYT_ChungTu_ChiTiet,
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sLNS,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.sTNG,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		chungtudonvi.fDuToan/@Donvitinh fDuToan,
		chungtudonvi.fDaQuyetToan/@Donvitinh fDaQuyetToan,
		chungtudonvi.fConLai/@Donvitinh fConLai,
		chungtudonvi.fSoPhaiThu/@Donvitinh fSoPhaiThu
		from
		(select
			BH_DM_MucLucNganSach.iID_MLNS,
			BH_DM_MucLucNganSach.iID_MLNS_Cha,
			BH_DM_MucLucNganSach.bHangCha,
			BH_DM_MucLucNganSach.sLNS,
			BH_DM_MucLucNganSach.sL,
			BH_DM_MucLucNganSach.sK,
			BH_DM_MucLucNganSach.sM,
			BH_DM_MucLucNganSach.sTM,
			BH_DM_MucLucNganSach.sTTM,
			BH_DM_MucLucNganSach.sNG,
			BH_DM_MucLucNganSach.sTNG,
			BH_DM_MucLucNganSach.sXauNoiMa,
			BH_DM_MucLucNganSach.sMoTa
		from BH_DM_MucLucNganSach 
		where sLNS like '903%'
		and iNamLamViec = @NamLamViec) mlns
		left join
		(select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.*
			from
			BH_QTTM_BHYT_Chung_Tu ct
			join
			BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct on ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = 1
			and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		
		order by mlns.sXauNoiMa
END
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh]    Script Date: 11/14/2023 10:58:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh]
@ChungTuId NVARCHAR(255),
@DonViTinh int
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

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

	select result.* into tbl_ddt_bhxh from
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

	select STT, MaSo, NoiDung, DttDauNam/@DonViTinh DttDauNam, Dtt6ThangDauNam/@DonViTinh Dtt6ThangDauNam, Dtt6ThangCuoiNam/@DonViTinh Dtt6ThangCuoiNam, TongCong/@DonViTinh TongCong, Tang/@DonViTinh Tang, Giam/@DonViTinh Giam, Khoi, Thu, Type, BHangCha from tbl_ddt_bhxh;

END
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bang_luong_thang_bhxh_chitiet]    Script Date: 11/14/2023 10:58:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_tl_bang_luong_thang_bhxh_chitiet]
	@Id UNIQUEIDENTIFIER
AS
DECLARE @cols AS NVARCHAR(MAX);
DECLARE @query  AS NVARCHAR(MAX);

SET @cols = STUFF(
  (
    SELECT
      DISTINCT ',' + QUOTENAME(chedo.sMaCheDo) 
    FROM TL_DM_CheDoBHXH chedo where chedo.sMaCheDoCha is not null and sMaCheDoCha <> ''
        FOR XML PATH(''), TYPE
  ).value('.', 'NVARCHAR(MAX)'),1,1,'')
  
SET @query = '
	WITH ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo	AS MaCanBo,
			canBo.Ten_CanBo	AS TenCanBo,
			donVi.Ma_DonVi	AS MaDonVi,
			donVi.Ten_DonVi	AS TenDonVi
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent = donVi.Ma_DonVi
		WHERE
			canBo.IsDelete = 1
			AND canBo.Khong_Luong = 0
	)
	SELECT Thang, Nam, MaCanBo, TenCanBo, TenDonVi, ' + @cols + ' FROM (
		SELECT
			bangLuongThang.iThang		AS Thang,
			bangLuongThang.iNam			AS Nam,
			canBo.MaCanBo				AS MaCanBo,
			canBo.TenCanBo				AS TenCanBo,
			canBo.TenDonVi				AS TenDonVi,
			bangLuongThang.nGiaTri		AS GiaTri,
			bangLuongThang.sMaCheDo	AS MaCheDo
		FROM TL_BangLuong_ThangBHXH bangLuongThang
		INNER JOIN ThongTinCanBo canBo
			ON bangLuongThang.sMaCBo = canBo.MaCanBo
		WHERE
			bangLuongThang.iID_Parent = + ''' + CONVERT(NVARCHAR(100), @Id) + '''
	) x
	PIVOT 
	(
		SUM(GiaTri)
		FOR MaCheDo IN (' + @cols + ')
	) p '
execute(@query)
;
;
GO
