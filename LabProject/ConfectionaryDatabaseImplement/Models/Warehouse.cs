﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConfectionaryDatabaseImplement.Models
{
    public class Warehouse
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Manager { get; set; }

        [Required]
        public DateTime DateCreate { get; set; }

        [ForeignKey("WarehouseId")]
        public virtual List<WarehouseComponent> WarehouseComponents { get; set; }
    }
}
