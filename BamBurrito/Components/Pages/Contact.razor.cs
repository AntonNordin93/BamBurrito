using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using BamBurrito.Core.Entities;
using BamBurrito.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace BamBurrito.Components.Pages
{
    public partial class Contact : ComponentBase
    {
        [Inject]
        public IEmailService EmailService { get; set; } = default!;

        [Inject]
        public ILogger<Contact> Logger { get; set; } = default!;

        private bool isModalOpen = false;
        private bool isSuccessModalOpen = false;
        private bool isSubmitting = false;
        private bool showError = false;
        
        private OfferRequest offerRequest = new();

        private void OpenModal()
        {
            offerRequest = new OfferRequest(); // Återställ formuläret när det öppnas
            showError = false;
            isModalOpen = true;
        }

        private void CloseModal()
        {
            isModalOpen = false;
        }

        private void CloseSuccessModal()
        {
            isSuccessModalOpen = false;
        }

        private async Task HandleSubmit()
        {
            isSubmitting = true;
            showError = false;
            StateHasChanged();

            try
            {
                await EmailService.SendOfferRequestEmailAsync(offerRequest);
                
                // Vid framgång
                isModalOpen = false;
                isSuccessModalOpen = true;
            }
            catch (System.Exception ex)
            {
                Logger.LogError(ex, "Misslyckades att skicka offertförfrågan via UI");
                showError = true;
            }
            finally
            {
                isSubmitting = false;
                StateHasChanged();
            }
        }
    }
}
