/****** Object:  StoredProcedure [dbo].[sp_vdt_tt_get_khvthuhoiung]    Script Date: 12/1/2022 8:24:54 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_tt_get_khvthuhoiung]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_tt_get_khvthuhoiung]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_insert_phanbovondonvichitietpheduyet2]    Script Date: 12/1/2022 8:24:54 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_insert_phanbovondonvichitietpheduyet2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_insert_phanbovondonvichitietpheduyet2]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_chutruongdautu_index]    Script Date: 12/1/2022 8:24:54 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_chutruongdautu_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_chutruongdautu_index]
GO
/****** Object:  UserDefinedTableType [dbo].[t_tbl_phanbovonchitiet4]    Script Date: 12/1/2022 8:24:54 AM ******/
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N't_tbl_phanbovonchitiet4' AND ss.name = N'dbo')
DROP TYPE [dbo].[t_tbl_phanbovonchitiet4]
GO
/****** Object:  UserDefinedTableType [dbo].[t_tbl_phanbovonchitiet3]    Script Date: 12/1/2022 8:24:54 AM ******/
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N't_tbl_phanbovonchitiet3' AND ss.name = N'dbo')
DROP TYPE [dbo].[t_tbl_phanbovonchitiet3]
GO
/****** Object:  UserDefinedTableType [dbo].[t_tbl_phanbovonchitiet3]    Script Date: 12/1/2022 8:24:54 AM ******/
CREATE TYPE [dbo].[t_tbl_phanbovonchitiet3] AS TABLE(
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
	[fThanhToanDeXuat] [float] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[t_tbl_phanbovonchitiet4]    Script Date: 12/1/2022 8:24:54 AM ******/
CREATE TYPE [dbo].[t_tbl_phanbovonchitiet4] AS TABLE(
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
	[fThanhToanDeXuat] [float] NULL
)
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_chutruongdautu_index]    Script Date: 12/1/2022 8:24:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_chutruongdautu_index] 
	@YearOfWork int,
	@UserName nvarchar(100)
	
