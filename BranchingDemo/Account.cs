using System;

namespace BranchingDemo
{
    public class Account
    {
        public decimal Balance { get; private set; }

        private IAccountState AccountState { get; set; }

        public Account(Action onUnfreeze)
        {
            this.AccountState = new NotVerified(onUnfreeze);
        }

        public void Deposit(decimal amount)
        {
            this.AccountState = this.AccountState.Deposit(() => { this.Balance += amount; });
        }

        public void Withdraw(decimal amount)
        {
            this.AccountState = this.AccountState.Withdraw(() => { this.Balance -= amount; });
        }

        public void HolderVerified()
        {
            this.AccountState = this.AccountState.HolderVerified();
        }

        public void Freeze()
        {
            this.AccountState = this.AccountState.Freeze();
        }
    }
}