/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]    Script Date: 2/28/2024 1:49:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]    Script Date: 2/28/2024 1:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_quyet_toan_chinamKCB_chitiet] @IdChungTu uniqueidentifier,
	@Lns nvarchar (MAX),
	@INamLamViec INT,
	@IsTongHop4Quy BIT,
	@MaDonVi nvarchar (100),
	@Loai BIT AS BEGIN---Lấy danh sách mục lục ngân sách
	SELECT
		danhmuc.iID_MLNS AS iID_MLNS,
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
		danhmuc.sDuToanChiTietToi INTO #tblMucLucNganSach 
	FROM
		BH_DM_MucLucNganSach AS danhmuc 
	WHERE
		danhmuc.iNamLamViec = @INamLamViec 
		AND danhmuc.sLNs IN (SELECT * FROM splitstring (@Lns)) 
		AND danhmuc.iTrangThai= 1 ---Lấy thông tin chi tiết chứng từ
	SELECT
		qtcn_ct.ID_QTC_Nam_KCB_QuanYDonVi_ChiTiet,
		qtcn_ct.iID_MucLucNganSach,
		qtcn_ct.sNoiDung,
		qtcn_ct.sXauNoiMa,
		qtcn_ct.iNamLamViec,
		qtcn_ct.iID_MaDonVi,
--qtcn_ct.fTien_DuToanNamTruocChuyenSang,
--qtcn_ct.fTien_DuToanGiaoNamNay,
--qtcn_ct.fTien_TongDuToanDuocGiao,
		qtcn_ct.fTien_ThucChi,
		(
		CASE
				
				WHEN isnull(qtcn_ct.fTien_TongDuToanDuocGiao, 0) > isnull(qtcn_ct.fTien_ThucChi, 0) THEN
				isnull(qtcn_ct.fTien_TongDuToanDuocGiao, 0) - isnull(qtcn_ct.fTien_ThucChi, 0) ELSE 0 
			END 
			) AS fTienThua,
			(
			CASE
					
					WHEN isnull(qtcn_ct.fTien_ThucChi, 0) > isnull(qtcn_ct.fTien_TongDuToanDuocGiao, 0) THEN
					isnull(qtcn_ct.fTien_ThucChi, 0) - isnull(qtcn_ct.fTien_TongDuToanDuocGiao, 0) ELSE 0 
				END 
				) AS fTienThieu INTO #tblQuyetToanNamChiTiet 
			FROM
				BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet AS qtcn_ct
				INNER JOIN BH_QTC_Nam_KCB_QuanYDonVi AS qtcn ON qtcn_ct.iID_QTC_Nam_KCB_QuanYDonVi = qtcn.ID_QTC_Nam_KCB_QuanYDonVi 
			WHERE
				qtcn_ct.iID_QTC_Nam_KCB_QuanYDonVi = @IdChungTu;
---Kết quả hiển thị trả về
			IF
				(@Loai = 1) SELECT
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
				mucluc.sDuToanChiTietToi AS SDuToanChiTietToi,
				chi_tiet.ID_QTC_Nam_KCB_QuanYDonVi_ChiTiet,
				mucluc.iID_MLNS AS iID_MucLucNganSach,
				mucluc.sMoTa AS sNoiDung,
				chi_tiet.iID_MaDonVi,
				chi_tiet.iNamLamViec,
--chi_tiet.fTien_DuToanNamTruocChuyenSang,
				(
					isnull(dtTruoc.fTienTuChi, 0)  - isnull(qtTruoc.fTien_ThucChi, 0) - isnull(qtTruoc.fTien_ThucChi, 0) 
				) fTien_DuToanNamTruocChuyenSang,
				isnull(dt.fTienTuChi, 0)  fTien_DuToanGiaoNamNay,
