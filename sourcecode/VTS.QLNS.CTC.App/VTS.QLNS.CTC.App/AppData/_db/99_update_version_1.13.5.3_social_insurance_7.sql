/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkcb_thongtriloai1_bh]    Script Date: 11/20/2023 2:18:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_qkcb_thongtriloai1_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_qkcb_thongtriloai1_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qbhxh_thongtriloai1_bh]    Script Date: 11/20/2023 2:18:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_qbhxh_thongtriloai1_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_qbhxh_thongtriloai1_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qbhxh_thongtriloai1_bh]    Script Date: 11/20/2023 2:18:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chung tu theo don vi và theo lns
Create PROCEDURE [dbo].[sp_rpt_qtc_qbhxh_thongtriloai1_bh]
	 @NamLamViec int,
	 @iQuy nvarchar(100),
	 @IdDonVi nvarchar(max),
	 @UserName nvarchar(100),
	 @iLoaiTongHop int,
	 @Dvt int
AS
BEGIN 
	SET NOCOUNT ON;

	IF @iLoaiTongHop=1
	BEGIN
	SELECT  ct.iID_MaDonVi as SMaDonVi,
		ISNULL(ct.fTongTien_PheDuyet,0)/ @Dvt FTongTienXacNhanQuyetToanQuyNay,
		ct.iID_DonVi as IID_DonVi,
		dv.sTenDonVi as STenDonVi
		FROM BH_QTC_Quy_CheDoBHXH ct
		LEFT JOIN DonVi dv ON ct.iID_DonVi=dv.iID_DonVi
			WHERE ct.iNamChungTu=@NamLamViec
				    AND ct.iQuyChungTu=@iQuy
					--AND ct.bIsKhoa=1
					--AND ct.iLoaiTongHop=@iLoaiTongHop
					AND ct.iID_MaDonVi IN (select * from f_split(@IdDonVi))
	END
	ELSE
	BEGIN
	SELECT  ct.iID_MaDonVi as SMaDonVi,
		ISNULL(ct.fTongTien_PheDuyet,0)/ @Dvt FTongTienXacNhanQuyetToanQuyNay,
		ct.iID_DonVi as IID_DonVi,
		dv.sTenDonVi as STenDonVi
		FROM BH_QTC_Quy_CheDoBHXH ct
		LEFT JOIN DonVi dv ON ct.iID_DonVi=dv.iID_DonVi
			WHERE ct.iNamChungTu=@NamLamViec
				    AND ct.iQuyChungTu=@iQuy
					--AND ct.bIsKhoa=1
					--AND ct.iLoaiTongHop=@iLoaiTongHop
					AND ct.iID_MaDonVi IN (select * from f_split(@IdDonVi))
	END

END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkcb_thongtriloai1_bh]    Script Date: 11/20/2023 2:18:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chung tu theo don vi và theo lns
Create PROCEDURE [dbo].[sp_rpt_qtc_qkcb_thongtriloai1_bh]
	 @NamLamViec int,
	 @iQuy nvarchar(100),
	 @IdDonVi nvarchar(max),
	 @UserName nvarchar(100),
	 @iLoaiTongHop int,
	 @Dvt int
AS
BEGIN 
	SET NOCOUNT ON;

	IF @iLoaiTongHop=1
	BEGIN
	SELECT  ct.iID_MaDonVi as SMaDonVi,
		ISNULL(ct.fTongTienXacNhanQuyetToanQuyNay,0)/ @Dvt FTongTienXacNhanQuyetToanQuyNay,
		ct.iID_DonVi as IID_DonVi,
		dv.sTenDonVi as STenDonVi
		FROM BH_QTC_Quy_KCB ct
		LEFT JOIN DonVi dv ON ct.iID_DonVi=dv.iID_DonVi
			WHERE ct.iNamChungTu=@NamLamViec
				    AND ct.iQuyChungTu=@iQuy
					--AND ct.bIsKhoa=1
					--AND ct.iLoaiTongHop=@iLoaiTongHop
					AND ct.iID_MaDonVi IN (select * from f_split(@IdDonVi))
	END
	ELSE
	BEGIN
	SELECT  ct.iID_MaDonVi as SMaDonVi,
		ISNULL(ct.fTongTienXacNhanQuyetToanQuyNay,0)/ @Dvt FTongTienXacNhanQuyetToanQuyNay,
		ct.iID_DonVi as IID_DonVi,
		dv.sTenDonVi as STenDonVi
		FROM BH_QTC_Quy_KCB ct
		LEFT JOIN DonVi dv ON ct.iID_DonVi=dv.iID_DonVi
			WHERE ct.iNamChungTu=@NamLamViec
				    AND ct.iQuyChungTu=@iQuy
					--AND ct.bIsKhoa=1
					--AND ct.iLoaiTongHop=@iLoaiTongHop
					AND ct.iID_MaDonVi IN (select * from f_split(@IdDonVi))
	END

END
;
;


GO
