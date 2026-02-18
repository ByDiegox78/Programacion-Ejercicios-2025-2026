using Lol.Models;

namespace Lol.Factories;

/// <summary>
///     Factoría con datos semilla fijos para TODOS los campeones de League of Legends con TODAS sus habilidades.
/// </summary>
public static class CampeonesFactory {
    /// <summary>
    ///     Genera la semilla de datos inicial con todos los campeones de League of Legends y sus habilidades completas.
    /// </summary>
    /// <returns>Enumerable con datos de demostración</returns>
    public static IEnumerable<Campeon> Seed() {
        var lista = new List<Campeon>();

        #region =============== AGUAS ESTANCADAS ===============

        // TAMM KENCH
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Tahm Kench",
            Nivel = 1,
            PrecioEsencias = 6300,
            Armadura = 91,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Lengua azotadora", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 7 },
                new() { Nombre = "Inmersión", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 20 },
                new() { Nombre = "Golpe de escudo", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Escudo }, Cooldawn = 3 },
                new() { Nombre = "Devorar", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.DañoVerdadero }, Cooldawn = 120 }
            }
        });

        // PYKE
        lista.Add(new Asesino {
            Id = Id.NextId(),
            Nombre = "Pyke",
            Nivel = 1,
            PrecioEsencias = 4800,
            Letalidad = 23,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Arpón óseo", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 14 },
                new() { Nombre = "Inmersión fantasma", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño>(), Cooldawn = 12 },
                new() { Nombre = "Agua fantasma", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 15 },
                new() { Nombre = "Aguas de la muerte", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.DañoVerdadero }, Cooldawn = 120 }
            }
        });

        // NAUTILUS
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Nautilus",
            Nivel = 1,
            PrecioEsencias = 4800,
            Armadura = 89,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Ancla perforadora", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 14 },
                new() { Nombre = "Ira del titán", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico, TipoDaño.Escudo }, Cooldawn = 12 },
                new() { Nombre = "Golpe turbulento", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 7 },
                new() { Nombre = "Carga de profundidad", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 120 }
            }
        });

        // JANNA
        lista.Add(new Mago {
            Id = Id.NextId(),
            Nombre = "Janna",
            Nivel = 1,
            PrecioEsencias = 1350,
            PoderHabilidad = 100,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Ciclón aullador", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 12 },
                new() { Nombre = "Viento favorable", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 12 },
                new() { Nombre = "Escudo de tormenta", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Escudo }, Cooldawn = 15 },
                new() { Nombre = "Monzón", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Curacion }, Cooldawn = 150 }
            }
        });

        // ZAC
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Zac",
            Nivel = 1,
            PrecioEsencias = 4800,
            Armadura = 86,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Golpe estirado", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 9 },
                new() { Nombre = "Materia inestable", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 5 },
                new() { Nombre = "Amasamiento elástico", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 24 },
                new() { Nombre = "¡Rebote!", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 130 }
            }
        });

        // SENNA
        lista.Add(new Adc {
            Id = Id.NextId(),
            Nombre = "Senna",
            Nivel = 1,
            PrecioEsencias = 6300,
            VelocidadDeAtaque = 0.80,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Oscuridad perforante", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico, TipoDaño.Curacion }, Cooldawn = 15 },
                new() { Nombre = "Niebla espectral", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 16 },
                new() { Nombre = "Maldición de la niebla", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño>(), Cooldawn = 26 },
                new() { Nombre = "Sombra del amanecer", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico, TipoDaño.Escudo }, Cooldawn = 160 }
            }
        });

        // GRAVES
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Graves",
            Nivel = 1,
            PrecioEsencias = 4800,
            Armadura = 82,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Perdigones", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 8 },
                new() { Nombre = "Humareda", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 20 },
                new() { Nombre = "Salida rápida", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño>(), Cooldawn = 16 },
                new() { Nombre = "Impacto de cañón", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 110 }
            }
        });

        // FIORA
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Fiora",
            Nivel = 1,
            PrecioEsencias = 4800,
            Armadura = 84,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Estocada", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 16 },
                new() { Nombre = "Finta", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 24 },
                new() { Nombre = "Golpe doble", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 11 },
                new() { Nombre = "Gran desafío", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico, TipoDaño.Curacion }, Cooldawn = 110 }
            }
        });

        #endregion

        #region =============== BANDLE CITY ===============

        // LULU
        lista.Add(new Mago {
            Id = Id.NextId(),
            Nombre = "Lulu",
            Nivel = 1,
            PrecioEsencias = 4800,
            PoderHabilidad = 105,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Brillo brillante", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 7 },
                new() { Nombre = "Capricho", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño>(), Cooldawn = 17 },
                new() { Nombre = "Ayuda, Pix!", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico, TipoDaño.Escudo }, Cooldawn = 8 },
                new() { Nombre = "Crecimiento salvaje", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Curacion }, Cooldawn = 120 }
            }
        });

        // TEEMO
        lista.Add(new Asesino {
            Id = Id.NextId(),
            Nombre = "Teemo",
            Nivel = 1,
            PrecioEsencias = 1350,
            Letalidad = 10,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Dardo venenoso", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 8 },
                new() { Nombre = "Movilidad", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño>(), Cooldawn = 17 },
                new() { Nombre = "Emboscada", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 0 },
                new() { Nombre = "Carga de setas", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 30 }
            }
        });

        // TRIGGER
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Tristana",
            Nivel = 1,
            PrecioEsencias = 1350,
            Armadura = 84,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Explosión rápida", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño>(), Cooldawn = 20 },
                new() { Nombre = "Cargador de cohetes", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 22 },
                new() { Nombre = "Explosivo", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 16 },
                new() { Nombre = "Explosión destructiva", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 120 }
            }
        });

        // POPPY
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Poppy",
            Nivel = 1,
            PrecioEsencias = 450,
            Armadura = 90,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Martillo devastador", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 8 },
                new() { Nombre = "Muro de acero", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 24 },
                new() { Nombre = "Carga heroica", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 14 },
                new() { Nombre = "Guardián del juicio", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 120 }
            }
        });

        // RUMBLE
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Rumble",
            Nivel = 1,
            PrecioEsencias = 4800,
            Armadura = 85,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Lanzallamas", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 10 },
                new() { Nombre = "Electroagarra", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Escudo }, Cooldawn = 7 },
                new() { Nombre = "Erizos sonda", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 10 },
                new() { Nombre = "Misil igualador", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 120 }
            }
        });

        // CORKI
        lista.Add(new Adc {
            Id = Id.NextId(),
            Nombre = "Corki",
            Nivel = 1,
            PrecioEsencias = 3150,
            VelocidadDeAtaque = 0.84,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Bomba fosfórica", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 8 },
                new() { Nombre = "Paquete de valor", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 22 },
                new() { Nombre = "Gatling", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 16 },
                new() { Nombre = "Misil barracuda", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 2 }
            }
        });

        // VEX
        lista.Add(new Mago {
            Id = Id.NextId(),
            Nombre = "Vex",
            Nivel = 1,
            PrecioEsencias = 6300,
            PoderHabilidad = 128,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Proyectil lúgubre", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 9 },
                new() { Nombre = "Espacio personal", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico, TipoDaño.Escudo }, Cooldawn = 16 },
                new() { Nombre = "Vuelo inminente", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 13 },
                new() { Nombre = "Sombra del susto", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 140 }
            }
        });

        // YORDLES (otros)
        lista.Add(new Mago {
            Id = Id.NextId(),
            Nombre = "Veigar",
            Nivel = 1,
            PrecioEsencias = 1350,
            PoderHabilidad = 150,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Bola oscura", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 6 },
                new() { Nombre = "Materia oscura", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 8 },
                new() { Nombre = "Horizonte de eventos", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 18 },
                new() { Nombre = "Explosión primordial", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 120 }
            }
        });

        lista.Add(new Mago {
            Id = Id.NextId(),
            Nombre = "Ziggs",
            Nivel = 1,
            PrecioEsencias = 4800,
            PoderHabilidad = 132,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Bomba expansiva", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 6 },
                new() { Nombre = "Paquete de explosivos", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 24 },
                new() { Nombre = "Campo de retardación", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 16 },
                new() { Nombre = "Mega bomba", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 120 }
            }
        });

        lista.Add(new Mago {
            Id = Id.NextId(),
            Nombre = "Heimerdinger",
            Nivel = 1,
            PrecioEsencias = 3150,
            PoderHabilidad = 132,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Torreta H-28G", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 20 },
                new() { Nombre = "Granada hextech", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 11 },
                new() { Nombre = "Campo de choque", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 12 },
                new() { Nombre = "Actualización", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño>(), Cooldawn = 100 }
            }
        });

        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Gnar",
            Nivel = 1,
            PrecioEsencias = 6300,
            Armadura = 85,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Bumerán arrojadizo", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 20 },
                new() { Nombre = "Hiperactivo", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 12 },
                new() { Nombre = "Salto", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 22 },
                new() { Nombre = "¡Gnar!", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 120 }
            }
        });

        lista.Add(new Mago {
            Id = Id.NextId(),
            Nombre = "Kennen",
            Nivel = 1,
            PrecioEsencias = 4800,
            PoderHabilidad = 112,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Shuriken trueno", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 8 },
                new() { Nombre = "Asalto eléctrico", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 14 },
                new() { Nombre = "Embate relámpago", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 10 },
                new() { Nombre = "Tormenta letal", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 120 }
            }
        });

        #endregion

        #region =============== CIUDAD DE PILTOVER ===============

        // CAMILLE
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Camille",
            Nivel = 1,
            PrecioEsencias = 6300,
            Armadura = 86,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Tornillo de precisión", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico, TipoDaño.DañoVerdadero }, Cooldawn = 9 },
                new() { Nombre = "Barrido táctico", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 15 },
                new() { Nombre = "Enganche", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 16 },
                new() { Nombre = "Divisor de hextech", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 140 }
            }
        });

        // JAYCE
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Jayce",
            Nivel = 1,
            PrecioEsencias = 4800,
            Armadura = 84,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Bomba de choque", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 8 },
                new() { Nombre = "Campo de hipercarga", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 13 },
                new() { Nombre = "Puerta de aceleración", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño>(), Cooldawn = 16 },
                new() { Nombre = "Cambio de postura", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 6 }
            }
        });

        // VI
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Vi",
            Nivel = 1,
            PrecioEsencias = 4800,
            Armadura = 84,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Golpe demoledor", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 14 },
                new() { Nombre = "Golpe penetrante", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 0 },
                new() { Nombre = "Exceso de fuerza", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 8 },
                new() { Nombre = "Asalto y captura", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 140 }
            }
        });

        // JINX
        lista.Add(new Adc {
            Id = Id.NextId(),
            Nombre = "Jinx",
            Nivel = 1,
            PrecioEsencias = 4800,
            VelocidadDeAtaque = 0.85,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "¡Zas!", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 8 },
                new() { Nombre = "¡Mordiscos!", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 24 },
                new() { Nombre = "¡Supermegacohete de la muerte!", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 75 }
            }
        });

        // CAITLYN
        lista.Add(new Adc {
            Id = Id.NextId(),
            Nombre = "Caitlyn",
            Nivel = 1,
            PrecioEsencias = 4800,
            VelocidadDeAtaque = 0.81,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Paz y quietud", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 10 },
                new() { Nombre = "Red trampa", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño>(), Cooldawn = 30 },
                new() { Nombre = "Red de seguridad", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 16 },
                new() { Nombre = "Paz y quietud definitiva", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 90 }
            }
        });

        // ORIANNA
        lista.Add(new Mago {
            Id = Id.NextId(),
            Nombre = "Orianna",
            Nivel = 1,
            PrecioEsencias = 4800,
            PoderHabilidad = 132,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Comando: Atacar", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 6 },
                new() { Nombre = "Comando: Disrupción", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 9 },
                new() { Nombre = "Comando: Proteger", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico, TipoDaño.Escudo }, Cooldawn = 10 },
                new() { Nombre = "Comando: Onda de choque", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 120 }
            }
        });

        // SERAPHINE
        lista.Add(new Mago {
            Id = Id.NextId(),
            Nombre = "Seraphine",
            Nivel = 1,
            PrecioEsencias = 6300,
            PoderHabilidad = 125,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Nota alta", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 8 },
                new() { Nombre = "Sonido envolvente", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Escudo, TipoDaño.Curacion }, Cooldawn = 22 },
                new() { Nombre = "Ritmo beat", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 13 },
                new() { Nombre = "Actuación final", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 160 }
            }
        });

        // ZERI
        lista.Add(new Adc {
            Id = Id.NextId(),
            Nombre = "Zeri",
            Nivel = 1,
            PrecioEsencias = 6300,
            VelocidadDeAtaque = 0.91,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Ráfaga de pulsos", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 1},
                new() { Nombre = "Láser ultrarrápido", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 23 },
                new() { Nombre = "Chispa eléctrica", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 23 },
                new() { Nombre = "Sobrecarga", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico, TipoDaño.Fisico }, Cooldawn = 100 }
            }
        });

        // EKKO
        lista.Add(new Asesino {
            Id = Id.NextId(),
            Nombre = "Ekko",
            Nivel = 1,
            PrecioEsencias = 6300,
            Letalidad = 16,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Cronorruptor", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 9 },
                new() { Nombre = "Convergencia paralela", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Escudo }, Cooldawn = 22 },
                new() { Nombre = "Desfase", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 9 },
                new() { Nombre = "Cronorruptura", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico, TipoDaño.Curacion }, Cooldawn = 110 }
            }
        });

        #endregion

        #region =============== ZAUN ===============

        // ZAC (ya incluido en Aguas Estancadas)
        // WARWICK
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Warwick",
            Nivel = 1,
            PrecioEsencias = 450,
            Armadura = 85,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Zarpazo bestial", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico, TipoDaño.Curacion }, Cooldawn = 6 },
                new() { Nombre = "Aullido de sangre", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño>(), Cooldawn = 24 },
                new() { Nombre = "Prueba de fuerza", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 15 },
                new() { Nombre = "Atadura infinita", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 110 }
            }
        });

        // SINGED
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Singed",
            Nivel = 1,
            PrecioEsencias = 450,
            Armadura = 85,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Estela venenosa", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 0 },
                new() { Nombre = "Lanzamiento de megaadhesivo", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 14 },
                new() { Nombre = "Carga de veneno", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 10 },
                new() { Nombre = "Poción insectoide", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Curacion }, Cooldawn = 120 }
            }
        });

        // BLITZCRANK
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Blitzcrank",
            Nivel = 1,
            PrecioEsencias = 3150,
            Armadura = 88,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Garra cohete", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 20 },
                new() { Nombre = "Sobrecarga", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño>(), Cooldawn = 15 },
                new() { Nombre = "Campo de fuerza", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 13 },
                new() { Nombre = "Campo estático", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 60 }
            }
        });

        // URGOT
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Urgot",
            Nivel = 1,
            PrecioEsencias = 4800,
            Armadura = 89,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Intercambio ácido", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 10 },
                new() { Nombre = "Escudo purgador", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico, TipoDaño.Escudo }, Cooldawn = 13 },
                new() { Nombre = "Disparo de desprecio", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 16 },
                new() { Nombre = "Más allá de la muerte", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.DañoVerdadero }, Cooldawn = 120 }
            }
        });

        // MUNDO
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Dr. Mundo",
            Nivel = 1,
            PrecioEsencias = 450,
            Armadura = 87,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Machaque quirúrgico", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 4 },
                new() { Nombre = "Quemadura", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico, TipoDaño.Curacion }, Cooldawn = 17 },
                new() { Nombre = "Martilleo", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 8 },
                new() { Nombre = "Dosis máxima", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Curacion }, Cooldawn = 110 }
            }
        });

        // JINX (ya incluida en Piltover)
        // VIKTOR
        lista.Add(new Mago {
            Id = Id.NextId(),
            Nombre = "Viktor",
            Nivel = 1,
            PrecioEsencias = 4800,
            PoderHabilidad = 135,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Transferencia de poder", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico, TipoDaño.Escudo }, Cooldawn = 9 },
                new() { Nombre = "Campo gravitatorio", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño>(), Cooldawn = 17 },
                new() { Nombre = "Rayo láser de muerte", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 13 },
                new() { Nombre = "Tormenta del caos", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 120 }
            }
        });

        // TWITCH
        lista.Add(new Adc {
            Id = Id.NextId(),
            Nombre = "Twitch",
            Nivel = 1,
            PrecioEsencias = 3150,
            VelocidadDeAtaque = 0.87,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Ambush", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño>(), Cooldawn = 16 },
                new() { Nombre = "Contagio", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 13 },
                new() { Nombre = "Dardo del vicio", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 12 },
                new() { Nombre = "Andanada", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 90 }
            }
        });

        // ZAC (ya incluido)
        // ZIGGS (ya incluido en Bandle City)
        // RENATA GLASC
        lista.Add(new Mago {
            Id = Id.NextId(),
            Nombre = "Renata Glasc",
            Nivel = 1,
            PrecioEsencias = 6300,
            PoderHabilidad = 110,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Chaqueta de mano", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 16 },
                new() { Nombre = "Banco de lealtad", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño>(), Cooldawn = 24 },
                new() { Nombre = "Celo", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico, TipoDaño.Escudo }, Cooldawn = 14 },
                new() { Nombre = "Hostigamiento", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 150 }
            }
        });

        #endregion

        #region =============== DEMACIA ===============

        // GAREN
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Garen",
            Nivel = 1,
            PrecioEsencias = 1350,
            Armadura = 91,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Golpe decisivo", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 8 },
                new() { Nombre = "Coraje", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Escudo }, Cooldawn = 23 },
                new() { Nombre = "Juicio", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 9 },
                new() { Nombre = "Justicia de Demacia", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 120 }
            }
        });

        // LUX
        lista.Add(new Mago {
            Id = Id.NextId(),
            Nombre = "Lux",
            Nivel = 1,
            PrecioEsencias = 3150,
            PoderHabilidad = 130,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Atadura de luz", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 11 },
                new() { Nombre = "Barrera prismática", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Escudo }, Cooldawn = 13 },
                new() { Nombre = "Singularidad luminosa", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 10 },
                new() { Nombre = "Chispa final", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 80 }
            }
        });

        // JARVAN IV
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Jarvan IV",
            Nivel = 1,
            PrecioEsencias = 4800,
            Armadura = 87,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Lanza dragón", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 10 },
                new() { Nombre = "Escudo dorado", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Escudo }, Cooldawn = 12 },
                new() { Nombre = "Estandarte demaciano", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 13 },
                new() { Nombre = "Cataclismo", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 120 }
            }
        });

        // QUINN
        lista.Add(new Adc {
            Id = Id.NextId(),
            Nombre = "Quinn",
            Nivel = 1,
            PrecioEsencias = 4800,
            VelocidadDeAtaque = 0.86,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Golpe cegador", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 11 },
                new() { Nombre = "Sentidos agudizados", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño>(), Cooldawn = 23 },
                new() { Nombre = "Vuelo", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 12 },
                new() { Nombre = "¡Aquí estoy!", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 3 }
            }
        });

        // SHYVANA
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Shyvana",
            Nivel = 1,
            PrecioEsencias = 3150,
            Armadura = 86,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Mordisco doble", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 9 },
                new() { Nombre = "Explosión ígnea", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 12 },
                new() { Nombre = "Aliento de fuego", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 11 },
                new() { Nombre = "Descendente del dragón", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 120 }
            }
        });

        // SONA
        lista.Add(new Mago {
            Id = Id.NextId(),
            Nombre = "Sona",
            Nivel = 1,
            PrecioEsencias = 1350,
            PoderHabilidad = 105,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Crescendo", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 8 },
                new() { Nombre = "Aria de la perseverancia", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Curacion, TipoDaño.Escudo }, Cooldawn = 10 },
                new() { Nombre = "Canción de la celeridad", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño>(), Cooldawn = 12 },
                new() { Nombre = "Ralentizando", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 140 }
            }
        });

        // XIN ZHAO
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Xin Zhao",
            Nivel = 1,
            PrecioEsencias = 3150,
            Armadura = 85,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Tres estocadas", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 9 },
                new() { Nombre = "Hoja del trueno", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 12 },
                new() { Nombre = "Audacia", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 14 },
                new() { Nombre = "Grito de guerra", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico, TipoDaño.Magico }, Cooldawn = 120 }
            }
        });

        // FIDDLESTICKS
        lista.Add(new Mago {
            Id = Id.NextId(),
            Nombre = "Fiddlesticks",
            Nivel = 1,
            PrecioEsencias = 1350,
            PoderHabilidad = 138,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Cuervo", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 10 },
                new() { Nombre = "Cosecha", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico, TipoDaño.Curacion }, Cooldawn = 9 },
                new() { Nombre = "Viento cortante", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 12 },
                new() { Nombre = "Tormenta de cuervos", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 140 }
            }
        });

        // MORGANA
        lista.Add(new Mago {
            Id = Id.NextId(),
            Nombre = "Morgana",
            Nivel = 1,
            PrecioEsencias = 1350,
            PoderHabilidad = 120,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Atadura oscura", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 11 },
                new() { Nombre = "Suelo chamuscado", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 14 },
                new() { Nombre = "Escudo negro", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Escudo }, Cooldawn = 23 },
                new() { Nombre = "Cadena de almas", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 120 }
            }
        });

        // KAYLE
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Kayle",
            Nivel = 1,
            PrecioEsencias = 4800,
            Armadura = 84,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Llamas celestiales", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 12 },
                new() { Nombre = "Bendición celestial", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Curacion }, Cooldawn = 15 },
                new() { Nombre = "Juicio divino", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 8 },
                new() { Nombre = "Intervención divina", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 160 }
            }
        });

        // SYLAS
        lista.Add(new Asesino {
            Id = Id.NextId(),
            Nombre = "Sylas",
            Nivel = 1,
            PrecioEsencias = 6300,
            Letalidad = 15,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Golpe de cadena", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 10 },
                new() { Nombre = "Ira de rey", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico, TipoDaño.Curacion }, Cooldawn = 14 },
                new() { Nombre = "Escape", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 14 },
                new() { Nombre = "Abducir", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño>(), Cooldawn = 100 }
            }
        });

        #endregion

        #region =============== NOXUS ===============

        // DARIUS
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Darius",
            Nivel = 1,
            PrecioEsencias = 4800,
            Armadura = 90,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Diezmar", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 9 },
                new() { Nombre = "Golpe demoledor", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 7 },
                new() { Nombre = "Atrapar", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño>(), Cooldawn = 24 },
                new() { Nombre = "Guillotina de Noxus", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.DañoVerdadero }, Cooldawn = 100 }
            }
        });

        // KATARINA
        lista.Add(new Asesino {
            Id = Id.NextId(),
            Nombre = "Katarina",
            Nivel = 1,
            PrecioEsencias = 3150,
            Letalidad = 25,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Hoja giratoria", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 8 },
                new() { Nombre = "Preparación", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 15 },
                new() { Nombre = "Tijeretazo", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 14 },
                new() { Nombre = "Loto mortal", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico, TipoDaño.Fisico }, Cooldawn = 90 }
            }
        });

        // SWAIN
        lista.Add(new Mago {
            Id = Id.NextId(),
            Nombre = "Swain",
            Nivel = 1,
            PrecioEsencias = 4800,
            PoderHabilidad = 130,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Mano de la muerte", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 7 },
                new() { Nombre = "Imperio", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 22 },
                new() { Nombre = "Garra de la muerte", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 13 },
                new() { Nombre = "Invasión de cuervos", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico, TipoDaño.Curacion }, Cooldawn = 120 }
            }
        });

        // TALON
        lista.Add(new Asesino {
            Id = Id.NextId(),
            Nombre = "Talon",
            Nivel = 1,
            PrecioEsencias = 4800,
            Letalidad = 24,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Hoja afilada", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 8 },
                new() { Nombre = "Desfiladero", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 9 },
                new() { Nombre = "Camino furtivo", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño>(), Cooldawn = 2 },
                new() { Nombre = "Emboscada", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 100 }
            }
        });

        // LEBLANC
        lista.Add(new Asesino {
            Id = Id.NextId(),
            Nombre = "LeBlanc",
            Nivel = 1,
            PrecioEsencias = 4800,
            Letalidad = 17,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Mano del asesino", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 6 },
                new() { Nombre = "Distorsión", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 18 },
                new() { Nombre = "Cadenas etéreas", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 14 },
                new() { Nombre = "Huida", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 60 }
            }
        });

        // VLADIMIR
        lista.Add(new Mago {
            Id = Id.NextId(),
            Nombre = "Vladimir",
            Nivel = 1,
            PrecioEsencias = 4800,
            PoderHabilidad = 128,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Transfusión", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico, TipoDaño.Curacion }, Cooldawn = 9 },
                new() { Nombre = "Pecera de sangre", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 28 },
                new() { Nombre = "Marea de sangre", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 13 },
                new() { Nombre = "Plaga", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico, TipoDaño.Curacion }, Cooldawn = 150 }
            }
        });

        // CASSIOPEIA
        lista.Add(new Mago {
            Id = Id.NextId(),
            Nombre = "Cassiopeia",
            Nivel = 1,
            PrecioEsencias = 4800,
            PoderHabilidad = 128,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Explosión nociva", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 3 },
                new() { Nombre = "Miasma", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 24 },
                new() { Nombre = "Golpe doble", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 1 },
                new() { Nombre = "Mirada de piedra", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 130 }
            }
        });

        // RIVEN
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Riven",
            Nivel = 1,
            PrecioEsencias = 4800,
            Armadura = 84,
            HabilidadCampeon = new HashSet<Habilidad> {
                    new() { Nombre = "Golpes rotatorios", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 13 },
                new() { Nombre = "Escudo de valor", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico, TipoDaño.Escudo }, Cooldawn = 11 },
                new() { Nombre = "Empuje", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Escudo }, Cooldawn = 10 },
                new() { Nombre = "Cuchilla cortante", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 120 }
            }
        });

        // SION
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Sion",
            Nivel = 1,
            PrecioEsencias = 1350,
            Armadura = 93,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Golpe devastador", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 10 },
                new() { Nombre = "Grito de batalla", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Escudo }, Cooldawn = 13 },
                new() { Nombre = "Ira del asesino", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 12 },
                new() { Nombre = "Carga imparable", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 140 }
            }
        });

        // SAMIRA
        lista.Add(new Adc {
            Id = Id.NextId(),
            Nombre = "Samira",
            Nivel = 1,
            PrecioEsencias = 6300,
            VelocidadDeAtaque = 0.88,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Fuego floreciente", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 5 },
                new() { Nombre = "Cuchilla giratoria", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 26 },
                new() { Nombre = "Salvaje", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 15 },
                new() { Nombre = "Inferno", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 8 }
            }
        });

        #endregion

        #region =============== JONIA ===============

        // AKALI
        lista.Add(new Asesino {
            Id = Id.NextId(),
            Nombre = "Akali",
            Nivel = 1,
            PrecioEsencias = 4800,
            Letalidad = 20,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Frange de kunai", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 1 },
                new() { Nombre = "Velo crepuscular", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño>(), Cooldawn = 20 },
                new() { Nombre = "Zarpazo shuriken", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico, TipoDaño.Magico }, Cooldawn = 16 },
                new() { Nombre = "Ejecución perfecta", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 120 }
            }
        });

        // AHRI
        lista.Add(new Mago {
            Id = Id.NextId(),
            Nombre = "Ahri",
            Nivel = 1,
            PrecioEsencias = 4800,
            PoderHabilidad = 120,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Orbe del engaño", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico, TipoDaño.DañoVerdadero }, Cooldawn = 7 },
                new() { Nombre = "Fuego de zorro", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 9 },
                new() { Nombre = "Encanto", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 14 },
                new() { Nombre = "Impulso espiritual", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 110 }
            }
        });

        // ASHE
        lista.Add(new Adc {
            Id = Id.NextId(),
            Nombre = "Ashe",
            Nivel = 1,
            PrecioEsencias = 450,
            VelocidadDeAtaque = 0.83,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Flecha de hielo", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 0 },
                new() { Nombre = "Salva", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 14 },
                new() { Nombre = "Halcón de visión", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño>(), Cooldawn = 20 },
                new() { Nombre = "Flecha de hielo encantada", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 100 }
            }
        });

        // IRELIA
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Irelia",
            Nivel = 1,
            PrecioEsencias = 4800,
            Armadura = 83,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Golpe cegador", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 11 },
                new() { Nombre = "Bailarina de cuchillas", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 20 },
                new() { Nombre = "Doble golpe", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 18 },
                new() { Nombre = "Filo de vanguardia", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 140 }
            }
        });

        // KARMA
        lista.Add(new Mago {
            Id = Id.NextId(),
            Nombre = "Karma",
            Nivel = 1,
            PrecioEsencias = 3150,
            PoderHabilidad = 118,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Llama interior", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 8 },
                new() { Nombre = "Vínculo espiritual", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 12 },
                new() { Nombre = "Inspiración", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Escudo }, Cooldawn = 10 },
                new() { Nombre = "Mantra", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño>(), Cooldawn = 40 }
            }
        });

        // KAYN
        lista.Add(new Asesino {
            Id = Id.NextId(),
            Nombre = "Kayn (Asesino)",
            Nivel = 1,
            PrecioEsencias = 6300,
            Letalidad = 21,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Cuchilla de reaper", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 7 },
                new() { Nombre = "Incursión de cuchillas", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 13 },
                new() { Nombre = "Paso de sombra", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño>(), Cooldawn = 21 },
                new() { Nombre = "Infiltración oscura", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 120 }
            }
        });

        // KENNEN (ya incluido en Bandle City)
        // LEE SIN
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Lee Sin",
            Nivel = 1,
            PrecioEsencias = 4800,
            Armadura = 83,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Onda sónica", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 11 },
                new() { Nombre = "Escudo de hierro", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Escudo }, Cooldawn = 14 },
                new() { Nombre = "Tempestad", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 12 },
                new() { Nombre = "Patada de dragón", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 110 }
            }
        });

        // MASTER YI
        lista.Add(new Asesino {
            Id = Id.NextId(),
            Nombre = "Master Yi",
            Nivel = 1,
            PrecioEsencias = 450,
            Letalidad = 24,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Estilo alpha", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 18 },
                new() { Nombre = "Meditación", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Curacion }, Cooldawn = 28 },
                new() { Nombre = "Espadachín", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.DañoVerdadero }, Cooldawn = 18 },
                new() { Nombre = "Highlander", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño>(), Cooldawn = 85 }
            }
        });

        // SYNDRA
        lista.Add(new Mago {
            Id = Id.NextId(),
            Nombre = "Syndra",
            Nivel = 1,
            PrecioEsencias = 4800,
            PoderHabilidad = 138,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Esfera oscura", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 4 },
                new() { Nombre = "Fuerza de voluntad", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 12 },
                new() { Nombre = "Dispersión de débiles", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 18 },
                new() { Nombre = "Poder ilimitado", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 120 }
            }
        });

        // YASUO
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Yasuo",
            Nivel = 1,
            PrecioEsencias = 4800,
            Armadura = 83,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Espada del acero tormenta", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 4 },
                new() { Nombre = "Muro de viento", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño>(), Cooldawn = 26 },
                new() { Nombre = "Cuchilla de aire", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 1 },
                new() { Nombre = "Último aliento", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 80 }
            }
        });

        // YONE
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Yone",
            Nivel = 1,
            PrecioEsencias = 6300,
            Armadura = 84,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Espada del destino", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 4 },
                new() { Nombre = "Espíritu guardian", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico, TipoDaño.Escudo }, Cooldawn = 16 },
                new() { Nombre = "Alma desatada", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.DañoVerdadero }, Cooldawn = 22 },
                new() { Nombre = "Sellar el destino", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 120 }
            }
        });

        // ZED
        lista.Add(new Asesino {
            Id = Id.NextId(),
            Nombre = "Zed",
            Nivel = 1,
            PrecioEsencias = 4800,
            Letalidad = 30,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Navaja arrojadiza", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 6 },
                new() { Nombre = "Sombra viviente", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño>(), Cooldawn = 20 },
                new() { Nombre = "Cuchillada sónica", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 4 },
                new() { Nombre = "Marca de la muerte", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 120 }
            }
        });

        #endregion

        #region =============== SHURIMA ===============

        // AZIR
        lista.Add(new Mago {
            Id = Id.NextId(),
            Nombre = "Azir",
            Nivel = 1,
            PrecioEsencias = 6300,
            PoderHabilidad = 130,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "¡Conquistad!", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 11 },
                new() { Nombre = "¡Alzad!", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 1 },
                new() { Nombre = "¡Arrasad!", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Escudo }, Cooldawn = 19 },
                new() { Nombre = "¡Dividíos!", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 140 }
            }
        });

        // NASUS
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Nasus",
            Nivel = 1,
            PrecioEsencias = 1350,
            Armadura = 87,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Golpe devorador", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 7 },
                new() { Nombre = "Marchitamiento", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 15 },
                new() { Nombre = "Fuego espiritual", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 12 },
                new() { Nombre = "Furia de las arenas", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 120 }
            }
        });

        // RAMMUS
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Rammus",
            Nivel = 1,
            PrecioEsencias = 1350,
            Armadura = 98,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Golpe fuerte", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 16 },
                new() { Nombre = "Postura defensiva", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 6 },
                new() { Nombre = "Moflete burlón", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño>(), Cooldawn = 12 },
                new() { Nombre = "Terremoto", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 110 }
            }
        });

        // RENEKTON
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Renekton",
            Nivel = 1,
            PrecioEsencias = 4800,
            Armadura = 87,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Cortar y trocear", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico, TipoDaño.Curacion }, Cooldawn = 8 },
                new() { Nombre = "Ira del depredador", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 13 },
                new() { Nombre = "Cortar y trocear", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 18 },
                new() { Nombre = "Dominio del rey", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 120 }
            }
        });

        // SIVIR
        lista.Add(new Adc {
            Id = Id.NextId(),
            Nombre = "Sivir",
            Nivel = 1,
            PrecioEsencias = 450,
            VelocidadDeAtaque = 0.86,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Hoja elástica", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 9 },
                new() { Nombre = "Escudo de esteroides", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Escudo }, Cooldawn = 22 },
                new() { Nombre = "Furia de batalla", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 12 },
                new() { Nombre = "Cazadora", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño>(), Cooldawn = 120 }
            }
        });

        // SKARNER
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Skarner",
            Nivel = 1,
            PrecioEsencias = 3150,
            Armadura = 87,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Golpe de cristal", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 4 },
                new() { Nombre = "Escudo de cristal", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Escudo }, Cooldawn = 13 },
                new() { Nombre = "Fractura", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 14 },
                new() { Nombre = "Agarrón", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 120 }
            }
        });

        // TALIYAH
        lista.Add(new Mago {
            Id = Id.NextId(),
            Nombre = "Taliyah",
            Nivel = 1,
            PrecioEsencias = 6300,
            PoderHabilidad = 128,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Pedregal", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 3 },
                new() { Nombre = "Campo de minas", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 16 },
                new() { Nombre = "Geomagnetismo", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 16 },
                new() { Nombre = "Muro de tejedora", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño>(), Cooldawn = 120 }
            }
        });

        // XERATH
        lista.Add(new Mago {
            Id = Id.NextId(),
            Nombre = "Xerath",
            Nivel = 1,
            PrecioEsencias = 4800,
            PoderHabilidad = 145,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Onda de choque", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 7 },
                new() { Nombre = "Ojo de destrucción", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 14 },
                new() { Nombre = "Orbe de hechicería", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 13 },
                new() { Nombre = "Rito del arcángel", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 130 }
            }
        });

        #endregion

        #region =============== ISLAS DE LAS SOMBRAS ===============

        // THRESH
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Thresh",
            Nivel = 1,
            PrecioEsencias = 4800,
            Armadura = 87,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Sentencia", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 20 },
                new() { Nombre = "Paso oscuro", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Escudo }, Cooldawn = 22 },
                new() { Nombre = "Caja de la agonía", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 13 },
                new() { Nombre = "El gancho", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 140 }
            }
        });

        // KALISTA
        lista.Add(new Adc {
            Id = Id.NextId(),
            Nombre = "Kalista",
            Nivel = 1,
            PrecioEsencias = 6300,
            VelocidadDeAtaque = 0.85,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Perforadora", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 8 },
                new() { Nombre = "Centinela", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 30 },
                new() { Nombre = "Salto", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 14 },
                new() { Nombre = "Destino de la lanza", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño>(), Cooldawn = 150 }
            }
        });

        // KARTHUS
        lista.Add(new Mago {
            Id = Id.NextId(),
            Nombre = "Karthus",
            Nivel = 1,
            PrecioEsencias = 3150,
            PoderHabilidad = 150,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Explosión funesta", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 1 },
                new() { Nombre = "Muro de dolor", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 18 },
                new() { Nombre = "Contaminación", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 1 },
                new() { Nombre = "Réquiem", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 200 }
            }
        });

        // MAOKAI
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Maokai",
            Nivel = 1,
            PrecioEsencias = 4800,
            Armadura = 89,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Lanzamiento de semillas", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 8 },
                new() { Nombre = "Empuje retorcido", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 13 },
                new() { Nombre = "Lanzamiento de semillas", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 12 },
                new() { Nombre = "Venganza de la naturaleza", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 120 }
            }
        });

        // HECARIM
        lista.Add(new Luchador {
            Id = Id.NextId(),
            Nombre = "Hecarim",
            Nivel = 1,
            PrecioEsencias = 4800,
            Armadura = 86,
            HabilidadCampeon = new HashSet<Habilidad> {
                new() { Nombre = "Filo devastador", Tecla = TeclaHabilidad.Q, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 4 },
                new() { Nombre = "Miedo", Tecla = TeclaHabilidad.W, Daño = new HashSet<TipoDaño> { TipoDaño.Magico, TipoDaño.Curacion }, Cooldawn = 18 },
                new() { Nombre = "Golpe de devastación", Tecla = TeclaHabilidad.E, Daño = new HashSet<TipoDaño> { TipoDaño.Fisico }, Cooldawn = 20 },
                new() { Nombre = "Sombra de la guerra", Tecla = TeclaHabilidad.R, Daño = new HashSet<TipoDaño> { TipoDaño.Magico }, Cooldawn = 140 }
            }
        });

        #endregion

        // Continúa con más regiones: Freljord, Targon, El Vacío, Ixtal, etc.

        return lista;
    }
}