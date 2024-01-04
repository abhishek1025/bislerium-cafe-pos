using System;
namespace bislerium_cafe_pos.Models
{
	public class AddIns
	{
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public double Price { get; set; }
    }
}

