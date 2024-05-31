INSERT INTO trip.Country (Name)
VALUES ('Germany'),
       ('France'),
       ('Italy'),
       ('Spain'),
       ('Netherlands'),
       ('Portugal'),
       ('Austria'),
       ('Czech Republic');

-- Insert sample clients
INSERT INTO trip.Client (FirstName, LastName, Email, Telephone, Pesel)
VALUES ('John', 'Doe', 'john.doe@example.com', '1234567890', '12345678901'),
       ('Jane', 'Smith', 'jane.smith@example.com', '1234567891', '12345678902'),
       ('Alice', 'Johnson', 'alice.johnson@example.com', '1234567892', '12345678903'),
       ('Bob', 'Brown', 'bob.brown@example.com', '1234567893', '12345678904'),
       ('Charlie', 'Davis', 'charlie.davis@example.com', '1234567894', '12345678905'),
       ('Diana', 'Martinez', 'diana.martinez@example.com', '1234567895', '12345678906'),
       ('Edward', 'Garcia', 'edward.garcia@example.com', '1234567896', '12345678907'),
       ('Fiona', 'Miller', 'fiona.miller@example.com', '1234567897', '12345678908'),
       ('George', 'Wilson', 'george.wilson@example.com', '1234567898', '12345678909'),
       ('Hannah', 'Moore', 'hannah.moore@example.com', '1234567899', '12345678910');

-- Insert sample trips
INSERT INTO trip.Trip (Name, Description, DateFrom, DateTo, MaxPeople)
VALUES ('Berlin Tour', 'A wonderful tour of Berlin', '2024-06-01', '2024-06-07', 5),
       ('Paris Adventure', 'Explore the beauty of Paris', '2024-07-01', '2024-07-07', 5),
       ('Rome Journey', 'Discover the ancient history of Rome', '2024-08-01', '2024-08-07', 5),
       ('Madrid Fiesta', 'Experience the vibrant culture of Madrid', '2024-09-01', '2024-09-07', 5),
       ('Amsterdam Highlights', 'Enjoy the canals and museums of Amsterdam', '2024-10-01', '2024-10-07', 5),
       ('Venice Dreams', 'A romantic trip to Venice', '2024-06-15', '2024-06-21', 5),
       ('Barcelona Excursion', 'A fantastic journey through Barcelona', '2024-07-15', '2024-07-21', 5),
       ('Vienna Visit', 'Explore the classical beauty of Vienna', '2024-08-15', '2024-08-21', 5),
       ('Prague Exploration', 'Discover the charm of Prague', '2024-09-15', '2024-09-21', 5),
       ('Lisbon Getaway', 'A delightful trip to Lisbon', '2024-10-15', '2024-10-21', 5);

-- Insert trip-country associations
DECLARE @GermanyId INT = (SELECT IdCountry
                          FROM trip.Country
                          WHERE Name = 'Germany');
DECLARE @FranceId INT = (SELECT IdCountry
                         FROM trip.Country
                         WHERE Name = 'France');
DECLARE @ItalyId INT = (SELECT IdCountry
                        FROM trip.Country
                        WHERE Name = 'Italy');
DECLARE @SpainId INT = (SELECT IdCountry
                        FROM trip.Country
                        WHERE Name = 'Spain');
DECLARE @NetherlandsId INT = (SELECT IdCountry
                              FROM trip.Country
                              WHERE Name = 'Netherlands');
DECLARE @PortugalId INT = (SELECT IdCountry
                           FROM trip.Country
                           WHERE Name = 'Portugal');
DECLARE @AustriaId INT = (SELECT IdCountry
                          FROM trip.Country
                          WHERE Name = 'Austria');
DECLARE @CzechRepublicId INT = (SELECT IdCountry
                                FROM trip.Country
                                WHERE Name = 'Czech Republic');

DECLARE @BerlinTourId INT = (SELECT IdTrip
                             FROM trip.Trip
                             WHERE Name = 'Berlin Tour');
DECLARE @ParisAdventureId INT = (SELECT IdTrip
                                 FROM trip.Trip
                                 WHERE Name = 'Paris Adventure');
DECLARE @RomeJourneyId INT = (SELECT IdTrip
                              FROM trip.Trip
                              WHERE Name = 'Rome Journey');
DECLARE @MadridFiestaId INT = (SELECT IdTrip
                               FROM trip.Trip
                               WHERE Name = 'Madrid Fiesta');
DECLARE @AmsterdamHighlightsId INT = (SELECT IdTrip
                                      FROM trip.Trip
                                      WHERE Name = 'Amsterdam Highlights');
DECLARE @VeniceDreamsId INT = (SELECT IdTrip
                               FROM trip.Trip
                               WHERE Name = 'Venice Dreams');
DECLARE @BarcelonaExcursionId INT = (SELECT IdTrip
                                     FROM trip.Trip
                                     WHERE Name = 'Barcelona Excursion');
DECLARE @ViennaVisitId INT = (SELECT IdTrip
                              FROM trip.Trip
                              WHERE Name = 'Vienna Visit');
