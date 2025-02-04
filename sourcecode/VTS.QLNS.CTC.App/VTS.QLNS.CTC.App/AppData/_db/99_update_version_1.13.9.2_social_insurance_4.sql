/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]    Script Date: 1/23/2024 8:59:40 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_phulucquyettoannam_KCB]    Script Date: 1/23/2024 8:59:40 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_phulucquyettoannam_KCB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_phulucquyettoannam_KCB]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]    Script Date: 1/23/2024 8:59:40 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_danhsachquyettoannam_index]    Script Date: 1/23/2024 10:42:59 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_danhsachquyettoannam_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_danhsachquyettoannam_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_bhxh]    Script Date: 1/23/2024 10:14:46 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]    Script Date: 1/23/2024 8:59:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@Lns nvarchar(max),
@Donvitinh int,
@IsTongHop int
as
begin
	---Lấy danh sách mục lục ngân sách
		select 
			danhmuc.iID_MLNS as iID_MLNS,
			danhmuc.iID_MLNS_Cha,
			danhmuc.sLNS,
			danhmuc.sL,
			danhmuc.sK,
			danhmuc.sM,
			danhmuc.sTM,
			danhmuc.sTTM,
			danhmuc.sNG,
			danhmuc.sTNG,
			danhmuc.sXauNoiMa,
			danhmuc.sMoTa,
			danhmuc.bHangCha,
			danhmuc.sDuToanChiTietToi
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach as danhmuc
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs in (select * from splitstring(@Lns))
				and danhmuc.iTrangThai=1


	---Lấy danh sách chi tiết
		select	
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sNoiDung,
			Sum(qtcn_ct.fTien_DuToanNamTruocChuyenSang)/@Donvitinh as fTien_DuToanNamTruocChuyenSang,
			Sum(qtcn_ct.fTien_DuToanGiaoNamNay)/@Donvitinh as fTien_DuToanGiaoNamNay,
			Sum(qtcn_ct.fTien_TongDuToanDuocGiao)/@Donvitinh as fTien_TongDuToanDuocGiao,
			Sum(qtcn_ct.fTien_ThucChi)/@Donvitinh as fTien_ThucChi,
			Sum(qtcn_ct.fTienThua)/@Donvitinh as fTienThua,
			Sum(qtcn_ct.fTienThieu)/@Donvitinh as fTienThieu,
			Sum(qtcn_ct.fTiLeThucHienTrenDuToan) as fTiLeThucHienTrenDuToan
			--CASE WHEN ISNULL(Sum(qtcn_ct.fTien_TongDuToanDuocGiao),0) >  ISNULL(Sum(qtcn_ct.fTien_ThucChi),0) THEN 
			--ISNULL(Sum(qtcn_ct.fTien_TongDuToanDuocGiao),0) -  ISNULL(Sum(qtcn_ct.fTien_ThucChi),0) ELSE 0 END fTienThua,

			--CASE WHEN ISNULL(Sum(qtcn_ct.fTien_TongDuToanDuocGiao),0) <  ISNULL(Sum(qtcn_ct.fTien_ThucChi),0) THEN 
			--ISNULL(Sum(qtcn_ct.fTien_ThucChi),0)- ISNULL(Sum(qtcn_ct.fTien_TongDuToanDuocGiao),0)  ELSE 0 END fTienThieu,
			--Sum(qtcn_ct.fTien_TongDuToanDuocGiao) - Sum(qtcn_ct.fTien_ThucChi)  as fTienThua,
			--Sum(qtcn_ct.fTien_ThucChi) - Sum(qtcn_ct.fTien_TongDuToanDuocGiao) as fTienThieu,
			--CASE WHEN ISNULL(Sum(qtcn_ct.fTien_ThucChi),0) > 0 and ISNULL(Sum(qtcn_ct.fTien_TongDuToanDuocGiao),0) > 0 THEN 
			--ISNULL(Sum(qtcn_ct.fTien_ThucChi),0) / ISNULL(Sum(qtcn_ct.fTien_TongDuToanDuocGiao),0)  ELSE 0 END fTiLeThucHienTrenDuToan
			--Sum(qtcn_ct.fTiLeThucHienTrenDuToan) as fTiLeThucHienTrenDuToan

		into #tbl_qtcn_chitiet
		from BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet as  qtcn_ct
		inner join  BH_QTC_Nam_KCB_QuanYDonVi as qtcn on qtcn_ct.iID_QTC_Nam_KCB_QuanYDonVi = qtcn.ID_QTC_Nam_KCB_QuanYDonVi
		where qtcn.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
		and qtcn.iNamLamViec = @INamLamViec
		--and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
		group by qtcn_ct.iID_MucLucNganSach,qtcn_ct.sNoiDung



		---Kết quả hiển thị trả về
		select 

			mucluc.iID_MLNS,
			mucluc.iID_MLNS_Cha,
			mucluc.sLNS,
			mucluc.sL,
			mucluc.sK,
			mucluc.sM,
			mucluc.sTM,
			mucluc.sTTM,
			mucluc.sNG,
			mucluc.sTNG,
			mucluc.sXauNoiMa,
			mucluc.bHangCha,
			mucluc.iID_MLNS as iID_MucLucNganSach,
			mucluc.sDuToanChiTietToi,
			mucluc.sMoTa as sNoiDung,
			chi_tiet.fTien_DuToanNamTruocChuyenSang, 
			chi_tiet.fTien_DuToanGiaoNamNay,
			chi_tiet.fTien_TongDuToanDuocGiao,
			chi_tiet.fTien_ThucChi,
			chi_tiet.fTienThua,
			chi_tiet.fTienThieu,
			chi_tiet.fTiLeThucHienTrenDuToan

		from #tblMucLucNganSach as mucluc
		left join #tbl_qtcn_chitiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
		order by mucluc.sXauNoiMa


		drop table #tblMucLucNganSach;
		drop table #tbl_qtcn_chitiet;

