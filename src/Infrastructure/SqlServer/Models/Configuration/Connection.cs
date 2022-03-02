using ExpressCargo.Core.Extensions;

namespace ExpressCargo.SqlServer.Models.Configuration
{
    public class Connection : Core.Models.Configurations.Connection
    {
        #region Overrides of Connection

        public override string ToString(string delimiter = ";")
        {
            var results = new Dictionary<string, string>
            {
                { "Data Source", Datasource           },
                { "Database",    Database             },
                { "User Id",     UserId               },
                { "Password",    Password             },
                { "",            AdditionalParameters }
            };
            return string.Join("=", results.Where(e => ValidParameter(e.Value)));
        }

        #endregion
    }
}
