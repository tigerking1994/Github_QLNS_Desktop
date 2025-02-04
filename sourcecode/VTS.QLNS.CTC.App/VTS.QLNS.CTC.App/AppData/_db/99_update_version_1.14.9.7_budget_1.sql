GO
IF NOT EXISTS (SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Type = 'rptNS_DuToan_PAPB_MauSo1_PhuLucI')
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_DuToan_PAPB_MauSo1_PhuLucI', NULL, N'rptNS_DuToan_PAPB_MauSo1_PhuLucI', NULL, NULL, NULL, NULL, NULL, N'DU_TOAN_PHUONG_AN_PHAN_BO', NULL, N'Phương án phân bổ dự toán - Theo Công văn 2344/QĐ-CTC', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'Phụ lục I', N'2', N'PHƯƠNG ÁN PHÂN BỔ NGÂN SÁCH NĂM 2024', N'(Kèm theo Báo cáo số …………/BC-…………ngày……/……/20…… của…………………………………………)', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]    Script Date: 10/18/2024 10:41:42 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra]    Script Date: 10/18/2024 10:41:42 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl1]    Script Date: 10/18/2024 10:41:42 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl1]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl1]    Script Date: 10/18/2024 10:41:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl1]
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@FromDate nvarchar(100),
	@ToDate nvarchar(100),
	@DVT int
