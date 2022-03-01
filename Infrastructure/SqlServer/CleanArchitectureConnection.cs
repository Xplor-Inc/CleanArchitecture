using CleanArchitecture.Core.Models.Configurations;

namespace CleanArchitecture.SqlServer;
public class CleanArchitectureConnection : Connection
{
    #region Overrides of Connection

    public override string ToString(string delimiter = ";")
    {
        return $"Data Source={Datasource}; Database={Database}; User Id={UserId}; Password={Password}; {AdditionalParameters}";
    }

    #endregion
}