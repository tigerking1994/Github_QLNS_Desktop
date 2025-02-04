/****** Object:  StoredProcedure [dbo].[sp_vdt_tt_get_khvthanhtoan]    Script Date: 16/06/2022 5:30:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_tt_get_khvthanhtoan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_tt_get_khvthanhtoan]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_khv_kehoach_5_nam_de_xuat_export]    Script Date: 16/06/2022 5:30:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_khv_kehoach_5_nam_de_xuat_export]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_khv_kehoach_5_nam_de_xuat_export]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_duan_in_phanbovon_donvi]    Script Date: 16/06/2022 5:30:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_duan_in_phanbovon_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_duan_in_phanbovon_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_get_skt_chungtuchitiet]    Script Date: 16/06/2022 5:30:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_get_skt_chungtuchitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_get_skt_chungtuchitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_khv_khth_list_dexuat_chitiet]    Script Date: 16/06/2022 5:30:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khv_khth_list_dexuat_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khv_khth_list_dexuat_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_tonghopnguonnsdautu_tang]    Script Date: 16/06/2022 5:30:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_insert_tonghopnguonnsdautu_tang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_insert_tonghopnguonnsdautu_tang]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_denghithanhtoan_index_2]    Script Date: 17/06/2022 10:36:24 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_denghithanhtoan_index_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_denghithanhtoan_index_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_denghithanhtoan_index_2]    Script Date: 17/06/2022 10:36:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_denghithanhtoan_index_2]
	@YearOfWork int,
	@UserName nvarchar(100)
AS
BEGIN

	DECLARE @CountDonViCha int;
	SELECT 
		@CountDonViCha = count(*) FROM (SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName AND iNamLamViec = @YearOfWork AND iTrangThai = 1) nddv
	INNER JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1 AND iLoai = 0) dv
	ON dv.iID_MaDonVi = nddv.iID_MaDonVi;

	SELECT DISTINCT iID_DeNghiThanhToanID, tbl.iLoai,
		(CASE WHEN khvn.Id IS NOT NULL THEN khvn.sSoQuyetDinh
			WHEN khvu.Id IS NOT NULL THEN khvu.sSoQuyetDinh
			WHEN qt.Id IS NOT NULL THEN qt.sSoDeNghi
		END) as sSoQuyetDinh INTO #tmp
	FROM VDT_TT_DeNghiThanhToan_KHV as tbl
	LEFT JOIN VDT_KHV_PhanBoVon as khvn on tbl.iID_KeHoachVonID = khvn.Id AND tbl.iLoai = 1
	LEFT JOIN VDT_KHV_KeHoachVonUng as khvu on tbl.iID_KeHoachVonID = khvu.Id AND tbl.iLoai = 2
	LEFT JOIN VDT_QT_BCQuyetToanNienDo as qt on tbl.iID_KeHoachVonID = qt.Id AND tbl.iLoai in (3,4)


	SELECT 
	  iID_DeNghiThanhToanID,
	  STUFF((
		SELECT '; ' + sSoQuyetDinh
		FROM #tmp 
		WHERE (iID_DeNghiThanhToanID = Results.iID_DeNghiThanhToanID) 
		FOR XML PATH(''),TYPE).value('(./text())[1]','VARCHAR(MAX)')
	  ,1,2,'') AS sKeHoachVon ,
	  STUFF((
		SELECT '; ' + CAST(iLoai as nvarchar(5))
		FROM #tmp 
		WHERE (iID_DeNghiThanhToanID = Results.iID_DeNghiThanhToanID) 
		FOR XML PATH(''),TYPE).value('(./text())[1]','VARCHAR(MAX)')
	  ,1,2,'') AS sLoaiKeHoachVon
	  INTO #tmpKhv
	FROM #tmp Results
	GROUP BY iID_DeNghiThanhToanID


	SELECT tbl.*, ns.sTen as sNguonVon, lnv.sMoTa as sLoaiNguonVon, dv.sTenDonVi as sTenDonVi, 
		da.sTenDuAn, hd.sSoHopDong, hd.dNgayHopDong, hd.fTienHopDong as fGiaTriHopDong, nt.sMaNhaThau, da.sMaDuAn, khv.sKeHoachVon, khv.sLoaiKeHoachVon, tbl.iID_ChiPhiID as IIdChiPhiId,
		hd.sTenHopDong
	FROM VDT_TT_DeNghiThanhToan as tbl
	LEFT JOIN NguonNganSach as ns on tbl.iID_NguonVonID = ns.iID_MaNguonNganSach
	LEFT JOIN NS_MucLucNganSach as lnv on tbl.iID_LoaiNguonVonID = lnv.iID_MLNS
	LEFT JOIN DonVi as dv on tbl.iID_DonViQuanLyID = dv.iID_DonVi
	LEFT JOIN VDT_DA_DuAn as da on tbl.iID_DuAnId = da.iID_DuAnID
	LEFT JOIN VDT_DA_TT_HopDong as hd on tbl.iID_HopDongId = hd.Id
	LEFT JOIN VDT_DM_NhaThau as nt on tbl.iID_NhaThauId = nt.Id
	LEFT JOIN #tmpKhv as khv on tbl.Id = khv.iID_DeNghiThanhToanID
	WHERE 
	(
		( EXISTS (SELECT * from f_split(tbl.iID_MaDonViQuanLy) INTERSECT SELECT iID_MaDonVi FROM NguoiDung_DonVi  WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork)
			AND (@CountDonViCha = 0)
		)
		OR (@CountDonViCha <> 0 AND tbl.bKhoa = 1)
		OR 
		(   EXISTS (SELECT * from f_split(tbl.iID_MaDonViQuanLy) INTERSECT SELECT iID_MaDonVi FROM NguoiDung_DonVi  WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork)
			AND (@CountDonViCha <> 0)
		)
	) and (tbl.bTongHop is null or tbl.bTongHop != 1)


	ORDER BY tbl.dDateCreate DESC

	DROP TABLE #tmp
END
GO

/****** Object:  StoredProcedure [dbo].[sp_insert_tonghopnguonnsdautu_tang]    Script Date: 16/06/2022 5:30:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_insert_tonghopnguonnsdautu_tang]
@sLoai nvarchar(100),
@iTypeExecute int,
@uIdQuyetDinh uniqueidentifier,
@iIDQuyetDinhOld uniqueidentifier
AS
BEGIN
	CREATE TABLE #lstMaNguon(sMaNguon nvarchar(100))

	DECLARE @RankDate DATETIME = (SELECT TOP(1) dNgayQuyetDinh FROM VDT_TongHop_NguonNSDauTu WHERE sMaNguon COLLATE DATABASE_DEFAULT = 'QUYET_TOAN' ORDER BY dNgayQuyetDinh DESC)

	IF(@sLoai = 'KHVN')
	BEGIN
		INSERT INTO #lstMaNguon(sMaNguon)
		VALUES('KHVN'), ('101'), ('102'),('111'),('112')
	END
	ELSE IF(@sLoai = 'KHVU')
	BEGIN
		INSERT INTO #lstMaNguon(sMaNguon)
		VALUES('KHVU'), ('121a'), ('122a')
	END
	ELSE IF(@sLoai = 'QUYET_TOAN')
	BEGIN
		INSERT INTO #lstMaNguon(sMaNguon)
		VALUES('QUYET_TOAN'), ('131'), ('132'), ('211c'), ('212c'), ('301'), ('302'), ('321a'), ('322a')
			, ('000'), ('321b'), ('322b')
	END

	IF(@iTypeExecute in (2,3,4))
	BEGIN 
		IF (@iTypeExecute in (2,3))
		BEGIN
			-- dao nguoc but toan
			INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, sMaNguonCha, fGiaTri, bIsLog, iStatus, sMaTienTrinh,
												iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG)
			--OUTPUT inserted.Id, inserted.sMaDich, inserted.iID_DuAnID, ISNULL(inserted.fGiaTri, 0) INTO #tmp(iId, sMaNguon, iIdDuAnId, fDaThanhToan)
			SELECT tbl.iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, tbl.sMaDich, tbl.sMaNguon, tbl.sMaNguonCha, fGiaTri, 1, 2, '300',
				iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG
			FROM VDT_TongHop_NguonNSDauTu as tbl
			INNER JOIN #lstMaNguon as dt on tbl.sMaNguon COLLATE DATABASE_DEFAULT = dt.sMaNguon COLLATE DATABASE_DEFAULT
			WHERE tbl.iID_ChungTu = @uIdQuyetDinh 
				AND bIsLog = 0 AND sMaTienTrinh = (CASE WHEN @sLoai = 'QUYET_TOAN' THEN '100' ELSE '200' END)

			-- khoa but toan da update
			UPDATE tbl 
			SET 
				bIsLog = 1
			FROM VDT_TongHop_NguonNSDauTu as tbl
			INNER JOIN #lstMaNguon as dt on tbl.sMaNguon COLLATE DATABASE_DEFAULT = dt.sMaNguon COLLATE DATABASE_DEFAULT
			WHERE tbl.iID_ChungTu = @uIdQuyetDinh
				AND bIsLog = 0 AND sMaTienTrinh in ('100', '200')
		END
		ELSE IF (@iTypeExecute = 4)
		BEGIN
			-- dao nguoc but toan
			INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, sMaNguonCha, fGiaTri, bIsLog, iStatus, sMaTienTrinh,
												iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG)
			--OUTPUT inserted.Id, inserted.sMaDich, inserted.iID_DuAnID, ISNULL(inserted.fGiaTri, 0) INTO #tmp(iId, sMaNguon, iIdDuAnId, fDaThanhToan)
			SELECT tbl.iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, tbl.sMaDich, tbl.sMaNguon, tbl.sMaNguonCha, fGiaTri, 1, 2, '300',
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG
			FROM VDT_TongHop_NguonNSDauTu as tbl
			INNER JOIN #lstMaNguon as dt on tbl.sMaNguon COLLATE DATABASE_DEFAULT = dt.sMaNguon COLLATE DATABASE_DEFAULT
			WHERE tbl.iID_ChungTu = @iIDQuyetDinhOld 
				AND bIsLog = 0 AND sMaTienTrinh in ('100', '200')

			-- khoa but toan da update
			UPDATE tbl 
			SET 
				bIsLog = 1
			FROM VDT_TongHop_NguonNSDauTu as tbl
			INNER JOIN #lstMaNguon as dt on tbl.sMaNguon COLLATE DATABASE_DEFAULT = dt.sMaNguon COLLATE DATABASE_DEFAULT
			WHERE tbl.iID_ChungTu = @iIDQuyetDinhOld
				AND bIsLog = 0 AND sMaTienTrinh in ('100', '200')
		END

		-- deleted thi khong xu ly nua
		IF(@iTypeExecute = 3)
		BEGIN
			RETURN
		END
	END

	IF(@sLoai = 'KHVN')
	BEGIN
		-- insert but toan moi vao 
		INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, fGiaTri, iStatus, sMaTienTrinh,
											iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG)
		SELECT dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID,
				tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon, '000', SUM(ISNULL(dt.fGiaTri, 0)), 0, 
				(CASE WHEN sMaNguon in ('101', '102') THEN '200' ELSE '100' END),
				iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG
		FROM VDT_KHV_PhanBoVon as tbl
		INNER JOIN (select Id,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, dt.fGiaTri, 
					(CASE colName WHEN 'fCapPhatTaiKhoBac' THEN '101' 
								WHEN 'fCapPhatBangLenhChi' THEN '102'END) as sMaNguon
					from 
					(select Id,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID , fCapPhatTaiKhoBac,fCapPhatBangLenhChi,fGiaTriThuHoiNamTruocKhoBac,fGiaTriThuHoiNamTruocLenhChi from VDT_KHV_PhanBoVon_ChiTiet) as tbl
					UNPIVOT
					(fGiaTri FOR colName IN (fCapPhatTaiKhoBac,fCapPhatBangLenhChi,fGiaTriThuHoiNamTruocKhoBac,fGiaTriThuHoiNamTruocLenhChi)) as dt) as dt on dt.iID_PhanBoVonID = tbl.Id
		LEFT JOIN NS_MucLucNganSach as ml on (dt.iID_MucID = ml.iID OR dt.iID_TieuMucID = ml.iID OR dt.iID_TietMucID = ml.iID OR dt.iID_NganhID = ml.iID)
		WHERE tbl.Id = @uIdQuyetDinh AND tbl.iLoaiDuToan <> 2
		GROUP BY dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID, tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon,
				iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG

		INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, fGiaTri, iStatus, sMaTienTrinh,
											iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG)
		SELECT dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID,
				tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon, '000', SUM(ISNULL(dt.fGiaTri, 0)), 0, 
				(CASE WHEN sMaNguon in ('101', '102') THEN '200' ELSE '100' END),
				iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG
		FROM VDT_KHV_PhanBoVon as tbl
		INNER JOIN (select Id,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, dt.fGiaTri, 
					(CASE colName WHEN 'fCapPhatTaiKhoBac' THEN '111' 
								WHEN 'fCapPhatBangLenhChi' THEN '112'END) as sMaNguon
					from 
					(select Id,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID , fCapPhatTaiKhoBac,fCapPhatBangLenhChi,fGiaTriThuHoiNamTruocKhoBac,fGiaTriThuHoiNamTruocLenhChi from VDT_KHV_PhanBoVon_ChiTiet) as tbl
					UNPIVOT
					(fGiaTri FOR colName IN (fCapPhatTaiKhoBac,fCapPhatBangLenhChi,fGiaTriThuHoiNamTruocKhoBac,fGiaTriThuHoiNamTruocLenhChi)) as dt) as dt on dt.iID_PhanBoVonID = tbl.Id
		LEFT JOIN NS_MucLucNganSach as ml on (dt.iID_MucID = ml.iID OR dt.iID_TieuMucID = ml.iID OR dt.iID_TietMucID = ml.iID OR dt.iID_NganhID = ml.iID)
		WHERE tbl.Id = @uIdQuyetDinh AND tbl.iLoaiDuToan = 2
		GROUP BY dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID, tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon,
				iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG
	END
	ELSE IF(@sLoai = 'KHVU')
	BEGIN
		-- insert but toan moi vao 
		INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, fGiaTri, iStatus, sMaTienTrinh,
											iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG)
		SELECT dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID,
				tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon , '000', SUM(ISNULL(dt.fGiaTri, 0)), 0, '200',
				iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG
		FROM VDT_KHV_KeHoachVonUng as tbl
		INNER JOIN (select Id,dt.iID_KeHoachUngID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, dt.fGiaTri, 
					(CASE colName WHEN 'fCapPhatTaiKhoBac' THEN '121a' WHEN 'fCapPhatBangLenhChi' THEN '122a' END) as sMaNguon
					from 
					(select Id,iID_KeHoachUngID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, fCapPhatTaiKhoBac,fCapPhatBangLenhChi from VDT_KHV_KeHoachVonUng_ChiTiet) as tbl
					UNPIVOT
					(fGiaTri FOR colName IN (fCapPhatTaiKhoBac,fCapPhatBangLenhChi)) as dt) as dt on dt.iID_KeHoachUngID = tbl.Id
		LEFT JOIN NS_MucLucNganSach as ml on (dt.iID_MucID = ml.iID OR dt.iID_TieuMucID = ml.iID OR dt.iID_TietMucID = ml.iID OR dt.iID_NganhID = ml.iID)
		WHERE tbl.Id = @uIdQuyetDinh
		GROUP BY dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID, tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon,
			iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG
	END
	ELSE IF(@sLoai = 'QUYET_TOAN')
	BEGIN
		-- insert but toan moi vao 
		INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, fGiaTri, iStatus, sMaTienTrinh)
		SELECT tbl.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID, tbl.sSoDeNghi, tbl.dNgayDeNghi, 
			(CASE WHEN tbl.sMaNguon = '211c' OR tbl.sMaNguon = '212c' THEN tbl.iNamKeHoach ELSE tbl.iNamKeHoach + 1 END) as iNamKeHoach
			, tbl.iID_ChungTu, tbl.sMaNguon, tbl.sMaDich, tbl.fGiaTri, tbl.iStatus, '100'
		FROM 
		(SELECT dt.iID_DuAnID, 
				tbl.iID_MaDonViQuanLy, 
				tbl.iID_NguonVonID,
				tbl.sSoDeNghi, 
				tbl.dNgayDeNghi, 
				tbl.iNamKeHoach as iNamKeHoach, 
				tbl.Id as iID_ChungTu, 
				(CASE 
					WHEN colName = 'fGiaTriTamUngDieuChinhGiam' AND dt.iCoQuanThanhToan = 1 THEN '211c'
					WHEN colName = 'fGiaTriTamUngDieuChinhGiam' AND dt.iCoQuanThanhToan = 2 THEN '212c'
				END) as sMaNguon, 
				'000' as sMaDich, 
				SUM(ISNULL(dt.fGiaTri, 0)) as fGiaTri, 
				SUM(ISNULL(dt.fGiaTri, 0)) as fSoDu, 
				0 as iStatus
		FROM VDT_QT_BCQuyetToanNienDo as tbl
		INNER JOIN (select Id, iID_BCQuyetToanNienDo, iCoQuanThanhToan, iID_DuAnID, dt.fGiaTri, colName
					from 
					(select Id, iID_BCQuyetToanNienDo, iCoQuanThanhToan, iID_DuAnID, fGiaTriTamUngDieuChinhGiam from VDT_QT_BCQuyetToanNienDo_ChiTiet_01) as tbl
					UNPIVOT
					(fGiaTri FOR colName IN (fGiaTriTamUngDieuChinhGiam)) as dt) as dt on dt.iID_BCQuyetToanNienDo = tbl.Id
		WHERE tbl.Id = @uIdQuyetDinh
		GROUP BY dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID, tbl.sSoDeNghi, tbl.dNgayDeNghi, tbl.iNamKeHoach, tbl.Id , dt.colName, dt.iCoQuanThanhToan
		) as tbl

		INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, fGiaTri, iStatus, sMaTienTrinh)
		SELECT dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID, tbl.sSoDeNghi, tbl.dNgayDeNghi, (tbl.iNamKeHoach + 1), tbl.Id, 
			(CASE WHEN dt.iCoQuanThanhToan = 1 THEN '131' ELSE '132' END), '000', 
			SUM(ISNULL(dt.fKHUngTrcChuaThuHoiTrcNamQuyetToan, 0) - ISNULL(dt.fThuHoiUngTruoc, 0) + ISNULL(dt.fKHUngNamNay, 0) - 
				(ISNULL(dt.fLKThanhToanDenTrcNamQuyetToan_KHUng, 0) - ISNULL(dt.fGiaTriThuHoiTheoGiaiNganThucTe, 0) + ISNULL(dt.fThanhToan_KHUngNamTrcChuyenSang, 0) + ISNULL(dt.fThanhToan_KHUngNamNay, 0))), 
			2, '100'
		FROM VDT_QT_BCQuyetToanNienDo as tbl
		INNER JOIN VDT_QT_BCQuyetToanNienDo_ChiTiet_01 as dt on tbl.Id = dt.iID_BCQuyetToanNienDo
		WHERE tbl.Id = @uIdQuyetDinh AND iLoaiThanhToan = 2
		GROUP BY dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID, tbl.sSoDeNghi, tbl.dNgayDeNghi, tbl.iNamKeHoach, tbl.Id, dt.iCoQuanThanhToan
	END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_khv_khth_list_dexuat_chitiet]    Script Date: 16/06/2022 5:30:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_khv_khth_list_dexuat_chitiet]
	@VoucherId nvarchar(255)
AS
BEGIN
	declare @VoucherAgregate uniqueidentifier;
	select @VoucherAgregate = iID_TongHopParent from VDT_KHV_KeHoach5Nam_DeXuat where Id = @VoucherId;

	SELECT DISTINCT
		ctct.STT					AS STT,
		ctct.Id						AS Id,
		ctct.iID_KeHoach5NamID		AS IIdKeHoach5NamId,
		ctctdd.iID_DuAnID			AS iIdDuAnId,
		ctct.sTen					AS STen,
		ctct.SDiaDiem				AS SDiaDiem,
		ctct.IGiaiDoanTu			AS IGiaiDoanTu,
		ctct.IGiaiDoanDen			AS IGiaiDoanDen,
		ctct.iID_LoaiCongTrinhID	AS IIdLoaiCongTrinhId,
		NULL						AS STenLoaiCongTrinh,
		ctct.iID_NguonVonID			AS IIdNguonVonId,
		NULL						AS STenNguonVon,
		ctct.iID_DonViQuanLyID		AS IIdDonViId,
		ctct.iID_MaDonVi			AS IIdMaDonVi,
		NULL						AS STenDonVi,
		ctct.fHanMucDauTu			AS FHanMucDauTu,
		CAST(0 AS float)			AS FVonNSQPLuyKe,
		CAST(0 AS float)			AS FVonNSQP,
		ctct.fGiaTriNamThuNhat		AS FGiaTriNamThuNhat,
		ctct.fGiaTriNamThuHai		AS FGiaTriNamThuHai,
		ctct.fGiaTriNamThuBa		AS FGiaTriNamThuBa,
		ctct.fGiaTriNamThuTu		AS FGiaTriNamThuTu,
		ctct.fGiaTriNamThuNam		AS FGiaTriNamThuNam,
		ctct.fGiaTriBoTri			AS FGiaTriBoTri,
		ctct.fGiaTriKeHoach			AS FGiaTriKeHoach,
		CAST(0 AS float)			AS FTongSoNhuCauNSQP,
		CAST(0 AS float)			AS FGiaTriNamThuNhatOrigin,
		CAST(0 AS float)			AS FGiaTriNamThuHaiOrigin,
		CAST(0 AS float)			AS FGiaTriNamThuBaOrigin,
		CAST(0 AS float)			AS FGiaTriNamThuTuOrigin,
		CAST(0 AS float)			AS FGiaTriNamThuNamOrigin,
		CAST(0 AS float)			AS FGiaTriBoTriOrigin,
		CAST(0 AS float)			AS FGiaTriKeHoachOrigin,
		CAST(0 AS float)			AS FTongSoNhuCauNSQPOrigin,
		ctct.iID_ParentModified		AS IdParentModified,
		ctct.sGhiChu				AS SGhiChu,
		CASE 
			WHEN ctct.IdParent IS NULL THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT)
		END							AS IsHangCha,
		ctct.Level					AS Level,
		ctct.indexCode				AS IndexCode,
		ctct.SMaOrder				AS SMaOrder,
		ctct.IdReference			AS IdReference,
		ctct.IdParent				AS IdParent,
		ctct.IsParent				AS IsParent,
		ctct.IsStatus				AS IsStatus,
		ctct.sTrangThai				AS STrangThai
  FROM VDT_KHV_KeHoach5Nam_DeXuat as tbl
  left JOIN VDT_KHV_KeHoach5Nam_DeXuat_ChiTiet ctct on ctct.iID_KeHoach5NamID = tbl.Id
  left JOIN VDT_DA_DuAn da ON ctct.Id = da.Id_DuAnKhthDeXuat OR ctct.IdReference = da.Id_DuAnKhthDeXuat OR ctct.iID_DuAnID = da.iID_DuAnID
  left JOIN 
  (
    SELECT ddct.iID_DuAnID, ddct.iID_NguonVonID, dd.iGiaiDoanTu, dd.iGiaiDoanDen 
    FROM VDT_KHV_KeHoach5Nam as dd 
    INNER JOIN VDT_KHV_KeHoach5Nam_ChiTiet as ddct on dd.iID_KeHoach5NamID = ddct.iID_KeHoach5NamID
  ) as ctctdd  ON ctctdd.iID_DuAnID = da.iID_DuAnID AND (ctct.iID_NguonVonID = ctctdd.iID_NguonVonID OR ctct.IdReference IS NULL) AND (tbl.iGiaiDoanTu = ctctdd.iGiaiDoanTu AND tbl.iGiaiDoanDen = ctctdd.iGiaiDoanDen)
	WHERE 
		ctct.iID_KeHoach5NamID = @VoucherAgregate
		and ctct.iID_TongHop = @VoucherId
END
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_kehoach_chitiet_canbo]    Script Date: 15/12/2021 6:34:37 PM ******/
SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_get_skt_chungtuchitiet]    Script Date: 16/06/2022 5:30:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ns_get_skt_chungtuchitiet]
	@YearOfWork int,
	@iID_MaBQuanLy nvarchar(200)
	