AS
BEGIN
	
	declare @CapDonVi int = (select top 1 iCapDonVi from DonVi where iLoai = 0 and inamlamviec = @YearOfWork)

	declare @DsDonVi nvarchar(max) = (
	select distinct STUFF((
		SELECT distinct ',' + iID_MaDonVi
		from DonVi where iCapDonVi = 2 and iNamLamViec = @YearOfWork and iTrangThai = 1
		FOR XML PATH(''),TYPE).value('(./text())[1]','NVARCHAR(MAX)')
	  ,1,1,'') iID_MaDonVi)

	declare @DsDonViDuToan nvarchar(max) = (
	select distinct STUFF((
		SELECT distinct ',' + iID_MaDonVi
		from DonVi where iKhoi = 2 and inamlamviec = @YearOfWork and iTrangThai = 1
		FOR XML PATH(''),TYPE).value('(./text())[1]','NVARCHAR(MAX)')
	  ,1,1,'') iID_MaDonVi)

	declare @DsDonViDoanhNghiep nvarchar(max) = (
	select distinct STUFF((
		SELECT distinct ',' + iID_MaDonVi
		from DonVi where iKhoi = 1 and inamlamviec = @YearOfWork and iTrangThai = 1
		FOR XML PATH(''),TYPE).value('(./text())[1]','NVARCHAR(MAX)')
	  ,1,1,'') iID_MaDonVi)

	declare @DsDonViBVTC nvarchar(max) = (
	select distinct STUFF((
		SELECT distinct ',' + iID_MaDonVi
		from DonVi where iKhoi = 3 and inamlamviec = @YearOfWork and iTrangThai = 1
		FOR XML PATH(''),TYPE).value('(./text())[1]','NVARCHAR(MAX)')
	  ,1,1,'') iID_MaDonVi)

	select * into #basemlns
	from NS_MucLucNganSach 
	where iNamLamViec = @YearOfWork 
	and bHangChaDuToan IS NOT NULL 
	and iTrangThai = 1 
	and sLNS in (select * from f_split(@LNS))
	
	--Thu
	select ctct.* into #basedatathu
	from TN_DT_ChungTuChiTiet ctct
	join TN_DT_ChungTu ct on ctct.Id_ChungTu = ct.Id
	where ct.NamLamViec = @YearOfWork and ct.NamNganSach = @YearOfBudget
		and convert(date, ct.NgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.NgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		
	--Chi
	select ctct.* into #basedatachi
	from NS_DT_ChungTuChiTiet ctct
	join NS_DT_ChungTu ct on ctct.iID_DTChungTu = ct.iID_DTChungTu
	where ct.iNamLamViec = @YearOfWork and ct.iNamNganSach = @YearOfBudget
		and convert(date, ct.dNgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.dNgayQuyetDinh, 103) <= convert(date, @ToDate, 103)

	select
       mlns.iID_MLNS IIdMlns,
       mlns.iID_MLNS_Cha IIdMlnsCha,
       mlns.sXauNoiMa,
       mlns.sLNS,
       mlns.sMoTa,
       mlns.bHangCha,
	   sum(isnull(dtnsdg.TuChi, 0))/@DVT fDuToanNSDuocGiao,
	   (sum(isnull(dtnsdg.TuChi, 0)) - sum(isnull(cpb.TuChi, 0)))/@DVT fChoPhanBo,
	   case when sum(isnull(dtnsdg.TuChi, 0)) <> 0 then round(((sum(isnull(dtnsdg.TuChi, 0)) - sum(isnull(cpb.TuChi, 0)))/sum(isnull(dtnsdg.TuChi, 0))), 2) else null end fTyLe1,
	   (sum(isnull(ctbt.TuChi, 0)) + sum(isnull(dt.TuChi, 0)) + sum(isnull(dn.TuChi, 0)) + sum(isnull(bvtc.TuChi, 0)))/@DVT fCong,
	   case when sum(isnull(dtnsdg.TuChi, 0)) <> 0 then round((sum(isnull(ctbt.TuChi, 0)) + sum(isnull(dt.TuChi, 0)) + sum(isnull(dn.TuChi, 0)) + sum(isnull(bvtc.TuChi, 0)))/sum(isnull(dtnsdg.TuChi, 0)), 2) else null end fTyLe2,
	   sum(isnull(ctbt.TuChi, 0))/@DVT fChiTaiBanThan,
	   case when sum(isnull(dtnsdg.TuChi, 0)) <> 0 then round(sum(isnull(ctbt.TuChi, 0))/sum(isnull(dtnsdg.TuChi, 0)), 2) else null end fTyLe3,
	   (sum(isnull(dt.TuChi, 0)) + sum(isnull(dn.TuChi, 0)) + sum(isnull(bvtc.TuChi, 0)))/@DVT fTongSo,
	   case when sum(isnull(dtnsdg.TuChi, 0)) <> 0 then round((sum(isnull(dt.TuChi, 0)) + sum(isnull(dn.TuChi, 0)) + sum(isnull(bvtc.TuChi, 0)))/sum(isnull(dtnsdg.TuChi, 0)), 2) else null end fTyLe4,
	   sum(isnull(dt.TuChi, 0))/@DVT fKhoiDuToan,
	   case when sum(isnull(dtnsdg.TuChi, 0)) <> 0 then round(sum(isnull(dt.TuChi, 0))/sum(isnull(dtnsdg.TuChi, 0)), 2) else null end fTyLe5,
	   sum(isnull(dn.TuChi, 0))/@DVT fKhoiDoanhNghiep,
	   case when sum(isnull(dtnsdg.TuChi, 0)) <> 0 then round(sum(isnull(dn.TuChi, 0))/sum(isnull(dtnsdg.TuChi, 0)), 2) else null end fTyLe6,
	   sum(isnull(bvtc.TuChi, 0))/@DVT fBVTC,
	   case when sum(isnull(dtnsdg.TuChi, 0)) <> 0 then round(sum(isnull(bvtc.TuChi, 0))/sum(isnull(dtnsdg.TuChi, 0)), 2) else null end fTyLe7,
	   'THU' sLoai
	from #basemlns mlns
	left join #basedatathu dtnsdg ON mlns.sXauNoiMa = dtnsdg.XauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.NguonNganSach = @BudgetSource
	left join #basedatathu cpb ON mlns.sXauNoiMa = cpb.XauNoiMa and cpb.iPhanCap = 1 and dtnsdg.NguonNganSach = 1
	left join #basedatathu ctbt ON mlns.sXauNoiMa = ctbt.XauNoiMa and ctbt.iPhanCap = 1 and ctbt.NguonNganSach = @BudgetSource and ctbt.Id_DonVi in (SELECT * FROM dbo.f_split(@DsDonVi))
	left join #basedatathu dt ON mlns.sXauNoiMa = dt.XauNoiMa and dt.iPhanCap = 1 and dt.NguonNganSach = @BudgetSource and dt.Id_DonVi in (SELECT * FROM dbo.f_split(@DsDonViDuToan))
	left join #basedatathu dn ON mlns.sXauNoiMa = dn.XauNoiMa and dn.iPhanCap = 1 and dn.NguonNganSach = @BudgetSource and dn.Id_DonVi in (SELECT * FROM dbo.f_split(@DsDonViDoanhNghiep))
	left join #basedatathu bvtc ON mlns.sXauNoiMa = bvtc.XauNoiMa and bvtc.iPhanCap = 1 and bvtc.NguonNganSach = @BudgetSource and bvtc.Id_DonVi in (SELECT * FROM dbo.f_split(@DsDonViBVTC))
	where mlns.sLNS like '8%'
	group by mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sMoTa,
		mlns.bHangCha

	UNION

	select
       mlns.iID_MLNS IIdMlns,
       mlns.iID_MLNS_Cha IIdMlnsCha,
       mlns.sXauNoiMa,
       mlns.sLNS,
       mlns.sMoTa,
       mlns.bHangCha,
	   sum(isnull(dtnsdg.fTuChi, 0))/@DVT fDuToanNSDuocGiao,
	   (sum(isnull(dtnsdg.fTuChi, 0)) - sum(isnull(cpb.fTuChi, 0)))/@DVT fChoPhanBo,
	   case when sum(isnull(dtnsdg.fTuChi, 0)) <> 0 then round(((sum(isnull(dtnsdg.fTuChi, 0)) - sum(isnull(cpb.fTuChi, 0)))/sum(isnull(dtnsdg.fTuChi, 0))), 2) else null end fTyLe1,
	   (sum(isnull(ctbt.fTuChi, 0)) + sum(isnull(dt.fTuChi, 0)) + sum(isnull(dn.fTuChi, 0)) + sum(isnull(bvtc.fTuChi, 0)))/@DVT fCong,
	   case when sum(isnull(dtnsdg.fTuChi, 0)) <> 0 then round((sum(isnull(ctbt.fTuChi, 0)) + sum(isnull(dt.fTuChi, 0)) + sum(isnull(dn.fTuChi, 0)) + sum(isnull(bvtc.fTuChi, 0)))/sum(isnull(dtnsdg.fTuChi, 0)), 2) else null end fTyLe2,
	   sum(isnull(ctbt.fTuChi, 0))/@DVT fChiTaiBanThan,
	   case when sum(isnull(dtnsdg.fTuChi, 0)) <> 0 then round(sum(isnull(ctbt.fTuChi, 0))/sum(isnull(dtnsdg.fTuChi, 0)), 2) else null end fTyLe3,
	   (sum(isnull(dt.fTuChi, 0)) + sum(isnull(dn.fTuChi, 0)) + sum(isnull(bvtc.fTuChi, 0)))/@DVT fTongSo,
	   case when sum(isnull(dtnsdg.fTuChi, 0)) <> 0 then round((sum(isnull(dt.fTuChi, 0)) + sum(isnull(dn.fTuChi, 0)) + sum(isnull(bvtc.fTuChi, 0)))/sum(isnull(dtnsdg.fTuChi, 0)), 2) else null end fTyLe4,
	   sum(isnull(dt.fTuChi, 0))/@DVT fKhoiDuToan,
	   case when sum(isnull(dtnsdg.fTuChi, 0)) <> 0 then round(sum(isnull(dt.fTuChi, 0))/sum(isnull(dtnsdg.fTuChi, 0)), 2) else null end fTyLe5,
	   sum(isnull(dn.fTuChi, 0))/@DVT fKhoiDoanhNghiep,
	   case when sum(isnull(dtnsdg.fTuChi, 0)) <> 0 then round(sum(isnull(dn.fTuChi, 0))/sum(isnull(dtnsdg.fTuChi, 0)), 2) else null end fTyLe6,
	   sum(isnull(bvtc.fTuChi, 0))/@DVT fBVTC,
	   case when sum(isnull(dtnsdg.fTuChi, 0)) <> 0 then round(sum(isnull(bvtc.fTuChi, 0))/sum(isnull(dtnsdg.fTuChi, 0)), 2) else null end fTyLe7,
	   'CHI' sLoai
	from #basemlns mlns
	left join #basedatachi dtnsdg ON mlns.sXauNoiMa = dtnsdg.sXauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
	left join #basedatachi cpb ON mlns.sXauNoiMa = cpb.sXauNoiMa and cpb.iPhanCap = 1 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
	left join #basedatachi ctbt ON mlns.sXauNoiMa = ctbt.sXauNoiMa and ctbt.iPhanCap = 1 and ctbt.iID_MaNguonNganSach = @BudgetSource and ctbt.iID_MaDonVi in (SELECT * FROM dbo.f_split(@DsDonVi))
	left join #basedatachi dt ON mlns.sXauNoiMa = dt.sXauNoiMa and dt.iPhanCap = 1 and dt.iID_MaNguonNganSach = @BudgetSource and dt.iID_MaDonVi in (SELECT * FROM dbo.f_split(@DsDonViDuToan))
	left join #basedatachi dn ON mlns.sXauNoiMa = dn.sXauNoiMa and dn.iPhanCap = 1 and dn.iID_MaNguonNganSach = @BudgetSource and dn.iID_MaDonVi in (SELECT * FROM dbo.f_split(@DsDonViDoanhNghiep))
	left join #basedatachi bvtc ON mlns.sXauNoiMa = bvtc.sXauNoiMa and bvtc.iPhanCap = 1 and bvtc.iID_MaNguonNganSach = @BudgetSource and bvtc.iID_MaDonVi in (SELECT * FROM dbo.f_split(@DsDonViBVTC))
	where mlns.sLNS not like '8%'
	group by mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sMoTa,
		mlns.bHangCha

END
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra]    Script Date: 10/18/2024 10:41:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra]
	@idDV varchar(max),
	@LoaiChungTu int,
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@dvt int,
	@iLoaiNNS int
