/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]    Script Date: 3/19/2024 3:58:40 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]    Script Date: 3/19/2024 3:58:40 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]    Script Date: 3/19/2024 3:58:40 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]    Script Date: 3/19/2024 3:58:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@SLN nvarchar(max),
@IsTongHop bit,
@IQuy int,
@Donvitinh int
as
begin
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
			danhmuc.sDuToanChiTietToi,
			danhmuc.bHangCha
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach as danhmuc
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs  in (select * from dbo.splitstring(@SLN)) 
		
		
		
	---Lấy thông tin chi tiết chứng từ
		select 
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sXauNoiMa,
			ROUND(Sum(qtcn_ct.fTienDuToanDuyet),0) as fTienDuToanDuyet,
			Sum(qtcn_ct.iSoLuyKeCuoiQuyNay) as iSoLuyKeCuoiQuyNay,
			ROUND(Sum(qtcn_ct.fTienLuyKeCuoiQuyNay),0) as fTienLuyKeCuoiQuyNay,
			sum(qtcn_ct.iSoSQ_DeNghi) as iSoSQ_DeNghi ,
			ROUND(sum(qtcn_ct.fTienSQ_DeNghi),0) as fTienSQ_DeNghi ,
			sum(qtcn_ct.iSoQNCN_DeNghi) as iSoQNCN_DeNghi,
			ROUND(sum(qtcn_ct.fTienQNCN_DeNghi),0) as fTienQNCN_DeNghi ,
			sum(qtcn_ct.iSoCNVCQP_DeNghi) as iSoCNVCQP_DeNghi ,
			ROUND(sum(qtcn_ct.fTienCNVCQP_DeNghi),0) as fTienCNVCQP_DeNghi,
			sum(qtcn_ct.iSoHSQBS_DeNghi) as iSoHSQBS_DeNghi,
			ROUND(sum(qtcn_ct.fTienHSQBS_DeNghi),0) as fTienHSQBS_DeNghi,
			sum(qtcn_ct.iTongSo_DeNghi) as iTongSo_DeNghi,
			ROUND(sum(qtcn_ct.fTongTien_DeNghi),0) as fTongTien_DeNghi,
			ROUND(sum(qtcn_ct.fTongTien_PheDuyet),0) as fTongTien_PheDuyet,
			sum(qtcn_ct.iSoLDHD_DeNghi) as iSoLDHD_DeNghi,
			ROUND(sum(qtcn_ct.fTienLDHD_DeNghi),0) as fTienLDHD_DeNghi
		into #tblQuyetToanQuyChiTiet
		from BH_QTC_Quy_CheDoBHXH_ChiTiet as qtcn_ct
		inner join BH_QTC_Quy_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Quy_CheDoBHXH = qtcn.ID_QTC_Quy_CheDoBHXH
		where qtcn.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
		and qtcn.iNamChungTu = @INamLamViec
		--and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
		and qtcn.iQuyChungTu = @IQuy
		group by qtcn_ct.iID_MucLucNganSach,qtcn_ct.sXauNoiMa

		--- Get tien du toan 
		SELECT ctct.sXauNoiMa,
			SUM(ctct.fTienTuChi)  fTienDuToanDuyet
			into #tempDuToan
			FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct 
			JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID 
				WHERE ctct.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
				AND BIsKhoa = 1
				AND ct.iNamChungTu = @INamLamViec
				GROUP BY ctct.sXauNoiMa
	--- lay luy kê quy truoc
	SELECT SUM
			(isnull(fTienCNVCQP_DeNghi, 0)) fTienCNVCQP_DeNghi,
			SUM (isnull(fTienHSQBS_DeNghi, 0)) fTienHSQBS_DeNghi,
			SUM (isnull(fTienLDHD_DeNghi, 0)) fTienLDHD_DeNghi,
			SUM (isnull(fTienQNCN_DeNghi, 0)) fTienQNCN_DeNghi,
			SUM (isnull(fTienSQ_DeNghi, 0)) fTienSQ_DeNghi,
			SUM (isnull(iSoCNVCQP_DeNghi, 0)) iSoCNVCQP_DeNghi,
			SUM (isnull(ctct.fTongTien_PheDuyet, 0)) fTongTien_PheDuyet,
			SUM (isnull(iSoHSQBS_DeNghi, 0)) iSoHSQBS_DeNghi,
			SUM (isnull(iSoLDHD_DeNghi, 0)) iSoLDHD_DeNghi,
			SUM (isnull(iSoQNCN_DeNghi, 0)) iSoQNCN_DeNghi,
			SUM (isnull(iSoSQ_DeNghi, 0)) iSoSQ_DeNghi,
			ct.iID_MaDonVi,
			ct.iNamChungTu,
			ctct.sXauNoiMa
			into #tempDuLieuQuyTruoc
		FROM
			BH_QTC_Quy_CheDoBHXH_ChiTiet ctct
			INNER JOIN BH_QTC_Quy_CheDoBHXH ct ON ctct.iID_QTC_Quy_CheDoBHXH = ct.ID_QTC_Quy_CheDoBHXH 
		WHERE
			ct.iQuyChungTu < @IQuy 
			and ct.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
			and ct.iNamChungTu=@INamLamViec
		GROUP BY
			ct.iID_MaDonVi,
			ct.iNamChungTu,
			ctct.sXauNoiMa

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
			mucluc.sDuToanChiTietToi,
			mucluc.bHangCha,
			mucluc.iID_MLNS as iID_MucLucNganSach,
			mucluc.sMoTa as sLoaiTroCap,
			duToan.fTienDuToanDuyet,
			(
			isnull(duLieuQuyTruoc.fTienCNVCQP_DeNghi, 0) + isnull(duLieuQuyTruoc.fTienHSQBS_DeNghi, 0) + isnull(duLieuQuyTruoc.fTienLDHD_DeNghi, 0) + isnull(duLieuQuyTruoc.fTienQNCN_DeNghi, 0) + isnull(duLieuQuyTruoc.fTienSQ_DeNghi, 0) 
			) fTienLuyKeCuoiQuyTruoc,
			(
				isnull(duLieuQuyTruoc.iSoCNVCQP_DeNghi, 0) + isnull(duLieuQuyTruoc.iSoHSQBS_DeNghi, 0) + isnull(duLieuQuyTruoc.iSoLDHD_DeNghi, 0) + isnull(duLieuQuyTruoc.iSoQNCN_DeNghi, 0) + isnull(duLieuQuyTruoc.iSoSQ_DeNghi, 0) 
			) iSoLuyKeCuoiQuyTruoc,
			chi_tiet.iSoSQ_DeNghi,
			chi_tiet.fTienSQ_DeNghi,
			chi_tiet.iSoQNCN_DeNghi,
			chi_tiet.fTienQNCN_DeNghi,
			chi_tiet.iSoCNVCQP_DeNghi,
			chi_tiet.fTienCNVCQP_DeNghi,
			chi_tiet.iSoHSQBS_DeNghi,
			chi_tiet.fTienHSQBS_DeNghi,
			chi_tiet.iTongSo_DeNghi,
			chi_tiet.fTongTien_DeNghi,
			chi_tiet.fTongTien_PheDuyet,
			chi_tiet.iSoLDHD_DeNghi,
			chi_tiet.fTienLDHD_DeNghi

		from #tblMucLucNganSach as mucluc
		left join #tblQuyetToanQuyChiTiet as chi_tiet on mucluc.sXauNoiMa = chi_tiet.sXauNoiMa
		left join #tempDuToan duToan on mucluc.sXauNoiMa=duToan.sXauNoiMa
		left join #tempDuLieuQuyTruoc duLieuQuyTruoc on chi_tiet.sXauNoiMa=duLieuQuyTruoc.sXauNoiMa
		order by mucluc.sXauNoiMa
	
	drop table #tblMucLucNganSach;
	drop table #tblQuyetToanQuyChiTiet;
	drop table #tempDuToan;
	drop table #tempDuLieuQuyTruoc;