AS
BEGIN
	SET NOCOUNT ON;

	if(@iID_MaBQuanLy = '0')
		begin
			select distinct mlskt.sKyHieu sSKT_KyHieu, mlskt.sM, mlskt.sNG_Cha, mlskt.sNG 
			into #NS_MLSKT_MLNS_map_tem
			from NS_MLSKT_MLNS map
			inner join NS_MucLucNganSach ns
			on ns.sXauNoiMa = map.sNS_XauNoiMa
			inner join NS_SKT_MucLuc mlskt  on  map.sSKT_KyHieu = mlskt.sKyHieu 
			where ns.sLNS in (select sLNS from  NS_MucLucNganSach dm where dm.iID_MaBQuanLy = '07')
			and ns.iNamLamViec = @YearOfWork
			and map.iNamLamViec = @YearOfWork
			and mlskt.iNamLamViec = @YearOfWork

			select distinct sSKT_KyHieu from  #NS_MLSKT_MLNS_map_tem 
			union all
			select distinct  mlskt.sKyHieu from  NS_SKT_MucLuc  mlskt inner join #NS_MLSKT_MLNS_map_tem on mlskt.sNG_Cha = #NS_MLSKT_MLNS_map_tem.sNG_Cha 
			where mlskt.sNG is null or  mlskt.sNG  = '' and  mlskt.iNamLamViec = @YearOfWork
			union all
			select  distinct  mlskt.sKyHieu from  NS_SKT_MucLuc  mlskt inner join #NS_MLSKT_MLNS_map_tem on mlskt.sM = #NS_MLSKT_MLNS_map_tem.sM 
			where (mlskt.sNG is null or mlskt.sNG ='')  and (mlskt.sNG_Cha is null or   mlskt.sNG_Cha = '') 
			and mlskt.iNamLamViec = @YearOfWork
			order by sSKT_KyHieu

			drop table #NS_MLSKT_MLNS_map_tem
		end
		
	if(@iID_MaBQuanLy != '0')
		begin
			select distinct mlskt.sKyHieu sSKT_KyHieu, mlskt.sM, mlskt.sNG_Cha, mlskt.sNG 
			into #NS_MLSKT_MLNS_map_tem_
			from NS_MLSKT_MLNS map
			inner join NS_MucLucNganSach ns
			on ns.sXauNoiMa = map.sNS_XauNoiMa
			inner join NS_SKT_MucLuc mlskt  on  map.sSKT_KyHieu = mlskt.sKyHieu 
			where ns.sLNS in (select sLNS from  NS_MucLucNganSach)
			and ns.iNamLamViec = @YearOfWork
			and map.iNamLamViec = @YearOfWork
			and mlskt.iNamLamViec = @YearOfWork

			select distinct sSKT_KyHieu from  #NS_MLSKT_MLNS_map_tem_ 
			union all
			select distinct  mlskt.sKyHieu from  NS_SKT_MucLuc  mlskt inner join #NS_MLSKT_MLNS_map_tem_ on mlskt.sNG_Cha = #NS_MLSKT_MLNS_map_tem_.sNG_Cha 
			where mlskt.sNG is null or  mlskt.sNG  = '' and  mlskt.iNamLamViec = @YearOfWork
			union all
			select  distinct  mlskt.sKyHieu from  NS_SKT_MucLuc  mlskt inner join #NS_MLSKT_MLNS_map_tem_ on mlskt.sM = #NS_MLSKT_MLNS_map_tem_.sM 
			where (mlskt.sNG is null or mlskt.sNG ='')  and (mlskt.sNG_Cha is null or   mlskt.sNG_Cha = '') 
			and mlskt.iNamLamViec = @YearOfWork
			order by sSKT_KyHieu

			drop table #NS_MLSKT_MLNS_map_tem_
		end
