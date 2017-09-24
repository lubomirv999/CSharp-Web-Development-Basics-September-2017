using System.Collections.Generic;

public class Order
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public Customer Customer { get; set; }

    public List<ItemOrder> Items { get; set; } = new List<ItemOrder>();
}