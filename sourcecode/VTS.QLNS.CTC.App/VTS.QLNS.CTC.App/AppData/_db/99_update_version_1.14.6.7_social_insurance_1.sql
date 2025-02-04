/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_tonghop]    Script Date: 7/15/2024 4:51:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]    Script Date: 7/15/2024 4:51:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_thctdv]    Script Date: 7/15/2024 4:51:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_thctdv]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_thctdv]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_tonghop]    Script Date: 7/15/2024 4:51:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong]    Script Date: 7/15/2024 4:51:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong]    Script Date: 7/15/2024 4:51:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong]
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

	declare @dNgayDieuChinh Datetime = (select top 1 dNgayChungTu from BH_DTT_BHXH_DieuChinh where iNamLamViec = @NamLamViec and iID_MaDonVi = @MaDonVi order by dNgayChungTu desc);

	--BHXH
	--Lấy dữ liệu NLĐ, NSD của khối dự toán
	select child.* into tbl_child from
	(
	select
	5 STT,
	N'5' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	1 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec 

	union all

	select 6 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec --and mlns.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan
		and mlns.sXauNoiMa in ('9020001-010-011-0001-0000', '9020001-010-011-0001-0003-0001', '9020001-010-011-0001-0004-0001', '9020001-010-011-0001-0005-0001') --Sĩ quan
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select 7 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec --and mlns.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
		and mlns.sXauNoiMa in ('9020001-010-011-0001-0001','9020001-010-011-0001-0003-0002','9020001-010-011-0001-0005-0002','9020001-010-011-0001-0004-0002') --QNCN
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select 8 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select 9 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select 10 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
	11 STT,
	N'6' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NSD' Type,
	1 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select 12 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec --and mlns.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan
		and mlns.sXauNoiMa in ('9020001-010-011-0001-0000', '9020001-010-011-0001-0003-0001', '9020001-010-011-0001-0004-0001', '9020001-010-011-0001-0005-0001') --Sĩ quan
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select 13 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum( ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum( ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum( ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum( ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec --and mlns.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
		and mlns.sXauNoiMa in ('9020001-010-011-0001-0001','9020001-010-011-0001-0003-0002','9020001-010-011-0001-0005-0002','9020001-010-011-0001-0004-0002') --QNCN
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select 14 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select 15 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select 16 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	--Lấy dữ liệu NLĐ, NSD khối hạch toán
	union all
	select
	18 STT,
	N'8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	1 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select 19 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec --and mlns.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
		and mlns.sXauNoiMa in ('9020002-010-011-0001-0000', '9020002-010-011-0001-0003-0001', '9020002-010-011-0001-0004-0001', '9020002-010-011-0001-0005-0001') --Sĩ quan
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select 20 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec --and mlns.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
		and mlns.sXauNoiMa in ('9020002-010-011-0001-0001','9020002-010-011-0001-0003-0002','9020002-010-011-0001-0005-0002','9020002-010-011-0001-0004-0002') --QNCN
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select 21 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select 22 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select 23 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
	24 STT,
	N'9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	N'HT' Khoi,
	N'BHXH' Thu,
	'NSD' Type,
	1 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select 25 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec --and mlns.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
		and mlns.sXauNoiMa in ('9020002-010-011-0001-0000', '9020002-010-011-0001-0003-0001', '9020002-010-011-0001-0004-0001', '9020002-010-011-0001-0005-0001') --Sĩ quan
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select 26 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec --and mlns.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
		and mlns.sXauNoiMa in ('9020002-010-011-0001-0001','9020002-010-011-0001-0003-0002','9020002-010-011-0001-0005-0002','9020002-010-011-0001-0004-0002') --QNCN
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select 27 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select 28 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select 29 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	-----------------------------------------
	--BHYT
	-- Khối dự toán
	union

	select
	36 STT,
	N'16' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) DttDauNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0) - isnull(sum(pbct.fBHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	1 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHYT_NLD, pb.fBHYT_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NLD, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.sXauNoiMa in
		(
		'9020001-010-011-0001-0000',  -- Sĩ quan
		'9020001-010-011-0001-0001'	, -- QNCN
		'9020001-010-011-0001-0002'  -- HSQ, BS
		)
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
	40 STT,
	N'18' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	1 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHYT_NLD, pb.fBHYT_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NLD, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.sXauNoiMa in
		(
		'9020001-010-011-0002-0000',  -- CC,CN, VCQP
		'9020001-010-011-0002-0001'  -- LĐHĐ
		)
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
		41 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHYT_NLD, pb.fBHYT_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NLD, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020001-010-011-0002-0000' -- CC,CN, VCQP
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
		42 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHYT_NLD, pb.fBHYT_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NLD, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020001-010-011-0002-0001' -- LĐHĐ
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
	43 STT,
	N'19' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	1 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHYT_NLD, pb.fBHYT_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NLD, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.sXauNoiMa in
		(
		'9020001-010-011-0002-0000',  -- CC,CN, VCQP
		'9020001-010-011-0002-0001'  -- LĐHĐ
		)
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
		44 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHYT_NLD, pb.fBHYT_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NLD, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020001-010-011-0002-0000' -- CC,CN, VCQP
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
		45 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHYT_NLD, pb.fBHYT_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NLD, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020001-010-011-0002-0001' -- LĐHĐ
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec


	-- Khối hạch toán
	union all

	select
	47 STT,
	N'21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) DttDauNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0) - isnull(sum(pbct.fBHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	1 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHYT_NLD, pb.fBHYT_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NLD, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.sXauNoiMa in
		(
		'9020002-010-011-0001-0000',  -- Sĩ quan
		'9020002-010-011-0001-0001'	, -- QNCN
		'9020002-010-011-0001-0002'  -- HSQ, BS
		)
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
	49 STT,
	N'23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	1 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHYT_NLD, pb.fBHYT_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NLD, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.sXauNoiMa in
		(
		'9020002-010-011-0002-0000',  -- CC,CN, VCQP
		'9020002-010-011-0002-0001'  -- LĐHĐ
		)
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
		50 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHYT_NLD, pb.fBHYT_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NLD, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020002-010-011-0002-0000'  -- CC,CN, VCQP
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
		51 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHYT_NLD, pb.fBHYT_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NLD, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020002-010-011-0002-0001'  -- LĐHĐ
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
	52 STT,
	N'24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	1 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHYT_NLD, pb.fBHYT_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NLD, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.sXauNoiMa in
		(
		'9020002-010-011-0002-0000',  -- CC,CN, VCQP
		'9020002-010-011-0002-0001'  -- LĐHĐ
		)
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
		53 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHYT_NLD, pb.fBHYT_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NLD, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020002-010-011-0002-0000'  -- CC,CN, VCQP
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
		54 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHYT_NLD, pb.fBHYT_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NLD, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020002-010-011-0002-0001'  -- LĐHĐ
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	--BHTN
	--Lấy dữ liệu khối dự toán
	union all

	select
	59 STT,
	N'29' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	1 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
		60 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020001-010-011-0001-0000' -- Sĩ quan
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
		61 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020001-010-011-0001-0001' -- QNCN
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
		62 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020001-010-011-0001-0002' -- HSQ-BS
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
		63 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
		64 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
	65 STT,
	N'30' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	1 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
		66 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan 
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
		67 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
		68 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
		69 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
		70 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	--Lấy dữ liệu khối hạch toán
	union all

	select
	72 STT,
	N'32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	1 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
		73 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
		74 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
		75 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
		76 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
		77 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
	78 STT,
	N'33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	1 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
		79 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
		80 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
		81 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
		82 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union all

	select
		83 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec
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
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NLD'
	and MaSo in (5, 8)

	union all
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
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NSD'
	and MaSo in (6, 9)

	union all
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
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'DT'
	and MaSo in (5, 6)

	union all
	select
	17 STT,
	N'7=8+9' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'HT'
	and MaSo in (8, 9)

	--BHYT
	union all
	select
	31 STT,
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
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'QN'
	and MaSo in (16, 21)

	union all
	select
	33 STT,
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
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NLD'
	and MaSo in (18, 23)

	union all
	select
	34 STT,
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
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NSD'
	and MaSo in (19, 24)

	union all
	select
	37 STT,
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
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'DT'
	and STT in (40, 43) and MaSo in (18, 19)

	union all
	select
		38 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(DttDauNam), 0) DttDauNam,
		isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
		((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
		(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'' Type,
		1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
		and Khoi = 'DT'
		and STT in (41,44)

	union all
	select
		39 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(DttDauNam), 0) DttDauNam,
		isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
		((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
		(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'' Type,
		1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
		and Khoi = 'DT'
		and STT in (42,45)

	union all
	select
	48 STT,
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
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'HT'
	and STT in (49,52) and MaSo in (23, 24)
	--BHTN
	union all
	select
	56 STT,
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
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NLD'
	and MaSo in (29, 32)

	union all
	select
	57 STT,
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
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NSD'
	and MaSo in (30, 33)

	union all
	select
	58 STT,
	N'28=29+30' MaSo,
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
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'DT'
	and MaSo in (29, 30)

	union all
	select
	71 STT,
	N'31=32+33' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'HT'
	and MaSo in (32, 33)
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
	and MaSo in ('4=5+6', '7=8+9')

	union all
	select
	55 STT,
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
	and MaSo in ('28=29+30', '31=32+33')

	union all
	select
	32 STT,
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
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHYT'
	and Type = 'LDHD'
	and MaSo in ('13=18+23', '14=19+24')

	union all
	select
	35 STT,
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
	1 BHangCha
	from tbl_child_cat
	where STT in (36, 37) and MaSo in ('16','17=18+19')
	union all
	select
	46 STT,
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
	1 BHangCha
	from tbl_child_cat
	where STT in (47, 48) and MaSo in ('21','22=23+24')
	) parent

	update tbl_parent 
	set DttDauNam = isnull(DttDauNam, 0) + isnull((select DttDauNam from tbl_child where STT in (36)), 0),
		Dtt6ThangDauNam = isnull(Dtt6ThangDauNam, 0) + isnull((select Dtt6ThangDauNam from tbl_child where STT in (36)), 0),
		Dtt6ThangCuoiNam = isnull(Dtt6ThangCuoiNam, 0) + isnull((select Dtt6ThangCuoiNam from tbl_child where STT in (36)), 0),
		TongCong = isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0),
		Tang = (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0)) - isnull(DttDauNam, 0),
		Giam = isnull(DttDauNam, 0) - (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0))
	where STT = 15

	update tbl_parent 
	set TongCong = isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0),
		Tang = (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0)) - isnull(DttDauNam, 0),
		Giam =isnull( DttDauNam, 0) - (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0))
	where STT = 15

	update tbl_parent 
	set DttDauNam = isnull(DttDauNam, 0) + isnull((select DttDauNam from tbl_child where STT in (47)), 0),
		Dtt6ThangDauNam = isnull(Dtt6ThangDauNam, 0) + isnull((select Dtt6ThangDauNam from tbl_child where STT in (47)), 0),
		Dtt6ThangCuoiNam = isnull(Dtt6ThangCuoiNam, 0) + isnull((select Dtt6ThangCuoiNam from tbl_child where STT in (47)), 0),
		TongCong = isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0),
		Tang = (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0)) - isnull(DttDauNam, 0),
		Giam = isnull(DttDauNam, 0) - (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0))
	where STT = 20

	update tbl_parent 
	set TongCong = isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0),
		Tang = (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0)) - isnull(DttDauNam, 0),
		Giam = isnull(DttDauNam, 0) - (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0))
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
	30 STT,
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
	where STT in (35, 46) and MaSo in ('15=16+17','20=21+22')
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
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_tonghop]    Script Date: 7/15/2024 4:51:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_tonghop]
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

	declare @dNgayDieuChinh Datetime = (select top 1 dNgayChungTu from BH_DTT_BHXH_DieuChinh where iNamLamViec = @NamLamViec and iID_MaDonVi = @MaDonVi order by dNgayChungTu desc);

	--BHXH
	--Lấy dữ liệu NLĐ, NSD của khối dự toán
	select child.* into tbl_child from
	(
	select
	5 STT,
	N'5' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	1 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHXH_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHXH_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec

	union all

	select 6 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHXH_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHXH_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec --and mlns.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan
		and mlns.sXauNoiMa in ('9020001-010-011-0001-0000', '9020001-010-011-0001-0003-0001', '9020001-010-011-0001-0004-0001', '9020001-010-011-0001-0005-0001') --Sĩ quan
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec

	union all

	select 7 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHXH_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHXH_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec 
	where --ctct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
	ctct.sXauNoiMa in ('9020001-010-011-0001-0001','9020001-010-011-0001-0003-0002','9020001-010-011-0001-0005-0002','9020001-010-011-0001-0004-0002') --QNCN

	union all

	select 8 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHXH_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHXH_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec 
	where ctct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS

	union all

	select 9 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHXH_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHXH_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
	where ctct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP

	union all

	select 10 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHXH_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHXH_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
	where ctct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

	union all

	select
	11 STT,
	N'6' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NSD' Type,
	1 BHangCha
	from
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHXH_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHXH_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec

	union all

	select 12 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHXH_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHXH_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where --ctct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan
			ctct.sXauNoiMa in ('9020001-010-011-0001-0000', '9020001-010-011-0001-0003-0001', '9020001-010-011-0001-0004-0001', '9020001-010-011-0001-0005-0001') --Sĩ quan

	union all

	select 13 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum( ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum( ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum( ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum( ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHXH_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHXH_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where --ctct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
			ctct.sXauNoiMa in ('9020001-010-011-0001-0001','9020001-010-011-0001-0003-0002','9020001-010-011-0001-0005-0002','9020001-010-011-0001-0004-0002') --QNCN

	union all

	select 14 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHXH_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHXH_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS

	union all

	select 15 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHXH_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHXH_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP

	union all

	select 16 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHXH_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHXH_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

	--Lấy dữ liệu NLĐ, NSD khối hạch toán
	union all

	select
	18 STT,
	N'8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	1 BHangCha
	from
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHXH_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHXH_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec

	union all

	select 19 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHXH_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHXH_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where --ctct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
			ctct.sXauNoiMa in ('9020002-010-011-0001-0000', '9020002-010-011-0001-0003-0001', '9020002-010-011-0001-0004-0001', '9020002-010-011-0001-0005-0001') --Sĩ quan

	union all

	select 20 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHXH_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHXH_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where --ctct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
			ctct.sXauNoiMa in ('9020002-010-011-0001-0001','9020002-010-011-0001-0003-0002','9020002-010-011-0001-0005-0002','9020002-010-011-0001-0004-0002') --QNCN

	union all

	select 21 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHXH_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHXH_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS

	union all

	select 22 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHXH_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHXH_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP

	union all

	select 23 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHXH_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHXH_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

	union all

	select
	24 STT,
	N'9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	N'HT' Khoi,
	N'BHXH' Thu,
	'NSD' Type,
	1 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHXH_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHXH_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec

	union all

	select 25 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHXH_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHXH_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where --ctct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
			ctct.sXauNoiMa in ('9020002-010-011-0001-0000', '9020002-010-011-0001-0003-0001', '9020002-010-011-0001-0004-0001', '9020002-010-011-0001-0005-0001') --Sĩ quan

	union all

	select 26 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHXH_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHXH_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where --ctct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
			ctct.sXauNoiMa in ('9020002-010-011-0001-0001','9020002-010-011-0001-0003-0002','9020002-010-011-0001-0005-0002','9020002-010-011-0001-0004-0002') --QNCN

	union all

	select 27 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHXH_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHXH_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS

	union all
	select 28 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHXH_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHXH_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP

	union all

	select 29 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHXH_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHXH_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

	-----------------------------------------
	--BHYT
	-- Khối dự toán
	union

	select
	36 STT,
	N'16' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fThu_BHYT_NLD), 0) + isnull(sum(pbct.fThu_BHYT_NSD), 0)) DttDauNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0) - isnull(sum(pbct.fThu_BHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fThu_BHYT_NLD), 0) + isnull(sum(pbct.fThu_BHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	1 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHYT_NLD, pb.fThu_BHYT_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHYT_NLD, pbct.fThu_BHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.sXauNoiMa in
		(
		'9020001-010-011-0001-0000',  -- Sĩ quan
		'9020001-010-011-0001-0001'	, -- QNCN
		'9020001-010-011-0001-0002'  -- HSQ, BS
		)
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec

	union all

	select
	40 STT,
	N'18' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHYT_NLD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	1 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHYT_NLD, pb.fThu_BHYT_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHYT_NLD, pbct.fThu_BHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.sXauNoiMa in
		(
		'9020001-010-011-0002-0000',  -- CC,CN, VCQP
		'9020001-010-011-0002-0001'  -- LĐHĐ
		)
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec

	union all

	select
		41 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHYT_NLD, pb.fThu_BHYT_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHYT_NLD, pbct.fThu_BHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020001-010-011-0002-0000' -- CC,CN, VCQP

	union all

	select
		42 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHYT_NLD, pb.fThu_BHYT_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHYT_NLD, pbct.fThu_BHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020001-010-011-0002-0001' -- LĐHĐ

	union all

	select
	43 STT,
	N'19' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHYT_NSD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	1 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHYT_NLD, pb.fThu_BHYT_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHYT_NLD, pbct.fThu_BHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.sXauNoiMa in
		(
		'9020001-010-011-0002-0000',  -- CC,CN, VCQP
		'9020001-010-011-0002-0001'  -- LĐHĐ
		)
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec

	union all

	select
		44 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHYT_NLD, pb.fThu_BHYT_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHYT_NLD, pbct.fThu_BHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020001-010-011-0002-0000' -- CC,CN, VCQP

	union all

	select
		45 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHYT_NLD, pb.fThu_BHYT_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHYT_NLD, pbct.fThu_BHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020001-010-011-0002-0001' -- LĐHĐ

	-- Khối hạch toán
	union all

	select
	47 STT,
	N'21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fThu_BHYT_NLD), 0) + isnull(sum(pbct.fThu_BHYT_NSD), 0)) DttDauNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0) - isnull(sum(pbct.fThu_BHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fThu_BHYT_NLD), 0) + isnull(sum(pbct.fThu_BHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	1 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHYT_NLD, pb.fThu_BHYT_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHYT_NLD, pbct.fThu_BHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.sXauNoiMa in
		(
		'9020002-010-011-0001-0000',  -- Sĩ quan
		'9020002-010-011-0001-0001'	, -- QNCN
		'9020002-010-011-0001-0002'  -- HSQ, BS
		)
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec

	union all

	select
	49 STT,
	N'23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHYT_NLD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	1 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHYT_NLD, pb.fThu_BHYT_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHYT_NLD, pbct.fThu_BHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.sXauNoiMa in
		(
		'9020002-010-011-0002-0000',  -- CC,CN, VCQP
		'9020002-010-011-0002-0001'  -- LĐHĐ
		)
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec

	union all

	select
		50 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHYT_NLD, pb.fThu_BHYT_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHYT_NLD, pbct.fThu_BHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020002-010-011-0002-0000'  -- CC,CN, VCQP

	union all

	select
		51 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHYT_NLD, pb.fThu_BHYT_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHYT_NLD, pbct.fThu_BHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020002-010-011-0002-0001'  -- LĐHĐ

	union all

	select
	52 STT,
	N'24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHYT_NSD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	1 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHYT_NLD, pb.fThu_BHYT_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHYT_NLD, pbct.fThu_BHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.sXauNoiMa in
		(
		'9020002-010-011-0002-0000',  -- CC,CN, VCQP
		'9020002-010-011-0002-0001'  -- LĐHĐ
		)
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec

	union all

	select
		53 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHYT_NLD, pb.fThu_BHYT_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHYT_NLD, pbct.fThu_BHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020002-010-011-0002-0000'  -- CC,CN, VCQP

	union all

	select
		54 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHYT_NLD, pb.fThu_BHYT_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHYT_NLD, pbct.fThu_BHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020002-010-011-0002-0001'  -- LĐHĐ

	--BHTN
	--Lấy dữ liệu khối dự toán
	union all

	select
	59 STT,
	N'29' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	1 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHTN_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHTN_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec

	union all

	select
		60 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHTN_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHTN_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020001-010-011-0001-0000' -- Sĩ quan

	union all

	select
		61 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHTN_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHTN_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020001-010-011-0001-0001' -- QNCN

	union all

	select
		62 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHTN_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHTN_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020001-010-011-0001-0002' -- HSQ-BS

	union all

	select
		63 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHTN_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHTN_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP

	union all

	select
		64 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHTN_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHTN_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

	union all

	select
	65 STT,
	N'30' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	1 BHangCha
	from
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHTN_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHTN_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec

	union all

	select
		66 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHTN_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHTN_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan 

	union all

	select
		67 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHTN_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHTN_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN

	union all

	select
		68 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHTN_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHTN_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS

	union all

	select
		69 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHTN_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHTN_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP

	union all

	select
		70 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHTN_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHTN_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

	--Lấy dữ liệu khối hạch toán
	union all

	select
	72 STT,
	N'32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	1 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHTN_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHTN_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec

	union all

	select
		73 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHTN_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHTN_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan

	union all

	select
		74 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHTN_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHTN_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN

	union all

	select
		75 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHTN_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHTN_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS

	union all

	select
		76 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHTN_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHTN_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
	union all

	select
		77 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHTN_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHTN_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

	union all

	select
	78 STT,
	N'33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	1 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHTN_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHTN_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec

	union all

	select
		79 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHTN_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHTN_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan

	union all

	select
		80 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHTN_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHTN_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN

	union all

	select
		81 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHTN_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHTN_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS

	union all

	select
		82 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHTN_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHTN_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP

	union all

	select
		83 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHTN_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHTN_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa 
			and ctct.iNamLamViec = @NamLamViec
			where ctct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ
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
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NLD'
	and MaSo in (5, 8)

	union all
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
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NSD'
	and MaSo in (6, 9)

	union all
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
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'DT'
	and MaSo in (5, 6)

	union all
	select
	17 STT,
	N'7=8+9' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'HT'
	and MaSo in (8, 9)

	--BHYT
	union all
	select
	31 STT,
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
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'QN'
	and MaSo in (16, 21)

	union all
	select
	33 STT,
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
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NLD'
	and MaSo in (18, 23)

	union all
	select
	34 STT,
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
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NSD'
	and MaSo in (19, 24)

	union all
	select
	37 STT,
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
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'DT'
	and STT in (40, 43) and MaSo in (18, 19)

	union all
	select
		38 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(DttDauNam), 0) DttDauNam,
		isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
		((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
		(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'' Type,
		1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
		and Khoi = 'DT'
		and STT in (41,44)

	union all
	select
		39 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(DttDauNam), 0) DttDauNam,
		isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
		((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
		(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'' Type,
		1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
		and Khoi = 'DT'
		and STT in (42,45)

	union all
	select
	48 STT,
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
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'HT'
	and STT in (49,52) and MaSo in (23, 24)
	--BHTN
	union all
	select
	56 STT,
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
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NLD'
	and MaSo in (29, 32)

	union all
	select
	57 STT,
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
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NSD'
	and MaSo in (30, 33)

	union all
	select
	58 STT,
	N'28=29+30' MaSo,
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
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'DT'
	and MaSo in (29, 30)

	union all
	select
	71 STT,
	N'31=32+33' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'HT'
	and MaSo in (32, 33)
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
	and MaSo in ('4=5+6', '7=8+9')

	union all
	select
	55 STT,
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
	and MaSo in ('28=29+30', '31=32+33')

	union all
	select
	32 STT,
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
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHYT'
	and Type = 'LDHD'
	and MaSo in ('13=18+23', '14=19+24')

	union all
	select
	35 STT,
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
	1 BHangCha
	from tbl_child_cat
	where STT in (36, 37) and MaSo in ('16','17=18+19')
	union all
	select
	46 STT,
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
	1 BHangCha
	from tbl_child_cat
	where STT in (47, 48) and MaSo in ('21','22=23+24')
	) parent

	update tbl_parent 
	set DttDauNam = isnull(DttDauNam, 0) + isnull((select DttDauNam from tbl_child where STT in (36)), 0),
		Dtt6ThangDauNam = isnull(Dtt6ThangDauNam, 0) + isnull((select Dtt6ThangDauNam from tbl_child where STT in (36)), 0),
		Dtt6ThangCuoiNam = isnull(Dtt6ThangCuoiNam, 0) + isnull((select Dtt6ThangCuoiNam from tbl_child where STT in (36)), 0),
		TongCong = isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0),
		Tang = (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0)) - isnull(DttDauNam, 0),
		Giam = isnull(DttDauNam, 0) - (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0))
	where STT = 15

	update tbl_parent 
	set TongCong = isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0),
		Tang = (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0)) - isnull(DttDauNam, 0),
		Giam = isnull(DttDauNam, 0) - (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0))
	where STT = 15

	update tbl_parent 
	set DttDauNam = isnull(DttDauNam, 0) + isnull((select DttDauNam from tbl_child where STT in (47)), 0),
		Dtt6ThangDauNam = isnull(Dtt6ThangDauNam, 0) + isnull((select Dtt6ThangDauNam from tbl_child where STT in (47)), 0),
		Dtt6ThangCuoiNam = isnull(Dtt6ThangCuoiNam, 0) + isnull((select Dtt6ThangCuoiNam from tbl_child where STT in (47)), 0),
		TongCong = isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0),
		Tang = (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0)) - isnull(DttDauNam, 0),
		Giam = isnull(DttDauNam, 0) - (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0))
	where STT = 20

	update tbl_parent 
	set TongCong = isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0),
		Tang = (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0)) - isnull(DttDauNam, 0),
		Giam = isnull(DttDauNam, 0) - (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0))
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
	30 STT,
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
	where STT in (35, 46) and MaSo in ('15=16+17','20=21+22')
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
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_thctdv]    Script Date: 7/15/2024 4:51:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_thctdv]
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

	declare @dNgayDieuChinh Datetime = (select top 1 dNgayChungTu from BH_DTT_BHXH_DieuChinh where iNamLamViec = @NamLamViec and iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) order by dNgayChungTu desc);

	--BHXH
	--Lấy dữ liệu NLĐ, NSD của khối dự toán
	select child.* into tbl_child from
	(
	select
	5 STT,
	N'5' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
	isnull(sum(ctct.Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	(isnull(sum(ctct.Dtt6ThangDauNam), 0) + isnull(sum(ctct.Dtt6ThangCuoiNam), 0)) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, isnull(sum(pb.fBHXH_NLD), 0) fBHXH_NLD--, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		group by mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu
		) pbct
		left join (
		select sXauNoiMa, isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam, isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam 
		from BH_DTT_BHXH_DieuChinh_ChiTiet 
		where iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and iNamLamViec = @NamLamViec
		group by sXauNoiMa
			) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa

	union all

	select pb.STT, pb.MaSo, pb.NoiDung, dt.fBHXH_NLD DttDauNam, pb.Dtt6ThangDauNam, pb.Dtt6ThangCuoiNam, pb.TongCong, pb.Tang, pb.Giam, pb.Khoi, pb.Thu, pb.Type, pb.BHangCha from
	(select
			5 STT,
			'' MaSo,
			'    ' + dv.sTenDonVi NoiDung,
			isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
			isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
			isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
			isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
			((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
			(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
			'DT' Khoi,
			'BHXH' Thu,
			'NLD' Type,
			0 BHangCha,
			pbct.iID_MaDonVi
		from (select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NLD, pb.iID_MaDonVi
			from BH_DM_MucLucNganSach mlns
			cross join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
			BH_DTT_BHXH_PhanBo_ChungTu pb 
			join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
			where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
			and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
			) pb --on mlns.sXauNoiMa = pb.sXauNoiMa
			where 
			mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
			and mlns.iTrangThai = 1
			) pbct
			left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi 
				and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec
			left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and dv.iNamLamViec = @NamLamViec
			group by dv.sTenDonVi, pbct.iID_MaDonVi) pb
			left join
			(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NLD, pb.iID_MaDonVi
			from BH_DM_MucLucNganSach mlns
			left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
			BH_DTT_BHXH_PhanBo_ChungTu pb 
			join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
			where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
			and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
			) pb on mlns.sXauNoiMa = pb.sXauNoiMa
			where 
			mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
			and mlns.iTrangThai = 1
			) dt on pb.iID_MaDonVi = dt.iID_MaDonVi

	union all

	select
		6 STT,
		N'6' MaSo,
		N'- Người sử dụng LĐ đóng' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, isnull(sum(pb.fBHXH_NSD), 0) fBHXH_NSD--, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		group by mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu
		) pbct
		left join (
		select sXauNoiMa, isnull(sum(fThuBHXH_NSD_QTDauNam), 0) fThuBHXH_NSD_QTDauNam, isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) fThuBHXH_NSD_QTCuoiNam 
		from BH_DTT_BHXH_DieuChinh_ChiTiet 
		where iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and iNamLamViec = @NamLamViec
		group by sXauNoiMa
			) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa

	union all

	select pb.STT, pb.MaSo, pb.NoiDung, dt.fBHXH_NSD BhxhNsdDauNam, pb.BhxhNsd6ThangDauNam, pb.BhxhNsd6ThangCuoiNam, pb.TongCong, pb.Tang, pb.Giam, pb.Khoi, pb.Thu, pb.Type, pb.BHangCha from
	(select
		6 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha,
		pbct.iID_MaDonVi
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NSD, pb.iID_MaDonVi
			from BH_DM_MucLucNganSach mlns
			cross join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
			BH_DTT_BHXH_PhanBo_ChungTu pb 
			join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
			where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
			and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
			) pb --on mlns.sXauNoiMa = pb.sXauNoiMa
			where 
			mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
			and mlns.iTrangThai = 1
			) pbct
			left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi 
				and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec
			left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and dv.iNamLamViec = @NamLamViec
			group by dv.sTenDonVi, pbct.iID_MaDonVi) pb
			left join
			(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NSD, pb.iID_MaDonVi
			from BH_DM_MucLucNganSach mlns
			left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
			BH_DTT_BHXH_PhanBo_ChungTu pb 
			join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
			where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
			and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
			) pb on mlns.sXauNoiMa = pb.sXauNoiMa
			where 
			mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
			and mlns.iTrangThai = 1
			) dt on pb.iID_MaDonVi = dt.iID_MaDonVi

	--Lấy dữ liệu NLĐ, NSD khối hạch toán
	union all

	select
		8 STT,
		N'8' MaSo,
		N'- Người lao động đóng' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from (select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, isnull(sum(pb.fBHXH_NLD), 0) fBHXH_NLD--, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		group by mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu
		) pbct
		left join (
		select sXauNoiMa, isnull(sum(fThuBHXH_NLD_QTDauNam), 0) fThuBHXH_NLD_QTDauNam, isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) fThuBHXH_NLD_QTCuoiNam 
		from BH_DTT_BHXH_DieuChinh_ChiTiet 
		where iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and iNamLamViec = @NamLamViec
		group by sXauNoiMa
			) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa

	union all

	select pb.STT, pb.MaSo, pb.NoiDung, dt.fBHXH_NLD DttDauNam, pb.Dtt6ThangDauNam, pb.Dtt6ThangCuoiNam, pb.TongCong, pb.Tang, pb.Giam, pb.Khoi, pb.Thu, pb.Type, pb.BHangCha from
	(select
		8 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha,
		pbct.iID_MaDonVi
	from (select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NLD, pb.iID_MaDonVi
			from BH_DM_MucLucNganSach mlns
			cross join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
			BH_DTT_BHXH_PhanBo_ChungTu pb 
			join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
			where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
			and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
			) pb --on mlns.sXauNoiMa = pb.sXauNoiMa
			where 
			mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
			and mlns.iTrangThai = 1
			) pbct
			left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi 
				and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec
			left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and dv.iNamLamViec = @NamLamViec
			group by dv.sTenDonVi, pbct.iID_MaDonVi) pb
			left join
			(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NLD, pb.iID_MaDonVi
			from BH_DM_MucLucNganSach mlns
			left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
			BH_DTT_BHXH_PhanBo_ChungTu pb 
			join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
			where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
			and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
			) pb on mlns.sXauNoiMa = pb.sXauNoiMa
			where 
			mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
			and mlns.iTrangThai = 1
			) dt on pb.iID_MaDonVi = dt.iID_MaDonVi

	union all

	select
		9 STT,
		N'9' MaSo,
		N'- Người sử dụng LĐ đóng' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		N'HT' Khoi,
		N'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from (select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, isnull(sum(pb.fBHXH_NSD), 0) fBHXH_NSD--, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		group by mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu
		) pbct
		left join (
		select sXauNoiMa, isnull(sum(fThuBHXH_NSD_QTDauNam), 0) fThuBHXH_NSD_QTDauNam, isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) fThuBHXH_NSD_QTCuoiNam 
		from BH_DTT_BHXH_DieuChinh_ChiTiet 
		where iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and iNamLamViec = @NamLamViec
		group by sXauNoiMa
			) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa

	union all

	select pb.STT, pb.MaSo, pb.NoiDung, dt.fBHXH_NSD BhxhNsdDauNam, pb.Dtt6ThangDauNam, pb.Dtt6ThangCuoiNam, pb.TongCong, pb.Tang, pb.Giam, pb.Khoi, pb.Thu, pb.Type, pb.BHangCha from
	(select
		9 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		N'HT' Khoi,
		N'BHXH' Thu,
		'NSD' Type,
		0 BHangCha,
		pbct.iID_MaDonVi
	from (select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NSD, pb.iID_MaDonVi
			from BH_DM_MucLucNganSach mlns
			cross join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
			BH_DTT_BHXH_PhanBo_ChungTu pb 
			join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
			where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
			and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
			) pb --on mlns.sXauNoiMa = pb.sXauNoiMa
			where 
			mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
			and mlns.iTrangThai = 1
			) pbct
			left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi 
				and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec
			left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and dv.iNamLamViec = @NamLamViec
			group by dv.sTenDonVi, pbct.iID_MaDonVi) pb
			left join
			(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NSD, pb.iID_MaDonVi
			from BH_DM_MucLucNganSach mlns
			left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
			BH_DTT_BHXH_PhanBo_ChungTu pb 
			join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
			where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
			and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
			) pb on mlns.sXauNoiMa = pb.sXauNoiMa
			where 
			mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
			and mlns.iTrangThai = 1
			) dt on pb.iID_MaDonVi = dt.iID_MaDonVi
	-----------------------------------------
	--BHYT
	-- Khối dự toán
	union all

	select
		16 STT,
		N'16' MaSo,
		N'- BHYT quân nhân' NoiDung,
		(isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) DttDauNam,
		(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
		(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
		(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0) - isnull(sum(pbct.fBHYT_NSD), 0))) Tang,
		((isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'QN' Type,
		0 BHangCha
	from (select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, isnull(sum(pb.fBHYT_NLD), 0) fBHYT_NLD, isnull(sum(pb.fBHYT_NSD), 0) fBHYT_NSD--, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NLD, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.sXauNoiMa in
		(
		'9020001-010-011-0001-0000',  -- Sĩ quan
		'9020001-010-011-0001-0001'	, -- QNCN
		'9020001-010-011-0001-0002'  -- HSQ, BS
		)
		and mlns.iTrangThai = 1
		group by mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu
		) pbct
		left join (
		select sXauNoiMa, isnull(sum(fThuBHYT_NLD_QTDauNam), 0) fThuBHYT_NLD_QTDauNam, isnull(sum(fThuBHYT_NSD_QTDauNam), 0) fThuBHYT_NSD_QTDauNam,
			isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) fThuBHYT_NLD_QTCuoiNam, isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) fThuBHYT_NSD_QTCuoiNam
		from BH_DTT_BHXH_DieuChinh_ChiTiet 
		where iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and iNamLamViec = @NamLamViec
		group by sXauNoiMa
			) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa

	union all

	select pb.STT, pb.MaSo, pb.NoiDung, dt.DttDauNam, pb.Dtt6ThangDauNam, pb.Dtt6ThangCuoiNam, pb.TongCong, pb.Tang, pb.Giam, pb.Khoi, pb.Thu, pb.Type, pb.BHangCha from
	(select
		16 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		(isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) DttDauNam,
		(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
		(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
		(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0) - isnull(sum(pbct.fBHYT_NSD), 0))) Tang,
		((isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'QN' Type,
		0 BHangCha,
		pbct.iID_MaDonVi
	from (select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHYT_NLD, pb.fBHYT_NSD, pb.iID_MaDonVi
			from BH_DM_MucLucNganSach mlns
			cross join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NLD, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
			BH_DTT_BHXH_PhanBo_ChungTu pb 
			join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
			where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
			and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
			) pb --on mlns.sXauNoiMa = pb.sXauNoiMa
			where 
			mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
			and mlns.sXauNoiMa in (
			'9020001-010-011-0001-0000',  -- Sĩ quan
			'9020001-010-011-0001-0001'	, -- QNCN
			'9020001-010-011-0001-0002')  -- HSQ, BS
			and mlns.iTrangThai = 1
			) pbct
			left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi 
				and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec
			left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and dv.iNamLamViec = @NamLamViec
			group by dv.sTenDonVi, pbct.iID_MaDonVi) pb
			left join
			(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, (isnull(sum(pb.fBHYT_NLD), 0) + isnull(sum(pb.fBHYT_NSD), 0)) DttDauNam, pb.iID_MaDonVi
			from BH_DM_MucLucNganSach mlns
			left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NLD, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
			BH_DTT_BHXH_PhanBo_ChungTu pb 
			join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
			where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
			and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
			) pb on mlns.sXauNoiMa = pb.sXauNoiMa
			where 
			mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
			and mlns.sXauNoiMa in (
			'9020001-010-011-0001-0000',  -- Sĩ quan
			'9020001-010-011-0001-0001'	, -- QNCN
			'9020001-010-011-0001-0002')  -- HSQ, BS
			and mlns.iTrangThai = 1
			group by mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.iID_MaDonVi
			) dt on pb.iID_MaDonVi = dt.iID_MaDonVi

	union all

	select
		18 STT,
		N'18' MaSo,
		N'+ Người lao động đóng' NoiDung,
		isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from (select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, isnull(sum(pb.fBHYT_NLD), 0) fBHYT_NLD--, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.sXauNoiMa in
		(
		'9020001-010-011-0002-0000',  -- CC,CN, VCQP
		'9020001-010-011-0002-0001'  -- LĐHĐ
		)
		and mlns.iTrangThai = 1
		group by mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu
		) pbct
		left join (
		select sXauNoiMa, isnull(sum(fThuBHYT_NLD_QTDauNam), 0) fThuBHYT_NLD_QTDauNam, isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) fThuBHYT_NLD_QTCuoiNam 
		from BH_DTT_BHXH_DieuChinh_ChiTiet 
		where iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and iNamLamViec = @NamLamViec
		group by sXauNoiMa
			) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa

	union all

	select pb.STT, pb.MaSo, pb.NoiDung, dt.DttDauNam, pb.Dtt6ThangDauNam, pb.Dtt6ThangCuoiNam, pb.TongCong, pb.Tang, pb.Giam, pb.Khoi, pb.Thu, pb.Type, pb.BHangCha from
	(select
		18 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha,
		pbct.iID_MaDonVi
	from (select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHYT_NLD, pb.fBHYT_NSD, pb.iID_MaDonVi
			from BH_DM_MucLucNganSach mlns
			cross join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NLD, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
			BH_DTT_BHXH_PhanBo_ChungTu pb 
			join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
			where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
			and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
			) pb --on mlns.sXauNoiMa = pb.sXauNoiMa
			where 
			mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
			and mlns.sXauNoiMa in (
			'9020001-010-011-0002-0000',  -- CC,CN, VCQP
			'9020001-010-011-0002-0001')  -- LĐHĐ
			and mlns.iTrangThai = 1
			) pbct
			left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi 
				and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec
			left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and dv.iNamLamViec = @NamLamViec
			group by dv.sTenDonVi, pbct.iID_MaDonVi) pb
			left join
			(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, isnull(sum(pb.fBHYT_NLD), 0) DttDauNam, pb.iID_MaDonVi
			from BH_DM_MucLucNganSach mlns
			left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NLD, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
			BH_DTT_BHXH_PhanBo_ChungTu pb 
			join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
			where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
			and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
			) pb on mlns.sXauNoiMa = pb.sXauNoiMa
			where 
			mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
			and mlns.sXauNoiMa in (
			'9020001-010-011-0002-0000',  -- CC,CN, VCQP
			'9020001-010-011-0002-0001')  -- LĐHĐ
			and mlns.iTrangThai = 1
			group by mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.iID_MaDonVi
			) dt on pb.iID_MaDonVi = dt.iID_MaDonVi

	union all

	select
		19 STT,
		N'19' MaSo,
		N'+ Người sử dụng LĐ đóng' NoiDung,
		isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from (select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, isnull(sum(pb.fBHYT_NSD), 0) fBHYT_NSD--, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.sXauNoiMa in
		(
		'9020001-010-011-0002-0000',  -- CC,CN, VCQP
		'9020001-010-011-0002-0001'  -- LĐHĐ
		)
		and mlns.iTrangThai = 1
		group by mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu
		) pbct
		left join (
		select sXauNoiMa, isnull(sum(fThuBHYT_NSD_QTDauNam), 0) fThuBHYT_NSD_QTDauNam, isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) fThuBHYT_NSD_QTCuoiNam 
		from BH_DTT_BHXH_DieuChinh_ChiTiet 
		where iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and iNamLamViec = @NamLamViec
		group by sXauNoiMa
			) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa

	union all

	select pb.STT, pb.MaSo, pb.NoiDung, dt.DttDauNam, pb.Dtt6ThangDauNam, pb.Dtt6ThangCuoiNam, pb.TongCong, pb.Tang, pb.Giam, pb.Khoi, pb.Thu, pb.Type, pb.BHangCha from
	(select
		19 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha,
		pbct.iID_MaDonVi
	from (select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHYT_NSD, pb.iID_MaDonVi
			from BH_DM_MucLucNganSach mlns
			cross join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
			BH_DTT_BHXH_PhanBo_ChungTu pb 
			join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
			where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
			and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
			) pb --on mlns.sXauNoiMa = pb.sXauNoiMa
			where 
			mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
			and mlns.sXauNoiMa in (
			'9020001-010-011-0002-0000',  -- CC,CN, VCQP
			'9020001-010-011-0002-0001')  -- LĐHĐ
			and mlns.iTrangThai = 1
			) pbct
			left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi 
				and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec
			left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and dv.iNamLamViec = @NamLamViec
			group by dv.sTenDonVi, pbct.iID_MaDonVi) pb
			left join
			(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, isnull(sum(pb.fBHYT_NSD), 0) DttDauNam, pb.iID_MaDonVi
			from BH_DM_MucLucNganSach mlns
			left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
			BH_DTT_BHXH_PhanBo_ChungTu pb 
			join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
			where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
			and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
			) pb on mlns.sXauNoiMa = pb.sXauNoiMa
			where 
			mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
			and mlns.sXauNoiMa in (
			'9020001-010-011-0002-0000',  -- CC,CN, VCQP
			'9020001-010-011-0002-0001')  -- LĐHĐ
			and mlns.iTrangThai = 1
			group by mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.iID_MaDonVi
			) dt on pb.iID_MaDonVi = dt.iID_MaDonVi

	-- Khối hạch toán

	union all

	select
	21 STT,
	N'21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) DttDauNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0) - isnull(sum(pbct.fBHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from (select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, isnull(sum(pb.fBHYT_NLD), 0) fBHYT_NLD, isnull(sum(pb.fBHYT_NSD), 0) fBHYT_NSD--, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NLD, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.sXauNoiMa in
		(
		'9020002-010-011-0001-0000',  -- Sĩ quan
		'9020002-010-011-0001-0001'	, -- QNCN
		'9020002-010-011-0001-0002'  -- HSQ, BS
		)
		and mlns.iTrangThai = 1
		group by mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu
		) pbct
		left join (
		select sXauNoiMa, isnull(sum(fThuBHYT_NLD_QTDauNam), 0) fThuBHYT_NLD_QTDauNam, isnull(sum(fThuBHYT_NSD_QTDauNam), 0) fThuBHYT_NSD_QTDauNam,
			isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) fThuBHYT_NLD_QTCuoiNam, isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) fThuBHYT_NSD_QTCuoiNam
		from BH_DTT_BHXH_DieuChinh_ChiTiet 
		where iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and iNamLamViec = @NamLamViec
		group by sXauNoiMa
			) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa

	union all

	select pb.STT, pb.MaSo, pb.NoiDung, dt.DttDauNam, pb.Dtt6ThangDauNam, pb.Dtt6ThangCuoiNam, pb.TongCong, pb.Tang, pb.Giam, pb.Khoi, pb.Thu, pb.Type, pb.BHangCha from
	(select
		21 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		(isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) DttDauNam,
		(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
		(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
		(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0) - isnull(sum(pbct.fBHYT_NSD), 0))) Tang,
		((isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'QN' Type,
		0 BHangCha,
		pbct.iID_MaDonVi
	from (select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHYT_NLD, pb.fBHYT_NSD, pb.iID_MaDonVi
			from BH_DM_MucLucNganSach mlns
			cross join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NLD, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
			BH_DTT_BHXH_PhanBo_ChungTu pb 
			join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
			where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
			and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
			) pb --on mlns.sXauNoiMa = pb.sXauNoiMa
			where 
			mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
			and mlns.sXauNoiMa in (
			'9020002-010-011-0001-0000',  -- Sĩ quan
			'9020002-010-011-0001-0001'	, -- QNCN
			'9020002-010-011-0001-0002')  -- HSQ, BS
			and mlns.iTrangThai = 1
			) pbct
			left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi 
				and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec
			left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and dv.iNamLamViec = @NamLamViec
			group by dv.sTenDonVi, pbct.iID_MaDonVi) pb
			left join
			(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, (isnull(sum(pb.fBHYT_NLD), 0) + isnull(sum(pb.fBHYT_NSD), 0)) DttDauNam, pb.iID_MaDonVi
			from BH_DM_MucLucNganSach mlns
			left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NLD, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
			BH_DTT_BHXH_PhanBo_ChungTu pb 
			join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
			where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
			and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
			) pb on mlns.sXauNoiMa = pb.sXauNoiMa
			where 
			mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
			and mlns.sXauNoiMa in (
			'9020002-010-011-0001-0000',  -- Sĩ quan
			'9020002-010-011-0001-0001'	, -- QNCN
			'9020002-010-011-0001-0002')  -- HSQ, BS
			and mlns.iTrangThai = 1
			group by mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.iID_MaDonVi
			) dt on pb.iID_MaDonVi = dt.iID_MaDonVi

	union all

	select
	23 STT,
	N'23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	0 BHangCha
	from (select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, isnull(sum(pb.fBHYT_NLD), 0) fBHYT_NLD--, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.sXauNoiMa in
		(
		'9020002-010-011-0002-0000',  -- CC,CN, VCQP
		'9020002-010-011-0002-0001'  -- LĐHĐ
		)
		and mlns.iTrangThai = 1
		group by mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu
		) pbct
		left join (
		select sXauNoiMa, isnull(sum(fThuBHYT_NLD_QTDauNam), 0) fThuBHYT_NLD_QTDauNam, isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) fThuBHYT_NLD_QTCuoiNam 
		from BH_DTT_BHXH_DieuChinh_ChiTiet 
		where iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and iNamLamViec = @NamLamViec
		group by sXauNoiMa
			) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa

	union all

	select pb.STT, pb.MaSo, pb.NoiDung, dt.DttDauNam, pb.Dtt6ThangDauNam, pb.Dtt6ThangCuoiNam, pb.TongCong, pb.Tang, pb.Giam, pb.Khoi, pb.Thu, pb.Type, pb.BHangCha from
	(select
		23 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha,
		pbct.iID_MaDonVi
	from (select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHYT_NLD, pb.fBHYT_NSD, pb.iID_MaDonVi
			from BH_DM_MucLucNganSach mlns
			cross join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NLD, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
			BH_DTT_BHXH_PhanBo_ChungTu pb 
			join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
			where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
			and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
			) pb --on mlns.sXauNoiMa = pb.sXauNoiMa
			where 
			mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
			and mlns.sXauNoiMa in (
			'9020002-010-011-0002-0000',  -- CC,CN, VCQP
			'9020002-010-011-0002-0001'  -- LĐHĐ
			)
			and mlns.iTrangThai = 1
			) pbct
			left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi 
				and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec
			left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and dv.iNamLamViec = @NamLamViec
			group by dv.sTenDonVi, pbct.iID_MaDonVi) pb
			left join
			(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, isnull(sum(pb.fBHYT_NLD), 0) DttDauNam, pb.iID_MaDonVi
			from BH_DM_MucLucNganSach mlns
			left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NLD, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
			BH_DTT_BHXH_PhanBo_ChungTu pb 
			join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
			where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
			and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
			) pb on mlns.sXauNoiMa = pb.sXauNoiMa
			where 
			mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
			and mlns.sXauNoiMa in (
			'9020002-010-011-0002-0000',  -- CC,CN, VCQP
			'9020002-010-011-0002-0001'  -- LĐHĐ
			)
			and mlns.iTrangThai = 1
			group by mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.iID_MaDonVi
			) dt on pb.iID_MaDonVi = dt.iID_MaDonVi

	union all

	select
	24 STT,
	N'24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	0 BHangCha
	from (select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, isnull(sum(pb.fBHYT_NSD), 0) fBHYT_NSD--, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.sXauNoiMa in
			(
			'9020002-010-011-0002-0000',  -- CC,CN, VCQP
			'9020002-010-011-0002-0001'  -- LĐHĐ
			)
		and mlns.iTrangThai = 1
		group by mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu
		) pbct
		left join (
		select sXauNoiMa, isnull(sum(fThuBHYT_NSD_QTDauNam), 0) fThuBHYT_NSD_QTDauNam, isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) fThuBHYT_NSD_QTCuoiNam 
		from BH_DTT_BHXH_DieuChinh_ChiTiet 
		where iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and iNamLamViec = @NamLamViec
		group by sXauNoiMa
			) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa

	union all

	select pb.STT, pb.MaSo, pb.NoiDung, dt.DttDauNam, pb.Dtt6ThangDauNam, pb.Dtt6ThangCuoiNam, pb.TongCong, pb.Tang, pb.Giam, pb.Khoi, pb.Thu, pb.Type, pb.BHangCha from
	(select
		24 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha,
		pbct.iID_MaDonVi
	from (select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHYT_NSD, pb.iID_MaDonVi
			from BH_DM_MucLucNganSach mlns
			cross join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
			BH_DTT_BHXH_PhanBo_ChungTu pb 
			join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
			where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
			and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
			) pb --on mlns.sXauNoiMa = pb.sXauNoiMa
			where 
			mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
			and mlns.sXauNoiMa in (
			'9020002-010-011-0002-0000',  -- CC,CN, VCQP
			'9020002-010-011-0002-0001'  -- LĐHĐ
			)
			and mlns.iTrangThai = 1
			) pbct
			left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi 
				and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec
			left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and dv.iNamLamViec = @NamLamViec
			group by dv.sTenDonVi, pbct.iID_MaDonVi) pb
			left join
			(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, isnull(sum(pb.fBHYT_NSD), 0) DttDauNam, pb.iID_MaDonVi
			from BH_DM_MucLucNganSach mlns
			left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
			BH_DTT_BHXH_PhanBo_ChungTu pb 
			join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
			where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
			and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
			) pb on mlns.sXauNoiMa = pb.sXauNoiMa
			where 
			mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
			and mlns.sXauNoiMa in (
			'9020002-010-011-0002-0000',  -- CC,CN, VCQP
			'9020002-010-011-0002-0001'  -- LĐHĐ
			)
			and mlns.iTrangThai = 1
			group by mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.iID_MaDonVi
			) dt on pb.iID_MaDonVi = dt.iID_MaDonVi

	--BHTN
	--Lấy dữ liệu khối dự toán
	union all

	select
	29 STT,
	N'29' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	0 BHangCha
	from (select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, isnull(sum(pb.fBHTN_NLD), 0) fBHTN_NLD--, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		group by mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu
		) pbct
		left join (
		select sXauNoiMa, isnull(sum(fThuBHTN_NLD_QTDauNam), 0) fThuBHTN_NLD_QTDauNam, isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) fThuBHTN_NLD_QTCuoiNam 
		from BH_DTT_BHXH_DieuChinh_ChiTiet 
		where iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and iNamLamViec = @NamLamViec
		group by sXauNoiMa
			) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa

	union all

	select pb.STT, pb.MaSo, pb.NoiDung, dt.fBHTN_NLD DttDauNam, pb.Dtt6ThangDauNam, pb.Dtt6ThangCuoiNam, pb.TongCong, pb.Tang, pb.Giam, pb.Khoi, pb.Thu, pb.Type, pb.BHangCha from
	(select
		29 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha,
		pbct.iID_MaDonVi
	from (select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NLD, pb.iID_MaDonVi
			from BH_DM_MucLucNganSach mlns
			cross join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
			BH_DTT_BHXH_PhanBo_ChungTu pb 
			join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
			where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
			and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
			) pb --on mlns.sXauNoiMa = pb.sXauNoiMa
			where 
			mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
			and mlns.iTrangThai = 1
			) pbct
			left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi 
				and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec
			left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and dv.iNamLamViec = @NamLamViec
			group by dv.sTenDonVi, pbct.iID_MaDonVi) pb
			left join
			(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NLD, pb.iID_MaDonVi
			from BH_DM_MucLucNganSach mlns
			left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
			BH_DTT_BHXH_PhanBo_ChungTu pb 
			join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
			where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
			and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
			) pb on mlns.sXauNoiMa = pb.sXauNoiMa
			where 
			mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
			and mlns.iTrangThai = 1
			) dt on pb.iID_MaDonVi = dt.iID_MaDonVi

	union all

	select
	30 STT,
	N'30' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	0 BHangCha
	from (select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, isnull(sum(pb.fBHTN_NSD), 0) fBHTN_NSD--, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		group by mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu
		) pbct
		left join (
		select sXauNoiMa, isnull(sum(fThuBHTN_NSD_QTDauNam), 0) fThuBHTN_NSD_QTDauNam, isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) fThuBHTN_NSD_QTCuoiNam 
		from BH_DTT_BHXH_DieuChinh_ChiTiet 
		where iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and iNamLamViec = @NamLamViec
		group by sXauNoiMa
			) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa

	union all

	select pb.STT, pb.MaSo, pb.NoiDung, dt.fBHTN_NSD BhxhNsdDauNam, pb.BhxhNsd6ThangDauNam, pb.BhxhNsd6ThangCuoiNam, pb.TongCong, pb.Tang, pb.Giam, pb.Khoi, pb.Thu, pb.Type, pb.BHangCha from
	(select
		30 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha,
		pbct.iID_MaDonVi
	from (select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NSD, pb.iID_MaDonVi
			from BH_DM_MucLucNganSach mlns
			cross join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
			BH_DTT_BHXH_PhanBo_ChungTu pb 
			join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
			where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
			and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
			) pb --on mlns.sXauNoiMa = pb.sXauNoiMa
			where 
			mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
			and mlns.iTrangThai = 1
			) pbct
			left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi 
				and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec
			left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and dv.iNamLamViec = @NamLamViec
			group by dv.sTenDonVi, pbct.iID_MaDonVi) pb
			left join
			(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NSD, pb.iID_MaDonVi
			from BH_DM_MucLucNganSach mlns
			left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
			BH_DTT_BHXH_PhanBo_ChungTu pb 
			join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
			where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
			and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
			) pb on mlns.sXauNoiMa = pb.sXauNoiMa
			where 
			mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
			and mlns.iTrangThai = 1
			) dt on pb.iID_MaDonVi = dt.iID_MaDonVi

	--Lấy dữ liệu khối hạch toán
	union all

	select
	32 STT,
	N'32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	0 BHangCha
	from (select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, isnull(sum(pb.fBHTN_NLD), 0) fBHTN_NLD--, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		group by mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu
		) pbct
		left join (
		select sXauNoiMa, isnull(sum(fThuBHTN_NLD_QTDauNam), 0) fThuBHTN_NLD_QTDauNam, isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) fThuBHTN_NLD_QTCuoiNam 
		from BH_DTT_BHXH_DieuChinh_ChiTiet 
		where iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and iNamLamViec = @NamLamViec
		group by sXauNoiMa
			) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa

	union all

	select pb.STT, pb.MaSo, pb.NoiDung, dt.fBHTN_NLD DttDauNam, pb.Dtt6ThangDauNam, pb.Dtt6ThangCuoiNam, pb.TongCong, pb.Tang, pb.Giam, pb.Khoi, pb.Thu, pb.Type, pb.BHangCha from
	(select
		32 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha,
		pbct.iID_MaDonVi
	from (select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NLD, pb.iID_MaDonVi
			from BH_DM_MucLucNganSach mlns
			cross join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
			BH_DTT_BHXH_PhanBo_ChungTu pb 
			join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
			where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
			and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
			) pb --on mlns.sXauNoiMa = pb.sXauNoiMa
			where 
			mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
			and mlns.iTrangThai = 1
			) pbct
			left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi 
				and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec
			left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and dv.iNamLamViec = @NamLamViec
			group by dv.sTenDonVi, pbct.iID_MaDonVi) pb
			left join
			(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NLD, pb.iID_MaDonVi
			from BH_DM_MucLucNganSach mlns
			left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
			BH_DTT_BHXH_PhanBo_ChungTu pb 
			join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
			where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
			and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
			) pb on mlns.sXauNoiMa = pb.sXauNoiMa
			where 
			mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
			and mlns.iTrangThai = 1
			) dt on pb.iID_MaDonVi = dt.iID_MaDonVi

	union all

	select
	33 STT,
	N'33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	0 BHangCha
	from (select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, isnull(sum(pb.fBHTN_NSD), 0) fBHTN_NSD--, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
		BH_DTT_BHXH_PhanBo_ChungTu pb 
		join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
		where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
		and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		group by mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu
		) pbct
		left join (
		select sXauNoiMa, isnull(sum(fThuBHTN_NSD_QTDauNam), 0) fThuBHTN_NSD_QTDauNam, isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) fThuBHTN_NSD_QTCuoiNam 
		from BH_DTT_BHXH_DieuChinh_ChiTiet 
		where iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and iNamLamViec = @NamLamViec
		group by sXauNoiMa
			) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa

	union all

	select pb.STT, pb.MaSo, pb.NoiDung, dt.fBHTN_NSD BhxhNsdDauNam, pb.Dtt6ThangDauNam, pb.Dtt6ThangCuoiNam, pb.TongCong, pb.Tang, pb.Giam, pb.Khoi, pb.Thu, pb.Type, pb.BHangCha from
	(select
		33 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha,
		pbct.iID_MaDonVi
	from (select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NSD, pb.iID_MaDonVi
			from BH_DM_MucLucNganSach mlns
			cross join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
			BH_DTT_BHXH_PhanBo_ChungTu pb 
			join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
			where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
			and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
			) pb --on mlns.sXauNoiMa = pb.sXauNoiMa
			where 
			mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
			and mlns.iTrangThai = 1
			) pbct
			left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi 
				and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec
			left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and dv.iNamLamViec = @NamLamViec
			group by dv.sTenDonVi, pbct.iID_MaDonVi) pb
			left join
			(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NSD, pb.iID_MaDonVi
			from BH_DM_MucLucNganSach mlns
			left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
			BH_DTT_BHXH_PhanBo_ChungTu pb 
			join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
			where pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi)) and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
			and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
			) pb on mlns.sXauNoiMa = pb.sXauNoiMa
			where 
			mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
			and mlns.iTrangThai = 1
			) dt on pb.iID_MaDonVi = dt.iID_MaDonVi
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
	and MaSo in (5, 8)
	union all
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
	and MaSo in (6, 9)
	union all
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
	and MaSo in (5, 6)
	union all
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
	and MaSo in (8, 9)
	--BHYT
	union all
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
	and MaSo in (16, 21)

	union all
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
	and MaSo in (18, 23)

	union all
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
	and MaSo in (19, 24)

	union all
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
	and MaSo in (18, 19)

	union all
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
	and MaSo in (23, 24)
	--BHTN
	union all
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
	and MaSo in (29, 32)

	union all
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
	and MaSo in (30, 33)

	union all
	select
	28 STT,
	N'28=29+30' MaSo,
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
	and MaSo in (29, 30)

	union all
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
	and MaSo in (32, 33)
	) child_ml

	select tmp.* into temp1
	from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child
	union all
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
	and MaSo in ('4=5+6', '7=8+9')
	union all
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
	and MaSo in ('28=29+30', '31=32+33')

	union all
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
	union all
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
	where STT in (16, 17) and MaSo in ('16', '17=18+19')
	union all
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
	where STT in (21, 22) and MaSo in ('21', '22=23+24')
	) parent

	update tbl_parent 
	set DttDauNam = isnull(DttDauNam, 0) + isnull((select DttDauNam from tbl_child where STT = 16 and MaSo = 16), 0),
		Dtt6ThangDauNam = isnull(Dtt6ThangDauNam, 0) + isnull((select Dtt6ThangDauNam from tbl_child where STT = 16 and MaSo = 16), 0),
		Dtt6ThangCuoiNam = isnull(Dtt6ThangCuoiNam, 0) + isnull((select Dtt6ThangCuoiNam from tbl_child where STT = 16 and MaSo = 16), 0),
		TongCong = isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0),
		Tang = (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0)) - isnull(DttDauNam, 0),
		Giam = isnull(DttDauNam, 0) - (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0))
	where STT = 15 and MaSo = '15=16+17'

	update tbl_parent 
	set TongCong = isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0),
		Tang = (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0)) - isnull(DttDauNam, 0),
		Giam = isnull(DttDauNam, 0) - (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0))
	where STT = 15 and MaSo = '15=16+17'

	update tbl_parent 
	set DttDauNam = isnull(DttDauNam, 0) + isnull((select DttDauNam from tbl_child where STT = 21 and MaSo = 21), 0),
		Dtt6ThangDauNam = isnull(Dtt6ThangDauNam, 0) + isnull((select Dtt6ThangDauNam from tbl_child where STT = 21 and MaSo = 21), 0),
		Dtt6ThangCuoiNam = isnull(Dtt6ThangCuoiNam, 0) + isnull((select Dtt6ThangCuoiNam from tbl_child where STT = 21 and MaSo = 21), 0),
		TongCong = isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0),
		Tang = (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0)) - isnull(DttDauNam, 0),
		Giam = isnull(DttDauNam, 0) - (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0))
	where STT = 20 and MaSo = '20=21+22'

	update tbl_parent 
	set TongCong = isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0),
		Tang = (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0)) - isnull(DttDauNam, 0),
		Giam = isnull(DttDauNam, 0) - (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0))
	where STT = 20 and MaSo = '20=21+22'

	--------------------------------------------

	select result.* into tbl_ddt_bhxh from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from temp1
	union all
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_parent
	union all
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
	) result;

	select STT, MaSo, NoiDung, DttDauNam/@DonViTinh DttDauNam, Dtt6ThangDauNam/@DonViTinh Dtt6ThangDauNam, Dtt6ThangCuoiNam/@DonViTinh Dtt6ThangCuoiNam, TongCong/@DonViTinh TongCong, Tang/@DonViTinh Tang, Giam/@DonViTinh Giam, Khoi, Thu, Type, BHangCha 
	from tbl_ddt_bhxh
	order by STT

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]    Script Date: 7/15/2024 4:51:33 PM ******/
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

	declare @dNgayDieuChinh Datetime = (select top 1 dNgayChungTu from BH_DTT_BHXH_DieuChinh where iNamLamViec = @NamLamViec and iID_MaDonVi = @MaDonVi order by dNgayChungTu desc);

	--BHXH
	--Lấy dữ liệu NLĐ, NSD của khối dự toán
	select child.* into tbl_child from
	(
	select
	5 STT,
	N'5' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	0 BHangCha
	from 
	(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NLD, pb.iID_MaDonVi
	from BH_DM_MucLucNganSach mlns
	left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
	BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
	and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	) pb on mlns.sXauNoiMa = pb.sXauNoiMa
	where 
	mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
	and mlns.iTrangThai = 1
	) pbct
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec 

	union

	select
	6 STT,
	N'6' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NSD' Type,
	0 BHangCha
	from 
	(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NSD, pb.iID_MaDonVi
	from BH_DM_MucLucNganSach mlns
	left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
	BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
	and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	) pb on mlns.sXauNoiMa = pb.sXauNoiMa
	where 
	mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
	and mlns.iTrangThai = 1
	) pbct
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec 

	--Lấy dữ liệu NLĐ, NSD khối hạch toán
	union

	select
	8 STT,
	N'8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	0 BHangCha
	from 
	(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NLD, pb.iID_MaDonVi
	from BH_DM_MucLucNganSach mlns
	left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
	BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
	and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	) pb on mlns.sXauNoiMa = pb.sXauNoiMa
	where 
	mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
	and mlns.iTrangThai = 1
	) pbct
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union

	select
	9 STT,
	N'9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	N'HT' Khoi,
	N'BHXH' Thu,
	'NSD' Type,
	0 BHangCha
	from 
	(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHXH_NSD, pb.iID_MaDonVi
	from BH_DM_MucLucNganSach mlns
	left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHXH_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
	BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
	and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	) pb on mlns.sXauNoiMa = pb.sXauNoiMa
	where 
	mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
	and mlns.iTrangThai = 1
	) pbct
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec 
	-----------------------------------------
	--BHYT
	-- Khối dự toán
	union

	select
	16 STT,
	N'16' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) DttDauNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0) - isnull(sum(pbct.fBHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) - (isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from 
	(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHYT_NLD, pb.fBHYT_NSD, pb.iID_MaDonVi
	from BH_DM_MucLucNganSach mlns
	left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NLD, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
	BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
	and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	) pb on mlns.sXauNoiMa = pb.sXauNoiMa
	where 
	mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
	and mlns.sXauNoiMa in
		('9020001-010-011-0001-0000',  -- Sĩ quan
		'9020001-010-011-0001-0001'	, -- QNCN
		'9020001-010-011-0001-0002'  -- HSQ, BS
		)
	and mlns.iTrangThai = 1
	) pbct
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec 

	union

	select
	18 STT,
	N'18' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	0 BHangCha
	from 
	(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHYT_NLD, pb.fBHYT_NSD, pb.iID_MaDonVi
	from BH_DM_MucLucNganSach mlns
	left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NLD, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
	BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
	and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	) pb on mlns.sXauNoiMa = pb.sXauNoiMa
	where 
	mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
	and mlns.sXauNoiMa in
		('9020001-010-011-0002-0000',  -- CC,CN, VCQP
		'9020001-010-011-0002-0001'  -- LĐHĐ
		)
	and mlns.iTrangThai = 1
	) pbct
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec 

	union

	select
	19 STT,
	N'19' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	0 BHangCha
	from 
	(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHYT_NLD, pb.fBHYT_NSD, pb.iID_MaDonVi
	from BH_DM_MucLucNganSach mlns
	left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NLD, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
	BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
	and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	) pb on mlns.sXauNoiMa = pb.sXauNoiMa
	where 
	mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
	and mlns.sXauNoiMa in
		('9020001-010-011-0002-0000',  -- CC,CN, VCQP
		'9020001-010-011-0002-0001'  -- LĐHĐ
		)
	and mlns.iTrangThai = 1
	) pbct
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec 

	-- Khối hạch toán
	union

	select
	21 STT,
	N'21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) DttDauNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0) - isnull(sum(pbct.fBHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) - (isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from 
	(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHYT_NLD, pb.fBHYT_NSD, pb.iID_MaDonVi
	from BH_DM_MucLucNganSach mlns
	left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NLD, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
	BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
	and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	) pb on mlns.sXauNoiMa = pb.sXauNoiMa
	where 
	mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
	and mlns.sXauNoiMa in
		('9020002-010-011-0001-0000',  -- Sĩ quan
		'9020002-010-011-0001-0001'	, -- QNCN
		'9020002-010-011-0001-0002'  -- HSQ, BS
		)
	and mlns.iTrangThai = 1
	) pbct
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union

	select
	23 STT,
	N'23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	0 BHangCha
	from 
	(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHYT_NLD, pb.fBHYT_NSD, pb.iID_MaDonVi
	from BH_DM_MucLucNganSach mlns
	left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NLD, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
	BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
	and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	) pb on mlns.sXauNoiMa = pb.sXauNoiMa
	where 
	mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
	and mlns.sXauNoiMa in
		('9020002-010-011-0002-0000',  -- CC,CN, VCQP
		'9020002-010-011-0002-0001'  -- LĐHĐ
		)
	and mlns.iTrangThai = 1
	) pbct
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union

	select
	24 STT,
	N'24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	0 BHangCha
	from 
	(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHYT_NLD, pb.fBHYT_NSD, pb.iID_MaDonVi
	from BH_DM_MucLucNganSach mlns
	left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHYT_NLD, pbct.fBHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
	BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
	and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	) pb on mlns.sXauNoiMa = pb.sXauNoiMa
	where 
	mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
	and mlns.sXauNoiMa in
		('9020002-010-011-0002-0000',  -- CC,CN, VCQP
		'9020002-010-011-0002-0001'  -- LĐHĐ
		)
	and mlns.iTrangThai = 1
	) pbct
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	--BHTN
	--Lấy dữ liệu khối dự toán
	union

	select
	29 STT,
	N'29' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	0 BHangCha
	from 
	(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NLD, pb.iID_MaDonVi
	from BH_DM_MucLucNganSach mlns
	left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
	BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
	and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	) pb on mlns.sXauNoiMa = pb.sXauNoiMa
	where 
	mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
	and mlns.iTrangThai = 1
	) pbct
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union

	select
	30 STT,
	N'30' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	0 BHangCha
	from 
	(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NSD, pb.iID_MaDonVi
	from BH_DM_MucLucNganSach mlns
	left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
	BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
	and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	) pb on mlns.sXauNoiMa = pb.sXauNoiMa
	where 
	mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
	and mlns.iTrangThai = 1
	) pbct
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	--Lấy dữ liệu khối hạch toán
	union

	select
	32 STT,
	N'32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	0 BHangCha
	from 
	(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NLD, pb.iID_MaDonVi
	from BH_DM_MucLucNganSach mlns
	left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
	BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
	and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	) pb on mlns.sXauNoiMa = pb.sXauNoiMa
	where 
	mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
	and mlns.iTrangThai = 1
	) pbct
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec

	union

	select
	33 STT,
	N'33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	0 BHangCha
	from 
	(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pb.fBHTN_NSD, pb.iID_MaDonVi
	from BH_DM_MucLucNganSach mlns
	left join (select pb.sSoQuyetDinh, pb.bKhoa, pb.DngayChungTu, pbct.fBHTN_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa from
	BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bKhoa = 1
	and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	) pb on mlns.sXauNoiMa = pb.sXauNoiMa
	where 
	mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
	and mlns.iTrangThai = 1
	) pbct
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = @MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = @NamLamViec
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
	1 BHangCha
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
	1 BHangCha
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
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'DT'

	union

	select
	7 STT,
	N'7=8+9' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	1 BHangCha
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
	N'28=29+30' MaSo,
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
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'DT'

	union

	select
	31 STT,
	N'31=32+33' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	1 BHangCha
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
	1 BHangCha
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
	1 BHangCha
	from tbl_child_cat
	where STT in (21, 22)
	) parent

	update tbl_parent 
	set DttDauNam = isnull(DttDauNam, 0) + isnull((select DttDauNam from tbl_child where STT in (16)), 0),
		Dtt6ThangDauNam = isnull(Dtt6ThangDauNam, 0) + isnull((select Dtt6ThangDauNam from tbl_child where STT in (16)), 0),
		Dtt6ThangCuoiNam = isnull(Dtt6ThangCuoiNam, 0) + isnull((select Dtt6ThangCuoiNam from tbl_child where STT in (16)), 0),
		TongCong = isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0),
		Tang = (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0)) - isnull(DttDauNam, 0),
		Giam = isnull(DttDauNam, 0) - (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0))
	where STT = 15

	update tbl_parent 
	set TongCong = isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0),
		Tang = (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0)) - isnull(DttDauNam, 0),
		Giam = isnull(DttDauNam, 0) - (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0))
	where STT = 15

	update tbl_parent 
	set DttDauNam = isnull(DttDauNam, 0) + isnull((select DttDauNam from tbl_child where STT in (21)), 0),
		Dtt6ThangDauNam = isnull(Dtt6ThangDauNam, 0) + isnull((select Dtt6ThangDauNam from tbl_child where STT in (21)), 0),
		Dtt6ThangCuoiNam = isnull(Dtt6ThangCuoiNam, 0) + isnull((select Dtt6ThangCuoiNam from tbl_child where STT in (21)), 0),
		TongCong = isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0),
		Tang = (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0)) - isnull(DttDauNam, 0),
		Giam = isnull(DttDauNam, 0) - (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0))
	where STT = 20

	update tbl_parent 
	set TongCong = isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0),
		Tang = (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0)) - isnull(DttDauNam, 0),
		Giam = isnull(DttDauNam, 0) - (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0))
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

	--IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	--IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	--IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	--IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	--IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_tonghop]    Script Date: 7/15/2024 4:51:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_tonghop]
	@MaDonVi NVARCHAR(255),
	@DonViTinh int,
	@NamLamViec int,
	@LstMaDonVi NVARCHAR(MAX)
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

	declare @dNgayDieuChinh Datetime = (select top 1 dNgayChungTu from BH_DTT_BHXH_DieuChinh where iNamLamViec = @NamLamViec and iID_MaDonVi = @MaDonVi order by dNgayChungTu desc);

	--BHXH
	--Lấy dữ liệu NLĐ, NSD của khối dự toán
	select child.* into tbl_child from
	(
	select
		5 STT,
		N'5' MaSo,
		N'- Người lao động đóng' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHXH_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHXH_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join 
		(select ctct.*
			from
			BH_DTT_BHXH_DieuChinh ct join
			BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
			where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
			and ct.iNamLamViec = @NamLamViec
		) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa

	union

	select
	6 STT,
	N'6' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NSD' Type,
	0 BHangCha
	from 
	(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHXH_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHXH_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join 
		(select ctct.*
			from
			BH_DTT_BHXH_DieuChinh ct join
			BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
			where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
			and ct.iNamLamViec = @NamLamViec
		) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa

	--Lấy dữ liệu NLĐ, NSD khối hạch toán
	union

	select
	8 STT,
	N'8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHXH_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHXH_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join 
		(select ctct.*
			from
			BH_DTT_BHXH_DieuChinh ct join
			BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
			where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
			and ct.iNamLamViec = @NamLamViec
		) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa

	union

	select
	9 STT,
	N'9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	N'HT' Khoi,
	N'BHXH' Thu,
	'NSD' Type,
	0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHXH_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHXH_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join 
		(select ctct.*
			from
			BH_DTT_BHXH_DieuChinh ct join
			BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
			where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
			and ct.iNamLamViec = @NamLamViec
		) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa
	-----------------------------------------
	--BHYT
	-- Khối dự toán
	union
	select
	16 STT,
	N'16' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fThu_BHYT_NLD), 0) + isnull(sum(pbct.fThu_BHYT_NSD), 0)) DttDauNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0) - isnull(sum(pbct.fThu_BHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fThu_BHYT_NLD), 0) + isnull(sum(pbct.fThu_BHYT_NSD), 0)) - (isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHYT_NLD, pb.fThu_BHYT_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHYT_NLD, pbct.fThu_BHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.sXauNoiMa in
		(
		'9020001-010-011-0001-0000',  -- Sĩ quan
		'9020001-010-011-0001-0001'	, -- QNCN
		'9020001-010-011-0001-0002'  -- HSQ, BS
		)
		and mlns.iTrangThai = 1
		) pbct
		left join 
		(select ctct.*
			from
			BH_DTT_BHXH_DieuChinh ct join
			BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
			where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
			and ct.iNamLamViec = @NamLamViec
		) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa

	union

	select
	18 STT,
	N'18' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHYT_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHYT_NLD), 0) - (isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHYT_NLD, pb.fThu_BHYT_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHYT_NLD, pbct.fThu_BHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.sXauNoiMa in
		(
		'9020001-010-011-0002-0000',  -- CC,CN, VCQP
		'9020001-010-011-0002-0001'  -- LĐHĐ
		)
		and mlns.iTrangThai = 1
		) pbct
		left join 
		(select ctct.*
			from
			BH_DTT_BHXH_DieuChinh ct join
			BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
			where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
			and ct.iNamLamViec = @NamLamViec
		) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa

	union

	select
	19 STT,
	N'19' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHYT_NSD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHYT_NSD), 0) - (isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHYT_NLD, pb.fThu_BHYT_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHYT_NLD, pbct.fThu_BHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.sXauNoiMa in
		(
		'9020001-010-011-0002-0000',  -- CC,CN, VCQP
		'9020001-010-011-0002-0001'  -- LĐHĐ
		)
		and mlns.iTrangThai = 1
		) pbct
		left join 
		(select ctct.*
			from
			BH_DTT_BHXH_DieuChinh ct join
			BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
			where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
			and ct.iNamLamViec = @NamLamViec
		) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa

	-- Khối hạch toán
	union

	select
	21 STT,
	N'21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fThu_BHYT_NLD), 0) + isnull(sum(pbct.fThu_BHYT_NSD), 0)) DttDauNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0) - isnull(sum(pbct.fThu_BHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fThu_BHYT_NLD), 0) + isnull(sum(pbct.fThu_BHYT_NSD), 0)) - (isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHYT_NLD, pb.fThu_BHYT_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHYT_NLD, pbct.fThu_BHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.sXauNoiMa in
		(
		'9020002-010-011-0001-0000',  -- Sĩ quan
		'9020002-010-011-0001-0001'	, -- QNCN
		'9020002-010-011-0001-0002'  -- HSQ, BS
		)
		and mlns.iTrangThai = 1
		) pbct
		left join 
		(select ctct.*
			from
			BH_DTT_BHXH_DieuChinh ct join
			BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
			where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
			and ct.iNamLamViec = @NamLamViec
		) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa

	union

	select
	23 STT,
	N'23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHYT_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHYT_NLD), 0) - (isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHYT_NLD, pb.fThu_BHYT_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHYT_NLD, pbct.fThu_BHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.sXauNoiMa in
		(
		'9020002-010-011-0002-0000',  -- CC,CN, VCQP
		'9020002-010-011-0002-0001'  -- LĐHĐ
		)
		and mlns.iTrangThai = 1
		) pbct
		left join 
		(select ctct.*
			from
			BH_DTT_BHXH_DieuChinh ct join
			BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
			where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
			and ct.iNamLamViec = @NamLamViec
		) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa

	union

	select
	24 STT,
	N'24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHYT_NSD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHYT_NSD), 0) - (isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHYT_NLD, pb.fThu_BHYT_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHYT_NLD, pbct.fThu_BHYT_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.sXauNoiMa in
		(
		'9020002-010-011-0002-0000',  -- CC,CN, VCQP
		'9020002-010-011-0002-0001'  -- LĐHĐ
		)
		and mlns.iTrangThai = 1
		) pbct
		left join 
		(select ctct.*
			from
			BH_DTT_BHXH_DieuChinh ct join
			BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
			where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
			and ct.iNamLamViec = @NamLamViec
		) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa

	--BHTN
	--Lấy dữ liệu khối dự toán
	union

	select
	29 STT,
	N'29' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHTN_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHTN_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join 
		(select ctct.*
			from
			BH_DTT_BHXH_DieuChinh ct join
			BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
			where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
			and ct.iNamLamViec = @NamLamViec
		) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa

	union

	select
	30 STT,
	N'30' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHTN_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHTN_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020001' -- Khối dự toán
		and mlns.iTrangThai = 1
		) pbct
		left join 
		(select ctct.*
			from
			BH_DTT_BHXH_DieuChinh ct join
			BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
			where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
			and ct.iNamLamViec = @NamLamViec
		) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa

	--Lấy dữ liệu khối hạch toán
	union

	select
	32 STT,
	N'32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHTN_NLD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHTN_NLD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join 
		(select ctct.*
			from
			BH_DTT_BHXH_DieuChinh ct join
			BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
			where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
			and ct.iNamLamViec = @NamLamViec
		) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa

	union

	select
	33 STT,
	N'33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	0 BHangCha
	from 
		(select mlns.sLNS, mlns.sXauNoiMa, pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pb.fThu_BHTN_NSD, pb.iID_MaDonVi
		from BH_DM_MucLucNganSach mlns
		left join (select pb.sSoQuyetDinh, pb.bIsKhoa, pb.DngayChungTu, pbct.fThu_BHTN_NSD, pbct.iID_MaDonVi, pbct.sXauNoiMa 
		from BH_DTT_BHXH_ChungTu pb 
		join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
		where pbct.iID_MaDonVi = @MaDonVi and pbct.iNamLamViec = @NamLamViec and pb.bIsKhoa = 1
		and isnull(pb.sSoQuyetDinh, '') <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
		) pb on mlns.sXauNoiMa = pb.sXauNoiMa
		where 
		mlns.iNamLamViec = @NamLamViec and mlns.sLNS = '9020002' -- Khối hạch toán
		and mlns.iTrangThai = 1
		) pbct
		left join 
		(select ctct.*
			from
			BH_DTT_BHXH_DieuChinh ct join
			BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ct.iID_DTT_BHXH_DieuChinh = ctct.iID_DTT_BHXH_DieuChinh
			where ct.iID_MaDonVi in (SELECT * FROM f_split(@LstMaDonVi))
			and ct.iNamLamViec = @NamLamViec
		) ctct on ctct.sXauNoiMa = pbct.sXauNoiMa
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
	1 BHangCha
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
	1 BHangCha
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
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'DT'

	union

	select
	7 STT,
	N'7=8+9' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	1 BHangCha
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
	N'28=29+30' MaSo,
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
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'DT'

	union

	select
	31 STT,
	N'31=32+33' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	1 BHangCha
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
	1 BHangCha
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
	1 BHangCha
	from tbl_child_cat
	where STT in (21, 22)
	) parent

	update tbl_parent 
	set DttDauNam = isnull(DttDauNam, 0) + isnull((select DttDauNam from tbl_child where STT in (16)), 0),
		Dtt6ThangDauNam = isnull(Dtt6ThangDauNam, 0) + isnull((select Dtt6ThangDauNam from tbl_child where STT in (16)), 0),
		Dtt6ThangCuoiNam = isnull(Dtt6ThangCuoiNam, 0) + isnull((select Dtt6ThangCuoiNam from tbl_child where STT in (16)), 0),
		TongCong = isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0),
		Tang = (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0)) - isnull(DttDauNam, 0),
		Giam = isnull(DttDauNam, 0) - (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0))
	where STT = 15

	update tbl_parent 
	set TongCong = isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0),
		Tang = (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0)) - isnull(DttDauNam, 0),
		Giam = isnull(DttDauNam, 0) - (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0))
	where STT = 15

	update tbl_parent 
	set DttDauNam = isnull(DttDauNam, 0) + isnull((select DttDauNam from tbl_child where STT in (21)), 0),
		Dtt6ThangDauNam = isnull(Dtt6ThangDauNam, 0) + isnull((select Dtt6ThangDauNam from tbl_child where STT in (21)), 0),
		Dtt6ThangCuoiNam = isnull(Dtt6ThangCuoiNam, 0) + isnull((select Dtt6ThangCuoiNam from tbl_child where STT in (21)), 0),
		TongCong = isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0),
		Tang = (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0)) - isnull(DttDauNam, 0),
		Giam = isnull(DttDauNam, 0) - (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0))
	where STT = 20

	update tbl_parent 
	set TongCong = isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0),
		Tang = (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0)) - isnull(DttDauNam, 0),
		Giam = isnull(DttDauNam, 0) - (isnull(Dtt6ThangDauNam, 0) + isnull(Dtt6ThangCuoiNam, 0))
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
;
;
;
;
GO