AS
BEGIN

DECLARE @DonViBanThan NVARCHAR(MAX)
	SET @DonViBanThan = (SELECT SUBSTRING(( SELECT ',' + iID_MaDonVi AS [text()] 
						FROM DonVi pr
						WHERE iNamLamViec = @NamLamViec 
								and iLoai = 1
								and iCapDonVi = (SELECT TOP 1 iCapDonVi FROM DonVi WHERE iNamLamViec = @NamLamViec and iLoai = 0)
						FOR XML PATH (''), TYPE
							).value('text()[1]','nvarchar(max)'), 2, 1000))

SELECT ml.iID_MLSKTCha IIdMlsktCha,
       ml.iID_MLSKT IIdMlskt,
       ml.sM,
	   case when ml.iID_MLSKTCha = '00000000-0000-0000-0000-000000000000' or ml.iID_MLSKTCha is null then ml.sL
		   else null end sL,
	   ml.sK,
	   ml.sNG,
	   ml.sKyHieu,
       ml.sSTT,
       ml.sSTTBC,
       ml.sMoTa,
       ml.bHangCha ,
       --IsNull(sum(ct.sumToTal), 0)/@dvt TongTuChi ,
		(IsNull(sum(ct.sumDonViBanthan), 0) + IsNull(sum(ct.sumDonVi), 0))/@dvt TongTuChi,
		IsNull(sum(ct.sumTotalMuaHangHienVat), 0)/@dvt TongMuaHangHienVat ,
		IsNull(sum(ct.sumTotalDacThu), 0)/@dvt TongDacThu ,
		IsNull(sum(ct.sumDonVi), 0)/@dvt TongTuChiPB ,
		IsNull(sum(ct.sumDonViMuaHangHienVat), 0)/@dvt TongMuaHangHienVatPB ,
		IsNull(sum(ct.sumDonViDacThu), 0)/@dvt TongDacThuPB,
		IsNull(sum(ct.sumDonViBanthan), 0)/@dvt TuChiBanThan
