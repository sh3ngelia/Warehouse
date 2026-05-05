create table RolePermissions
(
    RoleId int references Roles(RoleId),
    PermissionId int references Permissions(PermissionId),
    primary key (RoleId, PermissionId)
);