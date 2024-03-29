﻿using FluentValidation;
using Patronage.Api.MediatR.Board.Commands.UpdateLight;

namespace Patronage.Api.Validators.Board
{
    public class UpdateBoardLightCommandValidator : AbstractValidator<UpdateBoardLightCommand>
    {
        public UpdateBoardLightCommandValidator(PartialBoardValidator partialBoardValidator)
        {
            RuleFor(x => x.Data).SetValidator(partialBoardValidator);
        }
    }
}