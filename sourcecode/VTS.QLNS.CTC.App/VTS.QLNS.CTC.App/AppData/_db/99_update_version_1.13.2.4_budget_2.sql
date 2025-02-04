/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_quyetoannam_donvi_index1]    Script Date: 10/5/2023 4:01:27 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_quyetoannam_donvi_index1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_quyetoannam_donvi_index1]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_quyetoannam_donvi_index1]    Script Date: 10/5/2023 4:01:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_qt_rpt_quyetoannam_donvi_index1]
	@YearOfWork int,
	@AgencyId nvarchar(100),
	@LNS nvarchar(max),
	@DataType int,
	@Dvt int,
	@YearOfBuget int --1: Tổng hợp, 2: Năm trước chuyển sang, 3: Năm nay
AS
BEGIN
    ----1. Lấy dữ liệu năm trước chuyển sang

	SELECT sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, ct_namtruocchuyensang.sXauNoiMa,
		   mlns.iID_MLNS,
		   mlns.iID_MLNS_Cha,
		   mlns.sMoTa,
		   iID_MaDonVi,
		   TenDonVi,
		   ChiTieuNamNay = SUM(ChiTieuNamNay) / @Dvt,
		   ChiTieuNamSau = SUM(ChiTieuNamSau) / @Dvt,
		   QuyetToan = SUM(QuyetToan) / @Dvt
		INTO tbl_NamTruocChuyenSang
		FROM 
		(
			--- + Dự toán
			 SELECT sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa,
					iID_MaDonVi,
					Loai = 1,
					ChiTieuNamNay = CASE
									   WHEN @DataType=1 THEN fTuChi
									   ELSE fHienVat
									END,
					ChiTieuNamSau = 0,
					QuyetToan = 0
			   FROM NS_DT_ChungTuChiTiet
			   WHERE iNamLamViec = @YearOfWork
				 AND iNamNganSach in (1,4)
				 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@AgencyId)))
				 AND (@LNS IS NULL OR sLNS in (SELECT * FROM f_split(@LNS)))
				 AND IDuLieuNhan = 0 
				 AND ( isnull(fTuChi,0) > 0 or isnull(fHienVat,0) > 0)
				 

			UNION ALL 

			--- + Quyêt toán
			SELECT sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa,
					iID_MaDonVi,
					Loai = 1,
					ChiTieuNamNay = 0,
					ChiTieuNamSau = fDeNghi_ChuyenNamSau,
					QuyetToan = CASE
									WHEN @DataType = 1 THEN fTuChi_PheDuyet
									ELSE cast(0 AS float)
								END
				   FROM NS_QT_ChungTuChiTiet
				   WHERE iNamLamViec=@YearOfWork
					 AND iNamNganSach in (1, 4)
					 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT *  FROM f_split(@AgencyId)))
					 AND (@LNS IS NULL OR sLNS in (SELECT *  FROM f_split(@LNS)))
					 AND ( isnull(fTuChi_PheDuyet,0) > 0)

		) AS ct_namtruocchuyensang

		--Lấy thêm thông tin mục lục ngân sách
		LEFT JOIN
		  (SELECT sMoTa,
				  sXauNoiMa,
				  iID_MLNS,
				  iID_MLNS_Cha

		   FROM NS_MucLucNganSach
		   WHERE iTrangThai = 1
				 AND iNamLamViec = @YearOfWork) AS mlns ON mlns.sXauNoiMa = ct_namtruocchuyensang.sXauNoiMa 

		-- Lấy thêm thông tin đơn vị
		LEFT JOIN
		  (SELECT iID_MaDonVi AS dv_id,
				  TenDonVi = iID_MaDonVi + ' - ' + sTenDonVi
		   FROM DonVi
		   WHERE iTrangThai = 1
			 AND iNamLamViec = @YearOfWork) AS dv ON dv.dv_id = ct_namtruocchuyensang.iID_MaDonVi

		GROUP BY Loai, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, ct_namtruocchuyensang.sXauNoiMa,
				 iID_MaDonVi, TenDonVi,
				 mlns.iID_MLNS,
				 mlns.iID_MLNS_Cha,
				 mlns.sMoTa

		HAVING SUM(ChiTieuNamNay) <> 0
		OR SUM(ChiTieuNamSau) <> 0
		OR SUM(QuyetToan) <> 0;

	  ---Lấy danh sách cây cha con
	  WITH LNSTreeParentNamTruoc AS
	  (SELECT sLNS,
			sL,
			sK,
			sM,
			sTM,
			sTTM,
			sNG,
			sTNG,
			sTNG1, 
			sTNG2, 
			sTNG3,
			sXauNoiMa,
			iID_MLNS,
			iID_MLNS_Cha,
			cast(iID_MaDonVi AS nvarchar(500)) AS IdDonVi,
			cast(TenDonVi AS nvarchar(500)) AS TenDonVi,
			cast(ChiTieuNamNay AS float) AS ChiTieuNamNay,
			cast(ChiTieuNamSau AS float) AS ChiTieuNamSau,
			cast(QuyetToan AS float) AS QuyetToan,
			sMoTa,
			cast(0 AS bit) AS IsHangCha,
			1 as Level
	   FROM tbl_NamTruocChuyenSang
	   UNION ALL 
	   SELECT mlnsParent.sLNS,
			mlnsParent.sL,
			mlnsParent.sK,
			mlnsParent.sM,
			mlnsParent.sTM,
			mlnsParent.sTTM,
			mlnsParent.sNG,
			mlnsParent.sTNG,
			mlnsParent.sTNG1,
			mlnsParent.sTNG2,
			mlnsParent.sTNG3,
			mlnsParent.sXauNoiMa,
			mlnsParent.iID_MLNS,
			mlnsParent.iID_MLNS_Cha,
			cast('' AS nvarchar(500)) AS IdDonVi,
			cast('' AS nvarchar(500)) AS TenDonVi,
			cast(0 AS float) AS ChiTieuNamNay,
			cast(0 AS float) AS ChiTieuNamSau,
			cast(0 AS float) AS QuyetToan,
			mlnsParent.sMoTa,
			cast(1 AS bit) AS IsHangCha,
			1 as Level
	   FROM NS_MucLucNganSach mlnsParent
	   INNER JOIN LNSTreeParentNamTruoc ON mlnsParent.iID_MLNS = LNSTreeParentNamTruoc.iID_MLNS_Cha
	   WHERE mlnsParent.iNamLamViec = @YearOfWork)

	---Insert dữ liệu vào bảng tạm
		SELECT DISTINCT LNSTreeParentNamTruoc.*
		INTO tbl_NamTruocChuyenSang_Rs
		FROM LNSTreeParentNamTruoc 

	----Insert dòng tiêu đề
		INSERT INTO tbl_NamTruocChuyenSang_Rs (sLNS,sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa,sMoTa,iID_MLNS, iID_MLNS_Cha,IsHangCha,Level,IdDonVi,TenDonVi,ChiTieuNamNay,
				ChiTieuNamSau,QuyetToan   ) 
		SELECT '', '','','','','','','','','','',N'A. NĂM TRƯỚC CHUYỂN SANG',N'A. NĂM TRƯỚC CHUYỂN SANG', NEWID(), NEWID(),1,0, '','',ChiTieuNamNay, ChiTieuNamSau, QuyetToan
				FROM
				(
					SELECT 
						SUM(ChiTieuNamNay) as ChiTieuNamNay,
						SUM(ChiTieuNamSau) as ChiTieuNamSau,
						SUM(QuyetToan) as QuyetToan
					FROM
					tbl_NamTruocChuyenSang_Rs
					where isHangCha = 0
					
				) as ct_namtruoctong

	----Insert dòng tổng cộng
		INSERT INTO tbl_NamTruocChuyenSang_Rs (sLNS,sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa,sMoTa,iID_MLNS, iID_MLNS_Cha,IsHangCha,Level,IdDonVi,TenDonVi,ChiTieuNamNay,
				ChiTieuNamSau,QuyetToan   ) 
		SELECT '', '','','','','','','','','','',N'CỘNG NĂM TRƯỚC',N' CỘNG', NEWID(), NEWID(),1,2, '','',ChiTieuNamNay, ChiTieuNamSau, QuyetToan
		FROM tbl_NamTruocChuyenSang_Rs
		WHERE Level = 0;
		

	---- 2. Lấy dữ liệu Năm nay

	SELECT sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, ct_namnay.sXauNoiMa,
		   mlns.iID_MLNS,
		   mlns.iID_MLNS_Cha,
		   mlns.sMoTa,
		   iID_MaDonVi,
		   TenDonVi,
		   ChiTieuNamNay = SUM(ChiTieuNamNay) / @Dvt,
		   ChiTieuNamSau = SUM(ChiTieuNamSau) / @Dvt,
		   QuyetToan = SUM(QuyetToan) / @Dvt
	INTO tbl_NamNay
	FROM 
		(
			--- + Dự toán
			SELECT sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa,
					iID_MaDonVi,
						Loai = 2,
						ChiTieuNamNay = CASE
										   WHEN @DataType=1 THEN fTuChi
										   ELSE fHienVat
										END,
						ChiTieuNamSau = 0,
						QuyetToan = 0
				   FROM NS_DT_ChungTuChiTiet
				   WHERE iNamLamViec = @YearOfWork
					 AND iNamNganSach = 2
					 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@AgencyId)))
					 AND (@LNS IS NULL OR sLNS in (SELECT * FROM f_split(@LNS)))
					 AND IDuLieuNhan = 0 
					 AND ( isnull(fTuChi,0) > 0 or isnull(fHienVat,0) > 0)

				UNION ALL 
				--- + Quyêt toán
				SELECT sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa,
						iID_MaDonVi,
						Loai = 2,
						ChiTieuNamNay = 0,
						ChiTieuNamSau = fDeNghi_ChuyenNamSau,
						QuyetToan = CASE
										WHEN @DataType = 1 THEN fTuChi_PheDuyet
										ELSE cast(0 AS float)
									END
					   FROM NS_QT_ChungTuChiTiet
					   WHERE iNamLamViec=@YearOfWork
						 AND iNamNganSach = 2
						 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT *  FROM f_split(@AgencyId)))
						 AND (@LNS IS NULL OR sLNS in (SELECT *  FROM f_split(@LNS)))
						 AND ( isnull(fTuChi_PheDuyet,0) > 0)
			) AS ct_namnay
					
			--Lấy thêm thông tin mục lục ngân sách
			LEFT JOIN
				(SELECT sMoTa,
					sXauNoiMa,
					iID_MLNS,
					iID_MLNS_Cha

				  FROM NS_MucLucNganSach
				  WHERE iTrangThai = 1
					AND iNamLamViec = @YearOfWork) AS mlns ON mlns.sXauNoiMa = ct_namnay.sXauNoiMa 

			--- Lấy thêm thông tin đơn vị
			LEFT JOIN
				 (SELECT iID_MaDonVi AS dv_id,TenDonVi = iID_MaDonVi + ' - ' + sTenDonVi
				  FROM DonVi
				  WHERE iTrangThai = 1
				AND iNamLamViec = @YearOfWork) AS dv ON dv.dv_id = ct_namnay.iID_MaDonVi

			GROUP BY Loai, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, ct_namnay.sXauNoiMa,
						 iID_MaDonVi, TenDonVi,
						 mlns.iID_MLNS,
						 mlns.iID_MLNS_Cha,
						 mlns.sMoTa

			HAVING SUM(ChiTieuNamNay) <> 0
			OR SUM(ChiTieuNamSau) <> 0
			OR SUM(QuyetToan) <> 0;

		---Lấy danh sách cha con
		WITH LNSTreeParentNamNay AS
		  (SELECT sLNS,
				sL,
				sK,
				sM,
				sTM,
				sTTM,
				sNG,
				sTNG,
				sTNG1, 
				sTNG2, 
				sTNG3,
				sXauNoiMa,
				iID_MLNS,
				iID_MLNS_Cha,
				cast(iID_MaDonVi AS nvarchar(500)) AS IdDonVi,
				cast(TenDonVi AS nvarchar(500)) AS TenDonVi,
				cast(ChiTieuNamNay AS float) AS ChiTieuNamNay,
				cast(ChiTieuNamSau AS float) AS ChiTieuNamSau,
				cast(QuyetToan AS float) AS QuyetToan,
				sMoTa,
				cast(0 AS bit) AS IsHangCha,
				4 as Level
		   FROM tbl_NamNay
		   UNION ALL 
		   SELECT mlnsParent.sLNS,
				mlnsParent.sL,
				mlnsParent.sK,
				mlnsParent.sM,
				mlnsParent.sTM,
				mlnsParent.sTTM,
				mlnsParent.sNG,
				mlnsParent.sTNG,
				mlnsParent.sTNG1,
				mlnsParent.sTNG2,
				mlnsParent.sTNG3,
				mlnsParent.sXauNoiMa,
				mlnsParent.iID_MLNS,
				mlnsParent.iID_MLNS_Cha,
				cast('' AS nvarchar(500)) AS IdDonVi,
				cast('' AS nvarchar(500)) AS TenDonVi,
				cast(0 AS float) AS ChiTieuNamNay,
				cast(0 AS float) AS ChiTieuNamSau,
				cast(0 AS float) AS QuyetToan,
				mlnsParent.sMoTa,
				cast(1 AS bit) AS IsHangCha,
				4 as Level
		   FROM NS_MucLucNganSach mlnsParent
		   INNER JOIN LNSTreeParentNamNay ON mlnsParent.iID_MLNS = LNSTreeParentNamNay.iID_MLNS_Cha
		   WHERE mlnsParent.iNamLamViec = @YearOfWork)

		---Insert dữ liệu vào bảng tạm
		SELECT DISTINCT  LNSTreeParentNamNay.*
		INTO tbl_NamNay_Rs
		FROM LNSTreeParentNamNay 

		----Insert dòng tiêu đề
		INSERT INTO tbl_NamNay_Rs (sLNS,sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa,sMoTa,iID_MLNS, iID_MLNS_Cha,IsHangCha,Level,IdDonVi,TenDonVi,ChiTieuNamNay,
				ChiTieuNamSau,QuyetToan   ) 
		SELECT '', '','','','','','','','','','',N'B. NĂM NAY	',N'B. NĂM NAY', NEWID(), NEWID(),1,3, '','',ChiTieuNamNay, ChiTieuNamSau, QuyetToan
				FROM
				(
					SELECT 
						SUM(ChiTieuNamNay) as ChiTieuNamNay,
						SUM(ChiTieuNamSau) as ChiTieuNamSau,
						SUM(QuyetToan) as QuyetToan
					FROM
					tbl_NamNay_Rs
					where isHangCha = 0
					
				) as ct_namtruoctong

	----Insert dòng tổng cộng
		INSERT INTO tbl_NamNay_Rs (sLNS,sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa,sMoTa,iID_MLNS, iID_MLNS_Cha,IsHangCha,Level,IdDonVi,TenDonVi,ChiTieuNamNay,
				ChiTieuNamSau,QuyetToan   ) 
		SELECT '', '','','','','','','','','','',N'CỘNG NĂM NAY',N' CỘNG', NEWID(), NEWID(),1,5, '','',ChiTieuNamNay, ChiTieuNamSau, QuyetToan
		FROM tbl_NamNay_Rs
		WHERE Level = 3;


	---Hiển thị kết quả trả về
	 IF @YearOfBuget = 1
		BEGIN

			SELECT * FROM tbl_NamTruocChuyenSang_Rs 
			UNION ALL 
			SELECT * FROM tbl_NamNay_Rs 
			ORDER BY  Level, sXauNoiMa
		END
	ELSE
		IF @YearOfBuget = 2
			BEGIN
				SELECT * FROM tbl_NamTruocChuyenSang_Rs 
				ORDER BY Level, sXauNoiMa
			END 
		ELSE
			BEGIN
				SELECT * from tbl_NamNay_Rs 
				ORDER BY Level, sXauNoiMa
			END

	DROP TABLE tbl_NamTruocChuyenSang;
	DROP TABLE tbl_NamNay;
	DROP TABLE tbl_NamTruocChuyenSang_Rs;
	DROP TABLE tbl_NamNay_Rs;

END
;
;
;
;
--ALTER TABLE 
--NS_QT_ChungTuChiTiet
--ADD fDeNghi_ChuyenNamSau float NULL
GO
