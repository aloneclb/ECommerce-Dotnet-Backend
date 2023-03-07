using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;

namespace ETicaret.Persistence.ExecutionStrategies;

public class ETicaretExecutionStrategy : ExecutionStrategy
{
    public ETicaretExecutionStrategy(ExecutionStrategyDependencies dependencies, int maxRetryCount, TimeSpan maxRetryDelay)
        : base(dependencies, maxRetryCount, maxRetryDelay)
    {
    }

    protected override bool ShouldRetryOn(Exception exception)
    {
        // yalnizca sqlException'larda tekrar denemenin otomatik yapilmasini istiyoruz
        // dbupdate exception'larini manuel handle edecegiz
        // ornek ISafeTaskRunner interface'inde
        var retry = exception is SqlException;
        return retry;
    }
}