/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_thang]    Script Date: 7/16/2024 5:04:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_quy]    Script Date: 7/16/2024 5:04:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_thang]    Script Date: 7/16/2024 5:04:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_quy]    Script Date: 7/16/2024 5:04:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_thang]    Script Date: 7/16/2024 5:04:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]    Script Date: 7/16/2024 5:04:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_thang]    Script Date: 7/16/2024 5:04:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]    Script Date: 7/16/2024 5:04:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_thang]    Script Date: 7/16/2024 5:04:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]    Script Date: 7/16/2024 5:04:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]    Script Date: 7/16/2024 5:04:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IsTongHop bit,
	@IQuy int,
	@ILoaiQuy int,
	@IsLuyKe bit
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempchungtudonvi]') AND type in (N'U')) drop table tempchungtudonvi;

	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVi;

	-- Ko In luy ke
	if (@IsLuyKe = 0)
	begin
		select
			ct.iID_MaDonVi,
			ct.iNamLamViec
			,ctct.iQSBQNam
			,ctct.fLuongChinh
			,ctct.fPCChucVu
			,ctct.fPCTNNghe
			,ctct.fPCTNVuotKhung
			,ctct.fNghiOm
			,ctct.fHSBL
			,ctct.fTongQTLN
			,ctct.fDuToan
			,ctct.fDaQuyetToan
			,ctct.fConLai
			,ctct.fThu_BHXH_NLD
			,ctct.fThu_BHXH_NSD
			,ctct.fTongSoPhaiThuBHXH
			,ctct.fThu_BHYT_NLD
			,ctct.fThu_BHYT_NSD
			,ctct.fTongSoPhaiThuBHYT
			,ctct.fThu_BHTN_NLD
			,ctct.fThu_BHTN_NSD
			,ctct.fTongSoPhaiThuBHTN
			,ctct.fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
		into tempchungtudonvi
		from
			BH_QTT_BHXH_ChungTu ct
			join
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
		where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.iQuyNamLoai = @ILoaiQuy
			--and ct.iID_MaDonVi = @IdDonVi
			and ((@isCha = 0 and ctct.iID_MaDonVi =@IdDonVi) or (@isCha = 1 and ct.iID_MaDonVi =@IdDonVi))
			and ct.iLoaiTongHop = 1
			--and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
	end
	-- In luy ke
	else
	begin
		select
			ct.iID_MaDonVi,
			ct.iNamLamViec
			,sum(isnull(ctct.iQSBQNam, 0)) iQSBQNam
			,sum(isnull(ctct.fLuongChinh, 0)) fLuongChinh
			,sum(isnull(ctct.fPCChucVu, 0)) fPCChucVu
			,sum(isnull(ctct.fPCTNNghe, 0)) fPCTNNghe
			,sum(isnull(ctct.fPCTNVuotKhung, 0)) fPCTNVuotKhung
			,sum(isnull(ctct.fNghiOm, 0)) fNghiOm
			,sum(isnull(ctct.fHSBL, 0)) fHSBL
			,sum(isnull(ctct.fTongQTLN, 0)) fTongQTLN
			,sum(isnull(ctct.fDuToan, 0)) fDuToan
			,sum(isnull(ctct.fDaQuyetToan, 0)) fDaQuyetToan
			,sum(isnull(ctct.fConLai, 0)) fConLai
			,sum(isnull(ctct.fThu_BHXH_NLD, 0)) fThu_BHXH_NLD
			,sum(isnull(ctct.fThu_BHXH_NSD, 0)) fThu_BHXH_NSD
			,sum(isnull(ctct.fTongSoPhaiThuBHXH, 0)) fTongSoPhaiThuBHXH
			,sum(isnull(ctct.fThu_BHYT_NLD, 0)) fThu_BHYT_NLD
			,sum(isnull(ctct.fThu_BHYT_NSD, 0)) fThu_BHYT_NSD
			,sum(isnull(ctct.fTongSoPhaiThuBHYT, 0)) fTongSoPhaiThuBHYT
			,sum(isnull(ctct.fThu_BHTN_NLD, 0)) fThu_BHTN_NLD
			,sum(isnull(ctct.fThu_BHTN_NSD, 0)) fThu_BHTN_NSD
			,sum(isnull(ctct.fTongSoPhaiThuBHTN, 0)) fTongSoPhaiThuBHTN
			,sum(isnull(ctct.fTongCong, 0)) fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			into tempchungtudonvi
			from
				BH_QTT_BHXH_ChungTu ct
				join
				BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			where ct.iNamLamViec = @NamLamViec
				and ct.iQuyNam <= @IQuy
				and ct.iQuyNamLoai = @ILoaiQuy
				--and ct.iID_MaDonVi = @IdDonVi
				and ((@isCha = 0 and ctct.iID_MaDonVi = @IdDonVi) or (@isCha = 1 and ct.iID_MaDonVi = @IdDonVi))
				and ct.iLoaiTongHop = 1
				--and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
			group by
				ct.iID_MaDonVi,
				ct.iNamLamViec,
				ctct.sGhiChu,
				ctct.iID_MLNS,
				ctct.iID_MLNS_Cha,
				ctct.sXauNoiMa,
				ctct.sLNS
		end

	select
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
		chungtudonvi.fPCChucVu/@Donvitinh fPhuCapChucVu,
		chungtudonvi.fPCTNNghe/@Donvitinh fPCTNNghe,
		chungtudonvi.fPCTNVuotKhung/@Donvitinh fPCTNVuotKhung,
		chungtudonvi.fNghiOm/@Donvitinh fNghiOm,
		chungtudonvi.fHSBL/@Donvitinh fHSBL,
		(chungtudonvi.fLuongChinh + chungtudonvi.fPCChucVu + chungtudonvi.fPCTNNghe + chungtudonvi.fPCTNVuotKhung + chungtudonvi.fNghiOm + chungtudonvi.fHSBL)/@Donvitinh fTongQTLN,
		chungtudonvi.fDuToan/@Donvitinh fDuToan,
		chungtudonvi.fDaQuyetToan/@Donvitinh fDaQuyetToan,
		chungtudonvi.fConLai/@Donvitinh fConLai,
		chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
		chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fTongSoPhaiThuBHXH,
		chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
		chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
		(chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fTongSoPhaiThuBHYT,
		chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
		chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
		(chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongSoPhaiThuBHTN,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fTongNLD,
		(chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongNSD,
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
		left join tempchungtudonvi chungtudonvi 
		on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		order by mlns.sXauNoiMa

		IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempchungtudonvi]') AND type in (N'U')) drop table tempchungtudonvi;
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_thang]    Script Date: 7/16/2024 5:04:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_thang]
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IQuy int,
	@ILoaiQuy int,
	@IsLuyKe bit
