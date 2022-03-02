namespace ExpressCargo.Core.Utilities.Errors;
public static class Try
{
    public static bool Catch(Action action)
    {
        try
        {
            action();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
