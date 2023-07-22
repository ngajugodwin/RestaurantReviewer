﻿using IRestaurantReviewer_API.Domain.Repositories;
using IRestaurantReviewer_API.Persistence.Contexts;

namespace IRestaurantReviewer_API.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
