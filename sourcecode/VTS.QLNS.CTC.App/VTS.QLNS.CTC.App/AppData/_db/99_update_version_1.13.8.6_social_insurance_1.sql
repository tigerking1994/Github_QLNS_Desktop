/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]    Script Date: 1/16/2024 9:04:12 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh]    Script Date: 1/16/2024 9:04:12 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_khtm_bhyt_chung_tu_chi_tiet_donvi]    Script Date: 1/16/2024 9:04:12 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khtm_bhyt_chung_tu_chi_tiet_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khtm_bhyt_chung_tu_chi_tiet_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_dtt_bhxh_find_all_dutoan_dotnhan_phanbo]    Script Date: 1/16/2024 9:04:12 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dtt_bhxh_find_all_dutoan_dotnhan_phanbo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dtt_bhxh_find_all_dutoan_dotnhan_phanbo]
GO
/****** Object:  StoredProcedure [dbo].[sp_dieu_chinh_dtt_tao_tonghop_chungtu_chitiet]    Script Date: 1/16/2024 9:04:12 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dieu_chinh_dtt_tao_tonghop_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dieu_chinh_dtt_tao_tonghop_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_luong_can_cu_quy]    Script Date: 1/16/2024 9:04:12 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_luong_can_cu_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_luong_can_cu_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_luong_can_cu_nam]    Script Date: 1/16/2024 9:04:12 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_luong_can_cu_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_luong_can_cu_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]    Script Date: 1/16/2024 9:04:12 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]    Script Date: 1/16/2024 9:04:12 AM ******/
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
	0 as iRowType,
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
	ISNULL(nhanPB.fThuBHXHNLD, 0) fBHXHNLD,
	ISNULL(tblSoChuaPhanBo.fBHXHNLDTruocDieuChinh, 0) fBHXHNLDTruocDieuChinh,
	ISNULL(nhanPB.fThuBHXHNSD, 0) fBHXHNSD,
	ISNULL(tblSoChuaPhanBo.fBHXHNSDTruocDieuChinh, 0) fBHXHNSDTruocDieuChinh,
	ISNULL(nhanPB.fThuBHYTNLD, 0) fBHYTNLD,
	ISNULL(tblSoChuaPhanBo.fBHYTNLDTruocDieuChinh, 0) fBHYTNLDTruocDieuChinh,
	ISNULL(nhanPB.fThuBHYTNSD, 0) fBHYTNSD,
	ISNULL(tblSoChuaPhanBo.fBHYTNSDTruocDieuChinh, 0) fBHYTNSDTruocDieuChinh,
	ISNULL(nhanPB.fThuBHTNNLD, 0) fBHTNNLD,
	ISNULL(tblSoChuaPhanBo.fBHTNNLDTruocDieuChinh, 0) fBHTNNLDTruocDieuChinh,
	ISNULL(nhanPB.fThuBHTNNSD, 0) fBHTNNSD,
	ISNULL(tblSoChuaPhanBo.fBHTNNSDTruocDieuChinh, 0) fBHTNNSDTruocDieuChinh,
	case when tblSoChuaPhanBo.iRowType = 2 then 1 else tblMucLucNganSach.iRowType end iRowType,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	tblSoChuaPhanBo.sSoQuyetDinh as sSoQuyetDinh,
	case when tblSoChuaPhanBo.iRowType = 2 then 1 else tblMucLucNganSach.bHangCha end bHangCha,
	0 as IsRemainRow
	into tblMucLucNganSach_duplicateDTT
	from tblMucLucNganSachDTT tblMucLucNganSach
	left join tblSoChuaPhanBoDTT tblSoChuaPhanBo on tblMucLucNganSach.iID_MLNS = tblSoChuaPhanBo.iID_MLNS
	left join tbl_tblChiTietDuToanNhan_MucLucDTT nhanPB on tblMucLucNganSach.iID_MLNS = nhanPB.iID_MLNS
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_luong_can_cu_nam]    Script Date: 1/16/2024 9:04:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_get_luong_can_cu_nam] 
	@Year int
