/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2]    Script Date: 1/24/2024 5:13:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]    Script Date: 1/24/2024 5:13:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_2]    Script Date: 1/24/2024 5:13:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang]    Script Date: 1/24/2024 5:13:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_export_cap_phat_bo_sung]    Script Date: 1/24/2024 5:13:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_export_cap_phat_bo_sung]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_export_cap_phat_bo_sung]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dttm_danhsach_pbdttm_chitiet]    Script Date: 1/24/2024 5:13:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dttm_danhsach_pbdttm_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dttm_danhsach_pbdttm_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]    Script Date: 1/24/2024 5:13:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]    Script Date: 1/24/2024 5:13:38 PM ******/
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

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblMucLucNganSachDTT]') AND type in (N'U')) drop table tblMucLucNganSachDTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblDonViDTT]') AND type in (N'U')) drop table tblDonViDTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblChungTuNhanPhanBoDTT]') AND type in (N'U')) drop table tblChungTuNhanPhanBoDTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblChiTietDuToanNhanDTT]') AND type in (N'U')) drop table tblChiTietDuToanNhanDTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_tblChiTietDuToanNhan_MucLucDTT]') AND type in (N'U')) drop table tbl_tblChiTietDuToanNhan_MucLucDTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDTT]') AND type in (N'U')) drop table tempDTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1DTT]') AND type in (N'U')) drop table temp1DTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblSoChuaPhanBoDTT]') AND type in (N'U')) drop table tblSoChuaPhanBoDTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblMucLucNganSach_duplicateDTT]') AND type in (N'U')) drop table tblMucLucNganSach_duplicateDTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpNhanDuToanThu]') AND type in (N'U')) drop table tmpNhanDuToanThu;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpDaPhanBoThu]') AND type in (N'U')) drop table tmpDaPhanBoThu;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblResult]') AND type in (N'U')) drop table tblResult;
	
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
						select 
							sum(ISNULL(fBHXH_NLD,0)) as fBHXHNLD,
							sum(ISNULL(fBHXH_NSD,0)) as fBHXHNSD,
							sum(ISNULL(fBHYT_NLD,0)) as fBHYTNLD,
							sum(ISNULL(fBHYT_NSD,0)) as fBHYTNSD,
							sum(ISNULL(fBHTN_NLD,0)) as fBHTNNLD,
							sum(ISNULL(fBHTN_NSD,0)) as fBHTNNSD,
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
	select distinct
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

	-----------
	-- Dữ liệu nhận phân bổ
	declare @IdDotNhan nvarchar(500) = (select iID_DotNhan from BH_DTT_BHXH_PhanBo_ChungTu where iID_DTT_BHXH_PhanBo_ChungTu = @ChungTuId);

	select ct.iID_MLNS, ct.iID_DTT_BHXH, ct.fThu_BHXH_NLD, ct.fThu_BHXH_NSD, ct.fThu_BHYT_NLD, ct.fThu_BHYT_NSD, ct.fThu_BHTN_NLD, ct.fThu_BHTN_NSD INTO tmpNhanDuToanThu from BH_DTT_BHXH_ChungTu_ChiTiet ct
	JOIN BH_DTT_BHXH_ChungTu dt on dt.iID_DTT_BHXH = ct.iID_DTT_BHXH
	where dt.iID_DTT_BHXH IN (select * from splitstring( @IdDotNhan))
	-----------
	-- Dữ liệu đã phân bổ
	declare @dNgayQuyetDinh Datetime = (select dNgayQuyetDinh from BH_DTT_BHXH_PhanBo_ChungTu where iID_DTT_BHXH_PhanBo_ChungTu = @ChungTuId);

	select iID_MLNS, iID_CTDuToan_Nhan, 
		0 - SUM(ISNULL(fBHXH_NLD, 0)) fBHXH_NLD,
		0 - SUM(ISNULL(fBHXH_NSD, 0)) fBHXH_NSD,
		0 - SUM(ISNULL(fBHYT_NLD, 0)) fBHYT_NLD,
		0 - SUM(ISNULL(fBHYT_NSD, 0)) fBHYT_NSD,
		0 - SUM(ISNULL(fBHTN_NLD, 0)) fBHTN_NLD,
		0 - SUM(ISNULL(fBHTN_NSD, 0)) fBHTN_NSD
	INTO tmpDaPhanBoThu from BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	JOIN BH_DTT_BHXH_PhanBo_ChungTu ct ON ctct.iID_DTT_BHXH_ChungTu = ct.iID_DTT_BHXH_PhanBo_ChungTu
	where 
	ct.iNamLamViec = @NamLamViec
	AND ct.dNgayQuyetDinh < @dNgayQuyetDinh
	group by iID_MLNS, iID_CTDuToan_Nhan;
	-----------
	select * into tblResult from
	(
		SELECT * from tblMucLucNganSach_duplicateDTT
		UNION ALL
		SELECT * from tblSoChuaPhanBoDTT
		UNION ALL
		SELECT * from temp1DTT
	) as result
	--order by result.sXauNoiMa, result.sSoQuyetDinh, result.iID_MaDonVi, result.iRowType, result.IsRemainRow
	--Result

	SELECT
	rs.iID_CTDuToan_Nhan,
	rs.iID_DTT_BHXH_ChungTu_ChiTiet,
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
	rs.sMoTa,
	rs.iRowType,
	rs.iID_MaDonVi,
	rs.sTenDonVi,
	rs.sSoQuyetDinh as sSoQuyetDinh,
	rs.IsRemainRow,
	rs.bHangCha,
	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fThu_BHXH_NLD, 0) + (ISNULL(dpb.fBHXH_NLD, 0))
		WHEN rs.IsRemainRow = 0 AND rs.iRowType = 1 THEN ISNULL(dt.fThu_BHXH_NLD,0) + (ISNULL(dpb.fBHXH_NLD, 0))
		ELSE rs.fBHXHNLD
	END as fBHXHNLD,
	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fThu_BHXH_NSD, 0) + (ISNULL(dpb.fBHXH_NSD, 0))
		WHEN rs.IsRemainRow = 0 AND rs.iRowType = 1 THEN ISNULL(dt.fThu_BHXH_NSD,0) + (ISNULL(dpb.fBHXH_NSD, 0))
		ELSE rs.fBHXHNSD
	END as fBHXHNSD,
	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fThu_BHYT_NLD, 0) + (ISNULL(dpb.fBHYT_NLD, 0))
		WHEN rs.IsRemainRow = 0 AND rs.iRowType = 1 THEN ISNULL(dt.fThu_BHYT_NLD,0) + (ISNULL(dpb.fBHYT_NLD, 0))
		ELSE rs.fBHYTNLD
	END as fBHYTNLD,
	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fThu_BHYT_NSD, 0) + (ISNULL(dpb.fBHYT_NSD, 0))
		WHEN rs.IsRemainRow = 0 AND rs.iRowType = 1 THEN ISNULL(dt.fThu_BHYT_NSD,0) + (ISNULL(dpb.fBHYT_NSD, 0))
		ELSE rs.fBHYTNSD
	END as fBHYTNSD,
	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fThu_BHTN_NLD, 0) + (ISNULL(dpb.fBHTN_NLD, 0))
		WHEN rs.IsRemainRow = 0 AND rs.iRowType = 1 THEN ISNULL(dt.fThu_BHTN_NLD,0) + (ISNULL(dpb.fBHTN_NLD, 0))
		ELSE rs.fBHTNNLD
	END as fBHTNNLD,
	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fThu_BHTN_NSD, 0) + (ISNULL(dpb.fBHTN_NSD, 0))
		WHEN rs.IsRemainRow = 0 AND rs.iRowType = 1 THEN ISNULL(dt.fThu_BHTN_NSD,0) + (ISNULL(dpb.fBHTN_NSD, 0))
		ELSE rs.fBHTNNSD
	END as fBHTNNSD,
	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fThu_BHXH_NLD, 0) + (ISNULL(dpb.fBHXH_NLD, 0))
		WHEN rs.IsRemainRow = 0 AND rs.iRowType = 1 THEN ISNULL(dt.fThu_BHXH_NLD,0) + (ISNULL(dpb.fBHXH_NLD, 0))
		ELSE rs.fBHXHNLD
	END as fBHXHNLDTruocDieuChinh,
	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fThu_BHXH_NSD, 0) + (ISNULL(dpb.fBHXH_NSD, 0))
		WHEN rs.IsRemainRow = 0 AND rs.iRowType = 1 THEN ISNULL(dt.fThu_BHXH_NSD,0) + (ISNULL(dpb.fBHXH_NSD, 0))
		ELSE rs.fBHXHNSD
	END as fBHXHNSDTruocDieuChinh,
	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fThu_BHYT_NLD, 0) + (ISNULL(dpb.fBHYT_NLD, 0))
		WHEN rs.IsRemainRow = 0 AND rs.iRowType = 1 THEN ISNULL(dt.fThu_BHYT_NLD,0) + (ISNULL(dpb.fBHYT_NLD, 0))
		ELSE rs.fBHYTNLD
	END as fBHYTNLDTruocDieuChinh,
	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fThu_BHYT_NSD, 0) + (ISNULL(dpb.fBHYT_NSD, 0))
		WHEN rs.IsRemainRow = 0 AND rs.iRowType = 1 THEN ISNULL(dt.fThu_BHYT_NSD,0) + (ISNULL(dpb.fBHYT_NSD, 0))
		ELSE rs.fBHYTNSD
	END as fBHYTNSDTruocDieuChinh,
	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fThu_BHTN_NLD, 0) + (ISNULL(dpb.fBHTN_NLD, 0))
		WHEN rs.IsRemainRow = 0 AND rs.iRowType = 1 THEN ISNULL(dt.fThu_BHTN_NLD,0) + (ISNULL(dpb.fBHTN_NLD, 0))
		ELSE rs.fBHTNNLD
	END as fBHTNNLDTruocDieuChinh,
	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fThu_BHTN_NSD, 0) + (ISNULL(dpb.fBHTN_NSD, 0))
		WHEN rs.IsRemainRow = 0 AND rs.iRowType = 1 THEN ISNULL(dt.fThu_BHTN_NSD,0) + (ISNULL(dpb.fBHTN_NSD, 0))
		ELSE rs.fBHTNNSD
	END as fBHTNNSDTruocDieuChinh
	FROM tblResult rs
	LEFT JOIN tmpNhanDuToanThu dt ON rs.iID_MLNS = dt.iID_MLNS AND rs.iID_CTDuToan_Nhan = dt.iID_DTT_BHXH
	LEFT JOIN tmpDaPhanBoThu dpb ON rs.iID_MLNS = dpb.iID_MLNS AND rs.iID_CTDuToan_Nhan = dpb.iID_CTDuToan_Nhan
	order by rs.sXauNoiMa, rs.sSoQuyetDinh, rs.iID_MaDonVi, rs.iRowType, rs.IsRemainRow

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dttm_danhsach_pbdttm_chitiet]    Script Date: 1/24/2024 5:13:38 PM ******/
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
	into tblMucLucNganSachPBDTTM
	from BH_DM_MucLucNganSach 
	where sLNS  IN (SELECT * FROM f_split(@LNS)) and iNamLamViec = @NamLamViec  and sLNS LIKE '903%'

	---Lấy danh sách đơn vị được phân bổ
	select * 
	into tblDonViPBDTTM
	from DonVi where iNamLamViec = 2023 and iID_MaDonVi  IN (SELECT * FROM f_split(@IdDonVi)) ;

	--lấy danh sách dự toán nhận phân bổ
	select *
	into tblChungTuNhanPhanBoPBDTTM
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
	into tblChiTietDuToanNhanPBDTTM
	from BH_DTTM_BHYT_ThanNhan_ChiTiet as nhanpb_chitiet
	inner join BH_DTTM_BHYT_ThanNhan as nhanpb on nhanpb.iID_DTTM_BHYT_ThanNhan = nhanpb_chitiet.iID_DTTM_BHYT_ThanNhan
	where nhanpb.iID_DTTM_BHYT_ThanNhan in (select iID_DTTM_BHYT_NhanPhanBo from tblChungTuNhanPhanBoPBDTTM)

	
	

	---Lấy  danh sách chi tiết nhận phân bổ có thông tin mục lục ngân sách
	select 
		tblChiTietDuToanNhanPBDTTM.iID_DTTM_BHYT_ThanNhan,
		tblChiTietDuToanNhanPBDTTM.iID_MLNS as iID_MLNS,
		tblMucLucNganSachPBDTTM.iID_MLNS_Cha,
		tblMucLucNganSachPBDTTM.sLNS,
		tblMucLucNganSachPBDTTM.sL,
		tblMucLucNganSachPBDTTM.sK, 
		tblMucLucNganSachPBDTTM.sM,
		tblMucLucNganSachPBDTTM.sTM,
		tblMucLucNganSachPBDTTM.sTTM,
		tblMucLucNganSachPBDTTM.sNG,
		tblMucLucNganSachPBDTTM.sTNG,
		tblMucLucNganSachPBDTTM.sXauNoiMa,
		tblMucLucNganSachPBDTTM.sNoiDung,
		tblChiTietDuToanNhanPBDTTM.sSoQuyetDinh,
		tblChiTietDuToanNhanPBDTTM.fDuToan,
		3 as Type
	into tbl_MucLucPBDTTM
	from tblMucLucNganSachPBDTTM
	inner join tblChiTietDuToanNhanPBDTTM on tblMucLucNganSachPBDTTM.iID_MLNS = tblChiTietDuToanNhanPBDTTM.iID_MLNS

	


	---Hiển thị danh sách chi tiết nhận phân bổ theo đơn vị được chọn phân bổ
	select  tbl_MucLucPBDTTM.*,tblDonViPBDTTM.iID_MaDonVi, concat(tblDonViPBDTTM.iID_MaDonVi, '-', tblDonViPBDTTM.sTenDonVi) as sTenDonVi, 0 as bHangCha
	into tempPBDTTM
	from tbl_MucLucPBDTTM cross join tblDonViPBDTTM 

	

	---Map với bảng BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet để lấy thông tin fDuToan đã được phân bổ
	select 
		tempPBDTTM.iID_DTTM_BHYT_ThanNhan, 
		chitiet_phanbo.iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet as iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet,
		tempPBDTTM.iID_MLNS,
		tempPBDTTM.iID_MLNS_Cha,
		tempPBDTTM.sLNS,
		tempPBDTTM.sL,
		tempPBDTTM.sK,
		tempPBDTTM.sM,
		tempPBDTTM.sTM,
		tempPBDTTM.sTTM,
		tempPBDTTM.sNG,
		tempPBDTTM.sTNG,
		tempPBDTTM.sXauNoiMa,
		tempPBDTTM.sNoiDung as sNoiDung,
		chitiet_phanbo.fDuToan as fDuToan,
		tempPBDTTM.fDuToan as fDuToanTruocDieuChinh,
		3 as Type,
		tempPBDTTM.iID_MaDonVi,
		tempPBDTTM.sTenDonVi,
		tempPBDTTM.sSoQuyetDinh,
		0 as bHangCha,
		0 as IsRemainRow
	into temp1PBDTTM
	from tempPBDTTM
	left join 
		(
			select * 
			from BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet 
			where iID_DTTM_BHYT_ThanNhan_PhanBo = @ChungTuId
		) as chitiet_phanbo
		on chitiet_phanbo.iID_DTTM_BHYT_ThanNhan = tempPBDTTM.iID_DTTM_BHYT_ThanNhan and chitiet_phanbo.iID_MaDonVi = tempPBDTTM.iID_MaDonVi and chitiet_phanbo.iID_MLNS = tempPBDTTM.iID_MLNS
		
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
	N'Số chưa phân bổ' as sNoiDung,
	chitiet_chuaphanbo.fDuToan as fDuToan,
	chitiet_chuaphanbo.fDuToan as fDuToanTruocDieuChinh,
	2 as Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	npb.sSoQuyetDinh as sSoQuyetDinh,
	1 as bHangCha,
	1 as IsRemainRow
	into tblSoChuaPhanBoPBDTTM
	from tblMucLucNganSachPBDTTM as muluc_ngansach
	inner join 
	(
		select (ISNULL(ct_npb.fDuToan,0) - ISNULL(ct_pb_t.fDuToan,0)) as fDuToan, ct_npb.iID_MLNS, ct_npb.iID_DTTM_BHYT_ThanNhan 
		from BH_DTTM_BHYT_ThanNhan_ChiTiet as ct_npb
		left join
			(
				select sum(fDuToan) as fDuToan , iID_MLNS, iID_DTTM_BHYT_ThanNhan
				from BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet as ct_pb
				where iID_DTTM_BHYT_ThanNhan in (select iID_DTTM_BHYT_NhanPhanBo from tblChungTuNhanPhanBoPBDTTM)
				group by  iID_MLNS, iID_DTTM_BHYT_ThanNhan
			)as ct_pb_t  

		on ct_pb_t.iID_MLNS = ct_npb.iID_MLNS and  ct_npb.iID_DTTM_BHYT_ThanNhan = ct_pb_t.iID_DTTM_BHYT_ThanNhan) as chitiet_chuaphanbo
		on chitiet_chuaphanbo.iID_MLNS = muluc_ngansach.iID_MLNS 
	inner join BH_DTTM_BHYT_ThanNhan as npb on npb.iID_DTTM_BHYT_ThanNhan = chitiet_chuaphanbo.iID_DTTM_BHYT_ThanNhan
	where npb.iID_DTTM_BHYT_ThanNhan in ( select iID_DTTM_BHYT_NhanPhanBo from tblChungTuNhanPhanBoPBDTTM)

	---- Lấy mục lục ngân sách có dupliate các mục lục ngân sách con
	select distinct
	tblSoChuaPhanBoPBDTTM.iID_DTTM_BHYT_ThanNhan as IID_DTTM_BHYT_ThanNhan,
	CAST(NULL AS VARCHAR(50)) as iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet,
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
	tblMucLucNganSach.sNoiDung as sNoiDung,
	tblSoChuaPhanBoPBDTTM.fDuToan as fDuToan,
	tblSoChuaPhanBoPBDTTM.fDuToanTruocDieuChinh as fDuToanTruocDieuChinh,
	case when tblSoChuaPhanBoPBDTTM.Type = 2 then 2 else tblMucLucNganSach.Type end Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	tblSoChuaPhanBoPBDTTM.sSoQuyetDinh as sSoQuyetDinh,
	case when tblSoChuaPhanBoPBDTTM.Type = 2 then 1 else tblMucLucNganSach.bHangCha end bHangCha,
	0 as IsRemainRow
	into tblMLNS_duplicatePBDTTM
	from tblMucLucNganSachPBDTTM tblMucLucNganSach
	left join tblSoChuaPhanBoPBDTTM on tblMucLucNganSach.iID_MLNS = tblSoChuaPhanBoPBDTTM.iID_MLNS
	order by sXauNoiMa
	
	-----------
	-- Dữ liệu nhận phân bổ
	declare @IdDotNhan nvarchar(500) = (select sDS_DotNhan from BH_DTTM_BHYT_ThanNhan_PhanBo where iID_DTTM_BHYT_ThanNhan_PhanBo = @ChungTuId);

	select ct.iID_MLNS, ct.iID_DTTM_BHYT_ThanNhan, ct.fDuToan INTO tmpNhanDTTM from BH_DTTM_BHYT_ThanNhan_ChiTiet ct
	JOIN BH_DTTM_BHYT_ThanNhan dt on dt.iID_DTTM_BHYT_ThanNhan = ct.iID_DTTM_BHYT_ThanNhan
	where dt.iID_DTTM_BHYT_ThanNhan IN (select * from splitstring( @IdDotNhan))
	-----------
	-- Dữ liệu đã phân bổ
	declare @dNgayQuyetDinhThuMua Datetime = (select dNgayQuyetDinh from BH_DTTM_BHYT_ThanNhan_PhanBo where iID_DTTM_BHYT_ThanNhan_PhanBo = @ChungTuId);

	select iID_MLNS, iID_DTTM_BHYT_ThanNhan, (0 - SUM(ISNULL(ctct.fDuToan, 0))) fDuToan
	INTO tmpDaPhanBoDTTM from BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
	JOIN BH_DTTM_BHYT_ThanNhan_PhanBo ct ON ctct.iID_DTTM_BHYT_ThanNhan_PhanBo = ct.iID_DTTM_BHYT_ThanNhan_PhanBo
	where 
	ct.iNamLamViec = @NamLamViec
	AND ct.dNgayQuyetDinh < @dNgayQuyetDinhThuMua
	group by iID_MLNS, iID_DTTM_BHYT_ThanNhan;
	----------------
	select * into tblResultThuMua from
	(
		SELECT * from tblMLNS_duplicatePBDTTM
		UNION ALL
		SELECT * from tblSoChuaPhanBoPBDTTM
		UNION ALL
		SELECT * from temp1PBDTTM
	) as test
	--order by test.sXauNoiMa, test.sSoQuyetDinh, test.iID_MaDonVi,test.Type,test.IsRemainRow

	SELECT
	rs.IID_DTTM_BHYT_ThanNhan,
	rs.iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet,
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
	rs.sNoiDung,
	rs.Type,
	rs.iID_MaDonVi,
	rs.sTenDonVi,
	rs.sSoQuyetDinh as sSoQuyetDinh,
	rs.IsRemainRow,
	rs.bHangCha,
	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fDuToan, 0) + (ISNULL(dpb.fDuToan, 0))
		WHEN rs.IsRemainRow = 0 AND rs.Type = 2 THEN ISNULL(dt.fDuToan,0) + (ISNULL(dpb.fDuToan, 0))
		ELSE rs.fDuToan
	END as fDuToan,
	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fDuToan,0) + (ISNULL(dpb.fDuToan, 0))
		WHEN rs.IsRemainRow = 0 AND rs.Type = 2 THEN ISNULL(dt.fDuToan,0) + (ISNULL(dpb.fDuToan, 0))
		ELSE rs.fDuToan
	END as fDuToanTruocDieuChinh
	FROM tblResultThuMua rs
	LEFT JOIN tmpNhanDTTM dt ON rs.iID_MLNS = dt.iID_MLNS and rs.IID_DTTM_BHYT_ThanNhan = dt.iID_DTTM_BHYT_ThanNhan
	LEFT JOIN tmpDaPhanBoDTTM dpb ON dpb.iID_MLNS = rs.iID_MLNS AND dpb.iID_DTTM_BHYT_ThanNhan = rs.IID_DTTM_BHYT_ThanNhan
	order by rs.sXauNoiMa, rs.sSoQuyetDinh, rs.iID_MaDonVi,rs.Type, rs.IsRemainRow


