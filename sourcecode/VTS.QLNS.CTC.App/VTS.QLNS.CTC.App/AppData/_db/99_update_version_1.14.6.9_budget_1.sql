/****** Object:  StoredProcedure [dbo].[sp_skt_get_can_cu_phan_bo_so_kiem_tra]    Script Date: 7/25/2024 3:12:40 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_get_can_cu_phan_bo_so_kiem_tra]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_get_can_cu_phan_bo_so_kiem_tra]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_get_can_cu_phan_bo_so_kiem_tra]    Script Date: 7/25/2024 3:12:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_get_can_cu_phan_bo_so_kiem_tra]
	@LoaiChungTu int,
	@IdDonVi varchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@MaNguoiNganSach int,
	@IsParent bit
AS
BEGIN
	-- Chứng từ cha
	IF (@IsParent = 1)
	BEGIN
		SELECT mlns.SXauNoiMa,
			   mlns.iID_MLNS IdMlns,
			   mlns.iID_MLNS_Cha IdMlnsCha,
			   mlns.bHangCha,
			   sum(isnull(TuChi,0)) TuChi,
			   sum(isnull(HangNhap,0)) HangNhap,
			   sum(isnull(HangMua,0)) HangMua,
			   sum(isnull(PhanCap,0)) PhanCap,
			   sum(isnull(TuChi,0)) MuaHangHienVat,
			   sum(isnull(TuChi,0)) DacThu
		FROM NS_MucLucNganSach mlns
		 JOIN
		  (SELECT ctct.SXauNoiMa,
				  ctct.iID_MLNS,
				  ctct.iID_MLNS_Cha,
				  ctct.bHangCha,
				  sum(fTuChi) TuChi,
				  sum(fHangNhap) HangNhap,
				  sum(fHangMua) HangMua,
				  sum(fPhanCap) PhanCap,
				  sum(fTuChi) MuaHangHienVat,
				  sum(fTuChi) DacThu
		   FROM NS_DT_ChungTuChiTiet ctct
		   JOIN NS_DT_ChungTu ct ON ct.iID_DTChungTu = ctct.iID_DTChungTu
		   WHERE ct.iNamLamViec = @NamLamViec
			 AND ct.iNamNganSach = @NamNganSach
			 AND ct.iID_MaNguonNganSach = @MaNguoiNganSach
			 AND (@LoaiChungTu = 1
				  OR ctct.sLNS in ('1040100',
								   '1040200',
								   '1040300'))
			 AND ct.bKhoa = 1
			 AND ct.iLoai = 1
			 AND ct.iLoaiDuToan = 1
		   GROUP BY ctct.SXauNoiMa,
					ctct.iID_MLNS,
					ctct.iID_MLNS_Cha,
					ctct.bHangCha) ctct ON mlns.iID_MLNS = ctct.iID_MLNS AND mlns.iNamLamViec = @NamLamViec
		GROUP BY mlns.SXauNoiMa,
				 mlns.iID_MLNS,
				 mlns.iID_MLNS_Cha,
				 mlns.bHangCha;
	END
	ELSE
		BEGIN
			SELECT mlns.SXauNoiMa,
			   mlns.iID_MLNS IdMlns,
			   mlns.iID_MLNS_Cha IdMlnsCha,
			   mlns.bHangCha,
			   sum(isnull(TuChi,0)) TuChi,
			   sum(isnull(HangNhap,0)) HangNhap,
			   sum(isnull(HangMua,0)) HangMua,
			   sum(isnull(PhanCap,0)) PhanCap,
			   sum(isnull(TuChi,0)) MuaHangHienVat,
			   sum(isnull(TuChi,0)) DacThu
			FROM NS_MucLucNganSach mlns
			 JOIN
			  (SELECT ctct.SXauNoiMa,
					  ctct.iID_MLNS,
					  ctct.iID_MLNS_Cha,
					  ctct.bHangCha,
					  sum(fTuChi) TuChi,
					  sum(fHangNhap) HangNhap,
					  sum(fHangMua) HangMua,
					  sum(fPhanCap) PhanCap,
					  sum(fTuChi) MuaHangHienVat,
					  sum(fTuChi) DacThu
			   FROM NS_DT_ChungTuChiTiet ctct
			   JOIN NS_DT_ChungTu ct ON ct.iID_DTChungTu = ctct.iID_DTChungTu
			   WHERE ct.iNamLamViec = @NamLamViec
				 AND ct.iNamNganSach = @NamNganSach
				 AND ct.iID_MaNguonNganSach = @MaNguoiNganSach
				 AND (@LoaiChungTu = 1
					  OR ctct.sLNS in ('1040100',
									   '1040200',
									   '1040300'))
				 AND ct.bKhoa = 1
				 AND ct.iLoai = 1
				 AND ct.iLoaiDuToan = 1
				 AND ctct.iID_MaDonVi = @IdDonVi
			   GROUP BY ctct.SXauNoiMa,
						ctct.iID_MLNS,
						ctct.iID_MLNS_Cha,
						ctct.bHangCha) ctct ON mlns.iID_MLNS = ctct.iID_MLNS AND mlns.iNamLamViec = @NamLamViec
			GROUP BY mlns.SXauNoiMa,
					 mlns.iID_MLNS,
					 mlns.iID_MLNS_Cha,
					 mlns.bHangCha;
		END
END
;
;
GO
