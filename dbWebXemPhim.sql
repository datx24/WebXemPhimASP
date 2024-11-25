/*CREATE DATABASE MovieDatabase_64130299;
USE MovieDatabase_64130299;
DROP DATABASE MovieDatabase_64130299;*/

-- Tạo bảng User
CREATE TABLE User_64130299 (
    UserId VARCHAR(100) PRIMARY KEY,
    Email NVARCHAR(256) UNIQUE NOT NULL,
    Password NVARCHAR(256) NOT NULL,
    Username NVARCHAR(50) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()
);

-- Tạo bảng Movie
CREATE TABLE Movie_64130299 (
    MovieId VARCHAR(100) PRIMARY KEY,
    Title NVARCHAR(256) NOT NULL,
    Description NVARCHAR(MAX),
    GenreId BIT DEFAULT(1), -- 0 thì là phim lẻ, 1 là phim bộ
    GenreName NVARCHAR(100),
    DirectorName NVARCHAR(256),
    ActorName NVARCHAR(256),
	Country NVARCHAR(100),
    ReleaseDate DATE,
    PosterUrl NVARCHAR(512),
    TrailerUrl NVARCHAR(512),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()
);
-- Thêm AccessLevel vào bảng Movie
ALTER TABLE Movie_64130299
ADD AccessLevel NVARCHAR(50) NOT NULL DEFAULT 'Standard';

-- Tạo bảng MovieUrls
CREATE TABLE MovieUrls_64130299 (
    MovieUrlId VARCHAR(100) PRIMARY KEY,
    MovieId VARCHAR(100) NOT NULL,
    Url NVARCHAR(512),
    FOREIGN KEY (MovieId) REFERENCES Movie_64130299(MovieId)
);

-- Tạo bảng Favorites
CREATE TABLE Favorite_64130299 (
    FavoriteId VARCHAR(100) PRIMARY KEY,
    UserId VARCHAR(100) NOT NULL,
    MovieId VARCHAR(100) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES User_64130299(UserId),
    FOREIGN KEY (MovieId) REFERENCES Movie_64130299(MovieId)
);

-- Tạo bảng WatchHistory
CREATE TABLE WatchHistory_64130299 (
    HistoryId VARCHAR(100) PRIMARY KEY,
    UserId VARCHAR(100) NOT NULL,
    MovieId VARCHAR(100) NOT NULL,
    LastWatchedTime DATETIME DEFAULT GETDATE(),
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES User_64130299(UserId),
    FOREIGN KEY (MovieId) REFERENCES Movie_64130299(MovieId)
);

-- Tạo bảng Ratings
CREATE TABLE Rating_64130299 (
    RatingId VARCHAR(100) PRIMARY KEY,
    UserId VARCHAR(100) NOT NULL,
    MovieId VARCHAR(100) NOT NULL,
    Rating INT CHECK (Rating BETWEEN 1 AND 5),
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES User_64130299(UserId),
    FOREIGN KEY (MovieId) REFERENCES Movie_64130299(MovieId)
);

-- Tạo bảng Comments
CREATE TABLE Comment_64130299 (
    CommentId VARCHAR(100) PRIMARY KEY,
    UserId VARCHAR(100) NOT NULL,
    MovieId VARCHAR(100) NOT NULL,
    Content NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES User_64130299(UserId),
    FOREIGN KEY (MovieId) REFERENCES Movie_64130299(MovieId)
);

CREATE TABLE AdminUsers_64130299 (
    Id INT IDENTITY(1,1) PRIMARY KEY, -- Sử dụng IDENTITY để tự động tăng
    Username NVARCHAR(255) NOT NULL, -- Sử dụng NVARCHAR để hỗ trợ Unicode
    PasswordHash NVARCHAR(255) NOT NULL -- Sử dụng NVARCHAR cho mật khẩu mã hóa
);

