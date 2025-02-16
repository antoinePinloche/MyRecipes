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
                throw new WrongParameterException("Invalide parameter", "parameter ID is invalide");
            }
            try
            {
                var result = await _sender.Send(guid.ToInstructionByIdQuery());
                return Ok(result.ToInstructionResponse());
            }
            catch (WrongParameterException ex)
            {
                throw new WrongParameterException(ex.Error, ex.Message);
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
                throw new WrongParameterException("Invalide parameter", "model is invalide");
            }
            try
            {
                await _sender.Send(model.ToCommand());
                return Created();
            }
            catch (WrongParameterException ex)
            {
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (InstructionAlreadyExisteException ex)
            {
                throw new InstructionAlreadyExisteException(ex.Error, ex.Message);
            }

        }

        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> CreateInstructionList(List<CreateInstructionModel> model)
        {
            if (!ModelState.IsValid)
            {
                throw new WrongParameterException("Invalide parameter", "model is invalide");
            }
            if (model.IsNullOrEmpty())
            {
                throw new WrongParameterException("Invalide parameter", "model is empty");
            }
            try
            {
                await _sender.Send(model.ToCommand());
                return Created();
            }
            catch (WrongParameterException ex)
            {
                throw new WrongParameterException(ex.Error, ex.Message);
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
                throw new WrongParameterException("Invalide parameter", "parameter ID is invalide");
            }
            if (!ModelState.IsValid)
            {
                throw new WrongParameterException("Invalide parameter", "model is invalide");
            }
            try
            {
                await _sender.Send(model.ToCommand(guid));
                return Created();
            }
            catch (WrongParameterException ex)
            {
                throw new WrongParameterException(ex.Error, ex.Message);
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
                throw new WrongParameterException("Invalide parameter", "parameter ID is invalide");
            }
            try
            {
                await _sender.Send(guid.ToDeleteInstructionCommand());
            }
            catch (WrongParameterException ex)
            {
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (InstructionNotFoundException ex)
            {
                throw new InstructionNotFoundException(ex.Error, ex.Message);
            }
            return Ok();
        }
    }
}
