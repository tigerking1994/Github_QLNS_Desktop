/****** Object:  StoredProcedure [dbo].[sp_vdt_thongtinduan_index]    Script Date: 11/28/2023 11:20:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_thongtinduan_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_thongtinduan_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_report_cap_phat_thanh_toan_new]    Script Date: 11/28/2023 11:20:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_report_cap_phat_thanh_toan_new]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_report_cap_phat_thanh_toan_new]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_insert_phanbovonchitiet_new]    Script Date: 11/28/2023 11:20:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_insert_phanbovonchitiet_new]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_insert_phanbovonchitiet_new]
GO
/****** Object:  UserDefinedTableType [dbo].[t_tbl_phanbovonchitiet7]    Script Date: 11/28/2023 11:20:10 AM ******/
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N't_tbl_phanbovonchitiet7' AND ss.name = N'dbo')
DROP TYPE [dbo].[t_tbl_phanbovonchitiet7]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_thongtri_detail]    Script Date: 11/28/2023 11:20:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_thongtri_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_thongtri_detail]
GO
/****** Object:  UserDefinedTableType [dbo].[t_tbl_phanbovonchitiet7]    Script Date: 11/28/2023 11:20:10 AM ******/
CREATE TYPE [dbo].[t_tbl_phanbovonchitiet7] AS TABLE(
	[iID_PhanBoVonID] [uniqueidentifier] NULL,
	[iID_DuAnID] [uniqueidentifier] NULL,
	[sTrangThaiDuAnDangKy] [nvarchar](200) NULL,
	[fGiaTrDeNghi] [float] NULL,
	[fGiaTrPhanBo] [float] NULL,
	[fGiaTriThuHoi] [float] NULL,
	[iID_DonViTienTeID] [uniqueidentifier] NULL,
	[iID_TienTeID] [uniqueidentifier] NULL,
	[fTiGiaDonVi] [float] NULL,
	[fTiGia] [float] NULL,
	[sGhiChu] [nvarchar](max) NULL,
	[iID_LoaiNguonVonID] [uniqueidentifier] NULL,
	[sLNS] [nvarchar](50) NULL,
	[sL] [nvarchar](50) NULL,
	[sK] [nvarchar](50) NULL,
	[sM] [nvarchar](50) NULL,
	[sTM] [nvarchar](50) NULL,
	[sTTM] [nvarchar](50) NULL,
	[sNG] [nvarchar](50) NULL,
	[sXauNoiMa] [nvarchar](100) NULL,
	[fCapPhatTaiKhoBac] [float] NULL,
	[fCapPhatBangLenhChi] [float] NULL,
	[fGiaTriThuHoiNamTruocKhoBac] [float] NULL,
	[fGiaTriThuHoiNamTruocLenhChi] [float] NULL,
	[fCapPhatTaiKhoBacDc] [float] NULL,
	[fCapPhatBangLenhChiDc] [float] NULL,
	[fGiaTriThuHoiNamTruocKhoBacDc] [float] NULL,
	[fGiaTriThuHoiNamTruocLenhChiDc] [float] NULL,
	[iID_Parent] [uniqueidentifier] NULL,
	[ILoaiDuAn] [int] NULL,
	[IIdLoaiCongTrinh] [uniqueidentifier] NULL,
	[fThanhToanDeXuat] [float] NULL,
	[iID_DuAn_HangMucID] [uniqueidentifier] NULL
)
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_insert_phanbovonchitiet_new]    Script Date: 11/28/2023 11:20:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_vdt_insert_phanbovonchitiet_new]
@bIsEdit bit,
@sUserLogin nvarchar(100),
@tbl_PhanBoVonChiTiet t_tbl_phanbovonchitiet7 READONLY,
@sTypeError int OUTPUT
AS
BEGIN
	set @sTypeError = 0
	DECLARE @iIdPhanBoVon uniqueidentifier = (SELECT TOP(1) iID_PhanBoVonID FROM @tbl_PhanBoVonChiTiet)
	DECLARE @iNamKeHoach int = (SELECT TOP(1)iNamKeHoach FROM VDT_KHV_PhanBoVon WHERE id = @iIdPhanBoVon)
	
	--DECLARE @lstIdParent nvarchar(max);


	DECLARE @iCountPass int =  (SELECT COUNT(tbl.sLNS)
									FROM @tbl_PhanBoVonChiTiet as tbl
									INNER JOIN NS_MucLucNganSach as ml on ml.sLNS = tbl.sLNS
																	AND ISNULL(tbl.sL, '') = ml.sL 
																	AND ISNULL(tbl.sK, '') = ml.sK
																	AND ISNULL(tbl.sM, '')= ml.sM 
																	AND ISNULL(tbl.sTM, '') = ml.sTM 
																	--AND ISNULL(tbl.sTTM, '') = ml.sTTM
																	--AND ISNULL(tbl.sNG, '') = ml.sNG 
																	AND ml.iNamLamViec = @iNamKeHoach
																	--AND ml.sXauNoiMa = (tbl.sLNS +'-' + tbl.sL+'-' + tbl.sK+'-' + tbl.sM+'-' + tbl.sTM)
																	AND ml.sXauNoiMa = tbl.sXauNoiMa

									)
	DECLARE @iCountInput int = (SELECT COUNT(*)
								FROM @tbl_PhanBoVonChiTiet )

	IF(@iCountPass < @iCountInput) 
	BEGIN
		SET @sTypeError = 1
		RETURN
	END

	IF(@bIsEdit = 1)
	BEGIN 
		DELETE VDT_KHV_PhanBoVon_ChiTiet WHERE iID_PhanBoVonID = @iIdPhanBoVon
		--select @lstIdParent = (cast(pbvct1.iId_Parent as nvarchar(1000)) + ',') from VDT_KHV_PhanBoVon_ChiTiet pbvct1 WHERE iID_PhanBoVonID = @iIdPhanBoVon and pbvct1.iId_Parent is not null
		--delete VDT_KHV_PhanBoVon_ChiTiet where iID_PhanBoVonID in (select * from dbo.splitstring(@lstIdParent))
	END

	INSERT INTO VDT_KHV_PhanBoVon_ChiTiet(Id, iID_PhanBoVonID, iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, sTrangThaiDuAnDangKy, fGiaTrDeNghi, fGiaTrPhanBo, fGiaTriThuHoi, 
											--new--
											fCapPhatTaiKhoBac, fCapPhatBangLenhChi,fGiaTriThuHoiNamTruocKhoBac, fGiaTriThuHoiNamTruocLenhChi, fThanhToanDeXuat,
											iID_DonViTienTeID, iID_TienTeID, fTiGiaDonVi, fTiGia, sGhiChu, LNS, L, K, M, TM, TTM, NG, fCapPhatTaiKhoBacDc, fCapPhatBangLenhChiDc, fGiaTriThuHoiNamTruocKhoBacDc,
											fGiaTriThuHoiNamTruocLenhChiDc, iId_Parent, bActive, ILoaiDuAn, iID_LoaiCongTrinh, iID_DuAn_HangMucID)
	SELECT NEWID(), iID_PhanBoVonID, iID_DuAnID, 
			(CASE WHEN ISNULL(tbl.sTM, '') = '' AND ISNULL(tbl.sTTM, '') = '' AND ISNULL(tbl.sNG, '') = '' THEN ml.iID ELSE NULL END) as iID_MucID, 
			(CASE WHEN ISNULL(tbl.sTM, '') <> '' AND ISNULL(tbl.sTTM, '') = '' AND ISNULL(tbl.sNG, '') = '' THEN ml.iID ELSE NULL END) as iID_TieuMucID,
			(CASE WHEN ISNULL(tbl.sTM, '') <> '' AND ISNULL(tbl.sTTM, '') <> '' AND ISNULL(tbl.sNG, '') = '' THEN ml.iID ELSE NULL END) as iID_TietMucID,
			(CASE WHEN ISNULL(tbl.sTM, '') <> '' AND ISNULL(tbl.sTTM, '') <> '' AND ISNULL(tbl.sNG, '') <> '' THEN ml.iID ELSE NULL END) as iID_NganhID,
			sTrangThaiDuAnDangKy, fGiaTrDeNghi, fGiaTrPhanBo, fGiaTriThuHoi, 
			--new--
			fCapPhatTaiKhoBac, fCapPhatBangLenhChi,fGiaTriThuHoiNamTruocKhoBac, fGiaTriThuHoiNamTruocLenhChi, fThanhToanDeXuat,
			iID_DonViTienTeID, iID_TienTeID, fTiGiaDonVi, fTiGia, sGhiChu,
			tbl.sLNS, tbl.sL, tbl.sK, tbl.sM, tbl.sTM, tbl.sTTM, tbl.sNG,
			tbl.fCapPhatTaiKhoBacDc, tbl.fCapPhatBangLenhChiDc, tbl.fGiaTriThuHoiNamTruocKhoBacDc, tbl.fGiaTriThuHoiNamTruocLenhChiDc, tbl.iID_Parent, 1, tbl.ILoaiDuAn, tbl.IIdLoaiCongTrinh, tbl.IID_DuAn_HangMucID
	FROM @tbl_PhanBoVonChiTiet as tbl
	INNER JOIN NS_MucLucNganSach as ml on ml.sLNS = tbl.sLNS 
									AND ISNULL(TBL.SL, '') = ML.SL 
									AND ISNULL(TBL.SK, '') = ML.SK
									AND ISNULL(TBL.SM, '')= ML.SM 
									AND ISNULL(TBL.STM, '') = ML.STM 
									--AND ISNULL(tbl.sTTM, '') = ml.sTTM 
	  						--	    AND ISNULL(tbl.sNG, '') = ml.sNG 
									AND ml.iNamLamViec = @iNamKeHoach
									--AND ml.sXauNoiMa = (tbl.sLNS +'-' + tbl.sL+'-' + tbl.sK+'-' + tbl.sM+'-' + tbl.sTM)
									AND ml.sXauNoiMa = tbl.sXauNoiMa

														
									
	DECLARE @fGiaTriDeNghi float = (SELECT SUM(fGiaTrDeNghi) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fGiaTriThuHoi float = (SELECT SUM(fGiaTriThuHoi) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fGiaTrPhanBo float = (SELECT SUM(fGiaTrPhanBo) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fCapPhatTaiKhoBac float = (SELECT SUM(fCapPhatTaiKhoBac) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fCapPhatBangLenhChi float = (SELECT SUM(fCapPhatBangLenhChi) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fGiaTriThuHoiNamTruocKhoBac float = (SELECT SUM(fGiaTriThuHoiNamTruocKhoBac) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fGiaTriThuHoiNamTruocLenhChi float = (SELECT SUM(fGiaTriThuHoiNamTruocLenhChi) FROM @tbl_PhanBoVonChiTiet)

	SELECT @fGiaTriDeNghi = (@fGiaTriDeNghi + ISNULL(fGiaTrDeNghi, 0)),
			@fGiaTriThuHoi = (@fGiaTriThuHoi + ISNULL(fGiaTriThuHoi, 0)),
			@fGiaTrPhanBo = (@fGiaTrPhanBo + ISNULL(fGiaTrPhanBo, 0)),
			@fCapPhatTaiKhoBac = (@fCapPhatTaiKhoBac + ISNULL(fCapPhatTaiKhoBac, 0)),
			@fCapPhatBangLenhChi = (@fCapPhatBangLenhChi + ISNULL(fCapPhatBangLenhChi, 0)),
			@fGiaTriThuHoiNamTruocKhoBac = (@fGiaTriThuHoiNamTruocKhoBac + ISNULL(fGiaTriThuHoiNamTruocKhoBac, 0)),
			@fGiaTriThuHoiNamTruocLenhChi = (@fGiaTriThuHoiNamTruocLenhChi + ISNULL(fGiaTriThuHoiNamTruocLenhChi, 0))

	FROM VDT_KHV_PhanBoVon 
	WHERE Id = (SELECT iID_ParentId FROM VDT_KHV_PhanBoVon WHERE Id = @iIdPhanBoVon)

	UPDATE VDT_KHV_PhanBoVon
	set 
	fGiaTrDeNghi = @fGiaTriDeNghi,
	fGiaTriThuHoi = @fGiaTriThuHoi,
	fGiaTrPhanBo = @fGiaTrPhanBo,
	fCapPhatTaiKhoBac = @fCapPhatTaiKhoBac,
	fCapPhatBangLenhChi = @fCapPhatBangLenhChi,
	fGiaTriThuHoiNamTruocKhoBac = @fGiaTriThuHoiNamTruocKhoBac,
	fGiaTriThuHoiNamTruocLenhChi = @fGiaTriThuHoiNamTruocLenhChi
	WHERE Id = @iIdPhanBoVon

	UPDATE da
	SET
		sTrangThaiDuAn = 'THUC_HIEN'
	FROM @tbl_PhanBoVonChiTiet as tbl
	INNER JOIN VDT_DA_DuAn as da on tbl.iID_DuAnID = da.iID_DuAnID
	WHERE da.sTrangThaiDuAn = 'KhoiTao'
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_report_cap_phat_thanh_toan_new]    Script Date: 11/28/2023 11:20:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_vdt_report_cap_phat_thanh_toan_new]
@Id varchar(MAX),
@NamLamViec int,
@dvt int
AS 
BEGIN

	SELECT duan.sTenDuAn AS TenDuAn,
		duan.sMaDuAn AS MaDuAn,
		donvi.sTenDonVi AS TenDonVi,
		chudadutu.sTenDonVi AS TenChuDauTu,
		hopdong.sSoHopDong as TenHopDong,
		hopdong.dNgayHopDong AS NgayHopDong,
		hopdong.fTienHopDong/@dvt AS GiaTriHopDong,
		thanhtoan.sSoDeNghi AS SoDeNghi,
		thanhtoan.iLoaiThanhToan AS LoaiThanhToan,
		nguonvon.sTen AS TenNguonVon,
		thanhtoan.iNamKeHoach AS NamKeHoach,
		isnull(thanhtoan.fGiaTriThanhToanTN, 0) AS ThanhToanTN,
		isnull(thanhtoan.fGiaTriThanhToanNN, 0) AS ThanhToanNN,
		(isnull(thanhtoan.fGiaTriThuHoiTN, 0) + isnull(thanhtoan.fGiaTriThuHoiUngTruocTN, 0)) AS ThuHoiTN,
		(isnull(thanhtoan.fGiaTriThuHoiNN, 0) + isnull(thanhtoan.fGiaTriThuHoiUngTruocNN, 0)) AS ThuHoiNN,
		isnull(thanhtoan.fThueGiaTriGiaTang, 0) AS ThueGiaTriGiaTang,
		isnull(thanhtoan.fChuyenTienBaoHanh, 0) AS ChuyenTienBaoHanh,
		thanhtoan.sGhiChu as NoiDung,
		nhathau.sTenNhaThau AS TenNhaThau,
		nhathau.sSoTaiKhoan AS SoTaiKhoanNhaThau,
		pbv.dNgayQuyetDinh as khvNgayQuyetDinh,
		thanhtoan.dNgayDeNghi as thanhtoanNgayDeNghi,
		duan.sBanQuanLyDuAn as SBanQuanLyDuAn
	FROM VDT_TT_DeNghiThanhToan thanhtoan
	LEFT JOIN VDT_DA_DuAn duan ON thanhtoan.iID_DuAnId = duan.iID_DuAnID
	LEFT JOIN
	  (SELECT *
	   FROM DonVi
	   WHERE iNamLamViec = @NamLamViec ) donvi ON thanhtoan.iID_MaDonViQuanLy = donvi.iID_MaDonVi
	left join VDT_KHV_PhanBoVon pbv on thanhtoan.iID_PhanBoVonID = pbv.Id
	LEFT JOIN VDT_DA_TT_HopDong hopdong ON thanhtoan.iID_HopDongId = hopdong.Id
	LEFT JOIN NguonNganSach nguonvon ON thanhtoan.iID_NguonVonID = nguonvon.iID_MaNguonNganSach
	LEFT JOIN DM_ChuDauTu chudadutu ON chudadutu.iID_DonVi = duan.iID_ChuDauTuID
	LEFT JOIN VDT_DM_NhaThau nhathau on thanhtoan.iID_NhaThauId = nhathau.Id
	WHERE thanhtoan.Id = @Id
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_thongtinduan_index]    Script Date: 11/28/2023 11:20:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_vdt_thongtinduan_index] 
	
AS
BEGIN
	SELECT
		da.iID_DuAnID as Id,
		da.sTenDuAn as STenDuAn,
		da.sMaDuAn as SMaDuAn,
		ISNULL(da.sMaDuAn,'') + '-' + ISNULL(da.sTenDuAn,'') as NoiDung,
		da.sMaKetNoi,
		pcda.sTen as TenCapPheDuyet,
		da.iID_MaDonViThucHienDuAnID as IIDMaDonViQuanLy,
		da.iID_MaChuDauTuID as IIDMaChuDauTuID,
		da.iID_DonViThucHienDuAnID as IIdDonViQuanLyId,
		da.iID_ChuDauTuID as IIdChuDauTuId,
		da.sMucTieu as SMucTieu,
		da.sQuyMo as SQuyMo,
		da.sDiaDiem as SDiaDiem,
		da.sSuCanThietDauTu as SSuCanThietDauTu,
		da.sDienTichSuDungDat as SDienTichSuDungDat,
		da.sNguonGocSuDungDat as SNguonGocSuDungDat,
		da.fTongMucDauTuDuKien as FTongMucDauTuDuKien,
		da.fTongMucDauTuThamDinh as FTongMucDauTuThamDinh,
		da.fTongMucDauTu as FTongMucDauTu,
		da.fHanMucDauTu as FHanMucDauTu,
		da.iID_NhomDuAnID as IIdNhomDuAnId,
		da.iID_NganhDuAnID as IIdNganhDuAnId,
		da.iID_LoaiDuAnId as IIdLoaiDuAnId,
		da.iID_HinhThucDauTuID as IIdHinhThucDauTuId,
		da.iID_HinhThucQuanLyID as IIdHinhThucQuanLyId,
		da.iID_NhomQuanLyID as IIdNhomQuanLyId,
		da.iID_LoaiCongTrinhID as IIdLoaiCongTrinhId,
		da.iID_CapPheDuyetID as IIdCapPheDuyetId,
		da.sKhoiCong as SKhoiCong,
		da.sKetThuc as SKetThuc,
		da.bIsDuPhong as BIsDuPhong,
		(da.sKhoiCong  + '-'+ da.sKetThuc) as ThoiGianThucHien,
		da.sBanQuanLyDuAn as SBanQuanLyDuAn,
		dv.sTenDonVi as TenDonVi, 
		dv.iID_MaDonVi as IdDonVi, 
		lct.sTenLoaiCongTrinh as TenLoaiCongTrinh,
		ctdt.iID_ChuTruongDauTuID as IdChuTruongDauTu,
		ctdt.sSoQuyetDinh as SoQdChuTruongDauTu,
		ctdt.dNgayQuyetDinh as NgayQdChuTruongDauTu,
		ctdt.sKhoiCong as SKhoiCongChuTruong,
		ctdt.sHoanThanh as SHoanThanhChuTruong,
		ctdt.fTMDTDuKienPheDuyet as FTmdtduKienPheDuyet,
		qddt.iID_QDDauTuID as IdQdDauTu,
		qddt.sSoQuyetDinh as SoQdQdDauTu,
		qddt.dNgayQuyetDinh as NgayQdQdDauTu,
		qddt.fTongMucDauTuPheDuyet as FTongMucDauTuPheDuyet,
		cdt.iID_MaDonVi as IdChuDTString,
		dt.sSoQuyetDinh as sSoQuyetDinhDuToan,
		dt.dNgayQuyetDinh as dNgayQuyetDinhDuToan,
		dt.bLaTongDuToan,
		da.sTrangThaiDuAn,
		cdt.sTenDonVi as TenChuDauTu,
		htql.sTenHinhThucQuanLy as TenHinhThucQL,
		nda.sTenNhomDuAn as TenNhomDuAn,
		da.sUserCreate as SUserCreate,
		(SELECT COUNT(Id) FROM Attachment WHERE ModuleType = 204 AND ObjectId = da.iID_DuAnID) AS TotalFiles
	FROM
		VDT_DA_DuAn da			
		LEFT JOIN VDT_DM_PhanCapDuAn pcda on da.iID_CapPheDuyetID = pcda.iID_PhanCapID
		LEFT JOIN VDT_DM_DonViThucHienDuAn dv on da.iID_MaDonViThucHienDuAnID = dv.iID_MaDonVi
		LEFT JOIN VDT_DM_LoaiCongTrinh lct on da.iID_LoaiCongTrinhID = lct.iID_LoaiCongTrinh
		LEFT JOIN VDT_DA_ChuTruongDauTu ctdt ON ctdt.iID_DuAnID = da.iID_DuAnID AND ctdt.bActive = 1
		LEFT JOIN VDT_DA_QDDauTu qddt ON qddt.iID_DuAnID = da.iID_DuAnID AND qddt.bActive = 1 AND qddt.iID_QDDauTuID IN  (select top 1 iID_QDDauTuID from VDT_DA_QDDauTu where iID_DuAnID = qddt.iID_DuAnID)
		LEFT JOIN VDT_DA_DuToan as dt on da.iID_DuAnID = dt.iID_DuAnID AND dt.bActive = 1
		LEFT JOIN DM_ChuDauTu cdt on da.iID_ChuDauTuID = cdt.iID_DonVi
		LEFT JOIN VDT_DM_HinhThucQuanLy htql ON htql.iID_HinhThucQuanLyID = da.iID_HinhThucQuanLyID
		LEFT JOIN VDT_DM_NhomDuAn nda ON nda.iID_NhomDuAnID = da.iID_NhomDuAnID

	ORDER BY da.dDateCreate desc
END;
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_vdt_thongtri_detail]   Script Date: 11/28/2023 11:20:10 AM ******/
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
			dahm.IdLoaiCongTrinh as IIdLoaiCongTrinhId,
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
		LEFT JOIN VDT_DA_DuAn_HangMuc as dahm on dahm.iID_DuAnID = da.iID_DuAnID and dahm.iID_DuAn_HangMucID = tbl.ID_DuAn_HangMuc
		INNER JOIN NguonNganSach as nv on tbl.iID_NguonVonID = nv.iID_MaNguonNganSach
		LEFT JOIN NS_MucLucNganSach as ml on dt.iID_MucID = ml.iID OR dt.iID_TieuMucID = ml.iID OR dt.iID_TietMucID = ml.iID OR dt.iID_NganhID = ml.iID
		WHERE tbl.iLoaiThanhToan = (CASE WHEN @iLoaiThongTri = 1 THEN 1 ELSE 2 END)
			AND ( tbl.iID_ThongTriThanhToanID = @iIdThongTriId)
			--AND tbl.iID_MaDonViQuanLy = @sMaDonViQuanLy
			--AND tbl.iNamKeHoach = @iNamkeHoach
			--AND CAST(tbl.dNgayDeNghi as DATE) <= CAST(@dNgayThongTri as DATE)
			--AND (@sMaNguonVon IS NULL OR LOWER(nv.sMoTa) = LOWER(@sMaNguonVon))

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
			da_hm.IdLoaiCongTrinh as IIdLoaiCongTrinhId,
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
			SELECT iID_PhanBoVonID, iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, iID_LoaiCongTrinh, colName,iID_DuAn_HangMucID
			from 
			(select iID_PhanBoVonID, iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, iID_LoaiCongTrinh, fCapPhatTaiKhoBac, fCapPhatBangLenhChi,iID_DuAn_HangMucID from VDT_KHV_PhanBoVon_ChiTiet) as tbl
			UNPIVOT
			(fGiaTri FOR colName IN (fCapPhatTaiKhoBac, fCapPhatBangLenhChi)) as dt
			GROUP BY iID_PhanBoVonID, iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, iID_LoaiCongTrinh, colName,iID_DuAn_HangMucID
			HAVING SUM(ISNULL(fGiaTri, 0)) > 0
		) as dt on tbl.Id = dt.iID_PhanBoVonID
		
		INNER JOIN VDT_DA_DuAn as da on dt.iID_DuAnId = da.iID_DuAnID
		INNER JOIN VDT_DA_DuAn_HangMuc as da_hm on da_hm.iID_DuAnID = da.iID_DuAnID and dt.iID_DuAn_HangMucID = da_hm.iID_DuAn_HangMucID
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
;
;
;
