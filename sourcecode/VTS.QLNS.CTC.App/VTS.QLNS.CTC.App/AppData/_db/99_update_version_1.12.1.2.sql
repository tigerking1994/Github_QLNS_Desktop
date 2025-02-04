update TL_DM_PhuCap set bSaoChep=0 where Ma_PhuCap in ('PCBAOMAT_HS', 'PCBAOMAT_TT')

update TL_DM_Cach_TinhLuong_Chuan set CongThuc='(LHT_TT+PCCV_TT+HSBL_TT+PCTNVK_TT)*PCDTQUANSU_HS'
where Ma_Cot='PCDTQUANSU_TT' and Ma_CachTL='CACH0'

begin
ALTER TABLE NS_SKT_MucLuc ALTER COLUMN sM nvarchar(10) null;
 if not exists (select 1 from NS_SKT_MucLuc
 where sKyHieu = '1-2-01-24-05' and iNamLamViec =2023)
INSERT INTO [dbo].[NS_SKT_MucLuc]
           ([iID]
           ,[bHangCha]
           
           ,[iID_MLSKT]
           ,[iID_MLSKTCha]
           ,[iNamLamViec]
           ,[iTrangThai]
           ,[KyHieuCha]
           ,[sKyHieu]
           ,[sLoaiNhap]
           ,[sM]
           ,[sMoTa]
           ,[sNG_Cha]
           ,[sNG]
           ,[sSTT]
           ,[sSTTBC]
           )
   select newid(), 
		   0,
		   newid()	,
		   [iID_MLSKT],
		  	2023,
				1,
			'1-2-01-24',
			 '1-2-01-24-05'	,
			 '1,2',
			 '7000',
			N'7000: Tủ sách pháp luật',
			    '51',
		         '81',
				'-',
			'12012405'
			from NS_SKT_MucLuc
			where iNamLamViec = 2023 and sKyHieu ='1-2-01-24'
 if not exists (select 1 from NS_SKT_MucLuc
 where sKyHieu = '1-2-01-15-06' and iNamLamViec =2023)
INSERT INTO [dbo].[NS_SKT_MucLuc]
           ([iID]
           ,[bHangCha]
           ,[iID_MLSKT]
           ,[iID_MLSKTCha]
           ,[iNamLamViec]
           ,[iTrangThai]
           ,[KyHieuCha]
           ,[sKyHieu]
           ,[sLoaiNhap]
           ,[sM]
           ,[sMoTa]
           ,[sNG_Cha]
           ,[sNG]
           ,[sSTT]
           ,[sSTTBC]
           )
   select newid(),
   0,
   newid(),
   [iID_MLSKT],
   2023,
   1,
   	'1-2-01-15',
	'1-2-01-15-06',
		'1,2',
		null, 
		N'Cải cách hành chính',
		 '51',
		 '70',
		 '-',
		 '12011506'
		 from NS_SKT_MucLuc
			where iNamLamViec = 2023 and sKyHieu ='1-2-01-15'
if not exists (select 1 from NS_SKT_MucLuc
 where sKyHieu = '1-2-01-15-07' and iNamLamViec =2023)
INSERT INTO [dbo].[NS_SKT_MucLuc]
           ([iID]
           ,[bHangCha]
           ,[iID_MLSKT]
           ,[iID_MLSKTCha]
           ,[iNamLamViec]
           ,[iTrangThai]
           ,[KyHieuCha]
           ,[sKyHieu]
           ,[sLoaiNhap]
           ,[sM]
           ,[sMoTa]
           ,[sNG_Cha]
           ,[sNG]
           ,[sSTT]
           ,[sSTTBC]
           )
   select newid(),
   0,
   newid(),
   [iID_MLSKT],
   2023,
   1,
   	'1-2-01-15',
	'1-2-01-15-07',
		'1,2',
		null, 
		N'Áp dụng hệ thống chất lượng theo Tiêu chuẩn quốc gia  TCVN ISO 9001: 2005',
		 '51',
		 '70',
		 '-',
		 '12011507'
		 from NS_SKT_MucLuc
			where iNamLamViec = 2023 and sKyHieu ='1-2-01-15'
if not exists (select 1 from NS_SKT_MucLuc
 where sKyHieu = '1-2-01-05-07' and iNamLamViec =2023)
