
-- Drop the database if it exists
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'VoVo')
BEGIN
    USE master;
    ALTER DATABASE VoVo SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [VoVo];
END

-- Create the new database
CREATE DATABASE VoVo;

-- Switch to the new database
USE VoVo;


-- Tạo bảng Users
-- Tạo bảng Users
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255),
    DateOfBirth DATE,
    Gender VARCHAR(10),
    Location VARCHAR(255),
    Description TEXT,
    Email VARCHAR(255),
    Password VARCHAR(255),
    UserType VARCHAR(10) DEFAULT 'FREE',
    Status BIT DEFAULT 1,
    IPAddress VARCHAR(255) DEFAULT '::1',
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
);

-- Tạo bảng Admin
CREATE TABLE Admin (
    AdminID INT IDENTITY(1,1) PRIMARY KEY,
    Avatar VARCHAR(255),
    Name NVARCHAR(255),
    Email VARCHAR(255),
    Password VARCHAR(255)
);

-- Tạo bảng UserPhotos
CREATE TABLE UserPhotos (
    UserID INT,
    PhotoLink VARCHAR(255),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Tạo bảng Likes
CREATE TABLE Likes (
    LikeID INT IDENTITY(1,1) PRIMARY KEY,
    LikerID INT,
    LikedUserID INT,
    DateLiked DATE,
    FOREIGN KEY (LikerID) REFERENCES Users(UserID),
    FOREIGN KEY (LikedUserID) REFERENCES Users(UserID)
);

-- Tạo bảng LikeLimit
CREATE TABLE LikeLimit (
    UserID INT PRIMARY KEY,
    LikesToday INT,
    DailyLikeLimit INT,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Tạo bảng Matches
CREATE TABLE Matches (
    MatchID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT,
    MatchedUserID INT,
    DateMatched DATE,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (MatchedUserID) REFERENCES Users(UserID)
);

-- Tạo bảng Messages
CREATE TABLE Messages (
    MessageID INT IDENTITY(1,1) PRIMARY KEY,
    FromUserID INT,
    ToUserID INT,
    MessageText TEXT,
    DateSent DATETIME,
    FOREIGN KEY (FromUserID) REFERENCES Users(UserID),
    FOREIGN KEY (ToUserID) REFERENCES Users(UserID)
);

-- Tạo bảng Setting ( View, Cost of Gold )
CREATE TABLE Setting (
    SettingID INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Value VARCHAR(255) DEFAULT '0'
);

-- Tạo bảng Report ( Report User )
CREATE TABLE Report (
    ReportID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT,
    Description NVARCHAR(255),
    Status BIT DEFAULT 0,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Tạo bảng IPBanned
CREATE TABLE IPBanned (
    BannedIp VARCHAR(255) PRIMARY KEY NOT NULL
);

-- Tạo bảng Statistic
CREATE TABLE Statistic (
    StatisticID INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Value VARCHAR(255) DEFAULT 0
);

-- Tạo bảng Plans
CREATE TABLE Plans (
    PlanID INT IDENTITY(1,1) PRIMARY KEY,
    PlanName VARCHAR(20) NOT NULL,
    Amount MONEY NOT NULL,
    Duration INT NOT NULL
);

-- Tạo bảng Subscriptions
CREATE TABLE Subscriptions (
    SubscriptionID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    PlanID INT NOT NULL,
	TotalCost MONEY,
    StartDate DATETIME, -- Updated upon successful payment
    EndDate DATETIME,
    PaymentStatus VARCHAR(20) DEFAULT 'PENDING', -- 'PENDING', 'SUCCESS', 'FAILED', etc.
    CreatedDate DATETIME NOT NULL, -- Date and time of successful payment
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (PlanID) REFERENCES Plans(PlanID)
);

-- Tạo bảng UpgradeHistory
CREATE TABLE UpgradeHistory (
    UserID INT NOT NULL,
    PlanID INT,
    TotalCost MONEY NOT NULL,
    UpgradeDate DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (PlanID) REFERENCES Plans(PlanID)
);

go

INSERT INTO Setting(Name, Value) VALUES ('Premium Price Month','129.9'),('Premium Price Year','1299.9');


INSERT INTO Admin (Avatar, Name, Email, Password)
VALUES ('thang.jpeg',N'Lê Thắng', 'lethang@gmail.com', '123456'),
       ('hieu.jpg',N'Chung Hiếu', 'chtalong@gmail.com', 'hieuprono1');

INSERT INTO Users (Name, DateOfBirth, Gender, Location, Description, Email, Password, UserType)
VALUES (N'Lê Thắng', '1990-05-15', 'Male', 'New York', 'I am a software engineer', 'lethangd@gmail.com', '1234', 'FREE'),
       ('Jane Smith', '1985-09-22', 'Female', 'Los Angeles', 'I work in marketing', 'jane.smith@example.com', 'pass456', 'PREMIUM'),
       ('Tom Johnson', '1993-12-10', 'Male', 'Chicago', 'I love playing guitar', 'tom.johnson@example.com', 'guitar123', 'FREE'),
	    ('John Doe', '1990-05-15', 'Male', 'New York', 'Description for John', 'john@example.com', 'password123', 'FREE'),
    ('Alice Johnson', '1997-03-10', 'Female', 'Chicago', 'Description for Alice', 'alice@example.com', 'password789', 'FREE'),
    ('Bob Brown', '1988-12-03', 'Male', 'Houston', 'Description for Bob', 'bob@example.com', 'passwordABC', 'FREE'),
	(N'Lê Thắng', '1990-05-15', 'Male', 'New York', 'I am a software engineer', 'lethangd@gmail.com', '1234', 'FREE'),
       ('Jane Smith', '1985-09-22', 'Female', 'Los Angeles', 'I work in marketing', 'jane.smith@example.com', 'pass456', 'PREMIUM'),
       ('Tom Johnson', '1993-12-10', 'Male', 'Chicago', 'I love playing guitar', 'tom.johnson@example.com', 'guitar123', 'FREE'),
	    ('John Doe', '1990-05-15', 'Male', 'New York', 'Description for John', 'john@example.com', 'password123', 'FREE'),
    ('Alice Johnson', '1997-03-10', 'Female', 'Chicago', 'Description for Alice', 'alice@example.com', 'password789', 'FREE'),
    ('Bob Brown', '1988-12-03', 'Male', 'Houston', 'Description for Bob', 'bob@example.com', 'passwordABC', 'FREE'),
    ('Eva Davis', '1995-11-25', 'Female', 'Miami', 'Description for Eva', 'eva@example.com', 'passwordXYZ', 'FREE'),
    ('Frank Wilson', '1983-09-08', 'Male', 'Seattle', 'Description for Frank', 'frank@example.com', 'password7890', 'FREE'),
	(N'Lê Thắng', '1990-05-15', 'Male', 'New York', 'I am a software engineer', 'lethangd@gmail.com', '1234', 'FREE'),
       ('Jane Smith', '1985-09-22', 'Female', 'Los Angeles', 'I work in marketing', 'jane.smith@example.com', 'pass456', 'PREMIUM'),
       ('Tom Johnson', '1993-12-10', 'Male', 'Chicago', 'I love playing guitar', 'tom.johnson@example.com', 'guitar123', 'FREE'),
	    ('John Doe', '1990-05-15', 'Male', 'New York', 'Description for John', 'john@example.com', 'password123', 'FREE'),
    ('Alice Johnson', '1997-03-10', 'Female', 'Chicago', 'Description for Alice', 'alice@example.com', 'password789', 'FREE'),
    ('Bob Brown', '1988-12-03', 'Male', 'Houston', 'Description for Bob', 'bob@example.com', 'passwordABC', 'FREE'),
    ('Eva Davis', '1995-11-25', 'Female', 'Miami', 'Description for Eva', 'eva@example.com', 'passwordXYZ', 'FREE'),
    ('Frank Wilson', '1983-09-08', 'Male', 'Seattle', 'Description for Frank', 'frank@example.com', 'password7890', 'FREE'),
	(N'Lê Thắng', '1990-05-15', 'Male', 'New York', 'I am a software engineer', 'lethangd@gmail.com', '1234', 'FREE'),
       ('Jane Smith', '1985-09-22', 'Female', 'Los Angeles', 'I work in marketing', 'jane.smith@example.com', 'pass456', 'PREMIUM'),
       ('Tom Johnson', '1993-12-10', 'Male', 'Chicago', 'I love playing guitar', 'tom.johnson@example.com', 'guitar123', 'FREE'),
	    ('John Doe', '1990-05-15', 'Male', 'New York', 'Description for John', 'john@example.com', 'password123', 'FREE'),
    ('Alice Johnson', '1997-03-10', 'Female', 'Chicago', 'Description for Alice', 'alice@example.com', 'password789', 'FREE'),
    ('Bob Brown', '1988-12-03', 'Male', 'Houston', 'Description for Bob', 'bob@example.com', 'passwordABC', 'FREE'),
    ('Eva Davis', '1995-11-25', 'Female', 'Miami', 'Description for Eva', 'eva@example.com', 'passwordXYZ', 'FREE'),
    ('Frank Wilson', '1983-09-08', 'Male', 'Seattle', 'Description for Frank', 'frank@example.com', 'password7890', 'FREE'),
	(N'Lê Thắng', '1990-05-15', 'Male', 'New York', 'I am a software engineer', 'lethangd@gmail.com', '1234', 'FREE'),
       ('Jane Smith', '1985-09-22', 'Female', 'Los Angeles', 'I work in marketing', 'jane.smith@example.com', 'pass456', 'PREMIUM'),
       ('Tom Johnson', '1993-12-10', 'Male', 'Chicago', 'I love playing guitar', 'tom.johnson@example.com', 'guitar123', 'FREE'),
	    ('John Doe', '1990-05-15', 'Male', 'New York', 'Description for John', 'john@example.com', 'password123', 'FREE'),
    ('Alice Johnson', '1997-03-10', 'Female', 'Chicago', 'Description for Alice', 'alice@example.com', 'password789', 'FREE'),
    ('Bob Brown', '1988-12-03', 'Male', 'Houston', 'Description for Bob', 'bob@example.com', 'passwordABC', 'FREE'),
    ('Eva Davis', '1995-11-25', 'Female', 'Miami', 'Description for Eva', 'eva@example.com', 'passwordXYZ', 'FREE'),
    ('Frank Wilson', '1983-09-08', 'Male', 'Seattle', 'Description for Frank', 'frank@example.com', 'password7890', 'FREE'),
    ('Eva Davis', '1995-11-25', 'Female', 'Miami', 'Description for Eva', 'eva@example.com', 'passwordXYZ', 'FREE'),
    ('Frank Wilson', '1983-09-08', 'Male', 'Seattle', 'Description for Frank', 'frank@example.com', 'password7890', 'FREE');

	
INSERT INTO Statistic(Name, Value)
SELECT 'User update gold', COUNT(*) AS Number FROM Users WHERE UserType = 'PREMIUM';

INSERT INTO UserPhotos (UserID, PhotoLink)
VALUES (1, 'https://randomuser.me/api/portraits/men/40.jpg'),(1, 'https://randomuser.me/api/portraits/men/11.jpg'),(1, 'https://randomuser.me/api/portraits/men/12.jpg'),
(2, 'https://randomuser.me/api/portraits/women/41.jpg'),(2, 'https://randomuser.me/api/portraits/women/11.jpg'),(2, 'https://randomuser.me/api/portraits/women/12.jpg'),
(3, 'https://randomuser.me/api/portraits/women/42.jpg'),(3, 'https://randomuser.me/api/portraits/women/13.jpg'),(3, 'https://randomuser.me/api/portraits/women/14.jpg'),
(4, 'https://randomuser.me/api/portraits/women/43.jpg'),(4, 'https://randomuser.me/api/portraits/women/15.jpg'),(4, 'https://randomuser.me/api/portraits/women/16.jpg'),
(5, 'https://randomuser.me/api/portraits/women/44.jpg'),(5, 'https://randomuser.me/api/portraits/women/17.jpg'),(5, 'https://randomuser.me/api/portraits/women/18.jpg'),
(6, 'https://randomuser.me/api/portraits/women/45.jpg'),
(7, 'https://randomuser.me/api/portraits/women/46.jpg'),
(8, 'https://randomuser.me/api/portraits/women/47.jpg'),
(9, 'https://randomuser.me/api/portraits/women/48.jpg'),
(10, 'https://randomuser.me/api/portraits/women/49.jpg'),
(11, 'https://randomuser.me/api/portraits/women/50.jpg'),
(12, 'https://randomuser.me/api/portraits/women/1.jpg'),
(13, 'https://randomuser.me/api/portraits/women/2.jpg'),
(14, 'https://randomuser.me/api/portraits/women/3.jpg'),
(15, 'https://randomuser.me/api/portraits/women/4.jpg'),
(16, 'https://randomuser.me/api/portraits/women/5.jpg'),
(17, 'https://randomuser.me/api/portraits/women/6.jpg'),
(18, 'https://randomuser.me/api/portraits/women/7.jpg'),
(19, 'https://randomuser.me/api/portraits/women/8.jpg'),
(20, 'https://randomuser.me/api/portraits/women/9.jpg'),
(21, 'https://randomuser.me/api/portraits/women/10.jpg'),
(22, 'https://randomuser.me/api/portraits/women/11.jpg'),
(23, 'https://randomuser.me/api/portraits/women/12.jpg'),
(24, 'https://randomuser.me/api/portraits/women/13.jpg'),
(25, 'https://randomuser.me/api/portraits/women/14.jpg'),
(26, 'https://randomuser.me/api/portraits/women/15.jpg'),
(27, 'https://randomuser.me/api/portraits/women/16.jpg'),
(28, 'https://randomuser.me/api/portraits/women/17.jpg'),
(29, 'https://randomuser.me/api/portraits/women/18.jpg'),
(30, 'https://randomuser.me/api/portraits/women/19.jpg');
INSERT INTO Matches (UserID, MatchedUserID, DateMatched)
VALUES (1, 2, '2023-10-19'),(3, 1, '2023-10-21'),
       (2, 1, '2023-10-20'),
       (1, 3, '2023-10-21'),
(8,	1, '2023-10-21'),
(9,	1, '2023-10-21'),
(10,	1, '2023-10-21'),
(11,	1, '2023-10-21'),
(12,	1, '2023-10-21'),
(13,	1, '2023-10-21'),
(14,	1, '2023-10-21'),
(15,	1, '2023-10-21'),
(16,	1, '2023-10-21'),
(17,	1, '2023-10-21'),
(18,	1, '2023-10-21'),
(19,	1, '2023-10-21'),
(20,	1, '2023-10-21'),
(21,	1, '2023-10-21'),
(22,	1, '2023-10-21'),
(23,	1, '2023-10-21'),
(24,	1, '2023-10-21'),
(25,	1, '2023-10-21'),
(26,	1, '2023-10-21'),
(27,	1, '2023-10-21'),
(28,	1, '2023-10-21'),
(29,	1, '2023-10-21'),
(30,	1, '2023-10-21');

INSERT INTO Messages (FromUserID, ToUserID, MessageText, DateSent)
VALUES (1, 2, 'Hello, how are you?', '2023-10-19 10:00:00'),
       (2, 1, 'I''m good, thanks!', '2023-10-19 11:30:00'),
       (1, 3, 'Nice to meet you!', '2023-10-20 09:15:00');
INSERT INTO Plans (PlanName, Amount, Duration)
VALUES 
    ('Free', 0.00, 0), -- Assuming Free plan has no cost and duration is 0
    ('Gold', 19.99, 1),
    ('Gold', 99.99, 6),
    ('Gold', 199.99, 12);
INSERT INTO Subscriptions (UserID, PlanID,TotalCost, StartDate, EndDate, PaymentStatus, CreatedDate)
VALUES 
    (1, 1,1000, GETDATE(), NULL, 'SUCCESS', GETDATE()),
	(2, 2,2000, GETDATE(), DATEADD(MONTH, 1, GETDATE()), 'SUCCESS', GETDATE());

INSERT INTO LikeLimit (UserID, LikesToday, DailyLikeLimit)
VALUES (1, 5, 10);
