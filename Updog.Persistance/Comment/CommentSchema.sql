CREATE TABLE Comment (
    Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    UserId INT,
    PostId INT,
    ParentId INT,
    Body VARCHAR(10000) NOT NULL,
    CreationDate DateTime NOT NULL,
    WasUpdated BOOLEAN,
    WasDeleted BOOLEAN,
    FOREIGN KEY (UserId) REFERENCES User(Id),
    FOREIGN KEY (PostId) REFERENCES Post(Id)
);
