using System;
using System.Runtime.CompilerServices;

namespace BranchingDemo
{
    public interface IAccountState
    {
        IAccountState Deposit(Action addToBalance);
        IAccountState Withdraw(Action subtractFromBalance);
        IAccountState Freeze();
        IAccountState HolderVerified();
        IAccountState Close();
    }

    public class NotVerified : IAccountState
    {
        public NotVerified(Action onUnFreeze)
        {
            this.OnUnfreeze = onUnFreeze;
        }

        public Action OnUnfreeze { get; set; }

        public IAccountState Deposit(Action addToBalance)
        {
            addToBalance();
            return this;
        }

        public IAccountState Withdraw(Action subtractFromBalance) => this;
        public IAccountState Freeze() => this;
        public IAccountState HolderVerified() => new Active(this.OnUnfreeze);
        public IAccountState Close() => new Closed();
    }

    public class Closed : IAccountState
    {
        public IAccountState Deposit(Action addToBalance) => this;

        public IAccountState Withdraw(Action subtractFromBalance) => this;

        public IAccountState Freeze() => this;

        public IAccountState HolderVerified() => this;

        public IAccountState Close() => this;
    }

    public class Active : IAccountState
    {
        private Action OnUnfreeze { get; }

        public Active(Action onUnfreeze)
        {
            this.OnUnfreeze = onUnfreeze;
        }

        public IAccountState Deposit(Action addToBalance)
        {
            throw new NotImplementedException();
        }

        public IAccountState Withdraw(Action subtractFromBalance)
        {
            return this;
        }

        public IAccountState Freeze()
        {
            return new Frozen(this.OnUnfreeze);
        }

        public IAccountState HolderVerified() => this;

        public IAccountState Close() => new Closed();
    }

    public class Frozen : IAccountState
    {
        private Action OnUnfreeze { get; }

        public Frozen(Action onUnfreeze)
        {
            this.OnUnfreeze = onUnfreeze;
        }

        public IAccountState Deposit(Action addToBalance)
        {
            this.OnUnfreeze();
            addToBalance();
            return new Active(this.OnUnfreeze);
        }

        public IAccountState Withdraw(Action subtractFromBalance)
        {
            this.OnUnfreeze();
            subtractFromBalance();
            return new Active(this.OnUnfreeze);
        }

        public IAccountState Freeze()
        {
            return this;
        }

        public IAccountState HolderVerified()
        {
            throw new NotImplementedException();
        }

        public IAccountState Close()
        {
            throw new NotImplementedException();
        }
    }
}