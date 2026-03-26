using NUnit.Framework;
using TestEntorno;

Main();

void Main() {
    
}



[TestFixture]
public class ValidarTest {
    [Test]
    public void Es_Administrador() {
        string usuario = "admin";
        string password = "1234";
        int reintentos = 1;
        
        string resultado = Validar.ValidarAcceso(usuario, password, reintentos);
        
        Assert.That(resultado, Is.EqualTo("Acceso concedido"));
    }

    [Test]
    public void NoEsAdmin() {
        string usuario = "user";
        string password = "12345";
        int reintentos = 1;
        
        string resultado = Validar.ValidarAcceso(usuario, password, reintentos);
        
        Assert.That(resultado, Is.EqualTo("Usuario no existe"));
    }

    [Test]
    public void ContraseñaIncorrectaAdmin() {
        string usuario = "admin";
        string password = "12345";
        int reintentos = 1;
        
        string resultado = Validar.ValidarAcceso(usuario, password, reintentos);
        
        Assert.That(resultado, Is.EqualTo("Password incorrecto"));
        
    }

    [Test]
    public void IntentosSuperados() {
        string usuario = "admin";
        string password = "12345";
        int reintentos = 4;
        
        string resultado = Validar.ValidarAcceso(usuario, password, reintentos);
        
        Assert.That(resultado, Is.EqualTo("Cuenta bloqueada"));
    }
    
    
}