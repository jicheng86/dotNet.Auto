using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Auto.Model.Extend
{
    /// <summary>
    /// Lambda Expression 合并扩展类
    /// </summary>
    public static class ExpressionWhereExtend
    {
        /// <summary>
        /// 合并两个lmabda Expression
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first">第一个lmabda Expression</param>
        /// <param name="second">第二个lmabda Expression</param>
        /// <param name="mergeMethod">合并方法</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Compose<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second, Func<Expression, Expression, BinaryExpression> mergeMethod)
        {
            var parameter = Expression.Parameter(typeof(T));
            var firstVistor = new ParameterComposeVisitor(first.Parameters[0], parameter);
            Expression firstExp = firstVistor.Visit(first.Body);
            var rightVisitor = new ParameterComposeVisitor(second.Parameters[0], parameter);//不是用.Parameters[0]结果是不对的
            Expression secondeExp = rightVisitor.Visit(second.Body);

            return Expression.Lambda<Func<T, bool>>(mergeMethod(firstExp, secondeExp), parameter);
        }
        /// <summary>
        /// （扩展）新增一个Expression AND 运算
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original">初始的Expression</param>
        /// <param name="objectives">添加的目标Expression</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> original, Expression<Func<T, bool>> objectives)
        {
            return original.Compose<T>(objectives, Expression.AndAlso);
        }
        /// <summary>
        ///（扩展）合并一个Expression OR 运算
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original">初始的Expression</param>
        /// <param name="objectives">添加的目标Expression</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> original, Expression<Func<T, bool>> objectives)
        {
            return original.Compose<T>(objectives, mergeMethod: Expression.OrElse);
        }
    }
    /// <summary>
    /// Parameter Compose Visitor
    /// </summary>
    public class ParameterComposeVisitor : ExpressionVisitor
    {
        private readonly Expression OldExpression;
        private readonly Expression NewExpression;

        public ParameterComposeVisitor(Expression oldExpression, Expression newExpression)
        {
            OldExpression = oldExpression ?? throw new ArgumentNullException(nameof(oldExpression));
            NewExpression = newExpression ?? throw new ArgumentNullException(nameof(newExpression));
        }

        public override Expression Visit(Expression expression)
        {
            if (expression == OldExpression)
            {
                return NewExpression;
            }
            return base.Visit(expression);
        }
    }

}
