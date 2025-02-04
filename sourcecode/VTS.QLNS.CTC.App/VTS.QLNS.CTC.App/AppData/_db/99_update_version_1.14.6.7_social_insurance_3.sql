/****** Object:  StoredProcedure [dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhyt_quannhan]    Script Date: 7/15/2024 4:48:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhyt_quannhan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhyt_quannhan]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhxh_bhtn]    Script Date: 7/15/2024 4:48:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhxh_bhtn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhxh_bhtn]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_report_tong_hop_quyet_toan_thu_chi_bhxh_bhyt_bhtn]    Script Date: 7/15/2024 4:48:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_report_tong_hop_quyet_toan_thu_chi_bhxh_bhyt_bhtn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_report_tong_hop_quyet_toan_thu_chi_bhxh_bhyt_bhtn]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_report_tham_dinh_quyet_toan_tong_hop_thu_chi]    Script Date: 7/15/2024 4:48:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_report_tham_dinh_quyet_toan_tong_hop_thu_chi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_report_tham_dinh_quyet_toan_tong_hop_thu_chi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_report_tham_dinh_quyet_toan_tong_hop_thu_chi]    Script Date: 7/15/2024 4:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_report_tham_dinh_quyet_toan_tong_hop_thu_chi]
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
			INSERT INTO #result(STT,IIdChungTu, IIdParent, SNoiDung, ILevel, IThuTu, ILoaiChi, FDuToan, FHachToan, IKinhPhiKCB)
			--<DATA INSERT>--
			(SELECT '1',NEWID(), NULL, N'Thu Bảo hiểm xã hội', 1, 1, @Type,
				SUM( CASE WHEN iMa in (7, 8, 9, 10, 12, 13, 14, 15, 19, 20, 21, 22, 24, 25, 26, 27, 29, 30, 259,261, 262, 264, 265, 267, 268) THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				SUM( CASE WHEN iMa in (34, 35, 36, 37, 39, 40, 41, 42, 46, 47, 48, 49, 51, 52, 53, 54, 56, 57, 260, 269, 270) THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND iMa in (7, 8, 9, 10, 12, 13, 14, 15, 19, 20, 21, 22, 24, 25, 26, 27, 29, 30, 34, 35, 36, 37, 39, 40, 41, 42, 46, 47, 48, 49, 51, 52, 53, 54, 56, 57,259,260)
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '2',NEWID(), NULL, N'Thu Bảo hiểm thất nghiệp', 1, 2, @Type,
				SUM( CASE WHEN iMa in (70, 71, 73, 74) THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				SUM( CASE WHEN iMa in (77, 78, 80, 81) THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND iMa in (70, 71, 73, 74, 77, 78, 80, 81)
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '3', @IIdThuBHYT, NULL, N'Thu Bảo hiểm y tế', 1, 3, @Type,
				0,
				0,
				0
			UNION ALL
			SELECT '3.1',NEWID(), @IIdThuBHYT, N'Thu BHYT quân nhân', 2, 1, @Type,
				SUM( CASE WHEN iMa in (133, 134, 135, 136,272, 273) THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				SUM( CASE WHEN iMa in (138, 139, 140, 141) THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND iMa in (133, 134, 135, 136, 138, 139, 140, 141)
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '3.2',NEWID(), @IIdThuBHYT, N'Thu BHYT người lao động', 2, 2, @Type,
				SUM( CASE WHEN iMa in (95, 96, 98, 99, 102, 103, 105, 106) THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				SUM( CASE WHEN iMa in (110, 111, 113, 114, 117, 118, 120, 121) THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND iMa in (95, 96, 98, 99, 102, 103, 105, 106, 110, 111, 113, 114, 117, 118, 120, 121)
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '3.3',NEWID(), @IIdThuBHYT, N'Thu BHYT thân nhân quân nhân', 2, 3, @Type,
				SUM( CASE WHEN iMa = 151 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
				SUM( CASE WHEN iMa = 151 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
				0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 151
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL
			SELECT '3.4',NEWID(), @IIdThuBHYT, N'Thu BHYT thân nhân CNVCQP', 2, 4, @Type,
				SUM( CASE WHEN iMa = 155 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
				SUM( CASE WHEN iMa = 155 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
				0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 155
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL
			SELECT '3.5',NEWID(), @IIdThuBHYT, N'Thu BHYT HVQS Xã phường', 2, 5, @Type,
				SUM( CASE WHEN iMa = 159 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
				SUM( CASE WHEN iMa = 159 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
				0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec
					AND ctct.iMa = 159
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL
			SELECT '3.6',NEWID(), @IIdThuBHYT, N'Thu BHYT SQDB', 2, 6, @Type,
				SUM( CASE WHEN iMa = 171 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
				SUM( CASE WHEN iMa = 171 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
				0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec
					AND ctct.iMa = 171
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL
			SELECT '3.7',NEWID(), @IIdThuBHYT, N'Thu BHYT HS,SV', 2, 7, @Type,
				SUM( CASE WHEN iMa = 163 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
				SUM( CASE WHEN iMa = 163 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
				0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec
					AND ctct.iMa = 163
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL
			SELECT '3.8',NEWID(), @IIdThuBHYT, N'Thu BHYT Lưu HS', 2, 8, @Type,
				SUM( CASE WHEN iMa = 167 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
				SUM( CASE WHEN iMa = 167 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
				0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec
					AND ctct.iMa = 167
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			);
			SELECT STT, IIdChungTu, IIdParent, SNoiDung, ILevel, IThuTu, ILoaiChi, FDuToan/@DVT FDuToan, FHachToan/@DVT FHachToan, IKinhPhiKCB from #result

		END
	ELSE
		BEGIN
			---Phần chi---
			DECLARE @IIdChiCDBHYT uniqueidentifier = NewID();
			DECLARE @IIdKinhPhiKCB uniqueidentifier = NewID();
			DECLARE @IIdChiTieuKPQL uniqueidentifier = NewID();
			DECLARE @IIdChiTieuKPKCB uniqueidentifier = NewID();
			DECLARE @IIdChiTieuKPKCBTSDK uniqueidentifier = NewID();
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
				SUM( CASE WHEN iMa = 183 THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				SUM( CASE WHEN iMa = 193 THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND iMa in (193, 183)
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '2',NEWID(), @IIdChiCDBHYT, N'Trợ cấp thai sản', 2, 2, @Type,
				SUM( CASE WHEN iMa = 184 THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				SUM( CASE WHEN iMa = 194 THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND iMa in (184, 194)
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '3',NEWID(), @IIdChiCDBHYT, N'Trợ cấp tai nạn lao động, BNN', 2, 3, @Type,
				SUM( CASE WHEN iMa = 185 THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				SUM( CASE WHEN iMa = 195 THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND iMa in (185, 195)
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '4',NEWID(), @IIdChiCDBHYT, N'Trợ cấp hưu trí', 2, 4, @Type,
				SUM( CASE WHEN iMa = 186 THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				SUM( CASE WHEN iMa = 196 THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND iMa in (186, 196)
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '5',NEWID(), @IIdChiCDBHYT, N'Trợ cấp phục viên', 2, 5, @Type,
				SUM( CASE WHEN iMa = 187 THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				SUM( CASE WHEN iMa = 197 THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND iMa in (187, 197)
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '6',NEWID(), @IIdChiCDBHYT, N'Trợ cấp xuất ngũ', 2, 6, @Type,
				SUM( CASE WHEN iMa = 188 THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				SUM( CASE WHEN iMa = 198 THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND iMa in (188, 198)
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '7',NEWID(), @IIdChiCDBHYT, N'Trợ cấp thôi việc', 2, 7, @Type,
					SUM( CASE WHEN iMa = 189 THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 199 THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
					0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND iMa in (189, 199)
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '8',NEWID(), @IIdChiCDBHYT, N'Trợ cấp tử tuất', 2, 8, @Type,
					SUM( CASE WHEN iMa = 190 THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 200 THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
					0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND iMa in (190, 200)
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL ---II---
			SELECT 'II',NEWID(), NULL, N'Kinh phí quản lý BHXH, BHYT', 1, 2, @Type,
					SUM( CASE WHEN iMa = 252 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 252 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					2
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 252
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL

			SELECT '1', @IIdChiTieuKPQL, @IIdKinhPhiKCB, N'Dự toán', 2, 1, @Type,
				0,
				0,
				1
			UNION ALL
			SELECT '-',NEWID(), @IIdChiTieuKPQL, N'Năm trước chuyển sang', 3, 1, @Type,
					SUM( CASE WHEN iMa = 247 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 247 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 247
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL
			SELECT '-',NEWID(), @IIdChiTieuKPQL, N'Năm nay', 3, 2, @Type,
					SUM( CASE WHEN iMa = 248 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 248 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 248
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1

			UNION ALL
			SELECT '2',NEWID(), @IIdKinhPhiKCB, N'Số quyết toán', 2, 2, @Type,
					SUM( CASE WHEN iMa = 252 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 252 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					2
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 252
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1

			UNION ALL
			SELECT '3',NEWID(), @IIdKinhPhiKCB, N'Dự toán chuyển năm sau', 2, 2, @Type,
					SUM( CASE WHEN iMa = 254 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 254 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					2
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 254
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL
			SELECT 'III',@IIdKinhPhiKCB, NULL, N'Kinh phí KCB tại quân y đơn vị', 1, 3, @Type,
					SUM( CASE WHEN iMa = 223 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 223 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					2
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 223
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL
			SELECT '1',@IIdChiTieuKPKCB, @IIdKinhPhiKCB, N'Dự toán', 2, 1, @Type,
				0,
				0,
				1
			UNION ALL
			SELECT '-',NEWID(), @IIdChiTieuKPKCB, N'Năm trước chuyển sang', 3, 1, @Type,
					SUM( CASE WHEN iMa = 220 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 220 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 220
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL
			SELECT '-',NEWID(), @IIdChiTieuKPKCB, N'Năm nay', 3, 2, @Type,
					SUM( CASE WHEN iMa = 221 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 221 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 221
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL
			SELECT '2',NEWID(), @IIdKinhPhiKCB, N'Số quyết toán', 2, 2, @Type,
					SUM( CASE WHEN iMa = 223 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 223 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					2
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 223
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL
			SELECT '3',NEWID(), @IIdKinhPhiKCB, N'Dự toán chuyển năm sau', 2, 3, @Type,
					SUM( CASE WHEN iMa = 225 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 225 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					3
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 225
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL --IV--
			SELECT 'IV',NEWID(), NULL, N'Kinh phí KCB tại trường sa - DK', 1, 4, @Type,
					SUM( CASE WHEN iMa = 238 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 238 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					2
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 238
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL
			SELECT '1',@IIdChiTieuKPKCBTSDK, @IIdKinhPhiKCB, N'Dự toán', 2, 1, @Type,
					0,
					0,
					1
			UNION ALL
			SELECT '-',NEWID(), @IIdChiTieuKPKCBTSDK, N'Năm trước chuyển sang', 3, 1, @Type,
					SUM( CASE WHEN iMa = 235 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 235 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 235
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL
			SELECT '-',NEWID(), @IIdChiTieuKPKCBTSDK, N'Năm nay', 3, 2, @Type,
					SUM( CASE WHEN iMa = 236 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 236 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 236
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL
			SELECT '2',NEWID(), @IIdKinhPhiKCB, N'Số quyết toán', 2, 2, @Type,
					SUM( CASE WHEN iMa = 238 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 238 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					2
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 238
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL
			SELECT '3',NEWID(), @IIdKinhPhiKCB, N'Dự toán chuyển năm sau', 2, 4, @Type,
					SUM( CASE WHEN iMa = 240 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 240 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					3
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 240
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL --V--
			SELECT 'V',NEWID(), NULL, N'Chi phí mua sắm thiết bị y tế', 1, 5, @Type,
					SUM( CASE WHEN iMa = 231 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 231 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 231
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1

			UNION ALL --VI--
			SELECT 'VI',@IIdChiCSK, NULL, N'Chi chăm sóc sức khỏe ban đầu', 1, 6, @Type,
					SUM( CASE WHEN iMa in (209, 215) AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa in (209, 215) AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa in (209, 215)
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			
			UNION ALL --VII-- 
			SELECT 'VII',NEWID(), NULL, N'Chi hỗ trợ người lao động tham gia BHTN', 1, 7, @Type,
					SUM( CASE WHEN iMa = 243 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 243 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 243
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			);
			SELECT STT, IIdChungTu, IIdParent, SNoiDung, ILevel, IThuTu, ILoaiChi, FDuToan/@DVT FDuToan, FHachToan/@DVT FHachToan, IKinhPhiKCB from #result
			--SELECT * from #result

		END
		DROP TABLE #result;
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_report_tong_hop_quyet_toan_thu_chi_bhxh_bhyt_bhtn]    Script Date: 7/15/2024 4:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_report_tong_hop_quyet_toan_thu_chi_bhxh_bhyt_bhtn]
	@NamLamViec int ,
	@LstMaDonVi nvarchar(max),
	@DVT int,
	@IsTongHop bit
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
			AND ctct.iMa in (7,8,9,10,12,13,14,15, 19, 20, 21, 22, 24, 25, 26, 27, 29, 30,34, 35, 36, 37, 39, 40, 41, 42, 46, 47, 48, 49, 51, 52, 53, 54, 56, 57,  261, 262, 264, 265, 267, 268, 269, 270)
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
			AND ctct.iMa in (133, 134, 135, 136, 138, 139, 140, 141, 272, 273)
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
		SELECT '-', NEWID(), @IIdChiKcbBHYT, N'Chi KCB tại các cơ sở y tế (Phụ lục XV)', 3, 5, 
		CASE WHEN @IsTongHop = 1 THEN SUM(ISNULL(ctct.fQuyetToanQuyNay, 0))
		ELSE 0 END fQuyetToanQuyNay
		FROM BH_QTC_CapKinhPhi_KCB_ChiTiet ctct
		JOIN BH_QTC_CapKinhPhi_KCB ct ON ct.iID_ChungTu = ctct.iID_ChungTu
		WHERE ct.iNamLamViec = @NamLamViec 
			AND ct.iQuy = @NamLamViec
	)

	SELECT STT, IIdChungTu, IIdParent, SNoiDung, ILevel, IThuTu, FSoTien/@DVT FSoTien from #result

	DROP TABLE #result;

END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhxh_bhtn]    Script Date: 7/15/2024 4:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhxh_bhtn]
	@NamLamViec int,
	@LstSelectedUnit ntext,
	@Dvt int,
	@IsBHXH bit
AS
BEGIN
		declare @DTBHXHNLDDONG table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), BhxhNldDongDuToan float);
		declare @DTBHXHNLDLDDONG table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), BhxhNldLdDongDuToan float);
		declare @HTBHXHNLDDONG table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), BhxhNldDongHachToan float);
		declare @HTBHXHNLDLDDONG table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), BhxhNldLdDongHachToan float);
		
		INSERT INTO @DTBHXHNLDDONG (sTenDonVI, idDonVi, BhxhNldDongDuToan)
			SELECT dt_dv.sTenDonVi, dt_dv.id idDonVi, SUM(IsNull(A.ThuBHXHNLDDong, 0))
			FROM(
						SELECT iID_MaDonVi AS id, sTenDonVi, iLoai
						FROM DonVi
						WHERE iTrangThai = 1
						AND iNamLamViec = @NamLamViec
						AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
				) AS dt_dv LEFT JOIN
			  (
				SELECT ct.iID_MaDonVi, IsNull(ctct.fSoThamDinh, 0) ThuBHXHNLDDong
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN BH_ThamDinhQuyetToan_ChungTu ct ON ct.iID_BH_TDQT_ChungTu = ctct.iID_BH_TDQT_ChungTu AND ctct.iNamLamViec = @NamLamViec
				WHERE ct.iNamLamViec = @NamLamViec AND
				((@IsBHXH = 1 AND ctct.iMa IN (7,8,9,10,12,13,14,15, 261, 262, 264, 265)) OR (@IsBHXH = 0 AND ctct.iMa IN (70,71)))
			   ) AS A 
			   ON A.iID_MaDonVi=dt_dv.id
			GROUP BY dt_dv.sTenDonVi, dt_dv.id;

		INSERT INTO @DTBHXHNLDLDDONG (sTenDonVI, idDonVi, BhxhNldLdDongDuToan)
			SELECT dt_dv.sTenDonVi, dt_dv.id idDonVi, SUM(IsNull(A.ThuBHXHNLDDong, 0))
			FROM (
						SELECT iID_MaDonVi AS id, sTenDonVi, iLoai
						FROM DonVi
						WHERE iTrangThai = 1
						AND iNamLamViec = @NamLamViec
						AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
				) AS dt_dv LEFT JOIN
			  (
				SELECT ct.iID_MaDonVi, IsNull(ctct.fSoThamDinh, 0) ThuBHXHNLDDong
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN BH_ThamDinhQuyetToan_ChungTu ct ON ct.iID_BH_TDQT_ChungTu = ctct.iID_BH_TDQT_ChungTu AND ctct.iNamLamViec = @NamLamViec
				WHERE ct.iNamLamViec = @NamLamViec AND 
				((@IsBHXH = 1 AND ctct.iMa IN (19,20,21,22,24,25,26,27,29,30,267, 268)) OR (@IsBHXH = 0 AND ctct.iMa IN (7,73,74)))
			   ) AS A 
			   ON A.iID_MaDonVi=dt_dv.id
			GROUP BY dt_dv.sTenDonVi, dt_dv.id;

		INSERT INTO @HTBHXHNLDDONG (sTenDonVI, idDonVi, BhxhNldDongHachToan)
			SELECT dt_dv.sTenDonVi, dt_dv.id idDonVi, SUM(IsNull(A.ThuBHXHNLDDong, 0))
			FROM  (
						SELECT iID_MaDonVi AS id, sTenDonVi, iLoai
						FROM DonVi
						WHERE iTrangThai = 1
						AND iNamLamViec = @NamLamViec
						AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
				) AS dt_dv LEFT JOIN
			  (
				SELECT ct.iID_MaDonVi, IsNull(ctct.fSoThamDinh, 0) ThuBHXHNLDDong
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN BH_ThamDinhQuyetToan_ChungTu ct ON ct.iID_BH_TDQT_ChungTu = ctct.iID_BH_TDQT_ChungTu AND ctct.iNamLamViec = @NamLamViec
				WHERE ct.iNamLamViec = @NamLamViec AND
				((@IsBHXH = 1 AND  ctct.iMa IN (34,35,36,37,39,40,41,42, 269, 270)) OR (@IsBHXH = 0 AND ctct.iMa IN (77,78)))
			   ) AS A 
			   ON A.iID_MaDonVi=dt_dv.id
			GROUP BY dt_dv.sTenDonVi, dt_dv.id;

		INSERT INTO @HTBHXHNLDLDDONG (sTenDonVI, idDonVi, BhxhNldLdDongHachToan)
			SELECT dt_dv.sTenDonVi, dt_dv.id idDonVi, SUM(IsNull(A.ThuBHXHNLDDong, 0))
			FROM
			 (
						SELECT iID_MaDonVi AS id, sTenDonVi, iLoai
						FROM DonVi
						WHERE iTrangThai = 1
						AND iNamLamViec = @NamLamViec
						AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
				) AS dt_dv  LEFT JOIN
			  (
				SELECT ct.iID_MaDonVi, IsNull(ctct.fSoThamDinh, 0) ThuBHXHNLDDong
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN BH_ThamDinhQuyetToan_ChungTu ct ON ct.iID_BH_TDQT_ChungTu = ctct.iID_BH_TDQT_ChungTu AND ctct.iNamLamViec = @NamLamViec
				WHERE ct.iNamLamViec = @NamLamViec AND 
				((@IsBHXH = 1 AND ctct.iMa IN (46,47,48,49,51,52,53,54,56,57)) OR (@IsBHXH = 0 AND ctct.iMa IN (80,81)))
			   ) AS A 
			   ON A.iID_MaDonVi=dt_dv.id
			GROUP BY dt_dv.sTenDonVi, dt_dv.id;

		SELECT dt.idDonVi, dt.sTenDonVI, 
		Case when @IsBHXH = 1 then IsNull(dt.BhxhNldDongDuToan, 0)/@Dvt end as fBhxhNldDongDuToan,
		Case when @IsBHXH = 0 then IsNull(dt.BhxhNldDongDuToan, 0)/@Dvt end as fBhtnNldDongDuToan,
		Case when @IsBHXH = 1 then IsNull(dtld.BhxhNldLdDongDuToan, 0)/@Dvt end as fBhxhNsddDongDuToan, 
		Case when @IsBHXH = 0 then IsNull(dtld.BhxhNldLdDongDuToan, 0)/@Dvt end as fBhtnNsddDongDuToan,
		Case when @IsBHXH = 1 then (IsNull(dt.BhxhNldDongDuToan, 0)/@Dvt + IsNull(dtld.BhxhNldLdDongDuToan, 0)/@Dvt) end as fBHXHTongCongDuToan,
		Case when @IsBHXH = 0 then (IsNull(dt.BhxhNldDongDuToan, 0)/@Dvt + IsNull(dtld.BhxhNldLdDongDuToan, 0)/@Dvt) end as fBHTNTongCongDuToan,
		Case when @IsBHXH = 1 then IsNull(ht.BhxhNldDongHachToan, 0)/@Dvt end as fBhxhNldDongHachToan,
		Case when @IsBHXH = 0 then IsNull(ht.BhxhNldDongHachToan, 0)/@Dvt end as fBhtnNldDongHachToan, 
		Case when @IsBHXH = 1 then IsNull(htld.BhxhNldLdDongHachToan, 0)/@Dvt end as fBhxhNsddDongHachToan,
		Case when @IsBHXH = 0 then IsNull(htld.BhxhNldLdDongHachToan, 0)/@Dvt end as fBhtnNsddDongHachToan,
		Case when @IsBHXH = 1 then (IsNull(ht.BhxhNldDongHachToan, 0)/@Dvt + IsNull(htld.BhxhNldLdDongHachToan, 0)/@Dvt) end as fBHXHTongCongHachToan,
		Case when @IsBHXH = 0 then (IsNull(ht.BhxhNldDongHachToan, 0)/@Dvt + IsNull(htld.BhxhNldLdDongHachToan, 0)/@Dvt) end as fBHTNTongCongHachToan
		FROM @DTBHXHNLDDONG dt
		LEFT JOIN @DTBHXHNLDLDDONG dtld ON dt.idDonVi = dtld.idDonVi
		LEFT JOIN @HTBHXHNLDDONG ht ON dt.idDonVi = ht.idDonVi
		LEFT JOIN @HTBHXHNLDLDDONG htld ON dt.idDonVi = htld.idDonVi
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhyt_quannhan]    Script Date: 7/15/2024 4:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhyt_quannhan]
	@NamLamViec int,
	@LstSelectedUnit ntext,
	@Dvt int
AS
BEGIN
		declare @DTBHYTNLDDONG table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), BhytNldDongDuToan float);
		declare @HTBHYTNLDDONG table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), BhytNldDongHachToan float);
		
		INSERT INTO @DTBHYTNLDDONG (sTenDonVI, idDonVi, BhytNldDongDuToan)
			SELECT dt_dv.sTenDonVi, dt_dv.id idDonVi, SUM(IsNull(A.ThuBHXHNLDDong, 0))
			FROM
			 (
						SELECT iID_MaDonVi AS id, sTenDonVi, iLoai
						FROM DonVi
						WHERE iTrangThai = 1
						AND iNamLamViec = @NamLamViec
						AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
				) AS dt_dv
				LEFT JOIN
			  (
				SELECT ct.iID_MaDonVi, IsNull(ctct.fSoThamDinh, 0) ThuBHXHNLDDong
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN BH_ThamDinhQuyetToan_ChungTu ct ON ct.iID_BH_TDQT_ChungTu = ctct.iID_BH_TDQT_ChungTu AND ctct.iNamLamViec = @NamLamViec
				WHERE ct.iNamLamViec = @NamLamViec AND ctct.iMa IN (133, 134, 135, 136, 272, 273)
			   ) AS A 
			   ON A.iID_MaDonVi=dt_dv.id
			GROUP BY dt_dv.sTenDonVi, dt_dv.id;

		INSERT INTO @HTBHYTNLDDONG (sTenDonVI, idDonVi, BhytNldDongHachToan)
			SELECT dt_dv.sTenDonVi, dt_dv.id idDonVi, SUM(IsNull(A.ThuBHXHNLDDong, 0))
			FROM
			(
						SELECT iID_MaDonVi AS id, sTenDonVi, iLoai
						FROM DonVi
						WHERE iTrangThai = 1
						AND iNamLamViec = @NamLamViec
						AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
				) AS dt_dv
				LEFT JOIN
			  (
				SELECT ct.iID_MaDonVi, IsNull(ctct.fSoThamDinh, 0) ThuBHXHNLDDong
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN BH_ThamDinhQuyetToan_ChungTu ct ON ct.iID_BH_TDQT_ChungTu = ctct.iID_BH_TDQT_ChungTu AND ctct.iNamLamViec = @NamLamViec
				WHERE ct.iNamLamViec = @NamLamViec AND ctct.iMa IN (138, 139, 140, 141)
			   ) AS A 
			    ON A.iID_MaDonVi=dt_dv.id
			GROUP BY dt_dv.sTenDonVi, dt_dv.id;

		SELECT dt.idDonVi, dt.sTenDonVI, 
		IsNull(dt.BhytNldDongDuToan, 0)/@Dvt fBHYTTongCongDuToan,
		IsNull(ht.BhytNldDongHachToan, 0)/@Dvt fBHYTTongCongHachToan
		FROM @DTBHYTNLDDONG dt
		LEFT JOIN @HTBHYTNLDDONG ht ON dt.idDonVi = ht.idDonVi
END
;
;
;
GO
UPDATE BH_DM_ThamDinhQuyetToan
SET iLock = 1;

/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_tnld]    Script Date: 7/17/2024 10:04:46 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_luong_tro_cap_tnld]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_tnld]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh]    Script Date: 7/17/2024 10:04:46 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh]    Script Date: 7/17/2024 10:04:46 AM ******/
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
	@sQuarter nvarchar(50) ,
	@Donvi  nvarchar(50),
	@NamLamViec int 
