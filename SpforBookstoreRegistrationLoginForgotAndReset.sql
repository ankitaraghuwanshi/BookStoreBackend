create database BookStore;--database creation

create table Users  --creating table
(
UserId int IDENTITY(1,1) PRIMARY KEY,
FullName varchar(255),
Email varchar(255),
Password varchar(255),
MobileNumber bigint
);
select * from Users;


-----------for registration---------

Create procedure spUserRegister     --creating sp for registration  
(        
    @FullName varchar(255),
    @Email varchar(255),
    @Password varchar(255),
    @MobileNumber bigint       
)        
as         
Begin         
    Insert into Users (FullName,Email,Password,MobileNumber)         
    Values (@FullName,@Email,@Password,@MobileNumber);        
End


------------for login---------

create procedure spUserLogin
(
@Email varchar(255),
@Password varchar(255)
)
as
begin
select * from Users
where Email = @Email and Password = @Password
End;
-----------for forgot password------
create procedure spUserForgotPassword
(
@Email varchar(255)
)
as
begin
Update Users
set Password = 'Null'
where Email = @Email;
select * from Users where Email = @Email;
End;

------spfor reset password-------

create procedure spUserResetPassword
(
@Email varchar(250),
@Password varchar(250)
)
AS
BEGIN
UPDATE Users 
SET 
Password = @Password 
WHERE Email = @Email;
End;
