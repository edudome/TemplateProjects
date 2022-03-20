
-------------------------------------------------------
-- Author: Eduardo Domenech
-- Description: Obtiene el aaa
-------------------------------------------------------
CREATE OR ALTER PROCEDURE [dbo].[GET_AAA_BY_ID]
	@AAAId int
AS
BEGIN
	select * from AAA where AAAId = @AAAId
END

GO