AS
BEGIN
	DECLARE @STongHop nvarchar(max);	
	DECLARE @XauNoiMaREPLACE nvarchar(max);
	set @XauNoiMaREPLACE =case when @sXauNoiMa like '9010001%' 
									then REPLACE(@sXauNoiMa,'9010001-' , '') 
									else REPLACE(@sXauNoiMa,'9010002-' , '') end  

	SELECT sMaCheDo 
			, bDisplay
			, case when sXauNoiMaMlnsBHXH like '9010001%' 
			then REPLACE(sXauNoiMaMlnsBHXH,'9010001-' , '')
			else REPLACE(sXauNoiMaMlnsBHXH,'9010002-' , '') end  sXauNoiMaMlnsBHXH
				into #tempTL_DM_CheDoBHXH
			FROM TL_DM_CheDoBHXH
			WHERE bDisplay = 1
	select top(1) bangluong.STongHop into #temp1  FROM TL_BangLuong_ThangBHXH bangluongchitiet 
			JOIN  TL_DS_CapNhap_BangLuong bangluong on bangluongchitiet.iID_Parent = bangluong.Id
			where
				bangluongchitiet.iThang IN (SELECT * FROM splitstring(@sQuarter))
					AND bangluongchitiet.sMaCheDo IN (SELECT sMaCheDo 
							FROM #tempTL_DM_CheDoBHXH
							WHERE bDisplay = 1 
								AND sXauNoiMaMlnsBHXH = @XauNoiMaREPLACE )
					AND (
						bangluongchitiet.sMaCB LIKE '1%'  
						OR bangluongchitiet.sMaCB LIKE '2%'  
						OR bangluongchitiet.sMaCB = '3.1'  
						OR bangluongchitiet.sMaCB = '3.2'  
						OR bangluongchitiet.sMaCB = '3.3'
						OR bangluongchitiet.sMaCB = '413' 
						OR bangluongchitiet.sMaCB = '415' 
						OR bangluongchitiet.sMaCB = '423' 
						OR bangluongchitiet.sMaCB = '425' 
						OR bangluongchitiet.sMaCB = '43' 
						OR bangluongchitiet.sMaCB LIKE '0%'
			)
			and bangluong.Ma_CachTL = N'CACH2'
			and bangluong.Ma_CBo = @Donvi
			and bangluong.KhoaBangLuong = 1
			and bangluong.Nam = @NamLamViec
			and bangluong.STongHop is not null

	set @STongHop = (select #temp1.STongHop from #temp1);

	select * into #tblSTongHop from splitstring(@STongHop);

	-- Get bang luong chi tiet 
	select * into #temp2 from TL_BangLuong_ThangBHXH bangluongchitiet  where bangluongchitiet.sMaDonVi in (select * from #tblSTongHop)
	and bangluongchitiet.iThang IN (SELECT * FROM splitstring(@sQuarter))
		AND bangluongchitiet.sMaCheDo IN (SELECT sMaCheDo 
										FROM #tempTL_DM_CheDoBHXH
										WHERE bDisplay = 1 
										AND sXauNoiMaMlnsBHXH = @XauNoiMaREPLACE )
	AND (
		bangluongchitiet.sMaCB LIKE '1%'  
			OR bangluongchitiet.sMaCB LIKE '2%'  
			OR bangluongchitiet.sMaCB = '3.1'  
			OR bangluongchitiet.sMaCB = '3.2'  
			OR bangluongchitiet.sMaCB = '3.3'
			OR bangluongchitiet.sMaCB = '413' 
			OR bangluongchitiet.sMaCB = '415' 
			OR bangluongchitiet.sMaCB = '423' 
			OR bangluongchitiet.sMaCB = '425' 
			OR bangluongchitiet.sMaCB = '43' 
			OR bangluongchitiet.sMaCB LIKE '0%'
	)

	--- Lấy tiền truy lĩnh---
	SELECT tbl.*, t2.fSoNgayTruyLinh, t2.nGiaTriTruyLinh INTO #temp3
	FROM #temp2 tbl
	LEFT JOIN 
		(SELECT tbl.sMaCBo , tbl.sMaDonVi, tbl.iThang, tbl.iNam, SUM(ISNULL(canbo.fSoNgayHuongBHXH, 0)) fSoNgayTruyLinh, SUM(ISNULL(tbl.nGiaTri, 0)) as nGiaTriTruyLinh
		FROM TL_BangLuong_ThangBHXH tbl
		INNER JOIN #temp2 t2 ON tbl.sMaCBo = t2.sMaCBo AND tbl.sMaDonVi = t2.sMaDonVi AND t2.iThang = tbl.iThang AND tbl.iNam = t2.iNam 
		LEFT JOIN TL_CanBo_CheDoBHXH canbo ON tbl.sMaCBo = canbo.sMaCanBo and tbl.sMaCheDo=canbo.sMaCheDo
		WHERE tbl.sMaCheDo IN (SELECT sMaCheDo FROM TL_DM_CheDoBHXH where sLoaiTruyLinh IN (select sMaCheDo from #temp2 ))
		GROUP BY tbl.sMaCBo , tbl.sMaDonVi, tbl.iThang, tbl.iNam
		) as t2 ON  tbl.sMaCBo = t2.sMaCBo AND tbl.sMaDonVi = t2.sMaDonVi AND t2.iThang = tbl.iThang AND tbl.iNam = t2.iNam 
	
		
	

	SELECT 
		@sXauNoiMa as SXauNoiMa,
		sMaHieuCanBo as SMaHieuCanBo,
		sTenCbo as STenCanBo,
		sMaCBo As SMaCanBo,
		sMaCB as SMaCapBac,
		capbac.Note aS STenCapBac,
		bangluongchitiet.sMaDonVi as ID_MaPhanHo,
		dv.Ten_DonVi as STenPhanHo,
		canbo.fSoNgayHuongBHXH as ISoNgayHuong,
		canbo.sSoQuyetDinh as SSoQuyetDinh,
		canbo.dNgayQuyetDinh as DNgayQuyetDinh,
		bangluongchitiet.nGiaTri as FSoTien,
		bangluongchitiet.nGiaTriTruyLinh as FTienTruyLinh,
		bangluongchitiet.fSoNgayTruyLinh as ISoNgayTruyLinh,
		canbo.dTuNgay AS DTuNgay,
		canbo.dDenNgay AS DDenNgay,
		bangluongchitiet.sMaCB
	FROM #temp3 bangluongchitiet 
	LEFT JOIN TL_CanBo_CheDoBHXH canbo ON bangluongchitiet.sMaCBo = canbo.sMaCanBo and bangluongchitiet.sMaCheDo=canbo.sMaCheDo
	LEFT JOIN TL_DM_CapBac capbac ON capbac.Ma_Cb  = bangluongchitiet.sMaCB
	LEFT JOIN TL_DM_DonVi dv On dv.Ma_DonVi = bangluongchitiet.sMaDonVi
	where  (
			bangluongchitiet.sMaCB LIKE '1%'  
			OR bangluongchitiet.sMaCB LIKE '2%'  
			OR bangluongchitiet.sMaCB = '3.1'  
			OR bangluongchitiet.sMaCB = '3.2'  
			OR bangluongchitiet.sMaCB = '3.3'
			OR bangluongchitiet.sMaCB = '413' 
			OR bangluongchitiet.sMaCB = '415' 
			OR bangluongchitiet.sMaCB = '423' 
			OR bangluongchitiet.sMaCB = '425' 
			OR bangluongchitiet.sMaCB = '43' 
			OR bangluongchitiet.sMaCB LIKE '0%'
		)
		and canbo.iNam=@NamLamViec
DROP TABLE #temp3,#temp2,#temp1,#tblSTongHop;
	--	SELECT 
	--	N'9010001-010-011-0001-0001-0001-01-02' as SXauNoiMa,
	--	sMaHieuCanBo as SMaHieuCanBo,
	--	sTenCbo as STenCanBo,
	--	sMaCBo As SMaCanBo,
	--	sMaCB as SMaCapBac,
	--	capbac.Note aS STenCapBac,
	--	bangluong.sMaDonVi as ID_MaPhanHo,
	--	dv.Ten_DonVi as STenPhanHo,
	--	canbo.fSoNgayHuongBHXH as ISoNgayHuong,
	--	canbo.sSoQuyetDinh as SSoQuyetDinh,
	--	canbo.dNgayQuyetDinh as DNgayQuyetDinh,
	--	bangluong.nGiaTri as FSoTien,
	--	canbo.dTuNgay AS DTuNgay,
	--	canbo.dDenNgay AS DDenNgay

	--FROM TL_BangLuong_ThangBHXH bangluong
	--LEFT JOIN TL_CanBo_CheDoBHXH canbo ON bangluong.sMaCBo = canbo.sMaCanBo
	--LEFT JOIN TL_DM_CapBac capbac ON capbac.Ma_Cb  = bangluong.sMaCB
	--LEFT JOIN TL_DM_DonVi dv On dv.Ma_DonVi = bangluong.sMaDonVi
	--WHERE 
	--	bangluong.iThang IN (SELECT * FROM splitstring('1,2,3'))
	--	AND bangluong.sMaCheDo IN (SELECT sMaCheDo FROM TL_DM_CheDoBHXH WHERE bDisplay = 1 AND sXauNoiMaMlnsBHXH = N'9010001-010-011-0001-0001-0001-01-02' )
	--	AND (
	--		bangluong.sMaCB LIKE '1%'  
	--		OR bangluong.sMaCB LIKE '2%'  
	--		OR bangluong.sMaCB LIKE '3.1%'  
	--		OR bangluong.sMaCB LIKE '3.2%'  
	--		OR bangluong.sMaCB LIKE '3.3%'  
	--		OR bangluong.sMaCB LIKE '43%' 
	--		OR bangluong.sMaCB LIKE '0%' 
	--	)

	-- Get data 
	--SELECT 
	--	@sXauNoiMa as SXauNoiMa,
	--	sMaHieuCanBo as SMaHieuCanBo,
	--	sTenCbo as STenCanBo,
	--	sMaCBo As SMaCanBo,
	--	sMaCB as SMaCapBac,
	--	capbac.Note aS STenCapBac,
	--	bangluongchitiet.sMaDonVi as ID_MaPhanHo,
	--	dv.sTenDonVi as STenPhanHo,
	--	canbo.fSoNgayHuongBHXH as ISoNgayHuong,
	--	canbo.sSoQuyetDinh as SSoQuyetDinh,
	--	canbo.dNgayQuyetDinh as DNgayQuyetDinh,
	--	bangluongchitiet.nGiaTri as FSoTien,
	--	canbo.dTuNgay AS DTuNgay,
	--	canbo.dDenNgay AS DDenNgay

	--FROM TL_BangLuong_ThangBHXH bangluongchitiet 
	--LEFT JOIN TL_CanBo_CheDoBHXH canbo ON bangluongchitiet.sMaCBo = canbo.sMaCanBo
	--LEFT JOIN TL_DM_CapBac capbac ON capbac.Ma_Cb  = bangluongchitiet.sMaCB
	--LEFT JOIN DonVi dv On dv.iID_MaDonVi = bangluongchitiet.sMaDonVi
	--JOIN  TL_DS_CapNhap_BangLuong bangluong on bangluongchitiet.iID_Parent = bangluong.Id
	--WHERE 
	--	bangluongchitiet.iThang IN (SELECT * FROM splitstring(@sQuarter))
	--	AND bangluongchitiet.sMaCheDo IN (SELECT sMaCheDo FROM TL_DM_CheDoBHXH WHERE bDisplay = 1 AND sXauNoiMaMlnsBHXH = @sXauNoiMa )
	--	AND (
	--		bangluongchitiet.sMaCB LIKE '1%'  
	--		OR bangluongchitiet.sMaCB LIKE '2%'  
	--		OR bangluongchitiet.sMaCB LIKE '3.1%'  
	--		OR bangluongchitiet.sMaCB LIKE '3.2%'  
	--		OR bangluongchitiet.sMaCB LIKE '3.3%'  
	--		OR bangluongchitiet.sMaCB LIKE '43%' 
	--		OR bangluongchitiet.sMaCB LIKE '0%' 
	--	)
	--	and bangluong.Ma_CachTL = N'CACH2'
	--	and bangluong.Ma_CBo=@Donvi
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_tnld]    Script Date: 7/17/2024 10:04:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_tnld] 
 	@DsMaDonVi nvarchar(100),
	@NamLamViec int ,
	@Thang int ,
	@DonViTinh int 
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD]') AND type in (N'U'))
	drop table TBL_TCTNLD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_DOC]') AND type in (N'U'))
	drop table TBL_TCTNLD_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_SQ]') AND type in (N'U'))
	drop table TBL_TCTNLD_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_QNCN]') AND type in (N'U'))
	drop table TBL_TCTNLD_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_HSQBS]') AND type in (N'U'))
	drop table TBL_TCTNLD_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_VCQP]') AND type in (N'U'))
	drop table TBL_TCTNLD_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_LDHD]') AND type in (N'U'))
	drop table TBL_TCTNLD_LDHD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_RESULT]') AND type in (N'U'))
	drop table TBL_TCTNLD_RESULT;

	DECLARE @MaCheDo nvarchar(1000) = 'CHIGIAMDINH,HOTRO_CDNN,HOTRO_PHONGNGUA,TAINANLD_DUONGSUCPHSK,TAINANLD_TROCAP1LAN,TROCAPCHETDOTNLD,TROCAPPHCN,TROCAPPHUCVU,TROCAPTHEOPHIEUTRUYTRA,TROCAPHANGTHANG,CHIGIAMDINH_TRUYLINH,HOTRO_CDNN_TRUYLINH,HOTRO_PHONGNGUA_TRUYLINH,TAINANLD_DUONGSUCPHSK_TRUYLINH,TAINANLD_TROCAP1LAN_TRUYLINH,TROCAPCHETDOTNLD_TRUYLINH,TROCAPPHCN_TRUYLINH,TROCAPPHUCVU_TRUYLINH,TROCAPHANGTHANG_TRUYLINH,TROCAPTHEOPHIEUTRUYTRA_TRUYLINH'
	--Lay thong tin luong theo tro cap tai nan lao dong
	select * into TBL_TCTNLD from
	(select donvi.Ma_DonVi, donvi.Ten_DonVi, luong.*
	from TL_BangLuong_ThangBHXH luong
	join TL_DM_DonVi donvi on luong.sMaDonVi = donvi.Ma_DonVi
	where 
	luong.sMaDonVi in (SELECT * FROM f_split(@DsMaDonVi))
	and luong.iNam = @NamLamViec
	and luong.iThang = @Thang
	and luong.sMaCheDo in (select * from splitstring(@MaCheDo))) tctnld
	select distinct 
		TBL_TCTNLD.sMaCB,
		TBL_TCTNLD.sMaCBo,
		--TBL_TCTNLD.sMaCheDo,
		TBL_TCTNLD.sTenCbo,
		TBL_TCTNLD.Ma_DonVi,
		TBL_TCTNLD.Ten_DonVi,
		CHIGIAMDINH.nGiaTri fChiGiamDinh,
		TROCAP1LAN.nGiaTri fTroCap1Lan,
		TROCAPTHEOPHIEUTRUYTRA.nGiaTri fTroCapTheoPhieuTruyTra,
		TROCAPHANGTHANG.nGiaTri fTroCapHangThang,
		TROCAPPHCN.nGiaTri fTroCapPHCN,
		HOTROCDNN.nGiaTri fHoTroCdnn,
		HOTROPHONGNGUA.nGiaTri fHoTroPhongNgua,
		TROCAPCHETDOTNLD.nGiaTri fTroCapChetDoTNLD,
		TAINANLD_DUONGSUCPHSK.SoNgayDuongSucTNLD,
		TAINANLD_DUONGSUCPHSK.nGiaTri fDuongSucTNLD,
		chedocha.sSoQuyetDinh,
		chedocha.dNgayQuyetDinh,
		CHIGIAMDINH_TL.nGiaTri fChiGiamDinhTruyLinh,
		TROCAP1LAN_TL.nGiaTri fTroCap1LanTruyLinh,
		HOTRO_CDNN_TRUYLINH_TL.nGiaTri fHoTroCdnnTruyLinh,
		HOTRO_PHONGNGUA_TRUYLINH_TL.nGiaTri fHoTroPhongNguaTruyLinh,
		TROCAPHANGTHANG_TL.nGiaTri fTroCapHangThangTruyLinh,
		TROCAPPHCN_TL.nGiaTri fTroCapPHCNTruyLinh,
		TROCAPCHETDOTNLD_TL.nGiaTri fTroCapChetDoTNLDTruyLinh,
		TAINANLD_DUONGSUCPHSK_TL.SoNgayDuongSucTNLD SoNgayDuongSucTNLDTruyLinh,
		TAINANLD_DUONGSUCPHSK_TL.nGiaTri fDuongSucTNLDTruyLinh
		into TBL_TCTNLD_DOC
	from TBL_TCTNLD TBL_TCTNLD
		left join (select top 1 * from TL_CanBo_CheDoBHXH 
		where sMaCheDo in (select * from splitstring(@MaCheDo))
		and ((sSoQuyetDinh is not null and sSoQuyetDinh <> '') 
		or (dNgayQuyetDinh is not null and dNgayQuyetDinh <> ''))) chedocha
		on TBL_TCTNLD.sMaCBo = chedocha.sMaCanBo
		left join
		(select tnld.sMaDonVi, tnld.nGiaTri, tnld.sMaCB, tnld.sMaCBo, tnld.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCap1Lan
		from TBL_TCTNLD tnld left join TL_CanBo_CheDoBHXH chedo on tnld.sMaCBo = chedo.sMaCanBo and tnld.sMaCheDo = chedo.sMaCheDo
		where tnld.sMaCheDo = 'TAINANLD_TROCAP1LAN') TROCAP1LAN
		on TBL_TCTNLD.sMaCBo = TROCAP1LAN.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAP1LAN.sMaDonVi
		left join
		(select tnld_1.sMaDonVi, tnld_1.nGiaTri, tnld_1.sMaCB, tnld_1.sMaCBo, tnld_1.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapTheoPhieuTruyTra
		from TBL_TCTNLD tnld_1 left join TL_CanBo_CheDoBHXH chedo on tnld_1.sMaCBo = chedo.sMaCanBo and tnld_1.sMaCheDo = chedo.sMaCheDo
		where tnld_1.sMaCheDo = 'TROCAPTHEOPHIEUTRUYTRA') TROCAPTHEOPHIEUTRUYTRA
		on TBL_TCTNLD.sMaCBo = TROCAPTHEOPHIEUTRUYTRA.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPTHEOPHIEUTRUYTRA.sMaDonVi
		left join
		(select tnld_2.sMaDonVi, tnld_2.nGiaTri, tnld_2.sMaCB, tnld_2.sMaCBo, tnld_2.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapHangThang
		from TBL_TCTNLD tnld_2 left join TL_CanBo_CheDoBHXH chedo on tnld_2.sMaCBo = chedo.sMaCanBo and tnld_2.sMaCheDo = chedo.sMaCheDo
		where tnld_2.sMaCheDo = 'TROCAPHANGTHANG') TROCAPHANGTHANG
		on TBL_TCTNLD.sMaCBo = TROCAPHANGTHANG.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPHANGTHANG.sMaDonVi
		left join
		(select tnld_3.sMaDonVi, sum(tnld_3.nGiaTri) nGiaTri, tnld_3.sMaCB, tnld_3.sMaCBo, tnld_3.sTenCbo, sum(chedo.fSoNgayHuongBHXH) SoNgayTroCapPHCN
		from TBL_TCTNLD tnld_3 left join TL_CanBo_CheDoBHXH chedo on tnld_3.sMaCBo = chedo.sMaCanBo and tnld_3.sTenCbo = chedo.sMaCheDo
		where tnld_3.sMaCheDo in ('TROCAPPHCN', 'TROCAPPHUCVU')
		group by tnld_3.sMaDonVi, tnld_3.sMaCB, tnld_3.sMaCBo, tnld_3.sTenCbo) TROCAPPHCN
		on TBL_TCTNLD.sMaCBo = TROCAPPHCN.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPPHCN.sMaDonVi
		left join
		(select tnld_3_1.sMaDonVi, sum(tnld_3_1.nGiaTri) nGiaTri, tnld_3_1.sMaCB, tnld_3_1.sMaCBo, tnld_3_1.sTenCbo, sum(chedo.fSoNgayHuongBHXH) SoNgayHOTROCDNN
		from TBL_TCTNLD tnld_3_1 left join TL_CanBo_CheDoBHXH chedo on tnld_3_1.sMaCBo = chedo.sMaCanBo and tnld_3_1.sTenCbo = chedo.sMaCheDo
		where tnld_3_1.sMaCheDo in ('HOTRO_CDNN')
		group by tnld_3_1.sMaDonVi, tnld_3_1.sMaCB, tnld_3_1.sMaCBo, tnld_3_1.sTenCbo) HOTROCDNN
		on TBL_TCTNLD.sMaCBo = HOTROCDNN.sMaCBo and TBL_TCTNLD.sMaDonVi = HOTROCDNN.sMaDonVi
		left join
		(select tnld_3_2.sMaDonVi, sum(tnld_3_2.nGiaTri) nGiaTri, tnld_3_2.sMaCB, tnld_3_2.sMaCBo, tnld_3_2.sTenCbo, sum(chedo.fSoNgayHuongBHXH) SoNgayHOTROPHONGNGUA
		from TBL_TCTNLD tnld_3_2 left join TL_CanBo_CheDoBHXH chedo on tnld_3_2.sMaCBo = chedo.sMaCanBo and tnld_3_2.sTenCbo = chedo.sMaCheDo
		where tnld_3_2.sMaCheDo in ('HOTRO_PHONGNGUA')
		group by tnld_3_2.sMaDonVi, tnld_3_2.sMaCB, tnld_3_2.sMaCBo, tnld_3_2.sTenCbo) HOTROPHONGNGUA
		on TBL_TCTNLD.sMaCBo = HOTROPHONGNGUA.sMaCBo and TBL_TCTNLD.sMaDonVi = HOTROPHONGNGUA.sMaDonVi
		left join
		(select tnld_4.sMaDonVi, tnld_4.nGiaTri, tnld_4.sMaCB, tnld_4.sMaCBo, tnld_4.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapChetDoTNLD
		from TBL_TCTNLD tnld_4 left join TL_CanBo_CheDoBHXH chedo on tnld_4.sMaCBo = chedo.sMaCanBo and tnld_4.sMaCheDo = chedo.sMaCheDo
		where tnld_4.sMaCheDo = 'TROCAPCHETDOTNLD') TROCAPCHETDOTNLD
		on TBL_TCTNLD.sMaCBo = TROCAPCHETDOTNLD.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPCHETDOTNLD.sMaDonVi
		left join
		(select tnld_5.sMaDonVi, tnld_5.nGiaTri, tnld_5.sMaCB, tnld_5.sMaCBo, tnld_5.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayDuongSucTNLD
		from TBL_TCTNLD tnld_5 left join TL_CanBo_CheDoBHXH chedo on tnld_5.sMaCBo = chedo.sMaCanBo and tnld_5.sMaCheDo = chedo.sMaCheDo
		where tnld_5.sMaCheDo = 'TAINANLD_DUONGSUCPHSK') TAINANLD_DUONGSUCPHSK
		on TBL_TCTNLD.sMaCBo = TAINANLD_DUONGSUCPHSK.sMaCBo and TBL_TCTNLD.sMaDonVi = TAINANLD_DUONGSUCPHSK.sMaDonVi
		left join
		(select tnld_6.sMaDonVi, tnld_6.nGiaTri, tnld_6.sMaCB, tnld_6.sMaCBo, tnld_6.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayCHIGIAMDINH
		from TBL_TCTNLD tnld_6 left join TL_CanBo_CheDoBHXH chedo on tnld_6.sMaCBo = chedo.sMaCanBo and tnld_6.sMaCheDo = chedo.sMaCheDo
		where tnld_6.sMaCheDo = 'CHIGIAMDINH') CHIGIAMDINH
		on TBL_TCTNLD.sMaCBo = CHIGIAMDINH.sMaCBo and TBL_TCTNLD.sMaDonVi = CHIGIAMDINH.sMaDonVi

		-- TRUYLINH
		left join
		(select tnld.sMaDonVi, tnld.nGiaTri, tnld.sMaCB, tnld.sMaCBo, tnld.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCap1Lan
		from TBL_TCTNLD tnld left join TL_CanBo_CheDoBHXH chedo on tnld.sMaCBo = chedo.sMaCanBo and tnld.sMaCheDo = chedo.sMaCheDo
		where tnld.sMaCheDo = 'TAINANLD_TROCAP1LAN_TRUYLINH') TROCAP1LAN_TL
		on TBL_TCTNLD.sMaCBo = TROCAP1LAN_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAP1LAN_TL.sMaDonVi
		left join
		(select tnld_1.sMaDonVi, tnld_1.nGiaTri, tnld_1.sMaCB, tnld_1.sMaCBo, tnld_1.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapTheoPhieuTruyTra
		from TBL_TCTNLD tnld_1 left join TL_CanBo_CheDoBHXH chedo on tnld_1.sMaCBo = chedo.sMaCanBo and tnld_1.sMaCheDo = chedo.sMaCheDo
		where tnld_1.sMaCheDo = 'HOTRO_CDNN_TRUYLINH') HOTRO_CDNN_TRUYLINH_TL
		on TBL_TCTNLD.sMaCBo = HOTRO_CDNN_TRUYLINH_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = HOTRO_CDNN_TRUYLINH_TL.sMaDonVi
		left join
		(select tnld_pn.sMaDonVi, tnld_pn.nGiaTri, tnld_pn.sMaCB, tnld_pn.sMaCBo, tnld_pn.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayHoTroPhongNgua
		from TBL_TCTNLD tnld_pn left join TL_CanBo_CheDoBHXH chedo on tnld_pn.sMaCBo = chedo.sMaCanBo and tnld_pn.sMaCheDo = chedo.sMaCheDo
		where tnld_pn.sMaCheDo = 'HOTRO_PHONGNGUA_TRUYLINH') HOTRO_PHONGNGUA_TRUYLINH_TL
		on TBL_TCTNLD.sMaCBo = HOTRO_PHONGNGUA_TRUYLINH_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = HOTRO_PHONGNGUA_TRUYLINH_TL.sMaDonVi
		left join
		(select tnld_2.sMaDonVi, tnld_2.nGiaTri, tnld_2.sMaCB, tnld_2.sMaCBo, tnld_2.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapHangThang
		from TBL_TCTNLD tnld_2 left join TL_CanBo_CheDoBHXH chedo on tnld_2.sMaCBo = chedo.sMaCanBo and tnld_2.sMaCheDo = chedo.sMaCheDo
		where tnld_2.sMaCheDo = 'TROCAPHANGTHANG_TRUYLINH') TROCAPHANGTHANG_TL
		on TBL_TCTNLD.sMaCBo = TROCAPHANGTHANG_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPHANGTHANG_TL.sMaDonVi
		left join
		(select tnld_3.sMaDonVi, sum(tnld_3.nGiaTri) nGiaTri, tnld_3.sMaCB, tnld_3.sMaCBo, tnld_3.sTenCbo, sum(chedo.fSoNgayHuongBHXH) SoNgayTroCapPHCN
		from TBL_TCTNLD tnld_3 left join TL_CanBo_CheDoBHXH chedo on tnld_3.sMaCBo = chedo.sMaCanBo and tnld_3.sTenCbo = chedo.sMaCheDo
		where tnld_3.sMaCheDo in ('TROCAPPHCN_TRUYLINH', 'TROCAPPHUCVU_TRUYLINH')
		group by tnld_3.sMaDonVi, tnld_3.sMaCB, tnld_3.sMaCBo, tnld_3.sTenCbo) TROCAPPHCN_TL
		on TBL_TCTNLD.sMaCBo = TROCAPPHCN_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPPHCN_TL.sMaDonVi
		left join
		(select tnld_4.sMaDonVi, tnld_4.nGiaTri, tnld_4.sMaCB, tnld_4.sMaCBo, tnld_4.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapChetDoTNLD
		from TBL_TCTNLD tnld_4 left join TL_CanBo_CheDoBHXH chedo on tnld_4.sMaCBo = chedo.sMaCanBo and tnld_4.sMaCheDo = chedo.sMaCheDo
		where tnld_4.sMaCheDo = 'TROCAPCHETDOTNLD_TRUYLINH') TROCAPCHETDOTNLD_TL
		on TBL_TCTNLD.sMaCBo = TROCAPCHETDOTNLD_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPCHETDOTNLD_TL.sMaDonVi
		left join
		(select tnld_5.sMaDonVi, tnld_5.nGiaTri, tnld_5.sMaCB, tnld_5.sMaCBo, tnld_5.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayDuongSucTNLD
		from TBL_TCTNLD tnld_5 left join TL_CanBo_CheDoBHXH chedo on tnld_5.sMaCBo = chedo.sMaCanBo and tnld_5.sMaCheDo = chedo.sMaCheDo
		where tnld_5.sMaCheDo = 'TAINANLD_DUONGSUCPHSK_TRUYLINH ') TAINANLD_DUONGSUCPHSK_TL
		on TBL_TCTNLD.sMaCBo = TAINANLD_DUONGSUCPHSK_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = TAINANLD_DUONGSUCPHSK_TL.sMaDonVi
		left join
		(select tnld_6.sMaDonVi, tnld_6.nGiaTri, tnld_6.sMaCB, tnld_6.sMaCBo, tnld_6.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayCHIGIAMDINH
		from TBL_TCTNLD tnld_6 left join TL_CanBo_CheDoBHXH chedo on tnld_6.sMaCBo = chedo.sMaCanBo and tnld_6.sMaCheDo = chedo.sMaCheDo
		where tnld_6.sMaCheDo = 'CHIGIAMDINH_TRUYLINH') CHIGIAMDINH_TL
		on TBL_TCTNLD.sMaCBo = CHIGIAMDINH_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = CHIGIAMDINH_TL.sMaDonVi

	--Lấy lương Sĩ quan
	select * into TBL_TCTNLD_SQ from
	(select
		1 bHangCha, 1 RowNum, 'I' STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'SQ' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fHoTroCdnn, null fHoTroPhongNgua, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData,
		null SoNgayDuongSucTNLDTruyLinh, null fChiGiamDinhTruyLinh, null fTroCap1LanTruyLinh, null fHoTroCdnnTruyLinh, null fHoTroPhongNguaTruyLinh, null fTroCapHangThangTruyLinh, null fTroCapPHCNTruyLinh, null fTroCapChetDoTNLDTruyLinh, null fDuongSucTNLDTruyLinh
	union all
	select
		0 bHangCha, 1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Ma_DonVi,  Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD,0 bHasData,
		SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh
	from TBL_TCTNLD_DOC
	where sMaCB like '1%' and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fHoTroCdnn, 0) <> 0 or isnull(fHoTroPhongNgua, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0 OR
				ISNULL(fChiGiamDinhTruyLinh,0) <> 0 OR ISNULL(fTroCap1LanTruyLinh,0) <> 0 OR ISNULL(fHoTroCdnnTruyLinh,0) <> 0 OR ISNULL(fHoTroPhongNguaTruyLinh,0) <> 0 OR ISNULL(fTroCapHangThangTruyLinh,0) <> 0 OR
				ISNULL( fTroCapPHCNTruyLinh,0) <> 0 OR ISNULL(fTroCapChetDoTNLDTruyLinh,0) <> 0 OR ISNULL( fDuongSucTNLDTruyLinh, 0) <> 0)
		) sq
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_SQ) > 1
		update TBL_TCTNLD_SQ set bHasData = 1

	--Lấy lương QNCN
	select * into TBL_TCTNLD_QNCN from
	(select
		1 bHangCha, 2 RowNum, 'II' STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'QNCN' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fHoTroCdnn, null fHoTroPhongNgua, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData,
		null SoNgayDuongSucTNLDTruyLinh, null fChiGiamDinhTruyLinh, null fTroCap1LanTruyLinh, null fHoTroCdnnTruyLinh, null fHoTroPhongNguaTruyLinh, null fTroCapHangThangTruyLinh, null fTroCapPHCNTruyLinh, null fTroCapChetDoTNLDTruyLinh, null fDuongSucTNLDTruyLinh
	union all
	select
		0 bHangCha, 2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Ma_DonVi,  Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD, 0 bHasData,
		SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh
	from TBL_TCTNLD_DOC
	where sMaCB like '2%' and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fHoTroCdnn, 0) <> 0 or isnull(fHoTroPhongNgua, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0)) qncn
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_QNCN) > 1
		update TBL_TCTNLD_QNCN set bHasData = 1

	--Lấy lương HSQ_BS
	select * into TBL_TCTNLD_HSQBS from
	(select
		1 bHangCha, 3 RowNum, 'III' STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'HSQ_BS' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fHoTroCdnn, null fHoTroPhongNgua, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData,
		null SoNgayDuongSucTNLDTruyLinh, null fChiGiamDinhTruyLinh, null fTroCap1LanTruyLinh, null fHoTroCdnnTruyLinh, null fHoTroPhongNguaTruyLinh, null fTroCapHangThangTruyLinh, null fTroCapPHCNTruyLinh, null fTroCapChetDoTNLDTruyLinh, null fDuongSucTNLDTruyLinh
	union all
	select
		0 bHangCha, 3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Ma_DonVi,  Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD, 0 bHasData,
		SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh
	from TBL_TCTNLD_DOC
	where sMaCB like '0%' and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fHoTroCdnn, 0) <> 0 or isnull(fHoTroPhongNgua, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0 OR
				ISNULL(fChiGiamDinhTruyLinh,0) <> 0 OR ISNULL(fTroCap1LanTruyLinh,0) <> 0 OR ISNULL(fHoTroCdnnTruyLinh,0) <> 0 OR ISNULL(fHoTroPhongNguaTruyLinh,0) <> 0 OR ISNULL(fTroCapHangThangTruyLinh,0) <> 0 OR
				ISNULL( fTroCapPHCNTruyLinh,0) <> 0 OR ISNULL(fTroCapChetDoTNLDTruyLinh,0) <> 0 OR ISNULL( fDuongSucTNLDTruyLinh, 0) <> 0)
		)  hsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_HSQBS) > 1
		update TBL_TCTNLD_HSQBS set bHasData = 1

	--Lấy lương VCQP
	select * into TBL_TCTNLD_VCQP from
	(select
		1 bHangCha, 4 RowNum, 'IV' STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'VCQP' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fHoTroCdnn, null fHoTroPhongNgua, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData,
		null SoNgayDuongSucTNLDTruyLinh, null fChiGiamDinhTruyLinh, null fTroCap1LanTruyLinh, null fHoTroCdnnTruyLinh, null fHoTroPhongNguaTruyLinh, null fTroCapHangThangTruyLinh, null fTroCapPHCNTruyLinh, null fTroCapChetDoTNLDTruyLinh, null fDuongSucTNLDTruyLinh
	union all
	select
		0 bHangCha, 4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Ma_DonVi,  Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD, 0 bHasData,
		SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh
	from TBL_TCTNLD_DOC
	where sMaCB in ('3.1', '3.2', '3.3', '413', '415') and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fHoTroCdnn, 0) <> 0 or isnull(fHoTroPhongNgua, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0)) vcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_VCQP) > 1
		update TBL_TCTNLD_VCQP set bHasData = 1

	--Lấy lương Lao Dộng hợp Dông
	select * into TBL_TCTNLD_LDHD from
	(select
		1 bHangCha, 5 RowNum, 'V' STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'LDHD' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fHoTroCdnn, null fHoTroPhongNgua, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData,
		null SoNgayDuongSucTNLDTruyLinh, null fChiGiamDinhTruyLinh, null fTroCap1LanTruyLinh, null fHoTroCdnnTruyLinh, null fHoTroPhongNguaTruyLinh, null fTroCapHangThangTruyLinh, null fTroCapPHCNTruyLinh, null fTroCapChetDoTNLDTruyLinh, null fDuongSucTNLDTruyLinh
	union all
	select
		0 bHangCha, 5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Ma_DonVi,  Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD, 0 bHasData,
		SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh
	from TBL_TCTNLD_DOC
	where sMaCB in ('43','423','425') and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fHoTroCdnn, 0) <> 0 or isnull(fHoTroPhongNgua, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0 OR
				ISNULL(fChiGiamDinhTruyLinh,0) <> 0 OR ISNULL(fTroCap1LanTruyLinh,0) <> 0 OR ISNULL(fHoTroCdnnTruyLinh,0) <> 0 OR ISNULL(fHoTroPhongNguaTruyLinh,0) <> 0 OR ISNULL(fTroCapHangThangTruyLinh,0) <> 0 OR
				ISNULL( fTroCapPHCNTruyLinh,0) <> 0 OR ISNULL(fTroCapChetDoTNLDTruyLinh,0) <> 0 OR ISNULL( fDuongSucTNLDTruyLinh, 0) <> 0)
		)  ldhd
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_LDHD) > 1
		update TBL_TCTNLD_LDHD set bHasData = 1

	--Ket qua
	select result.* into TBL_TCTNLD_RESULT from
	(select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) fTongSoTienThangNay,
	SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh,
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) fTongSoTienTruyLinh,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) +
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) FTongSoTien
	from TBL_TCTNLD_SQ
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) fTongSoTienThangNay,
	SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh,
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) fTongSoTienTruyLinh,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) +
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) FTongSoTien
	from TBL_TCTNLD_QNCN
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) fTongSoTienThangNay,
	SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh,
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) fTongSoTienTruyLinh,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) +
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) FTongSoTien
	from TBL_TCTNLD_HSQBS
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) fTongSoTienThangNay,
	SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh,
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) fTongSoTienTruyLinh,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) +
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) FTongSoTien
	from TBL_TCTNLD_VCQP
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) fTongSoTienThangNay,
	SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh,
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) fTongSoTienTruyLinh,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) +
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) FTongSoTien
	from TBL_TCTNLD_LDHD) result

	select 
		RowNum,
		STT,
		DoiTuong, 
		LoaiDoiTuong,
		sMaCB MaCb, 
		sMaCBo MaCbo, 
		sTenCbo TenCbo,
		sSoQuyetDinh SSoQuyetDinh,
		dNgayQuyetDinh DNgayQuyetDinh,
		Ma_DonVi MaDonVi,
		TenDonVi,
		SoNgayDuongSucTNLD,
		fChiGiamDinh/@DonViTinh fChiGiamDinh,
		fTroCap1Lan/@DonViTinh fTroCap1Lan,
		fTroCapTheoPhieuTruyTra/@DonViTinh fTroCapTheoPhieuTruyTra,
		fTroCapHangThang/@DonViTinh fTroCapHangThang,
		fTroCapPHCN/@DonViTinh fTroCapPHCN,
		fHoTroCdnn/@DonViTinh fHoTroCdnn,
		fHoTroPhongNgua/@DonViTinh fHoTroPhongNgua,
		fTroCapChetDoTNLD/@DonViTinh fTroCapChetDoTNLD,
		fDuongSucTNLD/@DonViTinh fDuongSucTNLD,
		fTongSoTienThangNay/@DonViTinh fTongSoTienThangNay,
		bHangCha IsHangCha,
		bHasData IsHasData,
		SoNgayDuongSucTNLDTruyLinh,
		fChiGiamDinhTruyLinh/@DonViTinh fChiGiamDinhTruyLinh,
		fTroCap1LanTruyLinh/@DonViTinh fTroCap1LanTruyLinh,
		fHoTroCdnnTruyLinh/@DonViTinh fHoTroCdnnTruyLinh,
		fHoTroPhongNguaTruyLinh/@DonViTinh fHoTroPhongNguaTruyLinh,
		fTroCapHangThangTruyLinh/@DonViTinh fTroCapHangThangTruyLinh,
		fTroCapPHCNTruyLinh/@DonViTinh fTroCapPHCNTruyLinh,
		fTroCapChetDoTNLDTruyLinh/@DonViTinh fTroCapChetDoTNLDTruyLinh,
		fDuongSucTNLDTruyLinh/@DonViTinh fDuongSucTNLDTruyLinh,
		fTongSoTienTruyLinh/@DonViTinh fTongSoTienTruyLinh,
		fTongSoTien/@DonViTinh fTongSoTien

	from TBL_TCTNLD_RESULT
	order by RowNum, LoaiDoiTuong, DoiTuong desc

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD]') AND type in (N'U'))
	drop table TBL_TCTNLD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_DOC]') AND type in (N'U'))
	drop table TBL_TCTNLD_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_SQ]') AND type in (N'U'))
	drop table TBL_TCTNLD_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_QNCN]') AND type in (N'U'))
	drop table TBL_TCTNLD_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_HSQBS]') AND type in (N'U'))
	drop table TBL_TCTNLD_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_VCQP]') AND type in (N'U'))
	drop table TBL_TCTNLD_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_LDHD]') AND type in (N'U'))
	drop table TBL_TCTNLD_LDHD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_RESULT]') AND type in (N'U'))
	drop table TBL_TCTNLD_RESULT;

