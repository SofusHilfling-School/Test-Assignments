DELETE FROM Customers;
DELETE FROM Employees;
DELETE FROM Bookings;


INSERT INTO Customers (firstname, lastname, birthdate, phoneNumber) VALUES 
    ('John', 'Doe', '1990-10-14', '12345678'),
    ('Bob', 'Johnson', '1990-4-6', '87654321'),
    ('Jane', 'Doe', '1990-6-30', '22446688'),
    ('Alice', 'Christoffersen', '1990-10-28', '11335599');

INSERT INTO Employees (firstname, lastname, birthdate) VALUES
    ('Richard', 'Roe', '1974-5-12'),
    ('Michell', 'Smith', '1995-2-2');

INSERT INTO Bookings (customerId, employeeId, date, start, end) VALUES
    (1, 2, '2020-7-23', TIME(TIME('00:00:00')), TIME(TIME('23:59:59'))),
    (1, 2, '2021-3-16', TIME('00:00:00'), TIME('23:59:59')),
    (4, 2, '2020-7-16', TIME('00:00:00'), TIME('23:59:59')),
    (3, 1, '2020-7-28', TIME('00:00:00'), TIME('23:59:59')),
    (3, 2, '2020-12-17', TIME('00:00:00'), TIME('23:59:59')),
    (2, 1, '2020-8-12', TIME('00:00:00'), TIME('23:59:59')),
    (2, 2, '2021-1-1', TIME('00:00:00'), TIME('23:59:59')),
    (2, 2, '2021-3-10', TIME('00:00:00'), TIME('23:59:59'));