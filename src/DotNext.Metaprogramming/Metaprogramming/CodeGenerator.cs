using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Diagnostics;

namespace DotNext.Metaprogramming
{
    using Linq.Expressions;

    /// <summary>
    /// Represents code generator.
    /// </summary>
    public static class CodeGenerator
    {
        private static void Place<D, S>(this S statement, D scope)
            where D : MulticastDelegate
            where S : Statement, ILexicalScope<Expression, D>
            => LexicalScope.Current.AddStatement(statement.Build(scope));

        /// <summary>
        /// Obtains local variable declared in the current or outer lexical scope.
        /// </summary>
        /// <param name="name">The name of the local variable.</param>
        /// <returns>Declared local variable; or <see langword="null"/>, if there is no declared local variable with the given name.</returns>
        public static ParameterExpression Variable(string name) => LexicalScope.Current[name];

        /// <summary>
        /// Adds no-operation instruction to this scope.
        /// </summary>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void Nop() => LexicalScope.Current.AddStatement(Expression.Empty());

        /// <summary>
        /// Installs breakpoint.
        /// </summary>
        /// <remarks>
        /// This method installs breakpoint in DEBUG configuration.
        /// </remarks>
        [Conditional("DEBUG")]
        public static void Breakpoint() => LexicalScope.Current.AddStatement(ExpressionBuilder.Breakpoint());

        [Conditional("DEBUG")]
        public static void Assert(Expression test, string message = null) 
            => LexicalScope.Current.AddStatement(test.Assert(message));

        /// <summary>
        /// Adds assignment operation to this scope.
        /// </summary>
        /// <param name="variable">The variable to modify.</param>
        /// <param name="value">The value to be assigned to the variable.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void Assign(ParameterExpression variable, Expression value) 
            => LexicalScope.Current.AddStatement(variable.Assign(value));

        /// <summary>
        /// Adds assignment operation to this scope.
        /// </summary>
        /// <param name="indexer">The indexer property or array element to modify.</param>
        /// <param name="value">The value to be assigned to the member or array element.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void Assign(IndexExpression indexer, Expression value) => LexicalScope.Current.AddStatement(indexer.Assign(value));

        /// <summary>
        /// Adds assignment operation to this scope.
        /// </summary>
        /// <param name="member">The field or property to modify.</param>
        /// <param name="value">The value to be assigned to the member.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void Assign(MemberExpression member, Expression value) => LexicalScope.Current.AddStatement(member.Assign(value));

        /// <summary>
        /// Adds an expression that increments given variable by 1 and assigns the result back to the variable.
        /// </summary>
        /// <param name="variable">The variable to be modified.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void PreIncrementAssign(ParameterExpression variable) => LexicalScope.Current.AddStatement(variable.PreIncrementAssign());

        /// <summary>
        /// Adds an expression that represents the assignment of given variable followed by a subsequent increment by 1 of the original variable.
        /// </summary>
        /// <param name="variable">The variable to be modified.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void PostIncrementAssign(ParameterExpression variable) => LexicalScope.Current.AddStatement(variable.PostIncrementAssign());

        /// <summary>
        /// Adds an expression that decrements given variable by 1 and assigns the result back to the variable.
        /// </summary>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void PreDecrementAssign(ParameterExpression variable) => LexicalScope.Current.AddStatement(variable.PreDecrementAssign());

        /// <summary>
        /// Adds an expression that represents the assignment of given variable followed by a subsequent decrement by 1 of the original variable.
        /// </summary>
        /// <param name="variable">The variable to be modified.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void PostDecrementAssign(ParameterExpression variable) => LexicalScope.Current.AddStatement(variable.PostDecrementAssign());

        /// <summary>
        /// Adds an expression that increments given field or property by 1 and assigns the result back to the member.
        /// </summary>
        /// <param name="member">The member to be modified.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void PreIncrementAssign(MemberExpression member) => LexicalScope.Current.AddStatement(member.PreIncrementAssign());

        /// <summary>
        /// Adds an expression that represents the assignment of given field or property followed by a subsequent increment by 1 of the original member.
        /// </summary>
        /// <param name="member">The member to be modified.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void PostIncrementAssign(MemberExpression member) => LexicalScope.Current.AddStatement(member.PostIncrementAssign());

        /// <summary>
        /// Adds an expression that decrements given field or property by 1 and assigns the result back to the member.
        /// </summary>
        /// <param name="member">The member to be modified.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void PreDecrementAssign(MemberExpression member) => LexicalScope.Current.AddStatement(member.PreDecrementAssign());

