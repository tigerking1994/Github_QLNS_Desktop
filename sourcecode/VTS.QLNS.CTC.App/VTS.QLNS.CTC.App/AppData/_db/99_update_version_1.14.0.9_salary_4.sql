/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 3/5/2024 6:06:34 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bangluong_thang_dulieu_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bangluong_thang_dulieu_insert]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_nkqk_chitiet_bh]    Script Date: 3/5/2024 6:06:34 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_nkqk_chitiet_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_nkqk_chitiet_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]    Script Date: 3/5/2024 6:06:34 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_nkp_ql_chitiet]    Script Date: 3/5/2024 6:06:34 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtc_nkp_ql_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtc_nkp_ql_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_nkp_ql_chitiet]    Script Date: 3/5/2024 6:06:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chi tiet chung tu 
CREATE PROCEDURE [dbo].[sp_bh_qtc_nkp_ql_chitiet]
	 @NamLamViec int,
	 @iD uniqueidentifier,
	 @IdDonVi nvarchar(max),
	 @LNS nvarchar(max)
AS
BEGIN 
	SET NOCOUNT ON;
	DECLARE @iDCT uniqueidentifier='00000000-0000-0000-0000-000000000000';

	DECLARE @fSoThamDinh INT;
	SELECT @fSoThamDinh = fSoThamDinh
	FROM BH_ThamDinhQuyetToan_ChungTuChiTiet 
	WHERE iNamLamViec = @NamLamViec - 1 and iMa = 254

	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha, sDuToanChiTietToi
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

SELECT   isnull(ctct.ID_QTC_Nam_KinhPhiQuanLy_ChiTiet, @iDCT) as ID_QTC_Nam_KinhPhiQuanLy_ChiTiet,
		@iD as IID_QTC_Nam_KinhPhiQuanLy,
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
		mlns.sDuToanChiTietToi as SDuToanChiTietToi,
		ctct.iID_MaDonVi as IIdMaDonVi,
		ddv.sTenDonVi as STenDonVi,
		@NamLamViec AS INamLamViec,
		mlns.bHangCha as IsHangCha,
		isnull(ctct.dNgayTao, getdate()) AS dNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS dNgaySua,
		ctct.sNguoiTao AS sNguoiTao,
		ctct.sNguoiSua AS sNguoiSua,
		CASE WHEN mlns.sXauNoiMa = @LNS THEN @fSoThamDinh ELSE 0 END as fDuToanNamTruocChuyenSang,
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
		(SELECT * FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet 
				WHERE iID_QTC_Nam_KinhPhiQuanLy in
					( SELECT ID_QTC_Nam_KinhPhiQuanLy FROM BH_QTC_Nam_KinhPhiQuanLy
								WHERE iNamLamViec=@NamLamViec
								AND ID_QTC_Nam_KinhPhiQuanLy=@iD
								AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
								)) ctct
	LEFT JOIN 
		(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON ctct.iID_MaDonVi = ddv.iID_MaDonVi
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
	Order by mlns.sXauNoiMa
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]    Script Date: 3/5/2024 6:06:34 PM ******/
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
					isnull(dt.fTienTuChi, 0) + isnull(dt.fTienHienVat, 0)
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
				SUM(isnull(dt.fTienTuChi, 0) + isnull(dt.fTienHienVat, 0)) fTien_DuToanGiaoNamNay,
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
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_nkqk_chitiet_bh]    Script Date: 3/5/2024 6:06:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chi tiet chung tu 
CREATE PROCEDURE [dbo].[sp_qtc_nkqk_chitiet_bh]
	 @NamLamViec int,
	 @iD uniqueidentifier,
	 @IdDonVi nvarchar(max),
	 @LNS nvarchar(max)
