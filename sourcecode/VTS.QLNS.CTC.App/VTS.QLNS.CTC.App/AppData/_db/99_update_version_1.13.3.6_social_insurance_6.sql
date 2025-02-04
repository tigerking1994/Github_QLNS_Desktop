/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkpk_thongtriloai2_bh]    Script Date: 10/25/2023 8:53:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_qkpk_thongtriloai2_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_qkpk_thongtriloai2_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkpk_thongtriloai1_bh]    Script Date: 10/25/2023 8:53:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_qkpk_thongtriloai1_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_qkpk_thongtriloai1_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkpk_kehoach_bh]    Script Date: 10/25/2023 8:53:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_qkpk_kehoach_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_qkpk_kehoach_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkp_ql_thongtriloai2_bh]    Script Date: 10/25/2023 8:53:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_qkp_ql_thongtriloai2_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_qkp_ql_thongtriloai2_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkp_ql_thongtriloai1_bh]    Script Date: 10/25/2023 8:53:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_qkp_ql_thongtriloai1_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_qkp_ql_thongtriloai1_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkp_ql_chungtu_chi_tiet]    Script Date: 10/25/2023 8:53:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_qkp_ql_chungtu_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_qkp_ql_chungtu_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nqlkp_phucluc_bh]    Script Date: 10/25/2023 8:53:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_nqlkp_phucluc_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_nqlkp_phucluc_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]    Script Date: 10/25/2023 8:53:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nkpk_phucluc_bh]    Script Date: 10/25/2023 8:53:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_nkpk_phucluc_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_nkpk_phucluc_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nkpk_kehoach_bh]    Script Date: 10/25/2023 8:53:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_nkpk_kehoach_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_nkpk_kehoach_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_ktsdk_chungtu_tonghop_bhxh]    Script Date: 10/25/2023 8:53:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khc_ktsdk_chungtu_tonghop_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khc_ktsdk_chungtu_tonghop_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_khssv_nld_chungtu_tonghop_bhxh]    Script Date: 10/25/2023 8:53:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khc_khssv_nld_chungtu_tonghop_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khc_khssv_nld_chungtu_tonghop_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_khacget_tienquyet_toandaduyet]    Script Date: 10/25/2023 8:53:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_quykinhphi_khacget_tienquyet_toandaduyet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_quykinhphi_khacget_tienquyet_toandaduyet]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_khac_chungtu_index_bh]    Script Date: 10/25/2023 8:53:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_quykinhphi_khac_chungtu_index_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_quykinhphi_khac_chungtu_index_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_khac_chungtu_chitiet_tao_tonghop]    Script Date: 10/25/2023 8:53:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_quykinhphi_khac_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_quykinhphi_khac_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_khac_chungtu_chi_tiet]    Script Date: 10/25/2023 8:53:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_quykinhphi_khac_chungtu_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_quykinhphi_khac_chungtu_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_qkpql_create_data_summary_bh]    Script Date: 10/25/2023 8:53:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_qkpql_create_data_summary_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_qkpql_create_data_summary_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_nkqk_chitiet_export_excel_bh]    Script Date: 10/25/2023 8:53:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_nkqk_chitiet_export_excel_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_nkqk_chitiet_export_excel_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_nkqk_chitiet_bh]    Script Date: 10/25/2023 8:53:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_nkqk_chitiet_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_nkqk_chitiet_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_nkpk_create_data_summary_bh]    Script Date: 10/25/2023 8:53:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_nkpk_create_data_summary_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_nkpk_create_data_summary_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_namkinhphi_quanly_gettien_thuchi]    Script Date: 10/25/2023 8:53:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_namkinhphi_quanly_gettien_thuchi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_namkinhphi_quanly_gettien_thuchi]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_namkinhphi_quanly_chungtu_index_bh]    Script Date: 10/25/2023 8:53:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_namkinhphi_quanly_chungtu_index_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_namkinhphi_quanly_chungtu_index_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_namkinh_phikhac_gettien_thuchi_bh]    Script Date: 10/25/2023 8:53:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_namkinh_phikhac_gettien_thuchi_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_namkinh_phikhac_gettien_thuchi_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_namkinh_phikhac_chungtu_index_bh]    Script Date: 10/25/2023 8:53:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_namkinh_phikhac_chungtu_index_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_namkinh_phikhac_chungtu_index_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_khac_chungtu_chitiet_tao_tonghop]    Script Date: 10/25/2023 8:53:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khc_khac_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khc_khac_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_k_chi_tiet]    Script Date: 10/25/2023 8:53:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khc_k_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khc_k_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_nkp_ql_chitiet]    Script Date: 10/25/2023 8:53:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtc_nkp_ql_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtc_nkp_ql_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[bh_kehoach_chikham_chubenh_index]    Script Date: 10/25/2023 8:53:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bh_kehoach_chikham_chubenh_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[bh_kehoach_chikham_chubenh_index]
GO
/****** Object:  StoredProcedure [dbo].[bh_kehoach_chikham_chubenh_index]    Script Date: 10/25/2023 8:53:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 13/06/2023
-- Description:	Lấy danh sách hiển thị lâp kế hoạch chi kham chu benh

-- =============================================
CREATE PROCEDURE [dbo].[bh_kehoach_chikham_chubenh_index]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
	DISTINCT 
		 kcb.iID_BH_KHC_KCB 
		, kcb.sSoChungTu
		, kcb.dNgayChungTu
		, kcb.sSoQuyetDinh
		, kcb.dNgayQuyetDinh
		, kcb.iNamChungTu
		, kcb.iID_DonVi
		, kcb.iID_MaDonVi
		, kcb.sMoTa
		, kcb.fTongTienDaThucHienNamTruoc
		, kcb.fTongTienUocThucHienNamTruoc
		, kcb.fTongTienKeHoachThucHienNamNay
		, kcb.sTongHop
		, kcb.iID_TongHopID
		, kcb.iLoaiTongHop
		, kcb.bIsKhoa
		, kcb.dNgaySua
		, kcb.dNgayTao
		, kcb.sNguoiSua
		, kcb.sNguoiTao
		, kcb.dNgayTao
		, donVi.sTenDonVi
		-- Tong dự toán todo
	FROM BH_KHC_KCB  kcb
	LEFT JOIN DonVi donVi
		ON kcb.iID_MaDonVi = donVi.iID_MaDonVi AND donVi.iID_DonVi = kcb.iID_DonVi
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_nkp_ql_chitiet]    Script Date: 10/25/2023 8:53:23 AM ******/
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
	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
			into #tblMlnsByPhanCap
	FROM BH_DM_MucLucNganSach 
	WHERE 
		iNamLamViec = 2023 
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
		(SELECT * FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet 
				WHERE iID_QTC_Nam_KinhPhiQuanLy in
					( SELECT ID_QTC_Nam_KinhPhiQuanLy FROM BH_QTC_Nam_KinhPhiQuanLy
								WHERE iNamChungTu=@NamLamViec
								AND ID_QTC_Nam_KinhPhiQuanLy=@iD
								AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
								)) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
	Order by mlns.sXauNoiMa
END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_khc_k_chi_tiet]    Script Date: 10/25/2023 8:53:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[sp_khc_k_chi_tiet]
	@NamLamViec int,
	@IdDonVi nvarchar(100),
	@iID_BH_KHC_K uniqueidentifier
AS
BEGIN
SELECT 
		ct.iID_BH_KHC_K_ChiTiet ,
		ct.iID_KHC_K ,
		ct.iID_MaDonVi,
		ct.iID_MucLucNganSach,
		ct.sMoTa,
		ct.sNoiDung,
		ct.fTienDaThucHienNamTruoc,
		ct.fTienUocThucHienNamTruoc,
		ct.fTienKeHoachThucHienNamNay,
		ct.sGhiChu,
		ct.dNgaySua,
		ct.dNgayTao,
		ct.sNguoiSua,
		ct.sNguoiTao,
		ct.sTenDonVi

	FROM
		(
			SELECT
				bh.iID_MaDonVi,
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_KHC_K bh
			JOIN 
				BH_KHC_K_ChiTiet bhct ON bh.iID_BH_KHC_K = bhct.iID_KHC_K
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_MaDonVi = @IdDonVi
				--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
		) ct
		where ct.iID_KHC_K=@iID_BH_KHC_K
		;

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_khac_chungtu_chitiet_tao_tonghop]    Script Date: 10/25/2023 8:53:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  -- Author:    <Author,,Name>
  -- Create date: <Create Date,,>
  -- Description:  <Description,,>
  -- =============================================
  Create PROCEDURE [dbo].[sp_khc_khac_chungtu_chitiet_tao_tonghop] 
  @ListIdChungTuTongHop ntext, 
  @Nguoitao nvarchar(50),
  @IdChungTu nvarchar(100), 
  @NamLamViec int
  AS BEGIN 
  INSERT INTO [dbo].[BH_KHC_K_ChiTiet] (
    iID_BH_KHC_K_ChiTiet, iID_KHC_K, 
    iID_MucLucNganSach,  sNoiDung, 
    fTienDaThucHienNamTruoc, fTienUocThucHienNamTruoc, 
    fTienKeHoachThucHienNamNay, dNgaySua, dNgayTao, 
    sNguoiSua, sNguoiTao
  ) 
SELECT 
DISTINCT
  NEWID(), 
  @idChungTu, 
  iID_MucLucNganSach, 
  sNoiDung, 
  sum(fTienDaThucHienNamTruoc), 
  sum(fTienUocThucHienNamTruoc), 
  sum(fTienKeHoachThucHienNamNay), 
  Null, 
  GETDATE(), 
  Null, 
  @Nguoitao 
FROM 
  BH_KHC_K_ChiTiet 
WHERE 
  iID_KHC_K in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) 
GROUP BY 
  iID_MucLucNganSach, 
  sNoiDung;
--danh dau chung tu da tong hop
update 
  BH_KHC_K 
set 
  iLoaiTongHop = 2 
where 
  iID_BH_KHC_K in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) 
  
END;



GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_namkinh_phikhac_chungtu_index_bh]    Script Date: 10/25/2023 8:53:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 13/06/2023
-- Description:	Lấy danh sách hiển thị chứng từ quyết toán chi nam kinh phí khac

-- =============================================
Create PROCEDURE [dbo].[sp_qtc_namkinh_phikhac_chungtu_index_bh]
	@YearOfWork int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
	DISTINCT 
		 ct.ID_QTC_Nam_KPK
		, ct.iID_DonVi
		, ct.iID_MaDonVi
		, ct.sSoChungTu
		, ct.dNgayChungTu
		, ct.sSoQuyetDinh
		, ct.dNgayQuyetDinh
		, ct.bThucChiTheo4Quy
		, ct.iNamChungTu
		, ct.sMoTa
		, ct.dNgaySua
		, ct.dNgayTao
		, ct.sNguoiSua
		, ct.sNguoiTao
		, ct.sTongHop
		, ct.iID_TongHopID
		, ct.iLoaiTongHop
		, ct.bIsKhoa
		, ct.fTongTien_DuToanNamTruocChuyenSang
		, ct.fTongTien_DuToanGiaoNamNay
		, ct.fTongTien_TongDuToanDuocGiao
		, ct.fTongTien_ThucChi
		, ct.fTongTienThua
		, ct.fTongTienThieu
		, ct.fTiLeThucHienTrenDuToan
		, ct.iID_LoaiChi
		, lc.sTenDanhMucLoaiChi
		, dv.sTenDonVi
		-- Tong dự toán todo
	FROM BH_QTC_Nam_KPK ct
	LEFT JOIN DonVi dv on ct.iID_DonVi=dv.iID_DonVi 
	and ct.iID_MaDonVi=dv.iID_MaDonVi 
	and dv.iNamLamViec=ct.iNamChungTu
	LEFT JOIN BH_DM_LoaiChi lc on lc.iID=ct.iID_LoaiChi
	WHERE ct.iNamChungTu=@YearOfWork 
	and dv.iTrangThai=1
	Order by ct.sSoChungTu
END


GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_namkinh_phikhac_gettien_thuchi_bh]    Script Date: 10/25/2023 8:53:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		
-- Create date: 
-- Description:	Lấy danh sách hiển thị thực chi theo quý của chứng từ quyết toán năm chi kinh phí khác chi tiết
Create PROCEDURE [dbo].[sp_qtc_namkinh_phikhac_gettien_thuchi_bh]
	@YearOfWork int,
	@AgencyId nvarchar(500),
	@VoucherDate datetime,
	@iQuyChungTu int
AS
BEGIN

	SELECT  
		SUM(CTCT.fTienThucChi) AS FTien_ThucChi,
		CT.iID_MaDonVi,
		CTCT.iID_MucLucNganSach
	FROM
	BH_QTC_Quy_KPK CT
	INNER JOIN BH_QTC_Quy_KPK_ChiTiet CTCT
	ON CT.ID_QTC_Quy_KPK=CTCT.iID_QTC_Quy_KPK
	WHERE CT.iNamChungTu = @YearOfWork
          AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
		  AND CT.bIsKhoa=1
		  AND CT.iQuyChungTu=@iQuyChungTu
	GROUP BY  CT.iID_MaDonVi,CTCT.iID_MucLucNganSach

END

GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_namkinhphi_quanly_chungtu_index_bh]    Script Date: 10/25/2023 8:53:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 13/06/2023
-- Description:	Lấy danh sách hiển thị chứng từ quyết toán chi nam kinh phí quản lý

