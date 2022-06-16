create Table AdminTable-------created admin table---
(
AdminId int primary key identity(1,1),
FullName varchar(255),
Email varchar(255),
Password varchar(255),
PhoneNumber Bigint
);
-------admin is onlyone so inserted admin details directly here----
Insert into AdminTable values('Ankitaraghu', 'bittanraghu@gmail.com', 'bittan@123', '9752701428');
select * from AdminTable

--------sp for admin login-------
create procedure AdminLogin
(
@Email varchar(255),
@Password varchar(255)
)
as
BEGIN
If(Exists(select * from AdminTable where Email = @Email and Password = @Password))
Begin
select AdminId, FullName, Email,Password, PhoneNumber from AdminTable;
end
Else
Begin
select 2;
End
END;