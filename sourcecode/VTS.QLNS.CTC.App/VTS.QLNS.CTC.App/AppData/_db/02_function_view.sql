/****** Object:  View [dbo].[V_PhanBoDuAn]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[V_PhanBoDuAn]'))
DROP VIEW [dbo].[V_PhanBoDuAn]
GO
/****** Object:  UserDefinedFunction [dbo].[f_skt_dutoan]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_skt_dutoan]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_skt_dutoan]
GO
/****** Object:  UserDefinedFunction [dbo].[f_rpt_get_mlns]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_rpt_get_mlns]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_rpt_get_mlns]
GO
/****** Object:  UserDefinedFunction [dbo].[f_recursive_donvi]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_recursive_donvi]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_recursive_donvi]
GO
/****** Object:  UserDefinedFunction [dbo].[f_quyettoan_tonghop]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_quyettoan_tonghop]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_quyettoan_tonghop]
GO
/****** Object:  UserDefinedFunction [dbo].[f_quyettoan]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_quyettoan]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_quyettoan]
GO
/****** Object:  UserDefinedFunction [dbo].[f_qt_dot]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_qt_dot]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_qt_dot]
GO
/****** Object:  UserDefinedFunction [dbo].[f_qt_done]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_qt_done]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_qt_done]
GO
/****** Object:  UserDefinedFunction [dbo].[f_ns_DTDV]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_ns_DTDV]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_ns_DTDV]
GO
/****** Object:  UserDefinedFunction [dbo].[f_ns]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_ns]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_ns]
GO
/****** Object:  UserDefinedFunction [dbo].[f_mlns_by_lns]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_mlns_by_lns]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_mlns_by_lns]
GO
/****** Object:  UserDefinedFunction [dbo].[f_loai_hinh_by_lns]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_loai_hinh_by_lns]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_loai_hinh_by_lns]
GO
/****** Object:  UserDefinedFunction [dbo].[f_loai_cong_trinh_get_list_childrent]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_loai_cong_trinh_get_list_childrent]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_loai_cong_trinh_get_list_childrent]
GO
/****** Object:  UserDefinedFunction [dbo].[f_lct_by_loai_cong_trinh]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_lct_by_loai_cong_trinh]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_lct_by_loai_cong_trinh]
GO
/****** Object:  UserDefinedFunction [dbo].[f_get_parent_mlns_by_lns_xau_noi_ma_report_dutoandaunam]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_get_parent_mlns_by_lns_xau_noi_ma_report_dutoandaunam]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_get_parent_mlns_by_lns_xau_noi_ma_report_dutoandaunam]
GO
/****** Object:  UserDefinedFunction [dbo].[f_get_parent_mlns_by_lns_xau_noi_ma]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_get_parent_mlns_by_lns_xau_noi_ma]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_get_parent_mlns_by_lns_xau_noi_ma]
GO
/****** Object:  UserDefinedFunction [dbo].[f_get_parent_mlns_by_lns_mlns_id]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_get_parent_mlns_by_lns_mlns_id]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_get_parent_mlns_by_lns_mlns_id]
GO
/****** Object:  UserDefinedFunction [dbo].[f_get_mlns_by_lns]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_get_mlns_by_lns]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_get_mlns_by_lns]
GO
/****** Object:  UserDefinedFunction [dbo].[f_dutoan_tonghop]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_dutoan_tonghop]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_dutoan_tonghop]
GO
/****** Object:  UserDefinedFunction [dbo].[f_dt_dot_ngayquyetdinh_full]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_dt_dot_ngayquyetdinh_full]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_dt_dot_ngayquyetdinh_full]
GO
/****** Object:  UserDefinedFunction [dbo].[f_dt_dot_full]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_dt_dot_full]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_dt_dot_full]
GO
/****** Object:  UserDefinedFunction [dbo].[f_dt_ngayquyetdinh_full]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_dt_ngayquyetdinh_full]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_dt_ngayquyetdinh_full]
GO
/****** Object:  UserDefinedFunction [dbo].[f_dt_ngayquyetdinh]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_dt_ngayquyetdinh]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_dt_ngayquyetdinh]
GO
/****** Object:  UserDefinedFunction [dbo].[f_skt_cancu]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_skt_cancu]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_skt_cancu]
GO
/****** Object:  UserDefinedFunction [dbo].[f_skt_dulieu]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_skt_dulieu]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_skt_dulieu]
GO
/****** Object:  UserDefinedFunction [dbo].[f_dt_full]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_dt_full]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_dt_full]
GO
/****** Object:  UserDefinedFunction [dbo].[f_dt]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_dt]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_dt]
GO
/****** Object:  UserDefinedFunction [dbo].[splitstring]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[splitstring]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[splitstring]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetNgayLapThongTriGanNhat]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_GetNgayLapThongTriGanNhat]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_GetNgayLapThongTriGanNhat]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_CheckKieuDuAn_KeHoachNamDeXuat]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_CheckKieuDuAn_KeHoachNamDeXuat]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_CheckKieuDuAn_KeHoachNamDeXuat]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_CheckGiaTriPhanBoTang]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_CheckGiaTriPhanBoTang]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_CheckGiaTriPhanBoTang]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_CheckGiaTriPhanBoGiam]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_CheckGiaTriPhanBoGiam]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_CheckGiaTriPhanBoGiam]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_CheckDieuKienDuAn]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_CheckDieuKienDuAn]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_CheckDieuKienDuAn]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Calculator_GiaTriDauTu_CuoiCung_By_DuAnId]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_Calculator_GiaTriDauTu_CuoiCung_By_DuAnId]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_Calculator_GiaTriDauTu_CuoiCung_By_DuAnId]
GO
/****** Object:  UserDefinedFunction [dbo].[f_split_lns]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_split_lns]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_split_lns]
GO
/****** Object:  UserDefinedFunction [dbo].[f_split_empty]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_split_empty]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_split_empty]
GO
/****** Object:  UserDefinedFunction [dbo].[f_split]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_split]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_split]
GO
/****** Object:  UserDefinedFunction [dbo].[f_luong_ntn]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_luong_ntn]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_luong_ntn]
GO
/****** Object:  UserDefinedFunction [dbo].[f_get_ky_hieu_by_xaunoima]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_get_ky_hieu_by_xaunoima]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_get_ky_hieu_by_xaunoima]
GO
/****** Object:  UserDefinedFunction [dbo].[f_check_contain]    Script Date: 5/20/2022 9:02:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_check_contain]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_check_contain]
GO
/****** Object:  UserDefinedFunction [dbo].[f_check_contain]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create FUNCTION  [dbo].[f_check_contain]
(    
	  @IdDonVi1 nvarchar(max),
	  @IdDonVi2 nvarchar(max)
)
RETURNS int
as
BEGIN
	
  RETURN  (select CAST( count(*) as int) from (
		select * FROM f_split(@IdDonVi1)
				) a
  
  inner join 
	( select * FROM f_split(@IdDonVi2)) 
	b
	
	on a.Item = b.Item)

	end
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_get_ky_hieu_by_xaunoima]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_get_ky_hieu_by_xaunoima]
(    
	  @XauNoiMa nvarchar(max),
	  @NamLamViec int
)
RETURNS nvarchar(max)
AS
BEGIN
	RETURN (
		SELECT TOP 1 mucluc.sKyHieu FROM NS_SKT_MucLuc mucluc 
		INNER JOIN NS_MLSKT_MLNS map ON mucluc.sKyHieu = map.sSKT_KyHieu
		WHERE 
			mucluc.iNamLamViec = @NamLamViec
			AND map.iNamLamViec = @NamLamViec 
			AND map.sNS_XauNoiMa = @XauNoiMa
	)
END
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_luong_ntn]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 04/05/2022
-- Description:	Tính năm thâm niên
-- =============================================
CREATE FUNCTION [dbo].[f_luong_ntn]
(
	@NgayNN DATETIME,
	@NgayXN DATETIME,
	@NgayTN DATETIME,
	@ThangTNN int,
	@Thang int,
	@Nam int
)
RETURNS int
AS
BEGIN
	DECLARE @NamThamNien int SET @NamThamNien = 0

	IF (@NgayNN IS NOT NULL)
	BEGIN
		IF (@NgayXN IS NULL AND @NgayTN IS NULL)
		BEGIN
			SET @NamThamNien = (12 * (@Nam - YEAR(@NgayNN)) + @Thang - MONTH(@NgayNN) + @ThangTNN) / 12
		END

		ELSE
		BEGIN
			IF (@NgayTN IS NULL)
			BEGIN
				SET @NamThamNien = (12 * (YEAR(@NgayXN) - YEAR(@NgayNN)) + MONTH(@NgayXN) - MONTH(@NgayNN) + @ThangTNN) / 12
			END

			ELSE
			BEGIN
				DECLARE @Lan1 int SET @Lan1 = 12 * (YEAR(@NgayXN) - YEAR(@NgayNN)) + MONTH(@NgayXN) - MONTH(@NgayNN)
				
				IF (@Lan1 < 6)
				BEGIN
					SET @NamThamNien = (12 * (@Nam - YEAR(@NgayTN)) + @Thang - MONTH(@NgayTN) + @ThangTNN) / 12
				END

				ELSE 
				BEGIN
					DECLARE @ThoiGianNgoaiQD int SET @ThoiGianNgoaiQD = 12 * (YEAR(@NgayTN) - YEAR(@NgayXN)) + MONTH(@NgayTN) - MONTH(@NgayXN)

					IF (@ThoiGianNgoaiQD <= 12)
					BEGIN
						SET @NamThamNien = (12 * (@Nam - YEAR(@NgayNN)) + @Thang - MONTH(@NgayNN) + @ThangTNN) / 12
					END

					ELSE
					BEGIN
						SET @NamThamNien = (@Lan1 + 12 * (@Nam - YEAR(@NgayNN)) + @Thang - MONTH(@NgayNN) + @ThangTNN) / 12
					END
				END
			END
		END
	END
	RETURN @NamThamNien
END
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_split]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create FUNCTION [dbo].[f_split]
(    
      @Input NVARCHAR(MAX)
)
RETURNS @Output TABLE (
      Item NVARCHAR(1000)
)
AS
BEGIN
      DECLARE @StartIndex INT, @EndIndex INT
      DECLARE @Character CHAR(1)


      SET @StartIndex = 1
	  SET @Character = ','

      IF SUBSTRING(@Input, LEN(@Input) - 1, LEN(@Input)) <> @Character
      BEGIN
            SET @Input = @Input + @Character
      END

      WHILE CHARINDEX(@Character, @Input) > 0
      BEGIN
            SET @EndIndex = CHARINDEX(@Character, @Input)

            INSERT INTO @Output(Item)
            SELECT SUBSTRING(@Input, @StartIndex, @EndIndex - 1)

            SET @Input = SUBSTRING(@Input, @EndIndex + 1, LEN(@Input))
      END

      RETURN
END
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_split_empty]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create FUNCTION [dbo].[f_split_empty]
(    
      @Input NVARCHAR(MAX)
)
RETURNS NVARCHAR(500)
AS
BEGIN
      DECLARE @StartIndex INT, @EndIndex INT
      DECLARE @Character CHAR(1)
	  DECLARE @Name NVARCHAR(500)

      SET @StartIndex = 1
	  SET @Character = ' '

      IF SUBSTRING(@Input, LEN(@Input) - 1, LEN(@Input)) <> @Character
      BEGIN
            SET @Input = @Input + @Character
      END

      WHILE CHARINDEX(@Character, @Input) > 0
      BEGIN
            SET @EndIndex = CHARINDEX(@Character, @Input)

             SET @Name = SUBSTRING(@Input, @StartIndex, @EndIndex - 1)

            SET @Input = SUBSTRING(@Input, @EndIndex + 1, LEN(@Input))
      END

      RETURN @Name
END
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_split_lns]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_split_lns]
(    
      @Input NVARCHAR(MAX)
)
RETURNS @Output TABLE (
      Item NVARCHAR(1000)
)
AS
BEGIN
      DECLARE @StartIndex INT, @EndIndex INT
      DECLARE @Character CHAR(1)
	  DECLARE @InputSplit nvarchar(100)

      SET @StartIndex = 1
	  SET @Character = ','

      IF SUBSTRING(@Input, LEN(@Input) - 1, LEN(@Input)) <> @Character
      BEGIN
            SET @Input = @Input + @Character
      END

      WHILE CHARINDEX(@Character, @Input) > 0
      BEGIN
            SET @EndIndex = CHARINDEX(@Character, @Input)
			SET @InputSplit = SUBSTRING(@Input, @StartIndex, @EndIndex - 1)

            INSERT INTO @Output(Item)
            SELECT @InputSplit

			INSERT INTO @Output(Item)
            SELECT LEFT(@InputSplit, 1)

			INSERT INTO @Output(Item)
            SELECT LEFT(@InputSplit, 3)

			INSERT INTO @Output(Item)
            SELECT LEFT(@InputSplit, 5)

            SET @Input = SUBSTRING(@Input, @EndIndex + 1, LEN(@Input))
      END

      RETURN
END
;
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Calculator_GiaTriDauTu_CuoiCung_By_DuAnId]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_Calculator_GiaTriDauTu_CuoiCung_By_DuAnId] 
(
	@DuAnID uniqueidentifier
)
RETURNS float
AS
BEGIN	
	DECLARE @result float , @GiaTriDauTuInQDDT float;
	-- Tính tiền giá trị đầu tư trong quyết định đầu tư nguồn vốn.
	SET @GiaTriDauTuInQDDT = (
							select 
								ISNULL(SUM(qddtnv.fTienPheDuyet),0) 
							from 
								VDT_DA_QDDauTu_NguonVon qddtnv
							inner join
								VDT_DA_QDDauTu qddt
							on qddtnv.iID_QDDauTuID = qddt.iID_QDDauTuID
							where 
								qddt.iID_DuAnID = @DuAnID
						 )
	IF (@GiaTriDauTuInQDDT != 0)
		BEGIN
			SET @result=@GiaTriDauTuInQDDT;
		end	
	Else
		BEGIN
			--Tính tiền hạn mức đầu tư của dự án
			SET @result = (
							SELECT ISNULL(fHanMucDauTu,0) FROM VDT_DA_DuAn WHERE iID_DuAnID = @DuAnID
						 )
		end
	Return @result
END
;
GO
/****** Object:  UserDefinedFunction [dbo].[fn_CheckDieuKienDuAn]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_CheckDieuKienDuAn] 
(
@DuAnID uniqueidentifier,
@NgayLap DateTime
)
RETURNS bit 
AS
BEGIN	
	-- Kiểm tra nếu tồn tại dự án tại thỏa mãn điệu kiện chưa kết thúc hoặc null thì Trả về dự án thỏa mãn điều kiện.
	DECLARE @TrangThai bit,@GTTT float,@GTKB float,@GTQT float,@kq decimal
	SET @TrangThai=0;
	
	IF Exists (SELECT *	FROM VDT_DA_DuAn da WHERE da.iID_DuAnID = @DuAnID AND (da.bIsKetThuc = 0 OR da.bIsKetThuc IS NULL))
		BEGIN
			SET @TrangThai= 1;			
		END
	Else
	BEGIN
		--Kiểm tra (Giá trị quyết toán - (Giá trị thanh toán + Giá trị kho bạc) > 0 ) .Trả về dự án thỏa mãn điều kiện.
			--Tính tiền của dự án trong bảng Đề nghị thanh toán.
			SET @GTTT =(Select isnull(SUM((ISNULL(dntt.fGiaTriThanhToanNN, 0) + ISNULL(dntt.fGiaTriThanhToanTN, 0))),0)
								 from VDT_TT_DeNghiThanhToan dntt
								Where (dntt.dNgayDeNghi IS NULL OR dntt.dNgayDeNghi<= @ngayLap) And (dntt.fGiaTriThanhToanNN is NOT NULL OR dntt.fGiaTriThanhToanTN is NOT NULL )
									And dntt.iID_DuAnID=@DuAnID)
			--Tính tiền của dự án trong kho bạc.							
			SET @GTKB=(Select isnull(SUM(ttkbct.fGiaTriThanhToan * ttkbct.fTiGia * ttkbct.fTiGiaDonVi),0)
								 from VDT_TT_ThanhToanQuaKhoBac_ChiTiet ttkbct join VDT_TT_ThanhToanQuaKhoBac ttkb
								ON ttkb.Id=ttkbct.iID_ThanhToanID
								Where ttkb.dNgayThanhToan=(SELECT TOP 1 ttkb1.dNgayThanhToan from VDT_TT_ThanhToanQuaKhoBac_ChiTiet ttkbct1 join VDT_TT_ThanhToanQuaKhoBac ttkb1
															ON ttkb1.Id=ttkbct1.iID_ThanhToanID WHERE ttkbct1.iID_DuAnID=@DuAnID
															ORDER BY ttkb1.dNgayThanhToan DESC)
										And ttkbct.iID_DuAnID=@DuAnID)
			-- Tính tiền của dự án trong quyết toán.
			SET @GTQT=(Select isnull(SUM(qt.fTienQuyetToanPheDuyet * qt.fTiGia * qt.fTiGiaDonVi),0)
								 from VDT_QT_QuyetToan qt WHERE qt.iID_DuAnID=@DuAnID)
			SET @kq= cast(@GTQT AS decimal)- cast(@GTTT AS decimal) -  cast(@GTKB AS decimal)			
		-- kiểm tra nếu dự án là kết thúc mà quyết toán hoàn thành lớn hơn thanh toán của dự án									
		IF ( @kq > 0 ANd (Exists (SELECT *	FROM VDT_DA_DuAn da WHERE da.iID_DuAnID = @DuAnID AND da.bIsKetThuc=1)) )
			BEGIN
				SET @TrangThai=1;
			End													
	End
	Return @TrangThai
END
;
GO
/****** Object:  UserDefinedFunction [dbo].[fn_CheckGiaTriPhanBoGiam]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_CheckGiaTriPhanBoGiam] 
(
	@giaTriPhanBo float
)
RETURNS float
AS
BEGIN	
	DECLARE @kq float
	IF @giaTriPhanBo < 0 OR @giaTriPhanBo Is null
		BEGIN
			SET @kq= ISNULL(@giaTriPhanBo,0)
		End
	Else 
		BEGIN
			SET @kq= 0
		end
	Return @kq
END
;
GO
/****** Object:  UserDefinedFunction [dbo].[fn_CheckGiaTriPhanBoTang]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_CheckGiaTriPhanBoTang] 
(
@giaTriPhanBo float
)
RETURNS float
AS
BEGIN	
	DECLARE @kq float
	IF @giaTriPhanBo > 0 OR @giaTriPhanBo Is null
		BEGIN
			SET @kq= ISNULL(@giaTriPhanBo,0)
		End
	Else 
		BEGIN
			SET @kq= 0
		end
	Return @kq
END
;
GO
/****** Object:  UserDefinedFunction [dbo].[fn_CheckKieuDuAn_KeHoachNamDeXuat]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_CheckKieuDuAn_KeHoachNamDeXuat] 
(
@iIdDuAn uniqueidentifier,
@iNamKeHoach int
)
RETURNS int
AS
BEGIN	
	DECLARE @iCountDuAn int = (SELECT COUNT(dt.Id)
								FROM VDT_KHV_PhanBoVon_DonVi as tbl
								INNER JOIN VDT_KHV_PhanBoVon_DonVi_ChiTiet as dt on tbl.Id = dt.iId_PhanBoVon_DonVi
								INNER JOIN VDT_DA_DuAn as da on dt.iID_DuAnID = da.iID_DuAnID
								WHERE tbl.iNamKeHoach = (@iNamKeHoach - 1) AND dt.iID_DuAnID = @iIdDuAn AND da.sTrangThaiDuAn = 'THUC_HIEN')
	if(ISNULL(@iCountDuAn, 0) > 0) 
	BEGIN
		return 2
	end
	return 1
END
;
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetNgayLapThongTriGanNhat]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_GetNgayLapThongTriGanNhat] 
(@id uniqueidentifier, @sMaDonVi nvarchar(50), @iNamKeHoach int, @sMaNguonVon nvarchar(50), @iIdLoaiThongTri uniqueidentifier)
RETURNS DATE
AS
BEGIN
	RETURN (SELECT TOP(1)dNgayThongTri
			FROM VDT_ThongTri
			WHERE Id <> @id AND iID_MaDonViID = @sMaDonVi AND iNamThongTri = @iNamKeHoach AND sMaNguonVon = @sMaNguonVon AND iID_LoaiThongTriID = @iIdLoaiThongTri
			ORDER BY dNgayThongTri DESC)
END
;
GO
/****** Object:  UserDefinedFunction [dbo].[splitstring]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[splitstring] (@stringToSplit VARCHAR(MAX) )
RETURNS
 @returnList TABLE ([Name] [nvarchar] (500))
AS
BEGIN

 DECLARE @name NVARCHAR(255)
 DECLARE @pos INT

 WHILE CHARINDEX(',', @stringToSplit) > 0
 BEGIN
  SELECT @pos  = CHARINDEX(',', @stringToSplit)  
  SELECT @name = SUBSTRING(@stringToSplit, 1, @pos-1)

  INSERT INTO @returnList 
  SELECT @name

  SELECT @stringToSplit = SUBSTRING(@stringToSplit, @pos+1, LEN(@stringToSplit)-@pos)
 END

 INSERT INTO @returnList
 SELECT @stringToSplit

 RETURN
END
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_dt]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_dt]
(    
      @NamLamViec int,
	  @NamNganSach nvarchar(20),
	  @NguonNganSach nvarchar(20),
	  @lns nvarchar(500),
	  @NgayChungTu datetime,
	  @dvt int,
	  @LoaiDuToan int
)
RETURNS TABLE
AS RETURN
SELECT sLNS AS LNS,
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
       sXauNoiMa AS XauNoiMa,
       sMoTa AS MoTa, --Id_DonVi,
 TuChi =sum(fTuChi) /@dvt,
 HienVat =sum(fHienVat) /@dvt,
 HangNhap =sum(fHangNhap) /@dvt,
 HangMua =sum(fHangMua) /@dvt,
 PhanCap =sum(fPhanCap) /@dvt,
 DuPhong =sum(fDuPhong) /@dvt
FROM NS_DT_ChungTuChiTiet
WHERE iNamLamViec=@NamLamViec
  AND (@NamNganSach IS NULL
       OR iNamNganSach in
         (SELECT *
          FROM f_split(@NamNganSach)))
  AND (@NguonNganSach IS NULL
       OR iID_MaNguonNganSach in
         (SELECT *
          FROM f_split(@NguonNganSach)))
  AND iPhanCap=0
  AND (@lns IS NULL
       OR sLNS in
         (SELECT *
          FROM f_split(@lns)))
  AND iID_DTChungTu in
    (SELECT iID_DTChungTu
     FROM NS_DT_ChungTu
     WHERE iNamLamViec=@NamLamViec
       AND ((iLoaiDuToan = @LoaiDuToan)
            OR (@LoaiDuToan = 0))
       AND (@NamNganSach IS NULL
            OR iNamNganSach in
              (SELECT *
               FROM f_split(@NamNganSach)))
       AND (@NguonNganSach IS NULL
            OR iID_MaNguonNganSach in
              (SELECT *
               FROM f_split(@NguonNganSach)))
       AND iLoai=0
       AND cast(dNgayChungTu AS date) <= cast(@NgayChungTu AS date) )
GROUP BY sLNS,
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
         sXauNoiMa,
         sMoTa
HAVING sum(fTuChi)<>0
OR sum(fHienVat)<>0
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_dt_full]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_dt_full]
(    
      @NamLamViec int,
	  @NamNganSach nvarchar(20),
	  @NguonNganSach nvarchar(20),
	  @lns nvarchar(500),
	  @NgayChungTu datetime,
	  @dvt int,
	  @LoaiDuToan int
)
RETURNS TABLE
AS RETURN

select	
		LNS,L,K,M,TM,TTM,NG,TNG,TNG1,TNG2,TNG3,
		XauNoiMa,MoTa,
		TuChi		=sum(TuChi),
		HienVat		=sum(HienVat)
from 
(
	-- trenphanbo

	select 
		LNS,L,K,M,TM,TTM,NG,TNG,TNG1,TNG2,TNG3
		,XauNoiMa,MoTa
		,TuChi
		,HienVat
	from f_dt(@NamLamViec,@NamNganSach,@NguonNganSach,@lns,@NgayChungTu,@dvt,@LoaiDuToan)

	-- hangnhap
	union all
	select 
		LNS='1040200',L,K,M,TM,TTM,NG,TNG,TNG1,TNG2,TNG3
		,XauNoiMa='1040200'+'-'+L+'-'+K+'-'+M+'-'+TM+'-'+TTM+'-'+NG+(case tng when '' then '' else (+'-'+TNG) end)
		,MoTa
		,TuChi		=HangNhap
		,HienVat=0
	from f_dt(@NamLamViec,@NamNganSach,@NguonNganSach,'1040100',@NgayChungTu,@dvt,@LoaiDuToan)
	where	(@lns is null or '1040100' in (select * from f_split(@lns)))

	-- hangmua
	union all
	select 
		LNS='1040300',L,K,M,TM,TTM,NG,TNG,TNG1,TNG2,TNG3
		,XauNoiMa='1040300'+'-'+L+'-'+K+'-'+M+'-'+TM+'-'+TTM+'-'+NG+(case tng when '' then '' else (+'-'+TNG) end)
		,MoTa
		,TuChi		=HangMua
		,HienVat=0
	from f_dt(@NamLamViec,@NamNganSach,@NguonNganSach,'1040100',@NgayChungTu,@dvt,@LoaiDuToan)
	where	(@lns is null or '1040100' in (select * from f_split(@lns)))

)as a
group by LNS, L,K,M,TM,TTM,NG,TNG,TNG1,TNG2,TNG3,XauNoiMa,MoTa
having	sum(TuChi)<>0 or sum(HienVat)<>0
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_skt_dulieu]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_skt_dulieu] (
@NamLamViec int,
@id_donvi nvarchar(MAX),
@LoaiChungTu nvarchar(50)) 
RETURNS TABLE 
AS 
RETURN
SELECT iID_MaDonVi AS Id_DonVi,
       sLNS AS LNS,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS NG,
       sMoTa AS MoTa ,
       XauNoiMa ,
       DuToan =sum(DuToan) ,
       QuyetToan =sum(QuyetToan)
FROM
  (SELECT XauNoiMa = sLNS+'-'+sL+'-'+sK+'-'+sM+'-'+sTM+'-'+sTTM+'-'+sNG,
          sLNS,
          sL,
          sK,
          sM,
          sTM,
          sTTM,
          sNG,
          sMoTa,
          iID_MaDonVi,
          DuToan =sum(fTuChi),
          QuyetToan=0
   FROM NS_DT_ChungTuChiTiet
   WHERE
       (SELECT count(*)
        FROM NS_DTDauNam_ChungTuChiTiet
        WHERE iNamLamViec=@NamLamViec-1
          AND iLoai=2
          AND iLoaiChungTu = @LoaiChungTu)=0
     AND iNamLamViec=(@NamLamViec-1)
     AND iID_DTChungTu in
       (SELECT iID_DTChungTu
        FROM NS_DT_ChungTu
        WHERE iNamLamViec=@NamLamViec-1
          AND iLoai=1)
     AND (@id_donvi IS NULL
          OR iID_MaDonVi in
            (SELECT *
             FROM f_split(@id_donvi)))
   GROUP BY sLNS,
            sL,
            sK,
            sM,
            sTM,
            sTTM,
            sNG,
            sMoTa,
            iID_MaDonVi -- Lấy số dự toán đã đẩy vào làm căn cứ

   UNION ALL SELECT XauNoiMa = sLNS+'-'+sL+'-'+sK+'-'+sM+'-'+sTM+'-'+sTTM+'-'+sNG,
                    sLNS,
                    sL,
                    sK,
                    sM,
                    sTM,
                    sTTM,
                    sNG,
                    sMoTa,
                    iID_MaDonVi,
                    CASE
                        WHEN @LoaiChungTu = '1' THEN sum(fTuChi)
                        WHEN @LoaiChungTu = '2' THEN sum(fHangNhap) + SUM (fHangMua) + sum(fPhanCap)
                        ELSE 0
                    END AS DuToan,
                    QuyetToan=0
   FROM NS_DTDauNam_ChungTuChiTiet
   WHERE iLoaiChungTu = @LoaiChungTu
     AND iNamLamViec=(@NamLamViec-1)
     AND (@id_donvi IS NULL
          OR iID_MaDonVi in
            (SELECT *
             FROM f_split(@id_donvi)))
   GROUP BY sLNS,
            sL,
            sK,
            sM,
            sTM,
            sTTM,
            sNG,
            sMoTa,
            iID_MaDonVi
   UNION ALL SELECT XauNoiMa = sLNS+'-'+sL+'-'+sK+'-'+sM+'-'+sTM+'-'+sTTM+'-'+sNG,
                    sLNS,
                    sL,
                    sK,
                    sM,
                    sTM,
                    sTTM,
                    sNG,
                    sMoTa,
                    iID_MaDonVi,
                    DuToan=0,
                    CASE
                        WHEN @LoaiChungTu = '1' THEN sum(fTuChi)
                        WHEN @LoaiChungTu = '2' THEN sum(fHangNhap) + SUM (fHangMua) + sum(fPhanCap)
                        ELSE 0
                    END AS QuyetToan
   FROM NS_DTDauNam_ChungTuChiTiet
   WHERE iNamLamViec=(@NamLamViec-2)
     AND iLoai=1
     AND iLoaiChungTu = @LoaiChungTu
     AND (@id_donvi IS NULL
          OR iID_MaDonVi in
            (SELECT *
             FROM f_split(@id_donvi)))
   GROUP BY sLNS,
            sL,
            sK,
            sM,
            sTM,
            sTTM,
            sNG,
            sMoTa,
            iID_MaDonVi) AS a
WHERE sLNS like '1%'
GROUP BY iID_MaDonVi,
         sLNS,
         sL,
         sK,
         sM,
         sTM,
         sTTM,
         sNG,
         sMoTa ,
         XauNoiMa
HAVING sum(DuToan)<>0
OR sum(QuyetToan)<>0
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_skt_cancu]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create FUNCTION [dbo].[f_skt_cancu] (
@NamLamViec int,
@id_donvi nvarchar(MAX),
@LoaiChungTu nvarchar(MAX)) 
RETURNS 
TABLE 
AS 
RETURN
SELECT Id_DonVi,
       SKT_XauNoiMa,
       DuToan =sum(DuToan),
       QuyetToan =sum(QuyetToan)
FROM
  (SELECT XauNoiMa,
 Id_DonVi,
 m.SKT_XauNoiMa,
 DuToan,
 QuyetToan
   FROM
     (SELECT XauNoiMa,
             DuToan,
             QuyetToan,
             Id_DonVi
      FROM f_skt_dulieu(@NamLamViec, @id_donvi, @LoaiChungTu)) AS dt
   LEFT JOIN
     (SELECT *
      FROM
        (-- lấy bộ máp skt_mucluc -> mlns
 SELECT map.sNS_XauNoiMa AS SKT_XauNoiMa,
        sSKT_KyHieu,
        map.sNS_XauNoiMa AS NS_XauNoiMa
         FROM NS_MLSKT_MLNS AS MAP
         LEFT JOIN
           (SELECT *
            FROM NS_SKT_MucLuc
            WHERE iNamLamViec=@NamLamViec) AS ml ON ml.sKyHieu=map.sSKT_KyHieu) AS mucluc) AS m ON m.NS_XauNoiMa=dt.XauNoiMa) AS a 

WHERE SKT_XauNoiMa IS NOT NULL
GROUP BY Id_DonVi,
         SKT_XauNoiMa

HAVING sum(DuToan)<>0
OR sum(QuyetToan)<>0
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_dt_ngayquyetdinh]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_dt_ngayquyetdinh]
(    
      @NamLamViec int,
	  @NamNganSach nvarchar(20),
	  @NguonNganSach nvarchar(20),
	  @lns nvarchar(500),
	  @NgayQuyetDinh datetime,
	  @dvt int,
	  @LoaiDuToan int
)
RETURNS TABLE
AS RETURN
SELECT sLNS AS LNS,
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
       sXauNoiMa AS XauNoiMa,
       sMoTa AS MoTa, --Id_DonVi,
 TuChi =sum(fTuChi) /@dvt,
 HienVat =sum(fHienVat) /@dvt,
 HangNhap =sum(fHangNhap) /@dvt,
 HangMua =sum(fHangMua) /@dvt,
 PhanCap =sum(fPhanCap) /@dvt,
 DuPhong =sum(fDuPhong) /@dvt
FROM NS_DT_ChungTuChiTiet
WHERE iNamLamViec=@NamLamViec
  AND (@NamNganSach IS NULL
       OR iNamNganSach in
         (SELECT *
          FROM f_split(@NamNganSach)))
  AND (@NguonNganSach IS NULL
       OR iID_MaNguonNganSach in
         (SELECT *
          FROM f_split(@NguonNganSach)))
  AND iPhanCap=0
  AND (@lns IS NULL
       OR sLNS in
         (SELECT *
          FROM f_split(@lns)))
  AND iID_DTChungTu in
    (SELECT iID_DTChungTu
     FROM NS_DT_ChungTu
     WHERE iNamLamViec=@NamLamViec
       AND ((iLoaiDuToan = @LoaiDuToan)
            OR (@LoaiDuToan = 0))
       AND (@NamNganSach IS NULL
            OR iNamNganSach in
              (SELECT *
               FROM f_split(@NamNganSach)))
       AND (@NguonNganSach IS NULL
            OR iID_MaNguonNganSach in
              (SELECT *
               FROM f_split(@NguonNganSach)))
       AND iLoai=0
       AND cast(dNgayQuyetDinh AS date) <= cast(@NgayQuyetDinh AS date) )
GROUP BY sLNS,
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
         sXauNoiMa,
         sMoTa
HAVING sum(fTuChi)<>0
OR sum(fHienVat)<>0
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_dt_ngayquyetdinh_full]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_dt_ngayquyetdinh_full]
(    
      @NamLamViec int,
	  @NamNganSach nvarchar(20),
	  @NguonNganSach nvarchar(20),
	  @lns nvarchar(500),
	  @NgayQuyetDinh datetime,
	  @dvt int,
	  @LoaiDuToan int
)
RETURNS TABLE
AS RETURN

select	
		LNS,L,K,M,TM,TTM,NG,TNG,TNG1,TNG2,TNG3,
		XauNoiMa,MoTa
		,TuChi		=sum(TuChi),
		HienVat		=sum(HienVat)
from 
(
	-- trenphanbo

	select 
		LNS,L,K,M,TM,TTM,NG,TNG,TNG1,TNG2,TNG3
		,XauNoiMa,MoTa
		,TuChi
		,HienVat
	from f_dt_ngayquyetdinh(@NamLamViec,@NamNganSach,@NguonNganSach,@lns,@NgayQuyetDinh,@dvt,@LoaiDuToan)

	-- hangnhap
	union all
	select 
		LNS='1040200',L,K,M,TM,TTM,NG,TNG,TNG1,TNG2,TNG3
		,XauNoiMa='1040200'+'-'+L+'-'+K+'-'+M+'-'+TM+'-'+TTM+'-'+NG+(case tng when '' then '' else (+'-'+TNG) end)
		,MoTa
		,TuChi		=HangNhap
		,HienVat=0
	from f_dt_ngayquyetdinh(@NamLamViec,@NamNganSach,@NguonNganSach,'1040100',@NgayQuyetDinh,@dvt,@LoaiDuToan)
	where	(@lns is null or '1040100' in (select * from f_split(@lns)))

	-- hangmua
	union all
	select 
		LNS='1040300',L,K,M,TM,TTM,NG,TNG,TNG1,TNG2,TNG3
		,XauNoiMa='1040300'+'-'+L+'-'+K+'-'+M+'-'+TM+'-'+TTM+'-'+NG+(case tng when '' then '' else (+'-'+TNG) end)
		,MoTa
		,TuChi		=HangMua
		,HienVat=0
	from f_dt_ngayquyetdinh(@NamLamViec,@NamNganSach,@NguonNganSach,'1040100',@NgayQuyetDinh,@dvt,@LoaiDuToan)
	where	(@lns is null or '1040100' in (select * from f_split(@lns)))

)as a
group by LNS, L,K,M,TM,TTM,NG,TNG,TNG1,TNG2,TNG3,XauNoiMa,MoTa
having	sum(TuChi)<>0 or sum(HienVat)<>0
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_dt_dot_full]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_dt_dot_full]
(    
      @NamLamViec int,
	  @NamNganSach nvarchar(20),
	  @NguonNganSach nvarchar(20),
	  @lns nvarchar(500),
	  @NgayChungTu datetime,
	  @dvt int,
	  @LoaiDuToan int
)
RETURNS TABLE
AS RETURN

SELECT sLNS AS LNS,
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
       sXauNoiMa AS XauNoiMa,
       sMoTa AS MoTa,
       TuChi =sum(fTuChi)/@dvt,
       HienVat =sum(fHienVat)/@dvt
FROM NS_DT_ChungTuChiTiet
WHERE iNamLamViec=@NamLamViec
  AND (@NamNganSach IS NULL
       OR iNamNganSach in
         (SELECT *
          FROM f_split(@NamNganSach)))
  AND (@NguonNganSach IS NULL
       OR iID_MaNguonNganSach in
         (SELECT *
          FROM f_split(@NguonNganSach)))
  AND iPhanCap=0
  AND (@lns IS NULL
       OR sLNS in
         (SELECT *
          FROM f_split(@lns)))
  AND iID_DTChungTu in
    (SELECT iID_DTChungTu
     FROM NS_DT_ChungTu
     WHERE ((iLoaiDuToan = @LoaiDuToan)
            OR (@LoaiDuToan = 0))
       AND iNamLamViec=@NamLamViec
       AND (@NamNganSach IS NULL
            OR iNamNganSach in
              (SELECT *
               FROM f_split(@NamNganSach)))
       AND (@NguonNganSach IS NULL
            OR iID_MaNguonNganSach in
              (SELECT *
               FROM f_split(@NguonNganSach)))
       AND iLoai=0
       AND cast(dNgayChungTu AS date) <= cast(@NgayChungTu AS date))
GROUP BY sLNS,
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
         sXauNoiMa,
         sMoTa
HAVING sum(fTuChi)<>0
OR sum(fHienVat)<>0
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_dt_dot_ngayquyetdinh_full]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_dt_dot_ngayquyetdinh_full]
(    
      @NamLamViec int,
	  @NamNganSach nvarchar(20),
	  @NguonNganSach nvarchar(20),
	  @lns nvarchar(500),
	  @NgayQuyetDinh datetime,
	  @dvt int,
	  @LoaiDuToan int
)
RETURNS TABLE
AS RETURN

SELECT sLNS AS LNS,
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
       sXauNoiMa AS XauNoiMa,
       sMoTa AS MoTa,
       TuChi =sum(fTuChi)/@dvt,
       HienVat =sum(fHienVat)/@dvt
FROM NS_DT_ChungTuChiTiet
WHERE iNamLamViec=@NamLamViec
  AND (@NamNganSach IS NULL
       OR iNamNganSach in
         (SELECT *
          FROM f_split(@NamNganSach)))
  AND (@NguonNganSach IS NULL
       OR iID_MaNguonNganSach in
         (SELECT *
          FROM f_split(@NguonNganSach)))
  AND iPhanCap=0
  AND (@lns IS NULL
       OR sLNS in
         (SELECT *
          FROM f_split(@lns)))
  AND iID_DTChungTu in
    (SELECT iID_DTChungTu
     FROM NS_DT_ChungTu
     WHERE ((iLoaiDuToan = @LoaiDuToan)
            OR (@LoaiDuToan = 0))
       AND iNamLamViec=@NamLamViec
       AND (@NamNganSach IS NULL
            OR iNamNganSach in
              (SELECT *
               FROM f_split(@NamNganSach)))
       AND (@NguonNganSach IS NULL
            OR iID_MaNguonNganSach in
              (SELECT *
               FROM f_split(@NguonNganSach)))
       AND iLoai=0
       AND cast(dNgayQuyetDinh AS date) <= cast(@NgayQuyetDinh AS date))
GROUP BY sLNS,
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
         sXauNoiMa,
         sMoTa
HAVING sum(fTuChi)<>0
OR sum(fHienVat)<>0
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_dutoan_tonghop]    Script Date: 5/20/2022 9:02:55 AM ******/
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
		   TuChi =sum(fTuChi),
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
		   AND dNgayChungTu <= @NgayChungTu)
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi;
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_get_mlns_by_lns]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_get_mlns_by_lns]
(	
	@YearOfWork int,
	@LNS nvarchar(100)
)
RETURNS TABLE 
AS
RETURN 
(
	WITH LNSTreeChild AS (
		SELECT
			*
		FROM NS_MucLucNganSach
		WHERE
			sL = ''
			AND iNamLamViec = @YearOfWork
			AND sLNS in (SELECT * FROM dbo.splitstring(@LNS))
			and iTrangThai = 1
		UNION ALL
		SELECT
			mlnsChild.*
		FROM NS_MucLucNganSach mlnsChild
		INNER JOIN LNSTreeChild
			ON mlnsChild.iID_MLNS_Cha = LNSTreeChild.iID_MLNS
		WHERE
			mlnsChild.iNamLamViec = @YearOfWork
			AND mlnsChild.sLNS in (SELECT * FROM dbo.splitstring(@LNS))
	),
	LNSTreeParent AS (
		SELECT
			*
		FROM NS_MucLucNganSach
		WHERE
			sL = ''
			AND iNamLamViec = @YearOfWork
			AND sLNS in (SELECT * FROM dbo.splitstring(@LNS))
			AND iTrangThai = 1
		UNION ALL
		SELECT
			mlnsParent.*
		FROM NS_MucLucNganSach mlnsParent
		INNER JOIN LNSTreeParent
			ON mlnsParent.iID_MLNS = LNSTreeParent.iID_MLNS_Cha
		WHERE mlnsParent.iNamLamViec = @YearOfWork 
	)
	SELECT DISTINCT * FROM LNSTreeParent 
	UNION
	SELECT DISTINCT * FROM LNSTreeChild
)
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_get_parent_mlns_by_lns_mlns_id]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_get_parent_mlns_by_lns_mlns_id] (
@YearOfWork int,
@LNS nvarchar(100),
@MLNS_ID ntext) 
RETURNS TABLE 
AS RETURN
  (WITH LNSTreeParent AS
     (SELECT *
      FROM NS_MucLucNganSach
      WHERE iNamLamViec = @YearOfWork
        AND iID_MLNS in
          (SELECT *
           FROM dbo.splitstring(@MLNS_ID))
        AND sLNS in
          (SELECT *
           FROM dbo.splitstring(@LNS))
      UNION ALL SELECT mlnsParent.*
      FROM NS_MucLucNganSach mlnsParent
      INNER JOIN LNSTreeParent ON mlnsParent.iID_MLNS = LNSTreeParent.iID_MLNS_Cha
      WHERE mlnsParent.iNamLamViec = @YearOfWork ) SELECT DISTINCT *
   FROM LNSTreeParent --where L is null or L = '' Order by LNS, L, K, M, TM, TTM, NG, TNG
)
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_get_parent_mlns_by_lns_xau_noi_ma]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create FUNCTION [dbo].[f_get_parent_mlns_by_lns_xau_noi_ma]
(	
	@YearOfWork int,
	@LNS nvarchar(100),
	@XauNoiMa ntext
)
RETURNS TABLE 
AS
RETURN 
(
	with
LNSTreeParent AS (
		SELECT
			*
		FROM NS_MucLucNganSach
		WHERE
			 iNamLamViec = @YearOfWork
			AND sXauNoiMa in (SELECT * FROM dbo.splitstring(@XauNoiMa))
			AND sLNS in (SELECT * FROM dbo.splitstring(@LNS))
			AND iTrangThai = 1
		UNION ALL
		SELECT
			mlnsParent.*
		FROM NS_MucLucNganSach mlnsParent
		INNER JOIN LNSTreeParent
			ON mlnsParent.iID_MLNS = LNSTreeParent.iID_MLNS_Cha
		WHERE mlnsParent.iNamLamViec = @YearOfWork 
	)
	SELECT distinct * FROM LNSTreeParent where sLNS <> '1' and (sTTM is null or sTTM = '')-- Order by LNS desc, L desc, K desc, M desc, TM desc, TTM desc, NG desc, TNG desc
)
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_get_parent_mlns_by_lns_xau_noi_ma_report_dutoandaunam]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_get_parent_mlns_by_lns_xau_noi_ma_report_dutoandaunam]
(	
	@YearOfWork int,
	@XauNoiMa ntext
)
RETURNS TABLE 
AS
RETURN 
(
		SELECT

			*
		FROM NS_MucLucNganSach
		WHERE
			 iNamLamViec = @YearOfWork and bHangCha =1
			AND sXauNoiMa in (SELECT * FROM dbo.splitstring(@XauNoiMa))  
			AND iTrangThai = 1
)
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_lct_by_loai_cong_trinh]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_lct_by_loai_cong_trinh]
(	
	@lct nvarchar(max)
)
RETURNS TABLE 
AS
RETURN 
(
	WITH LNSTreeChild AS (
		SELECT
			*
		FROM VDT_DM_LoaiCongTrinh
		WHERE
			iID_LoaiCongTrinh in (select * from dbo.splitstring(@lct))
		UNION ALL
		SELECT
			mlnsChild.*
		FROM VDT_DM_LoaiCongTrinh mlnsChild
		INNER JOIN LNSTreeChild
			ON mlnsChild.iID_Parent = LNSTreeChild.iID_LoaiCongTrinh
		WHERE
			mlnsChild.iID_LoaiCongTrinh in (select * from dbo.splitstring(@lct))
	),

	LNSTreeParent AS (
		SELECT
			*
		FROM VDT_DM_LoaiCongTrinh
		WHERE
			iID_LoaiCongTrinh in (select * from dbo.splitstring(@lct))
		UNION ALL
		SELECT
			mlnsParent.*
		FROM VDT_DM_LoaiCongTrinh mlnsParent
		INNER JOIN LNSTreeParent
			ON mlnsParent.iID_LoaiCongTrinh = LNSTreeParent.iID_Parent
	)
	SELECT DISTINCT * FROM LNSTreeParent 
	UNION
	SELECT DISTINCT * FROM LNSTreeChild
)
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_loai_cong_trinh_get_list_childrent]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_loai_cong_trinh_get_list_childrent]
(	
	@lct nvarchar(max)
)
RETURNS TABLE 
AS
RETURN 
(
	WITH LoaiCongTrinh AS 
	 (
	  SELECT a.*
	  FROM VDT_DM_LoaiCongTrinh a
	  WHERE iID_LoaiCongTrinh in (select * from dbo.splitstring(@lct))

	  UNION ALL

	  SELECT a.*
	  FROM VDT_DM_LoaiCongTrinh a JOIN LoaiCongTrinh c ON a.iID_Parent = c.iID_LoaiCongTrinh

	  )
	select * from LoaiCongTrinh
)
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_loai_hinh_by_lns]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_loai_hinh_by_lns]
(	
	@YearOfWork int,
	@LNS nvarchar(100)
)
RETURNS TABLE 
AS
RETURN 
(
	WITH LNSTreeChild AS (
		SELECT
			*
		FROM TN_DanhMucLoaiHinh
		WHERE
			iNamLamViec = @YearOfWork
			AND LNS in (SELECT * FROM dbo.splitstring(@LNS))
			and iTrangThai = 1
		UNION ALL
		SELECT
			mlnsChild.*
		FROM TN_DanhMucLoaiHinh mlnsChild
		INNER JOIN LNSTreeChild
			ON mlnsChild.ID_MaLoaiHinh_Cha = LNSTreeChild.ID_MaLoaiHinh
		WHERE
			mlnsChild.iNamLamViec = @YearOfWork
			AND mlnsChild.LNS in (SELECT * FROM dbo.splitstring(@LNS))
	),
	LNSTreeParent AS (
		SELECT
			*
		FROM TN_DanhMucLoaiHinh
		WHERE
			iNamLamViec = @YearOfWork
			AND LNS in (SELECT * FROM dbo.splitstring(@LNS))
			AND iTrangThai = 1
		UNION ALL
		SELECT
			mlnsParent.*
		FROM TN_DanhMucLoaiHinh mlnsParent
		INNER JOIN LNSTreeParent
			ON mlnsParent.ID_MaLoaiHinh = LNSTreeParent.ID_MaLoaiHinh_Cha
		WHERE mlnsParent.iNamLamViec = @YearOfWork 
	)
	SELECT DISTINCT * FROM LNSTreeParent 
	UNION
	SELECT DISTINCT * FROM LNSTreeChild
)
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_mlns_by_lns]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[f_mlns_by_lns]
(	
	@YearOfWork int,
	@LNS nvarchar(100)
)
RETURNS TABLE 
AS
RETURN 
(
	WITH LNSTreeChild AS (
		SELECT
			*
		FROM NS_MucLucNganSach
		WHERE
			sL = ''
			AND iNamLamViec = @YearOfWork
			AND sLNS in (SELECT * FROM dbo.splitstring(@LNS))
			AND iTrangThai = 1
		UNION ALL
		SELECT
			mlnsChild.*
		FROM NS_MucLucNganSach mlnsChild
		INNER JOIN LNSTreeChild
			ON mlnsChild.iID_MLNS_Cha = LNSTreeChild.iID_MLNS
		WHERE
			mlnsChild.iNamLamViec = @YearOfWork
			AND mlnsChild.sLNS in (SELECT * FROM dbo.splitstring(@LNS))
	),
	LNSTreeParent AS (
		SELECT
			*
		FROM NS_MucLucNganSach
		WHERE
			sL = ''
			AND iNamLamViec = @YearOfWork
			AND sLNS in (SELECT * FROM dbo.splitstring(@LNS))
			AND iTrangThai = 1
		UNION ALL
		SELECT
			mlnsParent.*
		FROM NS_MucLucNganSach mlnsParent
		INNER JOIN LNSTreeParent
			ON mlnsParent.iID_MLNS = LNSTreeParent.iID_MLNS_Cha
		WHERE mlnsParent.iNamLamViec = @YearOfWork 
	)
	SELECT DISTINCT * FROM LNSTreeParent 
	UNION
	SELECT DISTINCT * FROM LNSTreeChild
)
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_ns]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*