-- =============================================
Create PROCEDURE [dbo].[sp_qtc_namkinhphi_quanly_chungtu_index_bh]
	@YearOfWork int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
	DISTINCT 
		 ct.ID_QTC_Nam_KinhPhiQuanLy
		, ct.iID_DonVi
		, ct.iID_MaDonVi
		, ct.sSoChungTu
		, ct.dNgayChungTu
		, ct.sSoQuyetDinh
		, ct.dNgayQuyetDinh
		, ct.bThucChiTheo4Quy
		, ct.iNamChungTu
		, ct.sMoTa
		, ct.dNgaySua
		, ct.dNgayTao
		, ct.sNguoiSua
		, ct.sNguoiTao
		, ct.sTongHop
		, ct.iID_TongHopID
		, ct.iLoaiTongHop
		, ct.bIsKhoa
		, ct.fTongTien_DuToanNamTruocChuyenSang
		, ct.fTongTien_DuToanGiaoNamNay
		, ct.fTongTien_TongDuToanDuocGiao
		, ct.fTongTien_ThucChi
		, ct.fTongTienThua
		, ct.fTongTienThieu
		, ct.fTiLeThucHienTrenDuToan
		, dv.sTenDonVi
		-- Tong dự toán todo
	FROM BH_QTC_Nam_KinhPhiQuanLy ct
	LEFT JOIN DonVi dv on ct.iID_DonVi=dv.iID_DonVi 
	and ct.iID_MaDonVi=dv.iID_MaDonVi 
	and dv.iNamLamViec=ct.iNamChungTu
	WHERE ct.iNamChungTu=@YearOfWork 
	and dv.iTrangThai=1
	Order by ct.sSoChungTu
END


GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_namkinhphi_quanly_gettien_thuchi]    Script Date: 10/25/2023 8:53:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 20/09/2023
-- Description:	Lấy danh sách hiển thị thực chi theo quý của chứng từ quyết toán năm chi kinh phí quản lý chi tiết
CREATE PROCEDURE [dbo].[sp_qtc_namkinhphi_quanly_gettien_thuchi]
	@YearOfWork int,
	@AgencyId nvarchar(500),
	@VoucherDate datetime,
	@iQuyChungTu int
AS
BEGIN

	SELECT  
		SUM(fTienThucChi) AS FTien_ThucChi,
		CT.iID_MaDonVi,
		CTCT.iID_MucLucNganSach
	FROM
	BH_QTC_Quy_KinhPhiQuanLy CT
	INNER JOIN BH_QTC_Quy_KinhPhiQuanLy_chiTiet CTCT
	ON CT.ID_QTC_Quy_KinhPhiQuanLy=CTCT.iID_QTC_Quy_KinhPhiQuanLy
	WHERE CT.iNamChungTu = @YearOfWork
          AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
		  AND CT.bIsKhoa=1
		  AND CT.iQuyChungTu=@iQuyChungTu
	GROUP BY  CT.iID_MaDonVi,CTCT.iID_MucLucNganSach

END

GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_nkpk_create_data_summary_bh]    Script Date: 10/25/2023 8:53:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_qtc_nkpk_create_data_summary_bh]
@IdChungTu nvarchar(MAX),
@NguoiTao nvarchar(500),
@YearOfWork int,
@LstIdChungTuSummary nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

INSERT INTO BH_QTC_Nam_KPK_ChiTiet
(ID_QTC_Nam_KPK_ChiTiet,
iID_QTC_Nam_KPK,
iID_MucLucNganSach,
sNoiDung,
dNgaySua,
dNgayTao,
sNguoiSua,
sNguoiTao,
fTien_DuToanNamTruocChuyenSang,
fTien_DuToanGiaoNamNay,
fTien_TongDuToanDuocGiao,
fTien_ThucChi,
fTienThua,
fTienThieu,
fTiLeThucHienTrenDuToan
 )
SELECT 
	   NEWID(),
	   @IdChungTu,
       iID_MucLucNganSach,
	   sNoiDung,
	   null,
	   GETDATE(),
	   @NguoiTao,
	   '',
	   SUM(fTien_DuToanNamTruocChuyenSang),
	   SUM(fTien_DuToanGiaoNamNay),
	   SUM(fTien_TongDuToanDuocGiao),
	   SUM(fTien_ThucChi),
	   SUM(fTienThua),
	   SUM(fTienThieu),
	   SUM(fTiLeThucHienTrenDuToan)
FROM BH_QTC_Nam_KPK_ChiTiet
WHERE  iID_QTC_Nam_KPK IN
    (SELECT *
     FROM f_split(@LstIdChungTuSummary))
group by iID_MucLucNganSach,sNoiDung

UPDATE BH_QTC_Nam_KPK SET iLoaiTongHop = 2 WHERE ID_QTC_Nam_KPK IN (SELECT * FROM f_split(@LstIdChungTuSummary));
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_nkqk_chitiet_bh]    Script Date: 10/25/2023 8:53:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chi tiet chung tu 
Create PROCEDURE [dbo].[sp_qtc_nkqk_chitiet_bh]
	 @NamLamViec int,
	 @iD uniqueidentifier,
	 @IdDonVi nvarchar(max),
	 @LNS nvarchar(max)
AS
BEGIN 
	SET NOCOUNT ON;
	DECLARE @iDCT uniqueidentifier='00000000-0000-0000-0000-000000000000';
	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
			into #tblMlnsByPhanCap
	FROM BH_DM_MucLucNganSach 
	WHERE 
		iNamLamViec = 2023 
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
								WHERE iNamChungTu=@NamLamViec
								AND iID_QTC_Nam_KPK=@iD
								AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
								)) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
	Order by mlns.sXauNoiMa
END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_nkqk_chitiet_export_excel_bh]    Script Date: 10/25/2023 8:53:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chi tiet chung tu 
Create PROCEDURE [dbo].[sp_qtc_nkqk_chitiet_export_excel_bh]
	 @NamLamViec int,
	 @iD uniqueidentifier,
	 @IdDonVi nvarchar(max),
	 @LNS nvarchar(max)
AS
BEGIN 
	SET NOCOUNT ON;
	DECLARE @iDCT uniqueidentifier='00000000-0000-0000-0000-000000000000';
	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
			into #tblMlnsByPhanCap
	FROM BH_DM_MucLucNganSach 
	WHERE 
		iNamLamViec = 2023 
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
								WHERE iNamChungTu=@NamLamViec
								AND iID_QTC_Nam_KPK=@iD
								AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
								)) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
	Order by mlns.sXauNoiMa
END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_qkpql_create_data_summary_bh]    Script Date: 10/25/2023 8:53:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qtc_qkpql_create_data_summary_bh]
@IdChungTu nvarchar(MAX),
@NguoiTao nvarchar(500),
@YearOfWork int,
@LstIdChungTuSummary nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

INSERT INTO BH_QTC_Nam_KinhPhiQuanLy_ChiTiet
(ID_QTC_Nam_KinhPhiQuanLy_ChiTiet,
iID_QTC_Nam_KinhPhiQuanLy,
iID_MucLucNganSach,
sM,
sTM,
sNoiDung,
dNgaySua,
dNgayTao,
sNguoiSua,
sNguoiTao,
fTien_DuToanNamTruocChuyenSang,
fTien_DuToanGiaoNamNay,
fTien_TongDuToanDuocGiao,
fTien_ThucChi,
fTienThua,
fTienThieu,
fTiLeThucHienTrenDuToan
 )
SELECT 
	   NEWID(),
	   @IdChungTu,
       iID_MucLucNganSach,
       sM,
	   sTM,
	   sNoiDung,
	   null,
	   GETDATE(),
	   @NguoiTao,
	   '',
	   SUM(fTien_DuToanNamTruocChuyenSang),
	   SUM(fTien_DuToanGiaoNamNay),
	   SUM(fTien_TongDuToanDuocGiao),
	   SUM(fTien_ThucChi),
	   SUM(fTienThua),
	   SUM(fTienThieu),
	   SUM(fTiLeThucHienTrenDuToan)
FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet
WHERE  iID_QTC_Nam_KinhPhiQuanLy IN
    (SELECT *
     FROM f_split(@LstIdChungTuSummary))
group by iID_MucLucNganSach,sM,sTM,sNoiDung

UPDATE BH_QTC_Nam_KinhPhiQuanLy SET iLoaiTongHop = 2 WHERE ID_QTC_Nam_KinhPhiQuanLy IN (SELECT * FROM f_split(@LstIdChungTuSummary));
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_khac_chungtu_chi_tiet]    Script Date: 10/25/2023 8:53:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 20/09/2023
-- Description:	Lấy danh sách hiển thị chứng từ quyết toán chi quy kinh phí khác chi tiết

-- =============================================
CREATE PROCEDURE [dbo].[sp_qtc_quykinhphi_khac_chungtu_chi_tiet]
	@VoucherId uniqueidentifier,
	@LNS nvarchar(max),
	@YearOfWork int,
	@AgencyId nvarchar(500),
	@VoucherDate datetime,
	@iID_LoaiDanhMucChi uniqueidentifier,
	@iQuyChungTu int
