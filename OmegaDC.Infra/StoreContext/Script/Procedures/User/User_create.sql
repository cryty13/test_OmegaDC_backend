CREATE PROCEDURE User_Create
    @Name VARCHAR(40),
    @Password VARCHAR(Max),
    @Email VARCHAR(60)
AS

    INSERT INTO [Sec.User] (
        [Name], 
        [Password], 
        [Email], 
        [DtCriacao], 
        [Ativo]
    ) VALUES (
        @Name,
		@Password,
        @Email,
        getdate(),
		1
    )