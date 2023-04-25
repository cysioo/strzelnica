CREATE TABLE [Competition] (
  [Id] INTEGER NOT NULL
, [Name] TEXT NOT NULL
, [NumberOfRounds] INTEGER NOT NULL
, [TournamentId] INTEGER NOT NULL
, CONSTRAINT [PK_Competition] PRIMARY KEY ([Id])
);

GO;
