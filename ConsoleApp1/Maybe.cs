using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    public class Maybe<T>
    {
        private IEnumerable<T> optionalElem { get; set; }

        private Maybe(T obj)
        {
            this.optionalElem = new List<T>() {obj};
        }

        private Maybe()
        {
            this.optionalElem = new List<T>() { };
        }

        public static Maybe<T> Some(T elem)
        {
            return new Maybe<T>(elem);
        }

        public static Maybe<T> None()
        {
            return new Maybe<T>();
        }

        public void Do(Action<T> func)
        {
            foreach (var elem in optionalElem)
            {
                func(elem);
            }
        }

        public Maybe<TResult> Do<TResult>(Func<T, TResult> func)
        {
            foreach (var elem in optionalElem)
            {
                var x = func(elem);
                return Maybe<TResult>.Some(x);
            }

            return Maybe<TResult>.None();
        }

        public T GetValueOrDefault()
        {
            return optionalElem.FirstOrDefault();
        }
    }
}