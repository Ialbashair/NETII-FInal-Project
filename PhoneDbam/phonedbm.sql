/* CHeck to see if the database exists, if so, drop it */
IF EXISTS (SELECT 1 FROM master.dbo.sysdatabases
				WHERE name = 'phonedbam')
BEGIN
	DROP DATABASE phonedbam
	print '' print '*** Dropping database phonedbam ***'
END
GO

print '' print '*** creating database phonedbam ***'
GO
CREATE DATABASE phonedbam
GO

print '' print '**** using database phonedbam ***'
GO
USE [phonedbam]
GO

/* Employee Table */
print '' print ' **** creating employee table ****'
GO
CREATE TABLE [dbo].[Employee] (
	[EmployeeID]	[int] IDENTITY (100000, 1)	NOT NULL,
	[GivenName] 	[nvarchar] (50)				NOT NULL,
	[FamilyName]	[nvarchar] (50)				NOT NULL,
	[Email]			[nvarchar] (100)			NOT NULL,
	[Phone]			[nvarchar] (11)				NOT NULL,
	[PasswordHash]	[nvarchar] (100)			NOT NULL DEFAULT
		'9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e',
	[Active]		[bit]						NOT NULL DEFAULT 1,
	CONSTRAINT [pk_EmployeeID] PRIMARY KEY ([EmployeeID]),
	CONSTRAINT [ak_Email] UNIQUE ([Email])
)
GO



print '' print ' **** insterting employee test records ****'
GO
INSERT INTO [dbo].[Employee]
		([GivenName], [FamilyName], [Phone], [Email])
	VALUES
		('Joanne', 'Smith', '3195551111', 'joanne@company.com'),
		('Martin', 'Jones', '3195552222', 'martin@company.com'),
		('Leo', 'Williams', '3195553333', 'leo@company.com'),
		('Maria', 'Perez', '3195554444', 'maria@company.com'),
		('Ahmed', 'Rawi', '3195555555', 'ahmed@company.com')
GO

print '' print ' **** creating status table ****'
GO
CREATE TABLE [dbo].[Status] (
	[StatusID] 	[nvarchar] (25)				NOT NULL,
	CONSTRAINT [pk_Status] PRIMARY KEY ([StatusID])
)
GO

print '' print ' **** insterting status test records ****'
GO
INSERT INTO [dbo].[Status]
		([StatusID])
	VALUES
		('Ready'),
		('Maintenance'),
		('Sold')
GO

/* Role Table */
print '' print ' **** creating role table ****'
GO
CREATE TABLE [dbo].[Role] (
	[RoleID]		[nvarchar](50),
	CONSTRAINT [pk_RoleID] PRIMARY KEY ([RoleID])
)
GO

print '' print ' **** insterting role test records ****'
GO
INSERT INTO [dbo].[Role]
		([RoleID])
	VALUES
		('Sale'),
		('CheckOut'),
		('CheckIn'),
		('Maintenance'),
		('Prep'),
		('Manager'),
		('Customer'),
		('Admin')	
GO


print '' print ' **** creating customer table ****'
GO
CREATE TABLE [dbo].[Customer] (
	[CustomerID]	[int] IDENTITY (100000, 1)	NOT NULL,
	[GivenName] 	[nvarchar] (50)				NOT NULL,
	[FamilyName]	[nvarchar] (50)				NOT NULL,
	[Email]			[nvarchar] (250)			NOT NULL,
	[Phone]			[nvarchar] (11)				NOT NULL,
	CONSTRAINT [pk_CustomerID] PRIMARY KEY ([CustomerID]),
	CONSTRAINT [ak_CustomerEmail] UNIQUE ([Email])
)
GO

print '' print ' **** insterting customer test records ****'
GO
INSERT INTO [dbo].[Customer]
		( [GivenName], [FamilyName], [Email], [Phone])
	VALUES
		( 'David', 'Goggins', 'davidgoggins@gmail.com' , '1923456789'),
		( 'Britney' , 'Spears', 'spearsbritney@gmail.com', '1892347898'),
		( 'Josh', 'Freeman', 'freemanjosh@yahoo.com', '1234567891')
GO

print '' print ' **** creating typetable table ****'
GO
CREATE TABLE [dbo].[TypeTable] (
	[TypeID]  		[int] IDENTITY (100000, 1) NOT NULL,
	[Make]			[nvarchar] (25)		  	   NOT NULL,
	[Model]			[nvarchar] (25)			   NOT NULL,
	[MakeYear]		[int]					   NOT NULL,
	CONSTRAINT [pk_Type] PRIMARY KEY ([TypeID])
)

