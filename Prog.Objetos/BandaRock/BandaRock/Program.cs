// See https://aka.ms/new-console-template for more information

using BandaRock.Factory;

Main();

void Main() {
    var lista = BandaFactory.DemoDataMejorado();

    foreach (var item in lista) {
        Console.WriteLine(item);
    }
}