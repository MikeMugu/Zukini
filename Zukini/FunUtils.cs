using System;
using System.Linq;

namespace Zukini
{
    public class FunUtils
    {
        /// <summary>
        /// Allows to concatenate functions. Probably should live outside.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="f1">Function 1</param>
        /// <param name="f2">Function 2</param>
        /// <returns>Function that is the result of two</returns>
        public static Func<A, C> Compose<A, B, C>(Func<A, B> f1, Func<B, C> f2)
        {
            return (a) => f2(f1(a));
        }

        /// <summary>
        /// Combines array of predicates to a common one, using OR logic.
        /// </summary>
        /// <typeparam name="TX"></typeparam>
        /// <param name="predicates"></param>
        /// <returns></returns>
        public static Predicate<TX> Or<TX>(params Predicate<TX>[] predicates) => 
            item => predicates.Any(predicate => predicate(item));

        /// <summary>
        /// Combines array of predicates to a common one, using AND logic.
        /// </summary>
        /// <typeparam name="TX"></typeparam>
        /// <param name="predicates"></param>
        /// <returns></returns>
        public static Predicate<TX> And<TX>(params Predicate<TX>[] predicates) => 
            item => predicates.All(predicate => predicate(item));
    }
}