        /// <summary>
        /// Adds an expression that represents the assignment of given field or property followed by a subsequent decrement by 1 of the original member.
        /// </summary>
        /// <param name="member">The member to be modified.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void PostDecrementAssign(MemberExpression member) => LexicalScope.Current.AddStatement(member.PostDecrementAssign());

        /// <summary>
        /// Adds an expression that increments given field or property by 1 and assigns the result back to the member.
        /// </summary>
        /// <param name="member">The member to be modified.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void PreIncrementAssign(IndexExpression member) => LexicalScope.Current.AddStatement(member.PreIncrementAssign());

        /// <summary>
        /// Adds an expression that represents the assignment of given field or property followed by a subsequent increment by 1 of the original member.
        /// </summary>
        /// <param name="member">The member to be modified.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void PostIncrementAssign(IndexExpression member) => LexicalScope.Current.AddStatement(member.PostIncrementAssign());

        /// <summary>
        /// Adds an expression that decrements given field or property by 1 and assigns the result back to the member.
        /// </summary>
        /// <param name="member">The member to be modified.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void PreDecrementAssign(IndexExpression member) => LexicalScope.Current.AddStatement(member.PreDecrementAssign());

        /// <summary>
        /// Adds an expression that represents the assignment of given field or property followed by a subsequent decrement by 1 of the original member.
        /// </summary>
        /// <param name="member">The member to be modified.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void PostDecrementAssign(IndexExpression member) => LexicalScope.Current.AddStatement(member.PostDecrementAssign());

        /// <summary>
        /// Adds constant as in-place statement.
        /// </summary>
        /// <typeparam name="T">The type of the constant.</typeparam>
        /// <param name="value">The value to be placed as statement.</param>
        public static void InPlaceValue<T>(T value) => LexicalScope.Current.AddStatement(value.Const());

        /// <summary>
        /// Adds local variable assignment operation this scope.
        /// </summary>
        /// <param name="variableName">The name of the declared local variable.</param>
        /// <param name="value">The value to be assigned to the local variable.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void Assign(string variableName, Expression value) => Assign(Variable(variableName), value);

        /// <summary>
        /// Adds instance property assignment.
        /// </summary>
        /// <param name="instance"><see langword="this"/> argument.</param>
        /// <param name="instanceProperty">Instance property to be assigned.</param>
        /// <param name="value">A new value of the property.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void Assign(Expression instance, PropertyInfo instanceProperty, Expression value)
            => LexicalScope.Current.AddStatement(Expression.Assign(Expression.Property(instance, instanceProperty), value));

        /// <summary>
        /// Adds static property assignment.
        /// </summary>
        /// <param name="staticProperty">Static property to be assigned.</param>
        /// <param name="value">A new value of the property.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void Assign(PropertyInfo staticProperty, Expression value)
            => Assign(null, staticProperty, value);

        /// <summary>
        /// Adds instance field assignment.
        /// </summary>
        /// <param name="instance"><see langword="this"/> argument.</param>
        /// <param name="instanceField">Instance field to be assigned.</param>
        /// <param name="value">A new value of the field.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void Assign(Expression instance, FieldInfo instanceField, Expression value)
            => LexicalScope.Current.AddStatement(Expression.Assign(Expression.Field(instance, instanceField), value));

        /// <summary>
        /// Adds static field assignment.
        /// </summary>
        /// <param name="staticField">Static field to be assigned.</param>
        /// <param name="value">A new value of the field.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void Assign(FieldInfo staticField, Expression value)
            => Assign(null, staticField, value);

        /// <summary>
        /// Adds invocation statement.
        /// </summary>
        /// <param name="delegate">The expression providing delegate to be invoked.</param>
        /// <param name="arguments">Delegate invocation arguments.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void Invoke(Expression @delegate, IEnumerable<Expression> arguments) => LexicalScope.Current.AddStatement(Expression.Invoke(@delegate, arguments));

        /// <summary>
        /// Adds invocation statement.
        /// </summary>
        /// <param name="delegate">The expression providing delegate to be invoked.</param>
        /// <param name="arguments">Delegate invocation arguments.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void Invoke(Expression @delegate, params Expression[] arguments) => Invoke(@delegate, (IEnumerable<Expression>)arguments);

        /// <summary>
        /// Adds instance method call statement.
        /// </summary>
        /// <param name="instance"><see langword="this"/> argument.</param>
        /// <param name="method">The method to be called.</param>
        /// <param name="arguments">Method call arguments.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void Call(Expression instance, MethodInfo method, IEnumerable<Expression> arguments)
            => LexicalScope.Current.AddStatement(Expression.Call(instance, method, arguments));

