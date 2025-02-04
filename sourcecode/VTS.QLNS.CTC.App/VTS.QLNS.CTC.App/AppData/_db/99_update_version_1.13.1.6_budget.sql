/****** Object:  StoredProcedure [dbo].[sp_delete_data_mlns_by_type]    Script Date: 13/09/2023 8:02:27 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_delete_data_mlns_by_type]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_delete_data_mlns_by_type]
GO
/****** Object:  StoredProcedure [dbo].[sp_check_data_used_mlns]    Script Date: 13/09/2023 8:02:27 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_check_data_used_mlns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_check_data_used_mlns]
GO
/****** Object:  StoredProcedure [dbo].[sp_check_data_used_mlns]    Script Date: 13/09/2023 8:02:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_check_data_used_mlns] 
	@YearOfWork int,
	@CodeChain nvarchar(max),
	@Type int

AS
BEGIN

	SET NOCOUNT ON;

	IF (@Type = 0)

	SELECT
		dulieu.ID,
		dulieu.ID_Parent,
		dulieu.LoaiChungTu,
		dulieu.Loai,
		dulieu.SoChungTu,
		dulieu.NgayChungTu,
		dulieu.SoQuyetDinh,
		dulieu.NgayQuyetDinh,
		dulieu.MaDonVi,
		donvi.sTenDonVi AS TenDonVi,
		dulieu.MoTa,
		dulieu.SoTien
	FROM (
	SELECT 
	t1.iID_CTDTDauNamChiTiet AS ID,
	t3.iID_CTDTDauNam AS ID_Parent,
	N'DTDN' AS LoaiChungTu,
	'DU_TOAN_DAU_NAM' AS Loai,
	t3.sSoChungTu AS SoChungTu,
	t3.dNgayChungTu AS NgayChungTu,
	'' AS SoQuyetDinh,
	NULL AS NgayQuyetDinh,
	t1.iID_MaDonVi AS MaDonVi,
	t3.sMoTa AS MoTa,
	(t1.fTuChi + t1.fHienVat + t1.fHangNhap + t1.fHangMua + t1.fPhanCap + ISNULL(t2.fTuChi, 0)) AS SoTien
	FROM NS_DTDauNam_ChungTuChiTiet t1
	LEFT JOIN NS_DTDauNam_PhanCap t2 ON t1.iID_CTDTDauNamChiTiet = t2.iID_CTDTDauNamChiTiet
	JOIN NS_DTDauNam_ChungTu t3 ON t1.iID_CTDTDauNam = t3.iID_CTDTDauNam
	WHERE t1.iNamLamViec = @YearOfWork AND t1.sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))

	UNION

	SELECT
	t1.iID_DTCTChiTiet AS ID,
	t2.iID_DTChungTu AS ID_Parent,
	N'Nhận DT' AS LoaiChungTu,
	'NHAN_DU_TOAN' AS Loai,
	t2.sSoChungTu AS SoChungTu,
	t2.dNgayChungTu AS NgayChungTu,
	t2.sSoQuyetDinh AS SoQuyetDinh,
	t2.dNgayQuyetDinh AS NgayQuyetDinh,
	t1.iID_MaDonVi AS MaDonVi,
	t2.sMoTa AS MoTa,
	(t1.fTuChi + t1.fHienVat + t1.fHangNhap + t1.fHangMua + t1.fPhanCap) AS SoTien
	FROM NS_DT_ChungTuChiTiet t1
	JOIN NS_DT_ChungTu t2 ON t1.iID_DTChungTu = t2.iID_DTChungTu AND t2.iLoai = 0 AND t1.iDuLieuNhan = 0
	WHERE t1.iNamLamViec = @YearOfWork AND t1.sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))

	UNION

	SELECT
	t1.iID_DTCTChiTiet AS ID,
	t2.iID_DTChungTu AS ID_Parent,
	N'Phân bổ DT' AS LoaiChungTu,
	'PHAN_BO_DU_TOAN' AS Loai,
	t2.sSoChungTu AS SoChungTu,
	t2.dNgayChungTu AS NgayChungTu,
	t2.sSoQuyetDinh AS SoQuyetDinh,
	t2.dNgayQuyetDinh AS NgayQuyetDinh,
	t1.iID_MaDonVi AS MaDonVi,
	t2.sMoTa AS MoTa,
	(t1.fTuChi + t1.fHienVat + t1.fHangNhap + t1.fHangMua + t1.fPhanCap) AS SoTien
	FROM NS_DT_ChungTuChiTiet t1
	JOIN NS_DT_ChungTu t2 ON t1.iID_DTChungTu = t2.iID_DTChungTu AND t2.iLoai = 1
	WHERE t1.iNamLamViec = @YearOfWork AND t1.sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))

	UNION

	SELECT
	t1.iID_CTNganhChiTiet AS ID,
	t3.iID_CTNganh AS ID_Parent, 
	N'Phân cấp NS ngành' AS LoaiChungTu,
	'PHAN_CAP_NGAN_SACH_NGANH' AS Loai,
	t3.sSoChungTu AS SoChungTu,
	t3.dNgayChungTu AS NgayChungTu,
	t3.sSoCongVan AS SoQuyetDinh,
	t3.dNgayQuyetDinh AS NgayQuyetDinh,
	t1.iiD_MaDonVi AS MaDonVi,
	t3.sMoTa AS MoTa,
	(t1.fTuChi + t1.fHangNhap + t1.fHangMua + t1.fPhanCap + t2.fHienVat + t2.fPhanCap) AS SoTien
	FROM NS_Nganh_ChungTuChiTiet t1
	LEFT JOIN NS_Nganh_ChungTuChiTiet_PhanCap t2 ON t1.iID_CTNganhChiTiet = t2.iID_CTNganhChiTiet
	JOIN NS_Nganh_ChungTu t3 ON t1.iID_CTNganh = t3.iID_CTNganh
	WHERE t1.iNamLamViec = @YearOfWork AND t1.sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))

	) dulieu
	LEFT JOIN DonVi donvi ON dulieu.MaDonVi = donvi.iID_MaDonVi AND donvi.iNamLamViec = @YearOfWork
	ORDER BY LoaiChungTu, NgayChungTu, SoChungTu
	
	ELSE IF (@Type = 1)

	SELECT
		dulieu.ID,
		dulieu.ID_Parent,
		dulieu.SoChungTu,
		dulieu.NgayChungTu,
		dulieu.MaDonVi,
		donvi.sTenDonVi AS TenDonVi,
		dulieu.ThangQuy,
		dulieu.Loai,
		dulieu.LoaiQuyetToan,
		dulieu.MoTa,
		dulieu.SoTien
	FROM (
	SELECT
	t1.iID_QTCTChiTiet AS ID,
	t2.iID_QTChungTu AS ID_Parent,
	t2.sSoChungTu AS SoChungTu,
	t2.dNgayChungTu AS NgayChungTu,
	t2.iID_MaDonVi AS MaDonVi,
	t2.iThangQuy AS ThangQuy,
	'QUYET_TOAN' AS Loai,
	CASE
		WHEN t2.sLoai = '101' THEN N'Thường xuyên'
		WHEN t2.sLoai = '1' THEN N'Quốc phòng'
		WHEN t2.sLoai = '2' THEN N'Nhà nước'
		WHEN t2.sLoai = '3' THEN N'Ngoại hối'
		WHEN t2.sLoai = '4' THEN N'Kinh phí khác'
		ELSE ''
	END AS LoaiQuyetToan,
	t2.sMoTa AS MoTa,
	t1.fTuChi_PheDuyet AS SoTien
	FROM NS_QT_ChungTuChiTiet t1
	JOIN NS_QT_ChungTu t2 ON t1.iID_QTChungTu = t2.iID_QTChungTu
	WHERE t1.iNamLamViec = @YearOfWork AND t1.sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))
	) dulieu
	LEFT JOIN DonVi donvi ON dulieu.MaDonVi = donvi.iID_MaDonVi AND donvi.iNamLamViec = @YearOfWork
	ORDER BY LoaiQuyetToan, NgayChungTu, SoChungTu

	ELSE

	SELECT
		dulieu.ID,
		dulieu.ID_Parent,
		dulieu.SoChungTu,
		dulieu.NgayChungTu,
		dulieu.MaDonVi,
		donvi.sTenDonVi AS TenDonVi,
		dulieu.Loai,
		dulieu.MoTa,
		dulieu.SoTien,
		danhmuc.sTen as LoaiCapPhat
	FROM (
	SELECT
	t1.iID_CTCapPhatChiTiet AS ID,
	t2.iID_CTCapPhat AS ID_Parent,
	t2.sSoChungTu AS SoChungTu,
	t2.dNgayChungTu AS NgayChungTu,
	t2.sDSID_MaDonVi AS MaDonVi,
	'CAP_PHAT' AS Loai,
	t2.sMoTa AS MoTa,
	t2.iID_MaDMCapPhat,
	t1.fTuChi AS SoTien
	FROM NS_CP_ChungTuChiTiet t1
	JOIN NS_CP_ChungTu t2 ON t1.iID_CTCapPhat = t2.iID_CTCapPhat
	WHERE t1.iNamLamViec = @YearOfWork AND t1.sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))
	) dulieu
	LEFT JOIN DonVi donvi ON dulieu.MaDonVi = donvi.iID_MaDonVi AND donvi.iNamLamViec = @YearOfWork
	LEFT JOIN 
		(SELECT * FROM CP_DanhMuc WHERE iNamLamViec = @YearOfWork) danhmuc 
	ON danhmuc.iID_MaDMCapPhat = dulieu.iID_MaDMCapPhat
	ORDER BY LoaiCapPhat, NgayChungTu, SoChungTu
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_delete_data_mlns_by_type]    Script Date: 13/09/2023 8:02:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_delete_data_mlns_by_type] 
	@CodeChain nvarchar(max),
	@Type nvarchar(max),
	@VoucherID nvarchar(max)

AS
BEGIN

	SET NOCOUNT ON;

	IF (@Type = 'NHAN_DU_TOAN')
	DELETE NS_DT_ChungTuChiTiet 
	WHERE sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))
	AND iID_DTCTChiTiet = @VoucherID

	IF (@Type = 'PHAN_BO_DU_TOAN')
	DELETE NS_DT_ChungTuChiTiet 
	WHERE sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))
	AND iID_DTCTChiTiet = @VoucherID

	IF (@Type = 'DU_TOAN_DAU_NAM')
	BEGIN
	DELETE NS_DTDauNam_ChungTuChiTiet 
	WHERE sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))
	AND iID_CTDTDauNamChiTiet = @VoucherID    
	
	DELETE NS_DTDauNam_PhanCap
	WHERE sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))
	AND iID_CTDTDauNamChiTiet = @VoucherID
	END

	IF (@Type = 'PHAN_CAP_NGAN_SACH_NGANH')
	BEGIN
	DELETE NS_Nganh_ChungTuChiTiet 
	WHERE sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))
	AND iID_CTNganhChiTiet = @VoucherID

	DELETE NS_Nganh_ChungTuChiTiet_PhanCap
	WHERE sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))
	AND iID_CTNganhChiTiet = @VoucherID
	END

	IF (@Type = 'QUYET_TOAN')
	DELETE NS_QT_ChungTuChiTiet 
	WHERE sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))
	AND iID_QTCTChiTiet = @VoucherID

	IF (@Type = 'CAP_PHAT')
	DELETE NS_CP_ChungTuChiTiet 
	WHERE sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))
	AND iID_CTCapPhatChiTiet = @VoucherID

END
;
;
;
GO