AS
BEGIN
	DECLARE @iD uniqueidentifier='00000000-0000-0000-0000-000000000000';
	DECLARE @CountIndex INT;
	SELECT * into #tempLNS from f_split(@LNS);
	SELECT * into #tempAgency from  f_split(@AgencyId);
	SELECT @CountIndex = COUNT(*) FROM 
									BH_QTC_Quy_KPK_ChiTiet 
									WHERE iID_QTC_Quy_KPK =@VoucherId

	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
			into #tblNsMlns
	FROM BH_DM_MucLucNganSach 
	where iNamLamViec = @YearOfWork 

		-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(fTongTien) AS fTien_DuToanGiaoNamNay,
          0 AS fTien_DuToanNamTruocChuyenSang,
          0 AS fTien_TongDuToanDuocGiao,
		  0 AS fTienThucChi,
		  0 AS fTienQuyetToanDaDuyet,
		  0 AS fTienDeNghiQuyetToanQuyNay,
		  0 AS fTienXacNhanQuyetToanQuyNay,
          iID_MaDonVi,
          iID_MucLucNganSach
		  into #tblPhanBoDuToan
   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
   WHERE iID_DTC_PhanBoDuToanChi IN
       (SELECT ID
        FROM BH_DTC_PhanBoDuToanChi
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iNamChungTu = @YearOfWork
          AND iID_LoaiDanhMucChi = @iID_LoaiDanhMucChi
		  AND bIsKhoa=1
          AND cast(dNgayQuyetDinh AS date) <= cast(@VoucherDate AS date))
     AND iID_MaDonVi in  (SELECT * FROM f_split(@AgencyId))-- donvi
   GROUP BY iID_MaDonVi, 
   iID_MucLucNganSach

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
		INamLamViec = @YearOfWork 
		AND iID_MaDonVi IN (SELECT * FROM #tempAgency)

	-- Tao bang tam luu chu mlng con
	SELECT * INTO #tblMlnsByPhanCapExistDonVi
	FROM #tblMlnsByPhanCap tbl, #tblDonVi dv
	WHERE  tbl.bHangCha = 0 OR (tbl.bHangCha = 1 and (IsNull(tbl.sM, '') != '' OR tbl.sM <> '' OR IsNull(tbl.sTM, '') != '')  )
	AND tbl.sTM !=''  


	-- Tao bang tam luu chu mlng cha
	SELECT *, '' AS iID_MaDonVi, '' AS sTenDonVi  INTO #tblMlnsByPhanCapNotDonVi
	FROM #tblMlnsByPhanCap 
		WHERE bHangCha = 1 AND ( (IsNull(sM, '') = '' or sM = '')) AND ( (IsNull(sTM, '') = '' or sTM = '')) OR ((ISNULL(sTM,'')='' OR sTM='')) OR sTM='0'

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
		WHERE bHangCha = 0
		UNION ALL
		SELECT *, null AS iID_MaDonVi, null AS sTenDonVi  
		FROM #tblNsMlns 
		WHERE bHangCha = 1
	) mlns

	IF @CountIndex=0
	BEGIN
	-- lấy dữ liệu đã cấp
	SELECT  
		0 AS fTien_DuToanGiaoNamNay,
		SUM(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang,
		SUM(fTien_TongDuToanDuocGiao) AS fTien_TongDuToanDuocGiao,
		SUM(fTienThucChi) AS fTienThucChi,
		SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,
		SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay,
		SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay,
		CT.iID_MaDonVi,
	    CTCT.iID_MucLucNganSach
		into #tblDataDaCap
	FROM
	BH_QTC_Quy_KPK CT
	INNER JOIN BH_QTC_Quy_KPK_ChiTiet CTCT
	ON CT.ID_QTC_Quy_KPK=CTCT.iID_QTC_Quy_KPK
	WHERE CT.iNamChungTu = @YearOfWork
		  --AND i = @iID_LoaiDanhMucChi
          AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  AND CT.ID_QTC_Quy_KPK=@VoucherId
		  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
		  AND CT.iQuyChungTu<@iQuyChungTu

	GROUP BY  CT.iID_MaDonVi,CTCT.iID_MucLucNganSach

	--SELECT * into #tempAgency from  f_split(N'001'); 


	--SELECT 0 AS fTienDuToanDuocGiao,
	--	  SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay,
	--	  SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,
	--	  SUM(fTienThucChi) AS fTienThucChi,
	--	  SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay
 --         --iID_MaDonVi,
 --         --iID_MucLucNganSach
	--	  --into #tblDataDaCap
	--FROM BH_QTC_Quy_KinhPhiQuanLy_chiTiet
	--WHERE iID_QTC_Quy_KinhPhiQuanLy IN
 --      (
	--	SELECT iID_QTC_Quy_KinhPhiQuanLy
 --       FROM BH_QTC_Quy_KinhPhiQuanLy
 --       WHERE iNamChungTu = 2023
	--	  --AND i = @iID_LoaiDanhMucChi
 --         AND CAST(dNgayChungTu AS DATE) <= CAST('09-20-2023' AS DATE)
	--	  AND EXISTS(SELECT * FROM #tempAgency INTERSECT SELECT * FROM f_split(N'001'))
	--	  AND iID_QTC_Quy_KinhPhiQuanLy='4033EB9C-ECDE-4D24-A41C-FB5B303B5883'
	--	  AND iID_MaDonVi IN (SELECT * FROM  f_split(N'001'))
	--	)
		
	--GROUP BY iID_MucLucNganSach
	SELECT * into #tblDataDuToan FROM #tblPhanBoDuToan

	SELECT sum(fTien_DuToanGiaoNamNay) AS fTien_DuToanGiaoNamNay
	, sum(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang
	, sum(fTien_TongDuToanDuocGiao) fTien_TongDuToanDuocGiao
	, SUM(fTienThucChi) AS fTienThucChi
	,SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet
	,SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay
	, SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay
	,iID_MaDonVi
	, iID_MucLucNganSach into #tblDataDaCapDuToan FROM (
		SELECT * FROM #tblDataDaCap
		UNION ALL
		SELECT * FROM #tblDataDuToan
		) data
	GROUP BY iID_MaDonVi, iID_MucLucNganSach;
	--select * from  #tblMlnsRoot
	SELECT mlns.iID_MLNS,
		   mlns.iID_MLNS_Cha,
		   mlns.sXauNoiMa,
		   fTien_DuToanGiaoNamNay,
		   fTien_DuToanNamTruocChuyenSang,
		   fTien_TongDuToanDuocGiao,
		   fTienThucChi,
		   fTienQuyetToanDaDuyet,
		   fTienDeNghiQuyetToanQuyNay,
		   fTienXacNhanQuyetToanQuyNay,
		   isnull(mlns.iID_MaDonVi, daCapDuToan.iID_MaDonVi) as iID_MaDonVi
		   INTO #tblDaCapDuToan
	FROM #tblMlnsRoot mlns
	LEFT JOIN
	  #tblDataDaCapDuToan daCapDuToan
	ON mlns.iID_MLNS = daCapDuToan.iID_MucLucNganSach and ((mlns.iID_MaDonVi is not null and mlns.iID_MaDonVi = daCapDuToan.iID_MaDonVi) or mlns.iID_MaDonVi is null)

	SELECT iID_MLNS, iID_MLNS_Cha, iID_MaDonVi, sum(fTien_DuToanGiaoNamNay) AS fTien_DuToanGiaoNamNay, sum(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang, SUM(fTien_TongDuToanDuocGiao) as fTien_TongDuToanDuocGiao,SUM(fTienThucChi) as fTienThucChi,SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay,SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay INTO #tblDaCapDuToanResult FROM (
		SELECT distinct T.iID_MLNS,  
			   T.iID_MLNS_Cha,
			   --isnull(T.iID_MaDonVi, T.iID_MaDonVi) as iID_MaDonVi,
			   T.iID_MaDonVi,
			   T.fTien_DuToanGiaoNamNay,
			   T.fTien_DuToanNamTruocChuyenSang,
			   T.fTien_TongDuToanDuocGiao,
			   T.fTienThucChi,
			   T.fTienQuyetToanDaDuyet,
			   T.fTienDeNghiQuyetToanQuyNay,
			   T.fTienXacNhanQuyetToanQuyNay
		FROM #tblDaCapDuToan T 
		WHERE T.fTien_DuToanGiaoNamNay <> 0 OR  T.fTien_DuToanNamTruocChuyenSang <> 0 OR  T.fTien_TongDuToanDuocGiao <> 0 OR  T.fTienThucChi <> 0 OR  T.fTienQuyetToanDaDuyet <> 0 OR  T.fTienDeNghiQuyetToanQuyNay <> 0 OR  T.fTienXacNhanQuyetToanQuyNay <> 0) data
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	OPTION (maxrecursion 0)

	-- Get data
	SELECT
		isnull(ctct.ID_QTC_Quy_KPK_ChiTiet, @iD) AS ID_QTC_Quy_KPK_ChiTiet,
		@VoucherId AS iID_QTC_Quy_KPK,
		mlnsPhanBo.iID_MLNS AS iID_MucLucNganSach,
		mlnsPhanBo.iID_MLNS_Cha AS IdParent,
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
		mlnsPhanBo.bHangCha As IsHangCha,
		@YearOfWork AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(daCapDuToan.fTien_DuToanGiaoNamNay, 0) as fTien_DuToanGiaoNamNay,
		isnull(daCapDuToan.fTien_DuToanNamTruocChuyenSang, 0) as fTien_DuToanNamTruocChuyenSang,
		isnull(daCapDuToan.fTien_TongDuToanDuocGiao, 0) as fTien_TongDuToanDuocGiao,
		isnull(daCapDuToan.fTienThucChi, 0) as fTienThucChi,
		isnull(daCapDuToan.fTienQuyetToanDaDuyet, 0) as fTienQuyetToanDaDuyet,
		isnull(daCapDuToan.fTienDeNghiQuyetToanQuyNay, 0) as fTienDeNghiQuyetToanQuyNay,
		isnull(daCapDuToan.fTienXacNhanQuyetToanQuyNay, 0) as fTienXacNhanQuyetToanQuyNay,

		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsRoot AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_QTC_Quy_KPK_ChiTiet
			WHERE 
		 		iID_QTC_Quy_KPK = @VoucherId
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach --and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_QTC_Quy_KPK ct ON ctct.iID_QTC_Quy_KPK = ct.ID_QTC_Quy_KPK
	LEFT JOIN #tblDaCapDuToanResult daCapDuToan
	ON mlnsPhanBo.iID_MLNS = daCapDuToan.iID_MLNS and daCapDuToan.iID_MaDonVi LIKE '%' + mlnsPhanBo.iID_MaDonVi + '%' 
	WHERE mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	ORDER BY mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	END
	ELSE 
		BEGIN
	-- lấy dữ liệu đã cấp
	SELECT  
		SUM(fTien_DuToanGiaoNamNay) AS fTien_DuToanGiaoNamNay,
		SUM(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang,
		SUM(fTien_TongDuToanDuocGiao) AS fTien_TongDuToanDuocGiao,
		SUM(fTienThucChi) AS fTienThucChi,
		SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,
		SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay,
		SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay,
		CT.iID_MaDonVi,
		CTCT.iID_MucLucNganSach
		into #tblDataDaCapExist
	FROM
	BH_QTC_Quy_KPK CT
	INNER JOIN BH_QTC_Quy_KPK_ChiTiet CTCT
	ON CT.ID_QTC_Quy_KPK=CTCT.iID_QTC_Quy_KPK
	WHERE CT.iNamChungTu = @YearOfWork
		  --AND i = @iID_LoaiDanhMucChi
          AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  AND CT.ID_QTC_Quy_KPK=@VoucherId
		  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
		  AND CT.iQuyChungTu=@iQuyChungTu

	GROUP BY  CT.iID_MaDonVi,CTCT.iID_MucLucNganSach


	SELECT sum(fTien_DuToanGiaoNamNay) AS fTien_DuToanGiaoNamNay
	, sum(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang
	, SUM(fTien_TongDuToanDuocGiao) AS fTien_TongDuToanDuocGiao
	,SUM(fTienThucChi) AS fTienThucChi
	,SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet
	,SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay
	,SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay
	, iID_MaDonVi, iID_MucLucNganSach into #tblDataDaCapDuToanExist FROM (
		SELECT * FROM #tblDataDaCapExist
		) data
	GROUP BY iID_MaDonVi, iID_MucLucNganSach;
	--select * from  #tblMlnsRoot
	SELECT mlns.iID_MLNS,
		   mlns.iID_MLNS_Cha,
		   mlns.sXauNoiMa,
		   fTien_DuToanGiaoNamNay,
		   fTien_DuToanNamTruocChuyenSang,
		   fTien_TongDuToanDuocGiao,
		   fTienThucChi,
		   fTienQuyetToanDaDuyet,
		   fTienDeNghiQuyetToanQuyNay,
		   fTienXacNhanQuyetToanQuyNay,
		   isnull(mlns.iID_MaDonVi, daCapDuToan.iID_MaDonVi) as iID_MaDonVi
		   INTO #tblDaCapDuToanExist
	FROM #tblMlnsRoot mlns
	LEFT JOIN
	  #tblDataDaCapDuToanExist daCapDuToan
	ON mlns.iID_MLNS = daCapDuToan.iID_MucLucNganSach and ((mlns.iID_MaDonVi is not null and mlns.iID_MaDonVi = daCapDuToan.iID_MaDonVi) or mlns.iID_MaDonVi is null)

	SELECT iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	, sum(fTien_DuToanGiaoNamNay) AS fTien_DuToanGiaoNamNay
	, sum(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang
	, SUM(fTien_TongDuToanDuocGiao) as fTien_TongDuToanDuocGiao
	, SUM(fTienThucChi) AS fTienThucChi
	, SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet
	, SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay
	, SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay INTO #tblDaCapDuToanResultExist FROM (
		SELECT distinct T.iID_MLNS,  
			   T.iID_MLNS_Cha,
			   --isnull(T.iID_MaDonVi, T.iID_MaDonVi) as iID_MaDonVi,
			   T.iID_MaDonVi,
			   T.fTien_DuToanGiaoNamNay,
			   T.fTien_DuToanNamTruocChuyenSang,
			   T.fTien_TongDuToanDuocGiao,
			   T.fTienThucChi,
			   T.fTienQuyetToanDaDuyet,
			   T.fTienDeNghiQuyetToanQuyNay,
			   T.fTienXacNhanQuyetToanQuyNay
		FROM #tblDaCapDuToanExist T 
		WHERE  T.fTien_DuToanGiaoNamNay <> 0 OR T.fTien_DuToanNamTruocChuyenSang<>0 
				OR   T.fTien_TongDuToanDuocGiao<>0 OR T.fTienThucChi <>0 
				OR T.fTienQuyetToanDaDuyet <>0 OR T.fTienDeNghiQuyetToanQuyNay <>0 
				OR T.fTienXacNhanQuyetToanQuyNay<>0) data
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	OPTION (maxrecursion 0)

	-- Get data
	SELECT
		isnull(ctct.ID_QTC_Quy_KPK_ChiTiet, @iD) AS ID_QTC_Quy_KPK_ChiTiet,
		@VoucherId AS iID_QTC_Quy_KPK,
		mlnsPhanBo.iID_MLNS AS iID_MucLucNganSach,
		mlnsPhanBo.iID_MLNS_Cha AS IdParent,
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
		mlnsPhanBo.bHangCha As IsHangCha,
		@YearOfWork AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(daCapDuToan.fTien_DuToanGiaoNamNay, 0) as fTien_DuToanGiaoNamNay,
		isnull(daCapDuToan.fTien_DuToanNamTruocChuyenSang, 0) as fTien_DuToanNamTruocChuyenSang,
		isnull(daCapDuToan.fTien_TongDuToanDuocGiao, 0) as fTien_TongDuToanDuocGiao,
		isnull(daCapDuToan.fTienThucChi, 0) as fTienThucChi,
		isnull(daCapDuToan.fTienQuyetToanDaDuyet, 0) as fTienQuyetToanDaDuyet,
		isnull(daCapDuToan.fTienDeNghiQuyetToanQuyNay, 0) as fTienDeNghiQuyetToanQuyNay,
		isnull(daCapDuToan.fTienXacNhanQuyetToanQuyNay, 0) as fTienXacNhanQuyetToanQuyNay,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsRoot AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_QTC_Quy_KPK_chiTiet
			WHERE 
		 		iID_QTC_Quy_KPK = @VoucherId
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach --and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_QTC_Quy_KPK ct ON ctct.iID_QTC_Quy_KPK = ct.ID_QTC_Quy_KPK
	LEFT JOIN #tblDaCapDuToanResultExist daCapDuToan
	ON mlnsPhanBo.iID_MLNS = daCapDuToan.iID_MLNS and daCapDuToan.iID_MaDonVi LIKE '%' + mlnsPhanBo.iID_MaDonVi + '%' 
	WHERE mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	ORDER BY mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	END
END
;
;
;
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_khac_chungtu_chitiet_tao_tonghop]    Script Date: 10/25/2023 8:53:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  -- Author:    <Author,,Name>
  -- Create date: <Create Date,,>
  -- Description:  <Description,,>
  -- =============================================
  CREATE PROCEDURE [dbo].[sp_qtc_quykinhphi_khac_chungtu_chitiet_tao_tonghop] 
  @ListIdChungTuTongHop ntext, 
  @Nguoitao nvarchar(50),
  @IdChungTu nvarchar(100), 
  @NamLamViec int
  AS BEGIN 
  INSERT INTO [dbo].BH_QTC_Quy_KPK_ChiTiet (
    ID_QTC_Quy_KPK_ChiTiet,
	iID_QTC_Quy_KPK, 
    iID_MucLucNganSach, 
    sNoiDung, 
    fTien_DuToanNamTruocChuyenSang,
	fTien_DuToanGiaoNamNay, 
    fTien_TongDuToanDuocGiao,
	fTienThucChi, 
    fTienQuyetToanDaDuyet,
	fTienDeNghiQuyetToanQuyNay,
	fTienXacNhanQuyetToanQuyNay,
	dNgaySua,
	dNgayTao, 
    sNguoiSua,
	sNguoiTao
  ) 
SELECT 
DISTINCT
  NEWID(), 
  @idChungTu, 
  iID_MucLucNganSach, 
  sNoiDung, 
  sum(fTien_DuToanNamTruocChuyenSang), 
  sum(fTien_DuToanGiaoNamNay), 
  sum(fTien_TongDuToanDuocGiao), 
  sum(fTienThucChi), 
  sum(fTienQuyetToanDaDuyet), 
  sum(fTienDeNghiQuyetToanQuyNay), 
  sum(fTienXacNhanQuyetToanQuyNay), 
  Null, 
  GETDATE(), 
  Null, 
  @Nguoitao 
FROM 
  BH_QTC_Quy_KPK_ChiTiet 
WHERE 
  iID_QTC_Quy_KPK in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) 
GROUP BY 
  iID_MucLucNganSach, 
  sNoiDung;
--danh dau chung tu da tong hop
update 
  BH_QTC_Quy_KPK 
set 
  iLoaiTongHop = 2 
where 
  ID_QTC_Quy_KPK in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) 
  
