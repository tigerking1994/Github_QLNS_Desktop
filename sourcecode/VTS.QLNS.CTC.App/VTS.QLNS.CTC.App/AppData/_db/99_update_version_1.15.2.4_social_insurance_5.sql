/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_thongtri_donvi_bh]    Script Date: 12/20/2024 4:20:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_thongtri_donvi_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_thongtri_donvi_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_kehoach_cho_cssk_hssv_nld_bh]    Script Date: 12/20/2024 4:20:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_kehoach_cho_cssk_hssv_nld_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_kehoach_cho_cssk_hssv_nld_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_kehoach_bh]    Script Date: 12/20/2024 4:20:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_kehoach_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_kehoach_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_thongtri_nhieuloaichi]    Script Date: 12/20/2024 4:20:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_get_thongtri_nhieuloaichi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_get_thongtri_nhieuloaichi]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_lns_rpt_thongtri_lns_bh]    Script Date: 12/20/2024 4:20:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_lns_rpt_thongtri_lns_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_lns_rpt_thongtri_lns_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_lns_rpt_thongtri_lns_bh]    Script Date: 12/20/2024 4:20:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_lns_rpt_thongtri_lns_bh]
	 @NamLamViec int,
	 @IDLoaiChi nvarchar(500),
	 @IdDonvi nvarchar(max),
	 @LNS nvarchar(max),
	 @UserName nvarchar(100),
	 @Dvt int,
     @Quy int
AS
BEGIN 
	SET NOCOUNT ON;
	-- lấy ra mlns theo phân cấp
		SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha, sDuToanChiTietToi
			into #tblMlnsByPhanCap
	FROM BH_DM_MucLucNganSach 
	WHERE 
		iNamLamViec = @NamLamViec 
		AND iTrangThai = 1
		and  sLNS in (select * from splitstring(@LNS))
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
		mlns.sDuToanChiTietToi,
		ROUND(ISNULL(ctct.fTienKeHoachCap, 0) / @Dvt,0) as FTienKeHoach 
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(select SUM(ISNULL(ctct.fTienKeHoachCap, 0)) fTienKeHoachCap, ctct.iID_MucLucNganSach from BH_CP_ChungTu ct 
		left join BH_CP_ChungTu_ChiTiet ctct on ct.iID_CP_ChungTu=ctct.iID_CP_ChungTu
		where ct.iNamChungTu=@NamLamViec
		and ct.iID_LoaiCap=@IDLoaiChi
		and iID_MaDonVi in (select * from f_split(@IdDonVi))
		and ct.iQuy = @Quy
		group by ctct.iID_MucLucNganSach
		) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
	Order by mlns.sXauNoiMa
