using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspnetRunBasics.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;

        public IndexModel(IProductRepository productRepository, ICartRepository cartRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        }

        public IEnumerable<Entities.Product> ProductList { get; set; } = new List<Entities.Product>();
        public IEnumerable<Entities.Category> CategoryList { get; set; } = new List<Entities.Category>();

        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; }
        public async Task<IActionResult> OnGetAsync(int? categoryId)
        {
            CategoryList = await _productRepository.GetCategories();
            if (categoryId.HasValue)
            {
                ProductList = await _productRepository.GetProductByCategory(categoryId.Value);
                SelectedCategory = CategoryList.FirstOrDefault(c => c.Id == categoryId.Value)?.Name;
            }
            else
            {
                ProductList = await _productRepository.GetProducts();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(int productId)
        {

            await _cartRepository.AddItem("test", productId);
            return RedirectToPage("Cart");
        }
    }
}
