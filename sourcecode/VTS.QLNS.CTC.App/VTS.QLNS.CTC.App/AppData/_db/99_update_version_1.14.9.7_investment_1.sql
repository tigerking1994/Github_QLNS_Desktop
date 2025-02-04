IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_kehoachvon_capphatthanhtoan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_kehoachvon_capphatthanhtoan]
GO
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
	IF(@iCoQuanThanhToan = 1) --KHO BẠC
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
			(SELECT top 1 CASE WHEN ISNULL(fCapPhatTaiKhoBacDC, 0) > 0 THEN fCapPhatTaiKhoBacDC ELSE fCapPhatTaiKhoBac END from VDT_KHV_PhanBoVon_chitiet khvn_dd where khvn_dd.iID_PhanBoVonID=#tmp.Id)
			-- Kế hoạch vốn ứng
			else (SELECT top 1 fCapPhatTaiKhoBac from VDT_KHV_KeHoachVonUng_ChiTiet khvu_dd where khvu_dd.iID_KeHoachUngID=#tmp.Id) end		
			-- Kế hoạch năm trước chuyển sang (111) và kế hoạch vốn ứng năm trước chuyển sang (131) chưa clear
			WHERE Id = (SELECT top 1 Id FROM #tmpSTT1 where stt = @counter)  and sMaNguonCha in (select * from #tmpMaNguon)
			set @counter = @counter - 1;
		end 
		DROP TABLE #tmpSTT1;
	END
	ELSE IF(@iCoQuanThanhToan = 2) --LENH CHI
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
			(SELECT top 1 CASE WHEN ISNULL(fCapPhatBangLenhChiDC, 0) > 0 THEN fCapPhatBangLenhChiDC ELSE fCapPhatBangLenhChi END  from VDT_KHV_PhanBoVon_chitiet khvn_dd where khvn_dd.iID_PhanBoVonID=#tmp.Id)
			--(SELECT top 1 fCapPhatTaiKhoBac from VDT_KHV_PhanBoVon_chitiet khvn_dd where khvn_dd.iID_PhanBoVonID=#tmp.Id)
			-- Kế hoạch vốn ứng
			else (SELECT top 1 fCapPhatBangLenhChi from VDT_KHV_KeHoachVonUng_ChiTiet khvu_dd where khvu_dd.iID_KeHoachUngID=#tmp.Id) end			
			-- Kế hoạch năm trước chuyển sang (112) và kế hoạch vốn ứng năm trước chuyển sang (132) chưa clear
			WHERE Id = (SELECT top 1 Id FROM #tmpSTT2 where stt = @counter)  and sMaNguonCha in (select * from #tmpMaNguon)
			set @counter = @counter - 1;
		end 
		DROP TABLE #tmpSTT2
	END
	ELSE IF(@iCoQuanThanhToan = 3) --TON KHOAN DON VI
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
			(SELECT top 1 CASE WHEN ISNULL(fTonKhoanTaiDonViDC, 0) > 0 THEN fTonKhoanTaiDonViDC ELSE fTonKhoanTaiDonVi END  from VDT_KHV_PhanBoVon_chitiet khvn_dd where khvn_dd.iID_PhanBoVonID=#tmp.Id)
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
;
GO