END




GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_duan_in_phanbovon_donvi]    Script Date: 16/06/2022 5:30:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_get_duan_in_phanbovon_donvi]
	@iNamKeHoach int, 
	@ngayLap DateTime,
	@maDonViQuanLyId nvarchar(50),
	@nguonVonID int
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
		da.dDateCreate INTO #tmp
		from VDT_DA_DuAn da
		LEFT JOIN VDT_KHV_KeHoach5Nam_ChiTiet khvct on da.iID_DuAnID = khvct.iID_DuAnID
		LEFT JOIN NguonNganSach nv ON khvct.iID_NguonVonID = nv.iID_MaNguonNganSach 
		LEFT JOIN VDT_DA_DuAn_HangMuc dahm on da.iID_DuAnID = dahm.iID_DuAnID
		LEFT JOIN VDT_DM_LoaiCongTrinh as lct on da.iID_LoaiCongTrinhID = lct.iID_LoaiCongTrinh or dahm.IdLoaiCongTrinh = lct.iID_LoaiCongTrinh
		LEFT JOIN VDT_DM_PhanCapDuAn as pc on da.iID_CapPheDuyetID = pc.iID_PhanCapID
		LEFT JOIN DM_ChuDauTu as cdt on da.iID_ChuDauTuID = cdt.iID_DonVi
		LEFT JOIN VDT_DM_DonViThucHienDuAn dvthda on da.iID_MaDonViThucHienDuAnID = dvthda.iID_MaDonVi
		Where  da.iID_MaDonViThucHienDuAnID in (select * from f_recursive_donvi(@maDonViQuanLyId)) And [dbo].[fn_CheckDieuKienDuAn](da.iID_DuAnID,@ngayLap) = 1 
			And ( da.iID_DuAnID in (SELECT DISTINCT qqdt.iID_DuAnID FROM VDT_DA_QDDauTu qqdt JOIN VDT_DA_QDDauTu_NguonVon qddtnv ON qqdt.iID_QDDauTuID=qddtnv.iID_QDDauTuID 
												JOIN NguonNganSach nv ON qddtnv.iID_NguonVonID=nv.iID_MaNguonNganSach 
															
												WHERE  (@nguonVonID IS NULL OR nv.iID_MaNguonNganSach=@nguonVonID))
				)


	select tmp.* into #tmpData from #tmp as tmp

	Union ALL

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
		da.dDateCreate
	from VDT_DA_DuAn da
		LEFT JOIN VDT_KHV_KeHoach5Nam_ChiTiet khvct on da.iID_DuAnID = khvct.iID_DuAnID
		LEFT JOIN NguonNganSach nv ON khvct.iID_NguonVonID = nv.iID_MaNguonNganSach 
		LEFT JOIN VDT_DA_DuAn_HangMuc dahm on da.iID_DuAnID = dahm.iID_DuAnID
		LEFT JOIN VDT_DM_LoaiCongTrinh as lct on da.iID_LoaiCongTrinhID = lct.iID_LoaiCongTrinh or dahm.IdLoaiCongTrinh = lct.iID_LoaiCongTrinh
		LEFT JOIN VDT_DM_PhanCapDuAn as pc on da.iID_CapPheDuyetID = pc.iID_PhanCapID
		LEFT JOIN DM_ChuDauTu as cdt on da.iID_ChuDauTuID = cdt.iID_DonVi
		LEFT JOIN VDT_DM_DonViThucHienDuAn dvthda on da.iID_MaDonViThucHienDuAnID = dvthda.iID_MaDonVi
	Where 
		da.iID_DuAnID in (SELECT ctdt.iID_DuAnID FROM VDT_DA_ChuTruongDauTu ctdt)
		and da.iID_DuAnID not in (select tmpexisted.iID_DuAnID from #tmp tmpexisted)
		And da.iID_MaDonViThucHienDuAnID in (select * from f_recursive_donvi(@maDonViQuanLyId))

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
				when ((da.sTrangThaiDuAn = N'THUC_HIEN') and (da.bIsKetThuc IS NULL)) then 2 else 1
			end ILoaiDuAn,
			isnull(khvnct.fVonBoTriTuNamDenNam, 0) as FKeHoachTrungHanDuocDuyet
		FROM #tmpData as tmp
		LEFT JOIN #tmpDataPD as tbl_tmdt on tmp.iID_DuAnID = tbl_tmdt.iID_DuAnID
		LEFT JOIN #tmpLuyKeNamTruoc as lknt on tmp.iID_DuAnID = lknt.iID_DuAnID
		LEFT JOIN #tmpKeHoachVonDuocDuyetNamNay as khnn on tmp.iID_DuAnID = khnn.iID_DuAnID and khnn.iID_LoaiCongTrinh = tmp.iID_LoaiCongTrinhID
		LEFT JOIN #tmpVonKeoDaiCacNamTruoc as vkd on tmp.iID_DuAnID = vkd.iID_DuAnID
		LEFT JOIN #tmpThDd khvnct on tmp.iID_DuAnID = khvnct.iID_DuAnID and tmp.iID_LoaiCongTrinhID = khvnct.iID_LoaiCongTrinhID and khvnct.iID_NguonVonID = @nguonVonID
		LEFT JOIN VDT_DA_DuAn da on tmp.iID_DuAnID = da.iID_DuAnID
		ORDER BY tmp.dDateCreate desc

	drop table #tmpThDd;
	drop table #tmp;
	drop table #tmpData;
	drop table #tmpDataPD;
	drop table #tmpLuyKeNamTruoc;
	drop table #tmpKeHoachVonDuocDuyetNamNay;
	drop table #tmpVonKeoDaiCacNamTruoc;

End
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_khv_kehoach_5_nam_de_xuat_export]    Script Date: 16/06/2022 5:30:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_khv_kehoach_5_nam_de_xuat_export]
	@Id nvarchar(max),
	@lct nvarchar(max),
	@IdNguonVon nvarchar(max),
	@type int,
	@MenhGiaTienTe float,
	@iNamLamViec int
