Create database MovieTickets;
Use MovieTickets;
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
	Name Nvarchar(100) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    Password NVARCHAR(100) NOT NULL
);
Insert into Users Values('Bashat Parween','bashat@gmail.com','bashat123');

Create procedure SP_GetAllDetails
As
Begin 
Select * from Users;
End;

Execute SP_GetAllDetails;

Create procedure SP_GetDetails
@Email nvarchar(100),
@Password nvarchar(100)
As
Begin
Select * from Users
where Email=@Email and Password=@Password;
End;

Execute SP_GetDetails @Email='bashat@gmail.com',@Password='bashat123';

Create procedure SP_InsertDetails
@Name nvarchar(100),
@Email nvarchar(100),
@Password nvarchar(100)
As
Begin
Insert Into Users(Name,Email,Password) Values
(@Name,@Email , @Password)
End;

Execute SP_InsertDetails 
'Piya Agarwal','piya@gmail.com','piya123';

Create procedure SP_UpdateDetails
@Id int,
@Name nvarchar(100),
@Email nvarchar(100),
@Password nvarchar(100)
As
Begin
Update Users Set Name=@Name,Email=@Email,Password=@Password
Where Id=@Id;
End;

Execute SP_UpdateDetails
@Id=2,@Name='Neha Gupta',@Email='neha@gmail.com',@Password='neha123';


