using System.Diagnostics;
using Ficha.Collections.Lista;
using Ficha.Repository.Dvd;
using Ficha.Repository.Libro;
using Ficha.Repository.Revista;
using Ficha.Validator.Dvd;
using Ficha.Validator.LibroValidate;
using Ficha.Validator.RevistasValidator;

namespace Ficha.Service;
using Ficha.Models;

public class FichaService(
    IDvdRepository dvdRepository,
    ILibrosRepository librosRepository,
    IRevistasRepository revistaRepository,

    IRevistaValidate revistaValidate,
    IDvdValidate dvdValidate,
    ILibroValidate libroValidate
) : IFichaService {


    //Service de Dvd
    public int TotalDvd { get; } = dvdRepository.TotalDvd;

    public Dvd GetDvdByDirector(string director) {
        return dvdRepository.GetDvdByDirector(director) ?? 
               throw new KeyNotFoundException($"No se encontró el dvd con Director: {director}");
    }
    public Dvd GetByIdDvd(int id) {
        return dvdRepository.GetById(id) ??
               throw new KeyNotFoundException($"No se encontró el dvd con ID: {id}");
    }

    public ILista<Dvd> GetAllDvd() {
        return dvdRepository.GetAll();
    }
    
    public Dvd SaveDvd(Dvd dvd) {
        var dvdSaved = dvdValidate.Validate(dvd);
        return dvdRepository.Create(dvdSaved) ??
               throw new ArgumentException(
                   $"No se pudo guardar el alumno con DNI {dvdSaved.Id}, puede que ya exista");
    }

    
    public Dvd UpdateDvd(Dvd dvd) {
        var dvdSaved = dvdValidate.Validate(dvd);
        return dvdRepository.Update(dvdSaved, dvdSaved.Id) ??
               throw new KeyNotFoundException($"Dvd con ID {dvdSaved.Id} no encontrado para actualización.");
    } 
    public Dvd DeleteDvd(int id) {
        return dvdRepository.Delete(id) ??
               throw new KeyNotFoundException($"Dvd con ID {id} no encontrado para eliminar.");
    }
    //Libro Service
    public Libro GetLibroByAutor(string autor) {
        return librosRepository.GetLibroByAutor(autor) ?? 
               throw new KeyNotFoundException($"No se encontró el libro con Autor: {autor}");
    }
    public int TotalLibros { get; } = librosRepository.TotalLibro;
    
    public ILista<Libro> GetAllLibro() {
        return librosRepository.GetAll();
    }

    public Libro GetByIdLibro(int id)
    {
        return librosRepository.GetById(id) ??
               throw new KeyNotFoundException($"No se encontró el libro con ID: {id}");
    }

    public Libro SaveLibro(Libro libro)
    {
        var libroSaved = libroValidate.Validate(libro);

        return librosRepository.Create(libroSaved) ??
               throw new ArgumentException(
                   $"No se pudo guardar el libro con ID {libroSaved.Id}, puede que ya exista");
    }

    public Libro UpdateLibro(Libro libro)
    {
        var libroSaved = libroValidate.Validate(libro);

        return librosRepository.Update(libroSaved, libroSaved.Id) ??
               throw new KeyNotFoundException(
                   $"Libro con ID {libroSaved.Id} no encontrado para actualización.");
    }

    public Libro DeleteLibro(int id)
    {
        return librosRepository.Delete(id) ??
               throw new KeyNotFoundException(
                   $"Libro con ID {id} no encontrado para eliminar.");
    }
    
    //Service Resvistas
    
    public int TotalRevistas { get; } = revistaRepository.TotalRevista;
    
    public ILista<Revista> GetAllRevista() {
        return revistaRepository.GetAll();
    }

    public Revista GetrevistaByNumber(int number) {
        return revistaRepository.GetRevistaByNumber(number) ?? 
               throw new KeyNotFoundException($"No se encontró la revista con numero: {number}");
    }
    
    public Revista GetByIdRevista(int id) {
        return revistaRepository.GetById(id) ??
               throw new KeyNotFoundException($"No se encontró la revista con ID: {id}");
    }

    public Revista SaveRevista(Revista revista) {
        var revistaSaved = revistaValidate.Validate(revista);

        return revistaRepository.Create(revistaSaved) ??
               throw new ArgumentException(
                   $"No se pudo guardar la revista con ID {revistaSaved.Id}, puede que ya exista");
    }

    public Revista UpdateRevista(Revista revista) {
        var revistaSaved = revistaValidate.Validate(revista);

        return revistaRepository.Update(revistaSaved, revistaSaved.Id) ??
               throw new KeyNotFoundException(
                   $"Revista con ID {revistaSaved.Id} no encontrada para actualización.");
    }
    public Revista DeleteRevista(int id) {
        return revistaRepository.Delete(id) ??
               throw new KeyNotFoundException(
                   $"Revista con ID {id} no encontrada para eliminar.");
    }


}
