CREATE TABLE Books (
  Id int IDENTITY(1,1) PRIMARY KEY,
  Author varchar(2000),
  LaunchDate datetime NOT NULL,
  Price decimal(18,2) NOT NULL,
  Title varchar(2000),
)