print '' print ' **** insterting type test records ****'
GO
INSERT INTO [dbo].[TypeTable]
		( [Make], [Model], [MakeYear])
	VALUES
		( 'Apple', 'Iphone 12 PRO MAX', 2021),
		( 'Apple', 'Iphone 11', 2020),
		( 'Apple', 'Iphone 13 PRO', 2022),
		( 'Samsung', 'Galaxy s18', 2018),
		( 'Google', 'Pixel', 2021)
GO

print '' print ' **** creating phone table ****'
GO
CREATE TABLE [dbo].[Phone] (
	[PhoneID]  		[int] IDENTITY (100000, 1) NOT NULL,
	[Make]			[nvarchar] (25)		  	   NOT NULL,
	[Model]			[nvarchar] (25)			   NOT NULL,
	[MakeYear]		[int]					   NOT NULL,
	[Status]		[nvarchar] (25)			   NOT NULL,
	[Price]			[Float]					NOT NULL
	CONSTRAINT [pk_Phone] PRIMARY KEY ([PhoneID])
)

print '' print ' **** insterting Phone test records ****'
GO
INSERT INTO [dbo].[Phone]
		( [Make], [Model], [MakeYear], [Status], [Price])
	VALUES
		( 'Apple', 'Iphone 12 PRO MAX', 2021, 'Ready', 478.18),	
		( 'Apple', 'Iphone 11 PRO', 2020, 'Ready', 293.99),
		( 'Apple', 'Iphone 12 PRO', 2021, 'Ready', 385.19),
		( 'Apple', 'Iphone 10 PRO MAX', 2019, 'Ready', 203.22),
		( 'Apple', 'Iphone 13 PRO MAX', 2023, 'Ready', 601.15),
		( 'Apple', 'Iphone 13 MAX', 2023, 'Ready', 518.94),
		( 'Apple', 'Iphone 13 PRO', 2023, 'Ready', 629.99),
		( 'Apple', 'Iphone 8 MAX', 2018, 'Ready', 97.00),
		( 'Apple', 'Iphone 12 PRO MAX', 2021, 'Maintenance', 487.18),
		( 'Samsung', 'Galaxy s23', 2019, 'Maintenance', 666.00),
		( 'Samsung', 'Galaxy s22', 2019, 'Ready', 599.00),
		( 'Google', 'Pixel 7', 2020, 'Sold', 699.00)
GO


print '' print ' **** creating maintenance table ****'
GO
CREATE TABLE [dbo].[Maintenance] (
	[MaintenanceID]	[int] IDENTITY (100000, 1)	NOT NULL,
	[EmployeeID] 	[int] 						NOT NULL,
	[PhoneID]		[int]						NOT NULL,
	[Date_Time]		[datetime]					NOT NULL,
	CONSTRAINT [fk_Maintenance_EmployeeID] FOREIGN KEY ([EmployeeID])
		REFERENCES [dbo].[Employee] ([EmployeeID]),
	CONSTRAINT [fk_Maintenance_PhoneID] FOREIGN KEY ([PhoneID])
		REFERENCES [dbo].[Phone] ([PhoneID]),
	CONSTRAINT [pk_Maintenance] PRIMARY KEY ([MaintenanceID])
	
)
GO

print '' print ' **** creating EmployeeRole table ****'
GO
CREATE TABLE [dbo].[EmployeeRole] (
	[EmployeeID]	[int]					NOT NULL,
	[RoleID]		[nvarchar](50)			NOT NULL,
	CONSTRAINT [fk_EmployeeRole_EmployeeID] FOREIGN KEY ([EmployeeID])
		REFERENCES [dbo].[Employee] ([EmployeeID]),
	CONSTRAINT [fk_EmployeeRole_RoleID] FOREIGN KEY ([RoleID])
		REFERENCES [dbo].[Role] ([RoleID]),
	CONSTRAINT [pk_EmployeeRole] PRIMARY KEY([EmployeeID], [RoleID])
)
GO

print '' print ' **** inserting EmployeeRole test records ****'
GO
INSERT INTO [dbo].[EmployeeRole]
		([EmployeeID], [RoleID])
	VALUES
		(100000, 'Admin'),
		(100000, 'Manager'),				
		(100002, 'CheckOut'),
		(100002, 'Sale'),
		(100003, 'Prep'),
		(100003, 'CheckIn'),
		(100004, 'Maintenance')
GO

print '' print ' **** creating mline table ****'
GO
CREATE TABLE [dbo].[Mline] (
	[MaintenanceID] [int] 						NOT NULL,
	[PhoneID]		[int]						NOT NULL,	
	[Description] 	[nvarchar] (512)			NOT NULL,
	CONSTRAINT [fk_Mline_MaintenanceID] FOREIGN KEY ([MaintenanceID])
		REFERENCES [dbo].[Maintenance] ([MaintenanceID]),
	CONSTRAINT [fk_Mline_PhoneID] FOREIGN KEY ([PhoneID])
		REFERENCES [dbo].[Phone] ([PhoneID])
)
GO

