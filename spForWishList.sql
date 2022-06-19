-----Creating WishList Table---------
create table WishList
(
	WishListId int identity(1,1) not null primary key,
	UserId int foreign key references Users(UserId) on delete no action,
	BookId int foreign key references BooksTable(BookId) on delete no action
);

select * from WishList


----Add WishList in Stored Procedure------

create proc spAddWishList
(
@UserId int,
@BookId int
)
as
begin 
       insert into WishList
	   values (@UserId,@BookId);
end;

---------------------sp  for delete from wishlist------------

create proc DeleteFromWishList
(
@WishListId int,
@UserId int
)
as
begin
delete WishList where WishListId = @WishListId and UserId=@UserId;
end;


-------------------sp for GetWishList by Userid -----

create proc spGetWishListByUserId
(
	@UserId int
)
as
begin
	select WishListId,
	UserId,
	c.BookId,
	BookName,
	AuthorName,
	DiscountPrice,
	OriginalPrice,
	BookImage from WishList c join BooksTable b on c.BookId=b.BookId 
	where UserId=@UserId;
end;