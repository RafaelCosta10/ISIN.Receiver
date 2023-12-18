// See https://aka.ms/new-console-template for more information


public interface IDatabaseRepository
{
    bool Save(string isin, decimal price);

}

public interface ILog
{
    void Log(string message);
    void LogError(string errorMessage);
}
public interface IAPIClient
{
    decimal GetSecurityPrice(string isin);
}


