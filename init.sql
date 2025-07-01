--Создание таблички
CREATE TABLE IF NOT EXISTS cars (
    Id SERIAL PRIMARY KEY, --Айди
    Brand VARCHAR(100) NOT NULL, --Бренд, строка, можно было выделить и поменьше
    Model VARCHAR(100) NOT NULL, --Модель, аналогично
    Year INTEGER CHECK(YEAR >= 1980 AND YEAR <= 2100), --Программистам в 2100 году соболезную
    OwnerName VARCHAR(200), --Что-то
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP --Пусть будет
);
--Вставка данных в табличку
INSERT INTO cars (Brand, Model, Year, OwnerName) values
('AAZ', '42-0', 2050, null),
('BAZ', '42-1', 2010, null),
('VAZ', '42-2', 2033, 'Иванов А.А.'),
('GAZ', '42-3', 2005, 'Иванов А.Б.'),
('DAZ', '42-4', 1995, 'Иванов А.В.'),
('EAZ', '42-5', 1991, 'Иванов Б.А.'),
('YoAZ', '42-6', 1981, 'Иванов Б.Б.'),
('JAZ', '42-7', 1997, 'Иванов Б.В.');


--Создание таблички
CREATE TABLE IF NOT EXISTS services (
    Id SERIAL PRIMARY KEY, 
    Name VARCHAR(100) NOT NULL, 
    Description VARCHAR(500) NOT NULL,
    Price DOUBLE precision CHECK(Price >= 0)
);

INSERT INTO services (name, description, price) values
('Чистка', 'Чистка машины', 2000),
('Диагностика', 'Диагностика машины', 1500),
('Ремонт', 'Ремонт машины', 99999);

--Создание таблички
CREATE TABLE IF NOT EXISTS ServiceRecordModel (
    Id SERIAL PRIMARY KEY, 
    CarId Integer Not Null, 
    ServiceId Integer Not Null, 
    FOREIGN KEY (CarId) REFERENCES cars(Id) ON DELETE CASCADE,
    FOREIGN KEY (ServiceId) REFERENCES services(Id) ON DELETE CASCADE,
    DateService TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    Status VARCHAR(100) NOT NULL
);

INSERT INTO ServiceRecordModel (CarId, ServiceId, Status) values
(4, 1, 'В процессе'),
(4, 2, 'Завершено'),
(6, 3, 'Ждем комплектующих');

create index brand_index on cars(brand);
create index year_index on cars(year);

SELECT 'Products table created and populated' as Status;