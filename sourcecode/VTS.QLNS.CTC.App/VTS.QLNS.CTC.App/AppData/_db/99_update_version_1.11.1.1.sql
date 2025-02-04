/****** Object:  StoredProcedure [dbo].[sp_vdt_thongtri_detail]    Script Date: 03/06/2022 7:02:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_thongtri_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_thongtri_detail]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_trongnuoc_index]    Script Date: 03/06/2022 7:02:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_trongnuoc_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_trongnuoc_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_trongnuoc_index]    Script Date: 03/06/2022 7:02:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_nh_goithau_trongnuoc_index]
	@ILoai int,
	@IThuocMenu int
as begin
	Select 
		goiThau.iID_GoiThauID as Id,
		goiThau.iID_GoiThauGocID as IIdGoiThauGocId,
		goiThau.iID_ParentID as IIdParentId,
		goiThau.iID_NhaThauID as IIdNhaThauId,
		goiThau.iID_CacQuyetDinhID as IIdCacQuyetDinhId,
		goiThau.iID_ParentAdjustID as IIdParentAdjustId,
		goiThau.iId_KHLCNhaThau as IIdKhlcnhaThau,
		goiThau.iID_TiGiaUSD_NgoaiTeKhacID as IIdTiGiaUSDNgoaiTeKhacId,
		goiThau.iID_TiGiaUSD_VNDID as IIdTiGiaUSDVNDId,
		goiThau.iID_TiGiaUSD_EURID as IIdTiGiaUSDEURId,
		goiThau.iID_HinhThucChonNhaThauID as IIdHinhThucChonNhaThauId,
		goiThau.iID_PhuongThucDauThauID as IIdPhuongThucDauThauId,
		goiThau.iID_LoaiHopDongID as IIdLoaiHopDongId,
		--DuAn.ID as IIdDuAnId,
		goiThau.sSoQuyetDinh as SSoQuyetDinh,
		goiThau.dNgayQuyetDinh as DNgayQuyetDinh,
		goiThau.sMaGoiThau as SMaGoiThau,
		goiThau.sTenGoiThau as STenGoiThau,
		goiThau.LoaiGoiThau as LoaiGoiThau,
		goiThau.dBatDauChonNhaThau as DBatDauChonNhaThau,
		goiThau.dKetThucChonNhaThau as DKetThucChonNhaThau,
		goiThau.iThoiGianThucHien as IThoiGianThucHien,
		goiThau.fGiaGoiThauEUR as FGiaGoiThauEUR,
		goiThau.fGiaGoiThauUSD as FGiaGoiThauUSD,
		goiThau.fGiaGoiThauVND as FGiaGoiThauVND,
		goiThau.fGiaGoiThauNgoaiTeKhac as fGiaGoiThauNgoaiTeKhac,
		goiThau.bIsGoc as BIsGoc,
		goiThau.bActive as BActive,
		goiThau.iLanDieuChinh as ILanDieuChinh,
		goiThau.bIsKhoa as BIsKhoa,
		goiThau.dNgayTao as DNgayTao,
		goiThau.sNguoiTao as SNguoiTao,
		goiThau.sNguoiSua as SNguoiSua,
		goiThau.dNgaySua as DNgaySua,
		goiThau.dNgayXoa as DNgayXoa,
		goiThau.sNguoiXoa as SNguoiXoa,
		goiThau.iID_TiGiaID as IIdTiGiaId,
		goiThau.sMaNgoaiTeKhac as SMaNgoaiTeKhac,
		goiThau.bIsXoa as BIsXoa,
		DonVi.sTenDonVi as STenDonVi,
		nvc.sTenNhiemVuChi as STenChuongTrinh,
		DuAn.sTenDuAn as STenDuAn,
		HinhThucChonNhaThau.sTenHinhThucChonNhaThau as STenHinhThucChonNhaThau,
		PhuongThucChonNhaThau.sTenPhuongThucChonNhaThau as STenPhuongThucChonNhaThau,
		ChuDauTu.sTenDonVi as STenChuDauTu,
		DuAn.sDiaDiem as SDiaDiem,
		QDDauTu.fGiaTriUSD as FQDDTTongPheDuyetUSD,
		QDDauTu.fGiaTriVND as FQDDTTongPheDuyetVND,
		QDDauTu.fGiaTriEUR as FQDDTTongPheDuyetEUR,
		QDDauTu.fGiaTriNgoaiTeKhac as FQDDTTongPheDuyetNgoaiTeKhac,
		LoaiHopDong.sTenLoaiHopDong as STenHopDong,
		DuToan.fGiaTriUSD as FDTTongPheDuyetUSD,
		DuToan.fGiaTriVND as FDTTongPheDuyetVND,
		DuToan.fGiaTriEUR as FDTTongPheDuyetEUR,
		DuToan.fGiaTriNgoaiTeKhac as FDTTongPheDuyetNgoaiTeKhac,
		khttnvc.ID as IIdKHTTNhiemVuChiId,
		( SELECT COUNT ( Id ) FROM Attachment WHERE ModuleType = 405 AND ObjectId = goiThau.iID_GoiThauID ) AS TotalFiles,
		CASE
		WHEN goiThau.iID_ParentAdjustId IS NULL THEN
		'' ELSE ( SELECT TOP 1 hdpr.sMaGoiThau FROM NH_DA_GoiThau hdpr WHERE hdpr.iID_GoiThauID = goiThau.iID_ParentAdjustId ) 
		END DieuChinhTu ,
		LCNhaThau.sMoTa as SMota
	from NH_DA_KHLCNhaThau LCNhaThau
	inner join NH_DA_GoiThau goiThau
		on LCNhaThau.Id = GoiThau.iId_KHLCNhaThau
	/*inner join NH_DA_DuAn DuAn
		on LCNhaThau.iID_DuAnID = DuAn.ID
	inner join DonVi
		on DuAn.iID_DonViQuanLyID = DonVi.iID_DonVi
	inner join DM_ChuDauTu ChuDauTu
		on DuAn.iID_ChuDauTuID = ChuDauTu.iID_DonVi*/
	left join NH_DA_DuAn DuAn
		on LCNhaThau.iID_DuAnID = DuAn.ID
	left join DonVi
		on DuAn.iID_DonViQuanLyID = DonVi.iID_DonVi
	left join DM_ChuDauTu ChuDauTu
		on DuAn.iID_ChuDauTuID = ChuDauTu.iID_DonVi
	left join NH_KHTongThe_NhiemVuChi khttnvc 
		on LCNhaThau.iID_KHTT_NhiemVuChiID = khttnvc.ID 
	left join NH_DM_NhiemVuChi nvc
		on khttnvc.iID_NhiemVuChiID = nvc.ID 
	left join NH_DA_QDDauTu QDDauTu
		on LCNhaThau.iID_QDDauTuID = QDDauTu.ID
	left join NH_DA_DuToan DuToan
		on LCNhaThau.iID_DuToanID = DuToan.ID
	left join NH_DM_HinhThucChonNhaThau HinhThucChonNhaThau
		on goiThau.iID_HinhThucChonNhaThauID = HinhThucChonNhaThau.ID 
	left join NH_DM_PhuongThucChonNhaThau PhuongThucChonNhaThau
		on goiThau.iID_PhuongThucDauThauID = PhuongThucChonNhaThau.ID 
	left join NH_DM_LoaiHopDong LoaiHopDong
		on goiThau.iID_LoaiHopDongID = LoaiHopDong.iID_LoaiHopDongID 
	WHERE goiThau.iLoai = @ILoai and goiThau.iThuocMenu = @IThuocMenu
