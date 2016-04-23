
CREATE PROCEDURE getdetail 
(
@id int,@title nvarchar(50) output
)
AS
BEGIN
	SET NOCOUNT ON;
	declare @newscount int 
	select @title=title,@newscount= COUNT(id)
	from mynews
	where(id=@id)
	group by title
	return @newscount
END
GO
