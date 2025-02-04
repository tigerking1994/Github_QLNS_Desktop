/****** Object:  StoredProcedure [dbo].[sp_tl_update_dmphucap_canbo]    Script Date: 14/12/2022 5:24:44 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_update_dmphucap_canbo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_update_dmphucap_canbo]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_update_dmphucap_canbo]    Script Date: 14/12/2022 5:24:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_tl_update_dmphucap_canbo]
@nam int,
@thang int,
@bIsDelete bit,
@phucapIds t_tbl_uniqueidentifier READONLY
AS
BEGIN
	SELECT Ma_PhuCap, Gia_Tri, Ten_NganHang INTO #tmpPC
	FROM @phucapIds as tmp
	INNER JOIN TL_DM_PhuCap as pc on tmp.Id = pc.Id
	
	SELECT cb.Ma_CanBo, pc.Gia_Tri, pc.Ma_PhuCap INTO #tmpCanBoPC
	FROM TL_DM_CanBo as cb, #tmpPC as pc
	WHERE cb.Nam > @nam OR (cb.Thang >= @thang AND cb.Nam = @nam)

	IF(@bIsDelete = 0)
	BEGIN
		CREATE TABLE #tmpUpdate(MaCB nvarchar(100), MaPC nvarchar(500))

		UPDATE tbl
		SET GIA_TRI = pc.Gia_Tri
		OUTPUT inserted.MA_CBO, inserted.MA_PHUCAP INTO #tmpUpdate(MaCB, MaPC)
		FROM TL_CanBo_PhuCap as tbl
		INNER JOIN #tmpCanBoPC as pc ON tbl.MA_PHUCAP = pc.Ma_PhuCap AND tbl.MA_CBO = pc.Ma_CanBo

		INSERT INTO TL_CanBo_PhuCap(bSaoChep, CHON, CONG_THUC, GIA_TRI, HE_SO, HuongPC_SN, MA_CBO, MA_KMCP, MA_PHUCAP, PHANTRAM_CT)
		SELECT pc.bSaoChep, pc.CHON, pc.CONG_THUC, tbl.Gia_Tri, HE_SO, HuongPC_SN, tbl.Ma_CanBo, MA_KMCP, tbl.Ma_PhuCap, PHANTRAM_CT
		FROM #tmpCanBoPC as tbl
		INNER JOIN TL_DM_PhuCap as pc on tbl.Ma_PhuCap = pc.Ma_PhuCap
		LEFT JOIN #tmpUpdate as dt on tbl.Ma_CanBo = dt.MaCB AND tbl.Ma_PhuCap = dt.MaPC
		WHERE dt.MaCB IS NULL OR dt.MaPC IS NULL

		IF (EXISTS (SELECT * FROM #tmpPC WHERE Ma_PhuCap = 'TENNGANHANG'))
		BEGIN
			UPDATE TL_DM_CanBo SET Ten_KhoBac = (SELECT Ten_NganHang FROM TL_DM_PhuCap WHERE Ma_PhuCap = 'TENNGANHANG')
			WHERE  Nam > @nam OR (Thang >= @thang AND Nam = @nam)
		END
		DROP TABLE #tmpUpdate
	END
	ELSE
	BEGIN
		DELETE tbl
		FROM TL_CanBo_PhuCap as tbl
		INNER JOIN #tmpCanBoPC as pc ON tbl.MA_PHUCAP = pc.Ma_PhuCap AND tbl.MA_CBO = pc.Ma_CanBo
	END

	DROP TABLE #tmpPC
	DROP TABLE #tmpCanBoPC
END
GO
