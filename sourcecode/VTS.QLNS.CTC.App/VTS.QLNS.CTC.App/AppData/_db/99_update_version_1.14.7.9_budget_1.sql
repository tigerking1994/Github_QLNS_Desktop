
GO
IF EXISTS (SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Type = 'rptNS_PhanBo_SoKiemTra_DonVi_NSSD')
UPDATE [dbo].[DM_ChuKy] SET TieuDe1_MoTa = N'THÔNG BÁO',
TieuDe2_MoTa = N'SỐ KIỂM TRA NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG NĂM 2024',
TieuDe3_MoTa = N'(Kèm theo số quyết định số .............., ngày......tháng......năm...........của..........................)'
WHERE Id_Type = 'rptNS_PhanBo_SoKiemTra_DonVi_NSSD'
ELSE
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_PhanBo_SoKiemTra_DonVi_NSSD', NULL, N'rptNS_PhanBo_SoKiemTra_DonVi_NSSD', NULL, NULL, NULL, NULL, NULL, N'SO_KIEM_TRA_PHAN_BO', NULL, N'Báo cáo chi tiết phân bổ số kiểm tra theo đơn vị', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'THÔNG BÁO', N'2', N'SỐ KIỂM TRA NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG NĂM 2024', N'(Kèm theo số quyết định số .............., ngày......tháng......năm...........của..........................)', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO


/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_phan_bo_so_kiem_tra_dv]    Script Date: 9/12/2024 9:46:25 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_phan_bo_so_kiem_tra_dv]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_phan_bo_so_kiem_tra_dv]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_phan_bo_so_kiem_tra_dv]    Script Date: 9/12/2024 9:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_skt_phan_bo_so_kiem_tra_dv]
	@idDV varchar(20),
	@idChungTu varchar(200),
	@LoaiChungTu int,
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@dvt int,
	@iLoai int,
	@iLoaiNNS int
AS
BEGIN
SELECT case when isnull(ml.sNG_Cha, '') = '' and ml.iID_MLSKTCha <> '00000000-0000-0000-0000-000000000000' then null
	   else ml.SL end AS L ,
	   ml.SK AS K ,
	   ml.sM AS M ,
	   ml.sNG AS NG ,
       ml.sKyHieu AS KyHieu ,
       ml.sMoTa AS MoTa ,
       ml.iID_MLSKT AS IdMucLuc ,
       ml.iID_MLSKTCha AS IdParent ,
       ml.sSTT AS STT ,
       ml.sSTTBC AS SSTTBC ,
       ml.bHangCha ,
	   ct.sGhiChu,
       TuChi =ISNULL(SUM(ct.fTuChi), 0)/@dvt ,
       HuyDong =ISNULL(SUM(ct.fHuyDongTonKho), 0)/@dvt ,
       PhanCap =ISNULL(SUM(ct.fPhanCap), 0)/@dvt ,
       MuaHangHienVat =ISNULL(SUM(ct.fMuaHangCapHienVat), 0)/@dvt ,
       DacThu =ISNULL(SUM(ct.fPhanCap), 0)/@dvt ,
       TongCongNSSD = ISNULL(SUM(ct.fTuChi), 0)/@dvt ,
       TongCongNSBD = ISNULL(SUM(ct.fTuChi), 0)/@dvt
FROM NS_SKT_MucLuc ml
LEFT JOIN
(
	SELECT ctct.* FROM NS_SKT_ChungTuChiTiet ctct
	INNER JOIN NS_SKT_ChungTu ctc 
	ON ctct.iID_CTSoKiemTra = ctc.iID_CTSoKiemTra 
	AND ctc.iNamLamViec = ctct.iNamLamViec
	AND ctc.iNamLamViec = @NamLamViec
	AND (@iLoaiNNS = 0 OR ctc.iLoaiNguonNganSach = @iLoaiNNS)
) ct
ON ml.sKyHieu = ct.sKyHieu AND ml.iNamLamViec = ct.iNamLamViec
	AND ct.iNamLamViec = @NamLamViec
	AND ct.iLoai = @iLoai
	AND ct.iLoaiChungTu = @LoaiChungTu
	AND ct.iID_MaDonVi = @idDV
	--AND ct.iID_CTSoKiemTra = @idChungTu
	AND ct.iNamNganSach = @NamNganSach
	AND ct.iID_MaNguonNganSach = @NguonNganSach
WHERE ml.iTrangThai = 1
	AND ml.iNamLamViec = @NamLamViec

GROUP BY case when isnull(ml.sNG_Cha, '') = '' and ml.iID_MLSKTCha <> '00000000-0000-0000-0000-000000000000' then null
	     else ml.SL end,
		 ml.sK , ml.sM , ml.sNG ,
         ml.sKyHieu ,
         ml.sMoTa ,
         ml.iID_MLSKT ,
         ml.iID_MLSKTCha ,
         ml.sSTT ,
         ml.sSTTBC ,
         ml.bHangCha,
		 ct.sGhiChu
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
