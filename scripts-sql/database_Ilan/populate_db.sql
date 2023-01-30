USE `db_negosud`;

INSERT INTO `Supplier` (
    `name`,
    `email`,
    `address`,
    `city`,
    `postalCode`)
VALUES
    ('Domaine Tariquet',        'tariquet@localhost',   'Château du Tariquet', 'Eauze',    '32800'),
    ('Domaine de Pellehaut',    'pellehaut@localhost',  'Pellehaut',           'Montréal', '32250'),
    ('Domaine de Joÿ',          'joy@localhost',        'Lieu dit Joy, D33',   'Panjas',   '32110'),
    ('Maison Fontan',           'fontan@localhost',     'Maubet',              'Noulens',  '32800'),
    ('Domaine Uby',             'uby@localhost',        'UBY',                 'Cazaubon', '32150');

INSERT INTO `WineFamily` (`name`)
VALUES
    ('Blanc'),
    ('Rosé'),
    ('Rouge');

INSERT INTO `Product` (
    `name`,
    `reference`,
    `price`,
    `tva`,
    `priceTTC`,
    `description`,
    `age`,
    `stock`,
    `stockMin`,
    `fkSupplier`,
    `fkWineFamily`)
VALUES
    (
        'Chardonnay',
        'TA06',
        24.99,
        20,
        29.988,
        "",
        2,
        57,
        15,
        1,
        1
    ),
    (
        'Rosé de pressée',
        'TA13',
        29.99,
        20,
        35.988,
        "",
        '3',
        33,
        10,
        1,
        2
    ),
    (
        'Les Marcottes',
        'PE03ELV',
        14.00,
        20,
        16.8,
        "",
        '4',
        69,
        20,
        2,
        3
    );

INSERT INTO `SupplierCommand` (
	`commandType`,
    `commandStatus`,
	`totalCost`,
    `transportCost`,
    `fkSupplier`
    )
VALUES
	(0,	0,	1849.3,	5.25,	1),
    (0,	0,	420,	8.3,	2);

INSERT INTO `SupplierProductList` (
	`fkProduct`,
    `fkSupplierCommand`,
    `quantity`
    )
VALUES
	(1, 1, 50),
    (2, 1, 20),
    (3, 2, 30);

INSERT INTO `Client` (
    `email`,
    `password`,
    `firstname`,
    `lastname`)
VALUES
    (
        'p.balkany@localhost',
        MD5('1234'),
        'Patrick',
        'Balkany'
    );

INSERT INTO `ClientCommand` (
    `commandStatus`,

    `address`,
    `city`,
	`region`,
    `postalCode`,
	`country`,
	
    `totalCost`,
    `transportCost`,

    `fkClient`
    )
VALUES
	(2,	"35 Rue de la Détente",	"Levallois Perret",	"Île-de-France",	"92300",	"France",	29.99,	15,	1),
    (0,	"35 Rue du Stress",		"Levallois Perret",	"Île-de-France",	"92300",	"France",	149.95,	20,	1);

INSERT INTO `ClientProductList` (
    `fkClientCommand`,
	`fkProduct`,
    `quantity`
    )
VALUES
	(1, 2, 1),
    (2, 2, 5);