author:		longsam
date:		11/08/2019
desc:		Lay du toan da phan bo

select * from f_pb(2019,null,null,getdate(),null,1)

*/

CREATE FUNCTION [dbo].[f_ns]
(    
      @NamLamViec int,
	  @NamNganSach nvarchar(20),
	  @NguonNganSach nvarchar(20),
	  @NgayChungTu datetime,
	  @id_donvi nvarchar(max),
	  @lns nvarchar(max)
)
RETURNS TABLE
AS RETURN

	SELECT sLNS,
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
		   sXauNoiMa,
		   iID_MaDonVi,
		   TuChi =sum(fTuChi),
		   HienVat =sum(fHienVat)
	FROM NS_DT_ChungTuChiTiet
	WHERE iNamLamViec=@NamLamViec
	  AND iDuLieuNhan = 0
	  AND (@NamNganSach IS NULL
		   OR iNamNganSach in
			 (SELECT *
			  FROM f_split(@NamNganSach)))
	  AND (@NguonNganSach IS NULL
		   OR iID_MaNguonNganSach in
			 (SELECT *
			  FROM f_split(@NguonNganSach)))
	  AND (@id_donvi IS NULL
		   OR iID_MaDonVi in
			 (SELECT *
			  FROM f_split(@id_donvi)))
	  AND (@lns IS NULL
		   OR sLNS in
			 (SELECT *
			  FROM f_split(@lns)))
	  AND iID_DTChungTu in
		(SELECT iID_DTChungTu
		 FROM NS_DT_ChungTu
		 WHERE iNamLamViec=@NamLamViec
		   AND (@NamNganSach IS NULL
				OR iNamNganSach in
				  (SELECT *
				   FROM f_split(@NamNganSach)))
		   AND (@NguonNganSach IS NULL
				OR iID_MaNguonNganSach in
				  (SELECT *
				   FROM f_split(@NguonNganSach)))
		   --AND iLoai=0
		   AND dNgayChungTu <= @NgayChungTu)
	GROUP BY sLNS,
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
			 sXauNoiMa,
			 iID_MaDonVi