print '' print ' **** creating prep table ****'
GO
CREATE TABLE [dbo].[Prep] (
	[PrepID]		[int] IDENTITY (100000, 1) 	NOT NULL,
	[EmployeeID] 	[int] 						NOT NULL,
	[PhoneID]		[int]						NOT NULL, 
	[Date_Time]		[datetime]					NOT NULL,
	CONSTRAINT [fk_Prep_EmployeeID] FOREIGN KEY ([EmployeeID])
		REFERENCES [dbo].[Employee] ([EmployeeID]),
	CONSTRAINT [fk_Prep_PhoneID] FOREIGN KEY ([PhoneID])
		REFERENCES [dbo].[Phone] ([PhoneID]),
	CONSTRAINT [pk_Prep] PRIMARY KEY([PrepID])
)
GO

print '' print ' **** creating sale table ****'
GO
CREATE TABLE [dbo].[Sale] (
	[SaleID]		[int] IDENTITY (100000, 1) 	NOT NULL,
	[PhoneID]		[int]						NOT NULL,
	[CustomerID]	[int]						NOT NULL,
	[EmployeeID]	[int] 						NOT NULL,
	[DateOfSale] 	[nvarchar] (50)				NOT NULL,
	[Total]			[float]						NOT NULL,
	[Active]		[bit]						NOT NULL,
	
	CONSTRAINT [fk_Sale_CustomerID] FOREIGN KEY ([CustomerID])
		REFERENCES [dbo].[Customer] ([CustomerID]),
	CONSTRAINT [fk_Sale_PhoneID] FOREIGN KEY ([PhoneID])
		REFERENCES [dbo].[Phone] ([PhoneID]),
	CONSTRAINT [fk_Sale_EmployeeID] FOREIGN KEY ([EmployeeID])
		REFERENCES [dbo].[Employee] ([EmployeeID]),
	CONSTRAINT [pk_Sale] PRIMARY KEY([SaleID])
)

print '' print ' **** inserting sale test records ****'
GO
INSERT INTO [dbo].[sale]
		([PhoneID], [CustomerID], [EmployeeID], [DateOfSale], [Total], [Active])
	VALUES
		(100000, 100000, 100000, '2023-15-04', 650.40, 1),
		(100000, 100000, 100002, '2023-12-08', 423.98, 0),			
		(100002, 100000, 100001, '2023-23-11', 192.00, 1),
		(100002, 100002, 100001, '2023-27-04', 873.03, 1),
		(100003, 100002, 100003, '2023-08-05', 230.51, 1),
		(100003, 100001, 100003, '2023-16-06', 456.65, 1),
		(100000, 100001, 100001, '2023-07-07', 723.49, 1)
GO

/* login related stored procedures */

print '' print '**** creating sp_authenticate_employee ****'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_employee]
(
	@Email				[nvarchar] (100),
	@PasswordHash		[nvarchar] (100)
)
AS
	BEGIN
		SELECT COUNT([EmployeeID]) as 'Authenticated'
		FROM	[Employee]
		WHERE	@Email = [Email]
			AND @PasswordHash = [PasswordHash]
			AND [Active] = 1
	END
GO
	
print '' print '**** creating sp_select_employee_using_email ****'
GO
CREATE PROCEDURE [dbo].[sp_select_employee_using_email]

(
	@Email				[nvarchar](100)
)	
AS
	BEGIN
		Select [EmployeeID], [GivenName], [FamilyName], [Phone],
				[Email], [Active]
		FROM [Employee]
		WHERE @Email = [Email]
	END
GO

print '' print '**** creating sp_select_employee_using_givenname ****'
GO
CREATE PROCEDURE [dbo].[sp_select_employee_using_givenname]

(
	@GivenName				[nvarchar](100)
)	
AS
	BEGIN
		Select [EmployeeID], [GivenName], [FamilyName], [Phone],
				[Email], [Active]
		FROM [Employee]
		WHERE @GivenName = [GivenName]
	END
GO

print '' print '**** creating sp_select_roles_using_employeeID ****'
GO
CREATE PROCEDURE [dbo].[sp_select_roles_using_employeeID]

(
	@EmployeeID				[int]
)	
AS
	BEGIN
		Select [RoleID]
		FROM [EmployeeRole]
		WHERE @EmployeeID = [EmployeeID]
	END
GO

print '' print '**** creating sp_update_passwordHash ****'
GO
CREATE PROCEDURE [dbo].[sp_update_passwordHash]

