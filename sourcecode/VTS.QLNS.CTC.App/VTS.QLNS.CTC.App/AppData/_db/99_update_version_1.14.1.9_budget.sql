/****** Object:  StoredProcedure [dbo].[sp_cp_get_donvi_2]    Script Date: 3/22/2024 2:01:58 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_get_donvi_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_get_donvi_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_get_donvi_2]    Script Date: 3/22/2024 2:01:58 PM ******/
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
		WHERE
			iID_CTCapPhat IN (SELECT * FROM f_split(@CapPhatIds))
			AND (@ILoaiNganSach = -1 OR 
				(@ILoaiNganSach = 0 and ct.sLNS like '101%') OR
				(@ILoaiNganSach = 1 and ct.sLNS like '102%') OR
				(@ILoaiNganSach = 2 and ct.sLNS like '2%') OR
				(@ILoaiNganSach = 3 and ct.sLNS like '4%') OR
				(@ILoaiNganSach = 4 and ct.sLNS like '109%')
				) 
	) ctct
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork) dv
	on dv.iID_MaDonVi = ctct.iID_MaDonVi
END
;
;
;
;
GO