AS
BEGIN
	if(@type = 1)
	begin
		select tbl.* from (

		select
			lct.iID_LoaiCongTrinh as IdLoaiCongTrinh,
			lct.iID_Parent as IdLoaiCongTrinhParent,
			lct.sMaLoaiCongTrinh as SMaLoaiCongTrinh,
			3 as Loai,
			lct.sTenLoaiCongTrinh as STenDuAn,
			'' as STenDonVi,
			'' as SDiaDiem,
			'' as SThoiGianThucHien,
			0 as FHanMucDauTu,
			'' as STenNguonVon,
			0 as FTongSoNhuCau,
			0 as FTongSo,
			0 as FGiaTriNamThuNhat,
			0 as FGiaTriNamThuHai,
			0 as FGiaTriNamThuBa,
			0 as FGiaTriNamThuTu,
			0 as FGiaTriNamThuNam,
			0 as FGiaTriBoTri,
			'' as SGhiChu,
			cast(1 as bit) as IsHangCha,
			NEWID() as Id,
			case
				when lct.iID_Parent is null then 0 else 1
			end LoaiParent,
			0 as IIdNguonVon,
			0 as LuyKeVonNSQPDaBoTri,
			0 as LuyKeVonNSQPDeNghiBoTri,
			0 as TongLuyKe,
			null as DNgayQuyetDinh,
			cast(0 as float) as FHanMucDauTuDP,
			cast(0 as float) as FHanMucDauTuNN,
			cast(0 as float) as FHanMucDauTuOrther,
			cast(0 as float) as FHanMucDauTuQP,
			'' as SSoQuyetDinh
		from f_loai_cong_trinh_get_list_childrent(@lct) lct

		union all

		select 
			dmlct.iID_LoaiCongTrinh as IdLoaiCongTrinh,
			dmlct.iID_Parent as IdLoaiCongTrinhParent,
			dmlct.sMaLoaiCongTrinh as SMaLoaiCongTrinh,
			4 as Loai,
			ctct.sTen as STenDuAn,
			dv.sTenDonVi as STenDonVi,
			ctct.SDiaDiem as SDiaDiem,
			(cast(ctct.IGiaiDoanTu as nvarchar) + ' - ' + cast(ctct.IGiaiDoanDen as nvarchar)) as SThoiGianThucHien,
			ctct.fHanMucDauTu/@MenhGiaTienTe as FHanMucDauTu,
			ns.sTen as STenNguonVon,
			((isnull(ctct.fGiaTriNamThuNhat, 0) + isnull(ctct.fGiaTriNamThuHai, 0) 
				+ isnull(ctct.fGiaTriNamThuBa, 0) + isnull(ctct.fGiaTriNamThuTu, 0) 
				+ isnull(ctct.fGiaTriNamThuNam, 0) + isnull(ctct.fGiaTriBoTri, 0))/@MenhGiaTienTe) as FTongSoNhuCau,
			((isnull(ctct.fGiaTriNamThuNhat, 0) 
				+ isnull(ctct.fGiaTriNamThuHai, 0) + isnull(ctct.fGiaTriNamThuBa, 0)
				+ isnull(ctct.fGiaTriNamThuTu, 0) + isnull(ctct.fGiaTriNamThuNam, 0))/@MenhGiaTienTe) as FTongSo,
			(ctct.fGiaTriNamThuNhat/@MenhGiaTienTe) as FGiaTriNamThuNhat,
			(ctct.fGiaTriNamThuHai/@MenhGiaTienTe) as FGiaTriNamThuHai,
			(ctct.fGiaTriNamThuBa/@MenhGiaTienTe) as FGiaTriNamThuBa,
			(ctct.fGiaTriNamThuTu/@MenhGiaTienTe) as FGiaTriNamThuTu,
			(ctct.fGiaTriNamThuNam/@MenhGiaTienTe) as FGiaTriNamThuNam,
			(ctct.fGiaTriBoTri/@MenhGiaTienTe) as FGiaTriBoTri,
			ctct.sGhiChu as SGhiChu,
			cast(0 as bit) as IsHangCha,
			NEWID() as Id,
			1 as LoaiParent,
			ctct.iID_NguonVonID as IIdNguonVon,
			cast(0 as float) as LuyKeVonNSQPDaBoTri,
			cast(0 as float) as LuyKeVonNSQPDeNghiBoTri,
			cast(0 as float) as TongLuyKe,
			null as DNgayQuyetDinh,
			cast(0 as float) as FHanMucDauTuDP,
			cast(0 as float) as FHanMucDauTuNN,
			cast(0 as float) as FHanMucDauTuOrther,
			cast(0 as float) as FHanMucDauTuQP,
			'' as SSoQuyetDinh
		from 
			f_loai_cong_trinh_get_list_childrent(@lct) dmlct
		left join
			VDT_KHV_KeHoach5Nam_DeXuat_ChiTiet ctct
		on
			dmlct.iID_LoaiCongTrinh = ctct.iID_LoaiCongTrinhID
		left join
			VDT_DM_DonViThucHienDuAn dv
		on ctct.iID_DonViQuanLyID = dv.iID_DonVi
		left join 
			NguonNganSach ns
		on ctct.iID_NguonVonID = ns.iID_MaNguonNganSach
		where
			ctct.iID_KeHoach5NamID = @Id
			--and ctct.IsStatus = 2
			--and ctct.iID_ParentModified is null
			and ctct.iID_NguonVonID in (select * from dbo.splitstring(@IdNguonVon))) as tbl
			order by tbl.IdLoaiCongTrinh, tbl.Loai
	end
	else
	if(@type = 2)
	begin
		select 
			dmlct.iID_LoaiCongTrinh as IdLoaiCongTrinh,
			dmlct.iID_Parent as IdLoaiCongTrinhParent,
			dmlct.sMaLoaiCongTrinh as SMaLoaiCongTrinh,
			5 as Loai,
			ctct.iID_KeHoach5NamID,
			ctct.sTen as STenDuAn,
			dv.sTenDonVi as STenDonVi,
			vndx.iID_MaDonViQuanLy as IdMaDonViQuanLy,
			ctct.SDiaDiem as SDiaDiem,
			(cast(ctct.IGiaiDoanTu as nvarchar) + ' - ' + cast(ctct.IGiaiDoanDen as nvarchar)) as SThoiGianThucHien,
			(ctct.fHanMucDauTu/@MenhGiaTienTe) as FHanMucDauTu,
			ns.sTen as STenNguonVon,
			--((ctct.fGiaTriKeHoach + ctct.fGiaTriNamThuNhat + ctct.fGiaTriNamThuHai + ctct.fGiaTriNamThuBa + ctct.fGiaTriNamThuTu + ctct.fGiaTriNamThuNam + ctct.fGiaTriBoTri)/@MenhGiaTienTe) as FTongSoNhuCau,
			((ctct.fGiaTriNamThuNhat + ctct.fGiaTriNamThuHai + ctct.fGiaTriNamThuBa + ctct.fGiaTriNamThuTu + ctct.fGiaTriNamThuNam + ctct.fGiaTriBoTri)/@MenhGiaTienTe) as FTongSoNhuCau,
			((ctct.fGiaTriNamThuNhat + ctct.fGiaTriNamThuHai + ctct.fGiaTriNamThuBa + ctct.fGiaTriNamThuTu + ctct.fGiaTriNamThuNam)/@MenhGiaTienTe) as FTongSo,
			(ctct.fGiaTriNamThuNhat/@MenhGiaTienTe) as FGiaTriNamThuNhat,
			(ctct.fGiaTriNamThuHai/@MenhGiaTienTe) as FGiaTriNamThuHai,
			(ctct.fGiaTriNamThuBa/@MenhGiaTienTe) as FGiaTriNamThuBa,
			(ctct.fGiaTriNamThuTu/@MenhGiaTienTe) as FGiaTriNamThuTu,
			(ctct.fGiaTriNamThuNam/@MenhGiaTienTe) as FGiaTriNamThuNam,
			(ctct.fGiaTriBoTri/@MenhGiaTienTe) as FGiaTriBoTri,
			ctct.sGhiChu as SGhiChu,
			cast(0 as bit) as IsHangCha,
			2 as LoaiParent,
			ctct.iID_NguonVonID as IIdNguonVon,
			cast(0 as float) as LuyKeVonNSQPDaBoTri,
			cast(0 as float) as LuyKeVonNSQPDeNghiBoTri,
			cast(0 as float) as TongLuyKe,
			null as DNgayQuyetDinh,
			cast(0 as float) as FHanMucDauTuDP,
			cast(0 as float) as FHanMucDauTuNN,
			cast(0 as float) as FHanMucDauTuOrther,
			cast(0 as float) as FHanMucDauTuQP,
			'' as SSoQuyetDinh
		  into #tmp
		from 
		  f_loai_cong_trinh_get_list_childrent(@lct) dmlct
		left join
		  VDT_KHV_KeHoach5Nam_DeXuat_ChiTiet ctct
		on
		  dmlct.iID_LoaiCongTrinh = ctct.iID_LoaiCongTrinhID
		left join
			VDT_DM_DonViThucHienDuAn dv
		on ctct.iID_DonViQuanLyID = dv.iID_DonVi
		left join 
			NguonNganSach ns
		on ctct.iID_NguonVonID = ns.iID_MaNguonNganSach
		inner join VDT_KHV_KeHoach5Nam_DeXuat vndx
		on ctct.iID_KeHoach5NamID = vndx.Id
		where
		  ctct.iID_KeHoach5NamID in (select * from dbo.splitstring(@Id))
		  and ctct.iID_NguonVonID in (select * from dbo.splitstring(@IdNguonVon))

		select 
			khct.IdLoaiCongTrinh, 
			khct.IdLoaiCongTrinhParent, 
			khct.SMaLoaiCongTrinh, 
			4 as Loai, 
			khct.iID_KeHoach5NamID, 
			dv.sTenDonVi as STenDuAn,
			'' as STenDonVi,
			dv.iID_MaDonVi as IdMaDonViQuanLy,
			'' as SDiaDiem,
			'' as SThoiGianThucHien,
			Sum(khct.FHanMucDauTu) as FHanMucDauTu,
			'' as STenNguonVon,
			Sum(khct.FTongSoNhuCau) as FTongSoNhuCau,
			Sum(khct.FTongSo) as FTongSo,
			Sum(khct.FGiaTriNamThuNhat) as FGiaTriNamThuNhat,
			Sum(khct.FGiaTriNamThuHai) as FGiaTriNamThuHai,
			Sum(khct.FGiaTriNamThuBa) as FGiaTriNamThuBa,
			Sum(khct.FGiaTriNamThuTu) as FGiaTriNamThuTu,
			Sum(khct.FGiaTriNamThuNam) as FGiaTriNamThuNam,
			Sum(khct.FGiaTriBoTri) as FGiaTriBoTri,
			'' as GhiChu,
			cast(1 as bit) as IsHangCha,
			khct.LoaiParent,
			khct.IIdNguonVon as IIdNguonVon,
			cast(0 as float) as LuyKeVonNSQPDaBoTri,
			cast(0 as float) as LuyKeVonNSQPDeNghiBoTri,
			cast(0 as float) as TongLuyKe,
			null as DNgayQuyetDinh,
			cast(0 as float) as FHanMucDauTuDP,
			cast(0 as float) as FHanMucDauTuNN,
			cast(0 as float) as FHanMucDauTuOrther,
			cast(0 as float) as FHanMucDauTuQP,
			'' as SSoQuyetDinh
		  into #tmpDuAn
		  from #tmp khct
		  left join DonVi dv on khct.IdMaDonViQuanLy = dv.iID_MaDonVi and dv.iNamLamViec = @iNamLamViec
		  group by khct.IdLoaiCongTrinh, khct.IdLoaiCongTrinhParent, khct.SMaLoaiCongTrinh, khct.Loai, khct.iID_KeHoach5NamID, dv.sTenDonVi, dv.iID_MaDonVi, khct.IsHangCha,khct.LoaiParent, khct.IIdNguonVon

		select tbl_sum.* from (

			select
				lct.iID_LoaiCongTrinh as IdLoaiCongTrinh,
				lct.iID_Parent as IdLoaiCongTrinhParent,
				lct.sMaLoaiCongTrinh as SMaLoaiCongTrinh,
				3 as Loai,
				null as iID_KeHoach5NamID,
				lct.sTenLoaiCongTrinh as STenDuAn,
				'' as STenDonVi,
				'' as IdMaDonViQuanLy,
				'' as SDiaDiem,
				'' as SThoiGianThucHien,
				cast(0 as float) as FHanMucDauTu,
				'' as STenNguonVon,
				cast(0 as float) as FTongSoNhuCau,
				cast(0 as float) as FTongSo,
				cast(0 as float) as FGiaTriNamThuNhat,
				cast(0 as float) as FGiaTriNamThuHai,
				cast(0 as float) as FGiaTriNamThuBa,
				cast(0 as float) as FGiaTriNamThuTu,
				cast(0 as float) as FGiaTriNamThuNam,
				cast(0 as float) as FGiaTriBoTri,
				'' as SGhiChu,
				cast(1 as bit) as IsHangCha,
				case
					when lct.iID_Parent is null then 0 else 1
				end LoaiParent,
				cast(0 as int) as IIdNguonVon,
				cast(0 as float) as LuyKeVonNSQPDaBoTri,
				cast(0 as float) as LuyKeVonNSQPDeNghiBoTri,
				cast(0 as float) as TongLuyKe,
				null as DNgayQuyetDinh,
				cast(0 as float) as FHanMucDauTuDP,
				cast(0 as float) as FHanMucDauTuNN,
				cast(0 as float) as FHanMucDauTuOrther,
				cast(0 as float) as FHanMucDauTuQP,
				'' as SSoQuyetDinh
			from f_loai_cong_trinh_get_list_childrent(@lct) lct

			union all

			select * from #tmp
			union all
			select * from #tmpDuAn
			) as tbl_sum
			order by tbl_sum.IdLoaiCongTrinh, tbl_sum.iID_KeHoach5NamID, tbl_sum.Loai

		drop table #tmp
		drop table #tmpDuAn
	end
