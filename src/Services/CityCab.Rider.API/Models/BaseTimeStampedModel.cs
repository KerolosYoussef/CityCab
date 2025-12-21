namespace CityCab.Rider.API.Models
{
    public class BaseTimeStampedModel : BaseModel
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