end
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_thongtri_detail]    Script Date: 03/06/2022 7:02:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_vdt_thongtri_detail]
@iIdThongTriId uniqueidentifier,
@sMaDonViQuanLy nvarchar(50),
@iLoaiThongTri int,
@iNamkeHoach int,
@dNgayThongTri DATE,
@sMaNguonVon nvarchar(max)
AS
BEGIN
	IF @iLoaiThongTri in (1,2)
	BEGIN
		SELECT
			(CASE tbl.iLoaiThanhToan WHEN 1 THEN 
					(CASE WHEN dt.colName in ('fGiaTriThanhToanTN', 'fGiaTriThanhToanNN') 
							THEN (CASE tbl.iID_NguonVonID WHEN 1 THEN 'TT_CTT_KPQP' WHEN 2 THEN 'TT_Cap_KPNN' ELSE 'TT_Cap_KPK' END)
						WHEN dt.colName in ('fGiaTriThuHoiNamTruocTN', 'fGiaTriThuHoiNamTruocNN', 'fGiaTriThuHoiNamNayTN', 'fGiaTriThuHoiNamNayNN', 'fGiaTriThuHoiUngTruocNamTruocTN', 'fGiaTriThuHoiUngTruocNamTruocNN', 'fGiaTriThuHoiUngTruocNamNayTN', 'fGiaTriThuHoiUngTruocNamNayNN')
							THEN (CASE tbl.iID_NguonVonID WHEN 1 THEN 'TT_ThuUng_KPQP' WHEN 2 THEN 'TT_ThuUng_KPNN' ELSE 'TT_ThuUng_KPK' END)
						END)
				WHEN 0 THEN (CASE tbl.iID_NguonVonID WHEN 1 THEN 'TT_TamUng_KPQP' WHEN 2 THEN 'TT_TamUng_KPNN' ELSE 'TT_TamUng_KPK' END) END) as SMaKieuThongTri,
			NULL as SSoThongTri,
			dt.FGiaTri as FSoTien,
			tbl.iID_DuAnId as IIdDuAnId,
			tbl.iID_NhaThauId as IIdNhaThauId,
			dt.iID_MucID as IIdMucId,
			dt.iID_TieuMucID as IIdTieuMucId,
			dt.iID_TietMucID as IIdTietMucId,
			dt.iID_NganhID as IIdNganhId,
			da.iID_LoaiCongTrinhID as IIdLoaiCongTrinhId,
			NULL as IIdLoaiNguonVonId,

			da.iID_CapPheDuyetID as IIdCapPheDuyetId,
			da.STenDuAn,
			ml.SLns,
			ml.SL,
			ml.SK,
			ml.SM,
			ml.STm,
			ml.STtm,
			ml.SNg,
			tbl.sTenDonViThuHuong as SDonViThuHuong INTO #tmpThanhToan
		FROM VDT_TT_DeNghiThanhToan as tbl
		INNER JOIN (
				SELECT iID_DeNghiThanhToanID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, SUM(dt.fGiaTri) as fGiaTri, colName
				from 
				(select iID_DeNghiThanhToanID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, fGiaTriThanhToanTN, fGiaTriThanhToanNN, fGiaTriThuHoiNamTruocTN, fGiaTriThuHoiNamTruocNN, fGiaTriThuHoiNamNayTN, fGiaTriThuHoiNamNayNN, fGiaTriThuHoiUngTruocNamTruocTN, fGiaTriThuHoiUngTruocNamTruocNN, fGiaTriThuHoiUngTruocNamNayTN, fGiaTriThuHoiUngTruocNamNayNN from VDT_TT_PheDuyetThanhToan_ChiTiet) as tbl
				UNPIVOT
				(fGiaTri FOR colName IN (fGiaTriThanhToanTN, fGiaTriThanhToanNN, fGiaTriThuHoiNamTruocTN, fGiaTriThuHoiNamTruocNN, fGiaTriThuHoiNamNayTN, fGiaTriThuHoiNamNayNN, fGiaTriThuHoiUngTruocNamTruocTN, fGiaTriThuHoiUngTruocNamTruocNN, fGiaTriThuHoiUngTruocNamNayTN, fGiaTriThuHoiUngTruocNamNayNN)) as dt
				GROUP BY iID_DeNghiThanhToanID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, colName
				HAVING SUM(dt.fGiaTri) <> 0
			) as dt on tbl.Id = dt.iID_DeNghiThanhToanID
		INNER JOIN VDT_DA_DuAn as da on tbl.iID_DuAnId = da.iID_DuAnID
		INNER JOIN NguonNganSach as nv on tbl.iID_NguonVonID = nv.iID_MaNguonNganSach
		LEFT JOIN NS_MucLucNganSach as ml on dt.iID_MucID = ml.iID OR dt.iID_TieuMucID = ml.iID OR dt.iID_TietMucID = ml.iID OR dt.iID_NganhID = ml.iID
		WHERE tbl.iLoaiThanhToan = (CASE WHEN @iLoaiThongTri = 1 THEN 1 ELSE 0 END)
			AND (tbl.iID_ThongTriThanhToanID IS NULL OR tbl.iID_ThongTriThanhToanID = @iIdThongTriId)
			AND tbl.iID_MaDonViQuanLy = @sMaDonViQuanLy
			AND tbl.iNamKeHoach = @iNamkeHoach
			AND CAST(tbl.dNgayDeNghi as DATE) <= CAST(@dNgayThongTri as DATE)
			AND (@sMaNguonVon IS NULL OR LOWER(nv.sMoTa) = LOWER(@sMaNguonVon))

		SELECT SMaKieuThongTri, SSoThongTri, SUM(ISNULL(FSoTien, 0)) as FSoTien, IIdDuAnId, IIdNhaThauId, IIdMucId, IIdTieuMucId, IIdTietMucId, IIdNganhId, IIdLoaiCongTrinhId, IIdLoaiNguonVonId,
			IIdCapPheDuyetId, STenDuAn, SLns, SL, SK, SM, STm, STtm, SNg, SDonViThuHuong 
		FROM #tmpThanhToan
		GROUP BY SMaKieuThongTri, SSoThongTri, IIdDuAnId, IIdNhaThauId, IIdMucId, IIdTieuMucId, IIdTietMucId, IIdNganhId, IIdLoaiCongTrinhId, IIdLoaiNguonVonId,
			IIdCapPheDuyetId, STenDuAn, SLns, SL, SK, SM, STm, STtm, SNg, SDonViThuHuong 

		DROP TABLE #tmpThanhToan
	END 
	ELSE
	BEGIN
		SELECT (CASE dt.colName WHEN 'fCapPhatTaiKhoBac' THEN 'hop_thuc' 
					ELSE 'kinh_phi' END) SMaKieuThongTri,
			NULL as SSoThongTri,
			dt.iID_DuAnId as IIdDuAnId,
			NULL as IIdNhaThauId,
			dt.iID_MucID as IIdMucId,
			dt.iID_TieuMucID as IIdTieuMucId,
			dt.iID_TietMucID as IIdTietMucId,
			dt.iID_NganhID as IIdNganhId,
			da.iID_LoaiCongTrinhID as IIdLoaiCongTrinhId,
			NULL as IIdLoaiNguonVonId,
			da.iID_CapPheDuyetID as IIdCapPheDuyetId,
			da.STenDuAn,
			ml.SLns,
			ml.SL,
			ml.SK,
			ml.SM,
			ml.STm,
			ml.STtm,
			ml.SNg,
			NULL as SDonViThuHuong INTO #tmpKHV
		FROM VDT_KHV_PhanBoVon as tbl
		INNER JOIN (
			SELECT iID_PhanBoVonID, iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, iID_LoaiCongTrinh, colName
			from 
			(select iID_PhanBoVonID, iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, iID_LoaiCongTrinh, fCapPhatTaiKhoBac, fCapPhatBangLenhChi from VDT_KHV_PhanBoVon_ChiTiet) as tbl
			UNPIVOT
			(fGiaTri FOR colName IN (fCapPhatTaiKhoBac, fCapPhatBangLenhChi)) as dt
			GROUP BY iID_PhanBoVonID, iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, iID_LoaiCongTrinh, colName
			HAVING SUM(ISNULL(fGiaTri, 0)) > 0
		) as dt on tbl.Id = dt.iID_PhanBoVonID
		
		INNER JOIN VDT_DA_DuAn as da on dt.iID_DuAnId = da.iID_DuAnID
		INNER JOIN NS_MucLucNganSach as ml on dt.iID_MucID = ml.iID OR dt.iID_TieuMucID = ml.iID OR dt.iID_TietMucID = ml.iID OR dt.iID_NganhID = ml.iID
		INNER JOIN NguonNganSach as nv on tbl.iID_NguonVonID = nv.iID_MaNguonNganSach
		WHERE tbl.iID_MaDonViQuanLy = @sMaDonViQuanLy
			AND tbl.iNamKeHoach = @iNamkeHoach
			AND CAST(tbl.dNgayQuyetDinh as DATE) <= CAST(@dNgayThongTri as DATE)
			AND (@sMaNguonVon IS NULL OR LOWER(nv.sMoTa) = LOWER(@sMaNguonVon))
			AND dt.colName = (CASE WHEN @iLoaiThongTri = 3 THEN 'fCapPhatBangLenhChi' ELSE 'fCapPhatTaiKhoBac' END)

		SELECT SMaKieuThongTri, SSoThongTri, CAST(0 as float) as FSoTien, IIdDuAnId, IIdNhaThauId, IIdMucId, IIdTieuMucId, IIdTietMucId, IIdNganhId, IIdLoaiCongTrinhId, IIdLoaiNguonVonId,
			IIdCapPheDuyetId, STenDuAn, SLns, SL, SK, SM, STm, STtm, SNg, SDonViThuHuong 
		FROM #tmpKHV
		GROUP BY SMaKieuThongTri, SSoThongTri, IIdDuAnId, IIdNhaThauId, IIdMucId, IIdTieuMucId, IIdTietMucId, IIdNganhId, IIdLoaiCongTrinhId, IIdLoaiNguonVonId,
			IIdCapPheDuyetId, STenDuAn, SLns, SL, SK, SM, STm, STtm, SNg, SDonViThuHuong 

		DROP TABLE #tmpKHV
	END
END
GO