AS
BEGIN
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_y]') AND type in (N'U')) drop table tbl_cancu_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result_y]') AND type in (N'U')) drop table tbl_cancu_result_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu_y]') AND type in (N'U')) drop table tbl_luong_can_cu_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result_final_y]') AND type in (N'U')) drop table tbl_cancu_result_final_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu_final_y]') AND type in (N'U')) drop table tbl_luong_can_cu_final_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_so_nguoi_y]') AND type in (N'U')) drop table tbl_cancu_so_nguoi_y;
	
	select
	 pc.Ma_PhuCap,
	 pc.Ma_Cb,
	 sum(ctct.DieuChinh) fGiaTri
	into tbl_cancu_y
	from TL_PhuCap_MLNS pc
	join TL_QT_ChungTuChiTiet ctct on pc.Ma_Cb = ctct.MaCb and pc.XauNoiMa = ctct.XauNoiMa
	join TL_QT_ChungTu ct on ct.ID = ctct.Id_ChungTu
	where isnull(ctct.MaCachTl,'') = ''
	and pc.Ma_Cb in ('1', '2', '3.1', '3.2', '3.3', '43', '4')
	and pc.Ma_PhuCap in ('LHT_TT','PCCV_TT','PCTN_TT','PCTNVK_TT','HSBL_TT')
	and ct.Nam = @Year
	and pc.Nam = @Year
	and ct.bKhoa = 1
	group by
	 pc.Ma_PhuCap,
	 pc.Ma_Cb
	 --get so nguoi
	select
	 pc.Ma_Cb,
	 sum(ctct.SoNguoi) IQSBQNam
	 into tbl_cancu_so_nguoi_y
	from TL_PhuCap_MLNS pc
	join TL_QT_ChungTuChiTiet ctct on pc.Ma_Cb = ctct.MaCb and pc.XauNoiMa = ctct.XauNoiMa
	join TL_QT_ChungTu ct on ct.ID = ctct.Id_ChungTu
	where isnull(ctct.MaCachTl,'') = ''
	and pc.Ma_Cb in ('1', '2', '3.1', '3.2', '3.3', '43', '4')
	and pc.Ma_PhuCap = 'LHT_TT'
	and ct.Nam = @Year
	and pc.Nam = @Year
	and ct.bKhoa = 1
	group by
	 pc.Ma_Cb
	 ------------------------------------------
	 select distinct
	  cancu.Ma_Cb SMaCapBac,
	  songuoi.IQSBQNam/12 IQSBQNam,
	  luongchinh.fGiaTri FGiaTriLuongChinh,
	  pccv.fGiaTri FGiaTriPCCV,
	  pctn.fGiaTri FGiaTriPCTN,
	  pctnvk.fGiaTri FGiaTriPCTNVK,
	  hsbl.fGiaTri FGiaTriHSBL
	 into tbl_cancu_result_y
	 from tbl_cancu_y cancu
	 left join tbl_cancu_y luongchinh on cancu.Ma_Cb = luongchinh.Ma_Cb and luongchinh.Ma_PhuCap = 'LHT_TT'
	 left join tbl_cancu_y pccv on cancu.Ma_Cb = pccv.Ma_Cb and pccv.Ma_PhuCap = 'PCCV_TT'
	 left join tbl_cancu_y pctn on cancu.Ma_Cb = pctn.Ma_Cb and pctn.Ma_PhuCap = 'PCTN_TT'
	 left join tbl_cancu_y pctnvk on cancu.Ma_Cb = pctnvk.Ma_Cb and pctnvk.Ma_PhuCap = 'PCTNVK_TT'
	 left join tbl_cancu_y hsbl on cancu.Ma_Cb = hsbl.Ma_Cb and hsbl.Ma_PhuCap = 'HSBL_TT'
	 left join tbl_cancu_so_nguoi_y songuoi on cancu.Ma_Cb = songuoi.Ma_Cb

	 update tbl_cancu_result_y set SMaCapBac = '3' where SMaCapBac in ('3.1','3.2','3.3')

	 select distinct
	  cancu.SMaCapBac,
	  sum(cancu.IQSBQNam) IQSBQNam,
	  sum(cancu.FGiaTriLuongChinh) FGiaTriLuongChinh,
	  sum(cancu.FGiaTriPCCV) FGiaTriPCCV,
	  sum(cancu.FGiaTriPCTN) FGiaTriPCTN,
	  sum(cancu.FGiaTriPCTNVK) FGiaTriPCTNVK,
	  sum(cancu.FGiaTriHSBL) FGiaTriHSBL
	 into tbl_cancu_result_final_y
	 from tbl_cancu_result_y cancu
	 group by cancu.SMaCapBac

	 --Luong BHXH
	 select
	  sMaCB SMaCapBac,
	  sum(nGiaTri) FNghiOm
	 into tbl_luong_can_cu_y
	 from TL_BangLuong_ThangBHXH
	 where iNam = @Year
	  and sMaCheDo in ('BENHDAINGAY_D14NGAY', 'OMKHAC_D14NGAY')
	  and (sMaCB like '1%' or sMaCB like '2%' or sMaCB like '0%' or sMaCB = '43' or sMaCB in ('3.1', '3.2', '3.3'))
	 group by sMaCB

	 update tbl_luong_can_cu_y set SMaCapBac = '1' where SMaCapBac like '1%'
	 update tbl_luong_can_cu_y set SMaCapBac = '2' where SMaCapBac like '2%'
	 update tbl_luong_can_cu_y set SMaCapBac = '4' where SMaCapBac like '0%'
	 update tbl_luong_can_cu_y set SMaCapBac = '3' where SMaCapBac in ('3.1','3.2','3.3')

	 select
	  SMaCapBac,
	  sum(FNghiOm) FNghiOm
	 into tbl_luong_can_cu_final_y
	 from tbl_luong_can_cu_y
	 group by SMaCapBac

	 --result
	 select
	  luong.SMaCapBac,
	  luong.IQSBQNam,
	  luong.FGiaTriLuongChinh,
	  luong.FGiaTriPCCV,
	  luong.FGiaTriPCTN,
	  luong.FGiaTriPCTNVK,
	  luong.FGiaTriHSBL,
	  CAST(bhxh.FNghiOm AS FLOAT) FNghiOm
	 from tbl_cancu_result_final_y luong
	 left join tbl_luong_can_cu_final_y bhxh on luong.SMaCapBac = bhxh.SMaCapBac
	 -----------------------------------------------
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_y]') AND type in (N'U')) drop table tbl_cancu_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result_y]') AND type in (N'U')) drop table tbl_cancu_result_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu_y]') AND type in (N'U')) drop table tbl_luong_can_cu_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result_final_y]') AND type in (N'U')) drop table tbl_cancu_result_final_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu_final_y]') AND type in (N'U')) drop table tbl_luong_can_cu_final_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_so_nguoi_y]') AND type in (N'U')) drop table tbl_cancu_so_nguoi_y;

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_luong_can_cu_quy]    Script Date: 1/16/2024 9:04:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_get_luong_can_cu_quy] 
	@Year int,
	@Months nvarchar(20)