--Lưu thông tin các gói thành viên
CREATE TABLE SubscriptionPlans_64130299 (
    PlanId INT PRIMARY KEY IDENTITY(1,1),  -- Mã gói thành viên
    PlanName NVARCHAR(50) NOT NULL,         -- Tên gói (ví dụ: "1 tháng", "1 năm")
    DurationMonths INT NOT NULL,            -- Số tháng của gói (1 tháng, 12 tháng)
    Price DECIMAL(10, 2) NOT NULL,          -- Giá tiền của gói
    CreatedAt DATETIME DEFAULT GETDATE(),   -- Ngày tạo gói
    UpdatedAt DATETIME DEFAULT GETDATE()    -- Ngày cập nhật gói
);

-- Tạo bảng MemberSubscription để đăng ký thành viên
CREATE TABLE MemberSubscription_64130299 (
    SubscriptionId VARCHAR(100) PRIMARY KEY,  -- Mã đăng ký, có thể là GUID hoặc UUID
    UserId VARCHAR(100) NOT NULL,             -- ID người dùng từ bảng User
    PlanId INT NOT NULL,                      -- Mã gói từ bảng SubscriptionPlans
    StartDate DATETIME NOT NULL DEFAULT GETDATE(), -- Ngày bắt đầu đăng ký
    ExpiryDate DATETIME NOT NULL,            -- Ngày hết hạn thẻ thành viên
    AccessLevel NVARCHAR(50) NOT NULL DEFAULT 'Premium', -- Phân loại thẻ, "Premium" hoặc "Free"
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',  -- Trạng thái thẻ ("Active", "Inactive", "Expired")
    PaymentMethod NVARCHAR(50) NOT NULL,      -- Phương thức thanh toán (VNPay, MoMo, ...)
    AmountPaid DECIMAL(10, 2) NOT NULL,       -- Số tiền đã thanh toán
    RenewalDate DATETIME NULL,                -- Ngày gia hạn (nếu có)
    CreatedAt DATETIME DEFAULT GETDATE(),     -- Ngày tạo bản ghi
    UpdatedAt DATETIME DEFAULT GETDATE(),     -- Ngày cập nhật bản ghi
    FOREIGN KEY (UserId) REFERENCES User_64130299(UserId),  -- Liên kết với bảng User
    FOREIGN KEY (PlanId) REFERENCES SubscriptionPlans_64130299(PlanId)  -- Liên kết với bảng SubscriptionPlans
);



INSERT INTO AdminUsers_64130299 (Username, PasswordHash)
VALUES ('admin', '6088d7f1f66b1d3d311f8db7d9217dcef9e89e03c536adb5ae37e740fabf1a03');



INSERT INTO User_64130299 (UserId,Email, Password, Username)
VALUES
('1-nguyen1','user1@example.com', 'password123', 'nguyen1'),
('2-nguyen2','user2@example.com', 'password123', 'nguyen2'),
('3-nguyen3','user3@example.com', 'password123', 'nguyen3'),
('4-nguyen4','user4@example.com', 'password123', 'nguyen4'),
('5-nguyen5','user5@example.com', 'password123', 'nguyen5'),
('6-nguyen6','user6@example.com', 'password123', 'nguyen6'),
('7-nguyen7','user7@example.com', 'password123', 'nguyen7'),
('8-nguyen8','user8@example.com', 'password123', 'nguyen8'),
('9-nguyen9','user9@example.com', 'password123', 'nguyen9'),
('10-nguyen10','user10@example.com', 'password123', 'nguyen10');


