using backend.Data;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
  
    public class d 
    {
        private readonly DataContext _context;
        public d(DataContext context)
        {
            _context = context;
            
        }

        public bool CabinExist(int id)
        {
            return _context.CabinCrew.Any(p => p.id == id);
        }

        public bool cabinUserExist(int id)
        {
            throw new NotImplementedException();
        }

        public bool CreateCabin(Cabincrew cabin)
        {
            _context.Add(cabin);

            return Save();

        }

        public bool DeleteCabin(Cabincrew cabin)
        {
            _context.Remove(cabin);
            return Save();
        }

        public Cabincrew GetCabin(int id)
        {
            return _context.CabinCrew.Where(air => air.id == id).Include(air => air.User).FirstOrDefault();

        }

        public ICollection<Cabincrew> GetCabinByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public ICollection<Flight> GetCabinByflight(int cabinId)
        {
           throw new NotImplementedException();
        }

        public Cabincrew GetCabinByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        /*   public Aircraft GetAircraft(string name)
       {
           return _context.Aircrafts.Where(air => air.AirCraftModel == name).FirstOrDefault();
       }*/

        /*  public Aircraft GetAircrafts()
 {
     return context.Aircrafts.OrderBy(p => p.id).FirstOrDefault();
 }*/

        public ICollection<Cabincrew> GetCabins()
        {
            return _context.CabinCrew.Include(air => air.User).ToList();    //toList is to get multiple data 

        }

        public ICollection<Cabincrew> GetCabinsHome()
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCabin(Cabincrew cabin)
        {
            _context.Update(cabin);
            return Save();
        }
/*
        public Cabincrew GetCain(int pokeId)
        {
            return _context.PokemonOwners.Where(p => p.Pokemon.Id == pokeId).Select(o => o.Owner).ToList();
        }
*/
    }
}
