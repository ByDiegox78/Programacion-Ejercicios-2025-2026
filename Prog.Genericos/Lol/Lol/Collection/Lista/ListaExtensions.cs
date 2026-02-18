namespace Ficha.Collections.Lista;

public static class ListaExtensions {

    extension<T>(ILista<T> lista) where T : class {
        
        public ILista<T> Where(Predicate<T> predicado) {
            var resultado = new Lista<T>();
            foreach (var elemento in lista)
                if (predicado(elemento))
                    resultado.AgregarFinal(elemento);

            return resultado;
        }

        public T? Find(Predicate<T> predicado) {
            foreach (var elemento in lista) {
                if (predicado(elemento)) {
                    return elemento;
                }
            }
            return null;
        }

        public int Count(Predicate<T> predicado) {
            var contador = 0;
            foreach (var elemento in lista) {
                if (predicado(elemento)) contador++;
            }
            return contador;
        }

        public ILista<K> Select<K>(Func<T, K> selector) {
            var resultado = new Lista<K>();
            foreach (var elemento in lista) resultado.AgregarFinal(selector(elemento));
            return resultado;
        }

        public int Sum(Func<T, int> selector) {
            var suma = 0;
            foreach (var elemento in lista)
                suma += selector(elemento);
            return suma;
        }

        public int Aggregate(Func<int, T, int> acumulador, int valorInicial) {
            var res = valorInicial;
            foreach (var elemento in lista)
                res = acumulador(res, elemento);
            return res;
        }

        public void ForEach(Action<T> accion) {
            foreach (var elemento in lista) {
                accion(elemento);
            }
        }
        
        public bool Any(Predicate<T> predicado) {
            foreach (var elemento in lista)
                if (predicado(elemento))
                    return true;
            return false;
        }
        
        public bool All(Predicate<T> predicado) {
            foreach (var elemento in lista)
                if (!predicado(elemento))
                    return false;
            return true;
        }
        
        public int FindIndex(Predicate<T> predicado) {
            int index = 0;
            foreach (var elemento in lista) {
                if (predicado(elemento)) return index;
                index++;
            }
            return -1;
        }
    }
    
    
}