drop table tblMucLucNganSachPBDTTM;
drop table tblDonViPBDTTM;
drop table tblChungTuNhanPhanBoPBDTTM;
drop table tblChiTietDuToanNhanPBDTTM;
drop table tbl_MucLucPBDTTM;
drop table tempPBDTTM;
drop table temp1PBDTTM;
drop table tblSoChuaPhanBoPBDTTM;
drop table tblMLNS_duplicatePBDTTM;
drop table tmpNhanDTTM;
drop table tmpDaPhanBoDTTM;
drop table tblResultThuMua;

end
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_export_cap_phat_bo_sung]    Script Date: 1/24/2024 5:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bhxh_export_cap_phat_bo_sung]
	@VoucherId uniqueidentifier,
	@MaCSYT nvarchar(100),
	@NamLamViec int
AS
BEGIN
	
	select
		mlns.sXauNoiMa, mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sMoTa STenMLNS, mlns.bHangCha,
		case
			when chungtu.iID_MaCoSoYTe is null then null
			else CONCAT(chungtu.iID_MaCoSoYTe, ' - ', chungtu.sTenCoSoYTe)
		end as STenCSYT,
		chungtu.fDaQuyetToan,
		chungtu.fDaCapUng,
		chungtu.fThuaThieu,
		chungtu.fSoCapBoSung,
		chungtu.sGhiChu
	from
	(select sXauNoiMa, iID_MLNS, iID_MLNS_Cha, sMoTa, bHangCha from BH_DM_MucLucNganSach
	where sLNS like '904%'
	and iNamLamViec = @NamLamViec) mlns
	left join
	(select ctct.iID_MLNS,
		csyt.sTenCoSoYTe,
		csyt.iID_MaCoSoYTe,
		ctct.fDaQuyetToan,
		ctct.fDaCapUng,
		ctct.fThuaThieu,
		ctct.fSoCapBoSung,
		ctct.sGhiChu
	from BH_CP_CapBoSung_KCB_BHYT_ChiTiet ctct
	join BH_CP_CapBoSung_KCB_BHYT ct on ctct.iID_CTCapPhatBS = ct.iID_CTCapPhatBS
	join DM_CoSoYTe csyt on ctct.iID_MaCoSoYTe = csyt.iID_MaCoSoYTe
	where 
	ct.iID_CTCapPhatBS = @VoucherId
	and ctct.iID_MaCoSoYTe = @MaCSYT
	and csyt.iNamLamViec = @NamLamViec) chungtu on mlns.iID_MLNS = chungtu.iID_MLNS
	order by mlns.sXauNoiMa

