using System;

public class SignalHandlerAttribute : Attribute
{
    private string signalName;
    public string SignalName {
        get {
            return signalName;
        }
    }

    public SignalHandlerAttribute(string signalName) {
        this.signalName = signalName;
    }
}
