IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_insert_phanbovonchitiet_new]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_insert_phanbovonchitiet_new]
GO
/****** Object:  UserDefinedTableType [dbo].[t_tbl_phanbovonchitiet7]    Script Date: 10/21/2024 5:03:54 PM ******/
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N't_tbl_phanbovonchitiet7' AND ss.name = N'dbo')
DROP TYPE [dbo].[t_tbl_phanbovonchitiet7]
GO

/****** Object:  UserDefinedTableType [dbo].[t_tbl_phanbovonchitiet7]    Script Date: 10/21/2024 5:03:54 PM ******/
CREATE TYPE [dbo].[t_tbl_phanbovonchitiet7] AS TABLE(
	[iID_PhanBoVonID] [uniqueidentifier] NULL,
	[iID_DuAnID] [uniqueidentifier] NULL,
	[sTrangThaiDuAnDangKy] [nvarchar](200) NULL,
	[fGiaTrDeNghi] [float] NULL,
	[fGiaTrPhanBo] [float] NULL,
	[fGiaTriThuHoi] [float] NULL,
	[iID_DonViTienTeID] [uniqueidentifier] NULL,
	[iID_TienTeID] [uniqueidentifier] NULL,
	[fTiGiaDonVi] [float] NULL,
	[fTiGia] [float] NULL,
	[sGhiChu] [nvarchar](max) NULL,
	[iID_LoaiNguonVonID] [uniqueidentifier] NULL,
	[sLNS] [nvarchar](50) NULL,
	[sL] [nvarchar](50) NULL,
	[sK] [nvarchar](50) NULL,
	[sM] [nvarchar](50) NULL,
	[sTM] [nvarchar](50) NULL,
	[sTTM] [nvarchar](50) NULL,
	[sNG] [nvarchar](50) NULL,
	[sXauNoiMa] [nvarchar](100) NULL,
	[fCapPhatTaiKhoBac] [float] NULL,
	[fCapPhatBangLenhChi] [float] NULL,
	[fTonKhoanTaiDonVi] [float] NULL,
	[fGiaTriThuHoiNamTruocKhoBac] [float] NULL,
	[fGiaTriThuHoiNamTruocLenhChi] [float] NULL,
	[fCapPhatTaiKhoBacDc] [float] NULL,
	[fCapPhatBangLenhChiDc] [float] NULL,
	[fTonKhoanTaiDonViDc] [float] NULL,
	[fGiaTriThuHoiNamTruocKhoBacDc] [float] NULL,
	[fGiaTriThuHoiNamTruocLenhChiDc] [float] NULL,
	[iID_Parent] [uniqueidentifier] NULL,
	[ILoaiDuAn] [int] NULL,
	[IIdLoaiCongTrinh] [uniqueidentifier] NULL,
	[fThanhToanDeXuat] [float] NULL,
	[iID_DuAn_HangMucID] [uniqueidentifier] NULL
)
GO


CREATE PROC [dbo].[sp_vdt_insert_phanbovonchitiet_new]
@bIsEdit bit,
@sUserLogin nvarchar(100),
@tbl_PhanBoVonChiTiet t_tbl_phanbovonchitiet7 READONLY,
@sTypeError int OUTPUT
AS
BEGIN
set @sTypeError = 0
DECLARE @iIdPhanBoVon uniqueidentifier = (SELECT TOP(1) iID_PhanBoVonID FROM @tbl_PhanBoVonChiTiet)
DECLARE @iNamKeHoach int = (SELECT TOP(1)iNamKeHoach FROM VDT_KHV_PhanBoVon WHERE id = @iIdPhanBoVon)

--DECLARE @lstIdParent nvarchar(max);