AS
BEGIN
	
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu]') AND type in (N'U')) drop table tbl_cancu;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result]') AND type in (N'U')) drop table tbl_cancu_result;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu]') AND type in (N'U')) drop table tbl_luong_can_cu;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result_final]') AND type in (N'U')) drop table tbl_cancu_result_final;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu_final]') AND type in (N'U')) drop table tbl_luong_can_cu_final;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_so_nguoi]') AND type in (N'U')) drop table tbl_cancu_so_nguoi;
	
	select
	 pc.Ma_PhuCap,
	 pc.Ma_Cb,
	 sum(ctct.DieuChinh) fGiaTri
	into tbl_cancu
	from TL_PhuCap_MLNS pc
	join TL_QT_ChungTuChiTiet ctct on pc.Ma_Cb = ctct.MaCb and pc.XauNoiMa = ctct.XauNoiMa
	join TL_QT_ChungTu ct on ct.ID = ctct.Id_ChungTu
	where isnull(ctct.MaCachTl,'') = ''
	and pc.Ma_Cb in ('1', '2', '3.1', '3.2', '3.3', '43', '4')
	and pc.Ma_PhuCap in ('LHT_TT','PCCV_TT','PCTN_TT','PCTNVK_TT','HSBL_TT')
	and ct.Thang in (SELECT * FROM f_split(@Months))
	and ct.Nam = @Year
	and pc.Nam = @Year
	and ct.bKhoa = 1
	group by
	 pc.Ma_PhuCap,
	 pc.Ma_Cb
	 
	--get so nguoi
	select
	 pc.Ma_Cb,
	 sum(ctct.SoNguoi) IQSBQNam
	 into tbl_cancu_so_nguoi
	from TL_PhuCap_MLNS pc
	join TL_QT_ChungTuChiTiet ctct on pc.Ma_Cb = ctct.MaCb and pc.XauNoiMa = ctct.XauNoiMa
	join TL_QT_ChungTu ct on ct.ID = ctct.Id_ChungTu
	where isnull(ctct.MaCachTl,'') = ''
	and pc.Ma_Cb in ('1', '2', '3.1', '3.2', '3.3', '43', '4')
	and pc.Ma_PhuCap = 'LHT_TT'
	and ct.Thang in (SELECT * FROM f_split(@Months))
	and ct.Nam = @Year
	and pc.Nam = @Year
	and ct.bKhoa = 1
	group by
	 pc.Ma_Cb
	 ------------------------------------------
	 select distinct
	  cancu.Ma_Cb SMaCapBac,
	  songuoi.IQSBQNam/3 IQSBQNam,
	  luongchinh.fGiaTri FGiaTriLuongChinh,
	  pccv.fGiaTri FGiaTriPCCV,
	  pctn.fGiaTri FGiaTriPCTN,
	  pctnvk.fGiaTri FGiaTriPCTNVK,
	  hsbl.fGiaTri FGiaTriHSBL
	 into tbl_cancu_result
	 from tbl_cancu cancu
	 left join tbl_cancu luongchinh on cancu.Ma_Cb = luongchinh.Ma_Cb and luongchinh.Ma_PhuCap = 'LHT_TT'
	 left join tbl_cancu pccv on cancu.Ma_Cb = pccv.Ma_Cb and pccv.Ma_PhuCap = 'PCCV_TT'
	 left join tbl_cancu pctn on cancu.Ma_Cb = pctn.Ma_Cb and pctn.Ma_PhuCap = 'PCTN_TT'
	 left join tbl_cancu pctnvk on cancu.Ma_Cb = pctnvk.Ma_Cb and pctnvk.Ma_PhuCap = 'PCTNVK_TT'
	 left join tbl_cancu hsbl on cancu.Ma_Cb = hsbl.Ma_Cb and hsbl.Ma_PhuCap = 'HSBL_TT'
	 left join tbl_cancu_so_nguoi songuoi on cancu.Ma_Cb = songuoi.Ma_Cb

	 update tbl_cancu_result set SMaCapBac = '3' where SMaCapBac in ('3.1','3.2','3.3')

	 select distinct
	  cancu.SMaCapBac,
	  sum(cancu.IQSBQNam) IQSBQNam,
	  sum(cancu.FGiaTriLuongChinh) FGiaTriLuongChinh,
	  sum(cancu.FGiaTriPCCV) FGiaTriPCCV,
	  sum(cancu.FGiaTriPCTN) FGiaTriPCTN,
	  sum(cancu.FGiaTriPCTNVK) FGiaTriPCTNVK,
	  sum(cancu.FGiaTriHSBL) FGiaTriHSBL
	 into tbl_cancu_result_final
	 from tbl_cancu_result cancu
	 group by cancu.SMaCapBac

	 --Luong BHXH
	 select
	  sMaCB SMaCapBac,
	  sum(nGiaTri) FNghiOm
	 into tbl_luong_can_cu
	 from TL_BangLuong_ThangBHXH
	 where iNam = @Year
	  and iThang in (SELECT * FROM f_split(@Months))
	  and sMaCheDo in ('BENHDAINGAY_D14NGAY', 'OMKHAC_D14NGAY')
	  and (sMaCB like '1%' or sMaCB like '2%' or sMaCB like '0%' or sMaCB = '43' or sMaCB in ('3.1', '3.2', '3.3'))
	 group by sMaCB

	update tbl_luong_can_cu set SMaCapBac = '1' where SMaCapBac like '1%'
	update tbl_luong_can_cu set SMaCapBac = '2' where SMaCapBac like '2%'
	update tbl_luong_can_cu set SMaCapBac = '4' where SMaCapBac like '0%'
	update tbl_luong_can_cu set SMaCapBac = '3' where SMaCapBac in ('3.1','3.2','3.3')

	select
	  SMaCapBac,
	  sum(FNghiOm) FNghiOm
	 into tbl_luong_can_cu_final
	 from tbl_luong_can_cu
	 group by SMaCapBac

	 --result
	 select
	  luong.SMaCapBac,
	  luong.IQSBQNam,
	  luong.FGiaTriLuongChinh,
	  luong.FGiaTriPCCV,
	  luong.FGiaTriPCTN,
	  luong.FGiaTriPCTNVK,
	  luong.FGiaTriHSBL,
	  CAST(bhxh.FNghiOm AS FLOAT) FNghiOm
	 from tbl_cancu_result_final luong
	 left join tbl_luong_can_cu_final bhxh on luong.SMaCapBac = bhxh.SMaCapBac
	 -----------------------------------------------
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu]') AND type in (N'U')) drop table tbl_cancu;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result]') AND type in (N'U')) drop table tbl_cancu_result;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu]') AND type in (N'U')) drop table tbl_luong_can_cu;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result_final]') AND type in (N'U')) drop table tbl_cancu_result_final;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu_final]') AND type in (N'U')) drop table tbl_luong_can_cu_final;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_so_nguoi]') AND type in (N'U')) drop table tbl_cancu_so_nguoi;

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dieu_chinh_dtt_tao_tonghop_chungtu_chitiet]    Script Date: 1/16/2024 9:04:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dieu_chinh_dtt_tao_tonghop_chungtu_chitiet]
	@ListIdChungTuTongHop ntext, 
	@Nguoitao nvarchar(50), 
	@IdChungTu nvarchar(100), 
	@NamLamViec int 
