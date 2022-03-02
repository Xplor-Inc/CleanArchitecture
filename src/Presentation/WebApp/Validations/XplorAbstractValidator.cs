namespace CleanArchitecture.WebApp.Validations
{
    public class XplorAbstractValidator
    {
        protected bool BeGreaterThan0<T>(T _, long value)
        {
            return value > 0;
        }

        protected bool BeGreaterThan0WhenSet<T>(T _, long? value)
        {
            return value == null || value > 0;
        }

        protected bool BeGreaterThanOrEqualTo0WhenSet<T>(T _, int? value)
        {
            return value == null || value >= 0;
        }

        protected bool BeGreaterThanOrEqualTo0WhenSet<T>(T _, decimal? value)
        {
            return value == null || value >= 0;
        }
        protected bool EndDateLaterThanOrEqualToStartDate(DateTime startDate, DateTime endDate)
        {
            return DateTime.Compare(startDate.Date, endDate.Date) <= 0;
        }

        protected bool EndTimeLaterThanStartTime(DateTime startDate, DateTime endDate)
        {
            return DateTime.Compare(startDate, endDate) < 0;
        }
    }
}