END
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_getDataQuyetToanGiaiThich]    Script Date: 7/18/2024 3:45:52 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_getDataQuyetToanGiaiThich]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_getDataQuyetToanGiaiThich]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_getDataQuyetToanGiaiThich]    Script Date: 7/18/2024 3:45:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<DungNV>
-- Create date: <01/02/2024>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_bh_getDataQuyetToanGiaiThich] 
	-- Add the parameters for the stored procedure here
	@IdChungTu uniqueidentifier, 
	@XauNoiMa nvarchar(100) ,
	@NamLamViec int 
AS
BEGIN
	declare @IType int = (SELECT TOP(1) iDonViTinh FROM BH_DM_MucLucNganSach WHERE iNamLamViec = @NamLamViec AND sXauNoiMa = @XauNoiMa);--1:Ngay, 2:Thang, 3: nguoi
	DECLARE @DonViTinh int ;
	IF(@IType = 3) -- tinh so nguoi
		BEGIN
			select 
		               COUNT(CASE WHEN sMaCapBac LIKE '1%' then 1 ELSE NULL END) as ISoSQDeNghi,
		               SUM(CASE WHEN sMaCapBac LIKE '1%' then ISNULL(fSoTien, 0) + ISNULL(fTienTruyLinh, 0) ELSE 0 END) as FTienSQDeNghi,
		               COUNT(CASE WHEN sMaCapBac LIKE '2%' then 1 ELSE NULL END) as ISoQNCNDeNghi,
		               SUM(CASE WHEN sMaCapBac LIKE '2%' then ISNULL(fSoTien, 0) + ISNULL(fTienTruyLinh, 0) ELSE 0 END) as FTienQNCNDeNghi,
		               COUNT(CASE WHEN sMaCapBac LIKE '3.1%' OR sMaCapBac LIKE '3.2%' OR sMaCapBac LIKE '3.3%'  OR sMaCapBac = '413' OR sMaCapBac = '415' then 1 ELSE    NULL     END)    as       ISoCNVCQPDeNghi,
		               SUM(CASE WHEN sMaCapBac LIKE '3.1%' OR sMaCapBac LIKE '3.2%' OR sMaCapBac LIKE '3.3%' OR sMaCapBac = '413' OR sMaCapBac = '415' then ISNULL(fSoTien, 0) + ISNULL(fTienTruyLinh, 0)    ELSE 0   END)   as     FTienCNVCQPDeNghi,		
		               COUNT(CASE WHEN sMaCapBac LIKE '0%' then 1 ELSE NULL END) as ISoHSQBSDeNghi,
		               SUM(CASE WHEN sMaCapBac LIKE '0%' then ISNULL(fSoTien, 0) + ISNULL(fTienTruyLinh, 0) ELSE 0 END) as FTienHSQBSDeNghi,		
		               COUNT(CASE WHEN sMaCapBac = '423' OR sMaCapBac = '425' OR sMaCapBac = '43' then 1 ELSE NULL END) as ISoLDHDDeNghi,
		               SUM(CASE WHEN sMaCapBac = '423' OR sMaCapBac = '425' OR sMaCapBac = '43' then ISNULL(fSoTien, 0) + ISNULL(fTienTruyLinh, 0) ELSE 0 END) as FTienLDHDDeNghi,

					   -- TRUY LINH
		               gttc.sXauNoiMa as SXauNoiMa
	               FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gttc
				   LEFT JOIN BH_DM_MucLucNganSach mucluc ON gttc.sXauNoiMa = mucluc.sXauNoiMa and  mucluc.iNamLamViec= @NamLamViec
	               WHERE 
	               		 gttc.iID_QTC_Quy_ChungTu=@IdChungTu
	               		 and gttc.sXauNoiMa = @XauNoiMa
	               GROUP BY gttc.sXauNoiMa;
		END
	ELSE
		BEGIN

			select 
		               CASE
							WHEN @IType = 2 THEN SUM(CASE WHEN sMaCapBac LIKE '1%' then ISNULL(iSoNgayHuong, 0) + ISNULL(iSoNgayTruyLinh, 0) ELSE 0 END)/30
							ELSE SUM(CASE WHEN sMaCapBac LIKE '1%' then  ISNULL(iSoNgayHuong, 0) + ISNULL(iSoNgayTruyLinh, 0) ELSE 0 END)
					   END
					   AS ISoSQDeNghi,
		               SUM(CASE WHEN sMaCapBac LIKE '1%' then ISNULL(fSoTien, 0) + ISNULL(fTienTruyLinh, 0) ELSE 0 END) as FTienSQDeNghi,
		               CASE
							WHEN @IType = 2 THEN SUM(CASE WHEN sMaCapBac LIKE '2%' then  ISNULL(iSoNgayHuong, 0) + ISNULL(iSoNgayTruyLinh, 0) ELSE 0 END)/30
							ELSE SUM(CASE WHEN sMaCapBac LIKE '2%' then  ISNULL(iSoNgayHuong, 0) + ISNULL(iSoNgayTruyLinh, 0) ELSE 0 END)
					   END
					   AS ISoQNCNDeNghi,
		               SUM(CASE WHEN sMaCapBac LIKE '2%' then ISNULL(fSoTien, 0) + ISNULL(fTienTruyLinh, 0) ELSE 0 END) as FTienQNCNDeNghi,
		               CASE
							WHEN @IType = 2 THEN SUM(CASE WHEN sMaCapBac LIKE '3.1%' OR sMaCapBac LIKE '3.2%' OR sMaCapBac LIKE '3.3%' OR sMaCapBac = '413' OR sMaCapBac = '415' then  ISNULL(iSoNgayHuong, 0) + ISNULL(iSoNgayTruyLinh, 0) ELSE 0 END)/30
							ELSE SUM(CASE WHEN sMaCapBac LIKE '3.1%' OR sMaCapBac LIKE '3.2%' OR sMaCapBac LIKE '3.3%' OR sMaCapBac = '413' OR sMaCapBac = '415' then  ISNULL(iSoNgayHuong, 0) + ISNULL(iSoNgayTruyLinh, 0) ELSE 0 END)
					   END
					   AS ISoCNVCQPDeNghi,
		               SUM(CASE WHEN sMaCapBac LIKE '3.1%' OR sMaCapBac LIKE '3.2%' OR sMaCapBac LIKE '3.3%' OR sMaCapBac = '413' OR sMaCapBac = '415' then ISNULL(fSoTien, 0) + ISNULL(fTienTruyLinh, 0)    ELSE 0   END)   as     FTienCNVCQPDeNghi,	
		               CASE
							WHEN @IType = 2 THEN SUM(CASE WHEN SMaCapBac LIKE '0%' then  ISNULL(iSoNgayHuong, 0) + ISNULL(iSoNgayTruyLinh, 0) ELSE 0 END)/30
							ELSE SUM(CASE WHEN SMaCapBac LIKE '0%' then  ISNULL(iSoNgayHuong, 0) + ISNULL(iSoNgayTruyLinh, 0) ELSE 0 END)
					   END
					   AS ISoHSQBSDeNghi,					   
		               SUM(CASE WHEN sMaCapBac  LIKE '0%' then ISNULL(fSoTien, 0) + ISNULL(fTienTruyLinh, 0) ELSE 0 END) as FTienHSQBSDeNghi,		
		               CASE
							WHEN @IType = 2 THEN SUM(CASE WHEN sMaCapBac = '423' OR sMaCapBac = '425' OR sMaCapBac = '43' then  ISNULL(iSoNgayHuong, 0) + ISNULL(iSoNgayTruyLinh, 0) ELSE 0 END)/30
							ELSE SUM(CASE WHEN sMaCapBac = '423' OR sMaCapBac = '425' OR sMaCapBac = '43' then  ISNULL(iSoNgayHuong, 0) + ISNULL(iSoNgayTruyLinh, 0) ELSE 0 END)
					   END
					   AS ISoLDHDDeNghi,
		               SUM(CASE WHEN sMaCapBac = '423' OR sMaCapBac = '425' OR sMaCapBac = '43' then ISNULL(fSoTien, 0) + ISNULL(fTienTruyLinh, 0) ELSE 0 END) as FTienLDHDDeNghi,
		               gttc.sXauNoiMa as SXauNoiMa
	               FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gttc
					LEFT JOIN BH_DM_MucLucNganSach mucluc ON gttc.sXauNoiMa = mucluc.sXauNoiMa and  mucluc.iNamLamViec = @NamLamViec
	               WHERE 
	               		 gttc.iID_QTC_Quy_ChungTu=@IdChungTu
	               		 and gttc.sXauNoiMa = @XauNoiMa
	               GROUP BY gttc.sXauNoiMa;		
				   END

END
;
;
GO

---Sửa lại mô tả và iDonViTinh của mục lục Dưỡng sức phục hồi SK trong phần trợ cấp TNLĐ BNN - Khối dự toán
update BH_DM_MucLucNganSach
set
sMoTa = N'- DS, PHSK sau TNLĐ, BNN (ngày)',
iDonViTinh = 1
where sXauNoiMa = '9010001-010-011-0003-0001-0009-00';

---Sửa lại mô tả và iDonViTinh của mục lục Dưỡng sức phục hồi SK trong phần trợ cấp TNLĐ BNN - Khối hạch toán
update BH_DM_MucLucNganSach
set
sMoTa = N'- DS, PHSK sau TNLĐ, BNN (ngày)',
iDonViTinh = 1
where sXauNoiMa = '9010002-010-011-0003-0001-0009-00';