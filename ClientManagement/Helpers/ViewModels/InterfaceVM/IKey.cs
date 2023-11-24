using System;

namespace ClientManagement.Helpers.ViewModels.InterfaceVM
{
    public interface IKey<TClientKey> where TClientKey : IEquatable<TClientKey>
    {
        TClientKey Id { get; set; }
    }
}
