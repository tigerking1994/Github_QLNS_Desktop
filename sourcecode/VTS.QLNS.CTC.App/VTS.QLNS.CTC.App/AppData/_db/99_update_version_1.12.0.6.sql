/****** Object:  StoredProcedure [dbo].[sp_vdt_getall_dutoan_hangmuc_by_dutoan]    Script Date: 11/10/2022 1:45:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_getall_dutoan_hangmuc_by_dutoan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_getall_dutoan_hangmuc_by_dutoan]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop]    Script Date: 11/10/2022 1:45:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_canbo_phucap_saochep]    Script Date: 11/10/2022 1:45:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_canbo_phucap_saochep]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_canbo_phucap_saochep]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 11/10/2022 1:45:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bangluong_thang_dulieu_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bangluong_thang_dulieu_insert]
GO
/****** Object:  StoredProcedure [dbo].[sp_luong_dt_chungtu_danhsach]    Script Date: 11/10/2022 1:45:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_luong_dt_chungtu_danhsach]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_luong_dt_chungtu_danhsach]
GO
/****** Object:  StoredProcedure [dbo].[sp_luong_dt_chungtu_danhsach]    Script Date: 11/10/2022 1:45:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_luong_dt_chungtu_danhsach] 
	@YearOfBudget int,
	@YearOfWork int
AS
BEGIN


	SELECT *
	FROM NS_DT_ChungTu
	WHERE iNamNganSach = @YearOfBudget
	  AND iNamLamViec = @YearOfWork
	  --AND iID_MaNguonNganSach = @BudgetSource
	  --AND iLoaiChungTu = @VoucherType
	  AND bKhoa = 1
	ORDER BY sSoChungTu
END
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 11/10/2022 1:45:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		TungNH
-- Create date: 21/04/2022
-- Description:	Lấy dữ liệu cho thêm mới bảng lương tháng
-- =============================================
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

    WITH SoLieuTienAn AS (
		SELECT
			MA_CBO MaCanBo,
			SUM (
				CASE
					--WHEN MA_PHUCAP IN ('TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4', 'TA_DOCHAI_DG', 'TA_OM_DG') THEN ISNULL(HuongPC_SN, 0) * GIA_TRI
					WHEN MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN ISNULL(@SoNgay, 0) * GIA_TRI 
					ELSE ISNULL(HuongPC_SN, 0) * GIA_TRI
				END
			) GiaTri
		FROM TL_CanBo_PhuCap canBoPhuCap
		--WHERE
			--canBoPhuCap.MA_PHUCAP IN ('TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4', 'TA_DOCHAI_DG', 'TA_OM_DG', 'TA_BB_DG', 'TA_TRUCTRAI_DG')
		WHERE 
			canBoPhuCap.MA_PHUCAP IN (SELECT Ma_PhuCap FROM TL_DM_PhuCap WHERE PARENT LIKE 'TIENAN')
		GROUP BY canBoPhuCap.MA_CBO
	),
	SoLieuTienAn2 AS (
		SELECT
			MA_CBO MaCanBo,
			SUM (
				CASE
					--WHEN MA_PHUCAP IN ('TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4', 'TA_DOCHAI_DG', 'TA_OM_DG') THEN ISNULL(HuongPC_SN, 0) * GIA_TRI
					WHEN MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN ISNULL(@SoNgay, 0) * GIA_TRI 
					ELSE ISNULL(HuongPC_SN, 0) * GIA_TRI
				END
			) GiaTri
		FROM TL_CanBo_PhuCap canBoPhuCap
		--WHERE
			--canBoPhuCap.MA_PHUCAP IN ('TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4', 'TA_DOCHAI_DG', 'TA_OM_DG', 'TA_BB_DG', 'TA_TRUCTRAI_DG')
		WHERE 
			canBoPhuCap.MA_PHUCAP IN (SELECT Ma_PhuCap FROM TL_DM_PhuCap WHERE PARENT LIKE 'TIENAN2')
		GROUP BY canBoPhuCap.MA_CBO
	), 
	ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo AS MaCanBo,
			canBo.Ten_CanBo AS TenCanBo,
			donVi.Ma_DonVi MaDonVi,
			canBo.Ma_Hieu_CanBo AS MaHieuCanBo,
			capBac.Ma_Cb MaCapBac,
			canBo.BHTN,
			canBo.PCCV
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent = donVi.Ma_DonVi
		INNER  JOIN TL_DM_CapBac capBac
			ON canBo.Ma_CB = capBac.Ma_Cb
		WHERE
			canBo.Thang = @Thang
			AND canBo.Nam = @Nam
			AND canBo.Parent IN (SELECT * FROM f_split(@MaDonVi))
	)

	SELECT
		newid()					AS Id,
		@Thang					AS Thang,
		@Nam					AS Nam,
		canBo.MaCanBo			AS MaCbo,
		canBo.TenCanBo			AS TenCbo,
		canBo.MaDonVi			AS MaDonVi,
		@MaCachTl				AS MaCachTl,
		canBoPhuCap.MA_PHUCAP	AS MaPhuCap,
		CASE
			WHEN (canBoPhuCap.MA_PHUCAP = 'TCVIECLAM_TT' AND cb.Parent in (1, 2, 3)) THEN 0 
			WHEN (canBoPhuCap.MA_PHUCAP = 'NTN' AND canBoPhuCap.GIA_TRI < 5) OR (canBoPhuCap.MA_PHUCAP = 'BHTNCN_HS' AND canBo.BHTN = 0) OR (canBoPhuCap.MA_PHUCAP = 'PCCOV_HS' AND canBo.PCCV = 0) THEN 0
			WHEN canBoPhuCap.MA_PHUCAP = 'TA_TT' THEN soLieuTienAn.GiaTri
			WHEN canBoPhuCap.MA_PHUCAP = 'TA_TT2' THEN soLieuTienAn2.GiaTri
			WHEN canBoPhuCap.MA_PHUCAP = 'TRICHLUONG_TT' THEN (canBoPhuCap.GIA_TRI / 1000) * 1000
			ELSE canBoPhuCap.GIA_TRI
		END						AS GiaTri,
		canBo.MaHieuCanBo		AS MaHieuCanBo,
		canBo.MaCapBac			AS MaCb,
		canBoPhuCap.HuongPC_SN	AS HuongPcSn,
		cachTinhLuong.CongThuc	AS CongThuc,
		CASE WHEN cachTinhLuong.Id IS NULL THEN 1 ELSE 0 END AS IsCalculated
	FROM TL_CanBo_PhuCap canBoPhuCap
	INNER JOIN ThongTinCanBo canBo
		ON canBo.MaCanBo = canBoPhuCap.MA_CBO
	LEFT JOIN SoLieuTienAn soLieuTienAn
		ON canBoPhuCap.MA_CBO = soLieuTienAn.MaCanBo
	LEFT JOIN SoLieuTienAn2 soLieuTienAn2
		ON canBoPhuCap.MA_CBO = soLieuTienAn.MaCanBo
	LEFT JOIN TL_DM_Cach_TinhLuong_Chuan cachTinhLuong
		ON cachTinhLuong.Ma_Cot = canBoPhuCap.MA_PHUCAP
	left join TL_DM_CapBac cb on canBo.MaCapBac = cb.Ma_Cb
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_canbo_phucap_saochep]    Script Date: 11/10/2022 1:45:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		TungNH
-- Create date: 04/05/2022
-- Description:	Sao chép cán bộ sang tháng mới
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_canbo_phucap_saochep]
	@MaCanBo NVARCHAR(MAX),
	@FromYear int,
	@FromMonth int,
	@ToYear int,
	@ToMonth int,
	@IsCopyValue bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	WITH DsCanBo AS (
		SELECT
			Ma_CanBo,
			Ngay_NN,
			Ngay_XN,
			Ngay_TN,
			Thang_TNN,
			Ma_Hieu_CanBo
		FROM TL_DM_CanBo
		WHERE
			Ma_CanBo IN (SELECT * FROM f_split(@MaCanBo))
	), HsPhuCapTruyLinh AS (
		SELECT
			cboPhuCap.MA_CBO,
			cboPhuCap.MA_PHUCAP + '_CU' AS MA_PHUCAP,
			cboPhuCap.GIA_TRI
		FROM TL_CanBo_PhuCap cboPhuCap
		INNER JOIN DsCanBo cbo ON cboPhuCap.MA_CBO = cbo.Ma_CanBo
		WHERE cboPhuCap.MA_PHUCAP IN ('LHT_HS', 'PCCV_HS', 'PCTHUHUT_HS', 'PCCOV_HS', 'PCCU_HS')
	)

	SELECT
		NEWID()					AS Id,
		FORMAT(@ToYear, 'D4') + FORMAT(@ToMonth, 'D2') + cbo.Ma_Hieu_CanBo	AS MaCbo,
		cboPhuCap.MA_PHUCAP		AS MaPhuCap,
		CASE
			WHEN cboPhuCap.MA_PHUCAP IN ('LCS', 'GTNN', 'GTPT_DG', 'TA_DG') THEN phuCap.Gia_Tri 
			WHEN cboPhuCap.MA_PHUCAP IN ('LHT_HS_CU', 'PCCV_HS_CU', 'PCTHUHUT_HS_CU', 'PCCOV_HS_CU', 'PCCU_HS_CU') THEN phuCapTruyLinh.GIA_TRI
			WHEN cboPhuCap.MA_PHUCAP = 'TTL' THEN 0
			WHEN cboPhuCap.ISoThang_Huong IS NOT NULL AND phuCap.IThang_ToiDa IS NOT NULL AND cboPhuCap.ISoThang_Huong >= phuCap.IThang_ToiDa THEN 0
			WHEN cboPhuCap.MA_PHUCAP = 'NTN' THEN (select dbo.f_luong_ntn(cbo.Ngay_NN, cbo.Ngay_XN, cbo.Ngay_TN, cbo.Thang_TNN, @ToMonth, @ToYear))
			WHEN ISNULL(phuCap.bSaoChep, 0) = 0 OR (@IsCopyValue = 1 AND phuCap.bSaoChep = 1) THEN cboPhuCap.GIA_TRI ELSE 0
			--cboPhuCap.bSaoChep IS NOT NULL AND (@IsCopyValue = 0 OR cboPhuCap.bSaoChep = 0) THEN 0 ELSE cboPhuCap.GIA_TRI
		END						AS GiaTri,
		cboPhuCap.HE_SO			AS HeSo,
		cboPhuCap.MA_KMCP		AS MaKmcp,
		cboPhuCap.CONG_THUC		AS CongThuc,
		cboPhuCap.PHANTRAM_CT	AS PhanTramCt,
		cboPhuCap.CHON			AS Chon,
		cboPhuCap.HuongPC_SN	AS HuongPcSn,
		0						AS Flag,
		cboPhuCap.DateStart		AS DateStart,
		cboPhuCap.DateEnd		AS DateEnd,
		CASE
			WHEN cboPhuCap.ISoThang_Huong IS NOT NULL AND (phuCap.IThang_ToiDa IS NULL OR cboPhuCap.ISoThang_Huong < phuCap.IThang_ToiDa) THEN cboPhuCap.ISoThang_Huong + 1
			ELSE cboPhuCap.ISoThang_Huong
		END						AS ISoThang_Huong,
		cboPhuCap.bSaoChep		AS BSaoChep
	FROM TL_CanBo_PhuCap cboPhuCap
	INNER JOIN DsCanBo cbo ON cboPhuCap.MA_CBO = cbo.Ma_CanBo
	LEFT JOIN HsPhuCapTruyLinh phuCapTruyLinh ON cboPhuCap.MA_CBO = phuCapTruyLinh.MA_CBO AND cboPhuCap.MA_PHUCAP = phuCapTruyLinh.MA_CBO
	LEFT JOIN TL_DM_PhuCap phuCap ON cboPhuCap.MA_PHUCAP = phuCap.Ma_PhuCap
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop]    Script Date: 11/10/2022 1:45:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop]
	@ListIdChungTuTongHop ntext,
	@IdChungTu nvarchar(100),
	@IdDonVi nvarchar(10),
	@TenDonVi nvarchar(250),
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int
AS
BEGIN

DECLARE @iIdChungTuOld uniqueidentifier,
	@iIdChungTuDuToanOld uniqueidentifier

SELECT TOP(1) @iIdChungTuOld = ol.ID, @iIdChungTuDuToanOld = ol.IIdChungTuDuToan
FROM TL_QT_ChungTu as ol
INNER JOIN (SELECT * FROM TL_QT_ChungTu WHERE ID = @IdChungTu) as nw 
on ol.Thang = (CASE WHEN nw.Thang = 1 THEN 12 ELSE nw.Thang - 1 END)
	AND ol.Nam = (CASE WHEN nw.Thang = 1 THEN nw.Nam - 1 ELSE nw.Nam END)
	AND ol.Ma_DonVi = nw.Ma_DonVi
	AND ol.sTongHop IS NOT NULL
ORDER BY ol.Ngay_tao DESC

CREATE TABLE #tmp(sXauNoiMa nvarchar(100), DDuToan decimal)

IF(@iIdChungTuOld IS NOT NULL AND @iIdChungTuDuToanOld IS NOT NULL)
BEGIN
	UPDATE TL_QT_ChungTu SET IIdChungTuDuToan = @iIdChungTuDuToanOld WHERE ID = @IdChungTu

	INSERT INTO #tmp(sXauNoiMa, DDuToan)
	SELECT XauNoiMa, SUM(ISNULL(DDuToan, 0))
	FROM TL_QT_ChungTuChiTiet 
	WHERE Id_ChungTu = @iIdChungTuOld
	GROUP BY XauNoiMa
END

INSERT INTO [dbo].[TL_QT_ChungTuChiTiet] ([Id_ChungTu] , [MLNS_Id] , [MLNS_Id_Parent] , [XauNoiMa] , [LNS] , [L] , [K] , [M] , [TM] , [TTM] , [NG] , [TNG] , [TNG1] , [TNG2] , [TNG3] , [MoTa] , [Chuong] , [NamNganSach] , [NguonNganSach] , [NamLamViec] , [iTrangThai] , [iThangQuy] , [Id_DonVi] , [TenDonVi] , [MucAn] , [GhiChu] , [DateCreated] , [UserCreator] , [DateModified] , [UserModifier] , [TongCong] , [BHangCha] , [ChiTietToi] , [SoNgay] , [SoNguoi] , [DieuChinh] , [MaCachTl], [DDuToan])
SELECT @idChungTu,
       MLNS_Id,
       MLNS_Id_Parent,
       XauNoiMa,
       LNS,
       L,
       K,
       M,
       TM,
       TTM,
       NG,
       TNG,
       TNG1,
       TNG2,
       TNG3,
       MoTa,
       NULL,
       @NamNganSach,
       @NguonNganSach,
       @NamLamViec,
       1,
       NULL,
       @IdDonVi,
       @TenDonVi,
       NULL,
       NULL,
       GETDATE(),
       NULL,
       NULL,
       NULL,
       sum(Isnull(TongCong, 0)),
       BHangCha,
       NULL,
       sum(Isnull(SoNgay, 0)),
       sum(Isnull(SoNguoi, 0)),
       sum(Isnull(DieuChinh, 0)),
	   MaCachTl,
	   SUM(ISNULL(tmp.DDuToan, 0))
FROM TL_QT_ChungTuChiTiet ct
LEFT JOIN #tmp as tmp on ct.XauNoiMa = tmp.sXauNoiMa
WHERE ct.Id_ChungTu in
    (SELECT *
     FROM f_split(@ListIdChungTuTongHop))
GROUP BY MLNS_Id,
         MLNS_Id_Parent,
         XauNoiMa,
         LNS,
         L,
         K,
         M,
         TM,
         TTM,
         NG,
         TNG,
         TNG1,
         TNG2,
         TNG3,
         MoTa,
         BHangCha,
		 MaCachTl,
		 tmp.sXauNoiMa

	DROP TABLE #tmp
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_getall_dutoan_hangmuc_by_dutoan]    Script Date: 11/10/2022 1:45:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_vdt_getall_dutoan_hangmuc_by_dutoan] 
	@duToanId nvarchar(200)
AS
BEGIN

select 
				dm.Id as Id,
				dthm.iID_DuToan_HangMuciID as IdDuToanHangMuc,
				dm.sMaHangMuc as MaHangMuc,
				dm.sTenHangMuc as TenHangMuc,
				dthm.iID_ChiPhiID as  IdChiPhi,
				dthm.iID_DuAn_ChiPhi as  IdDuAnChiPhi,
				dm.Id as  IdDuAnHangMuc,
				dthm.iID_DuToanID as  IIdDuToanId,
				ISNULL(dthm.fTienPheDuyet, 0 ) as GiaTriPheDuyet,
				ISNULL(dthm.fTienPheDuyetQDDT, 0 ) as FTienPheDuyetQDDT,
				ISNULL(dthm.fTienPheDuyet, 0 ) - ISNULL(dthm.fTienPheDuyetQDDT, 0 )  as FTienChenhLech,
				dm.iID_ParentID as HangMucParentId,
				CAST(1 as bit) as IsHangMucOld,
				dm.maOrder,
				null as FGiaTriDieuChinh,
				(ISNULL(dthm.fTienPheDuyet, 0 ) - ISNULL(dthm.fGiaTriDieuChinh, 0 )) as GiaTriTruocDieuChinh,
				null as TenChiPhi,
				null as TenLoaiCT,
				dm.IdLoaiCongTrinh ,
				isnull(cast(case when parentId.iID_ParentID is not null or dm.iID_ParentID is null then 1 else 0 end as bit),0)  as IsHangCha
			from VDT_DA_DuToan_DM_HangMuc dm
			inner join VDT_DA_DuToan_HangMuc dthm ON dthm.iID_HangMucID = dm.Id AND dthm.iID_DuToanID = @duToanId

		left join
		(
			select distinct iID_ParentID from VDT_DA_DuToan_DM_HangMuc tb1
			inner join VDT_DA_DuToan_HangMuc tb2 ON tb1.Id = tb2.iID_HangMucID AND tb2.iID_DuToanID = @duToanId
			where  tb1.iID_ParentID is not null ) as parentId ON parentId.iID_ParentID = dm.Id
			order by  MaOrDer
END;
;
;
GO