end
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]    Script Date: 3/19/2024 3:58:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet] @IdChungTu uniqueidentifier,
	@SLNS nvarchar (MAX),
	@INamLamViec INT,
	@MaDonVi nvarchar (100),
	@Loai BIT
	AS BEGIN
	DECLARE
		@quy INT;
	SELECT
		@quy = iquychungtu 
	FROM
		BH_QTC_Quy_CheDoBHXH 
	WHERE
		ID_QTC_Quy_CheDoBHXH = @IdChungTu;
---Lấy danh sách mục lục ngân sách
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
		AND danhmuc.sLNs IN (SELECT * FROM f_split (@SLNS)) ---Lấy thông tin chi tiết chứng từ
	SELECT
		qtcn_ct.ID_QTC_Quy_CheDoBHXH_ChiTiet,
		qtcn_ct.iID_MucLucNganSach,
		qtcn_ct.sLoaiTroCap,
		qtcn_ct.fTienDuToanDuyet,
		(
			isnull(qtcn_ct_truoc.fTienCNVCQP_DeNghi, 0) + isnull(qtcn_ct_truoc.fTienHSQBS_DeNghi, 0) + isnull(qtcn_ct_truoc.fTienLDHD_DeNghi, 0) + isnull(qtcn_ct_truoc.fTienQNCN_DeNghi, 0) + isnull(qtcn_ct_truoc.fTienSQ_DeNghi, 0) 
		) fTienLuyKeCuoiQuyTruoc,
		(
			isnull(qtcn_ct_truoc.iSoCNVCQP_DeNghi, 0) + isnull(qtcn_ct_truoc.iSoHSQBS_DeNghi, 0) + isnull(qtcn_ct_truoc.iSoLDHD_DeNghi, 0) + isnull(qtcn_ct_truoc.iSoQNCN_DeNghi, 0) + isnull(qtcn_ct_truoc.iSoSQ_DeNghi, 0) 
		) iSoLuyKeCuoiQuyTruoc,
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
		qtcn_ct.fTongTien_PheDuyet,
		qtcn_ct.iSoLDHD_DeNghi,
		qtcn_ct.fTienLDHD_DeNghi,
		qtcn.iID_MaDonVi iIDMaDonVi,
		qtcn.iNamChungTu iNamLamViec INTO #tblQuyetToanQuyChiTiet 
	FROM
		BH_QTC_Quy_CheDoBHXH_ChiTiet AS qtcn_ct
		INNER JOIN BH_QTC_Quy_CheDoBHXH AS qtcn ON qtcn_ct.iID_QTC_Quy_CheDoBHXH = qtcn.ID_QTC_Quy_CheDoBHXH
		LEFT JOIN (
		SELECT SUM
			(isnull(fTienCNVCQP_DeNghi, 0)) fTienCNVCQP_DeNghi,
			SUM (isnull(fTienHSQBS_DeNghi, 0)) fTienHSQBS_DeNghi,
			SUM (isnull(fTienLDHD_DeNghi, 0)) fTienLDHD_DeNghi,
			SUM (isnull(fTienQNCN_DeNghi, 0)) fTienQNCN_DeNghi,
			SUM (isnull(fTienSQ_DeNghi, 0)) fTienSQ_DeNghi,
			SUM (isnull(iSoCNVCQP_DeNghi, 0)) iSoCNVCQP_DeNghi,
			SUM (isnull(ctct.fTongTien_PheDuyet, 0)) fTongTien_PheDuyet,
			SUM (isnull(iSoHSQBS_DeNghi, 0)) iSoHSQBS_DeNghi,
			SUM (isnull(iSoLDHD_DeNghi, 0)) iSoLDHD_DeNghi,
			SUM (isnull(iSoQNCN_DeNghi, 0)) iSoQNCN_DeNghi,
			SUM (isnull(iSoSQ_DeNghi, 0)) iSoSQ_DeNghi,
			ct.iID_MaDonVi,
			ct.iNamChungTu,
			ctct.iID_MucLucNganSach
		FROM
			(
				SELECT fTienCNVCQP_DeNghi, fTienHSQBS_DeNghi, fTienLDHD_DeNghi, fTienQNCN_DeNghi, fTienSQ_DeNghi, iSoCNVCQP_DeNghi, fTongTien_PheDuyet, iSoHSQBS_DeNghi,
					iSoLDHD_DeNghi,  iSoQNCN_DeNghi, iSoSQ_DeNghi, iID_QTC_Quy_CheDoBHXH, iID_MucLucNganSach
				FROM BH_QTC_Quy_CheDoBHXH_ChiTiet ct
				INNER JOIN BH_DM_MucLucNganSach dmns on ct.sXauNoiMa = dmns.sXauNoiMa
				where dmns.bHangCha = 0 and dmns.iNamLamViec = @INamLamViec
			)ctct
			INNER JOIN BH_QTC_Quy_CheDoBHXH ct ON ctct.iID_QTC_Quy_CheDoBHXH = ct.ID_QTC_Quy_CheDoBHXH 
		WHERE
			ct.iQuyChungTu < @quy 
		GROUP BY
			ct.iID_MaDonVi,
			ct.iNamChungTu,
			ctct.iID_MucLucNganSach
		) qtcn_ct_truoc ON qtcn.iID_MaDonVi = qtcn_ct_truoc.iID_MaDonVi 
		AND qtcn.iNamChungTu = qtcn_ct_truoc.iNamChungTu 
		AND qtcn_ct.iID_MucLucNganSach = qtcn_ct_truoc.iID_MucLucNganSach
	WHERE
		qtcn_ct.iID_QTC_Quy_CheDoBHXH = @IdChungTu 
		AND qtcn.iNamChungTu=@INamLamViec;
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
		mucluc.sDuToanChiTietToi,
		chi_tiet.ID_QTC_Quy_CheDoBHXH_ChiTiet,
		mucluc.iID_MLNS AS iID_MucLucNganSach,
		mucluc.sMoTa AS sLoaiTroCap,
		(isnull(dt.fTienTuChi, 0)) fTienDuToanDuyet,
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
		chi_tiet.fTongTien_PheDuyet,
		chi_tiet.iSoLDHD_DeNghi,
		chi_tiet.fTienLDHD_DeNghi,
		chi_tiet.iIDMaDonVi,
		chi_tiet.iNamLamViec 
	FROM
		#tblMucLucNganSach AS mucluc
		LEFT JOIN (
			SELECT ctct.sXauNoiMa, SUM(ctct.fTienTuChi)  fTienTuChi
			FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct 
			JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID 
				WHERE ctct.iID_MaDonVi = @MaDonVi 
				AND BIsKhoa = 1
				AND ct.iNamChungTu = @INamLamViec
				GROUP BY ctct.sXauNoiMa) dt ON dt.sXauNoiMa = mucluc.sXauNoiMa
		LEFT JOIN #tblQuyetToanQuyChiTiet AS chi_tiet ON mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach 
	ORDER BY
		mucluc.sXauNoiMa ELSE SELECT
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
		mucluc.iID_MLNS AS iID_MucLucNganSach,
		mucluc.sMoTa AS sLoaiTroCap,
		(isnull(dt.fTienTuChi, 0) ) fTienDuToanDuyet,
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
		chi_tiet.fTongTien_PheDuyet,
		chi_tiet.iSoLDHD_DeNghi,
		chi_tiet.fTienLDHD_DeNghi,
		chi_tiet.iIDMaDonVi,
		chi_tiet.iNamLamViec 
	FROM
		#tblMucLucNganSach AS mucluc
		LEFT JOIN (
					SELECT ctct.sXauNoiMa,
							SUM(ctct.fTienTuChi) fTienTuChi
							FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct 
							JOIN BH_DTC_DuToanChiTrenGiao ct ON ctct.iID_DTC_DuToanChiTrenGiao = ct.ID 
		WHERE ct.iID_MaDonVi = @MaDonVi
		AND BIsKhoa = 1
		AND ct.iNamLamViec = @INamLamViec
		GROUP BY ctct.sXauNoiMa) dt ON dt.sXauNoiMa = mucluc.sXauNoiMa
		LEFT JOIN #tblQuyetToanQuyChiTiet AS chi_tiet ON mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach 
	ORDER BY
		mucluc.sXauNoiMa DROP TABLE #tblMucLucNganSach;
	DROP TABLE #tblQuyetToanQuyChiTiet;
	
