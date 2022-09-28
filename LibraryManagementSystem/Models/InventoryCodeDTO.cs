﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.EF_Models
{



    [ModelMetadataType(typeof(InventoryCodeDTO))]
    public partial class LmsInventoryCode
    {
        
    }


    public class InventoryCodeDTO
    {
        public int Id { get; set; }
        [Required]
        //[MaxLength(5,ErrorMessage = "Code can be 5 length")]
        [StringLength(5, ErrorMessage = "Code can be 5 lengthsssssssss")]
        [Remote("IsValidCode", "InventoryCode",ErrorMessage =("Code can be 5 length"))]
        public string? Code { get; set; }
    }
}