AS
BEGIN
	SELECT DISTINCT iID_MaDonVi INTO #tmpDonVi 
	FROM NguoiDung_DonVi 
	WHERE iNamLamViec = @YearOfWork AND iID_MaNguoiDung = @UserName AND iTrangThai = 1;


	WITH SoLieuDieuChinh AS 
	 (
		SELECT 
			ct.iID_ChuTruongDauTuID, ct.iID_ParentId
		FROM 
			VDT_DA_ChuTruongDauTu ct 
		WHERE 
			ct.iID_ParentId is not null

		UNION ALL

		SELECT 
			ct.iID_ChuTruongDauTuID, ct.iID_ParentId
		FROM 
			VDT_DA_ChuTruongDauTu ct JOIN SoLieuDieuChinh ctpr ON ct.iID_ParentId = ctpr.iID_ChuTruongDauTuID
		WHERE ct.iID_ParentId is not null
	  ),SoLanDieuChinh AS (
		   SELECT
			sdc.iID_ChuTruongDauTuID,sdc.iID_ParentId,  COUNT(sdc.iID_ChuTruongDauTuID) AS iSoLanDieuChinh
		  FROM 
			SoLieuDieuChinh sdc
		  GROUP BY sdc.iID_ParentId,sdc.iID_ChuTruongDauTuID
	  )

	SELECT
		ctdt.iID_ChuTruongDauTuID AS Id,
		ctdt.sSoQuyetDinh AS SSoQuyetDinh,
		ctdt.dNgayQuyetDinh AS DNgayQuyetDinh,
		ctdt.sSoToTrinh AS SSoToTrinh,
		ctdt.dNgayToTrinh AS DNgayToTrinh,
		ctdt.sSoThamDinh AS SSoThamDinh,
		ctdt.dNgayThamDinh AS DNgayThamDinh,
		ctdt.sCoQuanThamDinh AS SCoQuanThamDinh,
		ctdt.iID_DuAnID AS IIdDuAnId,
		ctdt.fTMDTDuKienPheDuyet AS FTmdtduKienPheDuyet,
		ctdt.sLoaiDieuChinh AS SLoaiDieuChinh,
		ctdt.sKhoiCong AS SKhoiCong,
		ctdt.sHoanThanh AS SHoanThanh,
		(da.sMaDuAn +'-'+ da.sTenDuAn ) AS STenDuAn,
		ctdt.sDiaDiem AS SDiaDiem,
		ctdt.sNguonGocSuDungDat AS SNguonGocSuDungDat,
		ctdt.sDienTichSuDungDat AS SDienTichSuDungDat,
		ctdt.iID_ChuDauTuID AS IIdChuDauTuId,
		ctdt.sSuCanThietDauTu AS SSuCanThietDauTu,
		ctdt.sMucTieu AS SMucTieu,
		ctdt.sQuyMo AS SQuyMo,
		ctdt.iID_DonViThucHienID AS IIdDonViThucHienId,
		ctdt.iID_LoaiDuAn AS IIdLoaiDuAn,
		ctdt.iID_HinhThucDauTuID AS IIdHinhThucDauTuId,
		ctdt.iID_HinhThucQuanLyID AS IIdHinhThucQuanLyId,
		ctdt.iID_NhomDuAnID AS IIdNhomDuAnId,
		da.iID_DonViThucHienDuAnID AS IIdDonViQuanLyId,
		ctdt.iID_CapPheDuyetID AS IIdCapPheDuyetId,
		ctdt.iID_LoaiCongTrinhID AS IIdLoaiCongTrinhId,
		ctdt.iID_NhomQuanLyID AS IIdNhomQuanLyId,
		ctdt.iID_MaDonViQuanLy AS IIdMaDonViQuanLy,
		ctdt.iID_MaChuDauTuID AS IIdMaChuDauTuId,
		ctdt.bIsGoc AS BIsGoc,
		ctdt.iID_ParentID AS IIdParentId,
		ctdt.bActive AS BActive,
		ctdt.sMota as SMoTa,
		isnull(tbl.iSoLanDieuChinh,0) AS ILanDieuChinh ,
		dv.sTenDonVi AS TenDonVi,
		dv.iID_MaDonVi AS IdDonvi,
		cdt.sTenDonVi AS TenChuDauTu,
		cpd.sTen AS TenCapPheDuyet,
		ctdt.sUserCreate as SUserCreate,
		ctdt.bKhoa as BKhoa,
		(SELECT COUNT(Id) FROM Attachment WHERE ModuleType = 203 AND ObjectId = ctdt.iID_ChuTruongDauTuID) AS TotalFiles
	FROM
		VDT_DA_ChuTruongDauTu ctdt
		INNER JOIN #tmpDonVi as dvCheck on ctdt.iID_MaDonViQuanLy = dvCheck.iID_MaDonVi
		LEFT JOIN VDT_DA_DuAn da ON da.iID_DuAnID = ctdt.iID_DuAnID
		--LEFT JOIN VDT_DM_DonViThucHienDuAn dv ON da.iID_DonViThucHienDuAnID = dv.iID_DonVi 
		LEFT JOIN DONVI dv on ctdt.iID_MaDonViQuanLy = dv.iID_MaDonVi
		LEFT JOIN DM_ChuDauTu cdt ON cdt.iID_DonVi = ctdt.iID_ChuDauTuID
		LEFT JOIN VDT_DM_PhanCapDuAn cpd ON cpd.iID_PhanCapID = ctdt.iID_CapPheDuyetID
		left join SoLanDieuChinh tbl ON tbl.iID_ChuTruongDauTuID = ctdt.iID_ChuTruongDauTuID

		where dv.iNamLamViec = @YearOfWork and dv.iLoai in ('0','1') -- để đồng bộ với điều kiện tìm kiếm khi thêm mới

	ORDER BY ctdt.dDateCreate DESC

	DROP TABLE #tmpDonVi
