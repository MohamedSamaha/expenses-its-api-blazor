CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;
CREATE TABLE "Expenses" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Expenses" PRIMARY KEY AUTOINCREMENT,
    "Title" TEXT NOT NULL,
    "Amount" TEXT NOT NULL,
    "Currency" TEXT NOT NULL,
    "Category" TEXT NOT NULL,
    "OccurredOn" TEXT NOT NULL,
    "CreatedByUserId" TEXT NOT NULL,
    "RowVersion" BLOB NULL
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260223091959_InitialCreate', '10.0.3');

COMMIT;

