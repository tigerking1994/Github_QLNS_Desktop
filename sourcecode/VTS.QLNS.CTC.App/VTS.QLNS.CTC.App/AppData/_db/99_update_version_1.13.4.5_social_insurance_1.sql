/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_thu_bhxh_index]    Script Date: 11/8/2023 11:17:29 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_thu_bhxh_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_thu_bhxh_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_get_tong_nhan_du_toan_thu_mua]    Script Date: 11/8/2023 11:17:29 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qttm_get_tong_nhan_du_toan_thu_mua]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qttm_get_tong_nhan_du_toan_thu_mua]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_tong_du_toan_nhan_dtt]    Script Date: 11/8/2023 11:17:29 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_tong_du_toan_nhan_dtt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_tong_du_toan_nhan_dtt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_tong_du_toan_nhan_dtt]    Script Date: 11/8/2023 11:17:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qtt_get_tong_du_toan_nhan_dtt]
	@NamLamViec int,
	@MaDonVi nvarchar(100)
AS
BEGIN
	select 
		ctct.iID_MLNS,
		sum(ctct.fTongCong) fTongCong
	from
		BH_DTT_BHXH_ChungTu_ChiTiet ctct
		join BH_DTT_BHXH_ChungTu ct on ctct.iID_DTT_BHXH = ct.iID_DTT_BHXH
	where ct.iNamLamViec = @NamLamViec
		and ct.iID_MaDonVi = @MaDonVi
		group by
		ctct.iID_MLNS
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_get_tong_nhan_du_toan_thu_mua]    Script Date: 11/8/2023 11:17:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qttm_get_tong_nhan_du_toan_thu_mua]
	@NamLamViec int,
	@MaDonVi nvarchar(100)
AS
BEGIN
	select 
		ctct.iID_MLNS,
		sum(ctct.fDuToan) fDuToan
	from
		BH_DTTM_BHYT_ThanNhan_ChiTiet ctct
		join BH_DTTM_BHYT_ThanNhan ct on ctct.iID_DTTM_BHYT_ThanNhan = ct.iID_DTTM_BHYT_ThanNhan
	where ct.iNamChungTu = @NamLamViec
		and ct.iID_MaDonVi = @MaDonVi
		group by
		ctct.iID_MLNS
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_thu_bhxh_index]    Script Date: 11/8/2023 11:17:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_quyet_toan_thu_bhxh_index] 
	@YearOfWork int
AS
BEGIN
	SELECT
	ct.iID_QTT_BHXH_ChungTu,
	ct.iID_MaDonVi IIDMaDonVi,
	dv.sTenDonVi AS sTenDonVi,
	ct.iNamLamViec,
	ct.iLoaiTongHop,
	ct.bDaTongHop,
	ct.bIsKhoa,
	ct.sSoChungTu,
	ct.dNgayChungTu,
	ct.iQuyNam,
	ct.iQuyNamLoai,
	ct.sQuyNamMoTa,
	ct.sMoTa,
	ct.sNguoiTao,
	ct.sNguoiSua,
	ct.dNgayTao,
	ct.dNgaySua,
	ct.sDS_MLNS SDsMlns,
	ct.sTongHop,
	ct.iQSBQNam,
	ct.fLuongChinh,
	ct.fPCChucVu,
	ct.fPCTNNghe,
	ct.fPCTNVuotKhung,
	ct.fNghiOm,
	ct.fHSBL,
	ct.fTongQTLN,
	ct.fDuToan,
	ct.fDaQuyetToan,
	ct.fConLai,
	ct.fThu_BHXH_NLD FThuBHXHNLD,
	ct.fThu_BHXH_NSD FThuBHXHNSD,
	ct.fTongSoPhaiThuBHXH,
	ct.fThu_BHYT_NLD FThuBHYTNLD,
	ct.fThu_BHYT_NSD FThuBHYTNSD,
	ct.fTongSoPhaiThuBHYT,
	ct.fThu_BHTN_NLD FThuBHTNNLD,
	ct.fThu_BHTN_NSD FThuBHTNNSD,
	ct.fTongSoPhaiThuBHTN,
	ct.fTongCong,
	(ct.fThu_BHXH_NLD + ct.fThu_BHYT_NLD + ct.fThu_BHTN_NLD) FSoPhaiThuBHXHNLD,
	(ct.fThu_BHXH_NSD + ct.fThu_BHYT_NSD + ct.fThu_BHTN_NSD) FSoPhaiThuBHXHNSD,
	(ct.fThu_BHXH_NLD + ct.fThu_BHYT_NLD + ct.fThu_BHTN_NLD + ct.fThu_BHXH_NSD + ct.fThu_BHYT_NSD + ct.fThu_BHTN_NSD) FTongPhaiThuBHXH
	
	FROM BH_QTT_BHXH_ChungTu ct
	LEFT JOIN DonVi dv
	ON ct.iID_MaDonVi = dv.iID_MaDonVi
	WHERE dv.iNamLamViec = @YearOfWork
END
;
GO
