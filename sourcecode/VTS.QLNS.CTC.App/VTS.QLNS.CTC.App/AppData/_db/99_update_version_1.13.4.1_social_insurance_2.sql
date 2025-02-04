/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkpk_thongtriloai2_bh]    Script Date: 11/2/2023 5:34:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_qkpk_thongtriloai2_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_qkpk_thongtriloai2_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkpk_thongtriloai1_bh]    Script Date: 11/2/2023 5:34:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_qkpk_thongtriloai1_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_qkpk_thongtriloai1_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkpk_kehoach_bh]    Script Date: 11/2/2023 5:34:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_qkpk_kehoach_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_qkpk_kehoach_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkp_ql_chungtu_chi_tiet]    Script Date: 11/2/2023 5:34:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_qkp_ql_chungtu_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_qkp_ql_chungtu_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nkpk_kehoach_bh]    Script Date: 11/2/2023 5:34:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_nkpk_kehoach_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_nkpk_kehoach_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_namkinh_phikhac_gettien_thuchi_bh]    Script Date: 11/2/2023 5:34:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_namkinh_phikhac_gettien_thuchi_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_namkinh_phikhac_gettien_thuchi_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_kehoach_cho_cssk_hssv_nld_bh]    Script Date: 11/2/2023 5:34:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_kehoach_cho_cssk_hssv_nld_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_kehoach_cho_cssk_hssv_nld_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_kehoach_bh]    Script Date: 11/2/2023 5:34:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_kehoach_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_kehoach_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_create_quyet_toan_chinamKPQL_chitiet_theo4quy]    Script Date: 11/2/2023 5:34:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_create_quyet_toan_chinamKPQL_chitiet_theo4quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_create_quyet_toan_chinamKPQL_chitiet_theo4quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_create_quyet_toan_chinamKPK_chitiet_theo4quy]    Script Date: 11/2/2023 5:34:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_create_quyet_toan_chinamKPK_chitiet_theo4quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_create_quyet_toan_chinamKPK_chitiet_theo4quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_create_quyet_toan_chinamKPK_chitiet_theo4quy]    Script Date: 11/2/2023 5:34:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_create_quyet_toan_chinamKPK_chitiet_theo4quy]
@IdChungTu uniqueidentifier,
@IdMaDonVi nvarchar(max),
@INamLamViec int,
@User  nvarchar(50),
@IDLoaiCap uniqueidentifier
as
begin
INSERT INTO BH_QTC_Nam_KPK_ChiTiet
(
	ID_QTC_Nam_KPK_ChiTiet,
	iID_QTC_Nam_KPK,
	iID_MucLucNganSach,
	sNoiDung,
	dNgaySua,
	dNgayTao,
	sNguoiSua,
	sNguoiTao,
	fTien_ThucChi
)
SELECT 
	NEWID(),
	@IdChungTu,
	tb_qtcy.iID_MucLucNganSach,
	tb_qtcy.sNoiDung,
	NULL,
	GETDATE(),
	NULL,
	@User,
	tb_qtcy.fTienThucChi
	FROM 
	(
		SELECT 
			CTCT.iID_MucLucNganSach,
			CTCT.sNoiDung,
			ISNULL(CTCT.fTienThucChi, 0) fTienThucChi
		FROM
		BH_QTC_Quy_KPK AS CT
		LEFT JOIN BH_QTC_Quy_KPK_ChiTiet CTCT ON  CT.ID_QTC_Quy_KPK=CTCT.iID_QTC_Quy_KPK
		WHERE CT.iID_MaDonVi=@IdMaDonVi 
			AND CT.iQuyChungTu=4
			AND CT.bIsKhoa=1
			AND CT.iNamChungTu=@INamLamViec
			AND  CT.iID_LoaiChi=@IDLoaiCap
	) as tb_qtcy;

