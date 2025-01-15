using BlazorBootstrap;
using MooDeng.Blazor.Pages;
using MooDeng.Parties.IServices.Dtos;
using System.Net.Http.Json;

namespace MooDeng.Blazor.ViewModels
{
    public class OrganizationViewModel : OrganizationDto
    {
        public OrganizationViewModel()
        {
            PartyRole = new TypeInfoDto
            {
                TypeCode = "ZOO",
                EffectiveDateTime = DateTime.Today,
                ExpiryDateTime = DateTime.MaxValue,
            };
        }
        public bool IsEditing { get; set; }

        public async Task Edit()
        {
            IsEditing = true;
        }

        public async Task Cancel(List<OrganizationViewModel> repo)
        {
            if (!PartyId.HasValue)
            {
                repo.Remove(this);
            }
            IsEditing = false;
        }
    }
}
