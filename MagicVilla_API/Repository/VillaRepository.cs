using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Repository.IRepository;

namespace MagicVilla_API.Repository
{
    public class VillaRepository : Repository<Villa>, IVillaRepository //acepta cualquier tipo de entidad le decimos que tipo Villa, y que herede de la interfaz
    {
        private readonly ApplicationDbContext _db;

        public VillaRepository(ApplicationDbContext db) : base(db) //como ya esta en la interfaz se lo pasamos al padre con el base
        {
            _db = db;
        }

        public async Task<Villa> Update(Villa entidad)
        {
            entidad.FechaActualizacio = DateTime.Now;
            _db.Villas.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
