/****** Object:  StoredProcedure [dbo].[sp_skt_dutoan_daunam_phancap_dtdn]    Script Date: 29/10/2022 4:06:14 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_dutoan_daunam_phancap_dtdn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_dutoan_daunam_phancap_dtdn]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_ldtdn_delete_chitiet_by_delete_mlns]    Script Date: 29/10/2022 4:06:14 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_ldtdn_delete_chitiet_by_delete_mlns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_ldtdn_delete_chitiet_by_delete_mlns]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_dutoandaunamchitietcancu_by_chungtuid]    Script Date: 29/10/2022 4:06:14 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_dutoandaunamchitietcancu_by_chungtuid]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_dutoandaunamchitietcancu_by_chungtuid]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_duan_find_from_qddautu]    Script Date: 29/10/2022 4:06:14 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_duan_find_from_qddautu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_duan_find_from_qddautu]
GO
/****** Object:  StoredProcedure [dbo].[sp_export_kehoachvonung_duocduyet]    Script Date: 29/10/2022 4:06:14 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_export_kehoachvonung_duocduyet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_export_kehoachvonung_duocduyet]
GO
/****** Object:  StoredProcedure [dbo].[sp_export_kehoachvonung_donvi]    Script Date: 29/10/2022 4:06:14 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_export_kehoachvonung_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_export_kehoachvonung_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_export_kehoachvonung_donvi]    Script Date: 29/10/2022 4:06:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_export_kehoachvonung_donvi]
@Ids t_tbl_uniqueidentifier READONLY
AS
BEGIN
	SELECT dt.iID_DuAnID as IIDDuAnID, da.sTenDuAn, da.sMaDuAn, dt.fGiaTriDeNghi as FGiaTriDeNghi, dt.sGhiChu as SGhiChu, da.iID_MaDonViThucHienDuAnID as IIDMaDonViQuanLy, dt.iID_KeHoachUngID ,
	(SELECT SUM(ISNULL(fTongMucDauTuPheDuyet,0)) FROM VDT_DA_QDDauTu WHERE iID_DuAnID =dt.iID_DuAnID AND dNgayQuyetDinh <= tbl.dNgayDeNghi AND bActive = 1) as FTongMucDauTuPheDuyet
	FROM @Ids as tmp
	INNER JOIN VDT_KHV_KeHoachVonUng_DX_ChiTiet as dt on tmp.Id = dt.iID_KeHoachUngID
	INNER JOIN VDT_KHV_KeHoachVonUng_DX as tbl on dt.iID_KeHoachUngID = tbl.Id
	INNER JOIN VDT_DA_DuAn as da on dt.iID_DuAnID = da.iID_DuAnID	
END
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_all_dutoan_nguonvon_by_duan]    Script Date: 26/11/2021 10:19:14 PM ******/
SET ANSI_NULLS ON
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_export_kehoachvonung_duocduyet]    Script Date: 29/10/2022 4:06:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_export_kehoachvonung_duocduyet]
@Ids t_tbl_uniqueidentifier READONLY
AS
BEGIN
	select da.sTenDuAn as sTenDuAn, da.sMaDuAn as sMaDuAn, dt.fCapPhatTaiKhoBac, dt.fCapPhatBangLenhChi as fCapPhatBangLenhChi, dt.iID_DuAnID as IIDDuAnID, dt.iID_KeHoachUngID as iID_KeHoachUngID, dt.sGhiChu, dv.sTenDonVi,
	(SELECT SUM(ISNULL(fTongMucDauTuPheDuyet,0)) FROM VDT_DA_QDDauTu WHERE iID_DuAnID =dt.iID_DuAnID AND dNgayQuyetDinh <= tbl.dNgayQuyetDinh AND bActive = 1) as fTongMucDauTuPheDuyet,
	ml.sLNS as sLNS, ml.sL as sL, ml.sK as sK, ml.sM as sM, ml.sTM as sTM, ml.sTTM as sTTM, ml.sNG as sNG
	FROM @Ids as tmp
	INNER JOIN VDT_KHV_KeHoachVonUng_ChiTiet as dt on tmp.Id = dt.iID_KeHoachUngID
	INNER JOIN VDT_KHV_KeHoachVonUng as tbl on dt.iID_KeHoachUngID = tbl.Id
	INNER JOIN VDT_DA_DuAn as da on dt.iID_DuAnID = da.iID_DuAnID
	LEFT JOIN DonVi dv on tbl.iID_DonViQuanLyID = dv.iID_DonVi
	LEFT JOIN NS_MucLucNganSach as ml on dt.iID_MucID = ml.iId
									OR dt.iID_TieuMucID = ml.iId
									OR dt.iID_TietMucID = ml.iId
									OR dt.iID_NganhID = ml.iId	