DECLARE @iCountPass int = (SELECT COUNT(tbl.sLNS)
FROM @tbl_PhanBoVonChiTiet as tbl
INNER JOIN NS_MucLucNganSach as ml on ml.sLNS = tbl.sLNS
AND ISNULL(tbl.sL, '') = ml.sL
AND ISNULL(tbl.sK, '') = ml.sK
AND ISNULL(tbl.sM, '')= ml.sM
AND ISNULL(tbl.sTM, '') = ml.sTM
--AND ISNULL(tbl.sTTM, '') = ml.sTTM
--AND ISNULL(tbl.sNG, '') = ml.sNG
AND ml.iNamLamViec = @iNamKeHoach
--AND ml.sXauNoiMa = (tbl.sLNS +'-' + tbl.sL+'-' + tbl.sK+'-' + tbl.sM+'-' + tbl.sTM)
AND ml.sXauNoiMa = tbl.sXauNoiMa

)
DECLARE @iCountInput int = (SELECT COUNT(*)
FROM @tbl_PhanBoVonChiTiet )

IF(@iCountPass < @iCountInput)
BEGIN
SET @sTypeError = 1
RETURN
END

IF(@bIsEdit = 1)
BEGIN
DELETE VDT_KHV_PhanBoVon_ChiTiet WHERE iID_PhanBoVonID = @iIdPhanBoVon
--select @lstIdParent = (cast(pbvct1.iId_Parent as nvarchar(1000)) + ',') from VDT_KHV_PhanBoVon_ChiTiet pbvct1 WHERE iID_PhanBoVonID = @iIdPhanBoVon and pbvct1.iId_Parent is not null
--delete VDT_KHV_PhanBoVon_ChiTiet where iID_PhanBoVonID in (select * from dbo.splitstring(@lstIdParent))
END

INSERT INTO VDT_KHV_PhanBoVon_ChiTiet(Id, iID_PhanBoVonID, iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, sTrangThaiDuAnDangKy, fGiaTrDeNghi, fGiaTrPhanBo, fGiaTriThuHoi,
--new--
fCapPhatTaiKhoBac, fCapPhatBangLenhChi, fTonKhoanTaiDonVi, fGiaTriThuHoiNamTruocKhoBac, fGiaTriThuHoiNamTruocLenhChi, fThanhToanDeXuat,
iID_DonViTienTeID, iID_TienTeID, fTiGiaDonVi, fTiGia, sGhiChu, LNS, L, K, M, TM, TTM, NG, fCapPhatTaiKhoBacDc, fCapPhatBangLenhChiDc, fTonKhoanTaiDonViDC, fGiaTriThuHoiNamTruocKhoBacDc,
fGiaTriThuHoiNamTruocLenhChiDc, iId_Parent, bActive, ILoaiDuAn, iID_LoaiCongTrinh, iID_DuAn_HangMucID)
SELECT NEWID(), iID_PhanBoVonID, iID_DuAnID,
(CASE WHEN ISNULL(tbl.sTM, '') = '' AND ISNULL(tbl.sTTM, '') = '' AND ISNULL(tbl.sNG, '') = '' THEN ml.iID ELSE NULL END) as iID_MucID,
(CASE WHEN ISNULL(tbl.sTM, '') <> '' AND ISNULL(tbl.sTTM, '') = '' AND ISNULL(tbl.sNG, '') = '' THEN ml.iID ELSE NULL END) as iID_TieuMucID,
(CASE WHEN ISNULL(tbl.sTM, '') <> '' AND ISNULL(tbl.sTTM, '') <> '' AND ISNULL(tbl.sNG, '') = '' THEN ml.iID ELSE NULL END) as iID_TietMucID,
(CASE WHEN ISNULL(tbl.sTM, '') <> '' AND ISNULL(tbl.sTTM, '') <> '' AND ISNULL(tbl.sNG, '') <> '' THEN ml.iID ELSE NULL END) as iID_NganhID,
sTrangThaiDuAnDangKy, fGiaTrDeNghi, fGiaTrPhanBo, fGiaTriThuHoi,
--new--
fCapPhatTaiKhoBac, fCapPhatBangLenhChi,fTonKhoanTaiDonVi, fGiaTriThuHoiNamTruocKhoBac, fGiaTriThuHoiNamTruocLenhChi, fThanhToanDeXuat,
iID_DonViTienTeID, iID_TienTeID, fTiGiaDonVi, fTiGia, sGhiChu,
tbl.sLNS, tbl.sL, tbl.sK, tbl.sM, tbl.sTM, tbl.sTTM, tbl.sNG,
tbl.fCapPhatTaiKhoBacDc, tbl.fCapPhatBangLenhChiDc, tbl.fTonKhoanTaiDonViDc, tbl.fGiaTriThuHoiNamTruocKhoBacDc, tbl.fGiaTriThuHoiNamTruocLenhChiDc, tbl.iID_Parent, 1, tbl.ILoaiDuAn, tbl.IIdLoaiCongTrinh, tbl.IID_DuAn_HangMucID
FROM @tbl_PhanBoVonChiTiet as tbl
INNER JOIN NS_MucLucNganSach as ml on ml.sLNS = tbl.sLNS
AND ISNULL(TBL.SL, '') = ML.SL
AND ISNULL(TBL.SK, '') = ML.SK
AND ISNULL(TBL.SM, '')= ML.SM
AND ISNULL(TBL.STM, '') = ML.STM
--AND ISNULL(tbl.sTTM, '') = ml.sTTM
-- AND ISNULL(tbl.sNG, '') = ml.sNG
AND ml.iNamLamViec = @iNamKeHoach
--AND ml.sXauNoiMa = (tbl.sLNS +'-' + tbl.sL+'-' + tbl.sK+'-' + tbl.sM+'-' + tbl.sTM)
AND ml.sXauNoiMa = tbl.sXauNoiMa



