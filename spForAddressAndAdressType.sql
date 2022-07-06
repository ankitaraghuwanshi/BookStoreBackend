----------------- Address Table------------------
create table  AddressTable
(
	AddressId int identity(1,1) primary key,
	Address varchar(max) not null,
	City varchar(100) not null,
	State varchar(100) not null,
	TypeId int not null 
	FOREIGN KEY (TypeId) REFERENCES AddressType(TypeId),
	UserId INT not null
	FOREIGN KEY (UserId) REFERENCES Users(UserId),
);

create table  AddressType
(
	
	TypeId int identity(1,1) primary key,
	AddressType Varchar(max) not null
);
 insert into  AddressType
 Values ('Home'),('Office'),('Others');
 ----------------sp for addaddress---------------
 Create procedure spAddAddress     
(        
    @Address varchar(255),
    @City varchar(255),
    @State varchar(255),
    @TypeId varchar(255),  
	@UserId varchar(255)
)        
as         
Begin         
    Insert into AddressTable (Address,city,state,TypeId,UserId)         
    Values (@Address,@City,@State,@TypeId,@UserId);        
End
--------------spDeleteAddress-----------

create procedure spDeleteAddress
(
@UserId varchar(255),
@AddressId varchar(255)
)
as
begin
delete from AddressTable where AddressId = @AddressId and UserId =@UserId;
                            
End;

---------------------spUpdateAddress--------------
 create proc spforUpdateAddress
(
	@Address varchar(255),
    @City varchar(255),
    @State varchar(255),
    @TypeId varchar(255),  
	@UserId varchar(255),
	@AddressId varchar(255)
)
as
begin

update AddressTable set Address =@Address,
				City =@City,
				State  =@State,
				TypeId =@TypeId
				where UserId=@UserId and AddressId=@Addressid;
end;

------------------sp for get all adresses---------------------
alter Proc GetAllAddresses
(
	@UserId int
)
as
begin
	select Address, City, State, a.UserId, b.TypeId,AddressId
	from AddressTable a
    Inner join AddressType b on b.TypeId = a.TypeId 
	where 
	UserId = @UserId;
end;
