using System;
using System.Runtime.InteropServices.ComTypes;

namespace ConsoleApp1
{
    public class Or<T,U>
    {
        private Maybe<T> TElem { get; set; } = Maybe<T>.None();    
        private Maybe<U> UElem { get; set; } = Maybe<U>.None();

        public Or(T elem)
        {
            this.TElem = Maybe<T>.Some(elem);
        }

        public Or(U elem)
        {
            this.UElem = Maybe<U>.Some(elem);
        }

        public Or<T,U> Do(Action<T> func)
        {
            TElem.Do(func);
            return this;
        }
        public Or<T,U> Do(Action<U> func)
        {
            UElem.Do(func);
            return this;
        }
        public Or<T,Z> Do<Z>(Func<U,Z> func)
        {
            var zElem = UElem.Do(func);
            return new Or<T, Z>(zElem.GetValueOrDefault());
        }

        public void Extract(out T t, out U u)
        {
            t = TElem.GetValueOrDefault();
            u = UElem.GetValueOrDefault();
        }
    }
}