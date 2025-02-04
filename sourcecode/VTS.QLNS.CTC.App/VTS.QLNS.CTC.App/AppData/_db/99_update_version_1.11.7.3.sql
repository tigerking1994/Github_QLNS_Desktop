/****** Object:  StoredProcedure [dbo].[sp_vdt_denghithanhtoan_index_2]    Script Date: 23/08/2022 5:44:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_denghithanhtoan_index_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_denghithanhtoan_index_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_qddt_khlcnhathau_index]    Script Date: 23/08/2022 5:44:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qddt_khlcnhathau_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qddt_khlcnhathau_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_get_lns1]    Script Date: 23/08/2022 5:44:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_rpt_get_lns1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_rpt_get_lns1]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_get_lns1]    Script Date: 23/08/2022 5:44:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_rpt_get_lns1]
	 @NamLamViec int,
	 @ChungTuId nvarchar(MAX),
	 @NgayQuyetDinh datetime,
	 @Type int
AS
BEGIN 
	SET NOCOUNT ON;
	if(@Type = 1)
	begin
		select 
		*
		from NS_MucLucNganSach
		where iID_MLNS IN (
		select
			mlns.iID_MLNS
		from 
			NS_DT_ChungTuChiTiet as chitiet
		inner join
			NS_MucLucNganSach as mlns
		on chitiet.sLNS = mlns.sLNS
		where
			iID_DTChungTu	IN (
				select
					iID_DTChungTu
				from 
					NS_DT_ChungTu
				where 
					convert(nvarchar(max), iID_DTChungTu) IN (select * from dbo.splitstring(@ChungTuId))
					AND dNgayQuyetDinh <= dateadd(dd,-1,(dateadd(mm,1,@NgayQuyetDinh)))
					AND 
					sDSLNS <> ''
			) 
				AND (chitiet.fTuChi <> 0 OR chitiet.fHienVat <> 0 OR chitiet.fDuPhong <> 0 OR chitiet.fHangMua <> 0 OR chitiet.fHangNhap <> 0 OR chitiet.fPhanCap <> 0)
				AND mlns.sL = '' 
			) AND iNamLamViec = @NamLamViec
	end
	else if(@Type = 4)
	begin
		select 
		*
		from NS_MucLucNganSach
		where iID_MLNS IN (
		select
			mlns.iID_MLNS
		from 
			NS_DT_ChungTuChiTiet as chitiet
		inner join
			NS_MucLucNganSach as mlns
		on chitiet.sLNS = mlns.sLNS
		where
			iID_DTChungTu	IN (
				select
					iID_DTChungTu
				from 
					NS_DT_ChungTu
				where 
					convert(nvarchar(max), iID_DTChungTu) IN (select * from dbo.splitstring(@ChungTuId))
					AND dNgayQuyetDinh <= dateadd(dd,-1,(dateadd(mm,1,@NgayQuyetDinh)))
					AND sDSLNS <> ''
			) 
				AND (chitiet.fTuChi <> 0)
				AND mlns.sL = '' 
			) AND iNamLamViec = @NamLamViec
	end
	else if(@Type = 5)
	begin
		select 
		*
		from NS_MucLucNganSach
		where iID_MLNS IN (
		select
			mlns.iID_MLNS
		from 
			NS_DT_ChungTuChiTiet as chitiet
		inner join
			NS_MucLucNganSach as mlns
		on chitiet.sLNS = mlns.sLNS
		where
			iID_DTChungTu	IN (
				select
					iID_DTChungTu
				from 
					NS_DT_ChungTu
				where 
					convert(nvarchar(max), iID_DTChungTu) IN (select * from dbo.splitstring(@ChungTuId))
					AND dNgayQuyetDinh <= dateadd(dd,-1,(dateadd(mm,1,@NgayQuyetDinh)))
					AND sDSLNS <> ''
			) 
				AND (chitiet.fHienVat <> 0)
				AND mlns.sL = '' 
			) AND iNamLamViec = @NamLamViec
	end
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qddt_khlcnhathau_index]    Script Date: 23/08/2022 5:44:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec sp_qddt_khlcnhathau_index

CREATE PROC [dbo].[sp_qddt_khlcnhathau_index]
AS
BEGIN

	WITH SoLieuDieuChinh AS 
	 (
		SELECT 
			ct.Id, ct.iID_ParentId
		FROM 
			VDT_QDDT_KHLCNhaThau ct 
		WHERE 
			ct.iID_ParentId is not null

		UNION ALL

		SELECT 
			ct.Id, ct.iID_ParentId
		FROM 
			VDT_QDDT_KHLCNhaThau ct JOIN SoLieuDieuChinh ctpr ON ct.iID_ParentId = ctpr.Id
		WHERE ct.iID_ParentId is not null
	  ),SoLanDieuChinh AS (
		   SELECT
			sdc.Id,sdc.iID_ParentId,  COUNT(sdc.Id) AS iSoLanDieuChinh
		  FROM 
			SoLieuDieuChinh sdc
		  GROUP BY sdc.iID_ParentId,sdc.Id
	  )


	SELECT
		tbl.Id,
		CAST(ROW_NUMBER() OVER(ORDER BY tbl.dDateCreate DESC) as int) as IRowIndex,
		tbl.iID_DuToanID AS IIdDuToanId,
		tbl.iID_QDDauTuID as IIdQdDauTuId,
		tbl.iID_ChuTruongDauTuID as IIdChuTruongDauTuId,
		tbl.sSoQuyetDinh AS SSoQuyetDinh,
		tbl.dNgayQuyetDinh AS DNgayQuyetDinh,
		da.iID_DonViThucHienDuAnID AS IIdDonViQuanLyId,
		dv.sTenDonVi AS STenDonVi,
		da.iID_MaDonViThucHienDuAnID AS IIdMaDonViQuanLy,
		tbl.iID_DuAnID AS IIdDuAnId,
		da.sTenDuAn AS STenDuAn, 
		tbl.sMoTa AS SMoTa,
		tbl.sUserCreate as SUserCreate,
		tbl.bActive AS BActive,
		tbl.iID_ParentID AS IIdParentId,
		tbl.iID_LCNhaThauGocID AS IIdLcNhaThauGocId,
		ISNULL(tbl.bKhoa, 0) as BKhoa,
		CAST(isnull(dc.iSoLanDieuChinh,0) as int) AS ISoLanDieuChinh,
		CAST((SELECT COUNT(Id) FROM Attachment WHERE ModuleType = 207 AND ObjectId = tbl.Id) as int) AS ITotalFiles,
		ISNULL(tbl.bIsGoc, 0) AS BIsGoc
	FROM VDT_QDDT_KHLCNhaThau AS tbl
	LEFT JOIN VDT_DA_DuAn AS da ON tbl.iID_DuAnID = da.iID_DuAnID
	LEFT JOIN VDT_DM_DonViThucHienDuAn AS dv ON da.iID_MaDonViThucHienDuAnID = dv.iID_MaDonVi
	LEFT JOIN DM_ChuDauTu cdt ON cdt.iID_DonVi = da.iID_ChuDauTuID
	LEFT JOIN SoLanDieuChinh dc ON tbl.Id = dc.Id
	--WHERE tbl.bActive = 1
	ORDER BY tbl.dDateCreate desc
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_denghithanhtoan_index_2]    Script Date: 23/08/2022 5:44:26 PM ******/
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
		hd.sTenHopDong, pdtt.fGiaTriThanhToanTN as fGiaTriThanhToanTNDuocDuyet, pdtt.fGiaTriThanhToanNN as fGiaTriThanhToanNNDuocDuyet
	FROM VDT_TT_DeNghiThanhToan as tbl
	left join VDT_TT_PheDuyetThanhToan_ChiTiet pdtt on tbl.Id = pdtt.iID_DeNghiThanhToanID
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
;
GO