END;


GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_khac_chungtu_index_bh]    Script Date: 10/25/2023 8:53:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 13/06/2023
-- Description:	Lấy danh sách hiển thị chứng từ quyết toán chi quy kinh phí khác

-- =============================================
CREATE PROCEDURE [dbo].[sp_qtc_quykinhphi_khac_chungtu_index_bh]
	@YearOfWork int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
	DISTINCT 
		 ct.ID_QTC_Quy_KPK
		, ct.iID_DonVi
		, ct.iID_MaDonVi
		, ct.sSoChungTu
		, ct.dNgayChungTu
		, ct.sSoQuyetDinh
		, ct.dNgayQuyetDinh
		, ct.iQuyChungTu
		, ct.iNamChungTu
		, ct.sMoTa
		, ct.dNgaySua
		, ct.dNgayTao
		, ct.sNguoiSua
		, ct.sNguoiTao
		, ct.sTongHop
		, ct.iID_TongHopID
		, ct.iLoaiTongHop
		, ct.bIsKhoa
		, ct.fTongTien_DuToanNamTruocChuyenSang
		, ct.fTongTien_DuToanGiaoNamNay
		, ct.fTongTien_TongDuToanDuocGiao
		, ct.fTongTienThucChi
		, ct.fTongTienQuyetToanDaDuyet
		, ct.fTongTienDeNghiQuyetToanQuyNay
		, ct.fTongTienXacNhanQuyetToanQuyNay
		, ct.iID_LoaiChi
		, ct.sDSLNS
		, dv.sTenDonVi as STenDonVi
		, lc.sTenDanhMucLoaiChi
		-- Tong dự toán todo
	FROM BH_QTC_Quy_KPK ct
	LEFT JOIN DonVi dv on ct.iID_DonVi=dv.iID_DonVi 
	and ct.iID_MaDonVi=dv.iID_MaDonVi 
	and dv.iNamLamViec=ct.iNamChungTu
	LEFT JOIN BH_DM_LoaiChi lc on ct.iID_LoaiChi=lc.iID
	WHERE ct.iNamChungTu=@YearOfWork 
	and dv.iTrangThai=1
	Order by ct.sSoChungTu
END


GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_khacget_tienquyet_toandaduyet]    Script Date: 10/25/2023 8:53:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 20/09/2023
-- Description:	Lấy danh sách hiển thị Tien Quyet Toan Da Duyet theo quý của chứng từ quyết toán chi quy kinh phí khac chi tiết
CREATE PROCEDURE [dbo].[sp_qtc_quykinhphi_khacget_tienquyet_toandaduyet]
	@YearOfWork int,
	@AgencyId nvarchar(500),
	@VoucherDate datetime,
	@iQuyChungTu int
AS
BEGIN

	SELECT  
		SUM(fTienXacNhanQuyetToanQuyNay) AS fTienQuyetToanDaDuyet,
		CT.iID_MaDonVi,
		CTCT.iID_MucLucNganSach
	FROM
	BH_QTC_Quy_KPK CT
	INNER JOIN BH_QTC_Quy_KPK_ChiTiet CTCT
	ON CT.ID_QTC_Quy_KPK=CTCT.iID_QTC_Quy_KPK
	WHERE CT.iNamChungTu = @YearOfWork
          AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
		  AND CT.bIsKhoa=1
		  AND CT.iQuyChungTu<@iQuyChungTu
	GROUP BY  CT.iID_MaDonVi,CTCT.iID_MucLucNganSach

END

GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_khssv_nld_chungtu_tonghop_bhxh]    Script Date: 10/25/2023 8:53:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_khc_khssv_nld_chungtu_tonghop_bhxh]
	@listTenDonVi ntext,
	@namLamViec int,
	@iLoaiTongHop int,
	@listSLNS nvarchar(max)

AS
BEGIN
	SELECT 
	   dt_dv.sTenDonVi,
	   dt_dv.id idDonVi,
	   A.sM,
	   A.sLNS,
	   fTienDaThucHienNamTruocDT=SUM(IsNull(A.fTienDaThucHienNamTruoc,0)),
	   fTienUocThucHienNamTruocDT=SUM(IsNull(A.fTienUocThucHienNamTruoc,0)),
	   fTienKeHoachThucHienNamNayDT=SUM(IsNull(A.fTienKeHoachThucHienNamNay,0))
	   into #temp
FROM
  (SELECT ml.iID_MLNS ,
                ml.iID_MLNS_Cha,
                ml.sXauNoiMa,
                ml.sNG,
				ml.sM,
				ml.sLNS,
                ml.sMoTa,
				ml.bHangCha,
				ct.iID_DonVi,
				ct.iID_MaDonVi,
				ctct.fTienDaThucHienNamTruoc,
				ctct.fTienUocThucHienNamTruoc,
				ctct.fTienKeHoachThucHienNamNay

   FROM BH_KHC_K_ChiTiet ctct
   LEFT JOIN BH_KHC_K ct ON ct.iID_BH_KHC_K = ctct.iID_KHC_K
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec = @namLamViec--@namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS IN  (SELECT *
     FROM f_split(@listSLNS))
   WHERE ct.iNamChungTu = @namLamViec--@namLamViec
	 AND ct.iLoaiTongHop = @iLoaiTongHop) AS A 
   LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai = 1
   AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id  
GROUP BY dt_dv.sTenDonVi,
		dt_dv.id,
		 A.sM,A.sLNS;

SELECT dt.idDonVi, 
dt.sTenDonVI, 
dt.sM,
dt.sLNS,
IsNull(dt.fTienDaThucHienNamTruocDT, 0) TienDaThucHienNamTruocDT,
IsNull(dt.fTienUocThucHienNamTruocDT, 0) TienUocThucHienNamTruocDT,
IsNull(dt.fTienKeHoachThucHienNamNayDT, 0) TienKeHoachThucHienNamNayDT
FROM #temp dt
WHERE dt.idDonVi in 
    (SELECT *
     FROM f_split(@listTenDonVi))	 
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_ktsdk_chungtu_tonghop_bhxh]    Script Date: 10/25/2023 8:53:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[sp_rpt_khc_ktsdk_chungtu_tonghop_bhxh]
	@listTenDonVi ntext,
	@namLamViec int,
	@iLoaiTongHop int,
	@sLNSDuToan nvarchar(max),
	@sLNSHachToan nvarchar(max)
AS
BEGIN
declare @DataDuToan table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), sM nvarchar(50),sLNSDT nvarchar(50), 
fTienDaThucHienNamTruocDT float,fTienUocThucHienNamTruocDT float, fTienKeHoachThucHienNamNayDT float
);
declare @DataHachToan table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), sM nvarchar(50),sLNSHT nvarchar(50),
fTienDaThucHienNamTruocHT float,fTienUocThucHienNamTruocHT float, fTienKeHoachThucHienNamNayHT float
);

INSERT INTO @DataDuToan (idDonVi,sTenDonVI , sM,sLNSDT ,
fTienDaThucHienNamTruocDT ,fTienUocThucHienNamTruocDT , fTienKeHoachThucHienNamNayDT 
)
	SELECT 
	   dt_dv.sTenDonVi,
	   dt_dv.id idDonVi,
	   A.sM,
	   A.sLNS,
	   TienDaThucHienNamTruoc=SUM(IsNull(A.fTienDaThucHienNamTruoc,0)),
	   TienUocThucHienNamTruoc=SUM(IsNull(A.fTienUocThucHienNamTruoc,0)),
	   TienKeHoachThucHienNamNay=SUM(IsNull(A.fTienKeHoachThucHienNamNay,0))


FROM
  (SELECT ml.iID_MLNS ,
                ml.iID_MLNS_Cha,
                ml.sXauNoiMa,
                ml.sNG,
				ml.sM,
				ml.sLNS,
                ml.sMoTa,
				ml.bHangCha,
				ct.iID_DonVi,
				ct.iID_MaDonVi,

				ctct.fTienDaThucHienNamTruoc,
				ctct.fTienUocThucHienNamTruoc,
				ctct.fTienKeHoachThucHienNamNay

   FROM BH_KHC_K_ChiTiet ctct
   LEFT JOIN BH_KHC_K ct ON ct.iID_BH_KHC_K = ctct.iID_KHC_K
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec = @namLamViec--@namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS = @sLNSDuToan
   WHERE ct.iNamChungTu = @namLamViec--@namLamViec
	 AND ct.iLoaiTongHop = @iLoaiTongHop) AS A 
   LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai = 1
   AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   --AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
GROUP BY dt_dv.sTenDonVi,
		dt_dv.id,
		 A.sM,A.sLNS;

INSERT INTO @DataHachToan (idDonVi,sTenDonVI , sM,sLNSHT ,
fTienDaThucHienNamTruocHT ,fTienUocThucHienNamTruocHT , fTienKeHoachThucHienNamNayHT 
)
	SELECT 
	   dt_dv.sTenDonVi,
	   dt_dv.id idDonVi,
	   A.sM,
	   A.sLNS,

	   TienDaThucHienNamTruoc=SUM(IsNull(A.fTienDaThucHienNamTruoc,0)),
	   TienUocThucHienNamTruoc=SUM(IsNull(A.fTienUocThucHienNamTruoc,0)),
	   TienKeHoachThucHienNamNay=SUM(IsNull(A.fTienKeHoachThucHienNamNay,0))

FROM
  (SELECT ml.iID_MLNS ,
                ml.iID_MLNS_Cha,
                ml.sXauNoiMa,
                ml.sNG,
				ml.sM,
				ml.sLNS,
                ml.sMoTa,
				ml.bHangCha,
				ct.iID_DonVi,
				ct.iID_MaDonVi,

				ctct.fTienDaThucHienNamTruoc,
				ctct.fTienUocThucHienNamTruoc,
				ctct.fTienKeHoachThucHienNamNay

   FROM BH_KHC_K_ChiTiet ctct
   LEFT JOIN BH_KHC_K ct ON ct.iID_BH_KHC_K = ctct.iID_KHC_K
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec =@namLamViec --@namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS = @sLNSHachToan
   WHERE ct.iNamChungTu =@namLamViec --@namLamViec
	 AND ct.iLoaiTongHop = @iLoaiTongHop) AS A 
   LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai = 1
   --AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
GROUP BY dt_dv.sTenDonVi,
		dt_dv.id,
		 A.sM,A.sLNS;

SELECT dt.idDonVi, 
dt.sTenDonVI, 
dt.sM,
dt.sLNSDT,
ht.sLNSHT,

IsNull(dt.fTienDaThucHienNamTruocDT, 0) TienDaThucHienNamTruocDT,
IsNull(ht.fTienDaThucHienNamTruocHT, 0) TienDaThucHienNamTruocHT,

IsNull(dt.fTienUocThucHienNamTruocDT, 0) TienUocThucHienNamTruocDT,
IsNull(ht.fTienUocThucHienNamTruocHT, 0) TienUocThucHienNamTruocHT,

IsNull(dt.fTienKeHoachThucHienNamNayDT, 0) TienKeHoachThucHienNamNayDT,
IsNull(ht.fTienKeHoachThucHienNamNayHT, 0) TienKeHoachThucHienNamNayHT

FROM @DataDuToan dt
LEFT JOIN @DataHachToan ht ON dt.idDonVi = ht.idDonVi
where dt.sTenDonVI in 
    (SELECT *
     FROM f_split(@listTenDonVi))

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nkpk_kehoach_bh]    Script Date: 10/25/2023 8:53:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chi tiet chung tu 
Create PROCEDURE [dbo].[sp_rpt_qtc_nkpk_kehoach_bh]
	 @NamLamViec int,
	 @iD uniqueidentifier,
	 @IdDonVi nvarchar(max),
	 @LNS nvarchar(max)
AS
BEGIN 
	SET NOCOUNT ON;
	DECLARE @iDCT uniqueidentifier='00000000-0000-0000-0000-000000000000';
	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
			into #tblMlnsByPhanCap
	FROM BH_DM_MucLucNganSach 
	WHERE 
		iNamLamViec = 2023 
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
								WHERE iNamChungTu=@NamLamViec
								AND iID_QTC_Nam_KPK=@iD
								AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
								)) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
	Order by mlns.sXauNoiMa
