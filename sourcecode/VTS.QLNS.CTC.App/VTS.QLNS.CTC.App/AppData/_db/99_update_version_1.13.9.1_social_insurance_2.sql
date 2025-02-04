/****** Object:  StoredProcedure [dbo].[sp_qtc_nkqk_chitiet_export_excel_bh]    Script Date: 1/22/2024 11:13:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_nkqk_chitiet_export_excel_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_nkqk_chitiet_export_excel_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]    Script Date: 1/22/2024 11:13:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]    Script Date: 1/22/2024 11:13:43 AM ******/
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
			qtcn_ct.iSoLuyKeCuoiQuyNay,
			qtcn_ct.fTienLuyKeCuoiQuyNay,
			qtcn_ct.iSoSQ_DeNghi,
			qtcn_ct.fTienSQ_DeNghi,
			qtcn_ct.iSoQNCN_DeNghi,
			qtcn_ct.fTienQNCN_DeNghi,
			qtcn_ct.iSoCNVCQP_DeNghi,
			qtcn_ct.fTienCNVCQP_DeNghi,
			qtcn_ct.iSoHSQBS_DeNghi,
			qtcn_ct.fTienHSQBS_DeNghi,
			qtcn_ct.iTongSo_DeNghi,
			qtcn_ct.fTongTien_DeNghi,
			qtcn_ct.fTongTien_PheDuyet,
			qtcn_ct.iSoLDHD_DeNghi,
			qtcn_ct.fTienLDHD_DeNghi,
			qtcn.iID_MaDonVi iIDMaDonVi,
			qtcn.iNamChungTu iNamLamViec
		into #tblQuyetToanQuyChiTiet
		from BH_QTC_Quy_CheDoBHXH_ChiTiet as qtcn_ct
		inner join BH_QTC_Quy_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Quy_CheDoBHXH = qtcn.ID_QTC_Quy_CheDoBHXH
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
			chi_tiet.iSoLuyKeCuoiQuyNay,
			chi_tiet.fTienLuyKeCuoiQuyNay,
			chi_tiet.iSoLuyKeCuoiQuyNay iSoLuyKeCuoiQuyNayBefore,
			chi_tiet.fTienLuyKeCuoiQuyNay fTienLuyKeCuoiKyBefore ,
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
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_nkqk_chitiet_export_excel_bh]    Script Date: 1/22/2024 11:13:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qtc_nkqk_chitiet_export_excel_bh]
	 @NamLamViec int,
	 @iD uniqueidentifier,
	 @IdDonVi nvarchar(max),
	 @LNS nvarchar(max)
AS
BEGIN 
	SET NOCOUNT ON;
	DECLARE @iDCT uniqueidentifier='00000000-0000-0000-0000-000000000000';
	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha,sDuToanChiTietToi
			into #tblMlnsByPhanCap
	FROM BH_DM_MucLucNganSach 
	WHERE 
		iNamLamViec = @NamLamViec 
		AND iTrangThai = 1
		--AND (
		--	(@PhanCap = 'M' AND (sTM IS NULL OR sTM = ''))
		--	OR (@PhanCap = 'TM' AND (sTTM IS NULL OR sTTM = ''))
		--	OR (@PhanCap = 'NG' AND (sTNG IS NULL OR sTNG = ''))
		--	)
		AND sLNS IN (SELECT * FROM f_split(@LNS))
		--(
		--			SELECT DISTINCT VALUE
		--			FROM 
		--			(
		--				SELECT 
		--					CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1, 
		--					CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3, 
		--					CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5, 
		--					CAST(sLNS AS nvarchar(10)) LNS 
		--				FROM
		--					NS_NguoiDung_LNS 
		--				WHERE 
		--					sMaNguoiDung = @UserName
		--					AND INamLamViec = @NamLamViec
		--					AND sLNS IN (SELECT * FROM f_split(@LNS))
		--			) LNS
		--			UNPIVOT
		--			(
		--				value
		--				FOR col in (LNS1, LNS3, LNS5, LNS)
		--			) un) 

SELECT   isnull(ctct.ID_QTC_Nam_KPK_ChiTiet, @iDCT) as ID_QTC_Nam_KPK_ChiTiet,
		@iD as iID_QTC_Nam_KPK,
		mlns.iID_MLNS as iID_MucLucNganSach,
		mlns.iID_MLNS_Cha as IdParent,
		mlns.sXauNoiMa as sXauNoiMa,
		mlns.sLNS as SLNS,
		mlns.sL as SL,
		mlns.sK as SK,
		mlns.sM as SM,
		mlns.sTM as STM,
		mlns.sTTM as STTM,
		mlns.sNG as SNG,
		mlns.sTNG as STNG,
		mlns.sMoTa as SNoiDung,
		mlns.sDuToanChiTietToi,
		@NamLamViec AS INamLamViec,
		mlns.bHangCha as IsHangCha,
		isnull(ctct.dNgayTao, getdate()) AS dNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS dNgaySua,
		ctct.sNguoiTao AS sNguoiTao,
		ctct.sNguoiSua AS sNguoiSua,
		ISNULL(ctct.fTien_DuToanNamTruocChuyenSang, 0)  as fTien_DuToanNamTruocChuyenSang, 
		ISNULL(ctct.fTien_DuToanGiaoNamNay, 0)  as fTien_DuToanGiaoNamNay,
		ISNULL(ctct.fTien_TongDuToanDuocGiao, 0)  as fTien_TongDuToanDuocGiao, 
		ISNULL(ctct.fTien_ThucChi, 0)  as fTien_ThucChi, 
		ISNULL(ctct.fTienThua, 0)  as fTienThua, 
		ISNULL(ctct.fTienThieu, 0)  as fTienThieu, 
		ISNULL(ctct.fTiLeThucHienTrenDuToan, 0)  as fTiLeThucHienTrenDuToan
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(SELECT * FROM BH_QTC_Nam_KPK_ChiTiet 
				WHERE iID_QTC_Nam_KPK in
					( SELECT iID_QTC_Nam_KPK FROM BH_QTC_Nam_KPK
								WHERE iNamLamViec=@NamLamViec
								AND iID_QTC_Nam_KPK=@iD
								AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
								)) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
	Order by mlns.sXauNoiMa
END
;
;
;
GO
