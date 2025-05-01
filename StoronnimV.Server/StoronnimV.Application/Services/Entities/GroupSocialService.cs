using StoronnimV.Application.Contracts.Entities;
using StoronnimV.Application.Exceptions;
using StoronnimV.Domain.Contracts.AzureBlobStorage;
using StoronnimV.Domain.Contracts.Database;
using StoronnimV.Domain.Projections;

namespace StoronnimV.Application.Services.Entities;

public class GroupSocialService(
    IGroupSocialRepository groupSocialRepository,
    IBlobRepository blobRepository)
    : IGroupSocialService
{
    public async Task<GroupSocialProjection> GetItemByIdAsync(long id, CancellationToken ct)
    {
        GroupSocialProjection? groupSocialProjection = await groupSocialRepository.GetByIdAsNoTrackingAsync(id, ct);

        if (groupSocialProjection is null)
        {
            throw new EntityNotFoundException($"Group social with {nameof(id)}: {id} was not found");
        }

        return groupSocialProjection;
    }

    public async Task<IEnumerable<GroupSocialProjection>> GetAllAsync(CancellationToken ct)
    {
        var groupSocials = await groupSocialRepository.GetAllAsNoTrackingAsync(ct);

        return groupSocials ?? new List<GroupSocialProjection>();
    }
}