end
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_phulucquyettoannam_KCB]    Script Date: 1/23/2024 8:59:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_phulucquyettoannam_KCB]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@Lns nvarchar(max),
@Donvitinh int,
@IsTongHop int
as
begin
	---Lấy quyết toán năm trước
	--select
	--	donvi.sTenDonVi,
	--	0 as fChiTieuNamTruoc,
	--	--sum(chungtu_ct.fTien_TongDuToanDuocGiao) as fChiTieuNamTruoc,
	--	0 as fChiTieuNamNay,
	--	0 as fTongCong,
	--	0 as fTienQuyetToan,
	--	0 as fTienThua,
	--	0 as fTienThieu
	--into #tblNamTruoc
	--from BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet as chungtu_ct
	--inner join BH_QTC_Nam_KCB_QuanYDonVi as chungtu on chungtu_ct.iID_QTC_Nam_KCB_QuanYDonVi = chungtu.ID_QTC_Nam_KCB_QuanYDonVi
	--inner join (
	--				select * from BH_DM_MucLucNganSach where iNamLamViec = @INamLamViec -1 and sLNs in (select * from dbo.splitstring(@Lns))
	--			) as danhmuc on danhmuc.iID_MLNS = chungtu_ct.iID_MucLucNganSach
	--inner join (
	--				select * from DonVi where iNamLamViec = @INamLamViec -1 
	--			) as donvi on donvi.iID_MaDonVi = chungtu.iID_MaDonVi
	--where chungtu.iNamLamViec = @INamLamViec -1
	--and chungtu.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi))
	----and ((@IsTongHop = 0 and chungtu.bDaTongHop = 0 and chungtu.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  chungtu.sDSSoChungTuTongHop is not null))
	--group by donvi.sTenDonVi

	-----Lấy quyết toán năm trước

	--select
	--	donvi.sTenDonVi,
	--	sum(chungtu_ct.fTien_DuToanNamTruocChuyenSang) fChiTieuNamTruoc,
	--	sum(chungtu_ct.fTien_TongDuToanDuocGiao) as fChiTieuNamNay,
	--	0 as fTongCong,
	--	sum(fTien_ThucChi) as fTienQuyetToan,
	--	0 as fTienThua,
	--	0 as fTienThieu
	--into #tblNamNay
	--from BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet as chungtu_ct
	--inner join BH_QTC_Nam_KCB_QuanYDonVi as chungtu on chungtu_ct.iID_QTC_Nam_KCB_QuanYDonVi = chungtu.ID_QTC_Nam_KCB_QuanYDonVi
	--inner join (
	--				select * from BH_DM_MucLucNganSach where iNamLamViec = @INamLamViec and sLNs in (select * from dbo.splitstring(@Lns))
	--			) as danhmuc on danhmuc.iID_MLNS = chungtu_ct.iID_MucLucNganSach
	--inner join (
	--				select * from DonVi where iNamLamViec = @INamLamViec
	--			) as donvi on donvi.iID_MaDonVi = chungtu.iID_MaDonVi
	--where chungtu.iNamLamViec = @INamLamViec
	--and chungtu.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi))
	----and ((@IsTongHop = 0 and chungtu.bDaTongHop = 0 and chungtu.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  chungtu.sDSSoChungTuTongHop is not null))
	--group by donvi.sTenDonVi

	----- Kết quả trả về

	--select
	--	ROW_NUMBER () OVER (ORDER BY sTenDonVi) sTT,
	--	sTenDonVi,
	--	sum(fChiTieuNamTruoc) as fChiTieuNamTruoc,
	--	sum(fChiTieuNamNay) as fChiTieuNamNay,
	--	isnull(sum(fChiTieuNamTruoc) ,0) + isnull(sum(fChiTieuNamNay) ,0) as fTongCong,
	--	sum(fTienQuyetToan) as fTienQuyetToan,
	--	--isnull(sum(fChiTieuNamTruoc) ,0) + isnull(sum(fChiTieuNamNay) ,0) - isnull(sum(fTienQuyetToan) ,0) as fTienThua,
	--	--isnull(sum(fTienQuyetToan) ,0) - isnull(sum(fChiTieuNamTruoc) ,0) + isnull(sum(fChiTieuNamNay) ,0) as fTienThieu
	--	CASE WHEN ( isnull(sum(fChiTieuNamTruoc) ,0) + isnull(sum(fChiTieuNamNay) ,0)) > isnull(sum(fTienQuyetToan) ,0) THEN 
	--	isnull(sum(fChiTieuNamTruoc) ,0) + isnull(sum(fChiTieuNamNay) ,0) - isnull(sum(fTienQuyetToan) ,0) ELSE 0  END  fTienThua,
	--	CASE WHEN  (isnull(sum(fChiTieuNamTruoc) ,0) + isnull(sum(fChiTieuNamNay) ,0)) < isnull(sum(fTienQuyetToan) ,0) THEN 
	--	isnull(sum(fTienQuyetToan) ,0) - (isnull(sum(fChiTieuNamTruoc) ,0) + isnull(sum(fChiTieuNamNay) ,0)) ELSE 0  END  fTienThieu

	--from
	--(
	--	select * from #tblNamTruoc
	--	union all
	--	select * from #tblNamNay
	--) as result

	--group by sTenDonVi
	--order by sTenDonVi


	--drop table #tblNamTruoc;
	--drop table #tblNamNay;

	select
		ROW_NUMBER () OVER (ORDER BY sTenDonVi) sTT,
		donvi.sTenDonVi,
		sum(chungtu_ct.fTien_DuToanNamTruocChuyenSang)/@Donvitinh fChiTieuNamTruoc,
		sum(chungtu_ct.fTien_TongDuToanDuocGiao)/@Donvitinh as fChiTieuNamNay,
		sum (chungtu_ct.fTien_TongDuToanDuocGiao)/@Donvitinh as fTongCong,
		sum(fTien_ThucChi)/@Donvitinh as fTienQuyetToan,
		sum(fTienThua)/@Donvitinh as fTienThua,
		sum(fTienThieu)/@Donvitinh as fTienThieu
	--into #tblNamNay
	from BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet as chungtu_ct
	inner join BH_QTC_Nam_KCB_QuanYDonVi as chungtu on chungtu_ct.iID_QTC_Nam_KCB_QuanYDonVi = chungtu.ID_QTC_Nam_KCB_QuanYDonVi
	inner join (
					select * from BH_DM_MucLucNganSach where iNamLamViec = @INamLamViec and sLNs in (select * from dbo.splitstring(@Lns))
				) as danhmuc on danhmuc.iID_MLNS = chungtu_ct.iID_MucLucNganSach
	inner join (
					select * from DonVi where iNamLamViec = @INamLamViec
				) as donvi on donvi.iID_MaDonVi = chungtu.iID_MaDonVi
	where chungtu.iNamLamViec = @INamLamViec
	and chungtu.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi))
	AND danhmuc.sDuToanChiTietToi='LNS'
	--and ((@IsTongHop = 0 and chungtu.bDaTongHop = 0 and chungtu.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  chungtu.sDSSoChungTuTongHop is not null))
	group by donvi.sTenDonVi
