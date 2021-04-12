using System;

namespace Backend.DAL.Models
{
    public class News : EntityBase
    {
        public virtual User Creator { get; set; }
        
        public DateTime CreatedTime { get; set; }
        
        public string Text { get; set; }
        
        
    }
}