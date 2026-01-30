using FluentAssertions;
using NSubstitute;
using Xunit;

namespace OrderSystem.Tests;

public class CreateOrderUseCaseTests
{
    [Fact]
    public async void CriacaoOrdemTaxaAtual()
    {
        //Arrange
        var orderRepository = Substitute.For<IOrderRepository>();
        var featureFlagPort = Substitute.For<IFeatureFlagPort>();

        featureFlagPort.IsTaxReformEnabled().Returns(false);
        orderRepository.ExistsExternalOrderAsync(Arg.Any<int>()).Returns(false);

        var useCase = new CreateOrderUseCase(
            orderRepository,
            featureFlagPort,
            new CurrentTaxService(),
            new TaxReformService()
        );
        var listItems = new List<OrderItem>();
        listItems.Add(new OrderItem(1, 1, 1));
        var orderRequest = new OrderRequest() 
        {
            pedidoId = 1,
            clienetId = 1,
            items = listItems
        };
        //Act
        var id = await useCase.ExecuteAsync(orderRequest);

        //Assert
        id.Should().NotBeEmpty();
        await orderRepository.Received(1).SaveAsync(Arg.Any<Order>());
    }
    
    [Fact]
    public async void CriacaoOrdemTaxaReforma()
    {
        //Arrange
        var orderRepository = Substitute.For<IOrderRepository>();
        var featureFlagPort = Substitute.For<IFeatureFlagPort>();

        featureFlagPort.IsTaxReformEnabled().Returns(true);
        orderRepository.ExistsExternalOrderAsync(Arg.Any<int>()).Returns(false);

         var useCase = new CreateOrderUseCase(
            orderRepository,
            featureFlagPort,
            new CurrentTaxService(),
            new TaxReformService()
        );

         var listItems = new List<OrderItem>();
        listItems.Add(new OrderItem(1, 1, 1));
        var orderRequest = new OrderRequest() 
        {
            pedidoId = 2,
            clienetId = 1,
            items = listItems
        };
        //Act
        var id = await useCase.ExecuteAsync(orderRequest);

        //Assert
        id.Should().NotBeEmpty();
        await orderRepository.Received(1).SaveAsync(Arg.Any<Order>());
    }

    [Fact]
    public async void CriacaoOrdemDuplicada()
    {
        //Arrange
        var orderRepository = Substitute.For<IOrderRepository>();
        var featureFlagPort = Substitute.For<IFeatureFlagPort>();

        featureFlagPort.IsTaxReformEnabled().Returns(true);
        orderRepository.ExistsExternalOrderAsync(2).Returns(true);

         var useCase = new CreateOrderUseCase(
            orderRepository,
            featureFlagPort,
            new CurrentTaxService(),
            new TaxReformService()
        );

         var listItems = new List<OrderItem>();
        listItems.Add(new OrderItem(1, 1, 1));
        var orderRequest = new OrderRequest() 
        {
            pedidoId = 2,
            clienetId = 1,
            items = listItems
        };
        //Act
        Func<Task> act = async () => await useCase.ExecuteAsync(orderRequest);


        //Assert
        await act.Should().ThrowAsync<Exception>()
            .WithMessage("Pedido duplicado");
    }
}