;
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_ns_DTDV]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*

author:		longsam
date:		11/08/2019
desc:		Lay du toan da phan bo

select * from f_pb(2019,null,null,getdate(),null,1)

*/

CREATE FUNCTION [dbo].[f_ns_DTDV]
(    
      @NamLamViec int,
	  @NamNganSach int,
	  @NguonNganSach int,
	  @NgayChungTu datetime,
	  @id_donvi nvarchar(max),
	  @lns nvarchar(max),
	  @dvt int
)
RETURNS TABLE
AS RETURN

	SELECT sLNS,
		   sL,
		   sK,
		   sM,
		   sTM,
		   sTTM,
		   sNG,
		   sTNG,
		   sXauNoiMa,
		   iID_MaDonVi,
		   TuChi =sum(fTuChi) /@dvt,
		   HienVat =sum(fHienVat) /@dvt,
		   sMoTa
	FROM NS_DT_ChungTuChiTiet
	WHERE iNamLamViec=@NamLamViec
	  AND (@NamNganSach IS NULL
		   OR iNamNganSach in
			 (SELECT *
			  FROM f_split(@NamNganSach)))
	  AND (@NguonNganSach IS NULL
		   OR iID_MaNguonNganSach in
			 (SELECT *
			  FROM f_split(@NguonNganSach)))
	  AND (@id_donvi IS NULL
		   OR iID_MaDonVi in
			 (SELECT *
			  FROM f_split(@id_donvi)))
	  AND (@lns IS NULL
		   OR sLNS in
			 (SELECT *
			  FROM f_split(@lns)))
	  AND iID_DTChungTu in
		(SELECT iID_DTChungTu
		 FROM NS_DT_ChungTu
		 WHERE iNamLamViec=@NamLamViec
		   AND (@NamNganSach IS NULL
				OR iNamNganSach in
				  (SELECT *
				   FROM f_split(@NamNganSach)))
		   AND (@NguonNganSach IS NULL
				OR iID_MaNguonNganSach in
				  (SELECT *
				   FROM f_split(@NguonNganSach)))
		   AND cast(dNgayChungTu AS date) <= cast(@NgayChungTu AS date))
	GROUP BY sLNS,
			 sL,
			 sK,
			 sM,
			 sTM,
			 sTTM,
			 sNG,
			 sTNG,
			 sXauNoiMa,
			 iID_MaDonVi,
			 sMoTa
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_qt_done]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*

