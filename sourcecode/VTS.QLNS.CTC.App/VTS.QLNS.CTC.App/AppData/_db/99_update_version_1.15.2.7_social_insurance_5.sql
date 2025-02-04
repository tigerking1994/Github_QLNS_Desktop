/****** Object:  StoredProcedure [dbo].[sp_rptBH_QT_TinhHinhNhanVaQuyetToan]    Script Date: 12/26/2024 2:37:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rptBH_QT_TinhHinhNhanVaQuyetToan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rptBH_QT_TinhHinhNhanVaQuyetToan]
GO
/****** Object:  StoredProcedure [dbo].[sp_rptBH_QT_TinhHinhNhanVaQuyetToan]    Script Date: 12/26/2024 2:37:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rptBH_QT_TinhHinhNhanVaQuyetToan]
	-- Add the parameters for the stored procedure here
	@MaDonVi nvarchar(max),
	@NamLamViec int,
	@DonviTinh int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	CREATE TABLE TBL_DATA (STT nvarchar(50), IsFillData bit, IMa int,IMaCha int, IKieuChu int,  NoiDung nvarchar(50),  FDuToanNamTruoc float, FDuToanNamNay float, FKinhPhiNamTruoc float, FKinhPhiNamNay float, FSoDeNghi float);
	INSERT INTO TBL_DATA(STT, IsFillData, IMa, IMaCha,IKieuChu, NoiDung, FDuToanNamTruoc,FDuToanNamNay,FKinhPhiNamTruoc,FKinhPhiNamNay,FSoDeNghi)
	SELECT 'A'   ,0, 1, 0, 1, N'Qũy BHXH', 0 , 0, 0 , 0 , 0 
	UNION ALL    	  
	SELECT '1'   ,1, 2, 1, 2, N'Chi các chế độ BHXH', 0 , 0, 0 , 0 , 0 
	UNION ALL    	  
	SELECT '2'   ,1, 3, 1, 2, N'Chi kinh phí quản lý BHXH, BHYT', 0 , 0, 0 , 0 , 0 
	UNION ALL    	  
	SELECT 'B'   ,0, 4, 0, 1, N'Qũy BHYT', 0 , 0, 0 , 0 , 0 
	UNION ALL    	  
	SELECT '1'   ,0, 5, 4, 2, N'Chi kinh phí chăm sóc sức khỏe ban đầu ', 0 , 0, 0 , 0 , 0 
	UNION ALL		  
	SELECT '1.1' ,1, 6, 5, 3, N'Người lao động', 0 , 0, 0 , 0 , 0 
	UNION ALL		  
	SELECT '1.2' ,1, 7, 5, 3, N'Học sinh, sinh viên', 0 , 0, 0 , 0 , 0 
	UNION ALL		  
	SELECT '2'	 ,1, 8, 4, 2, N'Chi KCB của QN tại quân y đơn vị (10%)', 0 , 0, 0 , 0 , 0 
	UNION ALL	 	  
	SELECT '3'	 ,1, 9, 4, 2, N'Chi mua sắm trang thiết bị y tế', 0 , 0, 0 , 0 , 0 
	UNION ALL	 
	SELECT '4'	 ,1, 10,4, 2, N'Chi kinh phí KCB tại Trường sa - DK', 0 , 0, 0 , 0 , 0 ;

	--GetData DuToanNamTruoc
	CREATE TABLE TBL_DATA_DUTOAN_NAMTRUOC(IMa int, MaDonVi nvarchar(50), FDuToanNamTruoc float);
	INSERT INTO TBL_DATA_DUTOAN_NAMTRUOC(IMa, MaDonVi, FDuToanNamTruoc)
	SELECT 3 , '', SUM(ISNULL(fSoThamDinh, 0)) 
	FROM BH_ThamDinhQuyetToan_ChungTuChiTiet  WHERE iMa = 254 and iNamLamViec = @NamLamViec - 1 and iID_MaDonVi IN (select * from splitstring(@MaDonVi))
	--GROUP BY iID_MaDonVi
	UNION ALL
	SELECT 8 , '', SUM(ISNULL(fSoThamDinh, 0)) 
	FROM BH_ThamDinhQuyetToan_ChungTuChiTiet  WHERE iMa = 225 and iNamLamViec = @NamLamViec - 1 and iID_MaDonVi IN (select * from splitstring(@MaDonVi))
	--GROUP BY iID_MaDonVi
	UNION ALL
	SELECT 9 , '', SUM(ISNULL(fSoThamDinh, 0)) 
	FROM BH_ThamDinhQuyetToan_ChungTuChiTiet  WHERE iMa = 274 and iNamLamViec = @NamLamViec - 1 and iID_MaDonVi IN (select * from splitstring(@MaDonVi))
	--GROUP BY iID_MaDonVi
	UNION ALL
	SELECT 10 , '', SUM(ISNULL(fSoThamDinh, 0)) 
	FROM BH_ThamDinhQuyetToan_ChungTuChiTiet  WHERE iMa = 240 and iNamLamViec = @NamLamViec - 1 and iID_MaDonVi IN (select * from splitstring(@MaDonVi));
	--GROUP BY iID_MaDonVi

	--GetData DuToanNamNay
	CREATE TABLE TBL_DATA_DUTOAN_NAMNAY(IMa int, MaDonVi nvarchar(50), FDuToanNamNay float);
	INSERT INTO TBL_DATA_DUTOAN_NAMNAY(IMa, MaDonVi, FDuToanNamNay)
	SELECT 2 , '', SUM(ISNULL(fTienTuChi, 0)) 
	FROM BH_DTC_PhanBoDuToanChi_ChiTiet  WHERE ( sXauNoiMa like '9010001%' or sXauNoiMa like '9010002%')  and iNamLamViec = @NamLamViec  and iID_MaDonVi IN (select * from splitstring(@MaDonVi))
	--GROUP BY iID_MaDonVi
	UNION ALL
	SELECT 3 , '', SUM(ISNULL(fTienTuChi, 0)) 
	FROM BH_DTC_PhanBoDuToanChi_ChiTiet  WHERE  sXauNoiMa like '9010003%' and iNamLamViec = @NamLamViec and iID_MaDonVi IN (select * from splitstring(@MaDonVi))
	--GROUP BY iID_MaDonVi
	UNION ALL
	SELECT 8 , '', SUM(ISNULL(fTienTuChi, 0)) 
	FROM BH_DTC_PhanBoDuToanChi_ChiTiet  WHERE sXauNoiMa like '9010004%'  and iNamLamViec = @NamLamViec  and iID_MaDonVi IN (select * from splitstring(@MaDonVi))
	--GROUP BY iID_MaDonVi
	UNION ALL
	SELECT 9 , '', SUM(ISNULL(fTienTuChi, 0)) 
	FROM BH_DTC_PhanBoDuToanChi_ChiTiet  WHERE  sXauNoiMa like '9010009%'  and iNamLamViec = @NamLamViec  and iID_MaDonVi IN (select * from splitstring(@MaDonVi))
	--GROUP BY iID_MaDonVi
	UNION ALL
	SELECT 10 , '', SUM(ISNULL(fTienTuChi, 0)) 
	FROM BH_DTC_PhanBoDuToanChi_ChiTiet  WHERE  sXauNoiMa like '9010006%'  and iNamLamViec = @NamLamViec  and iID_MaDonVi IN (select * from splitstring(@MaDonVi));
	--GROUP BY iID_MaDonVi

	--GetData KinhPhiNamTruoc
	CREATE TABLE TBL_DATA_KINHPHI_NAMTRUOC(IMa int, MaDonVi nvarchar(50), FKinhPhiNamTruoc float);
	INSERT INTO TBL_DATA_KINHPHI_NAMTRUOC(IMa, MaDonVi, FKinhPhiNamTruoc)
	SELECT 2 , '', SUM(ISNULL(fSoThamDinh, 0)) 
	FROM BH_ThamDinhQuyetToan_ChungTuChiTiet  WHERE iMa = 202 and iNamLamViec = @NamLamViec - 1 and iID_MaDonVi IN (select * from splitstring(@MaDonVi))
	--GROUP BY iID_MaDonVi
	UNION ALL
	SELECT 3 , '', SUM(ISNULL(fSoThamDinh, 0)) 
	FROM BH_ThamDinhQuyetToan_ChungTuChiTiet  WHERE iMa = 253 and iNamLamViec = @NamLamViec - 1 and iID_MaDonVi IN (select * from splitstring(@MaDonVi))
	--GROUP BY iID_MaDonVi
	UNION ALL
	SELECT 6 , '', SUM(ISNULL(fSoThamDinh, 0)) 
	FROM BH_ThamDinhQuyetToan_ChungTuChiTiet  WHERE iMa = 210 and iNamLamViec = @NamLamViec - 1 and iID_MaDonVi IN (select * from splitstring(@MaDonVi))
	--GROUP BY iID_MaDonVi
	UNION ALL
	SELECT 7 , '', SUM(ISNULL(fSoThamDinh, 0)) 
	FROM BH_ThamDinhQuyetToan_ChungTuChiTiet  WHERE iMa = 216 and iNamLamViec = @NamLamViec - 1 and iID_MaDonVi IN (select * from splitstring(@MaDonVi))
	--GROUP BY iID_MaDonVi

	UNION ALL
	SELECT 8 , '', SUM(ISNULL(fSoThamDinh, 0)) 
	FROM BH_ThamDinhQuyetToan_ChungTuChiTiet  WHERE iMa = 224 and iNamLamViec = @NamLamViec - 1 and iID_MaDonVi IN (select * from splitstring(@MaDonVi))
	--GROUP BY iID_MaDonVi
	UNION ALL
	SELECT 9 , '', SUM(ISNULL(fSoThamDinh, 0)) 
	FROM BH_ThamDinhQuyetToan_ChungTuChiTiet  WHERE iMa = 232 and iNamLamViec = @NamLamViec - 1 and iID_MaDonVi IN (select * from splitstring(@MaDonVi))
	--GROUP BY iID_MaDonVi
	UNION ALL
	SELECT 10 , '', SUM(ISNULL(fSoThamDinh, 0)) 
	FROM BH_ThamDinhQuyetToan_ChungTuChiTiet  WHERE iMa = 239 and iNamLamViec = @NamLamViec - 1 and iID_MaDonVi IN (select * from splitstring(@MaDonVi));
	--GROUP BY iID_MaDonVi

	
	--GetData KinhPhiNamNay
	CREATE TABLE TBL_DATA_KINHPHI_NAMNAY(IMa int, MaDonVi nvarchar(50), FKinhPhiNamNay float);
	INSERT INTO TBL_DATA_KINHPHI_NAMNAY(IMa, MaDonVi, FKinhPhiNamNay)
	SELECT 2 , '', SUM(ISNULL(fTienKeHoachCap, 0)) 
	FROM BH_CP_ChungTu_ChiTiet  WHERE sXauNoiMa = '901'  and iNamLamViec = @NamLamViec and iID_MaDonVi IN (select * from splitstring(@MaDonVi))
	--GROUP BY iID_MaDonVi
	UNION ALL
	SELECT 3 , '', SUM(ISNULL(fTienKeHoachCap, 0)) 
	FROM BH_CP_ChungTu_ChiTiet  WHERE sXauNoiMa = '9010003' and iNamLamViec = @NamLamViec and iID_MaDonVi IN (select * from splitstring(@MaDonVi))
	--GROUP BY iID_MaDonVi
	UNION ALL
	SELECT 6 , '', SUM(ISNULL(fTienKeHoachCap, 0)) 
	FROM BH_CP_ChungTu_ChiTiet  WHERE  sXauNoiMa = '9050001-010-011-0001'  and iNamLamViec = @NamLamViec and iID_MaDonVi IN (select * from splitstring(@MaDonVi))
	--GROUP BY iID_MaDonVi
	UNION ALL
	SELECT 7 , '', SUM(ISNULL(fTienKeHoachCap, 0)) 
	FROM BH_CP_ChungTu_ChiTiet  WHERE  sXauNoiMa = '9050002-010-011-0002'  and iNamLamViec = @NamLamViec and iID_MaDonVi IN (select * from splitstring(@MaDonVi))
	--GROUP BY iID_MaDonVi

	UNION ALL
	SELECT 8 , '', SUM(ISNULL(fTienKeHoachCap, 0)) 
	FROM BH_CP_ChungTu_ChiTiet  WHERE  sXauNoiMa = '9010004'  and iNamLamViec = @NamLamViec and iID_MaDonVi IN (select * from splitstring(@MaDonVi))
	--GROUP BY iID_MaDonVi
	UNION ALL
	SELECT 9 , '', SUM(ISNULL(fTienKeHoachCap, 0)) 
	FROM BH_CP_ChungTu_ChiTiet  WHERE  sXauNoiMa = '9010009'  and iNamLamViec = @NamLamViec and iID_MaDonVi IN (select * from splitstring(@MaDonVi))
	--GROUP BY iID_MaDonVi
	UNION ALL
	SELECT 10 , '', SUM(ISNULL(fTienKeHoachCap, 0)) 
	FROM BH_CP_ChungTu_ChiTiet  WHERE  sXauNoiMa = '9010006' and iNamLamViec = @NamLamViec and iID_MaDonVi IN (select * from splitstring(@MaDonVi));
	--GROUP BY iID_MaDonVi

		--GetData SoDeNghiQuyetDinh
	CREATE TABLE TBL_DATA_SODENGHI(IMa int, MaDonVi nvarchar(50), FSoDeNghi float);
	INSERT INTO TBL_DATA_SODENGHI(IMa, MaDonVi, FSoDeNghi)
	SELECT 2 , '', SUM(ISNULL(fTienSQ_ThucChi, 0)+ISNULL(fTienQNCN_ThucChi,0)+ISNULL(fTienHSQBS_ThucChi,0)+ISNULL(fTienCNVCQP_ThucChi,0)+ISNULL(fTienLDHD_ThucChi,0)) 
	FROM BH_QTC_Nam_CheDoBHXH_ChiTiet  WHERE iNamLamViec = @NamLamViec  and iID_MaDonVi IN (select * from splitstring(@MaDonVi))
	--GROUP BY iID_MaDonVi
	UNION ALL
	SELECT 3 , '', SUM(ISNULL(fTien_ThucChi, 0)) 
	FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet  WHERE iNamLamViec = @NamLamViec  and iID_MaDonVi IN (select * from splitstring(@MaDonVi))
	--GROUP BY iID_MaDonVi
	UNION ALL
	SELECT 6 , '', SUM(ISNULL(fTien_ThucChi, 0)) 
	FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet  WHERE  sXauNoiMa LIKE '9050001-010-011-0001%'  and iNamLamViec = @NamLamViec and iID_MaDonVi IN (select * from splitstring(@MaDonVi))
	--GROUP BY iID_MaDonVi
	UNION ALL
	SELECT 7 , '', SUM(ISNULL(fTien_ThucChi, 0)) 
	FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet  WHERE  sXauNoiMa LIKE '9050002-010-011-0002%'  and iNamLamViec = @NamLamViec and iID_MaDonVi IN (select * from splitstring(@MaDonVi))
	--GROUP BY iID_MaDonVi

	UNION ALL
	SELECT 8 , '', SUM(ISNULL(fTien_ThucChi, 0)) 
	FROM BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet  WHERE iNamLamViec = @NamLamViec  and iID_MaDonVi IN (select * from splitstring(@MaDonVi))
	--GROUP BY iID_MaDonVi
	UNION ALL
	SELECT 9 , '', SUM(ISNULL(fTien_ThucChi, 0)) 
	FROM BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet  WHERE  sXauNoiMa = '9010009%'  and iNamLamViec = @NamLamViec  and iID_MaDonVi IN (select * from splitstring(@MaDonVi))
	--GROUP BY iID_MaDonVi
	UNION ALL
	SELECT 10 , '', SUM(ISNULL(fTien_ThucChi, 0)) 
	FROM BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet  WHERE  sXauNoiMa = '9010006%' and iNamLamViec = @NamLamViec and iID_MaDonVi IN (select * from splitstring(@MaDonVi));
	--GROUP BY iID_MaDonVi;;


	---------RESULTS----------
	SELECT STT,
		  t1.IMa,
		  t1.IMaCha,
		  t1.IKieuChu,
		  t1.NoiDung,
		  ISNULL(t2.FDuToanNamTruoc, 0 )/@DonviTinh as FDuToanNamTruoc,
		  ISNULL(t3.FDuToanNamNay, 0 )/@DonviTinh as FDuToanNamNay,
		  ISNULL(t4.FKinhPhiNamTruoc, 0 )/@DonviTinh as FKinhPhiNamTruoc,
		  ISNULL(t5.FKinhPhiNamNay, 0 )/@DonviTinh as FKinhPhiNamNay,
		  ISNULL(t6.FSoDeNghi, 0 )/@DonviTinh as FSoDeNghi,
		  t1.IsFillData
	FROM TBL_DATA t1
	LEFT JOIN TBL_DATA_DUTOAN_NAMTRUOC t2 ON t1.IMa = t2.IMa
	LEFT JOIN TBL_DATA_DUTOAN_NAMNAY t3 ON t1.IMa = t3.IMa
	LEFT JOIN TBL_DATA_KINHPHI_NAMTRUOC t4 ON t1.IMa = t4.IMa
	LEFT JOIN TBL_DATA_KINHPHI_NAMNAY t5 ON t1.IMa = t5.IMa
	LEFT JOIN TBL_DATA_SODENGHI t6 ON t1.IMa = t6.IMa



	----DROP TABLE -----
	DROP TABLE TBL_DATA, TBL_DATA_DUTOAN_NAMNAY,TBL_DATA_DUTOAN_NAMTRUOC,TBL_DATA_KINHPHI_NAMNAY,TBL_DATA_KINHPHI_NAMTRUOC,TBL_DATA_SODENGHI;
END
GO
