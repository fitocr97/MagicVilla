﻿using MagicVilla_API.Models.Especificaciones;
using System.Linq.Expressions;

namespace MagicVilla_API.Repository.IRepository
{
    public interface IRepository<T> where T : class //interfaz generica contrato donde solo se declaran las funciones
    {
        Task Create(T entidad); //recibe entidad

        //filto (Expression<Func<T, bool>>? filtro = null, string? incluirPropiedades = null)  using System.Linq.Expressions;
        // si no se envía un filtro devolvera toda la lista
        // si se envia el filtro filtrará segun lo que se envie
        Task<List<T>> GetAll(Expression<Func<T, bool>>? filtro = null, string? incluirPropiedades = null);

        //declarar el metodo para implementarlo
        PagedList<T> GetAllPaginado(Parametros parametros, Expression<Func<T, bool>>? filtro = null, string? incluirPropiedades = null);

        //un solo registro segun la entidad , para evitar el error tracking
        Task<T> GetOne(Expression<Func<T, bool>> filtro = null, bool tracked = true, string? incluirPropiedades = null);

        Task Delete(T entidad);

        Task Save(); //saveChanges DbContext
    }
}
