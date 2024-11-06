/*CREATE DATABASE MovieDatabase_64130299;
USE MovieDatabase_64130299;
DROP DATABASE MovieDatabase_64130299;*/

/*THIẾT KẾ CƠ SỞ DỮ LIỆU*/
-- Tạo bảng User
CREATE TABLE User_64130299 (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Email NVARCHAR(256) UNIQUE NOT NULL,
    Password NVARCHAR(256) NOT NULL,
    Username NVARCHAR(50) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()
);

-- Tạo bảng Movies
CREATE TABLE Movie_64130299 (
    MovieId VARCHAR(100) PRIMARY KEY,
    Title NVARCHAR(256) NOT NULL,
    Description NVARCHAR(MAX),
	GenreId BIT DEFAULT(1), //0 thì là phim lẻ,1 là phim bộ
	GenreName NVARCHAR(100),
	DirectorName NVARCHAR(256),
	ActorName NVARCHAR(256)
    ReleaseDate DATE,
    PosterUrl NVARCHAR(512),
    TrailerUrl NVARCHAR(512),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()
);
CREATE TABLE MovieUrls_64130299 (
    MovieUrlId VARCHAR(100) PRIMARY KEY,
    MovieId VARCHAR(100),
    Url NVARCHAR(512),
    FOREIGN KEY (MovieId) REFERENCES Movie_64130299(MovieId)
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


INSERT INTO User_64130299 (Email, Password, Username)
VALUES
('user1@example.com', 'password123', 'nguyen1'),
('user2@example.com', 'password123', 'nguyen2'),
('user3@example.com', 'password123', 'nguyen3'),
('user4@example.com', 'password123', 'nguyen4'),
('user5@example.com', 'password123', 'nguyen5'),
('user6@example.com', 'password123', 'nguyen6'),
('user7@example.com', 'password123', 'nguyen7'),
('user8@example.com', 'password123', 'nguyen8'),
('user9@example.com', 'password123', 'nguyen9'),
('user10@example.com', 'password123', 'nguyen10');


INSERT INTO Movie_64130299 (MovieId,Title,Description,GenreId,GenreName,DirectorName,ActorName,ReleaseDate,PosterUrl,TrailerUrl)
VALUES
('1-ttzkndd'.'Tận Thế Z: Khởi Nguồn Đại Dịch', 'Mô tả về Phim 1',0,'Hành động','Alexandre Bruguès','Richard Armitage, Ana de Armas','2020-01-01', 'https://ohaytv.com/storage/hinh-anh/jaBToJ1DZcwn5wOsQeLOXFVlBLn.jpg', 'https://www.youtube.com/embed/pPvaIGldT-I?si=n-EXlY1Usw3rAur5'),
('2-vqat','Về quê ăn Tết', 'Mô tả về Phim 2',0,'Hành động','Nguyễn Hoàng Anh', 'Ngô Thanh Vân, Jun Phạm', '2021-02-02', 'https://ohaytv.com/storage/hinh-anh/ve-que-an-tet-thumb.jpg', 'https://www.youtube.com/embed/HUxQIqTvqIU?si=X6n20Vq7Ad533rXP'),
('3-tkxx','Thoát Khỏi Xiềng Xích', 'Mô tả về Phim 3',0,'Hành động','M. Night Shyamalan','James McAvoy, Anya Taylor-Joy', '2022-03-03','https://phimmoi.gay/storage/images/shaggy-scooby-doo-get-a-clue-phan-1-172439095333179/e9dcc9b2b631d38c6e099c0d5d9f7884.jpg', 'https://www.youtube.com/embed/Sbl6vozFEaI?si=006as2XkowZm35Je'),
('4-o','Onibaba', 'Mô tả về Phim 4',0,'Hành động','Kaneto Shindo','Nobuko Otowa, Jitsuko Yoshimura, Kei Satô', '2023-04-04','https://phimmoi.gay/storage/images/onibaba/onibaba-thumb.jpg', 'https://www.youtube.com/embed/s2SkmwgU8qs?si=TTlZEsVBiooqAnMK',),
('5-achx','Anh chàng hàng xóm', 'Mô tả về Phim 5',0,'Hành động','Rob Cohen','Jennifer Lopez, Ryan Guzman', '2020-05-05','https://phimmoi.gay/storage/images/anh-chang-hang-xom/anh-chang-hang-xom-thumb.jpg', 'https://www.youtube.com/embed/vV77Sz3qFrI?si=w-4BUlwbr5YdSTnM%22'),
('6-cbaf','CẬU BÉ ARTEMIS FOWL', 'Mô tả về Phim 6',0,'Hành động','Eoin Colfer','Ferdia Shaw, Lara McDonnell ,Josh Gad, Colin Farrell', '2021-06-06','https://phimmoi.gay/storage/images/cau-be-artemis-fowl/iwSUqrUZn0kqLT1GRHX2fY1wi7I.jpg9mcXR.jpg', 'https://www.youtube.com/embed/qDRPO466ANA?si=Totdc1QjeAvnTFBV'),
('7-d','Diana', 'Mô tả về Phim 7',0,'Hành động','Oliver Hirschbiegel','Naomi Watts, Douglas Hodge, Juliet Stevenson', '2022-07-07','https://phimmoi.gay/storage/images/diana/diana-thumb.jpg', 'https://www.youtube.com/embed/WllZh9aekDg?si=4WM0USP91pK_q3l9'),
('8-nnln','Những Nhà Leo Núi', 'Mô tả về Phim 8',0,'Hành động','Lý Nhân Cảng','ử Di, Hồ Ca, Ngô Kinh, Thành Long, Tỉnh Bách Nhiên, và Trương Địch', '2023-08-08','https://phimmoi.gay/storage/images/nhung-nha-leo-nui/eRaUlATfycdhVcF0MRwD1X0qbHk.jpg', 'https://www.youtube.com/embed/eYxCcQk0J-I?si=D-5LLNvZMIyXZqrH'),
('9-wc','Web Chìm', 'Mô tả về Phim 9',0,'Hành động','Alex Winter','Keanu Reeves, Ross Ulbricht, Cody Wilson, Lyn Ulbricht, Kirk Ulbricht' '2020-09-09','https://phimmoi.gay/storage/images/web-chim/deep-web-thumb.jpg', 'https://www.youtube.com/embed/BvC9oDlT8mM?si=mBJYCmjbAX-Siuda'),
('10-sth','Sát thủ hào', 'Mô tả về Phim 10',0,'Hành động','Gary McKendry','Jason Statham, Clive Owen, Robert De Niro','2021-10-10','https://phimmoi.gay/storage/images/sat-thu-hao/sat-thu-hao-thumb.jpg', 'https://www.youtube.com/embed/1zX_9xnComI?si=6BRramxFVko-jQQ0'),
('11-msty','Men Say Tình Yêu', 'Mô tả về Phim 11',1,'Tình cảm','Park Seon-ho','Chae Yong-ju, Yun Min-ju','2021-10-10','https://phimmoi.gay/storage/images/men-say-tinh-yeu/men-say-tinh-yeu-thumb.jpg', 'https://www.youtube.com/embed/3c9VmrTjiso?si=kHPs5PnQ7Upc4aum');

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









