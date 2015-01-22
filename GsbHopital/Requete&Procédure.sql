CREATE TABLE Customer

(

CustomerID INT IDENTITY (1,1) NOT NULL,

CustomerName NVARCHAR (40) NOT NULL,

YTDOrders INT NOT NULL,

YTDSales INT NOT NULL,

);

 

CREATE TABLE Orders

(

OrderID INT IDENTITY (1,1) NOT NULL,

CustomerID INT NOT NULL,

OrderDate DATE NOT NULL,

FilledDate DATE NULL,

Status CHAR (1) NOT NULL,

Amount INT NOT NULL,

);

 ALTER TABLE Customer

ADD CONSTRAINT PK_Customer PRIMARY KEY (CustomerID);

ALTER TABLE Orders

ADD CONSTRAINT PK_Orders PRIMARY KEY (OrderID)

ALTER TABLE Orders

ADD CONSTRAINT FK_Orders_Customer FOREIGN KEY (CustomerID)

            REFERENCES Customer (CustomerID);

ALTER TABLE Customer

            ADD CONSTRAINT DEF_Customer_YTDOrders DEFAULT 0 FOR YTDOrders ;

 

ALTER TABLE Customer

            ADD CONSTRAINT DEF_Customer_YTDSales DEFAULT 0 FOR YTDSales ;

			CREATE PROCEDURE uspCancelOrder 

       @OrderID INT

AS

 

BEGIN

       DECLARE @Montant INT

       DECLARE @CustomerID INT

       DECLARE @ERR INT

 

       SET @ERR = 0

BEGIN TRANSACTION

 

       SELECT @Montant = Amount, @CustomerID = CustomerID

       FROM Orders WHERE OrderID = @OrderID;

 

       UPDATE Orders SET Status = 'X' WHERE OrderID = @OrderID

       SET @ERR=@ERR + @@ERROR;

 

       UPDATE Customer SET YTDOrders = YTDOrders - @Montant WHERE CustomerID = @CustomerID

       SET @ERR=@ERR + @@ERROR

 

       IF @ERR=0

             COMMIT TRANSACTION

       ELSE

             ROLLBACK TRANSACTION

END

CREATE PROCEDURE uspFillOrder  
@OrderID INT,
@FilledDate Date
AS

BEGIN 
DECLARE @Montant INT
DECLARE @CustomerID INT
DECLARE @ERR INT

SET @ERR = 0
BEGIN TRANSACTION

SELECT @Montant = Amount, @CustomerID = CustomerID 
FROM Orders	WHERE OrderID = @OrderID

UPDATE Orders SET FilledDate = @FilledDate WHERE OrderID = @OrderID
SET @ERR=@ERR + @@ERROR

UPDATE Orders SET Status = 'F' WHERE OrderID = @OrderID
SET @ERR=@ERR + @@ERROR

UPDATE Customer SET YTDSales = YTDSales + @Montant WHERE CustomerID = @CustomerID
SET @ERR=@ERR + @@ERROR

IF @ERR=0
COMMIT TRANSACTION
ELSE
ROLLBACK TRANSACTION
END

CREATE PROCEDURE uspNewCustomer 
@CustomerName NVARCHAR (40),
@CustomerID INT OUTPUT

AS
BEGIN
INSERT INTO Customer(CustomerName) VALUES (@CustomerName);
SET @CustomerID = SCOPE_IDENTITY();
RETURN @@ERROR
END

DECLARE @CustomerID INT;
exec uspNewCustomer 'Hotel St Jacques', @CustomerID OUTPUT;
print @CustomerID





CREATE PROCEDURE uspPlaceNewOrders @CustomerID int,
									@Amount int,
									@OrderDate date,
									@Status char(1) as

									BEGIN

									IF @Status = NULL
									SET @Status = 'O';

									-- Procédure uspPlaceNewOrders
									INSERT INTO Orders (CustomerID , OrderDate, Status, Amount) VALUES (@CustomerID, @OrderDate, @Status, @Amount);
									-- Fin de la Procédure uspPlaceNewOrders
									END;
CREATE PROCEDURE uspShowOrderDetails @CustomerID int as
BEGIN
SELECT Customer.CustomerName, OrderDate, FilledDate, Status, Amount FROM Orders
INNER JOIN Customer ON Orders.CustomerID = Customer.CustomerID
WHERE Orders.CustomerID = @CustomerID;
END;
		
		CREATE PROCEDURE getOrdersById @OrderID int  
AS
BEGIN
SELECT OrderId, Orders.CustomerID, FilledDate, Status, Amount FROM Orders WHERE OrderID = @OrderID
END;


							exec uspPlaceNewOrders 1, 1500, '10/12/2013', 'F';
									exec uspPlaceNewOrders 1, 200, '14/06/2013';
									exec uspShowOrderDetails 1;