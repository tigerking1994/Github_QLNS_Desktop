/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_tonghop]    Script Date: 4/24/2024 8:19:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]    Script Date: 4/24/2024 8:19:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_thctdv]    Script Date: 4/24/2024 8:19:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_thctdv]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_thctdv]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_tonghop]    Script Date: 4/24/2024 8:19:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong]    Script Date: 4/24/2024 8:19:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong]    Script Date: 4/24/2024 8:19:04 AM ******/
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

	declare @dNgayDieuChinh Datetime = (select top 1 dNgayChungTu from BH_DTT_BHXH_DieuChinh where iNamLamViec = @NamLamViec and iID_MaDonVi = @MaDonVi);

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' -- CC,CN, VCQP
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' -- LĐHĐ

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' -- CC,CN, VCQP
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' -- LĐHĐ

	-- Khối hạch toán
	union
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000'  -- CC,CN, VCQP
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001'  -- LĐHĐ

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000'  -- CC,CN, VCQP
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001'  -- LĐHĐ
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0000' -- Sĩ quan
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0001' -- QNCN
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0002' -- HSQ-BS
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan 
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ
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
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (36)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (36)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (36)),
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
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (47)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (47)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (47)),
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
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_tonghop]    Script Date: 4/24/2024 8:19:04 AM ******/
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

	declare @dNgayDieuChinh Datetime = (select top 1 dNgayChungTu from BH_DTT_BHXH_DieuChinh where iNamLamViec = @NamLamViec and iID_MaDonVi = @MaDonVi);

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' -- CC,CN, VCQP
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' -- LĐHĐ

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' -- CC,CN, VCQP
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' -- LĐHĐ

	-- Khối hạch toán
	union
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000'  -- CC,CN, VCQP
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001'  -- LĐHĐ

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000'  -- CC,CN, VCQP
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001'  -- LĐHĐ
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0000' -- Sĩ quan
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0001' -- QNCN
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0002' -- HSQ-BS
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan 
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ
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
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (36)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (36)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (36)),
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
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (47)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (47)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (47)),
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
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_thctdv]    Script Date: 4/24/2024 8:19:04 AM ******/
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

	declare @dNgayDieuChinh Datetime = (select top 1 dNgayChungTu from BH_DTT_BHXH_DieuChinh where iNamLamViec = @NamLamViec and iID_MaDonVi = @MaDonVi);

	--BHXH
	--Lấy dữ liệu NLĐ, NSD của khối dự toán
	select child.* into tbl_child from
	(
	select
	5 STT,
	N'5' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	union all
	select
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
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
		left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020001' -- Khối dự toán
		and dv.iTrangThai = 1
	group by dv.sTenDonVi

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020001' -- Khối dự toán

	union all
	select
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
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020001' -- Khối dự toán
		and dv.iTrangThai = 1
	group by dv.sTenDonVi
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020002' -- Khối hạch toán

	union all
	select
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
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020002' -- Khối hạch toán
		and dv.iTrangThai = 1
	group by dv.sTenDonVi

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020002' -- Khối hạch toán

	union all
	select
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
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020002' -- Khối hạch toánand dv.iTrangThai = 1
	group by dv.sTenDonVi
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020001' -- Khối dự toán
		and ctct.sXauNoiMa in
		(
		'9020001-010-011-0001-0000',  -- Sĩ quan
		'9020001-010-011-0001-0001'	, -- QNCN
		'9020001-010-011-0001-0002'  -- HSQ, BS
		)

	union all
	select
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
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020001' -- Khối dự toán
		and ctct.sXauNoiMa in (
		'9020001-010-011-0001-0000',  -- Sĩ quan
		'9020001-010-011-0001-0001'	, -- QNCN
		'9020001-010-011-0001-0002')  -- HSQ, BS
		and dv.iTrangThai = 1
	group by dv.sTenDonVi

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020001' -- Khối dự toán
		and ctct.sXauNoiMa in
		(
		'9020001-010-011-0002-0000',  -- CC,CN, VCQP
		'9020001-010-011-0002-0001'  -- LĐHĐ
		)

	union all
	select
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
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020001' -- Khối dự toán
		and ctct.sXauNoiMa in (
		'9020001-010-011-0002-0000',  -- CC,CN, VCQP
		'9020001-010-011-0002-0001')  -- LĐHĐ
	and dv.iTrangThai = 1
	group by dv.sTenDonVi

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020001' -- Khối dự toán
		and ctct.sXauNoiMa in
		(
		'9020001-010-011-0002-0000',  -- CC,CN, VCQP
		'9020001-010-011-0002-0001'  -- LĐHĐ
		)

	union all
	select
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
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020001' -- Khối dự toán
		and ctct.sXauNoiMa in (
		'9020001-010-011-0002-0000',  -- CC,CN, VCQP
		'9020001-010-011-0002-0001')  -- LĐHĐ
		and dv.iTrangThai = 1
	group by dv.sTenDonVi

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0001-0000',  -- Sĩ quan
	'9020002-010-011-0001-0001'	, -- QNCN
	'9020002-010-011-0001-0002'  -- HSQ, BS
	)

	union all
	select
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
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020002' -- Khối hạch toán
		and ctct.sXauNoiMa in (
		'9020002-010-011-0001-0000',  -- Sĩ quan
		'9020002-010-011-0001-0001'	, -- QNCN
		'9020002-010-011-0001-0002')  -- HSQ, BS
		and dv.iTrangThai = 1
	group by dv.sTenDonVi

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)

	union all
	select
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
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020002' -- Khối hạch toán
		and ctct.sXauNoiMa in (
		'9020002-010-011-0002-0000',  -- CC,CN, VCQP
		'9020002-010-011-0002-0001')  -- LĐHĐ
		and dv.iTrangThai = 1
	group by dv.sTenDonVi

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)

	union all
	select
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
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020002' -- Khối hạch toán
		and ctct.sXauNoiMa in (
		'9020002-010-011-0002-0000',  -- CC,CN, VCQP
		'9020002-010-011-0002-0001')  -- LĐHĐ
		and dv.iTrangThai = 1
	group by dv.sTenDonVi

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	union all
	select
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
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020001' -- Khối dự toán
		and dv.iTrangThai = 1
	group by dv.sTenDonVi

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	union all
	select
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
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020001' -- Khối dự toán
		and dv.iTrangThai = 1
	group by dv.sTenDonVi

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán

	union all
	select
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
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020002' -- Khối hạch toán
		and dv.iTrangThai = 1
	group by dv.sTenDonVi

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán

	union all
	select
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
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020002' -- Khối hạch toán
		and dv.iTrangThai = 1
	group by dv.sTenDonVi
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
	and MaSo in ('28=29+330', '31=32+33')

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
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT = 16 and MaSo = 16),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT = 16 and MaSo = 16),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT = 16 and MaSo = 16),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15 and MaSo = '15=16+17'

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15 and MaSo = '15=16+17'

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT = 21 and MaSo = 21),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT = 21 and MaSo = 21),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT = 21 and MaSo = 21),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20 and MaSo = '20=21+22'

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
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
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]    Script Date: 4/24/2024 8:19:04 AM ******/
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

	declare @dNgayDieuChinh Datetime = (select top 1 dNgayChungTu from BH_DTT_BHXH_DieuChinh where iNamLamViec = @NamLamViec and iID_MaDonVi = @MaDonVi);

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
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
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_tonghop]    Script Date: 4/24/2024 8:19:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_tonghop]
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

	declare @dNgayDieuChinh Datetime = (select top 1 dNgayChungTu from BH_DTT_BHXH_DieuChinh where iNamLamViec = @NamLamViec and iID_MaDonVi = @MaDonVi);

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
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
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
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
;
;
GO


