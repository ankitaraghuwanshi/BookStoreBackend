------------------ Create  table for the Cart------------
Create Table Cart
(
CartId int identity(1,1) primary key,
BookQuantity int default 1,
UserId int not null foreign key (UserId) references Users(UserId),
BookId int not null Foreign key (BookId) references BooksTable(BookId)
)

select  *  From Cart

----------------- Create procedure for the Add to cart -----------


Create procedure spAddCart
(@BookQuantity int,
@UserId int,
@BookId int
)
As
Begin
	insert into Cart(BookQuantity,UserId,BookId)
	values ( @BookQuantity,@UserId, @BookId);
End

------------------------- sp for the Removeing book from Cart -----------------

Create proc spDeleteFromCart
(
@CartId int
)
As
Begin
	delete from Cart where CartId = @CartId;
end
  

 