--chi_tiet.fTien_TongDuToanDuocGiao,
				chi_tiet.fTien_ThucChi,
				chi_tiet.fTienThua,
				chi_tiet.fTienThieu 
			FROM
				#tblMucLucNganSach AS mucluc
				LEFT JOIN (
				SELECT
					Sum(ctct.fTienTuChi) fTienTuChi,
					ctct.sXauNoiMa
				FROM
					BH_DTC_PhanBoDuToanChi_ChiTiet ctct
					JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID 
				WHERE
					ctct.iID_MaDonVi = @MaDonVi 
					AND iNamLamViec = @INamLamViec 
					AND BIsKhoa = 1 
					group by ctct.sXauNoiMa
				) dt ON dt.sXauNoiMa = mucluc.sXauNoiMa
				LEFT JOIN (
				SELECT
					(ctct.fTienTuChi) fTienTuChi,
					ctct.sXauNoiMa
				FROM
					BH_DTC_PhanBoDuToanChi_ChiTiet ctct
					JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID 
				WHERE
					ctct.iID_MaDonVi = @MaDonVi 
					AND ct.iNamChungTu = @INamLamViec - 1 
					AND BIsKhoa = 1 
					group by ctct.sXauNoiMa
				) dtTruoc ON dt.sXauNoiMa = mucluc.sXauNoiMa
				LEFT JOIN (
				SELECT
					ctct.* 
				FROM
					BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet ctct
					JOIN BH_QTC_Nam_KCB_QuanYDonVi ct ON ctct.iID_QTC_Nam_KCB_QuanYDonVi = ct.ID_QTC_Nam_KCB_QuanYDonVi 
				WHERE
					ctct.iID_MaDonVi = @MaDonVi 
					AND ct.iNamLamViec = @INamLamViec - 1 
					AND BIsKhoa = 1 
				) qtTruoc ON qtTruoc.sXauNoiMa = mucluc.sXauNoiMa
				LEFT JOIN #tblQuyetToanNamChiTiet AS chi_tiet ON mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach 
			ORDER BY
				mucluc.sXauNoiMa ELSE ---Kết quả hiển thị trả về
			SELECT
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
				mucluc.sDuToanChiTietToi AS SDuToanChiTietToi,
				chi_tiet.ID_QTC_Nam_KCB_QuanYDonVi_ChiTiet,
				mucluc.iID_MLNS AS iID_MucLucNganSach,
				mucluc.sMoTa AS sNoiDung,
				chi_tiet.iID_MaDonVi,
				chi_tiet.iNamLamViec,
--chi_tiet.fTien_DuToanNamTruocChuyenSang,
				(
					isnull(dtTruoc.fTienTuChi, 0)  - isnull(qtTruoc.fTien_ThucChi, 0) - isnull(qtTruoc.fTien_ThucChi, 0) 
				) fTien_DuToanNamTruocChuyenSang,
				(isnull(dt.fTienTuChi, 0) ) fTien_DuToanGiaoNamNay,
--chi_tiet.fTien_TongDuToanDuocGiao,
				chi_tiet.fTien_ThucChi,
				chi_tiet.fTienThua,
				chi_tiet.fTienThieu 
			FROM
				#tblMucLucNganSach AS mucluc
				LEFT JOIN (
				SELECT
					SUM(ctct.fTienTuChi)  fTienTuChi,
					ctct.sXauNoiMa
				FROM
					BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
					JOIN BH_DTC_DuToanChiTrenGiao ct ON ctct.iID_DTC_DuToanChiTrenGiao = ct.ID 
				WHERE
					ctct.iID_MaDonVi = @MaDonVi 
					AND ct.iNamLamViec = @INamLamViec 
					AND BIsKhoa = 1 
					group by ctct.sXauNoiMa
				) dt ON dt.sXauNoiMa = mucluc.sXauNoiMa
				LEFT JOIN (
				SELECT
					sum(ctct.fTienTuChi) fTienTuChi,
					ctct.sXauNoiMa
				FROM
					BH_DTC_PhanBoDuToanChi_ChiTiet ctct
					JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID 
				WHERE
					ctct.iID_MaDonVi = @MaDonVi 
					AND ct.iNamChungTu = @INamLamViec - 1 
					AND BIsKhoa = 1 
					group by ctct.sXauNoiMa
				) dtTruoc ON dt.sXauNoiMa = mucluc.sXauNoiMa
				LEFT JOIN (
				SELECT
					ctct.* 
				FROM
					BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet ctct
					JOIN BH_QTC_Nam_KCB_QuanYDonVi ct ON ctct.iID_QTC_Nam_KCB_QuanYDonVi = ct.ID_QTC_Nam_KCB_QuanYDonVi 
				WHERE
					ctct.iID_MaDonVi = @MaDonVi 
					AND ct.iNamLamViec = @INamLamViec - 1 
					AND BIsKhoa = 1 
				) qtTruoc ON qtTruoc.sXauNoiMa = mucluc.sXauNoiMa
				LEFT JOIN #tblQuyetToanNamChiTiet AS chi_tiet ON mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach 
			ORDER BY
				mucluc.sXauNoiMa DROP TABLE #tblMucLucNganSach;
			DROP TABLE #tblQuyetToanNamChiTiet;
			
		END;
	;
;
;
GO
