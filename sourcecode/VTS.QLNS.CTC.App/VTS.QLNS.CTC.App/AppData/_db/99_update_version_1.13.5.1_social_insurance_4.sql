/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_bhxh]    Script Date: 11/16/2023 5:07:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquytruocbhxh_chitiet]    Script Date: 11/16/2023 5:07:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chiquytruocbhxh_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chiquytruocbhxh_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]    Script Date: 11/16/2023 5:07:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKPQL_create_datafor_quaterbefore]    Script Date: 11/16/2023 5:07:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqKPQL_create_datafor_quaterbefore]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqKPQL_create_datafor_quaterbefore]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKPk_create_datafor_quaterbefore]    Script Date: 11/16/2023 5:07:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqKPk_create_datafor_quaterbefore]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqKPk_create_datafor_quaterbefore]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKCB_create_datafor_quaterbefore]    Script Date: 11/16/2023 5:07:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqKCB_create_datafor_quaterbefore]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqKCB_create_datafor_quaterbefore]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqBHXH_create_datafor_quaterbefore]    Script Date: 11/16/2023 5:07:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqBHXH_create_datafor_quaterbefore]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqBHXH_create_datafor_quaterbefore]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqBHXH_create_datafor_quaterbefore]    Script Date: 11/16/2023 5:07:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtcqBHXH_create_datafor_quaterbefore]
@IdChungTu nvarchar(MAX),
@Username nvarchar(500),
@NamLamViec int,
@Quy int,
@MaDonVi nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

	INSERT INTO BH_QTC_Quy_CheDoBHXH_ChiTiet
	( 
		ID_QTC_Quy_CheDoBHXH_ChiTiet,
		iID_QTC_Quy_CheDoBHXH,
		iID_MucLucNganSach,
		sLoaiTroCap,
		dNgayTao,
		sNguoiTao,
		fTienLuyKeCuoiQuyNay
	)
	SELECT 
		NEWID(),
		@IdChungTu,
		qtcn_ct.iID_MucLucNganSach,
		qtcn_ct.sLoaiTroCap,
		GETDATE(),
		@Username,
		SUM(qtcn_ct.fTongTien_DeNghi)
	FROM BH_QTC_Quy_CheDoBHXH_ChiTiet qtcn_ct
		inner join BH_QTC_Quy_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Quy_CheDoBHXH = qtcn.ID_QTC_Quy_CheDoBHXH
	WHERE qtcn.iQuyChungTu < @Quy
			AND qtcn.iID_MaDonVi = @MaDonVi
			AND qtcn.iNamChungTu = @NamLamViec
			AND qtcn.bIsKhoa=1
		GROUP BY qtcn_ct.iID_MucLucNganSach, qtcn_ct.sLoaiTroCap
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKCB_create_datafor_quaterbefore]    Script Date: 11/16/2023 5:07:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtcqKCB_create_datafor_quaterbefore]
@IdChungTu nvarchar(MAX),
@Username nvarchar(500),
@NamLamViec int,
@Quy int,
@MaDonVi nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

	INSERT INTO BH_QTC_Quy_KCB_ChiTiet
	( 
		ID_QTC_Quy_KCB_ChiTiet,
		iID_QTC_Quy_KCB,
		iID_MucLucNganSach,
		sNoiDung,
		dNgayTao,
		sNguoiTao,
		fTienQuyetToanDaDuyet
	)
	SELECT 
		NEWID(),
		@IdChungTu,
		qtcn_ct.iID_MucLucNganSach,
		qtcn_ct.sNoiDung,
		GETDATE(),
		@Username,
		SUM(qtcn_ct.fTienXacNhanQuyetToanQuyNay)
	FROM BH_QTC_Quy_KCB_ChiTiet qtcn_ct
		inner join BH_QTC_Quy_KCB  as qtcn on qtcn_ct.iID_QTC_Quy_KCB = qtcn.ID_QTC_Quy_KCB
	WHERE qtcn.iQuyChungTu < @Quy
			AND qtcn.iID_MaDonVi = @MaDonVi
			AND qtcn.iNamChungTu = @NamLamViec
			AND qtcn.bIsKhoa=1
		GROUP BY qtcn_ct.iID_MucLucNganSach, qtcn_ct.sNoiDung
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKPk_create_datafor_quaterbefore]    Script Date: 11/16/2023 5:07:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
cREATE PROCEDURE [dbo].[sp_bh_qtcqKPk_create_datafor_quaterbefore]
@IdChungTu nvarchar(MAX),
@Username nvarchar(500),
@NamLamViec int,
@Quy int,
@LoaiChi uniqueidentifier,
@MaDonVi nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

	INSERT INTO BH_QTC_Quy_KPK_ChiTiet
	( 
		ID_QTC_Quy_KPK_ChiTiet,
		iID_QTC_Quy_KPK,
		iID_MucLucNganSach,
		sNoiDung,
		dNgayTao,
		sNguoiTao,
		fTienQuyetToanDaDuyet
	)
	SELECT 
		NEWID(),
		@IdChungTu,
		qtcn_ct.iID_MucLucNganSach,
		qtcn_ct.sNoiDung,
		GETDATE(),
		@Username,
		SUM(qtcn_ct.fTienXacNhanQuyetToanQuyNay)
	FROM BH_QTC_Quy_KPK_ChiTiet qtcn_ct
		inner join BH_QTC_Quy_KPK  as qtcn on qtcn_ct.iID_QTC_Quy_KPK = qtcn.ID_QTC_Quy_KPK
	WHERE qtcn.iQuyChungTu < @Quy
			AND qtcn.iID_MaDonVi = @MaDonVi
			AND qtcn.iNamChungTu = @NamLamViec
			AND qtcn.bIsKhoa=1
			AND iID_LoaiChi=@LoaiChi
		GROUP BY qtcn_ct.iID_MucLucNganSach, qtcn_ct.sNoiDung
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKPQL_create_datafor_quaterbefore]    Script Date: 11/16/2023 5:07:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtcqKPQL_create_datafor_quaterbefore]
@IdChungTu nvarchar(MAX),
@Username nvarchar(500),
@NamLamViec int,
@Quy int,
@MaDonVi nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

	INSERT INTO BH_QTC_Quy_KinhPhiQuanLy_ChiTiet
	( 
		ID_QTC_Quy_KinhPhiQuanLy_ChiTiet,
		iID_QTC_Quy_KinhPhiQuanLy,
		iID_MucLucNganSach,
		sNoiDung,
		dNgayTao,
		sNguoiTao,
		fTienQuyetToanDaDuyet
	)
	SELECT 
		NEWID(),
		@IdChungTu,
		qtcn_ct.iID_MucLucNganSach,
		qtcn_ct.sNoiDung,
		GETDATE(),
		@Username,
		SUM(qtcn_ct.fTienXacNhanQuyetToanQuyNay)
	FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet qtcn_ct
		inner join BH_QTC_Quy_KinhPhiQuanLy  as qtcn on qtcn_ct.iID_QTC_Quy_KinhPhiQuanLy = qtcn.ID_QTC_Quy_KinhPhiQuanLy
	WHERE qtcn.iQuyChungTu < @Quy
			AND qtcn.iID_MaDonVi = @MaDonVi
			AND qtcn.iNamChungTu = @NamLamViec
			AND qtcn.bIsKhoa=1
		GROUP BY qtcn_ct.iID_MucLucNganSach, qtcn_ct.sNoiDung
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]    Script Date: 11/16/2023 5:07:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]
@IdChungTu uniqueidentifier,
@SLNS nvarchar(max),
@INamLamViec int
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
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs in (SELECT * FROM f_split(@SLNS))
		
		
		
	---Lấy thông tin chi tiết chứng từ
		select 
			qtcn_ct.ID_QTC_Quy_CheDoBHXH_ChiTiet,
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sLoaiTroCap,
			qtcn_ct.fTienDuToanDuyet,
			qtcn_ct.iSoLuyKeCuoiQuyNay,
			qtcn_ct.fTienLuyKeCuoiQuyNay,
			qtcn_ct.iSoSQ_DeNghi,
			qtcn_ct.fTienSQ_DeNghi,
			qtcn_ct.iSoQNCN_DeNghi,
			qtcn_ct.fTienQNCN_DeNghi,
			qtcn_ct.iSoCNVCQP_DeNghi,
			qtcn_ct.fTienCNVCQP_DeNghi,
			qtcn_ct.iSoHSQBS_DeNghi,
			qtcn_ct.fTienHSQBS_DeNghi,
			qtcn_ct.iTongSo_DeNghi,
			qtcn_ct.fTongTien_DeNghi,
			qtcn_ct.fTongTien_PheDuyet,
			qtcn_ct.iSoLDHD_DeNghi,
			qtcn_ct.fTienLDHD_DeNghi
		into #tblQuyetToanQuyChiTiet
		from BH_QTC_Quy_CheDoBHXH_ChiTiet as qtcn_ct
		inner join BH_QTC_Quy_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Quy_CheDoBHXH = qtcn.ID_QTC_Quy_CheDoBHXH
		where qtcn_ct.iID_QTC_Quy_CheDoBHXH = @IdChungTu
			AND qtcn.iNamChungTu=@INamLamViec;

		
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
			chi_tiet.ID_QTC_Quy_CheDoBHXH_ChiTiet,
			mucluc.iID_MLNS as iID_MucLucNganSach,
			mucluc.sMoTa as sLoaiTroCap,
			chi_tiet.fTienDuToanDuyet,
			chi_tiet.iSoLuyKeCuoiQuyNay,
			chi_tiet.fTienLuyKeCuoiQuyNay,
			chi_tiet.iSoSQ_DeNghi,
			chi_tiet.fTienSQ_DeNghi,
			chi_tiet.iSoQNCN_DeNghi,
			chi_tiet.fTienQNCN_DeNghi,
			chi_tiet.iSoCNVCQP_DeNghi,
			chi_tiet.fTienCNVCQP_DeNghi,
			chi_tiet.iSoHSQBS_DeNghi,
			chi_tiet.fTienHSQBS_DeNghi,
			chi_tiet.iTongSo_DeNghi,
			chi_tiet.fTongTien_DeNghi,
			chi_tiet.fTongTien_PheDuyet,
			chi_tiet.iSoLDHD_DeNghi,
			chi_tiet.fTienLDHD_DeNghi

		from #tblMucLucNganSach as mucluc
		left join #tblQuyetToanQuyChiTiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
		order by mucluc.sXauNoiMa
	
	drop table #tblMucLucNganSach;
	drop table #tblQuyetToanQuyChiTiet;
