/****** Object:  StoredProcedure [dbo].[sp_bk_tonghop]    Script Date: 24/07/2023 1:59:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bk_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bk_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bk_chungtu_danhsach]    Script Date: 24/07/2023 1:59:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bk_chungtu_danhsach]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bk_chungtu_danhsach]
GO
/****** Object:  StoredProcedure [dbo].[sp_bk_chungtu_danhsach]    Script Date: 24/07/2023 1:59:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_bk_chungtu_danhsach]
	@VoucherListId nvarchar(100),
	@YearOfWork int
AS
BEGIN
	SELECT ctct.*, dv.sTenDonVi FROM NS_BK_ChungTuChiTiet ctct
	LEFT JOIN (SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	WHERE ctct.iID_BKChungTu = @VoucherListId
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bk_tonghop]    Script Date: 24/07/2023 1:59:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_bk_tonghop]
	@YearOfWork int,
	@QuarterMonth int,
	@LNS nvarchar(max),
	@AgencyId nvarchar(100),
	@DataType int,
	@Dvt int,
    @Loai nvarchar(100)
AS
BEGIN
	SELECT mlns.sLNS,
       mlns.sL,
       mlns.sK,
       mlns.sM,
       mlns.sTM,
       mlns.sTTM,
       mlns.sNG,
       mlns.sTNG,
       mlns.sXauNoiMa,
       mlns.iID_MLNS,
       mlns.iID_MLNS_Cha,
       mlns.sMoTa,
       NoiDung,
       sSoChungTu,
       dNgayChungTu,
       sSoQuyetDinh,
	   sLoai,
	   iLoaiChi,
       sTenDonVi,
       fTongTuChi / @Dvt AS TuChi,
       fTongHienVat / @Dvt AS HienVat INTO #tblBkTongHop
	FROM
	  (SELECT sLNS,
			  sL,
			  sK,
			  sM,
			  sTM,
			  sTTM,
			  sNG,
			  sTNG,
			  sXauNoiMa,
			  iID_MLNS,
			  iID_MLNS_Cha,
			  ctct.sMoTa AS NoiDung,
			  iID_BKChungTu,
			  sSoChungTu,
			  sLoai,
			  iLoaiChi,
			  dNgayChungTu,
			  dv.iID_MaDonVi,
			  dv.sTenDonVi,
			  fTongTuChi,
			  fTongHienVat
	   FROM NS_BK_ChungTuChiTiet ctct
	   LEFT JOIN DonVi dv ON dv.iID_MaDonVi = ctct.iID_MaDonVi AND dv.iNamLamViec = ctct.iNamLamViec
	   WHERE iTrangThai=1
		 AND iThangQuy=@QuarterMonth
		 AND (@LNS IS NULL
			  OR sLNS in
				(SELECT *
				 FROM f_split(@LNS)))
		 AND (@AgencyId IS NULL
			  OR ctct.iID_MaDonVi in
				(SELECT *
				 FROM f_split(@AgencyId)))
		 AND ctct.iNamLamViec = @YearOfWork 
	     AND (ISNULL(@Loai, '0') = '0' OR ctct.sLoai = @Loai)
		 AND (@DataType IS NULL
			  OR (@DataType=1
				  AND fTongTuChi<>0)
			  OR (@DataType=2
				  AND fTongHienVat<>0)) ) AS ctct -- lay so ghi so

	LEFT JOIN
	  (SELECT iID_BKChungTu,
			  sSoQuyetDinh,
			  dNgayQuyetDinh
	   FROM NS_BK_ChungTu
	   WHERE iNamLamViec=@YearOfWork) AS ct ON ct.iID_BKChungTu = ctct.iID_BKChungTu
	LEFT JOIN
	  (SELECT *
	   FROM NS_MucLucNganSach
	   WHERE iNamLamViec = @YearOfWork
		 AND iTrangThai = 1) AS mlns ON mlns.iID_MLNS = ctct.iID_MLNS;

	WITH LNSTreeParent AS
	  (SELECT sLNS,
			  sL,
			  sK,
			  sM,
			  sTM,
			  sTTM,
			  sNG,
			  sTNG,
			  sXauNoiMa,
			  iID_MLNS,
			  iID_MLNS_Cha,
			  sMoTa ,
			  cast(NoiDung AS nvarchar(MAX)) AS NoiDung ,
			  cast(sSoChungTu AS nvarchar(500)) AS SoChungTu ,
			  CONVERT(NVARCHAR(100), dNgayChungTu, 103) AS NgayChungTu ,
			  cast(sSoQuyetDinh AS nvarchar(500)) AS SoQuyetDinh ,
			  cast(sTenDonVi AS nvarchar(500)) AS TenDonVi ,
			  cast(sLoai AS nvarchar(500)) AS SLoai ,
			  iLoaiChi AS ILoaiChi,
			  cast(TuChi AS float) AS TuChi ,
			  cast(HienVat AS float) AS HienVat ,
			  cast(0 AS bit) AS IsHangCha
	   FROM #tblBkTongHop
	   UNION ALL SELECT mlnsParent.sLNS,
						mlnsParent.sL,
						mlnsParent.sK,
						mlnsParent.sM,
						mlnsParent.sTM,
						mlnsParent.sTTM,
						mlnsParent.sNG,
						mlnsParent.sTNG,
						mlnsParent.sXauNoiMa,
						mlnsParent.iID_MLNS,
						mlnsParent.iID_MLNS_Cha,
						mlnsParent.sMoTa ,
						cast('' AS nvarchar(MAX)) AS NoiDung ,
						cast('' AS nvarchar(500)) AS SoChungTu ,
						cast('' AS nvarchar(100)) AS NgayChungTu ,
						cast('' AS nvarchar(500)) AS SoQuyetDinh ,
						cast('' AS nvarchar(500)) AS TenDonVi ,
						cast('' AS nvarchar(500)) AS SLoai ,
						cast(0 AS int) AS ILoaiChi,
						cast(0 AS float) AS TuChi ,
						cast(0 AS float) AS HienVat ,
						cast(1 AS bit) AS IsHangCha
	   FROM NS_MucLucNganSach mlnsParent
	   INNER JOIN LNSTreeParent ON mlnsParent.iID_MLNS = LNSTreeParent.iID_MLNS_Cha
	   WHERE mlnsParent.iNamLamViec = @YearOfWork )
	SELECT DISTINCT *
	FROM LNSTreeParent
	ORDER BY sLNS,
			 sL,
			 sK,
			 sM,
			 sTM,
			 sTTM,
			 sNG;


	DROP TABLE #tblBkTongHop;
END
;
;
;
GO
