IF EXISTS (SELECT * FROM DM_ChuKy
WHERE Id_Code = 'rptBH_QuyetToan_BaoCaoQuyetToanThongChiBHXH')
UPDATE DM_ChuKy
SET Ten6_MoTa = N'Chi các chế độ bảo hiểm xã hội'
WHERE Id_Code = 'rptBH_QuyetToan_BaoCaoQuyetToanThongChiBHXH'
ELSE INSERT INTO DM_ChuKy(Id_Code, Id_Type, TieuDe2_MoTa, Ten6_MoTa)
VALUES ('rptBH_QuyetToan_BaoCaoQuyetToanThongChiBHXH', 'rptBH_QuyetToan_BaoCaoQuyetToanThongChiBHXH', N'Xác nhận quyết toán chi các chế độ bảo hiểm xã hội', N'Chi các chế độ bảo hiểm xã hội')
GO

IF EXISTS (SELECT * FROM DM_ChuKy
WHERE Id_Code = 'rptBH_QTC_QKPQL_Thongtri_Loai1')
UPDATE DM_ChuKy
SET Ten6_MoTa = N'Chi kinh phí quản lý BHXH, BHYT'
WHERE Id_Code = 'rptBH_QTC_QKPQL_Thongtri_Loai1'
ELSE INSERT INTO DM_ChuKy(Id_Code, Id_Type, TieuDe2_MoTa, Ten6_MoTa)
VALUES ('rptBH_QTC_QKPQL_Thongtri_Loai1', 'rptBH_QTC_QKPQL_Thongtri_Loai1', N'Xác nhận quyết toán chi kinh phí quản lý BHXH, BHYT', N'Chi kinh phí quản lý BHXH, BHYT')
GO

IF EXISTS (SELECT * FROM DM_ChuKy
WHERE Id_Code = 'rptBH_QTC_KCB_ThongTriQuyetToanChiKCB')
UPDATE DM_ChuKy
SET Ten6_MoTa = N'Chi kinh phí KCB tại quân y đơn vị'
WHERE Id_Code = 'rptBH_QTC_KCB_ThongTriQuyetToanChiKCB'
ELSE INSERT INTO DM_ChuKy(Id_Code, Id_Type, TieuDe2_MoTa, Ten6_MoTa)
VALUES ('rptBH_QTC_KCB_ThongTriQuyetToanChiKCB', 'rptBH_QTC_KCB_ThongTriQuyetToanChiKCB', N'Xác nhận quyết toán chi kinh phí KCB tại quân y đơn vị', N'Chi kinh phí KCB tại quân y đơn vị')
GO

IF EXISTS (SELECT * FROM DM_ChuKy
WHERE Id_Code = 'rptBH_QTC_QKPK_TSDK_Thongtri_Loai2')
UPDATE DM_ChuKy
SET Ten6_MoTa = N'Chi kinh phí KCB tại Trường Sa - DK'
WHERE Id_Code = 'rptBH_QTC_QKPK_TSDK_Thongtri_Loai2'
ELSE INSERT INTO DM_ChuKy(Id_Code, Id_Type, TieuDe2_MoTa, Ten6_MoTa)
VALUES ('rptBH_QTC_QKPK_TSDK_Thongtri_Loai2', 'rptBH_QTC_QKPK_TSDK_Thongtri_Loai2', N'Xác nhận quyết toán chi kinh phí KCB tại Trường Sa - DK', N'Chi kinh phí KCB tại Trường Sa - DK')
GO

IF EXISTS (SELECT * FROM DM_ChuKy
WHERE Id_Code = 'rptBH_QTC_QKPK_TNKDQKCBBHYTQNTT_Thongtri_Loai2')
UPDATE DM_ChuKy
SET Ten6_MoTa = N'Chi từ nguồn kết dư quỹ KCB BHYT quân nhân'
WHERE Id_Code = 'rptBH_QTC_QKPK_TNKDQKCBBHYTQNTT_Thongtri_Loai2'
ELSE INSERT INTO DM_ChuKy(Id_Code, Id_Type, TieuDe2_MoTa, Ten6_MoTa)
VALUES ('rptBH_QTC_QKPK_TNKDQKCBBHYTQNTT_Thongtri_Loai2', 'rptBH_QTC_QKPK_TNKDQKCBBHYTQNTT_Thongtri_Loai2', N'Quyết toán chi từ nguồn kết dư quỹ KCB BHYT quân nhân', N'Chi từ nguồn kết dư quỹ KCB BHYT quân nhân')
GO


