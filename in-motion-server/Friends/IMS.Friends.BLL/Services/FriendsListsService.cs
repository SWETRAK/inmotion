using AutoMapper;
using IMS.Friends.Domain.Entities;
using IMS.Friends.IBLL.Services;
using IMS.Friends.IDAL.Repositories;
using IMS.Friends.Models.Dto.Outgoing;
using Microsoft.Extensions.Logging;

namespace IMS.Friends.BLL.Services;

public class FriendsListsService : IFriendsListsService
{
    private readonly IFriendshipRepository _friendshipRepository;
    private readonly IUserService _userService;
    private readonly ILogger<FriendsListsService> _logger;
    private readonly IMapper _mapper;

    public FriendsListsService(
        IFriendshipRepository friendshipRepository,
        ILogger<FriendsListsService> logger,
        IMapper mapper,
        IUserService userService)
    {
        _friendshipRepository = friendshipRepository;
        _logger = logger;
        _mapper = mapper;
        _userService = userService;
    }

    //TODO: Test this method
    public async Task<IEnumerable<AcceptedFriendshipDto>> GetFriendsAsync(string userStringId)
    {
        if (!Guid.TryParse(userStringId, out var userGuidId))
            throw new Exception();

        var acceptedUsers = await _friendshipRepository.GetAccepted(userGuidId);
        var userIds = acceptedUsers.Select(f => !f.FirstUserId.Equals(userGuidId) ? f.FirstUserId : f.SecondUserId);

        var friendsInfo = await _userService.GetUsersFromIdArray(userIds);

        var result = _mapper.Map<List<Friendship>, IEnumerable<AcceptedFriendshipDto>>(acceptedUsers,
            opt => opt.AfterMap((src, dest) =>
            {
                dest = dest.Select<AcceptedFriendshipDto, AcceptedFriendshipDto>((d) =>
                {
                    var sourceObject = src.First<Friendship>(f => f.Id.Equals(Guid.Parse(d.Id)));
                    d.ExternalUserId = !sourceObject.FirstUserId.Equals(userGuidId)
                        ? sourceObject.FirstUserId.ToString()
                        : sourceObject.SecondUserId.ToString();
                    d.ExternalUser = _mapper.Map<FriendInfoDto>(friendsInfo.FirstOrDefault(ui => ui.Id.Equals(Guid.Parse(d.ExternalUserId))));
                    return d;
                });
            }));
        return result;
    }

    // TODO: Test this method
    public async Task<IEnumerable<RequestFriendshipDto>> GetRequestsAsync(string userStringId)
    {
        if (!Guid.TryParse(userStringId, out var userGuidId))
            throw new Exception();

        var requestUsers = await _friendshipRepository.GetRequested(userGuidId);

        var userIds = requestUsers.Select(f => f.FirstUserId);

        var friendsInfo = await _userService.GetUsersFromIdArray(userIds);
        
        var result = _mapper.Map<List<Friendship>, IEnumerable<RequestFriendshipDto>>(requestUsers,
            opt => opt.AfterMap((src, dest) =>
            {
                dest = dest.Select<RequestFriendshipDto, RequestFriendshipDto>((d) =>
                {
                    var sourceObject = src.First<Friendship>(f => f.Id.Equals(Guid.Parse(d.Id)));
                    d.ExternalUserId = !sourceObject.FirstUserId.Equals(userGuidId)
                        ? sourceObject.FirstUserId.ToString()
                        : sourceObject.SecondUserId.ToString();
                    d.ExternalUser = _mapper.Map<FriendInfoDto>(friendsInfo.FirstOrDefault(ui => ui.Id.Equals(Guid.Parse(d.ExternalUserId))));
                    return d;
                });
            }));

        return result;
    }

    // TODO: Test this method
    public async Task<IEnumerable<InvitationFriendshipDto>> GetInvitationsAsync(string userStringId)
    {
        if (!Guid.TryParse(userStringId, out var userGuidId))
            throw new Exception();

        var invitationUsers = await _friendshipRepository.GetInvitation(userGuidId);
        var userIds = invitationUsers.Select(f => f.SecondUserId);
        
        var friendsInfo = await _userService.GetUsersFromIdArray(userIds);
        
        var result = _mapper.Map<List<Friendship>, IEnumerable<InvitationFriendshipDto>>(invitationUsers,
            opt => opt.AfterMap((src, dest) =>
            {
                dest = dest.Select<InvitationFriendshipDto, InvitationFriendshipDto>((d) =>
                {
                    var sourceObject = src.First<Friendship>(f => f.Id.Equals(Guid.Parse(d.Id)));
                    d.ExternalUserId = !sourceObject.FirstUserId.Equals(userGuidId)
                        ? sourceObject.FirstUserId.ToString()
                        : sourceObject.SecondUserId.ToString();
                    d.ExternalUser = _mapper.Map<FriendInfoDto>(friendsInfo.FirstOrDefault(ui => ui.Id.Equals(Guid.Parse(d.ExternalUserId))));
                    return d;
                });
            }));

        return result;
    }
}