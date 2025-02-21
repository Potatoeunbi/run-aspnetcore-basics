﻿using System;
using System.Threading.Tasks;
using AspnetRunBasics.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics
{
    public class ProductDetailModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;

        public ProductDetailModel(IProductRepository productRepository, ICartRepository cartRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        }

        public IEnumerable<Entities.Talk> TalkList { get; set; } = new List<Entities.Talk>();

        public Entities.Product Product { get; set; }

        [BindProperty]
        public Entities.Talk Talk { get; set; }


        [BindProperty]
        public string Color { get; set; }

        [BindProperty]
        public int Quantity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            Product = await _productRepository.GetProductById(productId.Value);
            if (Product == null)
            {
                return NotFound();
            }

            TalkList = await _productRepository.GetTalkByProduct(productId.Value);

            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(int productId)
        {
            //if (!User.Identity.IsAuthenticated)
            //    return RedirectToPage("./Account/Login", new { area = "Identity" });

            await _cartRepository.AddItem("test", productId, Quantity, Color);
            return RedirectToPage("Cart");
        }

        public async Task<IActionResult> OnPostAddReviewAsync(string writer, string comment, int RproductId)
        {
            //if (!User.Identity.IsAuthenticated)
            //    return RedirectToPage("./Account/Login", new { area = "Identity" });


            await _productRepository.AddTalk(writer, comment, RproductId);

            Product = await _productRepository.GetProductById(RproductId);

            return RedirectToPage("ProductDetail");
        }


    }
}