AS
BEGIN 
	SET NOCOUNT ON;
	DECLARE @iDCT uniqueidentifier='00000000-0000-0000-0000-000000000000';

	DECLARE @fSoThamDinh INT;
	SELECT @fSoThamDinh = fSoThamDinh
	FROM BH_ThamDinhQuyetToan_ChungTuChiTiet 
	WHERE iNamLamViec = @NamLamViec - 1 and iMa = 240

	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha, sDuToanChiTietToi
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
		mlns.sDuToanChiTietToi as SDuToanChiTietToi,
		@NamLamViec AS INamLamViec,
		mlns.bHangCha as IsHangCha,
		isnull(ctct.dNgayTao, getdate()) AS dNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS dNgaySua,
		ctct.sNguoiTao AS sNguoiTao,
		ctct.sNguoiSua AS sNguoiSua,
		ctct.iID_MaDonVi as IIdMaDonVi,
		CASE WHEN mlns.sXauNoiMa = @LNS THEN @fSoThamDinh ELSE 0 END as fDuToanNamTruocChuyenSang,
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
					( SELECT ID_QTC_Nam_KPK FROM BH_QTC_Nam_KPK
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 3/5/2024 6:06:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_bangluong_thang_dulieu_insert]
@Thang int,
@Nam int,
@MaDonVi NVARCHAR(MAX),
@MaCachTl NVARCHAR(50),
@SoNgay int
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

	select MA_CBO, gia_tri, HuongPC_SN, ma_phucap
	into #canBoPhuCap
	from TL_CanBo_PhuCap where MA_CBO like CONCAT(FORMAT(@Nam, 'D2'), FORMAT(@Thang, 'D2'), '%')

	SELECT
	MA_CBO MaCanBo,
	(CASE WHEN pc.Parent = 'TIENAN' THEN 'TA_TT'
	WHEN pc.Parent = 'TIENAN2' THEN 'TA_TT2' ELSE '' END) AS PARENT,
	SUM (
		--CASE WHEN PARENT <> N'TIENAN' THEN 0
		--WHEN canBoPhuCap.MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN ISNULL(@SoNgay, 0) * canBoPhuCap.GIA_TRI
		--ELSE ISNULL(canBoPhuCap.HuongPC_SN, 0) * canBoPhuCap.GIA_TRI
		--END
		CASE WHEN PARENT <> N'TIENAN' THEN 0
		WHEN ISNULL(canBoPhuCap.HuongPC_SN, 0) <> 0 AND bHuongPc_Sn = 1 THEN ISNULL(canBoPhuCap.HuongPC_SN, 0) * canBoPhuCap.GIA_TRI
		ELSE ISNULL(@SoNgay, 0) * canBoPhuCap.GIA_TRI
		END
	) GiaTriTA1,
	SUM (
		--CASE WHEN PARENT <> N'TIENAN2' THEN 0
		--WHEN canBoPhuCap.MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN ISNULL(@SoNgay, 0) * canBoPhuCap.GIA_TRI
		--ELSE ISNULL(canBoPhuCap.HuongPC_SN, 0) * canBoPhuCap.GIA_TRI
		--END
		CASE WHEN PARENT <> N'TIENAN2' THEN 0
		WHEN ISNULL(canBoPhuCap.HuongPC_SN, 0) <> 0 AND bHuongPc_Sn = 1 THEN ISNULL(canBoPhuCap.HuongPC_SN, 0) * canBoPhuCap.GIA_TRI
		ELSE ISNULL(@SoNgay, 0) * canBoPhuCap.GIA_TRI
		END
	) GiaTriTA2
	INTO #SoLieuTienAn
	FROM #canBoPhuCap canBoPhuCap
	INNER JOIN TL_DM_PhuCap as pc on canBoPhuCap.MA_PHUCAP = pc.Ma_PhuCap
	--WHERE
	--canBoPhuCap.MA_PHUCAP IN ('TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4', 'TA_DOCHAI_DG', 'TA_OM_DG', 'TA_BB_DG', 'TA_TRUCTRAI_DG')
	WHERE
	pc.PARENT IN ('TIENAN', 'TIENAN2') and canBoPhuCap.Gia_Tri <> 0 and canBoPhuCap.MA_CBO like CONCAT(FORMAT(@Nam, 'D2'), FORMAT(@Thang, 'D2'), '%')
	GROUP BY canBoPhuCap.MA_CBO, pc.PARENT


	SELECT
	canBo.Ma_CanBo AS MaCanBo,
	canBo.Ten_CanBo AS TenCanBo,
	donVi.Ma_DonVi MaDonVi,
	canBo.Ma_Hieu_CanBo AS MaHieuCanBo,
	capBac.Ma_Cb MaCapBac,
	canBo.BHTN,
	canBo.PCCV,
	canBo.BNuocNgoai,
	canBo.Ngay_XN as NgayXn,
	canBo.Ma_TangGiam
	INTO #ThongTinCanBo
	FROM TL_DM_CanBo canBo
	INNER JOIN TL_DM_DonVi donVi
	ON canBo.Parent = donVi.Ma_DonVi
	INNER JOIN TL_DM_CapBac capBac
	ON canBo.Ma_CB = capBac.Ma_Cb
	WHERE
	canBo.Thang = @Thang
	AND canBo.Nam = @Nam
	AND canBo.Parent IN (SELECT * FROM f_split(@MaDonVi))
	AND (canBo.IsDelete = 1 or (canbo.Ma_TangGiam = '320' and month(canbo.Ngay_XN) <= @thang and year(canbo.Ngay_XN) = @Nam))
	AND canBo.Khong_Luong <> 1


