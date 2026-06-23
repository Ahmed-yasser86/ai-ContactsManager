using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Entities;
using Microsoft.Extensions.Logging;
using ServiceContracts;
using ServiceContracts.DTOs;
using Servicess.Helpers;
using RepositryContracts;
using SerilogTimings;

namespace Servicess
{
    public class PersonUpdaterService : IPersonUpdaterService
    {
        private readonly PersonRepositryContract PersonRipository;
        private readonly ILogger<PersonUpdaterService> _logger;

        public PersonUpdaterService(PersonRepositryContract personRipository, ILogger<PersonUpdaterService> logger)
        {
            PersonRipository = personRipository;
            _logger = logger;
        }

        public async Task<PersonRespones?> UpdatePerson(PersonUpdateRequest? personUpdateRequest)
        {
            using (Operation.Time("Update person operation for ID: {PersonId}", personUpdateRequest?.PersonId))
            {
                _logger.LogInformation("Executing {MethodName} method at {Timestamp}. Request data: {@PersonUpdateRequest}",
                    nameof(UpdatePerson), DateTime.UtcNow, personUpdateRequest);

                try
                {
                    if (personUpdateRequest == null)
                    {
                        _logger.LogWarning("UpdatePerson called with null request parameter");
                        throw new ArgumentNullException(nameof(personUpdateRequest));
                    }

                    _logger.LogDebug("Validating person update request");
                    ValidationHelpers.ValidationFunction(personUpdateRequest);

                    _logger.LogDebug("Retrieving existing person with ID: {PersonId}", personUpdateRequest.PersonId);
                    var person = await PersonRipository?.GetPersonById(personUpdateRequest.PersonId);

                    if (person == null)
                    {
                        _logger.LogWarning("Attempted to update non-existent person with ID: {PersonId}", personUpdateRequest.PersonId);
                        throw new ArgumentException("Given person ID does not exist.");
                    }

                    _logger.LogDebug("Updating person fields for ID: {PersonId}", personUpdateRequest.PersonId);

                    // Log each field that is being updated
                    if (personUpdateRequest.Name != null && person.Name != personUpdateRequest.Name)
                        _logger.LogDebug("Updating Name from '{OldValue}' to '{NewValue}'", person.Name, personUpdateRequest.Name);

                    if (personUpdateRequest.DateOfBirth != null && person.DateOfBirth != personUpdateRequest.DateOfBirth)
                        _logger.LogDebug("Updating DateOfBirth from '{OldValue}' to '{NewValue}'", person.DateOfBirth, personUpdateRequest.DateOfBirth);

                    if (personUpdateRequest.email != null && person.email != personUpdateRequest.email)
                        _logger.LogDebug("Updating Email from '{OldValue}' to '{NewValue}'", person.email, personUpdateRequest.email);

                    if (personUpdateRequest.phone != null && person.phone != personUpdateRequest.phone)
                        _logger.LogDebug("Updating Phone from '{OldValue}' to '{NewValue}'", person.phone, personUpdateRequest.phone);

                    if (personUpdateRequest.Address != null && person.Address != personUpdateRequest.Address)
                        _logger.LogDebug("Updating Address from '{OldValue}' to '{NewValue}'", person.Address, personUpdateRequest.Address);

                    person.Name = personUpdateRequest.Name ?? person.Name;
                    person.DateOfBirth = personUpdateRequest.DateOfBirth ?? person.DateOfBirth;
                    person.email = personUpdateRequest.email ?? person.email;
                    person.phone = personUpdateRequest.phone ?? person.phone;
                    person.NewsLetter = personUpdateRequest.NewsLetter ?? person.NewsLetter;
                    person.Address = personUpdateRequest.Address ?? person.Address;
                    person.CountryId = personUpdateRequest.CountryId ?? person.CountryId;
                    person.Gender = personUpdateRequest.Gender.ToString() ?? person.Gender;

                    await PersonRipository.UpdatePerson(person);

                    var result = person.ConvertToPersonRespons();

                    _logger.LogInformation("Successfully updated person with ID: {PersonId}, Name: {PersonName}",
                        result.PersonId, result.Name);

                    return result;
                }
                catch (ArgumentException ex)
                {
                    _logger.LogWarning(ex, "Argument error in UpdatePerson for PersonId: {PersonId}",
                        personUpdateRequest?.PersonId);
                    throw;
                }
                catch (ValidationException ex)
                {
                    _logger.LogWarning(ex, "Validation error in UpdatePerson for request: {@PersonUpdateRequest}", personUpdateRequest);
                    throw;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unexpected error occurred in UpdatePerson for request: {@PersonUpdateRequest}", personUpdateRequest);
                    throw;
                }
            }
        }
    }
}