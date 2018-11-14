using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Pattern
{
    public interface IGestionServices<T> where T : class
    {
        void Ajouter(T entity);
        void Supprimer(T entity);
        void MisAjour(T entity);
        //  IEnumerable<T> GetAll(); si en passe parmetre null en getbycondition elle va retern get all
        T RetournerById(int id);
        T RetournerById(string id);
        //Expression c'est le type del lamda
        IEnumerable<T> RetournerByCondition(Expression<Func<T, bool>> condition = null, Expression<Func<T, bool>> orderBy = null);
        void Supprimer(Expression<Func<T, bool>> condition);
        void Commit();
    }
}
