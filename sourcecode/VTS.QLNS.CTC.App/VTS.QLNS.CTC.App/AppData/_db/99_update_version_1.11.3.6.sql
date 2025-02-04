/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_trongnuoc_index]    Script Date: 22/07/2022 8:10:08 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_trongnuoc_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_trongnuoc_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_trongnuoc_index]    Script Date: 22/07/2022 8:10:08 AM ******/
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
		DuAn.ID as IIdDuAnId,
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
