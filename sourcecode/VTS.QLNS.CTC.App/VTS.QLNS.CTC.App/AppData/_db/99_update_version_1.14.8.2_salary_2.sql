delete [HT_Quyen_ChucNang] where [iID_MaChucNang] = 'SALARY_MANAGEMENT_BACK_PAY_SALARY_INDEX';
delete [HT_ChucNang] where [iID_MaChucNang] = 'SALARY_MANAGEMENT_BACK_PAY_SALARY_INDEX';

DECLARE @iID_ChucNangCha nvarchar(100)
set @iID_ChucNangCha = (select iID_ChucNangCha  from HT_ChucNang
where iID_MaChucNang = 'SALARY_MANAGEMENT')

IF NOT EXISTS (SELECT 1 FROM [HT_ChucNang] where [iID_MaChucNang] = 'SALARY_MANAGEMENT_BACK_PAY_SALARY_INDEX')
BEGIN 
INSERT [dbo].[HT_ChucNang] ([iID_MaChucNang], [BHangCha], [iID_ChucNangCha], [ITrangThai], [iID_ChucNang], [sSTT], [sTenChucNang])
VALUES (N'SALARY_MANAGEMENT_BACK_PAY_SALARY_INDEX', 1, @iID_ChucNangCha, 1, N'8095b6cc-a8f3-4d5b-bcc5-05c93bee3716', N'06-02-04-00-00', N'Bảng lương tháng truy thu')
END
GO

IF NOT EXISTS (SELECT 1 FROM [HT_Quyen_ChucNang] where [iID_MaChucNang] = 'SALARY_MANAGEMENT_BACK_PAY_SALARY_INDEX')
BEGIN 
INSERT [dbo].[HT_Quyen_ChucNang] ([iID_MaChucNang], [iID_MaQuyen]) 
VALUES (N'SALARY_MANAGEMENT_BACK_PAY_SALARY_INDEX', N'ADMIN')
END
GO

