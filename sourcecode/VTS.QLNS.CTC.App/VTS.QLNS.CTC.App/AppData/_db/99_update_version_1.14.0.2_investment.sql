/****** Object:  StoredProcedure [dbo].[sp_vdt_get_duan_kehoachvonung_detail]    Script Date: 22/02/2024 10:26:32 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_duan_kehoachvonung_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_duan_kehoachvonung_detail]
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_tonghopnguonnsdautu_tang_new]    Script Date: 22/02/2024 10:26:32 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_insert_tonghopnguonnsdautu_tang_new]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_insert_tonghopnguonnsdautu_tang_new]
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_tonghopnguonnsdautu_tang_new]    Script Date: 22/02/2024 10:26:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_insert_tonghopnguonnsdautu_tang_new]
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
		VALUES('KHVN'), ('101'), ('102'),('111'),('112'),('000'),('211a'),('212a')
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
												iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG, iID_LoaiCongTrinh)
			--OUTPUT inserted.Id, inserted.sMaDich, inserted.iID_DuAnID, ISNULL(inserted.fGiaTri, 0) INTO #tmp(iId, sMaNguon, iIdDuAnId, fDaThanhToan)
			SELECT tbl.iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, tbl.sMaDich, tbl.sMaNguon, tbl.sMaNguonCha, fGiaTri, 1, 2, '300',
				iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG, iID_LoaiCongTrinh
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
												iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG, iID_LoaiCongTrinh)
			--OUTPUT inserted.Id, inserted.sMaDich, inserted.iID_DuAnID, ISNULL(inserted.fGiaTri, 0) INTO #tmp(iId, sMaNguon, iIdDuAnId, fDaThanhToan)
			SELECT tbl.iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, tbl.sMaDich, tbl.sMaNguon, tbl.sMaNguonCha, fGiaTri, 1, 2, '300',
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG, iID_LoaiCongTrinh
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
		IF(@iTypeExecute = 4)
		BEGIN

		-- Tao data insert vao but toan
			--  Du toan dau nam, nam nay
			SELECT dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID,
					tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon,
					(CASE dt.sMaNguon WHEN '101' THEN '000' 
									  WHEN '102' THEN '000'
									  WHEN '000' THEN '121a'
									  WHEN '555' THEN '122a'
									  END) as sMaDich, 
					SUM(ISNULL(dt.fGiaTri, 0)) as fGiaTri, 0 as iStatus, 
					(CASE WHEN sMaNguon in ('101', '102','000','555') THEN '200' ELSE '100' END) as sMaTienTrinh,
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, dt.iID_LoaiCongTrinh
					INTO #tmpDataInsertDC

			FROM VDT_KHV_PhanBoVon as tbl
			INNER JOIN (select Id,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, dt.fGiaTri, iID_LoaiCongTrinh,
						(CASE colName WHEN 'fCapPhatTaiKhoBacDC' THEN '101' 
									  WHEN 'fCapPhatBangLenhChiDC' THEN '102'
									  WHEN 'fGiaTriThuHoiNamTruocKhoBacDC' THEN '000'
									  WHEN 'fGiaTriThuHoiNamTruocLenhChiDC' THEN '555'
									  END) as sMaNguon
						from 
						(select Id,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, iID_LoaiCongTrinh, fCapPhatTaiKhoBacDC,fCapPhatBangLenhChiDC,fGiaTriThuHoiNamTruocKhoBacDC,fGiaTriThuHoiNamTruocLenhChiDC from VDT_KHV_PhanBoVon_ChiTiet) as tbl
						UNPIVOT
						(fGiaTri FOR colName IN (fCapPhatTaiKhoBacDC,fCapPhatBangLenhChiDC,fGiaTriThuHoiNamTruocKhoBacDC,fGiaTriThuHoiNamTruocLenhChiDC)) as dt) as dt on dt.iID_PhanBoVonID = tbl.Id
			LEFT JOIN NS_MucLucNganSach as ml on (dt.iID_MucID = ml.iID OR dt.iID_TieuMucID = ml.iID OR dt.iID_TietMucID = ml.iID OR dt.iID_NganhID = ml.iID)
			WHERE tbl.Id = @uIdQuyetDinh AND tbl.iLoaiDuToan <> 2
			GROUP BY dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID, tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon,
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, dt.iID_LoaiCongTrinh

			--DuToan nam truoc chuyen sang
			SELECT dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID,
					tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon,
					(CASE dt.sMaNguon WHEN '101' THEN '000' 
									  WHEN '102' THEN '000'
									  WHEN '211a' THEN '000'
									  WHEN '212a' THEN '000'
									  END) as sMaDich, 
					SUM(ISNULL(dt.fGiaTri, 0)) as fGiaTri, 0 as iStatus, 
					(CASE WHEN sMaNguon in ('101', '102','000') THEN '200' ELSE '100' END) as sMaTienTrinh,
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, dt.iID_LoaiCongTrinh
					INTO #tmpDataInsert2DC
			FROM VDT_KHV_PhanBoVon as tbl
			INNER JOIN (select Id, iID_LoaiCongTrinh,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, dt.fGiaTri, 
						(CASE colName WHEN 'fCapPhatTaiKhoBacDC' THEN '111' 
									  WHEN 'fCapPhatBangLenhChiDC' THEN '112'
									  WHEN 'fGiaTriThuHoiNamTruocKhoBacDC' THEN '211a'
									  WHEN 'fGiaTriThuHoiNamTruocLenhChiDC' THEN '212a'
									  END) as sMaNguon
						from 
						(select Id, iID_LoaiCongTrinh,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID , fCapPhatTaiKhoBacDC,fCapPhatBangLenhChiDC,fGiaTriThuHoiNamTruocKhoBacDC,fGiaTriThuHoiNamTruocLenhChiDC from VDT_KHV_PhanBoVon_ChiTiet) as tbl
						UNPIVOT
						(fGiaTri FOR colName IN (fCapPhatTaiKhoBacDC,fCapPhatBangLenhChiDC,fGiaTriThuHoiNamTruocKhoBacDC,fGiaTriThuHoiNamTruocLenhChiDC)) as dt) as dt on dt.iID_PhanBoVonID = tbl.Id
			LEFT JOIN NS_MucLucNganSach as ml on (dt.iID_MucID = ml.iID OR dt.iID_TieuMucID = ml.iID OR dt.iID_TietMucID = ml.iID OR dt.iID_NganhID = ml.iID)
			WHERE tbl.Id = @uIdQuyetDinh AND tbl.iLoaiDuToan = 2
			GROUP BY dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID, tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon,
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, dt.iID_LoaiCongTrinh

			-- doi lai sMaNguon cua thuhoitruoclenhchi dung
			update #tmpDataInsertDC
			set sMaNguon = '000'
			where sMaNguon = '555'
			--LoaiDuToan = 2
			update #tmpDataInsert2DC
			set sMaNguon = '000'
			where sMaNguon = '555'

			-- insert but toan moi vao 
			INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, fGiaTri, iStatus, sMaTienTrinh,
												iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG, iID_LoaiCongTrinh)
			--Insert data vua tao vao but toan--
			SELECT * FROM #tmpDataInsertDC

			INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, fGiaTri, iStatus, sMaTienTrinh,
												iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG, iID_LoaiCongTrinh)
			--Insert data vua tao vao but toan--
			SELECT * FROM #tmpDataInsert2DC
			DROP TABLE #tmpDataInsertDC;
			DROP TABLE #tmpDataInsert2DC;
		END
		ELSE
		BEGIN

			-- Tao data insert vao but toan
			--  Du toan dau nam, nam nay
			SELECT dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID,
					tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon,
					(CASE dt.sMaNguon WHEN '101' THEN '000' 
									  WHEN '102' THEN '000'
									  WHEN '000' THEN '121a'
									  WHEN '555' THEN '122a'
									  END) as sMaDich, 
					SUM(ISNULL(dt.fGiaTri, 0)) as fGiaTri, 0 as iStatus, 
					(CASE WHEN sMaNguon in ('101', '102','000','555') THEN '200' ELSE '100' END) as sMaTienTrinh,
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, dt.iID_LoaiCongTrinh
					INTO #tmpDataInsert

			FROM VDT_KHV_PhanBoVon as tbl
			INNER JOIN (select Id,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, dt.fGiaTri, iID_LoaiCongTrinh,
						(CASE colName WHEN 'fCapPhatTaiKhoBac' THEN '101' 
									  WHEN 'fCapPhatBangLenhChi' THEN '102'
									  WHEN 'fGiaTriThuHoiNamTruocKhoBac' THEN '000'
									  WHEN 'fGiaTriThuHoiNamTruocLenhChi' THEN '555'
									  END) as sMaNguon
						from 
						(select Id,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, iID_LoaiCongTrinh, fCapPhatTaiKhoBac,fCapPhatBangLenhChi,fGiaTriThuHoiNamTruocKhoBac,fGiaTriThuHoiNamTruocLenhChi from VDT_KHV_PhanBoVon_ChiTiet) as tbl
						UNPIVOT
						(fGiaTri FOR colName IN (fCapPhatTaiKhoBac,fCapPhatBangLenhChi,fGiaTriThuHoiNamTruocKhoBac,fGiaTriThuHoiNamTruocLenhChi)) as dt) as dt on dt.iID_PhanBoVonID = tbl.Id
			LEFT JOIN NS_MucLucNganSach as ml on (dt.iID_MucID = ml.iID OR dt.iID_TieuMucID = ml.iID OR dt.iID_TietMucID = ml.iID OR dt.iID_NganhID = ml.iID)
			WHERE tbl.Id = @uIdQuyetDinh AND tbl.iLoaiDuToan <> 2
			GROUP BY dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID, tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon,
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, dt.iID_LoaiCongTrinh

			--DuToan nam truoc chuyen sang
			SELECT dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID,
					tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon,
					(CASE dt.sMaNguon WHEN '101' THEN '000' 
									  WHEN '102' THEN '000'
									  WHEN '211a' THEN '000'
									  WHEN '212a' THEN '000'
									  WHEN '111' THEN '000'
									  WHEN '112' THEN '000'
									  END) as sMaDich, 
					SUM(ISNULL(dt.fGiaTri, 0)) as fGiaTri, 0 as iStatus, 
					(CASE WHEN sMaNguon in ('101', '102','000') THEN '200' ELSE '100' END) as sMaTienTrinh,
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, dt.iID_LoaiCongTrinh
					INTO #tmpDataInsert2
			FROM VDT_KHV_PhanBoVon as tbl
			INNER JOIN (select Id, iID_LoaiCongTrinh,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, dt.fGiaTri, 
						(CASE colName WHEN 'fCapPhatTaiKhoBac' THEN '111' 
									  WHEN 'fCapPhatBangLenhChi' THEN '112'
									  WHEN 'fGiaTriThuHoiNamTruocKhoBac' THEN '211a'
									  WHEN 'fGiaTriThuHoiNamTruocLenhChi' THEN '212a'
									  END) as sMaNguon
						from 
						(select Id, iID_LoaiCongTrinh,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID , fCapPhatTaiKhoBac,fCapPhatBangLenhChi,fGiaTriThuHoiNamTruocKhoBac,fGiaTriThuHoiNamTruocLenhChi from VDT_KHV_PhanBoVon_ChiTiet) as tbl
						UNPIVOT
						(fGiaTri FOR colName IN (fCapPhatTaiKhoBac,fCapPhatBangLenhChi,fGiaTriThuHoiNamTruocKhoBac,fGiaTriThuHoiNamTruocLenhChi)) as dt) as dt on dt.iID_PhanBoVonID = tbl.Id
			LEFT JOIN NS_MucLucNganSach as ml on (dt.iID_MucID = ml.iID OR dt.iID_TieuMucID = ml.iID OR dt.iID_TietMucID = ml.iID OR dt.iID_NganhID = ml.iID)
			WHERE tbl.Id = @uIdQuyetDinh AND tbl.iLoaiDuToan = 2
			GROUP BY dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID, tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon,
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, dt.iID_LoaiCongTrinh

			-- doi lai sMaNguon cua thuhoitruoclenhchi dung
			update #tmpDataInsert
			set sMaNguon = '000'
			where sMaNguon = '555'
			--LoaiDuToan = 2
			update #tmpDataInsert2
			set sMaNguon = '000'
			where sMaNguon = '555'
			-- insert but toan moi vao 
			INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, fGiaTri, iStatus, sMaTienTrinh,
												iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG, iID_LoaiCongTrinh)
			--Data Insert
			SELECT * FROM #tmpDataInsert

			INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, fGiaTri, iStatus, sMaTienTrinh,
												iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG, iID_LoaiCongTrinh)
			Select * from #tmpDataInsert2
			

			DROP TABLE #tmpDataInsert;
			DROP TABLE #tmpDataInsert2;

		END
	END
	ELSE IF(@sLoai = 'KHVU')
	BEGIN
		-- insert but toan moi vao 
		INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, fGiaTri, iStatus, sMaTienTrinh,
											iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG, iID_LoaiCongTrinh)
		SELECT dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID,
				tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon , '000', SUM(ISNULL(dt.fGiaTri, 0)), 0, '200',
				iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, dahm.IdLoaiCongTrinh
		FROM VDT_KHV_KeHoachVonUng as tbl
		INNER JOIN (select Id,dt.iID_KeHoachUngID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ID_DuAn_HangMuc, dt.fGiaTri, 
					(CASE colName WHEN 'fCapPhatTaiKhoBac' THEN '121a' WHEN 'fCapPhatBangLenhChi' THEN '122a' END) as sMaNguon
					from 
					(select Id,iID_KeHoachUngID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, fCapPhatTaiKhoBac,fCapPhatBangLenhChi, ID_DuAn_HangMuc from VDT_KHV_KeHoachVonUng_ChiTiet) as tbl
					UNPIVOT
					(fGiaTri FOR colName IN (fCapPhatTaiKhoBac,fCapPhatBangLenhChi)) as dt) as dt on dt.iID_KeHoachUngID = tbl.Id
		LEFT JOIN NS_MucLucNganSach as ml on (dt.iID_MucID = ml.iID OR dt.iID_TieuMucID = ml.iID OR dt.iID_TietMucID = ml.iID OR dt.iID_NganhID = ml.iID)
		LEFT JOIN VDT_DA_DuAn_HangMuc as dahm on dahm.iID_DuAn_HangMucID = dt.ID_DuAn_HangMuc		
		WHERE tbl.Id = @uIdQuyetDinh
		GROUP BY dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID, tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon,
			iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, dahm.IdLoaiCongTrinh
	END
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_duan_kehoachvonung_detail]    Script Date: 22/02/2024 10:26:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_get_duan_kehoachvonung_detail]
@iIdKeHoachVonUngDeXuat uniqueidentifier
AS
BEGIN
	SELECT dt.iID_DuAnID, SUM(ISNULL(dt.fGiaTriDeNghi, 0)) as fGiaTriDeNghi, dt.ID_DuAn_HangMuc INTO #tmpDX
	FROM VDT_KHV_KeHoachVonUng_DX as tbl 
	INNER JOIN VDT_KHV_KeHoachVonUng_DX_ChiTiet as dt on tbl.Id = dt.iID_KeHoachUngID
	WHERE tbl.Id = @iIdKeHoachVonUngDeXuat
	GROUP BY dt.iID_DuAnID, dt.ID_DuAn_HangMuc;

	SELECT da.iID_DuAnID, SUM(ISNULL(tbl.fTongMucDauTuPheDuyet,0)) as fTongMucDauTuPheDuyet, hm.iID_DuAn_HangMucID AS ID_DuAn_HangMuc INTO #tmp
	FROM #tmpDX as tmp
	INNER JOIN VDT_DA_QDDauTu as tbl on tmp.iID_DuAnID = tbl.iID_DuAnID
	INNER JOIN VDT_DA_DuAn as da on tbl.iID_DuAnID = da.iID_DuAnID 
	LEFT JOIN VDT_DA_DuAn_HangMuc hm on tmp.ID_DuAn_HangMuc = hm.iID_DuAn_HangMucID OR da.iID_DuAnID = hm.iID_DuAnID
	WHERE tbl.bActive = 1
	GROUP BY da.iID_DuAnID, hm.iID_DuAn_HangMucID;

	SELECT da.iID_LoaiCongTrinhID,tbl.*, da.sTenDuAn, da.sMaKetNoi, da.sTrangThaiDuAn as sTrangThaiDuAnDangKy,da.sMaDuAn,
			da.iID_DonViTienTeID, da.iID_TienTeID, da.fTiGia, da.fTiGiaDonVi, null as sGhiChu, 
			NULL as iID_MucID, NULL as iID_TieuMucID, NULL as iID_TietMucID, NULL as iID_NganhID, 
			lct.LNS as sLNS, lct.L as sL, lct.K as sK, lct.M as sM, lct.TM as sTM, lct.TTM as sTTM, lct.NG as sNG,
			NULL as fCapPhatBangLenhChi, NULL as fCapPhatTaiKhoBac, ISNULL(fGiaTriDeNghi, 0) as fGiaTriDeNghi,
			hm.sMaHangMuc, hm.sTenHangMuc as sTenHangMuc
	FROM #tmp as tbl 
	INNER JOIN #tmpDX as dx on tbl.iID_DuAnID = dx.iID_DuAnID
	INNER JOIN VDT_DA_DuAn as da on tbl.iID_DuAnID = da.iID_DuAnID
	LEFT JOIN VDT_DA_DuAn_HangMuc hm on tbl.ID_DuAn_HangMuc = hm.iID_DuAn_HangMucID
	LEFT JOIN VDT_DM_LoaiCongTrinh lct on hm.IdLoaiCongTrinh = lct.iID_LoaiCongTrinh

	DROP TABLE #tmp
	DROP TABLE #tmpDX
END
;
;
;
GO
