using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApp.Application.Dto;
using ProductApp.Application.Interfaces.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            /*
            Buradaki ilk sorun allList nesnesi List<Product> olarak dönmesi. Yani bizim api tarafından Core içindeki Product'ı dönmememiz gerekiyor.
            Ulaşmamızda sorun yok Onion architecture'a göre(Core'a ulaşabiliriz) ama dış dünyaya bizim db'deki objemizi vermeyelim.
            Dolayısıyla Product ile Application katmanında Dto altindaki ProductViewDto ile mapping yapmamız gerekiyor.
            Application altında mapping klasörü var ama dolu değil şu an. 
            Mesela mapping aşağıdaki gibi olsun;
             */
            var allList = await productRepository.GetAllAsync();
            var result = allList.Select(i => new ProductViewDto
            {
                Id = i.Id,
                Name = i.Name
            }).ToList();
            //Buradaki sorun bu convertion işleminden web api sorumlu değil.
            //Buradaki asıl olay Application tarafından bize ProductViewDto'nun ulaştırılması lazım.
            //Yani bu conversiton işlemi Application içinde olması gerekiyor.
            //Buradaki en iyi yol CQRS tasarımı, MediatR pattern'i ile yapabiliriz.
            return Ok(result);
        }
    }
}
