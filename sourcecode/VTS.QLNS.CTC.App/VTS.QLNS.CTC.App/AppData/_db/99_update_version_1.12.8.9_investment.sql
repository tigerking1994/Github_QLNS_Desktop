/****** Object:  StoredProcedure [dbo].[sp_vdt_khlcnt_get_chiphi_by_chungtu]    Script Date: 13/06/2023 12:02:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_khlcnt_get_chiphi_by_chungtu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_khlcnt_get_chiphi_by_chungtu]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_khlcnt_get_all_chiphi_by_loaicancu]    Script Date: 13/06/2023 12:02:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_khlcnt_get_all_chiphi_by_loaicancu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_khlcnt_get_all_chiphi_by_loaicancu]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_getall_hangmuc_by_duan_detail]    Script Date: 13/06/2023 12:02:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_getall_hangmuc_by_duan_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_getall_hangmuc_by_duan_detail]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_kehoachvon_capphatthanhtoan]    Script Date: 13/06/2023 12:02:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_kehoachvon_capphatthanhtoan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_kehoachvon_capphatthanhtoan]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_duan_kehoachvonungdx_detail]    Script Date: 13/06/2023 12:02:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_duan_kehoachvonungdx_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_duan_kehoachvonungdx_detail]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_find_phanbovondonvichitietpheduyet]    Script Date: 13/06/2023 12:02:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_find_phanbovondonvichitietpheduyet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_find_phanbovondonvichitietpheduyet]
GO
/****** Object:  StoredProcedure [dbo].[sp_dm_cdt]    Script Date: 13/06/2023 12:02:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dm_cdt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dm_cdt]
GO
/****** Object:  StoredProcedure [dbo].[sp_dm_cdt]    Script Date: 13/06/2023 12:02:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_dm_cdt]
	-- Add the parameters for the stored procedure here
	@YearOfWork int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	with cte as (
      select *, convert(varchar(max),iID_MaDonVi) as path
      from DM_ChuDauTu
      where iID_DonViCha is null 
	 -- where iID_DonViCha is null and iNamLamViec = @YearOfWork
      union all
      select t.*, convert(varchar(max),concat(path, '-', t.iID_MaDonVi))
      from cte join
           DM_ChuDauTu t
           on cte.iID_DonVi = t.iID_DonViCha 
		   --on cte.iID_DonVi = t.iID_DonViCha and cte.iNamLamViec = @YearOfWork and t.iNamLamViec = @YearOfWork
     )
	select cte.iID_DonVi,cte.iID_DonViCha, cte.sMoTa,
	cte.iID_MaDonVi, cte.sTenDonVi, cte.sKyHieu, cte.loai,cte.iNamLamViec, cte.itrangthai, cte.dNgayTao, cte.sNguoiTao, 
	cte.dNgaySua, cte.sNguoiSua, cte.bhangcha, cte.STKTrongNuoc, cte.ChiNhanhTrongNuoc, cte.STKNuocNgoai, cte.ChiNhanhNuocNgoai,
	cte.MaSoDVSDNS
	from cte
	order by path;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_find_phanbovondonvichitietpheduyet]    Script Date: 13/06/2023 12:02:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_vdt_find_phanbovondonvichitietpheduyet]
@iIdPhanBoVon uniqueidentifier,
@dNgayLap datetime
AS
BEGIN
	select 
		distinct
		--pbvct.Id,
		da.iID_DuAnID,
		--pbvct.iID_PhanBoVonID,
		da.sTenDuAn,
		da.sMaDuAn,
		da.sTrangThaiDuAn,
		da.sKhoiCong,
		da.sKetThuc,
		da.sMaKetNoi,
		da.iID_MaDonViThucHienDuAnID as IIdMaDonViThucHienDuAn,
		dv.sTenDonVi as STenDonViThucHienDuAn,
		lct.sTenLoaiCongTrinh,
		'' as sTenCapPheDuyet,
		cast(0 as float) as fGiaTriDauTu,
		pbvct.iID_LoaiCongTrinh as iID_LoaiCongTrinhID,
		null as iID_CapPheDuyetID,
		'' as sLNS,
		'' as sL,
		'' as sK,
		'' as sM,
		'' as sTM,
		'' as sTTM,
		'' as sNG,
		cast(0 as float) as fVonDaBoTri,
		cast(0 as float) as fVonConLai,
		cast(0 as float) as fChiTieuBoXungTrongNam,
		cast(0 as float) as fNamTruocChuyenSang,
		cast(0 as float) as fThuUngXDCB,
		cast(0 as float) as fChiTieuNganSach,
		pbvct.sGhiChu as sGhiChu,
		cast(0 as float) as fChiTieuGoc,
		cast(0 as float) as FCapPhatTaiKhoBac,
		cast(0 as float) as FCapPhatBangLenhChi,
		cast(0 as float) as FGiaTriThuHoiNamTruocKhoBac,
		cast(0 as float) as FGiaTriThuHoiNamTruocLenhChi,
		pbvct.fGiaTriThuHoi as FGiaTriThuHoi,
		case when pbvct.fGiaTrPhanBo is not null then pbvct.fGiaTrPhanBo else pbvdxct.fThanhToan end as FGiaTrPhanBo,
		--isnull(pbvct.Id, NEWID()) as IIdPhanBoVonId,
		pbvct.iID_PhanBoVon_DonVi_PheDuyet_ID as IIdPhanBoVon,
		pbvct.ILoaiDuAn as ILoaiDuAn,
		pbv.Id as IdChungTu,
		pbv.iID_ParentId as IdChungTuParent,
		pbv.bActive as BActive,
		pbv.bIsGoc as IsGoc,
		--case when pbvct.fThanhToanDeXuat is not null then pbvct.fThanhToanDeXuat else pbvdxct.fThanhToan end as fThanhToanDeXuat
		pbvdxct.fThanhToan as fThanhToanDeXuat,
		pbvct.iID_DuAn_HangMucID
	--from
	--	VDT_KHV_PhanBoVon_DonVi_ChiTiet_PheDuyet pbvct
	--inner join
	--	VDT_KHV_PhanBoVon_DonVi_PheDuyet pbv
	--on pbvct.iID_PhanBoVon_DonVi_PheDuyet_ID = pbv.Id
	--inner join 
	--VDT_KHV_PhanBoVon_DonVi_ChiTiet pbvdxct on pbvdxct.iID_DuAn_HangMucID = pbvct.iID_DuAn_HangMucID
	from
		VDT_KHV_PhanBoVon_DonVi_ChiTiet_PheDuyet pbvct
	inner join
		VDT_KHV_PhanBoVon_DonVi_PheDuyet pbv 
	on pbvct.iID_PhanBoVon_DonVi_PheDuyet_ID = pbv.Id
	inner  join VDT_KHV_PhanBoVon_DonVi  khndvdx  on  pbv.iID_VonNamDeXuatID    = khndvdx.id

	inner join  VDT_KHV_PhanBoVon_DonVi_ChiTiet pbvdxct  on    pbvdxct.iId_PhanBoVon_DonVi =  khndvdx.id
	and pbvct.iID_DuAn_HangMucID = pbvdxct.iID_DuAn_HangMucID  
	left join
		VDT_DA_DuAn da
	on
		pbvct.iID_DuAnID = da.iID_DuAnID
	left join
		VDT_DM_LoaiCongTrinh lct
	on
		pbvct.iID_LoaiCongTrinh = lct.iID_LoaiCongTrinh
	left join
		VDT_DM_DonViThucHienDuAn dv
	on da.iID_MaDonViThucHienDuAnID = dv.iID_MaDonVi
	where
		pbvct.iID_PhanBoVon_DonVi_PheDuyet_ID = @iIdPhanBoVon
		and pbv.bIsGoc = 1 --and pbvdxct.bActive = 1
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_duan_kehoachvonungdx_detail]    Script Date: 13/06/2023 12:02:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_get_duan_kehoachvonungdx_detail]
@sTongHop nvarchar(1200),
@iIdDonViQuanLy nvarchar(50),
@dNgayLap date
AS
BEGIN
	select * into #lstDonVi from f_recursive_donvi(@iIdDonViQuanLy);
	IF(ISNULL(@sTongHop, '') = '')
	BEGIN
		SELECT da.iID_DuAnID, SUM(ISNULL(tbl.fTongMucDauTuPheDuyet,0)) as fTongMucDauTuPheDuyet,th.iID_NguonVonID  INTO #tmp
		--SELECT da.iID_DuAnID, SUM(ISNULL(tbl.fTongMucDauTuPheDuyet,0)) as fTongMucDauTuPheDuyet  INTO #tmp
		FROM VDT_DA_QDDauTu as tbl
		INNER JOIN VDT_DA_DuAn as da on tbl.iID_DuAnID = da.iID_DuAnID AND da.iID_MaDonViThucHienDuAnID in (select * from #lstDonVi)
		INNER JOIN VDT_KHV_KeHoach5Nam_ChiTiet as th on tbl.iID_DuAnID = th.iID_DuAnID
		WHERE tbl.bActive = 1 AND CAST(tbl.dNgayQuyetDinh as DATE) <= CAST(@dNgayLap as DATE)
		GROUP BY da.iID_DuAnID,th.iID_NguonVonID
		--GROUP BY da.iID_DuAnID

		SELECT tbl.*, da.sTenDuAn, da.sMaKetNoi, da.sTrangThaiDuAn as sTrangThaiDuAnDangKy,da.sMaDuAn,
				da.iID_DonViTienTeID, da.iID_TienTeID, da.fTiGia, da.fTiGiaDonVi, null as sGhiChu , 
				null as iID_DonViQuanLyID, null as iID_MaDonViQuanLy, NULL as sTenDonVi,
				CAST(0 as float) as fGiaTriDeNghi
		FROM #tmp as tbl 
		INNER JOIN VDT_DA_DuAn as da on tbl.iID_DuAnID = da.iID_DuAnID AND da.iID_MaDonViThucHienDuAnID in (select * from #lstDonVi)

		DROP TABLE #tmp
	END
	ELSE
	BEGIN
		SELECT Item INTO #tmpChungTu
		FROM f_split(@sTongHop)

		SELECT dt.iID_DuAnID, tbl.iID_DonViQuanLyID, tbl.iID_MaDonViQuanLy, SUM(ISNULL(fGiaTriDeNghi, 0)) as fGiaTriDeNghi INTO #tmpTH
		FROM #tmpChungTu as tmp
		INNER JOIN VDT_KHV_KeHoachVonUng_DX as tbl on tmp.Item = tbl.sSoDeNghi
		INNER JOIN VDT_KHV_KeHoachVonUng_DX_ChiTiet as dt on tbl.Id = dt.iID_KeHoachUngID
		GROUP BY dt.iID_DuAnID, tbl.iID_DonViQuanLyID, tbl.iID_MaDonViQuanLy

		SELECT tmp.iID_DuAnID, SUM(ISNULL(dt.fTongMucDauTuPheDuyet,0)) as fTongMucDauTuPheDuyet,th.iID_NguonVonID INTO #tmpQDDT
		--SELECT tmp.iID_DuAnID, SUM(ISNULL(dt.fTongMucDauTuPheDuyet,0)) as fTongMucDauTuPheDuyet, INTO #tmpQDDT
		FROM #tmpTH as tmp
		LEFT JOIN VDT_DA_QDDauTu as dt on tmp.iID_DuAnID = dt.iID_DuAnID
		LEFT JOIN VDT_KHV_KeHoach5Nam_ChiTiet as th on tmp.iID_DuAnID = th.iID_DuAnID
		WHERE dt.bActive = 1 AND dt.iID_DuAnID IS NOT NULL AND th.iID_DuAnID IS NOT NULL
		GROUP BY tmp.iID_DuAnID,th.iID_NguonVonID
		--GROUP BY da.iID_DuAnID

		SELECT da.sMaDuAn, da.sTenDuAn, tmp.iID_DuAnID, da.sTrangThaiDuAn as sTrangThaiDuAnDangKy, ISNULL(qd.fTongMucDauTuPheDuyet, 0) as fTongMucDauTuPheDuyet,
			tmp.fGiaTriDeNghi, da.iID_DonViTienTeID, da.iID_TienTeID, da.fTiGiaDonVi, da.fTiGia, NULL as sGhiChu, 
			dv.sTenDonVi, tmp.iID_DonViQuanLyID, tmp.iID_MaDonViQuanLy
		FROM #tmpTH as tmp
		INNER JOIN VDT_DA_DuAn as da on tmp.iID_DuAnID = da.iID_DuAnID
		LEFT JOIN #tmpQDDT as qd on tmp.iID_DuAnID = qd.iID_DuAnID
		INNER JOIN DonVi as dv on tmp.iID_DonViQuanLyID = dv.iID_DonVi

		DROP TABLE #tmpTH
		DROP TABLE #tmpChungTu
		DROP TABLE #lstDonVi
	END
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_kehoachvon_capphatthanhtoan]    Script Date: 13/06/2023 12:02:38 PM ******/
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
			else (SELECT top 1 fGiaTriUng from VDT_KHV_KeHoachVonUng_ChiTiet khvu_dd where khvu_dd.iID_KeHoachUngID=#tmp.Id) end		
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
			else (SELECT top 1 fGiaTriUng from VDT_KHV_KeHoachVonUng_ChiTiet khvu_dd where khvu_dd.iID_KeHoachUngID=#tmp.Id) end			
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

GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_getall_hangmuc_by_duan_detail]    Script Date: 13/06/2023 12:02:38 PM ******/
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
			--null as LoaiCongTrinhId, -- Add by Anhnh156 08-06-2023
			lct.iID_LoaiCongTrinh as LoaiCongTrinhId, -- Add by Anhnh156 08-06-2023, to get LoaiCongTrinhId
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
/****** Object:  StoredProcedure [dbo].[sp_vdt_khlcnt_get_all_chiphi_by_loaicancu]    Script Date: 13/06/2023 12:02:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_khlcnt_get_all_chiphi_by_loaicancu]
@iIds t_tbl_uniqueidentifier READONLY,
@sLoaiCanCu nvarchar(1)
AS
BEGIN
	CREATE TABLE #tmp(IIdCanCuId uniqueidentifier, SNoiDung nvarchar(250), FGiaTriPheDuyet float, ILoai int, Id uniqueidentifier, IIdParentId uniqueidentifier, IsHangCha bit default(0),iThutu int)
	IF(@sLoaiCanCu = '1')
	BEGIN
		INSERT INTO #tmp(IIdCanCuId, SNoiDung, FGiaTriPheDuyet, ILoai, Id, IIdParentId,iThutu)
		SELECT tbl.Id as IIdCanCuId, cp.sTenChiPhi as SNoiDung, dt.fTienPheDuyet as FGiaTriPheDuyet, CAST(2 as int) as ILoai, cp.iID_DuAn_ChiPhi, cp.iID_ChiPhi_Parent,cp.iThuTu
		FROM @iIds as tbl
		INNER JOIN VDT_DA_DuToan_ChiPhi as dt on tbl.Id = dt.iID_DuToanID
		INNER JOIN VDT_DM_DuAn_ChiPhi as cp on dt.iID_DuAn_ChiPhi = cp.iID_DuAn_ChiPhi
	END
	ELSE IF(@sLoaiCanCu = '2')
	BEGIN
		INSERT INTO #tmp(IIdCanCuId, SNoiDung, FGiaTriPheDuyet, ILoai, Id, IIdParentId,iThutu)
		SELECT tbl.Id as IIdCanCuId, cp.sTenChiPhi as SNoiDung, dt.fTienPheDuyet as FGiaTriPheDuyet, CAST(2 as int) as ILoai, cp.iID_DuAn_ChiPhi, cp.iID_ChiPhi_Parent,cp.iThuTu
		FROM @iIds as tbl
		INNER JOIN VDT_DA_QDDauTu_ChiPhi as dt on tbl.Id = dt.iID_QDDauTuID
		INNER JOIN VDT_DM_DuAn_ChiPhi as cp on dt.iID_DuAn_ChiPhi = cp.iID_DuAn_ChiPhi
	END
	ELSE
	BEGIN
		INSERT INTO #tmp(IIdCanCuId, SNoiDung, FGiaTriPheDuyet, ILoai, Id, IsHangCha,iThutu)
		SELECT tbl.Id as IIdCanCuId, cp.sTenChiPhi as SNoiDung, CAST(0 as float) as FGiaTriPheDuyet,  CAST(2 as int) as ILoai, cp.iID_ChiPhi, CAST(0 as bit),cp.iThuTu
		FROM @iIds as tbl, VDT_DM_ChiPhi as cp 
	END

	update tmp
	SET
		IsHangCha = CAST(1 as bit)
	FROM #tmp as tmp
	INNER JOIN #tmp as cd on tmp.Id = cd.IIdParentId

	SELECT * FROM #tmp
	order by iThuTu

	DROP TABLE #tmp
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_khlcnt_get_chiphi_by_chungtu]    Script Date: 13/06/2023 12:02:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_khlcnt_get_chiphi_by_chungtu]
@iIdChungTu uniqueidentifier,
@sLoaiChungTu nvarchar(1),
@bIsAdd bit
AS
BEGIN
	-- DuToan
	IF(@sLoaiChungTu = '1')
	BEGIN
		SELECT CAST(0 as bit) as IsChecked, null as IIdGoiThauId, NULL as IIdNguonVonId, cp.iID_ChiPhi as IIdChiPhiGocId, tbl.iID_DuAn_ChiPhi as IIdChiPhiId, null as IIdHangMucId, cp.iID_ChiPhi_Parent as IIdParentId,
				CAST(0 as bit) as IsHangCha, cp.sTenChiPhi as SNoiDung, null as SMaOrder, tbl.fTienPheDuyet as FGiaTriDuocDuyet, CAST(0 as float) as FGiaTriGoiThau,cp.iThuTu
		FROM VDT_DA_DuToan_ChiPhi as tbl
		INNER JOIN VDT_DM_DuAn_ChiPhi as cp on tbl.iID_DuAn_ChiPhi = cp.iID_DuAn_ChiPhi
		WHERE tbl.iID_DuToanID = @iIdChungTu
		Order by cp.iThuTu
	END
	-- QdDauTu
	ELSE IF(@sLoaiChungTu = '2')
	BEGIN
		SELECT CAST(0 as bit) as IsChecked, null as IIdGoiThauId, NULL as IIdNguonVonId, cp.iID_ChiPhi as IIdChiPhiGocId, tbl.iID_DuAn_ChiPhi as IIdChiPhiId, null as IIdHangMucId, cp.iID_ChiPhi_Parent as IIdParentId,
				CAST(0 as bit) as IsHangCha, cp.sTenChiPhi as SNoiDung, null as SMaOrder, tbl.fTienPheDuyet as FGiaTriDuocDuyet, CAST(0 as float) as FGiaTriGoiThau,cp.iThuTu
		FROM VDT_DA_QDDauTu_ChiPhi as tbl
		INNER JOIN VDT_DM_DuAn_ChiPhi as cp on tbl.iID_DuAn_ChiPhi = cp.iID_DuAn_ChiPhi
		WHERE tbl.iID_QDDauTuID = @iIdChungTu
		Order by cp.iThuTu
	END
	-- Chu truong dau tu
	ELSE
	BEGIN
		IF(@bIsAdd = 1)
		BEGIN
			CREATE TABLE #tmp(iIdDuAnChiPhiId uniqueidentifier, iIdChiPhiGocId uniqueidentifier, sTenChiPhi nvarchar(250))
			INSERT INTO VDT_DM_DuAn_ChiPhi(iID_DuAn_ChiPhi, sTenChiPhi, iThuTu, dNgayTao, iID_ChiPhi)
			OUTPUT inserted.iID_DuAn_ChiPhi, inserted.iID_ChiPhi, inserted.sTenChiPhi INTO #tmp(iIdDuAnChiPhiId, iIdChiPhiGocId, sTenChiPhi)
			SELECT NEWID(), cp.sTenChiPhi, iThuTu, GETDATE(), cp.iID_ChiPhi
			FROM VDT_DM_ChiPhi as cp 

			SELECT CAST(0 as bit) as IsChecked, null as IIdGoiThauId, NULL as IIdNguonVonId, cp.iIdChiPhiGocId as IIdChiPhiGocId, cp.iIdDuAnChiPhiId as IIdChiPhiId, null as IIdHangMucId, NULL as IIdParentId,
				CAST(0 as bit) as IsHangCha, cp.sTenChiPhi as SNoiDung, null as SMaOrder, CAST(0 as float) as FGiaTriDuocDuyet, CAST(0 as float) as FGiaTriGoiThau
			FROM #tmp as cp

			DROP TABLE #tmp
		END
		ELSE
		BEGIN
			SELECT CAST(0 as bit) as IsChecked, null as IIdGoiThauId, NULL as IIdNguonVonId, iID_ChiPhi as IIdChiPhiGocId, tbl.iID_ChiPhi as IIdChiPhiId, null as IIdHangMucId, tbl.iID_ChiPhi_Parent as IIdParentId,
				CAST(0 as bit) as IsHangCha, sTenChiPhi as SNoiDung, null as SMaOrder, CAST(0 as float) as FGiaTriDuocDuyet, CAST(0 as float) as FGiaTriGoiThau,tbl.iThuTu
			FROM VDT_DM_ChiPhi as tbl
			Order by tbl.iThuTu
		END
	END
END
;
;
GO
