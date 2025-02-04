IF EXISTS (SELECT * FROM DM_ChuKy
WHERE Id_Code = 'rptBH_QuyetToan_BaoCaoQuyetToanThongChiBHXH')
UPDATE DM_ChuKy
SET TieuDe2_MoTa = N'Xác nhận quyết toán chi các chế độ bảo hiểm xã hội'
WHERE Id_Code = 'rptBH_QuyetToan_BaoCaoQuyetToanThongChiBHXH'
GO

IF EXISTS (SELECT * FROM DM_ChuKy
WHERE Id_Code = 'rptBH_QTC_QKPQL_Thongtri_Loai1')
UPDATE DM_ChuKy
SET TieuDe2_MoTa = N'Xác nhận quyết toán chi kinh phí quản lý BHXH, BHYT'
WHERE Id_Code = 'rptBH_QTC_QKPQL_Thongtri_Loai1'
GO

IF EXISTS (SELECT * FROM DM_ChuKy
WHERE Id_Code = 'rptBH_QTC_KCB_ThongTriQuyetToanChiKCB')
UPDATE DM_ChuKy
SET TieuDe2_MoTa = N'Xác nhận quyết toán chi kinh phí KCB tại quân y đơn vị'
WHERE Id_Code = 'rptBH_QTC_KCB_ThongTriQuyetToanChiKCB'
GO

IF EXISTS (SELECT * FROM DM_ChuKy
WHERE Id_Code = 'rptBH_QTC_QKPK_TSDK_Thongtri_Loai2')
UPDATE DM_ChuKy
SET TieuDe2_MoTa = N'Xác nhận quyết toán chi kinh phí KCB tại Trường Sa - DK'
WHERE Id_Code = 'rptBH_QTC_QKPK_TSDK_Thongtri_Loai2'
GO

IF EXISTS (SELECT * FROM DM_ChuKy
WHERE Id_Code = 'rptBH_QuyetToan_ThongTri_KPKCB_HBHYT')
UPDATE DM_ChuKy
SET TieuDe2_MoTa = N'Xác nhận quyết toán KP KCB BHYT'
WHERE Id_Code = 'rptBH_QuyetToan_ThongTri_KPKCB_HBHYT'
GO
