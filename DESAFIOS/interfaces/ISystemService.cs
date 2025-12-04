namespace Target.Interfaces
{
    public interface ISystemService
    {
        Guid NewGuid();
        DateTime Now();
    }
}
