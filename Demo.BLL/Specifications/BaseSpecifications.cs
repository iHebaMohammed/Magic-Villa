using Demo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Specifications
{
    public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
    {



        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; }
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; }

        public BaseSpecifications(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public BaseSpecifications()
        {
        }

        public void AddOrderBy(Expression<Func<T , object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        public void AddOrderDescending(Expression<Func<T , object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }

        public void ApplyPagination(int skip , int take)
        {
            IsPagingEnabled = true;
            Take = take;
            Skip = skip;
        }
    }
}