AS
BEGIN

	DECLARE @SMonths NVARCHAR(500)
	DECLARE @SLoaiQuy NVARCHAR(500)

	IF (@IQuy = 3) BEGIN SET @SMonths = '1,2,3' END
	ELSE IF (@IQuy = 6 AND @IsLuyKe = 0) BEGIN SET @SMonths = '4,5,6' END
	ELSE IF (@IQuy = 9 AND @IsLuyKe = 0) BEGIN SET @SMonths = '7,8,9' END
	ELSE IF (@IQuy = 12 AND @IsLuyKe = 0) BEGIN SET @SMonths = '10,11,12' END

	ELSE IF (@IQuy = 6 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6' END
	ELSE IF (@IQuy = 9 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6,7,8,9' END
	ELSE IF (@IQuy = 12 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6,7,8,9,10,11,12' END

	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVi;

	select
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
		chungtudonvi.fPCChucVu/@Donvitinh fPhuCapChucVu,
		chungtudonvi.fPCTNNghe/@Donvitinh fPCTNNghe,
		chungtudonvi.fPCTNVuotKhung/@Donvitinh fPCTNVuotKhung,
		chungtudonvi.fNghiOm/@Donvitinh fNghiOm,
		chungtudonvi.fHSBL/@Donvitinh fHSBL,
		(chungtudonvi.fLuongChinh + chungtudonvi.fPCChucVu + chungtudonvi.fPCTNNghe + chungtudonvi.fPCTNVuotKhung + chungtudonvi.fNghiOm + chungtudonvi.fHSBL)/@Donvitinh fTongQTLN,
		chungtudonvi.fDuToan/@Donvitinh fDuToan,
		chungtudonvi.fDaQuyetToan/@Donvitinh fDaQuyetToan,
		chungtudonvi.fConLai/@Donvitinh fConLai,
		chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
		chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fTongSoPhaiThuBHXH,
		chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
		chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
		(chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fTongSoPhaiThuBHYT,
		chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
		chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
		(chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongSoPhaiThuBHTN,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fTongNLD,
		(chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongNSD,
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
			ct.iNamLamViec
			,sum(isnull(ctct.iQSBQNam, 0)) iQSBQNam
			,sum(isnull(ctct.fLuongChinh, 0)) fLuongChinh
			,sum(isnull(ctct.fPCChucVu, 0)) fPCChucVu
			,sum(isnull(ctct.fPCTNNghe, 0)) fPCTNNghe
			,sum(isnull(ctct.fPCTNVuotKhung, 0)) fPCTNVuotKhung
			,sum(isnull(ctct.fNghiOm, 0)) fNghiOm
			,sum(isnull(ctct.fHSBL, 0)) fHSBL
			,sum(isnull(ctct.fTongQTLN, 0)) fTongQTLN
			,sum(isnull(ctct.fDuToan, 0)) fDuToan
			,sum(isnull(ctct.fDaQuyetToan, 0)) fDaQuyetToan
			,sum(isnull(ctct.fConLai, 0)) fConLai
			,sum(isnull(ctct.fThu_BHXH_NLD, 0)) fThu_BHXH_NLD
			,sum(isnull(ctct.fThu_BHXH_NSD, 0)) fThu_BHXH_NSD
			,sum(isnull(ctct.fTongSoPhaiThuBHXH, 0)) fTongSoPhaiThuBHXH
			,sum(isnull(ctct.fThu_BHYT_NLD, 0)) fThu_BHYT_NLD
			,sum(isnull(ctct.fThu_BHYT_NSD, 0)) fThu_BHYT_NSD
			,sum(isnull(ctct.fTongSoPhaiThuBHYT, 0)) fTongSoPhaiThuBHYT
			,sum(isnull(ctct.fThu_BHTN_NLD, 0)) fThu_BHTN_NLD
			,sum(isnull(ctct.fThu_BHTN_NSD, 0)) fThu_BHTN_NSD
			,sum(isnull(ctct.fTongSoPhaiThuBHTN, 0)) fTongSoPhaiThuBHTN
			,sum(isnull(ctct.fTongCong, 0)) fTongCong
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			from
			BH_QTT_BHXH_ChungTu ct
			join
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam in (SELECT * FROM f_split(@SMonths))
			and ct.iQuyNamLoai = @ILoaiQuy
			and ((@isCha = 0 and ctct.iID_MaDonVi =@IdDonVi) or (@isCha = 1 and ct.iID_MaDonVi =@IdDonVi))
			--and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = 1
			group by
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_MLNS,
			ctct.iID_MLNS_Cha,
			ctct.sXauNoiMa,
			ctct.sLNS
			) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		
		order by mlns.sXauNoiMa
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]    Script Date: 7/16/2024 5:04:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]
	@NamLamViec int,
	@IdDonVis nvarchar(100),
	@Donvitinh int,
	@IQuy int,
	@ILoaiQuy int,
	@IsLuyKe bit
AS
BEGIN
	
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempChiTietDonVi]') AND type in (N'U')) drop table tempChiTietDonVi;

	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVis;
	DECLARE @sSoChungTuTH nvarchar(1000)

	--Ko In luy ke
	if (@IsLuyKe = 0)
	begin
		set @sSoChungTuTH = (SELECT TOP 1 sTongHop FROM BH_QTT_BHXH_ChungTu pr where pr.iNamLamViec = @NamLamViec
																		and pr.iQuyNam = @IQuy
																		and pr.iQuyNamLoai = @ILoaiQuy
																		and pr.iID_MaDonVi = @IdDonVis
																		and pr.iLoaiTongHop = 2
																		and pr.bDaTongHop = 0)
	end
	--In luy ke
	else
	begin
	set @sSoChungTuTH = (SELECT TOP 1 sTongHop FROM BH_QTT_BHXH_ChungTu pr where pr.iNamLamViec = @NamLamViec
																		and pr.iQuyNam <= @IQuy
																		and pr.iQuyNamLoai = @ILoaiQuy
																		and pr.iID_MaDonVi = @IdDonVis
																		and pr.iLoaiTongHop = 2
																		and pr.bDaTongHop = 0)
	end

	CREATE TABLE #result(
	iID_MLNS uniqueidentifier,
	iID_MLNS_Cha uniqueidentifier,
	bHangCha bit, 
	sXauNoiMa nvarchar(200), 
	sMoTa nvarchar(200), 
	iNamLamViec int,
	iQSBQNam int,
	fLuongChinh float,
	fPhuCapChucVu float,
	fPCTNNghe float,
	fPCTNVuotKhung float,
	fNghiOm float,
	fHSBL float,
	fTongQTLN float,
	fDuToan float,
	fDaQuyetToan float,
	fConLai float,
	fThu_BHXH_NLD float,
	fThu_BHXH_NSD float,
	fTongSoPhaiThuBHXH float,
	fThu_BHYT_NLD float,
	fThu_BHYT_NSD float,
	fTongSoPhaiThuBHYT float,
	fThu_BHTN_NLD float,
	fThu_BHTN_NSD float,
	fTongSoPhaiThuBHTN float,
	fTongNLD float,
	fTongNSD float,
	fTongCong float,
	MaDonVi nvarchar(50),
	TenDonVi nvarchar(50)
	);

----------------END DETAIL AGENCY----------------
--Ko In luy ke
	if (@IsLuyKe = 0)
	begin
		select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTT_BHXH_ChungTu_ChiTiet
			,ctct.iID_QTT_BHXH_ChungTu
			,ctct.iQSBQNam
			,ctct.fLuongChinh
			,ctct.fPCChucVu
			,ctct.fPCTNNghe
			,ctct.fPCTNVuotKhung
			,ctct.fNghiOm
			,ctct.fHSBL
			,ctct.fTongQTLN
			,ctct.fDuToan
			,ctct.fDaQuyetToan
			,ctct.fConLai
			,ctct.fThu_BHXH_NLD
			,ctct.fThu_BHXH_NSD
			,ctct.fTongSoPhaiThuBHXH
			,ctct.fThu_BHYT_NLD
			,ctct.fThu_BHYT_NSD
			,ctct.fTongSoPhaiThuBHYT
			,ctct.fThu_BHTN_NLD
			,ctct.fThu_BHTN_NSD
			,ctct.fTongSoPhaiThuBHTN
			,ctct.fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			,mlns.sMoTa
			INTO tempChiTietDonVi
			from
			BH_QTT_BHXH_ChungTu ct
			INNER JOIN
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			LEFT JOIN BH_DM_MucLucNganSach mlns ON  mlns.sXauNoiMa = ctct.sXauNoiMa AND mlns.iNamLamViec = @NamLamViec
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.iQuyNamLoai = @ILoaiQuy
			and ct.sSoChungTu in (select * from splitstring(@sSoChungTuTH))
			and ct.iLoaiTongHop = 1
			and ct.bDaTongHop = 1;
	end
	--In luy ke
	else
	begin
		select
			ct.iID_MaDonVi,
			ct.iNamLamViec
			,sum(isnull(ctct.iQSBQNam, 0)) iQSBQNam
			,sum(isnull(ctct.fLuongChinh, 0)) fLuongChinh
			,sum(isnull(ctct.fPCChucVu, 0)) fPCChucVu
			,sum(isnull(ctct.fPCTNNghe, 0)) fPCTNNghe
			,sum(isnull(ctct.fPCTNVuotKhung, 0)) fPCTNVuotKhung
			,sum(isnull(ctct.fNghiOm, 0)) fNghiOm
			,sum(isnull(ctct.fHSBL, 0)) fHSBL
			,sum(isnull(ctct.fTongQTLN, 0)) fTongQTLN
			,sum(isnull(ctct.fDuToan, 0)) fDuToan
			,sum(isnull(ctct.fDaQuyetToan, 0)) fDaQuyetToan
			,sum(isnull(ctct.fConLai, 0)) fConLai
			,sum(isnull(ctct.fThu_BHXH_NLD, 0)) fThu_BHXH_NLD
			,sum(isnull(ctct.fThu_BHXH_NSD, 0)) fThu_BHXH_NSD
			,sum(isnull(ctct.fTongSoPhaiThuBHXH, 0)) fTongSoPhaiThuBHXH
			,sum(isnull(ctct.fThu_BHYT_NLD, 0)) fThu_BHYT_NLD
			,sum(isnull(ctct.fThu_BHYT_NSD, 0)) fThu_BHYT_NSD
			,sum(isnull(ctct.fTongSoPhaiThuBHYT, 0)) fTongSoPhaiThuBHYT
			,sum(isnull(ctct.fThu_BHTN_NLD, 0)) fThu_BHTN_NLD
			,sum(isnull(ctct.fThu_BHTN_NSD, 0)) fThu_BHTN_NSD
			,sum(isnull(ctct.fTongSoPhaiThuBHTN, 0)) fTongSoPhaiThuBHTN
			,sum(isnull(ctct.fTongCong, 0)) fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			,mlns.sMoTa
			INTO tempChiTietDonVi
			from
			BH_QTT_BHXH_ChungTu ct
			INNER JOIN
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			LEFT JOIN BH_DM_MucLucNganSach mlns ON  mlns.sXauNoiMa = ctct.sXauNoiMa AND mlns.iNamLamViec = @NamLamViec
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam <= @IQuy
			and ct.iQuyNamLoai = @ILoaiQuy
			and ct.sSoChungTu in (select * from splitstring(@sSoChungTuTH))
			and ct.iLoaiTongHop = 1
			and ct.bDaTongHop = 1
			group by
				ct.iID_MaDonVi,
				ct.iNamLamViec
				,ctct.sGhiChu
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				,mlns.sMoTa
	end
----------------END DETAIL----------------
----------------INSERT TOTAL----------------
--Ko In luy ke
	if (@IsLuyKe = 0)
	begin
		INSERT INTO #result
		(
		iID_MLNS ,
		iID_MLNS_Cha ,
		bHangCha , 
		sXauNoiMa , 
		sMoTa , 
		iNamLamViec ,
		iQSBQNam ,
		fLuongChinh ,
		fPhuCapChucVu ,
		fPCTNNghe ,
		fPCTNVuotKhung ,
		fNghiOm ,
		fHSBL ,
		fTongQTLN ,
		fDuToan ,
		fDaQuyetToan ,
		fConLai ,
		fThu_BHXH_NLD ,
		fThu_BHXH_NSD ,
		fTongSoPhaiThuBHXH ,
		fThu_BHYT_NLD ,
		fThu_BHYT_NSD ,
		fTongSoPhaiThuBHYT ,
		fThu_BHTN_NLD ,
		fThu_BHTN_NSD ,
		fTongSoPhaiThuBHTN ,
		fTongNLD ,
		fTongNSD ,
		fTongCong ,
		MaDonVi, 
		TenDonVi 
		)
		select
				mlns.iID_MLNS,
				mlns.iID_MLNS_Cha,
				mlns.bHangCha,
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
				(isnull(chungtudonvi.fLuongChinh, 0) + isnull(chungtudonvi.fPCChucVu, 0) + isnull(chungtudonvi.fPCTNNghe, 0) + isnull(chungtudonvi.fPCTNVuotKhung, 0) + isnull(chungtudonvi.fNghiOm, 0) + isnull(chungtudonvi.fHSBL, 0))/@Donvitinh fTongQTLN,
				chungtudonvi.fDuToan,
				chungtudonvi.fDaQuyetToan,
				chungtudonvi.fConLai,
				chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
				chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
				(isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHXH_NSD, 0))/@Donvitinh fTongSoPhaiThuBHXH,
				chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
				chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
				(isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0))/@Donvitinh fTongSoPhaiThuBHYT,
				chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
				chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
				(isnull(chungtudonvi.fThu_BHTN_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0))/@Donvitinh fTongSoPhaiThuBHTN,
				(isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NLD, 0)) fTongNLD,
				(isnull(chungtudonvi.fThu_BHXH_NSD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0)) fTongNSD,
				(isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHXH_NSD, 0) + isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0) + isnull(chungtudonvi.fThu_BHTN_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0))/@Donvitinh fTongCong,
				null,
				null
				FROM
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
					AND iNamLamViec = @NamLamViec) mlns
				LEFT JOIN(
					select
					ct.iID_MaDonVi,
					ct.iNamLamViec,
					ctct.iID_QTT_BHXH_ChungTu_ChiTiet
					,ctct.iID_QTT_BHXH_ChungTu
					,ctct.iQSBQNam
					,ctct.fLuongChinh
					,ctct.fPCChucVu
					,ctct.fPCTNNghe
					,ctct.fPCTNVuotKhung
					,ctct.fNghiOm
					,ctct.fHSBL
					,ctct.fTongQTLN
					,ctct.fDuToan
					,ctct.fDaQuyetToan
					,ctct.fConLai
					,ctct.fThu_BHXH_NLD
					,ctct.fThu_BHXH_NSD
					,ctct.fTongSoPhaiThuBHXH
					,ctct.fThu_BHYT_NLD
					,ctct.fThu_BHYT_NSD
					,ctct.fTongSoPhaiThuBHYT
					,ctct.fThu_BHTN_NLD
					,ctct.fThu_BHTN_NSD
					,ctct.fTongSoPhaiThuBHTN
					,ctct.fTongCong
					,ctct.sGhiChu
					,ctct.iID_MLNS
					,ctct.iID_MLNS_Cha
					,ctct.sXauNoiMa
					,ctct.sLNS
					from
					BH_QTT_BHXH_ChungTu ct
					join
					BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
					where ct.iNamLamViec = @NamLamViec
					and ct.iQuyNam = @IQuy
					and ct.iQuyNamLoai = @ILoaiQuy
					and ((@isCha = 0 and ctct.iID_MaDonVi =@IdDonVis) or (@isCha = 1 and ct.iID_MaDonVi =@IdDonVis))
					--and ct.iID_MaDonVi = @IdDonVis
					and ct.iLoaiTongHop = 2
		--			and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end			
				)chungtudonvi 
					on mlns.iID_MLNS = chungtudonvi.iID_MLNS
				ORDER BY mlns.sXauNoiMa;
		end
		--In luy ke
		else
		begin
		INSERT INTO #result
		(
		iID_MLNS ,
		iID_MLNS_Cha ,
		bHangCha , 
		sXauNoiMa , 
		sMoTa , 
		iNamLamViec ,
		iQSBQNam ,
		fLuongChinh ,
		fPhuCapChucVu ,
		fPCTNNghe ,
		fPCTNVuotKhung ,
		fNghiOm ,
		fHSBL ,
		fTongQTLN ,
		fDuToan ,
		fDaQuyetToan ,
		fConLai ,
		fThu_BHXH_NLD ,
		fThu_BHXH_NSD ,
		fTongSoPhaiThuBHXH ,
		fThu_BHYT_NLD ,
		fThu_BHYT_NSD ,
		fTongSoPhaiThuBHYT ,
		fThu_BHTN_NLD ,
		fThu_BHTN_NSD ,
		fTongSoPhaiThuBHTN ,
		fTongNLD ,
		fTongNSD ,
		fTongCong ,
		MaDonVi, 
		TenDonVi 
		)
		select
				mlns.iID_MLNS,
				mlns.iID_MLNS_Cha,
				mlns.bHangCha,
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
				(isnull(chungtudonvi.fLuongChinh, 0) + isnull(chungtudonvi.fPCChucVu, 0) + isnull(chungtudonvi.fPCTNNghe, 0) + isnull(chungtudonvi.fPCTNVuotKhung, 0) + isnull(chungtudonvi.fNghiOm, 0) + isnull(chungtudonvi.fHSBL, 0))/@Donvitinh fTongQTLN,
				chungtudonvi.fDuToan,
				chungtudonvi.fDaQuyetToan,
				chungtudonvi.fConLai,
				chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
				chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
				(isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHXH_NSD, 0))/@Donvitinh fTongSoPhaiThuBHXH,
				chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
				chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
				(isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0))/@Donvitinh fTongSoPhaiThuBHYT,
				chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
				chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
				(isnull(chungtudonvi.fThu_BHTN_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0))/@Donvitinh fTongSoPhaiThuBHTN,
				(isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NLD, 0)) fTongNLD,
				(isnull(chungtudonvi.fThu_BHXH_NSD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0)) fTongNSD,
				(isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHXH_NSD, 0) + isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0) + isnull(chungtudonvi.fThu_BHTN_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0))/@Donvitinh fTongCong,
				null,
				null
				FROM
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
					AND iNamLamViec = @NamLamViec) mlns
				LEFT JOIN(
					select
					ct.iID_MaDonVi,
					ct.iNamLamViec
					,sum(isnull(ctct.iQSBQNam, 0)) iQSBQNam
					,sum(isnull(ctct.fLuongChinh, 0)) fLuongChinh
					,sum(isnull(ctct.fPCChucVu, 0)) fPCChucVu
					,sum(isnull(ctct.fPCTNNghe, 0)) fPCTNNghe
					,sum(isnull(ctct.fPCTNVuotKhung, 0)) fPCTNVuotKhung
					,sum(isnull(ctct.fNghiOm, 0)) fNghiOm
					,sum(isnull(ctct.fHSBL, 0)) fHSBL
					,sum(isnull(ctct.fTongQTLN, 0)) fTongQTLN
					,sum(isnull(ctct.fDuToan, 0)) fDuToan
					,sum(isnull(ctct.fDaQuyetToan, 0)) fDaQuyetToan
					,sum(isnull(ctct.fConLai, 0)) fConLai
					,sum(isnull(ctct.fThu_BHXH_NLD, 0)) fThu_BHXH_NLD
					,sum(isnull(ctct.fThu_BHXH_NSD, 0)) fThu_BHXH_NSD
					,sum(isnull(ctct.fTongSoPhaiThuBHXH, 0)) fTongSoPhaiThuBHXH
					,sum(isnull(ctct.fThu_BHYT_NLD, 0)) fThu_BHYT_NLD
					,sum(isnull(ctct.fThu_BHYT_NSD, 0)) fThu_BHYT_NSD
					,sum(isnull(ctct.fTongSoPhaiThuBHYT, 0)) fTongSoPhaiThuBHYT
					,sum(isnull(ctct.fThu_BHTN_NLD, 0)) fThu_BHTN_NLD
					,sum(isnull(ctct.fThu_BHTN_NSD, 0)) fThu_BHTN_NSD
					,sum(isnull(ctct.fTongSoPhaiThuBHTN, 0)) fTongSoPhaiThuBHTN
					,sum(isnull(ctct.fTongCong, 0)) fTongCong
					,ctct.sGhiChu
					,ctct.iID_MLNS
					,ctct.iID_MLNS_Cha
					,ctct.sXauNoiMa
					,ctct.sLNS
					from
					BH_QTT_BHXH_ChungTu ct
					join
					BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
					where ct.iNamLamViec = @NamLamViec
					and ct.iQuyNam <= @IQuy
					and ct.iQuyNamLoai = @ILoaiQuy
					and ((@isCha = 0 and ctct.iID_MaDonVi = @IdDonVis) or (@isCha = 1 and ct.iID_MaDonVi = @IdDonVis))
					--and ct.iID_MaDonVi = @IdDonVis
					and ct.iLoaiTongHop = 2
		--			and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end	
					group by 
					ct.iID_MaDonVi,
					ct.iNamLamViec
					,ctct.sGhiChu
					,ctct.iID_MLNS
					,ctct.iID_MLNS_Cha
					,ctct.sXauNoiMa
					,ctct.sLNS
				)chungtudonvi 
					on mlns.iID_MLNS = chungtudonvi.iID_MLNS
				ORDER BY mlns.sXauNoiMa;
		end

