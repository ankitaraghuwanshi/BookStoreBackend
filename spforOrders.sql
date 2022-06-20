-------------------------created table for orders----------------

create table OrdersTable

(
	OrderId int identity(1,1) not null primary key,
	TotalPrice int not null,
	BookQuantity int not null,
	OrderDate Date not null,
	UserId INT NOT NULL FOREIGN KEY REFERENCES Users(UserId),
	BookId INT NOT NULL FOREIGN KEY REFERENCES BooksTable(BookId),
	AddressId int not null FOREIGN KEY REFERENCES AddressTable(AddressId)
);
select * from OrdersTable;

-------------------------sp for Add order----------------------
Create Proc spAddOrders
(
	@BookQuantity int,
	@UserId int,
	@BookId int,
	@AddressId int
)
as
Declare @TotalPrice int
BEGIN
	set @TotalPrice = (select DiscountPrice from BooksTable where BookId = @BookId);
	
			If(Exists (Select * from Bookstable where BookId = @BookId))
				BEGIN
					Begin try
						Begin Transaction
						Insert Into OrdersTable(TotalPrice, BookQuantity, OrderDate, UserId, BookId, AddressId)
						Values(@TotalPrice*@BookQuantity, @BookQuantity, GETDATE(), @UserId, @BookId, @AddressId);
						Update BooksTable set Quantity= Quantity-@BookQuantity where BookId = @BookId;
						Delete from Cart where BookId = @BookId and UserId = @UserId;
						select * from OrdersTable;
						commit Transaction
					End try
					Begin Catch
							rollback;
					End Catch
				END
			
	Else
		Begin
			Select 2;
		End
END;

-------------------Get All Order SP----------------------------

Create Proc spGetAllOrders
(
	@UserId int
)
as
begin
		Select 
		OrdersTable.OrderId,
		OrdersTable.UserId, 
		OrdersTable.AddressId,
		BooksTable.BookId,
		OrdersTable.TotalPrice,
		OrdersTable.BookQuantity,
		OrdersTable.OrderDate,
		BooksTable.BookName, 
		BooksTable.AuthorName, 
		BooksTable.BookImage
		FROM BooksTable 
		inner join OrdersTable on OrdersTable.BookId = BooksTable.BookId 
		where 
			OrdersTable.UserId = @UserId;
END
