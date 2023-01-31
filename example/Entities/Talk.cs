using System.ComponentModel.DataAnnotations;

namespace AspnetRunBasics.Entities
{
    public class Talk
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


        public string Review { get; set; }



        public int ProductId { get; set; }
        public Product Product { get; set; }


    }
}
