namespace Supermarket.Ecommerce.Core.Entities;

public abstract class EntityBase //clase abstracta
{
    public int Id { get; set; } //id
    public bool IsDeleted { get; set; }  //borrador
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string UpdatedBy { get; set; } //para saber quién lo actualizó
    public DateTime UpdatedDate { get; set; } //para saber cuando lo actualizó
}

public class Test1 : EntityBase
{
    
}