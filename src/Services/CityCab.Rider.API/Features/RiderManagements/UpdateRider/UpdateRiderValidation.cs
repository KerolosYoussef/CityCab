namespace CityCab.Rider.API.Features.RiderManagements.UpdateRider
{
    public class UpdateRiderValidation : BaseRiderValidation<UpdateRiderCommand>
    {
        public UpdateRiderValidation()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();
        }
    }
}