FROM NS_SKT_MucLuc ml
LEFT JOIN
  (SELECT ml.iID_MLSKT,
          ml.sKyHieu,
          ml.sSTT,
          ml.sSTTBC,
          ml.sMoTa ,
          sum(ISNull(ctc.fTuChi, 0)) sumTotal ,
          sum(ISNull(ctc.fMuaHangCapHienVat, 0)) sumTotalMuaHangHienVat ,
          sum(ISNull(ctc.fPhanCap, 0)) sumTotalDacThu ,
          sumDonVi = 0 ,
          sumDonViMuaHangHienVat = 0 ,
          sumDonViDacThu = 0,
		  sumDonViBanthan = 0
   FROM NS_SKT_MucLuc ml
   LEFT JOIN 
   (
	SELECT ctct.* FROM NS_SKT_ChungTuChiTiet ctct
	JOIN NS_SKT_ChungTu ct ON ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra AND ct.iNamLamViec = ctct.iNamLamViec
	AND (@iLoaiNNS = 0 OR ct.iLoaiNguonNganSach = @iLoaiNNS)
   ) ctc ON ml.sKyHieu = ctc.sKyHieu
   AND ml.iNamLamViec = @NamLamViec
   AND ctc.iNamLamViec = @NamLamViec
   AND ctc.iNamNganSach = @NamNganSach
   AND ctc.iID_MaNguonNganSach = @NguonNganSach
   WHERE ctc.iLoai = 3
     AND ctc.iLoaiChungTu = @LoaiChungTu
   GROUP BY ml.iID_MLSKT,
            ml.sKyHieu,
            ml.sSTT,
            ml.sSTTBC,
            ml.sMoTa

   UNION ALL 
   
   SELECT ml.iID_MLSKT,
        ml.sKyHieu,
        ml.sSTT,
        ml.sSTTBC,
        ml.sMoTa ,
        sumTotal = 0 ,
        sumTotalMuaHangHienVat = 0 ,
        sumTotalDacThu = 0 ,
        sum(ISNull(ctc.fTuChi, 0)) sumDonVi ,
        sum(ISNull(ctc.fMuaHangCapHienVat, 0)) sumDonViMuaHangHienVat ,
        sum(ISNull(ctc.fPhanCap, 0)) sumDonViDacThu,
		sum(ISNull(banthan.fTuChi, 0)) sumDonViBanthan
   FROM NS_SKT_MucLuc ml
   LEFT JOIN 
	(
	SELECT ctct.sKyHieu, sum(isnull(ctct.fTuChi, 0)) fTuChi, sum(isnull(ctct.fMuaHangCapHienVat, 0)) fMuaHangCapHienVat, sum(isnull(ctct.fPhanCap, 0)) fPhanCap FROM NS_SKT_ChungTuChiTiet ctct
	JOIN NS_SKT_ChungTu ct ON ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra AND ct.iNamLamViec = ctct.iNamLamViec AND (@iLoaiNNS = 0 OR ct.iLoaiNguonNganSach = @iLoaiNNS)
	WHERE ctct.iNamLamViec = @NamLamViec
		AND ctct.iNamNganSach = @NamNganSach
		AND ctct.iID_MaNguonNganSach = @NguonNganSach
		AND ctct.iID_MaDonVi in (SELECT * FROM f_split(@idDV))
		AND ctct.iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))
		AND (ctct.iLoai = 4 or ctct.iLoai = 2)
		AND ctct.iLoaiChungTu = @LoaiChungTu
	GROUP BY ctct.sKyHieu
	) ctc ON ml.sKyHieu = ctc.sKyHieu AND ml.iNamLamViec = @NamLamViec
	LEFT JOIN 
	(
	SELECT ctct.sKyHieu, sum(isnull(ctct.fTuChi, 0)) fTuChi FROM NS_SKT_ChungTuChiTiet ctct
	JOIN NS_SKT_ChungTu ct ON ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra AND ct.iNamLamViec = ctct.iNamLamViec AND (@iLoaiNNS = 0 OR ct.iLoaiNguonNganSach = @iLoaiNNS)
	WHERE ctct.iNamLamViec = @NamLamViec
		AND ctct.iNamNganSach = @NamNganSach
		AND ctct.iID_MaNguonNganSach = @NguonNganSach
		AND ctct.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
		AND (ctct.iLoai = 4 or ctct.iLoai = 2)
		AND ctct.iLoaiChungTu = @LoaiChungTu
	GROUP BY ctct.sKyHieu
	) banthan ON ml.sKyHieu = banthan.sKyHieu AND ml.iNamLamViec = @NamLamViec

   GROUP BY ml.iID_MLSKT,
            ml.sKyHieu,
            ml.sSTT,
            ml.sSTTBC,
            ml.sMoTa) AS ct ON ml.sKyHieu = ct.sKyHieu
