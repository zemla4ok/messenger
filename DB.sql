CREATE TABLE Users
	(Name nvarchar(20),
	SurName nvarchar(20),
	NickName nvarchar(20) primary key,
	Gender nvarchar(10),
	Birthday nvarchar(12),
	Country nvarchar(15),
	City nvarchar(15),
	Pass nvarchar(36));

SELECT * FROM Users;

DELETE Users

CREATE TABLE Chats (ChatName nvarchar(100) primary key,
	User1 nvarchar(20),
	User2 nvarchar(20),
	User3 nvarchar(20),
	User4 nvarchar(20),
	User5 nvarchar(20))

DROP TABLE Chats

SELECT * FROM Chats
SELECT * FROM Msgs

DELETE Msgs
DELETE Chats

CREATE TABLE Msgs
	(ChatName nvarchar(100) foreign key(ChatName) references Chats(ChatName),
	UserName nvarchar(20) foreign key(UserName) references Users(NickName),
	Msg nvarchar(255),
	TImeOfMsg datetime)


INSERT INTO Msgs (ChatName, UserName, Msg, TImeOfMsg) 
	values('qa', 'a', 'i"m fine', GETDATE())

SELECT Msgs.ChatName, Msgs.Msg FROM Msgs inner join Chats
	ON Msgs.ChatName = Chats.ChatName
	WHERE Msgs.ChatName=Chats.ChatName and 
	(Chats.User1 = 'q' or Chats.User2 = 'q' or Chats.User3 = 'q' or Chats.User4 = 'q' or Chats.User5 = 'q') ORDER BY Msgs.TImeOfMsg DESC



SELECT * FROM Chats 


SELECT * FROM  Chats
	WHERE User1 = 'q' or User2 = 'q' or User3 = 'q' or User4 = 'q' or User5 = 'q'
	