namespace AgileFood.Business.Exceptions;

/// <summary>
/// Violacao de regra de negocio provocada pelos dados da requisicao.
/// Representa erro esperado do cliente - nao bug interno - e a camada de API
/// a traduz para 400 com a mensagem exposta ao chamador.
/// </summary>
public class DomainException : Exception
{
    public DomainException(string message) : base(message)
    {
    }
}
