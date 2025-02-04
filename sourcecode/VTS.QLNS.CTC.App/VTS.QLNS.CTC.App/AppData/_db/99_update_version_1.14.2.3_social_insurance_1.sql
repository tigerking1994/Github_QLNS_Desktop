
if exists (select * from BH_DM_MucLucNganSach where sXauNoiMa = '9020002' and iNamLamViec = 2023)
update BH_DM_MucLucNganSach
set sMoTa = N'II. KHỐI HẠCH TOÁN'
where sXauNoiMa = '9020002' and iNamLamViec = 2023

/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nkpk_kehoach_bh]    Script Date: 3/27/2024 5:10:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_nkpk_kehoach_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_nkpk_kehoach_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_nkqk_chitiet_bh]    Script Date: 3/27/2024 5:10:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_nkqk_chitiet_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_nkqk_chitiet_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]    Script Date: 3/27/2024 5:10:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_nkp_ql_chitiet]    Script Date: 3/27/2024 5:10:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtc_nkp_ql_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtc_nkp_ql_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]    Script Date: 3/27/2024 5:41:29 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]    Script Date: 3/27/2024 5:41:29 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_khac_chungtu_chi_tiet]    Script Date: 3/28/2024 3:20:03 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_quykinhphi_khac_chungtu_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_quykinhphi_khac_chungtu_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet_clone]    Script Date: 3/28/2024 3:20:03 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]    Script Date: 3/28/2024 3:20:03 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_nkp_ql_chitiet]    Script Date: 3/27/2024 5:10:25 PM ******/
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
	 @LNS nvarchar(max),
	 @Loai int 
AS
BEGIN 
	SET NOCOUNT ON;
	DECLARE @iDCT uniqueidentifier='00000000-0000-0000-0000-000000000000';

	DECLARE @fSoThamDinh INT;
	SELECT @fSoThamDinh = fSoThamDinh
	FROM BH_ThamDinhQuyetToan_ChungTuChiTiet 
	WHERE iNamLamViec = @NamLamViec - 1 and iMa = 254 and iID_MaDonVi=@IdDonVi

	-- lấy ra đơn vị từ đơn vị của chứng từ
	SELECT iID_MaDonVi, iID_MaDonVi + ' - ' + sTenDonVi as sTenDonVi into #tblDonVi
	FROM DonVi 
	WHERE 
		INamLamViec = @NamLamViec 
		AND iTrangThai=1
		AND iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))

	-- lấy ra mlns theo phân cấp
	SELECT danhmuc.iID_MLNS, danhmuc.iID_MLNS_Cha, danhmuc.sXauNoiMa, danhmuc.sLNS, danhmuc.sL, danhmuc.sK, danhmuc.sM, danhmuc.sTM, danhmuc.sTTM, danhmuc.sNG, danhmuc.sTNG, danhmuc.sTNG1, danhmuc.sTNG2, danhmuc.sTNG3, danhmuc.sMoTa, danhmuc.bHangCha, danhmuc.sDuToanChiTietToi, dv.iID_MaDonVi,
		dv.sTenDonVi
			into #tblMlnsByPhanCap
	FROM BH_DM_MucLucNganSach AS danhmuc ,#tblDonVi dv
	WHERE 
		danhmuc.iNamLamViec = @NamLamViec 
		AND danhmuc.iTrangThai = 1
		AND danhmuc.sLNS IN (SELECT * FROM f_split(@LNS))
	-- Get data chung tu thuong
	IF @Loai=1
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
				mlns.iID_MaDonVi as IIdMaDonVi,
				mlns.sTenDonVi as STenDonVi,
				@NamLamViec AS INamLamViec,
				mlns.bHangCha as IsHangCha,
				isnull(ctct.dNgayTao, getdate()) AS dNgayTao,
				isnull(ctct.dNgaySua, getdate()) AS dNgaySua,
				ctct.sNguoiTao AS sNguoiTao,
				ctct.sNguoiSua AS sNguoiSua,
				CASE WHEN mlns.sXauNoiMa = @LNS THEN @fSoThamDinh ELSE 0 END as fDuToanNamTruocChuyenSang,
				--ISNULL(ctct.fTien_DuToanNamTruocChuyenSang, 0)  as fTien_DuToanNamTruocChuyenSang, 
				ISNULL(daDuToan.fTienDuToan, 0)  as fTien_DuToanGiaoNamNay,
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
										)) ctct on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON ctct.iID_MaDonVi = ddv.iID_MaDonVi
			LEFT JOIN (
					-- lấy ra dữ liệu dự toán
					SELECT 
						  SUM(cast(fTienTuChi as float)) AS fTienDuToan,
						  sXauNoiMa,
						  iID_MaDonVi
				   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
				   WHERE iID_DTC_PhanBoDuToanChi IN
					   (SELECT ID
						FROM BH_DTC_PhanBoDuToanChi
						WHERE sSoQuyetDinh <> ''
						  AND sSoQuyetDinh IS NOT NULL
						  AND iNamChungTu = @NamLamViec
						  AND bIsKhoa=1)
					 AND iID_MaDonVi in  (SELECT * FROM f_split(@IdDonVi))-- donvi
				   GROUP BY iID_MaDonVi, 
				   sXauNoiMa
				) daDuToan  on mlns.sXauNoiMa=daDuToan.sXauNoiMa
			Order by mlns.sXauNoiMa
	-- Get data chung tu tong hop
	ELSE
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
		mlns.iID_MaDonVi as IIdMaDonVi,
		mlns.sTenDonVi as STenDonVi,
		@NamLamViec AS INamLamViec,
		mlns.bHangCha as IsHangCha,
		isnull(ctct.dNgayTao, getdate()) AS dNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS dNgaySua,
		ctct.sNguoiTao AS sNguoiTao,
		ctct.sNguoiSua AS sNguoiSua,
		CASE WHEN mlns.sXauNoiMa = @LNS THEN @fSoThamDinh ELSE 0 END as fDuToanNamTruocChuyenSang,
		ISNULL(ctct.fTien_DuToanNamTruocChuyenSang, 0)  as fTien_DuToanNamTruocChuyenSang, 
		ISNULL(daDuToan.fTienDuToan, 0)  as fTien_DuToanGiaoNamNay,
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
								)) ctct on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
	LEFT JOIN 
		(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON ctct.iID_MaDonVi = ddv.iID_MaDonVi
	LEFT JOIN (
			SELECT 
				  SUM(cast(fTienTuChi as float)) AS fTienDuToan,
				  sXauNoiMa,
				  iID_MaDonVi
		   FROM BH_DTC_DuToanChiTrenGiao_ChiTiet
		   WHERE iID_DTC_DuToanChiTrenGiao IN
			   (SELECT ID
				FROM BH_DTC_DuToanChiTrenGiao
				WHERE sSoQuyetDinh <> ''
				  AND sSoQuyetDinh IS NOT NULL
				  AND iNamLamViec = @NamLamViec
				  AND bIsKhoa=1)
			 AND iID_MaDonVi in  (SELECT * FROM f_split(@IdDonVi))-- donvi
		   GROUP BY iID_MaDonVi, 
		   sXauNoiMa
		) daDuToan  on mlns.sXauNoiMa=daDuToan.sXauNoiMa
	Order by mlns.sXauNoiMa
END
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]    Script Date: 3/27/2024 5:10:25 PM ******/
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
	WHERE iNamLamViec = @INamLamViec - 1 and iMa = 225 and iID_MaDonVi=@MaDonVi

		-- lấy ra đơn vị từ đơn vị của chứng từ
	SELECT iID_MaDonVi, iID_MaDonVi + ' - ' + sTenDonVi as sTenDonVi into #tblDonVi
	FROM DonVi 
	WHERE 
		INamLamViec = @INamLamViec 
		AND iTrangThai=1
		AND iID_MaDonVi IN (SELECT * FROM f_split(@MaDonVi))


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
		danhmuc.sDuToanChiTietToi ,
		dv.iID_MaDonVi,
		dv.sTenDonVi
		INTO #tblMucLucNganSach 
	FROM
		BH_DM_MucLucNganSach AS danhmuc ,#tblDonVi dv
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
				mucluc.iID_MaDonVi,
				mucluc.sTenDonVi,
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
				mucluc.iID_MaDonVi,
				mucluc.sTenDonVi,
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
				mucluc.iID_MaDonVi,
				mucluc.sTenDonVi,
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
				mucluc.iID_MaDonVi,
				mucluc.sTenDonVi,
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_nkqk_chitiet_bh]    Script Date: 3/27/2024 5:10:25 PM ******/
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
	 @LNS nvarchar(max),
	 @Loai int
