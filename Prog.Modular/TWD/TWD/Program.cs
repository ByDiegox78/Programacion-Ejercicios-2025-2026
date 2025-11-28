
using System.Text;
using TWD.Enums;
using TWD.Struct;

const int DimensionesPorDefecto = 15;
const int InfectadosPorDefecto = 10;
const int SanosPorDefecto = 100;
const int PContagio = 35; // Sobre 100
//const int Tiempo = 100;
const int PMuerte = 15;
//const int PMatar = 5;

var rand = new Random();
Console.OutputEncoding = Encoding.UTF8;
Main(args);

void Main(string[] args) {
    Configuracion config = new Configuracion();
    config.Fila = DimensionesPorDefecto;
    config.Columna = DimensionesPorDefecto;
    
    Tipo[,] frontBuffer = new Tipo[config.Fila, config.Columna];
    var backbuffer = CloneMatrix(frontBuffer, config);
    InitMatriz(frontBuffer, config);
    PrintMatrix(frontBuffer, config);

    int contador = 5;
    while (contador > 0) {
        Step(frontBuffer, backbuffer, config);
        Console.WriteLine("");
        PrintMatrix(frontBuffer, config);
        Console.WriteLine("");
        (frontBuffer, backbuffer) = (backbuffer, frontBuffer);
        contador--;
    }
    Console.WriteLine("\nPresiona cualquier tecla para salir...");
    Console.ReadKey();
};

void InitMatriz(Tipo[,] frontBuffer, Configuracion config) {
    int zombiesColocados = 0;
    int sanosColocados = 0;
    for (int i = 0; i < config.Fila; i++)
    for (int j = 0; j < config.Columna; j++)
        frontBuffer[i, j] = Tipo.Libre;
    
    while (zombiesColocados < InfectadosPorDefecto) {
        var f = rand.Next(0, config.Fila);
        var c = rand.Next(0, config.Columna);
        if (frontBuffer[f, c] != Tipo.Zombie) {
            frontBuffer[f, c] = Tipo.Zombie;
            zombiesColocados++;
        }
    }

    while (sanosColocados < SanosPorDefecto) {
        var f = rand.Next(0, config.Fila);
        var c = rand.Next(0, config.Columna);
        if (frontBuffer[f, c] != Tipo.Zombie && frontBuffer[f, c] != Tipo.Sano) {
            frontBuffer[f, c] = Tipo.Sano;
            sanosColocados++;
        }
    }
}

void PrintMatrix(Tipo[,] matrix, Configuracion config) {
    
    for (int i = 0; i < config.Fila; i++) {
        for (int j = 0; j < config.Columna; j++) {
            switch (matrix[i,j]) {
                case Tipo.Zombie: Console.Write("[🧟]"); break;
                case Tipo.Libre: Console.Write("[  ]"); break;
                case Tipo.Sano: Console.Write("[🤵]"); break;
            }
        }
        Console.WriteLine(""); 
    }
}
Tipo[,] CloneMatrix(Tipo[,] front, Configuracion config) {
    //Creo una matriz nueva del mismo tamaño
    Tipo[,] newMatrix = new Tipo[config.Fila, config.Columna]; 
    
    for (int i = 0; i < config.Fila; i++) {
        for (int j = 0; j < config.Columna; j++) {
            //Copiamos el valor de la matriz original en la copia 
            newMatrix[i, j] = front[i, j]; 
        }
    }
    //Devolvemos la matriz independiente de la original
    return newMatrix; 
}




void Step(Tipo[,] frontMatrix, Tipo[,] backMatrix, Configuracion config) {
    for (int i = 0; i < config.Fila; i++) {
        for (int j = 0; j < config.Columna; j++) {
            
            // CONFIGURACION DEL ZOMBIE
            if (frontMatrix[i, j] == Tipo.Zombie) {
                FuncionalidadZombie(frontMatrix, backMatrix, config, i, j);
            } else if (frontMatrix[i, j] == Tipo.Sano) {
                FuncionalidadSano(frontMatrix, backMatrix, config, i, j);
            }
        }
    }
}

void FuncionalidadZombie(Tipo[,] front, Tipo[,] back, Configuracion config, int i, int j) {
    var random = rand.Next(0, 100);
    if (random < PMuerte) {
        back[i, j] = Tipo.Libre;
    } else {
        if (HasNeighbourFree(front, i, j, config)) { //Busco si hay una posicion vacia
            ColocarAdyacente(front, i, j, out int newI, out int newJ);
            back[i, j] = Tipo.Libre;
            back[newI, newJ] = Tipo.Zombie;
        } else { //Si no hay posicion vacia se queda quieto
            back[i, j] = Tipo.Zombie;
            
        }

        
    }
}




//Busco que haya al menos una posicion vacia
bool HasNeighbourFree(Tipo[,] front, int fZ, int cZ, Configuracion config) {
    for (var i = -1; i <= 1; i++) {
        for (var j = -1; j <= 1; j++) {
            if (i == 0 && j == 0) continue; 
            int f = fZ + i;
            int c = cZ + j;
            if (f >= 0 && f < config.Fila &&
                c >= 0 && c < config.Columna) {
                if (front[f, c] == Tipo.Libre) {
                    return true; 
                }
            }
        }
    }
    return false;
}
void HasNeighbourAlive(Tipo[,] front,Tipo[,] back, int fZ, int cZ) {
    for (var i = -1; i <= 1; i++) {
        for (var j = -1; j <= 1; j++) {
            if (i == 0 && j == 0) continue; 
            int f = fZ + i;
            int c = cZ + j;
            if (IsPosicionValida(front, f, c)&& front[f, c] == Tipo.Sano ) {
                int random = rand.Next(0, 100);
                if (random < PContagio) {
                    back[f, c] = Tipo.Zombie;
                    return;
                }
            }
        }
    }
    
}
bool ColocarAdyacente(Tipo[,] frontMatrix,int fZ, int cZ, out int newF, out int newC ) {
    int nuevaF;
    int nuevaC;
    int intentos = 8;
    newF = fZ;
    newC = cZ;
    
    while (intentos > 0) {
        SortearPosicionAleatorea(fZ, cZ, out nuevaF, out nuevaC);
        if (IsPosicionValida(frontMatrix, nuevaF, nuevaC)) {
            if (frontMatrix[nuevaF, nuevaC] == Tipo.Libre) {
                newF = nuevaF;
                newC = nuevaC;
                return true;
            }
        }
        intentos--;
    }
    return false;
}
//Sorteo una posicion aleatorea
void SortearPosicionAleatorea(int fZ, int cZ, out int newF, out int newC) {
    int f;
    int c;
    do {
        f = rand.Next(-1, 2);
        c = rand.Next(-1, 2);
    } while (f == 0 && c == 0);
    
    newF = fZ + f;
    newC = cZ + c;
}
bool IsPosicionValida(Tipo[,] front, int fila, int columna) {
    return fila >= 0 && fila < front.GetLength(0) && columna >= 0 && columna < front.GetLength(1);
}
