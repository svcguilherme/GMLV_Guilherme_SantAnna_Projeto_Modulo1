# Feedback - Avaliação Geral

## Front End

### Navegação
  * Pontos positivos:
    - Projeto MVC com controllers e views organizadas para categorias, produtos e autenticação.
    - CRUD completo funcional nas interfaces web.

  * Pontos negativos:
    - Utilização de `Session` no MVC compromete escalabilidade.

### Design
  - Layout limpo e coerente com o contexto administrativo.

### Funcionalidade
  * Pontos positivos:
    - CRUD funcional para produtos, categorias e vendedores em ambas as camadas.
    - Autenticação com ASP.NET Identity integrada.
    - Implementação correta da criação do vendedor com ID compartilhado no momento do registro.
    - Migrations e seed de dados automatizados com SQLite.
    - Modelagem adequada com abstrações e separações coerentes.

  * Pontos negativos:
    - Produto não é vinculado ao vendedor logado, o que compromete controle de propriedade.
    - Qualquer vendedor pode editar produtos de outro.
    - Seed duplicado na API e MVC, aumentando risco de manutenção.
    - Código, nomes e entidades em inglês, descumprindo o padrão exigido pelo desafio.

## Back End

### Arquitetura
  * Pontos positivos:
    - Arquitetura com camadas bem separadas: API, AppWeb, Application, Domain, Infra.
    - Uso de abstrações, repositórios, services e injeção de dependência bem estruturados.

  * Pontos negativos:
    - Complexidade excessiva para o escopo do desafio, com camadas realizando apenas encaminhamento de chamadas sem lógica relevante.
    - Overengineering com impacto direto na simplicidade, legibilidade e manutenibilidade.

### Funcionalidade
  * Pontos positivos:
    - Funcionalidade geral conforme escopo com entrega de operações principais.

  * Pontos negativos:
    - Ausência de regras para controle de propriedade de produto.

### Modelagem
  * Pontos positivos:
    - Separação de entidades, modelos e responsabilidades bem aplicada.
    - Uso de boas práticas de modelagem no domínio.

  * Pontos negativos:
    - Session como armazenamento de estado de vendedor.

## Projeto

### Organização
  * Pontos positivos:
    - Estrutura modular em `src`, `.sln` organizado, separação de arquivos bem feita.
    - Presença de `README.md` e `FEEDBACK.md`.

  * Pontos negativos:
    - Seed de dados duplicado.

### Documentação
  * Pontos positivos:
    - Documentação adequada, com instruções e explicações claras.

  * Pontos negativos:
    - Nenhum.

### Instalação
  * Pontos positivos:
    - Uso de SQLite, migrations e seed automatizado.
    - Execução simples e funcional do projeto.

  * Pontos negativos:
    - Duplicação de lógica de seed nos dois ambientes.

---

# 📊 Matriz de Avaliação de Projetos

| **Critério**                   | **Peso** | **Nota** | **Resultado Ponderado**                  |
|-------------------------------|----------|----------|------------------------------------------|
| **Funcionalidade**            | 30%      | 8        | 2,4                                      |
| **Qualidade do Código**       | 20%      | 8        | 1,6                                      |
| **Eficiência e Desempenho**   | 20%      | 7        | 1,4                                      |
| **Inovação e Diferenciais**   | 10%      | 8        | 0,8                                      |
| **Documentação e Organização**| 10%      | 10       | 1,0                                      |
| **Resolução de Feedbacks**    | 10%      | 8        | 0,8                                      |
| **Total**                     | 100%     | -        | **8,0**                                  |

## 🎯 **Nota Final: 8 / 10**