DECLARE @fGiaTriDeNghi float = (SELECT SUM(fGiaTrDeNghi) FROM @tbl_PhanBoVonChiTiet)
DECLARE @fGiaTriThuHoi float = (SELECT SUM(fGiaTriThuHoi) FROM @tbl_PhanBoVonChiTiet)
DECLARE @fGiaTrPhanBo float = (SELECT SUM(fGiaTrPhanBo) FROM @tbl_PhanBoVonChiTiet)
DECLARE @fCapPhatTaiKhoBac float = (SELECT SUM(fCapPhatTaiKhoBac) FROM @tbl_PhanBoVonChiTiet)
DECLARE @fCapPhatTaiKhoBacDC float = (SELECT SUM(fCapPhatTaiKhoBacDc) FROM @tbl_PhanBoVonChiTiet)
DECLARE @fCapPhatBangLenhChi float = (SELECT SUM(fCapPhatBangLenhChi) FROM @tbl_PhanBoVonChiTiet)
DECLARE @fCapPhatBangLenhChiDC float = (SELECT SUM(fCapPhatBangLenhChiDc) FROM @tbl_PhanBoVonChiTiet)
DECLARE @fTonKhoantaiDonVi float = (SELECT SUM(fTonKhoantaiDonVi) FROM @tbl_PhanBoVonChiTiet)
DECLARE @fTonKhoantaiDonViDC float = (SELECT SUM(fTonKhoanTaiDonViDc) FROM @tbl_PhanBoVonChiTiet)
DECLARE @fGiaTriThuHoiNamTruocKhoBac float = (SELECT SUM(fGiaTriThuHoiNamTruocKhoBac) FROM @tbl_PhanBoVonChiTiet)
DECLARE @fGiaTriThuHoiNamTruocLenhChi float = (SELECT SUM(fGiaTriThuHoiNamTruocLenhChi) FROM @tbl_PhanBoVonChiTiet)