end
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]    Script Date: 1/23/2024 8:59:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]
	 @NamLamViec int,
	 @iD uniqueidentifier,
	 @IdDonVi nvarchar(max),
	 @LNS nvarchar(max),
	 @DonViTnh int
AS
BEGIN 
	SET NOCOUNT ON;
	DECLARE @iDCT uniqueidentifier='00000000-0000-0000-0000-000000000000';
	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha, sDuToanChiTietToi
			into #tblMlnsByPhanCap
	FROM BH_DM_MucLucNganSach 
	WHERE 
		iNamLamViec = @NamLamViec 
		AND iTrangThai = 1
		--AND (
		--	(@PhanCap = 'M' AND (sTM IS NULL OR sTM = ''))
		--	OR (@PhanCap = 'TM' AND (sTTM IS NULL OR sTTM = ''))
		--	OR (@PhanCap = 'NG' AND (sTNG IS NULL OR sTNG = ''))
		--	)
		AND sLNS IN (SELECT * FROM f_split(@LNS))
		--(
		--			SELECT DISTINCT VALUE
		--			FROM 
		--			(
		--				SELECT 
		--					CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1, 
		--					CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3, 
		--					CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5, 
		--					CAST(sLNS AS nvarchar(10)) LNS 
		--				FROM
		--					NS_NguoiDung_LNS 
		--				WHERE 
		--					sMaNguoiDung = @UserName
		--					AND INamLamViec = @NamLamViec
		--					AND sLNS IN (SELECT * FROM f_split(@LNS))
		--			) LNS
		--			UNPIVOT
		--			(
		--				value
		--				FOR col in (LNS1, LNS3, LNS5, LNS)
		--			) un) 