END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_thongtri_nhieuloaichi]    Script Date: 12/20/2024 4:20:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_rpt_get_thongtri_nhieuloaichi]
@NamLamViec int,
@IdMaDonVi nvarchar(max),
@Quy int,
@donViTinh int,
@lstsIDLoaiChi nvarchar(max)
AS BEGIN
SET NOCOUNT ON;


			select 0 IsHangCha,N'Chi các chế độ BHXH' as SMoTa, ROUND(Sum(isnull(FTienKeHoachCap,0)) /@donViTinh, 0) FTienKeHoach from BH_CP_ChungTu_ChiTiet
			where iID_CP_ChungTu in 
			(select iID_CP_ChungTu from BH_CP_ChungTu 
				where iQuy=@Quy
				and iNamChungTu=@NamLamViec
				and iID_LoaiCap IN (select * from splitstring(@lstsIDLoaiChi))
			)
			and sXauNoiMa = '901'
			and iID_MaDonVi=@IdMaDonVi
			Union all

			select  0 IsHangCha,N'Chi kinh phí quản lý BHXH, BHYT' as SMoTa, ROUND(Sum(isnull(FTienKeHoachCap,0)) /@donViTinh, 0 ) FTienKeHoach  from BH_CP_ChungTu_ChiTiet
			where iID_CP_ChungTu in 
			(select iID_CP_ChungTu from BH_CP_ChungTu 
				where iQuy=@Quy
				and iNamChungTu=@NamLamViec
				and iID_LoaiCap IN (select * from splitstring(@lstsIDLoaiChi))
			)
			and sXauNoiMa = '9010003'
			and iID_MaDonVi=@IdMaDonVi
			Union all

			select  0 IsHangCha,N'Kinh phí KCB tại quân y đơn vị 10%' as SMoTa, ROUND(Sum(isnull(FTienKeHoachCap,0)) /@donViTinh, 0) FTienKeHoach  from BH_CP_ChungTu_ChiTiet
			where iID_CP_ChungTu in 
			(select iID_CP_ChungTu from BH_CP_ChungTu 
				where iQuy=@Quy
				and iNamChungTu=@NamLamViec
				and iID_LoaiCap IN (select * from splitstring(@lstsIDLoaiChi))
			)
			and sXauNoiMa = '9010004'
			and iID_MaDonVi=@IdMaDonVi
			Union all

			select  0 IsHangCha,N'Kinh phí KCB tại Trường Sa - DK' as SMoTa, ROUND(Sum(isnull(FTienKeHoachCap,0))/@donViTinh,0) FTienKeHoach  from BH_CP_ChungTu_ChiTiet
			where iID_CP_ChungTu in 
			(select iID_CP_ChungTu from BH_CP_ChungTu 
				where iQuy=@Quy
				and iNamChungTu=@NamLamViec
				and iID_LoaiCap IN (select * from splitstring(@lstsIDLoaiChi))
			)
			and sXauNoiMa = '9010006'
			and iID_MaDonVi=@IdMaDonVi
			Union all

			select  0 IsHangCha,N'Chi kinh phí chăm sóc sức khỏe ban đầu HSSV & NLĐ' as SMoTa, ROUND(Sum(isnull(FTienKeHoachCap,0)) /@donViTinh, 0 ) FTienKeHoach from BH_CP_ChungTu_ChiTiet
			where iID_CP_ChungTu in 
			(select iID_CP_ChungTu from BH_CP_ChungTu 
				where iQuy=@Quy
				and iNamChungTu=@NamLamViec
				and iID_LoaiCap IN (select * from splitstring(@lstsIDLoaiChi))
			)
			and sXauNoiMa like '9050001%'

			Union all

			select  0 IsHangCha,N'Chi từ nguồn kết dư Quỹ KCB BHYT quân nhân' as SMoTa, ROUND(Sum(isnull(FTienKeHoachCap,0)) /@donViTinh, 0 ) FTienKeHoach  from BH_CP_ChungTu_ChiTiet
			where iID_CP_ChungTu in 
			(select iID_CP_ChungTu from BH_CP_ChungTu 
				where iQuy=@Quy
				and iNamChungTu=@NamLamViec
				and iID_LoaiCap IN (select * from splitstring(@lstsIDLoaiChi))
			)
			and sXauNoiMa = '9010008'
			and iID_MaDonVi=@IdMaDonVi
			Union all

			select  0 IsHangCha,N'Kinh phí mua sắm trang thiết bị y tế' as SMoTa, ROUND(Sum(isnull(FTienKeHoachCap,0)) /@donViTinh, 0) FTienKeHoach from BH_CP_ChungTu_ChiTiet
			where iID_CP_ChungTu in 
			(select iID_CP_ChungTu from BH_CP_ChungTu 
				where iQuy=@Quy
				and iNamChungTu=@NamLamViec
				and iID_LoaiCap IN (select * from splitstring(@lstsIDLoaiChi))
			)
			and sXauNoiMa = '9010009'
			and iID_MaDonVi=@IdMaDonVi
			Union all

			select  0 IsHangCha,N'Chi hỗ trợ người lao động tham gia BHTN' as SMoTa, ROUND(Sum(isnull(FTienKeHoachCap,0)) /@donViTinh, 0) FTienKeHoach  from BH_CP_ChungTu_ChiTiet
			where iID_CP_ChungTu in 
			(select iID_CP_ChungTu from BH_CP_ChungTu 
				where iQuy=@Quy
				and iNamChungTu=@NamLamViec
				and iID_LoaiCap IN (select * from splitstring(@lstsIDLoaiChi))
			)
			and sXauNoiMa = '90100010'
			and iID_MaDonVi=@IdMaDonVi
END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_kehoach_bh]    Script Date: 12/20/2024 4:20:11 PM ******/
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
	@iIdLoaiCap uniqueidentifier,
	@MaLoaiChi NVARCHAR(100)
