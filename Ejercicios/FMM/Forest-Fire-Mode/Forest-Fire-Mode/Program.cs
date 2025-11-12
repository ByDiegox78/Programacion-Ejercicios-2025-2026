using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Forest_Fire_Mode;


var rand = new Random();
const int Vacio = 0;
const int Arbol = 1;
const int Ardiendo = 2;

const double Parder = 0.00006;
const double PCrecer = 0.01;

const int TiempoMaximo = 60;
const int Filas = 20;
const int Columnas = 20;
Console.OutputEncoding = Encoding.UTF8;

Main(args);

void Main(string[] args) {
   Configuracion config;
config.Filas = Filas;
config.Columnas = Columnas;

var celdasArdidas = 0;
int arbolesNacidos = 0; 
int celdasFinalVacia = 0;
int celdasFinalArbol = 0;
int celdasFinalArdiendo = 0;


//Inicializamos el frontBuffer
var frontBuffer = new int[config.Filas, config.Columnas];



//Inicializamos el backbuffer con el CloneMatrix que nos da una nueva matriz
//Con el contenido del frontbuffer
var backBuffer = CloneMatrix(frontBuffer, config.Filas, config.Columnas);

//Variable del tiempo
var tiempoRestante = TiempoMaximo;

//Metemos los arboles en el frontBuffer para la simulacion
InitForest(frontBuffer, config.Filas, config.Columnas);

//Bucle while que se ejecuta hasta q el tiempo llegue a 0
//Se va a cumplir porque a cada vuelta se reste 1
while(tiempoRestante != 0) {
    Console.WriteLine();
    
    //Imprimimos la matriz frontBuffer que es la que siempreVeremos
    PrintMatrix(frontBuffer,  config.Filas, config.Columnas);
    Console.WriteLine("-------------------------------------------------------------------------------");
    
    //Llamamos a step para que implemente las reglas al backBuffer
    Step(frontBuffer, backBuffer, config.Filas, config.Columnas,ref celdasArdidas, ref arbolesNacidos);

    //El corazon del doble buffer donde cambiamos el backbuffer modificado al frontbuffer
    //y el backbuffer coge los datos del frontBuffer
    var temp = frontBuffer;
    frontBuffer = backBuffer;
    backBuffer = temp;
    
    //Thread.Sleep(1000);
    tiempoRestante--;
}

for (int i = 0; i < config.Filas; i++) {
    for (int j = 0; j < config.Columnas; j++) {
        switch (frontBuffer[i, j]) {
            case Vacio: celdasFinalVacia++; break;
            case Arbol: celdasFinalArbol++; break;
            case Ardiendo: celdasFinalArdiendo++; break;
            
        }
    }
}
Console.WriteLine($"La cantidad de celdas ardidas es de: {celdasArdidas}");
Console.WriteLine($"La cantidad de celdas nacidas es de: {arbolesNacidos}");
Console.WriteLine($"La cantidad de celdas final arbol es de: {celdasFinalArbol}");
Console.WriteLine($"La cantidad de celdas final ardiendo es de: {celdasFinalArdiendo}");
Console.WriteLine($"La cantidad de celdas final vacias es de: {celdasFinalVacia}"); 
}

/*
 * Start Main
 */

/*
* Funcion de clonacion para el backbuffer con copia profunda O(n2)
*/
int[,] CloneMatrix(int[,] matrix, int filas, int columnas) {
    //Creo una matriz nueva del mismo tamaño
    var newMatrix = new int[filas, columnas]; 
    
    for (int i = 0; i < filas; i++) {
        for (int j = 0; j < columnas; j++) {
            //Copiamos el valor de la matriz original en la copia 
            newMatrix[i, j] = matrix[i, j]; 
        }
    }
    //Devolvemos la matriz independiente de la original
    return newMatrix; 
}
/*
 * Procedimiento de iniciañizacion de inicializacion del frontbuffer
 */


void InitForest(int[,] matrix, int filas, int columnas) {
    var porcentaje = rand.Next(30,81);
    var totalCeldas = filas * columnas;
    var totalArboles = (int)Math.Round(totalCeldas * porcentaje / 100.0);
    
    
    for(int i = 0; i < totalArboles; i++) {
        bool colocado = false;
        //bucle que nos sirve para que tire randoms sin parar hasta q encuentre un 
        //espacio vaci donde colocar un arbor
        while(!colocado) {
            var f = rand.Next(0,filas);
            var c = rand.Next(0,columnas);

            if(matrix[f,c] == Vacio) {
                matrix[f, c] = Arbol;
                colocado = true;
            }
        }
    }
}
/*
 * Procedimiento que imprime la matriz con los iconos con un switch
 */
