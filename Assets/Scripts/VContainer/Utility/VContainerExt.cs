using Maze.Services.Repository;
using UnityEngine;
using VContainer;

namespace Maze.VContainer.Utility
{
    public static class VContainerExt
    {
        private static readonly AddressableRepository AddressableRepository = new AddressableRepository();
 
        public static RegistrationBuilder RegisterScriptableObject<TImpl>(this IContainerBuilder builder)
            where TImpl : Object
        {
            return RegisterScriptableObjectInternal<TImpl>(builder, typeof(TImpl).Name);
        }

        public static RegistrationBuilder RegisterScriptableObject<TImpl>(this IContainerBuilder builder, string key)
            where TImpl : Object
        {
            return RegisterScriptableObjectInternal<TImpl>(builder, key);
        }

        private static RegistrationBuilder RegisterScriptableObjectInternal<TImpl>(
            IContainerBuilder builder,
            string key
        )
            where TImpl : Object
        {
            var instance = AddressableRepository.Load<TImpl>(key);
            return builder.RegisterInstance(instance).AsSelf();
        }
    }
}