INSERT INTO [dbo].[NS_SKT_MucLuc]
           ([iID]
           ,[bHangCha]
           ,[iID_MLSKT]
           ,[iID_MLSKTCha]
           ,[iNamLamViec]
           ,[iTrangThai]
           ,[KyHieuCha]
           ,[sKyHieu]
           ,[sLoaiNhap]
           ,[sM]
           ,[sMoTa]
           ,[sNG_Cha]
           ,[sNG]
           ,[sSTT]
           ,[sSTTBC]
           )
   select newid(),
   0,
   newid(),
   [iID_MLSKT],
   2023,
   1,
   	'1-2-01-05',
   	'1-2-01-05-07',

		'1,2',
		'7000', 
		N'Chi phí khám sức khỏe tuyển sinh quân sự',
		 '51',
		 '24',
		 '-',
		 '12010507'
		 from NS_SKT_MucLuc
			where iNamLamViec = 2023 and sKyHieu ='1-2-01-05'
if not exists (select 1 from NS_SKT_MucLuc
 where sKyHieu = '1-2-01-05-08' and iNamLamViec =2023)
INSERT INTO [dbo].[NS_SKT_MucLuc]
           ([iID]
           ,[bHangCha]
           ,[iID_MLSKT]
           ,[iID_MLSKTCha]
           ,[iNamLamViec]
           ,[iTrangThai]
           ,[KyHieuCha]
           ,[sKyHieu]
           ,[sLoaiNhap]
           ,[sM]
           ,[sMoTa]
           ,[sNG_Cha]
           ,[sNG]
           ,[sSTT]
           ,[sSTTBC]
           )
   select newid(),
   0,
   newid(),
   [iID_MLSKT],
   2023,
   1,
   '1-2-01-05',
   	'1-2-01-05-08',
		'1,2',
		'7750', 
		N'Hỗ trợ kinh phí đào tạo nguồn nhân lực ',
		 '51',
		 '24',
		 '-',
		 '12010508'
		 from NS_SKT_MucLuc
			where iNamLamViec = 2023 and sKyHieu ='1-2-01-05'


end 


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_mucluc_index_chungtu_bvtc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_mucluc_index_chungtu_bvtc]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_solieuchitiet_index_3]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_solieuchitiet_index_3]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tonghop_skt_benhvien_tuchu]    Script Date: 21/10/2022 9:52:16 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tonghop_skt_benhvien_tuchu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tonghop_skt_benhvien_tuchu]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tonghop_skt_benhvien_tuchu]    Script Date: 21/10/2022 9:52:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_rpt_skt_tonghop_skt_benhvien_tuchu]
@lstDonVi nvarchar(max),
@iNamLamViec int
AS
BEGIN
	SELECT * INTO #tmpDonVi
	FROM f_split(@lstDonVi)
	
	SELECT ml.iID_MLSKTCha as IIdMlsktCha,
		ml.iID_MLSKT IIdMlskt,
		ml.sKyHieu,
		ml.sStt,
		ml.sMoTa,
		ml.bHangCha,
		SUM(ISNULL(dt.fTuChi, 0)) as TongTuChi,
		SUM(ISNULL(dt.fTuChi, 0)) as TongTuChiPB,
		SUM(ISNULL(dt.fMuaHangCapHienVat, 0)) as TongMuaHangHienVat,
		SUM(ISNULL(dt.fMuaHangCapHienVat, 0)) as TongMuaHangHienVatPB,
		SUM(ISNULL(dt.fPhanCap, 0)) as TongDacThu,
		SUM(ISNULL(dt.fPhanCap, 0)) as TongDacThuPB
	FROM NS_SKT_MucLuc as ml 
	LEFT JOIN NS_SKT_ChungTuChiTiet as dt on ml.sKyHieu = dt.sKyHieu
	WHERE dt.iLoai = 2 
		AND ml.iTrangThai = 1
		AND ml.iNamLamViec = @iNamLamViec
		AND dt.iNamLamViec = @iNamLamViec 
		AND dt.iLoaiChungTu = 1
		AND dt.iID_MaDonVi in (SELECT * FROM #tmpDonVi)
	GROUP BY ml.iID_MLSKTCha, ml.iID_MLSKT, ml.sKyHieu, ml.sStt, ml.sMoTa, ml.bHangCha
END
GO

/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_index_3]    Script Date: 20/10/2022 4:59:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_solieuchitiet_index_3]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Loai int,
	@TypeGet int,
	@AgencyId nvarchar(500),
	@LoaiChungTu nvarchar(50),
	@Lns nvarchar(max),
	@VoucherId nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	select *  into #lnsTem  from f_split(@Lns);
	SELECT DISTINCT isnull(chitiet.iID_CTDTDauNamChiTiet, NEWID()) AS Id,
                chitiet.iID_CTDTDauNamChiTiet AS IdDb,
                mlns.iID_MLNS AS MlnsId,
                mlns.iID_MLNS_Cha AS MlnsIdParent,
                mlns.sXauNoiMa AS XauNoiMa,
                mlns.sLNS AS LNS,
                mlns.sL AS L,
                mlns.sK AS K,
                mlns.sM AS M,
                mlns.sTM AS TM,
                mlns.sTTM AS TTM,
                mlns.sNG AS NG,
                mlns.sTNG AS TNG,
                mlns.sTNG1 AS TNG1,
                mlns.sTNG2 AS TNG2,
                mlns.sTNG3 AS TNG3,
                mlns.sMoTa AS MoTa,
                '' AS Chuong,
                mlns.bHangCha,
                mlns.sChiTietToi AS ChiTietToi,
                chitiet.iID_MaDonVi AS IdDonVi,
                chitiet.sTenDonVi AS TenDonVi,
                chitiet.fTuChi AS ChiTiet,
				chitiet.fUocThucHien AS UocThucHien,
                chitiet.fHangNhap AS HangNhap,
                chitiet.fHangMua AS HangMua,
                ISNULL(chitiet.fPhanCap,0) AS PhanCap,
				case when chitiet_phancap.fTuChi is null then ISNULL(chitiet.fPhanCap,0) else ISNULL(chitiet.fPhanCap,0) - ISNULL(chitiet_phancap.fTuChi,0) end as PhanCapConLai,
				ISNull(chitiet_phancap.fTuChi,0) as TuChi,
                chitiet.fChuaPhanCap AS ChuaPhanCap,
                map.sSKT_KyHieu AS SKT_KyHieu,
				mlns.bHangChaDuToan
