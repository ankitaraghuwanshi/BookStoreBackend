---Create book table
create table Books(
BookId int identity (1,1)primary key,
BookName varchar(255),
AuthorName varchar(255),
Rating int,
TotalView int,
OriginalPrice decimal,
DiscountPrice decimal,
BookDetails varchar(255),
BookImage varchar(255),
);

create procedure AddBook
(
@BookName varchar(255),
@authorName varchar(255),
@rating int,
@totalView int,
@originalPrice Decimal,
@discountPrice Decimal,
@BookDetails varchar(255),
@bookImage varchar(255)
)
as
BEGIN
Insert into Books(BookName, authorName, rating, totalview, originalPrice, 
discountPrice, BookDetails, bookImage)
values (@bookName, @authorName, @rating, @totalView ,@originalPrice, @discountPrice,
@BookDetails, @bookImage);
End;