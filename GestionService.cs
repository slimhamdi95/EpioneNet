using Epione.Data.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Pattern
{
      public class GestionService<T> :IGestionServices<T> where T:class
    {
        public static UnitOfWork unitofwork = null;
        public GestionService( UnitOfWork utw)
        {
            unitofwork = utw;
        }
        public void Ajouter(T entity)
        {
            unitofwork.getRespository<T>().Add(entity);
        }

        public void Commit()
        {
            unitofwork.Commit();
        }

        public void MisAjour(T entity)
        {
            unitofwork.getRespository<T>().Update(entity);
        }

        public IEnumerable<T> RetournerByCondition(Expression<Func<T, bool>> condition = null,Expression<Func<T, bool>> orderBy = null)
        {
           return unitofwork.getRespository<T>().GetByCondition(condition,orderBy);
        }
        public IEnumerable<T> RetournerByCondition2(Expression<Func<T, bool>> condition = null, Expression<Func<T, bool>> orderBy = null)
        {
            return unitofwork.getRespository<T>().GetByCondition(condition, orderBy);
        }

        public T RetournerById(string id)
        {
            return unitofwork.getRespository<T>().GetById(id);
        }

        public T RetournerById(int id)
        {
          return   unitofwork.getRespository<T>().GetById(id);
        }

        public void Supprimer(Expression<Func<T, bool>> condition)
        {
            unitofwork.getRespository<T>().Remove(condition);
        }

        public void Supprimer(T entity)
        {
            unitofwork.getRespository<T>().Remove(entity);
        }

        
    }
}
