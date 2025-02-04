/****** Object:  StoredProcedure [dbo].[sp_vdt_get_kehoachvon_capphatthanhtoan]    Script Date: 10/11/2024 2:49:53 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_kehoachvon_capphatthanhtoan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_kehoachvon_capphatthanhtoan]
GO

/****** Object:  StoredProcedure [dbo].[sp_vdt_get_kehoachvon_capphatthanhtoan]    Script Date: 10/11/2024 2:49:53 PM ******/
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
										AND (dt.sMaNguon in ('101', '102','103')  AND dt.sMaDich = '000' AND dt.sMaTienTrinh = '200')
	GROUP BY tbl.Id,dt.sSoQuyetDinh, dt.dNgayQuyetDinh, dt.iNamKeHoach, dt.iID_NguonVonID,dt.sMaNguon

	INSERT INTO #tmp(Id, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_NguonVonID, FTongGiaTri, sMaNguonCha, TenLoai, PhanLoai)
	SELECT tbl.iID_ChungTu, tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.iID_NguonVonID, SUM(ISNULL(tbl.fGiaTri, 0)), sMaNguon,
		CASE WHEN sMaNguon COLLATE DATABASE_DEFAULT in ('121a', '122a','123a') THEN N'Kế hoạch vốn ứng'
			WHEN sMaNguon COLLATE DATABASE_DEFAULT in ('111', '112','113') THEN N'Kế hoạch năm trước chuyển sang'
			WHEN sMaNguon COLLATE DATABASE_DEFAULT in ('131', '132','133') THEN N'Kế hoạch ứng trước năm trước chuyển sang' END, 
		CASE WHEN sMaNguon COLLATE DATABASE_DEFAULT in ('121a', '122a','123a') THEN 2
			WHEN sMaNguon COLLATE DATABASE_DEFAULT in ('111', '112','113') THEN 3
			WHEN sMaNguon COLLATE DATABASE_DEFAULT in ('131', '132','133') THEN 4 END
	FROM VDT_TongHop_NguonNSDauTu as tbl
	WHERE ((tbl.sMaNguon COLLATE DATABASE_DEFAULT in ('121a', '122a','123a') AND sMaTienTrinh COLLATE DATABASE_DEFAULT = '200') OR (tbl.sMaNguon COLLATE DATABASE_DEFAULT in ('111', '112','113', '131', '132','133') AND sMaTienTrinh COLLATE DATABASE_DEFAULT = '100'))
		AND sMaDich COLLATE DATABASE_DEFAULT = '000'
		AND bIsLog = 0
		AND tbl.iID_DuAnID = @DuAnId AND tbl.iID_NguonVonID = @NguonVonId 
		AND CAST(tbl.dNgayQuyetDinh as DATE) <= CAST(@dNgayDeNghi as DATE)
		AND tbl.iNamKeHoach = @NamKeHoach
	GROUP BY tbl.iID_ChungTu, tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.iID_NguonVonID, sMaNguon

	--select * from #tmpChungTuVonNam

	
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
	ELSE IF(@iCoQuanThanhToan = 1)
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
			else (SELECT top 1 fCapPhatBangLenhChi from VDT_KHV_KeHoachVonUng_ChiTiet khvu_dd where khvu_dd.iID_KeHoachUngID=#tmp.Id) end			
			-- Kế hoạch năm trước chuyển sang (112) và kế hoạch vốn ứng năm trước chuyển sang (132) chưa clear
			WHERE Id = (SELECT top 1 Id FROM #tmpSTT2 where stt = @counter)  and sMaNguonCha in (select * from #tmpMaNguon)
			set @counter = @counter - 1;
		end 
		DROP TABLE #tmpSTT2
	END
	ELSE
	BEGIN
		INSERT INTO #tmpMaNguon(sMaNguon)
		VALUES('103'), ('123a'), ('113'), ('133')
		set @counter = (select count(Id) from #tmpChungTuVonNam);
		select ROW_NUMBER() over(order by id) as stt, Id into #tmpSTT3 from #tmpChungTuVonNam;
		while(@counter > 0)
		begin
			UPDATE #tmp 
			SET fTongGiaTri =
			-- Kế hoạch vốn năm
			case when sMaNguonCha = '103' or sMaNguonCha = '113' then 
			(SELECT top 1 fTonKhoanTaiDonVi  from VDT_KHV_PhanBoVon_chitiet khvn_dd where khvn_dd.iID_PhanBoVonID=#tmp.Id)
			-- Kế hoạch vốn ứng
			else (SELECT top 1 fTonKhoanTaiDonVi from VDT_KHV_KeHoachVonUng_ChiTiet khvu_dd where khvu_dd.iID_KeHoachUngID=#tmp.Id) end			
			-- Kế hoạch năm trước chuyển sang (113) và kế hoạch vốn ứng năm trước chuyển sang (133) chưa clear
			WHERE Id = (SELECT top 1 Id FROM #tmpSTT3 where stt = @counter)  and sMaNguonCha in (select * from #tmpMaNguon)
			set @counter = @counter - 1;
		end 
		DROP TABLE #tmpSTT3
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
;
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_insert_tonghopnguondautu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_insert_tonghopnguondautu]
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
			(CASE WHEN sMaNguonCha in ('121a','122a','123a') AND sMaDich in ('101', '102','103') THEN (CASE WHEN @sLoai = 'KHVN' THEN 1 ELSE 0 END) ELSE NULL END),
			tbl.IIDMucID, tbl.IIDTieuMucID, tbl.IIDTietMucID, tbl.IIDNganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, tbl.IIdLoaiCongTrinh

	FROM @data as tbl
	LEFT JOIN NS_MucLucNganSach as ml on ml.iID = tbl.IIDMucID OR ml.iID = tbl.IIDTieuMucID OR ml.iID = tbl.IIDTietMucID OR ml.iID = tbl.IIDNganhID
END
;
;
;
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_insert_tonghopnguonnsdautu_tang_new]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_insert_tonghopnguonnsdautu_tang_new]
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
						(select Id,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, iID_LoaiCongTrinh, fCapPhatTaiKhoBac,fCapPhatBangLenhChi,fTonKhoanTaiDonVi,fGiaTriThuHoiNamTruocKhoBac,fGiaTriThuHoiNamTruocLenhChi from VDT_KHV_PhanBoVon_ChiTiet) as tbl
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
						(select Id, iID_LoaiCongTrinh,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID , fCapPhatTaiKhoBac,fCapPhatBangLenhChi,fTonKhoanTaiDonVi,fGiaTriThuHoiNamTruocKhoBac,fGiaTriThuHoiNamTruocLenhChi from VDT_KHV_PhanBoVon_ChiTiet) as tbl
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
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_get_nguonvon_tonghopnguondautu_by_condition]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_get_nguonvon_tonghopnguondautu_by_condition]
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
												AND tbl.sMaDich in ('201', '202','203', '211a', '212a','213a')
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
;
GO


