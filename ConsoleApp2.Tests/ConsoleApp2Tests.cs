using Moq;
using NUnit.Framework;

public class TesteBNP_Tests
{

    private IDatabaseRepository _mockDataBase;
    private IAPIClient _mockApi;
    private ILog _mockLog;


    [SetUp]
    public void startup()
    {
        _mockDataBase = Mock.Of<IDatabaseRepository>();
        _mockApi = Mock.Of<IAPIClient>();
        _mockLog = Mock.Of<ILog>();
    }

    [Test]
    void ShouldSaveOnDataBase()
    {
        var _testeBNP = new TesteBNP(_mockDataBase, _mockApi, _mockLog);

        var result = _testeBNP.SaveOnDataBase("testOfSave001", 50);

        Assert.That(result, Is.True);
    }

    [Test]
    void ShouldNOTSaveOnDataBase()
    {
        var _testeBNP = new TesteBNP(_mockDataBase, _mockApi, _mockLog);
        
        //must configure the return of _mockDataBase, when ISIN code is null

        var result = _testeBNP.SaveOnDataBase(null, 50);

        Assert.That(result, Is.False);
    }
}