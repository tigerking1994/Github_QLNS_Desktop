/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_phucap_kehoach]    Script Date: 12/10/2022 10:25:29 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_phucap_kehoach]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_phucap_kehoach]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_phucap]    Script Date: 12/10/2022 10:25:29 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_phucap]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_phucap]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_create_data_report_2]    Script Date: 12/10/2022 10:25:29 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_solieuchitiet_create_data_report_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_solieuchitiet_create_data_report_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_all]    Script Date: 12/10/2022 10:25:29 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_all]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_all]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_quyettoan_niendo_tonghop_index]    Script Date: 12/10/2022 10:25:29 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_quyettoan_niendo_tonghop_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_quyettoan_niendo_tonghop_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_quyettoan_niendo_index]    Script Date: 12/10/2022 10:25:29 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_quyettoan_niendo_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_quyettoan_niendo_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_quyettoan_niendo_index]    Script Date: 12/10/2022 10:25:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 07/05/2022
-- Description:	Lấy danh sách hiển thị đê nghị quyết toán niên độ

-- Last update: 04/10/2022
-- Description: Join lại bảng đơn vị (join theo id + mã thay vì năm + mã)
-- =============================================
CREATE PROCEDURE [dbo].[sp_nh_quyettoan_niendo_index]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		qtNienDo.ID					AS Id,
		qtNienDo.iID_ParentID		AS IIdParentId,
		qtNienDo.iID_GocID			AS IIdGocId,
		qtNienDo.sSoDeNghi			AS SSoDeNghi,
		qtNienDo.dNgayDeNghi		AS DNgayDeNghi,
		qtNienDo.iNamKeHoach		AS INamKeHoach,
		donVi.iID_DonVi		AS IIdDonViId,
		qtNienDo.iID_MaDonVi		AS IIdMaDonVi,
		qtNienDo.iID_NguonVonID		AS IIdNguonVonId,
		qtNienDo.iLoaiThanhToan		AS ILoaiThanhToan,
		qtNienDo.iLoaiQuyetToan		AS ILoaiQuyetToan,
		qtNienDo.iCoQuanThanhToan	AS ICoQuanThanhToan,
		qtNienDo.iID_TiGiaID		AS IIdTiGiaId,
		qtNienDo.sMaNgoaiTeKhac		AS SMaNgoaiTeKhac,
		qtNienDo.sMoTa				AS SMoTa,
		qtNienDo.dNgayTao			AS DNgayTao,
		qtNienDo.sNguoiTao			AS SNguoiTao,
		qtNienDo.dNgaySua			AS DNgaySua,
		qtNienDo.sNguoiSua			AS SNguoiSua,
		qtNienDo.dNgayXoa			AS DNgayXoa,
		qtNienDo.sNguoiXoa			AS SNguoiXoa,
		qtNienDo.bIsActive			AS BIsActive,
		qtNienDo.bIsGoc				AS BIsGoc,
		qtNienDo.bIsKhoa			AS BIsKhoa,
		qtNienDo.iLanDieuChinh		AS ILanDieuChinh,
		qtNienDo.bIsXoa				AS BIsXoa,
		donVi.sTenDonVi				AS STenDonVi,
		nguonNganSach.sTen			AS STenNguonVon,
		qtNienDo.iID_TongHopID 		AS iID_TongHopID,
		qtNienDo.sTongHopChildID 	AS sTongHopChildID,
		CASE
			WHEN ILoaiThanhToan = 1 THEN N'Cấp kinh phí'
			WHEN ILoaiThanhToan = 2 THEN N'Tạm ứng'
			WHEN ILoaiThanhToan = 3 THEN N'Thanh toán'
			ELSE ''
		END							AS SLoaiThanhToan,
		CASE
			WHEN ILoaiQuyetToan = 1 THEN N'Thanh toán theo dự án'
			WHEN ILoaiQuyetToan = 2 THEN N'Thanh toán theo hợp đồng'
			ELSE ''
		END							AS SLoaiQuyetToan,
		CASE
			WHEN ICoQuanThanhToan = 1 THEN N'Kho bạc'
			WHEN ICoQuanThanhToan = 2 THEN N'Cơ quan tài chính Bộ Quốc phòng'
			ELSE ''
		END							AS SCoQuanThanhToan
	FROM NH_QT_QuyetToanNienDo qtNienDo
	LEFT JOIN DonVi donVi
		ON qtNienDo.iID_MaDonVi = donVi.iID_MaDonVi AND donVi.iID_DonVi = qtNienDo.iID_DonViID
	LEFT JOIN NguonNganSach nguonNganSach
		ON qtNienDo.iID_NguonVonID = nguonNganSach.iID_MaNguonNganSach
	WHERE qtNienDo.sTongHopChildID IS NULL
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_quyettoan_niendo_tonghop_index]    Script Date: 12/10/2022 10:25:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: LinhND
-- Create date: 16/09/2022
-- Description:	Lấy danh sách hiển thị đê nghị quyết toán niên độ tổng hợp

