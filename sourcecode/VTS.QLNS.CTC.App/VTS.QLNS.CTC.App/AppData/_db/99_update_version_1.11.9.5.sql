/****** Object:  StoredProcedure [dbo].[sp_nh_quyettoan_niendo_tonghop_index]    Script Date: 19/09/2022 9:32:35 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_quyettoan_niendo_tonghop_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_quyettoan_niendo_tonghop_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_quyettoan_niendo_index]    Script Date: 19/09/2022 9:32:35 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_quyettoan_niendo_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_quyettoan_niendo_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_chuyendulieu_quyettoan_index]    Script Date: 19/09/2022 9:32:35 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_chuyendulieu_quyettoan_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_chuyendulieu_quyettoan_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_chuyendulieu_quyettoan_index]    Script Date: 19/09/2022 9:32:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_chuyendulieu_quyettoan_index]
AS
BEGIN
	SELECT 
		cqt.ID AS Id,
		cqt.sSoChungTu,
		cqt.dNgayChungTu,
		cqt.iID_DonViID,
		cqt.iLoaiThoiGian,
		cqt.iThoiGian,
		cqt.sMoTa,
		cqt.iID_MaDonVi,
		cqt.sNguoiTao,
		cqt.dNgayTao,
		cqt.sNguoiSua,
		cqt.dNgaySua,
		CONCAT(DonVi.iID_MaDonVi, ' - ', ISNULL(DonVi.sTenDonVi,'')) AS STenDonVi,
		(CASE WHEN cqt.iLoaiThoiGian = 1 THEN 'Tháng'
					WHEN cqt.iLoaiThoiGian = 2 THEN 'Quý'
					ELSE ''
		END) AS STenLoaiThoiGian,
		(CASE WHEN cqt.iLoaiThoiGian = 1 THEN (CASE WHEN cqt.iThoiGian IS NOT NULL OR cqt.iThoiGian <> 0 THEN CONCAT(N'Tháng ', cqt.iThoiGian) ELSE '' END)
					WHEN cqt.iLoaiThoiGian = 2 THEN (CASE WHEN cqt.iThoiGian IS NOT NULL OR cqt.iThoiGian <> 0 THEN CONCAT(N'Quý ', cqt.iThoiGian) ELSE '' END)
					ELSE ''
		END) AS STenThoiGian
	FROM NH_QT_ChuyenQuyetToan cqt
	LEFT JOIN DonVi ON cqt.iID_DonViID = DonVi.iID_DonVi
	ORDER BY cqt.dNgayTao DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_quyettoan_niendo_index]    Script Date: 19/09/2022 9:32:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 07/05/2022
-- Description:	Lấy danh sách hiển thị đê nghị quyết toán niên độ

-- Last update: 15/09/2022
-- Description: Lọc những bản ghi tổng hợp ra khỏi danh sách.
-- =============================================
CREATE PROCEDURE [dbo].[sp_nh_quyettoan_niendo_index]
	@YearOfWork int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		qtNienDo.ID					AS Id,
		qtNienDo.iID_ParentID		AS IIdParentId,
		qtNienDo.iID_GocID			AS IIdGocId,
		qtNienDo.sSoDeNghi			AS SSoDeNghi,
		qtNienDo.dNgayDeNghi		AS DNgayDeNghi,
		qtNienDo.iNamKeHoach		AS INamKeHoach,
		qtNienDo.iID_DonViID		AS IIdDonViId,
		qtNienDo.iID_MaDonVi		AS IIdMaDonVi,
		qtNienDo.iID_NguonVonID		AS IIdNguonVonId,
		qtNienDo.iLoaiThanhToan		AS ILoaiThanhToan,
		qtNienDo.iCoQuanThanhToan	AS ICoQuanThanhToan,
		qtNienDo.iID_TiGiaID		AS IIdTiGiaId,
		qtNienDo.sMaNgoaiTeKhac		AS SMaNgoaiTeKhac,
		qtNienDo.sMoTa				AS SMoTa,
		qtNienDo.dNgayTao			AS DNgayTao,
		qtNienDo.sNguoiTao			AS SNguoiTao,
		qtNienDo.dNgaySua			AS DNgaySua,
		qtNienDo.sNguoiSua			AS SNguoiSua,
		qtNienDo.dNgayXoa			AS DNgayXoa,
		qtNienDo.sNguoiXoa			AS SNguoiXoa,
		qtNienDo.bIsActive			AS BIsActive,
		qtNienDo.bIsGoc				AS BIsGoc,
		qtNienDo.bIsKhoa			AS BIsKhoa,
		qtNienDo.iLanDieuChinh		AS ILanDieuChinh,
		qtNienDo.bIsXoa				AS BIsXoa,
		donVi.sTenDonVi				AS STenDonVi,
		nguonNganSach.sTen			AS STenNguonVon,
		qtNienDo.iID_TongHopID 		AS iID_TongHopID,
		qtNienDo.sTongHopChildID 	AS sTongHopChildID,
		CASE
			WHEN ILoaiThanhToan = 1 THEN N'Cấp kinh phí'
			WHEN ILoaiThanhToan = 2 THEN N'Tạm ứng'
			WHEN ILoaiThanhToan = 3 THEN N'Thanh toán'
			ELSE ''
		END							AS SLoaiThanhToan,
		CASE
			WHEN ICoQuanThanhToan = 1 THEN N'Kho bạc'
			WHEN ICoQuanThanhToan = 2 THEN N'Cơ quan tài chính Bộ Quốc phòng'
			ELSE ''
		END							AS SCoQuanThanhToan
	FROM NH_QT_QuyetToanNienDo qtNienDo
	LEFT JOIN DonVi donVi
		ON qtNienDo.iID_MaDonVi = donVi.iID_MaDonVi AND donVi.iNamLamViec = @YearOfWork
	LEFT JOIN NguonNganSach nguonNganSach
		ON qtNienDo.iID_NguonVonID = nguonNganSach.iID_MaNguonNganSach
	WHERE qtNienDo.sTongHopChildID IS NULL
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_quyettoan_niendo_tonghop_index]    Script Date: 19/09/2022 9:32:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: LinhND
-- Create date: 16/09/2022
-- Description:	Lấy danh sách hiển thị đê nghị quyết toán niên độ tổng hợp
-- =============================================

CREATE PROCEDURE [dbo].[sp_nh_quyettoan_niendo_tonghop_index]
	@YearOfWork int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		qtNienDo.ID					AS Id,
		qtNienDo.iID_ParentID		AS IIdParentId,
		qtNienDo.iID_GocID			AS IIdGocId,
		qtNienDo.sSoDeNghi			AS SSoDeNghi,
		qtNienDo.dNgayDeNghi		AS DNgayDeNghi,
		qtNienDo.iNamKeHoach		AS INamKeHoach,
		qtNienDo.iID_DonViID		AS IIdDonViId,
		qtNienDo.iID_MaDonVi		AS IIdMaDonVi,
		qtNienDo.iID_NguonVonID		AS IIdNguonVonId,
		qtNienDo.iLoaiThanhToan		AS ILoaiThanhToan,
		qtNienDo.iCoQuanThanhToan	AS ICoQuanThanhToan,
		qtNienDo.iID_TiGiaID		AS IIdTiGiaId,
		qtNienDo.sMaNgoaiTeKhac		AS SMaNgoaiTeKhac,
		qtNienDo.sMoTa				AS SMoTa,
		qtNienDo.dNgayTao			AS DNgayTao,
		qtNienDo.sNguoiTao			AS SNguoiTao,
		qtNienDo.dNgaySua			AS DNgaySua,
		qtNienDo.sNguoiSua			AS SNguoiSua,
		qtNienDo.dNgayXoa			AS DNgayXoa,
		qtNienDo.sNguoiXoa			AS SNguoiXoa,
		qtNienDo.bIsActive			AS BIsActive,
		qtNienDo.bIsGoc				AS BIsGoc,
		qtNienDo.bIsKhoa			AS BIsKhoa,
		qtNienDo.iLanDieuChinh		AS ILanDieuChinh,
		qtNienDo.bIsXoa				AS BIsXoa,
		donVi.sTenDonVi				AS STenDonVi,
		nguonNganSach.sTen			AS STenNguonVon,
		qtNienDo.iID_TongHopID 		AS iID_TongHopID,
		qtNienDo.sTongHopChildID 	AS sTongHopChildID,
		IIF(qtNienDo.sTongHopChildID IS NOT NULL, CAST(1 AS BIT), CAST(0 AS BIT)) AS HasChildren,
		CAST(0 AS BIT) 				AS IsShowChildren,
		CASE
			WHEN ILoaiThanhToan = 1 THEN N'Cấp kinh phí'
			WHEN ILoaiThanhToan = 2 THEN N'Tạm ứng'
			WHEN ILoaiThanhToan = 3 THEN N'Thanh toán'
			ELSE ''
		END							AS SLoaiThanhToan,
		CASE
			WHEN ICoQuanThanhToan = 1 THEN N'Kho bạc'
			WHEN ICoQuanThanhToan = 2 THEN N'Cơ quan tài chính Bộ Quốc phòng'
			ELSE ''
		END							AS SCoQuanThanhToan
	FROM NH_QT_QuyetToanNienDo qtNienDo
	LEFT JOIN DonVi donVi ON qtNienDo.iID_MaDonVi = donVi.iID_MaDonVi AND donVi.iNamLamViec = @YearOfWork
	LEFT JOIN NguonNganSach nguonNganSach ON qtNienDo.iID_NguonVonID = nguonNganSach.iID_MaNguonNganSach
	WHERE qtNienDo.sTongHopChildID IS NOT NULL OR qtNienDo.iID_TongHopID IS NOT NULL
END
GO
