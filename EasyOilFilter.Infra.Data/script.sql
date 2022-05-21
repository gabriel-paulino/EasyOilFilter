CREATE DATABASE EasyOilFilter;

CREATE TABLE Oil (
    Id uniqueidentifier primary key,
    Name varchar(100),
    Viscosity varchar(10),
    Price decimal(19,4),
	Type int,
	UnitOfMeasurement int,
	StockQuantity decimal(19,4)
);

CREATE TABLE Filter (
    Id uniqueidentifier primary key,
    Code varchar(100),
    Manufacturer varchar(15),
    Price decimal(19,4),
	Type int,
	StockQuantity decimal(19,4)
);