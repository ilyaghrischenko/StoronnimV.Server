using StoronnimV.Application.Contracts.Entities.Shared;
using StoronnimV.Application.DTO.Requests.Account;
using StoronnimV.Application.DTO.Requests.Entities.Pages.Addition;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Projections;
using StoronnimV.Domain.Projections.Admin;

namespace StoronnimV.Application.Contracts.Entities;

public interface IAdminService : IGetByIdService<AdminProjection>
{ }