AS
BEGIN 
	SET NOCOUNT ON;
	DECLARE @iDCT uniqueidentifier='00000000-0000-0000-0000-000000000000';

	DECLARE @fSoThamDinh INT;
	SELECT @fSoThamDinh = fSoThamDinh
	FROM BH_ThamDinhQuyetToan_ChungTuChiTiet 
	WHERE iNamLamViec = @NamLamViec - 1 and iMa = 240 and iID_MaDonVi=@IdDonVi

	
		-- lấy ra đơn vị từ đơn vị của chứng từ
	SELECT iID_MaDonVi, iID_MaDonVi + ' - ' + sTenDonVi as sTenDonVi into #tblDonVi
	FROM DonVi 
	WHERE 
		INamLamViec = @NamLamViec 
		AND iTrangThai=1
		AND iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))

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
		danhmuc.sDuToanChiTietToi ,
		dv.iID_MaDonVi,
		dv.sTenDonVi
		INTO #tblMlnsByPhanCap 
	FROM
		BH_DM_MucLucNganSach AS danhmuc ,#tblDonVi dv
	WHERE
		danhmuc.iNamLamViec = @NamLamViec 
		AND danhmuc.sLNs IN (SELECT * FROM splitstring (@LNS)) 
		AND danhmuc.iTrangThai= 1 

	
	-- Get data chung tu tong hop
	if @Loai=1
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
			mlns.iID_MaDonVi as IIdMaDonVi,
			mlns.sTenDonVi,
			CASE WHEN mlns.sXauNoiMa = @LNS THEN @fSoThamDinh ELSE 0 END as fDuToanNamTruocChuyenSang,
			ISNULL(ctct.fTien_DuToanNamTruocChuyenSang, 0)  as fTien_DuToanNamTruocChuyenSang, 
			ISNULL(daDuToan.fTienDuToan, 0)  as fTien_DuToanGiaoNamNay,
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
		LEFT JOIN (
			-- lấy ra dữ liệu dự toán
			SELECT 
				  SUM(cast(fTienTuChi as float)) AS fTienDuToan,
				  sXauNoiMa
		   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
		   WHERE iID_DTC_PhanBoDuToanChi IN
			   (SELECT ID
				FROM BH_DTC_PhanBoDuToanChi
				WHERE sSoQuyetDinh <> ''
				  AND sSoQuyetDinh IS NOT NULL
				  AND iNamChungTu = @NamLamViec
				  AND bIsKhoa=1)
			 AND iID_MaDonVi in  (SELECT * FROM f_split(@IdDonVi))-- donvi
		   GROUP BY sXauNoiMa
		) daDuToan  on mlns.sXauNoiMa=daDuToan.sXauNoiMa
		order by mlns.sXauNoiMa
	
	else
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
			mlns.iID_MaDonVi as IIdMaDonVi,
			mlns.sTenDonVi,
			CASE WHEN mlns.sXauNoiMa = @LNS THEN @fSoThamDinh ELSE 0 END as fDuToanNamTruocChuyenSang,
			ISNULL(ctct.fTien_DuToanNamTruocChuyenSang, 0)  as fTien_DuToanNamTruocChuyenSang, 
			ISNULL(daDuToan.fTienDuToan, 0)  as fTien_DuToanGiaoNamNay,
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
			LEFT JOIN (
			-- lấy ra dữ liệu dự toán
			SELECT 
				  SUM(cast(fTienTuChi as float)) AS fTienDuToan,
				  sXauNoiMa
		   FROM BH_DTC_DuToanChiTrenGiao_ChiTiet
		   WHERE iID_DTC_DuToanChiTrenGiao IN
			   (SELECT ID
				FROM BH_DTC_DuToanChiTrenGiao
				WHERE sSoQuyetDinh <> ''
				  AND sSoQuyetDinh IS NOT NULL
				  AND iNamLamViec = @NamLamViec
				  AND bIsKhoa=1)
			 AND iID_MaDonVi in  (SELECT * FROM f_split(@IdDonVi))-- donvi
		   GROUP BY sXauNoiMa
		) daDuToan  on mlns.sXauNoiMa=daDuToan.sXauNoiMa
		Order by mlns.sXauNoiMa