INSERT INTO Movie_64130299 (MovieId, Title, Description, GenreId, GenreName, DirectorName, ActorName, Country, ReleaseDate, PosterUrl, TrailerUrl)
VALUES
('1-ttzkndd', N'Tận Thế Z: Khởi Nguồn Đại Dịch', N'Mô tả về Phim 1', 0, N'Hành động', N'Alexandre Bruguès', N'Richard Armitage, Ana de Armas', N'Việt Nam', '2020-01-01', 'https://ohaytv.com/storage/hinh-anh/jaBToJ1DZcwn5wOsQeLOXFVlBLn.jpg', 'https://www.youtube.com/embed/pPvaIGldT-I?si=n-EXlY1Usw3rAur5'),
('2-vqat', N'Về quê ăn Tết', N'Mô tả về Phim 2', 0, N'Hành động', N'Nguyễn Hoàng Anh', N'Ngô Thanh Vân, Jun Phạm', N'Việt Nam', '2021-02-02', 'https://ohaytv.com/storage/hinh-anh/ve-que-an-tet-thumb.jpg', 'https://www.youtube.com/embed/HUxQIqTvqIU?si=X6n20Vq7Ad533rXP'),
('3-tkxx', N'Thoát Khỏi Xiềng Xích', N'Mô tả về Phim 3', 0, N'Hành động', N'M. Night Shyamalan', N'James McAvoy, Anya Taylor-Joy', N'Mỹ', '2022-03-03', 'https://phimmoi.gay/storage/images/shaggy-scooby-doo-get-a-clue-phan-1-172439095333179/e9dcc9b2b631d38c6e099c0d5d9f7884.jpg', 'https://www.youtube.com/embed/Sbl6vozFEaI?si=006as2XkowZm35Je'),
('4-o', N'Onibaba', N'Mô tả về Phim 4', 0, N'Hành động', N'Kaneto Shindo', N'Nobuko Otowa, Jitsuko Yoshimura, Kei Satô', N'Nhật Bản', '2023-04-04', 'https://phimmoi.gay/storage/images/onibaba/onibaba-thumb.jpg', 'https://www.youtube.com/embed/s2SkmwgU8qs?si=TTlZEsVBiooqAnMK'),
('5-achx', N'Anh chàng hàng xóm', N'Mô tả về Phim 5', 0, N'Hành động', N'Rob Cohen', N'Jennifer Lopez, Ryan Guzman', N'Mỹ', '2020-05-05', 'https://phimmoi.gay/storage/images/anh-chang-hang-xom/anh-chang-hang-xom-thumb.jpg', 'https://www.youtube.com/embed/vV77Sz3qFrI?si=w-4BUlwbr5YdSTnM'),
('6-cbaf', N'CẬU BÉ ARTEMIS FOWL', N'Mô tả về Phim 6', 0, N'Hành động', N'Eoin Colfer', N'Ferdia Shaw, Lara McDonnell ,Josh Gad, Colin Farrell', N'Mỹ', '2021-06-06', 'https://phimmoi.gay/storage/images/cau-be-artemis-fowl/iwSUqrUZn0kqLT1GRHX2fY1wi7I.jpg9mcXR.jpg', 'https://www.youtube.com/embed/qDRPO466ANA?si=Totdc1QjeAvnTFBV'),
('7-d', N'Diana', N'Mô tả về Phim 7', 0, N'Hành động', N'Oliver Hirschbiegel', N'Naomi Watts, Douglas Hodge, Juliet Stevenson', N'Anh', '2022-07-07', 'https://phimmoi.gay/storage/images/diana/diana-thumb.jpg', 'https://www.youtube.com/embed/WllZh9aekDg?si=4WM0USP91pK_q3l9'),
('8-nnln', N'Những Nhà Leo Núi', N'Mô tả về Phim 8', 0, N'Hành động', N'Lý Nhân Cảng', N'Tử Di, Hồ Ca, Ngô Kinh, Thành Long, Tỉnh Bách Nhiên, và Trương Địch', N'Nhật Bản', '2023-08-08', 'https://phimmoi.gay/storage/images/nhung-nha-leo-nui/eRaUlATfycdhVcF0MRwD1X0qbHk.jpg', 'https://www.youtube.com/embed/eYxCcQk0J-I?si=D-5LLNvZMIyXZqrH'),
('9-wc', N'Web Chìm', N'Mô tả về Phim 9', 0, N'Hành động', N'Alex Winter', N'Keanu Reeves, Ross Ulbricht, Cody Wilson, Lyn Ulbricht, Kirk Ulbricht', N'Mỹ', '2020-09-09', 'https://phimmoi.gay/storage/images/web-chim/deep-web-thumb.jpg', 'https://www.youtube.com/embed/BvC9oDlT8mM?si=mBJYCmjbAX-Siuda'),
('10-sth', N'Sát thủ hào', N'Mô tả về Phim 10', 0, N'Hành động', N'Gary McKendry', N'Jason Statham, Clive Owen, Robert De Niro', N'Anh', '2021-10-10', 'https://phimmoi.gay/storage/images/sat-thu-hao/sat-thu-hao-thumb.jpg', 'https://www.youtube.com/embed/1zX_9xnComI?si=6BRramxFVko-jQQ0'),
('11-msty', N'Men Say Tình Yêu', N'Mô tả về Phim 11', 1, N'Tình cảm', N'Park Seon-ho', N'Chae Yong-ju, Yun Min-ju', N'Hàn Quốc', '2021-10-10', 'https://phimmoi.gay/storage/images/men-say-tinh-yeu/men-say-tinh-yeu-thumb.jpg', 'https://www.youtube.com/embed/3c9VmrTjiso?si=kHPs5PnQ7Upc4aum');


