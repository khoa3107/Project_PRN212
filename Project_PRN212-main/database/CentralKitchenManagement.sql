CREATE DATABASE CentralKitchenManagement;
GO

USE CentralKitchenManagement;
GO

-- 1. Stores
CREATE TABLE Stores (
    StoreId INT PRIMARY KEY IDENTITY(1,1),
    StoreName NVARCHAR(100) NOT NULL,
    Address NVARCHAR(255) NOT NULL,
    Phone NVARCHAR(20),
    ManagerName NVARCHAR(100),
    Status NVARCHAR(50) NOT NULL DEFAULT N'Active'
);
GO

-- 2. Users
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(100) NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
    Role NVARCHAR(20) NOT NULL
        CHECK (Role IN ('admin', 'kitchen', 'store')),
    StoreId INT NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT N'Active',
    FOREIGN KEY (StoreId) REFERENCES Stores(StoreId)
);
GO

-- 3. Ingredients
CREATE TABLE Ingredients (
    IngredientId INT PRIMARY KEY IDENTITY(1,1),
    IngredientName NVARCHAR(100) NOT NULL,
    Unit NVARCHAR(50) NOT NULL,
    QuantityInStock DECIMAL(10,2) NOT NULL DEFAULT 0,
    ImportPrice DECIMAL(18,2) NULL,
    LastUpdated DATETIME NOT NULL DEFAULT GETDATE(),
    Status NVARCHAR(20) NOT NULL DEFAULT N'Available'
);
GO

-- 4. ProductionPlans
CREATE TABLE ProductionPlans (
    PlanId INT PRIMARY KEY IDENTITY(1,1),
    PlanName NVARCHAR(100) NOT NULL,
    DishName NVARCHAR(100) NOT NULL,
    PlannedQuantity INT NOT NULL,
    ProductionDate DATE NOT NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT N'Planned'
        CHECK (Status IN ('Planned', 'InProgress', 'Completed', 'Cancelled')),
    CreatedBy INT NOT NULL,
    Note NVARCHAR(255),
    FOREIGN KEY (CreatedBy) REFERENCES Users(UserId)
);
GO

-- 5. ProductionPlanDetails
CREATE TABLE ProductionPlanDetails (
    PlanDetailId INT PRIMARY KEY IDENTITY(1,1),
    PlanId INT NOT NULL,
    IngredientId INT NOT NULL,
    RequiredQuantity DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (PlanId) REFERENCES ProductionPlans(PlanId),
    FOREIGN KEY (IngredientId) REFERENCES Ingredients(IngredientId)
);
GO

-- 6. FinishedDishes
CREATE TABLE FinishedDishes (
    DishId INT PRIMARY KEY IDENTITY(1,1),
    PlanId INT NOT NULL,
    DishName NVARCHAR(100) NOT NULL,
    ProducedQuantity INT NOT NULL,
    Unit NVARCHAR(50) NOT NULL DEFAULT N'portion',
    ProductionDate DATE NOT NULL,
    ExpiryDate DATE NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT N'Available'
        CHECK (Status IN ('Available', 'Shipped', 'Expired')),
    FOREIGN KEY (PlanId) REFERENCES ProductionPlans(PlanId)
);
GO

-- 7. Shipments
CREATE TABLE Shipments (
    ShipmentId INT PRIMARY KEY IDENTITY(1,1),
    StoreId INT NOT NULL,
    ShipmentDate DATE NOT NULL,
    CreatedBy INT NOT NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT N'Pending'
        CHECK (Status IN ('Pending', 'Shipping', 'Received', 'Cancelled')),
    Note NVARCHAR(255),
    FOREIGN KEY (StoreId) REFERENCES Stores(StoreId),
    FOREIGN KEY (CreatedBy) REFERENCES Users(UserId)
);
GO