SELECT @fGiaTriDeNghi = (@fGiaTriDeNghi + ISNULL(fGiaTrDeNghi, 0)),
@fGiaTriThuHoi = (@fGiaTriThuHoi + ISNULL(fGiaTriThuHoi, 0)),
@fGiaTrPhanBo = (@fGiaTrPhanBo + ISNULL(fGiaTrPhanBo, 0)),
@fCapPhatTaiKhoBac = (@fCapPhatTaiKhoBacDC - @fCapPhatTaiKhoBac  + ISNULL(fCapPhatTaiKhoBac, 0)),
@fCapPhatBangLenhChi = (@fCapPhatBangLenhChiDC - @fCapPhatBangLenhChi + ISNULL(fCapPhatBangLenhChi, 0)),
@fTonKhoantaiDonVi = (@fTonKhoantaiDonViDC - @fTonKhoantaiDonVi + ISNULL(@fTonKhoantaiDonVi, 0)),
@fGiaTriThuHoiNamTruocKhoBac = (@fGiaTriThuHoiNamTruocKhoBac + ISNULL(fGiaTriThuHoiNamTruocKhoBac, 0)),
@fGiaTriThuHoiNamTruocLenhChi = (@fGiaTriThuHoiNamTruocLenhChi + ISNULL(fGiaTriThuHoiNamTruocLenhChi, 0))

FROM VDT_KHV_PhanBoVon
WHERE Id = (SELECT iID_ParentId FROM VDT_KHV_PhanBoVon WHERE Id = @iIdPhanBoVon)

UPDATE VDT_KHV_PhanBoVon
set
fGiaTrDeNghi = @fGiaTriDeNghi,
fGiaTriThuHoi = @fGiaTriThuHoi,
fGiaTrPhanBo = @fGiaTrPhanBo,
fCapPhatTaiKhoBac = @fCapPhatTaiKhoBac,
fCapPhatBangLenhChi = @fCapPhatBangLenhChi,
fTonKhoantaiDonVi = @fTonKhoantaiDonVi,
fGiaTriThuHoiNamTruocKhoBac = @fGiaTriThuHoiNamTruocKhoBac,
fGiaTriThuHoiNamTruocLenhChi = @fGiaTriThuHoiNamTruocLenhChi
WHERE Id = @iIdPhanBoVon

UPDATE da
SET
sTrangThaiDuAn = 'THUC_HIEN'
FROM @tbl_PhanBoVonChiTiet as tbl
INNER JOIN VDT_DA_DuAn as da on tbl.iID_DuAnID = da.iID_DuAnID
WHERE da.sTrangThaiDuAn = 'KhoiTao'
END
;
;
;
;

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_duan_in_phanbovon_new_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_duan_in_phanbovon_new_2]

GO
CREATE PROCEDURE [dbo].[sp_vdt_get_duan_in_phanbovon_new_2]
	@idPhanBoVonDeXuat nvarchar(max),
	@nguonVonID int
