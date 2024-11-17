using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using UdemyCarBook.Dto.BrandDTOS;
using UdemyCarBook.Dto.CarDTOS;

namespace UdemyCarBook.WebUI.Controllers
{
    public class AdminCarController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory; //istek yapmak için köprü görevi görür

        public AdminCarController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index() //Asenkron  kullanma sebebimiz metotlarımız asenkron aynı anda birden fazla işlem yapmamızı olanak sağlar
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7153/api/Cars/GetCarWithBrand"); //Api de consume işlemlerinde metota erişme görevi sağlar , verileri listeliyoruz , Bu adrese istek attık
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); //Response mesajdan gelen içeriği string formata dönüştür dedik , sonrasında oluşturduğumuz jsonData değişkeninin içine atadık
                var response = JsonConvert.DeserializeObject<CarResponseDto>(jsonData);
                var values = response.Result;
                return View(values);
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CreateCar()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7153/api/Brands/");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<List<ResultBrandDto>>(jsonData);

            List<SelectListItem> brandValues = (from x in value
                                                select new SelectListItem
                                                {
                                                    Text = x.Name,
                                                    Value = x.BrandID.ToString(),
                                                }).ToList();

            // ViewBag ile marka listesini view'a gönderiyoruz
            ViewBag.BrandValues = brandValues;

            // CreateCarDto nesnesi oluşturup, view'a model olarak gönderiyoruz
            var createCarDto = new CreateCarDto();

            return View(createCarDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar(CreateCarDto createCarDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createCarDto); //Gönderilecek değerleri serileze et metin tabanında ki veriyi json a çevirdik
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //inputlarda Türkçe karakter göndermemizi sağlar
            var responseMessage = await client.PostAsync("https://localhost:7153/api/Cars/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
          
        public async Task<IActionResult> DeleteCar(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7153/api/Cars/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet] // Endpoint'e ID parametresini ekliyoruz
        public async Task<IActionResult> UpdateCar(int id)
        {
            var client = _httpClientFactory.CreateClient();

            // Markalar listesini alıyoruz
            var responseMessage1 = await client.GetAsync("https://localhost:7153/api/Brands/");
            if (!responseMessage1.IsSuccessStatusCode)
            {
                // Eğer marka listesini alamadıysak, burada hata işleme yapabilirsiniz
                return View("Error"); // Hata görünümünü dönebiliriz
            }

            var jsonData1 = await responseMessage1.Content.ReadAsStringAsync();
            var brandList = JsonConvert.DeserializeObject<List<ResultBrandDto>>(jsonData1);

            // Marka listesini ViewBag'e atıyoruz
            ViewBag.BrandValues = brandList.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.BrandID.ToString(),
            }).ToList();

            // Güncellemek istediğimiz arabanın detaylarını alıyoruz
            var responseMessage = await client.GetAsync($"https://localhost:7153/api/Cars/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var carToUpdate = JsonConvert.DeserializeObject<UpdateCarDto>(jsonData); // Tekil nesne olarak deserialize et

                return View(carToUpdate); // Güncellenmek üzere nesneyi view'a gönderiyoruz
            }

            // Eğer araba bulunamazsa boş bir UpdateCarDto gönderiyoruz
            return View(new UpdateCarDto());
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCar(UpdateCarDto updateCarDto) //Dto içinde nelerin güncelleneceği verisini verdik
        {
            var client = _httpClientFactory.CreateClient(); //Yeni istek oluşturdukk
            var jsonData = JsonConvert.SerializeObject(updateCarDto);//Güncellenecek veriyi string formattan json formata dönüştür dedik.
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json"); //Yaptığımız jsondatayı encoding ile türkçe karakter olduğunu söylüyoruz.
            var responseMessage = await client.PutAsync("https://localhost:7153/api/Cars/", content);//Content burada içeriği ifade ediyor
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }




    }
}
