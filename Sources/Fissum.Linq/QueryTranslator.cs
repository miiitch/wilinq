using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using WiLinq.LinqProvider.Extensions;

namespace WiLinq.LinqProvider
{
    /// <summary>
    /// Translate the original expression tree into a WIQL request.
    /// </summary>
    internal class QueryTranslator : ExpressionVisitor
    {

        /// <summary>
        /// Object containing the query being built.
        /// </summary>
        private QueryBuilder _builder;
        private ILinqResolver _resolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryTranslator"/> class.
        /// </summary>
        internal QueryTranslator(ILinqResolver resolver)
        {
            _resolver = resolver;
        }

        /// <summary>
        /// Translates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        internal QueryBuilder Translate(Expression expression)
        {
            _builder = new QueryBuilder(QueryType.WorkItem);
            
            this.Visit(expression);

            return _builder;
        }

        /// <summary>
        /// Strips the quotes.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        private static Expression StripQuotes(Expression e)
        {

            while (e.NodeType == ExpressionType.Quote)
            {

                e = ((UnaryExpression)e).Operand;

            }

            return e;
        }



        /// <summary>
        /// Visits the method call.
        /// </summary>
        /// <param name="m">The m.</param>
        /// <returns></returns>
        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            
            if (m.Method.DeclaringType == typeof(Queryable) && m.Method.Name == "Where")
            {
                Visit(m.Arguments[0]);
                LambdaExpression lambda = (LambdaExpression)StripQuotes(m.Arguments[1]);

                ProcessWhereClause(lambda);

                return m;
            }
            else if (m.Method.DeclaringType == typeof(Queryable) && m.Method.Name == "Select")
            {
                Visit(m.Arguments[0]);
                LambdaExpression lambda = (LambdaExpression)StripQuotes(m.Arguments[1]);
                if (_builder.SelectLambda != null)
                {
                    throw new InvalidOperationException("Cannot support two select clause");
                }
                _builder.SelectLambda = lambda;
                _builder.SelectMethod = m.Method;
                return m;
            }
            else if (m.Method.DeclaringType == typeof(Queryable) && m.Method.Name == "ThenBy")
            {
                Visit(m.Arguments[0]);
                LambdaExpression lambda = (LambdaExpression)StripQuotes(m.Arguments[1]);
                ProcessOrderClause(lambda, true);
                return m;

            }
            else if (m.Method.DeclaringType == typeof(Queryable) && m.Method.Name == "ThenByDescending")
            {
                Visit(m.Arguments[0]);
                LambdaExpression lambda = (LambdaExpression)StripQuotes(m.Arguments[1]);
                ProcessOrderClause(lambda, false);
                return m;
            }
            else if (m.Method.DeclaringType == typeof(Queryable) && m.Method.Name == "OrderBy")
            {
                
                LambdaExpression lambda = (LambdaExpression)StripQuotes(m.Arguments[1]);
                ProcessOrderClause(lambda, true);
                Visit(m.Arguments[0]);
                return m;

            }
            else if (m.Method.DeclaringType == typeof(Queryable) && m.Method.Name == "OrderByDescending")
            {                
                LambdaExpression lambda = (LambdaExpression)StripQuotes(m.Arguments[1]);
                ProcessOrderClause(lambda, false);
                Visit(m.Arguments[0]);
                return m;
            }
            else if (m.Method.DeclaringType == typeof(Queryable) && m.Method.Name == "OfType")
            {
                Visit(m.Arguments[0]);
                return m;
            }
            throw new NotSupportedException(string.Format("The method '{0}' is not supported", m.Method.Name));
        }

        /// <summary>
        /// Determines whether type is a work item or not.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// 	<c>true</c> if type is a work item
        /// </returns>
        private bool IsAWorkItem(Type type)
        {
            if (type == typeof(WorkItem) || type == typeof(WorkItemBase))
            {
                return true;
            }

            if (type.BaseType != null)
            {
                return IsAWorkItem(type.BaseType);
            }

            return false;
        }

        /// <summary>
        /// Processes the where clause.
        /// </summary>
        /// <param name="lambda">The lambda.</param>
        private void ProcessWhereClause(LambdaExpression lambda)
        {
            if ((lambda.Parameters.Count != 1) ||
               (!(lambda.Parameters[0] is ParameterExpression)) ||
               (!IsAWorkItem(lambda.Parameters[0].Type)))
            {
                throw new InvalidOperationException("invalid where clase");
            }

            WhereClauseTranslator whereTranslator = new WhereClauseTranslator(_resolver,null);

            string whereClause = whereTranslator.Translate(lambda.Body, _builder,lambda.Parameters[0].Name);
            _builder.AddWhereClause(whereClause);
        }

        /// <summary>
        /// Processes the order clause.
        /// </summary>
        /// <param name="lambda">The lambda.</param>
        /// <param name="isAscending">if set to <c>true</c> [is ascending].</param>
        private void ProcessOrderClause(LambdaExpression lambda, bool isAscending)
        {            
            if ((lambda.Parameters.Count != 1) ||
                (!(lambda.Parameters[0] is ParameterExpression)) ||
                (!IsAWorkItem(lambda.Parameters[0].Type)))
            {
                throw new InvalidOperationException("invalid order clase");
            }

            Expression body = lambda.Body;
            MemberExpression me = body as MemberExpression;            
            if (me != null)
            {
                var fieldInfo = _resolver.Resolve(me.Member);
                if (fieldInfo == null)
                {
                    throw new InvalidOperationException("invalid order field");
                }
                _builder.AddOrderClause(fieldInfo.Name, isAscending);
                return;
            }

            MethodCallExpression mc = body as MethodCallExpression;
            if (mc != null)
            {
                var fieldInfo = _resolver.Resolve(lambda.Parameters[0].Name, mc);
                if (fieldInfo == null)
                {
                    throw new InvalidOperationException("invalid order field");
                }
                _builder.AddOrderClause(fieldInfo.Name, isAscending);
                             
            }
        }
    }
}