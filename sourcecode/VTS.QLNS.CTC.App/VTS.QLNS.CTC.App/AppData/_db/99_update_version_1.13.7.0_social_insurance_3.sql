/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqBHXH_create_datafor_quaterbefore]    Script Date: 12/21/2023 10:24:26 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqBHXH_create_datafor_quaterbefore]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqBHXH_create_datafor_quaterbefore]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh]    Script Date: 12/21/2023 10:24:26 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh]    Script Date: 12/21/2023 10:24:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh] 
	-- Add the parameters for the stored procedure here
	@sXauNoiMa nvarchar(max),
	@sQuarter nvarchar(50)
AS
BEGIN
	SELECT 
		@sXauNoiMa as SXauNoiMa,
		sMaHieuCanBo as SMaHieuCanBo,
		sTenCbo as STenCanBo,
		sMaCBo As SMaCanBo,
		sMaCB as SMaCapBac,
		capbac.Note aS STenCapBac,
		bangluong.sMaDonVi as ID_MaPhanHo,
		dv.Ten_DonVi as STenPhanHo,
		canbo.fSoNgayHuongBHXH as ISoNgayHuong,
		canbo.sSoQuyetDinh as SSoQuyetDinh,
		canbo.dNgayQuyetDinh as DNgayQuyetDinh,
		bangluong.nGiaTri as FSoTien,
		canbo.dTuNgay AS DTuNgay,
		canbo.dDenNgay AS DDenNgay

	FROM TL_BangLuong_ThangBHXH bangluong
	LEFT JOIN TL_CanBo_CheDoBHXH canbo ON bangluong.sMaCBo = canbo.sMaCanBo
	LEFT JOIN TL_DM_CapBac capbac ON capbac.Ma_Cb  = bangluong.sMaCB
	LEFT JOIN TL_DM_DonVi dv On dv.Ma_DonVi = bangluong.sMaDonVi
	WHERE 
		bangluong.iThang IN (SELECT * FROM splitstring(@sQuarter))
		AND bangluong.sMaCheDo IN (SELECT sMaCheDo FROM TL_DM_CheDoBHXH WHERE bDisplay = 1 AND sXauNoiMaMlnsBHXH = @sXauNoiMa )
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqBHXH_create_datafor_quaterbefore]    Script Date: 12/21/2023 10:24:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtcqBHXH_create_datafor_quaterbefore]
@IdChungTu nvarchar(MAX),
@Username nvarchar(500),
@NamLamViec int,
@Quy int,
@MaDonVi nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

	INSERT INTO BH_QTC_Quy_CheDoBHXH_ChiTiet
	( 
		ID_QTC_Quy_CheDoBHXH_ChiTiet,
		iID_QTC_Quy_CheDoBHXH,
		iID_MucLucNganSach,
		iNamLamViec,
		sLoaiTroCap,
		dNgayTao,
		sNguoiTao,
		fTienLuyKeCuoiQuyNay	
	)
	SELECT 
		NEWID(),
		@IdChungTu,
		qtcn_ct.iID_MucLucNganSach,
		@NamLamViec,
		qtcn_ct.sLoaiTroCap,
		GETDATE(),
		@Username,
		SUM(qtcn_ct.fTongTien_DeNghi)
	FROM BH_QTC_Quy_CheDoBHXH_ChiTiet qtcn_ct
		inner join BH_QTC_Quy_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Quy_CheDoBHXH = qtcn.ID_QTC_Quy_CheDoBHXH
	WHERE qtcn.iQuyChungTu < @Quy
			AND qtcn.iID_MaDonVi = @MaDonVi
			AND qtcn.iNamChungTu = @NamLamViec
			--AND qtcn.bIsKhoa=1
		GROUP BY qtcn_ct.iID_MucLucNganSach, qtcn_ct.sLoaiTroCap
END
;
;
;


GO
