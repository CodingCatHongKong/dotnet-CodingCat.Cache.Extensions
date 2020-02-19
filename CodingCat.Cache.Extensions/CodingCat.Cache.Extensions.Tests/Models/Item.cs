using System;

namespace CodingCat.Cache.Extensions.Tests.Models
{
    public class Item
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
    }
}