-- Last update: 04/10/2022
-- Description: Join lại bảng đơn vị theo mã + id thay vì mã + năm làm việc.
-- =============================================

CREATE PROCEDURE [dbo].[sp_nh_quyettoan_niendo_tonghop_index]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		qtNienDo.ID					AS Id,
		qtNienDo.iID_ParentID		AS IIdParentId,
		qtNienDo.iID_GocID			AS IIdGocId,
		qtNienDo.sSoDeNghi			AS SSoDeNghi,
		qtNienDo.dNgayDeNghi		AS DNgayDeNghi,
		qtNienDo.iNamKeHoach		AS INamKeHoach,
		qtNienDo.iID_DonViID		AS IIdDonViId,
		qtNienDo.iID_MaDonVi		AS IIdMaDonVi,
		qtNienDo.iID_NguonVonID		AS IIdNguonVonId,
		qtNienDo.iLoaiThanhToan		AS ILoaiThanhToan,
		qtNienDo.iCoQuanThanhToan	AS ICoQuanThanhToan,
		qtNienDo.iLoaiQuyetToan		AS ILoaiQuyetToan,
		qtNienDo.iID_TiGiaID		AS IIdTiGiaId,
		qtNienDo.sMaNgoaiTeKhac		AS SMaNgoaiTeKhac,
		qtNienDo.sMoTa				AS SMoTa,
		qtNienDo.dNgayTao			AS DNgayTao,
		qtNienDo.sNguoiTao			AS SNguoiTao,
		qtNienDo.dNgaySua			AS DNgaySua,
		qtNienDo.sNguoiSua			AS SNguoiSua,
		qtNienDo.dNgayXoa			AS DNgayXoa,
		qtNienDo.sNguoiXoa			AS SNguoiXoa,
		qtNienDo.bIsActive			AS BIsActive,
		qtNienDo.bIsGoc				AS BIsGoc,
		qtNienDo.bIsKhoa			AS BIsKhoa,
		qtNienDo.iLanDieuChinh		AS ILanDieuChinh,
		qtNienDo.bIsXoa				AS BIsXoa,
		donVi.sTenDonVi				AS STenDonVi,
		nguonNganSach.sTen			AS STenNguonVon,
		qtNienDo.iID_TongHopID 		AS iID_TongHopID,
		qtNienDo.sTongHopChildID 	AS sTongHopChildID,
		IIF(qtNienDo.sTongHopChildID IS NOT NULL, CAST(1 AS BIT), CAST(0 AS BIT)) AS HasChildren,
		CAST(0 AS BIT) 				AS IsShowChildren,
		CASE
			WHEN ILoaiThanhToan = 1 THEN N'Cấp kinh phí'
			WHEN ILoaiThanhToan = 2 THEN N'Tạm ứng'
			WHEN ILoaiThanhToan = 3 THEN N'Thanh toán'
			ELSE ''
		END							AS SLoaiThanhToan,
		CASE
			WHEN ILoaiThanhToan = 1 THEN N'Thanh toán theo dự án'
			WHEN ILoaiThanhToan = 2 THEN N'Thanh toán theo hợp đồng'
			ELSE ''
		END							AS SLoaiQuyetToan,
		CASE
			WHEN ICoQuanThanhToan = 1 THEN N'Kho bạc'
			WHEN ICoQuanThanhToan = 2 THEN N'Cơ quan tài chính Bộ Quốc phòng'
			ELSE ''
		END							AS SCoQuanThanhToan
	FROM NH_QT_QuyetToanNienDo qtNienDo
	LEFT JOIN DonVi donVi ON qtNienDo.iID_MaDonVi = donVi.iID_MaDonVi AND donVi.iID_DonVi = qtNienDo.iID_DonViID
	LEFT JOIN NguonNganSach nguonNganSach ON qtNienDo.iID_NguonVonID = nguonNganSach.iID_MaNguonNganSach
	WHERE qtNienDo.sTongHopChildID IS NOT NULL OR qtNienDo.iID_TongHopID IS NOT NULL
