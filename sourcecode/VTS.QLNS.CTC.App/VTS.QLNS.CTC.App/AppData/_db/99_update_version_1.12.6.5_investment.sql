/****** Object:  StoredProcedure [dbo].[sp_vdt_tt_get_mlns_by_khv]    Script Date: 06/03/2023 9:03:27 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_tt_get_mlns_by_khv]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_tt_get_mlns_by_khv]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_insert_tonghopquyettoan]    Script Date: 06/03/2023 9:03:27 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_insert_tonghopquyettoan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_insert_tonghopquyettoan]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_getall_hangmuc_by_duan_detail]    Script Date: 06/03/2023 9:03:27 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_getall_hangmuc_by_duan_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_getall_hangmuc_by_duan_detail]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_quyettoanniendovonnam_by_parentid]    Script Date: 06/03/2023 9:03:27 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_quyettoanniendovonnam_by_parentid]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_quyettoanniendovonnam_by_parentid]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_duan_in_phanbovon_donvi_in_kehoachtrunghan]    Script Date: 06/03/2023 9:03:27 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_duan_in_phanbovon_donvi_in_kehoachtrunghan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_duan_in_phanbovon_donvi_in_kehoachtrunghan]
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_tonghopnguondautu]    Script Date: 06/03/2023 9:03:27 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_insert_tonghopnguondautu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_insert_tonghopnguondautu]
GO
/****** Object:  StoredProcedure [dbo].[sp_get_nguonvon_tonghopnguondautu_by_condition]    Script Date: 06/03/2023 9:03:27 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_get_nguonvon_tonghopnguondautu_by_condition]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_get_nguonvon_tonghopnguondautu_by_condition]
GO
/****** Object:  StoredProcedure [dbo].[sp_delete_tonghopnguonnsdautu_giam]    Script Date: 06/03/2023 9:03:27 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_delete_tonghopnguonnsdautu_giam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_delete_tonghopnguonnsdautu_giam]
GO
/****** Object:  StoredProcedure [dbo].[sp_delete_tonghopnguonnsdautu_giam]    Script Date: 06/03/2023 9:03:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_delete_tonghopnguonnsdautu_giam]
@sLoai nvarchar(100),
@uIdQuyetDinh uniqueidentifier
AS
BEGIN
	CREATE TABLE #tmp(iId uniqueidentifier)
	CREATE TABLE #tmpNguon(sMaNguon nvarchar(100))
	CREATE TABLE #tmpDich(sMaDich nvarchar(100))

	IF(@sLoai = 'KHVN')
	BEGIN
		INSERT INTO #tmpNguon VALUES ('101'), ('102')
		INSERT INTO #tmpDich VALUES ('121a'), ('122a')
	END
	IF(@sLoai = 'KHVU')
	BEGIN
		INSERT INTO #tmpNguon VALUES ('121a'), ('122a')
	END
	IF(@sLoai = 'THANH_TOAN')
	BEGIN
		INSERT INTO #tmpDich VALUES ('201'), ('202'), ('211a'), ('212a')
		INSERT INTO #tmpNguon VALUES ('211a'), ('212a')
	END
	IF(@sLoai = 'QUYET_TOAN')
	BEGIN
		INSERT INTO #tmpNguon VALUES ('111'), ('112'), ('131'), ('132'), ('211c'), ('212c'), 
			('301'), ('302'), ('403'), ('404'), ('413a'), ('414a'), ('321a'), ('322a'), ('403'), ('404'), ('321b'), ('322b')
	END

	-- dao nguoc but toan
	INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, sMaNguonCha, fGiaTri, bIsLog, iStatus, sMaTienTrinh, iThuHoiTUCheDo, iLoaiUng, bKeHoach, iID_LoaiCongTrinh)
	OUTPUT inserted.Id INTO #tmp(iId)
	SELECT tbl.iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, tbl.sMaDich, tbl.sMaNguon, tbl.sMaNguonCha, fGiaTri, 1, 2, '300', tbl.iThuHoiTUCheDo, iLoaiUng, bKeHoach, tbl.iID_LoaiCongTrinh
	FROM VDT_TongHop_NguonNSDauTu as tbl
	LEFT JOIN #tmpDich as md on tbl.sMaDich COLLATE DATABASE_DEFAULT = md.sMaDich COLLATE DATABASE_DEFAULT AND tbl.sMaNguon = '000'
	LEFT JOIN #tmpNguon as mn on tbl.sMaNguon COLLATE DATABASE_DEFAULT = mn.sMaNguon COLLATE DATABASE_DEFAULT AND tbl.sMaDich = '000'
	WHERE tbl.iID_ChungTu = @uIdQuyetDinh 
		AND bIsLog = 0
		AND sMaTienTrinh COLLATE DATABASE_DEFAULT in ('100', '200') 

	-- khoa but toan da update
	UPDATE tbl 
	SET 
		bIsLog = 1
	FROM VDT_TongHop_NguonNSDauTu as tbl
	LEFT JOIN #tmpDich as md on tbl.sMaDich COLLATE DATABASE_DEFAULT = md.sMaDich COLLATE DATABASE_DEFAULT AND tbl.sMaNguon = '000'
	LEFT JOIN #tmpNguon as mn on tbl.sMaNguon COLLATE DATABASE_DEFAULT = mn.sMaNguon COLLATE DATABASE_DEFAULT AND tbl.sMaDich = '000'
	WHERE tbl.iID_ChungTu = @uIdQuyetDinh 
		AND bIsLog = 0
		AND sMaTienTrinh COLLATE DATABASE_DEFAULT in ('100', '200') 
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_get_nguonvon_tonghopnguondautu_by_condition]    Script Date: 06/03/2023 9:03:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_get_nguonvon_tonghopnguondautu_by_condition]
@data t_tbl_tonghopdautu_v2 READONLY
AS
BEGIN
	
	SELECT tbl.Id, tbl.iID_ChungTu, tbl.iID_DuAnID, tbl.sMaNguon, tbl.sMaDich, tbl.sMaNguonCha, SUM(ISNULL(tbl.fGiaTri, 0)) as fGiaTri, tbl.iID_LoaiCongTrinh INTO #tmp
	FROM @data as tmp
	INNER JOIN VDT_TongHop_NguonNSDauTu as tbl on tmp.sMaNguon = tbl.sMaNguon 
											AND tmp.iID_ChungTu = tbl.iID_ChungTu 
											AND tmp.sMaDich = tbl.sMaDich 
											AND tbl.sMaTienTrinh in ('200', '100')
											AND tbl.iID_LoaiCongTrinh = tmp.IIdLoaiCongTrinh
	GROUP BY tbl.Id, tbl.iID_ChungTu, tbl.iID_DuAnID, tbl.sMaNguon, tbl.sMaDich, tbl.sMaNguonCha, tbl.iID_LoaiCongTrinh

	SELECT iID_ChungTu, iID_DuAnID, sMaNguon, SUM(ISNULL(fGiaTri, 0)) as fGiaTri, tbl.iID_LoaiCongTrinh INTO #tmpThanhToan
	FROM
	(
		SELECT tbl.iID_ChungTu, tbl.iID_DuAnID, tbl.sMaNguon as sMaNguon, ISNULL(tbl.fGiaTri, 0) as fGiaTri, tbl.iID_LoaiCongTrinh
		FROM @data as tmp
		INNER JOIN VDT_TongHop_NguonNSDauTu as tbl on tmp.sMaNguon = tbl.sMaDich
												AND tmp.iID_ChungTu = tbl.iID_ChungTu 
												AND tbl.sMaNguon = tmp.sMaDich 
												AND tbl.sMaTienTrinh = '300'
												AND tbl.iID_LoaiCongTrinh = tmp.IIdLoaiCongTrinh
		UNION ALL
		SELECT tbl.iID_ChungTu, tbl.iID_DuAnID, tbl.sMaNguonCha as sMaNguon, ISNULL(tbl.fGiaTri, 0) as fGiaTri, tbl.iID_LoaiCongTrinh
		FROM @data as tmp
		INNER JOIN VDT_TongHop_NguonNSDauTu as tbl on tmp.sMaNguon = tbl.sMaNguonCha
												AND tmp.iID_ChungTu = tbl.iId_MaNguonCha 
												AND tbl.sMaDich in ('201', '202', '211a', '212a')
												AND tbl.sMaTienTrinh = '200'
												AND tbl.bIsLog = 0
												AND tbl.iID_LoaiCongTrinh = tmp.IIdLoaiCongTrinh
	) as tbl
	GROUP BY iID_ChungTu, iID_DuAnID, sMaNguon, iID_LoaiCongTrinh

	SELECT tbl.Id,
		tbl.iID_ChungTu, 
		tbl.iID_DuAnID, 
		tbl.sMaNguon, 
		tbl.sMaDich, 
		tbl.sMaNguonCha, 
		tbl.iID_LoaiCongTrinh as IIdLoaiCongTrinh,
		(ISNULL(tbl.fGiaTri, 0) - ISNULL(tt.fGiaTri, 0)) as fGiaTri, 
		0 as iStatus, 
		CAST(0 as bit) as bIsLog, 
		NULL as iId_MaNguonCha,
		NULL as iThuHoiTUCheDo,
		NULL as ILoaiUng,
		NULL as IIDMucID,
		NULL as IIDTieuMucID,
		NULL as IIDTietMucID,
		NULL as IIDNganhID
	FROM #tmp as tbl
	LEFT JOIN #tmpThanhToan as tt on tbl.iID_ChungTu = tt.iID_ChungTu AND tbl.iID_DuAnID = tt.iID_DuAnID AND tbl.sMaNguon = tt.sMaNguon AND tbl.iID_LoaiCongTrinh = tt.iID_LoaiCongTrinh


	DROP TABLE #tmp
	DROP TABLE #tmpThanhToan
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_tonghopnguondautu]    Script Date: 06/03/2023 9:03:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_insert_tonghopnguondautu]
@iIDChungTu uniqueidentifier,
@sLoai nvarchar(100),
@data t_tbl_tonghopdautu_v2 READONLY
AS
BEGIN
	DECLARE @iIDMaDonViQuanLy nvarchar(100)
	DECLARE @iIDNguonVonID int
	DECLARE @sSoQuyetDinh nvarchar(100)
	DECLARE @dNgayQuyetDinh DATETIME
	DECLARE @iNamKeHoach int

	IF (@sLoai = 'KHVN')
	BEGIN
		SELECT @iIDMaDonViQuanLy = iID_MaDonViQuanLy,
			@iIDNguonVonID = iID_NguonVonID,
			@sSoQuyetDinh = sSoQuyetDinh,
			@dNgayQuyetDinh = dNgayQuyetDinh,
			@iNamKeHoach = iNamKeHoach
		FROM VDT_KHV_PhanBoVon WHERE Id = @iIDChungTu
	END
	ELSE IF (@sLoai = 'THANH_TOAN')
	BEGIN
		SELECT @iIDMaDonViQuanLy = iID_MaDonViQuanLy,
			@iIDNguonVonID = iID_NguonVonID,
			@sSoQuyetDinh = sSoDeNghi,
			@dNgayQuyetDinh = dNgayPheDuyet,
			@iNamKeHoach = iNamKeHoach
		FROM VDT_TT_DeNghiThanhToan WHERE Id = @iIDChungTu
	END

	INSERT INTO VDT_TongHop_NguonNSDauTu(iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, sMaNguonCha, fGiaTri, iStatus, iID_DuAnID, iId_MaNguonCha, sMaTienTrinh, bIsLog, iThuHoiTUCheDo,ILoaiUng,bKeHoach,
										iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG, iID_LoaiCongTrinh)
	SELECT @iIDMaDonViQuanLy, @iIDNguonVonID, @sSoQuyetDinh, @dNgayQuyetDinh, @iNamKeHoach, @iIDChungTu, tbl.sMaNguon, tbl.sMaDich, tbl.sMaNguonCha, tbl.fGiaTri, tbl.iStatus, tbl.iID_DuAnID, tbl.iId_MaNguonCha, '200', 0, tbl.iThuHoiTUCheDo, tbl.ILoaiUng,
			(CASE WHEN sMaNguonCha in ('121a','122a') AND sMaDich in ('101', '102') THEN (CASE WHEN @sLoai = 'KHVN' THEN 1 ELSE 0 END) ELSE NULL END),
			tbl.IIDMucID, tbl.IIDTieuMucID, tbl.IIDTietMucID, tbl.IIDNganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, tbl.IIdLoaiCongTrinh

	FROM @data as tbl
	LEFT JOIN NS_MucLucNganSach as ml on ml.iID = tbl.IIDMucID OR ml.iID = tbl.IIDTieuMucID OR ml.iID = tbl.IIDTietMucID OR ml.iID = tbl.IIDNganhID
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_duan_in_phanbovon_donvi_in_kehoachtrunghan]    Script Date: 06/03/2023 9:03:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_vdt_get_duan_in_phanbovon_donvi_in_kehoachtrunghan]
	@iNamKeHoach int, 
	--@ngayLap DateTime,
	@maDonViQuanLyId nvarchar(50),
	@nguonVonID int,
	@filterHasQDDT int
AS
Begin
	Select
		da.iID_DuAnID, 
		da.sTenDuAn,
		da.sMaDuAn,
		nv.iID_MaNguonNganSach,
		CONCAT(da.sKhoiCong,'-', da.sKetThuc) as sThoiGianThucHien,
		da.iID_CapPheDuyetID,
		pc.sTen as sTenCapPheDuyet,
		case 
			when dahm.IdLoaiCongTrinh is not null then dahm.IdLoaiCongTrinh else da.iID_LoaiCongTrinhID
		end as iID_LoaiCongTrinhID,
		lct.sTenLoaiCongTrinh, 
		cdt.sTenDonVi as sTenChuDauTu,
		da.iID_DonViTienTeID,
		da.iID_TienTeID,
		da.fTiGiaDonVi,
		da.fTiGia,
		da.sTrangThaiDuAn as sTrangThaiDuAnDangKy,
		dvthda.sTenDonVi as STenDonViThucHienDuAn,
		da.dDateCreate, 
		dahm.iID_DuAn_HangMucID INTO #tmp
		from VDT_DA_DuAn da
		inner JOIN VDT_KHV_KeHoach5Nam_ChiTiet khvct on da.iID_DuAnID = khvct.iID_DuAnID
		LEFT JOIN NguonNganSach nv ON khvct.iID_NguonVonID = nv.iID_MaNguonNganSach 
		LEFT JOIN VDT_DA_DuAn_HangMuc dahm on da.iID_DuAnID = dahm.iID_DuAnID
		LEFT JOIN VDT_DM_LoaiCongTrinh as lct on da.iID_LoaiCongTrinhID = lct.iID_LoaiCongTrinh or dahm.IdLoaiCongTrinh = lct.iID_LoaiCongTrinh
		LEFT JOIN VDT_DM_PhanCapDuAn as pc on da.iID_CapPheDuyetID = pc.iID_PhanCapID
		LEFT JOIN DM_ChuDauTu as cdt on da.iID_ChuDauTuID = cdt.iID_DonVi
		LEFT JOIN VDT_DM_DonViThucHienDuAn dvthda on da.iID_MaDonViThucHienDuAnID = dvthda.iID_MaDonVi
		Where  da.iID_MaDonViThucHienDuAnID in (select * from f_recursive_donvi(@maDonViQuanLyId)) 
		--And [dbo].[fn_CheckDieuKienDuAn](da.iID_DuAnID,@ngayLap) = 1 
			And 
			( @filterHasQDDT is null  -- search all
			
			or

				( -- search có quyết định đầu tư
					(@filterHasQDDT = 1 and da.iID_DuAnID in (SELECT DISTINCT qqdt.iID_DuAnID FROM VDT_DA_QDDauTu qqdt JOIN VDT_DA_QDDauTu_NguonVon qddtnv ON qqdt.iID_QDDauTuID=qddtnv.iID_QDDauTuID 
													JOIN NguonNganSach nv ON qddtnv.iID_NguonVonID=nv.iID_MaNguonNganSach 
															
													WHERE  (@nguonVonID IS NULL OR nv.iID_MaNguonNganSach=@nguonVonID))
					) or
					-- search k có quyết định đầu tư nhưng có chủ trương đầu tư
					(@filterHasQDDT = 0 and da.iID_DuAnID in 
						(SELECT DISTINCT ctdt.iID_DuAnID FROM VDT_DA_ChuTruongDauTu ctdt 
							join VDT_DA_ChuTruongDauTu_NguonVon ctdtnv on ctdt.iID_ChuTruongDauTuID = ctdtnv.iID_ChuTruongDauTuID
							JOIN NguonNganSach nv ON ctdtnv.iID_NguonVonID=nv.iID_MaNguonNganSach 															
							WHERE  (@nguonVonID IS NULL OR nv.iID_MaNguonNganSach=@nguonVonID)
							and ctdt.iID_DuAnID not in ( select iID_DuAnID from VDT_DA_QDDauTu)
						)
					) or 
					-- search k có chủ trương đầu tư
					(
						@filterHasQDDT = 2 and da.iID_DuAnID in 
							(
								select distinct da.iID_DuAnID from VDT_DA_DuAn da where da.iID_DuAnID not in (select DuAnId as iID_DuAnID from VDT_DA_ChuTruongDauTu_NguonVon) 
							)
					)

				)
			)

			and da.iID_DuAnID not in (select iID_DuAnID from VDT_QT_QuyetToan)

	select tmp.* into #tmpData from #tmp as tmp

	--Union ALL

	--Select 
	--	da.iID_DuAnID,
	--	da.sTenDuAn,
	--	da.sMaDuAn,
	--	nv.iID_MaNguonNganSach,
	--	CONCAT(da.sKhoiCong,'-', da.sKetThuc) as sThoiGianThucHien,
	--	da.iID_CapPheDuyetID,
	--	pc.sTen as sTenCapPheDuyet,
	--	case 
	--		when dahm.IdLoaiCongTrinh is not null then dahm.IdLoaiCongTrinh else da.iID_LoaiCongTrinhID
	--	end as iID_LoaiCongTrinhID,
	--	lct.sTenLoaiCongTrinh,
	--	cdt.sTenDonVi as sTenChuDauTu,
	--	da.iID_DonViTienTeID,
	--	da.iID_TienTeID, 
	--	da.fTiGiaDonVi,
	--	da.fTiGia,
	--	da.sTrangThaiDuAn as sTrangThaiDuAnDangKy,
	--	dvthda.sTenDonVi as STenDonViThucHienDuAn,
	--	da.dDateCreate
	--from VDT_DA_DuAn da
	--	inner JOIN VDT_KHV_KeHoach5Nam_ChiTiet khvct on da.iID_DuAnID = khvct.iID_DuAnID
	--	LEFT JOIN NguonNganSach nv ON khvct.iID_NguonVonID = nv.iID_MaNguonNganSach 
	--	LEFT JOIN VDT_DA_DuAn_HangMuc dahm on da.iID_DuAnID = dahm.iID_DuAnID
	--	LEFT JOIN VDT_DM_LoaiCongTrinh as lct on da.iID_LoaiCongTrinhID = lct.iID_LoaiCongTrinh or dahm.IdLoaiCongTrinh = lct.iID_LoaiCongTrinh
	--	LEFT JOIN VDT_DM_PhanCapDuAn as pc on da.iID_CapPheDuyetID = pc.iID_PhanCapID
	--	LEFT JOIN DM_ChuDauTu as cdt on da.iID_ChuDauTuID = cdt.iID_DonVi
	--	LEFT JOIN VDT_DM_DonViThucHienDuAn dvthda on da.iID_MaDonViThucHienDuAnID = dvthda.iID_MaDonVi
	--Where 
	--	da.iID_DuAnID in (SELECT ctdt.iID_DuAnID FROM VDT_DA_ChuTruongDauTu ctdt)
	--	and da.iID_DuAnID not in (select tmpexisted.iID_DuAnID from #tmp tmpexisted)
	--	And da.iID_MaDonViThucHienDuAnID in (select * from f_recursive_donvi(@maDonViQuanLyId))

	-- Tong muc dau tu
	SELECT
		da.iID_DuAnID,
		CASE
			when qddt.iID_QDDauTuID is not null 
			then ISNULL(qddt.fTongMucDauTuPheDuyet, 0)
			else ISNULL(ctdt.fTMDTDuKienPheDuyet, 0)
		END fGiaTriDauTu
		INTO #tmpDataPD
	FROM
		#tmpData da
	LEFT JOIN
		VDT_DA_QDDauTu qddt
	ON
		da.iID_DuAnID = qddt.iID_DuAnID AND qddt.bActive = 1
	LEFT JOIN
		VDT_DA_ChuTruongDauTu ctdt
	ON
		da.iID_DuAnID = ctdt.iID_DuAnID AND ctdt.bActive = 1

	--luy ke von nam truoc
	BEGIN
		SELECT
			(SUM(isnull(pbvdvct.fCapPhatTaiKhoBac, 0)) + SUM(isnull(pbvdvct.fCapPhatBangLenhChi, 0))) as fLuyKeVonNamTruoc,
			--(SUM(isnull(bcqtndct.fGiaTriNamTruocChuyenNamSau, 0)) + SUM(isnull(bcqtndct.fGiaTriNamNayChuyenNamSau, 0)) - SUM(isnull(bcqtndct.fGiaTriTamUngDieuChinhGiam, 0)))) as fLuyKeVonNamTruoc,
			pbvdvct.iID_DuAnID as iID_DuAnID
			INTO #tmpLuyKeNamTruoc
		FROM
			VDT_KHV_PhanBoVon_ChiTiet pbvdvct
		INNER JOIN
			VDT_KHV_PhanBoVon pbvdv
		ON pbvdvct.iID_PhanBoVonID = pbvdv.Id
		--LEFT JOIN
		--	VDT_QT_BCQuyetToanNienDo_ChiTiet_01 bcqtndct
		--ON pbvdvct.iID_DuAnID = bcqtndct.iID_DuAnID
		WHERE 
			--pbvdv.iID_MaDonViQuanLy = @maDonViQuanLyId and 
			pbvdv.iID_NguonVonID = @nguonVonID and pbvdv.iNamKeHoach <= (@iNamKeHoach - 2)
		GROUP BY pbvdvct.iID_DuAnID
	END

	-- kế hoạch vốn năm nay
	BEGIN
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
	END

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
	
	BEGIN
		SELECT
			khthct.*,
			khth.ILoai
			into #tmpThDd
		FROM
			VDT_KHV_KeHoach5Nam_ChiTiet khthct
		INNER JOIN
			VDT_KHV_KeHoach5Nam khth
		ON khthct.iID_KeHoach5NamID = khth.iID_KeHoach5NamID
		WHERE @iNamKeHoach >= khth.iGiaiDoanTu and @iNamKeHoach <= khth.iGiaiDoanDen
	END

	SELECT
			tmp.*,
			tbl_tmdt.fGiaTriDauTu as fTongMucDauTuDuocDuyet,
			isnull(lknt.fLuyKeVonNamTruoc, 0) as fLuyKeVonNamTruoc,
			isnull(khnn.fKeHoachVonDuocDuyetNamNay, 0) as fKeHoachVonDuocDuyetNamNay,
			isnull(vkd.fVonKeoDaiCacNamTruoc, 0) as fVonKeoDaiCacNamTruoc,
			cast(0 as float) as fUocThucHien,
			cast(0 as float) as fThuHoiVonUngTruoc,
			cast(0 as float) as fThanhToan,
			cast(0 as float) as FUocThucHienSauDc,
			cast(0 as float) as FThuHoiVonUngTruocSauDc,
			cast(0 as float) as FThanhToanSauDc,
			null as IIDParentId,
			case
				when (((da.sTrangThaiDuAn = N'THUC_HIEN') and (da.bIsKetThuc IS NULL)) 
				or (select count(*) from VDT_KT_KhoiTao_DuLieu_ChiTiet ktdlct where ktdlct.iID_DuAnID = tmp.iID_DuAnID) > 0) then 2 else 1
			end ILoaiDuAn,
			isnull(khvnct.fVonBoTriTuNamDenNam, 0) as FKeHoachTrungHanDuocDuyet
		FROM #tmpData as tmp
		LEFT JOIN #tmpDataPD as tbl_tmdt on tmp.iID_DuAnID = tbl_tmdt.iID_DuAnID
		LEFT JOIN #tmpLuyKeNamTruoc as lknt on tmp.iID_DuAnID = lknt.iID_DuAnID
		LEFT JOIN #tmpKeHoachVonDuocDuyetNamNay as khnn on tmp.iID_DuAnID = khnn.iID_DuAnID and khnn.iID_LoaiCongTrinh = tmp.iID_LoaiCongTrinhID
		LEFT JOIN #tmpVonKeoDaiCacNamTruoc as vkd on tmp.iID_DuAnID = vkd.iID_DuAnID
		LEFT JOIN #tmpThDd khvnct on tmp.iID_DuAnID = khvnct.iID_DuAnID and tmp.iID_LoaiCongTrinhID = khvnct.iID_LoaiCongTrinhID and khvnct.iID_NguonVonID = @nguonVonID
		LEFT JOIN VDT_DA_DuAn da on tmp.iID_DuAnID = da.iID_DuAnID
		where iID_MaNguonNganSach = @nguonVonID
		ORDER BY tmp.dDateCreate desc

	drop table #tmpThDd;
	drop table #tmp;
	drop table #tmpData;
	drop table #tmpDataPD;
	drop table #tmpLuyKeNamTruoc;
	drop table #tmpKeHoachVonDuocDuyetNamNay;
	drop table #tmpVonKeoDaiCacNamTruoc;

End
;





GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_quyettoanniendovonnam_by_parentid]    Script Date: 06/03/2023 9:03:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_vdt_get_quyettoanniendovonnam_by_parentid]
@iIdQuyetToanId uniqueidentifier
AS
BEGIN
	DECLARE @iNamKeHoach int
	SELECT @iNamKeHoach = iNamKeHoach FROM VDT_QT_BCQuyetToanNienDo WHERE Id = @iIdQuyetToanId

	SELECT DISTINCT iID_DuAnID, CAST(0 AS bit) BIsChuyenTiep INTO #tmpDuAn
	FROM VDT_QT_BCQuyetToanNienDo_ChiTiet_01
	WHERE iID_BCQuyetToanNienDo = @iIdQuyetToanId

	UPDATE tmp
	SET
		BIsChuyenTiep = 1
	FROM #tmpDuAn as tmp
	INNER JOIN (
		SELECT DISTINCT dt.iID_DuAnID 
		FROM VDT_KHV_PhanBoVon as tbl 
		INNER JOIN VDT_KHV_PhanBoVon_ChiTiet as dt on tbl.Id = dt.iID_PhanBoVonID 
		WHERE tbl.bActive = 1 AND tbl.iNamKeHoach = (@iNamKeHoach - 1)
		) as mp on tmp.iID_DuAnID = mp.iID_DuAnID

	-- Tong muc dau tu
	SELECT tmp.iID_DuAnID, SUM(ISNULL(qd.fTongMucDauTuPheDuyet, 0)) as fTongMucDauTu INTO #tmpTongMucDauTu
	FROM #tmpDuAn as tmp
	INNER JOIN VDT_DA_QDDauTu as qd on tmp.iID_DuAnID = qd.iID_DuAnID
	WHERE qd.BActive = 1
	GROUP BY tmp.iID_DuAnID

	SELECT
		tbl.iID_DuAnID as IIDDuAnID,
		da.SMaDuAn,
		da.SDiaDiem,
		da.STenDuAn,
		tbl.ICoQuanThanhToan,
		tbl.iID_LoaiCongTrinh as IIdLoaiCongTrinh,
		lct.STenLoaiCongTrinh,
		lct.SMaLoaiCongTrinh,
		tmp.BIsChuyenTiep,
		ISNULL(tmdt.fTongMucDauTu, 0) as FTongMucDauTu, 
		ISNULL(tbl.FLKThanhToanDenTrcNamQuyetToan, 0) as FLuyKeThanhToanNamTruoc, --6
		ISNULL(tbl.FTamUngChuaThuHoiTrcNamQuyetToan, 0) as FTamUngTheoCheDoChuaThuHoiNamTruoc, --7
		ISNULL(tbl.FGiaTriTamUngDieuChinhGiam, 0) as FGiaTriTamUngDieuChinhGiam, --8
		ISNULL(tbl.FThuHoiUngNamTrc, 0) as FTamUngNamTruocThuHoiNamNay, --9
		ISNULL(tbl.FChiTieuNamTrcChuyenSang, 0) as FKHVNamTruocChuyenNamNay, --10
		ISNULL(tbl.fThanhToanKLHT_CTNamTrcChuyenSang, 0) as FTongThanhToanSuDungVonNamTruoc, --12
		ISNULL(tbl.FTamUngChuaThuHoi_CTNamTrcChuyenSang, 0) as FTamUngNamNayDungVonNamTruoc, CAST(0 as float) as FThuHoiTamUngNamNayDungVonNamTruoc,  --13
		ISNULL(tbl.FGiaTriNamTruocChuyenNamSau, 0) as FGiaTriNamTruocChuyenNamSau, --14
		ISNULL(tbl.FChiTieuNamNay, 0) as FKHVNamNay, --16
		ISNULL(tbl.FThanhToanKLHT_CTNamNay, 0) as FTongThanhToanSuDungVonNamNay, --18
		ISNULL(tbl.fTamUngChuaThuHoi_CTNamNay, 0) as FTongTamUngNamNay, CAST(0 as float) as FTongThuHoiTamUngNamNay, --19
		ISNULL(tbl.FGiaTriNamNayChuyenNamSau, 0) as FGiaTriNamNayChuyenNamSau, --20
		tbl.iID_LoaiCongTrinh as IIdLoaiCongTrinh
	FROM VDT_QT_BCQuyetToanNienDo_ChiTiet_01 as tbl
	INNER JOIN #tmpDuAn as tmp on tbl.iID_DuAnID = tmp.iID_DuAnID
	INNER JOIN VDT_DA_DuAn as da on tbl.iID_DuAnID = da.iID_DuAnID
	LEFT JOIN #tmpTongMucDauTu as tmdt on tbl.iID_DuAnID = tmdt.iID_DuAnID
	LEFT JOIN VDT_DM_LoaiCongTrinh as lct on tbl.iID_LoaiCongTrinh = lct.iID_LoaiCongTrinh
	WHERE iID_BCQuyetToanNienDo = @iIdQuyetToanId
	ORDER BY tbl.ICoQuanThanhToan

	DROP TABLE #tmpTongMucDauTu
	DROP TABLE #tmpDuAn
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_getall_hangmuc_by_duan_detail]    Script Date: 06/03/2023 9:03:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_getall_hangmuc_by_duan_detail] 
	@duAnId nvarchar(500)
	
AS
BEGIN
	
	select 
			dahm.iID_DuAn_HangMucID as Id,
			dahm.iID_DuAn_HangMucID as IdDuAnHangMuc,
			dahm.iID_DuAnID as IIdDuAnId,
			null as IdChuTruongHangMuc,
			dahm.sMaHangMuc as SMaHangMuc,
			dahm.sTenHangMuc as STenHangMuc,
			dahm.iID_ParentID as IIdParentId,
			dahm.maOrder as MaOrDer,
			null as IdChuTruong,
			null as LoaiCongTrinhId,
			null as IsEditHangMuc,
			lct.sTenLoaiCongTrinh as TenLoaiCongTrinh,
			dahm.fHanMucDauTu as HanMucDT,
			isnull(cast(case when parentId.iID_ParentID is not null or dahm.iID_ParentID is null then 1 else 0 end as bit),0)  as IsHangCha
			
	from VDT_DA_DuAn_HangMuc dahm
			left join VDT_DM_LoaiCongTrinh lct ON lct.iID_LoaiCongTrinh = dahm.IdLoaiCongTrinh AND dahm.iID_DuAnID = @duAnId
			left join
			(
				select distinct iID_ParentID from VDT_DA_DuAn_HangMuc tb1
				
				where  tb1.iID_ParentID is not null 
			) as parentId ON parentId.iID_ParentID = dahm.iID_DuAn_HangMucID
		where dahm.iID_DuAnID = @duAnId
		order by MaOrDer
END;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_insert_tonghopquyettoan]    Script Date: 06/03/2023 9:03:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_vdt_insert_tonghopquyettoan]
@iIdQuyetToan uniqueidentifier,
@table t_tbl_tonghopdautu_v2 READONLY
AS
BEGIN
	DECLARE @iIdMaDonVi nvarchar(100)
	DECLARE @iIdNguonVon int
	DECLARE @sSoQuyetDinh nvarchar(100)
	DECLARE @dNgayQuyetDinh date
	DECLARE @iNamKeHoach int

	DECLARE @iCheck int = (SELECT COUNT(id) FROM VDT_QT_BCQuyetToanNienDo WHERE Id = @iIdQuyetToan)

	IF(ISNULL(@iCheck, 0) <> 0)
	BEGIN
		SELECT @iIdMaDonVi = iID_MaDonViQuanLy,
			@iIdNguonVon = iID_NguonVonID,
			@sSoQuyetDinh = sSoDeNghi,
			@dNgayQuyetDinh = dNgayDeNghi,
			@iNamKeHoach = iNamKeHoach
		FROM VDT_QT_BCQuyetToanNienDo WHERE Id = @iIdQuyetToan
	END
	ELSE
	BEGIN
		SELECT @iIdMaDonVi = iID_MaDonVi,
			@iIdNguonVon = 1,
			@sSoQuyetDinh = NULL,
			@dNgayQuyetDinh = dNgayKhoiTao,
			@iNamKeHoach = iNamKhoiTao
		FROM VDT_KT_KhoiTao_DuLieu WHERE Id = @iIdQuyetToan
	END

	INSERT INTO VDT_TongHop_NguonNSDauTu(iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, sMaNguonCha, iStatus, bIsLog, iID_DuAnID, sMaTienTrinh, iThuHoiTUCheDo,ILoaiUng,
										iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG, fGiaTri, iID_LoaiCongTrinh)
	SELECT @iIdMaDonVi, @iIdNguonVon, @sSoQuyetDinh, @dNgayQuyetDinh, @iNamKeHoach, @iIdQuyetToan, tbl.sMaNguon, tbl.sMaDich, tbl.sMaNguonCha, 2, 0, tbl.iID_DuAnID, '100', tbl.iThuHoiTUCheDo,ILoaiUng,
			tbl.IIDMucID, tbl.IIDTieuMucID, tbl.IIDTietMucID, tbl.IIDNganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, fGiaTri, tbl.IIdLoaiCongTrinh
	FROM @table as tbl
	LEFT JOIN NS_MucLucNganSach as ml on ml.iID = tbl.IIDMucID OR ml.iID = tbl.IIDTieuMucID OR ml.iID = tbl.IIDTietMucID OR ml.iID = tbl.IIDNganhID
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_tt_get_mlns_by_khv]    Script Date: 06/03/2023 9:03:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_tt_get_mlns_by_khv]
	@iNamLamViec [int],
	@data [dbo].[t_tbl_tonghopdautu_v2] READONLY
WITH EXECUTE AS CALLER
AS
BEGIN
	SELECT tbl.iID_ChungTu as IidKeHoachVonId, 
		(CASE WHEN khvn.iID_PhanBoVonID IS NOT NULL THEN 1 ELSE 2 END) as ILoaiKeHoachVon,
		(CASE WHEN khvn.iID_PhanBoVonID IS NOT NULL THEN khvn.iID_MucID ELSE khvu.iID_MucID END) as iID_MucID,
		(CASE WHEN khvn.iID_PhanBoVonID IS NOT NULL THEN khvn.iID_TieuMucID ELSE khvu.iID_TieuMucID END) as iID_TieuMucID,
		(CASE WHEN khvn.iID_PhanBoVonID IS NOT NULL THEN khvn.iID_TietMucID ELSE khvu.iID_TietMucID END) as iID_TietMucID,
		(CASE WHEN khvn.iID_PhanBoVonID IS NOT NULL THEN khvn.iID_NganhID ELSE khvu.iID_NganhID END) as iID_NganhID INTO #tmp
	FROM @data as tbl
	LEFT JOIN VDT_KHV_PhanBoVon_ChiTiet as khvn on tbl.iID_ChungTu = khvn.iID_PhanBoVonID AND tbl.sMaNguon = N'KHVN' AND khvn.iID_DuAnID = tbl.iID_DuAnID and khvn.iID_DuAn_HangMucID = tbl.IIdLoaiCongTrinh
	LEFT JOIN VDT_KHV_KeHoachVonUng_ChiTiet as khvu on tbl.iID_ChungTu = khvu.iID_KeHoachUngID AND tbl.sMaNguon = N'KHVU' AND khvu.iID_DuAnID = tbl.iID_DuAnID and khvu.ID_DuAn_HangMuc = tbl.IIdLoaiCongTrinh

	SELECT tmp.IidKeHoachVonId, tmp.ILoaiKeHoachVon, ml.sLNS as LNS, ml.sL as L, ml.sK as K, ml.sM as M, ml.sTM as TM, ml.sTTM as TTM, ml.sNG as NG INTO #tmpMucLuc
	FROM #tmp as tmp
	INNER JOIN NS_MucLucNganSach as ml on ml.iID = tmp.iID_MucID
										OR ml.iID = tmp.iID_TieuMucID
										OR ml.iID = tmp.iID_TietMucID
										OR ml.iID = tmp.iID_NganhID

	SELECT IidKeHoachVonId, ILoaiKeHoachVon, LNS, L, K, M, '' as TM, '' as TTM, '' as NG INTO #tmpResult
	FROM #tmpMucLuc
	WHERE ISNULL(M, '') <> ''
	UNION ALL
	SELECT IidKeHoachVonId, ILoaiKeHoachVon, LNS, L, K, M, TM, '' as TTM, '' as NG
	FROM #tmpMucLuc
	WHERE ISNULL(TM, '') <> ''
	UNION ALL
	SELECT IidKeHoachVonId, ILoaiKeHoachVon, LNS, L, K, M, TM, TTM, '' as NG
	FROM #tmpMucLuc
	WHERE ISNULL(TTM, '') <> ''
	UNION ALL
	SELECT IidKeHoachVonId, ILoaiKeHoachVon, LNS, L, K, M, TM, TTM, NG
	FROM #tmpMucLuc
	WHERE ISNULL(NG, '') <> ''

	SELECT DISTINCT tbl.*, sMoTa as SMoTa
	FROM #tmpResult as tbl
	INNER JOIN NS_MucLucNganSach as ml on ml.iNamLamViec = @iNamLamViec
									AND tbl.LNS COLLATE DATABASE_DEFAULT = ml.sLNS COLLATE DATABASE_DEFAULT
									AND tbl.L COLLATE DATABASE_DEFAULT = ml.sL COLLATE DATABASE_DEFAULT
									AND tbl.K COLLATE DATABASE_DEFAULT = ml.sK COLLATE DATABASE_DEFAULT
									AND tbl.M COLLATE DATABASE_DEFAULT = ml.sM COLLATE DATABASE_DEFAULT
									AND tbl.TM COLLATE DATABASE_DEFAULT = ml.sTM COLLATE DATABASE_DEFAULT
									AND tbl.TTM COLLATE DATABASE_DEFAULT = ml.sTTM COLLATE DATABASE_DEFAULT
									AND tbl.NG COLLATE DATABASE_DEFAULT = ml.sNG COLLATE DATABASE_DEFAULT
	ORDER BY LNS, L, K, M, TM, TTM, NG

	 DROP TABLE #tmpResult
	 DROP TABLE #tmpMucLuc
	DROP TABLE #tmp
END
;
;
GO
