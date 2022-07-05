﻿using HotelListing.Data;
using HotelListing.IRepository;

namespace HotelListing.Repository
{
    public class UnitOfWork : IUnitWork
    {
        private readonly DatabaseContext _dbContext;

        private IGenericRepository<Country> _countries;
        private IGenericRepository<Hotel> _hotels;

        public UnitOfWork(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IGenericRepository<Country> Countries => _countries ??= new GenericRepository<Country>(_dbContext);

        public IGenericRepository<Hotel> Hotels => _hotels ??= new GenericRepository<Hotel>(_dbContext);

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
