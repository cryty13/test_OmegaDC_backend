CREATE PROCEDURE Login_Validate
    @Email VARCHAR(40),
    @Password VARCHAR(Max)
AS
       SELECT * FROM [Sec.User] 
			Where Email = @Email and Password = @Password 
