/****** Object:  StoredProcedure [dbo].[sp_khtm_bhyt_chi_tiet]    Script Date: 10/25/2023 9:03:57 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khtm_bhyt_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khtm_bhyt_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chungtu_chitiet_tao_tonghop]    Script Date: 10/25/2023 9:03:57 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kht_bhxh_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kht_bhxh_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chi_tiet]    Script Date: 10/25/2023 9:03:57 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kht_bhxh_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kht_bhxh_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]    Script Date: 10/25/2023 9:03:57 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_truy_thu]    Script Date: 10/25/2023 9:03:57 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_giai_thich_truy_thu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_truy_thu]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_tong_hop_so_sanh]    Script Date: 10/25/2023 9:03:57 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_giai_thich_tong_hop_so_sanh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_tong_hop_so_sanh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_bang_loi]    Script Date: 10/25/2023 9:03:57 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_giai_thich_bang_loi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_bang_loi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_giai_thich_truy_thu]    Script Date: 10/25/2023 9:03:57 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_giai_thich_truy_thu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_giai_thich_truy_thu]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_giai_thich_tong_hop_so_sanh]    Script Date: 10/25/2023 9:03:57 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_giai_thich_tong_hop_so_sanh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_giai_thich_tong_hop_so_sanh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_cap_kp_kcb_get_tong_da_quyet_toan]    Script Date: 10/25/2023 9:03:57 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtc_cap_kp_kcb_get_tong_da_quyet_toan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtc_cap_kp_kcb_get_tong_da_quyet_toan]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_cap_kp_kcb_get_ke_hoach_cap]    Script Date: 10/25/2023 9:03:57 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtc_cap_kp_kcb_get_ke_hoach_cap]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtc_cap_kp_kcb_get_ke_hoach_cap]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet_dieu_chinh]    Script Date: 10/25/2023 9:03:57 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet_dieu_chinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet_dieu_chinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]    Script Date: 10/25/2023 9:03:57 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]    Script Date: 10/25/2023 9:03:57 AM ******/
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

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblMucLucNganSach]') AND type in (N'U'))
	drop table tblMucLucNganSach;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblDonVi]') AND type in (N'U'))
	drop table tblDonVi;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblChungTuNhanPhanBo]') AND type in (N'U'))
	drop table tblChungTuNhanPhanBo;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblChiTietDuToanNhan]') AND type in (N'U'))
	drop table tblChiTietDuToanNhan;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_tblChiTietDuToanNhan_MucLuc]') AND type in (N'U'))
	drop table tbl_tblChiTietDuToanNhan_MucLuc;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp]') AND type in (N'U'))
	drop table temp;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U'))
	drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblSoChuaPhanBo]') AND type in (N'U'))
	drop table tblSoChuaPhanBo;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblMucLucNganSach_duplicate]') AND type in (N'U'))
	drop table tblMucLucNganSach_duplicate;

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
	into temp
	from tbl_tblChiTietDuToanNhan_MucLuc cross join tblDonVi 

	---Map với bảng BH_DTT_BHXH_PhanBo_ChungTuChiTiet để lấy thông tin đã được phân bổ
	select 
		temp.iID_CTDuToan_Nhan, 
		chitiet_phanbo.iID_DTT_BHXH_ChungTu_ChiTiet,
		temp.iID_MLNS,
		temp.iID_MLNS_Cha,
		temp.sLNS,
		temp.sL,
		temp.sK,
		temp.sM,
		temp.sTM,
		temp.sTTM,
		temp.sNG,
		temp.sTNG,
		temp.sXauNoiMa,
		temp.sMoTa as sMoTa,
		ISNULL(chitiet_phanbo.fBHXH_NLD, 0) as fBHXHNLD,
		ISNULL(temp.fThuBHXHNLD, 0) as fBHXHNLDTruocDieuChinh,
		ISNULL(chitiet_phanbo.fBHXH_NSD, 0) as fBHXHNSD,
		ISNULL(temp.fThuBHXHNSD, 0) as fBHXHNSDTruocDieuChinh,
		ISNULL(chitiet_phanbo.fBHYT_NLD, 0) as fBHYTNLD,
		ISNULL(temp.fThuBHYTNLD, 0) as fBHYTNLDTruocDieuChinh,
		ISNULL(chitiet_phanbo.fBHYT_NSD, 0) as fBHYTNSD,
		ISNULL(temp.fThuBHYTNSD, 0) as fBHYTNSDTruocDieuChinh,
		ISNULL(chitiet_phanbo.fBHTN_NLD, 0) as fBHTNNLD,
		ISNULL(temp.fThuBHTNNLD, 0) as fBHTNNLDTruocDieuChinh,
		ISNULL(chitiet_phanbo.fBHTN_NSD, 0) as fBHTNNSD,
		temp.fThuBHTNNSD as fBHTNNSDTruocDieuChinh,
		3 as iRowType,
		temp.iID_MaDonVi,
		temp.sTenDonVi,
		temp.sSoQuyetDinh,
		0 as bHangCha,
		0 as IsRemainRow
	into temp1
	from temp
	left join 
		(
			select * 
			from BH_DTT_BHXH_PhanBo_ChungTuChiTiet 
			where iID_DTT_BHXH_ChungTu = @ChungTuId
		) as chitiet_phanbo
		on chitiet_phanbo.iID_CTDuToan_Nhan = temp.iID_CTDuToan_Nhan and chitiet_phanbo.iID_MaDonVi = temp.iID_MaDonVi and chitiet_phanbo.iID_MLNS = temp.iID_MLNS


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
		SELECT * from temp1
	) as result
	order by result.sXauNoiMa,
		result.sSoQuyetDinh,
		result.iID_MaDonVi,
		result.iRowType,
		result.IsRemainRow
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet_dieu_chinh]    Script Date: 10/25/2023 9:03:57 AM ******/
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

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblMucLucNganSach]') AND type in (N'U'))
	drop table tblMucLucNganSach
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblChungTuBiDieuChinh]') AND type in (N'U'))
	drop table tblChungTuBiDieuChinh
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblChungTuBiDieuChinh_Ct]') AND type in (N'U'))
	drop table tblChungTuBiDieuChinh_Ct
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblThongTinChungTu_BiDieuChinh]') AND type in (N'U'))
	drop table tblThongTinChungTu_BiDieuChinh
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblThongTinChungTu_DieuChinhThemMoi]') AND type in (N'U'))
	drop table tblThongTinChungTu_DieuChinhThemMoi;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblThongTinChungTu]') AND type in (N'U'))
	drop table tblThongTinChungTu;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblThongTinChungTu_MLNS]') AND type in (N'U'))
	drop table tblThongTinChungTu_MLNS;

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
	
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_cap_kp_kcb_get_ke_hoach_cap]    Script Date: 10/25/2023 9:03:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qtc_cap_kp_kcb_get_ke_hoach_cap]
	@NamLamViec int,
	@Quy int,
	@MaCSYT nvarchar(50)
