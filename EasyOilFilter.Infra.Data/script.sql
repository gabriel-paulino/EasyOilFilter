CREATE DATABASE EasyOilFilter;

CREATE TABLE Product (
    Id uniqueidentifier primary key,
    Name varchar(100) not null,
    Viscosity varchar(10),
	Manufacturer varchar(15),
    Price decimal(19,4) not null,
	Type int not null,
	OilType int,
	FilterType int,
	UnitOfMeasurement int not null,
	StockQuantity decimal(19,4) not null
);

CREATE TABLE Sale (
    Id uniqueidentifier primary key,
    Description varchar(50) not null,
	PaymentMethod int not null,
    Total decimal(19,4) not null,
	Discount decimal(19,4),
	Date date not null,
	Time int not null,
	Remarks varchar(150),
	Status int not null
);

CREATE TABLE SaleItem (
    Id uniqueidentifier primary key,
    SaleId uniqueidentifier FOREIGN KEY REFERENCES Sale(Id),
	ProductId uniqueidentifier FOREIGN KEY REFERENCES Product(Id),
	ItemDescription varchar(100),
	UnitOfMeasurement int not null,
	Quantity decimal(19,4) not null,
	UnitaryPrice decimal(19,4) not null,
	TotalItem decimal(19,4) not null
);