END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nkpk_kehoach_bh]    Script Date: 3/27/2024 5:10:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chi tiet chung tu 
CREATE PROCEDURE [dbo].[sp_rpt_qtc_nkpk_kehoach_bh]
	 @NamLamViec int,
	 @IdDonVi nvarchar(max),
	 @LNS nvarchar(max),
	 @LoaiChi uniqueidentifier,
	 @Loai int,
	 @Dvt int
AS
BEGIN 
	SET NOCOUNT ON;
	DECLARE @fSoThamDinh INT;
	SELECT @fSoThamDinh = sum(fSoThamDinh)
	FROM BH_ThamDinhQuyetToan_ChungTuChiTiet 
	WHERE iNamLamViec = @NamLamViec - 1 and iMa = 240 and  iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))

	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha, sDuToanChiTietToi
			into #tblMlnsByPhanCap
	FROM BH_DM_MucLucNganSach 
	WHERE 
		iNamLamViec = @NamLamViec 
		AND iTrangThai = 1
		AND sLNS IN (SELECT * FROM f_split(@LNS))
		-- get chung tu thuong
	IF @Loai=1
		SELECT   
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
			CASE WHEN mlns.sXauNoiMa = @LNS THEN @fSoThamDinh / @Dvt ELSE 0 END as fDuToanNamTruocChuyenSang,
			ISNULL((daDuToan.fTienDuToan), 0) / @Dvt  as fTien_DuToanGiaoNamNay,
			ISNULL(Sum(ctct.fTien_TongDuToanDuocGiao), 0) / @Dvt  as fTien_TongDuToanDuocGiao, 
			ISNULL(Sum(ctct.fTien_ThucChi), 0)  / @Dvt  as fTien_ThucChi, 
			ISNULL(Sum(ctct.fTienThua), 0) / @Dvt  as fTienThua, 
			ISNULL(Sum(ctct.fTienThieu), 0) / @Dvt  as fTienThieu, 
			ISNULL(Sum(ctct.fTiLeThucHienTrenDuToan), 0)  as fTiLeThucHienTrenDuToan
		FROM 
			#tblMlnsByPhanCap mlns
		LEFT JOIN 
			(SELECT * FROM BH_QTC_Nam_KPK_ChiTiet 
					WHERE iID_QTC_Nam_KPK in
						( SELECT ID_QTC_Nam_KPK FROM BH_QTC_Nam_KPK
									WHERE iNamLamViec=@NamLamViec
									AND iID_LoaiChi=@LoaiChi
									AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
									)) ctct
		on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
		LEFT JOIN (
				-- lấy ra dữ liệu dự toán
				SELECT 
					  SUM(cast(fTienTuChi as float)) AS fTienDuToan,
					  sXauNoiMa
			   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
			   WHERE iID_DTC_PhanBoDuToanChi IN
				   (SELECT ID
					FROM BH_DTC_PhanBoDuToanChi
					WHERE sSoQuyetDinh <> ''
					  AND sSoQuyetDinh IS NOT NULL
					  AND iNamChungTu = @NamLamViec
					  AND bIsKhoa=1)
				 AND iID_MaDonVi in  (SELECT * FROM f_split(@IdDonVi))-- donvi
			   GROUP BY sXauNoiMa
			) daDuToan  on mlns.sXauNoiMa=daDuToan.sXauNoiMa
		Group by 
			mlns.iID_MLNS ,
			mlns.iID_MLNS_Cha ,
			mlns.sXauNoiMa ,
			mlns.sLNS ,
			mlns.sL ,
			mlns.sK ,
			mlns.sM ,
			mlns.sTM ,
			mlns.sTTM ,
			mlns.sNG ,
			mlns.sTNG ,
			mlns.sMoTa ,
			mlns.bHangCha ,
			mlns.sDuToanChiTietToi ,
			daDuToan.fTienDuToan
		Order by mlns.sXauNoiMa
	-- get chung tu tong hop
	ELSE
		SELECT  
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
		CASE WHEN mlns.sXauNoiMa = @LNS THEN @fSoThamDinh / @Dvt ELSE 0 END as fDuToanNamTruocChuyenSang,
		ISNULL((daDuToan.fTienDuToan), 0) / @Dvt  as fTien_DuToanGiaoNamNay,
		ISNULL(Sum(ctct.fTien_TongDuToanDuocGiao), 0) / @Dvt  as fTien_TongDuToanDuocGiao, 
		ISNULL(Sum(ctct.fTien_ThucChi), 0) / @Dvt  as fTien_ThucChi, 
		ISNULL(Sum(ctct.fTienThua), 0) / @Dvt  as fTienThua, 
		ISNULL(Sum(ctct.fTienThieu), 0) / @Dvt  as fTienThieu, 
		ISNULL(Sum(ctct.fTiLeThucHienTrenDuToan), 0)  as fTiLeThucHienTrenDuToan
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(SELECT * FROM BH_QTC_Nam_KPK_ChiTiet 
				WHERE iID_QTC_Nam_KPK in
					( SELECT ID_QTC_Nam_KPK FROM BH_QTC_Nam_KPK
								WHERE iNamLamViec=@NamLamViec
								AND iID_LoaiChi=@LoaiChi
								AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
								)) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
	LEFT JOIN (
			-- lấy ra dữ liệu dự toán
			SELECT 
				  SUM(cast(fTienTuChi as float)) AS fTienDuToan,
				  sXauNoiMa
		   FROM BH_DTC_DuToanChiTrenGiao_ChiTiet
		   WHERE iID_DTC_DuToanChiTrenGiao IN
			   (SELECT ID
				FROM BH_DTC_DuToanChiTrenGiao
				WHERE sSoQuyetDinh <> ''
				  AND sSoQuyetDinh IS NOT NULL
				  AND iNamLamViec = @NamLamViec
				  AND bIsKhoa=1)
			 AND iID_MaDonVi in  (SELECT * FROM f_split(@IdDonVi))-- donvi
		   GROUP BY sXauNoiMa
		) daDuToan  on mlns.sXauNoiMa=daDuToan.sXauNoiMa
	Group by 
		mlns.iID_MLNS ,
		mlns.iID_MLNS_Cha ,
		mlns.sXauNoiMa ,
		mlns.sLNS ,
		mlns.sL ,
		mlns.sK ,
		mlns.sM ,
		mlns.sTM ,
		mlns.sTTM ,
		mlns.sNG ,
		mlns.sTNG ,
		mlns.sMoTa ,
		mlns.bHangCha ,
		mlns.sDuToanChiTietToi,
		daDuToan.fTienDuToan

	Order by mlns.sXauNoiMa
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]    Script Date: 3/27/2024 5:41:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@Lns nvarchar(max),
@Donvitinh int,
@IsTongHop int
as
begin
	SET NOCOUNT ON;
		DECLARE @iDCT uniqueidentifier='00000000-0000-0000-0000-000000000000';
		DECLARE @fSoThamDinh INT;
		SELECT @fSoThamDinh = Sum(fSoThamDinh)
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet 
		WHERE iNamLamViec = @INamLamViec - 1 and iMa = 240 and iID_MaDonVi  IN (SELECT * FROM f_split(@IdMaDonVi))
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
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs in (select * from splitstring(@Lns))
				and danhmuc.iTrangThai=1

	---Lấy danh sách chi tiết 
		select	
			qtcn_ct.sXauNoiMa,
			qtcn_ct.sNoiDung,
			Sum(qtcn_ct.fTien_DuToanNamTruocChuyenSang)/@Donvitinh as fTien_DuToanNamTruocChuyenSang,
			Sum(qtcn_ct.fTien_DuToanGiaoNamNay)/@Donvitinh as fTien_DuToanGiaoNamNay,
			Sum(qtcn_ct.fTien_TongDuToanDuocGiao)/@Donvitinh as fTien_TongDuToanDuocGiao,
			Sum(qtcn_ct.fTien_ThucChi)/@Donvitinh as fTien_ThucChi,
			Sum(qtcn_ct.fTienThua)/@Donvitinh as fTienThua,
			Sum(qtcn_ct.fTienThieu)/@Donvitinh as fTienThieu,
			Sum(qtcn_ct.fTiLeThucHienTrenDuToan) as fTiLeThucHienTrenDuToan
		into #tbl_qtcn_chitiet
		from BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet as  qtcn_ct
		inner join  BH_QTC_Nam_KCB_QuanYDonVi as qtcn on qtcn_ct.iID_QTC_Nam_KCB_QuanYDonVi = qtcn.ID_QTC_Nam_KCB_QuanYDonVi
		where qtcn.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
		and qtcn.iNamLamViec = @INamLamViec
		--and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
		group by qtcn_ct.sXauNoiMa,qtcn_ct.sNoiDung

		---Kết quả hiển thị trả về chung tu thuong
	if @IsTongHop=0
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
			mucluc.iID_MLNS as iID_MucLucNganSach,
			mucluc.sDuToanChiTietToi,
			mucluc.sMoTa as sNoiDung,
			CASE WHEN mucluc.sXauNoiMa = @LNS THEN @fSoThamDinh / @Donvitinh ELSE 0 END as fTien_DuToanNamTruocChuyenSang, 
			daDuToan.fTienDuToan as fTien_DuToanGiaoNamNay,
			chi_tiet.fTien_TongDuToanDuocGiao,
			chi_tiet.fTien_ThucChi,
			chi_tiet.fTienThua,
			chi_tiet.fTienThieu,
			chi_tiet.fTiLeThucHienTrenDuToan

		from #tblMucLucNganSach as mucluc
		left join #tbl_qtcn_chitiet as chi_tiet on mucluc.sXauNoiMa = chi_tiet.sXauNoiMa
		LEFT JOIN (
			-- lấy ra dữ liệu dự toán
			SELECT 
				  SUM(cast(fTienTuChi as float)) AS fTienDuToan,
				  sXauNoiMa
		   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
		   WHERE iID_DTC_PhanBoDuToanChi IN
			   (SELECT ID
				FROM BH_DTC_PhanBoDuToanChi
				WHERE sSoQuyetDinh <> ''
				  AND sSoQuyetDinh IS NOT NULL
				  AND iNamChungTu = @INamLamViec
				  AND bIsKhoa=1)
			 AND iID_MaDonVi in  (SELECT * FROM f_split(@IdMaDonVi))-- donvi
		   GROUP BY sXauNoiMa
		) daDuToan  on mucluc.sXauNoiMa=daDuToan.sXauNoiMa
		order by mucluc.sXauNoiMa
		---Kết quả hiển thị trả về chung tu cha
	else
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
			mucluc.iID_MLNS as iID_MucLucNganSach,
			mucluc.sDuToanChiTietToi,
			mucluc.sMoTa as sNoiDung,
			CASE WHEN mucluc.sXauNoiMa = @LNS THEN @fSoThamDinh / @Donvitinh ELSE 0 END as fTien_DuToanNamTruocChuyenSang, 
			daDuToan.fTienDuToan as  fTien_DuToanGiaoNamNay,
			chi_tiet.fTien_TongDuToanDuocGiao,
			chi_tiet.fTien_ThucChi,
			chi_tiet.fTienThua,
			chi_tiet.fTienThieu,
			chi_tiet.fTiLeThucHienTrenDuToan

		from #tblMucLucNganSach as mucluc
		left join #tbl_qtcn_chitiet as chi_tiet on mucluc.sXauNoiMa = chi_tiet.sXauNoiMa
		LEFT JOIN (
			-- lấy ra dữ liệu dự toán
			SELECT 
				  SUM(cast(fTienTuChi as float)) AS fTienDuToan,
				  sXauNoiMa
		   FROM BH_DTC_DuToanChiTrenGiao_ChiTiet
		   WHERE iID_DTC_DuToanChiTrenGiao IN
			   (SELECT ID
				FROM BH_DTC_DuToanChiTrenGiao
				WHERE sSoQuyetDinh <> ''
				  AND sSoQuyetDinh IS NOT NULL
				  AND iNamLamViec = @INamLamViec
				  AND bIsKhoa=1)
			 AND iID_MaDonVi in  (SELECT * FROM f_split(@IdMaDonVi))-- donvi
		   GROUP BY sXauNoiMa
		) daDuToan  on mucluc.sXauNoiMa=daDuToan.sXauNoiMa
		order by mucluc.sXauNoiMa

		drop table #tblMucLucNganSach;
		drop table #tbl_qtcn_chitiet;

