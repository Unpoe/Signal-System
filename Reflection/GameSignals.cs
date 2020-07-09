using System.Collections.Generic;
using Signals;

public static class GameSignals
{
    public static readonly Signal DayFinished = new Signal("Day Finished");
    public static readonly Signal PlayerDamaged = new Signal("Player Damaged");

    private static Dictionary<string, Signal> SIGNALS = new Dictionary<string, Signal>() {
            {DayFinished.Name, DayFinished },
            {PlayerDamaged.Name, PlayerDamaged }
        };

    public static Signal GetSignalByName(string signalName) {
        return SIGNALS[signalName];
    }
}

