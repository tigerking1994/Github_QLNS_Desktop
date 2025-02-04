/****** Object:  StoredProcedure [dbo].[sp_nh_qt_taisan_thongketaisan]    Script Date: 26/09/2022 12:18:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_qt_taisan_thongketaisan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_qt_taisan_thongketaisan]
GO
/****** Object:  StoredProcedure [dbo].[sp_get_create_thongtriquyettoan_chitiet]    Script Date: 26/09/2022 12:18:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_get_create_thongtriquyettoan_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_get_create_thongtriquyettoan_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_get_create_thongtriquyettoan_chitiet]    Script Date: 26/09/2022 12:18:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_get_create_thongtriquyettoan_chitiet]
	@Id_DonVi UNIQUEIDENTIFIER = NULL,
	@Id_NhiemVuChi UNIQUEIDENTIFIER = NULL,
	@NamThongTri INT = NULL
AS
BEGIN
	SELECT
		NULL AS ID,
		QTCT.iID_KHTT_NhiemVuChiID,
		DM_NVC.sTenNhiemVuChi,
		QTCT.iID_DuAnID,
		DA.sTenDuAn,
		QTCT.iID_HopDongID,
		HD.sTenHopDong,
		QTCT.iID_ThanhToan_ChiTietID,
		TTCT.sTenNoiDungChi,
		QTCT.fDeNghiQTNamNay_USD AS fDeNghiQuyetToanNam_USD,
		QTCT.fDeNghiQTNamNay_VND AS fDeNghiQuyetToanNam_VND,
		QTCT.fThuaNopNSNN_USD AS fThuaNopTraNSNN_USD,
		QTCT.fThuaNopNSNN_VND AS fThuaNopTraNSNN_VND,
		QTCT.iID_MucLucNganSachID,
		TT.ID AS iID_ThanhToanID,
		TT.iLoaiNoiDungChi,
		MLNS.sM,
		MLNS.sTM,
		MLNS.sTTM,
		MLNS.sNG,
		QTCT.iID_ParentID
	FROM NH_QT_QuyetToanNienDo_ChiTiet AS QTCT
	
	-- Join to get data
	LEFT JOIN NH_TT_ThanhToan_ChiTiet AS TTCT ON QTCT.iID_ThanhToan_ChiTietID = TTCT.ID
	LEFT JOIN NH_TT_ThanhToan AS TT ON TTCT.iID_DeNghiThanhToanID = TT.ID
	LEFT JOIN NS_MucLucNganSach AS MLNS ON QTCT.iID_MucLucNganSachID = MLNS.iID
	LEFT JOIN NH_KHTongThe_NhiemVuChi AS NVC ON QTCT.iID_KHTT_NhiemVuChiID = NVC.ID
	LEFT JOIN NH_DM_NhiemVuChi DM_NVC ON NVC.iID_NhiemVuChiID = DM_NVC.ID
	LEFT JOIN NH_DA_DuAn AS DA ON QTCT.iID_DuAnID = DA.ID
	LEFT JOIN NH_DA_HopDong AS HD ON QTCT.iID_HopDongID = HD.ID
	
	-- Join to filter
	-- Filter by year, 
	LEFT JOIN NH_QT_QuyetToanNienDo AS QT ON QTCT.iID_QuyetToanNienDoID = QT.ID

	WHERE (@Id_DonVi IS NULL OR @Id_DonVi = '00000000-0000-0000-0000-000000000000' OR QT.iID_DonViID = @Id_DonVi)
		AND (@NamThongTri IS NULL OR QT.iNamKeHoach = @NamThongTri)
		AND (@Id_NhiemVuChi IS NULL OR @Id_NhiemVuChi = '00000000-0000-0000-0000-000000000000' OR QTCT.iID_KHTT_NhiemVuChiID = @Id_NhiemVuChi)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_qt_taisan_thongketaisan]    Script Date: 26/09/2022 12:18:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_qt_taisan_thongketaisan]
AS
BEGIN 
	SET NOCOUNT ON;
	SELECT DISTINCT iID_MaDonViID, iID_LoaiTaiSanID, iLoaiTaiSan, SUM(fSoLuong) AS fSoLuong
	INTO #temp1
	FROM Nh_qt_taisan
	GROUP BY iID_MaDonViID, iID_LoaiTaiSanID, iLoaiTaiSan;

	SELECT DISTINCT iID_MaDonViID, iID_LoaiTaiSanID, iTrangThai, SUM(fSoLuong) AS fSoLuong
	INTO #temp2
	FROM Nh_qt_taisan
	GROUP BY iID_MaDonViID, iID_LoaiTaiSanID, iTrangThai;

	SELECT DISTINCT iID_MaDonViID, iID_LoaiTaiSanID, iTinhTrangSuDung, SUM(fSoLuong) AS fSoLuong
	INTO #temp3
	FROM Nh_qt_taisan
	GROUP BY iID_MaDonViID, iID_LoaiTaiSanID, iTinhTrangSuDung;

	SELECT DISTINCT CONCAT(dv.iID_MaDonVi, ' - ', dv.STenDonVi) AS STenDonVi, lts.sMaLoaiTaiSan AS SMaTaiSan, lts.sTenLoaiTaiSan AS STenTaiSan,
		ts.iID_MaDonViID AS IIdMaDonViId, ts.iID_LoaiTaiSanID AS IIdLoaiTaiSan,
		tb1.fTaiSan1 AS FTaiSan1, tb1.fTaiSan2 AS FTaiSan2,
		tb2.fTrangThai1 AS FTrangThai1, tb2.fTrangThai2 AS FTrangThai2, tb2.fTrangThai3 AS FTrangThai3,
		tb3.fTinhTrangSuDung1 AS FTinhTrangSuDung1, tb3.fTinhTrangSuDung2 AS FTinhTrangSuDung2, tb3.fTinhTrangSuDung3 AS FTinhTrangSuDung3
	FROM Nh_qt_taisan ts
	LEFT JOIN DonVi dv on ts.iID_MaDonViID = dv.iID_DonVi
	LEFT JOIN NH_DM_LoaiTaiSan lts on ts.iID_LoaiTaiSanID = lts.ID
	INNER JOIN (
		SELECT iID_MaDonViID,iID_LoaiTaiSanID, [1] AS fTaiSan1, [2] AS fTaiSan2 FROM #temp1
		pivot(SUM(fSoLuong) FOR iLoaiTaiSan in ([1], [2])) AS pv1
	) AS tb1 ON ts.iID_MaDonViID = tb1.iID_MaDonViID AND ts.iID_LoaiTaiSanID = tb1.iID_LoaiTaiSanID
	INNER JOIN (
		SELECT iID_MaDonViID,iID_LoaiTaiSanID, [1] AS fTrangThai1, [2] AS fTrangThai2, [3] AS fTrangThai3 FROM #temp2
		pivot(SUM(fSoLuong) FOR iTrangThai in ([1], [2], [3])) AS pv2
	) AS tb2 ON ts.iID_MaDonViID = tb2.iID_MaDonViID AND ts.iID_LoaiTaiSanID = tb2.iID_LoaiTaiSanID
	INNER JOIN (
		SELECT iID_MaDonViID,iID_LoaiTaiSanID, [1] AS fTinhTrangSuDung1, [2] AS fTinhTrangSuDung2, [3] AS fTinhTrangSuDung3 FROM #temp3
		pivot(SUM(fSoLuong) FOR iTinhTrangSuDung in ([1], [2], [3])) AS pv3
	) AS tb3 ON ts.iID_MaDonViID = tb3.iID_MaDonViID AND ts.iID_LoaiTaiSanID = tb3.iID_LoaiTaiSanID
	WHERE ts.iID_ChungTuTaiSanID IS NOT NULL
	ORDER BY STenDonVi

	DROP TABLE #temp1;
	DROP TABLE #temp2;
	DROP TABLE #temp3;
END;
GO