/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_kehoach_cho_cssk_hssv_nld_bh]    Script Date: 3/4/2024 2:11:07 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_kehoach_cho_cssk_hssv_nld_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_kehoach_cho_cssk_hssv_nld_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_kehoach_bh]    Script Date: 3/4/2024 2:11:07 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_kehoach_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_kehoach_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_lns_BH]    Script Date: 3/4/2024 2:11:07 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_get_donvi_lns_BH]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_get_donvi_lns_BH]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_get_donvi_tonghop_bh]    Script Date: 3/4/2024 2:11:07 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_get_donvi_tonghop_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_get_donvi_tonghop_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_get_donvi_tonghop_bh]    Script Date: 3/4/2024 2:11:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay don vi cua chung tu tổng hop cap phat chi tiet theo id
-- =============================================
CREATE PROCEDURE [dbo].[sp_cp_get_donvi_tonghop_bh]
	@YearOfWork int,
	@Quy int,
	@IDLoaiChi nvarchar(500)
AS
BEGIN
	SELECT 
	distinct
	dv.* 
	FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN  DonVi  dv on dv.iID_MaDonVi IN (SELECT * FROM splitstring(ctct.iID_MaDonVi))
	where ctct.iID_CP_ChungTu in 
	(
		select cp.iID_CP_ChungTu from BH_CP_ChungTu cp
		where cp.iNamChungTu=@YearOfWork
			and cp.iID_LoaiCap=@IDLoaiChi
			and cp.iQuy=@Quy
	)
	and dv.iNamLamViec=@YearOfWork
	order by dv.iID_MaDonVi
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_lns_BH]    Script Date: 3/4/2024 2:11:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_rpt_get_donvi_lns_BH]
@NamLamViec int,
@Quy int,
@LoaiChi uniqueidentifier
AS BEGIN
SET NOCOUNT ON;

SELECT 
distinct
	dv.iID_DonVi AS Id,
	dv.iID_MaDonVi as IIDMaDonVi,
	dv.sTenDonVi as TenDonVi,
	dv.sKyHieu as KyHieu,
	dv.sMoTa as MoTa,
	dv.iLoai as Loai,
	dv.iNamLamViec as NamLamViec,
	dv.iTrangThai as iTrangThai,
	dv.dNgayTao as DNgayTao,
	dv.sNguoiTao as SNguoiTao,
	dv.dNgaySua as DNgaySua,
	dv.dNgaySua as SNguoiSua,
	dv.*

		FROM BH_CP_ChungTu_ChiTiet ctct
			LEFT JOIN  DonVi  dv on dv.iID_MaDonVi IN (SELECT * FROM splitstring(ctct.iID_MaDonVi))
			where ctct.iID_CP_ChungTu in 
			(
				select cp.iID_CP_ChungTu from BH_CP_ChungTu cp
				where cp.iNamChungTu=@NamLamViec
					and cp.iID_LoaiCap=@LoaiChi
					and cp.iQuy=@Quy
			)
			and dv.iNamLamViec=@NamLamViec
			order by dv.iID_MaDonVi
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_kehoach_bh]    Script Date: 3/4/2024 2:11:07 PM ******/
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
			isnull(dt.fTienDuToan,0) /@Donvitinh as FTienDuToan,
			sum(ctct.fTienDaCap)/@Donvitinh as FTienDaCap, 
			sum(ctct.fTienKeHoachCap)/@Donvitinh as FTienKeHoachCap,
			--ct.sLNS as SDSLNS,
			ctct.iID_MaDonVi 
			into #tblkehoach
		from BH_CP_ChungTu_ChiTiet as ctct
		left join BH_CP_ChungTu as ct on ctct.iID_CP_ChungTu = ct.iID_CP_ChungTu
		left join 
		(
				SELECT 
				  SUM(fTienTuChi) AS fTienDuToan,
				  iID_MaDonVi
				FROM BH_DTC_PhanBoDuToanChi_ChiTiet
				   WHERE iID_DTC_PhanBoDuToanChi IN
					   (SELECT ID
						FROM BH_DTC_PhanBoDuToanChi
						WHERE sSoQuyetDinh <> ''
						  AND sSoQuyetDinh IS NOT NULL
						  AND iNamChungTu = @NamLamViec
						  AND iID_LoaiDanhMucChi = @iIdLoaiCap
						  AND bIsKhoa=1
						  )
					 AND iID_MaDonVi in  (SELECT * FROM f_split(@IdMaDonVi))-- donvi
				   GROUP BY iID_MaDonVi
		) dt on dt.iID_MaDonVi=ctct.iID_MaDonVi
		where ctct.iID_MaDonVi In (SELECT * FROM f_split(@IdMaDonVi))
			and ct.iNamChungTu = @NamLamViec
			--and ct.iLoaiTongHop <> 2
			and ct.iQuy = @IQuy
			and ct.sNguoiTao=@UserName
			and ct.iID_LoaiCap=@iIdLoaiCap
			group by ctct.iID_MaDonVi,dt.fTienDuToan
			--,ct.sLNS


		select kh.*,dv.iID_MaDonVi,dv.sTenDonVi from #tblkehoach kh
		left join DonVi dv on kh.iID_MaDonVi=dv.iID_MaDonVi
		where dv.iNamLamViec=@NamLamViec
		order by STT

		drop table #tblkehoach
	
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_kehoach_cho_cssk_hssv_nld_bh]    Script Date: 3/4/2024 2:11:07 PM ******/
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
;
GO
