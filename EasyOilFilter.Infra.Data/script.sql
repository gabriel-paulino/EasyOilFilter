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