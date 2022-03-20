
DROP PROCEDURE [dbo].[IMPORTACION_AAAS]
DROP TYPE [dbo].[AAAUDT]
GO

---------------------------------------------
-- Author: Eduardo Domenech
-- Description: Tabla con datos de aaas
---------------------------------------------
CREATE TYPE [dbo].[AAAUDT] AS TABLE
(
	Nombre NVARCHAR(50),
	Apellido NVARCHAR(50),
	Cedula NVARCHAR(50) null
)


GO

