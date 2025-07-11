﻿CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;
CREATE TABLE "ApplicationUser" (
    "Id" text NOT NULL,
    "UserName" text,
    "NormalizedUserName" text,
    "Email" text,
    "NormalizedEmail" text,
    "EmailConfirmed" boolean NOT NULL,
    "PasswordHash" text,
    "SecurityStamp" text,
    "ConcurrencyStamp" text,
    "PhoneNumber" text,
    "PhoneNumberConfirmed" boolean NOT NULL,
    "TwoFactorEnabled" boolean NOT NULL,
    "LockoutEnd" timestamp with time zone,
    "LockoutEnabled" boolean NOT NULL,
    "AccessFailedCount" integer NOT NULL,
    CONSTRAINT "PK_ApplicationUser" PRIMARY KEY ("Id")
);

CREATE TABLE "ProductTypes" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Name" text NOT NULL,
    CONSTRAINT "PK_ProductTypes" PRIMARY KEY ("Id")
);

CREATE TABLE "Carts" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "UserId" text NOT NULL,
    CONSTRAINT "PK_Carts" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Carts_ApplicationUser_UserId" FOREIGN KEY ("UserId") REFERENCES "ApplicationUser" ("Id") ON DELETE CASCADE
);

CREATE TABLE "CheckoutDetails" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Email" text NOT NULL,
    "PhoneNumber" text NOT NULL,
    "AddressLine1" text NOT NULL,
    "AddressLine2" text NOT NULL,
    "City" text NOT NULL,
    "State" text NOT NULL,
    "PostalCode" text NOT NULL,
    "Country" text NOT NULL,
    "UserId" text NOT NULL,
    CONSTRAINT "PK_CheckoutDetails" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_CheckoutDetails_ApplicationUser_UserId" FOREIGN KEY ("UserId") REFERENCES "ApplicationUser" ("Id") ON DELETE CASCADE
);

CREATE TABLE "ProductAttributes" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Name" text NOT NULL,
    "DataType" text NOT NULL,
    "ProductTypeId" integer NOT NULL,
    CONSTRAINT "PK_ProductAttributes" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_ProductAttributes_ProductTypes_ProductTypeId" FOREIGN KEY ("ProductTypeId") REFERENCES "ProductTypes" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Products" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Price" numeric NOT NULL,
    "Stock" integer NOT NULL,
    "ProductTypeId" integer NOT NULL,
    CONSTRAINT "PK_Products" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Products_ProductTypes_ProductTypeId" FOREIGN KEY ("ProductTypeId") REFERENCES "ProductTypes" ("Id") ON DELETE CASCADE
);

CREATE TABLE "CartItems" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "CartId" integer NOT NULL,
    "ProductId" integer NOT NULL,
    "Quantity" integer NOT NULL,
    CONSTRAINT "PK_CartItems" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_CartItems_Carts_CartId" FOREIGN KEY ("CartId") REFERENCES "Carts" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_CartItems_Products_ProductId" FOREIGN KEY ("ProductId") REFERENCES "Products" ("Id") ON DELETE CASCADE
);

CREATE TABLE "ProductAttributeValues" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Value" text NOT NULL,
    "ProductId" integer NOT NULL,
    "ProductAttributeId" integer NOT NULL,
    CONSTRAINT "PK_ProductAttributeValues" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_ProductAttributeValues_ProductAttributes_ProductAttributeId" FOREIGN KEY ("ProductAttributeId") REFERENCES "ProductAttributes" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_ProductAttributeValues_Products_ProductId" FOREIGN KEY ("ProductId") REFERENCES "Products" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_CartItems_CartId" ON "CartItems" ("CartId");

CREATE INDEX "IX_CartItems_ProductId" ON "CartItems" ("ProductId");

CREATE UNIQUE INDEX "IX_Carts_UserId" ON "Carts" ("UserId");

CREATE INDEX "IX_CheckoutDetails_UserId" ON "CheckoutDetails" ("UserId");

CREATE INDEX "IX_ProductAttributes_ProductTypeId" ON "ProductAttributes" ("ProductTypeId");

CREATE INDEX "IX_ProductAttributeValues_ProductAttributeId" ON "ProductAttributeValues" ("ProductAttributeId");

CREATE INDEX "IX_ProductAttributeValues_ProductId" ON "ProductAttributeValues" ("ProductId");

