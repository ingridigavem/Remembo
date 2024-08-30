namespace Remembo.Domain.Remembo.DTOs;
public record ContentDetailDto {
    public ContentDetailDto(Guid contentId, string contentName, string note, short reviewNumber, ReviewDetailDto? currentReview) {
        ContentId = contentId;
        ContentName = contentName;
        Note = note;
        ReviewNumber = reviewNumber;
        CurrentReview = currentReview;
    }

    protected ContentDetailDto(Guid contentId, string contentName, string note, short reviewNumber) {
        ContentId = contentId;
        ContentName = contentName;
        Note = note;
        ReviewNumber = reviewNumber;
    }

    public Guid ContentId { get; set; }
    public string ContentName { get; set; }
    public string Note { get; set; }
    public short ReviewNumber { get; set; }
    public ReviewDetailDto? CurrentReview { get; set; }
}