-- 8. ShipmentDetails
CREATE TABLE ShipmentDetails (
    ShipmentDetailId INT PRIMARY KEY IDENTITY(1,1),
    ShipmentId INT NOT NULL,
    DishId INT NOT NULL,
    Quantity INT NOT NULL,
    FOREIGN KEY (ShipmentId) REFERENCES Shipments(ShipmentId),
    FOREIGN KEY (DishId) REFERENCES FinishedDishes(DishId)
);
GO

-- 9. StoreReceipts
CREATE TABLE StoreReceipts (
    ReceiptId INT PRIMARY KEY IDENTITY(1,1),
    ShipmentId INT NOT NULL UNIQUE,
    ReceivedBy INT NOT NULL,
    ReceivedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ReceiptStatus NVARCHAR(20) NOT NULL DEFAULT N'Received'
        CHECK (ReceiptStatus IN ('Received', 'Rejected')),
    Note NVARCHAR(255),
    FOREIGN KEY (ShipmentId) REFERENCES Shipments(ShipmentId),
    FOREIGN KEY (ReceivedBy) REFERENCES Users(UserId)
);
GO

-- DEMO DATA

-- Stores
INSERT INTO Stores (StoreName, Address, Phone, ManagerName)
VALUES
(N'Franchise Thu Duc', N'Thu Duc, TP.HCM', '0901111111', N'Nguyen Van A'),
(N'Franchise Go Vap', N'Go Vap, TP.HCM', '0902222222', N'Tran Thi B'),
(N'Franchise District 1', N'Quan 1, TP.HCM', '0903333333', N'Le Van C'),
(N'Franchise Binh Thanh', N'Binh Thanh, TP.HCM', '0904444444', N'Pham Thi D');
GO

-- Users
INSERT INTO Users (Username, Password, FullName, Role, StoreId)
VALUES
('admin', '123', N'System Admin', 'admin', NULL),
('kitchen01', '123', N'Kitchen Staff 01', 'kitchen', NULL),
('kitchen02', '123', N'Kitchen Staff 02', 'kitchen', NULL),
('store_thuduc', '123', N'Store User Thu Duc', 'store', 1),
('store_govap', '123', N'Store User Go Vap', 'store', 2),
('store_q1', '123', N'Store User Q1', 'store', 3),
('store_bt', '123', N'Store User Binh Thanh', 'store', 4);
GO

-- Ingredients
INSERT INTO Ingredients (IngredientName, Unit, QuantityInStock, ImportPrice)
VALUES
(N'Thịt gà', N'kg', 100, 120000),
(N'Rau xà lách', N'kg', 50, 40000),
(N'Bột chiên giòn', N'kg', 30, 35000),
(N'Sốt mayonnaise', N'lít', 20, 60000),
(N'Cơm trắng', N'kg', 80, 20000),
(N'Dưa leo', N'kg', 40, 15000),
(N'Trứng gà', N'quả', 200, 3000),
(N'Nước tương', N'lít', 25, 50000);
GO

-- ProductionPlans
INSERT INTO ProductionPlans (PlanName, DishName, PlannedQuantity, ProductionDate, Status, CreatedBy, Note)
VALUES
(N'Kế hoạch sáng 1', N'Gà rán', 100, '2026-03-26', 'Planned', 2, N'Sản xuất ca sáng'),
(N'Kế hoạch chiều 1', N'Salad gà', 50, '2026-03-26', 'Planned', 2, N'Sản xuất ca chiều'),
(N'Kế hoạch tối 1', N'Cơm gà', 80, '2026-03-27', 'Planned', 2, N'Sản xuất ca tối'),
(N'Kế hoạch sáng 2', N'Gà sốt', 60, '2026-03-27', 'Planned', 3, N'Sản xuất ca sáng');
GO

-- IngredientId:
-- 1: Thịt gà
-- 2: Rau xà lách
-- 3: Bột chiên giòn
-- 4: Sốt mayonnaise
-- 5: Cơm trắng
-- 6: Dưa leo
-- 7: Trứng gà
-- 8: Nước tương

