/****** Object:  StoredProcedure [dbo].[sp_qt_chungtu_chitiet_tao_tonghop]    Script Date: 23/02/2024 4:14:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_chungtu_chitiet_tao_tonghop]    Script Date: 23/02/2024 4:14:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_chungtu_chitiet_tao_tonghop]
	@VoucherIds ntext,
	@VoucherId nvarchar(100),
	@YearOfBudget int,
	@BudgetSource int,
	@YearOfWork int,
	@Type nvarchar(10),
	@QuarterMonthType int,
	@QuarterMonth int,
	@AgencyId nvarchar(10),
	@UserName nvarchar(100)
AS
BEGIN
	INSERT INTO [dbo].[NS_QT_ChungTuChiTiet]
           ([iID_QTChungTu]
           ,[iID_MLNS]
           ,[iID_MLNS_Cha]
           ,[sXauNoiMa]
           ,[sLNS]
           ,[sL]
           ,[sK]
           ,[sM]
           ,[sTM]
           ,[sTTM]
           ,[sNG]
           ,[sTNG]
		   ,[sTNG1]
		   ,[sTNG2]
		   ,[sTNG3]
           ,[bHangCha]
           ,[iNamNganSach]
           ,[iID_MaNguonNganSach]
           ,[iNamLamViec]
           ,[iThangQuyLoai]
           ,[iThangQuy]
           ,[iID_MaDonVi]
           ,[fSoNguoi]
           ,[fSoNgay]
           ,[fSoLuot]
		   ,[fTuChi_DeNghi]
		   ,[fTuChi_PheDuyet]
		   ,[fDeNghi_ChuyenNamSau]
           ,[sGhiChu]
           ,[dNgayTao]
           ,[sNguoiTao]
           ,[dNgaySua]
           ,[sNguoiSua])
    SELECT 
			@VoucherId
           ,mlns.iID_MLNS
           ,mlns.iID_MLNS_Cha
           ,mlns.sXauNoiMa
           ,mlns.sLNS
           ,mlns.sL
           ,mlns.sK
           ,mlns.sM
           ,mlns.sTM
           ,mlns.sTTM
           ,mlns.sNG
           ,mlns.sTNG
		   ,mlns.sTNG1
		   ,mlns.sTNG2
		   ,mlns.sTNG3
           ,mlns.bHangCha
           ,@YearOfBudget
           ,@BudgetSource
           ,@YearOfWork
           ,@QuarterMonthType
           ,@QuarterMonth
           ,@AgencyId
           ,Sum(fSoNguoi)
           ,Sum(fSoNgay)
           ,Sum(fSoLuot)
		   ,Sum(fTuChi_DeNghi)
		   ,Sum(fTuChi_PheDuyet)
		   ,Sum(fDeNghi_ChuyenNamSau)
           ,null
           ,GetDate()
           ,@UserName
           ,null
           ,null
	FROM NS_QT_ChungTuChiTiet ctct
	INNER JOIN NS_MucLucNganSach mlns ON ctct.sXauNoiMa = mlns.sXauNoiMa AND mlns.iNamLamViec = ctct.iNamLamViec
	INNER JOIN NS_QT_ChungTu ct ON ctct.iID_QTChungTu = ct.iID_QTChungTu
	WHERE ct.iID_QTChungTu IN (SELECT * FROM f_split(@VoucherIds)) AND ct.iLoaiChungTu <> 2
	GROUP BY mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG, mlns.sTNG1, mlns.sTNG2, mlns.sTNG3, mlns.bHangCha;

	-- Danh dau chung tu da tong hop
	UPDATE NS_QT_ChungTu SET bDaTongHop = 1 
	WHERE iID_QTChungTu in
		(SELECT *
		 FROM f_split(@VoucherIds))
END
;
;
;
;
;

GO
