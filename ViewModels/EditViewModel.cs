using Microsoft.AspNetCore.Http;
using MovieManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManagement.ViewModels
{
    public class EditViewModel:CreateViewModel
    {
        public int Id;
        public string ExistingPhotoPath { get; set; }
    }
}
