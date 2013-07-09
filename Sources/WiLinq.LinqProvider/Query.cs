using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace WiLinq.LinqProvider
{
    internal class Query<T> : IQueryable<T>, IQueryable, IEnumerable<T>, IEnumerable, IOrderedQueryable<T>, IOrderedQueryable       
    {
        IWorkItemLinqQueryProvider _provider;
        Expression _expression;
        DateTime? _asOfDate;

        public Query(IWorkItemLinqQueryProvider provider)
        {

            if (provider == null)
            {

                throw new ArgumentNullException("provider");

            }

            this._provider = provider;

            this._expression = Expression.Constant(this);
        }

        public Query(IWorkItemLinqQueryProvider provider, Expression expression)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }            

            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

      
            if (!typeof(IQueryable<T>).IsAssignableFrom(expression.Type))
            {
                throw new ArgumentOutOfRangeException("expression");
            }

            this._provider = provider;
            this._expression = expression;

        }



        Expression IQueryable.Expression
        {

            get { return this._expression; }

        }



        Type IQueryable.ElementType
        {

            get { return typeof(T); }

        }



        IQueryProvider IQueryable.Provider
        {

            get { return this._provider; }

        }



        public IEnumerator<T> GetEnumerator()
        {

            return ((IEnumerable<T>)this._provider.Execute(this._expression)).GetEnumerator();

        }



        IEnumerator IEnumerable.GetEnumerator()
        {

            return ((IEnumerable)this._provider.Execute(this._expression)).GetEnumerator();

        }



        public override string ToString()
        {

            return this._provider.GetQueryText(this._expression);

        }

        public static TPCQuery TransformAsWorkItemQuery(IQueryable<T> query)
        {
            Query<T> wiQuery = query as Query<T>;
            if (wiQuery == null)
            {
                return null;
            }

            return wiQuery._provider.TransformAsWorkItemQuery(query.Expression);            
        }


        public Query<T> AsOf(DateTime dt)
        {

            if (_asOfDate.HasValue)
            {
                throw new InvalidOperationException("AsOf Date already defined");                

            }
            _provider.AsOfDate = dt;

            _asOfDate = dt;
            return this;

        }

    }
}
