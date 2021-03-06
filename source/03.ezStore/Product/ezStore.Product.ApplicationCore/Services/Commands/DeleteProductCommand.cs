﻿using Ws4vn.Microservices.ApplicationCore.Commands;
using System;

namespace ezStore.Product.ApplicationCore.Services.Commands
{
    public class DeleteProductCommand : ValidationDecoratorCommand
    {
        public Guid Id { get; set; }

        public DeleteProductCommand(Guid id)
        {
            this.Id = id;
        }
        public override bool SelfValidate()
        {
            return true;
        }
    }
}
