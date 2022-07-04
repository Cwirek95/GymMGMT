namespace GymMGMT.Application.Caching
{
    public interface ICacheable
    {
        string CacheKey { get; }
    }
}