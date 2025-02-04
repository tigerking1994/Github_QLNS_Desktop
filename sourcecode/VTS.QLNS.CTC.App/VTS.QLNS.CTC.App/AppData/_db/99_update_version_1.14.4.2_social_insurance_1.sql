/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_tonghop]    Script Date: 5/6/2024 5:56:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]    Script Date: 5/6/2024 5:56:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_thctdv]    Script Date: 5/6/2024 5:56:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_thctdv]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_thctdv]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_tonghop]    Script Date: 5/6/2024 5:56:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong]    Script Date: 5/6/2024 5:56:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong]
GO
/****** Object:  StoredProcedure [dbo].[sp_dieu_chinh_dtt_tao_tonghop_chungtu_chitiet]    Script Date: 5/6/2024 5:56:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dieu_chinh_dtt_tao_tonghop_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dieu_chinh_dtt_tao_tonghop_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_dieu_chinh_dtt_tao_tonghop_chungtu_chitiet]    Script Date: 5/6/2024 5:56:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dieu_chinh_dtt_tao_tonghop_chungtu_chitiet]
	@ListIdChungTuTongHop ntext, 
	@Nguoitao nvarchar(50), 
	@IdChungTu nvarchar(100), 
	@NamLamViec int,
	@MaDonVi NVARCHAR(255)
AS 
BEGIN 

	declare @dNgayDieuChinh Datetime = (select top 1 dNgayChungTu from BH_DTT_BHXH_DieuChinh where iNamLamViec = @NamLamViec and iID_MaDonVi = @MaDonVi order by dNgayChungTu desc);

	INSERT INTO [dbo].BH_DTT_BHXH_DieuChinh_ChiTiet (
    iID_DTT_BHXH_DieuChinh_ChiTiet, iID_DTT_BHXH_DieuChinh, iID_MucLucNganSach, sLNS, sNoiDung, sXauNoiMa,
	fThuBHXH_NLD, fThuBHXH_NSD, fThuBHYT_NLD, fThuBHYT_NSD, fThuBHTN_NLD, fThuBHTN_NSD,
	fThuBHXH_NLD_QTDauNam, fThuBHXH_NSD_QTDauNam, fThuBHYT_NLD_QTDauNam, fThuBHYT_NSD_QTDauNam, fThuBHTN_NLD_QTDauNam, fThuBHTN_NSD_QTDauNam,
	fThuBHXH_NLD_QTCuoiNam, fThuBHXH_NSD_QTCuoiNam, fThuBHYT_NLD_QTCuoiNam, fThuBHYT_NSD_QTCuoiNam, fThuBHTN_NLD_QTCuoiNam, fThuBHTN_NSD_QTCuoiNam,
	fTongThuBHXH_NLD, fTongThuBHXH_NSD, fTongThuBHYT_NLD, fTongThuBHYT_NSD, fTongThuBHTN_NLD, fTongThuBHTN_NSD,
	fThuBHXH_NLD_Tang, fThuBHXH_NSD_Tang, fThuBHXH_Tang, fThuBHYT_NLD_Tang, fThuBHYT_NSD_Tang, fThuBHYT_Tang, fThuBHTN_NLD_Tang, fThuBHTN_NSD_Tang, fThuBHTN_Tang,
	fThuBHXH_NLD_Giam, fThuBHXH_NSD_Giam, fThuBHXH_Giam, fThuBHYT_NLD_Giam, fThuBHYT_NSD_Giam, fThuBHYT_Giam, fThuBHTN_NLD_Giam, fThuBHTN_NSD_Giam, fThuBHTN_Giam, fTongCong,
    dNgaySua, dNgayTao, sNguoiSua, sNguoiTao)

	SELECT 
		DISTINCT NEWID(), @idChungTu, dtct.iID_MLNS, dcct.sLNS, dcct.sNoiDung, dcct.sXauNoiMa,
		sum(isnull(dtct.fThu_BHXH_NLD, 0)), sum(isnull(dtct.fThu_BHXH_NSD, 0)), sum(isnull(dtct.fThu_BHYT_NLD, 0)), sum(isnull(dtct.fThu_BHYT_NSD, 0)), sum(isnull(dtct.fThu_BHTN_NLD, 0)), sum(isnull(dtct.fThu_BHTN_NSD, 0)),
		sum(isnull(dcct.fThuBHXH_NLD_QTDauNam, 0)), sum(isnull(dcct.fThuBHXH_NSD_QTDauNam, 0)), sum(isnull(dcct.fThuBHYT_NLD_QTDauNam, 0)), sum(isnull(dcct.fThuBHYT_NSD_QTDauNam, 0)), sum(isnull(dcct.fThuBHTN_NLD_QTDauNam, 0)), sum(isnull(dcct.fThuBHTN_NSD_QTDauNam, 0)),
		sum(isnull(dcct.fThuBHXH_NLD_QTCuoiNam, 0)), sum(isnull(dcct.fThuBHXH_NSD_QTCuoiNam, 0)), sum(isnull(dcct.fThuBHYT_NLD_QTCuoiNam, 0)), sum(isnull(dcct.fThuBHYT_NSD_QTCuoiNam, 0)), sum(isnull(dcct.fThuBHTN_NLD_QTCuoiNam, 0)), sum(isnull(dcct.fThuBHTN_NSD_QTCuoiNam, 0)),
		sum(isnull(dcct.fTongThuBHXH_NLD, 0)), sum(isnull(dcct.fTongThuBHXH_NSD, 0)), sum(isnull(dcct.fTongThuBHYT_NLD, 0)), sum(isnull(dcct.fTongThuBHYT_NSD, 0)), sum(isnull(dcct.fTongThuBHTN_NLD, 0)), sum(isnull(dcct.fTongThuBHTN_NSD, 0)),
		sum(isnull(dcct.fThuBHXH_NLD_Tang, 0)), sum(isnull(dcct.fThuBHXH_NSD_Tang, 0)), sum(isnull(dcct.fThuBHXH_Tang, 0)), sum(isnull(dcct.fThuBHYT_NLD_Tang, 0)), sum(isnull(dcct.fThuBHYT_NSD_Tang, 0)), sum(isnull(dcct.fThuBHYT_Tang, 0)), sum(isnull(dcct.fThuBHTN_NLD_Tang, 0)), sum(isnull(dcct.fThuBHTN_NSD_Tang, 0)), sum(isnull(dcct.fThuBHTN_Tang, 0)),
		sum(isnull(dcct.fThuBHXH_NLD_Giam, 0)), sum(isnull(dcct.fThuBHXH_NSD_Giam, 0)), sum(isnull(dcct.fThuBHXH_Giam, 0)), sum(isnull(dcct.fThuBHYT_NLD_Giam, 0)), sum(isnull(dcct.fThuBHYT_NSD_Giam, 0)), sum(isnull(dcct.fThuBHYT_Giam, 0)), sum(isnull(dcct.fThuBHTN_NLD_Giam, 0)), sum(isnull(dcct.fThuBHTN_NSD_Giam, 0)), sum(isnull(dcct.fThuBHTN_Giam, 0)),
		sum(isnull(dcct.fTongCong, 0)) fTongCong,
		Null, GETDATE(), Null, @Nguoitao 
	FROM BH_DTT_BHXH_ChungTu pb
	JOIN BH_DTT_BHXH_ChungTu_ChiTiet dtct ON pb.iID_DTT_BHXH = dtct.iID_DTT_BHXH
	LEFT JOIN BH_DTT_BHXH_DieuChinh_ChiTiet dcct ON dcct.sXauNoiMa = dtct.sXauNoiMa and dcct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	WHERE dcct.iID_DTT_BHXH_DieuChinh in (SELECT * FROM f_split(@ListIdChungTuTongHop)) 
		AND dtct.iID_MaDonVi = @MaDonVi
	GROUP BY 
	  dcct.sLNS,
	  dtct.iID_MLNS, 
	  dcct.sNoiDung,
	  dcct.sXauNoiMa

	  --danh dau chung tu da tong hop
		update 
		  BH_DTT_BHXH_DieuChinh 
		set 
		  bDaTongHop = 1
		where 
		  iID_DTT_BHXH_DieuChinh in (SELECT * FROM f_split(@ListIdChungTuTongHop))
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong]    Script Date: 5/6/2024 5:56:31 PM ******/
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán

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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan

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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán
	and pbct.sXauNoiMa in
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán
	and pbct.sXauNoiMa in
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0002-0000' -- CC,CN, VCQP
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0002-0001' -- LĐHĐ

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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán
	and pbct.sXauNoiMa in
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0002-0000' -- CC,CN, VCQP
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0002-0001' -- LĐHĐ

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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán
	and pbct.sXauNoiMa in
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán
	and pbct.sXauNoiMa in
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0002-0000'  -- CC,CN, VCQP
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0002-0001'  -- LĐHĐ

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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán
	and pbct.sXauNoiMa in
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0002-0000'  -- CC,CN, VCQP
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0002-0001'  -- LĐHĐ
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán

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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0001-0000' -- Sĩ quan
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0001-0001' -- QNCN
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0001-0002' -- HSQ-BS
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán

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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan 
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán

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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán

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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_tonghop]    Script Date: 5/6/2024 5:56:31 PM ******/
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán

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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan

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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán
	and pbct.sXauNoiMa in
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán
	and pbct.sXauNoiMa in
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0002-0000' -- CC,CN, VCQP
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0002-0001' -- LĐHĐ

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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán
	and pbct.sXauNoiMa in
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0002-0000' -- CC,CN, VCQP
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0002-0001' -- LĐHĐ

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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán
	and pbct.sXauNoiMa in
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán
	and pbct.sXauNoiMa in
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0002-0000'  -- CC,CN, VCQP
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0002-0001'  -- LĐHĐ

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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán
	and pbct.sXauNoiMa in
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0002-0000'  -- CC,CN, VCQP
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0002-0001'  -- LĐHĐ
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán

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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0001-0000' -- Sĩ quan
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0001-0001' -- QNCN
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0001-0002' -- HSQ-BS
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán

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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan 
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán

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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán

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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi = @MaDonVi
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_thctdv]    Script Date: 5/6/2024 5:56:31 PM ******/
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
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and  ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán

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
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and pbct.iNamLamViec = dv.iNamLamViec
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020001' -- Khối dự toán
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and  ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020001' -- Khối dự toán

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
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and pbct.iNamLamViec = dv.iNamLamViec
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020001' -- Khối dự toán
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and  ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020002' -- Khối hạch toán

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
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and pbct.iNamLamViec = dv.iNamLamViec
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020002' -- Khối hạch toán
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and  ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020002' -- Khối hạch toán

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
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and pbct.iNamLamViec = dv.iNamLamViec
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020002' -- Khối hạch toánand dv.iTrangThai = 1
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and  ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020001' -- Khối dự toán
		and pbct.sXauNoiMa in
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and pbct.iNamLamViec = dv.iNamLamViec
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020001' -- Khối dự toán
		and pbct.sXauNoiMa in (
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and  ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020001' -- Khối dự toán
		and pbct.sXauNoiMa in
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and pbct.iNamLamViec = dv.iNamLamViec
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020001' -- Khối dự toán
		and pbct.sXauNoiMa in (
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and  ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020001' -- Khối dự toán
		and pbct.sXauNoiMa in
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and pbct.iNamLamViec = dv.iNamLamViec
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020001' -- Khối dự toán
		and pbct.sXauNoiMa in (
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and  ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán
	and pbct.sXauNoiMa in
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and pbct.iNamLamViec = dv.iNamLamViec
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020002' -- Khối hạch toán
		and pbct.sXauNoiMa in (
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and  ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán
	and pbct.sXauNoiMa in
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and pbct.iNamLamViec = dv.iNamLamViec
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020002' -- Khối hạch toán
		and pbct.sXauNoiMa in (
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and  ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán
	and pbct.sXauNoiMa in
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and pbct.iNamLamViec = dv.iNamLamViec
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020002' -- Khối hạch toán
		and pbct.sXauNoiMa in (
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and  ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán

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
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and pbct.iNamLamViec = dv.iNamLamViec
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020001' -- Khối dự toán
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and  ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán

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
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and pbct.iNamLamViec = dv.iNamLamViec
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020001' -- Khối dự toán
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and  ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán

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
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and pbct.iNamLamViec = dv.iNamLamViec
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020002' -- Khối hạch toán
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and  ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán

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
	from BH_DTT_BHXH_PhanBo_ChungTu pb
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	left join DonVi dv on pbct.iID_MaDonVi = dv.iID_MaDonVi and pbct.iNamLamViec = dv.iNamLamViec
	where 
		pbct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and pbct.iNamLamViec = @NamLamViec
		and pbct.sLNS = '9020002' -- Khối hạch toán
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]    Script Date: 5/6/2024 5:56:31 PM ******/
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán

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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán

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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán

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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán
	and pbct.sXauNoiMa in
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán
	and pbct.sXauNoiMa in
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán
	and pbct.sXauNoiMa in
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán
	and pbct.sXauNoiMa in
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán
	and pbct.sXauNoiMa in
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán
	and pbct.sXauNoiMa in
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán

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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán
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
	from BH_DTT_BHXH_PhanBo_ChungTu pb 
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_tonghop]    Script Date: 5/6/2024 5:56:31 PM ******/
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán

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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán

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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán

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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán
	and pbct.sXauNoiMa in
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán
	and pbct.sXauNoiMa in
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán
	and pbct.sXauNoiMa in
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán
	and pbct.sXauNoiMa in
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán
	and pbct.sXauNoiMa in
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán
	and pbct.sXauNoiMa in
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán

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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020001' -- Khối dự toán

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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán
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
	from BH_DTT_BHXH_ChungTu pb
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH 
	left join BH_DTT_BHXH_DieuChinh_ChiTiet ctct on ctct.iID_MaDonVi = pbct.iID_MaDonVi and ctct.sXauNoiMa = pbct.sXauNoiMa and ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(@dNgayDieuChinh as date)
	where 
	pbct.iID_MaDonVi = @MaDonVi
	and pbct.iNamLamViec = @NamLamViec
	and pbct.sLNS = '9020002' -- Khối hạch toán
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
;
GO


/****** Object:  StoredProcedure [dbo].[sp_bh_rpt_thamdinhquyettoan_can_cu_bhxh_sang_bhyt]    Script Date: 5/7/2024 4:19:14 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_rpt_thamdinhquyettoan_can_cu_bhxh_sang_bhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_rpt_thamdinhquyettoan_can_cu_bhxh_sang_bhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_rpt_thamdinhquyettoan_can_cu_bhxh_sang_bhyt]    Script Date: 5/7/2024 4:19:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_rpt_thamdinhquyettoan_can_cu_bhxh_sang_bhyt]
	@NamLamViec int,
	@MaDonVi nvarchar(max),
	@Dvt int
AS
BEGIN
	
	select
		ct.iID_MaDonVi,
		ctct.iMa,
		ctct.fQuanNhan,
		ctct.fCNVLDHD
	into #temp_bhxh_bhyt
	from BH_ThamDinhQuyetToan_ChungTu ct
	join BH_ThamDinhQuyetToan_ChungTuChiTiet ctct on ct.iID_BH_TDQT_ChungTu = ctct.iID_BH_TDQT_ChungTu
	where ctct.iMa in (256, 257)
		and ct.iNamLamViec = @NamLamViec
		and ct.iID_MaDonVi = @MaDonVi

	select distinct
		donvi.sTenDonVi,
		sothang.fQuanNhan/@Dvt fSoThangQuanNhan,
		sothang.fCNVLDHD/@Dvt fSoThangCNVLDHD,
		(sothang.fQuanNhan + sothang.fCNVLDHD)/@Dvt fTongSoThang,
		sotien.fQuanNhan/@Dvt fSoTienQuanNhan,
		sotien.fCNVLDHD/@Dvt fSoTienCNVLDHD,
		(sotien.fQuanNhan + sotien.fCNVLDHD)/@Dvt fTongSoTien
	from #temp_bhxh_bhyt temp
	join DonVi donvi on temp.iID_MaDonVi = donvi.iID_MaDonVi and donvi.iNamLamViec = @NamLamViec and donvi.iTrangThai = 1
	left join #temp_bhxh_bhyt sothang on temp.iID_MaDonVi = sothang.iID_MaDonVi and sothang.iMa = 256
	left join #temp_bhxh_bhyt sotien on temp.iID_MaDonVi = sotien.iID_MaDonVi and sotien.iMa = 257

END
GO
