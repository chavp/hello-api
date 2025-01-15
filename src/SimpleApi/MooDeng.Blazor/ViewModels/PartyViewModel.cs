using MooDeng.Parties.IServices.Dtos;

namespace MooDeng.Blazor.ViewModels
{
    public class PartyViewModel: PartyDto
    {
        public bool IsEditing { get; set; }
        public PartyViewModel()
        {
            PartyRole = new PartyRoleDto
            {
                TypeCode = "PET",
                EffectiveDateTime = DateTime.Today,
                ExpiryDateTime = DateTime.MaxValue,
            };
            RelationshipParty = new RelationshipPartyDto
            {
                TypeCode = "BRING_UP",
                EffectiveDateTime = DateTime.Today,
                ExpiryDateTime = DateTime.MaxValue,
            };
        }

        public async Task Edit()
        {
            IsEditing = true;
        }

        public async Task Cancel(List<PartyViewModel> repo)
        {
            if (!PartyId.HasValue)
            {
                repo.Remove(this);
            }
            IsEditing = false;
        }
    }
}
