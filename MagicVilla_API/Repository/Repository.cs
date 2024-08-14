using MagicVilla_API.Data;
using MagicVilla_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace MagicVilla_API.Repository
{
    public class Repository<T> : IRepository<T> where T : class //interfaz es genérica y 
    {

        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>(); //se comvierte la T en una entidad
        }


        public async Task Create(T entidad) 
        {
            await _db.AddAsync(entidad);
            await Save();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }

        //recive un filtro y retorna un registro
        public async Task<T> GetOne(Expression<Func<T, bool>> filtro = null, bool tracked = true, string? incluirPropiedades = null)
        {
            IQueryable<T> query = dbSet;
            if (!tracked) 
            {
                query = query.AsNoTracking();
            }
            if (filtro != null) //si es diferente a nill nos estan enviando una expresión linq
            {
                query = query.Where(filtro); //where siempre trabaja con expresion linq
            }

            //verifica si pide datos de otro modelo
            if (incluirPropiedades != null)  // "Villa,OtroModelo" 
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp);
                }
            }

            return await query.FirstOrDefaultAsync();
        }


        //retorna una lista
        public async Task<List<T>> GetAll(Expression<Func<T, bool>>? filtro = null, string? incluirPropiedades = null)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            //verifica si pide datos de otro modelo
            if (incluirPropiedades != null)  // "Villa,OtroModelo"
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp);
                }
            }

            return await query.ToListAsync(); //ya sea filtrada o con todos los registros
        }

        public async Task Delete(T entidad)
        {
            dbSet.Remove(entidad);
            await Save();
        }
    }
}