        /// <summary>
        /// Adds instance method call statement.
        /// </summary>
        /// <param name="instance"><see langword="this"/> argument.</param>
        /// <param name="method">The method to be called.</param>
        /// <param name="arguments">Method call arguments.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void Call(Expression instance, MethodInfo method, params Expression[] arguments)
            => Call(instance, method, (IEnumerable<Expression>)arguments);

        /// <summary>
        /// Adds instance method call statement.
        /// </summary>
        /// <param name="instance"><see langword="this"/> argument.</param>
        /// <param name="methodName">The method to be called.</param>
        /// <param name="arguments">Method call arguments.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void Call(Expression instance, string methodName, params Expression[] arguments)
            => LexicalScope.Current.AddStatement(instance.Call(methodName, arguments));

        /// <summary>
        /// Adds static method call statement.,
        /// </summary>
        /// <param name="method">The method to be called.</param>
        /// <param name="arguments">Method call arguments.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void Call(MethodInfo method, IEnumerable<Expression> arguments)
            => LexicalScope.Current.AddStatement(Expression.Call(null, method, arguments));

        /// <summary>
        /// Adds static method call statement.
        /// </summary>
        /// <param name="method">The method to be called.</param>
        /// <param name="arguments">Method call arguments.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void Call(MethodInfo method, params Expression[] arguments)
            => Call(method, (IEnumerable<Expression>)arguments);

        /// <summary>
        /// Adds static method call.
        /// </summary>
        /// <param name="type">The type that declares static method.</param>
        /// <param name="methodName">The name of the static method.</param>
        /// <param name="arguments">The arguments to be passed into static method.</param>
        /// <returns>An expression representing static method call.</returns>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void CallStatic(Type type, string methodName, params Expression[] arguments)
            => LexicalScope.Current.AddStatement(type.CallStatic(methodName, arguments));

        /// <summary>
        /// Constructs static method call.
        /// </summary>
        /// <typeparam name="T">The type that declares static method.</typeparam>
        /// <param name="methodName">The name of the static method.</param>
        /// <param name="arguments">The arguments to be passed into static method.</param>
        /// <returns>An expression representing static method call.</returns>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void CallStatic<T>(string methodName, params Expression[] arguments)
            => CallStatic(typeof(T), methodName, arguments);

        /// <summary>
        /// Declares label of the specified type.
        /// </summary>
        /// <param name="type">The type of landing site.</param>
        /// <param name="name">The optional name of the label.</param>
        /// <returns>Declared label.</returns>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static LabelTarget Label(Type type, string name = null)
        {
            var target = Expression.Label(type, name);
            Label(target);
            return target;
        }

        /// <summary>
        /// Declares label of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of landing site.</typeparam>
        /// <param name="name">The optional name of the label.</param>
        /// <returns>Declared label.</returns>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static LabelTarget Label<T>(string name = null) => Label(typeof(T), name);

        /// <summary>
        /// Declares label in the current scope.
        /// </summary>
        /// <returns>Declared label.</returns>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static LabelTarget Label() => Label(typeof(void));

        /// <summary>
        /// Adds label landing site to this scope.
        /// </summary>
        /// <param name="target">The label target.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void Label(LabelTarget target) => LexicalScope.Current.AddStatement(Expression.Label(target));

        private static void Goto(LabelTarget target, Expression value, GotoExpressionKind kind)
            => LexicalScope.Current.AddStatement(Expression.MakeGoto(kind, target, value, value?.Type ?? typeof(void)));

        /// <summary>
        /// Adds unconditional control transfer statement to this scope.
        /// </summary>
        /// <param name="target">The label reference.</param>
        /// <param name="value">The value to be associated with the control transfer.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void Goto(LabelTarget target, Expression value) => Goto(target, value, GotoExpressionKind.Goto);

        /// <summary>
        /// Adds unconditional control transfer statement to this scope.
        /// </summary>
        /// <param name="target">The label reference.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void Goto(LabelTarget target) => Goto(target, null);

        /// <summary>
        /// Declares local variable in the current lexical scope.
        /// </summary>
        /// <typeparam name="T">The type of local variable.</typeparam>
        /// <param name="name">The name of local variable.</param>
        /// <returns>The expression representing local variable.</returns>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static ParameterExpression DeclareVariable<T>(string name) => DeclareVariable(typeof(T), name);

