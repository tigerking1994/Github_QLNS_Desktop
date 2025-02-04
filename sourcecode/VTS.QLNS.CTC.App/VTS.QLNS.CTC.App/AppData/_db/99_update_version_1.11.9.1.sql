/****** Object:  StoredProcedure [dbo].[sp_vdt_khlcnt_get_hangmuc_by_khlcntid]    Script Date: 09/09/2022 5:25:04 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_khlcnt_get_hangmuc_by_khlcntid]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_khlcnt_get_hangmuc_by_khlcntid]
GO
/****** Object:  UserDefinedFunction [dbo].[f_luong_ntn]    Script Date: 09/09/2022 5:25:04 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_luong_ntn]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_luong_ntn]
GO
/****** Object:  UserDefinedFunction [dbo].[f_luong_ntn]    Script Date: 09/09/2022 5:25:04 PM ******/
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

	IF (@ThangTNN IS NULL) SET @ThangTNN = 0

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
/****** Object:  StoredProcedure [dbo].[sp_vdt_khlcnt_get_hangmuc_by_khlcntid]    Script Date: 09/09/2022 5:25:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_khlcnt_get_hangmuc_by_khlcntid]
@iIdKhlcnt uniqueidentifier
AS
BEGIN
	DECLARE @iIdDuToanId uniqueidentifier
	DECLARE @iIdQDDauTuId uniqueidentifier

	SELECT @iIdDuToanId = iID_DuToanID, @iIdQDDauTuId = iID_QDDauTuID FROM VDT_QDDT_KHLCNhaThau WHERE Id = @iIdKhlcnt

	IF(@iIdDuToanId IS NOT NULL)
	BEGIN
		SELECT distinct dt.iID_HangMucID as IIdHangMucId, CAST(1 as bit) as IsChecked, tbl.iID_GoiThauID as IIdGoiThauId, null as IIdNguonVonId, null as IIdChiPhiGocId, dt.iID_ChiPhiID as IIdChiPhiId, hm.iID_ParentID as IIdParentId,
				CAST(0 as bit) as IsHangCha, hm.sTenHangMuc as SNoiDung, hm.maOrder as SMaOrder, CAST(0 as float) as FGiaTriDuocDuyet, ISNULL(dt.fTienGoiThau, 0) as FGiaTriGoiThau
		FROM VDT_DA_GoiThau as tbl
		INNER JOIN VDT_DA_GoiThau_HangMuc as dt on tbl.iID_GoiThauID = dt.iID_GoiThauID
		INNER JOIN VDT_DA_DuToan_DM_HangMuc as hm on dt.iID_HangMucID = hm.Id
		WHERE tbl.iId_KHLCNhaThau = @iIdKhlcnt
	END
	ELSE
	BEGIN
		SELECT distinct dt.iID_HangMucID as IIdHangMucId, CAST(1 as bit) as IsChecked, tbl.iID_GoiThauID as IIdGoiThauId, null as IIdNguonVonId, null as IIdChiPhiGocId, dt.iID_ChiPhiID as IIdChiPhiId, hm.iID_ParentID as IIdParentId,
				CAST(0 as bit) as IsHangCha, hm.sTenHangMuc as SNoiDung, hm.maOrder as SMaOrder, CAST(0 as float) as FGiaTriDuocDuyet, ISNULL(dt.fTienGoiThau, 0) as FGiaTriGoiThau
		FROM VDT_DA_GoiThau as tbl
		INNER JOIN VDT_DA_GoiThau_HangMuc as dt on tbl.iID_GoiThauID = dt.iID_GoiThauID
		INNER JOIN VDT_DA_QDDauTu_DM_HangMuc as hm on dt.iID_HangMucID = hm.id
		WHERE tbl.iId_KHLCNhaThau = @iIdKhlcnt
	END
END
;
;
GO


update TL_DM_PhuCap set Gia_Tri = 1 where Ma_PhuCap = 'TILE_HUONG';