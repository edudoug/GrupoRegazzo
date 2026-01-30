📦 Order Management System - .NET 9
Este projeto é uma solução robusta para o processamento de pedidos entre sistemas externos, focada em alta volumetria (200k+ pedidos/dia), escalabilidade e manutenibilidade. A aplicação realiza o cálculo de impostos dinâmicos e garante a integridade dos dados através de validações de duplicidade.

🛠️ Stack Técnica
Runtime: .NET 9 SDK

Linguagem: C# 13 (Features: Primary Constructors, Collection Expressions)

Framework Web: ASP.NET Core Minimal APIs / Controllers

ORM: Entity Framework Core 9

Logs: Serilog (Structured Logging)

Documentação: OpenAPI (Scalar UI)

Testes: xUnit, NSubstitute, FluentAssertions e Bogus

Feature Management: Microsoft.FeatureManagement

🏗️ Arquitetura e Design
A solução foi desenhada seguindo os princípios da Clean Architecture e SOLID:

Domain: Contém as entidades e a lógica central de negócio (Strategy Pattern para impostos).

Application/API: Orquestra os casos de uso e expõe os endpoints RESTful.

Data: Camada de persistência utilizando Repository Pattern e EF Core.

Padrões Aplicados:
Strategy Pattern: Utilizado para alternar entre o imposto atual (30%) e a Reforma Tributária (20%).

Feature Flag: Controle dinâmico da estratégia de cálculo sem necessidade de novo deploy.

Object Calisthenics: Código escrito com foco em alta coesão e baixo acoplamento (indentação mínima, sem uso de else).

🚀 Como Executar
Pré-requisitos
.NET 9 SDK instalado.

Docker (opcional, para testes de integração).

Passo a Passo
Clone o repositório:

Bash
git clone https://github.com/edudoug/GrupoRegazzo.git
Restaure as dependências:

Bash
dotnet restore
Execute a aplicação:

Bash
dotnet run --project src/OrderSystem.API
Acesse a documentação interativa:

Scalar UI: http://localhost:5000/scalar/v1

🧪 Testes Automatizados
O projeto possui alta cobertura de testes focada em cenários críticos:

Bash
# Executar todos os testes
dotnet test
Testes Unitários: Validação das regras de imposto e lógica de duplicidade utilizando mocks.

Cenários Testados: * Cálculo de imposto padrão (30%).

Cálculo com Reforma Tributária ativa (20%).

Prevenção de pedidos com mesma referência externa.

📝 Configuração de Feature Flags
No arquivo appsettings.json, você pode alternar a regra de negócio:

JSON
"FeatureManagement": {
  "TaxReform": false // Mude para true para aplicar 20% de imposto
}
📈 Logging e Rastreabilidade
Utilizamos Serilog para logs estruturados. Isso permite que em um ambiente de produção (com ElasticSearch ou Seq), possamos rastrear cada pedido através do seu ExternalReference e identificar gargalos ou falhas de processamento rapidamente.

👨‍💻 Autor
Douglas - Desenvolvedor .NET Sênior