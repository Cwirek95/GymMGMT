﻿using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Auth.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<ICommandResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}