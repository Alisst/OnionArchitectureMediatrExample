using AutoMapper;
using MediatR;
using ProductApp.Application.Dto;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApp.Application.Features.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<ServiceResponse<GetProductIdViewModel>>
    {
        public Guid Id { get; set; }

        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ServiceResponse<GetProductIdViewModel>>
        {
            private readonly IProductRepository repository;
            private readonly IMapper mapper;

            public GetProductByIdQueryHandler(IProductRepository repository, IMapper mapper)
            {
                this.repository = repository;
                this.mapper = mapper;
            }

            public async Task<ServiceResponse<GetProductIdViewModel>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                var product = await repository.GetByIdAsync(request.Id);
                var dto = mapper.Map<GetProductIdViewModel>(product);
                return new ServiceResponse<GetProductIdViewModel>(dto);
            }
        }
    }
}
