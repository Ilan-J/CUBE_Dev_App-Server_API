CREATE DATABASE IF NOT EXISTS `db_negosud`;
USE `db_negosud`;

CREATE TABLE IF NOT EXISTS `Supplier` (
    `pkSupplier` 	INT AUTO_INCREMENT,

    `name` 			VARCHAR(255) NOT NULL,
    `email` 		VARCHAR(255) NOT NULL,

    `address` 		VARCHAR(255) DEFAULT "",
    `city` 			VARCHAR(255) DEFAULT "",
    `postalCode` 	VARCHAR(5) DEFAULT "",

    PRIMARY KEY(`pkSupplier`),
    UNIQUE(`name`),
    UNIQUE(`email`)
);

CREATE TABLE IF NOT EXISTS `Client` (
    `pkClient` 		INT AUTO_INCREMENT,

    `email` 		VARCHAR(255) NOT NULL,
    `password` 		VARCHAR(255) NOT NULL,

    `firstname` 	VARCHAR(255) DEFAULT "",
    `lastname` 		VARCHAR(255) DEFAULT "",

    `address` 		VARCHAR(255) DEFAULT "",
    `city` 			VARCHAR(255) DEFAULT "",
	`region` 		VARCHAR(255) DEFAULT "",
    `postalCode` 	VARCHAR(5) DEFAULT "",
	`country` 		VARCHAR(255) DEFAULT "",

    PRIMARY KEY(`pkClient`),
	UNIQUE(`email`)
);

CREATE TABLE IF NOT EXISTS `WineFamily` (
    `pkWineFamily` 	INT AUTO_INCREMENT,

    `name`			VARCHAR(255) NOT NULL,

    PRIMARY KEY(`pkWineFamily`),
    UNIQUE(`name`)
);

CREATE TABLE IF NOT EXISTS `Product` (
    `pkProduct` 		INT AUTO_INCREMENT,

    `name` 				VARCHAR(255) NOT NULL,
    `reference` 		VARCHAR(255) NOT NULL,

    `price` 			FLOAT NOT NULL,
    `tva` 				INT NOT NULL,
    `priceTTC`			FLOAT NOT NULL,

    `age` 				INT DEFAULT 0,
    `description` 		TEXT,

    `stock` 			INT DEFAULT 0,
    `stockMin` 			INT DEFAULT 0,

    `fkSupplier` 		INT NOT NULL,
    `fkWineFamily` 		INT NOT NULL,

    PRIMARY KEY(`pkProduct`),
    UNIQUE(`name`),
    UNIQUE(`reference`),
    KEY `fkSupplier` 	(`fkSupplier`),
    KEY `fkWineFamily` 	(`fkWineFamily`),
    CONSTRAINT `product_ibfk_1` FOREIGN KEY(`fkSupplier`) 	REFERENCES `Supplier` 	(`pkSupplier`),
    CONSTRAINT `product_ibfk_2` FOREIGN KEY(`fkWineFamily`) REFERENCES `WineFamily` (`pkWineFamily`)
);

CREATE TABLE IF NOT EXISTS `SupplierCommand` (
    `pkSupplierCommand` INT AUTO_INCREMENT,

    `commandDate` 		TIMESTAMP DEFAULT NOW(),
    `commandType`		INT NOT NULL,
    `commandStatus`		INT NOT NULL,

    `totalCost` 		FLOAT NOT NULL,
    `transportCost` 	FLOAT DEFAULT 0,

    `fkSupplier` 		INT NOT NULL,

    PRIMARY KEY(`pkSupplierCommand`),
    KEY `fkSupplier` (`fkSupplier`),
    CONSTRAINT `supplierCommand_ibfk_1` FOREIGN KEY(`fkSupplier`) REFERENCES `Supplier` (`pkSupplier`)
);
CREATE TABLE IF NOT EXISTS `ClientCommand` (
    `pkClientCommand` 	INT AUTO_INCREMENT,

    `commandDate` 		TIMESTAMP DEFAULT NOW(),
    `commandStatus`		INT NOT NULL,

    `address` 			VARCHAR(255) NOT NULL,
    `city` 				VARCHAR(255) NOT NULL,
	`region` 			VARCHAR(255) NOT NULL,
    `postalCode` 		VARCHAR(5) NOT NULL,
	`country` 			VARCHAR(255) NOT NULL,
	
    `totalCost` 		FLOAT NOT NULL,
    `transportCost` 	FLOAT DEFAULT 0,

    `fkClient` 			INT NOT NULL,

    PRIMARY KEY(`pkClientCommand`),
    KEY `fkClient` (`fkClient`),
    CONSTRAINT `clientCommand_ibfk_1` FOREIGN KEY(`fkClient`) REFERENCES `Client` (`pkClient`)
);

CREATE TABLE IF NOT EXISTS `SupplierProductList` (
    `fkProduct` 		INT NOT NULL,
    `fkSupplierCommand` INT NOT NULL,

    `quantity` 			INT NOT NULL,

    KEY `fkProduct` 		(`fkProduct`),
    KEY `fkSupplierCommand` (`fkSupplierCommand`),
    CONSTRAINT `supplyList_ibfk_1` FOREIGN KEY(`fkProduct`) 		REFERENCES `Product` 			(`pkProduct`),
    CONSTRAINT `supplyList_ibfk_2` FOREIGN KEY(`fkSupplierCommand`) REFERENCES `SupplierCommand` 	(`pkSupplierCommand`)
);
CREATE TABLE IF NOT EXISTS `ClientProductList` (
    `fkClientCommand` 	INT NOT NULL,
    `fkProduct` 		INT NOT NULL,

    `quantity` 			INT NOT NULL,

    KEY `fkProduct` (`fkProduct`),
    KEY `fkClientCommand` (`fkClientCommand`),
    CONSTRAINT `purchaseList_ibfk_1` FOREIGN KEY (`fkProduct`) 			REFERENCES `Product` 		(`pkProduct`),
    CONSTRAINT `purchaseList_ibfk_2` FOREIGN KEY (`fkClientCommand`) 	REFERENCES `ClientCommand` 	(`pkClientCommand`)
);