        /// <summary>
        /// Declares local variable in the current lexical scope. 
        /// </summary>
        /// <param name="variableType">The type of local variable.</param>
        /// <param name="name">The name of local variable.</param>
        /// <returns>The expression representing local variable.</returns>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static ParameterExpression DeclareVariable(Type variableType, string name)
        {
            var variable = Expression.Variable(variableType, name);
            LexicalScope.Current.DeclareVariable(variable);
            return variable;
        }

        /// <summary>
        /// Declares initialized local variable of automatically
        /// inferred type.
        /// </summary>
        /// <remarks>
        /// The equivalent code is <c>var i = expr;</c>
        /// </remarks>
        /// <param name="name">The name of the variable.</param>
        /// <param name="init">Initialization expression.</param>
        /// <returns>The expression representing local variable.</returns>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static ParameterExpression DeclareVariable(string name, Expression init)
        {
            var variable = DeclareVariable(init.Type, name);
            Assign(variable, init);
            return variable;
        }

        /// <summary>
        /// Adds await operator.
        /// </summary>
        /// <param name="asyncResult">The expression representing asynchronous computing process.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void Await(Expression asyncResult) => LexicalScope.Current.AddStatement(asyncResult.Await());

        /// <summary>
        /// Adds if-then-else statement to this scope.
        /// </summary>
        /// <param name="test">Test expression.</param>
        /// <returns>Conditional statement builder.</returns>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static ConditionalBuilder If(Expression test) => new ConditionalBuilder(test, LexicalScope.Current);

        /// <summary>
        /// Constructs positive branch of the conditional expression.
        /// </summary>
        /// <param name="builder">Conditional statement builder.</param>
        /// <param name="body">Branch builder.</param>
        /// <returns>Conditional expression builder.</returns>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static ConditionalBuilder Then(this ConditionalBuilder builder, Action body)
        {
            using(var statement = BranchStatement.Positive(builder))
                return statement.Build(body);
        }

        /// <summary>
        /// Constructs negative branch of the conditional expression.
        /// </summary>
        /// <param name="builder">Conditional statement builder.</param>
        /// <param name="body">Branch builder.</param>
        /// <returns>Conditional expression builder.</returns>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static ConditionalBuilder Else(this ConditionalBuilder builder, Action body)
        {
            using(var statement = BranchStatement.Negative(builder))
                return statement.Build(body);
        }

        /// <summary>
        /// Adds if-then statement to this scope.
        /// </summary>
        /// <param name="test">Test expression.</param>
        /// <param name="ifTrue">Positive branch builder.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void IfThen(Expression test, Action ifTrue)
            => If(test).Then(ifTrue).End();

        /// <summary>
        /// Adds if-then-else statement to this scope.
        /// </summary>
        /// <param name="test">Test expression.</param>
        /// <param name="ifTrue">Positive branch builder.</param>
        /// <param name="ifFalse">Negative branch builder.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void IfThenElse(Expression test, Action ifTrue, Action ifFalse)
            => If(test).Then(ifTrue).Else(ifFalse).End();

        /// <summary>
        /// Adds <see langword="while"/> loop statement.
        /// </summary>
        /// <param name="test">Loop continuation condition.</param>
        /// <param name="body">Loop body.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        /// <seealso href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/while">while Statement</seealso>
        public static void While(Expression test, Action<LoopContext> body)
        {
            using(var statement = WhileStatement.While(test))
                statement.Place(body);
        }

        /// <summary>
        /// Adds <see langword="while"/> loop statement.
        /// </summary>
        /// <param name="test">Loop continuation condition.</param>
        /// <param name="body">Loop body.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        /// <seealso href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/while">while Statement</seealso>
        public static void While(Expression test, Action body)
        {
            using(var statement = WhileStatement.While(test))
                statement.Place(body);
        }

        /// <summary>
        /// Adds <c>do{ } while(condition);</c> loop statement.
        /// </summary>
        /// <param name="test">Loop continuation condition.</param>
        /// <param name="body">Loop body.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        /// <seealso href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/do">do-while Statement</seealso>
        public static void DoWhile(Expression test, Action<LoopContext> body)
        {
            using(var statement = WhileStatement.Until(test))
                statement.Place(body);
        }

        /// <summary>
        /// Adds <c>do{ } while(condition);</c> loop statement.
        /// </summary>
        /// <param name="test">Loop continuation condition.</param>
        /// <param name="body">Loop body.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        /// <seealso href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/do">do-while Statement</seealso>
        public static void DoWhile(Expression test, Action body)
        {
            using(var statement = WhileStatement.Until(test))
                statement.Place(body);
        }

