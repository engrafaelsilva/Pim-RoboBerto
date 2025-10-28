using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IA_RoboBerto.Modelos;
using IA_RoboBerto.Repositorio.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public static class RoboBertoDbInitializer
{
    public static async Task SeedAsync(RoboBertoContext context)
    {
        var hasher = new PasswordHasher<Usuario>();

        // evita inserir duplicado
        if (await context.Chamado.AnyAsync()) return;

        // Departamentos
        var depTI = new Departamento { Nome = "TI" };
        var depRH = new Departamento { Nome = "RH" };
        var depFinanceiro = new Departamento { Nome = "Financeiro" };
        var depDefault = new Departamento { Nome = "DEFAULT" };

        await context.Departamento.AddRangeAsync(depTI, depRH, depFinanceiro,depDefault);
        await context.SaveChangesAsync();

        // Roles
        var roleADM = new Role { Nome = "ADM" };
        var roleTecnico = new Role { Nome = "TECNICO" };
        var roleColab = new Role { Nome = "COLABORADOR" };

        await context.Role.AddRangeAsync(roleADM, roleTecnico, roleColab);
        await context.SaveChangesAsync();

        // Usuários
        var userAlice = new Usuario
        {
            Nome = "Alice",
            Email = "alice@empresa.com",
            Telefone = "11999999999",
            DataCriacao = DateTime.UtcNow,
            Departamento = depTI,
            Roles = new HashSet<Role> { roleADM }
        };
        userAlice.SenhaHash = hasher.HashPassword(userAlice, "senha123");

        var userBob = new Usuario
        {
            Nome = "Bob",
            Email = "bob@empresa.com",
            Telefone = "11988888888",
            DataCriacao = DateTime.UtcNow,
            Departamento = depRH,
            Roles = new HashSet<Role> { roleTecnico }
        };
        userBob.SenhaHash = hasher.HashPassword(userBob, "senha123");

        var userCarlos = new Usuario
        {
            Nome = "Carlos",
            Email = "carlos@empresa.com",
            Telefone = "11977777777",
            DataCriacao = DateTime.UtcNow,
            Departamento = depFinanceiro,
            Roles = new HashSet<Role> { roleColab }
        };
        userCarlos.SenhaHash = hasher.HashPassword(userCarlos, "senha123");

        await context.Usuario.AddRangeAsync(userAlice, userBob, userCarlos);
        await context.SaveChangesAsync();

        // Categorias
        var catSuporte = new Categoria { Nome = "Suporte" };
        var catInfra = new Categoria { Nome = "Infraestrutura" };
        var catFinanceiro = new Categoria { Nome = "Financeiro" };

        await context.Categoria.AddRangeAsync(catSuporte, catInfra, catFinanceiro);
        await context.SaveChangesAsync();

        // Chamados com mensagens interativas (5 mensagens cada)
        var now = DateTime.UtcNow;

        var chamado1 = new Chamado
        {
            Autor = userCarlos,      // colaborador abriu
            Tecnico = userBob,       // técnico
            Categoria = catSuporte,
            Titulo = "Computador não inicializa",
            Status = (IA_RoboBerto.Models.Enums.EStatusChamado)1,
            SugestaoGemini = "Reiniciar",
            Descricao = "Ao ligar o PC fica na tela preta após POST.",
            DataAbertura = now.AddHours(-5),
            Prioridade = (IA_RoboBerto.Models.Enums.EPrioridade)2,
            SlaVenceEm = now.AddHours(19),
            SugestaoResolveu = null,
            Mensagens = new HashSet<Mensagem>()
        };

        // Mensagens interativas (colaborador <-> técnico)
        chamado1.Mensagens.Add(new Mensagem
        {
            Texto = "Abri um chamado porque o computador fica na tela preta logo após ligar.",
            Autor = userCarlos,
            DataHoraMensagem = now.AddHours(-5).AddMinutes(1)
        });
        chamado1.Mensagens.Add(new Mensagem
        {
            Texto = "Você já tentou retirar dispositivos USB e reiniciar? Me descreve o que aparece na tela.",
            Autor = userBob,
            DataHoraMensagem = now.AddHours(-5).AddMinutes(5)
        });
        chamado1.Mensagens.Add(new Mensagem
        {
            Texto = "Sim, removi os dispositivos. Só aparece um cursor piscando no canto superior esquerdo.",
            Autor = userCarlos,
            DataHoraMensagem = now.AddHours(-5).AddMinutes(10)
        });
        chamado1.Mensagens.Add(new Mensagem
        {
            Texto = "Ok — pode abrir a tampa e me enviar foto do LED/BIOS boot ou dizer o modelo da máquina?",
            Autor = userBob,
            DataHoraMensagem = now.AddHours(-5).AddMinutes(20)
        });
        chamado1.Mensagens.Add(new Mensagem
        {
            Texto = "Modelo: Dell XPS 15. Vou anexar a foto (segue em seguida).",
            Autor = userCarlos,
            DataHoraMensagem = now.AddHours(-5).AddMinutes(30)
        });

        var chamado2 = new Chamado
        {
            Autor = userAlice,
            Tecnico = userBob,
            Categoria = catFinanceiro,
            Titulo = "Erro ao gerar relatório financeiro",
            Status = (IA_RoboBerto.Models.Enums.EStatusChamado)1,
            SugestaoGemini = "Verificar logs",
            Descricao = "Ao gerar relatório, ocorre uma exceção de index out of range.",
            DataAbertura = now.AddDays(-1),
            Prioridade = (IA_RoboBerto.Models.Enums.EPrioridade)1,
            SlaVenceEm = now.AddDays(1),
            SugestaoResolveu = null,
            Mensagens = new HashSet<Mensagem>()
        };

        // Mensagens interativas para o segundo chamado
        chamado2.Mensagens.Add(new Mensagem
        {
            Texto = "Ao tentar gerar o relatório de faturamento aparece IndexOutOfRange na linha 132.",
            Autor = userAlice,
            DataHoraMensagem = now.AddDays(-1).AddMinutes(2)
        });
        chamado2.Mensagens.Add(new Mensagem
        {
            Texto = "Você pode me enviar o arquivo de log correspondente ao horário do erro?",
            Autor = userBob,
            DataHoraMensagem = now.AddDays(-1).AddMinutes(10)
        });
        chamado2.Mensagens.Add(new Mensagem
        {
            Texto = "Enviei o log. O erro aparece quando filtro por cliente X.",
            Autor = userAlice,
            DataHoraMensagem = now.AddDays(-1).AddMinutes(20)
        });
        chamado2.Mensagens.Add(new Mensagem
        {
            Texto = "Recebi. Vou tentar reproduzir localmente e te passo o horário do teste.",
            Autor = userBob,
            DataHoraMensagem = now.AddDays(-1).AddMinutes(40)
        });
        chamado2.Mensagens.Add(new Mensagem
        {
            Texto = "Beleza, fico no aguardo. Obrigado!",
            Autor = userAlice,
            DataHoraMensagem = now.AddDays(-1).AddMinutes(60)
        });

        await context.Chamado.AddRangeAsync(chamado1, chamado2);
        await context.SaveChangesAsync();

        // opcional: buscar e ajustar shadow FKs (não necessário se usar nav props)
        // tudo salvo
    }
}
