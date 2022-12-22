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
        "",
        '4',
        69,
        20,
        2,
        3
    );

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