END
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_all_dutoan_nguonvon_by_duan]    Script Date: 26/11/2021 10:19:14 PM ******/
SET ANSI_NULLS ON
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_nh_duan_find_from_qddautu]    Script Date: 29/10/2022 4:06:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 16/03/2022
-- Description:	Lấy danh sách dự án cho màn quyết định đầu tư
-- =============================================
CREATE PROCEDURE [dbo].[sp_nh_duan_find_from_qddautu]
	@YearOfWork INT, 
	@MaDonVi NVARCHAR(50),
	@ILoai INT,
	@QdDauTuId UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @DuAnId UNIQUEIDENTIFIER;
	SELECT @DuAnId = iID_DuAnID FROM NH_DA_QDDauTu WHERE ID = @QdDauTuId;

    SELECT
		duAn.ID AS Id,
		duAn.iID_KHTT_NhiemVuChiID AS IIdKhttNhiemVuChiId,
		duAn.sMaDuAn AS SMaDuAn,
		duAn.sTenDuAn AS STenDuAn,
		duAn.iID_DonViQuanLyID AS IIdDonViQuanLyId,
		duAn.iID_ChuDauTuID AS IIdChuDauTuId,
		duAn.iID_MaChuDauTu AS IIdMaChuDauTu,
		duAn.iID_MaDonViQuanLy AS IIdMaDonViQuanLy,
		duAn.iID_CapPheDuyetID AS IIdCapPheDuyetId,
		duAn.sKhoiCong AS SKhoiCong,
		duAn.sKetThuc AS SKetThuc,
		duAn.bIsDuPhong AS BIsDuPhong,
		duAn.sDiaDiem AS SDiaDiem,
		duAn.sMucTieu AS SMucTieu,
		duAn.sQuyMo AS SQuyMo,
		duAn.iID_TiGiaUSD_EURID AS IIdTiGiaUsdEurid,
		duAn.iID_TiGiaUSD_VNDID AS IIdTiGiaUsdVndid,
		duAn.iID_TiGiaUSD_NgoaiTeKhacID AS IIdTiGiaUsdNgoaiTeKhacId,
		duAn.fUSD AS FUsd,
		duAn.fNgoaiTeKhac AS FNgoaiTeKhac,
		duAn.fVND AS FVnd,
		duAn.fEUR AS FEur,
		duAn.dNgayTao AS DNgayTao,
		duAn.sNguoiTao AS SNguoiTao,
		duAn.dNgaySua AS DNgaySua,
		duAn.sNguoiSua AS SNguoiSua,
		duAn.dNgayXoa AS DNgayXoa,
		duAn.sNguoiXoa AS SNguoiXoa,
		duAn.iID_ChuTruongDauTuID AS IIdChuTruongDauTuId,
		duAn.iID_TiGiaID AS IIdTiGiaId,
		duAn.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
		NULL AS STenDonVi,
		NULL AS STenPheDuyet,
		NULL AS STenChuDauTu
	FROM NH_DA_DuAn duAn
	WHERE
		1=1
		AND duAn.iID_MaDonViQuanLy = @MaDonVi
		AND duAn.ID IN (SELECT iID_DuAnID FROM NH_DA_ChuTruongDauTu) -- Lấy dự án đã có chủ trương đầu tư
		AND duAn.ID NOT IN (SELECT DISTINCT(iID_DuAnID) FROM NH_DA_QDDauTu WHERE iID_DuAnID IS NOT NULL AND ILoai = @ILoai AND (@QdDauTuId IS NULL OR iID_DuAnID <> @DuAnId)) -- Lấy dự án chưa có quyết định đầu tư
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_dutoandaunamchitietcancu_by_chungtuid]    Script Date: 29/10/2022 4:06:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_ns_dutoandaunamchitietcancu_by_chungtuid]
@iIdsChungTu t_tbl_uniqueidentifier READONLY
AS
BEGIN
	SELECT dt.*, cc.iID_MaChucNang, cc.iNamCanCu, cc.sModule
	FROM @iIdsChungTu as tbl
	INNER JOIN NS_DTDauNam_ChungTuChiTiet_CanCu as dt on tbl.Id = dt.iID_CTDTDauNam
	INNER JOIN NS_CauHinh_CanCu cc on dt.iID_CanCu = cc.iID_CauHinh_CanCu
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_ldtdn_delete_chitiet_by_delete_mlns]    Script Date: 29/10/2022 4:06:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_ns_ldtdn_delete_chitiet_by_delete_mlns]
@iID_CTDTDauNam AS uniqueidentifier ,
@sMLNS AS nvarchar(max),
@iNamLamViec int

As
Begin

	Delete NS_DTDauNam_ChungTuChiTiet_CanCu
	where iID_CTDTDauNam = @iID_CTDTDauNam
	and sLNS  in  (SELECT * FROM f_split(@sMLNS))
	and iNamLamViec = @iNamLamViec


	--Xóa chứng từ đầu năm chi tiết 
	Delete NS_DTDauNam_ChungTuChiTiet 
	where iID_CTDTDauNam = @iID_CTDTDauNam 
	and sLNS  in  (SELECT * FROM f_split(@sMLNS))
	and iNamLamViec = @iNamLamViec
	--

