/****** Object:  StoredProcedure [dbo].[sp_vdt_tt_get_mlns_by_khv_new]    Script Date: 15/12/2023 2:40:50 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_tt_get_mlns_by_khv_new]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_tt_get_mlns_by_khv_new]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan_bao_cao]    Script Date: 15/12/2023 2:40:50 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan_bao_cao]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan_bao_cao]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_kehoachvon_capphatthanhtoan]    Script Date: 18/12/2023 9:31:26 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_kehoachvon_capphatthanhtoan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_kehoachvon_capphatthanhtoan]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan_bao_cao]    Script Date: 15/12/2023 2:40:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--select * from VDT_TT_DeNghiThanhToan
CREATE PROCEDURE [dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan_bao_cao]
	@bThanhToanTheoHopDong bit,
	@iIdChungTu varchar(max), --- ID hợp đồng hoặc chi phí theo loại thanh toán theo hợp đồng hoặc chi phí
	@NgayDeNghi datetime,
	@NguonVonId int,
	@NamKeHoach int,
	@iCoQuanThanhToan int,
	@loaiCoQuanTaiChinh int,
	@loaiKhv int 
AS
BEGIN
	DECLARE @uIdEmpty uniqueidentifier = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER)
	DECLARE @fLuyKeTTKLHTNN float
	DECLARE @fLuyKeTTKLHTTN float
	DECLARE @fLuyKeTUChuaThuHoiCheDoNN float
	DECLARE @fLuyKeTUChuaThuHoiCheDoTN float
	DECLARE @fLuyKeTUChuaThuHoiUngTruocNN float
	DECLARE @fLuyKeTUChuaThuHoiUngTruocTN float
	DECLARE @fLuyKeThuHoiTN float
	DECLARE @fLuyKeThuHoiNN float	
	SELECT
		(CASE WHEN iLoaiThanhToan = 1 AND ISNULL(dt.fGiaTriThanhToanTN,0) <> 0 THEN SUM(ISNULL(dt.fGiaTriThanhToanTN, 0)) ELSE SUM(0) END) as ThanhToanTN,
		(CASE WHEN iLoaiThanhToan = 1 AND ISNULL(dt.fGiaTriThanhToanNN,0) <> 0 THEN SUM(ISNULL(dt.fGiaTriThanhToanNN, 0)) ELSE SUM(0) END) as ThanhToanNN,
		(CASE WHEN iLoaiThanhToan = 1 AND (ISNULL(dt.fGiaTriThuHoiNamTruocTN,0) <> 0 OR ISNULL(dt.fGiaTriThuHoiNamNayTN,0) <> 0) THEN SUM(ISNULL(fGiaTriThuHoiNamTruocTN, 0) + ISNULL(fGiaTriThuHoiNamNayTN, 0)) ELSE SUM(0) END) as ThuHoiUngTN,
		(CASE WHEN iLoaiThanhToan = 1 AND (ISNULL(dt.fGiaTriThuHoiNamTruocNN,0) <> 0 OR ISNULL(dt.fGiaTriThuHoiNamNayNN,0) <> 0) THEN SUM(ISNULL(fGiaTriThuHoiNamTruocNN, 0) + ISNULL(fGiaTriThuHoiNamNayNN, 0)) ELSE SUM(0) END) as ThuHoiUngNN,
		(CASE WHEN iLoaiThanhToan = 1 AND (ISNULL(dt.fGiaTriThuHoiUngTruocNamNayTN,0) <> 0 OR ISNULL(dt.fGiaTriThuHoiUngTruocNamTruocTN,0) <> 0) THEN SUM(ISNULL(fGiaTriThuHoiUngTruocNamNayTN, 0) + ISNULL(fGiaTriThuHoiUngTruocNamTruocTN, 0)) ELSE SUM(0) END) as ThuHoiUngUngTruocTN,
		(CASE WHEN iLoaiThanhToan = 1 AND (ISNULL(dt.fGiaTriThuHoiUngTruocNamNayNN,0) <> 0 OR ISNULL(dt.fGiaTriThuHoiUngTruocNamTruocNN,0) <> 0) THEN SUM(ISNULL(fGiaTriThuHoiUngTruocNamNayNN, 0) + ISNULL(fGiaTriThuHoiUngTruocNamTruocNN, 0)) ELSE SUM(0) END) as ThuHoiUngUngTruocNN,
		(case WHEN iLoaiThanhToan = 2 AND ISNULL(dt.fGiaTriThanhToanTN,0) <> 0 AND khv.iLoai in(2,4) THEN SUM(ISNULL(dt.fGiaTriThanhToanTN, 0)) ELSE SUM(0) END) as TamUngUngTruocTN,
		(case WHEN iLoaiThanhToan = 2 AND ISNULL(dt.fGiaTriThanhToanNN,0) <> 0 AND khv.iLoai in(2,4) THEN SUM(ISNULL(dt.fGiaTriThanhToanNN, 0)) ELSE SUM(0) END) as TamUngUngTruocNN,
		(case WHEN iLoaiThanhToan = 2 AND ISNULL(dt.fGiaTriThanhToanTN,0) <> 0 AND khv.iLoai in(1,3) THEN SUM(ISNULL(dt.fGiaTriThanhToanTN, 0)) ELSE SUM(0) END) as TamUngCheDoTN,
		(case WHEN iLoaiThanhToan = 2 AND ISNULL(dt.fGiaTriThanhToanNN,0) <> 0 AND khv.iLoai in(1,3) THEN SUM(ISNULL(dt.fGiaTriThanhToanNN, 0)) ELSE SUM(0) END) as TamUngCheDoNN,
		(CASE WHEN iLoaiThanhToan = 1 AND ISNULL(dt.fGiaTriThuHoiNamNayTN,0) <> 0 THEN SUM(ISNULL(dt.fGiaTriThuHoiNamNayTN, 0)) ELSE SUM(0) END) as ThuHoiTN,		
		(CASE WHEN iLoaiThanhToan = 1 AND ISNULL(dt.fGiaTriThuHoiNamNayNN,0) <> 0 THEN SUM(ISNULL(dt.fGiaTriThuHoiNamNayNN, 0)) ELSE SUM(0) END) as ThuHoiNN
		INTO #tmp
	FROM VDT_TT_DeNghiThanhToan tbl
	INNER JOIN VDT_TT_DeNghiThanhToan_KHV as khv on tbl.Id = khv.iID_DeNghiThanhToanID
	LEFT JOIN VDT_TT_PheDuyetThanhToan_ChiTiet as dt on tbl.Id = dt.iID_DeNghiThanhToanID
	WHERE @iIdChungTu = (CASE WHEN @bThanhToanTheoHopDong = 1 THEN tbl.iID_HopDongID ELSE tbl.iID_ChiPhiID END)
		  AND 
		  (
			  CONVERT(DATE, tbl.dNgayPheDuyet) < CONVERT(DATE, @NgayDeNghi) 
			  --CONVERT(DATE, tbl.dNgayPheDuyet) < CONVERT(DATE, '2023/12/12 00:00:00') 
			  and iNamKeHoach <= @NamKeHoach
		  )
		  AND (tbl.iCoQuanThanhToan = @iCoQuanThanhToan and (@iCoQuanThanhToan = 1 or (@iCoQuanThanhToan = 2 and tbl.loaiCoQuanTaiChinh = @loaiCoQuanTaiChinh)))
		  AND tbl.iID_NguonVonID = @NguonVonId
		  		  and (
						iLoaiThanhToan = 0 or
						-- nếu loại thanh toán là thanh toán -> tính tổng những đề nghị trước có loại kế hoạch vốn năm là 1,3 hoặc 2,4 phụ thuộc loại đang chọn
						(@loaiKhv in (1,3) and khv.iLoai in (1,3))
						or
						(@loaiKhv in (2,4) and khv.iLoai in (2,4))
						or @loaiKhv = 0
					)
	GROUP BY iLoaiThanhToan,dt.fGiaTriThanhToanTN, dt.fGiaTriThanhToanNN, dt.fGiaTriThuHoiNamTruocTN, dt.fGiaTriThuHoiNamNayTN, dt.fGiaTriThuHoiNamTruocNN, dt.fGiaTriThuHoiNamNayNN, khv.iLoai,
		fGiaTriThuHoiUngTruocNamNayTN, fGiaTriThuHoiUngTruocNamTruocTN, fGiaTriThuHoiUngTruocNamNayNN, fGiaTriThuHoiUngTruocNamTruocNN, fGiaTriThuHoiTN, fGiaTriThuHoiNN
	

	SELECT ISNULL(SUM(ISNULL(ThanhToanTN, 0)), 0) as ThanhToanTN,
			ISNULL(SUM(ISNULL(ThanhToanNN, 0)), 0) as ThanhToanNN,
			ISNULL(SUM(ISNULL(ThuHoiUngTN, 0)), 0) as ThuHoiUngTN,
			ISNULL(SUM(ISNULL(ThuHoiUngNN, 0)), 0) as ThuHoiUngNN,
			ISNULL(SUM(ISNULL(TamUngCheDoTN, 0)), 0) as TamUngTN,
			ISNULL(SUM(ISNULL(TamUngCheDoNN, 0)), 0) as TamUngNN,
			ISNULL(SUM(ISNULL(ThuHoiUngUngTruocNN, 0)), 0) as ThuHoiUngUngTruocNN,
			ISNULL(SUM(ISNULL(ThuHoiUngUngTruocTN, 0)), 0) as ThuHoiUngUngTruocTN,
			ISNULL(SUM(ISNULL(TamUngUngTruocTN, 0)), 0) as TamUngUngTruocTN,
			ISNULL(SUM(ISNULL(TamUngUngTruocNN, 0)), 0) as TamUngUngTruocNN,	
			ISNULL(SUM(ISNULL(ThuHoiTN, 0)), 0) as ThuHoiTN,
			ISNULL(SUM(ISNULL(ThuHoiNN, 0)), 0) as ThuHoiNN
	FROM  #tmp
	DROP TABLE #tmp
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_tt_get_mlns_by_khv_new]    Script Date: 15/12/2023 2:40:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_tt_get_mlns_by_khv_new]
	@iNamLamViec [int],
	@data [dbo].[t_tbl_tonghopdautu_v2] READONLY
WITH EXECUTE AS CALLER
AS
BEGIN
	--SELECT tbl.iID_ChungTu as IidKeHoachVonId, khvn.iID_LoaiCongTrinh,
	--	(CASE WHEN khvn.iID_PhanBoVonID IS NOT NULL THEN 1 ELSE 2 END) as ILoaiKeHoachVon,
	--	(CASE WHEN khvn.iID_PhanBoVonID IS NOT NULL THEN khvn.iID_MucID ELSE khvu.iID_MucID END) as iID_MucID,
	--	(CASE WHEN khvn.iID_PhanBoVonID IS NOT NULL THEN khvn.iID_TieuMucID ELSE khvu.iID_TieuMucID END) as iID_TieuMucID,
	--	(CASE WHEN khvn.iID_PhanBoVonID IS NOT NULL THEN khvn.iID_TietMucID ELSE khvu.iID_TietMucID END) as iID_TietMucID,
	--	(CASE WHEN khvn.iID_PhanBoVonID IS NOT NULL THEN khvn.iID_NganhID ELSE khvu.iID_NganhID END) as iID_NganhID INTO #tmp
	--FROM @data as tbl
	--LEFT JOIN VDT_KHV_PhanBoVon_ChiTiet as khvn on tbl.iID_ChungTu = khvn.iID_PhanBoVonID AND tbl.sMaNguon = N'KHVN' AND khvn.iID_DuAnID = tbl.iID_DuAnID and khvn.iID_DuAn_HangMucID = tbl.IIdLoaiCongTrinh
	--LEFT JOIN VDT_KHV_KeHoachVonUng_ChiTiet as khvu on tbl.iID_ChungTu = khvu.iID_KeHoachUngID AND tbl.sMaNguon = N'KHVU' AND khvu.iID_DuAnID = tbl.iID_DuAnID and khvu.ID_DuAn_HangMuc = tbl.IIdLoaiCongTrinh

	--SELECT tmp.IidKeHoachVonId, tmp.ILoaiKeHoachVon, ml.LNS as LNS, ml.L as L, ml.K as K, ml.M as M, ml.TM as TM, ml.TTM as TTM, ml.NG as NG INTO #tmpMucLuc
	--FROM #tmp as tmp
	--INNER JOIN VDT_DM_LoaiCongTrinh as ml on ml.iID_LoaiCongTrinh = tmp.iID_LoaiCongTrinh
										

	--SELECT IidKeHoachVonId, ILoaiKeHoachVon, LNS, L, K, M, '' as TM, '' as TTM, '' as NG INTO #tmpResult
	--FROM #tmpMucLuc
	--WHERE ISNULL(M, '') <> ''
	--UNION ALL
	--SELECT IidKeHoachVonId, ILoaiKeHoachVon, LNS, L, K, M, TM, '' as TTM, '' as NG
	--FROM #tmpMucLuc
	--WHERE ISNULL(TM, '') <> ''
	--UNION ALL
	--SELECT IidKeHoachVonId, ILoaiKeHoachVon, LNS, L, K, M, TM, TTM, '' as NG
	--FROM #tmpMucLuc
	--WHERE ISNULL(TTM, '') <> ''
	--UNION ALL
	--SELECT IidKeHoachVonId, ILoaiKeHoachVon, LNS, L, K, M, TM, TTM, NG
	--FROM #tmpMucLuc
	--WHERE ISNULL(NG, '') <> ''

	--SELECT DISTINCT tbl.*, sMoTa as SMoTa
	--FROM #tmpResult as tbl
	--INNER JOIN NS_MucLucNganSach as ml on ml.iNamLamViec = @iNamLamViec
	--								AND tbl.LNS COLLATE DATABASE_DEFAULT = ml.sLNS COLLATE DATABASE_DEFAULT
	--								AND tbl.L COLLATE DATABASE_DEFAULT = ml.sL COLLATE DATABASE_DEFAULT
	--								AND tbl.K COLLATE DATABASE_DEFAULT = ml.sK COLLATE DATABASE_DEFAULT
	--								AND tbl.M COLLATE DATABASE_DEFAULT = ml.sM COLLATE DATABASE_DEFAULT
	--								AND tbl.TM COLLATE DATABASE_DEFAULT = ml.sTM COLLATE DATABASE_DEFAULT
	--								AND tbl.TTM COLLATE DATABASE_DEFAULT = ml.sTTM COLLATE DATABASE_DEFAULT
	--								AND tbl.NG COLLATE DATABASE_DEFAULT = ml.sNG COLLATE DATABASE_DEFAULT
	--ORDER BY LNS, L, K, M, TM, TTM, NG

	-- DROP TABLE #tmpResult
	-- DROP TABLE #tmpMucLuc
	--DROP TABLE #tmp


	SELECT tbl.iID_ChungTu as IidKeHoachVonId, khvn.iID_LoaiCongTrinh,
		(CASE WHEN khvn.iID_PhanBoVonID IS NOT NULL THEN 1 ELSE 2 END) as ILoaiKeHoachVon,
		(CASE WHEN khvn.iID_PhanBoVonID IS NOT NULL THEN khvn.iID_MucID ELSE khvu.iID_MucID END) as iID_MucID,
		(CASE WHEN khvn.iID_PhanBoVonID IS NOT NULL THEN khvn.iID_TieuMucID ELSE khvu.iID_TieuMucID END) as iID_TieuMucID,
		(CASE WHEN khvn.iID_PhanBoVonID IS NOT NULL THEN khvn.iID_TietMucID ELSE khvu.iID_TietMucID END) as iID_TietMucID,
		(CASE WHEN khvn.iID_PhanBoVonID IS NOT NULL THEN khvn.iID_NganhID ELSE khvu.iID_NganhID END) as iID_NganhID INTO #tmp
	FROM @data as tbl
	LEFT JOIN VDT_KHV_PhanBoVon_ChiTiet as khvn on tbl.iID_ChungTu = khvn.iID_PhanBoVonID AND tbl.sMaNguon = N'KHVN' AND khvn.iID_DuAnID = tbl.iID_DuAnID and khvn.iID_DuAn_HangMucID = tbl.IIdLoaiCongTrinh
	LEFT JOIN VDT_KHV_KeHoachVonUng_ChiTiet as khvu on tbl.iID_ChungTu = khvu.iID_KeHoachUngID AND tbl.sMaNguon = N'KHVU' AND khvu.iID_DuAnID = tbl.iID_DuAnID and khvu.ID_DuAn_HangMuc = tbl.IIdLoaiCongTrinh

	SELECT IidKeHoachVonId
		, ILoaiKeHoachVon
		, ISNULL(sLNS, '') as LNS
		, ISNULL(sL, '') as L
		, ISNULL(sK, '') as K
		, ISNULL(sM, '') as M
		, ISNULL(sTM, '') as TM
		, ISNULL(sTTM, '') as TTM
		, ISNULL(sNG, '') as NG
		, sMoTa as SMoTa
	FROM #tmp as tmp
	INNER JOIN NS_MucLucNganSach mlns ON tmp.iID_NganhID = mlns.iID

	DROP TABLE #tmp
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_kehoachvon_capphatthanhtoan]    Script Date: 18/12/2023 9:31:26 AM ******/
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
			case when sMaNguonCha = '101' then 
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
			case when sMaNguonCha = '102' then 
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

GO
