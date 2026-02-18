using Ficha.Collections.Lista;
using Lol.Models;

namespace Lol.Service;

public interface ICampeonService {
    int totalCampeones { get; }
    
    ILista<Campeon> GetAll();
    
    Campeon GetById(int id);
    
    Campeon GetByNombre(string nombre);
    
    Campeon Save(Campeon campeon);
    
    Campeon Update(Campeon campeon, int id);
    
    Campeon Delete(Campeon campeon);
    
    ILista<Campeon> GetAllOrderBy(TipoOrdenamiento ordenamiento);
    
    ILista<Asesino> GetAsesinosOrderBy(TipoOrdenamiento ordenamiento);
    
    ILista<Mago>  GetMagosOrderBy(TipoOrdenamiento ordenamiento);
    
    ILista<Luchador>  GetLuchadorsOrderBy(TipoOrdenamiento ordenamiento);
    
    ILista<Adc>  GetAdcsOrderBy(TipoOrdenamiento ordenamiento);
    
    
}