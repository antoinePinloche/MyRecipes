using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyRecipes.Recipes.Application.Instruction.Command.DeleteInstruction;
using MyRecipes.Recipes.Application.Instruction.Query.GetAllInstruction;
using MyRecipes.Recipes.Application.Instruction.Query.GetInstructionById;
using MyRecipes.Transverse.Extension;
using MyRecipes.Web.API.Models.Class.Instruction;
using MyRecipes.Web.API.Mapper.Instruction;
using MyRecipes.Web.API.Mapper.RecipeIngredient;
using MyRecipes.Transverse.Exception;

namespace MyRecipes.Web.API.Controllers
{
    [ApiController]
    public class RecipeInstructionController : ControllerBase
    {
        private readonly ISender _sender;

        public RecipeInstructionController(ISender mediator)
        {
            _sender = mediator;
        }

        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> GetAllInstructionList()
        {
            var result = await _sender.Send(new GetAllInstructionQuery());
            return Ok(result.ToInstructionResponse());
        }

        [HttpGet]
        [Route("api/[controller]/[action]/{Id}")]
        public async Task<IActionResult> GetInstructionById(string Id)
        {
            if (!Guid.TryParse(Id, out Guid guid))
            {
                return BadRequest("GetInstructionById : BadParameter" + Id);
            }
            try
            {
                var result = await _sender.Send(guid.ToInstructionByIdQuery());
                return Ok(result.ToInstructionResponse());
            }
            catch (InstructionNotFoundException ex)
            {
                throw new InstructionNotFoundException(ex.Error, ex.Message);
            }
        }

        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> CreateInstruction(CreateInstructionModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }
            if (model.RecipeId.IsNullOrEmpty())
            {
                throw new Exception();
            }
            if (model.StepName.IsNullOrEmpty())
            {
            }
            if (model.StepInstruction.IsNullOrEmpty())
            {
            }
            await _sender.Send(model.ToCommand());
            return Created();
        }

        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> CreateInstructionList(List<CreateInstructionModel> model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }
            if (model.IsNullOrEmpty())
            {
                throw new Exception();
            }
            try
            {
                await _sender.Send(model.ToCommand());
                return Created();
            }
            catch (InstructionAlreadyExisteException ex)
            {
                throw new InstructionAlreadyExisteException(ex.Error, ex.Message);
            }
        }

        [HttpPut]
        [Route("api/[controller]/[action]/{Id}")]
        public async Task<IActionResult> UpdateInstruction(UpdateInstructionModel model, string Id)
        {
            if (!Guid.TryParse(Id, out Guid guid))
            {
                return BadRequest("GetInstructionById : BadParameter" + Id);
            }
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }
            if (model.StepName.IsNullOrEmpty())
            {
            }
            if (model.StepInstruction.IsNullOrEmpty())
            {
            }
            try
            {
                await _sender.Send(model.ToCommand(guid));
                return Created();
            }
            catch (InstructionNotFoundException ex)
            {
                throw new InstructionNotFoundException(ex.Error, ex.Message);
            }
            catch (InstructionAlreadyExisteException ex)
            {
                throw new InstructionAlreadyExisteException(ex.Error, ex.Message);
            }
        }

        [HttpDelete]
        [Route("api/[controller]/[action]/{Id}")]
        public async Task<IActionResult> DeleteInstructionById(string Id)
        {
            if (!Guid.TryParse(Id, out Guid guid))
            {
                return BadRequest("GetInstructionById : BadParameter" + Id);
            }
            try
            {
                await _sender.Send(guid.ToDeleteInstructionCommand());
            }
            catch (InstructionNotFoundException ex)
            {
                throw new InstructionNotFoundException(ex.Error, ex.Message);
            }
            return Ok();
        }
    }
}
