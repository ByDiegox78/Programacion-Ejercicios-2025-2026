using Conquistadores_de_Catán.Enum;

namespace Conquistadores_de_Catán.Class;

public class Casilla {
    public Recurso Recurso { get; set; }
    public Duenio? Duenio { get; set; } = null;

    public int Valor {
        get;
        set => field = (value > 0 && value < 7)
            ? value
            : throw new ArgumentOutOfRangeException(nameof(value), "El valor tiene que ser mayor que 0 y menor q 7.");
    }
}

//     public int GetValor() {
//         return Valor;
//     }
//     
//     public void SetValor(int valor) {
//         if (!IsValorValido(valor)) {
//             throw new ArgumentException("El valor debe estar entre 0 y 7");
//         }
//         Valor = valor;
//     }
//     private bool IsValorValido(int valorV) {
//         return valorV > 0 && valorV < 7;
//     }
// }