END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nkpk_phucluc_bh]    Script Date: 10/25/2023 8:53:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[sp_rpt_qtc_nkpk_phucluc_bh]
	@listTenDonVi ntext,
	@namLamViec int,
	@LNS nvarchar(200)
AS
BEGIN
declare @DataKhoi table (idDonVi nvarchar(50),sTenDonVI nvarchar(200),Loai int,
FTienDaThucHienNamTruoc float,FTienNamNay float,FTienCong float,FTienQuyetToan float, FTienThua float,FTienThieu float
);

INSERT INTO @DataKhoi (idDonVi,sTenDonVI , Loai,
FTienDaThucHienNamTruoc ,FTienNamNay , FTienCong ,FTienQuyetToan , FTienThua ,FTienThieu 
)
	SELECT 
	   dt_dv.sTenDonVi,
	   dt_dv.id idDonVi,
	   dt_dv.iLoai Loai,
	   FTienDaThucHienNamTruoc=SUM(IsNull(A.fTien_DuToanNamTruocChuyenSang,0)),
	   FTienNamNay=SUM(IsNull(A.fTien_DuToanGiaoNamNay,0)),
	   FTienQuyetToan=SUM(IsNull(A.fTien_TongDuToanDuocGiao,0)),
	   FTienCong=SUM(IsNull(A.fTien_ThucChi,0)),
	   FTienThua=SUM(IsNull(A.fTienThua,0)),
	   FTienThieu=SUM(IsNull(A.fTienThieu,0))
FROM
  (SELECT 
				ct.iID_DonVi,
				ct.iID_MaDonVi,
				ctct.fTien_DuToanNamTruocChuyenSang,
				ctct.fTien_DuToanGiaoNamNay,
				ctct.fTien_TongDuToanDuocGiao,
				ctct.fTien_ThucChi,
				ctct.fTienThua,
				ctct.fTienThieu,
				ctct.fTiLeThucHienTrenDuToan
   FROM BH_QTC_Nam_KPK_ChiTiet ctct
   LEFT JOIN BH_QTC_Nam_KPK ct ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec = @namLamViec--@namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS IN  (SELECT * FROM f_split(@LNS))
   WHERE ct.iNamChungTu = @namLamViec--@namLamViec
	---
	) AS A 
   LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai = 1
   AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   --AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
GROUP BY dt_dv.sTenDonVi,
		dt_dv.id,
		dt_dv.iLoai;

SELECT dt.idDonVi, 
dt.sTenDonVI, 
dt.Loai,
IsNull(dt.FTienDaThucHienNamTruoc, 0) FTienDaThucHienNamTruoc,
IsNull(dt.FTienNamNay, 0) FTienNamNay,
IsNull(dt.FTienCong, 0) FTienCong,
IsNull(dt.FTienQuyetToan, 0) FTienQuyetToan,
IsNull(dt.FTienThua, 0) FTienThua,
IsNull(dt.FTienThieu, 0) FTienThieu
FROM @DataKhoi dt
where dt.sTenDonVI in 
    (SELECT *
     FROM f_split(@listTenDonVi))
END
;


GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]    Script Date: 10/25/2023 8:53:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chi tiet chung tu 
Create PROCEDURE [dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]
	 @NamLamViec int,
	 @iD uniqueidentifier,
	 @IdDonVi nvarchar(max),
	 @LNS nvarchar(max)
AS
BEGIN 
	SET NOCOUNT ON;
	DECLARE @iDCT uniqueidentifier='00000000-0000-0000-0000-000000000000';
	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
			into #tblMlnsByPhanCap
	FROM BH_DM_MucLucNganSach 
	WHERE 
		iNamLamViec = 2023 
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
		@NamLamViec AS INamLamViec,
		mlns.bHangCha as IsHangCha,
		isnull(ctct.dNgayTao, getdate()) AS dNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS dNgaySua,
		ctct.sNguoiTao AS sNguoiTao,
		ctct.sNguoiSua AS sNguoiSua,
		ISNULL(ctct.fTien_DuToanNamTruocChuyenSang, 0)  as fTien_DuToanNamTruocChuyenSang, 
		ISNULL(ctct.fTien_DuToanGiaoNamNay, 0)  as fTien_DuToanGiaoNamNay,
		ISNULL(ctct.fTien_DuToanNamTruocChuyenSang, 0) + ISNULL(ctct.fTien_DuToanGiaoNamNay, 0)  as fTien_TongDuToanDuocGiao, 
		ISNULL(ctct.fTien_ThucChi, 0)  as fTien_ThucChi, 
		ISNULL(ctct.fTien_ThucChi, 0)  as fTienThua, 
		ISNULL(ctct.fTienThieu, 0)  as fTienThieu, 
		ISNULL(ctct.fTiLeThucHienTrenDuToan, 0)  as fTiLeThucHienTrenDuToan
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(SELECT * FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet 
				WHERE iID_QTC_Nam_KinhPhiQuanLy in
					( SELECT ID_QTC_Nam_KinhPhiQuanLy FROM BH_QTC_Nam_KinhPhiQuanLy
								WHERE iNamChungTu=@NamLamViec
								AND ID_QTC_Nam_KinhPhiQuanLy=@iD
								AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
								)) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
	Order by mlns.sXauNoiMa
END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nqlkp_phucluc_bh]    Script Date: 10/25/2023 8:53:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_qtc_nqlkp_phucluc_bh]
	@listTenDonVi ntext,
	@namLamViec int
AS
BEGIN
declare @DataKhoi table (idDonVi nvarchar(50),sTenDonVI nvarchar(200),Loai int,
FTienDaThucHienNamTruoc float,FTienNamNay float,FTienCong float,FTienQuyetToan float, FTienThua float,FTienThieu float
);

INSERT INTO @DataKhoi (idDonVi,sTenDonVI , Loai,
FTienDaThucHienNamTruoc ,FTienNamNay , FTienCong ,FTienQuyetToan , FTienThua ,FTienThieu 
)
	SELECT 
	   dt_dv.sTenDonVi,
	   dt_dv.id idDonVi,
	   dt_dv.iLoai Loai,
	   FTienDaThucHienNamTruoc=SUM(IsNull(A.fTien_DuToanNamTruocChuyenSang,0)),
	   FTienNamNay=SUM(IsNull(A.fTien_DuToanGiaoNamNay,0)),
	   FTienQuyetToan=SUM(IsNull(A.fTien_TongDuToanDuocGiao,0)),
	   FTienCong=SUM(IsNull(A.fTien_ThucChi,0)),
	   FTienThua=SUM(IsNull(A.fTienThua,0)),
	   FTienThieu=SUM(IsNull(A.fTienThieu,0))
FROM
  (SELECT 
				ct.iID_DonVi,
				ct.iID_MaDonVi,
				ctct.fTien_DuToanNamTruocChuyenSang,
				ctct.fTien_DuToanGiaoNamNay,
				ctct.fTien_TongDuToanDuocGiao,
				ctct.fTien_ThucChi,
				ctct.fTienThua,
				ctct.fTienThieu,
				ctct.fTiLeThucHienTrenDuToan
   FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet ctct
   LEFT JOIN BH_QTC_Nam_KinhPhiQuanLy ct ON ct.ID_QTC_Nam_KinhPhiQuanLy = ctct.iID_QTC_Nam_KinhPhiQuanLy
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec = @namLamViec--@namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS =9010003 --9020000
   WHERE ct.iNamChungTu = @namLamViec--@namLamViec
	---
	) AS A 
   LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai = 1
   AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   --AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
GROUP BY dt_dv.sTenDonVi,
		dt_dv.id,
		dt_dv.iLoai;

SELECT dt.idDonVi, 
dt.sTenDonVI, 
dt.Loai,
IsNull(dt.FTienDaThucHienNamTruoc, 0) FTienDaThucHienNamTruoc,
IsNull(dt.FTienNamNay, 0) FTienNamNay,
IsNull(dt.FTienCong, 0) FTienCong,
IsNull(dt.FTienQuyetToan, 0) FTienQuyetToan,
IsNull(dt.FTienThua, 0) FTienThua,
IsNull(dt.FTienThieu, 0) FTienThieu
FROM @DataKhoi dt
where dt.sTenDonVI in 
    (SELECT *
     FROM f_split(@listTenDonVi))
END
;


GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkp_ql_chungtu_chi_tiet]    Script Date: 10/25/2023 8:53:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 20/09/2023
-- Description:	Lấy danh sách report chứng từ quyết toán chi quy kinh phí quản lý chi tiết

-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_qtc_qkp_ql_chungtu_chi_tiet]
	@VoucherId uniqueidentifier,
	@LNS nvarchar(max),
	@YearOfWork int,
	@AgencyId nvarchar(500),
	@VoucherDate datetime,
	@iID_LoaiDanhMucChi uniqueidentifier,
	@iQuyChungTu int
