/****** Object:  StoredProcedure [dbo].[sp_clone_data_mlns]    Script Date: 5/22/2024 6:57:15 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_data_mlns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_data_mlns]
GO
/****** Object:  StoredProcedure [dbo].[sp_config_budget_category]    Script Date: 5/22/2024 7:12:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_config_budget_category]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_config_budget_category]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_data_mlns]    Script Date: 5/22/2024 6:57:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[sp_clone_data_mlns] 
	-- Add the parameters for the stored procedure here
	@sourceYear int,
	@destinationYear int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM [NS_MucLucNganSach] WHERE iNamLamViec = @destinationYear;
			INSERT INTO [NS_MucLucNganSach]
				([iID_MLNS]
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
				,[sMoTa]
				,[iNamLamViec]
				,[bHangCha]
				,[iTrangThai]
				,[iID_MaBQuanLy]
				,[dNgayTao]
				,[sNguoiTao]
				,[dNgaySua]
				,[sNguoiSua]
				,[Tag]
				,[Log]
				,[iLock]
				,[iLoai]
				,[sTNG1]
				,[sTNG2]
				,[sTNG3]
				,[sChiTietToi]
				,[bNgay]
				,[bSoNguoi]
				,[bTonKho]
				,[bTuChi]
				,[bHangNhap]
				,[bHangMua]
				,[bHienVat]
				,[bDuPhong]
				,[bPhanCap]
				,[sNhapTheoTruong]
				,[iID_MaDonVi]
				,[sCPChiTietToi]
				,[bHangChaDuToan]
				,[bHangChaQuyetToan]
				,[sDuToanChiTietToi]
				,[sQuyetToanChiTietToi])
			 SELECT [iID_MLNS]
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
				,[sMoTa]
				,@destinationYear
				,[bHangCha]
				,[iTrangThai]
				,[iID_MaBQuanLy]
				,GETDATE()
				,null
				,null
				,null
				,[Tag]
				,[Log]
				,[iLock]
				,[iLoai]
				,[sTNG1]
				,[sTNG2]
				,[sTNG3]
				,[sChiTietToi]
				,[bNgay]
				,[bSoNguoi]
				,[bTonKho]
				,[bTuChi]
				,[bHangNhap]
				,[bHangMua]
				,[bHienVat]
				,[bDuPhong]
				,[bPhanCap]
				,[sNhapTheoTruong]
				,[iID_MaDonVi]
				,[sCPChiTietToi]
				,[bHangChaDuToan]
				,[bHangChaQuyetToan]
				,[sDuToanChiTietToi]
				,[sQuyetToanChiTietToi]
		  FROM [NS_MucLucNganSach] where iNamLamViec = @sourceYear;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_config_budget_category]    Script Date: 5/22/2024 7:12:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[sp_config_budget_category]

AS
BEGIN

	SET NOCOUNT ON;

	UPDATE a
	SET a.iID_MLNS_Cha = b.iID_MLNS
	FROM ns_muclucngansach a
	INNER JOIN ns_muclucngansach b
	  ON (a.sXauNoiMa LIKE CONCAT(b.sXauNoiMa, '-', '%')
	  AND ((LEN(a.sXauNoiMa) - LEN(b.sxaunoima) = 3)
	  OR (LEN(a.sXauNoiMa) - LEN(b.sxaunoima) = 4)
	  OR (LEN(a.sXauNoiMa) - LEN(b.sxaunoima) = 5)))
	  OR (a.sXauNoiMa LIKE CONCAT(b.sXauNoiMa, '%')
	  AND ((LEN(a.sXauNoiMa) = 3
	  AND LEN(b.sxaunoima) = 1)
	  OR (LEN(a.sXauNoiMa) = 7
	  AND LEN(b.sxaunoima) = 3)))
	  AND a.iNamLamViec = b.iNamLamViec
	WHERE a.iID_MLNS_Cha IS NULL

	UPDATE a
	set a.iID_MLNS_Cha = b.iID_MLNS_Cha
	from NS_CP_ChungTuChiTiet a
	inner join NS_MucLucNganSach b
	on a.sXauNoiMa = b.sXauNoiMa  and a.iNamLamViec = b.iNamLamViec

	UPDATE a
	set a.iID_MLNS_Cha = b.iID_MLNS_Cha
	from NS_DT_ChungTuChiTiet a
	inner join NS_MucLucNganSach b
	on a.sXauNoiMa = b.sXauNoiMa  and a.iNamLamViec = b.iNamLamViec

	UPDATE a
	set a.iID_MLNS_Cha = b.iID_MLNS_Cha
	from NS_QT_ChungTuChiTiet a
	inner join NS_MucLucNganSach b
	on a.sXauNoiMa = b.sXauNoiMa  and a.iNamLamViec = b.iNamLamViec
END
;
GO
