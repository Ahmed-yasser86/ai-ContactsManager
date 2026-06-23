using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Microsoft.Extensions.Logging;
using ServiceContracts;
using ServiceContracts.DTOs;
using RepositryContracts;
using SerilogTimings;

namespace Servicess
{
    public class PersonSearcherService : IPersonSearcherService
    {
        private readonly PersonRepositryContract PersonRipository;
        private readonly ILogger<PersonSearcherService> _logger;

        public PersonSearcherService(PersonRepositryContract personRipository, ILogger<PersonSearcherService> logger)
        {
            PersonRipository = personRipository;
            _logger = logger;
        }

        public async Task<List<PersonRespones>> SearchPersonsBy(string? PersonParamter, string SearchBy)
        {
            using (Operation.Time("Search persons by {SearchBy} with parameter: {Parameter}", SearchBy ?? "none", PersonParamter ?? "null"))
            {
                _logger.LogInformation("Executing {MethodName} method at {Timestamp}. SearchBy: {SearchBy}, Parameter: {Parameter}",
                    nameof(SearchPersonsBy), DateTime.UtcNow, SearchBy, PersonParamter);

                try
                {
                    List<PersonRespones> MatchingResults = new List<PersonRespones>();

                    switch (SearchBy)
                    {
                        case nameof(PersonRespones.Name):
                            {
                                _logger.LogDebug("Searching persons by Name containing: {Parameter}", PersonParamter);
                                var filteredPersons = await PersonRipository.GetFilteredPersons(p => p.Name != null && p.Name.Contains(PersonParamter));
                                MatchingResults = filteredPersons
                                    .Where(p => p != null)
                                    .Select(p => p!.ConvertToPersonRespons())
                                    .ToList();
                                break;
                            }
                        case nameof(PersonRespones.email):
                            {
                                _logger.LogDebug("Searching persons by Email containing: {Parameter}", PersonParamter);
                                var filteredPersons = await PersonRipository.GetFilteredPersons(p => p.email != null && p.email.Contains(PersonParamter));
                                MatchingResults = filteredPersons
                                    .Where(p => p != null)
                                    .Select(p => p!.ConvertToPersonRespons())
                                    .ToList();
                                break;
                            }
                        case nameof(PersonRespones.phone):
                            {
                                _logger.LogDebug("Searching persons by Phone containing: {Parameter}", PersonParamter);
                                var filteredPersons = await PersonRipository.GetFilteredPersons(p => p.phone != null && p.phone.Contains(PersonParamter));
                                MatchingResults = filteredPersons
                                    .Where(p => p != null)
                                    .Select(p => p!.ConvertToPersonRespons())
                                    .ToList();
                                break;
                            }
                        case nameof(PersonRespones.DateOfBirth):
                            {
                                _logger.LogDebug("Searching persons by DateOfBirth containing: {Parameter}", PersonParamter);
                                var filteredPersons = await PersonRipository.GetFilteredPersons(p => p.DateOfBirth != null && p.DateOfBirth.Value.ToString("yyyy-MM-dd").Contains(PersonParamter));
                                MatchingResults = filteredPersons
                                    .Where(p => p != null)
                                    .Select(p => p!.ConvertToPersonRespons())
                                    .ToList();
                                break;
                            }
                        default:
                            {
                                _logger.LogWarning("Unknown search criteria: {SearchBy}. Returning all persons.", SearchBy);
                                var Persons = await PersonRipository.GetAllPersons();
                                MatchingResults = Persons
                                    .Select(p => p!.ConvertToPersonRespons())
                                    .ToList();
                                break;
                            }
                    }

                    _logger.LogInformation("{MethodName} completed successfully. Found {Count} results for SearchBy: {SearchBy}, Parameter: {Parameter}",
                        nameof(SearchPersonsBy), MatchingResults.Count, SearchBy, PersonParamter);

                    return MatchingResults;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred in {MethodName} method. SearchBy: {SearchBy}, Parameter: {Parameter}",
                        nameof(SearchPersonsBy), SearchBy, PersonParamter);
                    throw;
                }
            }
        }
    }
}