        /// <summary>
        /// Adds <see langword="foreach"/> loop statement.
        /// </summary>
        /// <param name="collection">The expression providing enumerable collection.</param>
        /// <param name="body">Loop body.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        /// <seealso href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/foreach-in">foreach Statement</seealso>
        public static void ForEach(Expression collection, Action<MemberExpression, LoopContext> body)
        {
            using(var statement = new ForEachStatement(collection))
                statement.Place(body);
        }

        /// <summary>
        /// Adds <see langword="foreach"/> loop statement.
        /// </summary>
        /// <param name="collection">The expression providing enumerable collection.</param>
        /// <param name="body">Loop body.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        /// <seealso href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/foreach-in">foreach Statement</seealso>
        public static void ForEach(Expression collection, Action<MemberExpression> body)
        {
            using(var statement = new ForEachStatement(collection))
                statement.Place(body);
        }

        /// <summary>
        /// Adds <see langword="for"/> loop statement.
        /// </summary>
        /// <remarks>
        /// This builder constructs the statement equivalent to <c>for(var i = initializer; condition; iter){ body; }</c>
        /// </remarks>
        /// <param name="initializer">Loop variable initialization expression.</param>
        /// <param name="condition">Loop continuation condition.</param>
        /// <param name="iteration">Iteration statements.</param>
        /// <param name="body">Loop body.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        /// <seealso href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/for">for Statement</seealso>
        public static void For(Expression initializer, ForExpression.LoopBuilder.Condition condition, Action<ParameterExpression> iteration, Action<ParameterExpression, LoopContext> body)
        {
            using(var statement = new ForStatement(initializer, condition, iteration))
                statement.Place(body);
        }

        /// <summary>
        /// Adds <see langword="for"/> loop statement.
        /// </summary>
        /// <remarks>
        /// This builder constructs the statement equivalent to <c>for(var i = initializer; condition; iter){ body; }</c>
        /// </remarks>
        /// <param name="initializer">Loop variable initialization expression.</param>
        /// <param name="condition">Loop continuation condition.</param>
        /// <param name="iteration">Iteration statements.</param>
        /// <param name="body">Loop body.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        /// <seealso href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/for">for Statement</seealso>
        public static void For(Expression initializer, ForExpression.LoopBuilder.Condition condition, Action<ParameterExpression> iteration, Action<ParameterExpression> body)
        {
            using(var statement = new ForStatement(initializer, condition, iteration))
                statement.Place(body);
        }

        /// <summary>
        /// Adds generic loop statement.
        /// </summary>
        /// <remarks>
        /// This loop is equvalent to <c>while(true){ }</c>
        /// </remarks>
        /// <param name="body">Loop body.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void Loop(Action<LoopContext> body) 
        {
            using(var statement = new LoopStatement())
                statement.Place(body);
        }

        /// <summary>
        /// Adds generic loop statement.
        /// </summary>
        /// <param name="body">Loop body.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void Loop(Action body) 
        {
            using(var statement = new LoopStatement())
                statement.Place(body);
        }

        /// <summary>
        /// Adds <see langword="throw"/> statement to the compound statement.
        /// </summary>
        /// <param name="exception">The exception to be thrown.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void Throw(Expression exception) => LexicalScope.Current.AddStatement(Expression.Throw(exception));

        /// <summary>
        /// Adds <see langword="throw"/> statement to the compound statement.
        /// </summary>
        /// <typeparam name="E">The exception to be thrown.</typeparam>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void Throw<E>() where E : Exception, new() => Throw(Expression.New(typeof(E).GetConstructor(Array.Empty<Type>())));

        /// <summary>
        /// Adds re-throw statement.
        /// </summary>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of catch clause.</exception>
        public static void Rethrow()
        {
            if(LexicalScope.IsInScope<CatchStatement>())
                LexicalScope.Current.AddStatement(Expression.Rethrow());
            else
                throw new InvalidOperationException(ExceptionMessages.InvalidRethrow);
                
        }

        /// <summary>
        /// Adds <see langword="using"/> statement.
        /// </summary>
        /// <param name="resource">The expression representing disposable resource.</param>
        /// <param name="body">The body of the statement.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        /// <seealso href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/using-statement">using Statement</seealso>
        public static void Using(Expression resource, Action<ParameterExpression> body)
        {
            using(var statement = new UsingStatement(resource))
                statement.Place(body);
        }

