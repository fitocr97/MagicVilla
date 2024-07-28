using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Repository.IRepository;

namespace MagicVilla_API.Repository
{
    public class NumeroVillaRepository : Repository<NumeroVilla>, INumeroVillaRepository //acepta cualquier tipo de entidad le decimos que tipo Villa, y que herede de la interfaz
    {
        private readonly ApplicationDbContext _db;

        public NumeroVillaRepository(ApplicationDbContext db) : base(db) //como ya esta en la interfaz se lo pasamos al padre con el base
        {
            _db = db;
        }

        public async Task<NumeroVilla> Update(NumeroVilla entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.NumeroVillas.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
