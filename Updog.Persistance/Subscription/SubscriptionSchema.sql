CREATE TABLE Subscription (
    Id SERIAL NOT NULL PRIMARY KEY,
    SpaceId INT,
    UserId INT,
    FOREIGN KEY (SpaceId) REFERENCES Space(Id),
    FOREIGN KEY (UserId) REFERENCES "User"(Id)
);
