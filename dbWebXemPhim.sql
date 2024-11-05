/*CREATE DATABASE MovieDatabase_64130299;
USE MovieDatabase_64130299;
DROP DATABASE MovieDatabase_64130299;*/

/*THIẾT KẾ CƠ SỞ DỮ LIỆU*/
--Tạo bảng user
CREATE TABLE User_64130299 (
    UserId INT IDENTITY(1,1) PRIMARY KEY,--chỉ số sẽ bắt đầu từ 1 và tăng dần, đảm bảo id mỗi người dùng khác nhau
    Email NVARCHAR(256) UNIQUE NOT NULL,
    Password NVARCHAR(256) NOT NULL,
	Username NVARCHAR(50) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()
);


-- Tạo bảng Movies
CREATE TABLE Movie_64130299 (
    MovieId INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(256) NOT NULL,
    Description NVARCHAR(MAX),
    ReleaseDate DATE,
    Genre NVARCHAR(100),
    PosterUrl NVARCHAR(512),
    TrailerUrl NVARCHAR(512),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()
);

-- Tạo bảng Favorites
CREATE TABLE Favorite_64130299 (
    FavoriteId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT FOREIGN KEY REFERENCES User_64130299(UserId),
    MovieId INT FOREIGN KEY REFERENCES Movie_64130299(MovieId),
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Tạo bảng WatchHistory
CREATE TABLE WatchHistory_64130299 (
    HistoryId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT FOREIGN KEY REFERENCES User_64130299(UserId),
    MovieId INT FOREIGN KEY REFERENCES Movie_64130299(MovieId),
    LastWatchedTime DATETIME DEFAULT GETDATE(),
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Tạo bảng Ratings
CREATE TABLE Rating_64130299 (
    RatingId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT FOREIGN KEY REFERENCES User_64130299(UserId),
    MovieId INT FOREIGN KEY REFERENCES Movie_64130299(MovieId),
    Rating INT CHECK (Rating BETWEEN 1 AND 5),
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Tạo bảng Comments
CREATE TABLE Comment_64130299 (
    CommentId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT FOREIGN KEY REFERENCES User_64130299(UserId),
    MovieId INT FOREIGN KEY REFERENCES Movie_64130299(MovieId),
    Content NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE()
);

/*CHÈN DỮ LIỆU MẪU*/
-- Chèn dữ liệu mẫu vào bảng Users
INSERT INTO User_64130299(Email, Password, Username, CreatedAt, UpdatedAt)
VALUES ('admin@example.com', 'hashedadminpassword', 'Nguyễn Xuân Đạt' , '2024-10-19','2024-10-19'),
       ('user1@example.com', 'hashedpassword1' , 'Trần Kim Quang' , '2024-10-19','2024-10-19'),


-- Chèn dữ liệu mẫu vào bảng Movies
INSERT INTO Movie_64130299(Title, Description, ReleaseDate, Genre, PosterUrl, TrailerUrl)
VALUES ('Movie Title 1', 'Description of movie 1', '2024-01-01', 'Action', 'http://example.com/poster1.jpg', 'http://example.com/trailer1.mp4'),
       ('Movie Title 2', 'Description of movie 2', '2024-02-01', 'Drama', 'http://example.com/poster2.jpg', 'http://example.com/trailer2.mp4');

-- Chèn dữ liệu mẫu vào bảng Favorites
INSERT INTO Favorite_64130299(UserId, MovieId)
VALUES (1, 1),
       (2, 2);

-- Chèn dữ liệu mẫu vào bảng WatchHistory
INSERT INTO WatchHistory_64130299(UserId, MovieId, LastWatchedTime)
VALUES (1, 1, '2024-01-10 10:00:00'),
       (2, 2, '2024-02-15 12:00:00');

-- Chèn dữ liệu mẫu vào bảng Comments
INSERT INTO Comment_64130299(UserId, MovieId, Content)
VALUES (1, 1, 'Great movie!'),
       (2, 2, 'Not bad, but could be better.');

/*TRUY VẤN DỮ LIỆU*/
--Truy vấn danh sách phim
SELECT * FROM Movie_64130299;

--Tìm kiếm phim theo tiêu đề hoặc thể loại
SELECT * FROM Movie_64130299
WHERE Title LIKE '%action%'
   OR Genre = 'Action';

--Lịch sử xem của người dùng
SELECT m.Title, wh.LastWatchedTime
FROM WatchHistory_64130299 wh
JOIN Movie_64130299 m ON wh.MovieId = m.MovieId
WHERE wh.UserId = 1;

--Danh sách yêu thích của người dùng
SELECT m.Title
FROM Favorite_64130299 f
JOIN Movie_64130299 m ON f.MovieId = m.MovieId
WHERE f.UserId = 1;

--Đánh giá phim của người dùng
SELECT m.Title, r.Rating
FROM Rating_64130299 r
JOIN Movie_64130299 m ON r.MovieId = m.MovieId
WHERE r.UserId = 1;

--Bình luận về phim
SELECT m.Title, c.Content
FROM Comment_64130299 c
JOIN Movie_64130299 m ON c.MovieId = m.MovieId
WHERE c.UserId = 1;


ALTER TABLE Movie_64130299
ADD PosterImage VARBINARY(MAX);

ALTER TABLE Movie_64130299
ADD MovieUrl NVARCHAR(MAX) NULL;

ALTER TABLE Movie_64130299
DROP COLUMN PosterImage;




