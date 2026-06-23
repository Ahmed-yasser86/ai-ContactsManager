using ServiceContracts.DTOs;

namespace ServiceContracts
{
    public interface IPersonSearcherService
    {
        Task<List<PersonRespones>> SearchPersonsBy(
            string? SearchBy,
            string SearchString
        );
    }
}