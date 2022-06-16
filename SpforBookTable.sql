----------------Create book table---------------------
create table BooksTable(
BookId int identity (1,1)primary key,
BookName varchar(255),
AuthorName varchar(255),
Rating int,
TotalView int,
OriginalPrice decimal,
DiscountPrice decimal,
BookDetails varchar(255),
BookImage varchar(255),
Quantity int
);
 
 select * from BooksTable
 -------------------------sp for adding book ----------
create procedure AddBookTable
(
@BookName varchar(255),
@authorName varchar(255),
@rating int,
@totalView int,
@originalPrice Decimal,
@discountPrice Decimal,
@BookDetails varchar(255),
@bookImage varchar(255),
@Quantity int
)
as
BEGIN
Insert into BooksTable(BookName, authorName, rating, totalview, originalPrice, 
discountPrice, BookDetails, bookImage, Quantity)
values (@bookName, @authorName, @rating, @totalView ,@originalPrice, @discountPrice,
@BookDetails, @bookImage, @Quantity);
End;

-----------------spfor Updatebook-----------------
create procedure UpdateBooktable
(
@BookId int,
@BookName varchar(255),
@authorName varchar(255),
@rating int,
@totalView int,
@originalPrice Decimal,
@discountPrice Decimal,
@BookDetails varchar(255),
@bookImage varchar(255),
@Quantity int

)

as
BEGIN
Update BooksTable set BookName = @BookName, 
authorName = @authorName,
rating=@rating,
totalView=@totalView,
originalPrice= @originalPrice,
discountPrice = @discountPrice,
BookDetails = @BookDetails,
bookImage = @bookImage,
Quantity = @Quantity
where BookId = @BookId;
End;

----------------------------------sp for Delete Book--------------------
Alter procedure spDeleteBooks
(
@BookId int
)
As
Begin
 delete from BooksTable where BookId = @BookId ;
end

---------sp for get all books----------------------------
create procedure spGetAllBook
as
BEGIN
	select * from BooksTable;
End;
----------------sp for get book by bookid-----------------

create procedure spGetBookByBookId
(
@bookId int
)
as
BEGIN
select * from BooksTable
where bookId = @bookId;
End;