END
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_all]    Script Date: 12/10/2022 10:25:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

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
             fTuChi = SUM(ISNULL(chitiet.fMuaHangCapHienVat, 0) + ISNULL(chitiet.fPhanCap, 0)),
             TuChi2 = 0
      FROM NS_SKT_ChungTuChiTiet chitiet
      LEFT JOIN NS_SKT_MucLuc mucluc ON chitiet.iID_MLSKT = mucluc.iID_MLSKT
      WHERE  chitiet.iNamLamViec=@NamLamViec
        AND chitiet.iLoai in (SELECT * FROM f_split(@Loai))
        AND chitiet.iLoaiChungTu = @LoaiChungTu
        AND (@IdDonVi IS NULL
             OR chitiet.iID_MaDonVi in
               (SELECT *
                FROM f_split(@IdDonVi)))
	 GROUP BY mucluc.sKyHieu

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
                       TuChi = 0 ,
                       TuChi2 = TuChi
      FROM(
		SELECT iID_MaDonVi AS Id_DonVi,
		   XauNoiMa,
		   TuChi =sum(TuChi)
			FROM
			  (SELECT XauNoiMa,
					  iID_MaDonVi,
					  TuChi
				FROM
					(SELECT XauNoiMa = sLNS+'-'+sL+'-'+sK+'-'+sM+'-'+sTM+'-'+sTTM+'-'+sNG, --MoTa,
					iID_MaDonVi,
					TuChi =sum(ISNULL(fHangMua, 0) + ISNULL(fHangNhap, 0) + ISNULL(fPhanCap, 0))
						FROM NS_DTDauNam_ChungTuChiTiet
						WHERE iNamLamViec=@NamLamViec
						AND iLoaiChungTu = @LoaiChungTu
						AND iLoai=3
						AND (@IdDonVi IS NULL
								OR iID_MaDonVi in
								(SELECT *
								FROM f_split(@IdDonVi)))
						GROUP BY sLNS,
								sL,
								sK,
								sM,
								sTM,
								sTTM,
								sNG,
								iID_MaDonVi) AS dt) AS a
		WHERE XauNoiMa IS NOT NULL
		GROUP BY iID_MaDonVi,
					XauNoiMa
		HAVING sum(TuChi)<>0
		  ) AS a
      WHERE dbo.f_get_ky_hieu_by_xaunoima(XauNoiMa,@NamLamViec) IS NOT NULL ) AS a
   GROUP BY KyHieu --order by Tuchi
 ) chitiet ON mucluc.sKyHieu = chitiet.KyHieu
WHERE chitiet.KyHieu IS NOT NULL
  AND mucluc.iNamLamViec = @NamLamViec