AS
Begin
	select 
		distinct
		pbvdvct.iID_DuAnID,
		pbvdvct.iID_LoaiCongTrinh as iID_LoaiCongTrinh,
		pbvdvct.ILoaiDuAn as ILoaiDuAn,
		pbvdvct.iID_DuAn_HangMucID,
		--case when pbct.fThanhToanDeXuat = null then pbvdvct.fThanhToanDeXuat else pbct.fThanhToanDeXuat end as fThanhToanDeXuat
		pbvdvct.fThanhToanDeXuat as fThanhToanDeXuat
		into #tmp_duan
	from VDT_KHV_PhanBoVon_DonVi_ChiTiet_PheDuyet pbvdvct
	--INNER JOIN VDT_DA_DuAn as da on pbvdvct.iID_DuAnID = da.iID_DuAnID
	left join VDT_KHV_PhanBoVon_ChiTiet pbct on pbct.iID_DuAn_HangMucID = pbvdvct.iID_DuAn_HangMucID
	where
		pbvdvct.iID_PhanBoVon_DonVi_PheDuyet_ID in (select  * from dbo.splitstring(@idPhanBoVonDeXuat));

	select
		distinct
		null as IdChungTu,
		null as IdChungTuParent,
		cast(1 as bit) as BActive,
		cast(1 as bit) as IsGoc,
		da.iID_DuAnID as iID_DuAnID,
		da.sTenDuAn as sTenDuAn,
		da.sMaDuAn as sMaDuAn,
		da.sTrangThaiDuAn as sTrangThaiDuAn,
		da.sKhoiCong as sKhoiCong,
		da.sKetThuc as sKetThuc,
		lct.sTenLoaiCongTrinh as sTenLoaiCongTrinh,
		da.sMaKetNoi as sMaKetNoi,
		cda.sTen as sTenCapPheDuyet,
		cast(0 as float) as fGiaTriDauTu,
		tmp_da.iID_LoaiCongTrinh as iID_LoaiCongTrinhID,
		da.iID_CapPheDuyetID as iID_CapPheDuyetID,
		lct.LNS as sLNS,
		lct.L as sL,
		lct.K as sK,
		lct.M as sM,
		lct.TM as sTM,
		lct.TTM as sTTM,
		lct.NG as sNG,
		cast(0 as float) as fVonDaBoTri,
		cast(0 as float) as fVonConLai,
		cast(0 as float) as fChiTieuBoXungTrongNam,
		cast(0 as float) as fNamTruocChuyenSang,
		cast(0 as float) as fThuUngXDCB,
		cast(0 as float) as fChiTieuNganSach,
		'' as sGhiChu,
		cast(0 as float) as FCapPhatTaiKhoBac,
		cast(0 as float) as FCapPhatTaiKhoBacDC,
		cast(0 as float) as FCapPhatBangLenhChi,
		cast(0 as float) as FCapPhatBangLenhChiDC,
		cast(0 as float) as FTonKhoanTaiDonVi,
		cast(0 as float) as FTonKhoanTaiDonViDC,
		cast(0 as float) as FGiaTriThuHoiNamTruocKhoBac,
		cast(0 as float) as FGiaTriThuHoiNamTruocKhoBacDC,
		cast(0 as float) as FGiaTriThuHoiNamTruocLenhChi,
		cast(0 as float) as FGiaTriThuHoiNamTruocLenhChiDC,
		cast(0 as float) as fChiTieuGoc,
		tmp_da.ILoaiDuAn as ILoaiDuAn,
		dv.sTenDonVi as STenDonViThucHienDuAn,
		dv.iID_MaDonVi as IIdMaDonViThucHienDuAn,
		tmp_da.fThanhToanDeXuat as fThanhToanDeXuat,
		dahm.iID_DuAn_HangMucID
	from
		VDT_DA_DuAn da
	inner join
		#tmp_duan tmp_da
	on da.iID_DuAnID = tmp_da.iID_DuAnID
	left join 
		VDT_DA_DuAn_HangMuc dahm
	on tmp_da.iID_DuAn_HangMucID = dahm.iID_DuAn_HangMucID
	left join 
		VDT_DM_PhanCapDuAn cda 
	on da.iID_CapPheDuyetID = cda.iID_PhanCapID
	left join
		VDT_DM_LoaiCongTrinh lct
	on dahm.IdLoaiCongTrinh = lct.iID_LoaiCongTrinh
	left join
		VDT_DM_DonViThucHienDuAn dv
	on 
		da.iID_MaDonViThucHienDuAnID = dv.iID_MaDonVi
	drop table #tmp_duan

End
;
;
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_find_phanbovonchitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_find_phanbovonchitiet]

