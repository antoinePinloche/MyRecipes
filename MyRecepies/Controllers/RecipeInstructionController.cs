using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyRecipes.Recipes.Application.Ingredient.Query.GetAllIngredient;
using MyRecipes.Recipes.Application.Instruction.Command.CreateInstruction;
using MyRecipes.Recipes.Application.Instruction.Command.CreateListOfInstruction;
using MyRecipes.Recipes.Application.Instruction.Command.DeleteInstruction;
using MyRecipes.Recipes.Application.Instruction.Query.GetAllInstruction;
using MyRecipes.Recipes.Application.Instruction.Query.GetInstructionById;
using MyRecipes.Transverse.Extension;
using MyRecipes.Web.API.Models.Class.Ingredient;
using MyRecipes.Web.API.Models.Class.Instruction;
using MyRecipes.Web.API.Models.Class.RecipeIngredient;

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
            return Ok(await _sender.Send(new GetAllInstructionQuery()));
        }

        [HttpGet]
        [Route("api/[controller]/[action]/{Id}")]
        public async Task<IActionResult> GetInstructionById(string Id)
        {
            if (!Guid.TryParse(Id, out Guid guid))
            {
                return BadRequest("GetInstructionById : BadParameter" + Id);
            }
            return Ok(await _sender.Send(new GetInstructionByIdQuery(guid)));
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
            await _sender.Send(new CreateInstructionCommand(model.RecipeId, model.Step, model.StepName, model.StepInstruction));
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

            CreateListOfInstructionCommand command  = new CreateListOfInstructionCommand(
                model.Select(s =>
                    new CreateListOfInstructionCommand.Instruction(
                            s.RecipeId,
                            s.Step,
                            s.StepName,
                            s.StepInstruction
                            )
                    ).ToList());

            await _sender.Send(command);
            return Created();
        }

        [HttpPut]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> UpdateInstruction(UpdateRecipeIngredientModel model)
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
            await _sender.Send(new CreateInstructionCommand(model.RecipeId, model.Step, model.StepName, model.StepInstruction));
            return Created();
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
                await _sender.Send(new DeleteInstructionCommand(guid));
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            
            return Ok();
        }
    }
}
