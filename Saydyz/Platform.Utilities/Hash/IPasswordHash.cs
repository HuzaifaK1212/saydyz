namespace Platform.Utilities.Hash
{
    public interface IPasswordHash
    {
        string GetHash(string password);
    }
}