WHERE ml.iNamLamViec = @NamLamViec
AND ml.iTrangThai = 1
GROUP BY ml.iID_MLSKTCha,
         ml.iID_MLSKT,
         ml.sM,
		 ml.sL,
		 ml.sK,
		 ml.sNG,
         ml.sKyHieu,
         ml.sSTT,
         ml.sSTTBC,
         ml.sMoTa,
         ml.bHangCha
ORDER BY ml.sKyHieu;
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
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]    Script Date: 10/18/2024 10:41:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]
	@idDV varchar(max),
	@LoaiChungTu int,
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@dvt int,
	@iLoaiNNS int
AS
BEGIN
	DECLARE @DonViBanThan NVARCHAR(MAX)
	SET @DonViBanThan = (SELECT SUBSTRING(( SELECT ',' + iID_MaDonVi AS [text()] 
						FROM DonVi pr
						WHERE iNamLamViec = @NamLamViec 
								and iLoai = 1
								and iCapDonVi = (SELECT TOP 1 iCapDonVi FROM DonVi WHERE iNamLamViec = @NamLamViec and iLoai = 0)
						FOR XML PATH (''), TYPE
							).value('text()[1]','nvarchar(max)'), 2, 1000))

	SELECT ml.iID_MLSKTCha IIdMlsktCha,
		   ml.iID_MLSKT IIdMlskt,
		   ml.sM,
		   case when ml.iID_MLSKTCha = '00000000-0000-0000-0000-000000000000' or ml.iID_MLSKTCha is null then ml.sL
		   else null end sL,
		   ml.sK,
		   ml.sNG,
		   ml.sKyHieu,
		   ml.sSTT,
		   ml.sSTTBC,
		   ml.sMoTa,
		   ml.bHangCha ,
		   --IsNull(sum(ct.sumToTal), 0)/@dvt TongTuChi ,
		   (IsNull(sum(ct.sumDonViBanthan), 0) + IsNull(sum(ct.sumDonVi), 0))/@dvt TongTuChi,
		   IsNull(sum(ct.sumTotalMuaHangHienVat), 0)/@dvt TongMuaHangHienVat ,
		   IsNull(sum(ct.sumTotalDacThu), 0)/@dvt TongDacThu ,
		   IsNull(sum(ct.sumDonVi), 0)/@dvt TongTuChiPB ,
		   IsNull(sum(ct.sumDonViMuaHangHienVat), 0)/@dvt TongMuaHangHienVatPB ,
		   IsNull(sum(ct.sumDonViDacThu), 0)/@dvt TongDacThuPB,
		   IsNull(sum(ct.sumDonViBanthan), 0)/@dvt TuChiBanThan
	FROM NS_SKT_MucLuc ml
	LEFT JOIN
	  (
	  SELECT ml.iID_MLSKT,
			  ml.sKyHieu,
			  ml.sSTT,
			  ml.sSTTBC,
			  ml.sMoTa ,
			  sum(ISNull(ctc.fTuChi, 0)) sumTotal ,
			  sum(ISNull(ctc.fMuaHangCapHienVat, 0)) sumTotalMuaHangHienVat ,
			  sum(ISNull(ctc.fPhanCap, 0)) sumTotalDacThu ,
			  sumDonVi = 0 ,
			  sumDonViMuaHangHienVat = 0 ,
			  sumDonViDacThu = 0,
			  sumDonViBanthan = 0
	   FROM NS_SKT_MucLuc ml
	   LEFT JOIN 
	   (SELECT ctct.* FROM NS_SKT_ChungTuChiTiet ctct 
	   JOIN NS_SKT_ChungTu ct ON ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra 
	   WHERE (ctct.iLoai = 3 OR (ctct.iLoai = 2 and ct.iLoai = 2))
	   AND ctct.iLoaiChungTu = @LoaiChungTu
	   AND (@iLoaiNNS = 0 OR ct.iLoaiNguonNganSach = @iLoaiNNS)
	   ) ctc
	   ON ml.sKyHieu = ctc.sKyHieu 
	   AND ml.iNamLamViec = @NamLamViec
	   AND ctc.iNamLamViec = @NamLamViec
	   AND ctc.iNamNganSach = @NamNganSach
	   AND ctc.iID_MaNguonNganSach = @NguonNganSach
   
	   GROUP BY ml.iID_MLSKT,
				ml.sKyHieu,
				ml.sSTT,
				ml.sSTTBC,
				ml.sMoTa

	   UNION ALL 

	   SELECT ml.iID_MLSKT,
			ml.sKyHieu,
			ml.sSTT,
			ml.sSTTBC,
			ml.sMoTa ,
			sumTotal = 0 ,
			sumTotalMuaHangHienVat = 0 ,
			sumTotalDacThu = 0 ,
			sum(ISNull(ctc.fTuChi, 0)) sumDonVi ,
			sum(ISNull(ctc.fMuaHangCapHienVat, 0)) sumDonViMuaHangHienVat ,
			sum(ISNull(ctc.fPhanCap, 0)) sumDonViDacThu,
			sum(ISNull(banthan.fTuChi, 0)) sumDonViBanthan
	   FROM NS_SKT_MucLuc ml
	   LEFT JOIN 
	   (
		SELECT ctct.sKyHieu, sum(isnull(ctct.fTuChi, 0)) fTuChi, sum(isnull(ctct.fMuaHangCapHienVat, 0)) fMuaHangCapHienVat, sum(isnull(ctct.fPhanCap, 0)) fPhanCap FROM NS_SKT_ChungTuChiTiet ctct
		JOIN NS_SKT_ChungTu ct ON ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra AND ct.iNamLamViec = ctct.iNamLamViec AND (@iLoaiNNS = 0 OR ct.iLoaiNguonNganSach = @iLoaiNNS)
		WHERE ctct.iNamLamViec = @NamLamViec
		   AND ctct.iNamNganSach = @NamNganSach
		   AND ctct.iID_MaNguonNganSach = @NguonNganSach
		   AND ctct.iID_MaDonVi in (SELECT * FROM f_split(@idDV))
		   AND ctct.iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))
		   AND (ctct.iLoai = 4 or ctct.iLoai = 2)
		   AND ctct.iLoaiChungTu = @LoaiChungTu
		GROUP BY ctct.sKyHieu
	   ) ctc ON ml.sKyHieu = ctc.sKyHieu AND ml.iNamLamViec = @NamLamViec
	   LEFT JOIN 
	   (
		SELECT ctct.sKyHieu, sum(isnull(ctct.fTuChi, 0)) fTuChi FROM NS_SKT_ChungTuChiTiet ctct
		JOIN NS_SKT_ChungTu ct ON ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra AND ct.iNamLamViec = ctct.iNamLamViec AND (@iLoaiNNS = 0 OR ct.iLoaiNguonNganSach = @iLoaiNNS)
		WHERE ctct.iNamLamViec = @NamLamViec
		   AND ctct.iNamNganSach = @NamNganSach
		   AND ctct.iID_MaNguonNganSach = @NguonNganSach
		   AND ctct.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
		   AND (ctct.iLoai = 4 or ctct.iLoai = 2)
		   AND ctct.iLoaiChungTu = @LoaiChungTu
		GROUP BY ctct.sKyHieu
	   ) banthan ON ml.sKyHieu = banthan.sKyHieu AND ml.iNamLamViec = @NamLamViec
	   
	   --WHERE (ctc.iLoai = 4 or ctc.iLoai = 2) --WHERE ctc.iLoai = 4 
		 --AND ctc.iLoaiChungTu = @LoaiChungTu
		 --AND iID_MaDonVi in
		 --  (SELECT *
			--FROM f_split(@idDV))
		 --AND exists
		 --  (SELECT iID_CTSoKiemTra
			--FROM NS_SKT_ChungTu ct
			--WHERE ct.iID_CTSoKiemTra = ctc.iID_CTSoKiemTra)
		 
	   GROUP BY ml.iID_MLSKT,
				ml.sKyHieu,
				ml.sSTT,
				ml.sSTTBC,
				ml.sMoTa
		) AS ct ON ml.sKyHieu = ct.sKyHieu
	WHERE 
	ml.iNamLamViec = @NamLamViec
	AND ml.iTrangThai = 1
	GROUP BY ml.iID_MLSKTCha,
			 ml.iID_MLSKT,
			 ml.sM,
			 ml.sL,
			 ml.sK,
			 ml.sNG,
			 ml.sKyHieu,
			 ml.sSTT,
			 ml.sSTTBC,
			 ml.sMoTa,
			 ml.bHangCha
	ORDER BY ml.sKyHieu;
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
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_ns_dieuchinh_get_by_chungtu]    Script Date: 10/18/2024 4:28:56 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_dieuchinh_get_by_chungtu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_dieuchinh_get_by_chungtu]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_dieuchinh_get_by_chungtu]    Script Date: 10/18/2024 4:28:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ns_dieuchinh_get_by_chungtu] 
	@VoucherID uniqueidentifier
