
use Ecommerce_3;

go;



-- Create Categories Table with Parent-Child Relationship
CREATE TABLE Categories (
                            CategoryID INT IDENTITY(1,1) PRIMARY KEY,
                            ParentCategoryID INT NULL,
                            Name NVARCHAR(100) NOT NULL,
                            Description NVARCHAR(MAX),
                            FOREIGN KEY (ParentCategoryID) REFERENCES Categories(CategoryID) ON DELETE NO ACTION
);

-- Create Attributes Table
CREATE TABLE Attributes (
                            AttributeID INT IDENTITY(1,1) PRIMARY KEY,
                            Name NVARCHAR(100) NOT NULL
);

-- Create AttributeValues Table
CREATE TABLE AttributeValues (
                                 AttributeValueID INT IDENTITY(1,1) PRIMARY KEY,
                                 AttributeID INT NOT NULL,
                                 Value NVARCHAR(100) NOT NULL,
                                 FOREIGN KEY (AttributeID) REFERENCES Attributes(AttributeID) ON DELETE CASCADE
);

-- Create Products Table (without StockQuantity)
CREATE TABLE Products (
                          ProductID INT IDENTITY(1,1) PRIMARY KEY,
                          Name NVARCHAR(100) NOT NULL,
                          Description NVARCHAR(MAX),
                          Price DECIMAL(10, 2) NOT NULL
);

-- Create ProductAttributes Table to link products with their attributes
CREATE TABLE ProductAttributes (
                                   ProductID INT NOT NULL,
                                   AttributeValueID INT NOT NULL,
                                   PRIMARY KEY (ProductID, AttributeValueID),
                                   FOREIGN KEY (ProductID) REFERENCES Products(ProductID) ON DELETE CASCADE,
                                   FOREIGN KEY (AttributeValueID) REFERENCES AttributeValues(AttributeValueID) ON DELETE CASCADE
);



-- Create Warehouse Table to manage stock quantities separately
CREATE TABLE Warehouse (
                           WarehouseID INT IDENTITY(1,1) PRIMARY KEY,
                           ProductID INT NOT NULL,
                           StockQuantity INT NOT NULL,
                           LastUpdated DATETIME NOT NULL DEFAULT GETDATE(),
                           FOREIGN KEY (ProductID) REFERENCES Products(ProductID) ON DELETE CASCADE
);

-- Create Junction Table for Many-to-Many Relationship between Products and Categories
CREATE TABLE ProductCategories (
                                   ProductID INT,
                                   CategoryID INT,
                                   PRIMARY KEY (ProductID, CategoryID),
                                   FOREIGN KEY (ProductID) REFERENCES Products(ProductID) ON DELETE CASCADE,
                                   FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID) ON DELETE CASCADE
);

-- Create OrderStatuses Table to manage different statuses of orders
CREATE TABLE OrderStatuses (
                               StatusID INT IDENTITY(1,1) PRIMARY KEY,
                               StatusName NVARCHAR(50) NOT NULL
);

-- Insert default order statuses
INSERT INTO OrderStatuses (StatusName) VALUES ('Pending'), ('Processing'), ('Shipped'), ('Delivered'), ('Cancelled');



-- Создание таблицы Orders без внешних ключей
CREATE TABLE Orders (
                        OrderID INT IDENTITY(1,1) PRIMARY KEY,
                        UserNameRef nvarchar(50) NOT NULL,
                        OrderDate DATETIME DEFAULT GETDATE(),
                        TotalAmount DECIMAL(10, 2) NOT NULL,
                        StatusID INT NOT NULL
);


-- Create OrderItems Table to link Products and Orders (Many-to-Many Relationship)
CREATE TABLE OrderItems (
                            OrderItemID INT IDENTITY(1,1) PRIMARY KEY,
                            OrderID INT NOT NULL,
                            ProductID INT NOT NULL,
                            Quantity INT NOT NULL,
                            UnitPrice DECIMAL(10, 2) NOT NULL,
                            FOREIGN KEY (OrderID) REFERENCES Orders(OrderID) ON DELETE CASCADE,
                            FOREIGN KEY (ProductID) REFERENCES Products(ProductID) ON DELETE CASCADE
);