end


GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquytruocbhxh_chitiet]    Script Date: 11/16/2023 5:07:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_chiquytruocbhxh_chitiet]
@MaDonVi nvarchar(max),
@SLNS nvarchar(max),
@Quy int,
@NamLamViec int
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
		where danhmuc.iNamLamViec = @NamLamViec and danhmuc.sLNs in (select * from splitstring(@SLNS))

	---Lấy thông tin chi tiết chứng từ
		select 
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sLoaiTroCap,
			SUM(qtcn_ct.fTongTien_DeNghi) fTongTien_DeNghi
		into #tblQuyetToanQuyChiTiet
		from BH_QTC_Quy_CheDoBHXH_ChiTiet as qtcn_ct
		inner join BH_QTC_Quy_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Quy_CheDoBHXH = qtcn.ID_QTC_Quy_CheDoBHXH
		where qtcn.iQuyChungTu < @Quy
			AND qtcn.iID_MaDonVi = @MaDonVi
			AND qtcn.iNamChungTu = @NamLamViec
		GROUP BY qtcn_ct.iID_MucLucNganSach, qtcn_ct.sLoaiTroCap
		
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
			chi_tiet.fTongTien_DeNghi
		from #tblMucLucNganSach as mucluc
		left join #tblQuyetToanQuyChiTiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
		where chi_tiet.fTongTien_DeNghi <> 0
		order by mucluc.sXauNoiMa
	
	drop table #tblMucLucNganSach;
	drop table #tblQuyetToanQuyChiTiet;
end
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_bhxh]    Script Date: 11/16/2023 5:07:20 PM ******/
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
				--and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
				and qtcn.iQuyChungTu = @IQuy
				group by qtcn.iID_MaDonVi,case when sLNS = '9010001' then REPLACE(sXauNoiMa,'9010001-' , '') else REPLACE(sXauNoiMa,'9010002-' , '') end

			) as as_qtct) as as_qtct_1

			inner join #tblDonVi on #tblDonVi.iID_MaDonVi = as_qtct_1.iID_MaDonVi
			group by as_qtct_1.iID_MaDonVi, #tblDonVi.sTenDonVi
		
		

		drop table #tblMucLucNganSach;
		drop table #tblDonVi


end

GO
