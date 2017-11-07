using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using WiLinq.LinqProvider.Extensions;
using WiLinq.LinqProvider.Wiql;

namespace WiLinq.LinqProvider.QueryGeneration
{
    /// <summary>
    ///     Visitor used to translate the Where expression into the Where part of the WIQL
    /// </summary>
    internal class WhereClauseTranslator : ExpressionVisitor
    {
        private Statement _statement;
        private string _parameterName;
        private readonly FieldOrigin _origin;

        private readonly Stack<WhereLocation> _locationStack;
        private readonly Stack<bool> _notBlockStack;
        private readonly ILinqResolver _resolver;


        private bool IsInNotBlock => _notBlockStack.Peek();

        private WhereLocation CurrentLocation => _locationStack.Count == 0 ? WhereLocation.BooleanOperation : _locationStack.Peek();

        private void PushLocation(WhereLocation location) => _locationStack.Push(location);

        private void PopLocation() => _locationStack.Pop();

        private void EnterNotBlock() => _notBlockStack.Push(!IsInNotBlock);

        private void LeaveNotBlock() => _notBlockStack.Pop();

        internal WhereClauseTranslator(ILinqResolver resolver, FieldOrigin origin = FieldOrigin.Default)
        {
            _locationStack = new Stack<WhereLocation>();
            _notBlockStack = new Stack<bool>();
            _resolver = resolver;
            _origin = origin;
        }

        internal bool IsWhereParameter(Expression expression)
        {
            if (expression is ParameterExpression pe)
            {
                return pe.Name == _parameterName;
            }
            return false;
        }

        internal string Translate(Expression expression, QueryBuilder queryBuilder, string parameterName)
        {
            expression = Evaluator.PartialEval(expression);

            if (IsTrueConstant(expression))
            {
                return string.Empty;
            }




            _parameterName = parameterName;
            _notBlockStack.Push(false); //initialize the not stack

            Visit(expression);
            if (!(_statement is WhereStatement whereStatement))
            {
                throw new InvalidOperationException("Invalid content");
            }

            var result = whereStatement.ConvertToQueryValue();
            return result;
        }

        private static bool IsTrueConstant(Expression expression)
        {
            return expression is ConstantExpression ce
                   && ce.Type == typeof(bool)
                   && (bool)ce.Value;
        }

        protected override Expression VisitUnary(UnaryExpression u)
        {
            switch (u.NodeType)
            {
                case ExpressionType.Not:
                    EnterNotBlock();
                    Visit(u.Operand);
                    LeaveNotBlock();
                    return u;
                default:
                    throw new NotSupportedException($"The unary operator '{u.NodeType}' is not supported");
            }
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            return base.VisitParameter(p);
        }

        protected override Expression VisitBinary(BinaryExpression b)
        {

            BooleanOperationStatementType? booleanOperator = null;
            FieldOperationStatementType? fieldOperator = null;
            var propagateNotStatus = false;
            var isNot = IsInNotBlock;


            switch (b.NodeType)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    propagateNotStatus = true;
                    booleanOperator = !isNot ? BooleanOperationStatementType.And : BooleanOperationStatementType.Or;
                    break;

                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    propagateNotStatus = true;
                    booleanOperator = !isNot ? BooleanOperationStatementType.Or : BooleanOperationStatementType.And;

                    break;

                case ExpressionType.Equal:
                    fieldOperator =
                        !isNot ? FieldOperationStatementType.Equals : FieldOperationStatementType.IsDifferent;
                    break;

                case ExpressionType.NotEqual:
                    fieldOperator =
                        !isNot ? FieldOperationStatementType.IsDifferent : FieldOperationStatementType.Equals;
                    break;

                case ExpressionType.LessThan:
                    fieldOperator = !isNot
                        ? FieldOperationStatementType.IsLess
                        : FieldOperationStatementType.IsGreaterOrEqual;
                    break;

                case ExpressionType.LessThanOrEqual:
                    fieldOperator = !isNot
                        ? FieldOperationStatementType.IsLessOrEqual
                        : FieldOperationStatementType.IsGreater;
                    break;

                case ExpressionType.GreaterThan:
                    fieldOperator = !isNot
                        ? FieldOperationStatementType.IsGreater
                        : FieldOperationStatementType.IsLessOrEqual;
                    break;

                case ExpressionType.GreaterThanOrEqual:
                    fieldOperator = !isNot
                        ? FieldOperationStatementType.IsGreaterOrEqual
                        : FieldOperationStatementType.IsLess;
                    break;

                default:
                    throw new NotSupportedException($"The binary operator '{b.NodeType}' is not supported");
            }


