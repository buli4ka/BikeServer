using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarShop.Models;

namespace CarShop.Repositories.Interfaces
{
    public interface ICarRepository
    {
        public List<Car> GetAll();
        public Task<Car> GetById(Guid id);
        
        public Car Create(Car model);
        public Car Update(Car model);
        public void Delete(Guid id);
    }
}