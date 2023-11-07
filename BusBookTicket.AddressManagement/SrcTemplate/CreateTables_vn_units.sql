USE BusBookTicket

 DROP TABLE IF EXISTS wards_tmp;
 DROP TABLE IF EXISTS districts_tmp;
 DROP TABLE IF EXISTS provinces_tmp;
 DROP TABLE IF EXISTS administrative_units_tmp;
 DROP TABLE IF EXISTS administrative_regions_tmp;

-- CREATE administrative_regions_tmp TABLE
CREATE TABLE administrative_regions_tmp (
	id integer NOT NULL,
	name nvarchar(255) NOT NULL,
	name_en nvarchar(255) NOT NULL,
	code_name nvarchar(255) NULL,
	code_name_en nvarchar(255) NULL,
	CONSTRAINT administrative_regions_pkey PRIMARY KEY (id)
);


-- CREATE administrative_units_tmp TABLE
CREATE TABLE administrative_units_tmp (
	id integer NOT NULL,
	full_name nvarchar(255) NULL,
	full_name_en nvarchar(255) NULL,
	short_name nvarchar(255) NULL,
	short_name_en nvarchar(255) NULL,
	code_name nvarchar(255) NULL,
	code_name_en nvarchar(255) NULL,
	CONSTRAINT administrative_units_pkey PRIMARY KEY (id)
);


-- CREATE provinces_tmp TABLE
CREATE TABLE provinces_tmp (
	code nvarchar(20) NOT NULL,
	name nvarchar(255) NOT NULL,
	name_en nvarchar(255) NULL,
	full_name nvarchar(255) NOT NULL,
	full_name_en nvarchar(255) NULL,
	code_name nvarchar(255) NULL,
	administrative_unit_id integer NULL,
	administrative_region_id integer NULL,
	CONSTRAINT provinces_pkey PRIMARY KEY (code)
);


-- provinces_tmp foreign keys

ALTER TABLE provinces_tmp ADD CONSTRAINT provinces_administrative_region_id_fkey FOREIGN KEY (administrative_region_id) REFERENCES administrative_regions_tmp(id);
ALTER TABLE provinces_tmp ADD CONSTRAINT provinces_administrative_unit_id_fkey FOREIGN KEY (administrative_unit_id) REFERENCES administrative_units_tmp(id);

CREATE INDEX idx_provinces_region ON provinces_tmp(administrative_region_id);
CREATE INDEX idx_provinces_unit ON provinces_tmp(administrative_unit_id);


-- CREATE districts_tmp TABLE
CREATE TABLE districts_tmp (
	code nvarchar(20) NOT NULL,
	name nvarchar(255) NOT NULL,
	name_en nvarchar(255) NULL,
	full_name nvarchar(255) NULL,
	full_name_en nvarchar(255) NULL,
	code_name nvarchar(255) NULL,
	province_code nvarchar(20) NULL,
	administrative_unit_id integer NULL,
	CONSTRAINT districts_pkey PRIMARY KEY (code)
);


-- districts_tmp foreign keys

ALTER TABLE districts_tmp ADD CONSTRAINT districts_administrative_unit_id_fkey FOREIGN KEY (administrative_unit_id) REFERENCES administrative_units_tmp(id);
ALTER TABLE districts_tmp ADD CONSTRAINT districts_province_code_fkey FOREIGN KEY (province_code) REFERENCES provinces_tmp(code);

CREATE INDEX idx_districts_province ON districts_tmp(province_code);
CREATE INDEX idx_districts_unit ON districts_tmp(administrative_unit_id);



-- CREATE wards_tmp TABLE
CREATE TABLE wards_tmp (
	code nvarchar(20) NOT NULL,
	name nvarchar(255) NOT NULL,
	name_en nvarchar(255) NULL,
	full_name nvarchar(255) NULL,
	full_name_en nvarchar(255) NULL,
	code_name nvarchar(255) NULL,
	district_code nvarchar(20) NULL,
	administrative_unit_id integer NULL,
	CONSTRAINT wards_pkey PRIMARY KEY (code)
);


-- wards_tmp foreign keys

ALTER TABLE wards_tmp ADD CONSTRAINT wards_administrative_unit_id_fkey FOREIGN KEY (administrative_unit_id) REFERENCES administrative_units_tmp(id);
ALTER TABLE wards_tmp ADD CONSTRAINT wards_district_code_fkey FOREIGN KEY (district_code) REFERENCES districts_tmp(code);

CREATE INDEX idx_wards_district ON wards_tmp(district_code);
CREATE INDEX idx_wards_unit ON wards_tmp(administrative_unit_id);