----------------END INSERT DETAIL----------------
----------------INSERT DETAIL----------------
	INSERT INTO #result
	(
	iID_MLNS ,
	iID_MLNS_Cha ,
	bHangCha , 
	sXauNoiMa , 
	sMoTa , 
	iNamLamViec ,
	iQSBQNam ,
	fLuongChinh ,
	fPhuCapChucVu ,
	fPCTNNghe ,
	fPCTNVuotKhung ,
	fNghiOm ,
	fHSBL ,
	fTongQTLN ,
	fDuToan ,
	fDaQuyetToan ,
	fConLai ,
	fThu_BHXH_NLD ,
	fThu_BHXH_NSD ,
	fTongSoPhaiThuBHXH ,
	fThu_BHYT_NLD ,
	fThu_BHYT_NSD ,
	fTongSoPhaiThuBHYT ,
	fThu_BHTN_NLD ,
	fThu_BHTN_NSD ,
	fTongSoPhaiThuBHTN ,
	fTongNLD ,
	fTongNSD ,
	fTongCong ,
	MaDonVi, 
	TenDonVi 
	)
	SELECT
	NULL ,
	NULL ,
	0 bHangCha , 
	sXauNoiMa , 
	dv.sTenDonVi,
	tempChiTietDonVi.iNamLamViec ,
	iQSBQNam ,
	fLuongChinh ,
	fPCChucVu ,
	fPCTNNghe ,
	fPCTNVuotKhung ,
	fNghiOm ,
	fHSBL ,
	fTongQTLN ,
	fDuToan ,
	fDaQuyetToan ,
	fConLai ,
	fThu_BHXH_NLD ,
	fThu_BHXH_NSD ,
	fTongSoPhaiThuBHXH ,
	fThu_BHYT_NLD ,
	fThu_BHYT_NSD ,
	fTongSoPhaiThuBHYT ,
	fThu_BHTN_NLD ,
	fThu_BHTN_NSD ,
	fTongSoPhaiThuBHTN ,
	0 fTongNLD ,
	0 fTongNSD ,
	fTongCong ,
	dv.iID_MaDonVi, 
	dv.sTenDonVi as TenDonVi 
	FROM tempChiTietDonVi 
	LEFT JOIN DonVi dv ON dv.iID_MaDonVi = tempChiTietDonVi.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec;
	SELECT * FROM #result ORDER BY sXauNoiMa , #result.MaDonVi;

DROP TABLE #result;
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempChiTietDonVi]') AND type in (N'U')) drop table tempChiTietDonVi;

END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_thang]    Script Date: 7/16/2024 5:04:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_thang]
	@NamLamViec int,
	@IdDonVis nvarchar(100),
	@Donvitinh int,
	@IQuy int,
	@ILoaiQuy int,
	@IsLuyKe bit
