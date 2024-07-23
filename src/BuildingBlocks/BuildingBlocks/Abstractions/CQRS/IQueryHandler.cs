using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Abstractions.CQRS
{
    public interface IQueryHandler <in TQuery , TResponce> 
        : IRequestHandler<TQuery, TResponce>
        where TQuery : IQuery<TResponce>
        where TResponce : notnull
    {
    }
}
