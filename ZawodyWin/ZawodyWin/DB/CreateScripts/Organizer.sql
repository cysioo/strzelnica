CREATE TABLE [Organizer] (
  [Id] INTEGER NOT NULL
, [Name] TEXT NOT NULL
, [License] TEXT NULL
, [AddressLine1] TEXT NULL
, [AddressLine2] TEXT NULL
, [LogoPath] TEXT NULL
, CONSTRAINT [PK_Organizer] PRIMARY KEY ([Id])
);

GO;

INSERT INTO [Organizer]
           (
          [Name]
           ,[License]
           ,[AddressLine1]
           ,[AddressLine2]
           ,[LogoPath])
     VALUES
           (
           'Klub Strzelecko Kolekcjonerski Tarcza Gdynia'
           ,'PZSS-LK-1047/2017'
           ,'81-350 Gdynia'
           ,'Ul. Portowa 3'
           ,'');