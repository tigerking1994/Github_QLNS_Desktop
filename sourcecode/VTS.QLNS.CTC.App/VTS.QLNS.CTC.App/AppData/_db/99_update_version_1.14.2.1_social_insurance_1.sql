/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqBHXH_create_data_summary]    Script Date: 3/26/2024 10:56:56 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqBHXH_create_data_summary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqBHXH_create_data_summary]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_bhxh_chi_tiet]    Script Date: 3/26/2024 3:58:50 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_bhxh_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_bhxh_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqBHXH_create_data_summary]    Script Date: 3/26/2024 10:56:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtcqBHXH_create_data_summary]
@IdChungTu nvarchar(MAX),
@NguoiTao nvarchar(500),
@YearOfWork int,
@IdChungTuSummary nvarchar(MAX),
@MaDonVi nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

INSERT INTO BH_QTC_Quy_CheDoBHXH_ChiTiet(
		ID_QTC_Quy_CheDoBHXH_ChiTiet,
		iID_QTC_Quy_CheDoBHXH,
		iID_MucLucNganSach,
		sLoaiTroCap,
		dNgaySua,
		dNgayTao,
		sNguoiSua,
		sNguoiTao,
		fTienDuToanDuyet,
		iSoLuyKeCuoiQuyNay,
		fTienLuyKeCuoiQuyNay,
		iSoSQ_DeNghi,
		fTienSQ_DeNghi,
		iSoQNCN_DeNghi,
		fTienQNCN_DeNghi,
		iSoCNVCQP_DeNghi,
		fTienCNVCQP_DeNghi,
		iSoHSQBS_DeNghi,
		fTienHSQBS_DeNghi,
		iSoLDHD_DeNghi,
		fTienLDHD_DeNghi,
		iTongSo_DeNghi,
		fTongTien_DeNghi,
		fTongTien_PheDuyet,
		iNamLamViec
	)
SELECT 
	   NEWID(),
	   @IdChungTuSummary,
       iID_MucLucNganSach,
       sLoaiTroCap,
	   GETDATE(),
	   GETDATE(),
	   @NguoiTao,
	   '',
	   SUM(fTienDuToanDuyet),
	   SUM(iSoLuyKeCuoiQuyNay),
	   SUM(fTienLuyKeCuoiQuyNay),
	   SUM(iSoSQ_DeNghi),
	   SUM(fTienSQ_DeNghi),
	   SUM(iSoQNCN_DeNghi),
	   SUM(fTienQNCN_DeNghi),
	   SUM(iSoCNVCQP_DeNghi),
	   SUM(fTienCNVCQP_DeNghi),
	   SUM(iSoHSQBS_DeNghi),
	   SUM(fTienHSQBS_DeNghi),
	   SUM(iSoLDHD_DeNghi),
	   SUM(fTienLDHD_DeNghi),
	   SUM(iTongSo_DeNghi),
	   SUM(fTongTien_DeNghi),
	   SUM(fTongTien_PheDuyet),
	   @YearOfWork

FROM BH_QTC_Quy_CheDoBHXH_ChiTiet
WHERE  iID_QTC_Quy_CheDoBHXH IN
    (SELECT *
     FROM f_split(@IdChungTu))
group by iID_MucLucNganSach, sLoaiTroCap

UPDATE BH_QTC_Quy_CheDoBHXH SET iLoaiTongHop=2 , bDaTongHop=1  WHERE ID_QTC_Quy_CheDoBHXH IN (SELECT * FROM f_split(@IdChungTu));
END
;
;
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_bhxh_chi_tiet]    Script Date: 3/26/2024 3:58:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_bhxh_chi_tiet] 
	@ChungTuId nvarchar(100),
	@NamLamViec int,
	@MaDonVi nvarchar(50)