AS
BEGIN
	select 
		ctct.iID_MLNS,
		sum(ctct.FTamUngQuyNay) fTamUngQuyNay
	from BH_CP_CapTamUng_KCB_BHYT_ChiTiet ctct
		join BH_CP_CapTamUng_KCB_BHYT ct on ct.iID_BH_CP_CapTamUng_KCB_BHYT = ctct.iID_BH_CP_CapTamUng_KCB_BHYT
	where ct.iNamLamViec = @NamLamViec
		and ct.iQuy = @Quy
		and ctct.iID_MaCoSoYTe = @MaCSYT
	group by
		ctct.iID_MLNS
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_cap_kp_kcb_get_tong_da_quyet_toan]    Script Date: 10/25/2023 9:03:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qtc_cap_kp_kcb_get_tong_da_quyet_toan] 
	@ChungTuId nvarchar(100),
	@NamLamViec int
AS
BEGIN
	SELECT ctctdqt.iID_MLNS,
		SUM(ctctdqt.fQuyetToanQuyNay) fDaQuyetToan
	FROM BH_QTC_CapKinhPhi_KCB ctdqt
		JOIN BH_QTC_CapKinhPhi_KCB_ChiTiet ctctdqt ON ctdqt.iID_ChungTu = ctctdqt.iID_ChungTu
	WHERE ctdqt.iNamLamViec = @NamLamViec
		AND ctdqt.iQuy < (SELECT iQuy from BH_QTC_CapKinhPhi_KCB WHERE iID_ChungTu = @ChungTuId)
	GROUP BY ctctdqt.iID_MLNS
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_giai_thich_tong_hop_so_sanh]    Script Date: 10/25/2023 9:03:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qtt_get_giai_thich_tong_hop_so_sanh]
	@NamLamViec int,
	@VoucherID uniqueidentifier,
	@MaDonVi nvarchar(50),
	@LoaiGiaiThich int