END
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_tt_get_khvthanhtoan]    Script Date: 16/06/2022 5:30:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_vdt_tt_get_khvthanhtoan]
	@iIdDuAnId uniqueidentifier,
	@iIdNguonVonId int,
	@dNgayDeNghi DATE,
	@iNamKeHoach int,
	@iCoQuanThanhToan int,
	@iIdPheDuyet uniqueidentifier
AS
BEGIN
	CREATE TABLE #tmp(
		IIdKeHoachVonId uniqueidentifier,
		SSoQuyetDinh nvarchar(100),
		INamKeHoach int,
		ILoaiKeHoachVon int,
		ILoaiNamKhv int,
		ICoQuanThanhToan int,
		FGiaTriKHV float,
	)

	-- Ke hoach von nam
	BEGIN
		WITH tmp as 
		(
			SELECT tbl.Id, 
				ROW_NUMBER() OVER (PARTITION BY tbl.iID_PhanBoGocID ORDER BY tbl.dNgayQuyetDinh DESC) as rn
			FROM VDT_KHV_PhanBoVon as tbl
			INNER JOIN VDT_KHV_PhanBoVon_ChiTiet as dt on tbl.Id = dt.iID_PhanBoVonID
			WHERE dt.iID_DuAnID = @iIdDuAnId
				AND tbl.iID_NguonVonID = @iIdNguonVonId
				AND iNamKeHoach = @iNamKeHoach
				AND CAST(tbl.dNgayQuyetDinh as DATE) <= CAST(@dNgayDeNghi as DATE)
		)
		INSERT INTO #tmp(IIdKeHoachVonId, SSoQuyetDinh, INamKeHoach, ILoaiKeHoachVon, ILoaiNamKhv, ICoQuanThanhToan, FGiaTriKHV)
		SELECT tmp.Id , tbl.SSoQuyetDinh, tbl.INamKeHoach, 1, (CASE WHEN tbl.iLoaiDuToan = 2 THEN 1 ELSE 2 END), @iCoQuanThanhToan,
		--SELECT tmp.Id , tbl.SSoQuyetDinh, tbl.INamKeHoach, 1, 2, @iCoQuanThanhToan,
				(CASE @iCoQuanThanhToan WHEN 1 THEN SUM(ISNULL(dt.fCapPhatTaiKhoBac, 0)) ELSE SUM(ISNULL(dt.fCapPhatBangLenhChi, 0)) END) as FGiaTri
		FROM tmp as tmp
		INNER JOIN VDT_KHV_PhanBoVon as tbl on tmp.Id = tbl.Id
		INNER JOIN VDT_KHV_PhanBoVon_ChiTiet as dt on tbl.Id = dt.iID_PhanBoVonID
		WHERE tmp.rn = 1 AND dt.iID_DuAnID = @iIdDuAnId
		GROUP BY tmp.Id, tbl.SSoQuyetDinh, tbl.INamKeHoach, tbl.iLoaiDuToan
	END

	-- Ke hoach von ung
	BEGIN
		INSERT INTO #tmp(IIdKeHoachVonId, SSoQuyetDinh, INamKeHoach, ILoaiKeHoachVon, ILoaiNamKhv, ICoQuanThanhToan, FGiaTriKHV)
		SELECT tbl.Id, tbl.sSoQuyetDinh, tbl.iNamKeHoach, 2 , 2, @iCoQuanThanhToan,
			(CASE WHEN @iCoQuanThanhToan = 1 THEN SUM(ISNULL(dt.fCapPhatTaiKhoBac, 0)) ELSE SUM(ISNULL(dt.fCapPhatBangLenhChi, 0)) END)
		FROM VDT_KHV_KeHoachVonUng as tbl
		INNER JOIN VDT_KHV_KeHoachVonUng_ChiTiet as dt on tbl.Id = dt.iID_KeHoachUngID
		WHERE dt.iID_DuAnID = @iIdDuAnId
			AND tbl.iID_NguonVonID = @iIdNguonVonId
			AND CAST(tbl.dNgayQuyetDinh as DATE) <= CAST(@dNgayDeNghi as DATE)
			AND tbl.iNamKeHoach = @iNamKeHoach
		GROUP BY tbl.Id, tbl.sSoQuyetDinh, tbl.iNamKeHoach
	END

	-- Ke hoach nam truoc chuyen sang
	BEGIN
		INSERT INTO #tmp(IIdKeHoachVonId, SSoQuyetDinh, INamKeHoach, ILoaiKeHoachVon, ILoaiNamKhv, ICoQuanThanhToan, FGiaTriKHV)
		SELECT tbl.Id, tbl.sSoDeNghi, tbl.iNamKeHoach, (CASE WHEN iLoaiThanhToan = 1 THEN 3 ELSE 4 END), 1, tbl.iCoQuanThanhToan,
			(CASE WHEN iLoaiThanhToan = 1 THEN SUM(ISNULL(dt.fGiaTriNamTruocChuyenNamSau, 0) + ISNULL(dt.fGiaTriNamNayChuyenNamSau, 0)) 
				ELSE SUM(ISNULL(dt.FGiaTriUngChuyenNamSau, 0) - (ISNULL(dt.fLKThanhToanDenTrcNamQuyetToan_KHUng, 0) - ISNULL(dt.FGiaTriThuHoiTheoGiaiNganThucTe, 0) + ISNULL(dt.fThanhToan_KHUngNamTrcChuyenSang, 0) + ISNULL(dt.fThanhToan_KHUngNamNay, 0))) END)
		FROM VDT_QT_BCQuyetToanNienDo as tbl
		INNER JOIN VDT_QT_BCQuyetToanNienDo_ChiTiet_01 as dt on tbl.Id = dt.iID_BCQuyetToanNienDo
		WHERE dt.iID_DuAnID = @iIdDuAnId
			AND tbl.iID_NguonVonID = @iIdNguonVonId
			AND CAST(tbl.dNgayDeNghi as DATE) < CAST(@dNgayDeNghi AS DATE)
			AND iNamKeHoach < @iNamKeHoach
			AND tbl.iCoQuanThanhToan = @iCoQuanThanhToan
		GROUP BY tbl.Id, tbl.sSoDeNghi, tbl.iNamKeHoach, iLoaiThanhToan, tbl.iCoQuanThanhToan
	END

	-- So tien da thanh toan
	SELECT tmp.IIdKeHoachVonId, tmp.ILoaiKeHoachVon, 
		SUM(ISNULL(dt.fGiaTriThanhToanNN, 0) + ISNULL(dt.fGiaTriThanhToanTN, 0)) as fThanhToan,
		SUM(ISNULL(fGiaTriThuHoiNamTruocTN, 0) + ISNULL(fGiaTriThuHoiNamTruocNN, 0) + ISNULL(fGiaTriThuHoiNamNayTN, 0) + ISNULL(fGiaTriThuHoiNamNayNN, 0)) AS fThuHoi
		INTO #tmpThanhToan
	FROM VDT_TT_DeNghiThanhToan as tbl
	INNER JOIN VDT_TT_PheDuyetThanhToan_ChiTiet as dt on tbl.Id = dt.iID_DeNghiThanhToanID
	INNER JOIN #tmp as tmp on dt.iID_KeHoachVonID = tmp.IIdKeHoachVonId
							 AND tmp.ILoaiKeHoachVon = dt.iLoaiKeHoachVon
	WHERE tbl.iID_DuAnId = @iIdDuAnId
		AND tbl.iCoQuanThanhToan = @iCoQuanThanhToan
		AND CAST(dNgayDeNghi as DATE) <= CAST(@dNgayDeNghi as DATE)
		AND (@iIdPheDuyet IS NULL OR tbl.Id <> @iIdPheDuyet)
	GROUP BY tmp.IIdKeHoachVonId, tmp.ILoaiKeHoachVon

	SELECT tbl.IIdKeHoachVonId, tbl.SSoQuyetDinh, tbl.ILoaiKeHoachVon, tbl.ILoaiNamKhv, tbl.INamKeHoach, tbl.ICoQuanThanhToan, tbl.FGiaTriKHV, SUM(ISNULL(dt.fThanhToan, 0)) as FGiaTriKHVDaThanhToan, SUM(ISNULL(dt.fThuHoi, 0)) as FGiaTriKHVDaThuHoi INTO #tbl
	FROM #tmp as tbl
	LEFT JOIN #tmpThanhToan as dt on tbl.IIdKeHoachVonId = dt.IIdKeHoachVonId AND tbl.ILoaiKeHoachVon = dt.ILoaiKeHoachVon
	GROUP BY tbl.IIdKeHoachVonId, tbl.SSoQuyetDinh, tbl.ILoaiKeHoachVon, tbl.ILoaiNamKhv, tbl.INamKeHoach, tbl.ICoQuanThanhToan, tbl.FGiaTriKHV

	SELECT iID_KeHoachVonID, iLoai, iID_DeNghiThanhToanID INTO #khv
	FROM VDT_TT_DeNghiThanhToan_KHV
	WHERE iID_DeNghiThanhToanID = @iIdPheDuyet

	SELECT tbl.*, CAST(0 as float) as FGiaTriThanhToanTN, CAST(0 as float) FGiaTriThanhToanNN, CAST(0 as float) FGiaTriThuHoiTrongNuoc, CAST(0 as float) FGiaTriThuHoiNgoaiNuoc, 0 as ILoaiNamTamUng
	--FROM #tbl as tbl
	--left JOIN #khv as dt on
	FROM #khv as dt
	left JOIN #tbl as tbl on 
	tbl.IIdKeHoachVonId = dt.iID_KeHoachVonID 
	--bỏ điều kiện do luôn set ILoaiKeHoachVon = 1, không có trường hợp ILoaiKeHoachVon = 3
	--AND tbl.ILoaiKeHoachVon = dt.iLoai
	WHERE tbl.FGiaTriKHV > (tbl.FGiaTriKHVDaThanhToan - tbl.FGiaTriKHVDaThuHoi) 

	DROP TABLE #tmp
	DROP TABLE #tmpThanhToan
	DROP TABLE #tbl
	DROP TABLE #khv
END
GO