IF EXISTS (SELECT * FROM DM_ChuKy
WHERE Id_Code = 'rptBH_QTC_QKPK_HSSV_Thongtri_Loai2')
UPDATE DM_ChuKy
SET Ten6_MoTa = N'Chi kinh phí chăm sóc sức khỏe ban đầu học sinh, sinh viên'
WHERE Id_Code = 'rptBH_QTC_QKPK_HSSV_Thongtri_Loai2'
ELSE INSERT INTO DM_ChuKy(Id_Code, Id_Type, TieuDe2_MoTa, Ten6_MoTa)
VALUES ('rptBH_QTC_QKPK_HSSV_Thongtri_Loai2', 'rptBH_QTC_QKPK_HSSV_Thongtri_Loai2', N'Xác nhận quyết toán chi kinh phí chăm sóc sức khỏe ban đầu cho học sinh, sinh viên', N'Chi kinh phí chăm sóc sức khỏe ban đầu học sinh, sinh viên')
GO

IF EXISTS (SELECT * FROM DM_ChuKy
WHERE Id_Code = 'rptBH_QTC_QKPK_NLD_Thongtri_Loai2')
UPDATE DM_ChuKy
SET Ten6_MoTa = N'Chi kinh phí chăm sóc sức khỏe ban đầu người lao động'
WHERE Id_Code = 'rptBH_QTC_QKPK_NLD_Thongtri_Loai2'
ELSE INSERT INTO DM_ChuKy(Id_Code, Id_Type, TieuDe2_MoTa, Ten6_MoTa)
VALUES ('rptBH_QTC_QKPK_NLD_Thongtri_Loai2', 'rptBH_QTC_QKPK_NLD_Thongtri_Loai2', N'Xác nhận quyết toán chi kinh phí chăm sóc sức khỏe ban đầu người lao động', N'Chi kinh phí chăm sóc sức khỏe ban đầu người lao động')
GO

IF EXISTS (SELECT * FROM DM_ChuKy
WHERE Id_Code = 'rptBH_QTC_QKPK_HTBHTNTT_Thongtri_Loai2')
UPDATE DM_ChuKy
SET Ten6_MoTa = N'Chi hỗ trợ người lao động tham gia BHTN'
WHERE Id_Code = 'rptBH_QTC_QKPK_HTBHTNTT_Thongtri_Loai2'
ELSE INSERT INTO DM_ChuKy(Id_Code, Id_Type, TieuDe2_MoTa, Ten6_MoTa)
VALUES ('rptBH_QTC_QKPK_HTBHTNTT_Thongtri_Loai2', 'rptBH_QTC_QKPK_HTBHTNTT_Thongtri_Loai2', N'Xác nhận quyết toán chi kinh phí hỗ trợ BHTN', N'Chi hỗ trợ người lao động tham gia BHTN')
GO

IF EXISTS (SELECT * FROM DM_ChuKy
WHERE Id_Code = 'rptBH_QTC_QKPK_MSTT_Thongtri_Loai2')
UPDATE DM_ChuKy
SET Ten6_MoTa = N'Quyết toán chi kinh phí mua sắm trang thiết bị ý tế'
WHERE Id_Code = 'rptBH_QTC_QKPK_MSTT_Thongtri_Loai2'
ELSE INSERT INTO DM_ChuKy(Id_Code, Id_Type, TieuDe2_MoTa, Ten6_MoTa)
VALUES ('rptBH_QTC_QKPK_MSTT_Thongtri_Loai2', 'rptBH_QTC_QKPK_MSTT_Thongtri_Loai2', N'Xác nhận quyết toán chi kinh phí mua sắm trang thiết bị ý tế', N'Quyết toán chi kinh phí mua sắm trang thiết bị ý tế')
GO

