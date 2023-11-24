﻿using ClientManagement.Helpers.Interfaces.BaseInterfaces;
using ClientManagement.Helpers.ViewModels;
using System;

namespace ClientManagement.Helpers.Interfaces
{
    public interface IBranch<TIdentityKey,TClientKey> : IGeneral<BranchVM<TClientKey>, TClientKey>
        where TIdentityKey : IEquatable<TIdentityKey>
        where TClientKey : IEquatable<TClientKey> //, IUpdate<BranchVM>
    {
    }
}
