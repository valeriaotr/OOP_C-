namespace OOP_ICT.Second.Models;

public class PayUnit
{
    public int Integer { get; set; }
    public double Fractional { get; set; }

    public PayUnit(int integer, double fractional)
    {
        Integer = integer;
        Fractional = fractional;
    }
    
    public PayUnit(double value)
    {
        Integer = (int)value;
        Fractional = (int)((value - Integer) * 100); 
    }
    
    public static bool operator <=(PayUnit a, PayUnit b)
    {
        return a.Integer < b.Integer || (a.Integer == b.Integer && a.Fractional <= b.Fractional);
    }

    public static bool operator >=(PayUnit a, PayUnit b)
    {
        return a.Integer > b.Integer || (a.Integer == b.Integer && a.Fractional >= b.Fractional);
    }
    
    public override bool Equals(object obj)
    {
        if (obj is PayUnit other)
        {
            return Integer == other.Integer && Fractional == other.Fractional;
        }
        return false;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(Integer, Fractional);
    }
}