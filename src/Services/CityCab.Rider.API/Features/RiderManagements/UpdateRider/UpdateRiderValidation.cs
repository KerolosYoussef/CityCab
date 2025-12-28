using CityCab.Rider.API.Features.RiderManagements.Shared;

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
