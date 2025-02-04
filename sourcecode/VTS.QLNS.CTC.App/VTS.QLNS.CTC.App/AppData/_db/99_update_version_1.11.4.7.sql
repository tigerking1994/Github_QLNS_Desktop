/****** Object:  UserDefinedFunction [dbo].[f_luong_ntn]    Script Date: 28/07/2022 4:01:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_luong_ntn]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_luong_ntn]
GO
/****** Object:  UserDefinedFunction [dbo].[f_luong_ntn]    Script Date: 28/07/2022 4:01:41 PM ******/
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
	DECLARE @monthDiff int SET @monthDiff = 0
	DECLARE @monthDiff2 int SET @monthDiff2 = 0

	IF (@NgayNN IS NOT NULL)
	BEGIN
		IF (@NgayXN IS NULL AND @NgayTN IS NULL)
		BEGIN
			SET @monthDiff = (@Nam - YEAR(@NgayNN)) * 12 + @Thang - MONTH(@NgayNN) + 1 + @ThangTNN
			IF(@monthDiff % 12 >= 1)
				BEGIN
					SET @NamThamNien = @monthDiff / 12
				END
			ELSE
				BEGIN
					SET @NamThamNien = @monthDiff / 12 - 1
				END
		END

		ELSE
		BEGIN
			--IF (@NgayTN IS NULL)
			--BEGIN
			--	SET @monthDiff = (YEAR(@NgayXN) - YEAR(@NgayNN)) * 12 + @Thang - MONTH(@NgayNN) + 1 + @ThangTNN
			--	IF(@monthDiff % 12 >= 1)
			--		BEGIN
			--			SET @NamThamNien = @monthDiff / 12
			--		END
			--	ELSE
			--		BEGIN
			--			SET @NamThamNien = @monthDiff / 12 - 1
			--		END
			--END

			--ELSE
			--BEGIN
			--	DECLARE @Lan1 int SET @Lan1 = 12 * (YEAR(@NgayXN) - YEAR(@NgayNN)) + MONTH(@NgayXN) - MONTH(@NgayNN) + 1
				
			--	IF (@Lan1 < 6)
			--	BEGIN
			--		set @monthDiff2 = 12 * (@Nam - YEAR(@NgayTN)) + @Thang - MONTH(@NgayTN) + @ThangTNN + 1
			--		IF(@monthDiff2 % 12 >= 1)
			--			BEGIN
			--				SET @NamThamNien = @monthDiff2 / 12
			--			END
			--		ELSE
			--			BEGIN
			--				SET @NamThamNien = @monthDiff2 / 12 - 1
			--			END
			--	END

			--	ELSE IF (@Lan1 >= 6 AND @Lan1 <= 12)
			--	BEGIN
			--		set @monthDiff2 = 12 * (@Nam - YEAR(@NgayNN)) + @Thang - MONTH(@NgayNN) + @ThangTNN + 1
			--		IF(@monthDiff2 % 12 >= 1)
			--			BEGIN
			--				SET @NamThamNien = @monthDiff2 / 12
			--			END
			--		ELSE
			--			BEGIN
			--				SET @NamThamNien = @monthDiff2 / 12 - 1
			--			END
			--	END

			--	ELSE 
			--	BEGIN
			--		set @monthDiff2 = 12 * (@Nam - YEAR(@NgayTN)) + @Thang - MONTH(@NgayTN) + 1 + @ThangTNN + @Lan1
			--		IF(@monthDiff2 % 12 >= 1)
			--			BEGIN
			--				SET @NamThamNien = @monthDiff2 / 12
			--			END
			--		ELSE
			--			BEGIN
			--				SET @NamThamNien = @monthDiff2 / 12 - 1
			--			END
			--	END
			--END
			IF (@NgayTN IS NULL)
			BEGIN
				SET @monthDiff = (YEAR(@NgayXN) - YEAR(@NgayNN)) * 12 + @Thang - MONTH(@NgayNN) + @ThangTNN
				IF(@monthDiff % 12 >= 1)
					BEGIN
						SET @NamThamNien = @monthDiff / 12
					END
				ELSE
					BEGIN
						SET @NamThamNien = @monthDiff / 12 - 1
					END
			END

			ELSE
			BEGIN
				DECLARE @Lan1 int SET @Lan1 = 12 * (YEAR(@NgayXN) - YEAR(@NgayNN)) + MONTH(@NgayXN) - MONTH(@NgayNN) 
				DECLARE @NgoaiQD int SET @NgoaiQD = 12 * (YEAR(@NgayXN) - YEAR(@NgayNN)) + MONTH(@NgayXN) - MONTH(@NgayNN) 
				
				IF (@NgoaiQD <= 12)
					BEGIN
						set @monthDiff2 = 12 * (@Nam - YEAR(@NgayNN)) + @Thang - MONTH(@NgayNN) + @ThangTNN + 1
						IF(@monthDiff2 % 12 >= 1)
							BEGIN
								SET @NamThamNien = @monthDiff2 / 12
							END
						ELSE
							BEGIN
								SET @NamThamNien = @monthDiff2 / 12 - 1
							END
					END
				ELSE
					IF (@Lan1 < 6)
					BEGIN
						set @monthDiff2 = 12 * (@Nam - YEAR(@NgayTN)) + @Thang - MONTH(@NgayTN) + @ThangTNN + 1
						IF(@monthDiff2 % 12 >= 1)
							BEGIN
								SET @NamThamNien = @monthDiff2 / 12
							END
						ELSE
							BEGIN
								SET @NamThamNien = @monthDiff2 / 12 - 1
							END
					END

					ELSE 
					BEGIN
						set @monthDiff2 = 12 * (@Nam - YEAR(@NgayTN)) + @Thang - MONTH(@NgayTN) + 1 + @ThangTNN + @Lan1
						IF(@monthDiff2 % 12 >= 1)
							BEGIN
								SET @NamThamNien = @monthDiff2 / 12
							END
						ELSE
							BEGIN
								SET @NamThamNien = @monthDiff2 / 12 - 1
							END
					END
			END
		END
	END
	RETURN @NamThamNien
END
;
GO