AS
BEGIN
	
	select
		ctct.iID_DCCTChiTiet,
		ctct.bHangCha,
		ctct.dNgaySua,
		ctct.dNgayTao,
		ctct.fDuKienQtCuoiNam DuKienQtCuoiNam,
		ctct.fDuKienQtDauNam DuKienQtDauNam,
		ctct.iID_DCChungTu,
		ctct.iID_MaDonVi,
		ctct.iID_MaNguonNganSach,
		ctct.iID_MLNS,
		ctct.iID_MLNS_Cha,
		ctct.iNamLamViec,
		ctct.iNamNganSach,
		ctct.sGhiChu,
		ctct.sK,
		ctct.sL,
		ctct.sLNS,
		ctct.sM,
		ctct.sMoTa,
		ctct.sNG,
		ctct.sNguoiSua,
		ctct.sNguoiTao,
		ctct.sTM,
		ctct.sTNG,
		ctct.sTNG1,
		ctct.sTNG2,
		ctct.sTNG3,
		ctct.sTTM,
		ctct.sXauNoiMa,
		ctct.fDuToan DuToanNganSachNam,
		ctct.fDuToanChuyenNamSau DuToanChuyenNamSau,
		ct.iLoaiDuKien
	from NS_DC_ChungTuChiTiet ctct
	join NS_DC_ChungTu ct on ctct.iID_DCChungTu = ct.iID_DCChungTu
	where ct.iID_DCChungTu = @VoucherID

