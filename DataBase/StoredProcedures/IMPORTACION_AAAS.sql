-----------------------------------------------------
-- Author: Eduardo Domenech
-- Description: Importa los aaas
-----------------------------------------------------
CREATE OR ALTER PROCEDURE [dbo].[IMPORTACION_AAAS]
	@AAAs AAAUDT readonly,
    @borrarPrevios bit = 0
AS
BEGIN

	BEGIN TRY
        BEGIN TRAN
        
        DECLARE @Return INT = 0
    

        --SET @Return = 1 / 0 --- ERROR!!

        IF @borrarPrevios = 1
        BEGIN
            --DELETE FROM AAA
            --;THROW 51000, 'Borrar Previo es True.', 16
            ;THROW 51000, '77-Borrar Previo es True.', 16
        END

	    INSERT INTO AAA(Nombre, Apellido, Cedula, Habilitado)
	    SELECT [Nombre], [Apellido], [Cedula], 1
	    FROM @AAAs

        COMMIT TRAN
        SET @Return = 1
        SELECT @Return

    END TRY
    BEGIN CATCH

		ROLLBACK TRAN
			
        declare @msg nvarchar(max) = dbo.GetExceptionMessage(ERROR_MESSAGE(), ERROR_LINE())
		;THROW 51000, @msg, 16

	END CATCH
END

GO

