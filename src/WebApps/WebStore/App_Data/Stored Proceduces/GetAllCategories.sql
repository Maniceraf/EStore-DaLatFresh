CREATE OR ALTER PROCEDURE GetAllCategories
AS
BEGIN
	SELECT c.Id, c.Name, COUNT(*) AS [Count] FROM Category AS c
	JOIN Product AS p ON p.CategoryId = c.Id
	GROUP BY c.Id, c.Name
END

exec GetAllCategories