END
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang]    Script Date: 1/24/2024 5:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang]
@MaDonVi NVARCHAR(max),
@Thang int,
@Nam AS int,
@IsOrderChucVu AS bit = 0,
@IsGiaTriAm AS bit = 0,
@IsCheckedMaHuongLuong AS bit = 0,
@IsNew AS bit = 0
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;
DECLARE @Cols AS NVARCHAR(MAX)
DECLARE @Query AS NVARCHAR(MAX)
DECLARE @ThangTruoc AS int
DECLARE @NamTruoc AS int
IF @Thang = 1 
BEGIN
SET @ThangTruoc = 12;
SET @NamTruoc = @Nam - 1;
END
ELSE 
BEGIN
SET @ThangTruoc = @Thang - 1;
SET @NamTruoc = @Nam;
END
SET @Cols = 'NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCKVCS_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,PCTEMTHU_TT,TA_TONG,PHAITRUKHAC_SUM'
SET @Query =
'
WITH CanBoThangTruoc as (
Select canbo.Ma_Hieu_CanBo, canbo.Parent MaDonViCu, canbo.Ten_DonVi TenDonViCu, canbo.Ma_CB CapBacCu
From TL_DM_CanBo canbo
Where 
canbo.Thang = ' + CAST(@ThangTruoc AS VARCHAR(2)) + '
And canbo.Nam = ' + CAST(@NamTruoc AS VARCHAR(4)) + '
AND canbo.Parent IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
),
blt AS (
SELECT * FROM TL_BangLuong_Thang
WHERE THANG = ' + CAST(@Thang AS VARCHAR(2)) + ' 
AND NAM = ' + CAST(@Nam AS VARCHAR(4)) + ' 
AND Ma_DonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
AND ( ' + CAST(@IsNew AS VARCHAR(2)) + '= 0 OR (Ma_Hieu_CanBo NOT IN (SELECT Ma_Hieu_CanBo FROM CanBoThangTruoc WHERE MaDonViCu = Ma_DonVi)))
),
BangLuongThang AS (
SELECT Thang, Nam, MaCanBo, ' + @Cols + ' FROM (
SELECT
dsCapNhapBangLuong.Thang AS Thang,
dsCapNhapBangLuong.Nam AS Nam,
bangLuong.Ma_CBo AS MaCanBo,
bangLuong.MA_PHUCAP AS MaPhuCap,
bangLuong.GIA_TRI AS GiaTri
FROM blt bangLuong
JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
ON bangLuong.parent = dsCapNhapBangLuong.Id
WHERE
bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @Cols + '''))
AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
AND dsCapNhapBangLuong.Status=1
) x
PIVOT
(
SUM(GiaTri)
FOR MaPhuCap IN (' + @Cols + ')
) pvt
), ThongTinCanBo AS (
SELECT
donVi.Ma_DonVi AS MaDonVi,
donVi.Ten_Donvi AS TenDonvi,
canBo.Ma_CanBo AS MaCanBo,
canBo.Ten_CanBo AS TenCanBo,
dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
canBo.Thang_TNN AS Tnn,
canBo.Nam_TN AS NTN,
dbo.f_luong_ntn(canBo.Ngay_NN, canBo.Ngay_XN, canBo.Ngay_TN, canBo.Thang_TNN, '+ CAST(@Thang AS VARCHAR(2)) +', '+ CAST(@Nam AS VARCHAR(4)) +') AS NamTn,
ISNULL(canBo.Ma_CB, ''0'') AS MaCapBac,
ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
chucVu.Ma_Cv AS MaChucVu,
chucVu.Ten_Cv AS TenChucVu,
CASE WHEN 1 = ' + CAST(@IsCheckedMaHuongLuong AS VARCHAR(10)) + ' THEN CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) ELSE canBo.So_TaiKhoan END AS Stk,
ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
capBac.XauNoiMa
FROM TL_DM_CanBo canBo
INNER JOIN TL_DM_DonVi donVi
ON canBo.Parent=donVi.Ma_DonVi
LEFT JOIN TL_DM_CapBac capBac
ON canBo.Ma_CB=capBac.Ma_Cb
LEFT JOIN TL_DM_ChucVu chucVu
ON canBo.Ma_CV=chucVu.Ma_Cv
WHERE
(canBo.IsDelete = 1 or (canbo.Ma_TangGiam = ''320'' and month(canbo.Ngay_XN) = '+ CAST(@Thang AS VARCHAR(2)) +' and year(canbo.Ngay_XN) = '+ CAST(@Nam AS VARCHAR(4)) +'))
AND canBo.Khong_Luong = 0
AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
)
SELECT
bangLuong.*,
canBo.MaDonVi,
canBo.TenDonVi,
canBo.TenCanBo,
canBo.Ten,
canBo.MaCapBac,
canBo.CapBac,
canBo.HSChucVu,
canBo.MaChucVu,
canBo.TenChucVu,
canBo.Stk,
canBo.NgayNhapNgu,
canBo.NgayXuatNgu,
canBo.NgayTaiNgu,
canBo.NamTN,
canBo.Tnn,
canBo.NTN,
canBo.XauNoiMa
FROM BangLuongThang bangLuong
INNER JOIN ThongTinCanBo canBo
ON bangLuong.MaCanBo = canBo.MaCanBo'
If @IsGiaTriAm = 1
SET @Query = @Query + ' WHERE bangLuong.THANHTIEN < 0';
IF @IsOrderChucVu = 1
SET @Query = @Query +' ORDER BY HSChucVu DESC, MaCapBac DESC, Ten ASC';
ELSE
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
execute(@Query)
END
;
;
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_2]    Script Date: 1/24/2024 5:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_2]
@MaDonVi NVARCHAR(max),
@Thang int,
@Nam AS int,
@IsOrderChucVu AS bit = 0,
@IsGiaTriAm AS bit = 0,
@IsCheckedMaHuongLuong AS bit = 0
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

DECLARE @Cols AS NVARCHAR(MAX)
DECLARE @Query AS NVARCHAR(MAX)

SET @Cols = 'NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,PCTEMTHU_TT,TA_TONG,PHAITRUKHAC_SUM'
SET @Query =
'
WITH BangLuongThang AS (
SELECT Thang, Nam, MaCanBo, ' + @Cols + ' FROM (
SELECT
dsCapNhapBangLuong.Thang AS Thang,
dsCapNhapBangLuong.Nam AS Nam,
bangLuong.Ma_CBo AS MaCanBo,
bangLuong.MA_PHUCAP AS MaPhuCap,
bangLuong.GIA_TRI AS GiaTri
FROM TL_BangLuong_Thang bangLuong
JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
ON bangLuong.parent = dsCapNhapBangLuong.Id
WHERE
bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @Cols + '''))
AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
AND dsCapNhapBangLuong.Status=1
) x
PIVOT
(
SUM(GiaTri)
FOR MaPhuCap IN (' + @Cols + ')
) pvt
), ThongTinCanBo AS (
SELECT
donVi.Ma_DonVi AS MaDonVi,
donVi.Ten_Donvi AS TenDonvi,
canBo.Ma_CanBo AS MaCanBo,
canBo.Ten_CanBo AS TenCanBo,
dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
canBo.Thang_TNN AS Tnn,
ISNULL(canBo.Ma_CB, ''0'') AS MaCapBac,
ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
chucVu.Ma_Cv AS MaChucVu,
chucVu.Ten_Cv AS TenChucVu,
CASE WHEN 1 = ' + CAST(@IsCheckedMaHuongLuong AS VARCHAR(10)) + ' THEN CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) ELSE canBo.So_TaiKhoan END AS Stk,
ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
capBac.XauNoiMa
FROM TL_DM_CanBo canBo
INNER JOIN TL_DM_DonVi donVi
ON canBo.Parent=donVi.Ma_DonVi
LEFT JOIN TL_DM_CapBac capBac
ON canBo.Ma_CB=capBac.Ma_Cb
LEFT JOIN TL_DM_ChucVu chucVu
ON canBo.Ma_CV=chucVu.Ma_Cv
WHERE
(canBo.IsDelete = 1 or (canbo.Ma_TangGiam = ''320'' and month(canbo.Ngay_XN) = '+ CAST(@Thang AS VARCHAR(2)) +' and year(canbo.Ngay_XN) = '+ CAST(@Nam AS VARCHAR(4)) +'))
AND canBo.Khong_Luong = 0
AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
)

