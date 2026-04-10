using FluentAssertions;
using Moq;
using PersonaService.Cache;
using PersonaService.Models;
using PersonaService.Repositories;
using PersonaService.Validators;
using PersonaService.Services;

namespace PersonaService.Test.Services;

[TestFixture]
public class PersonaServiceTest {
    [TestFixture]
    public class CasosValidos {
        private Mock<IPersonaRepository> _mockRepository = null;
        private Mock<IValidador<Persona>> _mockValidator = null!;
        private PersonaService.Services.PersonaService _service = null!;
        private Mock<ICache<int, Persona>> _mockCache = null;
        
        [SetUp]
        public void Setup() {
            _mockRepository = new Mock<IPersonaRepository>();
            _mockValidator = new Mock<IValidador<Persona>>();
            _mockCache = new Mock<ICache<int, Persona>>();
            _service = new PersonaService.Services.PersonaService(_mockRepository.Object, _mockValidator.Object, _mockCache.Object);
        }

        [Test]
        public void GetAll_Existen_DevolverTodas() {
            //arrange
            var personas = new List<Persona> {
                new Persona() { Id = 1, Nombre = "Paco", Email = "PacoHG@gmail.com" },
                new Persona() { Id = 2, Nombre = "Manuel", Email = "ManuelDH@gmail.com" }
            };
            
            _mockRepository.Setup(x => x.GetAll()).Returns(personas);
            
            //act
            var resultado = _service.GetAll();
            
            //assert
            resultado.Should().HaveCount(2);
            
            //veryfy
            _mockRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void GetById_Existen_DevolverPersona() {
            var personas = new Persona() { Id = 1, Nombre = "Paco", Email = "PacoHG@gmail.com" };
            _mockRepository.Setup(x => x.GetById(1)).Returns(personas);
            _mockCache.Setup(x => x.Get(It.IsAny<int>())).Returns(personas);
            
            //act
            var resultado = _service.GetById(1);
            
            //assert
            resultado.Should().NotBeNull();
            resultado.Nombre.Should().Be("Paco");
            
            //veryfy
            _mockRepository.Verify(x => x.GetById(1), Times.Once);
        }

        [Test]
        public void Create_Valido() {
            var personas = new Persona() { Id = 0, Nombre = "Paco", Email = "PacoHG@gmail.com" };
            var personaCreada = new Persona() { Id = 1, Nombre = "Paco", Email = "PacoHG@gmail.com" };
            
            _mockValidator.Setup(x => x.Validar(personas)).Returns(new List<string>());
            _mockRepository.Setup(x => x.Create(personas)).Returns(personaCreada);
            
            var resultado = _service.Create(personas);
            
            resultado.Should().NotBeNull();
            resultado.Id.Should().Be(1);
            
            _mockRepository.Verify(x => x.Create(personas), Times.Once);
            _mockValidator.Verify(x => x.Validar(personas), Times.Once);
            
        }
        
        [Test]
        public void Update_ValidaYExiste_RetornaActualizada() {
           
            var persona = new Persona() { Id = 0, Nombre = "Paco", Email = "PacoHernandez@gmail.com" };
            var personaActualizada = new Persona() { Id = 1, Nombre = "Paco", Email = "PacoHernandez@gmail.com" };
            
            _mockValidator.Setup(v => v.Validar(persona)).Returns(new List<string>());
            _mockRepository.Setup(r => r.Update(1, persona)).Returns(personaActualizada);

           
            var resultado = _service.Update(1, persona);

           
            resultado.Should().NotBeNull();
            resultado.Nombre.Should().Be("Juan Actualizado");

           
            _mockValidator.Verify(v => v.Validar(persona), Times.Once);
            _mockRepository.Verify(r => r.Update(1, persona), Times.Once);
        }
        
        [Test]
        public void Delete_Existe_RetornaEliminada()
        {
            // Arrange
            var persona = new Persona() { Id = 0, Nombre = "Paco", Email = "PacoHernandez@gmail.com" };
            _mockRepository.Setup(r => r.Delete(1, true)).Returns(persona);

            // Act
            var resultado = _service.Delete(1, true);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Nombre.Should().Be("Juan");

            // Verify
            _mockRepository.Verify(r => r.Delete(1, true), Times.Once);
        }
    }

    [TestFixture]
    public class CasosInvalidos {
        
    }
}

