AS
BEGIN

	DECLARE @SMonths NVARCHAR(50)

	IF (@IQuy = 3) BEGIN SET @SMonths = '1,2,3' END
	ELSE IF (@IQuy = 6 AND @IsLuyKe = 0) BEGIN SET @SMonths = '4,5,6' END
	ELSE IF (@IQuy = 9 AND @IsLuyKe = 0) BEGIN SET @SMonths = '7,8,9' END
	ELSE IF (@IQuy = 12 AND @IsLuyKe = 0) BEGIN SET @SMonths = '10,11,12' END

	ELSE IF (@IQuy = 6 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6' END
	ELSE IF (@IQuy = 9 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6,7,8,9' END
	ELSE IF (@IQuy = 12 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6,7,8,9,10,11,12' END

	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVis;

	DECLARE @sSoChungTuTH nvarchar(1000) = (SELECT TOP 1 sTongHop FROM BH_QTT_BHXH_ChungTu pr where pr.iNamLamViec = @NamLamViec
																			and pr.iQuyNam in (SELECT * FROM f_split(@SMonths))
																			and pr.iQuyNamLoai = @ILoaiQuy
																			and pr.iID_MaDonVi = @IdDonVis
																			and pr.iLoaiTongHop = 2
																			and pr.bDaTongHop = 0)

	CREATE TABLE #result(
		iID_MLNS uniqueidentifier,
		iID_MLNS_Cha uniqueidentifier,
		bHangCha bit, 
		sXauNoiMa nvarchar(200), 
		sMoTa nvarchar(200), 
		iNamLamViec int,
		iQSBQNam int,
		fLuongChinh float,
		fPhuCapChucVu float,
		fPCTNNghe float,
		fPCTNVuotKhung float,
		fNghiOm float,
		fHSBL float,
		fTongQTLN float,
		fDuToan float,
		fDaQuyetToan float,
		fConLai float,
		fThu_BHXH_NLD float,
		fThu_BHXH_NSD float,
		fTongSoPhaiThuBHXH float,
		fThu_BHYT_NLD float,
		fThu_BHYT_NSD float,
		fTongSoPhaiThuBHYT float,
		fThu_BHTN_NLD float,
		fThu_BHTN_NSD float,
		fTongSoPhaiThuBHTN float,
		fTongNLD float,
		fTongNSD float,
		fTongCong float,
		MaDonVi nvarchar(50),
		TenDonVi nvarchar(50)
	);

	----------------END DETAIL AGENCY----------------
			select
				ct.iID_MaDonVi,
				ct.iNamLamViec
				,ctct.iID_QTT_BHXH_ChungTu
				,ctct.iQSBQNam iQSBQNam
				,ctct.fLuongChinh fLuongChinh
				,ctct.fPCChucVu fPCChucVu
				,ctct.fPCTNNghe fPCTNNghe
				,ctct.fPCTNVuotKhung fPCTNVuotKhung
				,ctct.fNghiOm fNghiOm
				,ctct.fHSBL fHSBL
				,ctct.fTongQTLN fTongQTLN
				,ctct.fDuToan fDuToan
				,ctct.fDaQuyetToan fDaQuyetToan
				,ctct.fConLai fConLai
				,ctct.fThu_BHXH_NLD fThu_BHXH_NLD
				,ctct.fThu_BHXH_NSD fThu_BHXH_NSD
				,ctct.fTongSoPhaiThuBHXH fTongSoPhaiThuBHXH
				,ctct.fThu_BHYT_NLD fThu_BHYT_NLD
				,ctct.fThu_BHYT_NSD fThu_BHYT_NSD
				,ctct.fTongSoPhaiThuBHYT fTongSoPhaiThuBHYT
				,ctct.fThu_BHTN_NLD fThu_BHTN_NLD
				,ctct.fThu_BHTN_NSD fThu_BHTN_NSD
				,ctct.fTongSoPhaiThuBHTN fTongSoPhaiThuBHTN
				,ctct.fTongCong fTongCong
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				,mlns.sMoTa
				INTO #tempChiTietDonVi
				from
				BH_QTT_BHXH_ChungTu ct
				INNER JOIN
				BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
				LEFT JOIN BH_DM_MucLucNganSach mlns ON  mlns.sXauNoiMa = ctct.sXauNoiMa AND mlns.iNamLamViec = @NamLamViec
				where ct.iNamLamViec = @NamLamViec
				and ct.iQuyNam in (SELECT * FROM f_split(@SMonths))
				and ct.iQuyNamLoai = @ILoaiQuy
				and ct.sSoChungTu in (select * from splitstring(@sSoChungTuTH))
				and ct.iLoaiTongHop = 1
				and ct.bDaTongHop = 1;

	----------------END DETAIL----------------
	----------------INSERT TOTAL----------------
	INSERT INTO #result
	(
	iID_MLNS ,
	iID_MLNS_Cha ,
	bHangCha , 
	sXauNoiMa , 
	sMoTa , 
	iNamLamViec ,
	iQSBQNam ,
	fLuongChinh ,
	fPhuCapChucVu ,
	fPCTNNghe ,
	fPCTNVuotKhung ,
	fNghiOm ,
	fHSBL ,
	fTongQTLN ,
	fDuToan ,
	fDaQuyetToan ,
	fConLai ,
	fThu_BHXH_NLD ,
	fThu_BHXH_NSD ,
	fTongSoPhaiThuBHXH ,
	fThu_BHYT_NLD ,
	fThu_BHYT_NSD ,
	fTongSoPhaiThuBHYT ,
	fThu_BHTN_NLD ,
	fThu_BHTN_NSD ,
	fTongSoPhaiThuBHTN ,
	fTongNLD ,
	fTongNSD ,
	fTongCong ,
	MaDonVi, 
	TenDonVi 
	)
	select
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.bHangCha,
			mlns.sXauNoiMa,
			mlns.sMoTa,
			@NamLamViec iNamLamViec,
			sum(isnull(chungtudonvi.iQSBQNam, 0)) iQSBQNam,
			sum(isnull(chungtudonvi.fLuongChinh, 0))/@Donvitinh fLuongChinh,
			sum(isnull(chungtudonvi.fPCChucVu, 0))/@Donvitinh fPCChucVu,
			sum(isnull(chungtudonvi.fPCTNNghe, 0))/@Donvitinh fPCTNNghe,
			sum(isnull(chungtudonvi.fPCTNVuotKhung, 0))/@Donvitinh fPCTNVuotKhung,
			sum(isnull(chungtudonvi.fNghiOm, 0))/@Donvitinh fNghiOm,
			sum(isnull(chungtudonvi.fHSBL, 0))/@Donvitinh fHSBL,
			sum(isnull((chungtudonvi.fLuongChinh + chungtudonvi.fPCChucVu + chungtudonvi.fPCTNNghe + chungtudonvi.fPCTNVuotKhung + chungtudonvi.fNghiOm + chungtudonvi.fHSBL), 0))/@Donvitinh fTongQTLN,
			sum(isnull(chungtudonvi.fDuToan, 0))/@Donvitinh fDuToan,
			sum(isnull(chungtudonvi.fDaQuyetToan, 0))/@Donvitinh fDaQuyetToan,
			sum(isnull(chungtudonvi.fConLai, 0))/@Donvitinh fConLai,
			sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0))/@Donvitinh fThu_BHXH_NLD,
			sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0))/@Donvitinh fThu_BHXH_NSD,
			sum((isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHXH_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHXH,
			sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0))/@Donvitinh fThu_BHYT_NLD,
			sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0))/@Donvitinh fThu_BHYT_NSD,
			sum((isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHYT,
			sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0))/@Donvitinh fThu_BHTN_NLD,
			sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0))/@Donvitinh fThu_BHTN_NSD,
			sum((isnull(chungtudonvi.fThu_BHTN_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHTN,
			sum((isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NLD, 0)))/@Donvitinh fTongNLD,
			sum((isnull(chungtudonvi.fThu_BHXH_NSD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongNSD,
			sum((isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHXH_NSD, 0) + isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0) + isnull(chungtudonvi.fThu_BHTN_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongCong,
			null,
			null
			FROM
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
				AND iNamLamViec = @NamLamViec) mlns
			LEFT JOIN(
				select
				ct.iID_MaDonVi,
				ct.iNamLamViec
				,ctct.iQSBQNam iQSBQNam
				,ctct.fLuongChinh fLuongChinh
				,ctct.fPCChucVu fPCChucVu
				,ctct.fPCTNNghe fPCTNNghe
				,ctct.fPCTNVuotKhung fPCTNVuotKhung
				,ctct.fNghiOm fNghiOm
				,ctct.fHSBL fHSBL
				,ctct.fTongQTLN fTongQTLN
				,ctct.fDuToan fDuToan
				,ctct.fDaQuyetToan fDaQuyetToan
				,ctct.fConLai fConLai
				,ctct.fThu_BHXH_NLD fThu_BHXH_NLD
				,ctct.fThu_BHXH_NSD fThu_BHXH_NSD
				,ctct.fTongSoPhaiThuBHXH fTongSoPhaiThuBHXH
				,ctct.fThu_BHYT_NLD fThu_BHYT_NLD
				,ctct.fThu_BHYT_NSD fThu_BHYT_NSD
				,ctct.fTongSoPhaiThuBHYT fTongSoPhaiThuBHYT
				,ctct.fThu_BHTN_NLD fThu_BHTN_NLD
				,ctct.fThu_BHTN_NSD fThu_BHTN_NSD
				,ctct.fTongSoPhaiThuBHTN fTongSoPhaiThuBHTN
				,ctct.fTongCong fTongCong
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				from
				BH_QTT_BHXH_ChungTu ct
				join
				BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
				where ct.iNamLamViec = @NamLamViec
				and ct.iQuyNam in (SELECT * FROM f_split(@SMonths))
				and ct.iQuyNamLoai = @ILoaiQuy
				--and ct.iID_MaDonVi = @IdDonVis
				and ((@isCha = 0 and ctct.iID_MaDonVi =@IdDonVis) or (@isCha = 1 and ct.iID_MaDonVi =@IdDonVis))
				and ct.iLoaiTongHop = 2	
			)chungtudonvi 
				on mlns.iID_MLNS = chungtudonvi.iID_MLNS
				group by
					mlns.iID_MLNS,
					mlns.iID_MLNS_Cha,
					mlns.bHangCha,
					mlns.sXauNoiMa,
					mlns.sMoTa
				ORDER BY mlns.sXauNoiMa;


	----------------END INSERT DETAIL----------------
	----------------INSERT DETAIL----------------
	INSERT INTO #result
	(
	iID_MLNS ,
	iID_MLNS_Cha ,
	bHangCha , 
	sXauNoiMa , 
	sMoTa , 
	iNamLamViec ,
	iQSBQNam ,
	fLuongChinh ,
	fPhuCapChucVu ,
	fPCTNNghe ,
	fPCTNVuotKhung ,
	fNghiOm ,
	fHSBL ,
	fTongQTLN ,
	fDuToan ,
	fDaQuyetToan ,
	fConLai ,
	fThu_BHXH_NLD ,
	fThu_BHXH_NSD ,
	fTongSoPhaiThuBHXH ,
	fThu_BHYT_NLD ,
	fThu_BHYT_NSD ,
	fTongSoPhaiThuBHYT ,
	fThu_BHTN_NLD ,
	fThu_BHTN_NSD ,
	fTongSoPhaiThuBHTN ,
	fTongNLD ,
	fTongNSD ,
	fTongCong ,
	MaDonVi, 
	TenDonVi 
	)
	SELECT
		NULL ,
		NULL ,
		0 bHangCha , 
		dt.sXauNoiMa , 
		dv.sTenDonVi,
		dt.iNamLamViec ,
		sum(isnull(dt.iQSBQNam, 0)) iQSBQNam,
		sum(isnull(dt.fLuongChinh, 0)) fLuongChinh,
		sum(isnull(dt.fPCChucVu, 0)) fPCChucVu,
		sum(isnull(dt.fPCTNNghe, 0)) fPCTNNghe,
		sum(isnull(dt.fPCTNVuotKhung, 0)) fPCTNVuotKhung,
		sum(isnull(dt.fNghiOm, 0)) fNghiOm,
		sum(isnull(dt.fHSBL, 0)) fHSBL,
		sum(isnull(dt.fTongQTLN, 0)) fTongQTLN,
		sum(isnull(dt.fDuToan, 0)) fDuToan,
		sum(isnull(dt.fDaQuyetToan, 0)) fDaQuyetToan,
		sum(isnull(dt.fConLai, 0)) fConLai,
		sum(isnull(dt.fThu_BHXH_NLD, 0)) fThu_BHXH_NLD,
		sum(isnull(dt.fThu_BHXH_NSD, 0)) fThu_BHXH_NSD,
		sum(isnull(dt.fTongSoPhaiThuBHXH, 0)) fTongSoPhaiThuBHXH,
		sum(isnull(dt.fThu_BHYT_NLD, 0)) fThu_BHYT_NLD,
		sum(isnull(dt.fThu_BHYT_NSD, 0)) fThu_BHYT_NSD,
		sum(isnull(dt.fTongSoPhaiThuBHYT, 0)) fTongSoPhaiThuBHYT,
		sum(isnull(dt.fThu_BHTN_NLD, 0)) fThu_BHTN_NLD,
		sum(isnull(dt.fThu_BHTN_NSD, 0)) fThu_BHTN_NSD,
		sum(isnull(dt.fTongSoPhaiThuBHTN, 0)) fTongSoPhaiThuBHTN,
		0 fTongNLD ,
		0 fTongNSD ,
		sum(isnull(dt.fTongCong, 0)) ,
		dv.iID_MaDonVi, 
		dv.sTenDonVi as TenDonVi 
	FROM #tempChiTietDonVi dt
	LEFT JOIN DonVi dv ON dv.iID_MaDonVi = dt.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
	group by
		dt.sXauNoiMa, 
		dv.sTenDonVi,
		dt.iNamLamViec,
		dv.iID_MaDonVi, 
		dv.sTenDonVi;

	SELECT * FROM #result ORDER BY sXauNoiMa , #result.MaDonVi;

	DROP TABLE #tempChiTietDonVi;
	DROP TABLE #result;
END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]    Script Date: 7/16/2024 5:04:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]
	@NamLamViec int ,
	@IdDonVis nvarchar(1000),
	@Donvitinh int ,
	@IQuy int,
	@ILoaiQuy int,
	@IsLuyKe bit
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempChiTietDonVi]') AND type in (N'U')) drop table tempChiTietDonVi;

	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVis;

	CREATE TABLE #result(
		iID_MLNS uniqueidentifier,
		iID_MLNS_Cha uniqueidentifier,
		bHangCha bit, 
		sXauNoiMa nvarchar(200), 
		sMoTa nvarchar(200), 
		iNamLamViec int,
		iQSBQNam int,
		fLuongChinh float,
		fPhuCapChucVu float,
		fPCTNNghe float,
		fPCTNVuotKhung float,
		fNghiOm float,
		fHSBL float,
		fTongQTLN float,
		fDuToan float,
		fDaQuyetToan float,
		fConLai float,
		fThu_BHXH_NLD float,
		fThu_BHXH_NSD float,
		fTongSoPhaiThuBHXH float,
		fThu_BHYT_NLD float,
		fThu_BHYT_NSD float,
		fTongSoPhaiThuBHYT float,
		fThu_BHTN_NLD float,
		fThu_BHTN_NSD float,
		fTongSoPhaiThuBHTN float,
		fTongNLD float,
		fTongNSD float,
		fTongCong float,
		MaDonVi nvarchar(50),
		TenDonVi nvarchar(50)
	);

	--- GET CHI TIẾT ĐƠN VỊ
	-- Ko In luy ke
	if (@IsLuyKe = 0)
	begin
			select
				ct.iID_MaDonVi,
				ct.iNamLamViec,
				ctct.iID_QTT_BHXH_ChungTu_ChiTiet
				,ctct.iID_QTT_BHXH_ChungTu
				,ctct.iQSBQNam
				,ctct.fLuongChinh
				,ctct.fPCChucVu
				,ctct.fPCTNNghe
				,ctct.fPCTNVuotKhung
				,ctct.fNghiOm
				,ctct.fHSBL
				,ctct.fTongQTLN
				,ctct.fDuToan
				,ctct.fDaQuyetToan
				,ctct.fConLai
				,ctct.fThu_BHXH_NLD
				,ctct.fThu_BHXH_NSD
				,ctct.fTongSoPhaiThuBHXH
				,ctct.fThu_BHYT_NLD
				,ctct.fThu_BHYT_NSD
				,ctct.fTongSoPhaiThuBHYT
				,ctct.fThu_BHTN_NLD
				,ctct.fThu_BHTN_NSD
				,ctct.fTongSoPhaiThuBHTN
				,ctct.fTongCong
				,ctct.sGhiChu
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				,mlns.sMoTa
				INTO tempChiTietDonVi
				from
				BH_QTT_BHXH_ChungTu ct
				INNER JOIN
				BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
				LEFT JOIN BH_DM_MucLucNganSach mlns ON  mlns.sXauNoiMa = ctct.sXauNoiMa AND mlns.iNamLamViec = @NamLamViec
				where ct.iNamLamViec = @NamLamViec
				and ct.iQuyNam = @IQuy
				and ct.iQuyNamLoai = @ILoaiQuy
				--and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
				and ((@isCha = 0 and ctct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))) or (@isCha = 1 and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))))
				and ct.iLoaiTongHop = 1
				--and ct.bDaTongHop = 0;
		end
		--In luy ke
		else
		begin
			select
				ct.iID_MaDonVi,
				ct.iNamLamViec
				,sum(isnull(ctct.iQSBQNam, 0)) iQSBQNam
				,sum(isnull(ctct.fLuongChinh, 0)) fLuongChinh
				,sum(isnull(ctct.fPCChucVu, 0)) fPCChucVu
				,sum(isnull(ctct.fPCTNNghe, 0)) fPCTNNghe
				,sum(isnull(ctct.fPCTNVuotKhung, 0)) fPCTNVuotKhung
				,sum(isnull(ctct.fNghiOm, 0)) fNghiOm
				,sum(isnull(ctct.fHSBL, 0)) fHSBL
				,sum(isnull(ctct.fTongQTLN, 0)) fTongQTLN
				,sum(isnull(ctct.fDuToan, 0)) fDuToan
				,sum(isnull(ctct.fDaQuyetToan, 0)) fDaQuyetToan
				,sum(isnull(ctct.fConLai, 0)) fConLai
				,sum(isnull(ctct.fThu_BHXH_NLD, 0)) fThu_BHXH_NLD
				,sum(isnull(ctct.fThu_BHXH_NSD, 0)) fThu_BHXH_NSD
				,sum(isnull(ctct.fTongSoPhaiThuBHXH, 0)) fTongSoPhaiThuBHXH
				,sum(isnull(ctct.fThu_BHYT_NLD, 0)) fThu_BHYT_NLD
				,sum(isnull(ctct.fThu_BHYT_NSD, 0)) fThu_BHYT_NSD
				,sum(isnull(ctct.fTongSoPhaiThuBHYT, 0)) fTongSoPhaiThuBHYT
				,sum(isnull(ctct.fThu_BHTN_NLD, 0)) fThu_BHTN_NLD
				,sum(isnull(ctct.fThu_BHTN_NSD, 0)) fThu_BHTN_NSD
				,sum(isnull(ctct.fTongSoPhaiThuBHTN, 0)) fTongSoPhaiThuBHTN
				,sum(isnull(ctct.fTongCong, 0)) fTongCong
				,ctct.sGhiChu
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				,mlns.sMoTa
				INTO tempChiTietDonVi
				from
				BH_QTT_BHXH_ChungTu ct
				INNER JOIN
				BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
				LEFT JOIN BH_DM_MucLucNganSach mlns ON  mlns.sXauNoiMa = ctct.sXauNoiMa AND mlns.iNamLamViec = @NamLamViec
				where ct.iNamLamViec = @NamLamViec
				and ct.iQuyNam <= @IQuy
				and ct.iQuyNamLoai = @ILoaiQuy
				--and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
				and ((@isCha = 0 and ctct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))) or (@isCha = 1 and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))))
				and ct.iLoaiTongHop = 1
				group by 
				ct.iID_MaDonVi,
				ct.iNamLamViec,
				ctct.sGhiChu
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				,mlns.sMoTa
				--and ct.bDaTongHop = 0;
		end
	--END chi tiet

	INSERT INTO #result
	(
	iID_MLNS ,
	iID_MLNS_Cha ,
	bHangCha , 
	sXauNoiMa , 
	sMoTa , 
	iNamLamViec ,
	iQSBQNam ,
	fLuongChinh ,
	fPhuCapChucVu ,
	fPCTNNghe ,
	fPCTNVuotKhung ,
	fNghiOm ,
	fHSBL ,
	fTongQTLN ,
	fDuToan ,
	fDaQuyetToan ,
	fConLai ,
	fThu_BHXH_NLD ,
	fThu_BHXH_NSD ,
	fTongSoPhaiThuBHXH ,
	fThu_BHYT_NLD ,
	fThu_BHYT_NSD ,
	fTongSoPhaiThuBHYT ,
	fThu_BHTN_NLD ,
	fThu_BHTN_NSD ,
	fTongSoPhaiThuBHTN ,
	fTongNLD ,
	fTongNSD ,
	fTongCong ,
	MaDonVi, 
	TenDonVi 
	)
	--INSERT TOTAL
	select
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.bHangCha,
			mlns.sXauNoiMa,
			mlns.sMoTa,
			@NamLamViec iNamLamViec,
			sum(chungtudonvi.iQSBQNam) iQSBQNam,
			sum(chungtudonvi.fLuongChinh)/@Donvitinh fLuongChinh,
			sum(chungtudonvi.fPCChucVu)/@Donvitinh fPCChucVu,
			sum(chungtudonvi.fPCTNNghe)/@Donvitinh fPCTNNghe,
			sum(chungtudonvi.fPCTNVuotKhung)/@Donvitinh fPCTNVuotKhung,
			sum(chungtudonvi.fNghiOm)/@Donvitinh fNghiOm,
			sum(chungtudonvi.fHSBL)/@Donvitinh fHSBL,
			(sum(isnull(chungtudonvi.fLuongChinh, 0)) + sum(isnull(chungtudonvi.fPCChucVu, 0)) + sum(isnull(chungtudonvi.fPCTNNghe, 0)) + sum(isnull(chungtudonvi.fPCTNVuotKhung, 0)) + sum(isnull(chungtudonvi.fNghiOm, 0)) + sum(isnull(chungtudonvi.fHSBL, 0)))/@Donvitinh fTongQTLN,
			sum(chungtudonvi.fDuToan) fDuToan,
			sum(chungtudonvi.fDaQuyetToan) fDaQuyetToan,
			sum(chungtudonvi.fConLai) fConLai,
			sum(chungtudonvi.fThu_BHXH_NLD)/@Donvitinh fThu_BHXH_NLD,
			sum(chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fThu_BHXH_NSD,
			(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHXH,
			sum(chungtudonvi.fThu_BHYT_NLD)/@Donvitinh fThu_BHYT_NLD,
			sum(chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fThu_BHYT_NSD,
			(sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHYT,
			sum(chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fThu_BHTN_NLD,
			sum(chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fThu_BHTN_NSD,
			(sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHTN,
			(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0))) fTongNLD,
			(sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0))) fTongNSD,
			(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongCong,
			null,
			null
			FROM
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
				AND iNamLamViec = @NamLamViec) mlns
			LEFT JOIN tempChiTietDonVi chungtudonvi 
				on mlns.iID_MLNS = chungtudonvi.iID_MLNS
			GROUP BY
				mlns.iID_MLNS,
				mlns.iID_MLNS_Cha,
				mlns.bHangCha,
				mlns.sXauNoiMa,
				mlns.sMoTa
			order by mlns.sXauNoiMa;
		--INSERT CHI TIẾT	
	INSERT INTO #result
	(
	iID_MLNS ,
	iID_MLNS_Cha ,
	bHangCha , 
	sXauNoiMa , 
	sMoTa , 
	iNamLamViec ,
	iQSBQNam ,
	fLuongChinh ,
	fPhuCapChucVu ,
	fPCTNNghe ,
	fPCTNVuotKhung ,
	fNghiOm ,
	fHSBL ,
	fTongQTLN ,
	fDuToan ,
	fDaQuyetToan ,
	fConLai ,
	fThu_BHXH_NLD ,
	fThu_BHXH_NSD ,
	fTongSoPhaiThuBHXH ,
	fThu_BHYT_NLD ,
	fThu_BHYT_NSD ,
	fTongSoPhaiThuBHYT ,
	fThu_BHTN_NLD ,
	fThu_BHTN_NSD ,
	fTongSoPhaiThuBHTN ,
	fTongNLD ,
	fTongNSD ,
	fTongCong ,
	MaDonVi, 
	TenDonVi 
	)
	SELECT
	NULL ,
	NULL ,
	0 bHangCha , 
	sXauNoiMa , 
	dv.sTenDonVi,
	tempChiTietDonVi.iNamLamViec ,
	iQSBQNam ,
	fLuongChinh ,
	fPCChucVu ,
	fPCTNNghe ,
	fPCTNVuotKhung ,
	fNghiOm ,
	fHSBL ,
	fTongQTLN ,
	fDuToan ,
	fDaQuyetToan ,
	fConLai ,
	fThu_BHXH_NLD ,
	fThu_BHXH_NSD ,
	fTongSoPhaiThuBHXH ,
	fThu_BHYT_NLD ,
	fThu_BHYT_NSD ,
	fTongSoPhaiThuBHYT ,
	fThu_BHTN_NLD ,
	fThu_BHTN_NSD ,
	fTongSoPhaiThuBHTN ,
	0 fTongNLD ,
	0 fTongNSD ,
	fTongCong ,
	dv.iID_MaDonVi, 
	dv.sTenDonVi as TenDonVi 
	FROM tempChiTietDonVi 
	LEFT JOIN DonVi dv ON dv.iID_MaDonVi = tempChiTietDonVi.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec;
	SELECT * FROM #result ORDER BY sXauNoiMa , #result.MaDonVi;


	DROP TABLE #result;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempChiTietDonVi]') AND type in (N'U')) drop table tempChiTietDonVi;

END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_thang]    Script Date: 7/16/2024 5:04:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_thang]
	@NamLamViec int ,
	@IdDonVis nvarchar(1000),
	@Donvitinh int ,
	@IQuy int,
	@ILoaiQuy int,
	@IsLuyKe bit
AS
BEGIN
	
	DECLARE @SMonths NVARCHAR(50)

	IF (@IQuy = 3) BEGIN SET @SMonths = '1,2,3' END
	ELSE IF (@IQuy = 6 AND @IsLuyKe = 0) BEGIN SET @SMonths = '4,5,6' END
	ELSE IF (@IQuy = 9 AND @IsLuyKe = 0) BEGIN SET @SMonths = '7,8,9' END
	ELSE IF (@IQuy = 12 AND @IsLuyKe = 0) BEGIN SET @SMonths = '10,11,12' END

	ELSE IF (@IQuy = 6 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6' END
	ELSE IF (@IQuy = 9 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6,7,8,9' END
	ELSE IF (@IQuy = 12 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6,7,8,9,10,11,12' END

	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVis;

	CREATE TABLE #result(
		iID_MLNS uniqueidentifier,
		iID_MLNS_Cha uniqueidentifier,
		bHangCha bit, 
		sXauNoiMa nvarchar(200), 
		sMoTa nvarchar(200), 
		iNamLamViec int,
		iQSBQNam int,
		fLuongChinh float,
		fPhuCapChucVu float,
		fPCTNNghe float,
		fPCTNVuotKhung float,
		fNghiOm float,
		fHSBL float,
		fTongQTLN float,
		fDuToan float,
		fDaQuyetToan float,
		fConLai float,
		fThu_BHXH_NLD float,
		fThu_BHXH_NSD float,
		fTongSoPhaiThuBHXH float,
		fThu_BHYT_NLD float,
		fThu_BHYT_NSD float,
		fTongSoPhaiThuBHYT float,
		fThu_BHTN_NLD float,
		fThu_BHTN_NSD float,
		fTongSoPhaiThuBHTN float,
		fTongNLD float,
		fTongNSD float,
		fTongCong float,
		MaDonVi nvarchar(50),
		TenDonVi nvarchar(50)
	);

	--- GET CHI TIẾT ĐƠN VỊ
			select
				ct.iID_MaDonVi,
				ct.iNamLamViec,
				ctct.iID_QTT_BHXH_ChungTu_ChiTiet
				,ctct.iID_QTT_BHXH_ChungTu
				,ctct.iQSBQNam iQSBQNam
				,ctct.fLuongChinh fLuongChinh
				,ctct.fPCChucVu fPCChucVu
				,ctct.fPCTNNghe fPCTNNghe
				,ctct.fPCTNVuotKhung fPCTNVuotKhung
				,ctct.fNghiOm fNghiOm
				,ctct.fHSBL fHSBL
				,ctct.fTongQTLN fTongQTLN
				,ctct.fDuToan fDuToan
				,ctct.fDaQuyetToan fDaQuyetToan
				,ctct.fConLai fConLai
				,ctct.fThu_BHXH_NLD fThu_BHXH_NLD
				,ctct.fThu_BHXH_NSD fThu_BHXH_NSD
				,ctct.fTongSoPhaiThuBHXH fTongSoPhaiThuBHXH
				,ctct.fThu_BHYT_NLD fThu_BHYT_NLD
				,ctct.fThu_BHYT_NSD fThu_BHYT_NSD
				,ctct.fTongSoPhaiThuBHYT fTongSoPhaiThuBHYT
				,ctct.fThu_BHTN_NLD fThu_BHTN_NLD
				,ctct.fThu_BHTN_NSD fThu_BHTN_NSD
				,ctct.fTongSoPhaiThuBHTN fTongSoPhaiThuBHTN
				,ctct.fTongCong fTongCong
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				,mlns.sMoTa
				INTO #tempChiTietDonVi
				from
				BH_QTT_BHXH_ChungTu ct
				INNER JOIN
				BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
				LEFT JOIN BH_DM_MucLucNganSach mlns ON  mlns.sXauNoiMa = ctct.sXauNoiMa AND mlns.iNamLamViec = @NamLamViec
				where ct.iNamLamViec = @NamLamViec
				and ct.iQuyNam in (SELECT * FROM f_split(@SMonths))
				and ct.iQuyNamLoai = @ILoaiQuy
				--and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
				and ((@isCha = 0 and ctct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))) or (@isCha = 1 and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))))
				and ct.iLoaiTongHop = 1
	--END chi tiet

	INSERT INTO #result
	(
		iID_MLNS ,
		iID_MLNS_Cha ,
		bHangCha , 
		sXauNoiMa , 
		sMoTa , 
		iNamLamViec ,
		iQSBQNam ,
		fLuongChinh ,
		fPhuCapChucVu ,
		fPCTNNghe ,
		fPCTNVuotKhung ,
		fNghiOm ,
		fHSBL ,
		fTongQTLN ,
		fDuToan ,
		fDaQuyetToan ,
		fConLai ,
		fThu_BHXH_NLD ,
		fThu_BHXH_NSD ,
		fTongSoPhaiThuBHXH ,
		fThu_BHYT_NLD ,
		fThu_BHYT_NSD ,
		fTongSoPhaiThuBHYT ,
		fThu_BHTN_NLD ,
		fThu_BHTN_NSD ,
		fTongSoPhaiThuBHTN ,
		fTongNLD ,
		fTongNSD ,
		fTongCong ,
		MaDonVi, 
		TenDonVi 
	)
	--INSERT TOTAL
	select
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.bHangCha,
			mlns.sXauNoiMa,
			mlns.sMoTa,
			@NamLamViec iNamLamViec,
			sum(chungtudonvi.iQSBQNam) iQSBQNam,
			sum(chungtudonvi.fLuongChinh)/@Donvitinh fLuongChinh,
			sum(chungtudonvi.fPCChucVu)/@Donvitinh fPCChucVu,
			sum(chungtudonvi.fPCTNNghe)/@Donvitinh fPCTNNghe,
			sum(chungtudonvi.fPCTNVuotKhung)/@Donvitinh fPCTNVuotKhung,
			sum(chungtudonvi.fNghiOm)/@Donvitinh fNghiOm,
			sum(chungtudonvi.fHSBL)/@Donvitinh fHSBL,
			(sum(isnull(chungtudonvi.fLuongChinh, 0)) + sum(isnull(chungtudonvi.fPCChucVu, 0)) + sum(isnull(chungtudonvi.fPCTNNghe, 0)) + sum(isnull(chungtudonvi.fPCTNVuotKhung, 0)) + sum(isnull(chungtudonvi.fNghiOm, 0)) + sum(isnull(chungtudonvi.fHSBL, 0)))/@Donvitinh fTongQTLN,
			sum(chungtudonvi.fDuToan)/@Donvitinh fDuToan,
			sum(chungtudonvi.fDaQuyetToan)/@Donvitinh fDaQuyetToan,
			sum(chungtudonvi.fConLai)/@Donvitinh fConLai,
			sum(chungtudonvi.fThu_BHXH_NLD)/@Donvitinh fThu_BHXH_NLD,
			sum(chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fThu_BHXH_NSD,
			(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHXH,
			sum(chungtudonvi.fThu_BHYT_NLD)/@Donvitinh fThu_BHYT_NLD,
			sum(chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fThu_BHYT_NSD,
			(sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHYT,
			sum(chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fThu_BHTN_NLD,
			sum(chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fThu_BHTN_NSD,
			(sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHTN,
			(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)))/@Donvitinh fTongNLD,
			(sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongNSD,
			(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongCong,
			null,
			null
			FROM
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
				AND iNamLamViec = @NamLamViec) mlns
			LEFT JOIN #tempChiTietDonVi chungtudonvi 
				on mlns.iID_MLNS = chungtudonvi.iID_MLNS
			GROUP BY
				mlns.iID_MLNS,
				mlns.iID_MLNS_Cha,
				mlns.bHangCha,
				mlns.sXauNoiMa,
				mlns.sMoTa
			order by mlns.sXauNoiMa;
		--INSERT CHI TIẾT	
	INSERT INTO #result
	(
		iID_MLNS ,
		iID_MLNS_Cha ,
		bHangCha , 
		sXauNoiMa , 
		sMoTa , 
		iNamLamViec ,
		iQSBQNam ,
		fLuongChinh ,
		fPhuCapChucVu ,
		fPCTNNghe ,
		fPCTNVuotKhung ,
		fNghiOm ,
		fHSBL ,
		fTongQTLN ,
		fDuToan ,
		fDaQuyetToan ,
		fConLai ,
		fThu_BHXH_NLD ,
		fThu_BHXH_NSD ,
		fTongSoPhaiThuBHXH ,
		fThu_BHYT_NLD ,
		fThu_BHYT_NSD ,
		fTongSoPhaiThuBHYT ,
		fThu_BHTN_NLD ,
		fThu_BHTN_NSD ,
		fTongSoPhaiThuBHTN ,
		fTongNLD ,
		fTongNSD ,
		fTongCong ,
		MaDonVi, 
		TenDonVi 
	)
	SELECT
		NULL ,
		NULL ,
		0 bHangCha , 
		dt.sXauNoiMa , 
		dv.sTenDonVi,
		dt.iNamLamViec ,
		sum(isnull(dt.iQSBQNam, 0)) iQSBQNam,
		sum(isnull(dt.fLuongChinh, 0)) fLuongChinh,
		sum(isnull(dt.fPCChucVu, 0)) fPCChucVu,
		sum(isnull(dt.fPCTNNghe, 0)) fPCTNNghe,
		sum(isnull(dt.fPCTNVuotKhung, 0)) fPCTNVuotKhung,
		sum(isnull(dt.fNghiOm, 0)) fNghiOm,
		sum(isnull(dt.fHSBL, 0)) fHSBL,
		sum(isnull(dt.fTongQTLN, 0)) fTongQTLN,
		sum(isnull(dt.fDuToan, 0)) fDuToan,
		sum(isnull(dt.fDaQuyetToan, 0)) fDaQuyetToan,
		sum(isnull(dt.fConLai, 0)) fConLai,
		sum(isnull(dt.fThu_BHXH_NLD, 0)) fThu_BHXH_NLD,
		sum(isnull(dt.fThu_BHXH_NSD, 0)) fThu_BHXH_NSD,
		sum(isnull(dt.fTongSoPhaiThuBHXH, 0)) fTongSoPhaiThuBHXH,
		sum(isnull(dt.fThu_BHYT_NLD, 0)) fThu_BHYT_NLD,
		sum(isnull(dt.fThu_BHYT_NSD, 0)) fThu_BHYT_NSD,
		sum(isnull(dt.fTongSoPhaiThuBHYT, 0)) fTongSoPhaiThuBHYT,
		sum(isnull(dt.fThu_BHTN_NLD, 0)) fThu_BHTN_NLD,
		sum(isnull(dt.fThu_BHTN_NSD, 0)) fThu_BHTN_NSD,
		sum(isnull(dt.fTongSoPhaiThuBHTN, 0)) fTongSoPhaiThuBHTN,
		0 fTongNLD ,
		0 fTongNSD ,
		sum(isnull(dt.fTongCong, 0)) ,
		dv.iID_MaDonVi, 
		dv.sTenDonVi as TenDonVi 
	FROM #tempChiTietDonVi dt
	LEFT JOIN DonVi dv ON dv.iID_MaDonVi = dt.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
	group by
		dt.sXauNoiMa, 
		dv.sTenDonVi,
		dt.iNamLamViec,
		dv.iID_MaDonVi, 
		dv.sTenDonVi;

	SELECT * FROM #result ORDER BY sXauNoiMa , #result.MaDonVi;

	DROP TABLE #tempChiTietDonVi;
	DROP TABLE #result;

END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_quy]    Script Date: 7/16/2024 5:04:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_quy]
	@NamLamViec int,
	@IdDonVis nvarchar(50),
	@Donvitinh int,
	@IQuy int,
	@ILoaiQuy int,
	@IsLuyKe bit
AS
BEGIN
	
	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVis;

	-- Ko In luy ke
	if (@IsLuyKe = 0)
	begin
		select
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.bHangCha,
			mlns.sXauNoiMa,
			mlns.sMoTa,
			@NamLamViec iNamLamViec,
			sum(chungtudonvi.iQSBQNam) iQSBQNam,
			sum(chungtudonvi.fLuongChinh)/@Donvitinh fLuongChinh,
			sum(chungtudonvi.fPCChucVu)/@Donvitinh fPhuCapChucVu,
			sum(chungtudonvi.fPCTNNghe)/@Donvitinh fPCTNNghe,
			sum(chungtudonvi.fPCTNVuotKhung)/@Donvitinh fPCTNVuotKhung,
			sum(chungtudonvi.fNghiOm)/@Donvitinh fNghiOm,
			sum(chungtudonvi.fHSBL)/@Donvitinh fHSBL,
			(sum(isnull(chungtudonvi.fLuongChinh, 0)) + sum(isnull(chungtudonvi.fPCChucVu, 0)) + sum(isnull(chungtudonvi.fPCTNNghe, 0)) + sum(isnull(chungtudonvi.fPCTNVuotKhung, 0)) + sum(isnull(chungtudonvi.fNghiOm, 0)) + sum(isnull(chungtudonvi.fHSBL, 0)))/@Donvitinh fTongQTLN,
			sum(chungtudonvi.fDuToan)/@Donvitinh fDuToan,
			sum(chungtudonvi.fDaQuyetToan)/@Donvitinh fDaQuyetToan,
			sum(chungtudonvi.fConLai)/@Donvitinh fConLai,
			sum(chungtudonvi.fThu_BHXH_NLD)/@Donvitinh fThu_BHXH_NLD,
			sum(chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fThu_BHXH_NSD,
			(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHXH,
			sum(chungtudonvi.fThu_BHYT_NLD)/@Donvitinh fThu_BHYT_NLD,
			sum(chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fThu_BHYT_NSD,
			(sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHYT,
			sum(chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fThu_BHTN_NLD,
			sum(chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fThu_BHTN_NSD,
			(sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHTN,
			(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)))/@Donvitinh fTongNLD,
			(sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongNSD,
			(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongCong
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
				ctct.iID_QTT_BHXH_ChungTu_ChiTiet
				,ctct.iID_QTT_BHXH_ChungTu
				,ctct.iQSBQNam
				,ctct.fLuongChinh
				,ctct.fPCChucVu
				,ctct.fPCTNNghe
				,ctct.fPCTNVuotKhung
				,ctct.fNghiOm
				,ctct.fHSBL
				,ctct.fTongQTLN
				,ctct.fDuToan
				,ctct.fDaQuyetToan
				,ctct.fConLai
				,ctct.fThu_BHXH_NLD
				,ctct.fThu_BHXH_NSD
				,ctct.fTongSoPhaiThuBHXH
				,ctct.fThu_BHYT_NLD
				,ctct.fThu_BHYT_NSD
				,ctct.fTongSoPhaiThuBHYT
				,ctct.fThu_BHTN_NLD
				,ctct.fThu_BHTN_NSD
				,ctct.fTongSoPhaiThuBHTN
				,ctct.fTongCong
				,ctct.sGhiChu
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				from
				BH_QTT_BHXH_ChungTu ct
				join
				BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
				where ct.iNamLamViec = @NamLamViec
				and ct.iQuyNam = @IQuy
				and ct.iQuyNamLoai = @ILoaiQuy
				and ((@isCha = 0 and ctct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))) or (@isCha = 1 and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))))
				and ct.iLoaiTongHop = 1
					) chungtudonvi 
				on mlns.iID_MLNS = chungtudonvi.iID_MLNS
			group by
				mlns.iID_MLNS,
				mlns.iID_MLNS_Cha,
				mlns.bHangCha,
				mlns.sXauNoiMa,
				mlns.sMoTa
			order by mlns.sXauNoiMa
	end
	-- In luy ke
	else
	begin
		select
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.bHangCha,
			mlns.sXauNoiMa,
			mlns.sMoTa,
			@NamLamViec iNamLamViec,
			sum(chungtudonvi.iQSBQNam) iQSBQNam,
			sum(chungtudonvi.fLuongChinh)/@Donvitinh fLuongChinh,
			sum(chungtudonvi.fPCChucVu)/@Donvitinh fPhuCapChucVu,
			sum(chungtudonvi.fPCTNNghe)/@Donvitinh fPCTNNghe,
			sum(chungtudonvi.fPCTNVuotKhung)/@Donvitinh fPCTNVuotKhung,
			sum(chungtudonvi.fNghiOm)/@Donvitinh fNghiOm,
			sum(chungtudonvi.fHSBL)/@Donvitinh fHSBL,
			(sum(isnull(chungtudonvi.fLuongChinh, 0)) + sum(isnull(chungtudonvi.fPCChucVu, 0)) + sum(isnull(chungtudonvi.fPCTNNghe, 0)) + sum(isnull(chungtudonvi.fPCTNVuotKhung, 0)) + sum(isnull(chungtudonvi.fNghiOm, 0)) + sum(isnull(chungtudonvi.fHSBL, 0)))/@Donvitinh fTongQTLN,
			sum(chungtudonvi.fDuToan)/@Donvitinh fDuToan,
			sum(chungtudonvi.fDaQuyetToan)/@Donvitinh fDaQuyetToan,
			sum(chungtudonvi.fConLai)/@Donvitinh fConLai,
			sum(chungtudonvi.fThu_BHXH_NLD)/@Donvitinh fThu_BHXH_NLD,
			sum(chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fThu_BHXH_NSD,
			(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHXH,
			sum(chungtudonvi.fThu_BHYT_NLD)/@Donvitinh fThu_BHYT_NLD,
			sum(chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fThu_BHYT_NSD,
			(sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHYT,
			sum(chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fThu_BHTN_NLD,
			sum(chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fThu_BHTN_NSD,
			(sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHTN,
			(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)))/@Donvitinh fTongNLD,
			(sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongNSD,
			(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongCong
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
				ct.iNamLamViec
				,sum(isnull(ctct.iQSBQNam, 0)) iQSBQNam
				,sum(isnull(ctct.fLuongChinh, 0)) fLuongChinh
				,sum(isnull(ctct.fPCChucVu, 0)) fPCChucVu
				,sum(isnull(ctct.fPCTNNghe, 0)) fPCTNNghe
				,sum(isnull(ctct.fPCTNVuotKhung, 0)) fPCTNVuotKhung
				,sum(isnull(ctct.fNghiOm, 0)) fNghiOm
				,sum(isnull(ctct.fHSBL, 0)) fHSBL
				,sum(isnull(ctct.fTongQTLN, 0)) fTongQTLN
				,sum(isnull(ctct.fDuToan, 0)) fDuToan
				,sum(isnull(ctct.fDaQuyetToan, 0)) fDaQuyetToan
				,sum(isnull(ctct.fConLai, 0)) fConLai
				,sum(isnull(ctct.fThu_BHXH_NLD, 0)) fThu_BHXH_NLD
				,sum(isnull(ctct.fThu_BHXH_NSD, 0)) fThu_BHXH_NSD
				,sum(isnull(ctct.fTongSoPhaiThuBHXH, 0)) fTongSoPhaiThuBHXH
				,sum(isnull(ctct.fThu_BHYT_NLD, 0)) fThu_BHYT_NLD
				,sum(isnull(ctct.fThu_BHYT_NSD, 0)) fThu_BHYT_NSD
				,sum(isnull(ctct.fTongSoPhaiThuBHYT, 0)) fTongSoPhaiThuBHYT
				,sum(isnull(ctct.fThu_BHTN_NLD, 0)) fThu_BHTN_NLD
				,sum(isnull(ctct.fThu_BHTN_NSD, 0)) fThu_BHTN_NSD
				,sum(isnull(ctct.fTongSoPhaiThuBHTN, 0)) fTongSoPhaiThuBHTN
				,sum(isnull(ctct.fTongCong, 0)) fTongCong
				,ctct.sGhiChu
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				from
				BH_QTT_BHXH_ChungTu ct
				join
				BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
				where ct.iNamLamViec = @NamLamViec
					and ct.iQuyNam <= @IQuy
					and ct.iQuyNamLoai = @ILoaiQuy
					and ((@isCha = 0 and ctct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))) or (@isCha = 1 and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))))
					and ct.iLoaiTongHop = 1
				group by 
					ct.iID_MaDonVi,
					ct.iNamLamViec
					,ctct.sGhiChu
					,ctct.iID_MLNS
					,ctct.iID_MLNS_Cha
					,ctct.sXauNoiMa
					,ctct.sLNS
					) chungtudonvi 
				on mlns.iID_MLNS = chungtudonvi.iID_MLNS
			group by
				mlns.iID_MLNS,
				mlns.iID_MLNS_Cha,
				mlns.bHangCha,
				mlns.sXauNoiMa,
				mlns.sMoTa
			order by mlns.sXauNoiMa
	end

END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_thang]    Script Date: 7/16/2024 5:04:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_thang]
	@NamLamViec int,
	@IdDonVis nvarchar(50),
	@Donvitinh int,
	@IQuy int,
	@ILoaiQuy int,
	@IsLuyKe bit
AS
BEGIN

	DECLARE @SMonths NVARCHAR(50)
	DECLARE @SLoaiQuy NVARCHAR(50)

	IF (@IQuy = 3) BEGIN SET @SMonths = '1,2,3' END
	ELSE IF (@IQuy = 6 AND @IsLuyKe = 0) BEGIN SET @SMonths = '4,5,6' END
	ELSE IF (@IQuy = 9 AND @IsLuyKe = 0) BEGIN SET @SMonths = '7,8,9' END
	ELSE IF (@IQuy = 12 AND @IsLuyKe = 0) BEGIN SET @SMonths = '10,11,12' END

	ELSE IF (@IQuy = 6 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6' END
	ELSE IF (@IQuy = 9 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6,7,8,9' END
	ELSE IF (@IQuy = 12 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6,7,8,9,10,11,12' END

	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVis;

	select
		mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		sum(chungtudonvi.iQSBQNam) iQSBQNam,
		(sum(chungtudonvi.fLuongChinh))/@Donvitinh fLuongChinh,
		(sum(chungtudonvi.fPCChucVu))/@Donvitinh fPhuCapChucVu,
		(sum(chungtudonvi.fPCTNNghe))/@Donvitinh fPCTNNghe,
		(sum(chungtudonvi.fPCTNVuotKhung))/@Donvitinh fPCTNVuotKhung,
		(sum(chungtudonvi.fNghiOm))/@Donvitinh fNghiOm,
		(sum(chungtudonvi.fHSBL))/@Donvitinh fHSBL,
		((sum(isnull(chungtudonvi.fLuongChinh, 0)) + sum(isnull(chungtudonvi.fPCChucVu, 0)) + sum(isnull(chungtudonvi.fPCTNNghe, 0)) + sum(isnull(chungtudonvi.fPCTNVuotKhung, 0)) + sum(isnull(chungtudonvi.fNghiOm, 0)) + sum(isnull(chungtudonvi.fHSBL, 0))))/@Donvitinh fTongQTLN,
		(sum(chungtudonvi.fDuToan))/@Donvitinh fDuToan,
		(sum(chungtudonvi.fDaQuyetToan))/@Donvitinh fDaQuyetToan,
		(sum(chungtudonvi.fConLai))/@Donvitinh fConLai,
		(sum(chungtudonvi.fThu_BHXH_NLD))/@Donvitinh fThu_BHXH_NLD,
		(sum(chungtudonvi.fThu_BHXH_NSD))/@Donvitinh fThu_BHXH_NSD,
		((sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0))))/@Donvitinh fTongSoPhaiThuBHXH,
		(sum(chungtudonvi.fThu_BHYT_NLD))/@Donvitinh fThu_BHYT_NLD,
		(sum(chungtudonvi.fThu_BHYT_NSD))/@Donvitinh fThu_BHYT_NSD,
		((sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0))))/@Donvitinh fTongSoPhaiThuBHYT,
		(sum(chungtudonvi.fThu_BHTN_NLD))/@Donvitinh fThu_BHTN_NLD,
		(sum(chungtudonvi.fThu_BHTN_NSD))/@Donvitinh fThu_BHTN_NSD,
		((sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0))))/@Donvitinh fTongSoPhaiThuBHTN,
		((sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0))))/@Donvitinh fTongNLD,
		((sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0))))/@Donvitinh fTongNSD,
		((sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0))))/@Donvitinh fTongCong
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
			ctct.iID_QTT_BHXH_ChungTu_ChiTiet
			,ctct.iID_QTT_BHXH_ChungTu
			,ctct.iQSBQNam
			,ctct.fLuongChinh
			,ctct.fPCChucVu
			,ctct.fPCTNNghe
			,ctct.fPCTNVuotKhung
			,ctct.fNghiOm
			,ctct.fHSBL
			,ctct.fTongQTLN
			,ctct.fDuToan
			,ctct.fDaQuyetToan
			,ctct.fConLai
			,ctct.fThu_BHXH_NLD
			,ctct.fThu_BHXH_NSD
			,ctct.fTongSoPhaiThuBHXH
			,ctct.fThu_BHYT_NLD
			,ctct.fThu_BHYT_NSD
			,ctct.fTongSoPhaiThuBHYT
			,ctct.fThu_BHTN_NLD
			,ctct.fThu_BHTN_NSD
			,ctct.fTongSoPhaiThuBHTN
			,ctct.fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			from
			BH_QTT_BHXH_ChungTu ct
			join
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam in (SELECT * FROM f_split(@SMonths))
			and ct.iQuyNamLoai = @ILoaiQuy
			and ((@isCha = 0 and ctct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))) or (@isCha = 1 and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))))
			and ct.iLoaiTongHop = 1
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		group by
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.bHangCha,
			mlns.sXauNoiMa,
			mlns.sMoTa
		order by mlns.sXauNoiMa
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_quy]    Script Date: 7/16/2024 5:04:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_quy]
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IsTongHop bit,
	@IQuy int,
	@ILoaiQuy int,
	@IsLuyKe bit