SELECT   isnull(ctct.ID_QTC_Nam_KinhPhiQuanLy_ChiTiet, @iDCT) as ID_QTC_Nam_KinhPhiQuanLy_ChiTiet,
		ctct.IID_QTC_Nam_KinhPhiQuanLy,
		mlns.iID_MLNS as iID_MucLucNganSach,
		mlns.iID_MLNS_Cha as IdParent,
		mlns.sXauNoiMa as sXauNoiMa,
		mlns.sLNS as SLNS,
		mlns.sL as SL,
		mlns.sK as SK,
		mlns.sM as SM,
		mlns.sTM as STM,
		mlns.sTTM as STTM,
		mlns.sNG as SNG,
		mlns.sTNG as STNG,
		mlns.sMoTa as SNoiDung,
		mlns.sDuToanChiTietToi,
		--ctct.iID_MaDonVi,
		@NamLamViec AS INamLamViec,
		mlns.bHangCha as IsHangCha,
		isnull(ctct.dNgayTao, getdate()) AS dNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS dNgaySua,
		ctct.sNguoiTao AS sNguoiTao,
		ctct.sNguoiSua AS sNguoiSua,
		ISNULL(Sum(ctct.fTien_DuToanNamTruocChuyenSang), 0) / @DonViTnh  as fTien_DuToanNamTruocChuyenSang, 
		ISNULL(Sum(ctct.fTien_DuToanGiaoNamNay), 0) / @DonViTnh  as fTien_DuToanGiaoNamNay,
		ISNULL(Sum(ctct.fTien_DuToanNamTruocChuyenSang), 0) / @DonViTnh + ISNULL(Sum(ctct.fTien_DuToanGiaoNamNay), 0)/ @DonViTnh  as fTien_TongDuToanDuocGiao, 
		ISNULL(Sum(ctct.fTien_ThucChi), 0) / @DonViTnh  as fTien_ThucChi, 
		ISNULL(Sum(ctct.fTienThua), 0) / @DonViTnh  as fTienThua, 
		ISNULL(Sum(ctct.fTienThieu), 0) / @DonViTnh  as fTienThieu, 
		--ISNULL(Sum(ctct.fTiLeThucHienTrenDuToan), 0) / @DonViTnh  as fTiLeThucHienTrenDuToan
		ISNULL(Sum(ctct.fTiLeThucHienTrenDuToan), 0)   as fTiLeThucHienTrenDuToan
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(SELECT * FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet 
				WHERE iID_QTC_Nam_KinhPhiQuanLy in
					( SELECT ID_QTC_Nam_KinhPhiQuanLy FROM BH_QTC_Nam_KinhPhiQuanLy
								WHERE iNamLamViec=@NamLamViec
								AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
								)) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach
	Group by ctct.ID_QTC_Nam_KinhPhiQuanLy_ChiTiet,
		ctct.IID_QTC_Nam_KinhPhiQuanLy,
		--ctct.iID_MaDonVi,
		mlns.iID_MLNS ,
		mlns.iID_MLNS_Cha ,
		mlns.sXauNoiMa ,
		mlns.sLNS ,
		mlns.sL ,
		mlns.sK ,
		mlns.sM ,
		mlns.sTM ,
		mlns.sTTM ,
		mlns.sNG ,
		mlns.sTNG ,
		mlns.sMoTa ,
		mlns.bHangCha ,
		ctct.sNguoiTao ,
		ctct.sNguoiSua, 
		ctct.dNgayTao,
		mlns.sDuToanChiTietToi,
		ctct.dNgaySua

	Order by mlns.sXauNoiMa

	drop table #tblMlnsByPhanCap;

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_danhsachquyettoannam_index]    Script Date: 1/23/2024 10:42:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_danhsachquyettoannam_index]
@INamLamViec int,
@Search nvarchar(max)
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
	and (@Search is null or ( @Search is not null and  qtn.sSoChungTu like N'%'+@Search+ '%'))

	

