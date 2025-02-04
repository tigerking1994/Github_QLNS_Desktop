/****** Object:  StoredProcedure [dbo].[sp_bh_report_tong_hop_quyet_toan_nam]    Script Date: 12/6/2023 6:06:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_report_tong_hop_quyet_toan_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_report_tong_hop_quyet_toan_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_report_tong_hop_quyet_toan_nam]    Script Date: 12/6/2023 6:06:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<doantc,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_bh_report_tong_hop_quyet_toan_nam]
	-- Add the parameters for the stored procedure here
	@NamLamViec int ,
	@LstMaDonVi nvarchar(max),
	@Type int, --1:thu, 2:chi
	@DVT int
AS
BEGIN
    -- Insert statements for procedure here
	CREATE TABLE #result(STT nvarchar(50),IIdChungTu uniqueidentifier, IIdParent uniqueidentifier, SNoiDung nvarchar(200), ILevel int, IThuTu int, ILoaiChi int , FDuToan float, FHachToan Float, IKinhPhiKCB int)
	IF(@Type = 1)
		BEGIN
			DECLARE @IIdThuBHYT uniqueidentifier = NewID();
			INSERT INTO #result(STT,IIdChungTu, IIdParent, SNoiDung, ILevel, IThuTu, ILoaiChi, FDuToan, FHachToan,IKinhPhiKCB)
			--<DATA INSERT>--
			(SELECT 'I',NEWID(), NULL, N'Thu BHXH', 1, 1, @Type,
				SUM( CASE WHEN sLNS = '9020001' THEN ISNULL(fTongSoPhaiThuBHXH, 0) ELSE 0 END)/@DVT,
				SUM( CASE WHEN sLNS = '9020002' THEN ISNULL(fTongSoPhaiThuBHXH, 0) ELSE 0 END)/@DVT,
				0
				FROM BH_QTT_BHXH_ChungTu_ChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND  (sLNS = '9020002' OR sLNS = '9020001')
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT 'II',NEWID(), NULL, N'Thu BHTN', 1, 2, @Type,
				SUM( CASE WHEN sLNS = '9020001' THEN ISNULL(fTongSoPhaiThuBHTN, 0) ELSE 0 END)/@DVT,
				SUM( CASE WHEN sLNS = '9020002' THEN ISNULL(fTongSoPhaiThuBHTN, 0) ELSE 0 END)/@DVT,
				0
				FROM BH_QTT_BHXH_ChungTu_ChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND  (sLNS = '9020002' OR sLNS = '9020001')
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT 'II', @IIdThuBHYT, NULL, N'Thu BHXH', 1, 3, @Type,
				0,
				0,
				0
			UNION ALL
			SELECT '1',NEWID(), @IIdThuBHYT, N'Thu BHYT quân nhân', 2, 1, @Type,
				SUM( CASE WHEN sXauNoiMa LIKE '9020001-010-011-0001%' THEN ISNULL(fTongSoPhaiThuBHYT, 0) ELSE 0 END)/@DVT,
				SUM( CASE WHEN sXauNoiMa = '9020002-010-011-0001%' THEN ISNULL(fTongSoPhaiThuBHYT, 0) ELSE 0 END)/@DVT,
				0
				FROM BH_QTT_BHXH_ChungTu_ChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND  (sXauNoiMa LIKE '9020001-010-011-0001%' OR sXauNoiMa = '9020002-010-011-0001%')
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '2',NEWID(), @IIdThuBHYT, N'Thu BHYT người lao động', 2, 2, @Type,
				SUM( CASE WHEN sXauNoiMa LIKE '9020001-010-011-0002%' THEN ISNULL(fTongSoPhaiThuBHYT, 0) ELSE 0 END)/@DVT,
				SUM( CASE WHEN sXauNoiMa = '9020002-010-011-0002%' THEN ISNULL(fTongSoPhaiThuBHYT, 0) ELSE 0 END)/@DVT,
				0
				FROM BH_QTT_BHXH_ChungTu_ChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND  (sXauNoiMa LIKE '9020001-010-011-0002%' OR sXauNoiMa = '9020002-010-011-0002%')
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '3',NEWID(), @IIdThuBHYT, N'Thu BHYT thân nhân quân nhân', 2, 3, @Type,
				SUM( CASE WHEN sXauNoiMa LIKE '9030001-010-011-0000%' THEN ISNULL(fSoPhaiThu, 0) ELSE 0 END)/@DVT,
				SUM( CASE WHEN sXauNoiMa = '9030001-010-011-0001%' THEN ISNULL(fSoPhaiThu, 0) ELSE 0 END)/@DVT,
				0
				FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND  (sXauNoiMa LIKE '9030001-010-011-0000%' OR sXauNoiMa = '9030001-010-011-0001%')
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '4',NEWID(), @IIdThuBHYT, N'Thu BHYT thân nhân CNVCQP', 2, 4, @Type,
				SUM( CASE WHEN sXauNoiMa LIKE '9030002-010-011-0000%' THEN ISNULL(fSoPhaiThu, 0) ELSE 0 END)/@DVT,
				SUM( CASE WHEN sXauNoiMa = '9030002-010-011-0001%' THEN ISNULL(fSoPhaiThu, 0) ELSE 0 END)/@DVT,
				0
				FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND  (sXauNoiMa LIKE '9030002-010-011-0001%' OR sXauNoiMa = '9030002-010-011-0000%')
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '5',NEWID(), @IIdThuBHYT, N'Thu BHYT HVQS Xã phường', 2, 5, @Type,
				SUM(ISNULL(fSoPhaiThu, 0))/@DVT,
				0,
				0
				FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet
				WHERE iNamLamViec = @NamLamViec
					AND sXauNoiMa = '9030003'
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '6',NEWID(), @IIdThuBHYT, N'Thu BHYT SQDB', 2, 6, @Type,
				SUM(ISNULL(fSoPhaiThu, 0))/@DVT,
				0,
				0
				FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet
				WHERE iNamLamViec = @NamLamViec
					AND sXauNoiMa = '9030005'
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '7',NEWID(), @IIdThuBHYT, N'Thu BHYT HS,SV', 2, 7, @Type,
				SUM(ISNULL(fSoPhaiThu, 0))/@DVT,
				0,
				0
				FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet
				WHERE iNamLamViec = @NamLamViec
					AND sXauNoiMa = '9030004'
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '8',NEWID(), @IIdThuBHYT, N'Thu BHYT Lưu HS', 2, 8, @Type,
				SUM(ISNULL(fSoPhaiThu, 0))/@DVT,
				0,
				0
				FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet
				WHERE iNamLamViec = @NamLamViec
					AND sXauNoiMa = '9030006'
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			);
			SELECT * from #result

		END
	ELSE
		BEGIN
			---Phần chi---
			DECLARE @IIdChiCDBHYT uniqueidentifier = NewID();
			DECLARE @IIdKinhPhiKCB uniqueidentifier = NewID();
			DECLARE @IIdChiTieuKCB uniqueidentifier = NewID();
			DECLARE @IIdChiCSK uniqueidentifier = NewID();
			INSERT INTO #result(STT,IIdChungTu, IIdParent, SNoiDung, ILevel, IThuTu, ILoaiChi, FDuToan, FHachToan,IKinhPhiKCB)
			--<DATA INSERT>--
			(
			SELECT 'I',@IIdChiCDBHYT, NULL, N'Chi các chế độ BHXH', 1, 1, @Type,
				0,
				0,
				0
			UNION ALL
			SELECT '1',NEWID(), @IIdChiCDBHYT, N'Trợ cấp ốm đau', 2, 1, @Type,
				SUM( CASE WHEN sXauNoiMa LIKE '9010001-010-011-0001%' THEN ISNULL(fTongTien_ThucChi, 0) ELSE 0 END)/@DVT,
				SUM( CASE WHEN sXauNoiMa = '9010002-010-011-0001%' THEN ISNULL(fTongTien_ThucChi, 0) ELSE 0 END)/@DVT,
				0
				FROM BH_QTC_Nam_CheDoBHXH_ChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND  (sXauNoiMa LIKE '9010001-010-011-0001%' OR sXauNoiMa = '9010002-010-011-0001%')
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '2',NEWID(), @IIdChiCDBHYT, N'Trợ cấp thai sản', 2, 2, @Type,
				SUM( CASE WHEN sXauNoiMa LIKE '9010001-010-011-0002%' THEN ISNULL(fTongTien_ThucChi, 0) ELSE 0 END)/@DVT,
				SUM( CASE WHEN sXauNoiMa = '9010002-010-011-0002%' THEN ISNULL(fTongTien_ThucChi, 0) ELSE 0 END)/@DVT,
				0
				FROM BH_QTC_Nam_CheDoBHXH_ChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND  (sXauNoiMa LIKE '9010001-010-011-0002%' OR sXauNoiMa = '9010002-010-011-0002%')
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '3',NEWID(), @IIdChiCDBHYT, N'Trợ cấp tai nạn lao động, BNN', 2, 3, @Type,
				SUM( CASE WHEN sXauNoiMa LIKE '9010001-010-011-0003%' THEN ISNULL(fTongTien_ThucChi, 0) ELSE 0 END)/@DVT,
				SUM( CASE WHEN sXauNoiMa = '9010002-010-011-0003%' THEN ISNULL(fTongTien_ThucChi, 0) ELSE 0 END)/@DVT,
				0
				FROM BH_QTC_Nam_CheDoBHXH_ChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND  (sXauNoiMa LIKE '9010001-010-011-0001%' OR sXauNoiMa = '9010002-010-011-0001%')
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '4',NEWID(), @IIdChiCDBHYT, N'Trợ cấp hưu trí', 2, 4, @Type,
				SUM( CASE WHEN sXauNoiMa LIKE '9010001-010-011-0004%' THEN ISNULL(fTongTien_ThucChi, 0) ELSE 0 END)/@DVT,
				SUM( CASE WHEN sXauNoiMa = '9010002-010-011-0004%' THEN ISNULL(fTongTien_ThucChi, 0) ELSE 0 END)/@DVT,
				0
				FROM BH_QTC_Nam_CheDoBHXH_ChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND  (sXauNoiMa LIKE '9010001-010-011-0004%' OR sXauNoiMa = '9010002-010-011-0004%')
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '5',NEWID(), @IIdChiCDBHYT, N'Trợ cấp phục viên', 2, 5, @Type,
				SUM( CASE WHEN sXauNoiMa LIKE '9010001-010-011-0005%' THEN ISNULL(fTongTien_ThucChi, 0) ELSE 0 END)/@DVT,
				SUM( CASE WHEN sXauNoiMa = '9010002-010-011-0005%' THEN ISNULL(fTongTien_ThucChi, 0) ELSE 0 END)/@DVT,
				0
				FROM BH_QTC_Nam_CheDoBHXH_ChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND  (sXauNoiMa LIKE '9010001-010-011-0005%' OR sXauNoiMa = '9010002-010-011-0005%')
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '6',NEWID(), @IIdChiCDBHYT, N'Trợ cấp xuất ngũ', 2, 6, @Type,
				SUM( CASE WHEN sXauNoiMa LIKE '9010001-010-011-0006%' THEN ISNULL(fTongTien_ThucChi, 0) ELSE 0 END)/@DVT,
				SUM( CASE WHEN sXauNoiMa = '9010002-010-011-0006%' THEN ISNULL(fTongTien_ThucChi, 0) ELSE 0 END)/@DVT,
				0
				FROM BH_QTC_Nam_CheDoBHXH_ChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND  (sXauNoiMa LIKE '9010001-010-011-0006%' OR sXauNoiMa = '9010002-010-011-0006%')
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '7',NEWID(), @IIdChiCDBHYT, N'Trợ cấp thôi việc', 2, 7, @Type,
					SUM( CASE WHEN sXauNoiMa LIKE '9010001-010-011-0007%' THEN ISNULL(fTongTien_ThucChi, 0) ELSE 0 END)/@DVT,
					SUM( CASE WHEN sXauNoiMa = '9010002-010-011-0007%' THEN ISNULL(fTongTien_ThucChi, 0) ELSE 0 END)/@DVT,
					0
				FROM BH_QTC_Nam_CheDoBHXH_ChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND  (sXauNoiMa LIKE '9010001-010-011-0007%' OR sXauNoiMa = '9010002-010-011-0007%')
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '8',NEWID(), @IIdChiCDBHYT, N'Trợ cấp tử tuất', 2, 8, @Type,
					SUM( CASE WHEN sXauNoiMa LIKE '9010001-010-011-0008%' THEN ISNULL(fTongTien_ThucChi, 0) ELSE 0 END)/@DVT,
					SUM( CASE WHEN sXauNoiMa = '9010002-010-011-0008%' THEN ISNULL(fTongTien_ThucChi, 0) ELSE 0 END)/@DVT,
					0
				FROM BH_QTC_Nam_CheDoBHXH_ChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND  (sXauNoiMa LIKE '9010001-010-011-0008%' OR sXauNoiMa = '9010002-010-011-0008%')
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL ---II---
			SELECT 'II',NEWID(), NULL, N'Kinh phí quản lý BHXH, BHYT', 1, 2, @Type,
					SUM( ISNULL(fTien_ThucChi, 0))/@DVT,
					0,
					0
				FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND  (sXauNoiMa LIKE '9010003')
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT 'III',@IIdKinhPhiKCB, NULL, N'Kinh phí KCB tại quân y đơn vị', 1, 3, @Type,
					0,
					0,
					0
			UNION ALL
			SELECT '1',@IIdChiTieuKCB, @IIdKinhPhiKCB, N'Chỉ tiêu', 2, 1, @Type,
				0,
				0,
				1
			UNION ALL
			SELECT '-',NEWID(), @IIdChiTieuKCB, N'Năm trước chuyển sang', 3, 1, @Type,
					SUM( CASE WHEN sXauNoiMa LIKE '9010004%' THEN ISNULL(fTien_DuToanNamTruocChuyenSang, 0) ELSE 0 END)/@DVT,
					SUM( CASE WHEN sXauNoiMa LIKE '9010005%' THEN ISNULL(fTien_DuToanNamTruocChuyenSang, 0) ELSE 0 END)/@DVT,
					0
				FROM BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND  (sXauNoiMa LIKE '9010004' OR sXauNoiMa LIKE '9010005')
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '-',NEWID(), @IIdChiTieuKCB, N'Năm nay', 3, 2, @Type,
					SUM( CASE WHEN sXauNoiMa LIKE '9010004%' THEN ISNULL(fTien_DuToanGiaoNamNay, 0) ELSE 0 END)/@DVT,
					SUM( CASE WHEN sXauNoiMa LIKE '9010005%' THEN ISNULL(fTien_DuToanGiaoNamNay, 0) ELSE 0 END)/@DVT,
					0
				FROM BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND  (sXauNoiMa LIKE '9010004' OR sXauNoiMa LIKE '9010005')
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '2',NEWID(), @IIdKinhPhiKCB, N'Số quyết toán', 2, 2, @Type,
					SUM( CASE WHEN sXauNoiMa LIKE '9010004%' THEN ISNULL(fTien_ThucChi, 0) ELSE 0 END)/@DVT,
					SUM( CASE WHEN sXauNoiMa LIKE '9010005%' THEN ISNULL(fTien_ThucChi, 0) ELSE 0 END)/@DVT,
					2
				FROM BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND  (sXauNoiMa LIKE '9010004' OR sXauNoiMa LIKE '9010005')
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '3',NEWID(), @IIdKinhPhiKCB, N'Chỉ tiêu còn thừa', 2, 3, @Type,
				0,
				0,
				3
			UNION ALL --IV--
			SELECT 'IV',NEWID(), NULL, N'Kinh phí KCB tại trường sa -DK', 1, 4, @Type,
					SUM( CASE WHEN sXauNoiMa LIKE '9010006%' THEN ISNULL(fTien_ThucChi, 0) ELSE 0 END)/@DVT,
					SUM( CASE WHEN sXauNoiMa LIKE '9010007%' THEN ISNULL(fTien_ThucChi, 0) ELSE 0 END)/@DVT,
					0
				FROM BH_QTC_Nam_KPK_ChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND  (sXauNoiMa LIKE '9010006%' OR sXauNoiMa LIKE '9010007%')
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL --V--
			SELECT 'V',NEWID(), NULL, N'Chi phí mua sắm thiết bị y tế', 1, 5, @Type,
					SUM( ISNULL(fTien_ThucChi, 0))/@DVT,
					0,
					0
				FROM BH_QTC_Nam_KPK_ChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND  (sXauNoiMa LIKE '9010009%')
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL --VI--
			SELECT 'VI',@IIdChiCSK, NULL, N'Chi chăm sóc sức khỏe ban đầu', 1, 6, @Type,
					0,
					0,
					0
			UNION ALL --VI-- . 1
			SELECT '-',NEWID(), @IIdChiCSK, N'CSSK ban đầu người lao động', 3, 1, @Type,
					SUM( ISNULL(fTien_ThucChi, 0))/@DVT,
					0,
					0
				FROM BH_QTC_Nam_KPK_ChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND  (sXauNoiMa = '9050001')
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL --VI-- . 2
			SELECT '-',NEWID(), @IIdChiCSK, N'CSSK ban đầu HS, SV', 3, 2, @Type,
					SUM( ISNULL(fTien_ThucChi, 0))/@DVT,
					0,
					0
				FROM BH_QTC_Nam_KPK_ChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND  (sXauNoiMa = '9050002')
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL --VII-- 
			SELECT 'VII',NEWID(), NULL, N'Chi hỗ trợ người lao động tham gia BHTN', 1, 7, @Type,
					SUM( ISNULL(fTien_ThucChi, 0))/@DVT,
					0,
					0
				FROM BH_QTC_Nam_KPK_ChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND  (sXauNoiMa = '9050010')
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			);
			SELECT * from #result

		END
END
GO
