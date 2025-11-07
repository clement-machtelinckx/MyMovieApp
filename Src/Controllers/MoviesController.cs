using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using MovieAppApi.Src.Services.Env;
using MovieAppApi.Src.Views.DTO.SearchMovies;
using MovieAppApi.Src.Models.SearchMovies;

namespace MovieAppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IEnvService _envService;

        public MovieController(IHttpClientFactory httpClientFactory, IEnvService envService)
        {
            _httpClient = httpClientFactory.CreateClient();
            _envService = envService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies([FromQuery] SearchMoviesRequestQueryDto dto)
        {
            // Validation côté serveur
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // DTO → Model
            var model = new SearchMoviesRequestQueryModel(dto.search_term, dto.language);

            // Récupère ton token
            string accessToken = _envService.Get("TMDB_API_KEY");

            // 🔗 Construis l’URL TMDB avec les bons params
            string url = $"https://api.themoviedb.org/3/search/movie?query={model.SearchTerm}&language={model.Language}";

            // Préparer la requête
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Authorization", $"Bearer {accessToken}");
            request.Headers.Add("Accept", "application/json");

            // Exécuter la requête
            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, content);

            return Content(content, "application/json");
        }
    }
}