AS
BEGIN
	DECLARE @iD uniqueidentifier='00000000-0000-0000-0000-000000000000';
	DECLARE @CountIndex INT;
	SELECT * into #tempLNS from f_split(@LNS);
	SELECT * into #tempAgency from  f_split(@AgencyId);
	SELECT @CountIndex = COUNT(*) FROM 
									BH_QTC_Quy_KinhPhiQuanLy_ChiTiet 
									WHERE iID_QTC_Quy_KinhPhiQuanLy =@VoucherId

	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
			into #tblNsMlns
	FROM BH_DM_MucLucNganSach 
	where iNamLamViec = @YearOfWork 

		-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(fTongTien) AS fTienDuToanDuocGiao,
          0 AS fTienDeNghiQuyetToanQuyNay,
          0 AS fTienQuyetToanDaDuyet,
		  0 AS fTienThucChi,
		  0 AS fTienXacNhanQuyetToanQuyNay,
          iID_MaDonVi,
          iID_MucLucNganSach
		  into #tblPhanBoDuToan
   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
   WHERE iID_DTC_PhanBoDuToanChi IN
       (SELECT ID
        FROM BH_DTC_PhanBoDuToanChi
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iNamChungTu = @YearOfWork
          AND iID_LoaiDanhMucChi = @iID_LoaiDanhMucChi
		  AND bIsKhoa=1
          AND cast(dNgayQuyetDinh AS date) <= cast(@VoucherDate AS date))
     AND iID_MaDonVi in  (SELECT * FROM f_split(@AgencyId))-- donvi
   GROUP BY iID_MaDonVi, 
   iID_MucLucNganSach

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
		INamLamViec = @YearOfWork 
		AND iID_MaDonVi IN (SELECT * FROM #tempAgency)

	-- Tao bang tam luu chu mlng con
	SELECT * INTO #tblMlnsByPhanCapExistDonVi
	FROM #tblMlnsByPhanCap tbl, #tblDonVi dv
	WHERE  tbl.bHangCha = 0 OR (tbl.bHangCha = 1 and (IsNull(tbl.sM, '') != '' OR tbl.sM <> '' OR IsNull(tbl.sTM, '') != '')  )
	AND tbl.sTM !=''  


	-- Tao bang tam luu chu mlng cha
	SELECT *, '' AS iID_MaDonVi, '' AS sTenDonVi  INTO #tblMlnsByPhanCapNotDonVi
	FROM #tblMlnsByPhanCap 
		WHERE bHangCha = 1 AND ( (IsNull(sM, '') = '' or sM = '')) AND ( (IsNull(sTM, '') = '' or sTM = '')) OR ((ISNULL(sTM,'')='' OR sTM='')) OR sTM='0'

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
		WHERE bHangCha = 0
		UNION ALL
		SELECT *, null AS iID_MaDonVi, null AS sTenDonVi  
		FROM #tblNsMlns 
		WHERE bHangCha = 1
	) mlns

	IF @CountIndex=0
	BEGIN
	-- lấy dữ liệu đã cấp
	SELECT  
		0 AS fTienDuToanDuocGiao,
		SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay,
		SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,
		SUM(fTienThucChi) AS fTienThucChi,
		SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay,
		CT.iID_MaDonVi,
	    CTCT.iID_MucLucNganSach
		into #tblDataDaCap
	FROM
	BH_QTC_Quy_KinhPhiQuanLy CT
	INNER JOIN BH_QTC_Quy_KinhPhiQuanLy_chiTiet CTCT
	ON CT.ID_QTC_Quy_KinhPhiQuanLy=CTCT.iID_QTC_Quy_KinhPhiQuanLy
	WHERE CT.iNamChungTu = @YearOfWork
		  --AND i = @iID_LoaiDanhMucChi
          AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  AND CT.ID_QTC_Quy_KinhPhiQuanLy=@VoucherId
		  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
		  AND CT.iQuyChungTu<@iQuyChungTu

	GROUP BY  CT.iID_MaDonVi,CTCT.iID_MucLucNganSach

	--SELECT * into #tempAgency from  f_split(N'001'); 


	--SELECT 0 AS fTienDuToanDuocGiao,
	--	  SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay,
	--	  SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,
	--	  SUM(fTienThucChi) AS fTienThucChi,
	--	  SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay
 --         --iID_MaDonVi,
 --         --iID_MucLucNganSach
	--	  --into #tblDataDaCap
	--FROM BH_QTC_Quy_KinhPhiQuanLy_chiTiet
	--WHERE iID_QTC_Quy_KinhPhiQuanLy IN
 --      (
	--	SELECT iID_QTC_Quy_KinhPhiQuanLy
 --       FROM BH_QTC_Quy_KinhPhiQuanLy
 --       WHERE iNamChungTu = 2023
	--	  --AND i = @iID_LoaiDanhMucChi
 --         AND CAST(dNgayChungTu AS DATE) <= CAST('09-20-2023' AS DATE)
	--	  AND EXISTS(SELECT * FROM #tempAgency INTERSECT SELECT * FROM f_split(N'001'))
	--	  AND iID_QTC_Quy_KinhPhiQuanLy='4033EB9C-ECDE-4D24-A41C-FB5B303B5883'
	--	  AND iID_MaDonVi IN (SELECT * FROM  f_split(N'001'))
	--	)
		
	--GROUP BY iID_MucLucNganSach
	SELECT * into #tblDataDuToan FROM #tblPhanBoDuToan

	SELECT sum(fTienDuToanDuocGiao) AS fTienDuToanDuocGiao, sum(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay, sum(fTienQuyetToanDaDuyet) fTienQuyetToanDaDuyet, SUM(fTienThucChi) AS fTienThucChi,SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay, iID_MaDonVi, iID_MucLucNganSach into #tblDataDaCapDuToan FROM (
		SELECT * FROM #tblDataDaCap
		UNION ALL
		SELECT * FROM #tblDataDuToan
		) data
	GROUP BY iID_MaDonVi, iID_MucLucNganSach;
	--select * from  #tblMlnsRoot
	SELECT mlns.iID_MLNS,
		   mlns.iID_MLNS_Cha,
		   mlns.sXauNoiMa,
		   fTienDuToanDuocGiao,
		   fTienDeNghiQuyetToanQuyNay,
		   fTienQuyetToanDaDuyet,
		   fTienThucChi,
		   fTienXacNhanQuyetToanQuyNay,
		   isnull(mlns.iID_MaDonVi, daCapDuToan.iID_MaDonVi) as iID_MaDonVi
		   INTO #tblDaCapDuToan
	FROM #tblMlnsRoot mlns
	LEFT JOIN
	  #tblDataDaCapDuToan daCapDuToan
	ON mlns.iID_MLNS = daCapDuToan.iID_MucLucNganSach and ((mlns.iID_MaDonVi is not null and mlns.iID_MaDonVi = daCapDuToan.iID_MaDonVi) or mlns.iID_MaDonVi is null)

	SELECT iID_MLNS, iID_MLNS_Cha, iID_MaDonVi, sum(fTienDuToanDuocGiao) AS fTienDuToanDuocGiao, sum(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay, SUM(fTienQuyetToanDaDuyet) as fTienQuyetToanDaDuyet,SUM(fTienThucChi) as fTienThucChi,SUM(fTienXacNhanQuyetToanQuyNay) as fTienXacNhanQuyetToanQuyNay INTO #tblDaCapDuToanResult FROM (
		SELECT distinct T.iID_MLNS,  
			   T.iID_MLNS_Cha,
			   --isnull(T.iID_MaDonVi, T.iID_MaDonVi) as iID_MaDonVi,
			   T.iID_MaDonVi,
			   T.fTienDuToanDuocGiao,
			   T.fTienDeNghiQuyetToanQuyNay,
			   T.fTienQuyetToanDaDuyet,
			   T.fTienThucChi,
			   T.fTienXacNhanQuyetToanQuyNay
		FROM #tblDaCapDuToan T 
		WHERE T.fTienDuToanDuocGiao <> 0) data
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	OPTION (maxrecursion 0)

	-- Get data
	SELECT
		isnull(ctct.ID_QTC_Quy_KinhPhiQuanLy_ChiTiet, @iD) AS ID_QTC_Quy_KinhPhiQuanLy_ChiTiet,
		@VoucherId AS iID_QTC_Quy_KinhPhiQuanLy,
		mlnsPhanBo.iID_MLNS AS iID_MucLucNganSach,
		mlnsPhanBo.iID_MLNS_Cha AS IdParent,
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
		mlnsPhanBo.bHangCha As IsHangCha,
		@YearOfWork AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(daCapDuToan.fTienDeNghiQuyetToanQuyNay, 0) as FTienDeNghiQuyetToanQuyNay,
		isnull(daCapDuToan.fTienDuToanDuocGiao, 0) as FTienDuToanDuocGiao,
		isnull(daCapDuToan.fTienQuyetToanDaDuyet, 0) as FTienQuyetToanDaDuyet,
		isnull(daCapDuToan.fTienThucChi, 0) as FTienThucChi,
		isnull(daCapDuToan.fTienXacNhanQuyetToanQuyNay, 0) as FTienXacNhanQuyetToanQuyNay,
		ctct.sGhiChu AS SGhiChu,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsRoot AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_QTC_Quy_KinhPhiQuanLy_chiTiet
			WHERE 
		 		iID_QTC_Quy_KinhPhiQuanLy = @VoucherId
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach --and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_QTC_Quy_KinhPhiQuanLy ct ON ctct.iID_QTC_Quy_KinhPhiQuanLy = ct.ID_QTC_Quy_KinhPhiQuanLy
	LEFT JOIN #tblDaCapDuToanResult daCapDuToan
	ON mlnsPhanBo.iID_MLNS = daCapDuToan.iID_MLNS and daCapDuToan.iID_MaDonVi LIKE '%' + mlnsPhanBo.iID_MaDonVi + '%' 
	WHERE mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	ORDER BY mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	END
	ELSE 
		BEGIN
	-- lấy dữ liệu đã cấp
	SELECT  
		SUM(fTienDuToanDuocGiao) AS fTienDuToanDuocGiao,
		SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay,
		SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,
		SUM(fTienThucChi) AS fTienThucChi,
		SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay,
		CT.iID_MaDonVi,
		CTCT.iID_MucLucNganSach
		into #tblDataDaCapExist
	FROM
	BH_QTC_Quy_KinhPhiQuanLy CT
	INNER JOIN BH_QTC_Quy_KinhPhiQuanLy_chiTiet CTCT
	ON CT.ID_QTC_Quy_KinhPhiQuanLy=CTCT.iID_QTC_Quy_KinhPhiQuanLy
	WHERE CT.iNamChungTu = @YearOfWork
		  --AND i = @iID_LoaiDanhMucChi
          AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  AND CT.ID_QTC_Quy_KinhPhiQuanLy=@VoucherId
		  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
		  AND CT.iQuyChungTu=@iQuyChungTu

	GROUP BY  CT.iID_MaDonVi,CTCT.iID_MucLucNganSach


	SELECT sum(fTienDuToanDuocGiao) AS fTienDuToanDuocGiao, sum(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay, SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,SUM(fTienThucChi) AS fTienThucChi,SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay, iID_MaDonVi, iID_MucLucNganSach into #tblDataDaCapDuToanExist FROM (
		SELECT * FROM #tblDataDaCapExist
		) data
	GROUP BY iID_MaDonVi, iID_MucLucNganSach;
	--select * from  #tblMlnsRoot
	SELECT mlns.iID_MLNS,
		   mlns.iID_MLNS_Cha,
		   mlns.sXauNoiMa,
		   fTienDuToanDuocGiao,
		   fTienDeNghiQuyetToanQuyNay,
		   fTienQuyetToanDaDuyet,
		   fTienThucChi,
		   fTienXacNhanQuyetToanQuyNay,
		   isnull(mlns.iID_MaDonVi, daCapDuToan.iID_MaDonVi) as iID_MaDonVi
		   INTO #tblDaCapDuToanExist
	FROM #tblMlnsRoot mlns
	LEFT JOIN
	  #tblDataDaCapDuToanExist daCapDuToan
	ON mlns.iID_MLNS = daCapDuToan.iID_MucLucNganSach and ((mlns.iID_MaDonVi is not null and mlns.iID_MaDonVi = daCapDuToan.iID_MaDonVi) or mlns.iID_MaDonVi is null)

	SELECT iID_MLNS, iID_MLNS_Cha, iID_MaDonVi, sum(fTienDuToanDuocGiao) AS fTienDuToanDuocGiao, sum(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay, SUM(fTienQuyetToanDaDuyet) as fTienQuyetToanDaDuyet,SUM(fTienThucChi) AS fTienThucChi, SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay INTO #tblDaCapDuToanResultExist FROM (
		SELECT distinct T.iID_MLNS,  
			   T.iID_MLNS_Cha,
			   --isnull(T.iID_MaDonVi, T.iID_MaDonVi) as iID_MaDonVi,
			   T.iID_MaDonVi,
			   T.fTienDuToanDuocGiao,
			   T.fTienDeNghiQuyetToanQuyNay,
			   T.fTienQuyetToanDaDuyet,
			   T.fTienThucChi,
			   T.fTienXacNhanQuyetToanQuyNay
		FROM #tblDaCapDuToanExist T 
		WHERE  T.fTienDuToanDuocGiao <> 0 OR T.fTienDeNghiQuyetToanQuyNay<>0 OR   T.fTienQuyetToanDaDuyet<>0 OR T.fTienThucChi <>0 OR T.fTienXacNhanQuyetToanQuyNay<>0) data
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	OPTION (maxrecursion 0)

	-- Get data
	SELECT
		isnull(ctct.ID_QTC_Quy_KinhPhiQuanLy_ChiTiet, @iD) AS ID_QTC_Quy_KinhPhiQuanLy_ChiTiet,
		@VoucherId AS iID_QTC_Quy_KinhPhiQuanLy,
		mlnsPhanBo.iID_MLNS AS iID_MucLucNganSach,
		mlnsPhanBo.iID_MLNS_Cha AS IdParent,
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
		mlnsPhanBo.bHangCha As IsHangCha,
		@YearOfWork AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(daCapDuToan.fTienDeNghiQuyetToanQuyNay, 0) as fTienDeNghiQuyetToanQuyNay,
		isnull(daCapDuToan.fTienDuToanDuocGiao, 0) as fTienDuToanDuocGiao,
		isnull(daCapDuToan.fTienQuyetToanDaDuyet, 0) as fTienQuyetToanDaDuyet,
		isnull(daCapDuToan.fTienThucChi, 0) as fTienThucChi,
		isnull(daCapDuToan.fTienXacNhanQuyetToanQuyNay, 0) as fTienXacNhanQuyetToanQuyNay,
		ctct.sGhiChu AS SGhiChu,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsRoot AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_QTC_Quy_KinhPhiQuanLy_chiTiet
			WHERE 
		 		iID_QTC_Quy_KinhPhiQuanLy = @VoucherId
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach --and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_QTC_Quy_KinhPhiQuanLy ct ON ctct.iID_QTC_Quy_KinhPhiQuanLy = ct.ID_QTC_Quy_KinhPhiQuanLy
	LEFT JOIN #tblDaCapDuToanResultExist daCapDuToan
	ON mlnsPhanBo.iID_MLNS = daCapDuToan.iID_MLNS and daCapDuToan.iID_MaDonVi LIKE '%' + mlnsPhanBo.iID_MaDonVi + '%' 
	WHERE mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	ORDER BY mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	END
END
;
;
;
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkp_ql_thongtriloai1_bh]    Script Date: 10/25/2023 8:53:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chung tu theo don vi và theo lns
CREATE PROCEDURE [dbo].[sp_rpt_qtc_qkp_ql_thongtriloai1_bh]
	 @NamLamViec int,
	 @iQuy nvarchar(100),
	 @IdDonVi nvarchar(max),
	 @UserName nvarchar(100),
	 @iLoaiTongHop int,
	 @Dvt int
AS
BEGIN 
	SET NOCOUNT ON;

	IF @iLoaiTongHop=1
	BEGIN
	SELECT  ct.iID_MaDonVi as SMaDonVi,
		ISNULL(ct.fTongTienXacNhanQuyetToanQuyNay,0)/ @Dvt FTongTienXacNhanQuyetToanQuyNay,
		ct.iID_DonVi as IID_DonVi,
		dv.sTenDonVi as STenDonVi
		FROM BH_QTC_Quy_KinhPhiQuanLy ct
		LEFT JOIN DonVi dv ON ct.iID_DonVi=dv.iID_DonVi
			WHERE ct.iNamChungTu=@NamLamViec
				    AND ct.iQuyChungTu=@iQuy
					AND ct.bIsKhoa=1
					AND ct.iLoaiTongHop=@iLoaiTongHop
					AND ct.iID_MaDonVi IN (select * from f_split(@IdDonVi))
	END
	ELSE
	BEGIN
	SELECT  ct.iID_MaDonVi as SMaDonVi,
		ISNULL(ct.fTongTienXacNhanQuyetToanQuyNay,0)/ @Dvt FTongTienXacNhanQuyetToanQuyNay,
		ct.iID_DonVi as IID_DonVi,
		dv.sTenDonVi as STenDonVi
		FROM BH_QTC_Quy_KinhPhiQuanLy ct
		LEFT JOIN DonVi dv ON ct.iID_DonVi=dv.iID_DonVi
			WHERE ct.iNamChungTu=@NamLamViec
				    AND ct.iQuyChungTu=@iQuy
					--AND ct.bIsKhoa=1
					AND ct.iLoaiTongHop=@iLoaiTongHop
					AND ct.iID_MaDonVi IN (select * from f_split(@IdDonVi))
	END

