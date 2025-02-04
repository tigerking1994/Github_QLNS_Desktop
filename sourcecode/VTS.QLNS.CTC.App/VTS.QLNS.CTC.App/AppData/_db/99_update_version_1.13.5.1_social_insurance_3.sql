/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_bhxh]    Script Date: 11/15/2023 5:55:50 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_bhxh]    Script Date: 11/15/2023 5:55:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_bhxh]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@SLN nvarchar(max),
@IsTongHop bit,
@IQuy int,
@Donvitinh int
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
			danhmuc.bHangCha
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach as danhmuc
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs  in (select * from dbo.splitstring(@SLN)) 

		---Lấy danh sách đơn vị được chọn
		select 
			iID_DonVi,
			iID_MaDonVi,
			sTenDonVi
		into #tblDonVi
		from DonVi where iNamLamViec = @INamLamViec and iID_MaDonVi  in (select * from dbo.splitstring(@IdMaDonVi)) 

		---Lấy thông tin chi tiết chứng từ
		select
			ROW_NUMBER() OVER(ORDER BY as_qtct_1.iID_MaDonVi ASC) AS sTT,
			as_qtct_1.iID_MaDonVi,
			#tblDonVi.sTenDonVi,
			Sum(iSoNgaySinhConNNuoiCon) as iSoNgaySinhConNNuoiCon,
			Sum(fSoTienSinhConNNuoiCon) as fSoTienSinhConNNuoiCon,
			Sum(iSoNgaySinhTroCapSinhCon) as iSoNgaySinhTroCapSinhCon,
			Sum(fSoTienSinhTroCapSinhCon) as fSoTienSinhTroCapSinhCon,
			Sum(iSoNgayKhamThaiKHHGD) as ISoNgayKhamThaiKHHGD,
			Sum(fSoTienKhamThaiKHHGD) as fSoTienKhamThaiKHHGD,
			Sum(iSoNgayPHSKThaiSan) as iSoNgayPHSKThaiSan,
			Sum(fSoTienPHSKThaiSan) as fSoTienPHSKThaiSan,
			isnull(Sum(fSoTienSinhConNNuoiCon),0) + isnull(Sum(fSoTienSinhTroCapSinhCon),0) + isnull(Sum(fSoTienKhamThaiKHHGD),0) + isnull(Sum(fSoTienPHSKThaiSan),0)  as fTongTien
			
		from
			(
			select
				as_qtct.iID_MaDonVi,
				case when as_qtct.sXauNoiMa = '010-011-0002-0001-01-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-01-00-02' then iTongSo_DeNghi else 0 end iSoNgaySinhConNNuoiCon,
				case when as_qtct.sXauNoiMa = '010-011-0002-0001-01-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-01-00-02' then fTongTien_DeNghi else 0 end fSoTienSinhConNNuoiCon,
				case when as_qtct.sXauNoiMa = '010-011-0002-0001-02-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-02-00-02' then iTongSo_DeNghi else 0 end iSoNgaySinhTroCapSinhCon,
				case when as_qtct.sXauNoiMa = '010-011-0002-0001-02-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-02-00-02' then fTongTien_DeNghi else 0 end fSoTienSinhTroCapSinhCon,
				case when as_qtct.sXauNoiMa = '010-011-0002-0001-03-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-03-00-02' then iTongSo_DeNghi else 0 end iSoNgayKhamThaiKHHGD,
				case when as_qtct.sXauNoiMa = '010-011-0002-0001-03-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-03-00-02' then fTongTien_DeNghi else 0 end fSoTienKhamThaiKHHGD,
				case when as_qtct.sXauNoiMa = '010-011-0002-0001-04-00' then iTongSo_DeNghi else 0 end iSoNgayPHSKThaiSan,
				case when as_qtct.sXauNoiMa = '010-011-0002-0001-04-00' then fTongTien_DeNghi else 0 end fSoTienPHSKThaiSan
			from
			(
				select 
					qtcn.iID_MaDonVi,
					case when sLNS = '9010001' then REPLACE(sXauNoiMa,'9010001-' , '') else REPLACE(sXauNoiMa,'9010002-' , '') end sXauNoiMa,
					Sum(qtcn_ct.iTongSo_DeNghi) as iTongSo_DeNghi,
					Sum(qtcn_ct.fTongTien_DeNghi)  as fTongTien_DeNghi
				from BH_QTC_Quy_CheDoBHXH_ChiTiet as qtcn_ct
				inner join BH_QTC_Quy_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Quy_CheDoBHXH = qtcn.ID_QTC_Quy_CheDoBHXH
				inner join #tblMucLucNganSach on #tblMucLucNganSach.iID_MLNS = qtcn_ct.iID_MucLucNganSach
				and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
				and qtcn.iQuyChungTu = @IQuy
				group by qtcn.iID_MaDonVi,case when sLNS = '9010001' then REPLACE(sXauNoiMa,'9010001-' , '') else REPLACE(sXauNoiMa,'9010002-' , '') end

			) as as_qtct) as as_qtct_1

			inner join #tblDonVi on #tblDonVi.iID_MaDonVi = as_qtct_1.iID_MaDonVi
			group by as_qtct_1.iID_MaDonVi, #tblDonVi.sTenDonVi
		
		

		drop table #tblMucLucNganSach;
		drop table #tblDonVi


end

GO
