using System;

public class EnergyLimit
{
    private int _amount;

    public int Amount => _amount;
    public bool HasEnergy => _amount > 0;

    public void Set(int amount)
    {
        _amount = amount;
    }

    public void Add()
    {
        _amount ++;
    }

    public void Subtract()
    {
        _amount--;

        if (_amount < 0) 
            throw new InvalidOperationException($" {nameof (_amount)} cant be lover then 0");
    }
}