SELECT
bangLuong.*,
canBo.MaDonVi,
canBo.TenDonVi,
canBo.TenCanBo,
canBo.Ten,
canBo.MaCapBac,
canBo.CapBac,
canBo.HSChucVu,
canBo.MaChucVu,
canBo.TenChucVu,
canBo.Stk,
canBo.NgayNhapNgu,
canBo.NgayXuatNgu,
canBo.NgayTaiNgu,
canBo.Tnn,
canBo.XauNoiMa
FROM BangLuongThang bangLuong
INNER JOIN ThongTinCanBo canBo
ON bangLuong.MaCanBo = canBo.MaCanBo'
If @IsGiaTriAm = 1
SET @Query = @Query + ' WHERE bangLuong.THANHTIEN < 0';
IF @IsOrderChucVu = 1
SET @Query = @Query +' ORDER BY HSChucVu DESC, MaCapBac DESC, Ten ASC';
ELSE
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
execute(@Query)
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]    Script Date: 1/24/2024 5:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]
@MaDonVi NVARCHAR(max),
@Thang int,
@Nam AS int,
@IsOrderChucVu AS bit = 0,
@IsGiaTriAm AS bit = 0,
@IsCheckedMaHuongLuong AS bit = 0,
@IsNew AS bit = 0
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

