
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
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(255),
    DateOfBirth DATE,
    Gender VARCHAR(10),
    Location VARCHAR(255),
    Description TEXT,
    Email VARCHAR(255),
    Password VARCHAR(255),
	UserType VARCHAR(10) DEFAULT 'FREE',
	Status BIT DEFAULT 1,
	IPAddress VARCHAR(255) DEFAULT '::1'
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
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
);

-- Tạo bảng Likes
CREATE TABLE Likes (
    LikeID INT IDENTITY(1,1) PRIMARY KEY,
    LikerID INT,
    LikedUserID INT,
    DateLiked DATE,
    LikesToday INT,
    DailyLikeLimit INT,
    FOREIGN KEY (LikerID) REFERENCES Users(UserID),
    FOREIGN KEY (LikedUserID) REFERENCES Users(UserID)
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
go
-- Tạo bảng Setting ( View, Cost of Gold )
CREATE TABLE Setting (
    SettingID int IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(255) NOT NULL,
	Value VARCHAR(255) DEFAULT '0'
);
go
-- Tạo bảng Report ( Report User )
CREATE TABLE Report (
    ReportID int IDENTITY(1,1) PRIMARY KEY,
	UserID INT,
	Description NVARCHAR(255),
	Status BIT DEFAULT 0,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
);
go
INSERT INTO Setting(Name, Value) VALUES ('Gold Price Month','129.9'),('Gold Price Year','1299.9'),('View','100000')

INSERT INTO Admin (Avatar, Name, Email, Password)
VALUES ('thang.jpeg',N'Lê Thắng', 'lethang@gmail.com', '123456'),
       ('hieu.jpg',N'Chung Hiếu', 'chtalong@gmail.com', 'hieuprono1');

INSERT INTO Users (Name, DateOfBirth, Gender, Location, Description, Email, Password, UserType)
VALUES ('Lê Thắng', '1990-05-15', 'Male', 'New York', 'I am a software engineer', 'lethangd@gmail.com', '1234', 'FREE'),
       ('Jane Smith', '1985-09-22', 'Female', 'Los Angeles', 'I work in marketing', 'jane.smith@example.com', 'pass456', 'PREMIUM'),
       ('Tom Johnson', '1993-12-10', 'Male', 'Chicago', 'I love playing guitar', 'tom.johnson@example.com', 'guitar123', 'FREE'),
	    ('John Doe', '1990-05-15', 'Male', 'New York', 'Description for John', 'john@example.com', 'password123', 'FREE'),
    ('Alice Johnson', '1997-03-10', 'Female', 'Chicago', 'Description for Alice', 'alice@example.com', 'password789', 'FREE'),
    ('Bob Brown', '1988-12-03', 'Male', 'Houston', 'Description for Bob', 'bob@example.com', 'passwordABC', 'FREE'),
	('Lê Thắng', '1990-05-15', 'Male', 'New York', 'I am a software engineer', 'lethangd@gmail.com', '1234', 'FREE'),
       ('Jane Smith', '1985-09-22', 'Female', 'Los Angeles', 'I work in marketing', 'jane.smith@example.com', 'pass456', 'PREMIUM'),
       ('Tom Johnson', '1993-12-10', 'Male', 'Chicago', 'I love playing guitar', 'tom.johnson@example.com', 'guitar123', 'FREE'),
	    ('John Doe', '1990-05-15', 'Male', 'New York', 'Description for John', 'john@example.com', 'password123', 'FREE'),
    ('Alice Johnson', '1997-03-10', 'Female', 'Chicago', 'Description for Alice', 'alice@example.com', 'password789', 'FREE'),
    ('Bob Brown', '1988-12-03', 'Male', 'Houston', 'Description for Bob', 'bob@example.com', 'passwordABC', 'FREE'),
    ('Eva Davis', '1995-11-25', 'Female', 'Miami', 'Description for Eva', 'eva@example.com', 'passwordXYZ', 'FREE'),
    ('Frank Wilson', '1983-09-08', 'Male', 'Seattle', 'Description for Frank', 'frank@example.com', 'password7890', 'FREE'),
	('Lê Thắng', '1990-05-15', 'Male', 'New York', 'I am a software engineer', 'lethangd@gmail.com', '1234', 'FREE'),
       ('Jane Smith', '1985-09-22', 'Female', 'Los Angeles', 'I work in marketing', 'jane.smith@example.com', 'pass456', 'PREMIUM'),
       ('Tom Johnson', '1993-12-10', 'Male', 'Chicago', 'I love playing guitar', 'tom.johnson@example.com', 'guitar123', 'FREE'),
	    ('John Doe', '1990-05-15', 'Male', 'New York', 'Description for John', 'john@example.com', 'password123', 'FREE'),
    ('Alice Johnson', '1997-03-10', 'Female', 'Chicago', 'Description for Alice', 'alice@example.com', 'password789', 'FREE'),
    ('Bob Brown', '1988-12-03', 'Male', 'Houston', 'Description for Bob', 'bob@example.com', 'passwordABC', 'FREE'),
    ('Eva Davis', '1995-11-25', 'Female', 'Miami', 'Description for Eva', 'eva@example.com', 'passwordXYZ', 'FREE'),
    ('Frank Wilson', '1983-09-08', 'Male', 'Seattle', 'Description for Frank', 'frank@example.com', 'password7890', 'FREE'),
	('Lê Thắng', '1990-05-15', 'Male', 'New York', 'I am a software engineer', 'lethangd@gmail.com', '1234', 'FREE'),
       ('Jane Smith', '1985-09-22', 'Female', 'Los Angeles', 'I work in marketing', 'jane.smith@example.com', 'pass456', 'PREMIUM'),
       ('Tom Johnson', '1993-12-10', 'Male', 'Chicago', 'I love playing guitar', 'tom.johnson@example.com', 'guitar123', 'FREE'),
	    ('John Doe', '1990-05-15', 'Male', 'New York', 'Description for John', 'john@example.com', 'password123', 'FREE'),
    ('Alice Johnson', '1997-03-10', 'Female', 'Chicago', 'Description for Alice', 'alice@example.com', 'password789', 'FREE'),
    ('Bob Brown', '1988-12-03', 'Male', 'Houston', 'Description for Bob', 'bob@example.com', 'passwordABC', 'FREE'),
    ('Eva Davis', '1995-11-25', 'Female', 'Miami', 'Description for Eva', 'eva@example.com', 'passwordXYZ', 'FREE'),
    ('Frank Wilson', '1983-09-08', 'Male', 'Seattle', 'Description for Frank', 'frank@example.com', 'password7890', 'FREE'),
	('Lê Thắng', '1990-05-15', 'Male', 'New York', 'I am a software engineer', 'lethangd@gmail.com', '1234', 'FREE'),
       ('Jane Smith', '1985-09-22', 'Female', 'Los Angeles', 'I work in marketing', 'jane.smith@example.com', 'pass456', 'PREMIUM'),
       ('Tom Johnson', '1993-12-10', 'Male', 'Chicago', 'I love playing guitar', 'tom.johnson@example.com', 'guitar123', 'FREE'),
	    ('John Doe', '1990-05-15', 'Male', 'New York', 'Description for John', 'john@example.com', 'password123', 'FREE'),
    ('Alice Johnson', '1997-03-10', 'Female', 'Chicago', 'Description for Alice', 'alice@example.com', 'password789', 'FREE'),
    ('Bob Brown', '1988-12-03', 'Male', 'Houston', 'Description for Bob', 'bob@example.com', 'passwordABC', 'FREE'),
    ('Eva Davis', '1995-11-25', 'Female', 'Miami', 'Description for Eva', 'eva@example.com', 'passwordXYZ', 'FREE'),
    ('Frank Wilson', '1983-09-08', 'Male', 'Seattle', 'Description for Frank', 'frank@example.com', 'password7890', 'FREE'),
    ('Eva Davis', '1995-11-25', 'Female', 'Miami', 'Description for Eva', 'eva@example.com', 'passwordXYZ', 'FREE'),
    ('Frank Wilson', '1983-09-08', 'Male', 'Seattle', 'Description for Frank', 'frank@example.com', 'password7890', 'FREE');
INSERT INTO Admin (Name, Email, Password)
VALUES ('Admin User 1', 'admin1@example.com', 'adminpass1'),
       ('Admin User 2', 'admin2@example.com', 'adminpass2');
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
(12, 'https://randomuser.me/api/portraits/women/51.jpg'),
(13, 'https://randomuser.me/api/portraits/women/52.jpg'),
(14, 'https://randomuser.me/api/portraits/women/53.jpg'),
(15, 'https://randomuser.me/api/portraits/women/54.jpg'),
(16, 'https://randomuser.me/api/portraits/women/55.jpg'),
(17, 'https://randomuser.me/api/portraits/women/56.jpg'),
(18, 'https://randomuser.me/api/portraits/women/57.jpg'),
(19, 'https://randomuser.me/api/portraits/women/58.jpg'),
(20, 'https://randomuser.me/api/portraits/women/59.jpg'),
(21, 'https://randomuser.me/api/portraits/women/60.jpg'),
(22, 'https://randomuser.me/api/portraits/women/61.jpg'),
(23, 'https://randomuser.me/api/portraits/women/62.jpg'),
(24, 'https://randomuser.me/api/portraits/women/63.jpg'),
(25, 'https://randomuser.me/api/portraits/women/64.jpg'),
(26, 'https://randomuser.me/api/portraits/women/65.jpg'),
(27, 'https://randomuser.me/api/portraits/women/66.jpg'),
(28, 'https://randomuser.me/api/portraits/women/67.jpg'),
(29, 'https://randomuser.me/api/portraits/women/68.jpg'),
(30, 'https://randomuser.me/api/portraits/women/69.jpg');
INSERT INTO Matches (UserID, MatchedUserID, DateMatched)
VALUES (1, 2, '2023-10-19'),(3, 1, '2023-10-21'),
       (2, 1, '2023-10-20'),
       (1, 3, '2023-10-21');

INSERT INTO Messages (FromUserID, ToUserID, MessageText, DateSent)
VALUES (1, 2, 'Hello, how are you?', '2023-10-19 10:00:00'),
       (2, 1, 'I''m good, thanks!', '2023-10-19 11:30:00'),
       (1, 3, 'Nice to meet you!', '2023-10-20 09:15:00');