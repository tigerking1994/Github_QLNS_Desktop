/****** Object:  StoredProcedure [dbo].[sp_cp_lns_rpt_thongtri_donvi]    Script Date: 19/08/2022 4:42:40 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_lns_rpt_thongtri_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_lns_rpt_thongtri_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_get_donvi_2]    Script Date: 19/08/2022 4:42:40 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_get_donvi_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_get_donvi_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_get_donvi_2]    Script Date: 19/08/2022 4:42:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_cp_get_donvi_2]
	@YearOfWork int,
	@CapPhatIds nvarchar(max),
	@ILoaiNganSach int
AS
BEGIN
	SELECT dv.* 
	FROM 
	(
		SELECT DISTINCT ct.iID_MaDonVi 
		FROM NS_CP_ChungTuChiTiet ct
		INNER JOIN 
		(SELECT * FROM NS_MucLucNganSach WHERE iNamLamViec = @YearOfWork) ns 
		ON ct.iID_MLNS = ns.iID_MLNS
		WHERE
			iID_CTCapPhat IN (SELECT * FROM f_split(@CapPhatIds))
			AND (@ILoaiNganSach = -1 OR ns.iLoaiNganSach = @ILoaiNganSach) 
	) ctct
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork) dv
	on dv.iID_MaDonVi = ctct.iID_MaDonVi
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_lns_rpt_thongtri_donvi]    Script Date: 19/08/2022 4:42:40 PM ******/
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
		SUM(fTuChi) / @Dvt AS CapPhat
	FROM NS_CP_ChungTuChiTiet ctct
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	INNER JOIN 
		(SELECT * FROM NS_MucLucNganSach WHERE iNamLamViec = @NamLamViec) ns 
	ON ctct.iID_MLNS = ns.iID_MLNS
	WHERE iID_CTCapPhat = @CapPhatId 
		AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@DonViId))
		AND (@ILoaiNganSach = -1 OR ns.iLoaiNganSach = @ILoaiNganSach)
	GROUP BY ctct.iID_MaDonVi, dv.sTenDonVi
	ORDER BY ctct.iID_MaDonVi
END;
;
;
;
GO
