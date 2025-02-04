/****** Object:  StoredProcedure [dbo].[sp_bh_report_tong_hop_quyet_toan_thu_chi_bhxh_bhyt_bhtn]    Script Date: 2/5/2024 10:53:46 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_report_tong_hop_quyet_toan_thu_chi_bhxh_bhyt_bhtn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_report_tong_hop_quyet_toan_thu_chi_bhxh_bhyt_bhtn]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_report_tong_hop_quyet_toan_thu_chi_bhxh_bhyt_bhtn]    Script Date: 2/5/2024 10:53:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_report_tong_hop_quyet_toan_thu_chi_bhxh_bhyt_bhtn]
	@NamLamViec int ,
	@LstMaDonVi nvarchar(max),
	@DVT int
AS
BEGIN
	CREATE TABLE #result(STT nvarchar(50), IIdChungTu uniqueidentifier, IIdParent uniqueidentifier, SNoiDung nvarchar(200), ILevel int, IThuTu int, FSoTien float)
	DECLARE @IIdQTT uniqueidentifier = NewID();
	DECLARE @IIdQTC uniqueidentifier = NewID();
	DECLARE @IIdThuBHYT uniqueidentifier = NewID();
	DECLARE @IIdChiKcbBHYT uniqueidentifier = NewID();

	INSERT INTO #result(STT, IIdChungTu, IIdParent, SNoiDung, ILevel, IThuTu, FSoTien)
	(
		--I Quyết toán thu
		SELECT 'I', @IIdQTT, NULL, N'Quyết toán thu', 1, 1, 0
		
		UNION ALL
		SELECT '1', NEWID(), @IIdQTT, N'Thu bảo hiểm xã hội (Phụ lục II)', 2, 1, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa in (7,8,9,10,12,13,14,15, 19, 20, 21, 22, 24, 25, 26, 27, 29, 30,34, 35, 36, 37, 39, 40, 41, 42, 46, 47, 48, 49, 51, 52, 53, 54, 56, 57)
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '2', NEWID(), @IIdQTT, N'Thu bảo hiểm thất nghiệp (Phụ lục III)', 2, 2, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa in (70, 71, 73, 74,77, 78, 80, 81)
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '3', @IIdThuBHYT, @IIdQTT, N'Thu bảo hiểm y tế', 2, 3, 0

		UNION ALL
		SELECT '-', NEWID(), @IIdThuBHYT, N'BHYT quân nhân (Phụ lục IV)', 3, 1, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa in (133, 134, 135, 136, 138, 139, 140, 141)
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '-', NEWID(), @IIdThuBHYT, N'BHYT người lao động (Phụ lục V)', 3, 2, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa in (95, 96, 98, 99, 102, 103, 105, 106, 110, 111, 113, 114, 117, 118, 120, 121)
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '-', NEWID(), @IIdThuBHYT, N'BHYT thân nhân quân nhân (Phụ lục VI)', 3, 3, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 151
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '-', NEWID(), @IIdThuBHYT, N'BHYT thân nhân CN', 3, 4, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 155
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '-', NEWID(), @IIdThuBHYT, N'BHYT học sinh', 3, 5, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 163
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '-', NEWID(), @IIdThuBHYT, N'BHYT lưu học sinh (Phụ lục VII)', 3, 6, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 167
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '-', NEWID(), @IIdThuBHYT, N'BHYT HV QS xã phường (Phụ lục VII)', 3, 7, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 159
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '-', NEWID(), @IIdThuBHYT, N'BHYT SQ dự bị (Phụ lục VII)', 3, 8, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 171
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		--II Quyết toán chi
		UNION ALL
		SELECT 'II', @IIdQTC, NULL, N'Quyết toán chi', 1, 2, 0

		UNION ALL
		SELECT '1', NEWID(), @IIdQTC, N'Chi các chế độ BHXH (Phụ lục VIII)', 2, 1, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa in (183, 184, 185, 186, 187, 188, 189, 190, 193, 194, 195, 196, 197, 198, 199, 200)
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '2', NEWID(), @IIdQTC, N'Chi KP quản lý BHXH', 2, 2, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 252
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '3', NEWID(), @IIdQTC, N'Chi mua sắm TTB y tế (Phụ lục X)', 2, 3, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 231
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '4', @IIdChiKcbBHYT, @IIdQTC, N'Chi KCB BHYT', 2, 4, 0

		UNION ALL
		SELECT '-', NEWID(), @IIdChiKcbBHYT, N'Chi KCB cho quân nhân tại TS-DK (Phụ lục XI)', 3, 1, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 238
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '-', NEWID(), @IIdChiKcbBHYT, N'Chi KCB tại quân y đơn vị (Phụ lục XII)', 3, 2, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 223
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '-', NEWID(), @IIdChiKcbBHYT, N'Chi kinh phí CSSK BĐ người lao động (Phụ lục XIII)', 3, 3, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 209
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			AND dv.iTrangThai = 1
			AND dv.iKhoi = 2 --Khối dự toán

		UNION ALL
		SELECT '-', NEWID(), @IIdChiKcbBHYT, N'Chi kinh phí CSSK BĐ HSSV (Phụ lục XIV)', 3, 4, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 215
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			AND dv.iTrangThai = 1
			AND dv.iKhoi = 2 --Khối dự toán

		UNION ALL
		SELECT '-', NEWID(), @IIdChiKcbBHYT, N'Chi KCB tại các cơ sở y tế (Phụ lục XV)', 3, 5, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 260
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			AND dv.iTrangThai = 1
			AND dv.iKhoi = 2 --Khối dự toán
	)

	SELECT STT, IIdChungTu, IIdParent, SNoiDung, ILevel, IThuTu, FSoTien/@DVT FSoTien from #result

	DROP TABLE #result;

END
GO



/****** Object:  StoredProcedure [dbo].[sp_bh_report_tong_hop_du_toan_thu_chi_bhxh_bhyt_bhtn]    Script Date: 2/5/2024 5:11:23 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_report_tong_hop_du_toan_thu_chi_bhxh_bhyt_bhtn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_report_tong_hop_du_toan_thu_chi_bhxh_bhyt_bhtn]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_report_tong_hop_du_toan_thu_chi_bhxh_bhyt_bhtn]    Script Date: 2/5/2024 5:11:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_report_tong_hop_du_toan_thu_chi_bhxh_bhyt_bhtn]
	@NamLamViec int ,
	@LstMaDonVi nvarchar(max),
	@SoQuyetDinh nvarchar(200),
	@NgayQuyetDinh nvarchar(200),
	@DVT int
AS
BEGIN
	CREATE TABLE #result(STT nvarchar(50), IIdChungTu uniqueidentifier, IIdParent uniqueidentifier, SNoiDung nvarchar(200), ILevel int, IThuTu int, FSoTien float)
	DECLARE @IIdDTT uniqueidentifier = NewID();
	DECLARE @IIdDTC uniqueidentifier = NewID();
	DECLARE @IIdThuBHYTTN uniqueidentifier = NewID();

	INSERT INTO #result(STT, IIdChungTu, IIdParent, SNoiDung, ILevel, IThuTu, FSoTien)
	(
		--A Dự toán thu
		SELECT 'A', @IIdDTT, NULL, N'Dự toán thu', 1, 1, 0
		
		UNION ALL
		SELECT '1', NEWID(), @IIdDTT, N'Dự toán thu BHXH', 2, 1, SUM(ISNULL(ctct.fThuBHXH, 0)) FSoTien
		FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
		join BH_DTT_BHXH_PhanBo_ChungTu ct on ctct.iID_DTT_BHXH_ChungTu = ct.iID_DTT_BHXH_PhanBo_ChungTu
		WHERE ctct.iNamLamViec = @NamLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '2', NEWID(), @IIdDTT, N'Dự toán thu BHTN', 2, 2, SUM(ISNULL(ctct.fThuBHTN, 0)) FSoTien
		FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
		join BH_DTT_BHXH_PhanBo_ChungTu ct on ctct.iID_DTT_BHXH_ChungTu = ct.iID_DTT_BHXH_PhanBo_ChungTu
		WHERE ctct.iNamLamViec = @NamLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '3', NEWID(), @IIdDTT, N'Dự toán thu BHYT quân nhân', 2, 3, SUM(ISNULL(ctct.fThuBHYT, 0)) FSoTien
		FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
		join BH_DTT_BHXH_PhanBo_ChungTu ct on ctct.iID_DTT_BHXH_ChungTu = ct.iID_DTT_BHXH_PhanBo_ChungTu
		WHERE ctct.iNamLamViec = @NamLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			AND (ctct.sXauNoiMa like '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%')

		UNION ALL
		SELECT '4', NEWID(), @IIdDTT, N'Dự toán thu BHYT người lao động', 2, 4, SUM(ISNULL(ctct.fThuBHYT, 0)) FSoTien
		FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
		join BH_DTT_BHXH_PhanBo_ChungTu ct on ctct.iID_DTT_BHXH_ChungTu = ct.iID_DTT_BHXH_PhanBo_ChungTu
		WHERE ctct.iNamLamViec = @NamLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			AND (ctct.sXauNoiMa like '9020001-010-011-0002%' OR ctct.sXauNoiMa like '9020002-010-011-0002%')

		UNION ALL
		SELECT '5', @IIdThuBHYTTN, @IIdDTT, N'Dự toán thu BHYT TN', 2, 5, 0 FSoTien

		UNION ALL
		SELECT '-', NEWID(), @IIdThuBHYTTN, N'Thân nhân quân nhân', 3, 1, SUM(ISNULL(ctct.fDuToan, 0)) FSoTien
		FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
		join BH_DTTM_BHYT_ThanNhan_PhanBo ct on ctct.iID_DTTM_BHYT_ThanNhan_PhanBo = ct.iID_DTTM_BHYT_ThanNhan_PhanBo
		WHERE ctct.iNamLamViec = @NamLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			AND (ctct.sXauNoiMa like '9030001-010-011-0001%' OR ctct.sXauNoiMa like '9030001-010-011-0002%')

		UNION ALL
		SELECT '-', NEWID(), @IIdThuBHYTTN, N'Thân nhân người lao động', 3, 1, SUM(ISNULL(ctct.fDuToan, 0)) FSoTien
		FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
		join BH_DTTM_BHYT_ThanNhan_PhanBo ct on ctct.iID_DTTM_BHYT_ThanNhan_PhanBo = ct.iID_DTTM_BHYT_ThanNhan_PhanBo
		WHERE ctct.iNamLamViec = @NamLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			AND (ctct.sXauNoiMa like '9030002-010-011-0000%' OR ctct.sXauNoiMa like '9030002-010-011-0001%')


		--B Dự toán chi
		UNION ALL
		SELECT 'B', @IIdDTC, NULL, N'Dự toán chi', 1, 2, 0

		UNION ALL
		SELECT '1', NEWID(), @IIdDTC, N'Dự toán chi các chế độ BHXH', 2, 1, SUM(ISNULL(ctct.fTienTuChi, 0)) FSoTien
		FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
		join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
		WHERE ctct.iNamLamViec = @NamLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			AND ct.sMaLoaiChi = 1

		UNION ALL
		SELECT '2', NEWID(), @IIdDTC, N'Dự toán chi kinh phí quản lý BHXH', 2, 2, SUM(ISNULL(ctct.fTienTuChi, 0)) FSoTien
		FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
		join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
		WHERE ctct.iNamLamViec = @NamLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			AND ct.sMaLoaiChi = 2

		UNION ALL
		SELECT '3', NEWID(), @IIdDTC, N'Dự toán chi kinh phí KCB tại quân y đơn vị', 2, 3, SUM(ISNULL(ctct.fTienTuChi, 0)) FSoTien
		FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
		join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
		WHERE ctct.iNamLamViec = @NamLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			AND ct.sMaLoaiChi = 3

		UNION ALL
		SELECT '4', NEWID(), @IIdDTC, N'Dự toán chi KCB tại Trường Sa - DK', 2, 4, SUM(ISNULL(ctct.fTienTuChi, 0)) FSoTien
		FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
		join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
		WHERE ctct.iNamLamViec = @NamLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			AND ct.sMaLoaiChi = 4
			)

	SELECT STT, IIdChungTu, IIdParent, SNoiDung, ILevel, IThuTu, FSoTien/@DVT FSoTien from #result

	DROP TABLE #result;

END
GO
