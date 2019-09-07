CREATE TABLE Post (
    Id SERIAL NOT NULL PRIMARY KEY,
    UserId INT,
    SpaceId INT,
    Type SMALLINT NOT NULL,
    Title VARCHAR(300) NOT NULL,
    Body VARCHAR(10000) NOT NULL,
    CreationDate TIMESTAMP NOT NULL,
    WasUpdated BOOLEAN,
    WasDeleted BOOLEAN,
    Upvotes INT,
    Downvotes INT,
    CommentCount INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES "User"(Id)
    FOREIGN KEY (SpaceId) REFERENCES Space(Id)
);