(
	@Email					[nvarchar](100),
	@NewPasswordHash		[nvarchar](100),
	@OldPasswordHash		[nvarchar](100)
	
)	
AS
	BEGIN
		UPDATE 	[Employee]
		SET		[PasswordHash] = @NewPasswordHash
		WHERE 	@Email = [Email]
			AND @OldPasswordHash = [PasswordHash]
		RETURN @@ROWCOUNT
	END
GO

print '' print '**** creating sp_select_all_phones_using_Status ****'
GO
CREATE PROCEDURE [dbo].[sp_select_all_phones_using_Status]	
(
	@Status		[nvarchar] (25)
)
AS
	BEGIN
		SELECT [PhoneID], [Make], [Model], [MakeYear], [Price]
		FROM   [Phone]
		WHERE 	@Status = [Status]
	END
GO

print '' print '**** creating sp_select_phone_using_model ****'
GO
CREATE PROCEDURE [dbo].[sp_select_phone_using_model]	
(
	@Model		[nvarchar] (25)
)
AS
	BEGIN
		SELECT [PhoneID], [Make], [Model], [MakeYear], [Price]
		FROM   [Phone]
		WHERE 	@Model = [Model]
	END
GO

print '' print '**** creating sp_select_all_customers****'
GO
CREATE PROCEDURE [dbo].[sp_select_all_customers]	
AS
	BEGIN
		SELECT [CustomerID], [GivenName], [FamilyName], [Email], [Phone]
		FROM   [Customer]		
	END
GO

print '' print '**** creating sp_select_customer_using_givenname****'
GO
CREATE PROCEDURE [dbo].[sp_select_customer_using_givenname]	
(
	@GivenName				nvarchar (100)
)
AS
	BEGIN
		SELECT [CustomerID], [GivenName], [FamilyName], [Email], [Phone]
		FROM   [Customer]		
		WHERE  [GivenName] = @GivenName	
	END
GO

print '' print '**** creating sp_select_all_sales****'
GO
CREATE PROCEDURE [dbo].[sp_select_all_sales]	
AS
	BEGIN
		SELECT [SaleID], [PhoneID], [CustomerID], [EmployeeID], [DateOfSale], [Total], [Active]
		FROM   [Sale]		
	END
GO

print '' print '**** creating sp_select_active_sales****'
GO
CREATE PROCEDURE [dbo].[sp_select_active_sales]	
AS
	BEGIN
		SELECT [SaleID], [PhoneID], [CustomerID], [EmployeeID], [DateOfSale], [Total], [Active]
		FROM   [Sale]
		WHERE  [Active] = 1;
	END
GO

print '' print '**** creating sp_select_sale_using_saleid****'
GO
CREATE PROCEDURE [dbo].[sp_select_sale_using_saleid]
(
	@saleID   		int
)
AS
	BEGIN
		SELECT [SaleID], [PhoneID], [CustomerID], [EmployeeID], [DateOfSale], [Total], [Active]
		FROM   [Sale]	
		WHERE @saleID = saleID
	END
GO

print '' print '**** creating sp_deactivate_Sale****'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_Sale]
(
	@saleID   		int
)
AS
	BEGIN
		Update  [Sale]
		SET [Active] = 0	
		WHERE @saleID = saleID
		RETURN @@ROWCOUNT
	END
GO

print '' print '**** creating sp_activate_Sale****'
GO
CREATE PROCEDURE [dbo].[sp_activate_Sale]
(
	@saleID   		int
)
AS
	BEGIN
		Update  [Sale]
		SET [Active] = 1	
		WHERE @saleID = saleID
		RETURN @@ROWCOUNT
	END
GO

print '' print '**** creating sp_add_Sale****'
GO
CREATE PROCEDURE [dbo].[sp_add_Sale]
(	
	@phoneID		int,
	@customerID 	int,
	@employeeID		int,
	@dateOfSale		nvarchar (50),
	@total			float,
	@acitve			bit
)
AS
	 BEGIN
            INSERT INTO Sale
                        (
                         [PhoneID],
                         [CustomerID],
                         [EmployeeID],
                         [DateOfSale],
						 [Total],
						 [Active])
            VALUES     (	
                         @phoneID,	
                         @customerID, 
                         @employeeID,	
                         @dateOfSale,	
						 @total,
						 @acitve)
		END

GO 

print '' print '**** creating sp_update_Sale****'
GO
CREATE PROCEDURE [dbo].[sp_update_Sale]
(	
	@saleID			int,
	@phoneID		int,
	@customerID 	int,
	@employeeID		int,
	@total			float,
	@active			bit 
)
AS
	BEGIN
            UPDATE Sale
            SET [PhoneID] = @phoneID,  [CustomerID] = @customerID, [EmployeeID] = @employeeID,
				[Total] = @total, [Active] = @active
			WHERE [SaleID] = @saleID
	END

GO                        

