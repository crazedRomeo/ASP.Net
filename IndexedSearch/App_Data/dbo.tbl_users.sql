CREATE TABLE [dbo].[tbl_users] (
    [id]         INT           IDENTITY (1, 1) NOT NULL,
    [userId]     INT           NOT NULL,
    [username]   VARCHAR (100) NOT NULL,
    [role]       VARCHAR (50)  NOT NULL,
    [lastLogin]  DATETIME      NULL,
    [fname]      VARCHAR (100) NULL,
    [lname]      VARCHAR (100) NULL,
    [department] VARCHAR (100) NULL,
    [doj]        DATE          NOT NULL,
    [mgld]       INT           NULL,
    [seniority]  FLOAT (53)    NULL,
    [empCode]    VARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([userId] ASC),
    UNIQUE NONCLUSTERED ([username] ASC),
    UNIQUE NONCLUSTERED ([empCode] ASC)
);