end
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_create_quyet_toan_chinamKPQL_chitiet_theo4quy]    Script Date: 11/2/2023 5:34:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[sp_bh_create_quyet_toan_chinamKPQL_chitiet_theo4quy]
@IdChungTu uniqueidentifier,
@IdMaDonVi nvarchar(max),
@INamLamViec int,
@User  nvarchar(50)
as
begin
INSERT INTO BH_QTC_Nam_KinhPhiQuanLy_ChiTiet
(
	ID_QTC_Nam_KinhPhiQuanLy_ChiTiet,
	iID_QTC_Nam_KinhPhiQuanLy,
	iID_MucLucNganSach,
	sM,
	sTM,
	sNoiDung,
	dNgaySua,
	dNgayTao,
	sNguoiSua,
	sNguoiTao,
	fTien_ThucChi
)
SELECT 
	NEWID(),
	@IdChungTu,
	tb_qtcy.iID_MucLucNganSach,
	tb_qtcy.sM,
	tb_qtcy.sTM,
	tb_qtcy.sNoiDung,
	NULL,
	GETDATE(),
	NULL,
	@user,
	tb_qtcy.fTienThucChi
	FROM 
	(
		SELECT 
			CTCT.iID_MucLucNganSach,
			CTCT.sM,
			CTCT.sTM,
			CTCT.sNoiDung,
			ISNULL(CTCT.fTienThucChi, 0) fTienThucChi
		FROM
		BH_QTC_Quy_KinhPhiQuanLy AS CT
		LEFT JOIN BH_QTC_Quy_KinhPhiQuanLy_ChiTiet CTCT ON  CT.ID_QTC_Quy_KinhPhiQuanLy=CTCT.iID_QTC_Quy_KinhPhiQuanLy
		WHERE CT.iID_MaDonVi=@IdMaDonVi 
			AND CT.iQuyChungTu=4
			AND CT.bIsKhoa=1
			AND CT.iNamChungTu=@INamLamViec
			--GROUP BY CTCT.iID_MucLucNganSach,
			--CTCT.sM,
			--CTCT.sTM,
			--CTCT.sNoiDung
	) as tb_qtcy;

end



GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_kehoach_bh]    Script Date: 11/2/2023 5:34:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_rpt_kehoach_bh]
	@IdMaDonVi NVARCHAR(MAX),
	@IQuy int,
	@NamLamViec int,
	@UserName NVARCHAR(100),
	@Donvitinh int,
	@iIdLoaiCap uniqueidentifier
AS
BEGIN
		select 
			row_number() over (order by ctct.iID_MaDonVi) as STT,
			sum(ctct.fTienDuToan)/@Donvitinh as FTienDuToan,
			sum(ctct.fTienDaCap)/@Donvitinh as FTienDaCap, 
			sum(ctct.fTienKeHoachCap)/@Donvitinh as FTienKeHoachCap,
			ct.sLNS as SDSLNS,
			ctct.iID_MaDonVi
			into #tblkehoach
		from BH_CP_ChungTu_ChiTiet as ctct
		left join BH_CP_ChungTu as ct on ctct.iID_CP_ChungTu = ct.iID_CP_ChungTu
		where ctct.iID_MaDonVi In (SELECT * FROM f_split(@IdMaDonVi))
			and ct.iNamChungTu = @NamLamViec
			--and ct.iLoaiTongHop <> 2
			and ct.iQuy = @IQuy
			and ct.sNguoiTao=@UserName
			and ct.iID_LoaiCap=@iIdLoaiCap
			group by ctct.iID_MaDonVi,ct.sLNS

		select kh.*,dv.iID_MaDonVi,dv.sTenDonVi from #tblkehoach kh
		left join DonVi dv on kh.iID_MaDonVi=dv.iID_MaDonVi
		where dv.iNamLamViec=@NamLamViec
		order by STT

		drop table #tblkehoach
	
END


GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_kehoach_cho_cssk_hssv_nld_bh]    Script Date: 11/2/2023 5:34:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chung tu theo don vi và theo lns
CREATE PROCEDURE [dbo].[sp_cp_rpt_kehoach_cho_cssk_hssv_nld_bh]
	 @NamLamViec int,
	 @IDLoaiCap nvarchar(100),
	 @IdDonVi nvarchar(max),
	 @LNS nvarchar(max),
	 @UserName nvarchar(100),
	 @Dvt int,
	 @Quy int
AS
BEGIN 
	SET NOCOUNT ON;
	-- lấy ra mlns theo phân cấp
		SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
			into #tblLNS
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

		SELECT 
				CTCT.iID_MaDonVi,
				dv.sTenDonVi,
				mlns.sLNS as SDSLNS,
				SUM(ctct.fTienDaCap)/@Dvt FTienDuToan,
				SUM(ctct.fTienDuToan)/@Dvt FTienDaCap,
				SUM(ctct.fTienKeHoachCap)/@Dvt FTienKeHoachCap
		FROM 
				#tblLNS mlns
		LEFT JOIN 
				(SELECT * FROM BH_CP_ChungTu_ChiTiet 
						WHERE iID_CP_ChungTu IN
						(SELECT iID_CP_ChungTu FROM BH_CP_ChungTu
							WHERE iID_LoaiCap=@IDLoaiCap
								AND iQuy=@Quy
								AND iNamChungTu=@NamLamViec)
						AND iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))) ctct
			ON mlns.iID_MLNS = ctct.iID_MucLucNganSach 
		LEFT JOIN DonVi dv ON ctct.iID_MaDonVi=dv.iID_MaDonVi
		WHERE dv.iNamLamViec=@NamLamViec
		GROUP BY CTCT.iID_MaDonVi,dv.sTenDonVi,mlns.sLNS

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_namkinh_phikhac_gettien_thuchi_bh]    Script Date: 11/2/2023 5:34:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		
-- Create date: 
-- Description:	Lấy danh sách hiển thị thực chi theo quý của chứng từ quyết toán năm chi kinh phí khác chi tiết
CREATE PROCEDURE [dbo].[sp_qtc_namkinh_phikhac_gettien_thuchi_bh]
	@YearOfWork int,
	@AgencyId nvarchar(500),
	@VoucherDate datetime,
	@IQuyChungTu int,
	@ILoaiChi uniqueidentifier
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
		  AND CT.iQuyChungTu=@IQuyChungTu
		  AND CT.iID_LoaiChi=@ILoaiChi
	GROUP BY  CT.iID_MaDonVi,CTCT.iID_MucLucNganSach

END

GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nkpk_kehoach_bh]    Script Date: 11/2/2023 5:34:25 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkp_ql_chungtu_chi_tiet]    Script Date: 11/2/2023 5:34:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay ke hoach chung tu theo don vi
CREATE PROCEDURE [dbo].[sp_rpt_qtc_qkp_ql_chungtu_chi_tiet]
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
		mlns.sMoTa as SNoiDung,
		mlns.bHangCha as IsHangCha,
		ISNULL(ctct.fTienDuToanDuocGiao, 0) / @Dvt as FTienDuToanDuocGiao,
		ISNULL(ctct.fTienThucChi, 0) / @Dvt as FTienThucChi, 
		ISNULL(ctct.fTienQuyetToanDaDuyet, 0) / @Dvt as FTienQuyetToanDaDuyet, 
		ISNULL(ctct.fTienDeNghiQuyetToanQuyNay, 0) / @Dvt as FTienDeNghiQuyetToanQuyNay, 
		ISNULL(ctct.fTienXacNhanQuyetToanQuyNay, 0) / @Dvt as FTienXacNhanQuyetToanQuyNay 
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(SELECT * FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet 
				WHERE iID_QTC_Quy_KinhPhiQuanLy in
					( SELECT ID_QTC_Quy_KinhPhiQuanLy FROM BH_QTC_Quy_KinhPhiQuanLy
								WHERE iNamChungTu=@NamLamViec
								AND iQuyChungTu=@iQuy
								--AND bIsKhoa=1
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
		mlns.sMoTa as SNoiDung,
		mlns.bHangCha as IsHangCha,
		ISNULL(ctct.fTienDuToanDuocGiao, 0) / @Dvt as FTienDuToanDuocGiao,
		ISNULL(ctct.fTienThucChi, 0) / @Dvt as FTienThucChi, 
		ISNULL(ctct.fTienQuyetToanDaDuyet, 0) / @Dvt as FTienQuyetToanDaDuyet, 
		ISNULL(ctct.fTienDeNghiQuyetToanQuyNay, 0) / @Dvt as FTienDeNghiQuyetToanQuyNay, 
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
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkpk_kehoach_bh]    Script Date: 11/2/2023 5:34:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay ke hoach chung tu theo don vi
CREATE PROCEDURE [dbo].[sp_rpt_qtc_qkpk_kehoach_bh]
	 @NamLamViec int,
	 @iQuy nvarchar(100),
	 @IdDonVi nvarchar(max),
	 @LNS nvarchar(max),
	 @UserName nvarchar(100),
	 @IdLoaiChi uniqueidentifier,
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
		mlns.sMoTa as SNoiDung,
		mlns.bHangCha as IsHangCha,
		ISNULL(ctct.fTien_DuToanNamTruocChuyenSang, 0) / @Dvt as FTien_DuToanNamTruocChuyenSang,
		ISNULL(ctct.fTien_DuToanGiaoNamNay, 0) / @Dvt as FTien_DuToanGiaoNamNay, 
		ISNULL(ctct.fTien_TongDuToanDuocGiao, 0) / @Dvt as FTien_TongDuToanDuocGiao, 
		ISNULL(ctct.fTienThucChi, 0) / @Dvt as FTienThucChi, 
		ISNULL(ctct.fTienQuyetToanDaDuyet, 0) / @Dvt as FTienQuyetToanDaDuyet, 
		ISNULL(ctct.fTienDeNghiQuyetToanQuyNay, 0) / @Dvt as FTienDeNghiQuyetToanQuyNay, 
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
								--AND bIsKhoa=1
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
		mlns.sMoTa as SNoiDung,
		mlns.bHangCha as IsHangCha,
		ISNULL(ctct.fTien_DuToanNamTruocChuyenSang, 0) / @Dvt as FTien_DuToanNamTruocChuyenSang,
		ISNULL(ctct.fTien_DuToanGiaoNamNay, 0) / @Dvt as FTien_DuToanGiaoNamNay, 
		ISNULL(ctct.fTien_TongDuToanDuocGiao, 0) / @Dvt as FTien_TongDuToanDuocGiao, 
		ISNULL(ctct.fTienThucChi, 0) / @Dvt as FTienThucChi, 
		ISNULL(ctct.fTienQuyetToanDaDuyet, 0) / @Dvt as FTienQuyetToanDaDuyet, 
		ISNULL(ctct.fTienDeNghiQuyetToanQuyNay, 0) / @Dvt as FTienDeNghiQuyetToanQuyNay, 
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
											--AND bIsKhoa=1
											AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
											)) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
	Order by mlns.sXauNoiMa
END
END
;
;

select * from BH_QTC_Quy_KPK_ChiTiet
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkpk_thongtriloai1_bh]    Script Date: 11/2/2023 5:34:25 PM ******/
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
					--AND ct.bIsKhoa=1
					AND ct.iLoaiTongHop=@iLoaiTongHop
					AND ct.iID_MaDonVi IN (select * from f_split(@IdDonVi))) A
					LEFT  JOIN DonVi dv on A.iID_DonVi= dv.iID_DonVi
END

END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkpk_thongtriloai2_bh]    Script Date: 11/2/2023 5:34:25 PM ******/
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
								--AND bIsKhoa=1
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
