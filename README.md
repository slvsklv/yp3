 CREATE DATABASE skladb

-- Создание таблицы с ролями
CREATE TABLE Role (
    ID INT IDENTITY (1,1) PRIMARY KEY,
    Name CHAR(100),
    Description CHAR(200)
);

-- Создание таблицы пользователей
CREATE TABLE Users (
    ID INT IDENTITY (1,1) PRIMARY KEY,
    Login CHAR(100),
    Password CHAR(200),
    Role INT,
    FOREIGN KEY (Role) REFERENCES Role(ID)
);

-- Создание таблицы с поставщиками
CREATE TABLE Supplier (
    ID INT IDENTITY (1,1) PRIMARY KEY,
    Name CHAR(100),
    Users INT,
    FOREIGN KEY (Users) REFERENCES Users(ID)
);

-- Создание таблицы с поставками
CREATE TABLE Supply (
    ID INT IDENTITY (1,1) PRIMARY KEY,
    Product CHAR(100),
    Counts INT,
    Supplier INT,
    FOREIGN KEY (Supplier) REFERENCES Supplier(ID)
);

-- Создание таблицы с категориями
CREATE TABLE Category (
    ID INT IDENTITY (1,1) PRIMARY KEY,
    Category CHAR(100),
    Supply INT,
    FOREIGN KEY (Supply) REFERENCES Supply(ID)
);

-- Создание таблицы с товарами
CREATE TABLE Product (
    ID INT IDENTITY (1,1) PRIMARY KEY,
    Name CHAR(100),
    Counts INT,
    Description CHAR(200),
    Category INT,
    FOREIGN KEY (Category) REFERENCES Category(ID)
);

-- Создание таблицы с заказами
CREATE TABLE Orders (
    ID INT IDENTITY (1,1) PRIMARY KEY,
    Order_Time INT,
    Order_Status CHAR(200),
    Total_cost INT,
    Products INT,
    FOREIGN KEY (Products) REFERENCES Product(ID)
);