END;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]     Script Date: 3/19/2024 3:58:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_quyet_toan_chinamKCB_chitiet] @IdChungTu uniqueidentifier,
	@Lns nvarchar (MAX),
	@INamLamViec INT,
	@IsTongHop4Quy BIT,
	@MaDonVi nvarchar (100),
	@Loai BIT
AS BEGIN

	DECLARE @fSoThamDinh INT;
	SELECT @fSoThamDinh = fSoThamDinh
	FROM BH_ThamDinhQuyetToan_ChungTuChiTiet 
	WHERE iNamLamViec = @INamLamViec - 1 and iMa = 225

	---Lấy danh sách mục lục ngân sách
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
		AND danhmuc.iTrangThai= 1 
		
	---Lấy thông tin chi tiết chứng từ
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
		) AS fTienThieu 
		INTO #tblQuyetToanNamChiTiet 
		FROM
				BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet AS qtcn_ct
		INNER JOIN BH_QTC_Nam_KCB_QuanYDonVi AS qtcn ON qtcn_ct.iID_QTC_Nam_KCB_QuanYDonVi = qtcn.ID_QTC_Nam_KCB_QuanYDonVi 
		WHERE
				qtcn_ct.iID_QTC_Nam_KCB_QuanYDonVi = @IdChungTu;


			---Kết quả hiển thị trả về
			IF (@Loai = 1)
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
				CASE WHEN mucluc.sXauNoiMa = @Lns THEN @fSoThamDinh ELSE 0 END as fDuToanNamTruocChuyenSang,
				--SUM(
				--	isnull(dtTruoc.fTienTuChi, 0) + isnull(dtTruoc.fTienHienVat, 0) - isnull(qtTruoc.fTien_ThucChi, 0) - isnull(qtTruoc.fTien_ThucChi, 0) 
				--) fTien_DuToanNamTruocChuyenSang,
				SUM(
					isnull(dt.fTienTuChi, 0) 
				) fTien_DuToanGiaoNamNay,
				--chi_tiet.fTien_TongDuToanDuocGiao,
				chi_tiet.fTien_ThucChi,
				isnull(SUM(chi_tiet.fTienThua), 0) as fTienThua,
				isnull(SUM(chi_tiet.fTienThieu),0) as fTienThieu
			FROM
				#tblMucLucNganSach AS mucluc
				LEFT JOIN (
				SELECT
					ctct.* 
				FROM
					BH_DTC_PhanBoDuToanChi_ChiTiet ctct
					JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID 
				WHERE
					ctct.iID_MaDonVi = @MaDonVi 
					AND iNamLamViec = @INamLamViec 
					AND BIsKhoa = 1 
					AND ctct.sLNS IN (SELECT * FROM splitstring (@Lns)) 
				) dt ON dt.sXauNoiMa = mucluc.sXauNoiMa
				LEFT JOIN (
				SELECT
					ctct.* 
				FROM
					BH_DTC_PhanBoDuToanChi_ChiTiet ctct
					JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID 
				WHERE
					ctct.iID_MaDonVi = @MaDonVi 
					AND ct.iNamChungTu = @INamLamViec - 1 
					AND BIsKhoa = 1 
					AND ctct.sLNS IN (SELECT * FROM splitstring (@Lns))
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
			GROUP BY
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
				chi_tiet.ID_QTC_Nam_KCB_QuanYDonVi_ChiTiet,
				mucluc.iID_MLNS ,
				mucluc.sMoTa,
				chi_tiet.iID_MaDonVi,
				chi_tiet.iNamLamViec,
				chi_tiet.fTien_ThucChi
			ORDER BY
				mucluc.sXauNoiMa 
				
			ELSE ---Kết quả hiển thị trả về
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
				CASE WHEN mucluc.sXauNoiMa = @Lns THEN @fSoThamDinh ELSE 0 END as fDuToanNamTruocChuyenSang,
				--SUM(
				--	isnull(dtTruoc.fTienTuChi, 0) + isnull(dtTruoc.fTienHienVat, 0) - isnull(qtTruoc.fTien_ThucChi, 0) - isnull(qtTruoc.fTien_ThucChi, 0) 
				--) fTien_DuToanNamTruocChuyenSang,
				SUM(isnull(dt.fTienTuChi, 0)) fTien_DuToanGiaoNamNay,
				--chi_tiet.fTien_TongDuToanDuocGiao,
				chi_tiet.fTien_ThucChi,
				isnull(SUM(chi_tiet.fTienThua), 0) as fTienThua,
				isnull(SUM(chi_tiet.fTienThieu),0) as fTienThieu
			FROM
				#tblMucLucNganSach AS mucluc
				LEFT JOIN (
				SELECT
					ctct.* 
				FROM
					BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
					JOIN BH_DTC_DuToanChiTrenGiao ct ON ctct.iID_DTC_DuToanChiTrenGiao = ct.ID 
				WHERE
					ctct.iID_MaDonVi = @MaDonVi 
					AND ct.iNamLamViec = @INamLamViec 
					AND BIsKhoa = 1 
				) dt ON dt.sXauNoiMa = mucluc.sXauNoiMa
				LEFT JOIN (
				SELECT
					ctct.* 
				FROM
					BH_DTC_PhanBoDuToanChi_ChiTiet ctct
					JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID 
				WHERE
					ctct.iID_MaDonVi = @MaDonVi 
					AND ct.iNamChungTu = @INamLamViec - 1 
					AND BIsKhoa = 1 
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
			GROUP BY
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
				chi_tiet.ID_QTC_Nam_KCB_QuanYDonVi_ChiTiet,
				mucluc.iID_MLNS ,
				mucluc.sMoTa,
				chi_tiet.iID_MaDonVi,
				chi_tiet.iNamLamViec,
				chi_tiet.fTien_ThucChi
			ORDER BY
				mucluc.sXauNoiMa 
				
			DROP TABLE #tblMucLucNganSach;
			DROP TABLE #tblQuyetToanNamChiTiet;
			
		END;
	;
;
;
;
;
GO

