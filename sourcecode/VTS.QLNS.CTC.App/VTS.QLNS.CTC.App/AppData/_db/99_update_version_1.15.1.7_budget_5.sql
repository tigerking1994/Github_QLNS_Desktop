/****** Object:  StoredProcedure [dbo].[sp_rpt_tn_dutoan_hd4554]    Script Date: 11/26/2024 5:37:36 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_tn_dutoan_hd4554]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_tn_dutoan_hd4554]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_tn_dutoan_hd4554]    Script Date: 11/26/2024 5:37:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_tn_dutoan_hd4554]
	@agencies nvarchar(max)  ,
	@YearOfWork int ,
	@YearOfBudget int  ,
	@BudgetSource int ,
	@IdChungTuDutoan nvarchar(max),
	@IdChungTuThuNop nvarchar(max),
	@DonViTinh int,
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
			LEFT JOIN (
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
				GROUP BY NamLamViec,NamNganSach, NguonNganSach,Id_DonVi , XauNoiMa, iPhanCap,bHangCha
			) ctct ON mlns.sXauNoiMa = ctct.XauNoiMa
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
			LEFT JOIN (
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
				GROUP BY iNamLamViec,iNamNganSach, iID_MaNguonNganSach,iID_MaDonVi,sXauNoiMa , iPhanCap,bHangCha
			) ctct ON mlns.sXauNoiMa = ctct.sXauNoiMa
			WHERE
				mlns.iNamLamViec = @YearOfWork
				AND mlns.iTrangThai = 1
				--AND mlns.LNS in (SELECT * FROM dbo.splitstring(@LNS))
			ORDER BY mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG;
		END
		ELSE IF(@VoucherType = '1')
		BEGIN
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
			LEFT JOIN (
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
				GROUP BY NamLamViec,NamNganSach, NguonNganSach,Id_DonVi , XauNoiMa, iPhanCap,bHangCha
			) ctct ON mlns.sXauNoiMa = ctct.XauNoiMa
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
			LEFT JOIN (
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
				GROUP BY iNamLamViec,iNamNganSach, iID_MaNguonNganSach,iID_MaDonVi,sXauNoiMa , iPhanCap, bHangCha
			) ctct ON mlns.sXauNoiMa = ctct.sXauNoiMa
			WHERE
				mlns.iNamLamViec = @YearOfWork
				AND mlns.iTrangThai = 1
				--AND mlns.LNS in (SELECT * FROM dbo.splitstring(@LNS))
			ORDER BY mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG;
		END
	ELSE
		BEGIN					
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
			LEFT JOIN (
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
				GROUP BY NamLamViec,NamNganSach, NguonNganSach,Id_DonVi , XauNoiMa, iPhanCap,bHangCha
			) ctct ON mlns.sXauNoiMa = ctct.XauNoiMa
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
			LEFT JOIN (
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
				GROUP BY iNamLamViec,iNamNganSach, iID_MaNguonNganSach,iID_MaDonVi,sXauNoiMa , iPhanCap, bHangCha
			) ctct ON mlns.sXauNoiMa = ctct.sXauNoiMa
			WHERE
				mlns.iNamLamViec = @YearOfWork
				AND
			mlns.itrangthai = 1
				--AND mlns.LNS in (SELECT * FROM dbo.splitstring(@LNS))
			order
			by
			    mlns.slns, mlns.sl, mlns.sk, mlns.sm, mlns.stm, mlns.sttm, mlns.sng, mlns.stng;
    END

	--DROP TABLE #tmpDuToanChi;
	--DROP TABLE #tmpDuToanThuNop;
END
;
;
;
;
;
GO