author:		longsam
date:		11/08/2019
desc:		Lay so da quyet toan

select * from [f_ns](2019,null,null,getdate(),null,null,1)

*/

CREATE FUNCTION [dbo].[f_qt_done]
(    
      @NamLamViec int,
	  @NamNganSach nvarchar(20),
	  @NguonNganSach nvarchar(20),
	  @ithangquyloai int,
	  @ithangquy nvarchar(20),
	  @id_donvi nvarchar(max),
	  @lns nvarchar(max)
)
RETURNS TABLE
AS RETURN

	SELECT sLNS,
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
		   sXauNoiMa,
		   iID_MaDonVi,
		   TuChi = Sum(fTuChi_PheDuyet),
		   cast(0 AS float) AS HienVat,
		   SoNguoi = Sum(fSoNguoi),
		   SoNgay = Sum(fSoNgay),
		   SoLuot = Sum(fSoLuot)
	FROM NS_QT_ChungTuChiTiet
	WHERE iNamLamViec=@NamLamViec
	  AND (@NamNganSach IS NULL
		   OR iNamNganSach in
			 (SELECT *
			  FROM f_split(@NamNganSach)))
	  AND (@NguonNganSach IS NULL
		   OR iID_MaNguonNganSach in
			 (SELECT *
			  FROM f_split(@NguonNganSach)))
	  AND (@id_donvi IS NULL
		   OR iID_MaDonVi in
			 (SELECT *
			  FROM f_split(@id_donvi)))
	  AND (@lns IS NULL
		   OR sLNS in
			 (SELECT *
			  FROM f_split(@lns)))
      AND iThangQuyLoai = @ithangquyloai
	  AND (iThangQuy in (select * from f_split(@ithangquy)))
	GROUP BY sLNS,
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
			 sXauNoiMa,
			 iID_MaDonVi
	HAVING sum(fTuChi_PheDuyet)<>0
