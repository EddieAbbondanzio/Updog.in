CREATE TABLE Vote (
    Id SERIAL NOT NULL PRIMARY KEY,
    UserId INT,
    ResourceId INT,
    ResourceType INT,
    Direction INT,
    FOREIGN KEY (UserId) REFERENCES "User"(Id)
);