AS
BEGIN
	DECLARE @LCS float;
	SELECT @LCS = fGiaTri FROM BH_DM_CauHinhThamSo
	WHERE iNamLamViec = @NamLamViec 
	AND sMa = 'LCS' AND bTrangThai = 1;

	SELECT 
		ct.*
		FROM
			(
				SELECT
					ctct.iID_QTT_BHXH_ChungTu_ChiTiet,
					ct.iID_QTT_BHXH_ChungTu,
					ct.iID_MaDonVi,
					ddv.sTenDonVi,
					ctct.iQSBQNam,
					ctct.fLuongChinh,
					ctct.fPCChucVu,
					ctct.fPCTNNghe,
					ctct.fPCTNVuotKhung,
					ctct.fNghiOm,
					ctct.fHSBL,
					ctct.fTongQTLN,
					ctct.fDuToan,
					ctct.fDaQuyetToan,
					ctct.fConLai,
					ctct.fThu_BHXH_NLD,
					ctct.fThu_BHXH_NSD,
					ctct.fTongSoPhaiThuBHXH,
					ctct.fThu_BHYT_NLD,
					ctct.fThu_BHYT_NSD,
					ctct.fTongSoPhaiThuBHYT,
					ctct.fThu_BHTN_NLD,
					ctct.fThu_BHTN_NSD,
					ctct.fTongSoPhaiThuBHTN,
					ctct.fTongCong,
					ctct.sGhiChu,
					ctct.iID_MLNS,
					ctct.iID_MLNS_Cha,
					ctct.sXauNoiMa,
					ctct.sLNS,
					ct.iNamLamViec,
					@LCS as LCS
				FROM 
					BH_QTT_BHXH_ChungTu ct
				JOIN 
					BH_QTT_BHXH_ChungTu_ChiTiet ctct ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu 
				LEFT JOIN 
					(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON ct.iID_MaDonVi = ddv.iID_MaDonVi
				WHERE
					ct.iID_QTT_BHXH_ChungTu = @ChungTuId
			) ct;
END
;
GO

INSERT [dbo].[BH_DM_CauHinhThamSo] ([iID], [bTrangThai], [dNgaySua], [dNgayTao], [iNamLamViec], [sMa], [sMoTa], [sNguoiSua], [sNguoiTao], [sTen], [fGiaTri]) VALUES (N'e8a68ac5-313d-40c8-a24e-275a118dadc9', 1, NULL, CAST(N'2024-03-26T16:47:49.450' AS DateTime), 2024, N'LCS', NULL, NULL, N'admin', N'Lương cơ sở', 1800000)
GO
INSERT [dbo].[BH_DM_CauHinhThamSo] ([iID], [bTrangThai], [dNgaySua], [dNgayTao], [iNamLamViec], [sMa], [sMoTa], [sNguoiSua], [sNguoiTao], [sTen], [fGiaTri]) VALUES (N'a803512b-edce-4dfe-be37-6ed1fda9a860', 1, NULL, CAST(N'2024-03-25T17:57:36.050' AS DateTime), 2023, N'HESO_BHYT', NULL, NULL, N'admin', N'Hệ số thu mua thẻ BHYT thân nhân', 0.045)
GO
INSERT [dbo].[BH_DM_CauHinhThamSo] ([iID], [bTrangThai], [dNgaySua], [dNgayTao], [iNamLamViec], [sMa], [sMoTa], [sNguoiSua], [sNguoiTao], [sTen], [fGiaTri]) VALUES (N'd471e8e5-f987-4f96-a07d-7eb5e0fc4113', 1, NULL, CAST(N'2024-03-26T16:47:49.450' AS DateTime), 2024, N'HESO_BHYT', NULL, NULL, N'admin', N'Hệ số thu mua thẻ BHYT thân nhân', 0.045)
GO
INSERT [dbo].[BH_DM_CauHinhThamSo] ([iID], [bTrangThai], [dNgaySua], [dNgayTao], [iNamLamViec], [sMa], [sMoTa], [sNguoiSua], [sNguoiTao], [sTen], [fGiaTri]) VALUES (N'16687882-ca80-4475-a3be-d5fd60025704', 1, CAST(N'2024-03-26T16:46:59.387' AS DateTime), CAST(N'2024-03-26T15:57:04.717' AS DateTime), 2023, N'LCS', NULL, N'admin', N'admin', N'Lương cơ sở', 1800000)
GO