DECLARE @PragueExplorationId INT = (SELECT IdTrip
                                    FROM trip.Trip
                                    WHERE Name = 'Prague Exploration');
DECLARE @LisbonGetawayId INT = (SELECT IdTrip
                                FROM trip.Trip
                                WHERE Name = 'Lisbon Getaway');

INSERT INTO trip.Country_Trip (IdCountry, IdTrip)
VALUES (@GermanyId, @BerlinTourId),              -- Berlin, Germany
       (@FranceId, @ParisAdventureId),           -- Paris, France
       (@ItalyId, @RomeJourneyId),               -- Rome, Italy
       (@SpainId, @MadridFiestaId),              -- Madrid, Spain
       (@NetherlandsId, @AmsterdamHighlightsId), -- Amsterdam, Netherlands
       (@ItalyId, @VeniceDreamsId),              -- Venice, Italy
       (@SpainId, @BarcelonaExcursionId),        -- Barcelona, Spain
       (@AustriaId, @ViennaVisitId),             -- Vienna, Austria
       (@CzechRepublicId, @PragueExplorationId), -- Prague, Czech Republic
       (@PortugalId, @LisbonGetawayId);          -- Lisbon, Portugal

-- Insert client-trip bookings
DECLARE @ClientId1 INT = (SELECT IdClient
                          FROM trip.Client
                          WHERE Email = 'john.doe@example.com');
DECLARE @ClientId2 INT = (SELECT IdClient
                          FROM trip.Client
                          WHERE Email = 'jane.smith@example.com');
DECLARE @ClientId3 INT = (SELECT IdClient
                          FROM trip.Client
                          WHERE Email = 'alice.johnson@example.com');
DECLARE @ClientId4 INT = (SELECT IdClient
                          FROM trip.Client
                          WHERE Email = 'bob.brown@example.com');
DECLARE @ClientId6 INT = (SELECT IdClient
                          FROM trip.Client
                          WHERE Email = 'diana.martinez@example.com');
DECLARE @ClientId7 INT = (SELECT IdClient
                          FROM trip.Client
                          WHERE Email = 'edward.garcia@example.com');
DECLARE @ClientId8 INT = (SELECT IdClient
                          FROM trip.Client
                          WHERE Email = 'fiona.miller@example.com');
DECLARE @ClientId9 INT = (SELECT IdClient
                          FROM trip.Client
                          WHERE Email = 'george.wilson@example.com');
DECLARE @ClientId10 INT = (SELECT IdClient
                           FROM trip.Client
                           WHERE Email = 'hannah.moore@example.com');

INSERT INTO trip.Client_Trip (IdClient, IdTrip, RegisteredAt, PaymentDate)
VALUES (@ClientId1, @BerlinTourId, GETDATE(), NULL),
       (@ClientId2, @BerlinTourId, GETDATE(), NULL),
       (@ClientId6, @ParisAdventureId, GETDATE(), NULL),
       (@ClientId7, @ParisAdventureId, GETDATE(), NULL),
       (@ClientId8, @ParisAdventureId, GETDATE(), NULL),
       (@ClientId1, @RomeJourneyId, GETDATE(), NULL),
       (@ClientId2, @RomeJourneyId, GETDATE(), NULL),
       (@ClientId3, @RomeJourneyId, GETDATE(), NULL),
       (@ClientId4, @RomeJourneyId, GETDATE(), NULL),
       (@ClientId6, @MadridFiestaId, GETDATE(), NULL),
       (@ClientId6, @VeniceDreamsId, GETDATE(), NULL),
       (@ClientId7, @VeniceDreamsId, GETDATE(), NULL),
       (@ClientId8, @VeniceDreamsId, GETDATE(), NULL),
       (@ClientId9, @VeniceDreamsId, GETDATE(), NULL),
       (@ClientId10, @VeniceDreamsId, GETDATE(), NULL), -- Fully book trip 6
       (@ClientId1, @BarcelonaExcursionId, GETDATE(), NULL),
       (@ClientId2, @BarcelonaExcursionId, GETDATE(), NULL),
       (@ClientId3, @BarcelonaExcursionId, GETDATE(), NULL),
       (@ClientId6, @ViennaVisitId, GETDATE(), NULL),
       (@ClientId7, @ViennaVisitId, GETDATE(), NULL),
       (@ClientId8, @ViennaVisitId, GETDATE(), NULL),
       (@ClientId9, @ViennaVisitId, GETDATE(), NULL),
       (@ClientId10, @ViennaVisitId, GETDATE(), NULL),  -- Fully book trip 8
       (@ClientId1, @PragueExplorationId, GETDATE(), NULL),
       (@ClientId2, @PragueExplorationId, GETDATE(), NULL),
       (@ClientId3, @PragueExplorationId, GETDATE(), NULL),
       (@ClientId6, @LisbonGetawayId, GETDATE(), NULL),
       (@ClientId7, @LisbonGetawayId, GETDATE(), NULL),
       (@ClientId8, @LisbonGetawayId, GETDATE(), NULL),
       (@ClientId9, @LisbonGetawayId, GETDATE(), NULL),
       (@ClientId10, @LisbonGetawayId, GETDATE(), NULL); -- Fully book trip 10