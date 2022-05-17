using AutoMapper;
using GymMGMT.Application.Contracts.Repositories;
using MediatR;

namespace GymMGMT.Application.CQRS.Auth.Queries.GetUsersList
{
    public class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, UsersInListViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUsersListQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UsersInListViewModel> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();

            return _mapper.Map<UsersInListViewModel>(users);
        }
    }
}
