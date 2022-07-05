using HotelListing.Data;

namespace HotelListing.IRepository
{
    // Register for every variation of GenericRepository
    public interface IUnitWork : IDisposable
    {
        IGenericRepository<Country> Countries { get; }
        IGenericRepository<Hotel> Hotels { get; }

        Task Save();
    }
}
