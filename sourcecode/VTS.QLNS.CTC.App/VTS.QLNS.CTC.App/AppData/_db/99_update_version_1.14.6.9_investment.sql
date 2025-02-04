/****** Object:  StoredProcedure [dbo].[sp_vdt_get_kehoachvon_capphatthanhtoan]    Script Date: 7/26/2024 8:59:08 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_kehoachvon_capphatthanhtoan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_kehoachvon_capphatthanhtoan]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_baocaodquyettoanniendo1]    Script Date: 7/26/2024 8:59:08 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_baocaodquyettoanniendo1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_baocaodquyettoanniendo1]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_baocaodquyettoanniendo1]    Script Date: 7/26/2024 8:59:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_vdt_get_baocaodquyettoanniendo1]
@iIdMaDonVi nvarchar(100),
@iNamKeHoach int,
@iIdNguonVon int
AS
BEGIN
	SELECT DISTINCT da.iID_DuAnID as IIDDuAnID, da.SMaDuAn, da.SDiaDiem, da.STenDuAn, dt.iID_LoaiCongTrinh, CAST(0 as bit) BIsChuyenTiep INTO #tmp
	FROM VDT_KHV_PhanBoVon as tbl
	INNER JOIN VDT_KHV_PhanBoVon_ChiTiet as dt on tbl.Id = dt.iID_PhanBoVonID
	INNER JOIN VDT_DA_DuAn as da on dt.iID_DuAnID = da.iID_DuAnID
	WHERE da.iID_MaDonViThucHienDuAnID = @iIdMaDonVi AND tbl.iNamKeHoach = @iNamKeHoach AND tbl.iID_NguonVonID = @iIdNguonVon
	UNION ALL
	SELECT DISTINCT da.iID_DuAnID as IIDDuAnID, da.SMaDuAn, da.SDiaDiem, da.STenDuAn, dt.iID_LoaiCongTrinh, CAST(0 as bit) BIsChuyenTiep
	FROM VDT_QT_BCQuyetToanNienDo as tbl
	INNER JOIN VDT_QT_BCQuyetToanNienDo_ChiTiet_01 as dt on tbl.Id = dt.iID_BCQuyetToanNienDo
	INNER JOIN VDT_DA_DuAn as da on dt.iID_DuAnID = da.iID_DuAnID
	WHERE iNamKeHoach = @iNamKeHoach AND (ISNULL(dt.fGiaTriNamTruocChuyenNamSau, 0) <> 0 OR ISNULL(dt.fGiaTriNamNayChuyenNamSau, 0) <> 0)
	UNION ALL
	SELECT DISTINCT da.iID_DuAnID as IIDDuAnID, da.SMaDuAn, da.SDiaDiem, da.STenDuAn, ct.iID_LoaiCongTrinh, CAST(0 as bit) BIsChuyenTiep
	FROM VDT_KT_KhoiTao_DuLieu as  kt
	INNER JOIN VDT_KT_KhoiTao_DuLieu_ChiTiet as ct on kt.Id = ct.iID_KhoiTaoDuLieuID
	INNER JOIN VDT_DA_DuAn as da on ct.iID_DuAnID = da.iID_DuAnID
	WHERE iNamKhoiTao = 2025 - 1 
	AND (ISNULL(ct.fKHVN_VonBoTriHetNamTruoc, 0) <> 0)




	

	-- Check du an chuyen tiep
	UPDATE tmp
	SET
		BIsChuyenTiep = 1
	FROM #tmp as tmp
	INNER JOIN (
		SELECT DISTINCT dt.iID_DuAnID 
		FROM VDT_KHV_PhanBoVon as tbl 
		INNER JOIN VDT_KHV_PhanBoVon_ChiTiet as dt on tbl.Id = dt.iID_PhanBoVonID 
		WHERE tbl.bActive = 1 AND tbl.iNamKeHoach = (@iNamKeHoach - 1)
		) as mp on tmp.IIDDuAnID = mp.iID_DuAnID

	-- Tong muc dau tu
	SELECT tmp.IIDDuAnID, SUM(ISNULL(qd.fTongMucDauTuPheDuyet, 0)) as fTongMucDauTu INTO #tmpTongMucDauTu
	FROM (SELECT DISTINCT IIDDuAnID FROM #tmp) as tmp
	INNER JOIN VDT_DA_QDDauTu as qd on tmp.IIDDuAnID = qd.iID_DuAnID
	WHERE qd.BActive = 1
	GROUP BY tmp.IIDDuAnID

	---- Kho bac
	BEGIN
		-- TongHopDuLieu nam truoc
		SELECT tbl.IIDDuAnID, 
			tbl.iID_LoaiCongTrinh,
			SUM(ISNULL(fLuyKeThanhToanNamTruoc, 0)) fLuyKeThanhToanNamTruoc,
			SUM(ISNULL(fLuyKeThanhToanNamTruocDelete, 0)) fLuyKeThanhToanNamTruocDelete,
			SUM(ISNULL(FTamUngTheoCheDoChuaThuHoiNamTruoc, 0)) FTamUngTheoCheDoChuaThuHoiNamTruoc,
			SUM(ISNULL(FTamUngTheoCheDoChuaThuHoiNamTruocDelete, 0)) FTamUngTheoCheDoChuaThuHoiNamTruocDelete,
			SUM(ISNULL(fDieuChinhGiamNamTruoc, 0)) fDieuChinhGiamNamTruoc,
			SUM(ISNULL(fDieuChinhGiamNamTruocDelete, 0)) fDieuChinhGiamNamTruocDelete INTO #tmpNamTruocKB
		FROM
		(
			SELECT tmp.IIDDuAnID,
					tmp.iID_LoaiCongTrinh,
				   (CASE WHEN (sMaDich = '403' AND sMaNguonCha = '301' AND sMaTienTrinh = '100') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fLuyKeThanhToanNamTruoc,
				   (CASE WHEN (sMaNguon = '403' AND sMaNguonCha = '301' AND sMaTienTrinh = '300') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fLuyKeThanhToanNamTruocDelete,

				   (CASE WHEN (sMaDich = '413a' AND sMaNguonCha = '301' AND sMaTienTrinh = '100') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as FTamUngTheoCheDoChuaThuHoiNamTruoc,
				   (CASE WHEN (sMaNguon = '413a' AND sMaNguonCha = '301' AND sMaTienTrinh = '300') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as FTamUngTheoCheDoChuaThuHoiNamTruocDelete,

				   (CASE WHEN (sMaNguon = '211c' AND (sMaNguonCha IS NULL OR sMaNguonCha NOT IN ('121a', '131')) AND sMaTienTrinh = '100') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fDieuChinhGiamNamTruoc,
				   (CASE WHEN (sMaDich = '211c' AND (sMaNguonCha IS NULL OR sMaNguonCha NOT IN ('121a', '131')) AND sMaTienTrinh = '300') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fDieuChinhGiamNamTruocDelete
			FROM (SELECT DISTINCT IIDDuAnID, iID_LoaiCongTrinh FROM #tmp) as tmp
			INNER JOIN VDT_TongHop_NguonNSDauTu as dt on tmp.IIDDuAnID = dt.iID_DuAnID AND tmp.iID_LoaiCongTrinh = dt.iID_LoaiCongTrinh
			WHERE dt.iNamKeHoach = @iNamKeHoach-1
			GROUP BY tmp.IIDDuAnID, sMaDich, sMaNguon, sMaNguonCha, sMaTienTrinh, tmp.iID_LoaiCongTrinh
		) as tbl
		 GROUP BY tbl.IIDDuAnID, tbl.iID_LoaiCongTrinh

		
		-- TongHopDuLieu nam nay
		SELECT tbl.IIDDuAnID,
			tbl.iID_LoaiCongTrinh,
			SUM(ISNULL(fTamUngNamTruocThuHoiNamNay, 0)) fTamUngNamTruocThuHoiNamNay,
			SUM(ISNULL(fTamUngNamTruocThuHoiNamNayDelete, 0)) fTamUngNamTruocThuHoiNamNayDelete,
			SUM(ISNULL(fKHVNamTruocChuyenNamNay, 0)) fKHVNamTruocChuyenNamNay,
			SUM(ISNULL(fKHVNamTruocChuyenNamNayDelete, 0)) fKHVNamTruocChuyenNamNayDelete,
			SUM(ISNULL(fTongThanhToanSuDungVonNamTruoc, 0)) fTongThanhToanSuDungVonNamTruoc,
			SUM(ISNULL(fTongThanhToanSuDungVonNamTruocDelete, 0)) fTongThanhToanSuDungVonNamTruocDelete,
			SUM(ISNULL(fTamUngNamNayDungVonNamTruoc, 0)) fTamUngNamNayDungVonNamTruoc,
			SUM(ISNULL(fTamUngNamNayDungVonNamTruocDelete, 0)) fTamUngNamNayDungVonNamTruocDelete,
			SUM(ISNULL(fThuHoiTamUngNamNayDungVonNamTruoc, 0)) fThuHoiTamUngNamNayDungVonNamTruoc,
			SUM(ISNULL(fThuHoiTamUngNamNayDungVonNamTruocDelete, 0)) fThuHoiTamUngNamNayDungVonNamTruocDelete,
			SUM(ISNULL(fKHVNamNay, 0)) fKHVNamNay,
			SUM(ISNULL(fKHVNamNayDelete, 0)) fKHVNamNayDelete,
			SUM(ISNULL(fTongThanhToanSuDungVonNamNay, 0)) fTongThanhToanSuDungVonNamNay,
			SUM(ISNULL(fTongThanhToanSuDungVonNamNayDelete, 0)) fTongThanhToanSuDungVonNamNayDelete,
			SUM(ISNULL(fThuHoiUngCacNamTruoc, 0)) fThuHoiUngCacNamTruoc,
			SUM(ISNULL(fThuHoiUngCacNamTruocDelete, 0)) fThuHoiUngCacNamTruocDelete,
			SUM(ISNULL(fKeHoachUngNamNay, 0)) fKeHoachUngNamNay,
			SUM(ISNULL(fKeHoachUngNamNayDelete, 0)) fKeHoachUngNamNayDelete,
			SUM(ISNULL(fTongTamUngNamNay, 0)) fTongTamUngNamNay,
			SUM(ISNULL(fTongTamUngNamNayDelete, 0)) fTongTamUngNamNayDelete,
			SUM(ISNULL(fTongThuHoiTamUngNamNay, 0)) fTongThuHoiTamUngNamNay,
			SUM(ISNULL(fTongThuHoiTamUngNamNayDelete, 0)) fTongThuHoiTamUngNamNayDelete,
			SUM(ISNULL(fThuHoiUngNamTruoc, 0)) fThuHoiUngNamTruoc,
			SUM(ISNULL(fThuHoiUngNamTruocDelete, 0)) fThuHoiUngNamTruocDelete,
			SUM(ISNULL(fTongThuHoiUngNamNay, 0)) fTongThuHoiUngNamNay,
			SUM(ISNULL(fTongThuHoiUngNamNayDelete, 0)) fTongThuHoiUngNamNayDelete INTO #tmpNamNayKB
		FROM
		(
			SELECT  tmp.IIDDuAnID, tmp.iID_LoaiCongTrinh,
					(CASE WHEN (sMaNguon = '211a' AND iThuHoiTUCheDo = 1 AND ILoaiUng = 1 AND sMaNguonCha NOT IN ('121a', '131') AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTamUngNamTruocThuHoiNamNay,
					(CASE WHEN (sMaDich = '211a' AND iThuHoiTUCheDo = 1 AND ILoaiUng = 1 AND sMaNguonCha NOT IN ('121a', '131') AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTamUngNamTruocThuHoiNamNayDelete,
					
					(CASE WHEN sMaNguon = '111' AND sMaDich = '000' AND sMaTienTrinh = '100' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKHVNamTruocChuyenNamNay,
					(CASE WHEN sMaDich = '111' AND sMaNguon = '000' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKHVNamTruocChuyenNamNayDelete,
					
					(CASE WHEN (sMaDich = '201' AND sMaNguonCha = '111' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThanhToanSuDungVonNamTruoc,
					(CASE WHEN (sMaNguon = '201' AND sMaNguonCha = '111' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThanhToanSuDungVonNamTruocDelete,
					
					(CASE WHEN (sMaDich = '211a' AND sMaNguonCha = '111' AND iThuHoiTUCheDo IS NULL AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTamUngNamNayDungVonNamTruoc,
					(CASE WHEN (sMaNguon = '211a' AND sMaNguonCha = '111' AND iThuHoiTUCheDo IS NULL AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTamUngNamNayDungVonNamTruocDelete,
					
					(CASE WHEN (sMaNguon = '211a' AND sMaNguonCha = '111' AND iThuHoiTUCheDo = 2 AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiTamUngNamNayDungVonNamTruoc,
					(CASE WHEN (sMaDich = '211a' AND sMaNguonCha = '111' AND iThuHoiTUCheDo = 2 AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiTamUngNamNayDungVonNamTruocDelete,
					
					(CASE WHEN sMaNguon = '101' AND sMaDich = '000' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKHVNamNay,
					(CASE WHEN sMaDich = '101' AND sMaNguon = '000' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKHVNamNayDelete,
					
					(CASE WHEN (sMaDich = '201' AND sMaNguonCha = '101' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThanhToanSuDungVonNamNay,
					(CASE WHEN (sMaNguon = '201' AND sMaNguonCha = '101' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThanhToanSuDungVonNamNayDelete,
					
					(CASE WHEN (sMaNguon = '211a' AND sMaNguonCha = '101' AND iThuHoiTUCheDo = 1 AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngCacNamTruoc,
					(CASE WHEN (sMaDich = '211a' AND sMaNguonCha = '101' AND iThuHoiTUCheDo = 1 AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngCacNamTruocDelete,
					
					(CASE WHEN (sMaDich = '121a' AND sMaNguonCha = '101' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKeHoachUngNamNay,
					(CASE WHEN (sMaNguon = '121a' AND sMaNguonCha = '101' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKeHoachUngNamNayDelete,

					(CASE WHEN (sMaDich = '211a' AND sMaNguonCha = '101' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongTamUngNamNay,
					(CASE WHEN (sMaNguon = '211a' AND sMaNguonCha = '101' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongTamUngNamNayDelete,
					
					(CASE WHEN (sMaNguon = '211a' AND sMaNguonCha = '101' AND iThuHoiTUCheDo = 2 AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThuHoiTamUngNamNay,
					(CASE WHEN (sMaDich = '211a' AND sMaNguonCha = '101' AND iThuHoiTUCheDo = 2 AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThuHoiTamUngNamNayDelete,

					(CASE WHEN (sMaNguon = '211a' AND sMaNguonCha = '111' AND iThuHoiTUCheDo = 1 AND sMaTienTrinh = '200') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngNamTruoc,
					(CASE WHEN (sMaDich = '211a' AND sMaNguonCha = '111' AND iThuHoiTUCheDo = 1 AND sMaTienTrinh = '300') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngNamTruocDelete,

					(CASE WHEN (sMaDich = '121a' AND sMaNguonCha = '101' AND sMaTienTrinh = (CASE WHEN iID_NguonVonID = 1 THEN '100' ELSE '200' END) AND iID_NguonVonID = @iIdNguonVon AND ISNULL(bKeHoach, 1) = (CASE WHEN iID_NguonVonID = 1 THEN ISNULL(bKeHoach, 1) ELSE 0 END)) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThuHoiUngNamNay,
				   (CASE WHEN (sMaNguon = '121a' AND sMaNguonCha = '101' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon AND ISNULL(bKeHoach, 1) = (CASE WHEN iID_NguonVonID = 1 THEN ISNULL(bKeHoach, 1) ELSE 0 END)) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThuHoiUngNamNayDelete
			FROM (SELECT DISTINCT IIDDuAnID, iID_LoaiCongTrinh FROM #tmp) as tmp
			INNER JOIN VDT_TongHop_NguonNSDauTu as dt on tmp.IIDDuAnID = dt.iID_DuAnID AND tmp.iID_LoaiCongTrinh = dt.iID_LoaiCongTrinh
			WHERE dt.iNamKeHoach = @iNamKeHoach
			GROUP BY tmp.IIDDuAnID, sMaDich, sMaNguon, sMaNguonCha, iID_NguonVonID, sMaTienTrinh, iThuHoiTUCheDo, ILoaiUng, bKeHoach, tmp.iID_LoaiCongTrinh
		) as tbl
		GROUP BY tbl.IIDDuAnID, tbl.iID_LoaiCongTrinh

	
	END
	
	-- co quan tai chinh
	BEGIN
		SELECT tbl.IIDDuAnID, tbl.iID_LoaiCongTrinh,
				SUM(ISNULL(fLuyKeThanhToanNamTruoc, 0)) fLuyKeThanhToanNamTruoc,
				SUM(ISNULL(fLuyKeThanhToanNamTruocDelete, 0)) fLuyKeThanhToanNamTruocDelete,
				
				SUM(ISNULL(FTamUngTheoCheDoChuaThuHoiNamTruoc, 0)) FTamUngTheoCheDoChuaThuHoiNamTruoc,
				SUM(ISNULL(FTamUngTheoCheDoChuaThuHoiNamTruocDelete, 0)) FTamUngTheoCheDoChuaThuHoiNamTruocDelete,
				SUM(ISNULL(fDieuChinhGiamNamTruoc, 0)) fDieuChinhGiamNamTruoc,
				SUM(ISNULL(fDieuChinhGiamNamTruocDelete, 0)) fDieuChinhGiamNamTruocDelete INTO #tmpNamTruoc
			FROM
			(
				-- TongHopDuLieu nam truoc
				SELECT tmp.IIDDuAnID, tmp.iID_LoaiCongTrinh,
					(CASE WHEN (sMaDich = '404' AND sMaNguonCha = '302' AND sMaTienTrinh = '100') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fLuyKeThanhToanNamTruoc,
					(CASE WHEN (sMaNguon = '404' AND sMaNguonCha = '302' AND sMaTienTrinh = '300') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fLuyKeThanhToanNamTruocDelete,

					

					(CASE WHEN (sMaDich = '414a' AND sMaNguonCha = '302' AND sMaTienTrinh = '100') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as FTamUngTheoCheDoChuaThuHoiNamTruoc,
					(CASE WHEN (sMaNguon = '414a' AND sMaNguonCha = '302' AND sMaTienTrinh = '300') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as FTamUngTheoCheDoChuaThuHoiNamTruocDelete,

					(CASE WHEN (sMaNguon = '212c' AND (sMaNguonCha IS NULL OR sMaNguonCha NOT IN ('122a', '132')) AND sMaTienTrinh = '100') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fDieuChinhGiamNamTruoc,
					(CASE WHEN (sMaDich = '212c' AND (sMaNguonCha IS NULL OR sMaNguonCha NOT IN ('122a', '132')) AND sMaTienTrinh = '300') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fDieuChinhGiamNamTruocDelete
				FROM (SELECT DISTINCT IIDDuAnID, iID_LoaiCongTrinh FROM #tmp) as tmp
				INNER JOIN VDT_TongHop_NguonNSDauTu as dt on tmp.IIDDuAnID = dt.iID_DuAnID AND tmp.iID_LoaiCongTrinh = dt.iID_LoaiCongTrinh
				WHERE dt.iNamKeHoach = @iNamKeHoach -1
				GROUP BY tmp.IIDDuAnID, sMaDich, sMaNguon, sMaNguonCha, sMaTienTrinh, tmp.iID_LoaiCongTrinh
			) as tbl
		GROUP BY tbl.IIDDuAnID, tbl.iID_LoaiCongTrinh

		-- TongHopDuLieu nam nay
		SELECT tbl.IIDDuAnID, tbl.iID_LoaiCongTrinh,
			SUM(ISNULL(fTamUngNamTruocThuHoiNamNay, 0)) fTamUngNamTruocThuHoiNamNay,
			SUM(ISNULL(fTamUngNamTruocThuHoiNamNayDelete, 0)) fTamUngNamTruocThuHoiNamNayDelete,
			SUM(ISNULL(fKHVNamTruocChuyenNamNay, 0)) fKHVNamTruocChuyenNamNay,
			SUM(ISNULL(fKHVNamTruocChuyenNamNayDelete, 0)) fKHVNamTruocChuyenNamNayDelete,
			SUM(ISNULL(fTongThanhToanSuDungVonNamTruoc, 0)) fTongThanhToanSuDungVonNamTruoc,
			SUM(ISNULL(fTongThanhToanSuDungVonNamTruocDelete, 0)) fTongThanhToanSuDungVonNamTruocDelete,
			SUM(ISNULL(fTamUngNamNayDungVonNamTruoc, 0)) fTamUngNamNayDungVonNamTruoc,
			SUM(ISNULL(fTamUngNamNayDungVonNamTruocDelete, 0)) fTamUngNamNayDungVonNamTruocDelete,
			SUM(ISNULL(fThuHoiTamUngNamNayDungVonNamTruoc, 0)) fThuHoiTamUngNamNayDungVonNamTruoc,
			SUM(ISNULL(fThuHoiTamUngNamNayDungVonNamTruocDelete, 0)) fThuHoiTamUngNamNayDungVonNamTruocDelete,
			SUM(ISNULL(fKHVNamNay, 0)) fKHVNamNay,
			SUM(ISNULL(fKHVNamNayDelete, 0)) fKHVNamNayDelete,
			SUM(ISNULL(fTongThanhToanSuDungVonNamNay, 0)) fTongThanhToanSuDungVonNamNay,
			SUM(ISNULL(fTongThanhToanSuDungVonNamNayDelete, 0)) fTongThanhToanSuDungVonNamNayDelete,
			SUM(ISNULL(fThuHoiUngCacNamTruoc, 0)) fThuHoiUngCacNamTruoc,
			SUM(ISNULL(fThuHoiUngCacNamTruocDelete, 0)) fThuHoiUngCacNamTruocDelete,
			SUM(ISNULL(fKeHoachUngNamNay, 0)) fKeHoachUngNamNay,
			SUM(ISNULL(fKeHoachUngNamNayDelete, 0)) fKeHoachUngNamNayDelete,
			SUM(ISNULL(fTongTamUngNamNay, 0)) fTongTamUngNamNay,
			SUM(ISNULL(fTongTamUngNamNayDelete, 0)) fTongTamUngNamNayDelete,
			SUM(ISNULL(fTongThuHoiTamUngNamNay, 0)) fTongThuHoiTamUngNamNay,
			SUM(ISNULL(fTongThuHoiTamUngNamNayDelete, 0)) fTongThuHoiTamUngNamNayDelete,
			SUM(ISNULL(fThuHoiUngNamTruoc, 0)) fThuHoiUngNamTruoc,
			SUM(ISNULL(fThuHoiUngNamTruocDelete, 0)) fThuHoiUngNamTruocDelete,
			SUM(ISNULL(fTongThuHoiUngNamNay, 0)) fTongThuHoiUngNamNay,
			SUM(ISNULL(fTongThuHoiUngNamNayDelete, 0)) fTongThuHoiUngNamNayDelete INTO #tmpNamNay
		FROM
		(
			SELECT  tmp.IIDDuAnID, tmp.iID_LoaiCongTrinh,
					(CASE WHEN (sMaNguon = '212a' AND iThuHoiTUCheDo = 1 AND ILoaiUng = 1 AND sMaNguonCha NOT IN ('122a', '132') AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTamUngNamTruocThuHoiNamNay,
					(CASE WHEN (sMaDich = '212a' AND iThuHoiTUCheDo = 1 AND ILoaiUng = 1 AND sMaNguonCha NOT IN ('122a', '132') AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTamUngNamTruocThuHoiNamNayDelete,

					(CASE WHEN sMaNguon = '112' AND sMaDich = '000' AND sMaTienTrinh = '100' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKHVNamTruocChuyenNamNay,
					(CASE WHEN sMaDich = '112' AND sMaNguon = '000' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKHVNamTruocChuyenNamNayDelete,

					(CASE WHEN (sMaDich = '202' AND sMaNguonCha = '112' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThanhToanSuDungVonNamTruoc,
					(CASE WHEN (sMaNguon = '202' AND sMaNguonCha = '112' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThanhToanSuDungVonNamTruocDelete,

					(CASE WHEN (sMaDich = '212a' AND sMaNguonCha = '112' AND iThuHoiTUCheDo IS NULL AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTamUngNamNayDungVonNamTruoc,
					(CASE WHEN (sMaNguon = '212a' AND sMaNguonCha = '112' AND iThuHoiTUCheDo IS NULL AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTamUngNamNayDungVonNamTruocDelete,

					(CASE WHEN (sMaNguon = '212a' AND sMaNguonCha = '112' AND iThuHoiTUCheDo = 2 AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiTamUngNamNayDungVonNamTruoc,
					(CASE WHEN (sMaDich = '212a' AND sMaNguonCha = '112' AND iThuHoiTUCheDo = 2 AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiTamUngNamNayDungVonNamTruocDelete,

					(CASE WHEN sMaNguon = '102' AND sMaDich = '000' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKHVNamNay,
					(CASE WHEN sMaDich = '102' AND sMaNguon = '000' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKHVNamNayDelete,

					(CASE WHEN (sMaDich = '202' AND sMaNguonCha = '102' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThanhToanSuDungVonNamNay,
					(CASE WHEN (sMaNguon = '202' AND sMaNguonCha = '102' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThanhToanSuDungVonNamNayDelete,
					
					(CASE WHEN (sMaNguon = '212a' AND sMaNguonCha = '102' AND iThuHoiTUCheDo = 1 AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngCacNamTruoc,
					(CASE WHEN (sMaDich = '212a' AND sMaNguonCha = '102' AND iThuHoiTUCheDo = 1 AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngCacNamTruocDelete,
					
					(CASE WHEN (sMaDich = '122a' AND sMaNguonCha = '102' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKeHoachUngNamNay,
					(CASE WHEN (sMaNguon = '122a' AND sMaNguonCha = '102' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKeHoachUngNamNayDelete,

					(CASE WHEN (sMaDich = '212a' AND sMaNguonCha = '102' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongTamUngNamNay,
					(CASE WHEN (sMaNguon = '212a' AND sMaNguonCha = '102' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongTamUngNamNayDelete,

					(CASE WHEN (sMaNguon = '212a' AND sMaNguonCha = '102' AND iThuHoiTUCheDo = 2 AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThuHoiTamUngNamNay,
					(CASE WHEN (sMaDich = '212a' AND sMaNguonCha = '102' AND iThuHoiTUCheDo = 2 AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThuHoiTamUngNamNayDelete,

					(CASE WHEN (sMaNguon = '212a' AND sMaNguonCha = '112' AND iThuHoiTUCheDo = 1 AND sMaTienTrinh = '200') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngNamTruoc,
					(CASE WHEN (sMaDich = '212a' AND sMaNguonCha = '112' AND iThuHoiTUCheDo = 1 AND sMaTienTrinh = '300') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngNamTruocDelete,
					
					(CASE WHEN (sMaDich = '122a' AND sMaNguonCha = '102' AND sMaTienTrinh = (CASE WHEN iID_NguonVonID = 1 THEN '100' ELSE '200' END) AND iID_NguonVonID = @iIdNguonVon  AND ISNULL(bKeHoach, 1) = (CASE WHEN iID_NguonVonID = 1 THEN ISNULL(bKeHoach, 1) ELSE 0 END)) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThuHoiUngNamNay,
					(CASE WHEN (sMaNguon = '122a' AND sMaNguonCha = '102' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon  AND ISNULL(bKeHoach, 1) = (CASE WHEN iID_NguonVonID = 1 THEN ISNULL(bKeHoach, 1) ELSE 0 END)) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThuHoiUngNamNayDelete
			FROM (SELECT DISTINCT IIDDuAnID, iID_LoaiCongTrinh FROM #tmp) as tmp
			INNER JOIN VDT_TongHop_NguonNSDauTu as dt on tmp.IIDDuAnID = dt.iID_DuAnID AND dt.iID_LoaiCongTrinh = tmp.iID_LoaiCongTrinh
			WHERE dt.iNamKeHoach = @iNamKeHoach
			GROUP BY tmp.IIDDuAnID, sMaDich, sMaNguon, sMaNguonCha, iID_NguonVonID, sMaTienTrinh, iThuHoiTUCheDo, ILoaiUng, bKeHoach, tmp.iID_LoaiCongTrinh
		) as tbl
		GROUP BY tbl.IIDDuAnID, tbl.iID_LoaiCongTrinh
	END




	
	SELECT  tmp.IIDDuAnID, tmp.SMaDuAn, tmp.SDiaDiem, tmp.STenDuAn, CAST(1 as int) as ICoQuanThanhToan, tmp.iID_LoaiCongTrinh as IIdLoaiCongTrinh, lct.STenLoaiCongTrinh, tmp.BIsChuyenTiep, lct.SMaLoaiCongTrinh,
		(ISNULL(nn.fKHVNamNay, 0) - ISNULL(nn.fKHVNamNayDelete, 0)) as FKHVNamNay, 
		(ISNULL(nn.fKHVNamTruocChuyenNamNay, 0) - ISNULL(nn.fKHVNamTruocChuyenNamNayDelete, 0)) as FKHVNamTruocChuyenNamNay, 
		(ISNULL(nn.fTamUngNamTruocThuHoiNamNay, 0) - ISNULL(nn.fTamUngNamTruocThuHoiNamNayDelete, 0)) as FTamUngNamTruocThuHoiNamNay,
		(ISNULL(nn.fThuHoiTamUngNamNayDungVonNamTruoc, 0) - ISNULL(nn.fThuHoiTamUngNamNayDungVonNamTruocDelete, 0)) as FThuHoiTamUngNamNayDungVonNamTruoc, 
		(ISNULL(nn.fTongTamUngNamNay, 0) - ISNULL(nn.fTongTamUngNamNayDelete, 0)) as FTongTamUngNamNay, 
		(((ISNULL(nn.fTongThanhToanSuDungVonNamNay, 0) - ISNULL(nn.fThuHoiUngCacNamTruoc, 0) + ISNULL(nn.fTongThuHoiUngNamNay, 0)) 
			- (ISNULL(nn.fTongThanhToanSuDungVonNamNayDelete, 0) - ISNULL(nn.fThuHoiUngCacNamTruocDelete, 0) + ISNULL(nn.fTongThuHoiUngNamNayDelete, 0)))) as FTongThanhToanSuDungVonNamNay,
		((ISNULL(nn.fTongThanhToanSuDungVonNamTruoc, 0) - ISNULL(nn.fThuHoiUngNamTruoc, 0)) - (ISNULL(nn.fTongThanhToanSuDungVonNamTruocDelete, 0) - ISNULL(nn.fThuHoiUngNamTruocDelete, 0))) as FTongThanhToanSuDungVonNamTruoc, 
		(ISNULL(nn.fTongThuHoiTamUngNamNay, 0) - ISNULL(nn.fTongThuHoiTamUngNamNayDelete, 0)) as FTongThuHoiTamUngNamNay, 
		(ISNULL(nn.fTamUngNamNayDungVonNamTruoc, 0) - ISNULL(nn.fTamUngNamNayDungVonNamTruocDelete, 0)) as FTamUngNamNayDungVonNamTruoc,
		(ISNULL(nt.fLuyKeThanhToanNamTruoc, 0) - ISNULL(nt.fLuyKeThanhToanNamTruocDelete, 0)) as FLuyKeThanhToanNamTruoc, 
		(ISNULL(nt.FTamUngTheoCheDoChuaThuHoiNamTruoc, 0) - ISNULL(nt.FTamUngTheoCheDoChuaThuHoiNamTruocDelete, 0)) as FTamUngTheoCheDoChuaThuHoiNamTruoc,
		ISNULL(dt.fTongMucDauTu, 0) as FTongMucDauTu,
		CAST(0 as float) as FGiaTriTamUngDieuChinhGiam,
		CAST(0 as float) as FGiaTriNamTruocChuyenNamSau,
		CAST(0 as float) as FGiaTriNamNayChuyenNamSau
	FROM (SELECT DISTINCT * FROM #tmp) as tmp
	LEFT JOIN #tmpNamNayKB as nn on tmp.IIDDuAnID = nn.IIDDuAnID and tmp.iID_LoaiCongTrinh = nn.iID_LoaiCongTrinh
	LEFT JOIN #tmpNamTruocKB as nt on tmp.IIDDuAnID = nt.IIDDuAnID and tmp.iID_LoaiCongTrinh = nt.iID_LoaiCongTrinh
	LEFT JOIN #tmpTongMucDauTu as dt on tmp.IIDDuAnID = dt.IIDDuAnID
	LEFT JOIN VDT_DM_LoaiCongTrinh as lct on tmp.iID_LoaiCongTrinh = lct.iID_LoaiCongTrinh
	UNION ALL
	SELECT tmp.IIDDuAnID, tmp.SMaDuAn, tmp.SDiaDiem, tmp.STenDuAn, CAST(2 as int) as ICoQuanThanhToan, tmp.iID_LoaiCongTrinh as IIdLoaiCongTrinh, lct.STenLoaiCongTrinh, tmp.BIsChuyenTiep, lct.SMaLoaiCongTrinh,
		(ISNULL(nn.fKHVNamNay, 0) - ISNULL(nn.fKHVNamNayDelete, 0)) as FKHVNamNay, 
		(ISNULL(nn.fKHVNamTruocChuyenNamNay, 0) - ISNULL(nn.fKHVNamTruocChuyenNamNayDelete, 0)) as FKHVNamTruocChuyenNamNay, 
		(ISNULL(nn.fTamUngNamTruocThuHoiNamNay, 0) - ISNULL(nn.fTamUngNamTruocThuHoiNamNayDelete, 0)) as FTamUngNamTruocThuHoiNamNay,
		(ISNULL(nn.fThuHoiTamUngNamNayDungVonNamTruoc, 0) - ISNULL(nn.fThuHoiTamUngNamNayDungVonNamTruocDelete, 0)) as FThuHoiTamUngNamNayDungVonNamTruoc, 
		(ISNULL(nn.fTongTamUngNamNay, 0) - ISNULL(nn.fTongTamUngNamNayDelete, 0)) as FTongTamUngNamNay, 
		(((ISNULL(nn.fTongThanhToanSuDungVonNamNay, 0) - ISNULL(nn.fThuHoiUngCacNamTruoc, 0) + ISNULL(nn.fTongThuHoiUngNamNay, 0)) 
			- (ISNULL(nn.fTongThanhToanSuDungVonNamNayDelete, 0) - ISNULL(nn.fThuHoiUngCacNamTruocDelete, 0) + ISNULL(nn.fTongThuHoiUngNamNayDelete, 0)))) as FTongThanhToanSuDungVonNamNay,
		((ISNULL(nn.fTongThanhToanSuDungVonNamTruoc, 0) - ISNULL(nn.fThuHoiUngNamTruoc, 0)) - (ISNULL(nn.fTongThanhToanSuDungVonNamTruocDelete, 0) - ISNULL(nn.fThuHoiUngNamTruocDelete, 0))) as FTongThanhToanSuDungVonNamTruoc, 
		(ISNULL(nn.fTongThuHoiTamUngNamNay, 0) - ISNULL(nn.fTongThuHoiTamUngNamNayDelete, 0)) as FTongThuHoiTamUngNamNay, 
		(ISNULL(nn.fTamUngNamNayDungVonNamTruoc, 0) - ISNULL(nn.fTamUngNamNayDungVonNamTruocDelete, 0)) as FTamUngNamNayDungVonNamTruoc,
		(ISNULL(nt.fLuyKeThanhToanNamTruoc, 0) - ISNULL(nt.fLuyKeThanhToanNamTruocDelete, 0)) as FLuyKeThanhToanNamTruoc, 
		(ISNULL(nt.FTamUngTheoCheDoChuaThuHoiNamTruoc, 0) - ISNULL(nt.FTamUngTheoCheDoChuaThuHoiNamTruocDelete, 0)) as FTamUngTheoCheDoChuaThuHoiNamTruoc, 
		ISNULL(dt.fTongMucDauTu, 0) as FTongMucDauTu,
		CAST(0 as float) as FGiaTriTamUngDieuChinhGiam,
		CAST(0 as float) as FGiaTriNamTruocChuyenNamSau,
		CAST(0 as float) as FGiaTriNamNayChuyenNamSau
	FROM (SELECT DISTINCT * FROM #tmp) as tmp
	LEFT JOIN #tmpNamNay as nn on tmp.IIDDuAnID = nn.IIDDuAnID and tmp.iID_LoaiCongTrinh = nn.iID_LoaiCongTrinh
	LEFT JOIN #tmpNamTruoc as nt on tmp.IIDDuAnID = nt.IIDDuAnID and tmp.iID_LoaiCongTrinh = nt.iID_LoaiCongTrinh
	LEFT JOIN #tmpTongMucDauTu as dt on tmp.IIDDuAnID = dt.IIDDuAnID
	LEFT JOIN VDT_DM_LoaiCongTrinh as lct on tmp.iID_LoaiCongTrinh = lct.iID_LoaiCongTrinh

	DROP TABLE #tmpNamTruoc
	DROP TABLE #tmpNamNay
	DROP TABLE #tmpNamTruocKB
	DROP TABLE #tmpNamNayKB
	DROP TABLE #tmp
	DROP TABLE #tmpTongMucDauTu
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_kehoachvon_capphatthanhtoan]    Script Date: 7/26/2024 8:59:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_get_kehoachvon_capphatthanhtoan]
	@DuAnId [varchar](max),
	@NguonVonId [int],
	@dNgayDeNghi [date],
	@NamKeHoach [int],
	@iCoQuanThanhToan [int],
	@iIdPheDuyet [uniqueidentifier]
WITH EXECUTE AS CALLER
AS
BEGIN
	CREATE TABLE #tmp(
		Id uniqueidentifier, 
		sSoQuyetDinh nvarchar(100), 
		dNgayQuyetDinh datetime,
		iNamKeHoach int,
		iID_NguonVonID int,
		PhanLoai int,
		LenhChi float,
		FTongGiaTri float,
		TenLoai nvarchar(600),
		sMaNguonCha nvarchar(100)
	)

	-- Ke hoach von nam
	
	SELECT Id INTO #tmpChungTuVonNam
	FROM 
	(
		SELECT Id, ROW_NUMBER() OVER(PARTITION BY iID_PhanBoGocID ORDER BY dNgayQuyetDinh DESC, dDateCreate DESC) as rn
		FROM VDT_KHV_PhanBoVon 
		WHERE CAST(dNgayQuyetDinh as DATE) <= CAST(@dNgayDeNghi as DATE)
			AND iNamKeHoach = @NamKeHoach
			AND iID_NguonVonID = @NguonVonId
	) as tbl 
	WHERE tbl.rn = 1

	INSERT INTO #tmp(Id, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_NguonVonID, FTongGiaTri, sMaNguonCha, TenLoai, PhanLoai)
	SELECT tbl.Id, dt.sSoQuyetDinh, dt.dNgayQuyetDinh, dt.iNamKeHoach, dt.iID_NguonVonID, SUM(ISNULL(dt.fGiaTri, 0)) as fGiaTri,
		dt.sMaNguon, N'Kế hoạch vốn năm', 1
	FROM #tmpChungTuVonNam as tbl
	INNER JOIN VDT_TongHop_NguonNSDauTu as dt on tbl.Id = dt.iID_ChungTu AND dt.iID_DuAnID = @DuAnId AND dt.iID_NguonVonID = @NguonVonId AND dt.bIsLog = 0
										AND (dt.sMaNguon in ('101', '102')  AND dt.sMaDich = '000' AND dt.sMaTienTrinh = '200')
	GROUP BY tbl.Id,dt.sSoQuyetDinh, dt.dNgayQuyetDinh, dt.iNamKeHoach, dt.iID_NguonVonID,dt.sMaNguon

	INSERT INTO #tmp(Id, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_NguonVonID, FTongGiaTri, sMaNguonCha, TenLoai, PhanLoai)
	SELECT tbl.iID_ChungTu, tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.iID_NguonVonID, SUM(ISNULL(tbl.fGiaTri, 0)), sMaNguon,
		CASE WHEN sMaNguon COLLATE DATABASE_DEFAULT in ('121a', '122a') THEN N'Kế hoạch vốn ứng'
			WHEN sMaNguon COLLATE DATABASE_DEFAULT in ('111', '112') THEN N'Kế hoạch năm trước chuyển sang'
			WHEN sMaNguon COLLATE DATABASE_DEFAULT in ('131', '132') THEN N'Kế hoạch ứng trước năm trước chuyển sang' END, 
		CASE WHEN sMaNguon COLLATE DATABASE_DEFAULT in ('121a', '122a') THEN 2
			WHEN sMaNguon COLLATE DATABASE_DEFAULT in ('111', '112') THEN 3
			WHEN sMaNguon COLLATE DATABASE_DEFAULT in ('131', '132') THEN 4 END
	FROM VDT_TongHop_NguonNSDauTu as tbl
	WHERE ((tbl.sMaNguon COLLATE DATABASE_DEFAULT in ('121a', '122a') AND sMaTienTrinh COLLATE DATABASE_DEFAULT = '200') OR (tbl.sMaNguon COLLATE DATABASE_DEFAULT in ('111', '112', '131', '132') AND sMaTienTrinh COLLATE DATABASE_DEFAULT = '100'))
		AND sMaDich COLLATE DATABASE_DEFAULT = '000'
		AND bIsLog = 0
		AND tbl.iID_DuAnID = @DuAnId AND tbl.iID_NguonVonID = @NguonVonId 
		AND CAST(tbl.dNgayQuyetDinh as DATE) <= CAST(@dNgayDeNghi as DATE)
		AND tbl.iNamKeHoach = @NamKeHoach
	GROUP BY tbl.iID_ChungTu, tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.iID_NguonVonID, sMaNguon



	
	-- Luy ke da thanh toan
	SELECT tmp.id as iIdChungTu, tmp.sMaNguonCha, 
		SUM(ISNULL(CASE WHEN dt.sMaNguon = '000' THEN ISNULL(dt.fGiaTri, 0) ELSE 0 END, 0)) as fThanhToan,
		SUM(ISNULL(CASE WHEN dt.sMaDich = '000' THEN ISNULL(dt.fGiaTri, 0) ELSE 0 END, 0)) as fThuHoi INTO #tmpThanhToan
	FROM #tmp as tmp
	INNER JOIN VDT_TongHop_NguonNSDauTu as dt on dt.iId_MaNguonCha = tmp.id
												AND tmp.sMaNguonCha COLLATE DATABASE_DEFAULT = dt.sMaNguonCha COLLATE DATABASE_DEFAULT
												AND sMaTienTrinh COLLATE DATABASE_DEFAULT = '200'
												AND dt.iID_DuAnID = @DuAnId
												 AND bIsLog = 0
	WHERE dt.iID_ChungTu <> @iIdPheDuyet 
	GROUP BY tmp.id, tmp.sMaNguonCha


	CREATE TABLE #tmpMaNguon(sMaNguon nvarchar(100))
	IF(@iCoQuanThanhToan = 1)
	BEGIN
		INSERT INTO #tmpMaNguon(sMaNguon)
		VALUES('101'), ('121a'), ('111'), ('131')
		declare @counter int = (select count(Id) from #tmp );
		select ROW_NUMBER() over(order by id) as stt, Id into #tmpSTT1 from #tmp ;
		while(@counter > 0)
		begin
			UPDATE #tmp 
			SET fTongGiaTri =
			-- Kế hoạch vốn năm
			case when sMaNguonCha = '101' or sMaNguonCha = '111' then 
			(SELECT top 1 fCapPhatTaiKhoBac from VDT_KHV_PhanBoVon_chitiet khvn_dd where khvn_dd.iID_PhanBoVonID=#tmp.Id)
			-- Kế hoạch vốn ứng
			else (SELECT top 1 fCapPhatTaiKhoBac from VDT_KHV_KeHoachVonUng_ChiTiet khvu_dd where khvu_dd.iID_KeHoachUngID=#tmp.Id) end		
			-- Kế hoạch năm trước chuyển sang (111) và kế hoạch vốn ứng năm trước chuyển sang (131) chưa clear
			WHERE Id = (SELECT top 1 Id FROM #tmpSTT1 where stt = @counter)  and sMaNguonCha in (select * from #tmpMaNguon)
			set @counter = @counter - 1;
		end 
		DROP TABLE #tmpSTT1;
	END
	ELSE
	BEGIN
		INSERT INTO #tmpMaNguon(sMaNguon)
		VALUES('102'), ('122a'), ('112'), ('132')
		set @counter = (select count(Id) from #tmpChungTuVonNam);
		select ROW_NUMBER() over(order by id) as stt, Id into #tmpSTT2 from #tmpChungTuVonNam;
		while(@counter > 0)
		begin
			UPDATE #tmp 
			SET fTongGiaTri =
			-- Kế hoạch vốn năm
			case when sMaNguonCha = '102' or sMaNguonCha = '112' then 
			(SELECT top 1 fCapPhatBangLenhChi  from VDT_KHV_PhanBoVon_chitiet khvn_dd where khvn_dd.iID_PhanBoVonID=#tmp.Id)
			--(SELECT top 1 fCapPhatTaiKhoBac from VDT_KHV_PhanBoVon_chitiet khvn_dd where khvn_dd.iID_PhanBoVonID=#tmp.Id)
			-- Kế hoạch vốn ứng
			else (SELECT top 1 fCapPhatTaiKhoBac from VDT_KHV_KeHoachVonUng_ChiTiet khvu_dd where khvu_dd.iID_KeHoachUngID=#tmp.Id) end			
			-- Kế hoạch năm trước chuyển sang (112) và kế hoạch vốn ứng năm trước chuyển sang (132) chưa clear
			WHERE Id = (SELECT top 1 Id FROM #tmpSTT2 where stt = @counter)  and sMaNguonCha in (select * from #tmpMaNguon)
			set @counter = @counter - 1;
		end 
		DROP TABLE #tmpSTT2
	END

	SELECT tmp.Id, 
		tmp.sSoQuyetDinh, 
		tmp.dNgayQuyetDinh, 
		tmp.iNamKeHoach, 
		tmp.iID_NguonVonID, 
		ISNULL(tmp.FTongGiaTri, 0) as FTongGiaTri,
		(ISNULL(dt.fThanhToan, 0) - ISNULL(dt.fThuHoi, 0)) as FLuyKeThanhToan,
		tmp.sMaNguonCha, 
		tmp.TenLoai, 
		tmp.PhanLoai,
		NULL as iID_DonViQuanLyID,
		NULL as iID_MaDonViQuanLy
	FROM #tmp as tmp
	INNER JOIN #tmpMaNguon as tbl on tmp.sMaNguonCha = tbl.sMaNguon
	LEFT JOIN #tmpThanhToan as dt on tmp.Id = dt.iIdChungTu 
		AND dt.sMaNguonCha COLLATE DATABASE_DEFAULT = tmp.sMaNguonCha COLLATE DATABASE_DEFAULT
	--WHERE (ISNULL(tmp.FTongGiaTri, 0) - ISNULL(dt.fThanhToan, 0) + ISNULL(dt.fThuHoi, 0)) != 0

	DROP TABLE #tmpMaNguon
	DROP TABLE #tmpThanhToan
	DROP TABLE #tmp
	DROP TABLE #tmpChungTuVonNam
END
;
;
;
;
GO