;
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_qt_dot]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*

author:		longsam
date:		11/08/2019
desc:		Lay so da quyet toan

select * from [f_ns](2019,null,null,getdate(),null,null,1)

*/

CREATE FUNCTION [dbo].[f_qt_dot]
(    
      @NamLamViec int,
	  @NamNganSach nvarchar(20),
	  @NguonNganSach nvarchar(20),
	  @ithangquyloai int,
	  @ithangquy nvarchar(20),
	  @id_donvi nvarchar(max),
	  @lns nvarchar(max)
)
RETURNS TABLE
AS RETURN

	SELECT sLNS,
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
		   sXauNoiMa,
		   iID_MaDonVi,
		   TuChi =sum(fTuChi_PheDuyet),
		   cast(0 AS float) AS HienVat,
		   SoNguoi =sum(fSoNguoi),
		   SoNgay =sum(fSoNgay),
		   SoLuot =sum(fSoLuot)
	FROM NS_QT_ChungTuChiTiet
	WHERE iNamLamViec=@NamLamViec
	  AND (@NamNganSach IS NULL
		   OR iNamNganSach in
			 (SELECT *
			  FROM f_split(@NamNganSach)))
	  AND (@NguonNganSach IS NULL
		   OR iID_MaNguonNganSach in
			 (SELECT *
			  FROM f_split(@NguonNganSach)))
	  AND (@id_donvi IS NULL
		   OR iID_MaDonVi in
			 (SELECT *
			  FROM f_split(@id_donvi)))
	  AND (@lns IS NULL
		   OR sLNS in
			 (SELECT *
			  FROM f_split(@lns)))
	  AND iThangQuyLoai = @ithangquyloai
	  AND (@ithangquy IS NULL
		   OR iThangQuy in
			 (SELECT *
			  FROM f_split(@ithangquy)))
	GROUP BY sLNS,
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
			 sXauNoiMa,
			 iID_MaDonVi
	HAVING sum(fTuChi_PheDuyet)<>0
