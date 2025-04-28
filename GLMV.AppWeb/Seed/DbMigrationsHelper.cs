using GLMV.Domain.Models;
using GLMV.Infra.Data;
using Microsoft.EntityFrameworkCore;

public static class DbMigrationsHelpers
{

    public static async Task EnsureSeedData(WebApplication serviceScope)
    {
        var services = serviceScope.Services.CreateScope().ServiceProvider;
        await EnsureSeedData(services);
    }

    public static async Task EnsureSeedData(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (env.IsDevelopment() || env.IsEnvironment("Docker"))
        {
            await context.Database.MigrateAsync();

            await EnsureSeedProducts(context);
        }
    }

    private static async Task EnsureSeedProducts(AppDbContext context)
    {
        if (context.Products.Any())
        {
            return;
        }

        var product1 = new Product { Title = "Smartphone Samsung Galaxy A54", Description = "Smartphone Samsung Galaxy A54, com tela infinita, 128GB de armazenamento.", Price = 2499.99m, Quantity = 50, CategoryId = 1, ImageUrl = "/images/products/default1.jpg", SalesPersonId = "1" };
        
        context.Products.Add(product1);
        context.SaveChangesAsync();
        
        var product2 = new Product { Title = "Notebook Dell Inspiron 15 3000", Description = "Notebook Dell Inspiron, processador Intel i5, SSD 512GB, 8GB RAM.", Price = 3799.00m, Quantity = 20, CategoryId = 2, ImageUrl = "/images/products/default2.jpg", SalesPersonId = "1" };

        context.Products.Add(product2);
        context.SaveChangesAsync();

        var product3 = new Product { Title = "Televisão Samsung 50\" 4K UHD", Description = "Smart TV Samsung Crystal UHD 4K de 50 polegadas, Wi-Fi integrado.", Price = 2899.00m, Quantity = 15, CategoryId = 3, ImageUrl = "/images/products/default3.jpg", SalesPersonId = "1" };

        context.Products.Add(product3);
        context.SaveChangesAsync();

        var product4 = new Product { Title = "Cafeteira Nespresso Essenza Mini", Description = "Cafeteira Nespresso de cápsulas, compacta e eficiente para cafés rápidos.", Price = 499.90m, Quantity = 100, CategoryId = 4, ImageUrl = "/images/products/default4.jpg", SalesPersonId = "1" };

        context.Products.Add(product4);
        context.SaveChangesAsync();
        
        var product5 = new Product { Title = "Console PlayStation 5", Description = "Console Sony PlayStation 5, ultra velocidade SSD e jogos imersivos.", Price = 4999.99m, Quantity = 10, CategoryId = 5, ImageUrl = "/images/products/default5.jpg", SalesPersonId = "1" };

        context.Products.Add(product5);
        context.SaveChangesAsync();
        var product6 = new Product { Title = "Câmera Canon EOS Rebel T7", Description = "Câmera digital Canon, ideal para iniciantes, com Wi-Fi embutido.", Price = 2999.00m, Quantity = 25, CategoryId = 6, ImageUrl = "/images/products/default6.jpg", SalesPersonId = "1" };

        context.Products.Add(product6);
        context.SaveChangesAsync();
        var product7 = new Product { Title = "Fone de Ouvido JBL Tune 510BT", Description = "Fone de ouvido sem fio JBL, bateria de até 40 horas, som potente.", Price = 249.90m, Quantity = 75, CategoryId = 7, ImageUrl = "/images/products/default7.jpg", SalesPersonId = "1" };


        context.Products.Add(product7);
        context.SaveChangesAsync();
        var product8 = new Product { Title = "Relógio Smartwatch Amazfit Bip U", Description = "Relógio inteligente Amazfit, monitoramento de saúde e atividades físicas.", Price = 349.90m, Quantity = 30, CategoryId = 8, ImageUrl = "/images/products/default8.jpg", SalesPersonId = "1" };

        context.Products.Add(product8);
        context.SaveChangesAsync();
        var product9 =new Product { Title = "Aspirador de Pó Robô Roomba", Description = "Aspirador de pó robô iRobot Roomba com navegação inteligente.", Price = 1999.90m, Quantity = 12, CategoryId = 9, ImageUrl = "/images/products/default9.jpg", SalesPersonId = "1" };

        context.Products.Add(product9);
        context.SaveChangesAsync();
        var product10 =new Product { Title = "Tablet Samsung Galaxy Tab A8", Description = "Tablet Samsung de 10,5\", processador octa-core e bateria de longa duração.", Price = 1499.90m, Quantity = 40, CategoryId = 10, ImageUrl = "/images/products/default10.jpg", SalesPersonId = "1" };

        context.Products.Add(product10);
        context.SaveChangesAsync();
        var product11 =new Product { Title = "Caixa de Som Bluetooth JBL Flip 5", Description = "Caixa de som portátil JBL, resistente à água, até 12 horas de reprodução.", Price = 599.90m, Quantity = 60, CategoryId = 7, ImageUrl = "/images/products/default11.jpg", SalesPersonId = "1" };

        context.Products.Add(product11);
        context.SaveChangesAsync();
        var product12 = new Product { Title = "Monitor Gamer AOC 24\" 144Hz", Description = "Monitor gamer Full HD 24 polegadas AOC, taxa de atualização 144Hz.", Price = 1299.90m, Quantity = 22, CategoryId = 2, ImageUrl = "/images/products/default12.jpg", SalesPersonId = "1" };

        context.Products.Add(product12);
        context.SaveChangesAsync();
        var product13 =new Product { Title = "Kindle Paperwhite 11ª Geração", Description = "E-reader Kindle Paperwhite à prova d'água, iluminação embutida ajustável.", Price = 649.90m, Quantity = 18, CategoryId = 12, ImageUrl = "/images/products/default14.jpg", SalesPersonId = "1" };
        
        context.Products.Add(product13);
        context.SaveChangesAsync();
        var product14 =new Product { Title = "Teclado Mecânico Redragon Kumara", Description = "Teclado mecânico Redragon Kumara, switch Outemu Blue, LED vermelho.", Price = 279.90m, Quantity = 35, CategoryId = 2, ImageUrl = "/images/products/default15.jpg", SalesPersonId = "1" };

        context.Products.Add(product14);
        context.SaveChangesAsync();
        var product15 = new Product { Title = "Patinete Elétrico Xiaomi Mi Pro 2", Description = "Patinete elétrico Xiaomi, autonomia de 45km, dobrável, motor potente.", Price = 3999.00m, Quantity = 8, CategoryId = 13, ImageUrl = "/images/products/default16.jpg", SalesPersonId = "1" };

        context.Products.Add(product15);
        context.SaveChangesAsync();
        var product16 =new Product { Title = "Roteador TP-Link Archer AX10", Description = "Roteador Wi-Fi 6, alta velocidade e maior capacidade de conexão.", Price = 499.90m, Quantity = 55, CategoryId = 14, ImageUrl = "/images/products/default17.jpg", SalesPersonId = "1" };

        context.Products.Add(product16);
        context.SaveChangesAsync();
        var product17 = new Product { Title = "Mochila Antifurto Impermeável", Description = "Mochila antifurto para notebook, resistente à água e moderna.", Price = 199.90m, Quantity = 80, CategoryId = 11, ImageUrl = "/images/products/default13.jpg", SalesPersonId = "1" };
        
        context.Products.Add(product17);
        context.SaveChangesAsync();
        var product18 = new Product { Title = "Fogão Atlas 4 Bocas Mônaco", Description = "Fogão de piso Atlas, acendimento automático, forno amplo.", Price = 1199.90m, Quantity = 14, CategoryId = 4, ImageUrl = "/images/products/default20.jpg", SalesPersonId = "1" };

        context.Products.Add(product18);
        context.SaveChangesAsync();
        var product19 = new Product { Title = "Bicicleta Caloi Vulcan Aro 29", Description = "Bicicleta Caloi para trilha, 21 marchas, quadro de alumínio.", Price = 2599.00m, Quantity = 7, CategoryId = 13, ImageUrl = "/images/products/default18.jpg", SalesPersonId = "1" };


        context.Products.Add(product19);
        context.SaveChangesAsync();
        var product20 =new Product { Title = "Liquidificador Mondial Turbo Power", Description = "Liquidificador potente, 12 velocidades, copo resistente e design moderno.", Price = 189.90m, Quantity = 90, CategoryId = 4, ImageUrl = "/images/products/default19.jpg", SalesPersonId = "1" };

        context.Products.Add(product20);
        context.SaveChangesAsync();


    }

}