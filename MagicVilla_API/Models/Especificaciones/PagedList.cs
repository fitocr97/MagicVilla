namespace MagicVilla_API.Models.Especificaciones
{
    //encargada de los cortes y saltos
    public class PagedList<T> : List<T> //clase generica que acepta cualquier tipo de modelo y hereda de una lista
    {
        public MetaData MetaData { get; set; }

        public PagedList(List<T> items, int count, int pageNumber, int pageSize) 
        {
            MetaData = new MetaData //incializamos parametros de metadata
            {
                TotalCount = count,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)  // 1.5 lo transforma en 2
            };
            AddRange(items);
        }

        //recibe lista generica, 
        public static PagedList<T> ToPagedList(IEnumerable<T> entidad, int pageNumber, int pageSize) 
        {
            var count = entidad.Count();
            var items = entidad.Skip((pageNumber - 1) * pageSize) //salta
                               .Take(pageSize).ToList(); //toma

            return new PagedList<T>(items, count, pageNumber, pageSize); //lista ya cortada
        }
    }
}
