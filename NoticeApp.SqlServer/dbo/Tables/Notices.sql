--[1] Table : Notices 공지사항 테이블
CREATE TABLE [dbo].[Notices]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1) , -- Serial Number
	[ParentId] INT NUll,                          -- ParentId, AppId, SiteId, ...

	[Name]  NVarChar(255) Not Null,
	[Title] NVarChar(255) NUll,
	[Content] NVarChar(Max) Null, 
	[IsPinned] Bit Null Default(0),
	[CreatedBy] NvarChar(255) Null,
	[Created] DateTime Default(GetDate()) Null,
	[ModifiedBy] NvarChar(255) Null,
	[Modified] DateTime Null

)

Go