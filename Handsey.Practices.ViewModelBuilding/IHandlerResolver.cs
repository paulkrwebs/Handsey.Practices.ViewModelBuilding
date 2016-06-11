﻿namespace Handsey.Practices.ViewModelBuilding
{
    using System.Collections.Generic;

    public interface IHandlerResolver
    {
        IEnumerable<THandles> ResolverAll<THandles>() where THandles : IHandler;
    }
}