void PrintMatrix(int[,] matrix, int filas, int columnas) {
    for (int i = 0; i < filas; i++) {
        for (int j = 0; j < columnas; j++) {
            switch (matrix[i,j]) {
                case Arbol: Console.Write("[🌳]"); break;
                case Vacio: Console.Write("[  ]"); break;
                case Ardiendo: Console.Write("[🔥]"); break;
            }
        }
        Console.WriteLine(""); 
    }
}


/*
 * Funcion que devuelve un boleano si encuentra un vecino que esta ardiendo
 * En cualquier direccion sin salirse de los limites
 */
bool HasBurningNeighbour(int[,] matrix, int i, int j, int filas, int columnas) {

    var isBurning = (
        (i > 0 && j > 0 && matrix[i-1,j-1] == Ardiendo) ||
        (i > 0 && matrix[i-1,j] == Ardiendo) || 
        (i > 0 && j < columnas - 1 && matrix[i-1,j+1] == Ardiendo) ||
        (j > 0 && matrix[i,j-1] == Ardiendo) ||
        (j < columnas - 1 && matrix[i,j+1] == Ardiendo) ||
        (i < filas - 1 && j > 0 && matrix[i+1,j-1] == Ardiendo) ||
        (i < filas - 1 && matrix[i+1,j] == Ardiendo) ||
        (i < filas - 1 && j < columnas - 1 && matrix[i+1,j+1] == Ardiendo)
    );
    
    return isBurning;
} 
/*
 * Procedimiento que añade estados nuevos al backbuffer segun las 4 reglas que se pide en el enunciado
*/
void Step(int[,] frontMatrix, int[,] backMatrix, int filas, int columnas, ref int celdasBurning, ref int celdasHasGrow) {
    for (int i = 0; i < filas; i++) {
        for (int j = 0; j < columnas; j++) {
            
            // 1. Ardiendo --> Vacía: Una celda que está ardiendo se convierte en un espacio vacío en el siguiente paso.
            if (frontMatrix[i, j] == Ardiendo) {
                backMatrix[i, j] = Vacio; 
                // 2. Árbol --> Ardiendo (Contagio): Un árbol arderá si al menos uno de sus vecinos adyacentes (8 direcciones) está ardiendo.    
                
            } else if (frontMatrix[i, j] == Arbol) {
                if (HasBurningNeighbour(frontMatrix, i, j, filas, columnas)) {
                    backMatrix[i, j] = Ardiendo;
                    celdasBurning++;
                    
                    // 3. Árbol --> Ardiendo (Espontáneo): Un árbol comienza a arder con una probabilidad incluso si no tiene ningún vecino ardiendo.
                    //Hacemos el else dentro del del if de HasBurningNeighbour porque si no tiene vecino lanza el randompara ver si arde    
                } else {
                    
                    if (rand.NextDouble() < Parder) {
                        backMatrix[i, j] = Ardiendo;
                        
                    // Si no se da ninguna opcion debemos actualizar el estado en el backBuffer 
                    } else {
                        backMatrix[i, j] = Arbol;
                    }
                }
                // 4. Vacía --> Árbol: Un árbol brota en un espacio vacío con una probabilidad PCRECER
                
            } else if (frontMatrix[i, j] == Vacio) {
                double randomValue = rand.NextDouble();
                if (randomValue < PCrecer) {
                    backMatrix[i, j] = Arbol;
                    celdasHasGrow++;

                }
                //Si no se da la probabilidad actualizamos elbackBuffer
                else {
                    backMatrix[i, j] = Vacio;
                }
            }
        }
    }
}
/*
 * El porque se usa el SWAP O(1) es porque se intercambian los punteros de ambas matrizes. Y como son
 * 60 ciclos solo se cambian 3 referencias por ciclo y no copiamos el valor celda a celda que seria completamente ineficiente
 *
 * 
 *
 * Mientras que en la clonacion usamos la otra O(n2) porque necesitamos copiar el valor
 * de cada de la matriz y como solo se hace 1 vez es una buena practica
 */
