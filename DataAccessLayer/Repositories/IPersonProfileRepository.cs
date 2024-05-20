namespace DataAccessLayer.Models
{
    public interface IPersonProfileRepository
    {
        Task<PersonProfile> GetPersonProfile(int personId); 
    }
}