/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_quy]    Script Date: 4/25/2024 10:05:34 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_quy]    Script Date: 4/25/2024 10:05:34 AM ******/
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
	@ILoaiQuy int
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
END
;
;
;
;
GO


/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_thang]    Script Date: 4/25/2024 10:39:36 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]    Script Date: 4/25/2024 10:39:36 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_nam]    Script Date: 4/25/2024 10:39:36 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_nam]    Script Date: 4/25/2024 10:39:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_nam]
	@NamLamViec int,
	@IdDonVis nvarchar(100),
	@Donvitinh int,
	@IQuy int  
AS
BEGIN
declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVis;
DECLARE @sSoChungTuTH nvarchar(1000) = (SELECT TOP 1 sTongHop FROM BH_QTT_BHXH_ChungTu pr 	where pr.iNamLamViec = @NamLamViec
																		and pr.iQuyNam = @IQuy
																		and pr.iID_MaDonVi = @IdDonVis
																		and pr.iLoaiTongHop = 2
																		and pr.bDaTongHop = 0)
CREATE TABLE #result(
iID_MLNS uniqueidentifier,
iID_MLNS_Cha uniqueidentifier,
bHangCha bit,
sM nvarchar(200),
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
			ct.iNamLamViec,
			ctct.iID_QTT_BHXH_ChungTu_ChiTiet
			,ctct.iID_QTT_BHXH_ChungTu
			,ctct.iQSBQNam
			,ctct.fLuongChinh
			,ctct.fPCChucVu fPhuCapChucVu
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
			,mlns.sM
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
			and ct.iQuyNam = @IQuy
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
sM,
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
		mlns.sM,
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
			AND iNamLamViec = @NamLamViec			
			UNION SELECT NewId() iID_MLNS, (SELECT DISTINCT iID_MLNS_Cha FROM BH_DM_MucLucNganSach WHERE  sLNS = '9020001' and sL is not null and sL <> '' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020001' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020001-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
			UNION SELECT NewId() iID_MLNS, (SELECT DISTINCT iID_MLNS_Cha FROM BH_DM_MucLucNganSach WHERE  sLNS = '9020002' and sL is not null and sL <> '' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020002' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020002-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
		) mlns
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
			and ((@isCha = 0 and ctct.iID_MaDonVi =@IdDonVis) or (@isCha = 1 and ct.iID_MaDonVi =@IdDonVis))
			--and ct.iID_MaDonVi = @IdDonVis
			and ct.iLoaiTongHop = 2
--			and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end			
		)chungtudonvi 
			on mlns.sXauNoiMa = chungtudonvi.sXauNoiMa
		ORDER BY mlns.sXauNoiMa;


