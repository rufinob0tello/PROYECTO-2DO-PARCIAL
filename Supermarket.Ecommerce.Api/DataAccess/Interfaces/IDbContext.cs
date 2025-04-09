using System.Data.Common;

namespace Supermarket.Ecommerce.Api.DataAccess.Interfaces;

public interface IDbContext
{
    DbConnection Connection { get; }
}