DECLARE @Cols AS NVARCHAR(MAX)
DECLARE @Query AS NVARCHAR(MAX)
DECLARE @ThangTruoc AS int
DECLARE @NamTruoc AS int

IF @Thang = 1 
BEGIN
	SET @ThangTruoc = 12;
	SET @NamTruoc = @Nam - 1;
END
ELSE 
BEGIN
	SET @ThangTruoc =  @Thang - 1;
	SET @NamTruoc = @Nam;
END

SET @Cols = 'NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCKVCS_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,PCTEMTHU_TT,TA_TONG,PHAITRUKHAC_SUM'
SET @Query =
'
WITH CanBoThangTruoc as (
			Select canbo.Ma_Hieu_CanBo, canbo.Parent MaDonViCu, canbo.Ten_DonVi TenDonViCu, canbo.Ma_CB CapBacCu
			From TL_DM_CanBo canbo
			Where 
				canbo.Thang = ' + CAST(@ThangTruoc AS VARCHAR(2)) + '
				And canbo.Nam = ' + CAST(@NamTruoc AS VARCHAR(4)) + '
				AND canbo.Parent IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		),
blt AS (
	SELECT * FROM TL_BangLuong_Thang
	WHERE THANG = ' + CAST(@Thang AS VARCHAR(2)) + ' 
	AND NAM = ' + CAST(@Nam AS VARCHAR(4)) + ' 
	AND Ma_DonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
	AND ( ' + CAST(@IsNew AS VARCHAR(2)) + '= 0 OR (Ma_Hieu_CanBo NOT IN (SELECT Ma_Hieu_CanBo FROM CanBoThangTruoc WHERE MaDonViCu = Ma_DonVi)))
),
BangLuongBHXH AS (
	select distinct base.sMaCbo, base.sTenCbo, base.sMaDonVi, base.iNam, base.iThang,
		LHT_TT.GiaTri_LHT_TT,
		PCCV_TT.GiaTri_PCCVBH_TT,
		PCTN_TT.GiaTri_PCTNBH_TT,
		BHXHCN_TT.GiaTri_BHXHCN_TT,
		BHYTCN_TT.GiaTri_BHYTCN_TT
	from TL_BangLuong_ThangBHXH base
	left join (
	select LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang, sum(nGiaTri) GiaTri_LHT_TT
	from TL_BangLuong_ThangBHXH LHT_TT
	where LHT_TT.iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND LHT_TT.iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND LHT_TT.sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND LHT_TT.sMaCheDo in (''OK_D14N_LBH_TT'', ''OK_T14N_LBH_TT'', ''BDN_D14N_LBH_TT'', ''BDN_T14N_LBH_TT'', ''CONOM_LBH_TT'')
	group by LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang) LHT_TT on base.sMaCbo = LHT_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCCVBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCCVBH_TT'', ''OK_T14N_PCCVBH_TT'', ''BDN_D14N_PCCVBH_TT'', ''BDN_T14N_PCCVBH_TT'', ''CONOM_LBH_PCCVBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCCV_TT on base.sMaCbo = PCCV_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCTNBH_TT'', ''OK_T14N_PCTNBH_TT'', ''BDN_D14N_PCTNBH_TT'', ''BDN_T14N_PCTNBH_TT'', ''CONOM_LBH_PCTNBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTN_TT on base.sMaCbo = PCTN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHXHCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_BHXHCN_TT'', ''OK_T14N_BHXHCN_TT'', ''BDN_D14N_BHXHCN_TT'', ''BDN_T14N_BHXHCN_TT'', ''CONOM_LBH_BHXHCN_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHXHCN_TT on base.sMaCbo = BHXHCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHYTCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''BDN_D14N_BHYTCN_TT'', ''OK_D14N_BHYTCN_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHYTCN_TT on base.sMaCbo = BHYTCN_TT.sMaCbo

	where base.iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND base.iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND base.sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
),
BangLuongThang AS (
SELECT Thang, Nam, MaCanBo, ' + @Cols + ' FROM (
SELECT
dsCapNhapBangLuong.Thang AS Thang,
dsCapNhapBangLuong.Nam AS Nam,
bangLuong.Ma_CBo AS MaCanBo,
bangLuong.MA_PHUCAP AS MaPhuCap,
CASE
	WHEN bangLuong.Ma_PhuCap = ''LHT_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_LHT_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCCV_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCCVBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCTN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCTNBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''BHXHCN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_BHXHCN_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''BHYTCN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_BHYTCN_TT, 0)
	ELSE bangLuong.Gia_Tri
END AS GiaTri
FROM blt bangLuong
JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
ON bangLuong.parent = dsCapNhapBangLuong.Id
LEFT JOIN BangLuongBHXH bhxh ON bangLuong.Ma_CBo = bhxh.sMaCbo AND bangLuong.Ma_DonVi = bhxh.sMaDonVi AND bangLuong.NAM = bhxh.iNam AND bangLuong.THANG = bhxh.iThang
WHERE
bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @Cols + '''))
AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
AND dsCapNhapBangLuong.Status=1
) x
PIVOT
(
SUM(GiaTri)
FOR MaPhuCap IN (' + @Cols + ')
) pvt
), ThongTinCanBo AS (
SELECT
donVi.Ma_DonVi AS MaDonVi,
donVi.Ten_Donvi AS TenDonvi,
canBo.Ma_CanBo AS MaCanBo,
canBo.Ten_CanBo AS TenCanBo,
dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
canBo.Thang_TNN AS Tnn,
canBo.Nam_TN AS NTN,
dbo.f_luong_ntn(canBo.Ngay_NN, canBo.Ngay_XN, canBo.Ngay_TN, canBo.Thang_TNN, '+ CAST(@Thang AS VARCHAR(2)) +', '+ CAST(@Nam AS VARCHAR(4)) +') AS NamTn,
ISNULL(canBo.Ma_CB, ''0'') AS MaCapBac,
ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
chucVu.Ma_Cv AS MaChucVu,
chucVu.Ten_Cv AS TenChucVu,
CASE WHEN 1 = ' + CAST(@IsCheckedMaHuongLuong AS VARCHAR(10)) + ' THEN CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) ELSE canBo.So_TaiKhoan END AS Stk,
ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
capBac.XauNoiMa
FROM TL_DM_CanBo canBo
INNER JOIN TL_DM_DonVi donVi
ON canBo.Parent=donVi.Ma_DonVi
LEFT JOIN TL_DM_CapBac capBac
ON canBo.Ma_CB=capBac.Ma_Cb
LEFT JOIN TL_DM_ChucVu chucVu
ON canBo.Ma_CV=chucVu.Ma_Cv
WHERE
(canBo.IsDelete = 1 or (canbo.Ma_TangGiam = ''320'' and month(canbo.Ngay_XN) = '+ CAST(@Thang AS VARCHAR(2)) +' and year(canbo.Ngay_XN) = '+ CAST(@Nam AS VARCHAR(4)) +'))
AND canBo.Khong_Luong = 0
AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
)

SELECT
bangLuong.*,
canBo.MaDonVi,
canBo.TenDonVi,
canBo.TenCanBo,
canBo.Ten,
canBo.MaCapBac,
canBo.CapBac,
canBo.HSChucVu,
canBo.MaChucVu,
canBo.TenChucVu,
canBo.Stk,
canBo.NgayNhapNgu,
canBo.NgayXuatNgu,
canBo.NgayTaiNgu,
canBo.NamTN,
canBo.Tnn,
canBo.NTN,
canBo.XauNoiMa
FROM BangLuongThang bangLuong
INNER JOIN ThongTinCanBo canBo
ON bangLuong.MaCanBo = canBo.MaCanBo'
If @IsGiaTriAm = 1
SET @Query = @Query + ' WHERE bangLuong.THANHTIEN < 0';
IF @IsOrderChucVu = 1
SET @Query = @Query +' ORDER BY HSChucVu DESC, MaCapBac DESC, Ten ASC';
ELSE
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
execute(@Query)
END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2]    Script Date: 1/24/2024 5:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2]
@MaDonVi NVARCHAR(max),
@Thang int,
@Nam AS int,
@IsOrderChucVu AS bit = 0,
@IsGiaTriAm AS bit = 0,
@IsCheckedMaHuongLuong AS bit = 0
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

DECLARE @Cols AS NVARCHAR(MAX)
DECLARE @Query AS NVARCHAR(MAX)

SET @Cols = 'NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,PCTEMTHU_TT,TA_TONG,PHAITRUKHAC_SUM'
SET @Query =
'
WITH 
BangLuongBHXH AS (
	select distinct base.sMaCbo, base.sTenCbo, base.sMaDonVi, base.iNam, base.iThang,
		LHT_TT.GiaTri_LHT_TT,
		PCCV_TT.GiaTri_PCCVBH_TT,
		PCTN_TT.GiaTri_PCTNBH_TT,
		BHXHCN_TT.GiaTri_BHXHCN_TT,
		BHYTCN_TT.GiaTri_BHYTCN_TT
	from TL_BangLuong_ThangBHXH base
	left join (
	select LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang, sum(nGiaTri) GiaTri_LHT_TT
	from TL_BangLuong_ThangBHXH LHT_TT
	where LHT_TT.iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND LHT_TT.iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND LHT_TT.sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND LHT_TT.sMaCheDo in (''OK_D14N_LBH_TT'', ''OK_T14N_LBH_TT'', ''BDN_D14N_LBH_TT'', ''BDN_T14N_LBH_TT'', ''CONOM_LBH_TT'')
	group by LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang) LHT_TT on base.sMaCbo = LHT_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCCVBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCCVBH_TT'', ''OK_T14N_PCCVBH_TT'', ''BDN_D14N_PCCVBH_TT'', ''BDN_T14N_PCCVBH_TT'', ''CONOM_LBH_PCCVBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCCV_TT on base.sMaCbo = PCCV_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCTNBH_TT'', ''OK_T14N_PCTNBH_TT'', ''BDN_D14N_PCTNBH_TT'', ''BDN_T14N_PCTNBH_TT'', ''CONOM_LBH_PCTNBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTN_TT on base.sMaCbo = PCTN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHXHCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_BHXHCN_TT'', ''OK_T14N_BHXHCN_TT'', ''BDN_D14N_BHXHCN_TT'', ''BDN_T14N_BHXHCN_TT'', ''CONOM_LBH_BHXHCN_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHXHCN_TT on base.sMaCbo = BHXHCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHYTCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''BDN_D14N_BHYTCN_TT'', ''OK_D14N_BHYTCN_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHYTCN_TT on base.sMaCbo = BHYTCN_TT.sMaCbo

	where base.iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND base.iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND base.sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
),
BangLuongThang AS (
SELECT Thang, Nam, MaCanBo, ' + @Cols + ' FROM (
SELECT
dsCapNhapBangLuong.Thang AS Thang,
dsCapNhapBangLuong.Nam AS Nam,
bangLuong.Ma_CBo AS MaCanBo,
bangLuong.MA_PHUCAP AS MaPhuCap,
CASE
	WHEN bangLuong.Ma_PhuCap = ''LHT_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_LHT_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCCV_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCCVBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCTN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCTNBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''BHXHCN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_BHXHCN_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''BHYTCN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_BHYTCN_TT, 0)
	ELSE bangLuong.Gia_Tri
END AS GiaTri
FROM TL_BangLuong_Thang bangLuong
JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
ON bangLuong.parent = dsCapNhapBangLuong.Id
LEFT JOIN BangLuongBHXH bhxh ON bangLuong.Ma_CBo = bhxh.sMaCbo AND bangLuong.Ma_DonVi = bhxh.sMaDonVi AND bangLuong.NAM = bhxh.iNam AND bangLuong.THANG = bhxh.iThang
WHERE
bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @Cols + '''))
AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
AND dsCapNhapBangLuong.Status=1
) x
PIVOT
(
SUM(GiaTri)
FOR MaPhuCap IN (' + @Cols + ')
) pvt
), ThongTinCanBo AS (
SELECT
donVi.Ma_DonVi AS MaDonVi,
donVi.Ten_Donvi AS TenDonvi,
canBo.Ma_CanBo AS MaCanBo,
canBo.Ten_CanBo AS TenCanBo,
dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
canBo.Thang_TNN AS Tnn,
ISNULL(canBo.Ma_CB, ''0'') AS MaCapBac,
ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
chucVu.Ma_Cv AS MaChucVu,
chucVu.Ten_Cv AS TenChucVu,
CASE WHEN 1 = ' + CAST(@IsCheckedMaHuongLuong AS VARCHAR(10)) + ' THEN CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) ELSE canBo.So_TaiKhoan END AS Stk,
ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
capBac.XauNoiMa
FROM TL_DM_CanBo canBo
INNER JOIN TL_DM_DonVi donVi
ON canBo.Parent=donVi.Ma_DonVi
LEFT JOIN TL_DM_CapBac capBac
ON canBo.Ma_CB=capBac.Ma_Cb
LEFT JOIN TL_DM_ChucVu chucVu
ON canBo.Ma_CV=chucVu.Ma_Cv
WHERE
(canBo.IsDelete = 1 or (canbo.Ma_TangGiam = ''320'' and month(canbo.Ngay_XN) = '+ CAST(@Thang AS VARCHAR(2)) +' and year(canbo.Ngay_XN) = '+ CAST(@Nam AS VARCHAR(4)) +'))
AND canBo.Khong_Luong = 0
AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
)