-- Триггер для проверки существования пользователя
CREATE TRIGGER trg_CheckUserExists
    ON dbo.Orders
    AFTER INSERT, UPDATE
    AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM inserted i
                 LEFT JOIN Auth_3.dbo.Users u ON i.UserNameRef = u.userName
        WHERE u.userName IS NULL
    )
        BEGIN
            RAISERROR('Ошибка целостности данных: UserID не существует в Auth_3.dbo.Users.', 16, 1);
            ROLLBACK TRANSACTION;
        END
END;

-- Триггер для проверки существования статуса заказа
    CREATE TRIGGER trg_CheckOrderStatusExists
        ON dbo.Orders
        AFTER INSERT, UPDATE
        AS
    BEGIN
        IF EXISTS (
            SELECT 1
            FROM inserted i
                     LEFT JOIN dbo.OrderStatuses os ON i.StatusID = os.StatusID
            WHERE os.StatusID IS NULL
        )
            BEGIN
                RAISERROR('Ошибка целостности данных: StatusID не существует в dbo.OrderStatuses.', 16, 1);
                ROLLBACK TRANSACTION;
            END
    END;

-- Триггер для проверки существования товара в заказе
        CREATE TRIGGER trg_CheckProductExists
            ON dbo.OrderItems
            AFTER INSERT, UPDATE
            AS
        BEGIN
            IF EXISTS (
                SELECT 1
                FROM inserted i
                         LEFT JOIN dbo.Products p ON i.ProductID = p.ProductID
                WHERE p.ProductID IS NULL
            )
                BEGIN
                    RAISERROR('Ошибка целостности данных: ProductID не существует в dbo.Products.', 16, 1);
                    ROLLBACK TRANSACTION;
                END
        END;


alter table Orders
add constraint FK_Orders_Status
foreign key (StatusID) references OrderStatuses(StatusID);


CREATE TRIGGER trg_AddParentCategoryToProduct
ON dbo.ProductCategories
AFTER INSERT
AS
BEGIN
    WITH CategoryHierarchy AS
    (
        -- Базовый уровень (только что вставленные категории)
        SELECT i.ProductID, c.CategoryID, c.ParentCategoryID
        FROM inserted i
        JOIN Categories c ON i.CategoryID = c.CategoryID

        UNION ALL

        -- Рекурсивно добавляем родительские категории
        SELECT ch.ProductID, c.CategoryID, c.ParentCategoryID
        FROM CategoryHierarchy ch
        JOIN Categories c ON ch.ParentCategoryID = c.CategoryID
        WHERE ch.ParentCategoryID IS NOT NULL
    )
    INSERT INTO ProductCategories (ProductID, CategoryID)
    SELECT ProductID, ParentCategoryID
    FROM CategoryHierarchy
    WHERE ParentCategoryID IS NOT NULL;
END;


CREATE TRIGGER trg_AddParentCategoryToProduct
ON dbo.ProductCategories
AFTER INSERT
AS
BEGIN
    DECLARE @ProductID INT, @CategoryID INT, @ParentCategoryID INT;

    -- Начальная установка значений
    SELECT TOP 1 @ProductID = i.ProductID, @CategoryID = i.CategoryID
    FROM inserted i;

    -- Получаем первого родителя
    SELECT @ParentCategoryID = ParentCategoryID
    FROM Categories
    WHERE CategoryID = @CategoryID;

    -- Запускаем цикл для добавления всех родительских категорий
    WHILE @ParentCategoryID IS NOT NULL
    BEGIN
        -- Вставляем родительскую категорию
        INSERT INTO ProductCategories (ProductID, CategoryID)
        VALUES (@ProductID, @ParentCategoryID);

        -- Получаем следующего родителя
        SELECT @ParentCategoryID = ParentCategoryID
        FROM Categories
        WHERE CategoryID = @ParentCategoryID;
    END
END;
GO

