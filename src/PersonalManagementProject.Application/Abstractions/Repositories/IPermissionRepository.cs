﻿using PersonalManagementProject.Domain.Entities.Auth;
using PersonalManagementProject.Shared.Persistence.Abstraction;

namespace PersonalManagementProject.Application.Abstractions.Repositories;

public interface IPermissionRepository : IReadRepository<Permission>, IWriteRepository<Permission>
{
}