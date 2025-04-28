using GLMV.Domain.Models;
using GLMV.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

public static class DbMigrationsHelpers
{

    public static async Task EnsureSeedData(WebApplication serviceScope)
    {
        var services = serviceScope.Services.CreateScope().ServiceProvider;
         EnsureSeedData(services);
    }

    public static async Task EnsureSeedData(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (env.IsDevelopment() || env.IsEnvironment("Docker"))
        {
             context.Database.Migrate();

             EnsureSeedProducts(context);
        }
    }

    private static async Task EnsureSeedProducts(AppDbContext context)
    {
        /*   if (context.Categories.Any())
           {
               return;
           }

           var categoria1 = new Category { Id = 1, Description = "Categoria Produtos 1 Geral", Name = "Categoria Produtos 1 Geral", DataCadastro = DateOnly.FromDateTime(DateTime.Now), DataAtualizacao = DateOnly.FromDateTime(DateTime.Now) };

           context.Categories.Add(categoria1);



           var categoria2 = new Category { Id = 2, Description = "Categoria Produtos 2 Geral", Name = "Categoria Produtos 2 Geral", DataCadastro = DateOnly.FromDateTime(DateTime.Now), DataAtualizacao = DateOnly.FromDateTime(DateTime.Now) };

           context.Categories.Add(categoria2);

           context.SaveChanges();

        */

        if (context.Products.Any())
        {
            return;
        }

       

        var product1 = new Product { Title = "Smartphone Samsung Galaxy A54", Description = "Smartphone Samsung Galaxy A54, com tela infinita, 128GB de armazenamento.", Price = 2499.99m, Quantity = 50, CategoryId = 1, ImageUrl = "/images/products/notfound.jpg", SalesPersonId = "7de17ce8-1ebc-40ea-b34d-9fbf2f97b3f3" };
        
        context.Products.Add(product1);
      
        
        var product2 = new Product { Title = "Notebook Dell Inspiron 15 3000", Description = "Notebook Dell Inspiron, processador Intel i5, SSD 512GB, 8GB RAM.", Price = 3799.00m, Quantity = 20, CategoryId = 1, ImageUrl = "/images/products/notfound.jpg", SalesPersonId = "7de17ce8-1ebc-40ea-b34d-9fbf2f97b3f3" };

        context.Products.Add(product2);
      

        var product3 = new Product { Title = "Televisão Samsung 50\" 4K UHD", Description = "Smart TV Samsung Crystal UHD 4K de 50 polegadas, Wi-Fi integrado.", Price = 2899.00m, Quantity = 15, CategoryId = 1, ImageUrl = "/images/products/notfound.jpg", SalesPersonId = "7de17ce8-1ebc-40ea-b34d-9fbf2f97b3f3" };

        context.Products.Add(product3);
      

        var product4 = new Product { Title = "Cafeteira Nespresso Essenza Mini", Description = "Cafeteira Nespresso de cápsulas, compacta e eficiente para cafés rápidos.", Price = 499.90m, Quantity = 100, CategoryId = 1, ImageUrl = "/images/products/notfound.jpg", SalesPersonId = "7de17ce8-1ebc-40ea-b34d-9fbf2f97b3f3" };

        context.Products.Add(product4);
        
        context.SaveChanges();

    }

}