ORDER BY sSTTBC END
;
;
;
-- exec [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_all]  '4', '112',2022,1000,'2'

GO
/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_create_data_report_2]    Script Date: 12/10/2022 10:25:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_solieuchitiet_create_data_report_2]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Loai int,
	@TypeGet int,
	@AgencyId nvarchar(500),
	@LoaiChungTu nvarchar(50),
	@ChungTuTongHop nvarchar(max),
	@NguoiTao nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;


	SELECT TOP 1 * INTO #Table_A FROM NS_CauHinh_CanCu
	WHERE sModule = 'BUDGET_DEMANDCHECK_PLAN'
	AND iID_MaChucNang = 'BUDGET_ESTIMATE'
	AND iNamLamViec = @YearOfWork
	AND iNamCanCu = @YearOfWork - 1

    SELECT TOP 1 * INTO #Table_B FROM NS_CauHinh_CanCu
	WHERE sModule = 'BUDGET_DEMANDCHECK_PLAN'
	AND iID_MaChucNang = 'BUDGET_SETTLEMENT'
	AND iNamLamViec = @YearOfWork
	AND iNamCanCu = @YearOfWork - 2

  DELETE NS_DTDauNam_ChungTuChiTiet
  WHERE iNamLamViec = @YearOfWork
  AND ((@TypeGet = 1
        AND iLoai = @Loai)
       OR (@TypeGet = 0
           AND iLoai <> @Loai))
  AND iNamNganSach = @YearOfBudget
  AND iID_MaNguonNganSach =@BudgetSource
  AND iLoaiChungTu = @LoaiChungTu
  AND iID_MaDonVi = @AgencyId; 

  	DECLARE @iIDCauHinhCanCuDuToan nvarchar(200)
	SET @iIDCauHinhCanCuDuToan = (SELECT iID_CauHinh_CanCu FROM #Table_A)
	DECLARE @iIDCauHinhCanCuQuyetToan nvarchar(200)
    SET @iIDCauHinhCanCuQuyetToan = (SELECT iID_CauHinh_CanCu FROM #Table_B)


	DECLARE @chungTuId nvarchar(200)
	SET @chungTuId = (select iID_CTDTDauNam FROM NS_DTDauNam_ChungTu   
	WHERE iNamLamViec = @YearOfWork
	AND iNamNganSach = @YearOfBudget
	AND iID_MaNguonNganSach = @BudgetSource
	AND iLoaiChungTu = @LoaiChungTu
	and iID_MaDonVi = @AgencyId)

	DECLARE @TenDonVi nvarchar(max)
	SET @TenDonVi = (select sTenDonVi FROM DonVi where iNamLamViec = @YearOfWork and iID_MaDonVi = @AgencyId)

INSERT INTO NS_DTDauNam_ChungTuChiTiet(sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sMoTa, sChuong, iNamNganSach, iID_MaNguonNganSach,
iNamLamViec, bHangCha, iLoai, iID_MaDonVi, sTenDonVi, fTuChi, fHienVat, fHangNhap, fHangMua, fPhanCap, fChuaPhanCap,
fDuPhong, fUocThucHien, sGhiChu, dNgayTao, sNguoiTao, dNgaySua, sNguoiSua, bKhoa, iLoaiChungTu, iID_CTDTDauNam)

SELECT 
	   sXauNoiMa,
       sLNS,
       sL,
       sK,
       sM,
       sTM,
       sTTM,
       sNG,
       sTNG,
       sMoTa,
       sChuong,
       iNamNganSach,
       iID_MaNguonNganSach,
       iNamLamViec,
       bHangCha,
       iLoai,
       @AgencyId,
       @TenDonVi,
       sum(fTuChi),
       sum(fHienVat),
       sum(fHangNhap),
       sum(fHangMua),
       sum(fPhanCap),
	   sum(fChuaPhanCap),
       sum(fDuPhong),
	   sum(fUocThucHien),
       '',
       GETDATE(),
       'sNguoiTao',
       GETDATE(),
       '',
       bKhoa,
       iLoaiChungTu,
	   @chungTuId
FROM NS_DTDauNam_ChungTuChiTiet

WHERE iNamLamViec = @YearOfWork
  AND ((@TypeGet = 1
        AND iLoai = @Loai)
       OR (@TypeGet = 0
           AND iLoai <> @Loai))
  AND iNamNganSach = @YearOfBudget
  AND iID_MaNguonNganSach =@BudgetSource
  AND iLoaiChungTu = @LoaiChungTu
  AND iID_MaDonVi IN (
	select iID_MaDonVi FROM NS_DTDauNam_ChungTu
	where iNamLamViec = @YearOfWork and iID_MaNguonNganSach = @BudgetSource and iNamNganSach = @YearOfBudget and iLoaiChungTu = @LoaiChungTu and bKhoa = 1 )

	and iID_CTDTDauNam IN (SELECT * FROM f_split(@ChungTuTongHop))
	
group by sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sMoTa, sChuong, iNamNganSach, iID_MaNguonNganSach, iNamLamViec,
bHangCha,bKhoa, iLoaiChungTu, iLoai

UPDATE NS_DTDauNam_ChungTuChiTiet
SET bKhoa = 1
WHERE iNamLamViec = @YearOfWork
  AND ((@TypeGet = 1
        AND iLoai = @Loai)
       OR (@TypeGet = 0
           AND iLoai <> @Loai))
  AND iID_CTDTDauNam IN (SELECT * FROM f_split(@ChungTuTongHop))
  AND iNamNganSach = @YearOfBudget
  AND iID_MaNguonNganSach =@BudgetSource
  AND iLoaiChungTu = @LoaiChungTu;

  --danh dau chung tu da tong hop
update NS_DTDauNam_ChungTu set bDaTongHop = 0 
where iNamLamViec = @YearOfWork 
		and iNamNganSach = @YearOfBudget 
		and iID_MaNguonNganSach = @BudgetSource
		and iLoaiChungTu = @LoaiChungTu;
update NS_DTDauNam_ChungTu set bDaTongHop = 1 
where iID_CTDTDauNam in
    (SELECT *
     FROM f_split(@ChungTuTongHop))





---------------------------------------------------------------------- Xoa NS_DTDauNam_ChungTuChiTiet_CanCu
DELETE FROM NS_DTDauNam_ChungTuChiTiet_CanCu
WHERE iNamLamViec = @YearOfWork
AND iNamNganSach = @YearOfBudget
AND iID_MaNguonNganSach = @BudgetSource
AND iLoaiChungTu = @LoaiChungTu
AND iID_MaDonVi = @AgencyId; 

---------------------------------------------------------------------- Du Toan Nam Truoc
SELECT * INTO #DuToanNamTruoc FROM NS_DT_ChungTu 
WHERE iNamLamViec = @YearOfWork - 1
AND bKhoa = 1
AND iLoai = 0
AND iNamNganSach = @YearOfBudget
AND iID_MaNguonNganSach = @BudgetSource
AND iLoaiDuToan = 1

IF EXISTS (SELECT * FROM #DuToanNamTruoc)   

INSERT INTO NS_DTDauNam_ChungTuChiTiet_CanCu
(iID, bHangCha, bKhoa, dNgaySua, dNgayTao, fChuaPhanCap, 
fHangMua, fHangNhap, fHienVat, fPhanCap, fTuChi, iID_CanCu, iID_MaDonVi, iID_MaNguonNganSach,
iLoaiChungTu, iNamLamViec, iNamNganSach, sGhiChu, sK, sL, sLNS, sM, sMoTa, sNG, sNguoiSua,
sNguoiTao, sTenDonVi, sTM, sTNG, sTTM, sXauNoiMa)
SELECT
NEWID(), 0, 0, GETDATE(), GETDATE(), 0,
SUM(fHangMua), SUM(fHangNhap), SUM(fHienVat), SUM(fPhanCap), SUM(fTuChi),
iID_CanCu = @iIDCauHinhCanCuDuToan, iID_MaDonVi = @AgencyId,
iID_MaNguonNganSach, iLoaiChungTu = 1, iNamLamViec = @YearOfWork, iNamNganSach = @YearOfBudget,
sGhiChu = '', sK, sL, sLNS, sM, sMoTa = '', sNG, sNguoiSua = @NguoiTao, 
sNguoiTao = @NguoiTao, sTenDonVi = @TenDonVi, sTM, sTNG, sTTM, sXauNoiMa
FROM NS_DT_ChungTuChiTiet WHERE iID_DTChungTu IN (SELECT iID_DTChungTu FROM #DuToanNamTruoc)
GROUP BY iID_MaNguonNganSach, iNamLamViec, iNamNganSach, sK, sL, sLNS, sM, sNG, sTM, sTNG, sTTM, sXauNoiMa

ELSE 

INSERT INTO NS_DTDauNam_ChungTuChiTiet_CanCu
(iID, bHangCha, bKhoa, dNgaySua, dNgayTao, fChuaPhanCap, 
fHangMua, fHangNhap, fHienVat, fPhanCap, fTuChi, iID_CanCu, iID_MaDonVi, iID_MaNguonNganSach,
iLoaiChungTu, iNamLamViec, iNamNganSach, sGhiChu, sK, sL, sLNS, sM, sMoTa, sNG, sNguoiSua,
sNguoiTao, sTenDonVi, sTM, sTNG, sTTM, sXauNoiMa)
SELECT
NEWID(), 0, 0, GETDATE(), GETDATE(), 0,
SUM(fHangMua), SUM(fHangNhap), SUM(fHienVat), SUM(fPhanCap), SUM(fTuChi),
iID_CanCu = @iIDCauHinhCanCuDuToan, iID_MaDonVi = @AgencyId,
iID_MaNguonNganSach, iLoaiChungTu = 1, iNamLamViec = @YearOfWork, iNamNganSach = @YearOfBudget,
sGhiChu = '', sK, sL, sLNS, sM, sMoTa = '', sNG, sNguoiSua = @NguoiTao, 
sNguoiTao = @NguoiTao, sTenDonVi = @TenDonVi, sTM, sTNG, sTTM, sXauNoiMa

FROM NS_DTDauNam_ChungTuChiTiet_CanCu WHERE
iID_CanCu = @iIDCauHinhCanCuDuToan
AND iNamLamViec = @YearOfWork
AND iNamNganSach = @YearOfBudget
AND iID_MaDonVi IN (SELECT iID_MaDonVi FROM NS_DTDauNam_ChungTu WHERE bDaTongHop = 1 AND iID_CTDTDauNam IN (SELECT * FROM f_split(@ChungTuTongHop)))
GROUP BY iID_MaNguonNganSach, iNamLamViec, iNamNganSach, sK, sL, sLNS, sM, sNG, sTM, sTNG, sTTM, sXauNoiMa


---------------------------------------------------------------------- Quyet Toan Nam Truoc
SELECT * INTO #QuyetToanNamTruoc FROM NS_QT_ChungTu 
WHERE iNamLamViec = @YearOfWork - 2
AND iNamNganSach = @YearOfBudget
AND iID_MaNguonNganSach = @BudgetSource
AND iID_MaDonVi = @AgencyId
AND bKhoa = 1

IF EXISTS (SELECT * FROM #QuyetToanNamTruoc)   

INSERT INTO NS_DTDauNam_ChungTuChiTiet_CanCu
(iID, bHangCha, bKhoa, dNgaySua, dNgayTao, fChuaPhanCap, 
fHangMua, fHangNhap, fHienVat, fPhanCap, fTuChi, iID_CanCu, iID_MaDonVi, iID_MaNguonNganSach,
iLoaiChungTu, iNamLamViec, iNamNganSach, sGhiChu, sK, sL, sLNS, sM, sMoTa, sNG, sNguoiSua,
sNguoiTao, sTenDonVi, sTM, sTNG, sTTM, sXauNoiMa)
SELECT
NEWID(), 0, 0, GETDATE(), GETDATE(), 0,
0, 0, 0, 0, SUM(fTuChi_PheDuyet),
iID_CanCu = @iIDCauHinhCanCuQuyetToan, iID_MaDonVi = @AgencyId,
iID_MaNguonNganSach, iLoaiChungTu = 1, iNamLamViec = @YearOfWork, iNamNganSach = @YearOfBudget,
sGhiChu = '', sK, sL, sLNS, sM, sMoTa = '', sNG, sNguoiSua = @NguoiTao, 
sNguoiTao = @NguoiTao, sTenDonVi = @TenDonVi, sTM, sTNG, sTTM, sXauNoiMa
FROM NS_QT_ChungTuChiTiet WHERE iID_QTChungTu IN (SELECT iID_QTChungTu FROM #QuyetToanNamTruoc)
GROUP BY iID_MaNguonNganSach, iNamLamViec, iNamNganSach, sK, sL, sLNS, sM, sNG, sTM, sTNG, sTTM, sXauNoiMa

ELSE 

INSERT INTO NS_DTDauNam_ChungTuChiTiet_CanCu
(iID, bHangCha, bKhoa, dNgaySua, dNgayTao, fChuaPhanCap, 
fHangMua, fHangNhap, fHienVat, fPhanCap, fTuChi, iID_CanCu, iID_MaDonVi, iID_MaNguonNganSach,
iLoaiChungTu, iNamLamViec, iNamNganSach, sGhiChu, sK, sL, sLNS, sM, sMoTa, sNG, sNguoiSua,
sNguoiTao, sTenDonVi, sTM, sTNG, sTTM, sXauNoiMa)
SELECT
NEWID(), 0, 0, GETDATE(), GETDATE(), 0,
SUM(fHangMua), SUM(fHangNhap), SUM(fHienVat), SUM(fPhanCap), SUM(fTuChi),
iID_CanCu = @iIDCauHinhCanCuQuyetToan, iID_MaDonVi = @AgencyId,
iID_MaNguonNganSach, iLoaiChungTu = 1, iNamLamViec = @YearOfWork, iNamNganSach = @YearOfBudget,
sGhiChu = '', sK, sL, sLNS, sM, sMoTa = '', sNG, sNguoiSua = @NguoiTao, 
sNguoiTao = @NguoiTao, sTenDonVi = @TenDonVi, sTM, sTNG, sTTM, sXauNoiMa

FROM NS_DTDauNam_ChungTuChiTiet_CanCu WHERE
iID_CanCu = @iIDCauHinhCanCuQuyetToan
AND iNamLamViec = @YearOfWork
AND iNamNganSach = @YearOfBudget
AND iID_MaDonVi IN (SELECT iID_MaDonVi FROM NS_DTDauNam_ChungTu WHERE bDaTongHop = 1 AND iID_CTDTDauNam IN (SELECT * FROM f_split(@ChungTuTongHop)))
GROUP BY iID_MaNguonNganSach, iNamLamViec, iNamNganSach, sK, sL, sLNS, sM, sNG, sTM, sTNG, sTTM, sXauNoiMa


END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_phucap]    Script Date: 12/10/2022 10:25:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_tl_get_data_phucap] @maCanBo AS nvarchar(50) AS BEGIN
SELECT PhuCap.Id AS Id,
       PhuCap.Ma_PhuCap AS MaPhuCap,
       PhuCap.Parent AS Parent,
	   PhuCap.bGiaTri as BGiaTri,
	   PhuCap.bHuongPc_Sn as BHuongPcSn,
	   PhuCap.Ten_PhuCap AS TenPhuCap,
       CONCAT(PhuCapCha.Ma_PhuCap, '-', PhuCapCha.Ten_PhuCap) AS ParentName,
       CanboPhucap.DateStart AS DateStart,
       CanboPhucap.ISoThang_Huong AS ISoThangHuong,
       CanboPhucap.HuongPC_SN AS HuongPCSN,
     (case
     when CanboPhucap.GIA_TRI is null then PhuCap.Gia_Tri
     else CanboPhucap.GIA_TRI
     end) as GiaTri,
	 PhuCap.FGiaTriNhoNhat,
	 PhuCap.FGiaTriLonNhat,
	 PhuCap.fGiaTriPhuCap_KemTheo as FGiaTriPhuCapKemTheo,
	 PhuCap.iId_PhuCap_KemTheo as IIdPhuCapKemTheo,
	 PhuCap.iId_Ma_PhuCap_KemTheo as IIdMaPhuCapKemTheo
FROM TL_DM_PhuCap PhuCap
LEFT JOIN TL_DM_PhuCap PhuCapCha ON PhuCap.Parent = PhuCapCha.Ma_PhuCap
LEFT JOIN TL_CanBo_PhuCap AS CanboPhucap ON PhuCap.Ma_PhuCap = CanboPhucap.MA_PHUCAP
AND CanboPhucap.MA_CBO = @maCanBo
WHERE PhuCap.Chon = 1
  AND PhuCap.Is_Formula = 0
  AND PhuCap.Is_Readonly = 0
  AND PhuCap.Parent IN ( select Ma_PhuCap from TL_DM_PhuCap where Parent = '' and Chon = 1)
Order By ParentName
end
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_phucap_kehoach]    Script Date: 12/10/2022 10:25:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_tl_get_data_phucap_kehoach] @maCanBo AS nvarchar(50) AS BEGIN
SELECT PhuCap.Id AS Id,
       PhuCap.Ma_PhuCap AS MaPhuCap,
       PhuCap.Parent AS Parent,
	   PhuCap.Ten_PhuCap AS TenPhuCap,
	   PhuCap.bGiaTri as BGiaTri,
	   PhuCap.bHuongPc_Sn as BHuongPcSn,
       CONCAT(PhuCapCha.Ma_PhuCap, '-', PhuCapCha.Ten_PhuCap) AS ParentName,
       CanboPhucap.GIA_TRI AS GiaTri,
       CanboPhucap.DateStart AS DateStart,
       CanboPhucap.ISoThang_Huong AS ISoThangHuong,
       CanboPhucap.HuongPC_SN AS HuongPCSN,
		 PhuCap.FGiaTriNhoNhat,
		 PhuCap.FGiaTriLonNhat,
		 PhuCap.fGiaTriPhuCap_KemTheo as FGiaTriPhuCapKemTheo,
		 PhuCap.iId_PhuCap_KemTheo as IIdPhuCapKemTheo,
		 PhuCap.iId_Ma_PhuCap_KemTheo as IIdMaPhuCapKemTheo
FROM TL_DM_PhuCap PhuCap
LEFT JOIN TL_DM_PhuCap PhuCapCha ON PhuCap.Parent = PhuCapCha.Ma_PhuCap
LEFT JOIN TL_CanBo_PhuCap_KeHoach AS CanboPhucap ON PhuCap.Ma_PhuCap = CanboPhucap.MA_PHUCAP
AND CanboPhucap.Ma_CanBo = @maCanBo
WHERE PhuCap.Chon = 1
  AND PhuCap.Is_Formula = 0
  AND PhuCap.Is_Readonly = 0
  AND PhuCap.Parent IN ( select Ma_PhuCap from TL_DM_PhuCap where Parent = '' and Chon = 1)
Order By ParentName
END
;
;
GO
