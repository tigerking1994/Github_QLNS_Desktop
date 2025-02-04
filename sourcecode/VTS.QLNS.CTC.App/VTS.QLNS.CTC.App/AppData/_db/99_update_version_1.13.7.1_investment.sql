/****** Object:  StoredProcedure [dbo].[sp_vdt_ke_hoach_5_nam_creation]    Script Date: 21/12/2023 5:33:03 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_ke_hoach_5_nam_creation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_ke_hoach_5_nam_creation]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_ke_hoach_5_nam_creation]    Script Date: 21/12/2023 5:33:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_ke_hoach_5_nam_creation]
	@iID_KeHoach5Nam nvarchar(100),
	@iID_KeHoach5F nvarchar(100)
AS
BEGIN
	INSERT INTO VDT_KHV_KeHoach5Nam_ChiTiet(
		iID_KeHoach5Nam_ChiTietID,
		iID_KeHoach5NamID,
		iID_DuAnID,
		fGiaTriKeHoach,
		iID_NguonVonID,
		sTrangThai,
		sGhiChu,
		fGiaTriBoTri,
		iID_LoaiCongTrinhID,
		fVonDaGiao,
		fVonBoTriTuNamDenNam,
		fHanMucDauTu,
		iID_DonViQuanLyID,
		sTen,
		bActive,
		iID_MaDonVi,
		iID_ParentID,
		sStt
	)

	SELECT
			NEWID(),
			@iID_KeHoach5Nam,
			iID_DuAnID,
			fGiaTriKeHoach,
			iID_NguonVonID,
			sTrangThai,
			sGhiChu,
			fGiaTriBoTri,
			iID_LoaiCongTrinhID,
			fVonDaGiao,
			fVonBoTriTuNamDenNam,
			fHanMucDauTu,
			iID_DonViQuanLyID,
			sTen,
			1,
			iID_MaDonVi,
			iID_KeHoach5Nam_ChiTietID,
			sStt
		from 
			VDT_KHV_KeHoach5Nam_ChiTiet 
		where
			iID_KeHoach5NamID = @iID_KeHoach5F

	update VDT_KHV_KeHoach5Nam_ChiTiet set bActive = 0 where iID_KeHoach5NamID = @iID_KeHoach5F
END
;
;

select * from VDT_KHV_KeHoach5Nam_ChiTiet
GO