End
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_bhxh]    Script Date: 1/23/2024 10:14:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_bhxh]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@Lns nvarchar(max),
@Donvitinh int,
@IsTongHop int
as
begin
	---Lấy danh sách mục lục ngân sách
		select 
			danhmuc.iID_MLNS as iID_MLNS,
			danhmuc.iID_MLNS_Cha,
			danhmuc.sLNS,
			danhmuc.sL,
			danhmuc.sK,
			danhmuc.sM,
			danhmuc.sTM,
			danhmuc.sTTM,
			danhmuc.sNG,
			danhmuc.sTNG,
			danhmuc.sXauNoiMa,
			danhmuc.sMoTa,
			danhmuc.bHangCha,
			danhmuc.sDuToanChiTietToi
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach as danhmuc
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs in (select * from dbo.splitstring(@Lns))


	---Lấy danh sách chi tiết
		select	
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sLoaiTroCap,
			Sum(qtcn_ct.fTienDuToanDuyet) as fTienDuToanDuyet,
			Sum(qtcn_ct.iSoDuToanDuocDuyet) as iSoDuToanDuocDuyet,
			Sum(qtcn_ct.fTongTien_ThucChi) as fTongTien_ThucChi,
			Sum(qtcn_ct.iTongSo_ThucChi) as iTongSo_ThucChi,
			Sum(qtcn_ct.iSoSQ_ThucChi) as iSoSQ_ThucChi,
			Sum(qtcn_ct.fTienSQ_ThucChi) as fTienSQ_ThucChi,
			Sum(qtcn_ct.iSoQNCN_ThucChi) as iSoQNCN_ThucChi,
			Sum(qtcn_ct.fTienQNCN_ThucChi) as fTienQNCN_ThucChi,
			Sum(qtcn_ct.iSoCNVCQP_ThucChi) as iSoCNVCQP_ThucChi,
			Sum(qtcn_ct.fTienCNVCQP_ThucChi) as fTienCNVCQP_ThucChi,
			Sum(qtcn_ct.iSoLDHD_ThucChi) as iSoLDHD_ThucChi,
			Sum(qtcn_ct.fTienLDHD_ThucChi) as fTienLDHD_ThucChi,
			Sum(qtcn_ct.iSoHSQBS_ThucChi) as iSoHSQBS_ThucChi,
			Sum(qtcn_ct.fTienHSQBS_ThucChi) as fTienHSQBS_ThucChi,
			Sum(qtcn_ct.fTienDuToanDuyet) - Sum(qtcn_ct.fTongTien_ThucChi) as fTienThua,
			Sum(qtcn_ct.fTongTien_ThucChi) - Sum(qtcn_ct.fTienDuToanDuyet) as fTienThieu
			
		into #tbl_qtcn_chitiet
		from BH_QTC_Nam_CheDoBHXH_ChiTiet as  qtcn_ct
		inner join  BH_QTC_Nam_CheDoBHXH as qtcn on qtcn_ct.iID_QTC_Nam_CheDoBHXH = qtcn.ID_QTC_Nam_CheDoBHXH
		where qtcn.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
		and qtcn.iNamLamViec = @INamLamViec
		--and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
		group by qtcn_ct.iID_MucLucNganSach,qtcn_ct.sLoaiTroCap



		---Kết quả hiển thị trả về
		select 
			mucluc.iID_MLNS,
			mucluc.iID_MLNS_Cha,
			mucluc.sLNS,
			mucluc.sL,
			mucluc.sK,
			mucluc.sM,
			mucluc.sTM,
			mucluc.sTTM,
			mucluc.sNG,
			mucluc.sTNG,
			mucluc.sXauNoiMa,
			mucluc.bHangCha,
			mucluc.iID_MLNS as iID_MucLucNganSach,
			mucluc.sDuToanChiTietToi,
			mucluc.sMoTa as sLoaiTroCap,
			chi_tiet.fTienDuToanDuyet, 
			chi_tiet.fTongTien_ThucChi,
			chi_tiet.iTongSo_ThucChi,
			chi_tiet.iSoSQ_ThucChi,
			chi_tiet.fTienSQ_ThucChi,
			chi_tiet.iSoQNCN_ThucChi,
			chi_tiet.fTienQNCN_ThucChi,
			chi_tiet.iSoCNVCQP_ThucChi,
			chi_tiet.fTienCNVCQP_ThucChi,
			chi_tiet.iSoLDHD_ThucChi,
			chi_tiet.fTienLDHD_ThucChi,
			chi_tiet.iSoHSQBS_ThucChi,
			chi_tiet.fTienHSQBS_ThucChi
		from #tblMucLucNganSach as mucluc
		left join #tbl_qtcn_chitiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
		order by mucluc.sXauNoiMa


		drop table #tblMucLucNganSach;
		drop table #tbl_qtcn_chitiet;

end
;
GO

