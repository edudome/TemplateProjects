--------------------------------------------------------------
-- Author: Eduardo Domenech
-- Description: Forma una tabla desde el Split de un string
-- Uso: select * from dbo.SplitString('1,2,3,4,5,6', ',')
--------------------------------------------------------------
CREATE OR ALTER FUNCTION [dbo].[SplitString] (@String nvarchar (max), @Delimitador nvarchar (10)) 
                returns @ValueTable table ([Value] nvarchar(4000))
begin
 declare @NextString nvarchar(4000)
 declare @Pos int
 declare @NextPos int
 declare @CommaCheck nvarchar(1)
  
 --Inicializa
 set @NextString = ''
 set @CommaCheck = right(@String,1) 
  
 set @String = @String + @Delimitador
  
 --Busca la posici�n del primer delimitador
 set @Pos = charindex(@Delimitador,@String)
 set @NextPos = 1
  
 --Itera mientras exista un delimitador en el string
 while (@pos <>  0)  
 begin
  set @NextString = substring(@String,1,@Pos - 1)
  
  insert into @ValueTable ( [Value]) Values (@NextString)
  
  set @String = substring(@String,@pos +1,len(@String))
   
  set @NextPos = @Pos
  set @pos  = charindex(@Delimitador,@String)
 end
  
 return
end


GO

