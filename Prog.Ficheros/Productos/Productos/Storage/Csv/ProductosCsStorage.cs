using System.Text;
using Productos.Dto;
using Productos.Mappers;
using Productos.Models;

namespace Productos.Storage.Csv;

public class ProductosCsStorage : IProductosCsv {
    public ProductosCsStorage() {
        InitStorage();
    }
    
    public void Salvar(IEnumerable<Producto> items, string path) {
        try {
            using var writer = new StreamWriter(path, false, Encoding.UTF8);
            //Cabezera Csv
            writer.WriteLine("id,name,supplier,category,unitPrice,unitsInStock");
            items.Select(p => p.ToDto())
                .ToList()
                .ForEach(dto => {
                    writer.WriteLine(
                        $"{dto.Id}," +
                        $"{dto.Name}," +
                        $"{dto.Supplier}," +
                        $"{dto.Categoria}," +
                        $"{dto.UnitPrice}," +
                        $"{dto.UnitInPrice}"
                    );
                });
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }

    public IEnumerable<Producto> Carrgar(string path) {
        if (!Path.Exists(path)) {
            return [];
        }

        try {
            return File.ReadLines(path, Encoding.UTF8)
                .Skip(1)
                .Select(l => l.Split(','))
                .Select(c => new ProductosDto(
                    int.Parse(c[0]),
                    c[1],
                    int.Parse(c[2]),
                    int.Parse(c[3]),
                    double.Parse(c[5]),
                    int.Parse(c[6], System.Globalization.CultureInfo.InvariantCulture)
                ).ToModels());
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }

    private void InitStorage() {
        if (Directory.Exists("Da")) {
            return;
        }

        Directory.CreateDirectory("data");
}