IF EXISTS (SELECT * FROM DM_ChuKy
WHERE Id_Code = 'rptBH_QTC_QKPK_TNKDQKCBBHYTQNTT_Thongtri_Loai2')
UPDATE DM_ChuKy
SET Ten6_MoTa = N'Chi kinh phí từ nguồn kết dư quỹ KCB BHYT quân nhân'
WHERE Id_Code = 'rptBH_QTC_QKPK_TNKDQKCBBHYTQNTT_Thongtri_Loai2'
ELSE INSERT INTO DM_ChuKy(Id_Code, Id_Type, TieuDe2_MoTa, Ten6_MoTa)
VALUES ('rptBH_QuyetToan_BaoCaoQuyetToanThongChiBHXH', 'rptBH_QuyetToan_BaoCaoQuyetToanThongChiBHXH', N'Xác nhận quyết toán chi các chế độ bảo hiểm xã hội', N'Chi các chế độ bảo hiểm xã hội')
GO

IF NOT EXISTS (SELECT * FROM BH_DM_ThamDinhQuyetToan WHERE iNamLamViec = 2024)
INSERT INTO BH_DM_ThamDinhQuyetToan(iKieuChu, iMa, iMaCha, iNamLamViec, iTrangThai, sNguoiSua, sNguoiTao, sNoiDung, sSTT, sXauNoiMa)
SELECT iKieuChu, iMa, iMaCha, 2024, 1, 'admin', 'admin', sNoiDung, sSTT, sXauNoiMa FROM BH_DM_ThamDinhQuyetToan WHERE iNamLamViec = 2023
GO

