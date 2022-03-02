using ExpressCargo.Core.Models.Configurations;

namespace ExpressCargo.SqlServer;
public class ExpressCargoConnection : Connection
{
    #region Overrides of Connection

    public override string ToString(string delimiter = ";")
    {
        return $"Data Source={Datasource}; Database={Database}; User Id={UserId}; Password={Password}; {AdditionalParameters}";
    }

    #endregion
}