GO
CREATE PROC [dbo].[sp_vdt_find_phanbovonchitiet]
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
		pbvct.LNS as sLNS,
		pbvct.L as sL,
		pbvct.K as sK,
		pbvct.M as sM,
		pbvct.TM as sTM,
		pbvct.TTM as sTTM,
		pbvct.NG as sNG,
		cast(0 as float) as fVonDaBoTri,
		cast(0 as float) as fVonConLai,
		cast(0 as float) as fChiTieuBoXungTrongNam,
		cast(0 as float) as fNamTruocChuyenSang,
		cast(0 as float) as fThuUngXDCB,
		cast(0 as float) as fChiTieuNganSach,
		pbvct.sGhiChu as sGhiChu,
		cast(0 as float) as fChiTieuGoc,
		pbvct.fCapPhatTaiKhoBac as FCapPhatTaiKhoBac,
		pbvct.fCapPhatTaiKhoBacDC as FCapPhatTaiKhoBacDC,
		pbvct.fCapPhatBangLenhChi as FCapPhatBangLenhChi,
		pbvct.fCapPhatBangLenhChiDC as FCapPhatBangLenhChiDC,
		pbvct.fTonKhoanTaiDonVi as FTonKhoanTaiDonVi,
		pbvct.fTonKhoanTaiDonViDC as FTonKhoanTaiDonViDC,
		pbvct.fGiaTriThuHoiNamTruocKhoBac as FGiaTriThuHoiNamTruocKhoBac,
		pbvct.fGiaTriThuHoiNamTruocKhoBacDC as FGiaTriThuHoiNamTruocKhoBacDC,
		pbvct.fGiaTriThuHoiNamTruocLenhChi as FGiaTriThuHoiNamTruocLenhChi,
		pbvct.fGiaTriThuHoiNamTruocLenhChiDC as FGiaTriThuHoiNamTruocLenhChiDC,
		--isnull(pbvct.Id, NEWID()) as IIdPhanBoVonId,
		pbvct.iID_PhanBoVonID as IIdPhanBoVon,
		pbvct.ILoaiDuAn as ILoaiDuAn,
		pbv.Id as IdChungTu,
		pbv.iID_ParentId as IdChungTuParent,
		pbv.bActive as BActive,
		pbv.bIsGoc as IsGoc,
		pbvct.iID_DuAn_HangMucID
	from
		VDT_KHV_PhanBoVon_ChiTiet pbvct
	inner join
		VDT_KHV_PhanBoVon pbv
	on pbvct.iID_PhanBoVonID = pbv.Id
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
		pbvct.iID_PhanBoVonID = @iIdPhanBoVon
		--and pbv.bIsGoc = 1
END
;
;
;
;
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_phanbovon_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_phanbovon_index]