AS 
BEGIN 
	INSERT INTO [dbo].BH_DTT_BHXH_DieuChinh_ChiTiet (
    iID_DTT_BHXH_DieuChinh_ChiTiet, iID_DTT_BHXH_DieuChinh, iID_MucLucNganSach, sLNS, sNoiDung, sXauNoiMa,
	fThuBHXH_NLD, fThuBHXH_NSD, fThuBHYT_NLD, fThuBHYT_NSD, fThuBHTN_NLD, fThuBHTN_NSD,
	fThuBHXH_NLD_QTDauNam, fThuBHXH_NSD_QTDauNam, fThuBHYT_NLD_QTDauNam, fThuBHYT_NSD_QTDauNam, fThuBHTN_NLD_QTDauNam, fThuBHTN_NSD_QTDauNam,
	fThuBHXH_NLD_QTCuoiNam, fThuBHXH_NSD_QTCuoiNam, fThuBHYT_NLD_QTCuoiNam, fThuBHYT_NSD_QTCuoiNam, fThuBHTN_NLD_QTCuoiNam, fThuBHTN_NSD_QTCuoiNam,
	fTongThuBHXH_NLD, fTongThuBHXH_NSD, fTongThuBHYT_NLD, fTongThuBHYT_NSD, fTongThuBHTN_NLD, fTongThuBHTN_NSD,
	fThuBHXH_NLD_Tang, fThuBHXH_NSD_Tang, fThuBHXH_Tang, fThuBHYT_NLD_Tang, fThuBHYT_NSD_Tang, fThuBHYT_Tang, fThuBHTN_NLD_Tang, fThuBHTN_NSD_Tang, fThuBHTN_Tang,
	fThuBHXH_NLD_Giam, fThuBHXH_NSD_Giam, fThuBHXH_Giam, fThuBHYT_NLD_Giam, fThuBHYT_NSD_Giam, fThuBHYT_Giam, fThuBHTN_NLD_Giam, fThuBHTN_NSD_Giam, fThuBHTN_Giam,
    dNgaySua, dNgayTao, sNguoiSua, sNguoiTao)

	SELECT 
	DISTINCT NEWID(), @idChungTu, iID_MucLucNganSach, sLNS, sNoiDung, sXauNoiMa,
	sum(fThuBHXH_NLD), sum(fThuBHXH_NSD), sum(fThuBHYT_NLD), sum(fThuBHYT_NSD), sum(fThuBHTN_NLD), sum(fThuBHTN_NSD),
	sum(fThuBHXH_NLD_QTDauNam), sum(fThuBHXH_NSD_QTDauNam), sum(fThuBHYT_NLD_QTDauNam), sum(fThuBHYT_NSD_QTDauNam), sum(fThuBHTN_NLD_QTDauNam), sum(fThuBHTN_NSD_QTDauNam),
	sum(fThuBHXH_NLD_QTCuoiNam), sum(fThuBHXH_NSD_QTCuoiNam), sum(fThuBHYT_NLD_QTCuoiNam), sum(fThuBHYT_NSD_QTCuoiNam), sum(fThuBHTN_NLD_QTCuoiNam), sum(fThuBHTN_NSD_QTCuoiNam),
	sum(fTongThuBHXH_NLD), sum(fTongThuBHXH_NSD), sum(fTongThuBHYT_NLD), sum(fTongThuBHYT_NSD), sum(fTongThuBHTN_NLD), sum(fTongThuBHTN_NSD),
	sum(fThuBHXH_NLD_Tang), sum(fThuBHXH_NSD_Tang), sum(fThuBHXH_Tang), sum(fThuBHYT_NLD_Tang), sum(fThuBHYT_NSD_Tang), sum(fThuBHYT_Tang), sum(fThuBHTN_NLD_Tang), sum(fThuBHTN_NSD_Tang), sum(fThuBHTN_Tang),
	sum(fThuBHXH_NLD_Giam), sum(fThuBHXH_NSD_Giam), sum(fThuBHXH_Giam), sum(fThuBHYT_NLD_Giam), sum(fThuBHYT_NSD_Giam), sum(fThuBHYT_Giam), sum(fThuBHTN_NLD_Giam), sum(fThuBHTN_NSD_Giam), sum(fThuBHTN_Giam),
	Null, GETDATE(), Null, @Nguoitao 
	FROM 
	  BH_DTT_BHXH_DieuChinh_ChiTiet 
	WHERE 
	  iID_DTT_BHXH_DieuChinh in (SELECT * FROM f_split(@ListIdChungTuTongHop)) 
	GROUP BY 
	  sLNS,
	  iID_MucLucNganSach, 
	  sNoiDung,
	  sXauNoiMa

	  --danh dau chung tu da tong hop
		update 
		  BH_DTT_BHXH_DieuChinh 
		set 
		  bDaTongHop = 1
		where 
		  iID_DTT_BHXH_DieuChinh in (SELECT * FROM f_split(@ListIdChungTuTongHop))
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dtt_bhxh_find_all_dutoan_dotnhan_phanbo]    Script Date: 1/16/2024 9:04:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dtt_bhxh_find_all_dutoan_dotnhan_phanbo]
	@YearOfWork int,
	@Date DateTime,
	@LoaiDuToan int