            if (isNot && !propagateNotStatus)
            {
                EnterNotBlock();
            }

            PushLocation(fieldOperator.HasValue ? WhereLocation.LeftOperatorClause : WhereLocation.BooleanOperation);
            Visit(b.Left);
            PopLocation();

            var leftStatement = _statement;

            PushLocation(fieldOperator.HasValue ? WhereLocation.RightOperatorClause : WhereLocation.BooleanOperation);
            Visit(b.Right);
            PopLocation();

            var rightStatement = _statement;

            if (fieldOperator.HasValue)
            {
                CreateFieldOperatorStatement(fieldOperator, leftStatement, rightStatement);
            }
            else
            {
                _statement = new BooleanOperationStatement(leftStatement as WhereStatement, booleanOperator.Value, rightStatement as WhereStatement);
            }

            if (isNot && !propagateNotStatus)
            {
                LeaveNotBlock();
            }


            return b;
        }

        private void CreateFieldOperatorStatement(FieldOperationStatementType? fieldOperator, Statement leftStatement, Statement rightStatement)
        {
            if (!(leftStatement is FieldStatement fieldStatement))
            {
                throw new InvalidOperationException($"Statement: '{leftStatement.ConvertToQueryValue()}' should be a WorkItem field");
            }

            switch (rightStatement)
            {
                case ValueStatement valueStatement:
                    _statement = new FieldOperationStatement(fieldStatement, fieldOperator.Value, valueStatement);
                    break;
                case FieldStatement rigthFieldStaement:
                    _statement = new FieldOperationStatement(fieldStatement, fieldOperator.Value, rigthFieldStaement);
                    break;
                default:

                    throw new InvalidOperationException("");
            }
        }

        /// <summary>
        ///     Translates a constant into a WIQL parameter
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns></returns>
        protected override Expression VisitConstant(ConstantExpression c)
        {
            if (CurrentLocation != WhereLocation.RightOperatorClause)
            {
                throw new NotSupportedException("constant location not supported");
            }
            object value = null;
            if (c.Value != null)
            {
                var converter = WorkItemValueConverterRepository.GetConverter(c.Value.GetType());

                value = converter == null ? c.Value : converter.Resolve(c.Value);
            }


            if (IsInNotBlock)
            {
                if (value is bool b)
                {
                    value = !b;
                }
                else
                {
                    throw new NotSupportedException("'Not' statement not supported on a non-boolean constant");
                }
            }

            if (value == null)
            {
                value = string.Empty;
            }

            _statement = CreateValueStatement(value);

            return c;
        }

        private ValueStatement CreateValueStatement(object value)
        {
            switch (value)
            {
                case long l:
                    return new IntegerValueStatement(l);

                case int i:
                    return new IntegerValueStatement(i);

                case float f:
                    return new DoubleValueStatement(f);

                case double d:
                    return new DoubleValueStatement(d);

                case DateTime dt:
                    return new DateValueStatement(dt);

                case string str:
                    return new StringValueStatement(str);

                case bool boolean:
                    return new BooleanValueStatement(boolean);

                default:
                    throw new NotSupportedException($"Not supported value type '{value.GetType()}");

            }
        }

        protected override Expression VisitMember(MemberExpression m)
        {
            var param = m.Expression as ParameterExpression;

            if (IsInNotBlock)
            {
                throw new NotSupportedException("Not statement not supported");
            }

            if (param != null && param.Name == _parameterName)
            {
                if (_resolver == null)
                {
                    throw new NotSupportedException("Cannont resolve field");
                }

                var field = _resolver.Resolve(m.Member);
                if (field == null)
                {
                    throw new NotSupportedException("Non-mapped field not supported");
                }

                _statement = new FieldStatement(field.Name, _origin);
                return m;
            }

            if (m.Member.DeclaringType == typeof(QueryConstant))
            {
                if (CurrentLocation != WhereLocation.RightOperatorClause)
                {
                    throw new NotSupportedException("Member position not supported");
                }

                _statement = new ParameterValueStatement(m.Member.Name);
                return m;
            }

            throw new NotSupportedException($"The member '{m.Member.Name}' is not supported");
        }