GO
CREATE PROC [dbo].[sp_vdt_phanbovon_index]
@iLoaiKeHoachVon int
AS
BEGIN
	-- số laand điều chỉnh
	WITH SoLieuDieuChinh AS 
	 (
		SELECT 
			ct.Id, ct.iID_ParentId, ct.sSoQuyetDinh, ct.iNamKeHoach
		FROM 
			VDT_KHV_PhanBoVon ct 
		WHERE 
			ct.iID_ParentId is not null

		UNION ALL

		SELECT 
			ct.Id, ct.iID_ParentId, ct.sSoQuyetDinh, ct.iNamKeHoach
		FROM 
			VDT_KHV_PhanBoVon ct JOIN SoLieuDieuChinh ctpr ON ct.iID_ParentId = ctpr.Id
		WHERE ct.iID_ParentId is not null
	  )

	   SELECT 
			sdc.iID_ParentId, sdc.iNamKeHoach, COUNT(sdc.Id) AS iSoLanDieuChinh
			INTO #tmpDieuChinh
		FROM 
		SoLieuDieuChinh sdc
		GROUP BY sdc.iID_ParentId, sdc.iNamKeHoach

	-- chỉ tiêu bổ xung, điều chỉnh
	SELECT iID_MaDonViQuanLy, iID_NguonVonID, iNamKeHoach, iID_LoaiNguonVonID ,(CASE WHEN @iLoaiKeHoachVon = 1 THEN SUM(ISNULL(dt.fGiaTrPhanBo,0)) ELSE SUM(ISNULL(dt.fGiaTrDeNghi,0)) END) as fChiTieuBoXung INTO #tmpChiTieuBoXung
	FROM VDT_KHV_PhanBoVon as tbl
	INNER JOIN VDT_KHV_PhanBoVon_ChiTiet as dt  on tbl.Id = dt.iID_PhanBoVonID
	WHERE bIsGoc = 0 AND iLoai = @iLoaiKeHoachVon
	GROUP BY iID_MaDonViQuanLy, iID_NguonVonID, iNamKeHoach, iID_LoaiNguonVonID


	SELECT DISTINCT tbl.Id, tbl.iID_TongHopSoLieuID, tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.iID_LoaiNguonVonID, tbl.iID_DonViQuanLyID,
		tbl.iID_MaDonViQuanLy, tbl.iID_NhomQuanLyID, tbl.iID_LoaiNganSachID, tbl.iID_KhoanNganSachID, tbl.sLoaiDieuChinh, tbl.iID_ParentId,
		tbl.bActive, tbl.bIsGoc, tbl.bLaThayThe, tbl.fGiaTrDeNghi, tbl.fGiaTrPhanBo, tbl.fGiaTriThuHoi, tbl.iID_DonViTienTeID, tbl.iID_TienTeID,
		tbl.fTiGiaDonVi, tbl.fTiGia, tbl.sUserCreate, tbl.dDateCreate, tbl.sUserUpdate, tbl.dDateUpdate, tbl.sUserDelete,
		tbl.dDateDelete, tbl.bIsCanBoDuyet, tbl.bIsDuyet, tbl.iID_NguonVonID, tbl.iLoai,
		tbl.fCapPhatTaiKhoBac, tbl.fCapPhatBangLenhChi, tbl.fTonKhoanTaiDonVi, tbl.fGiaTriThuHoiNamTruocKhoBac, tbl.fGiaTriThuHoiNamTruocLenhChi,
		tbl.bKhoa as BKhoa, ISNULL(tbl.iLoaiDuToan, 0) as ILoaiDuToan,
		--tbl.iID_VonNamDeXuatID, 
		tbl.iID_PhanBoGocID as IIdPhanBoGocID, nv.sTen as sNguonVon, lnv.sMoTa as sLoaiNguonVon, dv.sTenDonVi as sTenDonVi, 
			(CASE WHEN @iLoaiKeHoachVon = 1 THEN ISNULL(ctdn .fGiaTrPhanBo,0) ELSE ISNULL(ctdn .fGiaTrDeNghi,0) END) as fChiTieuDauNam, (CASE WHEN tbl.bIsGoc = 1 THEN 0 ELSE ISNULL(ctbx.fChiTieuBoXung,0) END) as fChiTieuBoXung, 
			ISNULL(iSoLanDieuChinh,0) as iSoLanDieuChinh,
			case
				when tbl.iID_ParentId is null then ''
				else (select pbvpr.sSoQuyetDinh from VDT_KHV_PhanBoVon pbvpr where pbvpr.Id = tbl.iID_ParentId)
			end DieuChinhTu
	FROM VDT_KHV_PhanBoVon as tbl
	LEFT JOIN NguonNganSach as nv on tbl.iID_NguonVonID = nv.iID_MaNguonNganSach
	LEFT JOIN NS_MucLucNganSach as lnv on tbl.iID_LoaiNguonVonID = lnv.iID
	LEFT JOIN DonVi as dv on tbl.iID_DonViQuanLyID = dv.iID_DonVi
	LEFT JOIN VDT_KHV_PhanBoVon as ctdn on ctdn.bIsGoc = 1 AND tbl.iID_DonViQuanLyID = ctdn.iID_DonViQuanLyID
										AND tbl.iID_NguonVonID = ctdn.iID_NguonVonID
										AND tbl.iNamKeHoach = ctdn.iNamKeHoach 
										AND tbl.iID_LoaiNguonVonID = ctdn.iID_LoaiNguonVonID
										AND ctdn.iLoai = @iLoaiKeHoachVon
	LEFT JOIN #tmpChiTieuBoXung as ctbx on tbl.iID_MaDonViQuanLy = ctbx.iID_MaDonViQuanLy
										AND tbl.iID_NguonVonID = ctbx.iID_NguonVonID
										AND tbl.iID_LoaiNguonVonID = ctbx.iID_LoaiNguonVonID
										AND tbl.iNamKeHoach = ctbx.iNamKeHoach
	LEFT JOIN #tmpDieuChinh as dc on tbl.iID_ParentId = dc.iID_ParentId and tbl.iNamKeHoach = dc.iNamKeHoach
	WHERE 
		tbl.iLoai = @iLoaiKeHoachVon
	ORDER BY tbl.dDateCreate DESC

	DROP TABLE #tmpChiTieuBoXung
	DROP TABLE #tmpDieuChinh
