using GestionItv.Models;

namespace GestionItv.Factory.Vehiculos;

public static class VehiculosFactory {
    public static IEnumerable<Vehiculo> Seed() {
        var now = DateTime.Now;
        return new List<Vehiculo> {
            new(0, "1234BCD", "Seat Ibiza", 1200, Motor.Gasolina, "12345678Z", false, now, now),
            new(0, "2345BCF", "Volkswagen Golf", 1600, Motor.Diesel, "23456789M", false, now, now),
            new(0, "3456BCG", "Toyota Corolla", 1800, Motor.Hibrido, "34567890A", false, now, now),
            new(0, "4567BCH", "Tesla Model 3", 0, Motor.Electrico, "45678901S", false, now, now),
            new(0, "5678BCJ", "Ford Focus", 1500, Motor.Gasolina, "56789012R", false, now, now),
            new(0, "6789BCK", "BMW Serie 3", 2000, Motor.Diesel, "67890123W", false, now, now),
            new(0, "7890BCL", "Audi A4", 2000, Motor.Hibrido, "78901234A", false, now, now),
            new(0, "8901BCM", "Mercedes C", 2200, Motor.Gasolina, "89012345B", false, now, now),
            new(0, "9012BCN", "Nissan Leaf", 0, Motor.Electrico, "90123456C", false, now, now),
            new(0, "0123BCP", "Kia Ceed", 1400, Motor.Gasolina, "01234567D", false, now, now),
            new(0, "1234BCQ", "Hyundai i30", 1600, Motor.Diesel, "11234568E", false, now, now),
            new(0, "2345BCR", "Renault Clio", 1200, Motor.Hibrido, "21234569F", false, now, now),
            new(0, "3456BCS", "Peugeot 208", 1300, Motor.Gasolina, "31234570G", false, now, now),
            new(0, "4567BCT", "Citroen C3", 1400, Motor.Diesel, "41234571H", false, now, now),
            new(0, "5678BCV", "Opel Astra", 1600, Motor.Gasolina, "51234572J", false, now, now),
            new(0, "6789BCW", "Mazda 3", 1800, Motor.Hibrido, "61234573K", false, now, now),
            new(0, "7890BCX", "Honda Civic", 2000, Motor.Gasolina, "71234574L", false, now, now),
            new(0, "8901BCY", "Suzuki Swift", 1200, Motor.Gasolina, "81234575M", false, now, now),
            new(0, "9012BCZ", "Ford Fiesta", 1400, Motor.Diesel, "91234576N", false, now, now),
            new(0, "0123BDC", "Volkswagen Polo", 1200, Motor.Gasolina, "10234567P", false, now, now),
            new(0, "1234BDD", "Toyota Yaris", 1000, Motor.Hibrido, "20234568Q", false, now, now),
            new(0, "2345BDF", "Tesla Model Y", 0, Motor.Electrico, "30234569R", false, now, now),
            new(0, "3456BDG", "BMW X1", 2000, Motor.Diesel, "40234570S", false, now, now),
            new(0, "4567BDH", "Audi Q3", 1800, Motor.Gasolina, "50234571T", false, now, now),
            new(0, "5678BDJ", "Mercedes GLA", 2000, Motor.Hibrido, "60234572U", false, now, now),
            new(0, "6789BDK", "Kia Sportage", 1600, Motor.Diesel, "70234573V", false, now, now),
            new(0, "7890BDL", "Hyundai Tucson", 1800, Motor.Gasolina, "80234574W", false, now, now),
            new(0, "8901BDM", "Renault Captur", 1400, Motor.Hibrido, "90234575X", false, now, now),
            new(0, "9012BDN", "Peugeot 3008", 2000, Motor.Diesel, "01234576Y", false, now, now),
            new(0, "0123BDP", "Nissan Qashqai", 1600, Motor.Gasolina, "11234577Z", false, now, now)
        };
    }
}