using Microsoft.EntityFrameworkCore.Migrations;

namespace PIS.Data.Migrations
{
    public partial class defaultdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql( "SET IDENTITY_INSERT[dbo].[Unit] ON " );
			migrationBuilder.Sql( "INSERT INTO[dbo].[Unit]([Id], [strName], [strAbbrev], [iSIMultiplier], [MaterialId], [dtBeg], [dtEnd], [dtCreationDate]) VALUES( 1, N'Darab', N'db', 1, NULL, N'2021-12-04 12:00:00', N'9999-12-31 23:59:59', N'2021-12-04 12:00:00' )" );
			migrationBuilder.Sql( "INSERT INTO [dbo].[Unit] ([Id], [strName], [strAbbrev], [iSIMultiplier], [MaterialId], [dtBeg], [dtEnd], [dtCreationDate]) VALUES( 2, N'Liter', N'l', 1, NULL, N'2021-12-04 12:00:00', N'9999-12-31 23:59:59', N'2021-12-04 12:00:00' )" );
			migrationBuilder.Sql( "INSERT INTO [dbo].[Unit] ([Id], [strName], [strAbbrev], [iSIMultiplier], [MaterialId], [dtBeg], [dtEnd], [dtCreationDate]) VALUES( 3, N'Kilo', N'kg', 1, NULL, N'2021-12-04 12:00:00', N'9999-12-31 23:59:59', N'2021-12-04 12:00:00' )" );
			migrationBuilder.Sql( "SET IDENTITY_INSERT [dbo].[Unit] OFF" );

			migrationBuilder.Sql( "SET IDENTITY_INSERT[dbo].[Currency] ON" );
			migrationBuilder.Sql( "INSERT INTO[dbo].[Currency]([Id], [strName], [strAbbrev], [dtBeg], [dtEnd], [dtCreationDate]) VALUES( 1, N'Forint', N'Ft', N'2021-12-04 12:00:00', N'9999-12-31 23:59:59', N'2021-12-04 12:00:00' )" );
			migrationBuilder.Sql( "INSERT INTO [dbo].[Currency] ([Id], [strName], [strAbbrev], [dtBeg], [dtEnd], [dtCreationDate]) VALUES( 2, N'Euro', N'E', N'2021-12-04 12:00:00', N'9999-12-31 23:59:59', N'2021-12-04 12:00:00' )" );
			migrationBuilder.Sql( "INSERT INTO [dbo].[Currency] ([Id], [strName], [strAbbrev], [dtBeg], [dtEnd], [dtCreationDate]) VALUES( 3, N'Dollar', N'$', N'2021-12-04 12:00:00', N'9999-12-31 23:59:59', N'2021-12-04 12:00:00' )" );
			migrationBuilder.Sql( "SET IDENTITY_INSERT [dbo].[Currency] OFF" );


			migrationBuilder.Sql( "SET IDENTITY_INSERT[dbo].[Price] ON" );
			migrationBuilder.Sql( "INSERT INTO[dbo].[Price]([Id], [ePriceType], [nPrice], [CurrencyId], [MaterialId], [dtBeg], [dtEnd], [dtCreationDate]) VALUES( 1, 0, CAST( 200.00 AS Decimal( 18, 2 ) ), 2, NULL, N'2021-12-04 12:00:00', N'9999-12-31 23:59:59', N'2021-12-04 12:00:00' )" );
			migrationBuilder.Sql( "INSERT INTO [dbo].[Price] ([Id], [ePriceType], [nPrice], [CurrencyId], [MaterialId], [dtBeg], [dtEnd], [dtCreationDate]) VALUES( 2, 0, CAST( 2000.00 AS Decimal( 18, 2 ) ), 1, NULL, N'2021-12-04 12:00:00', N'9999-12-31 23:59:59', N'2021-12-04 12:00:00' )" );
			migrationBuilder.Sql( "SET IDENTITY_INSERT [dbo].[Price] OFF" );

