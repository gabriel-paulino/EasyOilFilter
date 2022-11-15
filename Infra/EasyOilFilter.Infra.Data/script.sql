CREATE DATABASE EasyOilFilter;

CREATE TABLE Product (
    Id uniqueidentifier primary key,
    Name varchar(100) not null,
    Viscosity varchar(10),
	Api varchar(10),
	Manufacturer varchar(30),
    DefaultPrice decimal(19,4) not null,
    AlternativePrice decimal(19,4),
	Type int not null,
	OilType int,
	FilterType int,
	DefaultUoM int not null,
	AlternativeUoM int,
	StockQuantity decimal(19,4) not null,
	HasAlternative bit
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
	ProductId uniqueidentifier FOREIGN KEY REFERENCES Product(Id) ON DELETE SET NULL,
	ItemDescription varchar(100),
	UnitOfMeasurement int not null,
	Quantity decimal(19,4) not null,
	UnitaryPrice decimal(19,4) not null,
	TotalItem decimal(19,4) not null
);

CREATE TABLE Purchase (
    Id uniqueidentifier primary key,
    Provider varchar(100) not null,
    Total decimal(19,4) not null,
	Date date not null,
	Remarks varchar(150),
	Status int not null
);

CREATE TABLE PurchaseItem (
    Id uniqueidentifier primary key,
    PurchaseId uniqueidentifier FOREIGN KEY REFERENCES Purchase(Id),
	ProductId uniqueidentifier FOREIGN KEY REFERENCES Product(Id) ON DELETE SET NULL,
	ItemDescription varchar(100),
	UnitOfMeasurement int not null,
	Quantity decimal(19,4) not null,
	UnitaryPrice decimal(19,4) not null,
	TotalItem decimal(19,4) not null
);

CREATE TABLE Payment (
    Id uniqueidentifier primary key,
    PurchaseId uniqueidentifier FOREIGN KEY REFERENCES Purchase(Id),
	PaymentDate date not null,
	AmountPaid decimal(19,4) not null,
	BankAccount int not null,
	Status int not null
);