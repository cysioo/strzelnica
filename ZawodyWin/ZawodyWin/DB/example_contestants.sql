PRAGMA temp_store = 2; /* 2 means use in-memory */
    CREATE TEMP TABLE _Variables(Name TEXT PRIMARY KEY, IntegerValue INTEGER);

    /* Declaring a variable */
    INSERT INTO _Variables (Name) VALUES ('pid'), ('cid'), ('score');
    
    
INSERT INTO [Person]
           ([Name]
           ,[Surname]
           ,[ClubName])
     VALUES ('Name ' || random(), 'Surname ' || random(), 'klub');
UPDATE _Variables set IntegerValue = last_insert_rowid() where Name = 'pid';
           
           
INSERT INTO [Contestant]
           ([ClubName]
           ,[CompetitionId]
           ,[Notes]
           ,[PersonId])
     VALUES
           ('klub',1,NULL ,
           (SELECT IntegerValue FROM _Variables WHERE Name = 'pid' LIMIT 1) );
UPDATE _Variables set IntegerValue = last_insert_rowid() where Name = 'cid';

INSERT INTO [Score]  ([CompetitionId] ,[ContestantId] ,[Points] ,[Round])
     VALUES (1, 
     	(SELECT IntegerValue FROM _Variables WHERE Name = 'cid' LIMIT 1),
     	abs(random()) % 100, 
     	1);
     	
INSERT INTO [Score]  ([CompetitionId] ,[ContestantId] ,[Points] ,[Round])
     VALUES (2, 
     	(SELECT IntegerValue FROM _Variables WHERE Name = 'cid' LIMIT 1),
     	abs(random()) % 100, 
     	1);
     	
INSERT INTO [Score]  ([CompetitionId] ,[ContestantId] ,[Points] ,[Round])
     VALUES (2, 
     	(SELECT IntegerValue FROM _Variables WHERE Name = 'cid' LIMIT 1),
     	abs(random()) % 100, 
     	2);
     
     
DROP TABLE _Variables;