-- ProductionPlanDetails
INSERT INTO ProductionPlanDetails (PlanId, IngredientId, RequiredQuantity)
VALUES
(1, 1, 25),-- PlanId=1 (ProductionPlans: Gà rán), IngredientId=1 (Ingredients: Thịt gà), cần 25
(1, 3, 10),-- PlanId=1, IngredientId=3 (Bột chiên), cần 10

(2, 1, 10), -- PlanId=2 (Salad gà), IngredientId=1 (Thịt gà), cần 10
(2, 2, 8),  -- PlanId=2, IngredientId=2 (Rau xà lách), cần 8
(2, 4, 3),  -- PlanId=2, IngredientId=4 (Sốt mayonnaise), cần 3

(3, 1, 20), -- PlanId=3 (Cơm gà), IngredientId=1 (Thịt gà), cần 20
(3, 5, 30), -- PlanId=3, IngredientId=5 (Cơm trắng), cần 30
(3, 6, 10), -- PlanId=3, IngredientId=6 (Dưa leo), cần 10

(4, 1, 15), -- PlanId=4 (Gà sốt), IngredientId=1 (Thịt gà), cần 15
(4, 8, 5);  -- PlanId=4, IngredientId=8 (Nước tương), cần 5
GO

-- FinishedDishes
INSERT INTO FinishedDishes (PlanId, DishName, ProducedQuantity, Unit, ProductionDate, ExpiryDate, Status)
VALUES
(1, N'Gà rán', 100, N'phần', '2026-03-26', '2026-03-27', 'Available'),
(2, N'Salad gà', 50, N'phần', '2026-03-26', '2026-03-27', 'Available'),
(3, N'Cơm gà', 80, N'phần', '2026-03-27', '2026-03-28', 'Available'),
(4, N'Gà sốt', 60, N'phần', '2026-03-27', '2026-03-28', 'Available');
GO

-- Shipments
INSERT INTO Shipments (StoreId, ShipmentDate, CreatedBy, Status, Note)
VALUES
(1, '2026-03-26', 2, 'Shipping', N'Giao ca chiều'),
(2, '2026-03-26', 2, 'Pending', N'Chờ xuất kho'),
(3, '2026-03-27', 2, 'Shipping', N'Giao buổi tối'),
(4, '2026-03-27', 3, 'Pending', N'Chờ giao hàng'),
(1, '2026-03-27', 2, 'Received', N'Đã giao xong');
GO

-- DishId:
-- 1: Gà rán
-- 2: Salad gà
-- 3: Cơm gà
-- 4: Gà sốt

-- ShipmentDetails
INSERT INTO ShipmentDetails (ShipmentId, DishId, Quantity)
VALUES
(1, 1, 40), -- ShipmentId=1 (Shipments: store 1), DishId=1 (Gà rán), gửi 40 phần
(1, 2, 20), -- ShipmentId=1, DishId=2 (Salad gà), gửi 20 phần

(2, 1, 30), -- ShipmentId=2 (store 2), DishId=1 (Gà rán), gửi 30 phần

(3, 3, 50), -- ShipmentId=3 (store 3), DishId=3 (Cơm gà), gửi 50 phần
(3, 4, 20), -- ShipmentId=3, DishId=4 (Gà sốt), gửi 20 phần

(4, 4, 30), -- ShipmentId=4 (store 4), DishId=4 (Gà sốt), gửi 30 phần

(5, 1, 20); -- ShipmentId=5 (store 1), DishId=1 (Gà rán), gửi 20 phần
GO

-- StoreReceipts
INSERT INTO StoreReceipts (ShipmentId, ReceivedBy, ReceiptStatus, Note)
VALUES
(1, 4, 'Received', N'Đã nhận đủ hàng'),
(5, 4, 'Received', N'Nhận đủ hàng'),
(3, 6, 'Received', N'Đã kiểm tra hàng');
GO