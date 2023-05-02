public class Wallet
{
    public int MoneyAmount { get; private set; }

    public Wallet()
    {
        MoneyAmount = 0;
    }

    public void IncreaseMoney(int amount)
    {
        MoneyAmount += amount;
    }

    public void DecreaseMoney(int amount)
    {
        MoneyAmount -= amount;
    }
}
