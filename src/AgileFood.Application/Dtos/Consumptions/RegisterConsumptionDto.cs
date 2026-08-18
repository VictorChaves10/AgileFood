namespace AgileFood.Application.Dtos.Consumptions;

public record RegisterConsumptionDto(
    string EmployeeCode,
    string Pin,
    IReadOnlyCollection<RegisterConsumptionItemDto> Items
);
