using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Abstractions.CQRS
{
    public interface IQuery<out TResponce> : IRequest<TResponce>
        where TResponce : notnull
    {
    }
}
