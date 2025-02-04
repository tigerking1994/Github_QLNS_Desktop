INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_QuanSo_TangGiam_TongHop_DonVi', NULL, N'rptNS_QuanSo_TangGiam_TongHop_DonVi', NULL, NULL, NULL, NULL, NULL, N'QUAN_SO', NULL, N'Tổng hợp quân số tăng giảm theo đơn vị', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'Tổng hợp quân số tăng giảm theo đơn vị', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_tonghop_thang]    Script Date: 08/08/2023 5:39:27 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_dutoan_tonghop_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_dutoan_tonghop_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_copy_doi_tuong_mlns]    Script Date: 08/08/2023 5:39:27 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_copy_doi_tuong_mlns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_copy_doi_tuong_mlns]
GO
/****** Object:  StoredProcedure [dbo].[sp_copy_doi_tuong_mlns]    Script Date: 08/08/2023 5:39:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[sp_copy_doi_tuong_mlns]
	@DestYearOfWork int,
	@SourceYearOfWork int
AS
BEGIN

	SET NOCOUNT ON;

	UPDATE a
	SET a.sMaCB = b.sMaCB
	FROM ns_muclucngansach a
	INNER JOIN ns_muclucngansach b
	  ON a.sXauNoiMa = b.sXauNoiMa
	WHERE a.iNamLamViec = @DestYearOfWork 
	AND b.iNamLamViec = @SourceYearOfWork
	AND ISNULL(a.sMaCB, '') = ''

END
;




GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_tonghop_thang]    Script Date: 08/08/2023 5:39:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_rpt_dutoan_tonghop_thang]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@EstimateAgencyId nvarchar(max),
	@AgencyId nvarchar(max),
	@LNS nvarchar(max),
	@QuarterMonth nvarchar(100),
	@QuarterMonthBefore nvarchar(100),
	@VoucherDate datetime,
	@Dvt int
AS
BEGIN
	SELECT dv.iID_MaDonVi,
		   TenDonVi,
		   ChiTieu = SUM(ChiTieu) / @Dvt,
		   TuChi = SUM(TuChi) / @Dvt,
		   Thang1 = SUM(Thang1) / @Dvt,
		   Thang2 = SUM(Thang2) / @Dvt,
		   Thang3 = SUM(Thang3) / @Dvt,
		   Thang4 = SUM(Thang4) / @Dvt,
		   Thang5 = SUM(Thang5) / @Dvt,
		   Thang6 = SUM(Thang6) / @Dvt,
		   Thang7 = SUM(Thang7) / @Dvt,
		   Thang8 = SUM(Thang8) / @Dvt,
		   Thang9 = SUM(Thang9) / @Dvt,
		   Thang10 = SUM(Thang10) / @Dvt,
		   Thang11 = SUM(Thang11) / @Dvt,
		   Thang12 = SUM(Thang12) / @Dvt
	FROM
	  (--chi tieu theo don vi
	  SELECT 
			iID_MaDonVi,
			ChiTieu = fTuChi,
			TuChi = 0,
			Thang1 = 0,
			Thang2 = 0,
			Thang3 = 0,
			Thang4 = 0,
			Thang5 = 0,
			Thang6 = 0,
			Thang7 = 0,
			Thang8 = 0, 
			Thang9 = 0,
			Thang10 = 0,
			Thang11 = 0,
			Thang12 = 0
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iNamNganSach = @YearOfBudget
		 AND iID_MaNguonNganSach = @BudgetSource
		 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@AgencyId)))
		 AND (@LNS IS NULL OR sLNS in (select * from f_split(@LNS)))
		 AND IDuLieuNhan = 0
		 AND iID_DTChungTu IN (
			SELECT iID_DTChungTu
			FROM NS_DT_ChungTu
			WHERE (sSoQuyetDinh IS NOT NULL OR sSoQuyetDinh <> '')
				AND cast(dNgayQuyetDinh as date) <= cast(@VoucherDate AS date))
		 
	   UNION ALL 
	   SELECT 
			iID_MaDonVi,
			ChiTieu = 0,
			TuChi = fTuChi_PheDuyet,
			Thang1 = 0,
			Thang2 = 0,
			Thang3 = 0,
			Thang4 = 0,
			Thang5 = 0,
			Thang6 = 0,
			Thang7 = 0,
			Thang8 = 0, 
			Thang9 = 0,
			Thang10 = 0,
			Thang11 = 0,
			Thang12 = 0
	   FROM f_quyettoan(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonth, @AgencyId, @LNS)

	   UNION ALL 
	   select 
			iID_MaDonVi,
			ChiTieu = 0,
			TuChi = 0,
			isnull([1], 0) AS Thang1,
			isnull([2], 0) AS Thang2,
			isnull([3], 0) AS Thang3,
			isnull([4], 0) AS Thang4,
			isnull([5], 0) AS Thang5,
			isnull([6], 0) AS Thang6,
			isnull([7], 0) AS Thang7,
			isnull([8], 0) AS Thang8,
			isnull([9], 0) AS Thang9,
			isnull([10], 0) AS Thang10,
			isnull([11], 0) AS Thang11,
			isnull([12], 0) AS Thang12
			from (
				select sLNS, iID_MLNS, iID_MaDonVi, isnull(fTuChi_PheDuyet, 0) as TuChi, iThangQuy 
				FROM NS_QT_ChungTuChiTiet
				WHERE iID_QTChungTu in
					(SELECT iID_QTChungTu
					FROM NS_QT_ChungTu
					WHERE iNamLamViec = @YearOfWork
						AND INamNganSach = @YearOfBudget
						AND iID_MaNguonNganSach = @BudgetSource
						AND iThangQuy in (select * from f_split(@QuarterMonth))
						)
					AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@AgencyId)))
					AND (@LNS IS NULL OR sLNS in (select * from f_split(@LNS)))
			) as data
			pivot 
			(
				SUM(TuChi) for iThangQuy IN ( [1], [2], [3], [4], [5], [6], [7], [8], [9], [10], [11], [12] )
			) as Thang
	) AS ct
	LEFT JOIN
	  (SELECT iID_MaDonVi,
			  TenDonVi = iID_MaDonVi + ' - ' + sTenDonVi
	   FROM DonVi
	   WHERE iTrangThai=1
		 AND iNamLamViec = @YearOfWork) AS dv ON dv.iID_MaDonVi = ct.iID_MaDonVi
	GROUP BY dv.iID_MaDonVi,
			 TenDonVi
	HAVING SUM(TuChi) <> 0
	OR SUM(ChiTieu) <> 0
	OR SUM(Thang1) <> 0
	OR SUM(Thang2) <> 0
	OR SUM(Thang3) <> 0
	OR SUM(Thang4) <> 0
	OR SUM(Thang5) <> 0
	OR SUM(Thang6) <> 0
	OR SUM(Thang7) <> 0
	OR SUM(Thang8) <> 0
	OR SUM(Thang9) <> 0
	OR SUM(Thang10) <> 0
	OR SUM(Thang11) <> 0
	OR SUM(Thang12) <> 0
	ORDER BY dv.iID_MaDonVi

END
;
;
;
;
GO