AS
BEGIN
	
		select
			--ml.iSTT,
			chungtudonvi.iID_QT_CTCT_GiaiThich,
			ml.iID_MLGT IIDMLNS,
			ml.sNoiDung,
			chungtudonvi.fTruyThu_BHXH_NLD FTruyThuBHXHNLD,
			chungtudonvi.fTruyThu_BHXH_NSD FTruyThuBHXHNSD,
			chungtudonvi.fTruyThu_BHXH_TongCong FTruyThuBHXHTongCong,
			chungtudonvi.fTruyThu_BHYT_NLD FTruyThuBHYTNLD,
			chungtudonvi.fTruyThu_BHYT_NSD FTruyThuBHYTNSD,
			chungtudonvi.fTruyThu_BHYT_TongCong FTruyThuBHYTTongCong,
			chungtudonvi.fTruyThu_BHTN_NLD FTruyThuBHTNNLD,
			chungtudonvi.fTruyThu_BHTN_NSD FTruyThuBHTNNSD,
			chungtudonvi.fTruyThu_BHTN_TongCong FTruyThuBHTNTongCong,
			chungtudonvi.fTongTruyThu_BHXH FTongTruyThuBHXH,
			chungtudonvi.sKienNghi,
			chungtudonvi.fPhaiNop_BHXH FPhaiNopBHXH,
			chungtudonvi.fPhaiNop_TrongQuyNam FPhaiNopTrongQuyNam,
			chungtudonvi.fTruyThu_QuyNamTruoc FTruyThuQuyNamTruoc,
			chungtudonvi.fPhaiNop_QuyNamTruoc FPhaiNopQuyNamTruoc,
			chungtudonvi.fDaNop_TrongQuyNam FDaNopTrongQuyNam,
			chungtudonvi.fConPhaiNopTiep,
			chungtudonvi.fSoPhaiThuNop,
			chungtudonvi.fSoDaNopTrongNam,
			chungtudonvi.fSoDaNopSau31_12 FSoDaNopSau3112,
			chungtudonvi.fTongSoDaNop,
			chungtudonvi.fSoConPhaiNop,
			chungtudonvi.iQuanSo,
			chungtudonvi.fQuyTienLuongCanCu,
			chungtudonvi.fSoTienGiamDong,
			chungtudonvi.dTuNgay,
			chungtudonvi.dDenNgay,
			chungtudonvi.sNguoiTao,
			chungtudonvi.dNgayTao,
			chungtudonvi.sNguoiSua,
			chungtudonvi.sNguoiSua
		from
		(select
			iSTT,
			iID_MLGT,
			sNoiDung,
			iLoai,
			iNamLamViec
		from BH_QTT_MucLucGiaiThich
		where iLoai = @LoaiGiaiThich) ml
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
			ctgt.sKienNghi,
			ctgt.fPhaiNop_BHXH,
			ctgt.fPhaiNop_TrongQuyNam,
			ctgt.fTruyThu_QuyNamTruoc,
			ctgt.fPhaiNop_QuyNamTruoc,
			ctgt.fDaNop_TrongQuyNam,
			ctgt.fConPhaiNopTiep,
			ctgt.fTruyThu_BHXH_NLD,
			ctgt.fTruyThu_BHXH_NSD,
			ctgt.fTruyThu_BHXH_TongCong,
			ctgt.fTruyThu_BHYT_NLD,
			ctgt.fTruyThu_BHYT_NSD,
			ctgt.fTruyThu_BHYT_TongCong,
			ctgt.fTruyThu_BHTN_NLD,
			ctgt.fTruyThu_BHTN_NSD,
			ctgt.fTruyThu_BHTN_TongCong,
			ctgt.fTongTruyThu_BHXH,
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
			ctgt.iID_MLNS
			from
			BH_QTT_BHXH_ChungTu ct
			join
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			join
			BH_QTT_BHXH_CTCT_GiaiThich ctgt on ct.iID_QTT_BHXH_ChungTu = ctgt.iID_QTT_BHXH_ChungTu
			where
			ct.iID_QTT_BHXH_ChungTu = @VoucherID
			and ctgt.iID_MaDonVi = @MaDonVi
				) chungtudonvi 
			on ml.iID_MLGT = chungtudonvi.iID_MLNS
		
		order by ml.iSTT

END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_giai_thich_truy_thu]    Script Date: 10/25/2023 9:03:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qtt_get_giai_thich_truy_thu]
	@NamLamViec int,
	@VoucherID uniqueidentifier,
	@MaDonVi nvarchar(50)
