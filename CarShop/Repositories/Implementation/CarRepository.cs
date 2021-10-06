using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Database;
using CarShop.Models;
using CarShop.Models.Base;
using CarShop.Models.CarAttributes;
using CarShop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarShop.Repositories.Implementation
{
    public class CarRepository : ICarRepository
    {
        private ShopContext Context { get; set; }

        public CarRepository(ShopContext context)
        {
            Context = context;
        }

        public List<Car> GetAll()
        {
            return Context.Set<Car>().ToList();
        }

        public Task<Car> GetById(Guid id)
        {
            return Context.Cars
                .OrderBy(i => i.Id == id)
                .Include(e => e.Engine)
                .Include(e => e.Manufacturer)
                .Include(e => e.Transmission)
                .Include(e => e.Comfort)
                .Include(e => e.Securities)
                .Include(e => e.WheelDrive)
                .Include(e => e.Images)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public Car Create(Car requestCar)
        {
            var car = new Car
            {
                Name = requestCar.Name,
                Description = requestCar.Description,
                Price = requestCar.Price,
                ManufacturerId = requestCar.Manufacturer.Id,
                EngineId = requestCar.Engine.Id,
                WheelDriveId = requestCar.WheelDrive.Id,
                TransmissionId = requestCar.Transmission.Id,
                Comfort = requestCar.Comfort.Select(i => Context.Set<Comfort>().FirstOrDefault(m => m.Id == i.Id)).ToList(),
                Securities = requestCar.Securities.Select(i => Context.Set<Security>().FirstOrDefault(m => m.Id == i.Id)).ToList(),
            };
            Context.Set<Car>().Add(car);
            Context.SaveChanges();
            return car;
        }

        public Car Update(Car model)
        {
            var toUpdate = Context.Set<Car>().FirstOrDefault(m => m.Id == model.Id);
            if (toUpdate != null)
            {
                toUpdate = model;
            }

            Context.Update(toUpdate);
            Context.SaveChanges();
            return toUpdate;
        }

        public void Delete(Guid id)
        {
            var toDelete = Context.Set<Car>().FirstOrDefault(m => m.Id == id);
            Context.Set<Car>().Remove(toDelete);
            Context.SaveChanges();
        }
    }
}