        private void BuildOperationFromMethodCall(MethodCallExpression m, MemberExpression me, FieldOperationStatementType fieldOperator,
            bool isFieldOperator)
        {
            PushLocation(isFieldOperator ? WhereLocation.LeftOperatorClause : WhereLocation.BooleanOperation);
            var isNot = IsInNotBlock;
            if (isNot)
            {
                EnterNotBlock();
            }
            Visit(me);
            var leftStatement = _statement;
            if (isNot)
            {
                LeaveNotBlock();
            }
            PopLocation();

            if (isNot)
            {
                EnterNotBlock();
            }
            PushLocation(isFieldOperator ? WhereLocation.RightOperatorClause : WhereLocation.BooleanOperation);
            Visit(m.Arguments[0]);
            PopLocation();

            var rightStatement = _statement;
            if (isNot)
            {
                LeaveNotBlock();
            }

            CreateFieldOperatorStatement(fieldOperator, leftStatement, rightStatement);

        }

        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            // ReSharper disable HeuristicUnreachableCode
            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            if (m == null)
            {
                return null;
            }
            // ReSharper restore ConditionIsAlwaysTrueOrFalse
            // ReSharper restore HeuristicUnreachableCode
            switch (CurrentLocation)
            {
                case WhereLocation.LeftOperatorClause:
                    {
                        var fieldInfo = _resolver.Resolve(_parameterName, m);
                        if (fieldInfo != null)
                        {
                            _statement = new FieldStatement(fieldInfo.Name, _origin);

                        }
                        else
                        {
                            throw new NotSupportedException("Invalid expression");
                        }
                    }
                    break;
                case WhereLocation.RightOperatorClause:
                    {
                        var fieldInfo = _resolver.Resolve(_parameterName, m);
                        if (fieldInfo != null)
                        {

                            _statement = new FieldStatement(fieldInfo.Name, _origin);
                        }
                    }
                    break;
                case WhereLocation.BooleanOperation:
                    {
                        var handled = false;


                        var argCount = m.Arguments.Count;

                        if (m.Object == null)
                        {
                            if (m.Method.Name == "Is" &&
                                m.Arguments.Count == 1 &&
                                IsWhereParameter(m.Arguments[0]) &&
                                m.Method.DeclaringType == typeof(QueryExtender) &&
                                m.Method.IsGenericMethod)
                            {
                                var genericParameters = m.Method.GetGenericArguments();
                                if (genericParameters.Length != 1)
                                {
                                    throw new InvalidOperationException("Invalid 'Is' operator");
                                }
                                var wiType = genericParameters[0];
                                if (wiType == typeof(WorkItem))
                                {
                                    throw new InvalidOperationException(
                                        "Invalid usage of 'Is': it cannot be used with the class WorkItem");
                                }
                                if (wiType.IsSubclassOf(typeof(GenericWorkItem)))
                                {
                                    var wiTypeName = GenericWorkItemHelpersCore.GetWorkItemType(wiType);

                                    if (string.IsNullOrWhiteSpace(wiTypeName))
                                    {
                                        throw new InvalidOperationException($"Missing workitem type for '{wiType.FullName}'");
                                    }
                                    var op = !IsInNotBlock ? FieldOperationStatementType.Equals : FieldOperationStatementType.IsDifferent;

                                    _statement = new FieldOperationStatement(
                                        new FieldStatement(SystemField.WorkItemType, _origin),
                                        op,
                                        new StringValueStatement(wiTypeName));

                                    handled = true;
                                }
                                else
                                {
                                    throw new InvalidOperationException("Invalid 'Is' operator");
                                }
                            }
                        }
                        else
                        {
                            if (m.Object is MemberExpression me && IsWhereParameter(me.Expression))
                            {
                                //processing "wi.op(arg)" pattern
                                if (me.Type == typeof(string))
                                {
                                    if (m.Method.Name == "Contains" && argCount == 1)
                                    {
                                        var op = IsInNotBlock ? FieldOperationStatementType.NotContains : FieldOperationStatementType.Contains;
                                        BuildOperationFromMethodCall(m, me, op, true);
                                        handled = true;
                                    }
                                }
                            }
                            else
                            {
                                var call = m;
                                if (IsWhereParameter(call.Object))
                                {
                                    var operation = _resolver.Resolve(call, IsInNotBlock);

                                    handled = true;

                                    var valueStatement = CreateValueStatement(operation.value);

                                    _statement = new FieldOperationStatement(new FieldStatement(operation.field.Name,_origin),operation.op,valueStatement );
                                }
                            }
                        }
                        if (!handled)
                        {
                            throw new NotSupportedException($"MethodCall: {m.Method.Name}");
                        }
                    }
                    break;
                default:
                    throw new InvalidOperationException();
            }
            return m;
        }

        #region Not Supported Expressions

        protected override MemberBinding VisitMemberBinding(MemberBinding node)
        {
            throw new NotSupportedException("MemberBinding");
        }

        protected override Expression VisitConditional(ConditionalExpression c)
        {
            throw new NotSupportedException("ConditionalExpression");
        }

        protected override Expression VisitInvocation(InvocationExpression iv)
        {
            throw new NotSupportedException("Invocation");
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            throw new NotSupportedException("Lambda");
        }

        protected override Expression VisitListInit(ListInitExpression init)
        {
            throw new NotSupportedException("ListInit");
        }

        protected override MemberAssignment VisitMemberAssignment(MemberAssignment assignment)
        {
            throw new NotSupportedException("MemberAssignment");
        }

        protected override Expression VisitMemberInit(MemberInitExpression init)
        {
            throw new NotSupportedException("MemberInit");
        }

        protected override MemberListBinding VisitMemberListBinding(MemberListBinding binding)
        {
            throw new NotSupportedException("VisitMemberListBinding");
        }

        protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding binding)
        {
            throw new NotSupportedException("VisitMemberMemberBinding");
        }

        protected override Expression VisitNewArray(NewArrayExpression na)
        {
            throw new NotSupportedException("VisitNewArray");
        }

        protected override Expression VisitBlock(BlockExpression node)
        {
            throw new NotSupportedException("VisitBlock");
        }

        protected override Expression VisitDebugInfo(DebugInfoExpression node)
        {
            throw new NotSupportedException("VisitDebugInfo");
        }

        protected override CatchBlock VisitCatchBlock(CatchBlock node)
        {
            throw new NotSupportedException("VisitCatchBlock");
        }

        protected override Expression VisitDefault(DefaultExpression node)
        {
            throw new NotSupportedException("VisitDefault");
        }

        protected override Expression VisitTypeBinary(TypeBinaryExpression node)
        {
            throw new NotSupportedException("VisitTypeBinary");
        }

        protected override ElementInit VisitElementInit(ElementInit node)
        {
            throw new NotSupportedException("VisitElementInit");
        }

        protected override Expression VisitDynamic(DynamicExpression node)
        {
            throw new NotSupportedException("VisitDynamic");
        }

        protected override Expression VisitExtension(Expression node)
        {
            throw new NotSupportedException("VisitExtension");
        }

        protected override Expression VisitGoto(GotoExpression node)
        {
            throw new NotSupportedException("VisitGoto");
        }

        protected override Expression VisitIndex(IndexExpression node)
        {
            throw new NotSupportedException("VisitIndex");
        }

        protected override Expression VisitLabel(LabelExpression node)
        {
            throw new NotSupportedException("VisitLabel");
        }

        protected override LabelTarget VisitLabelTarget(LabelTarget node)
        {
            throw new NotSupportedException("VisitLabelTarget");
        }

        protected override Expression VisitLoop(LoopExpression node)
        {
            throw new NotSupportedException("VisitLoop");
        }

        protected override Expression VisitNew(NewExpression node)
        {
            throw new NotSupportedException("VisitNew");
        }

        protected override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
        {
            throw new NotSupportedException("VisitRuntimeVariables");
        }

        protected override Expression VisitSwitch(SwitchExpression node)
        {
            throw new NotSupportedException("VisitSwitch");
        }

        protected override SwitchCase VisitSwitchCase(SwitchCase node)
        {
            throw new NotSupportedException("VisitSwitchCase");
        }

        protected override Expression VisitTry(TryExpression node)
        {
            throw new NotSupportedException("VisitTry");
        }

        #endregion
    }
}