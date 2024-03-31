﻿namespace IraqWebsite.ViewModels.Department
{
    public class CreateDepartmentDto
	{
		public string Name { get; set; } = string.Empty;
		public string NameAr { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string DescriptionAr { get; set; } = string.Empty;
		public IFormFile ImageOne { get; set; }
		public IFormFile ImageTwo { get; set; }
	}
}
