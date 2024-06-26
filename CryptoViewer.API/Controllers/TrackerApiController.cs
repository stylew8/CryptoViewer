﻿using Microsoft.AspNetCore.Mvc;
using CryptoViewer.DAL.Models;
using CryptoViewer.DAL.Repositories;
using CryptoViewer.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using CryptoViewer.BL.Crypto.Interface;
using Microsoft.AspNetCore.Authorization;

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
        /// <summary>
        /// Retrieves a list of all available cryptocurrencies.
        /// </summary>
        /// <remarks>
        /// This method fetches all the available cryptocurrencies from the repository and returns them as a list of resources.
        /// </remarks>
        /// <response code="200">Returns a list of cryptocurrencies.</response>
        /// <response code="500">An error occurred while retrieving the cryptocurrencies.</response>
        [HttpGet(Name = "GetCryptocurrencies")]
        public async Task<IActionResult> GetCryptocurrencies()
        {
            var cryptocurrencies = await _repository.GetCryptocurrenciesAsync();
            var resources = cryptocurrencies.Select(c => ToResource(c));
            return Ok(resources);
        }
        /// <summary>
        /// Retrieves a specific cryptocurrency by its unique ID.
        /// </summary>
        /// <remarks>
        /// This method fetches a cryptocurrency from the repository by its ID and returns it as a resource. If the cryptocurrency is not found, a 404 Not Found response is returned.
        /// </remarks>
        /// <param name="id">The unique ID of the cryptocurrency to retrieve.</param>
        /// <response code="200">Returns the cryptocurrency resource.</response>
        /// <response code="404">Cryptocurrency not found.</response>
        /// <response code="500">An error occurred while retrieving the cryptocurrency.</response>
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
        /// <summary>
        /// Adds a new cryptocurrency to the repository.
        /// </summary>
        /// <remarks>
        /// This method allows an admin to add a new cryptocurrency by providing its details. If the addition is successful, a 201 Created response with the new cryptocurrency is returned. 
        /// If the model state is invalid, a 400 Bad Request response with the validation errors is returned.
        /// </remarks>
        /// <param name="model">The view model containing the cryptocurrency details to be added.</param>
        /// <response code="201">Cryptocurrency added successfully, returns the created cryptocurrency resource.</response>
        /// <response code="400">Model state is invalid, returns validation errors.</response>
        /// <response code="500">An error occurred while adding the cryptocurrency.</response>
        [HttpPost]
        [Authorize(Roles = "admin, user")]
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
        /// <summary>
        /// Updates an existing cryptocurrency in the repository.
        /// </summary>
        /// <remarks>
        /// This method allows an admin to update the details of an existing cryptocurrency. If the update is successful, a 200 OK response with the updated cryptocurrency is returned.
        /// If the cryptocurrency is not found, a 404 Not Found response is returned.
        /// If the model state is invalid, a 400 Bad Request response with the validation errors is returned.
        /// </remarks>
        /// <param name="id">The unique ID of the cryptocurrency to update.</param>
        /// <param name="model">The view model containing the updated cryptocurrency details.</param>
        /// <response code="200">Cryptocurrency updated successfully, returns the updated cryptocurrency resource.</response>
        /// <response code="400">Model state is invalid, returns validation errors.</response>
        /// <response code="404">Cryptocurrency not found.</response>
        /// <response code="500">An error occurred while updating the cryptocurrency.</response>
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateCryptocurrency(int id, AddCryptocurrencyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var cryptocurrency = (await _repository.GetCryptocurrenciesAsync()).FirstOrDefault(c => c.Id == id);
                if (cryptocurrency == null)
                {
                    return NotFound();
                }
                cryptocurrency.Id = id;
                cryptocurrency.Name = model.Name;
                cryptocurrency.LogoPath = model.LogoPath;
                cryptocurrency.TrackerAction = model.TrackerAction;
                cryptocurrency.BorderColor = model.BorderColor;

                await _repository.UpdateCryptocurrencyAsync(cryptocurrency);
                var resource = ToResource(cryptocurrency);

                return Ok(resource);
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Deletes an existing cryptocurrency from the repository.
        /// </summary>
        /// <remarks>
        /// This method allows an admin to delete an existing cryptocurrency by its ID. If the deletion is successful, a 204 No Content response is returned.
        /// If the cryptocurrency is not found, a 404 Not Found response is returned.
        /// </remarks>
        /// <param name="id">The unique ID of the cryptocurrency to delete.</param>
        /// <response code="204">Cryptocurrency deleted successfully.</response>
        /// <response code="404">Cryptocurrency not found.</response>
        /// <response code="500">An error occurred while deleting the cryptocurrency.</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteCryptocurrency(int id)
        {
            var cryptocurrency = (await _repository.GetCryptocurrenciesAsync()).FirstOrDefault(c => c.Id == id);
            if (cryptocurrency == null)
            {
                return NotFound();
            }

            await _repository.DeleteCryptocurrencyAsync(id);
            return NoContent();
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
