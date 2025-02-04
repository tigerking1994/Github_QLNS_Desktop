/****** Object:  StoredProcedure [dbo].[sp_rpt_phanbo_skt_mucluc_don_vi]    Script Date: 10/19/2023 2:05:40 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_phanbo_skt_mucluc_don_vi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_phanbo_skt_mucluc_don_vi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_phanbo_skt_mucluc_don_vi]    Script Date: 10/19/2023 2:05:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_phanbo_skt_mucluc_don_vi] 
	-- Add the parameters for the stored procedure here
    @Nganh nvarchar(max) = '00,39,65',
	@NamLamViec int = 2024,
	@NamNganSach int= 2,
	@NguonNganSach int = 1,
	@dvt int = 100
AS
BEGIN
-----SKT nam truoc
	SELECT iID_MLSKT IIdMlskt,
       iID_MLSKTCha IIdMlsktCha,
       dt_dv.id idDonVi,
       dt_dv.sTenDonVi,
       dt_dv.iLoai,
       sKyHieu,
       sSTT STT,
       bHangCha as BHangCha,
       sNG,
       sMoTa,
       sNG_Cha sNgCha,
       TuChiSoKiemTraNamTruoc =SUM(IsNull(A.TuChi,0))/@dvt
	   INTO #tmpSKtNamTruoc
FROM
  (SELECT ml.iID_MLSKT ,
                ml.iID_MLSKTCha,
                ml.sKyHieu,
                ml.sNG,
                ml.sMoTa,
                ml.sNG_Cha,
                ml.sSTT,
                ml.bHangCha,
                ct.iID_MaDonVi,
                sTenDonVi,
                IsNull(ct.fTuChi, 0) TuChi
   FROM NS_SKT_ChungTuChiTiet ct
   INNER JOIN NS_SKT_ChungTu chungtu ON ct.iID_CTSoKiemTra = chungtu.iID_CTSoKiemTra --and chungtu.iLoai <> 0
   RIGHT JOIN NS_SKT_MucLuc ml ON ct.sKyHieu = ml.sKyHieu
								  AND ml.iNamLamViec = @NamLamViec
								  AND ml.iTrangThai = 1
   WHERE ct.iNamLamViec= @NamLamViec - 1
     AND ct.iNamNganSach = @NamNganSach
     AND ct.iID_MaNguonNganSach = @NguonNganSach
     AND CT.iLoai = 4 
     AND ct.iLoaiChungTu = 1
     AND ml.sNG in
       (SELECT *
        FROM f_split(@Nganh)))AS A 
LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai=1
   AND iNamLamViec=@NamLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   --WHERE (@Loai = 0 and dt_dv.iLoai != 0) or @Loai <> 0
GROUP BY iID_MLSKT,
         iID_MLSKTCha,
         dt_dv.id,
         dt_dv.sTenDonVi,
         sKyHieu,
         sSTT,
         bHangCha,
         sNG,
         sMoTa,
         sNG_Cha,
         iLoai
         order by sKyHieu;
----SNC nam nay
	SELECT	iID_MLSKT IIdMlskt,
			iID_MLSKTCha IIdMlsktCha,
			dt_dv.id idDonVi,
			dt_dv.sTenDonVi,
			dt_dv.iLoai,
			sKyHieu,
			sSTT STT,
			bHangCha as BHangCha,
			sNG,
			sMoTa,
			sNG_Cha sNgCha,
			TuChiSoNhuCau =SUM(IsNull(A.TuChiSoNhuCau,0))/@dvt,
			TuChiSoKiemTraDeXuat =SUM(IsNull(A.TuChiSoKiemTraDeXuat,0))/@dvt
			INTO #tmpSKtNamNay
FROM
  (SELECT		ml.iID_MLSKT ,
                ml.iID_MLSKTCha,
                ml.sKyHieu,
                ml.sNG,
                ml.sMoTa,
                ml.sNG_Cha,
                ml.sSTT,
                ml.bHangCha,
                ct.iID_MaDonVi,
                sTenDonVi,			
				(CASE When ct.iLoai=0 Then fTuChi Else 0 End ) as TuChiSoNhuCau,
				(CASE When ct.iLoai=4 Then fTuChi Else 0 End ) as TuChiSoKiemTraDeXuat
   FROM NS_SKT_ChungTuChiTiet ct
   INNER JOIN NS_SKT_ChungTu chungtu ON ct.iID_CTSoKiemTra = chungtu.iID_CTSoKiemTra --and chungtu.iLoai <> 0
   RIGHT JOIN NS_SKT_MucLuc ml ON ct.sKyHieu = ml.sKyHieu
   AND ml.iNamLamViec = @NamLamViec
   AND ml.iTrangThai = 1
   WHERE ct.iNamLamViec= @NamLamViec
     AND ct.iNamNganSach = @NamNganSach
     AND ct.iID_MaNguonNganSach = @NguonNganSach
     AND CT.iLoai in (1,4) 
     AND ct.iLoaiChungTu = 1
     AND ml.sNG in
       (SELECT *
        FROM f_split(@Nganh)))AS A 
LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai=1
   AND iNamLamViec=@NamLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   --WHERE (@Loai = 0 and dt_dv.iLoai != 0) or @Loai <> 0
GROUP BY iID_MLSKT,
         iID_MLSKTCha,
         dt_dv.id,
         dt_dv.sTenDonVi,
         sKyHieu,
         sSTT,
         bHangCha,
         sNG,
         sMoTa,
         sNG_Cha,
         iLoai
         order by sKyHieu;


		 ---DuToanDuocGiao n -1
		 SELECT  sSKT_KyHieu,SUM(ISNULL(fUocThucHien,0))/@dvt as DuToan INTO #tmpDutoan 
		 
		 FROM NS_DTDauNam_ChungTuChiTiet ct
		 INNER JOIN  NS_MLSKT_MLNS map on ct.sXauNoiMa = map.sNS_XauNoiMa and  map.iNamLamViec = @NamLamViec - 1
		 WHERE ct.iNamLamViec = @NamLamViec -1
		 AND iNamNganSach = @NamNganSach
		 AND iID_MaNguonNganSach = @NguonNganSach
		 GROUP BY  sSKT_KyHieu;

		 ----OutPut
		 select namNay.STT,
				namNay.IIdMlskt,
				namNay.IIdMlsktCha,
				namNay.BHangCha,
				namNay.iLoai,
				namNay.idDonVi,
				namNay.sTenDonVi,
				namNay.sMoTa,
				namNay.sKyHieu,
				namNay.sNG,
				namNay.sNgCha,
				namNay.TuChiSoNhuCau,
				namNay.TuChiSoKiemTraDeXuat,
				sktNamTruoc.TuChiSoKiemTraNamTruoc,
				ISNULL(dutoan.DuToan,0) as DuToan
				INTO #tmpOutPut
		 FROM #tmpSKtNamNay namNay
		 LEFT JOIN #tmpSKtNamTruoc sktNamTruoc on namNay.sKyHieu = sktNamTruoc.sKyHieu 
		 LEFT JOIN #tmpDutoan dutoan on namNay.sKyHieu = dutoan.sSKT_KyHieu;
		 --ORDER BY namNay.sNG,namNay.sKyHieu;

		 --Insert ban ghi cha

		 WiTH #treeParent
		 AS
		 (
		 	SELECT * FROM NS_SKT_MucLuc where iID_MLSKT IN (select DISTINCT IIdMlsktCha FROM #tmpOutPut) AND iNamLamViec = @NamLamViec and iTrangThai = 1 
		 	UNION ALL
		 	SELECT pr.* from NS_SKT_MucLuc pr  
		 	INNER JOIN #treeParent child ON pr.iID_MLSKT = child.iID_MLSKTCha AND pr.iNamLamViec = @NamLamViec and pr.iTrangThai = 1 
		 )
		 SELECT DISTINCT * INTO #tmpParent FROM #treeParent;

		 INSERT INTO #tmpOutPut(STT, IIdMlskt, IIdMlsktCha, BHangCha, iLoai, idDonVi, sTenDonVi, sMoTa, sKyHieu, sNG, sNgCha, TuChiSoNhuCau, TuChiSoKiemTraDeXuat, TuChiSoKiemTraNamTruoc, DuToan)
		( SELECT 
				mlnsCha.sSTT,
				mlnsCha.iID_MLSKT,
				mlnsCha.iID_MLSKTCha,mlnsCha.bHangCha,
				'',
				'',
				'',
				mlnsCha.sMoTa,
				mlnsCha.sKyHieu,
				mlnsCha.sNG,
				mlnsCha.sNG_Cha,
				0,
				0,
				0,
				0
		 FROM 
		 #tmpParent as mlnsCha);


		 With #TreeEndOut(STT, IIdMlskt, IIdMlsktCha, BHangCha, iLoai, idDonVi, sTenDonVi, sMoTa, sKyHieu, sNG, sNgCha, TuChiSoNhuCau, TuChiSoKiemTraDeXuat, TuChiSoKiemTraNamTruoc, FDuToan, position)
		 AS
		 (
			SELECT *, CAST(ROW_NUMBER() OVER(ORDER BY STT) AS NVARCHAR(MAX)) AS position 
			FROM #tmpOutPut 
			WHERE IIdMlsktCha = '00000000-0000-0000-0000-000000000000'
			UNION ALL
			SELECT child.*, CONCAT(parent.position,'.',CAST(ROW_NUMBER() OVER(ORDER BY child.sNG,child.sKyHieu) AS NVARCHAR(MAX))) AS position 
			FROM #tmpOutPut as child
			INNER JOIN #TreeEndOut as parent ON parent.IIdMlskt = child.IIdMlsktCha
		 )

		 --SELECT OUTPUT
		 SELECT *,
		 	cast('/' + replace(position, '.', '/') + '/' as hierarchyid) AS sort
		 FROM  #TreeEndOut
		 ORDER  BY sort;

		 DROP TABLE #tmpDutoan;
		 DROP TABLE #tmpSKtNamNay;
		 DROP TABLE #tmpSKtNamTruoc;
		 DROP TABLE #tmpOutPut;
		 DROP TABLE #tmpParent;
END
GO
