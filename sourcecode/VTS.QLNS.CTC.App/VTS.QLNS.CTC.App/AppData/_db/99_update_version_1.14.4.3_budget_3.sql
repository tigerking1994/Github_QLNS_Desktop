/****** Object:  StoredProcedure [dbo].[sp_nc3y_chungtu_chitiet_tao_tonghop]    Script Date: 5/9/2024 7:41:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nc3y_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nc3y_chungtu_chitiet_tao_tonghop]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_NS_NC3Y_ChungTuChiTiet_NS_SKT_ChungTu_iID_CTSoKiemTra]') AND parent_object_id = OBJECT_ID(N'[dbo].[NS_NC3Y_ChungTuChiTiet]'))
ALTER TABLE [dbo].[NS_NC3Y_ChungTuChiTiet] DROP CONSTRAINT [FK_NS_NC3Y_ChungTuChiTiet_NS_SKT_ChungTu_iID_CTSoKiemTra]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__NS_NC3Y_C__fNCNa__18F838D8]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[NS_NC3Y_ChungTuChiTiet] DROP CONSTRAINT [DF__NS_NC3Y_C__fNCNa__18F838D8]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__NS_NC3Y_C__fNCNa__1804149F]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[NS_NC3Y_ChungTuChiTiet] DROP CONSTRAINT [DF__NS_NC3Y_C__fNCNa__1804149F]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__NS_NC3Y_C__fNCNa__170FF066]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[NS_NC3Y_ChungTuChiTiet] DROP CONSTRAINT [DF__NS_NC3Y_C__fNCNa__170FF066]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_NS_NC3Y_ChungTuChiTiet_fDuToan1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[NS_NC3Y_ChungTuChiTiet] DROP CONSTRAINT [DF_NS_NC3Y_ChungTuChiTiet_fDuToan1]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__NS_NC3Y_C__fDuTo__161BCC2D]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[NS_NC3Y_ChungTuChiTiet] DROP CONSTRAINT [DF__NS_NC3Y_C__fDuTo__161BCC2D]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__NS_NC3Y_C__dNgay__1527A7F4]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[NS_NC3Y_ChungTuChiTiet] DROP CONSTRAINT [DF__NS_NC3Y_C__dNgay__1527A7F4]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__NS_NC3Y_C__iID_C__143383BB]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[NS_NC3Y_ChungTuChiTiet] DROP CONSTRAINT [DF__NS_NC3Y_C__iID_C__143383BB]
END
GO
/****** Object:  Table [dbo].[NS_NC3Y_ChungTuChiTiet]    Script Date: 5/9/2024 7:41:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NS_NC3Y_ChungTuChiTiet]') AND type in (N'U'))
DROP TABLE [dbo].[NS_NC3Y_ChungTuChiTiet]
GO
/****** Object:  Table [dbo].[NS_NC3Y_ChungTuChiTiet]    Script Date: 5/9/2024 7:41:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NS_NC3Y_ChungTuChiTiet](
	[iID_CTNC3YChiTiet] [uniqueidentifier] NOT NULL,
	[dNgaySua] [datetime] NULL,
	[dNgayTao] [datetime] NULL,
	[fDuToan] [float] NOT NULL,
	[fUocTH] [float] NOT NULL,
	[fNCNam1] [float] NOT NULL,
	[fNCNam2] [float] NOT NULL,
	[fNCNam3] [float] NOT NULL,
	[iID_CTSoKiemTra] [uniqueidentifier] NOT NULL,
	[iID_MaDonVi] [nvarchar](50) NULL,
	[iID_MaNguonNganSach] [int] NULL,
	[iID_MLSKT] [uniqueidentifier] NOT NULL,
	[iLoai] [int] NOT NULL,
	[iLoaiChungTu] [int] NULL,
	[iNamLamViec] [int] NOT NULL,
	[iNamNganSach] [int] NULL,
	[sGhiChu] [nvarchar](max) NULL,
	[sKyHieu] [nvarchar](max) NULL,
	[sMoTa] [nvarchar](max) NOT NULL,
	[sNguoiSua] [nvarchar](50) NULL,
	[sNguoiTao] [nvarchar](50) NULL,
	[sTenDonVi] [nvarchar](250) NULL,
 CONSTRAINT [PK_NC3Y_ChungTuChiTiet] PRIMARY KEY NONCLUSTERED 
(
	[iID_CTNC3YChiTiet] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[NS_NC3Y_ChungTuChiTiet] ADD  CONSTRAINT [DF__NS_NC3Y_C__iID_C__143383BB]  DEFAULT (newid()) FOR [iID_CTNC3YChiTiet]
GO
ALTER TABLE [dbo].[NS_NC3Y_ChungTuChiTiet] ADD  CONSTRAINT [DF__NS_NC3Y_C__dNgay__1527A7F4]  DEFAULT (getdate()) FOR [dNgayTao]
GO
ALTER TABLE [dbo].[NS_NC3Y_ChungTuChiTiet] ADD  CONSTRAINT [DF__NS_NC3Y_C__fDuTo__161BCC2D]  DEFAULT ((0)) FOR [fDuToan]
GO
ALTER TABLE [dbo].[NS_NC3Y_ChungTuChiTiet] ADD  CONSTRAINT [DF_NS_NC3Y_ChungTuChiTiet_fDuToan1]  DEFAULT ((0)) FOR [fUocTH]
GO
ALTER TABLE [dbo].[NS_NC3Y_ChungTuChiTiet] ADD  CONSTRAINT [DF__NS_NC3Y_C__fNCNa__170FF066]  DEFAULT ((0)) FOR [fNCNam1]
GO
ALTER TABLE [dbo].[NS_NC3Y_ChungTuChiTiet] ADD  CONSTRAINT [DF__NS_NC3Y_C__fNCNa__1804149F]  DEFAULT ((0)) FOR [fNCNam2]
GO
ALTER TABLE [dbo].[NS_NC3Y_ChungTuChiTiet] ADD  CONSTRAINT [DF__NS_NC3Y_C__fNCNa__18F838D8]  DEFAULT ((0)) FOR [fNCNam3]
GO
ALTER TABLE [dbo].[NS_NC3Y_ChungTuChiTiet]  WITH CHECK ADD  CONSTRAINT [FK_NS_NC3Y_ChungTuChiTiet_NS_SKT_ChungTu_iID_CTSoKiemTra] FOREIGN KEY([iID_CTSoKiemTra])
REFERENCES [dbo].[NS_SKT_ChungTu] ([iID_CTSoKiemTra])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NS_NC3Y_ChungTuChiTiet] CHECK CONSTRAINT [FK_NS_NC3Y_ChungTuChiTiet_NS_SKT_ChungTu_iID_CTSoKiemTra]
GO
/****** Object:  StoredProcedure [dbo].[sp_nc3y_chungtu_chitiet_tao_tonghop]    Script Date: 5/9/2024 7:41:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_nc3y_chungtu_chitiet_tao_tonghop]
	@ListIdChungTuTongHop ntext,
	@IdChungTu nvarchar(100),
	@IdDonVi nvarchar(10),
	@TenDonVi nvarchar(250),
	@LoaiChungTu int,
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@LoaiNguonNganSach int
AS
BEGIN

INSERT INTO [dbo].[NS_NC3Y_ChungTuChiTiet] (iID_CTSoKiemTra , iID_MaDonVi , sTenDonVi , iID_MLSKT, sKyHieu, sMoTa , fDuToan , fUocTH , fNCNam1, fNCNam2, fNCNam3, sGhiChu , iLoai , iNamLamViec , dNgayTao , sNguoiTao , dNgaySua , sNguoiSua , iLoaiChungTu , iNamNganSach , iID_MaNguonNganSach)
SELECT @idChungTu,
       @IdDonVi,
	   @TenDonVi,
       iID_MLSKT,
	   sKyHieu,
       sMoTa ,
       sum(fDuToan) ,
       sum(fUocTH) ,
	   sum(fNCNam1) ,
	   sum(fNCNam2) ,
	   sum(fNCNam3),
       NULL ,
       99 ,
       @NamLamViec ,
       NULL ,
       NULL ,
       NULL ,
       NULL ,
       @LoaiChungTu ,
       @NamNganSach ,
       @NguonNganSach
FROM NS_NC3Y_ChungTuChiTiet
WHERE iID_CTSoKiemTra in
    (SELECT *
     FROM f_split(@ListIdChungTuTongHop))
GROUP BY iID_MLSKT,
		 sKyHieu,
         sMoTa;

--danh dau chung tu da tong hop
update NS_SKT_ChungTu set bDaTongHop = 0 
where iNamLamViec = @NamLamViec 
		and iNamNganSach = @NamNganSach 
		and iID_MaNguonNganSach = @NguonNganSach
		and iLoaiChungTu = @LoaiChungTu
		and iLoai = 0
		and iLoaiNguonNganSach = @LoaiNguonNganSach;

update NS_SKT_ChungTu set bDaTongHop = 1 
where iID_CTSoKiemTra in
    (SELECT *
     FROM f_split(@ListIdChungTuTongHop))
	 and iLoaiNguonNganSach = @LoaiNguonNganSach


END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi_clone]    Script Date: 5/10/2024 3:27:15 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_dutoan_donvi_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_dutoan_donvi_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi_clone]    Script Date: 5/10/2024 3:27:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_rpt_dutoan_donvi_clone]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@EstimateAgencyId nvarchar(max),
	@AgencyId nvarchar(max),
	@LNS nvarchar(max),
	@QuarterMonth nvarchar(100),
	@QuarterMonthBefore nvarchar(100),
	@VoucherDate datetime,
	@Dvt int
AS
BEGIN
	declare @AdjustMonth nvarchar(100)
	declare @CurrentMonth nvarchar(100)
	if (@QuarterMonth like '%,%')
	begin
		set @AdjustMonth = (select top(1) Item from f_split(@QuarterMonth))
		set @CurrentMonth = REPLACE(@QuarterMonth,@AdjustMonth + ',', '')
	end
	else 
	begin
		set @AdjustMonth = @QuarterMonth
		set @CurrentMonth = ''
	end
	DECLARE @IsDonViCha bit;
	IF @EstimateAgencyId = @AgencyId
		SET @IsDonViCha = 0
	ELSE
		SET @IsDonViCha = 1
	SELECT sLNS,
		   iID_MLNS,
		   TenDonVi,
		   SoNguoi = SUM(SoNguoi),
		   SoNgay = SUM(SoNgay),
		   ChiTieu = SUM(ChiTieu) / @Dvt,
		   TuChi2 = SUM(TuChi2) / @Dvt,
		   TuChi = SUM(TuChi) / @Dvt 
		   INTO #tblEstimateSettlement
	FROM
	  (--chi tieu theo don vi
	  SELECT sLNS,
			iID_MLNS,
			iID_MaDonVi,
			SoNguoi = 0,
			SoNgay = 0,
			ChiTieu = fTuChi + fHangNhap + fHangMua,
			TuChi2 = 0,
			TuChi = 0
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iID_DTChungTu in 
		(
			SELECT iID_DTChungTu 
			FROM NS_DT_ChungTu 
			WHERE iNamLamViec = @YearOfWork
				AND iNamNganSach = @YearOfBudget
				AND iID_MaNguonNganSach = @BudgetSource
				AND (sSoQuyetDinh is not null or sSoQuyetDinh <> '')
				AND (cast(dNgayQuyetDinh as DATE) <= cast(@VoucherDate as date)
				OR (YEAR(CAST(dNgayQuyetDinh AS DATE)) - YEAR(CAST(@VoucherDate AS DATE)) = 1
				AND MONTH(CAST(dNgayQuyetDinh AS DATE)) IN (1,2,3) AND MONTH(CAST(@VoucherDate AS DATE)) = 12))
		)
		 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@EstimateAgencyId)))
		 AND (@LNS IS NULL OR sLNS in (SELECT * FROM dbo.splitstring(@LNS)))
		 AND IDuLieuNhan = 0		 
	   --số đã quyết toán
	   UNION ALL 
	   SELECT sLNS=dbo.f_get_lns_by_id(@YearOfWork,iID_MLNS),
			iID_MLNS,
			CASE
			    WHEN @IsDonViCha = 1 THEN @EstimateAgencyId
		    ELSE 
				iID_MaDonVi
		    END AS iID_MaDonVi,
			SoNguoi = 0,
			SoNgay = 0,
			ChiTieu =0,
			TuChi2 = TuChi,
			TuChi = 0
		from f_quyettoan_tonghop_dieuchinh(@YearOfWork, @YearOfBudget,@BudgetSource,@QuarterMonthBefore,@AdjustMonth,@AgencyId,@LNS)
	   --FROM f_quyettoan_tonghop_dieuchinh(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonthBefore, @AgencyId, @LNS)
		 
	   --quyết toán đợt này
	   UNION ALL 
	   SELECT sLNS=dbo.f_get_lns_by_id(@YearOfWork,iID_MLNS),
			iID_MLNS,
			CASE
			    WHEN @IsDonViCha = 1 THEN @EstimateAgencyId
		    ELSE 
				iID_MaDonVi
		    END AS iID_MaDonVi,
			SoNguoi,
			SoNgay,
			ChiTieu = 0,
			TuChi2 = 0,
			TuChi
	   FROM f_quyettoan_tonghop_dieuchinh_hientai(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonth, @AgencyId, @lns)
	   
	   UNION ALL 
	   SELECT sLNS=dbo.f_get_lns_by_id(@YearOfWork,iID_MLNS),
			iID_MLNS,
			CASE
			    WHEN @IsDonViCha = 1 THEN @EstimateAgencyId
		    ELSE 
				iID_MaDonVi
		    END AS iID_MaDonVi,
			SoNguoi,
			SoNgay,
			ChiTieu = 0,
			TuChi2 = 0,
			TuChi
	   FROM f_quyettoan_tonghop_dieuchinh_hientai_quy(@YearOfWork, @YearOfBudget, @BudgetSource, @CurrentMonth, @AgencyId, @lns)) AS ct
	LEFT JOIN
	  (SELECT iID_MaDonVi,
			  TenDonVi = iID_MaDonVi + ' - ' + sTenDonVi
	   FROM DonVi
	   WHERE iTrangThai=1
		 AND iNamLamViec = @YearOfWork) AS dv ON dv.iID_MaDonVi = ct.iID_MaDonVi
	GROUP BY 
			 sLNS,
			 TenDonVi,
			 iID_MLNS
	HAVING SUM(TuChi) <> 0
	OR SUM(TuChi2) <> 0
	OR SUM(ChiTieu) <> 0;

	select distinct sLNS into #tblLNS from #tblEstimateSettlement;
	SELECT mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.sLNS,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.sTNG,
		mlns.sTNG1,
		mlns.sTNG2,
		mlns.sTNG3,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		mlns.bHangCha as IsHangCha,
		isnull(dt.SoNguoi, 0) as SoNguoi,
		isnull(dt.SoNgay, 0) as SoNgay,
		isnull(dt.ChiTieu, 0) as ChiTieu,
		isnull(dt.TuChi2, 0) as TuChi2,
		isnull(dt.TuChi, 0) as TuChi,
		TenDonVi
	FROM (
		SELECT *
		   FROM NS_MucLucNganSach
		   WHERE iTrangThai=1
			 AND iNamLamViec = @YearOfWork
			 AND sLNS in
				(SELECT DISTINCT VALUE
					FROM
					(SELECT CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1,
							CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3,
							CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5,
							CAST(sLNS AS nvarchar(10)) LNS
					FROM #tblLNS) LNS UNPIVOT (value FOR col in (LNS1, LNS3, LNS5, LNS)) un)) mlns
	LEFT JOIN #tblEstimateSettlement dt
	ON dt.iID_MLNS = mlns.iID_MLNS
	WHERE bHangCha = 1 OR (bHangCha = 0 AND (dt.TuChi <> 0 OR dt.ChiTieu <> 0 OR dt.TuChi2 <> 0))
	ORDER BY mlns.sXauNoiMa, dt.TenDonVi
	DROP TABLE #tblEstimateSettlement, #tblLNS;


END
;
;
GO

BEGIN 
	IF NOT EXISTS ( SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Code in ('rptNS_SNC3Y_TongHop'))
	BEGIN
		INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
		VALUES (N'f1dff55e-c694-42da-aaf2-6f1fac9f06d1', 1, N'11012', NULL, N'11012', NULL, N'NTT', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_SNC3Y_TongHop', NULL, N'rptNS_SNC3Y_TongHop', NULL, NULL, NULL, NULL, NULL, N'SO_NHU_CAU', NULL, N'Báo cáo số nhu cầu tổng hợp 3 năm', N'Tên 1', NULL, N'Tên 1', NULL, N'Tên 1', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'11015', NULL, N'11011', NULL, N'Nguyễn Văn QUyết', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'Nhu cầu chi thường xuyên NSNN 3 năm (2024 - 2026)', NULL, N'(Kèm theo Công văn số: ......./............... ngày ...../5/2022 của .................)', NULL, NULL, NULL, NULL, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL)
	END
END
;
;
GO

BEGIN 
	IF NOT EXISTS ( SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Code in ('rptNS_SNC3Y_ChiTiet'))
	BEGIN
		INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
		VALUES (N'076ae036-2d38-401a-8697-894936cc4308', 1, N'NTT', NULL, N'NTT', NULL, N'NLTT', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_SNC3Y_ChiTiet', NULL, N'rptNS_SNC3Y_ChiTiet', NULL, NULL, NULL, NULL, NULL, N'SO_NHU_CAU', NULL, N'Báo cáo chi tiết số nhu cầu 3 năm', N'TLBCTTLL', NULL, N'TTBNG', NULL, N'BTTU - Nguyễn Mạnh Cường', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Nguyễn Văn QUyết', NULL, N'Nguyễn Văn QUyết', NULL, N'Nguyễn Văn QUyết', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'Nhu cầu chi thường xuyên NSNN 3 năm 2024-2026', NULL, N'(Kèm theo Công văn số: ......./............... ngày ...../5/2024 của .................)', N'', NULL, NULL, NULL, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL)
	END
END
;
;
GO