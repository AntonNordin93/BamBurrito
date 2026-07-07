using System;
using System.ComponentModel.DataAnnotations;

namespace BamBurrito.Core.Entities
{
    public class OfferRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Namn är obligatoriskt")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email är obligatoriskt")]
        [EmailAddress(ErrorMessage = "Ogiltig email-adress")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Adress är obligatorisk")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Datum är obligatoriskt")]
        public DateTime Date { get; set; } = DateTime.Now.Date.AddDays(7);

        [Required(ErrorMessage = "Från-tid är obligatorisk")]
        public DateTime StartTime { get; set; } = DateTime.Now.Date.AddHours(12);

        [Required(ErrorMessage = "Till-tid är obligatorisk")]
        public DateTime EndTime { get; set; } = DateTime.Now.Date.AddHours(15);

        [Required(ErrorMessage = "Ett meddelande måste anges")]
        public string Message { get; set; } = string.Empty;
    }
}
