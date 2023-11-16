USE BusBookTicket

-- Move data in temp to primary data, and handling format

-- Table AdministrativeRegions
SET IDENTITY_INSERT AdministrativeRegions ON;
INSERT INTO AdministrativeRegions (Id, Name, NameEnglish, CodeName, CodeNameEnglish, DateCreate, DateUpdate, UpdateBy, CreateBy, Status)
SELECT CAST (id as INT),name,name_en,code_name,code_name_en, GETDATE(), GETDATE(), -1, -1, 1
FROM administrative_regions_tmp
SET IDENTITY_INSERT AdministrativeRegions OFF;

-- Table AdministrativeUnits
SET IDENTITY_INSERT AdministrativeUnits ON;
INSERT INTO AdministrativeUnits (id, FullName, FullNameEnglish, CodeName, CodeNameEnglish, ShortName, ShortNameEnglish, DateCreate, DateUpdate, UpdateBy, CreateBy, Status)
SELECT CAST(id AS INT),full_name,full_name_en,code_name,code_name_en, short_name,short_name_en, GETDATE(), GETDATE(), -1, -1, 1
FROM administrative_units_tmp 
SET IDENTITY_INSERT AdministrativeUnits OFF;

-- Table Provinces
SET IDENTITY_INSERT Provinces ON;
INSERT INTO Provinces (id, FullName, FullNameEnglish, CodeName, Name, NameEnglish, AdministrativeRegionId, AdministrativeUnitId, DateCreate, DateUpdate, UpdateBy, CreateBy, Status)
SELECT CAST(code as INT),full_name,full_name_en,code_name,name,name_en,CAST(administrative_region_id as INT), CAST(administrative_unit_id as INT), GETDATE(), GETDATE(), -1, -1, 1
FROM provinces_tmp
SET IDENTITY_INSERT Provinces OFF;

-- Table Districts
SET IDENTITY_INSERT Districts ON;
INSERT INTO Districts (id, FullName, FullNameEnglish, CodeName, Name, NameEnglish, provinceId, AdministrativeUnitId, DateCreate, DateUpdate, UpdateBy, CreateBy, Status)
SELECT CAST(code as INT),full_name,full_name_en,code_name, name,name_en,CAST(province_code as INT),CAST(administrative_unit_id as INT), GETDATE(), GETDATE(), -1, -1, 1
FROM districts_tmp 
SET IDENTITY_INSERT Districts OFF;

-- Table Wards
SET IDENTITY_INSERT Wards ON;
INSERT INTO Wards(id, FullName, FullNameEnglish, CodeName, Name, NameEnglish, DistrictId, AdministrativeUnitId, DateCreate, DateUpdate, UpdateBy, CreateBy, Status)
SELECT CAST(code as INT),full_name,full_name_en,code_name,name,name_en,district_code,administrative_unit_id, GETDATE(), GETDATE(), -1, -1, 1
FROM wards_tmp 
SET IDENTITY_INSERT Wards OFF;

-- DROP table temp
DROP TABLE wards_tmp
DROP TABLE districts_tmp
DROP TABLE provinces_tmp
DROP TABLE administrative_units_tmp
DROP TABLE administrative_regions_tmp

-- End