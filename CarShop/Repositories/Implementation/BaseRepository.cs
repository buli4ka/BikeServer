using System;
using System.Collections.Generic;
using System.Linq;
using CarShop.Database;
using CarShop.Models.Base;
using CarShop.Models.CarAttributes;
using CarShop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarShop.Repositories.Implementation
{
    public class BaseRepository<TDbModel> : IBaseRepository<TDbModel> where TDbModel : BaseModel
    {
        private ShopContext Context { get; set; }

        public BaseRepository(ShopContext context)
        {
            Context = context;
        }

        public TDbModel Create(TDbModel model)
        {
            Context.Set<TDbModel>().Add(model);
            Context.SaveChanges();
            return model;
        }

        public void Delete(Guid id)
        {
            var toDelete = Context.Set<TDbModel>().FirstOrDefault(m => m.Id == id);
            Context.Set<TDbModel>().Remove(toDelete);
            Context.SaveChanges();
        }

        public List<TDbModel> GetAll()
        {
            return Context.Set<TDbModel>().ToList();
        }

        public TDbModel Update(TDbModel model)
        {
            var toUpdate = Context.Set<TDbModel>().FirstOrDefault(m => m.Id == model.Id);
            if (toUpdate != null)
            {
                toUpdate = model;
            }

            Context.Update(toUpdate);
            Context.SaveChanges();
            return toUpdate;
        }

        public TDbModel GetById(Guid id)
        {
            return Context.Set<TDbModel>().FirstOrDefault(m => m.Id == id);
        }

    
       
    }
}