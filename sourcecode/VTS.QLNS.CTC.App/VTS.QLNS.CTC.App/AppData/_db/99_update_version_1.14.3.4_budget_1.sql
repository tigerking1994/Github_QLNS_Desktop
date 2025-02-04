/****** Object:  UserDefinedFunction [dbo].[f_dutoan_tonghop_contain]    Script Date: 16/04/2024 11:15:26 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_dutoan_tonghop_contain]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_dutoan_tonghop_contain]
GO
/****** Object:  UserDefinedFunction [dbo].[f_dutoan_tonghop]    Script Date: 16/04/2024 11:15:26 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_dutoan_tonghop]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_dutoan_tonghop]
GO
/****** Object:  UserDefinedFunction [dbo].[f_dutoan_tonghop]    Script Date: 16/04/2024 11:15:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_dutoan_tonghop]
(    
      @NamLamViec int,
	  @NamNganSach nvarchar(20),
	  @NguonNganSach nvarchar(20),
	  @NgayChungTu datetime,
	  @IdDonVi nvarchar(max),
	  @lns nvarchar(max)
)
RETURNS TABLE
AS RETURN

	SELECT iID_MLNS,
		   iID_MLNS_Cha,
		   iID_MaDonVi,
		   TuChi =sum(fTuChi) + sum(fHangNhap) + sum(fHangMua),
		   HienVat =sum(fHienVat)
	FROM NS_DT_ChungTuChiTiet
	WHERE iNamLamViec = @NamLamViec
	  AND iDuLieuNhan = 0
	  AND (@NamNganSach IS NULL OR iNamNganSach = @NamNganSach)
	  AND (@NguonNganSach IS NULL OR iID_MaNguonNganSach = @NguonNganSach)
	  AND (@IdDonVi IS NULL
		   OR iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi)))
	  AND (@lns IS NULL
		   OR sLNS in (SELECT * FROM f_split(@lns)))
	  AND iID_DTChungTu in
		(SELECT iID_DTChungTu
		 FROM NS_DT_ChungTu
		 WHERE iNamLamViec = @NamLamViec
		   AND (@NamNganSach IS NULL OR iNamNganSach = @NamNganSach)
		   AND (@NguonNganSach IS NULL OR iID_MaNguonNganSach = @NguonNganSach)
		   AND (CAST(dNgayQuyetDinh AS DATE) <= @NgayChungTu)
		   OR (YEAR(CAST(dNgayQuyetDinh AS DATE)) - YEAR(CAST(@NgayChungTu AS DATE)) = 1
		   AND MONTH(CAST(dNgayQuyetDinh AS DATE)) IN (1,2,3) AND MONTH(CAST(@NgayChungTu AS DATE)) = 12))
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi;
;
;
;
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_dutoan_tonghop_contain]    Script Date: 16/04/2024 11:15:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_dutoan_tonghop_contain]
(    
      @NamLamViec int,
	  @NamNganSach nvarchar(100),
	  @NguonNganSach nvarchar(20),
	  @NgayChungTu datetime,
	  @IdDonVi nvarchar(max),
	  @lns nvarchar(max)
)
RETURNS TABLE
AS RETURN

	SELECT iID_MLNS,
		   iID_MLNS_Cha,
		   iID_MaDonVi,
		   TuChi =sum(fTuChi) + sum(fHangNhap) + sum(fHangMua),
		   HienVat =sum(fHienVat)
	FROM NS_DT_ChungTuChiTiet
	WHERE iNamLamViec = @NamLamViec
	  AND iDuLieuNhan = 0
	  AND (@NamNganSach IS NULL OR iNamNganSach in (select * from f_split(@NamNganSach)))
	  AND (@NguonNganSach IS NULL OR iID_MaNguonNganSach = @NguonNganSach)
	  AND (@IdDonVi IS NULL
		   OR iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi)))
	  AND (@lns IS NULL
		   OR sLNS in (SELECT * FROM f_split(@lns)))
	  AND iID_DTChungTu in
		(SELECT iID_DTChungTu
		 FROM NS_DT_ChungTu
		 WHERE iNamLamViec = @NamLamViec
		   AND (@NamNganSach IS NULL OR iNamNganSach in (select * from f_split(@NamNganSach)))
		   AND (@NguonNganSach IS NULL OR iID_MaNguonNganSach = @NguonNganSach)
		   AND (CAST(dNgayQuyetDinh AS DATE) <= @NgayChungTu)
		   OR (YEAR(CAST(dNgayQuyetDinh AS DATE)) - YEAR(CAST(@NgayChungTu AS DATE)) = 1
		   AND MONTH(CAST(dNgayQuyetDinh AS DATE)) IN (1,2,3) AND MONTH(CAST(@NgayChungTu AS DATE)) = 12))
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi;
;
;
;
;
;
GO