END;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_insert_phanbovondonvichitietpheduyet2]    Script Date: 12/1/2022 8:24:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_insert_phanbovondonvichitietpheduyet2]
@bIsEdit bit,
@sUserLogin nvarchar(100),
@tbl_PhanBoVonChiTiet t_tbl_phanbovonchitiet3 READONLY,
@sTypeError int OUTPUT
AS
BEGIN
	set @sTypeError = 0
	DECLARE @iIdPhanBoVon uniqueidentifier = (SELECT TOP(1) iID_PhanBoVonID FROM @tbl_PhanBoVonChiTiet)
	DECLARE @iNamKeHoach int = (SELECT TOP(1)iNamKeHoach FROM VDT_KHV_PhanBoVon_DonVi_PheDuyet WHERE id = @iIdPhanBoVon)
	--DECLARE @lstIdParent nvarchar(max);

	--DECLARE @iCountError int =  (SELECT COUNT(*)
	--								FROM @tbl_PhanBoVonChiTiet as tbl
	--								LEFT JOIN NS_MucLucNganSach as ml on ml.sLNS = tbl.sLNS
	--																AND ISNULL(tbl.sL, '') = ml.sL 
	--																AND ISNULL(tbl.sK, '') = ml.sK
	--																AND ISNULL(tbl.sM, '')= ml.sM 
	--																AND ISNULL(tbl.sTM, '') = ml.sTM 
	--																AND ISNULL(tbl.sTTM, '') = ml.sTTM
	--																AND ISNULL(tbl.sNG, '') = ml.sNG 
	--																AND ml.iNamLamViec = @iNamKeHoach
	--								WHERE ml.iID  IS NULL)

	--IF(@iCountError <> 0) 
	--BEGIN
	--	SET @sTypeError = 1
	--	RETURN
	--END

	IF(@bIsEdit = 1)
	BEGIN 
		DELETE VDT_KHV_PhanBoVon_DonVi_ChiTiet_PheDuyet WHERE iID_PhanBoVon_DonVi_PheDuyet_ID = @iIdPhanBoVon
		--select @lstIdParent = (cast(pbvct1.iId_Parent as nvarchar(1000)) + ',') from VDT_KHV_PhanBoVon_ChiTiet pbvct1 WHERE iID_PhanBoVonID = @iIdPhanBoVon and pbvct1.iId_Parent is not null
		--delete VDT_KHV_PhanBoVon_ChiTiet where iID_PhanBoVonID in (select * from dbo.splitstring(@lstIdParent))
	END

	INSERT INTO VDT_KHV_PhanBoVon_DonVi_ChiTiet_PheDuyet(Id, iID_PhanBoVon_DonVi_PheDuyet_ID, iID_DuAnID, fGiaTrPhanBo, fGiaTriThuHoi, 
											--new--
											iID_DonViTienTeID, iID_TienTeID, fTiGiaDonVi, fTiGia, sGhiChu, fThanhToanDeXuat,
											 iId_Parent, bActive, ILoaiDuAn, iID_LoaiCongTrinh)
	SELECT NEWID(), iID_PhanBoVonID, iID_DuAnID, fGiaTrPhanBo, fGiaTriThuHoi, 
			--new--
			iID_DonViTienTeID, iID_TienTeID, fTiGiaDonVi, fTiGia, sGhiChu, fThanhToanDeXuat, tbl.iID_Parent, 1, tbl.ILoaiDuAn, tbl.IIdLoaiCongTrinh
	FROM @tbl_PhanBoVonChiTiet as tbl
									
	DECLARE @fGiaTriDeNghi float = (SELECT SUM(fGiaTrDeNghi) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fGiaTriThuHoi float = (SELECT SUM(fGiaTriThuHoi) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fGiaTrPhanBo float = (SELECT SUM(fGiaTrPhanBo) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fCapPhatTaiKhoBac float = (SELECT SUM(fCapPhatTaiKhoBac) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fCapPhatBangLenhChi float = (SELECT SUM(fCapPhatBangLenhChi) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fGiaTriThuHoiNamTruocKhoBac float = (SELECT SUM(fGiaTriThuHoiNamTruocKhoBac) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fGiaTriThuHoiNamTruocLenhChi float = (SELECT SUM(fGiaTriThuHoiNamTruocLenhChi) FROM @tbl_PhanBoVonChiTiet)

	SELECT  @fGiaTriThuHoi = (@fGiaTriThuHoi + ISNULL(fGiaTriThuHoi, 0)),
			@fGiaTrPhanBo = (@fGiaTrPhanBo + ISNULL(fGiaTrPhanBo, 0))
	FROM VDT_KHV_PhanBoVon_DonVi_PheDuyet 
	WHERE Id = (SELECT iID_ParentId FROM VDT_KHV_PhanBoVon_DonVi_PheDuyet WHERE Id = @iIdPhanBoVon)

	UPDATE VDT_KHV_PhanBoVon_DonVi_PheDuyet
	set 
	fGiaTriThuHoi = @fGiaTriThuHoi,
	fGiaTrPhanBo = @fGiaTrPhanBo
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
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_tt_get_khvthuhoiung]    Script Date: 12/1/2022 8:24:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_tt_get_khvthuhoiung]
	@iIdDuAnId uniqueidentifier,
	@iIdNguonVonId int,
	@dNgayDeNghi DATE,
	@iNamKeHoach int,
	@iCoQuanThanhToan int,
	@iIdPheDuyet uniqueidentifier
AS
BEGIN
	SELECT Id, tbl.iNamKeHoach INTO #tmp
	FROM VDT_TT_DeNghiThanhToan as tbl
	WHERE tbl.iID_DuAnId = @iIdDuAnId
		AND tbl.iID_NguonVonID = @iIdNguonVonId
		AND CAST(tbl.dNgayDeNghi as DATE) <= CAST(@dNgayDeNghi as DATE)
		AND tbl.iCoQuanThanhToan = @iCoQuanThanhToan
		AND tbl.iNamKeHoach <= @iNamKeHoach
		AND iLoaiThanhToan = 0

	SELECT dt.iID_KeHoachVonID as IIdKeHoachVonId,
		(CASE WHEN tmp.iNamKeHoach < @iNamKeHoach THEN 1 ELSE 2 END ) as ILoaiNamTamUng,
		(CASE WHEN khvn.Id IS NOT NULL THEN khvn.sSoQuyetDinh WHEN khvu.Id IS NOT NULL THEN khvu.sSoQuyetDinh ELSE qtnd.sSoDeNghi END) as SSoQuyetDinh,
		(CASE WHEN khvn.Id IS NOT NULL THEN khvn.iNamKeHoach WHEN khvu.Id IS NOT NULL THEN khvu.iNamKeHoach ELSE qtnd.iNamKeHoach END) as INamKeHoach,
		(CASE WHEN dt.iLoaiKeHoachVon in (1,3) THEN 1 ELSE 2 END) as iLoaiKeHoachVon,
		SUM(ISNULL(dt.fGiaTriThanhToanTN, 0)) as FGiaTriThanhToanTN,
		SUM(ISNULL(dt.fGiaTriThanhToanNN, 0)) as FGiaTriThanhToanNN INTO #tmpTamUng
	FROM #tmp as tmp
	INNER JOIN VDT_TT_PheDuyetThanhToan_ChiTiet as dt on tmp.Id = dt.iID_DeNghiThanhToanID
	LEFT JOIN VDT_KHV_PhanBoVon as khvn on dt.iID_KeHoachVonID = khvn.Id AND iLoaiKeHoachVon = 1
	LEFT JOIN VDT_KHV_KeHoachVonUng as khvu on dt.iID_KeHoachVonID = khvu.Id AND iLoaiKeHoachVon = 2
	LEFT JOIN VDT_QT_BCQuyetToanNienDo as qtnd on dt.iID_KeHoachVonID = qtnd.Id AND iLoaiKeHoachVon in (3,4)
	LEFT JOIN NS_MucLucNganSach as ml on ml.iID = dt.iID_MucID
										OR ml.iID = dt.iID_TieuMucID
										OR ml.iID = dt.iID_TietMucID
										OR ml.iID = dt.iID_NganhID
	GROUP BY dt.iID_KeHoachVonID, khvn.Id, khvu.Id, khvn.sSoQuyetDinh, khvu.sSoQuyetDinh, qtnd.sSoDeNghi, khvn.iNamKeHoach, khvu.iNamKeHoach, tmp.iNamKeHoach, qtnd.iNamKeHoach,
		dt.iLoaiKeHoachVon

	SELECT tmp.IIdKeHoachVonId, 
		(CASE WHEN dt.iLoaiKeHoachVon in (1,3) THEN 1 ELSE 2 END) as iLoaiKeHoachVon,
		SUM(ISNULL(dt.fGiaTriThuHoiNamTruocTN, 0) + ISNULL(dt.fGiaTriThuHoiNamNayTN, 0) + ISNULL(dt.fGiaTriThuHoiUngTruocNamTruocTN, 0) + ISNULL(dt.fGiaTriThuHoiUngTruocNamNayTN, 0)) as FGiaTriThuHoiTrongNuoc,
		SUM(ISNULL(dt.fGiaTriThuHoiNamTruocNN, 0) + ISNULL(dt.fGiaTriThuHoiNamNayNN, 0) + ISNULL(dt.fGiaTriThuHoiUngTruocNamTruocNN, 0) + ISNULL(dt.fGiaTriThuHoiUngTruocNamNayNN, 0)) as FGiaTriThuHoiNgoaiNuoc INTO #tmpThuHoi
	FROM #tmpTamUng as tmp
	INNER JOIN VDT_TT_PheDuyetThanhToan_ChiTiet as dt on tmp.IIdKeHoachVonId = dt.iID_KeHoachVonID
											AND ((tmp.iLoaiKeHoachVon = 1 AND dt.iLoaiKeHoachVon in (1,3)) OR (tmp.iLoaiKeHoachVon = 1 AND dt.iLoaiKeHoachVon in (2,4)))
											AND (fGiaTriThuHoiNamTruocTN IS NOT NULL OR fGiaTriThuHoiNamTruocNN IS NOT NULL 
												OR fGiaTriThuHoiNamNayTN IS NOT NULL OR fGiaTriThuHoiNamNayNN IS NOT NULL 
												OR fGiaTriThuHoiUngTruocNamNayTN IS NOT NULL OR fGiaTriThuHoiUngTruocNamNayNN IS NOT NULL
												OR fGiaTriThuHoiUngTruocNamTruocTN IS NOT NULL OR fGiaTriThuHoiUngTruocNamTruocNN IS NOT NULL)
	INNER JOIN VDT_TT_DeNghiThanhToan as tbl on dt.iID_DeNghiThanhToanID = tbl.Id
	WHERE dt.iID_DeNghiThanhToanID <> @iIdPheDuyet AND tbl.iID_DuAnId = @iIdDuAnId AND CAST(tbl.dNgayDeNghi as DATE) < CAST(@dNgayDeNghi as DATE)
	GROUP BY tmp.IIdKeHoachVonId, dt.iLoaiKeHoachVon

	SELECT tbl.IIdKeHoachVonId, tbl.SSoQuyetDinh, tbl.INamKeHoach, tbl.ILoaiKeHoachVon, tbl.FGiaTriThanhToanTN, tbl.FGiaTriThanhToanNN, 
		(CASE WHEN tbl.INamKeHoach < @iNamKeHoach THEN 1 ELSE 2 END) as ILoaiNamKhv, tbl.ILoaiNamTamUng,
		SUM(ISNULL(dt.FGiaTriThuHoiTrongNuoc, 0 )) as FGiaTriThuHoiTrongNuoc, SUM(ISNULL(dt.FGiaTriThuHoiNgoaiNuoc, 0)) as FGiaTriThuHoiNgoaiNuoc INTO #tbl
	FROM #tmpTamUng as tbl
	LEFT JOIN #tmpThuHoi as dt on tbl.IIdKeHoachVonId = dt.IIdKeHoachVonId AND tbl.iLoaiKeHoachVon = dt.iLoaiKeHoachVon
	GROUP BY tbl.IIdKeHoachVonId, tbl.SSoQuyetDinh, tbl.INamKeHoach, tbl.ILoaiKeHoachVon, tbl.FGiaTriThanhToanTN, tbl.FGiaTriThanhToanNN, tbl.ILoaiNamTamUng
	
	SELECT iID_KeHoachVonID, iLoai INTO #khv
	FROM VDT_TT_DeNghiThanhToan_KHV
	WHERE iID_DeNghiThanhToanID = @iIdPheDuyet

	SELECT tbl.*, @iCoQuanThanhToan as ICoQuanThanhToan, 
		(ISNULL(FGiaTriThanhToanNN, 0) + ISNULL(FGiaTriThanhToanTN, 0)) as FGiaTriKHV, 
		(ISNULL(FGiaTriThanhToanNN, 0) + ISNULL(FGiaTriThanhToanTN, 0) - (ISNULL(FGiaTriThuHoiNgoaiNuoc, 0) + ISNULL(FGiaTriThuHoiTrongNuoc, 0))) FGiaTriKHVDaThanhToan
	FROM #tbl as tbl
	INNER JOIN #khv as dt on tbl.IIdKeHoachVonId = dt.iID_KeHoachVonID AND ILoaiNamTamUng = 2 AND tbl.ILoaiKeHoachVon = (CASE WHEN dt.iLoai in (1,3) THEN 1 ELSE 2 END)
	--WHERE FGiaTriThanhToanNN > FGiaTriThuHoiNgoaiNuoc OR FGiaTriThanhToanTN > FGiaTriThuHoiTrongNuoc
	UNION ALL
	SELECT tbl.*, @iCoQuanThanhToan as ICoQuanThanhToan,
		(ISNULL(FGiaTriThanhToanNN, 0) + ISNULL(FGiaTriThanhToanTN, 0)) as FGiaTriKHV, 
		(ISNULL(FGiaTriThanhToanNN, 0) + ISNULL(FGiaTriThanhToanTN, 0) - (ISNULL(FGiaTriThuHoiNgoaiNuoc, 0) + ISNULL(FGiaTriThuHoiTrongNuoc, 0))) FGiaTriKHVDaThanhToan
	FROM #tbl as tbl
	WHERE ILoaiNamTamUng = 1 --AND (FGiaTriThanhToanNN > FGiaTriThuHoiNgoaiNuoc OR FGiaTriThanhToanTN > FGiaTriThuHoiTrongNuoc)

	DROP TABLE #tbl
	DROP TABLE #tmp
	DROP TABLE #tmpTamUng
	DROP TABLE #tmpThuHoi
END
;
;
GO
