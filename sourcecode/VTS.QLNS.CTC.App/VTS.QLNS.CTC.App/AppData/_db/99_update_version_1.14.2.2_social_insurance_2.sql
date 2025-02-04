/****** Object:  StoredProcedure [dbo].[sp_bh_create_summary_tham_dinh_quyet_toan]    Script Date: 3/26/2024 6:01:36 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_create_summary_tham_dinh_quyet_toan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_create_summary_tham_dinh_quyet_toan]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_lns_rpt_thongtri_donvi]    Script Date: 3/27/2024 9:31:18 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_lns_rpt_thongtri_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_lns_rpt_thongtri_donvi]
GO

/****** Object:  StoredProcedure [dbo].[sp_bh_create_summary_tham_dinh_quyet_toan]    Script Date: 3/26/2024 6:01:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_create_summary_tham_dinh_quyet_toan]
@IdChungTu nvarchar(MAX),
@NguoiTao nvarchar(500),
@YearOfWork int,
@IdChungTuSummary nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

INSERT INTO BH_ThamDinhQuyetToan_ChungTuChiTiet(
	   [iID_BH_TDQT_ChungTuChiTiet]
      ,[fCNVLDHD]
      ,[fQuanNhan]
      ,[fSoBaoCao]
      ,[fSoThamDinh]
      ,[iID_BH_TDQT_ChungTu]
      ,[iID_MaDonVi]
      ,[iMa]
      ,[iNamLamViec]
      ,[sGhiChu]
	)
SELECT 
	   NEWID(),
	   SUM(ISNULL([fCNVLDHD], 0)),
	   SUM(ISNULL([fQuanNhan], 0)),
	   SUM(ISNULL([fSoBaoCao], 0)),
	   SUM(ISNULL([fSoThamDinh], 0)),
	   @IdChungTuSummary,
       '000',
       [iMa],
       @YearOfWork,
       ''

FROM BH_ThamDinhQuyetToan_ChungTuChiTiet
WHERE  [iID_BH_TDQT_ChungTu] IN
    (SELECT *
     FROM f_split(@IdChungTu))
GROUP BY [iMa]

UPDATE BH_ThamDinhQuyetToan_ChungTu SET bDaTongHop=1  WHERE [iID_BH_TDQT_ChungTu] IN (SELECT * FROM f_split(@IdChungTu));
END
;
;
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_cp_lns_rpt_thongtri_donvi]    Script Date: 3/27/2024 9:31:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_lns_rpt_thongtri_donvi] 
	@NamLamViec int,
    @CapPhatId nvarchar(100),
	@DonViId nvarchar(max),
	@Dvt int,
	@ILoaiNganSach int
AS
BEGIN
SET NOCOUNT ON;
	SELECT ctct.iID_MaDonVi AS MaDonVi,
		dv.sTenDonVi AS TenDonVi,
		ROUND((SUM(fTuChi) / @Dvt), 0) AS CapPhat
	FROM NS_CP_ChungTuChiTiet ctct
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	INNER JOIN 
		(SELECT * FROM NS_MucLucNganSach WHERE iNamLamViec = @NamLamViec) ns 
	ON ctct.iID_MLNS = ns.iID_MLNS
	WHERE iID_CTCapPhat = @CapPhatId 
		AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@DonViId))
		--AND (@ILoaiNganSach = -1 OR ns.iLoaiNganSach = @ILoaiNganSach)
		AND (@ILoaiNganSach = -1
			OR (@ILoaiNganSach = 0 and ns.sLNS like '101%')
			OR (@ILoaiNganSach = 1 and ns.sLNS like '102%')
			OR (@ILoaiNganSach = 2 and ns.sLNS like '2%')
			OR (@ILoaiNganSach = 3 and ns.sLNS like '4%')
			OR (@ILoaiNganSach = 4 and ns.sLNS like '109%')
		)
	GROUP BY ctct.iID_MaDonVi, dv.sTenDonVi
	ORDER BY ctct.iID_MaDonVi
END;
;
;
;
;
GO