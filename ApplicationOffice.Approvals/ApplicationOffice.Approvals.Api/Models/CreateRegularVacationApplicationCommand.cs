using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApplicationOffice.Approvals.Api.Models
{
    public class CreateRegularVacationApplicationCommand : IValidatableObject
    {
        [Required]
        public string Description { get; set; } = default!;

        public DateTime VacationFrom { get; set; }
        public DateTime VacationTo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (VacationFrom.Date <= DateTime.UtcNow.Date)
                yield return new ValidationResult("Неверная дата начала отпуска");
            if (VacationTo.Date < VacationFrom)
                yield return new ValidationResult("Неверная дата окончания отпуска");
        }
    }
}