AS
BEGIN
	DECLARE @DieuChinh int = 2;
	--Lấy danh sách dự toán nhận phân bổ
		SELECT iID_DTT_BHXH,
			   sSoChungTu,
			   sDSLNS,
			   iID_MaDonVi,
			   dNgayChungTu,
			   sSoQuyetDinh,
			   dNgayQuyetDinh,
			   fDuToan AS fSoPhanBo
		INTO  tblNhanPhanBo
		FROM BH_DTT_BHXH_ChungTu 
		WHERE iNamLamViec = @YearOfWork 
			AND iLoaiDuToan = @LoaiDuToan
			AND (
				(dNgayQuyetDinh IS NOT NULL AND CAST(dNgayQuyetDinh AS DATE) <= CAST(@Date AS DATE)) 
				OR 
				(dNgayQuyetDinh IS NULL AND dNgayChungTu IS NOT NULL AND CAST(dNgayChungTu AS DATE) <= CAST(@Date AS DATE)))

	--Lấy danh sách dự toán đã được phân bô
		SELECT pbmap.iID_CTDuToan_Nhan, fTongCong fDaPhanBo
		INTO tblChungTuNhanPhanBoMap
		FROM BH_DTT_Nhan_Phan_Bo_Map AS pbmap
		LEFT JOIN 
			(	SELECT sum(pbdtct.fTongCong) fTongCong, pbdt.iID_DTT_BHXH_PhanBo_ChungTu
				FROM BH_DTT_BHXH_PhanBo_ChungTu AS pbdt
				LEFT JOIN  BH_DTT_BHXH_PhanBo_ChungTuChiTiet AS pbdtct ON pbdt.iID_DTT_BHXH_PhanBo_ChungTu = pbdtct.iID_DTT_BHXH_ChungTu
				GROUP BY pbdt.iID_DTT_BHXH_PhanBo_ChungTu) AS tblDaPhanBo 
				ON pbmap.iID_CTDuToan_PhanBo = tblDaPhanBo.iID_DTT_BHXH_PhanBo_ChungTu

	---Lay danh sach du toan nhan phan bo, so phan bo, chua phan bo
		SELECT distinct npb.iID_DTT_BHXH as Id,
			    npb.sSoChungTu, 
				npb.sDSLNS,
				npb.iID_MaDonVi,
				npb.dNgayChungTu, 
				npb.sSoQuyetDinh, 
				npb.dNgayQuyetDinh, 
				npb.fSoPhanBo, 
				npbm.fDaPhanBo,
				(npb.fSoPhanBo - npbm.fDaPhanBo) AS fSoChuaPhanBo
		FROM tblNhanPhanBo AS npb
		left join tblChungTuNhanPhanBoMap AS npbm ON npb.iID_DTT_BHXH = npbm.iID_CTDuToan_Nhan

	   IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblNhanPhanBo]') AND type in (N'U')) drop table tblNhanPhanBo;
	   IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblChungTuNhanPhanBoMap]') AND type in (N'U')) drop table tblChungTuNhanPhanBoMap;

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_khtm_bhyt_chung_tu_chi_tiet_donvi]    Script Date: 1/16/2024 9:04:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_khtm_bhyt_chung_tu_chi_tiet_donvi]
	@YearOfWork int,
	@LoaiTongHop int,
	@UserName nvarchar(100)