AS
BEGIN
	
	select
			chungtudonvi.iID_QT_CTCT_GiaiThich,
			mlns.iID_MLNS IIDMLNS,
			mlns.sMoTa sNoiDung,
			mlns.iID_MLNS_Cha IIDMLNSCha,
			mlns.bHangCha,
			mlns.sLNS,
			mlns.sXauNoiMa,
			mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			chungtudonvi.fTruyThu_BHXH_NLD FTruyThuBHXHNLD,
			chungtudonvi.fTruyThu_BHXH_NSD FTruyThuBHXHNSD,
			chungtudonvi.fTruyThu_BHXH_TongCong FTruyThuBHXHTongCong,
			chungtudonvi.fTruyThu_BHYT_NLD FTruyThuBHYTNLD,
			chungtudonvi.fTruyThu_BHYT_NSD FTruyThuBHYTNSD,
			chungtudonvi.fTruyThu_BHYT_TongCong FTruyThuBHYTTongCong,
			chungtudonvi.fTruyThu_BHTN_NLD FTruyThuBHTNNLD,
			chungtudonvi.fTruyThu_BHTN_NSD FTruyThuBHTNNSD,
			chungtudonvi.fTruyThu_BHTN_TongCong FTruyThuBHTNTongCong,
			(chungtudonvi.fTruyThu_BHXH_TongCong + chungtudonvi.fTruyThu_BHYT_TongCong + chungtudonvi.fTruyThu_BHTN_TongCong) FTongTruyThuBHXH,
			chungtudonvi.sKienNghi,
			chungtudonvi.fPhaiNop_BHXH FPhaiNopBHXH,
			chungtudonvi.fPhaiNop_TrongQuyNam FPhaiNopTrongQuyNam,
			chungtudonvi.fTruyThu_QuyNamTruoc FTruyThuQuyNamTruoc,
			chungtudonvi.fPhaiNop_QuyNamTruoc FPhaiNopQuyNamTruoc,
			chungtudonvi.fDaNop_TrongQuyNam FDaNopTrongQuyNam,
			chungtudonvi.fConPhaiNopTiep,
			chungtudonvi.fSoPhaiThuNop,
			chungtudonvi.fSoDaNopTrongNam,
			chungtudonvi.fSoDaNopSau31_12,
			chungtudonvi.fTongSoDaNop,
			chungtudonvi.fSoConPhaiNop,
			chungtudonvi.iQuanSo,
			chungtudonvi.fQuyTienLuongCanCu,
			chungtudonvi.fSoTienGiamDong,
			chungtudonvi.dTuNgay,
			chungtudonvi.dDenNgay,
			chungtudonvi.sNguoiTao,
			chungtudonvi.dNgayTao,
			chungtudonvi.sNguoiSua,
			chungtudonvi.sNguoiSua
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
				ctgt.sKienNghi,
				ctgt.fPhaiNop_BHXH,
				ctgt.fPhaiNop_TrongQuyNam,
				ctgt.fTruyThu_QuyNamTruoc,
				ctgt.fPhaiNop_QuyNamTruoc,
				ctgt.fDaNop_TrongQuyNam,
				ctgt.fConPhaiNopTiep,
				ctgt.fTruyThu_BHXH_NLD,
				ctgt.fTruyThu_BHXH_NSD,
				ctgt.fTruyThu_BHXH_TongCong,
				ctgt.fTruyThu_BHYT_NLD,
				ctgt.fTruyThu_BHYT_NSD,
				ctgt.fTruyThu_BHYT_TongCong,
				ctgt.fTruyThu_BHTN_NLD,
				ctgt.fTruyThu_BHTN_NSD,
				ctgt.fTruyThu_BHTN_TongCong,
				ctgt.fTongTruyThu_BHXH,
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
				ctgt.iID_MLNS
				from
				BH_QTT_BHXH_ChungTu ct
				join
				BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
				join
				BH_QTT_BHXH_CTCT_GiaiThich ctgt on ct.iID_QTT_BHXH_ChungTu = ctgt.iID_QTT_BHXH_ChungTu
				where
				ct.iID_QTT_BHXH_ChungTu = @VoucherID
				and ctgt.iID_MaDonVi = @MaDonVi
					) chungtudonvi 
				on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		
			order by mlns.sXauNoiMa
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_bang_loi]    Script Date: 10/25/2023 9:03:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_bang_loi]
	@Quy int,
	@NamLamViec int
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
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_tong_hop_so_sanh]    Script Date: 10/25/2023 9:03:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_tong_hop_so_sanh]
	@NamLamViec int
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
		from BH_QTT_MucLucGiaiThich
		where iNamLamViec = @NamLamViec) mlns
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
			where ctgt.iQuyNam = @NamLamViec) chungtudonvi
		on mlns.iID_MLGT = chungtudonvi.iID_MLNS
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_truy_thu]    Script Date: 10/25/2023 9:03:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_truy_thu]
	@NamLamViec int
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
			and ctgt.iQuyNam = @NamLamViec) chungtudonvi 
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
			and ctgt.iQuyNam = @NamLamViec) chungtudonvi 
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
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]    Script Date: 10/25/2023 9:03:57 AM ******/
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
			and ct.iLoaiTongHop = case when @IsTongHop = 1 then 2 else 1 end
			and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		
		order by mlns.sXauNoiMa
