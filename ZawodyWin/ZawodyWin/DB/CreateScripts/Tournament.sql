-- Script Date: 17.03.2023 11:42  - ErikEJ.SqlCeScripting version 3.5.2.94
CREATE TABLE [Tournament] (
  [Id] INTEGER NOT NULL
, [Name] TEXT NOT NULL
, [Date] TEXT NULL
, [OrganizerId] INTEGER NULL
, [Place] TEXT NULL
, [LeadingRefereeId] INTEGER NULL
, CONSTRAINT [PK_Tournament] PRIMARY KEY ([Id])
);