			migrationBuilder.Sql( "SET IDENTITY_INSERT[dbo].[Material] ON" );
			migrationBuilder.Sql( "INSERT INTO[dbo].[Material]([Id], [strName], [strDescription], [PrimaryUnitId], [eMaterialType], [dtBeg], [dtEnd], [dtCreationDate], [DefaultUnitPriceId]) VALUES( 1, N'Parfüm', N'Dolce Gabannna', 1, 0, N'2021-12-04 12:00:00', N'9999-12-31 23:59:59', N'2021-12-04 12:00:00', 1 )" );
			migrationBuilder.Sql( "INSERT INTO [dbo].[Material] ([Id], [strName], [strDescription], [PrimaryUnitId], [eMaterialType], [dtBeg], [dtEnd], [dtCreationDate], [DefaultUnitPriceId]) VALUES( 2, N'Szén', N'Üzemanyag', 3, 0, N'2021-12-04 12:00:00', N'9999-12-31 23:59:59', N'2021-12-04 12:00:00', 1 )" );
			migrationBuilder.Sql( "INSERT INTO [dbo].[Material] ([Id], [strName], [strDescription], [PrimaryUnitId], [eMaterialType], [dtBeg], [dtEnd], [dtCreationDate], [DefaultUnitPriceId]) VALUES( 3, N'Kóla', N'', 2, 0, N'2021-12-04 12:00:00', N'9999-12-31 23:59:59', N'2021-12-04 12:00:00', 2 )" );
			migrationBuilder.Sql( "INSERT INTO [dbo].[Material] ([Id], [strName], [strDescription], [PrimaryUnitId], [eMaterialType], [dtBeg], [dtEnd], [dtCreationDate], [DefaultUnitPriceId]) VALUES( 4, N'Pokemon tazó', N'Ez ingyen volt régen', 1, 0, N'2021-12-04 12:00:00', N'9999-12-31 23:59:59', N'2021-12-04 12:00:00', 2 )" );
			migrationBuilder.Sql( "INSERT INTO [dbo].[Material] ([Id], [strName], [strDescription], [PrimaryUnitId], [eMaterialType], [dtBeg], [dtEnd], [dtCreationDate], [DefaultUnitPriceId]) VALUES( 5, N'Katalizátor', N'', 3, 0, N'2021-12-04 12:00:00', N'9999-12-31 23:59:59', N'2021-12-04 12:00:00', 2 )" );
			migrationBuilder.Sql( "SET IDENTITY_INSERT [dbo].[Material] OFF" );

			migrationBuilder.Sql( "SET IDENTITY_INSERT[dbo].[Address] ON" );
			migrationBuilder.Sql( "INSERT INTO[dbo].[Address]([Id], [strCountry], [strPostCode], [strCity], [ePublicPlace], [strPlaceName], [strHouseNumber], [dtBeg], [dtEnd], [dtCreationDate]) VALUES( 1, N'Magyarország', N'7630', N'Pécs', 0, N'Petőfi', N'1', N'2021-12-04 12:00:00', N'9999-12-31 23:59:59', N'2021-12-04 12:00:00' )" );
			migrationBuilder.Sql( "INSERT INTO [dbo].[Address] ([Id], [strCountry], [strPostCode], [strCity], [ePublicPlace], [strPlaceName], [strHouseNumber], [dtBeg], [dtEnd], [dtCreationDate]) VALUES( 2, N'Magyarország', N'7633', N'Pécs', 0, N'Mecsek', N'1', N'2021-12-04 12:00:00', N'9999-12-31 23:59:59', N'2021-12-04 12:00:00' )" );
			migrationBuilder.Sql( "INSERT INTO [dbo].[Address] ([Id], [strCountry], [strPostCode], [strCity], [ePublicPlace], [strPlaceName], [strHouseNumber], [dtBeg], [dtEnd], [dtCreationDate]) VALUES( 3, N'Magyarország', N'7624', N'Pécs', 0, N'Őz', N'1', N'2021-12-04 12:00:00', N'9999-12-31 23:59:59', N'2021-12-04 12:00:00' )" );
			migrationBuilder.Sql( "SET IDENTITY_INSERT [dbo].[Address] OFF" );
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
