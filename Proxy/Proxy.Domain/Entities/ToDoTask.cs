using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy.Domain.Entities
{
    public class ToDoTask
    {
        [Key]
        public int Id { get; set; }
        
        public int UserId { get; set; }
        
        public bool IsCompleted { get; set; }
        
        public string Name { get; set; }

        /// <summary>
        /// Task identifier in cloud
        /// </summary>
        public int? CloudId { get; set; }

        /// <summary>
        /// Create/delete task flag
        /// </summary>
        public bool Create { get; set; }
    }
}
