// See https://aka.ms/new-console-template for more information


using System.ComponentModel;
using System.Diagnostics;

public class TesteBNP
{
    private readonly IDatabaseRepository _databaseRepository;
    private readonly IAPIClient _apiClient;
    private readonly ILog _log;

    public TesteBNP(IDatabaseRepository databaseRepository, IAPIClient apiClient, ILog log)
    {
        _databaseRepository = databaseRepository;
        _log = log;
        _apiClient = apiClient;
    }


    public void IsinReceiver(string[] isins)
    {
        if (!isins.Any() || !IsValid(isins))
            return;

        foreach (var isin in isins)
        {
            decimal price = GetIsinSecurityPrice(isin);

            if (SaveOnDataBase(isin, price))
                _log.Log($"Success on save the price of ISIN: '{isin}");
        }
    }

    public decimal GetIsinSecurityPrice(string isin)
    {
        try
        {
            return _apiClient.GetSecurityPrice(isin);
        }
        catch (Exception ex)
        {
            _log.LogError($" Error on obatin the price of ISIN: '{isin}'. Error message: {ex.Message};");
            return 0;
        }
    }

    public bool SaveOnDataBase(string isin, decimal price)
    {
        try
        {
            return _databaseRepository.Save(isin, price);
        }
        catch (Exception ex)
        {
            _log.LogError($" Error on store the price of ISIN: '{isin}'. Error message: {ex.Message};");
            return false;
        }
    }

    private bool IsValid(string[] isins)
    {
        var isValid = false;

        foreach (var isin in isins)
        {
            isValid = isin.Length == 12 ? false : true;

            if (!isValid)
                return isValid;
        }
        return isValid;

    }
}
