/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_thongtri_theodonvi_cssk_hssv_nld_bh]    Script Date: 11/29/2023 9:34:05 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_thongtri_theodonvi_cssk_hssv_nld_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_thongtri_theodonvi_cssk_hssv_nld_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_thongtri_donvi_bh]    Script Date: 11/29/2023 9:34:05 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_thongtri_donvi_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_thongtri_donvi_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_kehoach_bh]    Script Date: 11/29/2023 9:34:05 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_kehoach_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_kehoach_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_lns_BH]    Script Date: 11/29/2023 9:34:05 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_get_donvi_lns_BH]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_get_donvi_lns_BH]
GO
/****** Object:  StoredProcedure [dbo].[bh_qtcq_ctct_gttrocap_index]    Script Date: 11/29/2023 9:34:05 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bh_qtcq_ctct_gttrocap_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[bh_qtcq_ctct_gttrocap_index]
GO
/****** Object:  StoredProcedure [dbo].[bh_qtcq_ctct_gttrocap_index]    Script Date: 11/29/2023 9:34:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 13/06/2023
-- Description:	Lấy danh sách hiển thị giai thich 
-- =============================================
CREATE PROCEDURE [dbo].[bh_qtcq_ctct_gttrocap_index]

@YearWork int,
@IdQTCQuyCheDoBHXH uniqueidentifier,
@SXauNoiMa nvarchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
	DISTINCT 
		 gttc.iiD_QTC_Quy_CTCT_GiaiThichTroCap
		, gttc.iID_QTC_Quy_ChungTu as IID_QTC_Quy_ChungTu
		, gttc.iNamLamViec
		, gttc.iQuy
		, gttc.sNguoiSua
		, gttc.sNguoiTao
		, gttc.dNgaySua
		, gttc.dNgayTao
		, gttc.iSoNgayHuong
		, gttc.sMa_Hieu_Can_Bo AS SMaHieuCanBo
		, gttc.iiD_MaPhanHo AS  ID_MaPhanHo
		, gttc.iCapBac
		, gttc.sTenCapBac
		, gttc.fSoTien
		, gttc.iiD_MaPhanHo
		, gttc.sSoQuyetDinh
		, gttc.sTenCanBo
		, gttc.sXauNoiMa
		, gttc.dNgayQuyetDinh
		, gttc.iiD_MaDonVi AS ID_MaDonVi
		-- Tong dự toán todo
	FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gttc
	WHERE gttc.iNamLamViec=@YearWork
			AND gttc.iID_QTC_Quy_ChungTu=@IdQTCQuyCheDoBHXH
			AND gttc.sXauNoiMa=@SXauNoiMa
END
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_lns_BH]    Script Date: 11/29/2023 9:34:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_cp_rpt_get_donvi_lns_BH]
@NamLamViec int,
@Quy int,
@LoaiChi uniqueidentifier
AS BEGIN
SET NOCOUNT ON;

SELECT 
	donvi.iID_DonVi AS Id,
	donvi.iID_MaDonVi as IIDMaDonVi,
	donvi.sTenDonVi as TenDonVi,
	donvi.sKyHieu as KyHieu,
	donvi.sMoTa as MoTa,
	donvi.iLoai as Loai,
	donvi.iNamLamViec as NamLamViec,
	donvi.iTrangThai as iTrangThai,
	donvi.dNgayTao as DNgayTao,
	donvi.sNguoiTao as SNguoiTao,
	donvi.dNgaySua as DNgaySua,
	donvi.dNgaySua as SNguoiSua,
	donvi.*

FROM BH_CP_ChungTu chungtu
LEFT JOIN DonVi donvi ON donvi.iID_MaDonVi IN (SELECT * FROM splitstring(chungtu.sID_MaDonVi))
WHERE chungtu.iNamChungTu = @NamLamViec
  AND chungtu.iQuy=@Quy
  AND chungtu.sID_MaDonVi <> ''
  AND chungtu.sID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
  AND chungtu.iID_LoaiCap=@LoaiChi
  --AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_kehoach_bh]    Script Date: 11/29/2023 9:34:06 AM ******/
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
			--ct.sLNS as SDSLNS,
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
			group by ctct.iID_MaDonVi
			--,ct.sLNS

		select kh.*,dv.iID_MaDonVi,dv.sTenDonVi from #tblkehoach kh
		left join DonVi dv on kh.iID_MaDonVi=dv.iID_MaDonVi
		where dv.iNamLamViec=@NamLamViec
		order by STT

		drop table #tblkehoach
	
END


GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_thongtri_donvi_bh]    Script Date: 11/29/2023 9:34:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_cp_rpt_thongtri_donvi_bh]
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
			--ct.sLNS as SDSLNS,
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
			group by ctct.iID_MaDonVi
			--,ct.sLNS

		select kh.*,dv.iID_MaDonVi,dv.sTenDonVi from #tblkehoach kh
		left join DonVi dv on kh.iID_MaDonVi=dv.iID_MaDonVi
		where dv.iNamLamViec=@NamLamViec
		order by STT

		drop table #tblkehoach
	
END


GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_thongtri_theodonvi_cssk_hssv_nld_bh]    Script Date: 11/29/2023 9:34:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chung tu theo don vi và theo lns
Create PROCEDURE [dbo].[sp_cp_rpt_thongtri_theodonvi_cssk_hssv_nld_bh]
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