/****** Object:  StoredProcedure [dbo].[sp_bh_report_tong_hop_du_toan_thu_chi_bhxh_bhyt_bhtn]    Script Date: 9/23/2024 4:35:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_report_tong_hop_du_toan_thu_chi_bhxh_bhyt_bhtn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_report_tong_hop_du_toan_thu_chi_bhxh_bhyt_bhtn]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_report_tong_hop_du_toan_thu_chi_bhxh_bhyt_bhtn]    Script Date: 9/23/2024 4:35:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_report_tong_hop_du_toan_thu_chi_bhxh_bhyt_bhtn]
	@NamLamViec int ,
	@LstMaDonVi nvarchar(max),
	@SoQuyetDinh nvarchar(200),
	@NgayQuyetDinh nvarchar(200),
	@DVT int,
	@IsMillionRound bit
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
			AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@LstMaDonVi))

		UNION ALL
		SELECT '2', NEWID(), @IIdDTT, N'Dự toán thu BHTN', 2, 2, SUM(ISNULL(ctct.fThuBHTN, 0)) FSoTien
		FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
		join BH_DTT_BHXH_PhanBo_ChungTu ct on ctct.iID_DTT_BHXH_ChungTu = ct.iID_DTT_BHXH_PhanBo_ChungTu
		WHERE ctct.iNamLamViec = @NamLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
			AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@LstMaDonVi))

		UNION ALL
		SELECT '3', NEWID(), @IIdDTT, N'Dự toán thu BHYT quân nhân', 2, 3, SUM(ISNULL(ctct.fThuBHYT, 0)) FSoTien
		FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
		join BH_DTT_BHXH_PhanBo_ChungTu ct on ctct.iID_DTT_BHXH_ChungTu = ct.iID_DTT_BHXH_PhanBo_ChungTu
		WHERE ctct.iNamLamViec = @NamLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
			AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@LstMaDonVi))
			AND (ctct.sXauNoiMa like '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%')

		UNION ALL
		SELECT '4', NEWID(), @IIdDTT, N'Dự toán thu BHYT người lao động', 2, 4, SUM(ISNULL(ctct.fThuBHYT, 0)) FSoTien
		FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
		join BH_DTT_BHXH_PhanBo_ChungTu ct on ctct.iID_DTT_BHXH_ChungTu = ct.iID_DTT_BHXH_PhanBo_ChungTu
		WHERE ctct.iNamLamViec = @NamLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
			AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@LstMaDonVi))
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
			AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@LstMaDonVi))
			AND (ctct.sXauNoiMa like '9030001-010-011-0001%' OR ctct.sXauNoiMa like '9030001-010-011-0002%')

		UNION ALL
		SELECT '-', NEWID(), @IIdThuBHYTTN, N'Thân nhân người lao động', 3, 1, SUM(ISNULL(ctct.fDuToan, 0)) FSoTien
		FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
		join BH_DTTM_BHYT_ThanNhan_PhanBo ct on ctct.iID_DTTM_BHYT_ThanNhan_PhanBo = ct.iID_DTTM_BHYT_ThanNhan_PhanBo
		WHERE ctct.iNamLamViec = @NamLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
			AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@LstMaDonVi))
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
			AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@LstMaDonVi))
			AND ctct.sMaLoaiChi = '01'

		UNION ALL
		SELECT '2', NEWID(), @IIdDTC, N'Dự toán chi kinh phí quản lý BHXH', 2, 2, SUM(ISNULL(ctct.fTienTuChi, 0)) FSoTien
		FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
		join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
		WHERE ctct.iNamLamViec = @NamLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
			AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@LstMaDonVi))
			AND ctct.sMaLoaiChi = '02'

		UNION ALL
		SELECT '3', NEWID(), @IIdDTC, N'Dự toán chi kinh phí KCB tại quân y đơn vị', 2, 3, SUM(ISNULL(ctct.fTienTuChi, 0)) FSoTien
		FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
		join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
		WHERE ctct.iNamLamViec = @NamLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
			AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@LstMaDonVi))
			AND ctct.sMaLoaiChi = '03'

		UNION ALL
		SELECT '4', NEWID(), @IIdDTC, N'Dự toán chi KCB tại Trường Sa - DK', 2, 4, SUM(ISNULL(ctct.fTienTuChi, 0)) FSoTien
		FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
		join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
		WHERE ctct.iNamLamViec = @NamLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
			AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@LstMaDonVi))
			AND ctct.sMaLoaiChi = '04'
			)
	if (@IsMillionRound = 1)
	SELECT STT, IIdChungTu, IIdParent, SNoiDung, ILevel, IThuTu, round(FSoTien / 1000000, 0) * 1000000 / @DVT FSoTien from #result
	else 
	SELECT STT, IIdChungTu, IIdParent, SNoiDung, ILevel, IThuTu, FSoTien/@DVT FSoTien from #result
	DROP TABLE #result;

END
;
;
;
GO

delete DM_ChuKy
where Id_Type='rptBHYT_KHTM_ChiTiet'
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) VALUES (N'73b83f79-4581-4801-ba55-65539c593dc8', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptBHYT_KHTM_ChiTiet', NULL, N'rptBHYT_KHTM_ChiTiet', NULL, NULL, NULL, NULL, NULL, N'KHTM_BHYT_THANNHAN', NULL, N'Báo cáo kế hoạch thu mua BHYT thân nhân', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'KẾ HOẠCH MUA THẺ BẢO HIỂM Y TẾ THÂN NHÂN QUÂN NHÂN; THÂN NHÂN CƠ YẾU; THÂN NHÂN CN&VCQP; HS; SV;HVQS XÃ, PHƯỜNG; HV ĐÀO TẠO SĨ QUAN DỰ BỊ; HV QUỐC TẾ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