end
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]    Script Date: 3/27/2024 5:41:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]
	 @NamLamViec int,
	 @iD uniqueidentifier,
	 @IdDonVi nvarchar(max),
	 @LNS nvarchar(max),
	 @DonViTnh int,
	 @Loai int
AS
BEGIN 
	SET NOCOUNT ON;
	DECLARE @iDCT uniqueidentifier='00000000-0000-0000-0000-000000000000';
	DECLARE @fSoThamDinh INT;
	SELECT @fSoThamDinh = Sum(fSoThamDinh)
	FROM BH_ThamDinhQuyetToan_ChungTuChiTiet 
	WHERE iNamLamViec = @NamLamViec - 1 and iMa = 240 and iID_MaDonVi  IN (SELECT * FROM f_split(@IdDonVi))
	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha, sDuToanChiTietToi
			into #tblMlnsByPhanCap
	FROM BH_DM_MucLucNganSach 
	WHERE 
		iNamLamViec = @NamLamViec 
		AND iTrangThai = 1
		AND sLNS IN (SELECT * FROM f_split(@LNS))

IF @Loai=1
	SELECT   isnull(ctct.ID_QTC_Nam_KinhPhiQuanLy_ChiTiet, @iDCT) as ID_QTC_Nam_KinhPhiQuanLy_ChiTiet,
			ctct.IID_QTC_Nam_KinhPhiQuanLy,
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
			--ctct.iID_MaDonVi,
			@NamLamViec AS INamLamViec,
			mlns.bHangCha as IsHangCha,
			isnull(ctct.dNgayTao, getdate()) AS dNgayTao,
			isnull(ctct.dNgaySua, getdate()) AS dNgaySua,
			ctct.sNguoiTao AS sNguoiTao,
			ctct.sNguoiSua AS sNguoiSua,
			CASE WHEN mlns.sXauNoiMa = @LNS THEN @fSoThamDinh / @DonViTnh ELSE 0 END as fTien_DuToanNamTruocChuyenSang,
			ISNULL((daDuToan.fTienDuToan), 0) / @DonViTnh  as fTien_DuToanGiaoNamNay,
			ISNULL(Sum(ctct.fTien_DuToanNamTruocChuyenSang), 0) / @DonViTnh + ISNULL(Sum(ctct.fTien_DuToanGiaoNamNay), 0)/ @DonViTnh  as fTien_TongDuToanDuocGiao, 
			ISNULL(Sum(ctct.fTien_ThucChi), 0) / @DonViTnh  as fTien_ThucChi, 
			ISNULL(Sum(ctct.fTienThua), 0) / @DonViTnh  as fTienThua, 
			ISNULL(Sum(ctct.fTienThieu), 0) / @DonViTnh  as fTienThieu, 
			--ISNULL(Sum(ctct.fTiLeThucHienTrenDuToan), 0) / @DonViTnh  as fTiLeThucHienTrenDuToan
			ISNULL(Sum(ctct.fTiLeThucHienTrenDuToan), 0)   as fTiLeThucHienTrenDuToan
		FROM 
			#tblMlnsByPhanCap mlns
		LEFT JOIN 
			(SELECT * FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet 
					WHERE iID_QTC_Nam_KinhPhiQuanLy in
						( SELECT ID_QTC_Nam_KinhPhiQuanLy FROM BH_QTC_Nam_KinhPhiQuanLy
									WHERE iNamLamViec=@NamLamViec
									AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
									)) ctct
		on mlns.iID_MLNS = ctct.iID_MucLucNganSach
		LEFT JOIN (
			-- lấy ra dữ liệu dự toán
			SELECT 
				  SUM(cast(fTienTuChi as float)) AS fTienDuToan,
				  sXauNoiMa
		   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
		   WHERE iID_DTC_PhanBoDuToanChi IN
			   (SELECT ID
				FROM BH_DTC_PhanBoDuToanChi
				WHERE sSoQuyetDinh <> ''
				  AND sSoQuyetDinh IS NOT NULL
				  AND iNamChungTu = @NamLamViec
				  AND bIsKhoa=1)
			 AND iID_MaDonVi in  (SELECT * FROM f_split(@IdDonVi))-- donvi
		   GROUP BY sXauNoiMa
		) daDuToan  on mlns.sXauNoiMa=daDuToan.sXauNoiMa
		Group by ctct.ID_QTC_Nam_KinhPhiQuanLy_ChiTiet,
			ctct.IID_QTC_Nam_KinhPhiQuanLy,
			--ctct.iID_MaDonVi,
			mlns.iID_MLNS ,
			mlns.iID_MLNS_Cha ,
			mlns.sXauNoiMa ,
			mlns.sLNS ,
			mlns.sL ,
			mlns.sK ,
			mlns.sM ,
			mlns.sTM ,
			mlns.sTTM ,
			mlns.sNG ,
			mlns.sTNG ,
			mlns.sMoTa ,
			mlns.bHangCha ,
			ctct.sNguoiTao ,
			ctct.sNguoiSua, 
			ctct.dNgayTao,
			mlns.sDuToanChiTietToi,
			ctct.dNgaySua,
			daDuToan.fTienDuToan
	Order by mlns.sXauNoiMa
	ELSE

		SELECT   isnull(ctct.ID_QTC_Nam_KinhPhiQuanLy_ChiTiet, @iDCT) as ID_QTC_Nam_KinhPhiQuanLy_ChiTiet,
			ctct.IID_QTC_Nam_KinhPhiQuanLy,
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
			--ctct.iID_MaDonVi,
			@NamLamViec AS INamLamViec,
			mlns.bHangCha as IsHangCha,
			isnull(ctct.dNgayTao, getdate()) AS dNgayTao,
			isnull(ctct.dNgaySua, getdate()) AS dNgaySua,
			ctct.sNguoiTao AS sNguoiTao,
			ctct.sNguoiSua AS sNguoiSua,
			CASE WHEN mlns.sXauNoiMa = @LNS THEN @fSoThamDinh / @DonViTnh ELSE 0 END as fTien_DuToanNamTruocChuyenSang,
			ISNULL((daDuToan.fTienDuToan), 0) / @DonViTnh  as fTien_DuToanGiaoNamNay,
			ISNULL(Sum(ctct.fTien_DuToanNamTruocChuyenSang), 0) / @DonViTnh + ISNULL(Sum(ctct.fTien_DuToanGiaoNamNay), 0)/ @DonViTnh  as fTien_TongDuToanDuocGiao, 
			ISNULL(Sum(ctct.fTien_ThucChi), 0) / @DonViTnh  as fTien_ThucChi, 
			ISNULL(Sum(ctct.fTienThua), 0) / @DonViTnh  as fTienThua, 
			ISNULL(Sum(ctct.fTienThieu), 0) / @DonViTnh  as fTienThieu, 
			--ISNULL(Sum(ctct.fTiLeThucHienTrenDuToan), 0) / @DonViTnh  as fTiLeThucHienTrenDuToan
			ISNULL(Sum(ctct.fTiLeThucHienTrenDuToan), 0)   as fTiLeThucHienTrenDuToan
		FROM 
			#tblMlnsByPhanCap mlns
		LEFT JOIN 
			(SELECT * FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet 
					WHERE iID_QTC_Nam_KinhPhiQuanLy in
						( SELECT ID_QTC_Nam_KinhPhiQuanLy FROM BH_QTC_Nam_KinhPhiQuanLy
									WHERE iNamLamViec=@NamLamViec
									AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
									)) ctct
		on mlns.iID_MLNS = ctct.iID_MucLucNganSach
		LEFT JOIN (
			-- lấy ra dữ liệu dự toán
			SELECT 
				  SUM(cast(fTienTuChi as float)) AS fTienDuToan,
				  sXauNoiMa
		   FROM BH_DTC_DuToanChiTrenGiao_ChiTiet
		   WHERE iID_DTC_DuToanChiTrenGiao IN
			   (SELECT ID
				FROM BH_DTC_DuToanChiTrenGiao
				WHERE sSoQuyetDinh <> ''
				  AND sSoQuyetDinh IS NOT NULL
				  AND iNamLamViec = @NamLamViec
				  AND bIsKhoa=1)
			 AND iID_MaDonVi in  (SELECT * FROM f_split(@IdDonVi))-- donvi
		   GROUP BY sXauNoiMa
		) daDuToan  on mlns.sXauNoiMa=daDuToan.sXauNoiMa
		Group by ctct.ID_QTC_Nam_KinhPhiQuanLy_ChiTiet,
			ctct.IID_QTC_Nam_KinhPhiQuanLy,
			--ctct.iID_MaDonVi,
			mlns.iID_MLNS ,
			mlns.iID_MLNS_Cha ,
			mlns.sXauNoiMa ,
			mlns.sLNS ,
			mlns.sL ,
			mlns.sK ,
			mlns.sM ,
			mlns.sTM ,
			mlns.sTTM ,
			mlns.sNG ,
			mlns.sTNG ,
			mlns.sMoTa ,
			mlns.bHangCha ,
			ctct.sNguoiTao ,
			ctct.sNguoiSua, 
			ctct.dNgayTao,
			mlns.sDuToanChiTietToi,
			ctct.dNgaySua,
			daDuToan.fTienDuToan
	Order by mlns.sXauNoiMa

	drop table #tblMlnsByPhanCap;

