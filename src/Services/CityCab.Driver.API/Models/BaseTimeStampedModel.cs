namespace CityCab.Driver.API.Models
{
    public class BaseTimeStampedModel : BaseModel
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
