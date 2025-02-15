namespace Core.Interfaces;

/// <summary>
/// This is a marker interface
/// It's role is to convert a base entity as a DTO through our extension method toDto
/// This is used to get around a limitation in our BaseApiController.
/// For example we need to get orders for the admin in the format of OrderDTO, but it's not a base enity
/// and we need to use Orders as our base entity to return the list of order
/// 
/// We can then use this interface to convert the result from the Order format to OrderDTO format
/// </summary>
public interface IDtoConvertible
{

}