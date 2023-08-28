-----RESET RANK-----
use MyInventoryDb;
WITH RankedCustomers AS (
    SELECT Id, ROW_NUMBER() OVER (ORDER BY Id) AS NewRank
    FROM Customer
)
UPDATE c
SET Rank = rc.NewRank
FROM Customer c
JOIN RankedCustomers rc ON c.Id = rc.Id;
-----RESET RANK-----

use MyInventoryDb;

--------INCREASING RANK ------------------

--DECLARE @NewRank INT = 2; -- New rank for the customer
--DECLARE @OldRank INT; -- Old rank of the customer
--SELECT @OldRank = Rank FROM Customer WHERE FirstName = 'Noel' AND LastName = 'Clark';

---- Adjust ranks for other customers
--UPDATE Customer
--SET Rank = Rank + 1
--WHERE Rank >= @NewRank AND Rank < @OldRank;

---- Update the rank of the target customer
--UPDATE Customer
--SET Rank = @NewRank
--WHERE FirstName = 'Noel' AND LastName = 'Clark';

--------INCREASING RANK ------------------
--------DECREASING RANK-----------------
use MyInventoryDb;
DECLARE @NewRank INT = 10; -- New rank for the customer
DECLARE @OldRank INT; -- Old rank of the customer
SELECT @OldRank = Rank FROM Customer WHERE FirstName = 'Michael' AND LastName = 'Johnson';

-- Adjust ranks for other customers
UPDATE Customer
SET Rank = Rank - 1
WHERE Rank <= @NewRank AND Rank > @OldRank;

-- Update the rank of the target customer
UPDATE Customer
SET Rank = @NewRank
WHERE FirstName = 'Michael' AND LastName = 'Johnson';

--------DECREASING RANK-----------------