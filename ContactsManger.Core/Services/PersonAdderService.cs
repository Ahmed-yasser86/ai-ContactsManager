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
    public class PersonAdderService : IPersonAdderService
    {
        private readonly PersonRepositryContract PersonRipository;
        private readonly ILogger<PersonAdderService> _logger;

        public PersonAdderService(PersonRepositryContract personRipository, ILogger<PersonAdderService> logger)
        {
            PersonRipository = personRipository;
            _logger = logger;
        }

        public async Task<PersonRespones> AddPerson(PersonAddRequest? personAddRequest)
        {
            using (Operation.Time("Add person operation for: {PersonName}", personAddRequest?.Name ?? "null"))
            {
                _logger.LogInformation("Executing {MethodName} method at {Timestamp}. Request data: {@PersonAddRequest}",
                    nameof(AddPerson), DateTime.UtcNow, personAddRequest);

                try
                {
                    if (personAddRequest == null)
                    {
                        _logger.LogWarning("AddPerson called with null request parameter");
                        throw new ArgumentNullException(nameof(personAddRequest));
                    }

                    _logger.LogDebug("Validating person add request");
                    ValidationHelpers.ValidationFunction(personAddRequest);

                    _logger.LogDebug("Converting PersonAddRequest to Person entity");
                    var Person = personAddRequest.ToPerson();
                    Person.PersonId = Guid.NewGuid();

                    _logger.LogDebug("Adding new person with ID: {PersonId}, Name: {PersonName}",
                        Person.PersonId, Person.Name);

                    await PersonRipository.AddPerson(Person);

                    var PersonResponsType = Person.ConvertToPersonRespons();
                    PersonResponsType.CountryName = Person.Country?.CountryName;

                    _logger.LogInformation("Successfully added new person. ID: {PersonId}, Name: {PersonName}, Country: {CountryName}",
                        PersonResponsType.PersonId, PersonResponsType.Name, PersonResponsType.CountryName);

                    return PersonResponsType;
                }
                catch (ValidationException ex)
                {
                    _logger.LogWarning(ex, "Validation error in AddPerson for request: {@PersonAddRequest}", personAddRequest);
                    throw;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unexpected error occurred in AddPerson for request: {@PersonAddRequest}", personAddRequest);
                    throw;
                }
            }
        }
    }
}