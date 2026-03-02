namespace Productos.Models;
public class IdCounter {
    private static int _idCounter = 0;
    
    public static int NextId() {
        _idCounter++;
        return _idCounter;
    }
}