END
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chi_tiet]    Script Date: 10/25/2023 9:03:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_kht_bhxh_chi_tiet]
	@KhtBHXHId nvarchar(100),
	@NamLamViec int,
	@MaDonVi nvarchar(100)
AS
BEGIN
SELECT 
		ct.iID_KHT_BHXH as BhKhtBHXHId,
		ct.*
	FROM
		(
			SELECT
				ddv.iID_MaDonVi,
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.iID_KHT_BHXHChiTiet,
				bhct.iID_KHT_BHXH,
				bhct.iID_LoaiDoiTuong,
				bhct.sTenLoaiDoiTuong,
				bhct.iQSBQNam,
				bhct.fLuongChinh,
				bhct.fPhuCapChucVu,
				bhct.fPCTNNghe,
				bhct.fPCTNVuotKhung,
				bhct.fNghiOm,
				bhct.fHSBL,
				bhct.fTongQTLN,
				bhct.fThu_BHXH_NLD,
				bhct.fThu_BHXH_NSD, 
				bhct.fTongThuBHXH, 
				bhct.fThu_BHYT_NLD,
				bhct.fThu_BHYT_NSD,
				bhct.fTongThuBHYT,
				bhct.fThu_BHTN_NLD,
				bhct.fThu_BHTN_NSD,
				bhct.fTongThuBHTN,
				bhct.fTongCong,
				bhct.iID_MucLucNganSach,
				bhct.dNgayTao,
				bhct.dNgaySua,
				bhct.sNguoiTao,
				bhct.sNguoiSua
			FROM 
				BH_KHT_BHXH bh
			JOIN 
				BH_KHT_BHXH_ChiTiet bhct ON bh.iID_KHT_BHXH = bhct.iID_KHT_BHXH 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_KHT_BHXH = @KhtBHXHId
				and bh.iID_MaDonVi = @MaDonVi
		) ct;

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chungtu_chitiet_tao_tonghop]    Script Date: 10/25/2023 9:03:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_kht_bhxh_chungtu_chitiet_tao_tonghop]
	@ListIdChungTuTongHop ntext,
	@IdChungTu nvarchar(100),
	@NamLamViec int
AS
BEGIN
	INSERT INTO BH_KHT_BHXH_ChiTiet (iID_KHT_BHXH, iID_MucLucNganSach, iQSBQNam, fLuongChinh , fPhuCapChucVu, fPCTNNghe, fPCTNVuotKhung, fNghiOm , fThu_BHXH_NLD
	, fThu_BHXH_NSD , fThu_BHYT_NLD , fThu_BHYT_NSD , fThu_BHTN_NLD , fThu_BHTN_NSD,fTongThuBHXH, fTongThuBHYT, fTongThuBHTN, fTongCong
	, dNgayTao, dNgaySua, sNguoiTao)
SELECT @idChungTu,
       iID_MucLucNganSach,
	   sum(iQSBQNam),
       sum(fLuongChinh) ,
       sum(fPhuCapChucVu) ,
	   sum(fPCTNNghe) ,
	   sum(fPCTNVuotKhung) ,
	   sum(fNghiOm),
       sum(fThu_BHXH_NLD) ,
	   sum(fThu_BHXH_NSD) ,
	   sum(fThu_BHYT_NLD) ,
	   sum(fThu_BHYT_NSD) ,
	   sum(fThu_BHTN_NLD) ,
	   sum(fThu_BHTN_NSD) ,
	   sum(fTongThuBHXH) ,
	   Sum(fTongThuBHYT) ,
	   Sum(fTongThuBHTN) ,
	   Sum(fTongCong) ,
       NULL ,
       NULL ,
       NULL 
