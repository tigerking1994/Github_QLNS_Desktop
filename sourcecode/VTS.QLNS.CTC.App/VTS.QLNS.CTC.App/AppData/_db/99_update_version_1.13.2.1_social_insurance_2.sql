/****** Object:  StoredProcedure [dbo].[sp_qtc_qkpql_create_data_summary_bh]    Script Date: 9/29/2023 3:16:27 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_qkpql_create_data_summary_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_qkpql_create_data_summary_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_namkinhphi_quanly_gettien_thuchi]    Script Date: 9/29/2023 3:16:27 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_namkinhphi_quanly_gettien_thuchi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_namkinhphi_quanly_gettien_thuchi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_nkp_ql_chitiet]    Script Date: 9/29/2023 3:16:27 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtc_nkp_ql_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtc_nkp_ql_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_nkp_ql_chitiet]    Script Date: 9/29/2023 3:16:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chi tiet chung tu 
CREATE PROCEDURE [dbo].[sp_bh_qtc_nkp_ql_chitiet]
	 @NamLamViec int,
	 @iD uniqueidentifier,
	 @IdDonVi nvarchar(max),
	 @LNS nvarchar(max)
AS
BEGIN 
	SET NOCOUNT ON;
	DECLARE @iDCT uniqueidentifier='00000000-0000-0000-0000-000000000000';
	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
			into #tblMlnsByPhanCap
	FROM BH_DM_MucLucNganSach 
	WHERE 
		iNamLamViec = 2023 
		AND iTrangThai = 1
		--AND (
		--	(@PhanCap = 'M' AND (sTM IS NULL OR sTM = ''))
		--	OR (@PhanCap = 'TM' AND (sTTM IS NULL OR sTTM = ''))
		--	OR (@PhanCap = 'NG' AND (sTNG IS NULL OR sTNG = ''))
		--	)
		AND sLNS IN (SELECT * FROM f_split(@LNS))
		--(
		--			SELECT DISTINCT VALUE
		--			FROM 
		--			(
		--				SELECT 
		--					CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1, 
		--					CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3, 
		--					CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5, 
		--					CAST(sLNS AS nvarchar(10)) LNS 
		--				FROM
		--					NS_NguoiDung_LNS 
		--				WHERE 
		--					sMaNguoiDung = @UserName
		--					AND INamLamViec = @NamLamViec
		--					AND sLNS IN (SELECT * FROM f_split(@LNS))
		--			) LNS
		--			UNPIVOT
		--			(
		--				value
		--				FOR col in (LNS1, LNS3, LNS5, LNS)
		--			) un) 

SELECT   isnull(ctct.ID_QTC_Nam_KinhPhiQuanLy_ChiTiet, @iDCT) as ID_QTC_Nam_KinhPhiQuanLy_ChiTiet,
		@iD as IID_QTC_Nam_KinhPhiQuanLy,
		mlns.iID_MLNS as iID_MucLucNganSach,
		mlns.iID_MLNS_Cha as IdParent,
		mlns.sXauNoiMa as sXauNoiMa,
		mlns.sLNS as SLNS,
		mlns.sL as SL,
		mlns.sK as SK,
		mlns.sM as SM,
		mlns.sTM as STM,
		mlns.sTTM as STTM,
		mlns.sNG as SNG,
		mlns.sTNG as STNG,
		mlns.sMoTa as SNoiDung,
		@NamLamViec AS INamLamViec,
		mlns.bHangCha as IsHangCha,
		isnull(ctct.dNgayTao, getdate()) AS dNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS dNgaySua,
		ctct.sNguoiTao AS dNgayTao,
		ctct.sNguoiSua AS dNgaySua,
		ISNULL(ctct.fTien_DuToanNamTruocChuyenSang, 0)  as fTien_DuToanNamTruocChuyenSang, 
		ISNULL(ctct.fTien_DuToanGiaoNamNay, 0)  as fTien_DuToanGiaoNamNay,
		ISNULL(ctct.fTien_TongDuToanDuocGiao, 0)  as fTien_TongDuToanDuocGiao, 
		ISNULL(ctct.fTien_ThucChi, 0)  as fTien_ThucChi, 
		ISNULL(ctct.fTien_ThucChi, 0)  as fTienThua, 
		ISNULL(ctct.fTienThieu, 0)  as fTienThieu, 
		ISNULL(ctct.fTiLeThucHienTrenDuToan, 0)  as fTiLeThucHienTrenDuToan
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(SELECT * FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet 
				WHERE iID_QTC_Nam_KinhPhiQuanLy in
					( SELECT ID_QTC_Nam_KinhPhiQuanLy FROM BH_QTC_Nam_KinhPhiQuanLy
								WHERE iNamChungTu=@NamLamViec
								AND ID_QTC_Nam_KinhPhiQuanLy=@iD
								AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
								)) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
	Order by mlns.sXauNoiMa
END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_namkinhphi_quanly_gettien_thuchi]    Script Date: 9/29/2023 3:16:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 20/09/2023
-- Description:	Lấy danh sách hiển thị Tien Quyet Toan Da Duyet theo quý của chứng từ quyết toán chi quy kinh phí quản lý chi tiết
CREATE PROCEDURE [dbo].[sp_qtc_namkinhphi_quanly_gettien_thuchi]
	@YearOfWork int,
	@AgencyId nvarchar(500),
	@VoucherDate datetime,
	@iQuyChungTu int
AS
BEGIN

	SELECT  
		SUM(fTienThucChi) AS FTien_ThucChi,
		CT.iID_MaDonVi,
		CTCT.iID_MucLucNganSach
	FROM
	BH_QTC_Quy_KinhPhiQuanLy CT
	INNER JOIN BH_QTC_Quy_KinhPhiQuanLy_chiTiet CTCT
	ON CT.ID_QTC_Quy_KinhPhiQuanLy=CTCT.iID_QTC_Quy_KinhPhiQuanLy
	WHERE CT.iNamChungTu = @YearOfWork
          AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
		  AND CT.bIsKhoa=1
		  AND CT.iQuyChungTu=@iQuyChungTu
	GROUP BY  CT.iID_MaDonVi,CTCT.iID_MucLucNganSach

END

GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_qkpql_create_data_summary_bh]    Script Date: 9/29/2023 3:16:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_qtc_qkpql_create_data_summary_bh]
@IdChungTu nvarchar(MAX),
@NguoiTao nvarchar(500),
@YearOfWork int,
@LstIdChungTuSummary nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

INSERT INTO BH_QTC_Nam_KinhPhiQuanLy_ChiTiet
(ID_QTC_Nam_KinhPhiQuanLy_ChiTiet,
iID_QTC_Nam_KinhPhiQuanLy,
iID_MucLucNganSach,
sM,
sTM,
sNoiDung,
dNgaySua,
dNgayTao,
sNguoiSua,
sNguoiTao,
fTien_DuToanNamTruocChuyenSang,
fTien_DuToanGiaoNamNay,
fTien_TongDuToanDuocGiao,
fTien_ThucChi,
fTienThua,
fTienThieu,
fTiLeThucHienTrenDuToan
 )
SELECT 
	   NEWID(),
	   @IdChungTu,
       iID_MucLucNganSach,
       sM,
	   sTM,
	   sNoiDung,
	   null,
	   GETDATE(),
	   @NguoiTao,
	   '',
	   SUM(fTien_DuToanNamTruocChuyenSang),
	   SUM(fTien_DuToanGiaoNamNay),
	   SUM(fTien_TongDuToanDuocGiao),
	   SUM(fTien_ThucChi),
	   SUM(fTienThua),
	   SUM(fTienThieu),
	   SUM(fTiLeThucHienTrenDuToan)
FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet
WHERE  iID_QTC_Nam_KinhPhiQuanLy IN
    (SELECT *
     FROM f_split(@LstIdChungTuSummary))
group by iID_MucLucNganSach,sM,sTM,sNoiDung

UPDATE BH_QTC_Nam_KinhPhiQuanLy SET iLoaiTongHop = 1 WHERE ID_QTC_Nam_KinhPhiQuanLy IN (SELECT * FROM f_split(@LstIdChungTuSummary));
END
;
;
GO
