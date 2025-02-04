/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_danhsachquyettoanquyKCB_index]    Script Date: 5/16/2024 10:06:45 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_danhsachquyettoanquyKCB_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_danhsachquyettoanquyKCB_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_danhsachquyettoanquy_index]    Script Date: 5/16/2024 10:06:45 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_danhsachquyettoanquy_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_danhsachquyettoanquy_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_danhsachquyettoannamKCB_index]    Script Date: 5/16/2024 10:06:45 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_danhsachquyettoannamKCB_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_danhsachquyettoannamKCB_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_danhsachquyettoannam_index]    Script Date: 5/16/2024 10:06:45 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_danhsachquyettoannam_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_danhsachquyettoannam_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_export_kehoachcaptamung_kcbbhyt]    Script Date: 5/16/2024 10:52:54 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_export_kehoachcaptamung_kcbbhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_export_kehoachcaptamung_kcbbhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_danhsachquyettoannam_index]    Script Date: 5/16/2024 10:06:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_danhsachquyettoannam_index]
@INamLamViec int
As
Begin
	select 
		qtn.ID_QTC_Nam_CheDoBHXH,
		qtn.iID_DonVi,
		qtn.iID_MaDonVi,
		qtn.sSoChungTu,
		qtn.dNgayChungTu,
		qtn.bThucChiTheo4Quy,
		qtn.iNamLamViec,
		qtn.sSoQuyetDinh,
		qtn.dNgayQuyetDinh,
		qtn.sMoTa,
		qtn.bIsKhoa,
		qtn.iLoaiTongHop,
		qtn.sTongHop,
		qtn.fTongTien_DuToanDuyet,
		qtn.iTongSo_LuyKeCuoiQuyNay,
		qtn.fTongTien_LuyKeCuoiQuyNay,
		qtn.iTongSoSQ_DeNghi,
		qtn.fTongTienSQ_DeNghi,
		qtn.iTongSoQNCN_DeNghi,
		qtn.fTongTienQNCN_DeNghi,
		qtn.iTongSoCNVCQP_DeNghi,
		qtn.fTongTienCNVCQP_DeNghi,
		qtn.iTongSoHSQBS_DeNghi,
		qtn.fTongTienHSQBS_DeNghi,
		qtn.iTongSo_DeNghi,
		qtn.fTongTien_DeNghi,
		qtn.fTongTien_PheDuyet,
		Case when isnull(qtn.fTongTien_DuToanDuyet,0) > isnull(qtn.fTongTien_DeNghi,0) then isnull(qtn.fTongTien_DuToanDuyet,0) - isnull(qtn.fTongTien_DeNghi,0)  ELSE  0 end fTongTienThua,
		--isnull(qtn.fTongTien_DuToanDuyet,0) - isnull(qtn.fTongTien_DeNghi,0) as fTongTienThua,
		Case when isnull(qtn.fTongTien_DeNghi,0) > isnull(qtn.fTongTien_DuToanDuyet,0) then isnull(qtn.fTongTien_DeNghi,0) - isnull(qtn.fTongTien_DuToanDuyet,0)  ELSE  0 end fTongTienThieu,
		--isnull(qtn.fTongTien_DeNghi,0) - isnull(qtn.fTongTien_DuToanDuyet,0) as fTongTienThieu,
		qtn.bDaTongHop,
		qtn.sDSSoChungTuTongHop,
		dv.sTenDonVi,
		qtn.sNguoiTao,
		qtn.sDSLNS
	from BH_QTC_Nam_CheDoBHXH as qtn
	left join DonVi as dv on qtn.iID_DonVi = dv.iID_DonVi
	where dv.iNamLamViec = @INamLamViec

End
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_danhsachquyettoannamKCB_index]    Script Date: 5/16/2024 10:06:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_danhsachquyettoannamKCB_index]
@INamLamViec int
As
Begin
	select 
		qtn.ID_QTC_Nam_KCB_QuanYDonVi,
		qtn.iID_DonVi,
		qtn.iID_MaDonVi,
		qtn.sSoChungTu,
		qtn.dNgayChungTu,
		qtn.bThucChiTheo4Quy,
		qtn.iNamLamViec,
		qtn.sSoQuyetDinh,
		qtn.dNgayQuyetDinh,
		qtn.sMoTa,
		qtn.bIsKhoa,
		qtn.iLoaiTongHop,
		qtn.sTongHop,
		qtn.fTongTien_DuToanNamTruocChuyenSang,
		qtn.fTongTien_DuToanGiaoNamNay,
		qtn.fTongTien_TongDuToanDuocGiao,
		qtn.fTongTien_ThucChi,
		qtn.fTongTienThua,
		qtn.fTongTienThieu,
		qtn.bDaTongHop,
		qtn.sDSSoChungTuTongHop,
		dv.sTenDonVi,
		qtn.sNguoiTao,
		qtn.sDSLNS
	from BH_QTC_Nam_KCB_QuanYDonVi as qtn
	left join DonVi as dv on qtn.iID_DonVi = dv.iID_DonVi
	where dv.iNamLamViec = @INamLamViec

	