FROM BH_KHT_BHXH_ChiTiet
WHERE iID_KHT_BHXH in
    (SELECT *
     FROM f_split(@ListIdChungTuTongHop))
GROUP BY iID_MucLucNganSach

--danh dau chung tu da tong hop
update BH_KHT_BHXH set bDaTongHop = 1 
where iID_KHT_BHXH in
    (SELECT *
     FROM f_split(@ListIdChungTuTongHop));

END
GO
/****** Object:  StoredProcedure [dbo].[sp_khtm_bhyt_chi_tiet]    Script Date: 10/25/2023 9:03:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_khtm_bhyt_chi_tiet]
	@KhtmBHYTId nvarchar(100),
	@NamLamViec int,
	@MaDonVi nvarchar(100)
AS
BEGIN
	SELECT 
		ct.iID_KHTM_BHYT IdKhtmBHYT,
		ct.*
	FROM
		(
			SELECT
				ddv.iID_DonVi,
				ddv.iID_MaDonVi,
				bh.sMoTa SMoTa,
				bhct.iID_KHTM_BHYT_ChiTiet,
				bhct.iID_KHTM_BHYT,
				bhct.iID_NoiDung,
				bhct.sTenNoiDung,
				bhct.iSoNguoi,
				bhct.iSoThang,
				bhct.fDinhMuc,
				bhct.fThanhTien,
				bhct.sGhiChu,
				bhct.dNgayTao,
				bhct.dNgaySua,
				bhct.sNguoiTao,
				bhct.sNguoiSua,
				ddv.sTenDonVi
			FROM 
				BH_KHTM_BHYT bh
			JOIN 
				BH_KHTM_BHYT_ChiTiet bhct ON bh.iID_KHTM_BHYT = bhct.iID_KHTM_BHYT 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_KHTM_BHYT = @khtmBHYTId
				and bh.iID_MaDonVi = @MaDonVi
		) ct;

END
GO
DELETE FROM [dbo].[BH_QTT_MucLucGiaiThich]
GO
INSERT [dbo].[BH_QTT_MucLucGiaiThich] ([iID_MLGT], [sNoiDung], [iLoai], [iNamLamViec], [iSTT]) VALUES (N'39aa89d4-d45b-4256-8e2f-372268f873f1', N'Bảo hiểm y tế quân nhân', 3, 2023, 3)
GO
INSERT [dbo].[BH_QTT_MucLucGiaiThich] ([iID_MLGT], [sNoiDung], [iLoai], [iNamLamViec], [iSTT]) VALUES (N'e98c1f21-a388-43e0-b8a8-528a55a520e1', N'Giảm đóng vào Qũy bảo hiểm thất nghiệp', 4, 2023, 3)
GO
INSERT [dbo].[BH_QTT_MucLucGiaiThich] ([iID_MLGT], [sNoiDung], [iLoai], [iNamLamViec], [iSTT]) VALUES (N'fb7ad561-8214-4bc7-9b3f-62fc854aa920', N'Bảo hiểm thất nghiệp', 3, 2023, 2)
GO
INSERT [dbo].[BH_QTT_MucLucGiaiThich] ([iID_MLGT], [sNoiDung], [iLoai], [iNamLamViec], [iSTT]) VALUES (N'68ca2112-5110-4259-b509-73507e590359', N'Bảo hiểm y tế của CC, CNVC, LĐHĐ', 3, 2023, 4)
GO
INSERT [dbo].[BH_QTT_MucLucGiaiThich] ([iID_MLGT], [sNoiDung], [iLoai], [iNamLamViec], [iSTT]) VALUES (N'ba4dde1a-5127-4e27-9319-8b42e6e1f240', N'Tạm dừng đóng quỹ hưu trí và tử tuất', 4, 2023, 2)
GO
INSERT [dbo].[BH_QTT_MucLucGiaiThich] ([iID_MLGT], [sNoiDung], [iLoai], [iNamLamViec], [iSTT]) VALUES (N'04b5f638-4b0e-4f36-967d-a851a8e9a395', N'Giảm mức đóng quỹ tai nạn lao động, bệnh nghề nghiệp', 4, 2023, 1)
GO
INSERT [dbo].[BH_QTT_MucLucGiaiThich] ([iID_MLGT], [sNoiDung], [iLoai], [iNamLamViec], [iSTT]) VALUES (N'638f36a9-a102-42b9-a6f9-cd00dd001dd9', N'Bảo hiểm xã hội', 3, 2023, 1)
GO
