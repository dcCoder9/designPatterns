using System;
using System.Net.Cache;
using System.Runtime.CompilerServices;
using Core;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var h = new StandardHat().WithPropertiesOf(new OldHat().WithPropertiesOf(new NewHat()))
                .WithPropertiesOf(new PremiumHat());
            h.GetName().Dump();
            h.GetPrice().Dump();

            var hatOrDog = new Or<Hat, Dog>(h);
            var s = hatOrDog.Do(x => x.Age).Do(age => Console.WriteLine(age + 12));
            Console.WriteLine(s);

            hatOrDog.Extract(out var first, out var second);
        }
    }

    public class Dog
    {
        public int Age { get; set; }

        public void Bark()
        {
            Console.WriteLine("woof");
        }
    }

    public abstract class Hat
    {
        protected Maybe<Hat> hat { get; set; } = Maybe<Hat>.None();
        public abstract string GetName();
        public abstract decimal GetPrice();


        private Action<Hat> SetProperty;

        protected Hat()
        {
            void SetDecoratorsProperty(Hat h) => this.hat.Do(x => x.SetProperty(h));

            void SetOwnProperty(Hat h)
            {
                this.hat = Maybe<Hat>.Some(h);
                this.SetProperty = SetDecoratorsProperty;
            }

            this.SetProperty = SetOwnProperty;
        }

        public Hat WithPropertiesOf(Hat h)
        {
            SetProperty(h);
            return this;
        }
    }

    public class NewHat : Hat
    {
        public override string GetName()
        {
            return "New hat" + this.hat.Do(h => h.GetName()).GetValueOrDefault();
        }

        public override decimal GetPrice()
        {
            return 5 + this.hat.Do(h => h.GetPrice()).GetValueOrDefault();
        }
    }

    public class OldHat : Hat
    {
        public override string GetName()
        {
            return "Old hat" + this.hat.Do(h => h.GetName()).GetValueOrDefault();
        }

        public override decimal GetPrice()
        {
            return 5 + this.hat.Do(h => h.GetPrice()).GetValueOrDefault();
        }
    }

    public class StandardHat : Hat
    {
        public override string GetName()
        {
            return "Standard hat" + this.hat.Do(h => h.GetName()).GetValueOrDefault();
        }

        public override decimal GetPrice()
        {
            return 10 + this.hat.Do(h => h.GetPrice()).GetValueOrDefault();
        }
    }

    public class PremiumHat : Hat
    {
        public override string GetName()
        {
            return "Premium hat" + this.hat.Do(h => h.GetName()).GetValueOrDefault();
        }

        public override decimal GetPrice()
        {
            return 20 + this.hat.Do(h => h.GetPrice()).GetValueOrDefault();
        }
    }
}