FROM NS_MucLucNganSach mlns
LEFT JOIN
  (SELECT *
   FROM NS_DTDauNam_ChungTuChiTiet
   WHERE iNamLamViec = @YearOfWork
     AND (iLoai = 3)
     AND iNamNganSach = @YearOfBudget
     AND iID_MaNguonNganSach =@BudgetSource
     AND (iID_MaDonVi = @AgencyId
          OR (@AgencyId = '00'
              AND bKhoa = 0))
	 AND iID_CTDTDauNam = @VoucherId
     AND iLoaiChungTu = @LoaiChungTu ) chitiet ON mlns.sXauNoiMa = chitiet.sXauNoiMa
LEFT JOIN
  (SELECT *
   FROM NS_MLSKT_MLNS
   WHERE iNamLamViec = @YearOfWork ) map ON mlns.sXauNoiMa = map.sNS_XauNoiMa

LEFT JOIN
( select IsNULL(Sum(fTuChi),0) as fTuChi, iID_CTDTDauNamChiTiet
		from  NS_DTDauNam_PhanCap
		group by iID_CTDTDauNamChiTiet
		) as chitiet_phancap ON chitiet_phancap.iID_CTDTDauNamChiTiet = chitiet.iID_CTDTDauNamChiTiet
--inner join  lnsTem ON  mlns.sLNS  = LNSTEM.Item 
WHERE mlns.iNamLamViec = @YearOfWork
  AND mlns.iTrangThai = 1  
  AND mlns.bHangChaDuToan IS NOT NULL
 AND (mlns.sLNS = '1'
     OR ((mlns.sLNS like '104%'
          AND @LoaiChungTu = '2')
         OR (mlns.sLNS not like '104%'
             AND @LoaiChungTu = '1')))
