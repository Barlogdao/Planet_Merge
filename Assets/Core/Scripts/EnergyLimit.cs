using System;

public class EnergyLimit
{
    private int _amount;

    public event Action<int> AmountChanged;

    public bool HasEnergy => _amount > 0;

    public void Prepare(int amount)
    {
        _amount = amount;

        RaiseAmountChangedEvent();
    }

    public void Add()
    {
        _amount ++;
        RaiseAmountChangedEvent();
    }

    public bool Subtract()
    {
        if (HasEnergy)
        {
            _amount--;
            RaiseAmountChangedEvent();
            return true;
        }

        return false;
    }

    private void RaiseAmountChangedEvent()
    {
        AmountChanged?.Invoke(_amount);
    }
}
