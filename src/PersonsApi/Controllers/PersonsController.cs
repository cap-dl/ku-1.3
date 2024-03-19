using AutoMapper;
using Diware.SL.Pagination;
using Core;
using FluentResults.Extensions.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using PersonsApi.Models;
using FluentResults;

namespace PersonsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly PersonsService personsService;

        public PersonsController(
            IMapper mapper,
            PersonsService personsService)
        {
            this.mapper = mapper;
            this.personsService = personsService;
        }


        [HttpGet("no-model/all")]
        public async Task<IActionResult> GetAllAsync_NoModel(
            CancellationToken ct)
        {
            var items = await personsService.OldGetAllPersons(ct);
            return Ok(items);
        }


        [HttpGet("no-model/{id}")]
        public async Task<IActionResult> GetOneAsync_NoModel(
            int id, CancellationToken ct)
        {
            var person = await personsService
                .OldGetPersonAsync(new PersonId(id), ct);
            return person == null
                ? NotFound()
                : Ok(person);
        }


        [HttpGet("api-model/all")]
        public async Task<IActionResult> GetAllAsync_ApiModel(
            CancellationToken ct)
        {
            var items = await personsService
                .OldGetAllPersons(ct);
            var models = mapper.Map<IEnumerable<PersonModel>>(items);
            return Ok(models);
        }


        [HttpGet("api-model/{id}")]
        public async Task<IActionResult> GetOneAsync_ApiModel(
            int id,
            CancellationToken ct)
        {
            var person = await personsService
                .OldGetPersonAsync(new PersonId(id), ct);
            var rv = mapper.Map<PersonModel>(person);
            return rv == null
                ? NotFound()
                : Ok(rv);
        }


        [HttpGet("result-based/all")]
        public async Task<IActionResult> GetAllAsync_Result(
            CancellationToken ct)
        {
            var items = await personsService.OldGetPagedPersonsAsync(
                PageInfo.All(), ct);
            var models = mapper.Map<IEnumerable<PersonModel>>(items);
            //var rv = 
            throw new NotImplementedException();
        }


        [HttpGet("result-based/page/{p}")]
        public async Task<IActionResult> GetAllAsync_Result_Paged(
            int p,
            [FromQuery()]
            int i = 10,
            CancellationToken ct = default)
        {
            var pi = new PageInfo(p, i);

            var page = await personsService.OldGetPagedPersonsAsync(pi, ct);
            var rv = mapper.Map<ListPage<PersonModel>>(page);

            return Ok(rv);
        }


        [HttpGet("result-based/{id}")]
        public async Task<IActionResult> GetOneAsync_ResultRaw(
            int id, CancellationToken ct)
        {
            //IResult<PersonModel> rv;

            var result = await personsService
                .GetPersonAsync(new PersonId(id), ct);

            return result;
        }


        [HttpGet("result-based/{id}")]
        public async Task<IActionResult> GetOneAsync_Result(
            int id, CancellationToken ct)
        {
            //IResult<PersonModel> rv;

            var result = await personsService
                .GetPersonAsync(new PersonId(id), ct);

            var rv = result
                .Map(person => mapper.Map<PersonModel>(person))
                .ToActionResult();

            return rv;
        }
    }
}