AS
BEGIN

	-- Ko In luy ke
	if (@IsLuyKe = 0)
	begin
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
		chungtudonvi.fPCChucVu/@Donvitinh fPhuCapChucVu,
		chungtudonvi.fPCTNNghe/@Donvitinh fPCTNNghe,
		chungtudonvi.fPCTNVuotKhung/@Donvitinh fPCTNVuotKhung,
		chungtudonvi.fNghiOm/@Donvitinh fNghiOm,
		chungtudonvi.fHSBL/@Donvitinh fHSBL,
		(isnull(chungtudonvi.fLuongChinh, 0) + isnull(chungtudonvi.fPCChucVu, 0) + isnull(chungtudonvi.fPCTNNghe, 0) + isnull(chungtudonvi.fPCTNVuotKhung, 0) + isnull(chungtudonvi.fNghiOm, 0) + isnull(chungtudonvi.fHSBL, 0))/@Donvitinh fTongQTLN,
		chungtudonvi.fDuToan/@Donvitinh fDuToan,
		chungtudonvi.fDaQuyetToan/@Donvitinh fDaQuyetToan,
		chungtudonvi.fConLai/@Donvitinh fConLai,
		chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
		chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
		(isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHXH_NSD, 0))/@Donvitinh fTongSoPhaiThuBHXH,
		chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
		chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
		(isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0))/@Donvitinh fTongSoPhaiThuBHYT,
		chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
		chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
		(isnull(chungtudonvi.fThu_BHTN_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0))/@Donvitinh fTongSoPhaiThuBHTN,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fTongNLD,
		(chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongNSD,
		(isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHXH_NSD, 0) + isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0) + isnull(chungtudonvi.fThu_BHTN_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0))/@Donvitinh fTongCong
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
			ctct.iID_QTT_BHXH_ChungTu_ChiTiet
			,ctct.iID_QTT_BHXH_ChungTu
			,ctct.iQSBQNam
			,ctct.fLuongChinh
			,ctct.fPCChucVu
			,ctct.fPCTNNghe
			,ctct.fPCTNVuotKhung
			,ctct.fNghiOm
			,ctct.fHSBL
			,ctct.fTongQTLN
			,ctct.fDuToan
			,ctct.fDaQuyetToan
			,ctct.fConLai
			,ctct.fThu_BHXH_NLD
			,ctct.fThu_BHXH_NSD
			,ctct.fTongSoPhaiThuBHXH
			,ctct.fThu_BHYT_NLD
			,ctct.fThu_BHYT_NSD
			,ctct.fTongSoPhaiThuBHYT
			,ctct.fThu_BHTN_NLD
			,ctct.fThu_BHTN_NSD
			,ctct.fTongSoPhaiThuBHTN
			,ctct.fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			from
			BH_QTT_BHXH_ChungTu ct
			join
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.iQuyNamLoai = @ILoaiQuy
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = 2
			--and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		order by mlns.sXauNoiMa
	end
	-- In luy ke
	else
	begin
		select
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
		chungtudonvi.fPCChucVu/@Donvitinh fPhuCapChucVu,
		chungtudonvi.fPCTNNghe/@Donvitinh fPCTNNghe,
		chungtudonvi.fPCTNVuotKhung/@Donvitinh fPCTNVuotKhung,
		chungtudonvi.fNghiOm/@Donvitinh fNghiOm,
		chungtudonvi.fHSBL/@Donvitinh fHSBL,
		(isnull(chungtudonvi.fLuongChinh, 0) + isnull(chungtudonvi.fPCChucVu, 0) + isnull(chungtudonvi.fPCTNNghe, 0) + isnull(chungtudonvi.fPCTNVuotKhung, 0) + isnull(chungtudonvi.fNghiOm, 0) + isnull(chungtudonvi.fHSBL, 0))/@Donvitinh fTongQTLN,
		chungtudonvi.fDuToan/@Donvitinh fDuToan,
		chungtudonvi.fDaQuyetToan/@Donvitinh fDaQuyetToan,
		chungtudonvi.fConLai/@Donvitinh fConLai,
		chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
		chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
		(isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHXH_NSD, 0))/@Donvitinh fTongSoPhaiThuBHXH,
		chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
		chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
		(isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0))/@Donvitinh fTongSoPhaiThuBHYT,
		chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
		chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
		(isnull(chungtudonvi.fThu_BHTN_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0))/@Donvitinh fTongSoPhaiThuBHTN,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fTongNLD,
		(chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongNSD,
		(isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHXH_NSD, 0) + isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0) + isnull(chungtudonvi.fThu_BHTN_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0))/@Donvitinh fTongCong
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
			ct.iNamLamViec
			,sum(isnull(ctct.iQSBQNam, 0)) iQSBQNam
			,sum(isnull(ctct.fLuongChinh, 0)) fLuongChinh
			,sum(isnull(ctct.fPCChucVu, 0)) fPCChucVu
			,sum(isnull(ctct.fPCTNNghe, 0)) fPCTNNghe
			,sum(isnull(ctct.fPCTNVuotKhung, 0)) fPCTNVuotKhung
			,sum(isnull(ctct.fNghiOm, 0)) fNghiOm
			,sum(isnull(ctct.fHSBL, 0)) fHSBL
			,sum(isnull(ctct.fTongQTLN, 0)) fTongQTLN
			,sum(isnull(ctct.fDuToan, 0)) fDuToan
			,sum(isnull(ctct.fDaQuyetToan, 0)) fDaQuyetToan
			,sum(isnull(ctct.fConLai, 0)) fConLai
			,sum(isnull(ctct.fThu_BHXH_NLD, 0)) fThu_BHXH_NLD
			,sum(isnull(ctct.fThu_BHXH_NSD, 0)) fThu_BHXH_NSD
			,sum(isnull(ctct.fTongSoPhaiThuBHXH, 0)) fTongSoPhaiThuBHXH
			,sum(isnull(ctct.fThu_BHYT_NLD, 0)) fThu_BHYT_NLD
			,sum(isnull(ctct.fThu_BHYT_NSD, 0)) fThu_BHYT_NSD
			,sum(isnull(ctct.fTongSoPhaiThuBHYT, 0)) fTongSoPhaiThuBHYT
			,sum(isnull(ctct.fThu_BHTN_NLD, 0)) fThu_BHTN_NLD
			,sum(isnull(ctct.fThu_BHTN_NSD, 0)) fThu_BHTN_NSD
			,sum(isnull(ctct.fTongSoPhaiThuBHTN, 0)) fTongSoPhaiThuBHTN
			,sum(isnull(ctct.fTongCong, 0)) fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			from
			BH_QTT_BHXH_ChungTu ct
			join
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam <= @IQuy
			and ct.iQuyNamLoai = @ILoaiQuy
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = 2
			--and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
			group by 
			ct.iID_MaDonVi,
			ct.iNamLamViec
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		order by mlns.sXauNoiMa
	end
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_thang]    Script Date: 7/16/2024 5:04:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_thang]
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IQuy int,
	@ILoaiQuy int,
	@IsLuyKe bit
AS
BEGIN

	DECLARE @SMonths NVARCHAR(50)
	DECLARE @SLoaiQuy NVARCHAR(50)

	IF (@IQuy = 3) BEGIN SET @SMonths = '1,2,3' END
	ELSE IF (@IQuy = 6 AND @IsLuyKe = 0) BEGIN SET @SMonths = '4,5,6' END
	ELSE IF (@IQuy = 9 AND @IsLuyKe = 0) BEGIN SET @SMonths = '7,8,9' END
	ELSE IF (@IQuy = 12 AND @IsLuyKe = 0) BEGIN SET @SMonths = '10,11,12' END

	ELSE IF (@IQuy = 6 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6' END
	ELSE IF (@IQuy = 9 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6,7,8,9' END
	ELSE IF (@IQuy = 12 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6,7,8,9,10,11,12' END

	select
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
		chungtudonvi.fPhuCapChucVu/@Donvitinh fPhuCapChucVu,
		chungtudonvi.fPCTNNghe/@Donvitinh fPCTNNghe,
		chungtudonvi.fPCTNVuotKhung/@Donvitinh fPCTNVuotKhung,
		chungtudonvi.fNghiOm/@Donvitinh fNghiOm,
		chungtudonvi.fHSBL/@Donvitinh fHSBL,
		(chungtudonvi.fLuongChinh + chungtudonvi.fPhuCapChucVu + chungtudonvi.fPCTNNghe + chungtudonvi.fPCTNVuotKhung + chungtudonvi.fNghiOm + chungtudonvi.fHSBL)/@Donvitinh fTongQTLN,
		chungtudonvi.fDuToan/@Donvitinh fDuToan,
		chungtudonvi.fDaQuyetToan/@Donvitinh fDaQuyetToan,
		chungtudonvi.fConLai/@Donvitinh fConLai,
		chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
		chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fTongSoPhaiThuBHXH,
		chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
		chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
		(chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fTongSoPhaiThuBHYT,
		chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
		chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
		(chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongSoPhaiThuBHTN,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fTongNLD,
		(chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongNSD,
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
			ct.iNamLamViec
			,sum(isnull(ctct.iQSBQNam, 0)) iQSBQNam
			,sum(isnull(ctct.fLuongChinh, 0)) fLuongChinh
			,sum(isnull(ctct.fPCChucVu, 0)) fPhuCapChucVu
			,sum(isnull(ctct.fPCTNNghe, 0)) fPCTNNghe
			,sum(isnull(ctct.fPCTNVuotKhung, 0)) fPCTNVuotKhung
			,sum(isnull(ctct.fNghiOm, 0)) fNghiOm
			,sum(isnull(ctct.fHSBL, 0)) fHSBL
			,sum(isnull(ctct.fTongQTLN, 0)) fTongQTLN
			,sum(isnull(ctct.fDuToan, 0)) fDuToan
			,sum(isnull(ctct.fDaQuyetToan, 0)) fDaQuyetToan
			,sum(isnull(ctct.fConLai, 0)) fConLai
			,sum(isnull(ctct.fThu_BHXH_NLD, 0)) fThu_BHXH_NLD
			,sum(isnull(ctct.fThu_BHXH_NSD, 0)) fThu_BHXH_NSD
			,sum(isnull(ctct.fTongSoPhaiThuBHXH, 0)) fTongSoPhaiThuBHXH
			,sum(isnull(ctct.fThu_BHYT_NLD, 0)) fThu_BHYT_NLD
			,sum(isnull(ctct.fThu_BHYT_NSD, 0)) fThu_BHYT_NSD
			,sum(isnull(ctct.fTongSoPhaiThuBHYT, 0)) fTongSoPhaiThuBHYT
			,sum(isnull(ctct.fThu_BHTN_NLD, 0)) fThu_BHTN_NLD
			,sum(isnull(ctct.fThu_BHTN_NSD, 0)) fThu_BHTN_NSD
			,sum(isnull(ctct.fTongSoPhaiThuBHTN, 0)) fTongSoPhaiThuBHTN
			,sum(isnull(ctct.fTongCong, 0)) fTongCong
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			from
			BH_QTT_BHXH_ChungTu ct
			join
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam in (SELECT * FROM f_split(@SMonths))
			and ct.iQuyNamLoai = @ILoaiQuy
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = 2
			group by
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_MLNS,
			ctct.iID_MLNS_Cha,
			ctct.sXauNoiMa,
			ctct.sLNS
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		order by mlns.sXauNoiMa
END
;
;
;
;
GO


/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]    Script Date: 7/18/2024 1:34:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_chi]    Script Date: 7/18/2024 1:34:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_tong_hop_thu_chi_chi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_chi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_thu]    Script Date: 7/18/2024 1:34:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_thu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_thu]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_chi]    Script Date: 7/18/2024 1:34:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_chi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_chi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_bang_loi_tong_hop_don_vi_quy]    Script Date: 7/18/2024 1:34:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_giai_thich_bang_loi_tong_hop_don_vi_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_bang_loi_tong_hop_don_vi_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_bang_loi]    Script Date: 7/18/2024 1:34:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_giai_thich_bang_loi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_bang_loi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_bang_loi]    Script Date: 7/18/2024 1:34:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_bang_loi]
	@NamLamViec int,
	@Quy int,
	@LoaiQuy int,
	@MaDonVi nvarchar(500),
	@LoaiThu nvarchar(500)
AS
BEGIN
	
	if(@LoaiThu = 'ALL')
	begin
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
			and ctgt.iQuyNamLoai = @LoaiQuy
			and ctgt.iNamLamViec = @NamLamViec
			and ctgt.iID_MaDonVi = @MaDonVi
			and ctgt.iLoaiGiaiThich = 1 --loai giai thich bang loi
			and ctgt.sLoaiThu is null
	end
	else
	begin
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
			and ctgt.iQuyNamLoai = @LoaiQuy
			and ctgt.iNamLamViec = @NamLamViec
			and ctgt.iID_MaDonVi = @MaDonVi
			and ctgt.iLoaiGiaiThich = 1 --loai giai thich bang loi
			and ctgt.sLoaiThu = @LoaiThu
	end

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_bang_loi_tong_hop_don_vi_quy]    Script Date: 7/18/2024 1:34:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_bang_loi_tong_hop_don_vi_quy] 
	@NamLamViec int,
	@Quy int,
	@LoaiQuy int,
	@MaDonVis nvarchar(MAX),
	@LoaiThu nvarchar(500)
AS
BEGIN
	
	if(@LoaiThu = 'ALL')
	begin
		select distinct
			--ctgt.iID_QT_CTCT_GiaiThich,
			--ctgt.iID_QTT_BHXH_ChungTu,
			--ctgt.sNguoiTao,
			--ctgt.sNguoiSua,
			--ctgt.dNgayTao,
			--ctgt.dNgaySua,
			--ctgt.iID_MaDonVi,
			--ctgt.iNamLamViec,
			--ctgt.iQuyNam,
			--ctgt.iQuyNamLoai,
			--ctgt.sQuyNamMoTa,
			ctgt.iID_MLNS,
			ctgt.sNoiDung,
			sum(ctgt.fPhaiNop_TrongQuyNam) fPhaiNop_TrongQuyNam,
			sum(ctgt.fTruyThu_QuyNamTruoc) fTruyThu_QuyNamTruoc,
			sum(ctgt.fPhaiNop_QuyNamTruoc) fPhaiNop_QuyNamTruoc,
			sum(ctgt.fDaNop_TrongQuyNam) fDaNop_TrongQuyNam,
			sum(ctgt.fConPhaiNopTiep) fConPhaiNopTiep,
			null sKienNghi
		from BH_QTT_BHXH_CTCT_GiaiThich ctgt
		where ctgt.iQuyNam = @Quy
			and ctgt.iQuyNamLoai = @LoaiQuy
			and ctgt.iNamLamViec = @NamLamViec
			and ctgt.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVis))
			and ctgt.iLoaiGiaiThich = 1 --loai giai thich bang loi
			and ctgt.sLoaiThu is null
		group by
			ctgt.iID_MLNS,
			ctgt.sNoiDung
	end
	else
	begin
		select distinct
			ctgt.iID_MLNS,
			ctgt.sNoiDung,
			sum(ctgt.fPhaiNop_TrongQuyNam) fPhaiNop_TrongQuyNam,
			sum(ctgt.fTruyThu_QuyNamTruoc) fTruyThu_QuyNamTruoc,
			sum(ctgt.fPhaiNop_QuyNamTruoc) fPhaiNop_QuyNamTruoc,
			sum(ctgt.fDaNop_TrongQuyNam) fDaNop_TrongQuyNam,
			sum(ctgt.fConPhaiNopTiep) fConPhaiNopTiep,
			null sKienNghi
		from BH_QTT_BHXH_CTCT_GiaiThich ctgt
		where ctgt.iQuyNam = @Quy
			and ctgt.iQuyNamLoai = @LoaiQuy
			and ctgt.iNamLamViec = @NamLamViec
			and ctgt.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVis))
			and ctgt.iLoaiGiaiThich = 1 --loai giai thich bang loi
			and ctgt.sLoaiThu = @LoaiThu
		group by
			ctgt.iID_MLNS,
			ctgt.sNoiDung
	end
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_chi]    Script Date: 7/18/2024 1:34:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_chi]
	@NamLamViec int,
	@DonVis nvarchar(600),
	@Dvt int,
	@SoQuyetDinh nvarchar(200),
	@IsMillionRound bit
AS
BEGIN
	---CHI---
	select ctct.*, dv.iKhoi into TBL_DTC from BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
	left join DonVi dv on ctct.iid_MaDonVi = dv.iID_MaDonVi and dv.iNamLamViec = @NamLamViec and dv.iTrangThai = 1
	where ctct.iNamLamViec = @NamLamViec and ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) and ctct.iid_MaDonVi in (SELECT * FROM f_split(@DonVis))

	--Chi các chế độ BHXH DUTOAN
	select * into TBL_CHI_CHEDO_DUTOAN from
	(select 1 rowNum, 1 bHangCha, '1' stt, N'Chi các chế độ BHXH' sMoTa, null fTongSoChi, null fDuToan, null fHachToan
	union all
	select 2 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Ốm đau' sMoTa, null fTongSo, sum(isnull(fTongTien, 0)) fDuToan, null fHachToan
	from TBL_DTC where (sXauNoiMa like '9010001-010-011-0001%' or sXauNoiMa like '9010002-010-011-0001%') and iKhoi = 2
	union all 
	select 3 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Thai sản' sMoTa, null fTongSo, sum(isnull(fTongTien, 0)) fDuToan, null fHachToan
	from TBL_DTC where (sXauNoiMa like '9010001-010-011-0002%' or sXauNoiMa like '9010002-010-011-0002%') and iKhoi = 2
	union all 
	select 4 rowNum, 0 bHangCha, null stt, N'- Trợ cấp tai nạn lao động, BNN' sMoTa, null fTongSo, sum(isnull(fTongTien, 0)) fDuToan, null fHachToan
	from TBL_DTC where (sXauNoiMa like '9010001-010-011-0003%'  or sXauNoiMa like '9010002-010-011-0003%') and iKhoi = 2
	union all 
	select 5 rowNum, 0 bHangCha, null stt, N'- Trợ cấp hưu trí' sMoTa, null fTongSo, sum(isnull(fTongTien, 0)) fDuToan, null fHachToan
	from TBL_DTC where (sXauNoiMa like '9010001-010-011-0004%' or sXauNoiMa like '9010002-010-011-0004%') and iKhoi = 2
	union all 
	select 6 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Phục viên' sMoTa, null fTongSo, sum(isnull(fTongTien, 0)) fDuToan, null fHachToan
	from TBL_DTC where (sXauNoiMa like '9010001-010-011-0005%' or sXauNoiMa like '9010002-010-011-0005%') and iKhoi = 2
	union all 
	select 7 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Xuất ngũ' sMoTa, null fTongSo, sum(isnull(fTongTien, 0)) fDuToan, null fHachToan
	from TBL_DTC where (sXauNoiMa like '9010001-010-011-0006%' or sXauNoiMa like '9010002-010-011-0006%') and iKhoi = 2
	union all 
	select 8 rowNum, 0 bHangCha, null stt, N'- Trợ cấp thôi việc' sMoTa, null fTongSo, sum(isnull(fTongTien, 0)) fDuToan, null fHachToan
	from TBL_DTC where (sXauNoiMa like '9010001-010-011-0007%' or sXauNoiMa like '9010002-010-011-0007%') and iKhoi = 2
	union all 
	select 9 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Tử tuất' sMoTa, null fTongSo, sum(isnull(fTongTien, 0)) fDuToan, null fHachToan
	from TBL_DTC where (sXauNoiMa like '9010001-010-011-0008%' or sXauNoiMa like '9010002-010-011-0008%') and iKhoi = 2
	union all 
	select 10 rowNum, 1 bHangCha, 2 stt, N'Kinh phí quản lý BHXH, BHYT' sMoTa, null fTongSo, sum(isnull(fTongTien, 0)) fDuToan, null fHachToan
	from TBL_DTC where sXauNoiMa like '9010003%' and iKhoi = 2
	union all 
	select 11 rowNum, 1 bHangCha, 3 stt, N'Kinh phí KCB tại quân y đơn vị' sMoTa, null fTongSo, sum(isnull(fTongTien, 0)) fDuToan, null fHachToan
	from TBL_DTC where (sXauNoiMa like '9010004%' or sXauNoiMa like '9010005%') and iKhoi = 2
	union all 
	select 12 rowNum, 1 bHangCha, 4 stt, N'Kinh phí KCB tại Trường sa - DK' sMoTa, null fTongSo, sum(isnull(fTongTien, 0)) fDuToan, null fHachToan
	from TBL_DTC where (sXauNoiMa like '9010006%' or sXauNoiMa like '9010007%') and iKhoi = 2
	) chidutoan



	--Chi các chế độ BHXH hạch toán
	select * into TBL_CHI_CHEDO_HACHTOAN from
	(select 1 rowNum, 1 bHangCha, '1' stt, N'Chi các chế độ BHXH' sMoTa, null fTongSoChi, null fDuToan, null fHachToan
	union all
	select 2 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Ốm đau' sMoTa, null fTongSo, null fDuToan, sum(isnull(fTongTien, 0)) fHachToan
	from TBL_DTC where (sXauNoiMa like '9010001-010-011-0001%' or sXauNoiMa like '9010002-010-011-0001%') and iKhoi <> 2
	union all 
	select 3 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Thai sản' sMoTa, null fTongSo, null fDuToan, sum(isnull(fTongTien, 0)) fHachToan
	from TBL_DTC where (sXauNoiMa like '9010001-010-011-0002%' or sXauNoiMa like '9010002-010-011-0002%') and iKhoi <> 2
	union all 
	select 4 rowNum, 0 bHangCha, null stt, N'- Trợ cấp tai nạn lao động, BNN' sMoTa, null fTongSo, null fDuToan, sum(isnull(fTongTien, 0)) fHachToan
	from TBL_DTC where  (sXauNoiMa like '9010001-010-011-0003%'  or sXauNoiMa like '9010002-010-011-0003%') and iKhoi <> 2
	union all 
	select 5 rowNum, 0 bHangCha, null stt, N'- Trợ cấp hưu trí' sMoTa, null fTongSo, null fDuToan, sum(isnull(fTongTien, 0)) fHachToan
	from TBL_DTC where  (sXauNoiMa like '9010001-010-011-0004%' or sXauNoiMa like '9010002-010-011-0004%') and iKhoi <> 2
	union all 
	select 6 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Phục viên' sMoTa, null fTongSo, null fDuToan, sum(isnull(fTongTien, 0)) fHachToan
	from TBL_DTC where (sXauNoiMa like '9010001-010-011-0005%' or sXauNoiMa like '9010002-010-011-0005%') and iKhoi <> 2
	union all 
	select 7 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Xuất ngũ' sMoTa, null fTongSo, null fDuToan, sum(isnull(fTongTien, 0)) fHachToan
	from TBL_DTC where (sXauNoiMa like '9010001-010-011-0006%' or sXauNoiMa like '9010002-010-011-0006%') and iKhoi <> 2
	union all 
	select 8 rowNum, 0 bHangCha, null stt, N'- Trợ cấp thôi việc' sMoTa, null fTongSo, null fDuToan, sum(isnull(fTongTien, 0)) fHachToan
	from TBL_DTC where (sXauNoiMa like '9010001-010-011-0007%' or sXauNoiMa like '9010002-010-011-0007%') and iKhoi <> 2
	union all 
	select 9 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Tử tuất' sMoTa, null fTongSo, null fDuToan, sum(isnull(fTongTien, 0)) fHachToan
	from TBL_DTC where (sXauNoiMa like '9010001-010-011-0008%' or sXauNoiMa like '9010002-010-011-0008%') and iKhoi <> 2
	) chihachtoan

	if (@IsMillionRound = 1)
	begin
	select dt.rowNum, 
			dt.bHangCha, 
			dt.stt, 
			dt.sMoTa, 
			round((isnull(dt.fDuToan, 0) + isnull(ht.fHachToan, 0)) / 1000000, 0) * 1000000 / @Dvt fTongSoChi, 
			round(dt.fDuToan / 1000000, 0) * 1000000 / @Dvt fDuToan, 
			round(ht.fHachToan / 1000000, 0) * 1000000 /@Dvt fHachToan
	into TBL_DTC_RESULT
	from TBL_CHI_CHEDO_DUTOAN dt
	left join TBL_CHI_CHEDO_HACHTOAN ht on dt.rowNum = ht.rowNum

	update TBL_DTC_RESULT set fTongSoChi = (select sum(fTongSoChi) from TBL_DTC_RESULT where bHangCha = 0),
							fDuToan = (select sum(fDuToan) from TBL_DTC_RESULT where bHangCha = 0),
							fHachToan = (select sum(fHachToan) from TBL_DTC_RESULT where bHangCha = 0)
						where bHangCha = 1 and stt = 1

	--result
	select * from TBL_DTC_RESULT
	end
	else 
		begin
	select dt.rowNum, 
			dt.bHangCha, 
			dt.stt, 
			dt.sMoTa, 
			(isnull(dt.fDuToan, 0) + isnull(ht.fHachToan, 0))/@Dvt fTongSoChi, 
			dt.fDuToan/@Dvt fDuToan, 
			ht.fHachToan/@Dvt fHachToan
	into TBL_DTC_RESULT1
	from TBL_CHI_CHEDO_DUTOAN dt
	left join TBL_CHI_CHEDO_HACHTOAN ht on dt.rowNum = ht.rowNum

	update TBL_DTC_RESULT1 set fTongSoChi = (select sum(fTongSoChi) from TBL_DTC_RESULT1 where bHangCha = 0),
							fDuToan = (select sum(fDuToan) from TBL_DTC_RESULT1 where bHangCha = 0),
							fHachToan = (select sum(fHachToan) from TBL_DTC_RESULT1 where bHangCha = 0)
						where bHangCha = 1 and stt = 1

	--result
	select * from TBL_DTC_RESULT1
	end

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTC]') AND type in (N'U')) drop table TBL_DTC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_CHI_CHEDO_DUTOAN]') AND type in (N'U')) drop table TBL_CHI_CHEDO_DUTOAN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_CHI_CHEDO_HACHTOAN]') AND type in (N'U')) drop table TBL_CHI_CHEDO_HACHTOAN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTC_RESULT]') AND type in (N'U')) drop table TBL_DTC_RESULT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTC_RESULT1]') AND type in (N'U')) drop table TBL_DTC_RESULT1;

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_thu]    Script Date: 7/18/2024 1:34:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_thu]
	@NamLamViec int,
	@DonVis nvarchar(600),
	@Dvt int,
	@SoQuyetDinh nvarchar(200),
	@IsMillionRound bit
AS
BEGIN
	---THU---
	--Dữ liệu phân bổ dự toán thu BHXH
	select ctct.*, dv.iKhoi into TBL_DTT from BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu ct on ctct.iID_DTT_BHXH_ChungTu = ct.iID_DTT_BHXH_PhanBo_ChungTu
	left join DonVi dv on ctct.iid_MaDonVi = dv.iID_MaDonVi and dv.iNamLamViec = @NamLamViec and dv.iTrangThai = 1
	where ctct.iNamLamViec = @NamLamViec and ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) and ctct.iid_MaDonVi in (SELECT * FROM f_split(@DonVis))
	--Thu BHXH
	select * into TBL_THU_BHXH from
	(select 1 rowNum, 1 bHangCha, '1' stt, N'Thu BHXH' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai
	
	union all
	
	select 2 rowNum, 0 bHangCha, null stt, N'Đơn vị dự toán' sMoTa, (sum(isnull(fBHXH_NLD, 0)) + sum(isnull(fBHXH_NSD, 0))) fTongSo, sum(fBHXH_NLD) fNLD, sum(fBHXH_NSD) fNSD, null sLoai
	from TBL_DTT where iKhoi = 2 -- sXauNoiMa like '9020001%'
	
	union all
	
	select 3 rowNum, 0 bHangCha, null stt, N'Đơn vị hạch toán' sMoTa, (sum(isnull(fBHXH_NLD, 0)) + sum(isnull(fBHXH_NSD, 0))) fTongSo, sum(fBHXH_NLD) fNLD, sum(fBHXH_NSD) fNSD, null sLoai
	from TBL_DTT where iKhoi <> 2 --sXauNoiMa like '9020002%'
	) thubhxh

	update TBL_THU_BHXH set fTongSo = (select sum(fTongSo) from TBL_THU_BHXH where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHXH where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHXH where bHangCha = 0)
						where bHangCha = 1

	--Thu BHTN
	select * into TBL_THU_BHTN from
	(select 4 rowNum, 1 bHangCha, '2' stt, N'Thu BHTN' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai
	
	union all
	
	select 5 rowNum, 0 bHangCha, null stt, N'Đơn vị dự toán' sMoTa, (sum(isnull(fBHTN_NLD, 0)) + sum(isnull(fBHTN_NSD, 0))) fTongSo, sum(fBHTN_NLD) fNLD, sum(fBHTN_NSD) fNSD, null sLoai
	from TBL_DTT where iKhoi = 2 --sXauNoiMa like '9020001%'
	
	union all
	
	select 6 rowNum, 0 bHangCha, null stt, N'Đơn vị hạch toán' sMoTa, (sum(isnull(fBHTN_NLD, 0)) + sum(isnull(fBHTN_NSD, 0))) fTongSo, sum(fBHTN_NLD) fNLD, sum(fBHTN_NSD) fNSD, null sLoai
	from TBL_DTT where iKhoi <> 2 --sXauNoiMa like '9020002%'
	) thubhtn

	update TBL_THU_BHTN set fTongSo = (select sum(fTongSo) from TBL_THU_BHTN where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHTN where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHTN where bHangCha = 0)
						where bHangCha = 1

	--Thu BHYT quân nhân
	select * into TBL_THU_BHYT_QN from
	(select 7 rowNum, 1 bHangCha, '3' stt, N'Thu BHYT quân nhân' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai
	
	union all
	
	select 8 rowNum, 0 bHangCha, null stt, N'Đơn vị dự toán' sMoTa, (sum(isnull(fBHYT_NLD, 0)) + sum(isnull(fBHYT_NSD, 0))) fTongSo, sum(fBHYT_NLD) fNLD, sum(fBHYT_NSD) fNSD, null sLoai
	from TBL_DTT where (sXauNoiMa like '9020001-010-011-0001%' or sXauNoiMa like '9020002-010-011-0001%') and iKhoi = 2
	
	union all
	
	select 9 rowNum, 0 bHangCha, null stt, N'Đơn vị hạch toán' sMoTa, (sum(isnull(fBHYT_NLD, 0)) + sum(isnull(fBHYT_NSD, 0))) fTongSo, sum(fBHYT_NLD) fNLD, sum(fBHYT_NSD) fNSD, null sLoai
	from TBL_DTT where (sXauNoiMa like '9020001-010-011-0001%' or sXauNoiMa like '9020002-010-011-0001%') and iKhoi <> 2
	) thubhytquannhan

	update TBL_THU_BHYT_QN set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_QN where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_QN where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_QN where bHangCha = 0)
						where bHangCha = 1

	--Thu BHYT người lao động
	select * into TBL_THU_BHYT_NLD from
	(select 10 rowNum, 1 bHangCha, '4' stt, N'Thu BHYT người lao động' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai
	
	union all
	
	select 11 rowNum, 0 bHangCha, null stt, N'Đơn vị dự toán' sMoTa, (sum(isnull(fBHYT_NLD, 0)) + sum(isnull(fBHYT_NSD, 0))) fTongSo, sum(fBHYT_NLD) fNLD, sum(fBHYT_NSD) fNSD, null sLoai
	from TBL_DTT where (sXauNoiMa like '9020001-010-011-0002%' or sXauNoiMa like '9020001-010-011-0002%') and iKhoi = 2
	
	union all
	
	select 12 rowNum, 0 bHangCha, null stt, N'Đơn vị hạch toán' sMoTa, (sum(isnull(fBHYT_NLD, 0)) + sum(isnull(fBHYT_NSD, 0))) fTongSo, sum(fBHYT_NLD) fNLD, sum(fBHYT_NSD) fNSD, null sLoai
	from TBL_DTT where (sXauNoiMa like '9020001-010-011-0002%' or sXauNoiMa like '9020001-010-011-0002%') and iKhoi <> 2) thubhytnld

	update TBL_THU_BHYT_NLD set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_NLD where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_NLD where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_NLD where bHangCha = 0)
						where bHangCha = 1
	
	--Dữ liệu phân bổ dự toán thu mua BHYT
	select ctct.*, dv.iKhoi into TBL_DTTM from BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
	join  BH_DTTM_BHYT_ThanNhan_PhanBo ct on ctct.iID_DTTM_BHYT_ThanNhan_PhanBo = ct.iID_DTTM_BHYT_ThanNhan_PhanBo
	left join DonVi dv on ctct.iid_MaDonVi = dv.iID_MaDonVi and dv.iNamLamViec = @NamLamViec and dv.iTrangThai = 1
	where ctct.iNamLamViec = @NamLamViec and ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) and ctct.iid_MaDonVi in (SELECT * FROM f_split(@DonVis))
	--Thu BHYT người lao động
	select * into TBL_THU_BHYT_TN from
	(select 13 rowNum, 1 bHangCha, '5' stt, N'Thu BHYT thân nhân' sMoTa, null fTongSo, null fNLD, null fNSD, N'BHYT_THANNHAN' sLoai
	
	union all

	select 14 rowNum, 1 bHangCha, 'a' stt, N'Quân nhân' sMoTa, null fTongSo, null fNLD, null fNSD, N'BHYT_THANNHAN_QN' sLoai
	
	union all
	
	select 15 rowNum, 0 bHangCha, null stt, N'- Đơn vị dự toán' sMoTa, sum(isnull(fDuToan, 0)) fTongSo, null fNLD, sum(isnull(fDuToan, 0)) fNSD, N'BHYT_THANNHAN_QN' sLoai
	from TBL_DTTM where (sXauNoiMa like '9030001-010-011-0000%' or sXauNoiMa like '9030001-010-011-0001%') and iKhoi = 2
	
	union all
	
	select 16 rowNum, 0 bHangCha, null stt, N'- Đơn vị hạch toán' sMoTa, sum(isnull(fDuToan, 0)) fTongSo, null fNLD, sum(isnull(fDuToan, 0)) fNSD, N'BHYT_THANNHAN_QN' sLoai
	from TBL_DTTM where (sXauNoiMa like '9030001-010-011-0000%' or sXauNoiMa like '9030001-010-011-0001%') and iKhoi <> 2
	
	union all
	
	select 17 rowNum, 1 bHangCha, 'b' stt, N'Công nhân, VCQP' sMoTa, null fTongSo, null fNLD, null fNSD, N'BHYT_THANNHAN_VCQP' sLoai
	
	union all
	
	select 18 rowNum, 0 bHangCha, null stt, N'- Đơn vị dự toán' sMoTa, sum(isnull(fDuToan, 0)) fTongSo, null fNLD, sum(isnull(fDuToan, 0)) fNSD, N'BHYT_THANNHAN_VCQP' sLoai
	from TBL_DTTM where (sXauNoiMa like '9030002-010-011-0000%' or sXauNoiMa like '9030002-010-011-0001%') and iKhoi = 2
	
	union all
	
	select 19 rowNum, 0 bHangCha, null stt, N'- Đơn vị hạch toán' sMoTa, sum(isnull(fDuToan, 0)) fTongSo, null fNLD, sum(isnull(fDuToan, 0)) fNSD, N'BHYT_THANNHAN_VCQP' sLoai
	from TBL_DTTM where (sXauNoiMa like '9030002-010-011-0000%' or sXauNoiMa like '9030002-010-011-0001%') and iKhoi <> 2
	) thubhytnld

	update TBL_THU_BHYT_TN set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_QN'),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_QN'),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_QN')
						where bHangCha = 1 and sLoai = 'BHYT_THANNHAN_QN'

	update TBL_THU_BHYT_TN set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_VCQP'),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_VCQP'),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_VCQP')
						where bHangCha = 1 and sLoai = 'BHYT_THANNHAN_VCQP'

	update TBL_THU_BHYT_TN set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_TN where bHangCha = 1 and sLoai <> 'BHYT_THANNHAN'),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_TN where bHangCha = 1 and sLoai <> 'BHYT_THANNHAN'),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_TN where bHangCha = 1 and sLoai <> 'BHYT_THANNHAN')
						where bHangCha = 1 and sLoai = 'BHYT_THANNHAN'

	select * into TBL_THU from
	(select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHXH
	union all
	select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHTN
	union all
	select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHYT_QN
	union all
	select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHYT_NLD
	union all
	select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHYT_TN) tblthu

	--Result
	if (@IsMillionRound = 1)
	select
	rowNum, 
	bHangCha, 
	stt, 
	sMoTa, 
	round(fTongSo / 1000000, 0) * 1000000 / @Dvt fTongSo, 
	round(fNLD / 1000000, 0) * 1000000 / @Dvt fNLD, 
	round(fNSD / 1000000, 0) * 1000000 / @Dvt fNSD 
	from TBL_THU
	else 
	select
	rowNum, 
	bHangCha, 
	stt, 
	sMoTa, 
	fTongSo/@Dvt fTongSo, 
	fNLD/@Dvt fNLD, 
	fNSD/@Dvt fNSD
	from TBL_THU

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTT]') AND type in (N'U')) drop table TBL_DTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHXH]') AND type in (N'U')) drop table TBL_THU_BHXH;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHTN]') AND type in (N'U')) drop table TBL_THU_BHTN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHYT_QN]') AND type in (N'U')) drop table TBL_THU_BHYT_QN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHYT_NLD]') AND type in (N'U')) drop table TBL_THU_BHYT_NLD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTTM]') AND type in (N'U')) drop table TBL_DTTM;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHYT_TN]') AND type in (N'U')) drop table TBL_THU_BHYT_TN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU]') AND type in (N'U')) drop table TBL_THU;

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_chi]    Script Date: 7/18/2024 1:34:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_chi]
	@NamLamViec int,
	@DonVis nvarchar(600),
	@SoQuyetDinh nvarchar(200),
	@DVT int,
	@NgayQuyetDinh nvarchar(200),
	@IsMillionRound bit
