﻿using GymMGMT.Application.Caching;

namespace GymMGMT.Application.CQRS.Memberships.Queries.GetMembershipsList
{
    public class GetMembershipsListQuery : IRequest<IEnumerable<MembershipsInListViewModel>>, ICacheable
    {
        public string CacheKey => $"GetMembershipsList";
    }
}