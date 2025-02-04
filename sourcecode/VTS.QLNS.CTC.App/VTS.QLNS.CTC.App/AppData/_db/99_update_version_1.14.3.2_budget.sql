/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_bhxh_qt_chi_tiet]    Script Date: 4/11/2024 7:28:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_bhxh_qt_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_bhxh_qt_chi_tiet]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_split_string]    Script Date: 4/11/2024 7:28:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_split_string]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_split_string]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_split_string]    Script Date: 4/11/2024 7:28:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create FUNCTION [dbo].[fn_split_string]
(
    @string    nvarchar(max),
    @delimiter nvarchar(max)
)
RETURNS TABLE AS RETURN
(
    SELECT 
        Split.a.value('let $n := . return count(../*[. << $n]) + 1', 'int') AS id
      , Split.a.value('.', 'NVARCHAR(MAX)')                                 AS value
    FROM
    (
        SELECT CAST('<X>'+REPLACE(@string, @delimiter, '</X><X>')+'</X>' AS XML) AS String
    ) AS a
    CROSS APPLY String.nodes('/X') AS Split(a)
)
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_bhxh_qt_chi_tiet]    Script Date: 4/11/2024 7:28:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_bhxh_qt_chi_tiet] 
	@IdDonVis nvarchar(max), 
	@NamLamViec int, 
	@NamNganSach int, 
	@NguonNganSach int, 
	@IQuy int, 
	@ILoaiQuy int