End
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_danhsachquyettoanquy_index]    Script Date: 5/16/2024 10:06:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_danhsachquyettoanquy_index]
@INamLamViec int

As
Begin
	select 
		qtq.ID_QTC_Quy_CheDoBHXH,
		qtq.iID_DonVi,
		qtq.iID_MaDonVi,
		qtq.sSoChungTu,
		qtq.dNgayChungTu,
		qtq.sSoQuyetDinh,
		qtq.dNgayQuyetDinh,
		qtq.iQuyChungTu,
		qtq.iNamChungTu,
		qtq.sMoTa,
		qtq.dNgaySua,
		qtq.dNgayTao,
		qtq.sNguoiSua,
		qtq.sNguoiTao,
		qtq.sTongHop,
		qtq.iID_TongHopID,
		qtq.iLoaiTongHop,
		qtq.bIsKhoa,
		qtq.fTongTien_DuToanDuyet,
		qtq.iTongSo_LuyKeCuoiQuyNay,
		qtq.fTongTien_LuyKeCuoiQuyNay,
		qtq.iTongSoSQ_DeNghi,
		qtq.fTongTienSQ_DeNghi,
		qtq.iTongSoQNCN_DeNghi,
		qtq.fTongTienQNCN_DeNghi,
		qtq.iTongSoCNVCQP_DeNghi,
		qtq.fTongTienCNVCQP_DeNghi,
		qtq.iTongSoHSQBS_DeNghi,
		qtq.fTongTienHSQBS_DeNghi,
		qtq.iTongSo_DeNghi,
		qtq.fTongTien_DeNghi,
		qtq.fTongTien_PheDuyet,
		dv.sTenDonVi,
		qtq.bDaTongHop,
		qtq.sDSSoChungTuTongHop,
		qtq.sDSLNS
	from BH_QTC_Quy_CheDoBHXH as qtq
	left join DonVi as dv on qtq.iID_DonVi = dv.iID_DonVi
	where dv.iNamLamViec = @INamLamViec
End
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_danhsachquyettoanquyKCB_index]    Script Date: 5/16/2024 10:06:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_danhsachquyettoanquyKCB_index]
@INamLamViec int
As
Begin
	select 
		qtq.ID_QTC_Quy_KCB,
		qtq.iID_DonVi,
		qtq.iID_MaDonVi,
		qtq.sSoChungTu,
		qtq.dNgayChungTu,
		qtq.sSoQuyetDinh,
		qtq.dNgayQuyetDinh,
		qtq.iQuyChungTu,
		qtq.iNamChungTu,
		qtq.sMoTa,
		qtq.dNgaySua,
		qtq.dNgayTao,
		qtq.sNguoiSua,
		qtq.sNguoiTao,
		qtq.sTongHop,
		qtq.iID_TongHopID,
		qtq.iLoaiTongHop,
		qtq.bIsKhoa,
		qtq.fTongTien_DuToanNamTruocChuyenSang,
		qtq.fTongTien_DuToanGiaoNamNay,
		qtq.fTongTien_TongDuToanDuocGiao,
		qtq.fTongTienThucChi,
		qtq.fTongTienQuyetToanDaDuyet,
		qtq.fTongTienDeNghiQuyetToanQuyNay,
		qtq.fTongTienXacNhanQuyetToanQuyNay,
		dv.sTenDonVi,
		qtq.bDaTongHop,
		qtq.sDSSoChungTuTongHop,
		qtq.sDSLNS
	from BH_QTC_Quy_KCB as qtq
	left join DonVi as dv on qtq.iID_DonVi = dv.iID_DonVi
	where dv.iNamLamViec = @INamLamViec
