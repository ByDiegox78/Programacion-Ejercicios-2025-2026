using Conquistadores_de_Catán.Enum;

namespace Conquistadores_de_Catán.Class;

public class Casilla {
    private Recurso _recurso;
    private Duenio? _duenio = null;
    private int _valor;

    public Recurso GetRecurso() {
        return _recurso;
    }

    public Duenio? GetDueño() {
        return _duenio;
    }
    public int GetValor() {
        return _valor;
    }

    public void SetRecurso(Recurso recurso) {
        _recurso = recurso;
    }

    public void SetDueño(Duenio duenio) {
        _duenio = duenio;
    }
    public void SetValor(int valor) {
        if (!IsValorValido(valor)) {
            throw new ArgumentException("El valor debe estar entre 0 y 7");
        }
        _valor = valor;
    }
    private bool IsValorValido(int valorV) {
        return valorV > 0 && valorV < 7;
    }
}