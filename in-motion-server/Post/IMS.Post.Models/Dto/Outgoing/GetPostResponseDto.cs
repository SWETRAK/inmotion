namespace IMS.Post.Models.Dto.Outgoing;

public class GetPostResponseDto
{
    public string Id { get; set; }
    
    public string Description { get; set;}
    public string Title { get; set; }
    
    public PostAuthorDto Author { get; set; }
    
    public IEnumerable<PostTagDto> Tags { get; set; }
    
    public PostLocalizationDto Localization { get; set; }

    public IEnumerable<PostVideoDto> Videos { get; set; }
    
    public uint PostCommentsCount { get; set; }
    public uint PostReactionsCount { get; set; }
    
    public DateTime CreatedAt { get; set; }
}