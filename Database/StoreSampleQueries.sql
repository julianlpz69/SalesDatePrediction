-- =============================================
-- 1. Sales Date Prediction
-- =============================================

WITH OrderDifferences AS (
    SELECT 
        C.CustID,
        C.CompanyName,
        O.OrderDate,
        LAG(O.OrderDate) OVER (PARTITION BY C.CustID ORDER BY O.OrderDate) AS PrevOrderDate
    FROM Sales.Customers C
    JOIN Sales.Orders O ON C.CustID = O.CustID
)
SELECT 
    CompanyName AS [Customer Name],
    MAX(OrderDate) AS [Last Order Date],
    DATEADD(DAY, 
        AVG(DATEDIFF(DAY, PrevOrderDate, OrderDate)), 
        MAX(OrderDate)
    ) AS [Next Predicted Order]
FROM OrderDifferences
WHERE PrevOrderDate IS NOT NULL
GROUP BY CompanyName, CustID
HAVING COUNT(*) > 1;

-- =============================================
-- 2. Get Client Orders
-- =============================================

SELECT 
    OrderID,
    RequiredDate,
    ShippedDate,
    ShipName,
    ShipAddress,
    ShipCity
FROM Sales.Orders
WHERE CustID = @CustomerId;

-- =============================================
-- 3. Get Employees
-- =============================================

SELECT 
    EmpID,
    FirstName + ' ' + LastName AS FullName
FROM HR.Employees;

-- =============================================
-- 4. Get Shippers
-- =============================================

SELECT 
    ShipperID,
    CompanyName
FROM Sales.Shippers;

-- =============================================
-- 5. Get Products
-- =============================================

SELECT 
    ProductID,
    ProductName
FROM Production.Products;

-- =============================================
-- 6. Add New Order
-- =============================================

-- Variable declarations for manual execution (in production they would be parameters)
DECLARE @OrderID INT;
DECLARE @EmpID INT = 1;
DECLARE @ShipperID INT = 1;
DECLARE @ShipName NVARCHAR(100) = 'Technical Order';
DECLARE @ShipAddress NVARCHAR(100) = '123 Example Street';
DECLARE @ShipCity NVARCHAR(50) = 'CityX';
DECLARE @OrderDate DATE = GETDATE();
DECLARE @RequiredDate DATE = DATEADD(DAY, 7, GETDATE());
DECLARE @ShippedDate DATE = DATEADD(DAY, 2, GETDATE());
DECLARE @Freight MONEY = 30.00;
DECLARE @ShipCountry NVARCHAR(50) = 'CountryX';

-- Product details
DECLARE @ProductID INT = 1;
DECLARE @UnitPrice MONEY = 15.50;
DECLARE @Qty SMALLINT = 2;
DECLARE @Discount FLOAT = 0.1;

-- Note: CustomerID is omitted since the technical test does not require it.
-- If your database has it as NOT NULL, you must add it.

-- Insert into Orders
INSERT INTO Sales.Orders (
    EmpID, ShipperID, ShipName, ShipAddress, ShipCity, 
    OrderDate, RequiredDate, ShippedDate, Freight, ShipCountry
) VALUES (
    @EmpID, @ShipperID, @ShipName, @ShipAddress, @ShipCity,
    @OrderDate, @RequiredDate, @ShippedDate, @Freight, @ShipCountry
);

-- Get the newly created OrderID
SET @OrderID = SCOPE_IDENTITY();

-- Insert into OrderDetails
INSERT INTO Sales.OrderDetails (
    OrderID, ProductID, UnitPrice, Qty, Discount
) VALUES (
    @OrderID, @ProductID, @UnitPrice, @Qty, @Discount
);