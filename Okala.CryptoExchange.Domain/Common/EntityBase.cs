﻿namespace Okala.CryptoExchange.Domain.Common
{
    public abstract class EntityBase<TKey>
    {
        public TKey Id { get; set; }
    }
}
