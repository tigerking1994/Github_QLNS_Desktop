/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_trongnuoc_index]    Script Date: 7/31/2023 5:16:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_trongnuoc_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_trongnuoc_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_trongnuoc_index]    Script Date: 7/31/2023 5:16:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_nh_goithau_trongnuoc_index]
	@ILoai int,
	@IThuocMenu int
as begin
	Select DISTINCT
		goiThau.iID_GoiThauID as Id,
		goiThau.iID_GoiThauGocID as IIdGoiThauGocId,
		goiThau.iID_ParentID as IIdParentId,
		goiThau.iID_NhaThauID as IIdNhaThauId,
		case when goiThau.iCheckLuong is null then khlcnt.iID_DuToanID else   goiThau.iID_DuToanID  end  as IIdDuToanId,
		goiThau.iID_CacQuyetDinhID as IIdCacQuyetDinhId,
		goiThau.iID_ParentAdjustID as IIdParentAdjustId,
		goiThau.iId_KHLCNhaThau as IIdKhlcnhaThau,
		goiThau.iID_TiGiaUSD_NgoaiTeKhacID as IIdTiGiaUSDNgoaiTeKhacId,
		goiThau.iID_TiGiaUSD_VNDID as IIdTiGiaUSDVNDId,
		goiThau.iID_TiGiaUSD_EURID as IIdTiGiaUSDEURId,
		goiThau.fTiGiaNhap as FTiGiaNhap,
		goiThau.iID_HinhThucChonNhaThauID as IIdHinhThucChonNhaThauId,
		goiThau.iID_PhuongThucDauThauID as IIdPhuongThucDauThauId,
		goiThau.iID_LoaiHopDongID as IIdLoaiHopDongId,
		--DuAn.ID as IIdDuAnId,
		DuAn.ID as IIdDuAnId,
		goiThau.sSoQuyetDinh as SSoQuyetDinh,
		case when   goiThau.iCheckLuong is null then LCNhaThau.iID_DonViQuanLyID else  goiThau.iID_DonViQuanLyID  end  as IID_DonViQuanLyID,
		case when   goiThau.iCheckLuong is null then nvc.ID else  goiThau.iID_KHTT_NhiemVuChiID  end  as IID_KHTT_NhiemVuChiID,
		case when goiThau.iCheckLuong is null then LCNhaThau.dNgayQuyetDinh else goiThau.dNgayQuyetDinh end as DNgayQuyetDinh, --KhaiPD update 13/10/2022
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
		goiThau.sSoKeHoachDatHang as SSoKeHoachDatHang,
		goiThau.dNgayKeHoach as DNgayKeHoach,
		goiThau.iCheckLuong as IcheckLuong,
		goiThau.bActive as BActive,
		goiThau.iLanDieuChinh as ILanDieuChinh,
		goiThau.bIsKhoa as BIsKhoa,
		goiThau.dNgayTao as DNgayTao,
		goiThau.sNguoiTao as SNguoiTao,
		goiThau.sNguoiSua as SNguoiSua,
		goiThau.dNgaySua as DNgaySua,
		goiThau.dNgayXoa as DNgayXoa,
		goiThau.sNguoiXoa as SNguoiXoa,
		 case when goiThau.iCheckLuong is null then khlcnt.iID_TiGiaID else   goiThau.iID_TiGiaID  end  as IIdTiGiaId,
		 case when goiThau.iCheckLuong is null then khlcnt.sMaNgoaiTeKhac else   goiThau.sMaNgoaiTeKhac  end  as SMaNgoaiTeKhac,
		goiThau.bIsXoa as BIsXoa,
		 concat(DonVi.iID_MaDonVi,' -', DonVi.sTenDonVi )as TenDonVi,
		nvc.sTenNhiemVuChi as STenNhiemVuChi,
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
		( SELECT COUNT ( Id ) FROM Attachment WHERE ModuleType = 405 AND ObjectId = goiThau.iID_GoiThauID ) AS TotalFiles,
		CASE
		WHEN goiThau.iID_ParentAdjustId IS NULL THEN
		'' ELSE ( SELECT TOP 1 hdpr.sMaGoiThau FROM NH_DA_GoiThau hdpr WHERE hdpr.iID_GoiThauID = goiThau.iID_ParentAdjustId ) 
		END DieuChinhTu ,
		LCNhaThau.sMoTa as SMota,
		nvChi.ID as IIdKHTTNhiemVuChiId,  
		nvChi.iID_NhiemVuChiID as IIDNhiemVuChiID,
		nvChi.iID_KHTongTheID as IIdKHTongTheID,
		nvChi.STenChuongTrinh as STenChuongTrinh

		from  NH_DA_GoiThau goiThau
	left join NH_DA_KHLCNhaThau LCNhaThau
		on LCNhaThau.Id = GoiThau.iId_KHLCNhaThau
	left join NH_DA_DuAn DuAn
		on goiThau.iID_DuAnID = DuAn.ID
	left join DonVi on (LCNhaThau.iID_DonViQuanLyID = DonVi.iID_DonVi AND goiThau.iCheckLuong is null) OR (goiThau.iID_DonViQuanLyID = DonVi.iID_DonVi  AND goiThau.iCheckLuong = 1)
	left join DM_ChuDauTu ChuDauTu
		on DuAn.iID_ChuDauTuID = ChuDauTu.iID_DonVi
	left join NH_KHTongThe_NhiemVuChi khttnvc 
		on  (LCNhaThau.iID_KHTT_NhiemVuChiID = khttnvc.ID  AND goiThau.iCheckLuong is null) OR (goiThau.iID_KHTT_NhiemVuChiID = khttnvc.ID AND goiThau.iCheckLuong = 1) 
	left join NH_DM_NhiemVuChi nvc
		--on (khttnvc.iID_NhiemVuChiID = nvc.ID  AND goiThau.iCheckLuong is null) OR (goiThau.iID_KHTT_NhiemVuChiID = nvc.ID AND goiThau.iCheckLuong = 1) 
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
	left join NH_DA_KHLCNhaThau khlcnt
		on khlcnt.Id=goiThau.iId_KHLCNhaThau
	LEFT JOIN (SELECT  n.ID, n.iID_KHTongTheID, d.sTenNhiemVuChi AS STenChuongTrinh, n.iID_NhiemVuChiID
	 FROM NH_KHTongThe_NhiemVuChi AS n 
	 INNER JOIN NH_DM_NhiemVuChi AS d 
	 ON n.iID_NhiemVuChiID = d.ID
	) AS nvChi ON goiThau.iID_KHTT_NhiemVuChiID = nvChi.ID
	WHERE goiThau.iLoai = @ILoai and goiThau.iThuocMenu = @IThuocMenu
	 ORDER BY goiThau.dNgayTao DESC
end
;
GO
