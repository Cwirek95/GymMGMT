﻿using MediatR;

namespace GymMGMT.Application.CQRS.Auth.Queries.GetUsersList
{
    public class GetUsersListQuery : IRequest<UsersInListViewModel>
    {
    }
}