END
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
		VALUES('QUYET_TOAN'), ('131'), ('132'),('133'), ('211c'), ('212c'), ('301'), ('302'), ('321a'), ('322a')
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
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_kehoachvonungchitiet_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_kehoachvonungchitiet_detail]
GO


CREATE PROCEDURE [dbo].[sp_vdt_get_kehoachvonungchitiet_detail]
@iIdKeHoachVonUng uniqueidentifier
AS
BEGIN
	DECLARE @iIdKeHoachVonUngDeXuat uniqueidentifier = (SELECT iID_KeHoachUngDeXuatID FROM VDT_KHV_KeHoachVonUng WHERE Id = @iIdKeHoachVonUng)

	SELECT dt.iID_DuAnID, dt.ID_DuAn_HangMuc, SUM(ISNULL(dt.fGiaTriDeNghi, 0)) as fGiaTriDeNghi INTO #tmpDX
	FROM VDT_KHV_KeHoachVonUng_DX as tbl 
	INNER JOIN VDT_KHV_KeHoachVonUng_DX_ChiTiet as dt on tbl.Id = dt.iID_KeHoachUngID
	WHERE tbl.Id = @iIdKeHoachVonUngDeXuat
	GROUP BY dt.iID_DuAnID, dt.ID_DuAn_HangMuc

	SELECT dt.iID_DuAnID, da.sTenDuAn, da.sMaKetNoi, da.sTrangThaiDuAn as sTrangThaiDuAnDangKy, da.sMaDuAn, dx.id_duan_hangmuc, dahm.sTenHangMuc, 
			da.iID_DonViTienTeID, da.iID_TienTeID, da.fTiGia, da.fTiGiaDonVi, dt.fCapPhatBangLenhChi,dt.fTonKhoanTaiDonVi, dt.fCapPhatTaiKhoBac , dt.sGhiChu,
			(SELECT SUM(ISNULL(fTongMucDauTuPheDuyet,0)) FROM VDT_DA_QDDauTu WHERE iID_DuAnID =dt.iID_DuAnID AND dNgayQuyetDinh <= tbl.dNgayQuyetDinh AND bActive = 1) as fTongMucDauTuPheDuyet,
			dt.iID_MucID, dt.iID_TieuMucID, dt.iID_TietMucID, dt.iID_NganhID,
			ml.sLNS as sLNS, ml.sL as sL, ml.sK as sK, ml.sM as sM, ml.sTM as sTM, ml.sTTM as sTTM, ml.sNG as sNG, dx.fGiaTriDeNghi
	FROM VDT_KHV_KeHoachVonUng as tbl 
	INNER JOIN VDT_KHV_KeHoachVonUng_ChiTiet as dt on tbl.Id = dt.iID_KeHoachUngID
	INNER JOIN VDT_DA_DuAn as da on dt.iID_DuAnID = da.iID_DuAnID
	LEFT JOIN #tmpDX as dx on dt.iID_DuAnID = dx.iID_DuAnID
	left join vdt_da_duan_hangmuc dahm on dahm.iID_DuAn_HangMucID = dx.id_duan_hangmuc

	LEFT JOIN NS_MucLucNganSach as ml on dt.iID_MucID = ml.iId
									OR dt.iID_TieuMucID = ml.iId
									OR dt.iID_TietMucID = ml.iId
									OR dt.iID_NganhID = ml.iId
	WHERE tbl.Id = @iIdKeHoachVonUng
END
;
;
;
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_duan_kehoachvonung_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_duan_kehoachvonung_detail]
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
			NULL as fCapPhatBangLenhChi, NULL as fCapPhatTaiKhoBac, NULL as fTonKhoanTaiDonVi, ISNULL(fGiaTriDeNghi, 0) as fGiaTriDeNghi,
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
;
GO




