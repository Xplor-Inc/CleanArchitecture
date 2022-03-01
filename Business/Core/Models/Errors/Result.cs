namespace CleanArchitecture.Core.Models.Errors;
    public class Result<T>
    {
        #region Properties

        public virtual List<string>               Errors         { get; set; }
        public virtual bool                       HasErrors      => Errors != null && Errors.Any();
        public virtual T                          ResultObject   { get; set; }
        public long                               RowCount       { get; set; }


        #endregion Properties


        #region Constructors

        public Result() {}
        public Result(string errorMessage)                  => this.AddError(errorMessage);
        public Result(T resultObject)                       => ResultObject = resultObject;

        #endregion Constructors
    }