--AND mlns.sLNS IN (SELECT * from f_split(@Lns))
AND mlns.sLNS IN (select * from #lnsTem)
--AND EXISTS (SELECT *  AS sLNS from f_split(@Lns) where Item = mlns.sLNS)
ORDER BY mlns.sLNS,
         mlns.sL,
         mlns.sK,
         mlns.sM,
         mlns.sTM,
         mlns.sTTM,
         mlns.sNG,
         mlns.sTNG;

drop table #lnsTem;

END
;
GO

/****** Object:  StoredProcedure [dbo].[sp_skt_mucluc_index_chungtu_bvtc]    Script Date: 20/10/2022 10:22:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_mucluc_index_chungtu_bvtc]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetOfSource int,
	@VoucherId nvarchar(max),
	@Loai nvarchar(max),
	@LoaiChungTu int,
	@AgencyId nvarchar(500)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT mucluc.iID AS Id,
       mucluc.iID_MLSKT AS IdMucLuc,
       mucluc.sKyHieu AS KyHieu,
       mucluc.sM AS M,
       mucluc.sSTT AS STT,
       mucluc.sMoTa AS MoTa,
       mucluc.bHangCha ,
       mucluc.iNamLamViec AS NamLamViec,
       mucluc.dNgayTao AS DateCreated,
       mucluc.dNguoiTao AS UserCreator,
       mucluc.dNgaySua AS DateModified,
       mucluc.dNguoiSua AS UserModifier,
       mucluc.Muc,
       '' AS LNS,
       mucluc.iID_MLSKTCha AS IdParent ,
       datachitiet.TuChi ,
       ISNULL(datachitiet.HangMua, 0) AS HangMua ,
       ISNULL(datachitiet.HangNhap, 0) AS HangNhap ,
       ISNULL(datachitiet.PhanCap, 0) AS PhanCap ,
       ISNULL(datachitiet.MuaHangHienVat, 0) AS MuaHangHienVat ,
       ISNULL(datachitiet.DacThu, 0) AS DacThu,

	   ISNULL(dutoandaunam.TuChi, 0) AS DtTuChi,
	   ISNULL(dutoandaunam.HangNhap, 0) AS DtHangNhap,
	   ISNULL(dutoandaunam.HangMua, 0) AS DtHangMua,
	   ISNULL(dutoandaunam.PhanCap, 0) AS DtPhanCap,
	   ISNULL(dutoandaunam.DuPhong, 0) AS DtDuPhong,
	   ISNULL(dutoandaunam.ChuaPhanCap, 0) AS DtChuaPhanCap
FROM NS_SKT_MucLuc mucluc
LEFT JOIN
  (SELECT SUM(fTuChi) AS TuChi,
          CAST(0 AS FLOAT) AS HangMua,
          CAST(0 AS FLOAT) AS HangNhap,
          SUM(fPhanCap) AS PhanCap,
          SUM(fMuaHangCapHienVat) AS MuaHangHienVat,
          SUM(fPhanCap) AS DacThu,
          iID_MLSKT
   FROM NS_SKT_ChungTuChiTiet as chitiet
   inner join NS_SKT_ChungTu as chungtu on chitiet.iID_CTSoKiemTra = chungtu.iID_CTSoKiemTra
   WHERE chitiet.iNamLamViec = @YearOfWork
     AND chitiet.iNamNganSach = @YearOfBudget
	 AND chitiet.iID_MaNguonNganSach = @BudgetOfSource
     AND chitiet.iLoaiChungTu = @LoaiChungTu
     AND chitiet.iLoai in (select * from f_split(@loai))
     AND chitiet.iID_MaDonVi = @AgencyId
	 AND chungtu.bKhoa = 1
   GROUP BY iID_MLSKT) datachitiet ON mucluc.iID_MLSKT = datachitiet.iID_MLSKT

LEFT JOIN 
	(
	select 
	SUM(chitiet.fTuChi) AS TuChi, 
	SUM(chitiet.fHangNhap) AS HangNhap,
	SUM(chitiet.fHangMua) AS HangMua,
	SUM(chitiet.fPhanCap) AS PhanCap,
	SUM(chitiet.fDuPhong) AS DuPhong,
	SUM(chitiet.fChuaPhanCap) AS ChuaPhanCap,
	mucluc.iID_MLSKT

	FROM NS_DTDauNam_ChungTuChiTiet chitiet
	left join (select * FROM NS_MLSKT_MLNS where iNamLamViec = @YearOfWork) map on chitiet.sXauNoiMa = map.sNS_XauNoiMa
	left join (select * FROM NS_SKT_MucLuc where iNamLamViec = @YearOfWork) mucluc on map.sSKT_KyHieu = mucluc.sKyHieu
	where
	chitiet.iNamLamViec = @YearOfWork
     AND chitiet.iNamNganSach = @YearOfBudget
	 AND chitiet.iID_MaNguonNganSach = @BudgetOfSource
     AND chitiet.iLoaiChungTu = @LoaiChungTu
	 AND chitiet.iID_MaDonVi = @AgencyId
	 AND chitiet.iID_CTDTDauNam <> @VoucherId
	 AND mucluc.iID_MLSKT is not null
	group by mucluc.iID_MLSKT
	) dutoandaunam on dutoandaunam.iID_MLSKT = mucluc.iID_MLSKT

WHERE mucluc.iNamLamViec = @YearOfWork
  AND mucluc.iTrangThai = 1
ORDER BY mucluc.sKyHieu
END
;
GO