END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]    Script Date: 3/28/2024 3:20:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]
@IdChungTu uniqueidentifier,
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

		-- lấy ra đơn vị từ đơn vị của chứng từ
	SELECT iID_MaDonVi, iID_MaDonVi + ' - ' + sTenDonVi as sTenDonVi into #tblDonVi
	FROM DonVi 
	WHERE 
		INamLamViec = @INamLamViec 
		AND iTrangThai=1
		AND iID_MaDonVi IN (SELECT * FROM f_split(@MaDonVi))

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
		danhmuc.sDuToanChiTietToi,
		dv.iID_MaDonVi,
		dv.sTenDonVi
		INTO #tblMucLucNganSach 
	FROM
		BH_DM_MucLucNganSach AS danhmuc ,#tblDonVi dv
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
		mucluc.iID_MaDonVi iIDMaDonVi,
		mucluc.sTenDonVi,
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
		mucluc.iID_MaDonVi iIDMaDonVi,
		mucluc.sTenDonVi,
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet_clone]    Script Date: 3/28/2024 3:20:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet_clone]
	@IdChungTu uniqueidentifier,
	@IDLoaiChi uniqueidentifier,
	@SLNS nvarchar(max),
	@SMaLoaiChi nvarchar(50),
	@IIdMaDonVi nvarchar(500),
	@DNgayChungTu datetime,
	@iQuyChungTu int,
	@INamLamViec int,
	@Loai int