End
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_export_kehoachcaptamung_kcbbhyt]    Script Date: 5/16/2024 10:52:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_export_kehoachcaptamung_kcbbhyt]
@IdCsYTe NVARCHAR(MAX),
@ILoai int,
@IQuy int,
@NamLamViec int,
@UserName NVARCHAR(100),
@Donvitinh int,
@Lns nvarchar(1000),
@IsRoundMillion bit
As
begin
	if (@IsRoundMillion = 1)
	begin
		select 
			row_number() over (order by cptu_ct.iID_MaCoSoYTe) as sTT,
			ROUND(sum(ISNULL(cptu_ct.fQTQuyTruoc,0)),0)/@Donvitinh as fQTQuyTruoc, 
			ROUND(sum(ISNULL(cptu_ct.fTamUngQuyNay,0))/1000000 , 0)*1000000 /@Donvitinh as fTamUngQuyNay, 
			--ROUND(sum(ISNULL(cp_bs.fSoCapBoSung,0) + ISNULL(cp_bs.fDaCapUng,0)),0)/@Donvitinh  as  fLuyKeCapDenCuoiQuy,
			ROUND(sum(ISNULL(cptu_ct.fLuyKeCapDenCuoiQuy,0))/1000000 , 0)*1000000 /@Donvitinh as fLuyKeCapDenCuoiQuy,
			cptu_ct.iID_MaCoSoYTe as iID_MaCoSoYTe,
			csyt.sTenCoSoYTe as sTenCoSoYTe,
			cptu_ct.sGhiChu
		from BH_CP_CapTamUng_KCB_BHYT_ChiTiet as cptu_ct
		inner join BH_CP_CapTamUng_KCB_BHYT as cptu on cptu_ct.iID_BH_CP_CapTamUng_KCB_BHYT = cptu.iID_BH_CP_CapTamUng_KCB_BHYT
		left join (
					select sum(isnull(ct.fSoCapBoSung,0)) as fSoCapBoSung, sum(isnull(ct.fDaCapUng,0)) as fDaCapUng,  iID_MLNS, iID_MaCoSoYTe
					from BH_CP_CapBoSung_KCB_BHYT_ChiTiet as ct
					inner join   BH_CP_CapBoSung_KCB_BHYT bs on ct.iID_CTCapPhatBS = bs.iID_CTCapPhatBS
					where bs.iQuy = @iQuy - 1
					group by iID_MLNS, iID_MaCoSoYTe) as cp_bs on cp_bs.iID_MaCoSoYTe = cptu_ct.iID_MaCoSoYTe and cptu_ct.iID_MLNS = cp_bs.iID_MLNS
		inner join DM_CoSoYTe as csyt on csyt.iID_MaCoSoYTe = cptu_ct.iID_MaCoSoYTe
		where cptu_ct.iID_MaCoSoYTe In (SELECT * FROM f_split(@IdCsYTe))
			--and cptu.bIsTongHop <> 1 and cptu.sDSSoChungTuTongHop is null
			and cptu.iQuy = @IQuy
			and cptu_ct.sLNS In (SELECT * FROM f_split(@Lns))
			AND csyt.iNamLamViec = @NamLamViec
			and cptu.iNamLamViec = @NamLamViec
			--and ((@ILoai = 1 and cptu_ct.sLNS = '9050001') or (@ILoai = 2 and cptu_ct.sLNS = '9050002'))
		group by cptu_ct.iID_MaCoSoYTe, csyt.sTenCoSoYTe,cptu_ct.sGhiChu
	end
	else
	begin
		select 
			row_number() over (order by cptu_ct.iID_MaCoSoYTe) as sTT,
			ROUND(sum(ISNULL(cptu_ct.fQTQuyTruoc,0)),0)/@Donvitinh as fQTQuyTruoc, 
			ROUND(sum(ISNULL(cptu_ct.fTamUngQuyNay,0)),0)/@Donvitinh as fTamUngQuyNay, 
			ROUND(sum(ISNULL(cptu_ct.fLuyKeCapDenCuoiQuy,0)),0)/@Donvitinh as fLuyKeCapDenCuoiQuy,
			cptu_ct.iID_MaCoSoYTe as iID_MaCoSoYTe,
			csyt.sTenCoSoYTe as sTenCoSoYTe,
			cptu_ct.sGhiChu
		from BH_CP_CapTamUng_KCB_BHYT_ChiTiet as cptu_ct
		inner join BH_CP_CapTamUng_KCB_BHYT as cptu on cptu_ct.iID_BH_CP_CapTamUng_KCB_BHYT = cptu.iID_BH_CP_CapTamUng_KCB_BHYT
		left join (
					select sum(isnull(ct.fSoCapBoSung,0)) as fSoCapBoSung, sum(isnull(ct.fDaCapUng,0)) as fDaCapUng,  iID_MLNS, iID_MaCoSoYTe
					from BH_CP_CapBoSung_KCB_BHYT_ChiTiet as ct
					inner join   BH_CP_CapBoSung_KCB_BHYT bs on ct.iID_CTCapPhatBS = bs.iID_CTCapPhatBS
					where bs.iQuy = @iQuy - 1
					group by iID_MLNS, iID_MaCoSoYTe) as cp_bs on cp_bs.iID_MaCoSoYTe = cptu_ct.iID_MaCoSoYTe and cptu_ct.iID_MLNS = cp_bs.iID_MLNS
		inner join DM_CoSoYTe as csyt on csyt.iID_MaCoSoYTe = cptu_ct.iID_MaCoSoYTe
		where cptu_ct.iID_MaCoSoYTe In (SELECT * FROM f_split(@IdCsYTe))
			and cptu.iQuy = @IQuy
			and cptu_ct.sLNS In (SELECT * FROM f_split(@Lns))
			AND csyt.iNamLamViec = @NamLamViec
			and cptu.iNamLamViec = @NamLamViec
		group by cptu_ct.iID_MaCoSoYTe, csyt.sTenCoSoYTe,cptu_ct.sGhiChu
	end
end
;
;
;
;
;
GO
