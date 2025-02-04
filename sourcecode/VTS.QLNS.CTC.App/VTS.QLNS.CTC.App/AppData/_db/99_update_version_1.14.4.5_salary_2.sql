/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangluongthang_phaitru_nq104]    Script Date: 5/14/2024 5:26:22 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangluongthang_phaitru_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangluongthang_phaitru_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangluongthang_phaitru_nq104]    Script Date: 5/14/2024 5:26:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 21/12/2021
-- Description:	Lấy dữ liệu phụ cấp khác
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangluongthang_phaitru_nq104]
	@MaCanBo VARCHAR(50), @maPhuCap varchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		ROW_NUMBER() OVER(ORDER BY phuCap.Ma_PhuCap ASC) STT,
		phuCap.Ma_PhuCap MaPhuCap,
		phuCap.Ten_PhuCap TenPhuCap,
		bangLuongBridge.gia_tri GiaTri
	FROM TL_BangLuong_Thang_NQ104 bangLuong
	INNER JOIN TL_BangLuong_Thang_Bridge_NQ104 bangLuongBridge on bangLuong.Ma_CBo=bangLuongBridge.ma_can_bo
	INNER JOIN TL_DM_PhuCap_NQ104 phuCap ON phuCap.Ma_PhuCap = bangLuongBridge.ma_phu_cap
	WHERE
		bangLuong.Ma_CBo = @MaCanBo
		AND bangLuongBridge.gia_tri > 0
		And bangLuong.Ma_CachTL = 'CACH0'
		And phuCap.Ma_PhuCap IN (SELECT * FROM f_split(@maPhuCap))
END
;
;
;

GO
