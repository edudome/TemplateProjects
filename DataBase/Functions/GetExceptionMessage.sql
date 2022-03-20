-----------------------------------------------------------------------------------
-- Author: Eduardo Domenech
-- Description: Tira una excepción
-- Uso: Dentro del Catch de un SP usar: dbo.GetExceptionMessage('77-Error X', 64)
-----------------------------------------------------------------------------------
CREATE OR ALTER FUNCTION [dbo].[GetExceptionMessage](@errorMsg NVARCHAR(MAX), @errorLine INT)
RETURNS VARCHAR(max)
AS
BEGIN
    DECLARE @errorCode nvarchar(max) = ''
    DECLARE @msg nvarchar(max) = ''
    DECLARE @t_codigoMsg table (indice int, msg varchar(max))
    INSERT INTO @t_codigoMsg select row_number() over(order by (select null)), value FROM STRING_SPLIT(@errorMsg, '-')
    DECLARE @count int = ISNULL((select COUNT(*) from @t_codigoMsg), 0)
    IF @count = 2
    BEGIN
        SET @errorCode = ISNULL((select REPLACE(msg, ' ', '') from @t_codigoMsg where indice = 1), '0')
        SET @msg = ISNULL((select msg from @t_codigoMsg where indice = 2), 'Error no controlado')
    END
    ELSE
    BEGIN
        IF @count > 0
        BEGIN
            SET @errorCode = '0'
            SET @msg = ISNULL((select msg from @t_codigoMsg where indice = 1), 'Error no controlado')
        END
    END
	DECLARE @msgResult nvarchar(max) = '###sp_error###' + @errorCode + '###sp_error###' + Convert(nvarchar, @errorLine) + '###sp_error###' + @msg
    RETURN @msgResult
END

GO

