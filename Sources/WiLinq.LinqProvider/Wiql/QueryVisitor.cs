using System;

namespace WiLinq.LinqProvider.Wiql
{
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
            var fieldOperationStatement = whereStatement as FieldOperationStatement;

            if (fieldOperationStatement != null)
            {
                return VisitFieldOperationStatement(fieldOperationStatement);
            }

            var booleanOperationStatement = whereStatement as BooleanOperationStatement;


            if (booleanOperationStatement != null)
            {
                return VisitBooleanOperationStatement(booleanOperationStatement);
            }

            throw new InvalidOperationException("Unknown wherestatement");
        }

        private BooleanOperationStatement VisitBooleanOperationStatement(BooleanOperationStatement booleanOperationStatement)
        {
            var left = VisitWhereStatements(booleanOperationStatement.Left);
            var right = VisitWhereStatements(booleanOperationStatement.Right);

            return new BooleanOperationStatement(left, booleanOperationStatement.Type, right);
        }

        protected virtual FieldOperationStatement VisitFieldOperationStatement(FieldOperationStatement fieldOperationStatement)
        {
           return new FieldOperationStatement(fieldOperationStatement.Field,fieldOperationStatement.Type,fieldOperationStatement.Value.Copy(),fieldOperationStatement.FieldType);
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
}