INSERT INTO MovieUrls_64130299 (MovieUrlId,MovieId,Url)
VALUES
('1-tan-the-z-khoi-nguon-dai-dich','1-ttzkndd','https://embed12.streamc.xyz/embed.php?hash=e9c15d20759c2eaf41f29963d3af041d'),
('2-ve-que-an-tet','2-vqat','https://vip.opstream14.com/share/d57c3910d36def0f811078b484fd8530'),
('3-thoat-khoi-xien-xich','3-tkxx','https://player.phimapi.com/player/?url=https://s5.phim1280.tv/20241106/IwUu5wHo/index.m3u8'),
('4-onibaba','4-o','https://vip.opstream11.com/share/60b5b22c8caf9f96a50b9de57355be83'),
('5-anh-chang-hang-xom','5-achx','https://vip.opstream15.com/share/eb160de1de89d9058fcb0b968dbbbd68'),
('6-cau-be-artermis-fowl','6-cbaf','https://embed.streamc.xyz/embed.php?hash=8ee8d98028d075ee13a044567a89544b'),
('7-diana','7-d','https://vip.opstream12.com/share/fd272fe04b7d4e68effd01bddcc6bb34'),
('8-nhung-nha-leo-nui','8-nnln','https://embed.streamc.xyz/embed.php?hash=4d114977fc9d007616944ae3097ee376'),
('9-web-chim','9-wc','https://embed.streamc.xyz/embed.php?hash=46d25f0404c750d53dba92e4fff8ee98'),
('10-sat-thu-hao','10-sth','https://vip.opstream11.com/share/674bfc5f6b72706fb769f5e93667bd23'),
('11-men-say-tinh-yeu-tap-1','11-msty','https://player.phimapi.com/player/?url=https://s4.phim1280.tv/20241104/vxFw722R/index.m3u8'),
('12-men-say-tinh-yeu-tap-2','11-msty','https://player.phimapi.com/player/?url=https://s5.phim1280.tv/20241105/z39OzEuB/index.m3u8');

-- Thêm dữ liệu vào bảng Ratings
INSERT INTO Rating_64130299 (RatingId, UserId, MovieId, Rating)
VALUES
('1-rating', '1-nguyen1', '1-ttzkndd', 5),
('2-rating', '2-nguyen2', '2-vqat', 4),
('3-rating', '3-nguyen3', '3-tkxx', 3);

-- Thêm dữ liệu vào bảng Comments
INSERT INTO Comment_64130299 (CommentId, UserId, MovieId, Content)
VALUES
('1-comment', '1-nguyen1', '1-ttzkndd', N'Phim rất hấp dẫn và đầy bất ngờ!'),
('2-comment', '2-nguyen2', '2-vqat', N'Phim đáng xem cho những người thích hành động.');

-- Thêm dữ liệu vào bảng Favorites
INSERT INTO Favorite_64130299 (FavoriteId, UserId, MovieId)
VALUES
('1-favorite', '1-nguyen1', '1-ttzkndd'),
('2-favorite', '2-nguyen2', '2-vqat');

-- Thêm dữ liệu vào bảng WatchHistory
INSERT INTO WatchHistory_64130299 (HistoryId, UserId, MovieId, LastWatchedTime)
VALUES
('1-history', '1-nguyen1', '1-ttzkndd', '2024-11-06 14:30:00'),
('2-history', '2-nguyen2', '2-vqat', '2024-11-06 15:00:00');









