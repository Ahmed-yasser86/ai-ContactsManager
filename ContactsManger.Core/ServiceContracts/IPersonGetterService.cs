using ServiceContracts.DTOs;

namespace ServiceContracts
{
    public interface IPersonGetterService
    {
        Task<List<PersonRespones>> GetAllPersons();

        Task<PersonRespones?> GetPersonByPersonId(Guid? personId);
    }
}