END
GO

/****** Object:  StoredProcedure [dbo].[sp_ns_phanbo_dutoan_get_dulieu_dieuchinh]    Script Date: 10/21/2024 10:11:27 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_phanbo_dutoan_get_dulieu_dieuchinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_phanbo_dutoan_get_dulieu_dieuchinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_phanbo_dutoan_get_dulieu_dieuchinh]    Script Date: 10/21/2024 10:11:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ns_phanbo_dutoan_get_dulieu_dieuchinh] 
	@MaDonVi nvarchar(max),
	@NamLamViec int
AS
BEGIN
	select
		ctct.iID_DCCTChiTiet,
		ctct.bHangCha,
		ctct.dNgaySua,
		ctct.dNgayTao,
		ctct.fDuKienQtCuoiNam DuKienQtCuoiNam,
		ctct.fDuKienQtDauNam DuKienQtDauNam,
		ctct.iID_DCChungTu,
		ctct.iID_MaDonVi,
		ctct.iID_MaNguonNganSach,
		ctct.iID_MLNS,
		ctct.iID_MLNS_Cha,
		ctct.iNamLamViec,
		ctct.iNamNganSach,
		ctct.sGhiChu,
		ctct.sK,
		ctct.sL,
		ctct.sLNS,
		ctct.sM,
		ctct.sMoTa,
		ctct.sNG,
		ctct.sNguoiSua,
		ctct.sNguoiTao,
		ctct.sTM,
		ctct.sTNG,
		ctct.sTNG1,
		ctct.sTNG2,
		ctct.sTNG3,
		ctct.sTTM,
		ctct.sXauNoiMa,
		ctct.fDuToan DuToanNganSachNam,
		ctct.fDuToanChuyenNamSau DuToanChuyenNamSau

	from NS_DC_ChungTuChiTiet ctct
	join NS_DC_ChungTu ct on ctct.iID_DCChungTu = ct.iID_DCChungTu
	where ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ct.bDaTongHop = 1
END
GO