END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkp_ql_thongtriloai2_bh]    Script Date: 10/25/2023 8:53:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chung tu theo don vi và theo lns
CREATE PROCEDURE [dbo].[sp_rpt_qtc_qkp_ql_thongtriloai2_bh]
	 @NamLamViec int,
	 @iQuy nvarchar(100),
	 @IdDonVi nvarchar(max),
	 @LNS nvarchar(max),
	 @UserName nvarchar(100),
	 @LoaiTongHop int,
	 @Dvt int
AS
BEGIN 
	SET NOCOUNT ON;
	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
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

IF @LoaiTongHop=1
BEGIN
SELECT 
		mlns.iID_MLNS as IdMlns,
		mlns.iID_MLNS_Cha as IdMlnsCha,
		mlns.sXauNoiMa as XauNoiMa,
		mlns.sLNS as SLNS,
		mlns.sL as SL,
		mlns.sK as SK,
		mlns.sM as SM,
		mlns.sTM as STM,
		mlns.sTTM as STTM,
		mlns.sNG as SNG,
		mlns.sTNG as STNG,
		mlns.sMoTa as SMoTa,
		mlns.bHangCha as IsHangCha,
		ISNULL(ctct.fTienXacNhanQuyetToanQuyNay, 0) / @Dvt as FTienXacNhanQuyetToanQuyNay 
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(SELECT * FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet 
				WHERE iID_QTC_Quy_KinhPhiQuanLy in
					( SELECT ID_QTC_Quy_KinhPhiQuanLy FROM BH_QTC_Quy_KinhPhiQuanLy
								WHERE iNamChungTu=@NamLamViec
								AND iQuyChungTu=@iQuy
								AND bIsKhoa=1
								AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
								)) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
	Order by mlns.sXauNoiMa
END
ELSE
BEGIN
SELECT 
		mlns.iID_MLNS as IdMlns,
		mlns.iID_MLNS_Cha as IdMlnsCha,
		mlns.sXauNoiMa as XauNoiMa,
		mlns.sLNS as SLNS,
		mlns.sL as SL,
		mlns.sK as SK,
		mlns.sM as SM,
		mlns.sTM as STM,
		mlns.sTTM as STTM,
		mlns.sNG as SNG,
		mlns.sTNG as STNG,
		mlns.sMoTa as SMoTa,
		mlns.bHangCha as IsHangCha,
		ISNULL(ctct.fTienXacNhanQuyetToanQuyNay, 0) / @Dvt as FTienXacNhanQuyetToanQuyNay 
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(SELECT * FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet 
				WHERE iID_QTC_Quy_KinhPhiQuanLy in
					( SELECT ID_QTC_Quy_KinhPhiQuanLy FROM BH_QTC_Quy_KinhPhiQuanLy
								WHERE iNamChungTu=@NamLamViec
								AND iQuyChungTu=@iQuy
								AND iLoaiTongHop=@LoaiTongHop
								AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
								)) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
	Order by mlns.sXauNoiMa
END
END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkpk_kehoach_bh]    Script Date: 10/25/2023 8:53:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 20/09/2023
-- Description:	Lấy danh sách report hiển thị chứng từ quyết toán chi quy kinh phí khác chi tiết

-- =============================================
Create PROCEDURE [dbo].[sp_rpt_qtc_qkpk_kehoach_bh]
	@VoucherId uniqueidentifier,
	@LNS nvarchar(max),
	@YearOfWork int,
	@AgencyId nvarchar(500),
	@VoucherDate datetime,
	@iID_LoaiDanhMucChi uniqueidentifier,
	@iQuyChungTu int
