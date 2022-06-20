------------------feadback table-----------------------



create Table FeedbackTable
(
	FeedbackId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Comment varchar(max) not null,
	Rating decimal not null,
	BookId int not null FOREIGN KEY (BookId) REFERENCES BooksTable(BookId),
	UserId INT NOT NULL FOREIGN KEY (UserId) REFERENCES Users(UserId),
);



select * from FeedbackTable;
---------------------------creatinf sp for adding feedback---------------------

Create Proc spAddFeedback
(
	@Comment varchar(max),
	@Rating decimal,
	@BookId int,
	@UserId int
)
as
Declare @AverageRating int;
BEGIN
	IF (EXISTS(SELECT * FROM FeedbackTable WHERE BookId = @BookId and UserId=@UserId))
		select 1;
	Else
	Begin
		IF (EXISTS(SELECT * FROM BooksTable WHERE BookId = @BookId))
		Begin  select * from FeedbackTable
			Begin try
				Begin transaction
					Insert into FeedbackTable (Comment, Rating, BookId, UserId) values(@Comment, @Rating, @BookId, @UserId);		
					set @AverageRating = (Select AVG(Rating) from FeedbackTable where BookId = @BookId);
					Update BooksTable set Rating = @AverageRating
								 where  BookId = @BookId;
				Commit Transaction
			End Try
			Begin catch
				Rollback transaction
			End catch
		End
		Else
		Begin
			Select 2; 
		End
	End
END

-----------------------------sp for Get All Feedback --------------------------------

Create Proc spGetAllFeedback
(
	@BookId int
)
as
BEGIN
	Select FeedbackTable.FeedbackId, FeedbackTable.UserId, FeedbackTable.BookId,FeedbackTable.Comment,FeedbackTable.Rating, Users.FullName
	From Users
	Inner Join FeedbackTable
	on FeedbackTable.UserId = Users.UserId
	where
	 BookId = @BookId;
END;