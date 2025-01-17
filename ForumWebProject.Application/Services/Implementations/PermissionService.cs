﻿using ForumWebProject.Application.Auth;
using ForumWebProject.Application.Exceptions;
using ForumWebProject.Application.Models;
using ForumWebProject.Application.Models.Views;
using ForumWebProject.Application.Services.Interfaces;
using ForumWebProject.Infrastructure.Identity;
using ForumWebProject.Infrastructure.Repositories.Interfaces;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;

namespace ForumWebProject.Application.Services.Implementations;

public class PermissionService : IPermissionService
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly RoleManager<Role> _roleManager;
    private readonly ICurrentUser _currentUser;
    private readonly IMapper _mapper;
    
    public PermissionService(IPermissionRepository permissionRepository, IMapper mapper, ICurrentUser currentUser, RoleManager<Role> roleManager)
    {
        _permissionRepository = permissionRepository;
        _mapper = mapper;
        _currentUser = currentUser;
        _roleManager = roleManager;
    }

    public async Task<IEnumerable<PermissionView>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var permissions = await _permissionRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<PermissionView>>(permissions);
    }

    public async Task<PermissionView> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var permission = await _permissionRepository.GetByIdAsync(id);
        if (permission is null)
        {
            throw new NotFoundException($"Permission with id {id} not found.");
        }

        return _mapper.Map<PermissionView>(permission);
    }

    public async Task GrantToRole(Guid permissionId, Guid roleId, CancellationToken cancellationToken = default)
    {
        await _permissionRepository.GrantToRole(permissionId, roleId, cancellationToken);
    }

    public async Task RevokeFromRole(Guid permissionId, Guid roleId, CancellationToken cancellationToken = default)
    {
        await _permissionRepository.RevokeFromRole(permissionId, roleId, cancellationToken);
    }
}