SELECT
bangLuong.*,
canBo.MaDonVi,
canBo.TenDonVi,
canBo.TenCanBo,
canBo.Ten,
canBo.MaCapBac,
canBo.CapBac,
canBo.HSChucVu,
canBo.MaChucVu,
canBo.TenChucVu,
canBo.Stk,
canBo.NgayNhapNgu,
canBo.NgayXuatNgu,
canBo.NgayTaiNgu,
canBo.Tnn,
canBo.XauNoiMa
FROM BangLuongThang bangLuong
INNER JOIN ThongTinCanBo canBo
ON bangLuong.MaCanBo = canBo.MaCanBo'
If @IsGiaTriAm = 1
SET @Query = @Query + ' WHERE bangLuong.THANHTIEN < 0';
IF @IsOrderChucVu = 1
SET @Query = @Query +' ORDER BY HSChucVu DESC, MaCapBac DESC, Ten ASC';
ELSE
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
execute(@Query)
END
;
;
;
GO


/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]    Script Date: 1/25/2024 8:57:54 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]    Script Date: 1/25/2024 8:57:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]
	@MaDonVi NVARCHAR(255),
	@DonViTinh int,
	@NamLamViec int
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
	isnull(sum(fThuBHXH_NLD), 0) DttDauNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NLD), 0)) Tang,
	(isnull(sum(fThuBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	union
	select
	6 STT,
	N'6' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(fThuBHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NSD), 0)) Tang,
	(isnull(sum(fThuBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	--Lấy dữ liệu NLĐ, NSD khối hạch toán
	union
	select
	8 STT,
	N'8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(fThuBHXH_NLD), 0) DttDauNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NLD), 0)) Tang,
	(isnull(sum(fThuBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	union
	select
	9 STT,
	N'9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(fThuBHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NSD), 0)) Tang,
	(isnull(sum(fThuBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	N'HT' Khoi,
	N'BHXH' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	-----------------------------------------
	--BHYT
	-- Khối dự toán
	union
	select
	16 STT,
	N'16' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(fThuBHYT_NLD), 0) + isnull(sum(fThuBHYT_NSD), 0)) DttDauNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NLD), 0) - isnull(sum(fThuBHYT_NSD), 0))) Tang,
	((isnull(sum(fThuBHYT_NLD), 0) + isnull(sum(fThuBHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
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
	isnull(sum(fThuBHYT_NLD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NLD), 0)) Tang,
	(isnull(sum(fThuBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
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
	isnull(sum(fThuBHYT_NSD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NSD), 0)) Tang,
	(isnull(sum(fThuBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
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
	(isnull(sum(fThuBHYT_NLD), 0) + isnull(sum(fThuBHYT_NSD), 0)) DttDauNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NLD), 0) - isnull(sum(fThuBHYT_NSD), 0))) Tang,
	((isnull(sum(fThuBHYT_NLD), 0) + isnull(sum(fThuBHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
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
	isnull(sum(fThuBHYT_NLD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NLD), 0)) Tang,
	(isnull(sum(fThuBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
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
	isnull(sum(fThuBHYT_NSD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NSD), 0)) Tang,
	(isnull(sum(fThuBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
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
	isnull(sum(fThuBHTN_NLD), 0) DttDauNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHTN_NLD), 0)) Tang,
	(isnull(sum(fThuBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	union
	select
	30 STT,
	N'30' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(fThuBHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NSD), 0)) Tang,
	(isnull(sum(fThuBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	--Lấy dữ liệu khối hạch toán
	union
	select
	32 STT,
	N'32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(fThuBHTN_NLD), 0) DttDauNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHTN_NLD), 0)) Tang,
	(isnull(sum(fThuBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	union
	select
	33 STT,
	N'33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(fThuBHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHTN_NSD), 0)) Tang,
	(isnull(sum(fThuBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
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
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
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
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
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
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
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
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
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
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
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
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
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
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
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
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
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
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
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
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
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
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
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
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
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
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
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
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
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
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
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
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
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
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
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
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
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
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	(isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_parent
	where STT in (15, 20)
	) result
	order by result.STT;

	select STT, MaSo, NoiDung, DttDauNam/@DonViTinh DttDauNam, Dtt6ThangDauNam/@DonViTinh Dtt6ThangDauNam, Dtt6ThangCuoiNam/@DonViTinh Dtt6ThangCuoiNam, TongCong/@DonViTinh TongCong, Tang/@DonViTinh Tang, Giam/@DonViTinh Giam, Khoi, Thu, Type, BHangCha from tbl_ddt_bhxh;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

END
;
;
GO


/****** Object:  StoredProcedure [dbo].[sp_bhxh_get_so_quyet_dinh_dtt_theo_ct]    Script Date: 1/25/2024 4:25:29 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_get_so_quyet_dinh_dtt_theo_ct]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_get_so_quyet_dinh_dtt_theo_ct]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_get_so_quyet_dinh_dtt]    Script Date: 1/25/2024 4:25:29 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_get_so_quyet_dinh_dtt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_get_so_quyet_dinh_dtt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_getalldotdutoanbhxh]    Script Date: 1/25/2024 4:25:29 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_getalldotdutoanbhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_getalldotdutoanbhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_getalldotdutoanbhxh]    Script Date: 1/25/2024 4:25:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_getalldotdutoanbhxh]
As
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DT]') AND type in (N'U')) drop table TBL_DT;
	
	SELECT 
		sSoQuyetDinh,
		dNgayQuyetDinh,
		iID_DTChungTu,
		sNgayQuyetDinh,
		sNgayQuyetDinhDMY,
		iLoaiDuToan,
		ROW_NUMBER() OVER (PARTITION BY sSoQuyetDinh, sNgayQuyetDinh ORDER BY iID_DTChungTu) as RowNum
	INTO TBL_DT
	FROM
	(
		SELECT iID_DTT_BHXH_PhanBo_ChungTu as iID_DTChungTu
			 , sSoQuyetDinh
			 , dNgayQuyetDinh
			 , CONVERT(varchar, dNgayQuyetDinh, 101) sNgayQuyetDinh
			 , CONVERT(varchar, dNgayQuyetDinh, 105) sNgayQuyetDinhDMY
			 , iLoaiDuToan
		FROM BH_DTT_BHXH_PhanBo_ChungTu

		UNION

		SELECT iID_DTTM_BHYT_ThanNhan_PhanBo as iID_DTChungTu
			 , sSoQuyetDinh
			 , dNgayQuyetDinh
			 , CONVERT(varchar, dNgayQuyetDinh, 101) sNgayQuyetDinh
			 , CONVERT(varchar, dNgayQuyetDinh, 105) sNgayQuyetDinhDMY
			 , iLoaiDuToan
		FROM BH_DTTM_BHYT_ThanNhan_PhanBo

		UNION 

		SELECT ID as iID_DTChungTu
			 , sSoQuyetDinh
			 , dNgayQuyetDinh
			 , CONVERT(varchar, dNgayQuyetDinh, 101) sNgayQuyetDinh
			 , CONVERT(varchar, dNgayQuyetDinh, 105) sNgayQuyetDinhDMY
			 , iLoaiDotNhanPhanBo iLoaiDuToan
		FROM BH_DTC_PhanBoDuToanChi
	) tbldt

	SELECT 
		sSoQuyetDinh,
		dNgayQuyetDinh,
		iID_DTChungTu,
		sNgayQuyetDinh,
		sNgayQuyetDinhDMY,
		iLoaiDuToan
	FROM TBL_DT
	WHERE RowNum = 1;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DT]') AND type in (N'U')) drop table TBL_DT;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_get_so_quyet_dinh_dtt]    Script Date: 1/25/2024 4:25:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_get_so_quyet_dinh_dtt]
	@NamLamViec int
AS
BEGIN
	select distinct dtt.sSoQuyetDinh, dtt.iLoaiDuToan,
		Convert(varchar, dtt.dNgayQuyetDinh, 101) sNgayQuyetDinh
	from BH_DTT_BHXH_PhanBo_ChungTu dtt
	where iNamLamViec = @NamLamViec
	--union
	--select distinct  dtt.sSoQuyetDinh, Convert(varchar, dtt.dNgayQuyetDinh, 101) sNgayQuyetDinh
	--	from BH_DTTM_BHYT_ThanNhan_PhanBo dtt
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_get_so_quyet_dinh_dtt_theo_ct]    Script Date: 1/25/2024 4:25:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_get_so_quyet_dinh_dtt_theo_ct]
	@NamLamViec int
AS
BEGIN

	select distinct dtt.sSoChungTu, dtt.sSoQuyetDinh, dtt.iLoaiDuToan,
		Convert(varchar, dtt.dNgayQuyetDinh, 101) sNgayQuyetDinh
	from BH_DTT_BHXH_PhanBo_ChungTu dtt
	where iNamLamViec = @NamLamViec

END
;
GO
