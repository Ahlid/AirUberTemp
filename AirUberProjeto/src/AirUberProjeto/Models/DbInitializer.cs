using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirUberProjeto.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AirUberProjeto.Models
{
    public class DbInitializer
    {

        public static void Initialize(AirUberDbContext context)
        {
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

        

            if (!context.Pais.Any())
            {

                context.Pais.Add(new Pais { Nome = "Portugal" });
                context.Pais.Add(new Pais { Nome = "Espanha" });
                context.Pais.Add(new Pais { Nome = "França" });
                context.Pais.Add(new Pais { Nome = "USA" });

                /*
                 * Added
                 *
                 */
                context.Cliente.Add(new Cliente
                {
                    Nome = "Artur",
                    Apelido = "Esteves",
                    Ativo = true,
                    DataCriacao = DateTime.Now, 
                    Contacto = "+351...",
                    Email = "artur@airuber.com"
                });

                context.Cliente.Add(new Cliente
                {
                    Nome = "João",
                    Apelido = "Rafael",
                    Ativo = true,
                    DataCriacao = DateTime.Now,
                    Contacto = "+351...",
                    Email = "joao@airuber.com"
                });


                context.SaveChanges();
            }




        }

        public static async Task AddRoles(IServiceProvider serviceProvider, AirUberDbContext context)
        {

            context.Database.EnsureCreated();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            

            if (!await roleManager.RoleExistsAsync(Roles.ROLE_CLIENTE))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.ROLE_CLIENTE));
            }

            if (!await roleManager.RoleExistsAsync(Roles.ROLE_COLABORADOR))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.ROLE_COLABORADOR));
            }
            if (!await roleManager.RoleExistsAsync(Roles.ROLE_COLABORADOR_ADMIN))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.ROLE_COLABORADOR_ADMIN));
            }

            if (!await roleManager.RoleExistsAsync(Roles.ROLE_HELPDESK))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.ROLE_HELPDESK));
            }



        }

    }

}

