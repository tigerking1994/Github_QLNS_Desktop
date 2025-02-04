/****** Object:  StoredProcedure [dbo].[sp_rpt_tn_dutoan_hd4554]    Script Date: 12/27/2024 2:25:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_tn_dutoan_hd4554]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_tn_dutoan_hd4554]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_tn_dutoan_hd4554]    Script Date: 12/27/2024 2:25:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_tn_dutoan_hd4554]
	@agencies nvarchar(max)  ,
	@YearOfWork int ,
	@YearOfBudget int  ,
	@BudgetSource int  ,
	@IdChungTuDutoan nvarchar(max),
	@IdChungTuThuNop nvarchar(max),
	@DonViTinh int ,
	@VoucherType nvarchar(10)  

AS
BEGIN 

	declare @Id_DotNhanThu nvarchar(max),
	 @Id_DotNhanChi nvarchar(max);

		select @Id_DotNhanThu =   STUFF (
		(SELECT ',' + Id_DotNhan
			FROM TN_DT_ChungTu WHERE NamLamViec = @YearOfWork AND NguonNganSach = @BudgetSource AND NamNganSach = @YearOfBudget AND Id IN (select * from splitstring(@IdChungTuThuNop)) 
			FOR XML PATH(''),TYPE).value('.','NVARCHAR(MAX)'),1,1,'');
    
		select @Id_DotNhanChi =   STUFF (
		(SELECT ',' + iID_DotNhan
			FROM NS_DT_ChungTu WHERE iNamLamViec = @YearOfWork AND iID_MaNguonNganSach = @BudgetSource AND iNamNganSach = @YearOfBudget AND iID_DTChungTu IN (select * from splitstring(@IdChungTuDutoan))
			FOR XML PATH(''),TYPE).value('.','NVARCHAR(MAX)'),1,1,'');  

	--SELECT Id_DotNhan INTO #tmpDuToanThuNop FROM TN_DT_ChungTu WHERE NamLamViec = @YearOfWork AND NguonNganSach = @BudgetSource AND NamNganSach = @YearOfBudget AND Id IN (select * from splitstring(@IdChungTuThuNop))
	--SELECT iID_DotNhan INTO #tmpDuToanChi FROM NS_DT_ChungTu WHERE iNamLamViec = @YearOfWork AND iID_MaNguonNganSach = @BudgetSource AND iNamNganSach = @YearOfBudget AND iID_DTChungTu IN (select * from splitstring(@IdChungTuDutoan))
	IF (@VoucherType = '0')
		BEGIN
				SELECT t1.* INTO #t1 FROM
				(SELECT
				SUM(isnull(TuChi, 0)) / @DonViTinh as FTuChi,
				0 as FDuToanNamNay,
				0  as FThucThuNamTruoc,
				0 as FUocThucHienNamNay,
				NamLamViec,NamNganSach, NguonNganSach,Id_DonVi,XauNoiMa, iPhanCap ,bHangCha
				FROM
					TN_DT_ChungTuChiTiet
				WHERE
					NamLamViec = @YearOfWork
					AND NamNganSach = @YearOfBudget
					AND NguonNganSach = @BudgetSource
					AND( Id_DonVi in (select * from dbo.splitstring(@agencies)))
					AND (Id_ChungTu IN  (select * from dbo.splitstring(@IdChungTuThuNop)))
				GROUP BY NamLamViec,NamNganSach, NguonNganSach,Id_DonVi , XauNoiMa, iPhanCap,bHangCha

				UNION ALL --DATA DU TOAN
				SELECT 
					SUM(isnull(TuChi, 0)) / @DonViTinh as FTuChi,
					0 as FDuToanNamNay,
					0  as FThucThuNamTruoc,
					0 as FUocThucHienNamNay,
					NamLamViec,NamNganSach, NguonNganSach,Id_DonVi,XauNoiMa, iPhanCap ,bHangCha
				FROM 
				TN_DT_ChungTuChiTiet
				WHERE 
					NamLamViec = @YearOfWork
					AND NamNganSach = @YearOfBudget
					AND NguonNganSach = @BudgetSource
					AND (Id_ChungTu IN  (select * from splitstring(@Id_DotNhanThu)))
					AND iPhanCap = 0
				GROUP BY NamLamViec,NamNganSach, NguonNganSach,Id_DonVi , XauNoiMa, iPhanCap,bHangCha) as t1;


				SELECT t2.* INTO #t2 FROM
				(SELECT
				(SUM(isnull(fTuChi, 0)) + SUM(isnull(fHienVat, 0)) + SUM(isnull(fHangNhap, 0)) + SUM(isnull(fHangMua, 0))) / @DonViTinh as FTuChi,
				iNamLamViec NamLamViec  ,
				iNamNganSach NamNganSach,
				iID_MaNguonNganSach NguonNganSach,
				iID_MaDonVi,
				sXauNoiMa , 
				iPhanCap,
				bHangCha
				FROM
					NS_DT_ChungTuChiTiet
				WHERE
					iNamLamViec = @YearOfWork
					AND iNamNganSach = @YearOfBudget
					AND iID_MaNguonNganSach = @BudgetSource
					AND( iID_MaDonVi in (select * from dbo.splitstring(@agencies)))
					AND (iID_DTChungTu IN  (select * from dbo.splitstring(@IdChungTuDutoan)) )
				GROUP BY iNamLamViec,iNamNganSach, iID_MaNguonNganSach,iID_MaDonVi,sXauNoiMa , iPhanCap, bHangCha

				UNION ALL --DATA DU TOAN
				SELECT 
					(SUM(isnull(fTuChi, 0)) + SUM(isnull(fHienVat, 0)) + SUM(isnull(fHangNhap, 0)) + SUM(isnull(fHangMua, 0))) / @DonViTinh as FTuChi,
					iNamLamViec NamLamViec  ,
					iNamNganSach NamNganSach,
					iID_MaNguonNganSach NguonNganSach,
					iID_MaDonVi,
					sXauNoiMa , 
					iPhanCap,
					bHangCha
				FROM 
				NS_DT_ChungTuChiTiet
				WHERE 
					iNamLamViec = @YearOfWork
					AND iNamNganSach = @YearOfBudget
					AND iID_MaNguonNganSach = @BudgetSource
					AND (iID_DTChungTu IN  (select * from splitstring(@Id_DotNhanChi)))
					AND iPhanCap = 0
				GROUP BY iNamLamViec,iNamNganSach, iID_MaNguonNganSach,iID_MaDonVi,sXauNoiMa , iPhanCap,bHangCha) as t2 ;

				SELECT
				mlns.iID_MLNS as MlnsId,
				mlns.iID_MLNS_Cha as MlnsIdParent,
				mlns.sXauNoiMa as XauNoiMa,
				mlns.sLNS as LNS,
				mlns.sL as L,
				mlns.sK as K,
				mlns.sM as M,
				mlns.sTM as TM,
				mlns.sTTM as TTM,
				mlns.sNG as NG,
				mlns.sTNG as TNG,
				mlns.sTNG1 as TNG1,
				mlns.sTNG2 as TNG2,
				mlns.sTNG3 as TNG3,
				mlns.sDuToanChiTietToi ChiTietToi,
				isnull(mlns.sMoTa, '') as MoTa,
				CAST(isnull(ctct.NamNganSach, @YearOfBudget) as int) as INamNganSach,
				CAST(ctct.NguonNganSach AS int)as IIdMaNguonNganSach,
				CAST(ctct.NamLamViec AS int) INamLamViec,
				case WHEN ctct.bHangCha is null then mlns.bHangCha else ctct.bHangCha end bHangCha,
				mlns.sChiTietToi as ChiTietToi,
				FTuChi,
				cast(1 as bit) IsThuNop,
				iPhanCap IPhanCap,
				ctct.Id_DonVi iIdMaDonVi

			FROM (	
			SELECT * 
				FROM NS_MucLucNganSach) mlns
			LEFT JOIN #t1 ctct ON mlns.sXauNoiMa = ctct.XauNoiMa
			WHERE
				mlns.iNamLamViec = @YearOfWork
				AND mlns.iTrangThai = 1
				--AND mlns.LNS in (SELECT * FROM dbo.splitstring(@LNS))	
			UNION ALL 
			SELECT
				mlns.iID_MLNS as MlnsId,
				mlns.iID_MLNS_Cha as MlnsIdParent,
				mlns.sXauNoiMa as XauNoiMa,
				mlns.sLNS as LNS,
				mlns.sL as L,
				mlns.sK as K,
				mlns.sM as M,
				mlns.sTM as TM,
				mlns.sTTM as TTM,
				mlns.sNG as NG,
				mlns.sTNG as TNG,
				mlns.sTNG1 as TNG1,
				mlns.sTNG2 as TNG2,
				mlns.sTNG3 as TNG3,
				mlns.sDuToanChiTietToi ChiTietToi,
				isnull(mlns.sMoTa, '') as MoTa,
				CAST(isnull(ctct.NamNganSach, @YearOfBudget) as int) as INamNganSach,
				CAST(ctct.NguonNganSach AS int)as IIdMaNguonNganSach,
				CAST(ctct.NamLamViec AS int) INamLamViec,
				case WHEN ctct.bHangCha is null then mlns.bHangCha else ctct.bHangCha end bHangCha,
				mlns.sChiTietToi as ChiTietToi,
				FTuChi,
				cast(0 as bit) IsThuNop,
				iPhanCap IPhanCap,
				ctct.iID_MaDonVi iIdMaDonVi
			FROM (	
			SELECT * 
				FROM NS_MucLucNganSach) mlns
			LEFT JOIN #t2 ctct ON mlns.sXauNoiMa = ctct.sXauNoiMa
			WHERE
				mlns.iNamLamViec = @YearOfWork
				AND mlns.iTrangThai = 1
				--AND mlns.LNS in (SELECT * FROM dbo.splitstring(@LNS))
			ORDER BY mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG;
			DROP TABLE #t1,#t2;
		END
		ELSE IF(@VoucherType = '1')
		BEGIN
			SELECT t3.* INTO #t3 FROM (
			SELECT
				SUM(isnull(TuChi, 0)) / @DonViTinh as FTuChi,
				0 as FDuToanNamNay,
				0  as FThucThuNamTruoc,
				0 as FUocThucHienNamNay,
				NamLamViec,NamNganSach, NguonNganSach,Id_DonVi,XauNoiMa, iPhanCap ,bHangCha
				FROM
					TN_DT_ChungTuChiTiet
				WHERE
					NamLamViec = @YearOfWork
					AND NamNganSach = @YearOfBudget
					AND NguonNganSach = @BudgetSource
					AND( Id_DonVi in (select * from dbo.splitstring(@agencies)) )
					AND (Id_ChungTu IN  (select * from dbo.splitstring(@IdChungTuThuNop)))
					and XauNoiMa not like '104%'
				GROUP BY NamLamViec,NamNganSach, NguonNganSach,Id_DonVi , XauNoiMa, iPhanCap,bHangCha

				UNION ALL --DATA DU TOAN
				SELECT 
					SUM(isnull(TuChi, 0)) / @DonViTinh as FTuChi,
					0 as FDuToanNamNay,
					0  as FThucThuNamTruoc,
					0 as FUocThucHienNamNay,
					NamLamViec,NamNganSach, NguonNganSach,Id_DonVi,XauNoiMa, iPhanCap ,bHangCha
				FROM 
				TN_DT_ChungTuChiTiet
				WHERE 
					NamLamViec = @YearOfWork
					AND NamNganSach = @YearOfBudget
					AND NguonNganSach = @BudgetSource
					AND (Id_ChungTu IN (select * from splitstring(@Id_DotNhanThu)))
					and XauNoiMa not like '104%'
					AND iPhanCap = 0
				GROUP BY NamLamViec,NamNganSach, NguonNganSach,Id_DonVi , XauNoiMa, iPhanCap,bHangCha) as t3;

				SELECT t4.* INTO #t4 FROM (
				SELECT
				(SUM(isnull(fTuChi, 0)) + SUM(isnull(fHienVat, 0)) + SUM(isnull(fHangNhap, 0)) + SUM(isnull(fHangMua, 0))) / @DonViTinh as FTuChi,
				iNamLamViec NamLamViec  ,
				iNamNganSach NamNganSach,
				iID_MaNguonNganSach NguonNganSach,
				iID_MaDonVi,
				sXauNoiMa , 
				iPhanCap,bHangCha
				FROM
					NS_DT_ChungTuChiTiet
				WHERE
					iNamLamViec = @YearOfWork
					AND iNamNganSach = @YearOfBudget
					AND iID_MaNguonNganSach = @BudgetSource
					AND( iID_MaDonVi in (select * from dbo.splitstring(@agencies)))
					AND (iID_DTChungTu IN  (select * from dbo.splitstring(@IdChungTuDutoan)) )
					and sXauNoiMa not like '104%'
				GROUP BY iNamLamViec,iNamNganSach, iID_MaNguonNganSach,iID_MaDonVi,sXauNoiMa , iPhanCap,bHangCha

				UNION ALL --DATA DU TOAN
				SELECT 
					(SUM(isnull(fTuChi, 0)) + SUM(isnull(fHienVat, 0)) + SUM(isnull(fHangNhap, 0)) + SUM(isnull(fHangMua, 0))) / @DonViTinh as FTuChi,
					iNamLamViec NamLamViec  ,
					iNamNganSach NamNganSach,
					iID_MaNguonNganSach NguonNganSach,
					iID_MaDonVi,
					sXauNoiMa , 
					iPhanCap, bHangCha
				FROM 
				NS_DT_ChungTuChiTiet
				WHERE 
					iNamLamViec = @YearOfWork
					AND iNamNganSach = @YearOfBudget
					AND iID_MaNguonNganSach = @BudgetSource
					AND (iID_DTChungTu IN  (select * from splitstring(@Id_DotNhanChi)))
					and sXauNoiMa not like '104%'
					AND iPhanCap = 0
				GROUP BY iNamLamViec,iNamNganSach, iID_MaNguonNganSach,iID_MaDonVi,sXauNoiMa , iPhanCap, bHangCha ) t4;

			SELECT
			mlns.iID_MLNS as MlnsId,
			mlns.iID_MLNS_Cha as MlnsIdParent,
			mlns.sXauNoiMa as XauNoiMa,
			mlns.sLNS as LNS,
			mlns.sL as L,
			mlns.sK as K,
			mlns.sM as M,
			mlns.sTM as TM,
			mlns.sTTM as TTM,
			mlns.sNG as NG,
			mlns.sTNG as TNG,
			mlns.sTNG1 as TNG1,
			mlns.sTNG2 as TNG2,
			mlns.sTNG3 as TNG3,
			mlns.sDuToanChiTietToi ChiTietToi,
			isnull(mlns.sMoTa, '') as MoTa,
			CAST(isnull(ctct.NamNganSach, @YearOfBudget) as int) as INamNganSach,
			CAST(ctct.NguonNganSach AS int)as IIdMaNguonNganSach,
			CAST(ctct.NamLamViec AS int) INamLamViec,
			case WHEN ctct.bHangCha is null then mlns.bHangCha else ctct.bHangCha end bHangCha,
			mlns.sChiTietToi as ChiTietToi,
			FTuChi,
			cast(1 as bit) IsThuNop,
			iPhanCap IPhanCap,
			ctct.Id_DonVi iIdMaDonVi

			FROM (	
			SELECT * 
				FROM NS_MucLucNganSach) mlns
			LEFT JOIN #t3 ctct ON mlns.sXauNoiMa = ctct.XauNoiMa
			WHERE
				mlns.iNamLamViec = @YearOfWork
				AND mlns.iTrangThai = 1
				--AND mlns.LNS in (SELECT * FROM dbo.splitstring(@LNS))	
			UNION ALL 
			SELECT
				mlns.iID_MLNS as MlnsId,
				mlns.iID_MLNS_Cha as MlnsIdParent,
				mlns.sXauNoiMa as XauNoiMa,
				mlns.sLNS as LNS,
				mlns.sL as L,
				mlns.sK as K,
				mlns.sM as M,
				mlns.sTM as TM,
				mlns.sTTM as TTM,
				mlns.sNG as NG,
				mlns.sTNG as TNG,
				mlns.sTNG1 as TNG1,
				mlns.sTNG2 as TNG2,
				mlns.sTNG3 as TNG3,
				mlns.sDuToanChiTietToi ChiTietToi,
				isnull(mlns.sMoTa, '') as MoTa,
				CAST(isnull(ctct.NamNganSach, @YearOfBudget) as int) as INamNganSach,
				CAST(ctct.NguonNganSach AS int)as IIdMaNguonNganSach,
				CAST(ctct.NamLamViec AS int) INamLamViec,
				case WHEN ctct.bHangCha is null then mlns.bHangCha else ctct.bHangCha end bHangCha,
				mlns.sChiTietToi as ChiTietToi,
				FTuChi,
				cast(0 as bit) IsThuNop,
				iPhanCap IPhanCap,
				ctct.iID_MaDonVi iIdMaDonVi
			FROM (	
			SELECT * 
				FROM NS_MucLucNganSach) mlns
			LEFT JOIN #t4 ctct ON mlns.sXauNoiMa = ctct.sXauNoiMa
			WHERE
				mlns.iNamLamViec = @YearOfWork
				AND mlns.iTrangThai = 1
				--AND mlns.LNS in (SELECT * FROM dbo.splitstring(@LNS))
			ORDER BY mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG;

			DROP TABLE #t3,#t4;

		END
	ELSE
		BEGIN	
			SELECT t5.* INTO #t5 FROM (
				SELECT
				SUM(isnull(TuChi, 0)) / @DonViTinh as FTuChi,
				0 as FDuToanNamNay,
				0  as FThucThuNamTruoc,
				0 as FUocThucHienNamNay,
				NamLamViec,NamNganSach, NguonNganSach,Id_DonVi,XauNoiMa, iPhanCap ,bHangCha
				FROM
					TN_DT_ChungTuChiTiet
				WHERE
					NamLamViec = @YearOfWork
					AND NamNganSach = @YearOfBudget
					AND NguonNganSach = @BudgetSource
					AND (Id_DonVi in (select * from dbo.splitstring(@agencies)))
					AND (Id_ChungTu IN  (select * from dbo.splitstring(@IdChungTuThuNop)))
					and XauNoiMa  like '104%'
				GROUP BY NamLamViec,NamNganSach, NguonNganSach,Id_DonVi , XauNoiMa, iPhanCap,bHangCha

				UNION ALL --DATA DU TOAN
				SELECT 
					SUM(isnull(TuChi, 0)) / @DonViTinh as FTuChi,
					0 as FDuToanNamNay,
					0  as FThucThuNamTruoc,
					0 as FUocThucHienNamNay,
					NamLamViec,NamNganSach, NguonNganSach,Id_DonVi,XauNoiMa, iPhanCap, bHangCha 
				FROM 
				TN_DT_ChungTuChiTiet
				WHERE 
					NamLamViec = @YearOfWork
					AND NamNganSach = @YearOfBudget
					AND NguonNganSach = @BudgetSource
					AND (Id_ChungTu IN  (select * from splitstring(@Id_DotNhanThu)))
					and XauNoiMa  like '104%'
					AND iPhanCap = 0
				GROUP BY NamLamViec,NamNganSach, NguonNganSach,Id_DonVi , XauNoiMa, iPhanCap,bHangCha ) as t5;

				SELECT t6.* INTO #t6 FROM (
				SELECT
				(SUM(isnull(fTuChi, 0)) + SUM(isnull(fHienVat, 0)) + SUM(isnull(fHangNhap, 0)) + SUM(isnull(fHangMua, 0))) / @DonViTinh as FTuChi,
				iNamLamViec NamLamViec  ,
				iNamNganSach NamNganSach,
				iID_MaNguonNganSach NguonNganSach,
				iID_MaDonVi,
				sXauNoiMa , 
				iPhanCap,
				bHangCha
				FROM
					NS_DT_ChungTuChiTiet
				WHERE
					iNamLamViec = @YearOfWork
					AND iNamNganSach = @YearOfBudget
					AND iID_MaNguonNganSach = @BudgetSource
					AND ( iID_MaDonVi in (select * from dbo.splitstring(@agencies)))
					AND (iID_DTChungTu IN  (select * from dbo.splitstring(@IdChungTuDutoan)))
					and sXauNoiMa  like '104%'
				GROUP BY iNamLamViec,iNamNganSach, iID_MaNguonNganSach,iID_MaDonVi,sXauNoiMa , iPhanCap,bHangCha

				UNION ALL --DATA DU TOAN
				SELECT 
					(SUM(isnull(fTuChi, 0)) + SUM(isnull(fHienVat, 0)) + SUM(isnull(fHangNhap, 0)) + SUM(isnull(fHangMua, 0))) / @DonViTinh as FTuChi,
					iNamLamViec NamLamViec  ,
					iNamNganSach NamNganSach,
					iID_MaNguonNganSach NguonNganSach,
					iID_MaDonVi,
					sXauNoiMa , 
					iPhanCap,
					bHangCha
				FROM 
				NS_DT_ChungTuChiTiet
				WHERE 
					iNamLamViec = @YearOfWork
					AND iNamNganSach = @YearOfBudget
					AND iID_MaNguonNganSach = @BudgetSource
					AND (iID_DTChungTu IN  (select * from splitstring(@Id_DotNhanChi)))
					and sXauNoiMa  like '104%'
					AND iPhanCap = 0
				GROUP BY iNamLamViec,iNamNganSach, iID_MaNguonNganSach,iID_MaDonVi,sXauNoiMa , iPhanCap, bHangCha) as t6 ;
		 SELECT
			mlns.iID_MLNS as MlnsId,
			mlns.iID_MLNS_Cha as MlnsIdParent,
			mlns.sXauNoiMa as XauNoiMa,
			mlns.sLNS as LNS,
			mlns.sL as L,
			mlns.sK as K,
			mlns.sM as M,
			mlns.sTM as TM,
			mlns.sTTM as TTM,
			mlns.sNG as NG,
			mlns.sTNG as TNG,
			mlns.sTNG1 as TNG1,
			mlns.sTNG2 as TNG2,
			mlns.sTNG3 as TNG3,
			mlns.sDuToanChiTietToi ChiTietToi,
			isnull(mlns.sMoTa, '') as MoTa,
			CAST(isnull(ctct.NamNganSach, @YearOfBudget) as int) as INamNganSach,
			CAST(ctct.NguonNganSach AS int)as IIdMaNguonNganSach,
			CAST(ctct.NamLamViec AS int) INamLamViec,
			case WHEN ctct.bHangCha is null then mlns.bHangCha else ctct.bHangCha end bHangCha,
			mlns.sChiTietToi as ChiTietToi,
			FTuChi,
			cast(1 as bit) IsThuNop,
			iPhanCap IPhanCap,
			ctct.Id_DonVi iIdMaDonVi

			FROM (	
			SELECT * 
				FROM NS_MucLucNganSach) mlns
			LEFT JOIN #t5 ctct ON mlns.sXauNoiMa = ctct.XauNoiMa
			WHERE
				mlns.iNamLamViec = @YearOfWork
				AND mlns.iTrangThai = 1
				--AND mlns.LNS in (SELECT * FROM dbo.splitstring(@LNS))	
			UNION ALL 
			SELECT
				mlns.iID_MLNS as MlnsId,
				mlns.iID_MLNS_Cha as MlnsIdParent,
				mlns.sXauNoiMa as XauNoiMa,
				mlns.sLNS as LNS,
				mlns.sL as L,
				mlns.sK as K,
				mlns.sM as M,
				mlns.sTM as TM,
				mlns.sTTM as TTM,
				mlns.sNG as NG,
				mlns.sTNG as TNG,
				mlns.sTNG1 as TNG1,
				mlns.sTNG2 as TNG2,
				mlns.sTNG3 as TNG3,
				mlns.sDuToanChiTietToi ChiTietToi,
				isnull(mlns.sMoTa, '') as MoTa,
				CAST(isnull(ctct.NamNganSach, @YearOfBudget) as int) as INamNganSach,
				CAST(ctct.NguonNganSach AS int)as IIdMaNguonNganSach,
				CAST(ctct.NamLamViec AS int) INamLamViec,
				case WHEN ctct.bHangCha is null then mlns.bHangCha else ctct.bHangCha end bHangCha,
				mlns.sChiTietToi as ChiTietToi,
				FTuChi,
				cast(0 as bit) IsThuNop,
				iPhanCap IPhanCap,
				ctct.iID_MaDonVi iIdMaDonVi
			FROM (	
			SELECT * 
				FROM NS_MucLucNganSach) mlns
			LEFT JOIN #t6 ctct ON mlns.sXauNoiMa = ctct.sXauNoiMa
			WHERE
				mlns.iNamLamViec = @YearOfWork
				AND
			mlns.itrangthai = 1
				--AND mlns.LNS in (SELECT * FROM dbo.splitstring(@LNS))
			order
			by
			    mlns.slns, mlns.sl, mlns.sk, mlns.sm, mlns.stm, mlns.sttm, mlns.sng, mlns.stng;

				DROP TABLE #t5,#t6;
    END

	--DROP TABLE #tmpDuToanChi;
	--DROP TABLE #tmpDuToanThuNop;
END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_inchitieu_donvi]    Script Date: 12/30/2024 8:40:48 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dutoan_rpt_inchitieu_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dutoan_rpt_inchitieu_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_inchitieu_donvi]    Script Date: 12/30/2024 8:40:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dutoan_rpt_inchitieu_donvi]				
	 @NamLamViec int,							
	 @NguonNganSach nvarchar(50),
	 @NamNganSach nvarchar(50),
	 @IdDonvi nvarchar(2000),
	 @IdChungTu nvarchar(4000),
	 @NgayQuyetDinh datetime,
	 @Dvt int,
	 @IsLuyKe int
AS	 
BEGIN 
	SET NOCOUNT ON;
	 

	 --GET MLNS
	 SELECT        
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
       mlns.sXauNoiMa AS XauNoiMa,
       mlns.sMoTa AS MoTa ,
	   mlns.iID_MLNS as MlnsId,
	   mlns.iID_MLNS_Cha as MlnsIdParent
	   INTO #tmpMlns
	 FROM NS_MucLucNganSach mlns WHERE iNamLamViec= @NamLamViec;
	 CREATE INDEX IX_t1
	 ON #tmpMlns (MlnsId,XauNoiMa)
SELECT 
	   LNS1 = Left(mlns.LNS, 1),
       LNS3 = Left(mlns.LNS, 3),
       LNS5 = Left(mlns.LNS, 5),
       mlns.LNS ,
       mlns.L,
       mlns.K,
       mlns.M,
       mlns.TM,
       mlns.TTM,
       mlns.NG,
       mlns.TNG,
	   mlns.TNG1,
	   mlns.TNG2 ,
	   mlns.TNG3,
       mlns.XauNoiMa,
       mlns.MoTa  ,
	   ct.iID_MaDonVi as MaDonVi,
	   '' as TenDonVi,
	   mlns.MlnsId,
	   mlns.MlnsIdParent,
       TuChi = ROUND(sum(fTuChi)/@Dvt,0) ,
       HienVat = ROUND(sum(fHienVat)/@Dvt,0),
	   DuPhong = ROUND(sum(fDuPhong)/@Dvt,0),
	   HangNhap = ROUND(sum(fHangNhap)/@Dvt,0),
	   HangMua = ROUND(sum(fHangMua)/@Dvt,0),
	   PhanCap = ROUND(sum(fPhanCap)/@Dvt,0),
	   RutKBNN = ROUND(sum(fRutKBNN)/@Dvt,0)
	FROM NS_DT_ChungTuChiTiet ct
		INNER JOIN #tmpMlns mlns ON ct.sXauNoiMa = mlns.XauNoiMa
		
		--AND mlns.iNamLamViec = @NamLamViec
	WHERE (@IdDonvi IS NULL
       OR ct.iID_MaDonVi in
         (SELECT *
          FROM f_split(@IdDonvi)))
		  AND (iID_DTChungTu in (select * from f_split(@IdChungTu)))
GROUP BY mlns.LNS,
         mlns.L,
         mlns.K,
         mlns.M,
         mlns.TM,
         mlns.TTM,
         mlns.NG,
         mlns.TNG,
		 mlns.TNG1,
	     mlns.TNG2,
	     mlns.TNG3,
         mlns.XauNoiMa,
         mlns.MoTa,
		 mlns.MlnsId,
		 mlns.MlnsIdParent,
		 ct.iID_MaDonVi
HAVING sum(fTuChi) <> 0
OR sum(fHienVat) <> 0
OR sum(fDuPhong) <> 0
OR sum(fHangNhap) <> 0
OR sum(fHangMua) <> 0
OR sum(fPhanCap) <> 0
OR sum(fRutKBNN) <> 0;
DROP TABLE #tmpMlns;
END
;
;
;
;
;
GO
