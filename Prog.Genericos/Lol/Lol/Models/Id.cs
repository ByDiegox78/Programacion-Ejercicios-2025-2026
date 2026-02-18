namespace Lol.Models;

public static class Id {
    private static int _idCounter = 0;
    public static int NextId()
    {
        _idCounter++;
        return _idCounter;
    }}