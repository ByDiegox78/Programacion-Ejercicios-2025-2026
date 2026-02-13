namespace Lol.Errors.Common;

public abstract class DomainExceptions(string message) : Exception(message);