-----------------------------------------------------------------------------------
-- Author: Eduardo Domenech
-- Description: Encripta un password en SHA2_256
-- Uso: dbo.EncriptarPassword('123456')
-----------------------------------------------------------------------------------
CREATE OR ALTER FUNCTION [dbo].[EncriptarPassword](@Password NVARCHAR(MAX))
RETURNS VARCHAR(max)
AS
BEGIN
    DECLARE @salt nvarchar(max) = 'c111de66-99a2-41c4-a2d2-e30bf299b3f4' -- Tiene que ser la misma que en el appsetting.json
    DECLARE @hash varbinary(300) = HASHBYTES('SHA2_256', @salt + @Password);
    DECLARE @passwordFinal varchar(max) = '0x' + CONVERT(varchar(max),@hash,2)
    RETURN @passwordFinal
END

GO

