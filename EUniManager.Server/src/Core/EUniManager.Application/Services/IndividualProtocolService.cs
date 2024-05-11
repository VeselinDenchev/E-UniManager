﻿using EUniManager.Application.Mappers;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Application.Models.IndividualProtocols.Dtos;
using EUniManager.Application.Models.IndividualProtocols.Interfaces;
using EUniManager.Application.Services.Base;
using EUniManager.Domain.Entities;

using Microsoft.AspNetCore.Identity;

namespace EUniManager.Application.Services;

public sealed class IndividualProtocolService 
    : BaseService<IndividualProtocol, Guid, IndividualProtocolDto, IndividualProtocolDetailsDto>,
      IIndividualProtocolService
{
    private readonly IndividualProtocolMapper _mapper = new();
    private readonly UserManager<IdentityUser<Guid>> _userManager;
    
    public IndividualProtocolService(IEUniManagerDbContext dbContext, UserManager<IdentityUser<Guid>> userManager) 
        : base(dbContext)
    {
        _userManager = userManager;
    }

    public override async Task<List<IndividualProtocolDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public override async ValueTask<IndividualProtocolDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public override async Task CreateAsync(ICreateDto dto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public override async Task UpdateAsync(Guid id, IUpdateDto dto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}