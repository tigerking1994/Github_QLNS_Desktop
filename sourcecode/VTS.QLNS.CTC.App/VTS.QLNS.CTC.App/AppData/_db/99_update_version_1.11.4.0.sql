/****** Object:  StoredProcedure [dbo].[sp_tl_delete_capnhat]    Script Date: 26/07/2022 3:39:58 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_delete_capnhat]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_delete_capnhat]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_donvi_nam]    Script Date: 26/07/2022 3:39:58 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_donvi_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_donvi_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtricapphat_dspheduyetthanhtoan]    Script Date: 26/07/2022 3:39:58 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thongtricapphat_dspheduyetthanhtoan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thongtricapphat_dspheduyetthanhtoan]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtri_capphat_index]    Script Date: 26/07/2022 3:39:58 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thongtri_capphat_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thongtri_capphat_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_report_thongtri_capphat]    Script Date: 26/07/2022 3:39:58 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_report_thongtri_capphat]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_report_thongtri_capphat]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_nhucauchiquy_index]    Script Date: 26/07/2022 3:39:58 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_nhucauchiquy_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_nhucauchiquy_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_hopdongtrongnuoc_index]    Script Date: 26/07/2022 3:39:58 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_hopdongtrongnuoc_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_hopdongtrongnuoc_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_baocao_nhucau_chitiet]    Script Date: 26/07/2022 3:39:58 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_baocao_nhucau_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_baocao_nhucau_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_baocao_nhucau_chitiet]    Script Date: 26/07/2022 3:39:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_baocao_nhucau_chitiet]
	@idNhuCauChiQuy uniqueidentifier
AS
BEGIN
	
	SELECT chitiet.fNhuCauQuyNay_USD AS Usd,
		chitiet.fNhuCauQuyNay_VND AS Vnd,
		chitiet.fNhuCauQuyNay_EUR AS Eur, 
		chitiet.fNhuCauQuyNay_NgoaiTeKhac AS NgoaiTe,
		hopdong.fGiaTriHopDongUSD AS HopDongUsd,
		hopdong.fGiaTriHopDongVND AS HopDongVnd,
		(CASE 
			WHEN chitiet.iID_HopDongID IS NULL THEN chitiet.sNoiDung
			ELSE hopdong.sTenHopDong
		END) AS STenNoiDung ,
		hopdong.iLoai AS ILoai,
		chitiet.iID_DuAnID AS IIdDuAnId,
		(CASE
			WHEN NH_DM_NhiemVuChi.iID_ParentID IS NOT NULL THEN NH_DM_NhiemVuChi.Id
			ELSE NH_DM_NhiemVuChi.iID_ParentID
		END) AS IdNhiemVuChi,
		NH_KHTongThe_NhiemVuChi.fGiaTriKH_TTCP AS TTCP,
		NH_KHTongThe_NhiemVuChi.fGiaTriKH_BQP AS BQP,
		NH_DM_NhiemVuChi.sTenNhiemVuChi AS STenNhiemVuChi
		FROM NH_NhuCauChiQuy_ChiTiet chitiet LEFT JOIN NH_DA_HopDong hopdong
			ON chitiet.iID_HopDongID = hopdong.Id
		LEFT JOIN NH_KHTongThe_NhiemVuChi 
			ON chitiet.iID_KHTT_NhiemVuChiID = NH_KHTongThe_NhiemVuChi.ID
		LEFT JOIN NH_DM_NhiemVuChi 
			ON NH_KHTongThe_NhiemVuChi.iID_NhiemVuChiID = NH_DM_NhiemVuChi.ID
		WHERE chitiet.iID_NhuCauChiQuyID = @idNhuCauChiQuy
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_hopdongtrongnuoc_index]    Script Date: 26/07/2022 3:39:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_hopdongtrongnuoc_index]

AS BEGIN
	SELECT 
	hopdong.Id AS Id,
	hopdong.sSoHopDong AS SSoHopDong,
	hopdong.dNgayHopDong AS DNgayHopDong,
	hopdong.sTenHopDong AS STenHopDong,
	hopdong.dKhoiCongDuKien AS DKhoiCongDuKien,
	hopdong.dKetThucDuKien AS DKetThucDuKien,
	hopdong.iID_LoaiHopDongID AS IIdLoaiHopDongId,
	hopdong.iID_CacQuyetDinhID AS IIdCacQuyetDinhId,
	hopdong.iID_KHTongThe_NhiemVuChiID AS IIdKhTongTheNhiemVuChiId,
	hopdong.iID_NhaThauThucHienID AS IIdNhaThauThucHienId,
	hopdong.iID_ParentAdjustID AS IIdParentAdjustId,
	hopdong.iID_ParentID AS IIdParentId,
	hopdong.iID_GoiThauID AS IIdGoiThauId,
	hopdong.iID_TiGiaID AS IIdTiGiaId,
	hopdong.iID_DuAnID AS IIdDuAnId,
	hopdong.fGiaTriHopDongNgoaiTeKhac AS FGiaTriHopDongNgoaiTeKhac,
	hopdong.fGiaTriHopDongUSD AS FGiaTriHopDongUSD,
	hopdong.fGiaTriHopDongEUR AS FGiaTriHopDongEUR,
	hopdong.fGiaTriHopDongVND AS FGiaTriHopDongVND,
	hopdong.dNgayTao AS DNgayTao,
	hopdong.sNguoiTao AS SNguoiTao,
	hopdong.dNgaySua AS DNgaySua,
	hopdong.sNguoiSua AS SNguoiSua,
	hopdong.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
	hopdong.dNgayXoa AS DNgayXoa,
	hopdong.sNguoiXoa AS SNguoiXoa,
	hopdong.bIsActive AS BIsActive,
	hopdong.bIsGoc AS BIsGoc,
	hopdong.bIsKhoa As BIsKhoa,
	hopdong.iLanDieuChinh AS ILanDieuChinh,
	duan.sTenDuAn AS STenDuAn,
	DonVi.sTenDonVi As STenDonVi,
	quyetdinh.sSoQuyetDinh as SSoQuyeDinh,
	DonVi.iID_DonVi AS IIdDonViId,
	( SELECT COUNT ( Id ) FROM Attachment WHERE ModuleType = 406 AND ObjectId = hopdong.ID ) AS TotalFiles,
	CASE
		WHEN hopdong.iID_ParentAdjustId IS NULL THEN
		'' ELSE ( SELECT TOP 1 hdpr.sSoHopDong FROM NH_DA_HopDong hdpr WHERE hdpr.Id = hopdong.iID_ParentAdjustId ) 
	END DieuChinhTu
	FROM NH_DA_HopDong hopdong
	LEFT JOIN NH_DA_DuAn duan
		ON hopdong.iID_DuAnID = duan.ID
	LEFT JOIN DonVi
		ON duan.iID_DonViQuanLyID = DonVi.iID_DonVi
	LEFT JOIN NH_HDNK_CacQuyetDinh quyetdinh
		ON hopdong.iID_CacQuyetDinhID = quyetdinh.ID
	where hopdong.iLoai = 4
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_nhucauchiquy_index]    Script Date: 26/07/2022 3:39:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_nhucauchiquy_index]
AS BEGIN
	SELECT 
		chiquy.ID AS ID,
		chiquy.iID_ParentID AS IIdParentId,
		chiquy.iID_GocID AS IIdGocId,
		chiquy.sSoDeNghi AS SSoDeNghi,
		chiquy.dNgayDeNghi AS DNgayDeNghi,
		chiquy.iNamKeHoach AS INamKeHoach,
		chiquy.iQuy AS IQuy,
		chiquy.iID_DonViID AS IIdDonViId,
		chiquy.iID_MaDonVi AS IIdMaDonVi,
		chiquy.iID_NguonVonID AS IIdNguonVonId,
		chiquy.sNguoiLap AS SNguoiLap,
		chiquy.iID_TiGiaID AS IIdTiGiaId,
		chiquy.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
		chiquy.dNgayTao AS DNgayTao,
		chiquy.sNguoiTao AS SNguoiTao,
		chiquy.dNgaySua AS DNgaySua,
		chiquy.sNguoiSua AS SNguoiSua,
		chiquy.dNgayXoa AS DNgayXoa,
		chiquy.sNguoiXoa AS SNguoiXoa,
		chiquy.bIsActive AS BIsActive,
		chiquy.bIsGoc AS BIsGoc,
		chiquy.bIsKhoa AS BIsKhoa,
		chiquy.iLanDieuChinh AS iLanDieuChinh,
		chiquy.bIsXoa AS BIsXoa,
		( SELECT COUNT ( Id ) FROM Attachment WHERE ModuleType = 413 AND ObjectId = chiquy.ID ) AS TotalFiles,
		CONCAT(DonVi.iID_MaDonVi , ' - ', DonVi.sTenDonVi) AS STenDonVi,
		NguonNganSach.sTen AS STenNguonVon,
		chiquy.sTongHop AS STongHop
	FROM NH_NhuCauChiQuy chiquy
	LEFT JOIN DonVi 
	ON chiquy.iID_DonViID = DonVi.iID_DonVi
	LEFT JOIN NguonNganSach
	ON chiquy.iID_NguonVonID = NguonNganSach.iID_MaNguonNganSach
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_report_thongtri_capphat]    Script Date: 26/07/2022 3:39:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_report_thongtri_capphat]
	@idThongTri uniqueidentifier
AS
BEGIN

	SELECT  NS_MucLucNganSach.sM,
		NS_MucLucNganSach.sTM,
		NS_MucLucNganSach.sTTM,
		NS_MucLucNganSach.sTNG,
		NS_MucLucNganSach.sMoTa,
		NH_TT_ThanhToan_ChiTiet.fPheDuyetCapKyNay_USD AS USD,
		NH_TT_ThanhToan_ChiTiet.fPheDuyetCapKyNay_VND AS VND,
		NH_TT_ThanhToan_ChiTiet.fPheDuyetCapKyNay_EUR AS EUR,
		NH_TT_ThanhToan_ChiTiet.fPheDuyetCapKyNay_NgoaiTeKhac AS NgoaiTeKhac
	FROM NH_TT_ThanhToan_ChiTiet INNER JOIN NS_MucLucNganSach
	ON NH_TT_ThanhToan_ChiTiet.iID_MucLucNganSachID = NS_MucLucNganSach.iID
		INNER JOIN NH_TT_ThanhToan
	ON NH_TT_ThanhToan_ChiTiet.iID_DeNghiThanhToanID = NH_TT_ThanhToan.ID
		INNER JOIN NH_TT_ThongTriCapPhat_ChiTiet 
	ON NH_TT_ThanhToan.ID = NH_TT_ThongTriCapPhat_ChiTiet.iID_PheDuyetThanhToanID
		WHERE NH_TT_ThongTriCapPhat_ChiTiet.iID_ThongTriCapPhatID = @idThongTri

END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtri_capphat_index]    Script Date: 26/07/2022 3:39:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_thongtri_capphat_index]
AS
BEGIN
	SELECT 
		thongtri.ID AS Id,
		thongtri.iID_MaDonViID AS IIdMaDonViId,
		thongtri.iID_DonViID AS IIdDonViId,
		thongtri.iID_NguonVonID AS IIdNguonVonId,
		thongtri.sMaThongTri AS SMaThongTri,
		thongtri.dNgayLapThongTri AS DNgayLapThongTri,
		thongtri.iNamThucHien AS INamThucHien,
		thongtri.iID_DonViTienTeID AS IIdDonViTienTeId,
		thongtri.dNgayGhiSo AS DNgayGhiSo,
		thongtri.sTK1 AS STk1,
		thongtri.sSoCT1 AS SSoCt1,
		thongtri.sTK2 AS STk2,
		thongtri.sSoCT2 AS SSoCt2,
		thongtri.iID_TiGiaUSD_VNDID AS IIdTiGiaUsdVndid,
		thongtri.iID_TiGiaUSD_NgoaiTeKhacID AS IIdTiGiaUsdNgoaiTeKhacId,
		thongtri.fTongGiaTriNgoaiTeKhac AS FTongGiaTriNgoaiTeKhac,
		thongtri.fTongGiaTriUSD AS FTongGiaTriUsd,
		thongtri.fTongGiaTriVND AS FTongGiaTriVnd,
		thongtri.sTongGiaTri_BangChu AS STongGiaTriBangChu,
		thongtri.sNguoiTao AS SNguoiTao,
		thongtri.dNgayTao AS DNgayTao,
		thongtri.sNguoiSua AS SNguoiSua,
		thongtri.dNgaySua AS DNgaySua,
		thongtri.sNguoiXoa AS SNguoiXoa,
		thongtri.dNgayXoa AS DNgayXoa,
		thongtri.bIsActive AS BIsActive,
		thongtri.bIsGoc AS BIsGoc,
		thongtri.bIsKhoa AS BIsKhoa,
		thongtri.iLanDieuChinh AS ILanDieuChinh,
		thongtri.iID_TiGiaID AS IIdTiGiaId,
		thongtri.bIsXoa AS BIsXoa,
		thongtri.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
		CONCAT(DonVi.iID_MaDonVi, ' - ', DonVi.sTenDonVi) AS STenDonVi,
		NguonNganSach.sTen AS STenNguonVon,
		tiente.sTenTienTe AS STenTienTe
	FROM NH_TT_ThongTriCapPhat thongtri
	LEFT JOIN DonVi ON thongtri.iID_DonViID = DonVi.iID_DonVi
	LEFT JOIN NguonNganSach ON thongtri.iID_NguonVonID = NguonNganSach.iID_MaNguonNganSach
	LEFT JOIN NH_DM_LoaiTienTe tiente ON thongtri.iID_DonViTienTeID = tiente.ID
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtricapphat_dspheduyetthanhtoan]    Script Date: 26/07/2022 3:39:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_thongtricapphat_dspheduyetthanhtoan]
AS
BEGIN
	SELECT 
		thanhtoan.ID AS IIdPheDuyetThanhToanId,
		chitiet.ID AS Id,
		chitiet.iID_ThongTriCapPhatID AS IIdThongTriCapPhatId,
		chitiet.sMaOrder AS SMaOrder,
		chitiet.iTrangThai AS ITrangThai,
		CASE 
			WHEN thanhtoan.iTrangThai = 2  THEN thanhtoan.sSoDeNghi 
		END AS SSoDeNghi,
		nhiemvuchi.sTenNhiemVuChi AS STenNhiemVuChi,
		hopdong.sTenHopDong AS STenHopDong,
		CASE
			WHEN thanhtoan.iLoaiDeNghi = 1 THEN N'Cấp kinh phí'
			WHEN thanhtoan.iLoaiDeNghi = 2 THEN N'Tạm ứng'
			WHEN thanhtoan.iLoaiDeNghi = 3 THEN N'Thanh toán'
		END AS SLoaiDeNghi,
		SUM(TTchitiet.fPheDuyetCapKyNay_USD) AS FPheDuyetUsd,
		SUM(TTchitiet.fPheDuyetCapKyNay_VND) AS FPheDuyetVnd,
		SUM(TTchitiet.fPheDuyetCapKyNay_EUR) AS FPheDuyetEur,
		SUM(TTchitiet.fPheDuyetCapKyNay_NgoaiTeKhac) AS FPheDuyetNgoaiTeKhac
	FROM NH_TT_ThanhToan thanhtoan 
	LEFT JOIN NH_TT_ThongTriCapPhat_ChiTiet chitiet ON thanhtoan.ID = chitiet.iID_PheDuyetThanhToanID
	LEFT JOIN NH_DM_NhiemVuChi nhiemvuchi ON thanhtoan.iID_NhiemVuChiID = nhiemvuchi.ID
	LEFT JOIN NH_DA_HopDong hopdong ON thanhtoan.iID_HopDongID = hopdong.Id
	LEFT JOIN NH_TT_ThanhToan_ChiTiet TTchitiet ON thanhtoan.ID = TTchitiet.iID_DeNghiThanhToanID
	WHERE thanhtoan.iTrangThai = 2
	GROUP BY thanhtoan.ID, chitiet.ID, chitiet.iID_ThongTriCapPhatID, chitiet.sMaOrder, chitiet.iTrangThai, thanhtoan.iLoaiDeNghi,
	nhiemvuchi.sTenNhiemVuChi, hopdong.sTenHopDong, thanhtoan.iTrangThai, thanhtoan.sSoDeNghi
END
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_donvi_nam]    Script Date: 26/07/2022 3:39:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_donvi_nam]
	@YearOfWork int,
	@BudgetSource int,
	@DataType int,
	@LNS nvarchar(250),
	@Loai nvarchar(10)
AS
BEGIN
	SELECT dv.*
	FROM
	  (SELECT DISTINCT iID_MaDonVi
	   FROM NS_QT_ChungTuChiTiet
	   WHERE iNamLamViec=@YearOfWork
		 AND iID_MaNguonNganSach=@BudgetSource
		 AND ((@DataType=0
			   AND (fTuChi_DeNghi<>0
					OR fTuChi_PheDuyet<>0))
			  OR (@DataType=1
				  AND fTuChi_DeNghi<>0)
			  OR (@DataType=2
				  AND fTuChi_DeNghi<>0))
		 AND (@LNS IS NULL
			  OR sLNS in
				(SELECT *
				 FROM f_split(@LNS)))
	   UNION SELECT DISTINCT iID_MaDonVi
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iNamLamViec=@YearOfWork
	     AND iDuLieuNhan = 0
		 AND iPhanCap=1
		 AND iID_MaNguonNganSach=@BudgetSource
		 AND ((@DataType=0
			   AND (fTuChi<>0
					OR fHienVat<>0))
			  OR (@DataType=1
				  AND fTuChi<>0)
			  OR (@DataType=2
				  AND fHienVat<>0))
		 AND (@LNS IS NULL
			  OR sLNS in
				(SELECT *
				 FROM f_split(@LNS))) )AS ct -- lay ten don vi

	INNER JOIN
	  (SELECT *
	   FROM DonVi
	   WHERE iTrangThai=1
		 AND iLoai = @Loai
		 AND iNamLamViec=@YearOfWork) AS dv ON dv.iID_MaDonVi = ct.iID_MaDonVi
	ORDER BY iID_MaDonVi

END
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_delete_capnhat]    Script Date: 26/07/2022 3:39:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_tl_delete_capnhat]
	@idDsbangLuong NVARCHAR(MAX)
AS
BEGIN
	DELETE FROM TL_BangLuong_Thang 
	WHERE TL_BangLuong_Thang.parent IN (SELECT * FROM f_split(@idDsbangLuong))
END
GO

