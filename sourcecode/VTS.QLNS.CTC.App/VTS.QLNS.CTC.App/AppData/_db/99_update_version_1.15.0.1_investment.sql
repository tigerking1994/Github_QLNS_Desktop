/****** Object:  StoredProcedure [dbo].[sp_vdt_tt_get_khvthanhtoan]    Script Date: 10/24/2024 4:31:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_tt_get_khvthanhtoan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_tt_get_khvthanhtoan]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_vdt_theochitieu_capphatduan]    Script Date: 10/24/2024 4:31:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_vdt_theochitieu_capphatduan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_vdt_theochitieu_capphatduan]
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_tonghopnguonnsdautu_tang_new]    Script Date: 10/24/2024 4:31:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_insert_tonghopnguonnsdautu_tang_new]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_insert_tonghopnguonnsdautu_tang_new]
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_tonghopnguonnsdautu_tang_new]    Script Date: 10/24/2024 4:31:31 PM ******/
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
		VALUES('KHVN'), ('101'), ('102'),('103'),('111'),('112'), ('113'),('000'),('211a'),('212a'),('213a')
	END
	ELSE IF(@sLoai = 'KHVU')
	BEGIN
		INSERT INTO #lstMaNguon(sMaNguon)
		VALUES('KHVU'), ('121a'), ('122a') , ('123a')
	END
	ELSE IF(@sLoai = 'QUYET_TOAN')
	BEGIN
		INSERT INTO #lstMaNguon(sMaNguon)
		VALUES('QUYET_TOAN'), ('131'), ('132'),('133'), ('211c'), ('212c'),('213c'), ('301'), ('302'),('303'), ('321a'), ('322a'),('323a')
			, ('000'), ('321b'), ('322b'),('323b')	
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
									  WHEN '103' THEN '000'
									  WHEN '000' THEN '121a'
									  WHEN '555' THEN '122a'
									  END) as sMaDich, 
					SUM(ISNULL(dt.fGiaTri, 0)) as fGiaTri, 0 as iStatus, 
					(CASE WHEN sMaNguon in ('101', '102','103','000','555') THEN '200' ELSE '100' END) as sMaTienTrinh,
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, dt.iID_LoaiCongTrinh
					INTO #tmpDataInsertDC

			FROM VDT_KHV_PhanBoVon as tbl
			INNER JOIN (select Id,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, dt.fGiaTri, iID_LoaiCongTrinh,
						(CASE colName WHEN 'fCapPhatTaiKhoBacDC' THEN '101' 
									  WHEN 'fCapPhatBangLenhChiDC' THEN '102'
									  WHEN 'fTonKhoanTaiDonViDC' THEN '103'
									  WHEN 'fGiaTriThuHoiNamTruocKhoBacDC' THEN '000'
									  WHEN 'fGiaTriThuHoiNamTruocLenhChiDC' THEN '555'
									  END) as sMaNguon
						from 
						(select Id,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, iID_LoaiCongTrinh, fCapPhatTaiKhoBacDC,fCapPhatBangLenhChiDC,fTonKhoanTaiDonViDC,fGiaTriThuHoiNamTruocKhoBacDC,fGiaTriThuHoiNamTruocLenhChiDC from VDT_KHV_PhanBoVon_ChiTiet) as tbl
						UNPIVOT
						(fGiaTri FOR colName IN (fCapPhatTaiKhoBacDC,fCapPhatBangLenhChiDC,fTonKhoanTaiDonViDC,fGiaTriThuHoiNamTruocKhoBacDC,fGiaTriThuHoiNamTruocLenhChiDC)) as dt) as dt on dt.iID_PhanBoVonID = tbl.Id
			LEFT JOIN NS_MucLucNganSach as ml on (dt.iID_MucID = ml.iID OR dt.iID_TieuMucID = ml.iID OR dt.iID_TietMucID = ml.iID OR dt.iID_NganhID = ml.iID)
			WHERE tbl.Id = @uIdQuyetDinh AND tbl.iLoaiDuToan <> 2
			GROUP BY dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID, tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon,
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, dt.iID_LoaiCongTrinh

			--DuToan nam truoc chuyen sang
			SELECT dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID,
					tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon,
					(CASE dt.sMaNguon WHEN '101' THEN '000' 
									  WHEN '102' THEN '000'
									  WHEN '103' THEN '000'
									  WHEN '211a' THEN '000'
									  WHEN '212a' THEN '000'
									  WHEN '213a' THEN '000'
									  END) as sMaDich, 
					SUM(ISNULL(dt.fGiaTri, 0)) as fGiaTri, 0 as iStatus, 
					(CASE WHEN sMaNguon in ('101', '102','103','000') THEN '200' ELSE '100' END) as sMaTienTrinh,
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, dt.iID_LoaiCongTrinh
					INTO #tmpDataInsert2DC
			FROM VDT_KHV_PhanBoVon as tbl
			INNER JOIN (select Id, iID_LoaiCongTrinh,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, dt.fGiaTri, 
						(CASE colName WHEN 'fCapPhatTaiKhoBacDC' THEN '111' 
									  WHEN 'fCapPhatBangLenhChiDC' THEN '112'
									  WHEN 'fTonKhoanTaiDonViDC' THEN '113'
									  WHEN 'fGiaTriThuHoiNamTruocKhoBacDC' THEN '211a'
									  WHEN 'fGiaTriThuHoiNamTruocLenhChiDC' THEN '212a'
									  END) as sMaNguon
						from 
						(select Id, iID_LoaiCongTrinh,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID , fCapPhatTaiKhoBacDC,fCapPhatBangLenhChiDC,fTonKhoanTaiDonViDC,fGiaTriThuHoiNamTruocKhoBacDC,fGiaTriThuHoiNamTruocLenhChiDC from VDT_KHV_PhanBoVon_ChiTiet) as tbl
						UNPIVOT
						(fGiaTri FOR colName IN (fCapPhatTaiKhoBacDC,fCapPhatBangLenhChiDC,fTonKhoanTaiDonViDC,fGiaTriThuHoiNamTruocKhoBacDC,fGiaTriThuHoiNamTruocLenhChiDC)) as dt) as dt on dt.iID_PhanBoVonID = tbl.Id
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
									  WHEN '103' THEN '000'
									  WHEN '000' THEN '121a'
									  WHEN '000' THEN '123a'
									  WHEN '555' THEN '122a'
									  END) as sMaDich, 
					SUM(ISNULL(dt.fGiaTri, 0)) as fGiaTri, 0 as iStatus, 
					(CASE WHEN sMaNguon in ('101', '102','103','000','555') THEN '200' ELSE '100' END) as sMaTienTrinh,
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, dt.iID_LoaiCongTrinh
					INTO #tmpDataInsert

			FROM VDT_KHV_PhanBoVon as tbl
			INNER JOIN (select Id,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, dt.fGiaTri, iID_LoaiCongTrinh,
						(CASE colName WHEN 'fCapPhatTaiKhoBac' THEN '101' 
									  WHEN 'fCapPhatBangLenhChi' THEN '102'
									  WHEN 'fTonKhoanTaiDonVi' THEN '103'
									  WHEN 'fGiaTriThuHoiNamTruocKhoBac' THEN '000'
									  WHEN 'fGiaTriThuHoiNamTruocLenhChi' THEN '555'
									  END) as sMaNguon
						from 
						(select pbvct.Id
							,pbvct.iID_PhanBoVonID
							,pbvct.iID_DuAnID
							, pbvct.iID_MucID
							, pbvct.iID_TieuMucID
							, pbvct.iID_TietMucID
							, pbvct.iID_NganhID
							, pbvct.iID_LoaiCongTrinh
							,case when pbv.iID_ParentId is null then  pbvct.fCapPhatTaiKhoBac else pbvct.fCapPhatTaiKhoBacDC end fCapPhatTaiKhoBac
							,case when pbv.iID_ParentId is null then  pbvct.fCapPhatBangLenhChi else pbvct.fCapPhatBangLenhChiDC end fCapPhatBangLenhChi
							,case when pbv.iID_ParentId is null then  pbvct.fTonKhoanTaiDonVi else pbvct.fTonKhoanTaiDonViDC end fTonKhoanTaiDonVi
							,case when pbv.iID_ParentId is null then  pbvct.fGiaTriThuHoiNamTruocKhoBac else pbvct.fGiaTriThuHoiNamTruocKhoBacDC end fGiaTriThuHoiNamTruocKhoBac
							,case when pbv.iID_ParentId is null then  pbvct.fGiaTriThuHoiNamTruocLenhChi else pbvct.fGiaTriThuHoiNamTruocLenhChiDC end fGiaTriThuHoiNamTruocLenhChi  
							from VDT_KHV_PhanBoVon pbv inner join VDT_KHV_PhanBoVon_ChiTiet pbvct on pbv.Id = pbvct.iID_PhanBoVonID) as tbl
						UNPIVOT
						(fGiaTri FOR colName IN (fCapPhatTaiKhoBac,fCapPhatBangLenhChi,fTonKhoanTaiDonVi,fGiaTriThuHoiNamTruocKhoBac,fGiaTriThuHoiNamTruocLenhChi)) as dt) as dt on dt.iID_PhanBoVonID = tbl.Id
			LEFT JOIN NS_MucLucNganSach as ml on (dt.iID_MucID = ml.iID OR dt.iID_TieuMucID = ml.iID OR dt.iID_TietMucID = ml.iID OR dt.iID_NganhID = ml.iID)
			WHERE tbl.Id = @uIdQuyetDinh AND tbl.iLoaiDuToan <> 2
			GROUP BY dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID, tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon,
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, dt.iID_LoaiCongTrinh

			--DuToan nam truoc chuyen sang
			SELECT dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID,
					tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon,
					(CASE dt.sMaNguon WHEN '101' THEN '000' 
									  WHEN '102' THEN '000'
									  WHEN '103' THEN '000'
									  WHEN '211a' THEN '000'
									  WHEN '212a' THEN '000'
									  WHEN '213a' THEN '000'
									  WHEN '111' THEN '000'
									  WHEN '112' THEN '000'
									  WHEN '113' THEN '000'
									  END) as sMaDich, 
					SUM(ISNULL(dt.fGiaTri, 0)) as fGiaTri, 0 as iStatus, 
					(CASE WHEN sMaNguon in ('101', '102','103','000') THEN '200' ELSE '100' END) as sMaTienTrinh,
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, dt.iID_LoaiCongTrinh
					INTO #tmpDataInsert2
			FROM VDT_KHV_PhanBoVon as tbl
			INNER JOIN (select Id, iID_LoaiCongTrinh,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, dt.fGiaTri, 
						(CASE colName WHEN 'fCapPhatTaiKhoBac' THEN '111' 
									  WHEN 'fCapPhatBangLenhChi' THEN '112'
									  WHEN 'fTonKhoanTaiDonVi' THEN '113'
									  WHEN 'fGiaTriThuHoiNamTruocKhoBac' THEN '211a'
									  WHEN 'fGiaTriThuHoiNamTruocLenhChi' THEN '212a'
									  END) as sMaNguon
						from 
						(select pbvct.Id
							,pbvct.iID_PhanBoVonID
							,pbvct.iID_DuAnID
							,pbvct.iID_MucID
							, pbvct.iID_TieuMucID
							, pbvct.iID_TietMucID
							, pbvct.iID_NganhID
							, pbvct.iID_LoaiCongTrinh
							,case when pbv.iID_ParentId is null then  pbvct.fCapPhatTaiKhoBac else pbvct.fCapPhatTaiKhoBacDC end fCapPhatTaiKhoBac
							,case when pbv.iID_ParentId is null then  pbvct.fCapPhatBangLenhChi else pbvct.fCapPhatBangLenhChiDC end fCapPhatBangLenhChi
							,case when pbv.iID_ParentId is null then  pbvct.fTonKhoanTaiDonVi else pbvct.fTonKhoanTaiDonViDC end fTonKhoanTaiDonVi
							,case when pbv.iID_ParentId is null then  pbvct.fGiaTriThuHoiNamTruocKhoBac else pbvct.fGiaTriThuHoiNamTruocKhoBacDC end fGiaTriThuHoiNamTruocKhoBac
							,case when pbv.iID_ParentId is null then  pbvct.fGiaTriThuHoiNamTruocLenhChi else pbvct.fGiaTriThuHoiNamTruocLenhChiDC end fGiaTriThuHoiNamTruocLenhChi  
							from VDT_KHV_PhanBoVon pbv inner join VDT_KHV_PhanBoVon_ChiTiet pbvct on pbv.Id = pbvct.iID_PhanBoVonID) as tbl
						UNPIVOT
						(fGiaTri FOR colName IN (fCapPhatTaiKhoBac,fCapPhatBangLenhChi,fTonKhoanTaiDonVi,fGiaTriThuHoiNamTruocKhoBac,fGiaTriThuHoiNamTruocLenhChi)) as dt) as dt on dt.iID_PhanBoVonID = tbl.Id
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
					(CASE colName WHEN 'fCapPhatTaiKhoBac' THEN '121a' 
								  WHEN 'fCapPhatBangLenhChi' THEN '122a' 
								  WHEN 'fTonKhoanTaiDonVi' THEN '123a'
					END) as sMaNguon
					from 
					(select Id,iID_KeHoachUngID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, fCapPhatTaiKhoBac,fCapPhatBangLenhChi,fTonKhoanTaiDonVi, ID_DuAn_HangMuc from VDT_KHV_KeHoachVonUng_ChiTiet) as tbl
					UNPIVOT
					(fGiaTri FOR colName IN (fCapPhatTaiKhoBac,fCapPhatBangLenhChi,fTonKhoanTaiDonVi)) as dt) as dt on dt.iID_KeHoachUngID = tbl.Id
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
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_vdt_theochitieu_capphatduan]    Script Date: 10/24/2024 4:31:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_vdt_theochitieu_capphatduan]
@donViID nvarchar(200), 
@namThucHien int,
@nguonVonId int

