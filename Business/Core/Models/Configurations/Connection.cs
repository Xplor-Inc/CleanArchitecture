using CleanArchitecture.Core.Interfaces.Data;

namespace CleanArchitecture.Core.Models.Configurations;
public class Connection : IConnection
{
    #region Properties

        public string AdditionalParameters  { get; set; }
        public string Database              { get; set; }
        public string Datasource            { get; set; }
        public string Password              { get; set; }
        public string UserId                { get; set; }

        #endregion Properties


    #region Public Methods

        public virtual string ToString(string delimiter = ";")
        {
            var results = new List<string>
            {
                Datasource,
                Database,
                Password,
                UserId,
                AdditionalParameters
            };

            return string.Join(delimiter, results.Where(ValidParameter));
        }

        #endregion Public Methods


    #region Protected Methods

        protected static bool ValidParameter(string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        #endregion Protected Methods
}
