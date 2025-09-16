-- Creacion de tabla:
CREATE TABLE Patiens(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	DocumentType NVARCHAR(50) NOT NULL,
	DocumentNumber NVARCHAR(20) NOT NULL, 
	Name NVARCHAR(100) NOT NULL,
	BirthDate DATE NOT NULL,
	Email NVARCHAR(150) NOT NULL,
	Gender NVARCHAR(20) NOT NULL,
	Address NVARCHAR(200) NOT NULL,
	PhoneNumber NVARCHAR(50) NOT NULL,
	IsActive BIT NOT NULL DEFAULT 1,
	CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
	UpdatedAt DATETIME NULL
);


-- SPs de Patiens:
CREATE PROCEDURE sp_GetByIdAsync
	@Id INT
AS 
BEGIN
	SELECT * FROM Patiens;
END
GO

-- sp_GetAllAsync
CREATE PROCEDURE sp_GetAllAsync 
AS 
BEGIN
	SELECT * FROM Patiens WHERE Id = @Id;
END
GO


-- sp_CreatePatient
CREATE PROCEDURE sp_CreatePatient 
	@DocumentType NVARCHAR(50),
    @DocumentNumber NVARCHAR(20),
    @Name NVARCHAR(100),
    @BirthDate DATETIME,
    @Email NVARCHAR(150),
    @Gender NVARCHAR(20),
    @Address NVARCHAR(200),
    @PhoneNumber NVARCHAR(50),
    @IsActive BIT
AS 
BEGIN
	INSERT INTO Patiens (DocumentType, DocumentNumber, Name, BirthDate, Email, Gender,  Address, PhoneNumber, IsActive)
	VALUES (@DocumentType, @DocumentNumber, @Name, @BirthDate, @Email, @Gender,  @Address, @PhoneNumber, @IsActive);
END
GO

-- sp_UpdatePatient
CREATE PROCEDURE sp_UpdatePatient 
	@Id INT,
	@DocumentType NVARCHAR(50),
    @DocumentNumber NVARCHAR(20),
    @Name NVARCHAR(100),
    @BirthDate DATETIME,
    @Email NVARCHAR(150),
    @Gender NVARCHAR(20),
    @Address NVARCHAR(200),
    @PhoneNumber NVARCHAR(50),
    @IsActive BIT
AS 
BEGIN
	UPDATE Patiens 
	SET DocumentType = @DocumentType, 
		DocumentNumber = @DocumentNumber, 
		Name = @Name, 
		BirthDate = @BirthDate, 
		Email = @Email, 
		Gender = @Gender,  
		Address = @Address, 
		PhoneNumber = @PhoneNumber, 
		IsActive = @IsActive
	WHERE Id = @Id;
END
GO

-- sp_DeletePatient [soft delete]
CREATE PROCEDURE sp_DeletePatient 
	@Id INT
AS 
BEGIN
	UPDATE Patiens SET IsActive = 0 WHERE Id = @Id;
END
GO