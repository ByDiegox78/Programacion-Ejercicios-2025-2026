namespace TestEntorno;

public class Validar {
    public static string ValidarAcceso(string usuario, string password, int reintentos) {
        if (usuario == "admin") {
            if (password == "1234") {
                return "Acceso concedido";          
            } else { 
                if (reintentos > 3) {
                    return "Cuenta bloqueada";     
                } 
                return "Password incorrecto";       
            }   
        } else {
            return "Usuario no existe";            
        }
    }
}