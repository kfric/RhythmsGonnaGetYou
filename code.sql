CREATE TABLE "Bands" (
  "Id"					SERIAL PRIMARY KEY,
  "Name"                TEXT NOT NULL,
  "CountryOfOrigin"     TEXT,
  "NumberOfMemebers"    INT,
  "Website"             TEXT,
  "Style"               TEXT,
  "IsSigned"			TEXT,
  "ContactName"			TEXT,
  "ContactPhoneNumber"	INT
);

CREATE TABLE "Songs" (
  "Id"			SERIAL PRIMARY KEY,
  "TrackNumber" INT,
  "Title"     	TEXT NOT NULL,
  "Duration"    INT
);

CREATE TABLE "Albums" (
  "Id"			SERIAL PRIMARY KEY,
  "Title"		TEXT NOT NULL,
  "IsExplicit"	TEXT,
  "ReleaseDate" TEXT,
);


-- add Ids to tables
ALTER TABLE "Albums" ADD COLUMN "BandId" INTEGER NULL REFERENCES "Bands" ("Id");

ALTER TABLE "Songs" ADD COLUMN "AlbumId" INTEGER NULL REFERENCES "Albums" ("Id");



-- add values to bands table
INSERT INTO "Bands" ("Name", "CountryOfOrigin", "NumberOfMemebers", "Website", "Style", "IsSigned", "ContactName", "ContactPhoneNumber")
VALUES ('Linkin Park', 'USA', 9, 'linkinpark.com', 'Rock', 'True', 'Ben Jerry', 1234);

INSERT INTO "Bands" ("Name", "CountryOfOrigin", "NumberOfMemebers", "Website", "Style", "IsSigned", "ContactName", "ContactPhoneNumber")
VALUES ('Brockhampton', 'USA', 16, 'brckhmptn.com', 'Hip-Hop', 'True', 'Kevin Abstract', 4567);

INSERT INTO "Bands" ("Name", "CountryOfOrigin", "NumberOfMemebers", "Website", "Style", "IsSigned", "ContactName", "ContactPhoneNumber")
VALUES ('San Holo', 'Netherlands', 1, 'sanholo.com', 'Electronic', 'True', 'Ben Solo', 7891);

-- add values to albums table and assoc to band with Id
INSERT INTO "Albums" ("Title", "IsExplicit", "ReleaseDate", "BandId")
VALUES ('Meteora', 'True', '03-25-2003', 1);

INSERT INTO "Albums" ("Title", "IsExplicit", "ReleaseDate", "BandId")
VALUES ('Roadrunner: New Light, New Machine', 'True', '04-09-2021', 2);

INSERT INTO "Albums" ("Title", "IsExplicit", "ReleaseDate", "BandId")
VALUES ('album1', 'False', '09-21-2018', 3);


-- add values to songs and assoc to album with Id
INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES (9, 'Breaking the Habit', 256, 1);

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES (7, 'Ill Take You On', 255, 2);

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES (1, 'Everything Matters (when it comes to you)', 337, 3);