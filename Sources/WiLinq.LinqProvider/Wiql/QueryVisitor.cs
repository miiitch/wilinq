using System;

namespace WiLinq.LinqProvider.Wiql
{
#if false
    public class QueryVisitor
    {
        public Query Visit(Query query)
        {
            var result = new Query(query.Mode);
            foreach (var field in query.Fields)
            {
                result.Fields.Add(VisitSelectField(field));
            }


            foreach (var whereStatement in query.WhereStatements)
            {
                result.WhereStatements.Add(VisitWhereStatements(whereStatement));
            }

            foreach (var orderStatement in query.OrderStatements)
            {
                result.OrderStatements.Add(VisitOrderStatement(orderStatement));
            }


            return result;
        }

        private WhereStatement VisitWhereStatements(WhereStatement whereStatement)
        {
            switch (whereStatement)
            {
                case FieldOperationStatement fieldOperationStatement:
                    return VisitFieldOperationStatement(fieldOperationStatement);
                case BooleanOperationStatement booleanOperationStatement:
                    return VisitBooleanOperationStatement(booleanOperationStatement);
            }


            throw new InvalidOperationException("Unknown wherestatement");
        }

        private BooleanOperationStatement VisitBooleanOperationStatement(
            BooleanOperationStatement booleanOperationStatement)
        {
            var left = VisitWhereStatements(booleanOperationStatement.Left);
            var right = VisitWhereStatements(booleanOperationStatement.Right);

            return new BooleanOperationStatement(left, booleanOperationStatement.Type, right);
        }

        protected virtual FieldOperationStatement VisitFieldOperationStatement(
            FieldOperationStatement fieldOperationStatement)
        {
            return new FieldOperationStatement(fieldOperationStatement.Field, fieldOperationStatement.Type,
                fieldOperationStatement.Value.Copy(), fieldOperationStatement.FieldOrigin);
        }

        protected virtual string VisitSelectField(string field)
        {
            return field;
        }

        protected virtual OrderStatement VisitOrderStatement(OrderStatement orderStatement)
        {
            return new OrderStatement
            {
                Field = orderStatement.Field,
                IsAscending = orderStatement.IsAscending
            };
        }
    }
#endif
}