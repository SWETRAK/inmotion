using IMS.Shared.Domain.Consts;

namespace IMS.Shared.Domain.Entities.User;

// TODO: Add needed fields when data from server be known
public class Provider
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    public virtual User User { get; set; }

    public Providers Name { get; set; }
    public string AuthKey { get; set; }
}