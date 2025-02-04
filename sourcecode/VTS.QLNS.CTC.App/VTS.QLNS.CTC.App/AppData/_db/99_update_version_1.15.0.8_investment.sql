/****** Object:  StoredProcedure [dbo].[sp_vdt_get_nhucauchiquy_detail]    Script Date: 11/8/2024 7:52:13 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_nhucauchiquy_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_nhucauchiquy_detail]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_baocaodquyettoanniendo1]    Script Date: 11/8/2024 7:52:13 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_baocaodquyettoanniendo1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_baocaodquyettoanniendo1]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_baocaodquyettoanniendo1]    Script Date: 11/8/2024 7:52:13 PM ******/
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
	WHERE iNamKeHoach = @iNamKeHoach -1 AND (ISNULL(dt.fGiaTriNamTruocChuyenNamSau, 0) <> 0 OR ISNULL(dt.fGiaTriNamNayChuyenNamSau, 0) <> 0) and tbl.iID_NguonVonID = @iIdNguonVon
	UNION ALL
	SELECT DISTINCT da.iID_DuAnID as IIDDuAnID, da.SMaDuAn, da.SDiaDiem, da.STenDuAn, ct.iID_LoaiCongTrinh, CAST(0 as bit) BIsChuyenTiep
	FROM VDT_KT_KhoiTao_DuLieu as  kt
	INNER JOIN VDT_KT_KhoiTao_DuLieu_ChiTiet as ct on kt.Id = ct.iID_KhoiTaoDuLieuID
	INNER JOIN VDT_DA_DuAn as da on ct.iID_DuAnID = da.iID_DuAnID
	WHERE iNamKhoiTao = @iNamKeHoach - 1  
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
			INNER JOIN VDT_TongHop_NguonNSDauTu as dt on tmp.IIDDuAnID = dt.iID_DuAnID AND tmp.iID_LoaiCongTrinh = dt.iID_LoaiCongTrinh  and dt.iID_NguonVonID = @iIdNguonVon
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
			INNER JOIN VDT_TongHop_NguonNSDauTu as dt on tmp.IIDDuAnID = dt.iID_DuAnID AND tmp.iID_LoaiCongTrinh = dt.iID_LoaiCongTrinh and dt.iID_NguonVonID = @iIdNguonVon
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
				INNER JOIN VDT_TongHop_NguonNSDauTu as dt on tmp.IIDDuAnID = dt.iID_DuAnID AND tmp.iID_LoaiCongTrinh = dt.iID_LoaiCongTrinh and dt.iID_NguonVonID = @iIdNguonVon
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
			INNER JOIN VDT_TongHop_NguonNSDauTu as dt on tmp.IIDDuAnID = dt.iID_DuAnID AND dt.iID_LoaiCongTrinh = tmp.iID_LoaiCongTrinh and dt.iID_NguonVonID = @iIdNguonVon
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_nhucauchiquy_detail]    Script Date: 11/8/2024 7:52:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_vdt_get_nhucauchiquy_detail]
@iIdMaDonVi nvarchar(100),
@iNamKeHoach int,
@iIdNguonVon int,
@iQuy int,
@DonviTinh int
AS
BEGIN
	set @DonviTinh = 1;
	SELECT dt.iID_DuAnID, SUM(dt.fCapPhatBangLenhChi) as fCapPhatBangLenhChi, SUM(dt.fCapPhatTaiKhoBac) as fCapPhatTaiKhoBac INTO #tmp
	FROM VDT_KHV_PhanBoVon as tbl
	INNER JOIN VDT_KHV_PhanBoVon_ChiTiet as dt on tbl.Id = dt.iID_PhanBoVonID
	WHERE tbl.iID_MaDonViQuanLy = @iIdMaDonVi AND tbl.iNamKeHoach = @iNamKeHoach AND tbl.iID_NguonVonID = @iIdNguonVon
	GROUP BY dt.iID_DuAnID

	SELECT iID_DuAnId, 
		(CASE WHEN iCoQuanThanhToan = 2 THEN 'CQTC' ELSE N'Kho bạc' END ) as sLoaiThanhToan ,
		SUM(ISNULL(fThanhToanKLHTQuyTruoc, 0)) as fThanhToanKLHTQuyTruoc, SUM(ISNULL(fThanhToanTamUngQuyTruoc, 0)) as fThanhToanTamUngQuyTruoc INTO #tmpQuyTruoc
	FROM
	(
		SELECT tbl.iID_DuAnId, tbl.iCoQuanThanhToan,
			(CASE WHEN tbl.iLoaiThanhToan = 1 THEN SUM(ISNULL(dt.fGiaTriThanhToanNN, 0) + ISNULL(dt.fGiaTriThanhToanTN, 0)) ELSE CAST(0 as float) END) as fThanhToanKLHTQuyTruoc,
			(CASE WHEN tbl.iLoaiThanhToan = 2 THEN SUM(ISNULL(dt.fGiaTriThanhToanNN, 0) + ISNULL(dt.fGiaTriThanhToanTN, 0)) ELSE CAST(0 as float) END) as fThanhToanTamUngQuyTruoc
		FROM #tmp as tmp
		INNER JOIN VDT_TT_DeNghiThanhToan as tbl on tmp.iID_DuAnID = tbl.iID_DuAnId
		INNER JOIN VDT_TT_PheDuyetThanhToan_ChiTiet as dt on tbl.Id = dt.iID_DeNghiThanhToanID
		WHERE (
			(@iQuy = 1 AND MONTH(tbl.dNgayDeNghi) in (10,11,12) AND tbl.iNamKeHoach = (@iNamKeHoach - 1)) 
			OR (@iQuy = 2 AND MONTH(tbl.dNgayDeNghi) in (1,2,3) AND tbl.iNamKeHoach = @iNamKeHoach) 
			OR (@iQuy = 3 AND MONTH(tbl.dNgayDeNghi) in (4,5,6) AND tbl.iNamKeHoach = @iNamKeHoach) 
			OR (@iQuy = 4 AND MONTH(tbl.dNgayDeNghi) in (7,8,9) AND tbl.iNamKeHoach = @iNamKeHoach) 
		)
		AND tbl.iID_NguonVonID = @iIdNguonVon
		GROUP BY tbl.iID_DuAnId, tbl.iLoaiThanhToan, tbl.iCoQuanThanhToan
	) as tbl
	GROUP BY tbl.iID_DuAnId, tbl.iCoQuanThanhToan

	

	SELECT iID_DuAnId, 
		(CASE WHEN iCoQuanThanhToan = 2 THEN 'CQTC' ELSE N'Kho bạc' END ) as sLoaiThanhToan ,
		SUM(ISNULL(fThanhToanKLHTQuyNay ,0)) as fThanhToanKLHTQuyNay, SUM(ISNULL(fThanhToanTamUngQuyNay ,0)) as fThanhToanTamUngQuyNay, SUM(ISNULL(fThuHoiUng ,0)) as fThuHoiUng  INTO #tmpQuyNay
	FROM
	(
		SELECT tbl.iID_DuAnId, tbl.iCoQuanThanhToan,
			(CASE WHEN tbl.iLoaiThanhToan = 1 THEN SUM(ISNULL(dt.fGiaTriThanhToanNN, 0) + ISNULL(dt.fGiaTriThanhToanTN, 0)) ELSE CAST(0 as float) END) as fThanhToanKLHTQuyNay,
			(CASE WHEN tbl.iLoaiThanhToan = 2 THEN SUM(ISNULL(dt.fGiaTriThanhToanNN, 0) + ISNULL(dt.fGiaTriThanhToanTN, 0)) ELSE CAST(0 as float) END) as fThanhToanTamUngQuyNay,
			SUM(ISNULL(dt.fGiaTriThuHoiNamNayNN, 0) + ISNULL(dt.fGiaTriThuHoiNamNayTN, 0)) as fThuHoiUng
		FROM #tmp as tmp
		INNER JOIN VDT_TT_DeNghiThanhToan as tbl on tmp.iID_DuAnID = tbl.iID_DuAnId
		INNER JOIN VDT_TT_PheDuyetThanhToan_ChiTiet as dt on tbl.Id = dt.iID_DeNghiThanhToanID
		WHERE tbl.iNamKeHoach = @iNamKeHoach AND 
			((@iQuy = 1 AND MONTH(tbl.dNgayDeNghi) in (1,2,3)) 
			OR (@iQuy = 2 AND MONTH(tbl.dNgayDeNghi) in (4,5,6)) 
			OR (@iQuy = 3 AND MONTH(tbl.dNgayDeNghi) in (7,8,9)) 
			OR (@iQuy = 4 AND MONTH(tbl.dNgayDeNghi) in (10,11,12))
		)
		AND tbl.iID_NguonVonID = @iIdNguonVon
		GROUP BY tbl.iID_DuAnId, tbl.iLoaiThanhToan, tbl.iCoQuanThanhToan
	) as tbl
	GROUP BY tbl.iID_DuAnId, tbl.iCoQuanThanhToan

	
	
	if(@DonviTinh Is not null)
	BEGIN
	SELECT tmp.iID_DuAnID, da.sTenDuAn, tmp.sLoaiThanhToan, da.iID_LoaiCongTrinhID, da.sMaDuAn, ISNULL(tmp.fGiaTri, 0)/@DonviTinh as fKeHoachVonNam, 
		ISNULL(tblQuyTruoc.fThanhToanKLHTQuyTruoc, 0)/@DonviTinh as fThanhToanKLHTQuyTruoc, ISNULL(tblQuyTruoc.fThanhToanTamUngQuyTruoc, 0)/@DonviTinh as fThanhToanTamUngQuyTruoc, 
		ISNULL(tblQuyNay.fThanhToanKLHTQuyNay, 0)/@DonviTinh as fThanhToanKLHTQuyNay, ISNULL(tblQuyNay.fThanhToanTamUngQuyNay, 0)/@DonviTinh as fThanhToanTamUngQuyNay
		, ISNULL(tblQuyNay.fThuHoiUng, 0)/@DonviTinh as fThuHoiUng, CAST(0 as float)/@DonviTinh as fGiaTriDeNghi, null as sGhiChu
	FROM 
	(
		SELECT iID_DuAnID, (CASE colname WHEN 'fCapPhatBangLenhChi' THEN N'CQTC' WHEN 'fCapPhatTaiKhoBac' THEN N'Kho bạc' END) as sLoaiThanhToan, fGiaTri
		FROM #tmp
		UNPIVOT 
		(fGiaTri FOR colname in (fCapPhatBangLenhChi, fCapPhatTaiKhoBac)) as dt
	) as tmp
	INNER JOIN VDT_DA_DuAn as da on tmp.iID_DuAnID = da.iID_DuAnID
	LEFT JOIN #tmpQuyTruoc as tblQuyTruoc on tmp.iID_DuAnID = tblQuyTruoc.iID_DuAnId AND tmp.sLoaiThanhToan = tblQuyTruoc.sLoaiThanhToan
	LEFT JOIN #tmpQuyNay as tblQuyNay on tmp.iID_DuAnID = tblQuyNay.iID_DuAnId AND tmp.sLoaiThanhToan = tblQuyNay.sLoaiThanhToan
	END
END
;
;
;
GO
