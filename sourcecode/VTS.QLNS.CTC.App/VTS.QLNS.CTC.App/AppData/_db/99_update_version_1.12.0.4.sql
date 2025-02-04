/****** Object:  StoredProcedure [dbo].[sp_vdt_hopdong_dieuchinh_find_hangmuc_by_chiphi_hopdongdieuchinh]    Script Date: 07/10/2022 5:28:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_hopdong_dieuchinh_find_hangmuc_by_chiphi_hopdongdieuchinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_hopdong_dieuchinh_find_hangmuc_by_chiphi_hopdongdieuchinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_all]    Script Date: 07/10/2022 5:28:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_all]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_all]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtin_hopdong_index]    Script Date: 07/10/2022 5:28:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thongtin_hopdong_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thongtin_hopdong_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_dotnhan_phanbo_findAll]    Script Date: 07/10/2022 5:28:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dutoan_dotnhan_phanbo_findAll]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dutoan_dotnhan_phanbo_findAll]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_index_3]    Script Date: 07/10/2022 5:31:29 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_solieuchitiet_index_3]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_solieuchitiet_index_3]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_dotnhan_phanbo_findAll]    Script Date: 07/10/2022 5:28:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_dt_dutoan_dotnhan_phanbo_findAll]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Type int,
	@VoucherType int,
	@Date datetime
AS
BEGIN
	DECLARE @DieuChinh int = 4;
	WITH tblNhanPhanBo AS (
		SELECT *, 
			   fTongTuChi + fTongHienVat + fTongPhanCap + fTongHangNhap + fTongHangMua + fTongDuPhong + fTongTonKho AS SoPhanBo
		FROM NS_DT_ChungTu 
		WHERE iNamLamViec = @YearOfWork 
			AND iNamNganSach = @YearOfBudget 
			AND iID_MaNguonNganSach = @BudgetSource
			AND iLoai = @Type 
			AND iLoaiChungTu = @VoucherType
			AND (fTongTuChi <> 0 OR fTongHienVat <> 0 OR fTongPhanCap <> 0 OR fTongHangNhap <> 0 OR fTongHangMua <> 0 OR fTongDuPhong <> 0 OR fTongTonKho <> 0)
			AND (
				(dNgayQuyetDinh IS NOT NULL AND CAST(dNgayQuyetDinh AS DATE) <= CAST(@Date AS DATE)) 
				OR 
				(dNgayChungTu IS NOT NULL AND CAST(dNgayChungTu AS DATE) <= CAST(@Date AS DATE)))
	),
	tblChungTuNhanPhanBoMap AS (
		SELECT * 
		FROM NS_DT_Nhan_PhanBo_Map 
		WHERE iID_CTDuToan_Nhan IN (SELECT iID_DTChungTu FROM tblNhanPhanBo)
	),
	tblDaPhanBo AS (
		SELECT ctpb.iID_CTDuToan_Nhan, 
			   SUM(fTuChi) + SUM(fHienVat) + SUM(fPhanCap) + SUM(fHangNhap) + SUM(fHangMua) + SUM(fDuPhong) + SUM(fTonKho) AS DaPhanBo 
		FROM tblChungTuNhanPhanBoMap ctpb
		LEFT JOIN 
			(SELECT dtctct.*, dtct.iLoaiDuToan, dtct.iID_DotNhan FROM NS_DT_ChungTuChiTiet dtctct
			 LEFT JOIN NS_DT_ChungTu dtct
			 ON dtctct.iID_DTChungTu = dtct.iID_DTChungTu
			 WHERE dtctct.iNamLamViec = @YearOfWork 
				   AND dtct.iLoai = 1
				   AND dtct.iLoaiChungTu = 1
				   AND (dNgayQuyetDinh IS NOT NULL AND CAST(dNgayQuyetDinh AS DATE) < CAST(@Date AS DATE)) 
				   OR (dNgayQuyetDinh IS NULL AND dNgayChungTu IS NOT NULL AND CAST(dNgayChungTu AS DATE) < CAST(@Date AS DATE))
				   AND dtctct.iNamNganSach = @YearOfBudget 
				   AND dtctct.iID_MaNguonNganSach = @BudgetSource) ctct
		ON ctpb.iID_CTDuToan_Nhan = ctct.iID_CTDuToan_Nhan
		WHERE ctct.iID_DTChungTu IS NOT NULL
		and 
		(
			(ctpb.iID_CTDuToan_PhanBo = ctct.iID_DTChungTu AND ctct.iLoaiDuToan <> @DieuChinh)
			OR
			(ctct.iLoaiDuToan = @DieuChinh AND ctct.iID_DotNhan = ctpb.iID_CTDuToan_PhanBo)
		)
		GROUP BY ctpb.iID_CTDuToan_Nhan
	)

	SELECT 
		   npb.iID_DTChungTu AS Id,
		   npb.sSoChungTu AS SSoChungTu,
		   npb.dNgayChungTu AS DNgayChungTu,
		   npb.sDSLNS AS SDslns,
		   npb.sDSID_MaDonVi AS SDsidMaDonVi,
		   npb.sSoQuyetDinh AS SSoQuyetDinh,
		   npb.dNgayQuyetDinh AS DNgayQuyetDinh,
		   ISNULL(npb.SoPhanBo, 0) AS SoPhanBo,
		   ISNULL(DaPhanBo, 0) AS DaPhanBo,
		   ISNULL(npb.SoPhanBo, 0) - ISNULL(DaPhanBo, 0) AS SoChuaPhanBo 
	FROM tblNhanPhanBo npb
	LEFT JOIN tblDaPhanBo dpb
	ON npb.iID_DTChungTu = dpb.iID_CTDuToan_Nhan
	WHERE ISNULL(npb.SoPhanBo, 0) > ISNULL(DaPhanBo, 0)
	ORDER BY npb.sSoChungTu

END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtin_hopdong_index]    Script Date: 07/10/2022 5:28:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_thongtin_hopdong_index]
@iThuocMenu INT = NULL
AS
BEGIN
SELECT 
	hd.Id,
	hd.sSoHopDong AS SSoHopDong,
	hd.dNgayHopDong AS DNgayHopDong,
	hd.sTenHopDong AS STenHopDong,
	hd.dKhoiCongDuKien AS DKhoiCongDuKien,
	hd.dKetThucDuKien AS DKetThucDuKien,
	hd.iID_KHTongThe_NhiemVuChiID AS IIdKhTongTheNhiemVuChiId,
	hd.iID_LoaiHopDongID AS IIdLoaiHopDongId,
	hd.iID_ParentAdjustID AS IIdParentAdjustId,
	hd.iID_GoiThauID AS IIdGoiThauId,
	hd.iID_ParentID AS IIdParentId,
	hd.iID_NhaThauThucHienID AS IIdNhaThauThucHienId,
	hd.iID_TiGiaID AS IIdTiGiaId,
	hd.iID_DuAnID AS IIdDuAnId,
	hd.iLoai AS ILoai,
	hd.fGiaTriUSD AS FGiaTriUsd,
	hd.fGiaTriVND AS FGiaTriVnd,
	hd.fGiaTriEUR AS FGiaTriEur,
	hd.fGiaTriNgoaiTeKhac AS FGiaTriNgoaiTeKhac,
	hd.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
	hd.sNguoiTao AS SNguoiTao,
	hd.sNguoiSua AS SNguoiSua,
	hd.sNguoiXoa AS SNguoiXoa,
	hd.dNgaySua AS DNgaySua,
	hd.dNgayTao AS DNgayTao,
	hd.dNgayXoa AS DNgayXoa,
	hd.bIsActive AS BIsActive,
	hd.bIsGoc AS BIsGoc,
	hd.bIsKhoa AS BIsKhoa,
	lhd.sTenLoaiHopDong AS SLoaiHopDong,
	hd.iLanDieuChinh AS ILanDieuChinh,
	nvChi.iID_KHTongTheID AS IIdKhTongTheId,
	nvChi.STenChuongTrinh AS STenChuongTrinh,
	da.sTenDuAn AS STenDuAn,
	--CONCAT(donVi.iID_MaDonVi, ' - ', donVi.sTenDonVi) AS STenDonVi,
	CONCAT(donVi.iID_MaDonVi, ' - ', donVi.sTenDonVi) AS STenDonVi,
	hd.iID_DonViQuanLyID AS IIdDonViQuanLyId,
	hd.iID_MaDonViQuanLy AS IIdMaDonViQuanLy,
	CASE
		WHEN hd.iID_ParentAdjustId IS NULL THEN
		'' ELSE ( SELECT TOP 1 hdpr.sSoHopDong FROM NH_DA_HopDong hdpr WHERE hdpr.Id = hd.iID_ParentAdjustId ) 
	END DieuChinhTu
	
FROM NH_DA_HopDong hd
LEFT JOIN NH_DM_LoaiHopDong lhd on hd.iID_LoaiHopDongID = lhd.iID_LoaiHopDongID
LEFT JOIN NH_DA_DuAn da on hd.iID_DuAnID = da.ID
LEFT JOIN DonVi donVi
ON hd.iID_DonViQuanLyID = donVi.iID_DonVi AND hd.iID_MaDonViQuanLy = donVi.iID_MaDonVi
LEFT JOIN
		(SELECT  n.ID, n.iID_KHTongTheID, d.sTenNhiemVuChi AS STenChuongTrinh, n.iID_NhiemVuChiID
		 FROM NH_KHTongThe_NhiemVuChi AS n 
		 INNER JOIN NH_DM_NhiemVuChi AS d 
		 ON n.iID_NhiemVuChiID = d.ID) 
AS nvChi
ON hd.iID_KHTongThe_NhiemVuChiID = nvChi.iID_NhiemVuChiID

WHERE @iThuocMenu IS NULL OR hd.iThuocMenu = @iThuocMenu
ORDER BY
	hd.dNgayTao DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_all]    Script Date: 07/10/2022 5:28:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_all] 
@Loai varchar(MAX),
@IdDonVi varchar(MAX),
@NamLamViec int, 
@Dvt int,
@LoaiChungTu int 
AS
BEGIN
SELECT DISTINCT mucluc.iID AS Id,
                mucluc.iID_MLSKT AS IDMucLuc,
                mucluc.sSTT AS STT,
                mucluc.sMoTa AS MoTa,
                mucluc.iID_MLSKTCha AS IdParent,
                mucluc.sSTTBC AS STTBC,
                mucluc.sM AS M,
                CAST(0 AS BIT) AS IsHangCha,
                chitiet.*
FROM NS_SKT_MucLuc mucluc
LEFT JOIN
  (SELECT KyHieu ,
          QuyetToan =sum(QuyetToan)/@Dvt ,
          DuToan =sum(DuToan)/@Dvt ,
          TuChi =sum(fTuChi)/@Dvt ,
          TuChi2 =sum(TuChi2)/@Dvt
   FROM
     (SELECT mucluc.sKyHieu AS KyHieu,
             QuyetToan = 0 ,
             DuToan = 0 ,
             fTuChi = chitiet.fMuaHangCapHienVat,
             TuChi2 = chitiet.fPhanCap
      FROM NS_SKT_ChungTuChiTiet chitiet
      LEFT JOIN NS_SKT_MucLuc mucluc ON chitiet.iID_MLSKT = mucluc.iID_MLSKT
      WHERE  chitiet.iNamLamViec=@NamLamViec
        AND chitiet.iLoai in (SELECT * FROM f_split(@Loai))
        AND chitiet.iLoaiChungTu = @LoaiChungTu
        AND (@IdDonVi IS NULL
             OR chitiet.iID_MaDonVi in
               (SELECT *
                FROM f_split(@IdDonVi)))

      UNION ALL SELECT dbo.f_get_ky_hieu_by_xaunoima(SKT_XauNoiMa,@NamLamViec) AS KyHieu ,
                       QuyetToan,
                       DuToan,
                       TuChi = 0 ,
                       TuChi2 = 0
      FROM f_skt_cancu(@NamLamViec, @IdDonVi, cASt(@LoaiChungTu AS nvarchar(MAX))) AS a
      WHERE dbo.f_get_ky_hieu_by_xaunoima(SKT_XauNoiMa,@NamLamViec) IS NOT NULL -- lap chi tiet du toan

      UNION ALL SELECT dbo.f_get_ky_hieu_by_xaunoima(XauNoiMa,@NamLamViec) AS KyHieu ,
                       QuyetToan = 0 ,
                       DuToan = 0 ,
                       TuChi = TuChi ,
                       TuChi2 = 0
      FROM f_skt_dutoan(@NamLamViec, @IdDonVi, cASt(@LoaiChungTu AS nvarchar(MAX))) AS a
      WHERE dbo.f_get_ky_hieu_by_xaunoima(XauNoiMa,@NamLamViec) IS NOT NULL ) AS a
   GROUP BY KyHieu --order by Tuchi
 ) chitiet ON mucluc.sKyHieu = chitiet.KyHieu
WHERE chitiet.KyHieu IS NOT NULL
  AND mucluc.iNamLamViec = @NamLamViec
ORDER BY sSTTBC END
;
;
;
-- exec [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_all]  '4', '004',2022,1000,'2'
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_hopdong_dieuchinh_find_hangmuc_by_chiphi_hopdongdieuchinh]    Script Date: 07/10/2022 5:28:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_vdt_hopdong_dieuchinh_find_hangmuc_by_chiphi_hopdongdieuchinh] 
	-- Add the parameters for the stored procedure here
	@chiphiID uniqueidentifier,
	@idHopDongGoiThauNhaThau uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select t1.*, t3.sTenHangMuc, t3.maOrder, t3.iID_ParentID, ISNULL(t1.fGiaTriDuocDuyet, t4.fTienGoiThau) as fTienGoiThau, t1.iID_HopDonggoiThauNhaThauID as IdHopDongGoiThauNhaThau,
        t1.fGiatri as fGiatriSuDung, ISNULL(t1.fGiaTriTruocDC, t1.fGiatri) as giatritruocdc
        from VDT_DA_HopDong_goiThau_HangMuc t1
        left join VDT_DA_HopDong_goiThau_NhaThau t2 on t1.iID_HopDongGoiThauNhaThauID = t2.id
        left join VDT_DA_DuToan_DM_HangMuc t3 on t3.id = t1.iID_HangMucID
        left join VDT_DA_goiThau_HangMuc t4 on t4.iID_goiThauID = t2.iID_GoiThauID and t4.iID_ChiPhiID = t1.iID_ChiPhiID and t4.iID_HangMucID = t1.iID_HangMucID
        where t1.iID_ChiPhiID = @chiphiID and t1.iID_HopDonggoiThauNhaThauID = @idHopDongGoiThauNhaThau
END
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_index_3]    Script Date: 07/10/2022 5:31:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_solieuchitiet_index_3]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Loai int,
	@TypeGet int,
	@AgencyId nvarchar(500),
	@LoaiChungTu nvarchar(50),
	@Lns nvarchar(max),
	@VoucherId nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	select *  into #lnsTem  from f_split(@Lns);
	SELECT DISTINCT isnull(chitiet.iID_CTDTDauNamChiTiet, NEWID()) AS Id,
                chitiet.iID_CTDTDauNamChiTiet AS IdDb,
                mlns.iID_MLNS AS MlnsId,
                mlns.iID_MLNS_Cha AS MlnsIdParent,
                mlns.sXauNoiMa AS XauNoiMa,
                mlns.sLNS AS LNS,
                mlns.sL AS L,
                mlns.sK AS K,
                mlns.sM AS M,
                mlns.sTM AS TM,
                mlns.sTTM AS TTM,
                mlns.sNG AS NG,
                mlns.sTNG AS TNG,
                mlns.sTNG1 AS TNG1,
                mlns.sTNG2 AS TNG2,
                mlns.sTNG3 AS TNG3,
                mlns.sMoTa AS MoTa,
                '' AS Chuong,
                mlns.bHangCha,
                mlns.sChiTietToi AS ChiTietToi,
                chitiet.iID_MaDonVi AS IdDonVi,
                chitiet.sTenDonVi AS TenDonVi,
                chitiet.fTuChi AS ChiTiet,
				chitiet.fUocThucHien AS UocThucHien,
                chitiet.fHangNhap AS HangNhap,
                chitiet.fHangMua AS HangMua,
                chitiet.fPhanCap AS PhanCap,
				ISNULL(chitiet.fPhanCap,0) - ISNULL(chitiet_phancap.fTuChi,0) as PhanCapConLai,
                chitiet.fChuaPhanCap AS ChuaPhanCap,
                map.sSKT_KyHieu AS SKT_KyHieu,
				mlns.bHangChaDuToan
FROM NS_MucLucNganSach mlns
LEFT JOIN
  (SELECT *
   FROM NS_DTDauNam_ChungTuChiTiet
   WHERE iNamLamViec = @YearOfWork
     AND (iLoai = 3)
     AND iNamNganSach = @YearOfBudget
     AND iID_MaNguonNganSach =@BudgetSource
     AND (iID_MaDonVi = @AgencyId
          OR (@AgencyId = '00'
              AND bKhoa = 0))
	 AND iID_CTDTDauNam = @VoucherId
     AND iLoaiChungTu = @LoaiChungTu ) chitiet ON mlns.sXauNoiMa = chitiet.sXauNoiMa
LEFT JOIN
  (SELECT *
   FROM NS_MLSKT_MLNS
   WHERE iNamLamViec = @YearOfWork ) map ON mlns.sXauNoiMa = map.sNS_XauNoiMa

LEFT JOIN
NS_DTDauNam_PhanCap as chitiet_phancap ON chitiet_phancap.iID_CTDTDauNamChiTiet = chitiet.iID_CTDTDauNamChiTiet
--inner join  lnsTem ON  mlns.sLNS  = LNSTEM.Item 
WHERE mlns.iNamLamViec = @YearOfWork
  AND mlns.iTrangThai = 1  
  AND mlns.bHangChaDuToan IS NOT NULL
 AND (mlns.sLNS = '1'
     OR ((mlns.sLNS like '104%'
          AND @LoaiChungTu = '2')
         OR (mlns.sLNS not like '104%'
             AND @LoaiChungTu = '1')))
--AND mlns.sLNS IN (SELECT * from f_split(@Lns))
AND mlns.sLNS IN (select * from #lnsTem)
--AND EXISTS (SELECT *  AS sLNS from f_split(@Lns) where Item = mlns.sLNS)
ORDER BY mlns.sLNS,
         mlns.sL,
         mlns.sK,
         mlns.sM,
         mlns.sTM,
         mlns.sTTM,
         mlns.sNG,
         mlns.sTNG;

drop table #lnsTem;

END
;
;
;
;
GO