AS
BEGIN
	---CHI---
	select ctct.* into TBL_DTC from BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
	where ctct.iNamLamViec = @NamLamViec 
		and ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
		and ctct.iid_MaDonVi in (SELECT * FROM f_split(@DonVis))
		and Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))

	
	--- khoi du toan 2
	select A.*
	 into #tempKhoiDuToan
	 from TBL_DTC A
	left join DonVi B on A.iID_MaDonVi=B.iID_MaDonVi and A.iNamLamViec=b.iNamLamViec
	where B.iKhoi = 2

	--Chi các chế độ BHXH DUTOAN
	select * into TBL_CHI_CHEDO_DUTOAN from
	(select 1 rowNum, 1 bHangCha, '1' stt, N'Chi các chế độ BHXH' sMoTa, null fTongSoChi, null fDuToan, null fHachToan
	union all
	select 2 rowNum,
			0 bHangCha, 
			null stt, 
			N'- Trợ cấp Ốm đau' sMoTa, 
			null fTongSo, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fDuToan, 
			null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010001-010-011-0001%' or sXauNoiMa like '9010002-010-011-0001%'
	union all 
	select 3 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Trợ cấp Thai sản' sMoTa, 
			null fTongSo, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fDuToan, 
			null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010001-010-011-0002%' or sXauNoiMa like '9010002-010-011-0002%'
	union all 
	select 4 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Trợ cấp tai nạn lao động, BNN' sMoTa, 
			null fTongSo, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fDuToan, 
			null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010001-010-011-0003%' or sXauNoiMa like '9010002-010-011-0003%'
	union all 
	select 5 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Trợ cấp hưu trí' sMoTa, 
			null fTongSo, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fDuToan, 
			null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010001-010-011-0004%' or sXauNoiMa like '9010002-010-011-0004%'
	union all 
	select 6 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Trợ cấp Phục viên' sMoTa,
			null fTongSo, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fDuToan, 
			null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010001-010-011-0005%' or sXauNoiMa like '9010002-010-011-0005%'
	union all 
	select 7 rowNum, 
			0 bHangCha, 
			null stt,
			N'- Trợ cấp Xuất ngũ' sMoTa,
			null fTongSo,
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fDuToan,
			null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010001-010-011-0006%' or sXauNoiMa like '9010002-010-011-0006%'
	union all 
	select 8 rowNum,
			0 bHangCha, 
			null stt, 
			N'- Trợ cấp thôi việc' sMoTa, 
			null fTongSo, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fDuToan,
			null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010001-010-011-0007%' or sXauNoiMa like '9010002-010-011-0007%'
	union all 
	select 9 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Trợ cấp Tử tuất' sMoTa, 
			null fTongSo, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fDuToan, 
			null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010001-010-011-0008%' or sXauNoiMa like '9010002-010-011-0008%'
	union all 
	select 10 rowNum, 
			1 bHangCha, 
			2 stt, 
			N'Kinh phí quản lý BHXH, BHYT' sMoTa, 
			null fTongSo, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fDuToan, 
			null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010003%' 
	union all 
	select 11 rowNum, 
			1 bHangCha, 
			3 stt, 
			N'Kinh phí KCB tại quân y đơn vị' sMoTa,
			null fTongSo,
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fDuToan, 
			null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010004%' or sXauNoiMa like '9010005%'
	union all 
	select 12 rowNum,
			1 bHangCha, 
			4 stt, 
			N'Kinh phí KCB tại Trường sa - DK' sMoTa, 
			null fTongSo, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fDuToan, 
			null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010006%' or sXauNoiMa like '9010007%'
	) chidutoan

	--- khoi du toan 1
	select A.*
	 into #tempKhoiHoachToan
	 from TBL_DTC A
	left join DonVi B on A.iID_MaDonVi=B.iID_MaDonVi and A.iNamLamViec=b.iNamLamViec
	where B.iKhoi <> 2


	--Chi các chế độ BHXH hạch toán
	select * into TBL_CHI_CHEDO_HACHTOAN from
	(select 1 rowNum, 1 bHangCha, '1' stt, N'Chi các chế độ BHXH' sMoTa, null fTongSoChi, null fDuToan, null fHachToan
	union all
	select 2 rowNum, 
			0 bHangCha, 
			null stt,
			N'- Trợ cấp Ốm đau' sMoTa, 
			null fTongSo, 
			null fDuToan, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010001-010-011-0001%' or sXauNoiMa like '9010002-010-011-0001%'
	union all 
	select 3 rowNum, 
			0 bHangCha, 
			null stt,
			N'- Trợ cấp Thai sản' sMoTa, 
			null fTongSo, 
			null fDuToan,
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010001-010-011-0002%' or sXauNoiMa like '9010002-010-011-0002%'
	union all 
	select 4 rowNum,
			0 bHangCha, 
			null stt, 
			N'- Trợ cấp tai nạn lao động, BNN' sMoTa,
			null fTongSo, 
			null fDuToan,
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010001-010-011-0003%' or sXauNoiMa like '9010002-010-011-0003%'
	union all 
	select 5 rowNum, 
			0 bHangCha,
			null stt, 
			N'- Trợ cấp hưu trí' sMoTa,
			null fTongSo, 
			null fDuToan,
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010001-010-011-0004%' or sXauNoiMa like '9010002-010-011-0004%'
	union all 
	select 6 rowNum,
			0 bHangCha, 
			null stt,
			N'- Trợ cấp Phục viên' sMoTa,
			null fTongSo,
			null fDuToan, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010001-010-011-0005%' or sXauNoiMa like '9010002-010-011-0005%'
	union all 
	select 7 rowNum,
			0 bHangCha,
			null stt,
			N'- Trợ cấp Xuất ngũ' sMoTa,
			null fTongSo, 
			null fDuToan, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010001-010-011-0006%' or sXauNoiMa like '9010002-010-011-0006%'
	union all 
	select 8 rowNum,
			0 bHangCha, 
			null stt,
			N'- Trợ cấp thôi việc' sMoTa,
			null fTongSo, 
			null fDuToan,
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010001-010-011-0007%' or sXauNoiMa like '9010002-010-011-0007%'
	union all 
	select 9 rowNum, 
			0 bHangCha,
			null stt,
			N'- Trợ cấp Tử tuất' sMoTa, 
			null fTongSo, 
			null fDuToan,
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010001-010-011-0008%' or sXauNoiMa like '9010002-010-011-0008%'
	union all 
	select 10 rowNum,
			0 bHangCha, 
			2 stt,
			N'Kinh phí quản lý BHXH, BHYT' sMoTa, 
			null fTongSo, 
			null fDuToan,
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010003%' 
	union all 
	select 11 rowNum,
			0 bHangCha, 
			3 stt, 
			N'Kinh phí KCB tại quân y đơn vị' sMoTa,
			null fTongSo,
			null fDuToan,
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010004%' or sXauNoiMa like '9010005%'
	union all 
	select 12 rowNum, 
			0 bHangCha, 
			4 stt, 
			N'Kinh phí KCB tại Trường sa - DK' sMoTa, 
			null fTongSo,
			null fDuToan, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010006%' or sXauNoiMa like '9010007%'
	) chihachtoan

	--if (@IsMillionRound = 0)
	--begin
	select dt.rowNum, 
			dt.bHangCha, 
			dt.stt, 
			dt.sMoTa, 
			(isnull(dt.fDuToan, 0) + isnull(ht.fHachToan, 0))/@DVT fTongSoChi, 
			dt.fDuToan/@DVT fDuToan, 
			ht.fHachToan/@DVT fHachToan
	into TBL_DTC_RESULT
	from TBL_CHI_CHEDO_DUTOAN dt
	left join TBL_CHI_CHEDO_HACHTOAN ht on dt.rowNum = ht.rowNum

	update TBL_DTC_RESULT set fTongSoChi = (select sum(fTongSoChi) from TBL_DTC_RESULT where bHangCha = 0),
							fDuToan = (select sum(fDuToan) from TBL_DTC_RESULT where bHangCha = 0),
							fHachToan = (select sum(fHachToan) from TBL_DTC_RESULT where bHangCha = 0)
						where bHangCha = 1 and stt = 1

	--result
	select * from TBL_DTC_RESULT
	--end
	--else 
	--	begin
	--select dt.rowNum, 
	--		dt.bHangCha, 
	--		dt.stt, 
	--		dt.sMoTa, 
	--		round((isnull(dt.fDuToan, 0) + isnull(ht.fHachToan, 0)) / 1000000, 0) * 1000000 /@DVT fTongSoChi, 
	--		round(dt.fDuToan / 1000000, 0) * 1000000 / @DVT fDuToan, 
	--		round(dt.fHachToan / 1000000, 0) * 1000000 / @DVT fHachToan
	--into TBL_DTC_RESULT1
	--from TBL_CHI_CHEDO_DUTOAN dt
	--left join TBL_CHI_CHEDO_HACHTOAN ht on dt.rowNum = ht.rowNum

	--update TBL_DTC_RESULT1 set fTongSoChi = (select sum(fTongSoChi) from TBL_DTC_RESULT1 where bHangCha = 0),
	--						fDuToan = (select sum(fDuToan) from TBL_DTC_RESULT1 where bHangCha = 0),
	--						fHachToan = (select sum(fHachToan) from TBL_DTC_RESULT1 where bHangCha = 0)
	--					where bHangCha = 1 and stt = 1

	----result
	--select * from TBL_DTC_RESULT1
	--end

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTC]') AND type in (N'U')) drop table TBL_DTC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_CHI_CHEDO_DUTOAN]') AND type in (N'U')) drop table TBL_CHI_CHEDO_DUTOAN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_CHI_CHEDO_HACHTOAN]') AND type in (N'U')) drop table TBL_CHI_CHEDO_HACHTOAN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTC_RESULT]') AND type in (N'U')) drop table TBL_DTC_RESULT;
	--IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTC_RESULT1]') AND type in (N'U')) drop table TBL_DTC_RESULT1;

END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]    Script Date: 7/18/2024 1:34:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]
	@NamLamViec int,
	@DonVis nvarchar(600),
	@SoQuyetDinh nvarchar(200),
	@DVT int,
	@NgayQuyetDinh nvarchar(200),
	@IsMillionRound bit
AS
BEGIN
	---THU---
	--Dữ liệu phân bổ dự toán thu BHXH
	select ctct.*, dv.iKhoi into TBL_DTT from BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu ct on ctct.iID_DTT_BHXH_ChungTu = ct.iID_DTT_BHXH_PhanBo_ChungTu
	left join DonVi dv on ctct.iid_MaDonVi = dv.iID_MaDonVi and dv.iNamLamViec = @NamLamViec and dv.iTrangThai = 1
	where ctct.iNamLamViec = @NamLamViec 
		and ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
		and ctct.iid_MaDonVi in (SELECT * FROM f_split(@DonVis))
		and Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
	--Thu BHXH
	select * into TBL_THU_BHXH from
	(select 1 rowNum, 1 bHangCha, '1' stt, N'Thu BHXH' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai

	union all

	select 2 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị dự toán' sMoTa, 
			ROUND((sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHXH_NLD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHXH_NLD, 0) END) + sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHXH_NSD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHXH_NSD, 0) END)),0) fTongSo,
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHXH_NLD/1000000, 0)* 1000000 ELSE fBHXH_NLD END),0) fNLD, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHXH_NSD/1000000, 0)* 1000000 ELSE fBHXH_NSD END), 0) fNSD, 
			null sLoai
	from TBL_DTT where iKhoi = 2 --sXauNoiMa like '9020001%'

	union all

	select 3 rowNum, 
		   0 bHangCha, 
		   null stt, 
		   N'- Đơn vị hạch toán' sMoTa, 
		   ROUND((sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHXH_NLD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHXH_NLD, 0) END) + sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHXH_NSD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHXH_NSD, 0) END)),0) fTongSo, 
		   ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHXH_NLD/1000000, 0)* 1000000 ELSE fBHXH_NLD END), 0) fNLD, 
		   ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHXH_NSD/1000000, 0)* 1000000 ELSE fBHXH_NSD END),0) fNSD,
		   null sLoai
	from TBL_DTT where iKhoi <> 2 --sXauNoiMa like '9020002%'
	) thubhxh

	update TBL_THU_BHXH set fTongSo = (select sum(fTongSo) from TBL_THU_BHXH where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHXH where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHXH where bHangCha = 0)
						where bHangCha = 1

	--Thu BHTN
	select * into TBL_THU_BHTN from
	(select 4 rowNum, 1 bHangCha, '2' stt, N'Thu BHTN' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai

	union all

	select 5 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị dự toán' sMoTa, 
			ROUND((sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHTN_NLD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHTN_NLD, 0) END) + sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHTN_NSD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHTN_NSD, 0) END)),0) fTongSo, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHTN_NLD/1000000, 0)* 1000000 ELSE fBHTN_NLD END),0) fNLD, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHTN_NSD/1000000, 0)* 1000000 ELSE fBHTN_NSD END),0) fNSD, 
			null sLoai
	from TBL_DTT where iKhoi = 2 --sXauNoiMa like '9020001%'

	union all

	select 6 rowNum, 
			0 bHangCha,
			null stt, 
			N'- Đơn vị hạch toán' sMoTa, 
			ROUND((sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHTN_NLD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHTN_NLD, 0) END) + sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHTN_NSD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHTN_NSD, 0) END)),0) fTongSo, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHTN_NLD/1000000, 0)* 1000000 ELSE fBHTN_NLD END),0) fNLD, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHTN_NSD/1000000, 0)* 1000000 ELSE fBHTN_NSD END),0) fNSD,  
			null sLoai
	from TBL_DTT where iKhoi <> 2 --sXauNoiMa like '9020002%'
	) thubhtn

	update TBL_THU_BHTN set fTongSo = (select sum(fTongSo) from TBL_THU_BHTN where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHTN where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHTN where bHangCha = 0)
						where bHangCha = 1

	--Thu BHYT quân nhân
	select * into TBL_THU_BHYT_QN from
	(select 7 rowNum, 1 bHangCha, '3' stt, N'Thu BHYT quân nhân' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai

	union all
	
	select 8 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị dự toán' sMoTa, 
			ROUND((sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHYT_NLD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHYT_NLD, 0) END) + sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHYT_NSD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHYT_NSD, 0) END)),0) fTongSo, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHYT_NLD/1000000, 0)* 1000000 ELSE fBHYT_NLD END),0) fNLD, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHYT_NSD/1000000, 0)* 1000000 ELSE fBHYT_NSD END),0) fNSD, 
			null sLoai
	from TBL_DTT where (sXauNoiMa like '9020001-010-011-0001%' or sXauNoiMa like '9020002-010-011-0001%')
						and iKhoi = 2
	union all
	
	select 9 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị hạch toán' sMoTa, 
			ROUND((sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHYT_NLD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHYT_NLD, 0) END) + sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHYT_NSD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHYT_NSD, 0) END)),0) fTongSo, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHYT_NLD/1000000, 0)* 1000000 ELSE fBHYT_NLD END),0) fNLD, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHYT_NSD/1000000, 0)* 1000000 ELSE fBHYT_NSD END),0) fNSD, 
			null sLoai
	from TBL_DTT where (sXauNoiMa like '9020001-010-011-0001%' or sXauNoiMa like '9020002-010-011-0001%')
						and iKhoi <> 2) thubhytquannhan

	update TBL_THU_BHYT_QN set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_QN where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_QN where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_QN where bHangCha = 0)
	where bHangCha = 1

	--Thu BHYT người lao động
	select * into TBL_THU_BHYT_NLD from
	(select 10 rowNum, 1 bHangCha, '4' stt, N'Thu BHYT người lao động' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai
	
	union all
	
	select 11 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị dự toán' sMoTa, 
			ROUND((sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHYT_NLD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHYT_NLD, 0) END) + sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHYT_NSD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHYT_NSD, 0) END)),0) fTongSo, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHYT_NLD/1000000, 0)* 1000000 ELSE fBHYT_NLD END),0) fNLD, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHYT_NSD/1000000, 0)* 1000000 ELSE fBHYT_NSD END),0) fNSD,
			null sLoai
	from TBL_DTT where (sXauNoiMa like '9020001-010-011-0002%' or sXauNoiMa like '9020002-010-011-0002%')
						and iKhoi = 2
	union all
	
	select 12 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị hạch toán' sMoTa, 
			ROUND((sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHYT_NLD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHYT_NLD, 0) END) + sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHYT_NSD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHYT_NSD, 0) END)),0) fTongSo, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHYT_NLD/1000000, 0)* 1000000 ELSE fBHYT_NLD END),0) fNLD, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHYT_NSD/1000000, 0)* 1000000 ELSE fBHYT_NSD END),0) fNSD,
			null sLoai
	from TBL_DTT where (sXauNoiMa like '9020001-010-011-0002%' or sXauNoiMa like '9020002-010-011-0002%')
						and iKhoi <> 2) thubhytnld

	update TBL_THU_BHYT_NLD set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_NLD where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_NLD where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_NLD where bHangCha = 0)
						where bHangCha = 1
	
	--Dữ liệu phân bổ dự toán thu mua BHYT
	select ctct.*, dv.iKhoi into TBL_DTTM from BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
	join  BH_DTTM_BHYT_ThanNhan_PhanBo ct on ctct.iID_DTTM_BHYT_ThanNhan_PhanBo = ct.iID_DTTM_BHYT_ThanNhan_PhanBo
	left join DonVi dv on ctct.iid_MaDonVi = dv.iID_MaDonVi and dv.iNamLamViec = @NamLamViec and dv.iTrangThai = 1
	where ctct.iNamLamViec = @NamLamViec and ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) and ctct.iid_MaDonVi in (SELECT * FROM f_split(@DonVis))
	--Thu BHYT người lao động
	select * into TBL_THU_BHYT_TN from
	(select 13 rowNum, 1 bHangCha, '5' stt, N'Thu BHYT thân nhân' sMoTa, null fTongSo, null fNLD, null fNSD, N'BHYT_THANNHAN' sLoai
	
	union all
	
	select 14 rowNum, 1 bHangCha, 'a' stt, N'Quân nhân' sMoTa, null fTongSo, null fNLD, null fNSD, N'BHYT_THANNHAN_QN' sLoai
	union all
	select 15 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị dự toán' sMoTa, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fDuToan/1000000, 0)* 1000000 ELSE fDuToan END),0) fTongSo, 
			null fNLD, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fDuToan/1000000, 0)* 1000000 ELSE fDuToan END),0) fNSD, 
			N'BHYT_THANNHAN_QN' sLoai
	from TBL_DTTM where (sXauNoiMa like '9030001-010-011-0001%' or sXauNoiMa like '9030001-010-011-0002%')
						and iKhoi = 2
	union all
	select 16 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị hạch toán' sMoTa, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fDuToan/1000000, 0)* 1000000 ELSE fDuToan END),0) fTongSo, 
			null fNLD, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fDuToan/1000000, 0)* 1000000 ELSE fDuToan END),0) fNSD, 
			N'BHYT_THANNHAN_QN' sLoai
	from TBL_DTTM where (sXauNoiMa like '9030001-010-011-0001%' or sXauNoiMa like '9030001-010-011-0002%')
						and iKhoi <> 2
	union all

	select 17 rowNum,
			1 bHangCha, 
			'b' stt, 
			N'Công nhân, VCQP' sMoTa, 
			null fTongSo, 
			null fNLD, 
			null fNSD, 
			N'BHYT_THANNHAN_VCQP' sLoai

	union all

	select 18 rowNum,
			0 bHangCha, 
			null stt, 
			N'- Đơn vị dự toán' sMoTa, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fDuToan/1000000, 0)* 1000000 ELSE fDuToan END),0) fTongSo, 
			null fNLD, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fDuToan/1000000, 0)* 1000000 ELSE fDuToan END),0) fNSD, 
			N'BHYT_THANNHAN_VCQP' sLoai
	from TBL_DTTM where (sXauNoiMa like '9030002-010-011-0000%' or sXauNoiMa like '9030002-010-011-0001%')
						and iKhoi = 2
	union all

	select 19 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị hạch toán' sMoTa,
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fDuToan/1000000, 0)* 1000000 ELSE fDuToan END),0) fTongSo,
			null fNLD,
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fDuToan/1000000, 0)* 1000000 ELSE fDuToan END),0) fNSD,
			N'BHYT_THANNHAN_VCQP' sLoai
	from TBL_DTTM where (sXauNoiMa like '9030002-010-011-0000%' or sXauNoiMa like '9030002-010-011-0001%')
						and iKhoi <> 2
	) thubhytnld

	update TBL_THU_BHYT_TN set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_QN'),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_QN'),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_QN')
						where bHangCha = 1 and sLoai = 'BHYT_THANNHAN_QN'

	update TBL_THU_BHYT_TN set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_VCQP'),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_VCQP'),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_VCQP')
						where bHangCha = 1 and sLoai = 'BHYT_THANNHAN_VCQP'

	update TBL_THU_BHYT_TN set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_TN where bHangCha = 1 and sLoai <> 'BHYT_THANNHAN'),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_TN where bHangCha = 1 and sLoai <> 'BHYT_THANNHAN'),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_TN where bHangCha = 1 and sLoai <> 'BHYT_THANNHAN')
						where bHangCha = 1 and sLoai = 'BHYT_THANNHAN'

	select * into TBL_THU from
	(select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHXH
	union all
	select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHTN
	union all
	select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHYT_QN
	union all
	select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHYT_NLD
	union all
	select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHYT_TN) tblthu

	--Result
	--if (@IsMillionRound = 1)
	--select
	--rowNum, 
	--bHangCha, 
	--stt, 
	--sMoTa, 
	--round(fTongSo / 1000000, 0) * 1000000 /@DVT fTongSo, 
	--round(fNLD / 1000000, 0) * 1000000 /@DVT fNLD, 
	--round(fNSD / 1000000, 0) * 1000000 /@DVT fNSD
	--from TBL_THU
	--else
	select
	rowNum, 
	bHangCha, 
	stt, 
	sMoTa, 
	fTongSo/@DVT fTongSo, 
	fNLD/@DVT fNLD, 
	fNSD/@DVT fNSD
	from TBL_THU

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTT]') AND type in (N'U')) drop table TBL_DTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHXH]') AND type in (N'U')) drop table TBL_THU_BHXH;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHTN]') AND type in (N'U')) drop table TBL_THU_BHTN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHYT_QN]') AND type in (N'U')) drop table TBL_THU_BHYT_QN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHYT_NLD]') AND type in (N'U')) drop table TBL_THU_BHYT_NLD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTTM]') AND type in (N'U')) drop table TBL_DTTM;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHYT_TN]') AND type in (N'U')) drop table TBL_THU_BHYT_TN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU]') AND type in (N'U')) drop table TBL_THU;

END
;
;
;
;
;
;
;
GO


/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_giai_thich_che_do_om_dau]    Script Date: 7/19/2024 2:03:27 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_qtcq_giai_thich_che_do_om_dau]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_giai_thich_che_do_om_dau]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_giai_thich_che_do_om_dau]    Script Date: 7/19/2024 2:03:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_giai_thich_che_do_om_dau]
	@NamLamViec int,
	@DVT int,
	@Quy int,
	@DonVi nvarchar(200)
