using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using WiLinq.LinqProvider.Extensions;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace WiLinq.LinqProvider
{
	/// <summary>
	/// Visitor used to translate the Where expression into the Where part of the WIQL
	/// </summary>
	internal class WhereClauseTranslator : System.Linq.Expressions.ExpressionVisitor
	{

		private StringBuilder _builder;
		private string _parameterName;
		private string _prefix;
		private QueryBuilder _queryBuilder;
		private Stack<WhereLocation> _locationStack;
		private Stack<bool> _notBlockStack;
		private ILinqResolver _resolver;


		private bool IsInNotBlock
		{
			get
			{
				return _notBlockStack.Peek();
			}
		}

		private WhereLocation CurrentLocation
		{
			get
			{
				if (_locationStack.Count == 0)
				{
					return WhereLocation.ElseWhere;
				}
				return _locationStack.Peek();
			}
		}

		private void PushLocation(WhereLocation location)
		{
			_locationStack.Push(location);
		}

		private void PopLocation()
		{
			_locationStack.Pop();
		}

		private void PushNot()
		{
			_notBlockStack.Push(!IsInNotBlock);
		}

		private void PopNot()
		{
			_notBlockStack.Pop();
		}

		internal WhereClauseTranslator(ILinqResolver resolver, string prefix)
		{
			_locationStack = new Stack<WhereLocation>();
			_notBlockStack = new Stack<bool>();
			_resolver = resolver;
			_prefix = prefix;
		}

		internal bool IsWhereParameter(Expression expression)
		{
			var pe = expression as ParameterExpression;
			if (pe == null)
			{
				return false;
			}

			return pe.Name == _parameterName;
		}

		internal string Translate(Expression expression, QueryBuilder queryBuilder, string parameterName)
		{

			expression = Evaluator.PartialEval(expression);

			
			if (IsTrueConstant(expression))
			{
				return string.Empty;
			}


			_builder = new StringBuilder();
			_queryBuilder = queryBuilder;
			_parameterName = parameterName;
			_notBlockStack.Push(false); //initialize the not stack

			this.Visit(expression);

			return _builder.ToString();
		}

		private static bool IsTrueConstant(Expression expression)
		{
			var ce = expression as ConstantExpression;
			return ce != null 
				&& ce.Type == typeof(bool)
				&& (bool)ce.Value;
		}

#if false

		private static Expression StripQuotes(Expression e)
		{

			while (e.NodeType == ExpressionType.Quote)
			{

				e = ((UnaryExpression)e).Operand;

			}
			return e;
		}
#endif

		protected override Expression VisitUnary(UnaryExpression u)
		{
			if (u.NodeType == ExpressionType.Not)
			{
				PushNot();
				Visit(u.Operand);
				PopNot();
				return u;
			}
			else
			{
				throw new NotSupportedException(string.Format("The unary operator '{0}' is not supported", u.NodeType));
			}
		}

		protected override Expression VisitParameter(ParameterExpression p)
		{
			return base.VisitParameter(p);
		}

		protected override Expression VisitBinary(BinaryExpression b)
		{
			string binaryOperator;
			bool propagateNotStatus = false;
			bool isNot = IsInNotBlock;
			bool isFieldOperator = false;
			_builder.Append("(");


			switch (b.NodeType)
			{

				case ExpressionType.And:
				case ExpressionType.AndAlso:
					propagateNotStatus = true;
					binaryOperator = !isNot ? "AND" : "OR";
					break;

				case ExpressionType.Or:
				case ExpressionType.OrElse:
					propagateNotStatus = true;
					binaryOperator = !isNot ? "OR" : "AND";

					break;

				case ExpressionType.Equal:
					binaryOperator = !isNot ? "=" : "<>";
					isFieldOperator = true;
					break;

				case ExpressionType.NotEqual:
					binaryOperator = !isNot ? "<>" : "=";
					isFieldOperator = true;
					break;

				case ExpressionType.LessThan:
					binaryOperator = !isNot ? "<" : ">=";
					isFieldOperator = true;
					break;

				case ExpressionType.LessThanOrEqual:
					binaryOperator = !isNot ? "<=" : ">";
					isFieldOperator = true;
					break;

				case ExpressionType.GreaterThan:
					binaryOperator = !isNot ? ">" : "<=";
					isFieldOperator = true;
					break;

				case ExpressionType.GreaterThanOrEqual:
					binaryOperator = !isNot ? ">=" : "<";
					isFieldOperator = true;
					break;

				default:
					throw new NotSupportedException(string.Format("The binary operator '{0}' is not supported", b.NodeType));
			}


			if (isNot && !propagateNotStatus)
			{
				PushNot();
			}

			PushLocation(isFieldOperator?WhereLocation.LeftOperatorClause:WhereLocation.ElseWhere);
			this.Visit(b.Left);
			PopLocation();

			_builder.Append(" ");
			_builder.Append(binaryOperator);
			_builder.Append(" ");

			PushLocation(isFieldOperator ? WhereLocation.RightOperatorClause : WhereLocation.ElseWhere);
			this.Visit(b.Right);
			PopLocation();


			if (isNot && !propagateNotStatus)
			{
				PopNot();
			}
			_builder.Append(")");

			return b;

		}

		/// <summary>
		/// Translates a constant into a WIQL parameter
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

				if (converter == null)
				{
					value = c.Value;
				}
				else
				{
					value = converter.Resolve(c.Value);
				}
			}



			if (IsInNotBlock)
			{
				if (value is bool)
				{
					value = !((bool)value);
				}
				else
				{
					throw new NotSupportedException("'Not' statement not supported on a non-boolean constant");
				}
			}

			string macro = _queryBuilder.GenerateMacro(value);

			_builder.Append(macro);

			return c;
		}       

		protected override Expression VisitMember(MemberExpression m)
		{
			ParameterExpression param = m.Expression as ParameterExpression;

			if (IsInNotBlock)
			{
				throw new NotSupportedException("Not statement not supported");
			}

			if ((param != null) && (param.Name == _parameterName))
			{
				if (CurrentLocation != WhereLocation.LeftOperatorClause)
				{
					throw new NotSupportedException("Member position not supported");
				}

				if (_resolver == null)
				{
					throw new NotSupportedException("Cannont resolve field");
				}

				var field = _resolver.Resolve(m.Member);
				if (field == null)
				{
					throw new NotSupportedException("Non-mapped field not supported");
				}
				if (_prefix != null)
				{
					_builder.AppendFormat("[{0}].[{0}]",_prefix,field.Name);
				}
				else
				{
					_builder.Append(field.Name);
				}
				return m;
			}

			if (m.Member.DeclaringType == typeof(QueryConstant))
			{
				if (CurrentLocation != WhereLocation.RightOperatorClause)
				{
					throw new NotSupportedException("Member position not supported");
				}

				_builder.Append("@" + m.Member.Name);
				return m;
			}

			throw new NotSupportedException(string.Format("The member '{0}' is not supported", m.Member.Name));
		}

		private void BuildOperationFromMethodCall(MethodCallExpression m, MemberExpression me, string op, bool isFieldOperator)
		{
			
			
			PushLocation(isFieldOperator?WhereLocation.LeftOperatorClause:WhereLocation.ElseWhere);
			bool isNot = IsInNotBlock;
			if (isNot)
			{
				PushNot();
			}
			Visit(me);
			if (isNot)
			{
				PopNot();
			}
			PopLocation();
			_builder.Append(String.Format(" {0} ", op));
			if (isNot)
			{
				PushNot();
			}
			PushLocation(isFieldOperator?WhereLocation.RightOperatorClause:WhereLocation.ElseWhere);
			Visit(m.Arguments[0]);
			PopLocation();
			if (isNot)
			{
				PopNot();
			}
		}
		protected override Expression VisitMethodCall(MethodCallExpression m)
		{

			if (CurrentLocation == WhereLocation.LeftOperatorClause)
			{
				var fieldInfo = _resolver.Resolve(_parameterName, m);
				if (fieldInfo != null)
				{
					if (_prefix != null)
					{
						_builder.AppendFormat("[{0}].[{0}]", _prefix, fieldInfo.Name);
					}
					else
					{
						_builder.Append(fieldInfo.Name);
					}                  
				}
				else
				{
					throw new NotSupportedException("Invalid expression");
				}

			}          
			else
			{


				bool handled = false;
				int argCount = m.Arguments.Count;
		   
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
						Type wiType = genericParameters[0];
						if (wiType == typeof(WorkItem))
						{
							throw new InvalidOperationException("Invalid usage of 'Is': it cannot be used with the class Fissum.TeamSystem.WorkItem");
						}
						else if (wiType.IsSubclassOf(typeof(WorkItem)))
						{

							var wiPropUtilityType = typeof(WorkItemPropertyUtility<>).MakeGenericType(wiType);

							PropertyInfo wiTypeNamePropInfo = wiPropUtilityType.GetProperty("WorkItemTypeName", BindingFlags.Static | BindingFlags.Public);

							string wiTypeName = wiTypeNamePropInfo.GetValue(null, null) as string;

							string op = !IsInNotBlock ? "==" : "<>";

							string test = String.Format("{0} {1} {2}", SystemField.WorkItemType, op, _queryBuilder.GenerateMacro(wiTypeName));

							_builder.Append(test);
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
					MemberExpression me = m.Object as MemberExpression;


					if (me != null && IsWhereParameter(me.Expression))
					{
						//processing "wi.op(arg)" pattern
						if (me.Type == typeof(string))
						{
							if (m.Method.Name == "Contains" && argCount == 1)
							{
								string op = null;
								if (IsInNotBlock)
								{
									op = "not contains";
								}
								else
								{
									op = "contains";
								}
								BuildOperationFromMethodCall(m, me, op,true);
								handled = true;
							}
						}
						else if (me.Type == typeof(Node))
						{
							if (m.Method.Name == "IsUnder" && argCount == 1)
							{
								string op = null;
								if (IsInNotBlock)
								{
									op = "not under";
								}
								else
								{
									op = "under";
								}
								BuildOperationFromMethodCall(m, me, op,true);
								handled = true;
							}
						}
					}
					else
					{
						var call = m as MethodCallExpression;
						if (call != null && IsWhereParameter(call.Object))
						{
							var arguments = call.Arguments;

							var resolved = _resolver.Resolve(call, IsInNotBlock);

							if (resolved != null)
							{
								handled = true;


								string test = String.Format("[{0}] {1} {2}", resolved.Item1,  resolved.Item2, _queryBuilder.GenerateMacro(resolved.Item3));

								_builder.Append(test);
							}

						}
					}

					
					
				}
				if (!handled)
				{
					throw new NotSupportedException(String.Format("MethodCall: {0}", m.Method.Name));
				}

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