        /// <summary>
        /// Adds <see langword="using"/> statement.
        /// </summary>
        /// <param name="resource">The expression representing disposable resource.</param>
        /// <param name="body">The body of the statement.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        /// <seealso href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/using-statement">using Statement</seealso>
        public static void Using(Expression resource, Action body)
        {
            using(var statement = new UsingStatement(resource))
                statement.Place(body);
        }

        /// <summary>
        /// Adds <see langword="lock"/> statement.
        /// </summary>
        /// <param name="syncRoot">The object to be locked during execution of the compound statement.</param>
        /// <param name="body">Synchronized scope of code.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        /// <seealso href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/lock-statement">lock Statement</seealso>
        public static void Lock(Expression syncRoot, Action<ParameterExpression> body)
        {
            using(var statement = new LockStatement(syncRoot))
                statement.Place(body);
        }

        /// <summary>
        /// Adds <see langword="lock"/> statement.
        /// </summary>
        /// <param name="syncRoot">The object to be locked during execution of the compound statement.</param>
        /// <param name="body">Synchronized scope of code.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        /// <seealso href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/lock-statement">lock Statement</seealso>
        public static void Lock(Expression syncRoot, Action body)
        {
            using(var statement = new LockStatement(syncRoot))
                statement.Place(body);
        }

        /// <summary>
        /// Adds compound statement hat repeatedly refer to a single object or 
        /// structure so that the statements can use a simplified syntax when accessing members 
        /// of the object or structure.
        /// </summary>
        /// <param name="expression">The implicitly referenced object.</param>
        /// <param name="body">The statement body.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        /// <seealso href="https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/statements/with-end-with-statement">With..End Statement</seealso>
        public static void With(Expression expression, Action<ParameterExpression> body)
        {
            using(var statement = new WithStatement(expression))
                statement.Place(body);
        }

        /// <summary>
        /// Adds selection expression.
        /// </summary>
        /// <param name="value">The value to be handled by the selection expression.</param>
        /// <returns>A new instance of selection expression builder.</returns>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        /// <seealso href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/switch">switch Statement</seealso>
        public static SwitchBuilder Switch(Expression value) => new SwitchBuilder(value, LexicalScope.Current);

        /// <summary>
        /// Specifies a pattern to compare to the match expression
        /// and action to be executed if matching is successful.
        /// </summary>
        /// <param name="builder">Selection builder.</param>
        /// <param name="testValues">A list of test values.</param>
        /// <param name="body">The block code to be executed if input value is equal to one of test values.</param>
        /// <returns>Modified selection builder.</returns>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static SwitchBuilder Case(this SwitchBuilder builder, IEnumerable<Expression> testValues, Action body)
        {
            using(var statement = new CaseStatement(builder, testValues))
                return statement.Build(body);
        }


        /// <summary>
        /// Specifies a pattern to compare to the match expression
        /// and action to be executed if matching is successful.
        /// </summary>
        /// <param name="builder">Selection builder.</param>
        /// <param name="test">Single test value.</param>
        /// <param name="body">The block code to be executed if input value is equal to one of test values.</param>
        /// <returns>Modified selection builder.</returns>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static SwitchBuilder Case(this SwitchBuilder builder, Expression test, Action body)
            => Case(builder, Sequence.Singleton(test), body);

        /// <summary>
        /// Specifies the switch section to execute if the match expression
        /// doesn't match any other cases.
        /// </summary>
        /// <param name="builder">Selection builder.</param>
        /// <param name="body">The block code to be executed if input value is equal to one of test values.</param>
        /// <returns>Modified selection builder.</returns>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static SwitchBuilder Default(this SwitchBuilder builder, Action body)
        {
            using(var statement = new DefaultStatement(builder))
                return statement.Build(body);
        }

        /// <summary>
        /// Constructs exception handling section.
        /// </summary>
        /// <param name="builder">Structured exception handling builder.</param>
        /// <param name="exceptionType">Expected exception.</param>
        /// <param name="filter">Additional filter to be applied to the caught exception.</param>
        /// <param name="handler">Exception handling block.</param>
        /// <returns>Structured exception handling builder.</returns>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static TryBuilder Catch(this TryBuilder builder, Type exceptionType, TryBuilder.Filter filter, Action<ParameterExpression> handler)
        {
            using(var statement = new CatchStatement(builder, exceptionType, filter))
                return statement.Build(handler);
        }

