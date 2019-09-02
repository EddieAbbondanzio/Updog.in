CREATE TABLE Comment (
    Id SERIAL NOT NULL PRIMARY KEY,
    UserId INT,
    PostId INT,
    ParentId INT,
    Body VARCHAR(10000) NOT NULL,
    CreationDate TIMESTAMP NOT NULL,
    WasUpdated BOOLEAN,
    WasDeleted BOOLEAN,
    FOREIGN KEY (UserId) REFERENCES "User"(Id),
    FOREIGN KEY (PostId) REFERENCES Post(Id)
);
