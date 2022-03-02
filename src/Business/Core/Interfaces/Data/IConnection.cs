namespace ExpressCargo.Core.Interfaces.Data;
public interface IConnection
{
    #region Properties
        string AdditionalParameters { get; }
        string Database             { get; }
        string Datasource           { get; }
        string Password             { get; }
        string UserId               { get; }

        #endregion Properties


    #region Methods
        string ToString(string delimiter = ";");

        #endregion
}