        /// <summary>
        /// Constructs exception handling section.
        /// </summary>
        /// <param name="builder">Structured exception handling builder.</param>
        /// <param name="exceptionType">Expected exception.</param>
        /// <param name="handler">Exception handling block.</param>
        /// <returns>Structured exception handling builder.</returns>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static TryBuilder Catch(this TryBuilder builder, Type exceptionType, Action handler)
        {
            using(var statement = new CatchStatement(builder, exceptionType))
                return statement.Build(handler);
        }
        
        /// <summary>
        /// Constructs exception handling section.
        /// </summary>
        /// <param name="builder">Structured exception handling builder.</param>
        /// <param name="exceptionType">Expected exception.</param>
        /// <param name="handler">Exception handling block.</param>
        /// <returns>Structured exception handling builder.</returns>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static TryBuilder Catch(this TryBuilder builder, Type exceptionType, Action<ParameterExpression> handler)
            => Catch(builder, exceptionType, null, handler);

        /// <summary>
        /// Constructs exception handling section.
        /// </summary>
        /// <typeparam name="E">Expected exception.</typeparam>
        /// <param name="builder">Structured exception handling builder.</param>
        /// <param name="handler">Exception handling block.</param>
        /// <returns>Structured exception handling builder.</returns>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static TryBuilder Catch<E>(this TryBuilder builder, Action<ParameterExpression> handler)
            where E : Exception
            => Catch(builder, typeof(E), handler);
        
        /// <summary>
        /// Constructs exception handling section.
        /// </summary>
        /// <typeparam name="E">Expected exception.</typeparam>
        /// <param name="builder">Structured exception handling builder.</param>
        /// <param name="handler">Exception handling block.</param>
        /// <returns>Structured exception handling builder.</returns>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static TryBuilder Catch<E>(this TryBuilder builder, Action handler)
            where E : Exception
            => Catch(builder, typeof(E), handler);

        /// <summary>
        /// Constructs exception handling section that may capture any exception.
        /// </summary>
        /// <param name="builder">Structured exception handling builder.</param>
        /// <param name="handler">Exception handling block.</param>
        /// <returns>Structured exception handling builder.</returns>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static TryBuilder Catch(this TryBuilder builder, Action handler)
        {
            using(var statement = new CatchStatement(builder))
                return statement.Build(handler);
        }

        /// <summary>
        /// Constructs block of code which will be executed in case
        /// of any exception.
        /// </summary>
        /// <param name="builder">Structured exception handling builder.</param>
        /// <param name="fault">Fault handling block.</param>
        /// <returns><see langword="this"/> builder.</returns>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static TryBuilder Fault(this TryBuilder builder, Action fault)
        {
            using(var statement = new FaultStatement(builder))
                return statement.Build(fault);
        }

        /// <summary>
        /// Adds structured exception handling statement.
        /// </summary>
        /// <param name="scope"><see langword="try"/> block builder.</param>
        /// <returns>Structured exception handling builder.</returns>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        /// <seealso href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/try-catch-finally">try-catch-finally Statement</seealso>
        public static TryBuilder Try(Action scope) 
            => InitStatement<TryBuilder, Action, TryStatement, LexicalScope.IFactory<TryStatement>>(TryStatement.Factory, scope);

        /// <summary>
        /// Adds structured exception handling statement.
        /// </summary>
        /// <param name="body"><see langword="try"/> block.</param>
        /// <returns>Structured exception handling builder.</returns>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        /// <seealso href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/try-catch-finally">try-catch-finally Statement</seealso>        
        public static TryBuilder Try(Expression body) => new TryBuilder(body, CurrentScope);

        /// <summary>
        /// Constructs block of code run when control leaves a <see langword="try"/> statement.
        /// </summary>
        /// <param name="builder">Structured exception handling builder.</param>
        /// <param name="body">The block of code to be executed.</param>
        /// <returns><see langword="this"/> builder.</returns>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static TryBuilder Finally(this TryBuilder builder, Action body) 
            => InitStatement<TryBuilder, Action, FinallyStatement, FinallyStatement.Factory>(new FinallyStatement.Factory(builder), body); 

        /// <summary>
        /// Restarts execution of the loop.
        /// </summary>
        /// <param name="loop">The loop reference.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void Continue(LoopContext loop) => Goto(loop.ContinueLabel, null, GotoExpressionKind.Continue);

        /// <summary>
        /// Restarts execution of the entire loop.
        /// </summary>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void Continue()
        {
            var loop = FindScope<LoopLexicalScope>() ?? throw new InvalidOperationException(ExceptionMessages.LoopNotAvailable);
            Continue(new LoopContext(loop));
        }

