namespace AgileFood.Application.Dtos.Consumptions;

public record RegisterConsumptionDto(
    string Cpf,
    string Pin,
    IReadOnlyCollection<RegisterConsumptionItemDto> Items
);
