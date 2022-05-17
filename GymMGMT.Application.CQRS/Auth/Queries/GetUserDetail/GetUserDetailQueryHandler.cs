using AutoMapper;
using GymMGMT.Application.Contracts.Repositories;
using MediatR;

namespace GymMGMT.Application.CQRS.Auth.Queries.GetUserDetail
{
    public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserDetailViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserDetailQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDetailViewModel> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdWithDetailsAsync(request.Id);

            return _mapper.Map<UserDetailViewModel>(user);
        }
    }
}