        /// <summary>
        /// Stops execution the specified loop.
        /// </summary>
        /// <param name="loop">The loop reference.</param>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void Break(LoopContext loop) => Goto(loop.BreakLabel, null, GotoExpressionKind.Break);

        /// <summary>
        /// Stops execution of the entire loop.
        /// </summary>
        /// <exception cref="InvalidOperationException">Attempts to call this method out of lexical scope.</exception>
        public static void Break()
        {
            var loop = FindScope<LoopLexicalScope>() ?? throw new InvalidOperationException(ExceptionMessages.LoopNotAvailable);
            Break(new LoopContext(loop));
        }

        /// <summary>
        /// Adds <see langword="return"/> instruction to return from
        /// underlying lambda function having non-<see langword="void"/>
        /// return type.
        /// </summary>
        /// <param name="result">Optional value to be returned from the lambda function.</param>
        /// <exception cref="InvalidOperationException">This method is not called from within body of lambda function.</exception>
        public static void Return(Expression result = null)
        {
            var lambda = FindScope<LambdaExpression>() ?? throw new InvalidOperationException(ExceptionMessages.OutOfLexicalScope);
            CurrentScope.AddStatement(lambda.Return(result));
        }

        /// <summary>
        /// Constructs lamdba function capturing the current lexical scope.
        /// </summary>
        /// <typeparam name="D">The delegate describing signature of lambda function.</typeparam>
        /// <param name="tailCall"><see langword="true"/> if the lambda expression will be compiled with the tail call optimization, otherwise <see langword="false"/>.</param>
        /// <param name="body">Lambda function builder.</param>
        /// <returns>Constructed lambda expression.</returns>
        public static Expression<D> Lambda<D>(bool tailCall, Action<LambdaContext> body)
            where D : Delegate
            => InitStatement<Expression<D>, Action<LambdaContext>, LambdaExpression<D>, LambdaExpression<D>.Factory>(new LambdaExpression<D>.Factory(tailCall), body);

        /// <summary>
        /// Constructs lamdba function capturing the current lexical scope.
        /// </summary>
        /// <typeparam name="D">The delegate describing signature of lambda function.</typeparam>
        /// <param name="tailCall"><see langword="true"/> if the lambda expression will be compiled with the tail call optimization, otherwise <see langword="false"/>.</param>
        /// <param name="body">Lambda function builder.</param>
        /// <returns>Constructed lambda expression.</returns>
        public static Expression<D> Lambda<D>(bool tailCall, Action<LambdaContext, ParameterExpression> body)
            where D : Delegate
            => InitStatement<Expression<D>, Action<LambdaContext, ParameterExpression>, LambdaExpression<D>, LambdaExpression<D>.Factory>(new LambdaExpression<D>.Factory(tailCall), body);

        /// <summary>
        /// Constructs lamdba function capturing the current lexical scope.
        /// </summary>
        /// <typeparam name="D">The delegate describing signature of lambda function.</typeparam>
        /// <param name="body">Lambda function builder.</param>
        /// <returns>Constructed lambda expression.</returns>
        public static Expression<D> Lambda<D>(Action<LambdaContext> body)
            where D : Delegate
            => Lambda<D>(false, body);

        /// <summary>
        /// Constructs lamdba function capturing the current lexical scope.
        /// </summary>
        /// <typeparam name="D">The delegate describing signature of lambda function.</typeparam>
        /// <param name="body">Lambda function builder.</param>
        /// <returns>Constructed lambda expression.</returns>
        public static Expression<D> Lambda<D>(Action<LambdaContext, ParameterExpression> body)
            where D : Delegate
            => Lambda<D>(false, body);

        /// <summary>
        /// Constructs async lambda function capturing the current lexical scope.
        /// </summary>
        /// <typeparam name="D">The delegate describing signature of lambda function.</typeparam>
        /// <param name="body">Lambda function builder.</param>
        /// <returns>Constructed lambda expression.</returns>
        /// <seealso cref="AwaitExpression"/>
        /// <seealso cref="AsyncResultExpression"/>
        /// <seealso href="https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/#BKMK_HowtoWriteanAsyncMethod">Async methods</seealso>
        public static Expression<D> AsyncLambda<D>(Action<LambdaContext> body)
            where D : Delegate
            => InitStatement<Expression<D>, Action<LambdaContext>, AsyncLambdaExpression<D>, LexicalScope.IFactory<AsyncLambdaExpression<D>>>(AsyncLambdaExpression<D>.Factory, body);
    }
}