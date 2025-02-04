/****** Object:  StoredProcedure [dbo].[sp_vdt_insert_phanbovondonvichitietpheduyet2]    Script Date: 10/30/2024 5:15:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_insert_phanbovondonvichitietpheduyet2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_insert_phanbovondonvichitietpheduyet2]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_von_bo_tri_5_nam]    Script Date: 10/30/2024 5:15:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_von_bo_tri_5_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_von_bo_tri_5_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_find_phanbovondonvichitiet]    Script Date: 10/30/2024 5:15:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_find_phanbovondonvichitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_find_phanbovondonvichitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_find_phanbovondonvichitiet]    Script Date: 10/30/2024 5:15:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- exec sp_vdt_find_phanbovondonvichitiet 'dbf4f050-9f98-4d26-a6d4-2f993a43da8e'

CREATE PROCEDURE [dbo].[sp_vdt_find_phanbovondonvichitiet]
@iIdPhanBoVon uniqueidentifier
AS
BEGIN
	DECLARE @iNamKeHoach int;
	DECLARE @nguonVonID int;

	SELECT @iNamKeHoach = iNamKeHoach, @nguonVonID = iID_NguonVonID FROM VDT_KHV_PhanBoVon_DonVi WHERE Id = @iIdPhanBoVon;
	
	-- kế hoạch vốn năm nay
	SELECT
			(SUM(isnull(pbvdvct.fCapPhatTaiKhoBac, 0)) + SUM(isnull(pbvdvct.fCapPhatBangLenhChi, 0))) as fKeHoachVonDuocDuyetNamNay,
			pbvdvct.iID_DuAnID as iID_DuAnID,
			pbvdvct.iID_LoaiCongTrinh as iID_LoaiCongTrinh
			INTO #tmpKeHoachVonDuocDuyetNamNay
		FROM
			VDT_KHV_PhanBoVon_ChiTiet pbvdvct
		INNER JOIN
			VDT_KHV_PhanBoVon pbvdv
		ON pbvdvct.iID_PhanBoVonID = pbvdv.Id
		WHERE 
			--pbvdv.iID_MaDonViQuanLy = @maDonViQuanLyId and 
			pbvdv.iID_NguonVonID = @nguonVonID and pbvdv.iNamKeHoach = (@iNamKeHoach - 1)
		GROUP BY pbvdvct.iID_DuAnID, pbvdvct.iID_LoaiCongTrinh

   -- vôn kéo dài các năm trước
	BEGIN
		SELECT
			(SUM(isnull(bcqtndct.fGiaTriNamTruocChuyenNamSau, 0)) + SUM(isnull(bcqtndct.fGiaTriNamNayChuyenNamSau, 0)) - SUM(isnull(bcqtndct.fGiaTriTamUngDieuChinhGiam, 0))) as fVonKeoDaiCacNamTruoc,
			bcqtndct.iID_DuAnID as iID_DuAnID
			INTO #tmpVonKeoDaiCacNamTruoc
		FROM
			VDT_QT_BCQuyetToanNienDo_ChiTiet_01 bcqtndct
		INNER JOIN
			VDT_QT_BCQuyetToanNienDo bcqtnd
		ON bcqtndct.iID_BCQuyetToanNienDo = bcqtnd.Id
		WHERE 
			--bcqtnd.iID_MaDonViQuanLy = @maDonViQuanLyId and 
			bcqtnd.iID_NguonVonID = @nguonVonID and bcqtnd.iNamKeHoach < (@iNamKeHoach - 1)
		GROUP BY bcqtndct.iID_DuAnID
	END

	SELECT
		khthct.*
		INTO #KhthDuocDuyet
	FROM
		VDT_KHV_KeHoach5Nam_ChiTiet khthct
	INNER JOIN
		VDT_KHV_KeHoach5Nam khth
	ON khthct.iID_KeHoach5NamID = khth.iID_KeHoach5NamID
	WHERE @iNamKeHoach >= khth.iGiaiDoanTu and @iNamKeHoach <= khth.iGiaiDoanDen

	SELECT DISTINCT
		da.iID_DuAnID,
		da.sTenDuAn, 
		da.sMaDuAn,
		CONCAT(sKhoiCong,'-', sKetThuc) as sThoiGianThucHien, 
		da.iID_CapPheDuyetID, 
		pc.sTen as sTenCapPheDuyet, 
		dt.iID_LoaiCongTrinhID,
		lct.sTenLoaiCongTrinh, 
		cdt.sTenDonVi as sTenChuDauTu, 
		dt.fTongMucDauTuDuocDuyet,
		dt.fLuyKeVonNamTruoc,
		khvnn.fKeHoachVonDuocDuyetNamNay,
		dt.fVonKeoDaiCacNamTruoc as fVonKeoDaiCacNamTruoc,
		dt.iID_DonViTienTeID,
		dt.iID_TienTeID,
		dt.fTiGiaDonVi,
		dt.fTiGia,
		dt.sTrangThaiDuAnDangKy,
		dt.ILoaiDuAn,
		isnull(khvnct.fVonBoTriTuNamDenNam, 0) as FKeHoachTrungHanDuocDuyet,
		case
			when dt.iID_ParentID is not null
			then 
				pbvctpr.fUocThucHien
			else
				dt.fUocThucHien
		end fUocThucHien,
		case
			when dt.iID_ParentID is not null
			then 
				pbvctpr.fThuHoiVonUngTruoc
			else
				dt.fThuHoiVonUngTruoc
		end fThuHoiVonUngTruoc,
		case
			when dt.iID_ParentID is not null
			then 
				pbvctpr.fThanhToan
			else
				dt.fThanhToan
		end fThanhToan,
		dt.fUocThucHien as FUocThucHienSauDc,
		dt.fThuHoiVonUngTruocDc as FThuHoiVonUngTruocSauDc,
		dt.fThanhToanDC as FThanhToanSauDc,
		dt.iID_ParentId as IIDParentId,
		dvthda.sTenDonVi as STenDonViThucHienDuAn,
		dt.iID_DuAn_HangMucID
		FROM VDT_KHV_PhanBoVon_DonVi as tbl
		INNER JOIN VDT_KHV_PhanBoVon_DonVi_ChiTiet as dt on tbl.Id = dt.iId_PhanBoVon_DonVi
		INNER JOIN VDT_DA_DuAn as da on dt.iID_DuAnID = da.iID_DuAnID
		LEFT JOIN DM_ChuDauTu as cdt on da.iID_ChuDauTuID = cdt.iID_DonVi
		LEFT JOIN VDT_DM_PhanCapDuAn as pc on da.iID_CapPheDuyetID = pc.iID_PhanCapID
		LEFT JOIN VDT_DM_LoaiCongTrinh as lct on dt.iID_LoaiCongTrinhID = lct.iID_LoaiCongTrinh
		LEFT JOIN #KhthDuocDuyet khvnct on dt.iID_DuAnID = khvnct.iID_DuAnID and khvnct.iID_LoaiCongTrinhID = dt.iID_LoaiCongTrinhID and khvnct.bActive = 1
		LEFT JOIN VDT_KHV_PhanBoVon_DonVi_ChiTiet pbvctpr on dt.iID_ParentId = pbvctpr.Id
		LEFT JOIn #tmpKeHoachVonDuocDuyetNamNay khvnn on dt.iID_DuAnID = khvnn.iID_DuAnID and dt.iID_LoaiCongTrinhID = khvnn.iID_LoaiCongTrinh
		LEFT JOIN VDT_DM_DonViThucHienDuAn dvthda on dvthda.iID_MaDonVi = da.iID_MaDonViThucHienDuAnID
		LEFT JOIN #tmpVonKeoDaiCacNamTruoc as vkd on dt.iID_DuAnID = vkd.iID_DuAnID
		WHERE tbl.Id = @iIdPhanBoVon

		DROP TABLE #KhthDuocDuyet;
		DROP TABLE #tmpKeHoachVonDuocDuyetNamNay;
		DROP TABLE #tmpVonKeoDaiCacNamTruoc;
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_von_bo_tri_5_nam]    Script Date: 10/30/2024 5:15:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_get_von_bo_tri_5_nam]
	@lstId nvarchar(max),
	@YearPlan int
AS
BEGIN
	select ISNULL(SUM(khthddct.fHanMucDauTu), 0) HanMucDauTu
	,ISNULL(SUM(case when isnull(khthddct.fVonBoTriTuNamDenNamDc,0) = 0 then khthddct.fVonBoTriTuNamDenNam else khthddct.fVonBoTriTuNamDenNamDc end),0) VonBoTri5Nam
	,ISNULL(SUM(case when isnull(khthddct.fGiaTriBoTriDc,0) = 0 then khthddct.fGiaTriBoTri else khthddct.fGiaTriBoTriDc end),0) VonBoTriSau5Nam
	from VDT_KHV_KeHoach5Nam khthdd
	INNER JOIN VDT_KHV_KeHoach5Nam_ChiTiet khthddct
		on khthdd.iID_KeHoach5NamID = khthddct.iID_KeHoach5NamID
	where khthdd.iGiaiDoanTu <= @YearPlan AND khthdd.iGiaiDoanDen >= @YearPlan 
	and khthddct.iID_DuAnID = @lstId 
	and khthddct.bActive = 1
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_insert_phanbovondonvichitietpheduyet2]    Script Date: 10/30/2024 5:15:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_insert_phanbovondonvichitietpheduyet2]
@bIsEdit bit,
@sUserLogin nvarchar(100),
@tbl_PhanBoVonChiTiet t_tbl_phanbovonchitiet6 READONLY,
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
											 iId_Parent, bActive, ILoaiDuAn, iID_LoaiCongTrinh, iID_DuAn_HangMucID)
	SELECT NEWID(), iID_PhanBoVonID, iID_DuAnID, fGiaTrPhanBo, fGiaTriThuHoi, 
			--new--
			iID_DonViTienTeID, iID_TienTeID, fTiGiaDonVi, fTiGia, sGhiChu, fThanhToanDeXuat, tbl.iID_Parent, 1, tbl.ILoaiDuAn, tbl.IIdLoaiCongTrinh, tbl.IID_DuAn_HangMucID
	FROM @tbl_PhanBoVonChiTiet as tbl
									
	DECLARE @fGiaTriDeNghi float = (SELECT SUM(fGiaTrDeNghi) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fGiaTriThuHoi float = (SELECT SUM(fGiaTriThuHoi) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fGiaTrPhanBo float = (SELECT SUM(fGiaTrPhanBo) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fCapPhatTaiKhoBac float = (SELECT SUM(fCapPhatTaiKhoBac) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fCapPhatBangLenhChi float = (SELECT SUM(fCapPhatBangLenhChi) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fGiaTriThuHoiNamTruocKhoBac float = (SELECT SUM(fGiaTriThuHoiNamTruocKhoBac) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fGiaTriThuHoiNamTruocLenhChi float = (SELECT SUM(fGiaTriThuHoiNamTruocLenhChi) FROM @tbl_PhanBoVonChiTiet)

	--SELECT  @fGiaTriThuHoi = (@fGiaTriThuHoi + ISNULL(fGiaTriThuHoi, 0)),
	--		@fGiaTrPhanBo = (@fGiaTrPhanBo + ISNULL(fGiaTrPhanBo, 0))
	--FROM VDT_KHV_PhanBoVon_DonVi_PheDuyet 
	--WHERE Id = (SELECT iID_ParentId FROM VDT_KHV_PhanBoVon_DonVi_PheDuyet WHERE Id = @iIdPhanBoVon)

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
;
GO
