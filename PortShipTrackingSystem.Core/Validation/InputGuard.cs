namespace PortShipTrackingSystem.Core.Validation;

public static class InputGuard
{
    public static bool ContainsHtmlTags(string input)
    {
        return input.Contains('<') || input.Contains('>');
    }
}