AS
BEGIN
	DECLARE @iD uniqueidentifier='00000000-0000-0000-0000-000000000000';
	DECLARE @CountIndex INT;
	SELECT * into #tempLNS from f_split(@SLNS);
	SELECT * into #tempAgency from  f_split(@IIdMaDonVi);
	SELECT @CountIndex = COUNT(*) FROM 
									BH_QTC_QUY_KCB_Chitiet 
									WHERE iID_QTC_Quy_KCB =@IdChungTu

	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha,sDuToanChiTietToi
			into #tblNsMlns
	FROM BH_DM_MucLucNganSach 
	where iNamLamViec = @INamLamViec 

		-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(fTienTuChi) AS fTien_DuToanGiaoNamNay,
          iID_MaDonVi,
          iID_MucLucNganSach,
		  sXauNoiMa
		  into #tblPhanBoDuToan
   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
   WHERE iID_DTC_PhanBoDuToanChi IN
       (SELECT ID
        FROM BH_DTC_PhanBoDuToanChi
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iNamChungTu = @INamLamViec
 
		  AND bIsKhoa=1
          AND cast(dNgayQuyetDinh AS date) <= cast(@DNgayChungTu AS date))
     AND iID_MaDonVi in  (SELECT * FROM f_split(@IIdMaDonVi))-- donvi
   GROUP BY iID_MaDonVi, 
   iID_MucLucNganSach,
   sXauNoiMa

   	SELECT 
		  SUM(fTienTuChi) AS fTien_DuToanGiaoNamNay,
          iID_MaDonVi,
          iID_MucLucNganSach,
		  sXauNoiMa
		  into #tblNhanPhanBoTrenGiao
   FROM BH_DTC_DuToanChiTrenGiao_ChiTiet
   WHERE iID_DTC_DuToanChiTrenGiao IN
       (SELECT ID
        FROM BH_DTC_DuToanChiTrenGiao
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iNamLamViec = @INamLamViec
		  AND bIsKhoa=1
		  )
     AND iID_MaDonVi in  (SELECT * FROM f_split(@IIdMaDonVi))-- donvi
   GROUP BY iID_MaDonVi, 
   iID_MucLucNganSach,
   sXauNoiMa


	SELECT DISTINCT iID_MucLucNganSach
	--, iID_MLNS_Cha 
	into #tblPhanBoDuToanGroupbyiID_MLNS from #tblPhanBoDuToan

	SELECT T.* INTO #tblNsMlns1
	FROM #tblNsMlns T
	INNER JOIN #tblPhanBoDuToanGroupbyiID_MLNS E ON T.iID_MLNS = E.iID_MucLucNganSach 


	;WITH C AS  
	(  
	  SELECT *
	  FROM #tblNsMlns1
	  UNION ALL 
	  SELECT T.*
	  FROM #tblNsMlns T
	  INNER JOIN C
	  ON T.iID_MLNS = C.iID_MLNS_Cha
	)

	SELECT DISTINCT * into #mlnsPhanBo from C;

	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa,
			CASE
				WHEN (sNG is null OR sNG = '') THEN cast(1 as bit)
			ELSE cast(0 as bit)
			END AS bHangCha	
			into #tblMlnsByPhanCap
	FROM #mlnsPhanBo 
	WHERE 
		sLNS IN (SELECT * FROM #tempLNS)

	-- lấy ra đơn vị từ đơn vị của chứng từ
	SELECT iID_MaDonVi, iID_MaDonVi + ' - ' + sTenDonVi as sTenDonVi into #tblDonVi
	FROM DonVi 
	WHERE 
		INamLamViec = @INamLamViec 
		AND iID_MaDonVi IN (SELECT * FROM #tempAgency)

	-- Tao bang tam luu chu mlng con
	SELECT * INTO #tblMlnsByPhanCapExistDonVi
	FROM #tblMlnsByPhanCap tbl, #tblDonVi dv
	WHERE  tbl.bHangCha = 0 OR (tbl.bHangCha = 1 and (IsNull(tbl.sM, '') != '' OR tbl.sM <> '' OR IsNull(tbl.sTM, '') != '')  )
	AND tbl.sTM !=''  


	-- Tao bang tam luu chu mlng cha
	SELECT *, '' AS iID_MaDonVi, '' AS sTenDonVi  INTO #tblMlnsByPhanCapNotDonVi
	FROM #tblMlnsByPhanCap 
		WHERE bHangCha = 1 AND ( (IsNull(sM, '') = '' or sM = '')) AND ( (IsNull(sTM, '') = '' or sTM = ''))
		OR ((ISNULL(sTM,'')='' OR sTM='')) OR sTM='0'

	-- map bảng mlnsPhanCap và đơn vị
	SELECT * INTO #tblMLNS FROM (
		SELECT *
		FROM #tblMlnsByPhanCapExistDonVi tbl
		WHERE sTM !='0'
		UNION ALL
		SELECT *
		FROM #tblMlnsByPhanCapNotDonVi 
	) mlns

	-- map bảng mlns ns và đơn vị
	SELECT * INTO #tblMlnsRoot FROM (
		SELECT * FROM #tblNsMlns, #tblDonVi 
		--WHERE bHangCha = 0
		--UNION ALL
		--SELECT *, null AS iID_MaDonVi, null AS sTenDonVi  
		--FROM #tblNsMlns 
		--WHERE bHangCha = 1
	) mlns

	SELECT CTCT.* 
		INTO #TempChungTuQuyTruoc
	FROM BH_QTC_QUY_KCB_Chitiet CTCT
		WHERE CTCT.iID_QTC_Quy_KCB IN
	(
		SELECT ID_QTC_Quy_KCB FROM  BH_QTC_QUY_KCB 
		WHERE iID_MaDonVi IN ( SELECT * FROM  f_split(@IIdMaDonVi))
					AND iNamChungTu=@INamLamViec
					AND iQuyChungTu=1
	)

	-- lay du lieu da quyet toan 
	SELECT  
		SUM(fTienXacNhanQuyetToanQuyNay) AS fTienQuyetToanDaDuyet,
		CT.iID_MaDonVi,
		CTCT.sXauNoiMa
		into #TemptblTienDaDuyet
	FROM
	BH_QTC_QUY_KCB CT
	INNER JOIN BH_QTC_QUY_KCB_Chitiet CTCT
	ON CT.ID_QTC_Quy_KCB=CTCT.iID_QTC_Quy_KCB
	WHERE CT.iNamChungTu = @INamLamViec
		  --AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@IIdMaDonVi))
		  --AND CT.bIsKhoa=1
		  AND CT.iQuyChungTu<@iQuyChungTu
	GROUP BY  CT.iID_MaDonVi,CTCT.sXauNoiMa

	--- chung tu thuong
	if 	@Loai=1	
	-- Get data
	SELECT
		isnull(ctct.ID_QTC_QUY_KCB_Chitiet, @iD) AS ID_QTC_QUY_KCB_Chitiet,
		@IdChungTu AS iID_QTC_QUY_KCB,
		mlnsPhanBo.iID_MLNS,
		mlnsPhanBo.iID_MLNS_Cha,
		mlnsPhanBo.iID_MLNS as iID_MucLucNganSach,
		mlnsPhanBo.sXauNoiMa AS sXauNoiMa,
		mlnsPhanBo.sLNS AS SLNS,
		mlnsPhanBo.sL AS SL,
		mlnsPhanBo.sK AS SK,
		mlnsPhanBo.sM AS SM,
		mlnsPhanBo.sTM AS STM,
		mlnsPhanBo.sTTM AS STTM,
		mlnsPhanBo.sNG AS SNG,
		mlnsPhanBo.sTNG AS STNG,
		mlnsPhanBo.sTNG1 AS STNG1,
		mlnsPhanBo.sTNG2 AS STNG2,
		mlnsPhanBo.sTNG3 AS STNG3,
		mlnsPhanBo.sMoTa AS SNoiDung,
		mlnsPhanBo.bHangCha ,
		mlnsPhanBo.sDuToanChiTietToi,
		@INamLamViec AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(tblQuyTruoc.fTien_DuToanNamTruocChuyenSang, 0) as FTienDuToanNamTruocChuyenSang,
		isnull(daCapDuToan.fTien_DuToanGiaoNamNay, 0) as FTienDuToanGiaoNamNay,
		isnull(ctct.fTien_TongDuToanDuocGiao, 0) as FTienTongDuToanDuocGiao,
		isnull(ctct.fTienThucChi, 0) as FTienThucChi,
		isnull(tblDaDuyet.fTienQuyetToanDaDuyet, 0) as FTienQuyetToanDaDuyet,
		isnull(ctct.fTienDeNghiQuyetToanQuyNay, 0) as FTienDeNghiQuyetToanQuyNay,
		isnull(ctct.fTienXacNhanQuyetToanQuyNay, 0) as FTienXacNhanQuyetToanQuyNay,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsRoot AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_QTC_QUY_KCB_Chitiet
			WHERE 
		 		iID_QTC_QUY_KCB = @IdChungTu
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach --and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_QTC_QUY_KCB ct ON ctct.iID_QTC_QUY_KCB = ct.ID_QTC_QUY_KCB
	LEFT JOIN #tblPhanBoDuToan daCapDuToan
	ON mlnsPhanBo.sXauNoiMa = daCapDuToan.sXauNoiMa
	LEFT JOIN #TempChungTuQuyTruoc tblQuyTruoc on mlnsPhanBo.sXauNoiMa=tblQuyTruoc.sXauNoiMa
	LEFT JOIN #TemptblTienDaDuyet tblDaDuyet on mlnsPhanBo.sXauNoiMa=tblDaDuyet.sXauNoiMa
	WHERE mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	ORDER BY mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	------ Chứng từ tong hop
	ELSE 
	---- Get data
	SELECT
		isnull(ctct.ID_QTC_QUY_KCB_Chitiet, @iD) AS ID_QTC_QUY_KCB_Chitiet,
		@IdChungTu AS iID_QTC_QUY_KCB,
		mlnsPhanBo.iID_MLNS ,
		mlnsPhanBo.iID_MLNS_Cha ,
		mlnsPhanBo.iID_MLNS as iID_MucLucNganSach,
		mlnsPhanBo.sXauNoiMa AS sXauNoiMa,
		mlnsPhanBo.sLNS AS SLNS,
		mlnsPhanBo.sL AS SL,
		mlnsPhanBo.sK AS SK,
		mlnsPhanBo.sM AS SM,
		mlnsPhanBo.sTM AS STM,
		mlnsPhanBo.sTTM AS STTM,
		mlnsPhanBo.sNG AS SNG,
		mlnsPhanBo.sTNG AS STNG,
		mlnsPhanBo.sTNG1 AS STNG1,
		mlnsPhanBo.sTNG2 AS STNG2,
		mlnsPhanBo.sTNG3 AS STNG3,
		mlnsPhanBo.sMoTa AS SNoiDung,
		mlnsPhanBo.bHangCha ,
		mlnsPhanBo.sDuToanChiTietToi,
		@INamLamViec AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(ctct.fTien_DuToanNamTruocChuyenSang, 0) as FTienDuToanNamTruocChuyenSang,
		isnull(daCapDuToan.fTien_DuToanGiaoNamNay, 0) as FTienDuToanGiaoNamNay,
		isnull(ctct.fTien_TongDuToanDuocGiao, 0) as FTienTongDuToanDuocGiao,
		isnull(ctct.fTienThucChi, 0) as FTienThucChi,
		isnull(tblDaDuyet.fTienQuyetToanDaDuyet, 0) as FTienQuyetToanDaDuyet,
		isnull(ctct.fTienDeNghiQuyetToanQuyNay, 0) as FTienDeNghiQuyetToanQuyNay,
		isnull(ctct.fTienXacNhanQuyetToanQuyNay, 0) as FTienXacNhanQuyetToanQuyNay,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsRoot AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_QTC_QUY_KCB_Chitiet
			WHERE 
		 		iID_QTC_QUY_KCB = @IdChungTu
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach --and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_QTC_QUY_KCB ct ON ctct.iID_QTC_QUY_KCB = ct.ID_QTC_QUY_KCB
	LEFT JOIN #tblNhanPhanBoTrenGiao daCapDuToan
	ON mlnsPhanBo.sXauNoiMa = daCapDuToan.sXauNoiMa 
	LEFT JOIN #TempChungTuQuyTruoc tblQuyTruoc on mlnsPhanBo.sXauNoiMa=tblQuyTruoc.sXauNoiMa
	LEFT JOIN #TemptblTienDaDuyet tblDaDuyet on mlnsPhanBo.sXauNoiMa=tblDaDuyet.sXauNoiMa
	WHERE mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	ORDER BY mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	--END
END
;
;
;
;
;
;
;
;
;