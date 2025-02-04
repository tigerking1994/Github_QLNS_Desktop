/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]    Script Date: 1/9/2024 10:39:22 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh]    Script Date: 1/9/2024 10:39:22 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_phan_bo_dtt_dieu_chinh_chi_tiet]    Script Date: 1/9/2024 10:39:22 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_phan_bo_dtt_dieu_chinh_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_phan_bo_dtt_dieu_chinh_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_thang_luong_gan_nhat]    Script Date: 1/9/2024 10:39:22 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_get_thang_luong_gan_nhat]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_get_thang_luong_gan_nhat]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dcdtt_chi_tiet_lay_quyet_toan_thu_bhxh]    Script Date: 1/9/2024 10:39:22 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dcdtt_chi_tiet_lay_quyet_toan_thu_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dcdtt_chi_tiet_lay_quyet_toan_thu_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dcdtt_chi_tiet_lay_quyet_toan_thu_bhxh]    Script Date: 1/9/2024 10:39:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_dcdtt_chi_tiet_lay_quyet_toan_thu_bhxh] 
	@NamLamViec int,
	@IdDonVi nvarchar(100)
AS
BEGIN
		select
			ctct.sXauNoiMa,
			ctct.iID_MaDonVi,
			sum(ctct.fThu_BHXH_NLD) fThuBHXH_NLD_QTDauNam,
			sum(ctct.fThu_BHXH_NSD) fThuBHXH_NSD_QTDauNam,
			sum(ctct.fThu_BHYT_NLD) fThuBHYT_NLD_QTDauNam,
			sum(ctct.fThu_BHYT_NSD) fThuBHYT_NSD_QTDauNam,
			sum(ctct.fThu_BHTN_NLD) fThuBHTN_NLD_QTDauNam,
			sum(ctct.fThu_BHTN_NSD) fThuBHTN_NSD_QTDauNam
		from BH_QTT_BHXH_ChungTu_ChiTiet ctct
		join BH_QTT_BHXH_ChungTu ct on ctct.iID_QTT_BHXH_ChungTu = ct.iID_QTT_BHXH_ChungTu
		where ct.iNamLamViec = @NamLamViec
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iQuyNam in (3, 6) -- Quy I, II
			and ct.bIsKhoa = 1
		group by ctct.sXauNoiMa, ctct.iID_MaDonVi
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_thang_luong_gan_nhat]    Script Date: 1/9/2024 10:39:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_get_thang_luong_gan_nhat]
	@MaHieuCanBo nvarchar(50),
	@Thang int,
	@Nam int
AS
BEGIN
	select * from TL_BangLuong_Thang
	where Ma_Hieu_CanBo = @MaHieuCanBo
		--and THANG < @Thang
		and NAM <= @Nam
		and Ma_PhuCap = 'BHCN'
		and Ma_CachTL = 'CACH0'
		and Gia_Tri > 0
	order by NAM, THANG desc
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_phan_bo_dtt_dieu_chinh_chi_tiet]    Script Date: 1/9/2024 10:39:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_phan_bo_dtt_dieu_chinh_chi_tiet]
	@NamLamViec int,
	@IdDonVi nvarchar(100),
	@ngayChungTu date
AS
BEGIN
	SELECT 
		ct.iID_MLNS,
		sum(ct.fBHXH_NLD) fBHXHNLD,
		sum(ct.fBHXH_NSD) fBHXHNSD,
		sum(ct.fBHYT_NLD) fBHYTNLD,
		sum(ct.fBHYT_NSD) fBHYTNSD,
		sum(ct.fBHTN_NLD) fBHTNNLD,
		sum(ct.fBHTN_NSD) fBHTNNSD
	FROM
		(
			SELECT
				ddv.sTenDonVi,
				bhct.iID_MLNS,
				bhct.fBHXH_NLD,
				bhct.fBHXH_NSD,
				bhct.fBHYT_NLD,
				bhct.fBHYT_NSD,
				bhct.fBHTN_NLD,
				bhct.fBHTN_NSD
			FROM 
				BH_DTT_BHXH_PhanBo_ChungTu bh
			JOIN 
				BH_DTT_BHXH_PhanBo_ChungTuChiTiet bhct ON bh.iID_DTT_BHXH_PhanBo_ChungTu = bhct.iID_DTT_BHXH_ChungTu 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bhct.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bhct.iID_MaDonVi = @IdDonVi
				and bh.bKhoa = 1
				AND bh.sSoQuyetDinh IS NOT NULL
				AND bh.sSoQuyetDinh <> ''
				AND cast(bh.DngayChungTu as date) <= cast(@ngayChungTu as date)
		) ct
		group by ct.iID_MLNS
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh]    Script Date: 1/9/2024 10:39:22 AM ******/
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
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]    Script Date: 1/9/2024 10:39:22 AM ******/
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
GO