AS
BEGIN
		select 
			row_number() over (order by ctct.iID_MaDonVi) as STT,
			ROUND(isnull(dt.fTienDuToan,0) /@Donvitinh,0) as FTienDuToan,
			ROUND(sum(ctct.fTienDaCap)/@Donvitinh, 0) as FTienDaCap, 
			ROUND(sum(ctct.fTienKeHoachCap)/@Donvitinh, 0 ) as FTienKeHoachCap,
			ctct.sGhiChu,
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
						  AND bIsKhoa=1
						  )
					 AND iID_MaDonVi in  (SELECT * FROM f_split(@IdMaDonVi))
					 AND sMaLoaiChi=@MaLoaiChi
				   GROUP BY iID_MaDonVi
		) dt on dt.iID_MaDonVi=ctct.iID_MaDonVi
		where ctct.iID_MaDonVi In (SELECT * FROM f_split(@IdMaDonVi))
			and ct.iNamChungTu = @NamLamViec
			--and ct.iLoaiTongHop <> 2
			and ct.iQuy = @IQuy
			and ct.sNguoiTao=@UserName
			and ct.iID_LoaiCap=@iIdLoaiCap
			group by ctct.iID_MaDonVi,dt.fTienDuToan,ctct.sGhiChu
			--,ct.sLNS


		select kh.*,dv.iID_MaDonVi,dv.sTenDonVi from #tblkehoach kh
		left join DonVi dv on kh.iID_MaDonVi=dv.iID_MaDonVi
		where dv.iNamLamViec=@NamLamViec
		order by STT

		drop table #tblkehoach
	
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_kehoach_cho_cssk_hssv_nld_bh]    Script Date: 12/20/2024 4:20:11 PM ******/
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
	 @Quy int,
	 @MaLoaiChi nvarchar(100)
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
		AND sLNS IN (SELECT * FROM f_split(@LNS))
		
		SELECT 
				ctct.iID_MaDonVi,
				dv.sTenDonVi,
				mlns.sLNS as SDSLNS,
				ctct.sGhiChu,
				ROUND(SUM(ctct.fTienDaCap)/@Dvt , 0) FTienDuToan,
				ROUND(SUM(ctct.fTienDuToan)/@Dvt, 0) FTienDaCap,
				ROUND(SUM(ctct.fTienKeHoachCap)/@Dvt,0) FTienKeHoachCap
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
		LEFT JOIN 
			(
					SELECT 
					  SUM(fTienTuChi) AS fTienDuToan,
					  sXauNoiMa,
					  iID_MaDonVi
					FROM BH_DTC_PhanBoDuToanChi_ChiTiet
					   WHERE iID_DTC_PhanBoDuToanChi IN
						   (SELECT ID
							FROM BH_DTC_PhanBoDuToanChi
							WHERE sSoQuyetDinh <> ''
							  AND sSoQuyetDinh IS NOT NULL
							  AND iNamChungTu = @NamLamViec
							  --AND iID_LoaiDanhMucChi = @iIdLoaiCap
							  AND bIsKhoa=1
							  )
						 AND iID_MaDonVi in  (SELECT * FROM f_split(@IdDonVi))-- donvi
						 AND sMaLoaiChi=@MaLoaiChi
					   GROUP BY iID_MaDonVi,sXauNoiMa
			) dt on dt.sXauNoiMa=ctct.sXauNoiMa
		LEFT JOIN DonVi dv ON ctct.iID_MaDonVi=dv.iID_MaDonVi
		WHERE dv.iNamLamViec=@NamLamViec
		GROUP BY CTCT.iID_MaDonVi,dv.sTenDonVi,mlns.sLNS,ctct.sGhiChu

END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_thongtri_donvi_bh]    Script Date: 12/20/2024 4:20:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_rpt_thongtri_donvi_bh]
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
			ROUND(sum(ctct.fTienDuToan)/@Donvitinh,0) as FTienDuToan,
			ROUND(sum(ctct.fTienDaCap)/@Donvitinh , 0) as FTienDaCap, 
			ROUND(sum(ctct.fTienKeHoachCap)/@Donvitinh,0) as FTienKeHoachCap,
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
;
GO
