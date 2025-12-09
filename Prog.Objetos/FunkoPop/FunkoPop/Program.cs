using System;
using FunkoPop.Enums;
using FunkoPop.Models;
using FunkoPop.Repository;
using FunkoPop.Service;
using FunkoPop.Utils;
using FunkoPop.Validator;

Main();

void Main() {
    // Creamos la instancia del repositorio (singleton)
    var repository = FunkoRepository.GetInstance();

    // Creamos el validador (aunque no se use en este ejemplo)
    var validator = new FunkoValidator();

    // Creamos el service
    var service = new FunkoService(repository, validator);

    OpcionMenu menu;
    do {
        menu = (OpcionMenu)int.Parse(Utilities.ValidarMenu("--- Elije una opcion ---", FunkoValidator.RegexMenu));
        switch (menu) {
            case (int)OpcionMenu.Salir:
                break;
            case (int)OpcionMenu.MostrarTodos:
                service.GetAllFunko(service);
                break;
        }
    } while (menu != OpcionMenu.Salir);
}