;
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_quyettoan]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[f_quyettoan]
(	
	@NamLamViec int,
	@NamNganSach nvarchar(20),
	@NguonNganSach nvarchar(20),
	@ithangquy nvarchar(20),
	@IdDonVi nvarchar(max),
	@lns nvarchar(max)
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT sLNS,
			iID_MLNS,
			iID_MaDonVi,
			fTuChi_PheDuyet 
	   FROM NS_QT_ChungTuChiTiet
	   WHERE iID_QTChungTu in
		   (SELECT iID_QTChungTu
			FROM NS_QT_ChungTu
			WHERE iNamLamViec = @NamLamViec
			  AND INamNganSach = @NamNganSach
			  AND iID_MaNguonNganSach = @NguonNganSach
			  AND iThangQuy in (select * from f_split(@ithangquy)))
		 AND (@IdDonVi IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi)))
		 AND (@lns IS NULL OR sLNS like @lns + '%')
)
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_quyettoan_tonghop]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_quyettoan_tonghop]
(    
      @NamLamViec int,
	  @NamNganSach nvarchar(20),
	  @NguonNganSach nvarchar(20),
	  @ithangquy nvarchar(20),
	  @IdDonVi nvarchar(max),
	  @lns nvarchar(max)
)
RETURNS TABLE
AS RETURN

	SELECT iID_MLNS,
		   iID_MLNS_Cha,
		   iID_MaDonVi,
		   TuChi = Sum(fTuChi_PheDuyet),
		   cast(0 AS float) AS HienVat,
		   SoNguoi = Sum(fSoNguoi),
		   SoNgay = Sum(fSoNgay),
		   SoLuot = Sum(fSoLuot)
	FROM NS_QT_ChungTuChiTiet
	WHERE iNamLamViec=@NamLamViec
	  AND (@NamNganSach IS NULL OR iNamNganSach = @NamNganSach)
	  AND (@NguonNganSach IS NULL OR iID_MaNguonNganSach = @NguonNganSach)
	  AND (@IdDonVi = ''
		   OR iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi)))
	  AND (@lns IS NULL
		   OR sLNS in (SELECT * FROM f_split(@lns)))
	  AND iID_QTChungTu IN 
		(
			SELECT iID_QTChungTu 
			FROM NS_QT_ChungTu
			where iThangQuy in (select * from f_split(@ithangquy))
		)
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	HAVING sum(fTuChi_PheDuyet) <> 0;
;
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_recursive_donvi]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[f_recursive_donvi]
(	
	-- Add the parameters for the function here
	@MaDonVi nvarchar(max)
)
RETURNS TABLE 
AS
RETURN 
(
	-- Add the SELECT statement with parameter references here
	with cte as (
      select *
      from VDT_DM_DonViThucHienDuAn
      where iID_MaDonVi = @MaDonVi
      union all
      select t.*
      from cte join
           VDT_DM_DonViThucHienDuAn t
           on cte.iID_DonVi = t.iID_DonViCha 
     )
	 select iID_MaDonVi from cte 
)
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_rpt_get_mlns]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create FUNCTION [dbo].[f_rpt_get_mlns]
(	
	@YearOfWork int
)
RETURNS TABLE 
AS
RETURN 
(
	with LNSTreeParent
		As
		(
			SELECT iID ,iID_MLNS as MLNS_Id,iID_MLNS_Cha as MLNS_Id_Parent,sXauNoiMa as XauNoiMa
			FROM NS_MucLucNganSach
			WHERE  iNamLamViec = @YearOfWork
				--AND LNS in (SELECT * FROM dbo.splitstring(@LNS))
				and iTrangThai = 1
			union all
			select mlnsParent.iID,mlnsParent.iID_MLNS as MLNS_Id,mlnsParent.iID_MLNS_Cha as MLNS_Id_Parent,mlnsParent.sXauNoiMa as XauNoiMa
			from NS_MucLucNganSach mlnsParent
			inner join LNSTreeParent
			on mlnsParent.iID_MLNS = LNSTreeParent.MLNS_Id_Parent and mlnsParent.iNamLamViec = @YearOfWork and mlnsParent.iTrangThai =1 and bHangCha =1
			 
		)
	select distinct  MLNS_Id from LNSTreeParent 
)
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_skt_dutoan]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_skt_dutoan] (
@NamLamViec int,
@id_donvi nvarchar(MAX), 
@LoaiChungTu nvarchar(MAX)) 
RETURNS 
TABLE 
AS 
RETURN
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
 TuChi =sum(fTuChi)
      FROM NS_DTDauNam_ChungTuChiTiet
      WHERE iNamLamViec=@NamLamViec
        AND iLoaiChungTu = @LoaiChungTu
        AND iLoai=3
        AND (@id_donvi IS NULL
             OR iID_MaDonVi in
               (SELECT *
                FROM f_split(@id_donvi)))
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
;
GO
/****** Object:  View [dbo].[V_PhanBoDuAn]    Script Date: 5/20/2022 9:02:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_PhanBoDuAn]
AS
SELECT tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.iID_LoaiNguonVonID, tbl.iID_DonViQuanLyID, 
tbl.iID_NhomQuanLyID, tbl.iID_LoaiNganSachID, tbl.iID_KhoanNganSachID, tbl.sLoaiDieuChinh, tbl.iID_ParentId, 
tbl.bActive, tbl.bIsGoc, tbl.bLaThayThe, tbl.bIsCanBoDuyet, tbl.bIsDuyet, tbl.iID_NguonVonID, tbl.iLoai,tbl.iID_MaDonViQuanLy,
dt.Id, dt.iID_DuAnID, dt.iID_MucID, dt.iID_TieuMucID, dt.iID_TietMucID, dt.iID_NganhID, dt.sTrangThaiDuAnDangKy, 
dt.fGiaTrDeNghi, dt.fGiaTrPhanBo, dt.fGiaTriThuHoi, dt.iID_DonViTienTeID, dt.iID_TienTeID, dt.fTiGiaDonVi, dt.fTiGia, dt.sGhiChu,
da.sTenDuAn, da.bIsKetThuc
FROM VDT_KHV_PhanBoVon as tbl
INNER JOIN VDT_KHV_PhanBoVon_ChiTiet as dt on tbl.Id = dt.iID_PhanBoVonID
INNER JOIN VDT_DA_DuAn as da on dt.iID_DuAnID = da.iID_DuAnID
;
GO
