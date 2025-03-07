﻿using System.ComponentModel;

namespace TPDeMVC02.Web.ViewModels.Brands
{
	public class BrandListVm
	{
		public int BrandId { get; set; }
		[DisplayName("Brand Name")]
		public string BrandName { get; set; } = null!;
		public bool Active { get; set; } = true;
		[DisplayName("Shoe Quantity")]
		public string? ImageUrl { get; set; }
		public int ShoesQuantity { get; set; }
	}
}
