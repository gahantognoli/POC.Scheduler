CREATE DATABASE HANGFIRE

CREATE DATABASE TodoDB
GO
USE TodoDB

CREATE TABLE Todos (
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	Title VARCHAR(100) NOT NULL,
	Completed BIT NOT NULL
)