AS
BEGIN
	---Bệnh dài ngày
	select gt.* into TBL_BenhDaiNgay from BH_QTC_Quy_CTCT_GiaiThichTroCap gt
	where (sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02' or sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02')
		and iNamLamViec = @NamLamViec and iQuy = @Quy and iiD_MaDonVi in (SELECT * FROM f_split(@DonVi))

	--Sy quan
	select * into TBL_BDN_SiQuan from
	(select 1 bHangCha, N'A' LoaiTC, 1 RowNum, N'A' STT, null DoiTuong, null LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Thuộc Danh mục bệnh cần chữa trị dài ngày' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 1 bHangCha, N'A' LoaiTC, 1 RowNum, N'I' STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Sỹ quan' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 0 bHangCha, 'A' LoaiTC,  1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, sum(isnull(iSoNgayHuong, 0)) iSoNgayHuong, sum(isnull(fSoTien, 0)) fSoTien, iiD_MaPhanHo, 0 bHasData
	from TBL_BenhDaiNgay
	 where sMaCapBac like '1%' 
	 group by sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iiD_MaPhanHo) sq

	 if (select count(1) from TBL_BDN_SiQuan) > 2
		update TBL_BDN_SiQuan set bHasData = 1

	 --QNCN
	select * into TBL_BDN_QNCN from
	(select 1 bHangCha, N'A' LoaiTC, 1 RowNum, N'A' STT, null DoiTuong, null LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Thuộc Danh mục bệnh cần chữa trị dài ngày' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 1 bHangCha, N'A' LoaiTC, 2 RowNum, N'I' STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Quân nhân chuyên nghiệp' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 0 bHangCha, 'A' LoaiTC,  2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, sum(isnull(iSoNgayHuong, 0)) iSoNgayHuong, sum(isnull(fSoTien, 0)) fSoTien, iiD_MaPhanHo, 0 bHasData
	from TBL_BenhDaiNgay
	 where sMaCapBac like '2%' 
	 group by sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iiD_MaPhanHo) qncn

	 if (select count(1) from TBL_BDN_QNCN) > 2
		update TBL_BDN_QNCN set bHasData = 1

	 --HSQ_BS
	select * into TBL_BDN_HSQ_BS from
	(select 1 bHangCha, N'A' LoaiTC, 1 RowNum, N'A' STT, null DoiTuong, null LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Thuộc Danh mục bệnh cần chữa trị dài ngày' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 1 bHangCha, N'A' LoaiTC, 3 RowNum, N'I' STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'HSQ, BS' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 0 bHangCha, 'A' LoaiTC,  3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, sum(isnull(iSoNgayHuong, 0)) iSoNgayHuong, sum(isnull(fSoTien, 0)) fSoTien, iiD_MaPhanHo, 0 bHasData
	from TBL_BenhDaiNgay
	 where sMaCapBac like '0%' 
	 group by sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iiD_MaPhanHo) hsq

	 if (select count(1) from TBL_BDN_HSQ_BS) > 2
		update TBL_BDN_HSQ_BS set bHasData = 1

	 --VCQP
	select * into TBL_BDN_VCQP from
	(select 1 bHangCha, N'A' LoaiTC, 1 RowNum, N'A' STT, null DoiTuong, null LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Thuộc Danh mục bệnh cần chữa trị dài ngày' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 1 bHangCha, N'A' LoaiTC, 4 RowNum, N'I' STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'CC, CN và VC quốc phòng' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 0 bHangCha, 'A' LoaiTC,  4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, sum(isnull(iSoNgayHuong, 0)) iSoNgayHuong, sum(isnull(fSoTien, 0)) fSoTien, iiD_MaPhanHo, 0 bHasData
	from TBL_BenhDaiNgay
	 where sMaCapBac in ('3.1', '3.2', '3.3')
	 group by sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iiD_MaPhanHo) vcqp

	 if (select count(1) from TBL_BDN_VCQP) > 2
		update TBL_BDN_VCQP set bHasData = 1

	 --HDLD
	select * into TBL_BDN_HDLD from
	(select 1 bHangCha, N'A' LoaiTC, 1 RowNum, N'A' STT, null DoiTuong, null LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Thuộc Danh mục bệnh cần chữa trị dài ngày' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 1 bHangCha, N'A' LoaiTC, 5 RowNum, N'I' STT, 'HDLD' DoiTuong, 'HDLD' LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Hợp đồng lao động' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 0 bHangCha, 'A' LoaiTC,  5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT, '', 'HDLD' LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, sum(isnull(iSoNgayHuong, 0)) iSoNgayHuong, sum(isnull(fSoTien, 0)) fSoTien, iiD_MaPhanHo, 0 bHasData
	from TBL_BenhDaiNgay
	 where sMaCapBac = '43'
	 group by sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iiD_MaPhanHo) vcqp

	 if (select count(1) from TBL_BDN_HDLD) > 2
		update TBL_BDN_HDLD set bHasData = 1

	----------------------------
	---Ốm khác
	select * into TBL_OmKhac from BH_QTC_Quy_CTCT_GiaiThichTroCap
	where (sXauNoiMa in ('9010001-010-011-0001-0001-0001-02-02','9010001-010-011-0001-0002','9010001-010-011-0001-0003','9010002-010-011-0001-0001-0001-02-02','9010002-010-011-0001-0002','9010002-010-011-0001-0003'))
		and iNamLamViec = @NamLamViec and iQuy = @Quy and iiD_MaDonVi in (SELECT * FROM f_split(@DonVi))

	--Sy quan
	select * into TBL_OK_SiQuan from
	(select 1 bHangCha, N'B' LoaiTC, 1 RowNum, N'B' STT, null DoiTuong, null LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Không thuộc Danh mục bệnh cần chữa trị dài ngày' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 1 bHangCha, N'B' LoaiTC, 1 RowNum, N'I' STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Sỹ quan' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 0 bHangCha, 'B' LoaiTC,  1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, sum(isnull(iSoNgayHuong, 0)) iSoNgayHuong, sum(isnull(fSoTien, 0)) fSoTien, iiD_MaPhanHo, 0 bHasData
	from TBL_OmKhac
	 where sMaCapBac like '1%' 
	 group by sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iiD_MaPhanHo) sq

	 if (select count(1) from TBL_OK_SiQuan) > 2
		update TBL_OK_SiQuan set bHasData = 1

	 --QNCN
	select * into TBL_OK_QNCN from
	(select 1 bHangCha, N'B' LoaiTC, 1 RowNum, N'B' STT, null DoiTuong, null LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Không thuộc Danh mục bệnh cần chữa trị dài ngày' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 1 bHangCha, N'B' LoaiTC, 2 RowNum, N'I' STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Quân nhân chuyên nghiệp' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 0 bHangCha, 'B' LoaiTC,  2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, sum(isnull(iSoNgayHuong, 0)) iSoNgayHuong, sum(isnull(fSoTien, 0)) fSoTien, iiD_MaPhanHo, 0 bHasData
	from TBL_OmKhac
	 where sMaCapBac like '2%' 
	 group by sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iiD_MaPhanHo) qncn

	 if (select count(1) from TBL_OK_QNCN) > 2
		update TBL_OK_QNCN set bHasData = 1

	 --HSQ_BS
	select * into TBL_OK_HSQ_BS from
	(select 1 bHangCha, N'B' LoaiTC, 1 RowNum, N'B' STT, null DoiTuong, null LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Không thuộc Danh mục bệnh cần chữa trị dài ngày' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 1 bHangCha, N'B' LoaiTC, 3 RowNum, N'I' STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'HSQ, BS' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 0 bHangCha, 'B' LoaiTC,  3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, sum(isnull(iSoNgayHuong, 0)) iSoNgayHuong, sum(isnull(fSoTien, 0)) fSoTien, iiD_MaPhanHo, 0 bHasData
	from TBL_OmKhac
	 where sMaCapBac like '0%' 
	 group by sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iiD_MaPhanHo) hsq

	 if (select count(1) from TBL_OK_HSQ_BS) > 2
		update TBL_OK_HSQ_BS set bHasData = 1

	 --VCQP
	select * into TBL_OK_VCQP from
	(select 1 bHangCha, N'B' LoaiTC, 1 RowNum, N'B' STT, null DoiTuong, null LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Không thuộc Danh mục bệnh cần chữa trị dài ngày' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 1 bHangCha, N'B' LoaiTC, 4 RowNum, N'I' STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'CC, CN và VC quốc phòng' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 0 bHangCha, 'B' LoaiTC,  4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, sum(isnull(iSoNgayHuong, 0)) iSoNgayHuong, sum(isnull(fSoTien, 0)) fSoTien, iiD_MaPhanHo, 0 bHasData
	from TBL_OmKhac
	 where sMaCapBac in ('3.1', '3.2', '3.3')
	 group by sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iiD_MaPhanHo) vcqp

	 if (select count(1) from TBL_OK_VCQP) > 2
		update TBL_OK_VCQP set bHasData = 1

	 --HDLD
	select * into TBL_OK_HDLD from
	(select 1 bHangCha, N'B' LoaiTC, 1 RowNum, N'B' STT, null DoiTuong, null LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Không thuộc Danh mục bệnh cần chữa trị dài ngày' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 1 bHangCha, N'B' LoaiTC, 5 RowNum, N'I' STT, 'HDLD' DoiTuong, 'HDLD' LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Hợp đồng lao động' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 0 bHangCha, 'B' LoaiTC,  5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT, '', 'HDLD' LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, sum(isnull(iSoNgayHuong, 0)) iSoNgayHuong, sum(isnull(fSoTien, 0)) fSoTien, iiD_MaPhanHo, 0 bHasData
	from TBL_OmKhac
	 where sMaCapBac = '43'
	 group by sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iiD_MaPhanHo) vcqp

	 if (select count(1) from TBL_OK_HDLD) > 2
		update TBL_OK_HDLD set bHasData = 1
	-----------------------------------
	--Ket qua
	select TBL_GTCD_RESULT.* into TBL_GTCD_RESULT from(
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iSoNgayHuong, fSoTien, iiD_MaPhanHo, bHasData 
	from TBL_BDN_SiQuan
	union all
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iSoNgayHuong, fSoTien,iiD_MaPhanHo, bHasData 
	from TBL_BDN_QNCN
	union all
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iSoNgayHuong, fSoTien, iiD_MaPhanHo, bHasData 
	from TBL_BDN_HSQ_BS
	union all
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iSoNgayHuong, fSoTien, iiD_MaPhanHo, bHasData 
	from TBL_BDN_VCQP
	union all
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iSoNgayHuong, fSoTien, iiD_MaPhanHo, bHasData 
	from TBL_BDN_HDLD
	union all
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iSoNgayHuong, fSoTien, iiD_MaPhanHo, bHasData 
	from TBL_OK_SiQuan
	union all
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iSoNgayHuong, fSoTien, iiD_MaPhanHo, bHasData 
	from TBL_OK_QNCN
	union all
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iSoNgayHuong, fSoTien, iiD_MaPhanHo, bHasData 
	from TBL_OK_HSQ_BS
	union all
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iSoNgayHuong, fSoTien, iiD_MaPhanHo, bHasData 
	from TBL_OK_VCQP
	union all
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iSoNgayHuong, fSoTien, iiD_MaPhanHo, bHasData 
	from TBL_OK_HDLD) TBL_GTCD_RESULT

	select distinct
		gt.bHangCha IsHangCha, 
		gt.LoaiTC, 
		gt.RowNum, 
		gt.STT, 
		gt.DoiTuong,
		gt.LoaiDoiTuong,
		case 
			when gt.bHangCha = 0 then concat(gt.sMa_Hieu_Can_Bo, ' - ', donvi.Ten_DonVi)
			else ''
		end as sMa_Hieu_Can_Bo,
		gt.sTenCanBo, 
		gt.sTenCapBac, 
		gt.sSoSoBHXH, 
		gt.fTienLuongThangDongBHXH, 
		gt.dTuNgay, 
		gt.dDenNgay, 
		gt.iSoNgayHuong, 
		gt.fSoTien/@DVT fSoTien, 
		gt.bHasData from TBL_GTCD_RESULT gt
		left join TL_DM_DonVi donvi on gt.iiD_MaPhanHo = donvi.Ma_DonVi and donvi.iTrangThai = 1
	where bHasData = 1
	order by LoaiTC, RowNum, LoaiDoiTuong, DoiTuong desc

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_BenhDaiNgay]') AND type in (N'U')) drop table TBL_BenhDaiNgay;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_OmKhac]') AND type in (N'U')) drop table TBL_OmKhac;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_BDN_SiQuan]') AND type in (N'U')) drop table TBL_BDN_SiQuan;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_BDN_QNCN]') AND type in (N'U')) drop table TBL_BDN_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_BDN_HSQ_BS]') AND type in (N'U')) drop table TBL_BDN_HSQ_BS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_BDN_VCQP]') AND type in (N'U')) drop table TBL_BDN_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_BDN_HDLD]') AND type in (N'U')) drop table TBL_BDN_HDLD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_OK_SiQuan]') AND type in (N'U')) drop table TBL_OK_SiQuan;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_OK_QNCN]') AND type in (N'U')) drop table TBL_OK_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_OK_HSQ_BS]') AND type in (N'U')) drop table TBL_OK_HSQ_BS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_OK_VCQP]') AND type in (N'U')) drop table TBL_OK_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_OK_HDLD]') AND type in (N'U')) drop table TBL_OK_HDLD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_GTCD_RESULT]') AND type in (N'U')) drop table TBL_GTCD_RESULT;

END
;
GO


/****** Object:  StoredProcedure [dbo].[sp_tl_bhxh_get_bang_luong_phan_ho]    Script Date: 7/22/2024 9:25:52 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bhxh_get_bang_luong_phan_ho]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bhxh_get_bang_luong_phan_ho]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_giaithich_trocap]    Script Date: 7/22/2024 9:25:52 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_giaithich_trocap]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_giaithich_trocap]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_giaithich_trocap]    Script Date: 7/22/2024 9:25:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_giaithich_trocap]
	-- Add the parameters for the stored procedure here
	@lstmaCanbo nvarchar(max) ,
	@Thang int ,
	@Nam int ,
	@DonViTinh int ,
	@TypeOutPut int,  -- 2: Đơn vị; 1: theo đối tượng,
	@MaDonVi nvarchar(max) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	--Declare ma phu cap --
	DECLARE @LstMaPhuCapBDN_D14N nvarchar(1000) = 'BDN_D14N_PCCVBH_TT,BDN_D14N_PCTNBH_TT,BDN_D14N_HSBLBH_TT,BDN_D14N_LBH_TT,BDN_D14N_PCTNVKBH_TT,BENHDAINGAY_D14NGAY';
	DECLARE @LstMaPhuCapBDN_T14N nvarchar(1000) = 'BDN_T14N_PCCVBH_TT,BDN_T14N_PCTNBH_TT,BDN_T14N_HSBLBH_TT,BDN_T14N_LBH_TT,BDN_T14N_PCTNVKBH_TT,BENHDAINGAY_T14NGAY';
	DECLARE @LstMaPhuCapOK_D14N nvarchar(1000) = 'OK_D14N_PCCVBH_TT,OK_D14N_PCTNBH_TT,OK_D14N_HSBLBH_TT,OK_D14N_LBH_TT,OK_D14N_PCTNVKBH_TT,OMKHAC_D14NGAY';
	DECLARE @LstMaPhuCapOK_T14N nvarchar(1000) = 'OK_T14N_PCCVBH_TT,OK_T14N_PCTNBH_TT,OK_T14N_HSBLBH_TT,OK_T14N_LBH_TT,OK_T14N_PCTNVKBH_TT,OMKHAC_T14NGAY';
	DECLARE @LstMaPhuCap_CON_OM nvarchar(1000) = 'CONOM_PCCVBH_TT,CONOM_PCTNBH_TT,CONOM_HSBLBH_TT,CONOM_LBH_TT,CONOM_PCTNVKBH_TT,CONOM';

	DECLARE @NameBDN_D14 nvarchar(1000) = N'Bệnh dài ngày - Dưới 14 ngày';
	DECLARE @NameBDN_T14 nvarchar(1000)=N'Bệnh dài ngày - Từ 14 ngày trở lên';
	DECLARE @NameOK_D14 nvarchar(1000)=N'Ốm khác - Dưới 14 ngày';
	DECLARE @NameOK_T14 nvarchar(1000)=N'Ốm khác - Từ 14 ngày trở lên';
	DECLARE @NameCON_OM nvarchar(1000)=N'Con ốm';


	CREATE TABLE #tempResult(STT nvarchar(6) ,TenChiTieu nvarchar(1000), MaCapBac nvarchar(50),MaCanBo nvarchar(50), TenCanBo  nvarchar(500), MaDonVi nvarchar(50), PCCVBH_TT numeric, PCTNBH_TT numeric, HSBLBH_TT numeric,  LBH_TT numeric, PCTNVKBH_TT numeric, Total numeric, LoaiDoiTuong nvarchar(50), rowNumber int)
    -- Insert statements for procedure here
	-- Bệnh dài ngày dứoi 14 ngày
	INSERT INTO #tempResult(STT, TenChiTieu, MaCapBac,MaCanBo, TenCanBo, MaDonVi, PCCVBH_TT, PCTNBH_TT, HSBLBH_TT, LBH_TT, PCTNVKBH_TT, Total, LoaiDoiTuong,rowNumber)
	SELECT CAST('1' as nvarchar(6)), @NameBDN_D14 , sMaCB, sMaCBo, sTenCbo,sMaDonVi,
	  BDN_D14N_PCCVBH_TT,BDN_D14N_PCTNBH_TT,BDN_D14N_HSBLBH_TT,BDN_D14N_LBH_TT,BDN_D14N_PCTNVKBH_TT,BENHDAINGAY_D14NGAY,
	  CASE
		WHEN sMaCB LIKE '1%' THEN 'SQ'
		WHEN sMaCB LIKE '2%' THEN 'QNCN'
		WHEN sMaCB LIKE '0%' THEN 'HSQ_BS'
		WHEN sMaCB IN('3.1','3.2','3.3') THEN 'VCQP'
		WHEN sMaCB = '43' THEN 'LDHD'
		ELSE
			NULL
	 END,
	 CASE
		WHEN sMaCB LIKE '1%' THEN 1
		WHEN sMaCB LIKE '2%' THEN 2
		WHEN sMaCB LIKE '0%' THEN 3
		WHEN sMaCB IN('3.1','3.2','3.3') THEN 4
		WHEN sMaCB = '43' THEN 5
		ELSE
			NULL
	 END
	FROM  
	(
	  SELECT SUM(ISNULL(nGiaTri,0)) as nGiaTri, sMaCheDo ,sMaCBo,sMaDonVi  ,sTenCbo,sMaCB
	  FROM TL_BangLuong_ThangBHXH
	  where sMaCBo IN (SELECT * FROM splitstring(@lstmaCanbo)) and sMaCheDo IN (SELECT * FROM splitstring(@LstMaPhuCapBDN_D14N)) AND iThang =@Thang and iNam = @Nam
	  and sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
	  group by sMaDonVi,sMaCheDo ,sMaCBo,sTenCbo,sMaCB
	) AS SourceTable  
	PIVOT  
	(  
	  SUM (nGiaTri) 
	  FOR sMaCheDo IN (BDN_D14N_PCCVBH_TT,BDN_D14N_PCTNBH_TT,BDN_D14N_HSBLBH_TT,BDN_D14N_LBH_TT,BDN_D14N_PCTNVKBH_TT,BENHDAINGAY_D14NGAY)  
	) AS PivotTable
	UNION
	-- Bệnh dài ngày trên 14 ngày
	SELECT  '2',@NameBDN_T14, sMaCB, sMaCBo, sTenCbo,sMaDonVi,
			BDN_T14N_PCCVBH_TT,BDN_T14N_PCTNBH_TT,BDN_T14N_HSBLBH_TT,BDN_T14N_LBH_TT,BDN_T14N_PCTNVKBH_TT,BENHDAINGAY_T14NGAY,
	  	  CASE
			WHEN sMaCB LIKE '1%' THEN 'SQ'
			WHEN sMaCB LIKE '2%' THEN 'QNCN'
			WHEN sMaCB LIKE '0%' THEN 'HSQ_BS'
			WHEN sMaCB IN('3.1','3.2','3.3') THEN 'VCQP'
			WHEN sMaCB = '43' THEN 'LDHD'
			ELSE
				NULL
		 END,
	 CASE
		WHEN sMaCB LIKE '1%' THEN 1
		WHEN sMaCB LIKE '2%' THEN 2
		WHEN sMaCB LIKE '0%' THEN 3
		WHEN sMaCB IN('3.1','3.2','3.3') THEN 4
		WHEN sMaCB = '43' THEN 5
		ELSE
			NULL
	 END
	FROM  
	(
	  SELECT SUM(ISNULL(nGiaTri,0)) as nGiaTri, sMaCheDo ,sMaCBo,sMaDonVi  ,sTenCbo,sMaCB
	  FROM TL_BangLuong_ThangBHXH
	  where sMaCBo IN (SELECT * FROM splitstring(@lstmaCanbo))   and sMaCheDo IN (SELECT * FROM splitstring(@LstMaPhuCapBDN_T14N)) AND iThang =@Thang and iNam = @Nam
	  and sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
	  group by sMaDonVi,sMaCheDo ,sMaCBo,sTenCbo,sMaCB
	) AS SourceTable  
	PIVOT  
	(  
	  SUM (nGiaTri) 
	  FOR sMaCheDo IN (BDN_T14N_PCCVBH_TT,BDN_T14N_PCTNBH_TT,BDN_T14N_HSBLBH_TT,BDN_T14N_LBH_TT,BDN_T14N_PCTNVKBH_TT,BENHDAINGAY_T14NGAY)  
	) AS PivotTable
		UNION

	--Ốm khác dưới 14 ngày
	SELECT  '3',@NameOK_D14,  sMaCB, sMaCBo, sTenCbo,sMaDonVi,
			OK_D14N_PCCVBH_TT,OK_D14N_PCTNBH_TT,OK_D14N_HSBLBH_TT,OK_D14N_LBH_TT,OK_D14N_PCTNVKBH_TT,OMKHAC_D14NGAY,
	  	  CASE
			WHEN sMaCB LIKE '1%' THEN 'SQ'
			WHEN sMaCB LIKE '2%' THEN 'QNCN'
			WHEN sMaCB LIKE '0%' THEN 'HSQ_BS'
			WHEN sMaCB IN('3.1','3.2','3.3') THEN 'VCQP'
			WHEN sMaCB = '43' THEN 'LDHD'
			ELSE
				NULL
		 END,
	 CASE
		WHEN sMaCB LIKE '1%' THEN 1
		WHEN sMaCB LIKE '2%' THEN 2
		WHEN sMaCB LIKE '0%' THEN 3
		WHEN sMaCB IN('3.1','3.2','3.3') THEN 4
		WHEN sMaCB = '43' THEN 5
		ELSE
			NULL
	 END
	 FROM  
	(
	  SELECT SUM(ISNULL(nGiaTri,0)) as nGiaTri, sMaCheDo ,sMaCBo,sMaDonVi  ,sTenCbo,sMaCB
	  FROM TL_BangLuong_ThangBHXH
	  where sMaCBo IN (SELECT * FROM splitstring(@lstmaCanbo))   and sMaCheDo IN (SELECT * FROM splitstring(@LstMaPhuCapOK_D14N)) AND iThang =@Thang and iNam = @Nam
	  and sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
	  group by sMaDonVi,sMaCheDo ,sMaCBo,sTenCbo,sMaCB
	) AS SourceTable  
	PIVOT  
	(  
	  SUM (nGiaTri) 
	  FOR sMaCheDo IN (OK_D14N_PCCVBH_TT,OK_D14N_PCTNBH_TT,OK_D14N_HSBLBH_TT,OK_D14N_LBH_TT,OK_D14N_PCTNVKBH_TT,OMKHAC_D14NGAY)  
	) AS PivotTable
		UNION

	-- Ốm khác trên 14 ngày
	SELECT '4', @NameOK_T14, sMaCB, sMaCBo, sTenCbo,sMaDonVi,
			OK_T14N_PCCVBH_TT,OK_T14N_PCTNBH_TT,OK_T14N_HSBLBH_TT,OK_T14N_LBH_TT,OK_T14N_PCTNVKBH_TT,OMKHAC_T14NGAY,
	  	  CASE
			WHEN sMaCB LIKE '1%' THEN 'SQ'
			WHEN sMaCB LIKE '2%' THEN 'QNCN'
			WHEN sMaCB LIKE '0%' THEN 'HSQ_BS'
			WHEN sMaCB IN('3.1','3.2','3.3') THEN 'VCQP'
			WHEN sMaCB = '43' THEN 'LDHD'
			ELSE
				NULL
		 END,
	 CASE
		WHEN sMaCB LIKE '1%' THEN 1
		WHEN sMaCB LIKE '2%' THEN 2
		WHEN sMaCB LIKE '0%' THEN 3
		WHEN sMaCB IN('3.1','3.2','3.3') THEN 4
		WHEN sMaCB = '43' THEN 5
		ELSE
			NULL
	 END
	FROM  
	(
	  SELECT SUM(ISNULL(nGiaTri,0)) as nGiaTri, sMaCheDo ,sMaCBo,sMaDonVi  ,sTenCbo,sMaCB
	  FROM TL_BangLuong_ThangBHXH
	  where sMaCBo IN (SELECT * FROM splitstring(@lstmaCanbo))   and sMaCheDo IN (SELECT * FROM splitstring(@LstMaPhuCapOK_T14N)) AND iThang =@Thang and iNam = @Nam
	  and sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
	  group by sMaDonVi,sMaCheDo ,sMaCBo,sTenCbo,sMaCB
	) AS SourceTable  
	PIVOT  
	(  
	  SUM (nGiaTri) 
	  FOR sMaCheDo IN (OK_T14N_PCCVBH_TT,OK_T14N_PCTNBH_TT,OK_T14N_HSBLBH_TT,OK_T14N_LBH_TT,OK_T14N_PCTNVKBH_TT,OMKHAC_T14NGAY)  
	) AS PivotTable

	UNION
	-- con ốm
	SELECT '5', @NameCON_OM, sMaCB, sMaCBo, sTenCbo,sMaDonVi,
			CONOM_PCCVBH_TT,CONOM_PCTNBH_TT,CONOM_HSBLBH_TT,CONOM_LBH_TT,CONOM_PCTNVKBH_TT,CONOM,
	  	  CASE
			WHEN sMaCB LIKE '1%' THEN 'SQ'
			WHEN sMaCB LIKE '2%' THEN 'QNCN'
			WHEN sMaCB LIKE '0%' THEN 'HSQ_BS'
			WHEN sMaCB IN('3.1','3.2','3.3') THEN 'VCQP'
			WHEN sMaCB = '43' THEN 'LDHD'
			ELSE
				NULL
		 END,
	 CASE
		WHEN sMaCB LIKE '1%' THEN 1
		WHEN sMaCB LIKE '2%' THEN 2
		WHEN sMaCB LIKE '0%' THEN 3
		WHEN sMaCB IN('3.1','3.2','3.3') THEN 4
		WHEN sMaCB = '43' THEN 5
		ELSE
			NULL
	 END
	FROM  
	(
	  SELECT SUM(ISNULL(nGiaTri,0)) as nGiaTri, sMaCheDo ,sMaCBo,sMaDonVi  ,sTenCbo,sMaCB
	  FROM TL_BangLuong_ThangBHXH
	  where sMaCBo IN (SELECT * FROM splitstring(@lstmaCanbo))   and sMaCheDo IN (SELECT * FROM splitstring(@LstMaPhuCap_CON_OM)) AND iThang =@Thang and iNam = @Nam
	  and sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
	  group by sMaDonVi,sMaCheDo ,sMaCBo,sTenCbo,sMaCB
	) AS SourceTable  
	PIVOT  
	(  
	  SUM (nGiaTri) 
	  FOR sMaCheDo IN (CONOM_PCCVBH_TT,CONOM_PCTNBH_TT,CONOM_HSBLBH_TT,CONOM_LBH_TT,CONOM_PCTNVKBH_TT,CONOM)  
	) AS PivotTable;  


	INSERT INTO #tempResult(STT, TenChiTieu, MaCapBac,MaCanBo, TenCanBo, MaDonVi, PCCVBH_TT, PCTNBH_TT, HSBLBH_TT, LBH_TT, PCTNVKBH_TT, Total,LoaiDoiTuong,rowNumber)
	SELECT '0',TenCanBo,MaCapBac, MaCanBo,TenCanBo, MaDonVi, 
			SUM(ISNULL(PCCVBH_TT,0)) PCCVBH_TT,
			SUM(ISNULL(PCTNBH_TT,0)) PCTNBH_TT, 
			SUM(ISNULL(HSBLBH_TT,0)) HSBLBH_TT, 
			SUM(ISNULL(LBH_TT,0)) LBH_TT, 
			SUM(ISNULL(PCTNVKBH_TT,0)) PCTNVKBH_TT,
			SUM(ISNULL(Total,0)) Total,
			LoaiDoiTuong,
			rowNumber
	FROM #tempResult group by MaCanBo, TenCanBo, MaCapBac,MaDonVi, LoaiDoiTuong,rowNumber;
	--SELECT LEFT(MaCapBac, 1),* FROM #tempResult ORDER BY MaCanBo , STT;
	IF(@TypeOutPut = 2)
		BEGIN
		--Lấy Đơn vị
			SELECT 
				0 as Level,
				CAST (NULL as nvarchar(6)) STT,
				CAST (NULL as nvarchar(50)) LoaiDoiTuong,
				donvi.Ten_DonVi as TenChiTieu,
				CAST (NULL as nvarchar(50)) MaCapBac,
				CAST (NULL as nvarchar(50)) MaCanBo,
				CAST (NULL as nvarchar(50)) TenCanBo,
				rs.MaDonVi MaDonVi, 
				donvi.Ten_DonVi as TenDonVi,
				SUM(ISNULL(PCCVBH_TT,0))/@DonViTinh PCCVBH_TT,
				SUM(ISNULL(PCTNBH_TT,0))/@DonViTinh PCTNBH_TT, 
				SUM(ISNULL(HSBLBH_TT,0))/@DonViTinh HSBLBH_TT, 
				SUM(ISNULL(LBH_TT,0))/@DonViTinh LBH_TT, 
				SUM(ISNULL(PCTNVKBH_TT,0))/@DonViTinh PCTNVKBH_TT,
				SUM(ISNULL(Total,0))/@DonViTinh Total,
				0 as rowNumber
				INTO #tempDonVi
				FROM #tempResult rs
				LEFT JOIN TL_DM_DonVi donvi ON donvi.Ma_DonVi = rs.MaDonVi
				WHERE rs.STT <> 0
				GROUP BY rs.MaDonVi, donvi.Ten_DonVi
				ORDER BY rs.MaDonVi;
			-- Lấy Loại đối tượng
			SELECT 
				1 Level,
	  			CAST( CASE
					WHEN rs.LoaiDoiTuong = 'SQ' THEN 'I'
					WHEN rs.LoaiDoiTuong = 'QNCN' THEN 'II'
					WHEN rs.LoaiDoiTuong = 'HSQ_BS' THEN 'III'
					WHEN rs.LoaiDoiTuong = 'VCQP' THEN 'IV'
					WHEN rs.LoaiDoiTuong = 'LDHD' THEN 'V'
					ELSE
						NULL
				END AS nvarchar(6))  STT,
				rs.LoaiDoiTuong,
				rs.LoaiDoiTuong as TenChiTieu,
				CAST (NULL as nvarchar(50)) MaCapBac,
				CAST (NULL as nvarchar(50)) MaCanBo,
				CAST (NULL as nvarchar(50)) TenCanBo,
				rs.MaDonVi MaDonVi, 
				donvi.Ten_DonVi as TenDonVi,
				SUM(ISNULL(PCCVBH_TT,0))/@DonViTinh PCCVBH_TT,
				SUM(ISNULL(PCTNBH_TT,0))/@DonViTinh PCTNBH_TT, 
				SUM(ISNULL(HSBLBH_TT,0))/@DonViTinh HSBLBH_TT, 
				SUM(ISNULL(LBH_TT,0))/@DonViTinh LBH_TT, 
				SUM(ISNULL(PCTNVKBH_TT,0))/@DonViTinh PCTNVKBH_TT,
				SUM(ISNULL(Total,0))/@DonViTinh Total,
				CAST( CASE
					WHEN rs.LoaiDoiTuong = 'SQ' THEN 1
					WHEN rs.LoaiDoiTuong = 'QNCN' THEN 2
					WHEN rs.LoaiDoiTuong = 'HSQ_BS' THEN 3
					WHEN rs.LoaiDoiTuong = 'VCQP' THEN 4
					WHEN rs.LoaiDoiTuong = 'LDHD' THEN 5
					ELSE
						NULL
				END AS int)  rowNumber
				INTO #tempLoaiDoiTuong
				FROM #tempResult rs
				LEFT JOIN TL_DM_DonVi donvi ON donvi.Ma_DonVi = rs.MaDonVi
				WHERE rs.STT <> '0'
				GROUP BY rs.MaDonVi,rs.LoaiDoiTuong,donvi.Ten_DonVi

			-- lấy chi tiết từng đối tượng
			SELECT 
				CASE
					WHEN STT = '0' THEN 2 
					ELSE 3
				END AS Level,
				STT,
				LoaiDoiTuong,
				TenChiTieu,
				MaCapBac, 
				MaCanBo,
				TenCanBo,
				rs.MaDonVi MaDonVi, 
				donvi.Ten_DonVi as TenDonVi,
				ISNULL(PCCVBH_TT,0)/@DonViTinh PCCVBH_TT,
				ISNULL(PCTNBH_TT,0)/@DonViTinh PCTNBH_TT, 
				ISNULL(HSBLBH_TT,0)/@DonViTinh HSBLBH_TT, 
				ISNULL(LBH_TT,0)/@DonViTinh LBH_TT, 
				ISNULL(PCTNVKBH_TT,0)/@DonViTinh PCTNVKBH_TT,
				ISNULL(Total,0)/@DonViTinh Total,
				rs.rowNumber
				INTO #tempCanBo 
				FROM #tempResult rs
				LEFT JOIN TL_DM_DonVi donvi ON donvi.Ma_DonVi = rs.MaDonVi;

				--OUTPUT---
			SELECT rs.*  FROM
			(
			SELECT * FROM #tempDonVi
			UNION 
			SELECT * FROM #tempLoaiDoiTuong
			UNION
			SELECT * FROM #tempCanBo
			) rs
			ORDER BY rs.MaDonVi,rs.rowNumber,rs.MaCanBo

			DROP TABLE #tempDonVi;
			DROP TABLE #tempLoaiDoiTuong;
			DROP TABLE #tempCanBo;
		END
	ELSE
		BEGIN 
-- Lấy Loại đối tượng
			SELECT 
				1 Level,
	  			CAST( CASE
					WHEN rs.LoaiDoiTuong = 'SQ' THEN 'I'
					WHEN rs.LoaiDoiTuong = 'QNCN' THEN 'II'
					WHEN rs.LoaiDoiTuong = 'HSQ_BS' THEN 'III'
					WHEN rs.LoaiDoiTuong = 'VCQP' THEN 'IV'
					WHEN rs.LoaiDoiTuong = 'LDHD' THEN 'V'
					ELSE
						NULL
				END AS nvarchar(6))  STT,
				rs.LoaiDoiTuong,
				rs.LoaiDoiTuong as TenChiTieu,
				CAST (NULL as nvarchar(50)) MaCapBac,
				CAST (NULL as nvarchar(50)) MaCanBo,
				CAST (NULL as nvarchar(50)) TenCanBo,
				CAST (NULL as nvarchar(50)) MaDonVi,
				SUM(ISNULL(PCCVBH_TT,0))/@DonViTinh PCCVBH_TT,
				SUM(ISNULL(PCTNBH_TT,0))/@DonViTinh PCTNBH_TT, 
				SUM(ISNULL(HSBLBH_TT,0))/@DonViTinh HSBLBH_TT, 
				SUM(ISNULL(LBH_TT,0))/@DonViTinh LBH_TT, 
				SUM(ISNULL(PCTNVKBH_TT,0))/@DonViTinh PCTNVKBH_TT,
				SUM(ISNULL(Total,0))/@DonViTinh Total,
				CAST( CASE
					WHEN rs.LoaiDoiTuong = 'SQ' THEN 1
					WHEN rs.LoaiDoiTuong = 'QNCN' THEN 2
					WHEN rs.LoaiDoiTuong = 'HSQ_BS' THEN 3
					WHEN rs.LoaiDoiTuong = 'VCQP' THEN 4
					WHEN rs.LoaiDoiTuong = 'LDHD' THEN 5
					ELSE
						NULL
				END AS int)  rowNumber
				INTO #tempLoaiDoiTuong2
				FROM #tempResult rs
				LEFT JOIN TL_DM_DonVi donvi ON donvi.Ma_DonVi = rs.MaDonVi
				WHERE rs.STT <> '0'
				GROUP BY rs.LoaiDoiTuong

			-- lấy chi tiết từng đối tượng
			SELECT 
				CASE
					WHEN STT = '0' THEN 2 
					ELSE 3
				END AS Level,
				STT,
				LoaiDoiTuong,
				TenChiTieu,
				MaCapBac, 
				MaCanBo,
				TenCanBo,
				CASE
					WHEN STT = '0' THEN rs.MaDonVi 
					ELSE NULL
				END AS MaDonVi,
				ISNULL(PCCVBH_TT,0)/@DonViTinh PCCVBH_TT,
				ISNULL(PCTNBH_TT,0)/@DonViTinh PCTNBH_TT, 
				ISNULL(HSBLBH_TT,0)/@DonViTinh HSBLBH_TT, 
				ISNULL(LBH_TT,0)/@DonViTinh LBH_TT, 
				ISNULL(PCTNVKBH_TT,0)/@DonViTinh PCTNVKBH_TT,
				ISNULL(Total,0)/@DonViTinh Total,
				rs.rowNumber
				INTO #tempCanBo2 
				FROM #tempResult rs

				--OUTPUT---
			SELECT rs.*,donvi.Ten_DonVi as TenDonVi  FROM
			(
			SELECT * FROM #tempLoaiDoiTuong2
			UNION
			SELECT * FROM #tempCanBo2
			) rs
			LEFT JOIN TL_DM_DonVi donvi ON donvi.Ma_DonVi = rs.MaDonVi
			ORDER BY rs.rowNumber,rs.MaCanBo

			DROP TABLE #tempLoaiDoiTuong2;
			DROP TABLE #tempCanBo2;		
		END
	DROP TABLE #tempResult;
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bhxh_get_bang_luong_phan_ho]    Script Date: 7/22/2024 9:25:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_tl_bhxh_get_bang_luong_phan_ho]
	@Nam int,
	@Thang int
AS
BEGIN
	
	select
		ctct.Id,
		ctct.nGiaTri GiaTri,
		ctct.nHuongPCSN HuongPcSn,
		ctct.iLoaiBL LoaiBl,
		ctct.sMaCachTL MaCachTl,
		ctct.sMaCB MaCb,
		ctct.sMaCBo MaCbo,
		ctct.sMaCheDo MaCheDo,
		ctct.sMaDonVi MaDonVi,
		ctct.sMaHieuCanBo MaHieuCanBo,
		ctct.iNam Nam,
		ctct.dNgayHT NgayHT,
		ctct.iID_Parent Parent,
		ctct.iSoTT SoTt,
		ctct.sTenCachTL TenCachTL,
		ctct.sTenCbo TenCbo,
		ctct.iThang Thang,
		ctct.sUserName UserName
	from TL_BangLuong_ThangBHXH ctct
	join TL_DS_CapNhap_BangLuong ct on ctct.iID_Parent = ct.Id
	where ctct.iNam = @Nam
		and ctct.iThang = @Thang
		and ct.STongHop is null

END
GO
