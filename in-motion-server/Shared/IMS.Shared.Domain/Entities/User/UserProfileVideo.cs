using IMS.Shared.Domain.Entities.Bases;

namespace IMS.Shared.Domain.Entities.User;

// TODO: Add reactions
public class UserProfileVideo: BaseVideo
{
    public virtual IEnumerable<UserProfileVideoReaction> UserProfileVideoReactions { get; set; }
}