SELECT
	newid() AS Id,
	@Thang AS Thang,
	@Nam AS Nam,
	canBo.MaCanBo AS MaCbo,
	canBo.TenCanBo AS TenCbo,
	canBo.MaDonVi AS MaDonVi,
	canBo.BNuocNgoai ,
	@MaCachTl AS MaCachTl,
	canBoPhuCap.MA_PHUCAP AS MaPhuCap,
	canBo.Ma_TangGiam as MaTangGiam,
	CASE
	WHEN canBoPhuCap.MA_PHUCAP = 'TCVIECLAM_TT' AND cb.Parent in ('1', '2', '3') THEN 0
	WHEN canBoPhuCap.MA_PHUCAP = 'THANG_TCVIECLAM' AND canbo.NgayXn is null THEN 0
	WHEN canBoPhuCap.MA_PHUCAP = 'THANG_TCVIECLAM' and canbo.NgayXn is not null and year(canbo.NgayXn) <> @NAM THEN 0
	WHEN canBoPhuCap.MA_PHUCAP = 'THANG_TCVIECLAM' and canbo.NgayXn is not null and year(canbo.NgayXn) = @NAM and month(canbo.NgayXn) <> @Thang THEN 0
	WHEN canBoPhuCap.MA_PHUCAP = 'THANG_TCXN' AND canbo.NgayXn is null THEN 0
	WHEN canBoPhuCap.MA_PHUCAP = 'THANG_TCXN' and canbo.NgayXn is not null and year(canbo.NgayXn) <> @NAM THEN 0
	WHEN canBoPhuCap.MA_PHUCAP = 'THANG_TCXN' and canbo.NgayXn is not null and year(canbo.NgayXn) = @NAM and month(canbo.NgayXn) <> @Thang THEN 0
	WHEN (canBoPhuCap.MA_PHUCAP = 'NTN' AND canBoPhuCap.GIA_TRI < 5) OR (canBoPhuCap.MA_PHUCAP = 'BHTNCN_HS' AND canBo.BHTN = 0) OR (canBoPhuCap.MA_PHUCAP = 'PCCOV_HS' AND canBo.PCCV = 0) THEN 0
	WHEN canBoPhuCap.MA_PHUCAP = 'TA_TT' THEN soLieuTienAn.GiaTriTA1
	WHEN canBoPhuCap.MA_PHUCAP = 'TA_TT2' THEN soLieuTienAn.GiaTriTA2
	WHEN canBoPhuCap.MA_PHUCAP = 'TRICHLUONG_TT' THEN (canBoPhuCap.GIA_TRI / 1000) * 1000
	ELSE canBoPhuCap.GIA_TRI
	END AS GiaTri,
	canBo.MaHieuCanBo AS MaHieuCanBo,
	canBo.MaCapBac AS MaCb,
	canBoPhuCap.HuongPC_SN AS HuongPcSn,
	cachTinhLuong.CongThuc AS CongThuc,
	CASE WHEN cachTinhLuong.Id IS NULL THEN 1 ELSE 0 END AS IsCalculated
	--,canBoPhuCap.bCapNhat as IsCapNhat
FROM #canBoPhuCap canBoPhuCap
	INNER JOIN #ThongTinCanBo canBo
	ON canBo.MaCanBo = canBoPhuCap.MA_CBO
	LEFT JOIN #SoLieuTienAn soLieuTienAn
	ON canBoPhuCap.MA_CBO = soLieuTienAn.MaCanBo AND canBoPhuCap.MA_PHUCAP = soLieuTienAn.Parent
	LEFT JOIN (select Id, Ma_Cot, CongThuc from TL_DM_Cach_TinhLuong_Chuan) cachTinhLuong
	ON cachTinhLuong.Ma_Cot = canBoPhuCap.MA_PHUCAP
	left join TL_DM_CapBac cb on canBo.MaCapBac = cb.Ma_Cb

	--DELETE blt
	--FROM TL_BangLuong_Thang blt
	--INNER JOIN #ThongTinCanBo ttcb ON blt.Ma_CBo = ttcb.MaCanBo
	--INNER JOIN TL_CanBo_PhuCap cbpc ON cbpc.MA_CBO = blt.Ma_CBo
	--WHERE bCapNhat = 1

	--UPDATE cbpc
	--SET bCapNhat = 0
	--FROM TL_CanBo_PhuCap cbpc
	--INNER JOIN #ThongTinCanBo ttcb ON cbpc.MA_CBO = ttcb.MaCanBo

drop table #SoLieuTienAn
drop table #ThongTinCanBo
drop table #canBoPhuCap

END
;
GO