AS
Begin
create table #tmpDA(IdDuAn uniqueidentifier);
select iID_DuAnID into #tmpIdDA
from
( select khvpdct.iID_DuAnID
	from VDT_KHV_PhanBoVon_DonVi_PheDuyet khvpd
			inner join VDT_KHV_PhanBoVon_DonVi_ChiTiet_PheDuyet khvpdct 
				on khvpdct.iID_PhanBoVon_DonVi_PheDuyet_ID = khvpd.iID_PhanBoGocID
			inner join VDT_DA_DuAn da on khvpdct.iID_DuAnID = da.iID_DuAnID
	where khvpd.iNamKeHoach = @namThucHien
	and khvpd.iID_NguonVonID = @nguonVonId
	and da.iID_MaDonViThucHienDuAnID = @donViID
) as da

select 0 as CT
,0 as CPD
,'' as MaThuTu
,thongtinduan.SDuAnCongTrinh
,thongtinduan.SoQuyetDinhDauTu
,thongtinduan.NgayQuyetDinhDauTu
,thongtinduan.TienDo
,thongtinduan.TMDT_NSQP
,thongtinduan.TMDT_NSNN
,thongtinduan.TMDT_Khac
,thongtinduan.Tong_TMDT
,isnull(vonnamtruoc.LuyKeVonNamTruoc,0) LuyKeVonNamTruoc
,isnull(vonnamnay.ChiTieuNganSachNam,0) ChiTieuNganSachNam
,giaingan.ThanhToan
,giaingan.TamUng
,giaingan.ThuUng
,giaingan.KeHoachUngNgoaiChiTieu
,giaingan.CapUngNgoaiChiTieu
,giaingan.ThuUngXDCB
,0 SoUngConPhaiThu
,isnull(vonnamnay.ChiTieuNganSachNam,0) - giaingan.ThanhToan - giaingan.TamUng + giaingan.ThuUng as ChiTieuConLaiChuaCap
,thongtinduan.Tong_TMDT - isnull(vonnamtruoc.LuyKeVonNamTruoc,0) - isnull(vonnamnay.ChiTieuNganSachNam,0) - giaingan.KeHoachUngNgoaiChiTieu as SoVonConBoTriTiep
,0 as IsHangCha
from 
(
	select a.SDuAnCongTrinh
	,a.IdDuAn
	,a.SoQuyetDinhDauTu
	,a.NgayQuyetDinhDauTu
	,a.TienDo
	,sum(a.TMDT_NSQP) as TMDT_NSQP
	,sum(a.TMDT_NSNN) as TMDT_NSNN
	,sum(a.TMDT_Khac) as TMDT_Khac
	,sum(a.TMDT_NSQP) + sum(a.TMDT_NSNN) + sum(a.TMDT_Khac)  as Tong_TMDT
	from
	(
		select da.sTenDuAn as SDuAnCongTrinh
		,qddt.iID_DuAnID as IdDuAn
		,qddt.sSoQuyetDinh as SoQuyetDinhDauTu
		,qddt.dNgayQuyetDinh as NgayQuyetDinhDauTu
		,qddt.sKhoiCong + '-' + qddt.sKetThuc as TienDo
		,case when qddt_nv.iID_NguonVonID = 1 then fTienPheDuyet else 0 end as TMDT_NSQP
		,case when qddt_nv.iID_NguonVonID = 2 then fTienPheDuyet else 0 end as TMDT_NSNN
		,case when qddt_nv.iID_NguonVonID != 1 and qddt_nv.iID_NguonVonID != 2 then fTienPheDuyet else 0 end as TMDT_Khac
		from VDT_DA_QDDauTu qddt
			inner join VDT_DA_DuAn da on qddt.iID_DuAnID = da.iID_DuAnID
			inner join VDT_DA_QDDauTu_NguonVon qddt_nv on qddt_nv.iID_QDDauTuID = qddt.iID_QDDauTuID
		where qddt.bActive = 1 
				and qddt.iID_DuAnID in (select iID_DuAnID from #tmpIdDA)
	) as a
	group by SoQuyetDinhDauTu,NgayQuyetDinhDauTu,IdDuAn,SDuAnCongTrinh,TienDo
) as thongtinduan
left join
(
	select a.iID_DuAnID
	, sum(a.fCapPhatKhoBac) + sum(a.fCapPhatLenhChi) + sum(a.fTonKhoanDonVi) as LuyKeVonNamTruoc
	from
	(
		select pbv.Id
		,pbv.bActive
		,pbv.iID_ParentId
		,pbv.iNamKeHoach
		,pbv.sSoQuyetDinh
		,case when pbv.iID_ParentId is not null then isnull(pbvct.fCapPhatTaiKhoBacDC,0) else isnull(pbvct.fCapPhatTaiKhoBac,0) end fCapPhatKhoBac
		,case when pbv.iID_ParentId is not null then isnull(pbvct.fCapPhatBangLenhChiDC,0) else isnull(pbvct.fCapPhatBangLenhChi,0) end fCapPhatLenhChi
		,case when pbv.iID_ParentId is not null then isnull(pbvct.fTonKhoanTaiDonViDC,0) else isnull(pbvct.fTonKhoanTaiDonVi,0) end fTonKhoanDonVi
		,pbvct.iID_DuAnID
		from VDT_KHV_PhanBoVon pbv
				inner join VDT_KHV_PhanBoVon_ChiTiet pbvct on pbvct.iID_PhanBoVonID = pbv.iID_PhanBoGocID
		where pbv.bActive = 1
		and iNamKeHoach < @namThucHien
		and pbvct.iID_DuAnID in (select iID_DuAnID from #tmpIdDA)
	) as a
	group by a.iID_DuAnID
) as vonnamtruoc on thongtinduan.IdDuAn = vonnamtruoc.iID_DuAnID
left join
(
	select a.iID_DuAnID
	, sum(a.fCapPhatKhoBac) + sum(a.fCapPhatLenhChi) + sum(a.fTonKhoanDonVi) as ChiTieuNganSachNam
	from
	(
		select pbv.Id
		,pbv.bActive
		,pbv.iID_ParentId
		,pbv.iNamKeHoach
		,pbv.sSoQuyetDinh
		,case when pbv.iID_ParentId is not null then isnull(pbvct.fCapPhatTaiKhoBacDC,0) else isnull(pbvct.fCapPhatTaiKhoBac,0) end fCapPhatKhoBac
		,case when pbv.iID_ParentId is not null then isnull(pbvct.fCapPhatBangLenhChiDC,0) else isnull(pbvct.fCapPhatBangLenhChi,0) end fCapPhatLenhChi
		,case when pbv.iID_ParentId is not null then isnull(pbvct.fTonKhoanTaiDonViDC,0) else isnull(pbvct.fTonKhoanTaiDonVi,0) end fTonKhoanDonVi
		,pbvct.iID_DuAnID
		from VDT_KHV_PhanBoVon pbv
				inner join VDT_KHV_PhanBoVon_ChiTiet pbvct on pbvct.iID_PhanBoVonID = pbv.iID_PhanBoGocID
		where pbv.bActive = 1
		and iNamKeHoach = @namThucHien
		and pbvct.iID_DuAnID in (select iID_DuAnID from #tmpIdDA)
	) as a
	group by a.iID_DuAnID
) vonnamnay on thongtinduan.IdDuAn = vonnamnay.iID_DuAnID
left join 
(
	select iID_DuAnID
	, sum (case when sMaDich = '201' and sMaNguonCha = '101' and sMaTienTrinh = '200' then fGiaTri else 0 end) 
	+ sum (case when sMaDich = '202' and sMaNguonCha = '102' and sMaTienTrinh = '200' then fGiaTri else 0 end)
	+ sum (case when sMaDich = '203' and sMaNguonCha = '103' and sMaTienTrinh = '200' then fGiaTri else 0 end)
	- sum (case when sMaDich = '201' and sMaNguonCha = '101' and sMaTienTrinh = '300' then fGiaTri else 0 end)
	- sum (case when sMaDich = '202' and sMaNguonCha = '102' and sMaTienTrinh = '300' then fGiaTri else 0 end)
	- sum (case when sMaDich = '203' and sMaNguonCha = '103' and sMaTienTrinh = '300' then fGiaTri else 0 end)
	- sum (case when sMaDich = '211a' and iThuHoiTUCheDo = 1 and sMaNguonCha = '101' and sMaTienTrinh = '200' then fGiaTri else 0 end)
	- sum (case when sMaDich = '212a' and iThuHoiTUCheDo = 1 and sMaNguonCha = '102' and sMaTienTrinh = '200' then fGiaTri else 0 end)
	- sum (case when sMaDich = '213a' and iThuHoiTUCheDo = 1 and sMaNguonCha = '103' and sMaTienTrinh = '200' then fGiaTri else 0 end)
	+ sum (case when sMaDich = '121a' and sMaNguonCha = '101' and sMaTienTrinh = '200' then fGiaTri else 0 end)
	+ sum (case when sMaDich = '122a' and sMaNguonCha = '102' and sMaTienTrinh = '200' then fGiaTri else 0 end)
	+ sum (case when sMaDich = '123a' and sMaNguonCha = '103' and sMaTienTrinh = '200' then fGiaTri else 0 end)
	as ThanhToan
	, sum (case when sMaDich = '211a' and sMaNguonCha = '101' and sMaTienTrinh = '200' then fGiaTri else 0 end) 
	+ sum (case when sMaDich = '212a' and sMaNguonCha = '102' and sMaTienTrinh = '200' then fGiaTri else 0 end) 
	+ sum (case when sMaDich = '213a' and sMaNguonCha = '103' and sMaTienTrinh = '200' then fGiaTri else 0 end) 
	- sum (case when sMaDich = '211a' and sMaNguonCha = '101' and sMaTienTrinh = '300' then fGiaTri else 0 end) 
	- sum (case when sMaDich = '212a' and sMaNguonCha = '102' and sMaTienTrinh = '300' then fGiaTri else 0 end) 
	- sum (case when sMaDich = '213a' and sMaNguonCha = '103' and sMaTienTrinh = '300' then fGiaTri else 0 end) 
	- sum (case when sMaDich = '211a' and iThuHoiTUCheDo = 2 and sMaNguonCha = '101' and sMaTienTrinh = '200' then fGiaTri else 0 end)
	- sum (case when sMaDich = '212a' and iThuHoiTUCheDo = 2 and sMaNguonCha = '102' and sMaTienTrinh = '200' then fGiaTri else 0 end)
	- sum (case when sMaDich = '213a' and iThuHoiTUCheDo = 2 and sMaNguonCha = '103' and sMaTienTrinh = '200' then fGiaTri else 0 end)
	+ sum (case when sMaDich = '211a' and sMaNguonCha = '101' and sMaTienTrinh = '300' then fGiaTri else 0 end) 
	+ sum (case when sMaDich = '212a' and sMaNguonCha = '102' and sMaTienTrinh = '300' then fGiaTri else 0 end) 
	+ sum (case when sMaDich = '213a' and sMaNguonCha = '103' and sMaTienTrinh = '300' then fGiaTri else 0 end) 
	as TamUng
	, sum (case when sMaDich = '211a' and iThuHoiTUCheDo = 2 and sMaNguonCha = '101' and sMaTienTrinh = '300' then fGiaTri else 0 end) 
	+ sum (case when sMaDich = '212a' and iThuHoiTUCheDo = 2 and sMaNguonCha = '102' and sMaTienTrinh = '300' then fGiaTri else 0 end)  
	+ sum (case when sMaDich = '213a' and iThuHoiTUCheDo = 2 and sMaNguonCha = '103' and sMaTienTrinh = '300' then fGiaTri else 0 end) 
	as ThuUng
	, sum (case when sMaNguon='121a' and sMaTienTrinh = '200' then fGiaTri else 0 end) 
	+ sum (case when sMaNguon='122a' and sMaTienTrinh = '200' then fGiaTri else 0 end)
	+ sum (case when sMaNguon='123a' and sMaTienTrinh = '200' then fGiaTri else 0 end)
	- sum (case when sMaNguon='121a' and sMaTienTrinh = '300' then fGiaTri else 0 end) 
	- sum (case when sMaNguon='122a' and sMaTienTrinh = '300' then fGiaTri else 0 end)
	- sum (case when sMaNguon='123a' and sMaTienTrinh = '300' then fGiaTri else 0 end)
	as KeHoachUngNgoaiChiTieu
	,0 as CapUngNgoaiChiTieu
	,0 as ThuUngXDCB
	from VDT_TongHop_NguonNSDauTu
	where iNamKeHoach = @namThucHien and iID_NguonVonID = @nguonVonId
	and iID_DuAnID  in (select iID_DuAnID from #tmpIdDA)
	group by iID_DuAnID
) giaingan on thongtinduan.IdDuAn = giaingan.iID_DuAnID

drop table #tmpIdDA;

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_tt_get_khvthanhtoan]    Script Date: 10/24/2024 4:31:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_tt_get_khvthanhtoan]
	@iIdDuAnId [uniqueidentifier],
	@iIdNguonVonId [int],
	@dNgayDeNghi [date],
	@iNamKeHoach [int],
	@iCoQuanThanhToan [int],
	@iIdPheDuyet [uniqueidentifier],
	@ID_DuAn_HangMuc [uniqueidentifier]
WITH EXECUTE AS CALLER
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
		ID_DuAn_HangMuc uniqueidentifier
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
				and tbl.bActive = 1
				AND tbl.iID_NguonVonID = @iIdNguonVonId
				AND iNamKeHoach = @iNamKeHoach
				AND CAST(tbl.dNgayQuyetDinh as DATE) <= CAST(@dNgayDeNghi as DATE)
		)
		INSERT INTO #tmp(IIdKeHoachVonId, SSoQuyetDinh, INamKeHoach, ILoaiKeHoachVon, ILoaiNamKhv, ICoQuanThanhToan, FGiaTriKHV, ID_DuAn_HangMuc)
		SELECT tmp.Id , tbl.SSoQuyetDinh, tbl.INamKeHoach, 1, (CASE WHEN tbl.iLoaiDuToan = 2 THEN 1 ELSE 2 END), @iCoQuanThanhToan,
		--SELECT tmp.Id , tbl.SSoQuyetDinh, tbl.INamKeHoach, 1, 2, @iCoQuanThanhToan,
				(CASE @iCoQuanThanhToan 
					WHEN 1 THEN SUM(CASE WHEN tbl.iID_ParentId is null THEN  ISNULL(dt.fCapPhatTaiKhoBac, 0) ELSE ISNULL(dt.fCapPhatTaiKhoBacDC, 0) END)
					WHEN 2 THEN SUM(CASE WHEN tbl.iID_ParentId is null THEN  ISNULL(dt.fCapPhatBangLenhChi, 0) ELSE ISNULL(dt.fCapPhatBangLenhChiDC, 0) END)
					WHEN 3 THEN SUM(CASE WHEN tbl.iID_ParentId is null THEN  ISNULL(dt.fTonKhoanTaiDonVi, 0) ELSE ISNULL(dt.fTonKhoanTaiDonViDC, 0) END)
					ELSE 0 END) as FGiaTri,
		dt.iID_DuAn_HangMucID
		FROM tmp as tmp
		INNER JOIN VDT_KHV_PhanBoVon as tbl on tmp.Id = tbl.Id
		INNER JOIN VDT_KHV_PhanBoVon_ChiTiet as dt on tbl.Id = dt.iID_PhanBoVonID
		WHERE tmp.rn = 1 AND dt.iID_DuAnID = @iIdDuAnId and dt.iID_DuAn_HangMucID = @ID_DuAn_HangMuc
		GROUP BY tmp.Id, tbl.SSoQuyetDinh, tbl.INamKeHoach, tbl.iLoaiDuToan, dt.iID_DuAn_HangMucID
	END

	-- Ke hoach von ung
	BEGIN
		INSERT INTO #tmp(IIdKeHoachVonId, SSoQuyetDinh, INamKeHoach, ILoaiKeHoachVon, ILoaiNamKhv, ICoQuanThanhToan, FGiaTriKHV, ID_DuAn_HangMuc)
		SELECT tbl.Id, tbl.sSoQuyetDinh, tbl.iNamKeHoach, 2 , 2, @iCoQuanThanhToan,
			(CASE @iCoQuanThanhToan 
					WHEN 1 THEN SUM(ISNULL(dt.fCapPhatTaiKhoBac, 0)) 
					WHEN 2 THEN SUM(ISNULL(dt.fCapPhatBangLenhChi, 0))
					WHEN 3 THEN SUM(ISNULL(dt.fTonKhoanTaiDonVi, 0)) 
					ELSE 0 END) as FGiaTri,
		dt.ID_DuAn_HangMuc
		FROM VDT_KHV_KeHoachVonUng as tbl
		INNER JOIN VDT_KHV_KeHoachVonUng_ChiTiet as dt on tbl.Id = dt.iID_KeHoachUngID
		WHERE dt.iID_DuAnID = @iIdDuAnId and dt.ID_DuAn_HangMuc = @ID_DuAn_HangMuc
			AND tbl.iID_NguonVonID = @iIdNguonVonId
			AND CAST(tbl.dNgayQuyetDinh as DATE) <= CAST(@dNgayDeNghi as DATE)
			AND tbl.iNamKeHoach = @iNamKeHoach
		GROUP BY tbl.Id, tbl.sSoQuyetDinh, tbl.iNamKeHoach, dt.ID_DuAn_HangMuc
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
	WHERE tbl.iID_DuAnId = @iIdDuAnId and tbl.ID_DuAn_HangMuc = @ID_DuAn_HangMuc
		AND tbl.iCoQuanThanhToan = @iCoQuanThanhToan
		AND CAST(dNgayDeNghi as DATE) <= CAST(@dNgayDeNghi as DATE)
		AND (@iIdPheDuyet IS NULL OR tbl.Id <> @iIdPheDuyet)
	GROUP BY tmp.IIdKeHoachVonId, tmp.ILoaiKeHoachVon

	SELECT tbl.IIdKeHoachVonId, tbl.SSoQuyetDinh, tbl.ILoaiKeHoachVon, tbl.ILoaiNamKhv, tbl.INamKeHoach, tbl.ICoQuanThanhToan, tbl.FGiaTriKHV, SUM(ISNULL(dt.fThanhToan, 0)) as FGiaTriKHVDaThanhToan, SUM(ISNULL(dt.fThuHoi, 0)) as FGiaTriKHVDaThuHoi, tbl.ID_DuAn_HangMuc INTO #tbl
	FROM #tmp as tbl
	LEFT JOIN #tmpThanhToan as dt on tbl.IIdKeHoachVonId = dt.IIdKeHoachVonId AND tbl.ILoaiKeHoachVon = dt.ILoaiKeHoachVon
	GROUP BY tbl.IIdKeHoachVonId, tbl.SSoQuyetDinh, tbl.ILoaiKeHoachVon, tbl.ILoaiNamKhv, tbl.INamKeHoach, tbl.ICoQuanThanhToan, tbl.FGiaTriKHV, tbl.ID_DuAn_HangMuc

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
;
;
GO