CREATE INDEX "IX_Products_ProductTypeId" ON "Products" ("ProductTypeId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250620073447_InitialCreate', '9.0.6');

ALTER TABLE "Carts" DROP CONSTRAINT "FK_Carts_ApplicationUser_UserId";

ALTER TABLE "CheckoutDetails" DROP CONSTRAINT "FK_CheckoutDetails_ApplicationUser_UserId";

ALTER TABLE "ApplicationUser" DROP CONSTRAINT "PK_ApplicationUser";

ALTER TABLE "ApplicationUser" RENAME TO "AspNetUsers";

ALTER TABLE "AspNetUsers" ALTER COLUMN "UserName" TYPE character varying(256);

ALTER TABLE "AspNetUsers" ALTER COLUMN "NormalizedUserName" TYPE character varying(256);

ALTER TABLE "AspNetUsers" ALTER COLUMN "NormalizedEmail" TYPE character varying(256);

ALTER TABLE "AspNetUsers" ALTER COLUMN "Email" TYPE character varying(256);

ALTER TABLE "AspNetUsers" ADD CONSTRAINT "PK_AspNetUsers" PRIMARY KEY ("Id");

CREATE TABLE "AspNetRoles" (
    "Id" text NOT NULL,
    "Name" character varying(256),
    "NormalizedName" character varying(256),
    "ConcurrencyStamp" text,
    CONSTRAINT "PK_AspNetRoles" PRIMARY KEY ("Id")
);

CREATE TABLE "AspNetUserClaims" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "UserId" text NOT NULL,
    "ClaimType" text,
    "ClaimValue" text,
    CONSTRAINT "PK_AspNetUserClaims" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_AspNetUserClaims_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "AspNetUserLogins" (
    "LoginProvider" character varying(128) NOT NULL,
    "ProviderKey" character varying(128) NOT NULL,
    "ProviderDisplayName" text,
    "UserId" text NOT NULL,
    CONSTRAINT "PK_AspNetUserLogins" PRIMARY KEY ("LoginProvider", "ProviderKey"),
    CONSTRAINT "FK_AspNetUserLogins_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "AspNetUserTokens" (
    "UserId" text NOT NULL,
    "LoginProvider" character varying(128) NOT NULL,
    "Name" character varying(128) NOT NULL,
    "Value" text,
    CONSTRAINT "PK_AspNetUserTokens" PRIMARY KEY ("UserId", "LoginProvider", "Name"),
    CONSTRAINT "FK_AspNetUserTokens_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "AspNetRoleClaims" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "RoleId" text NOT NULL,
    "ClaimType" text,
    "ClaimValue" text,
    CONSTRAINT "PK_AspNetRoleClaims" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_AspNetRoleClaims_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE
);

CREATE TABLE "AspNetUserRoles" (
    "UserId" text NOT NULL,
    "RoleId" text NOT NULL,
    CONSTRAINT "PK_AspNetUserRoles" PRIMARY KEY ("UserId", "RoleId"),
    CONSTRAINT "FK_AspNetUserRoles_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_AspNetUserRoles_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE INDEX "EmailIndex" ON "AspNetUsers" ("NormalizedEmail");

CREATE UNIQUE INDEX "UserNameIndex" ON "AspNetUsers" ("NormalizedUserName");

CREATE INDEX "IX_AspNetRoleClaims_RoleId" ON "AspNetRoleClaims" ("RoleId");

CREATE UNIQUE INDEX "RoleNameIndex" ON "AspNetRoles" ("NormalizedName");

CREATE INDEX "IX_AspNetUserClaims_UserId" ON "AspNetUserClaims" ("UserId");

CREATE INDEX "IX_AspNetUserLogins_UserId" ON "AspNetUserLogins" ("UserId");

CREATE INDEX "IX_AspNetUserRoles_RoleId" ON "AspNetUserRoles" ("RoleId");

ALTER TABLE "Carts" ADD CONSTRAINT "FK_Carts_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE;

ALTER TABLE "CheckoutDetails" ADD CONSTRAINT "FK_CheckoutDetails_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250620164701_AddIdentityTables', '9.0.6');

ALTER TABLE "AspNetUsers" ADD "UserType" text NOT NULL DEFAULT '';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250620172930_AddUserTypeToApplicationUser', '9.0.6');

ALTER TABLE "CartItems" DROP CONSTRAINT "FK_CartItems_Carts_CartId";

DROP TABLE "Carts";

DROP INDEX "IX_CartItems_CartId";

ALTER TABLE "CartItems" DROP COLUMN "CartId";

ALTER TABLE "CartItems" ADD "UserId" text NOT NULL DEFAULT '';

CREATE INDEX "IX_CartItems_UserId" ON "CartItems" ("UserId");

ALTER TABLE "CartItems" ADD CONSTRAINT "FK_CartItems_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250621123833_integratesCartEntityIntoUserAsField', '9.0.6');

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250621183718_productRequireType', '9.0.6');

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250621184902_productModelCleanup', '9.0.6');

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250621190140_productModelCleanup2', '9.0.6');

ALTER TABLE "AspNetUsers" DROP COLUMN "UserType";

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250623161500_removesUserTypeInUserModel', '9.0.6');

ALTER TABLE "ProductAttributes" ADD "IsRequired" boolean NOT NULL DEFAULT FALSE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250623213424_addsIsRequireedForAttributes', '9.0.6');

ALTER TABLE "Products" ADD "ImageUrl" text;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250626175308_AddProductImageUrl', '9.0.6');

ALTER TABLE "Products" ADD "Name" character varying(200) NOT NULL DEFAULT '';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250628181845_AddProductName', '9.0.6');

INSERT INTO "ProductTypes" ("Id", "Name")
VALUES (1, 'Electronics');
INSERT INTO "ProductTypes" ("Id", "Name")
VALUES (2, 'Fire Safety Equipment');
INSERT INTO "ProductTypes" ("Id", "Name")
VALUES (3, 'Clothing');

INSERT INTO "ProductAttributes" ("Id", "DataType", "IsRequired", "Name", "ProductTypeId")
VALUES (1, 'string', TRUE, 'Color', 1);
INSERT INTO "ProductAttributes" ("Id", "DataType", "IsRequired", "Name", "ProductTypeId")
VALUES (2, 'string', TRUE, 'Storage', 1);
INSERT INTO "ProductAttributes" ("Id", "DataType", "IsRequired", "Name", "ProductTypeId")
VALUES (3, 'string', TRUE, 'Brand', 1);
INSERT INTO "ProductAttributes" ("Id", "DataType", "IsRequired", "Name", "ProductTypeId")
VALUES (4, 'string', TRUE, 'ExtinguisherClass', 2);
INSERT INTO "ProductAttributes" ("Id", "DataType", "IsRequired", "Name", "ProductTypeId")
VALUES (5, 'string', TRUE, 'Capacity', 2);
INSERT INTO "ProductAttributes" ("Id", "DataType", "IsRequired", "Name", "ProductTypeId")
VALUES (6, 'string', FALSE, 'Material', 2);
INSERT INTO "ProductAttributes" ("Id", "DataType", "IsRequired", "Name", "ProductTypeId")
VALUES (7, 'string', TRUE, 'Size', 3);
INSERT INTO "ProductAttributes" ("Id", "DataType", "IsRequired", "Name", "ProductTypeId")
VALUES (8, 'string', TRUE, 'Material', 3);
INSERT INTO "ProductAttributes" ("Id", "DataType", "IsRequired", "Name", "ProductTypeId")
VALUES (9, 'string', TRUE, 'Color', 3);

INSERT INTO "Products" ("Id", "ImageUrl", "Name", "Price", "ProductTypeId", "Stock")
VALUES (1, 'https://example.com/iphone15.jpg', 'iPhone 15', 999.0, 1, 50);
INSERT INTO "Products" ("Id", "ImageUrl", "Name", "Price", "ProductTypeId", "Stock")
VALUES (2, 'https://example.com/galaxy-s24.jpg', 'Samsung Galaxy S24', 899.0, 1, 30);
INSERT INTO "Products" ("Id", "ImageUrl", "Name", "Price", "ProductTypeId", "Stock")
VALUES (3, 'https://example.com/macbook-pro.jpg', 'MacBook Pro', 1999.0, 1, 20);
INSERT INTO "Products" ("Id", "ImageUrl", "Name", "Price", "ProductTypeId", "Stock")
VALUES (4, 'https://example.com/abc-extinguisher.jpg', 'ABC Fire Extinguisher', 89.99, 2, 100);
INSERT INTO "Products" ("Id", "ImageUrl", "Name", "Price", "ProductTypeId", "Stock")
VALUES (5, 'https://example.com/co2-extinguisher.jpg', 'CO2 Fire Extinguisher', 149.99, 2, 75);
INSERT INTO "Products" ("Id", "ImageUrl", "Name", "Price", "ProductTypeId", "Stock")
VALUES (6, 'https://example.com/fire-blanket.jpg', 'Fire Blanket', 29.99, 2, 200);
INSERT INTO "Products" ("Id", "ImageUrl", "Name", "Price", "ProductTypeId", "Stock")
VALUES (7, 'https://example.com/firefighter-jacket.jpg', 'Firefighter Jacket', 299.99, 3, 25);
INSERT INTO "Products" ("Id", "ImageUrl", "Name", "Price", "ProductTypeId", "Stock")
VALUES (8, 'https://example.com/safety-helmet.jpg', 'Safety Helmet', 89.99, 3, 60);

INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (1, 1, 1, 'Black');
INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (2, 1, 1, 'White');
INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (3, 2, 1, '128GB');
INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (4, 2, 1, '256GB');
INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (5, 3, 1, 'Apple');
INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (6, 1, 2, 'Black');
INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (7, 1, 2, 'Blue');
INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (8, 2, 2, '128GB');
INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (9, 2, 2, '512GB');
INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (10, 3, 2, 'Samsung');
INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (11, 1, 3, 'Space Gray');
INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (12, 1, 3, 'Silver');
INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (13, 2, 3, '512GB');
INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (14, 2, 3, '1TB');
INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (15, 3, 3, 'Apple');
INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (16, 4, 4, 'ABC');
INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (17, 5, 4, '5 lbs');
INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (18, 5, 4, '10 lbs');
INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (19, 6, 4, 'Steel');
INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (20, 4, 5, 'CO2');
INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (21, 5, 5, '10 lbs');
INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (22, 6, 5, 'Aluminum');
INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (23, 4, 6, 'N/A');
INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (24, 5, 6, '4ft x 6ft');
INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (25, 6, 6, 'Fiberglass');
INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (26, 7, 7, 'Large');
INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (27, 7, 7, 'XL');
INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (28, 8, 7, 'Nomex');
INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (29, 9, 7, 'Yellow');
INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (30, 9, 7, 'Orange');
INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (31, 7, 8, 'Medium');
INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (32, 7, 8, 'Large');
INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (33, 8, 8, 'ABS Plastic');
INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (34, 9, 8, 'White');
INSERT INTO "ProductAttributeValues" ("Id", "ProductAttributeId", "ProductId", "Value")
VALUES (35, 9, 8, 'Yellow');

SELECT setval(
    pg_get_serial_sequence('"ProductTypes"', 'Id'),
    GREATEST(
        (SELECT MAX("Id") FROM "ProductTypes") + 1,
        nextval(pg_get_serial_sequence('"ProductTypes"', 'Id'))),
    false);
SELECT setval(
    pg_get_serial_sequence('"ProductAttributes"', 'Id'),
    GREATEST(
        (SELECT MAX("Id") FROM "ProductAttributes") + 1,
        nextval(pg_get_serial_sequence('"ProductAttributes"', 'Id'))),
    false);
SELECT setval(
    pg_get_serial_sequence('"Products"', 'Id'),
    GREATEST(
        (SELECT MAX("Id") FROM "Products") + 1,
        nextval(pg_get_serial_sequence('"Products"', 'Id'))),
    false);
SELECT setval(
    pg_get_serial_sequence('"ProductAttributeValues"', 'Id'),
    GREATEST(
        (SELECT MAX("Id") FROM "ProductAttributeValues") + 1,
        nextval(pg_get_serial_sequence('"ProductAttributeValues"', 'Id'))),
    false);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250628193324_SeedData', '9.0.6');

ALTER TABLE "AspNetUsers" ADD "RefreshToken" text;

ALTER TABLE "AspNetUsers" ADD "RefreshTokenExpiryTime" timestamp with time zone;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250628211215_AddRefreshTokenFields', '9.0.6');

COMMIT;