AS
BEGIN
	DECLARE @CountDonViCha int;
	SELECT 
		@CountDonViCha = count(*) FROM (SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName AND iNamLamViec = @YearOfWork AND iTrangThai = 1) nddv
	INNER JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1 AND iLoai = 0) dv
	ON dv.iID_MaDonVi = nddv.iID_MaDonVi;
	
	SELECT
	KHTM.iID_KHTM_BHYT,
	KHTM.sSoChungTu,
	KHTM.iNamLamViec,
	KHTM.dNgayChungTu,
	KHTM.iID_MaDonVi AS IIDMaDonVi,
	DV.sTenDonVi AS sTenDonVi,
	KHTM.sMoTa,
	KHTM.bKhoa,
	KHTM.fTongKeHoach,
	KHTM.sSoQuyetDinh,
	KHTM.dNgayQuyetDinh,
	KHTM.sNguoiTao,
	KHTM.sNguoiSua,
	KHTM.dNgayTao,
	KHTM.dNgaySua,
	KHTM.iLoaiTongHop,
	KHTM.sTongHop,
	KHTM.iTongSoNguoi,
	KHTM.iTongSoThang,
	KHTM.fTongDinhMuc,
	KHTM.fTongThanhTien,
	KHTM.bDaTongHop
	into #tblChungTuCT
	FROM BH_KHTM_BHYT KHTM
	LEFT JOIN DonVi DV
	ON KHTM.iID_MaDonVi = DV.iID_MaDonVi
	WHERE DV.iNamLamViec = @YearOfWork
	AND KHTM.iNamLamViec = @YearOfWork
	AND KHTM.iLoaiTongHop = @LoaiTongHop
	ORDER BY KHTM.dNgayQuyetDinh DESC

	IF @CountDonViCha = 0
		SELECT 
			ct.*
		FROM #tblChungTuCT ct
		INNER JOIN 
			(SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork and iTrangThai = 1) dv
		ON ct.IIDMaDonVi = dv.iID_MaDonVi
		ORDER BY sSoChungTu desc;
	ELSE
		SELECT 
			ct.*
		FROM #tblChungTuCT ct
		LEFT JOIN 
			(SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork and iTrangThai = 1) dv
		ON ct.IIDMaDonVi = dv.iID_MaDonVi
		WHERE ((dv.iID_MaDonVi IS NULL AND ct.bKhoa = 1) OR (dv.iID_MaDonVi IS NOT NULL))
		ORDER BY sSoChungTu desc;

	drop table #tblChungTuCT;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh]    Script Date: 1/16/2024 9:04:12 AM ******/
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
	(sum(fThuBHYT_NLD) + sum(fThuBHYT_NSD)) DttDauNam,
	(sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NSD_QTDauNam)) Dtt6ThangDauNam,
	(sum(fThuBHYT_NLD_QTCuoiNam) + sum(fThuBHYT_NSD_QTCuoiNam)) Dtt6ThangCuoiNam,
	(sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam) + sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NSD_QTCuoiNam)) TongCong,
	((sum(fThuBHYT_NLD_QTDauNam) + (sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam) + sum(fThuBHYT_NSD_QTCuoiNam)) - sum(fThuBHYT_NLD) - sum(fThuBHYT_NSD))) Tang,
	((sum(fThuBHYT_NLD) + sum(fThuBHYT_NSD)) - (sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam) + sum(fThuBHYT_NSD_QTCuoiNam))) Giam,
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
	(sum(fThuBHYT_NLD) + sum(fThuBHYT_NSD)) DttDauNam,
	(sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NSD_QTDauNam)) Dtt6ThangDauNam,
	(sum(fThuBHYT_NLD_QTCuoiNam) + sum(fThuBHYT_NSD_QTCuoiNam)) Dtt6ThangCuoiNam,
	(sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam) + sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NSD_QTCuoiNam)) TongCong,
	((sum(fThuBHYT_NLD_QTDauNam) + (sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam) + sum(fThuBHYT_NSD_QTCuoiNam)) - sum(fThuBHYT_NLD) - sum(fThuBHYT_NSD))) Tang,
	((sum(fThuBHYT_NLD) + sum(fThuBHYT_NSD)) - (sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam) + sum(fThuBHYT_NSD_QTCuoiNam))) Giam,
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
	and MaSo in (18,19)
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
	and MaSo in (23,24)
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

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (16)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (16)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (16)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (21)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (21)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (21)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

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
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]    Script Date: 1/16/2024 9:04:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]
	@MaDonVi NVARCHAR(255),
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
	ctct.iID_MaDonVi = @MaDonVi
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
	ctct.iID_MaDonVi = @MaDonVi
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
	ctct.iID_MaDonVi = @MaDonVi
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
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.sLNS = '9020002' -- Khối hạch toán
	-----------------------------------------
	--BHYT
	-- Khối dự toán
	union
	select
	16 STT,
	N'16' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(sum(fThuBHYT_NLD) + sum(fThuBHYT_NSD)) DttDauNam,
	(sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NSD_QTDauNam)) Dtt6ThangDauNam,
	(sum(fThuBHYT_NLD_QTCuoiNam) + sum(fThuBHYT_NSD_QTCuoiNam)) Dtt6ThangCuoiNam,
	(sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam) + sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NSD_QTCuoiNam)) TongCong,
	((sum(fThuBHYT_NLD_QTDauNam) + (sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam) + sum(fThuBHYT_NSD_QTCuoiNam)) - sum(fThuBHYT_NLD) - sum(fThuBHYT_NSD))) Tang,
	((sum(fThuBHYT_NLD) + sum(fThuBHYT_NSD)) - (sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam) + sum(fThuBHYT_NSD_QTCuoiNam))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
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
	ctct.iID_MaDonVi = @MaDonVi
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
	ctct.iID_MaDonVi = @MaDonVi
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
	(sum(fThuBHYT_NLD) + sum(fThuBHYT_NSD)) DttDauNam,
	(sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NSD_QTDauNam)) Dtt6ThangDauNam,
	(sum(fThuBHYT_NLD_QTCuoiNam) + sum(fThuBHYT_NSD_QTCuoiNam)) Dtt6ThangCuoiNam,
	(sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam) + sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NSD_QTCuoiNam)) TongCong,
	((sum(fThuBHYT_NLD_QTDauNam) + (sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam) + sum(fThuBHYT_NSD_QTCuoiNam)) - sum(fThuBHYT_NLD) - sum(fThuBHYT_NSD))) Tang,
	((sum(fThuBHYT_NLD) + sum(fThuBHYT_NSD)) - (sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam) + sum(fThuBHYT_NSD_QTCuoiNam))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
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
	ctct.iID_MaDonVi = @MaDonVi
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
	ctct.iID_MaDonVi = @MaDonVi
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
	ctct.iID_MaDonVi = @MaDonVi
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
	ctct.iID_MaDonVi = @MaDonVi
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
	ctct.iID_MaDonVi = @MaDonVi
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
	ctct.iID_MaDonVi = @MaDonVi
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
	and MaSo in (18,19)
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
	and MaSo in (23,24)
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

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (16)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (16)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (16)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (21)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (21)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (21)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

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
;
;
GO
