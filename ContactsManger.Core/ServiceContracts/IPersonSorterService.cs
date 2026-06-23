using ServiceContracts.DTOs;
using ServiceContracts.DTOs.Enums;

namespace ServiceContracts
{
    public interface IPersonSorterService
    {
        Task<List<PersonRespones>> getPersonsSorted(
            List<PersonRespones> persons,
            string? sortBy,
            sortedListOp sortOrder
        );
    }
}