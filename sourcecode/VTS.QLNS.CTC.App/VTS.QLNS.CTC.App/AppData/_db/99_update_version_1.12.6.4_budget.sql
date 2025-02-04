/****** Object:  StoredProcedure [dbo].[sp_qs_tao_chitiet]    Script Date: 28/02/2023 1:50:34 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_tao_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_tao_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_tao_chitiet]    Script Date: 28/02/2023 1:50:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qs_tao_chitiet]
	@IdChungTu nvarchar(100),
	@YearOfWork int,
	@Thang int,
	@User nvarchar(100)
AS
BEGIN

	select iID_MaDonVi into #dv 
	from DonVi 
	where iLoai = 1
	and iNamLamViec = @YearOfWork

	IF EXISTS (SELECT * FROM #dv) AND @Thang <> 0
		BEGIN
			select * into #qs1 
			from NS_QS_MucLuc 
			where sKyHieu in ('100', '700') and iNamLamViec = @YearOfWork

			select * into #data1 from #qs1, #dv
			insert into NS_QS_ChungTuChiTiet
				(iID_QSChungTu, iID_MLNS, iID_MLNS_Cha, sKyHieu, sMoTa, bHangCha, iThangQuy, iID_MaDonVi, iNamLamViec, 
				fSoThieuUy, fSoTrungUy, fSoThuongUy, fSoDaiUy, fSoThieuTa, fSoTrungTa, fSoThuongTa, fSoDaiTa, fSoTuong,
				fSoTSQ, fSoBinhNhi, fSoBinhNhat, fSoHaSi, fSoTrungSi, fSoThuongSi, fSoQNCN, fSoCNVQP, fSoLDHD, 
				fSoCNVQPCT, fSoQNVQPHD, fTongSo, fSoSQ_KH, fSoHSQBS_KH, fSoCNVQP_KH, fSoLDHD_KH, fSoQNCN_KH, fSoVCQP, 
				fSoCY_H, fSoCY_KT, sGhiChu, dNgayTao, sNguoiTao, dNgaySua, sNgaySua)

				select @IdChungTu, iID_MLNS, iID_MLNS_Cha, sKyHieu, sMoTa, bHangCha, @Thang, iID_MaDonVi, @YearOfWork, 
					0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 
					0, 0, null, getdate(), @User, null, null
				from #data1
			drop table #qs1, #dv, #data1
		END

	ELSE IF EXISTS (SELECT * FROM #dv) AND @Thang = 0
		BEGIN
			select * into #qs2 
			from NS_QS_MucLuc 
			where iNamLamViec = @YearOfWork
			select * into #data2 from #qs2, #dv

			insert into NS_QS_ChungTuChiTiet
				(iID_QSChungTu, iID_MLNS, iID_MLNS_Cha, sKyHieu, sMoTa, bHangCha, iThangQuy, iID_MaDonVi, iNamLamViec, 
				fSoThieuUy, fSoTrungUy, fSoThuongUy, fSoDaiUy, fSoThieuTa, fSoTrungTa, fSoThuongTa, fSoDaiTa, fSoTuong,
				fSoTSQ, fSoBinhNhi, fSoBinhNhat, fSoHaSi, fSoTrungSi, fSoThuongSi, fSoQNCN, fSoCNVQP, fSoLDHD, 
				fSoCNVQPCT, fSoQNVQPHD, fTongSo, fSoSQ_KH, fSoHSQBS_KH, fSoCNVQP_KH, fSoLDHD_KH, fSoQNCN_KH, fSoVCQP, 
				fSoCY_H, fSoCY_KT, sGhiChu, dNgayTao, sNguoiTao, dNgaySua, sNgaySua)

				select @IdChungTu, iID_MLNS, iID_MLNS_Cha, sKyHieu, sMoTa, bHangCha, @Thang, iID_MaDonVi, @YearOfWork, 
					0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 
					0, 0, null, getdate(), @User, null, null
				from #data2
			drop table #qs2, #dv, #data2
		END
	ELSE IF @Thang <> 0
		BEGIN
			select iID_MaDonVi into #dv2 
			from DonVi 
			where iLoai = 0
			and iNamLamViec = @YearOfWork

			select * into #qs3 
			from NS_QS_MucLuc 
			where sKyHieu in ('100', '700') and iNamLamViec = @YearOfWork

			select * into #data3 from #qs3, #dv2
			insert into NS_QS_ChungTuChiTiet
				(iID_QSChungTu, iID_MLNS, iID_MLNS_Cha, sKyHieu, sMoTa, bHangCha, iThangQuy, iID_MaDonVi, iNamLamViec, 
				fSoThieuUy, fSoTrungUy, fSoThuongUy, fSoDaiUy, fSoThieuTa, fSoTrungTa, fSoThuongTa, fSoDaiTa, fSoTuong,
				fSoTSQ, fSoBinhNhi, fSoBinhNhat, fSoHaSi, fSoTrungSi, fSoThuongSi, fSoQNCN, fSoCNVQP, fSoLDHD, 
				fSoCNVQPCT, fSoQNVQPHD, fTongSo, fSoSQ_KH, fSoHSQBS_KH, fSoCNVQP_KH, fSoLDHD_KH, fSoQNCN_KH, fSoVCQP, 
				fSoCY_H, fSoCY_KT, sGhiChu, dNgayTao, sNguoiTao, dNgaySua, sNgaySua)

				select @IdChungTu, iID_MLNS, iID_MLNS_Cha, sKyHieu, sMoTa, bHangCha, @Thang, iID_MaDonVi, @YearOfWork, 
					0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 
					0, 0, null, getdate(), @User, null, null
				from #data3
			drop table #qs3, #dv2, #data3
		END
	ELSE 
		BEGIN
			select iID_MaDonVi into #dv4 
			from DonVi 
			where iLoai = 0
			and iNamLamViec = @YearOfWork
		
			select * into #qs4 
			from NS_QS_MucLuc 
			where iNamLamViec = @YearOfWork

			select * into #data4 from #qs4, #dv4
			insert into NS_QS_ChungTuChiTiet
				(iID_QSChungTu, iID_MLNS, iID_MLNS_Cha, sKyHieu, sMoTa, bHangCha, iThangQuy, iID_MaDonVi, iNamLamViec, 
				fSoThieuUy, fSoTrungUy, fSoThuongUy, fSoDaiUy, fSoThieuTa, fSoTrungTa, fSoThuongTa, fSoDaiTa, fSoTuong,
				fSoTSQ, fSoBinhNhi, fSoBinhNhat, fSoHaSi, fSoTrungSi, fSoThuongSi, fSoQNCN, fSoCNVQP, fSoLDHD, 
				fSoCNVQPCT, fSoQNVQPHD, fTongSo, fSoSQ_KH, fSoHSQBS_KH, fSoCNVQP_KH, fSoLDHD_KH, fSoQNCN_KH, fSoVCQP, 
				fSoCY_H, fSoCY_KT, sGhiChu, dNgayTao, sNguoiTao, dNgaySua, sNgaySua)

				select @IdChungTu, iID_MLNS, iID_MLNS_Cha, sKyHieu, sMoTa, bHangCha, @Thang, iID_MaDonVi, @YearOfWork, 
					0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 
					0, 0, null, getdate(), @User, null, null
				from #data4
			drop table #qs4, #dv4, #data4
		END

	
END
;
;
;
;
GO