----------------END INSERT DETAIL----------------
----------------INSERT DETAIL----------------
INSERT INTO #result
(
iID_MLNS ,
iID_MLNS_Cha ,
bHangCha , 
sM,
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
sM,
sXauNoiMa , 
dv.sTenDonVi,
#tempChiTietDonVi.iNamLamViec ,
iQSBQNam ,
fLuongChinh ,
fPhuCapChucVu,
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
FROM #tempChiTietDonVi 
LEFT JOIN DonVi dv ON dv.iID_MaDonVi = #tempChiTietDonVi.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec;

	-- Lay data truy thu
	select sLNS,
		sum(isnull(fTruyThu_BHXH_NLD, 0)) fTruyThu_BHXH_NLD,
		sum(isnull(fTruyThu_BHXH_NSD, 0)) fTruyThu_BHXH_NSD,
		(sum(isnull(fTruyThu_BHXH_NLD, 0)) + sum(isnull(fTruyThu_BHXH_NSD, 0))) fTongTruyThu_BHXH,
		sum(isnull(fTruyThu_BHYT_NLD, 0)) fTruyThu_BHYT_NLD,
		sum(isnull(fTruyThu_BHYT_NSD, 0)) fTruyThu_BHYT_NSD,
		(sum(isnull(fTruyThu_BHYT_NLD, 0)) + sum(isnull(fTruyThu_BHYT_NSD, 0))) fTongTruyThu_BHYT,
		sum(isnull(fTruyThu_BHTN_NLD, 0)) fTruyThu_BHTN_NLD,
		sum(isnull(fTruyThu_BHTN_NSD, 0)) fTruyThu_BHTN_NSD,
		(sum(isnull(fTruyThu_BHTN_NLD, 0)) + sum(isnull(fTruyThu_BHTN_NSD, 0))) fTongTruyThu_BHTN
	into #tbl_qtn_truythu
	from BH_QTT_BHXH_CTCT_GiaiThich
	where iLoaiGiaiThich = 2
		and iQuyNam = @IQuy
		and iID_MaDonVi = @IdDonVis
	group by sLNS

	--Update so truy thu
	update #result
	set fThu_BHXH_NLD = (select fTruyThu_BHXH_NLD from #tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHXH_NSD = (select fTruyThu_BHXH_NSD from #tbl_qtn_truythu where sLNS = '9020001'),
		FTongSoPhaiThuBHXH = (select fTongTruyThu_BHXH from #tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHYT_NLD = (select fTruyThu_BHYT_NLD from #tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHYT_NSD = (select fTruyThu_BHYT_NSD from #tbl_qtn_truythu where sLNS = '9020001'),
		fTongSoPhaiThuBHYT = (select fTongTruyThu_BHYT from #tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHTN_NLD = (select fTruyThu_BHTN_NLD from #tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHTN_NSD = (select fTruyThu_BHTN_NSD from #tbl_qtn_truythu where sLNS = '9020001'),
		fTongSoPhaiThuBHTN = (select fTongTruyThu_BHTN from #tbl_qtn_truythu where sLNS = '9020001'),
		fTongCong = (select isnull(fTongTruyThu_BHXH, 0) + isnull(fTongTruyThu_BHYT, 0) + isnull(fTongTruyThu_BHTN, 0) from #tbl_qtn_truythu where sLNS = '9020001')
	where sXauNoiMa = '9020001-010-011-0003'

	update #result
	set fThu_BHXH_NLD = (select fTruyThu_BHXH_NLD from #tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHXH_NSD = (select fTruyThu_BHXH_NSD from #tbl_qtn_truythu where sLNS = '9020002'),
		FTongSoPhaiThuBHXH = (select fTongTruyThu_BHXH from #tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHYT_NLD = (select fTruyThu_BHYT_NLD from #tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHYT_NSD = (select fTruyThu_BHYT_NSD from #tbl_qtn_truythu where sLNS = '9020002'),
		fTongSoPhaiThuBHYT = (select fTongTruyThu_BHYT from #tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHTN_NLD = (select fTruyThu_BHTN_NLD from #tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHTN_NSD = (select fTruyThu_BHTN_NSD from #tbl_qtn_truythu where sLNS = '9020002'),
		fTongSoPhaiThuBHTN = (select fTongTruyThu_BHTN from #tbl_qtn_truythu where sLNS = '9020002'),
		fTongCong = (select isnull(fTongTruyThu_BHXH, 0) + isnull(fTongTruyThu_BHYT, 0) + isnull(fTongTruyThu_BHTN, 0) from #tbl_qtn_truythu where sLNS = '9020002')
	where sXauNoiMa = '9020002-010-011-0003'

-- Ket qua
SELECT * FROM #result ORDER BY sXauNoiMa, MaDonVi;

DROP TABLE #tempChiTietDonVi;
DROP TABLE #tbl_qtn_truythu;
DROP TABLE #result;
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]    Script Date: 4/25/2024 10:39:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]
	@NamLamViec int,
	@IdDonVis nvarchar(100),
	@Donvitinh int,
	@IQuy int,
	@ILoaiQuy int
AS
BEGIN
declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVis;
DECLARE @sSoChungTuTH nvarchar(1000) = (SELECT TOP 1 sTongHop FROM BH_QTT_BHXH_ChungTu pr 	where pr.iNamLamViec = @NamLamViec
																		and pr.iQuyNam = @IQuy
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
			INTO #tempChiTietDonVi
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
#tempChiTietDonVi.iNamLamViec ,
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
FROM #tempChiTietDonVi 
LEFT JOIN DonVi dv ON dv.iID_MaDonVi = #tempChiTietDonVi.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec;
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
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_thang]    Script Date: 4/25/2024 10:39:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_thang]
	@NamLamViec int,
	@IdDonVis nvarchar(100),
	@Donvitinh int,
	@IQuy int,
	@ILoaiQuy int
AS
BEGIN

	DECLARE @SMonths NVARCHAR(50)

	IF (@IQuy = 3) SET @SMonths = '1,2,3'
	IF (@IQuy = 6) SET @SMonths = '4,5,6'
	IF (@IQuy = 9) SET @SMonths = '7,8,9'
	IF (@IQuy = 12) SET @SMonths = '10,11,12'

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
GO


DELETE FROM DM_ChuKy WHERE Id_Code = 'rptBHXH_KHT_ChiTiet';
