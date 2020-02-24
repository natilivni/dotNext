using System;
using System.Linq.Expressions;
using System.Reflection;
using Debug = System.Diagnostics.Debug;

namespace DotNext.Linq.Expressions
{
    /// <summary>
    /// Expresses construction of <see cref="Range"/>.
    /// </summary>
    public sealed class RangeExpression : Expression
    {
        /// <summary>
        /// Initializes a new range with the specified starting and ending indexes.
        /// </summary>
        /// <param name="start">The inclusive start index of the range.</param>
        /// <param name="end">The exclusive end index of the range.</param>
        public RangeExpression(ItemIndexExpression? start = null, ItemIndexExpression? end = null)
        {
            Start = start ?? ItemIndexExpression.First;
            End = end ?? ItemIndexExpression.Last;
        }

        /// <summary>
        /// Gets the inclusive start index of the range.
        /// </summary>
        /// <value>The inclusive start index of the range.</value>
        public ItemIndexExpression Start { get; }

        /// <summary>
        /// Gets the exclusive end index of the range.
        /// </summary>
        /// <value>The end index of the range.</value>
        public ItemIndexExpression End { get; }

        /// <summary>
        /// Gets result type of asynchronous operation.
        /// </summary>
        public override Type Type => typeof(Range);

        /// <summary>
        /// Always return <see langword="true"/>.
        /// </summary>
        public override bool CanReduce => true;

        /// <summary>
        /// Gets expression node type.
        /// </summary>
        /// <see cref="ExpressionType.Extension"/>
        public override ExpressionType NodeType => ExpressionType.Extension;

        /// <summary>
        /// Translates this expression into predefined set of expressions
        /// using Lowering technique.
        /// </summary>
        /// <returns>Translated expression.</returns>
        public override Expression Reduce()
        {
            ConstructorInfo? ctor = typeof(Range).GetConstructor(new []{ typeof(Index), typeof(Index) });
            Debug.Assert(!(ctor is null));
            return New(ctor, Start, End);
        }

        /// <summary>
        /// Visit children expressions.
        /// </summary>
        /// <param name="visitor">Expression visitor.</param>
        /// <returns>Potentially modified expression if one of children expressions is modified during visit.</returns>
        protected override Expression VisitChildren(ExpressionVisitor visitor)
        {
            static ItemIndexExpression ToIndex(Expression value)
                => value is ItemIndexExpression index ? index : new ItemIndexExpression(value);

            var start = visitor.Visit(Start);
            var end = visitor.Visit(End);
            return ReferenceEquals(start, Start) && ReferenceEquals(end, End) ? this : new RangeExpression(ToIndex(start), ToIndex(end));
        }
    }
}