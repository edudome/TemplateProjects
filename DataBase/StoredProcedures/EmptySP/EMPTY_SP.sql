-----------------------------------------------------
-- Author: 
-- Description: 
--
-- Alter: 
-- Comments: 
-- 
-- Alter: 
-- Comments: 
-----------------------------------------------------
CREATE OR ALTER PROCEDURE [dbo].[NOMBRE_SP]
	--@AAAs AAAUDT readonly,
    @EmpresaId int
AS
BEGIN
	BEGIN TRY
        BEGIN TRAN
            
        DECLARE @Return INT = 1
    
        --------





        --;THROW 51000, 'Error intencional.', 16
        --;THROW 51000, '3-Error intencional con errorCode.', 16
        --------

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

