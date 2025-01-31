-- UPDATE Products SET ImageUri = '/images/headphone.jpg' WHERE Name LIKE '%Noise%';
-- UPDATE Products SET ImageUri = '/images/tent.jpg' WHERE Name = 'Camping Tent';
-- UPDATE Products SET ImageUri = '/images/air.jpg' WHERE Name = 'Air Purifier';
-- UPDATE Products SET ImageUri = '/images/bike.jpg' WHERE Name = 'Mountain Bike';
-- UPDATE Products SET ImageUri = '/images/water.jpg' WHERE Name = 'Water Bottle';
-- UPDATE Products SET ImageUri = '/images/jacket.jpg' WHERE Name = 'Winter Jacket';
-- UPDATE Products SET ImageUri = '/images/shoe.jpg' WHERE Name = 'Running Shoe';
-- UPDATE Products SET ImageUri = '/images/watch.jpg' WHERE Name = 'Smartwatch';
-- UPDATE Products SET ImageUri = '/images/backpack.jpg' WHERE Name = 'Hiking Backpack';
-- UPDATE Products SET ImageUri = '/images/vacuum.jpg' WHERE Name = 'Vacuum Cleaner';
-- UPDATE Products SET ImageUri = '/images/tv.jpg' WHERE Name = '4K Television';
-- UPDATE Products SET ImageUri = '/images/desk.jpg' WHERE Name = 'Office Chair';
-- .tables
-- SELECT *FROM Products;
-- SELECT * FROM Orders;
-- DELETE FROM Orders WHERE UserId="07086e6c-0592-4679-9ed6-e41260738a80";
-- INSERT INTO Orders (Id,UserId, ProductId, Quantity, TotalPrice, OrderDate) VALUES (1,'07086e6c-0592-4679-9ed6-e41260738a80', 1, 10, 199.99, '2021-01-01');
--UPDATE AspNetUsers SET EmailConfirmed=1 WHERE Email="ivysong678@outlook.com";
-- SELECT * FROM AspNetUsers;
-- Drop TABLE Orders;
-- CREATE TABLE Orders (
--     Id INTEGER PRIMARY KEY AUTOINCREMENT,
--     OrderDate DATETIME,
--     UserId TEXT
-- );
CREATE TABLE OrderDetails (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    OrderId INTEGER,
    ProductId INTEGER,
    Quantity INTEGER,
    TotalPrice DECIMAL(12, 2),
    FOREIGN KEY (OrderId) REFERENCES Orders(Id),
    FOREIGN KEY (ProductId) REFERENCES Products(Id)
);