AS
BEGIN
	DECLARE @LCS float;
	SELECT @LCS = fGiaTri FROM BH_DM_CauHinhThamSo
	WHERE iNamLamViec = @NamLamViec AND sMa = 'LCS';

	SELECT		sXauNoiMa, iID_MaDonVi, sum(fSoNguoi) as fSoNguoi, sum(fTuChi_PheDuyet) as fTuChi_PheDuyet
	INTO		#tempQTDonVi 
	FROM		NS_QT_ChungTuChiTiet
	where		iNamLamViec = @NamLamViec
				and iNamNganSach = @NamNganSach
				and iID_MaNguonNganSach = @NguonNganSach
				and sXauNoiMa like '101%'
				and iThangQuy = @IQuy
				and iThangQuyLoai = @ILoaiQuy
				and iID_MaDonVi in (select * from f_split(@IdDonVis))
	group by	sXauNoiMa, iID_MaDonVi

	declare @cttemp uniqueidentifier
	set @cttemp = newid()

	CREATE TABLE #result(
				iID_QTT_BHXH_ChungTu_ChiTiet uniqueidentifier default newid(),
				iID_QTT_BHXH_ChungTu uniqueidentifier,				
				iID_MaDonVi nvarchar(50),
				sTenDonVi nvarchar(50),
				iQSBQNam int,
				fLuongChinh float,
				fPCChucVu float,
				fPCTNNghe float,
				fPCTNVuotKhung float,
				fNghiOm float,
				fHSBL float,
				fTongQTLN float,
				fDuToan float,
				fDaQuyetToan float,
				fConLai float,
				fThu_BHXH_NLD float,
				fThu_BHXH_NSD float,
				fTongSoPhaiThuBHXH float,
				fThu_BHYT_NLD float,
				fThu_BHYT_NSD float,
				fTongSoPhaiThuBHYT float,
				fThu_BHTN_NLD float,
				fThu_BHTN_NSD float,
				fTongSoPhaiThuBHTN float,
				fTongNLD float,
				fTongNSD float,
				fTongCong float,
				sGhiChu nvarchar(max),
				iID_MLNS uniqueidentifier,
				iID_MLNS_Cha uniqueidentifier,
				sXauNoiMa nvarchar(200),
				sLNS nvarchar(200),
				iNamLamViec int,
				LCS float
				);

	insert into #result(iID_QTT_BHXH_ChungTu,sXauNoiMa,iID_MLNS,iID_MLNS_Cha,iNamLamViec,iID_MaDonVi,sTenDonVi)
	select	@cttemp,ml.sXauNoiMa,ml.iID_MLNS,ml.iID_MLNS_Cha,@NamLamViec,dv.iID_MaDonVi, dv.sTenDonVi
	from	BH_DM_MucLucNganSach ml, DonVi dv
	where	ml.iNamLamViec = @NamLamViec
			and ml.bHangCha = 0
			and ml.sXauNoiMa like '9020002%'
			and dv.iNamLamViec = @NamLamViec
			and dv.iTrangThai = 1
			and dv.iID_MaDonVi in (select * from f_split(@IdDonVis))
			and dv.iKhoi = 1

	insert into #result(iID_QTT_BHXH_ChungTu,sXauNoiMa,iID_MLNS,iID_MLNS_Cha,iNamLamViec,iID_MaDonVi,sTenDonVi)
	select	@cttemp,ml.sXauNoiMa,ml.iID_MLNS,ml.iID_MLNS_Cha,@NamLamViec,dv.iID_MaDonVi, dv.sTenDonVi
	from	BH_DM_MucLucNganSach ml, DonVi dv
	where	ml.iNamLamViec = @NamLamViec
			and ml.bHangCha = 0
			and ml.sXauNoiMa like '9020001%'
			and dv.iNamLamViec = @NamLamViec
			and dv.iTrangThai = 1
			and dv.iID_MaDonVi in (select * from f_split(@IdDonVis))
			and dv.iKhoi <> 1
	
	create table #tempMap(	
				sXauNoiMaBH nvarchar(200),
				sXauNoiMaNS nvarchar(200),
				iType int,
				);

	insert into #tempMap(sXauNoiMaBH,iType,sXauNoiMaNS)
	select  sXauNoiMa, 1, ltrim(rtrim(value))
	from	BH_DM_MucLucNganSach 
			cross apply fn_split_string(sNS_LuongChinh,',')			
	where	iNamLamViec = @NamLamViec
			and bHangCha = 0
			and sXauNoiMa like '902%'
			and sNS_LuongChinh is not null
			and sNS_LuongChinh <> ''

	insert into #tempMap(sXauNoiMaBH,iType,sXauNoiMaNS)
	select  sXauNoiMa, 2, ltrim(rtrim(value))
	from	BH_DM_MucLucNganSach 
			cross apply fn_split_string(sNS_LuongChinh,',')			
	where	iNamLamViec = @NamLamViec
			and bHangCha = 0
			and sXauNoiMa like '902%'
			and sNS_LuongChinh is not null
			and sNS_LuongChinh <> ''

	insert into #tempMap(sXauNoiMaBH,iType,sXauNoiMaNS)
	select  sXauNoiMa, 3, ltrim(rtrim(value))
	from	BH_DM_MucLucNganSach 
			cross apply fn_split_string(sNS_PCCV,',')			
	where	iNamLamViec = @NamLamViec
			and bHangCha = 0
			and sXauNoiMa like '902%'
			and sNS_PCCV is not null
			and sNS_PCCV <> ''

	insert into #tempMap(sXauNoiMaBH,iType,sXauNoiMaNS)
	select  sXauNoiMa, 4, ltrim(rtrim(value))
	from	BH_DM_MucLucNganSach 
			cross apply fn_split_string(sNS_PCTN,',')			
	where	iNamLamViec = @NamLamViec
			and bHangCha = 0
			and sXauNoiMa like '902%'
			and sNS_PCTN is not null
			and sNS_PCTN <> ''

	insert into #tempMap(sXauNoiMaBH,iType,sXauNoiMaNS)
	select  sXauNoiMa, 5, ltrim(rtrim(value))
	from	BH_DM_MucLucNganSach 
			cross apply fn_split_string(sNS_PCTNVK,',')			
	where	iNamLamViec = @NamLamViec
			and bHangCha = 0
			and sXauNoiMa like '902%'
			and sNS_PCTNVK is not null
			and sNS_PCTNVK <> ''

	insert into #tempMap(sXauNoiMaBH,iType,sXauNoiMaNS)
	select  sXauNoiMa, 6, ltrim(rtrim(value))
	from	BH_DM_MucLucNganSach 
			cross apply fn_split_string(sNS_HSBL,',')		
	where	iNamLamViec = @NamLamViec
			and bHangCha = 0
			and sXauNoiMa like '902%'
			and sNS_HSBL is not null
			and sNS_HSBL <> ''

	create table #resultMap(	
				MaDonVi nvarchar(200),
				sXauNoiMaBH nvarchar(200),
				sXauNoiMaNS nvarchar(200),
				iType int,
				quanso int,
				money float
				);

	insert into	#resultMap (MaDonVi, sXauNoiMaBH, sXauNoiMaNS, iType)
	select	iID_MaDonVi, sXauNoiMaBH, sXauNoiMaNS, iType
	from	#result left join #tempMap on sXauNoiMaBH = sXauNoiMa
	where	sXauNoiMaBH is not null

	update #resultMap
	set		quanso = isnull((select top(1) fSoNguoi from #tempQTDonVi where #resultMap.MaDonVi = #tempQTDonVi.iID_MaDonVi and #resultMap.sXauNoiMaNS = #tempQTDonVi.sXauNoiMa),0),
			money = 0
	where	iType = 1

	update #resultMap
	set		quanso = 0,
			money = isnull((select top(1) sum(fTuChi_PheDuyet) from #tempQTDonVi where #resultMap.MaDonVi = #tempQTDonVi.iID_MaDonVi and #resultMap.sXauNoiMaNS = #tempQTDonVi.sXauNoiMa),0)
	where	iType <> 1

	update #result
	set	   iQSBQNam = (select sum(quanso) from #resultMap where #result.iID_MaDonVi = #resultMap.MaDonVi and #result.sXauNoiMa = #resultMap.sXauNoiMaBH),
		   fLuongChinh = (select sum(money) from #resultMap where #result.iID_MaDonVi = #resultMap.MaDonVi and #result.sXauNoiMa = #resultMap.sXauNoiMaBH and iType = 2),
		   fPCChucVu = (select sum(money) from #resultMap where #result.iID_MaDonVi = #resultMap.MaDonVi and #result.sXauNoiMa = #resultMap.sXauNoiMaBH and iType = 3),
		   fPCTNNghe = (select sum(money) from #resultMap where #result.iID_MaDonVi = #resultMap.MaDonVi and #result.sXauNoiMa = #resultMap.sXauNoiMaBH and iType = 4),
		   fPCTNVuotKhung = (select sum(money) from #resultMap where #result.iID_MaDonVi = #resultMap.MaDonVi and #result.sXauNoiMa = #resultMap.sXauNoiMaBH and iType = 5)

	select * from #result
END
;
;
;
GO
