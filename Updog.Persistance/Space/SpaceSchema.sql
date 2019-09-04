CREATE TABLE Space (
    Id SERIAL NOT NULL PRIMARY KEY,
    Name VARCHAR(24) NOT NULL UNIQUE,
    CreationDate TIMESTAMP,
    UserId INT,
    SubscriptionCount INT,
    IsDefault BOOLEAN,
    Description VARCHAR(512),
    FOREIGN KEY (UserId) REFERENCES "User"(Id)
);