/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]    Script Date: 22/01/2024 7:10:04 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]    Script Date: 22/01/2024 7:10:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]
@IdChungTu uniqueidentifier,
@SLNS nvarchar(max),
@INamLamViec int
as
begin

		declare @quy int;
		select @quy = iquychungtu from BH_QTC_Quy_CheDoBHXH where ID_QTC_Quy_CheDoBHXH = @IdChungTu;
	---Lấy danh sách mục lục ngân sách
		select 
			danhmuc.iID_MLNS as iID_MLNS,
			danhmuc.iID_MLNS_Cha,
			danhmuc.sLNS,
			danhmuc.sL,
			danhmuc.sK,
			danhmuc.sM,
			danhmuc.sTM,
			danhmuc.sTTM,
			danhmuc.sNG,
			danhmuc.sTNG,
			danhmuc.sXauNoiMa,
			danhmuc.sMoTa,
			danhmuc.bHangCha,
			danhmuc.sDuToanChiTietToi
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach as danhmuc
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs in (SELECT * FROM f_split(@SLNS))
		
	---Lấy thông tin chi tiết chứng từ
		select 
			qtcn_ct.ID_QTC_Quy_CheDoBHXH_ChiTiet,
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sLoaiTroCap,
			qtcn_ct.fTienDuToanDuyet,
			(isnull(qtcn_ct_truoc.fTienCNVCQP_DeNghi, 0) + isnull(qtcn_ct_truoc.fTienHSQBS_DeNghi, 0) + isnull(qtcn_ct_truoc.fTienLDHD_DeNghi, 0) + isnull(qtcn_ct_truoc.fTienQNCN_DeNghi, 0) + isnull(qtcn_ct_truoc.fTienSQ_DeNghi, 0)) fTienLuyKeCuoiQuyTruoc,
			(isnull(qtcn_ct_truoc.iSoCNVCQP_DeNghi, 0) + isnull(qtcn_ct_truoc.iSoHSQBS_DeNghi, 0) + isnull(qtcn_ct_truoc.iSoLDHD_DeNghi, 0) + isnull(qtcn_ct_truoc.iSoQNCN_DeNghi, 0) + isnull(qtcn_ct_truoc.iSoSQ_DeNghi, 0)) iSoLuyKeCuoiQuyTruoc,
			qtcn_ct.iSoSQ_DeNghi,
			qtcn_ct.fTienSQ_DeNghi,
			qtcn_ct.iSoQNCN_DeNghi,
			qtcn_ct.fTienQNCN_DeNghi,
			qtcn_ct.iSoCNVCQP_DeNghi,
			qtcn_ct.fTienCNVCQP_DeNghi,
			qtcn_ct.iSoHSQBS_DeNghi,
			qtcn_ct.fTienHSQBS_DeNghi,
			--qtcn_ct.iTongSo_DeNghi,
			--qtcn_ct.fTongTien_DeNghi,
			--qtcn_ct.fTongTien_PheDuyet,
			qtcn_ct.iSoLDHD_DeNghi,
			qtcn_ct.fTienLDHD_DeNghi,
			qtcn.iID_MaDonVi iIDMaDonVi,
			qtcn.iNamChungTu iNamLamViec
		into #tblQuyetToanQuyChiTiet
		from BH_QTC_Quy_CheDoBHXH_ChiTiet as qtcn_ct
		inner join BH_QTC_Quy_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Quy_CheDoBHXH = qtcn.ID_QTC_Quy_CheDoBHXH
		left join (
			select sum(isnull(fTienCNVCQP_DeNghi, 0)) fTienCNVCQP_DeNghi, 
			sum(isnull(fTienHSQBS_DeNghi, 0)) fTienHSQBS_DeNghi, 
			sum(isnull(fTienLDHD_DeNghi, 0)) fTienLDHD_DeNghi, 
			sum(isnull(fTienQNCN_DeNghi, 0)) fTienQNCN_DeNghi, 
			sum(isnull(fTienSQ_DeNghi, 0)) fTienSQ_DeNghi, 
			sum(isnull(iSoCNVCQP_DeNghi, 0)) iSoCNVCQP_DeNghi, 
			sum(isnull(iSoHSQBS_DeNghi, 0)) iSoHSQBS_DeNghi, 
			sum(isnull(iSoLDHD_DeNghi, 0)) iSoLDHD_DeNghi, 
			sum(isnull(iSoQNCN_DeNghi, 0)) iSoQNCN_DeNghi, 
			sum(isnull(iSoSQ_DeNghi, 0)) iSoSQ_DeNghi, 
			ct.iID_MaDonVi, ct.iNamChungTu from BH_QTC_Quy_CheDoBHXH_ChiTiet ctct
			inner join BH_QTC_Quy_CheDoBHXH ct
			on ctct.iID_QTC_Quy_CheDoBHXH = ct.ID_QTC_Quy_CheDoBHXH
			where ct.iQuyChungTu < @quy
			group by ct.iID_MaDonVi, ct.iNamChungTu
			) qtcn_ct_truoc 
			on qtcn.iID_MaDonVi = qtcn_ct_truoc.iID_MaDonVi and qtcn.iNamChungTu = qtcn_ct_truoc.iNamChungTu
		where qtcn_ct.iID_QTC_Quy_CheDoBHXH = @IdChungTu
			AND qtcn.iNamChungTu=@INamLamViec;

		
	---Kết quả hiển thị trả về
		select 
			mucluc.iID_MLNS,
			mucluc.iID_MLNS_Cha,
			mucluc.sLNS,
			mucluc.sL,
			mucluc.sK,
			mucluc.sM,
			mucluc.sTM,
			mucluc.sTTM,
			mucluc.sNG,
			mucluc.sTNG,
			mucluc.sXauNoiMa,
			mucluc.bHangCha,
			mucluc.sDuToanChiTietToi,
			chi_tiet.ID_QTC_Quy_CheDoBHXH_ChiTiet,
			mucluc.iID_MLNS as iID_MucLucNganSach,
			mucluc.sMoTa as sLoaiTroCap,
			chi_tiet.fTienDuToanDuyet,
			chi_tiet.iSoLuyKeCuoiQuyTruoc,
			chi_tiet.fTienLuyKeCuoiQuyTruoc,
			chi_tiet.iSoSQ_DeNghi,
			chi_tiet.fTienSQ_DeNghi,
			chi_tiet.iSoQNCN_DeNghi,
			chi_tiet.fTienQNCN_DeNghi,
			chi_tiet.iSoCNVCQP_DeNghi,
			chi_tiet.fTienCNVCQP_DeNghi,
			chi_tiet.iSoHSQBS_DeNghi,
			chi_tiet.fTienHSQBS_DeNghi,
			--chi_tiet.iTongSo_DeNghi,
			--chi_tiet.fTongTien_DeNghi,
			--chi_tiet.fTongTien_PheDuyet,
			chi_tiet.iSoLDHD_DeNghi,
			chi_tiet.fTienLDHD_DeNghi,
			chi_tiet.iIDMaDonVi,
			chi_tiet.iNamLamViec
		from #tblMucLucNganSach as mucluc
		left join #tblQuyetToanQuyChiTiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
		order by mucluc.sXauNoiMa
	
	drop table #tblMucLucNganSach;
	drop table #tblQuyetToanQuyChiTiet;
end
;
;
;
GO
