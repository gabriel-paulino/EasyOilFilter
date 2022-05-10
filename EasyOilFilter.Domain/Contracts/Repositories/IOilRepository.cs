﻿using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;

namespace EasyOilFilter.Domain.Contracts.Repositories
{
    public interface IOilRepository
    {
        Task<IEnumerable<Oil>> Get(int page, int quantity);
        Task<Oil> Get(Guid id);
        Task<IEnumerable<Oil>> GetByName(string name);
        Task<IEnumerable<Oil>> GetByViscosity(string viscosity);
        Task<IEnumerable<Oil>> Get(OilType type);

        Task<bool> Create(Oil oil);
        Task<bool> Update(Oil oil);
        Task<bool> Delete(Guid id);
    }
}
