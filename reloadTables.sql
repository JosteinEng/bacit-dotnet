create database if not exists webAppDatabase;
use webAppDatabase;

drop table if exists Suggestions;

drop table if exists justdoit;

drop table if exists Teams;

drop table if exists users;

drop table if exists AspNetUserLogins;

drop table if exists AspNetUserClaims;

drop table if exists AspNetRoleClaims;

drop table if exists AspNetUserRoles;

drop table if exists AspNetUsers;

drop table if exists AspNetRoles;

drop table if exists AspNetUserTokens;

create or replace table AspNetUserTokens
(
    UserId        varchar(255) not null,
    LoginProvider varchar(255) not null,
    Name          varchar(255) not null,
    Value         varchar(255) null,
    primary key (UserId, LoginProvider)
);

create or replace table AspNetRoles
(
    Id               varchar(255) not null
        primary key,
    Name             varchar(255) null,
    NormalizedName   varchar(255) null,
    ConcurrencyStamp varchar(255) null
);

create or replace table AspNetUsers
(
    Id                   varchar(255)                          not null
        primary key,
    UserName             varchar(255)                          null,
    NormalizedUserName   varchar(255)                          null,
    Email                varchar(255)                          null,
    NormalizedEmail      varchar(255)                          null,
    EmailConfirmed       bit                                   not null,
    PasswordHash         varchar(255)                          null,
    SecurityStamp        varchar(255)                          null,
    ConcurrencyStamp     varchar(255)                          null,
    PhoneNumber          varchar(50)                           null,
    PhoneNumberConfirmed bit                                   not null,
    TwoFactorEnabled     bit                                   not null,
    LockoutEnd           timestamp default current_timestamp() not null on update current_timestamp(),
    LockoutEnabled       bit                                   not null,
    AccessFailedCount    int                                   not null,
    constraint Id
        unique (Id)
);

create or replace table AspNetUserRoles
(
    UserId varchar(255) not null,
    RoleId varchar(255) not null,
    primary key (UserId, RoleId),
    constraint aspnetuserroles_ibfk_1
        foreign key (UserId) references AspNetUsers (Id),
    constraint aspnetuserroles_ibfk_2
        foreign key (RoleId) references AspNetRoles (Id)
);

create or replace table AspNetRoleClaims
(
    Id         int auto_increment
        primary key,
    ClaimType  varchar(255) not null,
    ClaimValue varchar(255) not null,
    RoleId     varchar(255) null,
    constraint Id
        unique (Id),
    constraint aspnetroleclaims_ibfk_1
        foreign key (RoleId) references AspNetRoles (Id)
);

create or replace table AspNetUserClaims
(
    Id         int auto_increment
        primary key,
    ClaimType  varchar(255) null,
    ClaimValue varchar(255) null,
    UserId     varchar(255) null,
    constraint Id
        unique (Id),
    constraint aspnetuserclaims_ibfk_1
        foreign key (UserId) references AspNetUsers (Id)
);

create or replace table AspNetUserLogins
(
    LoginProvider       int auto_increment
        primary key,
    ProviderKey         varchar(255) not null,
    ProviderDisplayName varchar(255) not null,
    UserId              varchar(255) not null,
    constraint LoginProvider
        unique (LoginProvider),
    constraint aspnetuserlogins_ibfk_1
        foreign key (UserId) references AspNetUsers (Id)
);

create or replace table users
(
    Id             int auto_increment
        primary key,
    FirstName      varchar(255) null,
    LastName       varchar(255) null,
    Email          varchar(255) null,
    Password       varchar(255) null,
    EmployeeNumber varchar(255) null,
    Team           varchar(255) null,
    Role           varchar(255) null,
    constraint Email
        unique (Email),
    constraint Id
        unique (Id)
);

create or replace table Teams
(
    teamID    int auto_increment
        primary key,
    team_name varchar(255) not null,
    userID    int          not null,
    constraint team_user_fk
        foreign key (userID) references users (Id)
);

create or replace table justdoit
(
    justdoitId      int auto_increment
        primary key,
    employeeId      int          not null,
    title           varchar(50)  not null,
    description     varchar(500) not null,
    createdDate     datetime     not null,
    category        varchar(50)  not null,
    attachments     longblob     null,
    attachmentAfter longblob     null,
    teamId          int          not null,
    constraint justdoit_employee_fk
        foreign key (employeeId) references users (Id),
    constraint justdoit_team_fk
        foreign key (teamId) references Teams (teamID)
);

create table Suggestions
(
    suggestionId int auto_increment
        primary key,
    employeeId   int          not null,
    title        varchar(50)  not null,
    description  varchar(500) not null,
    createdDate  datetime     not null,
    deadline     date         not null,
    status	     varchar(50)  not null,
    phase 	     varchar(50)  not null,
    category     varchar(50)  not null,
    attachments  longblob     null,
    attachmentsAfter longblob     null,
    teamID       int          not null,
    userID       int          not null,
    constraint suggestion_team_fk
		foreign key (teamID) references Teams (teamID),
    constraint suggestion_user_fk
        foreign key (userID) references users (Id)
);

INSERT INTO AspNetRoles(id, Name, NormalizedName) values('Administrator', 'Administrator', 'Administrator');
INSERT INTO users(FirstName, LastName, Email) values('Admin','Admin', '0');
INSERT INTO AspNetUsers (Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount) VALUES ('606e1db6-5c43-498e-af7c-e71584a996d9', '0', '0', '0', '0', false, 'AQAAAAEAACcQAAAAELQ/9cNgM3ennubpIne79w06/vzScUYxAurHgtfGfFSlD/DnN+mIJIrrvd3tajjGVg==', '5S5BZLOOPQTHUNKU4NZPS46OWSOY3I44', '6eb488b9-617c-4e32-a036-1c7879ba7cec', null, false, false, '2022-11-03 10:17:59', false, 0);
INSERT INTO webAppDatabase.AspNetUserRoles (UserId, RoleId) VALUES ('606e1db6-5c43-498e-af7c-e71584a996d9', 'Administrator'
);