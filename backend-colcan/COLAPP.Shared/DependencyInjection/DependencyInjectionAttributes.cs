using System;

namespace COLAPP.Shared.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class ScopedAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class SingletonAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class TransientAttribute : Attribute { }
}