AS
BEGIN
	DECLARE @iD uniqueidentifier='00000000-0000-0000-0000-000000000000';
	DECLARE @CountIndex INT;
	SELECT * into #tempLNS from f_split(@LNS);
	SELECT * into #tempAgency from  f_split(@AgencyId);
	SELECT @CountIndex = COUNT(*) FROM 
									BH_QTC_Quy_KPK_ChiTiet 
									WHERE iID_QTC_Quy_KPK =@VoucherId

	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
			into #tblNsMlns
	FROM BH_DM_MucLucNganSach 
	where iNamLamViec = @YearOfWork 

		-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(fTongTien) AS fTien_DuToanGiaoNamNay,
          0 AS fTien_DuToanNamTruocChuyenSang,
          0 AS fTien_TongDuToanDuocGiao,
		  0 AS fTienThucChi,
		  0 AS fTienQuyetToanDaDuyet,
		  0 AS fTienDeNghiQuyetToanQuyNay,
		  0 AS fTienXacNhanQuyetToanQuyNay,
          iID_MaDonVi,
          iID_MucLucNganSach
		  into #tblPhanBoDuToan
   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
   WHERE iID_DTC_PhanBoDuToanChi IN
       (SELECT ID
        FROM BH_DTC_PhanBoDuToanChi
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iNamChungTu = @YearOfWork
          AND iID_LoaiDanhMucChi = @iID_LoaiDanhMucChi
		  AND bIsKhoa=1
          AND cast(dNgayQuyetDinh AS date) <= cast(@VoucherDate AS date))
     AND iID_MaDonVi in  (SELECT * FROM f_split(@AgencyId))-- donvi
   GROUP BY iID_MaDonVi, 
   iID_MucLucNganSach

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
		INamLamViec = @YearOfWork 
		AND iID_MaDonVi IN (SELECT * FROM #tempAgency)

	-- Tao bang tam luu chu mlng con
	SELECT * INTO #tblMlnsByPhanCapExistDonVi
	FROM #tblMlnsByPhanCap tbl, #tblDonVi dv
	WHERE  tbl.bHangCha = 0 OR (tbl.bHangCha = 1 and (IsNull(tbl.sM, '') != '' OR tbl.sM <> '' OR IsNull(tbl.sTM, '') != '')  )
	AND tbl.sTM !=''  


	-- Tao bang tam luu chu mlng cha
	SELECT *, '' AS iID_MaDonVi, '' AS sTenDonVi  INTO #tblMlnsByPhanCapNotDonVi
	FROM #tblMlnsByPhanCap 
		WHERE bHangCha = 1 AND ( (IsNull(sM, '') = '' or sM = '')) AND ( (IsNull(sTM, '') = '' or sTM = '')) OR ((ISNULL(sTM,'')='' OR sTM='')) OR sTM='0'

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
		WHERE bHangCha = 0
		UNION ALL
		SELECT *, null AS iID_MaDonVi, null AS sTenDonVi  
		FROM #tblNsMlns 
		WHERE bHangCha = 1
	) mlns

	IF @CountIndex=0
	BEGIN
	-- lấy dữ liệu đã cấp
	SELECT  
		0 AS fTien_DuToanGiaoNamNay,
		SUM(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang,
		SUM(fTien_TongDuToanDuocGiao) AS fTien_TongDuToanDuocGiao,
		SUM(fTienThucChi) AS fTienThucChi,
		SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,
		SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay,
		SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay,
		CT.iID_MaDonVi,
	    CTCT.iID_MucLucNganSach
		into #tblDataDaCap
	FROM
	BH_QTC_Quy_KPK CT
	INNER JOIN BH_QTC_Quy_KPK_ChiTiet CTCT
	ON CT.ID_QTC_Quy_KPK=CTCT.iID_QTC_Quy_KPK
	WHERE CT.iNamChungTu = @YearOfWork
		  --AND i = @iID_LoaiDanhMucChi
          AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  AND CT.ID_QTC_Quy_KPK=@VoucherId
		  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
		  AND CT.iQuyChungTu<@iQuyChungTu

	GROUP BY  CT.iID_MaDonVi,CTCT.iID_MucLucNganSach

	--SELECT * into #tempAgency from  f_split(N'001'); 


	--SELECT 0 AS fTienDuToanDuocGiao,
	--	  SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay,
	--	  SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,
	--	  SUM(fTienThucChi) AS fTienThucChi,
	--	  SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay
 --         --iID_MaDonVi,
 --         --iID_MucLucNganSach
	--	  --into #tblDataDaCap
	--FROM BH_QTC_Quy_KinhPhiQuanLy_chiTiet
	--WHERE iID_QTC_Quy_KinhPhiQuanLy IN
 --      (
	--	SELECT iID_QTC_Quy_KinhPhiQuanLy
 --       FROM BH_QTC_Quy_KinhPhiQuanLy
 --       WHERE iNamChungTu = 2023
	--	  --AND i = @iID_LoaiDanhMucChi
 --         AND CAST(dNgayChungTu AS DATE) <= CAST('09-20-2023' AS DATE)
	--	  AND EXISTS(SELECT * FROM #tempAgency INTERSECT SELECT * FROM f_split(N'001'))
	--	  AND iID_QTC_Quy_KinhPhiQuanLy='4033EB9C-ECDE-4D24-A41C-FB5B303B5883'
	--	  AND iID_MaDonVi IN (SELECT * FROM  f_split(N'001'))
	--	)
		
	--GROUP BY iID_MucLucNganSach
	SELECT * into #tblDataDuToan FROM #tblPhanBoDuToan

	SELECT sum(fTien_DuToanGiaoNamNay) AS fTien_DuToanGiaoNamNay
	, sum(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang
	, sum(fTien_TongDuToanDuocGiao) fTien_TongDuToanDuocGiao
	, SUM(fTienThucChi) AS fTienThucChi
	,SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet
	,SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay
	, SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay
	,iID_MaDonVi
	, iID_MucLucNganSach into #tblDataDaCapDuToan FROM (
		SELECT * FROM #tblDataDaCap
		UNION ALL
		SELECT * FROM #tblDataDuToan
		) data
	GROUP BY iID_MaDonVi, iID_MucLucNganSach;
	--select * from  #tblMlnsRoot
	SELECT mlns.iID_MLNS,
		   mlns.iID_MLNS_Cha,
		   mlns.sXauNoiMa,
		   fTien_DuToanGiaoNamNay,
		   fTien_DuToanNamTruocChuyenSang,
		   fTien_TongDuToanDuocGiao,
		   fTienThucChi,
		   fTienQuyetToanDaDuyet,
		   fTienDeNghiQuyetToanQuyNay,
		   fTienXacNhanQuyetToanQuyNay,
		   isnull(mlns.iID_MaDonVi, daCapDuToan.iID_MaDonVi) as iID_MaDonVi
		   INTO #tblDaCapDuToan
	FROM #tblMlnsRoot mlns
	LEFT JOIN
	  #tblDataDaCapDuToan daCapDuToan
	ON mlns.iID_MLNS = daCapDuToan.iID_MucLucNganSach and ((mlns.iID_MaDonVi is not null and mlns.iID_MaDonVi = daCapDuToan.iID_MaDonVi) or mlns.iID_MaDonVi is null)

	SELECT iID_MLNS, iID_MLNS_Cha, iID_MaDonVi, sum(fTien_DuToanGiaoNamNay) AS fTien_DuToanGiaoNamNay, sum(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang, SUM(fTien_TongDuToanDuocGiao) as fTien_TongDuToanDuocGiao,SUM(fTienThucChi) as fTienThucChi,SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay,SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay INTO #tblDaCapDuToanResult FROM (
		SELECT distinct T.iID_MLNS,  
			   T.iID_MLNS_Cha,
			   --isnull(T.iID_MaDonVi, T.iID_MaDonVi) as iID_MaDonVi,
			   T.iID_MaDonVi,
			   T.fTien_DuToanGiaoNamNay,
			   T.fTien_DuToanNamTruocChuyenSang,
			   T.fTien_TongDuToanDuocGiao,
			   T.fTienThucChi,
			   T.fTienQuyetToanDaDuyet,
			   T.fTienDeNghiQuyetToanQuyNay,
			   T.fTienXacNhanQuyetToanQuyNay
		FROM #tblDaCapDuToan T 
		WHERE T.fTien_DuToanGiaoNamNay <> 0 OR  T.fTien_DuToanNamTruocChuyenSang <> 0 OR  T.fTien_TongDuToanDuocGiao <> 0 OR  T.fTienThucChi <> 0 OR  T.fTienQuyetToanDaDuyet <> 0 OR  T.fTienDeNghiQuyetToanQuyNay <> 0 OR  T.fTienXacNhanQuyetToanQuyNay <> 0) data
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	OPTION (maxrecursion 0)

	-- Get data
	SELECT
		isnull(ctct.ID_QTC_Quy_KPK_ChiTiet, @iD) AS ID_QTC_Quy_KPK_ChiTiet,
		@VoucherId AS iID_QTC_Quy_KPK,
		mlnsPhanBo.iID_MLNS AS iID_MucLucNganSach,
		mlnsPhanBo.iID_MLNS_Cha AS IdParent,
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
		mlnsPhanBo.bHangCha As IsHangCha,
		@YearOfWork AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(daCapDuToan.fTien_DuToanGiaoNamNay, 0) as fTien_DuToanGiaoNamNay,
		isnull(daCapDuToan.fTien_DuToanNamTruocChuyenSang, 0) as fTien_DuToanNamTruocChuyenSang,
		isnull(daCapDuToan.fTien_TongDuToanDuocGiao, 0) as fTien_TongDuToanDuocGiao,
		isnull(daCapDuToan.fTienThucChi, 0) as fTienThucChi,
		isnull(daCapDuToan.fTienQuyetToanDaDuyet, 0) as fTienQuyetToanDaDuyet,
		isnull(daCapDuToan.fTienDeNghiQuyetToanQuyNay, 0) as fTienDeNghiQuyetToanQuyNay,
		isnull(daCapDuToan.fTienXacNhanQuyetToanQuyNay, 0) as fTienXacNhanQuyetToanQuyNay,

		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsRoot AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_QTC_Quy_KPK_ChiTiet
			WHERE 
		 		iID_QTC_Quy_KPK = @VoucherId
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach --and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_QTC_Quy_KPK ct ON ctct.iID_QTC_Quy_KPK = ct.ID_QTC_Quy_KPK
	LEFT JOIN #tblDaCapDuToanResult daCapDuToan
	ON mlnsPhanBo.iID_MLNS = daCapDuToan.iID_MLNS and daCapDuToan.iID_MaDonVi LIKE '%' + mlnsPhanBo.iID_MaDonVi + '%' 
	WHERE mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	ORDER BY mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	END
	ELSE 
		BEGIN
	-- lấy dữ liệu đã cấp
	SELECT  
		SUM(fTien_DuToanGiaoNamNay) AS fTien_DuToanGiaoNamNay,
		SUM(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang,
		SUM(fTien_TongDuToanDuocGiao) AS fTien_TongDuToanDuocGiao,
		SUM(fTienThucChi) AS fTienThucChi,
		SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,
		SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay,
		SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay,
		CT.iID_MaDonVi,
		CTCT.iID_MucLucNganSach
		into #tblDataDaCapExist
	FROM
	BH_QTC_Quy_KPK CT
	INNER JOIN BH_QTC_Quy_KPK_ChiTiet CTCT
	ON CT.ID_QTC_Quy_KPK=CTCT.iID_QTC_Quy_KPK
	WHERE CT.iNamChungTu = @YearOfWork
		  --AND i = @iID_LoaiDanhMucChi
          AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  AND CT.ID_QTC_Quy_KPK=@VoucherId
		  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
		  AND CT.iQuyChungTu=@iQuyChungTu

	GROUP BY  CT.iID_MaDonVi,CTCT.iID_MucLucNganSach


	SELECT sum(fTien_DuToanGiaoNamNay) AS fTien_DuToanGiaoNamNay
	, sum(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang
	, SUM(fTien_TongDuToanDuocGiao) AS fTien_TongDuToanDuocGiao
	,SUM(fTienThucChi) AS fTienThucChi
	,SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet
	,SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay
	,SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay
	, iID_MaDonVi, iID_MucLucNganSach into #tblDataDaCapDuToanExist FROM (
		SELECT * FROM #tblDataDaCapExist
		) data
	GROUP BY iID_MaDonVi, iID_MucLucNganSach;
	--select * from  #tblMlnsRoot
	SELECT mlns.iID_MLNS,
		   mlns.iID_MLNS_Cha,
		   mlns.sXauNoiMa,
		   fTien_DuToanGiaoNamNay,
		   fTien_DuToanNamTruocChuyenSang,
		   fTien_TongDuToanDuocGiao,
		   fTienThucChi,
		   fTienQuyetToanDaDuyet,
		   fTienDeNghiQuyetToanQuyNay,
		   fTienXacNhanQuyetToanQuyNay,
		   isnull(mlns.iID_MaDonVi, daCapDuToan.iID_MaDonVi) as iID_MaDonVi
		   INTO #tblDaCapDuToanExist
	FROM #tblMlnsRoot mlns
	LEFT JOIN
	  #tblDataDaCapDuToanExist daCapDuToan
	ON mlns.iID_MLNS = daCapDuToan.iID_MucLucNganSach and ((mlns.iID_MaDonVi is not null and mlns.iID_MaDonVi = daCapDuToan.iID_MaDonVi) or mlns.iID_MaDonVi is null)

	SELECT iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	, sum(fTien_DuToanGiaoNamNay) AS fTien_DuToanGiaoNamNay
	, sum(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang
	, SUM(fTien_TongDuToanDuocGiao) as fTien_TongDuToanDuocGiao
	, SUM(fTienThucChi) AS fTienThucChi
	, SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet
	, SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay
	, SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay INTO #tblDaCapDuToanResultExist FROM (
		SELECT distinct T.iID_MLNS,  
			   T.iID_MLNS_Cha,
			   --isnull(T.iID_MaDonVi, T.iID_MaDonVi) as iID_MaDonVi,
			   T.iID_MaDonVi,
			   T.fTien_DuToanGiaoNamNay,
			   T.fTien_DuToanNamTruocChuyenSang,
			   T.fTien_TongDuToanDuocGiao,
			   T.fTienThucChi,
			   T.fTienQuyetToanDaDuyet,
			   T.fTienDeNghiQuyetToanQuyNay,
			   T.fTienXacNhanQuyetToanQuyNay
		FROM #tblDaCapDuToanExist T 
		WHERE  T.fTien_DuToanGiaoNamNay <> 0 OR T.fTien_DuToanNamTruocChuyenSang<>0 
				OR   T.fTien_TongDuToanDuocGiao<>0 OR T.fTienThucChi <>0 
				OR T.fTienQuyetToanDaDuyet <>0 OR T.fTienDeNghiQuyetToanQuyNay <>0 
				OR T.fTienXacNhanQuyetToanQuyNay<>0) data
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	OPTION (maxrecursion 0)

	-- Get data
	SELECT
		isnull(ctct.ID_QTC_Quy_KPK_ChiTiet, @iD) AS ID_QTC_Quy_KPK_ChiTiet,
		@VoucherId AS iID_QTC_Quy_KPK,
		mlnsPhanBo.iID_MLNS AS iID_MucLucNganSach,
		mlnsPhanBo.iID_MLNS_Cha AS IdParent,
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
		mlnsPhanBo.bHangCha As IsHangCha,
		@YearOfWork AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(daCapDuToan.fTien_DuToanGiaoNamNay, 0) as fTien_DuToanGiaoNamNay,
		isnull(daCapDuToan.fTien_DuToanNamTruocChuyenSang, 0) as fTien_DuToanNamTruocChuyenSang,
		isnull(daCapDuToan.fTien_TongDuToanDuocGiao, 0) as fTien_TongDuToanDuocGiao,
		isnull(daCapDuToan.fTienThucChi, 0) as fTienThucChi,
		isnull(daCapDuToan.fTienQuyetToanDaDuyet, 0) as fTienQuyetToanDaDuyet,
		isnull(daCapDuToan.fTienDeNghiQuyetToanQuyNay, 0) as fTienDeNghiQuyetToanQuyNay,
		isnull(daCapDuToan.fTienXacNhanQuyetToanQuyNay, 0) as fTienXacNhanQuyetToanQuyNay,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsRoot AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_QTC_Quy_KPK_chiTiet
			WHERE 
		 		iID_QTC_Quy_KPK = @VoucherId
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach --and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_QTC_Quy_KPK ct ON ctct.iID_QTC_Quy_KPK = ct.ID_QTC_Quy_KPK
	LEFT JOIN #tblDaCapDuToanResultExist daCapDuToan
	ON mlnsPhanBo.iID_MLNS = daCapDuToan.iID_MLNS and daCapDuToan.iID_MaDonVi LIKE '%' + mlnsPhanBo.iID_MaDonVi + '%' 
	WHERE mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	ORDER BY mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	END
END
;
;
;
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkpk_thongtriloai1_bh]    Script Date: 10/25/2023 8:53:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay báo cáo thong tri theo don vi
CREATE PROCEDURE [dbo].[sp_rpt_qtc_qkpk_thongtriloai1_bh]
	 @NamLamViec int,
	 @iQuy nvarchar(100),
	 @IdDonVi nvarchar(max),
	 @UserName nvarchar(100),
	 @iLoaiTongHop int,
	 @IDLoaichi uniqueidentifier,
	 @Dvt int
AS
BEGIN 
	SET NOCOUNT ON;
IF @iLoaiTongHop=1
BEGIN
	SELECT 
				A.IID_DonVi,
				A.FTongTienXacNhanQuyetToanQuyNay,
				A.sLNS,
				A.SMaDonVi,
				dv.sTenDonVi,
				dv.iLoai
					FROM
					(SELECT 
					ct.iID_MaDonVi as SMaDonVi,
					ISNULL(ctct.fTienXacNhanQuyetToanQuyNay,0)/ @Dvt FTongTienXacNhanQuyetToanQuyNay,
					ct.iID_DonVi as IID_DonVi,
					dm.sLNS
					FROM BH_QTC_Quy_KPK ct
					LEFT JOIN BH_QTC_Quy_KPK_ChiTiet ctct
					ON ct.ID_QTC_Quy_KPK=ctct.iID_QTC_Quy_KPK
					LEFT JOIN BH_DM_MucLucNganSach dm on ctct.iID_MucLucNganSach=dm.iID_MLNS
					WHERE ct.iNamChungTu=@NamLamViec
				    AND ct.iQuyChungTu=@iQuy
					AND ct.iID_LoaiChi=@IDLoaichi
					--AND ct.bIsKhoa=1
					AND ct.iLoaiTongHop=@iLoaiTongHop
					AND ct.iID_MaDonVi IN (select * from f_split(@IdDonVi))) A
					LEFT  JOIN DonVi dv on A.iID_DonVi= dv.iID_DonVi
END
ELSE
BEGIN
			SELECT 
				A.IID_DonVi,
				A.FTongTienXacNhanQuyetToanQuyNay,
				A.sLNS,
				A.SMaDonVi,
				dv.sTenDonVi,
				dv.iLoai
					FROM
					(SELECT 
					ct.iID_MaDonVi as SMaDonVi,
					ISNULL(ctct.fTienXacNhanQuyetToanQuyNay,0)/ @Dvt FTongTienXacNhanQuyetToanQuyNay,
					ct.iID_DonVi as IID_DonVi,
					dm.sLNS
					FROM BH_QTC_Quy_KPK ct
					LEFT JOIN BH_QTC_Quy_KPK_ChiTiet ctct
					ON ct.ID_QTC_Quy_KPK=ctct.iID_QTC_Quy_KPK
					LEFT JOIN BH_DM_MucLucNganSach dm on ctct.iID_MucLucNganSach=dm.iID_MLNS
					WHERE ct.iNamChungTu=@NamLamViec
				    AND ct.iQuyChungTu=@iQuy
					AND ct.iID_LoaiChi=@IDLoaichi
					AND ct.bIsKhoa=1
					AND ct.iLoaiTongHop=@iLoaiTongHop
					AND ct.iID_MaDonVi IN (select * from f_split(@IdDonVi))) A
					LEFT  JOIN DonVi dv on A.iID_DonVi= dv.iID_DonVi
END

END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkpk_thongtriloai2_bh]    Script Date: 10/25/2023 8:53:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chung tu theo don vi và theo lns
CREATE PROCEDURE [dbo].[sp_rpt_qtc_qkpk_thongtriloai2_bh]
	 @NamLamViec int,
	 @iQuy nvarchar(100),
	 @IdDonVi nvarchar(max),
	 @LNS nvarchar(max),
	 @UserName nvarchar(100),
	 @IdLoaiChi uniqueidentifier,
	 @LoaiTongHop int ,
	 @Dvt int
AS
BEGIN 
	SET NOCOUNT ON;
	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
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

IF @LoaiTongHop=1
BEGIN
SELECT 
		mlns.iID_MLNS as IdMlns,
		mlns.iID_MLNS_Cha as IdMlnsCha,
		mlns.sXauNoiMa as XauNoiMa,
		mlns.sLNS as SLNS,
		mlns.sL as SL,
		mlns.sK as SK,
		mlns.sM as SM,
		mlns.sTM as STM,
		mlns.sTTM as STTM,
		mlns.sNG as SNG,
		mlns.sTNG as STNG,
		mlns.sMoTa as SMoTa,
		mlns.bHangCha as IsHangCha,
		ISNULL(ctct.fTienXacNhanQuyetToanQuyNay, 0) / @Dvt as FTienXacNhanQuyetToanQuyNay 
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(SELECT * FROM BH_QTC_Quy_KPK_ChiTiet 
				WHERE iID_QTC_Quy_KPK in
					( SELECT ID_QTC_Quy_KPK FROM BH_QTC_Quy_KPK
								WHERE iNamChungTu=@NamLamViec
								AND iQuyChungTu=@iQuy
								AND iID_LoaiChi=@IdLoaiChi
								AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
								)) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
	Order by mlns.sXauNoiMa
END
ELSE
BEGIN 
SELECT 
		mlns.iID_MLNS as IdMlns,
		mlns.iID_MLNS_Cha as IdMlnsCha,
		mlns.sXauNoiMa as XauNoiMa,
		mlns.sLNS as SLNS,
		mlns.sL as SL,
		mlns.sK as SK,
		mlns.sM as SM,
		mlns.sTM as STM,
		mlns.sTTM as STTM,
		mlns.sNG as SNG,
		mlns.sTNG as STNG,
		mlns.sMoTa as SMoTa,
		mlns.bHangCha as IsHangCha,
		ISNULL(ctct.fTienXacNhanQuyetToanQuyNay, 0) / @Dvt as FTienXacNhanQuyetToanQuyNay 
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(SELECT * FROM BH_QTC_Quy_KPK_ChiTiet 
				WHERE iID_QTC_Quy_KPK in
					( SELECT ID_QTC_Quy_KPK FROM BH_QTC_Quy_KPK
								WHERE iNamChungTu=@NamLamViec
								AND iQuyChungTu=@iQuy
								AND bIsKhoa=1
								AND iLoaiTongHop=@LoaiTongHop
								AND iID_LoaiChi=@IdLoaiChi
								AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
								)) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
	Order by mlns.sXauNoiMa
END 
END
;
;

GO
