using Microsoft.AspNetCore.Mvc;
using CryptoViewer.DAL.Models;
using CryptoViewer.DAL.Repositories;
using CryptoViewer.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using CryptoViewer.BL.Crypto.Interface;

namespace CryptoViewer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackerApiController : ControllerBase
    {
        private readonly ICrypto _repository;

        public TrackerApiController(ICrypto repository)
        {
            _repository = repository;
        }

        [HttpGet(Name = "GetCryptocurrencies")]
        public async Task<IActionResult> GetCryptocurrencies()
        {
            var cryptocurrencies = await _repository.GetCryptocurrenciesAsync();
            var resources = cryptocurrencies.Select(c => ToResource(c));
            return Ok(resources);
        }

        [HttpGet("{id}", Name = "GetCryptocurrency")]
        public async Task<IActionResult> GetCryptocurrency(int id)
        {
            var cryptocurrency = (await _repository.GetCryptocurrenciesAsync()).FirstOrDefault(c => c.Id == id);
            if (cryptocurrency == null)
            {
                return NotFound();
            }
            var resource = ToResource(cryptocurrency);
            return Ok(resource);
        }

        [HttpPost]
        public async Task<IActionResult> AddCryptocurrency(AddCryptocurrencyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var crypto = new Cryptocurrency
                {
                    Name = model.Name,
                    LogoPath = model.LogoPath,
                    TrackerAction = model.TrackerAction,
                    BorderColor = model.BorderColor
                };

                await _repository.AddCryptocurrencyAsync(crypto);
                var resource = ToResource(crypto);

                return CreatedAtRoute("GetCryptocurrency", new { id = crypto.Id }, resource);
            }

            return BadRequest(ModelState);
        }

        private CryptocurrencyResource ToResource(Cryptocurrency crypto)
        {
            var resource = new CryptocurrencyResource
            {
                Id = crypto.Id,
                Name = crypto.Name,
                LogoPath = crypto.LogoPath,
                TrackerAction = crypto.TrackerAction,
                BorderColor = crypto.BorderColor
            };

            resource.Links.Add(new LinkResource
            {
                Href = Url.Link("GetCryptocurrency", new { id = crypto.Id }),
                Rel = "self",
                Method = "GET"
            });

            resource.Links.Add(new LinkResource
            {
                Href = Url.Link("GetCryptocurrencies", null),
                Rel = "all",
                Method = "GET"
            });

            return resource;
        }
    }
}
