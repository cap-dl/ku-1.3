using AutoMapper;
using Diware.SL.Pagination;
using Core;
using FluentResults.Extensions.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using PersonsApi.Models;
using FluentResults;
using ApiShared.Models;
using slJson = Diware.SL.SystemTextJsonModels.Pagination;

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
            var result = await personsService.GetPagedPersonsAsync(
                PageInfo.All(), ct);
            var rv = mapper.Map<RefResultModel<slJson.ListPage<PersonModel>>>(result);

            return Ok(rv);
        }


        [HttpGet("result-based/page/{p}")]
        public async Task<IActionResult> GetAllAsync_Result_Paged(
            int p,
            [FromQuery()]
            int i = 10,
            CancellationToken ct = default)
        {
            var pi = new PageInfo(p, i);

            var page = await personsService.GetPagedPersonsAsync(pi, ct);
            var rv = mapper.Map<RefResultModel<slJson.ListPage<PersonModel>>>(page);

            return Ok(rv);
        }


        [HttpGet("result-based-raw/{id}")]
        public async Task<IActionResult> GetOneAsync_ResultRaw(
            int id, CancellationToken ct)
        {
            var result = await personsService
                .GetPersonAsync(new PersonId(id), ct);

            return result != null
                ? Ok(result)
                : NotFound();
        }


        [HttpGet("result-based-built-in-model/{id}")]
        public async Task<IActionResult> GetOneAsync_Result_BuiltInModel(
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


        [HttpGet("result-based/{id}")]
        public async Task<IActionResult> GetOneAsync_Result(
            int id, CancellationToken ct)
        {
            var result = await personsService
                .GetPersonAsync(new PersonId(id), ct);

            var rv = mapper.Map<RefResultModel<PersonModel>>(result);

            return Ok(rv);
        }


        [HttpGet("result-based-built-in-model/nested-errors/{a:int}/{b:int}")]
        public async Task<IActionResult> GetNestedErrorsAsync_BuiltInModel(
            int a, int b,
            CancellationToken ct)
        {
            var rv = Divide(a, b);
            return rv.ToActionResult();
        }


        [HttpGet("result-based/nested-errors/{a:int}/{b:int}")]
        public async Task<IActionResult> GetNestedErrorsAsync(
            int a, int b,
            CancellationToken ct)
        {
            if (a == 404)
            {
                return NotFound("Cannot find the endpoint.");
            }


            var rv = Divide(a, b);
            var model = mapper.Map<ValueResultModel<int>>(rv);
            return Ok(model);
        }


        private Result<int> Divide(int a, int b)
        {
            var r = CheckNull(b);
            if (r.IsFailed)
            {
                return Result.Fail("Cannot divide!")
                    .WithErrors(r.Errors);
            }
            else
            {
                var rv = a / r.Value;
                return Result.Ok(rv);
            }

        }


        private Result<int> CheckNull(int number)
        {
            if (number != 0)
            {
                return Result.Ok(number);
            }
            else
            {
                return Result.Fail<int>("Divider is a zero.");
            }
        }
    }
}