End;

GO
/****** Object:  StoredProcedure [dbo].[sp_skt_dutoan_daunam_phancap_dtdn]    Script Date: 29/10/2022 4:06:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[sp_skt_dutoan_daunam_phancap_dtdn] 
@YearOfWork int,
@XauNoiMaString nvarchar(MAX),
@XauNoiMa nvarchar(MAX),
@ChiTietId nvarchar(200),
@iID_CTDTDauNam uniqueidentifier,
@XauNoiMaGoc nvarchar(MAX)
AS 
BEGIN
SET NOCOUNT ON;

SELECT iID_MLNS AS MucLucID,
       sLNS AS LNS,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS NG,
	   sTNG AS TNG,
       sTNG1 AS TNG1,
       sTNG2 AS TNG2,
       sTNG3 AS TNG3,
       sMoTa AS MoTa,
       sXauNoiMa AS XauNoiMa,
       '' AS IdDonViMLNS,
       NULL AS Id,
       NULL AS SoLieuChiTietId,
       '' AS IdDonVi,
       '' AS TenDonVi,
       NULL AS MLNSId,
       0 AS TuChi,
       '' AS GhiChu,
       bHangCha
FROM NS_MucLucNganSach
WHERE iNamLamViec = @YearOfWork
  AND bHangCha =1
  AND sXauNoiMa in
    (SELECT *
     FROM f_split(@XauNoiMaString))
UNION ALL
SELECT MucLucID,
       sLNS,
       sL,
       sK,
       sM,
       sTM,
       sTTM,
       sNG,
	   sTNG,
       sTNG1,
       sTNG2,
       sTNG3,
       sMoTa,
       sXauNoiMa,
       IdDonViMLNS,
       iID_DTDauNam_PhanCap,
       SoLieuChiTietId,
       isnull(IdDonVi, IdDonViMLNS) AS IdDonVi,
       sTenDonVi,
       MLNSId,
       isnull(TuChi, 0) AS TuChi,
       sGhiChu,
       cast(0 AS bit) AS bHangCha
FROM (
        (SELECT mlns.iID_MLNS AS MucLucID,
                mlns.sLNS,
                mlns.sL,
                mlns.sK,
                mlns.sM,
                mlns.sTM,
                mlns.sTTM,
                mlns.sNG,
				mlns.sTNG,
                mlns.sTNG1,
                mlns.sTNG2,
                mlns.sTNG3,
                mlns.sMoTa,
                mlns.sXauNoiMa,
                donvi.iID_MaDonVi AS IdDonViMLNS,
                donvi.sTenDonVi
         FROM NS_MucLucNganSach mlns,
              DonVi donvi
         WHERE mlns.iNamLamViec = @YearOfWork
           AND mlns.sXauNoiMa = @XauNoiMa
           AND donvi.iNamLamViec = @YearOfWork
           AND donvi.iLoai = '2'
           AND donvi.iTrangThai =1) mlns
      LEFT JOIN
        (SELECT iID_DTDauNam_PhanCap,
                iID_CTDTDauNamChiTiet AS SoLieuChiTietId,
                iID_MaDonVi AS IdDonVi,
                iID_MLNS AS MLNSId,
                isnull(fTuChi, 0) AS TuChi,
                sGhiChu
         FROM NS_DTDauNam_PhanCap
         WHERE (@ChiTietId is null or iID_CTDTDauNamChiTiet = @ChiTietId) and  iID_CTDTDauNam = @iID_CTDTDauNam And sXauNoiMaGoc = @XauNoiMaGoc
           AND iNamLamViec = @YearOfWork ) phancap ON mlns.MucLucID = phancap.MLNSId
      AND mlns.IdDonViMLNS = phancap.IdDonVi)
ORDER BY sXauNoiMa END
;
;
GO

update NS_DTDauNam_ChungTuChiTiet_CanCu
set iID_CTDTDauNam = (select top 1 iID_CTDTDauNam from NS_DTDauNam_ChungTu dt
where dt.iID_MaDonVi = NS_DTDauNam_ChungTuChiTiet_CanCu.iID_MaDonVi
and dt.iID_MaNguonNganSach=NS_DTDauNam_ChungTuChiTiet_CanCu.iID_MaNguonNganSach
and dt.iNamLamViec = NS_DTDauNam_ChungTuChiTiet_CanCu.iNamLamViec)
where (iID_CTDTDauNam is null or iID_CTDTDauNam ='00000000-0000-0000-0000-000000000000')

update NS_DTDauNam_PhanCap
set iID_CTDTDauNam = (select iID_CTDTDauNam from NS_DTDauNam_ChungTuChiTiet ct
where ct.iID_CTDTDauNamChiTiet = NS_DTDauNam_PhanCap .iID_CTDTDauNamChiTiet)
where (iID_CTDTDauNam is null or iID_CTDTDauNam ='00000000-0000-0000-0000-000000000000')